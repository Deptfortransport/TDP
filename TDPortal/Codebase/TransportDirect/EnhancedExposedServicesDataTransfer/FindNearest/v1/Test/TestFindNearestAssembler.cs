// *********************************************** 
// NAME                 : FindNearestAssembler.cs
// AUTHOR               : Russell Wilby
// DATE CREATED         : 02/02/2006
// DESCRIPTION  		: Test class for FindNearestAssembler
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/FindNearest/v1/Test/TestFindNearestAssembler.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:28   mturner
//Initial revision.
//
//   Rev 1.4   Mar 30 2006 14:48:30   mguney
//Fix for stream0018 merge.
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.3   Feb 24 2006 16:08:00   RWilby
//Fix for stream3129 merge.  Added TestMockCache to ServiceDiscovery. 
//
//   Rev 1.2   Feb 08 2006 14:41:28   RWilby
//Updated to achieve 100% code coverage
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.1   Feb 03 2006 11:53:32   RWilby
//Updated test and added more comments
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.0   Feb 02 2006 17:47:22   RWilby
//Initial revision.
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110

using System;
using System.Collections;
using NUnit.Framework;
using System.Diagnostics;

using TransportDirect.UserPortal.LocationService;
using TransportDirect.EnhancedExposedServices;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;  
using TransportDirect.EnhancedExposedServices.DataTransfer.FindNearest.V1;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.SessionManager;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.FindNearest.V1.Test
{
	/// <summary>
	/// Test class for FindNearestAssembler
	/// </summary>
	[TestFixture]
	public class TestFindNearestAssembler
	{
		public TestFindNearestAssembler()
		{
		}

		/// <summary>
		/// Method to initalise the simulated result
		/// </summary>
		[SetUp]		
		public void SetUp()
		{

			// Initialise the service discovery
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestFindNearestAssemblerInitialisation( ));
			IPropertyProvider property = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];

			Trace.Listeners.Remove("TDTraceListener");
		
			IEventPublisher[] customPublishers = new IEventPublisher[0];																								
			ArrayList errors = new ArrayList();
				
			try
			{
				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
			}
			catch (TDException e)
			{
				Assert.Fail(e.Message);
			}		
				
		}

		/// <summary>
		/// Test CreateNaptanPromimityArrayDT method
		/// </summary>
		[Test]
		public void TestCreateNaptanPromimityArrayDT()
		{
			int easting =1001;
			int northing = 2001;

			EnhancedExposedServices.DataTransfer.Common.V1.OSGridReference osGridReference = new EnhancedExposedServices.DataTransfer.Common.V1.OSGridReference();
			osGridReference.Easting = easting;
			osGridReference.Northing = northing;

			UserPortal.LocationService.OSGridReference lsOSGridReference = new TransportDirect.UserPortal.LocationService.OSGridReference(easting,northing);

			//Create TDNaptan array to convert to NaptanProximity Array
			TDNaptan[] tdNaptans = new TDNaptan[3];
			tdNaptans[0] = new TDAirportNaptan(new TDNaptan( "TestAirportNaptan",lsOSGridReference));
			tdNaptans[1] = new TDBusStopNaptan(new TDNaptan("TestBusStopNaptan",lsOSGridReference),"TestSmsCode");
			tdNaptans[2] = new TDRailNaptan(new TDNaptan("TestRailNaptan",lsOSGridReference),"TestCrsCode");

			//Invoke conversion method
			NaptanProximity[] naptanProximityArray = 
				FindNearestAssembler.CreateNaptanPromimityArrayDT(osGridReference,tdNaptans);
			
			//Check NaptanProximity Array data
			Assert.IsTrue((naptanProximityArray[0] as NaptanProximity).NaptanId == "TestAirportNaptan");
			Assert.IsTrue((naptanProximityArray[0] as NaptanProximity).GridReference.Easting == easting);
			Assert.IsTrue((naptanProximityArray[0] as NaptanProximity).GridReference.Northing == northing);
			Assert.IsNull((naptanProximityArray[0] as NaptanProximity).SMSCode );
			Assert.IsNull((naptanProximityArray[0] as NaptanProximity).CRSCode );
			Assert.IsNotNull((naptanProximityArray[0] as NaptanProximity).IATA) ;
			Assert.IsTrue((naptanProximityArray[0] as NaptanProximity).Distance == 0);
			Assert.IsNotNull((naptanProximityArray[0] as NaptanProximity).Name);
			Assert.IsTrue((naptanProximityArray[0] as NaptanProximity).Type == NaptanType.Airport);
			
			Assert.IsTrue((naptanProximityArray[1] as NaptanProximity).NaptanId == "TestBusStopNaptan");
			Assert.IsTrue((naptanProximityArray[1] as NaptanProximity).GridReference.Easting == easting);
			Assert.IsTrue((naptanProximityArray[1] as NaptanProximity).GridReference.Northing == northing);
			Assert.IsTrue((naptanProximityArray[1] as NaptanProximity).SMSCode == "TestSmsCode");
			Assert.IsNull((naptanProximityArray[1] as NaptanProximity).CRSCode);
			Assert.IsNull((naptanProximityArray[1] as NaptanProximity).IATA) ;
			Assert.IsTrue((naptanProximityArray[1] as NaptanProximity).Distance == 0);
			Assert.IsNotNull((naptanProximityArray[1] as NaptanProximity).Name);
			Assert.IsTrue((naptanProximityArray[1] as NaptanProximity).Type == NaptanType.BusStop);

			Assert.IsTrue((naptanProximityArray[2] as NaptanProximity).NaptanId == "TestRailNaptan");
			Assert.IsTrue((naptanProximityArray[2] as NaptanProximity).GridReference.Easting == easting);
			Assert.IsTrue((naptanProximityArray[2] as NaptanProximity).GridReference.Northing == northing);
			Assert.IsTrue((naptanProximityArray[2] as NaptanProximity).CRSCode == "TestCrsCode");
			Assert.IsNull((naptanProximityArray[2] as NaptanProximity).SMSCode);
			Assert.IsNull((naptanProximityArray[2] as NaptanProximity).IATA) ;
			Assert.IsTrue((naptanProximityArray[2] as NaptanProximity).Distance == 0);
			Assert.IsNotNull((naptanProximityArray[2] as NaptanProximity).Name);
			Assert.IsTrue((naptanProximityArray[2] as NaptanProximity).Type == NaptanType.Station);

			//Added to get 100% coverage with NCover.
			NaptanProximity TestNaptanProximityGridReference = new NaptanProximity();
			TestNaptanProximityGridReference.GridReference = (naptanProximityArray[2] as NaptanProximity).GridReference;

		}
	}

	public class TestFindNearestAssemblerInitialisation : IServiceInitialisation
	{		

		public void Populate(Hashtable serviceCache)
		{
			serviceCache.Add(ServiceDiscoveryKey.Crypto, new CryptoFactory() );
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			serviceCache.Add (ServiceDiscoveryKey.GisQuery, new TestMockGisQuery());
			serviceCache.Add( ServiceDiscoveryKey.Cache, new TransportDirect.UserPortal.SessionManager.TestMockCache());
		}	
	}
}
