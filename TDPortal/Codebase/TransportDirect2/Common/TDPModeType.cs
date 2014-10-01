// *********************************************** 
// NAME             : TDPModeType.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 23 Mar 2011
// DESCRIPTION  	: Enumeration for TDPModeType
// ************************************************
// 
                
using System;

namespace TDP.Common
{
    /// <summary>
    /// TDPModeType enum
    /// </summary>
    [Serializable()]
    public enum TDPModeType
    {
        Air = 0,
        Bus = 1,
        Car = 2,
        Coach = 3,
        Cycle = 4,
        Drt = 5,
        Ferry = 6,
        Metro = 7,
        Rail = 8,
        RailReplacementBus = 9,
        Taxi = 10,
        Telecabine = 11,
        Tram = 12,
        Underground = 13,
        Walk = 14,
        CheckIn = 15,
        CheckOut = 16,
        Transfer = 17,
        Unknown = 18,
        // Modes for internal use
        TransitShuttleBus = 18,
        TransitRail = 19,
        Queue = 20,
        WalkInterchange = 21,
        EuroTunnel = 22
    }
}
