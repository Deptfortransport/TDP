// ***********************************************
// NAME 		: SessionKey.cs
// AUTHOR 		: A Windley
// DATE CREATED : 02/07/2003
// DESCRIPTION 	: A repositry of all the type safe keys
// defined for use with the TDSessionManger
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/SessionKey.cs-arc  $
//
//   Rev 1.4   Aug 28 2012 10:19:58   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.3   Feb 17 2010 15:13:32   apatel
//International planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Dec 02 2009 15:32:08   mmodi
//Added key for transitioning from information to result page setting flag to show the map view
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.1   Nov 18 2009 11:20:32   apatel
//Updated for Journey input planner mapping enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.0   Nov 08 2007 12:41:10   mturner
//Initial revision.
//
//   Rev 1.29   Nov 14 2006 09:11:12   rbroddle
//Merge for stream4220
//
//   Rev 1.28.1.0   Nov 07 2006 11:18:20   tmollart
//Added new session keys for Rail Search By Price
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.28   Mar 23 2006 17:40:26   tmollart
//Manual merge of stream 0025.
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.27   Mar 22 2006 17:29:50   halkatib
//Changes due to Merge of stream3152 Landing Page phase 3
//
//   Rev 1.26   Mar 13 2006 15:48:34   echan
//stream3353 manual merge
//
//   Rev 1.24.2.0   Feb 13 2006 18:24:52   NMoorhouse
//One off keys to store journey selected leg for transition between RefineDetails and Refine Map pages.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.25   Jan 04 2006 10:06:22   tolomolaiye
//Updates folllowing Visit Planner code review
//
//   Rev 1.24   Dec 08 2005 12:31:00   jmcallister
//Fix for IR3320. New session keys added for use by Travel News Landing Page.
//
//   Rev 1.23   Nov 17 2005 13:50:22   kjosling
//Automatically merged from branch for stream2880
//
//   Rev 1.22.1.1   Nov 10 2005 09:28:22   RGriffith
//Extra session key to set JourneyParameters.PublicModes on the JourneyPlannerInput page from the PlanAJourneyControl
//
//   Rev 1.22.1.0   Nov 07 2005 18:45:16   RGriffith
//Stream2880 PreMerge for getting trunk changes into the stream
//
//   Rev 1.22   Nov 03 2005 10:25:10   jmcallister
//Manual merge for stream 2877. New session keys for Travel News Landing.
//
//   Rev 1.21   Oct 31 2005 14:18:22   tmollart
//Merge of stream 2638. 
//Resolution for 2929: Visit Planner Merge Activity
//
//   Rev 1.20   Sep 29 2005 18:14:00   asinclair
//Merge for 2610
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.19.1.0   Sep 14 2005 09:34:08   halkatib
//Added new Session keys for use with the Landing Page
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.19   Aug 19 2005 14:06:42   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.18.1.0   Jul 22 2005 19:57:02   RPhilpott
//Add key for ServiceDetails page.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.18   Feb 18 2005 19:39:24   rhopkins
//Added one-use keys for Buy Tickets buttons
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.17   Feb 18 2005 14:45:24   tmollart
//Added two new One Use Keys:
//- FindModeFare
//- FindModeTrunkCostBased
//
//   Rev 1.16   Nov 03 2004 14:54:46   Schand
//// added Seasonal Notice board
//
//   Rev 1.15   Nov 01 2004 15:29:16   passuied
//added 2 new OneUseKey NotFindAMode and ClassicMode
//
//   Rev 1.14   Aug 20 2004 12:14:44   jgeorge
//IR1338
//
//   Rev 1.13   Aug 03 2004 11:51:36   COwczarek
//Add NewSearch key
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.12   Jul 26 2004 20:16:46   passuied
//added a session key to flag an amend Search click
//
//   Rev 1.11   Jun 22 2004 19:09:08   RHopkins
//Changed declaration for ItineraryManagerModeChanged
//
//   Rev 1.10   Jun 04 2004 19:09:04   RHopkins
//Added a OneUse key to indicate that the ItineraryManager has changed "mode"
//
//   Rev 1.9   Apr 28 2004 16:19:46   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.8   Mar 10 2004 15:53:14   PNorell
//Updated for IR498 to be included in 5.2
//
//   Rev 1.7   Nov 03 2003 16:35:46   passuied
//implemented anchorage for Help and journeyPlannerInput
//
//   Rev 1.6   Sep 30 2003 11:32:58   PNorell
//Corrected printable outlook.
//Added support for moving outside the screen flow state as is needed by some printable pages.
//
//   Rev 1.5   Sep 26 2003 18:23:32   RPhilpott
//Add ForceRedirect flag for ScreenFlow
//
//   Rev 1.4   Sep 12 2003 11:27:12   PNorell
//Added nessecary key for detailed leg map.
//
//   Rev 1.3   Jul 29 2003 13:23:24   AWindley
//added a boolean key FeedbackSubmitted
//
//   Rev 1.2   Jul 17 2003 13:46:14   kcheung
//Added key defs for screenflow
//
//   Rev 1.1   Jul 04 2003 16:27:02   AWindley
//Removed empty constructor
//
//   Rev 1.0   Jul 03 2003 17:30:12   AWindley
//Initial Revision

using System;

namespace TransportDirect.UserPortal.Resource
{
	/// <summary>
	/// Global key repository where each key must be defined 
	/// prior to being used by the Session Manager.
	/// </summary>
	public class SessionKey
	{
		
		// Key Definitions
		// To add new key definitions, follow the example format:
		//
		//	public static readonly IntKey BaseFarePrice = new IntKey("BaseFarePrice");
		
		// The following keys are used by the landing page
		public static readonly BoolKey LandingPageCheck = new BoolKey("LANDING");
		public static readonly BoolKey LandingPageAutoPlan = new BoolKey("AUTOPLAN");
		public static readonly BoolKey LandingPageNewsShowNewsComplete = new BoolKey("SHOWNEWSCOMPLETE");
		public static readonly BoolKey LandingPageNewsRegionSelectComplete = new BoolKey("REGIONSELECTCOMPLETE");
		public static readonly BoolKey LandingPageBothDataNotNull = new BoolKey("BOTHDATANOTNULL");
		public static readonly StringKey LandingPageDestinationInputType = new StringKey("DESTINATIONINPUTTYPE");
		public static readonly StringKey LandingPageOriginInputType = new StringKey("ORIGININPUTTYPE");
		public static readonly StringKey LandingPageNewsRegionInputType = new StringKey("NEWSREGIONINPUTTYPE");
		public static readonly StringKey LandingPageNewsTransportInputType = new StringKey("NEWSTRANSPORTINPUTTYPE");
		public static readonly StringKey LandingPageNewsSeverityInputType = new StringKey("NEWSSEVERITYINPUTTYPE");
		public static readonly StringKey LandingPageNewsDisplayInputType = new StringKey("NEWSDISPLAYINPUTTYPE");
		
		// The following five keys are used by ScreenFlow.		
		public static readonly BoolKey Transferred   = new BoolKey("Transferred");
		public static readonly BoolKey ForceRedirect = new BoolKey("ForceRedirect");
		public static readonly PageIdKey NextPageId  = new PageIdKey("NextPageId");
		public static readonly TransitionEventKey TransitionEvent =
			new TransitionEventKey("TransitionEvent");
		public static readonly BoolKey SkipScreenFlow = new BoolKey("SkipScreenFlow");
		
		// This key is used by Feedback.
		public static readonly BoolKey FeedbackSubmitted = new BoolKey("FeedbackSubmitted");

		// This keys are used by the JourneyPlan output pages
		public static readonly BoolKey JourneyMapOutward = new BoolKey("JourneyMapOutward"); // DetailedLegMap

		// Indicates direction of selected leg to be used by ServiceDetails page
		public static readonly BoolKey JourneyDetailsOutward = new BoolKey("JourneyDetailsOutward"); 

		// used to anchor a position in a page
		public static readonly StringKey Anchor = new StringKey("Anchor");

		public static readonly OneUseKey IndirectLocationPostBack = new OneUseKey("IndirectLocationPostBack");

		// Indicates that the ItineraryManager mode has changed between No-Itinerary, Itinerary-only, or Itinerary-with-ExtendInProgress
		public static readonly BoolKey ItineraryManagerModeChanged = new BoolKey("ItineraryManagerModeChanged");

		// used to record user's name
		public static readonly StringKey Username = new StringKey("Username");

		// used to indicate an amendment is needed in FindStation
		public static readonly OneUseKey FindStationAmend = new OneUseKey("FindStationAmend");

        // used to indicate that new search button has been clicked on result page
        public static readonly OneUseKey NewSearch = new OneUseKey("NewSearch");

		// used to indicate that the journey results haven't been viewed yet
		public static readonly OneUseKey FirstViewingOfResults = new OneUseKey("FirstViewingOfResults");

		// used to indicate that FindStationInput in station mode should be displayed
		// (Code in TDPage to append query string to Url)
		public static readonly OneUseKey NotFindAMode = new OneUseKey("NotFindAMode");

		// used to indicate that FindTrunk is in classic mode (not station mode) should be displayed
		// (Code in TDPage to append query string to Url)
		public static readonly OneUseKey ClassicMode = new OneUseKey("ClassicMode");

		// Used for cost based searching and transistions between FindTrunkInput and FindFareInput
		public static readonly OneUseKey FindModeFare = new OneUseKey("FindModeFare");
		public static readonly OneUseKey FindModeTrunkCostBased = new OneUseKey("FindModeTrunkCostBased");

		// Used for cost based searching for transistions to TicketRetailers
		public static readonly OneUseKey FindFareBuySingleOrReturn = new OneUseKey("FindFareBuySingleOrReturn");
		public static readonly OneUseKey FindFareBuyOutwardSingle = new OneUseKey("FindFareBuyOutwardSingle");
		public static readonly OneUseKey FindFareBuyReturnSingle = new OneUseKey("FindFareBuyReturnSingle");
		public static readonly OneUseKey FindFareBuyBothSingle = new OneUseKey("FindFareBuyBothSingle");

		// Added for FindAPlaceControl
		public static readonly OneUseKey FindALocationFromHomePage = new OneUseKey("FindALocationFromHomePage");

		// Added for PlanAJourneyControl setting of JourneyParametersPublicModes
		public static readonly OneUseKey PublicModesRequired = new OneUseKey("PublicModesRequired");
        public static readonly OneUseKey ExpandOptionsRequired = new OneUseKey("ExpandOptionsRequired");
		
		// Added for Extend, Replan and Adjust setting of Outward Map Leg Selection
		public static readonly OneUseKey MapOutwardSelectedLeg = new OneUseKey("MapOutwardSelectedLeg");

		// Added for Extend, Replan and Adjust setting of Return Map Leg Selection
		public static readonly OneUseKey MapReturnSelectedLeg = new OneUseKey("MapReturnSelectedLeg");

		// Added of Park and Ride setting of ParkAndRideDestination
		public static readonly OneUseKey ParkAndRideDestination = new OneUseKey("ParkAndRideDestination");

		// Added for Rail Search by Price and used for determining if the user has switched
		// between time and cost based planning modes.
		public static readonly OneUseKey TransferredFromTimeBasedPlanning = new OneUseKey("TransferredFromTimeBasedPlanning");
		public static readonly OneUseKey TransferredFromCostBasedPlanning = new OneUseKey("TransferredFromCostBasedPlanning");

        public static readonly OneUseKey JourneyPlannerInputToMap = new OneUseKey("JourneyPlannerInputToMap");

        // Added for door to door link on journey details page when International journey planned
        // This causes journey planner input page to skip the initialisation of journey parameters
        public static readonly OneUseKey InternationalPlannerInput = new OneUseKey("InternationalPlannerInput");

        // Added to indicate to a page which supports different views, to show the Map view, e.g. for VisitPlannerResults
        public static readonly OneUseKey MapView = new OneUseKey("MapView");
	}
}
