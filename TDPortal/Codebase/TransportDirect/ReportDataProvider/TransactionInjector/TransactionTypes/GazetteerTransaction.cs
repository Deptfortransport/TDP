// *********************************************** 
// NAME			: GazetteerTransaction.cs
// AUTHOR		: Peter Norell
// DATE CREATED	: 07/01/2004
// DESCRIPTION	: Gazetteer transaction events.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/GazetteerTransaction.cs-arc  $
//
//   Rev 1.3   May 18 2010 12:14:24   MTurner
//Added extra error trapping
//Resolution for 5537: TI Active polling may miss some exceptions
//
//   Rev 1.2   Mar 22 2010 10:14:30   MTurner
//Updates to add new active poll functionality.
//Resolution for 5472: SLA02 failing to achieve SLA
//
//   Rev 1.1   Mar 19 2009 15:47:24   mturner
//Manual merge of stream 5215 due to failure of automatic merge.
//
//   Rev 1.0.1.0   Jan 13 2009 17:16:00   mturner
//Updated for tech refresh
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.0   Nov 08 2007 12:39:50   mturner
//Initial revision.
//
//   Rev 1.6   Jun 22 2004 15:37:26   passuied
//IR 1040 : Enhancements for Transaction Injector. 
//Resolution for 1040: Enhancements to TransactionInjector 6.0
//
//   Rev 1.5   May 14 2004 16:52:50   GEaton
//IR882
//
//   Rev 1.4   May 12 2004 17:55:44   GEaton
//IR866 - clean up after time outs
//
//   Rev 1.3   Apr 23 2004 17:21:48   geaton
//IR827 - Allow timeout specification for transactions.
//
//   Rev 1.2   Feb 12 2004 09:31:18   geaton
//Incident 642 - log exceptions as reference transaction events.
//
//   Rev 1.1   Jan 09 2004 12:41:16   PNorell
//Updated transactions.
//
//   Rev 1.0   Jan 08 2004 19:41:48   PNorell
//Initial Revision
using System;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Net;

using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Summary description for GazetteerTransaction.
	/// </summary>
	[Serializable]
	public class GazetteerTransaction : TDTransaction
	{

		private SearchType searchType;
		/// <summary>
		/// Gets/Sets the searchtype used for the gazetteer
		/// </summary>
		public SearchType SearchType 
		{
			get { return this.searchType; }
			set { this.searchType = value; }
		}

		private string searchText;
		/// <summary>
		/// Gets/Sets the search text used for the gazetteer
		/// </summary>
		public string SearchText 
		{
			get { return this.searchText; }
			set { this.searchText = value; }
		}

		private bool fuzzy;
		/// <summary>
		/// Gets/Sets if the search should be fuzzy or not
		/// </summary>
		public bool Fuzzy
		{
			get { return this.fuzzy; }
			set { this.fuzzy = value; }
		}

		private bool picklist;
		/// <summary>
		/// Gets/Sets if this search should result in a picklist (multiple items) or not
		/// </summary>
		public bool Picklist
		{
			get { return this.picklist; }
			set { this.picklist = value; }
		}

		private bool drilldown;
		/// <summary>
		/// Gets/Sets if this search should do a drill down instead of getting location details
		/// </summary>
		public bool Drilldown
		{
			get { return this.drilldown; }
			set { this.drilldown = value; }
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
		/// Submits a Gazetteer Reference Transaction.
		/// </summary>
		public override void ExecuteTransaction()
		{
            if ((DateTime.Now.Minute - offset) % frequency == 0)
            {
                try
                {
                    string resultData = String.Empty;

                    RequestGazetteerParams rgp = new RequestGazetteerParams();
                    rgp.searchType = SearchType;
                    rgp.fuzzy = Fuzzy;
                    rgp.searchText = SearchText;
                    rgp.picklist = Picklist;
                    rgp.drilldown = Drilldown;

                    bool success = false;

                    Webservice.Timeout = (int)this.Timeout.TotalMilliseconds;

                    LogStart();

                    try
                    {
                        success = Webservice.RequestGazetteer(rgp, ref resultData);
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
                    LogBadEnd(String.Format(Messages.Service_GazetteerException, exception.Message));
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
