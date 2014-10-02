// ********************************************************* 
// NAME			: TDTransaction.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 03/09/2003 
// DESCRIPTION	: Base class for all transaction types 
// injected by the injector component.
// ********************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/TDTransaction.cs-arc  $
//
//   Rev 1.4   Dec 14 2011 11:48:26   MTurner
//Emergency fix for Christmas handling bug
//Resolution for 5775: Emergency fix for Traveline Checker Christmas Bug
//
//   Rev 1.3   Jul 16 2010 09:39:30   PScott
//IR 5561  -  Changes after review. Read injector machine name at load and persist through each group.
//Old way did it in part of object passed through to other servers and the webserver names were being picked up instead of injector name.
//Resolution for 5561: Capture All Transaction Injectors Data to file.
//
//   Rev 1.2   Jun 28 2010 14:07:50   PScott
//SCR 5561 - write MachineName to reference transactions
//
//   Rev 1.1   Mar 16 2009 12:24:04   build
//Automatically merged from branch for stream5215
//
//   Rev 1.0.1.3   Jan 21 2009 10:08:58   mturner
//Added subsystem property and a mechanism to have two levels of threshold alert (red and amber) rather than just one.
//
//   Rev 1.0.1.2   Jan 14 2009 15:07:12   mturner
//Fixed bug in CalculateActualJourneyTime method
//
//   Rev 1.0.1.1   Jan 13 2009 17:34:22   mturner
//Added logic to 'Skip' injections when neccessary
//
//   Rev 1.0.1.0   Jan 13 2009 12:24:22   mturner
//Updates for tech refresh
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.0   Nov 08 2007 12:40:02   mturner
//Initial revision.
//
//   Rev 1.17   Aug 29 2007 11:18:50   nrankin
//USD: 1334515
//IR 4485
//
//Amend the Transaction Injector to avoid Xmas and other bank holidays (i.e 26th Dec and 1st Jan)
//Resolution for 4485: Amend the Transaction Injector to avoid Xmas and other bank holidays
//
//   Rev 1.16   Nov 12 2004 12:54:54   rhopkins
//Change so that journeys transactions are not injected for "special" days - Friday, Saturday, Sunday, Monday.  Inject them for the next "normal" day.
//
//   Rev 1.15   May 14 2004 16:52:58   GEaton
//IR882
//
//   Rev 1.14   Apr 23 2004 17:22:16   geaton
//IR827 - Allow timeout specification for transactions.
//
//   Rev 1.13   Feb 16 2004 17:31:34   geaton
//Incident 643. Allow TDTransactions to be cloned (allows separate instances to be injected rather than same instance).
//
//   Rev 1.12   Dec 02 2003 20:07:58   geaton
//Log unsuccessful transactions.
//
//   Rev 1.11   Nov 13 2003 11:57:02   geaton
//Added method common to all transactions.
//
//   Rev 1.10   Nov 11 2003 15:50:14   geaton
//Added additional logging of results.
//
//   Rev 1.9   Nov 10 2003 12:32:48   geaton
//Removed TDTransactionCategory - a string will be used instead to allow new categories to be added at runtime.
//
//   Rev 1.8   Nov 06 2003 17:19:52   geaton
//Refactored following handover from JT to GE.

using System;
using System.Diagnostics;
using System.Threading;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Base class for all transaction types.
	/// </summary>
	[Serializable]
	public abstract class TDTransaction : IDisposable, ICloneable
	{
		private		string							category;
        private     string                          subSystem;
		private		bool							serviceLevelAgreement;
		private		string							sessionId;	
		private		TDTransactionServiceOverride	webservice;
		private		DateTime						submitted;
		private		TimeSpan						repeatFrequency;
		private		bool							disposed = false;
		private		TimeSpan						timeout;
        private     string                          machineName;

		/// <summary>
		/// Implements clone interface.
		/// </summary>
		/// <returns>Cloned instance of a transaction.</returns>
		public object Clone()
		{
			return this.Clone(true); // delegate to type safe clone
		} 

		
		/// <summary>
		/// Type safe clone method.
		/// </summary>
		/// <returns>Cloned transaction.</returns>
		public virtual TDTransaction Clone(bool shallow)
		{
			TDTransaction transaction = (TDTransaction)this.MemberwiseClone();
			
			return transaction;
		}


		/// <summary>
		/// Gets the frequency at which the transaction should be injected.
		/// </summary>
		public TimeSpan RepeatFrequency
		{
			get { return repeatFrequency; }
		}

		/// <summary>
		/// Gets the timeout value for the transaction.
		/// </summary>
		public TimeSpan Timeout
		{
			get { return timeout; }
		}

		/// <summary>
		/// Get and sets the transaction category.
		/// </summary>
		/// <remarks>
		/// Property is public and Set is provided 
		/// to allow deserialization of class instance from XML.
		/// </remarks>
		public string Category
		{
			get { return category;  }
			set { category = value; }
		}

        /// <summary>
        /// Get and sets the transaction sub-system.
        /// </summary>
        /// <remarks>
        /// Property is public and Set is provided 
        /// to allow deserialization of class instance from XML.
        /// </remarks>
        public string SubSystem
        {
            get { return subSystem; }
            set { subSystem = value; }
        }

		/// <summary>
		/// Gets and sets service level agreement flag.
		/// </summary>
		/// <remarks>
		/// Property is public and Set is provided 
		/// to allow deserialization of class instance from XML.
		/// </remarks>
		public bool ServiceLevelAgreement
		{
			get { return serviceLevelAgreement;  }
			set { serviceLevelAgreement = value; }
		}

		/// <summary>
		/// Gets the session id.
		/// </summary>
		public string SessionId
		{
			get { return sessionId;  }			
		}

        /// <summary>
        /// Get and sets the transaction category.
        /// </summary>
        /// <remarks>
        /// Property is public and Set is provided 
        /// to allow deserialization of class instance from XML.
        /// </remarks>
        public string MachineName
        {
            get { return machineName; }
            set {  machineName = value; }
        }

		/// <summary>
		/// Gets the web service.
		/// </summary>
		protected TDTransactionServiceOverride Webservice
		{
			get { return webservice;  }			
		}

		/// <summary>
		/// Initialises the transaction.
		/// Creates a unique session identifier to use when injecting the transaction.
		/// </summary>
		/// <param name="webservice">Web service that transaction must use.</param>
		/// <param name="frequency">Frequency at which transaction should be injected.</param>
		/// <param name="timeout">Duration after which the transaction should be timed out.</param>
		/// <remarks>
		/// No validation is performed on parameters passed.
		/// </remarks>
		public void Initialise(TDTransactionServiceOverride webservice, TimeSpan frequency, TimeSpan timeout)
		{
			this.sessionId  = Guid.NewGuid().ToString();
			this.webservice = webservice;
			this.repeatFrequency = frequency;
			this.timeout = timeout;

			if (TDTraceSwitch.TraceInfo)
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, this.sessionId, TDTraceLevel.Info, String.Format(Messages.Injector_InitialisedTransaction, Category.ToString(), this.repeatFrequency.TotalSeconds.ToString(), this.timeout.TotalSeconds.ToString())));
		}

		/// <summary>
		/// Must be implemented to execute the transaction using the configured web service.
		/// </summary>
		public abstract void ExecuteTransaction();

		/// <summary>
		/// Captures start time of the transaction.
		/// This method must be called immediately before injecting the transaction.
		/// </summary>
		protected void LogStart()
		{
			submitted = DateTime.Now;
		}

		/// <summary>
		/// Logs the transaction data for a transaction that returned the expected results.
		/// Must be called immediately after receiving results from the transaction.
		/// </summary>
		/// <param name="resultData">Result data.</param>
		/// <remarks>
		/// The current date/time will be used as the completed time, as a consequence of logging the ReferenceTransactionEvent.
		/// </remarks>
		protected void LogEnd(string resultData)
		{

            //try
            //{
            //    this.machineName = System.Environment.MachineName;
            //    this.machineName = 

            //}
            //catch (Exception e)
            //{
            //    this.machineName = "unknown";
            //    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, this.sessionId, TDTraceLevel.Verbose, String.Format("Unknown injector name "+e.Message, this.category, resultData)));
            //}

			// Log the reference transaction data.
            ReferenceTransactionEvent refTransEvent = new ReferenceTransactionEvent(this.category, this.serviceLevelAgreement, this.submitted, this.sessionId, true, this.machineName);
			Trace.Write(refTransEvent);

			// Log informational. 
			if (TDTraceSwitch.TraceVerbose)
			{
				if (resultData.Length == 0)
					resultData = Messages.Injector_NoResults;

				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, this.sessionId, TDTraceLevel.Verbose, String.Format(Messages.Service_TransactionSucceeded, this.category, resultData)));
			}

			// Check if time to execute transaction has exceeded either the Amber or Red thresholds (if a threshold has been set)
            string redAlertThresholdString = Properties.Current[String.Format(Keys.TransactionInjectorAlertRedThreshold, this.category)];
            string amberAlertThresholdString = Properties.Current[String.Format(Keys.TransactionInjectorAlertAmberThreshold, this.category)];

			if (redAlertThresholdString != null)
			{
				TimeSpan timeTaken = refTransEvent.Time - this.submitted;

				int redAlertThresholdMs = int.Parse(redAlertThresholdString);
                TimeSpan redThreshold = new TimeSpan(0, 0, 0, 0, redAlertThresholdMs);
               
                if (timeTaken > redThreshold)
                {
                    TimeSpan exceeded = timeTaken - redThreshold;
                    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure,
                                                     this.sessionId,
                                                     TDTraceLevel.Error,
                                                     String.Format(Messages.Service_TransactionExceededRedThreshold, this.subSystem, this.Category, redAlertThresholdMs, (int)exceeded.TotalMilliseconds)));
                }
                else
                {
                    if (amberAlertThresholdString != null)
                    {
                        int amberAlertThresholdMs = int.Parse(amberAlertThresholdString);
                        TimeSpan amberThreshold = new TimeSpan(0, 0, 0, 0, amberAlertThresholdMs);

                        if (timeTaken > amberThreshold)
                        {
                            TimeSpan exceeded = timeTaken - amberThreshold;
                            Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure,
                                                             this.sessionId,
                                                             TDTraceLevel.Error,
                                                             String.Format(Messages.Service_TransactionExceededAmberThreshold, this.subSystem, this.Category, amberAlertThresholdMs, (int)exceeded.TotalMilliseconds)));
                        }
                    }
                }
			}
		}

		/// <summary>
		/// Logs the transaction data for a transaction that did not return the expected results.
		/// Must be called immediately after receiving results from the transaction.
		/// </summary>
		/// <param name="resultData">Data that may be useful to determine why transaction failed.</param>
		protected void LogBadEnd(string resultData)
		{
            //try
            //{
            //    this.machineName = System.Environment.MachineName;
            //}
            //catch (Exception e)
            //{
            //    this.machineName = "unknown";
            //    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, this.sessionId, TDTraceLevel.Verbose, String.Format("Unknown injector name " + e.Message, this.category, resultData)));
            //}

			// Log the (failed) reference transaction data.
            Trace.Write(new ReferenceTransactionEvent(this.category, this.serviceLevelAgreement, this.submitted, this.sessionId, false, this.machineName));

			// Always log error if a reference transaction does not return expected results.
			Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, this.sessionId, TDTraceLevel.Error, String.Format(Messages.Service_TransactionFailed, this.subSystem, this.category, resultData)));					
		}

        /// <summary>
		/// Dummy placeholder for a transaction that was not actually injected.
		/// This occurs when this particular transaction needs to miss this cycle to be in line 
        /// with the schedule defined in TDP/OP/003 TD Portal Operating Procedures
        /// 
        /// This method has been created in case we want to monitor these non injections in the future.
		/// </summary>
        protected void LogSkipped()
		{			
            // Do nothing as we do not want to log this!
		}
      
		/// <summary>
		/// Calculates actual journey time based on configuration parameters.
		/// Allows relative dates and times to be specified in XML configuration files.
		/// </summary>
		/// <param name="absoluteDateTime">The absolute date and time specified.</param>
		/// <param name="timeSpanFromNow">The time span to be added to the current datetime.</param>
		/// <param name="time">The time of the journey.</param>
		/// <returns>The actual journey datetime.</returns>
		/// <remarks>
		/// Made public to allow unit testing.
		/// </remarks>
        public static DateTime CalculateActualJourneyTime(TDDateTime absoluteDateTime, int targetDay, TDTimeSpan time)
        {
            try
            {
                // Add span to current datetime.
                DateTime temp = DateTime.Now;

                bool dayValid = false;
                DayOfWeek dow = (DayOfWeek)targetDay;
                temp = temp.AddDays(1);
                while (!dayValid)
                {
                    if (temp.DayOfWeek != dow)
                    {
                        temp = temp.AddDays(1);
                    }
                    else
                    {
                        dayValid = true;
                    }
                }

                if (temp.Day == 25 && temp.Month == 12)
                {
                    temp = temp.AddDays(14);
                }
                else
                {
                    if (temp.Day == 26 && temp.Month == 12)
                    {
                        temp = temp.AddDays(7);
                    }
                    else 
                    {
                        // Avoid substitute bank holidays if Christmas fell on a weekend
                        if (temp.Day == 27 && temp.Month == 12 && temp.DayOfWeek == DayOfWeek.Monday)
                        {
                            temp = temp.AddDays(14);
                        }
                        else
                        {
                            if ((temp.Day == 27 || temp.Day == 28)&& temp.Month == 12 && temp.DayOfWeek == DayOfWeek.Tuesday)
                            {
                                temp = temp.AddDays(7);
                            }
                            else
                            {
                                // Should never be true but here for completeness
                                if (temp.Day == 1 && temp.Month == 1)
                                {
                                    temp = temp.AddDays(7);
                                }
                            }
                        }
                    }
                }
                
                return new DateTime(temp.Year, temp.Month, temp.Day,
                    time.Hours, time.Minutes, time.Seconds);
            }
            catch (Exception exception) // Insufficient documentation so catch all.
            {
                throw new TDException(String.Format(Messages.Service_FailedCalculatingJourneyTime, exception.Message), false, TDExceptionIdentifier.RDPTransactionInjectorPropertiesServiceFailed);
            }
        }
    
		/// <summary>
		/// Disposes of resources. 
		/// Can be called by clients (via Dispose()) or runtime (via destructor).
		/// </summary>
		/// <param name="disposing">
		/// True when called by clients.
		/// False when called by runtime.
		/// </param>
		public void Dispose(bool disposing)
		{
			// Check to see if Dispose has already been called.
			if (!this.disposed)
			{
				if (disposing)
				{
					// Dispose of any managed resources:			
				}
             
				// Dispose of any unmanaged resources:
				
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
		~TDTransaction()      
		{
			Dispose(false);
		}
	}
	
}
