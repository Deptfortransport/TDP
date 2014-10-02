// *********************************************** 
// NAME                 : FindTrunkInput.aspx.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 28/07/2004 
// DESCRIPTION          : Input page for trunk journeys.  
//                        Allows specification of journey
//						  requirements for multiple
//						  public transport journeys. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindTrunkInput.aspx.cs-arc  $ 
//
//   Rev 1.14   Jul 28 2011 16:20:14   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.13   Oct 29 2010 09:11:20   RBroddle
//Removed explicit wire up to Page_Init & Page_PreRender as AutoEventWireUp=true for this page so they were firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.12   May 13 2010 13:05:26   mmodi
//Added code call to clear the printable map session information 
//Resolution for 5535: Printable maps session logic improvement
//
//   Rev 1.11   May 13 2010 10:45:26   apatel
//Updated to resolve the issue with locations not populating automatically when journey planned from find nearest
//Resolution for 5529: Issue with plan a journey with find nearest
//
//   Rev 1.10   May 04 2010 16:33:20   apatel
//Updated to resolve the issue with locations not populating automatically when journey planned using find nearest station
//Resolution for 5529: Issue with plan a journey with find nearest
//
//   Rev 1.9   Mar 26 2010 11:50:28   MTurner
//Added code to clear journey results from session if you are coming directly from another planner.
//
//Resolution for 5481: Session issue when going from FAT to D2D using the left hand menu
//
//   Rev 1.8   Jan 29 2010 14:45:30   mmodi
//Updated to reset CycleResult to correct error when planning journey after coming directly from Cycle journey details
//Resolution for 5388: Cycle Planner - Server error when planning Door to door after a cycle journey
//
//   Rev 1.7   Jan 30 2009 10:44:18   apatel
//Search Engine Optimasation changes - CCN 624
//Resolution for 5229: Search Engin Optimisation Changes -  CCN624
//
//   Rev 1.6   Jun 23 2008 17:02:40   mmodi
//Locations are not editable when in Find station mode
//Resolution for 5027: Find a station - minor display issues
//
//   Rev 1.5   Jun 23 2008 11:53:42   mmodi
//Information text update when in Find a station mode
//Resolution for 5027: Find a station - minor display issues
//
//   Rev 1.4   May 08 2008 11:41:24   mmodi
//Changes made to multiple files to fix the find a car park feature of the city-to-city trunk mode. Testing performed to ensure that the trunk car park features has not broken the drive to car park mode.
//Resolution for 4954: Include 'Drive to Car Park' functionality in City to City
//
//   Rev 1.3   May 01 2008 17:23:30   mmodi
//Updated to display session timeout error
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.2   Mar 31 2008 13:24:42   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 05 2008 13:00:00   mmodi
//Added call to locations control which sets location dropdownss to be in Amendable mode
//
//DEVFACTORY FEB 21 2008 sbarker
//Page icon added
//
//   Rev 1.0   Nov 08 2007 13:29:42   mturner
//Initial revision.
//
//Rev DevFatory Feb 8th 16:28:00 dgath
//Line added to Page_Load event for White Label left menu configuration
//
//   Rev DevFactory Jan 20 2008 19:00:00 dgath
//CCN0382b City to City enhancements:
//Page transition set to new JourneyOverview page in case of FindAMode.Trunk (city to city request), otherwise transition event remains  with TransitionEventFindAInputOk.
//
//   Rev 1.43   Sep 03 2007 15:25:08   pscott
//CCN407 IR 4490
//title and key word changes for Google natural search
//
//   Rev 1.42   Apr 27 2006 11:20:52   mtillett
//Prevent calendar button dislpay on ambiguity page after next or back buttons clicked
//Resolution for 3510: Apps: Calendr Control problems on input/ambiguity screen
//
//   Rev 1.41   Apr 26 2006 12:15:04   RPhilpott
//Manual merge of stream 35
//
//   Rev 1.40   Mar 24 2006 17:29:48   kjosling
//Manually merged stream 0023 Journey Results 
//
//   Rev 1.39.1.0   Mar 10 2006 18:36:32   kjosling
//Support for ambiguity for the AmendToolbar
//Resolution for 23: DEL 8.1 Workstream - Journey Results - Phase 1
//
//   Rev 1.39   Feb 23 2006 19:41:26   AViitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.38   Feb 10 2006 18:17:30   kjosling
//Fixed merge
//
//   Rev 1.38   Feb 10 2006 17:24:48   kjosling
//Fixed
//
//   Rev 1.37   Feb 10 2006 10:47:54   jmcallister
//Manual Merge of Homepage 2. IR3180
//
//   Rev 1.36   Jan 17 2006 09:47:52   tolomolaiye
//Fix for IR 3388
//Resolution for 3388: Find a Fare Turned off:  City to City text mentions Cost after ambiguity page back button pressed
//
//   Rev 1.35   Jan 12 2006 18:16:06   RPhilpott
//Reset TDItineraryManager to default (mode "None") in page initialisation to allow for case where we are coming from VisitPlanner.
//Resolution for 3450: DEL 8: Server error when returning to Quickplanner results from Visit Planner input
//
//   Rev 1.34   Dec 09 2005 14:30:46   mtillett
//Ensure that the correct text is displayed on the city-to-city page, when find a fare not available
//Resolution for 3349: Home page switch to turn off find a fare
//
//   Rev 1.33   Nov 21 2005 10:46:14   mguney
//searchTypeControl.SearchTypeChanged event rewired up.
//Resolution for 3117: DN040: City-to-City Cost Radio button
//
//   Rev 1.32   Nov 15 2005 20:17:22   rgreenwood
//IR2990 Wired up Help Button
//Resolution for 2990: UEE Post Build Enhancement: Add Help Pages to Input Pages
//
//   Rev 1.31   Nov 10 2005 14:32:50   NMoorhouse
//TD093 UEE Input Pages - Soft Content
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.30   Nov 09 2005 16:49:52   RPhilpott
//Merge for stream2818.
//
//   Rev 1.29   Nov 03 2005 16:09:46   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.28.1.4   Oct 28 2005 18:42:16   AViitanen
//TD093 Page Input - Page formatting
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.28.1.3   Oct 25 2005 20:09:10   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.28.1.2   Oct 24 2005 21:20:42   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.28.1.1   Oct 12 2005 12:50:14   mtillett
//Updates to advanced options control to remove help and move hide button to single place in FindPageOptionsControl
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.28.1.0   Oct 10 2005 19:08:38   rgreenwood
//TD089 ES020 Image Button Replacement - Changed resource refs to fix build
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.28   Jun 08 2005 13:11:22   rgreenwood
//IR 2532: Removed cost-based search option from Stations/Airports FindTrunkInput screen.
//
//   Rev 1.27   Apr 20 2005 12:00:06   tmollart
//Modified InitialRequestSetup so that if user has cost based results planned in trunkcostbased mode they are redirected to the appropriate results page.
//Removed TimeOut bool as this was always set to false so conditions using it no longer required.
//Resolution for 2147: PT - Cost based city to city planner automatically jumps to previously planned find a fare results when selected.
//
//   Rev 1.26   Apr 15 2005 12:48:16   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.25   Mar 24 2005 14:20:10   rgeraghty
//Temporary switch implemented for costbasedsearching
//Resolution for 1972: Front End Switch for Find A Fare/City to City Cost Based Search
//
//   Rev 1.24   Mar 08 2005 16:28:42   bflenk
//TimeOut functionality implemented in TDPage.cs, removed from this file - IR1720
//
//   Rev 1.23   Feb 18 2005 14:41:26   tmollart
//Added search type control so user can select search mode. Code changes are to facilitate that change.
//
//   Rev 1.22   Nov 25 2004 14:37:56   jgeorge
//Moved line of code which retrieves PageState so that updates are reflected correctly
//Resolution for 1785: City to City should only plan direct flights
//
//   Rev 1.21   Nov 19 2004 11:37:38   asinclair
//Fix for IR1720
//
//   Rev 1.20   Nov 04 2004 11:28:46   passuied
//Additional check when checking if Extend is in progress to make sure we don't reset the Input params when in TrunkStation mode
//Resolution for 1731: Find a train  / flight / coach input page is not populted after selecting the locations in Find nearest station/airport page
//
//   Rev 1.19   Nov 03 2004 12:54:10   passuied
//Changes to enable a new FindAMode TrunkStation similar to Trunk but with differences...
//
//   Rev 1.18   Nov 02 2004 16:23:06   passuied
//No change.
//
//   Rev 1.17   Nov 02 2004 15:55:16   passuied
//Changes in Title, error messages and instruction for FindTrunkInput
//
//   Rev 1.16   Nov 02 2004 11:40:56   passuied
//fixed various display bugs
//
//   Rev 1.15   Nov 01 2004 18:05:06   passuied
//Changes for FindPlace new functionality
//
//   Rev 1.14   Oct 15 2004 12:39:10   jgeorge
//Changed to take account of new JourneyPlanStateData and changes to existing JourneyPlanControlData.
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.13   Oct 05 2004 14:19:58   passuied
//redirect to FindStationInput when clear page clicked
//
//   Rev 1.12   Sep 06 2004 15:29:54   passuied
//Added extra setting of SearchType in click handler of New Location buttons.
//Resolution for 1529: Find a Variety of Transport - redefaults to address/postcode should be City/town/suburb
//
//   Rev 1.11   Aug 27 2004 17:03:30   RPhilpott
//Correct interaction between control hierarchy and LocationSearchHelper.
//Resolution for 1329: Both train and coach stops can be selected on a Find A Train or Find A Coach page
//
//   Rev 1.10   Aug 27 2004 12:23:58   passuied
//set visibility of labelFromToTitle.
//
//   Rev 1.9   Aug 27 2004 10:43:26   passuied
//added back button at top of page
//
//   Rev 1.8   Aug 24 2004 11:32:36   passuied
//Call UpdateDateControl only on Postback
//
//   Rev 1.7   Aug 23 2004 13:11:42   passuied
//Added overloaded version of AmbiguitySearch which specifies if must accept postcodes or not.
//
//   Rev 1.6   Aug 19 2004 15:39:10   passuied
//Added new type in MapMode called FromFindAInput, used by FindTrunkInput and FindCarInput when calling the map. The map page and controls behave exactly as with enum MapMode.FromJourneyInput except that it shows the FindA header in the first case and the JourneyPlanner one in the latter. 
//Also checks if MapMode.FromFindAInput and if not wipe the FindAMode (CreateInstance(FindAMode.None)).
//Resolution for 1361: Maps no longer displays all gazetteer options
//
//   Rev 1.5   Aug 19 2004 13:26:48   COwczarek
//Reset Find A session data when page is loaded for the first 
//time and there is an itinerary or an extension is in progress.
//Resolution for 1306: Find a Train extends a journey incorrectly.
//
//   Rev 1.4   Aug 17 2004 13:37:04   esevern
//added initialisation of FindLeaveReturnDatesControl to InitialRequestSetup.  When display of Help forces redirect, date control values should be re-initialised from session data
//
//   Rev 1.3   Aug 17 2004 09:21:24   COwczarek
//Prior to commencing journey planning, reset the itinerary if one
//exists since it is not currently possible to extend using a Find A
//function. At this point save the current Find A mode and journey
//parameters to record what was used to plan the journey.
//
//   Rev 1.2   Aug 10 2004 11:58:20   esevern
//now setting back button visibility
//
//   Rev 1.1   Aug 05 2004 14:48:10   esevern
//added event handlers
//
//   Rev 1.0   Jul 28 2004 11:11:56   esevern
//Initial revision.

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.DataServices;


namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// 
	/// </summary>
	public partial class FindTrunkInput : TDPage
	{
		protected TransportDirect.UserPortal.Web.Controls.FindToFromLocationsControl locationsControl;
		protected TransportDirect.UserPortal.Web.Controls.FindLeaveReturnDatesControl dateControl;
		protected TransportDirect.UserPortal.Web.Controls.FindPageOptionsControl pageOptionsControl;
		protected TransportDirect.UserPortal.Web.Controls.SearchTypeControl searchTypeControl;

		private InputPageState inputPageState;
		
		/// <summary>
		/// Holds user's current page state for this page
		/// </summary>
		private FindTrunkPageState pageState;

		/// <summary>
		/// Hold user's current journey parameters for trunk journey search
		/// </summary>
		private TDJourneyParametersMulti journeyParams;

		/// <summary>
		/// Helper class responsible for common methods to Find A pages
		/// </summary>
		private FindTrunkInputAdapter findInputAdapter; 

		/// <summary>
		/// Constructor.
		/// </summary>
		public FindTrunkInput()
		{
			this.pageId = PageId.FindTrunkInput;
		}

		#region Private Methods

		/// <summary>
		/// Updates session data with control values for those controls which do not 
		/// raise events to signal that user has changed values (i.e. the date controls)
		/// </summary>
		private void UpdateOtherControls() 
		{
			pageState = (FindTrunkPageState)TDSessionManager.Current.FindPageState;
			journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;   
			inputPageState = TDSessionManager.Current.InputPageState;
			findInputAdapter = new FindTrunkInputAdapter(journeyParams, pageState, inputPageState);
			findInputAdapter.UpdateJourneyDates(dateControl);
		}

		/// <summary>
		/// Performs initialisation when page is loaded for the first time. This includes delegating to
		/// session manager by calling InitialiseJourneyParametersPageStates to create a new session data
		/// if necesssary (i.e. journey parameters and page state objects). If session data contains
		/// journey results then the user is redirected to the journey summary page.
		/// </summary>
		private void InitialRequestSetup() 
		{

			bool resetDone;

			// hide preferences button in page options
			pageOptionsControl.AllowShowAdvancedOptions = false;
			
			ITDSessionManager sessionManager = TDSessionManager.Current;
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

            #region Clear cache of journey data
            // if an extension is in progress, cancel it
            sessionManager.ItineraryManager.CancelExtension();

            ClearCacheHelper helper = new ClearCacheHelper();

            // Force clear of any printable information if added by the journey result page
            helper.ClearPrintableResultCache(TDSessionPartition.TimeBased);

            // Fix for IR5481 Session issue when going from between different planners using the left hand menu
            if (TDSessionManager.Current.FindAMode != FindAMode.Trunk
                || TDSessionManager.Current.ItineraryMode != ItineraryManagerMode.None)
            {
                if (TDSessionManager.Current.FindAMode != FindAMode.TrunkStation)
                {
                    // We have come directly from another planner so clear results from session.
                    helper.ClearJourneyResultCache();
                }
            }
            #endregion
            

			// Next, Initialise JourneyParameters and PageStates if needed. 
			// Will return true, if reset has been performed.
			resetDone = sessionManager.InitialiseJourneyParameters(FindAMode.Trunk);

			// if in already in trunk mode but in station mode
			// And QueryString "ClassicMode" present, means user
			// clicked on one of the links... So reset page
			if (sessionManager.FindAMode == FindAMode.TrunkStation
				&& Request.QueryString["ClassicMode"] != null)
			{
				resetDone = sessionManager.InitialiseJourneyParameters(FindAMode.Trunk);
			}

			pageState = (FindTrunkPageState)TDSessionManager.Current.FindPageState;
			inputPageState = TDSessionManager.Current.InputPageState;
			journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;            
			findInputAdapter = new FindTrunkInputAdapter(journeyParams, pageState, inputPageState);
            
			if (resetDone)            
			{
				// if page state and journey parameters were re-instantiated, initialise to correct state
				// for this find A mode.
				findInputAdapter.InitJourneyParameters();
			}

			// initialise the date control from session (required as displaying help forces redirect)
			findInputAdapter.InitDateControl(dateControl);
	
			pageOptionsControl.AllowBack = pageState.AmbiguityMode;
		}
        #endregion Private Methods

		#region Event Handlers

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
            bool isFromCarParks = IsFromCarParks();

            #region Set locations to be amendable
            // Set dropdowns to amendable, only when not in Find nearest station mode
            if ((!Page.IsPostBack) && (pageState.AmendMode)
                && (TDSessionManager.Current.FindAMode != FindAMode.TrunkStation))
            {
                //Note that if the to location is a car park, we don't want
                //to allow the to drop down list to be edited straight away.
                bool toDropDownLocked = false;

                if (journeyParams != null)
                {
                    if (journeyParams.DestinationLocation.CarParking != null)
                    {
                        toDropDownLocked = true;
                    }
                }

                if (toDropDownLocked)
                {
                    locationsControl.SetFromLocationAmendable();
                }
                else
                {
                    locationsControl.SetLocationsAmendable();
                }
            }
            #endregion

            if (!FindInputAdapter.FindAFareAvailable || TDSessionManager.Current.FindAMode == FindAMode.TrunkStation)
			{
				searchTypeControl.Visible = false;
			}
			else
			{
				searchTypeControl.Visible = true;
			}

			// don't want to update the date when not postback...
			// otherwise double initial population
			if (Page.IsPostBack)
			{
				findInputAdapter.UpdateDateControl(dateControl);
			}
			findInputAdapter.UpdateErrorMessages(panelErrorMessage, 
				labelErrorMessages, TDSessionManager.Current.ValidationError);

            labelFromToTitle.Text = GetResource("FindTrunkInput.labelFromToTitleBeforeCostBased");
			labelFromToTitle.Visible = !pageState.AmbiguityMode;

            if (isFromCarParks)
            {
                locationsControl.SetFromLocationAmendable();
            }

            // Overwrite the information text displayed below journey input panel for Find station mode
            if (TDSessionManager.Current.FindAMode == FindAMode.TrunkStation)
            {
                string informationText = GetResource("TDPageInformationHtmlPlaceHolderDefinition", "/Channels/TransportDirect/JourneyPlanning/FindTrunkStationInput");

                if (TDPageInformationHtmlPlaceHolderDefinition.HasControls())
                {
                    TDPageInformationHtmlPlaceHolderDefinition.Controls.Clear();
                }

                TDPageInformationHtmlPlaceHolderDefinition.Controls.Add(new LiteralControl(informationText));
            }
		}
        
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            // DN049: title and instruction depends if in trunk-station mode or not!
            string resourceTitle = (TDSessionManager.Current.FindAMode == FindAMode.TrunkStation) ? "FindTrunkInput.labelFindPageTitle.Station" : "FindTrunkInput.labelFindPageTitle";
            labelFindPageTitle.Text = GetResource(resourceTitle);
            imageFindTrunk.ImageUrl = GetResource("HomeDefault.imageCompareCityToCity.ImageUrl");
            imageFindTrunk.AlternateText = " ";

            PageTitle = GetResource("FindTrunkInput.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

            #region Display Session Errors
            // Check if there are any errors in the InputPageState and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, TDSessionManager.Current.InputPageState.InputSessionErrors);

            // Clear the error messages
            TDSessionManager.Current.InputPageState.InputSessionErrors = null;
            #endregion

            if (Page.IsPostBack)
			{
				UpdateOtherControls();
			}
			else
			{
				InitialRequestSetup();
			}

            bool isFromCarParks = IsFromCarParks();

            findInputAdapter.InitialiseControls(locationsControl);

			if (pageState.AmbiguityMode)
			{
				Helpbuttoncontrol1.HelpUrl =  GetResource("FindTrunkInput.HelpAmbiguityUrl");
			}
			else
			{
				Helpbuttoncontrol1.HelpUrl =  GetResource("FindTrunkInput.HelpPageUrl");
			}

            #region Car Parks Functionality
            if (isFromCarParks)
            {
                // Clear any car park related flags
                TDSessionManager.Current.FindCarParkPageState.IsFromCityToCity = false;
                TDSessionManager.Current.FindCarParkPageState.CurrentFindMode = FindCarParkPageState.FindCarParkMode.Default;
                TDSessionManager.Current.FindCarParkPageState.CarParkFindMode = FindCarParkPageState.FindCarParkMode.Default;

                SubmitRequest();
            }
            #endregion

            //Added for white labelling
            ConfigureLeftMenu("FindTrunkInput.clientLink.BookmarkTitle", "Home.clientLink.LinkText", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFindTrunkInput);
            expandableMenuControl.AddExpandedCategory("Related links");
		}

		/// <summary>
		/// Event handler called when new location button is clicked for the "from" location.
		/// The journey parameters are updated with the "from" location control's new values.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void locationsControl_NewLocationFrom(object sender, EventArgs e) 
		{
			journeyParams.OriginLocation = locationsControl.FromLocationControl.TheLocation;
			journeyParams.Origin = locationsControl.FromLocationControl.TheSearch;
			journeyParams.Origin.SearchType = SearchType.Locality;
			journeyParams.Origin.DisableGisQuery();
			journeyParams.OriginType = locationsControl.FromLocationControl.LocationControlType;
		}

		/// <summary>
		/// Event handler called when new location button is clicked for the "to" location.
		/// The journey parameters are updated with the "to" location control's new values.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void locationsControl_NewLocationTo(object sender, EventArgs e) 
		{
			journeyParams.DestinationLocation = locationsControl.ToLocationControl.TheLocation;
			journeyParams.Destination = locationsControl.ToLocationControl.TheSearch;
			journeyParams.Destination.SearchType = SearchType.Locality;
			journeyParams.Destination.DisableGisQuery();
			journeyParams.DestinationType = locationsControl.ToLocationControl.LocationControlType;
		}

		/// <summary>
		/// Event handler called when "find nearest station" button for the "to" location is clicked. 
		/// The user is redirected to the find station page.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void locationsControlToLocationControl_FindNearestClick(object sender, EventArgs e) 
		{
			TDSessionManager.Current.FindStationPageState.LocationType = FindStationPageState.CurrentLocationType.To;
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationInputDefault;
		}

		/// <summary>
		/// Event handler called when "find nearest station" button for the "from" location is clicked. 
		/// The user is redirected to the find station page.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void locationsControlFromLocationControl_FindNearestClick(object sender, EventArgs e) 
		{
			TDSessionManager.Current.FindStationPageState.LocationType = FindStationPageState.CurrentLocationType.From;
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationInputDefault;
		}

		private void dateControlLeaveDateControl_DateChanged(object sender, EventArgs e) 
		{
			journeyParams.OutwardDayOfMonth = dateControl.LeaveDateControl.DateControl.Day;
			journeyParams.OutwardMonthYear = dateControl.LeaveDateControl.DateControl.MonthYear;
		}

		private void dateControlReturnDateControl_DateChanged(object sender, EventArgs e) 
		{
			journeyParams.ReturnDayOfMonth = dateControl.ReturnDateControl.DateControl.Day;
			journeyParams.ReturnMonthYear = dateControl.ReturnDateControl.DateControl.MonthYear;
		}

		/// <summary>
		/// Map from click event hadler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MapFromClick(object sender, EventArgs e)
		{
			
			inputPageState.MapType = CurrentLocationType.From;
			inputPageState.MapMode = CurrentMapMode.FromFindAInput; 

			
			if (journeyParams.OriginLocation.Status == TDLocationStatus.Unspecified )
			{
				findInputAdapter.MapSearch(journeyParams.Origin.InputText,
					journeyParams.Origin.SearchType, journeyParams.Origin.FuzzySearch);

				if (inputPageState.MapLocationSearch.InputText.Length == 0)
					inputPageState.MapLocationControlType.Type = TDJourneyParameters.ControlType.Default;
				else
					inputPageState.MapLocationControlType.Type = TDJourneyParameters.ControlType.NoMatch;
			} 
			else 
			{
				inputPageState.MapLocationSearch = journeyParams.Origin;
				inputPageState.MapLocation = journeyParams.OriginLocation;
			} 

			inputPageState.JourneyInputReturnStack.Push(pageId);
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputToMap;

		}

		/// <summary>
		/// Map to click event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MapToClick(object sender, EventArgs e)
		{
			inputPageState.MapType = CurrentLocationType.To;
			inputPageState.MapMode = CurrentMapMode.FromFindAInput; 
			
			if (journeyParams.DestinationLocation.Status == TDLocationStatus.Unspecified )
			{
			
				findInputAdapter.MapSearch( journeyParams.Destination.InputText,
					journeyParams.Destination.SearchType, journeyParams.Destination.FuzzySearch );

				if (inputPageState.MapLocationSearch.InputText.Length == 0)
					inputPageState.MapLocationControlType.Type = TDJourneyParameters.ControlType.Default;
				else
					inputPageState.MapLocationControlType.Type = TDJourneyParameters.ControlType.NoMatch;
			} 
			else 
			{
				inputPageState.MapLocationSearch = journeyParams.Destination;
				inputPageState.MapLocation = journeyParams.DestinationLocation;
			} 

			inputPageState.JourneyInputReturnStack.Push(pageId);
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputToMap;
		}


		/// <summary>
		/// Performs page initialisation including event wiring.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Init(object sender, EventArgs e)
		{
			locationsControl.NewLocationFrom += new EventHandler(locationsControl_NewLocationFrom);
			locationsControl.NewLocationTo += new EventHandler(locationsControl_NewLocationTo);
			locationsControl.ToLocationControl.FindNearestClick += new EventHandler(locationsControlToLocationControl_FindNearestClick);
			locationsControl.FromLocationControl.FindNearestClick += new EventHandler(locationsControlFromLocationControl_FindNearestClick);
			dateControl.LeaveDateControl.DateChanged += new EventHandler(dateControlLeaveDateControl_DateChanged);
			dateControl.ReturnDateControl.DateChanged += new EventHandler(dateControlReturnDateControl_DateChanged);
			locationsControl.FromLocationControl.TriLocationControl.MapClick += new EventHandler(MapFromClick);
			locationsControl.ToLocationControl.TriLocationControl.MapClick += new EventHandler(MapToClick);
			pageOptionsControl.Submit += new EventHandler(pageOptions_Submit);
			pageOptionsControl.Back += new EventHandler(pageOptions_Back);
			pageOptionsControl.Clear += new EventHandler(pageOptions_Clear);

		}
        #endregion Event Handlers

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			searchTypeControl.SearchTypeChanged +=new EventHandler(searchTypeControl_SearchTypeChanged);
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}

		#endregion

		/// <summary>
		/// Event handler called when back button clicked in ambiguous mode. Decrements the level of
		/// hierarchical location searches (origin, destination and public via). If all locations are at highest 
		/// level then page is reverted to input mode and original input parameters reinstated.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void pageOptions_Back(object sender, EventArgs e)
		{
			dateControl.CalendarClose();

			TDSessionManager.Current.ValidationError = null;
			if (findInputAdapter.IsAtHighestLevel()) 
			{
				//Check to see if there is a previous page waiting on the stack
				if(inputPageState.JourneyInputReturnStack.Count != 0)
				{
					TransitionEvent lastPage = (TransitionEvent)inputPageState.JourneyInputReturnStack.Pop();
					//If the user is returing to the previous journey results, re-validate them
					if(lastPage == TransitionEvent.FindAInputRedirectToResults)
					{
						TDSessionManager.Current.JourneyResult.IsValid = true;
					}
					TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = lastPage;
					return;
				}

				pageState.AmbiguityMode = false;
				pageState.ReinstateJourneyParameters(journeyParams);
				dateControl.LeaveDateControl.AmbiguityMode = false;
				dateControl.ReturnDateControl.AmbiguityMode = false;
				dateControl.LeaveDateControl.DateErrors = null;
				dateControl.ReturnDateControl.DateErrors = null;

				findInputAdapter.InitLocationsControl(locationsControl);

				if (locationsControl.FromLocationControl.TheLocation.Status != TDLocationStatus.Valid) 
				{
					locationsControl.FromLocationControl.SetLocationUnspecified();
				}
				if (locationsControl.ToLocationControl.TheLocation.Status != TDLocationStatus.Valid) 
				{
					locationsControl.ToLocationControl.SetLocationUnspecified();
				}
			} 
			else
			{
				// Decrement the drilldown level of any ambiguous locations
				// that are not yet at their highest level
				journeyParams.Origin.DecrementLevel();
				journeyParams.Destination.DecrementLevel();
			}
		}

		/// <summary>
		/// Event handler called when clear page button is clicked. Journey parameters are reset
		/// to initial values, page controls updated and the page set to input mode.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void pageOptions_Clear(object sender, EventArgs e)
		{
			findInputAdapter.InitJourneyParameters();
			pageState.AmbiguityMode = false;
			findInputAdapter.InitialiseControls(locationsControl);
			dateControl.LeaveDateControl.DateErrors = null;
			dateControl.LeaveDateControl.AmbiguityMode = false;
			dateControl.ReturnDateControl.DateErrors = null;
			dateControl.ReturnDateControl.AmbiguityMode = false;
			TDSessionManager.Current.ValidationError = null;
			TDPage.CloseAllSingleWindows(Page);

			// When page cleared, redirect to FindStationInput. Depending on if in Trunk-station mode or not, 
			// want to go either to stationInput page or trunkinput page
			if ( TDSessionManager.Current.FindAMode == FindAMode.TrunkStation)
			{
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationInputDefault;
				TDSessionManager.Current.SetOneUseKey(SessionKey.NotFindAMode, "true");
			}
				// else stay here and Force !Postback (so list gets populated again)
			else
			{
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindTrunkInputDefault;
				TDSessionManager.Current.FormShift[SessionKey.ForceRedirect] = true;
			}
		}
		
		/// <summary>
		/// Event handler called when next button clicked. The journey plan runner component validates the 
		/// current journey parameters. If invalid then the page is put into ambiguity mode otherwise 
		/// journey planning commences. User preferences are saved before commencing journey planning.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void pageOptions_Submit(object sender, EventArgs e)
		{
			dateControl.CalendarClose();

 			if (!pageState.AmbiguityMode) 
			{
				pageState.SaveJourneyParameters(journeyParams);
				findInputAdapter.AmbiguitySearch(locationsControl);
			} 
			else 
			{
				locationsControl.FromLocationControl.Search();
				locationsControl.ToLocationControl.Search();
			}

			findInputAdapter.InitialiseAsyncCallState();

			// Validate the JourneyParameters
			JourneyPlanRunner.JourneyPlanRunner runner = new JourneyPlanRunner.JourneyPlanRunner(
				Global.tdResourceManager);

			pageState.AmbiguityMode = !runner.ValidateAndRun(
				TDSessionManager.Current, 
				TDSessionManager.Current.JourneyParameters, 
				GetChannelLanguage(TDPage.SessionChannelName),
				true);

			pageOptionsControl.AllowBack = pageState.AmbiguityMode;

            if (pageState.AmbiguityMode)
            {
                dateControl.LeaveDateControl.DateErrors = TDSessionManager.Current.ValidationError;
                dateControl.ReturnDateControl.DateErrors = TDSessionManager.Current.ValidationError;
                dateControl.LeaveDateControl.AmbiguityMode = true;
                dateControl.ReturnDateControl.AmbiguityMode = true;
            }

            else
            {

                // Extending journeys using a Find A is not currently possible so clear down the itinerary
                TDItineraryManager itineraryManager = TDItineraryManager.Current;
                if (itineraryManager.Length >= 1)
                {
                    itineraryManager.ResetItinerary();
                }

                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindAInputOk;

            }
		}


		/// <summary>
		/// Event handler for when a new selection is made on the search type control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void searchTypeControl_SearchTypeChanged(object sender, System.EventArgs e)
		{
			//If the user has selected a cost based search
			if (searchTypeControl.CostSearch)
			{
				//Set up transition event to transfer the user to FindFareInput. Insert a one
				//use key so that a URL query string is appended onto the URL to force 
				//FindFareInput to adopt the correct mode.

				ITDSessionManager sessionManager = TDSessionManager.Current;

				sessionManager.Partition = TDSessionPartition.CostBased;

				FindCostBasedPageState fcbps = TDSessionManager.Current.FindPageState as FindCostBasedPageState;

				if (fcbps != null)
				{

					// Reset the itinerary manager
					sessionManager.ItineraryManager.NewSearch();

					// Flag new search button being clicked so that redirect page can perform any necessary initialisation
					sessionManager.SetOneUseKey(SessionKey.NewSearch,string.Empty);

					// invalidate the current journey result. Set the mode for which the results pertain to as being
					// none so that clicking the Find A tab will then redirect to the default find A input page.
					if (sessionManager.JourneyResult != null)
					{
						sessionManager.JourneyResult.IsValid = false;
					}

					//Reset state data and search result.
					fcbps.SearchResult = null;
				}

				sessionManager.SetOneUseKey(SessionKey.FindModeTrunkCostBased, string.Empty);
				sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindFareInputDefault;
			}
		}

        /// <summary>
        /// Checks if arrived to this planner from Find a car park
        /// </summary>
        /// <returns></returns>
        private bool IsFromCarParks()
        {
            // We can check if arrvied from car parks because car park results will have set the return page
            bool isFromCarParks = false;

            Stack returnStack = TDSessionManager.Current.InputPageState.JourneyInputReturnStack;

            if (returnStack.Count != 0)
            {
                PageId returnPageId = (PageId)returnStack.Pop();

                if (returnPageId == PageId.FindCarParkResults)
                {
                    isFromCarParks = true;
                }
            }

            return isFromCarParks;
        }

        private void SubmitRequest()
        {
            dateControl.CalendarClose();

            pageState.SaveJourneyParameters(journeyParams);
            
            findInputAdapter.InitialiseAsyncCallState();

            // Validate the JourneyParameters
            JourneyPlanRunner.JourneyPlanRunner runner = new JourneyPlanRunner.JourneyPlanRunner(Global.tdResourceManager);

            pageState.AmbiguityMode = !runner.ValidateAndRun(
                TDSessionManager.Current,
                TDSessionManager.Current.JourneyParameters,
                GetChannelLanguage(TDPage.SessionChannelName),
                true);

            pageOptionsControl.AllowBack = pageState.AmbiguityMode;

            if (pageState.AmbiguityMode)
            {
                dateControl.LeaveDateControl.DateErrors = TDSessionManager.Current.ValidationError;
                dateControl.ReturnDateControl.DateErrors = TDSessionManager.Current.ValidationError;
                dateControl.LeaveDateControl.AmbiguityMode = true;
                dateControl.ReturnDateControl.AmbiguityMode = true;
            }
            else
            {
                // Extending journeys using a Find A is not currently possible so clear down the itinerary
                TDItineraryManager itineraryManager = TDItineraryManager.Current;
                if (itineraryManager.Length >= 1)
                {
                    itineraryManager.ResetItinerary();
                }

                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindAInputOk;
            }
        }	
	}
}