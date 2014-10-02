// ************************************************************** 
// NAME			: TravelDate.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 22/12/2004 
// DESCRIPTION	: Implementation of the TravelDate class
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/TravelDate.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:00   mturner
//Initial revision.
//
//   Rev 1.27   Jan 17 2006 18:14:12   RPhilpott
//Code review updates.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.26   Jan 17 2006 18:07:32   RPhilpott
//Code review fixes.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.25   Dec 08 2005 12:23:04   RPhilpott
//Correction to min/max fare calculation for multi-tickets routes.
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.24   Nov 30 2005 09:38:20   RPhilpott
//Correct and simplfy comination of tickets on multi-leg journeys.
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.23   Nov 24 2005 18:23:02   RPhilpott
//Changes to support use of ticket-specific locations in Find-A-Fare AssembleServices calls.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.22   Nov 17 2005 18:28:28   RPhilpott
//Extra logging for coach fares.
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.21   Nov 09 2005 12:31:36   build
//Automatically merged from branch for stream2818
//
//   Rev 1.20.1.3   Nov 08 2005 16:55:34   RPhilpott
//NUnit fixes.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.20.1.2   Nov 07 2005 20:50:00   RPhilpott
//Open Returns are in Outwards Ticket Collection.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.20.1.1   Nov 07 2005 18:23:22   RPhilpott
//Default TicketType to None.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.20.1.0   Nov 04 2005 16:17:42   RPhilpott
//NUnit fixes.
//Resolution for 2818: DEL 8 Stream: Search by Price
//


using System;
using System.Text;
using System.Collections;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Summary description for TravelDate.
	/// </summary>
	[Serializable]
	public class TravelDate
	{
		#region Private members

		private static readonly string nl = Environment.NewLine;

		private int travelDateIndex;
		private TDDateTime outwardDate;
		private TDDateTime returnDate;		
		private TicketTravelMode travelMode;		
		private bool unlikelyToBeAvailable;
		private float minChildFare = float.NaN;
		private float maxChildFare = float.NaN;
		private float minAdultFare = float.NaN;
		private float maxAdultFare = float.NaN;
		private float lowestProbableAdultFare = float.NaN;
		private float lowestProbableChildFare = float.NaN;
		private bool errorForOutward;
		private bool errorForInward;
		private Probability combinedTicketProbability;

		//ticket type
		private TicketType ticketType = TicketType.None;		

		//tickets for this travel date
		private CostSearchTicket[] outwardTickets;
		private CostSearchTicket[] inwardTickets;
		private CostSearchTicket[] returnTickets;

		#endregion
		
		#region Private methods
		
		/// <summary>
		/// Method for adding a new value to an array of CostSearchTicket. 
		/// The NatExFaresSupplier needs to add CostSearchTickets dynamically
		/// during the cost based search process.
		/// </summary>
		/// <param name="array">Array of CostSearchTicket</param>
		/// <param name="ticket">new CostSearchTicket object</param>
		private void AddTicketToArray(ref CostSearchTicket[] array, CostSearchTicket ticket)
		{
			if (array != null)
			{
				//Store original array
				CostSearchTicket[] origArray = (CostSearchTicket[])array.Clone();
				// Create new, bigger array
				CostSearchTicket[] newArray = new CostSearchTicket[origArray.Length+1];
				// Copy old values to new array
				origArray.CopyTo(newArray, 0);
				// Add in new value
				newArray[newArray.Length-1] = ticket;
				// Update internal array
				array = newArray;
			}
			else
			{
				array = new CostSearchTicket[1];
				array[0] = ticket;
			}
		}

		/// <summary>
		/// Function calculates the max and min values for adult
		/// and child fares from an array of CostSearchTickets
		/// </summary>
		/// <param name="tickets">CostSearchTickets array</param>
		/// <returns>MinMaxFares</returns>
		private MinMaxFares GetMinMaxFromArray(CostSearchTicket[] tickets)
		{
			//Intialise MinMaxFares struct
			MinMaxFares minMax = new MinMaxFares();
			minMax.minAdultFare = float.MaxValue;
			minMax.maxAdultFare = float.MinValue;
			minMax.minChildFare = float.MaxValue;
			minMax.maxChildFare = float.MinValue;
			minMax.lowestProbableAdultFare = float.MaxValue;
			minMax.lowestProbableChildFare = float.MaxValue;
		
			// Get ArrayList of arrays of combined tickets, based on CombinedTicketIndex property
			ArrayList combinedTicketArrays = GetCombinedTicketArrays(tickets);

			// For each array of combined tickets
			foreach (CostSearchTicket[] combinedTickets in combinedTicketArrays)
			{
				// Reset amount values
				float adultFare = 0;
				float childFare = 0;
				float adultDiscountedFare = 0;
				float childDiscountedFare = 0;	
				float minAdultTicketFare = 0;
				float minChildTicketFare = 0;
				
				// Calculate the 'total' combined fares
				foreach (CostSearchTicket ticket in combinedTickets)
				{
					if	(float.IsNaN(ticket.AdultFare))
					{
						adultFare = 0;
					}
					else
					{
						adultFare += ticket.AdultFare;
					}

					if	(float.IsNaN(ticket.ChildFare))
					{
						childFare = 0;
					}
					else
					{
						childFare += ticket.ChildFare;
					}
				
					if	(float.IsNaN(ticket.DiscountedAdultFare))
					{
						adultDiscountedFare = 0;
					}
					else
					{
						adultDiscountedFare += ticket.DiscountedAdultFare;
					}
					
					if	(float.IsNaN(ticket.DiscountedChildFare))
					{
						childDiscountedFare = 0;
					}
					else
					{
						childDiscountedFare += ticket.DiscountedChildFare;
					}
					
					//track the lowest probability for this combined ticket
					combinedTicketProbability = Probability.High;
					
					if (ticket.Probability < combinedTicketProbability)
					{
						combinedTicketProbability = ticket.Probability;
					}
				}						

				// Update MinMaxFares based on 'total' fare values.
				minMax.maxAdultFare = (adultFare != 0) ? Math.Max(adultFare, minMax.maxAdultFare) : minMax.maxAdultFare;
				minMax.minAdultFare = (adultFare != 0) ? Math.Min(adultFare, minMax.minAdultFare) : minMax.minAdultFare;
				minMax.maxAdultFare = (adultDiscountedFare != 0) ? Math.Max(adultDiscountedFare, minMax.maxAdultFare) : minMax.maxAdultFare;
				minMax.minAdultFare = (adultDiscountedFare != 0) ? Math.Min(adultDiscountedFare, minMax.minAdultFare) : minMax.minAdultFare;

				minMax.maxChildFare = (childFare != 0) ? Math.Max(childFare, minMax.maxChildFare) : minMax.maxChildFare;
				minMax.minChildFare = (childFare != 0) ? Math.Min(childFare, minMax.minChildFare) : minMax.minChildFare;
				minMax.maxChildFare = (childDiscountedFare != 0 ) ? Math.Max(childDiscountedFare, minMax.maxChildFare) : minMax.maxChildFare;
				minMax.minChildFare = (childDiscountedFare != 0 ) ? Math.Min(childDiscountedFare, minMax.minChildFare) : minMax.minChildFare;
				
				//update lowest probable fares for this TravelDate
				minAdultTicketFare = (adultDiscountedFare!= 0) ? Math.Min(adultFare, adultDiscountedFare) : adultFare;
				minChildTicketFare = (childDiscountedFare!= 0) ? Math.Min(childFare, childDiscountedFare) : childFare;
				minMax.lowestProbableAdultFare = CalculateLowestProbableFare(combinedTicketProbability, minAdultTicketFare, minMax.lowestProbableAdultFare);
				minMax.lowestProbableChildFare = CalculateLowestProbableFare(combinedTicketProbability, minChildTicketFare, minMax.lowestProbableChildFare);
			}

			// If no Adult/Child tickets, convert float.MaxValue and float.MinValue into nulls
			if (minMax.maxAdultFare == float.MinValue) minMax.maxAdultFare = float.NaN; 
			if (minMax.minAdultFare == float.MaxValue) minMax.minAdultFare = float.NaN; 
			if (minMax.maxChildFare == float.MinValue) minMax.maxChildFare = float.NaN; 
			if (minMax.minChildFare == float.MaxValue) minMax.minChildFare = float.NaN; 

			//if lowestProbable fares have not been updated (i.e. still set to float.MaxValue)
			//it means that all tickets have a Low probability. In this case set the lowest probability fares to the lowest fares
			if (minMax.lowestProbableAdultFare == float.MaxValue) minMax.lowestProbableAdultFare = minMax.minAdultFare; 
			if (minMax.lowestProbableChildFare == float.MaxValue) minMax.lowestProbableChildFare = minMax.minChildFare;

			return minMax;
		}

		/// <summary>
		/// Method loops through an array of CostSearchTickets and combines 
		/// tickets together where the CombinedTicketIndex is not 0.
		/// The combined ticket arrays are then added to an ArrayList.
		/// </summary>
		/// <param name="tickets">Array of CostSearchTickets</param>
		/// <returns>ArrayList of CostSearchTicket arrays</returns>
		private ArrayList GetCombinedTicketArrays(CostSearchTicket[] tickets)
		{
			ArrayList combinedTicketArrays = new ArrayList();
            Hashtable combinedTickets = new Hashtable();
			
			// For each ticket
			foreach (CostSearchTicket ticket in tickets)
			{
				if (ticket.CombinedTicketIndex == 0)
				{
					// Do not need to combine with another ticket so
					// create an array of size one and add to list.
					CostSearchTicket[] ticketArray = new CostSearchTicket[1] {ticket};
					combinedTicketArrays.Add(ticketArray);
				}
				else if (combinedTickets.ContainsKey(ticket.CombinedTicketIndex))
				{
					// CombinedTicketIndex already exists in hashtable, 
					// so update ArrayList for this key.
					ArrayList al = (ArrayList)combinedTickets[ticket.CombinedTicketIndex];
					al.Add(ticket);
					combinedTickets[ticket.CombinedTicketIndex] = al;
				}
				else
				{
					// Add new ArrayList of tickets with CombinedTicketIndex key to hashtable.
					ArrayList al = new ArrayList();
					al.Add(ticket);
					combinedTickets.Add(ticket.CombinedTicketIndex, al);
				}
			}	
		
			// For each 'group' of tickets.
			foreach (int ticketGroupKey in combinedTickets.Keys)
			{
				// Convert to an array of CostSearchTickets and add to ArrayList
				ArrayList al = (ArrayList)combinedTickets[ticketGroupKey];
				CostSearchTicket[] ticketArray = (CostSearchTicket[])al.ToArray(typeof(CostSearchTicket));
				combinedTicketArrays.Add(ticketArray);
			}
					
			return combinedTicketArrays;
		}

		/// <summary>
		/// Private helper method that helps works out the lowest probable fares
		/// </summary>
		/// <param name="ticketProbability">The probability for this ticket</param>
		/// <param name="minFare">The minimum fare for this ticket</param>
		/// <param name="currentMinProbFare">The current minimum probable fare for this travel date</param>
		/// <returns>an updated minimum probable fare</returns>
		private float CalculateLowestProbableFare(Probability ticketProbability, float minFare, float currentMinProbFare)
		{
					
			//if ANY combined ticket has a probability of medium or high
			//then the unlikelyToBeAvailable flag will be false
			if ((ticketProbability == Probability.Medium) || (ticketProbability == Probability.High))
			{
				unlikelyToBeAvailable = false;				
			}				
		
			//lowest fare with a probability of medium or high						
			if (((minFare < currentMinProbFare) || (float.IsNaN(currentMinProbFare)))
				&& ((ticketProbability == Probability.Medium) || (ticketProbability == Probability.High))
				&& (minFare > 0))
			{
				currentMinProbFare = minFare;				
			}		
			
		
			//return the result
			return currentMinProbFare;
		}

		
		/// <summary>
		/// Update the CombinedTicketIndex properties to their correct values,
		/// 
		/// Visibility is internal for NUnit purposes only.
		/// </summary>
		/// <param name="tickets">ArrayList of tickets applicable to this TravelDate</param>
		internal int CombineTickets(ArrayList tickets, int startIndex)
		{
			int newCombinedIndex = startIndex;

			if	(tickets != null)
			{
				ArrayList newTickets = new ArrayList();

				foreach (CostSearchTicket cst in tickets)
				{
					if	(cst.CombinedTicketIndex == 0)
					{
						newTickets.Add(cst);
					}
					else if	(cst.LegNumber == 0)
					{
						foreach (CostSearchTicket cst2 in tickets)
						{
							if	(cst2.CombinedTicketIndex == cst.CombinedTicketIndex && cst2.LegNumber > 0)
							{
								if	(TicketsAreCompatible(cst, cst2))
								{
									CostSearchTicket newCst	 = (CostSearchTicket)cst.Clone();
									CostSearchTicket newCst2 = (CostSearchTicket)cst2.Clone();
									
									newCst.CombinedTicketIndex  = newCombinedIndex;
									newCst2.CombinedTicketIndex = newCombinedIndex;

									newTickets.Add(newCst);
									newTickets.Add(newCst2);

									newCombinedIndex++;
								}
							}
						}
					}
				}

				tickets.Clear();
				tickets.AddRange(newTickets);
			}

			return newCombinedIndex;
		}

	
		private bool TicketsAreCompatible(CostSearchTicket cst1, CostSearchTicket cst2)
		{
			// only combine tickets if they both have adult fares or both have child ...

			if	(!Single.IsNaN(cst1.AdultFare) && !Single.IsNaN(cst2.AdultFare))
			{
				return true;
			}

			if	(!Single.IsNaN(cst1.ChildFare) && !Single.IsNaN(cst2.ChildFare))
			{
				return true;
			}

			return false;
		}


		#endregion

		#region Constructor
		/// <summary>
		/// default constructor
		/// </summary>
		public TravelDate()
		{

		}

		/// <summary>
		/// Overloaded constructor for single journeys with child fare
		/// </summary>
		/// <param name="index"></param>
		/// <param name="outwardDate"></param>
		/// <param name="mode"></param>
		/// <param name="lowestProbableChildFare"></param>
		/// <param name="lowestProbableAdultFare"></param>
		/// <param name="unlikelyToBeAvailable"></param>
		public TravelDate(int index, TDDateTime outwardDate, TicketTravelMode mode, float lowestProbableChildFare, float lowestProbableAdultFare, bool unlikelyToBeAvailable) 
		{
			this.travelDateIndex = index;		
			this.outwardDate = outwardDate;			
			this.travelMode = mode;
			this.lowestProbableChildFare = lowestProbableChildFare;
			this.lowestProbableAdultFare = lowestProbableAdultFare;
			this.unlikelyToBeAvailable = unlikelyToBeAvailable;
		}
		
		/// <summary>
		/// Overloaded constructor for return journeys
		/// </summary>
		/// <param name="index"></param>
		/// <param name="outwardDate"></param>
		/// <param name="returnDate"></param>
		/// <param name="mode"></param>
		/// <param name="lowestProbableChildFare"></param>
		/// <param name="lowestProbableAdultFare"></param>
		/// <param name="unlikelyToBeAvailable"></param>
		public TravelDate(int index, TDDateTime outwardDate, TDDateTime returnDate, TicketTravelMode mode, float lowestProbableChildFare, float lowestProbableAdultFare, bool unlikelyToBeAvailable) 
			: this(index, outwardDate, mode, lowestProbableChildFare, lowestProbableAdultFare, unlikelyToBeAvailable) 
		{
			this.returnDate = returnDate;	
		}

		#endregion

		#region Public properties and methods
		
		/// <summary>
		/// read/write property for travelDateIndex
		/// </summary>
		public int Index
		{
			get
			{
				return travelDateIndex;
			}
			set
			{
				travelDateIndex = value;
			}
		}

		/// <summary>
		/// read/write property for outwardDate
		/// </summary>
		public TDDateTime OutwardDate
		{
			get
			{
				return outwardDate;
			}
			set
			{
				outwardDate = value;
			}
		}
		
		/// <summary>
		/// read/write property for returnDate
		/// </summary>
		public TDDateTime ReturnDate
		{
			get
			{
				return returnDate;
			}
			set
			{
				returnDate = value;
			}
		}
		
		/// <summary>
		/// read/write property for travelMode
		/// </summary>
		public TicketTravelMode TravelMode
		{
			get
			{
				return travelMode;
			}
			set
			{
				travelMode = value;
			}
		}

		/// <summary>
		/// read/write property for lowestProbableFare
		/// </summary>
		public float LowestProbableAdultFare
		{
			get
			{
				return lowestProbableAdultFare;
			}
			set
			{
				lowestProbableAdultFare = value;
			}
		}

		/// <summary>
		/// read/write property for lowestProbableFare
		/// </summary>
		public float LowestProbableChildFare
		{
			get
			{
				return lowestProbableChildFare;
			}
			set
			{
				lowestProbableChildFare = value;
			}
		}

		/// <summary>
		/// read/write property for unlikelyToBeAvailable
		/// </summary>
		public bool UnlikelyToBeAvailable
		{
			get
			{
				return unlikelyToBeAvailable;
			}
			set
			{
				unlikelyToBeAvailable = value;
			}
		}

		/// <summary>
		/// Read-only property for minChildFare
		/// </summary>
		public float MinChildFare
		{
			get
			{
				return minChildFare;
			}
		}

		/// <summary>
		/// Read-only property for maxChildFare
		/// </summary>
		public float MaxChildFare
		{
			get
			{
				return maxChildFare;
			}
		}

		/// <summary>
		/// Read-only property for minAdultFare
		/// </summary>
		public float MinAdultFare
		{
			get
			{
				return minAdultFare;
			}
		}

		/// <summary>
		/// Read-only property for maxAdultFare
		/// </summary>
		public float MaxAdultFare
		{
			get
			{
				return maxAdultFare;
			}
		}

		/// <summary>
		/// read/write for errorForOutward
		/// </summary>
		public bool ErrorForOutward
		{
			get
			{
				return errorForOutward;
			}
			set
			{
				errorForOutward = value;
			}
		}

		/// <summary>
		/// read/write for errorForOutward
		/// </summary>
		public bool ErrorForInward
		{
			get
			{
				return errorForInward;
			}
			set
			{
				errorForInward = value;
			}
		}

		/// <summary>
		/// read/write property for ticketType
		/// </summary>
		public TicketType TicketType
		{
			get
			{
				return ticketType;
			}
			set
			{
				ticketType = value;
			}
		}

		/// <summary>
		/// read/write property for outwardTickets
		/// </summary>
		public CostSearchTicket[] OutwardTickets
		{
			get
			{
				return (outwardTickets != null ? outwardTickets : (new CostSearchTicket[0])); 
			}
			set
			{
				outwardTickets = value;
			}
		}

		/// <summary>
		/// Public method for adding a CostSearchTicket to the 
		/// OutwardTickets array.
		/// </summary>
		/// <param name="ticket"></param>
		public void AddOutwardTicket(CostSearchTicket ticket)
		{
			AddTicketToArray(ref outwardTickets, ticket);
		}

		/// <summary>
		/// read/write property for inwardTickets
		/// </summary>
		public CostSearchTicket[] InwardTickets
		{
			get
			{
				return (inwardTickets != null ? inwardTickets : (new CostSearchTicket[0])); 
			}
			set
			{
				inwardTickets = value;
			}
		}

		/// <summary>
		/// Public method for adding a CostSearchTicket to the 
		/// InwardTickets array.
		/// </summary>
		/// <param name="ticket"></param>
		public void AddInwardTicket(CostSearchTicket ticket)
		{
			AddTicketToArray(ref inwardTickets, ticket);
		}
	
		/// <summary>
		/// read/write property for returnTickets
		/// </summary>
		public CostSearchTicket[] ReturnTickets
		{
			get
			{
				return (returnTickets != null ? returnTickets : (new CostSearchTicket[0])); 
			}
			set
			{
				returnTickets = value;
			}
		}

		/// <summary>
		/// Public method for adding a CostSearchTicket to the 
		/// ReturnTickets array.
		/// </summary>
		/// <param name="ticket"></param>
		public void AddReturnTicket(CostSearchTicket ticket)
		{
			AddTicketToArray(ref returnTickets, ticket);
		}

		/// <summary>
		/// Read only Public bool property HasTickets
		/// Returns true if the TravelDate has ANY tickets
		/// </summary>
		public bool HasTickets 
		{
			get { return ((HasOutwardTickets) || (HasInwardTickets) || (HasReturnTickets));	}				
		}

		/// <summary>
		/// Read only Public bool property HasOutwardTickets
		/// Returns true if the TravelDate has any outward tickets
		/// </summary>
		public bool HasOutwardTickets 
		{
			get { return ((outwardTickets != null) && (outwardTickets.Length > 0)); }	
		}

		/// <summary>
		/// Read only Public bool property HasInwardTickets
		/// Returns true if the TravelDate has any inward tickets
		/// </summary>
		public bool HasInwardTickets 
		{
			get { return ((inwardTickets != null) && (inwardTickets.Length > 0)); }		
		}

		/// <summary>
		/// Read only Public bool property HasReturnTickets
		/// Returns true if the TravelDate has any return tickets
		/// </summary>
		public bool HasReturnTickets 
		{
			get { return ((returnTickets != null) && (returnTickets.Length > 0)); }		
		}

		/// <summary>
		/// Method udpates the internal maxAdultFare, minAdultFare
		/// maxChildFare, minChildFare variables based on the internal 
		/// ticket collections.
		/// </summary>
		public void UpdateMaxMinFares()
		{
			//initially set this to true			
			unlikelyToBeAvailable = true;	

			// Intialise values to nulls in case no tickets/fares
			MinMaxFares minMax = new MinMaxFares();
			minMax.maxAdultFare = float.NaN;
			minMax.minAdultFare = float.NaN;
			minMax.maxChildFare = float.NaN;
			minMax.minChildFare = float.NaN;						
			minMax.lowestProbableChildFare = float.NaN;
			minMax.lowestProbableAdultFare = float.NaN;					
			
			if	(travelMode == TicketTravelMode.Coach)
			{
				ArrayList ticketsToCombine = new ArrayList(outwardTickets);
				int lastIndex = CombineTickets(ticketsToCombine, 100);
				outwardTickets = (CostSearchTicket[])ticketsToCombine.ToArray(typeof(CostSearchTicket));

				if	(ticketType == TicketType.Return)
				{
					ticketsToCombine = new ArrayList(returnTickets);
					CombineTickets(ticketsToCombine, lastIndex + 1);
					returnTickets = (CostSearchTicket[])ticketsToCombine.ToArray(typeof(CostSearchTicket));
				}
			}

			switch (ticketType)
			{
				// If we are not dealing with separate out and return singles, 
				// we can simply find the highest and lowest of each variant...
				case TicketType.Single:
				case TicketType.OpenReturn:
					if (this.HasOutwardTickets)
						minMax = GetMinMaxFromArray(outwardTickets);
					break;

				case TicketType.Return:
					if (this.HasReturnTickets)
						minMax = GetMinMaxFromArray(returnTickets);
					break;
				
				// If we *do* have separate out and return singles, we need to add
				// together the respective maxima and minima from inward and outward ...

				case TicketType.Singles:
					if (this.HasOutwardTickets && this.HasInwardTickets)
					{
						MinMaxFares minMaxOutward = GetMinMaxFromArray(outwardTickets);
						MinMaxFares minMaxInward  = GetMinMaxFromArray(inwardTickets);

						minMax.maxAdultFare = minMaxInward.maxAdultFare + minMaxOutward.maxAdultFare;
						minMax.minAdultFare = minMaxInward.minAdultFare + minMaxOutward.minAdultFare;
						minMax.maxChildFare = minMaxInward.maxChildFare + minMaxOutward.maxChildFare;
						minMax.minChildFare = minMaxInward.minChildFare + minMaxOutward.minChildFare;
						minMax.lowestProbableAdultFare = minMaxInward.lowestProbableAdultFare + minMaxOutward.lowestProbableAdultFare;
						minMax.lowestProbableChildFare = minMaxInward.lowestProbableChildFare + minMaxOutward.lowestProbableChildFare;
					}
					// partial results where error has occurred on inward or outward fare calcluations ...
					else if	(this.HasOutwardTickets)
					{
						minMax = GetMinMaxFromArray(outwardTickets);
					}
					else if	(this.HasInwardTickets)
					{
						minMax = GetMinMaxFromArray(inwardTickets);
					}
					break;
			}

			// Update the internal variables with values from minMax struct
			this.maxAdultFare = minMax.maxAdultFare;
			this.minAdultFare = minMax.minAdultFare;
			this.maxChildFare = minMax.maxChildFare;
			this.minChildFare = minMax.minChildFare;			
			this.LowestProbableAdultFare = minMax.lowestProbableAdultFare;			
			this.LowestProbableChildFare = minMax.lowestProbableChildFare;
		

			if (TDTraceSwitch.TraceVerbose) 
			{
				if	(travelMode == TicketTravelMode.Coach)
				{
					StringBuilder sb = new StringBuilder(500);

					sb.Append("After combining tickets, there are ");
					sb.Append(outwardTickets.Length);
					sb.Append(" remaining");
					sb.Append(nl);
					
					foreach (CostSearchTicket cst in outwardTickets)
					{
						sb.Append(cst.Code);
						sb.Append(" for operator ");
						sb.Append(cst.TicketCoachFareData.OperatorCode);
						sb.Append(" index = ");
						sb.Append(cst.CombinedTicketIndex);
						sb.Append(" leg = ");
						sb.Append(cst.LegNumber);
						sb.Append(" from ");
						sb.Append(cst.TicketCoachFareData.OriginNaptan);
						sb.Append(" to ");
						sb.Append(cst.TicketCoachFareData.DestinationNaptan);
						sb.Append(nl);

						sb.Append(cst.Flexibility.ToString());
						sb.Append(", fares (A/C, DA/DC):");
						sb.Append(cst.AdultFare);
						sb.Append(", ");
						sb.Append(cst.ChildFare);
						sb.Append(", ");
						sb.Append(cst.DiscountedAdultFare);
						sb.Append(", ");
						sb.Append(cst.DiscountedChildFare);
						sb.Append(nl);
					}
					
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, sb.ToString()));
				}
			}
		}

		

		#endregion
	}

	/// <summary>
	/// Struct to hold six float values representing
	/// max/min fares. Used in UpdateMaxMinFares.
	/// </summary>
	[Serializable]
	struct MinMaxFares
	{
		public float minAdultFare;
		public float maxAdultFare;
		public float minChildFare;
		public float maxChildFare;
		public float lowestProbableChildFare;
		public float lowestProbableAdultFare;		
	}
}
