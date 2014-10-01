// *********************************************** 
// NAME             : LocationServiceTest.cs 
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 02 Apr 2011
// DESCRIPTION  	: LocationServiceTest test class
// ************************************************
// 
                
using TDP.Common.LocationService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TDP.Common.ServiceDiscovery;
using LS = TDP.Common.LocationService;
using TDP.Common;

namespace TDP.TestProject.Common.LocationService
{
    
    
    /// <summary>
    ///This is a test class for LocationServiceTest and is intended
    ///to contain all LocationServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LocationServiceTest
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

            MockLocationServiceHelper.PopulateLocationLists();
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
        }
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
        ///A test for LocationService Constructor
        ///</summary>
        [TestMethod()]
        public void LocationServiceConstructorTest()
        {
            LS.LocationService target = new LS.LocationService();

            Assert.IsNotNull(target, "Expected LocationService object to not be null");
        }

        /// <summary>
        ///A test for GetAlternateLocations
        ///</summary>
        [TestMethod()]
        public void GetAlternateLocationsTest()
        {
            LocationService_Accessor target = new LocationService_Accessor();

            string searchString = "Bee";
            
            List<TDPLocation> actual = target.GetAlternateLocations(searchString);

            Assert.IsNotNull(actual, "Expected GetAlternateLocations to return an list and not be null");
            Assert.IsTrue(actual.Count > 0, "Expected GetAlternateLocations to return list containing locations");
        }

        /// <summary>
        ///A test for GetGNATLocations
        ///</summary>
        [TestMethod()]
        public void GetGNATLocationsTest()
        {
            LS.LocationService target = new LS.LocationService();
            
            List<TDPGNATLocation> actual = target.GetGNATLocations();

            Assert.IsNotNull(actual, "Expected GetGNATLocations to return an list and not be null");
            Assert.IsTrue(actual.Count > 0, "Expected GetGNATLocations to return list containing GNAT locations");
        }

        /// <summary>
        ///A test for GetLocation
        ///</summary>
        [TestMethod()]
        public void GetTDPLocationTest()
        {
            LocationService_Accessor target = new LocationService_Accessor();

            string searchString = string.Empty;
            TDPLocationType locType = TDPLocationType.Unknown;
            TDPLocation actual;

            // Venue test
            searchString = "8100STA";
            locType = TDPLocationType.Venue;
            actual = target.GetTDPLocation(searchString, locType);

            Assert.IsNotNull(actual, "Expected GetLocation to return an location and not be null");
            Assert.IsTrue(actual.ID == searchString, 
                string.Format("Expected search for TDPLocation with type[{0}] and string[{1}] to return location with ID[{2}] but instead is [{3}]", locType, searchString, searchString, actual.ID));

            // Locality test
            searchString = "E0000326";
            locType = TDPLocationType.Locality;
            actual = target.GetTDPLocation(searchString, locType);

            Assert.IsNotNull(actual, "Expected GetLocation to return an location and not be null");
            Assert.IsTrue(actual.ID == searchString,
                string.Format("Expected search for TDPLocation with type[{0}] and string[{1}] to return location with ID[{2}] but instead is [{3}]", locType, searchString, searchString, actual.ID));

            // Postcode test
            searchString = "NG9 1AL";
            locType = TDPLocationType.Postcode;
            actual = target.GetTDPLocation(searchString, locType);

            Assert.IsNotNull(actual, "Expected GetLocation to return an location and not be null");
            Assert.IsTrue(actual.ID == searchString.Replace(" ", string.Empty),
                string.Format("Expected search for TDPLocation with type[{0}] and string[{1}] to return location with ID[{2}] but instead is [{3}]", locType, searchString, searchString, actual.ID));


            // Station test
            searchString = "9100DRBY";
            locType = TDPLocationType.Station;
            actual = target.GetTDPLocation(searchString, locType);

            Assert.IsNotNull(actual, "Expected GetLocation to return an location and not be null");
            Assert.IsTrue(actual.Naptan[0] == searchString,
                string.Format("Expected search for TDPLocation with type[{0}] and string[{1}] to return location with Naptan[{2}] but instead is [{3}]", locType, searchString, searchString, actual.Naptan[0]));

            // Station group test
            searchString = "G1";
            locType = TDPLocationType.StationGroup;
            actual = target.GetTDPLocation(searchString, locType);

            Assert.IsNotNull(actual, "Expected GetLocation to return an location and not be null");
            Assert.IsTrue(actual.ID == searchString,
                string.Format("Expected search for TDPLocation with type[{0}] and string[{1}] to return location with ID[{2}] but instead is [{3}]", locType, searchString, searchString, actual.ID));

            // Unknown test
            searchString = "Chelmsford Rail Station";
            locType = TDPLocationType.Unknown;
            actual = target.GetTDPLocation(searchString, locType);

            Assert.IsNotNull(actual, "Expected GetLocation to return an location and not be null");
            Assert.IsTrue(actual.DisplayName.ToUpper() == searchString.ToUpper(),
                string.Format("Expected search for TDPLocation with type[{0}] and string[{1}] to return location with DisplayName[{2}] but instead is [{3}]", locType, searchString, searchString, actual.DisplayName));
        }

        /// <summary>
        ///A test for GetTDPVenueLocations
        ///</summary>
        [TestMethod()]
        public void GetTDPVenueLocationsTest()
        {
            LS.LocationService target = new LS.LocationService();
            
            List<TDPLocation> actual = target.GetTDPVenueLocations();

            Assert.IsNotNull(actual, "Expected GetTDPVenueLocations to return an list and not be null");
            Assert.IsTrue(actual.Count > 0, "Expected GetTDPVenueLocations to return list containing venue locations");
        }

        /// <summary>
        ///A test for GetTDPVenueAccessData
        ///</summary>
        [TestMethod()]
        public void GetTDPVenueAccessDataTest()
        {
            LS.LocationService target = new LS.LocationService();
            List<string> venueNaPTANs = new List<string>() {"8100MIL"};
            DateTime datetime = new DateTime(2012, 7, 27);
            List<TDPVenueAccess> actual = target.GetTDPVenueAccessData(venueNaPTANs, datetime);
            Assert.IsTrue(actual.Count > 0, "Expected venue access to be returned, update test to use latest data");

            // Should find null car parks
            venueNaPTANs = new List<string>() { "8100XXX" };
            actual = target.GetTDPVenueAccessData(venueNaPTANs, datetime);
            Assert.IsNull(actual, "Expected null venue access to be found");
        }

        /// <summary>
        ///A test for GetTDPVenueBlueBadgeCarParks
        ///</summary>
        [TestMethod()]
        public void GetTDPVenueBlueBadgeCarParksTest1()
        {
            LS.LocationService target = new LS.LocationService();
            List<string> venueNaPTANs = new List<string>() { "8100OPK" };
            DateTime outwardDate = new DateTime(2012, 7, 31, 10, 0, 0); 
            DateTime returnDate = new DateTime(2012, 7, 31, 16, 0, 0);
            List<TDPVenueCarPark> actual = target.GetTDPVenueBlueBadgeCarParks(venueNaPTANs, outwardDate, returnDate);
            Assert.IsNotNull(actual, "Expected car park containing blue badge spaced to be returned");
            Assert.IsTrue(actual.Count > 0, "Expected car park containing blue badge spaced to be returned");
            Assert.IsTrue(actual[0].BlueBadgeSpaces > 0, "Expected car park containing blue badge spaced to be returned");
        }

        /// <summary>
        ///A test for GetTDPVenueCarPark
        ///</summary>
        [TestMethod()]
        public void GetTDPVenueCarParkTest()
        {
            LS.LocationService target = new LS.LocationService();
            string carParkId = string.Empty;
            TDPVenueCarPark actual;
            actual = target.GetTDPVenueCarPark(carParkId);
            Assert.IsNull(actual, "Expected no carpark to be returned");

            carParkId = "OPK_EBB";
            actual = target.GetTDPVenueCarPark(carParkId);
            Assert.IsNotNull(actual, "Expected a carpark to be returned");
        }

        /// <summary>
        ///A test for GetTDPVenueCarParks
        ///</summary>
        [TestMethod()]
        public void GetTDPVenueCarParksTest()
        {
            LS.LocationService target = new LS.LocationService();
            List<string> venueNaPTANs = new List<string>(){"8100OPK"};
            DateTime outwardDate = new DateTime(2012, 7, 31);
            DateTime returnDate = new DateTime(2012, 7, 31);
            List<TDPVenueCarPark> actual;
            actual = target.GetTDPVenueCarParks(venueNaPTANs, outwardDate, returnDate);
            Assert.IsTrue(actual.Count > 0, "Expected car parks to be returned");

            // Should find null car parks
            venueNaPTANs = new List<string>() { "8100XXX" };
            actual = target.GetTDPVenueCarParks(venueNaPTANs, outwardDate, returnDate);
            Assert.IsNull(actual, "Expected null car parks to be found");
        }


        /// <summary>
        ///A test for GetTDPVenueCyclePark
        ///</summary>
        [TestMethod()]
        public void GetTDPVenueCycleParkTest()
        {
            LS.LocationService target = new LS.LocationService();
            string cycleParkId = string.Empty; 
            TDPVenueCyclePark actual = target.GetTDPVenueCyclePark(cycleParkId);
            Assert.IsNull(actual, "Expected no cycle park to be returned");

            cycleParkId = "WEACY01";
            actual = target.GetTDPVenueCyclePark(cycleParkId);
            Assert.IsNotNull(actual, "Expected cycle park to be returned");
        }

        /// <summary>
        ///A test for GetTDPVenueCycleParks
        ///</summary>
        [TestMethod()]
        public void GetTDPVenueCycleParksTest()
        {
            LS.LocationService target = new LS.LocationService();
            List<string> venueNaPTANs = new List<string>() {"8100GRP", "8100OPK"};
            DateTime outwardDate = new DateTime(2012, 8, 1, 10, 0, 0);
            DateTime returnDate = new DateTime(2012, 8, 1, 17, 0, 0);
            List<TDPVenueCyclePark> actual;
            actual = target.GetTDPVenueCycleParks(venueNaPTANs, outwardDate, returnDate);
            Assert.IsTrue(actual.Count > 0, "Expected cycle parks to be returned");

            // Should find null cycle parks
            venueNaPTANs = new List<string>() { "8100XXX" };
            actual = target.GetTDPVenueCycleParks(venueNaPTANs, outwardDate, returnDate);
            Assert.IsNull(actual, "Expected null cycle parks to be found");

            // Overnight availablity check
            venueNaPTANs = new List<string>() { "8100OPK" };
            outwardDate = new DateTime(2012, 8, 1, 0, 5, 0);
            returnDate = new DateTime(2012, 8, 1, 0, 5, 0);
            actual = target.GetTDPVenueCycleParks(venueNaPTANs, outwardDate, returnDate);
            Assert.IsTrue(actual.Count > 0, "Expected cycle park which closes after midnight to be returned, update test to use latest data");
        }
        
        /// <summary>
        ///A test for GetTDPVenueGate
        ///</summary>
        [TestMethod()]
        public void GetTDPVenueGateTest()
        {
            LS.LocationService target = new LS.LocationService();
            string venueGateNaPTAN = "8100HYDg0"; 
            TDPVenueGate actual = target.GetTDPVenueGate(venueGateNaPTAN);
            Assert.IsNotNull(actual, "Expected a venue gate to be returned");

            // Null test
            venueGateNaPTAN = "8100XXX";
            actual = target.GetTDPVenueGate(venueGateNaPTAN);
            Assert.IsNull(actual, "Expected a null venue gate");
        }

        /// <summary>
        ///A test for GetTDPVenueGateCheckConstraints
        ///</summary>
        [TestMethod()]
        public void GetTDPVenueGateCheckConstraintsTest()
        {
            LS.LocationService target = new LS.LocationService();
            string venueGateNaPTAN = "8100HYDg0";
            TDPVenueGate venueGate = target.GetTDPVenueGate(venueGateNaPTAN);
            bool isVenueEntry = true;
            TDPVenueGateCheckConstraint actual = target.GetTDPVenueGateCheckConstraints(venueGate, isVenueEntry);
            Assert.IsNotNull(actual, "Expected a check constraint to be returned");

            // Null test
            List<string> venueGateNaPTANs = new List<string>();
            venueGateNaPTANs.Add("8100XXX");
            List<TDPVenueGateCheckConstraint> actual2 = target.GetTDPVenueGateCheckConstraints(venueGateNaPTANs);
            Assert.IsNull(actual2, "Expected null check constraints to be returned");
        }
        
        /// <summary>
        ///A test for GetTDPVenueGateNavigationPaths
        ///</summary>
        [TestMethod()]
        public void GetTDPVenueGateNavigationPathsTest1()
        {
            // Venue test
            LocationService_Accessor target = new LocationService_Accessor();
            string searchString = "8100HYD";
            TDPLocationType locType = TDPLocationType.Venue;
            TDPLocation venue = target.GetTDPLocation(searchString, locType); 
            string venueGateNaPTAN = "8100HYDg0";
            TDPVenueGate venueGate = target.GetTDPVenueGate(venueGateNaPTAN);
            bool isToVenue = true;
            TDPVenueGateNavigationPath actual = target.GetTDPVenueGateNavigationPaths(venue, venueGate, isToVenue);
            Assert.IsNotNull(actual, "Expected a navigation path to be returned");

            isToVenue = false;
            actual = target.GetTDPVenueGateNavigationPaths(venue, venueGate, isToVenue);
            Assert.IsNotNull(actual, "Expected a navigation path to be returned");

            // Null test
            actual = target.GetTDPVenueGateNavigationPaths(venue, null, isToVenue);
            Assert.IsNull(actual, "Expected a navigation path not to be returned");

            // Null test
            List<string> venueGateNaPTANs = new List<string>();
            venueGateNaPTANs.Add("8100XXX");
            List<TDPVenueGateNavigationPath> actual2 = target.GetTDPVenueGateNavigationPaths(venueGateNaPTANs);
            Assert.IsNull(actual2, "Expected a navigation path not to be returned");
        }

        /// <summary>
        ///A test for GetTDPVenuePierNavigationPaths
        ///</summary>
        [TestMethod()]
        public void GetTDPVenuePierNavigationPathsTest1()
        {
            LS.LocationService target = new LS.LocationService();
            List<string> venueNaPTANs = new List<string>() {"8100GRP"};
            string venuePierNaPTAN = "9300GNW1";
            bool isToVenue = true; 
            TDPPierVenueNavigationPath actual = target.GetTDPVenuePierNavigationPaths(venueNaPTANs, venuePierNaPTAN, isToVenue);
            Assert.IsNotNull(actual, "Expected a pier navigation path to be returned");

            isToVenue = false; 
            actual = target.GetTDPVenuePierNavigationPaths(venueNaPTANs, venuePierNaPTAN, isToVenue);
            Assert.IsNotNull(actual, "Expected a pier navigation path to be returned");

            venuePierNaPTAN = "9300XXX";
            actual = target.GetTDPVenuePierNavigationPaths(venueNaPTANs, venuePierNaPTAN, isToVenue);
            Assert.IsNull(actual, "Expected a pier navigation path not to be returned");

            // Null test
            List<string> venueGateNaPTANs = new List<string>();
            venueGateNaPTANs.Add("8100XXX");
            List<TDPPierVenueNavigationPath> actual2 = target.GetTDPVenuePierNavigationPaths(venueGateNaPTANs);
            Assert.IsNull(actual2, "Expected a pier navigation path not to be returned");
        }

        /// <summary>
        ///A test for GetTDPVenueRiverServices
        ///</summary>
        [TestMethod()]
        public void GetTDPVenueRiverServicesTest()
        {
            LS.LocationService target = new LS.LocationService(); 
            List<string> venueNaPTANs = new List<string>(){"8100GRP"};
            List<TDPVenueRiverService> actual = target.GetTDPVenueRiverServices(venueNaPTANs);
            Assert.IsTrue(actual.Count > 0, "Expected many river services to be returned");

            // Null test
            venueNaPTANs = new List<string>() { "8100XXX" };
            actual = target.GetTDPVenueRiverServices(venueNaPTANs);
            Assert.IsNull(actual, "Expected null river services to be returned");
        }

        /// <summary>
        ///A test for IsDateValid
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.locationservice.dll")]
        public void IsDateValidTest()
        {
            LocationService_Accessor target = new LocationService_Accessor();

            // Outward only check
            List<DayOfWeek> daysValid = new List<DayOfWeek>() {DayOfWeek.Monday, DayOfWeek.Thursday};
            DateTime fromDate = new DateTime(2012, 7, 7); 
            DateTime toDate = new DateTime(2012, 7, 14);
            DateTime outwardDate = new DateTime(2012, 7, 9);
            DateTime returnDate = new DateTime(2012, 7, 9);
            bool checkOutwardDate = true;
            bool checkReturnDate = false;
            bool expected = true;
            bool actual = target.IsDateValid(daysValid, fromDate, toDate, outwardDate, returnDate, checkOutwardDate, checkReturnDate);
            Assert.AreEqual(expected, actual, "Expected the date to be valid");

            // Return only check
            daysValid = new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Thursday };
            fromDate = new DateTime(2012, 7, 7);
            toDate = new DateTime(2012, 7, 14);
            outwardDate = new DateTime(2012, 7, 9);
            returnDate = new DateTime(2012, 7, 9);
            checkOutwardDate = false;
            checkReturnDate = true;
            expected = true;
            actual = target.IsDateValid(daysValid, fromDate, toDate, outwardDate, returnDate, checkOutwardDate, checkReturnDate);
            Assert.AreEqual(expected, actual, "Expected the date to be valid");

            // Outward and return check
            daysValid = new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Thursday };
            fromDate = new DateTime(2012, 7, 7);
            toDate = new DateTime(2012, 7, 14);
            outwardDate = new DateTime(2012, 7, 9);
            returnDate = new DateTime(2012, 7, 9);
            checkOutwardDate = true;
            checkReturnDate = true;
            expected = true;
            actual = target.IsDateValid(daysValid, fromDate, toDate, outwardDate, returnDate, checkOutwardDate, checkReturnDate);
            Assert.AreEqual(expected, actual, "Expected the date to be valid");

            // Check neither (weird!)
            daysValid = new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Thursday };
            fromDate = new DateTime(2012, 7, 7);
            toDate = new DateTime(2012, 7, 14);
            outwardDate = new DateTime(2012, 7, 9);
            returnDate = new DateTime(2012, 7, 9);
            checkOutwardDate = false;
            checkReturnDate = false;
            expected = true;
            actual = target.IsDateValid(daysValid, fromDate, toDate, outwardDate, returnDate, checkOutwardDate, checkReturnDate);
            Assert.AreEqual(expected, actual, "Expected the date to be valid");
        }

        /// <summary>
        ///A test for IsTimeValid
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.locationservice.dll")]
        public void IsTimeValidTest()
        {
            LocationService_Accessor target = new LocationService_Accessor(); 

            // Check outward only not overnight
            TimeSpan openingTime = new TimeSpan(10, 0, 0);
            TimeSpan closingTime = new TimeSpan(17, 0, 0);
            DateTime outwardDate = new DateTime(2012, 7, 8, 11, 0, 0);
            DateTime returnDate = new DateTime(2012, 7, 8, 16, 0, 0); 
            bool checkOutwardDate = true;
            bool checkReturnDate = false;
            bool overnight = false;
            bool expected = true;
            bool actual = target.IsTimeValid(openingTime, closingTime, outwardDate, returnDate, checkOutwardDate, checkReturnDate, overnight);
            Assert.AreEqual(expected, actual, "Expected time to be valid");

            // Check outward only overnight
            openingTime = new TimeSpan(10, 0, 0);
            closingTime = new TimeSpan(17, 0, 0);
            outwardDate = new DateTime(2012, 7, 8, 11, 0, 0);
            returnDate = new DateTime(2012, 7, 8, 16, 0, 0);
            checkOutwardDate = true;
            checkReturnDate = false;
            overnight = true;
            expected = true;
            actual = target.IsTimeValid(openingTime, closingTime, outwardDate, returnDate, checkOutwardDate, checkReturnDate, overnight);
            Assert.AreEqual(expected, actual, "Expected time to be valid");

            // Check return only not overnight
            openingTime = new TimeSpan(10, 0, 0);
            closingTime = new TimeSpan(17, 0, 0);
            outwardDate = new DateTime(2012, 7, 8, 11, 0, 0);
            returnDate = new DateTime(2012, 7, 8, 16, 0, 0);
            checkOutwardDate = false;
            checkReturnDate = true;
            overnight = false;
            expected = true;
            actual = target.IsTimeValid(openingTime, closingTime, outwardDate, returnDate, checkOutwardDate, checkReturnDate, overnight);
            Assert.AreEqual(expected, actual, "Expected time to be valid");

            // Check return only overnight
            openingTime = new TimeSpan(10, 0, 0);
            closingTime = new TimeSpan(17, 0, 0);
            outwardDate = new DateTime(2012, 7, 8, 11, 0, 0);
            returnDate = new DateTime(2012, 7, 8, 16, 0, 0);
            checkOutwardDate = false;
            checkReturnDate = true;
            overnight = true;
            expected = true;
            actual = target.IsTimeValid(openingTime, closingTime, outwardDate, returnDate, checkOutwardDate, checkReturnDate, overnight);
            Assert.AreEqual(expected, actual, "Expected time to be valid");

            // Check both only not overnight
            openingTime = new TimeSpan(10, 0, 0);
            closingTime = new TimeSpan(17, 0, 0);
            outwardDate = new DateTime(2012, 7, 8, 11, 0, 0);
            returnDate = new DateTime(2012, 7, 8, 16, 0, 0);
            checkOutwardDate = true;
            checkReturnDate = true;
            overnight = false;
            expected = true;
            actual = target.IsTimeValid(openingTime, closingTime, outwardDate, returnDate, checkOutwardDate, checkReturnDate, overnight);
            Assert.AreEqual(expected, actual, "Expected time to be valid");

            // Check both overnight
            openingTime = new TimeSpan(10, 0, 0);
            closingTime = new TimeSpan(17, 0, 0);
            outwardDate = new DateTime(2012, 7, 8, 11, 0, 0);
            returnDate = new DateTime(2012, 7, 8, 16, 0, 0);
            checkOutwardDate = true;
            checkReturnDate = true;
            overnight = true;
            expected = true;
            actual = target.IsTimeValid(openingTime, closingTime, outwardDate, returnDate, checkOutwardDate, checkReturnDate, overnight);
            Assert.AreEqual(expected, actual, "Expected time to be valid");

            // Check neither
            openingTime = new TimeSpan(10, 0, 0);
            closingTime = new TimeSpan(17, 0, 0);
            outwardDate = new DateTime(2012, 7, 8, 11, 0, 0);
            returnDate = new DateTime(2012, 7, 8, 16, 0, 0);
            checkOutwardDate = false;
            checkReturnDate = false;
            overnight = false;
            expected = true;
            actual = target.IsTimeValid(openingTime, closingTime, outwardDate, returnDate, checkOutwardDate, checkReturnDate, overnight);
            Assert.AreEqual(expected, actual, "Expected time to be valid");

            // Check both at overnight threshold
            openingTime = new TimeSpan(10, 0, 0);
            closingTime = new TimeSpan(0, 30, 0);
            outwardDate = new DateTime(2012, 7, 8, 0, 5, 0);
            returnDate = new DateTime(2012, 7, 8, 0, 5, 0);
            checkOutwardDate = true;
            checkReturnDate = true;
            overnight = true;
            expected = true;
            actual = target.IsTimeValid(openingTime, closingTime, outwardDate, returnDate, checkOutwardDate, checkReturnDate, overnight);
            Assert.AreEqual(expected, actual, "Expected time to be valid");

            // Check both invalid
            openingTime = new TimeSpan(10, 0, 0);
            closingTime = new TimeSpan(17, 0, 0);
            outwardDate = new DateTime(2012, 7, 8, 4, 0, 0);
            returnDate = new DateTime(2012, 7, 8, 4, 0, 0);
            checkOutwardDate = true;
            checkReturnDate = true;
            overnight = true;
            expected = false;
            actual = target.IsTimeValid(openingTime, closingTime, outwardDate, returnDate, checkOutwardDate, checkReturnDate, overnight);
            Assert.AreEqual(expected, actual, "Expected time to be invalid");
        }

        /// <summary>
        ///A test for IsGNAT
        ///</summary>
        [TestMethod()]
        public void IsGNATTest()
        {            
            LS.LocationService target = new LS.LocationService();
            string naptan = "900010171";
            bool stepFree = true; 
            bool assistanceRequired = true; 
            bool expected = true;
            bool actual = target.IsGNAT(naptan, stepFree, assistanceRequired);
            Assert.AreEqual(expected, actual, "Expected venue to be have wheel chair access and assistance");
        }

        /// <summary>
        /// A test for IsGNATAdminArea
        /// </summary>
        [TestMethod()]
        public void IsGNATAdminAreaTest()
        {
            LS.LocationService target = new LS.LocationService();

            int adminArea = 82; 
            int districtCode = 282;
            bool stepFree = true;
            bool assistanceRequired = true;
            bool expected = true;
            bool actual = target.IsGNATAdminArea(adminArea, districtCode, stepFree, assistanceRequired);
            // Check database for existing record if test fails
            Assert.AreEqual(expected, actual, "Expected admin area to have wheel chair access and assistance");

            adminArea = 9999;
            expected = false;
            actual = target.IsGNATAdminArea(adminArea, districtCode, stepFree, assistanceRequired);
            // Check database for existing record if test fails
            Assert.AreEqual(expected, actual, "Expected admin area not to be found and therefore not have wheel chair access and assistance");
        }

        /// <summary>
        ///A test for LoadData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.locationservice.dll")]
        public void LoadDataTest()
        {
            LocationService_Accessor target = new LocationService_Accessor();
            target.LoadData();

            Assert.IsTrue(TDPVenueLocationCache_Accessor.GetVenuesList().Count > 0, "Expected venues to be populated");
            Assert.IsTrue(TDPGNATLocationCache_Accessor.GetGNATList().Count > 0, "Expected GNATs to be populated");
            Assert.IsNotNull(TDPLocationCache_Accessor.GetNaptanLocation("9100LESTER"), "Expected locations to be populated");
            Assert.IsNotNull(TDPLocationCache_Accessor.GetPostcodeLocation("AB101AL"), "Expected postcodes to be populated");
        }

    }
}
