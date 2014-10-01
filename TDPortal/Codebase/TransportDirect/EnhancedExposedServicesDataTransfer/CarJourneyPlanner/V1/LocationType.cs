// *********************************************** 
// NAME                 : LocationType.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Enumerator for the location types supported in a RequestLocation
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/LocationType.cs-arc  $
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
    /// Enumerator for the location types supported in a RequestLocation
    /// </summary>
    [System.Serializable]
    public enum LocationType
    {
        //These elements have been commented out because they will be added later
        //		Locality,
        Postcode,
        NaPTAN,
        Coordinate
    }
}
