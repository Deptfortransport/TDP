// *********************************************** 
// NAME             : ContentGroupTest.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Jun 2011
// DESCRIPTION  	: Unit tests for ContentGroup
// ************************************************
                
                
using TDP.Common.ResourceManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.Web;
using TDP.Common.ServiceDiscovery;
using TDP.Common.DatabaseInfrastructure;
using TDP.Common;

namespace TDP.TestProject.Common.ResourceManager
{
    
    
    /// <summary>
    ///This is a test class for ContentGroupTest and is intended
    ///to contain all ContentGroupTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ContentGroupTest
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
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{   
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
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
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            testDataManager.ClearData();
            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
        }
        //
        
        #endregion

                

        /// <summary>
        ///A test for ClearControlProperties
        ///</summary>
        [TestMethod()]
        public void ClearControlPropertiesTest()
        {
            ControlPropertyCollectionProvider_Accessor controlPropertyCollectionProvider = new ControlPropertyCollectionProvider_Accessor();
            string groupName = "TDPGeneral"; 
            ContentGroup_Accessor target = new ContentGroup_Accessor(groupName);
            target.GetControlProperties(Language.English);
            target.ClearControlProperties();
            Assert.AreEqual(0, controlPropertyCollectionProvider.dictionary.Count);
            
            
        }

        /// <summary>
        ///A test for GetControlProperties
        ///</summary>
        [TestMethod()]
        public void GetControlPropertiesTest()
        {
            string groupName = "TDPGeneral";
            ContentGroup_Accessor target = new ContentGroup_Accessor(groupName);

            ControlPropertyCollection actual;
            actual = target.GetControlProperties(Language.English);

            Assert.AreEqual(1, actual.GetControlCount());

            Assert.IsNotNull(actual.GetPropertyNames("General"));
            Assert.AreEqual(10, actual.GetPropertyNames("General").Length);

            Assert.IsNotNull(actual.GetPropertyNames("TEST"));
            Assert.AreEqual(0, actual.GetPropertyNames("TEST").Length);
        }

        /// <summary>
        ///A test for GetControlProperties
        ///</summary>
        [TestMethod()]
        public void GetControlPropertiesTest1()
        {
            string groupName = "TDPGeneral"; 
            ContentGroup_Accessor target = new ContentGroup_Accessor(groupName); 
            Language language = Language.Welsh; 
            ControlPropertyCollection actual;
            actual = target.GetControlProperties(language);
            Assert.AreEqual(1, actual.GetControlCount());
            Assert.IsNotNull(actual.GetPropertyNames("General"));
            // property names will be still 10 event though there is  only 1 row for welsh in test data
            // as the resource manger loads english content by default for those where welsh content not found
            Assert.AreEqual(10, actual.GetPropertyNames("General").Length);
            // Check that we loaded the welsh content
            Assert.AreEqual("Le To", actual.GetPropertyValue("General", "AccessibilityOpitons.LblTo.Text"));
        }

        /// <summary>
        ///A test for GroupName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.resourcemanager.dll")]
        public void GroupNameTest()
        {
            PrivateObject param0 = new PrivateObject(new ContentGroup_Accessor("TDPGeneral")); 
            ContentGroup_Accessor target = new ContentGroup_Accessor(param0); 
            string actual;
            actual = target.GroupName;
            Assert.AreEqual("TDPGeneral", actual);
        }
    }
}
