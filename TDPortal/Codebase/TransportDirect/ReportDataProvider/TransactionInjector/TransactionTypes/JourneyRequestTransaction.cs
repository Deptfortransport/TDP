// *********************************************** 
// NAME			: JourneyRequestTransaction.cs
// AUTHOR		: M.Turner
// DATE CREATED	: 12/01/09 
// DESCRIPTION	: Provides class for a journey request reference transaction.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/JourneyRequestTransaction.cs-arc  $
//
//   Rev 1.3   May 18 2010 12:14:56   mturner
//Added extra error trapping
//
//   Rev 1.2   Mar 22 2010 10:14:24   mturner
//Updates to add new active poll functionality.
//Resolution for 5472: SLA02 failing to achieve SLA
//
//   Rev 1.1   Mar 16 2009 12:24:04   build
//Automatically merged from branch for stream5215
//
//   Rev 1.0.1.3   Jan 14 2009 17:49:52   mturner
//Fixed bug that was leading to wrong TWS method being called
//
//   Rev 1.0.1.2   Jan 13 2009 17:37:34   mturner
//Added logic to 'skip' transactions when neccessary
//
//   Rev 1.0.1.1   Jan 13 2009 12:25:20   mturner
//Further updates for tech refresh
//
//   Rev 1.0.1.0   Jan 12 2009 16:20:14   mturner
//Updated for tech refresh
//Resolution for 5215: Workstream for RS620

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
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.ReportDataProvider.TransactionHelper;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{	
	/// <summary>
	/// Class used to inject journey request data into the transaction web service.
	/// </summary>
	[Serializable]
	public class JourneyRequestTransaction : TDTransaction 
	{		
		private TDJourneyRequest requestData = new TDJourneyRequest();
		
		private int minNumberOutwardRoadJourney;
		private int minNumberReturnRoadJourney;
		private int minNumberOutwardPublicJourney;
		private int minNumberReturnPublicJourney;

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
		public int MinNumberOutwardRoadJourney
		{
			get { return minNumberOutwardRoadJourney;  }
			set { minNumberOutwardRoadJourney = value; }
		}

		/// <summary>
		/// Get/Set minimum number of return road journey count.
		/// </summary>
		/// <remarks>
		/// Property is public and Set is provided 
		/// to allow deserialization of class instance from XML.
		/// </remarks>
		public int MinNumberReturnRoadJourney
		{
			get { return minNumberReturnRoadJourney;  }
			set { minNumberReturnRoadJourney = value; }
		}

		/// <summary>
		/// Get/Set minimum number of outward public journey count.
		/// </summary>
		/// <remarks>
		/// Property is public and Set is provided 
		/// to allow deserialization of class instance from XML.
		/// </remarks>
		public int MinNumberOutwardPublicJourney
		{
			get { return minNumberOutwardPublicJourney;  }
			set { minNumberOutwardPublicJourney = value; }
		}

		/// <summary>
		/// Get/Set minimum number of return public journey count.
		/// </summary>
		/// <remarks>
		/// Property is public and Set is provided 
		/// to allow deserialization of class instance from XML.
		/// </remarks>
		public int MinNumberReturnPublicJourney
		{
			get { return minNumberReturnPublicJourney;  }
			set { minNumberReturnPublicJourney = value; }
		}
		
		/// <summary>
        /// Get and sets the Day of the week to plan the outward journey for.
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
		/// Get and sets the Day of the week to plan the return journey for.
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
		public TDJourneyRequest RequestData
		{
			get { return requestData;  }
			set { requestData = value; }
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		public JourneyRequestTransaction() : base( )
		{

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
		/// Executes the query, and logs the time take to run the query.
		/// Logs an error message if the call to the webservice failed or an exception is thrown.
		/// </summary>
		public override void ExecuteTransaction()
		{
            if ((DateTime.Now.Minute - offset) % frequency == 0)
            {
                try
                {
                    string resultData = String.Empty;

                    // Using transaction properties, construct parameters to call web service.
                    RequestJourneyParams requestJourneyParams = new RequestJourneyParams();
                    requestJourneyParams.request = this.requestData;
                    requestJourneyParams.minNumberOutwardRoadJourneyCount = this.minNumberOutwardRoadJourney;
                    requestJourneyParams.minNumberReturnRoadJourneyCount = this.minNumberReturnRoadJourney;
                    requestJourneyParams.minNumberOutwardPublicJourneyCount = this.minNumberOutwardPublicJourney;
                    requestJourneyParams.minNumberReturnPublicJourneyCount = this.minNumberReturnPublicJourney;
                    requestJourneyParams.sessionId = this.SessionId;

                    // Calculate journey times - must be done at runtime in case journey times are relative to current date/time.
                    requestJourneyParams.dtOutwardDateTime =
                        TDTransaction.CalculateActualJourneyTime(this.requestData.OutwardDateTime[0], this.OutwardDayOfWeek, this.outwardTime);
                    if (requestData.IsReturnRequired)
                        requestJourneyParams.dtReturnDateTime =
                            TDTransaction.CalculateActualJourneyTime(this.requestData.ReturnDateTime[0], this.ReturnDayOfWeek, this.returnTime);


                    // Convert to binary string (otherwise data is lost in transit to web service!)
                    string requestJourneyParamsBinary = RequestJourneyParamsUtil.ToBase64Str(requestJourneyParams);

                    bool success = false;

                    Webservice.Timeout = (int)this.Timeout.TotalMilliseconds;

                    // Log the start of the call.
                    LogStart();

                    try
                    {
                        success = Webservice.RequestJourney(requestJourneyParamsBinary, ref resultData);
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
                    LogBadEnd(String.Format(Messages.Service_JourneyRequestGazException, this.Category.ToString(), exception.Message));
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


