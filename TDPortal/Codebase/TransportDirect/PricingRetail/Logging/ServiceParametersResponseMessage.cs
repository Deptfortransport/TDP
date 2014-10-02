//********************************************************************************
//NAME         : ServiceParameterResponseMessage.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-04-02
//DESCRIPTION  : Create printable summary of a Service Parameters response
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Logging/ServiceParametersResponseMessage.cs-arc  $
//
//   Rev 1.1   Jun 03 2010 09:11:36   mmodi
//Added additional parameters used for the RBO MR call to the output string
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.0   Nov 08 2007 12:37:02   mturner
//Initial revision.
//
//   Rev 1.4   Jan 18 2006 18:16:38   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.3   Apr 12 2005 09:43:16   RPhilpott
//Make output more Notepad-friendly.
//
//   Rev 1.2   Apr 08 2005 13:55:20   RPhilpott
//Change restrictionCodesToReapply from string[] to single string.
//
//   Rev 1.1   Apr 05 2005 11:20:12   RPhilpott
//Logging improvements.
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
	/// Create printable summary of a Service Parameters response
	/// </summary>
	public sealed class ServiceParameterResponseMessage
	{
		static readonly string nl = Environment.NewLine;

		// private ctor - static methods only
		private ServiceParameterResponseMessage()
		{
		}

		public static string Message(RailServiceParameters[] responses)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(nl + "Service Parameters response" + nl);

			int count = 0;

			foreach (RailServiceParameters param in responses)
			{
				sb.Append(nl + ((count == 0 ? "Outward" : "Return") + " parameter: "));
				count++;
				
				sb.Append(nl + "Include TOCs: ");
				foreach (TocDto toc in param.IncludeTocs)
				{
					sb.Append(toc.Code + " ");
				}

				sb.Append("\tExclude TOCs: ");
				foreach (TocDto toc in param.ExcludeTocs)
				{
					sb.Append(toc.Code + " ");
				}

				sb.Append(nl + "Include UIDs: ");
				foreach (string uid in param.IncludeTrainUids)
				{
					sb.Append(uid + " ");
				}

				sb.Append("\tExclude UIDs: ");
				foreach (string uid in param.ExcludeTrainUids)
				{
					sb.Append(uid + " ");
				}

				sb.Append(nl + "Restriction codes to reapply: ");
				sb.Append(param.RestrictionCodesToReapply + " ");

				sb.Append(nl + "Include locations: ");
				foreach (TDLocation loc in param.IncludeLocations)
				{
					foreach (TDNaptan tdn in loc.NaPTANs)
					{
						sb.Append(tdn.Naptan + " (" + tdn.Name + ") ");
					}
				}

				sb.Append(nl + "Exclude locations: ");
				foreach (TDLocation loc in param.ExcludeLocations)
				{
					foreach (TDNaptan tdn in loc.NaPTANs)
					{
						sb.Append(tdn.Naptan + " (" + tdn.Name + ") ");
					}
				}

				sb.Append(nl);

				if	(param.AdjustedDateTime != null)
				{
					sb.Append("Adjusted date/time = " + param.AdjustedDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
				}

				sb.Append("\tChanges allowed = " + param.ChangesAllowed);

                sb.Append(nl + "TOC check required: " + param.ConnectingTocsToCheck);
                sb.Append(nl + "Cross london check: " + param.CrossLondonToCheck);
                sb.Append(nl + "Zonal indicator check: " + param.ZonalIndicatorToCheck);
                sb.Append(nl + "Visit CRS check: " + param.VisitCRSToCheck);
                sb.Append("\tOutputGL: [" + param.OutputGL + "]");

				sb.Append(nl + "Error resource-ids: ");

				foreach (string err in param.ErrorResourceIds)
				{
					sb.Append(err + "\t");
				}
		
			}


			sb.Append(nl + nl + "End of Service Parameters response" + nl);
			return sb.ToString();
		}
	}
}