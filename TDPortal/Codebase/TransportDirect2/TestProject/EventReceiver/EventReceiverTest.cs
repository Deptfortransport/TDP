
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.ServiceDiscovery;
using TDP.Common.PropertyManager;
using TDP.Common.EventLogging;
using TDP.TestProject.EventLogging.MockObjects;
using System.Messaging;
using ER = TDP.Reporting.EventReceiver;
using TDP.Reporting.Events;

namespace TDP.TestProject.EventReceiver
{
    
    
    /// <summary>
    ///This is a test class for EventReceiverTest and is intended
    ///to contain all EventReceiverTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EventReceiverTest
    {

        private static object lockObject = new object();

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

            string queueName = string.Format(@"{0}\Private$\ERTestQueue1$", Environment.MachineName);

            if (!MessageQueue.Exists(queueName))
            {
                using (MessageQueue newQueue1 = MessageQueue.Create(queueName, false)) { }
            }

            queueName = string.Format(@"{0}\Private$\ERTestQueue2$", Environment.MachineName);

            if (!MessageQueue.Exists(queueName))
            {
                using (MessageQueue newQueue1 = MessageQueue.Create(queueName, false)) { }
            }
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            PurgeQueues();
        }
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
        /// Purges messages in the test queues
        /// </summary>
        public static void PurgeQueues()
        {
            string queueName = string.Format(@"{0}\Private$\ERTestQueue1$", Environment.MachineName);

            if (MessageQueue.Exists(queueName))
            {
                using (MessageQueue queue1 = new MessageQueue(queueName))
                {
                    queue1.Purge();
                }
            }

            queueName = string.Format(@"{0}\Private$\ERTestQueue2$", Environment.MachineName);

            if (MessageQueue.Exists(queueName))
            {
                using (MessageQueue queue2 = new MessageQueue(queueName))
                {
                    queue2.Purge();
                }
            }
        }

        /// <summary>
        ///A test for EventReceiver Constructor
        ///</summary>
        [TestMethod()]
        public void EventReceiverConstructorTest()
        {
            using (ER.EventReceiver target = new ER.EventReceiver())
            {
                Assert.IsNotNull(target, "Expected an object to be returned");

                try
                {
                    // need to run or dispose fails
                    target.Run();
                }
                catch
                {
                    // Ignore exceptions
                }
            }
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EventReceiver2.exe")]
        public void DisposeTest()
        {
            // Use lock to prevent process being stopped error as tests share the same test MessageQueue
            lock (lockObject)
            {
                ER.EventReceiver_Accessor target = new ER.EventReceiver_Accessor();
                bool disposing = true;

                try
                {
                    target.Run(new MockPropertiesGoodProperties());
                }
                finally
                {
                    target.Dispose(disposing);
                }

                Assert.IsTrue(target.messageQueueList.Count == 0, "Expected message queues to be removed");
            }
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        public void DisposeTest1()
        {
            // Use lock to prevent process being stopped error as tests share the same test MessageQueue
            lock (lockObject)
            {
                // Method doesn't do anything so can't test for a result
                ER.EventReceiver_Accessor target = new ER.EventReceiver_Accessor();

                try
                {
                    target.Run(new MockPropertiesGoodProperties());
                }
                finally
                {
                    target.Dispose();
                }

                Assert.IsTrue(target.messageQueueList.Count == 0, "Expected message queues to be removed");
            }
        }

        /// <summary>
        ///A test for Finalize
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EventReceiver2.exe")]
        public void FinalizeTest()
        {
            // Nothing to check
            using (ER.EventReceiver_Accessor target = new ER.EventReceiver_Accessor())
            {
                target.Finalize();
            }
        }

        /// <summary>
        ///A test for InitQueueList
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EventReceiver2.exe")]
        public void InitQueueListTest()
        {
            // Use lock to prevent process being stopped error as tests share the same test MessageQueue
            lock (lockObject)
            {
                using (ER.EventReceiver_Accessor target = new ER.EventReceiver_Accessor())
                {
                    IPropertyProvider properties = new MockPropertiesGoodProperties();
                    target.InitQueueList(properties);
                    Assert.IsTrue(target.messageQueueList.Count > 0, "Expected the message queue list to have been initialised");
                }
            }
        }

        /// <summary>
        ///A test for OnDefaultPublisherCalled
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EventReceiver2.exe")]
        public void OnDefaultPublisherCalledTest()
        {
            // Use lock to prevent process being stopped error as tests share the same test MessageQueue
            lock (lockObject)
            {
                using (ER.EventReceiver_Accessor target = new ER.EventReceiver_Accessor())
                {
                    target.Run(new MockPropertiesGoodProperties());
                    DefaultPublisherCalledEventArgs e = new DefaultPublisherCalledEventArgs(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, "Event message"));
                    object sender = null;
                    target.OnDefaultPublisherCalled(sender, e);
                    Assert.IsTrue(target.messageQueueList.Count == 0, "Expected message queues to be removed");
                }
            }
        }

        /// <summary>
        ///A test for Receive
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EventReceiver2.exe")]
        public void ReceiveTest()
        {
            // Use lock to prevent process being stopped error as tests share the same test MessageQueue
            lock (lockObject)
            {
                using (ER.EventReceiver_Accessor target = new ER.EventReceiver_Accessor())
                {
                    target.Run(new MockPropertiesGoodProperties());

                    JourneyPlanRequestEvent je = new JourneyPlanRequestEvent("RequestId", new System.Collections.Generic.List<TDP.Common.TDPModeType> { TDP.Common.TDPModeType.Rail }, true, "SessionId");
                    OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, "Operational event message");
                    string queueName = string.Format(@"{0}\Private$\ERTestQueue1$", Environment.MachineName);

                    // empty the queue in case any existing events exist
                    using (MessageQueue queue = new MessageQueue(queueName))
                    {
                        queue.Formatter = new BinaryMessageFormatter();
                        queue.Purge();

                        // create a new queue publisher
                        using (QueuePublisher queuePublisher =
                             new QueuePublisher("Identifier", MessagePriority.Normal, queueName, true))
                        {
                            queuePublisher.WriteEvent(je);
                            queuePublisher.WriteEvent(oe);
                        }

                        System.Threading.Thread.Sleep(5000);

                        Assert.IsTrue(queue.GetAllMessages().Length == 0, "Expected the message queue to be empty.");
                    }
                }
            }
        }

        /// <summary>
        ///A test for RecoverFromRemoteException
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EventReceiver2.exe")]
        public void RecoverFromRemoteExceptionTest()
        {
            // Use lock to prevent process being stopped error as tests share the same test MessageQueue
            lock (lockObject)
            {
                using (ER.EventReceiver_Accessor target = new ER.EventReceiver_Accessor())
                {
                    target.Run(new MockPropertiesGoodProperties());
                    target.RecoverFromRemoteException();
                    Assert.IsTrue(target.messageQueueList.Count > 0, "Expected queues to have been re-initialised");
                }
            }
        }

        /// <summary>
        ///A test for Run
        ///</summary>
        [TestMethod()]
        public void RunTest()
        {
            using (ER.EventReceiver_Accessor target = new ER.EventReceiver_Accessor())
            {
                target.Run();
                Assert.IsTrue(target.messageQueueList.Count > 0, "Expected the queues to have been initialised");
            }
        }

        /// <summary>
        ///A test for Run
        ///</summary>
        [TestMethod()]
        public void RunTest1()
        {
            // Use lock to prevent process being stopped error as tests share the same test MessageQueue
            lock (lockObject)
            {
                using (ER.EventReceiver_Accessor target = new ER.EventReceiver_Accessor())
                {
                    IPropertyProvider pp = new MockPropertiesGoodProperties();
                    target.Run(pp);
                    Assert.IsTrue(target.messageQueueList.Count > 0, "Expected message queues to be initialised");
                }
            }
        }

        /// <summary>
        ///A test for SetupQueues
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EventReceiver2.exe")]
        public void SetupQueuesTest()
        {
            // Use lock to prevent process being stopped error as tests share the same test MessageQueue
            lock (lockObject)
            {
                using (ER.EventReceiver_Accessor target = new ER.EventReceiver_Accessor())
                {
                    target.InitQueueList(new MockPropertiesGoodProperties());
                    target.SetupQueues();
                    Assert.IsNotNull(target.messageQueueList[0].Formatter, "Expected queues to be setup");
                }
            }
        }

        /// <summary>
        ///A test for StopMessageQueues
        ///</summary>
        [TestMethod()]
        [DeploymentItem("EventReceiver2.exe")]
        public void StopMessageQueuesTest()
        {
            // Use lock to prevent process being stopped error as tests share the same test MessageQueue
            lock (lockObject)
            {
                using (ER.EventReceiver_Accessor target = new ER.EventReceiver_Accessor())
                {
                    target.Run(new MockPropertiesGoodProperties());
                    target.StopMessageQueues();
                    Assert.IsTrue(target.messageQueueList.Count == 0, "Expected message queues to have been removed");
                }
            }
        }
    }
}
