// *********************************************** 
// NAME             : DBSActivityType.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2014
// DESCRIPTION  	: Enumeration for a stop Activity Type
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.UserPortal.DepartureBoardService.DepartureBoardServiceTypes
{
    /// <summary>
    /// Enumeration for a stop Activity Type
    /// </summary>
    public enum DBSActivityType
    {
        Arrive,
        Depart,
        ArriveDepart,
        Unavailable
    }
}
