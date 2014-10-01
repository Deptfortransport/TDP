// *********************************************** 
// NAME             : TDPVenueLocationCacheTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 02 Apr 2011
// DESCRIPTION  	: TDPVenueLocationCacheTest test class
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
    ///This is a test class for TDPVenueLocationCacheTest and is intended
    ///to contain all TDPVenueLocationCacheTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPVenueLocationCacheTest
    {
        private TestContext testContextInstance;

        private string venueToFind = "8100STA";

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
        ///A test for GetVenueLocation
        ///</summary>
        [TestMethod()]
        public void GetVenueLocationTest()
        {
            TDPLocation actual = TDPVenueLocationCache_Accessor.GetVenueLocation(venueToFind);

            Assert.IsNotNull(actual, "Expected GetVenueLocation to have found venue");
            Assert.IsTrue(actual.Naptan.Contains(venueToFind), "Expected GetVenueLocation location to have returned the requested venue");
        }

        /// <summary>
        ///A test for GetVenuesList
        ///</summary>
        [TestMethod()]
        public void GetVenuesListTest()
        {
            List<TDPLocation> actual = TDPVenueLocationCache_Accessor.GetVenuesList();

            bool hasVenues = actual.Count > 0;

            bool hasVenue = false;

            foreach (TDPLocation location in actual)
            {
                if (location.Naptan.Contains(venueToFind))
                {
                    hasVenue = true;
                    break;
                }
            }

            Assert.IsTrue(hasVenues, "Expected GetVenuesList to return a list containing at least 1 venue");
            Assert.IsTrue(hasVenue, string.Format("Expected GetVenuesList to return a list containing the venue with NaPTAN[{0}]", venueToFind));
        }

        /// <summary>
        ///A test for LoadVenues
        ///</summary>
        [TestMethod()]
        public void LoadVenuesTest()
        {
            TDPVenueLocationCache_Accessor.LoadVenues();

            // Check can get a venue
            TDPLocation actual = TDPVenueLocationCache_Accessor.GetVenueLocation(venueToFind);

            Assert.IsNotNull(actual, "Expected LoadVenues to have loaded venues");
        }

        /// <summary>
        ///A test for PopulateData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.locationservice.dll")]
        public void PopulateVenuesDataTest()
        {
            TDPVenueLocationCache_Accessor.PopulateVenuesData();

            Assert.IsTrue(TDPVenueLocationCache_Accessor.venuesList.Count > 0, "Expected venues data to have been populated");
            Assert.IsTrue(TDPVenueLocationCache_Accessor.venueLocations.Count > 0, "Expected venues data to have been populated");
        }
    }
}
