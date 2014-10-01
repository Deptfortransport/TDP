// *********************************************** 
// NAME             : LocationSearchTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: LocationSearch test
// ************************************************
// 
                
using TDP.Common.LocationService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TDP.Common.LocationService.Gazetteer;

namespace TDP.TestProject.Common.LocationService
{
    
    
    /// <summary>
    ///This is a test class for LocationSearchTest and is intended
    ///to contain all LocationSearchTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LocationSearchTest
    {


        private TestContext testContextInstance;

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
        ///A test for LocationSearch Constructor
        ///</summary>
        [TestMethod()]
        public void LocationSearchConstructorTest()
        {
            LocationSearch target = new LocationSearch();

            Assert.IsNotNull(target);

            string searchText = string.Empty;
            string searchId = string.Empty;
            TDPLocationType searchType = TDPLocationType.Station;
            bool javascriptEnabled = false;
            
            target = new LocationSearch(searchText, searchId, searchType, javascriptEnabled);

            Assert.IsNotNull(target);

            searchText = "SearchText";
            searchId = "SearchId";
            searchType = TDPLocationType.StationRail;
            javascriptEnabled = true;

            target = new LocationSearch(searchText, searchId, searchType, javascriptEnabled);

            Assert.AreEqual(searchText, target.SearchText);
            Assert.AreEqual(searchId, target.SearchId);
            Assert.AreEqual(searchType, target.SearchType);
            Assert.AreEqual(javascriptEnabled, target.JavascriptEnabled);

            Assert.IsTrue(target.ToString().Length > 0);

            OSGridReference osgr = new OSGridReference("312345,312345");
            target.GridReference = osgr;
            target.LocationQueryResults = new List<LocationQueryResult>();
            target.LocationCacheResults = new List<TDPLocation>();
            target.VagueSearch = true;
            target.SingleWord = true;
            target.SupportHierarchic = true;
            target.LocationChoiceSelected = null;
            target.LocationChoiceDrillDown = true;

            Assert.AreEqual(osgr, target.GridReference);
            Assert.AreEqual(false, target.LocationQueryResultsExist);
            Assert.AreEqual(false, target.LocationCacheResultsExist);
            Assert.AreEqual(true, target.VagueSearch);
            Assert.AreEqual(true, target.SingleWord);
            Assert.AreEqual(true, target.SupportHierarchic);
            Assert.AreEqual(null, target.LocationChoiceSelected);
            Assert.AreEqual(true, target.LocationChoiceDrillDown);

            Assert.IsTrue(target.ToString().Length > 0);
        }

        /// <summary>
        ///A test for LocationSearch CurrentLevel
        ///</summary>
        [TestMethod()]
        public void LocationSearchCurrentLevelTest()
        {
            LocationSearch target = new LocationSearch();
            int expected = -1;
            int actual;
            actual = target.CurrentLevel();
            
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LocationSearch GetLocationCacheResult
        ///</summary>
        [TestMethod()]
        public void LocationSearchGetLocationCacheResultTest()
        {
            LocationSearch target = new LocationSearch();
            target.LocationCacheResults = null;
            IList<TDPLocation> expected = null;
            IList<TDPLocation> actual;
            actual = target.GetLocationCacheResult(); 
            Assert.AreEqual(expected, actual);

            // Add dummy list of results
            List<TDPLocation> locationCacheResults = new List<TDPLocation>();
            locationCacheResults.Add(new TDPLocation());
            target.LocationCacheResults = locationCacheResults;

            actual = target.GetLocationCacheResult();
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for LocationSearch GetLocationChoices
        ///</summary>
        [TestMethod()]
        public void LocationSearchGetLocationChoicesTest()
        {
            LocationSearch target = new LocationSearch();
            int level = 1;
            LocationChoiceList expected = null;
            LocationChoiceList actual;
            actual = target.GetLocationChoices(level);
            Assert.AreEqual(expected, actual);

            // Add dummy list of results
            List<LocationQueryResult> locationQueryResults = new List<LocationQueryResult>();
            LocationQueryResult lqr = new LocationQueryResult(string.Empty);
            LocationChoiceList lcl = new LocationChoiceList();
            lcl.Add(new LocationChoice("Description", false, string.Empty, string.Empty, new OSGridReference(), "naptan", 0, "locality", string.Empty, false));
            lqr.LocationChoiceList = lcl;
            locationQueryResults.Add(lqr);
            target.LocationQueryResults = locationQueryResults;

            actual = target.GetLocationChoices(0);
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        ///A test for LocationSearch GetLocationQueryResult
        ///</summary>
        [TestMethod()]
        public void LocationSearchGetLocationQueryResultTest()
        {
            LocationSearch target = new LocationSearch();
            int level = -1;
            LocationQueryResult expected = null;
            LocationQueryResult actual;
            actual = target.GetLocationQueryResult(level);
            Assert.AreEqual(expected, actual);

            // Add dummy list of results
            List<LocationQueryResult> locationQueryResults = new List<LocationQueryResult>();
            locationQueryResults.Add(new LocationQueryResult(string.Empty));
            target.LocationQueryResults = locationQueryResults;

            actual = target.GetLocationQueryResult(0);
            Assert.IsNotNull(actual);
        }
    }
}
