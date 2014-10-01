// *********************************************** 
// NAME             : CycleAlgorithm.cs       
// AUTHOR           : Amit Patel
// DATE CREATED     : 21 Sep 2010
// DESCRIPTION  	: Represents Cycle Algorithm (Penalty Function) available in CTPES
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/CycleAlgorithm.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:39:34   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// CycleAlgorithm specifies penalty function use to affect the cycle journey e.g. Quickest journey,
    /// Quietest journey
    /// </summary>
    [Serializable]
    public class CycleAlgorithm
    {
        private string algorithmDescription;
        private string algorithmDLL;
        private string algorithmPenaltyFunction;
        private string algorithmCall;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CycleAlgorithm()
        {
        }

        /// <summary>
        /// Specifies the descriptive name of the algorithm (e.g. Quietest)
        /// </summary>
        public string AlgorithmDescription
        {
            get { return algorithmDescription; }
            set { algorithmDescription = value; }
        }

        /// <summary>
        /// Specifies the name of the DLL containing the algorithms (penalty functions)
        /// to be used by the cycle planner (e.g. CyclePenaltyFunctions.dll)
        /// </summary>
        public string AlgorithmDLL
        {
            get { return algorithmDLL; }
            set { algorithmDLL = value; }
        }

        /// <summary>
        /// Specifies the algorithm (penalty function) name to call contained within the DLL
        /// e.g. QuietestV1
        /// </summary>
        public string AlgorithmPenaltyFunction
        {
            get { return algorithmPenaltyFunction; }
            set { algorithmPenaltyFunction = value; }
        }

        /// <summary>
        /// Represents the algorithm string to be passed in to the CycleParameters.Algorithm property
        /// e.g. "Call CyclePenaltyFunctions.dll, QuietestV1"
        /// </summary>
        public string AlgorithmCall
        {
            get { return algorithmCall; }
            set { algorithmCall = value; }
        }
    }

}
