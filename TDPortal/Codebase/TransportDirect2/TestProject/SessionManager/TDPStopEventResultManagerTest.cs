using TDP.UserPortal.SessionManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.UserPortal.JourneyControl;
using System.Collections.Generic;

namespace TDP.TestProject.SessionManager
{
    
    
    /// <summary>
    ///This is a test class for TDPStopEventResultManagerTest and is intended
    ///to contain all TDPStopEventResultManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPStopEventResultManagerTest
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
        ///A test for TDPStopEventResultManager Constructor
        ///</summary>
        [TestMethod()]
        public void TDPStopEventResultManagerConstructorTest()
        {
            TDPStopEventResultManager target = new TDPStopEventResultManager();
            Assert.IsNotNull(target, "Null object returned");
        }

        /// <summary>
        ///A test for AddTDPJourneyResult
        ///</summary>
        [TestMethod()]
        public void AddTDPJourneyResultTest()
        {
            TDPStopEventResultManager target = new TDPStopEventResultManager();
            ITDPJourneyResult tdpJourneyResult = new TDPJourneyResult();
            target.AddTDPJourneyResult(tdpJourneyResult);

            // Add more results than supported, FIFO order
            tdpJourneyResult = new TDPJourneyResult();
            tdpJourneyResult.JourneyRequestHash = "hash1";
            target.AddTDPJourneyResult(tdpJourneyResult);

            tdpJourneyResult = new TDPJourneyResult();
            tdpJourneyResult.JourneyRequestHash = "hash2";
            target.AddTDPJourneyResult(tdpJourneyResult);

            tdpJourneyResult = new TDPJourneyResult();
            tdpJourneyResult.JourneyRequestHash = "hash3";
            target.AddTDPJourneyResult(tdpJourneyResult);

            tdpJourneyResult = new TDPJourneyResult();
            tdpJourneyResult.JourneyRequestHash = "hash4";
            target.AddTDPJourneyResult(tdpJourneyResult);

            tdpJourneyResult = new TDPJourneyResult();
            tdpJourneyResult.JourneyRequestHash = "hash5";
            target.AddTDPJourneyResult(tdpJourneyResult);

            tdpJourneyResult = new TDPJourneyResult();
            tdpJourneyResult.JourneyRequestHash = "hash6";
            target.AddTDPJourneyResult(tdpJourneyResult);

            tdpJourneyResult = new TDPJourneyResult();
            tdpJourneyResult.JourneyRequestHash = "hash7";
            target.AddTDPJourneyResult(tdpJourneyResult);
        }

        /// <summary>
        ///A test for DoesResultExist
        ///</summary>
        [TestMethod()]
        public void DoesResultExistTest()
        {
            TDPStopEventResultManager target = new TDPStopEventResultManager(); 
            string requestHash = "hash";
            bool expected = false; 
            bool actual;
            actual = target.DoesResultExist(requestHash);
            Assert.AreEqual(expected, actual);

            TDPJourneyResult journeyResult = new TDPJourneyResult();
            journeyResult.JourneyRequestHash = requestHash;
            target.AddTDPJourneyResult(journeyResult);
            expected = true;
            actual = target.DoesResultExist(requestHash);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetTDPJourneyResult
        ///</summary>
        [TestMethod()]
        public void GetTDPJourneyResultTest()
        {
            TDPStopEventResultManager target = new TDPStopEventResultManager();
            string requestHash = "hash2";
            ITDPJourneyResult expected = new TDPJourneyResult();
            expected.JourneyRequestHash = requestHash;
            target.AddTDPJourneyResult(expected);
            ITDPJourneyResult actual = target.GetTDPJourneyResult(requestHash);
            Assert.AreEqual(expected, actual);

            // Check for null result
            actual = target.GetTDPJourneyResult("null");
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for RemoveTDPJourneyResult
        ///</summary>
        [TestMethod()]
        public void RemoveTDPJourneyResultTest()
        {
            TDPStopEventResultManager target = new TDPStopEventResultManager();
            string requestHash = "hash7";

            TDPJourneyResult tdpJourneyResult = new TDPJourneyResult();
            tdpJourneyResult.JourneyRequestHash = requestHash;
            target.AddTDPJourneyResult(tdpJourneyResult);

            // Add more results to queue
            tdpJourneyResult = new TDPJourneyResult();
            tdpJourneyResult.JourneyRequestHash = "hash1";
            target.AddTDPJourneyResult(tdpJourneyResult);

            tdpJourneyResult = new TDPJourneyResult();
            tdpJourneyResult.JourneyRequestHash = "hash2";
            target.AddTDPJourneyResult(tdpJourneyResult);

            // Remove the first result
            target.RemoveTDPJourneyResult(requestHash);
        }

        /// <summary>
        ///A test for IsDirty
        ///</summary>
        [TestMethod()]
        public void IsDirtyTest()
        {
            TDPStopEventResultManager target = new TDPStopEventResultManager(); 
            bool expected = true; 
            bool actual;
            target.IsDirty = expected;
            actual = target.IsDirty;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for TDPJourneyResults
        ///</summary>
        [TestMethod()]
        public void TDPJourneyResultsTest()
        {
            TDPStopEventResultManager target = new TDPStopEventResultManager(); 
            Dictionary<string, ITDPJourneyResult> expected = new Dictionary<string, ITDPJourneyResult>();
            expected.Add("key", new TDPJourneyResult());
            Dictionary<string, ITDPJourneyResult> actual;
            target.TDPJourneyResults = expected;
            actual = target.TDPJourneyResults;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for TDPJourneyResultsQueue
        ///</summary>
        [TestMethod()]
        public void TDPJourneyResultsQueueTest()
        {
            TDPStopEventResultManager target = new TDPStopEventResultManager(); 
            Queue<string> expected = new Queue<string>();
            expected.Enqueue("item");
            Queue<string> actual;
            target.TDPJourneyResultsQueue = expected;
            actual = target.TDPJourneyResultsQueue;
            Assert.AreEqual(expected, actual);
        }
    }
}
