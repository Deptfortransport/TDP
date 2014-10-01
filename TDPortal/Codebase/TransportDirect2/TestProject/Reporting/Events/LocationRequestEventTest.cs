// *********************************************** 
// NAME             : LocationRequestEventTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: LocationRequestEvent test 
// ************************************************
// 

using TDP.Reporting.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.EventLogging;

namespace TDP.TestProject.Reporting.Events
{
    
    
    /// <summary>
    ///This is a test class for LocationRequestEventTest and is intended
    ///to contain all LocationRequestEventTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LocationRequestEventTest
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
        ///A test for LocationRequestEvent Constructor
        ///</summary>
        [TestMethod()]
        public void LocationRequestEventConstructorTest()
        {
            string journeyPlanRequestId = "requestId";
            JourneyPrepositionCategory prepositionCategory = JourneyPrepositionCategory.FirstLastService;
            string adminAreaCode = "123";
            string regionCode = "987";
            LocationRequestEvent target = new LocationRequestEvent(journeyPlanRequestId, prepositionCategory, adminAreaCode, regionCode);

            Assert.IsTrue(journeyPlanRequestId == target.JourneyPlanRequestId);
            Assert.IsTrue(prepositionCategory == target.PrepositionCategory);
            Assert.IsTrue(adminAreaCode == target.AdminAreaCode);
            Assert.IsTrue(regionCode == target.RegionCode);
        }

        /// <summary>
        ///A test for LocationRequestEvent FileFormatter
        ///</summary>
        [TestMethod()]
        public void LocationRequestEventFileFormatterTest()
        {
            string journeyPlanRequestId = "requestId";
            JourneyPrepositionCategory prepositionCategory = JourneyPrepositionCategory.FirstLastService;
            string adminAreaCode = "123";
            string regionCode = "987";
            LocationRequestEvent target = new LocationRequestEvent(journeyPlanRequestId, prepositionCategory, adminAreaCode, regionCode);

            IEventFormatter actual = target.FileFormatter;
            string eventAsString = actual.AsString(target);
            Assert.IsNotNull(eventAsString);

            actual = target.EmailFormatter;
            eventAsString = actual.AsString(target);
            Assert.IsNotNull(eventAsString);

            actual = target.EventLogFormatter;
            eventAsString = actual.AsString(target);
            Assert.IsNotNull(eventAsString);

            actual = target.ConsoleFormatter;
            eventAsString = actual.AsString(target);
            Assert.IsNotNull(eventAsString);
        }
    }
}
