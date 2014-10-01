// *********************************************** 
// NAME             : NoResultsEventTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: NoResultsEvent test 
// ************************************************
// 

using TDP.Reporting.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.EventLogging;

namespace TDP.TestProject.Reporting.Events
{
    
    
    /// <summary>
    ///This is a test class for NoResultsEventTest and is intended
    ///to contain all NoResultsEventTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NoResultsEventTest
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
        ///A test for NoResultsEvent Constructor
        ///</summary>
        [TestMethod()]
        public void NoResultsEventConstructorTest()
        {
            DateTime submitted = DateTime.Now;
            string sessionId = "sessionId";
            bool userLoggedOn = false;
            NoResultsEvent target = new NoResultsEvent(submitted, sessionId, userLoggedOn);

            Assert.IsTrue(sessionId == target.SessionId);
            Assert.IsTrue(submitted == target.Submitted);
            Assert.IsTrue(userLoggedOn == target.UserLoggedOn);
            
        }

        /// <summary>
        ///A test for NoResultsEvent FileFormatter
        ///</summary>
        [TestMethod()]
        public void NoResultsEventFileFormatterTest()
        {
            DateTime submitted = DateTime.Now;
            string sessionId = "sessionId";
            bool userLoggedOn = false;
            NoResultsEvent target = new NoResultsEvent(submitted, sessionId, userLoggedOn);

            IEventFormatter actual = target.FileFormatter;
            string eventAsString = actual.AsString(target);
            Assert.IsNotNull(eventAsString);
        }
    }
}
