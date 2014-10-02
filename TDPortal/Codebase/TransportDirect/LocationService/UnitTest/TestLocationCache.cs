// *********************************************** 
// NAME                 : TestLocationCache.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 06/09/2012 
// DESCRIPTION  	    : Unit test class containing unit tests for TDLocationCache, and LocationService classes
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestLocationCache.cs-arc  $ 
//
//   Rev 1.3   Feb 07 2013 09:27:24   mmodi
//Corrected accessible natpans
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.2   Jan 07 2013 11:15:10   mmodi
//Unit test updates
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Dec 05 2012 14:09:00   mmodi
//Updated for accessible locations
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Sep 10 2012 10:20:48   mmodi
//Initial revision.
//Resolution for 5832: CCN0668 Gazetteer Enhancements - Auto-Suggest drop downs
//

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.IO;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.DataServices;
using System.Collections;
using TransportDirect.Common.Logging;
using System.Diagnostics;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService.Cache;
using LSC = TransportDirect.UserPortal.LocationService.Cache;

namespace TransportDirect.UserPortal.LocationService
{
    /// <summary>
    /// Unit test class containing unit tests for TDLocationCache, and LocationService classes
    /// </summary>
    [TestFixture]
    public class TestLocationCache
    {
        #region Private members

        // Test Data
        private string TEST_DATA = string.Empty;
        private string SETUP_SCRIPT = Directory.GetCurrentDirectory() + "\\LocationService\\AccessibleAdminAreasSetup.sql";
        private string CLEARUP_SCRIPT = Directory.GetCurrentDirectory() + "\\LocationService\\AccessibleAdminAreasCleanUp.sql";

        private string connectionString = "Integrated Security=SSPI;Initial Catalog=TransientPortal;Data Source=localhost;Connect Timeout=30;";
        private TestDataManager tm;

        #endregion

        #region SetUp and TearDown

        [TestFixtureSetUp]
        public void SetUp()
        {
            // Setup the test data before initialise is called
            tm = new TestDataManager(TEST_DATA, SETUP_SCRIPT, CLEARUP_SCRIPT,
                connectionString, SqlHelperDatabase.AtosAdditionalDataDB);
            tm.Setup();

            // Reset and initialise service discovery, will populate location cache
            TDServiceDiscovery.ResetServiceDiscoveryForTest();
            TDServiceDiscovery.Init(new TestLocationCacheInitialisation());
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            tm.ClearData();
        }

        /// <summary>
        /// Resets runs before every test
        /// </summary>
        [SetUp]
        public void Reset()
        {
        }


        #endregion

        #region Test methods

        /// <summary>
        /// Tests the location service factory returns a LocationService object
        /// </summary>
        [Test]
        public void TestLocationServiceCacheFactory()
        {
            LSC.LocationService locationService = (LSC.LocationService)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationServiceCache];

            Assert.IsNotNull(locationService);
        }

        /// <summary>
        /// Tests the location service returns a version value
        /// </summary>
        [Test]
        public void TestLocationServiceCacheVersion()
        {
            LSC.LocationService locationService = (LSC.LocationService)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationServiceCache];

            string version = locationService.LocationVersion();

            Assert.IsTrue(!string.IsNullOrEmpty(version));
        }

        /// <summary>
        /// Tests the location service resolves locations
        /// </summary>
        [Test]
        public void TestLocationServiceCacheResolveLocation()
        {
            LSC.LocationService locationService = (LSC.LocationService)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationServiceCache];

            LocationSearch search = null;
            TDLocation location = null;

            // Empty search text and location is null
            location = null;
            ResetSearchAndLocation(ref search, ref location, string.Empty, SearchType.MainStationAirport, false, true, true);
            locationService.ResolveLocation(ref search, ref location, string.Empty, TDStopType.Unknown, StationType.Undetermined, 20, 10, false, "session", false);

            Assert.IsNotNull(location);
            Assert.IsTrue(location.Status == TDLocationStatus.Unspecified, string.Format("Location status is {0}", location.Status));

            // Rail station "auto-suggest"
            ResetSearchAndLocation(ref search, ref location, "Leicester Rail Station", SearchType.MainStationAirport, false, true, true);
            locationService.ResolveLocation(ref search, ref location, "9100LESTER", TDStopType.Rail, StationType.Undetermined, 20, 10, false, "session", false);

            Assert.IsNotNull(location);
            Assert.IsTrue(location.Status == TDLocationStatus.Valid, string.Format("Location status is {0}", location.Status));
            Assert.IsTrue(!string.IsNullOrEmpty(location.Description));
            Assert.IsTrue(!string.IsNullOrEmpty(location.Locality));
            Assert.IsTrue(location.GridReference.IsValid);


            // Group location "auto-suggest"
            ResetSearchAndLocation(ref search, ref location, "London (Main stations)", SearchType.MainStationAirport, false, true, true);
            locationService.ResolveLocation(ref search, ref location, "G1", TDStopType.Group, StationType.Undetermined, 20, 10, false, "session", false);

            Assert.IsNotNull(location);
            Assert.IsTrue(location.Status == TDLocationStatus.Valid, string.Format("Location status is {0}", location.Status));
            Assert.IsTrue(!string.IsNullOrEmpty(location.Description));
            Assert.IsTrue(!string.IsNullOrEmpty(location.Locality));
            Assert.IsTrue(location.GridReference.IsValid);


            // Locality location "auto-suggest"
            ResetSearchAndLocation(ref search, ref location, "Leicester", SearchType.Locality, false, true, true);
            locationService.ResolveLocation(ref search, ref location, "E0057189", TDStopType.Locality, StationType.Undetermined, 20, 10, false, "session", false);

            Assert.IsNotNull(location);
            Assert.IsTrue(location.Status == TDLocationStatus.Valid, string.Format("Location status is {0}", location.Status));
            Assert.IsTrue(!string.IsNullOrEmpty(location.Description));
            Assert.IsTrue(!string.IsNullOrEmpty(location.Locality));
            Assert.IsTrue(location.GridReference.IsValid);


            // Unknown location which is found
            ResetSearchAndLocation(ref search, ref location, "Leicester Rail Station", SearchType.MainStationAirport, false, true, true);
            locationService.ResolveLocation(ref search, ref location, "", TDStopType.Unknown, StationType.Undetermined, 20, 10, false, "session", false);

            Assert.IsNotNull(location);
            Assert.IsTrue(location.Status == TDLocationStatus.Valid, string.Format("Location status is {0}", location.Status));
            Assert.IsTrue(!string.IsNullOrEmpty(location.Description));
            Assert.IsTrue(!string.IsNullOrEmpty(location.Locality));
            Assert.IsTrue(location.GridReference.IsValid);


            // Ambiguous location "auto-suggest"
            ResetSearchAndLocation(ref search, ref location, "Leicester station", SearchType.MainStationAirport, false, true, true);
            locationService.ResolveLocation(ref search, ref location, "", TDStopType.Unknown, StationType.Undetermined, 20, 10, false, "session", false);

            Assert.IsNotNull(search);
            Assert.IsTrue(search.GetAmbiguitySearchResult().Count > 0, "Expected ambigous location search results");

            Assert.IsNotNull(location);
            Assert.IsTrue(location.Status == TDLocationStatus.Ambiguous, string.Format("Location status is {0}", location.Status));
            Assert.IsTrue(string.IsNullOrEmpty(location.Description));
            Assert.IsTrue(string.IsNullOrEmpty(location.Locality));
            Assert.IsTrue(!location.GridReference.IsValid);
        }

        /// <summary>
        /// Tests a gazetteer event is logged
        /// </summary>
        [Test]
        public void TestLocationServiceCacheLogGazEvent()
        {
            LSC.LocationService locationService = (LSC.LocationService)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationServiceCache];

            object[] parameters = new object[3];
            parameters[0] = TDStopType.Rail;
            parameters[1] = "session";
            parameters[2] = false;

            try
            {
                TestHelper.RunInstanceMethod(typeof(LSC.LocationService), "LogGazetteerEvent", locationService, parameters);
            }
            catch (Exception ex)
            {
                // Should succeed
                Assert.Fail(string.Format("Exception thrown attempting to log gazetteer event, {0}, {1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// Tests change notifications
        /// </summary>
        [Test]
        public void TestLocationServiceCacheChangeNotifications()
        {
            LSC.LocationService locationService = (LSC.LocationService)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationServiceCache];

            // Get the old version number
            string oldVersion = locationService.LocationVersion();

            LocationSearch search = null;
            TDLocation location = null;
            
            // Check can resolve a location
            ResetSearchAndLocation(ref search, ref location, "Leicester Rail Station", SearchType.MainStationAirport, false, true, true);
            locationService.ResolveLocation(ref search, ref location, "9100LESTER", TDStopType.Rail, StationType.Undetermined, 20, 10, false, "session", false);

            Assert.IsNotNull(location);
            Assert.IsTrue(location.Status == TDLocationStatus.Valid, string.Format("Location status is {0}", location.Status));
            Assert.IsTrue(!string.IsNullOrEmpty(location.Description));
            Assert.IsTrue(!string.IsNullOrEmpty(location.Locality));
            Assert.IsTrue(location.GridReference.IsValid);


            // Manually update the version number
            int versionNumber = Convert.ToInt32(oldVersion);
            UpdateVersionNo(versionNumber + 1);
            
            // Register data notification
            TestMockDataChangeNotification dataChangeNotification = (TestMockDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];
                        
            //Manually raise change notification
            dataChangeNotification.RaiseChangedEvent("LocationService");

            
            // Get the latest locationService
            locationService = (LSC.LocationService)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationServiceCache];

            string newVersion = locationService.LocationVersion();

            Assert.IsTrue(!string.IsNullOrEmpty(newVersion));
            Assert.IsTrue(!oldVersion.Equals(newVersion));

            // Check can resolve a location
            ResetSearchAndLocation(ref search, ref location, "Leicester Rail Station", SearchType.MainStationAirport, false, true, true);
            locationService.ResolveLocation(ref search, ref location, "9100LESTER", TDStopType.Rail, StationType.Undetermined, 20, 10, false, "session", false);

            Assert.IsNotNull(location);
            Assert.IsTrue(location.Status == TDLocationStatus.Valid, string.Format("Location status is {0}", location.Status));
            Assert.IsTrue(!string.IsNullOrEmpty(location.Description));
            Assert.IsTrue(!string.IsNullOrEmpty(location.Locality));
            Assert.IsTrue(location.GridReference.IsValid);

            // And put the version back to what it was
            UpdateVersionNo(versionNumber);
        }

        /// <summary>
        /// Tests accessible location naptan method
        /// </summary>
        [Test]
        public void TestLocationServiceCacheIsAccessibleLocation()
        {
            LSC.LocationService locationService = (LSC.LocationService)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationServiceCache];

            // Expected data in TransientPortal.AccessibleLocations table
                     // StopNaPTAN	    StopName	                        StepFree    Assistance  
            string n1 = "900015035";    //Cardiff Bus Stn	                1	        1	        
            string n2 = "9300CHP"; 	    //Chelsea Harbour Pier	            1	        0	        
            string n3 = "9400ZZLUACT";  //Acton Town Underground Station    0	        1

            bool result = false;

            string naptan = null; 
            bool requireStepFreeAccess = false; 
            bool requireSpecialAssistance = false;

            result = locationService.IsAccessibleLocation(naptan, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == false, string.Format("Expected naptan[null] to not be accessible"));

            naptan = string.Empty;
            result = locationService.IsAccessibleLocation(naptan, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == false, string.Format("Expected naptan[{0}] to not be accessible", naptan));

            naptan = "TEST123";
            requireStepFreeAccess = true;
            requireSpecialAssistance = true;
            result = locationService.IsAccessibleLocation(naptan, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == false, string.Format("Expected test naptan[{0}] with to not be accessible", naptan));

            naptan = n1;
            requireStepFreeAccess = false;
            requireSpecialAssistance = false;
            result = locationService.IsAccessibleLocation(naptan, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == false, string.Format("Expected naptan[{0}] with no accessible requirements to not be accessible", naptan));

            naptan = n1;
            requireStepFreeAccess = true;
            requireSpecialAssistance = true;
            result = locationService.IsAccessibleLocation(naptan, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == true, string.Format("Expected naptan[{0}] with accessible requirements to be accessible", naptan));

            naptan = n1;
            requireStepFreeAccess = true;
            requireSpecialAssistance = false;
            result = locationService.IsAccessibleLocation(naptan, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == true, string.Format("Expected naptan[{0}] with accessible requirement to be accessible", naptan));

            naptan = n1;
            requireStepFreeAccess = false;
            requireSpecialAssistance = true;
            result = locationService.IsAccessibleLocation(naptan, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == true, string.Format("Expected naptan[{0}] with accessible requirement to be accessible", naptan));

            naptan = n2;
            requireStepFreeAccess = true;
            requireSpecialAssistance = false;
            result = locationService.IsAccessibleLocation(naptan, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == true, string.Format("Expected naptan[{0}] with accessible requirement to be accessible", naptan));

            naptan = n2;
            requireStepFreeAccess = true;
            requireSpecialAssistance = true;
            result = locationService.IsAccessibleLocation(naptan, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == false, string.Format("Expected naptan[{0}] with partial accessible requirement to not be accessible", naptan));

            naptan = n2;
            requireStepFreeAccess = false;
            requireSpecialAssistance = true;
            result = locationService.IsAccessibleLocation(naptan, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == false, string.Format("Expected naptan[{0}] with partial accessible requirement to not be accessible", naptan));

            naptan = n3;
            requireStepFreeAccess = true;
            requireSpecialAssistance = false;
            result = locationService.IsAccessibleLocation(naptan, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == false, string.Format("Expected naptan[{0}] with partial accessible requirement to not be accessible", naptan));

            naptan = n3;
            requireStepFreeAccess = true;
            requireSpecialAssistance = true;
            result = locationService.IsAccessibleLocation(naptan, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == false, string.Format("Expected naptan[{0}] with partial accessible requirement to not be accessible", naptan));

            naptan = n3;
            requireStepFreeAccess = false;
            requireSpecialAssistance = true;
            result = locationService.IsAccessibleLocation(naptan, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == true, string.Format("Expected naptan[{0}] with accessible requirement to be accessible", naptan));
        }

        /// <summary>
        /// Tests accessible admin area method
        /// </summary>
        [Test]
        public void TestLocationServiceCacheIsAccessibleAdminArea()
        {
            LSC.LocationService locationService = (LSC.LocationService)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationServiceCache];

            // Expected TEST data in TransientPortal.AccessibleAdminAreas table
            // AdminCode	DistrictCode	StepFree	Assistance
            // 99991        99991            1           1
            // 99991        99992            1           0
            // 99991        99993            0           1
            // 99992        ALL             1           1

            bool result = false;

            string adminAreaCode = null;
            string districtCode = null;
            bool requireStepFreeAccess = true;
            bool requireSpecialAssistance = true;

            result = locationService.IsAccessibleAdminArea(adminAreaCode, districtCode, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == false, string.Format("Expected area[null] district[null] to not be accessible"));

            adminAreaCode = string.Empty;
            districtCode = string.Empty;
            requireStepFreeAccess = true;
            requireSpecialAssistance = true;
            result = locationService.IsAccessibleAdminArea(adminAreaCode, districtCode, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == false, string.Format("Expected area[{0}] district[{1}] to not be accessible", adminAreaCode, districtCode));

            adminAreaCode = "99991";
            districtCode = "99991";
            requireStepFreeAccess = true;
            requireSpecialAssistance = true;
            result = locationService.IsAccessibleAdminArea(adminAreaCode, districtCode, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == true, string.Format("Expected area[{0}] district[{1}] to be accessible", adminAreaCode, districtCode));

            adminAreaCode = "99991";
            districtCode = "99991";
            requireStepFreeAccess = true;
            requireSpecialAssistance = false;
            result = locationService.IsAccessibleAdminArea(adminAreaCode, districtCode, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == true, string.Format("Expected area[{0}] district[{1}] to be accessible", adminAreaCode, districtCode));

            adminAreaCode = "99991";
            districtCode = "99991";
            requireStepFreeAccess = false;
            requireSpecialAssistance = true;
            result = locationService.IsAccessibleAdminArea(adminAreaCode, districtCode, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == true, string.Format("Expected area[{0}] district[{1}] to be accessible", adminAreaCode, districtCode));

            adminAreaCode = "99991";
            districtCode = "99991";
            requireStepFreeAccess = false;
            requireSpecialAssistance = false;
            result = locationService.IsAccessibleAdminArea(adminAreaCode, districtCode, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == false, string.Format("Expected area[{0}] district[{1}] to not be accessible", adminAreaCode, districtCode));

            adminAreaCode = "99991";
            districtCode = "99992";
            requireStepFreeAccess = true;
            requireSpecialAssistance = false;
            result = locationService.IsAccessibleAdminArea(adminAreaCode, districtCode, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == true, string.Format("Expected area[{0}] district[{1}] to be accessible", adminAreaCode, districtCode));

            adminAreaCode = "99991";
            districtCode = "99992";
            requireStepFreeAccess = false;
            requireSpecialAssistance = true;
            result = locationService.IsAccessibleAdminArea(adminAreaCode, districtCode, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == false, string.Format("Expected area[{0}] district[{1}] to not be accessible", adminAreaCode, districtCode));

            adminAreaCode = "99991";
            districtCode = "99993";
            requireStepFreeAccess = true;
            requireSpecialAssistance = false;
            result = locationService.IsAccessibleAdminArea(adminAreaCode, districtCode, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == false, string.Format("Expected area[{0}] district[{1}] to not be accessible", adminAreaCode, districtCode));

            adminAreaCode = "99991";
            districtCode = "99993";
            requireStepFreeAccess = false;
            requireSpecialAssistance = true;
            result = locationService.IsAccessibleAdminArea(adminAreaCode, districtCode, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == true, string.Format("Expected area[{0}] district[{1}] to be accessible", adminAreaCode, districtCode));

            adminAreaCode = "99992";
            districtCode = "12345";
            requireStepFreeAccess = true;
            requireSpecialAssistance = false;
            result = locationService.IsAccessibleAdminArea(adminAreaCode, districtCode, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == true, string.Format("Expected area[{0}] district[{1}] to be accessible", adminAreaCode, districtCode));

            adminAreaCode = "99992";
            districtCode = "12345";
            requireStepFreeAccess = false;
            requireSpecialAssistance = true;
            result = locationService.IsAccessibleAdminArea(adminAreaCode, districtCode, requireStepFreeAccess, requireSpecialAssistance);
            Assert.IsTrue(result == true, string.Format("Expected area[{0}] district[{1}] to be accessible", adminAreaCode, districtCode));
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Resets search and location objects ready for testing
        /// </summary>
        private void ResetSearchAndLocation(ref LocationSearch search, ref TDLocation location,
            string locationInputText, SearchType searchType, bool fuzzySearch, bool allowGroupLocations, bool javascriptEnabled)
        {
            // Create default search and location objects
            search = new LocationSearch();
            location = new TDLocation();

            // Set search values
            search.InputText = locationInputText.Trim();
            search.SearchType = searchType;
            search.FuzzySearch = fuzzySearch;
            search.NoGroup = !allowGroupLocations;

            search.JavascriptEnabled = javascriptEnabled;
        }

        /// <summary>
        /// Helper method to update version no by direct call to database
        /// </summary>
        private void UpdateVersionNo(int versionNum)
        {
            // Database
            SqlHelper sqlHelper = new SqlHelper();
            SqlHelperDatabase database = SqlHelperDatabase.AtosAdditionalDataDB;

            try
            {
                string sproc = "UpdateLocationsVersion";
                Hashtable parameters = new Hashtable();
                parameters.Add("@NotificationTable", "LocationCache");
                parameters.Add("@Version", versionNum);

                sqlHelper.ConnOpen(database);

                sqlHelper.Execute(sproc, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlHelper.ConnIsOpen)
                    sqlHelper.ConnClose();
            }
        }

        #endregion
    }

    /// <summary>
    /// This service initialisation class specifically for including services used for the LocationCache tests
    /// </summary>
    public class TestLocationCacheInitialisation : IServiceInitialisation
    {
        public void Populate(Hashtable serviceCache)
        {

            serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
            serviceCache.Add(ServiceDiscoveryKey.GisQuery, new TestMockGisQuery());
            serviceCache.Add(ServiceDiscoveryKey.GazetteerFactory, new TDGazetteerFactory());
            serviceCache.Add(ServiceDiscoveryKey.Cache, new TestMockCache());

            // Enable Test ChangeNotification
            serviceCache.Add(ServiceDiscoveryKey.DataChangeNotification, new TestMockDataChangeNotification());

            // Enable the Location cache
            serviceCache.Add(ServiceDiscoveryKey.LocationServiceCache, new LocationServiceFactory());

            // Create TD Logging service.
            ArrayList errors = new ArrayList();
            IEventPublisher[] customPublishers = new IEventPublisher[0];
            try
            {
                Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
            }
            catch (TDException tdException)
            {
                throw tdException;
            }

        }
    }
}
