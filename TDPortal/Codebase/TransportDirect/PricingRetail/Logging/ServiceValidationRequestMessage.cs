//********************************************************************************
//NAME         : ServiceValidationRequestMessage.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-04-02
//DESCRIPTION  : Create printable summary of a Service Validation request
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Logging/ServiceValidationRequestMessage.cs-arc  $
//
//   Rev 1.1   Jun 03 2010 09:12:18   mmodi
//Added additional parameters used for the RBO MR call to the output string
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.0   Nov 08 2007 12:37:02   mturner
//Initial revision.
//
//   Rev 1.5   Jan 18 2006 18:16:38   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.4   Aug 19 2005 14:05:30   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.3.1.1   Aug 16 2005 11:19:08   RPhilpott
//Get rid of warnings from deprecated methods.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.3.1.0   Jul 08 2005 10:26:40   rgreenwood
//DN062: Class now references an instance of the VehicleFeatureToDtoConvertor to access vehicle features
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.3   Apr 12 2005 09:43:16   RPhilpott
//Make output more Notepad-friendly.
//
//   Rev 1.2   Apr 05 2005 15:19:50   RPhilpott
//Add more logging
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

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.PricingRetail.RBOGateway;


namespace TransportDirect.UserPortal.PricingRetail.Logging
{
	/// <summary>
	/// Create printable summary of a Service Validation request
	/// </summary>
	public sealed class ServiceValidationRequestMessage
	{
		static readonly string nl = Environment.NewLine;

		// private ctor - static methods only
		private ServiceValidationRequestMessage()
		{
		}

		public static string Message(TDLocation origin, TDLocation destination, TDDateTime outwardDate, 
				TDDateTime returnDate, RailFareData outwardFareData, RailFareData returnFareData, 
				ArrayList outwardJourneys, ArrayList returnJourneys, 
                string outwardRestrictionCodesToReapply, string returnRestrictionCodesToReapply,
                bool outwardTocCheckRequired, bool returnTocCheckRequired,
                bool outwardCrossLondonToCheck, bool returnCrossLondonToCheck,
                bool outwardZonalIndicatorToCheck, bool returnZonalIndicatorToCheck,
                bool outwardVisitCRSToCheck, bool returnVisitCRSToCheck)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(nl + "ServiceValidationForFare request" + nl);

			sb.Append("Origin: " + origin.Description + nl);

			foreach (TDNaptan tdn in origin.NaPTANs)
			{
				sb.Append("\t" + tdn.Naptan + " (" + tdn.Name + ")" + nl);
			}

			sb.Append("Destination: " + destination.Description + nl);

			foreach (TDNaptan tdn in destination.NaPTANs)
			{
				sb.Append("\t" + tdn.Naptan + " (" + tdn.Name + ")" + nl);
			}

			sb.Append("OutwardDate: ");
			sb.Append(outwardDate.ToString("yyyy-MM-dd"));
			sb.Append("\tReturnDate: ");
			sb.Append(returnDate != null ? returnDate.ToString("yyyy-MM-dd") : "none");

			sb.Append(nl + "Outward fare data:" + nl);
			sb.Append("Origin NLC/Actual: " + outwardFareData.OriginNlc + "/" + outwardFareData.OriginNlcActual);
			sb.Append("\tDestination NLC/Actual: " + outwardFareData.DestinationNlc + "/" + outwardFareData.DestinationNlcActual);
			sb.Append("\tTkt code: " + outwardFareData.ShortTicketCode);
			sb.Append("\tRailcard: " + outwardFareData.RailcardCode);
			sb.Append("\tRoute: " + outwardFareData.RouteCode);
			sb.Append("\tRestrictions: " + outwardFareData.RestrictionCode);

			sb.Append(nl + "Outward restrictions to reapply: " + outwardRestrictionCodesToReapply);
            sb.Append(nl + "Outward toc check required: " + outwardTocCheckRequired);
            sb.Append(nl + "Outward cross london check: " + outwardCrossLondonToCheck);
            sb.Append(nl + "Outward zonal indicator check: " + outwardZonalIndicatorToCheck);
            sb.Append(nl + "Outward visit CRS check: " + outwardVisitCRSToCheck); 

			if	(returnFareData == null)
			{
                sb.Append(nl + "Return fare data:" + nl);
				sb.Append("no return fare data");
			}
			else
			{
				sb.Append(nl + "Return fare data:" + nl);
				sb.Append("Origin NLC/Actual: " + returnFareData.OriginNlc + "/" + returnFareData.OriginNlcActual);
				sb.Append("\tDestination NLC/Actual: " + returnFareData.DestinationNlc + "/" + returnFareData.DestinationNlcActual);
				sb.Append("\tTkt code: " + returnFareData.ShortTicketCode);
				sb.Append("\tRailcard: " + returnFareData.RailcardCode);
				sb.Append("\tRoute: " + returnFareData.RouteCode);
				sb.Append("\tRestrictions: " + returnFareData.RestrictionCode);
			}

			sb.Append(nl + "Return restrictions to reapply: " + returnRestrictionCodesToReapply);
            sb.Append(nl + "Return toc check required: " + returnTocCheckRequired);
            sb.Append(nl + "Return cross london check: " + returnCrossLondonToCheck);
            sb.Append(nl + "Return zonal indicator check: " + returnZonalIndicatorToCheck);
            sb.Append(nl + "Return visit CRS check: " + returnVisitCRSToCheck); 

			sb.Append(nl + "Outward Journeys (" + outwardJourneys.Count + ")");
			
			foreach (PublicJourney pj in outwardJourneys)
			{

				sb.Append(nl + "Index:\t" + pj.JourneyIndex);

				foreach (PublicJourneyDetail pjd in pj.Details)
				{
					VehicleFeatureToDtoConvertor vehicleFeaturesConverter = new VehicleFeatureToDtoConvertor(pjd.GetVehicleFeatures());
					
					sb.Append(nl + " Dep: " + pjd.LegStart.DepartureDateTime.ToString("yyyyMMdd HHmm") + "  " + pjd.LegStart.Location.NaPTANs[0].Naptan + "  \t--  Arr: " + pjd.LegEnd.ArrivalDateTime.ToString("yyyyMMdd HHmm") + "  " + pjd.LegEnd.Location.NaPTANs[0].Naptan);
					sb.Append(nl + " UID:" +  pjd.Services[0].PrivateId + "\t Seats: " + vehicleFeaturesConverter.SeatingClass + "\t Sleeper:" + vehicleFeaturesConverter.SleeperClass + "\t Reservability: " + vehicleFeaturesConverter.Reservations + "\t Catering: " + vehicleFeaturesConverter.Catering);
				}
			}

			sb.Append(nl + "Return Journeys (" + returnJourneys.Count + ")");

			foreach (PublicJourney pj in returnJourneys)
			{
				sb.Append(nl + "Index:\t" + pj.JourneyIndex);

				foreach (PublicJourneyDetail pjd in pj.Details)
				{
					VehicleFeatureToDtoConvertor vehicleFeaturesConverter = new VehicleFeatureToDtoConvertor(pjd.GetVehicleFeatures());

					sb.Append(nl + " Dep: " + pjd.LegStart.DepartureDateTime.ToString("yyyyMMdd HHmm") + "  " + pjd.LegStart.Location.NaPTANs[0].Naptan + "  \t--  Arr: " + pjd.LegEnd.ArrivalDateTime.ToString("yyyyMMdd HHmm") + "  " + pjd.LegEnd.Location.NaPTANs[0].Naptan);
					sb.Append(nl + " UID:" +  pjd.Services[0].PrivateId + "\t Seats: " + vehicleFeaturesConverter.SeatingClass + "\t Sleeper:" + vehicleFeaturesConverter.SleeperClass + "\t Reservability: " + vehicleFeaturesConverter.Reservations + "\t Catering: " + vehicleFeaturesConverter.Catering);
				}
			}

			sb.Append(nl + "End of ServiceValidationForFare request" + nl);

			return sb.ToString();
		}
	}
}
