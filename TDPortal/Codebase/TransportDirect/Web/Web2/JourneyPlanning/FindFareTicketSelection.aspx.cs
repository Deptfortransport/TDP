// *********************************************** 
// NAME                 : FindFareTicketSelection.aspx.cs 
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 07/02/2005
// DESCRIPTION			: 
// This page is responsible for displaying all single tickets for a given mode
// and travel date selected on the FindFareDateSelection page. The user is able to
// change the travel dates within the range of the original search. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindFareTicketSelection.aspx.cs-arc  $
//
//   Rev 1.6   Oct 28 2010 16:36:12   RBroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this page so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.5   Jun 03 2010 09:34:40   mmodi
//Prevent double attach to the Init method - stops duplicate fares call being triggered on a submit
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.4   Mar 26 2010 11:39:10   mmodi
//Populate CJP user flag to allow fares debugging info to be shown
//Resolution for 5295: Search by Price - Journey planned is not to original station selected
//
//   Rev 1.3   Jan 26 2009 12:55:36   apatel
//No change.
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:24:30   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Feb 29 2008 11:19:00 apatel
// Changes made to show generic back button from JourneyChangeSelectionControl and removed the back button which was on page.
//
//   Rev 1.0   Nov 08 2007 13:29:30   mturner
//Initial revision.
//
//   Rev 1.16   Feb 23 2006 18:46:00   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.15   Feb 10 2006 10:55:38   aviitanen
//Manual merge for Homepage phase 2 (stream3180)
//
//   Rev 1.14   Dec 01 2005 15:16:18   rhopkins
//Corrections to the button alignments
//Resolution for 3216: UEE: Javascript disabled - Printer friendly button uses flat style
//Resolution for 3242: UEE: Netscape - buttons overlap with journey summary on results page
//
//   Rev 1.13.1.0   Nov 30 2005 17:57:14   rhopkins
//Corrections to the button alignments
//Resolution for 3216: UEE: Javascript disabled - Printer friendly button uses flat style
//Resolution for 3242: UEE: Netscape - buttons overlap with journey summary on results page
//
//   Rev 1.13   Nov 03 2005 16:16:44   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.12.1.1   Oct 24 2005 14:01:22   RGriffith
//Changes to accomodate AmendViewControl changes
//
//   Rev 1.12.1.0   Oct 17 2005 11:47:18   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.12   May 10 2005 17:41:26   rhopkins
//FxCop corrections
//
//   Rev 1.11   Apr 26 2005 11:04:24   COwczarek
//Add new hyperlink to singles ticket selection page that allows
//user to switch to return ticket selection page.
//Resolution for 2099: PT: Find A Fare singles ticket selection page needs a link to return ticket selection page
//
//   Rev 1.10   Apr 22 2005 11:45:30   rgeraghty
//Page_PreRender code updated to update the instructional text when there are no tickets left to select
//Resolution for 2066: PT - Train - Fare Ticket Selection - text needs to change when no tickets available
//
//   Rev 1.9   Apr 12 2005 14:51:26   tmollart
//Added code to PreRender event to save the currently selected list item.

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
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.CostSearch;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.Web.Templates
{

	/// <summary>
	/// This page is responsible for displaying all single tickets for a given mode
	/// and travel date selected on the FindFareDateSelection page. The user is able to
	/// change the travel dates within the range of the original search. 
	/// </summary>
	public partial class FindFareTicketSelection : TDPage, ILanguageHandlerIndependent
	{
        /// <summary>
        /// Label showing instructional text
        /// </summary>
		protected System.Web.UI.WebControls.Label instructionLabel;

        /// <summary>
        /// Page back button
        /// </summary>
		protected TransportDirect.UserPortal.Web.Controls.TDButton backButton;

        /// <summary>
        /// Page next button
        /// </summary>
		protected TransportDirect.UserPortal.Web.Controls.TDButton nextButton;

        /// <summary>
        /// Page note label
        /// </summary>
        protected System.Web.UI.WebControls.Label noteLabel;

        /// <summary>
        /// Control showing table of single tickets
        /// </summary>
		protected FindFareTicketSelectionControl ticketSelectionControl;

        /// <summary>
        /// Amend tool
        /// </summary>
		protected AmendSaveSendControl amendControl;

        /// <summary>
        /// Label showing messages returned from back end
        /// </summary>
        protected System.Web.UI.WebControls.Label messagesLabel;

		/// <summary>
		/// Panel containing label showing messages returned from back end
		/// </summary>
        protected System.Web.UI.WebControls.Panel messagePanel;

        /// <summary>
        /// Panel containing label showing instructional text
        /// </summary>
        protected System.Web.UI.WebControls.Panel instructionPanel;

        /// <summary>
        /// Session manager for current request used troughout lifecycle of page
        /// </summary>
        private ITDSessionManager sessionManager;

        /// <summary>
        /// Help display for page
        /// </summary>
        protected HelpLabelControl helpLabelControl;

        /// <summary>
        /// Helper class responsible for performing operations common to ticket selection pages
        /// </summary>
        protected FindFareTicketSelectionAdapter ticketSelectionAdapter;

		#region Constructors

        /// <summary>
        /// Constructor. Sets page id and local resource manager.
        /// </summary>
		protected FindFareTicketSelection()
		{
			pageId = PageId.FindFareTicketSelection;
			LocalResourceManager = TDResourceManager.FIND_A_FARE_RM;
		}

		#endregion Constructors

		#region Protected methods
		#endregion Protected methods

		#region Private methods

        /// <summary>
        /// Updates properties on the the ticket selection control with new values from page state
        /// </summary>
        private void updateTicketSelectionControl() 
        {
            FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;

            ticketSelectionControl.CJPUser = IsCJPUser();
            ticketSelectionControl.Tickets = 
                pageState.SingleOrReturnTicketTable;
            ticketSelectionControl.Single = true;
            ticketSelectionControl.SelectedIndex = pageState.SelectedSingleOrReturnTicketIndex;
            ticketSelectionControl.Outward = true;
            ticketSelectionControl.CurrentOutwardDate = pageState.SelectedSingleDate;
            ticketSelectionControl.OpenReturnOnly = 
                pageState.SelectedTravelDate.TravelDate.TicketType == TicketType.OpenReturn;
        }

		/// <summary>
		/// Initialisation performed for every page request. Initialises all labels, button
		/// image URLs, day selection control within amend tool and ticket selection control.
		/// </summary>
		private void initialiseControls() 
		{
            FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;

            updateTicketSelectionControl();

			instructionLabel.Text = GetResource("FindFareTicketSelection.instructionLabel.Text");
			noteLabel.Text = GetResource("FindFareTicketSelection.noteLabel.Text");

			TDResourceManager langstrings = TDResourceManager.GetResourceManagerFromCache(TDResourceManager.LANG_STRINGS);
			
			nextButton.Text = langstrings.GetString("FindFareTicketSelection.NextButton.Text");

            PageTitle = GetResource("FindFareTicketSelection.DefaultPageTitle");

            // Initialise day selection control within amend tool
			AmendCostSearchDayControl dayControl = amendControl.AmendCostSearchDayControl;
			dayControl.MinOutwardDate = pageState.SearchRequest.SearchOutwardStartDate;
			dayControl.MaxOutwardDate = pageState.SearchRequest.SearchOutwardEndDate;
			dayControl.ShowReturnDate = false;
			dayControl.CurrentOutwardDate = pageState.SelectedSingleDate;

            helpLabelControl.Text = GetResource("FindFareTicketSelectionHelpLabel");
            helpLabelControl.MoreHelpUrl = GetResource("FindFareTicketSelectionHelpLabel.moreURL");

            // Init control that allows partitions to be switched
            ticketSelectionAdapter.InitAmendViewControl(amendControl.AmendViewControl);

		}

		/// <summary>
		/// Performs initialisation when page is loaded for the first time.
		/// </summary>
		private void initialRequestSetup() 
		{
            ticketSelectionAdapter.CheckForResults();
		}

		/// <summary>
		/// Updates session data with control values for those controls that do not raise events
		/// to signal that user has changed values.
		/// </summary>
		private void updateOtherControls()
		{
		}

        /// <summary>
        /// Displays any error messages returned from back end processing and if errors present,
        /// causes ticket selection control to refresh with new results
        /// </summary>
        private void displayErrors()
        {
            FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;

            FindFareInputAdapter findFareInputAdapter = new FindFareInputAdapter(pageState, sessionManager);
            findFareInputAdapter.DisplayMessages(instructionPanel, messagePanel, messagesLabel);

            // Any errors that occurred during service processing should cause a new ticket
            // table to be regenerated in case some of the ticket information has changed
            // e.g. availability
            if (pageState.SearchResult.GetErrors().Length > 0) 
            {
                ticketSelectionAdapter.CreateDisplayableCostSearchTickets(
                    pageState.SelectedTravelDate.TravelDate.TicketType);
                updateTicketSelectionControl();
            }

            pageState.SearchResult.ClearErrors();

        }

        /// <summary>
        /// Sets up the mode for the FindFareStepsConrol
        /// </summary>
        private void SetUpStepsControl()
        {
            findFareStepsControl.Step = FindFareStepsControl.FindFareStep.FindFareStep2;
            findFareStepsControl.SessionManager = sessionManager;
            findFareStepsControl.PageState = sessionManager.FindPageState;
            findFareStepsControl.TicketSelectionAdapter = ticketSelectionAdapter;
        }

        #region Private helper method - CJP User status

        /// <summary>
        /// Method which returns true if user is a higher-level (e.g. CJP) user 
        /// </summary>
        private bool IsCJPUser()
        {
            bool userIsLoggedOn = TDSessionManager.Current.Authenticated;

            // Get the user's type
            int userType = userIsLoggedOn ? (int)TDSessionManager.Current.CurrentUser.UserType : (int)TDUserType.Standard;

            return (userType > 0);
        }

        #endregion

		#endregion Private methods

		#region Event handlers

        /// <summary>
        /// Event handler called when user changes the current travel date. Ticket data is
        /// retrieved from current search results for the new date and the ticket selection
        /// control is updated with this data.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void outwardDateChanged(object sender, System.EventArgs e) 
        {
            FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;

            // Get the new date
            pageState.SelectedSingleDate = amendControl.AmendCostSearchDayControl.CurrentOutwardDate;

            // Get the new TravelDate object for the new date
            ticketSelectionAdapter.UpdateTravelDate(
                pageState.SelectedTravelDate.TravelDate.TicketType);

            updateTicketSelectionControl();

        }

        /// <summary>
        /// Event handler called when user attempts to change session partition
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void switchPartition(object sender, EventArgs e) 
        {
            ticketSelectionAdapter.SwitchPartition(amendControl.AmendViewControl);
        }

        /// <summary>
		/// Event handler for page Load event fired by page
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{

            // CCN 0427 Setting the visibility of JourneyChangeSearchControl's generic back button
            this.journeyChangeSearchControl.GenericBackButtonVisible = true;

            sessionManager = TDSessionManager.Current;
            ticketSelectionAdapter = new FindFareTicketSelectionAdapter(sessionManager);

			// standard page processing
			if (Page.IsPostBack)
			{
				updateOtherControls();
			}
			else
			{
				initialRequestSetup();
			}

            initialiseControls();

            SetUpStepsControl();

            //Added for white labelling:
            ConfigureLeftMenu("FindFareTicketSelection.clientLink.BookmarkTitle", "FindFareTicketSelection.clientLink.LinkText", clientLink, expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for White-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFindFareTicketSelection);
            expandableMenuControl.AddExpandedCategory("Related links");
		}

		/// <summary>
		/// Performs page initialisation including event wiring.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Init(object sender, EventArgs e)
		{
            amendControl.AmendCostSearchDayControl.OutwardDateChanged += new EventHandler(outwardDateChanged);
            amendControl.AmendViewControl.SubmitButton.Click += new EventHandler(switchPartition);
        }

		/// <summary>
		/// Event handler for page PreRender event fired by page
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			//Save the current selected ticket onto the page state before the control is rendered.
			FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;
			pageState.SelectedSingleOrReturnTicketIndex = ticketSelectionControl.SelectedIndex;

            displayErrors();

            // Only allow user to continue if a ticket has been selected
            // (must be checked after calling displayErrors since index may be changed)
            ticketSelectionAdapter.EnableNextButton(nextButton);

			// if there is nothing left that the user can select if the next button is hidden
			if (!nextButton.Visible) 
			{	
				//hide the errors panel as we are only interested in displaying updated instructional text
				messagePanel.Visible = false;
				//display the instructional panel and update the instructional text
				instructionPanel.Visible = true;				
				instructionLabel.Text = GetResource("FindFareTicketSelection.instructionLabel.NoTickets.Text");
			}

		}

        /// <summary>
        /// Event handler called when back button clicked.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void backButton_Click(object sender, EventArgs e)
        {
            ticketSelectionAdapter.BackToTravelDateSelection();
        }

        /// <summary>
        /// Event handler called when next button clicked. The back end will be called to
        /// search for available services for the selected ticket.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void nextButton_Click(object sender, EventArgs e)
        {
            FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;
            pageState.SelectedSingleOrReturnTicketIndex = ticketSelectionControl.SelectedIndex;

            ticketSelectionAdapter.PerformServiceProcessing(this.pageId);

        }

        #endregion Event handlers

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			ExtraEventWireUp();
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Extra event subscription
		/// </summary>
		private void ExtraEventWireUp()
		{
            // CCN 0427 removed the back button from the page and used back button from journeyChangeSearchControl
			this.journeyChangeSearchControl.GenericBackButton.Click += new EventHandler(this.backButton_Click);
			this.nextButton.Click += new EventHandler(this.nextButton_Click);
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
