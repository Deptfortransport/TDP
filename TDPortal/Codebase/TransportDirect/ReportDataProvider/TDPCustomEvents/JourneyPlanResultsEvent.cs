// *************************************************
// NAME                 : JourneyPlanResultsEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 20/08/2003 
// DESCRIPTION  : Defines a custom event for logging
// journey plan results data.
// ************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/JourneyPlanResultsEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:24   mturner
//Initial revision.
//
//   Rev 1.4   Sep 09 2003 11:43:10   passuied
//commented out using JourneyControl
//
//   Rev 1.3   Sep 09 2003 11:18:34   geaton
//Flagged for moving into another project to prevent circular dependency.
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
//using TransportDirect.UserPortal.JourneyControl;


namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
/*
TO BE MOVED:	
	[Serializable]
	public class JourneyPlanResultsEvent : TDPCustomEvent
	{
		private string journeyRequestId;
		private ITDJourneyResult journeyResultsData;
		private static IEventFormatter fileFormatter = new JourneyPlanResultsEventFileFormatter();
		
		/// <summary>
		/// Constructor for a <c>JourneyPlanResultsEvent</c> class. 
		/// A <c>JourneyPlanResultsEvent</c> is used
		/// to log journey results transaction data using the Event Service.
		/// This class must be serializable to allow logging to MSMQs.
		/// </summary>
		/// <param name="sessionId">The session id used to perform the journey request that produced the results.</param>
		/// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or not (false).</param>
		/// <param name="journeyRequestId">Identifier used to identify the journey request that produced the results.</param>
		/// <param name="journeyResultsData">The journey results data that was returned for the journey plan request.</param>
		public JourneyPlanResultsEvent(string journeyRequestId,
									   ITDJourneyResult journeyResultsData,
									   bool userLoggedOn,
									   string sessionId): base(sessionId, userLoggedOn)
		{
			this.journeyResultsData = journeyResultsData;
			this.journeyRequestId = journeyRequestId;
		}

		/// <summary>
		/// Gets the journey results data
		/// </summary>
		public ITDJourneyResult JourneyResultsData
		{
			get{return journeyResultsData;}
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
