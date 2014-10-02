// *********************************************** 
// NAME             : DBWSFilterType.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: DBWSFilterType enumeration to define direction to apply filter stop to departure board 
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportDirect.WebService.DepartureBoardWebService.DataTransfer
{
    /// <summary>
    /// DBWSFilterType enumeration to define direction to apply stop filter to departure board
    /// </summary>
    public enum DBWSFilterType
    {
        ServicesTo,
        ServicesFrom
    }
}