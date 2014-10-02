// ***************************************************************
// NAME                 : FindTrainCostInputInput.aspx
// AUTHOR               : Tim Mollart
// DATE CREATED         : 05/10/2006
// DESCRIPTION			: Input page for cost based train planning
// ****************************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindTrainCostInput.aspx.cs-arc  $
//
//   Rev 1.17   Aug 01 2011 14:52:42   apatel
//Code update to stop FindTrainCostInput page and FindCarParkInput page from breaking in specific navigation flow
//Resolution for 5717: Find Cheaper Rail Fare page breaks in certain navigation flow
//
//   Rev 1.16   Jul 28 2011 16:20:10   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.15   Jul 29 2010 16:13:26   mmodi
//Changes to page layout and styles to be exactly consistent for all input pages in the Portal
//Resolution for 4760: IE7-find a car route check boxes
//
//   Rev 1.14   Jul 06 2010 15:46:54   apatel
//resolve the issue with switching between cost and time rail planners after amend
//Resolution for 5566: Issue with Switching between rail planner and rail cost planner
//
//   Rev 1.13   Jun 03 2010 09:35:00   mmodi
//Prevent double attach to the Init method - stops duplicate fares call being triggered on a submit
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.12   May 13 2010 13:05:24   mmodi
//Added code call to clear the printable map session information 
//Resolution for 5535: Printable maps session logic improvement
//
//   Rev 1.11   Mar 26 2010 12:01:28   MTurner
//Added code to clear journey results from session if you are coming directly from another planner.
//
//Resolution for 5481: Session issue when going from FAT to D2D using the left hand menu
//
//   Rev 1.10   Jan 29 2010 14:45:28   mmodi
//Updated to reset CycleResult to correct error when planning journey after coming directly from Cycle journey details
//Resolution for 5388: Cycle Planner - Server error when planning Door to door after a cycle journey
//
//   Rev 1.9   Feb 02 2009 17:40:36   mmodi
//Populate Routing Guide properties
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.8   Jan 30 2009 11:03:58   apatel
//Search Engine Optimisation changes - CCN624
//Resolution for 5229: Search Engin Optimisation Changes -  CCN624
//
//   Rev 1.7   May 22 2008 16:43:54   mmodi
//Updated to make locations editable
//Resolution for 4998: "Find Nearest" functionality on Find Train / Find Coach input not working
//Resolution for 5000: Find Train - when ambiguity page is diplayed, resolved locations still editable
//Resolution for 5002: Amend Find a train cost does not use the new location
//
//   Rev 1.6   May 19 2008 15:49:04   mmodi
//Make locations editable when in amend mode
//Resolution for 4988: Del 10.1: Text displayed above location input box after amend journey
//
//   Rev 1.5   May 01 2008 17:23:44   mmodi
//Updated to display session timeout error
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.4   Apr 08 2008 15:14:58   scraddock
//Advanced Text Not Hidden
//Resolution for 4847: Advanced Text Not Hidden
//
// rev devfactory 8 apr sbarker
// Advanced text hidden when advanced pressed, or in ambiguity mode
//
//   Rev 1.3   Apr 01 2008 14:43:20   apatel
//Moved back button from blue box to top of the screen
//
//   Rev 1.2   Mar 31 2008 13:24:38   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 25 2008 17:00:00   mmodi
//Updated to display icon and changed the title
//
//   Rev 1.0   Nov 08 2007 13:29:40   mturner
//Initial revision.
//
//   Rev 1.8   Jun 07 2007 11:24:44   mmodi
//Correct Advanced options visibility issue when Clear selected
//Resolution for 4251: RSbP: Advanced options panel hidden on Clear page
//
//   Rev 1.7   Nov 17 2006 11:02:00   dsawe
//changed for Dates are lost when Advanced Options selected 
//Resolution for 4220: Rail Search by Price
//Resolution for 4257: RSbP: Dates are lost when Advanced Options selected
//
//   Rev 1.6   Nov 16 2006 16:27:50   tmollart
//Modified preferences saving code so that the correct "Save these details" check box is interegated.
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.5   Nov 16 2006 11:34:14   dsawe
//to prevent advanced options panel hiding commented pagestate.initialise
//Resolution for 4220: Rail Search by Price
//Resolution for 4251: RSbP: Advanced options panel hidden on Clear page
//
//   Rev 1.4   Nov 14 2006 14:56:06   tmollart
//Modified CMS Placeholder definitions.
//
//   Rev 1.3   Nov 14 2006 10:35:04   dsawe
//changed for relatedLinksControl 
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.2   Nov 12 2006 14:00:18   dsawe
//added code to set TravelDetailsVisible property to false
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.1   Nov 09 2006 17:38:04   tmollart
//Work in progress.
//
//   Rev 1.0   Nov 07 2006 11:35:24   tmollart
//Initial revision.

using System;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using TransportDirect.Common;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.CostSearchRunner;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.Common.PropertyService.Properties;


namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Input page for cost based journey searches.
	/// </summary>
	public partial class FindTrainCostInput : TDPage, ILanguageHandlerIndependent
	{

		#region Declarations

		// Page System Objects
		
		// Page Controls
		protected TransportDirect.UserPortal.Web.Controls.ClientLinkControl clientLink;
		protected TransportDirect.UserPortal.Web.Controls.SearchTypeControl searchTypeControl;
		protected TransportDirect.UserPortal.Web.Controls.FindLeaveReturnDatesControl dateControl;
		protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl relatedLinksControl;
		protected TransportDirect.UserPortal.Web.Controls.FindToFromLocationsControl locationsControl;
		protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl expandableMenuControl;
		protected TransportDirect.UserPortal.Web.Controls.FindFarePreferenceControl preferencesControl;

		// MCMS Controls

		/// <summary>
		/// Header control.
		/// </summary>
		protected HeaderControl headerControl;

		/// <summary>
		/// Session manager reference.
		/// </summary>
		private ITDSessionManager sm;
		
		/// <summary>
		/// Cost Search Parameters
		/// </summary>
		private CostSearchParams searchParams;
		
		/// <summary>
		/// Is cost based searching available.
		/// </summary>
		private bool costBasedSearchAvailable;

		/// <summary>
		/// Cost Based Page State
		/// </summary>
		private FindCostBasedPageState pageState;
		
		/// <summary>
		/// Find Fare Helper
		/// </summary>
		private FindFareInputAdapter findFareInputAdapter;

		//Constants
		public const string FIND_A_FARE_RM = "FindAFare";
		private const string leaveFlexibilitySRLabel = "FindFare.FindFareInput.LeaveDateControl.FlexibilitySRLabel";
		private const string returnFlexibilitySRLabel = "FindFare.FindFareInput.ReturnDateControl.FlexibilitySRLabel";

		// Rail Search by Price variables
		private string originLocationText = string.Empty;
		private string destLocationText = string.Empty;
		private string outwardDayOfMonth = string.Empty;
		private string outwardMonthYear = string.Empty;
		private string returnDayOfMonth = string.Empty;
		private string returnMonthYear = string.Empty;

		#endregion


		#region Constructor
		/// <summary>
		/// Constructor.
		/// Sets page ID to FindFareInput.
		/// </summary>
		public FindTrainCostInput()
		{
			this.pageId = PageId.FindTrainCostInput;
		}

		#endregion


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			//this.Init += new System.EventHandler(this.Page_Init);
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}

		/// <summary>
		/// Page Initialisation.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Init(object sender, System.EventArgs e)
		{
			// Event Handler for default action button
			headerControl.DefaultActionEvent +=  new EventHandler(preferencesControl_Submit);

			// Search type control event handling
			this.searchTypeControl.SearchTypeChanged += new System.EventHandler(this.SearchTypeControl_SearchTypeChanged);

			// Date selection control event handling
			this.dateControl.LeaveDateControl.DateChanged += new System.EventHandler(this.DateSelectControl_DateChanged);
			this.dateControl.ReturnDateControl.DateChanged += new System.EventHandler(this.DateSelectControl_DateChanged);

			// Preference control event handling (for control selection changes).
			this.preferencesControl.DiscountRailCardChanged += new System.EventHandler(this.preferenceControl_DiscountRailCardChanged);
			this.preferencesControl.AdultChildChanged += new System.EventHandler(this.preferenceControl_AdultChildChanged);

			// Subscribe to events published by the FindFarePreferencesControl
			this.preferencesControl.PreferencesPageOptionsControl.Submit += new System.EventHandler(this.preferencesControl_Submit);
			this.preferencesControl.PreferencesPageOptionsControl.Clear += new System.EventHandler(this.preferencesControl_Clear);
			this.preferencesControl.PreferencesVisibleChanged += new System.EventHandler(this.preferencesControl_PreferencesVisibleChanged);

			// Back button event handlers
			this.preferencesControl.PreferencesPageOptionsControl.Back += new System.EventHandler(this.preferenceControl_BackButtonClicked);
           
            // added for the top PageOptionsControl
            pageOptionsControltop.Submit += new System.EventHandler(this.preferencesControl_Submit);
            
            pageOptionsControltop.Clear += new EventHandler(this.preferencesControl_Clear);
            pageOptionsControltop.ShowAdvancedOptions += new EventHandler(pageOptionsControltop_ShowAdvancedOptions);
            
            preferencesControl.PreferencesPageOptionsControl.HideAdvancedOptions += new EventHandler(pageOptionsControltop_HideAdvancedOptions);

            // Location control event handlers
			this.locationsControl.NewLocationFrom += new System.EventHandler(this.LocationsControl_NewLocationFrom);
			this.locationsControl.NewLocationTo += new System.EventHandler(this.LocationsControl_NewLocationTo);

            commandBack.Click += new EventHandler(this.preferenceControl_BackButtonClicked);
		}

		#endregion


		#region Private Properties

		/// <summary>
		/// Returns true if the sessions journey parameters search type for the origin or 
		/// dest location is Find Nearest.
		/// </summary>
		private bool IsFindNearest
		{
			get 
			{				
				return ((sm.JourneyParameters.OriginLocation.SearchType == SearchType.FindNearest) ||
					(sm.JourneyParameters.DestinationLocation.SearchType == SearchType.FindNearest));
			}
		}

		#endregion

	
		#region Event Handlers

		/// <summary>
		/// Page Load. Initialises page for input.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Assign local session manager reference
			sm = TDSessionManager.Current;

            TDJourneyParametersMulti timeParams = null;

			// Check if rail search by price functionality is available
			costBasedSearchAvailable = bool.Parse(Properties.Current["FindAFareAvailable.Rail"]);

			// Check if cost based planning is turned off. If so redirect the user to the
			// FindTrainInput page.
			if (!costBasedSearchAvailable)
			{
				sm.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindTrainInputDefault;
			}

			// Rail Search by Price functionality.
			// Checks if the user has been transferred from the FindTrainInput page
			// and if so will replace the users input parameters on this page.
			if (sm.GetOneUseKey(SessionKey.TransferredFromTimeBasedPlanning) != null )
			{
				timeParams = (TDJourneyParametersMulti)sm.JourneyParameters;

				// Locations
				originLocationText = timeParams.Origin.InputText;
				destLocationText = timeParams.Destination.InputText;

				// Dates
				outwardDayOfMonth = timeParams.OutwardDayOfMonth;
				outwardMonthYear = timeParams.OutwardMonthYear;
				returnDayOfMonth = timeParams.ReturnDayOfMonth;
				returnMonthYear = timeParams.ReturnMonthYear;
			}

            #region Display Session Errors
            // Check if there are any errors in the InputPageState and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, sm.InputPageState.InputSessionErrors);

            // Clear the error messages
            sm.InputPageState.InputSessionErrors = null;
            #endregion

            // Initial page set up.
			if (!Page.IsPostBack)
			{
				InitialPageSetup(false);
			}

			// Set up object references
			if (sm.JourneyParameters is CostSearchParams)
            {
                searchParams = (CostSearchParams)sm.JourneyParameters;
            }
            else 
            {
                // Possible user click the right hand link and the JourneyParameters are 
                // of type other than CostSearchParams or the searchParams are null.
                // Force page to reset only if its the first time and not postback.
                if (!IsPostBack)
                {
                    InitialPageSetup(true);
                }
            }

            pageState = (FindCostBasedPageState)sm.FindPageState;
            

			// Help class set up
			findFareInputAdapter = new FindFareInputAdapter(pageState, sm);

			// Rail Search by Price specific page set up.
			findFareInputAdapter.SetupDateControl(dateControl);
			InitialiseLocationControls();
			preferencesControl.OverrideCoachOptionsToInvisible = true;
			locationsControl.FromLocationControl.SpaceTableVisible = true;
			locationsControl.ToLocationControl.SpaceTableVisible = true;

            // Set the amend value of the locations
            SetLocationsAmendable();
			
			// Page Titles
			PageTitle = GetResource("FindTrainCostInput.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");
            labelFindPageTitle.Text = GetResource("FindTrainCostInput.labelFindTrainCostTitle");
			labelFromToTitle.Text = GetResource("FindTrainInput.labelFromToTitle");
            imageFindATrainCost.ImageUrl = GetResource("HomeDefault.imageFindTrainCost.Trimmed.ImageUrl");
            imageFindATrainCost.AlternateText = " ";
            commandBack.Text = GetResource("FindTrainInput.CommandBack.Text");

			// Help controls
			if (pageState.AmbiguityMode)
			{
				Helpbuttoncontrol1.HelpUrl = GetResource("FindTrainInput.HelpAmbiguityUrl");
			}
			else
			{
				Helpbuttoncontrol1.HelpUrl = GetResource("FindTrainInput.HelpPageUrl");
			}

			// Rail Search by Price Functionality.
			// If page not post back then load text into location controls.
			// Note: It may not be populated but this doenst matter.
			if (!Page.IsPostBack && sm.GetOneUseKey(SessionKey.TransferredFromTimeBasedPlanning) != null )
			{
				locationsControl.OriginSearch.InputText = originLocationText;
				locationsControl.DestinationSearch.InputText = destLocationText;

                if (timeParams.OriginLocation.Status == TDLocationStatus.Valid)
                {
                    searchParams.OriginLocation = locationsControl.OriginLocation = timeParams.OriginLocation;
                    searchParams.Origin = locationsControl.OriginSearch = timeParams.Origin;
                    locationsControl.FromLocationControl.AmendMode = true;
                }
                if (timeParams.DestinationLocation.Status == TDLocationStatus.Valid)
                {
                    searchParams.DestinationLocation = locationsControl.DestinationLocation = timeParams.DestinationLocation;
                    searchParams.Destination = locationsControl.DestinationSearch = timeParams.Destination;
                    locationsControl.ToLocationControl.AmendMode = true;
                }


				searchParams.OutwardDayOfMonth = outwardDayOfMonth;
				searchParams.OutwardMonthYear = outwardMonthYear;

				searchParams.ReturnDayOfMonth = returnDayOfMonth;
				searchParams.ReturnMonthYear = returnMonthYear;
			}

            #region Screen reader, Expandable Menu, Client Link
            //Set up screen reader labels contained in the date controls. This is done
			//here to use the local resource managger.
			dateControl.LeaveDateControl.DateControl.AmbiguousDateControl.FlexibilityScreenReaderLabel.Text = GetResource(leaveFlexibilitySRLabel);
			dateControl.ReturnDateControl.DateControl.AmbiguousDateControl.FlexibilityScreenReaderLabel.Text = GetResource(returnFlexibilitySRLabel);

			// Client side script for user navigation (when user hit enter, it should take the default action)
			UserExperienceEnhancementHelper.AddClientForUserNavigationDefaultAction(this.Page);

            //Added for white labelling:
			// Information column set up
			expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
			
			// Set up client link for bookmark on expandable menu
			clientLink.BookmarkTitle = GetResource("FindTrainInput.clientLink.BookmarkTitle");
			clientLink.LinkText = GetResource("FindTrainInput.clientLink.LinkText");

			// Determine url to save as bookmark
			IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
			
			string baseChannel = string.Empty;
			
			if (TDPage.SessionChannelName != null)
			{
				baseChannel = getBookmarkBaseChannelURL(TDPage.SessionChannelName);
			}

			PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindTrainInput);			

			string leftPartOfUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
			string url = leftPartOfUrl + baseChannel + pageTransferDetails.PageUrl;
			
			clientLink.BookmarkUrl = url;

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFindTrainCostInput);
            expandableMenuControl.AddExpandedCategory("Related links");
            #endregion
        }

		/// <summary>
		/// Page PreRender event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{		
			FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;
				
			// If page is a post back save the selected dates into the
			// search parameters.
			if (Page.IsPostBack)
			{
				findFareInputAdapter.UpdateJourneyDates(dateControl);
			}
            
            preferencesControl.AmbiguityMode = pageState.AmbiguityMode;

            // White Labeling - this section sets options for pageOptionsControltop.
            bool showPreferences = false;

            if (preferencesControl.AmbiguityMode)
            {
                showPreferences = (preferencesControl.PreferencesOptionsControl.Visible);
            }
            else
            {
                showPreferences = preferencesControl.PreferencesVisible;
            }


            pageOptionsControltop.AllowBack = preferencesControl.AmbiguityMode;
            pageOptionsControltop.AllowShowAdvancedOptions = !preferencesControl.AmbiguityMode && !preferencesControl.PreferencesVisible;

            if (showPreferences)
            {
                pageOptionsControltop.AllowHideAdvancedOptions = !preferencesControl.AmbiguityMode;
            }

            if (preferencesControl.AmbiguityMode)
            {
                pageOptionsControltop.Visible = true;
            }

			// Update the date controls with the values from the journey parameters.
			findFareInputAdapter.UpdateDateControl(dateControl);

			//Set visibility of controls based on the ambiguity mode.
			panelBackTop.Visible = pageState.AmbiguityMode;
			panelErrorMessage.Visible = pageState.AmbiguityMode;
            panelSubHeading.Visible = !pageState.AmbiguityMode;

			//If we are in ambiguity mode then test to see if the errors panel
			//needs to be updated with the latest error messages.
			if (pageState.AmbiguityMode)
			{
				findFareInputAdapter.UpdateErrorMessages(panelErrorMessage, labelErrorMessages, TDSessionManager.Current.ValidationError);
				dateControl.LeaveDateControl.DateErrors = TDSessionManager.Current.ValidationError;
				dateControl.ReturnDateControl.DateErrors = TDSessionManager.Current.ValidationError;
			}

			//Repopulate the drop down controls and the preferences with the 
			//values from the search params.
			findFareInputAdapter.InitPreferencesControl(preferencesControl);
			preferencesControl.TravelDetailsVisible = false;

            commandBack.Visible = pageState.AmbiguityMode;
            pageOptionsControltop.AllowBack = false;

            //Hide the advanced text if required.
            if (preferencesControl.PreferencesVisible || pageState.AmbiguityMode)
            {
                BlankPanelText(TDPageInformationHtmlPlaceHolderDefinition);
            }
		}
		
		/// <summary>
		/// Handle the pressing of the Next/Submit button.
		/// </summary>
		private void preferencesControl_Submit(object sender, System.EventArgs e)
		{
            // Must be done here to ensure if user was Amending journey -> changed a location ->
            // and it is ambiguous, then make the valid location resolved
            SetLocationsResolved();

			dateControl.CalendarClose();

			string returnDropDownValue;

			FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;
			//Holds retunred search wait state.
			AsyncCallStatus searchWaitState;

			//If we are not in ambiguity mode then save what is technically the users
			//inital input into temp. storage within the page state class. This can then
			//be used to reinstate the users initial input if the back button is pressed.
			if (!pageState.AmbiguityMode) 
			{
				pageState.SaveJourneyParameters(searchParams);
				findFareInputAdapter.AmbiguitySearch(locationsControl);
			} 
			else 
			{
				locationsControl.FromLocationControl.Search();
				locationsControl.ToLocationControl.Search();
			}

			//Save the users current info into the searchParams.
			//Note: We dont need to do this for the preference control as the event handling
			//takes care of putting new values into the searchParams object.
			findFareInputAdapter.SaveJourneyLocations(locationsControl);
			findFareInputAdapter.UpdateJourneyDates(dateControl);
			SaveTravelDetails();

			//Set the ticket type on the search params
			returnDropDownValue = dateControl.ReturnDateControl.DateControl.AmbiguousDateControl.ControlMonths.SelectedValue;
			if (returnDropDownValue == "NoReturn")
				pageState.SelectedTicketType = TicketType.Single;
			else if (returnDropDownValue == "OpenReturn")
				pageState.SelectedTicketType = TicketType.OpenReturn;
			else 
				pageState.SelectedTicketType = TicketType.Return;

			// Set up wait page
			int refreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.FindFare.FindFareInput"]);
			string resourceId = "WaitPageMessage.FindFare.FindFareInput";
			string resourceFilename = "FindAFare";

			//Invoke the cost search runner and return a searchWaitStateData object.
			findFareInputAdapter.InitialiseAsyncCallStateForRailSearchByPrice(refreshInterval, resourceFilename, resourceId);
			searchWaitState = findFareInputAdapter.InvokeValidateAndRunFares();

			//Reference reference to page state from the session manager. This is required
			//as the deferred storage will have been cleared so we need to access this directly
			//from the session manager.
			pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;
			pageState.FareDateTable = null;

			//Set the ambiguity mode of the page based on the returned value
			//in the searchWaitStateData object.
			pageState.AmbiguityMode = (searchWaitState == AsyncCallStatus.ValidationError);

			//Cause transition event to determine correct redirection.
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindTrainCostAmbiguityResolution;
		}
		
		/// <summary>
		/// Handle the processing of the Clear button
		/// </summary>
		private void preferencesControl_Clear(object sender, System.EventArgs e)
		{
			FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;

			// Set the visibility of this control before we initialise pagestate
			preferencesControl.PreferencesVisible = pageState.TravelDetailsVisible;

			// Reinitialise the page state.
			pageState.Initialise();

			// Sync up pagestate with Preferences visible
			pageState.TravelDetailsVisible = preferencesControl.PreferencesVisible;
			
			pageState.ShowChild = false;

			// Page is no longer ambiguous
			pageState.AmbiguityMode = false;
			
			// Call initalise journey paremeters to set up the cost search params
			InitialiseJourneyParameters();
			
			// Unset the locations
			locationsControl.SetLocationsUnspecified();
			
			// Set the locationn controls to have the values of the search parameters
			// which have now been initialised (so no locations set).
			findFareInputAdapter.InitLocationsControl(locationsControl);
			
			// Set visibility of date controls
			dateControl.LeaveDateControl.Visible = true;
			dateControl.ReturnDateControl.Visible = true;
			
			// Close all help windows
			TDPage.CloseAllSingleWindows(Page);
			
			// Load user preferences
			LoadTravelDetails();

			// Update date controls with reset parameters
			findFareInputAdapter.UpdateDateControl(dateControl);

		}

		/// <summary>
		/// Date control event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DateSelectControl_DateChanged(object sender, EventArgs e)
		{
			findFareInputAdapter.UpdateJourneyDates(dateControl);
		}

		/// <summary>
		/// Preference control visible event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_PreferencesVisibleChanged(object sender, EventArgs e)
		{
			FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;
			pageState.TravelDetailsVisible = preferencesControl.PreferencesVisible;
            if (!preferencesControl.PreferencesVisible)
                pageOptionsControltop.Visible = true;
		}

		//Handle the discount rail card changed event.
		/// <summary>
		/// Rail card changed preference control event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferenceControl_DiscountRailCardChanged(object sender, EventArgs e)
		{
			searchParams.RailDiscountedCard = preferencesControl.DiscountRailCard;
		}

		/// <summary>
		/// Adult/Child preference control event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferenceControl_AdultChildChanged(object sender, EventArgs e)
		{
			FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;
			pageState.ShowChild = !preferencesControl.AdultChild;
		}
		
		/// <summary>
		/// Search type changed event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>									 
		private void SearchTypeControl_SearchTypeChanged(object sender, System.EventArgs e)
		{
			//Dependant on which search type is selected perform a page
			//transistion to put the user on the correct page. Evaluate
			//the cost search property of the control.
			if (!searchTypeControl.CostSearch)
			{
				sm.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindTrainInputDefault;
			
				// Set one use key to indicate to time based page that we have come
				// from cost based planning.
				sm.SetOneUseKey(SessionKey.TransferredFromCostBasedPlanning, "true");

				findFareInputAdapter.UpdateJourneyDates(dateControl);
			}
		}

		/// <summary>
		/// Back button event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferenceControl_BackButtonClicked(object sender, System.EventArgs e)
		{

			// Get a reference to the cost based page state on session manager
			FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;

			// Clear any validation messages on the session manager.
			sm.ValidationError = null;

			// If the page is at the highest level then a number of controls
			// need to be reset/
			if (findFareInputAdapter.IsAtHighestLevel()) 
			{

				// Set up page state object. No longer ambiguous and also restate
				// the users original input parameters.
				pageState.AmbiguityMode = false;
				pageState.ReinstateJourneyParameters(searchParams);

				// Set preference control no longer show in ambiguous mode.
				preferencesControl.AmbiguityMode = false;

				// Set date controls to non-ambiguous and remove any errors displayed
				// on the date controls
				dateControl.LeaveDateControl.AmbiguityMode = false;
				dateControl.ReturnDateControl.AmbiguityMode = false;
				dateControl.LeaveDateControl.Visible = true;
				dateControl.ReturnDateControl.Visible = true;
				dateControl.LeaveDateControl.DateErrors = null;
				dateControl.ReturnDateControl.DateErrors = null;
				
				// Set the origin location to unspecified
				if (locationsControl.FromLocationControl.TheLocation.Status != TDLocationStatus.Valid) 
				{
					locationsControl.FromLocationControl.SetLocationUnspecified();
				}
				
				// Set the destination location to unspecified
				if (locationsControl.ToLocationControl.TheLocation.Status != TDLocationStatus.Valid) 
				{
					locationsControl.ToLocationControl.SetLocationUnspecified();
				}
			} 
			else
			{
				// Decrement the drilldown level of any ambiguous locations
				// that are not yet at their highest level
				searchParams.Origin.DecrementLevel();
				searchParams.Destination.DecrementLevel();
			}


            pageOptionsControltop.Visible = !preferencesControl.PreferencesVisible;
            
		}

		/// <summary>
		/// Event handler for when New Location is clicked on origin
		/// location.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LocationsControl_NewLocationFrom(object sender, System.EventArgs e)
		{
			//Update search params with the new location. This code will be initiated
			//by the user selecting a new location and submitting the page (via the Search
			//method in the find location control) or if the user clicks the "New location"
			//button.
			searchParams.OriginLocation = locationsControl.FromLocationControl.TheLocation;
			searchParams.Origin = locationsControl.FromLocationControl.TheSearch;
			searchParams.Origin.SearchType = SearchType.Locality;
			searchParams.Origin.DisableGisQuery();
			searchParams.OriginType = locationsControl.FromLocationControl.LocationControlType;
		}

		/// <summary>
		/// Event handler for when New Location is clicked on destination
		/// location.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LocationsControl_NewLocationTo(object sender, System.EventArgs e)
		{
			//See notes above.
			searchParams.DestinationLocation = locationsControl.ToLocationControl.TheLocation;
			searchParams.Destination = locationsControl.ToLocationControl.TheSearch;
			searchParams.Destination.SearchType = SearchType.Locality;
			searchParams.Destination.DisableGisQuery();
			searchParams.DestinationType = locationsControl.ToLocationControl.LocationControlType;
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
            pageOptionsControltop.Visible = false;
        }

		#endregion
		

		#region Private Methods
		
		/// <summary>
		/// Initial page set up method.
		/// </summary>
		private void InitialPageSetup(bool forceReset)
		{
			bool resetDone;

			// Get reference to current itinerary manager instance and if an extension is 
			// in progress, cancel it
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

            #region Clear cache of journey data
            // if an extension is in progress, cancel it
            sm.ItineraryManager.CancelExtension();

            ClearCacheHelper helper = new ClearCacheHelper();

            // Force clear of any printable information if added by the journey result page
            helper.ClearPrintableResultCache(TDSessionPartition.CostBased);

            // Fix for IR5481 Session issue when going from between different planners using the left hand menu
            if (((TDSessionManager.Current.FindAMode != FindAMode.RailCost && TDSessionManager.Current.FindAMode != FindAMode.Train) && TDSessionManager.Current.ItineraryMode != ItineraryManagerMode.None) || forceReset)
            {
                // We have come directly from another planner so clear results from session.
                helper.ClearJourneyResultCache();
            }
            #endregion

            //Initialise journey paremters and page state
			resetDone = sm.InitialiseJourneyParameters(FindAMode.RailCost);

			//Set up the page if the journey parameters have been reset.
			if (resetDone)            
			{
				sm.FindPageState.Initialise();
				InitialiseJourneyParameters();
				LoadTravelDetails();
			}
		}
		
		/// <summary>
		/// Initialise cost search journey paremeters.
		/// </summary>
		private void InitialiseJourneyParameters()
		{
			DataServices.DataServices populator = (DataServices.DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

			searchParams = (CostSearchParams)sm.JourneyParameters;

			searchParams.Initialise();

			searchParams.DestinationType = new TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);
			searchParams.OriginType = new TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);

			searchParams.Destination = new LocationSearch();
			searchParams.Origin = new LocationSearch();

			searchParams.Destination.SearchType = SearchType.MainStationAirport;
			searchParams.Origin.SearchType = SearchType.MainStationAirport;

			//Initialise preferences options
			searchParams.RailDiscountedCard = populator.GetDefaultListControlValue(DataServiceType.DiscountRailCardDrop);
			searchParams.CoachDiscountedCard = populator.GetDefaultListControlValue(DataServiceType.DiscountCoachCardDrop);

            // Set routing guide flags
            searchParams.RoutingGuideInfluenced = bool.Parse(Properties.Current["RoutingGuide.FindATrainCost.RoutingGuideInfluenced"]);
            searchParams.RoutingGuideCompliantJourneysOnly = bool.Parse(Properties.Current["RoutingGuide.FindATrainCost.RoutingGuideCompliantJourneysOnly"]);
		}

		/// <summary>
		/// Initialise the location controls if they have not already been
		/// set up.
		/// </summary>
		private void InitialiseLocationControls()
		{
			if (locationsControl.FromLocationControl.TheLocation == null)
			{
				//Initialise the start and end locations.
				locationsControl.OriginLocation = searchParams.OriginLocation;
				locationsControl.DestinationLocation = searchParams.DestinationLocation;

				//Initialise the start/end location control types.
				locationsControl.FromLocationControl.LocationControlType = searchParams.OriginType;
				locationsControl.ToLocationControl.LocationControlType = searchParams.DestinationType;

				//Initialise the start/search objects.
				locationsControl.OriginSearch = searchParams.Origin;
				locationsControl.DestinationSearch = searchParams.Destination;
			}
		}

        /// <summary>
        /// Sets the From and To locations into editable mode if user is Amending their journey
        /// </summary>
        private void SetLocationsAmendable()
        {
            // Set locations to be amendable
            if (
                ((!Page.IsPostBack) && (pageState.AmendMode))
                ||
                ((!pageState.AmbiguityMode) && (pageState.AmendMode)))
            {
                // Don't want the location to be amendable if it was a FindNearest
                if (searchParams.OriginLocation != null)
                    locationsControl.FromLocationControl.AmendMode = pageState.AmendMode;

                if (searchParams.DestinationLocation != null)
                    locationsControl.ToLocationControl.AmendMode = pageState.AmendMode;
            }
        }

        /// <summary>
        /// Sets the From and To locations into Resolved mode if they are valid
        /// Only done in Submit request because in case we go to ambiguity page, need to ensure we show a resolved location
        /// </summary>
        private void SetLocationsResolved()
        {
            if ((searchParams.OriginLocation != null) && (searchParams.OriginLocation.Status == TDLocationStatus.Valid))
            {
                locationsControl.FromLocationControl.AmendMode = false;
            }

            if ((searchParams.DestinationLocation != null) && (searchParams.DestinationLocation.Status == TDLocationStatus.Valid))
            {
                locationsControl.ToLocationControl.AmendMode = false;
            }
        }

		/// <summary>
		/// Saves travel details to the user preferences.
		/// Note: Does not save over any set coach discount card. The coach card is available
		/// here its just not displayed so saving it would overwrite any other coach card.
		/// </summary>
		private void SaveTravelDetails() 
		{
			if (TDSessionManager.Current.Authenticated && preferencesControl.PreferencesOptionsControl.SavePreferences)
			{
				TDProfile travelPreferences = TDSessionManager.Current.CurrentUser.UserProfile;
				travelPreferences.Properties[ProfileKeys.DISCOUNT_CARD_RAIL].Value = preferencesControl.DiscountRailCard;
				travelPreferences.Properties[ProfileKeys.ADULT_CHILD].Value = (!preferencesControl.AdultChild);
				travelPreferences.Update();
			}
		}

		/// <summary>
		/// Loads travel details if user is authenticated and copies them into parameters. Should
		/// be called before parameters are populated onto the page.
		/// </summary>
		private void LoadTravelDetails()
		{
			if (TDSessionManager.Current.Authenticated) 
			{
				FindCostBasedPageState pageState = (FindCostBasedPageState)sm.FindPageState;
				TDProfile travelPreferences = TDSessionManager.Current.CurrentUser.UserProfile;
				ProfileProperties curr;
			
				//Adult child preference - goes onto page state.
				curr = travelPreferences.Properties[ProfileKeys.ADULT_CHILD];
				if (curr != null && curr.Value != null)
				{
					pageState.ShowChild = (bool)curr.Value;
					pageState.TravelDetailsVisible = true;
				}

				//Discount rail card - goes onto search params.
				curr = travelPreferences.Properties[ProfileKeys.DISCOUNT_CARD_RAIL];
				if (curr != null && curr.Value != null)
				{
					searchParams.RailDiscountedCard = (string)curr.Value;
					pageState.TravelDetailsVisible = true;
				}

				//Discount coach card - goes onto search params;
				curr = travelPreferences.Properties[ProfileKeys.DISCOUNT_CARD_COACH];
				if (curr != null && curr.Value != null)
				{
					searchParams.CoachDiscountedCard = (string)curr.Value;
					pageState.TravelDetailsVisible = true;
				}
			}
		}

		#endregion
	}
}