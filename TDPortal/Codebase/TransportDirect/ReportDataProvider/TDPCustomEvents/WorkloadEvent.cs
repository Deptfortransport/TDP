// *********************************************** 
// NAME                 : WorkloadEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 18/08/2003 
// DESCRIPTION  : Defines a custom event for logging
// workload data.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/WorkloadEvent.cs-arc  $
//
//   Rev 1.1   Apr 29 2008 16:37:54   mturner
//Fixes to allow the web log reader to process host names from IIS logs.  This is to resolve IR4904/USD2517876
//
//   Rev 1.0   Nov 08 2007 12:39:38   mturner
//Initial revision.
//
//   Rev 1.6   Apr 19 2004 20:31:08   geaton
//IR785
//
//   Rev 1.5   Oct 07 2003 11:57:44   PScott
//workload event now passed datetime instead of ints
//
//   Rev 1.4   Sep 16 2003 16:35:42   ALole
//Updated WorkloadEvent - removed URIStem and BytesSent fields
//
//   Rev 1.3   Aug 28 2003 12:07:34   ALole
//Changed FileFormatter method to use WorkloadEventFileFormatter
//
//   Rev 1.2   Aug 21 2003 11:52:18   geaton
//Removed milliseconds constructor parameter since IIS web logs have lowest resolution of 1 second.
//
//   Rev 1.1   Aug 20 2003 10:38:44   geaton
//Added data fields
//
//   Rev 1.0   Aug 18 2003 19:11:40   geaton
//Initial Revision

using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	[Serializable]
	public class WorkloadEvent : TDPCustomEvent
	{
		private DateTime requested;
		private int numberRequested;
        private int partnerId;

		private static WorkloadEventFileFormatter fileFormatter = new WorkloadEventFileFormatter();

		/// <summary>
		/// Constructor for a WorkloadEvent class. A WorkloadEvent class is used
		/// to capture web page request data.
		/// This class must be serializable to allow logging to MSMQs.
		/// </summary>
		/// <remarks>
		/// A workload event is used to capture the number of one or more web page
		/// requests in a given time frame. Note that earlier versions of this
		/// class only allowed capture of a single web page request. A decision was made
		/// to aggregate requests to improve performance.
		/// </remarks>
		/// <param name="timeRequested">DateTime at which the web page request were made.</param>
		/// <param name="numberRequested">The number of web page requests that were made in the give <c>timeRequested</c>.</param>
		public WorkloadEvent(DateTime timeRequested, int numberRequested): base(String.Empty, false)
		{
			this.requested = timeRequested;
			this.numberRequested = numberRequested;
		}

        /// <summary>
        /// Constructor for a WorkloadEvent class. A WorkloadEvent class is used
        /// to capture web page request data.
        /// This class must be serializable to allow logging to MSMQs.
        /// </summary>
        /// <remarks>
        /// A workload event is used to capture the number of one or more web page
        /// requests in a given time frame. Note that earlier versions of this
        /// class only allowed capture of a single web page request. A decision was made
        /// to aggregate requests to improve performance.
        /// </remarks>
        /// <param name="timeRequested">DateTime at which the web page request were made.</param>
        /// <param name="numberRequested">The number of web page requests that were made in the give <c>timeRequested</c>.</param>
        /// <param name="partnerId">Id of the partner site</param>
        public WorkloadEvent(DateTime timeRequested, int numberRequested, int partnerId): base(String.Empty, false)
        {
            this.requested = timeRequested;
            this.numberRequested = numberRequested;
            this.partnerId = partnerId;
        }

		/// <summary>
		/// Gets the date/time at which the request for the resource was made.
		/// </summary>
		public DateTime Requested
		{
			get{return requested;}
		}

		/// <summary>
		/// Gets the number of web pages requested at the time stored in <c>Requested</c>.
		/// </summary>
		public int NumberRequested
		{
			get{return numberRequested;}
		}

        /// <summary>
        /// read only property: Id of the partner site
        /// </summary>
        public int PartnerId
        {
            get { return partnerId; }
        }

		/// <summary>
		/// Provides an event formatter for publishing the event using a file publisher.
		/// </summary>
		override public IEventFormatter FileFormatter
		{
			get {return fileFormatter;}
		}

	}
}
