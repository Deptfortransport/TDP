// *********************************************** 
// NAME             : CJPInfoType.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 26 Oct 2010
// DESCRIPTION  	: Enum defining deferent types of CJP Info available
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/CJPInfoType.cs-arc  $
//
//   Rev 1.3   Mar 21 2013 10:13:00   mmodi
//Updates for journey display notes filtering
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.2   Mar 19 2013 12:05:36   mmodi
//Updates to accessible icons and display of debug info from PublicJourneyDetail
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.1   Dec 10 2012 12:10:04   mmodi
//Add service detail info
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Oct 26 2010 14:32:58   apatel
//Initial revision.
//Resolution for 5623: Additional information available to CJP users
// 
                
                
using System;
using System.Collections.Generic;
using System.Web;

namespace TransportDirect.UserPortal.Web.Adapters
{
    /// <summary>
    /// Enum to define deferent types of CJP Info can be provided to power users
    /// </summary>
    public enum CJPInfoType
    {
        None,
        NaPTAN,
        Coordinate,
        WalkLength,
        InterchangeTime,
        JourneySpeed,
        LinkTOIDs,
        DataSource,
        TrunkExchangePoint,
        FareDetail,
        ServiceDetail,
        JourneyDisplayNotes,
        LegDebugInfo
    }
}
