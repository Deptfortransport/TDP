// ******************************************************** 
// NAME                 : JourneyPlanRequestVerboseEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 08/09/2003 
// DESCRIPTION  : Defines a custom event for logging ALL
// journey plan request data. This will be used to capture
// data used by the complaints service.
// ******************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/JourneyPlanRequestVerboseEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:48   mturner
//Initial revision.
//
//   Rev 1.2   Sep 16 2003 11:08:30   geaton
//Added extra comments following code review.
//
//   Rev 1.1   Sep 10 2003 10:47:02   geaton
//Renamed JourneyRequestId to JourneyPlanRequestId so consistent with naming of JourneyWebRequestId.
//
//   Rev 1.0   Sep 09 2003 13:37:42   RPhilpott
//Initial Revision
//
//   Rev 1.0   Sep 08 2003 12:49:04   geaton
//Initial Revision

using System;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.JourneyControl
{
	
	/// <summary>
	/// Class for capturing ALL Journey Plan Request data in the
	/// Journey Control TDP component.
	/// </summary>
	[Serializable]
	public class JourneyPlanRequestVerboseEvent : TDPCustomEvent
	{
		private string journeyPlanRequestId;
		private TDJourneyRequest journeyRequestData;

		/// <summary>
		/// Defines formatter for formatting event data for use by file publishers.
		/// Common across all instances of the JourneyPlanRequestVerboseEvent class.
		/// </summary>
		private static IEventFormatter fileFormatter = new JourneyPlanRequestVerboseEventFileFormatter();
		
		/// <summary>
		/// Constructor for a <c>JourneyPlanRequestVerboseEvent</c> class. 
		/// A <c>JourneyPlanRequestVerboseEvent</c> is used
		/// to log ALL journey request transaction data using the Event Service.
		/// This class must be serializable to allow logging to MSMQs.
		/// </summary>
		/// <param name="sessionId">The session id used to perform the journey request.</param>
		/// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or not (false).</param>
		/// <param name="journeyPlanRequestId">Identifier used to identify the journey request.</param>
		/// <param name="journeyRequestData">The journey plan data that was submitted for the journey plan request.</param>
		public JourneyPlanRequestVerboseEvent(string journeyPlanRequestId,
											  TDJourneyRequest journeyRequestData,
											  bool userLoggedOn,
											  string sessionId): base(sessionId, userLoggedOn)
		{
			this.journeyRequestData = journeyRequestData;
			this.journeyPlanRequestId = journeyPlanRequestId;
		}

		/// <summary>
		/// Gets the journey request data.
		/// </summary>
		public TDJourneyRequest JourneyRequestData
		{
			get{return journeyRequestData;}
		}

		/// <summary>
		/// Gets the journey plan request identifier.
		/// </summary>
		public string JourneyPlanRequestId
		{
			get{return journeyPlanRequestId;}
		}

		/// <summary>
		/// Provides an event formatter for publishing to files.
		/// </summary>
		override public IEventFormatter FileFormatter
		{
			get {return fileFormatter;}
		}
	}
}
