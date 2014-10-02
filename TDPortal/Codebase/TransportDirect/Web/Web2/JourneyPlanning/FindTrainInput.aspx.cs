// *********************************************** 
// NAME                 : FindTrainInput.aspx.cs 
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 13.07.04
// DESCRIPTION			: Input page for finding train journeys.
// Allows user to specify details for a train only journey and
// resolve any ambiguities before proceeding to journey planning
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindTrainInput.aspx.cs-arc  $
//
//   Rev 1.20   Mar 22 2013 10:49:16   DLane
//CCN0648c - Super Batch enhancements
//Resolution for 5907: CCN0648c - "Super Batch" enhancements
//
//   Rev 1.19   Jul 28 2011 16:20:12   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.18   Oct 29 2010 09:05:20   RBroddle
//Removed explicit wire up to Page_Init as AutoEventWireUp=true for this page so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.17   Jul 29 2010 16:13:32   mmodi
//Changes to page layout and styles to be exactly consistent for all input pages in the Portal
//Resolution for 4760: IE7-find a car route check boxes
//
//   Rev 1.16   Jul 06 2010 15:46:54   apatel
//resolve the issue with switching between cost and time rail planners after amend
//Resolution for 5566: Issue with Switching between rail planner and rail cost planner
//
//   Rev 1.15   Jun 17 2010 12:23:52   apatel
//remove the filter controls set for auto suggest
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.14   Jun 16 2010 10:21:14   apatel
//Updated to implement auto-suggest functionality
//
//   Rev 1.13   May 13 2010 13:05:24   mmodi
//Added code call to clear the printable map session information 
//Resolution for 5535: Printable maps session logic improvement
//
//   Rev 1.12   Apr 12 2010 15:46:46   mmodi
//Moved DisplaySessionErrors to be processed earlier before the errors are cleared from be initialise
//Resolution for 5503: Maps : When maps timeout you can still zoom and pan the static tiles
//
//   Rev 1.11   Mar 26 2010 12:01:26   MTurner
//Added code to clear journey results from session if you are coming directly from another planner.
//
//Resolution for 5481: Session issue when going from FAT to D2D using the left hand menu
//
//   Rev 1.10   Jan 29 2010 14:45:30   mmodi
//Updated to reset CycleResult to correct error when planning journey after coming directly from Cycle journey details
//Resolution for 5388: Cycle Planner - Server error when planning Door to door after a cycle journey
//
//   Rev 1.9   Jan 30 2009 10:44:16   apatel
//Search Engine Optimasation changes - CCN 624
//Resolution for 5229: Search Engin Optimisation Changes -  CCN624
//
//   Rev 1.8   May 22 2008 16:42:36   mmodi
//Updated to make locations editable
//Resolution for 4998: "Find Nearest" functionality on Find Train / Find Coach input not working
//Resolution for 5000: Find Train - when ambiguity page is diplayed, resolved locations still editable
//Resolution for 5002: Amend Find a train cost does not use the new location
//
//   Rev 1.7   May 02 2008 11:31:46   mmodi
//No change.
//Resolution for 4923: Control alignments: Find a train
//
//   Rev 1.6   May 01 2008 17:23:38   mmodi
//Updated to display session timeout error
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.5   Apr 11 2008 15:14:20   apatel
//Put a check to see if session manager is null in page_Unload event
//Resolution for 4855: Starting a session on FAT page causes error.
//
//   Rev 1.4   Apr 09 2008 14:56:06   scraddock
//Fixed bug with timeout on train input screen
//Resolution for 4752: DEL 10 - Find Train Input - session timeout
//
// rev devfactory apr 9 2008 sbarker
// Resolved problem with Train page yellow screening on timeout.
//
//   Rev 1.3   Apr 08 2008 15:15:02   scraddock
//Advanced Text Not Hidden
//Resolution for 4847: Advanced Text Not Hidden
//
// rev devfactory 8 apr sbarker
// Advanced text hidden when advanced pressed, or in ambiguity mode
//
//   Rev 1.2   Mar 31 2008 13:24:40   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory Feb 08 09:44:05 psheldrake
//   added support for new resx / mcms loading logic
//
//DEVFACTORY FEB 21 2008 sbarker
//Page icon added
//
//    Rev DevFactory Jan 08 09:44:05 aahmed
//    white labelling added new control pageOptionsControltop
//
//   Rev 1.43   Sep 03 2007 15:25:04   pscott
//CCN407 IR 4490
//title and key word changes for Google natural search
//
//   Rev 1.42   Nov 14 2006 14:56:10   tmollart
//Modified CMS Placeholder definitions.
//
//   Rev 1.41   Nov 14 2006 10:23:16   rbroddle
//Merge for stream4220
//
//   Rev 1.40.1.3   Nov 12 2006 13:41:06   tmollart
//Work in progress.
//
//   Rev 1.40.1.2   Nov 09 2006 17:38:06   tmollart
//Work in progress.
//
//   Rev 1.40.1.1   Nov 07 2006 11:44:24   tmollart
//Merge of Kens Changes for the new design page with the Rail Search by Price functionality. Work in progress check in.
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.40   Apr 26 2006 12:02:56   pscott
//Ir3510/3927
//Calendar Control Closure in Ambiguity page
//
//   Rev 1.39   Apr 25 2006 11:28:16   COwczarek
//Change logic in submitRequest method to set ambiguity mode
//to false if auto plan landing request
//Resolution for 3943: DN077 Landing Page: Dates not displayed on Ambiguity screen
//
//   Rev 1.38   Apr 13 2006 11:09:38   COwczarek
//Call ResetLandingPageSessionParameters in Unload event
//handler rather than PreRender event handler. This ensures
//parameters are reset even if redirect occurs due to autoplan
//being set.
//Resolution for 3902: Landing Page: Using Find A Car with autoplan set then clicking amend throws exception
//
//   Rev 1.37   Apr 12 2006 12:43:48   COwczarek
//Rearrange logic in Page_PreRender so that UpdateDateControl is not called on initial load if request is a landing page request.
//Resolution for 3773: Landing Page: Return date error when no return date or time specified
//
//   Rev 1.36   Mar 30 2006 10:37:36   halkatib
//Made page check if coming from landing page before initialising the journey paramters. Initialisation is not required on the page since the landing page does this already. When this happens twice in a landing page call the journeyparametes set by the landing are changed.
//
//   Rev 1.35   Mar 27 2006 10:28:50   kjosling
//Merged stream 0023 - Journey Results
//
//   Rev 1.34   Mar 22 2006 17:30:16   halkatib
//Changes due to Merge of stream3152 Landing Page phase 3
//
//   Rev 1.33   Mar 10 2006 12:46:28   pscott
//SCR3510
//Close Calendar Control when going to Ambiguity page
//
//   Rev 1.32   Feb 23 2006 19:43:04   AViitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.31   Feb 10 2006 18:11:08   kjosling
//Fixed merge
//
//   Rev 1.31   Feb 10 2006 17:07:00   kjosling
//Fixed
//
//   Rev 1.30   Feb 10 2006 10:47:54   jmcallister
//Manual Merge of Homepage 2. IR3180
//
//   Rev 1.29   Jan 12 2006 18:16:04   RPhilpott
//Reset TDItineraryManager to default (mode "None") in page initialisation to allow for case where we are coming from VisitPlanner.
//Resolution for 3450: DEL 8: Server error when returning to Quickplanner results from Visit Planner input
//
//   Rev 1.28   Nov 15 2005 19:07:56   rgreenwood
//IR2990 Wired up help button
//Resolution for 2990: UEE Post Build Enhancement: Add Help Pages to Input Pages
//
//   Rev 1.27   Nov 09 2005 17:07:38   jgeorge
//Manual merge for stream2818 (search by price)
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.26   Nov 09 2005 17:03:56   NMoorhouse
//TD093 - UEE Input Pages - Soft Content Updates
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.25   Nov 04 2005 10:57:28   NMoorhouse
//Manual merge of stream2816
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.24   Nov 02 2005 14:47:20   jgeorge
//Undid previous change, as this was not complete and caused an additional bug. Fix for the original IR was made in FindLocationControl and FindViaLocationControl.
//Resolution for 2935: Del 7.2: Expanding Train Preferences prevents user planning journey
//
//   Rev 1.23   Oct 11 2005 17:06:20   kjosling
//Fixed problem switching to preferences. 
//Resolution for 2842: DN79 UEE Stage 1:  "No Options found" message displayed when opening the Journey Details section after pressing Back button on ambiguity page
//
//   Rev 1.22.2.5   Nov 02 2005 18:15:18   NMoorhouse
//TD93 - UEE Input Pages - Update format following analyst screen walk-through
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.22.2.4   Oct 28 2005 18:30:10   AViitanen
//TD093 Input Pages - page formatting
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.22.2.3   Oct 28 2005 14:10:32   AViitanen
//TD093 Input Pages - Page formatting
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.22.2.2   Oct 24 2005 21:17:46   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.22.2.1   Oct 10 2005 19:07:10   rgreenwood
//TD089 ES020 Image Button Replacement - Changed resource refs to fix build
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.22   Sep 29 2005 12:52:16   build
//Automatically merged from branch for stream2673
//
//   Rev 1.21.1.1   Sep 09 2005 14:17:38   Schand
//DN079 UEE Enter Key.
//Updates for UEE.
//Resolution for 2756: DEL 8 Stream: Add default key functionality on Input pages to Next button
//
//   Rev 1.21.1.0   Sep 05 2005 17:59:12   Schand
//Added EventHandler for DefaultAction button, 
//Registered client side script.
//Resolution for 2756: DEL 8 Stream: Add default key functionality on Input pages to Next button
//
//   Rev 1.21   Apr 27 2005 10:20:34   COwczarek
//Fix compiler warnings
//
//   Rev 1.20   Apr 15 2005 12:48:14   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.19   Mar 08 2005 16:27:56   bflenk
//TimeOut functionality implemented in TDPage.cs, removed from this file - IR1720
//
//   Rev 1.18   Feb 23 2005 17:26:22   RAlavi
//This has been changed temporarily. It is now checked back in as version 1.16
//
//   Rev 1.17   Jan 28 2005 19:37:38   ralavi
//Updated for car costing
//
//   Rev 1.16   Nov 19 2004 11:32:54   asinclair
//Fix for IR 1720 Vantive 3482658
//
//   Rev 1.15   Oct 15 2004 12:39:10   jgeorge
//Changed to take account of new JourneyPlanStateData and changes to existing JourneyPlanControlData.
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.14   Sep 23 2004 10:39:22   jmorrissey
//Added // $Log$ to file. 


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
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.CommonWeb.Helpers;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
    /// Input page for finding train journeys.
    /// Allows user to specify details for a train only journey and
    /// resolve any ambiguities before proceeding to journey planning
	/// </summary>
    public partial class FindTrainInput : TDPage
	{

		#region Declarations

		// Page System Objects

		// Page Controls
		protected TransportDirect.UserPortal.Web.Controls.TDButton commandBack;
		protected TransportDirect.UserPortal.Web.Controls.ClientLinkControl clientLink;
		protected TransportDirect.UserPortal.Web.Controls.SearchTypeControl searchTypeControl;
		protected TransportDirect.UserPortal.Web.Controls.FindLeaveReturnDatesControl dateControl;
		protected TransportDirect.UserPortal.Web.Controls.FindToFromLocationsControl locationsControl;
		protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl expandableMenuControl;
		protected TransportDirect.UserPortal.Web.Controls.FindCoachTrainPreferencesControl preferencesControl;

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
		/// Holds user's current page state for this page.
		/// </summary>
		private FindTrainPageState pageState;
				
		/// <summary>
		/// Holds user's map current page state.
		/// </summary>
		private InputPageState inputPageState;

		/// <summary>
		/// Shows if cost based searching is available or not
		/// </summary>
		private bool costBasedSearchAvailable;

        /// <summary>
        /// Hold user's current journey parameters for train only journey.
        /// </summary>
        private TDJourneyParametersMulti journeyParams;

        /// <summary>
        /// Helper class responsible for common methods to Find A pages.
        /// </summary>
        private FindCoachTrainInputAdapter findInputAdapter;

		/// <summary>
		/// Helper class for Landing Page functionality.
		/// </summary>
		private LandingPageHelper landingPageHelper = new LandingPageHelper();

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
        /// </summary>
        public FindTrainInput()
        {
            this.pageId = PageId.FindTrainInput;
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

		/// <summary>
		/// Performs page initialisation including event wiring.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Init(object sender, EventArgs e)
		{
			// Event Handler for default action button
			headerControl.DefaultActionEvent +=  new EventHandler(preferencesControl_Submit);
			



			// Search type control event handling
			searchTypeControl.SearchTypeChanged += new System.EventHandler(SearchTypeControl_SearchTypeChanged);

			// Preference control event handling (for control selection changes).
			preferencesControl.ChangesOptionChanged += new EventHandler(preferencesControl_ChangesOptionChanged);
			preferencesControl.ChangeSpeedOptionChanged += new EventHandler(preferencesControl_ChangeSpeedOptionChanged);
			preferencesControl.PageOptionsControl.Submit += new EventHandler(preferencesControl_Submit);
			preferencesControl.PageOptionsControl.Back += new EventHandler(preferencesControl_Back);
			preferencesControl.PreferencesVisibleChanged += new EventHandler(preferencesControl_PreferencesVisibleChanged);
			preferencesControl.ViaLocationControl.NewLocation += new EventHandler(preferencesControl_NewLocation);
			preferencesControl.PageOptionsControl.Clear += new EventHandler(preferencesControl_Clear);
            // added for the top PageOptionsControl
            pageOptionsControltop.Submit += new EventHandler(preferencesControl_Submit);
            pageOptionsControltop.Back += new EventHandler(preferencesControl_Back);
            pageOptionsControltop.Clear += new EventHandler(preferencesControl_Clear);
            pageOptionsControltop.ShowAdvancedOptions += new EventHandler(pageOptionsControltop_ShowAdvancedOptions);
            pageOptionsControltop.HideAdvancedOptions += new EventHandler(pageOptionsControltop_HideAdvancedOptions);
			// Location control event handlers
			locationsControl.NewLocationFrom += new EventHandler(locationsControl_NewLocationFrom);
			locationsControl.NewLocationTo += new EventHandler(locationsControl_NewLocationTo);			
			locationsControl.ToLocationControl.FindNearestClick += new EventHandler(locationsControlToLocationControl_FindNearestClick);
			locationsControl.FromLocationControl.FindNearestClick += new EventHandler(locationsControlFromLocationControl_FindNearestClick);
			
			// Date selection control event handling
			dateControl.LeaveDateControl.DateChanged += new EventHandler(dateControlLeaveDateControl_DateChanged);
			dateControl.ReturnDateControl.DateChanged += new EventHandler(dateControlReturnDateControl_DateChanged);
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


		#region Private Properties

		/// <summary>
		/// IsFindNearest returns true if the 'Find Nearest' functionality has been used to choose the 
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


        #region Event Handlers
        
        /// <summary>
        /// Event handler for page Load event fired by page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, System.EventArgs e)
        {

			// Set session maanger reference
			sm = TDSessionManager.Current;

            #region Display Session Errors
            // Check if there are any errors in the InputPageState and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, sm.InputPageState.InputSessionErrors);

            // Clear the error messages
            sm.InputPageState.InputSessionErrors = null;
            #endregion

			// Rail Search by Price functionality.

			// Check if rail search by price functionality is available
			costBasedSearchAvailable = bool.Parse(Properties.Current["FindAFareAvailable.Rail"]);

			// Checks if the user has been transferred from the FindTrainCostInput page
			// and if so will replace the users input parameters on this page.
			if (sm.GetOneUseKey(SessionKey.TransferredFromCostBasedPlanning) != null )
			{
				CostSearchParams costParams = (CostSearchParams)sm.JourneyParameters;
				
				originLocationText = costParams.Origin.InputText;
				destLocationText = costParams.Destination.InputText;
				outwardDayOfMonth = costParams.OutwardDayOfMonth;
				outwardMonthYear = costParams.OutwardMonthYear;
				returnDayOfMonth = costParams.ReturnDayOfMonth;
				returnMonthYear = costParams.ReturnMonthYear;
			}

			// If the page is a postback then update the session data with the 
			// values from controls (that dont fire events) otherwise initalise the
			// page for a first request.
            // 
            labelFindPageTitle.Text = GetResource("FindTrainInput.labelFindTrainTitle");
            labelFromToTitle.Text = GetResource("FindTrainInput.labelFromToTitle");
            PageTitle = GetResource("FindTrainInput.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");
            imageFindATrain.ImageUrl = GetResource("HomeDefault.imageFindTrain.ImageUrl");
            imageFindATrain.AlternateText = " ";
            commandBack.Text = GetResource("FindTrainInput.CommandBack.Text");

			if (Page.IsPostBack)
			{
				UpdateOtherControls();
			}
			else
			{
				InitialRequestSetup();
			}

            // Make the locations editable
            SetLocationsAmendable();

			// Help controls
			if (pageState.AmbiguityMode)
			{
				Helpbuttoncontrol1.HelpUrl = GetResource("FindTrainInput.HelpAmbiguityUrl");
			}
			else
			{
				Helpbuttoncontrol1.HelpUrl = GetResource("FindTrainInput.HelpPageUrl");
			}

			// Initialise the preferences control and the locations control.
            findInputAdapter.InitialiseControls(preferencesControl, locationsControl);

			// Rail Search by Price Functionality.
			// If page not post back then load text into location controls.
			// Note: It may not be populated but this doenst matter.
			if (!Page.IsPostBack && sm.GetOneUseKey(SessionKey.TransferredFromCostBasedPlanning) != null )
			{
				locationsControl.OriginSearch.InputText = originLocationText;
				locationsControl.DestinationSearch.InputText = destLocationText;

				journeyParams.OutwardDayOfMonth = outwardDayOfMonth;
				journeyParams.OutwardMonthYear = outwardMonthYear;

				journeyParams.ReturnDayOfMonth = returnDayOfMonth;
				journeyParams.ReturnMonthYear = returnMonthYear;

				findInputAdapter.InitDateControl(dateControl);
			}


			// Client side script for user navigation (when user hit enter, it should take the default action)
			UserExperienceEnhancementHelper.AddClientForUserNavigationDefaultAction(this.Page);  

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

			
			//Check if we need to initiate an automatic search due to Landing Page Autoplan Mode			
			if (sm.Session[ SessionKey.LandingPageAutoPlan ])
			{
				//if required then submit request method. 				
				SubmitRequest();
            }

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFindTrainInput);
            expandableMenuControl.AddExpandedCategory("Related links");
            commandBack.Click += new EventHandler(preferencesControl_Back);
            
        }

		/// <summary>
		/// Event handler for page PreRender event fired by page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			// If the page is a post back then its necessary to update the date controls
			// with the values from the journey parameters.
			if (Page.IsPostBack)
			{
				findInputAdapter.UpdateDateControl(dateControl);
			}

			// Update error messages with any validation errors.
			findInputAdapter.UpdateErrorMessages(panelErrorMessage, labelErrorMessages, sm.ValidationError);

            // White Labeling Jan 08 - this section sets options for pageOptionsControltop.
            bool showPreferences;

            if (preferencesControl.AmbiguityMode)
            {
                showPreferences = (preferencesControl.ViaLocationControl.Visible);
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


			// Set control visibility
			panelBackTop.Visible = pageState.AmbiguityMode;
			searchTypeControl.Visible = costBasedSearchAvailable;
			panelSubHeading.Visible = !pageState.AmbiguityMode;
			commandBack.Visible = preferencesControl.AmbiguityMode;
            pageOptionsControltop.AllowBack = false;

            //Hide the advanced text if required.
            if (preferencesControl.PreferencesVisible || pageState.AmbiguityMode)
            {
                BlankPanelText(TDPageInformationHtmlPlaceHolderDefinition);
            }

            SetupAutoSuggestDropDowns();
		}

        /// <summary>
        /// Configures LocationSelectControl's filter control ids
        /// Also configures Via LocationSelectControl to not show group stations
        /// </summary>
        private void SetupAutoSuggestDropDowns()
        {
            LocationSelectControl2 fromSelectControl =  locationsControl.FromLocationControl.TriLocationControl.LocationUnspecifiedControl;
            LocationSelectControl2 toSelectControl = locationsControl.ToLocationControl.TriLocationControl.LocationUnspecifiedControl;
            LocationSelectControl2 viaSelectControl = preferencesControl.ViaLocationControl.TristateLocationControl.LocationUnspecifiedControl;

            viaSelectControl.AutoSuggestGroupStations = false;
        }

		/// <summary>
		/// Page Unload event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Unload(object sender, System.EventArgs e)
		{
			//reset landing page session parameters
            if (sm != null)
            {
                if (sm.Session[SessionKey.LandingPageCheck])
                {
                    landingPageHelper.ResetLandingPageSessionParameters();
                }
            }
		}

        /// <summary>
        /// Event handler called when "changes" type option changed. Updates journey parameters with new value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_ChangesOptionChanged(object sender, EventArgs e)
        {
            journeyParams.PublicAlgorithmType = preferencesControl.Changes;
        }

        /// <summary>
        /// Event handler called when "changes" speed option changed. Updates journey parameters with new value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_ChangeSpeedOptionChanged(object sender, EventArgs e)
        {
            journeyParams.InterchangeSpeed = preferencesControl.ChangesSpeed;
        }

        /// <summary>
        /// Event handler called when back button clicked in ambiguous mode. Decrements the level of
        /// hierarchical location searches (origin, destination and public via). If all locations are at highest 
        /// level then page is reverted to input mode and original input parameters reinstated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_Back(object sender, EventArgs e)
        {
			dateControl.CalendarClose();

            if (findInputAdapter.IsAtHighestLevel()) 
            {
				//Check to see if there is a previous page waiting on the stack
				if(inputPageState.JourneyInputReturnStack.Count != 0)
				{
					TransitionEvent lastPage = (TransitionEvent)inputPageState.JourneyInputReturnStack.Pop();
					//If the user is returing to the previous journey results, re-validate them
					if(lastPage == TransitionEvent.FindAInputRedirectToResults)
					{
						sm.JourneyResult.IsValid = true;
					}
					sm.FormShift[SessionKey.TransitionEvent] = lastPage;
					return;
				}

				// Set up page state object. No longer ambiguous and also restate
				// the users original input parameters.
                pageState.AmbiguityMode = false;
                pageState.ReinstateJourneyParameters(journeyParams);

				// Set up page objects
				preferencesControl.ChangesDisplayMode = GenericDisplayMode.Normal;
                preferencesControl.ChangesSpeedDisplayMode = GenericDisplayMode.Normal;
                dateControl.LeaveDateControl.AmbiguityMode = false;
                dateControl.ReturnDateControl.AmbiguityMode = false;
                dateControl.LeaveDateControl.DateErrors = null;
                dateControl.ReturnDateControl.DateErrors = null;
                
				// Clear error messages from the session manager
				sm.ValidationError = null;

				// Initalise location controls
                findInputAdapter.InitLocationsControl(locationsControl);
                findInputAdapter.InitViaLocationsControl(preferencesControl);

				// Reset all locations back to unspecificed
                if (locationsControl.FromLocationControl.TheLocation.Status != TDLocationStatus.Valid) 
                {
                    locationsControl.FromLocationControl.SetLocationUnspecified();
                }
                if (locationsControl.ToLocationControl.TheLocation.Status != TDLocationStatus.Valid) 
                {
                    locationsControl.ToLocationControl.SetLocationUnspecified();
                }
                if (preferencesControl.ViaLocationControl.TheLocation.Status != TDLocationStatus.Valid)
                {
                    preferencesControl.ViaLocationControl.SetLocationUnspecified();
                }
            } 
            else
            {
                // Decrement the drilldown level of any ambiguous locations
                // that are not yet at their highest level
                journeyParams.Origin.DecrementLevel();
                journeyParams.Destination.DecrementLevel();
                journeyParams.PublicVia.DecrementLevel();
            }

            // CCN 0427 Setting visibility of top page options control
            pageOptionsControltop.Visible = !preferencesControl.PreferencesVisible;

        }

        /// <summary>
        /// Event handler called when visibility of preferences changed. Updates page state with new value.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void preferencesControl_PreferencesVisibleChanged(object sender, EventArgs e) 
        {
            pageState.TravelDetailsVisible = preferencesControl.PreferencesVisible;
            if (!preferencesControl.PreferencesVisible)//white labelling
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
			SubmitRequest();
		}

		/// <summary>
		/// Event handler for the Search Type Control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>									 
		private void SearchTypeControl_SearchTypeChanged(object sender, System.EventArgs e)
		{
			//Dependant on which search type is selected perform a page
			//transistion to put the user on the correct page. Evaluate
			//the cost search property of the control.
			if (searchTypeControl.CostSearch)
			{
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindTrainCostInputDefault;
			}

			// Set one use key to indicate to time based page that we have come
			// from cost based planning.
			TDSessionManager.Current.SetOneUseKey(SessionKey.TransferredFromTimeBasedPlanning, "true");
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
            journeyParams.DestinationType = locationsControl.ToLocationControl.LocationControlType;
        }

        /// <summary>
        /// Event handler called when new location button is clicked for the "via" location.
        /// The journey parameters are updated with the "via" location control's new values.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void preferencesControl_NewLocation(object sender, EventArgs e) 
        {
            journeyParams.PublicVia = preferencesControl.ViaLocationControl.TheSearch;
            journeyParams.PublicViaLocation = preferencesControl.ViaLocationControl.TheLocation;
            journeyParams.PublicViaType = preferencesControl.ViaLocationControl.LocationControlType;
        }

        /// <summary>
        /// Event handler called when clear page button is clicked. Journey parameters are reset
        /// to initial values, page controls updated and the page set to input mode.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void preferencesControl_Clear(object sender, EventArgs e) 
        {
            findInputAdapter.InitJourneyParameters();
            journeyParams.PublicModes = new ModeType[] {ModeType.Rail};
            pageState.AmbiguityMode = false;
            findInputAdapter.InitialiseControls(preferencesControl, locationsControl);
            dateControl.LeaveDateControl.DateErrors = null;
            dateControl.LeaveDateControl.AmbiguityMode = false;
            dateControl.ReturnDateControl.DateErrors = null;
            dateControl.ReturnDateControl.AmbiguityMode = false;
            TDSessionManager.Current.ValidationError = null;
            TDPage.CloseAllSingleWindows(Page);
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

		/// <summary>
		/// Event Handler for back button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">args</param>
		private void commandBack_Click(object sender, EventArgs e)
		{
			preferencesControl_Back(sender, e);
		}

        #endregion Event Handlers

     
		#region Private Methods

		/// <summary>
		/// Method containing the preferencesControl_Submit method functionality so that it
		/// can be called by the Landing page when AutoPlanning is required. 
		/// </summary>
		private void SubmitRequest()
		{
            // Must be done here to ensure if user was Amending journey -> changed a location ->
            // and it is ambiguous, then make the valid location resolved
            SetLocationsResolved();

			dateControl.CalendarClose();
			if (preferencesControl.PreferencesOptionsControl.SavePreferences) 
			{
				findInputAdapter.SaveTravelDetails();
			}

			if (!pageState.AmbiguityMode) 
			{
				pageState.SaveJourneyParameters(journeyParams);
				findInputAdapter.AmbiguitySearch(locationsControl, preferencesControl);
			} 
			else 
			{
				locationsControl.FromLocationControl.Search();
				locationsControl.ToLocationControl.Search();
				preferencesControl.ViaLocationControl.Search();
			}

			findInputAdapter.InitialiseAsyncCallState();

			// Validate the JourneyParameters
			JourneyPlanRunner.JourneyPlanRunner runner = new JourneyPlanRunner.JourneyPlanRunner(Global.tdResourceManager);

			pageState.AmbiguityMode = !runner.ValidateAndRun(
				TDSessionManager.Current, 
				TDSessionManager.Current.JourneyParameters, 
				GetChannelLanguage(TDPage.SessionChannelName),
				true);

			if (pageState.AmbiguityMode) 
			{
				if (!TDSessionManager.Current.Session[SessionKey.LandingPageAutoPlan]) 
				{
					preferencesControl.ChangesDisplayMode = GenericDisplayMode.ReadOnly;
					preferencesControl.ChangesSpeedDisplayMode = GenericDisplayMode.ReadOnly;
					dateControl.LeaveDateControl.DateErrors = TDSessionManager.Current.ValidationError;
					dateControl.ReturnDateControl.DateErrors = TDSessionManager.Current.ValidationError;
					dateControl.LeaveDateControl.AmbiguityMode = true;
					dateControl.ReturnDateControl.AmbiguityMode = true;
				}
				else 
				{
					pageState.AmbiguityMode = false;
				}
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
		}



        /// <summary>
        /// Updates session data with control values for those controls that do not raise events
        /// to signal that user has changed values (i.e. the date controls)
        /// </summary>
        private void UpdateOtherControls() 
        {
            pageState = (FindTrainPageState)TDSessionManager.Current.FindPageState;
            journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;            
            findInputAdapter = new FindTrainInputAdapter(journeyParams,pageState, inputPageState);
			inputPageState = TDSessionManager.Current.InputPageState;

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

			ITDSessionManager sessionManager = TDSessionManager.Current;
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

            #region Clear cache of journey data
            // If an extension is in progress, cancel it
			sm.ItineraryManager.CancelExtension();
                        
            ClearCacheHelper helper = new ClearCacheHelper();

            // Force clear of any printable information if added by the journey result page
            helper.ClearPrintableResultCache(TDSessionPartition.TimeBased);

            // Fix for IR5481 Session issue when going from between different planners using the left hand menu
            if ((sessionManager.FindAMode != FindAMode.Train && sessionManager.FindAMode != FindAMode.RailCost) && sessionManager.ItineraryMode != ItineraryManagerMode.None)
            {
                // We have come directly from another planner so clear results from session.
                helper.ClearJourneyResultCache();
            }
            #endregion

            // No reset is required if coming from landing page, since the journey parameters 
			// have already been initialised.
			if (sm.Session[ SessionKey.LandingPageCheck ])
			{
				resetDone = false;
			}
			else
			{
				// Initialise page state and journey parameter objects in session data
				resetDone = sm.InitialiseJourneyParameters(FindAMode.Train);
			}

			pageState = (FindTrainPageState)sm.FindPageState; 
			journeyParams = sm.JourneyParameters as TDJourneyParametersMulti;		
			inputPageState = sm.InputPageState;

			findInputAdapter = new FindTrainInputAdapter(journeyParams, pageState, inputPageState);
            
			if (resetDone)          
			{
				// if page state and journey parameters were re-instantiated, initialise to correct state
				// for this find A mode.
				findInputAdapter.InitJourneyParameters();
			}

			if (sm.GetOneUseKey(SessionKey.TransferredFromCostBasedPlanning) == null)
			{
				// initialise the date control from session (required as displaying help forces redirect)
				findInputAdapter.InitDateControl(dateControl);
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
                if ((journeyParams.OriginLocation != null) && (journeyParams.OriginLocation.SearchType != SearchType.FindNearest))
                    locationsControl.FromLocationControl.AmendMode = pageState.AmendMode;

                if ((journeyParams.DestinationLocation != null) && (journeyParams.DestinationLocation.SearchType != SearchType.FindNearest))
                    locationsControl.ToLocationControl.AmendMode = pageState.AmendMode;
            }
        }

        /// <summary>
        /// Sets the From and To locations into Resolved mode if they are valid
        /// Only done in Submit request because in case we go to ambiguity page, need to ensure we show a resolved location
        /// </summary>
        private void SetLocationsResolved()
        {
            if ((journeyParams.OriginLocation != null) && (journeyParams.OriginLocation.Status == TDLocationStatus.Valid))
            {
                locationsControl.FromLocationControl.AmendMode = false;
            }

            if ((journeyParams.DestinationLocation != null) && (journeyParams.DestinationLocation.Status == TDLocationStatus.Valid))
            {
                locationsControl.ToLocationControl.AmendMode = false;
            }
        }

		#endregion Private Methods

	}
}
