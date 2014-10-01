// *********************************************** 
// NAME             : InternalRequestEventTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: InternalRequestEvent test 
// ************************************************
// 

using TDP.Reporting.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.EventLogging;

namespace TDP.TestProject.Reporting.Events
{
    
    
    /// <summary>
    ///This is a test class for JourneyWebRequestEventTest and is intended
    ///to contain all JourneyWebRequestEventTest Unit Tests
    ///</summary>
    [TestClass()]
    public class JourneyWebRequestEventTest
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
        ///A test for JourneyWebRequestEvent Constructor
        ///</summary>
        [TestMethod()]
        public void JourneyWebRequestEventConstructorTest()
        {
            string sessionId = "sessionId";
            string journeyWebRequestId = "requestId";
            DateTime submitted = DateTime.Now;
            JourneyWebRequestType requestType = JourneyWebRequestType.Journey;
            string regionCode = "L";
            bool success = false;
            bool refTransaction = false;
            JourneyWebRequestEvent target = new JourneyWebRequestEvent(sessionId, journeyWebRequestId, submitted, requestType, regionCode, success, refTransaction);

            Assert.IsTrue(sessionId == target.SessionId);
            Assert.IsTrue(submitted == target.Submitted);
            Assert.IsTrue(journeyWebRequestId == target.JourneyWebRequestId);
            Assert.IsTrue(requestType == target.RequestType);
            Assert.IsTrue(regionCode == target.RegionCode);
            Assert.IsTrue(success == target.Success);
            Assert.IsTrue(refTransaction == target.RefTransaction);
        }

        /// <summary>
        ///A test for JourneyWebRequestEvent FileFormatter
        ///</summary>
        [TestMethod()]
        public void JourneyWebRequestEventFileFormatterTest()
        {
            string sessionId = "sessionId";
            string journeyWebRequestId = "requestId";
            DateTime submitted = DateTime.Now;
            JourneyWebRequestType requestType = JourneyWebRequestType.Journey;
            string regionCode = "L";
            bool success = false;
            bool refTransaction = false;
            JourneyWebRequestEvent target = new JourneyWebRequestEvent(sessionId, journeyWebRequestId, submitted, requestType, regionCode, success, refTransaction);

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
