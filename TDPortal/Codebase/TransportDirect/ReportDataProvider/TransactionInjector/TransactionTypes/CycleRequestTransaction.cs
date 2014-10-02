// *********************************************** 
// NAME			: CycleRequestTransaction.cs
// AUTHOR		: Mark Turner
// DATE CREATED	: 04/08/2008 
// DESCRIPTION	: Provides class for a Cycle journey
// request reference transaction.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/CycleRequestTransaction.cs-arc  $
//
//   Rev 1.4   May 18 2010 12:15:34   mturner
//Added extra error trapping
//Resolution for 5537: TI Active polling may miss some exceptions
//
//   Rev 1.3   Mar 22 2010 10:14:22   MTurner
//Updates to add new active poll functionality.
//Resolution for 5472: SLA02 failing to achieve SLA
//
//   Rev 1.2   Mar 16 2009 12:24:04   build
//Automatically merged from branch for stream5215
//
//   Rev 1.1.1.2   Jan 14 2009 13:55:12   mturner
//Fixed bug in previous version that Stopped ReturnDayOfWeek from getting set.
//
//   Rev 1.1.1.1   Jan 13 2009 17:16:58   mturner
//Further tech refresh updates
//
//   Rev 1.1.1.0   Jan 13 2009 12:23:14   mturner
//Updates for tech refresh
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.1   Sep 10 2008 14:06:18   mturner
//Updated to use real class rather than the interface for serialization
//
//   Rev 1.0   Aug 04 2008 16:14:42   mturner
//Initial revision.


using System;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Web.Services.Protocols;
using System.Net;

using TransportDirect.Common.Logging;
using TransportDirect.Common;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.CyclePlannerService;
using TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService;
using TransportDirect.ReportDataProvider.TransactionHelper;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{	
	/// <summary>
	/// Class used to inject cycle journey request data into the transaction web service.
	/// </summary>
	[Serializable]
	public class CycleRequestTransaction : TDTransaction 
	{		
		private TDCyclePlannerRequest requestData = new TDCyclePlannerRequest();
        
        private int minNumberOutwardCycleJourney;
		private int minNumberReturnCycleJourney;
		
        private int outwardDayOfWeek;
		private TDTimeSpan outwardTime;
        private int returnDayOfWeek;
		private TDTimeSpan returnTime;

        private int frequency;
        private int offset;

        /// <summary>
        /// Get/Set the frequency (in minutes) that the journey is to be injected with.
        /// </summary>
        /// <remarks>
        /// Property is public and Set is provided 
        /// to allow deserialization of class instance from XML.
        /// </remarks>
        public int Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }

        /// <summary>
        /// Get/Set the offset of the transaction.
        /// </summary>
        /// <remarks>
        /// Property is public and Set is provided 
        /// to allow deserialization of class instance from XML.
        /// </remarks>
        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        /// <summary>
        /// Get/Set minimum number of outward road journey count.
        /// </summary>
        /// <remarks>
        /// Property is public and Set is provided 
        /// to allow deserialization of class instance from XML.
        /// </remarks>
        public int MinNumberOutwardCycleJourney
        {
            get { return minNumberOutwardCycleJourney; }
            set { minNumberOutwardCycleJourney = value; }
        }

        /// <summary>
        /// Get/Set minimum number of return road journey count.
        /// </summary>
        /// <remarks>
        /// Property is public and Set is provided 
        /// to allow deserialization of class instance from XML.
        /// </remarks>
        public int MinNumberReturnCycleJourney
        {
            get { return minNumberReturnCycleJourney; }
            set { minNumberReturnCycleJourney = value; }
        }

		/// <summary>
		/// Get and sets the outward day of week.
		/// </summary>
		/// <remarks>
		/// Property is public and Set is provided 
		/// to allow deserialization of class instance from XML.
		/// </remarks>
		public int OutwardDayOfWeek
		{
			get { return outwardDayOfWeek;  }
			set { outwardDayOfWeek = value; }
		}

		/// <summary>
		/// Get and sets the outward time.
		/// </summary>
		/// <remarks>
		/// Property is public and Set is provided 
		/// to allow deserialization of class instance from XML.
		/// </remarks>
		public TDTimeSpan OutwardTime
		{
			get { return outwardTime;  }
			set { outwardTime = value; }
		}

		/// <summary>
		/// Get and sets the return day of week.
		/// </summary>
		/// <remarks>
		/// Property is public and Set is provided 
		/// to allow deserialization of class instance from XML.
		/// </remarks>
		public int ReturnDayOfWeek
		{
			get { return returnDayOfWeek;  }
			set { returnDayOfWeek = value; }
		}

		/// <summary>
		/// Get and sets the return time from now.
		/// </summary>
		/// <remarks>
		/// Property is public and Set is provided 
		/// to allow deserialization of class instance from XML.
		/// </remarks>
		public TDTimeSpan ReturnTime
		{
			get { return returnTime;  }
			set { returnTime = value; }
		}
		
		
		/// <summary>
		/// Gets and sets the request data.
		/// </summary>
		/// <remarks>
		/// Property is public and Set is provided 
		/// to allow deserialization of class instance from XML.
		/// </remarks>
		public TDCyclePlannerRequest RequestData
		{
			get { return requestData;  }
			set { requestData = value; }
		}

        private bool activePoll;
        /// <summary>
        /// If true injection Injects a poll to the TransactionWebService
        /// when Offset and Interval dictate that no actual injection should
        /// take place.
        /// </summary>
        public bool ActivePoll
        {
            get { return activePoll; }
            set { activePoll = value; }
        }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public CycleRequestTransaction() : base( )
		{

		}

		/// <summary>
		/// Executes the query, and logs the time take to run the query.
		/// Logs an error message if the call to the webservice failed or an exception is thrown.
		/// </summary>
		public override void ExecuteTransaction()
		{	
	        if((DateTime.Now.Minute - offset) % frequency == 0)
            {
			    try
			    {			
				    string resultData = String.Empty;
                    RequestCycleJourneyParams requestCycleJourneyParams = new RequestCycleJourneyParams();
                   
                    // Using transaction properties, construct parameters to call web service.
				    requestCycleJourneyParams.request = this.requestData;
                    requestCycleJourneyParams.minNumberOutwardCycleJourneyCount = this.minNumberOutwardCycleJourney;
                    requestCycleJourneyParams.minNumberReturnCycleJourneyCount = this.minNumberReturnCycleJourney;
				    requestCycleJourneyParams.sessionId = this.SessionId;
    				
				    // Calculate journey times - must be done at runtime in case journey times are relative to current date/time.
				    requestCycleJourneyParams.dtOutwardDateTime = 
					    TDTransaction.CalculateActualJourneyTime(this.requestData.OutwardDateTime[0],
															     this.outwardDayOfWeek,
															     this.outwardTime );
				    if (requestData.IsReturnRequired)
					    requestCycleJourneyParams.dtReturnDateTime = 
						    TDTransaction.CalculateActualJourneyTime(this.requestData.ReturnDateTime[0],
																     this.returnDayOfWeek,
																     this.returnTime );

    				
				    // Convert to binary string (otherwise data is lost in transit to web service!)
				    string requestJourneyParamsBinary = RequestCycleJourneyParamsUtil.ToBase64Str(requestCycleJourneyParams);

				    bool success = false;

				    Webservice.Timeout = (int)this.Timeout.TotalMilliseconds;
                    
				    // Log the start of the call.
				    LogStart();

				    try
				    {
					    success = Webservice.RequestCycleJourney(requestJourneyParamsBinary, ref resultData);
				    }
				    catch (WebException we)
				    {
					    success = false;
					    resultData = string.Format(Messages.Injector_TransactionTimedOut, we.Message);
    					
				    }

				    if (success)				
					    LogEnd(resultData);
				    else	
					    LogBadEnd(resultData);
                }
                catch (Exception exception)
                {
                    LogBadEnd(String.Format(Messages.Service_CycleRequestException, this.Category.ToString(), exception.Message));
                }
            }
            else
            {
                // activePoll can be set to true in the request XML
                // The purpose of this is to create traffic between the TI and TWS to stop
                // connections dropping out through inactivity.
                try
                {
                    if (activePoll == true)
                    {
                        Webservice.TestActive();
                    }
                    LogSkipped();
                }
                catch (Exception)
                {
                    // We are only making this call to keep open the connection
                    // so aren't actually interested in any exceptions thrown
                }
            }
		}
	}
}


