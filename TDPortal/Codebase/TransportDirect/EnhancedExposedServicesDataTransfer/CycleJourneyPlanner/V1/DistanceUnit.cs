// *********************************************** 
// NAME             : DistanceUnit.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Enumerator to define the distance units of the cycle journey result
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/DistanceUnit.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:39:52   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// Enumerator to define the distance units of the cycle journey result
    /// </summary>
    [Serializable]
    public enum DistanceUnit
    {
        /// <summary>
        /// Cycle journey distances will be in Miles
        /// </summary>
        Miles,
        /// <summary>
        /// Cycle journey distances will be in Kms
        /// </summary>
        Kms
    }
}
