// *********************************************** 
// NAME                 : TestZonalServiceCatalogue.cs
// AUTHOR               : Ken Josling
// DATE CREATED         : 18/08/2005 
// DESCRIPTION			: Unit Test class for TestZonalServiceCatalogue
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ZonalServices/Test/TestZonalServiceCatalogue.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 13:03:24   mturner
//Initial revision.
//
//   Rev 1.4   Feb 17 2006 17:17:26   halkatib
//Changed test data source string to use the directory.getcurrentdirectory method instead of a hard coded string. 
//
//   Rev 1.3   Feb 09 2006 13:56:02   jbroome
//FX Cop update.
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.2   Feb 09 2006 10:37:54   jbroome
//Added missing documentation comments
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.1   Jan 20 2006 10:23:22   tolomolaiye
//Code review updates
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.0   Dec 19 2005 12:13:22   kjosling
//Initial revision.

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
using System.IO;

namespace TransportDirect.UserPortal.ZonalServices.Test
{
	/// <summary>
	/// Test class testing the ZonalServiceCatalogue
	/// </summary>
	[TestFixture]
	public class TestZonalServiceCatalogue
	{

		#region Private Properties

		private string TEST_DATA = Directory.GetCurrentDirectory() + "\\ZonalServices\\ZonalServiceData1.xml";
		private string TEST_DATA2 = Directory.GetCurrentDirectory() + "\\ZonalServices\\ZonalServiceData2.xml";
		private string CLEARDOWN_SCRIPT = Directory.GetCurrentDirectory() + "\\ZonalServices\\ClearDown.sql";
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
			TDServiceDiscovery.Init(new TestZonalServiceInitialization());
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
		/// Tests ZonalServiceCatalogue for naptan with multiple Zonal Services link
		/// </summary>
		[Test]
		public void TestGetZonalServiceLinksMultipleLinks()
		{		
			tm = new TestDataManager(TEST_DATA, CLEARDOWN_SCRIPT, connectionString);
			tm.LoadData();

			InitialiseExternalLinksService();
			ZonalServiceCatalogue refData = GetZonalServiceCatalogue();
			ExternalLinkDetail[] links = refData.GetZonalServiceLinks("9100ABC");

			Assert.IsTrue(links.Length ==2);
			Assert.IsTrue(links[0].GetType() == typeof(ExternalLinkDetail));
			Assert.IsTrue(links[1].GetType() == typeof(ExternalLinkDetail));
		}

		/// <summary>
		/// Tests ZonalServiceCatalogue for naptan with single Zonal Services link
		/// </summary>
		[Test]
		public void TestGetZonalServiceLinksSingleLink()
		{
			TestMockDataChangeNotification dataChangeNotification = AddDataChangeNotification();

			tm = new TestDataManager(TEST_DATA, CLEARDOWN_SCRIPT, connectionString);
			tm.LoadData();

			InitialiseExternalLinksService();
			ZonalServiceCatalogue refData = GetZonalServiceCatalogue();
			ExternalLinkDetail[] links = refData.GetZonalServiceLinks("9100DEF");

			Assert.IsTrue(links != null, "Expected a link to be returned. Received null");
			Assert.IsTrue(links.Length ==1);

			ExternalLinkDetail link1 = (ExternalLinkDetail)links[0];

			Assert.IsTrue(link1.LinkText.Length != 0);
			Assert.IsTrue(link1.Url.Length != 0);
		}

		/// <summary>
		/// Tests ZonalServiceCatalogue for naptan with no published links
		/// </summary>
		[Test]
		public void TestGetZonalServiceLinksSingleLinkNotPublished()
		{
			TestMockDataChangeNotification dataChangeNotification = AddDataChangeNotification();

			tm = new TestDataManager(TEST_DATA, CLEARDOWN_SCRIPT, connectionString);
			tm.LoadData();

			InitialiseExternalLinksService();
			ZonalServiceCatalogue refData = GetZonalServiceCatalogue();
			ExternalLinkDetail[] links = refData.GetZonalServiceLinks("9100GHI");

			Assert.IsTrue(links == null);
		}

		/// <summary>
		/// Tests ZonalServiceCatalogue for unknown naptan 
		/// </summary>
		[Test]
		public void TestGetZonalServiceLinksSingleLinkNoZonalDataAvailable()
		{
			TestMockDataChangeNotification dataChangeNotification = AddDataChangeNotification();

			tm = new TestDataManager(TEST_DATA, CLEARDOWN_SCRIPT, connectionString);
			tm.LoadData();

			InitialiseExternalLinksService();
			ZonalServiceCatalogue refData = GetZonalServiceCatalogue();
			ExternalLinkDetail[] links = refData.GetZonalServiceLinks("UNKNOWN");

			Assert.IsTrue(links == null);
		}

		/// <summary>
		/// Tests that the ZonalServiceCatalogue reloads data cache after DataChangeNotification event
		/// </summary>
		[Test]
		public void TestNotificationService()
		{
			TestMockDataChangeNotification dataChangeNotification = AddDataChangeNotification();

			tm = new TestDataManager(TEST_DATA, CLEARDOWN_SCRIPT, connectionString);
			tm.LoadData();

			InitialiseExternalLinksService();
			ZonalServiceCatalogue refData = GetZonalServiceCatalogue();
			ExternalLinkDetail[] links = refData.GetZonalServiceLinks("9100DEF");

			string oldLinkText = links[0].LinkText;

			tm.DataFile = TEST_DATA2;
			tm.LoadData();
		
			//Manually raise a change notification event
			dataChangeNotification.RaiseChangedEvent("ZonalServiceCatalogue");
			refData = (ZonalServiceCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.ZonalServices];

			links = refData.GetZonalServiceLinks("9100DEF");

			Assert.IsTrue(links[0].LinkText != oldLinkText);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Method retrieves and returns ZonalServiceCatalogue from ServiceDiscovery
		/// </summary>
		/// <returns></returns>
		private ZonalServiceCatalogue GetZonalServiceCatalogue()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.ZonalServices, new ZonalServiceCatalogueFactory());
			return (ZonalServiceCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.ZonalServices];
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
	public class TestZonalServiceInitialization: IServiceInitialisation
	{
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
			
			serviceCache.Add(ServiceDiscoveryKey.ExternalLinkService, new ExternalLinksFactory());

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
