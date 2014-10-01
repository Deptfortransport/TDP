﻿// *********************************************** 
// NAME             : TDPGNATLocationTest.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Jun 2011
// DESCRIPTION  	: Unit tests for TDPGNATLocation
// ************************************************
                
                
using TDP.Common.LocationService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TDP.TestProject.Common.LocationService
{
    
    
    /// <summary>
    ///This is a test class for TDPGNATLocationTest and is intended
    ///to contain all TDPGNATLocationTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPGNATLocationTest
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
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
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
        ///A test for TDPGNATLocation Constructor
        ///</summary>
        [TestMethod()]
        public void TDPGNATLocationConstructorTest()
        {
            string locationDisplayName = "Leicester Rail Station";  
            TDPLocationType locationType = TDPLocationType.Station;  
            string identifier = "LECRail";  
            bool stepFree = true;  
            bool assistance = true;
            string operatorCode = "AB";
            string countryCode = "Eng";  
            int adminAreaCode = 71;  
            int districtCode = 23;  
            TDPGNATLocationType gnatStopType = TDPGNATLocationType.Rail;  
            TDPGNATLocation target = new TDPGNATLocation(locationDisplayName, locationType, identifier, stepFree, assistance, operatorCode, countryCode, adminAreaCode, districtCode, gnatStopType);

            Assert.AreEqual(locationDisplayName, target.DisplayName);
            Assert.AreEqual(locationType, target.TypeOfLocation);
            Assert.AreEqual(identifier, target.ID);
            Assert.AreEqual(stepFree, target.StepFreeAccess);
            Assert.AreEqual(assistance, target.AssistanceAvailable);
            Assert.AreEqual(operatorCode, target.OperatorCode);
            Assert.AreEqual(countryCode, target.CountryCode);
            Assert.AreEqual(adminAreaCode, target.AdminAreaCode);
            Assert.AreEqual(districtCode, target.DistrictCode);
            Assert.AreEqual(gnatStopType, target.GNATStopType);

            // Update values and test
            stepFree = false;
            assistance = false;
            operatorCode = "XY";
            countryCode = "Wal";
            gnatStopType = TDPGNATLocationType.Bus;

            target.StepFreeAccess = stepFree;
            target.AssistanceAvailable = assistance;
            target.OperatorCode = operatorCode;
            target.CountryCode = countryCode;
            target.GNATStopType = gnatStopType;

            Assert.AreEqual(stepFree, target.StepFreeAccess);
            Assert.AreEqual(assistance, target.AssistanceAvailable);
            Assert.AreEqual(operatorCode, target.OperatorCode);
            Assert.AreEqual(countryCode, target.CountryCode);
            Assert.AreEqual(gnatStopType, target.GNATStopType);
        }

        /// <summary>
        ///A test for TDPGNATLocation ToString
        ///</summary>
        [TestMethod()]
        public void TDPGNATLocationToStringTest()
        {
            string locationDisplayName = "Leicester Rail Station";
            TDPLocationType locationType = TDPLocationType.Station;
            string identifier = "LECRail";
            bool stepFree = true;
            bool assistance = true;
            string operatorCode = "AB";
            string countryCode = "Eng";
            int adminAreaCode = 71;
            int districtCode = 23;
            TDPGNATLocationType gnatStopType = TDPGNATLocationType.Rail;
            TDPGNATLocation target = new TDPGNATLocation(locationDisplayName, locationType, identifier, stepFree, assistance, operatorCode, countryCode, adminAreaCode, districtCode, gnatStopType);

            string actual = target.ToString(false);
            Assert.IsFalse(string.IsNullOrEmpty(actual), "Expected ToString to return a string of the TDPGNATLocation");

            actual = target.ToString(true);
            Assert.IsFalse(string.IsNullOrEmpty(actual), "Expected ToString to return a string of the TDPGNATLocation");
        }
    }
}
