// *********************************************** 
// NAME                 : JourneyPlanRequestEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 18/08/2003 
// DESCRIPTION  : Defines a custom event for logging
// journey plan request data.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/JourneyPlanRequestEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:48   mturner
//Initial revision.
//
//   Rev 1.3   Jun 28 2004 15:40:50   passuied
//Fix for the Event Receiver
//
//   Rev 1.2   Sep 16 2003 11:08:32   geaton
//Added extra comments following code review.
//
//   Rev 1.1   Sep 10 2003 10:47:06   geaton
//Renamed JourneyRequestId to JourneyPlanRequestId so consistent with naming of JourneyWebRequestId.
//
//   Rev 1.0   Sep 09 2003 13:37:40   RPhilpott
//Initial Revision
//
//   Rev 1.1   Sep 08 2003 12:50:36   geaton
//Changed properties to include only data needed for MIS reports.
//
//   Rev 1.0   Aug 20 2003 10:41:52   geaton
//Initial Revision

using System;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.Logging;


namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Class for capturing Journey Plan Request data in the
	/// Journey Control TDP component.
	/// </summary>
	[Serializable]
	public class JourneyPlanRequestEvent : TDPCustomEvent
	{
		private string journeyPlanRequestId;
		private ModeType[] modes;

		private static JourneyPlanRequestEventFileFormatter fileFormatter = new JourneyPlanRequestEventFileFormatter();

		
		/// <summary>
		/// Constructor for a <c>JourneyPlanRequestEvent</c> class. 
		/// A <c>JourneyPlanRequestEvent</c> is used
		/// to log journey request transaction data using the Event Service.
		/// This class must be serializable to allow logging to MSMQs.
		/// </summary>
		/// <param name="sessionId">The session id used to perform the journey request.</param>
		/// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or not (false).</param>
		/// <param name="journeyPlanRequestId">Identifier used to identify the journey request.</param>
		/// <param name="modes">The modes of transport specified in the journey plan request.</param>
		public JourneyPlanRequestEvent(string journeyPlanRequestId,
									   ModeType[] modes,
									   bool userLoggedOn,
									   string sessionId): base(sessionId, userLoggedOn)
		{
			this.modes = modes;
			this.journeyPlanRequestId = journeyPlanRequestId;
		}

		/// <summary>
		/// Gets the journey request modes data.
		/// </summary>
		public ModeType[] Modes
		{
			get{return modes;}
		}

		/// <summary>
		/// Gets the journey plna request identifier.
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
