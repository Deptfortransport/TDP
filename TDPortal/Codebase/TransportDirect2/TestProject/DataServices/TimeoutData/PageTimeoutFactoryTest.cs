// *********************************************** 
// NAME             : PageTimeoutFactoryTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: PageTimeoutFactory test
// ************************************************
// 
                
using TDP.Common.DataServices.TimeoutData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.ServiceDiscovery;

namespace TDP.TestProject.Common.DataServices.TimeoutData
{
    
    
    /// <summary>
    ///This is a test class for PageTimeoutFactoryTest and is intended
    ///to contain all PageTimeoutFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PageTimeoutFactoryTest
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
        ///A test for PageTimeoutFactory Constructor
        ///</summary>
        [TestMethod()]
        public void PageTimeoutFactoryConstructorTest()
        {
            PageTimeoutFactory target = new PageTimeoutFactory();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for Get
        ///</summary>
        [TestMethod()]
        public void PageTimeoutGetTest()
        {
            PageTimeoutFactory target = new PageTimeoutFactory();
            object actual;
            actual = target.Get();
            Assert.IsInstanceOfType(actual, typeof(PageTimeoutData));
        }
    }
}
