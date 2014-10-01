// *********************************************** 
// NAME             : FindAPlannerMode.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 21 Dec 2010
// DESCRIPTION  	: Find a mode enum resembling the FindAMode enum in session manager
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/FindAPlannerMode.cs-arc  $
//
//   Rev 1.0   Dec 21 2010 14:15:02   apatel
//Initial revision.
//Resolution for 5651: CCN 593 - Show 10 results or show all
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.JourneyControl
{
    /// <summary>
    /// Enumeration defining the different modes
    /// of the FindA functionality
    /// </summary>
    public enum FindAPlannerMode
    {
        None,
        Flight,
        Station,
        Train,
        Coach,
        Fare,
        TrunkCostBased,
        Trunk,
        TrunkStation,
        Car,
        Bus,
        CarPark,
        RailCost,
        ParkAndRide,
        Cycle,
        EnvironmentalBenefits,
        International
    }
}
