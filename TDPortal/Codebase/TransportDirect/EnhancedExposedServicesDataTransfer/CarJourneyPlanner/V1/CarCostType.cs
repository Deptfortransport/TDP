// *********************************************** 
// NAME                 : CarCostType.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Enum to define the car cost types
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/CarCostType.cs-arc  $
//
//   Rev 1.0   Aug 04 2009 14:41:10   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1
{
    /// <summary>
    /// Enum to define the car cost types
    /// </summary>
    [System.Serializable]
    public enum CarCostType
    {
        TotalCost,
        TotalOtherCosts,
        FuelCost,
        RunningCost,
        OtherCost
    }
}
