// *********************************************** 
// NAME             : PageTimeoutDataTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: PageTimeoutData test
// ************************************************
// 

using TDP.Common.DataServices.TimeoutData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common;
using System.Xml.Schema;
using TDP.Common.ServiceDiscovery;

namespace TDP.TestProject.Common.DataServices.TimeoutData
{
    
    
    /// <summary>
    ///This is a test class for PageTimeoutDataTest and is intended
    ///to contain all PageTimeoutDataTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PageTimeoutDataTest
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
            TDPServiceDiscovery.Init(new TestInitialisationPropertiesLogging());
        }
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
        ///A test for PageTimeoutData Constructor
        ///</summary>
        [TestMethod()]
        public void PageTimeoutDataConstructorTest()
        {
            PageTimeoutData target = new PageTimeoutData();

            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for LoadData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.dataservices.dll")]
        public void PageTimeoutLoadDataTest()
        {
            PageTimeoutData_Accessor target = new PageTimeoutData_Accessor();
            target.LoadData();

            Assert.IsNotNull(target.pageTimeoutDataCache);
            Assert.IsTrue(target.pageTimeoutDataCache.Count > 0);
        }

        /// <summary>
        ///A test for ShowTimeoutMessage
        ///</summary>
        [TestMethod()]
        public void PageTimeoutShowTimeoutMessageTest()
        {
            PageTimeoutData target = new PageTimeoutData();
            
            PageId pageId = PageId.MobileDefault;
            string controlId = "publicTransportLogoBtn";
            bool expected = false;
            bool actual;
            actual = target.ShowTimeoutMessage(pageId, controlId);
            Assert.AreEqual(expected, actual);

            pageId = PageId.MobileDefault;
            controlId = "DOESNOTEXIST";
            expected = true;
            actual = target.ShowTimeoutMessage(pageId, controlId);
            Assert.AreEqual(expected, actual);

            pageId = PageId.Homepage;
            controlId = "DOESNOTEXIST";
            expected = true;
            actual = target.ShowTimeoutMessage(pageId, controlId);
            Assert.AreEqual(expected, actual);
        }
    }
}
