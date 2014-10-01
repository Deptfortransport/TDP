// *********************************************** 
// NAME             : StopAccessibilityLinksTest.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Jun 2011
// DESCRIPTION  	: Unit tests for StopAccessibilityLinks
// ************************************************
                
                
using TDP.Common.DataServices.StopAccessibilityLinks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.ServiceDiscovery;
using TDP.Common.DatabaseInfrastructure;
using SAL = TDP.Common.DataServices.StopAccessibilityLinks;

namespace TDP.TestProject.Common.DataServices.StopAccessibilityLinks
{
    
    
    /// <summary>
    ///This is a test class for StopAccessibilityLinksTest and is intended
    ///to contain all StopAccessibilityLinksTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StopAccessibilityLinksTest
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
        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            string test_data = @"DataServices\StopAccessibilityLinksData.xml";
            string setup_script = @"DataServices\DataServicesTestSetup.sql";
            string clearup_script = @"DataServices\DataServicesTestCleanUp.sql";
            string connectionString = @"Server=.\SQLEXPRESS;Initial Catalog=TransientPortal;Trusted_Connection=true";

            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
            TDPServiceDiscovery.Init(new TestInitialisationPropertiesLogging());
            testDataManager = new TestDataManager(
                test_data,
                setup_script,
                clearup_script,
                connectionString,
                SqlHelperDatabase.TransientPortalDB);
            testDataManager.Setup();

            testDataManager.LoadData(false);

        }

        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void TestClassCleanup()
        {
            testDataManager.ClearData();
            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
        }


        #endregion



        /// <summary>
        ///A test for GetAccessibilityURL
        ///</summary>
        [TestMethod()]
        public void GetAccessibilityURLTest()
        {
            SAL.StopAccessibilityLinks target = new SAL.StopAccessibilityLinks(); 
            string naptan = "9100LECSTER"; 
            string operatorCode =  "EM"; 
            DateTime date = new DateTime(2012, 08, 14); 
            string expected = "http://www.transportdirect.info"; 
            string actual;
            actual = target.GetAccessibilityURL(naptan, operatorCode, date);
            Assert.AreEqual(expected, actual);

            naptan = string.Empty;
            expected = string.Empty;
            actual = target.GetAccessibilityURL(naptan, operatorCode, date);
            Assert.AreEqual(expected, actual);

            naptan = "9100LECSTER";
            operatorCode = null;
            expected = "http://www.transportdirect.info";
            actual = target.GetAccessibilityURL(naptan, operatorCode, date);
            Assert.AreEqual(expected, actual);

            naptan = "9100XXX";
            operatorCode = null;
            expected = string.Empty;
            actual = target.GetAccessibilityURL(naptan, operatorCode, date);
            Assert.AreEqual(expected, actual);
        }

       
    }
}
