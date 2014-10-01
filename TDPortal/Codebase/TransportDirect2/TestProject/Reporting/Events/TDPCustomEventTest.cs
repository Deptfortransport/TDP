using TDP.Reporting.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.EventLogging;

namespace TDP.TestProject.Reporting.Events
{
    
    
    /// <summary>
    ///This is a test class for TDPCustomEventTest and is intended
    ///to contain all TDPCustomEventTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPCustomEventTest
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
        ///A test for TDPCustomEvent Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.reporting.events.dll")]
        public void TDPCustomEventConstructorTest()
        {
            string sessionId = string.Empty;
            bool userLoggedOn = false;
            TDPCustomEvent_Accessor target = new TDPCustomEvent_Accessor(sessionId, userLoggedOn);

            Assert.IsTrue(sessionId == target.SessionId);
            Assert.IsTrue(userLoggedOn == target.UserLoggedOn);
        }

        /// <summary>
        ///A test for ConsoleFormatter
        ///</summary>
        [TestMethod()]
        public void TDPCustomEventConsoleFormatterTest()
        {
            string sessionId = string.Empty;
            bool userLoggedOn = false;
            TDPCustomEvent_Accessor target = new TDPCustomEvent_Accessor(sessionId, userLoggedOn);

            IEventFormatter actual = target.ConsoleFormatter;

            OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, "message");

            string eventAsString = actual.AsString(oe);

            Assert.IsNotNull(eventAsString);
        }

        /// <summary>
        ///A test for EmailFormatter
        ///</summary>
        [TestMethod()]
        public void TDPCustomEventEmailFormatterTest()
        {
            string sessionId = string.Empty;
            bool userLoggedOn = false;
            TDPCustomEvent_Accessor target = new TDPCustomEvent_Accessor(sessionId, userLoggedOn);

            IEventFormatter actual = target.EmailFormatter;

            OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, "message");

            string eventAsString = actual.AsString(oe);

            Assert.IsNotNull(eventAsString);
        }

        /// <summary>
        ///A test for EventLogFormatter
        ///</summary>
        [TestMethod()]
        public void TDPCustomEventEventLogFormatterTest()
        {
            string sessionId = string.Empty;
            bool userLoggedOn = false;
            TDPCustomEvent_Accessor target = new TDPCustomEvent_Accessor(sessionId, userLoggedOn);

            IEventFormatter actual = target.EventLogFormatter;

            OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, "message");

            string eventAsString = actual.AsString(oe);

            Assert.IsNotNull(eventAsString);
        }

        /// <summary>
        ///A test for FileFormatter
        ///</summary>
        [TestMethod()]
        public void TDPCustomEventFileFormatterTest()
        {
            string sessionId = string.Empty;
            bool userLoggedOn = false;
            TDPCustomEvent_Accessor target = new TDPCustomEvent_Accessor(sessionId, userLoggedOn);

            IEventFormatter actual = target.FileFormatter;

            OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, "message");

            string eventAsString = actual.AsString(oe);

            Assert.IsNotNull(eventAsString);
        }
    }
}
