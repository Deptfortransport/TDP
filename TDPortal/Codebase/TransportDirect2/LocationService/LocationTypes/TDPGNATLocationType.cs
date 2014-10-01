// *********************************************** 
// NAME             : TDPGNATLocationType.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 09 June 2011
// DESCRIPTION  	: Enumeration of possible GNAT location types
// ************************************************
// 

using System;

namespace TDP.Common.LocationService
{
    /// <summary>
    /// Enumeration of possible GNAT location types
    /// </summary>
    [Serializable()]
    public enum TDPGNATLocationType
    {
        Bus,
        Rail,
        Coach,
        Tram,
        Underground,
        DLR,
        Ferry,
        Air,

    }
}