// *********************************************** 
// NAME             : TDPPrivateAlgorithmType.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 31 Mar 2011
// DESCRIPTION  	: Enumeration of TDPPrivateAlgorithmType
// ************************************************
// 
                
using System;

namespace TDP.UserPortal.JourneyControl
{
    [Serializable()]
    public enum TDPPrivateAlgorithmType
    {
        Fastest = 0,
        Shortest = 1,
        MostEconomical = 2,
        Cheapest = 3,
    }
}
