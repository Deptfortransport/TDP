#region Version and history
// *********************************************** 
// NAME                 : PageId.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 17/07/2003 
// DESCRIPTION  : Enumeration that holds all
// the page ids that exist.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/PageId.cs-arc  $ 
//
//   Rev 1.17   Jan 04 2013 15:29:28   mmodi
//Added accessible journey ajax pages
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.16   Dec 07 2012 15:57:58   DLane
//New find nearest TDAN page
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.15   Aug 28 2012 10:19:50   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.14   Aug 17 2012 10:54:40   DLane
//Cycle walk links
//Resolution for 5827: CCN Cycle Walk links
//
//   Rev 1.13   Feb 07 2012 12:43:50   dlane
//Check in part 1 for  BatchJourneyPlanner - edited classes
//Resolution for 5787: Batch Journey Planner
//
//   Rev 1.12   Sep 01 2011 10:43:14   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.11   Feb 11 2010 08:53:42   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.10   Nov 02 2009 17:45:48   mmodi
//Added Find map pages
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.9   Sep 21 2009 14:48:16   mmodi
//Updated for Environmental Benefits Calculator (EBC)
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.8   Sep 14 2009 10:55:06   apatel
//Stop Information page changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.7   Oct 14 2008 15:25:46   mmodi
//Manual merge for stream5014
//
//   Rev 1.6   Jul 02 2008 13:16:16   apatel
//fix the issue with popup blocker blocking the page on Ticket Retailers Hand off page. 
//Resolution for 5035: Ticket Retailers Hand off page popup blocker issue
//
//   Rev 1.5   Jun 27 2008 14:18:18   apatel
//CCN 0400 Ticket type feed files
//
//   Rev 1.4.1.3   Oct 13 2008 10:33:46   mmodi
//Added more cycle pages
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4.1.2   Aug 22 2008 10:04:44   mmodi
//Updated
//
//   Rev 1.4.1.1   Jul 18 2008 13:55:30   mmodi
//Added  cycle page
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4.1.0   Jun 20 2008 15:01:40   mmodi
//Updated for cycle planner
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   May 01 2008 17:10:20   mmodi
//Updated page ids for session timeout
//
//   Rev 1.3   Mar 10 2008 15:15:18   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.1   Nov 29 2007 10:21:06   mturner
//Updated for Del 9.8
//
//   Rev 1.88   Nov 08 2007 14:19:28   mmodi
//Added FindNearestLandingPage
//Resolution for 4524: Del 9.8 - Car Parking Page Landing
//
//   Rev 1.87   Oct 16 2007 09:48:54   pscott
//SCR4509 Cookies Error Page - post code review changes
//
//   Rev 1.86   Aug 31 2007 16:19:58   build
//Automatically merged from branch for stream4474
//
//   Rev 1.85.1.0   Aug 30 2007 17:46:06   asinclair
//Added RefineCheckCO2
//Resolution for 4474: DEL 9.7 Stream : Public Transport C02
//
//   Rev 1.85   May 24 2007 15:19:36   mmodi
//Added SorryPage
//Resolution for 4424: 9.6 - Page Landing with CRS Codes
//
//   Rev 1.84   May 23 2007 13:45:50   asinclair
//Added PageIds for events on Modify Journey page
//Resolution for 4421: 9.6 - Add Amend to Modify
//
//   Rev 1.83   Mar 06 2007 12:30:00   Build
//Automatically merged from branch for stream4350
//
//   Rev 1.82.1.1   Feb 20 2007 17:18:34   mmodi
//Added EmissionsCompareJourney page
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.82.1.0   Feb 06 2007 10:52:56   mmodi
//Added JourneyEmissionsCompare
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.82   Jan 14 2007 11:22:14   mmodi
//Added FeedbackViewer page
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.81   Jan 08 2007 10:07:56   mmodi
//Added new Feedback page
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.80   Jan 04 2007 13:33:44   dsawe
//added Page Id for JourneyLandingPage & LocationLandingPage
//Resolution for 4331: iFrames for LastMinute.com
//
//   Rev 1.79   Dec 07 2006 14:17:20   mturner
//Manual merge for stream4240
//
//   Rev 1.78   Nov 14 2006 09:02:54   rbroddle
//Merge for stream4220
//
//   Rev 1.77.1.0   Nov 07 2006 11:15:24   tmollart
//Updated with new page ID for FindTrainCostInput
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.77   Oct 06 2006 10:37:22   mturner
//Merge for stream SCR-4143
//
//   Rev 1.76.1.2   Aug 16 2006 16:12:56   mmodi
//Added Printable Find Car Park ids
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.76.1.1   Aug 01 2006 12:45:46   esevern
//Added CarParkInformation page
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.76.1.0   Jul 28 2006 14:30:52   esevern
//Added FindCarParkInput page
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.76   Apr 05 2006 15:15:18   esevern
//Manual merge of stream0030
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.75   Mar 28 2006 15:31:00   pcross
//Code review updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.74   Mar 28 2006 09:28:16   AViitanen
//Manual merge for Travel news (stream0024)
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.73   Mar 23 2006 17:40:20   tmollart
//Manual merge of stream 0025.
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.72   Mar 17 2006 17:06:00   asinclair
//Removed ExtendJourneyOptions as no longer needed
//
//   Rev 1.71   Mar 16 2006 18:39:58   pcross
//New page PrintableExtensionResultsSummary
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.70   Mar 13 2006 17:32:32   NMoorhouse
//Manual merge of stream3353 -> trunk
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.69   Mar 09 2006 11:44:22   esevern
//added HomePageFindAJourneyPlanner and HomePageFindAPlacePlanner page entries
//Resolution for 3586: Additional PageEntryEvents on HomePage
//
//   Rev 1.68   Feb 10 2006 11:23:06   aviitanen
//Manual merge for Homepage phase 2 (stream3180)
//
//   Rev 1.67   Dec 16 2005 10:30:16   ralonso
//Fixed problem with Claims Policy
//Resolution for 3317: UEE Buttons - Old style button on Claims Privacy Policy
//
//   Rev 1.65   Dec 13 2005 11:31:20   asinclair
//Merge for stream3143
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.64   Dec 12 2005 15:28:16   rgreenwood
//TD109: Added ToolbarDownloadButtonClick to differentiate between page and successful download requests.
//
//   Rev 1.63   Dec 09 2005 14:08:32   RWilby
//TD109: Added PageId HelpToolbar
//
//   Rev 1.62.1.11   Mar 09 2006 16:32:58   RGriffith
//Removal of SegmentsSummary pages and ExtensionResultsDetails and ExtensionResultMap pages
//
//   Rev 1.62.1.10   Feb 24 2006 14:32:38   NMoorhouse
//Changes to support the addition of new page to display CarDetails
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.62.1.9   Feb 16 2006 16:29:44   pcross
//Removed AdjustSegmentsSummary as page not required
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.62.1.8   Feb 14 2006 11:24:56   RGriffith
//New page for RefineTickets & PrintableRefineTickets page
//
//   Rev 1.62.1.7   Feb 09 2006 16:15:14   NMoorhouse
//Rename of ExtendedFullItineraryDetails and Map to RefineDetails and Map
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.62.1.6   Feb 07 2006 18:39:06   NMoorhouse
//New Pages Extended Full Itinerary Details and Map
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.62.1.5   Jan 30 2006 17:30:54   RGriffith
//Changes for renaming of Extended Summary pages
//
//   Rev 1.62.1.4   Jan 30 2006 13:00:18   RGriffith
//Changes to add Full Itinerary and Segments pages for Adjust and Replan.
//
//   Rev 1.62.1.3   Jan 27 2006 12:12:14   pcross
//Added ExtensionResults pages
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.62.1.2   Jan 27 2006 11:16:56   RGriffith
//Changes to add ExtendedFullItineraryResults and ExtendedSegmentsResults pages
//
//   Rev 1.62.1.1   Jan 19 2006 11:24:00   NMoorhouse
//New page Journey Replan Input
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.62.1.0   Jan 13 2006 14:15:16   asinclair
//Added ExtendJourneyOptions, ExtendJourneyInput, and RefineJourneyPlan
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.62   Dec 06 2005 11:20:18   rgreenwood
//TD109: Changed ToolbarDownloadControl to ToolbarDownload for page, not control
//
//   Rev 1.61   Dec 05 2005 15:20:26   rgreenwood
//TD109 Added ToolbarDownloadControl event
//
//   Rev 1.60   Nov 08 2005 16:21:04   ralonso
//PrintableLinks entry added
//
//   Rev 1.59   Nov 04 2005 12:43:36   rhopkins
//Manual merge of stream 2816
//
//   Rev 1.58   Nov 03 2005 11:32:08   kjosling
//Added new enum type
//
//   Rev 1.57   Oct 31 2005 17:39:34   tolomolaiye
//Merge for stream 2638 (Visit Planner)
//Resolution for 2929: Visit Planner Merge Activity
//
//   Rev 1.56   Oct 31 2005 17:38:28   tolomolaiye
//Merge for stream 2638 (Visit Planner)
//
//   Rev 1.55.1.0   Oct 25 2005 20:15:58   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.55   Sep 29 2005 17:26:32   halkatib
//Added code for Landing page merge
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.54   Sep 29 2005 10:34:12   schand
//Merged stream 2673 back into trunk
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.53   Sep 26 2005 18:43:56   rhopkins
//Merge stream 2596 back into trunk
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.52.3.3   Oct 13 2005 19:14:28   jbroome
//Added PrintableVisitPlannerResults
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.52.3.2   Oct 05 2005 09:37:40   tolomolaiye
//Added Visit Planner to enumeration
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.52.3.1   Sep 26 2005 12:31:46   jbroome
//Added Visit Planner Results
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.52.3.0   Sep 21 2005 10:36:50   asinclair
//New branch for 2638 with Del 7.1
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.50.3.2   Sep 15 2005 11:28:48   kjosling
//Merged in changes from version 1.52 (head)
//
//   Rev 1.50.3.1   Aug 12 2005 11:10:14   NMoorhouse
//DN058 Park And Ride, Added PrintableParkAndRide
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.50.3.0   Aug 02 2005 14:51:00   NMoorhouse
//Branched for stream2596
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.50   Apr 08 2005 16:04:48   rhopkins
//Change PageId for PrintableJourneyMap so that it is compatible with the PrinterFriendlyPageButtonControl
//
//   Rev 1.49   Mar 17 2005 17:15:22   rscott
//Added TDOnTheMove
//
//   Rev 1.48   Mar 14 2005 14:44:58   rgeraghty
//Added TicketUpgrade page
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.47   Mar 08 2005 16:36:12   bflenk
//Added TimeOut page
//
//   Rev 1.46   Mar 08 2005 15:03:00   jgeorge
//Added PrintableTicketRetailersHandOff page
//
//   Rev 1.45   Feb 25 2005 16:33:04   COwczarek
//Add Find Fare ticket selection pages
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.44   Feb 21 2005 17:21:02   rgeraghty
//Added RetailerInformation page
//
//   Rev 1.43   Feb 18 2005 16:02:42   rhopkins
//Added PrintableFindFareDateSelection and PrintableFindFareTicketSelection
//
//   Rev 1.42   Jan 14 2005 10:51:28   rhopkins
//Added FindFareInput, FindFareDateSelection and FindFareTicketSelection
//
//   Rev 1.41   Oct 27 2004 14:45:42   Schand
//// Added entry for SeasonalNoticeBoard
//
//   Rev 1.40   Oct 05 2004 14:35:24   jmorrissey
//Added User Survey page id 
//
//   Rev 1.39   Aug 05 2004 14:59:48   COwczarek
//Removal of redundant Find A pages (FindSummary, FindDetails and FindMap, including printable versions)
//Resolution for 1202: Implement FindTrainInput and FindCoachInput pages
//
//   Rev 1.38   Jul 09 2004 15:56:06   jmorrissey
//Added new Find A input pages
//
//   Rev 1.37   Jul 07 2004 09:44:42   AWindley
//Interim update for CCN098: CJP Logging
//
//   Rev 1.36   Jun 01 2004 13:10:28   passuied
//added printable pages for FindStation
//
//   Rev 1.35   May 11 2004 15:43:34   acaunt
//... and removed again (it has been replaced with FindStationInput)
//
//   Rev 1.34   May 11 2004 15:12:46   acaunt
//FindAStationInput readded (seemed to have gone missing)
//
//   Rev 1.33   May 11 2004 14:23:26   jgeorge
//Added entries for Find a Flight
//
//   Rev 1.32   May 05 2004 18:00:40   passuied
//added entry for FindAStationInput
//
//   Rev 1.31   Feb 06 2004 14:48:56   asinclair
//Added ClaimsPolicyPrinter for Del 5.2
//
//   Rev 1.30   Feb 03 2004 14:05:12   asinclair
//Added Page Ids for StaticNoPrint and HelpFullJPrinter.
//
//   Rev 1.29   Nov 24 2003 14:17:34   asinclair
//Added Maintenance page
//
//   Rev 1.28   Nov 18 2003 11:30:46   asinclair
//Added ClaimsPolicy
//
//   Rev 1.27   Nov 14 2003 14:02:32   asinclair
//Added error page info
//
//   Rev 1.26   Nov 05 2003 18:01:48   CHosegood
//Added PrintableJourneyFares
//
//   Rev 1.25   Nov 04 2003 18:03:32   COwczarek
//Add page id for printable ticket retailers page
//
//   Rev 1.24   Oct 31 2003 10:06:30   COwczarek
//Change page id of ticket retailer page
//
//   Rev 1.23   Oct 24 2003 11:33:22   kcheung
//Added ticket retailers hand off page id
//
//   Rev 1.22   Oct 21 2003 09:13:20   ALole
//Added PrintableTrafficMaps reference
//
//   Rev 1.21   Oct 17 2003 16:11:14   JMorrissey
//added entry for PrintableTravelNews
//
//   Rev 1.20   Oct 17 2003 15:13:34   CHosegood
//Added JourneyFares
//
//   Rev 1.19   Oct 09 2003 17:32:22   asinclair
//Added page ID for full help page
//
//   Rev 1.18   Oct 09 2003 15:09:24   COwczarek
//Modifications for addition of Ticket Retailers page
//
//   Rev 1.17   Oct 08 2003 17:07:18   ALole
//Changed TrafficMaps back to TrafficMap
//
//   Rev 1.16   Oct 08 2003 17:05:36   ALole
//Updated TrafficMap to TrafficMaps
//
//   Rev 1.15   Oct 08 2003 15:42:34   ALole
//Added TrafficMap to Enum
//
//   Rev 1.14   Oct 02 2003 16:53:18   passuied
//updated
//
//   Rev 1.13   Oct 02 2003 12:18:48   hahad
//Added ClaimPrintPage
//
//   Rev 1.12   Oct 01 2003 10:24:56   hahad
//Added ClaimsErrorsPage Enum
//
//   Rev 1.11   Sep 30 2003 20:11:56   asinclair
//Added code for DepartureBoards screen
//
//   Rev 1.10   Sep 29 2003 12:06:02   hahad
//Added ClaimsInputPage Enum
//
//   Rev 1.9   Sep 29 2003 11:48:12   hahad
//Added ContactUsPage Enum
//
//   Rev 1.8   Sep 23 2003 15:51:02   asinclair
//Added page idd
//
//   Rev 1.7   Sep 23 2003 11:48:34   hahad
//Added FeedbackInitialPage Enum
//
//   Rev 1.6   Sep 22 2003 18:10:02   asinclair
//Added ref to NetworkMaps, Home and Help pages
//Resolution for 2: Session Manager - Deferred storage
//
//   Rev 1.5   Sep 18 2003 09:57:04   jcotton
//Changes for intitial screenflow integration work
//
//   Rev 1.4   Sep 16 2003 16:24:26   jcotton
//Latest transition events added
//
//   Rev 1.3   Sep 16 2003 14:07:06   jcotton
//1.	Added to the PageID enumeration the following PageIDs:
//·	InitialPage
//·	JourneyPlannerInput
//·	JourneyPlannerAmbiguity
//·	JourneySummary
//·	JourneyPlannerLocationMap
//·	JourneyDetails
//·	JourneyMaps
//·	JourneyAdjust
//·	CompareAdjustedJourney
//·	DetailedLegMap
//·	WaitPage
//·	PrintableJourneySummary
//·	PrintableJourneyDetails
//·	PrintableJourneyMaps
//·	PrintableCompareAdjustedJourney
//·	Feedback
//·	Links
//·	Help
//·	GeneralMaps
//·	SiteMap
//2.	Updated PageTransferDetails.xml and PageTransferEvents.xml.
//3.	Corrected "Test Pages" that referenced PageID enumerations no longer used.
//
//   Rev 1.2   Sep 10 2003 15:17:18   PNorell
//Added journey output page ids.
//
//   Rev 1.1   Sep 10 2003 13:03:06   passuied
//Added new pageIds
//
//   Rev 1.0   Jul 17 2003 12:10:58   kcheung
//Initial Revision
#endregion

using System;

namespace TransportDirect.Common
{
	/// <summary>
	/// Enumeration of all existing pageIds.
	/// </summary>
	public enum PageId
	{ 
		Empty = -1,
		InitialPage,
		JourneyPlannerInput,
		JourneyPlannerAmbiguity,
		JourneySummary,
		JourneyPlannerLocationMap,
		PrintableJourneyPlannerLocationMap,
		JourneyDetails,
		JourneyMap,
		JourneyAdjust,
        JourneyFares,
		CompareAdjustedJourney,
		DetailedLegMap,
		WaitPage,
		PrintableJourneySummary,
		PrintableJourneyDetails,
		PrintableJourneyMap,
		PrintableJourneyMapInput,
        PrintableJourneyMapTile,
		PrintableCompareAdjustedJourney,
        PrintableTicketRetailers,
        PrintableJourneyFares,
		FeedbackInitialPage,
		ContactUsPage,
		ClaimsInputPage,
		ClaimsErrorsPage,
		ClaimPrintPage,
		Feedback,
		Links,
		PrintableLinks,
		Help,
		GeneralMaps,
		SiteMap,
        SiteMapDefault,
		NetworkMaps,
		LocationInformation,
		Map,
		Home,
		TravelNews,
		PrintableTravelNews,
		DepartureBoards,
		TrafficMap,
		PrintableTrafficMap,
        TicketRetailers,
		TicketRetailersHandOff,
        TicketRetailersHandOffFinal,
		HelpFullJP,
		PrintableHelpFullJP,
		Error,
		SorryPage,
		ClaimsPolicy,
		Maintenance,
		PrintableSoftContent,
		ClaimsPolicyPrinter,
		FindStationInput,
		FindStationResults,
		FindStationMap,
		PrintableFindStationResults,
		PrintableFindStationMap,
		FindFlightInput,
		VersionViewer,
		LogViewer,
		FindTrainInput,
		FindCoachInput,
		FindCarInput,
		FindTrunkInput,
		UserSurvey,
		SeasonalNoticeBoard,
		FindFareInput,
		FindFareDateSelection,
		FindFareTicketSelection,
        FindFareTicketSelectionReturn,
        FindFareTicketSelectionSingles,
        PrintableFindFareDateSelection,
		PrintableFindFareTicketSelection,
        PrintableFindFareTicketSelectionReturn,
        PrintableFindFareTicketSelectionSingles,
        RetailerInformation,
		PrintableTicketRetailersHandOff,
		TimeOut,
		TicketUpgrade,
		TDOnTheMove,
		ParkAndRide,
		PrintableParkAndRide,
		JourneyAccessibility,
		ServiceDetails,
		PrintableServiceDetails,
		JPLandingPage,
		JourneyExtensionLastUndo,
		JourneyExtensionAllUndo,
		HomeSessionAbandoned,
		VisitPlannerInput,
		VisitPlannerResults,
		PrintableVisitPlannerResults,
		TNLandingPage,
		ToolbarDownload,
		HelpToolbar,
		ToolbarDownloadButtonClick,
		BusinessLinks,
		HomePlanAJourney,
		HomeFindAPlace,
		HomeTravelInfo,
		HomeTipsTools, 
		HomePageFindAJourneyPlanner, 
		HomePageFindAPlacePlanner, 
		ExtendJourneyInput,
		RefineJourneyPlan,
		JourneyReplanInputPage,
		ExtendedFullItinerarySummary,
		PrintableExtendedFullItinerarySummary,
		ExtensionResultsSummary,
		PrintableExtensionResultsSummary,
		ExtensionResultsTicketsCostsView,
		ReplanFullItinerarySummary,
		PrintableReplanFullItinerarySummary,
		AdjustFullItinerarySummary,
		PrintableAdjustFullItinerarySummary,
		RefineDetails,
		PrintableRefineDetails,
		RefineMap,
		PrintableRefineMap,
		RefineTickets,
		PrintableRefineTickets,
		CarDetails,
		PrintableCarDetails,
		ParkAndRideInput,
		SpecialNoticeBoard,
		FindBusInput,
		FindCarParkInput,
		FindCarParkResults,
		FindCarParkMap,
		CarParkInformation,
		PrintableFindCarParkResults,
		PrintableFindCarParkMap,
		FindTrainCostInput,
		JourneyEmissions,
		PrintableJourneyEmissions,
		JourneyLandingPage,
		LocationLandingPage,
		FeedbackPage,
		FeedbackViewer,
		JourneyEmissionsCompare,
		PrintableJourneyEmissionsCompare,
		JourneyEmissionsCompareJourney,
		PrintableJourneyEmissionsCompareJourney,
		ModifyExtendOutwardClicked,
		ModifyExtendReturnClicked,
		ModifyReplaceOutwardClicked,
		ModifyReplaceReturnClicked,
		ModifyAdjustOutwardClicked,
		ModifyAdjustReturnClicked,
		ModifyAmendClicked,
		ModifyBackClicked,
		ModifyNewClicked,
		RefineCheckC02,
		ErrorPageCookies,
		FindNearestLandingPage,
        CO2LandingPage,
        JourneyOverview,
        PrintableJourneyOverview,
        LoginRegister,
        FAQ,
        LinksWithoutPrint, 
        PrintableTicketType,
        FindCycleInput,
        CycleJourneyDetails,
        CycleJourneyGPXDownload,
        PrintableCycleJourneyDetails,
        CycleJourneySummary,
        PrintableCycleJourneySummary,
        CycleJourneyMap,
        StopInformation,
        StopServiceDetails,
        StopInformationLandingPage,
        FindEBCInput,
        EBCJourneyDetails,
        EBCJourneyMap,
        PrintableEBCJourneyDetails,
        PrintableEBCJourneyMap,
        FindMapInput,
        FindMapResult,
        PrintableFindMapResult,
        FindInternationalInput,
        TDWebService,
        BatchJourneyPlanner,
        Admin,
        CycleLinkClicked,
        LocationMoreOptionsClicked,
        FindNearestAccessibleStop,
        Infographic,

        // AJAX postback page events
        FindNearestAccessibleStopTransportClickAJAX,
        FindNearestAccessibleStopMapClickAJAX,
        FindNearestAccessibleStopMapHideClickAJAX,

        // Mobile pages - See TDPMobile
        MobileDefault,
        MobileInput,
        RedirectMobile
	};

	// The test page id's and should be added to the PageId Enum
	// only for the duration of re-testing.  Once completed this
	// page should have "Undo Checkout..." applied.
//		Login,
//		ShowFavourites,
//		Home,
//		Bookmark,
//		TestCapture,
//		TestAmbiguous,
//		TestMap,
//		TestWaiting,

}
