// *********************************************** 
// NAME                 : JourneyMap.aspx.cs
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 30/09/2003
// DESCRIPTION			: Journey Map Page
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/JourneyMap.aspx.cs-arc  $
//
//   Rev 1.20   Oct 04 2011 15:44:42   mmodi
//Updated to set the road journey allow replan flag when a replan fails (due to validation and hence not submitted to cjp manager)
//Resolution for 5748: Real Time in Car - Replan is button displayed when no journeys can be planned
//
//   Rev 1.19   Oct 04 2011 13:37:18   MTurner
//Added missing using statement
//Resolution for 5747: Real Time In Car - Replan fails if map is showing
//
//   Rev 1.18   Oct 04 2011 10:59:00   mturner
//Added event handlers for the replan events.
//Resolution for 5747: Real Time In Car - Replan fails if map is showing
//
//   Rev 1.17   Sep 21 2011 09:53:28   mmodi
//Corrected to show last car journey planned when no more routes using the replan to avoid incidents
//Resolution for 5739: Real Time In Car - Failed journey Replan does not display last journey
//
//   Rev 1.16   Sep 01 2011 10:44:50   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.15   Mar 30 2010 10:16:50   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.14   Mar 19 2010 10:50:40   apatel
//Added custom related links for city to city result pages
//Resolution for 5468: Related link in city to city incorrect
//
//   Rev 1.13   Dec 10 2009 11:08:22   mmodi
//Fixed return cycle journey maps and set up cycle directions table links to map
//
//   Rev 1.12   Dec 02 2009 12:19:12   mmodi
//Updated to display map direction number link on Car journey details table
//
//   Rev 1.11   Nov 29 2009 12:44:40   mmodi
//Updated map initialise to display the show journey buttons, and ensure only  one map is shown
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.10   Nov 20 2009 09:28:10   apatel
//updated for map enhancement when javascript is disabled
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.9   Nov 16 2009 17:07:20   apatel
//Updated for mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.8   Nov 15 2009 18:17:48   mmodi
//Updated styles to make results pages look consistent
//
//   Rev 1.7   Nov 11 2009 18:39:28   mmodi
//Updated to use new MapJourneyControl
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.6   Oct 21 2009 11:18:26   PScott
//Add social bookmark links to Summary,maps, and fares screen
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.5   Oct 15 2008 17:14:02   mmodi
//Updated help for cycle journey
//
//   Rev 1.4   Oct 13 2008 16:44:28   build
//Automatically merged from branch for stream5014
//
//   Rev 1.3.1.4   Oct 13 2008 10:38:26   mmodi
//Updated page id for cycle mode
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3.1.3   Sep 18 2008 11:39:58   mmodi
//Do not sure Map directions button on Cycle Details page
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3.1.2   Sep 16 2008 10:52:32   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3.1.1   Aug 22 2008 10:37:18   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3.1.0   Jun 20 2008 14:29:02   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3.1.0   Jun 20 2008 14:17:40   mmodi
//Updated to detect cycle journey
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   May 07 2008 14:50:24   apatel
//made tiltle "Journey Found For" to look in table view when findamode is flight.
//
//   Rev 1.2   Mar 31 2008 13:24:54   mturner
//Drop3 from Dev Factory
//
//
//  Rev Devfactory Mar 28 2008 08:40:00 apatel
//  added styles for the park and ride page and car route page through code.
//
//  Rev Devfactory Mar 18 2008 12:21:00 apatel
//  SetControlVisibilities method modified to show overview map by default
//
//   Rev 1.0   Nov 08 2007 13:30:04   mturner
//Initial revision.
//
//   Rev DevFactory Jan 20 2008 19:00:00 dgath
//CCN0382b City to City enhancements:
//Updated to pass mode type to FindSummaryResultControl selected on 
//JourneyOverview page in City to City journeys. ModeType value obtained from Session.FindPageState.ModeType
//
//   Rev 1.86   Sep 10 2007 16:47:18   mmodi
//Changes to populate the car details with journey parameters
//
//   Rev 1.85   Jul 11 2006 14:24:30   mmodi
//Fix for vantive: 4357031.
//Amended condition to ensure outward map is refreshed when an alternate journeys radio button is selected.
//Resolution for 4135: Map not updated when a different journey is selected
//
//   Rev 1.84   Jun 14 2006 12:04:34   esevern
//Code fix for vantive 3918704 - Enable buttons when no outward journey is returned but a return journey is. Changed code to remove assumption that there is always an outward journey.
//Resolution for 3686: Buttons disabled when return journey returned and outward not
//
//   Rev 1.83   Apr 11 2006 13:38:18   mtillett
//Move population of TDImage properties outside postback condition
//Resolution for 3876: Regr: Missing arrow image on Journey Details - Maps page
//
//   Rev 1.82   Apr 05 2006 15:43:02   build
//Automatically merged from branch for stream0030
//
//   Rev 1.81.1.0   Mar 30 2006 16:52:30   esevern
//Amended to show outward and return display numbers if this is a FindBus journey result
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.81   Mar 20 2006 18:15:40   pcross
//Code review updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.80   Mar 14 2006 09:27:12   asinclair
//Mannual merge for stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.79   Feb 23 2006 19:08:12   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.78   Feb 21 2006 11:43:12   aviitanen
//Merge from stream0009 
//
//   Rev 1.77   Feb 10 2006 12:24:52   tolomolaiye
//Merge for stream 3180 - Homepage Phase 2
//
//   Rev 1.76   Dec 13 2005 11:21:34   asinclair
//Merge for stream3143
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.75.2.1   Dec 12 2005 16:53:50   tmollart
//Removed code to reinstate journey results.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.75.2.0   Nov 30 2005 14:55:42   RGriffith
//Changes applied for using new HeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.75.1.0   Nov 23 2005 11:26:10   jgeorge
//Added client links functionality
//Resolution for 3144: DEL 8 stream: Client Links Development
//
//   Rev 1.75   Nov 03 2005 16:02:16   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.74.1.1   Oct 24 2005 14:02:52   RGriffith
//Changes to accomodate AmendViewControl changes
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.74.1.0   Oct 12 2005 10:51:54   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.75   Oct 10 2005 17:33:24   RGriffith
//Replacing the image button with HTML button
//
//   Rev 1.74   Sep 29 2005 12:53:08   build
//Automatically merged from branch for stream2673
//
//   Rev 1.73.1.1   Sep 13 2005 09:55:42   rgreenwood
//DN079 UEE TD088 JourneyExtension Tracking: Added extra SetControlVisibilities() call to Page_Load. OutputNavigationControl was not being assigned pageID otherwise
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.73.1.0   Sep 07 2005 13:13:44   rgreenwood
//DN079 UEE ES015 24 Hour Help Button removal
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.73   Aug 17 2005 13:55:42   RWilby
//Merge Fix for stream2556
//
//   Rev 1.72   Aug 16 2005 14:38:34   RWilby
//Merge for stream2556
//
//   Rev 1.71   Aug 04 2005 13:07:06   asinclair
//fix for IR 2639
//Resolution for 2639: DEl 7: Car Costing: Do not display Congestion Charge for return journeys
//
//   Rev 1.70   May 23 2005 17:20:48   rgreenwood
//IR2500: Added FindAFare back button
//Resolution for 2500: PT - Back Button Missing in Find-a-Fare
//
//   Rev 1.69   May 20 2005 09:45:24   rgeraghty
//Tidied up printerfriendlypagebutton code in OnPreRender
//Resolution for 2493: Del 7 Car - Server error in '/Web' Application on printer friendly page
//
//   Rev 1.68   Apr 29 2005 14:26:02   rscott
//Further changes for IR1984
//
//   Rev 1.67   Apr 29 2005 11:53:12   pcross
//IR2282. 24 hr clock button no longer shifted to right when extending journey.
//Resolution for 2282: Extend Journey Results Map Page 24 Hour Button Docks Right Not Left
//
//   Rev 1.66   Apr 25 2005 19:25:36   asinclair
//Fix for IR 1983
//
//   Rev 1.65   Apr 22 2005 16:23:08   pcross
//IR2192. Now hands off the processing to the JourneyMapControl wrt showing a selected leg of a journey.
//Resolution for 2192: Display normal map page, add numbered start and end legs
//
//   Rev 1.64   Apr 20 2005 12:10:42   COwczarek
//Itinerary manager instance variable reassigned in pre-render
//event handler since partition may have been switched by user.
//PlannerOutputAdapter instantiated when needed to ensure it
//uses itinerary manager from correct partition.
//Resolution for 2079: PT - Extend journey does not work with PT cost based searches
//
//   Rev 1.63   Apr 19 2005 16:07:32   pcross
//IR1973. Skip link corrections for errors found on unit test.
//
//   Rev 1.62   Apr 13 2005 15:17:56   pcross
//Changed the way that the skip links image URL is accessed (now from langStrings, not HTML)
//
//   Rev 1.61   Apr 13 2005 14:54:58   asinclair
//Added fix for 2044
//
//   Rev 1.60   Apr 12 2005 12:49:50   pcross
//Added skip links
//
//   Rev 1.59   Apr 12 2005 10:59:20   bflenk
//Work in Progress - IR 1986
//
//   Rev 1.58   Apr 08 2005 16:46:04   rhopkins
//Corrected error with return hyperlink visibility
//
//   Rev 1.57   Apr 08 2005 16:02:10   rhopkins
//Added FindFareSelectedTicketLabelControl and AmendViewControl
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.56   Mar 18 2005 15:07:44   asinclair
//Updated to display car section maps.
//
//   Rev 1.55   Mar 01 2005 16:29:30   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.54   Mar 01 2005 15:32:20   asinclair
//Updated For Del 7 Car costing
//
//   Rev 1.53   Sep 27 2004 15:37:08   esevern
//added call to ReinstateParametersForResults on pageload
//
//   Rev 1.52   Sep 21 2004 16:38:32   esevern
//IR1581 - amended outward and return journey label for find a car (there will only be one journey)
//
//   Rev 1.51   Sep 20 2004 16:45:26   COwczarek
//Page title now displayed using JourneyPlannerOutputTitleControl.
//Resolution for 1505: Extend Journey interface in relation to the Outward itinerary
//
//   Rev 1.50   Sep 20 2004 12:23:58   jbroome
//IR 1533 - Ensure correct Map is displayed after returning from More Help...
//IR 1578 - Ensure correct Map is displayed for FInd A return journeys.
//
//   Rev 1.49   Sep 19 2004 15:04:20   jbroome
//IR1391 - visibility of Add Extension to Journey button if incomplete results returned.
//
//   Rev 1.48   Aug 31 2004 15:06:44   RHopkins
//IR1440 Rationalised the code for determining the state of the result data.  This improved code has been duplicated across all of the results pages to reduce the burden of future maintenance.
//
//   Rev 1.47   Aug 26 2004 14:25:12   RHopkins
//IR1382 Reinstated the assignment of the commandSubmit ClickEventHandler that had got removed during recent changes.
//
//   Rev 1.46   Aug 26 2004 12:39:32   RHopkins
//IR1443 Corrected results-state checking and visibility handling so that the controls are displayed correctly.
//
//   Rev 1.45   Aug 23 2004 11:08:38   jgeorge
//IR1319
//
//   Rev 1.44   Aug 10 2004 15:45:02   JHaydock
//Update of display of correct header control for help pages.
//
//   Rev 1.43   Jul 30 2004 16:43:42   RHopkins
//IR1113 The "Amend date/time" anchor link now varies its text depending upon whether the AmendSaveSend control is displaying "Amend date and time" or "Amend stopover time" or not displaying either.
//
//   Rev 1.42   Jul 22 2004 11:12:42   jgeorge
//Add find a functionality for Del 6.1
//
//   Rev 1.41   Jun 22 2004 16:46:36   jbroome
//Ensured correct journey label text displayed above summary tables.
//
//   Rev 1.40   Jun 22 2004 13:33:24   jbroome
//Corrected inputpagestate error.
//
//   Rev 1.39   Jun 17 2004 17:29:04   RHopkins
//Corrected state handling for Itinerary
//
//   Rev 1.38   Jun 07 2004 15:33:10   jbroome
//ExtendJourney - added controls and support needed for TDItineraryManager.
//
//   Rev 1.37   Apr 05 2004 14:20:32   CHosegood
//Added ExtraWiringEvents method back to onInit
//Resolution for 714: Clicking "Amend journey" or "New search" from map page has no effect
//
//   Rev 1.36   Apr 02 2004 10:16:30   AWindley
//DEL 5.2 QA Changes: Resolution for 692
//
//   Rev 1.35   Mar 16 2004 09:43:16   PNorell
//Updated to remember positions.
//
//   Rev 1.34   Mar 15 2004 18:13:02   CHosegood
//Del 5.2 map changes
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.33   Mar 12 2004 17:20:46   CHosegood
//Del5.2 Map changes::Check-in for integration
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.32   Mar 01 2004 13:47:00   asinclair
//Removed Printer hyperlink as not needed in Del 5.2
//
//   Rev 1.31   Nov 21 2003 09:45:36   kcheung
//Updated for the return requested but no outward journey found case.
//
//   Rev 1.30   Nov 17 2003 16:01:56   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.29   Nov 13 2003 12:46:12   kcheung
//Updated to use the output navigation control.
//
//   Rev 1.28   Nov 07 2003 11:29:18   PNorell
//Fixed n*mespace comment confusing NAnt.
//
//   Rev 1.27   Nov 05 2003 10:46:02   kcheung
//Inserted : as requested in QA
//
//   Rev 1.26   Nov 04 2003 13:53:48   kcheung
//Updated n*mespace to Web.Templates
//
//   Rev 1.25   Nov 03 2003 17:26:20   kcheung
//Updated so that Ticket Retailers and Fares button are greyed-out if car is selected.
//
//   Rev 1.24   Oct 28 2003 15:00:56   kcheung
//Removed show label and changed font size for QA
//
//   Rev 1.23   Oct 28 2003 10:19:30   CHosegood
//Added Fares button event handler
//
//   Rev 1.22   Oct 27 2003 18:41:26   COwczarek
//Add event handler to redirect to ticket retailers page
//
//   Rev 1.21   Oct 20 2003 18:22:22   kcheung
//Fixed for FXCOP
//
//   Rev 1.20   Oct 20 2003 17:11:44   kcheung
//Updated variable names for compliance with FXCOP
//
//   Rev 1.19   Oct 15 2003 13:25:04   kcheung
//Fixed HTML
//
//   Rev 1.18   Oct 14 2003 19:12:32   kcheung
//Fixed so that the return hyperlink is displayed only if a return journey exists.
//
//   Rev 1.17   Oct 13 2003 12:43:04   kcheung
//Fixed ALT text
//
//   Rev 1.16   Oct 10 2003 17:45:02   kcheung
//Updated alt text
//
//   Rev 1.15   Oct 10 2003 10:48:38   kcheung
//Fixed page title to read from langstrings
//
//   Rev 1.14   Oct 09 2003 16:26:42   kcheung
//Updated the initialise 
//
//   Rev 1.13   Oct 08 2003 13:12:58   kcheung
//Added Map Exception handling code.
//
//   Rev 1.12   Oct 08 2003 10:18:18   PNorell
//Removed exception throwing if a result contains no journeys.
//
//   Rev 1.11   Oct 03 2003 14:21:54   kcheung
//Uncommented call to InputPageState.Initialise()
//
//   Rev 1.10   Oct 01 2003 16:14:38   kcheung
//Added commented out code to call Initialise of InputPageState
//
//   Rev 1.9   Sep 30 2003 14:23:20   kcheung
//Integrated all HTML stuff

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web;
using System.Web.SessionState;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.JourneyPlanning.CJPInterface;
using System.Collections.Generic;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Journey Map Page
	/// </summary>
	public partial class JourneyMap : TDPage
    {
        #region Private members
        protected ResultsTableTitleControl resultsTableTitleControlOutward;
		protected ResultsTableTitleControl resultsTableTitleControlReturn;
		protected FindFareSelectedTicketLabelControl findFareSelectedTicketLabelControl;
		protected FindFareGotoTicketRetailerControl findFareGotoTicketRetailerControl;
		protected SummaryResultTableControl summaryResultTableControlOutward;
		protected SummaryResultTableControl summaryResultTableControlReturn;
		protected FindSummaryResultControl findSummaryResultTableControlOutward;
		protected FindSummaryResultControl findSummaryResultTableControlReturn;
		protected OutputNavigationControl theOutputNavigationControl;
		protected JourneyChangeSearchControl theJourneyChangeSearchControl;
		protected AmendSaveSendControl amendSaveSendControl;
		protected HeaderControl headerControl;
		protected ResultsFootnotesControl footnotesControl;

		protected CarAllDetailsControl carAllDetailsControlOutward;
		protected CarAllDetailsControl carAllDetailsControlReturn;
        
		protected System.Web.UI.WebControls.Label hyperLinkAmendDateTime;

		protected System.Web.UI.WebControls.Label labelDetailsReturnJourney;
		protected System.Web.UI.WebControls.Label labelCarReturn;

		private const string TOID_PREFIX = "JourneyControl.ToidPrefix";
		private const string MAP_ZOOM = "JourneyDetailsCarSection.Scale";

		private ITDSessionManager tdSessionManager;
		private TDItineraryManager itineraryManager;
		private MapHelper mapHelper;

		private TrackingControlHelper trackingHelper;
        private RoadJourneyHelper roadJourneyHelper;

		// State of results
		/// <summary>
		///  True if there is an outward trip for the current selection (Journey, Itinerary or Extension)
		/// </summary>
		private bool outwardExists = false;

		/// <summary>
		///  True if there is a return trip for the current selection (Journey, Itinerary or Extension)
		/// </summary>
		private bool returnExists = false;

		/// <summary>
		/// True if the Itinerary exists, containing the Initial journey and zero or more extensions
		/// </summary>
		private bool itineraryExists = false;

		/// <summary>
		/// True if an extension to an Itinerary is in the process of being planned and has not yet been added to the Itinerary
		/// </summary>
		private bool extendInProgress = false;

		/// <summary>
		/// True if the Itinerary exists and there are no extensions in the process of being planned
		/// </summary>
		private bool showItinerary = false;

		/// <summary>
		/// True if the results have been planned using FindA
		/// </summary>
		private bool showFindA = false;

		private bool returnArriveBefore = false;
		private bool outwardArriveBefore = false;

        #endregion

        #region Constructor / Page Load

        /// <summary>
		/// Constructor - sets the page id.
		/// </summary>
		public JourneyMap() : base()
		{
            pageId = PageId.JourneyMap;
		}

		/// <summary>
		/// Page Load Method
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            tdSessionManager = TDSessionManager.Current;
			itineraryManager = TDItineraryManager.Current;
			
			// Add Intellitracker tags for road replan journey clicked
            if (tdSessionManager.JourneyViewState.OutwardRoadReplanned)
            {
                AddReplanAvoidClosuresParam(false);
            }

            if (tdSessionManager.JourneyViewState.ReturnRoadReplanned)
            {
                AddReplanAvoidClosuresParam(true);
            }

            // Reset the journey view state fields
            tdSessionManager.JourneyViewState.OutwardRoadReplanned = false;
            tdSessionManager.JourneyViewState.ReturnRoadReplanned = false;

            // Override the PageId if we're in cycle mode
            if (tdSessionManager.FindAMode == FindAMode.Cycle)
            {
                pageId = PageId.CycleJourneyMap;
            }

			tdSessionManager.JourneyViewState.CongestionChargeAdded = false;
			tdSessionManager.JourneyViewState.VisitedCongestionCompany.Clear();

            //Added css to headElementControl for park and ride page
            if (tdSessionManager.FindAMode == FindAMode.ParkAndRide)
                headElementControl.Stylesheets += ",JourneyMapParkAndRide.aspx.css";

            //Added css to headElementControl for car route page
            if (tdSessionManager.FindAMode == FindAMode.Car)
                headElementControl.Stylesheets += ",JourneyMapCar.aspx.css";

            //Added css to headElementControl for cycle journey page
            if (tdSessionManager.FindAMode == FindAMode.Cycle)
                headElementControl.Stylesheets += ",CyclePlanner.css,JourneyMapCycle.aspx.css";

            this.PageTitle = GetResource("JourneyPlanner.JourneyMapsPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

			mapHelper = new MapHelper();

			carAllDetailsControlOutward.Printable = true;
			carAllDetailsControlReturn.Printable = true;

            RoadJourneyHelper roadJourneyHelper = new RoadJourneyHelper();

            bool showTravelNewsIncidents = true;
            
            if (mapHelper.PrivateOutwardJourney && journeyDetailsShowControlOutward.DirectionsVisible)
			{
				bool outward = true;

                RoadJourney roadJourney = itineraryManager.JourneyResult.OutwardRoadJourney();

                if (!roadJourney.JourneyMatchedForTravelNewsIncidents)
                {
                    roadJourneyHelper.ProcessRoadJourneyForTravelNewsIncidents(roadJourney);
                }

				carAllDetailsControlOutward.Initialise(outward, tdSessionManager.JourneyParameters as TDJourneyParametersMulti, showTravelNewsIncidents, 
                    roadJourney.HasClosure && roadJourney.AllowReplan);
                carAllDetailsControlOutward.SetMapProperties(true, mapJourneyControlOutward.MapId,
                    mapJourneyControlOutward.MapJourneyDisplayDetailsDropDownId, mapJourneyControlOutward.FirstElementId,
                    Session.SessionID);
			}

			if (mapHelper.PrivateReturnJourney && journeyDetailsShowControlReturn.DirectionsVisible)
			{
				bool outward = false;

                RoadJourney roadJourney = itineraryManager.JourneyResult.ReturnRoadJourney();

                if (!roadJourney.JourneyMatchedForTravelNewsIncidents)
                {
                    roadJourneyHelper.ProcessRoadJourneyForTravelNewsIncidents(roadJourney);
                }

                carAllDetailsControlReturn.Initialise(outward, tdSessionManager.JourneyParameters as TDJourneyParametersMulti, showTravelNewsIncidents, 
                    roadJourney.HasClosure && roadJourney.AllowReplan);
                carAllDetailsControlReturn.SetMapProperties(true, mapJourneyControlReturn.MapId,
                    mapJourneyControlReturn.MapJourneyDisplayDetailsDropDownId, mapJourneyControlReturn.FirstElementId,
                    Session.SessionID);
			}

            LoadHelp();

            if (tdSessionManager.FindAMode == FindAMode.Flight)
                JourneysSearchedForControl1.IsTableView = true;

            headElementControl.ImageSource = GetModeImageSource();
            headElementControl.Desc = JourneysSearchedForControl1.ToString();

            socialBookMarkLinkControl.BookmarkDescription = JourneysSearchedForControl1.ToString();
            socialBookMarkLinkControl.EmailLink.NavigateUrl = Request.Url.AbsoluteUri + "#JourneyOptions";

			SetControlVisibilities();

			//Initialise Hyperlink and Image properties as view state disabled
			InitialiseHyperlinkImage();

            InitialiseStaticLabels();

            if (tdSessionManager.FindPageState != null)
                JourneyPlannerOutputTitleControl1.ModeTypes = tdSessionManager.FindPageState.ModeType;
                        
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
            if (tdSessionManager.FindAMode == FindAMode.Trunk)
            {
                expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneyMapFindTrunkInput);
            }
            else
            {
                expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneyMap);
            }
            expandableMenuControl.AddExpandedCategory("Related links");
		}


		#endregion

        #region Page PreRender / OnUnload
        /// <summary>
		/// OnPreRender method. Sets dynamic text and calls base OnPreRender.
		/// </summary>
		protected override void OnPreRender(EventArgs e)
		{
            itineraryManager = TDItineraryManager.Current;

			//Find out what the current units are for Car directions and set PrinterFriendly
			//Url params.
			string  printerUnits = tdSessionManager.InputPageState.Units.ToString();
			string url = Global.tdResourceManager.GetString
				("JourneyPlanner.UrlPrintableJourneyDetails", TDCultureInfo.CurrentUICulture);
			
			// If the screen is switching in either direction between the display of normal results and
			// the display of the Itinerary then the controls must be reset
			if (itineraryManager.ItineraryManagerModeChanged)
			{
				SetControlVisibilities();
			}

            SetJourneyDetailsShowControl();

			// Prerender setup for the AmendSaveSend control and its child controls
            PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(tdSessionManager);

			plannerOutputAdapter.AmendSaveSendControlPreRender(amendSaveSendControl);

			plannerOutputAdapter.PopulateResultsTableTitles(resultsTableTitleControlOutward, resultsTableTitleControlReturn);

            #region Display Road journey directions
            RoadJourneyHelper roadJourneyHelper = new RoadJourneyHelper();

            bool showTravelNewsIncidents = true;

            if (mapHelper.PrivateOutwardJourney && journeyDetailsShowControlOutward.DirectionsVisible)
			{
				directionsPanel.Visible = true;

                RoadJourney roadJourney = TDItineraryManager.Current.JourneyResult.OutwardRoadJourney();

                if (!roadJourney.JourneyMatchedForTravelNewsIncidents)
                {
                    roadJourneyHelper.ProcessRoadJourneyForTravelNewsIncidents(roadJourney);
                }
                
                carAllDetailsControlOutward.Initialise(true, tdSessionManager.JourneyParameters as TDJourneyParametersMulti, showTravelNewsIncidents, 
                    roadJourney.HasClosure && roadJourney.AllowReplan);
                carAllDetailsControlOutward.SetMapProperties(true, mapJourneyControlOutward.MapId,
                    mapJourneyControlOutward.MapJourneyDisplayDetailsDropDownId, mapJourneyControlOutward.FirstElementId,
                    Session.SessionID);
			}
			else
			{
				directionsPanel.Visible = false;
			}

			if (mapHelper.PrivateReturnJourney && journeyDetailsShowControlReturn.DirectionsVisible)
			{
				directionsPanelReturn.Visible = true;
                
                RoadJourney roadJourney = TDItineraryManager.Current.JourneyResult.ReturnRoadJourney();

                if (!roadJourney.JourneyMatchedForTravelNewsIncidents)
                {
                    roadJourneyHelper.ProcessRoadJourneyForTravelNewsIncidents(roadJourney);
                }

                carAllDetailsControlReturn.Initialise(false, tdSessionManager.JourneyParameters as TDJourneyParametersMulti, showTravelNewsIncidents, 
                    roadJourney.HasClosure && roadJourney.AllowReplan);
                carAllDetailsControlReturn.SetMapProperties(true, mapJourneyControlReturn.MapId,
                    mapJourneyControlReturn.MapJourneyDisplayDetailsDropDownId, mapJourneyControlReturn.FirstElementId,
                    Session.SessionID);
			}
			else
			{
				directionsPanelReturn.Visible = false;
            }
            #endregion

            #region Display Cycle journey directions

            directionsCyclePanel.Visible = false;

            if (mapHelper.CycleOutwardJourney)
            {
                // Display the directions
                if (journeyDetailsShowControlOutward.DirectionsVisible)
                {
                    directionsCyclePanel.Visible = true;
                    cycleAllDetailsControlOutward.Visible = true;
                    cycleAllDetailsControlOutward.ButtonShowMap.Visible = false;

                    cycleAllDetailsControlOutward.Initialise(mapHelper.FindRelevantJourney(true) as CycleJourney,
                        true,
                        tdSessionManager.JourneyParameters as TDJourneyParametersMulti,
                        tdSessionManager.JourneyViewState);

                    cycleAllDetailsControlOutward.SetMapProperties(true, mapJourneyControlOutward.MapId,
                                    mapJourneyControlOutward.MapJourneyDisplayDetailsDropDownId,
                                    mapJourneyControlOutward.FirstElementId, Session.SessionID);
                }
            }

            directionsCyclePanelReturn.Visible = false;

            if (mapHelper.CycleReturnJourney)
            {
                // Display the directions
                if (journeyDetailsShowControlReturn.DirectionsVisible)
                {
                    directionsCyclePanelReturn.Visible = true;
                    cycleAllDetailsControlReturn.Visible = true;
                    cycleAllDetailsControlReturn.ButtonShowMap.Visible = false;

                    cycleAllDetailsControlReturn.Initialise(mapHelper.FindRelevantJourney(false) as CycleJourney,
                        false,
                        tdSessionManager.JourneyParameters as TDJourneyParametersMulti,
                        tdSessionManager.JourneyViewState);

                    cycleAllDetailsControlReturn.SetMapProperties(true, mapJourneyControlReturn.MapId,
                                    mapJourneyControlReturn.MapJourneyDisplayDetailsDropDownId,
                                    mapJourneyControlReturn.FirstElementId, Session.SessionID);
                }
            }
            #endregion

            SetupSkipLinksAndScreenReaderText();

            CheckJavascriptEnabled();

            SetPrintableControl();

			base.OnPreRender(e);
		}

		override protected void OnUnload(EventArgs e)
		{
			base.OnUnload(e);
        }

        #endregion

        #region Visibility
        /// <summary>
		/// Sets the visibilities of the "Outward" components.
		/// </summary>
		// This has been added as part of a fix for vantive 3918704
		private void HideOutwardComponents()
		{
			outwardPanel.Visible = false;
			summaryResultTableControlOutward.Visible = false;
			findSummaryResultTableControlOutward.Visible = false;
			hyperLinkOutwardJourneys.Visible = false;
			hyperLinkImageOutwardJourneys.Visible = false;
		}

		/// <summary>
		/// Sets the visibilities of the "Return" components.
		/// </summary>
		private void HideReturnComponents()
		{
			returnPanel.Visible = false;
			returnPanel2.Visible = false;
			summaryResultTableControlReturn.Visible = false;
			findSummaryResultTableControlReturn.Visible = false;
			hyperLinkTagReturnJourneys.Visible = false;
		}

		/// <summary>
		/// Sets the visibility of controls depending on whether we are displaying FindFare results
		/// </summary>
		/// <param name="findFare">True if we are displaying FindFare results</param>
		private void SetFindFareVisible(bool findFare)
		{
            selectedTicketRow.Visible = findFare;
			selectedTicketCell.Visible = findFare;
			findFareSelectedTicketLabelControl.Visible = findFare;
			findFareGotoTicketRetailerControl.Visible = findFare;

            panelFindFareSteps.Visible = findFare;

            theJourneyChangeSearchControl.GenericBackButtonVisible = findFare;
        }
        #endregion

        #region SetupControls

        /// <summary>
		/// Establish what mode the Itinerary Manager is in and whether we have any Return results
		/// </summary>
		private void DetermineStateOfResults()
		{
			itineraryExists = (itineraryManager.Length > 0);
			extendInProgress = itineraryManager.ExtendInProgress;
			showItinerary = (itineraryExists && !extendInProgress);
			showFindA = (!showItinerary && (tdSessionManager.IsFindAMode));

			if ( showItinerary )
			{
				outwardExists = (itineraryManager.OutwardLength > 0);
				returnExists = (itineraryManager.ReturnLength > 0);
			}
			else
			{
                //check for cycle result
                PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(tdSessionManager);
                outwardExists = plannerOutputAdapter.CycleExists(true);
                returnExists = plannerOutputAdapter.CycleExists(false);

				//check for normal result
				ITDJourneyResult result = tdSessionManager.JourneyResult;
				if(result != null) 
				{
					outwardExists = (((result.OutwardPublicJourneyCount + result.OutwardRoadJourneyCount) > 0) && result.IsValid) || outwardExists;
					returnExists = (((result.ReturnPublicJourneyCount + result.ReturnRoadJourneyCount) > 0) && result.IsValid) || returnExists;

					// Get time types for journey.
					outwardArriveBefore = tdSessionManager.JourneyViewState.JourneyLeavingTimeSearchType;
					returnArriveBefore = tdSessionManager.JourneyViewState.JourneyReturningTimeSearchType;
				}
			}
		}

		private void SetControlVisibilities()
		{
			DetermineStateOfResults();

			#region Initialise Controls
			// Initialise web user controls
			if (showFindA)
			{
                ModeType[] modeTypes = null;
                if (tdSessionManager.FindPageState != null)
                    modeTypes = tdSessionManager.FindPageState.ModeType;
                
                if (outwardExists)
				{
					findSummaryResultTableControlOutward.Initialise(false, true, outwardArriveBefore,modeTypes);
				}

				if (returnExists)
				{
					findSummaryResultTableControlReturn.Initialise(false, false, returnArriveBefore,modeTypes);
				}
			}
			else 
			{
				if (outwardExists)
				{
					summaryResultTableControlOutward.Initialise(false, true, outwardArriveBefore);
				}

				if (returnExists)
				{
					summaryResultTableControlReturn.Initialise(false, false, returnArriveBefore);
				}
			}


			// Setup the AmendSaveSend control and its child controls
            PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(tdSessionManager);

			plannerOutputAdapter.AmendSaveSendControlPageLoad(amendSaveSendControl, this.pageId);

			theOutputNavigationControl.Initialise(this.pageId);

			if (outwardExists)
			{
                mapJourneyControlOutward.Initialise(true, true, (outwardExists && returnExists));
			}

			if (returnExists)
			{
                mapJourneyControlReturn.Initialise(false, true, (outwardExists && returnExists));
			}

			#endregion Initialise Controls

			// Hide footnotes if no journey results
			if (!outwardExists && !returnExists)
			{
				footnotesControl.Visible = false;
			}


			returnPanel.CssClass = "boxtypeeighteen";
			summaryReturnTable.CssClass = (showFindA ? "jpsumrtnfinda" : "jpsumrtn");
			summaryOutwardTable.Attributes.Add("class", showFindA ? "jpsumoutfinda" : "jpsumout");

			if(outwardExists)
			{
				summaryResultTableControlOutward.Visible = !showFindA;
				findSummaryResultTableControlOutward.Visible = showFindA;
			}
	
			if(returnExists)
			{
				summaryResultTableControlReturn.Visible = !showFindA;
				findSummaryResultTableControlReturn.Visible = showFindA;
			}

			if (!itineraryExists && FindInputAdapter.IsCostBasedSearchMode(tdSessionManager.FindAMode))
			{
				// Set up controls that are visible in FindAFare mode
				SetFindFareVisible(true);
				findFareSelectedTicketLabelControl.PageState = tdSessionManager.FindPageState;
				findFareGotoTicketRetailerControl.PageState = tdSessionManager.FindPageState;

                SetUpStepsControl();
			}
			else
			{
				SetFindFareVisible(false);
			}

			if (!outwardExists)
			{
				// Outward results DO NOT exist. Set visibility of all outward controls to false.
				// This has been added as part of a fix for vantive 3918704
				HideOutwardComponents();
			}

			if (!returnExists)
			{
				// Return results DO NOT exist. Set visibility of all return controls to false.
				HideReturnComponents();
			}

            // Only allow outward OR return map to be shown, not both because the new 
            // AJAX maps only work for one map on the screen
            if (outwardExists && returnExists)
            {
                // Check view state to determine which is the initial map to show
                TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;

                // First time page is entered, set to show outward map
                if (!Page.IsPostBack)
                {
                    viewState.OutwardMapSelected = true;
                    viewState.ReturnMapSelected = false;
                }

                // Outward map should be shown in preference to return map
                panelMapJourneyControlOutward.Visible = true;
                panelMapJourneyControlReturn.Visible = false;

                // Check which map was selected from Details page
                if (viewState.OutwardShowMap)
                {
                    // Enter here to prevent return map being set if both outward and return flags are true
                }
                else if ((viewState.ReturnShowMap) || (viewState.ReturnMapSelected))
                {
                    panelMapJourneyControlOutward.Visible = false;
                    panelMapJourneyControlReturn.Visible = true;
                }

                // Commit selected view back to this pages viewstate
                viewState.OutwardMapSelected = panelMapJourneyControlOutward.Visible;
                viewState.ReturnMapSelected = panelMapJourneyControlReturn.Visible;
            }

            SetJourneyOverviewBackButton();

			// Set up correct mode for footnotes control
			if ( tdSessionManager.IsFindAMode )
			{
				if ( FindFareInputAdapter.IsCostBasedSearchMode(tdSessionManager.FindAMode) )
				{
					footnotesControl.CostBasedResults = true;
				}
				footnotesControl.Mode = tdSessionManager.FindAMode;
			}
		}

        /// <summary>
        /// Sets up the show directions button below the map
        /// </summary>
        private void SetJourneyDetailsShowControl()
        {
            if (outwardExists)
            {
                if ((mapHelper.PrivateOutwardJourney) || (mapHelper.CycleOutwardJourney))
                {
                    journeyDetailsShowControlOutward.Initialise(true, true, mapHelper.FindRelevantJourney(true));
                }
                else
                {
                    journeyDetailsShowControlOutward.Initialise(true, false, null);
                }
            }

            if (returnExists)
            {
                if ((mapHelper.PrivateReturnJourney) || (mapHelper.CycleReturnJourney))
                {
                    journeyDetailsShowControlReturn.Initialise(false, true, mapHelper.FindRelevantJourney(false));
                }
                else
                {
                    journeyDetailsShowControlReturn.Initialise(false, false, null);
                }
            }
        }

        /// <summary>
        /// Sets up the mode for the FindFareStepsConrol
        /// </summary>
        private void SetUpStepsControl()
        {
            findFareStepsControl.Visible = true;
            findFareStepsControl.Step = FindFareStepsControl.FindFareStep.FindFareStep3;
            findFareStepsControl.SessionManager = TDSessionManager.Current;
            findFareStepsControl.PageState = TDSessionManager.Current.FindPageState;
        }

        /// <summary>
        /// Sets the back button visibility when in Trunk mode
        /// </summary>
        private void SetJourneyOverviewBackButton()
        {
            if (TDSessionManager.Current.FindAMode == FindAMode.Trunk)
                theJourneyChangeSearchControl.GenericBackButtonVisible = true;
        }

        /// <summary>
        /// Sets up the printable control with the querystring params needed
        /// </summary>
        private void SetPrintableControl()
        {
            // setup printer friendly button for specific mode
            if (tdSessionManager.FindAMode == FindAMode.Cycle)
            {
                theJourneyChangeSearchControl.PrinterFriendlyPageButton.UsePageIdOverride = true;
                theJourneyChangeSearchControl.PrinterFriendlyPageButton.PageIdOverride = PageId.PrintableJourneyMapTile;
            }

            // Add the javascript to set the map viewstate on client side
            PrintableButtonHelper printHelper = null;

            if ((outwardExists) && (returnExists))
            {
                // Initialise for both outward and return maps
                printHelper = new PrintableButtonHelper(
                    mapJourneyControlOutward.MapId, 
                    mapJourneyControlReturn.MapId,
                    mapJourneyControlOutward.MapSymbolsSelectId,
                    mapJourneyControlReturn.MapSymbolsSelectId,
                    mapJourneyControlOutward.MapJourneyDisplayDetailsDropDownId,
                    mapJourneyControlReturn.MapJourneyDisplayDetailsDropDownId);
            }
            else if (outwardExists)
            {
                // Initialise only for outward map
                printHelper = new PrintableButtonHelper(
                    mapJourneyControlOutward.MapId,
                    string.Empty,
                    mapJourneyControlOutward.MapSymbolsSelectId,
                    string.Empty,
                    mapJourneyControlOutward.MapJourneyDisplayDetailsDropDownId,
                    string.Empty);
            }
            else if (returnExists)
            {
                // Initialise only for return map
                printHelper = new PrintableButtonHelper(
                    string.Empty,
                    mapJourneyControlReturn.MapId,
                    string.Empty,
                    mapJourneyControlReturn.MapSymbolsSelectId,
                    string.Empty,
                    mapJourneyControlReturn.MapJourneyDisplayDetailsDropDownId);
            }

            if (printHelper != null)
            {
                if (tdSessionManager.FindAMode == FindAMode.Cycle)
                {
                    // This is the number of maps we want to aim for
                    int targetNumberOfMaps = Convert.ToInt32(Properties.Current["CyclePlanner.InteractiveMapping.Map.NumberOfMapTilesTarget"]);

                    // first, get the number of maps for our default zoom level
                    double scale = Convert.ToDouble(Properties.Current["CyclePlanner.InteractiveMapping.Map.MapTilesDefaultScale"]);

                    theJourneyChangeSearchControl.PrinterFriendlyPageButton.PrintButton.OnClientClick = printHelper.GetCycleMapClientScript(scale,targetNumberOfMaps, panelMapJourneyControlOutward.Visible);
                }
                else
                {
                    theJourneyChangeSearchControl.PrinterFriendlyPageButton.PrintButton.OnClientClick = printHelper.GetClientScript();
                }
            }
        }

		        /// <summary>
        /// Adds intellitracker tag when a user clicks the link to re-plan car journey avoiding closed roads.
        /// </summary>
        /// <param name="isReturn">True if the tag needs adding for return journey, false otherwise</param>
        private void AddReplanAvoidClosuresParam(bool isReturn)
        {
            try
            {
                
                string trackingParamKey = isReturn ? "ReturnReplanCarAvoidClosures" : "OutwardReplanCarAvoidClosures";

                trackingHelper.AddTrackingParameter(this.pageId.ToString(), trackingParamKey, TrackingControlHelper.CLICK);
                
                
            }
            catch (Exception ex)
            {
                string message = "TrackingControlHelper Exception: " + ex.StackTrace;
                OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
                            TDTraceLevel.Error, message);
                Logger.Write(oe);
            }
        }
		
		#endregion

		#region Methods to initialise controls

		/// <summary>
		/// Initialises all hyperlinks and images from the resourcing manager.
		/// </summary>
		private void InitialiseHyperlinkImage()
		{
			// Initialise hyperlink text
			hyperLinkReturnJourneys.Text = Global.tdResourceManager.GetString(
				"JourneyPlanner.hyperLinkReturnJourneys.Text", TDCultureInfo.CurrentUICulture);

			hyperLinkImageReturnJourneys.ImageUrl = Global.tdResourceManager.GetString(
				"JourneyPlanner.hyperLinkImageReturnJourneys", TDCultureInfo.CurrentUICulture);

			hyperLinkOutwardJourneys.Text = Global.tdResourceManager.GetString(
				"JourneyPlanner.hyperLinkOutwardJourneys.Text", TDCultureInfo.CurrentUICulture);

			hyperLinkImageOutwardJourneys.ImageUrl = Global.tdResourceManager.GetString(
				"JourneyPlanner.hyperLinkImageOutwardJourneys", TDCultureInfo.CurrentUICulture);

			hyperLinkImageOutwardJourneys.AlternateText = Global.tdResourceManager.GetString(
				"JourneyPlanner.hyperLinkImageOutwardJourneys.AlternateText", TDCultureInfo.CurrentUICulture);

			hyperLinkImageReturnJourneys.AlternateText = Global.tdResourceManager.GetString(
				"JourneyPlanner.hyperLinkImageReturnJourneys.AlternateText", TDCultureInfo.CurrentUICulture);
		}

		/// <summary>
		/// Initialises all static labels from the resourcing manager.
		/// </summary>
		private void InitialiseStaticLabels()
		{
			// Initialise static labels, hypertext text and TD Button 
			// from Resourcing Mangager.

			
			// Display different text if the itinerary summary is selected otherwise
			// display the selected journey number
			if (itineraryManager.FullItinerarySelected)
			{
				labelDetailsOutwardJourney.Text = 
					Global.tdResourceManager.GetString(
					"JourneyDetails.OutwardSummaryText", TDCultureInfo.CurrentUICulture);

				labelDetailsJourneyReturn.Text = 
					Global.tdResourceManager.GetString(
					"JourneyDetails.ReturnSummaryText", TDCultureInfo.CurrentUICulture);
			} 
			else 
			{
				labelDetailsOutwardJourney.Text = Global.tdResourceManager.GetString(
					"JourneyDetails.labelDetailsOutwardJourney.Text", TDCultureInfo.CurrentUICulture);

				labelDetailsJourneyReturn.Text = Global.tdResourceManager.GetString(
					"JourneyDetails.labelDetailsReturnJourney.Text", TDCultureInfo.CurrentUICulture);
				
				if(tdSessionManager.FindAMode == FindAMode.None || tdSessionManager.FindAMode == FindAMode.Bus)
				{
					labelDetailsOutwardJourney.Text += " " + itineraryManager.OutwardDisplayNumber.ToString();
					labelDetailsJourneyReturn.Text += " " + itineraryManager.ReturnDisplayNumber.ToString();
				}
			
			}

			if(tdSessionManager.FindAMode == FindAMode.Car) 
			{
				labelCarOutward.Text = "(" + Global.tdResourceManager.GetString(
					"JourneyDetails.labelDetailsCar", TDCultureInfo.CurrentUICulture) + ")";

				labelReturnCar.Text = "(" + Global.tdResourceManager.GetString(
					"JourneyDetails.labelDetailsCar", TDCultureInfo.CurrentUICulture) + ")";
			}
			else 
			{
				labelCarOutward.Text = "(" + Global.tdResourceManager.GetString(
					"JourneyDetails.labelDetailsCar", TDCultureInfo.CurrentUICulture) + ")";

				labelReturnCar.Text = "(" + Global.tdResourceManager.GetString(
					"JourneyDetails.labelDetailsCar", TDCultureInfo.CurrentUICulture) + ")";
			} 

			DetailsHelpCustomControl.AlternateText = Global.tdResourceManager.GetString(
				"HelpWithCarDirections.AlternateText", TDCultureInfo.CurrentUICulture);

			DetailsReturnHelpCustomControl.AlternateText = Global.tdResourceManager.GetString(
				"HelpWithCarDirections.AlternateText", TDCultureInfo.CurrentUICulture);

			DetailsHelpCustomControl.ImageUrl = Global.tdResourceManager.GetString(
				"HelpControl.Icon.ImageUrl", TDCultureInfo.CurrentUICulture);

			DetailsReturnHelpCustomControl.ImageUrl = Global.tdResourceManager.GetString(
				"HelpControl.Icon.ImageUrl", TDCultureInfo.CurrentUICulture);


			
		}

        /// <summary>
        /// Sets up the help text
        /// </summary>
        private void LoadHelp()
        {
            // Specific handling for find a cycle
            if ((tdSessionManager.IsFindAMode) && (tdSessionManager.FindAMode == FindAMode.Cycle)) 
            {
                theJourneyChangeSearchControl.HelpUrl = GetResource("JourneyMap.HelpPageUrl.Cycle");
            }
            else
            {
                theJourneyChangeSearchControl.HelpCustomControl.HelpLabelControl = helpLabelJourneyMap;
                theJourneyChangeSearchControl.HelpCustomControl.HelpLabel = "helpLabelJourneyMap";
                theJourneyChangeSearchControl.HelpLabel = "helpLabelJourneyMap";
            }
        }

		#endregion

		#region Methods to set dynamic label text

		/// <summary>
		/// Constructs the text string for the Outward Journeys label.
		/// </summary>
		private string GetOutwardJourneyLabelText()
		{
			// Get the boilerplate label strings from Resourcing Manager
			string forString = Global.tdResourceManager.GetString("JourneyPlanner.labelFor", TDCultureInfo.CurrentUICulture);
			string anyString = Global.tdResourceManager.GetString("JourneyPlanner.labelAnyTime", TDCultureInfo.CurrentUICulture);

			TDDateTime outwardLeavingTime;
			string leavingTimeDate = String.Empty;

			if ( showItinerary )
			{
				if (itineraryManager.OutwardMultipleDates)
				{
					// There are multiple dates in the outward Itinerary, so we can't say anything useful in the label
					return String.Empty;
				}
				else
				{
					// Get the outward leaving time from TDItineraryManager
					outwardLeavingTime = itineraryManager.OutwardDepartDateTime();
					leavingTimeDate = outwardLeavingTime.ToString("ddd dd MMM yy");

					// return the constructed string for travelling at a specific date
					return String.Format("{0} {1}", forString, leavingTimeDate);
				}
			}
			else
			{
				if (tdSessionManager.JourneyViewState.OriginalJourneyRequest != null)
				{
					TDJourneyViewState viewState = tdSessionManager.JourneyViewState;

					// Get the outward leaving time from TDSessionManager
					outwardLeavingTime = viewState.OriginalJourneyRequest.OutwardDateTime[0];
					leavingTimeDate = outwardLeavingTime.ToString("ddd dd MMM yy");

					if (viewState.OriginalJourneyRequest.OutwardAnyTime)
					{
						// return the constructed string for travelling at any time
						return String.Format("{0} {1} {2}", forString, leavingTimeDate, anyString);
					}
					else
					{
						string leavingTimeTime = outwardLeavingTime.ToString("HH:mm");
						string outwardSearchType = String.Empty;

						if(tdSessionManager.JourneyViewState.JourneyLeavingTimeSearchType)
							outwardSearchType = Global.tdResourceManager.GetString(
								"JourneyPlanner.labelArrivingBefore", TDCultureInfo.CurrentUICulture);
						else
							outwardSearchType = Global.tdResourceManager.GetString(
								"JourneyPlanner.labelLeavingAfter", TDCultureInfo.CurrentUICulture);

						// return the constructed string for travelling at a specific date and time
						return String.Format("{0} {1} {2} {3}", forString, leavingTimeDate, outwardSearchType, leavingTimeTime);
					}
				}
				else
				{
					return String.Empty;
				}
			}
		}

		/// <summary>
		/// Constructs the text string for the Return Journeys label.
		/// </summary>
		private string GetReturnJourneyLabelText()
		{
			// Get the boilerplate label strings from Resourcing Manager
			string forString = Global.tdResourceManager.GetString("JourneyPlanner.labelFor", TDCultureInfo.CurrentUICulture);
			string anyString = Global.tdResourceManager.GetString("JourneyPlanner.labelAnyTime", TDCultureInfo.CurrentUICulture);

			TDDateTime returnLeavingTime;
			string leavingTimeDate = String.Empty;

			if ( showItinerary )
			{
				if (itineraryManager.ReturnMultipleDates)
				{
					// There are multiple dates in the return Itinerary, so we can't say anything useful in the label
					return String.Empty;
				}
				else
				{
					// Get the return leaving time from TDItineraryManager
					returnLeavingTime = itineraryManager.ReturnDepartDateTime();
					leavingTimeDate = returnLeavingTime.ToString("ddd dd MMM yy");

					// return the constructed string for travelling at a specific date
					return String.Format("{0} {1}", forString, leavingTimeDate);
				}
			}
			else
			{
				if (tdSessionManager.JourneyViewState.OriginalJourneyRequest != null)
				{
					TDJourneyViewState viewState = tdSessionManager.JourneyViewState;

					// Get the return leaving time from TDSessionManager
					returnLeavingTime = viewState.OriginalJourneyRequest.ReturnDateTime[0];
					leavingTimeDate = returnLeavingTime.ToString("ddd dd MMM yy");

					if (viewState.OriginalJourneyRequest.ReturnAnyTime)
					{
						// return the constructed string for travelling at any time
						return String.Format("{0} {1} {2}", forString, leavingTimeDate, anyString);
					}
					else
					{
						string leavingTimeTime = returnLeavingTime.ToString("HH:mm");
						string returnSearchType = String.Empty;

						if(tdSessionManager.JourneyViewState.JourneyLeavingTimeSearchType)
							returnSearchType = Global.tdResourceManager.GetString(
								"JourneyPlanner.labelArrivingBefore", TDCultureInfo.CurrentUICulture);
						else
							returnSearchType = Global.tdResourceManager.GetString(
								"JourneyPlanner.labelLeavingAfter", TDCultureInfo.CurrentUICulture);

						// return the constructed string for travelling at a specific date and time
						return String.Format("{0} {1} {2} {3}", forString, leavingTimeDate, returnSearchType, leavingTimeTime);
					}
				}
				else
				{
					return String.Empty;
				}
			}
		}

		/// <summary>
		/// Sets the text for the skip to links (for screenreader browsers).
		/// Handles visibility of links according to status of screen (eg whether return journeys exist)
		/// </summary>
		private void SetupSkipLinksAndScreenReaderText()
		{

			// Setup gif resource for images (1 invisible image for all skip links)
			string SkipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			imageMainContentSkipLink1.ImageUrl = SkipLinkImageUrl;
			imageJourneyButtonsSkipLink1.ImageUrl = SkipLinkImageUrl;
			imageJourneyButtonsSkipLink2.ImageUrl = SkipLinkImageUrl;
			imageOutwardJourneyMapSkipLink1.ImageUrl = SkipLinkImageUrl;
			imageFindTransportToStartLocationSkipLink.ImageUrl = SkipLinkImageUrl;
			imageFindTransportFromEndLocationSkipLink.ImageUrl = SkipLinkImageUrl;
			imageReturnJourneyMapSkipLink1.ImageUrl = SkipLinkImageUrl;

			imageMainContentSkipLink1.AlternateText = GetResource("JourneyMap.imageMainContentSkipLink.AlternateText");

			string journeyButtonsSkipLinkText = GetResource("JourneyMap.imageJourneyButtonsSkipLink.AlternateText");

			imageJourneyButtonsSkipLink1.AlternateText = journeyButtonsSkipLinkText;
			imageJourneyButtonsSkipLink2.AlternateText = journeyButtonsSkipLinkText;

			// Only show the link to outward journeys portion of the screen if we have outward journeys on screen
			if (outwardExists)
			{
				panelOutwardJourneyMapSkipLink1.Visible = true;
				imageOutwardJourneyMapSkipLink1.AlternateText = GetResource("JourneyMap.imageOutwardJourneyMapSkipLink1.AlternateText");

				if (itineraryExists)
				{
					panelFindTransportToStartLocationSkipLink.Visible = true;
					imageFindTransportToStartLocationSkipLink.AlternateText = GetResource("JourneyMap.imageFindTransportToStartLocationSkipLink.AlternateText");

					panelFindTransportFromEndLocationSkipLink.Visible = true;
					imageFindTransportFromEndLocationSkipLink.AlternateText = GetResource("JourneyMap.imageFindTransportFromEndLocationSkipLink.AlternateText");
				}
				else
				{
					panelFindTransportToStartLocationSkipLink.Visible = false;
					panelFindTransportFromEndLocationSkipLink.Visible = false;
				}
			}

			// Only show the link to return journeys portion of the screen if we have return journeys on screen
			if (returnExists)
			{
				panelReturnJourneyMapSkipLink1.Visible = true;
				imageReturnJourneyMapSkipLink1.AlternateText = GetResource("JourneyMap.imageReturnJourneyMapSkipLink1.AlternateText");

			}
		}

        /// <summary>
        /// Displays error message when validation fails for replanning a road journey to avoid closure or blockages
        /// </summary>
        private void ShowReplanAvoidClosureError()
        {
            if (tdSessionManager.ValidationError.Contains(ValidationErrorID.RouteAffectedByClosuresErrors))
            {
                string closureError = Global.tdResourceManager.GetString("ValidateAndRun.RouteAffectedByClosures", TDCultureInfo.CurrentUICulture);

                errorDisplayControl.ErrorStrings = new string[] { closureError };

                errorDisplayControl.ReferenceNumber = tdSessionManager.JourneyResult.JourneyReferenceNumber.ToString();

                if (errorDisplayControl.ErrorStrings.Length > 0)
                {
                    panelErrorDisplayControl.Visible = true;
                    errorDisplayControl.Visible = true;
                }

            }
        }

		#endregion Methods to set dynamic label text
        
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			ExtraWiringEvents();
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// 
		/// </summary>
		private void ExtraWiringEvents() 
		{
			// Subscribe to selection changed event of the Summary 
			// table controls so that the map can be updated when it fires.
			summaryResultTableControlOutward.SelectionChanged +=
				new SelectionChangedEventHandler(this.OutwardSelectionChanged);

			summaryResultTableControlReturn.SelectionChanged +=
				new SelectionChangedEventHandler(this.ReturnSelectionChanged);
			
			findSummaryResultTableControlOutward.SelectionChanged += 
				new SelectionChangedEventHandler(this.OutwardSelectionChanged);

			findSummaryResultTableControlReturn.SelectionChanged += 
				new SelectionChangedEventHandler(this.ReturnSelectionChanged);
            			
			carAllDetailsControlOutward.carjourneyDetailsTableControl.SectionMapSelected += new TransportDirect.UserPortal.Web.Controls.CarJourneyDetailsTableControl.SectionMapSelectedEventHandler(carjourneyDetailsTableControl_SectionMapSelected);
			carAllDetailsControlOutward.carjourneyDetailsTableControl.FirstLastMapSelected +=new TransportDirect.UserPortal.Web.Controls.CarJourneyDetailsTableControl.FirstLastMapSelectedEventHandler(carjourneyDetailsTableControl_FirstLastMapSelected);
			carAllDetailsControlReturn.carjourneyDetailsTableControl.SectionMapSelected +=new TransportDirect.UserPortal.Web.Controls.CarJourneyDetailsTableControl.SectionMapSelectedEventHandler(carjourneyDetailsTableControlReturn_SectionMapSelected);
			carAllDetailsControlReturn.carjourneyDetailsTableControl.FirstLastMapSelected +=new TransportDirect.UserPortal.Web.Controls.CarJourneyDetailsTableControl.FirstLastMapSelectedEventHandler(carjourneyDetailsTableControlReturn_FirstLastMapSelected);

            mapJourneyControlOutward.ShowReturnJourneyButton.Click += new EventHandler(ShowReturnJourneyButton_Click);
            mapJourneyControlReturn.ShowOutwardJourneyButton.Click += new EventHandler(ShowOutwardJourneyButton_Click);

			amendSaveSendControl.AmendViewControl.SubmitButton.Click += new EventHandler(AmendViewControl_Click);

            if (TDSessionManager.Current.FindAMode == FindAMode.Trunk)
                this.theJourneyChangeSearchControl.GenericBackButton.Click += new EventHandler(this.buttonBackJourneyOverview_Click);
            else
                this.theJourneyChangeSearchControl.GenericBackButton.Click += new EventHandler(this.findFareBackButton_Click);


            socialBookMarkLinkControl.EmailLinkButton.Click += new EventHandler(EmailLink_Click);

            carAllDetailsControlOutward.ReplanAvoidClosures += new EventHandler(carAllDetailsControlOutward_ReplanAvoidClosures);
            carAllDetailsControlReturn.ReplanAvoidClosures += new EventHandler(carAllDetailsControlReturn_ReplanAvoidClosures);            
        }

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			trackingHelper = new TrackingControlHelper();
            roadJourneyHelper = new RoadJourneyHelper();
		}

		#endregion

		#region Event Handling code

		/// <summary>
		/// Handler for the selection changed event from the the Outward Journey Summary.
		/// </summary>
		private void OutwardSelectionChanged(object sender, EventArgs e)
		{			
			
		}

		/// <summary>
		/// Handler for the selection changed event from the Return Journey Summary.
		/// </summary>
		private void ReturnSelectionChanged(object sender, EventArgs e)
		{
			
		}


		/// <summary>
		/// Handler for Information selected and More Help events
		/// </summary>
		private void OnMapStore(object sender, EventArgs e)
		{
			
		}

		/// <summary>
		/// Event handler that is fired when the "OK" button is clicked on the amendViewControl.
		/// This will switch Session partitions and display Summary page with the appropriate results.
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void AmendViewControl_Click(object sender,EventArgs e)
		{
            PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(tdSessionManager);

			plannerOutputAdapter.ViewPartitionResults(amendSaveSendControl.AmendViewControl.PartitionSelected);
		}

		/// <summary>
		/// Event handler that responds to changes in outward and return maps.  
		/// Changes are then saved in session data so the information 
		/// is available to the printer friendly page.
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void Map_OnMapChangedEvent(object sender, MapChangedEventArgs e)
		{
                       			
		}

        private void OutwardMapOverviewMapLink_Click(object sender, EventArgs e)
        {
            
        }

        private void ReturnMapOverviewMapLink_Click(object sender, EventArgs e)
        {
            
        }

        private string GetModeImageSource()
        {
            string modeimage = string.Empty;

            switch (tdSessionManager.FindAMode)
            {
                case FindAMode.Bus:
                    modeimage = GetResource("HomePlanAJourney.imageFindBus.ImageUrl");
                    break;
                case FindAMode.Car:
                    modeimage = GetResource("HomeDefault.imageFindCar.ImageUrl");
                    break;
                case FindAMode.CarPark:
                    modeimage = GetResource("HomeDefault.imageFindCarPark.ImageUrl");
                    break;
                case FindAMode.Coach:
                    modeimage = GetResource("HomeDefault.imageFindCoach.ImageUrl");
                    break;
                case FindAMode.Flight:
                    modeimage = GetResource("HomeDefault.imageFindFlight.ImageUrl");
                    break;
                case FindAMode.RailCost:
                case FindAMode.Train:
                    modeimage = GetResource("HomeDefault.imageFindTrain.ImageUrl");
                    break;
                default:
                    modeimage = GetResource("PlanAJourneyControl.imageDoorToDoor.ImageUrl");
                    break;

            }

            return modeimage;
        }

        /// <summary>
        /// Event handler that responds to the Social Link control's email link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmailLink_Click(object sender, EventArgs e)
        {
            if (amendSaveSendControl.IsLoggedIn())
            {
                amendSaveSendControl.SetActiveTab(AmendPanelMode.SendEmailNormal);
            }
            else
            {
                amendSaveSendControl.SetActiveTab(AmendPanelMode.SendEmailLogin);
            }
            amendSaveSendControl.Focus();


            string amendSaveSendControlFocusScript = @"<script>  function ScrollView() { var el = document.getElementById('" + amendSaveSendControl.SendEmailTabButton.ClientID
                                              + @"'); if (el != null){ el.scrollIntoView(); el.focus();}} window.onload = ScrollView;</script>";

            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "CtrlFocus", amendSaveSendControlFocusScript);

        }

        /// <summary>
        /// Handler for replan avoid closures event 
        /// to replan the return road journey avoiding closures/blockages affecting the journey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void carAllDetailsControlReturn_ReplanAvoidClosures(object sender, EventArgs e)
        {
            TDJourneyViewState viewState = itineraryManager.JourneyViewState;
            viewState.ReturnRoadReplanned = true;
            // only avoid the toids with closure/blockages
            bool success = roadJourneyHelper.ReplanRoadJourneyToAvoidClosures(false, true);
            if (!success)
            {
                ShowReplanAvoidClosureError();

                // Ensure the Replan button is no longer shown in the car details control
                RoadJourney roadJourney = TDItineraryManager.Current.JourneyResult.ReturnRoadJourney();
                roadJourney.AllowReplan = false;

                // Scroll to top of page so error message is in view
                ScrollManager.RestPageAtElement(headerControl.ClientID);
            }
        }

        /// <summary>
        /// Handler for replan avoid closures event 
        /// to replan the outward road journey avoiding closures/blockages affecting the journey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void carAllDetailsControlOutward_ReplanAvoidClosures(object sender, EventArgs e)
        {
            TDJourneyViewState viewState = itineraryManager.JourneyViewState;

            viewState.OutwardRoadReplanned = true;
            // only avoid the toids with closure/blockages
            bool success = roadJourneyHelper.ReplanRoadJourneyToAvoidClosures(true, true);
            if (!success)
            {
                ShowReplanAvoidClosureError();

                // Ensure the Replan button is no longer shown in the car details control
                RoadJourney roadJourney = TDItineraryManager.Current.JourneyResult.OutwardRoadJourney();
                roadJourney.AllowReplan = false;

                // Scroll to top of page so error message is in view
                ScrollManager.RestPageAtElement(headerControl.ClientID);
            }
        }

		#endregion

		#region ViewState
		/// <summary>
		/// Returns the correct journey view state from the session, 
		/// according to whether the user has extended a journey.
		/// </summary>
		/// <returns>The current JourneyViewState object.</returns>
		private TDJourneyViewState getJourneyViewState()
		{
			if ( showItinerary )
			{
				return itineraryManager.JourneyViewState;
			}
			else
			{
				return tdSessionManager.JourneyViewState;
			}

		}

		/// <summary>
		/// Returns the correct journey result from the session, 
		/// according to whether the user has extended a journey.
		/// </summary>
		/// <returns>The current JourneyResult object.</returns>
		private ITDJourneyResult getJourneyResult()
		{
			if ( showItinerary )
			{
				return itineraryManager.JourneyResult;
			}
			else
			{
				return tdSessionManager.JourneyResult;
			}	
		}
		#endregion

        #region Click events
        /// <summary>
		/// IR2500: Handler for the Back button click event. The Back button should only be visible if the user
		/// has arrived at this page via Find-A-Fare. Effectively the same as JourneySummary and JourneyDetails.
		/// Uses the JourneySummaryTicketSelection transition event. No need for additional transition events 
		/// at this time because the intended behaviour/navigation is just the same as JourneySummary.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void findFareBackButton_Click(object sender, EventArgs e)
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;
			sessionManager.Session[SessionKey.Transferred] = false;
			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneySummaryTicketSelectionBack;        
		}

        /// <summary>
        /// Handle button click to redirect user to journey overview page 
        /// </summary>
        /// <param name="sender">Originator of event</param>
        /// <param name="e">Event parameters</param>
        protected void buttonBackJourneyOverview_Click(object sender, EventArgs e)
        {
            ITDSessionManager sessionManager = TDSessionManager.Current;
            sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoJourneyOverview;
        }

        /// <summary>
        /// Button click event when the Outward journey button is clicked on the map journey control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowOutwardJourneyButton_Click(object sender, EventArgs e)
        {
            // Hide the return journey map and show the outward journey map
            panelMapJourneyControlOutward.Visible = true;
            panelMapJourneyControlReturn.Visible = false;

            // Commit selected view to session to ensure if they choose a different journey then
            // outward maps continue to be shown
            TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
            viewState.OutwardMapSelected = true;
            viewState.ReturnMapSelected = false;

            // If the user has selected to show map on the details page, update the selected view
            if (viewState.ReturnShowMap)
            {
                viewState.OutwardShowMap = true;
                viewState.ReturnShowMap = false;
            }

            this.ScrollManager.RestPageAtElement(mapJourneyControlOutward.FirstElementId);
        }

        /// <summary>
        /// Button click event when the Return journey button is clicked on the map journey control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowReturnJourneyButton_Click(object sender, EventArgs e)
        {
            // Hide the outward journey map and show the return journey map
            panelMapJourneyControlOutward.Visible = false;
            panelMapJourneyControlReturn.Visible = true;

            // Commit selected view to session to ensure if they choose a different journey then
            // return maps continue to be shown
            TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
            viewState.OutwardMapSelected = false;
            viewState.ReturnMapSelected = true;

            // If the user has selected to show map on the details page, update the selected view
            if (viewState.OutwardShowMap)
            {
                viewState.OutwardShowMap = false;
                viewState.ReturnShowMap = true;
            }

            this.ScrollManager.RestPageAtElement(mapJourneyControlReturn.FirstElementId);
        }

		private void carjourneyDetailsTableControl_SectionMapSelected(object sender, TransportDirect.UserPortal.Web.Controls.CarJourneyDetailsTableControl.SectionMapSelectedEventArgs e)
		{

		}

		private void carjourneyDetailsTableControlReturn_SectionMapSelected(object sender, TransportDirect.UserPortal.Web.Controls.CarJourneyDetailsTableControl.SectionMapSelectedEventArgs e)
		{

		}

		private void carjourneyDetailsTableControl_FirstLastMapSelected(object sender, TransportDirect.UserPortal.Web.Controls.CarJourneyDetailsTableControl.FirstLastMapSelectedEventArgs e)
		{
			
		}

		private void carjourneyDetailsTableControlReturn_FirstLastMapSelected(object sender, TransportDirect.UserPortal.Web.Controls.CarJourneyDetailsTableControl.FirstLastMapSelectedEventArgs e)
		{

        }

        #endregion

        #region AJAX Web method
        /// <summary>
        /// AJAX method which is called by the client to get the chart data. 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static CycleJourneyGraphControl.TDChartData GetChartData(bool outward)
        {
            // WebMethod must be in the ASPX page as it cannot be called directly from the control.
            // It must also be static to allow an AJAX call to be made to it 
            // (for explanation, see: http://encosia.com/2008/04/16/why-do-aspnet-ajax-page-methods-have-to-be-static/ )

            ITDSessionManager sessionManager = TDSessionManager.Current;

            CyclePlannerHelper cycleHelper = new CyclePlannerHelper(sessionManager);

            return cycleHelper.GetChartData(outward);
        }

        #endregion

        #region JavaScript Check
        /// <summary>
        /// Checks if javascript is diabled and shows info message if disabled for map
        /// </summary>
        private void CheckJavascriptEnabled()
        {
            if (!this.IsJavascriptEnabled)
            {
                errorDisplayControl.Type = ErrorsDisplayType.Custom;
                errorDisplayControl.ErrorsDisplayTypeText = GetResource("MapControl.JavaScriptDisabled.Heading.Text");

                List<string> errors = new List<string>();

                errors.Add(GetResource("MapControl.JavaScriptDisabled.Description.Text"));
                errors.Add(GetResource("MapControl.JavaScriptDisabled.JourneyMap.Text"));
                errorDisplayControl.ErrorStrings = errors.ToArray();

                panelErrorDisplayControl.Visible = true;
                errorDisplayControl.Visible = true;
                mapJourneyControlOutward.Visible = false;
                mapJourneyControlReturn.Visible = false;
            }
        }
        #endregion
    }
}