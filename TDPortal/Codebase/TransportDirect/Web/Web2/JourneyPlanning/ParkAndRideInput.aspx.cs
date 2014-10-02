// *********************************************** 
// NAME                 : ParkAndRideInput.aspx
// AUTHOR               : Hassan ALKATIB
// DATE CREATED         : 06/03/2006 
// DESCRIPTION  : Input Page for Park and Ride
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/ParkAndRideInput.aspx.cs-arc  $ 
//
//   Rev 1.24   Jul 28 2011 16:20:42   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.23   Mar 14 2011 15:12:06   rphilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.22   Oct 29 2010 10:07:00   rbroddle
//Removed explicit wire up to Page_Init & Page_PreRender as AutoEventWireUp=true for this page so they were firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.21   May 13 2010 13:05:28   mmodi
//Added code call to clear the printable map session information 
//Resolution for 5535: Printable maps session logic improvement
//
//   Rev 1.20   Apr 07 2010 15:24:46   mturner
//Changes to resolve UK:7264806
//Resolution for 5492: Issue with amend park and ride journey
//
//   Rev 1.19   Mar 26 2010 11:31:36   mturner
//Added code to clear journey results from session if you are coming directly from another planner.
//
//Resolution for 5481: Session issue when going from FAT to D2D using the left hand menu
//
//   Rev 1.18   Feb 26 2010 16:14:46   PScott
//Meta tag and title changes on numerous pages
//RS71001 
//SCR 5408
//Resolution for 5408: Meta tags
//
//   Rev 1.17   Jan 29 2010 14:45:34   mmodi
//Updated to reset CycleResult to correct error when planning journey after coming directly from Cycle journey details
//Resolution for 5388: Cycle Planner - Server error when planning Door to door after a cycle journey
//
//   Rev 1.16   Jan 19 2010 13:21:04   mmodi
//Updated for New Mapping RFC73 patch
//Resolution for 5376: Maps - ESRI RFC073 mandatory changes needed for patch
//
//   Rev 1.15   Dec 14 2009 11:06:16   apatel
//stop the map showing when new location button click after amend
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.14   Dec 09 2009 11:34:00   mmodi
//When Clear button is clicked, reset the map
//
//   Rev 1.13   Dec 03 2009 16:01:00   apatel
//input page mapping enhancement related changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.12   Dec 02 2009 11:54:14   apatel
//Input page work flow change for mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.11   Nov 30 2009 10:19:42   apatel
//mapping enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.10   Nov 30 2009 09:58:26   apatel
//input page find on map workflow changed
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.9   Nov 18 2009 11:42:08   apatel
//Added oneusekey for findonmap button click to move on to findmapinput page
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.8   Nov 18 2009 11:20:42   apatel
//Updated for Journey input planner mapping enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.7   Nov 10 2009 11:30:20   apatel
//Find Input pages mapping enhancement changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.6   Jan 30 2009 10:44:22   apatel
//Search Engine Optimasation changes - CCN 624
//Resolution for 5229: Search Engin Optimisation Changes -  CCN624
//
//   Rev 1.5   Dec 16 2008 10:39:20   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.4   May 01 2008 17:23:16   mmodi
//Updated to display session timeout error
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.3   Apr 08 2008 16:11:34   scraddock
//Advanced Text Not Hidden
//Resolution for 4847: Advanced Text Not Hidden
//
// rev devfactory 8 apr sbarker
// Advanced text hidden when advanced pressed, or in ambiguity mode
//
//   Rev 1.2   Mar 31 2008 13:25:06   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Jan 31 2008 14:21:00 sjohal
//  Modified to set PageOptionsControls inside the blue boxes. New PageOptionsControl added to page which
//  will be display/hide when hide/advance button will be clicked.
//
//   Rev 1.0   Nov 08 2007 13:30:26   mturner
//Initial revision.
//
//DEVFACTORY FEB 21 2008 sbarker
//Page icon added
//
//   Rev 1.25   Sep 03 2007 15:25:36   pscott
//CCN407 IR 4490
//title and key word changes for Google natural search
//
//   Rev 1.24   Jul 02 2007 14:43:24   pscott
//USD UK 1150530
//SCR 4459
//Default Time on entry
//
//   Rev 1.23   Jun 07 2007 15:14:18   mmodi
//Added event to handle Submit event from PreferenceOptionsControl
//Resolution for 4409: Minor Portal Changes
//
//   Rev 1.22   Jun 06 2007 12:17:54   sangle
//Added Footnote to journey results page for all journeys except returns and ParkAndRide.
//
//   Rev 1.21   May 17 2006 10:24:22   CRees
//IR4082 - Moved control hiding to pre-render method, to fix issue with Clear Page from Ambiguity page.
//
//   Rev 1.20   May 16 2006 16:30:16   CRees
//Hid return date control for pre-Del 8.1 park and ride issues.
//
//   Rev 1.19   May 05 2006 10:48:02   mtillett
//Add code to get previous state from stack when back button pressed (as for all other input page)
//Resolution for 4076: DN058 Park and Ride Phase 2: Arriving on ambiguity page from results page and clicking 'Back' sends user to input page
//
//   Rev 1.18   May 03 2006 11:01:32   mtillett
//Add comments to fix
//Resolution for 4039: DN058 Park and Ride Phase 2 - Find a car route has P&R sites drop-down box in To field
//
//   Rev 1.17   May 03 2006 09:24:48   mtillett
//Code added to Park and Ride and Find A Car input pages to reset the destiniation information when the SearchType is incorrect on load of page and back button. 
//Resolution for 4039: DN058 Park and Ride Phase 2 - Find a car route has P&R sites drop-down box in To field
//
//   Rev 1.16   May 02 2006 13:16:08   mtillett
//Associated the new ParkAndRideInput context with the park and ride input page
//Resolution for 4057: DN058 Park & Ride Phase 2: Schemes link missing from Plan to Park and Ride page
//
//   Rev 1.15   Apr 28 2006 16:51:46   mtillett
//TDLocation for the destination not recreated when the TDLocationDestination status set to Valid
//Resolution for 4032: DN058 Park and Ride Phase 2 - Resolved park and ride site becomes ambiguous again after using map to select origin
//
//   Rev 1.14   Apr 28 2006 12:13:14   esevern
//Check for one use key (if park and ride destination has been set) and populates initialised journey parameters appropriately. Performs check for fixed location before writing park and ride scheme selection to journey parameters.
//Resolution for 4015: DN058 Park and Ride Phase 2 - Park & ride site on input page not automatically populated
//
//   Rev 1.13   Apr 27 2006 11:21:02   mtillett
//Prevent calendar button dislpay on ambiguity page after next or back buttons clicked
//Resolution for 3510: Apps: Calendr Control problems on input/ambiguity screen
//
//   Rev 1.12   Apr 26 2006 14:37:56   AViitanen
//Amended help url langstring key.
//
//   Rev 1.11   Apr 21 2006 15:03:56   jbroome
//Ensure drop down is cleared when clicking Clear button
//Resolution for 3920: DN077 Landing Page: Clear page does not reset planner input page
//
//   Rev 1.10   Apr 20 2006 10:56:18   esevern
//corrected reset of park and ride scheme drop down list - should not be reset when page in ambiguity mode
//Resolution for 3803: DN058 Park and Ride Phase 2 - Amend from journey results page does not retain values
//
//   Rev 1.9   Apr 20 2006 10:30:54   esevern
//added check for park and ride scheme not being null before trying to set the chosen scheme if we're in amend mode (if the user has selected 'clear', there won't be one)
//Resolution for 3803: DN058 Park and Ride Phase 2 - Amend from journey results page does not retain values
//
//   Rev 1.8   Apr 19 2006 18:27:04   AViitanen
//Changed context to ParkAndRide. 
//Resolution for 3945: DN058 Park and Ride: incorrect suggestion links on Plan to Park and Ride input page
//
//   Rev 1.7   Apr 18 2006 16:09:12   esevern
//Added setting of destination location (selected park and ride location) to journey parameters and call to refresh the park and ride selection control on pre-render if the page is currently in AmendMode
//Resolution for 3803: DN058 Park and Ride Phase 2 - Amend from journey results page does not retain values
//
//   Rev 1.6   Apr 11 2006 16:48:12   rgreenwood
//IR 3828: Changed Page Title
//IR3829: Changed Help URL
//Resolution for 3828: DN058 Park and Ride Phase 2 - Input page browser window header
//Resolution for 3829: DN058 Park and Ride Phase 2 - Help text
//
//   Rev 1.5   Apr 11 2006 12:34:44   esevern
//added resetting of the journeyParameters destination search type to 'ParkAndRide' in the 'Clear' button event handler, so that the destination location control displays correctly with the ParkAndRide dropdown list.
//Resolution for 3801: DN058 Park and Ride Phase 2 - Clear on Plan to park and ride page
//
//   Rev 1.4   Mar 31 2006 09:32:00   tolomolaiye
//Updated to use the new Park and ride input adapter
//
//   Rev 1.3   Mar 21 2006 12:20:28   halkatib
//Applied changes resluting from code review
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.2   Mar 20 2006 13:58:12   halkatib
//removed conditional check on initialisation of journey parameteres and ammended page title to display as Plan to park and ride.
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.1   Mar 14 2006 10:32:44   halkatib
//Changes made for park and ride phase 2
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.0   Mar 07 2006 11:21:38   halkatib
//Initial revision.
//
//


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
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Web.Support;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.SuggestionLinkService;
using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
using System.Collections.Generic;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Input Page for FindA Car
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class ParkAndRideInput : TDPage
	{
		protected FindToFromLocationsControl locationsControl;
		protected FindLeaveReturnDatesControl dateControl;
		protected FindCarPreferencesControl preferencesControl;
		protected FindCarJourneyOptionsControl journeyOptionsControl;
		protected ExpandableMenuControl expandableMenuControl; 
		protected HeaderControl headerControl;

		/// <summary>
		/// Holds user's current page state for this page
		/// </summary>
		private FindCarPageState pageState;

		/// <summary>
		/// Hold user's current journey parameters for train only journey
		/// </summary>
		private TDJourneyParametersMulti journeyParams;

		/// <summary>
		/// Holds user's map current page state.
		/// </summary>
		private InputPageState inputPageState;

		/// <summary>
		/// Helper class responsible for common methods to Find A pages
		/// </summary>
		private ParkAndRideInputAdapter parkRideAdapter;

		private const string RES_FROMTOTITLE = "FindCarInput.labelFindCarNote";
		private const string RES_PAGETITLE = "ParkAndRideInput.labelFindCarTitle";		
		

		#region Constructor, Page Load, Init, PreRender
		/// <summary>
		/// Default Constructor
		/// </summary>
		public ParkAndRideInput()
		{
			pageId = PageId.ParkAndRideInput;
		}

		/// <summary>
		/// Page Load event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            LoadResources();

            #region Display Session Errors
            // Check if there are any errors in the InputPageState and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, TDSessionManager.Current.InputPageState.InputSessionErrors);

            // Clear the error messages
            TDSessionManager.Current.InputPageState.InputSessionErrors = null;
            #endregion

            if (Page.IsPostBack)
			{
				// The date controls don't raise events to say they have updated, so the params for these are
				// always update here
				updateOtherControls();

				// update the journey parameters with the destination location
				WriteToSession();

				CarCostingFuelValidationHelper FuelValidation = new CarCostingFuelValidationHelper();
				// Validate fuel cost entered by the user
				FuelValidation.FuelCostValidation(journeyParams, preferencesControl);
				// Validate fuel consumption eneterd by the user
				FuelValidation.FuelConsumptionValidation(journeyParams, preferencesControl);
			}
			else 
			{
				initialRequestSetup();
			}

			//reset the destiniation if previous data not park and ride (e.g. find a car)
			if (journeyParams.Destination.SearchType != SearchType.ParkAndRide)
			{
				journeyParams.DestinationLocation.ClearAll();
				journeyParams.Destination.ClearAll();
				//set destination search type to park and ride in all cases
				journeyParams.Destination.SearchType = SearchType.ParkAndRide;			
			}

			//check if we are in amend mode and if true, set the location controls to editable
            journeyOptionsControl.AmendMode = pageState.AmendMode && (journeyParams.PrivateViaLocation.Status == TDLocationStatus.Valid);
            locationsControl.FromLocationControl.AmendMode = pageState.AmendMode && (journeyParams.OriginLocation.Status == TDLocationStatus.Valid);
            locationsControl.ToLocationControl.AmendMode = pageState.AmendMode && (journeyParams.DestinationLocation.Status == TDLocationStatus.Valid);
			
			if (pageState.AmendMode)
			{
				journeyParams.DestinationType.Type = ControlType.Default;
				journeyParams.OriginType.Type = ControlType.Default;
				journeyParams.PrivateViaType.Type = ControlType.Default;
			}
	
			if (journeyParams.OutwardHour == "")
			{
				journeyParams.InitialiseDefaultOutwardTime();
				dateControl.LeaveDateControl.Populate(journeyParams.OutwardHour,
					journeyParams.OutwardMinute,
					journeyParams.OutwardDayOfMonth,
					journeyParams.OutwardMonthYear,false);
				parkRideAdapter.UpdateDateControl(dateControl);
			}


			parkRideAdapter.InitialiseControls(preferencesControl, locationsControl, journeyOptionsControl);

			if (!IsPostBack)
			{
				preferencesControl.DrivingSpeed = journeyParams.DrivingSpeed;
				preferencesControl.FindJourneyType = journeyParams.PrivateAlgorithmType;
				preferencesControl.CarSize = journeyParams.CarSize;
				preferencesControl.FuelType = journeyParams.CarFuelType;
				
				preferencesControl.FuelConsumptionUnit = journeyParams.FuelConsumptionUnit;
				preferencesControl.DoNotUseMotorways = journeyParams.DoNotUseMotorways;
				preferencesControl.FuelConsumptionValue = journeyParams.FuelConsumptionEntered;
				
				preferencesControl.FuelCostValue = journeyParams.FuelCostEntered;
				preferencesControl.FuelConsumptionOption = journeyParams.FuelConsumptionOption;
				preferencesControl.FuelCostOption = journeyParams.FuelCostOption;
			
				journeyOptionsControl.AvoidMotorways = journeyParams.AvoidMotorWays;
				journeyOptionsControl.AvoidFerries = journeyParams.AvoidFerries;
				journeyOptionsControl.AvoidTolls = journeyParams.AvoidTolls;
                journeyOptionsControl.BanLimitedAccess = journeyParams.BanUnknownLimitedAccess;
				journeyOptionsControl.AvoidRoadsList = journeyParams.AvoidRoadsList;
				journeyOptionsControl.UseRoadsList = journeyParams.UseRoadsList;
			}
		
			//Set help text for the Car preferences control (preferencesControl)
			TDResourceManager rm = Global.tdResourceManager;

			// Adding client side script for user navigation (when user hit enter, it should take the default action)
			UserExperienceEnhancementHelper.AddClientForUserNavigationDefaultAction(this.Page);

			if (TDSessionManager.Current.FindPageState.AmbiguityMode)
			{
				Helpbuttoncontrol1.HelpUrl = GetResource("FindCarInput.HelpAmbiguityUrl");
			}
			else
			{
				Helpbuttoncontrol1.HelpUrl = GetResource("ParkAndRide.UrlHelpPlanToParkAndRide");
			}

            //Added for white labelling:
            ConfigureLeftMenu("ParkAndRideInput.clientLink.BookmarkTitle", "Home.clientLink.LinkText", clientLink, expandableMenuControl, SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextParkAndRideInput);
            expandableMenuControl.AddExpandedCategory("Related links");

            commandBack.Text = GetResource("ParkAndRideInput.CommandBack.Text");

		}

		/// <summary>
		/// Performs page initialisation including event wiring.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Init(object sender, EventArgs e)
		{
			journeyOptionsControl = preferencesControl.JourneyOptionsControl;
			
			preferencesControl.JourneyTypeChanged += new EventHandler(preferencesControl_JourneyTypeChanged);
			preferencesControl.SpeedChanged += new EventHandler(preferencesControl_SpeedChanged);
			preferencesControl.CarSizeChanged += new EventHandler(preferencesControl_CarSizeChanged);
			preferencesControl.FuelTypeChanged += new EventHandler(preferencesControl_FuelTypeChanged);
			preferencesControl.ConsumptionOptionChanged += new EventHandler(preferencesControl_FuelConsumptionOptionChanged);
			preferencesControl.SpecificFuelUseUnitChanged += new EventHandler(preferencesControl_FuelUseUnitChanged);
			preferencesControl.FuelCostOptionChanged += new EventHandler(preferencesControl_FuelCostOptionChanged);
			preferencesControl.PreferencesVisibleChanged += new EventHandler(preferencesControl_PreferencesVisibleChanged);
			preferencesControl.FuelConsumptionTextChanged += new EventHandler(preferencesControl_FuelConsumptionTextChanged);
			preferencesControl.FuelCostTextChanged += new EventHandler(preferencesControl_FuelCostTextChanged);

			journeyOptionsControl.AvoidMotorwaysChanged += new EventHandler(preferencesControl_AvoidMotorwaysChanged);
			journeyOptionsControl.AvoidTollsChanged += new EventHandler(preferencesControl_AvoidTollsChanged);
            journeyOptionsControl.AvoidFerriesChanged += new EventHandler(preferencesControl_AvoidFerriesChanged);
            journeyOptionsControl.BanLimitedAccessChanged += new EventHandler(preferencesControl_BanLimitedAccessChanged);
            //preferencesControl.AvoidRoadsChanged += new EventHandler(preferencesControl_AvoidRoadsChanged);
			preferencesControl.DoNotUseMotorwayChanged += new EventHandler(preferencesControl_DoNotUseMotorwaysChanged);
			
			locationsControl.NewLocationFrom += new EventHandler(locationsControl_NewLocationFrom);
			locationsControl.NewLocationTo += new EventHandler(locationsControl_NewLocationTo);
			journeyOptionsControl.NewLocation += new EventHandler(journeyOptionsControl_NewLocation);
			preferencesControl.PageOptionsControl.Clear += new EventHandler(preferencesControl_Clear);
			preferencesControl.PageOptionsControl.Submit += new EventHandler(preferencesControl_Submit);
			preferencesControl.PreferencesOptionsControl.Submit += new EventHandler(preferencesControl_Submit);
			preferencesControl.PageOptionsControl.Back += new EventHandler(preferencesControl_Back);
            commandBack.Click += new EventHandler(preferencesControl_Back);

            // added for the top PageOptionsControl
            pageOptionsControltop.Submit += new EventHandler(preferencesControl_Submit);
            pageOptionsControltop.Back += new EventHandler(preferencesControl_Back);
            pageOptionsControltop.Clear += new EventHandler(preferencesControl_Clear);
            pageOptionsControltop.ShowAdvancedOptions += new EventHandler(pageOptionsControltop_ShowAdvancedOptions);
            pageOptionsControltop.HideAdvancedOptions += new EventHandler(pageOptionsControltop_HideAdvancedOptions);
			
			dateControl.LeaveDateControl.DateChanged +=
				new EventHandler(dateControlLeaveDateControl_DateChanged);
			dateControl.ReturnDateControl.DateChanged +=
				new EventHandler(dateControlReturnDateControl_DateChanged);

			journeyOptionsControl.MapClick +=
				new EventHandler(viaLocationControl_MapClick);

			locationsControl.FromLocationControl.TriLocationControl.MapClick += 
				new EventHandler(locationsControl_MapFromClick);
			locationsControl.ToLocationControl.TriLocationControl.MapClick += 
				new EventHandler(locationsControl_MapToClick);

			// Event Handler for default action button
			headerControl.DefaultActionEvent +=  new EventHandler(preferencesControl_Submit);			
		}

        /// <summary>
        /// White Labeling - Added event for pageOptionsControltop which raises when user hides AdvanceOptions.
        /// this will hide advance options. 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pageOptionsControltop_HideAdvancedOptions(object sender, EventArgs e)
        {
            preferencesControl.PreferencesVisible = false;
        }

        /// <summary>
        /// White Labeling - Added event for pageOptionsControltop which raises when user clicks AdvanceOptions.
        /// this will show advance options and hide pageOptionsControltop control. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pageOptionsControltop_ShowAdvancedOptions(object sender, EventArgs e)
        {
            // Showing Advance options
            preferencesControl.PreferencesVisible = true;
            // making page options control invisible
            pageOptionsControltop.AllowBack= false;
            pageOptionsControltop.AllowClear = false;
            pageOptionsControltop.AllowNext = true;
            pageOptionsControltop.AllowHideAdvancedOptions = false;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_PreRender(object sender, EventArgs e) 
		{
			// don't want to update the date when not postback...
			// otherwise double initial population
			if (Page.IsPostBack)
			{
				parkRideAdapter.UpdateDateControl(dateControl);
			}

			// if we're in amend, set the previously selected park and ride scheme
			if(pageState.AmendMode)
			{
				if(journeyParams.DestinationLocation.ParkAndRideScheme != null)
				{
					locationsControl.ToLocationControl.ParkAndRideSelection.RefreshParkAndRideSelectionControl(journeyParams.DestinationLocation);
				}
				else 
				{
					// if there is no park and ride scheme (eg. user has chosen 'amend' from results,
					// then selected 'clear' on the input page), reset the drop down list to default
					if(!pageState.AmbiguityMode)
					{
						locationsControl.ToLocationControl.ParkAndRideSelection.ParkAndRideList.SelectedIndex = -1;	
					}
				}
			}

			parkRideAdapter.UpdateErrorMessages(
				panelErrorMessage, labelErrorMessages, TDSessionManager.Current.ValidationError);
			// hide from to title in ambiguity mode!
			labelFromToTitle.Visible = !pageState.AmbiguityMode;

			preferencesControl.FuelConsumptionOption = journeyParams.FuelConsumptionOption;
			preferencesControl.FuelCostOption = journeyParams.FuelCostOption;
			preferencesControl.FuelConsumptionValid = journeyParams.FuelConsumptionValid;
			preferencesControl.FuelCostValid = journeyParams.FuelCostValid;
			preferencesControl.FuelType = journeyParams.CarFuelType;
			preferencesControl.DrivingSpeed = journeyParams.DrivingSpeed;
			preferencesControl.FindJourneyType = journeyParams.PrivateAlgorithmType;
			preferencesControl.CarSize = journeyParams.CarSize;
			preferencesControl.FuelConsumptionUnit = journeyParams.FuelConsumptionUnit;	
			
			// IR4082 - Added to remove return option for P&R journeys
			dateControl.ReturnDateControl.Visible = false;
			// end IR4082

            // White Labeling - this section sets options for pageOptionsControltop.
            bool showPreferences;

            if (preferencesControl.AmbiguityMode)
            {
                showPreferences = (preferencesControl.JourneyOptionsControl.Visible);
            }
            else
            {
                showPreferences = preferencesControl.PreferencesVisible;
            }

            pageOptionsControltop.AllowBack = preferencesControl.AmbiguityMode;
            pageOptionsControltop.AllowShowAdvancedOptions = !preferencesControl.AmbiguityMode && !preferencesControl.PreferencesVisible;


            if (showPreferences)
            {
                pageOptionsControltop.AllowHideAdvancedOptions = !preferencesControl.AmbiguityMode && !preferencesControl.PreferencesVisible;
                pageOptionsControltop.AllowClear = preferencesControl.AmbiguityMode || !preferencesControl.PreferencesVisible;

            }

            
            if (preferencesControl.AmbiguityMode)
            {
                pageOptionsControltop.Visible = true;
            }


            panelBackTop.Visible = pageState.AmbiguityMode;
            panelSubHeading.Visible = !pageState.AmbiguityMode;
            commandBack.Visible = preferencesControl.AmbiguityMode;
            pageOptionsControltop.AllowBack = false;

            if (preferencesControl.PreferencesVisible || pageState.AmbiguityMode)
            {
                BlankPanelText(TDPageInformationHtmlPlaceHolderDefinition);
            }

            SetupMap();
		}

		#endregion

		#region Private Methods
		private void LoadResources()
		{
			PageTitle =  GetResource("ParkAndRideInput.AppendPageTitle")  
				+ GetResource("JourneyPlanner.DefaultPageTitle");	
			labelFindPageTitle.Text = GetResource(RES_PAGETITLE);
			labelFromToTitle.Text = GetResource(RES_FROMTOTITLE);
            imageFindPage.ImageUrl = GetResource("HomePlanAJourney.imageParkAndRide.ImageUrl");
            imageFindPage.AlternateText = " ";
		}

		/// <summary>
		/// Updates session data with control values for those controls that do not raise events
		/// to signal that user has changed values (i.e. the date controls)
		/// </summary>
		private void updateOtherControls() 
		{
			pageState = (FindCarPageState)TDSessionManager.Current.FindPageState;
			journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;            
			inputPageState = TDSessionManager.Current.InputPageState;
			parkRideAdapter = new ParkAndRideInputAdapter(journeyParams,pageState, inputPageState);
			journeyParams.FuelConsumptionEntered = preferencesControl.FuelConsumptionValue;
			journeyParams.AvoidRoadsList = journeyOptionsControl.AvoidRoadsList;
			journeyParams.UseRoadsList = journeyOptionsControl.UseRoadsList;
			journeyParams.FuelCostEntered = preferencesControl.FuelCostValue;
			
			parkRideAdapter.UpdateJourneyDates(dateControl);

		}

		/// <summary>
		/// Performs initialisation when page is loaded for the first time. This includes delegating to
		/// session manager by calling InitialiseJourneyParametersPageStates to create a new session data
		/// if necesssary (i.e. journey parameters and page state objects). If session data contains
		/// journey results then the user is redirected to the journey summary page.
		/// </summary>
		private void initialRequestSetup() 
		{

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
            if (sessionManager.FindAMode != FindAMode.ParkAndRide)
            {
                // We have come directly from another planner so clear results from session.
                helper.ClearJourneyResultCache();
            }
            #endregion

			// Next, Initialise JourneyParameters and PageStates if needed. 
			// Will return true, if reset has been performed.
			bool resetDone;
	
			resetDone = sessionManager.InitialiseJourneyParameters(FindAMode.ParkAndRide);
			
			pageState = (FindCarPageState)TDSessionManager.Current.FindPageState;
			journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;            
			inputPageState = TDSessionManager.Current.InputPageState;
			parkRideAdapter = new ParkAndRideInputAdapter(journeyParams,pageState, inputPageState);
            
			if(resetDone) 
			{
				parkRideAdapter.InitJourneyParameters();
			}

            if (TDSessionManager.Current.GetOneUseKey(SessionKey.ParkAndRideDestination) != null)
            {
                string parkAndRideID = TDSessionManager.Current.GetOneUseKey(SessionKey.ParkAndRideDestination);
                int selectedParkAndRideID = Convert.ToInt32(parkAndRideID, TDCultureInfo.CurrentCulture.NumberFormat);

                // -1 passed to tdlocation constructor since the carparkid is not known.
                sessionManager.JourneyParameters.DestinationLocation = new TDLocation(selectedParkAndRideID, -1);
                sessionManager.JourneyParameters.Destination = new LocationSearch();
                sessionManager.JourneyParameters.Destination.SearchType = SearchType.ParkAndRide;
                sessionManager.JourneyParameters.Destination.LocationFixed = true;
                sessionManager.JourneyParameters.DestinationLocation.Status = TDLocationStatus.Valid;
                sessionManager.JourneyParameters.Origin.SearchType = SearchType.Locality;

                // refresh the park and ride control
                if (journeyParams.DestinationLocation.ParkAndRideScheme != null)
                {
                    locationsControl.ToLocationControl.ParkAndRideSelection.RefreshParkAndRideSelectionControl(journeyParams.DestinationLocation);
                }
            }
            else
            {
                // Allow a different Park and Ride location to be selected if user is amending their journey
                if((pageState.AmendMode) && (journeyParams.DestinationLocation != null) 
                    && (sessionManager.JourneyParameters.Destination.LocationFixed))
                {
                    sessionManager.JourneyParameters.Destination.LocationFixed = false;
                }
            }


			// initialise the date control from session (required as displaying help forces redirect)
			parkRideAdapter.InitDateControl(dateControl);
		}
		#endregion

		#region Event Handlers

		/// <summary>
		/// This method will save data to the journey parameters session object. This method is 
		/// needed as this functionality is not encapsulated within the ParkAndRideSelectionControl.
		/// </summary>
		private void WriteToSession()
		{															
			if(!journeyParams.Destination.LocationFixed && journeyParams.DestinationLocation.Status != TDLocationStatus.Valid)
				journeyParams.DestinationLocation = locationsControl.ToLocationControl.ParkAndRideSelection.Location;		
		}	

		/// <summary>
		/// Handler for JourneyType changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_JourneyTypeChanged(object sender, EventArgs e)
		{
			journeyParams.PrivateAlgorithmType = preferencesControl.FindJourneyType;
		}

		/// <summary>
		/// Handler for Driving speed changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_SpeedChanged(object sender, EventArgs e)
		{
			journeyParams.DrivingSpeed = preferencesControl.DrivingSpeed;
		}
		

		/// <summary>
		/// Handler for car size changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_CarSizeChanged(object sender, EventArgs e)
		{
			journeyParams.CarSize = preferencesControl.CarSize;
		}
		
		
		// <summary>
		/// Handler for fuel type changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_FuelTypeChanged(object sender, EventArgs e)
		{
			journeyParams.CarFuelType = preferencesControl.FuelType;
		}

		// <summary>
		/// Handler for consumption option changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_FuelConsumptionOptionChanged(object sender, EventArgs e)
		{
			journeyParams.FuelConsumptionOption = preferencesControl.FuelConsumptionOption;
			
		}

		// <summary>
		/// Handler for fuel use unit changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_FuelUseUnitChanged(object sender, EventArgs e)
		{
			journeyParams.FuelConsumptionUnit= preferencesControl.FuelConsumptionUnit;
		}

		// <summary>
		/// Handler for fuel cost option changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_FuelCostOptionChanged(object sender, EventArgs e)
		{
			journeyParams.FuelCostOption = preferencesControl.FuelCostOption;
		}
		private void preferencesControl_FuelConsumptionTextChanged(object sender, EventArgs e)
		{
			journeyParams.FuelConsumptionEntered = preferencesControl.FuelConsumptionValue;
			//journeyParams.PreConvertedFuelConsumption = preferencesControl.FuelConsumptionText;
		}
		private void preferencesControl_FuelCostTextChanged(object sender, EventArgs e)
		{
			journeyParams.FuelCostEntered = preferencesControl.FuelCostValue;
		}


		/// Handler for Avoid Motorways changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_AvoidMotorwaysChanged(object sender, EventArgs e)
		{
			journeyParams.AvoidMotorWays = journeyOptionsControl.AvoidMotorways;
		}

		/// Handler for DoNotUseMotorways changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_DoNotUseMotorwaysChanged(object sender, EventArgs e)
		{
			journeyParams.DoNotUseMotorways = preferencesControl.DoNotUseMotorways;
		}

        /// Handler for Avoid Tolls changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_AvoidTollsChanged(object sender, EventArgs e)
        {
            journeyParams.AvoidTolls = journeyOptionsControl.AvoidTolls;
        }


        /// Handler for Avoid Tolls changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_BanLimitedAccessChanged(object sender, EventArgs e)
        {
            journeyParams.BanUnknownLimitedAccess = journeyOptionsControl.BanLimitedAccess;
        }


        /// Handler for Avoid Ferries changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_AvoidFerriesChanged(object sender, EventArgs e)
		{
			journeyParams.AvoidFerries = journeyOptionsControl.AvoidFerries;
		}

		
		
		/// <summary>
		/// Event handler called when back button clicked in ambiguous mode. Decrements the level of
		/// hierarchical location searches (origin, destination and private via). If all locations are at highest 
		/// level then page is reverted to input mode and original input parameters reinstated.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void preferencesControl_Back(object sender, EventArgs e)
		{
			dateControl.CalendarClose();

			if (parkRideAdapter.IsAtHighestLevel()) 
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
				TDSessionManager.Current.ValidationError.Initialise();
				preferencesControl.TypeJourneyDisplayMode = GenericDisplayMode.Normal;
				preferencesControl.SpeedChangeDisplayMode = GenericDisplayMode.Normal;
				preferencesControl.CarSizeDisplayMode = GenericDisplayMode.Normal;
				preferencesControl.FuelTypeDisplayMode = GenericDisplayMode.Normal;
				preferencesControl.FuelConsumptionOptionMode = GenericDisplayMode.Normal;
				preferencesControl.FuelCostOptionMode = GenericDisplayMode.Normal;
				preferencesControl.FuelUseUnitDisplayMode = GenericDisplayMode.Normal;
				dateControl.LeaveDateControl.AmbiguityMode = false;
				dateControl.ReturnDateControl.AmbiguityMode = false;
				dateControl.LeaveDateControl.DateErrors = null;
				dateControl.ReturnDateControl.DateErrors = null;
				journeyOptionsControl.AvoidRoadDisplayMode = GenericDisplayMode.Normal;
				journeyOptionsControl.UseRoadDisplayMode = GenericDisplayMode.Normal;
				preferencesControl.FuelConsumptionOption = journeyParams.FuelConsumptionOption;
				preferencesControl.FuelCostOption = journeyParams.FuelCostOption;
				preferencesControl.FuelConsumptionValue = journeyParams.FuelConsumptionEntered;
				preferencesControl.FuelCostValue = journeyParams.FuelCostEntered;
				preferencesControl.FuelConsumptionUnit = journeyParams.FuelConsumptionUnit;

				parkRideAdapter.InitLocationsControl(locationsControl);
				parkRideAdapter.InitViaLocationsControl(journeyOptionsControl);

				if (locationsControl.FromLocationControl.TheLocation.Status != TDLocationStatus.Valid) 
				{
					locationsControl.FromLocationControl.SetLocationUnspecified();
				}
				if (locationsControl.ToLocationControl.TheLocation.Status != TDLocationStatus.Valid) 
				{
					locationsControl.ToLocationControl.SetLocationUnspecified();
				}
				if (journeyOptionsControl.TheLocation.Status != TDLocationStatus.Valid)
				{
					journeyOptionsControl.SetLocationUnspecified();
				}
				
		
			} 
			else
			{
				// Decrement the drilldown level of any ambiguous locations
				// that are not yet at their highest level
				journeyParams.Origin.DecrementLevel();
				journeyParams.Destination.DecrementLevel();
				journeyParams.PrivateVia.DecrementLevel();
			}

			//IR2222 - ensure the DivHider panel on the carpreferencescontrol is made visible
			preferencesControl.PanelDivHider.Visible=true;

			//reset the destiniation if previous data not park and ride (e.g. find a car)
			if (journeyParams.Destination.SearchType != SearchType.ParkAndRide)
			{
				journeyParams.DestinationLocation.ClearAll();
				journeyParams.Destination.ClearAll();
				//set destination search type to park and ride in all cases
				journeyParams.Destination.SearchType = SearchType.ParkAndRide;			
			}

            if (preferencesControl.PreferencesVisible)
            {
                pageOptionsControltop.AllowNext = true;
                pageOptionsControltop.AllowBack = false;
                pageOptionsControltop.AllowClear = false;
                pageOptionsControltop.AllowHideAdvancedOptions = false;
            }
            else
            {
                pageOptionsControltop.AllowBack = false;
                pageOptionsControltop.AllowClear = true;
                pageOptionsControltop.AllowNext = true;
                pageOptionsControltop.AllowHideAdvancedOptions = false;
            }
		}

		/// <summary>
		/// Event handler called when visibility of preferences changed. Updates page state with new value.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void preferencesControl_PreferencesVisibleChanged(object sender, EventArgs e) 
		{
			pageState.TravelDetailsVisible = preferencesControl.PreferencesVisible;
            if (!preferencesControl.PreferencesVisible)
                pageOptionsControltop.Visible = true;
		}

		/// <summary>
		/// Event handler called when next button clicked. The journey plan runner component validates the 
		/// current journey parameters. If invalid then the page is put into ambiguity mode otherwise 
		/// journey planning commences. User preferences are saved before commencing journey planning.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void preferencesControl_Submit(object sender, EventArgs e)
		{
			dateControl.CalendarClose();

			if (preferencesControl.SavePreferences)
			{
				parkRideAdapter.SaveTravelDetails();
			}

			if (!pageState.AmbiguityMode) 
			{
				pageState.SaveJourneyParameters(journeyParams);
				parkRideAdapter.AmbiguitySearch(locationsControl, journeyOptionsControl);
			} 
			else 
			{
				locationsControl.FromLocationControl.Search();
				locationsControl.ToLocationControl.Search();
				journeyOptionsControl.Search();
			}

			// Set up the JourneyPlanControlData
			parkRideAdapter.InitialiseAsyncCallState();

			// Validate the JourneyParameters
			JourneyPlanRunner.JourneyPlanRunner runner = new JourneyPlanRunner.JourneyPlanRunner(Global.tdResourceManager);

			pageState.AmbiguityMode = !runner.ValidateAndRun(
				TDSessionManager.Current, 
				TDSessionManager.Current.JourneyParameters, 
				GetChannelLanguage(TDPage.SessionChannelName),
				true);

			if (pageState.AmbiguityMode) 
			{
				preferencesControl.TypeJourneyDisplayMode = GenericDisplayMode.ReadOnly;
				preferencesControl.SpeedChangeDisplayMode = GenericDisplayMode.ReadOnly;
				dateControl.LeaveDateControl.DateErrors = TDSessionManager.Current.ValidationError;
				dateControl.ReturnDateControl.DateErrors = TDSessionManager.Current.ValidationError;
				dateControl.LeaveDateControl.AmbiguityMode = true;
				dateControl.ReturnDateControl.AmbiguityMode = true;
				preferencesControl.CarSizeDisplayMode = GenericDisplayMode.ReadOnly;
				preferencesControl.FuelTypeDisplayMode = GenericDisplayMode.ReadOnly;
				preferencesControl.FuelUseUnitDisplayMode = GenericDisplayMode.ReadOnly;
				preferencesControl.FuelConsumptionOptionMode = GenericDisplayMode.ReadOnly;
				preferencesControl.FuelCostOptionMode = GenericDisplayMode.ReadOnly;
				journeyOptionsControl.AvoidRoadDisplayMode = GenericDisplayMode.ReadOnly;
				journeyOptionsControl.UseRoadDisplayMode = GenericDisplayMode.ReadOnly;
			}
			else 
			{
				// Extending journeys using a Find A is not currently possible so clear down the itinerary
				TDItineraryManager itineraryManager = TDItineraryManager.Current;
				if (itineraryManager.Length >= 1 ) 
				{
					itineraryManager.ResetItinerary();
				}

				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindAInputOk;

			}

			// IR2468 - adding code to refresh the controls otherwise, the amendMode
			// change is not taken into account until next postback
			if (pageState.AmendMode)
			{
				//IR1417 -  Disable AmendMode when leaving the page
				pageState.AmendMode = false;

				// Refresh location control after amend mode changed to make sure the location
				// is up to date 
				journeyOptionsControl.AmendMode = pageState.AmendMode;
				locationsControl.FromLocationControl.AmendMode = pageState.AmendMode;	
				locationsControl.ToLocationControl.AmendMode = pageState.AmendMode;

				locationsControl.FromLocationControl.RefreshTristateControl(false);
				locationsControl.ToLocationControl.RefreshTristateControl(false);
				journeyOptionsControl.TristateLocationControl.ForceRefresh();
			}

            
				
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
			journeyParams.Destination.SearchType = SearchType.ParkAndRide;
			journeyParams.DestinationType = locationsControl.ToLocationControl.LocationControlType;
		}

        /// <summary>
        /// Sets up a map for the location search and type of map location mode
        /// If map location mode is via map gets initialised with start, via and end mode.
        /// By default map gets initialised with start and end mode.
        /// </summary>
        private void SetupMap()
        {
            MapLocationPoint[] locationsToShow = GetMapLocationPoints();
            SearchType searchType = SearchType.Map;

            LocationSearch locationSearch = inputPageState.MapLocationSearch;
            TDLocation location = inputPageState.MapLocation;

            if (locationSearch != null)
            {
                searchType = locationSearch.SearchType;
            }

            if (location != null)
            {
                if (location.GridReference.IsValid)
                {
                    mapInputControl.MapCenter = location.GridReference;
                }
            }

            if (preferencesControl.PreferencesVisible)
            {
                MapLocationMode[] mapModes = new MapLocationMode[2] { MapLocationMode.Start, MapLocationMode.Via };
                mapInputControl.Initialise(searchType, locationsToShow, mapModes);
            }
            else
            {
                MapLocationMode[] mapModes = new MapLocationMode[1] { MapLocationMode.Start };
                mapInputControl.Initialise(searchType, locationsToShow, mapModes);
            }

            if (!mapInputControl.Visible && TDSessionManager.Current.GetOneUseKey(SessionKey.JourneyPlannerInputToMap) != null)
            {
                if (!pageState.AmbiguityMode)
                {
                    mapInputControl.Visible = true;
                }
            }


        }

        /// <summary>
        /// Returns the location points to show on map
        /// </summary>
        /// <returns></returns>
        private MapLocationPoint[] GetMapLocationPoints()
        {
            MapHelper mapHelper = new MapHelper();

            List<MapLocationPoint> mapLocationPoints = new List<MapLocationPoint>();

            MapLocationPoint origin = mapHelper.GetMapLocationPoint(journeyParams.OriginLocation, MapLocationSymbolType.Start, true, false);

            MapLocationPoint destination = mapHelper.GetMapLocationPoint(journeyParams.DestinationLocation, MapLocationSymbolType.End, true, false);

            MapLocationPoint publicVia = mapHelper.GetMapLocationPoint(journeyParams.PublicViaLocation, MapLocationSymbolType.Via, true, false);

            MapLocationPoint privateVia = mapHelper.GetMapLocationPoint(journeyParams.PrivateViaLocation, MapLocationSymbolType.Via, true, false);

            if (origin.MapLocationOSGR.IsValid)
            {
                mapLocationPoints.Add(origin);
            }

            if (destination.MapLocationOSGR.IsValid)
            {
                mapLocationPoints.Add(destination);
            }

            if (publicVia.MapLocationOSGR.IsValid)
            {
                mapLocationPoints.Add(publicVia);
            }

            if (privateVia.MapLocationOSGR.IsValid)
            {
                mapLocationPoints.Add(privateVia);
            }

            return mapLocationPoints.ToArray();
        }


		private void locationsControl_MapFromClick(object sender, EventArgs e)
		{
            bool shiftForm = false;
			inputPageState.MapType = CurrentLocationType.From;
			inputPageState.MapMode = CurrentMapMode.FromFindAInput;
			
			if (journeyParams.OriginLocation.Status == TDLocationStatus.Unspecified )
			{
				parkRideAdapter.MapSearch(journeyParams.Origin.InputText, journeyParams.Origin.SearchType, journeyParams.Origin.FuzzySearch);
                if (inputPageState.MapLocationSearch.InputText.Length == 0)
                {
                    inputPageState.MapLocationControlType.Type = ControlType.Default;
                }
                else if (inputPageState.MapLocation.Status == TDLocationStatus.Valid)
                {
                    journeyParams.Origin = inputPageState.MapLocationSearch;
                    journeyParams.OriginLocation = inputPageState.MapLocation;
                    locationsControl.FromLocationControl.TheLocation = inputPageState.MapLocation;
                    locationsControl.FromLocationControl.RefreshTristateControl(false);
                    locationsControl.FromLocationControl.AmendMode = false;
                }
                else
                {
                    inputPageState.MapLocationControlType.Type = ControlType.NoMatch;
                    shiftForm = true;
                }
			} 
			else 
			{
				inputPageState.MapLocationSearch = journeyParams.Origin;
				inputPageState.MapLocation = journeyParams.OriginLocation;
			}

            if (shiftForm)
            {
                inputPageState.JourneyInputReturnStack.Push(pageId);
                //Set this property to tell the target page we are coming in from the page when findonmap button clicked			
                TDSessionManager.Current.SetOneUseKey(SessionKey.JourneyPlannerInputToMap, string.Empty);
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputToMap;
            }
            else
            {
                mapInputControl.Visible = true;
            }
		}

		private void locationsControl_MapToClick(object sender, EventArgs e)
		{
            bool shiftForm = false;
			inputPageState.MapType = CurrentLocationType.To;
			inputPageState.MapMode = CurrentMapMode.FromFindAInput;
			
			if (journeyParams.DestinationLocation.Status == TDLocationStatus.Unspecified )
			{
				parkRideAdapter.MapSearch(journeyParams.Destination.InputText, journeyParams.Destination.SearchType, journeyParams.Destination.FuzzySearch);
                if (inputPageState.MapLocationSearch.InputText.Length == 0)
                {
                    inputPageState.MapLocationControlType.Type = ControlType.Default;
                }
                else if (inputPageState.MapLocation.Status == TDLocationStatus.Valid)
                {
                    journeyParams.Destination = inputPageState.MapLocationSearch;
                    journeyParams.DestinationLocation = inputPageState.MapLocation;
                    locationsControl.ToLocationControl.TheLocation = inputPageState.MapLocation;
                    locationsControl.ToLocationControl.RefreshTristateControl(false);
                    locationsControl.ToLocationControl.AmendMode = false;
                }
                else
                {
                    shiftForm = true;
                    inputPageState.MapLocationControlType.Type = ControlType.NoMatch;
                }
			} 
			else 
			{
				inputPageState.MapLocationSearch = journeyParams.Destination;
				inputPageState.MapLocation = journeyParams.DestinationLocation;
			}

            if (shiftForm)
            {
                inputPageState.JourneyInputReturnStack.Push(pageId);
                //Set this property to tell the target page we are coming in from the page when findonmap button clicked			
                TDSessionManager.Current.SetOneUseKey(SessionKey.JourneyPlannerInputToMap, string.Empty);
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputToMap;
            }
            else
            {
                mapInputControl.Visible = true;
            }
		}

		/// <summary>
		/// Event handler called when new location button is clicked for the "via" location.
		/// The journey parameters are updated with the "via" location control's new values.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void journeyOptionsControl_NewLocation(object sender, EventArgs e) 
		{
			journeyParams.PrivateVia = journeyOptionsControl.TheSearch;
			journeyParams.PrivateViaLocation = journeyOptionsControl.TheLocation;
			journeyParams.PrivateVia.SearchType = SearchType.Locality;
			journeyParams.PrivateViaType = journeyOptionsControl.LocationControlType;
		}

		/// <summary>
		/// Event handler called when clear page button is clicked. Journey parameters are reset
		/// to initial values, page controls updated and the page set to input mode.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void preferencesControl_Clear(object sender, EventArgs e) 
		{
			parkRideAdapter.InitJourneyParameters();
			locationsControl.ToLocationControl.ParkAndRideSelection.RefreshParkAndRideSelectionControl(journeyParams.DestinationLocation);

			journeyParams.Destination.SearchType = SearchType.ParkAndRide;	

			pageState.AmbiguityMode = false;
			parkRideAdapter.InitialiseControls(preferencesControl, locationsControl, journeyOptionsControl);
			dateControl.LeaveDateControl.DateErrors = null;
			dateControl.LeaveDateControl.AmbiguityMode = false;
			dateControl.ReturnDateControl.DateErrors = null;
			dateControl.ReturnDateControl.AmbiguityMode = false;

            journeyParams.InitialiseDefaultOutwardTime();
            dateControl.LeaveDateControl.Populate(journeyParams.OutwardHour,
                journeyParams.OutwardMinute,
                journeyParams.OutwardDayOfMonth,
                journeyParams.OutwardMonthYear, false);
            parkRideAdapter.UpdateDateControl(dateControl);

			TDSessionManager.Current.ValidationError = null;
			TDPage.CloseAllSingleWindows(Page);
			
			preferencesControl.PanelDivHider.Visible=true;
			preferencesControl.InputConsumptionText = null;
			preferencesControl.InputCostText = null;
			clearRoadEntered();			
			preferencesControl.CarSizeDisplayMode = GenericDisplayMode.Normal;
			preferencesControl.FuelTypeDisplayMode = GenericDisplayMode.Normal;
			preferencesControl.FuelUseUnitDisplayMode = GenericDisplayMode.Normal;
			preferencesControl.FuelConsumptionOptionMode = GenericDisplayMode.Normal;
			preferencesControl.FuelCostOptionMode = GenericDisplayMode.Normal;			
			preferencesControl.DrivingSpeed = journeyParams.DrivingSpeed;
			preferencesControl.FindJourneyType = journeyParams.PrivateAlgorithmType;
			preferencesControl.CarSize = journeyParams.CarSize;
			preferencesControl.FuelType = journeyParams.CarFuelType;
			preferencesControl.FuelConsumptionOption = journeyParams.FuelConsumptionOption;
			preferencesControl.FuelCostOption = journeyParams.FuelCostOption;
			preferencesControl.FuelConsumptionUnit = journeyParams.FuelConsumptionUnit;
			preferencesControl.DoNotUseMotorways = journeyParams.DoNotUseMotorways;
			preferencesControl.FuelConsumptionValue = journeyParams.FuelConsumptionEntered;
			preferencesControl.FuelCostValue = journeyParams.FuelCostEntered;
			
			journeyOptionsControl.AvoidMotorways = journeyParams.AvoidMotorWays;
			journeyOptionsControl.AvoidFerries = journeyParams.AvoidFerries;
            journeyOptionsControl.AvoidTolls = journeyParams.AvoidTolls;
            journeyOptionsControl.BanLimitedAccess = journeyParams.BanUnknownLimitedAccess;
			journeyOptionsControl.AvoidRoadsList = journeyParams.AvoidRoadsList;
			journeyOptionsControl.UseRoadsList = journeyParams.UseRoadsList;

            // Reset the Map locations to show on the map (use Origin as it should have been reset by now)
            inputPageState.MapLocation = journeyParams.OriginLocation;
            inputPageState.MapLocationSearch = journeyParams.Origin;
		
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

		private void viaLocationControl_MapClick(object sender, EventArgs e)
		{
            bool shiftForm = false;

			inputPageState.MapType = CurrentLocationType.PrivateVia;
			inputPageState.MapMode = CurrentMapMode.FromFindAInput;
			
			if (journeyParams.PrivateViaLocation.Status == TDLocationStatus.Unspecified )
			{
				parkRideAdapter.MapSearch(journeyParams.PrivateVia.InputText, journeyParams.PrivateVia.SearchType, journeyParams.PrivateVia.FuzzySearch);
                if (inputPageState.MapLocationSearch.InputText.Length == 0)
                {
                    inputPageState.MapLocationControlType.Type = ControlType.Default;
                }
                else if (inputPageState.MapLocation.Status == TDLocationStatus.Valid)
                {
                    journeyParams.PrivateVia = inputPageState.MapLocationSearch;
                    journeyParams.PrivateViaLocation = inputPageState.MapLocation;
                    journeyOptionsControl.TristateLocationControl.Search(true);
                    journeyOptionsControl.TristateLocationControl.AmendMode = false;
                }
                else
                {
                    inputPageState.MapLocationControlType.Type = ControlType.NoMatch;
                    shiftForm = true;
                }
			} 
			else 
			{
				inputPageState.MapLocationSearch = journeyParams.PrivateVia;
				inputPageState.MapLocation = journeyParams.PrivateViaLocation;
			}

            if (shiftForm)
            {
                inputPageState.JourneyInputReturnStack.Push(pageId);
                //Set this property to tell the target page we are coming in from the page when findonmap button clicked			
                TDSessionManager.Current.SetOneUseKey(SessionKey.JourneyPlannerInputToMap, string.Empty);
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputToMap;
            }
            else
            {
                mapInputControl.Visible = true;
            }
		}

		private void clearRoadEntered()
		{
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl1.RoadUnspecified.RoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl1.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl1.RoadValid.Visible = false;
			preferencesControl.JourneyOptionsControl.LabelRoadAlert.Visible = false;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl1.RoadAmbiguous.Visible = false;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl1.RoadUnspecified.Visible = true;

			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl2.RoadUnspecified.RoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl2.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl2.RoadValid.Visible = false;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl2.RoadAmbiguous.Visible = false;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl2.RoadUnspecified.Visible = true;

			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl3.RoadUnspecified.RoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl3.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl3.RoadValid.Visible = false;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl3.RoadAmbiguous.Visible = false;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl3.RoadUnspecified.Visible = true;

			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl4.RoadUnspecified.RoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl4.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl4.RoadValid.Visible = false;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl4.RoadAmbiguous.Visible = false;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl4.RoadUnspecified.Visible = true;

			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl5.RoadUnspecified.RoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl5.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl5.RoadValid.Visible = false;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl5.RoadAmbiguous.Visible = false;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl5.RoadUnspecified.Visible = true;

			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl6.RoadUnspecified.RoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl6.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl6.RoadValid.Visible = false;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl6.RoadAmbiguous.Visible = false;
			preferencesControl.JourneyOptionsControl.UseAvoidSelectControl6.RoadUnspecified.Visible = true;

			preferencesControl.JourneyOptionsControl.UseSelectControl1.RoadUnspecified.RoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseSelectControl1.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseSelectControl1.RoadValid.Visible = false;
			preferencesControl.JourneyOptionsControl.UseSelectControl1.RoadAmbiguous.Visible = false;
			preferencesControl.JourneyOptionsControl.UseSelectControl1.RoadUnspecified.Visible = true;

			preferencesControl.JourneyOptionsControl.UseSelectControl2.RoadUnspecified.RoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseSelectControl2.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseSelectControl2.RoadValid.Visible = false;
			preferencesControl.JourneyOptionsControl.UseSelectControl2.RoadAmbiguous.Visible = false;
			preferencesControl.JourneyOptionsControl.UseSelectControl2.RoadUnspecified.Visible = true;

			preferencesControl.JourneyOptionsControl.UseSelectControl3.RoadUnspecified.RoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseSelectControl3.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseSelectControl3.RoadValid.Visible = false;
			preferencesControl.JourneyOptionsControl.UseSelectControl3.RoadAmbiguous.Visible = false;
			preferencesControl.JourneyOptionsControl.UseSelectControl3.RoadUnspecified.Visible = true;

			preferencesControl.JourneyOptionsControl.UseSelectControl4.RoadUnspecified.RoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseSelectControl4.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseSelectControl4.RoadValid.Visible = false;
			preferencesControl.JourneyOptionsControl.UseSelectControl4.RoadAmbiguous.Visible = false;
			preferencesControl.JourneyOptionsControl.UseSelectControl4.RoadUnspecified.Visible = true;

			preferencesControl.JourneyOptionsControl.UseSelectControl5.RoadUnspecified.RoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseSelectControl5.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseSelectControl5.RoadValid.Visible = false;
			preferencesControl.JourneyOptionsControl.UseSelectControl5.RoadAmbiguous.Visible = false;
			preferencesControl.JourneyOptionsControl.UseSelectControl5.RoadUnspecified.Visible = true;

			preferencesControl.JourneyOptionsControl.UseSelectControl6.RoadUnspecified.RoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseSelectControl6.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			preferencesControl.JourneyOptionsControl.UseSelectControl6.RoadValid.Visible = false;
			preferencesControl.JourneyOptionsControl.UseSelectControl6.RoadAmbiguous.Visible = false;
			preferencesControl.JourneyOptionsControl.UseSelectControl6.RoadUnspecified.Visible = true;
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

		}
		#endregion
	}
}
