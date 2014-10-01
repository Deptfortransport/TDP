// *********************************************** 
// NAME             : TDPLocationCacheTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 02 Apr 2011
// DESCRIPTION  	: TDPLocationCacheTest test class
// ************************************************
// 
                
using TDP.Common.LocationService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.ServiceDiscovery;
using System.Collections.Generic;

namespace TDP.TestProject.Common.LocationService
{
    /// <summary>
    ///This is a test class for TDPLocationCacheTest and is intended
    ///to contain all TDPLocationCacheTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPLocationCacheTest
    {
        private TestContext testContextInstance;

        private string unknownToFind = "Arbroath Rail Station";
        private string unknownToFindPostcode = "SW1A 1AA";
        private string naptanToFind = "9100LESTER";
        private string groupToFind = "G1";
        private string localityToFind = "E0000326";
        private string postcodeToFind = "NG9 1AL";
        private string alternativeToFind1 = "Glasgow";
        private string alternativeToFind2 = "London";

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
        //
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

        #region Location tests

        #region Unknown

        /// <summary>
        ///A test for GetUnknownLocation
        ///</summary>
        [TestMethod()]
        public void GetUnknownLocationTest()
        {
            // Check can get an unknown location (station)
            TDPLocation actual = TDPLocationCache_Accessor.GetUnknownLocation(unknownToFind);

            Assert.IsNotNull(actual, "Expected GetUnknownLocation to have returned a location");
            Assert.IsTrue(actual.Naptan.Count > 0, "Expected unknown station search to have naptans");
            Assert.IsTrue(!string.IsNullOrEmpty(actual.DisplayName));
            Assert.IsTrue(actual.DisplayName.ToUpper() == unknownToFind.ToUpper(), "Expected unknown station search DisplayName to match search string");
            Assert.IsTrue(!string.IsNullOrEmpty(actual.ID));
            Assert.IsTrue(!string.IsNullOrEmpty(actual.Name));
        }

        /// <summary>
        ///A test for PopulateUnknownData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.locationservice.dll")]
        public void PopulateUnknownDataTest()
        {
            // Tests if can load an unknown location (station) from the database
            TDPLocation actual = TDPLocationCache_Accessor.PopulateUnknownData(unknownToFind);

            Assert.IsNotNull(actual, "Expected PopulateUnknownData to have loaded unknown station location");
            Assert.IsTrue(actual.Naptan.Count > 0, "Expected unknown station search to have naptans");
            Assert.IsTrue(actual.DisplayName.ToUpper() == unknownToFind.ToUpper(), "Expected unknown station search DisplayName to match search string");

            // Tests if can load an unknown location (postcode) from the database
            actual = TDPLocationCache_Accessor.PopulateUnknownData(unknownToFindPostcode);

            // Currently Populate from database not implemented - postcode
            //Assert.IsNotNull(actual, "Expected PopulateUnknownData to have loaded unknown postcode location");
            //Assert.IsTrue(actual.Name.ToUpper() == unknownToFindPostcode.ToUpper().Replace(" ", string.Empty), "Expected unknown postcode search Name to match search string (without spaces)");           
        }

        #endregion

        #region NaPTAN

        /// <summary>
        ///A test for GetNaptanLocation
        ///</summary>
        [TestMethod()]
        public void GetNaptanLocationTest()
        {
            // Check can get a naptan location
            TDPLocation actual = TDPLocationCache_Accessor.GetNaptanLocation(naptanToFind);

            Assert.IsNotNull(actual, "Expected GetNaptanLocation to have returned a naptan location");
            Assert.IsTrue(actual.Naptan.Count > 0, "Expected station to have naptans");
            Assert.IsTrue(actual.Naptan[0] == naptanToFind, "Expected location Naptan to equal naptan searched for");
            Assert.IsTrue(!string.IsNullOrEmpty(actual.ID));
            Assert.IsTrue(!string.IsNullOrEmpty(actual.DisplayName));
            Assert.IsTrue(!string.IsNullOrEmpty(actual.Name));
        }

        /// <summary>
        ///A test for PopulateNaptanData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.locationservice.dll")]
        public void PopulateNaptanDataTest()
        {
            // Tests if can load a naptan location from the database
            TDPLocation actual = TDPLocationCache_Accessor.PopulateNaPTANData(naptanToFind);

            // Currently Populate from database not implemented
            //Assert.IsNotNull(actual, "Expected PopulateNaPTANData to have loaded naptan location");
            //Assert.IsTrue(actual.Naptan.Count > 0, "Expected station to have naptans");
            //Assert.IsTrue(actual.Naptan[0] == naptanToFind, "Expected location Naptan to equal naptan searched for");
            //Assert.IsTrue(actual.ID == naptanToFind, "Expected location ID to equal naptan searched for");
        }

        #endregion

        #region Group

        /// <summary>
        ///A test for GetGroupLocation
        ///</summary>
        [TestMethod()]
        public void GetGroupLocationTest()
        {
            // Check can get a group location
            TDPLocation actual = TDPLocationCache_Accessor.GetGroupLocation(groupToFind);

            Assert.IsNotNull(actual, "Expected GetGroupLocation to have returned a group location");
            Assert.IsTrue(actual.DataSetID == groupToFind, "Expected location DataSetID to equal group searched for");
            Assert.IsTrue(!string.IsNullOrEmpty(actual.ID));
            Assert.IsTrue(actual.IsGNAT == false);
            Assert.IsTrue(!string.IsNullOrEmpty(actual.DisplayName));
            Assert.IsTrue(!string.IsNullOrEmpty(actual.Name));
            Assert.IsTrue(actual.Naptan.Count > 0); // Group station should have no naptans
        }

        /// <summary>
        ///A test for PopulateGroupData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.locationservice.dll")]
        public void PopulateGroupDataTest()
        {
            // Tests if can load a group location from the database
            TDPLocation actual = TDPLocationCache_Accessor.PopulateGroupData(groupToFind);

            // Currently Populate from database not implemented
            //Assert.IsNotNull(actual, "Expected PopulateGroupData to have loaded group location");
            //Assert.IsTrue(actual.DataSetID == groupToFind, "Expected location DataSetID to equal group searched for");
            //Assert.IsTrue(actual.ID == groupToFind, "Expected location ID to equal group searched for");
        }

        #endregion

        #region Locality

        /// <summary>
        ///A test for GetLocalityLocation
        ///</summary>
        [TestMethod()]
        public void GetLocalityLocationTest()
        {
            // Check can get a locality location
            TDPLocation actual = TDPLocationCache_Accessor.GetLocalityLocation(localityToFind);

            Assert.IsNotNull(actual, "Expected GetLocalityLocation to have returned a locality location");
            Assert.IsTrue(actual.Locality == localityToFind, "Expected location Locality to equal locality searched for");
            Assert.IsTrue(!string.IsNullOrEmpty(actual.ID));
            Assert.IsTrue(actual.IsGNAT == false);
            Assert.IsTrue(!string.IsNullOrEmpty(actual.DisplayName));
            Assert.IsTrue(!string.IsNullOrEmpty(actual.Name));
            Assert.IsTrue(actual.Naptan.Count == 0); // Locality should have no naptans
        }

        /// <summary>
        ///A test for PopulateLocalityData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.locationservice.dll")]
        public void PopulateLocalityDataTest()
        {
            // Tests if can load a locality location from the database
            TDPLocation actual = TDPLocationCache_Accessor.PopulateLocalityData(localityToFind);

            // Currently Populate from database not implemented
            //Assert.IsNotNull(actual, "Expected PopulateLocalityData to have loaded locality location");
            //Assert.IsTrue(actual.Locality == localityToFind, "Expected location Locality to equal locality searched for");
            //Assert.IsTrue(actual.ID == localityToFind, "Expected location ID to equal locality searched for");
        }

        #endregion

        /// <summary>
        ///A test for LoadLocations
        ///</summary>
        [TestMethod()]
        public void LoadLocationsTest()
        {
            TDPLocationCache_Accessor.LoadLocations();

            // Check can get a location (e.g. check for locality)
            TDPLocation actual = TDPLocationCache_Accessor.GetLocalityLocation(localityToFind);

            Assert.IsNotNull(actual, "Expected LoadLocations to have loaded locations");
        }

        /// <summary>
        ///A test for PopulateLocationsData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.locationservice.dll")]
        public void PopulateLocationsDataTest()
        {
            // Tests if all locations have been loaded from the database
            TDPLocationCache_Accessor.PopulateLocationsData();

            Assert.IsTrue(TDPLocationCache_Accessor.locations.Count > 0, "Expected locations data to have been populated");
        }

        /// <summary>
        ///A test for PopulateLocationsData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.locationservice.dll")]
        public void GetCachedLocationTest()
        {
            bool useLocationCacheFlag = TDPLocationCache_Accessor.useLocationCache;
            bool usePostcodeCacheFlag = TDPLocationCache_Accessor.usePostcodeCache;

            // Set flag for to use the cache
            TDPLocationCache_Accessor.useLocationCache = true;
            TDPLocationCache_Accessor.usePostcodeCache = true;

            // Tests if all locations have been loaded from the database
            TDPLocationCache_Accessor.PopulateLocationsData();
            TDPLocationCache_Accessor.PopulatePostcodesData();

            TDPLocation actual1 = null;
            List<TDPLocation> actual2 = null;

            actual1 = TDPLocationCache_Accessor.GetUnknownLocation(unknownToFind);
            Assert.IsNotNull(actual1, "Expected GetUnknownLocation using cache to have returned a location");

            actual1 = TDPLocationCache_Accessor.GetNaptanLocation(naptanToFind);
            Assert.IsNotNull(actual1, "Expected GetNaptanLocation using cache to have returned a naptan location");

            actual1 = TDPLocationCache_Accessor.GetGroupLocation(groupToFind);
            Assert.IsNotNull(actual1, "Expected GetGroupLocation using cache to have returned a group location");

            actual1 = TDPLocationCache_Accessor.GetLocalityLocation(localityToFind);
            Assert.IsNotNull(actual1, "Expected GetLocalityLocation using cache to have returned a locality location");

            actual1 = TDPLocationCache_Accessor.GetPostcodeLocation(postcodeToFind);
            Assert.IsNotNull(actual1, "Expected GetPostcodeLocation using cache to have returned a postcode location");

            actual2 = TDPLocationCache_Accessor.GetAlternativeTDPLocations(alternativeToFind1);
            Assert.IsNotNull(actual2, "Expected GetAlternativeTDPLocations using cache to have returned locations");

            // Reset flag for any other tests using the TDPLocationCache
            TDPLocationCache_Accessor.useLocationCache = useLocationCacheFlag;
            TDPLocationCache_Accessor.usePostcodeCache = usePostcodeCacheFlag;

        }

        #endregion

        #region Postcode tests

        /// <summary>
        ///A test for GetPostcodeLocation
        ///</summary>
        [TestMethod()]
        public void GetPostcodeLocationTest()
        {
            // Check can get a postcode
            TDPLocation actual = TDPLocationCache_Accessor.GetPostcodeLocation(postcodeToFind);

            Assert.IsNotNull(actual, "Expected GetPostcodeLocation to have returned a postcode location");
            Assert.IsTrue(actual.DisplayName == postcodeToFind, "Expected location DisplayName to equal postcode searched for");
            Assert.IsTrue(!string.IsNullOrEmpty(actual.ID));
            Assert.IsTrue(actual.IsGNAT == false);
            Assert.IsTrue(!string.IsNullOrEmpty(actual.Locality));
            Assert.IsTrue(!string.IsNullOrEmpty(actual.Name));
            Assert.IsTrue(actual.Toid.Count > 0);
            Assert.IsTrue(actual.Naptan.Count == 0); // Should be no naptans for postcode
        }

        /// <summary>
        ///A test for LoadPostcodes
        ///</summary>
        [TestMethod()]
        public void LoadPostcodesTest()
        {
            TDPLocationCache_Accessor.LoadPostcodes();

            // Check can get a postcode
            TDPLocation actual = TDPLocationCache_Accessor.GetPostcodeLocation(postcodeToFind);

            Assert.IsNotNull(actual, "Expected LoadPostcodes to have loaded postcodes");
        }

        /// <summary>
        ///A test for PopulatePostcodeData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.locationservice.dll")]
        public void PopulatePostcodeDataTest()
        {
            // Tests if can load a postcode location from the database
            TDPLocation actual = TDPLocationCache_Accessor.PopulatePostcodeData(postcodeToFind);

            // Currently Populate from database not implemented
            //Assert.IsNotNull(actual, "Expected PopulatePostcodeData to have loaded postcode");
            //Assert.IsTrue(actual.DisplayName == postcodeToFind, "Expected location DisplayName to equal postcode searched for");
            //Assert.IsTrue(actual.ID == postcodeToFind.Replace(" ", string.Empty), "Expected location ID to equal postcode searched for");
        }

        /// <summary>
        ///A test for PopulatePostcodesData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.locationservice.dll")]
        public void PopulatePostcodesDataTest()
        {
            // Tests if all postcode locations have been loaded from the database
            TDPLocationCache_Accessor.PopulatePostcodesData();

            Assert.IsTrue(TDPLocationCache_Accessor.postcodeLocations.Count > 0, "Expected postcodes data to have been populated");
        }

        #endregion

        #region Alternative Location tests

        /// <summary>
        ///A test for GetAlternativeTDPLocationsFromCache
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.locationservice.dll")]
        public void GetAlternativeTDPLocationsFromCacheTest()
        {
            List<TDPLocation> actual = TDPLocationCache_Accessor.GetAlternativeTDPLocationsFromCache(alternativeToFind1, 1000);

            Assert.IsNotNull(actual, "Expected GetAlternativeTDPLocationsFromCache to have returned a list");

            // If test run seperately, and use location cache is false (likely as this will be run in Dev),
            // then no locations will exist in cache and so result is false - which is ok
            if ((!TDPLocationCache_Accessor.useLocationCache) && (actual.Count == 0 ))
            {
                Assert.IsTrue(actual.Count == 0, "Expected locations to not have been found");
            }
            // Otherwise test for true
            else
            {
                Assert.IsTrue(actual.Count > 0, "Expected locations to have been found");
            }
        }

        /// <summary>
        ///A test for GetAlternativeTDPLocationsFromDB
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.locationservice.dll")]
        public void GetAlternativeTDPLocationsFromDBTest()
        {
            // Tests if can load a naptan location from the database
            List<TDPLocation> actual = TDPLocationCache_Accessor.GetAlternativeTDPLocationsFromDB(alternativeToFind1, 1000);

            Assert.IsNotNull(actual, "Expected GetAlternativeTDPLocationsFromDB to have returned a list");
            Assert.IsTrue(actual.Count > 0, "Expected locations to have been found");

            // Group location in result
            actual = TDPLocationCache_Accessor.GetAlternativeTDPLocationsFromDB(alternativeToFind2, 1000);

            Assert.IsNotNull(actual, "Expected GetAlternativeTDPLocationsFromDB to have returned a list");
            Assert.IsTrue(actual.Count > 0, "Expected locations to have been found");

            // Sort and filter
            actual = TDPLocationCache_Accessor.SortAndFilterAlternativeTDPLocations(actual, 20);
            Assert.IsNotNull(actual, "Expected GetAlternativeTDPLocationsFromDB to have returned a list");
            Assert.IsTrue(actual.Count > 0, "Expected locations to have been found");
            Assert.IsTrue(actual.Count <= 20, "Expected locations to have been found");
        }

        #endregion
    }
}
