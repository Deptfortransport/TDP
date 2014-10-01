// *********************************************** 
// NAME             : TDPServiceDiscoveryTest.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 27 Jun 2011
// DESCRIPTION  	: This is a test class for TDPServiceDiscovery
// ************************************************
                
                
using TDP.Common.ServiceDiscovery;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common;

namespace TDP.TestProject.ServiceDiscovery
{
    
    
    /// <summary>
    ///This is a test class for TDPServiceDiscoveryTest and is intended
    ///to contain all TDPServiceDiscoveryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPServiceDiscoveryTest
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
            // set the service discover to null
            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
        }
        //
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            // set the service discover to null
            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
        }
        //
        #endregion
                
        /// <summary>
        ///A test for Get
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(TDPException))]
        public void GetTest()
        {
            TDPServiceDiscovery target = new TDPServiceDiscovery();
            string key = "TestKey";
            TestService actual;
            
            // Initalise the service discovery
            TDPServiceDiscovery.Init(new TestServiceFactoryInitialisation());
            actual = target.Get<TestService>(key);
            Assert.IsNotNull(actual);

            // Making sure that the test service factory has loaded the data
            Assert.AreEqual(5, actual.TestData.Count);

            // When Key not found TDPException gets thrown
            string keyNotFound = "TestKeyNotFound";
            actual = target.Get<TestService>(keyNotFound);
        }


        /// <summary>
        ///A test for Init
        ///</summary>
        [TestMethod()]
        public void InitTest()
        {
            TDPServiceDiscovery.Init(new TestServiceFactoryInitialisation());
            TDPServiceDiscovery_Accessor accessor = new TDPServiceDiscovery_Accessor(new PrivateObject(TDPServiceDiscovery.Current));

            Assert.IsNotNull(TDPServiceDiscovery.Current);
            Assert.AreEqual(1, accessor.serviceCache.Count);
            Assert.IsInstanceOfType(accessor.serviceCache["TestKey"], typeof(TestServiceFactory));
        }

        /// <summary>
        ///A test for Initialise
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.servicediscovery.dll")]
        public void InitialiseTest()
        {
            TDPServiceDiscovery_Accessor target = new TDPServiceDiscovery_Accessor();
            IServiceInitialisation initContext = new TestServiceFactoryInitialisation();
            target.Initialise(initContext);

            Assert.AreEqual(1, target.serviceCache.Count);
            Assert.IsInstanceOfType(target.serviceCache["TestKey"], typeof(TestServiceFactory));
        }

        /// <summary>
        ///A test for ResetServiceDiscoveryForTest
        ///</summary>
        [TestMethod()]
        public void ResetServiceDiscoveryForTestTest()
        {
            TDPServiceDiscovery.Init(new TestServiceFactoryInitialisation());

            Assert.IsNotNull(TDPServiceDiscovery.Current);

            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
            Assert.IsNull(TDPServiceDiscovery.Current);
        }

        /// <summary>
        ///A test for SetServiceForTest
        ///</summary>
        [TestMethod()]
        public void SetServiceForTestTest()
        {
            TDPServiceDiscovery target = new TDPServiceDiscovery();
            TDPServiceDiscovery_Accessor accessor = new TDPServiceDiscovery_Accessor(new PrivateObject(target));
            TDPServiceDiscovery_Accessor.current = target;
            string key = "TestKey";
            IServiceFactory serviceFactory = new TestServiceFactory();
            target.SetServiceForTest(key, serviceFactory);

            
            Assert.IsNotNull(TDPServiceDiscovery.Current);
            Assert.AreEqual(1, accessor.serviceCache.Count);
            Assert.IsInstanceOfType(accessor.serviceCache["TestKey"], typeof(TestServiceFactory));

            // Set the service to null
            target.SetServiceForTest(key, null);
            Assert.AreEqual(1, accessor.serviceCache.Count);
            Assert.IsNull(accessor.serviceCache["TestKey"]);           
        }        
    }
}
