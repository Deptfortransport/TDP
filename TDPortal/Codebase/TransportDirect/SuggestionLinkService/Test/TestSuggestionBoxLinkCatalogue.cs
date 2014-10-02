// *********************************************** 
// NAME                 : TestSuggestionBoxLinkCatalogue.cs
// AUTHOR               : Ken Josling
// DATE CREATED         : 18/08/2005 
// DESCRIPTION			: Unit Test class for TestSuggestionBoxLinkCatalogue
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SuggestionLinkService/Test/TestSuggestionBoxLinkCatalogue.cs-arc  $
//
//   Rev 1.1   Mar 10 2008 15:28:00   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:50:18   mturner
//Initial revision.
//
//   Rev 1.5   Feb 16 2006 16:47:04   kjosling
//Fixed merge. Added connectionstring property for TestDataManager constructor
//
//   Rev 1.4   Feb 16 2006 15:35:44   halkatib
//Merged stream 0002 into the trunk.
//
//   Rev 1.3   Feb 07 2006 13:28:54   mtillett
//Update file path to fix tests on build machine
//
//   Rev 1.2   Sep 02 2005 15:33:58   kjosling
//Updated following code review
//
//   Rev 1.1   Aug 31 2005 18:12:04   kjosling
//Updated following code review
//
//   Rev 1.0   Aug 24 2005 16:47:14   kjosling
//Initial revision.

using System;
using System.IO;
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

namespace TransportDirect.UserPortal.SuggestionLinkService.Test
{
	/// <summary>
	/// Summary description for TestSuggestionBoxLinkCatalogue.
	/// </summary>
	[TestFixture]
	public class TestSuggestionBoxLinkCatalogue
	{

		#region Private Properties

		private readonly string TEST_DATA1= Directory.GetCurrentDirectory() + @"\SuggestionLinkService\SuggestionListData1.xml";
		private readonly string TEST_DATA2= Directory.GetCurrentDirectory() + @"\SuggestionLinkService\SuggestionListData2.xml";
		private readonly string CONFIG_SCRIPT= Directory.GetCurrentDirectory() + @"\SuggestionLinkService\DataLoad.sql";
		private const string connectionString = "Server=.;Initial Catalog=TransientPortal;Trusted_Connection=true";

		#endregion

		#region Test Methods

		[SetUp]
		public void Init()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestSuggestionLinksServiceInitialization());
		}

		[TearDown]
		public void Teardown() 
		{}

		[Test]
		public void TestGetUniqueLinkByContext()
		{
		
			TestDataManager tm = new TestDataManager(TEST_DATA2, CONFIG_SCRIPT, connectionString);
			tm.LoadData();

			SuggestionBoxLinkCatalogue refData = GetSuggestionLinkService();
			Context[] contexts = new Context[] {Context.CONTEXT1, Context.CONTEXT2};
			SuggestionBoxLinkItem[] items = refData.GetUniqueLinkByContext(contexts, 1);
			Assert.IsTrue(items.Length ==5);

			contexts = new Context[] {Context.CONTEXT1};
			items = refData.GetUniqueLinkByContext(contexts, 1);
			Assert.IsTrue(items.Length ==3);

			contexts = new Context[] {Context.CONTEXT2};
			items = refData.GetUniqueLinkByContext(contexts, 1);
			Assert.IsTrue(items.Length ==2);
		}

		[Test]
		public void TestNotificationService()
		{
			TestMockDataChangeNotification dataChangeNotification = AddDataChangeNotification();

			TestDataManager tm = new TestDataManager(TEST_DATA1, CONFIG_SCRIPT, connectionString);
			tm.LoadData();
			SuggestionBoxLinkCatalogue refData = GetSuggestionLinkService();
			Context[] contexts = new Context[] {Context.CONTEXT1};
			SuggestionBoxLinkItem[] items = refData.GetUniqueLinkByContext(contexts, 1);
	
			System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");

			//Need to search for the specific changed record in case additional data is present during test
			SuggestionBoxLinkItem item = (SuggestionBoxLinkItem)items[0];
			Assert.IsNotNull(item, "Expected a valid SuggestionBoxLinkItem, received null");


			Assert.IsTrue(item.CategoryPriority == 110);
	
			tm.DataFile = TEST_DATA2;
			tm.LoadData();
			
			//Manually raise a change notification event
			dataChangeNotification.RaiseChangedEvent("SuggestionBoxLinkCatalogue");

			refData = GetSuggestionLinkService();

			items = refData.GetUniqueLinkByContext(contexts, 1);

			item = items[0];
			Assert.IsNotNull(item, "Expected a valid SuggestionBoxLinkItem, received null");

			Assert.IsTrue(item.CategoryPriority != 110);
		}

		#endregion

		#region Private Methods

		private SuggestionBoxLinkCatalogue GetSuggestionLinkService()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.SuggestionLinkService, new SuggestionBoxLinkCatalogueFactory());
			return (SuggestionBoxLinkCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.SuggestionLinkService];
		}

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
	public class TestSuggestionLinksServiceInitialization: IServiceInitialisation
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
