//********************************************************************************
//NAME         : PricingRequestMessage.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 06/11/2003
//DESCRIPTION  : Implementation of PricingRequestMessage class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Logging/PricingRequestMessage.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:02   mturner
//Initial revision.
//
//   Rev 1.8   Jan 18 2006 18:16:36   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.7   Jan 17 2006 18:10:48   RPhilpott
//Code review updates
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.6   Nov 16 2005 16:44:04   RPhilpott
//Omit Metro legs from list passed to RBO.
//Resolution for 3089: DN040: Manchester Metro on Find-A-Fare
//
//   Rev 1.5   Aug 24 2005 12:21:42   RWilby
//Updated class to check for null values in detail.Destination
//Resolution for 2683: Server Error on Coach journey Ticket/Costs tab
//
//   Rev 1.4   Aug 19 2005 14:05:08   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.3.1.1   Aug 16 2005 11:18:44   RPhilpott
//Get rid of warnings from deprecated methods.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.3.1.0   Jul 08 2005 10:16:12   rgreenwood
//DN062: Class now references an instance of the VehicleFeatureToDtoConvertor to access vehicle features
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.3   Apr 12 2005 09:43:14   RPhilpott
//Make output more Notepad-friendly.
//
//   Rev 1.2   Apr 05 2005 15:19:50   RPhilpott
//Add more logging
//
//   Rev 1.1   Apr 14 2004 14:18:06   CHosegood
//Logging failed if the TDLocaiton did not contain any naptans
//Resolution for 756: Fares crash - Leicester City F/b Club
//
//   Rev 1.0   Nov 07 2003 09:47:58   acaunt
//Initial Revision

using System;
using System.Text;
using Logger = System.Diagnostics.Trace;

using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.PricingRetail.RBOGateway;


namespace TransportDirect.UserPortal.PricingRetail.Logging
{
	/// <summary>
	/// Obtain a printable summary of a pricing request
	/// </summary>
	public sealed class PricingRequestMessage
	{
		private static readonly string nl = Environment.NewLine;

		private PricingRequestMessage()
		{
		}


		public static string Message(PricingUnit pricingUnit)
		{
			int i=0;
			StringBuilder builder = new StringBuilder();
			builder.Append("Pricing request information" + nl);
			builder.Append("Mode: "+pricingUnit.Mode + nl);
			builder.Append("Operator code: "+pricingUnit.OperatorCode+ nl);
			builder.Append("Matching return journey?" +pricingUnit.MatchingReturn+ nl);
			builder.Append("Includes underground?"+pricingUnit.IncludesUnderground+ nl);
			if (pricingUnit.OutboundLegs.Count != 0) 
			{
				builder.Append("Outbound legs:\n");
				i=0;
				foreach (PublicJourneyDetail detail in pricingUnit.OutboundLegs) 
				{

					LegMessage(builder, detail, ++i);
				}
			}
			if (pricingUnit.InboundLegs.Count != 0) 
			{
				builder.Append("Inbound legs:" + nl);
				i=0;
				foreach (PublicJourneyDetail detail in pricingUnit.InboundLegs) 
				{
					LegMessage(builder, detail, ++i);
				}
			}
			builder.Append(nl + "End of pricing request");
			return builder.ToString();
		}

        private static void LegMessage(StringBuilder builder, PublicJourneyDetail detail, int index)
        {
			
            builder.Append("  Leg: " + index);

            builder.Append("  OperatorCode :"+detail.Services[0].OperatorCode+ nl);

            builder.Append("  privateId :"+detail.Services[0].PrivateId + nl);

            //Boarding
            if ( detail.LegStart.Location != null ) 
            {
                if (detail.LegStart.Location.NaPTANs.Length > 0 ) 
                {
                    builder.Append("  Boarding :" + "EMPTY" + " ("
                        + detail.LegStart.Location.Description == null ? string.Empty : detail.LegStart.Location.Description
                        + ") at " + detail.LegStart.DepartureDateTime + nl );
                }
                else 
                {
                    builder.Append("  Boarding :" + detail.LegStart.Location.NaPTANs[0].Naptan + " ("
                        + detail.LegStart.Location.Description == null ? string.Empty : detail.LegStart.Location.Description
                        + ") at " + detail.LegStart.DepartureDateTime + nl );
                }
            }
            else 
            {
                builder.Append("  Boarding :"+"NULL"+" ("+"NULL"+") at "+detail.LegStart.DepartureDateTime + nl);
            }

            //Alighting
            if ( detail.LegEnd.Location != null ) 
            {
                if (detail.LegEnd.Location.NaPTANs.Length > 0 ) 
                {
                    builder.Append("  Alighting :" + detail.LegEnd.Location.NaPTANs[0].Naptan + " ("
                        + detail.LegEnd.Location.Description == null ? string.Empty : detail.LegEnd.Location.Description
                        + ") at " + detail.LegEnd.ArrivalDateTime + nl );
                }
                else 
                {
                    builder.Append("  Alighting :" + "EMPTY" + " ("
                        + detail.LegEnd.Location.Description == null ? string.Empty : detail.LegEnd.Location.Description
                        + ") at " + detail.LegEnd.ArrivalDateTime + nl );
                }
            }
            else 
            {
                builder.Append("  Alighting :"+"NULL"+" ("+"NULL"+") at "+detail.LegEnd.ArrivalDateTime + nl);
            }

            //Destination
            if (detail.Destination != null && detail.Destination.Location != null )
                if (detail.Destination.Location.NaPTANs.Length > 0 )
                {
                    builder.Append("  Destination :" + detail.Destination.Location.NaPTANs[0].Naptan + " ("
                        + detail.Destination.Location.Description == null ? string.Empty:detail.Destination.Location.Description
                        + ") at " + detail.Destination.ArrivalDateTime + nl );
                }
                else
                {
                    builder.Append("  Destination :" + "EMPTY" + " ("
                        + detail.Destination.Location.Description == null ? string.Empty : detail.Destination.Location.Description
                        + ") at " + detail.Destination.ArrivalDateTime + nl );
                }
            else 
                builder.Append("  Destination :"+"NULL"+" ("+"NULL"+")" + nl);

            //Operator Code & PrivateId
            if ( detail.Services != null ) 
            {
                if (detail.Services.Length > 0 ) 
                {
                    builder.Append("  OperatorCode :"
                        + detail.Services[0].OperatorCode == null?string.Empty:detail.Services[0].OperatorCode + nl);

					builder.Append("  UID :"
						+ detail.Services[0].PrivateId == null?string.Empty:detail.Services[0].PrivateId + nl);
				
					builder.Append("  retailId :"
						+ detail.Services[0].RetailTrainId == null?string.Empty:detail.Services[0].RetailTrainId + nl);
				}
                else 
                {
                    builder.Append("  OperatorCode :"+"EMPTY"+ nl);
					builder.Append("  UID :"+"EMPTY"+ nl);
					builder.Append("  retailId :"+"EMPTY"+ nl);
				}
            }
            else  
            {
                builder.Append("  OperatorCode :"+"NULL"+ nl);
				builder.Append("  UID :"+"EMPTY"+ nl);
				builder.Append("  retailId :"+"EMPTY"+ nl);
			}

			VehicleFeatureToDtoConvertor vehicleFeatures = new VehicleFeatureToDtoConvertor(detail.GetVehicleFeatures());

			

			builder.Append("Seats: \"" + vehicleFeatures.SeatingClass + "\"\t Sleeper: \"" + vehicleFeatures.SleeperClass + "\"\t Reservability: \"" + vehicleFeatures.Reservations + "\"\t Catering: " + vehicleFeatures.Catering + nl);

        }
	}
}
