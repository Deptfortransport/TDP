// *********************************************** 
// NAME                 : CarSizeType.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Enumerator that represents the car size type
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/CarSizeType.cs-arc  $
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
    /// Enumerator that represents the car size type
    /// </summary>
    [System.Serializable]
    public enum CarSizeType
    {
        Small,      // Small car size type is used in calculating the fuel consumption value
        Medium,     // Medium car size type is used in calculating the fuel consumption value. This is the default.
        Large       // Large car size type is used in calculating the fuel consumption value
    }
}
