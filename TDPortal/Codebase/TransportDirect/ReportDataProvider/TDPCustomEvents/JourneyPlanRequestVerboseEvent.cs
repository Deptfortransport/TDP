// ******************************************************** 
// NAME                 : JourneyPlanRequestVerboseEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 08/09/2003 
// DESCRIPTION  : Defines a custom event for logging ALL
// journey plan request data. This will be used to capture
// data used by the complaints service.
// ******************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/JourneyPlanRequestVerboseEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:22   mturner
//Initial revision.
//
//   Rev 1.2   Sep 09 2003 11:43:12   passuied
//commented out using JourneyControl
//
//   Rev 1.1   Sep 09 2003 11:18:38   geaton
//Flagged for moving into another project to prevent circular dependency.
//
//   Rev 1.0   Sep 08 2003 12:49:04   geaton
//Initial Revision

using System;
//using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
/*
TO BE MOVED	
	[Serializable]
	public class JourneyPlanRequestVerboseEvent : TDPCustomEvent
	{
		private string journeyRequestId;
		private ITDJourneyRequest journeyRequestData;
		private static IEventFormatter fileFormatter = new JourneyPlanRequestVerboseEventFileFormatter();
		
		/// <summary>
		/// Constructor for a <c>JourneyPlanRequestVerboseEvent</c> class. 
		/// A <c>JourneyPlanRequestVerboseEvent</c> is used
		/// to log ALL journey request transaction data using the Event Service.
		/// This class must be serializable to allow logging to MSMQs.
		/// </summary>
		/// <param name="sessionId">The session id used to perform the journey request.</param>
		/// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or not (false).</param>
		/// <param name="journeyRequestId">Identifier used to identify the journey request.</param>
		/// <param name="journeyRequestData">The journey plan data that was submitted for the journey plan request.</param>
		public JourneyPlanRequestVerboseEvent(string journeyRequestId,
											  ITDJourneyRequest journeyRequestData,
											  bool userLoggedOn,
											  string sessionId): base(sessionId, userLoggedOn)
		{
			this.journeyRequestData = journeyRequestData;
			this.journeyRequestId = journeyRequestId;
		}

		/// <summary>
		/// Gets the journey request data
		/// </summary>
		public ITDJourneyRequest JourneyRequestData
		{
			get{return journeyRequestData;}
		}

		/// <summary>
		/// Gets the journey request identifier
		/// </summary>
		public string JourneyRequestId
		{
			get{return journeyRequestId;}
		}

		/// <summary>
		/// Provides an event formatter for publishing to files.
		/// </summary>
		override public IEventFormatter FileFormatter
		{
			get {return fileFormatter;}
		}
	}
*/
}
