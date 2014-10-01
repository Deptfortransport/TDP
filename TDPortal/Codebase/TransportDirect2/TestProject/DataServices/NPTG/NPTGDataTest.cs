// *********************************************** 
// NAME             : NPTGDataTest.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Jun 2011
// DESCRIPTION  	: Unit tests for NPTGData
// ************************************************
                
                
using TDP.Common.DataServices.NPTG;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TDP.Common.ServiceDiscovery;
using TDP.Common.DatabaseInfrastructure;

namespace TDP.TestProject.Common.DataServices.NPTG
{
    
    
    /// <summary>
    ///This is a test class for NPTGDataTest and is intended
    ///to contain all NPTGDataTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NPTGDataTest
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
            string test_data = @"DataServices\NPTGTestData.xml";
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
        ///A test for GetAdminArea
        ///</summary>
        [TestMethod()]
        public void GetAdminAreaTest()
        {
            NPTGData target = new NPTGData(); 
            string countryCode = "Sco"; 
            int adminAreaCode = 111; 
            AdminArea actual;
            actual = target.GetAdminArea(countryCode, adminAreaCode);
            Assert.AreEqual("Aberdeen", actual.AreaName);

            countryCode = "XXX";
            actual = target.GetAdminArea(countryCode, adminAreaCode);
            Assert.IsNull(actual);

            actual = target.GetAdminArea(adminAreaCode);
            Assert.AreEqual("Aberdeen", actual.AreaName);
        }

        /// <summary>
        ///A test for GetAdminAreas
        ///</summary>
        [TestMethod()]
        public void GetAdminAreasTest()
        {
            NPTGData target = new NPTGData(); 
            string countryCode = "Wal"; 
            List<AdminArea> actual;
            actual = target.GetAdminAreas(countryCode);
            Assert.AreEqual(22, actual.Count);

            NPTGData_Accessor accessor = new NPTGData_Accessor();
            accessor.countryAdminAreasCache.Clear();
            actual = accessor.GetAdminAreas(countryCode);
            Assert.AreEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for GetAllAdminAreas
        ///</summary>
        [TestMethod()]
        public void GetAllAdminAreasTest()
        {
            NPTGData target = new NPTGData(); 
            List<AdminArea> actual;
            actual = target.GetAllAdminAreas();
            Assert.AreEqual(143, actual.Count);
            
        }

        /// <summary>
        ///A test for GetAllDistricts
        ///</summary>
        [TestMethod()]
        public void GetAllDistrictsTest()
        {
            NPTGData target = new NPTGData(); 
            List<District> actual;
            actual = target.GetAllDistricts();
            Assert.AreEqual(9, actual.Count);
           
        }

        /// <summary>
        ///A test for GetDistrict
        ///</summary>
        [TestMethod()]
        public void GetDistrictTest()
        {
            NPTGData target = new NPTGData(); 
            int adminAreaCode = 71; 
            int districtCode = 82; 
            District actual;
            actual = target.GetDistrict(adminAreaCode, districtCode);
            Assert.AreEqual("Fenland", actual.DistrictName);

            adminAreaCode = 9999;
            actual = target.GetDistrict(adminAreaCode, districtCode);
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for GetDistricts
        ///</summary>
        [TestMethod()]
        public void GetDistrictsTest()
        {
            NPTGData target = new NPTGData(); 
            int adminAreaCode = 71; 
            List<District> actual;
            actual = target.GetDistricts(adminAreaCode);
            Assert.AreEqual(5, actual.Count);

            NPTGData_Accessor accessor = new NPTGData_Accessor();
            accessor.adminAreaDistrictsCache.Clear();
            actual = accessor.GetDistricts(adminAreaCode);
            Assert.AreEqual(0, actual.Count);
        }

        
    }
}
