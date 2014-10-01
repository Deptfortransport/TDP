// *********************************************** 
// NAME                 : CarAlgorithmType.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Enumerator that represents the type of car journey planning required
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/CarAlgorithmType.cs-arc  $
//
//   Rev 1.0   Aug 04 2009 14:41:08   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1
{
    /// <summary>
    /// Enumerator that represents the type of car journey planning required
    /// </summary>
    [System.Serializable]
    public enum CarAlgorithmType
    {
        Fastest,            // The Fastest car journey will be planned. This is the default value.
        Shortest,           // The Shortest distance car journey will be planned
        MostEconomical,     // The Most fuel economic car journey will be planned
        Cheapest            // The Cheapest overall car journey will be planned
    }
}
