// *********************************************** 
// NAME			: MapTransaction.cs
// AUTHOR		: Peter Norell
// DATE CREATED	: 07/01/2004
// DESCRIPTION	: 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/MapTransaction.cs-arc  $
//
//   Rev 1.3   May 18 2010 12:13:42   MTurner
//Added extra error trapping
//Resolution for 5537: TI Active polling may miss some exceptions
//
//   Rev 1.2   Mar 22 2010 10:14:34   mturner
//Updates to add new active poll functionality.
//Resolution for 5472: SLA02 failing to achieve SLA
//
//   Rev 1.1   Mar 19 2009 15:45:28   mturner
//Manual merge of stream 5215 due to failure of automatic merge.
//
//   Rev 1.0.1.0   Jan 13 2009 17:17:56   mturner
//Tech refresh updates
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.0   Nov 08 2007 12:39:58   mturner
//Initial revision.
//
//   Rev 1.6   Jun 22 2004 15:37:50   passuied
//Enhancements for TI
//
//   Rev 1.5   May 14 2004 16:52:54   GEaton
//IR882
//
//   Rev 1.4   May 12 2004 17:55:38   GEaton
//IR866 - clean up after time outs
//
//   Rev 1.3   Apr 23 2004 17:22:00   geaton
//IR827 - Allow timeout specification for transactions.
//
//   Rev 1.2   Feb 12 2004 09:31:12   geaton
//Incident 642 - log exceptions as reference transaction events.
//
//   Rev 1.1   Jan 09 2004 12:41:12   PNorell
//Updated transactions.
//
//   Rev 1.0   Jan 08 2004 19:41:52   PNorell
//Initial Revision
using System;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Net;

using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Summary description for MapTransaction.
	/// </summary>
	[Serializable]
	public class MapTransaction : TDTransaction
	{

		private string type;
		/// <summary>
		/// Gets/sets the type of map request to do
		/// </summary>
		public string Type
		{
			get { return type; }
			set { type = value; }
		}

		private string northing;
		/// <summary>
		/// Gets/sets a list of northing to randomise between, the list is separated by | characters
		/// </summary>
		public string Northing
		{
			get { return northing; }
			set { northing = value; }
		}

		private string easting;
		/// <summary>
		/// Gets/Sets a list of easting to randomise between, the list is separated by | characters
		/// </summary>
		public string Easting
		{
			get { return easting; }
			set { easting = value; }
		}

		private string scale;
		/// <summary>
		/// Gets/Sets a list of scale to randomise between, the list is separated by | characters
		/// </summary>
		public string Scale
		{
			get { return scale; }
			set { scale = value; }
		}

		private string session;
		/// <summary>
		/// The session id used for a road route.
		/// </summary>
		public string Session
		{
			get { return session; }
			set { session = value; }
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
		/// Submits a Map Reference Transaction.
		/// </summary>
		public override void ExecuteTransaction()
		{
            if ((DateTime.Now.Minute - offset) % frequency == 0)
            {
                try
                {
                    string resultData = String.Empty;

                    LogStart();

                    RequestMapParams rmp = new RequestMapParams();
                    rmp.type = Type;
                    rmp.northing = Northing;
                    rmp.easting = Easting;
                    rmp.scale = Scale;
                    rmp.session = Session;

                    bool success = false;

                    Webservice.Timeout = (int)this.Timeout.TotalMilliseconds;

                    // Log the start of the call.
                    LogStart();

                    try
                    {
                        success = Webservice.RequestMap(rmp, ref resultData);
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
                    LogBadEnd(String.Format(Messages.Service_MapException, exception.Message));
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
