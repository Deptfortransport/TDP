// *********************************************** 
// NAME                 : FindStationInput.aspx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 7/05/2004 
// DESCRIPTION  : Input page that takes/and validates a location input. Part of the FindA Flight process.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindAStationInput.aspx.cs-arc  $ 
//
//   Rev 1.8   Jul 28 2011 16:19:40   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.7   Jan 30 2009 10:43:58   apatel
//Search Engine Optimasation changes - CCN 624
//Resolution for 5229: Search Engin Optimisation Changes -  CCN624
//
//   Rev 1.6   Dec 17 2008 16:27:12   devfactory
//No change.
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.5   Jun 24 2008 11:26:06   mmodi
//Updated check for when not in find a mode
//Resolution for 5027: Find a station - minor display issues
//
//   Rev 1.4   May 06 2008 09:39:26   apatel
//Added alt text for the header image
//Resolution for 4934: Accessibility issues
//
//   Rev 1.3   May 01 2008 17:24:24   mmodi
//Updated to display session timeout error
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.2   Mar 31 2008 13:24:18   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Jan 30 2008 14:45:00 apatel
//  Change page to add menu on left and right and also change the layout positions of buttons.
//
//   Rev 1.0   Nov 08 2007 13:29:18   mturner
//Initial revision.
//
//DEVFACTORY FEB 21 2008 sbarker
//Page icon added
//
//   Rev 1.55   Feb 23 2006 19:09:42   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.54   Feb 10 2006 15:08:50   build
//Automatically merged from branch for stream3180
//
//   Rev 1.53.1.1   Dec 12 2005 16:45:24   tmollart
//Modified to use new initialise parameters method on session manager. Removed any code that redirects users to existing results.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.53.1.0   Nov 29 2005 18:57:38   RGriffith
//Changes applied for using new HeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.53   Nov 29 2005 16:16:10   ECHAN
//Fixed the StationInputPage help button
//
//   Rev 1.52   Nov 23 2005 19:13:54   RGriffith
//Code review suggestions for stream2880
//
//   Rev 1.51   Nov 17 2005 11:56:44   pcross
//Manual merge of stream2880
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.50   Nov 10 2005 14:32:36   NMoorhouse
//TD093 UEE Input Pages - Soft Content
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.49.1.0   Nov 07 2005 19:07:40   schand
//Merged stream2880 (FindAPlaceControl)changes to this trunk
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.49   Nov 03 2005 16:18:12   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.48.1.0   Oct 06 2005 10:54:30   rgreenwood
//TD089 ES020 Image Button Replacement - HTML buttons for Back and Next buttons
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.48   Sep 29 2005 12:50:20   build
//Automatically merged from branch for stream2673
//
//   Rev 1.47.1.1   Sep 16 2005 17:29:58   Schand
//DN079 UEE
//Updates for Code Review
//Resolution for 2756: DEL 8 Stream: Add default key functionality on Input pages to Next button
//
//   Rev 1.47.1.0   Sep 09 2005 14:14:40   Schand
//DN079 UEE Enter Key.
//Updates for UEE.
//Resolution for 2756: DEL 8 Stream: Add default key functionality on Input pages to Next button
//
//   Rev 1.47   May 24 2005 16:09:10   ralavi
//Call method InitialiseJourneyParametersPageStates() only when the page is  entered for the first time to ensure page mode is not wrongly changed to station/airport.
//Resolution for 2536: Find a Flight - problems encountered when finding nearest airport
//
//   Rev 1.46   May 11 2005 16:33:34   jbroome
//Call to InitialiseJourneyParametersPageStates() to ensure that page doesn't crash when first entered.
//Resolution for 2476: Hyperlink from Maps page to FindAStationInput causes a crash
//
//   Rev 1.45   Apr 15 2005 13:11:14   COwczarek
//Fix to previous check-in.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.44   Apr 15 2005 12:48:04   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.43   Mar 21 2005 09:17:38   rscott
//NoWrap added to header div to prevent TDOnTheMove tab image button from wrapping.
//
//   Rev 1.42   Dec 14 2004 10:12:50   tmollart
//IR1850: Code changed so that different messages displayed based on status of page.
//Resolution for 1850: English/Welsh text changes for find a's
//
//   Rev 1.41   Nov 26 2004 15:31:02   asinclair
//Fix for IR1720
//
//   Rev 1.40   Nov 03 2004 12:54:10   passuied
//Changes to enable a new FindAMode TrunkStation similar to Trunk but with differences...
//
//   Rev 1.39   Nov 02 2004 12:00:58   passuied
//displayed different title in trunk mode
//
//   Rev 1.38   Oct 27 2004 17:14:20   esevern
//correction to setting of title, so that welsh text is displayed correctly
//
//   Rev 1.37   Oct 19 2004 13:30:42   esevern
//correction to note text when ambiguous location
//
//   Rev 1.36   Oct 07 2004 11:52:22   esevern
//corrected setting of station/airport title text: IR1623
//
//   Rev 1.35   Oct 01 2004 13:25:44   jmcallister
//IR1623 - Revisited this file as Prior IR appeared to be no longer fixed.
//
//   Rev 1.34   Sep 24 2004 16:21:20   JHaydock
//IR1623 - Update to display of text above input box
//
//   Rev 1.33   Sep 18 2004 16:16:14   COwczarek
//Check for NotFindAMode parameter in query string. This 
//parameter, if present, indicates that this page is not being
//used from another Find A function.
//Resolution for 1561: Find A sub navigation does not use real hyperlinks
//
//   Rev 1.32   Sep 06 2004 18:14:06   asinclair
//Updated for IR 1472 = if findpagestate is null then give it a value so that this page can be linked to without going to a find a page
//
//   Rev 1.31   Sep 01 2004 11:23:06   passuied
//displayed back button when control not in default mode
//Resolution for 1460: All Find a...The number of each journey in the Journey summary options should be dispalyed
//
//   Rev 1.30   Aug 27 2004 10:43:20   passuied
//added back button at top of page
//
//   Rev 1.29   Aug 18 2004 14:19:26   passuied
//Use of non duplicated code to Get transitionevent from FindAMode mode
//
//   Rev 1.28   Aug 16 2004 17:52:20   passuied
//Implemented back button for Station mode.
//Resolution for 1346: No back button for Find A Station/Airport location resolution
//
//   Rev 1.27   Jul 27 2004 15:18:08   passuied
//Del6.1 Added "Station name" image displayed in result table
//
//   Rev 1.26   Jul 27 2004 14:03:14   passuied
//FindStation Del6.1 :Finalised version 
//
//   Rev 1.25   Jul 26 2004 20:23:56   passuied
//Changes to implement AmendSeach Functionality. Created and Amend mode in the tristate to enable the display of a valid location inside the locationUnspecified control.
//We send this mode when a one use session key has been set by a click on AmendSearch button.
//Also tweak in toFromLocationControl to display the to and from location/station controls correctly
//
//   Rev 1.24   Jul 23 2004 17:42:00   passuied
//FindStation 6.1. Labels and text updates
//
//   Rev 1.23   Jul 22 2004 18:06:04   passuied
//Integration between pages and move of code to location service
//
//   Rev 1.22   Jul 22 2004 16:16:42   RPhilpott
//Pull most of the FindStations code into the LocationService - no longer need to pass ref to LocationSearch instance.
//
//   Rev 1.21   Jul 21 2004 15:26:36   passuied
//Changes to implement the New Location Button func for all different controls
//
//   Rev 1.20   Jul 21 2004 10:51:40   passuied
//Re work for integration with FindStation del6.1. Working. Needs work on resources
//
//   Rev 1.19   Jul 14 2004 16:36:32   passuied
//Changes for del6.1. FindFlight functionality working after SessionManager changes.
//
//   Rev 1.18   Jul 14 2004 13:00:36   passuied
//Changes in SessionManager with impact in Web for Del 6.1
//Compiles
//
//   Rev 1.17   Jul 12 2004 14:13:46   passuied
//use of new property Mode of FindPageState base class
//
//   Rev 1.16   Jul 09 2004 15:58:00   passuied
//minor changes
//
//   Rev 1.15   Jul 09 2004 15:53:30   passuied
//problem with radio button SearchType fixed
//
//   Rev 1.14   Jul 09 2004 13:08:12   passuied
//Updated FindStation and filter methods for FindStation 6.1 back end
//
//   Rev 1.13   Jul 01 2004 14:12:52   passuied
//changes following exhaustive testing
//
//   Rev 1.12   Jun 30 2004 15:43:06   passuied
//Cleaning up
//
//   Rev 1.11   Jun 23 2004 12:40:36   passuied
//change in back click event
//
//   Rev 1.10   Jun 23 2004 11:23:08   passuied
//addition of help for findStation pages
//
//   Rev 1.9   Jun 11 2004 17:18:32   passuied
//added code to create JourneyParametersFlight if PageMode is FindStation
//
//   Rev 1.8   Jun 04 2004 15:20:02   passuied
//added call FindStationPageState.Initialise when !Page.IsPostback
//
//   Rev 1.7   Jun 02 2004 16:40:22   passuied
//working version
//
//   Rev 1.6   May 28 2004 17:51:24   passuied
//update as part of FindStation development
//
//   Rev 1.5   May 24 2004 12:12:38   passuied
//checked in to comply with control changes
//
//   Rev 1.4   May 21 2004 15:49:56   passuied
//partly working Find station pages and controls. Check in for backup
//
//   Rev 1.3   May 12 2004 17:47:24   passuied
//compiling check in for FindStation pages and related
//
//   Rev 1.2   May 11 2004 15:56:14   passuied
//added 2 new pages
//
//   Rev 1.1   May 11 2004 14:19:18   passuied
//first working version of page. Transitions missing


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
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.ScreenFlow;


namespace TransportDirect.UserPortal.Web.Templates
{


	/// <summary>
	/// Input page that takes/and validates a location input. Part of the FindA Flight process.
	/// </summary>
	public partial class FindStationInput : TDPage
	{
		#region Controls declaration
		protected TransportDirect.UserPortal.Web.Controls.FindToFromLocationsControl toFromLocationControl;
		// DN079 UEE
		protected HeaderControl headerControl;
		
		#endregion

		#region Resource keys declaration
		private const string RES_TITLE_NOTSTATIONMODE		= "FindStationInput.labelTitle.NotStationMode";
		private const string RES_TITLE_STATIONMODE_NOVALID	= "FindStationInput.labelTitle.StationMode.NoValid";
		private const string RES_TITLE_STATIONMODE_VALID	= "FindStationInput.labelTitle.StationMode.Valid"; 

		//Station mode resource keys
		private const string RES_NOTE_STATIONMODE			= "FindStationInput.labelNote.StationMode";
		private const string RES_NOTE_STATIONMODE_VALID		= "FindStationInput.labelNote.StationMode.Valid";
		private const string RES_NOTE_STATIONMODE_NOVALID	= "FindStationInput.labelNote.StationMode.NoValid";

		//Not Station mode resource keys
		private const string RES_NOTE_NOTSTATIONMODE		  = "FindStationInput.labelNote.NotStationMode";
		private const string RES_NOTE_NOTSTATIONMODE_NOTVALID = "FindStationInput.labelNote.NotStationMode.NotValid";
	
		#endregion

		#region Private variables declaration
		
		private SessionManager.TDJourneyParameters journeyParameters;
		private SessionManager.FindStationPageState stationPageState;
		private SessionManager.FindPageState pageState;
		private DataServices.DataServices populator;

		private LocationSearch currentSearch = null;
		private TDLocation currentLocation = null;

		#endregion
	
		#region Constructor and Page Load
		/// <summary>
		/// Default constructor
		/// </summary>
		public FindStationInput()
		{
			pageId = PageId.FindStationInput;
			populator = (DataServices.DataServices) TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
		    			
		}

		/// <summary>
		/// Page Load Event Handler
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			LoadSessionVariables();

            #region Display Session Errors
            // Check if there are any errors in the InputPageState and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, TDSessionManager.Current.InputPageState.InputSessionErrors);

            // Clear the error messages
            TDSessionManager.Current.InputPageState.InputSessionErrors = null;
            #endregion

            //Set page icon:
            imageFindAStation.ImageUrl = GetResource("HomeFindAPlace.imageFindAStation.ImageUrl");
            imageFindAStation.AlternateText = " ";


			TDSessionManager.Current.Partition = TDSessionPartition.TimeBased;

			// Check if has came from FindAPlaceControl
			if( TDSessionManager.Current.GetOneUseKey(SessionKey.FindALocationFromHomePage) != null) 
			{
				pageState = FindPageState.CreateInstance(FindAMode.Station); 
				TDSessionManager.Current.FindPageState = pageState;

				if (pageState.Mode == FindAMode.Station 
					&& stationPageState.LocationFrom.Status == TDLocationStatus.Unspecified
					&& stationPageState.LocationTo.Status == TDLocationStatus.Unspecified)
					stationPageState.StationTypes = toFromLocationControl.FromLocationControl.StationTypesSelected;

				TDSessionManager.Current.JourneyParameters = new TDJourneyParametersMulti();				
				TDSessionManager.Current.InitialiseJourneyParameters(FindAMode.Station);
				TDSessionManager.Current.JourneyParameters.Initialise();  
				toFromLocationControl.FromLocationControl.AmendMode = stationPageState.AmendMode;

				// Also Hide the back button
				commandBack.Visible = false;
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
					// If station/Airport tab is selected directlty on entering the portal then ensure all the JourneyParametersPageSates
					// are initialised.
					if (pageState == null)
					{
						TDSessionManager.Current.InitialiseJourneyParameters(FindAMode.Station);
					}
				}
			
				// Force Find A mode to Find A Station if the user is accessing the page 
				// directly (e.g. via a bookmark) in which case pageState is null or
				// if the query string contains NotFindAMode. If present this indicates 
				// that the page is not being used from another Find A function.
                bool notFindAMode = false;
                if (TDSessionManager.Current.GetOneUseKey(SessionKey.NotFindAMode) != null)
                {
                    bool.TryParse(TDSessionManager.Current.GetOneUseKey(SessionKey.NotFindAMode), out notFindAMode);
                }
			
				if(pageState == null || Request.QueryString["NotFindAMode"] != null || notFindAMode)
				{
					pageState = FindPageState.CreateInstance(FindAMode.Station); 
					TDSessionManager.Current.FindPageState = pageState;										 
				}	
			
				// We can  come to this page (for not postback) at 2 different moments
				// 1. for a new search --> whole stationPageState must be initialised
				// 2. when one of the locations is valid and the other is not --> just initialise the CurrentLocation and Table state
				if (!Page.IsPostBack)
				{
					// if comes from the FindXXXinput pages, initialise everything except
					// the locationType which is set on the page...
					if (pageState.Mode != FindAMode.Station)
					{
						stationPageState.InitialiseLocation(FindStationPageState.CurrentLocationType.From);
						stationPageState.InitialiseLocation(FindStationPageState.CurrentLocationType.To);
						stationPageState.InitialiseCurrentLocation();
						stationPageState.InitialiseTableState();

					}
					else
					{
						// ^ = XOR --> true if Just one of them is valid
						if (
							(stationPageState.LocationFrom.Status == TDLocationStatus.Valid)
							^ (stationPageState.LocationTo.Status == TDLocationStatus.Valid))
						{
							stationPageState.InitialiseCurrentLocation();
							stationPageState.InitialiseTableState();
						}
							// don't initialise if amendKey has been set. we want the values kept in the boxes.
						else 
						{
						
							if( !stationPageState.AmendMode)
							{
								// In Station mode, we start from scratch!
								// new FindStationPageState
								// new JourneyParameters
								TDSessionManager.Current.JourneyParameters.Initialise();
								stationPageState.Initialise();
							}
						
						}
					}
				}
			}
			PopulateControls();

            PageTitle = GetResource("FindAStationInput.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");
           
            if (!Page.IsPostBack)
			{
				// Disable the GisQuery functionality for performance issues. No useless extra
				// GetLocationDetails done here.
				LocationSearchHelper.DisableGisQuery(stationPageState.CurrentSearch);

				switch(pageState.Mode)
				{
					case FindAMode.Flight:
						stationPageState.StationTypes = new StationType[]{StationType.Airport};
						break;
					case FindAMode.Train:
						stationPageState.StationTypes = new StationType[]{StationType.Rail};
						break;
					case FindAMode.Coach:
						stationPageState.StationTypes = new StationType[]{StationType.Coach};
						break;
				}
				
			}
			else
			{
				// save StationTypes only if in FindStation mode and if no location defined 
				if (pageState.Mode == FindAMode.Station 
					&& stationPageState.LocationFrom.Status == TDLocationStatus.Unspecified
					&& stationPageState.LocationTo.Status == TDLocationStatus.Unspecified)
					stationPageState.StationTypes = toFromLocationControl.FromLocationControl.StationTypesSelected;

			}

			// If it has came from FindAControl then perform the search
			if (TDSessionManager.Current.GetOneUseKey(SessionKey.FindALocationFromHomePage) != null)
			{ 
				toFromLocationControl.RefreshControl(); 
				toFromLocationControl.Search();			
				if ( currentLocation.Status == TDLocationStatus.Valid)
				{
					TDSessionManager.Current.Session[SessionKey.Transferred] = false;
					LocationSearchHelper.FindStations(ref currentLocation, stationPageState.StationTypes);
					stationPageState.StationResultsTable = FindStationHelper.BuildResultsDataTable(currentLocation);
					TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationInputUnambiguous;

					// Disable AmendMode when leaving the page
					stationPageState.DisableAmendMode();
				}
			}
			//------------------------------------------

			//DN079 UEE
			// Adding client side script for user navigation (when user hit enter, it should take the default action)
			UserExperienceEnhancementHelper.AddClientForUserNavigationDefaultAction(this.Page);

            #region CCN 0427 left hand navigation changes
            //Added for white labelling:
            ConfigureLeftMenu("FindAStationInput.clientLink.BookmarkTitle", "Home.clientLink.LinkText", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuFindAPlace);
            #endregion

			SetHelpUrl();

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFindAStationInput);
            expandableMenuControl.AddExpandedCategory("Related links");
		}

		private void SetControlsVisibility()
		{
			// invisible if in station mode or if we want to enable to drill up in location hierarchy
			panelBackTop.Visible = !(pageState.Mode == FindAMode.Station)|| stationPageState.LocationControlType.Type != TDJourneyParameters.ControlType.Default;
			
		}

		//setting the help url depending on the mode of the page
		private void SetHelpUrl()
		{
			if(stationPageState.LocationControlType.Type != TDJourneyParameters.ControlType.Default)
			{
				Helpbuttoncontrol1.HelpUrl = GetResource("FindAStationInput.HelpAmbiguityUrl");
			}
			else
			{
				Helpbuttoncontrol1.HelpUrl = GetResource("FindAStationInput.HelpPageUrl");
			}
		}
		
		/// <summary>
		/// EventHandler For event just before page renders
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			LoadResources();
			SetControlsVisibility();

			if (!Page.IsPostBack)
			{// Refresh the checklist with info from session
				toFromLocationControl.FromLocationControl.StationTypesSelected = stationPageState.StationTypes;
			}

			base.OnPreRender(e);
		}
		#endregion

		#region Private methods
		
		/// <summary>
		/// Populate controls properties
		/// </summary>
		private void PopulateControls()
		{
			// Fills the location essential properties
			if (stationPageState.LocationFrom.Status == TDLocationStatus.Valid)
			{
				toFromLocationControl.OriginLocation = stationPageState.LocationFrom;
				toFromLocationControl.OriginSearch = stationPageState.SearchFrom;

			}
			else
			{	// Current Location will be assigned to Origin Control
				toFromLocationControl.OriginLocation = stationPageState.CurrentLocation;
				toFromLocationControl.OriginSearch = stationPageState.CurrentSearch;
				toFromLocationControl.OriginControlType = stationPageState.LocationControlType;


			}

			if (stationPageState.LocationTo.Status == TDLocationStatus.Valid)
			{
				toFromLocationControl.DestinationLocation = stationPageState.LocationTo;
				toFromLocationControl.DestinationSearch = stationPageState.SearchTo;

			}
			else
			{
				// Current Location will be assigned to DestinationControl
				toFromLocationControl.DestinationLocation = stationPageState.CurrentLocation;
				toFromLocationControl.DestinationSearch = stationPageState.CurrentSearch;
				toFromLocationControl.DestinationControlType = stationPageState.LocationControlType;

			}

			// Refresh the amend mode from session
			toFromLocationControl.FromLocationControl.AmendMode = stationPageState.AmendMode;

			
		}
		/// <summary>
		/// Load page resources
		/// </summary>
		private void LoadResources()
		{
			commandBack.Text = GetResource("FindStationInput.commandBack.Text");
			commandBack.ToolTip = GetResource("FindStationInput.commandBack.ToolTip");
			commandNext.Text = GetResource("FindStationInput.commandNext.Text");
			commandNext.ToolTip = GetResource("FindStationInput.commandNext.ToolTip");

			SetText();

		}

		/// <summary>
		/// Set Text for controls in page
		/// </summary>
		private void SetText()
		{
			if (pageState.Mode != FindAMode.Station && pageState.Mode != FindAMode.Trunk && pageState.Mode != FindAMode.TrunkStation)
			{
				SetTextNotStationMode();
			}
			else
			{
				SetTextStationMode();
			}
		}

		/// <summary>
		/// Get string corresponding to stationType
		/// </summary>
		/// <returns>string stationtype</returns>
		private string GetStationTypeString()
		{
			string sStationType = string.Empty;
			// Get Station type string
			if (pageState.Mode == FindAMode.Flight)
			{
				sStationType = GetResource(FindStationHelper.RES_AIRPORT);
			}
			else
			{
				sStationType = GetResource(FindStationHelper.RES_STATION);
			}

			return sStationType;
		}

		/// <summary>
		/// Get direction (from/to)string in current language
		/// </summary>
		/// <returns>string direction</returns>
		private string GetDirectionString()
		{
			string sDirection = string.Empty;
			if (stationPageState.LocationType == FindStationPageState.CurrentLocationType.From)
			{
				sDirection = GetResource(FindStationHelper.RES_FROM);
			}
			else
			{
				sDirection = GetResource(FindStationHelper.RES_TO);
			}

			return sDirection;
		}

		/// <summary>
		/// station/airport title
		/// </summary>
		private void SetTextNotStationMode()
		{

			string sStationType = GetStationTypeString();
			string sDirection = GetDirectionString();
			string sNearest = GetResource(FindStationHelper.RES_TITLE_NEAREST);
			string sTravel = GetResource(FindStationHelper.RES_TITLE_TRAVEL);

			// Set Text for TITLE
			labelFindStationTitle.Text = string.Format(GetResource(RES_TITLE_NOTSTATIONMODE), sStationType, sDirection);            

			// Set Text for Note
			// IR1850: if in ambiguity mode a differnet note has to be displayed
			if (stationPageState.CurrentLocation.Status == TDLocationStatus.Ambiguous)
			{
				labelNote.Text = string.Format(GetResource(RES_NOTE_NOTSTATIONMODE_NOTVALID), sStationType);
			}
			else
			{
				labelNote.Text = string.Format(GetResource(RES_NOTE_NOTSTATIONMODE), sStationType);
			}
		}

		/// <summary>
		/// station title
		/// </summary>
		private void SetTextStationMode()
		{
			string sStationType = GetResource(FindStationHelper.RES_STATION_AIRPORT);
			string sNearest = GetResource(FindStationHelper.RES_TITLE_NEAREST);
			string sTravel = GetResource(FindStationHelper.RES_TITLE_TRAVEL);
			string sDirection = GetDirectionString();

			// for the title: either both are unspecified or one is valid
			if(stationPageState.LocationFrom.Status == TDLocationStatus.Unspecified &&
				stationPageState.LocationTo.Status == TDLocationStatus.Unspecified)
			{
				// CCN 624 title changed changed code line "labelFindStationTitle.Text = string.Format(
                //  GetResource(RES_TITLE_STATIONMODE_NOVALID), sStationType);" to following line

                labelFindStationTitle.Text = GetResource("FindStationInput.labelTitle");
			}
			else if(stationPageState.LocationFrom.Status == TDLocationStatus.Valid 
				|| stationPageState.LocationTo.Status == TDLocationStatus.Valid)
			{
				labelFindStationTitle.Text = string.Format(GetResource(RES_TITLE_STATIONMODE_VALID), 
					sStationType, sDirection);
			}

			// both are unspecified
			if(stationPageState.LocationFrom.Status == TDLocationStatus.Unspecified &&
				stationPageState.LocationTo.Status == TDLocationStatus.Unspecified)
			{
				labelNote.Text = GetResource(RES_NOTE_STATIONMODE);
			}

			// one unspecified and other valid
			if( (stationPageState.LocationFrom.Status == TDLocationStatus.Unspecified 
				&& stationPageState.LocationTo.Status == TDLocationStatus.Valid)
				||
				(stationPageState.LocationFrom.Status == TDLocationStatus.Valid 
				&& stationPageState.LocationTo.Status == TDLocationStatus.Unspecified) )
			{
				labelNote.Text = string.Format(GetResource(RES_NOTE_STATIONMODE_VALID), sStationType, sDirection);
			}

			// either ambiguous
			if(toFromLocationControl.OriginLocation.Status == TDLocationStatus.Ambiguous
				|| toFromLocationControl.DestinationLocation.Status == TDLocationStatus.Ambiguous) 
			{
				labelNote.Text = GetResource(RES_NOTE_STATIONMODE_NOVALID);
			}
		}



		/// <summary>
		/// Load Session variables and fills Control properties
		/// </summary>
		private void LoadSessionVariables()
		{
			journeyParameters = TDSessionManager.Current.JourneyParameters;
			stationPageState = TDSessionManager.Current.FindStationPageState;
			pageState = TDSessionManager.Current.FindPageState;

			currentSearch = stationPageState.CurrentSearch;
			currentLocation = stationPageState.CurrentLocation;
			
		}

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			toFromLocationControl.NewLocationFrom += new EventHandler(OnNewLocationFrom);
			toFromLocationControl.NewLocationTo += new EventHandler(OnNewLocationTo);

			// DN079 UEE
			// Event Handler for default action button
			headerControl.DefaultActionEvent +=  new EventHandler(this.DefaultActionClick);

			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
            commandBack.Click += new EventHandler(this.CommandBackClick);
            commandNext.Click += new EventHandler(this.CommandNextClick);
		}
		#endregion

		#region Event Handler methods

		/// <summary>
		/// Click event for next button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		protected void CommandNextClick(object sender, EventArgs e)
		{
			toFromLocationControl.Search();
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationInputDefault;

			if ( currentLocation.Status == TDLocationStatus.Valid)
			{
				LocationSearchHelper.FindStations(ref currentLocation, stationPageState.StationTypes);
				stationPageState.StationResultsTable = FindStationHelper.BuildResultsDataTable(currentLocation);
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationInputUnambiguous;

                // Disable AmendMode when leaving the page
				stationPageState.DisableAmendMode();
			}
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
		protected void CommandBackClick(object sender, EventArgs e)
		{
			if (stationPageState.CurrentSearch.CurrentLevel > 0)
				stationPageState.CurrentSearch.DecrementLevel();
			else
			{
				if (pageState.Mode != FindAMode.Station)
				{
					TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = FindInputAdapter.GetTransitionEventFromMode(pageState.Mode);
					// Disable AmendMode when leaving the page
					stationPageState.DisableAmendMode();
				}
					// in Station Mode we stay on the page and display the input mode
					// ClearSearch
					// Set Default control type
					// Set location status to unspecified
				else
				{
					stationPageState.CurrentSearch.ClearSearch();
					stationPageState.CurrentLocation.Status = TDLocationStatus.Unspecified;
					stationPageState.LocationControlType.Type = TDJourneyParameters.ControlType.Default;
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
			if (stationPageState.LocationFrom.Status != TDLocationStatus.Valid)
			{
				stationPageState.CurrentLocation = toFromLocationControl.OriginLocation;
				stationPageState.CurrentSearch = toFromLocationControl.OriginSearch;
				// Extra update
				stationPageState.CurrentSearch.SearchType = SearchType.Locality;
			}
			else
			{
				stationPageState.LocationFrom = toFromLocationControl.OriginLocation;
				stationPageState.SearchFrom = toFromLocationControl.OriginSearch;
				// Extra update
				stationPageState.SearchFrom.SearchType = SearchType.Locality;

				stationPageState.InitialiseCurrentLocation();
			}

			stationPageState.LocationControlType = toFromLocationControl.OriginControlType;
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnNewLocationTo(object sender, EventArgs e)
		{
			if (stationPageState.LocationTo.Status != TDLocationStatus.Valid)
			{
				stationPageState.CurrentLocation = toFromLocationControl.DestinationLocation;
				stationPageState.CurrentSearch = toFromLocationControl.DestinationSearch;
				// Extra update
				stationPageState.CurrentSearch.SearchType = SearchType.Locality;
			}
			else
			{
				stationPageState.LocationTo = toFromLocationControl.DestinationLocation;
				stationPageState.SearchTo = toFromLocationControl.DestinationSearch;
				// Extra update
				stationPageState.SearchTo.SearchType = SearchType.Locality;

				stationPageState.InitialiseCurrentLocation();

			}

			stationPageState.LocationControlType = toFromLocationControl.DestinationControlType;

		}

		#endregion

	}
}
