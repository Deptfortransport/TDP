// *********************************************** 
// NAME                 : JourneyChangeSearchControl.ascx.cs 
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 13.05.04
// DESCRIPTION			: Contains the buttons that invoke another journey search
//                        either by clearing existing parameters (new search) or by
//                        modifying them (amend search)
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyChangeSearchControl.ascx.cs-arc  $
//
//   Rev 1.9   Oct 27 2010 15:35:42   RBroddle
//Removed explicit wire up to Page_Load as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.8   Apr 23 2010 11:59:50   mmodi
//Set page state to be in Amend mode when Plan a return button selected to allow input page to handle as amending a journey
//Resolution for 5521: TD Extra - Drop Down List Change - CCN0575
//
//   Rev 1.7   Feb 17 2010 15:13:34   apatel
//International planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.6   Feb 16 2010 11:15:36   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.5   Oct 12 2009 09:11:30   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.5   Oct 12 2009 08:40:02   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.4   Feb 13 2009 11:01:46   apatel
//Hide the help button on Journey Overview page when its coming from city to city journey Input page
//Resolution for 5246: City to City Journey Result page Help Button problem
//
//   Rev 1.3   Oct 13 2008 16:41:42   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.0   Jun 20 2008 14:32:32   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:21:14   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Mar 07 2008 09:21:00 apatel
//  moved amend button logic from visitplannerresults page to this control.
//
//  Rev DevFactory Feb 29 2008 09:37 apatel
//  Added generic back button whose events should be handled by the page using this control. This control is set to visible false by default.
//
//   Rev 1.0   Nov 08 2007 13:15:12   mturner
//Initial revision.
//
//   Rev DevFactory Jan 20 2008 19:00:00 dgath
//CCN0382b City to City enhancements:
//Updated to add a back button when FindPageState.Mode=FindAMode.Trunk
//(city to city journey) to take the user back to the JourneyOverview page.
//Back button only visible when on Summary, Details, Map, and Tickets/Cost pages
//
//   Rev 1.47   Nov 22 2006 15:21:16   mturner
//Changed to redirect to rail input page when user amends a journey from the 'find cheaper' pages
//
//   Rev 1.46   Nov 22 2006 13:10:18   mturner
//Changed to allow redirects to the RSBP page when a user has selected the 'find cheaper fares' link from Time based planning.
//
//   Rev 1.45   Nov 16 2006 16:27:14   tmollart
//Modified logic so that New/Amend buttons are not not made INVISIBLE if mode is either FindFare or RailCost.
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.44   Oct 06 2006 14:34:06   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.43.1.0   Oct 03 2006 10:49:38   mmodi
//Added check for IsFromNearestCarParks
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4208: Car Parking: New Search a car park journey goes to Car route input
//
//   Rev 1.43   Apr 26 2006 12:15:04   RPhilpott
//Manual merge of stream 35
//
//   Rev 1.42   Mar 29 2006 16:32:36   tolomolaiye
//Fix for IR 3751
//Resolution for 3751: Park and Ride: Amend causes an error
//
//   Rev 1.41   Mar 23 2006 17:41:12   tmollart
//Manual merge of stream 0025.
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.40   Mar 22 2006 11:49:24   NMoorhouse
//TD090, IR3649 - Undo of Extension functionality no longer possible (hide the relevant buttons)
//Resolution for 3649: Extend, Replan & Adjust: Issues with Replan of second direction
//
//   Rev 1.39   Mar 13 2006 16:39:12   pcross
//Manual merge for stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.35.1.2   Mar 08 2006 11:13:30   tmollart
//Put in quick fix so back button wont appear on the replan input page.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.35.1.1   Jan 19 2006 11:36:16   NMoorhouse
//Hide the amend button for new page Journey Replan Input Page
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.35.1.0   Dec 20 2005 19:09:08   rhopkins
//Change to enable Extend to work with new TDItineraryManager.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.38   Feb 23 2006 16:11:38   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.37   Feb 10 2006 12:24:46   tolomolaiye
//Merge for stream 3180 - Homepage Phase 2
//
//   Rev 1.36   Jan 27 2006 18:24:34   jbroome
//Ensure Back button is never displayed for Visit Planner
//
//   Rev 1.35   Nov 30 2005 17:56:44   rhopkins
//Corrections to the button alignments
//Resolution for 3216: UEE: Javascript disabled - Printer friendly button uses flat style
//Resolution for 3242: UEE: Netscape - buttons overlap with journey summary on results page
//
//   Rev 1.34.1.1   Dec 22 2005 11:04:18   tmollart
//Removed references to OldJourneyParameters.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.34.1.0   Dec 12 2005 17:14:22   tmollart
//Removed references to OldFindAMode and changed where applicable to FindAMode.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.34   Nov 10 2005 11:39:18   ralonso
//buttonAmendJourney.Visible = true;
//
//   Rev 1.33   Nov 09 2005 16:58:48   jgeorge
//Manual merge for stream2818 (Search by Price)
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.32   Nov 07 2005 12:07:24   ralonso
//image buttons changed to tdbuttons
//
//   Rev 1.31   Nov 04 2005 14:46:30   ECHAN
//merged stream2816 for DEL8
//
//   Rev 1.30   Nov 02 2005 16:11:58   tmollart
//Fixed slight merge error.
//Resolution for 2929: Visit Planner Merge Activity
//
//   Rev 1.29   Nov 01 2005 09:30:08   tolomolaiye
//Merge for stream 2638 (Visit Planner)
//Resolution for 2929: Visit Planner Merge Activity
//
//   Rev 1.27.2.1   Oct 29 2005 14:12:42   tmollart
//Added code to handle New button click when itinerary is a visit plan.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.27.2.0   Oct 18 2005 14:46:00   tolomolaiye
//Hide extension buttons for Visist Planner
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.28   Sep 29 2005 12:48:32   build
//Automatically merged from branch for stream2673
//
//   Rev 1.27.1.1   Sep 22 2005 11:04:40   rgreenwood
//DN077 TD088 Code review actions: Moved logging outside of if statement
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//Resolution for 2771: DEL 8 Stream: PageEntry reporting for Extend functionality
//
//   Rev 1.27.1.0   Sep 13 2005 09:46:28   rgreenwood
//DN079 UEE TD088 JourneyExtension Tracking: Added logging for LastUndo and All Undo button clicks
//
//   Rev 1.27   Apr 15 2005 12:48:00   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.26   Apr 11 2005 16:00:32   tmollart
//Added InvalidateAndResetResults method that is called by the New and Amend button event handlers. This invalidates journey results and clears results on the required partition.
//
//   Rev 1.25   Apr 01 2005 16:42:20   tmollart
//Modified method for New and Amend button clicks so that the CostSearchWaitStateData object is set to null when these buttons are pressed.
//
//   Rev 1.24   Mar 17 2005 14:36:26   rhopkins
//Modified to cope with possibility that JourneyResults is null
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.23   Feb 25 2005 14:15:06   rhopkins
//Added button for Printer Friendly Page
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.22   Nov 03 2004 12:54:06   passuied
//Changes to enable a new FindAMode TrunkStation similar to Trunk but with differences...
//
//   Rev 1.21   Nov 01 2004 18:04:32   passuied
//changed for new search and amend journey when in trunk mode
//
//   Rev 1.20   Oct 05 2004 14:18:24   passuied
//Redirected to StationInput when in trunk mode
//
//   Rev 1.19   Sep 30 2004 13:13:08   jbroome
//Extend Journey additional changes (DD02101d)
//
//   Rev 1.18   Sep 28 2004 17:33:32   passuied
//added code to reset the PricingRetailerOptions object when doing a new searc or amend journey. That will stop from getting the previous fares when searching a new/amended journey
//Resolution for 1638: Find a coach - Train fare is displayed in Find a coach
//
//   Rev 1.17   Sep 22 2004 15:48:56   jbroome
//Ensure correct display of New Search button.
//
//   Rev 1.16   Sep 17 2004 12:59:46   jbroome
//Final tweaks to Extend Journey Usability changes.
//
//   Rev 1.15   Sep 17 2004 12:25:52   jbroome
//Added missing comments.
//
//   Rev 1.14   Sep 17 2004 12:11:22   jbroome
//IR1591 - Extend Journey Usability Changes

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.ReportDataProvider.TDPCustomEvents;
    using TransportDirect.Web.Support;
    using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Common.ServiceDiscovery;
    using TransportDirect.UserPortal.Resource;
    using TransportDirect.Common;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.UserPortal.LocationService;

	using Logger = System.Diagnostics.Trace;
    using TransportDirect.UserPortal.JourneyPlanRunner;


	/// <summary>
    ///	Contains the buttons that invoke another journey search
    /// either by clearing existing parameters (new search) or by
    /// modifying them (amend search)
	/// </summary>
	public partial  class JourneyChangeSearchControl : TDUserControl
	{
		
		#region Instance Members
		/// <summary>
		/// DIV that contains the Back buttons (not always displayed)
		/// </summary>
		protected System.Web.UI.HtmlControls.HtmlGenericControl backButtons;
		/// <summary>
		/// Button to invoke new search
		/// </summary>
		protected TransportDirect.UserPortal.Web.Controls.TDButton buttonNewSearch;
		/// <summary>
		/// Button to invoke new search for current extension
		/// </summary>
		protected TransportDirect.UserPortal.Web.Controls.TDButton buttonRedoExtension;
		/// <summary>
		/// Button that removes last extension added to the itinerary and
		/// allows user to reselect a different journey from same set of journey results
		/// </summary>
		protected TransportDirect.UserPortal.Web.Controls.TDButton backToExtensionSummaryButton;
		/// <summary>
		/// Button that removes last extension added to the itinerary;
		/// shown when extension has not yet been added to Itinerary
		/// </summary>
		protected TransportDirect.UserPortal.Web.Controls.TDButton undoThisExtensionButton;
		/// <summary>
		/// Button that removes last extension added to the itinerary;
		/// shown after extension has been added to Itinerary
		/// </summary>
		protected TransportDirect.UserPortal.Web.Controls.TDButton undoLastExtensionButton;
		/// <summary>
		/// Button that removes all journeys from the itinerary and
		/// allows user to reselect a different journey from initial set of journey results
		/// </summary>
		protected TransportDirect.UserPortal.Web.Controls.TDButton undoAllExtensionsButton;
		/// <summary>
		/// Button that allows user to amend the original journey request via the Journey Planner screen
		/// </summary>
		protected TransportDirect.UserPortal.Web.Controls.TDButton buttonAmendJourney;
		/// <summary>
		/// Button that allows user to amend the extended journey request via the Journey Planner screen
		/// </summary>
		protected TransportDirect.UserPortal.Web.Controls.TDButton buttonAmendExtension;
		/// <summary>
		/// Button that allows user to reselect a different journey from initial set of journey results
		/// when the initial journey is the only journey currently in the itinerary.
		/// </summary>
		protected TransportDirect.UserPortal.Web.Controls.TDButton backToInitialResultsSummaryButton;
        /// <summary>
        /// Button that returns to the Journey Overview page.
        /// </summary>
        protected TransportDirect.UserPortal.Web.Controls.TDButton buttonBackJourneyOverview;
		/// <summary>
		/// Button that opens a printer-friendly version of the current page in a new window
		/// </summary>
		protected PrinterFriendlyPageButtonControl printerFriendlyPageButton;
		/// <summary>
		/// Help custom control for in-Page Help
		/// </summary>
		protected TransportDirect.UserPortal.Web.Controls.HelpCustomControl pageHelpCustomControl;
		/// <summary>
		/// Help button for new-Page Help
		/// </summary>
		protected TransportDirect.UserPortal.Web.Controls.HelpButtonControl pageHelpButton;

		private string helpLabel;
		private string helpUrl;

        //CCN 0427 Private variable for generic back button visibility 
        private bool genericBackButtonVisible = false;

		#endregion

		# region Page Load and Pre Render
		protected void Page_Load(object sender, System.EventArgs e)
		{
			buttonRedoExtension.Text = GetResource("JourneyChangeSearchControl.buttonNewExtensionSearch.Text");
			undoThisExtensionButton.Text = GetResource("JourneyExtensionControl.undoThisExtensionButton.Text");
			undoLastExtensionButton.Text = GetResource("JourneyExtensionControl.undoLastExtensionButton.Text");
			undoAllExtensionsButton.Text = GetResource("JourneyExtensionControl.undoAllExtensionsButton.Text");
			backToInitialResultsSummaryButton.Text = GetResource("JourneyExtensionControl.backToResultsButton.Text");
            buttonBackJourneyOverview.Text = GetResource("JourneyChangeSearchControl.buttonBackJourneyOverview.Text");
			backToExtensionSummaryButton.Text = GetResource("JourneyExtensionControl.backToSummaryButton.Text");
			buttonAmendJourney.Text = GetResource("JourneyChangeSearchControl.buttonAmendJourney.Text");
			buttonAmendExtension.Text = GetResource("JourneyChangeSearchControl.buttonAmendExtensionSearch.Text");
			buttonNewSearch.Text = GetResource("JourneyChangeSearchControl.buttonNewSearch.Text");
            buttonReturnJourney.Text = GetResource("JourneyChangeSearchControl.buttonReturnJourney.Text");

            //CCN 0427 Added generic back button 
            backButton.Text = GetResource("JourneyChangeSearchControl.genericBackButton.Text");

			pageHelpCustomControl.HelpLabel = helpLabel;
			pageHelpButton.HelpUrl = helpUrl;

			SetButtonVisibility();
		}

		protected override void OnPreRender(System.EventArgs e)
		{
			if (TDItineraryManager.Current.ItineraryManagerModeChanged)
			{
				SetButtonVisibility();
			}
            backButton.Visible = GenericBackButtonVisible;
		}
		#endregion
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			ExtraWiringEvents();
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}

		/// <summary>
		/// Sets up the necessary event handlers.
		/// </summary>
		private void ExtraWiringEvents() 
		{
			buttonNewSearch.Click += new EventHandler(this.buttonNewSearch_Click);
			buttonAmendJourney.Click += new EventHandler(this.buttonAmendJourney_Click);
			buttonAmendExtension.Click += new EventHandler(this.buttonAmendJourney_Click);
			buttonRedoExtension.Click += new EventHandler(this.buttonRedoExtension_Click);
			undoLastExtensionButton.Click += new EventHandler(this.undoLastExtensionButton_Click);
			undoThisExtensionButton.Click += new EventHandler(this.undoLastExtensionButton_Click);
			undoAllExtensionsButton.Click += new EventHandler(this.undoAllExtensionsButton_Click);
			backToInitialResultsSummaryButton.Click += new EventHandler(this.undoAllExtensionsButton_Click);
			backToExtensionSummaryButton.Click += new EventHandler(this.backToExtensionSummaryButton_Click);
            buttonBackJourneyOverview.Click += new EventHandler(this.buttonBackJourneyOverview_Click);
            buttonReturnJourney.Click += new EventHandler(buttonReturnJourney_Click);
		}

		#endregion

		#region SetButtonVisibility
		/// <summary>
		/// Sets visibility of extend journey buttons. 
		/// </summary>
		public void SetButtonVisibility() 
		{
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

			// Make appropriate Help button visible, if required.
			pageHelpCustomControl.Visible = ((helpLabel != null) && (helpLabel.Length > 0));
			pageHelpButton.Visible = ((helpUrl != null) && (helpUrl.Length > 0));

			//TD90: undo extension functionality not currectly available anymore
			//left incase it comes back into scope 
			undoAllExtensionsButton.Visible = false;
			undoLastExtensionButton.Visible = false;
			undoThisExtensionButton.Visible = false; 

			// If itinerary exists
			if(itineraryManager.Length > 0) 
			{
				System.Type itineraryType = TDItineraryManager.Current.GetType();

				// If extend is in progress
				if(itineraryManager.ExtendInProgress) 
				{
					backButtons.Visible = true;
					buttonAmendExtension.Visible = true;
					backToExtensionSummaryButton.Visible = false;
					backToInitialResultsSummaryButton.Visible = false;

					buttonRedoExtension.Visible = false;
					buttonAmendJourney.Visible = false;
                    buttonBackJourneyOverview.Visible = false;
				}
				else 
				{
					if (itineraryManager.Length == 1) 
					{
						backButtons.Visible = true;
						buttonAmendExtension.Visible = false;
						backToExtensionSummaryButton.Visible = false;
						backToInitialResultsSummaryButton.Visible = true;

						buttonRedoExtension.Visible = false;
						buttonAmendJourney.Visible = false;
                        buttonBackJourneyOverview.Visible = false;
					}
					else // More than one journey in itinerary
					{
						if (itineraryManager.LatestExtensionAvailable)
						{
							backButtons.Visible = true;
							buttonAmendExtension.Visible = false;
							backToExtensionSummaryButton.Visible = true;
							backToInitialResultsSummaryButton.Visible = false;

							buttonRedoExtension.Visible = false;
							buttonAmendJourney.Visible = false;
                            buttonBackJourneyOverview.Visible = false;
						}
						else
						{
							// We cannot identify the most recent extension so we cannot show buttons to manipulate it.
							backButtons.Visible = false;

							buttonRedoExtension.Visible = false;
							buttonAmendJourney.Visible = false;
						}
					}
				}

				// Visibility of back button is always false for Visit Planner
				if (itineraryType == typeof(VisitPlannerItineraryManager))
				{
					backButtons.Visible = false;
				}

			}
			else
			{
				// Not using itinerary - hide all extend journey buttons. 

				backButtons.Visible = false;

				buttonRedoExtension.Visible = false;
				buttonAmendJourney.Visible = true;

            }

            #region Specific scenarios
            // Quick fix for replan. Not the best but saves redoing this whole area
			if (this.PageId == PageId.JourneyReplanInputPage)
			{
				buttonAmendJourney.Visible = false;
				backButtons.Visible = false;
			}

            // Show back button if arriving via City to city, because we want user to be
            // able to return to the JourneyOverview screen
            if ((this.PageId != PageId.JourneyOverview)
                && ((TDSessionManager.Current.FindAMode == FindAMode.Trunk) || (TDSessionManager.Current.FindAMode == FindAMode.International)))
            {
                backButtons.Visible = true;

                buttonBackJourneyOverview.Visible = true;

                buttonAmendExtension.Visible = false;
                backToExtensionSummaryButton.Visible = false;
                backToInitialResultsSummaryButton.Visible = false;

            }

            //Hide help button if arriving via City to city.
            if ((this.PageId == PageId.JourneyOverview)
                && ((TDSessionManager.Current.FindAMode == FindAMode.Trunk) || (TDSessionManager.Current.FindAMode == FindAMode.International)))
            {
                pageHelpCustomControl.Visible = false;

            }


			//	If we are in cost-based partition, and find-a-fare not enabled, 
			//   we have got here from find-cheaper and do not want to allow
			//   user to get back to the Find-A-Fare initial input page. 

			if	(TDSessionManager.Current.Partition == TDSessionPartition.CostBased)
			{
				if	(!FindInputAdapter.FindAFareAvailable && !FindInputAdapter.FindAFareAvailableRailCost)
				{
					buttonAmendJourney.Visible = false;
					backButtons.Visible = false;
					buttonNewSearch.Visible = false;
				}
            }

            // CCN 0427 if page id is VisitPlannerResults show amend button
            // as it moved to top left corner
            if (this.PageId == PageId.VisitPlannerResults)
            {
                buttonAmendJourney.Visible = true;
            }

            //International Planner return journey button
            buttonReturnJourney.Visible = (TDSessionManager.Current.FindAMode == FindAMode.International);

            
            #endregion
        }
		#endregion

		#region Public Properties

		/// <summary>
		/// Exposes the Print button control that links to a printer-friendly page.
		/// The Page ID of the printer-friendly page is the current page ID prefixed with "Printable".
		/// If no printer-friendly page is available then the Print button will not be shown.
		/// </summary>
		public PrinterFriendlyPageButtonControl PrinterFriendlyPageButton
		{
			get { return printerFriendlyPageButton; }
			set { printerFriendlyPageButton = value; }
		}

		/// <summary>
		/// Gets/Sets the Help Label that will be used by the HelpControl.
		/// The HelpControl provides help within the current page.
		/// If non-empty this will cause the HelpControl to be displayed.
		/// If empty the HelpControl will not be displayed.
		/// </summary>
		public string HelpLabel
		{
			get { return helpLabel; }
			set { helpLabel = value; }
		}

		/// <summary>
		/// Gets/Sets the Help URL that will be used by the HelpButton.
		/// The HelpButton provides help on a new page.
		/// If non-empty this will cause the HelpButton to be displayed.
		/// If empty the HelpButton will not be displayed.
		/// </summary>
		public string HelpUrl
		{
			get { return helpUrl; }
			set { helpUrl = value; }
		}

		/// <summary>
		/// Exposes the Help button, so that pages can apply special handling if required
		/// </summary>
		public HelpCustomControl HelpCustomControl
		{
			get { return pageHelpCustomControl; }
		}

		/// <summary>
		/// Exposes the Help button, so that pages can apply special handling if required
		/// </summary>
		public HelpButtonControl HelpButton
		{
			get { return pageHelpButton; }
		}

        /// <summary>
        /// Read only property giving access to the generic back button
        /// </summary>
        public TDButton GenericBackButton
        {
            get { return backButton; }
        }


        public TDButton AmendButton
        {
            get { return buttonAmendJourney; }
        }


        /// <summary>
        /// Read/write property setting the visiblity of the generic back button
        /// </summary>
        public bool GenericBackButtonVisible
        {
            get { return genericBackButtonVisible; }
            set { genericBackButtonVisible = value; }
        }

		#endregion Public Properties

		#region Event Handlers
        /// <summary>
        /// Handle button click to redirect user to journey input page initialised with
        /// default journey parameters
        /// </summary>
        /// <param name="sender">Originator of event</param>
        /// <param name="e">Event parameters</param>
        private void buttonNewSearch_Click(object sender, EventArgs e)
		{

            ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];
            TDItineraryManager itineraryManager = TDItineraryManager.Current;

            // Redirect user to the input page appropriate for the mode used to plan the initial journey.
            // Determining the transition event must happen before resetting itinerary manager in order for
            // BaseJourneyFindAMode to return correct value.

            if (sessionManager.FindAMode == FindAMode.TrunkStation)
            {
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationInputDefault;
                sessionManager.SetOneUseKey(SessionKey.NotFindAMode, "true");
            }
            else 
            {
                FindAMode baseFindAMode = itineraryManager.BaseJourneyFindAMode;

                if (baseFindAMode == FindAMode.None) 
                {
                    sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputDefault;
                } 
                else 
                {
					// Redirect users to the RailSearchbyPrice page if they are in normal search 
					// by price.
					// Done during panic build of Del 9.1
					if (baseFindAMode == FindAMode.Fare)
					{
						sessionManager.FormShift[SessionKey.TransitionEvent] = FindInputAdapter.GetTransitionEventFromMode(FindAMode.Train);
					}
					// End of Hack
					// Otherwise redirect to appropriate page
					else
					{
						sessionManager.FormShift[SessionKey.TransitionEvent] = FindInputAdapter.GetTransitionEventFromMode(baseFindAMode);
					}
                }
            }

			// If we are in visit planner we need to redirect to a new place.
			if (sessionManager.ItineraryMode == ItineraryManagerMode.VisitPlanner)
			{
				sessionManager.JourneyParameters = new TDJourneyParametersVisitPlan();
				sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.VisitPlannerNewClear;

				VisitPlannerAdapter adaptor = new VisitPlannerAdapter();
				adaptor.ClearDownRequestAndResults(sessionManager.ItineraryManager, sessionManager.ResultsPageState);

			}
            
			// If we are in park and ride we need to redirect to the aprk and ride page
			if (itineraryManager.JourneyParameters.DestinationLocation.ParkAndRideScheme != null)
			{
				sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.ParkAndRideInput;
			}

			// If we are in Find nearest car park mode, then we need to redirect to Find car park input
			if (sessionManager.IsFromNearestCarParks)
			{
				sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarParkInputDefault;
			}

            // Reset the itinerary manager
			itineraryManager.NewSearch();

			// Flag new search button being clicked so that redirect page can perform any necessary initialisation
			sessionManager.SetOneUseKey(SessionKey.NewSearch,string.Empty);

            // invalidate the current journey result. Set the mode for which the results pertain to as being
            // none so that clicking the Find A tab will then redirect to the default find A input page.
			if (sessionManager.JourneyResult != null)
			{
				sessionManager.JourneyResult.IsValid = false;
			}

            // invalidate the current cycle result
            if (sessionManager.CycleResult != null)
            {
                sessionManager.CycleResult.IsValid = false;
            }

			if (sessionManager.ItineraryMode != ItineraryManagerMode.VisitPlanner)
			{
				// Call method to invalidate and reset the results on the session.
				InvalidateAndResetResults();
			}
        }

		/// <summary>
		/// Handle button click to redirect user to journey input page initialised with
		/// default journey parameters for Extension
		/// </summary>
		/// <param name="sender">Originator of event</param>
		/// <param name="e">Event parameters</param>
		private void buttonRedoExtension_Click(object sender, EventArgs e)
		{
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

			itineraryManager.DeleteLastExtension();
			if (itineraryManager.ExtendEndOfItinerary)
			{
				itineraryManager.ExtendFromItineraryEndPoint();
			}
			else
			{
				itineraryManager.ExtendToItineraryStartPoint();
			}

			// Redirect user to journey planner input page.
			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			sessionManager.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.JourneyPlannerInputDefault;
		}

        /// <summary>
        /// Handle button click to redirect user to journey input page initialised with
        /// existing journey parameters
        /// </summary>
        /// <param name="sender">Originator of event</param>
        /// <param name="e">Event parameters</param>
        private void buttonAmendJourney_Click(object sender, EventArgs e)
		{
            if(this.PageId == PageId.VisitPlannerResults)
            {
                

                //invoke the appropriate transition
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.VisitPlannerAmend;
                return;
            }
            ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];
            
            // redirect user to the appropriate input page.
            // CCN 0427 moved amend button logic for visitplannerresults to this control
			if (sessionManager.FindAMode == FindAMode.None)
                
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputDefault;
			else
			{
				sessionManager.FindPageState.PrepareForAmendJourney();

				// Redirect users to the RailSearchbyPrice page if they are in normal search 
				// by price.
				// Done during panic build of Del 9.1
				if (sessionManager.FindAMode == FindAMode.Fare)
				{
					sessionManager.FormShift[SessionKey.TransitionEvent] = FindInputAdapter.GetTransitionEventFromMode(FindAMode.Train);
				}
				// End of Hack
				// Otherwise redirect to appropriate page
				else
				{
					sessionManager.FormShift[SessionKey.TransitionEvent] = FindInputAdapter.GetTransitionEventFromMode(sessionManager.FindAMode);
				}
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

		/// <summary>
		/// Handle button click to remove last extension added to the itinerary
		/// </summary>
		/// <param name="sender">Originator of event</param>
		/// <param name="e">Event parameters</param>
		private void undoLastExtensionButton_Click(object sender, EventArgs e)
		{
			ExtendItineraryManager itineraryManager = TDItineraryManager.Current as ExtendItineraryManager;
			if (itineraryManager != null)
			{
				if (itineraryManager.ExtendedFromInitialResultsPage && (itineraryManager.Length <= 2))
				{
					// Itinerary only consists of Initial Journey plus this Extension and the User didn't go through the Extend Journey intermediate page
					itineraryManager.ResetToInitialJourney();
				}
				else
				{
					itineraryManager.DeleteLastExtension();
				}
			}

			//DN079 UEE Journey Extension Tracking
			//Log this button click event
			PageEntryEvent logPage = new PageEntryEvent(PageId.JourneyExtensionLastUndo, Session.SessionID, TDSessionManager.Current.Authenticated);
			Logger.Write(logPage);
		}

		/// <summary>
		/// Handle button click to remove all extensions added to the itinerary
		/// </summary>
		/// <param name="sender">Originator of event</param>
		/// <param name="e">Event parameters</param>
		private void undoAllExtensionsButton_Click(object sender, EventArgs e)
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;
			ExtendItineraryManager itineraryManager = TDItineraryManager.Current as ExtendItineraryManager;

			if (itineraryManager != null)
			{
				itineraryManager.ResetToInitialJourney();
			}
			
			//DN079 UEE Journey Extension Tracking
			//Log this button click event
			PageEntryEvent logPage = new PageEntryEvent(PageId.JourneyExtensionAllUndo, Session.SessionID, TDSessionManager.Current.Authenticated);
			Logger.Write(logPage);
		}

		/// <summary>
		/// Handle button click to remove last extension added to the itinerary and
		/// allow user to reselect a different journey from same set of journey results
		/// </summary>
		/// <param name="sender">Originator of event</param>
		/// <param name="e">Event parameters</param>
		private void backToExtensionSummaryButton_Click(object sender, EventArgs e)
		{
			ExtendItineraryManager itineraryManager = TDItineraryManager.Current as ExtendItineraryManager;

			if (itineraryManager != null)
			{
				itineraryManager.ResetLastExtension();
			}

			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current [ServiceDiscoveryKey.SessionManager];

			sessionManager.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.GoJourneySummary;
		}

        /// <summary>
        /// Handle button click to redirect user to journey overview page 
        /// </summary>
        /// <param name="sender">Originator of event</param>
        /// <param name="e">Event parameters</param>
        private void buttonBackJourneyOverview_Click(object sender, EventArgs e)
        {
            ITDSessionManager sessionManager =
                (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];

            sessionManager.FormShift[SessionKey.TransitionEvent] =
                TransitionEvent.GoJourneyOverview;
        }

        /// <summary>
        /// Handle return journey button click event for international planner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReturnJourney_Click(object sender, EventArgs e)
        {
            if (TDSessionManager.Current.FindAMode != FindAMode.International)
            {
                return;
            }

            
            ITDSessionManager sessionManager =
                (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];

            InputPageState pageState = sessionManager.InputPageState;

            // Treat as Amending a journey so input page sets controls appropriately, e.g.
            // makes input fields editable
            sessionManager.FindPageState.PrepareForAmendJourney();

            TDJourneyParameters journeyParams = sessionManager.JourneyParameters;

            LocationSearch origin = journeyParams.Origin;            
            TDLocation originLocation = journeyParams.OriginLocation;
            LocationSearch destination = journeyParams.Destination;
            TDLocation destinationLocation = journeyParams.DestinationLocation;

            //switch the Journey Parameter origin and destinations
            sessionManager.JourneyParameters.Origin = destination;
            sessionManager.JourneyParameters.OriginLocation = destinationLocation;
            sessionManager.JourneyParameters.Destination = origin;
            sessionManager.JourneyParameters.DestinationLocation = originLocation;
            sessionManager.JourneyParameters.IsReturnRequired = true;

            
            sessionManager.FormShift[SessionKey.TransitionEvent] = FindInputAdapter.GetTransitionEventFromMode(sessionManager.FindAMode);
            
            // Call method to invalidate and reset the results on the session.
            InvalidateAndResetResults();
        }

		#endregion

        #region Private methods

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

            // Invalidate the current cycle result
            if (sessionManager.CycleResult != null)
            {
                sessionManager.CycleResult.IsValid = false;
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

   		#endregion

	}
}