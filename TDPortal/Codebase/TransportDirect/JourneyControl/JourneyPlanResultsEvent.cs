// *************************************************
// NAME                 : JourneyPlanResultsEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 20/08/2003 
// DESCRIPTION  : Defines a custom event for logging
// journey plan results data.
// ************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/JourneyPlanResultsEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:50   mturner
//Initial revision.
//
//   Rev 1.4   Jun 28 2004 15:40:50   passuied
//Fix for the Event Receiver
//
//   Rev 1.3   Sep 24 2003 18:21:30   geaton
//Moved results data to the JourneyPlanResultsVerboseEvent class. Added response category.
//
//   Rev 1.2   Sep 16 2003 11:08:30   geaton
//Added extra comments following code review.
//
//   Rev 1.1   Sep 10 2003 10:47:04   geaton
//Renamed JourneyRequestId to JourneyPlanRequestId so consistent with naming of JourneyWebRequestId.
//
//   Rev 1.0   Sep 09 2003 13:37:44   RPhilpott
//Initial Revision
//
//   Rev 1.2   Sep 08 2003 16:23:50   ALole
//Changed TDJourneyResults to TDJourneyResult as per the correct class name.
//
//   Rev 1.1   Sep 08 2003 12:49:48   geaton
//Added file formatter and correct namespace for results class.
//
//   Rev 1.0   Aug 20 2003 10:41:54   geaton
//Initial Revision

using System;
using TransportDirect.Common.Logging;
using TransportDirect.ReportDataProvider.TDPCustomEvents;


namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Class for capturing Journey Plan Results data in the
	/// Journey Control TDP component.
	/// </summary>
	[Serializable]
	public class JourneyPlanResultsEvent : TDPCustomEvent
	{
		private string journeyPlanRequestId;
		private JourneyPlanResponseCategory responseCategory;

		private static JourneyPlanResultsEventFileFormatter fileFormatter = new JourneyPlanResultsEventFileFormatter();

		
		/// <summary>
		/// Constructor for a <c>JourneyPlanResultsEvent</c> class. 
		/// A <c>JourneyPlanResultsEvent</c> is used
		/// to log journey results transaction data using the Event Service.
		/// This class must be serializable to allow logging to MSMQs.
		/// </summary>
		/// <param name="sessionId">The session id used to perform the journey request that produced the results.</param>
		/// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or not (false).</param>
		/// <param name="journeyPlanRequestId">Identifier used to identify the journey request that produced the results.</param>
		/// <param name="resultsCategory">The category of response returned by the journey request.</param>
		public JourneyPlanResultsEvent(string journeyPlanRequestId,
									   JourneyPlanResponseCategory responseCategory,
									   bool userLoggedOn,
									   string sessionId): base(sessionId, userLoggedOn)
		{
			this.responseCategory = responseCategory;
			this.journeyPlanRequestId = journeyPlanRequestId;
		}

		/// <summary>
		/// Gets the journey plan request identifier.
		/// </summary>
		public string JourneyPlanRequestId
		{
			get{return journeyPlanRequestId;}
		}
		
		/// <summary>
		/// Gets the journey response category.
		/// </summary>
		public JourneyPlanResponseCategory ResponseCategory
		{
			get{return responseCategory;}
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
