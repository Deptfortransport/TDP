// *********************************************** 
// NAME                 : TransitionEvent.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 17/07/2003 
// DESCRIPTION  : Enumeration that holds all
// the transition events that exist.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/TransitionEvent.cs-arc  $ 
//
//   Rev 1.11   Dec 07 2012 15:58:00   dlane
//New find nearest TDAN page
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.10   Mar 15 2012 17:36:18   dlane
//Adding batch page for login redirects
//Resolution for 5787: Batch Journey Planner
//
//   Rev 1.9   Feb 11 2010 08:53:12   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.8   Nov 02 2009 17:45:48   mmodi
//Added Find map pages
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.7   Sep 21 2009 14:48:16   mmodi
//Updated for Environmental Benefits Calculator (EBC)
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.6   Sep 14 2009 10:55:08   apatel
//Stop Information page changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.5   Oct 14 2008 15:27:24   mmodi
//Manual merge for stream5014
//
//   Rev 1.4   Jul 02 2008 13:16:16   apatel
//fix the issue with popup blocker blocking the page on Ticket Retailers Hand off page. 
//Resolution for 5035: Ticket Retailers Hand off page popup blocker issue
//
//   Rev 1.3.1.1   Oct 08 2008 09:25:10   rbroddle
//CCN460 Better Use of Seasonal Noticeboard
//Resolution for 5103: ATO585 CCN460 Better Use of Seasonal Noticeboard
//
//   Rev 1.3.1.0   Jun 20 2008 15:01:42   mmodi
//Updated for cycle planner
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   May 06 2008 10:06:54   apatel
//added loginregisterback transition event
//
//   Rev 1.2   Mar 10 2008 15:15:20   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:19:10   mturner
//Initial revision.
//
//   Rev 1.71   Aug 31 2007 16:20:00   build
//Automatically merged from branch for stream4474
//
//   Rev 1.70.1.0   Aug 30 2007 17:46:58   asinclair
//Added RefineCheckCO2
//Resolution for 4474: DEL 9.7 Stream : Public Transport C02
//
//   Rev 1.70   May 24 2007 15:23:34   mmodi
//Added SorryPage
//Resolution for 4424: 9.6 - Page Landing with CRS Codes
//
//   Rev 1.69   Mar 06 2007 12:30:02   Build
//Automatically merged from branch for stream4350
//
//   Rev 1.68.1.1   Feb 20 2007 17:40:08   mmodi
//Added JourneyEmissionsCompareJourney transitions
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.68.1.0   Feb 06 2007 10:52:10   mmodi
//Added JourneyEmissionsCompare
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.68   Jan 14 2007 11:28:54   mmodi
//Added GoFeedbackViewer transition
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.67   Dec 07 2006 14:21:30   mturner
//Manual merge for stream4240
//
//   Rev 1.66   Nov 14 2006 09:09:48   rbroddle
//Merge for stream4220
//
//   Rev 1.65.1.0   Nov 07 2006 11:17:30   tmollart
//Added new transition events for Rail Search By Price
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.65   Oct 06 2006 10:41:20   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.64.1.4   Sep 05 2006 11:42:30   esevern
//added results info trans event
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.64.1.3   Aug 14 2006 10:52:42   esevern
//Added transition events for NearestCarPark navigation (back, hide map etc).
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.64.1.2   Aug 02 2006 17:10:16   mmodi
//Added CarParkInformationBack
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.64.1.1   Aug 01 2006 12:45:46   esevern
//Added CarParkInformation page
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.64.1.0   Jul 28 2006 14:30:50   esevern
//Added FindCarParkInput page
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.64   Apr 05 2006 15:15:54   esevern
//Manual merge of stream0030
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.63   Mar 28 2006 15:31:26   pcross
//Code review updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.62   Mar 23 2006 17:40:22   tmollart
//Manual merge of stream 0025.
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.61   Mar 17 2006 17:29:22   asinclair
//Removed ExtendJourneyOptions
//
//   Rev 1.60   Mar 13 2006 17:31:14   NMoorhouse
//Manual merge of stream3353 -> trunk
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.59   Feb 10 2006 11:23:32   aviitanen
//Manual merge for Homepage phase 2 (stream3180)
//
//   Rev 1.58   Dec 12 2005 16:31:38   rgreenwood
//TD109: Code Review Actions: removed ToolbarDownloadButtonCick transition event
//
//   Rev 1.57   Dec 12 2005 15:29:56   rgreenwood
//TD109: Added ToolbarDownloadButtonClick to differentiate between page and successful download requests.
//
//   Rev 1.56   Dec 09 2005 14:09:24   RWilby
//TD109: Added HelpToolbar
//
//   Rev 1.55.1.15   Mar 09 2006 16:33:22   RGriffith
//Removal of SegmentsSummary pages and ExtensionResultsDetails and ExtensionResultMap pages
//
//   Rev 1.55.1.14   Mar 08 2006 11:11:28   tmollart
//Added new Transition Event for Refine back button.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.55.1.13   Feb 24 2006 14:32:40   NMoorhouse
//Changes to support the addition of new page to display CarDetails
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.55.1.12   Feb 20 2006 19:48:18   asinclair
//Added RefineJourney
//
//   Rev 1.55.1.11   Feb 16 2006 16:31:12   pcross
//Added outward and return transition types from Refine input to adjust input page
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.55.1.10   Feb 14 2006 11:59:46   asinclair
//Added ExtendJourneyInput values
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.55.1.8   Feb 09 2006 16:15:16   NMoorhouse
//Rename of ExtendedFullItineraryDetails and Map to RefineDetails and Map
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.55.1.7   Feb 07 2006 18:39:08   NMoorhouse
//New Pages Extended Full Itinerary Details and Map
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.55.1.6   Jan 30 2006 17:31:16   RGriffith
//Changes for renaming of Extended Summary pages
//
//   Rev 1.55.1.5   Jan 30 2006 13:00:02   RGriffith
//Changes to add Full Itinerary and Segments pages for Adjust and Replan.
//
//   Rev 1.55.1.4   Jan 27 2006 12:14:02   pcross
//ExtensionResultsSummaryView added
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.55.1.3   Jan 27 2006 11:17:36   RGriffith
//Changes to add ExtendedFullItineraryResults and ExtendedSegmentsResults pages
//
//   Rev 1.55.1.2   Jan 27 2006 11:02:16   tmollart
//Added transition events for replan.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.55.1.1   Jan 19 2006 11:24:50   NMoorhouse
//Transition for new page Journey Replan Input
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.55.1.0   Jan 13 2006 14:16:38   asinclair
//Added ExtendJourneyOptions, ExtendJourneyInput and RefineJourneyPlan
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.55   Dec 06 2005 11:16:12   rgreenwood
//TD109: Added ToolbarDownload
//
//   Rev 1.54   Nov 17 2005 13:05:22   pcross
//Manual merge of stream2880
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.52.1.0   Nov 07 2005 18:50:44   RGriffith
//Merging later chunk changes back into stream2880
//
//   Rev 1.53   Nov 10 2005 10:18:50   jgeorge
//Merge for stream2818 (search by price)
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.52   Oct 31 2005 17:40:02   tolomolaiye
//Merge for stream 2638 (Visit Planner)
//Resolution for 2929: Visit Planner Merge Activity
//
//   Rev 1.51   Sep 29 2005 10:34:32   schand
//Merged stream 2673 back into trunk
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.50   Sep 26 2005 18:45:16   rhopkins
//Merge stream 2596 back into trunk
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.49.2.0   Sep 26 2005 12:32:26   jbroome
//Added transition events for Visit Planner
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.47.3.1   Sep 15 2005 11:32:08   kjosling
//Merged in changes from v1.49
//
//   Rev 1.47.3.0   Aug 02 2005 15:07:30   NMoorhouse
//Branched for stream2596
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.47   Apr 11 2005 08:32:52   rgeraghty
//JourneySummaryTicketSelectionBack transition event added
//Resolution for 2058: PT: Back button missing from the Find Fare Results (Summary) Page
//
//   Rev 1.46   Apr 01 2005 15:30:22   tmollart
//FindFareDateSelectionDefault.
//
//   Rev 1.45   Mar 24 2005 15:36:36   COwczarek
//Add transition for switching partitions on find fare pages
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.44   Mar 17 2005 17:15:24   rscott
//Added TDOnTheMove
//
//   Rev 1.43   Mar 14 2005 14:45:46   rgeraghty
//Added transition event for Ticket Upgrade page
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.42   Mar 11 2005 15:10:58   COwczarek
//Add transition events for find fare ticket selection pages
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.41   Mar 09 2005 15:01:30   tmollart
//Added FindFareAmbiguityResolution.
//
//   Rev 1.40   Mar 08 2005 16:36:58   bflenk
//Added TimeOut page transition event
//
//   Rev 1.39   Mar 08 2005 10:48:22   rscott
//DEL 7 - new transition events added FindFareWaitingRefresh,
//FindFareServicesWaitingRefresh.
//
//   Rev 1.38   Mar 02 2005 16:12:44   jgeorge
//Added transition event to TicketRetailerHandoff page
//
//   Rev 1.37   Feb 24 2005 17:03:06   rhopkins
//Added TransitionEvent for FindFareTicketSelectionDefault
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.36   Feb 21 2005 17:21:52   rgeraghty
//Added RetailerInformationBack transition event
//
//   Rev 1.35   Feb 16 2005 14:08:08   tmollart
//Added FindFareDefault transition event.
//
//   Rev 1.34   Oct 14 2004 10:34:28   jmorrissey
//Added JourneyPrintBack
//
//   Rev 1.33   Oct 05 2004 14:37:56   jmorrissey
//Added User Survey transition event
//
//   Rev 1.32   Aug 05 2004 14:59:48   COwczarek
//Removal of redundant Find A pages (FindSummary, FindDetails and FindMap, including printable versions)
//Resolution for 1202: Implement FindTrainInput and FindCoachInput pages
//
//   Rev 1.31   Jul 29 2004 15:29:38   passuied
//Added FindCarInputDefault
//
//   Rev 1.30   Jul 22 2004 15:54:54   COwczarek
//Add transition FindAInputOk
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.29   Jul 21 2004 10:52:20   passuied
//added transition events
//
//   Rev 1.28   Jul 16 2004 10:34:38   COwczarek
//Add new enums for Find A Train
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.27   Jul 07 2004 09:44:34   AWindley
//Interim update for CCN098: CJP Logging
//
//   Rev 1.26   Jun 25 2004 13:47:06   jgeorge
//Added new transition FindFlightInputRedirectToResults
//
//   Rev 1.25   May 25 2004 09:54:00   JHaydock
//Update to FindSummary and related OutputNavigationControl
//
//   Rev 1.24   May 13 2004 19:09:38   passuied
//new transition events for FindStations
//
//   Rev 1.23   May 11 2004 14:23:28   jgeorge
//Added entries for Find a Flight
//
//   Rev 1.22   Feb 06 2004 14:43:14   CHosegood
//Added GoTrafficMap
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.21   Nov 13 2003 13:19:08   kcheung
//Added GoSummary to support the OutputNavigationControl
//
//   Rev 1.20   Oct 20 2003 10:23:38   CHosegood
//Added GoJourneyFares
//
//   Rev 1.19   Oct 16 2003 13:22:36   passuied
//added transition event
//
//   Rev 1.18   Oct 09 2003 15:09:24   COwczarek
//Modifications for addition of Ticket Retailers page
//
//   Rev 1.17   Oct 05 2003 16:16:22   hahad
//Added ClaimToPrint, ClaimToFeedback, ClaimToContact ENum
//
//   Rev 1.16   Oct 02 2003 11:15:48   hahad
//Added Page Transition Events for ContactUsPage
//
//   Rev 1.15   Oct 01 2003 18:09:02   hahad
//Added Transition Enum for UserFeedbackToClaim, UserFeedbackToFeedback, UserFeedbackToContactUs.  
//
//   Rev 1.14   Sep 30 2003 20:11:54   asinclair
//Added code for DepartureBoards screen
//
//   Rev 1.13   Sep 23 2003 19:10:04   asinclair
//added GoLive event
//
//   Rev 1.12   Sep 23 2003 18:56:06   RPhilpott
//Added LocationInformationBack
//
//   Rev 1.11   Sep 23 2003 11:46:32   PNorell
//Added new transition event.
//
//   Rev 1.10   Sep 23 2003 02:06:12   passuied
//added events
//
//   Rev 1.9   Sep 22 2003 19:46:00   AToner
//Addition of LocationInformation Transition
//
//   Rev 1.8   Sep 22 2003 18:57:42   PNorell
//Updated all transition events and page ids and interaction events.
//
//   Rev 1.7   Sep 19 2003 20:09:46   kcheung
//Added another transition events
//
//   Rev 1.6   Sep 18 2003 09:57:04   jcotton
//Changes for intitial screenflow integration work
//
//   Rev 1.5   Sep 16 2003 16:24:26   jcotton
//Latest transition events added
//
//   Rev 1.4   Sep 16 2003 15:43:36   passuied
//latest version
//
//   Rev 1.3   Sep 10 2003 13:03:16   passuied
//added new transition events
//
//   Rev 1.2   Jul 21 2003 13:55:24   kcheung
//Changed index for default to -1
//
//   Rev 1.1   Jul 21 2003 13:52:46   kcheung
//Added Default value
//
//   Rev 1.0   Jul 17 2003 12:11:04   kcheung
//Initial Revision

using System;

namespace TransportDirect.Common
{
	/// <summary>
	/// Enumeration that holds all the transition events.
	/// </summary>
	public enum TransitionEvent
	{
		Empty = -2,
		Default = -1,
		GoHome,
		JourneyPlannerInputDefault,
		JourneyPlannerInputRedirectToOutput,
		JourneyPlannerInputToMap,
		JourneyPlannerInputErrors,
		JourneyPlannerInputOK,
		JourneyPlannerAmbiguityDefault,
		JourneyPlannerAmbiguityMap,
		JourneyPlannerAmbiguityBack,
		JourneyPlannerAmbiguityFind,
		GoJourneySummary,
        GoJourneyOverview,
		GoJourneyDetails,
		GoJourneyMap,
		GoJourneyAdjust,
		GoJourneyAdjustOk,
		GoJourneyDetailMap,
        GoJourneyFares,
		JourneySummaryPrint,
		JourneySummaryNewSearch,
		JourneySummaryAmend,
		JourneySummaryTicketSelectionBack,
		AmbiguousBack,
		AmbiguousSubmit,
		GoFeedback,
		FeedbackBack,
		GoLinks,
		LinksBack,
		GoHelpFAQ,
		HelpFAQBack,
		GoGeneralMaps,
		GeneralMapsBack,
		GoSiteMap,
		SiteMapBack,
		GoMap,
		MapBack,
		LocationMapDefault,
		LocationMapBack,
		LocationMapStationInfo,
		LocationMapFindJourneys,
		LocationMapMoreDetail,
		LocationMapUseLocation,
		LocationInformation,
		LocationInformationBack,
		WaitingRefresh,
		GoLive,
		GoDepartureBoards,
		UserFeedbackToClaim,
		UserFeedbackToContactUs,
		ContactToFeedback,
		ContactToClaim,
		ClaimToPrint,
		ClaimToFeedback,
		ClaimToContact,
        GoTicketRetailers,
		HelpFullJPBack,
        GoTrafficMap,
		FindADefault,
		FindFlightInputDefault,
        FindTrainInputDefault,
        FindStationInputDefault,
		FindCoachInputDefault,
		FindTrunkInputDefault,
		FindCarInputDefault,
		FindFlightInputToStationFinder,
		FindFlightInputOk,
		FindStationInputBack,
		FindStationInputUnambiguous,
		FindStationInputAmbiguous,
		FindStationResultsShowMap,
		FindStationResultsTravelFromTo,
		FindStationResultsNewLocation,
		FindStationResultsInfo,
		FindStationMapHideMap,
        FindAInputRedirectToResults,
        FindAInputOk,
		GoVersionViewer,
		GoLogViewer,
		GoFeedbackViewer,
		GoUserSurvey,
		JourneyPrintBack,
		FindFareInputDefault,
		FindFareAmbiguityResolution,
		FindFareDateSelectionDefault,
		FindFareTicketSelectionDefault,
        FindFareTicketSelectionBack,
        FindFareServiceResults,
        FindFareSwitchPartition,
        RetailerInformationBack,
		RetailerHandoff,
        RetailerHandoffFinal,	
		TimeOut,
		GoTicketUpgrade,
		TDOnTheMove,
		ParkAndRide,
		ParkAndRideInput,
		JourneyAccessibility, 
		JourneyAccessibilityBack,
		ServiceDetails,
		ServiceDetailsBack,
		SessionAbandon,
		JourneyExtensionLastUndo,
		JourneyExtensionAllUndo,
		VisitPlannerInputAdvanced,
		VisitPlannerInputNext,
		VisitPlannerInputBack,
		VisitPlannerAmend,
		VisitPlannerNewClear,
		VisitPlannerResultsMore,
		VisitPlannerResultsSchematicView,
		VisitPlannerResultsTableView,
		VisitPlannerResultsMapView,
		PlanAJourneyAdvanced,
		ToolbarDownload,
		HelpToolbar,
		PlanAJourneyTab,
		FindAPlaceTab,
		TravelInfoTab,
		TipsToolsTab,
		ExtendJourneyInputStart,
		ExtendJourneyInputEnd,
		ExtendJourneyInput,
		RefineJourneyPlan,
		JourneyReplanOutward,
		JourneyReplanReturn,
		ExtendedFullItinerarySummary,
		ExtensionResultsSummaryView,
		ReplanFullItinerarySummary,
		AdjustFullItinerarySummary,
		RefineDetailsSchematic,
		RefineDetailsTabular,
		RefineMapView,
		RefineDetailsBack,
		RefineTicketsView,
		ExtendJourneyInputNext,
		JourneyAdjustOutward,
		JourneyAdjustReturn,
		RefineJourney,
		CarDetails,
		CarDetailsBack,
		RefineJourneyBack,
		FindBusInputDefault,
		FindBusInputOK,
		FindCarParkInputDefault,
		FindCarParkInputUnambiguous,
		FindCarParkInputAmbiguous,
		FindCarParkResultsShowMap,
		CarParkInformation,
		CarParkInformationBack,
		FindCarParkMapHideMap,
		FindCarParkResultsNewLocation,
		FindCarParkResultsInfo,
		FindTrainCostInputDefault,
		FindTrainCostAmbiguityResolution,
		JourneyEmissions,
		JourneyEmissionsBack,
		JourneyEmissionsCompare,
		JourneyEmissionsCompareJourney,
		JourneyEmissionsCompareJourneyBack,
		SorryPage,
		RefineCheckC02,
        LoginRegister,
        LoginRegisterBack,
        FindCycleInputDefault,
        CycleJourneyDetails,
        SeasonalNoticeBoard,
        SeasonalNoticeBoardBack,
        StopInformation,
        StopInformationBack,
        StopServiceDetails,
        StopServiceDetailsBack,
        FindEBCInputDefault,
        EBCJourneyDetails,
        EBCJourneyMap,
        FindMapInputDefault,
        FindMapResult,
        FindInternationalInputDefault,
        FindInternationalInputOK,
        BatchJourneyPlanner,
        FindNearestAccessibleStop,
        Infographic
	}
}
