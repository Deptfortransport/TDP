// *********************************************** 
// NAME			: TestInternationalPlannerManager.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 14/02/2010
// DESCRIPTION	: Nunit Test Class for testing the manager class for planning international journeys
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlannerControl/Test/TestInternationalPlannerManager.cs-arc  $
//
//   Rev 1.0   Feb 16 2010 17:48:54   mmodi
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.InternationalPlanner;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.InternationalPlannerControl.Test
{
    /// <summary>
    /// Test Class for testing the manager class for planning international journeys
    /// </summary>
    [TestFixture]
    public class TestInternationalPlannerManager
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
                
        private string validCityId1 = "1";
        private string validCityId2 = "2";

        // Test Data (same files from InternationalPlanner project)
        private string TEST_DATA = "C:\\TDPortal\\Codebase\\TransportDirect\\InternationalPlanner\\bin\\Debug\\InternationalPlannerTestData\\InternationalPlannerData.xml";
        private string SETUP_SCRIPT = "C:\\TDPortal\\Codebase\\TransportDirect\\InternationalPlanner\\bin\\Debug\\InternationalPlannerTestData\\InternationalPlannerSetup.sql";
        private string CLEARUP_SCRIPT = "C:\\TDPortal\\Codebase\\TransportDirect\\InternationalPlanner\\bin\\Debug\\InternationalPlannerTestData\\InternationalPlannerCleanUp.sql";
        private const string connectionString = "Server=.;Initial Catalog=InternationalData;Trusted_Connection=true";
        private TestDataManager tm;

        #endregion

        /// <summary>
        /// Initialisation in setup method called before every test method
        /// </summary>
        [TestFixtureSetUp]
        public void Init()
        {
            TDServiceDiscovery.ResetServiceDiscoveryForTest();
            TDServiceDiscovery.Init(new TestInitialisation());

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
            // Put database data back to what it was
            tm.ClearData();
        }

        /// <summary>
        /// Tests international journeys are returned from the International Planner
        /// </summary>
        [NUnit.Framework.Test]
        public void TestJouneyPlannerManager()
        {
            IInternationalPlannerManager manager = (IInternationalPlannerManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerManager];

            #region Setup Request

            string sessionId = "qwerty";
            int userType = 1;

            ITDJourneyRequest request = new TDJourneyRequest();

            OSGridReference osgr = new OSGridReference(123456, 123456);

            TDLocation originLocation = new TDLocation();
            originLocation.Description = "London";
            originLocation.CityId = validCityId1;
            originLocation.NaPTANs = new TDNaptan[3] { 
                new TDNaptan(validAirStopNaptan1, osgr),
                new TDNaptan(validRailStopNaptan1, osgr),
                new TDNaptan(validCoachStopNaptan1, osgr)};

            TDLocation destinationLocation = new TDLocation();
            destinationLocation.Description = "Paris";
            destinationLocation.CityId = validCityId2;
            destinationLocation.NaPTANs = new TDNaptan[3] { 
                new TDNaptan(validAirStopNaptan2, osgr),
                new TDNaptan(validRailStopNaptan2, osgr),
                new TDNaptan(validCoachStopNaptan2, osgr)};

            request.OriginLocation = originLocation;
            request.DestinationLocation = destinationLocation;

            request.Modes = new ModeType[3] { ModeType.Air, ModeType.Rail, ModeType.Coach };

            request.OutwardDateTime = new TDDateTime[1] { new TDDateTime(DateTime.Now) };
            
            #endregion

            // Get the journeys
            ITDJourneyResult result = manager.CallInternationalPlanner(request, sessionId, userType, false, false, "en");
                        

            Assert.IsNotNull(result, "No result object returned, result was null.\n");

            if (result != null)
            {
                Assert.IsNotNull(result.OutwardPublicJourneys, "No public journeys were returned in the result.\n");

                if (result.OutwardPublicJourneys != null)
                {
                    Assert.GreaterOrEqual(result.OutwardPublicJourneyCount, 1, "No public international journeys were returned, expected journey count to be greater than 0.\n");
                }
            }
        }
    }
}
