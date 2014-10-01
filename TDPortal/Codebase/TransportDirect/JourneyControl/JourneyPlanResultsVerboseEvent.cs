// *************************************************
// NAME                 : JourneyPlanResultsVerboseEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 20/08/2003 
// DESCRIPTION  : Defines a custom event for logging
// verbose journey plan results data.
// ************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/JourneyPlanResultsVerboseEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:50   mturner
//Initial revision.
//
//   Rev 1.0   Sep 24 2003 18:21:44   geaton
//Initial Revision

using System;
using TransportDirect.Common.Logging;
using TransportDirect.ReportDataProvider.TDPCustomEvents;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Class for capturing verbose Journey Plan Results data in the
	/// Journey Control TDP component.
	/// </summary>
	[Serializable]
	public class JourneyPlanResultsVerboseEvent : TDPCustomEvent
	{
		private string journeyPlanRequestId;
		private TDJourneyResult journeyResultsData;

		/// <summary>
		/// Defines formatter for formatting event data for use by file publishers.
		/// Common across all instances of the JourneyPlanResultsVerboseEvent class.
		/// </summary>
		private static IEventFormatter fileFormatter = new JourneyPlanResultsVerboseEventFileFormatter();
		
		/// <summary>
		/// Constructor for a <c>JourneyPlanResultsVerboseEvent</c> class. 
		/// A <c>JourneyPlanResultsVerboseEvent</c> is used
		/// to log journey results transaction data using the Event Service.
		/// This class must be serializable to allow logging to MSMQs.
		/// </summary>
		/// <param name="sessionId">The session id used to perform the journey request that produced the results.</param>
		/// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or not (false).</param>
		/// <param name="journeyPlanRequestId">Identifier used to identify the journey request that produced the results.</param>
		/// <param name="journeyResultsData">The journey results data that was returned for the journey plan request.</param>
		public JourneyPlanResultsVerboseEvent(string journeyPlanRequestId,
											  TDJourneyResult journeyResultsData,
											  bool userLoggedOn,
											  string sessionId): base(sessionId, userLoggedOn)
		{
			this.journeyResultsData = journeyResultsData;
			this.journeyPlanRequestId = journeyPlanRequestId;
		}

		/// <summary>
		/// Gets the journey results data
		/// </summary>
		public TDJourneyResult JourneyResultsData
		{
			get{return journeyResultsData;}
		}

		/// <summary>
		/// Gets the journey plan request identifier
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

