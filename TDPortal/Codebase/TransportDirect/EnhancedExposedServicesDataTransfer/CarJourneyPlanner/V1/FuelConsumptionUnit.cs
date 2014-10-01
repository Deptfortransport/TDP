// *********************************************** 
// NAME                 : DistanceUnit.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Enumerator to define the fuel consumption units
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/FuelConsumptionUnit.cs-arc  $
//
//   Rev 1.0   Aug 04 2009 14:41:14   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1
{
    /// <summary>
    /// Enumerator to define the fuel consumption units
    /// </summary>
    [System.Serializable]
    public enum FuelConsumptionUnit
    {
        MilesPerGallon,     // Fuel consumption is specified in Miles per gallon. Thie is the default.
        LitresPer100Km      // Fuel consumption is specified in Litres per 100 km
    }
}
