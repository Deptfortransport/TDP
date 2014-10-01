// *********************************************** 
// NAME                 : Messages.cs 
// AUTHOR               : David Lane 
// DATE CREATED         : 28/02/2012 
// DESCRIPTION			: Error / logging messages.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/BatchJourneyPlannerService/Messages.cs-arc  $
//
//   Rev 1.2   Feb 28 2012 15:52:28   dlane
//Batch updates and code commentry
//Resolution for 5787: Batch Journey Planner
//

using System;
using System.Collections.Generic;
using System.Text;

namespace BatchJourneyPlannerService
{
    public class Messages
    {
        // Message strings
        public const string Init_Failed = "BatchJourneyPlannerService initialisation failed with exception: {0}";
        public const string Init_Completed = "Initialisation of BatchJourneyPlannerService completed successfully.";
        public const string Init_PropertyServiceFailed = "Failed to initialise the TD Property Service: [{0}]";
        public const string Init_CryptographicServiceFailed = "Failed to initialise the TD Cryptographic Service: [{0}]";
        public const string Init_InvalidPropertyKeys = "BatchJourneyPlannerService properties failed validation: {0}";
        public const string Service_FailedRunning = "Failure when running the batch processor class: [{0}]";
        public const string Service_Disabled = "Failure when running the batch processor class: batch processing is disabled in properties.";
        public const string CjpCall_Errored = "Failure when calling CJP: {0}";
    }
}
