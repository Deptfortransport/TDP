﻿using TDP.UserPortal.JourneyPlanRunner;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.UserPortal.JourneyControl;
using TDP.UserPortal.SessionManager;
using TDP.Common.ServiceDiscovery;

using JC = TDP.UserPortal.JourneyControl;
using JPR = TDP.UserPortal.JourneyPlanRunner;
using Logger = System.Diagnostics.Trace;
using TDP.Common.PropertyManager;
using TDP.Common.LocationService;
using TDP.Common;
using TDP.UserPortal.StateServer;

namespace TDP.TestProject.JourneyPlanRunner
{
    
    
    /// <summary>
    ///This is a test class for StopEventRunnerCallerTest and is intended
    ///to contain all StopEventRunnerCallerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StopEventRunnerCallerTest
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
        [TestInitialize()]
        public void TestInitialize()
        {

            TDPServiceDiscovery.ResetServiceDiscoveryForTest();

            TDPServiceDiscovery.Init(new TestInitialisation());

            MockSessionFactory.ClearSession();

            TDPServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.SessionManager, new MockSessionFactory());

            TDPServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.JourneyPlanRunnerCaller, new JPR.JourneyPlanRunnerCallerFactory());

            TDPServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CJP, new CJPFactory());

            TDPServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CJPManager, new CjpManagerFactory());

            TDPServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.StopEventRunnerCaller, new StopEventRunnerCallerFactory());

            TDPServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.StopEventManager, new StopEventManagerFactory());
        }

        //
        //Use ClassCleanup to run code after all tests in a class have run
        [TestCleanup()]
        public void TestCleanup()
        {
            MockSessionFactory.ClearSession();
            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
        }

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
        ///A test for CallStopEvent
        ///</summary>
        [TestMethod()]
        public void CallStopEventTest()
        {
            StopEventRunnerCaller target = new StopEventRunnerCaller();
            ITDPJourneyRequest journeyRequest = InitialiseValidStopEventRequest();
            string sessionID = MockSessionFactory.mockSessionId;
            string lang = "en";
            target.CallStopEvent(journeyRequest, sessionID, lang);
            
            using (TDPStateServer stateServer = new TDPStateServer())
            {

                // Get the TDPResultManager
                object objResultManager = stateServer.Read(sessionID, SessionManagerKey.KeyStopEventResultManager.ID);

                // result manager should have never been created for this test
                Assert.IsNotNull(objResultManager);

                Assert.IsInstanceOfType(objResultManager, typeof(TDPStopEventResultManager));

                TDPStopEventResultManager resultManager = (TDPStopEventResultManager)objResultManager;

                ITDPJourneyResult result = resultManager.GetTDPJourneyResult(journeyRequest.JourneyRequestHash);

                Assert.IsNotNull(result);

            } // StateServer will be disposed, any out
        }

        /// <summary>
        ///A test for InvokeStopEvent with journey request passed as null
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.journeyplanrunner.dll")]
        public void InvokeStopEventTest_NullJourneyRequest()
        {
            StopEventRunnerCaller_Accessor target = new StopEventRunnerCaller_Accessor();
            ITDPJourneyRequest journeyRequest = null;
            string sessionID = MockSessionFactory.mockSessionId;
            string lang = "en";
            target.InvokeStopEvent(journeyRequest, sessionID, lang);


            using (TDPStateServer stateServer = new TDPStateServer())
            {

                // Get the TDPResultManager
                object objResultManager = stateServer.Read(sessionID, SessionManagerKey.KeyStopEventResultManager.ID);

                // result manager should have never been created for this test
                Assert.IsNull(objResultManager);


            } // StateServer will be disposed, any out
           
        }

        #region Private methods

        /// <summary>
        /// Initialises a stop event request with standard request values
        /// for an outward and return journey
        /// </summary>
        /// <returns></returns>
        private ITDPJourneyRequest InitialiseValidStopEventRequest()
        {
            IPropertyProvider pp = Properties.Current;

            ITDPJourneyRequest request = new TDPJourneyRequest();

            request.JourneyRequestHash = "Test";

            LocationService locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);

            TDPLocation origin = new TDPLocation("Tower Millennium Pier", TDPLocationType.Station, TDPLocationType.Unknown, "9300TMP");

            TDPLocation destination = new TDPLocation("Greenwich Pier ", TDPLocationType.Station, TDPLocationType.Unknown, "9300GNW");
            // Set the Locality as it needed by the CJP
            destination.Locality = "E0034328";

            request.Origin = origin;
            request.Destination = destination;

            // Fix to ensure planning for 2012
            DateTime dtOutward = DateTime.Now;
            DateTime dtOutward2012 = new DateTime(2012, 8, 1, 12, 0, 0);
            if (dtOutward < dtOutward2012)
            {
                dtOutward = dtOutward2012;
            }

            request.OutwardDateTime = dtOutward;
            request.ReturnDateTime = dtOutward.AddHours(3);
            request.OutwardArriveBefore = true;
            request.ReturnArriveBefore = false;
            request.IsReturnRequired = true;

            request.AccessiblePreferences = new TDPAccessiblePreferences();

            // Ferry 
            request.PlannerMode = TDPJourneyPlannerMode.RiverServices;
            request.Modes = new System.Collections.Generic.List<TDPModeType>(
                new TDPModeType[1] { TDPModeType.Ferry });

            // Stop event specific
            request.Sequence = 3;

            request.JourneyRequestHash = request.GetTDPHashCode().ToString();

            return request;
        }

        

        #endregion

        
    }
}
