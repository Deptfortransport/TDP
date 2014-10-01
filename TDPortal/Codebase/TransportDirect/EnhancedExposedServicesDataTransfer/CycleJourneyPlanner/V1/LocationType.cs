// *********************************************** 
// NAME             : LocationType.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Enumerator for the location types supported in a RequestLocation
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/LocationType.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:39:54   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// Enumerator for the location types supported in a RequestLocation
    /// </summary>
    [Serializable]
    public enum LocationType
    {
        Coordinate,
        NaPTAN,
        Postcode
    }
}
