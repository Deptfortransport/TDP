// *********************************************** 
// NAME             : TDPLocationTypeHelper.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 02 Apr 2011
// DESCRIPTION  	: TDPLocationTypeHelper class
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.Common.LocationService
{
    /// <summary>
    /// TDPLocationTypeHelper class
    /// </summary>
    public class TDPLocationTypeHelper
    {
        /// <summary>
        /// Parses a database location type value into an TDPLocationType
        /// </summary>
        /// <param name="cjpModeType"></param>
        public static TDPLocationType GetTDPLocationType(string dbLocationType)
        {
            string locationType = dbLocationType.ToUpper().Trim();
    
            switch (locationType)
            {
                case "AIRPORT":
                case "COACH":
                case "FERRY":
                case "RAIL STATION":
                case "TMU":
                    return TDPLocationType.Station;
                case "LOCALITY":
                    return TDPLocationType.Locality;
                case "VENUEPOI":
                    return TDPLocationType.Venue;
                case "EXCHANGE GROUP":
                    return TDPLocationType.StationGroup;
                case "POSTCODE":
                    return TDPLocationType.Postcode;
                case "POI":
                    return TDPLocationType.POI;
                default:
                    throw new TDPException(
                        string.Format("Error parsing database Location Type value into an TDPLocationType, unrecognised value[{0}]", dbLocationType),
                        false, TDPExceptionIdentifier.LSErrorParsingLocationType);
            }
        }

        /// <summary>
        /// Parses a database location type value into an TDPLocationType
        /// </summary>
        /// <param name="cjpModeType"></param>
        public static TDPLocationType GetTDPLocationTypeActual(string dbLocationType)
        {
            string locationType = dbLocationType.ToUpper().Trim();

            switch (locationType)
            {
                case "AIRPORT":
                    return TDPLocationType.StationAirport;
                case "COACH":
                    return TDPLocationType.StationCoach;
                case "FERRY":
                    return TDPLocationType.StationFerry;
                case "RAIL STATION":
                    return TDPLocationType.StationRail;
                case "TMU":
                    return TDPLocationType.StationTMU;
                case "LOCALITY":
                    return TDPLocationType.Locality;
                case "VENUEPOI":
                    return TDPLocationType.Venue;
                case "EXCHANGE GROUP":
                    return TDPLocationType.StationGroup;
                case "POSTCODE":
                    return TDPLocationType.Postcode;
                case "POI":
                    return TDPLocationType.POI;
                default:
                    throw new TDPException(
                        string.Format("Error parsing database Location Type value into an TDPLocationType, unrecognised value[{0}]", dbLocationType),
                        false, TDPExceptionIdentifier.LSErrorParsingLocationType);
            }
        }

        /// <summary>
        /// Parses a javascript "auto-suggest" location type value into an TDPLocationType
        /// </summary>
        /// <param name="cjpModeType"></param>
        public static TDPLocationType GetTDPLocationTypeJS(string jsLocationType)
        {
            // Null check as its from web
            if (jsLocationType == null)
                jsLocationType = string.Empty;

            string locationType = jsLocationType.ToUpper().Trim();

            if (!string.IsNullOrEmpty(locationType))
            {
                switch (locationType)
                {
                    case "VENUEPOI":
                        return TDPLocationType.Venue;
                    case "STATION":
                        return TDPLocationType.Station;
                    case "STATIONAIRPORT":
                    case "AIR":
                        return TDPLocationType.StationAirport;
                    case "STATIONCOACH":
                    case "COACH":
                        return TDPLocationType.StationCoach;
                    case "STATIONFERRY":
                    case "FERRY":
                        return TDPLocationType.StationFerry;
                    case "STATIONRAIL":
                    case "RAIL":
                        return TDPLocationType.StationRail;
                    case "STATIONTMU":
                    case "LIGHTRAIL":
                        return TDPLocationType.StationTMU;
                    case "STATIONGROUP":
                    case "GROUP":
                        return TDPLocationType.StationGroup;
                    case "LOCALITY":
                        return TDPLocationType.Locality;
                    case "POSTCODE":
                        return TDPLocationType.Postcode;
                    case "ADDRESS":
                        return TDPLocationType.Address;
                    case "POI":
                        return TDPLocationType.POI;
                    case "COORDINATEEN":
                        return TDPLocationType.CoordinateEN;
                    case "COORDINATELL":
                        return TDPLocationType.CoordinateLL;
                    default:
                        throw new TDPException(
                            string.Format("Error parsing javascript Location Type value into an TDPLocationType, unrecognised value[{0}]", jsLocationType),
                            false, TDPExceptionIdentifier.LSErrorParsingLocationType);
                }
            }

            return TDPLocationType.Unknown;
        }

        /// <summary>
        /// Parses a query string location type value into an TDPLocationType
        /// </summary>
        /// <param name="cjpModeType"></param>
        public static TDPLocationType GetTDPLocationTypeQS(string qsLocationType)
        {
            string locationType = qsLocationType.ToUpper().Trim();

            switch (locationType)
            {
                case "S":
                case "N": // Naptan (landing page handoff from TDP Web2)
                    return TDPLocationType.Station;
                case "LG":
                case "LC": // Locality (landing page handoff from TDP Web2)
                    return TDPLocationType.Locality;
                case "V":
                    return TDPLocationType.Venue;
                case "SG":
                    return TDPLocationType.StationGroup;
                case "P":
                    return TDPLocationType.Postcode;
                case "A":
                    return TDPLocationType.Address;
                case "PI":
                    return TDPLocationType.POI;
                case "EN":
                    return TDPLocationType.CoordinateEN;
                case "L":
                    return TDPLocationType.CoordinateLL;
                case "U":
                    return TDPLocationType.Unknown;
                default:
                    throw new TDPException(
                        string.Format("Error parsing querystring Location Type value into an TDPLocationType, unrecognised value[{0}]", qsLocationType),
                        false, TDPExceptionIdentifier.LSErrorParsingLocationType);
            }
        }

        /// <summary>
        /// Returns a query string representation of the TDPLocationType
        /// </summary>
        /// <param name="cjpModeType"></param>
        public static string GetTDPLocationTypeQS(TDPLocationType locationType)
        {
            switch (locationType)
            {
                case TDPLocationType.Station:
                case TDPLocationType.StationAirport:
                case TDPLocationType.StationCoach:
                case TDPLocationType.StationFerry:
                case TDPLocationType.StationRail:
                case TDPLocationType.StationTMU:
                    return "S";
                case TDPLocationType.Locality:
                    return "LG";
                case TDPLocationType.Venue:
                    return "V";
                case TDPLocationType.StationGroup:
                    return "SG";
                case TDPLocationType.Postcode:
                    return "P";
                case TDPLocationType.Address:
                    return "A";
                case TDPLocationType.POI:
                    return "PI";
                case TDPLocationType.CoordinateEN:
                    return "EN";
                case TDPLocationType.CoordinateLL:
                    return "L";
                default:
                    return "U";
            }
        }

        /// <summary>
        /// Method to return the TDPLocationType for a naptan string
        /// </summary>
        /// <param name="natpan">The naptan</param>
        /// <returns>TDPLocationType or TDPLocationType.Unknown if unable to match</returns>
        public static TDPLocationType GetTDPLocationTypeForNaPTAN(string naptan)
        {
            if (!string.IsNullOrEmpty(naptan))
            {
                // Fairly simple but works in majority of cases
                try
                {
                    if (naptan.StartsWith("9000")) return TDPLocationType.StationCoach;
                    if (naptan.StartsWith("9100")) return TDPLocationType.StationRail;
                    if (naptan.StartsWith("9200")) return TDPLocationType.StationAirport;
                    if (naptan.StartsWith("9300")) return TDPLocationType.StationFerry;
                    if (naptan.StartsWith("9400ZZLU")) return TDPLocationType.StationTMU;
                    if (naptan.StartsWith("9400ZZDL")) return TDPLocationType.StationTMU;
                    if (naptan.StartsWith("9400")) return TDPLocationType.StationTMU;
                }
                catch
                {
                    // Ignore exceptions
                }
            }

            return TDPLocationType.Unknown;
        }
    }
}
