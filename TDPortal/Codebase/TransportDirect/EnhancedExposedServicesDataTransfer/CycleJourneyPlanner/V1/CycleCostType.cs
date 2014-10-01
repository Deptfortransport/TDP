// *********************************************** 
// NAME             : CycleCostType.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Enum to define the cycle cost types
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/CycleCostType.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:39:40   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// Enum to define the cycle cost types
    /// </summary>
    [Serializable]
    public enum CycleCostType
    {
        /// <summary>
        /// All other costs associated for the journey (e.g. toll charge, ferry cost)
        /// </summary>
        OtherCost
    }
}
