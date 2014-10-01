// *********************************************** 
// NAME             : TDPCodeType.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2014
// DESCRIPTION  	: Enumeration of possible code types
// ************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.Common.LocationService
{
    [Serializable()]
    public enum TDPCodeType
    {
        Unknown, // Default
        NAPTAN, 
        CRS,
        SMS,
        IATA,
        Postcode
    }
}
