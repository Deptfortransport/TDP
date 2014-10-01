// *********************************************** 
// NAME			: ITDJourneyResult.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Definition of the ITDJourneyResult interface
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/ITDJourneyResult.cs-arc  $
//
//   Rev 1.6   Sep 06 2012 11:23:16   DLane
//Cycle walk links - reducing GIS calls
//Resolution for 5827: CCN667 Cycle Walk links
//
//   Rev 1.5   Sep 06 2011 11:20:28   apatel
//Updated for Real Time Information for Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.4   Sep 01 2011 10:43:18   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.3   Feb 25 2010 14:58:42   mmodi
//Added flag to ignore Transfer legs when generating orign and destination names on SummaryLines for the journeys
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Mar 10 2008 15:17:46   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:23:42   mturner
//Initial revision.
//
//   Rev 1.25   Oct 16 2007 13:45:58   mmodi
//Added fare request ID increment property
//Resolution for 4448: Logging: Request ID is not included when Coach fares request made
//
//   Rev 1.24   Jan 04 2007 13:53:08   mmodi
//Added UpdateRoadJourney
//Resolution for 4308: CO2: Find detailed journey costs should replan journey
//
//   Rev 1.23   Dec 06 2005 18:34:32   pcross
//Added new methods:
//
//GetSelectedOutwardJourneyIndex
//GetSelectedReturnJourneyIndex
//
//which get the index of the default journey where the index tracks chronological journeys from the default journey selection from the index that tracks journeys in order added to result set
//Resolution for 3263: Visit Planner: Selecting Earlier or Later leaves the Journey selected different to the Details displayed
//
//   Rev 1.22   Nov 10 2005 10:06:40   jbroome
//Exposed OutwardJourneyIndex as public read/write property
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.21   Nov 01 2005 15:12:58   build
//Automatically merged from branch for stream2638
//
//   Rev 1.20.1.0   Sep 21 2005 10:50:00   asinclair
//New branch for 2638 with Del 7.1
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.20   Aug 19 2005 14:03:50   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.19.1.0   Jul 27 2005 10:30:46   asinclair
//Added ErrorType to AddMessageToArray
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.19   May 05 2005 16:48:12   jbroome
//Made JourneyReferenceNumber a writable property
//Resolution for 2414: Coach Find A fare: Selecting next day then one fare causes out of bound exception
//
//   Rev 1.18   Mar 22 2005 11:10:56   jbroome
//Added OutwardPublicJourneys and ReturnPublicJourneys ArrayList properties.
//
//   Rev 1.17   Sep 17 2004 15:13:00   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.16   Jun 29 2004 17:11:14   jmorrissey
//Added CheckForJourneyStartInPast method
//
//   Rev 1.15   Jun 15 2004 14:42:48   COwczarek
//Add new method to check for overlapping outward/return journey times.
//Resolution for 867: Add extend journey functionality to summary and details pages
//
//   Rev 1.14   Jun 10 2004 17:13:56   RHopkins
//Added methods OutwardDisplayNumber and ReturnDisplayNumber
//
//   Rev 1.13   Nov 24 2003 15:06:18   kcheung
//Added the addmessage method
//
//   Rev 1.12   Nov 06 2003 17:02:58   kcheung
//Updated so that error messages are cleared once they have been displayed.
//
//   Rev 1.11   Sep 23 2003 14:06:26   RPhilpott
//Changes to TDJourneyResult (ctor and referenceNumber)
//
//   Rev 1.10   Sep 18 2003 13:25:04   RPhilpott
//Add IsValid flag
//
//   Rev 1.9   Sep 10 2003 14:39:40   RPhilpott
//Still work in progress ... 
//
//   Rev 1.8   Sep 09 2003 16:01:50   RPhilpott
//Add journey counts to result interface.
//
//   Rev 1.7   Sep 02 2003 12:43:24   kcheung
//Updated OutwardRoadJourney and ReturnRoadJourney
//
//   Rev 1.6   Aug 28 2003 17:58:00   kcheung
//Updated property to return Journey - does not return array anymore - but just single journey given a journeyIndex as specified in design doc
//
//   Rev 1.5   Aug 26 2003 17:18:56   PNorell
//Changed sequence number from string to int.
//
//   Rev 1.4   Aug 26 2003 11:21:14   kcheung
//Updated OutwardJourneyResult and ReturnJourneyResult header
//
//   Rev 1.3   Aug 20 2003 17:55:44   AToner
//Work in progress
using System;
using System.Collections;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for ITDJourneyResult.
	/// </summary>
	public interface ITDJourneyResult
	{
		CJPMessage[] CJPMessages{ get; }

		bool IsValid{ get; set; }

		int JourneyReferenceNumber{ get; set; }
		int LastReferenceSequence{ get; set; }
		int LastFareRequestNumber{ get; set; }
		int OutwardJourneyIndex { get; set; }

		int OutwardPublicJourneyCount { get; }
		int ReturnPublicJourneyCount { get; }

		int OutwardRoadJourneyCount { get; }
		int ReturnRoadJourneyCount { get; }

		string OutwardDisplayNumber(int journeyIndex);
		string ReturnDisplayNumber(int journeyIndex);

		/// <summary>
		/// Tests if the end time for any outward journey (public or private) exceeds the
		/// start time of any return journey (public or private)
		/// </summary>
		/// <param name="journeyRequest">The original journey request</param>
		/// <returns>True if any outward journey end time exceeds the start of any
		/// return journey start time or false otherwise</returns>
		bool CheckForReturnOverlap(ITDJourneyRequest journeyRequest);

		/// <summary>
		/// Tests if any of the journeys returned start in the past
		/// </summary>
		/// <param name="journeyRequest">The original journey request</param>
		/// <returns>True if any journey start time is in the past</returns>
		bool CheckForJourneyStartInPast(ITDJourneyRequest journeyRequest);

        bool CycleAlternativeCheckDone { get; set; }
        bool CycleAlternativeAvailable { get; set; }

        JourneySummaryLine[] OutwardJourneySummary(bool arriveBefore);
        JourneySummaryLine[] OutwardJourneySummary(bool arriveBefore, ModeType[] modeType);
        JourneySummaryLine[] OutwardJourneySummary(bool arriveBefore, ModeType[] modeType, bool ignoreStartLegTransferMode, bool ignoreEndLegTransferMode);
        JourneySummaryLine[] ReturnJourneySummary(bool arriveBefore);
        JourneySummaryLine[] ReturnJourneySummary(bool arriveBefore, ModeType[] modeType);
        JourneySummaryLine[] ReturnJourneySummary(bool arriveBefore, ModeType[] modeType,  bool ignoreStartLegTransferMode, bool ignoreEndLegTransferMode);

		PublicJourney OutwardPublicJourney(int journeyIndex);
		PublicJourney ReturnPublicJourney(int journeyIndex);

		int GetSelectedOutwardJourneyIndex(int journeyIndex);
		int GetSelectedReturnJourneyIndex(int journeyIndex);

		ArrayList OutwardPublicJourneys { get; }
		ArrayList ReturnPublicJourneys { get; }

		RoadJourney OutwardRoadJourney();
		RoadJourney ReturnRoadJourney();

		void UpdateOutwardRoadJourney(RoadJourney roadJourney);
		void UpdateReturnRoadJourney(RoadJourney roadJourney);

		PublicJourney AmendedOutwardPublicJourney{ get; set; }
		PublicJourney AmendedReturnPublicJourney{ get; set; }

		void AddPublicJourney(PublicJourney publicJourney, bool outward);
		void AddPublicJourney(PublicJourney publicJourney, bool outward, int index);

        /// <summary>
        /// Add a PublicJourney to a TDResult.  The outward flag determines where the information will
        /// be added.
        /// </summary>
        /// <param name="PublicJourney">The input PublicJouney</param>
        /// <param name="outward">Should this go into the outward or return arrays</param>
        void AddPublicJourney(PublicJourney publicJourney, bool outward, bool retainJourneyDate, bool retainJourneyDuration);
        
        /// <summary>
        /// Add a RoadJourney to a TDResult.  The outward flag determines where the information will
        /// be added.
        /// </summary>
        /// <param name="roadJourney"></param>
        /// <param name="outward"></param>
        void AddRoadJourney(RoadJourney roadJourney, bool outward, bool newRouteNumber);

		
		/// <summary>
		/// Clears the current list of messages.
		/// </summary>
		void ClearMessages();

		/// <summary>
		/// Adds a new message to the current message array.
		/// </summary>
		void AddMessageToArray(string description, string resourceId, int majorCode, int minorCode);

		/// <summary>
		/// Adds a new message to the current message array.
		/// </summary>
		void AddMessageToArray(string description, string resourceId, int majorCode, int minorCode, ErrorsType type);
	}
}