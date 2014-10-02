//********************************************************************************
//NAME         : NatExFaresSupplier.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 15/10/2003
//DESCRIPTION  : Implementation of NatExFaresSupplier class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/NatExFares/NatExFaresSupplier.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:06   mturner
//Initial revision.
//
//   Rev 1.42   Aug 19 2005 14:05:50   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.41.1.0   Aug 16 2005 11:19:58   RPhilpott
//Get rid of warnings from deprecated methods.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.41   Apr 29 2005 12:01:32   jbroome
//Removed FirstLegIndex and LastLegIndex properties of CoachPricingUnit fare as these are now redundant due to the corresponding properties being removed from the CostSearchTicket class.
//
//   Rev 1.40   Apr 28 2005 18:05:32   RPhilpott
//Split noPlacesAvaialble flag into singles and returns.
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.
//
//   Rev 1.39   Apr 28 2005 16:03:52   RPhilpott
//Add "NoPlacesAvailable" property to PricingResult to indicate that valid fares have been found, but with no seat availability.
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.
//
//   Rev 1.38   Apr 28 2005 13:57:56   jbroome
//Ensured tickets are added in correct order according to pricing unit
//Resolution for 2309: PT - Train - Destination wrong on rail ticket description.
//
//   Rev 1.37   Apr 25 2005 13:30:16   jbroome
//Updated after changes to PublicJourneyStore class
//
//   Rev 1.36   Apr 25 2005 10:03:58   jbroome
//Updated PriceRoute method to return PublicJourneyStore object
//
//   Rev 1.35   Apr 21 2005 13:42:20   jbroome
//Updated resource ids for error messages.
//
//   Rev 1.34   Apr 14 2005 11:58:50   jbroome
//Updated error handling checks for when no fares are returned.
//Resolution for 2100: PT: Find a Fare not correctly handling missing fare information
//
//   Rev 1.33   Apr 13 2005 14:19:22   jbroome
//Fixes for IRs 2062 and 2100
//Resolution for 2062: PT: Find Fare Date Selection 'NaN' displayed instead of fares
//Resolution for 2100: PT: Find a Fare not correctly handling missing fare information
//
//   Rev 1.32   Apr 08 2005 17:24:00   COwczarek
//Make combined ticket index unique across travel dates
//
//   Rev 1.31   Apr 08 2005 16:33:26   jbroome
//Add valid inward journeys to return CostSearchTickets 
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.30   Apr 06 2005 17:59:22   jbroome
//Re-structured UpdateTravelDateFaresAndTickets to handle correct grouping of CostSearchTickets and max and min fare values.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.29   Apr 06 2005 16:30:56   jgeorge
//Request should use the Any Time flags for both outward and return journeys.
//
//   Rev 1.28   Apr 05 2005 14:44:30   jbroome
//Fares are now filtered by Discount Card.
//Re-added setting Max/Min Adult/Child fares on traveldates, with added complexity for TicketType.Singles
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.27   Apr 01 2005 19:58:36   jmorrissey
//Updated PriceRoute method. Fix for calls to ExtractFareInformationFromJourney when PublicJourney date is incorrect
//
//   Rev 1.26   Apr 01 2005 16:51:48   jbroome
//Uses TDDateTime.AreSameDate() helper method
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.25   Apr 01 2005 15:59:58   jbroome
//Removed code to set Max/Min Adult/Child fares as this is now handled in CostSearchFacade.cs in CostSearch project.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.24   Mar 31 2005 18:04:20   jbroome
//Work in progress - bug fixes.
//
//   Rev 1.23   Mar 31 2005 16:50:52   jbroome
//Checks for null Fares array, not array.length = 0.
//
//   Rev 1.22   Mar 31 2005 15:19:14   jbroome
//Updated GetCoachJourneyFareDateIndex() to ensure that comparison is only on Date part of TDDateTime.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.21   Mar 31 2005 11:33:56   jbroome
//Ensure that request.ReturnDateTime array is always initialised.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.20   Mar 30 2005 17:39:18   jbroome
//Amended CreateTrunkCoachRequest to determine whether return journeys are required.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.19   Mar 30 2005 16:04:34   jbroome
//Added CombinePricingResultTickets() and CombineTickets() methods
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.18   Mar 30 2005 10:37:38   jbroome
//PriceRoute function now creates duplicate traveldates for Ticket Type Single|Singles.
//
//   Rev 1.17   Mar 23 2005 15:50:06   jbroome
//Made structs serializable, modified accessibility levels.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.16   Mar 23 2005 09:41:14   jbroome
//Added public PriceRoute method and associated private routines for Cost Based Searching.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.12   Oct 13 2004 15:42:32   ACaunt
//Logging added to record pricable portions of the journey and the associated fares
//
//   Rev 1.11   Jun 11 2004 15:35:40   acaunt
//Now uses TicketNameRule to generate appropriate SCL or NatEx ticket name.
//
//   Rev 1.10   May 28 2004 13:53:22   acaunt
//Updated to accomodate real NatEx and SCL data and with revised display rules.
//
//   Rev 1.9   Nov 24 2003 16:27:20   acaunt
//Checks to see if now fares have been found and handles gracefully.
//Also code tidyup.
//
//   Rev 1.8   Oct 27 2003 21:19:00   acaunt
//Makes use of the NatExFaresFilterPolicy.
//
//   Rev 1.7   Oct 26 2003 15:51:44   acaunt
//now ignores non general passenger types
//
//   Rev 1.6   Oct 23 2003 11:58:28   acaunt
//Now implements IPriceSupplier
//
//   Rev 1.5   Oct 22 2003 23:09:02   acaunt
//unit testing bug fixes
//
//   Rev 1.4   Oct 22 2003 17:47:06   acaunt
//Code compiles pre unit testing
//
//   Rev 1.3   Oct 22 2003 11:51:30   acaunt
//[Serializable] added
//
//   Rev 1.2   Oct 22 2003 11:40:04   acaunt
//Coded and builds
//
//   Rev 1.1   Oct 22 2003 10:17:22   acaunt
//add preprocess method
using System;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.PricingRetail.Logging;
using CJP =  TransportDirect.JourneyPlanning.CJPInterface;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
{	
	/// <summary>
	/// Organises the fare information associated with the PricingUnit from the data provided by Atkins
	/// and then supplies the appropriate details when required
	/// **** BIG WARNING ****
	/// Currently our understanding of the coach pricing information, in particular the use of the passengerType field vs the discountCardType
	/// field is poor. The processing below is based on the unproven assumption that the passengerType field is there for only information
	/// purposes and that all discount information can be determined using the discountCardType field.
	/// </summary>
	[Serializable]
	public class NatExFaresSupplier : IPriceSupplier
	{

		#region CombinedTicketIndexes

		/// <summary>
		/// Struct holds last-used indexes for outward, return
		/// and inward combined tickets.
		/// </summary>
		[Serializable]
			struct CombinedTicketIndexes
		{
			public int OutwardIndex, InwardIndex, ReturnIndex;
		}
		
		#endregion

		#region AdditionalFareProperties

		/// <summary>
		/// Struct holds values for StartLocationNaptan, EndLocationNaptan
		/// and PricingUnitIndex which apply to a CJP Fare, but 
		/// are not direct properties of it.
		/// </summary>
		[Serializable]
			struct AdditionalFareProperties
		{
			public string StartLocationNaptan, EndLocationNaptan;
			public int PricingUnitIndex;
		}
		
		#endregion

		#region Private members
		
		// Delegate allowing us the pass the name of the filter method to be used to Filter()
		private delegate bool FilterPolicy(Ticket ticket, Domain.PricingUnit pricingUnit);

		public static string NO_DISCOUNT = string.Empty;
		public static string GENERAL_PERSON_TYPE = string.Empty;
		
		// Collection of PricingResults keyed by Discount card
		// Package access for testing purposes
		private Hashtable singleResults = new Hashtable();
		private Hashtable returnResults = new Hashtable();
		private NatExFaresSupplier.CombinedTicketIndexes combinedTicketIndexes;

		private TicketNameRule nameRule = null;
		
		private ArrayList travelDates = null;
		private ArrayList inwardFareDates = null;
		private ArrayList outwardFareDates = null;

		private const string noResultsReturned = "CostSearchError.NoFaresResults";
		private const string noFaresReturned = "CostSearchError.FaresInternalError";

		#endregion

		#region Private methods

		/// <summary>
		/// Convert the fares provided by the CJP into a PricingResult objects (one for each discount card + one for undiscounted fares)
		/// </summary>
		/// <param name="cjpUnit"></param>
		/// <param name="itineraryType"></param>
		/// <returns></returns>
		private Hashtable CreatePricingResults(CJP.PricingUnit cjpUnit, ItineraryType itineraryType)
		{
			
			bool isSingle = (itineraryType == ItineraryType.Single);
			PricingResultsBuilder builder = new PricingResultsBuilder(nameRule);
			// find all the undiscounted fares of matching itineraryType and pass this information to the builder, to prepare all fares
			foreach (CJP.Fare fare in cjpUnit.prices)
			{
				if ((fare.discountCardType == null || fare.discountCardType == string.Empty) && fare.single == isSingle)
				{
					builder.AddUndiscountedFare(fare);
				}
			}
			// Retrieve the coach discount card types and add these to the builder
			foreach (string card in ((DataServices.DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices]).GetList(DataServiceType.DiscountCoachCards))
			{
				// Don't add the discount card name corresponding to 'No discount card'
				if (card != NO_DISCOUNT) 
				{
					builder.AddDiscountCard(card);
				}
			}
			// find all the discounted fares of matching itineraryType and pass this information to the builder to add discounted fares 
			foreach (CJP.Fare fare in cjpUnit.prices)
			{
				if ((fare.discountCardType != null && fare.discountCardType != string.Empty) && fare.single == isSingle)
				{
					builder.AddDiscountedFare(fare);
				} 
			}					
			return builder.GetPricingResults();
		}

		/// <summary>
		/// The complete set of fares associated with a journey may be larger than the ones that we are able to display.
		/// For example, certain return fares are only valid if the user has selected both and outward and return journey
		/// This method filters invalid fares before they are associated with a PricingUnit.
		/// </summary>
		/// <param name="unfilteredResult"></param>
		/// <param name="pricingUnit"></param>
		/// <param name="ssValid"></param>
		/// <returns></returns>
		private PricingResult FilterFares(PricingResult unfilteredResult, Domain.PricingUnit pricingUnit, FilterPolicy IsValid)
		{
			PricingResult filteredResult;
			// It may be that the are no results at all (this would happen if no fares information was provided in the first place
			// If this is the case then we create an empty PricingResult.
			// Otherwise we assumed that the filtered results will look like the unfiltered results
			if (unfilteredResult != null) 
			{
				filteredResult = (PricingResult)unfilteredResult.Clone();
				// When then go through each of the unfiltered tickets and if it isn't valid remove it from the filtered list.
				foreach (Ticket ticket in unfilteredResult.Tickets)
				{
					if (!IsValid(ticket, pricingUnit)) 
					{
						filteredResult.Tickets.Remove(ticket);
					}
				}
			} 
			else 
			{
				filteredResult = new PricingResult(0,0, false, false);
			}

			// Combine matching Tickets e.g. Standard Single Adult and Standard Single Child
			return CombinePricingResultTickets(filteredResult);
		}

		/// <summary>
		/// Method loops through Tickets collection of a PricingUnit and sees if
		/// any of the tickets can be combined. A Coach ticket will only ever have
		/// one fare value set, e.g. Adult, Child, DiscountedAdult, DiscountedChild.
		/// Matching Tickets will be combined together to create Tickets of the same
		/// type with multiple fare values set e.g. a Standard Single ticket with
		/// values for Adult, Child and DiscountedAdult fares.
		/// Internal visibility for NUnit testing purposes only.
		/// </summary>
		/// <param name="pricingResult">PricingResult to process</param>
		/// <returns>Processed PricingResult</returns>
		internal PricingResult CombinePricingResultTickets (PricingResult pricingResult)
		{
			IList origTickets = new ArrayList();
			ArrayList combinedTickets = new ArrayList();
			// Set reference to the original tickets collection
			origTickets = pricingResult.Tickets;
			
			// For each original Ticket
			foreach (Ticket origTicket in origTickets)
			{
				int ticketToCombine = -1;
				// See if it can be combined with another
				for (int i=0; i<combinedTickets.Count; i++)
				{
					if (CoachFareMergePolicy.AreTicketsMatching((Ticket)combinedTickets[i], origTicket))
					{
						ticketToCombine = i;
						break;					
					}
				}
				// Have found a ticket with which to combine so update combineTickets collection
				if (ticketToCombine > -1)
				{
					combinedTickets[ticketToCombine] = CombineTickets((Ticket)combinedTickets[ticketToCombine], origTicket);
				}
					// Haven't found a ticket with which we can combine
				else
				{
					combinedTickets.Add(origTicket);
				}
			}
			// Update Tickets collection of PricingResult to new 'combined' collection
			pricingResult.Tickets = combinedTickets;

			return pricingResult;
		}

		/// <summary>
		/// Method 'combines' two tickets, updating the relevant fare
		/// field of the first ticket with the value from the second.
		/// </summary>
		/// <param name="origTicket">Ticket</param>
		/// <param name="newTicket">Ticket to combine</param>
		/// <returns>Combined Ticket</returns>
		private Ticket CombineTickets(Ticket origTicket, Ticket newTicket)
		{
			if (!newTicket.AdultFare.Equals(float.NaN))
				origTicket.AdultFare = newTicket.AdultFare;
			else if (!newTicket.ChildFare.Equals(float.NaN))
				origTicket.ChildFare = newTicket.ChildFare;
			else if (!newTicket.DiscountedAdultFare.Equals(float.NaN))
				origTicket.DiscountedAdultFare = newTicket.DiscountedAdultFare;
			else if (!newTicket.DiscountedChildFare.Equals(float.NaN))
				origTicket.DiscountedChildFare = newTicket.DiscountedChildFare;

			return origTicket;
		}

		/// <summary>
		/// Method creates a copy of a TravelDate, setting any existing
		/// properties. The TicketType property is set according to the 
		/// TicketType property of the TravelDate being cloned.
		/// </summary>
		/// <param name="travelDate">TravelDate to clone</param>
		/// <param name="openReturn">bool openReturn</param>
		/// <param name="size">int size of original collection</param>
		/// <returns>cloned TravelDate</returns>
		private TravelDate GetSingleTravelDate(TravelDate travelDate, bool openReturn, int size)
		{
			TravelDate singleTravelDate = new TravelDate();
			
			singleTravelDate.Index = travelDate.Index + size;
			singleTravelDate.OutwardDate = travelDate.OutwardDate;
			singleTravelDate.ReturnDate = travelDate.ReturnDate;
			singleTravelDate.TravelMode = travelDate.TravelMode;
			singleTravelDate.TicketType = openReturn ? TicketType.Single : TicketType.Singles;
			
			return singleTravelDate;
		}
	

		/// <summary>
		/// Method updates the relevant index property within
		/// the combinedTicketIndexes object.
		/// </summary>
		/// <param name="outward">bool</param>
		/// <param name="single">bool</param>
		/// <returns>int of correct index</returns>
		private int GetCombinedTicketIndex(bool outward, bool single)
		{
			if (outward)
			{
				if (single)
					return combinedTicketIndexes.OutwardIndex;
				else
					return combinedTicketIndexes.ReturnIndex;
			}
			else
			{
				return combinedTicketIndexes.InwardIndex;
			}
		}

		/// <summary>
		/// Method updates the relevant index property within
		/// the combinedTicketIndexes object.
		/// </summary>
		/// <param name="outward">bool</param>
		/// <param name="single">bool</param>
		private void UpdateCombinedTicketIndex(bool outward, bool single)
		{
			if (outward)
			{
				if (single)
					combinedTicketIndexes.OutwardIndex ++;
				else
					combinedTicketIndexes.ReturnIndex ++;
			}
			else
				combinedTicketIndexes.InwardIndex ++;
		}

		/// <summary>
		/// Method returns an AdditionalFareProperties object, holding
		/// the indexes of the first and last legs to which a pricing unit applies.
		/// These values apply to a CJP Fare, but are not direct 
		/// properties of it. They are stored together seperately and 
		/// accessed when creating a CoachPricingUnitFare from the CJP Fare.
		/// </summary>
		/// <param name="journey">PublicJourney</param>
		/// <param name="pricingUnit">index of pricing unit</param>
		/// <returns>AdditionalFareProperties struct</returns>
		private AdditionalFareProperties GetAdditionalFareProperties(PublicJourney journey, int pricingUnit)
		{
			AdditionalFareProperties properties = new AdditionalFareProperties();
			// Obtain the index of the first and last journey legs to which the pricing unit applies
			int firstLegId = journey.Fares[pricingUnit].legs[0];
			int lastLegId = journey.Fares[pricingUnit].legs[journey.Fares[pricingUnit].legs.Length-1];
			
			properties.PricingUnitIndex = pricingUnit; 
			properties.StartLocationNaptan = journey.Details[firstLegId].LegStart.Location.NaPTANs[0].Naptan;
			properties.EndLocationNaptan = journey.Details[lastLegId].LegEnd.Location.NaPTANs[0].Naptan;

			return properties;
		}

		/// <summary>
		/// Gets the index from the OutwardFareDates or InwardFareDates collections 
		/// of a CoachJourneyFareDate, for the date specified
		/// </summary>
		/// <param name="date">TDDateTime</param>
		/// <param name="outward">bool outward/inward</param>
		/// <returns>Index of CoachJourneyFareDate</returns>
		private int GetCoachJourneyFareDateIndex(TDDateTime date, bool outward)
		{
			int result = -1;
			if (outward)
			{
				for (int i=0; i<outwardFareDates.Count; i++)
				{
					CoachJourneyFareDate journeyFareDate = (CoachJourneyFareDate)outwardFareDates[i];
					if (TDDateTime.AreSameDate(date, journeyFareDate.Date))
					{
						result = i;
						break;
					}
				}
			}
			else
			{
				for (int i=0; i<inwardFareDates.Count; i++)
				{
					CoachJourneyFareDate journeyFareDate = (CoachJourneyFareDate)inwardFareDates[i];
					if (TDDateTime.AreSameDate(date, journeyFareDate.Date))
					{
						result = i;
						break;
					}
				}
			}
			return result;
		}

		/// <summary>
		/// Method populates the Outward/Inward/Return Tickets 
		/// collection of a TravelDate with CostSearchTickets.
		/// </summary>
		/// <param name="travelDate">TravelDate to update</param>
		/// <param name="request">ITDJourneyRequest</param>
		/// <param name="result">ITDJourneyResult</param>
		private void UpdateTravelDateFaresAndTickets(TravelDate travelDate, ITDJourneyRequest request, PublicJourneyStore journeyStore)
		{
			ArrayList outwardTickets = new ArrayList();
			ArrayList inwardTickets = new ArrayList();
			int outwardIndex = 0, inwardIndex = 0;

			if ((travelDate.TicketType == TicketType.OpenReturn) || (travelDate.TicketType == TicketType.Return))
			{
				// Get index of correct outward fare date
				outwardIndex = GetCoachJourneyFareDateIndex(travelDate.OutwardDate, true);
				// Get the CostSearchTickets from each CoachJourneyFareSummary for FareDate
				for (int i=0; i<((CoachJourneyFareDate)outwardFareDates[outwardIndex]).FareSummary.Length; i++) 
				{
					// Only concerned with the return fares (will all be the same within a summary)
					if (((CoachJourneyFareDate)outwardFareDates[outwardIndex]).FareSummary[i].JourneyFares[0].IsSingle == false)
						outwardTickets.AddRange( CreateCostSearchTickets(travelDate, ((CoachJourneyFareDate)outwardFareDates[outwardIndex]).FareSummary[i], true, request, journeyStore) );
				}
				// Add CostSearchTickets to relevant collection
				foreach (CostSearchTicket ticket in outwardTickets)
				{
					if (travelDate.TicketType == TicketType.Return)
					{
						ArrayList inwardJourneyIndexes = new ArrayList();
						// Get first outward journey for ticket. Only concerned with generic properties
						// of journey which will be the same for each outward journey for a ticket.
						int outwardJourneyIndex = ((int)ticket.JourneysForTicket.OutwardJourneyIndexes[0]);
						int outDateIndex = journeyStore.GetDateIndex(travelDate.OutwardDate, true);
						int retDateIndex = journeyStore.GetDateIndex(travelDate.ReturnDate, false);
						PublicJourney outwardJourney = journeyStore.GetOutwardJourneysForDate(outDateIndex)[outwardJourneyIndex];

						// Get collection of public journeys for travel date's return date
						if (retDateIndex != -1)
						{
							foreach (PublicJourney inwardJourney in journeyStore.GetReturnJourneysForDate(retDateIndex))
							{
								if (TDDateTime.AreSameDate(inwardJourney.JourneyDate, travelDate.ReturnDate))
								{
									// Store valid inward journeys for the ticket's outward journeys
									if (CoachFareMergePolicy.IsInwardJourneyValid(outwardJourney, inwardJourney))
									{
										int inwardJourneyIndex = journeyStore.GetIndexForJourney(retDateIndex, false, inwardJourney);
										inwardJourneyIndexes.Add (inwardJourneyIndex);
									}
								}
							}
						}
						// Add valid inward journeys to ticket's InwardJourneys collection
						ticket.JourneysForTicket.InwardJourneyIndexes = inwardJourneyIndexes;
						// Add ticket to the TravelDates.ReturnTickets collection	
						travelDate.AddReturnTicket(ticket);
					}
					else if (travelDate.TicketType == TicketType.OpenReturn)
					{
						// Only add flexible CostSearchTickets for OpenReturn
						if (ticket.Flexibility != Flexibility.NoFlexibility) 
							travelDate.AddOutwardTicket(ticket);
					}
				}
			}
			else // TicketType = Single or Singles
			{
				// Get index of correct outward fare date
				outwardIndex = GetCoachJourneyFareDateIndex(travelDate.OutwardDate, true);
				// Get the CostSearchTickets from each CoachJourneyFareSummary for FareDate
				for (int i=0; i<((CoachJourneyFareDate)outwardFareDates[outwardIndex]).FareSummary.Length; i++) 
				{
					// Only concerned with the single fares (will all be the same within a summary)
					if (((CoachJourneyFareDate)outwardFareDates[outwardIndex]).FareSummary[i].JourneyFares[0].IsSingle == true)
						outwardTickets.AddRange( CreateCostSearchTickets(travelDate, ((CoachJourneyFareDate)outwardFareDates[outwardIndex]).FareSummary[i], true, request, journeyStore) );
				}
				// Add CostSearchTickets to OutwardTickets collection
				foreach (CostSearchTicket ticket in outwardTickets)
				{
					travelDate.AddOutwardTicket(ticket);
				}

				// If dealing with Singles, then add the single Inwards as well
				if (travelDate.TicketType == TicketType.Singles)
				{
					// Get the index of correct inward fare date
					inwardIndex = GetCoachJourneyFareDateIndex(travelDate.ReturnDate, false);
					// Get the CostSearchTickets from each CoachJourneyFareSummary for FareDate
					for (int i=0; i<((CoachJourneyFareDate)inwardFareDates[inwardIndex]).FareSummary.Length; i++) 
					{
						inwardTickets.AddRange( CreateCostSearchTickets(travelDate, ((CoachJourneyFareDate)inwardFareDates[inwardIndex]).FareSummary[i], false, request, journeyStore) );
					}
					// Add CostSearchTickets to InwardTickets collection
					foreach (CostSearchTicket ticket in inwardTickets)
					{
						travelDate.AddInwardTicket(ticket);
					}
				}
			}
		}

		/// <summary>
		/// This method loops through all the CoachJourneyFares in a 
		/// CoachJourneyFareSummary (which were created in ExtractFareInformationFromJourney)
		/// and creates corresponding CostSearchTickets. 
		/// </summary>
		/// <param name="travelDate">TravelDate</param>
		/// <param name="journeyFareSummary">CoachJourneyFareSummary containing fares</param>
		/// <param name="outward">bool outward journeys</param>
		/// <param name="request">original ITDJourneyRequest</param>
		/// <param name="resuly">original ITDJourneyResult</param>
		/// <returns>CostSearchTicket array</returns>
		private CostSearchTicket[] CreateCostSearchTickets(	TravelDate travelDate, 
															CoachJourneyFareSummary journeyFareSummary, 
															bool outward, 
															ITDJourneyRequest request,
															PublicJourneyStore journeyStore)
		{
			PricingResultsHelper helper = new PricingResultsHelper();
			// All coachJourneyFares within a fare summary will be same single/return
			bool singleFares = journeyFareSummary.JourneyFares[0].IsSingle;
			int pricingUnits = journeyFareSummary.JourneyFares[0].PricingUnitFares.Length;
			// Create one CostSearchTicket for each PricingUnit
			CostSearchTicket[] tickets = new CostSearchTicket[pricingUnits];
			
			if (pricingUnits > 1)
			{
				// Increment the correct last-used combined ticket index
				UpdateCombinedTicketIndex(outward, singleFares);
			}

			// For each Pricing Unit
			for (int i=0; i<pricingUnits; i++)
			{
				bool firstFare = true;

				// For each fare in summary
				foreach (CoachJourneyFare coachJourneyFare in journeyFareSummary.JourneyFares)
				{
					CoachPricingUnitFare pricingUnit = coachJourneyFare.PricingUnitFares[i];
					// Only create the CostSearch Ticket once per PricingUnit. 
					// Some generic properties will be the same for each CoachJourneyFare.
					if (firstFare)
					{
						tickets[i] = new CostSearchTicket(pricingUnit.FareType,pricingUnit.Flexibility, string.Empty);	
						tickets[i].TravelDateForTicket = travelDate;
						tickets[i].MaxChildAge = Convert.ToUInt32(pricingUnit.MaxChildFare);
						tickets[i].MinChildAge = Convert.ToUInt32(pricingUnit.MinChildFare);
						if (pricingUnits == 1)
							tickets[i].CombinedTicketIndex = 0;
						else
							// CombinedTicketIndex can be used to re-group CostSearchTickets into Pricing Units 
							tickets[i].CombinedTicketIndex = GetCombinedTicketIndex(outward, coachJourneyFare.IsSingle);

						firstFare = false;
					}
				
					// Each CoachJourneyFare in the FareSummary should update a
					// a different fare property e.g. Adult, Child, Child Discounted etc.
					helper.SetConvertedFare(tickets[i], coachJourneyFare.IsAdult, coachJourneyFare.IsDiscounted, coachJourneyFare.PricingUnitFares[i].FareAmount);
				}
				
				// Create a CostBasedJourney for this CostSearchTicket and add 
				// every journey in this FareSummary to its Inward/Outward Journeys collection
				CostBasedJourney costBasedJourney = new CostBasedJourney();
				costBasedJourney.OutwardDateIndex = journeyStore.GetDateIndex(travelDate.OutwardDate, true);  
				costBasedJourney.InwardDateIndex = journeyStore.GetDateIndex(travelDate.ReturnDate, false); 

				foreach (PublicJourney publicJourney in journeyFareSummary.Journeys)
				{
					
					if (outward)
					{
						int journeyIndex = journeyStore.GetIndexForJourney(costBasedJourney.OutwardDateIndex, true, publicJourney);
						costBasedJourney.OutwardJourneyIndexes.Add(journeyIndex);
					}
					else
					{
						int journeyIndex = journeyStore.GetIndexForJourney(costBasedJourney.InwardDateIndex, false, publicJourney);
						costBasedJourney.InwardJourneyIndexes.Add(journeyIndex);
					}
					
				}

				costBasedJourney.CostJourneyRequest = (TDJourneyRequest)request;

				tickets[i].JourneysForTicket = costBasedJourney;
			}

			return tickets;
		}

		/// <summary>
		/// This method loops through each CJP Fare within the public journey
		/// and creates corresponding CoachPricingUnitFares, which are grouped into
		/// CoachJourneyFares and added to CoachJourneyFareSummarys for the outward / 
		/// inward CoachJourneyFareDates.
		/// </summary>
		/// <param name="journey">PublicJourney from TDJourneyResult</param>
		/// <param name="outward">bool outward / inward</param>
		private void ExtractFareInformationFromJourney(PublicJourney journey, bool outward, string discountCard)
		{
			TDDateTime departureDate = journey.JourneyDate;

			// For each PricingUnit
			for (int currentPUIndex=0; currentPUIndex<journey.Fares.Length; currentPUIndex++)
			{
				// For each Fare within the PricingUnit
				foreach (CJP.Fare fare in journey.Fares[currentPUIndex].prices)
				{
					// If this is an inward journey, ignore return fares
					if (!outward && !fare.single) 
					{ break; }
					else
					{
						// If dealing with multiple PricingUnits, can only proceed if fares 
						// can be combined from different PricingUnits to cover whole journey
						bool wholeJourneyCovered = true;
						// ArrayLists to store Fares and OperatorCodes from each PricingUnit
						ArrayList allFares = new ArrayList();
						ArrayList additionalProperties = new ArrayList();
						ArrayList legsCovered = new ArrayList();
						// Add Fare and AdditionalFareProperties from current PricingUnit
						allFares.Add(fare);
						additionalProperties.Add(GetAdditionalFareProperties(journey, currentPUIndex));

						// Search through all other PricingUnits (if any) for combinable fares
						for (int i=0; i<journey.Fares.Length; i++)
						{
							legsCovered.AddRange(journey.Fares[i].legs);
							// Ignore current PricingUnit
							if (i!=currentPUIndex)
							{
								bool containsCombinableFare = false;
								// Use CoachFareMergePolicy to determine whether a fare can
								// be combined with the one from the current PricingUnit
								foreach (CJP.Fare fareToCombine in journey.Fares[i].prices)
								{
									if (CoachFareMergePolicy.CanFaresBeCombined(fare, fareToCombine))
									{
										containsCombinableFare = true;
										if (i>allFares.Count)
										{
											allFares.Add(fareToCombine);
											additionalProperties.Add(GetAdditionalFareProperties(journey, i));
										}
										else
										{
											allFares.Insert(i, fareToCombine);
											additionalProperties.Insert(i, GetAdditionalFareProperties(journey, i));
										}
										break;
									}
								}
								// Whole journey only covered if all PricingUnits contain a combinable Fare
								if (!containsCombinableFare) 
									wholeJourneyCovered = false;
							}
						}

						// Check that every leg is covered by a PricingUnit
						for (int i=0; i<journey.Details.Length; i++)
						{
							if (!legsCovered.Contains(i)) wholeJourneyCovered = false;
						}

						// Only proceed if whole journey is covered by Fares
						if (wholeJourneyCovered)
						{
							PricingResultsHelper helper = new PricingResultsHelper();
							// Instantiate and populate a CoachJourneyFare containing a CoachPricingUnitFare
							// for each Fare that makes up whole journey 
							CoachPricingUnitFare[] pricingUnitFares = new CoachPricingUnitFare[allFares.Count];

							// Create pricing unit for each fare and add to array
							for (int i=0; i<allFares.Count; i++)
							{
								CJP.Fare journeyFare = (CJP.Fare)allFares[i];

								// Set max / min child ages and Flexibility using helper
								int[] childAges = helper.GenerateChildAges(journeyFare.childAgeRange);
								Flexibility flexibility = helper.ConvertFlexibility(journeyFare);
								float fareAmount = helper.ConvertFare(journeyFare.fare);
								// Retrieve the additional properties
								AdditionalFareProperties additionalProps = (AdditionalFareProperties)additionalProperties[i];
								
								// Create pricing unit for correct fare to be combined and add to array
								pricingUnitFares[i] = new CoachPricingUnitFare( additionalProps.StartLocationNaptan, 
									additionalProps.EndLocationNaptan,
									fareAmount,
									journeyFare.fareType, 
									journeyFare.fareTypeConditions,
									additionalProps.PricingUnitIndex,
									journeyFare.discountCardType,
									childAges[0],childAges[1],
									flexibility);
							}

							CoachJourneyFare newJourneyFare = new CoachJourneyFare(pricingUnitFares);
							
							// Need to filter out some discounted fares
							bool ignoreDiscountedFare = false;
							// If user not specified a discount card, ignore all discounted fares
							if ((discountCard == null || discountCard.Length == 0) && newJourneyFare.IsDiscounted)
							{
								ignoreDiscountedFare = true;
							}
								// If user specified a discount card, ignore all 
								// discounted fares for other discount card types.
							else if ((discountCard.Length > 0 && newJourneyFare.IsDiscounted) && (newJourneyFare.DiscountCard != discountCard))
							{
								ignoreDiscountedFare = true;
							}

							if (!ignoreDiscountedFare)
							{
								// As CoachPricingUnitFares are already deemed to match, we know 
								// that IsAdult and IsSingle properties are the same for each.
								newJourneyFare.IsAdult = fare.adult;
								newJourneyFare.IsSingle = fare.single;
						
								// Determine CoachJourneyFareDate index from outward/inward collection
								int index = GetCoachJourneyFareDateIndex(departureDate, outward);
								if (index > -1)
								{
									// For this date, see if the CoachJourneyFareDate contains a
									// CoachJourneyFareSummary with a matching CoachJourneyFare
									bool journeyFareAlreadyExistsInFareSummary = false;

									// Get reference to correct CoachJourneyFareDate
									CoachJourneyFareDate coachJourneyFareDate = outward ? (CoachJourneyFareDate)outwardFareDates[index] : (CoachJourneyFareDate)inwardFareDates[index];

									// For each CoachJourneyFareSummary in the coachJourneyFareDate
									foreach(CoachJourneyFareSummary exisitingFareSummary in coachJourneyFareDate.FareSummary)
									{ 
										foreach (CoachJourneyFare existingJourneyFare in exisitingFareSummary.JourneyFares)
										{
											if (CoachFareMergePolicy.AreFaresMatching(existingJourneyFare, newJourneyFare))
											{
												journeyFareAlreadyExistsInFareSummary = true;

												// Add reference to this PublicJourney to the Journeys collection of 
												// the CoachJourneyFareSummary. Only distinct PublicJourneys will be added.
												exisitingFareSummary.AddDistinctJourney(journey);
									
												// And, if this fare type (adult/child etc) is not yet represented
												// in JourneyFares, add the new CoachJourneyFare.
												// Only distinct CoachJourneyFares will be added.
												exisitingFareSummary.AddDistinctJourneyFare(newJourneyFare);
												break;
											}
										}
										// If already found a matching CoachJourneyFare then do not search other summaries
										if (journeyFareAlreadyExistsInFareSummary)
											break;
									}
									// If CoachJourneyFare does not exist in any CoachJourneyFareSummary 
									if (!journeyFareAlreadyExistsInFareSummary || (coachJourneyFareDate.FareSummary.Length == 0))
									{
										// Instantiate a new CoachJourneyFareSummary
										string originNaPTAN = journey.Details[0].LegStart.Location.NaPTANs[0].Naptan;
										string destinationNaPTAN = journey.Details[journey.Details.Length-1].LegEnd.Location.NaPTANs[0].Naptan;
										CoachJourneyFareSummary newJourneyFareSummary = new CoachJourneyFareSummary(originNaPTAN, destinationNaPTAN);
											
										// Populate it with the new CoachJourneyFare and add reference to PublicJourney
										newJourneyFareSummary.AddDistinctJourneyFare(newJourneyFare);
										newJourneyFareSummary.AddDistinctJourney(journey);
											
										// Add it to the FareSummary list for the CoachJourneyFareDate
										coachJourneyFareDate.AddFareSummary(newJourneyFareSummary);
									}
								}
								else
								{
									// If cannot find an associated CoachJourneyFareDate, then CJP has 
									// returned journey that starts on a date not covered by a travel date. 
									// This should not happen, but just in case...
									Logger.Write( new OperationalEvent(TDEventCategory.CJP, TDTraceLevel.Error, string.Format("CJP has returned Public Journey with JourneyDate of '{0}'. This does not match any TDDateTime included in the request.", departureDate.ToString())));
								}
							}
						}
					}
				}					
			}
		}

		/// <summary>
		/// Returns a trunk coach TDJourneyRequest object populated with
		/// each outward and return date.
		/// </summary>
		/// <param name="origin">TDLocation</param>
		/// <param name="destination">TDLocation</param>
		/// <param name="outwardDates">Array of outward TDDateTime</param>
		/// <param name="returnDates">Array of return TDDateTime</param>
		/// <param name="returnRequired">bool</param>
		/// <returns>TDJourneyRequest</returns>
		private ITDJourneyRequest CreateTrunkCoachRequest(	TDLocation origin, 
															TDLocation destination, 
															TDDateTime[] outwardDates, 
															TDDateTime[] returnDates,
															bool openReturn)
		{
			ITDJourneyRequest request = new TDJourneyRequest();
			
			request.IsReturnRequired = !openReturn;
			request.OriginLocation = origin;
			request.DestinationLocation = destination;
			request.IsTrunkRequest = true;
			request.Modes = new CJP.ModeType[1];
			request.Modes[0] = CJP.ModeType.Coach;
            request.OutwardDateTime = outwardDates;
			request.OutwardAnyTime = true;
			request.ReturnDateTime = returnDates;
			request.ReturnAnyTime = true;

			return request;
		}

		/// <summary>
		/// Method retreives a CJPManager from the Service Discovery and invokes 
		/// the CJP with the specified trunk coach request object.
		/// </summary>
		/// <param name="request">ITDJourneyRequest trunk coah request</param>
		/// <param name="sessionInfo">CJPSessionInfo</param>
		/// <returns></returns>
		private ITDJourneyResult CallCJP(ITDJourneyRequest request, CJPSessionInfo sessionInfo)
		{
			// Get a CJP Manager from the service discovery
			ICJPManager cjpManager = (ICJPManager) TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];

			ITDJourneyResult result = cjpManager.CallCJP(	request, 
				sessionInfo.SessionId, 
				sessionInfo.UserType, 
				false, // Reference transaction?
				sessionInfo.IsLoggedOn, 
				sessionInfo.Language, 
				false); // IsExtension?

			return result;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Default Constructor
		/// </summary>
		public NatExFaresSupplier()
		{
		}

		/// <summary>
		/// Constructor which sets up the nameRule property
		/// </summary>
		/// <param name="nameRule">TicketNameRule</param>
		public NatExFaresSupplier(TicketNameRule nameRule)
		{
			this.nameRule = nameRule;
		}
	
		/// <summary>
		/// Implementation of IPriceSupplier.PreProcess.
		/// </summary>
		/// <param name="fareData"></param>
		public void PreProcess(UnprocessedFareData fareData)
		{
			// Retrieve the underlying 
			CJP.PricingUnit cjpUnit = fareData.UnprocessedData as CJP.PricingUnit;
			//Make sure we have something
			if (cjpUnit != null)
			{
				// Tickets required undiscounted and, if a discount card is present, discounted fares
				// Our strategy is to create Tickets for undiscounted fares and then to make copies of these are required
				// when we need to record discounted fare information

				// Create the Hashtables for the single and return fares
				singleResults = CreatePricingResults(cjpUnit, ItineraryType.Single);
				returnResults = CreatePricingResults(cjpUnit, ItineraryType.Return);
			} 
			else 
			{
				throw new Exception ("Unable to convert unprocessed data into a PricingUnit, type is "+fareData.UnprocessedData.GetType());
			}
		}

		/// <summary>
		/// Implementation of IPriceSupplier.Price
		/// </summary>
		/// <param name="pricingUnit"></param>
		/// <param name="discounts"></param>
		public void PricePricingUnit(Domain.PricingUnit pricingUnit, Discounts discounts)
		{
			if (TDTraceSwitch.TraceVerbose) 
			{
				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, PricingRequestMessage.Message(pricingUnit)));
			}

			string discountCard = (discounts.CoachDiscount != null && discounts.CoachDiscount != string.Empty) ? discounts.CoachDiscount : NO_DISCOUNT;
			// Check that the card exists in the single and return results for safety.
			// If it doesn't assume that we don't have any fares information for that discount card. ie. take the undiscounted fares
			string singleCard = singleResults.ContainsKey(discountCard) ? discountCard : NO_DISCOUNT;
			string returnCard = singleResults.ContainsKey(discountCard) ? discountCard : NO_DISCOUNT;
			// Retrieve the appropriate PricingResults from the collections
			PricingResult unfilteredSingleFares = (PricingResult)singleResults[singleCard];
			PricingResult unfilteredReturnFares = (PricingResult)returnResults[returnCard];

			// Filter the fares so that only fares appropriate to the selected journey are returned
			PricingResult singleFares = FilterFares(unfilteredSingleFares, pricingUnit, new FilterPolicy(NatExFaresFilterPolicy.IsValidSingle));
			PricingResult returnFares = FilterFares(unfilteredReturnFares, pricingUnit, new FilterPolicy(NatExFaresFilterPolicy.IsValidReturn));

			// Return the filtered set of fares
			pricingUnit.SetFares(singleFares, returnFares);
			if (TDTraceSwitch.TraceVerbose)
			{
				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, PricingResponseMessage.Message(pricingUnit)));
			}
		}


		/// <summary>
		/// Implementation of IPriceSupplier.PriceRoute
		/// Calls the CJP with each outward/return travel date. Extracts CJP 
		/// fare info from results and builds up fare summaries for dates. These are then
		/// used to create CostSearchTickets which are added to the TravelDates.
		/// Returns string array of error codes
		/// </summary>
		/// <param name="dates">Array List of TravelDate objects</param>
		/// <param name="origin">TDLocation</param>
		/// <param name="destination">TDLocation</param>
		/// <param name="dicounts">Discounts</param>
		/// <returns>String array of error codes</returns>
		public string[] PriceRoute(ArrayList dates, TDLocation origin, TDLocation destination, Discounts discounts, CJPSessionInfo sessionInfo, out PublicJourneyStore journeyStore)
		{			
			if (TDTraceSwitch.TraceVerbose) 
			{
				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, PriceRouteRequestMessage.Message(dates, origin, destination, discounts)));
			}

			// Initialise variables
			bool openReturn = true;
			ArrayList errors = new ArrayList();
			ArrayList distinctOutwardDates = new ArrayList();
			ArrayList distinctInwardDates = new ArrayList();
			ArrayList singleTravelDates = new ArrayList();
			this.travelDates = dates;
			this.inwardFareDates = new ArrayList();
			this.outwardFareDates = new ArrayList();

			// Set up CoachJourneyFareDates
			foreach (TravelDate travelDate in travelDates)
			{
				// Are we dealing with return TravelDates? (they will all be the same)
				if (travelDate.TicketType == TicketType.Return) openReturn = false;
				// Create CoachJourneyFareDate objects for each distinct outward and return date
				if (distinctOutwardDates.IndexOf(travelDate.OutwardDate) == -1)
				{
					distinctOutwardDates.Add(travelDate.OutwardDate); 
					outwardFareDates.Add(new CoachJourneyFareDate(travelDate.OutwardDate));
				}
				if ((!openReturn) && (distinctInwardDates.IndexOf(travelDate.ReturnDate) == -1))
				{
					distinctInwardDates.Add(travelDate.ReturnDate); 
					inwardFareDates.Add(new CoachJourneyFareDate(travelDate.ReturnDate));
				}
				// All TravelDates will have TicketType either OpenReturn or Return
				// Need to create duplicates with corresponding TicketType Single or Singles
				singleTravelDates.Add(GetSingleTravelDate(travelDate, openReturn, travelDates.Count));
			}

			// Add duplicate 'single' travel dates to main collection
			this.travelDates.AddRange(singleTravelDates);

			// Convert lists of distinct dates into TDDateTime arrays
			TDDateTime[] outwardDates = (TDDateTime[])distinctOutwardDates.ToArray(typeof(TDDateTime));
			TDDateTime[] returnDates = openReturn ? new TDDateTime[0] : (TDDateTime[])distinctInwardDates.ToArray(typeof(TDDateTime));
			
			// Create trunk coach journey request
			ITDJourneyRequest request = CreateTrunkCoachRequest(	origin, 
				destination, 
				outwardDates, 
				returnDates, 
				openReturn);
			// Call the CJP
			ITDJourneyResult result = CallCJP(request, sessionInfo);
            
			// Check for CJP errors
			if (result.CJPMessages.Length >0)
			{
				foreach (CJPMessage message in result.CJPMessages)
				{
					if (message.MessageResourceId == JourneyControlConstants.JourneyWebNoResults)
					{
						// If no journeys returned, then no fares returned. Do not proceed.
						errors.Add(noResultsReturned);
						journeyStore = null;
						return (string[]) errors.ToArray(typeof(string));
					}
					else
					{
						errors.Add(message.MessageResourceId);
					}
				}
			}

			//Populate journey store
			journeyStore = new PublicJourneyStore(result.OutwardPublicJourneys, result.ReturnPublicJourneys);

			// For each outward PublicJourney within the result, process the fare information
			foreach (PublicJourney outwardJourney in result.OutwardPublicJourneys)
			{	
				if ((outwardJourney.Fares != null)&&(outwardJourney.Fares.Length != 0))
				{
					ExtractFareInformationFromJourney (outwardJourney, true, discounts.CoachDiscount); 
				}
			}

			// For each inward PublicJourney within the result, process the fare information
			foreach (PublicJourney inwardJourney in result.ReturnPublicJourneys)
			{	
				if ((inwardJourney.Fares != null)&&(inwardJourney.Fares.Length != 0))
				{
					ExtractFareInformationFromJourney (inwardJourney, false, discounts.CoachDiscount); 
				}
			}
		
			//  If CJP has returned no Fares information do not proceed
			bool outwardJourneyFares = ((((CoachJourneyFareDate)outwardFareDates[0]).FareSummary != null) && (((CoachJourneyFareDate)outwardFareDates[0]).FareSummary.Length != 0));
			bool returnJourneyFares = (inwardFareDates.Count > 0) && ((((CoachJourneyFareDate)inwardFareDates[0]).FareSummary != null) && (((CoachJourneyFareDate)inwardFareDates[0]).FareSummary.Length != 0));

			if (!outwardJourneyFares && !returnJourneyFares)
			{
				errors.Add(noFaresReturned);
				return (string[]) errors.ToArray(typeof(string));
			}

			// Boolean flags used to determine status of tickets returned
			bool singleOutwardTickets = false; 
			bool singleInwardTickets = false;

            combinedTicketIndexes = new CombinedTicketIndexes();

            foreach (TravelDate travelDate in travelDates)
			{
				// Update each TravelDate with new tickets and fare info
				UpdateTravelDateFaresAndTickets(travelDate, request, journeyStore);
			
				// Were any outward or inward single tickets returned?
				if ((travelDate.TicketType == TicketType.Single) || (travelDate.TicketType == TicketType.Singles))
				{
					if ((travelDate.OutwardTickets!= null)&&(travelDate.OutwardTickets.Length > 0)) singleOutwardTickets = true;  
					if (travelDate.TicketType == TicketType.Singles)
					{
						if ((travelDate.InwardTickets!= null)&&(travelDate.InwardTickets.Length > 0)) singleInwardTickets = true;  
						// Flag for 'partial results' errors
						travelDate.ErrorForOutward = !singleOutwardTickets;
						travelDate.ErrorForInward = !singleInwardTickets;
					}
				}
			}

			// If no return date selected, but no outward single tickets,  
			// then indicate that no fares have been returned
			if ((openReturn && !singleOutwardTickets))
			{
				errors.Add(noResultsReturned);
			}

			if (TDTraceSwitch.TraceVerbose) 
			{
				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, PriceRouteResponseMessage.Message(dates)));
			}
		
			return (string[])errors.ToArray(typeof(string));
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Read-only Hashtable property.
		/// Collection of Single PricingResults 
		/// keyed by Discount card.
		/// </summary>
		public Hashtable SingleResults
		{
			get {return singleResults;}
		}

		/// Read-only Hashtable property.
		/// Collection of Return PricingResults 
		/// keyed by Discount card.
		public Hashtable ReturnResults
		{
			get {return returnResults;}
		}

		/// <summary>
		/// Read-only CoachJourneyFareDate array property.
		/// Collection of CoachJourneyFareDates that apply to the 
		/// OutwardDate values of the TravelDates specified in PriceRoute.
		/// Needs visibility only for testing purposes, so 
		/// restricted to current assembly using 'internal'.
		/// </summary>
		internal CoachJourneyFareDate[] InwardFareDates
		{
			get 
			{ 
				return (CoachJourneyFareDate[])inwardFareDates.ToArray(typeof(CoachJourneyFareDate));
			}
		}

		/// <summary>
		/// Read-only CoachJourneyFareDate array property.
		/// Collection of CoachJourneyFareDates that apply to the 
		/// InwardDate values of the TravelDates specified in PriceRoute.
		/// Needs visibility only for testing purposes, so 
		/// restricted to current assembly using 'internal'.
		/// </summary>
		internal CoachJourneyFareDate[] OutwardFareDates
		{
			get 
			{ 
				return (CoachJourneyFareDate[])outwardFareDates.ToArray(typeof(CoachJourneyFareDate));
			}
		}


		#endregion

	}
}
