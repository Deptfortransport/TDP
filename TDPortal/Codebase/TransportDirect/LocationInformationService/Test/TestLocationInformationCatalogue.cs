// *********************************************** 
// NAME			: TestLocationInformationCatalogue.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 18/10/07
// DESCRIPTION 	: Test class for Location Information catalogue. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationInformationService/Test/TestLocationInformationCatalogue.cs-arc  $
//
//   Rev 1.0   Nov 28 2007 14:56:48   mturner
//Initial revision.
//
//   Rev 1.2   Nov 01 2007 13:48:42   mmodi
//Corrected change event
//Resolution for 4518: Del 9.8 - Air Departure Boards
//
//   Rev 1.1   Oct 25 2007 17:57:56   mmodi
//Updated with AirportDataProvider service
//Resolution for 4518: Del 9.8 - Air Departure Boards
//
//   Rev 1.0   Oct 25 2007 15:40:42   mmodi
//Initial revision.
//Resolution for 4518: Del 9.8 - Air Departure Boards
//

using System;
using System.Xml;
using System.Globalization;
using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using System.Collections;
using System.Diagnostics;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.ExternalLinkService;
using ADPTestMockAirDataProvider = TransportDirect.UserPortal.AirDataProvider.TestMockAirDataProvider;
using System.IO;

namespace TransportDirect.UserPortal.LocationInformationService.Test
{
	/// <summary>
	/// Test class testing the LocationInformationCatalogue
	/// </summary>
	[TestFixture]
	public class TestLocationInformationCatalogue
	{
		#region Private Properties

		private string TEST_DATA = Directory.GetCurrentDirectory() + "\\LocationInformation\\LocationInformationData1.xml";
		private string TEST_DATA2 = Directory.GetCurrentDirectory() + "\\LocationInformation\\LocationInformationData2.xml";
		private string CLEARDOWN_SCRIPT = Directory.GetCurrentDirectory() + "\\LocationInformation\\LocationInformationCatalogueClearDown.sql";
		private const string connectionString = "Server=.;Initial Catalog=TransientPortal;Trusted_Connection=true";
		private TestDataManager tm;

		#endregion

		#region Test Methods

		/// <summary>
		/// Initialisation method sets up service discovery
		/// </summary>
		[SetUp]
		public void Init()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestLocationInformationInitialization());
		}

		/// <summary>
		/// Tear down method removes test data
		/// </summary>
		[TearDown]
		public void Teardown() 
		{
			tm.ClearData();
		}

		/// <summary>
		/// Tests LocationInformationCatalogue for a naptan with all links
		/// </summary>
		[Test]
		public void TestGetLocationInformationLinks()
		{		
			tm = new TestDataManager(TEST_DATA, CLEARDOWN_SCRIPT, connectionString);
			tm.LoadData();

			InitialiseExternalLinksService();
			LocationInformationCatalogue refData = GetLocationInformationCatalogue();
			LocationInformation li = refData.GetLocationInformation("9200ABC");
			
			Assert.IsTrue( (li.DepartureLink != null) && (li.ArrivalLink != null) && (li.InformationLink != null));
			Assert.IsTrue(li.DepartureLink.GetType() == typeof(ExternalLinkDetail));
			Assert.IsTrue(li.ArrivalLink.GetType() == typeof(ExternalLinkDetail));
			Assert.IsTrue(li.InformationLink.GetType() == typeof(ExternalLinkDetail));
		}

		/// <summary>
		/// Tests LocationInformationCatalogue for naptan with a single link
		/// </summary>
		[Test]
		public void TestGetZonalServiceLinksSingleLink()
		{
			TestMockDataChangeNotification dataChangeNotification = AddDataChangeNotification();

			tm = new TestDataManager(TEST_DATA, CLEARDOWN_SCRIPT, connectionString);
			tm.LoadData();

			InitialiseExternalLinksService();
			LocationInformationCatalogue refData = GetLocationInformationCatalogue();
			LocationInformation li = refData.GetLocationInformation("9200DEF");

			Assert.IsTrue( li != null, "Expected a location information object to be returned. Received null");
			Assert.IsTrue( (li.DepartureLink == null) && (li.ArrivalLink != null) && (li.InformationLink == null), "Expected only Arrival link to be populated" );

			ExternalLinkDetail link1 = li.ArrivalLink;

			Assert.IsTrue(link1.LinkText.Length != 0, "Expected the Arrival link in location information object to have link text");
			Assert.IsTrue(link1.Url.Length != 0, "Expected the Arrival link in location information object to have a url");
		}

		/// <summary>
		/// Tests LocationInformationCatalogue for a naptan with no published links
		/// </summary>
		[Test]
		public void TestGetZonalServiceLinksSingleLinkNotPublished()
		{
			TestMockDataChangeNotification dataChangeNotification = AddDataChangeNotification();

			tm = new TestDataManager(TEST_DATA, CLEARDOWN_SCRIPT, connectionString);
			tm.LoadData();

			InitialiseExternalLinksService();
			LocationInformationCatalogue refData = GetLocationInformationCatalogue();
			LocationInformation li = refData.GetLocationInformation("9200GHI");

			Assert.IsTrue(li == null, "Expected item to be null, but item was returned");
		}

		/// <summary>
		/// Tests LocationInformationCatalogue for unknown naptan 
		/// </summary>
		[Test]
		public void TestGetZonalServiceLinksSingleLinkNoZonalDataAvailable()
		{
			TestMockDataChangeNotification dataChangeNotification = AddDataChangeNotification();

			tm = new TestDataManager(TEST_DATA, CLEARDOWN_SCRIPT, connectionString);
			tm.LoadData();

			InitialiseExternalLinksService();
			LocationInformationCatalogue refData = GetLocationInformationCatalogue();
			LocationInformation li = refData.GetLocationInformation("UNKNOWN");

			Assert.IsTrue(li == null, "Expected item to be null, but item was returned");
		}

		/// <summary>
		/// Tests that the LocationInformationCatalogue reloads data cache after DataChangeNotification event
		/// </summary>
		[Test]
		public void TestNotificationService()
		{
			TestMockDataChangeNotification dataChangeNotification = AddDataChangeNotification();

			tm = new TestDataManager(TEST_DATA, CLEARDOWN_SCRIPT, connectionString);
			tm.LoadData();

			InitialiseExternalLinksService();
			LocationInformationCatalogue refData = GetLocationInformationCatalogue();
			LocationInformation li = refData.GetLocationInformation("9200JKL");

			string oldUrlText = li.DepartureLink.LinkText; // Expecting departure link to be loaded

			tm.DataFile = TEST_DATA2;
			tm.LoadData();
		
			//Manually raise a change notification event
			dataChangeNotification.RaiseChangedEvent("ExternalLinks");
			dataChangeNotification.RaiseChangedEvent("LocationInformationCatalogue");			
			refData = (LocationInformationCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationInformation];

			li = refData.GetLocationInformation("9200JKL");

			Assert.IsTrue(li.DepartureLink.Url != oldUrlText, "Expected the url to be different, but they are the same");
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Method retrieves and returns LocationInformationCatalogue from ServiceDiscovery
		/// </summary>
		/// <returns></returns>
		private LocationInformationCatalogue GetLocationInformationCatalogue()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.LocationInformation, new LocationInformationFactory());
			return (LocationInformationCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationInformation];
		}

		/// <summary>
		/// Method initialises test external links service
		/// </summary>
		private void InitialiseExternalLinksService()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.ExternalLinkService, new ExternalLinksFactory());
		}

		/// <summary>
		/// Method sets up DataChangeNotification service.
		/// </summary>
		/// <returns></returns>
		private TestMockDataChangeNotification AddDataChangeNotification()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.DataChangeNotification, new TestMockDataChangeNotification());
			return (TestMockDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];
		}

		#endregion
	}

	#region initialisation class

	/// <summary>
	/// Initialisation class 
	/// </summary>
	public class TestLocationInformationInitialization: IServiceInitialisation
	{
		private string airDataProvideDataFile = Directory.GetCurrentDirectory() + @"\LocationInformation\mockAirDataProviderData.xml";

		/// <summary>
		/// Populates sevice cache with services needed 
		/// </summary>
		/// <param name="serviceCache">Cache to populate.</param>
		public void Populate(Hashtable serviceCache)
		{
			// Add cryptographic scheme
			serviceCache.Add(ServiceDiscoveryKey.Crypto,  new CryptoFactory() );

			// Enable PropertyService					
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			
			// Enable ExternalLinks 
			serviceCache.Add(ServiceDiscoveryKey.ExternalLinkService, new ExternalLinksFactory());

			// Enable AirDataProvider			
			serviceCache.Add(ServiceDiscoveryKey.AirDataProvider, new ADPTestMockAirDataProvider(airDataProvideDataFile));

			// Enable logging service.
			ArrayList errors = new ArrayList();
			try
			{    
				IEventPublisher[] customPublishers = new IEventPublisher[0];

				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
			}
			catch (TDException)
			{
				foreach(string error in errors)
				{
					Console.WriteLine(error);
				}
				throw;
			}					

		}
		#endregion

	}
}
