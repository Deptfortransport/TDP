// *********************************************** 
// NAME             : TDPCustomEventPublisherTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 18 Apr 2011
// DESCRIPTION  	: TDPCustomEventPublisherTest test class
// ************************************************
// 

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDP.Common;
using TDP.Common.DatabaseInfrastructure;
using TDP.Common.ServiceDiscovery;
using TDP.Reporting.EventPublishers;
using TDP.Reporting.Events;
using TDP.Common.EventLogging;

namespace TDP.TestProject.Reporting.EventPublishers
{
    
    
    /// <summary>
    ///This is a test class for TDPCustomEventPublisherTest and is intended
    ///to contain all TDPCustomEventPublisherTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPCustomEventPublisherTest
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
        ///A test for TDPCustomEventPublisher Constructor
        ///</summary>
        [TestMethod()]
        public void TDPCustomEventPublisherConstructorTest()
        {
            string identifier = "TDPDB";
            SqlHelperDatabase targetDatabase = SqlHelperDatabase.ReportStagingDB;

            try
            {
                TDPCustomEventPublisher target = new TDPCustomEventPublisher(identifier, targetDatabase);

                Assert.IsNotNull(target, "Expected TDPCustomEventPublisher object to have been created");
                Assert.IsTrue(target.Identifier == identifier, "Expected TDPCustomEventPublisher identifier to equal the id passed in");
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("Exception thrown in TDPCustomEventPublisher constructor {0}", ex.Message));
            }

            // For code coveragae, test for Default database
            targetDatabase = SqlHelperDatabase.DefaultDB;

            try
            {
                TDPCustomEventPublisher target = new TDPCustomEventPublisher(identifier, targetDatabase);

                Assert.IsNotNull(target, "Expected TDPCustomEventPublisher object to have been created");
                Assert.IsTrue(target.Identifier == identifier, "Expected TDPCustomEventPublisher identifier to equal the id passed in");
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("Exception thrown in TDPCustomEventPublisher constructor {0}", ex.Message));
            }

            // Error test
            identifier = "DOESNOTEXIST";
            targetDatabase = SqlHelperDatabase.SqlHelperDatabaseEnd;

            try
            {
                TDPCustomEventPublisher target = new TDPCustomEventPublisher(identifier, targetDatabase);
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
        public void TDPCustomEventPublisherWriteEventTest()
        {
            string identifier = "TDPDB";
            SqlHelperDatabase targetDatabase = SqlHelperDatabase.ReportStagingDB;
            TDPCustomEventPublisher target = new TDPCustomEventPublisher(identifier, targetDatabase);

            // Test publishing 
            
            CyclePlannerRequestEvent cprqe = new CyclePlannerRequestEvent("TestRequestId", false, "TestSession");
            target.WriteEvent(cprqe);

            CyclePlannerResultEvent cprse = new CyclePlannerResultEvent("TestRequestId", JourneyPlanResponseCategory.Results, false, "TestSession");
            target.WriteEvent(cprse);

            DataGatewayEvent dge = new DataGatewayEvent("TestFeed", "TestSession", "TestFile", DateTime.Now, DateTime.Now, true, 0);
            target.WriteEvent(dge);

            GazetteerEvent ge = new GazetteerEvent(GazetteerEventCategory.GazetteerAddress, DateTime.Now, "TestSession", false);
            target.WriteEvent(ge);

            GISQueryEvent gise = new GISQueryEvent(GISQueryType.FindExchangePointsInRadius, DateTime.Now, "TestSession");
            target.WriteEvent(gise);


            // Get all modes
            System.Collections.Generic.List<TDPModeType> modes = new System.Collections.Generic.List<TDPModeType>();
            foreach (TDPModeType mode in Enum.GetValues(typeof(TDPModeType)))
            {
                modes.Add(mode);
            }
            
            JourneyPlanRequestEvent jprqe = new JourneyPlanRequestEvent("TestRequestId", modes, false, "TestSession");
            target.WriteEvent(jprqe);

            JourneyPlanResultsEvent jprse = new JourneyPlanResultsEvent("TestRequestId", JourneyPlanResponseCategory.Results, false, "TestSession");
            target.WriteEvent(jprse);

            LandingPageEntryEvent lpee = new LandingPageEntryEvent("TestPartner", LandingPageService.TDP, "TestSession", false);
            target.WriteEvent(lpee);

            PageEntryEvent pee = new PageEntryEvent(PageId.Empty, "TestSession", false);
            target.WriteEvent(pee);

            BasePageEntryEvent bpe = new BasePageEntryEvent("TestSession", false, "Test");
            target.WriteEvent(pee);
            
            ReferenceTransactionEvent rte = new ReferenceTransactionEvent("TestCategory", false, DateTime.Now, "TestSession", true, "TestMachine");
            target.WriteEvent(rte);

            RepeatVisitorEvent rve = new RepeatVisitorEvent(RepeatVisitorType.VisitorNew, "TestSessionIdOld", "TestSessionIdNew", DateTime.Now.Subtract(new TimeSpan(1, 0, 0)), "JourneyPlannerInput", "localhost", "Firefox");
            target.WriteEvent(rve);

            RetailerHandoffEvent rhe = new RetailerHandoffEvent("TestRetailer", "TestSession", false);
            target.WriteEvent(rhe);

            StopEventRequestEvent sere = new StopEventRequestEvent("TestRequestId", DateTime.Now, StopEventRequestType.Time, true);
            target.WriteEvent(sere);

            // Commented out until NoResultEvent supported in TDP
            //NoResultsEvent nre = new NoResultsEvent(DateTime.Now, "TestSession",false);
            //target.WriteEvent(nre);
                     
            WorkloadEvent wle = new WorkloadEvent(DateTime.Now, 1, -999);
            target.WriteEvent(wle);

            // Test publishing LogEvent
            try
            {
                LogEventTest le = new LogEventTest("TestSessionId");
                target.WriteEvent(le);

                Assert.Fail("Expected TDPCustomEventPublisher to throw exception for unknown LogEvent type");
            }
            catch (TDPException tdpEx)
            {
                // Expect exception to be thrown
                Assert.IsTrue(tdpEx.Identifier == TDPExceptionIdentifier.RDPUnsupportedCustomEventPublisherEvent,
                    "Expected TDPCustomEventPublisher to thrown unknown custom event error with TDPExceptionIdentifier.RDPUnsupportedCustomEventPublisherEvent");
            }
        }

    }

    #region Protected LogEventTest class

    /// <summary>
    /// LogEventTest class
    /// </summary>
    [Serializable()]
    public class LogEventTest : TDPCustomEvent
    {
        public LogEventTest(string sessionIdNew)
            : base(sessionIdNew, false)
        {
        }
    }

    #endregion
}
