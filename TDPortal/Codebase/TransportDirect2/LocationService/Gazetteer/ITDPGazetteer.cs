// *********************************************** 
// NAME             : ITDPGazetteer.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: Interface for all gazetteers
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.Common.LocationService.Gazetteer
{
    /// <summary>
    /// Interface for all gazetteers
    /// </summary>
    public interface ITDPGazetteer
    {
        LocationQueryResult FindLocation(string text, bool fuzzy);
        LocationQueryResult DrillDown(string text, bool fuzzy, string pickList, string queryRef, LocationChoice choice);
        TDPLocation GetLocationDetails(string text, bool fuzzy, string pickList, string queryRef, LocationChoice choice);

        bool SupportHierarchicSearch { get; }
    }
}
