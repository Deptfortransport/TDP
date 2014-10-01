// *********************************************** 
// NAME                 : FuelType.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Enumerator that represents the fuel type
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/FuelType.cs-arc  $
//
//   Rev 1.0   Aug 04 2009 14:41:16   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1
{
    /// <summary>
    /// Enumerator that represents the fuel type
    /// </summary>
    [System.Serializable]
    public enum FuelType
    {
        Petrol,     // Petrol is used in calculating the fuel consumption and fuel cost. This is the default value
        Diesel      // Diesel is used in calculating the fuel consumption and fuel cost
    }
}
