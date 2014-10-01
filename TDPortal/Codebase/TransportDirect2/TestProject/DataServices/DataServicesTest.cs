// *********************************************** 
// NAME             : DataServicesTest.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Jun 2011
// DESCRIPTION  	: Unit tests for DataServices
// ************************************************
                
                
using TDP.Common.DataServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDP.Common.DatabaseInfrastructure;
using System.Collections;
using System.Web.UI.WebControls;
using TDP.Common.ResourceManager;
using System.Data.SqlClient;
using System.Collections.Generic;
using TDP.Common.ServiceDiscovery;
using TDP.Common;
using TDP.Common.PropertyManager;
using DS = TDP.Common.DataServices;

namespace TDP.TestProject.Common.DataServices
{
    
    
    /// <summary>
    ///This is a test class for DataServicesTest and is intended
    ///to contain all DataServicesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DataServicesTest
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
        public static void MyClassInitialize(TestContext testContext)
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
        public static void MyClassCleanup()
        {
            testDataManager.ClearData();
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
        ///A test for FindDatabase
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.dataservices.dll")]
        public void FindDatabaseTest()
        {
            string DBName = "TransientPortalDB"; 
            SqlHelperDatabase expected = SqlHelperDatabase.TransientPortalDB; 
            SqlHelperDatabase actual;
            actual = DataServices_Accessor.FindDatabase(DBName);
            Assert.AreEqual(expected, actual);
           
        }

        /// <summary>
        ///A test for GetList
        ///</summary>
        [TestMethod()]
        public void GetListTest()
        {
            DataServices_Accessor target = new DataServices_Accessor();
            
            // Lock to prevent other tests reloading cache
            lock (DataServices_Accessor.cacheLockObj)
            {
                // Ensure cache is loaded
                target.LoadDataCache();

                ArrayList list = target.GetList(DataServiceType.NewsRegionDrop);

                Assert.IsNotNull(list);
                Assert.IsTrue(list.Count > 0);
            }
        }

        /// <summary>
        ///A test for GetList Illegal
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(TDPException))]
        public void GetListTestIlligalType()
        {
            DataServices_Accessor target = new DataServices_Accessor();

            // Lock to prevent other tests reloading cache
            lock (DataServices_Accessor.cacheLockObj)
            {
                DataServices_Accessor.cache.Clear();
                DataServices_Accessor.cache.Add(DataServiceType.DataServiceTypeEnd, new string[] { "test" });

                DataServiceType item = DataServiceType.DataServiceTypeEnd;
                ArrayList actual;

                actual = target.GetList(item);
            }
        }

        /// <summary>
        ///A test for LoadDataCache
        ///</summary>
        [TestMethod()]
        [DeploymentItem("tdp.common.dataservices.dll")]
        [ExpectedException(typeof(TDPException))]
        public void LoadDataCacheTest()
        {
            DataServices_Accessor target = new DataServices_Accessor();

            // Lock to prevent other tests reloading cache
            lock (DataServices_Accessor.cacheLockObj)
            {
                DataServices_Accessor.cache.Clear();
                target.LoadDataCache();
                Assert.IsTrue(DataServices_Accessor.cache.Keys.Count >= 2);

                // database property is not set - Exception gets thrown
                Properties_Accessor accessor = new Properties_Accessor(new PrivateObject(Properties.Current));
                string dbprop = accessor.propertyDictionary["TDP.UserPortal.DataServices.CountryDrop.db"];
                accessor.propertyDictionary["TDP.UserPortal.DataServices.CountryDrop.db"] = string.Empty;
                DataServices_Accessor.cache.Clear();
                target.LoadDataCache();
                // reset the property back
                accessor.propertyDictionary["TDP.UserPortal.DataServices.CountryDrop.db"] = dbprop;


                // query property is not set - Exception gets thrown
                accessor = new Properties_Accessor(new PrivateObject(Properties.Current));
                string queryProp = accessor.propertyDictionary["TDP.UserPortal.DataServices.NewsViewMode.query"];
                accessor.propertyDictionary["TDP.UserPortal.DataServices.NewsViewMode.query"] = string.Empty;
                DataServices_Accessor.cache.Clear();
                target.LoadDataCache();
                // reset the property back
                accessor.propertyDictionary["TDP.UserPortal.DataServices.NewsViewMode.query"] = queryProp;

                // wrong datatype defined - Exception gets thrown
                accessor = new Properties_Accessor(new PrivateObject(Properties.Current));
                string datatype = accessor.propertyDictionary["TDP.UserPortal.DataServices.NewsRegionDrop.type"];
                accessor.propertyDictionary["TDP.UserPortal.DataServices.NewsRegionDrop.type"] = "6";
                DataServices_Accessor.cache.Clear();
                target.LoadDataCache();
                // reset the property back
                accessor.propertyDictionary["TDP.UserPortal.DataServices.NewsRegionDrop.type"] = datatype;

                DataServices_Accessor.cache.Clear();
            }
        }

        /// <summary>
        ///A test for LoadListControl
        ///</summary>
        ///<remarks>This class assumes that the resource content database has content for the dropdown resource Ids</remarks>
        [TestMethod()]
        public void LoadListControlTest()
        {
            DataServices_Accessor target = new DataServices_Accessor();
            DataServiceType dataSet = DataServiceType.NewsRegionDrop;

            lock (DataServices_Accessor.cacheLockObj)
            {
                // Ensure cache is loaded
                target.LoadDataCache();

                using (DropDownList control = new DropDownList())
                {
                    TDPResourceManager rm = new TDPResourceManager();
                    target.LoadListControl(dataSet, control, rm, Language.English);
                    Assert.IsTrue(control.Items.Count == 12); // Should be 12 regions including All
                    Assert.IsTrue(control.Items[0].Text.Contains("All"));
                    Assert.IsTrue(control.Items[1].Text.Contains("London"));
                    Assert.IsTrue(control.Items[2].Text.Contains("East"));
                    Assert.AreEqual(true, control.Items[1].Selected);
                }

                using (RadioButtonList control = new RadioButtonList())
                {
                    TDPResourceManager rm = new TDPResourceManager();
                    target.LoadListControl(dataSet, control, rm, Language.English);
                    Assert.IsTrue(control.Items.Count == 12); // Should be 12 regions including All
                    Assert.IsTrue(control.Items[0].Text.Contains("All"));
                    Assert.IsTrue(control.Items[1].Text.Contains("London"));
                    Assert.IsTrue(control.Items[2].Text.Contains("East"));
                    Assert.AreEqual(true, control.Items[1].Selected);
                }

                using (ListBox control = new ListBox())
                {
                    TDPResourceManager rm = new TDPResourceManager();
                    target.LoadListControl(dataSet, control, rm, Language.English);
                    Assert.IsTrue(control.Items.Count == 12); // Should be 12 regions including All
                    Assert.IsTrue(control.Items[0].Text.Contains("All"));
                    Assert.IsTrue(control.Items[1].Text.Contains("London"));
                    Assert.IsTrue(control.Items[2].Text.Contains("East"));
                    Assert.AreEqual(true, control.Items[1].Selected);
                }
            }
        }

        /// <summary>
        ///A test for LoadListControl Illegal
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(TDPException))]
        public void LoadListControlTestIlligalType()
        {
            DataServices_Accessor target = new DataServices_Accessor();

            // Lock to prevent other tests reloading cache
            lock (DataServices_Accessor.cacheLockObj)
            {
                DataServices_Accessor.cache.Clear();

                DataServices_Accessor.cache.Add(DataServiceType.DataServiceTypeEnd, new string[] { "test" });

                using (DropDownList control = new DropDownList())
                {
                    TDPResourceManager rm = new TDPResourceManager();
                    target.LoadListControl(DataServiceType.DataServiceTypeEnd, control, rm, Language.English);
                }

                DataServices_Accessor.cache.Clear();
            }
        }

        /// <summary>
        ///A test for GetText
        ///</summary>
        [TestMethod()]
        public void GetTextTest()
        {
            DataServices_Accessor target = new DataServices_Accessor();
            string actual = string.Empty;
            TDPResourceManager rm = new TDPResourceManager();

            lock (DataServices_Accessor.cacheLockObj)
            {
                // Ensure cache is loaded
                target.LoadDataCache();

                // Valid value
                actual = target.GetText(DataServiceType.NewsRegionDrop, "London", rm, Language.English);

                Assert.IsTrue(!string.IsNullOrEmpty(actual));

                // InValid value
                actual = target.GetText(DataServiceType.NewsRegionDrop, "DOESNOTEXIST", rm, Language.English);

                Assert.IsTrue(string.IsNullOrEmpty(actual));
            }
        }
    }
}
