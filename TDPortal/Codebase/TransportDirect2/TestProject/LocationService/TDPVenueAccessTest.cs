// *********************************************** 
// NAME             : TDPVenueAccessTest.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Jun 2011
// DESCRIPTION  	: Unit tests for TDPVenueAccess
// ************************************************
                
                
using TDP.Common.LocationService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TDP.TestProject.Common.LocationService
{
    
    
    /// <summary>
    ///This is a test class for TDPVenueAccessTest and is intended
    ///to contain all TDPVenueAccessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPVenueAccessTest
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
        ///A test for TDPVenueAccess Constructor
        ///</summary>
        [TestMethod()]
        public void TDPVenueAccessConstructorTest()
        {
            string venueNaPTAN = "8100TST"; 
            string venueName = "Test"; 
            DateTime accessFrom = new DateTime(2012,07,01); 
            DateTime accessTo = new DateTime(2012,09,14); 
            TimeSpan accessToVenueDuration = new TimeSpan(0,40,35);
            List<TDPVenueAccessStation> stations = new List<TDPVenueAccessStation>();
            stations.Add(new TDPVenueAccessStation("9100TST","TestStation"));
            TDPVenueAccess target = new TDPVenueAccess(venueNaPTAN, venueName, accessFrom, accessTo, accessToVenueDuration);
            target.Stations = stations;

            TDPVenueAccess target1 = new TDPVenueAccess();
            target1.VenueName = venueName;
            target1.VenueNaPTAN = venueNaPTAN;
            target1.AccessFrom = accessFrom;
            target1.AccessTo = accessTo;
            target1.AccessToVenueDuration = accessToVenueDuration;
            target1.Stations = stations;

            Assert.AreEqual(target.AccessFrom, target1.AccessFrom);
            Assert.AreEqual(target.AccessTo, target1.AccessTo);
            Assert.AreEqual(target.AccessToVenueDuration, target1.AccessToVenueDuration);
            Assert.AreEqual(target.Stations.Count, target1.Stations.Count);
            Assert.AreEqual(target.VenueNaPTAN, target1.VenueNaPTAN);
            Assert.AreEqual(target.VenueName, target1.VenueName);

        }

        
    }
}
