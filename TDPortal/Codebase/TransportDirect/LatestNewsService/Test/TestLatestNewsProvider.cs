// *********************************************** 
// NAME			: TestLatestNewsProvider.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 08/04/08
// DESCRIPTION	: Class to test the Latest news provider
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LatestNewsService/Test/TestLatestNewsProvider.cs-arc  $
//
//   Rev 1.1   Apr 10 2008 15:43:22   mmodi
//Updates for testing
//
//   Rev 1.0   Apr 09 2008 18:21:56   mmodi
//Initial revision.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TD.ThemeInfrastructure;

using NUnit.Framework;




namespace TransportDirect.UserPortal.LatestNewsService.Test
{
    /// <summary>
    /// Test class testing the LatestNewsProvider
    /// </summary>
    [TestFixture]
    public class TestLatestNewsProvider
    {
        #region Private Properties

        private string TEST_DATA = Directory.GetCurrentDirectory() + "\\LatestNews\\SpecialContentData1.xml";
        private string TEST_DATA2 = Directory.GetCurrentDirectory() + "\\LatestNews\\SpecialContentData2.xml";
        private string CLEARDOWN_SCRIPT = Directory.GetCurrentDirectory() + "\\LatestNews\\SpecialContentClearDown.sql";
        private const string connectionString = "Server=.;Initial Catalog=TransientPortal;Trusted_Connection=true";
        private TestDataManager tm;

        #endregion

        #region Test Methods

        #region Setup and Cleanup
        /// <summary>
        /// Initialisation method sets up service discovery
        /// </summary>
        [TestFixtureSetUp]
        public void Init()
        {
            TDServiceDiscovery.ResetServiceDiscoveryForTest();
            TDServiceDiscovery.Init(new TestLatestNewsInitialization());
        }

        /// <summary>
        /// Tear down method removes test data
        /// </summary>
        [TestFixtureTearDown]
        public void Teardown()
        {
            tm.ClearData();
        }
        #endregion

        /// <summary>
        /// Tests LatestNewsProvider return news in English
        /// </summary>
        [Test]
        public void TestGetLatestNewsForEnglish()
        {
            tm = new TestDataManager(TEST_DATA, CLEARDOWN_SCRIPT, connectionString);
            tm.LoadData();

            LatestNewsProvider refData = GetLatestNewsProvider();
            string latestNewsText = refData.GetLatestNews("en-GB", "TestPlaceholder");

            Assert.IsTrue((!string.IsNullOrEmpty(latestNewsText)), "Expected latest news to be returned for en-GB, no text was returned");
        }

        /// <summary>
        /// Tests LatestNewsProvider return news in Welsh
        /// </summary>
        [Test]
        public void TestGetLatestNewsForWelsh()
        {
            tm = new TestDataManager(TEST_DATA, CLEARDOWN_SCRIPT, connectionString);
            tm.LoadData();

            LatestNewsProvider refData = GetLatestNewsProvider();
            string latestNewsText = refData.GetLatestNews("cy-GB", "TestPlaceholder");

            Assert.IsTrue((!string.IsNullOrEmpty(latestNewsText)), "Expected latest news to be returned for cy-GB, no text was returned");
        }

        /// <summary>
        /// Tests LatestNewsProvider does not return news for unknown language
        /// </summary>
        [Test]
        public void TestGetLatestNewsForUnknownLanguage()
        {
            tm = new TestDataManager(TEST_DATA, CLEARDOWN_SCRIPT, connectionString);
            tm.LoadData();

            LatestNewsProvider refData = GetLatestNewsProvider();
            string latestNewsText = refData.GetLatestNews("test", "TestPlaceholder");

            Assert.IsTrue((string.IsNullOrEmpty(latestNewsText)), "Expected no latest news text to be returned for unknown language, but news has been found");
        }

        /// <summary>
        /// Tests LatestNewsProvider returns SpecialNoticeBoard text in English
        /// </summary>
        [Test]
        public void TestGetSpecialNoticeBoard()
        {
            tm = new TestDataManager(TEST_DATA, CLEARDOWN_SCRIPT, connectionString);
            tm.LoadData();

            LatestNewsProvider refData = GetLatestNewsProvider();
            string latestNewsText = refData.GetSpecialNotice("en-GB", "TestPlaceholder");

            Assert.IsTrue((!string.IsNullOrEmpty(latestNewsText)), "Expected special notice board text to be returned for en-GB, no text was returned");
        }

        /// <summary>
        /// Tests that the LatestNewsProvider reloads data cache after DataChangeNotification event
        /// </summary>
        [Test]
        public void TestNotificationService()
        {
            TestMockDataChangeNotification dataChangeNotification = AddDataChangeNotification();

            tm = new TestDataManager(TEST_DATA, CLEARDOWN_SCRIPT, connectionString);
            tm.LoadData();

            LatestNewsProvider refData = GetLatestNewsProvider();
            string latestNewsTextBefore = refData.GetLatestNews("en-GB", "TestPlaceholder");

            tm.DataFile = TEST_DATA2;
            tm.LoadData();

            //Manually raise a change notification event
            dataChangeNotification.RaiseChangedEvent("LatestNewsService");
            refData = (LatestNewsProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.LatestNewsFactory];

            string latestNewsTextAfter = refData.GetLatestNews("en-GB", "TestPlaceholder");

            Assert.IsTrue((!string.IsNullOrEmpty(latestNewsTextBefore)));
            Assert.IsTrue((!string.IsNullOrEmpty(latestNewsTextAfter)));
            Assert.IsTrue((!string.Equals(latestNewsTextBefore, latestNewsTextAfter)) , "Expected the latest news to have changed, but they are the same");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method retrieves and returns LatestNewsProvider from ServiceDiscovery
        /// </summary>
        /// <returns></returns>
        private LatestNewsProvider GetLatestNewsProvider()
        {
            TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.LatestNewsFactory, new LatestNewsFactory());
            return (LatestNewsProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.LatestNewsFactory];
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
	public class TestLatestNewsInitialization : IServiceInitialisation
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
    }
    #endregion
}
