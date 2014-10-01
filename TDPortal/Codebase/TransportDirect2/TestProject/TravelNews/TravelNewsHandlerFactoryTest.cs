// *********************************************** 
// NAME             : TravelNewsHandlerFactoryTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 02 May 2011
// DESCRIPTION  	: TravelNewsHandlerFactoryTest test
// ************************************************
// 
                
using TDP.UserPortal.TravelNews;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.DatabaseInfrastructure;
using TDP.Common.ServiceDiscovery;

namespace TDP.TestProject.TravelNews
{
    
    
    /// <summary>
    ///This is a test class for TravelNewsHandlerFactoryTest and is intended
    ///to contain all TravelNewsHandlerFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TravelNewsHandlerFactoryTest
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
        ///A test for TravelNewsHandlerFactory Constructor
        ///</summary>
        [TestMethod()]
        public void TravelNewsHandlerFactoryConstructorTest()
        {
            TravelNewsHandlerFactory target = new TravelNewsHandlerFactory();

            Assert.IsNotNull(target, "Expected TravelNewsHandlerFactory to be not null");
        }

        /// <summary>
        ///A test for DataChangedNotificationReceived
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.travelnews.dll")]
        public void TravelNewsDataChangedNotificationReceivedTest()
        {
            TravelNewsHandlerFactory_Accessor target = new TravelNewsHandlerFactory_Accessor();
            object sender = null;
            ChangedEventArgs e = new ChangedEventArgs("TravelNews");

            try
            {
                target.DataChangedNotificationReceived(sender, e);
            }
            catch (Exception ex)
            {
                Assert.Fail(
                    string.Format("Exception was thrown when performing the DataChangedNotificationReceivedTest, exception: {0}", ex));
            }
        }

        /// <summary>
        ///A test for Get
        ///</summary>
        [TestMethod()]
        public void TravelNewsGetTest()
        {
            TravelNewsHandlerFactory target = new TravelNewsHandlerFactory();
            
            object actual = target.Get();

            Assert.IsTrue(actual is TravelNewsHandler, "Expected TravelNewsHandler to be returned");
        }

        /// <summary>
        ///A test for RegisterForChangeNotification
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.travelnews.dll")]
        public void TravelNewsRegisterForChangeNotificationTest()
        {
            // Reset 
            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
            TDPServiceDiscovery.Init(new TestInitialisationPropertiesLogging());

            TravelNewsHandlerFactory_Accessor target = new TravelNewsHandlerFactory_Accessor();

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

                    Assert.IsTrue(actual, "RegisterForChangeNotification failed");
                }
                catch (Exception ex)
                {
                    Assert.Fail(
                        string.Format("Exception was thrown when performing the RegisterForChangeNotification, exception: {0}", ex));
                }
            }
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.travelnews.dll")]
        public void TravelNewsUpdateTest()
        {
            TravelNewsHandlerFactory_Accessor target = new TravelNewsHandlerFactory_Accessor();

            try
            {
                target.Update();
            }
            catch (Exception ex)
            {
                Assert.Fail(
                    string.Format("Exception was thrown when performing the TravelNews Update, exception: {0}", ex));
            }
        }
    }
}
