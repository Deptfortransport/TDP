//********************************************************************************
//NAME         : ServiceValidationResponseMessage.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-04-02
//DESCRIPTION  : Create printable summary of a Service Validation response
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Logging/ServiceValidationResponseMessage.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:04   mturner
//Initial revision.
//
//   Rev 1.4   Jan 18 2006 18:16:38   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.3   Dec 02 2005 12:38:50   RPhilpott
//Log "no train found" condition
//Resolution for 3202: DN040: Specific handling of no inventory condition.
//
//   Rev 1.2   Apr 12 2005 09:43:16   RPhilpott
//Make output more Notepad-friendly.
//
//   Rev 1.1   Apr 05 2005 11:20:12   RPhilpott
//Logging improvements.
//
//   Rev 1.0   Apr 03 2005 18:24:10   RPhilpott
//Initial revision.
//

using System;
using System.Text;
using System.Collections;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.UserPortal.PricingRetail.Domain;


namespace TransportDirect.UserPortal.PricingRetail.Logging
{
	/// <summary>
	/// Create printable summary of a Service Validation response
	/// </summary>
	public sealed class ServiceValidationResponseMessage
	{
		
		static readonly string nl = Environment.NewLine;

		// private ctor - static methods only
		private ServiceValidationResponseMessage()
		{
		}

		public static string Message(RailServiceValidationResultsDto response)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(nl + "Service Validation response" + nl);

			sb.Append(nl + "Outward journeys:");

			foreach (JourneyValidityDto val in response.OutwardValidities)
			{
				sb.Append(nl + "Index: " + val.JourneyIndex + "\tvalidity: " + val.Validity);
				sb.Append("\tSupplements: ");

				foreach (SupplementDto sup in val.Supplements)
				{
					sb.Append("\t" + sup.Code + " (" + sup.Cost.ToString(CultureInfo.InvariantCulture) + ")");
				}
			}

			sb.Append(nl + nl + "Return journeys:");

			foreach (JourneyValidityDto val in response.ReturnValidities)
			{
				sb.Append(nl + "Index: " + val.JourneyIndex + "\tvalidity: " + val.Validity);
				sb.Append("\tSupplements: ");

				foreach (SupplementDto sup in val.Supplements)
				{
					sb.Append("\t" + sup.Code + " (" + sup.Cost.ToString(CultureInfo.InvariantCulture) + ")");
				}
			}

			sb.Append(nl + "NRS/RVBO results: ");

			foreach (RailAvailabilityResultDto rvbo in response.RailAvailabilityResults)
			{
				sb.Append(nl + rvbo.ToString());
			}

			sb.Append(nl + "Includes NoInventory Results: " + response.IncludesNoInventoryResults.ToString());

			sb.Append(nl + "Error resource-ids: ");

			foreach (string err in response.ErrorResourceIds)
			{
				sb.Append(err + "\t");
			}

			sb.Append(nl + "End of Service Validation response" + nl);
			return sb.ToString();
		}
	}
}