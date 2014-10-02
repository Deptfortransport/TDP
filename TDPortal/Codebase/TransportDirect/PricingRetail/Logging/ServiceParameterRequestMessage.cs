//********************************************************************************
//NAME         : ServiceParameterRequestMessage.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-04-02
//DESCRIPTION  : Create printable summary of a Service Parameters request
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Logging/ServiceParameterRequestMessage.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:02   mturner
//Initial revision.
//
//   Rev 1.4   Jan 18 2006 18:16:38   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.3   Jan 17 2006 18:10:50   RPhilpott
//Code review updates
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.2   Nov 24 2005 18:22:58   RPhilpott
//Changes to support use of ticket-specific locations in Find-A-Fare AssembleServices calls.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.1   Apr 12 2005 09:43:14   RPhilpott
//Make output more Notepad-friendly.
//
//   Rev 1.0   Apr 03 2005 18:24:08   RPhilpott
//Initial revision.
//

using System;
using System.Text;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.UserPortal.PricingRetail.Domain;


namespace TransportDirect.UserPortal.PricingRetail.Logging
{
	/// <summary>
	/// Create printable summary of a Service Parameters request
	/// </summary>
	public sealed class ServiceParameterRequestMessage
	{
		private static readonly string nl = Environment.NewLine;

		// private ctor - static methods only
		private ServiceParameterRequestMessage()
		{
		}

		public static string Message(TDDateTime outwardDate, TDDateTime returnDate, 
										RailFareData outwardFareData, RailFareData returnFareData)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(nl + "ServiceParametersForFare request" + nl);

			sb.Append("Origin: ");

			foreach (TDNaptan tdn in outwardFareData.Origin.NaPTANs)
			{
				sb.Append("\t" + tdn.Naptan + " (" + tdn.Name + ")" + nl);
			}

			sb.Append("Destination: ");

			foreach (TDNaptan tdn in outwardFareData.Destination.NaPTANs)
			{
				sb.Append("\t" + tdn.Naptan + " (" + tdn.Name + ")" + nl);
			}

			sb.Append("OutwardDate: ");
			sb.Append(outwardDate.ToString("yyyy-MM-dd"));
			sb.Append("\tReturnDate: ");
			sb.Append(returnDate != null ? returnDate.ToString("yyyy-MM-dd") : "none");

			sb.Append(nl + "Outward fare data:" + nl);
			sb.Append("Origin NLC: " + outwardFareData.OriginNlc);
			sb.Append("\tDestination NLC: " + outwardFareData.DestinationNlc);
			sb.Append("\tTkt code: " + outwardFareData.ShortTicketCode);
			sb.Append("\tRailcard: " + outwardFareData.RailcardCode);
			sb.Append("\tRoute: " + outwardFareData.RouteCode);
			sb.Append("\tRestrictions: " + outwardFareData.RestrictionCode + nl);
			sb.Append("From " );

			foreach (LocationDto loc in outwardFareData.Origins)
			{
				sb.Append(loc.Crs + " ");
			}

			sb.Append(" to " );

			foreach (LocationDto loc in outwardFareData.Destinations)
			{
				sb.Append(loc.Crs + " ");
			}

			sb.Append(nl);

			if	(returnFareData == null)
			{
				sb.Append(nl + "No return fare data:" + nl);
			}
			else
			{
				sb.Append(nl + "Return fare data:" + nl);
				sb.Append("Origin NLC: " + returnFareData.OriginNlc);
				sb.Append("\tDestination NLC: " + returnFareData.DestinationNlc);
				sb.Append("\tTkt code: " + returnFareData.ShortTicketCode);
				sb.Append("\tRailcard: " + returnFareData.RailcardCode);
				sb.Append("\tRoute: " + returnFareData.RouteCode);
				sb.Append("\tRestrictions: " + returnFareData.RestrictionCode + nl);
				sb.Append("From " );

				foreach (LocationDto loc in returnFareData.Origins)
				{
					sb.Append(loc.Crs + " ");
				}

				sb.Append(" to " );

				foreach (LocationDto loc in returnFareData.Destinations)
				{
					sb.Append(loc.Crs + " ");
				}

				sb.Append(nl);
			}

			sb.Append(nl + "End of ServiceParametersForFare request" + nl);

			return sb.ToString();
		}
	}
}
