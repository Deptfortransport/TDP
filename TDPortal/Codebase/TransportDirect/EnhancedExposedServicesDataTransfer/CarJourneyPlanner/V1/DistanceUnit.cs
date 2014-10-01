// *********************************************** 
// NAME                 : DistanceUnit.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Enumerator to define the distance units of the car journey result
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/DistanceUnit.cs-arc  $
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
    /// Enumerator to define the distance units of the car journey result
    /// </summary>
    [System.Serializable]
    public enum DistanceUnit
    {
        Miles,  // Car journey distances will be Miles
        Kms     // Car journey distances will be Kms
    }
}
