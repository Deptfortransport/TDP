// *********************************************** 
// NAME             : TDPLocationTypeHelperTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 02 Apr 2011
// DESCRIPTION  	: TDPLocationTypeHelperTest test class
// ************************************************
// 
                
using TDP.Common.LocationService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common;

namespace TDP.TestProject.Common.LocationService
{
    
    
    /// <summary>
    ///This is a test class for TDPLocationTypeHelperTest and is intended
    ///to contain all TDPLocationTypeHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPLocationTypeHelperTest
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
        ///A test for GetTDPLocationType
        ///</summary>
        [TestMethod()]
        public void GetTDPLocationTypeTest()
        {
            string dbLocationType = "COACH";
            TDPLocationType expected = TDPLocationType.Station;
            TDPLocationType actual = TDPLocationTypeHelper.GetTDPLocationType(dbLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", dbLocationType, expected));

            dbLocationType = "RAIL STATION";
            expected = TDPLocationType.Station;
            actual = TDPLocationTypeHelper.GetTDPLocationType(dbLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", dbLocationType, expected));

            dbLocationType = "TMU";
            expected = TDPLocationType.Station;
            actual = TDPLocationTypeHelper.GetTDPLocationType(dbLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", dbLocationType, expected));

            dbLocationType = "AIRPORT";
            expected = TDPLocationType.Station;
            actual = TDPLocationTypeHelper.GetTDPLocationType(dbLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", dbLocationType, expected));

            dbLocationType = "FERRY";
            expected = TDPLocationType.Station;
            actual = TDPLocationTypeHelper.GetTDPLocationType(dbLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", dbLocationType, expected));

            dbLocationType = "EXCHANGE GROUP";
            expected = TDPLocationType.StationGroup;
            actual = TDPLocationTypeHelper.GetTDPLocationType(dbLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", dbLocationType, expected));

            dbLocationType = "LOCALITY";
            expected = TDPLocationType.Locality;
            actual = TDPLocationTypeHelper.GetTDPLocationType(dbLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", dbLocationType, expected));

            dbLocationType = "VENUEPOI";
            expected = TDPLocationType.Venue;
            actual = TDPLocationTypeHelper.GetTDPLocationType(dbLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", dbLocationType, expected));

            dbLocationType = "POSTCODE";
            expected = TDPLocationType.Postcode;
            actual = TDPLocationTypeHelper.GetTDPLocationType(dbLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", dbLocationType, expected));


            try
            {
                dbLocationType = "abcdef";

                actual = TDPLocationTypeHelper.GetTDPLocationType(dbLocationType);

                Assert.Fail("Expected exception to be thrown parsing an invalid location type");
            }
            catch
            {
                // Exception should be thrown, pass
            }
        }

        /// <summary>
        ///A test for GetTDPLocationTypeActual
        ///</summary>
        [TestMethod()]
        public void GetTDPLocationTypeActualTest()
        {
            string dbLocationType = "COACH";
            TDPLocationType expected = TDPLocationType.StationCoach;
            TDPLocationType actual = TDPLocationTypeHelper.GetTDPLocationTypeActual(dbLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", dbLocationType, expected));

            dbLocationType = "RAIL STATION";
            expected = TDPLocationType.StationRail;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeActual(dbLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", dbLocationType, expected));

            dbLocationType = "TMU";
            expected = TDPLocationType.StationTMU;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeActual(dbLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", dbLocationType, expected));

            dbLocationType = "AIRPORT";
            expected = TDPLocationType.StationAirport;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeActual(dbLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", dbLocationType, expected));

            dbLocationType = "FERRY";
            expected = TDPLocationType.StationFerry;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeActual(dbLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", dbLocationType, expected));

            dbLocationType = "EXCHANGE GROUP";
            expected = TDPLocationType.StationGroup;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeActual(dbLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", dbLocationType, expected));

            dbLocationType = "LOCALITY";
            expected = TDPLocationType.Locality;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeActual(dbLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", dbLocationType, expected));

            dbLocationType = "VENUEPOI";
            expected = TDPLocationType.Venue;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeActual(dbLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", dbLocationType, expected));

            dbLocationType = "POSTCODE";
            expected = TDPLocationType.Postcode;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeActual(dbLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", dbLocationType, expected));


            try
            {
                dbLocationType = "abcdef";

                actual = TDPLocationTypeHelper.GetTDPLocationTypeActual(dbLocationType);

                Assert.Fail("Expected exception to be thrown parsing an invalid location type");
            }
            catch
            {
                // Exception should be thrown, pass
            }
        }

        /// <summary>
        /// A test for GetTDPLocationTypeQS method
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(TDPException))]
        public void GetTDPLocationTypeQSTest()
        {
            string queryLocationType = "S";
            TDPLocationType expected = TDPLocationType.Station;
            TDPLocationType actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(queryLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", queryLocationType, expected));

            queryLocationType = "LG";
            expected = TDPLocationType.Locality;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(queryLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", queryLocationType, expected));

            queryLocationType = "U";
            expected = TDPLocationType.Unknown;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(queryLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", queryLocationType, expected));

            queryLocationType = "V";
            expected = TDPLocationType.Venue;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(queryLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", queryLocationType, expected));

            queryLocationType = "SG";
            expected = TDPLocationType.StationGroup;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(queryLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", queryLocationType, expected));

            queryLocationType = "P";
            expected = TDPLocationType.Postcode;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(queryLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", queryLocationType, expected));

            queryLocationType = "A";
            expected = TDPLocationType.Address;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(queryLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", queryLocationType, expected));

            queryLocationType = "PI";
            expected = TDPLocationType.POI;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(queryLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", queryLocationType, expected));

            queryLocationType = "EN";
            expected = TDPLocationType.CoordinateEN;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(queryLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", queryLocationType, expected));

            queryLocationType = "L";
            expected = TDPLocationType.CoordinateLL;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(queryLocationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", queryLocationType, expected));


            // Exception thrown
            queryLocationType = "AB";
            actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(queryLocationType);

        }

        /// <summary>
        /// A test for GetTDPLocationTypeQS method to parse location type in to query string values
        /// </summary>
        [TestMethod()]
        public void GetTDPLocationTypeQS1Test()
        {
            string expected = "S";
            TDPLocationType locationType = TDPLocationType.Station;
            string actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(locationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", locationType, expected));

            expected = "S";
            locationType = TDPLocationType.StationAirport;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(locationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", locationType, expected));

            expected = "LG";
            locationType = TDPLocationType.Locality;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(locationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", locationType, expected));

            expected = "V";
            locationType = TDPLocationType.Venue;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(locationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", locationType, expected));

            expected = "SG";
            locationType = TDPLocationType.StationGroup;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(locationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", locationType, expected));

            expected = "P";
            locationType = TDPLocationType.Postcode;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(locationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", locationType, expected));

            expected = "A";
            locationType = TDPLocationType.Address;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(locationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", locationType, expected));

            expected = "PI";
            locationType = TDPLocationType.POI;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(locationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", locationType, expected));
                        
            expected = "EN";
            locationType = TDPLocationType.CoordinateEN;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(locationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", locationType, expected));

            expected = "L";
            locationType = TDPLocationType.CoordinateLL;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(locationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", locationType, expected));
            
            expected = "U";
            locationType = TDPLocationType.Unknown;
            actual = TDPLocationTypeHelper.GetTDPLocationTypeQS(locationType);

            Assert.AreEqual(expected, actual, string.Format("Expected location type[{0}] to be parsed into [{1}]", locationType, expected));

        }
    }
}
