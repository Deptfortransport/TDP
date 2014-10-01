using TDP.UserPortal.SessionManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TDP.TestProject.SessionManager
{
    
    
    /// <summary>
    ///This is a test class for ITDPSessionAwareTest and is intended
    ///to contain all ITDPSessionAwareTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ITDPSessionAwareTest
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


        internal virtual ITDPSessionAware CreateITDPSessionAware()
        {
            ITDPSessionAware target = new InputPageState();
            return target;
        }

        /// <summary>
        ///A test for IsDirty
        ///</summary>
        [TestMethod()]
        public void IsDirtyTest()
        {
            ITDPSessionAware target = CreateITDPSessionAware(); 
            bool expected = true; 
            bool actual;
            target.IsDirty = expected;
            actual = target.IsDirty;
            Assert.AreEqual(expected, actual);
        }
    }
}
