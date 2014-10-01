using TDP.Common.ServiceDiscovery;
using TDP.UserPortal.CCAgent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.DatabaseInfrastructure;

namespace TDP.TestProject.CommandAndControl
{


    /// <summary>
    ///This is a test class for CommandAndControlAgentServiceTest and is intended
    ///to contain all CommandAndControlAgentServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CommandAndControlAgentServiceTest
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
            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
            TDPServiceDiscovery.Init(new TestInitialisation());
        }
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Tests the service (start / poll / stop)
        ///</summary>
        [TestMethod()]
        public void AgentServiceTest()
        {
            CommandAndControlAgentService_Accessor target = new CommandAndControlAgentService_Accessor();

            // Start the service (which will start polling)
            target.OnStart(new string[0]);

            // Let it instantiate polling before calling the data changed receiver
            System.Threading.Thread.Sleep(10000);

            object sender = null;
            ChangedEventArgs e = new ChangedEventArgs("CommandControl");
            target.DataChangedNotificationReceived(sender, e);

            // Let it poll for a while (60 second polling interval)
            System.Threading.Thread.Sleep(100000);

            // Stop the service
            target.OnStop();
        }

    }
}
