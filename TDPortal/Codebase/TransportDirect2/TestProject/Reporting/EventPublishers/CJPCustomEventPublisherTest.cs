// *********************************************** 
// NAME             : CJPCustomEventPublisherTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: CJPCustomEventPublisher test
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
    ///This is a test class for CJPCustomEventPublisherTest and is intended
    ///to contain all CJPCustomEventPublisherTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CJPCustomEventPublisherTest
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
            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
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
        ///A test for CJPCustomEventPublisher Constructor
        ///</summary>
        [TestMethod()]
        public void CJPCustomEventPublisherConstructorTest()
        {
            string identifier = "CJPDB";
            SqlHelperDatabase targetDatabase = SqlHelperDatabase.ReportStagingDB;

            try
            {
                CJPCustomEventPublisher target = new CJPCustomEventPublisher(identifier, targetDatabase);

                Assert.IsNotNull(target, "Expected CJPCustomEventPublisher object to have been created");
                Assert.IsTrue(target.Identifier == identifier, "Expected CJPCustomEventPublisher identifier to equal the id passed in");
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("Exception thrown in CJPCustomEventPublisher constructor {0}", ex.Message));
            }

            // For code coveragae, test for Default database
            targetDatabase = SqlHelperDatabase.DefaultDB;

            try
            {
                CJPCustomEventPublisher target = new CJPCustomEventPublisher(identifier, targetDatabase);

                Assert.IsNotNull(target, "Expected CJPCustomEventPublisher object to have been created");
                Assert.IsTrue(target.Identifier == identifier, "Expected CJPCustomEventPublisher identifier to equal the id passed in");
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("Exception thrown in CJPCustomEventPublisher constructor {0}", ex.Message));
            }

            // Error test
            identifier = "DOESNOTEXIST";
            targetDatabase = SqlHelperDatabase.SqlHelperDatabaseEnd;

            try
            {
                CJPCustomEventPublisher target = new CJPCustomEventPublisher(identifier, targetDatabase);
                Assert.Fail("Exception expected");
            }
            catch
            {
                // Exception expected
            }
        }

        /// <summary>
        ///A test for CJPCustomEventPublisher WriteEvent
        ///</summary>
        [TestMethod()]
        public void CJPCustomEventPublisherWriteEventTest()
        {
            string identifier = "CJPDB";
            SqlHelperDatabase targetDatabase = SqlHelperDatabase.ReportStagingDB;
            CJPCustomEventPublisher target = new CJPCustomEventPublisher(identifier, targetDatabase);

            JourneyWebRequestEvent jwre = new JourneyWebRequestEvent("TestSession", "TestRequest", DateTime.Now, JourneyWebRequestType.Journey, "L", true, false);
            target.WriteEvent(jwre);

            LocationRequestEvent lre = new LocationRequestEvent("TestRequest", JourneyPrepositionCategory.FirstLastService, "123", "987");
            target.WriteEvent(lre);

            InternalRequestEvent ire = new InternalRequestEvent("TestSession", "TestRequest", DateTime.Now, InternalRequestType.AirTTBO, "AB", true, false);
            target.WriteEvent(ire);

            // Test publishing LogEvent
            try
            {
                LogEventTest le = new LogEventTest("TestSessionId");
                target.WriteEvent(le);

                Assert.Fail("Expected CJPCustomEventPublisher to throw exception for unknown LogEvent type");
            }
            catch (TDPException tdpEx)
            {
                // Expect exception to be thrown
                Assert.IsTrue(tdpEx.Identifier == TDPExceptionIdentifier.RDPUnsupportedCJPCustomEventPublisherEvent,
                    "Expected CJPCustomEventPublisher to thrown unknown custom event error with TDPExceptionIdentifier.RDPUnsupportedCJPCustomEventPublisherEvent");
            }
            
        }
    }
}
