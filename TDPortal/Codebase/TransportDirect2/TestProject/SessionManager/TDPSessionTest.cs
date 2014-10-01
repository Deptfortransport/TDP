using TDP.UserPortal.SessionManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web;
using System.Web.SessionState;
using TDP.Common;

namespace TDP.TestProject.SessionManager
{
    
    
    /// <summary>
    ///This is a test class for TDPSessionTest and is intended
    ///to contain all TDPSessionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPSessionTest
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
        
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                HttpContext.Current = new HttpContext(new HttpRequest("", "http://localhost/", ""), new HttpResponse(sw));

                System.Web.SessionState.SessionStateUtility.AddHttpSessionStateToContext(HttpContext.Current,
                    new HttpSessionStateContainer("SessionId", new SessionStateItemCollection(), new HttpStaticObjectsCollection(), 20000, true, HttpCookieMode.UseCookies, SessionStateMode.Off, false));
            }
        }
        
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            HttpContext.Current = null;
        }
        
        #endregion


        /// <summary>
        ///A test for TDPSession Constructor
        ///</summary>
        [TestMethod()]
        public void TDPSessionConstructorTest()
        {
            TDPSession target = new TDPSession();
            Assert.IsNotNull(target, "Null object returned");
        }

        /// <summary>
        ///A test for GetFullID
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.sessionmanager.dll")]
        public void GetFullIDTest()
        {
            TDPSession_Accessor target = new TDPSession_Accessor();
            StringKey key = new StringKey("keyName");
            string expected = "str@keyName";
            string actual = target.GetFullID(key);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest()
        {
            TDPSession target = new TDPSession(); 
            DateKey key = new DateKey("keyName");
            DateTime expected = DateTime.Now; 
            DateTime actual;
            target[key] = expected;
            actual = target[key];
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest1()
        {
            TDPSession target = new TDPSession();
            BoolKey key = new BoolKey("keyName");
            bool expected = true; 
            bool actual;

            // Check not set first, should be false
            Assert.IsTrue(target[key] == false);

            // Set value, then check again
            target[key] = expected;
            actual = target[key];
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest2()
        {
            TDPSession target = new TDPSession(); 
            PageIdKey key = new PageIdKey("keyName");
            PageId expected = PageId.Homepage;
            PageId actual;
            target[key] = expected;
            actual = target[key];
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest3()
        {
            TDPSession target = new TDPSession();
            IntKey key = new IntKey("keyName");
            int expected = 7;
            int actual;
            target[key] = expected;
            actual = target[key];
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest4()
        {
            TDPSession target = new TDPSession(); 
            StringKey key = new StringKey("keyName");
            string expected = "keyValue";
            string actual;
            target[key] = expected;
            actual = target[key];
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest5()
        {
            TDPSession target = new TDPSession(); 
            DoubleKey key = new DoubleKey("keyName");
            double expected = 1.7E+6;
            double actual;
            target[key] = expected;
            actual = target[key];
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SessionID
        ///</summary>
        [TestMethod()]
        public void SessionIDTest()
        {
            TDPSession target = new TDPSession(); 
            string actual;
            actual = target.SessionID;
            Assert.IsTrue(!string.IsNullOrEmpty(actual), "Null or empty session Id returned");
        }
    }
}
