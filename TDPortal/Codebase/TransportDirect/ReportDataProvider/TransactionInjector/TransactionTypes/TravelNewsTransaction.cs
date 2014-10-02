//*********************************************** 
// NAME			: TravelNewsTransaction.cs
// AUTHOR		: Mark Turner
// DATE CREATED	: 14/01/2009
// DESCRIPTION	: Travel News transaction events.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/TravelNewsTransaction.cs-arc  $
//
//   Rev 1.2   May 18 2010 11:58:38   mturner
//Added extra error trapping
//Resolution for 5537: TI Active polling may miss some exceptions
//
//   Rev 1.1   Mar 22 2010 10:14:22   mturner
//Updates to add new active poll functionality.
//Resolution for 5472: SLA02 failing to achieve SLA
//
//   Rev 1.0   Jan 14 2009 17:39:56   mturner
//Initial revision.
//Resolution for 5215: Workstream for RS620

using System;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Net;

using TransportDirect.Common.Logging;
using TransportDirect.ReportDataProvider.TransactionHelper;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Summary description for TravelNewsTransaction.
	/// </summary>
	[Serializable]
	public class TravelNewsTransaction : TDTransaction
	{

		private string region;
		/// <summary>
		/// Gets/Sets the region used for the search
		/// </summary>
		public string Region 
		{
			get { return this.region; }
			set { this.region = value; }
		}

        private string transport;
        /// <summary>
        /// Gets/Sets the transport used for the search
        /// </summary>
        public string Transport
        {
            get { return this.transport; }
            set { this.transport = value; }
        }

        private string delays;
        /// <summary>
        /// Gets/Sets the delays used for the search
        /// </summary>
        public string Delays
        {
            get { return this.delays; }
            set { this.delays = value; }
        }

        private string type;
        /// <summary>
        /// Gets/Sets the type used for the search
        /// </summary>
        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        private int day;
        /// <summary>
        /// The day used for a search
        /// </summary>
        public int Day
        {
            get { return this.day; }
            set { this.day = value; }
        }

        private int offset;
        /// <summary>
        /// The offset for a transacation.
        /// </summary>
        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        private int frequency;
        /// <summary>
        /// The frequency of a transacation.
        /// </summary>
        public int Frequency
        {
            get { return frequency; }
            set { frequency = value; }
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
		/// Submits a TravelNews Reference Transaction.
		/// </summary>
		public override void ExecuteTransaction()
		{
            if ((DateTime.Now.Minute - offset) % frequency == 0)
            {
                try
                {
                    string resultData = String.Empty;

                    RequestTravelNewsParams tnp = new RequestTravelNewsParams();
                    tnp.day = Day;
                    tnp.delays = Delays;
                    tnp.region = Region;
                    tnp.transport = Transport;
                    tnp.type = Type;
                    
                    bool success = false;

                    Webservice.Timeout = (int)this.Timeout.TotalMilliseconds;

                    LogStart();

                    try
                    {
                        success = Webservice.RequestTravelNews(tnp, ref resultData);
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
                    LogBadEnd(String.Format(Messages.Service_TravelNewsException, exception.Message));
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