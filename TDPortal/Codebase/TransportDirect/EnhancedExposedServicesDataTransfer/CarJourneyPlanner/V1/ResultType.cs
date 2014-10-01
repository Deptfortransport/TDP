// *********************************************** 
// NAME                 : ResultType.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Enum to define the car journey result output types, whether in summary format or full details format
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/ResultType.cs-arc  $
//
//   Rev 1.0   Aug 04 2009 14:41:18   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1
{
    /// <summary>
    /// Enum to define the car journey result output types, whether in summary format or
    /// full details format
    /// </summary>
    [System.Serializable]
    public enum ResultType
    {
        Summary,    // Returns only a summary format of the planned car journey(s)
        Detailed    // Returns the full detailed format of the planned car journey(s)
    }
}
