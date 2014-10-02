//********************************************************************************
//NAME         : CoachFareMergePolicy.cs
//AUTHOR       : James Broome
//DATE CREATED : 22/02/2005
//DESCRIPTION  : Implementation of CoachFareMergePolicy class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/NatExFares/CoachFareMergePolicy.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:04   mturner
//Initial revision.
//
//   Rev 1.4   Aug 19 2005 14:05:46   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.3.1.0   Aug 16 2005 11:20:00   RPhilpott
//Get rid of warnings from deprecated methods.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.3   Apr 16 2005 16:00:48   jbroome
//Updated IsInwardJourneyValid() method.
//
//   Rev 1.2   Apr 08 2005 16:31:24   jbroome
//Added IsInwardJourneyValid() method
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.1   Mar 30 2005 16:02:50   jbroome
//Added AreTicketsMatching() method
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.0   Mar 23 2005 09:30:20   jbroome
//Initial revision.
//Resolution for 1405: Adjusting Journey causes unexpected results (DEL5.4)

using System;
using System.Collections;

using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;

using CJP = TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
{
	/// <summary>
	/// Class provides helper methods for the NatExFaresSupplier to compare
	/// coach fares returned from the CJP during the cost based search process.
	/// </summary>
	public sealed class CoachFareMergePolicy
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		private CoachFareMergePolicy()
		{
		}

		/// <summary>
		/// Determines whether two CoachJourneyFare objects 'match'.
		/// If so, they can be added to the same CoachJourneyFareSummary.
		/// CoachJourneyFares with multiple PricingUnitFares only match if
		/// every PricingUnitFare in the first CoachJourneyFare can be matched
		/// with a PricingUnitFare from the second CoachJourneyFare.
		/// </summary>
		/// <param name="fare1">CoachJourneyFare</param>
		/// <param name="fare2">CoachJourneyFare</param>
		/// <returns>True if fares are matching</returns>
		static public bool AreFaresMatching(CoachJourneyFare fare1, CoachJourneyFare fare2)
		{
			bool result = true;
			// For every CoachPricingUnitFare in first CoachJourneyFare, search for
			// a matching one in the second CoachJourneyFare
			foreach (CoachPricingUnitFare puFare1 in fare1.PricingUnitFares)
			{
				bool matchingFare = false;
				foreach (CoachPricingUnitFare puFare2 in fare2.PricingUnitFares)
				{
					if (( puFare1.StartLocationNaptan == puFare2.StartLocationNaptan) 
						&& (puFare1.EndLocationNaptan == puFare2.EndLocationNaptan)
						&& (puFare1.PricingUnitIndex == puFare2.PricingUnitIndex)
						&& (puFare1.FareType == puFare2.FareType))
					{
						// If got this far then Start and End locations match, fares 
						// are from same Pricing Unit and Fare Types match 
						// If both IsAdult or both IsChild and have same discount, then amounts must match
						if ((fare1.IsAdult == fare2.IsAdult) && (puFare1.DiscountCardType == puFare2.DiscountCardType))
						{
							matchingFare = (puFare1.FareAmount == puFare2.FareAmount);
						}
						else
						{
							matchingFare = true;
						}
						// Found a matching fare, so move on to next 
						// CoachPricingUnitFare in first CoachJourneyFare
						if (matchingFare) break;
					}
				}
				if (!matchingFare)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		/// <summary>
		/// Determines whether two Ticket objects 'match'.
		/// A coach Ticket will only ever have one fare value 
		/// populated: Adult, Child, DiscountedAdult, DiscountedChild.
		/// If two tickets are deemed to match, then the two fare values
		/// can be combined onto a single Ticket object. E.g. Standard Single 
		/// with Adult, Child and DiscountedAdult fares values.
		/// </summary>
		/// <param name="ticket1">Ticket</param>
		/// <param name="ticket2">Ticket</param>
		/// <returns>true if tickets match</returns>
		static public bool AreTicketsMatching(Ticket ticket1, Ticket ticket2)
		{
			bool result = false;
			// Ticket codes must match e.g. 'Standard Single'
			if (ticket1.Code == ticket2.Code)
			{
				// Set IsAdult and IsDiscounted flags
				bool ticket1IsDiscounted = ((!ticket1.DiscountedAdultFare.Equals(float.NaN)) || (!ticket1.DiscountedChildFare.Equals(float.NaN)));
				bool ticket2IsDiscounted = ((!ticket2.DiscountedAdultFare.Equals(float.NaN)) || (!ticket2.DiscountedChildFare.Equals(float.NaN)));
				bool ticket1IsAdult = ((!ticket1.AdultFare.Equals(float.NaN)) || (!ticket1.DiscountedAdultFare.Equals(float.NaN)));
				bool ticket2IsAdult = ((!ticket2.AdultFare.Equals(float.NaN)) || (!ticket2.DiscountedAdultFare.Equals(float.NaN)));
				
				// If dealing with same type of ticket, fares only match if fares are the same.
                if ((ticket1IsAdult == ticket2IsAdult) && (ticket1IsDiscounted == ticket2IsDiscounted))
				{
					if (ticket1IsDiscounted)
					{
						if (ticket1IsAdult)
						{
							result = (ticket1.DiscountedAdultFare == ticket2.DiscountedAdultFare);
						}
						else
						{
							result = (ticket1.DiscountedChildFare == ticket2.DiscountedChildFare);
						}
					}
					else
					{
						if (ticket1IsAdult)
						{
							result = (ticket1.AdultFare == ticket2.AdultFare);
						}
						else
						{
							result = (ticket1.ChildFare == ticket2.ChildFare);
						}
					}
				}
				// Dealing with different types of ticket e.g. Adult and Child, so can combine
				else
				{
					result = true;
				}
			}
			return result;
		}

		/// <summary>
		/// Determines whether two fares from different PricingUnits (operators)
		/// are similar enough to combine to form a total fare for the whole journey
		/// </summary>
		/// <param name="fare1">CJP.Fare</param>
		/// <param name="fare2">CJP.Fare</param>
		/// <returns>True if fares can be combined</returns>
		static public bool CanFaresBeCombined(CJP.Fare fare1, CJP.Fare fare2)
		{
			if ((fare1.single == fare2.single) && (fare1.adult == fare2.adult) && (fare1.fareRestrictionType == fare2.fareRestrictionType))
			{
				// Can only match a discounted fare with an undiscounted fare from 
				// another pricing unit as discount cards are different between operators
				return ((fare2.discountCardType == null)||(fare2.discountCardType.Length==0));
			}
			else
			{
				// Fares cannot be combined as necessary properties do not match
				return false;
			}
		}

		/// <summary>
		/// Method determines if an inward PublicJourney is a valid journey for 
		/// the specified outward PublicJourney 
		/// As all return fares from CJP are 'open returns' and we have no info about
		/// the restrictions over the return journey for a ticket, we can only assume
		/// that a return journey is valid if the start and end locations match and
		/// the fare is from the same operator.
		/// </summary>
		/// <returns></returns>
		static public bool IsInwardJourneyValid(PublicJourney outwardJourney, PublicJourney inwardJourney)
		{
			bool result = false;

			// Retreive operator code lists from DataServices.
			IList natExCodes = null;
			IList sCLCodes = null;
			lock(typeof(CoachFareMergePolicy))
			{
				DataServices.IDataServices ds = (DataServices.IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
				natExCodes = ds.GetList(DataServiceType.NatExCodes);
				sCLCodes = ds.GetList(DataServiceType.SCLCodes);
			}
	
			int outwardPricingUnits = outwardJourney.Fares.Length;
			int inwardPricingUnits = inwardJourney.Fares.Length;

			// If not dealing with same number of pricing units, then journey invalid
			if (outwardPricingUnits == inwardPricingUnits)
			{
				// For each pricing unit in the outward PublicJourney
				for (int i=0; i<outwardPricingUnits; i++)
				{
					// Get the legs to which it applies
					int firstOutwardLeg = outwardJourney.Fares[i].legs[0];
					int lastOutwardLeg = outwardJourney.Fares[i].legs[outwardJourney.Fares[i].legs.Length-1];
					
					// Get the corresponding pricing unit from the inward journey
					int inwardPUIndex = inwardJourney.Fares.Length - 1 - i;
					// Get legs to which it applies
					int firstInwardLeg = inwardJourney.Fares[inwardPUIndex].legs[0];
					int lastInwardLeg = inwardJourney.Fares[inwardPUIndex].legs[inwardJourney.Fares[inwardPUIndex].legs.Length-1];
					
					// Get the start and end locations for the leg of the outward PublicJourney
					string outwardLegOrigin = outwardJourney.Details[firstOutwardLeg].LegStart.Location.Description;
					string outwardLegDestination = outwardJourney.Details[lastOutwardLeg].LegEnd.Location.Description;
					// Get the start and end locations for the corresponding leg of the inward PublicJourney
					string inwardLegOrigin = inwardJourney.Details[firstInwardLeg].LegStart.Location.Description;
					string inwardLegDestination = inwardJourney.Details[lastInwardLeg].LegEnd.Location.Description;

					// Inward PublicJourney must have opposite origin and 
					// destination locations for the corresponding outward legs.
					if ((outwardLegOrigin == inwardLegDestination) && (outwardLegDestination == inwardLegOrigin))
					{
						// If locations match, so must the operators for the pricing units
						string outwardLegOperator = outwardJourney.Details[firstOutwardLeg].Services[0].OperatorCode;
						string inwardLegOperator = inwardJourney.Details[firstInwardLeg].Services[0].OperatorCode;

						// Are they both Nat Ex?
						if (natExCodes.Contains(outwardLegOperator) && natExCodes.Contains(inwardLegOperator))
						{
							result = true;
						}
						// Or are they both SCL?
						else if (sCLCodes.Contains(outwardLegOperator) && sCLCodes.Contains(inwardLegOperator)) 
						{
							result = true;	
						}
						// Or are they both the same?
						else if (outwardLegOperator == inwardLegOperator) 
						{
							result = true;
						}
					}

					// All Pricing Units need to pass, so it one fails the whole journey is invalid.
					if (result == false) break;
				}
			}

			return result;
		}
	}
}
