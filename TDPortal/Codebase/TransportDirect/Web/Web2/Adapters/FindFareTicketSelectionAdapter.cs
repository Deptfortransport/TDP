// *********************************************** 
// NAME			: FindFareTicketSelectionAdapter.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 21/03/05
// DESCRIPTION	: Responsible for common functionality
// required by Find Fare Ticket selection pages.
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/FindFareTicketSelectionAdapter.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 12:59:02   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:20   mturner
//Initial revision.
//
//   Rev 1.9   Feb 23 2006 19:16:08   build
//Automatically merged from branch for stream3129
//
//   Rev 1.8.1.1   Jan 30 2006 12:18:52   mdambrine
//add TDCultureInfo is now in the common namespace
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.8.1.0   Jan 10 2006 15:17:36   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.8   Nov 09 2005 16:59:24   jgeorge
//Manual merge for stream2818 (Search by Price)
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.7   Nov 03 2005 17:04:28   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.6.1.0   Oct 14 2005 15:28:14   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.6   Apr 27 2005 16:43:16   jmorrissey
//Update to CreateDisplayableCostSearchTickets method
//Resolution for 2323: PT: Singles tickets being displayed more than once if flexibility selected
//
//   Rev 1.5   Apr 27 2005 09:51:26   COwczarek
//Pass correct session variable to GetSingleTicketDetails for single/open return ticket type.
//Resolution for 2306: PT - Find a Fare - No date displayed on the journey summary page
//
//   Rev 1.4   Apr 26 2005 11:04:26   COwczarek
//Add new hyperlink to singles ticket selection page that allows
//user to switch to return ticket selection page.
//Resolution for 2099: PT: Find A Fare singles ticket selection page needs a link to return ticket selection page
//
//   Rev 1.3   Apr 22 2005 12:28:26   COwczarek
//Add comments
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview

using System;using TransportDirect.Common.ResourceManager;
using System.Collections;
using TransportDirect.Common;
using System.Web.UI.WebControls;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.CostSearch;

namespace TransportDirect.UserPortal.Web.Adapters
{

    /// <summary>
    /// Responsible for common functionality required by Find Fare Ticket selection pages.
    /// </summary>
    public class FindFareTicketSelectionAdapter
    {

        /// <summary>
        /// Session context
        /// </summary>
        ITDSessionManager sessionManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sessionManager">Session context</param>
        public FindFareTicketSelectionAdapter(ITDSessionManager sessionManager)
        {
            this.sessionManager = sessionManager;
        }

        /// <summary>
        /// Redirects user to input page if no cost search results exist
        /// </summary>
        public void CheckForResults() 
        {
            if (!FindInputAdapter.IsCostBasedSearchMode(sessionManager.FindAMode) || 
                sessionManager.AsyncCallState.Status != AsyncCallStatus.CompletedOK)
            {
                sessionManager.Session[SessionKey.Transferred] = false;
                sessionManager.FormShift[SessionKey.TransitionEvent] =
                    TransitionEvent.FindFareInputDefault;
            }
        }

        /// <summary>
        /// Redirects user to travel date selection page
        /// </summary>
        public void BackToTravelDateSelection() 
        {
            sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindFareTicketSelectionBack;        
        }

        /// <summary>
        /// Switches user to time based results if user selects time based result option
        /// in amend view drop list. No action is taken if cost based option is selected.
        /// </summary>
        /// <param name="amendViewControl">Control providing partition selection</param>
        public void SwitchPartition(AmendViewControl amendViewControl) 
        {
            if (amendViewControl.PartitionSelected != PartitionType.FindAFare) 
            {
                sessionManager.Partition = TDSessionPartition.TimeBased;
                //Transfer the user to Time Based results
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindFareSwitchPartition;
            }
        }

        /// <summary>
        /// Invokes back end to search for available services and sets up session data to
        /// redirect user to results page.
        /// </summary>
        /// <param name="currentPageId">Invoking page id</param>
        public void PerformServiceProcessing(PageId currentPageId) 
        {
            FindFareInputAdapter findFareInputAdapter = new FindFareInputAdapter(
                (FindCostBasedPageState)sessionManager.FindPageState, sessionManager);

			findFareInputAdapter.InitialiseAsyncCallStateForServicesSearch(currentPageId);

            // Invoke back end to search for available services
            findFareInputAdapter.InvokeValidateAndRunServices();

            sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindFareServiceResults;
        }

        /// <summary>
        /// Sets visibility of "Next" button depending on whether a user has selected a ticket.
        /// The supplied button will be set to visible if a ticket (or tickets for singles) has
        /// been selected, invisible otherwise.
        /// </summary>
        /// <param name="nextButton">Button for which to set visibility</param>
        public void EnableNextButton(TDButton nextButton) 
        {
            FindCostBasedPageState pageState = (FindCostBasedPageState)sessionManager.FindPageState;

            switch (pageState.SelectedTravelDate.TravelDate.TicketType) 
            {
                case TicketType.Single:
                case TicketType.OpenReturn:
                case TicketType.Return:
                    nextButton.Visible = pageState.SelectedSingleOrReturnTicketIndex >= 0;
                    break;
                case TicketType.Singles:
                    nextButton.Visible = (pageState.SelectedOutwardTicketIndex >= 0 && 
                        pageState.SelectedInwardTicketIndex >= 0);
                    break;
            }
        }

        /// <summary>
        /// Initialises the control responsible for partition selection
        /// </summary>
        /// <param name="amendViewControl">Control providing partition selection</param>
        public void InitAmendViewControl(AmendViewControl amendViewControl) 
        {
            amendViewControl.PartitionSelectionAvailable = 
                sessionManager.HasTimeBasedJourneyResults;
            amendViewControl.TimeBasedFindAMode =
                sessionManager.TimeBasedFindAMode;
            amendViewControl.CurrentPartition = TDSessionPartition.CostBased;
        }

        /// <summary>
        /// Replaces the currently selected TravelDate object in page state with a new TravelDate object
        /// for the supplied ticket type using the currently selected outward (and possibly inward) dates. 
        /// The appropriate date properties on page state will need to be set for the new
        /// ticket type before calling this method. A new DisplayableTravelDate object is created to wrap
        /// the new TravelDate object and this is stored in page state. Tickets are then retrieved for the
        /// new travel date and a DisplayableCostSearchTicket object is created to wrap these tickets. This is 
        /// also stored in session state.
        /// </summary>
        /// <param name="ticketType">The ticket type to obtain TravelDate object for</param>
        public void UpdateTravelDate(TicketType ticketType) 
        {
            FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;

            ICostSearchResult result = pageState.SearchResult;

            // Obtain TravelDate object for the current date
            DisplayableTravelDate displayableTravelDate = pageState.SelectedTravelDate;
            TravelDate currentTravelDate = displayableTravelDate.TravelDate;

            TravelDate newTravelDate = null;

            // Obtain TravelDate object for the new date using the mode and ticket type from
            // the TravelDate object obtained for the current date
            switch (ticketType) 
            {
                case TicketType.Single:
                case TicketType.OpenReturn:
                    newTravelDate = result.GetTravelDate(
                        pageState.SelectedSingleDate,
                        currentTravelDate.TravelMode, ticketType);
                    break;
                case TicketType.Singles:
                    newTravelDate = result.GetTravelDate(
                        pageState.SelectedSinglesOutwardDate,
                        pageState.SelectedSinglesInwardDate,
                        currentTravelDate.TravelMode, ticketType);
                    break;
                case TicketType.Return:
                    newTravelDate = result.GetTravelDate(
                        pageState.SelectedReturnOutwardDate,
                        pageState.SelectedReturnInwardDate,
                        currentTravelDate.TravelMode, ticketType);
                    break;
            }

            if (newTravelDate != null) 
            {
                // Update the page state so that the TravelDate object for the new date
                // can be determined.
                pageState.SelectedTravelDateIndex = 
                    pageState.FareDateTable.GetTravelDateIndex(newTravelDate, pageState.SearchResult);

                pageState.SelectedTravelDate = new DisplayableTravelDate(newTravelDate, pageState.ShowChild, 0);

                // Update ticket table with new ticket data
                CreateDisplayableCostSearchTickets(ticketType);
            } 
            else 
            {
                // No TravelDate object could be obtained for the new date. Creating an
                // empty DisplayableCostSearchTickets objects will result in an empty table
                // being displayed and a message indicating no tickets available for date.
                CreateEmptyDisplayableCostSearchTickets(ticketType);
            }
        }

        /// <summary>
        /// Creates a DisplayableCostSearchTicket object with no tickets and assigns it to session state.
        /// Sets the selected ticket for this new table of tickets to "unselected".
        /// </summary>
        /// <param name="ticketType">The ticket type for which to create an empty table of tickets</param>
        public void CreateEmptyDisplayableCostSearchTickets(TicketType ticketType) 
        {

            FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;
            FindFareCostFacadeAdapter adapter = new FindFareCostFacadeAdapter(pageState);

            switch (ticketType)
            {
                case TicketType.Single:
                case TicketType.OpenReturn:
                case TicketType.Return:
                    pageState.SingleOrReturnTicketTable = new DisplayableCostSearchTickets();
                    pageState.SelectedSingleOrReturnTicketIndex = -1;
                    break;
                case TicketType.Singles:
                    pageState.OutwardTicketTable = new DisplayableCostSearchTickets();
                    pageState.SelectedOutwardTicketIndex = -1;
                    pageState.InwardTicketTable = new DisplayableCostSearchTickets();
                    pageState.SelectedInwardTicketIndex = -1;
                    break;
            }
        }

        /// <summary>
        /// Obtains tickets for the supplied ticket type from the current cost search results and creates
        /// a DisplayableCostSearchTicket object to wrap them. This object is then stored in page state.
        /// The default selected ticket for this new table of tickets is also set.
        /// </summary>
        /// <param name="ticketType">Ticket type to get tickets for</param>
        public void CreateDisplayableCostSearchTickets(TicketType ticketType) 
        {

            FindCostBasedPageState pageState = (FindCostBasedPageState)TDSessionManager.Current.FindPageState;
            FindFareCostFacadeAdapter adapter = new FindFareCostFacadeAdapter(pageState);

            switch (ticketType)
            {
                case TicketType.Single:
                case TicketType.OpenReturn:
                    pageState.SingleOrReturnTicketTable = 
                        adapter.GetSingleTicketDetails(true,pageState.SelectedSingleDate);
                    pageState.SelectedSingleOrReturnTicketIndex = pageState.SingleOrReturnTicketTable.DefaultSelectedIndex;
                    break;
                case TicketType.Singles:
                    pageState.OutwardTicketTable = 
                        adapter.GetSinglesTicketDetails(true, pageState.SelectedSinglesOutwardDate, pageState.SelectedSinglesInwardDate);
                    pageState.SelectedOutwardTicketIndex = pageState.OutwardTicketTable.DefaultSelectedIndex;
                    pageState.InwardTicketTable = 
                        adapter.GetSinglesTicketDetails(false, pageState.SelectedSinglesOutwardDate, pageState.SelectedSinglesInwardDate);
                    pageState.SelectedInwardTicketIndex = pageState.InwardTicketTable.DefaultSelectedIndex;
                    break;
                case TicketType.Return:
                    pageState.SingleOrReturnTicketTable = 
                        adapter.GetReturnTicketDetails(
                            pageState.SelectedReturnOutwardDate, pageState.SelectedReturnInwardDate);
                    pageState.SelectedSingleOrReturnTicketIndex = pageState.SingleOrReturnTicketTable.DefaultSelectedIndex;
                    break;
            }
        }

    }

}
