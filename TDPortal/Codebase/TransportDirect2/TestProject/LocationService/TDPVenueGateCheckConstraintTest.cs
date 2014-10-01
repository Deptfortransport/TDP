// *********************************************** 
// NAME             : TDPVenueGateCheckConstraintTest.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Jun 2011
// DESCRIPTION  	: Unit tests for TDPVenueGateCheckConstraint
// ************************************************
                
                
using TDP.Common.LocationService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TDP.TestProject.Common.LocationService
{
    
    
    /// <summary>
    ///This is a test class for TDPVenueGateCheckConstraintTest and is intended
    ///to contain all TDPVenueGateCheckConstraintTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPVenueGateCheckConstraintTest
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
        ///A test for TDPVenueGateCheckConstraint 
        ///</summary>
        [TestMethod()]
        public void TDPVenueGateCheckConstraintClassTest()
        {
            TDPVenueGateCheckConstraint target = new TDPVenueGateCheckConstraint();
            target.AverageDelay = new TimeSpan(0, 12, 0);
            target.CheckConstraintID = "TID";
            target.CheckConstraintName = "Test";
            target.Congestion = "not available";
            target.GateNaPTAN = "8100TSTG01";
            target.IsEntry = true;
            target.Process = "Test Process";

            Assert.AreEqual("TID", target.CheckConstraintID);
            Assert.AreEqual("Test", target.CheckConstraintName);
            Assert.AreEqual("not available", target.Congestion);
            Assert.AreEqual("8100TSTG01", target.GateNaPTAN);
            Assert.IsTrue(target.IsEntry);
            Assert.AreEqual("Test Process", target.Process);
            Assert.AreEqual(12, target.AverageDelay.Minutes);
        }

        


    }
}
