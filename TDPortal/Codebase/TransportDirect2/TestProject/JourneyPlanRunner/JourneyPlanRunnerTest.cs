
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.UserPortal.JourneyControl;


using TDP.Common.PropertyManager;
using TDP.Common.LocationService;
using TDP.Common.ServiceDiscovery;
using TDP.Common;
using TDP.Common.Extenders;
using JC = TDP.UserPortal.JourneyControl;
using JPR = TDP.UserPortal.JourneyPlanRunner;
using Logger = System.Diagnostics.Trace;
using System.Collections.Generic;
using TDP.Common.EventLogging;
using TDP.UserPortal.SessionManager;

namespace TDP.TestProject.JourneyPlanRunner
{
    
    
    /// <summary>
    ///This is a test class for JourneyPlanRunnerTest and is intended
    ///to contain all JourneyPlanRunnerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class JourneyPlanRunnerTest
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
        ///A test for ValidateAndRun when null request is supplied
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(TDPException))]
        public void ValidateAndRunTestNullRequest()
        {
            JPR.JourneyPlanRunner target = new JPR.JourneyPlanRunner();
            ITDPJourneyRequest journeyRequest = null;

            bool submitRequest = false;
            bool actual;
            actual = target.ValidateAndRun(journeyRequest, LanguageHelper.GetLanguageString(Language.English), submitRequest);
           
        }

        /// <summary>
        ///A test for ValidateAndRun when the request only needs validating and not submitting
        ///</summary>
        [TestMethod()]
        public void ValidateAndRunTestValidRequestSubmitFalse()
        {
            JPR.JourneyPlanRunner target = new JPR.JourneyPlanRunner();
            ITDPJourneyRequest journeyRequest = InitialiseValidJourneyRequest();

            bool submitRequest = false;
            bool expected = true;
            bool actual;
            actual = target.ValidateAndRun(journeyRequest, LanguageHelper.GetLanguageString(Language.English), submitRequest);
            Assert.AreEqual(expected, actual);
           
        }

        /// <summary>
        ///A test for ValidateAndRun when the request needs validating and not submitting
        ///</summary>
        [TestMethod()]
        public void ValidateAndRunTestValidRequestSubmitTrue()
        {
            JPR.JourneyPlanRunner target = new JPR.JourneyPlanRunner();
            ITDPJourneyRequest journeyRequest = InitialiseValidJourneyRequest();

            bool submitRequest = true;
            bool expected = true;
            bool actual;
            actual = target.ValidateAndRun(journeyRequest, LanguageHelper.GetLanguageString(Language.English), submitRequest);
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for ValidateAndRun when the request needs validating and not submitting
        ///</summary>
        [TestMethod()]
        public void ValidateAndRunTestInValidRequest()
        {
            JPR.JourneyPlanRunner target = new JPR.JourneyPlanRunner();
            ITDPJourneyRequest journeyRequest = InitialiseInValidJourneyRequest();

            bool submitRequest = true;
            bool expected = false;
            bool actual;
            actual = target.ValidateAndRun(journeyRequest, LanguageHelper.GetLanguageString(Language.English), submitRequest);
            Assert.AreEqual(expected, actual);

        }

        #region Private Helper Methods
        /// <summary>
        /// Initialises a journey request with standard request values
        /// for an outward and return journey
        /// </summary>
        /// <returns></returns>
        private ITDPJourneyRequest InitialiseValidJourneyRequest()
        {
            IPropertyProvider pp = Properties.Current;

            ITDPJourneyRequest request = new TDPJourneyRequest();

            request.JourneyRequestHash = "Test";

            request.Origin = locationService.GetTDPLocation("9100EDINBUR", TDPLocationType.Station);
            request.Destination = locationService.GetTDPLocation("8100OPK", TDPLocationType.Venue);

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

            // Public
            request.PlannerMode = TDPJourneyPlannerMode.PublicTransport;
            request.Modes = new System.Collections.Generic.List<TDPModeType>(
                new TDPModeType[8] 
                { TDPModeType.Rail, TDPModeType.Bus, TDPModeType.Coach, TDPModeType.Metro, TDPModeType.Underground, 
                  TDPModeType.Tram, TDPModeType.Ferry, TDPModeType.Air});

            request.OutwardJourneyPart = new Journey();
            request.ReturnJourneyPart = new Journey();

            // Public specific
            request.PublicAlgorithm = GetPublicAlgorithm(pp[JC.Keys.JourneyRequest_AlgorithmPublic]);

            request.Sequence = pp[JC.Keys.JourneyRequest_Sequence].Parse(3);
            request.InterchangeSpeed = pp[JC.Keys.JourneyRequest_InterchangeSpeed].Parse(0);
            request.WalkingSpeed = pp[JC.Keys.JourneyRequest_WalkingSpeed].Parse(80);
            request.MaxWalkingTime = pp[JC.Keys.JourneyRequest_MaxWalkingTime].Parse(30);
            request.RoutingGuideInfluenced = pp[JC.Keys.JourneyRequest_RoutingGuideInfluenced].Parse(false);
            request.RoutingGuideCompliantJourneysOnly = pp[JC.Keys.JourneyRequest_RoutingGuideCompliantJourneysOnly].Parse(false);
            request.RouteCodes = pp[JC.Keys.JourneyRequest_RouteCodes];
            request.OlympicRequest = pp[JC.Keys.JourneyRequest_OlympicRequest].Parse(true);

            request.TravelDemandPlanOutward = pp[JC.Keys.JourneyRequest_TravelDemandPlanOutward];
            request.TravelDemandPlanReturn = pp[JC.Keys.JourneyRequest_TravelDemandPlanReturn];
            request.RemoveAwkwardOvernight = pp[JC.Keys.JourneyRequest_RemoveAwkwardOvernight].Parse(false);

            // Car specific
            request.PrivateAlgorithm = GetPrivateAlgorithm(pp[JC.Keys.JourneyRequest_AlgorithmPrivate]);

            request.AvoidMotorways = pp[JC.Keys.JourneyRequest_AvoidMotorways].Parse(false);
            request.AvoidFerries = pp[JC.Keys.JourneyRequest_AvoidFerries].Parse(false);
            request.AvoidTolls = pp[JC.Keys.JourneyRequest_AvoidTolls].Parse(false);
            request.AvoidRoads = new List<string>();
            request.IncludeRoads = new List<string>();
            request.DrivingSpeed = pp[JC.Keys.JourneyRequest_DrivingSpeed].Parse(112);
            request.DoNotUseMotorways = pp[JC.Keys.JourneyRequest_DoNotUseMotorways].Parse(false);
            request.FuelConsumption = pp[JC.Keys.JourneyRequest_FuelConsumption];
            request.FuelPrice = pp[JC.Keys.JourneyRequest_FuelPrice];

            // Hash
            request.JourneyRequestHash = request.GetTDPHashCode().ToString();

            return request;
        }

        /// <summary>
        /// Initialises a journey request with standard request values
        /// for an outward and return journey
        /// </summary>
        /// <returns></returns>
        private ITDPJourneyRequest InitialiseInValidJourneyRequest()
        {
            IPropertyProvider pp = Properties.Current;

            ITDPJourneyRequest request = new TDPJourneyRequest();

            request.JourneyRequestHash = "Test";

            request.Origin = locationService.GetTDPLocation("9100EDINBUR", TDPLocationType.Station);
            request.Destination = locationService.GetTDPLocation("9100EDINBUR", TDPLocationType.Station);

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

            // Public
            request.PlannerMode = TDPJourneyPlannerMode.PublicTransport;
            request.Modes = new System.Collections.Generic.List<TDPModeType>(
                new TDPModeType[8] 
                { TDPModeType.Rail, TDPModeType.Bus, TDPModeType.Coach, TDPModeType.Metro, TDPModeType.Underground, 
                  TDPModeType.Tram, TDPModeType.Ferry, TDPModeType.Air});

            request.OutwardJourneyPart = new Journey();
            request.ReturnJourneyPart = new Journey();

            // Public specific
            request.PublicAlgorithm = GetPublicAlgorithm(pp[JC.Keys.JourneyRequest_AlgorithmPublic]);

            request.Sequence = pp[JC.Keys.JourneyRequest_Sequence].Parse(3);
            request.InterchangeSpeed = pp[JC.Keys.JourneyRequest_InterchangeSpeed].Parse(0);
            request.WalkingSpeed = pp[JC.Keys.JourneyRequest_WalkingSpeed].Parse(80);
            request.MaxWalkingTime = pp[JC.Keys.JourneyRequest_MaxWalkingTime].Parse(30);
            request.RoutingGuideInfluenced = pp[JC.Keys.JourneyRequest_RoutingGuideInfluenced].Parse(false);
            request.RoutingGuideCompliantJourneysOnly = pp[JC.Keys.JourneyRequest_RoutingGuideCompliantJourneysOnly].Parse(false);
            request.RouteCodes = pp[JC.Keys.JourneyRequest_RouteCodes];
            request.OlympicRequest = pp[JC.Keys.JourneyRequest_OlympicRequest].Parse(true);

            request.TravelDemandPlanOutward = pp[JC.Keys.JourneyRequest_TravelDemandPlanOutward];
            request.TravelDemandPlanReturn = pp[JC.Keys.JourneyRequest_TravelDemandPlanReturn];
            request.RemoveAwkwardOvernight = pp[JC.Keys.JourneyRequest_RemoveAwkwardOvernight].Parse(false);

            // Car specific
            request.PrivateAlgorithm = GetPrivateAlgorithm(pp[JC.Keys.JourneyRequest_AlgorithmPrivate]);

            request.AvoidMotorways = pp[JC.Keys.JourneyRequest_AvoidMotorways].Parse(false);
            request.AvoidFerries = pp[JC.Keys.JourneyRequest_AvoidFerries].Parse(false);
            request.AvoidTolls = pp[JC.Keys.JourneyRequest_AvoidTolls].Parse(false);
            request.AvoidRoads = new List<string>();
            request.IncludeRoads = new List<string>();
            request.DrivingSpeed = pp[JC.Keys.JourneyRequest_DrivingSpeed].Parse(112);
            request.DoNotUseMotorways = pp[JC.Keys.JourneyRequest_DoNotUseMotorways].Parse(false);
            request.FuelConsumption = pp[JC.Keys.JourneyRequest_FuelConsumption];
            request.FuelPrice = pp[JC.Keys.JourneyRequest_FuelPrice];

            // Hash
            request.JourneyRequestHash = request.GetTDPHashCode().ToString();

            return request;
        }

        /// <summary>
        /// Converts a string into a TDPPublicAlgorithmType. If unable to parse, Default is returned 
        /// and a warning logged
        /// </summary>
        private TDPPublicAlgorithmType GetPublicAlgorithm(string algorithm)
        {
            TDPPublicAlgorithmType algorithmType = TDPPublicAlgorithmType.Default;
            try
            {
                algorithmType = (TDPPublicAlgorithmType)Enum.Parse(typeof(TDPPublicAlgorithmType), algorithm, true);
            }
            catch
            {
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Warning,
                    string.Format("Failed to parse algorithm string[{0}] into an TDPPublicAlgorithmType, check property[{1}] contains a valid value for this algorithm type.",
                                   algorithm,
                                   JC.Keys.JourneyRequest_AlgorithmPublic)));
            }

            return algorithmType;
        }

        /// <summary>
        /// Converts a string into a TDPPrivateAlgorithmType. If unable to parse, Fastest is returned 
        /// and a warning logged
        /// </summary>
        private TDPPrivateAlgorithmType GetPrivateAlgorithm(string algorithm)
        {
            TDPPrivateAlgorithmType algorithmType = TDPPrivateAlgorithmType.Fastest;
            try
            {
                algorithmType = (TDPPrivateAlgorithmType)Enum.Parse(typeof(TDPPrivateAlgorithmType), algorithm, true);
            }
            catch
            {
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Warning,
                    string.Format("Failed to parse algorithm string[{0}] into an TDPPrivateAlgorithmType, check property[{1}] contains a valid value for this algorithm type.",
                                   algorithm,
                                   JC.Keys.JourneyRequest_AlgorithmPrivate)));
            }

            return algorithmType;
        }

        /// <summary>
        /// Returns true if list of journeys contains a journey with TDPModeType.Car
        /// </summary>
        /// <param name="journeys"></param>
        /// <returns></returns>
        private bool HasCarJourney(List<Journey> journeys)
        {
            List<TDPModeType> journeyModes;

            foreach (Journey journey in journeys)
            {
                if (journey.IsCarJourney())
                {
                    return true;
                }

                journeyModes = new List<TDPModeType>(journey.GetUsedModes());

                if (journeyModes.Contains(TDPModeType.Car))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
