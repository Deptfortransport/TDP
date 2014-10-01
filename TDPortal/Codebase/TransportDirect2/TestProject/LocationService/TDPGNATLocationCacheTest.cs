// *********************************************** 
// NAME             : TDPGNATLocationCacheTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 02 Apr 2011
// DESCRIPTION  	: TDPGNATLocationCacheTest test class
// ************************************************
// 
                
using TDP.Common.LocationService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TDP.Common.ServiceDiscovery;

namespace TDP.TestProject.Common.LocationService
{   
    /// <summary>
    ///This is a test class for TDPGNATLocationCacheTest and is intended
    ///to contain all TDPGNATLocationCacheTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPGNATLocationCacheTest
    {
        private TestContext testContextInstance;

        private string gnatToFind = "9100LESTER";
        private int gnatAdminAreaToFind = 82;

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
        public static void MyClassInitialize(TestContext testContext)
        {
            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
            TDPServiceDiscovery.Init(new TestInitialisationPropertiesLogging());

            MockLocationServiceHelper.PopulateLocationLists();
        }
        
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
        }
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GetGNATList
        ///</summary>
        [TestMethod()]
        public void GetGNATListTest()
        {
            List<TDPGNATLocation> actual = TDPGNATLocationCache_Accessor.GetGNATList();
            
            bool hasGNATStations = actual.Count > 0;

            bool hasGNATStation = false;

            foreach (TDPGNATLocation location in actual)
            {
                if (location.Naptan.Contains(gnatToFind))
                {
                    hasGNATStation = true;
                    break;
                }
            }

            Assert.IsTrue(hasGNATStations, "Expected GetGNATList to return a list containing at least 1 GNAT station");
            Assert.IsTrue(hasGNATStation, string.Format("Expected GetGNATList to return a list containing the GNAT station with NaPTAN[{0}]", gnatToFind));
        }

        /// <summary>
        ///A test for IsGNAT
        ///</summary>
        [TestMethod()]
        public void IsGNATTest()
        {
            // Check the GNAT station is in the list
            bool isGNAT = TDPGNATLocationCache_Accessor.IsGNAT(gnatToFind, true, true);
            Assert.IsTrue(isGNAT, "Expected GNAT station to have been found in GNAT stations list");

            isGNAT = TDPGNATLocationCache_Accessor.IsGNAT(gnatToFind, true, false);
            Assert.IsTrue(isGNAT, "Expected GNAT station to have been found in GNAT stations list");

            isGNAT = TDPGNATLocationCache_Accessor.IsGNAT(gnatToFind, false, true);
            Assert.IsTrue(isGNAT, "Expected GNAT station to have been found in GNAT stations list");

            // Wheelchair but no assistance
            isGNAT = TDPGNATLocationCache_Accessor.IsGNAT("9100EDINPRK", true, true);
            Assert.IsFalse(isGNAT, "Expected GNAT station to not have been found in GNAT stations list... Update test for latest data");
        }

        /// <summary>
        ///A test for IsGNATAdminArea
        ///</summary>
        [TestMethod()]
        public void IsGNATAdminAreaTest()
        {
            // Check the GNAT admin area is in the list
            // AreaCode and DistrctCode may no longer exist in the database so check if these tests fail
            bool isGNAT = TDPGNATLocationCache_Accessor.IsGNATAdminArea(gnatAdminAreaToFind, 282, true, true);
            Assert.IsTrue(isGNAT, "Expected GNAT admin area to have been found in GNAT admin areas list");

            isGNAT = TDPGNATLocationCache_Accessor.IsGNATAdminArea(gnatAdminAreaToFind, 281, true, false);
            Assert.IsTrue(isGNAT, "Expected GNAT admin area to have been found in GNAT admin areas list");

            isGNAT = TDPGNATLocationCache_Accessor.IsGNATAdminArea(gnatAdminAreaToFind, 288, false, true);
            Assert.IsTrue(isGNAT, "Expected GNAT admin area to have been found in GNAT admin areas list");

            // No All exists in data
            isGNAT = TDPGNATLocationCache_Accessor.IsGNATAdminArea(gnatAdminAreaToFind, TDPGNATLocationCache_Accessor.DISTRICTCODE_ALL, true, true);
            Assert.IsTrue(!isGNAT, "Expected GNAT admin area to have been found in GNAT admin areas list");
            
        }

        /// <summary>
        ///A test for LoadGNATStations
        ///</summary>
        [TestMethod()]
        public void LoadGNATStationsTest()
        {
            TDPGNATLocationCache_Accessor.LoadGNATStations();

            // Check the GNAT station is in the list
            bool isGNAT = TDPGNATLocationCache_Accessor.IsGNAT(gnatToFind, true, true);

            Assert.IsTrue(isGNAT, "Expected GNAT station to have been loaded and found");
        }

        /// <summary>
        ///A test for PopulateGNATData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.locationservice.dll")]
        public void PopulateGNATDataTest()
        {
            TDPGNATLocationCache_Accessor.PopulateGNATLocations();

            Assert.IsTrue(TDPGNATLocationCache_Accessor.gnatList.Count > 0, "Expected GNAT stations data to have been populated");
        }
    }
}
