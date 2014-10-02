//********************************************************************************
//NAME         : PriceRouteRequestMessage.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-31
//DESCRIPTION  : Create printable summary of a PriceRoute request
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Logging/PriceRouteRequestMessage.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:00   mturner
//Initial revision.
//
//   Rev 1.5   Jan 18 2006 18:16:36   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.4   Jan 17 2006 18:10:48   RPhilpott
//Code review updates
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.3   Nov 09 2005 12:31:40   build
//Automatically merged from branch for stream2818
//
//   Rev 1.2.1.2   Nov 03 2005 12:00:36   mguney
//Inline null checks changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2.1.1   Nov 03 2005 11:57:48   mguney
//Added message info for the new parameters sessionInfo, operatorCode,combinedTickets, legNumber, quotaFares. Also provided a new overloaded method.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2.1.0   Nov 03 2005 11:19:58   RPhilpott
//Added extra params.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Apr 12 2005 09:43:14   RPhilpott
//Make output more Notepad-friendly.
//
//   Rev 1.1   Apr 03 2005 18:23:38   RPhilpott
//Unit test improvements.
//
//   Rev 1.0   Mar 31 2005 18:40:38   RPhilpott
//Initial revision.
//

using System;
using System.Text;
using System.Collections;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.CoachRoutes;


namespace TransportDirect.UserPortal.PricingRetail.Logging
{
	/// <summary>
	/// Create printable summary of a PriceRoute request
	/// </summary>
	public sealed class PriceRouteRequestMessage
	{
		private static readonly string nl = Environment.NewLine;

		// private ctor - static methods only
		private PriceRouteRequestMessage()
		{
		}
		

		/// <summary>
		/// Provides log information for the given parameters.
		/// </summary>
		/// <param name="dates"></param>
		/// <param name="origin"></param>
		/// <param name="destination"></param>
		/// <param name="discounts"></param>
		/// <param name="sessionInfo"></param>
		/// <param name="operatorCode"></param>
		/// <param name="combinedTickets"></param>
		/// <param name="legNumber"></param>
		/// <param name="quotaFares"></param>
		/// <returns></returns>
		public static string Message(ArrayList dates, TDLocation origin, TDLocation destination, 
			Discounts discounts, CJPSessionInfo sessionInfo, string operatorCode, 
			int combinedTickets, int legNumber, QuotaFareList quotaFares)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(nl + "PriceRoute request information" + nl);

			sb.Append("Origin: " + origin.Description + nl);

			foreach (TDNaptan tdn in origin.NaPTANs)
			{
				sb.Append("\t" + tdn.Naptan + " (" + tdn.Name + ") (Fares = " + (tdn.UseForFareEnquiries ? "Y" : "N")  + ")" + nl);
			}

			sb.Append("Destination: "+ destination.Description + nl);

			foreach (TDNaptan tdn in destination.NaPTANs)
			{
				sb.Append("\t" + tdn.Naptan + " (" + tdn.Name + ") (Fares = " + (tdn.UseForFareEnquiries ? "Y" : "N")  + ")" + nl);
			}

			foreach (TravelDate td in dates)
			{
				sb.Append("OutwardDate: ");
				sb.Append(td.OutwardDate.ToString("yyyy-MM-dd"));
				sb.Append("\tReturnDate: ");
				sb.Append(td.ReturnDate != null ? td.ReturnDate.ToString("yyyy-MM-dd") : "none");
				sb.Append("\tMode: " + td.TravelMode.ToString() + nl);			
			}

			sb.Append("Rail discount: ");
			sb.Append((discounts.RailDiscount != null && discounts.RailDiscount.Length > 0) ? discounts.RailDiscount : "none");

			sb.Append(nl + "Coach discount: ");
			sb.Append((discounts.CoachDiscount != null && discounts.CoachDiscount.Length > 0) ? discounts.CoachDiscount : "none");

			sb.Append(nl + "Session Info: " + ((sessionInfo == null) ? "null" : string.Empty));
			if (sessionInfo != null)
			{
				sb.Append("IsLoggedOn: " + sessionInfo.IsLoggedOn.ToString(CultureInfo.InvariantCulture));
				sb.Append("\tIsLoggedOn: " + sessionInfo.Language);
				sb.Append("\tOriginAppDomainFriendlyName: " + sessionInfo.OriginAppDomainFriendlyName);
				sb.Append("\tSessionId: " + sessionInfo.SessionId);
				sb.Append("\tUserType: " + sessionInfo.UserType.ToString(CultureInfo.InvariantCulture));
			}

			sb.Append(nl + "Operator Code: " + operatorCode);
			
			sb.Append(nl + "Combined Tickets: " + combinedTickets.ToString(CultureInfo.InvariantCulture));
			
			sb.Append(nl + "Leg Number: " + legNumber.ToString(CultureInfo.InvariantCulture));

			sb.Append(nl + "Quota Fares: " + ((quotaFares == null) ? "null" : ""));
			if (quotaFares != null)
			{
				sb.Append("Count: " + quotaFares.Count.ToString(CultureInfo.InvariantCulture));
				for (int i=0;i < quotaFares.Count;i++)
				{					
					sb.Append(nl + "Quota Fare " + i.ToString(CultureInfo.InvariantCulture) + ": " + ((quotaFares[i] == null) ? "null" : ""));
					if (quotaFares[i] != null)
					{
						sb.Append("\tOriginNaPTAN: " + quotaFares[i].OriginNaPTAN);
						sb.Append("\tDestinationNaPTAN: " + quotaFares[i].DestinationNaPTAN);
						sb.Append("\tFare: " + quotaFares[i].Fare.ToString(CultureInfo.InvariantCulture));				
						sb.Append("\tTicketType: " + quotaFares[i].TicketType);
					}
					
				}
			}

			sb.Append(nl + "End of PriceRoute request" + nl);

			return sb.ToString();
		}

		/// <summary>
		/// Overloaded method.
		/// </summary>
		/// <param name="dates"></param>
		/// <param name="origin"></param>
		/// <param name="destination"></param>
		/// <param name="discounts"></param>
		/// <returns></returns>
		public static string Message(ArrayList dates, TDLocation origin, TDLocation destination, 
			Discounts discounts)
		{
			return Message(dates,origin,destination,discounts,null,null,-1,-1,null);
		}
	}
}
