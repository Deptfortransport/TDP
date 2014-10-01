using TDP.UserPortal.SessionManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.UserPortal.JourneyControl;
using System.Collections.Generic;

namespace TDP.TestProject.SessionManager
{
    
    
    /// <summary>
    ///This is a test class for TDPStopEventRequestManagerTest and is intended
    ///to contain all TDPStopEventRequestManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPStopEventRequestManagerTest
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
        ///A test for TDPStopEventRequestManager Constructor
        ///</summary>
        [TestMethod()]
        public void TDPStopEventRequestManagerConstructorTest()
        {
            TDPStopEventRequestManager target = new TDPStopEventRequestManager();
            Assert.IsNotNull(target, "Null object returned");
        }

        /// <summary>
        ///A test for AddTDPJourneyRequest
        ///</summary>
        [TestMethod()]
        public void AddTDPJourneyRequestTest()
        {
            TDPStopEventRequestManager target = new TDPStopEventRequestManager();
            ITDPJourneyRequest tdpJourneyRequest = new TDPJourneyRequest();
            target.AddTDPJourneyRequest(tdpJourneyRequest);
            Assert.IsTrue(target.TDPJourneyRequests.ContainsValue(tdpJourneyRequest), "Journey request not added");

            // Re-add existing request test
            target.AddTDPJourneyRequest(tdpJourneyRequest);

            // Add more requests than supported, FIFO order
            tdpJourneyRequest = new TDPJourneyRequest();
            tdpJourneyRequest.OutwardDateTime = DateTime.Now.AddDays(1);
            tdpJourneyRequest.JourneyRequestHash = tdpJourneyRequest.GetTDPHashCode().ToString();
            target.AddTDPJourneyRequest(tdpJourneyRequest);

            tdpJourneyRequest = new TDPJourneyRequest();
            tdpJourneyRequest.OutwardDateTime = DateTime.Now.AddDays(2);
            tdpJourneyRequest.JourneyRequestHash = tdpJourneyRequest.GetTDPHashCode().ToString();
            target.AddTDPJourneyRequest(tdpJourneyRequest);

            tdpJourneyRequest = new TDPJourneyRequest();
            tdpJourneyRequest.OutwardDateTime = DateTime.Now.AddDays(3);
            tdpJourneyRequest.JourneyRequestHash = tdpJourneyRequest.GetTDPHashCode().ToString();
            target.AddTDPJourneyRequest(tdpJourneyRequest);

            tdpJourneyRequest = new TDPJourneyRequest();
            tdpJourneyRequest.OutwardDateTime = DateTime.Now.AddDays(4);
            tdpJourneyRequest.JourneyRequestHash = tdpJourneyRequest.GetTDPHashCode().ToString();
            target.AddTDPJourneyRequest(tdpJourneyRequest);

            tdpJourneyRequest = new TDPJourneyRequest();
            tdpJourneyRequest.OutwardDateTime = DateTime.Now.AddDays(5);
            tdpJourneyRequest.JourneyRequestHash = tdpJourneyRequest.GetTDPHashCode().ToString();
            target.AddTDPJourneyRequest(tdpJourneyRequest);

            tdpJourneyRequest = new TDPJourneyRequest();
            tdpJourneyRequest.OutwardDateTime = DateTime.Now.AddDays(6);
            tdpJourneyRequest.JourneyRequestHash = tdpJourneyRequest.GetTDPHashCode().ToString();
            target.AddTDPJourneyRequest(tdpJourneyRequest);

            Assert.IsTrue(target.TDPJourneyRequests.ContainsValue(tdpJourneyRequest));
        }

        /// <summary>
        ///A test for GetTDPJourneyRequest
        ///</summary>
        [TestMethod()]
        public void GetTDPJourneyRequestTest()
        {
            TDPStopEventRequestManager target = new TDPStopEventRequestManager(); 
            string requestHash = "hash3"; 
            ITDPJourneyRequest expected = new TDPJourneyRequest();
            expected.JourneyRequestHash = requestHash; 
            ITDPJourneyRequest actual;
            target.AddTDPJourneyRequest(expected);
            actual = target.GetTDPJourneyRequest(requestHash);
            Assert.AreEqual(expected, actual);

            // Check for null request
            actual = target.GetTDPJourneyRequest("null");
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for IsDirty
        ///</summary>
        [TestMethod()]
        public void IsDirtyTest()
        {
            TDPStopEventRequestManager target = new TDPStopEventRequestManager(); 
            bool expected = true; 
            bool actual;
            target.IsDirty = expected;
            actual = target.IsDirty;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for TDPJourneyRequests
        ///</summary>
        [TestMethod()]
        public void TDPJourneyRequestsTest()
        {
            TDPStopEventRequestManager target = new TDPStopEventRequestManager(); 
            Dictionary<string, ITDPJourneyRequest> expected = new Dictionary<string,ITDPJourneyRequest>();
            expected.Add("key", new TDPJourneyRequest());
            Dictionary<string, ITDPJourneyRequest> actual;
            target.TDPJourneyRequests = expected;
            actual = target.TDPJourneyRequests;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for TDPJourneyRequestsQueue
        ///</summary>
        [TestMethod()]
        public void TDPJourneyRequestsQueueTest()
        {
            TDPStopEventRequestManager target = new TDPStopEventRequestManager(); 
            Queue<string> expected = new Queue<string>();
            expected.Enqueue("item");
            Queue<string> actual;
            target.TDPJourneyRequestsQueue = expected;
            actual = target.TDPJourneyRequestsQueue;
            Assert.AreEqual(expected, actual);
        }
    }
}
