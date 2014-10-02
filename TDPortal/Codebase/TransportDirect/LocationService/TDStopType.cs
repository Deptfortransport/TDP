// *********************************************** 
// NAME                 : TDStopType.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 10/09/2009
// DESCRIPTION  : Enumeration type for stop types for stop information, "auto-suggest" locations cache,
//              : and accessible locations cache
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/TDStopType.cs-arc  $
//
//   Rev 1.4   Jan 09 2013 11:42:18   mmodi
//Update for Stop naptan parser method
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.3   Dec 05 2012 14:10:50   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.2   Aug 28 2012 10:19:56   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.1   Sep 14 2009 15:28:08   apatel
//Stop Types enumeration used in Stop Information pages
//Resolution for 5315: CCN526 Stop Information Page Landing

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.LocationService
{
    /// <summary>
    /// Enumeration type for stop types for stop information, "auto-suggest" locations cache,
    /// and accessible locations cache
    /// </summary>
    public enum TDStopType
    {
        Unknown,
        Air,
        Bus,
        Coach,
        Ferry,
        LightRail, // TMU: "auto-suggest", Tram: accessible
        Rail,
        Underground, // Used for accessible location
        DLR, // Used for accessible location
        Taxi,
        Group, // Used for "auto-suggest" location
        Locality, // Used for "auto-suggest" location
        POI // Point of interest
    }

    /// <summary>
    /// Static helper class for TDStopType
    /// </summary>
    public static class TDStopTypeHelper
    {
        /// <summary>
        /// Method to return the TDStopType for a location type string
        /// </summary>
        /// <param name="type">Location type string in database or auto-suggest location type</param>
        /// <returns>Location type enum value</returns>
        public static TDStopType GetTDStopType(string type)
        {
            if (!string.IsNullOrEmpty(type))
            {
                // Try an enum parse
                try
                {
                    TDStopType tdStopType = (TDStopType)Enum.Parse(typeof(TDStopType), type, true);
                    return tdStopType;
                }
                catch
                {
                    // Ignore exceptions, could be a location type from the database
                    switch (type.Trim().ToUpper().Replace(" ", string.Empty))
                    {
                        case ("AIRPORT"): return TDStopType.Air;
                        case ("COACH"): return TDStopType.Coach;
                        case ("EXCHANGEGROUP"): return TDStopType.Group;
                        case ("FERRY"): return TDStopType.Ferry;
                        case ("LOCALITY"): return TDStopType.Locality;
                        case ("RAILSTATION"): return TDStopType.Rail;
                        case ("TMU"): return TDStopType.LightRail;
                        case ("POI"): return TDStopType.POI;
                        
                        case ("TRAM"): return TDStopType.LightRail; // Used for accessible location
                    }
                }
            }

            return TDStopType.Unknown;
        }

        /// <summary>
        /// Method to return the TDStopType for a naptan string
        /// </summary>
        /// <param name="natpan">The naptan</param>
        /// <returns>TDStopType or TDStopType.Unknown if unable to match</returns>
        public static TDStopType GetTDStopTypeForNaPTAN(string naptan)
        {
            if (!string.IsNullOrEmpty(naptan))
            {
                // Fairly simple but works in majority of cases
                try
                {
                    if (naptan.StartsWith("9000")) return TDStopType.Coach;
                    if (naptan.StartsWith("9100")) return TDStopType.Rail;
                    if (naptan.StartsWith("9200")) return TDStopType.Air;
                    if (naptan.StartsWith("9300")) return TDStopType.Ferry;
                    if (naptan.StartsWith("9400ZZLU")) return TDStopType.Underground;
                    if (naptan.StartsWith("9400ZZDL")) return TDStopType.DLR;
                    if (naptan.StartsWith("9400")) return TDStopType.LightRail;
                }
                catch
                {
                    // Ignore exceptions
                }
            }

            return TDStopType.Unknown;
        }
    }
}
