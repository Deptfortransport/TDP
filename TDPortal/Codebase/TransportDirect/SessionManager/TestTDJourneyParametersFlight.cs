// ***********************************************
// NAME 		: TestTDJourneyParametersFlight.cs
// AUTHOR 		: Jonathan George
// DATE CREATED : 10/06/2004
// DESCRIPTION 	: Tests the TDJourneyParametersFlight object
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TestTDJourneyParametersFlight.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:46   mturner
//Initial revision.
//
//   Rev 1.5   Feb 08 2006 13:33:48   mtillett
//Add Cache to service discovery
//
//   Rev 1.4   Feb 04 2005 11:14:02   RScott
//Updated assertions to assert
//
//   Rev 1.3   Feb 03 2005 09:27:46   RScott
//NUnit tests updated
//
//   Rev 1.2   Jul 08 2004 15:14:50   jgeorge
//Review comments
//
//   Rev 1.1   Jun 17 2004 16:36:50   jgeorge
//Added tests
//
//   Rev 1.0   Jun 10 2004 11:07:48   jgeorge
//Initial revision.

using System;
using System.IO;
using NUnit.Framework;
using System.Collections;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using ADPTestMockAirDataProvider = TransportDirect.UserPortal.AirDataProvider.TestMockAirDataProvider;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Tests the TDJourneyParametersFlight object
	/// </summary>
	[TestFixture]
	public class TestTDJourneyParametersFlight
	{
		/// <summary>
		/// Initialisation in setup method called before every test method
		/// </summary>
		[TestFixtureSetUp]
		public void Init() 
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new AirDataProviderTestInitialisation());
		}

		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TestFixtureTearDown]
		public void Dispose() { 
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}

		/// <summary>
		/// Tests that the object is created with expected default values, and
		/// that when the initialise method is called, the values are reset to those
		/// expected. Only values specific to the Flight parameters are tested, as
		/// it is assumed that the base class works correctly.
		/// </summary>
		[Test]
		public void TestCreateAndInitialise()
		{
			bool expectedDirectFlightsOnly = true;
			bool expectedOnlyUseSpecifiedOperators = false;
			int expectedSelectedOperatorsCount = 0;
			int expectedExtraCheckInTime = 0;
			int expectedOutwardStopover = 0;
			int expectedReturnStopover = 0;
			AirRegion expectedOriginSelectedRegion = null;
			int expectedOriginSelectedAirportsCount = 0;
			AirRegion expectedDestinationSelectedRegion = null;
			int expectedDestinationSelectedAirports = 0;
			Airport expectedViaSelectedAirport = null;
			TDLocation expectedViaLocation = new TDLocation();
			bool expectedOutwardAnyTime = true;
			bool expectedReturnAnyTime = false;
			string expectedOutwardHour = string.Empty;
			string expectedOutwardMinute = string.Empty;

			TDJourneyParametersFlight journeyParams = new TDJourneyParametersFlight();
	
			Assert.AreEqual(expectedDirectFlightsOnly, journeyParams.DirectFlightsOnly, "DirectFlightsOnly not as expected after initialisation");
			Assert.AreEqual(expectedOnlyUseSpecifiedOperators, journeyParams.OnlyUseSpecifiedOperators, "OnlyUseSpecifiedOperators not as expected after initialisation");
			Assert.AreEqual(expectedSelectedOperatorsCount, journeyParams.SelectedOperators.Length,"SelectedOperators not as expected after initialisation");
			Assert.AreEqual(expectedExtraCheckInTime, journeyParams.ExtraCheckInTime, "ExtraCheckInTime not as expected after initialisation");
			Assert.AreEqual(expectedOutwardStopover, journeyParams.OutwardStopover, "OutwardStopover not as expected after initialisation");
			Assert.AreEqual(expectedReturnStopover, journeyParams.ReturnStopover, "ReturnStopover not as expected after initialisation");
			Assert.AreEqual(expectedOriginSelectedRegion, journeyParams.OriginSelectedRegion(), "OriginSelectedRegion not as expected after initialisation");
			Assert.AreEqual(expectedOriginSelectedAirportsCount, journeyParams.OriginSelectedAirports().Length, "OriginSelectedAirports not as expected after initialisation");
			Assert.AreEqual(expectedDestinationSelectedRegion, journeyParams.DestinationSelectedRegion(), "DestinationSelectedRegion not as expected after initialisation");
			Assert.AreEqual(expectedDestinationSelectedAirports, journeyParams.DestinationSelectedAirports().Length, "DestinationSelectedAirports not as expected after initialisation");
			Assert.AreEqual(expectedViaSelectedAirport, journeyParams.ViaSelectedAirport, "ViaSelectedAirport not as expected after initialisation");
			Assert.AreEqual(expectedViaLocation.Description, journeyParams.ViaLocation.Description, "ViaLocation.Description not as expected after initialisation");
			Assert.AreEqual(expectedViaLocation.NaPTANs.Length, journeyParams.ViaLocation.NaPTANs.Length, "ViaLocation.NaPTANs.Length not as expected after initialisation");
			Assert.AreEqual(expectedOutwardAnyTime, journeyParams.OutwardAnyTime, "OutwardAnyTime not as expected after initialisation");
			Assert.AreEqual(expectedReturnAnyTime, journeyParams.ReturnAnyTime, "ReturnAnyTime not as expected after initialisation");
			Assert.AreEqual(expectedOutwardHour, journeyParams.OutwardHour, "OutwardHour not as expected after initialisation");
			Assert.AreEqual(expectedOutwardMinute, journeyParams.OutwardMinute, "OutwardMinute not as expected after initialisation");

		}

		/// <summary>
		/// Tests the ViaSelectedAirport and ViaLocation properties
		/// </summary>
		[Test]
		public void TestVia()
		{
			// When ViaSelectedAirport is set, the ViaLocation property should contain the
			// same naptans.

			//030105 - ViaLocation has been changed to return only the GlobalNaptan for a given airport
			//hence test altered

			IAirDataProvider airData = (IAirDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];
			Airport viaAirport = airData.GetAirport("LHR");
			Assert.IsNotNull(viaAirport, "Error calling airData.GetAirport(\"LHR\") - check data file is as expected.");
			
			TDJourneyParametersFlight journeyParams = new TDJourneyParametersFlight();
			journeyParams.ViaSelectedAirport = viaAirport;

			// Check the naptans in the ViaLocation
			TDLocation viaLocation = journeyParams.ViaLocation;

			ArrayList actualNaptans = new ArrayList();
			ArrayList expectedNaptans = new ArrayList();
		
			Assert.AreEqual(1, viaLocation.NaPTANs.Length, "Number of naptans in ViaLocation property differs from ViaSelectedAirport");
			Assert.AreEqual(TDLocationStatus.Valid, viaLocation.Status, "ViaLocation property doesn't have its status set to valid");

			// Now try it the other way round
			journeyParams = new TDJourneyParametersFlight();

			viaLocation = new TDLocation();
			viaLocation.NaPTANs = new TDNaptan[viaAirport.Naptans.Length];
			for (int index = 0; index < viaAirport.Naptans.Length; index++)
				viaLocation.NaPTANs[index] = new TDNaptan(viaAirport.Naptans[index], new OSGridReference());
			viaLocation.Status = TDLocationStatus.Valid;


			journeyParams.ViaLocation = viaLocation;
			viaAirport = journeyParams.ViaSelectedAirport;
			
			Assert.AreEqual(viaAirport.Naptans.Length, viaLocation.NaPTANs.Length, "Number of naptans in ViaSelectedAirport property differs from ViaLocation");
			
			// Now verify that the naptans match
			expectedNaptans = new ArrayList(viaLocation.NaPTANs);
			actualNaptans = new ArrayList(viaAirport.Naptans);
			foreach (TDNaptan curr in expectedNaptans)
				Assert.IsTrue(actualNaptans.Contains(curr.Naptan), "Naptans in ViaSelectedAirport property differ from ViaLocation");

		}

		/// <summary>
		/// Tests the OriginLocation property - similar to via location, except it is possible
		/// to have multiple airports
		/// </summary>
		[Test]
		public void TestOriginLocation()
		{
			IAirDataProvider airData = (IAirDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];
			Airport viaAirport = airData.GetAirport("LHR");

			// Use four airports
			Airport[] expectedAirports = new Airport[] { airData.GetAirport("INV"), airData.GetAirport("LGW"), airData.GetAirport("LHR"), airData.GetAirport("ABZ") };

			// Heathrow and Gatwick have 3 terminals each, but we will not specify all of them in the
			// location object we will supply to the OriginLocation property. When the property is
			// set, the SelectedOriginAirports property will be updated, as will the Naptans list.
			TDLocation newLocation = new TDLocation();
			newLocation.NaPTANs = new TDNaptan[5];
			newLocation.NaPTANs[0] = new TDNaptan(expectedAirports[0].Naptans[0], new OSGridReference());
			newLocation.NaPTANs[1] = new TDNaptan(expectedAirports[1].Naptans[1], new OSGridReference());
			newLocation.NaPTANs[2] = new TDNaptan(expectedAirports[2].Naptans[0], new OSGridReference());
			newLocation.NaPTANs[3] = new TDNaptan(expectedAirports[2].Naptans[2], new OSGridReference());
			newLocation.NaPTANs[4] = new TDNaptan(expectedAirports[3].Naptans[0], new OSGridReference());
			newLocation.Status = TDLocationStatus.Valid;

			TDJourneyParametersFlight journeyParams = new TDJourneyParametersFlight();
			journeyParams.OriginLocation = newLocation;

			// Firstly, check that the OriginAirports property contains the expected airports
			Assert.IsNull(journeyParams.OriginSelectedRegion(), "OriginSelectedRegion property was not as expected");
			Assert.IsNotNull(journeyParams.OriginSelectedAirports(), "OriginSelectedAirports property was null");
			Assert.AreEqual(expectedAirports.Length, journeyParams.OriginSelectedAirports().Length, "OriginSelectedAirports property didn't contain the expected airports");

			// Now compare the actual airports
			ArrayList actualAirports = new ArrayList(journeyParams.OriginSelectedAirports());
			foreach (Airport a in expectedAirports)
				Assert.IsTrue(actualAirports.Contains(a), "Airport " + a.IATACode + " missing from OriginSelectedAirports");

			// Now check that the DestinationLocation contains all the naptans from all the airports
			ArrayList actualNaptans = new ArrayList();
			foreach (TDNaptan naptan in journeyParams.OriginLocation.NaPTANs)
				actualNaptans.Add(naptan.Naptan);
			foreach (Airport a in expectedAirports)
			{
				string naptan = a.GlobalNaptan;
				Assert.IsTrue(actualNaptans.Contains(naptan), "Naptan " + naptan + " missing from DestinationLocation");
			}
		}

		/// <summary>
		/// Tests the DestinationLocation  property in exactly the same way as for TestOriginLocation
		/// </summary>
		[Test]
		public void TestDestinationLocation()
		{
			IAirDataProvider airData = (IAirDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];

			// Use four airports
			Airport[] expectedAirports = new Airport[] { airData.GetAirport("INV"), airData.GetAirport("LGW"), airData.GetAirport("LHR"), airData.GetAirport("ABZ") };

			// Heathrow and Gatwick have 3 terminals each, but we will not specify all of them in the
			// location object we will supply to the OriginLocation property. When the property is
			// set, the SelectedOriginAirports property will be updated, as will the Naptans list.
			TDLocation newLocation = new TDLocation();
			newLocation.NaPTANs = new TDNaptan[5];
			newLocation.NaPTANs[0] = new TDNaptan(expectedAirports[0].Naptans[0], new OSGridReference());
			newLocation.NaPTANs[1] = new TDNaptan(expectedAirports[1].Naptans[1], new OSGridReference());
			newLocation.NaPTANs[2] = new TDNaptan(expectedAirports[2].Naptans[0], new OSGridReference());
			newLocation.NaPTANs[3] = new TDNaptan(expectedAirports[2].Naptans[2], new OSGridReference());
			newLocation.NaPTANs[4] = new TDNaptan(expectedAirports[3].Naptans[0], new OSGridReference());
			newLocation.Status = TDLocationStatus.Valid;

			TDJourneyParametersFlight journeyParams = new TDJourneyParametersFlight();
			journeyParams.DestinationLocation = newLocation;

			Assert.IsNull(journeyParams.DestinationSelectedRegion(),"DestinationSelectedRegion property was not as expected");
			Assert.IsNotNull(journeyParams.DestinationSelectedAirports(), "DestinationSelectedAirports property was null");
			Assert.AreEqual(expectedAirports.Length, journeyParams.DestinationSelectedAirports().Length, "DestinationSelectedAirports property didn't contain the expected airports");

			// Now compare the actual airports
			ArrayList actualAirports = new ArrayList(journeyParams.DestinationSelectedAirports());
			foreach (Airport a in expectedAirports)
				Assert.IsTrue(actualAirports.Contains(a), "Airport " + a.IATACode + " missing from DestinationSelectedAirports");

			// Now check that the DestinationLocation contains all the naptans from all the airports
			ArrayList actualNaptans = new ArrayList();
			foreach (TDNaptan naptan in journeyParams.DestinationLocation.NaPTANs)
				actualNaptans.Add(naptan.Naptan);
			foreach (Airport a in expectedAirports)
			{
				string naptan = a.GlobalNaptan;
				Assert.IsTrue(actualNaptans.Contains(naptan), "Naptan " + naptan + " missing from DestinationLocation");
			}
		}

		/// <summary>
		/// Tests the SetOutwardDetails method
		/// </summary>
		[Test]
		public void TestSetOutwardDetails()
		{
			IAirDataProvider airData = (IAirDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];

			// First, test with a region and no airports. The location should then contain all airports
			// for the region
			AirRegion chosenRegion = airData.GetRegion(2);
			Airport[] expectedAirports = (Airport[])airData.GetRegionAirports(chosenRegion.Code).ToArray(typeof(Airport));
			ArrayList expectedNaptans = new ArrayList();

			foreach (Airport a in expectedAirports)
			{
				expectedNaptans.Add(a.GlobalNaptan);
			}
			
			TDJourneyParametersFlight journeyParams = new TDJourneyParametersFlight();
			journeyParams.SetOriginDetails(chosenRegion, null);

			Assert.AreEqual(chosenRegion, journeyParams.OriginSelectedRegion(), "OriginSelectedRegion property is incorrect");
			Assert.AreEqual(0, journeyParams.OriginSelectedAirports().Length, "OriginSelectedAirports property is incorrect");
			// Validate the OriginLocation property
			Assert.AreEqual(TDLocationStatus.Valid, journeyParams.OriginLocation.Status, "OriginLocation property is not valid");
			Assert.AreEqual(expectedNaptans.Count, journeyParams.OriginLocation.NaPTANs.Length, "OriginLocation property contains the wrong number of naptans");
			// Check the naptans
			ArrayList actualNaptans = new ArrayList();
			foreach (TDNaptan naptan in journeyParams.OriginLocation.NaPTANs)
				actualNaptans.Add(naptan.Naptan);
			foreach (string s in expectedNaptans)
				Assert.IsTrue(actualNaptans.Contains(s), "Expected naptan " + s + " missing from OriginLocation.NaPTANs");

			// Now test with no region but some airports
			expectedAirports = new Airport[] { airData.GetAirport("INV"), airData.GetAirport("LGW"), airData.GetAirport("LHR"), airData.GetAirport("ABZ") };
			expectedNaptans = new ArrayList();

			foreach (Airport a in expectedAirports)
			{
				expectedNaptans.Add(a.GlobalNaptan);
			}

			journeyParams.SetOriginDetails(expectedAirports);

			Assert.IsNull(journeyParams.OriginSelectedRegion(), "OriginSelectedRegion property is incorrect");
			Assert.AreEqual(4, journeyParams.OriginSelectedAirports().Length, "OriginSelectedAirports property is incorrect");
			// Validate the OriginLocation property
			Assert.AreEqual(TDLocationStatus.Valid, journeyParams.OriginLocation.Status, "OriginLocation property is not valid");
			Assert.AreEqual(expectedNaptans.Count, journeyParams.OriginLocation.NaPTANs.Length, "OriginLocation property contains the wrong number of naptans");
			// Check the naptans
			actualNaptans = new ArrayList();
			foreach (TDNaptan naptan in journeyParams.OriginLocation.NaPTANs)
				actualNaptans.Add(naptan.Naptan);
			foreach (string s in expectedNaptans)
				Assert.IsTrue(actualNaptans.Contains(s), "Expected naptan " + s + " missing from OriginLocation.NaPTANs");

			// Finally test with a region and some airports
			chosenRegion = airData.GetRegion(2);
			expectedAirports = new Airport[] { airData.GetAirport("LHR"), airData.GetAirport("LGW") };
			expectedNaptans = new ArrayList();

			foreach (Airport a in expectedAirports)
			{
				expectedNaptans.Add(a.GlobalNaptan);
			}
			journeyParams.SetOriginDetails(chosenRegion, expectedAirports);

			Assert.AreEqual(chosenRegion, journeyParams.OriginSelectedRegion(), "OriginSelectedRegion property is incorrect");
			Assert.AreEqual(2, journeyParams.OriginSelectedAirports().Length, "OriginSelectedAirports property is incorrect");
			// Validate the OriginLocation property
			Assert.AreEqual(TDLocationStatus.Valid, journeyParams.OriginLocation.Status, "OriginLocation property is not valid");
			Assert.AreEqual(expectedNaptans.Count, journeyParams.OriginLocation.NaPTANs.Length, "OriginLocation property contains the wrong number of naptans");
			// Check the naptans
			actualNaptans = new ArrayList();
			foreach (TDNaptan naptan in journeyParams.OriginLocation.NaPTANs)
				actualNaptans.Add(naptan.Naptan);
			foreach (string s in expectedNaptans)
				Assert.IsTrue(actualNaptans.Contains(s), "Expected naptan " + s + " missing from OriginLocation.NaPTANs");
		}

		/// <summary>
		/// Tests the SetReturnDetails method
		/// </summary>
		[Test]
		public void TestSetReturnDetails()
		{
			IAirDataProvider airData = (IAirDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];

			// First, test with a region and no airports. The location should then contain all airports
			// for the region
			AirRegion chosenRegion = airData.GetRegion(2);
			Airport[] expectedAirports = (Airport[])airData.GetRegionAirports(chosenRegion.Code).ToArray(typeof(Airport));
			ArrayList expectedNaptans = new ArrayList();

			foreach (Airport a in expectedAirports)
			{
				expectedNaptans.Add(a.GlobalNaptan);
			}
			TDJourneyParametersFlight journeyParams = new TDJourneyParametersFlight();
			journeyParams.SetDestinationDetails(chosenRegion, null);

			Assert.AreEqual(chosenRegion, journeyParams.DestinationSelectedRegion(), "DestinationSelectedRegion property is incorrect");
			Assert.AreEqual(0, journeyParams.DestinationSelectedAirports().Length, "DestinationSelectedAirports property is incorrect");
			// Validate the DestinationLocation property
			Assert.AreEqual(TDLocationStatus.Valid, journeyParams.DestinationLocation.Status, "DestinationLocation property is not valid");
			Assert.AreEqual(expectedNaptans.Count, journeyParams.DestinationLocation.NaPTANs.Length, "DestinationLocation property contains the wrong number of naptans");
			// Check the naptans
			ArrayList actualNaptans = new ArrayList();
			foreach (TDNaptan naptan in journeyParams.DestinationLocation.NaPTANs)
				actualNaptans.Add(naptan.Naptan);
			foreach (string s in expectedNaptans)
				Assert.IsTrue(actualNaptans.Contains(s), "Expected naptan " + s + " missing from DestinationLocation.NaPTANs");

			// Now test with no region but some airports
			expectedAirports = new Airport[] { airData.GetAirport("INV"), airData.GetAirport("LGW"), airData.GetAirport("LHR"), airData.GetAirport("ABZ") };
			expectedNaptans = new ArrayList();

			foreach (Airport a in expectedAirports)
			{
				expectedNaptans.Add(a.GlobalNaptan);
			}
			journeyParams.SetDestinationDetails(expectedAirports);

			Assert.IsNull(journeyParams.DestinationSelectedRegion(), "DestinationSelectedRegion property is incorrect");
			Assert.AreEqual(4, journeyParams.DestinationSelectedAirports().Length, "DestinationSelectedAirports property is incorrect");
			// Validate the DestinationLocation property
			Assert.AreEqual(TDLocationStatus.Valid, journeyParams.DestinationLocation.Status, "DestinationLocation property is not valid");
			Assert.AreEqual(expectedNaptans.Count, journeyParams.DestinationLocation.NaPTANs.Length, "DestinationLocation property contains the wrong number of naptans");
			// Check the naptans
			actualNaptans = new ArrayList();
			foreach (TDNaptan naptan in journeyParams.DestinationLocation.NaPTANs)
				actualNaptans.Add(naptan.Naptan);
			foreach (string s in expectedNaptans)
				Assert.IsTrue(actualNaptans.Contains(s), "Expected naptan " + s + " missing from DestinationLocation.NaPTANs");

			// Finally test with a region and some airports
			chosenRegion = airData.GetRegion(2);
			expectedAirports = new Airport[] { airData.GetAirport("LHR"), airData.GetAirport("LGW") };
			expectedNaptans = new ArrayList();

			foreach (Airport a in expectedAirports)
			{
				expectedNaptans.Add(a.GlobalNaptan);
			}
			journeyParams.SetDestinationDetails(chosenRegion, expectedAirports);

			Assert.AreEqual(chosenRegion, journeyParams.DestinationSelectedRegion(), "DestinationSelectedRegion property is incorrect");
			Assert.AreEqual(2, journeyParams.DestinationSelectedAirports().Length, "DestinationSelectedAirports property is incorrect");
			// Validate the DestinationLocation property
			Assert.AreEqual(TDLocationStatus.Valid, journeyParams.DestinationLocation.Status, "DestinationLocation property is not valid");
			Assert.AreEqual(expectedNaptans.Count, journeyParams.DestinationLocation.NaPTANs.Length, "DestinationLocation property contains the wrong number of naptans");
			// Check the naptans
			actualNaptans = new ArrayList();
			foreach (TDNaptan naptan in journeyParams.DestinationLocation.NaPTANs)
				actualNaptans.Add(naptan.Naptan);
			foreach (string s in expectedNaptans)
				Assert.IsTrue(actualNaptans.Contains(s), "Expected naptan " + s + " missing from DestinationLocation.NaPTANs");
		}


	}

	#region Initialisation class

	/// <summary>
	/// Initialisation class for air data provider test
	/// </summary>
	public class AirDataProviderTestInitialisation : IServiceInitialisation
	{
		public AirDataProviderTestInitialisation()
		{
		}

		// Need to add a file property provider
		// Enable PropertyService
		public void Populate(Hashtable serviceCache)
		{
			string dataFile = Directory.GetCurrentDirectory() + @"\SessionManager\mockAirDataProviderData.xml";
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			serviceCache.Add(ServiceDiscoveryKey.AirDataProvider, new ADPTestMockAirDataProvider(dataFile));
			serviceCache.Add(ServiceDiscoveryKey.DataServices, new TestMockDataServicesFactory());
			serviceCache.Add(ServiceDiscoveryKey.GisQuery, new TestStubGisQuery());
			serviceCache.Add(ServiceDiscoveryKey.Cache, new TestJourneyControlMockCache() );
		}

	}

	#endregion

}
