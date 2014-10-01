using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.CJPConfigDataUtility
{
    [TestFixture]
    public class TestCJPConfigDataUtility
    {
        #region Private members

        // Test Data
        private string TEST_DATA = Directory.GetCurrentDirectory() + "\\Test\\CJPConfigData.xml";
        private string SETUP_SCRIPT = Directory.GetCurrentDirectory() + "\\Test\\CJPConfigDataSetup.sql";
        private string CLEARUP_SCRIPT = Directory.GetCurrentDirectory() + "\\Test\\CJPConfigDataCleanUp.sql";
        
        private const string connectionString = "Server=.;Initial Catalog=TransientPortal;Trusted_Connection=true";
        private TestDataManager tm;
        private CJPConfigDataHelper cjpConfigDataHelper;
        #endregion

        #region SetUp and TearDown
        /// <summary>
        /// Sets up the required resources for unit tests
        /// Prepares database for unit testing
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            // Set up the test data to use
            tm = new TestDataManager(TEST_DATA, SETUP_SCRIPT, CLEARUP_SCRIPT, connectionString, SqlHelperDatabase.AtosAdditionalDataDB);
            tm.Setup();

            TDServiceDiscovery.ResetServiceDiscoveryForTest();
            TDServiceDiscovery.Init(new TestInitialization());

            cjpConfigDataHelper = new CJPConfigDataHelper();
        }

        /// <summary>
        /// Releases the Resources, Sets the database back to its original state.
        /// </summary>
        [TestFixtureTearDown]
        public void TearDown()
        {
            cjpConfigDataHelper = null;
            // Put database data back to what it was
            tm.ClearData();
            TDServiceDiscovery.ResetServiceDiscoveryForTest();
        }
        #endregion

        #region Property Provider helper methods
        /// <summary>
        /// Tests both ImportConfigData and ExportConfigFile methods to import and export 
        /// CJP config properties data 
        /// </summary>
        [Test]
        public void TestCJPConfigFileImportExport()
        {
            int expectedCount = 6;

            cjpConfigDataHelper.ImportConfigData();

            int actualCount = GetCJPConfigPropertyCount();

            Assert.AreEqual(expectedCount, actualCount);

            cjpConfigDataHelper.ExportConfigFile();

            Assert.IsTrue(TargetConfigFileExists());

            DataSet inputFileSet = ReadCJPConfigFile(false);
            DataSet exportFileSet = ReadCJPConfigFile(true);

            Assert.IsTrue(inputFileSet.Tables.Count == 1);
            Assert.IsTrue(exportFileSet.Tables.Count == 1);
            Assert.AreEqual(inputFileSet.Tables.Count, exportFileSet.Tables.Count);
            Assert.AreEqual(inputFileSet.Tables[0].Rows.Count, exportFileSet.Tables[0].Rows.Count);

            foreach (DataRow inputRow in inputFileSet.Tables[0].Rows)
            {
                bool found = false;
                foreach (DataRow exportRow in exportFileSet.Tables[0].Rows)
                {
                    // pValue are text in xml so it comes as 'property_Text' in dataset
                    if (exportRow["name"].ToString().Trim() == inputRow["name"].ToString().Trim()
                        && exportRow["property_Text"].ToString().Trim() == inputRow["property_Text"].ToString().Trim())
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    Assert.Fail("Not all values are imported or failed to export");
            }
        }

        #endregion

       

        #region Helper Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetCJPConfigPropertyCount()
        {
            // Database
            SqlHelper sqlHelper = new SqlHelper();
            SqlHelperDatabase database = SqlHelperDatabase.TransientPortalDB;

            int propCount = -1;

            try
            {

                string query = "SELECT COUNT(*) FROM CJPConfigData";
                
                sqlHelper.ConnOpen(database);

                propCount = (Int32)sqlHelper.GetScalar(query);

            }
            catch (Exception ex)
            {
                propCount = -1;
                throw ex;
            }
            finally
            {
                if (sqlHelper.ConnIsOpen)
                    sqlHelper.ConnClose();
            }

            return propCount;
        }

       

        private DataSet ReadCJPConfigFile(bool readExport)
        {
            string configFile = Properties.Current["CJPConfigData.ConfigFile.Server1"];

            if (readExport)
                configFile = Properties.Current["CJPConfigData.ConfigFilePath"];

            DataSet ds = new DataSet();
            ds.ReadXml(configFile);

            return ds;
        }

        /// <summary>
        /// Check if the script file generated by dropdownlocation provider exists
        /// </summary>
        /// <param name="scriptName"></param>
        /// <returns>true if the file with the script name exists in the tempScripts folder</returns>
        private bool TargetConfigFileExists()
        {
            string targetConfigFile = Properties.Current["CJPConfigData.ConfigFilePath"];
            
            // Log error if no script exists
            FileInfo configFileInfo = new FileInfo(targetConfigFile);

            return configFileInfo.Exists;
        }

       

       
        #endregion
    }

    #region Test Initialization class
    /// <summary>
    /// Class to initialise services that are used by the tests.
    /// </summary>
    public class TestInitialization : IServiceInitialisation
    {

        public void Populate(Hashtable serviceCache)
        {
            // Enable PropertyService
            serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

            // Enable logging service.
            IEventPublisher[] customPublishers = new IEventPublisher[0];

            // Create TD Logging service.
            ArrayList errors = new ArrayList();
            try
            {
                Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
                Trace.Listeners.Remove("System.Diagnostics.DefaultTraceListener");
            }
            catch (TDException tdException)
            {
                // Create error message.
                StringBuilder message = new StringBuilder(100);
                foreach (string error in errors)
                    message.Append(error + ",");

                throw new TDException(String.Format("Failed to initialise the TD Trace Listener class. Exception ID: [{0}]. Reason: [{1}].", tdException.Identifier.ToString("D"), message.ToString()), false, TDExceptionIdentifier.CCDTDTraceInitFailed);
            }
        }
    }

    #endregion 
}
