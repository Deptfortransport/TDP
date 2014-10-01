using TDP.Common.EventLogging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.PropertyManager;
using TDP.TestProject.EventLogging.MockObjects;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using TDP.Common;
using TDP.Common.ServiceDiscovery;

namespace TDP.TestProject.EventLogging
{
    
    
    /// <summary>
    ///This is a test class for TDPTraceSwitchTest and is intended
    ///to contain all TDPTraceSwitchTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPTraceSwitchTest
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
        ///A test for CheckLevel
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.eventlogging.dll")]
        [ExpectedException(typeof(TDPException))]
        public void CheckLevelTest()
        {
            
            IEventPublisher[] customPublishers = new IEventPublisher[2];
            customPublishers[0] = new TDPPublisher1("CustomPublisher1");
            customPublishers[1] = new TDPPublisher2("CustomPublisher2");
            List<string> errors = new List<string>();
            bool expected = true;
            bool actual;

            TDPServiceDiscovery_Accessor.current = new TDPServiceDiscovery();

            MockPropertiesGoodServiceFactory factory = new MockPropertiesGoodServiceFactory(new MockPropertiesGood());
            TDPServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PropertyService, factory);

            MockPropertiesGood goodProperties = (MockPropertiesGood)Properties.Current;

            try
            {
                Trace.Listeners.Add(new TDPTraceListener(goodProperties, customPublishers, errors));

                actual = TDPTraceSwitch_Accessor.CheckLevel(TDPTraceLevel.Error);
                Assert.AreEqual(expected, actual);
                Assert.IsTrue(TDPTraceSwitch.TraceError);

                factory.MockLevelChange1();
                actual = TDPTraceSwitch_Accessor.CheckLevel(TDPTraceLevel.Off);
                Assert.AreEqual(expected, actual);
                Assert.IsFalse(TDPTraceSwitch.TraceError);

                factory.MockLevelChange2();
                actual = TDPTraceSwitch_Accessor.CheckLevel(TDPTraceLevel.Warning);
                Assert.AreEqual(expected, actual);
                Assert.IsTrue(TDPTraceSwitch.TraceWarning);

                factory.MockLevelChange3();
                actual = TDPTraceSwitch_Accessor.CheckLevel(TDPTraceLevel.Info);
                Assert.AreEqual(expected, actual);
                Assert.IsTrue(TDPTraceSwitch.TraceInfo);

                
            }
            catch (TDPException)
            {
                Assert.IsTrue(false);
            }

            TDPTraceSwitch_Accessor.currentLevel = TDPTraceLevel.Undefined;
            actual = TDPTraceSwitch_Accessor.CheckLevel(TDPTraceLevel.Info);
            Assert.AreEqual(expected, actual);
           
            
        }

       
    }
}
