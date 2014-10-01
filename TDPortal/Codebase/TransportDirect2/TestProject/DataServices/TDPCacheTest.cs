// *********************************************** 
// NAME             : TDPCacheTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: TDPCache test 
// ************************************************
// 
                
using TDP.Common.DataServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Caching;

namespace TDP.TestProject.Common.DataServices
{
    
    
    /// <summary>
    ///This is a test class for TDPCacheTest and is intended
    ///to contain all TDPCacheTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPCacheTest
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
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void TDPCacheAddTest()
        {
            TDPCache target = new TDPCache();
            string key = "CacheKey1";
            string cachable = "Cache string";
            target.Add(key, cachable);

            Assert.IsNotNull(target[key]);
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void TDPCacheAddTest1()
        {
            TDPCache target = new TDPCache();
            string key = "CacheKey2";
            string cachable = "Cache string";
            DateTime absoluteExpiry = DateTime.Now.AddMinutes(1);
            target.Add(key, cachable, absoluteExpiry);

            Assert.IsNotNull(target[key]);
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void TDPCacheAddTest2()
        {
            TDPCache target = new TDPCache();
            string key = "CacheKey3";
            string cachable = "Cache string";
            TimeSpan delay = new TimeSpan(0, 1, 0);
            target.Add(key, cachable, delay);

            Assert.IsNotNull(target[key]);
        }

        /// <summary>
        ///A test for Get
        ///</summary>
        [TestMethod()]
        public void TDPCacheGetTest()
        {
            TDPCache target = new TDPCache();
            object actual;
            actual = target.Get();
            
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        [TestMethod()]
        public void TDPCacheRemoveTest()
        {
            TDPCache target = new TDPCache();
            string key = "CacheKey4";
            string cachable = "Cache string";
            target.Add(key, cachable);

            bool actual;
            actual = target.Remove(key);

            Assert.IsNull(target[key]);
        }

        /// <summary>
        ///A test for WebCache
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.dataservices.dll")]
        public void TDPCacheWebCacheTest()
        {
            TDPCache_Accessor target = new TDPCache_Accessor();
            Cache actual;
            actual = target.WebCache;

            Assert.IsNotNull(actual);
        }
    }
}
