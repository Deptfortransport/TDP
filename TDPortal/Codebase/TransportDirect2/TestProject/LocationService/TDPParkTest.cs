// *********************************************** 
// NAME             : TDPParkTest.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Jun 2011
// DESCRIPTION  	: Unit tests for TDPPark
// ************************************************
                
                
using TDP.Common.LocationService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TDP.TestProject.Common.LocationService
{
    
    
    /// <summary>
    ///This is a test class for TDPParkTest and is intended
    ///to contain all TDPParkTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPParkTest
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
        ///A test for IsOpenForTime
        ///</summary>
        [TestMethod()]
        public void IsOpenForTimeTest()
        {
            TDPPark target = new TDPPark();
            target.ID = "Test";
            target.Name = "TestVenue";
            target.VenueNaPTAN = "8100TST";
            target.Availability = new List<TDPParkAvailability>();
           
            DateTime fromDate = new DateTime(2012, 07, 1);
            DateTime toDate = new DateTime(2012, 09, 14);
            TimeSpan dailyOpeningTime = new TimeSpan(8, 55, 53);
            TimeSpan dailyClosingTime = new TimeSpan(18, 30, 30);
            DaysOfWeek daysOpen = DaysOfWeek.Everyday;
            TDPParkAvailability availability = new TDPParkAvailability(fromDate, toDate, dailyOpeningTime, dailyClosingTime, daysOpen);

            target.Availability.Add(availability);

            // within available time
            DateTime time = new DateTime(2012, 07, 14, 10, 56, 33);  
            bool expected = true;  
            bool actual;
            actual = target.IsOpenForTime(time);
            Assert.AreEqual(expected, actual);
            
            // outside the available time
            time = new DateTime(2012, 07, 14, 18, 56, 33);
            actual = target.IsOpenForTime(time);
            Assert.AreEqual(false, actual);

            // min time
            time = DateTime.MinValue;
            actual = target.IsOpenForTime(time);
            Assert.AreEqual(true, actual);

            // null availabilities
            target.Availability = null;
            actual = target.IsOpenForTime(time);
            Assert.AreEqual(true, actual);

            // opening and closing at midnight
            fromDate = new DateTime(2012, 07, 1);
            toDate = new DateTime(2012, 09, 14);
            dailyOpeningTime = TimeSpan.Zero;
            dailyClosingTime = TimeSpan.Zero;
            daysOpen = DaysOfWeek.Everyday;
            availability = new TDPParkAvailability(fromDate, toDate, dailyOpeningTime, dailyClosingTime, daysOpen);

            target.Availability = new List<TDPParkAvailability>();
            target.Availability.Add(availability);

            time = new DateTime(2012, 07, 14, 18, 56, 33);
            actual = target.IsOpenForTime(time);
            Assert.AreEqual(true, actual);

            // opening and closing are invalid
            fromDate = new DateTime(2012, 07, 1);
            toDate = new DateTime(2012, 09, 14);
            dailyOpeningTime = new TimeSpan(18, 30, 30);
            dailyClosingTime = new TimeSpan(8, 55, 53);
            daysOpen = DaysOfWeek.Everyday;
            availability = new TDPParkAvailability(fromDate, toDate, dailyOpeningTime, dailyClosingTime, daysOpen);

            target.Availability = new List<TDPParkAvailability>();
            target.Availability.Add(availability);

            time = new DateTime(2012, 07, 14, 15, 0, 0);
            actual = target.IsOpenForTime(time);
            Assert.AreEqual(false, actual);
        }

        
    }
}
