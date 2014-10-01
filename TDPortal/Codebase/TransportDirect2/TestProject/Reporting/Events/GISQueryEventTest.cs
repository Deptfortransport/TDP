// *********************************************** 
// NAME             : GISQueryEventTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: GISQueryEvent test 
// ************************************************
// 
                
using TDP.Reporting.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.EventLogging;

namespace TDP.TestProject.Reporting.Events
{
    
    
    /// <summary>
    ///This is a test class for GISQueryEventTest and is intended
    ///to contain all GISQueryEventTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GISQueryEventTest
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
        ///A test for GISQueryEvent Constructor
        ///</summary>
        [TestMethod()]
        public void GISQueryEventConstructorTest()
        {
            GISQueryType gisQueryType = GISQueryType.FindExchangePointsInRadius;
            DateTime submitted = DateTime.Now;
            string sessionId = "sessionId";
            GISQueryEvent target = new GISQueryEvent(gisQueryType, submitted, sessionId);

            Assert.IsTrue(gisQueryType == target.GISQueryType);
            Assert.IsTrue(submitted == target.Submitted);
        }

        /// <summary>
        ///A test for GISQueryEvent FileFormatter
        ///</summary>
        [TestMethod()]
        public void GISQueryFileFormatterTest()
        {
            GISQueryType gisQueryType = GISQueryType.FindExchangePointsInRadius;
            DateTime submitted = DateTime.Now;
            string sessionId = "sessionId";
            GISQueryEvent target = new GISQueryEvent(gisQueryType, submitted, sessionId);

            IEventFormatter actual = target.FileFormatter;

            string eventAsString = actual.AsString(target);

            Assert.IsNotNull(eventAsString);
        }
    }
}
