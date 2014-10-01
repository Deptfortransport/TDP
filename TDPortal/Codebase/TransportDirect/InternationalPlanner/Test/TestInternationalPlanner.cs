// *********************************************** 
// NAME			: TestInternationalPlanner.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 26/01/2010
// DESCRIPTION	: Nunit Test Class for testing the planning of international journeys using the International planner
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlanner/Test/TestInternationalPlanner.cs-arc  $
//
//   Rev 1.6   Apr 26 2010 11:25:52   mmodi
//Added test to check if javascript data file is created
//Resolution for 5521: TD Extra - Drop Down List Change - CCN0575
//
//   Rev 1.5   Feb 25 2010 13:19:06   mmodi
//Updated change notification group name
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.4   Feb 18 2010 16:37:56   mmodi
//Updated following database tables change
//
//   Rev 1.3   Feb 16 2010 17:47:14   mmodi
//Updated tests
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Feb 09 2010 09:54:32   mmodi
//Updated
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.1   Feb 04 2010 10:27:02   mmodi
//Updated tests
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Jan 29 2010 12:03:50   mmodi
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.InternationalPlanner.Test
{
    /// <summary>
    /// Test Class for testing the planning of international journeys using the International planner
    /// </summary>
    [TestFixture]
    public class TestInternationalPlanner
    {
        #region Private members

        // Ensure Naptans and City Ids are compatable (i.e. the stop naptan exists for the city id)
        // otherwise journeys may not be returned
        private string validAirStopNaptan1 = "9200LHR"; // London Heathrow
        private string validAirStopNaptan2 = "9200CDG"; // Paris Charles de Gaule
        private string validCoachStopNaptan1 = "90004567123"; // London Victoria
        private string validCoachStopNaptan2 = "90004321123"; // Paris Coach
        private string validRailStopNaptan1 = "91007015400"; // London St Pancras
        private string validRailStopNaptan2 = "91008700804"; // Paris 

        private string invalidStopNaptan = "xxx";
        private string invalidModeStopNaptan = "9200XXX";

        private string validCityId1 = "1";
        private string validCityId2 = "2";

        private string invalidCityId = "-1";

        // Test Data
        private string TEST_DATA = Directory.GetCurrentDirectory() + "\\InternationalPlannerTestData\\InternationalPlannerData.xml";
        private string SETUP_SCRIPT = Directory.GetCurrentDirectory() + "\\InternationalPlannerTestData\\InternationalPlannerSetup.sql";
        private string CLEARUP_SCRIPT = Directory.GetCurrentDirectory() + "\\InternationalPlannerTestData\\InternationalPlannerCleanUp.sql";
        private const string connectionString = "Server=.;Initial Catalog=InternationalData;Trusted_Connection=true";
        private TestDataManager tm;

        // Used for generating javascript file
        public static readonly string scriptsFolderPath = Directory.GetCurrentDirectory() + @"\InternationalPlannerTestData\TestScriptRepository";
        public static readonly string scriptsFilePath = scriptsFolderPath + @"\scripts.xml";
        public static readonly string tempScriptsFolderPath = scriptsFolderPath + @"\tempscripts";

        #endregion

        #region Setup and Teardown

        /// <summary>
        /// Initialisation in setup method called before every test method
        /// </summary>
        [TestFixtureSetUp]
        public void Init()
        {
            // Clear down temp scripts folder
            DirectoryInfo scriptsFolder = new DirectoryInfo(tempScriptsFolderPath);
            if (scriptsFolder.Exists)
                scriptsFolder.Delete(true);

            // Initialise the service discovery
            TDServiceDiscovery.ResetServiceDiscoveryForTest();
            TDServiceDiscovery.Init(new TestInternationalPlannerInitialisation());

            // Set up the test data to use
            tm = new TestDataManager(TEST_DATA, SETUP_SCRIPT, CLEARUP_SCRIPT, connectionString, SqlHelperDatabase.InternationalDataDB);
            tm.Setup();
            tm.LoadData(false);
        }

        /// <summary>
        /// Tidy up following test completion
        /// </summary>
        [TestFixtureTearDown]
        public void TearDown()
        {
            // Clear down temp scripts folder
            DirectoryInfo scriptsFolder = new DirectoryInfo(tempScriptsFolderPath);
            if (scriptsFolder.Exists)
                scriptsFolder.Delete(true);

            // Put database data back to what it was
            tm.ClearData();
        }

        #endregion

        #region Test Javascript file created
        
        /// <summary>
        /// Test to ensure that when the InternationalPlannerDataFactory is initialised with the ScriptRepository
        /// present in the TDServiceDiscovery cache, the temporary script file is created.
        /// </summary>
        [Test]
        public void TestInitialisationWithScriptRepository()
        {
            InternationalPlannerData intlPlannerData = (InternationalPlannerData)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerDataFactory];

            Assert.IsTrue(intlPlannerData.ScriptGenerated, "Temporary script was not created successfully.");

            // The adp thinks that the script was generated - verify that it's present.
            // We know it was empty when the script repository was initialised.
            DirectoryInfo scriptsFolder = new DirectoryInfo(tempScriptsFolderPath);
            if (scriptsFolder.Exists)
                Assert.AreEqual(1, scriptsFolder.GetFiles("*.js").Length, "An unexpected number of scripts was found in the temporary script repository folder.");
            else
                Assert.Fail("Temporary scripts folder was not created.");
        }

        #endregion

        #region Test Get International objects

        /// <summary>
        /// Tests the GetStopInformation method to retrieve International stops from the database
        /// </summary>
        [NUnit.Framework.Test]
        public void TestGetInternationalStop()
        {
            InternationalPlannerHelper helper = new InternationalPlannerHelper();

            string[] naptans = new string[0];

            #region Valid naptan

            naptans = new string[1] { validAirStopNaptan1 };
            InternationalStop[] stops = helper.GetInternationalStop(naptans, true);

            Assert.IsNotNull(stops, "Test for valid StopNaptan failed - InternationalStops array is null");
            
            if (stops != null)
            {
                Assert.AreEqual(1, stops.Length, string.Format("Test for valid StopNaptan failed - expected 1 InternationalStop to be returned but {0} were found", stops.Length));

                if (stops.Length > 0)
                {
                    Assert.IsNotEmpty(stops[0].StopCode, string.Format("Test for valid StopNaptans failed - expected international stop naptan {0} to have a stop code", naptans[0]));
                }
            }

            #endregion

            #region Multiple naptans

            naptans = new string[2] { validAirStopNaptan1, validRailStopNaptan1 };
            stops = helper.GetInternationalStop(naptans, true);

            Assert.IsNotNull(stops, "Test for multiple valid StopNaptans failed - InternationalStops array is null");

            if (stops != null)
            {
                Assert.AreEqual(2, stops.Length, string.Format("Test for valid StopNaptans failed - expected 2 InternationalStops to be returned but {0} were found", stops.Length));

                if (stops.Length > 1)
                {
                    Assert.IsNotEmpty(stops[0].StopCode, string.Format("Test for valid StopNaptans failed - expected international stop naptan {0} to have a stop code", naptans[0]));
                    Assert.IsNotEmpty(stops[1].StopName, string.Format("Test for valid StopNaptans failed - expected international stop naptan {0} to have a stop name", naptans[1]));
                }
            }

            #endregion

            #region Invalid naptans

            naptans = new string[2] { invalidStopNaptan, invalidStopNaptan };
            stops = helper.GetInternationalStop(naptans, true);

            Assert.IsNotNull(stops, "Test for invalid StopNaptans failed - InternationalStops array is null");

            if (stops != null)
            {
                Assert.AreEqual(1, stops.Length, string.Format("Test for invalid StopNaptans failed - expected 1 dummy InternationalStops to be returned but {0} were found", stops.Length));

                if (stops.Length >= 1)
                {
                    Assert.AreEqual(InternationalModeType.None, stops[0].StopType, string.Format("Test for invalid StopNaptans failed - expected stop ModeType to be InternationalModeType.None but it was {0}", stops[0].StopType));
                }
            } 

            #endregion

            #region Invalid mode type for international stop

            naptans = new string[1] { invalidModeStopNaptan };

            try
            {
                stops = helper.GetInternationalStop(naptans, true);

                Assert.IsNull(stops, "Test for invalid stop mode type failed - InternationalStops array was not null");
            }
            catch (TDException tdEx)
            {
                // Exception should be raised
                TDException innerEx = (TDException)tdEx.InnerException;

                Assert.AreEqual(TDExceptionIdentifier.IPUnrecognisedInternationalStopModeType, innerEx.Identifier,
                    string.Format("Test for invalid stop mode type failed - expected error identifier IPUnrecognisedInternationalStopModeType but actual identifier is [{0}]",
                    innerEx.Identifier));
            }

            #endregion
        }

        /// <summary>
        /// Tests the GetInternationalCity method to retrieve an International city from the database
        /// </summary>
        [NUnit.Framework.Test]
        public void TestGetInternationalCity()
        {
            InternationalPlannerHelper helper = new InternationalPlannerHelper();

            string cityID = string.Empty;

            #region Valid city

            cityID = validCityId1;
            
            InternationalCity city = helper.GetInternationalCity(cityID, true);

            Assert.IsNotNull(city, "Test for valid City failed - InternationalCity is null");

            if (city != null)
            {
                Assert.IsNotEmpty(city.CityName, "Test for valid City failed - expected international city to have a name");
            }

            #endregion

            #region Invalid city

            cityID = invalidCityId;
            
            city = helper.GetInternationalCity(cityID, true);

            Assert.IsNull(city, "Test for invalid city failed - International city was returned");
                        
            #endregion
        }

        #endregion

        #region Test Plan journeys

        /// <summary>
        /// Tests an international journey for Air is returned from the International Planner
        /// </summary>
        [NUnit.Framework.Test]
        public void TestInternationalPlannerForAir()
        {
            IInternationalPlannerRequest request = new InternationalPlannerRequest();

            #region Setup Request

            #region Initialise the request

            request.RequestID = "111111";
            request.SessionID = "aaaaaa";
            request.UserType = 1;

            #endregion

            #region Set locations and date/times

            request.OriginName = "Air Origin name";
            request.OriginCityID = validCityId1;
            request.OriginNaptans = new string[1] { validAirStopNaptan1 };
            request.DestinationName = "Air Destination name";
            request.DestinationCityID = validCityId2;
            request.DestinationNaptans = new string[1] { validAirStopNaptan2 };
            request.ModeType = new InternationalModeType[1] { InternationalModeType.Air };
            request.OutwardDateTime = DateTime.Now;

            #endregion

            #endregion

            IInternationalPlanner internationalPlanner = (IInternationalPlanner)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerFactory];

            IInternationalPlannerResult result = internationalPlanner.InternationalJourneyPlan(request);

            Assert.IsNotNull(result, "No international result object returned, result was null.\n");

            if (result != null)
            {
                Assert.IsNotNull(result.InternationalJourneys, "No international journeys were returned.\n");

                if (result.InternationalJourneys != null)
                {
                    Assert.GreaterOrEqual(result.InternationalJourneys.Length, 1, "No Air international journeys were returned, expected journey count to be greater than 0.\n");
                }
            }
        }

        /// <summary>
        /// Tests an international journey for Coach is returned from the International Planner
        /// </summary>
        [NUnit.Framework.Test]
        public void TestInternationalPlannerForCoach()
        {
            IInternationalPlannerRequest request = new InternationalPlannerRequest();

            #region Setup Request

            #region Initialise the request

            request.RequestID = "222222";
            request.SessionID = "bbbbbb";
            request.UserType = 1;

            #endregion

            #region Set locations and date/times

            request.OriginName = "Coach Origin name";
            request.OriginCityID = validCityId1;
            request.OriginNaptans = new string[1] { validCoachStopNaptan1 };
            request.DestinationName = "Coach Destination name";
            request.DestinationCityID = validCityId2;
            request.DestinationNaptans = new string[1] { validCoachStopNaptan2 };
            request.ModeType = new InternationalModeType[1] { InternationalModeType.Coach };
            request.OutwardDateTime = DateTime.Now;

            #endregion

            #endregion

            IInternationalPlanner internationalPlanner = (IInternationalPlanner)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerFactory];

            IInternationalPlannerResult result = internationalPlanner.InternationalJourneyPlan(request);

            Assert.IsNotNull(result, "No international result object returned, result was null.\n");

            if (result != null)
            {
                Assert.IsNotNull(result.InternationalJourneys, "No international journeys were returned.\n");

                if (result.InternationalJourneys != null)
                {
                    Assert.GreaterOrEqual(result.InternationalJourneys.Length, 1, "No Coach international journeys were returned, expected journey count to be greater than 0.\n");
                }
            }
        }

        /// <summary>
        /// Tests an international journey for Rail is returned from the International Planner
        /// </summary>
        [NUnit.Framework.Test]
        public void TestInternationalPlannerForRail()
        {
            IInternationalPlannerRequest request = new InternationalPlannerRequest();

            #region Setup Request

            #region Initialise the request

            request.RequestID = "333333";
            request.SessionID = "cccccc";
            request.UserType = 1;

            #endregion

            #region Set locations and date/times

            request.OriginName = "Rail Origin name";
            request.OriginCityID = validCityId1;
            request.OriginNaptans = new string[1] { validRailStopNaptan1 };
            request.DestinationName = "Rail Destination name";
            request.DestinationCityID = validCityId2;
            request.DestinationNaptans = new string[1] { validRailStopNaptan2 };
            request.ModeType = new InternationalModeType[1] { InternationalModeType.Rail };
            request.OutwardDateTime = DateTime.Now;

            #endregion

            #endregion

            IInternationalPlanner internationalPlanner = (IInternationalPlanner)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerFactory];

            IInternationalPlannerResult result = internationalPlanner.InternationalJourneyPlan(request);

            Assert.IsNotNull(result, "No international result object returned, result was null.\n");

            if (result != null)
            {
                Assert.IsNotNull(result.InternationalJourneys, "No international journeys were returned.\n");

                if (result.InternationalJourneys != null)
                {
                    Assert.GreaterOrEqual(result.InternationalJourneys.Length, 1, "No Rail international journeys were returned, expected journey count to be greater than 0.\n");
                }
            }
        }

        /// <summary>
        /// Tests an international journey for Car is returned from the International Planner
        /// </summary>
        [NUnit.Framework.Test]
        public void TestInternationalPlannerForCar()
        {
            IInternationalPlannerRequest request = new InternationalPlannerRequest();

            #region Setup Request

            #region Initialise the request

            request.RequestID = "444444";
            request.SessionID = "dddddd";
            request.UserType = 1;

            #endregion

            #region Set locations and date/times

            request.OriginName = "Car Origin name";
            request.OriginCityID = validCityId1;
            request.OriginNaptans = new string[1] { validAirStopNaptan1 }; // Naptan not needed for car journey
            request.DestinationName = "Car Destination name";
            request.DestinationCityID = validCityId2;
            request.DestinationNaptans = new string[1] { validAirStopNaptan2 }; // Naptan not needed for car journey
            request.ModeType = new InternationalModeType[1] { InternationalModeType.Car };
            request.OutwardDateTime = DateTime.Now;

            #endregion

            #endregion

            IInternationalPlanner internationalPlanner = (IInternationalPlanner)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerFactory];

            IInternationalPlannerResult result = internationalPlanner.InternationalJourneyPlan(request);

            Assert.IsNotNull(result, "No international result object returned, result was null.\n");

            if (result != null)
            {
                Assert.IsNotNull(result.InternationalJourneys, "No international journeys were returned.\n");

                if (result.InternationalJourneys != null)
                {
                    Assert.GreaterOrEqual(result.InternationalJourneys.Length, 1, "No Car international journeys were returned, expected journey count to be greater than 0.\n");
                }
            }
        }

        /// <summary>
        /// Tests an international journey for all International Modes is returned from the International Planner
        /// </summary>
        [NUnit.Framework.Test]
        public void TestInternationalPlannerForAllModes()
        {
            IInternationalPlannerRequest request = new InternationalPlannerRequest();

            #region Setup Request

            #region Initialise the request

            request.RequestID = "111111";
            request.SessionID = "aaaaaa";
            request.UserType = 1;

            #endregion

            #region Set locations and date/times

            request.OriginName = "Origin name";
            request.OriginCityID = validCityId1;
            request.OriginNaptans = new string[3] { validAirStopNaptan1, validRailStopNaptan1, validCoachStopNaptan1 };
            request.DestinationName = "Destination name";
            request.DestinationCityID = validCityId2;
            request.DestinationNaptans = new string[3] { validAirStopNaptan2, validRailStopNaptan2, validCoachStopNaptan2 };
            request.ModeType = new InternationalModeType[4] { InternationalModeType.Air, InternationalModeType.Coach, InternationalModeType.Rail, InternationalModeType.Car };
            request.OutwardDateTime = DateTime.Now;

            #endregion

            #endregion

            IInternationalPlanner internationalPlanner = (IInternationalPlanner)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerFactory];

            IInternationalPlannerResult result = internationalPlanner.InternationalJourneyPlan(request);

            Assert.IsNotNull(result, "No international result object returned, result was null.\n");

            if (result != null)
            {
                Assert.IsNotNull(result.InternationalJourneys, "No international journeys were returned.\n");

                if (result.InternationalJourneys != null)
                {
                    Assert.GreaterOrEqual(result.InternationalJourneys.Length, 1, "No international journeys were returned, expected journey count to be greater than 0.\n");


                    // Check if theres a journey for each mode type
                    List<InternationalModeType> modes = new List<InternationalModeType>();

                    foreach (InternationalJourney ij in result.InternationalJourneys)
                    {
                        if (!modes.Contains(ij.ModeType))
                        {
                            modes.Add(ij.ModeType);
                        }
                    }

                    // Should be 4 different modes
                    Assert.AreEqual(4, modes.Count, "International journeys planned did not contain all four InternationalModeType's");
                }
            }
        }

        /// <summary>
        /// Tests an international journey is returned from the cache
        /// </summary>
        [NUnit.Framework.Test]
        public void TestInternationalJourneyPlannedFromCache()
        {
            IInternationalPlannerRequest request = new InternationalPlannerRequest();

            #region Setup Request

            #region Initialise the request

            request.RequestID = "444444";
            request.SessionID = "dddddd";
            request.UserType = 1;

            #endregion

            #region Set locations and date/times

            request.OriginName = "Origin name";
            request.OriginCityID = validCityId1;
            request.OriginNaptans = new string[1] { validRailStopNaptan1 };
            request.DestinationName = "Destination name";
            request.DestinationCityID = validCityId2;
            request.DestinationNaptans = new string[1] { validRailStopNaptan2 };
            request.ModeType = new InternationalModeType[1] { InternationalModeType.Rail };
            request.OutwardDateTime = DateTime.Now;

            #endregion

            #endregion

            IInternationalPlanner internationalPlanner = (IInternationalPlanner)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerFactory];

            IInternationalPlannerResult result = internationalPlanner.InternationalJourneyPlan(request);

            Assert.IsNotNull(result, "No international result object returned, result was null.\n");

            if (result != null)
            {
                Assert.IsNotNull(result.InternationalJourneys, "No international journeys were returned.\n");

                if (result.InternationalJourneys != null)
                {
                    Assert.GreaterOrEqual(result.InternationalJourneys.Length, 1, "No international journeys were returned, expected journey count to be greater than 0.\n");
                }
            }

            // The journey should now have been added to the data cache, attempt to manually retrieve it from there first
            InternationalPlannerData internationalPlannerData = (InternationalPlannerData)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerDataFactory];

            InternationalJourney[] intlJourneys = internationalPlannerData.GetInternationalJourneys(request.OriginCityID, request.DestinationCityID, request.OriginNaptans[0], request.DestinationNaptans[0], InternationalModeType.Rail);

            Assert.IsNotNull(intlJourneys, "No international journeys were found in the data cache.\n");

            if (intlJourneys != null)
            {
                Assert.GreaterOrEqual(intlJourneys.Length, 1, "No international journeys were returned from data cache, expected journey count to be greater than 0.\n");
            }

            // Now call the planner again, which will retreive the journeys from the cache
            // Update the request time and test the returned journeys now are for the updated request date
            request.OutwardDateTime = request.OutwardDateTime.AddDays(5);

            result = internationalPlanner.InternationalJourneyPlan(request);

            Assert.IsNotNull(result, "No international result object returned from journey planned using data cache, result was null.\n");

            if (result != null)
            {
                Assert.IsNotNull(result.InternationalJourneys, "No international journeys were returned from journey planned using data cache.\n");

                if (result.InternationalJourneys != null)
                {
                    Assert.GreaterOrEqual(result.InternationalJourneys.Length, 1, "No international journeys were returned from journey planned using data cache, expected journey count to be greater than 0.\n");

                    if (result.InternationalJourneys.Length >= 1)
                    {
                        DateTime journeyDateTime = result.InternationalJourneys[0].DepartureDateTime;

                        // Check dates are the same
                        Assert.AreEqual(request.OutwardDateTime.Day, journeyDateTime.Day, "International journeys returned from cache journey dates were not updated to the requested day.\n" );
                        Assert.AreEqual(request.OutwardDateTime.Month, journeyDateTime.Month, "International journeys returned from cache journey dates were not updated to the requested month.\n");
                        Assert.AreEqual(request.OutwardDateTime.Year, journeyDateTime.Year, "International journeys returned from cache journey dates were not updated to the requested year.\n");
                    }
                }
            }

        }

        #endregion

        #region Test Change notification

        /// <summary>
        /// Tests that the InternationalPlannerData is cleared after DataChangeNotification event
        /// </summary>
        [NUnit.Framework.Test]
        public void TestNotificationService()
        {
            // Reset service discovery
            TDServiceDiscovery.ResetServiceDiscoveryForTest();
            TDServiceDiscovery.Init(new TestInternationalPlannerInitialisation());

            // Register data notification
            TestMockDataChangeNotification dataChangeNotification = (TestMockDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];

            // Data cache
            InternationalPlannerData refData = (InternationalPlannerData)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerDataFactory];


            string stopNaptan = validAirStopNaptan1;


            // Test there are no InternationalStops in cache
            InternationalStop intlStop = refData.GetInternationalStop(stopNaptan);

            Assert.IsNull(intlStop, "International stop was not null, the Data cache contains items and failed being reset");

            
            // Call method to get InternationalStop - this ensures the data is added to the data dictionary cache
            InternationalPlannerHelper helper = new InternationalPlannerHelper();
                        
            string[] naptans = new string[1] { stopNaptan };
            
            InternationalStop[] stops = helper.GetInternationalStop(naptans, true);

            Assert.IsNotNull(stops, "Test for valid StopNaptan failed - InternationalStops array is null");

            // Check its a valid stop
            if (stops != null)
            {
                Assert.AreEqual(1, stops.Length, string.Format("Test for valid StopNaptan failed - expected 1 InternationalStop to be returned but {0} were found", stops.Length));

                if (stops.Length > 0)
                {
                    Assert.IsNotEmpty(stops[0].StopCode, string.Format("Test for valid StopNaptans failed - expected international stop naptan {0} to have a stop code", naptans[0]));
                }
            }
            

            // Test the InternationalStops data cache now has a stop in it
            intlStop = null;
            intlStop = refData.GetInternationalStop(stopNaptan);

            Assert.IsNotNull(intlStop, "International stop was null, the Data cache does not contains items after it should have been populated");


            
            
            //Manually raise a change notification event
            dataChangeNotification.RaiseChangedEvent("InternationalPlanner");
            refData = (InternationalPlannerData)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerDataFactory];


            // Test there are no InternationalStops in cache after the change notification fired
            intlStop = refData.GetInternationalStop(stopNaptan);

            Assert.IsNull(intlStop, "International stop was not null after change notification event fired, the Data cache contains items and failed being cleared");

        }

        #endregion
    }

    #region Initialisation class

    /// <summary>
    /// Initialisation class 
    /// </summary>
    public class TestInternationalPlannerInitialisation : IServiceInitialisation
    {
        /// <summary>
        /// Populates sevice cache with services needed 
        /// </summary>
        /// <param name="serviceCache">Cache to populate.</param>
        public void Populate(Hashtable serviceCache)
        {
            // Add cryptographic scheme
            //serviceCache.Add(ServiceDiscoveryKey.Crypto, new CryptoFactory());

            // Enable PropertyService					
            serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

            // Enable logging service.
            ArrayList errors = new ArrayList();
            try
            {
                IEventPublisher[] customPublishers = new IEventPublisher[0];

                Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
            }
            catch (TDException)
            {
                foreach (string error in errors)
                {
                    Console.WriteLine(error);
                }
                throw;
            }

            // Enable Test ChangeNotification
            serviceCache.Add(ServiceDiscoveryKey.DataChangeNotification, new TestMockDataChangeNotification());

            // Enable Scripts
            serviceCache.Add(ServiceDiscoveryKey.ScriptRepository, new ScriptRepository.ScriptRepositoryFactory(TestInternationalPlanner.scriptsFolderPath, TestInternationalPlanner.scriptsFilePath));

            // Enable InternationalPlannerFactory
            serviceCache.Add(ServiceDiscoveryKey.InternationalPlannerFactory, new InternationalPlannerFactory());

            // Enable InternationalPlannerDataFactory
            serviceCache.Add(ServiceDiscoveryKey.InternationalPlannerDataFactory, new InternationalPlannerDataFactory());

        }
    }
    #endregion

}
