// *********************************************** 
// NAME             : CJPManagerTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 02 Apr 2011
// DESCRIPTION  	: CJPManagerTest class
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDP.Common;
using TDP.Common.EventLogging;
using TDP.Common.Extenders;
using TDP.Common.LocationService;
using TDP.Common.PropertyManager;
using TDP.Common.ServiceDiscovery;
using TDP.UserPortal.JourneyControl;
using JC = TDP.UserPortal.JourneyControl;
using Logger = System.Diagnostics.Trace;
using TDP.Common.DataServices;
using TDP.Common.LocationService.GIS;

namespace TDP.TestProject.JourneyControl
{   
    /// <summary>
    ///This is a test class for CJPManagerTest and is intended
    ///to contain all CJPManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CJPManagerTest
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
            TDPServiceDiscovery.Init(new TestInitialisationPropertiesLogging());

            TDPServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.Cache, new TDPCache());
            TDPServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.GisQuery, new GisQueryFactory());
            TDPServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.LocationService, new LocationServiceFactory());
            TDPServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CJP, new CJPFactory());

            try
            {
                //Configure the hosted remoting objects to be a remote object
                string configPath = AppDomain.CurrentDomain.BaseDirectory + @"\Remoting.config";
                if (File.Exists(configPath))
                {
                    RemotingConfiguration.Configure(configPath, false);
                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "Loaded remoting configuration from " + configPath));
                }
                else
                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, "Could not find remoting configuration file: " + configPath));
            }
            catch
            {
                // Ignore as it may already have been done
            }
        }
        
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

        #region Tests

        /// <summary>
        ///A test for CJPManager Constructor
        ///</summary>
        [TestMethod()]
        public void CJPManagerConstructorTest()
        {
            CJPManager target = new CJPManager();

            Assert.IsNotNull(target, "CJPManager is null, failed to create CJPManager object");

            CjpManagerFactory factory = new CjpManagerFactory();

            CJPManager manager = (CJPManager)factory.Get();

            Assert.IsNotNull(manager, "CJPManager is null, failed to get CJPManager using Factory object");

        }

        /// <summary>
        ///A test for CallCJP
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.journeycontrol.dll")]
        public void CallCJPPublicTransportTest()
        {
            CJPManager_Accessor target = new CJPManager_Accessor();
            target.sessionId = "CJPManagerTest_CallCJPTest";
            target.referenceTransaction = false;
            target.userType = 0;
            target.language = "en";
            
            // Initialise a journey request
            ITDPJourneyRequest request = InitialiseJourneyRequest();
                        
            // Perform test
            ITDPJourneyResult actual = target.CallCJP(request);

            // Check for journeys
            bool hasJourneys = (actual.OutwardJourneys.Count > 0);
            bool hasValidJourneys = ((actual.OutwardJourneys[0].Duration > TimeSpan.Zero) && (actual.OutwardJourneys[0].InterchangeCount >= 0));
            bool hasCorrectNumOfJourneysOutward = (actual.OutwardJourneys.Count == request.Sequence);
            bool hasCorrectNumOfJourneysReturn = (request.IsReturnRequired) ? (actual.ReturnJourneys.Count == request.Sequence) : true;
            bool hasCarJourneyOutward = (request.Modes.Contains(TDPModeType.Car)) ? (HasCarJourney(actual.OutwardJourneys)) : true;
            bool hasCarJourneyReturn = ((request.Modes.Contains(TDPModeType.Car)) && (request.IsReturnRequired)) ? (HasCarJourney(actual.ReturnJourneys)) : true;
            
            // Check test resuls
            Assert.IsNotNull(actual);
            Assert.IsTrue(hasJourneys, "Excepted at least 1 journey in the result");
            Assert.IsTrue(hasValidJourneys, "Excepted journey to have valid duration and interchange count");
            Assert.IsTrue(hasCorrectNumOfJourneysOutward, 
                            string.Format("Excepted [{0}] outward journeys in the result, result contained [{1}]", request.Sequence, actual.OutwardJourneys.Count));
            Assert.IsTrue(hasCorrectNumOfJourneysReturn,
                            string.Format("Excepted [{0}] return journeys in the result, result contained [{1}]", request.Sequence, actual.ReturnJourneys.Count));
            Assert.IsTrue(hasCarJourneyOutward, "Expect [1] outward car journey in the result, result contained [0]");
            Assert.IsTrue(hasCarJourneyReturn, "Expect [1] return car journey in the result, result contained [0]");
        }

        /// <summary>
        ///A test for CallCJP
        ///</summary>
        [TestMethod()]
        public void CallCJPPublicTransportTest1()
        {
            CJPManager target = new CJPManager();

            // Initialise a journey request
            ITDPJourneyRequest request = InitialiseJourneyRequest(); 

            // Only test for outward journey
            request.IsReturnRequired = false;
                        
            string sessionId = "CJPManagerTest_CallCJPTest1";
            bool referenceTransaction = false;
            string language = "en";
                     
            // Perform test
            ITDPJourneyResult actual = target.CallCJP(request, sessionId, referenceTransaction, language);

            // Check for journeys
            bool hasJourneys = (actual.OutwardJourneys.Count > 0);
            bool hasCorrectNumOfJourneysOutward = (actual.OutwardJourneys.Count == request.Sequence);
            bool hasCorrectNumOfJourneysReturn = (request.IsReturnRequired) ? (actual.ReturnJourneys.Count == request.Sequence) : true;
            bool hasCarJourneyOutward = (request.Modes.Contains(TDPModeType.Car)) ? (HasCarJourney(actual.OutwardJourneys)) : true;
            bool hasCarJourneyReturn = ((request.Modes.Contains(TDPModeType.Car)) && (request.IsReturnRequired)) ? (HasCarJourney(actual.ReturnJourneys)) : true;

            // Check test resuls
            Assert.IsNotNull(actual);
            Assert.IsTrue(hasJourneys, "Excepted at least 1 journey in the result");
            Assert.IsTrue(hasCorrectNumOfJourneysOutward,
                            string.Format("Excepted [{0}] outward journeys in the result, result contained [{1}]", request.Sequence, actual.OutwardJourneys.Count));
            Assert.IsTrue(hasCorrectNumOfJourneysReturn,
                            string.Format("Excepted [{0}] return journeys in the result, result contained [{1}]", request.Sequence, actual.ReturnJourneys.Count));
            Assert.IsTrue(hasCarJourneyOutward, "Expect [1] outward car journey in the result, result contained [0]");
            Assert.IsTrue(hasCarJourneyReturn, "Expect [1] return car journey in the result, result contained [0]");
        }

        /// <summary>
        ///A test for CallCJP
        ///</summary>
        [TestMethod()]
        public void CallCJPCarTest1()
        {
            CJPManager target = new CJPManager();

            // Initialise a journey request
            ITDPJourneyRequest request = InitialiseJourneyRequest();

            request.PlannerMode = TDPJourneyPlannerMode.ParkAndRide;
            request.Modes = new List<TDPModeType>(1) { TDPModeType.Car };

            request.Sequence = 1;

            // Date - Car journeys currently can only be planned for 2011
            request.OutwardDateTime = DateTime.Now; //new DateTime(2012, 08, 30, 12, 0, 0);
            request.ReturnDateTime = request.OutwardDateTime.AddHours(3);

            // Populate a car park
            
            // Add naptans to search on, include the parent in case child venue has none
            List<string> naptans = request.Destination.Naptan;
            naptans.Add(request.Destination.Parent);

            LocationService locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);

            List<TDPVenueCarPark> carParks = locationService.GetTDPVenueCarParks(request.Destination.Naptan);

            if ((carParks != null) && (carParks.Count > 0))
            {
                TDPVenueLocation venueLocation = request.Destination as TDPVenueLocation;

                venueLocation.SelectedTDPParkID = carParks[0].ID;

                request.Destination = venueLocation;
            }


            string sessionId = "CJPManagerTest_CallCJPTest1";
            bool referenceTransaction = false;
            string language = "en";

            // Perform test
            ITDPJourneyResult actual = target.CallCJP(request, sessionId, referenceTransaction, language);

            // Check for journeys
            bool hasJourneys = (actual.OutwardJourneys.Count > 0);
            bool hasCorrectNumOfJourneysOutward = (actual.OutwardJourneys.Count == request.Sequence);
            bool hasCorrectNumOfJourneysReturn = (request.IsReturnRequired) ? (actual.ReturnJourneys.Count == request.Sequence) : true;
            bool hasCarJourneyOutward = (request.Modes.Contains(TDPModeType.Car)) ? (HasCarJourney(actual.OutwardJourneys)) : true;
            bool hasCarJourneyReturn = ((request.Modes.Contains(TDPModeType.Car)) && (request.IsReturnRequired)) ? (HasCarJourney(actual.ReturnJourneys)) : true;

            // Check test resuls
            Assert.IsNotNull(actual);
            Assert.IsTrue(hasJourneys, "Excepted at least 1 journey in the result");
            Assert.IsTrue(hasCorrectNumOfJourneysOutward,
                            string.Format("Excepted [{0}] outward journeys in the result, result contained [{1}]", request.Sequence, actual.OutwardJourneys.Count));
            Assert.IsTrue(hasCorrectNumOfJourneysReturn,
                            string.Format("Excepted [{0}] return journeys in the result, result contained [{1}]", request.Sequence, actual.ReturnJourneys.Count));
            Assert.IsTrue(hasCarJourneyOutward, "Expect [1] outward car journey in the result, result contained [0]");
            Assert.IsTrue(hasCarJourneyReturn, "Expect [1] return car journey in the result, result contained [0]");
        }

        /// <summary>
        ///A test for CallCJP
        ///</summary>
        [TestMethod()]
        public void CallCJPCarTest2()
        {
            CJPManager target = new CJPManager();

            // Initialise a journey request
            ITDPJourneyRequest request = InitialiseJourneyRequest();

            LocationService locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);

            request.PlannerMode = TDPJourneyPlannerMode.BlueBadge;
            request.Modes = new List<TDPModeType>(1) { TDPModeType.Car, TDPModeType.Bus };

            LocationSearch originSearch = new LocationSearch("N7 0EE", string.Empty, TDPLocationType.Postcode, true);
            LocationSearch destinationSearch = new LocationSearch("EC1R 3HN", string.Empty, TDPLocationType.Postcode, true);

            request.Origin = locationService.ResolveLocation(ref originSearch, false, "TestSession");
            //request.Destination = locationService.GetTDPLocation("8100HGP", TDPLocationType.Venue);
            request.Destination = locationService.ResolveLocation(ref destinationSearch, false, "TestSession");

            request.Sequence = 1;

            // Date 
            request.OutwardDateTime = DateTime.Now; // new DateTime(2012, 08, 01, 12, 0, 0);
            request.ReturnDateTime = request.OutwardDateTime.AddHours(3);

            // Populate a car park
            // Add naptans to search on, include the parent in case child venue has none
            List<string> naptans = request.Destination.Naptan;
            naptans.Add(request.Destination.Parent);

            List<TDPVenueCarPark> carParks = locationService.GetTDPVenueBlueBadgeCarParks(request.Destination.Naptan);

            if ((carParks != null) && (carParks.Count > 0))
            {
                TDPVenueLocation venueLocation = request.Destination as TDPVenueLocation;

                venueLocation.SelectedTDPParkID = carParks[0].ID;

                request.Destination = venueLocation;
            }


            string sessionId = "CJPManagerTest_CallCJPTest2";
            bool referenceTransaction = false;
            string language = "en";

            // Perform test
            ITDPJourneyResult actual = target.CallCJP(request, sessionId, referenceTransaction, language);

            // Check for journeys
            bool hasJourneys = (actual.OutwardJourneys.Count > 0);
            bool hasJourneysReturn = (actual.ReturnJourneys.Count > 0);
            bool hasCarJourneyOutward = (request.Modes.Contains(TDPModeType.Car)) ? (HasCarJourney(actual.OutwardJourneys)) : true;
            bool hasCarJourneyReturn = ((request.Modes.Contains(TDPModeType.Car)) && (request.IsReturnRequired)) ? (HasCarJourney(actual.ReturnJourneys)) : true;

            // Check test resuls
            Assert.IsNotNull(actual);
            Assert.IsTrue(hasJourneys, "Excepted at least 1 outward journey in the result");
            Assert.IsTrue(hasJourneysReturn, "Excepted at least one return journey in the result");
            Assert.IsTrue(hasCarJourneyOutward, "Expect [1] outward car journey in the result, result contained [0]");
            Assert.IsTrue(hasCarJourneyReturn, "Expect [1] return car journey in the result, result contained [0]");
        }                
        /// <summary>
        ///A test for LogRequest
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.journeycontrol.dll")]
        public void LogRequestTest()
        {
            CJPManager_Accessor target = new CJPManager_Accessor();

            // Initialise a request, LogRequest will only use the modes value
            ITDPJourneyRequest request = InitialiseJourneyRequest();

            string requestId = "CJPManagerTest_LogRequestTest";
            bool isLoggedOn = false; 
            string sessionId = "LogRequestTest";

            // Perform test
            target.LogRequest(request, requestId, isLoggedOn, sessionId);

            // Method will write the request event to a log, cannot verify test here
        }

        /// <summary>
        ///A test for LogResponse
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.journeycontrol.dll")]
        public void LogResponseTest()
        {
            CJPManager_Accessor target = new CJPManager_Accessor();

            // LogResponse will write a result event to a log, the sucsess value can be Failure, ZeroResults, or Results

            // Initialise a result
            ITDPJourneyResult result = new TDPJourneyResult();

            string requestId = "CJPManagerTest_LogResponseTest";
            bool isLoggedOn = false;
            bool cjpFailed = true;
            string sessionId = "LogResponseTest";

            // Perform Test 1 - Failure type
            target.LogResponse(result, requestId, isLoggedOn, cjpFailed, sessionId);

            // Perform Test 1 - ZeroResults type
            cjpFailed = false;
            target.LogResponse(result, requestId, isLoggedOn, cjpFailed, sessionId);

            // Perform Test 1 - Results type
            result.OutwardJourneys.Add(new Journey());
            target.LogResponse(result, requestId, isLoggedOn, cjpFailed, sessionId);

            // Method will write the result event to a log, cannot verify test here
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initialises a journey request with standard request values
        /// for an outward and return journey
        /// </summary>
        /// <returns></returns>
        private ITDPJourneyRequest InitialiseJourneyRequest()
        {
            IPropertyProvider pp = Properties.Current;

            ITDPJourneyRequest request = new TDPJourneyRequest();

            request.JourneyRequestHash = "Test";

            LocationService locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);

            LocationSearch originSearch = new LocationSearch(string.Empty, "9100EDINBUR", TDPLocationType.Station, true);
            LocationSearch destinationSearch = new LocationSearch(string.Empty, "900067157", TDPLocationType.Station, true);

            request.Origin = locationService.ResolveLocation(ref originSearch, false, "TestSession");
            //request.Destination = locationService.GetTDPLocation("8100HGP", TDPLocationType.Venue);
            request.Destination = locationService.ResolveLocation(ref destinationSearch, false, "TestSession");
                        
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
                new TDPModeType[9] 
                { TDPModeType.Rail, TDPModeType.Bus, TDPModeType.Coach, TDPModeType.Metro, TDPModeType.Underground, 
                  TDPModeType.Tram, TDPModeType.Ferry, TDPModeType.Air, TDPModeType.Telecabine});

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
            request.OlympicRequest = pp[JC.Keys.JourneyRequest_OlympicRequest].Parse(false);

            request.TravelDemandPlanOutward = pp[JC.Keys.JourneyRequest_TravelDemandPlanOutward];
            request.TravelDemandPlanReturn = pp[JC.Keys.JourneyRequest_TravelDemandPlanReturn];
            request.RoutingGuideInfluenced = pp[JC.Keys.JourneyRequest_RoutingGuideInfluenced].Parse(false);
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
