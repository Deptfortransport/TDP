// *********************************************** 
// NAME             : ContentProviderTest.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Jun 2011
// DESCRIPTION  	: Unit tests for ContentProvider
// ************************************************
                
                
using TDP.Common.ResourceManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.DatabaseInfrastructure;
using TDP.Common.Web;
using System.Data.SqlClient;
using System.Collections.Generic;
using TDP.Common;
using TDP.Common.ServiceDiscovery;

namespace TDP.TestProject.Common.ResourceManager
{
    
    
    /// <summary>
    ///This is a test class for ContentProviderTest and is intended
    ///to contain all ContentProviderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ContentProviderTest
    {


        private TestContext testContextInstance;
        private static TestDataManager testDataManager;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [TestInitialize()]
        public void TestInitialize()
        {
            string test_data = @"ResourceManager\ResourceManagerTestData.xml";
            string setup_script = @"ResourceManager\ResourceManagerTestSetup.sql";
            string clearup_script = @"ResourceManager\ResourceManagerTestCleanUp.sql";
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Content;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1";
            //@"Server=.\SQLEXPRESS;Initial Catalog=TDPContent;Trusted_Connection=true";

            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
            TDPServiceDiscovery.Init(new TestInitialisationPropertiesLogging());
            testDataManager = new TestDataManager(
                test_data,
                setup_script,
                clearup_script,
                connectionString,
                SqlHelperDatabase.ContentDB);
            testDataManager.Setup();

            testDataManager.LoadData(false);

        }

        //
        //Use ClassCleanup to run code after all tests in a class have run
        [TestCleanup()]
        public void TestCleanup()
        {
            testDataManager.ClearData();
            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
        }


        #endregion


        
        /// <summary>
        ///A test for DataChangedNotificationReceived
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.resourcemanager.dll")]
        public void DataChangedNotificationReceivedTest()
        {
            using (DataChangeNotificationFactory dcnf = new DataChangeNotificationFactory())
            {
                // Ensure data notification is running
                TDPServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.DataChangeNotification, dcnf);

                ContentProvider_Accessor target = new ContentProvider_Accessor(new PrivateObject(ContentProvider.Instance));
                ContentGroup contentGroup = ContentProvider.Instance["TDPGeneral"];
                contentGroup.GetControlProperties(Language.English);

                InsertTestContentForDataChangeNotificationTest();

                object sender = null;
                ChangedEventArgs e = new ChangedEventArgs("Content");
                target.DataChangedNotificationReceived(sender, e);
                // content gets cleared upon datachange notification
                contentGroup.GetControlProperties(Language.English);
                TDPResourceManager resourceManger = new TDPResourceManager();
                Assert.IsNotNull(resourceManger.GetString(Language.English, "ChangeNotificationTest"));
                Assert.AreEqual("ChangeNotificationTestValue", resourceManger.GetString(Language.English, "ChangeNotificationTest"));
            }
        }

        

        /// <summary>
        ///A test for GetControlPropertyCollection, no resources returned due to invalid group name and/or language specified
        ///</summary>
        [TestMethod()]
        public void GetControlPropertyCollectionTestWIthException()
        {
            string groupName = string.Empty;
            Language language = new Language();
            ControlPropertyCollection actual;
            actual = ContentProvider_Accessor.GetControlPropertyCollection(groupName, language);

            Assert.IsTrue(actual.GetControlCount() == 0);
        }

        
        /// <summary>
        ///A test for RegisterForChangeNotification when the datachange notification not initialised and 
        ///Exception gets thrown
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.resourcemanager.dll")]
        public void RegisterForChangeNotificationTestException()
        {
            try
            {
                TDPServiceDiscovery.ResetServiceDiscoveryForTest();
                ContentProvider_Accessor target = new ContentProvider_Accessor();
                bool actual;
                actual = target.RegisterForChangeNotification();

                Assert.Fail("Expected exception to be thrown registering for change notification");
            }
            catch
            {
                // Exception expected
            }

            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
            TDPServiceDiscovery.Init(new TestInitialisationPropertiesLogging());

            using (DataChangeNotificationFactory dcnf = new DataChangeNotificationFactory())
            {
                try
                {
                    // Ensure data notification is running
                    TDPServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.DataChangeNotification, dcnf);

                    ContentProvider_Accessor target = new ContentProvider_Accessor();
                    bool actual;
                    actual = target.RegisterForChangeNotification();
                }
                catch
                {
                    Assert.Fail("Unexpected exception thrown registering for change notification");
                }
            }
        }

        #region Private Helper Methods
        /// <summary>
        /// Inserts text for testing datachange notifiction
        /// </summary>
        private void InsertTestContentForDataChangeNotificationTest()
        {
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    helper.ConnOpen(SqlHelperDatabase.ContentDB);

                    helper.Execute("EXEC AddContent 1, 'TDPGeneral', 'en', 'General', 'ChangeNotificationTest', 'ChangeNotificationTestValue'");
                }
                finally
                {
                    helper.ConnClose();
                }
            }
        }
        #endregion

    }
}
