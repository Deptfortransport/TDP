// *********************************************** 
// NAME             : JSLocationProviderTest.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Jun 2011
// DESCRIPTION  	: Unit tests for JSLocationProvider
// ************************************************
                
                
using TDP.Common.LocationJsGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TDP.Common.ServiceDiscovery;
using TDP.Common.DatabaseInfrastructure;
using System.Configuration;
using TDP.Common.PropertyManager;
using TDP.Common;



namespace TDP.TestProject.LocationJSGenerator
{
    
    
    /// <summary>
    ///This is a test class for JSLocationProviderTest and is intended
    ///to contain all JSLocationProviderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class JSLocationProviderTest
    {


        private TestContext testContextInstance;
        private static TestDataManager testDataManager;

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
        [TestInitialize()]
        public void TestInitialize()
        {
            string test_data = @"LocationJsGenerator\LocationJsGeneratorData.xml";
            string setup_script = @"LocationJsGenerator\LocationJsGeneratorTestSetup.sql";
            string clearup_script = @"LocationJsGenerator\LocationJsGeneratorTestCleanUp.sql";
            string connectionString = @"Server=.\SQLEXPRESS;Initial Catalog=TDPGazetteer;Trusted_Connection=true";
            
            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
            TDPServiceDiscovery.Init(new TestInitialisation());
            testDataManager = new TestDataManager(
                test_data,
                setup_script,
                clearup_script,
                connectionString,
                SqlHelperDatabase.GazetteerDB);
            testDataManager.Setup();

            testDataManager.LoadData(false);

        }

        //
        //Use ClassCleanup to run code after all tests in a class have run
        [TestCleanup()]
        public void TestClassCleanup()
        {
            testDataManager.ClearData();
            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
        }

       
        #endregion


        /// <summary>
        ///A test for GetJsLocationData
        ///</summary>
        [TestMethod()]
        public void GetJsLocationDataTest()
        {
            
            JSLocationProvider target = new JSLocationProvider(); 
            Dictionary<char, List<JSLocation>> actual;
            actual = target.GetJsLocationData(JSGeneratorMode.TDPWeb);
            Assert.AreEqual(25, actual.Count);

            Assert.IsNotNull(actual.ContainsKey('k'));

            // alias is added maked the count as 2
            Assert.AreEqual(2, actual['k'].Count);

            Assert.IsFalse(actual.ContainsKey('x'));

            // Mobile - this will load non-locality locations, current should only be 2 locations found in the call
            actual = target.GetJsLocationData(JSGeneratorMode.TDPMobile);
            Assert.AreEqual(2, actual.Count, "Expected only 2 locations to be found for GetJsLocationData for Mobile, check test data");
            
        }


        /// <summary>
        ///A test for GetJsLocationData when there is no alias file provided
        ///</summary>
        [TestMethod()]
        public void GetJsLocationDataTestNoAliasFile()
        {
            Properties_Accessor properties = new Properties_Accessor(new PrivateObject(Properties.Current));

            string oldValue = properties.propertyDictionary["AliasFile"];

            properties.propertyDictionary["AliasFile"] = string.Empty;

            try
            {
                

                JSLocationProvider target = new JSLocationProvider();

                Dictionary<char, List<JSLocation>> actual;
                actual = target.GetJsLocationData(JSGeneratorMode.TDPWeb);

                Assert.AreEqual(25, actual.Count);

                Assert.IsNotNull(actual.ContainsKey('k'));

                // no alias is added
                Assert.AreEqual(1, actual['k'].Count);

                Assert.IsFalse(actual.ContainsKey('x'));
            }
            finally
            {
                properties.propertyDictionary["AliasFile"] = oldValue;
            }

        }


        /// <summary>
        ///A test for GetJsLocationData when there is wrong alias file provided and exception gets thrown
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(TDPException))]
        public void GetJsLocationDataTestWrongAliasFile()
        {
            Properties_Accessor properties = new Properties_Accessor(new PrivateObject(Properties.Current));

            string oldValue = properties.propertyDictionary["AliasFile"];

            properties.propertyDictionary["AliasFile"] ="Test";

            try
            {
                
                JSLocationProvider target = new JSLocationProvider();
                Dictionary<char, List<JSLocation>> actual;
                actual = target.GetJsLocationData(JSGeneratorMode.TDPWeb);

                
            }
            finally
            {
                properties.propertyDictionary["AliasFile"] = oldValue;
            }

        }
    }
}
