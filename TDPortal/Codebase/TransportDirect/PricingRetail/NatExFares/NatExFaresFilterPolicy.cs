using System;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
{
	/// <summary>
	/// Policy class containing the business logic to determine whether we can associated a Ticket with a particular Coach PricingUnit
	/// </summary>
	public class NatExFaresFilterPolicy
	{
		/// <summary>
		/// Private constructor to prevent instantiation
		/// </summary>
		private NatExFaresFilterPolicy()
		{

		}

		/// <summary>
		/// Determines if a single ticket can be associated with a particular PricingUnit based on its outward and return legs
		/// </summary>
		/// <param name="ticket"></param>
		/// <param name="pricingUnit"></param>
		/// <returns></returns>
		public static bool IsValidSingle(Ticket ticket, PricingUnit pricingUnit)
		{
			// Regardless of whether we have return information or not, a single fare can be associated with a PricingUnit
			return true;
		}

		/// <summary>
		/// Determines if a return ticket can be associated with a particular PricingUnit based on its outward and return legs
		/// </summary>
		/// <param name="ticket"></param>
		/// <param name="pricingUnit"></param>
		/// <returns></returns>
		public static bool IsValidReturn(Ticket ticket, PricingUnit pricingUnit)
		{
			// If a PricingUnit as both outbound and inbound legs defined then we can associated fares of all flexibility with it.
			if (pricingUnit.InboundLegs.Count != 0) 
			{
				return true;
			}
			// If a PricingUnit only has outbound legs, then because we don't have any information about the return journey we 
			// are only able to offer fully flexible return fares
			else if (ticket.Flexibility == Flexibility.FullyFlexible || ticket.Flexibility == Flexibility.LimitedFlexibility)
			{
				return true;
			}
			else 
			{
				return false;
			}
		}
	}
}
