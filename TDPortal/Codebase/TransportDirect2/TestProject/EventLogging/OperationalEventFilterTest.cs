using TDP.Common.EventLogging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TDP.TestProject.EventLogging
{
    
    
    /// <summary>
    ///This is a test class for OperationalEventFilterTest and is intended
    ///to contain all OperationalEventFilterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OperationalEventFilterTest
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
        ///A test for ShouldLog
        ///</summary>
        [TestMethod()]
        public void ShouldLogTest()
        {
            OperationalEventFilter target = new OperationalEventFilter();

            OperationalEvent oe = new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "Test Message");

            TDPTraceSwitch_Accessor accessor = new TDPTraceSwitch_Accessor();

            TDPTraceSwitch_Accessor.currentLevel = TDPTraceLevel.Verbose;

            Assert.IsTrue(target.ShouldLog(oe));

            oe = new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Warning, "Test Message");

            TDPTraceSwitch_Accessor.currentLevel = TDPTraceLevel.Warning;

            Assert.IsTrue(target.ShouldLog(oe));

            oe = new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, "Test Message");

            TDPTraceSwitch_Accessor.currentLevel = TDPTraceLevel.Error;

            Assert.IsTrue(target.ShouldLog(oe));

            oe = new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Info, "Test Message");

            TDPTraceSwitch_Accessor.currentLevel = TDPTraceLevel.Info;

            Assert.IsTrue(target.ShouldLog(oe));

            oe = new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Info, "Test Message");

            TDPTraceSwitch_Accessor.currentLevel = TDPTraceLevel.Off;

            Assert.IsFalse(target.ShouldLog(oe));

            CustomEventOne cEvent = new CustomEventOne(TDPEventCategory.Infrastructure, TDPTraceLevel.Info, "Test Message", "user", 21213232);

            Assert.IsFalse(target.ShouldLog(cEvent));
        }
    }
}
