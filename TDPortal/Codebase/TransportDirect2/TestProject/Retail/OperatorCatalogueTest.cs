// *********************************************** 
// NAME             : OperatorCatalogueTest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 09 Sep 2013
// DESCRIPTION  	: OperatorCatalogue test
// ************************************************
// 
                
using TDP.UserPortal.Retail;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common;
using TDP.Common.ServiceDiscovery;
using System.Collections.Generic;

namespace TDP.TestProject.Retail
{
    
    
    /// <summary>
    ///This is a test class for OperatorCatalogueTest and is intended
    ///to contain all OperatorCatalogueTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OperatorCatalogueTest
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
        ///A test for OperatorCatalogue Constructor
        ///</summary>
        [TestMethod()]
        public void OperatorCatalogueConstructorTest()
        {
            OperatorCatalogue target = new OperatorCatalogue();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for GetAccessibleOperator
        ///</summary>
        [TestMethod()]
        public void OperatorCatalogueGetAccessibleOperatorTest()
        {
            OperatorCatalogue target = new OperatorCatalogue();
            ServiceOperator expected = null;
            ServiceOperator actual = null;

            // Valid operator - no service number
            expected = new ServiceOperator(TDPModeType.Rail, "EM", string.Empty, "http://");
            actual = target.GetAccessibleOperator("EM", string.Empty, "L", TDPModeType.Rail, DateTime.Now);
            
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Code.Equals(expected.Code));
            Assert.IsTrue(actual.BookingUrl.StartsWith(expected.BookingUrl));

            // Valid operator - service number
            expected = new ServiceOperator(TDPModeType.Rail, "EM", string.Empty, "http://");
            actual = target.GetAccessibleOperator("EM", "12345", "L", TDPModeType.Rail, DateTime.Now);

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Code.Equals(expected.Code));
            Assert.IsTrue(actual.BookingUrl.StartsWith(expected.BookingUrl));

            // Invalid operator
            actual = target.GetAccessibleOperator("XX", string.Empty, "L", TDPModeType.Telecabine, DateTime.Now);

            Assert.IsNull(actual);

            // Expired date
            actual = target.GetAccessibleOperator("EM", string.Empty, "L", TDPModeType.Rail, DateTime.MinValue);

            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for Load
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.userportal.retail.dll")]
        public void OperatorCatalogueLoadTest()
        {
            OperatorCatalogue_Accessor target = new OperatorCatalogue_Accessor();
            target.Load();

            // And try to find an operator
            ServiceOperator expected = new ServiceOperator(TDPModeType.Rail, "EM", string.Empty, "http://");

            ServiceOperator actual = target.GetAccessibleOperator("EM", string.Empty, "L", TDPModeType.Rail, DateTime.Now);

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Code.Equals(expected.Code));
            Assert.IsTrue(actual.BookingUrl.StartsWith(expected.BookingUrl));
        }
    }
}
