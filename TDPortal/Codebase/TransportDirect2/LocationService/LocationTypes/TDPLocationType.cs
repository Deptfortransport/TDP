// *********************************************** 
// NAME             : TDPLocationType.cs      
// AUTHOR           : Mark Turner
// DATE CREATED     : 22 Feb 2011
// DESCRIPTION  	: Enumeration of possible location types
// ************************************************
// 

using System;

namespace TDP.Common.LocationService
{
    [Serializable()]
    public enum TDPLocationType
    {
        Venue,
        Station,
        StationAirport,
        StationCoach,
        StationFerry,
        StationRail,
        StationTMU,
        StationGroup,
        Locality,
        Postcode,
        Address,
        POI,
        CoordinateEN, // Easting Northing
        CoordinateLL, // Latitude Longitude
        Unknown
    }
}
