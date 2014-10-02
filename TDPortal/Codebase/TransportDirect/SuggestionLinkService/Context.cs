// *********************************************** 
// NAME                 : Context.cs
// AUTHOR               : Ken Josling
// DATE CREATED         : 16/08/2005 
// DESCRIPTION			: An enumeration containing a list of valid contexts for a Suggestion Box 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SuggestionLinkService/Context.cs-arc  $
//
//   Rev 1.8   Dec 07 2012 15:58:04   DLane
//New find nearest TDAN page
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.7   Feb 07 2012 12:44:00   dlane
//Check in part 1 for  BatchJourneyPlanner - edited classes
//Resolution for 5787: Batch Journey Planner
//
//   Rev 1.6   Mar 19 2010 10:50:34   apatel
//Added custom related links for city to city result pages
//Resolution for 5468: Related link in city to city incorrect
//
//   Rev 1.5   Feb 25 2010 12:00:48   apatel
//Updated for International planner input page
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.4   Sep 21 2009 14:56:46   mmodi
//Updated for Environmental Benefits Calculator (EBC)
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.3   Oct 13 2008 16:46:40   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.0   Aug 01 2008 16:38:30   mmodi
//Added cycle related links context
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 10 2008 15:27:56   mturner
//Initial Del10 Codebase from Dev Factory
//
//  Rev DevFactory Feb 23 2008 18:00:00 apatel
//  updated some of the contexts to match context database values
//
//   Rev 1.0   Nov 08 2007 12:50:08   mturner
//Initial revision.
//
//   Rev 1.8   Sep 11 2007 11:22:36   mmodi
//Added Journey Emissions information link context
//Resolution for 4495: DEL 9.7 Related Links control updates
//
//   Rev 1.7   Sep 07 2007 16:21:54   mmodi
//Added JourneyEmissions compare
//Resolution for 4495: DEL 9.7 Related Links control updates
//
//   Rev 1.6   Sep 07 2007 10:43:02   mmodi
//Added context JourneyEmissions
//Resolution for 4495: DEL 9.7 Related Links control updates
//
//   Rev 1.5   Nov 14 2006 10:38:58   dsawe
//added FindTrainCostInput
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.3.1.0   Oct 23 2006 11:47:18   kjosling
//Added FindTrainInput context for Find A Train related links control
//
//   Rev 1.4   Oct 23 2006 11:46:02   kjosling
//Added FindTrainInput context for Find A Train Related Links
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.3   May 02 2006 13:15:24   mtillett
//Added new ParkAndRideInput context 
//Resolution for 4057: DN058 Park & Ride Phase 2: Schemes link missing from Plan to Park and Ride page
//
//   Rev 1.2   Feb 10 2006 15:04:38   build
//Automatically merged from branch for stream3180
//
//   Rev 1.1.1.1   Dec 14 2005 18:24:44   NMoorhouse
//New Suggestion Links for MiniHomepages
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.1.1.0   Dec 13 2005 12:03:10   NMoorhouse
//New Context from HomePage Expandable Menu
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.1   Aug 25 2005 16:26:40   NMoorhouse
//Added contexts for ParkAndRide and FindCarInput pages
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.0   Aug 24 2005 16:44:52   kjosling
//Initial revision.

using System;

namespace TransportDirect.UserPortal.SuggestionLinkService
{
	public enum Context
	{
		ParkAndRide,
		FindCarInput,
        FindCarParkInput,
		HomePageMenu,
		HomePlanAJourney,
		HomeFindAPlace,
		HomeTravelInfo,
		HomeTipsTools,
		ParkAndRideInput,
		FindTrainInput,
		FindTrainCostInput,
		JourneyEmissions, // Car speedo dial CO2 emissions page
		JourneyEmissionsCompare, // PT CO2 emissions page
		JourneyEmissionsCompareInfo,
        FindCoachInput, 
        //white labelling
        RelatedLinksContextParkAndRideInput,
        RelatedLinksContextAdjustFullItinerarySummary,
        RelatedLinksContextContactUsDetails,
        RelatedLinksContextFeedbackPage,
        RelatedLinksContextCarParkInformation,
        RelatedLinksContextCarDetails,
        RelatedLinksContextCO2LandingPage,
        RelatedLinksContextCompareAdjustedJourney,
        RelatedLinksContextExtendedFullItinerarySummary,
        RelatedLinksContextExtendJourneyInput,
        RelatedLinksContextExtensionResultsSummary,
        RelatedLinksContextFindAStationInput,
        RelatedLinksContextFindBusInput,
        RelatedLinksContextFindCarInput,
        RelatedLinksContextFindCarParkInput,
        RelatedLinksContextFindCarParkMap,
        RelatedLinksContextFindCarParkResults,
        RelatedLinksContextFindCoachInput,
        RelatedLinksContextFindFareDateSelection,
        RelatedLinksContextFindFareTicketSelection,
        RelatedLinksContextFindFareTicketSelectionReturn,
        RelatedLinksContextFindFareTicketSelectionSingles,
        RelatedLinksContextFindFlightInput,
        RelatedLinksContextFindTrainCostInput,
        RelatedLinksContextFindTrainInput,
        RelatedLinksContextFindTrunkInput,
        RelatedLinksContextHomeJourneyPlanning,
        RelatedLinksContextJourneyAccessibility,
        RelatedLinksContextJourneyAdjust,
        RelatedLinksContextJourneyDetails,
        RelatedLinksContextJourneyDetailsFindCarRoute,
        RelatedLinksContextJourneyDetailsFindTrunkInput,
        RelatedLinksContextJourneyEmissions,
        RelatedLinksContextJourneyEmissionsCompare,
        RelatedLinksContextJourneyEmissionsCompareJourney,
        RelatedLinksContextJourneyFares,
        RelatedLinksContextJourneyFaresFindTrunkInput,
        RelatedLinksContextJourneyMap,
        RelatedLinksContextJourneyMapFindTrunkInput,
        RelatedLinksContextJourneyOverview,
        RelatedLinksContextJourneyPlannerAmbiguity,
        RelatedLinksContextJourneyPlannerInput,
        RelatedLinksContextJourneyReplanInputPage,
        RelatedLinksContextJourneySummary,
        RelatedLinksContextJourneySummaryFindCarRoute,
        RelatedLinksContextJourneySummaryFindTrunkInput,
        RelatedLinksContextLocationInformation,
        RelatedLinksContextParkAndRide,
        RelatedLinksContextRefineDetails,
        RelatedLinksContextRefineJourneyPlan,
        RelatedLinksContextRefineMap,
        RelatedLinksContextRefineTickets,
        RelatedLinksContextReplanFullItinerarySummary,
        RelatedLinksContextRetailerInformation,
        RelatedLinksContextServiceDetails,
        RelatedLinksContextTicketRetailers,
        RelatedLinksContextTicketRetailersHandOff,
        RelatedLinksContextTicketUpgrade,
        RelatedLinksContextVisitPlannerInput,
        RelatedLinksContextVisitPlannerResults,
        RelatedLinksContextDepartureBoards,
        RelatedLinksContextHomeLiveTravel,
        RelatedLinksContextTravelNews,
        RelatedLinksContextHomeMaps,
        RelatedLinksContextJourneyPlannerLocationMap,
        RelatedLinksContextTDOnTheMove,
        RelatedLinksContextBusinessLinks,
        RelatedLinksContextHomeTools,
        RelatedLinksContextToolbarDownload,
        RelatedLinksContextFeedbackViewer,
        RelatedLinksContextLogViewer,
        RelatedLinksContextVersionViewer,
        RelatedLinksContextFindStationMap,
        RelatedLinksContextFindStationResults,
        RelatedLinksContextHome,
        RelatedLinksContextLoginRegister,
        RelatedLinksContextSeasonalNoticeBoard,
        RelatedLinksContextSpecialNoticeBoard,
        RelatedLinksContextTrafficMaps,
        RelatedLinksContextFindCycleInput,
        RelatedLinksContextFindEBCInput,
        RelatedLinksContextFindInternationalInput,
        RelatedLinksContextFindNearestAccessibleStop,
        BatchJourneyPlanner,
        HomePageMenuPlanAJourney,
        HomePageMenuFindAPlace,
        HomePageMenuLiveTravel,
        HomePageMenuTipsAndTools,
        HomePageMenuLoginRegister,
        HomePageMenuLoggedIn,//end added for white labelling
        AboutUsMenu,
        TermsConditionsMenu,
        PrivacyPolicyMenu,
        DataProvidersMenu,
        AccessibilityMenu,
        RelatedSitesMenu,
        FAQMenu,
       	CONTEXT1,
		CONTEXT2,
		CONTEXT3
	}
}
