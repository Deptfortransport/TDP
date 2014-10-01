using TDP.UserPortal.JourneyPlanRunner;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.UserPortal.JourneyControl;
using System.Collections.Generic;
using TDP.Common;
using JC = TDP.UserPortal.JourneyControl;
using JPR = TDP.UserPortal.JourneyPlanRunner;
using Logger = System.Diagnostics.Trace;
using TDP.Common.ServiceDiscovery;
using TDP.Common.PropertyManager;
using TDP.Common.LocationService;
using TDP.Common.EventLogging;
using TDP.Common.Extenders;

namespace TDP.TestProject.JourneyPlanRunner
{
    
    
    /// <summary>
    ///This is a test class for JourneyPlanRunnerBaseTest and is intended
    ///to contain all JourneyPlanRunnerBaseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class JourneyPlanRunnerBaseTest
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
        ///A test for PerformDateValidations
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.journeyplanrunner.dll")]
        public void PerformDateValidationsTest()
        {
            PrivateObject param0 = new PrivateObject(new JPR.JourneyPlanRunner());
            JourneyPlanRunnerBase_Accessor target = new JourneyPlanRunnerBase_Accessor(param0);
            ITDPJourneyRequest journeyRequest = InitialiseValidJourneyRequest();
            target.PerformDateValidations(journeyRequest);

            Assert.AreEqual(0, target.Messages.Count);
            DateTime origOutwardDate = journeyRequest.OutwardDateTime;

            //------------------------------ OUTWARD DATE -------------------------------

            // Invalid outward journey date - date time is not set
            journeyRequest.OutwardDateTime = DateTime.MinValue;
            target.PerformDateValidations(journeyRequest);

            Assert.IsTrue(target.Messages.Count > 0);

            Assert.IsTrue(target.listErrors.ContainsKey("ValidateAndRun.DateNotValid"));
            
            // Invalid outward journey date - date time is less that current date
            journeyRequest.OutwardDateTime = DateTime.Now.AddDays(-1);
            target.PerformDateValidations(journeyRequest);

            Assert.IsTrue(target.Messages.Count>0);

            Assert.IsTrue(target.listErrors.ContainsKey("ValidateAndRun.DateTimeIsInThePast"));

            // Invalid outward journey date - date time is less that event start date
            journeyRequest.OutwardDateTime = new DateTime(2012, 07, 17);
            target.PerformDateValidations(journeyRequest);

            Assert.IsTrue(target.Messages.Count > 0);

            // In case the test is run after the games start date, then do not fail
            if (journeyRequest.OutwardDateTime > DateTime.Now)
                Assert.IsTrue(target.listErrors.ContainsKey("ValidateAndRun.DateTimeIsBeforeEvent"));

            // Invalid outward journey date - date time is greater that event start date
            journeyRequest.OutwardDateTime = new DateTime(2012, 12, 1);
            target.PerformDateValidations(journeyRequest);

            Assert.IsTrue(target.Messages.Count > 0);

            Assert.IsTrue(target.listErrors.ContainsKey("ValidateAndRun.DateTimeIsAfterEvent"));

            // As this date is greater than the return date set in the journey request
            // There will be an additional message about outward date is greater than return date
            Assert.IsTrue(target.listErrors.ContainsKey("ValidateAndRun.OutwardDateIsAfterReturnDate"));

            // reset the outward date
            journeyRequest.OutwardDateTime = origOutwardDate;

            //------------------------------ RETURN DATE -------------------------------

            // Invalid return journey date - date time is not set
            journeyRequest.ReturnDateTime = DateTime.MinValue;
            target.PerformDateValidations(journeyRequest);

            Assert.IsTrue(target.Messages.Count > 0);

            Assert.IsTrue(target.listErrors.ContainsKey("ValidateAndRun.DateNotValid"));

            // Invalid return journey date - date time is less that current date
            journeyRequest.ReturnDateTime = DateTime.Now.AddDays(-1);
            target.PerformDateValidations(journeyRequest);

            Assert.IsTrue(target.Messages.Count > 0);

            Assert.IsTrue(target.listErrors.ContainsKey("ValidateAndRun.DateTimeIsInThePast"));

            // Invalid return journey date - date time is less that event start date
            journeyRequest.ReturnDateTime = new DateTime(2012, 07, 17);
            target.PerformDateValidations(journeyRequest);

            Assert.IsTrue(target.Messages.Count > 0);

            // In case the test is run after the games start date, then do not fail
            if (journeyRequest.ReturnDateTime > DateTime.Now)
                Assert.IsTrue(target.listErrors.ContainsKey("ValidateAndRun.DateTimeIsBeforeEvent"));

            // Invalid return journey date - date time is greater that event start date
            journeyRequest.ReturnDateTime = new DateTime(2012, 12, 1);
            target.PerformDateValidations(journeyRequest);

            Assert.IsTrue(target.Messages.Count > 0);

            Assert.IsTrue(target.listErrors.ContainsKey("ValidateAndRun.DateTimeIsAfterEvent"));

           
        }

        /// <summary>
        ///A test for PerformLocationValidations
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.journeyplanrunner.dll")]
        public void PerformLocationValidationsTest()
        {
            PrivateObject param0 = new PrivateObject(new JPR.JourneyPlanRunner());
            JourneyPlanRunnerBase_Accessor target = new JourneyPlanRunnerBase_Accessor(param0);
            ITDPJourneyRequest journeyRequest = InitialiseValidJourneyRequest();
            target.PerformLocationValidations(journeyRequest);

            Assert.AreEqual(0, target.Messages.Count);
                        
            TDPLocation orig_Origin = journeyRequest.Origin;
            TDPLocation orig_Destination = journeyRequest.Destination;

            // No venue supplied - both locations are not venue
            journeyRequest.Destination = locationService.GetTDPLocation("NG9 1LA", TDPLocationType.Postcode);
            target.PerformLocationValidations(journeyRequest);

            Assert.IsTrue(target.Messages.Count > 0);

            Assert.IsTrue(target.listErrors.ContainsKey("ValidateAndRun.AtleastOneLocationShouleBeVenue"));

            // Both locations are venue and are the same venues
            journeyRequest.Destination = orig_Destination;
            journeyRequest.Origin = locationService.GetTDPLocation("8100OPK", TDPLocationType.Venue);
            target.PerformLocationValidations(journeyRequest);

            Assert.IsTrue(target.Messages.Count > 0);

            Assert.IsTrue(target.listErrors.ContainsKey("ValidateAndRun.OriginAndDestinationAreSame"));

            // Both locations are venue and are Parent - Child
            journeyRequest.Destination = locationService.GetTDPLocation("8100AQC", TDPLocationType.Venue);
            journeyRequest.Origin = locationService.GetTDPLocation("8100OPK", TDPLocationType.Venue);
            target.PerformLocationValidations(journeyRequest);

            Assert.IsTrue(target.Messages.Count > 0);

            Assert.IsTrue(target.listErrors.ContainsKey("ValidateAndRun.OriginAndDestinationOverlaps"));
            
            
            
        }

        /// <summary>
        ///A test for SetValidationError
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.journeyplanrunner.dll")]
        public void SetValidationErrorTest1()
        {
            PrivateObject param0 = new PrivateObject(new JPR.JourneyPlanRunner());
            JourneyPlanRunnerBase_Accessor target = new JourneyPlanRunnerBase_Accessor(param0);
            string msgResourceID = "Test";
            List<string> msgArgs = new List<string>(new string[]{"ta1","ta2"});
            target.SetValidationError(msgResourceID, msgArgs);
            Assert.IsTrue(target.listErrors.ContainsKey(msgResourceID));
            Assert.IsNotNull(target.listErrors[msgResourceID]);
            Assert.IsTrue(target.listErrors[msgResourceID].MessageArgs.Count == 2);
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
