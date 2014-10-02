// *********************************************** 
// NAME                 : RefineJourneyPlan.ascx.cs 
// AUTHOR               : Andrew Sinclair
// DATE CREATED         : 09/01/2006
// DESCRIPTION			: Page allowing users to Extend, Replan or Adjust their journey
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/RefineJourneyPlan.aspx.cs-arc  $: 
//
//   Rev 1.10   Mar 11 2013 09:49:58   mmodi
//Added accessible journey note to amend option
//Resolution for 5897: Accessible options message is not displayed on the modify journey screen
//
//   Rev 1.9   Dec 11 2012 12:00:28   mmodi
//Hide replan option for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.8   Jul 28 2011 16:20:46   DLane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.7   Jul 29 2010 16:14:06   mmodi
//Changes to page layout and styles to be exactly consistent for all input pages in the Portal
//Resolution for 4760: IE7-find a car route check boxes
//
//   Rev 1.6   Mar 29 2010 16:40:36   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.5   Jan 06 2009 15:27:02   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.4   Jul 01 2008 15:27:54   rbroddle
//Added alt text for Adjust,Replan and Amend images
//Resolution for 5016: WAI WCAG level A compliance faults - Missing Alt text
//
//   Rev 1.3   Apr 30 2008 15:45:24   mmodi
//Fixed problem with summary being shown after clicking back on modify journey. Also, order of buttons and enablement of buttons sorted on modify journey screens.
//Resolution for 4911: When a journey is modified in any way, the default results tab is the summary tab
//
//   Rev 1.2   Mar 31 2008 13:25:38   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:14   mturner
//Initial revision.
//
//   Rev 1.30   May 30 2007 17:47:38   asinclair
//Updated image url for Amend Image
//Resolution for 4421: 9.6 - Add Amend to Modify
//
//   Rev 1.29   May 23 2007 13:55:16   asinclair
//Added logging events to next, new journey, back and each of the extend, adjust etc functions to track user usage
//Resolution for 4421: 9.6 - Add Amend to Modify
//
//   Rev 1.28   May 22 2007 17:37:16   asinclair
//Changes made to allow accces to Amend Journey
//Resolution for 4421: 9.6 - Add Amend to Modify
//
//   Rev 1.27   Apr 28 2006 13:24:08   COwczarek
//Pass summary type in call to FormattedSelectedJourneys method.
//Resolution for 3970: DN068 Extend: Wrong choice of default journey in Extension options
//
//   Rev 1.26   Mar 31 2006 16:13:48   RGriffith
//Addition of Default Click 
//
//   Rev 1.25   Mar 30 2006 18:18:26   pcross
//Moved the image part of the extend options into the extend options control so the whole lot can be shown / hidden at once
//Resolution for 3704: DN068 Extend: Allowed to add a 2nd extension to start despite 1st extension being planned in the past
//
//   Rev 1.24   Mar 28 2006 09:05:30   pcross
//Minor changes to layout and the way Results Table Title control is populated
//Resolution for 3670: Extend, Replan & Adjust: Date is missing against the Full initerary table heading
//
//   Rev 1.23   Mar 22 2006 12:13:26   pcross
//Help button update
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.22   Mar 21 2006 17:04:36   pcross
//Fixed invisible return text
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.21   Mar 21 2006 14:52:36   pcross
//Unit test fix
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.20   Mar 21 2006 12:17:22   asinclair
//Updated with Code Review comments
//
//   Rev 1.19   Mar 20 2006 12:30:00   asinclair
//Added Images
//
//   Rev 1.18   Mar 17 2006 08:54:18   pcross
//Wired up New Journey button
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.17   Mar 14 2006 13:19:36   pcross
//Added header, headelementcontrol, resource manager reference
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.16   Mar 10 2006 14:42:34   pcross
//Resource string ref update
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.15   Mar 08 2006 16:29:14   RGriffith
//FxCop Suggested Changes
//
//   Rev 1.14   Mar 08 2006 11:14:34   tmollart
//Added back button functionality.
//Added functionality to control when replan options are available.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.13   Mar 03 2006 16:13:18   tmollart
//Added code to reset itinerary on page load.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.12   Mar 03 2006 11:18:36   pcross
//Added skip links and layout improvements
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.11   Mar 02 2006 15:22:14   pcross
//Improved judgement of whether a journey is ok for adjust
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.10   Mar 02 2006 10:43:16   asinclair
//Replan options now display correct text depending on public or private selected journey
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.9   Mar 01 2006 09:56:40   pcross
//Handles the visibility of adjust options
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.8   Feb 27 2006 17:24:24   NMoorhouse
//Added error control (when no radio button has been selected) and footer
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7   Feb 24 2006 11:09:08   pcross
//Added results table title control
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.6   Feb 22 2006 16:32:44   asinclair
//Updates to fix problem with Back button from Extend Input pages
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Feb 20 2006 12:37:54   pcross
//Added handling of Adjust request
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Feb 16 2006 17:25:24   asinclair
//Work in progress check in
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan

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
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using Logger = System.Diagnostics.Trace;
using JC = TransportDirect.UserPortal.JourneyControl;

using TransportDirect.Common;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for RefineJourneyPlan.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class RefineJourneyPlan : TDPage
	{
		protected TransportDirect.UserPortal.Web.Controls.ResultsSummaryControl ResultsSummaryControl1;
		protected TransportDirect.UserPortal.Web.Controls.ExtendJourneyOptionsControl extendJourneyOptionsControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsTableTitleControl resultsTableTitleControl;
		protected TransportDirect.UserPortal.Web.Controls.ErrorDisplayControl errorDisplayControl1;
		protected TransportDirect.UserPortal.Web.Controls.FooterControl FooterControl1;
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;

		/// <summary>
		/// Constructor for the Page
		/// </summary>
		public RefineJourneyPlan() : base()
		{
			pageId = PageId.RefineJourneyPlan;
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
		}
	
		/// <summary>
		/// Handles page load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{

			// Clear any previous extend/replan itinerary.
			TDItineraryManager im = TDSessionManager.Current.ItineraryManager;

			// Ensure that there is no Itinerary
			im.ResetToInitialJourney();
			TDSessionManager.Current.ItineraryMode = ItineraryManagerMode.None;
			
			// Set up page
			buttonNewJourney.Text = GetResource("ButtonNewJourney.Text");
			labelTitle.Text = GetResource("RefineJourney.Title");
			labelIntroductoryText.Text = GetResource("RefineJourney.IntroductoryText");

			buttonHelp.HelpUrl = GetResource("RefineJourney.ButtonHelp.HelpUrl");

			if(!TDSessionManager.Current.JourneyParameters.IsReturnRequired)
			{
				labelAdjustOptionTwo.Visible = false;
			}

			labelReplanTitle.Text = GetResource("RefineJourney.labelReplanTitle");

			labelAdjustTitle.Text = GetResource("RefineJourney.labelAdjustTitle");
			labelAdjustOptionOne.Text = GetResource("RefineJourney.labelAdjustOptionOne");
			labelAdjustOptionTwo.Text = GetResource("RefineJourney.labelAdjustOptionTwo");

			labelAmendTitle.Text = GetResource("RefineJourney.labelAmendTitle");
			labelAmendOptionOne.Text = GetResource("RefineJourney.labelAdmendOptionOne");


			buttonBack.Text = GetResource("ExtendOptionsControl.Back");
			buttonNext.Text = GetResource("ExtendOptionsControl.Next");

			adjustImage.ImageUrl = GetResource("RefineJourney.adjustJourney.ImageURL");
            adjustImage.AlternateText = "";
			replanImage.ImageUrl = GetResource("RefineJourney.replanJourney.ImageURL");
            replanImage.AlternateText = "";
			amendImage.ImageUrl = GetResource("RefineJourney.amendJourney.ImageURL");
            amendImage.AlternateText = "";

            this.PageTitle = GetResource("RefineJourney.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

			// Summary table title
			PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(TDSessionManager.Current);
			
			string labelTextOverride = GetResource("RefineJourney.labelTitleControl");
			plannerOutputAdapter.PopulateResultsTableTitleFullItinerary(resultsTableTitleControl, labelTextOverride, true);

			PopulateFromSessionData();

			HandleReplanControlVisibility();

			// Clear page stack ready for use
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();

            //Added for white labelling:
            ConfigureLeftMenu("RefineJourneyPlan.clientLink.BookmarkTitle", "RefineJourneyPlan.clientLink.LinkText", clientLink, expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextRefineJourneyPlan);
            expandableMenuControl.AddExpandedCategory("Related links");

		}

		/// <summary>
		/// OnPreRender method - overrides base and updates the visiblity
		/// of controls depending on which should be rendered. Calls base OnPreRender
		/// as the final step.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			//Display the ErrorDisplayControl if it has been populated with error message
			if(errorDisplayControl1.ErrorStrings.Length > 0)
			{
				errorMessagePanel.Visible = true;
				errorDisplayControl1.Visible = true;
			}
			else
			{
				errorMessagePanel.Visible = false;
				errorDisplayControl1.Visible = false;
			}

            HandleAmendControlVisibility();
			HandleAdjustControlVisibility();
			//HandleReplanControlVisibility();

			SetupSkipLinksAndScreenReaderText();

			base.OnPreRender(e);
		}


		/// <summary>
		/// Populates the Results Summary Control with the current journey
		/// </summary>
		private void PopulateFromSessionData()
		{

			ResultsSummaryControl1.ShowDeleteColumn = false;
			ResultsSummaryControl1.ShowSelectColumn = false;
			ResultsSummaryControl1.ShowEmptyDeleteColumn = false;
			ResultsSummaryControl1.ShowEmptySelectColumn = false;

			// Populate the control
			ResultsAdapter helper = new ResultsAdapter();

			ITDSessionManager sessionManager = TDSessionManager.Current;

			ResultsSummaryControl1.SummaryLines = helper.FormattedSelectedJourneys(sessionManager, FormattedSummaryType.Replan);	
			
		}

		/// <summary>
		/// Sets visibility on Adjust controls according to whether there is a return journey and according to whether
		/// the outward / return legs are multi-leg (excluding walks)
		/// </summary>
		private void HandleAdjustControlVisibility()
		{

			RefineHelper refineHelper = new RefineHelper();

			bool outwardAdjustAvailable = false;
			bool returnAdjustAvailable = true;

			outwardAdjustAvailable = refineHelper.IsAdjustAvailable(true);
			returnAdjustAvailable = refineHelper.IsAdjustAvailable(false);


			// If both invisible then hide the whole adjust panel
			if (!outwardAdjustAvailable && !returnAdjustAvailable)
			{
				adjustPanel.Visible = false;
			}
			else
			{
				if (!outwardAdjustAvailable)
				{
					scriptableRadioButtonAdjustOutward.Visible = false;
					labelAdjustOptionOne.Visible = false;
				}

				if (!returnAdjustAvailable)
				{
					scriptableRadioButtonAdjustReturn.Visible = false;
					labelAdjustOptionTwo.Visible = false;
				}

			}

		}
        
		/// <summary>
		/// Sets visibility on Replan controls according to whether there is a return journey 
		/// </summary>
		private void HandleReplanControlVisibility()
		{
            RefineHelper refineHelper = new RefineHelper();

			ITDSessionManager sm = TDSessionManager.Current;

			bool replanAvailableOutward = false;
			bool replanAvailableReturn = false;
			
			// Is outward replan available.
			Journey outwardJourney = ( sm.JourneyViewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal ? (Journey)sm.JourneyResult.OutwardPublicJourney(sm.JourneyViewState.SelectedOutwardJourneyID) : (Journey)sm.JourneyResult.OutwardRoadJourney() );
			replanAvailableOutward = (!(outwardJourney.JourneyLegs.Length == 1 && outwardJourney.JourneyLegs[0].HasInvalidCoordinates));

            // Don't display for accessible journey
            if (replanAvailableOutward && refineHelper.IsAccessibleJourney(outwardJourney))
            {
                replanAvailableOutward = false;
            }

			// Is return replan available.
            if (sm.JourneyResult.ReturnRoadJourneyCount > 0 || sm.JourneyResult.ReturnPublicJourneyCount > 0)
            {
                Journey returnJourney = (Journey)(sm.JourneyViewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal ? (Journey)sm.JourneyResult.ReturnPublicJourney(sm.JourneyViewState.SelectedReturnJourneyID) : (Journey)sm.JourneyResult.ReturnRoadJourney());
                replanAvailableReturn = (!(returnJourney.JourneyLegs.Length == 1 && returnJourney.JourneyLegs[0].HasInvalidCoordinates));

                // Don't display for accessible journey
                if (replanAvailableReturn && refineHelper.IsAccessibleJourney(returnJourney))
                {
                    replanAvailableReturn = false;
                }
            }

			// Determine which replans are available and set control visibility
			// accordingly.
			replanPanel.Visible = ( replanAvailableOutward || replanAvailableReturn );

			if (replanPanel.Visible)
			{
				labelReplanOptionOne.Visible = replanAvailableOutward;
				scriptableRadioButtonReplanOutward.Visible = replanAvailableOutward;

				labelReplanOptionTwo.Visible = replanAvailableReturn;
				scriptableRadioButtonReplanReturn.Visible = replanAvailableReturn;
			
				// Load resource strings based on control visibility.
				if (labelReplanOptionOne.Visible)
				{
					labelReplanOptionOne.Text = ( sm.JourneyViewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal ? GetResource("RefineJourney.labelReplanOptionOneCar") : GetResource("RefineJourney.labelReplanOptionOnePublic") ); 
				}

				if (labelReplanOptionTwo.Visible)
				{
					labelReplanOptionTwo.Text = ( sm.JourneyViewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal ? GetResource("RefineJourney.labelReplanOptionTwoCar") : GetResource("RefineJourney.labelReplanOptionTwoPublic") );
				}
			}
		}

        /// <summary>
        /// Sets visibility on the Amend controls
        /// </summary>
        private void HandleAmendControlVisibility()
        {
            // Amend is always shown

            ITDSessionManager sessionManager = TDSessionManager.Current;
			ResultsAdapter helper = new ResultsAdapter();
			RefineHelper refineHelper = new RefineHelper();

			Journey outwardJourney = helper.GetRequiredJourney(sessionManager.JourneyResult, 
                sessionManager.CycleResult, sessionManager.JourneyViewState, false);
            Journey returnJourney = helper.GetRequiredJourney(sessionManager.JourneyResult, 
                sessionManager.CycleResult, sessionManager.JourneyViewState, true);
                        
            // Display the accessible journey note if required
            if ((refineHelper.IsAccessibleJourney(outwardJourney)) || (refineHelper.IsAccessibleJourney(returnJourney)))
            {
                rowAccessibleNote.Visible = true;

                labelAccessibleNote.Text = GetResource("RefineJourney.labelAccessibleNote");
            }
        }


		/// <summary>
		/// Sets the text for the skip to links (for screenreader browsers).
		/// </summary>
		private void SetupSkipLinksAndScreenReaderText()
		{

			// Setup gif resource for images (1 invisible image for all skip links)
			string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			imageMainContentSkipLink1.ImageUrl = skipLinkImageUrl;
			imageMainContentSkipLink1.AlternateText = GetResource("RefineJourneyPlan.imageMainContentSkipLink.AlternateText");

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			ExtraWiringEvents();
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
		/// Wire up extra event handlers
		/// </summary>
		private void ExtraWiringEvents()
		{
			this.headerControl.DefaultActionEvent += new System.EventHandler(this.buttonNext_Click);
			this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
			this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
			this.buttonNewJourney.Click += new System.EventHandler(this.buttonNewJourney_Click);
		}

		#endregion


		/// <summary>
		/// Back button event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonBack_Click(object sender, System.EventArgs e)
		{
            //Log the Amend Journey selection
			PageEntryEvent logpage = new PageEntryEvent(PageId.ModifyBackClicked, Session.SessionID, TDSessionManager.Current.Authenticated);
			Logger.Write(logpage);

			// Send user back to the journey results summary page.
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.RefineJourneyBack;
		}

		/// <summary>
		/// New journey button event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonNewJourney_Click(object sender, System.EventArgs e)
		{
			//Log the New Journey selection
			PageEntryEvent logpage = new PageEntryEvent(PageId.ModifyNewClicked, Session.SessionID, TDSessionManager.Current.Authenticated);
			Logger.Write(logpage);

			RefineHelper refineHelper = new RefineHelper();
			refineHelper.NewJourney();
		}

		/// <summary>
		/// Next button event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonNext_Click(object sender, System.EventArgs e)
		{

			bool radioChecked = false;
			ITDSessionManager sessionManager = TDSessionManager.Current;

			// Extend handling
			if(extendJourneyOptionsControl.ScriptableRadioButtonExtendDirection1.Checked)
			{

				//Log the Extend Outward selection
				PageEntryEvent logpage = new PageEntryEvent(PageId.ModifyExtendOutwardClicked, Session.SessionID, TDSessionManager.Current.Authenticated);
				Logger.Write(logpage);

				radioChecked = true;
				sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.ExtendJourneyInputStart;
			}

			if(extendJourneyOptionsControl.ScriptableRadioButtonExtendDirection2.Checked)
			{

				//Log the Extend Return selection
				PageEntryEvent logpage = new PageEntryEvent(PageId.ModifyExtendReturnClicked, Session.SessionID, TDSessionManager.Current.Authenticated);
				Logger.Write(logpage);

				radioChecked = true;
				sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.ExtendJourneyInputEnd;
			}

			// Replan handling
			if (scriptableRadioButtonReplanOutward.Checked)
			{
				//Log the Replace Outward selection
				PageEntryEvent logpage = new PageEntryEvent(PageId.ModifyReplaceOutwardClicked, Session.SessionID, TDSessionManager.Current.Authenticated);
				Logger.Write(logpage);

				radioChecked = true;
				sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyReplanOutward;
			}

			if (scriptableRadioButtonReplanReturn.Checked)
			{
				//Log the Replace Return selection
				PageEntryEvent logpage = new PageEntryEvent(PageId.ModifyReplaceReturnClicked, Session.SessionID, TDSessionManager.Current.Authenticated);
				Logger.Write(logpage);

				radioChecked = true;
				sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyReplanReturn;
			}

			// Adjust handling
			if(scriptableRadioButtonAdjustOutward.Checked)
			{
				//Log the Adjust Outward selection
				PageEntryEvent logpage = new PageEntryEvent(PageId.ModifyAdjustOutwardClicked, Session.SessionID, TDSessionManager.Current.Authenticated);
				Logger.Write(logpage);

				radioChecked = true;
				sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyAdjustOutward;
			}
			
			if(scriptableRadioButtonAdjustReturn.Checked)
			{
				//Log the Adjust Return selection
				PageEntryEvent logpage = new PageEntryEvent(PageId.ModifyAdjustReturnClicked, Session.SessionID, TDSessionManager.Current.Authenticated);
				Logger.Write(logpage);

				radioChecked = true;
				sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyAdjustReturn;
			}

			if(scriptableRadioButtonAmend.Checked)
			{
				//Log the Amend Journey selection
				PageEntryEvent logpage = new PageEntryEvent(PageId.ModifyAmendClicked, Session.SessionID, TDSessionManager.Current.Authenticated);
				Logger.Write(logpage);

				// redirect user to the appropriate input page.
				if (sessionManager.FindAMode == FindAMode.None)
					sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputDefault;
				else
				{
					sessionManager.FindPageState.PrepareForAmendJourney();
					sessionManager.FormShift[SessionKey.TransitionEvent] = FindInputAdapter.GetTransitionEventFromMode(sessionManager.FindAMode);
				}
            
				// If the results have been added to the Itinerary then we need to get them back out again
				ExtendItineraryManager itineraryManager = TDItineraryManager.Current as ExtendItineraryManager;
			
				if ((itineraryManager != null) && (itineraryManager.Length > 0) && !itineraryManager.ExtendInProgress)
				{
					if (itineraryManager.Length == 1)
					{
						// The Initial journey is the only journey in the Itinerary
						itineraryManager.ResetToInitialJourney();
					}
					else
					{
						itineraryManager.ResetLastExtension();
					}
				}

				// If we are in park and ride we need to redirect to the park and ride page
				TDItineraryManager currentItineraryManager = TDItineraryManager.Current;
				if (currentItineraryManager.JourneyParameters.DestinationLocation.ParkAndRideScheme != null)
				{
					currentItineraryManager.JourneyParameters.DestinationLocation.Status = TDLocationStatus.Unspecified;
					sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.ParkAndRideInput;
				}

				// Call method to invalidate and reset the results on the session.
				InvalidateAndResetResults();


			}

			if (radioChecked == false)
			{
				String[] errorsArray = new String[1] {GetResource("RefineJourney.ErrorDisplayControl.NoSelectedMade")};
				errorDisplayControl1.ErrorStrings = errorsArray;
			}
		}

		/// <summary>
		/// Invalidates current journey result on the session and also clears other results
		/// dependant on cost or time based mode.
		/// </summary>
		private void InvalidateAndResetResults()
		{
			ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];

			// Invalidate the current journey result. Set the mode for which the results pertain to as being
			// none so that clicking the Find A tab will then redirect to the default find A input page.
			if (sessionManager.JourneyResult != null) 
			{ 
				sessionManager.JourneyResult.IsValid = false; 
			} 

			//Reset results dependent on mode.
			if (FindInputAdapter.IsCostBasedSearchMode(sessionManager.FindAMode)) 
			{ 
				//Reset search result.
				((FindCostBasedPageState)sessionManager.FindPageState).SearchResult = null; 
			} 
			else 
			{ 
				TDItineraryManager.Current.PricingRetailOptions = null; 
			}

		}

	}
}
