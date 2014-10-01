// *********************************************** 
// NAME			: ITDCyclePlannerResult.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/06/2008
// DESCRIPTION	: Definition of the ITDCycleRequest Interface
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/ITDCyclePlannerResult.cs-arc  $
//
//   Rev 1.2   Oct 27 2008 16:03:22   mmodi
//Removed comments
//
//   Rev 1.1   Jul 18 2008 13:37:48   mmodi
//Updates as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 15:42:02   mmodi
//Initial revision.
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    public interface ITDCyclePlannerResult
    {
        CyclePlannerMessage[] CyclePlannerMessages { get; }

        bool IsValid { get; set; }

        // Used for the request/result
        int JourneyReferenceNumber { get; set; }
        int LastReferenceSequence { get; set; }

        // Used to provide each journey an index number
        int OutwardJourneyIndex { get; set; }
        int ReturnJourneyIndex { get; set; }

        string OutwardDisplayNumber(int journeyIndex);
        string ReturnDisplayNumber(int journeyIndex);

        // Holds the CycleJourneys
        ArrayList OutwardCycleJourneys { get; }
        ArrayList ReturnCycleJourneys { get; }

        // Methods to retrieve a CycleJourney
        CycleJourney OutwardCycleJourney();
        CycleJourney ReturnCycleJourney();
        CycleJourney OutwardCycleJourney(int journeyIndex);
        CycleJourney ReturnCycleJourney(int journeyIndex);

        // Journey counts
        int OutwardCycleJourneyCount { get; }
        int ReturnCycleJourneyCount { get; }

        // Returns the index position in the journeys arraylist for the journey using its JourneyIndex
        int GetSelectedOutwardJourneyIndex(int journeyIndex);
        int GetSelectedReturnJourneyIndex(int journeyIndex);

        /// <summary>
        /// Inserts the CycleJourney to back into cycle journey array, overwriting itself.
        /// This method should only be used to inject the CycleJourney back into the Result object
        /// for a cycle journey retrieved from the Result object.
        /// If the cycle journey does not exist in the array, it is not inserted.
        /// </summary>
        void AddOutwardCycleJourney(CycleJourney cycleJourney);
        void AddReturnCycleJourney(CycleJourney cycleJourney);

        /// <summary>
        /// Tests if the end time for any outward journey exceeds the
        /// start time of any return journey
        /// </summary>
        /// <param name="journeyRequest">The original journey request</param>
        /// <returns>True if any outward journey end time exceeds the start of any
        /// return journey start time or false otherwise</returns>
        bool CheckForReturnOverlap(ITDCyclePlannerRequest journeyRequest);

        /// <summary>
        /// Tests if any of the journeys returned start in the past
        /// </summary>
        /// <param name="journeyRequest">The original journey request</param>
        /// <returns>True if any journey start time is in the past</returns>
        bool CheckForJourneyStartInPast(ITDCyclePlannerRequest journeyRequest);

        // Used to populate the journey summary pages
        JourneySummaryLine[] OutwardJourneySummary(bool arriveBefore, ModeType[] modeType);
        JourneySummaryLine[] ReturnJourneySummary(bool arriveBefore, ModeType[] modeType);

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
