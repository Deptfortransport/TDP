// *********************************************** 
// NAME             : GazetteerEventCategory.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: Defines categories for gazeteer events
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.Reporting.Events
{
    /// <summary>
    /// Enumeration containing classifiers for <c>GazetteerEvent</c>.
    /// </summary>
    public enum GazetteerEventCategory
    {
        GazetteerAddress = 1,
        GazetteerPostCode,
        GazetteerPointOfInterest,
        GazetteerMajorStations,
        GazetteerAllStations,
        GazetteerLocality,
        GazetteerCode,
        GazetteerCoordinate,
        GazetteerUnknown,
        GazetteerAutoSuggestAirport,
        GazetteerAutoSuggestCoach,
        GazetteerAutoSuggestFerry,
        GazetteerAutoSuggestRail,
        GazetteerAutoSuggestTMU,
        GazetteerAutoSuggestGroup,
        GazetteerAutoSuggestLocality,
        GazetteerAutoSuggestPointOfInterest
    }
}
