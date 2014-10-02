// *********************************************** 
// NAME                 : FindFareTicketSelectionSingles.aspx.cs 
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 07/02/2005
// DESCRIPTION			: 
// This page is responsible for displaying outward and inward single tickets for 
// a given mode and travel date selected on the FindFareDateSelection page. 
// The user is able to change the travel dates within the range of the original search. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindFareTicketSelectionSingles.aspx.cs-arc  $
//
//   Rev 1.4   Oct 28 2010 16:51:34   RBroddle
//Removed explicit wire up to Page_PreRender & Page_Init as AutoEventWireUp=true for this page so events were firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.3   Jan 26 2009 12:55:38   apatel
//No change.
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:24:32   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Feb 29 2008 apatel
// Changes made to show generic back button from JourneyChangeSelectionControl and removed the back button which was on page.
//
//   Rev 1.0   Nov 08 2007 13:29:34   mturner
//Initial revision.
//
//   Rev 1.16   Feb 23 2006 18:47:48   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.15   Feb 10 2006 10:59:50   aviitanen
//Manual merge for Homepage phase 2 (stream3180)
//
//   Rev 1.14   Dec 01 2005 15:17:20   rhopkins
//Corrections to the button alignments
//Resolution for 3216: UEE: Javascript disabled - Printer friendly button uses flat style
//Resolution for 3242: UEE: Netscape - buttons overlap with journey summary on results page
//
//   Rev 1.13.1.0   Nov 30 2005 17:57:18   rhopkins
//Corrections to the button alignments
//Resolution for 3216: UEE: Javascript disabled - Printer friendly button uses flat style
//Resolution for 3242: UEE: Netscape - buttons overlap with journey summary on results page
//
//   Rev 1.13   Nov 03 2005 16:00:58   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.12.1.1   Oct 24 2005 14:02:06   RGriffith
//Changes to accomodate AmendViewControl changes
//
//   Rev 1.12.1.0   Oct 17 2005 11:47:10   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.12   May 10 2005 17:41:48   rhopkins
//FxCop corrections
//
//   Rev 1.11   Apr 27 2005 16:59:38   rscott
//IR2331
//
//   Rev 1.10   Apr 26 2005 11:04:26   COwczarek
//Add new hyperlink to singles ticket selection page that allows
//user to switch to return ticket selection page.
//Resolution for 2099: PT: Find A Fare singles ticket selection page needs a link to return ticket selection page
//
//   Rev 1.9   Apr 22 2005 14:47:48   COwczarek
//Set PartialResults property of ticket selection controls so that
//appropriate message can be displayed to user.
//Resolution for 2247: PT - Error handling by Retail Business Objects
//
//   Rev 1.8   Apr 22 2005 11:45:32   rgeraghty
//Page_PreRender code updated to update the instructional text when there are no tickets left to select
//Resolution for 2066: PT - Train - Fare Ticket Selection - text needs to change when no tickets available
//
//   Rev 1.7   Apr 21 2005 13:27:46   rhopkins
//Tidied up way that DisplayableTravelDates are created for child/adult.
//Resolution for 2287: FindAFare date selection is not displaying errors correctly
//
//   Rev 1.6   Apr 15 2005 19:53:44   rhopkins
//Handle moving to a "Return" Travel Date when in "Singles" mode
//
//   Rev 1.5   Apr 15 2005 11:55:42   rgeraghty
//Update to error message handling
//Resolution for 2148: PT - Fares Journey Planner tries to sell day returns for journeys seperated by a day
//
//   Rev 1.4   Apr 13 2005 14:45:12   rhopkins
//Don't need to change SelectedTicketType
//
//   Rev 1.3   Apr 12 2005 14:51:40   tmollart
//Added code to PreRender to save selected list item.

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
    /// This page is responsible for displaying outward and inward single tickets for 
    /// a given mode and travel date selected on the FindFareDateSelection page. 
    /// The user is able to change the travel dates within the range of the original search. 
    /// </summary>
    public partial class FindFareTicketSelectionSingles : TDPage, ILanguageHandlerIndependent
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
        /// Control showing table of single outward tickets
        /// </summary>
        protected FindFareTicketSelectionControl outwardTicketSelectionControl;

        /// <summary>
        /// Control showing table of single inward tickets
        /// </summary>
        protected FindFareTicketSelectionControl inwardTicketSelectionControl;

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
        /// Panel containing text/hyperlink for viewing return tickets
        /// </summary>
        protected System.Web.UI.WebControls.Panel viewReturnsPanel;

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

        /// <summary>
        /// First part of text that contains "returns" hyperlink
        /// </summary>
        protected System.Web.UI.WebControls.Label viewReturnsPart1Label;

        /// <summary>
        /// Second part of text that contains "returns" hyperlink
        /// </summary>
        protected System.Web.UI.WebControls.Label viewReturnsPart2Label;

        /// <summary>
        /// Hyperlink enclosed between parts 1 and 2 of "returns" hyperlink text
        /// </summary>
        protected System.Web.UI.WebControls.HyperLink viewReturnsHyperLink;

        #region Constructors

        /// <summary>
        /// Constructor. Sets page id and local resource manager.
        /// </summary>
        protected FindFareTicketSelectionSingles()
        {
            pageId = PageId.FindFareTicketSelectionSingles;
            LocalResourceManager = TDResourceManager.FIND_A_FARE_RM;
        }

        #endregion Constructors

        #region Protected methods
        #endregion Protected methods

        #region Private methods

        /// <summary>
        /// Updates properties on the the ticket selection controls with new values from page state
        /// </summary>
        private void updateTicketSelectionControl() 
        {
            FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;

            outwardTicketSelectionControl.Tickets = 
                pageState.OutwardTicketTable;
            outwardTicketSelectionControl.Single = true;
            outwardTicketSelectionControl.SelectedIndex = pageState.SelectedOutwardTicketIndex;
            outwardTicketSelectionControl.Outward = true;
            outwardTicketSelectionControl.CurrentOutwardDate = pageState.SelectedSinglesOutwardDate;
            outwardTicketSelectionControl.PartialResults = pageState.SelectedTravelDate.PartialResults;

            inwardTicketSelectionControl.Tickets = 
                pageState.InwardTicketTable;
            inwardTicketSelectionControl.Single = true;
            inwardTicketSelectionControl.SelectedIndex = pageState.SelectedInwardTicketIndex;
            inwardTicketSelectionControl.Outward = false;
            inwardTicketSelectionControl.CurrentOutwardDate = pageState.SelectedSinglesInwardDate;
            inwardTicketSelectionControl.PartialResults = pageState.SelectedTravelDate.PartialResults;
        
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

			//backButton.Text = langstrings.GetString("FindFareTicketSelection.BackButton.Text");
			nextButton.Text = langstrings.GetString("FindFareTicketSelection.NextButton.Text");

            helpLabelControl.Text = GetResource("FindFareTicketSelectionHelpLabel");
            helpLabelControl.MoreHelpUrl = GetResource("FindFareTicketSelectionHelpLabel.moreURL");

            // Initialise day selection control within amend tool
            AmendCostSearchDayControl dayControl = amendControl.AmendCostSearchDayControl;
            dayControl.ShowReturnDate = true;
            dayControl.MinOutwardDate = pageState.SearchRequest.SearchOutwardStartDate;
            dayControl.MaxOutwardDate = pageState.SearchRequest.SearchOutwardEndDate;
			dayControl.CurrentOutwardDate = pageState.SelectedSinglesOutwardDate;
            dayControl.CurrentReturnDate = pageState.SelectedSinglesInwardDate;
            dayControl.MinReturnDate = pageState.SearchRequest.SearchReturnStartDate;
            dayControl.MaxReturnDate = pageState.SearchRequest.SearchReturnEndDate;

            // Init control that allows partitions to be switched
            ticketSelectionAdapter.InitAmendViewControl(amendControl.AmendViewControl);

        }

        /// <summary>
        /// Performs initialisation when page is loaded for the first time.
        /// </summary>
        private void initialRequestSetup() 
        {
            ticketSelectionAdapter.CheckForResults();

            // This page has been requested to view singles tickets rather than return tickets
            // so switch the currently selected travel date from a singles travel date to a 
            // returns travel date
            string viewSingles = (string)Request.QueryString["ViewSingles"];
            if (viewSingles != null && viewSingles.Equals("true"))
            {
                switchToSingles();
            }
		}

        /// <summary>
        /// Switches the currently selected travel date from a return to a singles
        /// </summary>
        private void switchToSingles() 
        {
            FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;

			pageState.SelectedSinglesOutwardDate = pageState.SelectedReturnOutwardDate;
            pageState.SelectedSinglesInwardDate = pageState.SelectedReturnInwardDate;

            ticketSelectionAdapter.UpdateTravelDate(TicketType.Singles);
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

        #endregion Private methods

        #region Event handlers

        /// <summary>
        /// Event handler called when user changes the current outward travel date. Ticket data is
        /// retrieved from current search results for the new date and the ticket selection
        /// control is updated with this data.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void outwardDateChanged(object sender, System.EventArgs e) 
        {
            FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;

            // Get the new date
            pageState.SelectedSinglesOutwardDate = amendControl.AmendCostSearchDayControl.CurrentOutwardDate;

            // Get the new TravelDate object for the new date
            ticketSelectionAdapter.UpdateTravelDate(
                pageState.SelectedTravelDate.TravelDate.TicketType);

            updateTicketSelectionControl();
        }

        /// <summary>
        /// Event handler called when user changes the current return travel date. Ticket data is
        /// retrieved from current search results for the new date and the ticket selection
        /// control is updated with this data.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void returnDateChanged(object sender, System.EventArgs e) 
        {
            FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;

            // Get the new date
            pageState.SelectedSinglesInwardDate = amendControl.AmendCostSearchDayControl.CurrentReturnDate;

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
            //CCN 0427 use of the back button from the journeyChangeSearchControl control
            // removed the back button which was on the page
            journeyChangeSearchControl.GenericBackButtonVisible = true;

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
            ConfigureLeftMenu(expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFindFareTicketSelectionSingles);
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
            amendControl.AmendCostSearchDayControl.ReturnDateChanged += new EventHandler(returnDateChanged);
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
			pageState.SelectedOutwardTicketIndex = outwardTicketSelectionControl.SelectedIndex;
			pageState.SelectedInwardTicketIndex = inwardTicketSelectionControl.SelectedIndex;

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

            // Check if return tickets exist for the currently selected travel date
            TravelDate returnsTravelDate = pageState.SearchResult.GetTravelDate(
                pageState.SelectedSinglesOutwardDate, 
                pageState.SelectedSinglesInwardDate, 
                pageState.SelectedTravelDate.TravelDate.TravelMode, 
                TicketType.Return
                );

            // Only display hyperlink if return tickets exist for the currently selected travel date
            if (returnsTravelDate != null) 
            {
                viewReturnsPart1Label.Text = GetResource("FindFareTicketSelectionReturn.viewReturnsPart1Label.Text");
                viewReturnsPart2Label.Text = GetResource("FindFareTicketSelectionReturn.viewReturnsPart2Label.Text");
                viewReturnsHyperLink.Text = GetResource("FindFareTicketSelectionReturn.viewReturnsHyperLink.Text");

                string channelName = getBaseChannelURL(TDPage.SessionChannelName);

                viewReturnsHyperLink.NavigateUrl = channelName + 
                    GetResource("FindFareTicketSelectionReturn.viewReturnsHyperLink.Url");
            }
            viewReturnsPanel.Visible = returnsTravelDate != null;

        }

        /// <summary>
        /// Event handler called when back button clicked.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void backButton_Click(object sender, EventArgs e)
        {
            FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;

            // If user was viewing returns on date selection page and on returns ticket selection page
            // switched to singles using hyperlink, then switch the selected travel date back to returns, 
            // so that next time they click "next" on the date selection page, they see return tickets first.
            // Note if no return tickets exist for the date, then the selected travel date will remain as singles.
            if (pageState.SelectedTicketType == TicketType.Return) 
            {
                TravelDate returnsTravelDate = pageState.SearchResult.GetTravelDate(
                    pageState.SelectedSinglesOutwardDate, 
                    pageState.SelectedSinglesInwardDate, 
                    pageState.SelectedTravelDate.TravelDate.TravelMode, 
                    TicketType.Return
                    );

				if (returnsTravelDate != null) 
				{
					pageState.SelectedTravelDateIndex = 
						pageState.FareDateTable.GetTravelDateIndex(returnsTravelDate, pageState.SearchResult);

					pageState.SelectedTravelDate = new DisplayableTravelDate(returnsTravelDate, pageState.ShowChild, 0);
				}

            }
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
            pageState.SelectedOutwardTicketIndex = outwardTicketSelectionControl.SelectedIndex;
            pageState.SelectedInwardTicketIndex = inwardTicketSelectionControl.SelectedIndex;

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
