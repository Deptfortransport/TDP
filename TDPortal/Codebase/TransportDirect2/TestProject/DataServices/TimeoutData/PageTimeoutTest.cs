// *********************************************** 
// NAME             : PageTimeoutTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: PageTimeout test
// ************************************************
// 

using TDP.Common.DataServices.TimeoutData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TDP.Common;

namespace TDP.TestProject.Common.DataServices.TimeoutData
{
    
    
    /// <summary>
    ///This is a test class for PageTimeoutTest and is intended
    ///to contain all PageTimeoutTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PageTimeoutTest
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
        ///A test for PageTimeout Constructor
        ///</summary>
        [TestMethod()]
        public void PageTimeoutConstructorTest()
        {
            PageTimeout target = new PageTimeout();

            PageId pageId = PageId.Homepage;
            List<string> showTimeoutControlIds = new List<string>();
            List<string> hideTimeoutControlIds = new List<string>();

            showTimeoutControlIds.Add("Test1");
            hideTimeoutControlIds.Add("Test2");

            target.PageId = pageId;
            target.ShowTimeoutControlIds = showTimeoutControlIds;
            target.HideTimeoutControlIds = hideTimeoutControlIds;

            Assert.AreEqual(pageId, target.PageId);
            Assert.AreEqual(showTimeoutControlIds, target.ShowTimeoutControlIds);
            Assert.AreEqual(hideTimeoutControlIds, target.HideTimeoutControlIds);

        }
    }
}
