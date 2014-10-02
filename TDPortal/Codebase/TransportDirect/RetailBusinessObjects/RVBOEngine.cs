//********************************************************************************
//NAME         : RVBOEngine.cs
//AUTHOR       : Joe Morrissey
//DATE CREATED : 16/02/2005
//DESCRIPTION  : Wrapper for reservations business object DLLs.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/RVBOEngine.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:22   mturner
//Initial revision.
//
//   Rev 1.1   Apr 23 2005 12:39:50   RPhilpott
//Allow for RVBO reinitialisation after NRS comms error.
//Resolution for 2301: PT - RVBO discontinues comms with NRS after error
//
//   Rev 1.0   Feb 16 2005 16:47:24   jmorrissey
//Initial revision.

using System;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Wrapper for reservations business object dll.
	/// </summary>	
	public class RVBOEngine : Engine
	{

		private const string NRS_COMMS_SUSPENDED = "V013";
		
		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="id">Unique id of engine.</param>
		/// <param name="dllWrapper">DLL Engine to be used by business object.</param>
		/// <param name="timeout">Duration after which business object has timed out.</param>
		/// <param name="dllObjectId">Object id associated with underlying dll.</param>
		/// <param name="interfaceVersion">Interface version of engine.</param>
		/// <param name="dataSequence">Data sequence number that business object is using.</param>
		/// <param name="timeout">Duration (of no usage) after which engine is taken as timed out.</param>
		public RVBOEngine(int id,
			LegacyBusinessObjectWrapper dllWrapper,
			string dllObjectId,
			string interfaceVersion,
			int dataSequence,
			TimeSpan timeout) : base(id, dllWrapper, dllObjectId, interfaceVersion, 
			dataSequence, timeout)
		{
			
		}

		/// <summary>
		/// Overrides the Start method, and adds an HeaderInputParameter needed to 
		/// initialise the RVBO which is passed to NRS 
		/// </summary>
		/// <param name="severity">Error severity returned.</param>
		/// <param name="code">Error code returned.</param>
		/// <returns>True if engine was started successfully, else false.</returns>
		/// <exception cref="TDException">
		/// Failure when starting the engine.
		/// </exception>
		public override bool Start(out string severity, out string code)
		{
			//assign default values to NRS parameters
			string nrsUserID = string.Empty;
			string nrsIsTraining = string.Empty;
			string nrsIsTest = string.Empty;
			string businessObjectName = string.Empty;

			BusinessObjectInput inputHeader = 
				new BusinessObjectInput(this.objectId,
										InitialiseFunctionId,
										this.interfaceVersion);
			
			//get properties needed to pass to HeaderInputParameter constructor
			try
			{
				nrsUserID = Properties.Current["RetailBusinessObjects.RVBO.NrsUserID"];			
				nrsIsTraining = Properties.Current["RetailBusinessObjects.RVBO.NrsIsTraining"];		
				nrsIsTest = Properties.Current["RetailBusinessObjects.RVBO.NrsIsTest"];
			}
			catch
			{
				//log error 
				Logger.Write(new OperationalEvent(TDEventCategory.Database,TDTraceLevel.Error,
					"RVBOEngine.Start() - Missing property in Property database " ));				
			}

			//specific implementation for RVBO only
			businessObjectName =  Properties.Current["RetailBusinessObjects.RVBO.ObjectId"];
			if (objectId.Equals(businessObjectName))
			{
				inputHeader.AddInputParameter(new HeaderInputParameter(nrsUserID + nrsIsTraining + nrsIsTest), 0);
			}

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
		/// Runs the engine against the given input.
		/// Most of the processing is in the base class -- this override 
		/// only exists to add special handling in the case where RVBO 
		/// is returning an error because NRS communications are suspended.
		/// </summary>
		/// <param name="input">Engine input.</param>
		/// <returns>Engine output.</returns>
		/// <exception cref="TDException">
		/// Failure when calling the engine dll.
		/// </exception>
		public override BusinessObjectOutput Run(BusinessObjectInput input)
		{
			BusinessObjectOutput output = base.Run(input);

			if	(output.ErrorCode == NRS_COMMS_SUSPENDED)
			{
				RVBOPool rvboPool = RVBOPool.GetRVBOPool();
				rvboPool.SetReinitialisationNeeded();
			}

			return output;
		}

		/// <summary>
		/// Extract the error text from an output body 
		/// body returned by a GetState call.
		/// </summary>
		/// <param name="errorCode">Output body.</param>
		/// <returns>The error text.</returns>
		public override string ExtractErrorText(string output)
		{
			// RVBO doesn't return version info in GS output body ...
	
			if	(output.Length >= 200)
			{
				return output.Substring(0, 200).Trim();
			}
			else
			{
				return string.Empty;
			}
		}


	}
}
