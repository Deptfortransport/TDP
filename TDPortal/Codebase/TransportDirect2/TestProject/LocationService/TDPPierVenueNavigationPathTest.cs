﻿// *********************************************** 
// NAME             : TDPPierVenueNavigationPathTest.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Jun 2011
// DESCRIPTION  	: Unit tests for TDPPierVenueNavigationPath
// ************************************************
                
                
using TDP.Common.LocationService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TDP.TestProject.Common.LocationService
{
    
    
    /// <summary>
    ///This is a test class for TDPPierVenueNavigationPathTest and is intended
    ///to contain all TDPPierVenueNavigationPathTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPPierVenueNavigationPathTest
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
        ///A test for TDPPierVenueNavigationPath
        ///</summary>
        [TestMethod()]
        public void TDPPierVenueNavigationPathClassTest()
        {
            TDPPierVenueNavigationPath target = new TDPPierVenueNavigationPath();
            target.FromNaPTAN = "8100TST1";
            target.ToNaPTAN = "8100TST2";
            target.NavigationID = "8100TST1_8100TST2";
            target.NavigationPathTransfers = new System.Collections.Generic.Dictionary<TDP.Common.Language, string>();
            target.NavigationPathTransfers.Add(TDP.Common.Language.English,"Test Pier 1 to Pier 2");
            target.AddTransferText("Test Pier 1 onto Pier 2", TDP.Common.Language.English);
            target.AddTransferText("Test Pier 1 et Pier 2", TDP.Common.Language.Welsh);
            target.VenueNaPTAN = "8100TST";
            target.DefaultDuration = new TimeSpan(0, 40, 30);
            target.Distance = 40;

            Assert.AreEqual("8100TST1", target.FromNaPTAN);
            Assert.AreEqual("8100TST2", target.ToNaPTAN);
            Assert.AreEqual("8100TST1_8100TST2", target.NavigationID);
            Assert.AreEqual("Test Pier 1 onto Pier 2", target.GetTransferText(TDP.Common.Language.English));
            Assert.AreEqual("Test Pier 1 et Pier 2", target.NavigationPathTransfers[TDP.Common.Language.Welsh]);
            Assert.AreEqual("8100TST", target.VenueNaPTAN);
            Assert.AreEqual(new TimeSpan(0,40,30), target.DefaultDuration);
            Assert.AreEqual(40, target.Distance);
        }

        
    }
}
