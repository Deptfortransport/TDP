using TDP.UserPortal.CyclePlannerService;
using TDP.UserPortal.JourneyControl;
using TDP.Common.PropertyManager;
using TDP.Common.ServiceDiscovery;
using TDP.Common.LocationService;
using TDP.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TDP.UserPortal.CyclePlannerService.CyclePlannerWebService;
using JC = TDP.UserPortal.JourneyControl;

namespace TDP.TestProject.CyclePlannerService
{
    
    
    /// <summary>
    ///This is a test class for CyclePlannerTest and is intended
    ///to contain all CyclePlannerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CyclePlannerTest
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            TDPServiceDiscovery.ResetServiceDiscoveryForTest();

            TDPServiceDiscovery.Init(new TestInitialisation());
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
        ///A test for CycleJourneyPlan
        ///</summary>
        [TestMethod()]
        public void CycleJourneyPlanTest()
        {
            CyclePlannerManager_Accessor target = new CyclePlannerManager_Accessor();

            target.sessionId = "CyclePlannerTest_CallCyclePlannerTest";
            target.referenceTransaction = false;
            target.userType = 0;
            target.language = "en";

            // Initialise a cycle journey request
            ITDPJourneyRequest request = InitialiseCycleJourneyRequest();

            // Perform test
            ITDPJourneyResult actual = target.CallCyclePlanner(request);

            // Check for journeys
            bool hasJourneys = (actual.OutwardJourneys.Count > 0);
            bool hasCorrectNumOfJourneysOutward = (actual.OutwardJourneys.Count == request.Sequence);
            bool hasCorrectNumOfJourneysReturn = (request.IsReturnRequired) ? (actual.ReturnJourneys.Count == request.Sequence) : true;
            bool hasCycleJourneyOutward = (request.Modes.Contains(TDPModeType.Cycle)) ? (HasCycleJourney(actual.OutwardJourneys)) : true;
            bool hasCycleJourneyReturn = ((request.Modes.Contains(TDPModeType.Cycle)) && (request.IsReturnRequired)) ? (HasCycleJourney(actual.ReturnJourneys)) : true;

            // Check test resuls
            Assert.IsNotNull(actual);
            Assert.IsTrue(hasJourneys, "Excepted at least 1 journey in the result");
            Assert.IsTrue(hasCorrectNumOfJourneysOutward,
                            string.Format("Excepted [{0}] outward journeys in the result, result contained [{1}]", request.Sequence, actual.OutwardJourneys.Count));
            Assert.IsTrue(hasCorrectNumOfJourneysReturn,
                            string.Format("Excepted [{0}] return journeys in the result, result contained [{1}]", request.Sequence, actual.ReturnJourneys.Count));
            Assert.IsTrue(hasCycleJourneyOutward, "Expect [1] outward cycle journey in the result, result contained [0]");
            Assert.IsTrue(hasCycleJourneyReturn, "Expect [1] return cycle journey in the result, result contained [0]");
        }

        /// <summary>
        ///A test for CyclePlanner Constructor
        ///</summary>
        [TestMethod()]
        public void CyclePlannerConstructorTest()
        {
            CyclePlanner target = new CyclePlanner();
            Assert.IsNotNull(target, "No cycle planner object returned by constructor.");
        }

        /// <summary>
        ///A test for LogCallEvent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.cycleplannerservice.dll")]
        public void LogCallEventTest()
        {
            CyclePlanner_Accessor target = new CyclePlanner_Accessor(); 
            string webServiceMethod = "WebServiceMethodName";
            DateTime callStartTime = DateTime.Now;
            bool successful = true; 
            target.LogCallEvent(webServiceMethod, callStartTime, successful);

            // Method will write call to event log, can only test that this did not error
        }

        /// <summary>
        ///A test for LogException
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.cycleplannerservice.dll")]
        public void LogExceptionTest()
        {
            string message = "Exception message text";
            CyclePlanner_Accessor target = new CyclePlanner_Accessor();
            Exception ex = new Exception(message);

            try
            {
                target.LogException(ex);
                Assert.Fail("Exception not raised");
            }
            catch (TDPException tdpEx)
            {
                // Expected exception, test message is the same #
                Assert.AreEqual(string.Format("CyclePlanner - An exception has occured when trying to call the CyclePlannerWebService. Error: {0}", message), tdpEx.Message);
            }
            catch
            {
                Assert.Fail("Unexpected exception raised");
            }
        }

        #region PrivateMethods
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
        /// Returns true if list of journeys contains a journey with TDPModeType.Cycle
        /// </summary>
        /// <param name="journeys"></param>
        /// <returns></returns>
        private bool HasCycleJourney(List<TDP.UserPortal.JourneyControl.Journey> journeys)
        {
            List<TDPModeType> journeyModes;

            foreach (TDP.UserPortal.JourneyControl.Journey journey in journeys)
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

        #endregion
    }
}
