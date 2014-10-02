//********************************************************************************
//NAME         : PriceRouteResponseMessage.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-31
//DESCRIPTION  : Create printable summary of a PriceRoute response
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Logging/PriceRouteResponseMessage.cs-arc  $
//
//   Rev 1.1   Jun 03 2010 09:09:54   mmodi
//Updated message to display "actual" nlc locations
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.0   Nov 08 2007 12:37:02   mturner
//Initial revision.
//
//   Rev 1.12   Jan 18 2006 18:16:36   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.11   Jan 17 2006 18:10:48   RPhilpott
//Code review updates
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.10   Nov 23 2005 15:49:38   RPhilpott
//Add retailId for logging, remove redundant quotaControlled.
//Resolution for 3038: DN040: Double reporting/logging of NRS requests
//
//   Rev 1.9   Nov 17 2005 17:37:04   RPhilpott
//Additional logging for coach fares.
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.8   Apr 29 2005 20:45:40   RPhilpott
//Fix typos in logging.
//Resolution for 2342: Del 7 - PT - Door to Door planner does not respond to unavailable ticket as expected
//
//   Rev 1.7   Apr 12 2005 10:03:42   RPhilpott
//Set minimum discounted fares correctly.
//Resolution for 2071: PT: Minimum train fares not flagged
//
//   Rev 1.6   Apr 12 2005 09:43:14   RPhilpott
//Make output more Notepad-friendly.
//
//   Rev 1.5   Apr 07 2005 19:00:32   RPhilpott
//Add ticket numbers. etc.
//
//   Rev 1.4   Apr 05 2005 11:55:12   RPhilpott
//Unit test fix.
//
//   Rev 1.3   Apr 05 2005 11:20:00   RPhilpott
//Logging improvements.
//
//   Rev 1.2   Apr 03 2005 18:23:40   RPhilpott
//Unit test improvements.
//
//   Rev 1.1   Apr 01 2005 15:43:08   jbroome
//Added logging for InwardTickets collection
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.0   Mar 31 2005 18:40:38   RPhilpott
//Initial revision.
//

using System;
using System.Text;
using System.Collections;

using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.PricingRetail.Logging
{
	/// <summary>
	/// Create printable summary of a PriceRoute response
	/// </summary>
	public sealed class PriceRouteResponseMessage
	{
		private static readonly string nl = Environment.NewLine;
		
		// private ctor - static methods only
		private PriceRouteResponseMessage()
		{
		}

		public static string Message(ArrayList dates)
		{

			StringBuilder sb = new StringBuilder();
			sb.Append(nl + "PriceRoute response information" + nl);
			
			sb.Append(dates.Count + " TravelDates returned" + nl);

			int dateCount = 0;

			foreach (TravelDate td in dates)
			{
				dateCount++;
				
				sb.Append(nl + "TravelDate: " + dateCount);
				sb.Append("\t OutwardDate: ");
				sb.Append(td.OutwardDate.ToString("yyyy-MM-dd"));
				sb.Append("\t ReturnDate: ");
				sb.Append(td.ReturnDate != null ? td.ReturnDate.ToString("yyyy-MM-dd") : "none");
				sb.Append("\t Mode: " + td.TravelMode.ToString());			
				sb.Append("\t TicketType: " + td.TicketType.ToString() + nl);

				sb.Append("MinAdultFare: " + td.MinAdultFare + "\tMaxAdultFare: " + td.MaxAdultFare 
					+ "\tMinChildFare: " + td.MinChildFare + "\tMaxChildFare: " + td.MaxChildFare + nl); 	  

				int count = 0;

				if (td.HasOutwardTickets)
				{
					sb.Append("Outward tickets found: " + td.OutwardTickets.Length + nl);
				
					foreach (CostSearchTicket ticket in td.OutwardTickets)
					{
						TicketMessage(sb, ticket, ++count);
					}
				}
				else
				{
					sb.Append("No Outward tickets" + nl);
				}

				if (td.HasReturnTickets)
				{
					sb.Append("Return tickets found: "  + td.ReturnTickets.Length + nl);

					count = 0;

					foreach (CostSearchTicket ticket in td.ReturnTickets)
					{
						TicketMessage(sb, ticket, ++count);
					}
				}
				else
				{
					sb.Append("No Return tickets" + nl);
				}

				if (td.HasInwardTickets)
				{
					sb.Append("Inward tickets found: " + td.InwardTickets.Length + nl);
				
					count = 0;

					foreach (CostSearchTicket ticket in td.InwardTickets)
					{
						TicketMessage(sb, ticket, ++count);
					}
				}
				else
				{
					sb.Append("No Inward tickets" + nl);
				}

			}

			sb.Append(nl + "End of PriceRoute response" + nl);

			return sb.ToString();
		}

		private static void TicketMessage(StringBuilder builder, CostSearchTicket ticket, int index)
		{
			builder.Append(" Ticket " + index + " combinedTicketIndex = " + ticket.CombinedTicketIndex + " legNumber " + ticket.LegNumber + nl);
			builder.Append("   Name: " + ticket.Code + nl);
			builder.Append("   Code: " + ticket.ShortCode + nl);
			
			if	(ticket.TicketCoachFareData != null)
			{
				builder.Append("   Operator: " + ticket.TicketCoachFareData.OperatorCode + nl);
				builder.Append("   From: " + ticket.TicketCoachFareData.OriginNaptan + " to " + ticket.TicketCoachFareData.DestinationNaptan + nl);
			}

			builder.Append("   Flexibility: " + ticket.Flexibility + nl);
			builder.Append("   Adult fare:  " + ticket.AdultFare + nl);
			builder.Append("   Child fare:  " + ticket.ChildFare + nl);
			builder.Append("   Discounted adult fare: " + ticket.DiscountedAdultFare + nl);
			builder.Append("   Discounted child fare: " + ticket.DiscountedChildFare + nl);
			builder.Append("   Minimum adult fare: " + ticket.MinimumAdultFare + nl);
			builder.Append("   Minimum child fare: " + ticket.MinimumChildFare + nl);
			
			if	(ticket.TicketRailFareData != null)
			{
				builder.Append(string.Format("   Rail fare data: {0}/{1} to {2}/{3}",
                    ticket.TicketRailFareData.OriginNlc,
                    ticket.TicketRailFareData.OriginNlcActual,
                    ticket.TicketRailFareData.DestinationNlc,
                    ticket.TicketRailFareData.DestinationNlcActual) + nl);
				builder.Append("   IsReturn: " + ticket.TicketRailFareData.IsReturn + "  Railcard: " + ticket.TicketRailFareData.RailcardCode);
				builder.Append("   Restriction: " + ticket.TicketRailFareData.RestrictionCode + " Route: " + ticket.TicketRailFareData.RouteCode + nl);
			}

		}

	}
}
