// *********************************************** 
// NAME				 : VisitPlannerInput.aspx.cs
// AUTHOR			 : Andrew Sinclair
// DATE CREATED		 : 2005-09-15
// DESCRIPTION		 : Journey Input Page for VisitPlanner
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/VisitPlannerInput.aspx.cs-arc  $
//
//   Rev 1.21   Mar 08 2013 11:18:56   mmodi
//Updates to hide the via location control
//Resolution for 5895: Error displayed when clicked on Advanced options button
//
//   Rev 1.20   Jul 28 2011 16:20:58   DLane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.19   Jul 29 2010 16:14:10   mmodi
//Changes to page layout and styles to be exactly consistent for all input pages in the Portal
//Resolution for 4760: IE7-find a car route check boxes
//
//   Rev 1.18   May 13 2010 13:05:30   mmodi
//Added code call to clear the printable map session information 
//Resolution for 5535: Printable maps session logic improvement
//
//   Rev 1.17   Jan 29 2010 14:45:34   mmodi
//Updated to reset CycleResult to correct error when planning journey after coming directly from Cycle journey details
//Resolution for 5388: Cycle Planner - Server error when planning Door to door after a cycle journey
//
//   Rev 1.16   Jan 19 2010 13:21:06   mmodi
//Updated for New Mapping RFC73 patch
//Resolution for 5376: Maps - ESRI RFC073 mandatory changes needed for patch
//
//   Rev 1.15   Dec 15 2009 08:45:30   apatel
//resolved the issue with visitlocation2 control getting the same value as visitlocation1 control when location resolved by clicking on findonmap button
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.14   Dec 14 2009 11:06:18   apatel
//stop the map showing when new location button click after amend
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.13   Dec 09 2009 11:34:06   mmodi
//When Clear button is clicked, reset the map
//
//   Rev 1.12   Dec 02 2009 11:54:14   apatel
//Input page work flow change for mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.11   Nov 30 2009 09:58:26   apatel
//input page find on map workflow changed
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.10   Nov 18 2009 11:20:44   apatel
//Updated for Journey input planner mapping enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.9   Nov 10 2009 11:30:26   apatel
//Find Input pages mapping enhancement changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.8   Jan 30 2009 10:44:24   apatel
//Search Engine Optimasation changes - CCN 624
//Resolution for 5229: Search Engin Optimisation Changes -  CCN624
//
//   Rev 1.7   Dec 15 2008 12:46:44   devfactory
//XHTML Compliance changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.6   Oct 24 2008 14:53:26   mmodi
//Set Find Page state to null
//Resolution for 5150: Cycle Planner - 'Server Error in '/Web2' Application' page is displayed when user clicks on 'Find a cycle' link on the left side menu of 'Day trip planner - Input page'
//
//   Rev 1.5   May 02 2008 14:30:58   apatel
//make controls aligned
//
//   Rev 1.4   May 01 2008 17:23:12   mmodi
//Updated to display session timeout error
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.3   Apr 08 2008 15:55:20   scraddock
//Advanced Text Not Hidden
//Resolution for 4847: Advanced Text Not Hidden
//
// rev devfactory 8 apr sbarker
// Advanced text hidden when advanced pressed, or in ambiguity mode
//
//   Rev 1.2   Mar 31 2008 13:25:52   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:32:02   mturner
//Initial revision.
//
//DEVFACTORY FEB 21 2008 sbarker
//Page icon added
//
//   Rev 1.30   Jun 07 2007 15:19:44   mmodi
//Added a new Next submit button
//Resolution for 4409: Minor Portal Changes
//
//   Rev 1.29   Apr 27 2006 11:20:50   mtillett
//Prevent calendar button dislpay on ambiguity page after next or back buttons clicked
//Resolution for 3510: Apps: Calendr Control problems on input/ambiguity screen
//
//   Rev 1.28   Mar 13 2006 10:38:12   CRees
//Fix for IR3556 - added check to LoadUserPrefences for Amend pagestate; also resolved problem where clear button reset to system defaults, not user defaults.
//
//   Rev 1.27   Feb 24 2006 10:17:34   rgreenwood
//stream3129: Manual merge changes
//
//   Rev 1.26   Feb 10 2006 11:22:18   aviitanen
//Manual merge for Homepage phase 2 (stream3180)
//
//   Rev 1.25   Jan 24 2006 13:57:22   jbroome
//Ensures that default control type is set when clicking Back. 
//Resolution for 3476: Visit Planner: Locations have a red error border when Advanced options selected
//
//   Rev 1.24   Jan 23 2006 17:54:30   asinclair
//Added check to Page_Load to clear itinerary in certain situations
//Resolution for 3498: Visit Planner: in certain cases, amend journey clears out journey details
//
//   Rev 1.23   Jan 04 2006 10:06:24   tolomolaiye
//Updates folllowing Visit Planner code review
//
//   Rev 1.22   Dec 14 2005 11:36:52   jbroome
//Updated page title strings
//Resolution for 3315: VisitPlanner: Wrong Header Title
//
//   Rev 1.21   Dec 06 2005 17:18:10   pcross
//Changed browser title to be retrieved on page load from resource string file as usual
//Resolution for 3315: VisitPlanner: Wrong Header Title
//
//   Rev 1.20   Nov 25 2005 14:27:54   tolomolaiye
//Allow user preferences to be saved. Fix for IR 3176
//Resolution for 3167: Visit Planner - Using clear after choosing to amend should plan journey as if for the first time
//Resolution for 3176: Visit Planner  - When a logged-in user saves their Advanced preferences the options chosen are not saved
//
//   Rev 1.19   Nov 24 2005 10:13:08   tolomolaiye
//Fix for IR 3167
//
//   Rev 1.18   Nov 22 2005 17:35:40   tolomolaiye
//Fix for IR 3155
//Resolution for 3155: Visit Planner - Default Return to starting location option
//
//   Rev 1.17   Nov 17 2005 17:25:14   asinclair
//Added LoadPreferences and also changed the way the Find on map click events worked
//
//   Rev 1.16   Nov 17 2005 09:12:58   asinclair
//Fixes for VP IRs
//Resolution for 2954: Visit Planner (CG): Resolved locations wrongly need to be re-resolvedx after amend journey
//Resolution for 2961: Visit Planner - Dates and times in the past can be entered in the Input Page
//Resolution for 2962: Visit Planner - Invalid dates entered in Input Page cause server error
//Resolution for 3013: Visit Planner: Clicking New Location does not clear correctly
//Resolution for 3071: Visit Planner - Manchester (Any Rail / Coach) not found by journey input scren
//
//   Rev 1.15   Nov 11 2005 13:33:16   tolomolaiye
//Allow enter key to be used as the next button for Visit Planner
//Resolution for 2956: Visit Planner (CG): DEL 7.2 Using enter key for Next button doesn't work
//
//   Rev 1.14   Nov 10 2005 17:52:38   asinclair
//Updated to fix bugs
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.13   Nov 09 2005 14:11:48   NMoorhouse
//TD93 - UEE Input Pages - Update Visit Planner
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.12   Nov 07 2005 10:59:52   AViitanen
//In TD089, ES020, Manual merge changes for html buttons.
//
//   Rev 1.11   Nov 07 2005 09:58:36   tolomolaiye
//Modifications for Visit Planner
//
//   Rev 1.10   Oct 29 2005 20:07:40   asinclair
//Added ErrorDisplayControl
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.9   Oct 29 2005 11:49:08   asinclair
//Added Map click events and methods
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.8   Oct 26 2005 14:42:06   tolomolaiye
//Added property for drop down list
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.7   Oct 25 2005 11:32:22   jbroome
//Updated to fix some outstanding unit test issues
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.5   Oct 20 2005 21:36:18   asinclair
//Set the help url in pageload
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.4   Oct 20 2005 10:14:08   asinclair
//Work in progress
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.3   Oct 12 2005 11:26:10   asinclair
//Work in progress: Check in to allow journey planning testing
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Oct 11 2005 10:17:56   asinclair
//Check in to integrate front and back end code
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Sep 16 2005 15:29:54   asinclair
//Initial revision.
//Resolution for 2638: DEL 8 Stream: Visit Planner
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
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.Web.Adapters;

using TransportDirect.Web.Support;

using TransportDirect.JourneyPlanning.CJPInterface;

using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;
using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
using ControlPopulator = TransportDirect.UserPortal.DataServices.DataServices;
using System.Collections.Generic;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for VisitPlannerInput.
	/// </summary>
	public partial class VisitPlannerInput : TDPage
	{

		protected TransportDirect.UserPortal.Web.Controls.VisitPlannerLocationControl visitPlannerLocationOrigin;
		protected TransportDirect.UserPortal.Web.Controls.VisitPlannerLocationControl visitPlannerLocationVisitPlace1;
		protected TransportDirect.UserPortal.Web.Controls.VisitPlannerLocationControl visitPlannerLocationVisitPlace2;
		protected TransportDirect.UserPortal.Web.Controls.FindPageOptionsControl findPageOptionsControl;
		protected TransportDirect.UserPortal.Web.Controls.TransportTypesControl transportTypesControl;
		protected TransportDirect.UserPortal.Web.Controls.PtPreferencesControl ptpreferencesControl;
		protected TransportDirect.UserPortal.Web.Controls.ErrorDisplayControl errorDisplayControl;
		protected TransportDirect.UserPortal.Web.Controls.FindLeaveReturnDatesControl dateControl;
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
		protected System.Web.UI.WebControls.Label labelErrorMessage;

		private ControlPopulator populator;


		// Location objects
		private TDLocation originLocation;
		private TDLocation visit1Location;
		private TDLocation visit2Location;

		private LocationSearch originLocationSearch;
		private LocationSearch visit1LocationSearch;
		private LocationSearch visit2LocationSearch;

		private LocationSelectControlType originLocationSelectControlType;
		private LocationSelectControlType visit1LocationSelectControlType;
		private LocationSelectControlType visit2LocationSelectControlType;

		private InputPageState pageState;

		private TDJourneyParametersVisitPlan parameters;

		/// <summary>
		/// Helper class responsible for common date methods to non-Find A pages
		/// </summary>
		private LeaveReturnDatesControlAdapter inputDateAdapter;

		/// <summary>
		/// Helper class responsible for common methods to VisitPlannerPages
		/// </summary>
		private VisitPlannerAdapter visitPlannerAdapter;

		private FindDateValidation findDateValidation;
		private TDItineraryManager itinerary;
		private InputAdapter visitInputAdapter = new InputAdapter();
		
		/// <summary>
		/// Constructor - sets the page id and local resource manager
		/// </summary>
		public VisitPlannerInput(): base()
		{
			pageId = PageId.VisitPlannerInput;
			this.LocalResourceManager = TDResourceManager.VISIT_PLANNER_RM;
			
			// Get DataServices from Service Discovery
			populator = (ControlPopulator)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
		}
									
		/// <summary>
		/// Initialises controls used on page
		/// </summary>
		private void InitialiseControls() 
		{
			ptpreferencesControl.JourneyChangesOptionsControl.Changes = parameters.PublicAlgorithmType;
			ptpreferencesControl.JourneyChangesOptionsControl.ChangesSpeed = parameters.InterchangeSpeed;
			ptpreferencesControl.WalkingSpeedOptionsControl.WalkingDuration = parameters.MaxWalkingTime;
			ptpreferencesControl.WalkingSpeedOptionsControl.WalkingSpeed = parameters.WalkingSpeed;

		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			//get the title of the page
			PageTitle = GetResource("VisitPlanner.AppendPageTitle") + GetResource("VisitPlanner.DefaultPageTitle");

            #region Display Session Errors
            // Check if there are any errors in the InputPageState and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControlSession, TDSessionManager.Current.InputPageState.InputSessionErrors);

            // Clear the error messages
            TDSessionManager.Current.InputPageState.InputSessionErrors = null;
            #endregion

			//hide error control
            panelErrorDisplayControl2.Visible = false;
			errorDisplayControl.Visible = false;

			ITDSessionManager sessionManager = TDSessionManager.Current;

            if (!Page.IsPostBack)
            {
                #region Clear cache of journey data

                ClearCacheHelper helper = new ClearCacheHelper();

                // Force clear of any printable information if added by the journey result page
                helper.ClearPrintableResultCache(TDSessionPartition.TimeBased);

                // Fix for IR5481 Session issue when going from between different planners using the left hand menu
                if (sessionManager.FindPageState != null)
                {
                    // We have come directly from another planner so clear results from session.
                    helper.ClearJourneyResultCache();
                }

                #endregion
            }

			sessionManager.ItineraryMode = ItineraryManagerMode.VisitPlanner;
            
            // Force the Page state to be null. Visit planner does not use or change the page state, and this prevents 
            // potential errors when switching between a Find A planner and Visit planner
            sessionManager.FindPageState = null;
                        
			visitPlannerAdapter = new VisitPlannerAdapter();
			findDateValidation = new FindDateValidation();
			ResultsPageState resultsPageState = new ResultsPageState();

			inputDateAdapter = new LeaveReturnDatesControlAdapter();

			pageState = sessionManager.InputPageState;

			parameters = sessionManager.JourneyParameters as TDJourneyParametersVisitPlan;

			if (!Page.IsPostBack && !pageState.VisitAmendMode && parameters == null && (TDSessionManager.Current.GetOneUseKey(SessionKey.JourneyPlannerInputToMap) == null))
			{
				TDItineraryManager.Current.NewSearch(); 
			}

			if (parameters == null)
			{
				parameters = new TDJourneyParametersVisitPlan();
				parameters.ReturnToOrigin = true;
				sessionManager.JourneyParameters = parameters;
			}

			//save some settings if user is doing a postback
			if (Page.IsPostBack)
			{
				SaveCurrentSettings();
			}
			else
			{
				LoadUserPreferences();
			}

			LoadSessionVariables();	

			InitialiseControls();

            //Page title image:
            imageVisitPlanner.ImageUrl = GetResource("PlanAJourney.imageVisitPlanner.ImageUrl");
            imageVisitPlanner.AlternateText = " ";

			//If in AmendMode, then we need to display  the location controls to allow user input, but
			//we do not want to lose any Resolved Locations.
			visitPlannerLocationOrigin.LocationControl.AmendMode = pageState.VisitAmendMode;
			visitPlannerLocationVisitPlace1.LocationControl.AmendMode = pageState.VisitAmendMode;
			visitPlannerLocationVisitPlace2.LocationControl.AmendMode = pageState.VisitAmendMode;   
			
			if(pageState.VisitAmendMode)
			{
				parameters.GetLocationType(0).Type = TDJourneyParameters.ControlType.Default; 
				parameters.GetLocationType(1).Type = TDJourneyParameters.ControlType.Default; 
				parameters.GetLocationType(2).Type = TDJourneyParameters.ControlType.Default; 
			}

			itinerary = VisitPlannerItineraryManager.Current;
	
			visitPlannerLocationOrigin.Populate(DataServiceType.VisitPlannerLocationSelection, CurrentLocationType.VisitPlannerOrigin, ref originLocationSearch, ref originLocation, ref originLocationSelectControlType, false, true, true, Page.IsPostBack);
			
			visitPlannerLocationVisitPlace1.Populate(DataServiceType.VisitPlannerLocationSelection, CurrentLocationType.VisitPlannerVisitPlace1, ref visit1LocationSearch, ref visit1Location, ref visit1LocationSelectControlType, false, true, true, Page.IsPostBack);
			visitPlannerLocationVisitPlace2.Populate(DataServiceType.VisitPlannerLocationSelection, CurrentLocationType.VisitPlannerVisitPlace2, ref visit2LocationSearch, ref visit2Location, ref visit2LocationSelectControlType, false, true, true, Page.IsPostBack);
			
			visitPlannerLocationVisitPlace1.StayLengthValue = (int)parameters.GetStayDuration(0) / 60;
			visitPlannerLocationVisitPlace2.StayLengthValue = (int)parameters.GetStayDuration(1) / 60;

			visitPlannerLocationOrigin.ReturnToOrigin.Checked = parameters.ReturnToOrigin;

            populator.LoadListControl(DataServiceType.PublicTransportsCheck, transportTypesControl.ModesPublicTransport);

            if(Page.IsPostBack)
			{
				// Update date params if not in ambig mode, or in ambig mode with date errors
				if (!pageState.AmbiguityMode || (pageState.AmbiguityMode && FindDateValidation.IsAnyOutwardError))
				{
					inputDateAdapter.UpdateJourneyDates(dateControl, pageState.AmbiguityMode, parameters, TDSessionManager.Current.ValidationError);
				}
			}

			if (parameters.PublicModes == null)
			{
				parameters.PublicModes = transportTypesControl.PublicModes;
			}

			helpControl.HelpUrl = GetResource ("VisitPlannerInput.VisitPlannerInputHelp");
			
			dateControl.LeaveDateControl.ArriveByOption = false;

			//Adding client side script for user navigation (when user hits enter, it should take the default action)
			UserExperienceEnhancementHelper.AddClientForUserNavigationDefaultAction(this.Page);

            //Added for white labelling:
            ConfigureLeftMenu(expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextVisitPlannerInput);
            expandableMenuControl.AddExpandedCategory("Related links");
            TDResourceManager rm = Global.tdResourceManager;
            commandBack.Text = rm.GetString("VisitPlannerInput.CommandBack.Text", TDCultureInfo.CurrentUICulture); 
		}

		private void LoadSessionVariables()
		{
			originLocation = parameters.GetLocation(0);
			visit1Location = parameters.GetLocation(1);
			visit2Location = parameters.GetLocation(2);

			originLocationSearch = parameters.GetLocationSearch(0);
			visit1LocationSearch = parameters.GetLocationSearch(1);
			visit2LocationSearch = parameters.GetLocationSearch(2);

			originLocationSelectControlType = parameters.GetLocationType(0);
			visit1LocationSelectControlType = parameters.GetLocationType(1);
			visit2LocationSelectControlType = parameters.GetLocationType(2);
		}

		/// <summary>
		/// Loads user preferences for Advanced options for logged on users
		/// </summary>
		private void LoadUserPreferences()
		{
			// IR 3556 - added check to disable loading user preferences when in amend mode.
			if (TDSessionManager.Current.Authenticated && !pageState.VisitAmendMode)
			{
				UserPreferencesHelper.LoadPublicTransportPreferences( parameters);

				ptpreferencesControl.WalkingSpeedOptionsControl.WalkingSpeed = parameters.WalkingSpeed;
				ptpreferencesControl.WalkingSpeedOptionsControl.WalkingDuration = parameters.MaxWalkingTime;
				ptpreferencesControl.JourneyChangesOptionsControl.ChangesSpeed = parameters.InterchangeSpeed;
				ptpreferencesControl.JourneyChangesOptionsControl.Changes = parameters.PublicAlgorithmType;				
			}

			
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			ExtraEventWireUp();
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
		/// Extra event subscription
		/// </summary>
		private void ExtraEventWireUp()
		{
            // CCN 0427 setting page option control's event in WalkingSpeedOptionsControl
            FindPageOptionsControl bottomPageOptions = ptpreferencesControl.WalkingSpeedOptionsControl.PageOptionsControl;
            bottomPageOptions.ShowAdvancedOptions += new EventHandler(Advanced_CommandEventHandler);
            bottomPageOptions.HideAdvancedOptions += new EventHandler(Advanced_CommandEventHandler);
            bottomPageOptions.Submit += new EventHandler(Next_CommandEventHandler);
            bottomPageOptions.Clear += new EventHandler(NewClear_CommandEventHandler);
			
            
			findPageOptionsControl.ShowAdvancedOptions += new EventHandler(Advanced_CommandEventHandler);
			findPageOptionsControl.HideAdvancedOptions += new EventHandler(Advanced_CommandEventHandler);
			findPageOptionsControl.Submit += new EventHandler(Next_CommandEventHandler);
			findPageOptionsControl.Clear += new EventHandler(NewClear_CommandEventHandler);
			findPageOptionsControl.Back += new EventHandler(Back_CommandEventHandler);
			transportTypesControl.ModesPublicTransport.SelectedIndexChanged += new EventHandler(ModesPublicTransport_SelectedIndexChanged);
			visitPlannerLocationOrigin.LocationControl.MapClick += new EventHandler(MapOriginClick);
			visitPlannerLocationVisitPlace1.LocationControl.MapClick += new EventHandler(MapLocation1Click);
			visitPlannerLocationVisitPlace2.LocationControl.MapClick += new EventHandler(MapLocation2Click);
			dateControl.LeaveDateControl.DateChanged += new EventHandler(dateControlLeaveDateControl_DateChanged);
		
			visitPlannerLocationOrigin.LocationControl.NewLocation += new EventHandler(newOrigin);
			visitPlannerLocationVisitPlace1.LocationControl.NewLocation += new EventHandler(newVisitPlace1);
			visitPlannerLocationVisitPlace2.LocationControl.NewLocation += new EventHandler(newVisitPlace2);

            commandBack.Click += new EventHandler(Back_CommandEventHandler);

			//commandSubmit.Click += new EventHandler(Next_CommandEventHandler);

			headerControl.DefaultActionEvent += new EventHandler(this.Next_CommandEventHandler);
		}

		/// <summary>
		/// OnPreRender event
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			//Determine which populate methods are called, and therefore controls displayed
			//If the page is not in Ambiguity Mode
			if (!pageState.AmbiguityMode)
			{
				PopulateVisibleControls();
				ShowAndPopulateInputControl();
			}
			else //The page is in Ambiguity Mode
			{
				PopulateVisibleControls();
				ShowAndPopulateAmbiguityControl();
                findPageOptionsControl.Visible = true;
			}
			
			//If in AmendMode, then we need to display  the location controls to allow user input, but
			//we do not want to lose any Resolved Locations.
			visitPlannerLocationOrigin.LocationControl.AmendMode = pageState.VisitAmendMode;
			visitPlannerLocationVisitPlace1.LocationControl.AmendMode = pageState.VisitAmendMode;
			visitPlannerLocationVisitPlace2.LocationControl.AmendMode = pageState.VisitAmendMode;   
			
			if(pageState.VisitAmendMode)
			{
				parameters.GetLocationType(0).Type = TDJourneyParameters.ControlType.Default; 
				parameters.GetLocationType(1).Type = TDJourneyParameters.ControlType.Default; 
				parameters.GetLocationType(2).Type = TDJourneyParameters.ControlType.Default; 
			}

			//Populate Date Control
			inputDateAdapter.UpdateDateControl(dateControl, pageState.AmbiguityMode, parameters, TDSessionManager.Current.ValidationError);
            commandBack.Visible = pageState.AmbiguityMode;
            findPageOptionsControl.AllowBack = false;

			base.OnPreRender(e);

            //Hide the advanced text if required. Note that this must be done
            //after the base call of the method, since it is in there that
            //the panel text is initially populated.
            if (ptpreferencesControl.PreferencesVisible || pageState.AmbiguityMode)
            {
                BlankPanelText(TDPageInformationHtmlPlaceHolderDefinition);
            }

            SetupMap();
		}

		/// <summary>
		/// Method to populate controls that are always on the page
		/// </summary>
		private void PopulateVisibleControls()
		{	
			visitPlannerLocationOrigin.LocationType = CurrentLocationType.VisitPlannerOrigin;
			visitPlannerLocationVisitPlace1.LocationType = CurrentLocationType.VisitPlannerVisitPlace1;
			visitPlannerLocationVisitPlace2.LocationType = CurrentLocationType.VisitPlannerVisitPlace2;
		}

		/// <summary>
		/// Populate controls on page in Ambiguity mode
		/// </summary>
		private void ShowAndPopulateAmbiguityControl()
		{

			//Display the ErrorDisplayControl if it has been populated with error message
			if(errorDisplayControl.ErrorStrings.Length > 0)
			{
                panelErrorDisplayControl2.Visible = true;
				errorDisplayControl.Visible = true;
			}

			findPageOptionsControl.AllowClear = false;

			//Set title and instructional text for ambiguity
			labelVisitPlannerTitle.Text = GetResource ("VisitPlannerInput.PageHeader.Ambiguity");
			labelVisitPlannerInstructional.Text = GetResource ("VisitPlannerInput.Instructional.Ambiguity");

			visitPlannerLocationVisitPlace1.LenghtOfStay.SelectedIndex = (parameters.GetStayDuration(0)/60)-1;
			visitPlannerLocationVisitPlace2.LenghtOfStay.SelectedIndex = (parameters.GetStayDuration(1)/60)-1;

			//Sets the visibility of the transportTypesControl and the ptPreferencesControl
			panelTransportTypes.Visible = false;
			ptpreferencesControl.PreferencesVisible = false;

			//Displays the back button
			findPageOptionsControl.AllowBack = true;

            //return to origin checkbox is not visible if no ambiguity for Origin
			if(visitPlannerLocationOrigin.LocationControl.Status == TDLocationStatus.Valid)
			{
				visitPlannerLocationOrigin.ReturnToOrigin.Visible = false;	
			}
		}

		/// <summary>
		/// Populate the controls when page is in Input mode (i.e. AmbiguityMode is false)
		/// </summary>
		private void ShowAndPopulateInputControl()
		{
			//TDJourneyParameters parameters = TDSessionManager.Current.JourneyParameters;
            panelErrorDisplayControl2.Visible = false;
			errorDisplayControl.Visible = false;

			//Set title and instructional text for input mode
			labelVisitPlannerTitle.Text = GetResource ("VisitPlannerInput.PageHeader");
			labelVisitPlannerInstructional.Text = GetResource ("VisitPlannerInput.Instructional");			

			TDResourceManager rm = Global.tdResourceManager;

			//Populate the checkbox list within TransportTypesControl
			transportTypesControl.PublicModes = parameters.PublicModes;
 
			//The PTPreferencesControl is passed to the adapter method and populated.
			visitInputAdapter.InitialisePTPreferencesControl(ptpreferencesControl);
			visitInputAdapter.PopulatePTPreferencesControl(ptpreferencesControl, parameters);
			
			//Sets the visability of the controls on the ptPreferencesControl as not all are used by VP
			ptpreferencesControl.JourneyChangesOptionsControl.Visible = true;
			ptpreferencesControl.WalkingSpeedOptionsControl.Visible = true;
			ptpreferencesControl.ShowViaLocationControl = false;
			ptpreferencesControl.PreferencesVisible = true;
            // CCN 0427 Setting pageoptioncontrols in walkingspeedoptionscontrol.
            ptpreferencesControl.WalkingSpeedOptionsControl.PageOptionsControl.AllowHideAdvancedOptions = true;

            

			//Sets the visability of the transportTypesControl and the ptPreferencesControl
			findPageOptionsControl.AllowShowAdvancedOptions = !pageState.AdvancedOptionsVisible;
			labelAdvanced.Visible = pageState.AdvancedOptionsVisible;
			panelTransportTypes.Visible = pageState.AdvancedOptionsVisible;
			ptpreferencesControl.PreferencesVisible = pageState.AdvancedOptionsVisible;
			findPageOptionsControl.AllowHideAdvancedOptions = pageState.AdvancedOptionsVisible;
			
            labelAdvanced.Text = GetResource ("VisitPlannerInput.AdvancedOptions");

			//commandSubmit.Text = GetResource("VisitPlannerInput.Submit");
            if (pageState.AdvancedOptionsVisible)
            {
                findPageOptionsControl.AllowShowAdvancedOptions = false;
                findPageOptionsControl.AllowClear = false;
            }
            else
            {
                findPageOptionsControl.AllowShowAdvancedOptions = true;
                findPageOptionsControl.AllowClear = true;
            }

            findPageOptionsControl.AllowHideAdvancedOptions = false;
            //commandSubmit.Visible = pageState.AdvancedOptionsVisible;
		}

		/// <summary>
		/// Saves current values entered by the user. Useful when doing a postback
		/// </summary>
		private void SaveCurrentSettings() 
		{
			int duration1 = (visitPlannerLocationVisitPlace1.LenghtOfStay.SelectedIndex + 1)*60;
			int duration2 = (visitPlannerLocationVisitPlace2.LenghtOfStay.SelectedIndex + 1)*60;

			parameters.SetStayDuration(0, duration1);
			parameters.SetStayDuration(1, duration2);

			parameters.ReturnToOrigin = visitPlannerLocationOrigin.ReturnToOrigin.Checked;

			parameters.WalkingSpeed = ptpreferencesControl.WalkingSpeedOptionsControl.WalkingSpeed;
			parameters.MaxWalkingTime = ptpreferencesControl.WalkingSpeedOptionsControl.WalkingDuration;
			parameters.InterchangeSpeed = ptpreferencesControl.JourneyChangesOptionsControl.ChangesSpeed;
			parameters.PublicAlgorithmType = ptpreferencesControl.JourneyChangesOptionsControl.Changes;
			parameters.PublicModes = transportTypesControl.PublicModes;

			//save user preferences if the user is logged in
			if (ptpreferencesControl.SavePreferences)
			{
				UserPreferencesHelper.SavePublicTransportPreferences(parameters);
			}
		}

		/// <summary>
		/// Handles the ShowPreferences event of the FindPageOptionsControl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Advanced_CommandEventHandler(object sender, EventArgs e)
		{
			pageState.AdvancedOptionsVisible = !pageState.AdvancedOptionsVisible;

            //findPageOptionsControl.Visible = !pageState.AdvancedOptionsVisible;
		}

		/// <summary>
		/// Handles the Amend journey button click event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Amend_CommandEventHandler(object sender, EventArgs e)
		{
			//Set AmbiguityMode to false
			pageState.AmbiguityMode = false;

			//ReinstateJourneyParameters
			TDSessionManager.Current.AmbiguityResolution.ReinstateVisitJourneyParameters();

			//Sets the locations back to Unspecified
			visitPlannerLocationOrigin.SetLocationUnspecified();
			visitPlannerLocationVisitPlace1.SetLocationUnspecified();
			visitPlannerLocationVisitPlace2.SetLocationUnspecified();

			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.VisitPlannerAmend;
		}
	
		/// <summary>
		/// Handles the ShowPreferences event of the FindPageOptionsControl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Back_CommandEventHandler(object sender, EventArgs e)
		{	            
			dateControl.CalendarClose();

			//Set AmbiguityMode to false
			pageState.AmbiguityMode = false;

			dateControl.LeaveDateControl.AmbiguityMode = false;
			dateControl.ReturnDateControl.AmbiguityMode = false;
			dateControl.LeaveDateControl.DateErrors = null;
			dateControl.ReturnDateControl.DateErrors = null;

			LoadSessionVariables();

			if(originLocation.Status == TDLocationStatus.Valid)
			{
				originLocation = new TDLocation();
				parameters.SetLocation(0, originLocation); 
			}
            
			if(visit1Location.Status == TDLocationStatus.Valid)
			{
				visit1Location = new TDLocation();
				parameters.SetLocation(1, visit1Location); 
			}

			if(visit2Location.Status == TDLocationStatus.Valid)
			{
				visit2Location = new TDLocation();
				parameters.SetLocation(2, visit2Location); 
			}
			//Sets the locations back to Unspecified
			visitPlannerLocationOrigin.SetLocationUnspecified();
			visitPlannerLocationVisitPlace1.SetLocationUnspecified();
			visitPlannerLocationVisitPlace2.SetLocationUnspecified();

			// Set back to default location types
			parameters.SetLocationType(0, new LocationSelectControlType(ControlType.Default));
			parameters.SetLocationType(1, new LocationSelectControlType(ControlType.Default));
			parameters.SetLocationType(2, new LocationSelectControlType(ControlType.Default));
            

            // CCN 0427 Setting page option controls visibility
            //findPageOptionsControl.Visible = !pageState.AdvancedOptionsVisible;

			//Invoke the VisitPlannerInputBack transition event
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.VisitPlannerInputBack;	
		}

		/// <summary>
		/// Handles the Clear and New search event of the FindPageOptionsControl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NewClear_CommandEventHandler(object sender, EventArgs e)
		{
			//ResultsPageState resultsPageState = new ResultsPageState();
			ResultsPageState resultsPageState = TDSessionManager.Current.ResultsPageState;
			visitPlannerAdapter.ClearDownRequestAndResults(itinerary, resultsPageState);
			
			pageState.AmbiguityMode = false;


			//Advanced options are not visible
			pageState.AdvancedOptionsVisible = false;
            findPageOptionsControl.Visible = true;

			parameters = new TDJourneyParametersVisitPlan();
			TDSessionManager.Current.JourneyParameters = parameters;
			
			// IR 3556 added following line to force preferences to load on clear page:
			pageState.VisitAmendMode = false;
			LoadUserPreferences();
			// end IR 3556

			LoadSessionVariables();	

			// Repopulate transportTypesControl and update parameters with default values
			populator.LoadListControl(DataServiceType.PublicTransportsCheck, transportTypesControl.ModesPublicTransport);
			parameters.PublicModes = transportTypesControl.PublicModes;

			//Populate the VisitPlannerLocation controls
			visitPlannerLocationOrigin.SetLocationUnspecified();
			visitPlannerLocationVisitPlace1.SetLocationUnspecified();
			visitPlannerLocationVisitPlace2.SetLocationUnspecified();	

			ptpreferencesControl.JourneyChangesOptionsControl.Changes = parameters.PublicAlgorithmType;

			parameters.PublicAlgorithmType = PublicAlgorithmType.Default;

			// IR 3556 added following line to force preferences to load on clear page:
			LoadUserPreferences();
			// end IR 3556

			visitPlannerLocationVisitPlace1.StayLengthValue = (int)parameters.GetStayDuration(0) / 60;
			visitPlannerLocationVisitPlace2.StayLengthValue = (int)parameters.GetStayDuration(1) / 60;

			//Re-populate the location controls with the default values
			visitPlannerLocationOrigin.Populate(DataServiceType.VisitPlannerLocationSelection, CurrentLocationType.VisitPlannerOrigin, ref originLocationSearch, ref originLocation, ref originLocationSelectControlType, false, true, true, false);
			visitPlannerLocationVisitPlace1.Populate(DataServiceType.VisitPlannerLocationSelection, CurrentLocationType.VisitPlannerVisitPlace1, ref visit1LocationSearch, ref visit1Location, ref visit1LocationSelectControlType, false, true, true, false);
			visitPlannerLocationVisitPlace2.Populate(DataServiceType.VisitPlannerLocationSelection, CurrentLocationType.VisitPlannerVisitPlace2, ref visit2LocationSearch, ref visit2Location, ref visit2LocationSelectControlType, false, true, true, false);

			visitPlannerLocationOrigin.ReturnToOrigin.Checked = true;

            // Reset the Map locations to show on the map (use Origin as it should have been reset by now)
            pageState.MapLocation = parameters.OriginLocation;
            pageState.MapLocationSearch = parameters.Origin;

			//Invoke the VisitPlannerNewClear transition event
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.VisitPlannerNewClear;
		}

		/// <summary>
		/// Updates the origin control to accept input for a new location
		/// </summary>
		private void newOrigin(object sender, EventArgs e)
		{
            visitPlannerLocationOrigin.SetNewLocation();
            parameters.SetLocation(0, originLocation);
            parameters.SetLocationSearch(0, originLocationSearch);
            parameters.SetLocationType(0, new LocationSelectControlType(ControlType.Default));
		}

		/// <summary>
		/// Updates the Visit Place 1 control to accept input for a new location
		/// </summary>
		private void newVisitPlace1(object sender, EventArgs e)
		{
            
			visitPlannerLocationVisitPlace1.SetNewLocation();
            parameters.SetLocation(1, visit1Location);
            parameters.SetLocationSearch(1, visit1LocationSearch);
            parameters.SetLocationType(1, new LocationSelectControlType(ControlType.Default));
		}

		/// <summary>
		/// Updates the Visit Place 2 control to accept input for a new location
		/// </summary>
		private void newVisitPlace2(object sender, EventArgs e)
		{
			visitPlannerLocationVisitPlace2.SetNewLocation();
            parameters.SetLocation(2, visit2Location);
            parameters.SetLocationSearch(2, visit2LocationSearch);
            parameters.SetLocationType(2, new LocationSelectControlType(ControlType.Default));
		}

		/// <summary>
		/// Handles the Next button click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Next_CommandEventHandler(object sender, EventArgs e)
		{	
			dateControl.CalendarClose();

			pageState.VisitAmendMode = false;

			//Check for is valid naptan!!!!
			visitPlannerLocationOrigin.LocationControl.Search(true);
			visitPlannerLocationVisitPlace1.LocationControl.Search(true);
			visitPlannerLocationVisitPlace2.LocationControl.Search(true);

			//Check for return journey
			if(visitPlannerLocationOrigin.ReturnToOrigin.Checked)
			{
				parameters.ReturnToOrigin = true;
			}

			//Get the Public Modes used for this journey
			parameters.PublicModes = transportTypesControl.PublicModes;

			//Number of changes
			parameters.PublicAlgorithmType = ptpreferencesControl.JourneyChangesOptionsControl.Changes;
			
			//Speed for making changes
			parameters.InterchangeSpeed = ptpreferencesControl.JourneyChangesOptionsControl.ChangesSpeed;

			//Walking speed
			parameters.WalkingSpeed = ptpreferencesControl.WalkingSpeedOptionsControl.WalkingSpeed;

			//Time to walk
			parameters.MaxWalkingTime = ptpreferencesControl.WalkingSpeedOptionsControl.WalkingDuration;

			//page is in AmbiguityMode if ValidateAndSearch returns false
			pageState.AmbiguityMode = !visitPlannerAdapter.ValidateAndSearch();

			if(pageState.AmbiguityMode)
			{
                visitInputAdapter.PopulateErrorDisplayControl(errorDisplayControl, TDSessionManager.Current.ValidationError);
                panelErrorDisplayControl2.Visible = (errorDisplayControl.ErrorStrings != null) && (errorDisplayControl.ErrorStrings.Length > 0);
			}

			// Save journey parameters that may change during ambiguity resolution
			// so that the may be reinstated if the changes are cancelled
			TDSessionManager.Current.AmbiguityResolution = new AmbiguityResolutionState();
			TDSessionManager.Current.AmbiguityResolution.SaveVisitJourneyParameters();

			//If not Ambiguity, then invoke the VisitPlannerInputNext transition event
			//else populate the location controls for Ambiguity
			if (!pageState.AmbiguityMode)
			{
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.VisitPlannerInputNext;

			}
			else
			{
				visitPlannerLocationOrigin.LocationControl.AmendMode = pageState.VisitAmendMode;
				visitPlannerLocationVisitPlace1.LocationControl.AmendMode = pageState.VisitAmendMode;
				visitPlannerLocationVisitPlace2.LocationControl.AmendMode = pageState.VisitAmendMode; 

				visitPlannerLocationOrigin.Populate(DataServiceType.VisitPlannerLocationSelection, CurrentLocationType.VisitPlannerOrigin, ref originLocationSearch, ref originLocation, ref originLocationSelectControlType, false, true, true, true);
				visitPlannerLocationVisitPlace1.Populate(DataServiceType.VisitPlannerLocationSelection, CurrentLocationType.VisitPlannerVisitPlace1, ref visit1LocationSearch, ref visit1Location, ref visit1LocationSelectControlType, false, true, true, true);
				visitPlannerLocationVisitPlace2.Populate(DataServiceType.VisitPlannerLocationSelection, CurrentLocationType.VisitPlannerVisitPlace2, ref visit2LocationSearch, ref visit2Location, ref visit2LocationSelectControlType, false, true, true, true);

				// And hide the CommandSubmit button to prevent two Next buttons being shown
				//commandSubmit.Visible = false;
			}
		}

		/// <summary>
		/// Transport modes selection changed event handler
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">ImageClickEventArgs</param>
		private void ModesPublicTransport_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!pageState.TravelOptionsChanged.Contains(TravelOptionsChangedEnum.PublicTransport))
				pageState.TravelOptionsChanged.Add(TravelOptionsChangedEnum.PublicTransport);
		}

		/// <summary>
		/// Event handler called when date selected from calendar control. The journey parameters for the outward
		/// date are updated with the calendar date selection.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void dateControlLeaveDateControl_DateChanged(object sender, EventArgs e) 
		{
			parameters.OutwardDayOfMonth = dateControl.LeaveDateControl.DateControl.Day;
			parameters.OutwardMonthYear = dateControl.LeaveDateControl.DateControl.MonthYear;
		}

        /// <summary>
        /// Sets up a map for the location search and type of map location mode
        /// If map location mode is via map gets initialised with start, via and end mode.
        /// By default map gets initialised with start and end mode.
        /// </summary>
        private void SetupMap()
        {
            MapLocationPoint[] locationsToShow = GetMapLocationPoints();

            LocationSearch locationSearch = pageState.MapLocationSearch;
            TDLocation location = pageState.MapLocation;

            MapLocationMode[] mapModes = new MapLocationMode[3] { MapLocationMode.Start, MapLocationMode.Via, MapLocationMode.End };

            SearchType searchType = SearchType.Map;

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

            mapInputControl.Initialise(searchType, locationsToShow, mapModes);


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

            MapLocationPoint origin = mapHelper.GetMapLocationPoint(parameters.GetLocation(0), MapLocationSymbolType.Start, true, false);

            MapLocationPoint destination = mapHelper.GetMapLocationPoint(parameters.GetLocation(2), MapLocationSymbolType.End, true, false);

            MapLocationPoint via = mapHelper.GetMapLocationPoint(parameters.GetLocation(1), MapLocationSymbolType.Via, true, false);

            
            if (origin.MapLocationOSGR.IsValid)
            {
                mapLocationPoints.Add(origin);
            }

            if (destination.MapLocationOSGR.IsValid)
            {
                mapLocationPoints.Add(destination);
            }

            if (via.MapLocationOSGR.IsValid)
            {
                mapLocationPoints.Add(via);
            }

            return mapLocationPoints.ToArray();
        }


		/// <summary>
		/// VisitPlanner Origin Map click event hadler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MapOriginClick(object sender, EventArgs e)
		{
            bool shiftForm = false;

			pageState.MapType = CurrentLocationType.VisitPlannerOrigin;
			pageState.MapMode = CurrentMapMode.VisitPlannerLocationOrigin;

			pageState.JourneyInputReturnStack.Push(pageId);

			if (parameters.GetLocation(0).Status == TDLocationStatus.Unspecified )
			{
				mapSearch(originLocationSearch.InputText,  originLocationSearch.SearchType,  originLocationSearch.FuzzySearch, true, true);
                if (pageState.MapLocationSearch.InputText.Length == 0)
                {
                    pageState.MapLocationControlType.Type = ControlType.Default;
                }
                else if (pageState.MapLocation.Status == TDLocationStatus.Valid)
                {
                    originLocationSearch = pageState.MapLocationSearch;
                    originLocationSearch.LocationFixed = true;
                    originLocation = pageState.MapLocation;
                    parameters.OriginLocation = originLocation;
                    parameters.Origin = originLocationSearch;
                    visitPlannerLocationOrigin.LocationControl.Search(true);
                }
                else
                {
                    pageState.MapLocationControlType.Type = ControlType.NoMatch;
                    shiftForm = true;
                }
			} 
			else 
			{
				pageState.MapLocationSearch = parameters.GetLocationSearch(0);
				pageState.MapLocation = parameters.GetLocation(0);
			}

            if (shiftForm)
            {
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
		/// VisitPlanner Location1 Map click event hadler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MapLocation1Click(object sender, EventArgs e)
		{
            bool shiftForm = false;

			pageState.MapType = CurrentLocationType.VisitPlannerVisitPlace1;
			pageState.MapMode = CurrentMapMode.VisitPlannerLocation1;

			pageState.JourneyInputReturnStack.Push(pageId);

			if (parameters.GetLocation(1).Status == TDLocationStatus.Unspecified )
			{
				mapSearch(visit1LocationSearch.InputText,  visit1LocationSearch.SearchType,  visit1LocationSearch.FuzzySearch, true, true);
                if (pageState.MapLocationSearch.InputText.Length == 0)
                    pageState.MapLocationControlType.Type = ControlType.Default;
                else if (pageState.MapLocation.Status == TDLocationStatus.Valid)
                {
                    visit1LocationSearch = pageState.MapLocationSearch;
                    visit1LocationSearch.LocationFixed = true;
                    visit1Location = pageState.MapLocation;
                    parameters.SetLocation(1, visit1Location);
                    parameters.SetLocationSearch(1, visit1LocationSearch);
                    TDSessionManager.Current.JourneyParameters = parameters;
                    visitPlannerLocationVisitPlace1.LocationControl.Search(true);
                }
                else
                {
                    pageState.MapLocationControlType.Type = ControlType.NoMatch;
                    shiftForm = true;
                }
			} 
			else 
			{
				pageState.MapLocationSearch = parameters.GetLocationSearch(1);
				pageState.MapLocation = parameters.GetLocation(1);
			}

            if (shiftForm)
            {
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
		/// VisitPlanner Location2 Map click event hadler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MapLocation2Click(object sender, EventArgs e)
		{
            bool shiftForm = false;

			pageState.MapType = CurrentLocationType.VisitPlannerVisitPlace2;
			pageState.MapMode = CurrentMapMode.VisitPlannerLocation2;

			pageState.JourneyInputReturnStack.Push(pageId);

			if (parameters.GetLocation(2).Status == TDLocationStatus.Unspecified )
			{
				mapSearch(visit2LocationSearch.InputText,  visit2LocationSearch.SearchType,  visit2LocationSearch.FuzzySearch, true, true);
                if (pageState.MapLocationSearch.InputText.Length == 0)
                    pageState.MapLocationControlType.Type = ControlType.Default;
                else if (pageState.MapLocation.Status == TDLocationStatus.Valid)
                {
                    visit2LocationSearch = pageState.MapLocationSearch;
                    visit2LocationSearch.LocationFixed = true;
                    visit2Location = pageState.MapLocation;
                    parameters.SetLocation(2, visit2Location);
                    parameters.SetLocationSearch(2, visit2LocationSearch);
                    TDSessionManager.Current.JourneyParameters = parameters;
                    visitPlannerLocationVisitPlace2.LocationControl.Search(true);
                }
                else
                {
                    pageState.MapLocationControlType.Type = ControlType.NoMatch;
                    shiftForm = true;
                }
			} 
			else 
			{
				pageState.MapLocationSearch = parameters.GetLocationSearch(2);
				pageState.MapLocation = parameters.GetLocation(2);
			}

            if (shiftForm)
            {
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
		/// 
		/// </summary>
		/// <param name="listType"></param>
		/// <returns></returns>
		private SearchType GetDefaultSearchType(DataServiceType listType)
		{
			DataServices.IDataServices ds = (DataServices.IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			string defaultItemValue = ds.GetDefaultListControlValue(listType);
			return (SearchType) (Enum.Parse(typeof(SearchType), defaultItemValue));
		}

		/// <summary>
		/// Set up a new search using the map search and location objects
		/// (those in the input page state and not journey parameters) so that
		/// they do not overwrite the journey parameter search and location objects
		/// in case the location selected from the map is cancelled
		/// </summary>
		/// <param name="inputText">Input text</param>
		/// <param name="searchType">Search type</param>
		/// <param name="fuzzy">Fuzzy search</param>
		/// <param name="acceptsPostcode">Location can accept postcodes</param>
		/// <param name="acceptsPartPostcode">Location can accept partial postcodes</param>
		private void mapSearch(string inputText, SearchType searchType, bool fuzzy, bool acceptsPostcode, bool acceptsPartPostcode)
		{
			pageState.MapLocationSearch.ClearAll();
			pageState.MapLocation.Status = TDLocationStatus.Unspecified;
			pageState.MapLocationSearch.SearchType = GetDefaultSearchType(DataServiceType.VisitPlannerLocationSelection);

			LocationSearch thisSearch = pageState.MapLocationSearch;
			TDLocation thisLocation = pageState.MapLocation;

			LocationSearchHelper.SetupLocationParameters(
				inputText,
				searchType,
				fuzzy,
				0,
				parameters.MaxWalkingTime,
				parameters.WalkingSpeed,
				ref thisSearch,
				ref thisLocation,
				acceptsPostcode,
				acceptsPartPostcode,
				StationType.Undetermined
				);

			pageState.MapLocationSearch = thisSearch;
			pageState.MapLocation = thisLocation;
		}
	}
}
