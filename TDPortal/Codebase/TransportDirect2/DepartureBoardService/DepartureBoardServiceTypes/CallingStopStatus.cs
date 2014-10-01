// *********************************************** 
// NAME             : CallingStopStatus.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2014
// DESCRIPTION  	: Enumeration specifying if a stopEvents has calling stops, or not or if the information 
// is unknown at this stage
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.UserPortal.DepartureBoardService.DepartureBoardServiceTypes
{
    /// <summary>
    /// Enumeration specifying if a stopEvents has calling stops, or not or if the information 
    /// is unknown at this stage
    /// </summary>
    public enum CallingStopStatus
    {
        Unknown,
        HasCallingStops,
        NoCallingStops
    }
}
