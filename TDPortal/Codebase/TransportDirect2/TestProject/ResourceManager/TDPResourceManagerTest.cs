// *********************************************** 
// NAME             : TDPResourceManagerTest.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Jun 2011
// DESCRIPTION  	: Unit tests for TDPResourceManager
// ************************************************
                
                
using TDP.Common.ResourceManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.Web;
using TDP.Common;
using TDP.Common.ServiceDiscovery;
using TDP.Common.DatabaseInfrastructure;

namespace TDP.TestProject.Common.ResourceManager
{
    
    
    /// <summary>
    ///This is a test class for TDPResourceManagerTest and is intended
    ///to contain all TDPResourceManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPResourceManagerTest
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
        ///A test for GetString
        ///</summary>
        [TestMethod()]
        public void GetStringTest()
        {
            TDPResourceManager target = new TDPResourceManager(); 
            string groupName = TDPResourceManager.GROUP_DEFAULT; 
            string collectionName = TDPResourceManager.COLLECTION_DEFAULT; 
            string key = "AccessibilityOpitons.LblTo.Text"; 
            string expected = "To"; 
            string actual;
            actual = target.GetString(Language.English, groupName, collectionName, key);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetString for Welsh content
        ///</summary>
        [TestMethod()]
        public void GetStringTestWelshContent()
        {
            TDPResourceManager target = new TDPResourceManager(); 
            Language language = Language.Welsh;
            string groupName = TDPResourceManager.GROUP_DEFAULT;
            string collectionName = TDPResourceManager.COLLECTION_DEFAULT;
            string key = "AccessibilityOpitons.LblTo.Text";
            string expected = "Le To"; 
            string actual;
            actual = target.GetString(language, groupName, collectionName, key);
            Assert.AreEqual(expected, actual);
           
        }

        /// <summary>
        ///A test for GetString with only Key specified
        ///</summary>
        [TestMethod()]
        public void GetStringTestByKeyOnly()
        {
            TDPResourceManager target = new TDPResourceManager(); 
            string key = "AccessibilityOptions.Back.ToolTip"; 
            string expected = "Back"; 
            string actual;
            actual = target.GetString(Language.English, key);
            Assert.AreEqual(expected, actual);
            
        }

        /// <summary>
        ///A test for GetString with collection and key specified
        ///</summary>
        [TestMethod()]
        public void GetStringTestByCollectionAndKey()
        {
            TDPResourceManager target = new TDPResourceManager(); 
            string collectionName = TDPResourceManager.COLLECTION_DEFAULT; 
            string key = "AccessibilityOptions.Back.ToolTip"; 
            string expected = "Back"; 
            string actual;
            actual = target.GetString(Language.English, collectionName, key);
            Assert.AreEqual(expected, actual);
            
        }

        /// <summary>
        ///A test for GetString with invalid key specified
        ///</summary>
        [TestMethod()]
        public void GetStringTestInvalidKey()
        {
            TDPResourceManager target = new TDPResourceManager();
            string collectionName = TDPResourceManager.COLLECTION_DEFAULT;
            string key = "AccessibilityOptions.Back.TolTip";
            string actual;
            actual = target.GetString(Language.English, collectionName, key);
            Assert.IsNull(actual);

        }
    }
}
