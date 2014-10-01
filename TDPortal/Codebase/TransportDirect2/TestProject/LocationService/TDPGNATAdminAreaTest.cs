// *********************************************** 
// NAME             : TDPGNATAdminArea.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 30 Now 2011
// DESCRIPTION  	: Unit tests for TDPGNATAdminArea
// ************************************************

using TDP.Common.LocationService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TDP.TestProject.Common.LocationService
{
    
    
    /// <summary>
    ///This is a test class for TDPGNATAdminAreaTest and is intended
    ///to contain all TDPGNATAdminAreaTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TDPGNATAdminAreaTest
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
        ///A test for TDPGNATAdminArea Constructor
        ///</summary>
        [TestMethod()]
        public void TDPGNATAdminAreaConstructorTest()
        {
            int administrativeAreaCode = 123;
            int districtCode = 321;
            bool stepFreeAccess = true;
            bool assistanceAvailable = true;
            TDPGNATAdminArea target = new TDPGNATAdminArea(administrativeAreaCode, districtCode, stepFreeAccess, assistanceAvailable);
            Assert.AreEqual(administrativeAreaCode, target.AdministrativeAreaCode);
            Assert.AreEqual(districtCode, target.DistrictCode);
            Assert.AreEqual(stepFreeAccess, target.StepFreeAccess);
            Assert.AreEqual(assistanceAvailable, target.AssistanceAvailable);
        }

        /// <summary>
        ///A test for TDPGNATAdminArea Constructor
        ///</summary>
        [TestMethod()]
        public void TDPGNATAdminAreaConstructorTest1()
        {
            TDPGNATAdminArea target = new TDPGNATAdminArea();
            Assert.IsNotNull(target, "Expected TDPGNATAdminArea to be created");
        }
    }
}
