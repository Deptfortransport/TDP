// *********************************************** 
// NAME                 : TestDropDownLocationImporter.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 14/06/2010 
// DESCRIPTION  	    : Unit test class containing unit tests for DropDownLocationImporter class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestDropDownLocationImporter.cs-arc  $ 
//
//   Rev 1.0   Jun 16 2010 10:51:02   mmodi
//Initial revision.
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.IO;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService.DropDownLocationProvider;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.LocationService
{
    /// <summary>
    /// Unit test class containing unit tests for DropDownLocationImporter class
    /// </summary>
    [TestFixture]
    public class TestDropDownLocationImporter
    {
        #region Private members

        // Test Data
        private string TEST_DATA = Directory.GetCurrentDirectory() + "\\DropDownGaz\\DropDownGazData.xml";
        private string SETUP_SCRIPT = Directory.GetCurrentDirectory() + "\\DropDownGaz\\DropDownGazSetup.sql";
        private string CLEARUP_SCRIPT = Directory.GetCurrentDirectory() + "\\DropDownGaz\\DropDownGazCleanUp.sql";
        private const string connectionString = "Server=.;Initial Catalog=AtosAdditionalData;Trusted_Connection=true";
        private TestDataManager tm;

        // Import variables
        private string IMPORT_DATA_DIRECTORY = string.Empty;
        private string IMPORT_DATA_FILENAME = string.Empty;
        private string IMPORT_DATA_FILENAME_INVALID = string.Empty;
        private int testReturnCode = 0;
        private int compareCode = -1;

        #endregion

        #region SetUp and TearDown

        /// <summary>
        /// Method called before starting tests
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            TDServiceDiscovery.ResetServiceDiscoveryForTest();
            TDServiceDiscovery.Init(new TestInitialisation());
            
            // Set up the test data to use
            tm = new TestDataManager(TEST_DATA, SETUP_SCRIPT, CLEARUP_SCRIPT, connectionString, SqlHelperDatabase.AtosAdditionalDataDB);
            tm.Setup();
            tm.LoadData(false);
        }

        /// <summary>
        /// Method called after all tests have run
        /// </summary>
        [TestFixtureTearDown]
        public void TearDown()
        {
            // Put database data back to what it was
            tm.ClearData();
        }

        /// <summary>
        /// Initialisation in setup method called before every test method
        /// </summary>
        [SetUp]
        public void Init()
        {
            try
            {
                // Initialise services
                TDServiceDiscovery.Init(new TestInitialization());

                // Getting value from config
                IMPORT_DATA_DIRECTORY = Directory.GetCurrentDirectory() + "\\DropDownGaz";
                IMPORT_DATA_FILENAME = System.Configuration.ConfigurationManager.AppSettings["DropDownAliasImportDataFileName"];
                IMPORT_DATA_FILENAME_INVALID = System.Configuration.ConfigurationManager.AppSettings["DropDownAliasImportDataFileNameInvalid"];
            }
            catch (Exception e)
            {
                Assert.Fail("Error in init: " + e.Message);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public TestDropDownLocationImporter()
        {
        }

        #endregion

        #region Public tests

        #region Import Alias names tests

        /// <summary>
        /// Test handling invalid source csv file name 
        /// </summary>
        [Test]
        public void TestImportCsvFileNotFound()
        {
            try
            {
                DropDownLocationImporter import = new DropDownLocationImporter(Properties.Current["datagateway.sqlimport.dropdownalias.feedname"], "", "", "", IMPORT_DATA_DIRECTORY);
                
                testReturnCode = (int)(import.Run("foo.csv"));
                compareCode = (int)TDExceptionIdentifier.DDGCsvConversionFailed;
                Assert.IsTrue((testReturnCode == compareCode), "No CSV file found ");
            }
            catch (TDException e)
            {
                Assert.AreEqual(TDExceptionIdentifier.DDGImportFailed, e.Identifier,
                    "Expected to receive TDExceptionIdentifier.DDGCsvConversionFailed");
            }
        }

        /// <summary>
        /// Test handling of invalid feed name.
        /// </summary>
        [Test]
        public void TestInvalidFeedName()
        {
            try
            {
                DropDownLocationImporter import = new DropDownLocationImporter("foo", "", "", "", IMPORT_DATA_DIRECTORY);
                
                testReturnCode = (int)(import.Run(IMPORT_DATA_FILENAME));
                compareCode = (int)TDExceptionIdentifier.DGUnexpectedFeedName;
                Assert.IsTrue((testReturnCode == compareCode),
                    "Passed instead of failed as given feed name doesn't exist. ");
            }
            catch (TDException e)
            {
                Assert.AreEqual(TDExceptionIdentifier.DDGImportFailed, e.Identifier,
                    "Expected to receive TDExceptionIdentifier.DGUnexpectedFeedName");
            }
        }

        /// <summary>
        /// Test whether data has push correctly or not.
        /// </summary>
        [Test]
        public void TestDataImport()
        {
            SqlHelper sqlHelper = new SqlHelper();
            SqlHelperDatabase database = SqlHelperDatabase.AtosAdditionalDataDB;
            int count = -1;

            try
            {
                DropDownLocationImporter import = new DropDownLocationImporter(Properties.Current["datagateway.sqlimport.dropdownalias.feedname"], "", "", "", IMPORT_DATA_DIRECTORY); 
                
                testReturnCode = (int)(import.Run(IMPORT_DATA_FILENAME));
                compareCode = 0;

                Assert.AreEqual(compareCode, testReturnCode,
                    "Data Import was not successful ");

                // Check Alias names were added to the table(s)
                string query = "SELECT Count(*) FROM StopAlias";
                
                sqlHelper.ConnOpen(database);
                count = (Int32)sqlHelper.GetScalar(query);

                // Should be at least 5 alias names added from the sample import data file
                Assert.GreaterOrEqual(count, 5,
                    "Expected StopAlias table to have data following successful data import, but no rows were found");

                // Should be at least 5 alias stations transferred to the "live" table
                query = "SELECT Count(*) FROM DropDownStop WHERE IsAlias = 1";
                count = (Int32)sqlHelper.GetScalar(query);

                Assert.GreaterOrEqual(count, 5,
                    "Expected DropDownStop table to have data following successful data import, but no rows were found");
            }
            catch (TDException e)
            {
                string temp = e.Message;
                Assert.AreEqual(TDExceptionIdentifier.DDGImportFailed,
                    "Expected to data import to succeed" + temp);
            }
            finally
            {
                if (sqlHelper.ConnIsOpen)
                    sqlHelper.ConnClose();
            }
        }

        /// <summary>
        /// Test whether data has push correctly or not.
        /// </summary>
        [Test]
        public void TestDataImportInvalidFile()
        {
            try
            {
                DropDownLocationImporter import = new DropDownLocationImporter(Properties.Current["datagateway.sqlimport.dropdownalias.feedname"], "", "", "", IMPORT_DATA_DIRECTORY); 
                
                testReturnCode = (int)(import.Run(IMPORT_DATA_FILENAME_INVALID));
                compareCode = (int)TDExceptionIdentifier.DDGImportFailed;

                Assert.AreEqual(compareCode, testReturnCode,
                    "Data Import with invalid data file was not successful ");
            }
            catch (TDException e)
            {
                string temp = e.Message;
                Assert.AreEqual(TDExceptionIdentifier.DDGImportFailed,
                    "Expected to receive TDExceptionIdentifier.DDGImportFailed" + temp);
            }
        }

        #endregion

        #endregion
    }
}
