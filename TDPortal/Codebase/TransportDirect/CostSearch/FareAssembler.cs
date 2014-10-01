// ************************************************************** 
// NAME			: FareAssembler.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 18/10/2005 
// DESCRIPTION	: Definition of the FareAssembler base class
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/FareAssembler.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:18   mturner
//Initial revision.
//
//   Rev 1.7   May 17 2006 15:01:52   rphilpott
//Add RouteCode to UnavailableProducts table, associated SP's and all classes that use them.
//Resolution for 4084: DD075: Unavailable products - ticket and route codes
//
//   Rev 1.6   May 05 2006 16:16:00   RPhilpott
//Use NLC codes instead of location descriptions for unavailable products.
//Resolution for 4080: DD075: Unavailable fare not changed to Low availability
//
//   Rev 1.5   Dec 22 2005 17:18:18   RWilby
//Updated for code review
//
//   Rev 1.4   Nov 07 2005 19:01:18   RPhilpott
//Initialise TravelDate.TicketType to None.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Nov 07 2005 16:59:24   RWilby
//Added check for null routePriceSuppplier
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Nov 02 2005 09:34:32   RWilby
//Updated class
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 28 2005 16:35:56   RWilby
//Updated class to remove IPriceSupplierFactory refs
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 28 2005 15:31:30   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
using System;
using System.Collections;

using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.PricingRetail.AvailabilityEstimator;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// FareAssembler base class
	/// </summary>
	public abstract class FareAssembler
	{
		#region protected members

		//Results
		protected ArrayList arrayListResults;
		protected CostSearchResult faresResult;	

		//local copy of the CostSearchRequest
		protected ICostSearchRequest theRequest;

		//errors
		protected const string FARES_INTERNAL_ERROR	= "CostSearchError.FaresInternalError";
		protected string [] priceRouteErrors;
		protected ArrayList faresErrors;	

		//discounts
		protected Discounts discounts;

		#endregion

		#region protected methods
		/// <summary>
		/// Takes an array list of travel dates and the TravelMode, and updates them with availability info
		/// </summary>
		/// <param name="travelDates">ArrayList</param>
		/// <param name="mode">TicketTravelMode</param>
		protected void UpdateTicketAvailability(ArrayList travelDates, TicketTravelMode mode)
		{
			//get instance of availability estimator according to travel mode				
			AvailabilityEstimatorFactory availabilityFactory = new AvailabilityEstimatorFactory();
			IAvailabilityEstimator availabilityEstimator =  availabilityFactory.GetAvailabilityEstimator(mode);	
			AvailabilityRequest availabilityRequest;
			ArrayList tickets;										 
			
			//check if each TravelDate in the array list has any tickets
			//need to work backwards because we may be removing travel dates
			int startPosition = (travelDates.Count - 1);			
			for (int i = startPosition; i >= 0; i--)
			{				
				TravelDate thisDate = (TravelDate)travelDates[i];
 
				//if no tickets for a travel date then remove it from the array list				
				if (!thisDate.HasTickets)
				{
					travelDates.Remove(thisDate);						
				}
				else
				{
					//update outward tickets
					if (thisDate.HasOutwardTickets)
					{
						//temp array list that will hold tickets that have a probability greater than none
						tickets = new ArrayList();	                  

						for (int j = 0; j < thisDate.OutwardTickets.Length; j++)
						{
							if	(mode == TicketTravelMode.Rail)
							{
								availabilityRequest = new AvailabilityRequest(mode, theRequest.OriginLocation.Description,
									theRequest.DestinationLocation.Description, 
									thisDate.OutwardTickets[j].TicketRailFareData.OriginNlc,
									thisDate.OutwardTickets[j].TicketRailFareData.DestinationNlc,
									thisDate.OutwardTickets[j].ShortCode, 
									thisDate.OutwardTickets[j].TicketRailFareData.RouteCode, 
									thisDate.OutwardDate);
							}
							else
							{
								availabilityRequest = new AvailabilityRequest(mode,theRequest.OriginLocation.Description,
									theRequest.DestinationLocation.Description, thisDate.OutwardTickets[j].ShortCode, thisDate.OutwardDate);
							}

							//get estimate of the availability of each ticket
							thisDate.OutwardTickets[j].Probability = availabilityEstimator.GetAvailabilityEstimate(availabilityRequest);

							//only add the ticket to the temp array list if its probability is not none
							if (thisDate.OutwardTickets[j].Probability != Probability.None)
							{
								tickets.Add(thisDate.OutwardTickets[j]);
							}
						}

						//update the TravelDate's outward tickets collection
						thisDate.OutwardTickets = (CostSearchTicket[])tickets.ToArray(typeof(CostSearchTicket));				
					}

					//update inward tickets
					if (thisDate.HasInwardTickets)
					{
						//temp array list that will hold tickets that have a probability greater than none
						tickets = new ArrayList();	  

						for (int j = 0; j < thisDate.InwardTickets.Length; j++)
						{
							if	(mode == TicketTravelMode.Rail)
							{
								availabilityRequest = new AvailabilityRequest(mode, theRequest.OriginLocation.Description,
									theRequest.DestinationLocation.Description, 
									thisDate.InwardTickets[j].TicketRailFareData.OriginNlc,
									thisDate.InwardTickets[j].TicketRailFareData.DestinationNlc,
									thisDate.InwardTickets[j].ShortCode, 
									thisDate.InwardTickets[j].TicketRailFareData.RouteCode, 
									thisDate.ReturnDate);
							}
							else
							{
								availabilityRequest = new AvailabilityRequest(mode, theRequest.OriginLocation.Description,
									theRequest.DestinationLocation.Description, thisDate.InwardTickets[j].ShortCode, thisDate.ReturnDate);
							}

							//get estimate of the availability of each ticket
							thisDate.InwardTickets[j].Probability = availabilityEstimator.GetAvailabilityEstimate(availabilityRequest);

							//only add the ticket to the temp array list if its probability is not none
							if (thisDate.InwardTickets[j].Probability != Probability.None)
							{
								tickets.Add(thisDate.InwardTickets[j]);
							}
						}

						//update the TravelDate's inward tickets collection
						thisDate.InwardTickets = (CostSearchTicket[])tickets.ToArray(typeof(CostSearchTicket));	
					}

					//update return tickets
					if (thisDate.HasReturnTickets)
					{
						//temp array list that will hold tickets that have a probability greater than none
						tickets = new ArrayList();	  

						for (int j = 0; j < thisDate.ReturnTickets.Length; j++)
						{						
							if	(mode == TicketTravelMode.Rail)
							{
								availabilityRequest = new AvailabilityRequest(mode, 
									theRequest.OriginLocation.Description,
									theRequest.DestinationLocation.Description, 
									thisDate.ReturnTickets[j].TicketRailFareData.OriginNlc,
									thisDate.ReturnTickets[j].TicketRailFareData.DestinationNlc,
									thisDate.ReturnTickets[j].ShortCode, 
									thisDate.ReturnTickets[j].TicketRailFareData.RouteCode, 
									thisDate.OutwardDate, 
									thisDate.ReturnDate);
							}
							else
							{
								availabilityRequest = new AvailabilityRequest(mode, 
									theRequest.OriginLocation.Description,
									theRequest.DestinationLocation.Description, 
									thisDate.ReturnTickets[j].ShortCode, 
									thisDate.OutwardDate, 
									thisDate.ReturnDate);
							}

							//get estimate of the availability of each ticket
							thisDate.ReturnTickets[j].Probability = availabilityEstimator.GetAvailabilityEstimate(availabilityRequest);
					
							//only add the ticket to the temp array list if its probability is none
							if (thisDate.ReturnTickets[j].Probability != Probability.None)
							{
								tickets.Add(thisDate.ReturnTickets[j]);
							}
						}

						//update the TravelDate's return tickets collection
						thisDate.ReturnTickets = (CostSearchTicket[])tickets.ToArray(typeof(CostSearchTicket));	
					}	
				}
			}
		}						

		/// <summary>
		/// Puts together travel dates, one for each possible permutation of outward date, return date and mode
		/// </summary>
		/// <param name="isReturn">bool</param>
		/// <returns>ArrayList of TravelDates</returns>
		protected ArrayList CreateTravelDatePermutations( bool isReturn)
		{
			//return array list
			ArrayList travelDatesArrayList = new ArrayList();

			//local variables
			DateTime todayDate = DateTime.Today;
			int daysDiff = 0, outwardDateRange = 0, inwardDateRange = 0;				
			TravelDate travelDate,travelDateDifferentReturn;
			
			//Get the range of days that we need to create permutations of TravelDates for 
			//Note that the SearchOutwardStartDate, SearchOutwardEndDate, SearchOutwardStartDate and SearchOutwardStartDate
			//will already have been worked out by the CostSearchRunner	
			outwardDateRange = theRequest.SearchOutwardStartDate.GetDifferenceDates(theRequest.SearchOutwardEndDate);
			if (isReturn)
			{
				inwardDateRange = theRequest.SearchReturnStartDate.GetDifferenceDates(theRequest.SearchReturnEndDate);
			}
		
			//step through each possible outward date 				
			for (int i = 0; i <= outwardDateRange; i++)
			{
				travelDate = new TravelDate();

				//add outward date
				travelDate.OutwardDate = theRequest.SearchOutwardStartDate.AddDays(i);

				//if the outward date is not today's date, then remove the time part of it i.e. time set to 00:00:00
				daysDiff = DateTime.Compare(travelDate.OutwardDate.GetDateTime().Date, todayDate);
				if (daysDiff != 0)
				{																			
					travelDate.OutwardDate = theRequest.SearchOutwardStartDate.GetDateTime().AddDays(i);
				}

				//add return date if necessary
				if (isReturn)
				{
					//step through each possible return date
					for (int j = 0; j <= inwardDateRange; j++)
					{
						//new travel date
						travelDateDifferentReturn = new TravelDate();
						//get the outward date from the travelDate
						travelDateDifferentReturn.OutwardDate = travelDate.OutwardDate;

						//add return date
						travelDateDifferentReturn.ReturnDate = theRequest.SearchReturnStartDate.AddDays(j);
						
						//if the return date is not today's date, then remove the time part of the date i.e. time set to 00:00:00
						daysDiff = DateTime.Compare(travelDateDifferentReturn.ReturnDate.GetDateTime().Date, todayDate);
						if (daysDiff != 0)						
						{
							travelDateDifferentReturn.ReturnDate = theRequest.SearchReturnStartDate.GetDateTime().AddDays(j);
						}						

						travelDateDifferentReturn.TicketType = TicketType.None;

						//only add if return date is later than the outward date
						if (travelDateDifferentReturn.ReturnDate >= travelDateDifferentReturn.OutwardDate)
						{
							travelDatesArrayList.Add(travelDateDifferentReturn);
						}
					}
				}
					//no return date 
				else
				{
					//add to collection
					travelDate.TicketType = TicketType.None;
					travelDatesArrayList.Add(travelDate);
				}
			}			

			//return ArrayList of TravelDates			
			return travelDatesArrayList;				
		}


		/// <summary>
		/// Adds the list of TravelDates for a particular mode to the overall list of TravelDates
		/// </summary>
		/// <param name="travelDates"></param>
		protected void AddTravelDatesToFaresResult(ArrayList travelDates)
		{
			//add each travel date to the overall array list of travel dates
			for (int i = 0; i < travelDates.Count; i++)
			{
				//only add TravelDate if it has any tickets
				TravelDate date = (TravelDate)travelDates[i];
				if (date.HasTickets)
				{
					arrayListResults.Add(travelDates[i]);
				}
			}
		}

		/// <summary>
		/// Compares the fares information for a particular ticket with the current lowest probable fares
		/// for a travel date, and updates the current values if necessary
		/// </summary>
		/// <param name="travelDates"></param>
		protected void UpdateTravelDateFares(ArrayList travelDates)
		{		
			//check if each TravelDate in the array list has any tickest
			for (int i = 0; i < travelDates.Count; i++)
			{					
				//local variable to make life easier...
				TravelDate thisDate = (TravelDate)travelDates[i];				

				//set the min max fares and lowest probable fares for each travel date
				thisDate.UpdateMaxMinFares();		
			}
		}	
		#endregion

		/// <summary>
		/// FareAssembler static method. Returns appropriate concrete class based on mode.
		/// </summary>
		/// <param name="?">TicketTravelMode</param>
		/// <returns>FareAssembler for TravelMode</returns>
		public static FareAssembler GetAssembler (TicketTravelMode mode)
		{
			switch (mode) 
			{
				case TicketTravelMode.Rail :
					return new RailFareAssembler();
				case TicketTravelMode.Coach :
					return new CoachFareAssembler();
				default:
					return null;
			}
		}

		/// <summary>
		/// Override this method to return a CostSearchResult containing travel dates with fares information,
		/// based on the CostSearchRequest 
		/// </summary>
		public abstract ICostSearchResult AssembleFares(ICostSearchRequest request);
	
	}
}
