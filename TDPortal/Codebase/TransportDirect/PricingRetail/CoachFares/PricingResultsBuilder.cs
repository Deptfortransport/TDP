//********************************************************************************
//NAME         : PricingResultsBuilder.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 21/10/2003
//DESCRIPTION  : Implementation of PricingResultsBuilder class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFares/PricingResultsBuilder.cs-arc  $
//
//   Rev 1.1   May 14 2009 17:10:56   jfrank
//CCN0503 to append route 60 to the start of national express Route 60 fares.
//Resolution for 5288: National Express Route 60
//
//   Rev 1.0   Nov 08 2007 12:36:36   mturner
//Initial revision.
//
//   Rev 1.11   Jun 13 2007 15:18:22   mmodi
//Correct farecodesplit check to prevent error when SCL fare provided
//Resolution for 4450: NX Fares: Scottish citylink fares are not shown
//
//   Rev 1.10   May 25 2007 16:22:12   build
//Automatically merged from branch for stream4401
//
//   Rev 1.9.1.3   May 11 2007 14:30:50   mmodi
//Correct problem of new fares not being created, and tidied up if else logic
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.9.1.2   May 11 2007 13:38:56   asinclair
//Pass in Fare code outside of if statement for existing fares
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.9.1.1   May 11 2007 10:58:52   asinclair
//Now passing in the Fare code (as short code) when creating a ticket
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.9.1.0   May 10 2007 16:29:44   asinclair
//Added isNewFares bool and code.
//
//   Rev 1.9   Mar 06 2007 13:43:44   build
//Automatically merged from branch for stream4358
//
//   Rev 1.8.1.0   Mar 02 2007 11:05:14   asinclair
//Pass an extra parameter into the new Pricing Request
//Resolution for 4358: Del 9.x Stream: Improved Rail Fares CCN0354
//
//   Rev 1.8   Jan 18 2006 18:16:30   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.7   Dec 06 2005 11:25:24   mguney
//Exceptional fares handling changed.
//Resolution for 3311: IF098 Interface Stub: Journey Restrictions
//
//   Rev 1.6   Nov 29 2005 20:25:22   mguney
//Changed for Exceptional Fares.
//Resolution for 3230: DN040: Route60 and DayReturn fares should be handled in a different manner.
//
//   Rev 1.5   Nov 26 2005 11:54:30   mguney
//ChangeNaPTANs null check included in the CreateCoachFareData method.
//Resolution for 3213: IF098 Interface Stub: Issues with displaying fares from IF098 stub
//
//   Rev 1.4   Nov 07 2005 20:36:04   RPhilpott
//Populate CoachFareData with empty arrays if no restriction data.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Nov 03 2005 19:24:12   RPhilpott
//Add CoachFareData creation.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Nov 01 2005 17:20:38   mguney
//Constant names changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 21 2005 10:28:46   mguney
//Associated IR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 21 2005 10:27:10   mguney
//Initial revision.


//.. Rev 1.8   Apr 28 2005 18:05:30   RPhilpott
//Split noPlacesAvaialble flag into singles and returns.
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.
//
//   Rev 1.7   Apr 28 2005 16:03:54   RPhilpott
//Add "NoPlacesAvailable" property to PricingResult to indicate that valid fares have been found, but with no seat availability.
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.
//
//   Rev 1.6   Mar 23 2005 09:39:28   jbroome
//Extracted functionality into new PricingResultsHelper class
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.5   Jan 14 2005 11:16:44   jmorrissey
//Fixed warnings being generated in ConvertFlexibility method 
//
//   Rev 1.4   Jun 11 2004 15:35:40   acaunt
//Now uses TicketNameRule to generate appropriate SCL or NatEx ticket name.
//
//   Rev 1.3   May 28 2004 13:53:22   acaunt
//Updated to accomodate real NatEx and SCL data and with revised display rules.
//
//   Rev 1.2   Nov 24 2003 16:27:42   acaunt
//Coach fares are ordered before being stored
//
//   Rev 1.1   Oct 22 2003 23:08:58   acaunt
//unit testing bug fixes
//
//   Rev 1.0   Oct 22 2003 10:15:32   acaunt
//Initial Revision
using System;
using System.Collections;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// Class to encapsulate the creation of the set of PricingResults
	/// corresponding to pricing information provided by the fare provider
	/// To use this object:
	/// 1. Add all the undiscounted fares
	/// 2. Add all the discount cards we need to support
	/// 3. Add all the discount fare information
	/// 
	/// If discount fare information is not provided for a particular discount card then the fares associated
	/// with that card are taken to be the same as the undiscounted fares
	/// 
	/// The builder returns a Hashtable of PricingResults, keyed off discount card
	/// </summary>
	public class PricingResultsBuilder
	{
        /// <summary>
		/// Implementation of IComparer to order coach tickets
		/// </summary>
		private class CoachTicketOrderingComparer  : IComparer
		{
			/// <summary>
			/// Implementation of IComparer.Compare. Two tickets are compared based on there value of their fares and then by ticket name
			/// </summary>
			/// <param name="x"></param>
			/// <param name="y"></param>
			/// <returns></returns>
			public int Compare(Object x, Object y)
			{
				int comparison = 0; // the value we will return to indicate the comparison
				// Make sure we can cast both objects as Tickets
				Ticket ticketX = x as Ticket;
				Ticket ticketY = y as Ticket;
				// If either of the objects isn't a Ticket then throw an ArgumentException
				if (ticketX == null || ticketY == null) 
				{
					throw new ArgumentException("Unable to compare objects "+x+" and "+y+"as Tickets");
				}
				float fareX = GetFare(ticketX);
				float fareY = GetFare(ticketY);

				// Compare the fares
				comparison = (fareX < fareY) ? -1 : ((fareX > fareY) ? 1 : 0);
				// if they are still the same then compare the ticket names
				if (comparison == 0)
				{
					comparison = ticketX.Code.CompareTo(ticketY.Code);
				}
				return comparison;
			}

			/// <summary>
			/// Find the appropriate fare field
			/// </summary>
			/// <param name="ticket"></param>
			/// <returns></returns>
			private float GetFare(Ticket ticket)
			{
				if (!ticket.AdultFare.Equals(float.NaN))
				{
					return ticket.AdultFare;
				} 
				else if (!ticket.ChildFare.Equals(float.NaN))
				{
					return ticket.ChildFare;
				}
				else if (!ticket.DiscountedAdultFare.Equals(float.NaN))
				{
					return ticket.DiscountedAdultFare;
				}
				else 
				{
					return ticket.DiscountedChildFare;
				}
			}
		}

		// Key for indicating no associated discount card
		private static string NODISCOUNT = string.Empty;

		//flags for filtering
		private bool isDayReturn;
		private bool useExceptionalFaresLookup;

		PricingResultsHelper helper = new PricingResultsHelper();

		// Formatted string containing the min and max ages of child fares
		private string childAgeRange = string.Empty;
		// The extracted values
		private int minChildAge;
		private int maxChildAge;
		

		// The list of undiscounted fares. This is used to generate a template PricingResult
		private ArrayList undiscountedFares = new ArrayList();
		// Track whether the user should be adding discounted or undiscounted fares
		private bool undiscountedFaresComplete;
		// The PricingResults object for undiscounted Fares. This is used as a template for all other PricingResults
		private PricingResult templatePricingResult;
		// Our set of PricingResults organised by discount card
		private Hashtable pricingResults = new Hashtable();

        private string strRoute60 = "";
        private string strRoute60Append = "";

		/// <summary>
		/// Create a PricingResultsBuilder with the appropriate nameRule (NatEx or SCL)
		/// </summary>
		/// <param name="nameRule"></param>
		public PricingResultsBuilder(bool isDayReturn, bool useExceptionalFaresLookup)
		{			
			this.minChildAge = CoachFare.ChildAgeDefaultMin;
			this.maxChildAge = CoachFare.ChildAgeDefaultMax;

			this.isDayReturn = isDayReturn;
			this.useExceptionalFaresLookup = useExceptionalFaresLookup;

            strRoute60 = Properties.Current["CoachFares.NationalExpress.Route60.Detection"];
            strRoute60Append = Properties.Current["CoachFares.NationalExpress.Route60.AppendString"];
		}

		/// <summary>
		/// Create a new undiscounted fare
		/// </summary>
		/// <param name="fare"></param>
		/// <param name="operatorCode"></param>
		public void AddUndiscountedFare(CoachFare fare, string operatorCode, bool isNewFares)
		{
			if (undiscountedFaresComplete)
			{
				throw new InvalidOperationException("Unable to add an undiscounted fare after adding a discount card");
			}

			Ticket ticket = CreateTicket(fare, operatorCode, isNewFares);
			if (ticket != null)
				undiscountedFares.Add(ticket);

			this.minChildAge = fare.ChildAgeMin;
			this.maxChildAge = fare.ChildAgeMax;			
		}

		/// <summary>
		/// Add a new discount card to the set of supported discount cards
		/// </summary>
		/// <param name="card"></param>
		public void AddDiscountCard(string card)
		{
			if (!pricingResults.ContainsKey(card))
			{
				if (templatePricingResult == null)
				{
					CreateTemplatePricingResult();
				}
				
				pricingResults.Add(card, templatePricingResult.Clone());
			}
			
		}

		/// <summary>
		///  Create a new discounted fare associated with the matching discount card
		/// </summary>
		/// <param name="fare"></param>
		public void AddDiscountedFare(CoachFare fare, string operatorCode, bool isNewFares)
		{
			if (!undiscountedFaresComplete)
			{
				throw new InvalidOperationException("Unable to add a discounted fare until discount cards have been added");
			}
			// See if we have a PricingResult with a matching discount card and if so add a new Ticket to it
			if (pricingResults.ContainsKey(fare.DiscountCardType))
			{				
				Ticket ticket = CreateTicket(fare, operatorCode, isNewFares);
				if (ticket != null)
					((PricingResult)pricingResults[fare.DiscountCardType]).Tickets.Add(ticket);
			}
		}

		/// <summary>
		/// Return the set of generated PricingResults with the tickets of each PricingResult appropriate ordered
		/// </summary>
		/// <returns></returns>
		public Hashtable GetPricingResults()
		{
			// If we haven't got any results yet, then it is because no discount card has been added so the PricingResult
			// for undiscounted fares hasn't been created. So we create it.
			if (templatePricingResult == null)
			{
				CreateTemplatePricingResult();
			}
			// Sort the sets of tickets in each of the PricingResults
			IComparer comparer = new CoachTicketOrderingComparer();
			foreach (PricingResult pricingResult in pricingResults.Values)
			{
				pricingResult.Sort(comparer);
			}
			return pricingResults;
		}

		/// <summary>
		/// Once a discount card is added, assume that we now have all the undiscounted fares and create a PricingResult
		/// of undiscounted fares. This then forms a template for all the other PricingResults.
		/// </summary>
		private void CreateTemplatePricingResult()
		{
			// Once we create the template we don't want to accept any more undiscounted fares
			undiscountedFaresComplete = true;
			
			templatePricingResult = new PricingResult(minChildAge, maxChildAge, false, false, false);
			templatePricingResult.Tickets = undiscountedFares;
			// Add the template to the set of PricingResults
			pricingResults.Add(NODISCOUNT, templatePricingResult);

		}

		/// <summary>
		/// Creates the outline of a Ticket based on a CoachFare
		/// </summary>
		/// <param name="fare"></param>
		/// <returns></returns>
		private Ticket CreateTicket(CoachFare fare, string operatorCode, bool isNewFares)
		{
			// Set fare name and code values for when creating the ticket
			// this is because Coach fares are returned as e.g. "Flexible (FLX)"
			string[] fareCodeSplit = fare.FareType.Split( '(' );
			
			string fareName = fareCodeSplit[0].Trim();
			string fareCode = string.Empty;

            try
            {
                if (fare.PassengerType.ToString().ToLower() == strRoute60.ToLower())
                {
                    fareName = strRoute60Append + fareName;
                }
            }
            catch
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
                        TDTraceLevel.Verbose, "Error in PricingRetail.CoachFares.PricingResultsBuilder.CreateTicket() failed to process route 60 fares"));

            }

			// Check length because SCL do not include the fare code in the name
			if (fareCodeSplit.Length > 1)
                fareCode = fareCodeSplit[1].TrimEnd( ')' );

			// Using CoachFaresLookup (for the new NX fares)
			if (isNewFares)
			{
				ICoachFaresLookup coachFaresLookup = (ICoachFaresLookup)
					TDServiceDiscovery.Current[ServiceDiscoveryKey.CoachFaresLookup];
				CoachFaresAction coachFaresInclude = coachFaresLookup.GetCoachFaresAction(fareCode);
				if (coachFaresInclude != CoachFaresAction.Include)
				{
					return null;
				}
			}
			else if (useExceptionalFaresLookup)
				//Use Lookup for exceptional fares
			{
				//get the ExceptionalFaresLookup
				IExceptionalFaresLookup exceptionalFaresLookup = (IExceptionalFaresLookup)
					TDServiceDiscovery.Current[ServiceDiscoveryKey.ExceptionalFaresLookup];
				ExceptionalFaresAction exceptionalFaresAction = 
					exceptionalFaresLookup.GetExceptionalFaresAction(fare.FareType);
				//If 
				//	*DayReturn action
				//	*AND NOT (ReturnDate == OutwardDate)
				//OR
				//	*Exclude action
				//then don't return any tickets.				
				if (((exceptionalFaresAction == ExceptionalFaresAction.DayReturn) && (!isDayReturn))
					||
					(exceptionalFaresAction == ExceptionalFaresAction.Exclude))
				{
					return null;
				}
			}
			else
				//use IF098 CoachFareForRoute properties
			{
				if (((CoachFareForRoute)fare).IsDayReturn && (!isDayReturn))
					return null;
				if (((CoachFareForRoute)fare).IsConcessionaryFare)
					return null;
			}

			Ticket ticket = new Ticket(fareName, fare.Flexibility, fareCode);						
			
			bool discounted = ((fare.DiscountCardType != null) && (fare.DiscountCardType != NODISCOUNT));
			helper.SetFare(ticket, fare.IsAdult, discounted, fare.Fare);			

			ticket.TicketCoachFareData = CreateCoachFareData(fare, operatorCode);

			return ticket;
		}

		/// <summary>
		/// Creates a CoachFareData object based on a CoachFare
		/// </summary>
		/// <param name="fare"></param>
		/// <returns>A new CoachFareData object</returns>
		private CoachFareData CreateCoachFareData(CoachFare fare, string operatorCode)
		{
			CoachFareData cfd = new CoachFareData();

			cfd.OriginNaptan = fare.OriginNaPTAN.Naptan;
			cfd.DestinationNaptan = fare.DestinationNaPTAN.Naptan;
			cfd.IsQuotaFare = false;
			cfd.OperatorCode = operatorCode;
			cfd.Probability = fare.Availability;
            cfd.PassengerType = fare.PassengerType;

			if	(fare is CoachFareForRoute)
			{
				cfd.RestrictedOperatorCodes = ((CoachFareForRoute)fare).RestrictedOperatorCodes;
				cfd.RestrictedServices = ((CoachFareForRoute)fare).RestrictedServices;
				cfd.TimeRestrictions = ((CoachFareForRoute)fare).TimeRestrictions;

				if (((CoachFareForRoute)fare).ChangeNaPTANs != null)
				{
					cfd.ChangeNaptans = new string[((CoachFareForRoute)fare).ChangeNaPTANs.Length];

					for (int i = 0; i < ((CoachFareForRoute)fare).ChangeNaPTANs.Length; i++)
					{
						cfd.ChangeNaptans[i] = ((CoachFareForRoute)fare).ChangeNaPTANs[i].Naptan;
					}
				}
			}
			else
			{
				cfd.RestrictedOperatorCodes = new string[0];
				cfd.RestrictedServices		= new string[0];
				cfd.TimeRestrictions		= new TimeRestriction[0];
				cfd.ChangeNaptans			= new string[0];
			}

			return cfd;
		}
	}
}
