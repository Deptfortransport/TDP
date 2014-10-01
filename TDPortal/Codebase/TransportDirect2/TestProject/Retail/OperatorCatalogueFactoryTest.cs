// *********************************************** 
// NAME             : OperatorCatalogueFactoryTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 09 Sep 2013
// DESCRIPTION  	: OperatorCatalogueFactory test
// ************************************************
// 
                
using TDP.UserPortal.Retail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.DatabaseInfrastructure;
using TDP.Common.ServiceDiscovery;

namespace TDP.TestProject.Retail
{
    
    
    /// <summary>
    ///This is a test class for OperatorCatalogueFactoryTest and is intended
    ///to contain all OperatorCatalogueFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OperatorCatalogueFactoryTest
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
        ///A test for OperatorCatalogueFactory Constructor
        ///</summary>
        [TestMethod()]
        public void OperatorCatalogueFactoryConstructorTest()
        {
            OperatorCatalogueFactory target = new OperatorCatalogueFactory();

            Assert.IsNotNull(target, "Expected OperatorCatalogueFactory object to be created");
        }

        /// <summary>
        ///A test for Get
        ///</summary>
        [TestMethod()]
        public void OperatorCatalogueFactoryGetTest()
        {
            OperatorCatalogueFactory target = new OperatorCatalogueFactory();

            object actual = target.Get();

            Assert.IsNotNull(target, "Expected OperatorCatalogueFactory object to be created");

            OperatorCatalogue operatorCatalogue = (OperatorCatalogue)actual;

            Assert.IsNotNull(operatorCatalogue, "Expected Get to return an OperatorCatalogue object");
        }

        /// <summary>
        ///A test for RegisterForChangeNotification
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.retail.dll")]
        public void OperatorCatalogueFactoryRegisterForChangeNotificationTest()
        {
            // Reset 
            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
            TDPServiceDiscovery.Init(new TestInitialisationPropertiesLogging());

            OperatorCatalogueFactory_Accessor target = new OperatorCatalogueFactory_Accessor();
            bool expected = true;

            try
            {
                bool actual = target.RegisterForChangeNotification();

                // Change notification should not be enabled here, so fail
                Assert.Fail("Change notification should not be enabled at this point");
            }
            catch
            {
                // Expecting exception, pass
            }

            using (DataChangeNotificationFactory dcnf = new DataChangeNotificationFactory())
            {
                // Ensure data notification is running
                TDPServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.DataChangeNotification, dcnf);

                try
                {
                    bool actual = target.RegisterForChangeNotification();
                    Assert.AreEqual(expected, actual);
                }
                catch (Exception ex)
                {
                    Assert.Fail(string.Format("Exception thrown, {0}", ex.Message));
                }
            }
        }

        /// <summary>
        ///A test for DataChangedNotificationReceived
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.retail.dll")]
        public void OperatorCatalogueFactoryDataChangedNotificationReceivedTest()
        {
            using (DataChangeNotificationFactory dcnf = new DataChangeNotificationFactory())
            {
                // Ensure data notification is running
                TDPServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.DataChangeNotification, dcnf);

                OperatorCatalogueFactory_Accessor target = new OperatorCatalogueFactory_Accessor();
                object sender = null;
                ChangedEventArgs e = new ChangedEventArgs("OperatorCatalogue");

                try
                {
                    target.DataChangedNotificationReceived(sender, e);
                }
                catch (Exception ex)
                {
                    Assert.Fail(string.Format("Exception thrown, {0}", ex.Message));
                }
            }
        }
    }
}
