// ************************************************** 
// NAME                 : RetailBusinessObjectPool.cs 
// AUTHOR               : SchlumbergerSema
// DATE CREATED         : 11/07/2003 
// DESCRIPTION			: Abstract base class for Retail
// Business Object Pool classes.
// **************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/RetailBusinessObjectPool.cs-arc  $
//
//   Rev 1.1   Jan 11 2009 18:01:50   mmodi
//Updated to include a Terminate engine method, a Housekeeping completed flag, and an Allow intialise engine flag
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:46:18   mturner
//Initial revision.
//
//   Rev 1.18   May 20 2005 12:17:12   RPhilpott
//1) Do not disable timeout chaecking during unmanaged DLL calls.
//
//2) Always do stop/restart (TE/IN calls) on timed-out engine before returning it to the free pool.
//
//3) Do not do stop (TE) during engine disposal. 
//Resolution for 2511: PT costing - RBO initialisation failure
//
//   Rev 1.17   May 13 2005 14:57:54   RPhilpott
//Add extra logging and defensive coding for Application initialisation problem.
//Resolution for 2511: PT costing - RBO initialisation failure
//
//   Rev 1.16   Apr 23 2005 12:39:48   RPhilpott
//Allow for RVBO reinitialisation after NRS comms error.
//Resolution for 2301: PT - RVBO discontinues comms with NRS after error
//
//   Rev 1.15   Mar 30 2005 09:08:06   rscott
//Updated after running FXCop
//
//   Rev 1.14   Feb 16 2005 16:35:10   jmorrissey
//Fixed CLS Compliant warnings
//
//   Rev 1.13   Dec 15 2003 15:18:58   geaton
//Added code to ensure that number of engines created matches the pool size.
//Updated exception messages to include message exception message field.
//
//   Rev 1.12   Nov 27 2003 21:49:06   geaton
//Log Op Events rather than DataGateway events when housekeeping fails. This is consistent with other components. 
//(Prior to this change an error ocurred when logging the DataGatway events (on housekeeping failure) because the string code being logged could not be converted to an int. This was causing the pool to be left in an inconsistent state.)
//
//   Rev 1.11   Nov 27 2003 14:12:40   geaton
//Corrected typo made in previous change.
//
//   Rev 1.10   Nov 27 2003 14:02:32   geaton
//Added error message for use when housekeeping is initiated when housekeeping is already in progress.
//
//   Rev 1.9   Nov 20 2003 13:47:18   geaton
//Provision for housekeeping update files to be self-extracting exe's.
//
//   Rev 1.8   Oct 29 2003 19:47:16   geaton
//Added functionality to log Datagateway events for housekeeping.
//
//   Rev 1.7   Oct 28 2003 20:05:02   geaton
//Changes to support housekeeping and timeout functionality.
//
//   Rev 1.6   Oct 22 2003 09:19:18   geaton
//Added housekeeping support to business objects.
//
//   Rev 1.5   Oct 21 2003 15:22:54   geaton
//Changes to support business object timeout functionality.
//
//   Rev 1.4   Oct 17 2003 10:49:34   geaton
//Removed deadlock code.
//
//   Rev 1.3   Oct 16 2003 11:18:50   geaton
//Changed static fields to instance fields.
//
//   Rev 1.2   Oct 16 2003 10:42:28   geaton
//Added error handling.
//
//   Rev 1.1   Oct 15 2003 21:34:50   geaton
//Added exception handling.
//
//   Rev 1.0   Oct 15 2003 14:40:30   geaton
//Initial Revision

using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Text;
using System.IO;

using TransportDirect.Common; 
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.ReportDataProvider.TDPCustomEvents;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Provides values used to specify whether timeout checking should be performed
	/// on business objects provided by the pool.
	/// </summary>
	public enum TimeoutCheckingSwitch : int
	{
		Off,
		On
	}

	/// <summary>
	/// Provides status values for the housekeeping process.
	/// </summary>
	public enum HousekeepingStatusId : int
	{
		Idle,			// Housekeeping is not in progess.
		InProgress,		// Housekeeping is in progress.
	}

    /// <summary>
	/// Retail Business Object Pool classes must derive from this class.
	/// </summary>
	public abstract class RetailBusinessObjectPool : IDisposable
	{
		#region properties

		/// <summary>
		/// The date and time when housekeeping was successfully initiated.
		/// </summary>
		private DateTime housekeepingStartTime;

		/// <summary>
		/// The filepath of the data file used to perform housekeeping.
		/// </summary>
		private string housekeepingFile;

		/// <summary>
		/// When housekeeping is in progress, this stores the feed id of 
		/// the housekeeping data file. This is logged in a DataGatewayEvent.
		/// </summary>
		private string housekeepingFeedId;

		private bool disposed = false;
		/// <summary>
		/// Gets disposed indicator. True if pool has been disposed, else false.
		/// </summary>
		public bool Disposed
		{
			get{return disposed;}
		}

		private HousekeepingStatusId housekeepingStatus;
		/// <summary>
		/// Gets the current housekeeping status.
		/// </summary>
		public HousekeepingStatusId HousekeepingStatus
		{
			get{return housekeepingStatus;}
		}

        private bool housekeepingCompleted;

        /// <summary>
        /// Read/write. Flag set by this class when housekeeping has been completed. 
        /// Currently only used by the FBO to renew the ZPBO Pool
        /// </summary>
        public bool HousekeepingCompleted
        {
            get { return housekeepingCompleted; }
            set { housekeepingCompleted = value; }
        }

        protected int dataSequenceNumber;
		/// <summary>
		/// Gets the current data sequence.
		/// Used to control houeskeeping updates on business objects in the pool.
		/// </summary>
		public int DataSequence
		{
			get {return dataSequenceNumber;}
		}

	
		private string serverConfigFilepath;
		/// <summary>
		/// Gets the server configuration filename for business objects in the pool.
		/// This resides in the same directory as the business object engine dll file/s.
		/// The format of this file is described in section 3.7.1 of the Retail Business Objects User Guide.
		/// </summary>
		public string ServerConfigFilepath
		{
			get{return serverConfigFilepath;}
		}

		private TimeoutCheckingSwitch timeoutChecking;
		/// <summary>
		/// Gets and sets the timeout checking switch which determines whether timeout checking should be performed on pool objects.
		/// </summary>
		/// <remarks>
		/// Set access has been provided for unit testing and will not normally be used.
		/// </remarks>
		public TimeoutCheckingSwitch TimeoutChecking
		{
			get {return timeoutChecking;}
			set
			{
				if ((timeoutChecking == TimeoutCheckingSwitch.Off)
					&& (value == TimeoutCheckingSwitch.On))
				{
					this.timeoutCheckTimer.Change(this.timeoutCheckFrequency, this.timeoutCheckFrequency);
				}
				
				timeoutChecking = value;
			}
		}

		/// <summary>
		/// Class containing object to obtain locks on.
		/// </summary>
		internal class Lock{public static object monitor = new object();}

		/// <summary>
		/// Initial capacity for hash tables.
		/// </summary>
		private const int InitialEngineCapacity = 5;

		/// <summary>
		/// Holds engines not currently used by business objects and 
		/// are available for use when a request for a business object arrives.
		/// </summary>
		protected Hashtable freeEngines = Hashtable.Synchronized( new Hashtable(InitialEngineCapacity) );

		/// <summary>
		/// Holds engines currently used by business objects that have been released from pool.
		/// </summary>
		private Hashtable usedEngines = Hashtable.Synchronized( new Hashtable(InitialEngineCapacity) );
	
		protected string interfaceVersionNum;
		/// <summary>
		/// Gets the interface version of engines in pool.
		/// First two characters are major version, last two characters are minor version.
		/// </summary>
		public string InterfaceVersion
		{
			get {return interfaceVersionNum;}
		}

		/// <summary>
		/// Object id to use when pool creates engines.
		/// </summary>
		protected string objectId;

		protected int poolSizeNum;
		/// <summary>
		/// Gets the size of the pool. 
		/// This is not necessarily the number of free objects currently in pool.
		/// </summary>
		public int PoolSize
		{
			get{return poolSizeNum;}
		}

		/// <summary>
		/// Maximum number of objects in pool.
		/// </summary>
		protected int maxPoolSize;

		/// <summary>
		/// The minimum number of objects in pool that will provide the required level of service.
		/// </summary>
		protected int minimumPoolSize;

		protected TimeSpan engTimeoutDuration;
		/// <summary>
		/// Gets the duration after which engines are taken as having timed-out.
		/// </summary>
		public TimeSpan EngineTimeoutDuration
		{
			get {return engTimeoutDuration;}
		}
	
		private TimeSpan timeoutCheckFrequency;
		/// <summary>
		/// Frequency at which free pool should be checked for engine timeouts.
		/// </summary>
		public TimeSpan TimeoutCheckFrequency
		{
			get{return timeoutCheckFrequency;}
		}

		/// <summary>
		/// Timer used to perform checking for engine timeouts.
		/// </summary>
		private Timer timeoutCheckTimer;
		
		/// <summary>
		/// Gets the current number of objects that are free to use.
		/// </summary>
		public int NumberFree
		{
			get{return freeEngines.Count;}
		}

		/// <summary>
		/// Frequency at which pool should check for changes in housekeeping states.
		/// </summary>
		private TimeSpan housekeepingCheckFrequency;

		/// <summary>
		/// Timer used to perform checking for changes in housekeeping states.
		/// </summary>
		private Timer housekeepingCheckTimer;

        /// <summary>
        /// Flag to prevent the engines in this pool ever being intialised.
        /// Only used by FBO, to prevent engine being initialised when in ZPBO mode.
        /// </summary>
        private bool allowInitialiseEngine = true;

		#endregion
		

		#region protected_methods

		/// <summary>
		/// Implemented in subclasses to validate properties unique to the pool.
		/// </summary>
		/// <param name="errors">
		/// Used to store any errors resulting from validation.
		/// </param>
		abstract protected void ValidateProperties(ArrayList errors);

		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="interfaceVersionNum">
		/// Property key containing version of interface of engines in pool.
		/// </param>
		/// <param name="objectId">
		/// Property key containing object id to use when initialing engines in pool.
		/// </param>
		/// <param name="poolSizeKey">
		/// Property key containing number of engines that should be held in pool.
		/// </param>
		/// <param name="maxPoolSize">
		/// Maximum number of engines that can be held in pool.
		/// </param>
		/// <param name="timeoutDurationKey">
		/// Property key containing the time out duration in milliseconds.
		/// </param>
		/// <param name="timeoutCheckFrequencyKey">
		/// Property key containing the frequency at which time out should be checked in milliseconds.
		/// </param>
		/// <param name="timeoutCheckingKey">
		/// Property key containing switch to turn timeout checking on/off.
		/// </param>
		/// <param name="localConfigFilepath">
		/// Filepath of local config file (.ini) of the engines.
		/// </param>
		/// <param name="minimumPoolSizeKey">
		/// Property key containing the minimum required business objects in pool.
		/// </param>
		/// <exception cref="TDException">
		/// Thrown if property values of the keys passed fail validation.
		/// </exception>
		protected RetailBusinessObjectPool(string interfaceVersionKey,
										   string objectIdKey,
										   string poolSizeKey,
										   int maxPoolSize,
										   string timeoutDurationKey,
										   string timeoutCheckFrequencyKey,
										   string timeoutCheckingKey,
										   string localConfigFilepath,
										   string minimumPoolSizeKey)
		{
			ArrayList errors = new ArrayList();

			// Validate property values of keys passed in.
			RetailBusinessObjectPropertyValidator validator = new RetailBusinessObjectPropertyValidator(Properties.Current);
			validator.ValidateProperty(interfaceVersionKey, errors);
			validator.ValidateProperty(poolSizeKey, errors);
			validator.ValidateProperty(objectIdKey, errors);
			validator.ValidateProperty(timeoutDurationKey, errors);
			validator.ValidateProperty(timeoutCheckFrequencyKey, errors);
			validator.ValidateProperty(timeoutCheckingKey, errors);
			validator.ValidateProperty(minimumPoolSizeKey, errors);
			validator.ValidateProperty(Keys.HousekeepingCheckFrequency, errors);
			ValidateProperties(errors);

			if (!File.Exists(localConfigFilepath))
				errors.Add(String.Format(Messages.Pool_LocalConfigFileNotFound, localConfigFilepath, Environment.CurrentDirectory));
			else
			{
				this.serverConfigFilepath = RetailBusinessObjectConfiguration.GetServerConfigFilepath(localConfigFilepath, errors);
				if (this.serverConfigFilepath != null && this.serverConfigFilepath.Length > 0)
					this.dataSequenceNumber = RetailBusinessObjectConfiguration.GetCurrentDataSequence(this.serverConfigFilepath, errors);
			}

			// Report errors back to caller by throwing exception.
			if (errors.Count > 0)
			{
				StringBuilder messages = new StringBuilder(100);
				foreach( string error in errors )
				{
					messages.Append(error);
					messages.Append(" ");	
				}

				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Pool_PropertiesInvalid, messages.ToString())));
				throw new TDException(String.Format(Messages.Pool_PropertiesInvalid, messages.ToString()), true, TDExceptionIdentifier.PRHInvalidProperties);				
			}

			// Store the valid property values.
			this.interfaceVersionNum = Properties.Current[interfaceVersionKey];
			this.objectId = Properties.Current[objectIdKey];
			this.poolSizeNum = int.Parse(Properties.Current[poolSizeKey]);
			this.maxPoolSize = maxPoolSize;
			this.engTimeoutDuration = new TimeSpan(0, 0, 0, 0, int.Parse(Properties.Current[timeoutDurationKey]));
			this.timeoutCheckFrequency = new TimeSpan(0, 0, 0, 0, int.Parse(Properties.Current[timeoutCheckFrequencyKey]));
			this.timeoutChecking = (TimeoutCheckingSwitch)PropertyValidator.StringToEnum(typeof(TimeoutCheckingSwitch), Properties.Current[timeoutCheckingKey]);
			this.housekeepingStatus = HousekeepingStatusId.Idle;
			this.disposed = false;
			this.minimumPoolSize = int.Parse(Properties.Current[minimumPoolSizeKey]);
			this.housekeepingCheckFrequency = new TimeSpan(0, 0, 0, 0, int.Parse(Properties.Current[Keys.HousekeepingCheckFrequency]));

			// Log details of the property values for debugging.
			if (TDTraceSwitch.TraceInfo)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Pool_ConstructionProperty, Properties.Current[interfaceVersionKey], interfaceVersionKey)));
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Pool_ConstructionProperty, Properties.Current[objectIdKey], objectIdKey)));
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Pool_ConstructionProperty, Properties.Current[poolSizeKey], poolSizeKey)));
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Pool_ConstructionProperty, maxPoolSize.ToString(), "N/A(MaximumPoolSize)")));
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Pool_ConstructionProperty, Properties.Current[timeoutDurationKey], timeoutDurationKey)));
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Pool_ConstructionProperty, Properties.Current[timeoutCheckFrequencyKey], timeoutCheckFrequencyKey)));
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Pool_ConstructionProperty, Properties.Current[interfaceVersionKey], interfaceVersionKey)));
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Pool_ConstructionProperty, Properties.Current[timeoutCheckingKey], timeoutCheckingKey)));
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Pool_ConstructionProperty, localConfigFilepath, "N/A(LocalConfigPath)")));
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Pool_ConstructionProperty, Properties.Current[minimumPoolSizeKey], minimumPoolSizeKey)));
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Pool_ConstructionProperty, Properties.Current[Keys.HousekeepingCheckFrequency], Keys.HousekeepingCheckFrequency)));
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Pool_ConstructionProperty, this.serverConfigFilepath, "N/A(ServerConfigPath)")));
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Pool_ConstructionProperty, this.dataSequenceNumber, "N/A(DataSequence)")));	
			}
		}

		/// <summary>
		/// Adds a dll to be used by the engine of a business object.
		/// Only a 'single' business object will use the engine using this dll
		/// since retail business object dlls only support single threading.
		/// </summary>
		/// <param name="dllWrapper">
		/// Dll wrapper used to run an engine.
		/// </param>
		/// <exception cref="TDException">
		/// Error when adding the engine.
		/// </exception>
		protected virtual void AddEngineDll(LegacyBusinessObjectWrapper dllWrapper)
		{
			// Check that maximum pool size has not been exceeded.
			if (freeEngines.Count == this.maxPoolSize)
				throw new TDException(Messages.Pool_MaximumPoolSizeExceeded, false, TDExceptionIdentifier.PRHMaximumPoolSizeExceeded);

			// Create an engine based on the dll wrapper passed in.
			if (freeEngines.Count < this.poolSizeNum)
			{
				try
				{
					// Use the count number as the engines id. (Since this will be unique.)
					Engine engine = new Engine(freeEngines.Count,
											   dllWrapper,
											   this.objectId, 
											   this.interfaceVersionNum,
											   this.dataSequenceNumber,
											   this.engTimeoutDuration);

					// Use the engines id as the collection key, 
					// instead of a hash code (since low volume of engines will be stored).
					freeEngines.Add(engine.Id, engine);
				
				}
				catch (ArgumentNullException argumentNullException)
				{
					throw new TDException(Messages.Pool_EngineAddFailed, argumentNullException, false, TDExceptionIdentifier.PRHEngineAddFailed);
				}
				catch (NotSupportedException notSupportedException)
				{
					throw new TDException(Messages.Pool_EngineAddFailed, notSupportedException, false, TDExceptionIdentifier.PRHEngineAddFailed);
				}
			}
		}

        /// <summary>
		/// Initialises the pool for use. Should be called once on startup.
		/// </summary>
		/// <exception cref="TDException">
		/// Thrown if less than the minimum required BOs intialise.
		/// </exception>
        protected void Initialise()
        {
            this.Initialise(true);
        }

		/// <summary>
		/// Initialises the pool for use. Should be called once on startup.
		/// </summary>
        /// <param name="allowInitialiseEngine">AllowIntialiseEngine flag, set to false to prevent engines 
        /// in this pool ever being initialised (only used by FBO)</param>
		/// <exception cref="TDException">
		/// Thrown if less than the minimum required BOs intialise.
		/// </exception>
        protected void Initialise(bool allowInitialiseEngine)
		{
			if (TDTraceSwitch.TraceInfo)
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Pool_InitialisationStarted, this.objectId)));

            this.allowInitialiseEngine = allowInitialiseEngine;

			// Start business object engines. Force intialisation regardless of data sequence number.
			InitialiseEngines(true, allowInitialiseEngine);
		
			// Check that we have the minimum number of engines in pool to provide service
			if (freeEngines.Count < this.minimumPoolSize)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Pool_SizeLessThanMinimum, this.objectId, this.maxPoolSize, freeEngines.Count)));
				throw new TDException(String.Format(Messages.Pool_SizeLessThanMinimum, this.objectId, this.maxPoolSize, freeEngines.Count), true, TDExceptionIdentifier.PRHPoolSizeLessThanMinimum);
			}			

			// Create and start pool timeout checker timer. (NB Parameter values have been pre-validated.)
			this.timeoutCheckTimer = new Timer(new TimerCallback(this.CheckTimeouts), 
											   null,
											   this.timeoutCheckFrequency,
											   this.timeoutCheckFrequency);

			// Turn timeout timer off if configured to do so. (For performance only since engines are configured for timeouts individually.)
			if (this.timeoutChecking == TimeoutCheckingSwitch.Off)
			{
				this.timeoutCheckTimer.Change(Timeout.Infinite, Timeout.Infinite);

				if (TDTraceSwitch.TraceWarning)
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, String.Format(Messages.Pool_TimeoutCheckingDisabled, this.objectId)));
			}

			// Create and stop housekeeping check timer. (NB Parameter values have been pre-validated.)
			// Timer is started on housekeeping request.
			this.housekeepingCheckTimer = new Timer(new TimerCallback(this.PerformHousekeeping), 
													null,
													this.housekeepingCheckFrequency, 
													this.housekeepingCheckFrequency);
			housekeepingCheckTimer.Change(Timeout.Infinite, Timeout.Infinite);

			if (TDTraceSwitch.TraceInfo)
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Pool_InitialisationCompleted, this.objectId, this.freeEngines.Count)));
		}

		#endregion


		#region public_methods 

		/// <summary>
		/// Determines whether all free and used engines are running 
		/// at the pool data sequence number.
		/// </summary>
		/// <returns>
		/// True if all engines running at pool data sequence number, else false.
		/// </returns>
		/// <remarks>
		/// Provided primarily for unit testing.
		/// </remarks>
		public bool EngineDataSequencesMatch()
		{
			bool match = true;

			lock (this) 
			{
				IEnumerator enuUsed = usedEngines.Keys.GetEnumerator();
				while (enuUsed.MoveNext()) 
				{
					int key = (int)enuUsed.Current; 
					Engine engine = usedEngines[key] as Engine;
					match = match && (engine.DataSequence == this.dataSequenceNumber);
				}

				IEnumerator enuFree = freeEngines.Keys.GetEnumerator();
				while (enuFree.MoveNext()) 
				{
					int key = (int)enuFree.Current; 
					Engine engine = freeEngines[key] as Engine;
					match = match && (engine.DataSequence == this.dataSequenceNumber);
				}
			}

			return match;
		}

		/// <summary>
		/// Disposes of pool resources. 
		/// Can be called by clients (via Dispose()) or runtime (via destructor).
		/// </summary>
		/// <param name="disposing">
		/// True when called by clients.
		/// False when called by runtime.
		/// </param>
		public void Dispose(bool disposing)
		{
			// Check to see if Dispose has already been called.
			if(!this.disposed)
			{
				if (disposing)
				{
					// Dispose of any managed resources:
				
					this.timeoutCheckTimer.Dispose();
					this.housekeepingCheckTimer.Dispose();
					
					lock (freeEngines.SyncRoot) 
					{
						IEnumerator enu = freeEngines.Keys.GetEnumerator();
					
						while (enu.MoveNext()) 
						{
							Engine engine = null;
							int key = (int) enu.Current; 
							engine = freeEngines[key] as Engine;
							engine.Dispose(disposing);
						}
					}

					lock (usedEngines.SyncRoot) 
					{
						IEnumerator enu = usedEngines.Keys.GetEnumerator();
						while (enu.MoveNext()) 
						{
							Engine engine = null;
							int key = (int) enu.Current; 
							engine = usedEngines[key] as Engine;
							engine.Dispose(disposing);
						}
					}
				}
			}

			this.disposed = true;
		}

	
		/// <summary>
		/// Disposes of pool resources.
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
		~RetailBusinessObjectPool()      
		{
			Dispose(false);
		}

		/// <summary>
		/// Returns an object to the pool and 
		/// notifies any waiting threads that this has occured.
		/// </summary>
		/// <param name="businessObject">
		/// Reference to a business object to return to pool.
		/// It is necessary to pass in a reference (to a reference) 
		/// in order to nullify the reference at the client end.
		/// </param>
		/// <exception cref="TDException">
		/// Failed to release instance of business object passed.
		/// </exception>
		/// <remarks>
		/// Also initialises the business object engine if it is not using 
		/// the current pool data sequence.
		/// </remarks>
		public void Release(ref BusinessObject businessObject) 
		{
			
			lock(this)
			{
				bool released = false;
				bool releasePossible = true;
				Engine engine = null;

				// Null business object was passed.
				if (businessObject == null)
				{	
					if (TDTraceSwitch.TraceWarning)
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, Messages.Pool_ClientReleasedNullObject));	
					releasePossible = false;
				}

				// Check if engine allocated to business object is in used list.
				if (releasePossible)
				{
					if (!usedEngines.ContainsKey(businessObject.EngineId))
					{
						// Engine not found in used list - assume timed out.
						businessObject = null;
						if (TDTraceSwitch.TraceWarning)
							Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, Messages.Pool_ClientReleasedTimedOutEngine));	
						releasePossible = false;
					}
				}

				// Check if engine in used list is still allocated to the business object.
				if (releasePossible)
				{
					engine = usedEngines[businessObject.EngineId] as Engine;

					if (engine.AllocationId != businessObject.EngineAllocationId)
					{
						// Engine no longer allocated to business object - assume timed out. (And timeout checker has not yet released the engine back to free list.)
						businessObject = null;
						if (TDTraceSwitch.TraceWarning)
							Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, Messages.Pool_ClientReleasedTimedOutEngine));	
						releasePossible = false;
					}

				}

				// Remove engine from used list and put on the free list.
				if (releasePossible)
				{
					try
					{
						// Stop the engine from timing out.
						engine.DisableTimeout();

						// Remove from the used list.
						usedEngines.Remove(engine.Id);
						
						// Check if engine data needs updating. 
						// This will be required if housekeeping took place
						// while engine was in use by the business object, 
						// or if a restart following a comms failure is due.
						bool engineInvalidated = false;

						if (engine.DataSequence != this.dataSequenceNumber || engine.NeedsReinitialisation)
						{
							bool ignoreSequence = engine.NeedsReinitialisation;

							if (!InitialiseEngine(engine, ignoreSequence, false, allowInitialiseEngine))
							{
								// If the engine fails to initialise then remove from service.
								engineInvalidated = true;

								// Log error if the total engines in service has dipped below minimum required.
								int totalEngines = freeEngines.Count + usedEngines.Count;
								
								if (totalEngines < this.minimumPoolSize)
								{
									Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Pool_SizeLessThanMinimum, this.objectId, this.maxPoolSize, totalEngines)));
								}
							}
						}

                        // (Info)
                        // If this flag is set, then the class which set it must ensure that it recycles and
                        // Initialises the engines otherwise any use of this engine will through errors
                        if (engine.NeedsTerminating)
                        {
                            if (!TerminateEngine(engine, false, false))
                            {
                                // If the engine fails to terminate then remove from service.
                                engineInvalidated = true;

                                // Log error if the total engines in service has dipped below minimum required.
                                int totalEngines = freeEngines.Count + usedEngines.Count;

                                if (totalEngines < this.minimumPoolSize)
                                {
                                    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Pool_SizeLessThanMinimum, this.objectId, this.maxPoolSize, totalEngines)));
                                }
                            }
                        }
						
						// Add engine to free list.
						if (!engineInvalidated)
						{
							freeEngines.Add(engine.Id, engine);
							released = true;
						}
								
					}		
					catch (ArgumentNullException argumentNullException)
					{
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, Messages.Pool_ClientReleaseFailed, businessObject));
						throw new TDException(Messages.Pool_ClientReleaseFailed, argumentNullException, true, TDExceptionIdentifier.PRHBOReleaseFailed);
					}
					catch (NotSupportedException notSupportedException)
					{
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, Messages.Pool_ClientReleaseFailed, businessObject));
						throw new TDException(Messages.Pool_ClientReleaseFailed, notSupportedException, true, TDExceptionIdentifier.PRHBOReleaseFailed);
					}
					catch (ArgumentException argumentException)
					{
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, Messages.Pool_ClientReleaseFailed, businessObject));
						throw new TDException(Messages.Pool_ClientReleaseFailed, argumentException, true, TDExceptionIdentifier.PRHBOReleaseFailed);
					}
					finally
					{
						// Alway ensures that engine will not be used by business object,
						// regardless of whether all of release steps succeeded.
						engine.UpdateAllocationId(); 
						businessObject = null;
					}
				}

				// If an engine was released onto the free list then signal to waiting threads.
				if (released)
				{
					if (!NotifyWaitingThreads())
					{
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, Messages.Pool_SignalReleaseFailed));
						throw new TDException(Messages.Pool_SignalReleaseFailed, false, TDExceptionIdentifier.PRHSignalReleaseFailed);
					}
				}
			}

		}

		/// <summary>
		/// Notifies threads that are waiting on the pool lock monitor.
		/// </summary>
		/// <returns>True if threads notified successfully.</returns>
		private bool NotifyWaitingThreads()
		{
			bool notified = true;

			try
			{					
				Monitor.Enter(Lock.monitor);  // Acquire lock.	
				Monitor.Pulse(Lock.monitor);  // Notify waiting threads.	
				Monitor.Exit(Lock.monitor);   // Release lock.
			}
			catch (SynchronizationLockException synchronizationLockException)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Pool_ThreadNotificationFailed, synchronizationLockException.Message)));
				notified = false;
			}

			return notified;
		}

		/// <summary>
		/// Get a business object from the pool.
		/// If a business object is not available then call will be blocked
		/// until a business object is released to the pool by another client.
		/// </summary>
		/// <returns>
		/// An instance of a business object.
		/// </returns>
		/// <exception cref="TDException">
		/// Failed to get an instance of a business object.
		/// </exception>
		public BusinessObject GetInstance() 
		{
			// If no engines available then wait until one becomes free.
			if (freeEngines.Count == 0) 
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "All engines in use for object " + objectId + " - waiting"));

				// Acquire lock.
				Monitor.Enter(Lock.monitor); 

				// Wait until receive signal that an engine is free.
				Monitor.Wait(Lock.monitor); 
					
				// Continue to get business object now that an engine has become free.
				Monitor.Exit(Lock.monitor);

				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Engine now available for object " + objectId));
			}

			lock (this) 
			{
				BusinessObject businessObject = null;
				IEnumerator enu = freeEngines.Keys.GetEnumerator();

				if (!enu.MoveNext())
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Pool_GetInstanceFailed, this.objectId, Messages.Pool_PassedCollectionEnd)));
					throw new TDException(String.Format(Messages.Pool_GetInstanceFailed, this.objectId,  Messages.Pool_PassedCollectionEnd), true, TDExceptionIdentifier.PRHBOGetIntanceFailed);
				}
					
				int key = (int)enu.Current;

				try
				{
					// Get the engine.
					Engine engine = freeEngines[key] as Engine;

					// Remove the engine from free list.
					freeEngines.Remove(key);

					// Add the engine to the used list.
					usedEngines.Add(key, engine);

					// Update the engines allocation id.
					engine.UpdateAllocationId();

					// Create a business object using the engine.
					businessObject = new BusinessObject(engine);

					// Enable timeout on engine if global setting dictates.
					if (this.timeoutChecking == TimeoutCheckingSwitch.On)
						engine.EnableTimeout();
				}
				catch (ArgumentNullException argumentNullException)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Pool_GetInstanceFailed, this.objectId, argumentNullException.Message), argumentNullException));
					throw new TDException(String.Format(Messages.Pool_GetInstanceFailed, this.objectId, argumentNullException.Message), argumentNullException, true, TDExceptionIdentifier.PRHBOGetIntanceFailed);
				}
				catch (NotSupportedException notSupportedException)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Pool_GetInstanceFailed, this.objectId, notSupportedException.Message), notSupportedException));
					throw new TDException(String.Format(Messages.Pool_GetInstanceFailed, this.objectId, notSupportedException.Message), notSupportedException, true, TDExceptionIdentifier.PRHBOGetIntanceFailed);
				}
				catch (ArgumentException argumentException)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Pool_GetInstanceFailed, this.objectId, argumentException.Message), argumentException));
					throw new TDException(String.Format(Messages.Pool_GetInstanceFailed, this.objectId, argumentException.Message), argumentException, true, TDExceptionIdentifier.PRHBOGetIntanceFailed);
				}
				catch (InvalidOperationException invalidOperationException)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Pool_GetInstanceFailed, this.objectId, invalidOperationException.Message), invalidOperationException));
					throw new TDException(String.Format(Messages.Pool_GetInstanceFailed, this.objectId, invalidOperationException.Message), invalidOperationException, true, TDExceptionIdentifier.PRHBOGetIntanceFailed);
				}
				catch (NullReferenceException nullReferenceException)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Pool_GetInstanceFailed, this.objectId, nullReferenceException.Message), nullReferenceException));
					throw new TDException(String.Format(Messages.Pool_GetInstanceFailed, this.objectId, nullReferenceException.Message), nullReferenceException, true, TDExceptionIdentifier.PRHBOGetIntanceFailed);
				}
				catch (Exception exception)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Pool_GetInstanceFailed, this.objectId, exception.Message), exception));
					throw new TDException(String.Format(Messages.Pool_GetInstanceFailed, this.objectId, exception.Message), exception, true, TDExceptionIdentifier.PRHBOGetIntanceFailed);
				}

				return businessObject;
			}

		}
		
		/// <summary>
		/// Initiates the housekeeping process on the pool.
		/// If initiating not possible, then error is logged with reason.
		/// </summary>
		/// <param name="feedId">Feed id of housekeeping data used. NB This is not the "data sequence" number.</param>
		/// <returns>True if successfully initiated, otherwise false.</returns>
		public bool InitiateHousekeeping(string feedId)
		{
			bool ok = true;
			string message = String.Empty;

			if (TDTraceSwitch.TraceInfo)
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Pool_HousekeepingRequested, this.objectId)));
			
			if (ok)
			{
				if (this.housekeepingStatus != HousekeepingStatusId.Idle)
				{
					ok = false;
					message = Messages.Pool_HousekeepingInProgress;
				}
			}

			if (ok)
			{
				if (feedId.Length <= 0)
				{
					ok = false;
					message = Messages.Pool_HousekeepingFeedIdInvalid;
				}
			}
	
			if (ok)
			{
				ArrayList errors = new ArrayList();
				this.housekeepingFile = RetailBusinessObjectConfiguration.GetUpdateFilepath(this.serverConfigFilepath, errors);
				if (errors.Count != 0)
				{
					ok = false;
					StringBuilder sb = new StringBuilder(20);
					foreach(string error in errors)
						sb.Append(error + " ");
					message = String.Format(Messages.Pool_HousekeepingUpdatefileNotSpecified, sb.ToString());
				}
			}

			if (ok)
			{
				if (!File.Exists(this.housekeepingFile))
				{
					// Check if self-extracting exe file exists (this may be used instead of dat files when refreshes are performed)
					string datFile = this.housekeepingFile;
					string exeFile = datFile.Replace(".dat", ".exe");
					
					if (!File.Exists(exeFile))
					{
						ok = false;
						string missingFiles = datFile + " or " + exeFile;
						message = String.Format(Messages.Pool_HousekeepingUpdatefileNotFound, missingFiles);
					}

				}
			}

			if (ok)
			{
				if (!housekeepingCheckTimer.Change(new TimeSpan(0,0,0,0), this.housekeepingCheckFrequency))
				{
					ok = false;
					message = Messages.Pool_HousekeepingTimerFailed;
				}
			}

			if (ok)			
			{
				// Store details for logging on housekeeping completion
				this.housekeepingFeedId = feedId;
				this.housekeepingStartTime = DateTime.Now;
				this.housekeepingStatus = HousekeepingStatusId.InProgress;	
			}
				
			if (!ok)
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Pool_HousekeepingRequestFailed, this.objectId, message)));

			if (ok && TDTraceSwitch.TraceInfo)
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Pool_HousekeepingInitiated, this.objectId)));
			
			return ok;
		}

		#endregion


		#region private_methods

		/// <summary>
		/// Timer delegate that checks if any engines have timed out.
		/// Releases any timed out engines.
		/// </summary>
		/// <param name="stateInfo">Not used.</param>
		void CheckTimeouts(Object stateInfo)
		{
			if (this.timeoutChecking == TimeoutCheckingSwitch.On)
			{
				if (Timeouts > 0)
				{
					ReleaseTimeouts();
				}
			}

			if	(ReinitialisationDue)
			{
				ResetReinitialisationNeeded();	// reset that no longer due 
				SetInitialisationNeeded();		// flag all engines to indicate init needed
				InitialiseEngines(true, allowInitialiseEngine);		// do the init on all free engines
			}
		}

		/// <summary>
		/// Performs housekeeping on any single engine from the free list.
		/// </summary>
		/// <param name="attempted">
		/// Set to true if housekeeping was attempted on an engine.
		/// Otherwise false is an engine was not available to attempt housekeeping.
		/// </param>
		/// <param name="success">
		/// Set to true if housekeeping was completed successfully, else false.
		/// </param>
		/// <remarks>
		/// An engine is selected from the free list (rather than the Release method providing an engine)
		/// so that clients requiring engines are not blocked. ie housekeeping will only be performed
		/// when a spare engine is going free. This may result in clients using engines at the old
		/// data sequence until one becomes free.
		/// </remarks>
		private void HousekeepFreeEngine(ref bool attempted, ref bool success)
		{
			attempted = false;
			success = false;
			Engine housekeepEngine = null;
			int housekeepKey = 0;

			lock (freeEngines.SyncRoot) 
			{
				// Take an engine from the free list to perform housekeeping against.
				if (freeEngines.Count != 0)
				{
					IEnumerator enu = freeEngines.Keys.GetEnumerator();
					if (enu.MoveNext())
					{	
						housekeepKey = (int)enu.Current;	
						Engine engine = freeEngines[housekeepKey] as Engine;
						freeEngines.Remove(housekeepKey);
						housekeepEngine = engine;
					}
				}
			}

			if (housekeepEngine != null)
			{
				attempted = true;

                // Terminate the engine we want to housekeep
                success = TerminateEngine(housekeepEngine, true, true);

                if (success)
                {
                    // Perform housekeeping on the engine.
                    success = InitialiseEngine(housekeepEngine, true, true, true);
                }


				// Log success event (0 passed as error code).
				// Suffix housekeeping file path with the data sequence number.
				if (success)
				{
					Trace.Write( new DataGatewayEvent(this.housekeepingFeedId,
													  "NoSession",
													  this.housekeepingFile + " DataSequence: " + this.dataSequenceNumber.ToString(),
													  this.housekeepingStartTime,
													  DateTime.Now,
													  true, 0));
				}
				
				// Regardless of success result, put engine back in free list if it is running.
				lock (freeEngines.SyncRoot) 
				{
                    // (Info)
                    // Even if not allowed to initialise the engine, must keep in the freeEngines list
                    // to avoid triggering the minimum number of engines check (Currently this flag is only 
                    // used by the FBOPool)
					if ((housekeepEngine.Running) || (!allowInitialiseEngine))
					{
						freeEngines.Add(housekeepKey, housekeepEngine);
					}
					else
					{
						// If the engine is not running then discard the engine.	
						housekeepEngine.Dispose();
						housekeepEngine = null;
					
						// Check pool size after initialising engines.
						int totalEngines = freeEngines.Count + usedEngines.Count;
						if (totalEngines < this.minimumPoolSize)
							Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Pool_SizeLessThanMinimum, this.objectId, this.maxPoolSize, totalEngines)));
					}
				}
			}
			
		}
		
		/// <summary>
		/// Performs initialisation on an engine if engine data sequence does not 
		/// match pool data sequence.
		/// Engine data sequence is updated to reflect data sequence that it was 
		/// intialised against.
		/// Logs appropriate DataGatewayEvents if housekeeping requested on engine.
		/// </summary>
		/// <param name="engine">
		/// Engine to initialise.
		/// </param>
		/// <param name="ignoreSequence">
		/// True if initialisation should be performed regardless of whether 
		/// the engine data sequence already matches pool data sequence.
		/// </param>
		/// <param name="housekeep">
		/// True if the engine should also have housekeeping performed on it, 
		/// otherwise false.
		/// </param>
        /// <param name="allowInitialise">Flag must be true if this engine is to be started</param>
		/// <returns>True if successful, else false if unsuccessful.</returns>
		private bool InitialiseEngine(Engine engine, bool ignoreSequence, bool housekeep, bool allowInitialise)
		{
			bool failed = false;

			if ((engine.DataSequence != this.dataSequenceNumber) || (ignoreSequence))
			{
                if (engine.Running)
				{
					string stopSeverity;
					string stopCode;

					if (!engine.Stop(out stopSeverity, out stopCode))
					{
						failed = true;

						if (housekeep && (this.housekeepingStatus == HousekeepingStatusId.InProgress))
							Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Engine_StopFailedForHousekeep, stopSeverity, stopCode)));
						else
							Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Engine_StopFailed, stopSeverity, stopCode)));
						
					}
					else
					{
						if ((stopSeverity.Length != 0) && (TDTraceSwitch.TraceWarning))
							Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, String.Format(Messages.Engine_StopFeedback, stopSeverity, stopCode)));	
					}
				}

				if (!engine.Running && housekeep && !failed)
				{
					string housekeepSeverity;
					string housekeepCode;

					if (!engine.Housekeep(out housekeepSeverity, out housekeepCode))
					{
						failed = true;

						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Engine_HousekeepingFailed, housekeepSeverity, housekeepCode)));
					}
					else
					{
						if ((housekeepSeverity.Length != 0) && (TDTraceSwitch.TraceWarning))
							Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, String.Format(Messages.Engine_HousekeepingFeedback, housekeepSeverity, housekeepCode)));	

						ArrayList errors = new ArrayList();

						int latestDataSequence = RetailBusinessObjectConfiguration.GetCurrentDataSequence(this.serverConfigFilepath, errors);

						if (errors.Count != 0)
						{
							failed = true;
							StringBuilder errorMessages = new StringBuilder(100);
							
							foreach(string error in errors)
							{
								errorMessages.Append(error + " ");
							}
							Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Pool_HousekeepingFailedUpdatingSequence, errorMessages.ToString())));
						}
						else
						{
							this.dataSequenceNumber = latestDataSequence;
						}
						
					}
				}

				// Attempt to start engine regardless of whether any previous operation (ie housekeeping) failed.
                if ((!engine.Running) && (allowInitialise))
				{
					string startSeverity;
					string startCode;
					string stopSeverity;
					string stopCode;

					int previousDataSequence = engine.DataSequence;
					engine.DataSequence = this.dataSequenceNumber;

					if (!engine.Start(out startSeverity, out startCode))
					{

						if (housekeep)
						{
							Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Engine_StartFailedFollowingHousekeep, startSeverity, startCode)));			
						}
						else
						{
							Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Engine_StartFailed, startSeverity, startCode)));	
						}

						
						// if engine failed to start, try stopping it and then have another go at starting it ... 

						failed = !(engine.Stop(out stopSeverity, out stopCode));
						
						if	(failed)
						{
							Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Engine_StopFailed, stopSeverity, stopCode)));
						}
						else
						{
							failed = !(engine.Start(out startSeverity, out startCode));
						
							if	(failed)
							{
								if (housekeep)
								{
									Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Engine_StartFailedFollowingHousekeep, startSeverity, startCode)));			
								}
								else
								{
									Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Engine_StartFailed, startSeverity, startCode)));	
								}
							
								engine.DataSequence = previousDataSequence;
							}
						}
					}
					else
					{
						if ((startSeverity.Length != 0) && (TDTraceSwitch.TraceWarning))
						{
							Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, String.Format(Messages.Engine_StartFeedback, startSeverity, startCode)));		
						}
					}
				}
                else if (!allowInitialise)
                {
                    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Engine_StartNotDone, this.objectId)));
                }
			}

			return !failed;
		}


		/// <summary>
		/// Initialises engines that are not currently in use. 
		/// </summary>
		/// <param name="ignoreSequence">
		/// True if initialisation should be performed regardless of whether engines data sequences match the pool data sequence.
		/// </param>
        /// <param name="allowInitialise">Flag must be true to ensure the engines are started</param>
		/// <remarks>
		/// Engines that fail initialisation are removed from the pool permanently.
		/// </remarks>
		private void InitialiseEngines(bool ignoreSequence, bool allowInitialise)
		{
			lock (freeEngines.SyncRoot) 
			{
				IEnumerator enu = freeEngines.Keys.GetEnumerator();
				int[] failedKeys = new int[poolSizeNum];
				int failedCount = 0;
					
				while (enu.MoveNext()) 
				{
					Engine engine = null;
					int key = (int) enu.Current; 
					engine = freeEngines[key] as Engine;
						
					if (!InitialiseEngine(engine, ignoreSequence, false, allowInitialise))
					{
						failedKeys[failedCount] = key;
						failedCount++;
					}
				}
				
				for (int failed=0; failed < failedCount; failed++)
				{
					Engine engine = freeEngines[failedKeys[failed]] as Engine;
					freeEngines.Remove(failedKeys[failed]);
					engine.Dispose();
					engine = null;
				}
			}

		}

        /// <summary>
        /// Terminates an engine
        /// </summary>
        /// <param name="engine">Engine to terminate</param>
        /// <param name="forceTerminate">Flag to force a call to stop engine even if it is not running.
        /// If a force terminate is used and the engine isn't running, errors will be logged and false returned</param>
        /// <returns>True if successful, else false if unsuccessful.</returns>
        private bool TerminateEngine(Engine engine, bool forceTerminate, bool housekeep)
        {
            bool failed = false;

            if ((engine.Running) || (forceTerminate))
            {
                    string stopSeverity;
                    string stopCode;

                    if (!engine.Stop(out stopSeverity, out stopCode))
                    {
                        failed = true;

                        if (housekeep && (this.housekeepingStatus == HousekeepingStatusId.InProgress))
                            Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Engine_StopFailedForHousekeep, stopSeverity, stopCode)));
                        else
                            Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Engine_StopFailed, stopSeverity, stopCode)));

                    }
                    else
                    {
                        if ((stopSeverity.Length != 0) && (TDTraceSwitch.TraceWarning))
                            Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, String.Format(Messages.Engine_StopFeedback, stopSeverity, stopCode)));
                    }
            }

            return !failed;
        }

        /// <summary>
        /// Initialises engines that are not currently in use. 
        /// </summary>
        /// <param name="ignoreSequence">
        /// True if initialisation should be performed regardless of whether engines data sequences match the pool data sequence.
        /// </param>
        /// <param name="forceTerminate">Flag to force a call to stop engines even if they are not running.
        /// If a force terminate is used and the engine isn't running, errors will be logged</param>
        /// <remarks>
        /// Engines that fail initialisation are removed from the pool permanently.
        /// </remarks>
        public void TerminateEngines(bool forceTerminate)
        {
            lock (freeEngines.SyncRoot)
            {
                IEnumerator enu = freeEngines.Keys.GetEnumerator();
                int[] failedKeys = new int[poolSizeNum];
                int failedCount = 0;

                while (enu.MoveNext())
                {
                    Engine engine = null;
                    int key = (int)enu.Current;
                    engine = freeEngines[key] as Engine;

                    if (!TerminateEngine(engine, forceTerminate, false))
                    {
                        failedKeys[failedCount] = key;
                        failedCount++;
                    }
                }

                for (int failed = 0; failed < failedCount; failed++)
                {
                    Engine engine = freeEngines[failedKeys[failed]] as Engine;
                    freeEngines.Remove(failedKeys[failed]);
                    engine.Dispose();
                    engine = null;
                }
            }

            lock (usedEngines.SyncRoot)
            {
                IEnumerator enu = usedEngines.Keys.GetEnumerator();

                while (enu.MoveNext())
                {
                    int key = (int)enu.Current;
                    Engine engine = usedEngines[key] as Engine;
                    engine.NeedsTerminating = true;
                }
            }

        }

		/// <summary>
		/// Timer delegate that performs housekeeping on an engine.
		/// </summary>
		/// <param name="stateInfo">Not used.</param>
		/// <remarks>
		/// This method is called asynchronously by a timer.
		/// It is called asynchronously since the housekeeping operation
		/// may be time-consuming.
		/// </remarks>
		protected virtual void PerformHousekeeping(Object stateInfo)
		{		
			// Stop the timer to prevent checking while housekeeping in process.
			housekeepingCheckTimer.Change(Timeout.Infinite, Timeout.Infinite);
			
			// Attempt housekeeping on an engine.
			bool housekeepingAttempted = false;
			bool housekeepingSuccess = false;
			HousekeepFreeEngine(ref housekeepingAttempted, ref housekeepingSuccess);

			if (housekeepingSuccess)
			{
				// Initialise any engines currently in the free list. 
				// Engines in used list will be intialised later,
				// when business objects using them are returned to the pool.
				InitialiseEngines(false, allowInitialiseEngine);
				
				// Check pool size after initialising engines.
				int totalEngines = freeEngines.Count + usedEngines.Count;
				if (totalEngines < this.minimumPoolSize)
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Pool_SizeLessThanMinimum, this.objectId, this.maxPoolSize, totalEngines)));

				this.housekeepingStatus = HousekeepingStatusId.Idle;
                this.housekeepingCompleted = true;
			}
			else if (!housekeepingAttempted)
			{
				// No engines are free to attempt housekeeping so try again later...
				housekeepingCheckTimer.Change(new TimeSpan(0,0,0,0), this.housekeepingCheckFrequency);
			}
			else if (housekeepingAttempted && !housekeepingSuccess)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Pool_HousekeepingFailed, this.housekeepingFeedId, this.housekeepingFile)));
				this.housekeepingStatus = HousekeepingStatusId.Idle;
                this.housekeepingCompleted = true;
			}
		}

		/// <summary>
		/// Gets the number of engines in the used pool that have timed out.
		/// </summary>
		private int Timeouts
		{
			get 
			{
				int timeouts = 0;

				lock (usedEngines.SyncRoot) 
				{
					IEnumerator enu = usedEngines.Keys.GetEnumerator();
					
					while (enu.MoveNext()) 
					{
						int key = (int) enu.Current; 
						Engine engine = usedEngines[key] as Engine;
						if (engine.TimedOut)
						{
							timeouts++;
						}
					}
				}

				return timeouts;
			}
		}

		/// <summary>
		/// Gets the number of engines in the free pool that need reinitialising
		/// </summary>
		private int EnginesNeedingInitialisation
		{
			get 
			{
				int inits = 0;

				lock (freeEngines.SyncRoot) 
				{
					IEnumerator enu = freeEngines.Keys.GetEnumerator();
					
					while (enu.MoveNext()) 
					{
						int key = (int) enu.Current; 
						
						Engine engine = freeEngines[key] as Engine;
					
						if (engine.NeedsReinitialisation)
						{
							inits++;
						}
					}
				}

				return inits;
			}
		}

		/// <summary>
		/// Sets status of all engines in the pool to indicate 
		/// that reinitialisation following an error is needed.
		/// </summary>
		public void SetInitialisationNeeded()
		{
			lock (freeEngines.SyncRoot) 
			{
				IEnumerator enu = freeEngines.Keys.GetEnumerator();
				
				while (enu.MoveNext()) 
				{
					int key = (int) enu.Current; 
					Engine engine = freeEngines[key] as Engine;
					engine.NeedsReinitialisation = true;
				}
			}

			lock (usedEngines.SyncRoot) 
			{
				IEnumerator enu = usedEngines.Keys.GetEnumerator();
				
				while (enu.MoveNext()) 
				{
					int key = (int) enu.Current; 
					Engine engine = usedEngines[key] as Engine;
					engine.NeedsReinitialisation = true;
				}
			}

		}

		
		/// <summary>
		/// Checks whether re-initialisation is now due.
		/// </summary>
		/// <remarks>
		/// Currently only implemented by the RVBOPool subclass. 
		/// </remarks>
		protected virtual bool ReinitialisationDue
		{
			get { return false; }
		}

		/// <summary>
		/// Clears indidicator that re-initialisation is due.
		/// </summary>
		/// <remarks>
		/// Currently only implemented by the RVBOPool subclass. 
		/// </remarks>
		protected virtual void ResetReinitialisationNeeded()
		{
		}


		/// <summary>
		/// Puts timed out engines from used list back into free list 
		/// and updates engine allocation ids to prevent business objects
		/// from using the engines again.
		/// Signals to waiting threads if engines are added to free list.
		/// </summary>
		/// <remarks>
		/// Also initialises engine if not using the current pool data sequence.
		/// </remarks>
		private void ReleaseTimeouts()
		{
			lock (this)
			{
				if (TDTraceSwitch.TraceInfo)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Pool_TimeoutReleaseStarted, this.objectId)));
				}

				// Stop timer to prevent timeout checking during this release.
				// This is for performance only.
				timeoutCheckTimer.Change(Timeout.Infinite, Timeout.Infinite);

				int[] timedOutKeys = new int[poolSizeNum];
				int timedOutCount = 0;
				int addedCount = 0;

				IEnumerator enu = usedEngines.Keys.GetEnumerator();

				// Determine which engines have timed out.
				while (enu.MoveNext()) 
				{
					int key = (int) enu.Current; 
					Engine engine = usedEngines[key] as Engine;
					if (engine.TimedOut)
					{	
						timedOutKeys[timedOutCount] = key;
						timedOutCount++;
					}
				}

				// Remove timed out engine from used list and put in free list.
				// Cannot do this in above code because not possible to remove from a hash list while reading it.
				for (int count=0; count < timedOutCount; count++)
				{
					Engine engine = usedEngines[timedOutKeys[count]] as Engine;
					usedEngines.Remove(timedOutKeys[count]);
					bool engineInvalidated = false;

					// Reinitialise timed-out engine. Do this even if the data sequence number 
					//  hasn't been altered by housekeeping while engine was in use, becuase 
					//  it may have timed out while the actual DLL call was taking place, in which  
					//  case we want to put the DLL back into a determinate state ...

					if (!InitialiseEngine(engine, true, false, allowInitialiseEngine))
					{
						// Engine failed to intialise - remove it from service.
						engineInvalidated = true;
						
						int totalEngines = freeEngines.Count + usedEngines.Count;

						// Log error if total number of engines in service dips below minimum required.
						if (totalEngines < this.minimumPoolSize)
						{
							Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Pool_SizeLessThanMinimum, this.objectId, this.maxPoolSize, totalEngines)));
						}
					}

					// Add engine to the free list.
					if (!engineInvalidated)
					{
						freeEngines.Add(timedOutKeys[count], engine);
						addedCount++;
					}
								
				}
				
				// If added engines to free then signal to waiting threads.
				if (addedCount > 0)
				{	
					if (!NotifyWaitingThreads())
					{
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, Messages.Pool_SignalReleaseFailed));
						throw new TDException(Messages.Pool_SignalReleaseFailed, false, TDExceptionIdentifier.PRHSignalReleaseFailed);
					}
					
				}

				// Restart timeout checking.
				timeoutCheckTimer.Change(this.timeoutCheckFrequency, this.timeoutCheckFrequency);

				if (TDTraceSwitch.TraceInfo)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, String.Format(Messages.Pool_TimeoutReleaseCompleted, timedOutCount, this.objectId)));
				}
			}
		}

		#endregion
	}
}
