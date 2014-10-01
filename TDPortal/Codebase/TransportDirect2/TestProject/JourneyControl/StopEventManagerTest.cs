// *********************************************** 
// NAME             : StopEventManagerTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 26 Apr 2011
// DESCRIPTION  	: StopEventManagerTest test class
// ************************************************
// 
                
using TDP.UserPortal.JourneyControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.PropertyManager;
using TDP.Common.LocationService;
using TDP.Common.ServiceDiscovery;
using System.Runtime.Remoting;
using System.IO;
using TDP.Common.EventLogging;
using TDP.Common.Extenders;

using Logger = System.Diagnostics.Trace;
using TDP.Common;
using JC = TDP.UserPortal.JourneyControl;
using System.Collections.Generic;
using TDP.Common.DataServices;
using TDP.Common.LocationService.GIS;



namespace TDP.TestProject.JourneyControl
{
    
    
    /// <summary>
    ///This is a test class for StopEventManagerTest and is intended
    ///to contain all StopEventManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StopEventManagerTest
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
        ///A test for CallCJP
        ///</summary>
        [TestMethod()]
        public void StopEventManagerCallCJPTest()
        {
            StopEventManagerFactory factory = new StopEventManagerFactory();
            StopEventManager manager = (StopEventManager)factory.Get();
            Assert.IsNotNull(manager, "StopEventManager is null, failed to get StopEventManager using Factory object");

            StopEventManager target = new StopEventManager();
            ITDPJourneyRequest request = InitialiseStopEventRequest();

            string sessionId = "StopEventManagerTest_CallCJPTest";
            bool referenceTransaction = false;
            string language = "en";

            // Perform test
            ITDPJourneyResult actual = target.CallCJP(request, sessionId, referenceTransaction, language);

            // Check journeys were created for the stop event results
            bool hasJourneys = (actual.OutwardJourneys.Count > 0);
            bool hasCorrectNumOfJourneysOutward = (actual.OutwardJourneys.Count == request.Sequence);
            bool hasCorrectNumOfJourneysReturn = (request.IsReturnRequired) ? (actual.ReturnJourneys.Count == request.Sequence) : true;

            // Check test resuls
            Assert.IsNotNull(actual);
            Assert.IsTrue(hasJourneys, "Excepted at least 1 journey in the result");
            Assert.IsTrue(hasCorrectNumOfJourneysOutward,
                            string.Format("Excepted [{0}] outward journeys in the result, result contained [{1}]", request.Sequence, actual.OutwardJourneys.Count));
            Assert.IsTrue(hasCorrectNumOfJourneysReturn,
                            string.Format("Excepted [{0}] return journeys in the result, result contained [{1}]", request.Sequence, actual.ReturnJourneys.Count));
        }

        /// <summary>
        ///A test for CallCJP
        ///</summary>
        [TestMethod()]
        public void StopEventAndCJPTest()
        {
            #region Get Stop Event journeys

            StopEventManager target = new StopEventManager();
            ITDPJourneyRequest request = InitialiseStopEventRequest();

            string sessionId = "StopEventManagerTest_CallCJPTest";
            bool referenceTransaction = false;
            string language = "en";

            // Perform test
            ITDPJourneyResult actual = target.CallCJP(request, sessionId, referenceTransaction, language);

            // Check journeys were created for the stop event results
            bool hasJourneys = (actual.OutwardJourneys.Count > 0);
            bool hasCorrectNumOfJourneysOutward = (actual.OutwardJourneys.Count == request.Sequence);
            bool hasCorrectNumOfJourneysReturn = (request.IsReturnRequired) ? (actual.ReturnJourneys.Count == request.Sequence) : true;

            // Check test resuls
            Assert.IsNotNull(actual);
            Assert.IsTrue(hasJourneys, "Excepted at least 1 journey in the result");
            Assert.IsTrue(hasCorrectNumOfJourneysOutward,
                            string.Format("Excepted [{0}] outward journeys in the result, result contained [{1}]", request.Sequence, actual.OutwardJourneys.Count));
            Assert.IsTrue(hasCorrectNumOfJourneysReturn,
                            string.Format("Excepted [{0}] return journeys in the result, result contained [{1}]", request.Sequence, actual.ReturnJourneys.Count));

            #endregion

            #region Get CJP journeys, passing in the Stop Event journeys

            CJPManager cjpManager = new CJPManager();

            // Initialise a journey request
            ITDPJourneyRequest cjpRequest = InitialiseJourneyRequest();
            
            // Add the stop event journeys
            cjpRequest.OutwardJourneyPart = actual.OutwardJourneys[0];
            cjpRequest.ReturnJourneyPart = actual.ReturnJourneys[0];

            string cjpSessionId = "CJPManagerTest_CallCJPTest1";

            // Perform test, result should contain the journey parts we added
            ITDPJourneyResult cjpResult = cjpManager.CallCJP(cjpRequest, cjpSessionId, referenceTransaction, language);

            hasJourneys = (cjpResult.OutwardJourneys.Count > 0);
            hasCorrectNumOfJourneysOutward = (cjpResult.OutwardJourneys.Count == cjpRequest.Sequence);
            hasCorrectNumOfJourneysReturn = (cjpRequest.IsReturnRequired) ? (cjpResult.ReturnJourneys.Count == cjpRequest.Sequence) : true;

            // Check test resuls
            Assert.IsNotNull(actual);
            Assert.IsTrue(hasJourneys, "Excepted at least 1 journey in the cjp result");
            Assert.IsTrue(hasCorrectNumOfJourneysOutward,
                            string.Format("Excepted [{0}] outward journeys in the cjp result, result contained [{1}]", request.Sequence, actual.OutwardJourneys.Count));
            Assert.IsTrue(hasCorrectNumOfJourneysReturn,
                            string.Format("Excepted [{0}] return journeys in the cjp result, result contained [{1}]", request.Sequence, actual.ReturnJourneys.Count));
            
            #endregion
        }

        #region Private methods

        /// <summary>
        /// Initialises a stop event request with standard request values
        /// for an outward and return journey
        /// </summary>
        /// <returns></returns>
        private ITDPJourneyRequest InitialiseStopEventRequest()
        {
            IPropertyProvider pp = Properties.Current;

            ITDPJourneyRequest request = new TDPJourneyRequest();

            request.JourneyRequestHash = "Test";

            
            TDPLocation origin = new TDPLocation("Tower Millennium Pier", TDPLocationType.Station, TDPLocationType.Unknown, "9300TMP");

            TDPLocation destination = new TDPLocation("Greenwich Pier ", TDPLocationType.Station, TDPLocationType.Unknown, "9300GNW1");
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

            LocationSearch originSearch = new LocationSearch(string.Empty, "9100CAMBDGE", TDPLocationType.Station, true);
            LocationSearch destinationSearch = new LocationSearch(string.Empty, "9100GNWH", TDPLocationType.Station, true);

            request.Origin = locationService.ResolveLocation(ref originSearch, false, "TestSession");
            request.Destination = locationService.ResolveLocation(ref destinationSearch, false, "TestSession");

            // Fix to ensure planning for 2012
            DateTime dtOutward = DateTime.Now;
            DateTime dtOutward2012 = new DateTime(2012, 8, 1, 11, 0, 0);
            if (dtOutward < dtOutward2012)
            {
                dtOutward = dtOutward2012;
            }

            request.OutwardDateTime = dtOutward;
            request.ReturnDateTime = dtOutward.AddHours(4);
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

        #endregion
    }
}
