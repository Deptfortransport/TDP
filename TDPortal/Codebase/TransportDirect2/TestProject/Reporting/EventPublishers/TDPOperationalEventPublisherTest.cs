// *********************************************** 
// NAME             : TDPOperationalEventPublisherTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 18 Apr 2011
// DESCRIPTION  	: TDPOperationalEventPublisherTest test class
// ************************************************
// 
                
using TDP.Reporting.EventPublishers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.DatabaseInfrastructure;
using TDP.Common.EventLogging;
using TDP.Common.ServiceDiscovery;
using TDP.Common;
using TDP.Reporting.Events;

namespace TDP.TestProject.Reporting.EventPublishers
{
    
    
    /// <summary>
    ///This is a test class for TDPOperationalEventPublisherTest and is intended
    ///to contain all TDPOperationalEventPublisherTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPOperationalEventPublisherTest
    {
        private static TestDataManager testDataManager;

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
            TDPServiceDiscovery.Init(new TestInitialisationPropertiesLogging());

            string test_data = string.Empty;
            string setup_script = string.Empty;
            string clearup_script = @"Reporting\SJPCustomEventPublisherCleanUp.sql";
            string connectionString = @"Server=.\SQLEXPRESS;Initial Catalog=ReportStaging;Trusted_Connection=true";

            // Test data
            testDataManager = new TestDataManager(
                test_data,
                setup_script,
                clearup_script,
                connectionString,
                SqlHelperDatabase.ReportStagingDB);
            testDataManager.Setup();

        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            testDataManager.ClearData();
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
        ///A test for TDPOperationalEventPublisher Constructor
        ///</summary>
        [TestMethod()]
        public void TDPOperationalEventPublisherConstructorTest()
        {
            string identifier = "OPDB";
            SqlHelperDatabase targetDatabase = SqlHelperDatabase.ReportStagingDB;

            try
            {
                TDPOperationalEventPublisher target = new TDPOperationalEventPublisher(identifier, targetDatabase);

                Assert.IsNotNull(target, "Expected TDPOperationalEventPublisher object to have been created");
                Assert.IsTrue(target.Identifier == identifier, "Expected TDPOperationalEventPublisher identifier to equal the id passed in");
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("Exception thrown in TDPOperationalEventPublisher constructor {0}", ex.Message));
            }

            // For code coverage
            string message = string.Format(TDP.Reporting.EventPublishers.Messages.ConstructorFailed, "TEST");

            Assert.IsNotNull(message, "Expected EventPublishers.Message to not be null");

            // Error test
            identifier = "DOESNOTEXIST";
            targetDatabase = SqlHelperDatabase.SqlHelperDatabaseEnd;

            try
            {
                TDPOperationalEventPublisher target = new TDPOperationalEventPublisher(identifier, targetDatabase);
                Assert.Fail("Exception expected");
            }
            catch
            {
                // Exception expected
            }

        }

        /// <summary>
        ///A test for WriteEvent
        ///</summary>
        [TestMethod()]
        public void TDPOperationalEventPublisherWriteEventTest()
        {
            string identifier = "OPDB";
            SqlHelperDatabase targetDatabase = SqlHelperDatabase.ReportStagingDB;
            TDPOperationalEventPublisher target = new TDPOperationalEventPublisher(identifier, targetDatabase);

            // Test publishing 

            OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, "TestOperational");
            target.WriteEvent(oe);

            // Extra long field to enforce truncate
            OperationalEvent oe1 = new OperationalEvent(TDPEventCategory.Business, "TestSessionIdTestSessionIdTestSessionIdTestSessionIdTestSessionIdTestSessionIdTestSessionIdTestSessionId", 
                TDPTraceLevel.Error, "TestOperational");
            target.WriteEvent(oe1);

            // Target object
            TDPException ex = new TDPException("TestException", false, TDPExceptionIdentifier.LSAddressDrillLacksChildren);
            OperationalEvent oe2 = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, "TestOperational", ex);
            target.WriteEvent(oe2);

            ReceivedOperationalEvent roe = new ReceivedOperationalEvent(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, "TestOperational"));
            target.WriteEvent(roe);

            // Test publishing LogEvent
            try
            {
                LogEventTest le = new LogEventTest("TestSessionId");
                target.WriteEvent(le);

                Assert.Fail("Expected TDPOperationalEventPublisher to throw exception for unknown LogEvent type");
            }
            catch (TDPException tdpEx)
            {
                // Expect exception to be thrown
                Assert.IsTrue(tdpEx.Identifier == TDPExceptionIdentifier.RDPUnsupportedOperationalEventPublisherEvent,
                    "Expected TDPOperationalEventPublisher to thrown unknown custom event error with TDPExceptionIdentifier.RDPUnsupportedOperationalEventPublisherEvent");
            }


        }
    }
}
