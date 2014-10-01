﻿using TDP.Common.ServiceDiscovery;
using TDP.UserPortal.SessionManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web;
using System.Web.SessionState;

namespace TDP.TestProject.SessionManager
{
    
    
    /// <summary>
    ///This is a test class for ITDPSessionManagerTest and is intended
    ///to contain all ITDPSessionManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ITDPSessionManagerTest
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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                HttpContext.Current = new HttpContext(new HttpRequest("", "http://localhost/", ""), new HttpResponse(sw));
            }

            System.Web.SessionState.SessionStateUtility.AddHttpSessionStateToContext(HttpContext.Current,
                new HttpSessionStateContainer("SessionId", new SessionStateItemCollection(), new HttpStaticObjectsCollection(), 20000, true, HttpCookieMode.UseCookies, SessionStateMode.Off, false));

            TDPServiceDiscovery.ResetServiceDiscoveryForTest();

            TDPServiceDiscovery.Init(new TestInitialisationPropertiesLogging());
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            HttpContext.Current = null;
        }
        //
        #endregion


        /// <summary>
        ///A test for OnFormShift
        ///</summary>
        [TestMethod()]
        public void OnFormShiftTest()
        {
            using (TDPSessionManager target = new TDPSessionManager(new TDPSessionFactory()))
            {
                target.OnFormShift();
            }
        }

        /// <summary>
        ///A test for OnLoad
        ///</summary>
        [TestMethod()]
        public void OnLoadTest()
        {
            using (TDPSessionManager target = new TDPSessionManager(new TDPSessionFactory()))
            {
                target.OnLoad();
            }
        }

        /// <summary>
        ///A test for OnPreUnload
        ///</summary>
        [TestMethod()]
        public void OnPreUnloadTest()
        {
            using (TDPSessionManager target = new TDPSessionManager(new TDPSessionFactory()))
            {
                target.OnPreUnload();
            }
        }

        /// <summary>
        ///A test for OnUnload
        ///</summary>
        [TestMethod()]
        public void OnUnloadTest()
        {
            using (TDPSessionManager target = new TDPSessionManager(new TDPSessionFactory()))
            {
                target.OnUnload();
            }
        }

        /// <summary>
        ///A test for JourneyState
        ///</summary>
        [TestMethod()]
        public void JourneyStateTest()
        {
            using (TDPSessionManager target = new TDPSessionManager(new TDPSessionFactory()))
            {
                JourneyViewState expected = new JourneyViewState();
                JourneyViewState actual;
                target.JourneyState = expected;
                actual = target.JourneyState;
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        ///A test for PageState
        ///</summary>
        [TestMethod()]
        public void PageStateTest()
        {
            using (TDPSessionManager target = new TDPSessionManager(new TDPSessionFactory()))
            {
                InputPageState expected = new InputPageState();
                InputPageState actual;
                target.PageState = expected;
                actual = target.PageState;
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        ///A test for RequestManager
        ///</summary>
        [TestMethod()]
        public void RequestManagerTest()
        {
            using (TDPSessionManager target = new TDPSessionManager(new TDPSessionFactory()))
            {
                TDPRequestManager expected = new TDPRequestManager();
                TDPRequestManager actual;
                target.RequestManager = expected;
                actual = target.RequestManager;
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        ///A test for ResultManager
        ///</summary>
        [TestMethod()]
        public void ResultManagerTest()
        {
            using (TDPSessionManager target = new TDPSessionManager(new TDPSessionFactory()))
            {
                TDPResultManager expected = new TDPResultManager();
                TDPResultManager actual;
                target.ResultManager = expected;
                actual = target.ResultManager;
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        ///A test for Session
        ///</summary>
        [TestMethod()]
        public void SessionTest()
        {
            using (TDPSessionManager target = new TDPSessionManager(new TDPSessionFactory()))
            {
                ITDPSession actual;
                actual = target.Session;
            }
        }

        /// <summary>
        ///A test for StopEventRequestManager
        ///</summary>
        [TestMethod()]
        public void StopEventRequestManagerTest()
        {
            using (TDPSessionManager target = new TDPSessionManager(new TDPSessionFactory()))
            {
                TDPStopEventRequestManager expected = new TDPStopEventRequestManager();
                TDPStopEventRequestManager actual;
                target.StopEventRequestManager = expected;
                actual = target.StopEventRequestManager;
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        ///A test for StopEventResultManager
        ///</summary>
        [TestMethod()]
        public void StopEventResultManagerTest()
        {
            using (TDPSessionManager target = new TDPSessionManager(new TDPSessionFactory()))
            {
                TDPStopEventResultManager expected = new TDPStopEventResultManager();
                TDPStopEventResultManager actual;
                target.StopEventResultManager = expected;
                actual = target.StopEventResultManager;
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        ///A test for TravelNewsPageState
        ///</summary>
        [TestMethod()]
        public void TravelNewsPageStateTest()
        {
            using (TDPSessionManager target = new TDPSessionManager(new TDPSessionFactory()))
            {
                TravelNewsPageState expected = new TravelNewsPageState();
                TravelNewsPageState actual;
                target.TravelNewsPageState = expected;
                actual = target.TravelNewsPageState;
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
