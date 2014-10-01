using TDP.Common.ServiceDiscovery;
using TDP.UserPortal.SessionManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web;
using System.Web.SessionState;

namespace TDP.TestProject.SessionManager
{
    
    
    /// <summary>
    ///This is a test class for TDPSessionManagerTest and is intended
    ///to contain all TDPSessionManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPSessionManagerTest
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
        ///A test for TDPSessionManager Constructor
        ///</summary>
        [TestMethod()]
        public void TDPSessionManagerConstructorTest()
        {
            ITDPSessionFactory sessFactory = new TDPSessionFactory();
            using (TDPSessionManager target = new TDPSessionManager(sessFactory))
            {
                Assert.IsNotNull(target, "Null object returned");
            }
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.sessionmanager.dll")]
        public void DisposeTest()
        {
            TDPSessionFactory factory = new TDPSessionFactory();
            TDPSessionManager_Accessor target = new TDPSessionManager_Accessor(factory);
            bool disposing = true;
            target.Dispose(disposing);
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        public void DisposeTest1()
        {
            ITDPSessionFactory sessFactory = new TDPSessionFactory();
            TDPSessionManager target = new TDPSessionManager(sessFactory);
            target.Dispose();
        }

        /// <summary>
        ///A test for Finalize
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.sessionmanager.dll")]
        public void FinalizeTest()
        {
            TDPSessionFactory factory = new TDPSessionFactory();
            using (TDPSessionManager_Accessor target = new TDPSessionManager_Accessor(factory))
            {
                target.Finalize();
            }
        }

        /// <summary>
        ///A test for GetData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.sessionmanager.dll")]
        public void GetDataTest()
        {
            TDPSessionFactory factory = new TDPSessionFactory();
            using (TDPSessionManager_Accessor target = new TDPSessionManager_Accessor(factory))
            {
                IKey key = new StringKey("keyName");

                // Get non-existant key (force manager to go to session state server
                object notThere = target.GetData(key);
                Assert.IsNull(notThere, "Expected null as no key was uploaded");

                string expected = "keyValue";
                target.SetData(key, expected);
                string actual = (string)target.GetData(key);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        ///A test for OnFormShift
        ///</summary>
        [TestMethod()]
        public void OnFormShiftTest()
        {
            ITDPSessionFactory sessFactory = new TDPSessionFactory();
            using (TDPSessionManager target = new TDPSessionManager(sessFactory))
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
            ITDPSessionFactory sessFactory = new TDPSessionFactory();
            using (TDPSessionManager target = new TDPSessionManager(sessFactory))
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
            ITDPSessionFactory sessFactory = new TDPSessionFactory();
            using (TDPSessionManager target = new TDPSessionManager(sessFactory))
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
            ITDPSessionFactory sessFactory = new TDPSessionFactory();
            using (TDPSessionManager target = new TDPSessionManager(sessFactory))
            {
                target.OnUnload();
            }
        }

        /// <summary>
        ///A test for SaveData
        ///</summary>
        [TestMethod()]
        public void SaveDataTest()
        {
            ITDPSessionFactory sessFactory = new TDPSessionFactory();
            using (TDPSessionManager_Accessor targetAccessor = new TDPSessionManager_Accessor(sessFactory))
            {
                // Create a couple of keys to save then set one to null to go down the other code path
                StringKey key1 = new StringKey("key1");
                targetAccessor.SetData(key1, "keyValue1");
                StringKey key2 = new StringKey("key2");
                targetAccessor.SetData(key2, null);

                targetAccessor.SaveData();

                // Dirty object
                InputPageState inputPageState = new InputPageState();
                inputPageState.ShowEarlierLinkOutwardRiver = true;

                targetAccessor.PageState = inputPageState;
                targetAccessor.SaveData();
            }
        }

        /// <summary>
        ///A test for SetData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.sessionmanager.dll")]
        public void SetDataTest()
        {
            TDPSessionFactory factory = new TDPSessionFactory();
            using (TDPSessionManager_Accessor target = new TDPSessionManager_Accessor(factory))
            {
                IKey key = new StringKey("keyValue");
                string expected = "keyValue";
                target.SetData(key, expected);
                Assert.AreEqual(expected, target.GetData(key), "Stored value differs from expected");
            }
        }

        /// <summary>
        ///A test for Current
        ///</summary>
        [TestMethod()]
        public void CurrentTest()
        {
            // Insert session manager into ServiceDiscovery
            TDPServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.SessionManager, new TDPSessionFactory());

            ITDPSessionManager actual = TDPSessionManager.Current;
            Assert.IsNotNull(actual, "Null object returned");
        }

        /// <summary>
        ///A test for JourneyState
        ///</summary>
        [TestMethod()]
        public void JourneyStateTest()
        {
            ITDPSessionFactory sessFactory = new TDPSessionFactory();
            using (TDPSessionManager target = new TDPSessionManager(sessFactory))
            {
                // Check for null value first - Session manager will create by default
                Assert.IsNotNull(target.JourneyState);

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
            ITDPSessionFactory sessFactory = new TDPSessionFactory();
            using (TDPSessionManager target = new TDPSessionManager(sessFactory))
            {
                target.PageState = null;
                // Check for null value first - Session manager will create by default
                Assert.IsNotNull(target.PageState);

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
            ITDPSessionFactory sessFactory = new TDPSessionFactory();
            using (TDPSessionManager target = new TDPSessionManager(sessFactory))
            {
                // Check for null value first - Session manager will create by default
                Assert.IsNotNull(target.RequestManager);

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
            ITDPSessionFactory sessFactory = new TDPSessionFactory();
            using (TDPSessionManager target = new TDPSessionManager(sessFactory))
            {
                // Check for null value first - Session manager will create by default
                Assert.IsNotNull(target.ResultManager);

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
            ITDPSessionFactory sessFactory = new TDPSessionFactory();
            using (TDPSessionManager target = new TDPSessionManager(sessFactory))
            {
                ITDPSession actual;
                actual = target.Session;
                Assert.IsNotNull(target, "Null object returned");
            }
        }

        /// <summary>
        ///A test for StopEventRequestManager
        ///</summary>
        [TestMethod()]
        public void StopEventRequestManagerTest()
        {
            ITDPSessionFactory sessFactory = new TDPSessionFactory();
            using (TDPSessionManager target = new TDPSessionManager(sessFactory))
            {
                // Check for null value first - Session manager will create by default
                Assert.IsNotNull(target.StopEventRequestManager);

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
            ITDPSessionFactory sessFactory = new TDPSessionFactory();
            using (TDPSessionManager target = new TDPSessionManager(sessFactory))
            {
                // Check for null value first - Session manager will create by default
                Assert.IsNotNull(target.StopEventResultManager);

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
            ITDPSessionFactory sessFactory = new TDPSessionFactory();
            using (TDPSessionManager target = new TDPSessionManager(sessFactory))
            {
                // Check for null value first - Session manager will create by default
                Assert.IsNotNull(target.TravelNewsPageState);

                TravelNewsPageState expected = new TravelNewsPageState();
                TravelNewsPageState actual;
                target.TravelNewsPageState = expected;
                actual = target.TravelNewsPageState;
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
