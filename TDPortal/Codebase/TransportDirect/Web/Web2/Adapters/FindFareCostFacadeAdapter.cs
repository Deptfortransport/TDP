// *********************************************** 
// NAME			: FindFareCostFacadeAdapter.cs
// AUTHOR		: Richard Hopkins
// DATE CREATED	: 10/01/05
// DESCRIPTION	: Responsible for processing the Cost Search results into formats required by Find A Fare pages.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/FindFareCostFacadeAdapter.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 12:59:00   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:18   mturner
//Initial revision.
//
//   Rev 1.13   Feb 23 2006 19:16:08   build
//Automatically merged from branch for stream3129
//
//   Rev 1.12.1.1   Jan 30 2006 12:15:22   mdambrine
//add TDCultureInfo is now in the common namespace
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.12.1.0   Jan 10 2006 15:17:34   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.12   Nov 09 2005 12:31:54   build
//Automatically merged from branch for stream2818
//
//   Rev 1.11.1.0   Nov 08 2005 20:17:04   RPhilpott
//Support for ReturnsOnly dropdown type in AmendFareControl.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.11   Apr 27 2005 16:41:44   jmorrissey
//Added GetSinglesTicketDetails method
//Resolution for 2323: PT: Singles tickets being displayed more than once if flexibility selected
//
//   Rev 1.10   Apr 21 2005 15:15:44   rhopkins
//When creating a new FareDateTable set the SelectedTravelDate to null, so that it does not get carried over from the previous query.
//Resolution for 2288: FindFare if "Amend journey" is used after showing Returns as Singles then Date Selection fails
//
//   Rev 1.9   Apr 20 2005 12:26:54   rhopkins
//Handle possibility that query may return no TravelDates
//Resolution for 2100: PT - Coach - Find a Fare not correctly handling missing fare information
//
//   Rev 1.8   Apr 14 2005 16:08:02   rhopkins
//Changed GetTicketSummary() so that it calls the correct version of SearchResult.GetTravelDate(), depending on whether a ReturnDate exists.
//Resolution for 2183: FindFare - cannot switch to Open Return tickets on Date Selection page
//
//   Rev 1.7   Apr 13 2005 13:43:40   rhopkins
//Modify GetTicketSummary so that it maintains the currently selected date(s) and sort options
//Resolution for 2054: PT: Selected row number doesn't change on Find Fare Date Selection
//Resolution for 2055: PT: Actual sort order doesn't match what table is indicating
//
//   Rev 1.6   Apr 10 2005 13:34:06   COwczarek
//No longer pass adult/child flag to DisplayableCostSearchTicketComparer constructor.
//Resolution for 2068: PT sort order not correct in ticket selection table
//
//   Rev 1.5   Mar 30 2005 11:11:18   COwczarek
//Adding sorting to GetSingleTicketDetails  and
//GetReturnTicketDetails methods.
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.4   Mar 24 2005 16:28:08   COwczarek
//Use SelectedTravelDate property rather than index table directly.
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.3   Mar 14 2005 16:25:50   COwczarek
//Work in progress
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.2   Mar 08 2005 14:27:04   rhopkins
//Correct child fare errors
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.1   Feb 25 2005 13:20:26   rhopkins
//Corrections to sorting
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.0   Feb 10 2005 16:30:24   rhopkins
//Initial revision.
//

using System;using TransportDirect.Common.ResourceManager;
using System.Web.UI.WebControls;

using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.CostSearch;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common;


namespace TransportDirect.UserPortal.Web.Adapters
{

    /// <summary>
    /// Responsible for processing the Cost Search results into formats required by Find A Fare pages
    /// </summary>
    public class FindFareCostFacadeAdapter
    {

        /// <summary>
        /// Page state for Find A Fare input pages. Contains current cost search results.
        /// </summary>
		FindCostBasedPageState pageState;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="pageState">Page state for Find A Fare input pages</param>
		public FindFareCostFacadeAdapter(FindCostBasedPageState pageState)
		{
			this.pageState = pageState;
		}

        #region Public Methods

        /// <summary>
        /// This method returns a sorted table of travel dates for the current search resutls. The currently selected
        /// ticket type is used to determine the travel dates returned and the ticket types that constitue those
        /// travel dates as follows:
        /// <ul>
        /// <li>Single : Returns a table of travel dates with single tickets only</li>
        /// <li>Open return : Returns a table of travel dates with open return tickets only</li>
        /// <li>Return : Returns a table of travel dates with return tickets or open return tickets. If neither exist,
        /// a travel date with singles tickets will be returned for the same date, if available.</li>
        /// <li>Singles : Returns a table of travel dates with singles tickets only.</li>
        /// </ul>
        /// </summary>
        /// <returns>a table of travel dates for the current search resutls</returns>
		public DisplayableTravelDates GetTicketSummary()
		{
			if ( (pageState.FareDateTable == null) || (pageState.FareDateTable.Length == 0)
				|| (pageState.FareDateTable.SelectedTicketType != pageState.SelectedTicketType) )
			{
				TravelDate currentTravelDate = null;
				TravelDate newTravelDate = null;
				DisplayableTravelDateColumn sortColumn;
				bool sortAscending;

				if ( (pageState.FareDateTable != null) && (pageState.FareDateTable.Length > 0) )
				{
					// Retain the currently selected date and sort options
					currentTravelDate = pageState.SelectedTravelDate.TravelDate;

					// Obtain TravelDate object for the new date using the mode from
					// the TravelDate object obtained for the current date but for
					// the new ticket type
					if (currentTravelDate.ReturnDate == null)
					{
						newTravelDate = pageState.SearchResult.GetTravelDate(
							currentTravelDate.OutwardDate,
							currentTravelDate.TravelMode, pageState.SelectedTicketType);
					}
					else
					{
						newTravelDate = pageState.SearchResult.GetTravelDate(
							currentTravelDate.OutwardDate,
							currentTravelDate.ReturnDate,
							currentTravelDate.TravelMode, pageState.SelectedTicketType);
					}

					sortColumn = pageState.FareDateTable.SortColumn;
					sortAscending = pageState.FareDateTable.SortOrderAscending;
				}
				else
				{
					// Sort the table into its default order
					sortColumn = DisplayableTravelDateColumn.LowestFare;
					sortAscending = true;
				}

				// Ensure that there is no overriden SelectedTravelDate from previous requests
				pageState.SelectedTravelDate = null;

				TravelDatesResultSet travelDatesResults = pageState.SearchResult.GetTravelDates(pageState.SelectedTicketType);
				if ( (travelDatesResults != null) && (travelDatesResults.TravelDates != null) && (travelDatesResults.TravelDates.Length > 0) )
				{
					DisplayableTravelDates displayableTravelDates = new DisplayableTravelDates(travelDatesResults.TravelDates, pageState.ShowChild);
					displayableTravelDates.SelectedTicketType = pageState.SelectedTicketType;

					displayableTravelDates.SortColumn = sortColumn;
					displayableTravelDates.SortOrderAscending = sortAscending;
					DisplayableTravelDateSortOption sortOption = new DisplayableTravelDateSortOption(DisplayableTravelDateColumn.LowestFare, true);
					DisplayableTravelDateComparer dateComparer = new DisplayableTravelDateComparer(sortOption);
					displayableTravelDates.Sort(dateComparer);

					pageState.FareDateTable = displayableTravelDates;

					if (newTravelDate != null)
					{
						pageState.SelectedTravelDateIndex = displayableTravelDates.GetTravelDateIndex(newTravelDate, pageState.SearchResult);
					}
					else
					{
						pageState.SelectedTravelDateIndex = 0;
						newTravelDate = pageState.SelectedTravelDate.TravelDate;
					}

					switch (newTravelDate.TicketType)
					{
						case TicketType.Single:
						case TicketType.OpenReturn:
							pageState.SelectedSingleDate = newTravelDate.OutwardDate;
							break;

						case TicketType.Return:
							pageState.SelectedReturnOutwardDate = newTravelDate.OutwardDate;
							pageState.SelectedReturnInwardDate = newTravelDate.ReturnDate;
							break;

						case TicketType.Singles:
							pageState.SelectedSinglesOutwardDate = newTravelDate.OutwardDate;
							pageState.SelectedSinglesInwardDate = newTravelDate.ReturnDate;
							break;
					}
				}
				else
				{
					pageState.FareDateTable = new DisplayableTravelDates();
				}
			}

			// Ensure that data is correctly formatted for the current ShowChild selection.
			pageState.FareDateTable.ShowChild(pageState.ShowChild);

			return pageState.FareDateTable;
		}

        /// <summary>
        /// This method returns a sorted table of single, open return or singles (either outward or inward)
        /// tickets from the current search results for the supplied date. 
        /// The currently selected travel date object specifies the ticket type and travel mode for the search.
        /// If there are no tickets for the ticket type, travel mode and date combination, an empty table is
        /// returned.
        /// </summary>
        /// <param name="outward">If selected travel date object has a ticket type of singles, this specifies
        /// whether outward or inward tickets are required. True if outward, false otherwise.</param>
        /// <param name="date">Date for which to return tickets</param>
        /// <returns>Sorted table of tickets</returns>
        public DisplayableCostSearchTickets GetSingleTicketDetails(bool outward, TDDateTime date) 
        {
            ICostSearchResult result = pageState.SearchResult;

            TravelDate travelDate = pageState.SelectedTravelDate.TravelDate;			

            CostSearchTicket[] tickets = null;

            switch (travelDate.TicketType) 
            {
                case TicketType.Single:
                    tickets = result.GetSingleTickets(date,travelDate.TravelMode);
                    break;
                case TicketType.OpenReturn:
                    tickets = result.GetOpenReturnTickets(date,travelDate.TravelMode);
                    break;

            }

            DisplayableCostSearchTickets displayableCostSearchTickets =
                new DisplayableCostSearchTickets(!pageState.ShowChild, travelDate.TravelMode, tickets);

            displayableCostSearchTickets.Sort(new DisplayableCostSearchTicketComparer());

            return displayableCostSearchTickets;
        }

		/// <summary>
		/// This method returns a sorted table of singles (either outward or inward)
		/// tickets from the current search results for the supplied date. 
		/// The currently selected travel date object specifies the travel mode for the search.
		/// If there are no tickets for the ticket type, travel mode and date combination, an empty table is
		/// returned.
		/// </summary>
		/// <param name="outward">If selected travel date object has a ticket type of singles, this specifies
		/// whether outward or inward tickets are required. True if outward, false otherwise.</param>
		/// <param name="outDate">Date for which to outward tickets</param>
		/// <param name="retDate">Date for which to return tickets</param>
		/// <returns>Sorted table of tickets</returns>
		public DisplayableCostSearchTickets GetSinglesTicketDetails(bool outward, TDDateTime outDate, TDDateTime retDate) 
		{
			ICostSearchResult result = pageState.SearchResult;

			TravelDate travelDate = pageState.SelectedTravelDate.TravelDate;			

			CostSearchTicket[] tickets = null;			
			
			//look up either outward or inward singles
			if (outward) 
			{
				tickets = result.GetOutwardTickets(outDate, retDate, travelDate.TravelMode);
			} 
			else 
			{
				tickets = result.GetInwardTickets(outDate, retDate, travelDate.TravelMode);
			}		

			DisplayableCostSearchTickets displayableCostSearchTickets =
				new DisplayableCostSearchTickets(!pageState.ShowChild, travelDate.TravelMode, tickets);

			displayableCostSearchTickets.Sort(new DisplayableCostSearchTicketComparer());

			return displayableCostSearchTickets;
		}

        /// <summary>
        /// This method returns a sorted table of return tickets from the current search results for the
        /// supplied date. 
        /// The currently selected travel date object specifies the ticket type and travel mode for the search.
        /// If there are no tickets for the ticket type, travel mode and date combination, an empty table is
        /// returned.
        /// </summary>
        /// <param name="outwardDate">Outward date for which to return tickets</param>
        /// <param name="returnDate">Inward date for which to return tickets</param>
        /// <returns>Sorted table of tickets</returns>
        public DisplayableCostSearchTickets GetReturnTicketDetails(TDDateTime outwardDate, TDDateTime returnDate) 
        {
            ICostSearchResult result = pageState.SearchResult;

            TravelDate travelDate = pageState.SelectedTravelDate.TravelDate;

            DisplayableCostSearchTickets displayableCostSearchTickets = new DisplayableCostSearchTickets(
                !pageState.ShowChild,
                travelDate.TravelMode,
                result.GetReturnTickets(outwardDate, returnDate, travelDate.TravelMode)
                );

            displayableCostSearchTickets.Sort(new DisplayableCostSearchTicketComparer());

            return displayableCostSearchTickets;
        }


		/// <summary>
		///	Read-only property to return the appropriate drop-down list 
		///	of ticket types to be displayed in the AmendFaresControl.  
		/// </summary>
		public DataServiceType CurrentTicketTypeDropDownList
		{
			get
			{
				DataServiceType type;
					
				if	((pageState.SelectedTicketType == TicketType.Return) || (pageState.SelectedTicketType == TicketType.Singles))
				{
					if	(SinglesPresentInResults())
					{
						type = DataServiceType.ReturnTicketTypeDrop;
					}
					else
					{
						type = DataServiceType.ReturnsOnly;
					}
				}
				else
				{
					type = DataServiceType.SingleTicketTypeDrop;
				}

				return type;
			}
		}


		#endregion Public Methods

		#region Private Methods

		/// <summary>
		/// Determines if there are any "Singles" present in the current result.
		/// This will normally be the case if we have rail fares, but not if there
		/// are only coach fares in the results.
		/// </summary>
		/// <returns>True if "Singles" present.</returns>
		private bool SinglesPresentInResults()
		{
			ICostSearchResult result = pageState.SearchResult;

			foreach (TravelDate td in result.GetAllTravelDates())
			{
				if	(td.TicketType == TicketType.Singles && td.HasTickets)
				{
					return true;
				}
			}
			return false;
		}

		#endregion Private Methods
	
	}

}
