//********************************************************************************
//NAME         : PricingResponseMessage.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 06/11/2003
//DESCRIPTION  : Implementation of PricingResponseMessage class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Logging/PricingResponseMessage.cs-arc  $
//
//   Rev 1.3   Jun 03 2010 09:10:42   mmodi
//Updated message to display "actual" nlc location
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.2   Mar 10 2009 15:52:04   mmodi
//Add null check before displaying route code
//
//   Rev 1.1   Feb 18 2009 18:16:20   mmodi
//Output route code
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:37:02   mturner
//Initial revision.
//
//   Rev 1.5   Apr 26 2006 12:15:00   RPhilpott
//Manual merge of stream 35
//
//   Rev 1.4.1.0   Apr 05 2006 17:10:20   RPhilpott
//Add fare location names.
//Resolution for 35: DEL 8.1 Workstream - Find Cheaper (Rail)
//
//   Rev 1.4   Jan 18 2006 18:16:36   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.3   Aug 19 2005 14:05:26   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.2.1.0   Aug 16 2005 11:19:28   RPhilpott
//Get rid of warnings from deprecated methods.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.2   Apr 12 2005 09:43:14   RPhilpott
//Make output more Notepad-friendly.
//
//   Rev 1.1   Mar 30 2005 16:54:48   RPhilpott
//Add upgrade information to time-based tickets.
//
//   Rev 1.0   Nov 07 2003 09:48:04   acaunt
//Initial Revision

using System;
using System.Text;
using System.Collections;

using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.PricingRetail.Logging
{
	/// <summary>
	/// Obtain a printable summary of a pricing response
	/// </summary>
	public sealed class PricingResponseMessage
	{
		static readonly string nl = Environment.NewLine;

		private PricingResponseMessage()
		{
		}

		public static string Message(PricingUnit pricingUnit)
		{
			int i=0;
			StringBuilder builder = new StringBuilder();	
			builder.Append("Pricing response information" + nl);
			builder.Append("\tPricing request information" + nl);
			builder.Append("\tMode: "+pricingUnit.Mode+ nl);
			builder.Append("\tOperator code: "+pricingUnit.OperatorCode+ nl);
			builder.Append("\tMatching return journey?" +pricingUnit.MatchingReturn+ nl);
			builder.Append("\tIncludes underground?"+pricingUnit.IncludesUnderground+ nl);
			if (pricingUnit.OutboundLegs.Count != 0) 
			{
				builder.Append("\tOutbound summary:" + nl);
				JourneySummary(builder, pricingUnit.OutboundLegs);
			}
			if (pricingUnit.InboundLegs.Count != 0)
			{
				builder.Append("\tInbound summary:" + nl);
				JourneySummary(builder, pricingUnit.InboundLegs);
			}
			builder.Append("Single tickets:" + nl);
			i=0;
			foreach (Ticket ticket in pricingUnit.SingleFares.Tickets) 
			{

				TicketMessage(builder, ticket, ++i);
			}
			builder.Append("Return tickets:" + nl);
			i=0;
			foreach (Ticket ticket in pricingUnit.ReturnFares.Tickets) 
			{

				TicketMessage(builder, ticket, ++i);
			}
			builder.Append("End of pricing response");
			return builder.ToString();
		}

		private static void JourneySummary(StringBuilder builder, IList journey)
		{
			PublicJourneyDetail start = (PublicJourneyDetail)journey[0];
			PublicJourneyDetail end = (PublicJourneyDetail)journey[journey.Count-1];
			builder.Append("\tFrom "+start.LegStart.Location.NaPTANs[0].Naptan+" ("+start.LegStart.Location.Description+") ");
			builder.Append("\tto "+end.LegEnd.Location.NaPTANs[0].Naptan+" ("+end.LegEnd.Location.Description + ")" + nl);

		}

		private static void TicketMessage(StringBuilder builder, Ticket ticket, int index)
		{
			builder.Append(" Ticket: "+index + nl);
			builder.Append(" Name: "+ticket.Code + nl);
	
			if	(ticket.TicketRailFareData != null) 
			{
				builder.Append(string.Format(" Origin NLC/Actual: {0}{1} {2}",
                    ticket.TicketRailFareData.OriginNlc,
                    ticket.TicketRailFareData.OriginNlcActual,
                    ticket.TicketRailFareData.OriginName) + nl);
				builder.Append(string.Format(" Destination NLC/Actual: {0}{1} {2}",
                    ticket.TicketRailFareData.DestinationNlc,
                    ticket.TicketRailFareData.DestinationNlcActual, 
                    ticket.TicketRailFareData.DestinationName) + nl);
			}

			builder.Append(" Flexibility: "+ticket.Flexibility + nl);

            if (ticket.TicketRailFareData != null)
            {
                builder.Append(" Route code: " + ticket.TicketRailFareData.RouteCode + nl);
            }

			builder.Append(" Adult fare: "+ticket.AdultFare + nl);
			builder.Append(" Child fare: "+ticket.ChildFare + nl);
			builder.Append(" Discounted adult fare: "+ticket.DiscountedAdultFare + nl);
			builder.Append(" Discounted child fare: "+ticket.DiscountedChildFare + nl);

			foreach (Upgrade upgrade in ticket.Upgrades)
			{
				builder.Append(" Upgrade: " + upgrade.Code + " (" + upgrade.Description + ") cost = " + upgrade.Cost + nl);
			}
		}
	}
}
