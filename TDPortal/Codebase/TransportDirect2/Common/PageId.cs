﻿// *********************************************** 
// NAME             : PageId.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Feb 2011
// DESCRIPTION  	: Enumeration that holds all the page ids that exist
// ************************************************
// 
                
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.Common
{
    /// <summary>
	/// Enumeration of all page ids
	/// </summary>
    public enum PageId
    {
        Empty = -1,

        // Pages (physical pages)
        Default,
        Error,
        Sorry,
        PageNotFound,
        JourneyPlannerInput,
        JourneyLocations,
        JourneyOptions,
        Retailers,
        RetailerHandoff,
        PrintableJourneyOptions,
        TravelNews,
        CycleJourneyGPXDownload,
        AccessibilityOptions,
        MapBing,
        PrintableMapBing,
        MapGoogle,

        // Mobile Pages (physical pages)
        MobileDefault,
        MobileError,
        MobileSorry,
        MobilePageNotFound,
        MobileInput,
        MobileSummary,
        MobileDetail,
        MobileDirection,
        MobileMap,
        MobileMapSummary,
        MobileMapJourney,
        MobileTravelNews,
        MobileTravelNewsDetail,
        MobileAccessibilityOptions,
        MobileStopInformation,
        MobileStopInformationDetail,
        MobileTerms,
        MobilePrivacy,
        MobileCookie,

        // Page Actions (actions performed on site, e.g. AJAX callback to check for journey results)
        JourneyInputPartialUpdate,
        JourneyLocationsStopEventWait,
        JourneyLocationsStopEventResult,
        JourneyOptionsWait,
        JourneyOptionsResult,
        TravelNewsPartialUpdate,
        MobileInputPartialUpdate,
        MobileSummaryPartialUpdate,
        MobileSummaryWait,
        MobileSummaryResult,
        MobileTravelNewsPartialUpdate,
        RedirectMobile,
        
        // TDP/External pages (only used for sitemap/navigation)
        Homepage,
        Login,
        Contact,
        Sitemap,
        KizoomRail,
        KizoomBus,
        KizoomTravelNews
    }
}
