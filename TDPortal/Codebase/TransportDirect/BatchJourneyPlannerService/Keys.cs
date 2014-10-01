// *********************************************** 
// NAME                 : Keys.cs 
// AUTHOR               : David Lane 
// DATE CREATED         : 28/02/2012 
// DESCRIPTION			: Property keys.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/BatchJourneyPlannerService/Keys.cs-arc  $
//
//   Rev 1.2   Feb 28 2012 15:52:26   DLane
//Batch updates and code commentry
//Resolution for 5787: Batch Journey Planner
//

using System;
using System.Collections.Generic;

namespace BatchJourneyPlannerService
{
    public class Keys
    {
        // Property keys
        public const string BatchProcessingServiceEnabled = "BatchProcessingServiceEnabled";
        public const string BatchProcessingPeakStartTime = "BatchProcessingPeakStartTime";
        public const string BatchProcessingPeakEndTime = "BatchProcessingPeakEndTime";
        public const string BatchProcessingInWindowIntervalSeconds = "BatchProcessingInWindowIntervalSeconds";
        public const string BatchProcessingOutWindowIntervalSeconds = "BatchProcessingOutWindowIntervalSeconds";
        public const string BatchProcessingInWindowConcurrentRequests = "BatchProcessingInWindowConcurrentRequests";
        public const string BatchProcessingOutWindowConcurrentRequests = "BatchProcessingOutWindowConcurrentRequests";
        public const string BatchProcessingWindowCount = "BatchProcessingWindowCount";
        public const string BatchProcessingWindowNStart = "BatchProcessingWindow{0}Start";
        public const string BatchProcessingWindowNEnd = "BatchProcessingWindow{0}End";
        public const string BatchProcessingRunningDays = "BatchProcessingRunningDays";
    }
}
