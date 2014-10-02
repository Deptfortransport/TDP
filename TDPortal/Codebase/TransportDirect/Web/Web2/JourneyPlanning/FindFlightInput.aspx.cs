// *********************************************** 
// NAME                 : FindFlightInput.aspx.cs 
// AUTHOR               : Jonathan George 
// DATE CREATED         : 07/05/2004 
// DESCRIPTION			: Input page for flight planning. The page
//                        also displays ambiguities.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindFlightInput.aspx.cs-arc  $
//
//   Rev 1.14   Mar 22 2013 10:49:14   DLane
//CCN0648c - Super Batch enhancements
//Resolution for 5907: CCN0648c - "Super Batch" enhancements
//
//   Rev 1.13   Jul 28 2011 16:20:02   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.12   May 13 2010 13:05:22   mmodi
//Added code call to clear the printable map session information 
//Resolution for 5535: Printable maps session logic improvement
//
//   Rev 1.11   Mar 26 2010 11:54:14   mturner
//Code review actions
//Resolution for 5481: Session issue when going from FAT to D2D using the left hand menu
//
//   Rev 1.10   Mar 26 2010 11:34:26   mturner
//Added code to clear journey results from session if you are coming directly from another planner.
//
//Resolution for 5481: Session issue when going from FAT to D2D using the left hand menu
//
//   Rev 1.9   Mar 23 2010 16:47:22   pghumra
//Rearranged alignment of controls and added label for origin/destination controls
//Resolution for 5479: CODE FIX - INITIAL - DEL 10.x - Find nearest button not aligned in flight
//
//   Rev 1.8   Mar 10 2010 11:16:14   MTurner
//Changes to make selected locations editabl;e if in amend mode.
//Resolution for 5445: Find a flight page inconsistent
//
//   Rev 1.7   Jan 29 2010 14:45:28   mmodi
//Updated to reset CycleResult to correct error when planning journey after coming directly from Cycle journey details
//Resolution for 5388: Cycle Planner - Server error when planning Door to door after a cycle journey
//
//   Rev 1.6   Jan 30 2009 10:44:14   apatel
//Search Engine Optimasation changes - CCN 624
//Resolution for 5229: Search Engin Optimisation Changes -  CCN624
//
//   Rev 1.5   May 02 2008 11:46:00   mmodi
//Improved formatting
//Resolution for 4925: Control alignments: Find a flight
//
//   Rev 1.4   May 01 2008 17:23:50   mmodi
//Updated to display session timeout error
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.3   Apr 08 2008 15:15:56   scraddock
//Advanced Text Not Hidden
//
// rev devfactory 8 apr sbarker
// Advanced text hidden when advanced pressed, or in ambiguity mode
//
//   Rev 1.2   Mar 31 2008 13:24:34   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory Feb 08 09:44:05 psheldrake
//   added support for new resx / mcms loading logic
//
//  Rev Devfactory Jan 30 2008 12:30:00 apatel
//  CCN 427 Changes made to add left hand menu and added two more pageoptionscontrols to make them show/hide
//  depending on different options selected.
//
//   Rev 1.0   Nov 08 2007 13:29:34   mturner
//Initial revision.
//
//   Rev 1.66   Sep 03 2007 15:24:58   pscott
//CCN407 IR 4490
//title and key word changes for Google natural search
//
//   Rev 1.65   Apr 26 2006 12:01:40   pscott
//IR 3510/3927
//Calendar control closure in ambiguity page
//
//   Rev 1.64   Apr 13 2006 11:08:06   COwczarek
//Call ResetLandingPageSessionParameters in Unload event
//handler rather than PreRender event handler. This ensures
//parameters are reset even if redirect occurs due to autoplan
//being set.
//Resolution for 3902: Landing Page: Using Find A Car with autoplan set then clicking amend throws exception
//
//   Rev 1.63   Apr 10 2006 12:04:26   jbroome
//Fix for IR3797
//Resolution for 3797: Landing Page: Find a Flight - input page is resolved on entry preventing dates from being changed
//
//   Rev 1.62   Apr 05 2006 15:25:04   esevern
//Manual merge of stream0030
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.61   Mar 30 2006 10:37:32   halkatib
//Made page check if coming from landing page before initialising the journey paramters. Initialisation is not required on the page since the landing page does this already. When this happens twice in a landing page call the journeyparametes set by the landing are changed.
//
//   Rev 1.60   Mar 27 2006 10:27:02   kjosling
//Merged stream 0023 - Journey Results
//
//   Rev 1.59   Mar 22 2006 17:30:12   halkatib
//Changes due to Merge of stream3152 Landing Page phase 3
//
//   Rev 1.58   Mar 10 2006 12:42:56   pscott
//SCR3510
//Close Calendar Control when going to Ambiguity page
//
//   Rev 1.57   Feb 23 2006 19:29:32   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.56   Feb 10 2006 10:47:54   jmcallister
//Manual Merge of Homepage 2. IR3180
//
//   Rev 1.55   Jan 12 2006 18:16:12   RPhilpott
//Reset TDItineraryManager to default (mode "None") in page initialisation to allow for case where we are coming from VisitPlanner.
//Resolution for 3450: DEL 8: Server error when returning to Quickplanner results from Visit Planner input
//
//   Rev 1.54   Nov 22 2005 10:51:58   NMoorhouse
//Removed obsolete local date control update methods (code which has been moved into Adapter classes)
//Resolution for 3069: DN77 - Ambiguity pages not holding values
//
//   Rev 1.53   Nov 15 2005 19:41:36   rgreenwood
//IR2990 Wired up help button
//Resolution for 2990: UEE Post Build Enhancement: Add Help Pages to Input Pages
//
//   Rev 1.52   Nov 10 2005 14:32:48   NMoorhouse
//TD093 UEE Input Pages - Soft Content
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.51   Nov 09 2005 16:58:46   RPhilpott
//Merge for stream2818.
//
//   Rev 1.50   Nov 03 2005 16:00:24   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.49.1.2   Oct 27 2005 14:01:14   NMoorhouse
//TD93 - UEE Input Pages, Date Control element CUT
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.49.1.1   Oct 12 2005 15:55:30   mtillett
//UEE changes to the layout of the page (advanced options and location controls)
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.49.1.0   Oct 10 2005 19:05:48   rgreenwood
//TD089 ES020 Image Button Replacement - Changed resource refs to fix build
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.49   Sep 29 2005 12:50:36   build
//Automatically merged from branch for stream2673
//
//   Rev 1.48.1.0   Sep 07 2005 13:10:06   rgreenwood
//DN079 UEE ES015 24 Hour Help Button removal
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.48   Apr 28 2005 14:25:10   rscott
//Changes to resolve IR2360
//
//   Rev 1.47   Apr 15 2005 12:48:12   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.46   Mar 08 2005 16:26:56   bflenk
//TimeOut functionality implemented in TDPage.cs, removed from this file - IR1720
//
//   Rev 1.45   Dec 06 2004 13:54:40   asinclair
//Updated fix for 1720
//
//   Rev 1.44   Nov 26 2004 15:30:02   asinclair
//Further fix for IR 1720
//
//   Rev 1.43   Nov 19 2004 11:54:34   asinclair
//Fix for IR 1720
//
//   Rev 1.42   Oct 21 2004 17:58:00   esevern
//amended to use TDCultureInfo.CurrentUICulture
//
//   Rev 1.41   Oct 15 2004 12:39:08   jgeorge
//Changed to take account of new JourneyPlanStateData and changes to existing JourneyPlanControlData.
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.40   Oct 04 2004 16:00:08   rgeraghty
//IR1337 - Added alt text for help icon - direct flight checkbox
//
//   Rev 1.39   Oct 01 2004 11:04:22   COwczarek
//Make behaviour consistent for all Find A input pages when in ambiguity mode.
//Resolution for 1562: Find A input page in ambiguity mode always shows travel details
//
//   Rev 1.38   Sep 23 2004 10:35:50   jmorrissey
//Update to PageLoad
//
//   Rev 1.37   Sep 21 2004 15:06:10   jmorrissey
//IR1603
//Resolution for 1603: Find nearest: Error message is not quite accurate in the action specified.
//
//   Rev 1.36   Sep 21 2004 11:26:34   jmorrissey
//Distinguish how origin and destination were selected before setting 'overlapping' error messages
//
//   Rev 1.35   Sep 14 2004 16:42:44   jmorrissey
//IR1507 - now displays validation error if the selected from, to or via airports are the same. This prevents journeys such as Scotland to Scotland being planned.
//
//   Rev 1.34   Sep 08 2004 15:47:26   COwczarek
//Set tool tip text for 24 hour clock help button visible in
//ambiguous mode
//Resolution for 1336: Date controls for in Find A Flight inconsistent with other Find A pages
//
//   Rev 1.33   Sep 06 2004 19:07:32   jgeorge
//Amended UpdateDirectFlightsOnly method. 
//Resolution for 1255: Impossible to go back to change flight details
//
//   Rev 1.32   Sep 06 2004 11:50:16   jbroome
//IR 1474 - Added Any Time values to date drop down lists when in ambiguity mode.
//Replaced hard coded "Any" references with Adapters.FindInputAdapter.AnyTimeValue
//
//   Rev 1.31   Sep 02 2004 12:16:58   passuied
//corrected code inversion in UpdateReturnDate that was assigning selected value to JourneyParams rather than the opposite
//
//   Rev 1.30   Aug 27 2004 10:43:24   passuied
//added back button at top of page
//
//   Rev 1.29   Aug 26 2004 16:46:32   COwczarek
//Changes to display journey preferences consistently in read only mode across all Find A pages
//Resolution for 1421: Find a ambiguity pages (QA)
//
//   Rev 1.28   Aug 25 2004 10:24:24   jmorrissey
//IR1357 - the correct resolved location now passed to the AirportDisplayControl SetData() method.
//
//   Rev 1.27   Aug 23 2004 16:24:14   jgeorge
//IR1255
//
//   Rev 1.26   Aug 19 2004 13:29:26   COwczarek
//Reset Find A session data when the page is loaded and 
//the "new search" button was previously clicked or if there is
//an itinerary or an extension is in progress.
//Resolution for 1306: Find a Train extends a journey incorrectly.
//
//   Rev 1.25   Aug 17 2004 17:56:46   jmorrissey
//Interim check in for IR1327. Not ready for release.
//
//   Rev 1.24   Aug 17 2004 13:34:58   passuied
//Fixed display of location control, when clicking next, when searching with find Nearest and when coming back from Amend Journey
//Resolution for 1295: Find a flight does not display airports when using find nearest function
//
//   Rev 1.23   Aug 17 2004 11:01:52   jgeorge
//IR1284
//
//   Rev 1.22   Aug 17 2004 09:20:38   COwczarek
//Prior to commencing journey planning, reset the itinerary if one
//exists since it is not currently possible to extend using a Find A
//function. At this point save the current Find A mode and journey
//parameters to record what was used to plan the journey.
//
//   Rev 1.21   Aug 05 2004 14:52:26   COwczarek
//Redirect to journey summary page not redundant find summary page
//Resolution for 1202: Implement FindTrainInput and FindCoachInput pages
//
//   Rev 1.20   Aug 03 2004 16:46:26   RHopkins
//Removed spurious "using" reference.
//
//   Rev 1.19   Aug 03 2004 09:59:40   JHaydock
//Added back button, fixed issues with page submission state being remembered.
//
//   Rev 1.18   Jul 22 2004 18:06:04   passuied
//Integration between pages and move of code to location service
//
//   Rev 1.17   Jul 22 2004 17:10:18   passuied
//Added FindStationPageState Init before calling FindNearestStation
//
//   Rev 1.16   Jul 22 2004 15:26:36   esevern
//interim checkin - date validation error changes - uses FindDateValidation
//
//   Rev 1.15   Jul 14 2004 16:36:24   passuied
//Changes for del6.1. FindFlight functionality working after SessionManager changes.
//
//   Rev 1.14   Jul 14 2004 13:00:36   passuied
//Changes in SessionManager with impact in Web for Del 6.1
//Compiles
//
//   Rev 1.13   Jul 12 2004 14:13:48   passuied
//use of new property Mode of FindPageState base class
//
//   Rev 1.12   Jul 09 2004 15:23:34   jgeorge
//IR 1154
//
//   Rev 1.11   Jul 08 2004 15:41:44   jgeorge
//Actioned review comments
//
//   Rev 1.10   Jul 07 2004 12:20:06   jgeorge
//IR1129
//
//   Rev 1.9   Jul 02 2004 12:44:54   jgeorge
//Help and label text
//
//   Rev 1.8   Jun 24 2004 14:46:34   jgeorge
//Changed to use ChangeJourneyParametersType of TDSessionManager
//
//   Rev 1.7   Jun 23 2004 17:07:52   jgeorge
//Help text and transition to wait page
//
//   Rev 1.6   Jun 21 2004 15:16:58   jgeorge
//Added code to populate page title literal
//
//   Rev 1.5   Jun 17 2004 16:39:24   jgeorge
//Updated for changes to TDJourneyParametersFlight
//
//   Rev 1.4   Jun 17 2004 13:51:02   jgeorge
//Moved help labels
//
//   Rev 1.3   Jun 10 2004 10:19:02   jgeorge
//Updated for changes to FindFlightPageState and TDJourneyParametersFlight
//
//   Rev 1.2   Jun 09 2004 17:11:00   jgeorge
//Interim check in
//
//   Rev 1.1   Jun 02 2004 14:05:38   jgeorge
//Interim check in

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Web.Controls;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.CommonWeb.Helpers;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Class for the Find Flight input page
	/// </summary>
	public partial class FindFlightInput : TDPage
	{

		#region Standard controls

		protected System.Web.UI.WebControls.Literal literalPageTitle;
		private bool timeout;

		#endregion

		#region TransportDirect controls

		protected TransportDirect.UserPortal.Web.Controls.CalendarControl calendar;
		protected TransportDirect.UserPortal.Web.Controls.AirportBrowseControl airportBrowseFrom;
		protected TransportDirect.UserPortal.Web.Controls.AirportBrowseControl airportBrowseTo;
		protected TransportDirect.UserPortal.Web.Controls.AirportDisplayControl airportDisplayFrom;
		protected TransportDirect.UserPortal.Web.Controls.AirportDisplayControl airportDisplayTo;
		protected TransportDirect.UserPortal.Web.Controls.OperatorSelectionControl airOperatorSelection;
		protected TransportDirect.UserPortal.Web.Controls.AirStopOverControl findFlightInputStopover;
		protected TransportDirect.UserPortal.Web.Controls.TravelDetailsControl loginSaveOption;
		protected TransportDirect.UserPortal.Web.Controls.FindPageOptionsControl pageOptionsControl;
		protected TransportDirect.UserPortal.Web.Controls.FindLeaveReturnDatesControl dateControl;
		#endregion

		#region Private variables

		private IDataServices populator;
		private IAirDataProvider airData;
		private TDResourceManager rm;
		private TDLocation resolvedFromLocation;
		private TDLocation resolvedToLocation;

		private ValidationError errors;

		/// <summary>
		/// Holds user's current page state for this page
		/// </summary>
		private FindFlightPageState pageState;

		/// <summary>
		/// Hold user's current journey parameters for coach only journey
		/// </summary>
		private TDJourneyParametersFlight journeyParams;

		/// <summary>
		/// Helper class responsible for common methods to non-Find A pages
		/// </summary>
		private LeaveReturnDatesControlAdapter inputDateAdapter;

		/// <summary>
		/// Helper class for Landing Page functionality
		/// </summary>
		private LandingPageHelper landingPageHelper = new LandingPageHelper();


		#endregion

		#region Constructor

		/// <summary>
		/// Constructor. Sets the PageId property.
		/// </summary>
		public FindFlightInput()
		{
			this.pageId = PageId.FindFlightInput;
		}

		#endregion

		#region Page event handlers

		/// <summary>
		/// Initialise controls and local variables
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			rm = Global.tdResourceManager;
			airData = (IAirDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];
			inputDateAdapter = new LeaveReturnDatesControlAdapter();

            #region Display Session Errors
            // Check if there are any errors in the InputPageState and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, TDSessionManager.Current.InputPageState.InputSessionErrors);

            // Clear the error messages
            TDSessionManager.Current.InputPageState.InputSessionErrors = null;
            #endregion

			if(!Page.IsPostBack)
			{				
				populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
				rm = Global.tdResourceManager;
				airData = (IAirDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];

				ITDSessionManager sessionManager = TDSessionManager.Current;
				sessionManager.ItineraryMode = ItineraryManagerMode.None;
				TDItineraryManager itineraryManager = TDItineraryManager.Current;

                #region Clear cache of journey data
                // if an extension is in progress, cancel it
                sessionManager.ItineraryManager.CancelExtension();

                ClearCacheHelper helper = new ClearCacheHelper();

                // Force clear of any printable information if added by the journey result page
                helper.ClearPrintableResultCache(TDSessionPartition.TimeBased);

                // Fix for IR5481 Session issue when going from between different planners using the left hand menu
                if (sessionManager.FindAMode != FindAMode.Flight)
                {
                    // We have come directly from another planner so clear results from session.
                    helper.ClearJourneyResultCache();
                }
                #endregion

                // Initialise page state and journey parameter objects in session data

				bool resetDone;

				//No reset is required if coming from landing page, since the journey parameters 
				//have already been initialised.
				if (TDSessionManager.Current.Session[ SessionKey.LandingPageCheck ])
				{
					resetDone = false;
				}
				else
				{
					resetDone = sessionManager.InitialiseJourneyParameters(FindAMode.Flight);
				}

				pageState = (FindFlightPageState)TDSessionManager.Current.FindPageState;
				journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersFlight;

				if (resetDone)
				{
					LoadTravelDetails();
				}
			
				//IR 1357 - get the resolved to and from locations 
				if (TDSessionManager.Current.FindStationPageState.LocationFrom != null)
				{
					resolvedFromLocation = pageState.ResolvedFromLocation;
				}
			
				if (TDSessionManager.Current.FindStationPageState.LocationTo != null)
				{
					resolvedToLocation = pageState.ResolvedToLocation;
				}

                

				airportBrowseFrom.UseJavaScript = true;
				airportBrowseTo.UseJavaScript = true;

                pageOptionsControlsBottom.Visible = false;
                pageOptionsControlsInPanelCheckInTime.Visible = false;
			}

			if (Page.IsPostBack)
			{		
				pageState = (FindFlightPageState)TDSessionManager.Current.FindPageState;
				if (pageState == null)
				{
					TDPage.CloseAllSingleWindows(this);
					journeyParams.Initialise();
					pageState.Initialise();
					LoadTravelDetails();
					timeout = true;
					TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindFlightInputDefault;
					TDSessionManager.Current.FormShift[SessionKey.ForceRedirect] = true;
				}
				else
				{

					journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersFlight;
						
					//IR 1357 - get the resolved to and from locations 
					if (TDSessionManager.Current.FindStationPageState.LocationFrom != null)
					{
						resolvedFromLocation = pageState.ResolvedFromLocation;
					}
						
					if (TDSessionManager.Current.FindStationPageState.LocationTo != null)
					{
						resolvedToLocation = pageState.ResolvedToLocation;
					}
						
					airportBrowseFrom.UseJavaScript = true;
					airportBrowseTo.UseJavaScript = true;
						
					// The date controls don't raise events to say they have updated, so the params for these are
					// always update here
					UpdateOtherControls();
				}
			}
			else
			{
				// Initialise check in time control
				populator.LoadListControl(DataServiceType.CheckInTimeDrop, dropCheckInTime);
			}

            PageTitle = GetResource("FindFlightInput.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");
            labelFromToTitle.Text = GetResource("FindFlightInput.labelFromToTitle");
            labelCheckInTimeDropTitle.Text = GetResource("panelAirTravelPreferences.labelCheckInTimeDropTitle") + " ";
            
            labelCheckInTimeTitle.Text = GetResource("panelAirTravelPreferences.labelCheckInTimeTitle");
			labelCheckInTimeExplanation.Text = GetResource("panelAirTravelPreferences.labelCheckInTimeExplanation");
			labelCheckInTimeDropTitle.Text = GetResource("panelAirTravelPreferences.labelCheckInTimeDropTitle");
			labelCheckInTimeNote.Text = GetResource("panelAirTravelPreferences.labelCheckInTimeNote");

            

            airportBrowseFrom.labelText = GetResource("airportBrowseFrom.labelFrom");
            airportBrowseTo.labelText = GetResource("airportBrowseTo.labelFrom");

            airportDisplayFrom.labelText = GetResource("airportBrowseFrom.labelFrom");
            airportDisplayTo.labelText = GetResource("airportBrowseTo.labelFrom");
            
            //CCN 0427 setting image for the find a flight title
            imageFindFlight.ImageUrl = GetResource("HomeDefault.imageFindFlight.ImageUrl");
            imageFindFlight.AlternateText = " ";

            commandBack.Text = GetResource("FindFlightInput.CommandBack.Text");

            //Wire up the help button
			if (pageState.AmbiguityMode)
			{
				Helpbuttoncontrol1.HelpUrl =  GetResource("FindFlightInput.HelpAmbiguityUrl");

			}
			else
			{
				Helpbuttoncontrol1.HelpUrl =  GetResource("FindFlightInput.HelpPageUrl");
			}


            #region CCN 0427 left hand navigation changes
            //Added for white labelling:
            ConfigureLeftMenu("FindFlightInput.clientLink.BookmarkTitle", "Home.clientLink.LinkText", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFindFlightInput);
            expandableMenuControl.AddExpandedCategory("Related links");
            #endregion

			#region Landing Page Functionality
			//Check if we need to initiate an automatic search due to Landing Page Autoplan Mode			
			//if required then reset the autoplan flag and call the submit request method. 
			if (TDSessionManager.Current.Session[ SessionKey.LandingPageAutoPlan ])
			{
				SubmitRequest();
			}			
			#endregion


		}

		/// <summary>
		/// Page Unload event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Unload(object sender, System.EventArgs e)
		{
			//reset landing page session parameters
			if (TDSessionManager.Current.Session[ SessionKey.LandingPageCheck ])
			{
				landingPageHelper.ResetLandingPageSessionParameters();
			}
		}

		/// <summary>
		/// Connects additional event handlers
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Init(object sender, EventArgs e)
		{
			// Attach additional events
			airportBrowseFrom.AirportSelectionChanged += new EventHandler(airportBrowseFrom_AirportSelectionChanged);
			airportBrowseFrom.FindNearestClick += new EventHandler(airportBrowseFrom_AirportFindNearestClick);
			airportBrowseTo.AirportSelectionChanged += new EventHandler(airportBrowseTo_AirportSelectionChanged);
			airportBrowseTo.FindNearestClick += new EventHandler(airportBrowseTo_AirportFindNearestClick);
			airportDisplayFrom.NewLocationClick += new EventHandler(airportDisplayFrom_NewLocationClick);
			airportDisplayTo.NewLocationClick += new EventHandler(airportDisplayTo_NewLocationClick);

			airOperatorSelection.OperatorOptionChanged += new EventHandler(airOperatorSelection_OperatorOptionChanged);
			airOperatorSelection.OperatorSelectionChanged += new EventHandler(airOperatorSelection_OperatorSelectionChanged);
			findFlightInputStopover.StopOverAirportChanged += new EventHandler(this.findFlightInputStopover_StopOverAirportChanged);
			findFlightInputStopover.StopoverTimeOutwardChanged += new EventHandler(this.findFlightInputStopover_StopoverTimeOutwardChanged);
			findFlightInputStopover.StopoverTimeReturnChanged += new EventHandler(this.findFlightInputStopover_StopoverTimeReturnChanged);

			this.pageOptionsControl.ShowAdvancedOptions += new System.EventHandler(this.commandAirTravelPreferencesShow_Click);
			this.pageOptionsControl.HideAdvancedOptions += new System.EventHandler(this.commandAirTravelPreferencesHide_Click);
			this.pageOptionsControl.Back += new System.EventHandler(this.commandBack_Click);
			this.pageOptionsControl.Clear += new System.EventHandler(this.commandClear_Click);
			this.pageOptionsControl.Submit += new System.EventHandler(this.commandSubmit_Click);

            // CCN 0427 - Control events added for pageoptionsControlsInPanelCheckInTime and pageOptionsControlsBottom controls
            pageOptionsControlsInPanelCheckInTime.ShowAdvancedOptions += new System.EventHandler(this.commandAirTravelPreferencesShow_Click);
            pageOptionsControlsInPanelCheckInTime.HideAdvancedOptions += new System.EventHandler(this.commandAirTravelPreferencesHide_Click);
            pageOptionsControlsInPanelCheckInTime.Back += new System.EventHandler(this.commandBack_Click);
            pageOptionsControlsInPanelCheckInTime.Clear += new System.EventHandler(this.commandClear_Click);
            pageOptionsControlsInPanelCheckInTime.Submit += new System.EventHandler(this.commandSubmit_Click);

            pageOptionsControlsBottom.ShowAdvancedOptions += new System.EventHandler(this.commandAirTravelPreferencesShow_Click);
            pageOptionsControlsBottom.HideAdvancedOptions += new System.EventHandler(this.commandAirTravelPreferencesHide_Click);
            pageOptionsControlsBottom.Back += new System.EventHandler(this.commandBack_Click);
            pageOptionsControlsBottom.Clear += new System.EventHandler(this.commandClear_Click);
            pageOptionsControlsBottom.Submit += new System.EventHandler(this.commandSubmit_Click);

			dateControl.LeaveDateControl.DateChanged +=
				new EventHandler(dateControlLeaveDateControl_DateChanged);
			dateControl.ReturnDateControl.DateChanged +=
				new EventHandler(dateControlReturnDateControl_DateChanged);

            commandBack.Click += new EventHandler(commandBack_Click);
		}

		/// <summary>
		/// Ensures that the controls on the page are displaying what is stored in the 
		/// FindFlightPageState and TDjourneyParams objects. The references to these
		/// are set in Page_Load. This is where the task of transferring the contents of 
		/// journeyParams to the page is carried out, although most of the work is done
		/// by private methods.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, EventArgs e)
		{

			//bool needed for cases where "Then click 'Next'" text should not be shown after an error message
			bool showNextText = true;	

			this.AmbiguityMode = pageState.AmbiguityMode;

			// Perform initital processing of errors and update top errors label if necessary
			if (pageState.AmbiguityMode)
			{
				//Processing is as follows
				//1. Everything valid is fixed
				//2. If the specified route is invalid, then:
				//   - If one of the locations was specified using the finder, that is fixed and
				//     the other is reset.
				//   - If both locations were specified using the browse control, the from location
				//     is fixed.
				//   - If both locations were specified using the finder, the from location is retained
				//     and the too location is left.
				//4. If the dates are invalid, processing duplicates the multi modal planner.

				errors = TDSessionManager.Current.ValidationError;
				
				// Check that origin and destination locations were valid
				if ( !IsValidOrigin)
				{
					// This can only have come about if they didn't select an origin location. 
					// Reset it to be sure
					pageState.ResetOriginLocation();
					journeyParams.SetOriginDetails(null, null);
				}
				else
				{
					// The errors may not contain any errors for origin, but it may still need to be
					// specified if "new location" has been clicked. To check this, see if there are
					// any airports in the params
					pageState.OriginLocationFixed = (journeyParams.OriginSelectedAirports().Length != 0);
				}

				if (!IsValidDestination)
				{
					// This can only have come about if they didn't select an origin location. 
					// Reset it to be sure
					pageState.ResetDestinationLocation();
					journeyParams.SetDestinationDetails(null, null);
				}
				else
				{
					// The errors may not contain any errors for origin, but it may still need to be
					// specified if "new location" has been clicked. To check this, see if there are
					// any airports in the params
					pageState.DestinationLocationFixed = (journeyParams.DestinationSelectedAirports().Length != 0);
				}
				
				// If they were both valid, they may have represented an invalid route
				if (IsValidOrigin && IsValidDestination && !IsValidRoute)
				{
					pageState.OriginLocationFixed = true;
					pageState.ResetDestinationLocation();
				}

				labelErrorMessages.Text = string.Empty;

				// Display "select options from the highlighted sections" for those areas that are
				// signified with yellow fields
				if ( !( IsValidOrigin && IsValidDestination && IsValidRoute && IsValidStopover && IsValidOperatorSelection ) )
					labelErrorMessages.Text += rm.GetString("ValidateAndRun.FindFlightCorrectHighlighted", TDCultureInfo.CurrentUICulture) + " ";

				//IR1603 - distinguish how origin and destination were selected before setting error messages
				bool browseControlUsed = ((pageState.OriginLocationSelectionMethod == FlightLocationSelectionMethod.BrowseControl) ||
					(pageState.DestinationLocationSelectionMethod == FlightLocationSelectionMethod.BrowseControl));			
				string resourceID = String.Empty;
				string prefixError = String.Empty;
				prefixError = browseControlUsed ? "Flight" : "FindNearestFlight";
				
				//IR1507 - show error message if origin and destination locations have overlapping airports
				if ((IsOriginAndDestinationOverlapping) && (labelErrorMessages.Text == string.Empty))
				{
					resourceID = String.Format("ValidateAndRun.{0}OriginAndDestinationOverlap",prefixError);
					labelErrorMessages.Text += rm.GetString(resourceID, TDCultureInfo.CurrentUICulture) + " ";
					showNextText = false;		
				}

				//IR1507 - show error message if origin and via locations have overlapping airports
				if ((IsOriginAndViaOverlapping)  && (labelErrorMessages.Text == string.Empty))
				{
					resourceID = String.Format("ValidateAndRun.{0}OriginAndViaOverlap",prefixError);
					labelErrorMessages.Text += rm.GetString(resourceID, TDCultureInfo.CurrentUICulture) + " ";
					showNextText = false;		
				}

				//IR1507 - show error message if destination and via locations have overlapping airports
				if ((IsDestinationAndViaOverlapping)  && (labelErrorMessages.Text == string.Empty))
				{
					resourceID = String.Format("ValidateAndRun.{0}DestinationAndViaOverlap",prefixError);
					labelErrorMessages.Text += rm.GetString(resourceID, TDCultureInfo.CurrentUICulture) + " ";
					showNextText = false;		
				}				

				// Add "then click next" but only if there was error text displayed and
				//not showing an 'overlapping' error message
				if ((labelErrorMessages.Text.Length != 0) && (showNextText))		
					labelErrorMessages.Text += rm.GetString("ValidateAndRun.ClickNext", TDCultureInfo.CurrentUICulture);
			}
			

			// Now make sure all the controls are displaying the correct values 
			// from TDjourneyParams and FindFlightPageState

			UpdateDirectFlightsOnly();
			UpdateOriginLocation();
			UpdateDestinationLocation();

			// Apply restrictions to the airport browse controls if necessary
			if (journeyParams.DirectFlightsOnly && pageState.OriginLocationFixed && !pageState.DestinationLocationFixed)
				airportBrowseTo.Restrict( airData.GetValidDestinationAirports(journeyParams.OriginSelectedAirports()) );
			else if (journeyParams.DirectFlightsOnly && !pageState.OriginLocationFixed && pageState.DestinationLocationFixed)
				airportBrowseFrom.Restrict( airData.GetValidOriginAirports(journeyParams.DestinationSelectedAirports()) );

			inputDateAdapter.UpdateDateControl(dateControl, pageState.AmbiguityMode, journeyParams, TDSessionManager.Current.ValidationError);

			UpdateTravelPreferences();

			UpdateControlLinks();
            commandBack.Visible = pageState.AmbiguityMode;
            pageOptionsControlsBottom.AllowBack = false;
            pageOptionsControl.AllowBack = false;

            //Hide the advanced text if required.
            if (panelAirTravelPreferences.Visible || pageState.AmbiguityMode)
            {
                BlankPanelText(TDPageInformationHtmlPlaceHolderDefinition);
            }
		}

		#endregion

		#region Button click event handlers

		/// <summary>
		/// Resets the page, clearing all data.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commandClear_Click(object sender, System.EventArgs e)
		{
			TDPage.CloseAllSingleWindows(this);
			journeyParams.Initialise();
			pageState.Initialise();
			LoadTravelDetails();
		}

		/// <summary>
		/// Returns the user to the input page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commandBack_Click(object sender, System.EventArgs e)
		{
			
			// if Calendar is open close it
			dateControl.CalendarClose();
			//Check to see if there is a previous page waiting on the stack
			if(TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Count != 0)
			{
				TransitionEvent lastPage = (TransitionEvent)TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Pop();
				//If the user is returing to the previous journey results, re-validate them
				if(lastPage == TransitionEvent.FindAInputRedirectToResults)
				{
					TDSessionManager.Current.JourneyResult.IsValid = true;
				}
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = lastPage;
				return;
			}
			
			pageState.AmbiguityMode = false;

			dateControl.LeaveDateControl.AmbiguityMode = false;
			dateControl.ReturnDateControl.AmbiguityMode = false;
			dateControl.LeaveDateControl.DateErrors = null;
			dateControl.ReturnDateControl.DateErrors = null;

			// Reset the Origin/Destination to editable on click back if they were selected using the 
			// browse tool. Leave them as they are if they were selected with Find nearest airport - they
			// can always change them using New Location.
			if (pageState.OriginLocationSelectionMethod == FlightLocationSelectionMethod.BrowseControl)
				pageState.OriginLocationFixed = false;

			if (pageState.DestinationLocationSelectionMethod == FlightLocationSelectionMethod.BrowseControl)
				pageState.DestinationLocationFixed = false;

			TDSessionManager.Current.FormShift[SessionKey.ForceRedirect] = true;
		}

		/// <summary>
		/// Uses JourneyPlanRunner to validate the data, and redirects to the wait page
		/// if necessary.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commandSubmit_Click(object sender, System.EventArgs e)
		{
			SubmitRequest();
		}
		
		/// <summary>
		/// Method containing the commandSubmit_Click method functionality so that it
		/// can be called by the Landing page when AutoPlanning is required. 
		/// </summary>
		private void SubmitRequest()
		{
			
			// if Calendar is open close it
			dateControl.CalendarClose();
			if (timeout != true)
			{

				// Journey Plan Runner
				SaveTravelDetails();

				// This is a workaround because for some unknown reason the OperatorSelectionChanged event is not fired by
				// an OperatorSelectionControl object when all checkboxes are unselected. Hence we read the selected
				// operators at this point providing the control is visible and not in read only mode (in which case no
				// selection would be returned)
				if (airOperatorSelection.Visible && airOperatorSelection.DisplayMode != OperatorSelectionDisplayMode.ReadOnly ) 
				{
					airOperatorSelection_OperatorSelectionChanged(this,null);
				}

				AsyncCallState acs = new JourneyPlanState();
				// Determine refresh interval and resource string for the wait page
				acs.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.FindAFlight"]);
				acs.WaitPageMessageResourceFile = "langStrings";
				acs.WaitPageMessageResourceId = "WaitPageMessage.FindAFlight";
				acs.AmbiguityPage = PageId.FindFlightInput;
                acs.DestinationPage = PageId.JourneyDetails;
                acs.ErrorPage = PageId.JourneyDetails;
				TDSessionManager.Current.AsyncCallState = acs;

				JourneyPlanRunner.IJourneyPlanRunner runner = new JourneyPlanRunner.FlightJourneyPlanRunner(rm);
				if (runner.ValidateAndRun(TDSessionManager.Current, TDSessionManager.Current.JourneyParameters, GetChannelLanguage(TDPage.SessionChannelName)))
				{

					// Extending journeys using a Find A is not currently possible so clear down the itinerary
					TDItineraryManager itineraryManager = TDItineraryManager.Current;
					if (itineraryManager.Length >= 1 ) 
					{
						itineraryManager.ResetItinerary();
					}

					pageState.OriginLocationFixed = true;
					pageState.DestinationLocationFixed = true;

					TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindFlightInputOk;
				}
				else
				{
					// We are in ambiguity mode. Ensure that things are displayed correctly for this
					pageState.AmbiguityMode = true;
					TDPage.CloseAllSingleWindows(this);
				}
			}
			else
			{
				//do something
			}
		}

		/// <summary>
		/// Handles the AirportFindNearestClick event of the from airport browse control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void airportBrowseFrom_AirportFindNearestClick(object sender, EventArgs e)
		{
			ITDSessionManager sm = TDSessionManager.Current;
			sm.FindStationPageState.LocationType = FindStationPageState.CurrentLocationType.From;
			sm.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindFlightInputToStationFinder;
		}

		/// <summary>
		/// Handles the AirportFindNearestClick event of the to airport browse control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void airportBrowseTo_AirportFindNearestClick(object sender, EventArgs e)
		{
			ITDSessionManager sm = TDSessionManager.Current;
			sm.FindStationPageState.LocationType = FindStationPageState.CurrentLocationType.To;
			sm.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindFlightInputToStationFinder;
		}

		/// <summary>
		/// Handles a click on the "New Location" button in the "from" airport display control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void airportDisplayFrom_NewLocationClick(object sender, EventArgs e)
		{
			journeyParams.SetOriginDetails(null, null);
			pageState.ResetOriginLocation();
			journeyParams.SelectedOperators = new string[0];
			journeyParams.OnlyUseSpecifiedOperators = false;
		}

		/// <summary>
		/// Handles a click on the "New Location" button in the "to" airport display control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void airportDisplayTo_NewLocationClick(object sender, EventArgs e)
		{
			journeyParams.SetDestinationDetails(null, null);
			pageState.ResetDestinationLocation();
			journeyParams.SelectedOperators = new string[0];
			journeyParams.OnlyUseSpecifiedOperators = false;
		}

		#endregion

		#region Control event handlers

		/// <summary>
		/// This method reads the values of controls that do not raise events to indicate that
		/// they have changed. It should be called by every control that can initiate a postback
		/// (other than the "Clear" controls)
		/// </summary>
		private void UpdateOtherControls()
		{
			journeyParams.SaveDetails = loginSaveOption.SaveDetails;
			inputDateAdapter.UpdateJourneyDates(dateControl, pageState.AmbiguityMode, journeyParams, TDSessionManager.Current.ValidationError);
		}

		/// <summary>
		/// Handles the click event for the "Find Direct Flights Only" "checkbox" image.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commandDirectFlightsOnly_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			journeyParams.DirectFlightsOnly = !journeyParams.DirectFlightsOnly;

			if (journeyParams.DirectFlightsOnly)
			{
				journeyParams.ViaSelectedAirport = null;
				journeyParams.OutwardStopover = 0;
				journeyParams.ReturnStopover = 0;
			}
		}

		/// <summary>
		/// Handler for the "Show travel details" button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commandAirTravelPreferencesShow_Click(object sender, System.EventArgs e)
		{
			pageState.TravelDetailsVisible = true;
		}

		/// <summary>
		/// Handler for the "Hide travel details" button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void commandAirTravelPreferencesHide_Click(object sender, System.EventArgs e)
		{
			pageState.TravelDetailsVisible = false;
		}

		/// <summary>
		/// Handles the SelectedIndexChanged event of the dropCheckInTime control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void dropCheckInTime_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				// None of the possible exceptions from Convert.ToInt32 should ever happen, but
				// they are checked for completeness.
				journeyParams.ExtraCheckInTime = Convert.ToInt32( dropCheckInTime.SelectedItem.Value );
				pageState.TravelDetailsChanged = true;
			}
			catch ( ArgumentException )
			{
				// Happens when Convert.ToInt32 is passed a null reference.
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "The data for CheckInTimeDrop contains invalid Resource IDs - all Resource IDs should be numeric."));
				journeyParams.ExtraCheckInTime = 0;
			}
			catch ( FormatException )
			{
				// Happens when Convert.ToInt32 is passed something that isn't a number
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "The data for CheckInTimeDrop contains invalid Resource IDs - all Resource IDs should be numeric."));
				journeyParams.ExtraCheckInTime = 0;
			}
			catch ( OverflowException )
			{
				// Happens when Convert.ToInt32 is passed a number that can't be represented
				// in 32 bits.
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "The data for CheckInTimeDrop contains invalid Resource IDs - all Resource IDs should be numeric."));
				journeyParams.ExtraCheckInTime = 0;
			}
		}

		/// <summary>
		/// Handles the StopOverAirport changed event of the stopover control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void findFlightInputStopover_StopOverAirportChanged(object sender, EventArgs e)
		{
			journeyParams.ViaSelectedAirport = findFlightInputStopover.StopOverAirport;
			pageState.TravelDetailsChanged = true;
		}

		/// <summary>
		/// Handles the StopoverTimeOutwardChanged event of the stopover control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void findFlightInputStopover_StopoverTimeOutwardChanged(object sender, EventArgs e)
		{
			journeyParams.OutwardStopover = findFlightInputStopover.StopoverTimeOutward;
			pageState.TravelDetailsChanged = true;
		}

		/// <summary>
		/// Handles the StopoverTimeReturnChanged event of the stopover control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void findFlightInputStopover_StopoverTimeReturnChanged(object sender, EventArgs e)
		{
			journeyParams.ReturnStopover = findFlightInputStopover.StopoverTimeReturn;
			pageState.TravelDetailsChanged = true;
		}

		/// <summary>
		/// Handles the OperatorOptionChanged event of the stopover control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void airOperatorSelection_OperatorOptionChanged(object sender, EventArgs e)
		{
			journeyParams.OnlyUseSpecifiedOperators = airOperatorSelection.OnlyUseSpecifiedOperators;
			pageState.TravelDetailsChanged = true;
		}

		/// <summary>
		/// Handles the OperatorSelectionChanged event of the stopover control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void airOperatorSelection_OperatorSelectionChanged(object sender, EventArgs e)
		{
			journeyParams.SelectedOperators = airOperatorSelection.SelectedOperators;
			pageState.TravelDetailsChanged = true;
			pageState.AmendMode = journeyParams.SelectedOperators.Length != 0;
		}

		/// <summary>
		/// Handles the AirportSelectionChanged event of the from airport browse control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void airportBrowseFrom_AirportSelectionChanged(object sender, EventArgs e)
		{
			journeyParams.SetOriginDetails(airportBrowseFrom.SelectedRegion, airportBrowseFrom.SelectedAirports);
		}

		/// <summary>
		/// Handles the AirportSelectionChanged event of the to airport browse control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void airportBrowseTo_AirportSelectionChanged(object sender, EventArgs e)
		{
			journeyParams.SetDestinationDetails(airportBrowseTo.SelectedRegion, airportBrowseTo.SelectedAirports);
		}

		#endregion

		#region Private properties

		/// <summary>
		/// Switches the visibility of the from to title/error message labels
		/// </summary>
		private bool AmbiguityMode
		{
			get { return labelErrorMessages.Visible; }
			set 
			{
				labelErrorMessages.Visible = value;
				labelFromToTitle.Visible = !value;
			}
		}

		/// <summary>
		/// Valid origin is indicated by there being no errors to do with the origin. Also, the number of
		/// selected airports is checked, as the location can also be "invalid" if the "new location" 
		/// button has been clicked in ambiguity mode.
		/// </summary>
		private bool IsValidOrigin
		{
			get 
			{
				bool errorExists = (errors.Contains(ValidationErrorID.OriginLocationInvalid) || errors.Contains(ValidationErrorID.OriginLocationInvalidAndOtherErrors) || errors.Contains(ValidationErrorID.OriginLocationHasNoNaptan));
				return (!errorExists && (journeyParams.OriginSelectedAirports().Length != 0 ))|| (journeyParams.OriginLocation.Status == TDLocationStatus.Valid);

			}
		}

		/// <summary>
		/// Valid destination is indicated by there being no errors to do with the destination. Also, 
		/// the number of selected airports is checked, as the location can also be "invalid" if the 
		/// "new location" button has been clicked in ambiguity mode.
		/// </summary>
		private bool IsValidDestination
		{
			get 
			{
				bool errorExists = (errors.Contains(ValidationErrorID.DestinationLocationInvalid) || errors.Contains(ValidationErrorID.DestinationLocationInvalidAndOtherErrors) || errors.Contains(ValidationErrorID.DestinationLocationHasNoNaptan));
				return (!errorExists && (journeyParams.DestinationSelectedAirports().Length != 0)) || (journeyParams.DestinationLocation.Status == TDLocationStatus.Valid);

			}
		}

		private bool IsValidRoute
		{
			get { return !errors.Contains(ValidationErrorID.NoValidRoutes); } 
		}

		private bool IsValidStopover
		{
			get { return !(errors.Contains(ValidationErrorID.PublicViaLocationInvalid) || errors.Contains(ValidationErrorID.PublicViaLocationInvalidAndOtherErrors)); }
		}

		private bool IsValidOperatorSelection
		{
			get { return !(errors.Contains(ValidationErrorID.InvalidOperatorSelection)); }
		}

		/// <summary>
		/// IR1507 - check for overlapping airport naptans in from and to locations
		/// </summary>
		private bool IsOriginAndDestinationOverlapping
		{
			get { return (errors.Contains(ValidationErrorID.OriginAndDestinationOverlap)); }
		}

		/// <summary>
		/// IR1507 - check for overlapping airport naptans in from and via locations
		/// </summary>
		private bool IsOriginAndViaOverlapping
		{
			get { return (errors.Contains(ValidationErrorID.OriginAndViaOverlap)); }
		}

		/// <summary>
		///  IR1507 - check for overlapping airport naptans in to and via locations
		/// </summary>
		private bool IsDestinationAndViaOverlapping
		{
			get { return (errors.Contains(ValidationErrorID.DestinationAndViaOverlap)); }
		}		


		/// <summary>
		/// IR1619 - IsFindNearest returns true if the 'Find Nearest' functionality has been used to choose the 
		/// 'From' or 'To' location
		/// </summary>
		private bool IsFindNearest
		{
			get 
			{				
				return ((TDSessionManager.Current.JourneyParameters.OriginLocation.SearchType == SearchType.FindNearest) ||
					(TDSessionManager.Current.JourneyParameters.DestinationLocation.SearchType == SearchType.FindNearest));
			}
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Sets whether the "Direct flights only" checkbox is ticked. This also controls
		/// whether or not the "fly via" panel is visible, and whether the "from" dropdown is
		/// linked to the "to" dropdown.
		/// </summary>
		private void UpdateDirectFlightsOnly()
		{
			if ( pageState.AmbiguityMode && pageState.OriginLocationFixed && pageState.DestinationLocationFixed  )
			{
				commandDirectFlightsOnly.Visible = false;
				labelFindDirectFlightsTitle.Visible = false;
				labelFindDirectFlightsFixedAll.Visible = !journeyParams.DirectFlightsOnly;
				labelFindDirectFlightsFixedDirect.Visible = journeyParams.DirectFlightsOnly;
				labelSRcommandDirectFlightsOnly.Visible = false;
			}
			else
			{
				commandDirectFlightsOnly.Visible = true;
				labelFindDirectFlightsTitle.Visible = true;
				labelFindDirectFlightsFixedAll.Visible = false;
				labelFindDirectFlightsFixedDirect.Visible = false;
				labelSRcommandDirectFlightsOnly.Visible = true;

				if (journeyParams.DirectFlightsOnly)
				{
					commandDirectFlightsOnly.ImageUrl = rm.GetString("FindFlightInput.commandDirectFlightsOnly.True.ImageUrl" ,TDCultureInfo.CurrentUICulture);
					commandDirectFlightsOnly.AlternateText = rm.GetString("FindFlightInput.commandDirectFlightsOnly.True.AlternateText" ,TDCultureInfo.CurrentUICulture);
				}
				else
				{
					commandDirectFlightsOnly.ImageUrl = rm.GetString("FindFlightInput.commandDirectFlightsOnly.False.ImageUrl" ,TDCultureInfo.CurrentUICulture);
					commandDirectFlightsOnly.AlternateText = rm.GetString("FindFlightInput.commandDirectFlightsOnly.False.AlternateText" ,TDCultureInfo.CurrentUICulture);
				}				
			}
			if (pageState.AmbiguityMode) 
			{
				panelFlyVia.Visible = !findFlightInputStopover.AllOptionsDefault();
			} 
			else 
			{
				panelFlyVia.Visible = !journeyParams.DirectFlightsOnly;
			}
		}

		/// <summary>
		/// Updates the Air Travel Preferences panel if necessary.
		/// </summary>
		private void UpdateTravelPreferences()
		{

			panelAirTravelPreferences.Visible = true; // set to visible otherwise contained controls cannot be set visible

			UpdateOperatorSelection();

			if (pageState.AmbiguityMode) 
			{
				loginSaveOption.Visible = false;
				loginSaveOption.LoggedOutDisplay();
				pageOptionsControl.AllowBack = true;
				pageOptionsControl.AllowClear = true;

				bool showOptions = !AllOptionsDefault() || panelOperatorSelection.Visible;

				hidePreferencesBar.Visible = showOptions;
				panelAirTravelPreferences.Visible = showOptions;

                //CCN 0427 setting visibility of the pageOptionsControls
                pageOptionsControl.Visible = true;
                pageOptionsControlsInPanelCheckInTime.Visible = false;
                pageOptionsControlsBottom.Visible = false;

			} 
			else 
			{
				if ( pageState.TravelDetailsVisible ) 
				{
					loginSaveOption.Visible = true;
					pageOptionsControl.AllowHideAdvancedOptions = true;
					pageOptionsControl.AllowBack = false;
					if(TDSessionManager.Current.Authenticated) 
						loginSaveOption.LoggedInDisplay();
					else 
						loginSaveOption.LoggedOutDisplay();

					hidePreferencesBar.Visible = true;
					panelAirTravelPreferences.Visible = true;

                    // CCN 0427 Setting options and visibility of the pageOptionsControls
                    pageOptionsControlsInPanelCheckInTime.AllowHideAdvancedOptions = true;
                    pageOptionsControlsInPanelCheckInTime.AllowBack = false;
                    pageOptionsControlsInPanelCheckInTime.Visible = true;
                    pageOptionsControl.Visible = pageOptionsControlsBottom.Visible = false;

				} 
				else 
				{
					hidePreferencesBar.Visible = false;
					panelAirTravelPreferences.Visible = false; 

					pageOptionsControl.AllowShowAdvancedOptions = true;

                    // CCN 0427 Setting visibility of the pageOptionsControls
                    pageOptionsControlsInPanelCheckInTime.Visible = false;
                    pageOptionsControlsBottom.Visible = false;
                    pageOptionsControl.Visible = true;
				}            

				pageOptionsControl.AllowClear = true;
				pageOptionsControl.AllowBack = false;

			}

            if (!journeyParams.DirectFlightsOnly)
            {
                UpdateStopoverDetails();

                if (panelAirTravelPreferences.Visible)
                {
                    // CCN 0427 Setting options and visibility of the pageOptionsControls
                    pageOptionsControlsBottom.AllowHideAdvancedOptions = true;
                    pageOptionsControlsBottom.AllowBack = false;
                    pageOptionsControlsBottom.Visible = true;
                    pageOptionsControl.Visible = pageOptionsControlsInPanelCheckInTime.Visible = false;
                }
            }
                
			UpdateCheckInTime();
		}

		/// <summary>
		/// Handles displaying the correct data in the "From" area.
		/// </summary>
		private void UpdateOriginLocation()
		{
			if (pageState.OriginLocationFixed)
			{
                if (pageState.AmendMode != true || TDSessionManager.Current.JourneyParameters.OriginLocation.SearchType == SearchType.FindNearest)
                    
                {
                    airportBrowseFrom.Visible = false;
                    airportDisplayFrom.Visible = true;
                    if (journeyParams.OriginSelectedRegion() != null)
                        airportDisplayFrom.SetData(journeyParams.OriginSelectedRegion(), journeyParams.OriginSelectedAirports());
                    else
                        airportDisplayFrom.SetData(journeyParams.OriginSelectedAirports(), resolvedFromLocation);
                }
                else
                {
                    // Do not display a resolved location list if in Amend mode - FIX for USD UK:4922052
                    airportBrowseFrom.Visible = true;
                    airportDisplayFrom.Visible = false;
                    if (journeyParams.OriginSelectedRegion() != null || journeyParams.OriginSelectedAirports() != null)
                    {
                        airportBrowseFrom.SetData(journeyParams.OriginSelectedRegion(), journeyParams.OriginSelectedAirports());
                    }
                }
			}
			else
			{
				airportBrowseFrom.Visible = true;
				airportDisplayFrom.Visible = false;
				airportBrowseFrom.SetData(journeyParams.OriginSelectedRegion(), journeyParams.OriginSelectedAirports());
				if (pageState.AmbiguityMode)
				{
					airportBrowseFrom.DisplayAsAmbiguity = true;
					if (IsValidOrigin)
						// No valid route
						airportBrowseFrom.AmbiguityMessage = rm.GetString("ValidateAndRun.NoValidRoutes", TDCultureInfo.CurrentUICulture);
					else
						// No selection
						airportBrowseFrom.AmbiguityMessage = rm.GetString("ValidateAndRun.SelectAnOrigin", TDCultureInfo.CurrentUICulture);
						
				}
				else
				{
					airportBrowseFrom.DisplayAsAmbiguity = false;
				}
				
			}
		}

		/// <summary>
		/// Handles displaying the correct data in the "To" area.
		/// </summary>
		private void UpdateDestinationLocation()
		{
			if (pageState.DestinationLocationFixed)
			{
                if (pageState.AmendMode != true || TDSessionManager.Current.JourneyParameters.DestinationLocation.SearchType == SearchType.FindNearest)
                {
                    airportBrowseTo.Visible = false;
                    airportDisplayTo.Visible = true;
                    if (journeyParams.DestinationSelectedRegion() != null)
                        airportDisplayTo.SetData(journeyParams.DestinationSelectedRegion(), journeyParams.DestinationSelectedAirports());
                    else
                        airportDisplayTo.SetData(journeyParams.DestinationSelectedAirports(), resolvedToLocation);
                }
                else
                {
                    // Do not display a resolved location list if in Amend mode - FIX for USD UK:4922052
                    airportBrowseTo.Visible = true;
                    airportDisplayTo.Visible = false;
                    if (journeyParams.DestinationSelectedRegion() != null || journeyParams.DestinationSelectedAirports() != null)
                    {
                        airportBrowseTo.SetData(journeyParams.DestinationSelectedRegion(), journeyParams.DestinationSelectedAirports());
                    }
                }
			}
			else
			{
				airportBrowseTo.Visible = true;
				airportDisplayTo.Visible = false;
				airportBrowseTo.SetData(journeyParams.DestinationSelectedRegion(), journeyParams.DestinationSelectedAirports());
				if (pageState.AmbiguityMode)
				{
					airportBrowseTo.DisplayAsAmbiguity = true;
					if (IsValidDestination)
						// No valid route
						airportBrowseTo.AmbiguityMessage = rm.GetString("ValidateAndRun.NoValidRoutes", TDCultureInfo.CurrentUICulture);
					else
						// No selection
						airportBrowseTo.AmbiguityMessage = rm.GetString("ValidateAndRun.SelectADestination", TDCultureInfo.CurrentUICulture);
						
				}
				else
				{
					airportBrowseTo.DisplayAsAmbiguity = false;
				}
			}
		}

		/// <summary>
		/// Updates the additional check in time controls
		/// </summary>
		private void UpdateCheckInTime()
		{

			panelCheckInTime.Visible = (!pageState.AmbiguityMode && pageState.TravelDetailsVisible) ||
				(pageState.AmbiguityMode && !CheckInTimeIsDefault());

			if (panelCheckInTime.Visible) 
			{
				populator.Select(dropCheckInTime, journeyParams.ExtraCheckInTime.ToString());

				dropCheckInTime.Visible = !pageState.AmbiguityMode;
				labelCheckInTimeTitle.Visible = !pageState.AmbiguityMode;
				labelCheckInTimeExplanation.Visible = !pageState.AmbiguityMode;
				labelCheckInTimeNote.Visible = !pageState.AmbiguityMode;
				labelCheckInTimeFixed.Visible = pageState.AmbiguityMode;
				labelCheckInTimeDropTitle.Visible = !labelCheckInTimeFixed.Visible;
				labelSRdropCheckInTime.Visible = !pageState.AmbiguityMode;
				if (pageState.AmbiguityMode)
				{
					labelCheckInTimeFixed.Text = GetResource("panelAirTravelPreferences.labelCheckInTimeDropTitle") + ": " + dropCheckInTime.SelectedItem.Text;
				}
			}

		}

		/// <summary>
		/// Updates the operairOperatorSelectionator selection control
		/// </summary>
		private void UpdateOperatorSelection()
		{
			airOperatorSelection.SelectedOperators = journeyParams.SelectedOperators;
			airOperatorSelection.OnlyUseSpecifiedOperators = journeyParams.OnlyUseSpecifiedOperators;

			if (pageState.AmbiguityMode)
			{
				if (IsValidOperatorSelection)
				{
					if ((!pageState.OriginLocationFixed || !pageState.DestinationLocationFixed)
						&& pageState.AmendMode) 
					{
						airOperatorSelection.DisplayMode = OperatorSelectionDisplayMode.Normal;
						panelOperatorSelection.Visible = true;
					} 
					else 
					{
						airOperatorSelection.DisplayMode = OperatorSelectionDisplayMode.ReadOnly;
						panelOperatorSelection.Visible = journeyParams.SelectedOperators.Length != 0;
					}
				}
				else
				{
					airOperatorSelection.DisplayMode = OperatorSelectionDisplayMode.Ambiguity;
					airOperatorSelection.AmbiguityMessage = rm.GetString("ValidateAndRun.InvalidOperatorSelection", TDCultureInfo.CurrentUICulture);
					panelOperatorSelection.Visible = true;
				}                    
			}
			else
			{
				airOperatorSelection.DisplayMode = OperatorSelectionDisplayMode.Normal;
				panelOperatorSelection.Visible = true;
			}
			
			if (journeyParams.DirectFlightsOnly && (airOperatorSelection.DisplayMode != OperatorSelectionDisplayMode.ReadOnly ))
			{
				Airport[] originAirports = journeyParams.OriginSelectedAirports();
				Airport[] destinationAirports = journeyParams.DestinationSelectedAirports();
				// Apply restrictions for the fixed airports - get a list of valid operators
				if ( pageState.OriginLocationFixed && pageState.DestinationLocationFixed )
					// Restrict based on routes
					airOperatorSelection.RestrictOperators(airData.GetRouteOperators(originAirports, destinationAirports));
				else if (pageState.OriginLocationFixed)
					// Restrict based on origin airport
					airOperatorSelection.RestrictOperators(airData.GetAirportOperators(originAirports));
				else if (pageState.DestinationLocationFixed)
					// Restrict based on destination airport
					airOperatorSelection.RestrictOperators(airData.GetAirportOperators(destinationAirports));
				else
					// No restrictions
					airOperatorSelection.RemoveOperatorRestrictions();
			}
		}

		/// <summary>
		/// Updates the stopover details control
		/// </summary>
		private void UpdateStopoverDetails()
		{
			// Set fly via
			findFlightInputStopover.StopOverAirport = journeyParams.ViaSelectedAirport;
			findFlightInputStopover.StopoverTimeOutward = journeyParams.OutwardStopover;
			findFlightInputStopover.StopoverTimeReturn = journeyParams.ReturnStopover;

			if (pageState.AmbiguityMode)
			{
				if (IsValidStopover)
				{
					findFlightInputStopover.ReadOnly = true;
					findFlightInputStopover.FlyViaAmbiguityMessage = string.Empty;
				}
				else
				{
					findFlightInputStopover.ReadOnly = false;
					findFlightInputStopover.FlyViaAmbiguityMessage = rm.GetString("ValidateAndRun.InvalidViaSelection", TDCultureInfo.CurrentUICulture);;
				}

			}
			else
			{
				findFlightInputStopover.ReadOnly = false;
				findFlightInputStopover.FlyViaAmbiguityMessage = string.Empty;
			}
		}

		/// <summary>
		/// Updates the links between the origin, destination and operator selection 
		/// controls. These enable Javascript to be used to update the control when 
		/// the value in one is changed.
		/// </summary>
		private void UpdateControlLinks()
		{
			// Get the value to pass for operator selection control
			string operatorSelectionName = string.Empty;
			if (journeyParams.DirectFlightsOnly && pageState.TravelDetailsVisible && airOperatorSelection.DisplayMode != OperatorSelectionDisplayMode.ReadOnly)
				operatorSelectionName = airOperatorSelection.CheckBoxListClientId;

			// First do the origin control
			if (!pageState.OriginLocationFixed)
			{
				if (pageState.DestinationLocationFixed)
				{
					airportBrowseFrom.TargetControlName = airportDisplayTo.AirportIataCodesControlId;
					airportBrowseFrom.UpdateTargetControl = false;
				}
				else
				{
					airportBrowseFrom.TargetControlName = airportBrowseTo.DropDownClientID;
					airportBrowseFrom.UpdateTargetControl = journeyParams.DirectFlightsOnly;
				}

				airportBrowseFrom.OperatorSelectionControlName = operatorSelectionName;
			}

			if (!pageState.DestinationLocationFixed)
			{
				if (pageState.OriginLocationFixed)
					airportBrowseTo.TargetControlName = airportDisplayFrom.AirportIataCodesControlId;
				else
					airportBrowseTo.TargetControlName = airportBrowseFrom.DropDownClientID;
					
				airportBrowseTo.UpdateTargetControl = false;

				airportBrowseTo.OperatorSelectionControlName = operatorSelectionName;
			}
			
		}

		/// <summary>
		/// Returns true if all travel options (excluding operator selection) are set to their default drop down
		/// values, false otherwise
		/// </summary>
		/// <returns>true if all travel options (excluding operator selection) are set to their default drop down, 
		/// false otherwise</returns>
		private bool AllOptionsDefault() 
		{
			return findFlightInputStopover.AllOptionsDefault() &&
				CheckInTimeIsDefault();
		}

		/// <summary>
		/// Returns true if the choosen check in time is the drop down default value, false otherwise
		/// </summary>
		/// <returns></returns>
		private bool CheckInTimeIsDefault() 
		{
			string defaultItemValue = populator.GetDefaultListControlValue(DataServiceType.CheckInTimeDrop);
			return journeyParams.ExtraCheckInTime == Convert.ToInt32( defaultItemValue );
		}

		/// <summary>
		/// Event handler called when date selected from calendar control. The journey parameters for the outward
		/// date are updated with the calendar date selection.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void dateControlLeaveDateControl_DateChanged(object sender, EventArgs e) 
		{
			journeyParams.OutwardDayOfMonth = dateControl.LeaveDateControl.DateControl.Day;
			journeyParams.OutwardMonthYear = dateControl.LeaveDateControl.DateControl.MonthYear;
		}

		/// <summary>
		/// Event handler called when date selected from calendar control. The journey parameters for the return
		/// date are updated with the calendar date selection.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void dateControlReturnDateControl_DateChanged(object sender, EventArgs e) 
		{
			journeyParams.ReturnDayOfMonth = dateControl.ReturnDateControl.DateControl.Day;
			journeyParams.ReturnMonthYear = dateControl.ReturnDateControl.DateControl.MonthYear;
		}

		#endregion

		#region Load/save travel details

		/// <summary>
		/// Loads travel details from the user preferences
		/// </summary>
		public void LoadTravelDetails()
		{
			// Additional check for JourneyParameters is TDJourneyParametersFlight just to be on the 
			// side, although this is really paranoia!
			if (TDSessionManager.Current.Authenticated && (TDSessionManager.Current.JourneyParameters is TDJourneyParametersFlight)) 
			{
				TDProfile travelPreferences = TDSessionManager.Current.CurrentUser.UserProfile;
				ProfileProperties curr;
			
				curr = travelPreferences.Properties["FindFlightDetails.ExtraCheckInTime"];
				if (curr != null && curr.Value != null)
					((TDJourneyParametersFlight)TDSessionManager.Current.JourneyParameters).ExtraCheckInTime = (int)curr.Value;
			}
		}

		/// <summary>
		/// Saves travel details to the user preferences
		/// </summary>
		public void SaveTravelDetails() 
		{
			if (TDSessionManager.Current.Authenticated && loginSaveOption.SaveDetails) 
			{
				TDProfile travelPreferences = TDSessionManager.Current.CurrentUser.UserProfile;
				travelPreferences.Properties["FindFlightDetails.ExtraCheckInTime"].Value = journeyParams.ExtraCheckInTime;
				travelPreferences.Update();
			}
		}

		#endregion

		#region Web Form Designer generated code

		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.commandDirectFlightsOnly.Click += new System.Web.UI.ImageClickEventHandler(this.commandDirectFlightsOnly_Click);
            this.dropCheckInTime.SelectedIndexChanged += new EventHandler(this.dropCheckInTime_SelectedIndexChanged);
		}
		#endregion
	}
}
