// ************************************************ 
// NAME                 : JSLocationType.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 06/08/2012
// DESCRIPTION          : Represents the location type used in javascript generation
// ************************************************* 

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.LocationJsGenerator
{
    /// <summary>
    /// Represents the location type used in javascript generation
    /// </summary>
    public enum JSLocationType
    {
        // Same as TDStopType
        Unknown,
        Air,
        Bus,
        Coach,
        Ferry,
        LightRail,
        Rail,
        Taxi,
        Group,
        Locality,
        POI
    }

    /// <summary>
    /// Static helper class for JSLocationType
    /// </summary>
    public static class JSLocationTypeHelper
    {
        /// <summary>
        /// Method to determine the type of location
        /// </summary>
        /// <param name="type">Location type string in database</param>
        /// <returns>Location type enum value</returns>
        public static JSLocationType GetLocationType(string type)
        {
            if (!string.IsNullOrEmpty(type))
            {
                switch (type.Trim().ToUpper().Replace(" ", string.Empty))
                {
                    case ("AIRPORT"): return JSLocationType.Air;
                    case ("COACH"): return JSLocationType.Coach;
                    case ("EXCHANGEGROUP"): return JSLocationType.Group;
                    case ("FERRY"): return JSLocationType.Ferry;
                    case ("LOCALITY"): return JSLocationType.Locality;
                    case ("RAILSTATION"): return JSLocationType.Rail;
                    case ("TMU"): return JSLocationType.LightRail;
                    case ("POI"): return JSLocationType.POI;
                }
            }

            return JSLocationType.Unknown;
        }
    }
}
