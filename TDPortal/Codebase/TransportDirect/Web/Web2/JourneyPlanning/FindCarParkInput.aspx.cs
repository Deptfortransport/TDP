// *********************************************** 
// NAME                 : FindCarParkInput.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 28/07/2006 
// DESCRIPTION			: Input page to find nearest
//						  car parks for a specified
//						  location.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindCarParkInput.aspx.cs-arc  $ 
//
//   Rev 1.10   Mar 22 2013 10:49:08   dlane
//CCN0648c - Super Batch enhancements
//Resolution for 5907: CCN0648c - "Super Batch" enhancements
//
//   Rev 1.9   Aug 01 2011 14:52:44   apatel
//Code update to stop FindTrainCostInput page and FindCarParkInput page from breaking in specific navigation flow
//Resolution for 5717: Find Cheaper Rail Fare page breaks in certain navigation flow
//
//   Rev 1.8   Jul 28 2011 16:19:46   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.7   Oct 28 2010 15:26:28   RBroddle
//Removed explicit wire up to Page_Load & Page_Unload as AutoEventWireUp=true for this control so they were firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.6   Jan 29 2010 14:45:26   mmodi
//Updated to reset CycleResult to correct error when planning journey after coming directly from Cycle journey details
//Resolution for 5388: Cycle Planner - Server error when planning Door to door after a cycle journey
//
//   Rev 1.5   Jan 30 2009 10:44:12   apatel
//Search Engine Optimasation changes - CCN 624
//Resolution for 5229: Search Engin Optimisation Changes -  CCN624
//
//   Rev 1.4   May 08 2008 11:41:10   mmodi
//Changes made to multiple files to fix the find a car park feature of the city-to-city trunk mode. Testing performed to ensure that the trunk car park features has not broken the drive to car park mode.
//Resolution for 4954: Include 'Drive to Car Park' functionality in City to City
//
//   Rev 1.3   May 01 2008 17:24:08   mmodi
//Updated to display session timeout error
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.2   Mar 31 2008 13:24:22   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Feb 22 2008 13:59:00 apatel
//  related links label removed
//
//   Rev DevFactory   Feb 15 2008 13:00:00   mmodi
//Updated set mode method to also check for Find Nearest flag
//
//DEVFACTORY FEB 21 2008 sbarker
//Page icon added
//
//DEVFACTORY   JAN 08 2008 12:30:00   psheldrake
//Changes to conform to CCN426 : Car Park Usability Updates
//
//   Rev 1.3   Nov 29 2007 15:24:00   mturner
//Added an initial assignment to a variable declaration to remove compiler warning.
//
//   Rev 1.2   Nov 29 2007 14:59:50   mturner
//Minor changes to remove the .Net2 compiler warnings created by merging in the Del 9.8 changes.
//
//   Rev 1.2   Nov 29 2007 13:07:52   mturner
//Declared as partial class to make Del 9.8 cde .Net2 compliant
//
//   Rev 1.1   Nov 29 2007 11:46:48   build
//Updated for Del 9.8
//
//   Rev 1.23   Nov 08 2007 15:08:24   mmodi
//Reset landing page parameters when leaving page
//Resolution for 4524: Del 9.8 - Car Parking Page Landing
//
//   Rev 1.22   Nov 08 2007 14:27:32   mmodi
//Updates to handle FindNearest Landing Page request
//Resolution for 4524: Del 9.8 - Car Parking Page Landing
//
//   Rev 1.21   Sep 03 2007 15:24:48   pscott
//CCN407 IR 4490
//title and key word changes for Google natural search
//
//   Rev 1.20   Oct 27 2006 15:17:08   mmodi
//Correct Back button display when in ambiguity mode
//Resolution for 4237: Car Parking: Back button not shown on ambiguity mode
//
//   Rev 1.19   Oct 19 2006 10:50:38   mmodi
//Code to ensure location is retained when returning from Results page with Back selected
//Resolution for 4231: Car Parking: Back button navigation issue on results page
//
//   Rev 1.18   Oct 03 2006 10:30:44   mmodi
//Set the IsFromNearestCarPark property for when New Search is clicked
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4208: Car Parking: New Search a car park journey goes to Car route input
//
//   Rev 1.17   Sep 28 2006 16:36:10   mmodi
//Added code to reset SelectedCarParkIndex
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4206: Car Parking: Selected car park radio button defaults to first option
//
//   Rev 1.16   Sep 27 2006 12:58:28   mmodi
//Changed skip link text reference
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4185: Car Parking: Screen reader
//
//   Rev 1.15   Sep 22 2006 14:40:50   esevern
//Changes after code review  -  removed commented out code
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.14   Sep 19 2006 15:39:08   tmollart
//Modifications for skip links.
//Resolution for 4185: Car Parking: Screen reader
//
//   Rev 1.13   Sep 13 2006 16:21:28   mmodi
//Set car parks found value and corrected initialise page
//Resolution for 4172: Car Parking: No car parks found using homepage find miniplanner
//Resolution for 4178: Car Parking: Find Car park input page not cleared
//
//   Rev 1.12   Sep 08 2006 14:47:28   esevern
//Removed call to CarParkCatalogue.LoadData. Now done only when car park selected.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.11   Sep 04 2006 11:46:08   esevern
//Removed commented out code - 'onNewLocation' event handler 
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.10   Aug 31 2006 14:41:32   MModi
//Added Try Catch around car park search
//
//   Rev 1.9   Aug 30 2006 16:02:12   mmodi
//Moved CarParkCatalogue populater to before search
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.8   Aug 24 2006 12:00:46   esevern
//Removed TDLocation parameter from LoadData for car parks
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.7   Aug 23 2006 15:22:08   mmodi
//Changed skip link location
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.6   Aug 23 2006 15:06:10   mmodi
//Skip link added 
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.5   Aug 23 2006 14:45:30   mmodi
//Correct transition when Back clicked on input page arriving from Find car route
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.4   Aug 15 2006 16:45:52   esevern
//Added load of car park data when location validated and car parks found for the selected location.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.3   Aug 15 2006 11:36:30   esevern
//removed commented out code
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.2   Aug 14 2006 11:16:20   esevern
//Interim check-in for developer build
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.1   Aug 03 2006 17:19:46   mmodi
//Added code to populate labels
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.0   Jul 28 2006 14:41:02   esevern
//Initial revision.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2

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
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.CommonWeb.Helpers;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for FindCarParkInput.
	/// </summary>
	public partial class FindCarParkInput : TDPage
	{
		protected TransportDirect.UserPortal.Web.Controls.FindToFromLocationsControl toFromLocationControl;
        protected TransportDirect.UserPortal.Web.Controls.PlanAJourney planAJourneyControl;
        protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl expandableMenuControl;
        protected TransportDirect.UserPortal.Web.Controls.ClientLinkControl clientLink;
        protected TransportDirect.UserPortal.Web.Controls.RoundedCornerControl mainbit;

		protected HeaderControl headerControl;

		#region Resource keys declaration
	
		//Car park mode resource keys

        private const string RES_TITLE_DRIVETOCARPARKMODE   = "FindCarParkInput.labelDriveToCarParkTitle";
        private const string RES_TITLE_CARPARKMODE			= "FindCarParkInput.labelFindCarParkTitle";
		private const string RES_NOTE_CARPARKMODE			= "FindCarParkInput.labelFindCarParkNote";
		private const string RES_NOTE_CARPARKMODE_AMBIGUOUS	= "FindCarParkInput.labelNote.Ambiguous";
		
		#endregion

		#region Private variables declaration
		private DataServices.DataServices populator;
		private FindCarParkPageState carParkPageState;
		private SessionManager.FindPageState pageState;
		private SessionManager.TDJourneyParameters journeyParameters;
		private LocationSearch currentSearch = null;
		private TDLocation currentLocation = null;
		private bool timeout = false;

		/// <summary>
		/// Helper class for Landing Page functionality
		/// </summary>
		private LandingPageHelper landingPageHelper = new LandingPageHelper();

		#endregion

		#region Constructor, Page load, and Prerender

		/// <summary>
		/// Default constructor
		/// </summary>
		public FindCarParkInput()
		{
			pageId = PageId.FindCarParkInput;
			populator = (DataServices.DataServices) TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			LoadSessionVariables();
			TDSessionManager.Current.Partition = TDSessionPartition.TimeBased;

            #region Display Session Errors
            // Check if there are any errors in the InputPageState and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, TDSessionManager.Current.InputPageState.InputSessionErrors);

            // Clear errors in the prerender
            #endregion

			#region Set IsFromNearestCarParks value
			// If the user has started their journey planning from Find Car Park Input
			// then need to be able to return here on a New Search from Car route or Refine journey results
			if (!Page.IsPostBack)
			{
				if (((TDSessionManager.Current.FindPageState == null) 
					|| (TDSessionManager.Current.FindPageState.Mode == FindAMode.CarPark))
                    && (!carParkPageState.IsFromDoorToDoor) && (!carParkPageState.IsFromCityToCity))
				{
					TDSessionManager.Current.IsFromNearestCarParks = true;
				}
                else if ((TDSessionManager.Current.IsFromNearestCarParks) &&
                    (TDSessionManager.Current.FindPageState.Mode == FindAMode.Car)
                    && (carParkPageState.CurrentFindMode != FindCarParkPageState.FindCarParkMode.DriveTo)
                    && (carParkPageState.CarParkFindMode != FindCarParkPageState.FindCarParkMode.DriveTo))
                {
                    // ensures we return to Find Car Park Input if the New Search is selected again and again
                    // i.e. Find Car Park -> Plan car route -> New Search -> Find Car Park -> Plan car route -> New Search...
					TDSessionManager.Current.IsFromNearestCarParks = true;
					// Need to set our mode to CarPark so the Find car route input is then initialised correctly
					TDSessionManager.Current.InitialiseJourneyParameters(FindAMode.CarPark);
				}
				else
				{
                    // If in DriveTo mode then user has arrived from a different planner, therefore can never
                    // have started path from Car park input page.

					TDSessionManager.Current.IsFromNearestCarParks = false;
				}
			}
			#endregion

            SetFindCarParkMode();

			// Check if has came from FindAPlaceControl
			if( TDSessionManager.Current.GetOneUseKey(SessionKey.FindALocationFromHomePage) != null) 
			{
				pageState = FindPageState.CreateInstance(FindAMode.CarPark); 
				TDSessionManager.Current.FindPageState = pageState;

				TDSessionManager.Current.JourneyParameters = new TDJourneyParametersMulti();				
				TDSessionManager.Current.InitialiseJourneyParameters(FindAMode.CarPark);
				TDSessionManager.Current.JourneyParameters.Initialise();  
				toFromLocationControl.FromLocationControl.AmendMode = carParkPageState.AmendMode;
				commandBack.Visible = true;
			}
			else
			{
				if (Page.IsPostBack)
				{	
					// if an extension is in progress, cancel it
					TDSessionManager.Current.ItineraryManager.CancelExtension();
				}
				else
				{
                    // Only initialise the journey parameters if we havent come from Door to Door - Car Journey Details
                    // because we will lose the original search locations
                    if ((pageState == null) && (!carParkPageState.IsFromDoorToDoor) && (!carParkPageState.IsFromCityToCity))
					{
						TDSessionManager.Current.InitialiseJourneyParameters(FindAMode.CarPark);
					}
				}
				if(pageState == null || Request.QueryString["NotFindAMode"] != null)
				{
					pageState = FindPageState.CreateInstance(FindAMode.CarPark); 
					TDSessionManager.Current.FindPageState = pageState;										 
				}

                // If session partition was cost based and page state is cost based probably 
                // curreny findpagestate in session is null in that case initialise it with page state for car park
                if (pageState is FindCostBasedPageState && TDSessionManager.Current.FindPageState == null)
                {
                    TDSessionManager.Current.FindPageState = FindPageState.CreateInstance(FindAMode.CarPark); 
                }
			
				// We can  come to this page (for not postback) at 2 different moments
				// 1. for a new search --> whole stationPageState must be initialised
				// 2. when one of the locations is valid and the other is not --> just initialise the CurrentLocation and Table state
				if (!Page.IsPostBack)
				{
					// if comes from the FindXXXinput pages, initialise everything except
					// the locationType which is set on the page...
					if (pageState.Mode != FindAMode.CarPark)
					{
						// If we've selected Back on results page when intially come from Car route input
						// this prevents the location entered being lost
						if(( !carParkPageState.AmendMode) && (TDSessionManager.Current.FindCarParkPageState.CurrentFindMode != FindCarParkPageState.FindCarParkMode.DriveTo))
						{
							carParkPageState.InitialiseLocation(FindCarParkPageState.CurrentLocationType.From);
							carParkPageState.InitialiseLocation(FindCarParkPageState.CurrentLocationType.To);
							carParkPageState.InitialiseCurrentLocation();
							carParkPageState.InitialiseTableState();
						}
					}
					else
					{
						// We will only ever show/search for the From location, therefore no need to 
						// worry about the To location as on the Find nearest station planner.

						// don't initialise if amendKey has been set, or if we have come from Landing Page. we want the values kept in the boxes.
						if ((!carParkPageState.AmendMode) && (!TDSessionManager.Current.Session[ SessionKey.LandingPageCheck ]))
						{
                            // Only initialise the journey parameters if we havent come from Door to Door - Car Journey Details
                            // because we will lose the original search locations
                            if (!carParkPageState.IsFromDoorToDoor)
                            {
                                TDSessionManager.Current.JourneyParameters.Initialise();
                                carParkPageState.Initialise();
                            }
						}
					}
				}
			}

            if (!Page.IsPostBack)
            {
                //Clear the CycleResult when redirected from CyclePlanner
                if (TDSessionManager.Current.CycleResult != null)
                {
                    TDSessionManager.Current.CycleRequest = null;
                    TDSessionManager.Current.CycleResult = null;
                }
            }

			PopulateControls();

            PageTitle = GetResource("FindCarParkInput.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");
            // Skip link
            imageMainContentSkipLink1.ImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
            imageMainContentSkipLink1.AlternateText = GetResource("FindCarParkInput.SkipLink_CarPark.AlternateText");

            //Page icon:
            imageFindCarPark.ImageUrl = GetResource("HomeDefault.imageFindCarPark.ImageUrl");
            imageFindCarPark.AlternateText = " ";

            if (!Page.IsPostBack)
			{
				LocationSearchHelper.DisableGisQuery(carParkPageState.CurrentSearch);
			}

			#region Handle arrive from Homepage FindAControl and Landing Page, and from 'FindNearestCarParks link on results
			// If it has came from FindAControl then perform the search
			// or If it is a Landing page Auto plan
			if ((TDSessionManager.Current.GetOneUseKey(SessionKey.FindALocationFromHomePage) != null)
				||
				(TDSessionManager.Current.Session[ SessionKey.LandingPageAutoPlan ])
                ||
                (TDSessionManager.Current.FindCarParkPageState.CurrentFindMode == FindCarParkPageState.FindCarParkMode.DriveTo))
			{ 
				toFromLocationControl.RefreshControl(); 
				toFromLocationControl.Search();			
				if ( currentLocation.Status == TDLocationStatus.Valid)
				{
					TDSessionManager.Current.Session[SessionKey.Transferred] = false;
					TDSessionManager.Current.Session[SessionKey.LandingPageAutoPlan] = false;

					SubmitRequest();
				}
			}
			#endregion

			UserExperienceEnhancementHelper.AddClientForUserNavigationDefaultAction(this.Page);  
		
			SetHelpUrl();

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFindCarParkInput);
            expandableMenuControl.AddExpandedCategory("Related links");
		}

		/// <summary>
		/// EventHandler For event just before page renders
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			LoadResources();
			SetControlsVisibility();
            if (carParkPageState.LocationControlType.Type != TDJourneyParameters.ControlType.Default)
                commandBack.Visible = true;
            //save current page language
            FindCarParkHelper.SavePageLanguageToSession();

            ResetCarParkFindMode();

            // Clear the error messages in session
            TDSessionManager.Current.InputPageState.InputSessionErrors = null;
           
			base.OnPreRender(e);
		}

		/// <summary>
		/// Page Unload event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void Page_Unload(object sender, System.EventArgs e)
		{
			//reset landing page session parameters
			if (TDSessionManager.Current.Session[ SessionKey.LandingPageCheck ])
			{
				landingPageHelper.ResetLandingPageSessionParameters();
			}
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Sets the visibility of the top location control panel - only
		/// one location control should be visible
		/// </summary>
		private void SetControlsVisibility()
		{
			// invisible if we want to enable to drill up in location hierarchy
			panelBackTop.Visible = !(pageState.Mode == FindAMode.CarPark)
				|| carParkPageState.LocationControlType.Type != TDJourneyParameters.ControlType.Default;

			// if users path originally started from Find nearest car parks, then we need to stop
			// the Back button being displayed when New Search is selected from the Car route results or 
			// Refine journey results page
			if ((TDSessionManager.Current.IsFromNearestCarParks) && 
				(TDSessionManager.Current.FindPageState.Mode == FindAMode.CarPark))
			{
				// ensure Back button is displayed when on Ambiguity page
				if (carParkPageState.LocationControlType.Type != TDJourneyParameters.ControlType.Default)
					panelBackTop.Visible = true;
				else
					panelBackTop.Visible = false;
			}
		}

		/// <summary>
		/// Sets the text and tool tip for Next and Back buttons
		/// </summary>
		private void LoadResources()
		{
			commandBack.Text = GetResource("FindCarParkInput.commandBack.Text");
			commandBack.ToolTip = GetResource("FindCarParkInput.commandBack.ToolTip");
			commandNext.Text = GetResource("FindCarParkInput.commandNext.Text");
			commandNext.ToolTip = GetResource("FindCarParkInput.commandNext.ToolTip");

			SetText();
		}

		/// <summary>
		/// Populate controls properties
		/// </summary>
		private void PopulateControls()
		{
            
            
            //Populates the left hand nav content
            #region Determine context based on car parks mode
            TransportDirect.UserPortal.SuggestionLinkService.Context context;

            if (carParkPageState.CurrentFindMode == FindCarParkPageState.FindCarParkMode.Default)
                context = TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuFindAPlace;
            else
                context = TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney;
            #endregion

            expandableMenuControl.AddContext(context);

            // Set up client link for bookmark on expandable menu
            clientLink.BookmarkTitle = GetResource("FindCarParkInput.clientLink.BookmarkTitle");
            clientLink.LinkText = GetResource("Home.clientLink.LinkText");

            IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
            string baseChannel = string.Empty;
            string url = string.Empty;
            if (TDPage.SessionChannelName != null)
                baseChannel = getBookmarkBaseChannelURL(TDPage.SessionChannelName);

            // Determine url to save as bookmark
            string leftPartOfUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindCarParkInput);
            url = leftPartOfUrl + baseChannel + pageTransferDetails.PageUrl;
            clientLink.BookmarkUrl = url;
          
            // Fills the location essential properties
			if (carParkPageState.LocationFrom.Status == TDLocationStatus.Valid)
			{
				toFromLocationControl.OriginLocation = carParkPageState.LocationFrom;
				toFromLocationControl.OriginSearch = carParkPageState.SearchFrom;
			}
			else
			{	// Current Location will be assigned to Origin Control
				toFromLocationControl.OriginLocation = carParkPageState.CurrentLocation;
				toFromLocationControl.OriginSearch = carParkPageState.CurrentSearch;
				toFromLocationControl.OriginControlType = carParkPageState.LocationControlType;
			}

			if (carParkPageState.LocationTo.Status == TDLocationStatus.Valid)
			{
				toFromLocationControl.DestinationLocation = carParkPageState.LocationTo;
				toFromLocationControl.DestinationSearch = carParkPageState.SearchTo;
			}
			else
			{
				// Current Location will be assigned to DestinationControl
				toFromLocationControl.DestinationLocation = carParkPageState.CurrentLocation;
				toFromLocationControl.DestinationSearch = carParkPageState.CurrentSearch;
				toFromLocationControl.DestinationControlType = carParkPageState.LocationControlType;
			}
			// Refresh the amend mode from session
			toFromLocationControl.FromLocationControl.AmendMode = carParkPageState.AmendMode;
		}

		/// <summary>
		/// Sets page title and note text
		/// </summary>
		private void SetText()
		{
			labelFindCarParkTitle.Text = GetResource(RES_TITLE_CARPARKMODE);
			labelNote.Text = GetResource(RES_NOTE_CARPARKMODE);

            // Check page mode to determing which title to use
            if (carParkPageState.CurrentFindMode == FindCarParkPageState.FindCarParkMode.DriveFromAndTo)
                labelFindCarParkTitle.Text = GetResource("FindCarParkInput.labelDriveToCarParkTitle");
            else
                labelFindCarParkTitle.Text = GetResource("FindCarParkInput.labelFindCarParkTitle");
			
            if (toFromLocationControl.OriginLocation.Status == TDLocationStatus.Ambiguous)
			{
				labelNote.Text = GetResource(RES_NOTE_CARPARKMODE_AMBIGUOUS);
			}
		}

		/// <summary>
		/// Get direction (from/to)string in current language
		/// </summary>
		/// <returns>string direction</returns>
		private string GetDirectionString()
		{
			string sDirection = string.Empty;
			if (carParkPageState.LocationType == FindCarParkPageState.CurrentLocationType.From)
			{
				sDirection = GetResource(FindCarParkHelper.RES_FROM);
			}
			else
			{
				sDirection = GetResource(FindCarParkHelper.RES_TO);
			}

			return sDirection;
		}

		//setting the help url depending on the mode of the page
		private void SetHelpUrl()
		{
			if(carParkPageState.LocationControlType.Type != TDJourneyParameters.ControlType.Default)
			{
				helpButtonControl.HelpUrl = GetResource("FindCarParkInput.HelpAmbiguityUrl");
			}
			else
			{
				helpButtonControl.HelpUrl = GetResource("FindCarParkInput.HelpPageUrl");
			}
		}

		/// <summary>
		/// Load Session variables and fills Control properties
		/// </summary>
		private void LoadSessionVariables()
		{
			journeyParameters = TDSessionManager.Current.JourneyParameters;
			carParkPageState = TDSessionManager.Current.FindCarParkPageState;
			pageState = TDSessionManager.Current.FindPageState;
			currentSearch = carParkPageState.CurrentSearch;
			currentLocation = carParkPageState.CurrentLocation;
		}

		private void SubmitRequest()
		{
			// place in try catch as an exception is thrown if no car parks found
			try
			{
				// Default value of this property is -1, it should only be set via the Landing page
				if (carParkPageState.MaxNumberOfCarParks >= 0)
					LocationSearchHelper.FindCarParks(ref currentLocation, carParkPageState.MaxNumberOfCarParks);
				else
					LocationSearchHelper.FindCarParks(ref currentLocation);

				carParkPageState.ResultsTable = FindCarParkHelper.BuildResultsDataTable(currentLocation);
					
				// If no car park records added to table, set Found to false,
				// to prevent empty table being displayed
				if (carParkPageState.ResultsTable.Rows.Count == 0)
				{
					carParkPageState.CarParkFound = false;
				}
				else
				{
					carParkPageState.CarParkFound = true;

					// Ensures always the first item displayed in the Results table is selected by default
					TDSessionManager.Current.JourneyViewState.SelectedCarParkIndex = 0;
				}
			}
			catch
			{
				// Exceptions thrown so no car parks were found
				carParkPageState.CarParkFound = false;
			}
			finally
			{
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarParkInputUnambiguous;

				// Disable AmendMode when leaving the page
				carParkPageState.DisableAmendMode();
			}
		}

        /// <summary>
        /// Checks the URL query string and Session to determine which mode this page should be in
        /// Updates the carParkPageState with mode
        /// </summary>
        private void SetFindCarParkMode()
        {
            bool isDriveFromTo = false;
            bool isDriveTo = false;
            bool isFindNearest = false;

            // The mode this page is loaded in can be set by a QueryString value in the URL
            // or by the CarParkFindMode in the session
            if (Request.QueryString["DriveFromTo"] != null)
            {
                isDriveFromTo = Convert.ToBoolean(Request.QueryString["DriveFromTo"]);
            }
            else if (Request.QueryString["DriveTo"] != null)
            {
                isDriveTo = Convert.ToBoolean(Request.QueryString["DriveTo"]);
            }
            else if (Request.QueryString["FindNearest"] != null)
            {
                isFindNearest = Convert.ToBoolean(Request.QueryString["FindNearest"]);
            }

            // Set the FindCarParkMode
            if (FindCarParkHelper.HasLanguageChanged())
            {
                // Language has changed, therefore retain previous mode
                carParkPageState.CurrentFindMode = carParkPageState.CarParkFindModePrevious;
            }
            else if ((isDriveFromTo) || (carParkPageState.CarParkFindMode == FindCarParkPageState.FindCarParkMode.DriveFromAndTo))
            {
                // Go in to DriveFromAndTo mode
                carParkPageState.CurrentFindMode = FindCarParkPageState.FindCarParkMode.DriveFromAndTo;
                carParkPageState.CarParkFindModePrevious = FindCarParkPageState.FindCarParkMode.DriveFromAndTo;
            }
            else if ((isDriveTo) || (carParkPageState.CarParkFindMode == FindCarParkPageState.FindCarParkMode.DriveTo))
            {
                // Go in to DriveTo mode
                carParkPageState.CurrentFindMode = FindCarParkPageState.FindCarParkMode.DriveTo;
                carParkPageState.CarParkFindModePrevious = FindCarParkPageState.FindCarParkMode.DriveTo;
            }
            else if (isFindNearest)
            {
                // Explicitly asked for Find nearest mode
                carParkPageState.CurrentFindMode = FindCarParkPageState.FindCarParkMode.Default;
                carParkPageState.CarParkFindModePrevious = FindCarParkPageState.FindCarParkMode.Default;
            }
            else
            {
                // No query string value, and the mode wasn't set in the session - therefore page is in default mode
                if (carParkPageState.CurrentFindMode != carParkPageState.CarParkFindModePrevious)
                {
                    carParkPageState.CurrentFindMode = carParkPageState.CarParkFindModePrevious;
                }
                else
                {
                    carParkPageState.CurrentFindMode = FindCarParkPageState.FindCarParkMode.Default;
                }
            }

            //We need to remove the history of door to door and city to city planning:
            if (carParkPageState.CurrentFindMode != FindCarParkPageState.FindCarParkMode.DriveTo)
            {
                carParkPageState.IsFromCityToCity = false;
                carParkPageState.IsFromDoorToDoor = false;
            }
        }

        /// <summary>
        /// Resets the Car park mode set by a calling page
        /// </summary>
        private void ResetCarParkFindMode()
        {
            carParkPageState.CarParkFindMode = FindCarParkPageState.FindCarParkMode.Default;
        }

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			toFromLocationControl.NewLocationFrom += new EventHandler(OnNewLocationFrom);
			toFromLocationControl.NewLocationTo += new EventHandler(OnNewLocationTo);
			headerControl.DefaultActionEvent +=  new EventHandler(this.DefaultActionClick);
			
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
			this.commandBack.Click += new System.EventHandler(this.CommandBackClick);
			this.commandNext.Click += new System.EventHandler(this.CommandNextClick);
		}
		#endregion

		#region Event Handler methods

		/// <summary>
		/// Click event for next button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void CommandNextClick(object sender, EventArgs e)
		{
			if (timeout != true)
			{		
				toFromLocationControl.Search();
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarParkInputDefault;

				if ( currentLocation.Status == TDLocationStatus.Valid)
				{
					SubmitRequest();
				}
			}
		}

		/// <summary>
		/// The from Location has changed. 
		/// this method will update the session and do extra changes specific to the page.
		/// If the from location was valid, it will be updated, otherwise we will update the currentLocation
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnNewLocationFrom(object sender, EventArgs e)
		{
			if (carParkPageState.LocationFrom.Status != TDLocationStatus.Valid)
			{
				carParkPageState.CurrentLocation = toFromLocationControl.OriginLocation;
				carParkPageState.CurrentSearch = toFromLocationControl.OriginSearch;
				// Extra update
				carParkPageState.CurrentSearch.SearchType = SearchType.Locality;
			}
			else
			{
				carParkPageState.LocationFrom = toFromLocationControl.OriginLocation;
				carParkPageState.SearchFrom = toFromLocationControl.OriginSearch;
				// Extra update
				carParkPageState.SearchFrom.SearchType = SearchType.Locality; 
				carParkPageState.InitialiseCurrentLocation();
			}
			carParkPageState.LocationControlType = toFromLocationControl.OriginControlType;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnNewLocationTo(object sender, EventArgs e)
		{
			if (carParkPageState.LocationTo.Status != TDLocationStatus.Valid)
			{
				carParkPageState.CurrentLocation = toFromLocationControl.DestinationLocation;
				carParkPageState.CurrentSearch = toFromLocationControl.DestinationSearch;
				// Extra update
				carParkPageState.CurrentSearch.SearchType = SearchType.Locality;
			}
			else
			{
				carParkPageState.LocationTo = toFromLocationControl.DestinationLocation;
				carParkPageState.SearchTo = toFromLocationControl.DestinationSearch;
				// Extra update
				carParkPageState.SearchTo.SearchType = SearchType.Locality;
				carParkPageState.InitialiseCurrentLocation();
			}
			carParkPageState.LocationControlType = toFromLocationControl.DestinationControlType;
		}
		 
		/// <summary>
		/// // DN079 UEE
		/// Event handler for default action
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void DefaultActionClick(object sender, EventArgs e)
		{
			ImageClickEventArgs imageEventArgs = new ImageClickEventArgs(0,0);
			CommandNextClick(sender, imageEventArgs); 
		}

		
		/// <summary>
		/// Click event for the Back button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void CommandBackClick(object sender, EventArgs e)
		{
			if (carParkPageState.CurrentSearch.CurrentLevel > 0)
				carParkPageState.CurrentSearch.DecrementLevel();
			else
			{
				// if we're not in ambiguity mode then back button was clicked on the input page 
				// so return to Find car route input page
				// Note: the Back button is only displayed on the input page if we clicked on suggestion 
				// link from Find car route
				if (carParkPageState.CurrentSearch.CurrentLevel != 0)			
				{
					if (carParkPageState.Mode == FindAMode.CarPark)	
					{
						// Ensures page remains on Find car park input if user had just pressed Next
						// without entering any Location text
						if ((pageState.Mode == FindAMode.CarPark) || 
							(carParkPageState.LocationControlType.Type != TDJourneyParameters.ControlType.Default))
						{
							carParkPageState.CurrentSearch.ClearSearch();
							carParkPageState.CurrentLocation.Status = TDLocationStatus.Unspecified;
							carParkPageState.LocationControlType.Type = TDJourneyParameters.ControlType.Default;
						}
						else
						{                      
							TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = 
								FindInputAdapter.GetTransitionEventFromMode(FindAMode.Car);

							// Disable AmendMode when leaving the page
							carParkPageState.DisableAmendMode();
						}
					}										
				}
					// stay on the page and display the input mode
				else
				{
					carParkPageState.CurrentSearch.ClearSearch();
					carParkPageState.CurrentLocation.Status = TDLocationStatus.Unspecified;
					carParkPageState.LocationControlType.Type = TDJourneyParameters.ControlType.Default;
				}		
			}
		}

		#endregion
	}
}
