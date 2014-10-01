// *********************************************** 
// NAME			: TestJourneyRequestData.cs
// AUTHOR		: Richard Philpott
// DATE CREATED	: 2006-02-21 
// DESCRIPTION	: Shared Test data creation for various 
//				  NUnit tests of JourneyControl.		
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestJourneyRequestData.cs-arc  $
//
//   Rev 1.1   Sep 01 2011 10:43:28   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.0   Nov 08 2007 12:24:14   mturner
//Initial revision.
//
//   Rev 1.1   Feb 27 2006 12:20:08   RPhilpott
//Assign to IR 0018
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.0   Feb 27 2006 12:19:28   RPhilpott
//Initial revision.
//

using System;
using System.IO;
using System.Collections;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for TestJourneyRequestData.
	/// </summary>
	public class TestJourneyRequestData
	{
		private DateTime outwardDateTime; 
		private DateTime returnDateTime; 
		
		public TestJourneyRequestData(DateTime outwardDateTime, DateTime returnDateTime)
		{
			this.outwardDateTime = outwardDateTime;
			this.returnDateTime = returnDateTime;
		}

		public ITDJourneyRequest InitialiseDefaultRequest(bool outwardOnly)
		{
			TDJourneyRequest request = new TDJourneyRequest();

			request.IsReturnRequired = !outwardOnly;

			request.OutwardDateTime = new TDDateTime[1];
			request.OutwardDateTime[0] = new TDDateTime(outwardDateTime);

			if	(outwardOnly)
			{
				request.ReturnDateTime = new TDDateTime[0];
			}
			else
			{
				request.ReturnDateTime = new TDDateTime[1];
				request.ReturnDateTime[0] = new TDDateTime(returnDateTime);
			}

			request.OriginLocation = new TDLocation();
			request.OriginLocation.Description = "OriginName";
			request.OriginLocation.Locality = "E00001234";
			request.OriginLocation.NaPTANs = new TDNaptan[3];
			request.OriginLocation.NaPTANs[0] = new TDNaptan();
			request.OriginLocation.NaPTANs[1] = new TDNaptan();
			request.OriginLocation.NaPTANs[2] = new TDNaptan();
			request.OriginLocation.NaPTANs[0].Naptan = "9100Naptan1";
			request.OriginLocation.NaPTANs[1].Naptan = "9200Naptan2";
			request.OriginLocation.NaPTANs[2].Naptan = "9000Naptan3";
			request.OriginLocation.NaPTANs[0].GridReference = new OSGridReference(123, 456); 
			request.OriginLocation.NaPTANs[1].GridReference = new OSGridReference(1234, 4567); 
			request.OriginLocation.NaPTANs[2].GridReference = new OSGridReference(1234, 4567); 
			request.OriginLocation.Toid = new string[] { "1234", "5678"};

			request.DestinationLocation = new TDLocation();
			request.DestinationLocation.Description = "DestinationName";
			request.DestinationLocation.Locality = "E00004321";
			request.DestinationLocation.NaPTANs = new TDNaptan[2];
			request.DestinationLocation.NaPTANs[0] = new TDNaptan();
			request.DestinationLocation.NaPTANs[1] = new TDNaptan();
			request.DestinationLocation.NaPTANs[0].Naptan = "9100Naptan1";
			request.DestinationLocation.NaPTANs[1].Naptan = "9200Naptan2";
			request.DestinationLocation.NaPTANs[0].GridReference = new OSGridReference(123, 456); 
			request.DestinationLocation.NaPTANs[1].GridReference = new OSGridReference(1234, 4567); 
			request.DestinationLocation.Toid = new string[] { "ABCDE", "FGHIJ", "KLMNO"};

			request.Modes = new ModeType[] {ModeType.Rail, ModeType.Car, ModeType.Bus, ModeType.Coach};

			request.PrivateAlgorithm = PrivateAlgorithmType.Fastest;		
			request.PublicAlgorithm = PublicAlgorithmType.NoChanges;	
		
			request.PublicViaLocations = new TDLocation[1];
			request.PublicViaLocations[0] = new TDLocation();
			request.PublicViaLocations[0].Description = "Reigate";
			request.PublicViaLocations[0].Locality = "E00004321";
			request.PublicViaLocations[0].Status = TDLocationStatus.Valid;
		
			request.PublicSoftViaLocations = new TDLocation[1];
			request.PublicSoftViaLocations[0] = new TDLocation();
			request.PublicSoftViaLocations[0].Description = "Redhill";
			request.PublicSoftViaLocations[0].Locality = "E00004321";
			request.PublicSoftViaLocations[0].Status = TDLocationStatus.Valid;

			request.PublicNotViaLocations = new TDLocation[1];
			request.PublicNotViaLocations[0] = new TDLocation();
			request.PublicNotViaLocations[0].Description = "Bath";
			request.PublicNotViaLocations[0].Locality = "E00004321";
			request.PublicNotViaLocations[0].Status = TDLocationStatus.Valid;

			request.AvoidRoads = new string[]{ "A406(T)" };
            request.AvoidToidsOutward = new string[] { "4000000009316750", "4000000009316751" };
            request.AvoidToidsReturn = null;
			request.IncludeRoads = new string[]{ "A406" };

			request.PrivateViaLocation = new TDLocation();
			request.PrivateViaLocation.Description = "Reigate";
			request.PrivateViaLocation.Locality = "E00004321";
			request.PrivateViaLocation.Status = TDLocationStatus.Valid;
			request.PrivateViaLocation.Toid = new string[] { "ABCDE", "FGHIJ", "KLMNO"};


			return request;

		}

		public ITDJourneyRequest InitialiseDefaultFlightRequest(bool outwardOnly)
		{
			TDJourneyRequest request = new TDJourneyRequest();

			request.IsTrunkRequest = true;
			request.Modes = new ModeType[] { ModeType.Air };

			request.IsReturnRequired = !outwardOnly;
			request.OutwardAnyTime = false;
			request.ReturnAnyTime = false;

			request.OutwardDateTime = new TDDateTime[1];
			request.OutwardDateTime[0] = new TDDateTime(outwardDateTime);

			if	(outwardOnly)
			{
				request.ReturnDateTime = new TDDateTime[0];
			}
			else
			{
				request.ReturnDateTime = new TDDateTime[1];
				request.ReturnDateTime[0] = new TDDateTime(returnDateTime);
			}

			request.OriginLocation = new TDLocation();
			request.OriginLocation.Description = "OriginName";
			request.OriginLocation.Locality = "E00001234";
			request.OriginLocation.NaPTANs = new TDNaptan[3];
			request.OriginLocation.NaPTANs[0] = new TDNaptan();
			request.OriginLocation.NaPTANs[1] = new TDNaptan();
			request.OriginLocation.NaPTANs[2] = new TDNaptan();
			request.OriginLocation.NaPTANs[0].Naptan = "9200Naptan1";
			request.OriginLocation.NaPTANs[1].Naptan = "9200Naptan2";
			request.OriginLocation.NaPTANs[2].Naptan = "9200Naptan3";
			request.OriginLocation.NaPTANs[0].GridReference = new OSGridReference(123, 456); 
			request.OriginLocation.NaPTANs[1].GridReference = new OSGridReference(1234, 4567); 
			request.OriginLocation.NaPTANs[2].GridReference = new OSGridReference(1234, 4567); 
			request.OriginLocation.Toid = new string[] { "1234", "5678"};

			request.DestinationLocation = new TDLocation();
			request.DestinationLocation.Description = "DestinationName";
			request.DestinationLocation.Locality = "E00004321";
			request.DestinationLocation.NaPTANs = new TDNaptan[2];
			request.DestinationLocation.NaPTANs[0] = new TDNaptan();
			request.DestinationLocation.NaPTANs[1] = new TDNaptan();
			request.DestinationLocation.NaPTANs[0].Naptan = "9200Naptan1";
			request.DestinationLocation.NaPTANs[1].Naptan = "9200Naptan2";
			request.DestinationLocation.NaPTANs[0].GridReference = new OSGridReference(123, 456); 
			request.DestinationLocation.NaPTANs[1].GridReference = new OSGridReference(1234, 4567); 
			request.DestinationLocation.Toid = new string[] { "ABCDE", "FGHIJ", "KLMNO"};

			request.DirectFlightsOnly = true;
		
			request.ExtraCheckinTime = new TDDateTime(DateTime.MinValue);
			request.ViaLocationOutwardStopoverTime = new TDDateTime(DateTime.MinValue);
			request.ViaLocationReturnStopoverTime  = new TDDateTime(DateTime.MinValue);

			request.SelectedOperators = new string[0];
			request.UseOnlySpecifiedOperators = false;

			return request;

		}

		public ITDJourneyRequest InitialiseDefaultTrunkRequest(bool outwardOnly)
		{
			TDJourneyRequest request = new TDJourneyRequest();

			request.IsTrunkRequest = true;
			request.Modes = new ModeType[] { ModeType.Rail};

			request.IsReturnRequired = !outwardOnly;
			request.OutwardAnyTime = false;
			request.ReturnAnyTime = false;

			request.OutwardDateTime = new TDDateTime[1];
			request.OutwardDateTime[0] = new TDDateTime(outwardDateTime);

			if	(outwardOnly)
			{
				request.ReturnDateTime = new TDDateTime[0];
			}
			else
			{
				request.ReturnDateTime = new TDDateTime[1];
				request.ReturnDateTime[0] = new TDDateTime(returnDateTime);
			}

			request.OriginLocation = new TDLocation();
			request.OriginLocation.Description = "OriginName";
			request.OriginLocation.Locality = "E00001234";
			request.OriginLocation.NaPTANs = new TDNaptan[3];
			request.OriginLocation.NaPTANs[0] = new TDNaptan();
			request.OriginLocation.NaPTANs[1] = new TDNaptan();
			request.OriginLocation.NaPTANs[2] = new TDNaptan();
			request.OriginLocation.NaPTANs[0].Naptan = "9100Naptan1";
			request.OriginLocation.NaPTANs[1].Naptan = "9100Naptan2";
			request.OriginLocation.NaPTANs[2].Naptan = "9000Naptan3";
			request.OriginLocation.NaPTANs[0].GridReference = new OSGridReference(123, 456); 
			request.OriginLocation.NaPTANs[1].GridReference = new OSGridReference(1234, 4567); 
			request.OriginLocation.NaPTANs[2].GridReference = new OSGridReference(1234, 4567); 
			request.OriginLocation.Toid = new string[] { "1234", "5678"};

			request.DestinationLocation = new TDLocation();
			request.DestinationLocation.Description = "DestinationName";
			request.DestinationLocation.Locality = "E00004321";
			request.DestinationLocation.NaPTANs = new TDNaptan[2];
			request.DestinationLocation.NaPTANs[0] = new TDNaptan();
			request.DestinationLocation.NaPTANs[1] = new TDNaptan();
			request.DestinationLocation.NaPTANs[0].Naptan = "9100Naptan1";
			request.DestinationLocation.NaPTANs[1].Naptan = "9000Naptan2";
			request.DestinationLocation.NaPTANs[0].GridReference = new OSGridReference(123, 456); 
			request.DestinationLocation.NaPTANs[1].GridReference = new OSGridReference(1234, 4567); 
			request.DestinationLocation.Toid = new string[] { "ABCDE", "FGHIJ", "KLMNO"};

			request.PublicAlgorithm = PublicAlgorithmType.NoChanges;	
		
			request.ExtraCheckinTime = new TDDateTime(DateTime.MinValue);
			request.ViaLocationOutwardStopoverTime = new TDDateTime(DateTime.MinValue);
			request.ViaLocationReturnStopoverTime  = new TDDateTime(DateTime.MinValue);

			request.SelectedOperators = new string[0];
			request.UseOnlySpecifiedOperators = false;
		
			request.PublicViaLocations = new TDLocation[1];
			request.PublicViaLocations[0] = new TDLocation();
			request.PublicViaLocations[0].Description = "Reigate";
			request.PublicViaLocations[0].Locality = "E00001234";
			request.PublicViaLocations[0].Status = TDLocationStatus.Valid;
		
			request.PublicSoftViaLocations = new TDLocation[1];
			request.PublicSoftViaLocations[0] = new TDLocation();
			request.PublicSoftViaLocations[0].Description = "Redhill";
			request.PublicSoftViaLocations[0].Locality = "E00002341";
			request.PublicSoftViaLocations[0].Status = TDLocationStatus.Valid;

			request.PublicNotViaLocations = new TDLocation[1];
			request.PublicNotViaLocations[0] = new TDLocation();
			request.PublicNotViaLocations[0].Description = "Bath";
			request.PublicNotViaLocations[0].Locality = "E11111111";
			request.PublicNotViaLocations[0].Status = TDLocationStatus.Valid;

			return request;

		}


		public ITDJourneyRequest InitialiseDefaultCityToCityRequest(bool outwardOnly)
		{
			TDJourneyRequest request = new TDJourneyRequest();

			request.IsTrunkRequest = true;
			request.Modes = new ModeType[] { ModeType.Rail, ModeType.Rail, ModeType.Air};

			request.IsReturnRequired = !outwardOnly;
			request.OutwardAnyTime = false;
			request.ReturnAnyTime = false;

			request.OutwardDateTime = new TDDateTime[1];
			request.OutwardDateTime[0] = new TDDateTime(outwardDateTime);

			if	(outwardOnly)
			{
				request.ReturnDateTime = new TDDateTime[0];
			}
			else
			{
				request.ReturnDateTime = new TDDateTime[1];
				request.ReturnDateTime[0] = new TDDateTime(returnDateTime);
			}

			request.OriginLocation = new TDLocation();
			request.OriginLocation.Description = "OriginName";
			request.OriginLocation.Locality = "E00001234";
			request.OriginLocation.NaPTANs = new TDNaptan[3];
			request.OriginLocation.NaPTANs[0] = new TDNaptan();
			request.OriginLocation.NaPTANs[1] = new TDNaptan();
			request.OriginLocation.NaPTANs[2] = new TDNaptan();
			request.OriginLocation.NaPTANs[0].Naptan = "9100Naptan1";
			request.OriginLocation.NaPTANs[0].Locality =  "E00001234";
			request.OriginLocation.NaPTANs[0].Name = "OriginName";
			request.OriginLocation.NaPTANs[1].Naptan = "9000Naptan2";
			request.OriginLocation.NaPTANs[1].Locality =  "E00001234";
			request.OriginLocation.NaPTANs[1].Name = "OriginName";
			request.OriginLocation.NaPTANs[2].Naptan = "9200Naptan3";
			request.OriginLocation.NaPTANs[2].Locality =  "E00001234";
			request.OriginLocation.NaPTANs[2].Name = "OriginName";
			request.OriginLocation.NaPTANs[0].GridReference = new OSGridReference(123, 456); 
			request.OriginLocation.NaPTANs[1].GridReference = new OSGridReference(1234, 4567); 
			request.OriginLocation.NaPTANs[2].GridReference = new OSGridReference(1234, 4567); 
			request.OriginLocation.Toid = new string[] { "1234", "5678"};

			request.OriginLocation.CityInterchanges = new CityInterchange[3];

			request.OriginLocation.CityInterchanges[0] = new CityRailStation(request.OriginLocation.NaPTANs[0]);
			request.OriginLocation.CityInterchanges[1] = new CityCoachStation(request.OriginLocation.NaPTANs[1]);
			request.OriginLocation.CityInterchanges[2] = new CityAirport(new ModeType[] { ModeType.Rail },  true, true, request.OriginLocation.NaPTANs[2]);

			request.DestinationLocation = new TDLocation();
			request.DestinationLocation.Description = "DestinationName";
			request.DestinationLocation.Locality = "E00004321";
			request.DestinationLocation.NaPTANs = new TDNaptan[3];
			request.DestinationLocation.NaPTANs[0] = new TDNaptan();
			request.DestinationLocation.NaPTANs[1] = new TDNaptan();
			request.DestinationLocation.NaPTANs[2] = new TDNaptan();
			request.DestinationLocation.NaPTANs[0].Naptan = "9100Naptan3";
			request.DestinationLocation.NaPTANs[0].Locality = "E00004321";
			request.DestinationLocation.NaPTANs[0].Name = "DestinationName";
			request.DestinationLocation.NaPTANs[1].Naptan = "9200Naptan2";
			request.DestinationLocation.NaPTANs[1].Locality = "E00004321";
			request.DestinationLocation.NaPTANs[1].Name = "DestinationName";
			request.DestinationLocation.NaPTANs[2].Naptan = "9000Naptan4";
			request.DestinationLocation.NaPTANs[2].Locality = "E00004321";
			request.DestinationLocation.NaPTANs[2].Name = "DestinationName";
			request.DestinationLocation.NaPTANs[0].GridReference = new OSGridReference(123, 456); 
			request.DestinationLocation.NaPTANs[1].GridReference = new OSGridReference(1234, 4567); 
			request.DestinationLocation.NaPTANs[2].GridReference = new OSGridReference(1234, 4567); 
			request.DestinationLocation.Toid = new string[] { "ABCDE", "FGHIJ", "KLMNO"};

			request.DestinationLocation.CityInterchanges = new CityInterchange[3];

			request.DestinationLocation.CityInterchanges[0] = new CityRailStation(request.DestinationLocation.NaPTANs[0]);
			request.DestinationLocation.CityInterchanges[1] = new CityAirport(new ModeType[] { ModeType.Rail },  true, true, request.DestinationLocation.NaPTANs[1]);
			request.DestinationLocation.CityInterchanges[2] = new CityCoachStation(request.DestinationLocation.NaPTANs[2]);

			request.PublicAlgorithm = PublicAlgorithmType.NoChanges;	
			request.ExtraCheckinTime = new TDDateTime(DateTime.MinValue);

			request.SelectedOperators = new string[0];
			request.UseOnlySpecifiedOperators = false;

			return request;
		}
	}
}
