﻿using TDP.UserPortal.JourneyPlanRunner;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.UserPortal.JourneyControl;
using TDP.UserPortal.SessionManager;
using TDP.Common.ServiceDiscovery;
using JC = TDP.UserPortal.JourneyControl;
using JPR = TDP.UserPortal.JourneyPlanRunner;

using Logger = System.Diagnostics.Trace;
using TDP.UserPortal.StateServer;
using TDP.Common.PropertyManager;
using TDP.Common;
using System.Collections.Generic;
using TDP.Common.LocationService;

namespace TDP.TestProject.JourneyPlanRunner
{
    
    
    /// <summary>
    ///This is a test class for CycleJourneyPlanRunnerCallerTest and is intended
    ///to contain all CycleJourneyPlanRunnerCallerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CycleJourneyPlanRunnerCallerTest
    {
        private LocationService_Accessor locationService = new LocationService_Accessor();

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

            TDPServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CyclePlannerManager, new CyclePlannerManagerFactory());

            TDPServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CycleJourneyPlanRunnerCaller, new CycleJourneyPlanRunnerCallerFactory());
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
        ///A test for CallCyclePlanner
        ///</summary>
        [TestMethod()]
        public void CallCyclePlannerTest()
        {
            CycleJourneyPlanRunnerCaller target = new CycleJourneyPlanRunnerCaller();
            ITDPJourneyRequest journeyRequest = InitialiseCycleJourneyRequest();
            string sessionID = MockSessionFactory.mockSessionId; 
            string lang = "en"; 
            target.CallCyclePlanner(journeyRequest, sessionID, lang);

            using (TDPStateServer stateServer = new TDPStateServer())
            {

                // Get the TDPResultManager
                object objResultManager = stateServer.Read(sessionID, SessionManagerKey.KeyResultManager.ID);

                // result manager should have never been created for this test
                Assert.IsNotNull(objResultManager);

                Assert.IsInstanceOfType(objResultManager, typeof(TDPResultManager));

                TDPResultManager resultManager = (TDPResultManager)objResultManager;

                ITDPJourneyResult result = resultManager.GetTDPJourneyResult(journeyRequest.JourneyRequestHash);

                Assert.IsNotNull(result);

            } // StateServer will be disposed, any out

            
        }

        /// <summary>
        ///A test for InvokeCyclePlanner when null journey request being passed
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.journeyplanrunner.dll")]
        public void InvokeCyclePlannerTest_NullJourneyRequest()
        {
            CycleJourneyPlanRunnerCaller_Accessor target = new CycleJourneyPlanRunnerCaller_Accessor(); 
            ITDPJourneyRequest journeyRequest = null;
            string sessionID = MockSessionFactory.mockSessionId;
            string lang = "en"; 
            target.InvokeCyclePlanner(journeyRequest, sessionID, lang);

            using (TDPStateServer stateServer = new TDPStateServer())
            {
                
                // Get the TDPResultManager
                object objResultManager = stateServer.Read(sessionID, SessionManagerKey.KeyResultManager.ID);

                // result manager should have never been created for this test
                Assert.IsNull(objResultManager);

                
            } // StateServer will be disposed, any out

        }

        #region Private methods

        /// <summary>
        /// Initialises a cycle journey request with standard request values
        /// for an outward and return journey
        /// </summary>
        /// <returns></returns>
        private ITDPJourneyRequest InitialiseCycleJourneyRequest()
        {
            IPropertyProvider pp = Properties.Current;

            ITDPJourneyRequest request = new TDPJourneyRequest();

            request.JourneyRequestHash = "Test";

            request.Origin = locationService.GetTDPLocation("E15 2TF", TDPLocationType.Postcode);
            request.Destination = locationService.GetTDPLocation("8100OPK", TDPLocationType.Venue);

            // Populate a cycle park
            List<TDPVenueCyclePark> cycleParks = locationService.GetTDPVenueCycleParks(request.Destination.Naptan);

            if ((cycleParks != null) && (cycleParks.Count > 0))
            {
                TDPVenueLocation venueLocation = request.Destination as TDPVenueLocation;

                venueLocation.SelectedTDPParkID = cycleParks[0].ID;

                request.Destination = venueLocation;
            }


            // Just to ensure the objects are used (for code coverage)
            request.ReturnOrigin = request.Destination;
            request.ReturnDestination = request.Origin;

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

            // Cycle only
            request.PlannerMode = TDPJourneyPlannerMode.Cycle;
            request.Modes = new System.Collections.Generic.List<TDPModeType>(
                new TDPModeType[1] { TDPModeType.Cycle });


            // Cycle specific
            request.Sequence = 1; // Cycle journey only 1 required

            #region Penalty function

            request.PenaltyFunction = GetCycleAlgorithm(string.Empty);

            #endregion

            #region User preferences

            // The ID of each user preference must match the IDs specified in the cycle planner configuration file.
            List<TDPUserPreference> userPreferences = new List<TDPUserPreference>();

            // Initialise a userpreference (for code coverage)
            TDPUserPreference tdpUserPreference = new TDPUserPreference();
            tdpUserPreference.PreferenceKey = "-1";
            tdpUserPreference.PreferenceValue = "-1";

            // A property that denotes the size of the array of user preferences expected by the Atkins CTP
            int numOfProperties = Convert.ToInt32(pp[JC.Keys.JourneyRequest_UserPreferences_Count]);

            // Build the actual array of user preferences from properties
            // these are used in the request sent to the Atkins CTP.
            for (int i = 0; i < numOfProperties; i++)
            {
                // Override any preferences by User entered/chosen values
                switch (i)
                {
                    //case 5: // Max Speed
                    //    break;
                    //case 6:  // Avoid Time Based Restrictions
                    //    break;
                    //case 12: // Avoid Steep Climbs
                    //    break;
                    //case 13: // Avoid Unlit Roads
                    //    break;
                    //case 14: // Avoid Walking your bike
                    //    break;
                    default:
                        tdpUserPreference = new TDPUserPreference(i.ToString(),
                            pp[string.Format(JC.Keys.JourneyRequest_UserPreferences_Index, i.ToString())]);
                        break;
                }
                userPreferences.Add(tdpUserPreference);
            }

            request.UserPreferences = userPreferences;

            #endregion

            request.JourneyRequestHash = request.GetTDPHashCode().ToString();

            return request;
        }

        /// <summary>
        /// Uses the provided cycle penalty function algorithm to build up a penalty function algorithm
        /// to use for cycle planning. Values are read from properties for the specified algorithm
        /// </summary>
        /// <param name="algorithm">Algorithm name corresponding to properties value, empty will use default algorithm</param>
        /// <returns></returns>
        private string GetCycleAlgorithm(string algorithm)
        {
            IPropertyProvider pp = Properties.Current;

            // penalty function must be formatted as 
            // "Call <location of penalty function assembly file>,<penalty function type name>"
            // e.g. "Call C:\CyclePlannerService\Services\RoadInterfaceHostingService\atk.cp.PenaltyFunctions.dll,
            // AtkinsGlobal.JourneyPlanning.PenaltyFunctions.Fastest"

            string algorithmToUse = algorithm;

            // Construct penalty function using the properties
            if (string.IsNullOrEmpty(algorithmToUse))
            {
                algorithmToUse = pp[JC.Keys.JourneyRequest_PenaltyFunction_Algorithm];
            }

            string dllPath = pp[string.Format(JC.Keys.JourneyRequest_PenaltyFunction_DLLPath, algorithmToUse)];
            string dll = pp[string.Format(JC.Keys.JourneyRequest_PenaltyFunction_DLL, algorithmToUse)];
            string prefix = pp[string.Format(JC.Keys.JourneyRequest_PenaltyFunction_Prefix, algorithmToUse)];
            string suffix = pp[string.Format(JC.Keys.JourneyRequest_PenaltyFunction_Suffix, algorithmToUse)];

            #region Validate

            // Validate penalty function values
            if (string.IsNullOrEmpty(dllPath) ||
                string.IsNullOrEmpty(dll) ||
                string.IsNullOrEmpty(prefix) ||
                string.IsNullOrEmpty(suffix))
            {
                throw new TDPException(
                    string.Format("Cycle planner penalty function property values for algorithm[{0}] were missing or invalid, check properties[{1}, {2}, {3}, and {4}] are available.",
                        algorithmToUse,
                        string.Format(JC.Keys.JourneyRequest_PenaltyFunction_DLLPath, algorithmToUse),
                        string.Format(JC.Keys.JourneyRequest_PenaltyFunction_DLL, algorithmToUse),
                        string.Format(JC.Keys.JourneyRequest_PenaltyFunction_Prefix, algorithmToUse),
                        string.Format(JC.Keys.JourneyRequest_PenaltyFunction_Suffix, algorithmToUse)),
                    false,
                    TDPExceptionIdentifier.PSMissingProperty);
            }

            if (!dllPath.EndsWith("\\"))
            {
                dllPath = dllPath + "\\";
            }

            if (!prefix.EndsWith("."))
            {
                prefix = prefix + ".";
            }

            #endregion

            string penaltyFunction = string.Format("Call {0}{1}, {2}{3}", dllPath, dll, prefix, suffix);

            return penaltyFunction;
        }

        /// <summary>
        /// Returns true if list of journeys contains a journey with TDPModeType.Cycle
        /// </summary>
        /// <param name="journeys"></param>
        /// <returns></returns>
        private bool HasCycleJourney(List<Journey> journeys)
        {
            List<TDPModeType> journeyModes;

            foreach (Journey journey in journeys)
            {
                if (journey.IsCycleJourney())
                {
                    return true;
                }

                journeyModes = new List<TDPModeType>(journey.GetUsedModes());

                if (journeyModes.Contains(TDPModeType.Cycle))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

       
    }
}