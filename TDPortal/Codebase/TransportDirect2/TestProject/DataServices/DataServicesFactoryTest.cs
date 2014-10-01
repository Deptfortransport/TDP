﻿// *********************************************** 
// NAME             : DataServicesFactoryTest.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Jun 2011
// DESCRIPTION  	: Unit tests for DataServicesFactory
// ************************************************
                
                
using TDP.Common.DataServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.ServiceDiscovery;
using TDP.Common.DatabaseInfrastructure;

namespace TDP.TestProject.Common.DataServices
{
    
    
    /// <summary>
    ///This is a test class for DataServicesFactoryTest and is intended
    ///to contain all DataServicesFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DataServicesFactoryTest
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
        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            string test_data = @"DataServices\DataServicesData.xml";
            string setup_script = @"DataServices\DataServicesTestSetup.sql";
            string clearup_script = @"DataServices\DataServicesTestCleanUp.sql";
            string connectionString = @"Server=.\SQLEXPRESS;Initial Catalog=PermanentPortal;Trusted_Connection=true";

            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
            TDPServiceDiscovery.Init(new TestInitialisationPropertiesLogging());
            testDataManager = new TestDataManager(
                test_data,
                setup_script,
                clearup_script,
                connectionString,
                SqlHelperDatabase.DefaultDB);
            testDataManager.Setup();

            testDataManager.LoadData(false);

        }

        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void TestClassCleanup()
        {
            testDataManager.ClearData();
            TDPServiceDiscovery.ResetServiceDiscoveryForTest();
        }


        #endregion



        /// <summary>
        ///A test for Get
        ///</summary>
        [TestMethod()]
        public void GetTest()
        {
            DataServicesFactory target = new DataServicesFactory(); 
            object actual;
            actual = target.Get();
            Assert.IsInstanceOfType(actual, typeof(IDataServices));
           
        }
    }
}
