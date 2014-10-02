// *********************************************** 
// NAME                 : FindFareInput.aspx
// AUTHOR               : Tim Mollart
// DATE CREATED         : 21/01/2005
// DESCRIPTION			: Input page for find a fare.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindFareInput.aspx.cs-arc  $
//
//   Rev 1.3   May 01 2008 17:23:56   mmodi
//Updated to display session timeout error
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.2   Mar 31 2008 13:24:28   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:29:30   mturner
//Initial revision.
//
//DEVFACTORY FEB 22 2008 sbarker
//White-labelling
//
//   Rev 1.36   Apr 27 2006 11:20:52   mtillett
//Prevent calendar button dislpay on ambiguity page after next or back buttons clicked
//Resolution for 3510: Apps: Calendr Control problems on input/ambiguity screen
//
//   Rev 1.35   Apr 05 2006 15:42:54   build
//Automatically merged from branch for stream0030
//
//   Rev 1.34.1.0   Mar 29 2006 11:31:40   RGriffith
//Wait Page Ehancement changes
//Resolution for 33: DEL 8.1 Workstream: Wait Pages
//
//   Rev 1.34   Feb 23 2006 19:16:38   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.33   Feb 10 2006 12:24:50   tolomolaiye
//Merge for stream 3180 - Homepage Phase 2
//
//   Rev 1.32   Dec 19 2005 15:09:02   jgeorge
//Removed code from previous change, as it does not work correctly and is not required.
//Resolution for 3370: Navigation Error in Find a Fare
//
//   Rev 1.31.1.2   Dec 22 2005 11:20:46   tmollart
//Removed calls to now redudant SaveCurrentFindaMode.
//Removed reference to OldJourneyParameters where applicable.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.31.1.1   Dec 12 2005 16:45:28   tmollart
//Modified to use new initialise parameters method on session manager. Removed any code that redirects users to existing results.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.31.1.0   Nov 30 2005 10:42:22   RGriffith
//Changes applied for using new HeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.31   Nov 22 2005 12:10:30   mguney
//sessionManager.JourneyParameters is initialised in the beginning of initialPageSetup method. This is needed because of the new home page. (PlanAJourney conrol assigning sessionManager.JourneyParameters a TDJourneyParametersMulti parameter.)
//Resolution for 3164: DN040: SBP Invalid Cast Error  (SBP -> Home -> Find a Fare)
//
//   Rev 1.30   Nov 15 2005 19:03:04   rgreenwood
//IR 2990 Wired up Help Button for both input and ambiguity modes
//Resolution for 2990: UEE Post Build Enhancement: Add Help Pages to Input Pages
//
//   Rev 1.29   Nov 10 2005 14:32:46   NMoorhouse
//TD093 UEE Input Pages - Soft Content
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.28   Nov 09 2005 17:06:30   RPhilpott
//Merge for stream2818
//
//   Rev 1.27   Nov 09 2005 14:41:48   ECHAN
//Fix for code review comments #3
//
//   Rev 1.26   Nov 03 2005 17:01:30   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.25.1.2   Oct 28 2005 18:47:18   AViitanen
//TD093 Input page - page formatting
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.25.1.1   Oct 27 2005 14:01:08   NMoorhouse
//TD93 - UEE Input Pages, Date Control element CUT
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.25.1.0   Oct 10 2005 10:09:18   rgreenwood
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.25   Apr 22 2005 14:37:46   tmollart
//Changed when TravelDetails are loaded. Now on users 1st access of page (if we have reset params) and also when the user clears the page.
//Resolution for 2251: Door-to-door Logged In With Do Not Use Motorways Selected Cannot Unselect
//
//   Rev 1.24   Apr 20 2005 13:55:16   tmollart
//Updated page title to use correct langstring.
//Resolution for 2256: Del 7 - PT : Title of browser window on find fare input is incorrect
//
//   Rev 1.23   Apr 20 2005 11:56:42   tmollart
//Modified Page_Load so user is taken to correct page if they have cost based results.
//Modified initialPageSetUp method so that if user has already planned cost based results in trunkcostbased mode they are erased.
//Resolution for 2147: PT - Cost based city to city planner automatically jumps to previously planned find a fare results when selected.
//
//   Rev 1.22   Apr 18 2005 11:46:22   tmollart
//Added LoadTravelDetails and SaveTravelDetails methods for saving and loading user preferences.
//Resolution for 2159: Find a fare save preferences check-box
//
//   Rev 1.21   Apr 16 2005 09:42:34   tmollart
//Added code to change page title dependant on Find A Mode.
//Resolution for 2158: City to city cost based planner is given the title 'Find a Fare' when the page is accessed
//
//   Rev 1.20   Apr 15 2005 12:48:12   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.19   Apr 13 2005 14:26:18   tmollart
//Added event handler so date changes are recognised and saved into page state correctly.
//Resolution for 2135: PT - Find a f are - Calendar selection ignored for return journey
//
//   Rev 1.18   Apr 13 2005 14:18:20   rhopkins
//On submit set cached FareDateTable to null to force the Date Selection page to reload the new data.
//Resolution for 2114: FindFare amended searches don't display new TravelDate results
//
//   Rev 1.17   Apr 13 2005 09:19:24   tmollart
//Resolution for 2115. Added code to close all single windows in clear page event handler.
//Resolution for 2115: Del 7 - PT - Clear page does not clear help info in Find a Fare
//
//   Rev 1.16   Apr 07 2005 18:33:56   tmollart
//Added code to set the selected ticket type based on the selected return date drop down.
//
//   Rev 1.15   Apr 07 2005 18:03:24   COwczarek
//Fix page title and page redirection to results pages
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.14   Apr 04 2005 15:35:50   tmollart
//Removed session expiry code (IR1720).
//Modified event handler for Preferences Control Adult/Child control so that SearchParams.showAdult is correctly populated.
//
//   Rev 1.13   Apr 01 2005 16:40:52   tmollart
//Modified page load method so that the user is redirected to the results page if results exist.
//Added code to save find a mode when request is submitted.
//Tidied up code.
//
//   Rev 1.12   Mar 22 2005 11:22:34   tmollart
//Added additional functionality so it is possible to amend journey correctly. This means new coded added to correctly handle when the user changes locations.
//
//   Rev 1.11   Mar 16 2005 17:54:34   tmollart
//Removed erroneous line in InitialPageSetup.
//
//   Rev 1.10   Mar 16 2005 14:20:34   tmollart
//PageState now initialised when required to keep reference current to SessionManager. 
//Modified submit event handler to refresh pageState refernence from SessionManager after cost search has been invoked.
//
//   Rev 1.9   Mar 15 2005 08:44:32   tmollart
//Made changes to use the CostSearchWaitControlData and CostSearchWaitStateData properies on TDSessionManager as opposed to in the FindCostBasedPageState object.
//
//   Rev 1.8   Mar 10 2005 16:48:22   COwczarek
//Changed method call to invoke cost search runner
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.7   Mar 09 2005 15:50:18   rscott
//DEL 7 - Code to set the costbasedsearchwaitcontroldata added to preferences submit event.
//
//   Rev 1.6   Mar 09 2005 14:51:00   tmollart
//Modified Submit button handling code so it redirects back to the  same page. Specific state processing now takes care of handling the transition to the wait page etc.
//
//   Rev 1.5   Mar 04 2005 11:31:46   tmollart
//Work in progress.
//
//   Rev 1.4   Mar 01 2005 18:27:16   tmollart
//Work in progress.
//
//   Rev 1.3   Feb 25 2005 10:27:52   tmollart
//Work in progress.
//
//   Rev 1.2   Feb 18 2005 14:42:02   tmollart
//Work in progress to bring module up to specification.


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
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Web.Support;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.CostSearchRunner;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Input page for cost based journey searches.
	/// </summary>
	public partial class FindFareInput : TDPage, ILanguageHandlerIndependent
	{

		#region Declarations


		protected TransportDirect.UserPortal.Web.Controls.SearchTypeControl searchTypeControl;
		protected TransportDirect.UserPortal.Web.Controls.ModeSelectControl modeSelectControl;
		protected TransportDirect.UserPortal.Web.Controls.FindLeaveReturnDatesControl dateControl;
		protected TransportDirect.UserPortal.Web.Controls.FindToFromLocationsControl locationsControl;
		protected TransportDirect.UserPortal.Web.Controls.FindFarePreferenceControl preferencesControl;

		//Page level variables
		private FindAMode requiredMode;
		private CostSearchParams searchParams;
		private InputPageState inputPageState;
		private ITDSessionManager sessionManager;
		private FindFareInputAdapter findFareInputAdapter;

		//Constants
		public const string FIND_A_FARE_RM = "FindAFare";
	
		//Find Fare Mode page titles/labels.
		private const string findFareModeTitleKey = "FindFare.FindFareInput.PageTitle";
		private const string findFareModeFromToTitleKey = "FindFare.FindFareInput.FromToTitle";
	
		//Trunk Cost Based page titles/labels.
		private const string trunkCostBasedModeTitleKey = "FindTrunkInput.labelFindPageTitle";
		private const string trunkCostBasedFromToTitleKey = "FindTrunkInput.labelFromToTitle";

		private const string labelFindFromToTitleKey = "FindFare.FindFareInput.FromToTitle";
		private const string imageBackButtonUrlKey = "FindFare.FindFareInput.ImageBackButton.Url";
		private const string imageBackButtonAltTextKey = "FindFare.FindFareInput.ImageBackButton.AltText";
		private const string leaveFlexibilitySRLabel = "FindFare.FindFareInput.LeaveDateControl.FlexibilitySRLabel";
		private const string returnFlexibilitySRLabel = "FindFare.FindFareInput.ReturnDateControl.FlexibilitySRLabel";
		private const string backButtonUrlKey = "FindFare.FindFareInput.Back.Url";
		private const string backButtonAltKey = "FindFare.FindFareInput.Back.Alt";
		#endregion

		#region Constructor
		/// <summary>
		/// Constructor. Sets page ID to FindFareInput and sets the local resource manager.
		/// </summary>
		public FindFareInput()
		{
			this.pageId = PageId.FindFareInput;
			LocalResourceManager = TDResourceManager.FIND_A_FARE_RM;
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

		#region Page Initialisation
		protected void Page_Init(object sender, System.EventArgs e)
		{
			//Search type control event handling
			this.searchTypeControl.SearchTypeChanged += new System.EventHandler(this.SearchTypeControl_SearchTypeChanged);

			//Mode select control event handling
			this.modeSelectControl.TravelModeChanged += new System.EventHandler(this.ModeSelectControl_Changed);

			//Date selection control event handling
			this.dateControl.LeaveDateControl.DateChanged += new System.EventHandler(this.DateSelectControl_DateChanged);
			this.dateControl.ReturnDateControl.DateChanged += new System.EventHandler(this.DateSelectControl_DateChanged);

			//Preference control event handling (for control selection changes).
			this.preferencesControl.DiscountRailCardChanged += new System.EventHandler(this.preferenceControl_DiscountRailCardChanged);
			this.preferencesControl.DiscountCoachCardChanged += new System.EventHandler(this.preferenceControl_DiscountCoachCardChanged);
			this.preferencesControl.AdultChildChanged += new System.EventHandler(this.preferenceControl_AdultChildChanged);

			//Subscribe to events published by the FindFarePreferencesControl
			this.preferencesControl.PreferencesPageOptionsControl.Submit += new System.EventHandler(this.preferencesControl_Submit);
			this.preferencesControl.PreferencesPageOptionsControl.Clear += new System.EventHandler(this.preferencesControl_Clear);
			this.preferencesControl.PreferencesVisibleChanged += new System.EventHandler(this.preferencesControl_PreferencesVisibleChanged);

			//Back button event handlers
			this.preferencesControl.PreferencesPageOptionsControl.Back += new System.EventHandler(this.PreferenceControl_BackButtonClicked);

			//Location control event handlers
			this.locationsControl.NewLocationFrom += new System.EventHandler(this.LocationsControl_NewLocationFrom);
			this.locationsControl.NewLocationTo += new System.EventHandler(this.LocationsControl_NewLocationTo);

		}
		#endregion

		#region Page Load and Pre Render

		protected void Page_Load(object sender, System.EventArgs e)
		{
			FindCostBasedPageState pageState;
			sessionManager = TDSessionManager.Current;

            #region Display Session Errors
            // Check if there are any errors in the InputPageState and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, TDSessionManager.Current.InputPageState.InputSessionErrors);

            // Clear the error messages
            TDSessionManager.Current.InputPageState.InputSessionErrors = null;
            #endregion

			if (Page.IsPostBack)
			{
				pageState = (FindCostBasedPageState)sessionManager.FindPageState;
				searchParams = (CostSearchParams)sessionManager.JourneyParameters;
				findFareInputAdapter = new FindFareInputAdapter(pageState,sessionManager);
				findFareInputAdapter.UpdateJourneyDates(dateControl);
			}
			else
			{
				//User's first request of page.				
				initialPageSetup();

				//If the user has planned a journey that originated in find a fare then its possible
				//they may already have cost based results. We need to determine if any cost based
				//journeys exist in the cost based partition and if so redirect user to the appropriate
				//results page.
				if (TDSessionManager.Current.ItineraryManager.BaseJourneyFindAMode == FindAMode.Fare)
				{
					//Has the user got actual cost based results (step 3/3). If so redirect them
					//to the results page.
					if (sessionManager.HasCostBasedJourneyResults) 
					{
						TDSessionManager.Current.Session[SessionKey.Transferred] = false;
						TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindFareServiceResults;
					} 
						//If the user only has fares then then redirect them to the appropriate page
						//which is step 1/3.
					else if (sessionManager.HasCostBasedFaresResults) 
					{
						TDSessionManager.Current.Session[SessionKey.Transferred] = false;
						TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindFareDateSelectionDefault;
					}
				}
			}

			//The locations controls have to be initialised before then can be used. They may
			//have been initialised IF we called initialPageSetup. Ensure they are not initialised
			//twice. Use the locations control to check that initialisation has not taken place.
			if (locationsControl.FromLocationControl.TheLocation == null)
			{
				findFareInputAdapter.InitLocationsControl(locationsControl);
			}

			//Set up page title depending on find a mode.
			if (sessionManager.FindAMode == FindAMode.TrunkCostBased)
			{
				//Use standard resource manager - not find dare resource manager.
				labelFindPageTitle.Text = Global.tdResourceManager.GetString(trunkCostBasedModeTitleKey, TDCultureInfo.CurrentUICulture);
				labelFromToTitle.Text = Global.tdResourceManager.GetString(trunkCostBasedFromToTitleKey, TDCultureInfo.CurrentUICulture);
			}
			else
			{
				labelFindPageTitle.Text = GetResource(findFareModeTitleKey);
				labelFromToTitle.Text = GetResource(findFareModeFromToTitleKey);
			}

			//Set up page labels and graphics
			PageTitle = GetResource("JourneyPlanner.DefaultPageTitle") + GetResource("FindFareInput.AppendPageTitle");
			
			if (sessionManager.FindPageState.AmbiguityMode)
			{
				Helpbuttoncontrol1.HelpUrl = GetResource("FindFareInput.HelpAmbiguityUrl");

			}
			else
			{
				Helpbuttoncontrol1.HelpUrl = GetResource("FindFareInput.HelpPageUrl");
			}

			//Set up appearance of the date controls.
			findFareInputAdapter.SetupDateControl(dateControl);

			//Set up screen reader labels contained in the date controls. This is done
			//here to use the local resource managger.
			dateControl.LeaveDateControl.DateControl.AmbiguousDateControl.FlexibilityScreenReaderLabel.Text = GetResource(leaveFlexibilitySRLabel);
			dateControl.ReturnDateControl.DateControl.AmbiguousDateControl.FlexibilityScreenReaderLabel.Text = GetResource(returnFlexibilitySRLabel);

            //Added for white labelling:
            //(Note that this code might not work; this form isn't live and so not easy to
            //test. Changes will be minimal if needed.)
            ConfigureLeftMenu("FindFareInput.clientLink.BookmarkTitle", "FindFareInput.clientLink.LinkText", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.CONTEXT1);
		}

		protected void Page_PreRender(object sender, System.EventArgs e)
		{		
			FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;
			if (Page.IsPostBack)
			{
				//Required mode is used later on. We dont evaluate this across
				//postbacks so if the page is a postback (yes) and the required
				//mode is none (as it hasnt been evaluated) then set it to the
				//findamode of the current session.
				if (requiredMode == FindAMode.None)
					requiredMode = TDSessionManager.Current.FindAMode;

				//Update the date controls.
				findFareInputAdapter.UpdateDateControl(dateControl);
			}

			//Set visibility of searchtype and modeselect controls.
			modeSelectControl.Visible = (requiredMode == FindAMode.Fare);
			searchTypeControl.Visible = (requiredMode == FindAMode.TrunkCostBased);

			//Set visibility of controls based on the ambiguity mode.
			labelFromToTitle.Visible = !pageState.AmbiguityMode;
			panelBackTop.Visible = pageState.AmbiguityMode;
			panelErrorMessage.Visible = pageState.AmbiguityMode;
			preferencesControl.AmbiguityMode = pageState.AmbiguityMode;

			//If we are in ambiguity mode then test to see if the errors panel
			//needs to be updated with the latest error messages.
			if (pageState.AmbiguityMode)
			{
				modeSelectControl.DisplayMode = GenericDisplayMode.ReadOnly;
				findFareInputAdapter.UpdateErrorMessages(panelErrorMessage, labelErrorMessages, TDSessionManager.Current.ValidationError);
				dateControl.LeaveDateControl.DateErrors = TDSessionManager.Current.ValidationError;
				dateControl.ReturnDateControl.DateErrors = TDSessionManager.Current.ValidationError;
			}
			else
			{
				modeSelectControl.DisplayMode = GenericDisplayMode.Normal;
			}

			//Repopulate the drop down controls and the preferences with the 
			//values from the search params.
			findFareInputAdapter.InitPreferencesControl(preferencesControl);
			findFareInputAdapter.InitTravelModes(modeSelectControl);
		}

		#endregion
		
		#region Private Methods
		private void initialPageSetup()
		{
			bool resetDone;

			//Get reference to current itinerary manager instance
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

			// if an extension is in progress, cancel it
			sessionManager.ItineraryManager.CancelExtension();

			//Look at the request object to determine if the "FindMode" parameter has been
			//specified. This will indicate that we are in Find a Fare mode and not
			//Trunk Cost Based mode.
			switch (Request["FindMode"])
			{
				case "Fare":
					requiredMode = FindAMode.Fare;
					break;
				case "TrunkCostBased":
					requiredMode = FindAMode.TrunkCostBased;
					break;
				default:
					//If there is no findmode query string available set the current mode to 
					//current session manager mode. If the current session manager mode is not
					//fare and not trunk cost based set it to fare as a default.
					requiredMode = TDSessionManager.Current.FindAMode;
					if ((requiredMode != FindAMode.Fare) && (requiredMode != FindAMode.TrunkCostBased))
						requiredMode = FindAMode.Fare;
					break;
			}

			//Initialise journey paremters and page states
			resetDone = sessionManager.InitialiseJourneyParameters(requiredMode);

			//Create object references
			FindCostBasedPageState pageState = (FindCostBasedPageState)sessionManager.FindPageState;

			searchParams = sessionManager.JourneyParameters as CostSearchParams;
			inputPageState = sessionManager.InputPageState;
			findFareInputAdapter = new FindFareInputAdapter(pageState, sessionManager);

			//Set up the page
			if (resetDone)            
			{
				pageState.Initialise();
				findFareInputAdapter = new FindFareInputAdapter(pageState,sessionManager);
				findFareInputAdapter.InitJourneyParameters();
				findFareInputAdapter.InitLocationsControl(locationsControl);
				findFareInputAdapter.InitTravelModes(modeSelectControl);
				LoadTravelDetails();
			}

			//Initialise date controls with cost based search params and load any saved preferences.
			findFareInputAdapter.UpdateDateControl(dateControl);	
		}

		/// <summary>
		/// Loads travel details if user is authenticated and copies them into parameters. Should
		/// be called before parameters are populated onto the page.
		/// </summary>
		private void LoadTravelDetails()
		{
			if (TDSessionManager.Current.Authenticated) 
			{
				FindCostBasedPageState pageState = (FindCostBasedPageState)sessionManager.FindPageState;
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

		/// <summary>
		/// Saves travel details to the user preferences.
		/// </summary>
		private void SaveTravelDetails() 
		{
			if (TDSessionManager.Current.Authenticated && preferencesControl.PreferencesTravelDetailControl.SaveDetails)
			{
				TDProfile travelPreferences = TDSessionManager.Current.CurrentUser.UserProfile;
				travelPreferences.Properties[ProfileKeys.DISCOUNT_CARD_RAIL].Value = preferencesControl.DiscountRailCard;
				travelPreferences.Properties[ProfileKeys.DISCOUNT_CARD_COACH].Value = preferencesControl.DiscountCoachCard;
				travelPreferences.Properties[ProfileKeys.ADULT_CHILD].Value = (!preferencesControl.AdultChild);
				travelPreferences.Update();
			}
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
				return ((sessionManager.JourneyParameters.OriginLocation.SearchType == SearchType.FindNearest) ||
					(sessionManager.JourneyParameters.DestinationLocation.SearchType == SearchType.FindNearest));
			}
		}

		#endregion

		#region Event Handlers for Submit and Clear buttons

		//Handle the pressing of the Next/Submit button.
		private void preferencesControl_Submit(object sender, System.EventArgs e)
		{
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

			int refreshInterval;
			string resourceFilename = "FindAFare";
			string resourceId;

			// Determine refresh interval and resource string for the wait page
			if (requiredMode == FindAMode.TrunkCostBased)
			{
				// for City to City (cost based) searches
				refreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.FindFare.CityToCity"]);
				resourceId = "WaitPageMessage.FindFare.CityToCity";
			} 
			else
			{
				// For Find a fare input
				refreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.FindFare.FindFareInput"]);
				resourceId = "WaitPageMessage.FindFare.FindFareInput";
			}

			//Invoke the cost search runner and return a searchWaitStateData object.
			findFareInputAdapter.InitialiseAsyncCallStateForFaresSearch(refreshInterval, resourceFilename, resourceId);
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
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindFareAmbiguityResolution;
		}

		//Handle the processing of the Clear button
		private void preferencesControl_Clear(object sender, System.EventArgs e)
		{
			FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;
			//Initialise required controls and objects
			findFareInputAdapter.InitJourneyParameters();
			pageState.Initialise();
			locationsControl.SetLocationsUnspecified();
			locationsControl.FromLocationControl.ResetPlaceControlDropDown();
			locationsControl.ToLocationControl.ResetPlaceControlDropDown();		
			dateControl.LeaveDateControl.Visible = true;
			dateControl.ReturnDateControl.Visible = true;
			TDPage.CloseAllSingleWindows(Page);
			LoadTravelDetails();
		}

		#endregion

		#region Page Event Handlers (Private)
		//Handle the date control
		private void DateSelectControl_DateChanged(object sender, EventArgs e)
		{
			findFareInputAdapter.UpdateJourneyDates(dateControl);
		}

		//Handle the visibility of the preferences control.
		private void preferencesControl_PreferencesVisibleChanged(object sender, EventArgs e)
		{
			FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;
			pageState.TravelDetailsVisible = preferencesControl.PreferencesVisible;
		}

		//Handle the discount rail card changed event.
		private void preferenceControl_DiscountRailCardChanged(object sender, EventArgs e)
		{
			searchParams.RailDiscountedCard = preferencesControl.DiscountRailCard;
		}

		//Handle the discount coach card changed event.
		private void preferenceControl_DiscountCoachCardChanged(object sender, EventArgs e)
		{
			searchParams.CoachDiscountedCard = preferencesControl.DiscountCoachCard;
		}

		//Handle the adult/child changed event.
		private void preferenceControl_AdultChildChanged(object sender, EventArgs e)
		{
			FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;
			pageState.ShowChild = !preferencesControl.AdultChild;
		}
																				 
		private void SearchTypeControl_SearchTypeChanged(object sender, System.EventArgs e)
		{
			//Dependant on which search type is selected perform a page
			//transistion to put the user on the correct page. Evaluate
			//the cost search property of the control.
			if (!searchTypeControl.CostSearch)
			{
				//Transfer the user to FindTrunkInput
				sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindTrunkInputDefault;
			}
		}

		private void ModeSelectControl_Changed(object sender, System.EventArgs e)
		{
			ArrayList modeList = new ArrayList(3);

			//Check if Rail has been selected.
			if (modeSelectControl.RailSelected)
				modeList.Add(TicketTravelMode.Rail);

			//Check if Coach has been selected.
			if (modeSelectControl.CoachSelected)
				modeList.Add(TicketTravelMode.Coach);

			//Check if Air has been selected.
			if (modeSelectControl.AirSelected)
				modeList.Add(TicketTravelMode.Air);

			searchParams.TravelModesParams = (TicketTravelMode[])modeList.ToArray(typeof(TicketTravelMode));
		}

		#region Back Button Event Handling
		
		private void BackButton_Clicked(object sender, EventArgs e)
		{
			//raising back event with no data passed
			PreferenceControl_BackButtonClicked(sender,EventArgs.Empty);
		}
		
		private void PreferenceControl_BackButtonClicked(object sender, System.EventArgs e)
		{
			dateControl.CalendarClose();

			FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;
			//Reinstate the users initial input.
			pageState.ReinstateJourneyParameters(searchParams);

			//Reset all controls back to input values.
			TDSessionManager.Current.ValidationError = null;
			pageState.AmbiguityMode = false;
			dateControl.LeaveDateControl.AmbiguityMode = false;
			dateControl.ReturnDateControl.AmbiguityMode = false;
			dateControl.LeaveDateControl.DateErrors = null;
			dateControl.ReturnDateControl.DateErrors = null;
			dateControl.LeaveDateControl.Visible = true;
			dateControl.ReturnDateControl.Visible = true;
			modeSelectControl.DisplayMode = GenericDisplayMode.Normal;
			preferencesControl.AmbiguityMode = false;
			locationsControl.ToLocationControl.TheLocation.Status = TDLocationStatus.Unspecified;
			locationsControl.FromLocationControl.TheLocation.Status = TDLocationStatus.Unspecified;
		}

		#endregion

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

		private void LocationsControl_NewLocationTo(object sender, System.EventArgs e)
		{
			//See notes above.
			searchParams.DestinationLocation = locationsControl.ToLocationControl.TheLocation;
			searchParams.Destination = locationsControl.ToLocationControl.TheSearch;
			searchParams.Destination.SearchType = SearchType.Locality;
			searchParams.Destination.DisableGisQuery();
			searchParams.DestinationType = locationsControl.ToLocationControl.LocationControlType;
		}

		#endregion
		
	}
}

