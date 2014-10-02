//********************************************************************************
//NAME         : Engine.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : Wrapper for retail business object DLLs.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/Engine.cs-arc  $
//
//   Rev 1.1   Jan 11 2009 16:58:38   mmodi
//Added NeedsTerminating flag to allow an engine to be forced stopped if running
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:46:02   mturner
//Initial revision.
//
//   Rev 1.7   May 20 2005 12:17:10   RPhilpott
//1) Do not disable timeout chaecking during unmanaged DLL calls.
//
//2) Always do stop/restart (TE/IN calls) on timed-out engine before returning it to the free pool.
//
//3) Do not do stop (TE) during engine disposal. 
//Resolution for 2511: PT costing - RBO initialisation failure
//
//   Rev 1.6   Apr 23 2005 12:39:48   RPhilpott
//Allow for RVBO reinitialisation after NRS comms error.
//Resolution for 2301: PT - RVBO discontinues comms with NRS after error
//
//   Rev 1.5   Apr 20 2005 10:32:12   RPhilpott
//Add more thorough and robust error handling.
//Resolution for 2247: PT: error handling by Retail Business Objects
//
//   Rev 1.4   Mar 30 2005 09:15:54   rscott
//Updated after running FXCop
//
//   Rev 1.3   Feb 16 2005 16:36:44   jmorrissey
//Start method is now virtual. Also fixed up CLS compliant warnings.
//
//   Rev 1.2   Dec 11 2003 16:31:06   geaton
//Added additonal exception data when logging engine failure details.
//
//   Rev 1.1   Oct 29 2003 19:46:10   geaton
//Updated comments.
//
//   Rev 1.0   Oct 28 2003 20:04:04   geaton
//Initial Revision

using System;
using System.Text;
using System.Diagnostics;
using System.Threading;

using TransportDirect.Common;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Wrapper for retail business object dll.
	/// </summary>	
	public class Engine : IDisposable
	{
		/// <summary>
		/// Function IDs used to perform operations on business objects.
		/// </summary>
		protected const string InitialiseFunctionId = "IN";
		protected const string TerminateFunctionId	= "TE";
		protected const string GetStateFunctionId	= "GS";
		protected const string HousekeepFunctionId	= "HK";

		protected const int GetStateParameterLength = 304;

		private EngineAllocationIdentifier engAllocationId;
		/// <summary>
		/// Gets the engine allocation identifier.
		/// This is used by business objects to determine 
		/// if the engine initially allocated to it can still be used.
		/// It is needed in addition to the isTimedOut flag because an engine that
		/// has timed out may be re-added to the free list before the business
		/// object, to which it was initially allocated, uses it.
		/// </summary>
		public EngineAllocationIdentifier AllocationId
		{
			get{return engAllocationId;}
		}

		/// <summary>
		/// True if timeouts are enabled on engine.
		/// </summary>
		protected bool timeoutEnabled;

		/// <summary>
		/// Duration (of no usage) after which engine is taken as timed out.
		/// </summary>
		protected TimeSpan timeout;

		private bool isTimedOut;
		/// <summary>
		/// Gets timed out flag. True if engine has timed out, else false.
		/// </summary>
		public bool TimedOut
		{
			get{return isTimedOut;}
		}

		/// <summary>
		/// Timer used to determine timeouts.
		/// </summary>
		protected Timer idleTimer;

		private int engineId;
		/// <summary>
		/// Unique identifier of engine.
		/// </summary>
		public int Id
		{
			get{ return engineId; }
		}
		
		private int dataSequenceNumber;
		/// <summary>
		/// Gets and sets the sequence number of the data used by the underlying engine dll.
		/// Used to control houeskeeping updates of the data used by the dll.
		/// </summary>
		public int DataSequence
		{
			get {return dataSequenceNumber;}
			set {dataSequenceNumber = value;}
		}

		/// <summary>
		/// The interface version of the underlying dll used to run engine.
		/// </summary>
		protected string interfaceVersion;

		/// <summary>
		/// The object identifier of the underlying dll used to run engine.
		/// </summary>
		protected string objectId;

		/// <summary>
		/// Access wrapper to the underlying dll used to run engine.
		/// </summary> 
		protected LegacyBusinessObjectWrapper dllWrapper;

		/// <summary>
		/// True if instance has been disposed.
		/// </summary>
		private bool disposed;
		/// <summary>
		/// Returns and sets private property 
		/// </summary>		
		public bool Disposed
		{
			get{return disposed;}
			set{disposed = value;}
		}


		protected bool isRunning;
		/// <summary>
		/// Gets the running indicator.
		/// True if engine has been intialised successfully and is running, else false.
		/// </summary>
		public bool Running
		{
			get{return isRunning;}
		}

		/// <summary>
		/// Updates the engines allocation identifier.
		/// </summary>
		public void UpdateAllocationId()
		{
			this.engAllocationId = new EngineAllocationIdentifier(true);
		}

		protected bool reinitialisationIsNeeded;

		/// <summary>
		/// Indicates that reinitialisation is needed (other than for housekeeping)
		///  - for example, to recover from an error condition (such as RVBO comms failure)
		/// </summary>
		public bool NeedsReinitialisation
		{
			get { return reinitialisationIsNeeded; }
			set { reinitialisationIsNeeded = value; }
		}

        protected bool terminateEngineNeeded;

        public bool NeedsTerminating
        {
            get { return terminateEngineNeeded; }
            set { terminateEngineNeeded = value; }
        }

		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="id">Unique id of engine.</param>
		/// <param name="dllWrapper">DLL Engine to be used by business object.</param>
		/// <param name="timeout">Duration after which business object has timed out.</param>
		/// <param name="dllObjectId">Object id associated with underlying dll.</param>
		/// <param name="interfaceVersion">Interface version of engine.</param>
		/// <param name="dataSequenceNumber">Data sequence number that business object is using.</param>
		/// <param name="timeout">Duration (of no usage) after which engine is taken as timed out.</param>
		public Engine(int engineId,
					  LegacyBusinessObjectWrapper dllWrapper,
					  string dllObjectId,
					  string interfaceVersion,
					  int dataSequenceNumber,
					  TimeSpan timeout)
		{
			this.objectId = dllObjectId;
			this.interfaceVersion = interfaceVersion;
			this.dataSequenceNumber = dataSequenceNumber;
			this.disposed = false;
			this.engineId = engineId;
			this.dllWrapper = dllWrapper;
			this.isTimedOut = false;
			this.timeout = timeout;
			this.timeoutEnabled = false;
			this.engAllocationId = new EngineAllocationIdentifier(true);
			this.idleTimer = new Timer(new TimerCallback(this.IdleTimeoutCallback), null, Timeout.Infinite, Timeout.Infinite);
		}

		/// <summary>
		/// Converts a byte array into a string. Used to call underlying engine dll.
		/// </summary>
		/// <param name="bytes">Byte array to convert.</param>
		/// <returns>Byte array in string format.</returns>
		protected String ByteArrayToString(byte[] bytes) 
		{
			StringBuilder output = new StringBuilder();
			
			for (int j = 0; j<bytes.Length;j++) 
			{
				if ( ( (byte)bytes[j] > 127 ) || ( (byte)bytes[j] < 32) )
				{
					output.Append( " " );
				}
				else 
				{
					output.Append( (char)bytes[j] );
				}
			}
			
			return output.ToString();
		}

		/// <summary>
		/// Starts the engine.
		/// </summary>
		/// <param name="severity">Error severity returned.</param>
		/// <param name="code">Error code returned.</param>
		/// <returns>True if engine was started successfully, else false.</returns>
		/// <exception cref="TDException">
		/// Failure when starting the engine.
		/// </exception>
		public virtual bool Start(out string severity, out string code)
		{
			BusinessObjectInput inputHeader = 
				new BusinessObjectInput(this.objectId,
										InitialiseFunctionId,
										this.interfaceVersion);
			BusinessObjectOutput outputHeader;
			bool failed = false;

			outputHeader = Run(inputHeader);

			code = outputHeader.ErrorCode;
			severity = outputHeader.ErrorSeverity;

			// If critical or error then assume that the engine could not be started.
			if (severity.Equals(Messages.ErrorSeverityCritical) || severity.Equals(Messages.ErrorSeverityError))
			{
				failed = true;
			}

			if (!failed)
			{
				this.isRunning = true;
				this.reinitialisationIsNeeded = false;
			}

			return (!failed);
		}

		/// <summary>
		/// Runs housekeeping on the underlying engine dll.
		/// </summary>
		/// <param name="severity">Severity returned when housekeeping.</param>
		/// <param name="code">Code returned when housekeeping.</param>
		/// <exception cref="TDException">
		/// Failure when housekeeping the engine.
		/// </exception>
		public bool Housekeep(out string severity, out string code)
		{
			BusinessObjectInput inputHeader = 
				new BusinessObjectInput(this.objectId,
									    HousekeepFunctionId,
										this.interfaceVersion);
			BusinessObjectOutput outputHeader;
			bool failed = false;

			outputHeader = Run(inputHeader);

			code = outputHeader.ErrorCode;
			severity = outputHeader.ErrorSeverity;

			// If critical or error then assume that housekeeping failed on the engine.
			if (severity.Equals(Messages.ErrorSeverityCritical) || severity.Equals(Messages.ErrorSeverityError))
			{
				failed = true;
			}

			return (!failed);
		}

		/// <summary>
		/// Stops the engine.
		/// </summary>
		/// <param name="severity">Severity returned when stopping.</param>
		/// <param name="code">Code returned when stopping.</param>
		/// <returns>True if engine was stopped successfully, else false.</returns>
		/// <exception cref="TDException">
		/// Failure when stopping the engine.
		/// </exception>
		public bool Stop(out string severity, out string code)
		{
			BusinessObjectInput inputHeader = 
					new BusinessObjectInput(this.objectId,
											TerminateFunctionId,
											this.interfaceVersion);
			BusinessObjectOutput outputHeader;
			bool failed = false;

			outputHeader = Run(inputHeader);

			code = outputHeader.ErrorCode;
			severity = outputHeader.ErrorSeverity;

			// If critical or error then assume that the engine was not stopped.
			if (severity.Equals(Messages.ErrorSeverityCritical) || severity.Equals(Messages.ErrorSeverityError))
			{
				failed = true;
			}

			if (!failed)
			{
				this.isRunning = false;
			}

			return (!failed);
		}

		/// <summary>
		/// Runs the engine against the given input.
		/// </summary>
		/// <param name="input">Engine input.</param>
		/// <returns>Engine output.</returns>
		/// <exception cref="TDException">
		/// Failure when calling the engine dll.
		/// </exception>
		public virtual BusinessObjectOutput Run(BusinessObjectInput input)
		{
			BusinessObjectOutput output;
			byte[] outputHeader = new byte[134];
			byte[] outputBody = new byte[input.OutputLength];

			if (TDTraceSwitch.TraceVerbose)
			{
				Trace.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format(Messages.Engine_RunStarted, this.objectId, this.engAllocationId, this.dataSequenceNumber, input.ToString(), input.InputBody ) ) );
			}

			// Reset the timeout timer.
			if (this.timeoutEnabled)
			{
				idleTimer.Change(this.timeout, this.timeout);
			}

			// Call the engines DLL with the input.
			try
			{
				dllWrapper(input.ToString(), outputHeader, input.InputBody, outputBody);
			}
			catch (Exception exception) // Exceptions not documented so catch all.
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Engine_DllCallFailed, exception.Message, this.objectId, this.engAllocationId.ToString(), this.dataSequenceNumber, input.ToString(), input.InputBody, Environment.CurrentDirectory))); 
				throw new TDException(String.Format(Messages.Engine_DllCallFailed, exception.Message, this.objectId, this.engAllocationId, this.dataSequenceNumber, input.ToString(), input.InputBody, Environment.CurrentDirectory), exception, true, TDExceptionIdentifier.PRHBusinessObjectEngineDllCallFailed); 
			}

			output = new BusinessObjectOutput( ByteArrayToString( outputHeader ), ByteArrayToString(outputBody) );

			// Reset the timeout timer again.
			if (this.timeoutEnabled)
			{
				idleTimer.Change(this.timeout, this.timeout);
			}

			if (TDTraceSwitch.TraceVerbose)
			{
				Trace.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format(Messages.Engine_RunCompleted, this.objectId, this.engAllocationId.ToString(), this.dataSequenceNumber, output.ToString(), output.OutputBody ) ) );
			}

			return output;
		}

		/// <summary>
		/// Called when engine times out.
		/// </summary>
		/// <param name="State">Not used.</param>
		protected void IdleTimeoutCallback(object state)
		{
			// Stop timer. (An engine can only time out once in a single allocation.)
			DisableTimeout();
			
			// Flag that engine has timed out. This will be picked up by the owning pool
			// when it checks for engine time outs.
			this.isTimedOut = true;

			// Log a warning.
			if (TDTraceSwitch.TraceWarning)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, String.Format(Messages.Engine_Timedout, this.objectId, this.engAllocationId.ToString())));
			}

			// Allocate new allocation identifier to prevent the business object
			// that was allocated this engine from using it again.
			// NB This will not prevent a engine that is in the processing of running
			// a request from completing.
			UpdateAllocationId();
		}


		/// <summary>
		/// Retrieve the error text associted with a (previously returned) error code.
		/// </summary>
		/// <param name="errorCode">The four character code (Xnnn).</param>
		/// <returns>The error text.</returns>
		public string GetErrorMessage(string errorCode)
		{
			
			if	(errorCode.Length != 4)
			{
				return string.Empty;
			}
			
			BusinessObjectInput input = new BusinessObjectInput(this.objectId,
													GetStateFunctionId, this.interfaceVersion);

			input.ErrorCode = errorCode;

			input.AddOutputParameter(new HeaderOutputParameter(GetStateParameterLength), 0);

			BusinessObjectOutput output = Run(input);

			return ExtractErrorText(output.OutputBody);
		}


		/// <summary>
		/// Extract the error text from an output body 
		/// body returned by a GetState call.
		/// </summary>
		/// <param name="errorCode">Output body.</param>
		/// <returns>The error text.</returns>
		public virtual string ExtractErrorText(string output)
		{
			// In BO's apart from RVBO, the first four chars 
			//  of the output body are version information. 
	
			if	(output.Length >= 204)
			{
				return output.Substring(4, 200).Trim();
			}
			else
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Enables timeout on engine.
		/// </summary>
		public void EnableTimeout()
		{
			idleTimer.Change(this.timeout, this.timeout);
			this.timeoutEnabled = true;
			this.isTimedOut = false;
		}

		/// <summary>
		/// Disables timeout on engine.
		/// </summary>
		public void DisableTimeout()
		{
			idleTimer.Change(Timeout.Infinite, Timeout.Infinite);
			this.timeoutEnabled = false;
		}

		/// <summary>
		/// Dispose method for engines
		/// </summary>
		/// <param name="disposing">
		/// True when Dispose called by clients.
		/// False when Dispose called by runtime.
		/// </param>
		public void Dispose(bool disposing)
		{
			// Check to see if Dispose has already been called.
			if(!this.disposed)
			{
				if (disposing)
				{
					// Dispose of any managed resources:
					this.idleTimer.Dispose();					
				}
				
				// no longer stop running engines here -- the DLL  
				//  will now be stopped before restarting if/when  
				//  a new AppDomain is initialised. 
			}

			this.disposed = true;
		}

	
		/// <summary>
		/// Disposes of engine resources.
		/// Allows clients to dispose of resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this); // take off finalization queue to prevent dispose being called again.
		}

		/// <summary>
		/// Class destructor.
		/// </summary>
		~Engine()      
		{
			Dispose(false);
		}

	}
}
