// *********************************************** 
// NAME                 : TestDropDownLocationProviderService.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 14/06/2010 
// DESCRIPTION  	    : Unit test class containing unit tests for DropDownLocationProviderService class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestDropDownLocationProviderService.cs-arc  $ 
//
//   Rev 1.3   Jul 05 2010 11:04:10   mmodi
//Corrected test
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.2   Jun 15 2010 15:13:12   apatel
//Updated
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.1   Jun 15 2010 09:30:04   apatel
//Updated 
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.0   Jun 14 2010 12:08:24   apatel
//Initial revision.
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using System.IO;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.UserPortal.LocationService.DropDownLocationProvider;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common;
using TransportDirect.UserPortal.DataServices;

namespace TransportDirect.UserPortal.LocationService
{
    /// <summary>
    /// Unit tests covering DropDownLocationProviderService class
    /// </summary>
    [TestFixture]
    public class TestDropDownLocationProviderService
    {
        #region Private members

        // Test Data
        private string TEST_DATA = Directory.GetCurrentDirectory() + "\\DropDownGaz\\DropDownGazData.xml";
        private string SETUP_SCRIPT = Directory.GetCurrentDirectory() + "\\DropDownGaz\\DropDownGazSetup.sql";
        private string CLEARUP_SCRIPT = Directory.GetCurrentDirectory() + "\\DropDownGaz\\DropDownGazCleanUp.sql";
        private const string connectionString = "Server=.;Initial Catalog=AtosAdditionalData;Trusted_Connection=true";
        private TestDataManager tm;
        private DropDownLocationHelper dropDownLocationhelper;
        #endregion

        #region SetUp and TearDown
        [TestFixtureSetUp]
        public void SetUp()
        {
            TDServiceDiscovery.ResetServiceDiscoveryForTest();
            TDServiceDiscovery.Init(new TestInitialisation());
            TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.DropDownLocationProvider, new DropDownLocationProvider.DropDownLocationProviderServiceFactory());


            // Set up the test data to use
            tm = new TestDataManager(TEST_DATA, SETUP_SCRIPT, CLEARUP_SCRIPT, connectionString, SqlHelperDatabase.AtosAdditionalDataDB);
            tm.Setup();
            tm.LoadData(false);

            dropDownLocationhelper = new DropDownLocationHelper();
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            dropDownLocationhelper = null;
            // Put database data back to what it was
            tm.ClearData();
            
        }

        /// <summary>
        /// Resets Database values and removes scripts before every test
        /// </summary>
        [SetUp]
        public void Reset()
        {
            
            IDropDownLocationProviderService ddlProvider = (DropDownLocationProviderService)TDServiceDiscovery.Current[ServiceDiscoveryKey.DropDownLocationProvider];

            // Reset the tempDropDownDataScript dictionary
            TestHelper.SetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "tempDropDownDataScripts", new Dictionary<DropDownLocationType, string[]>());

            // Reset the currentDropDownDataScript dictionary
            TestHelper.SetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "currentDropDownDataScripts", new Dictionary<DropDownLocationType, string[]>());

            UpdateSyncLockStatus(DropDownLocationType.Rail, false);

            ClearTempScripts();

            ResetSequenceNo();

            ResetFileUsingAndCreatedCount();
        }

        
        #endregion

        #region Test Public Methods
        /// <summary>
        /// Tests GetDropDownLocationDataScriptName method
        /// </summary>
        [Test]
        public void TestGetDropDownLocationDataScriptName()
        {
            IDropDownLocationProviderService ddlProvider = (DropDownLocationProviderService)TDServiceDiscovery.Current[ServiceDiscoveryKey.DropDownLocationProvider];

            int currentSequenceNo = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail,false);

            int fileParts = dropDownLocationhelper.DropDownDataFileParts(DropDownLocationType.Rail);

            string[] expectedFiles = new string[fileParts];

            for (int i = 1; i <= fileParts; i++)
            {
                expectedFiles[i - 1] = string.Format(dropDownLocationhelper.GetDropDownDataFileName(DropDownLocationType.Rail), currentSequenceNo, i);
                // Remove Temp Scripts if they alreay been created.
                // This will cover the testing of RemoveTempScript method as well
                TestHelper.RunInstanceMethod(typeof(DropDownLocationProviderService), "RemoveTempScript", ddlProvider, new object[] { expectedFiles[i - 1] });
            }

           
            // Testing scripts return for the FindTrainInput page id 
            string[] testFiles = ddlProvider.GetDropDownLocationDataScriptName(PageId.FindTrainInput);

            Assert.AreEqual(fileParts, testFiles.Length);

            for (int count = 0; count < fileParts; count++)
            {
                Assert.AreEqual(expectedFiles[count], testFiles[count]);
            }
            
        }

        /// <summary>
        /// Tests DropDownLocationEnabled method
        /// </summary>
        [Test]
        public void TestDropDownLocationEnabled()
        {
            IDropDownLocationProviderService ddlProvider = (DropDownLocationProviderService)TDServiceDiscovery.Current[ServiceDiscoveryKey.DropDownLocationProvider];

            // test for FindTrainInput page
            bool testValue = ddlProvider.DropDownLocationEnabled(TransportDirect.Common.PageId.FindTrainInput);

            Assert.IsTrue(testValue);

            // test for FindTrainCostInput page
            testValue = ddlProvider.DropDownLocationEnabled(TransportDirect.Common.PageId.FindTrainCostInput);

            Assert.IsTrue(testValue);
          
        }
        
        /// <summary>
        /// Tests DropDownLocationEnabled method for pageId for which auto-suggest functionality is not enabled
        /// </summary>
        [Test]
        public void TestDropDownLocationEnabledNegativeTest()
        {
            IDropDownLocationProviderService ddlProvider = (DropDownLocationProviderService)TDServiceDiscovery.Current[ServiceDiscoveryKey.DropDownLocationProvider];

            bool testValue = ddlProvider.DropDownLocationEnabled(TransportDirect.Common.PageId.FindCoachInput);

            Assert.IsFalse(testValue);
        }
        #endregion

        #region Test Private Methods

        #region DropDown file generation
        /// <summary>
        /// Tests CreateDatafile method
        /// </summary>
        [Test]
        public void TestCreateDataFile()
        {

            try
            {
               
                IDropDownLocationProviderService ddlProvider = (DropDownLocationProviderService)TDServiceDiscovery.Current[ServiceDiscoveryKey.DropDownLocationProvider];

                // Reset the tempDropDownDataScript
                TestHelper.SetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "tempDropDownDataScripts", new Dictionary<DropDownLocationType, string[]>());

                Dictionary<DropDownLocationType, string[]> private_tempDropDownDataScripts = (Dictionary<DropDownLocationType, string[]>)TestHelper.GetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "tempDropDownDataScripts");

                // check the tempDropDownDataScript dictionary before the file generation
                Assert.IsFalse(private_tempDropDownDataScripts.ContainsKey(DropDownLocationType.Rail));

                                Object testValue = TestHelper.RunInstanceMethod(typeof(DropDownLocationProviderService), "LoadData", ddlProvider, new object[] { DropDownLocationType.Rail });

                Assert.IsInstanceOfType(typeof(List<DropDownLocation>), testValue);

                List<DropDownLocation> testlist = (List<DropDownLocation>)testValue;

                int currentSequenceNo = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail, false);

                int fileParts = dropDownLocationhelper.DropDownDataFileParts(DropDownLocationType.Rail);

                string[] expectedFiles = new string[fileParts];

                for (int i = 1; i <= fileParts; i++)
                {
                    expectedFiles[i - 1] = string.Format(dropDownLocationhelper.GetDropDownDataFileName(DropDownLocationType.Rail), currentSequenceNo, i);
                    // Remove Temp Scripts if they alreay been created.
                    TestHelper.RunInstanceMethod(typeof(DropDownLocationProviderService), "RemoveTempScript", ddlProvider, new object[] { expectedFiles[i - 1] });
                }


                TestHelper.RunInstanceMethod(typeof(DropDownLocationProviderService), "CreateDataFile", ddlProvider, new object[] { DropDownLocationType.Rail, testlist });

                // Check the tempDropDownDataScript dictionary after the file generation
                private_tempDropDownDataScripts = (Dictionary<DropDownLocationType, string[]>)TestHelper.GetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "tempDropDownDataScripts");
                
                Assert.IsTrue(private_tempDropDownDataScripts.ContainsKey(DropDownLocationType.Rail));

                string[] actualFiles = private_tempDropDownDataScripts[DropDownLocationType.Rail];

                Assert.AreEqual(expectedFiles.Length, actualFiles.Length);

                // Check if the files are actually generated
                foreach (string scriptFile in actualFiles)
                {
                    Assert.IsTrue(ScriptFileExists(scriptFile));
                }

            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            Reset();
        }
        #endregion

        #region DropDown file removal
        /// <summary>
        /// Tests RemoveDataFiles method
        /// </summary>
        [Test]
        public void TestRemoveDataFiles()
        {
            try
            {
                               
                int currentSequenceNo = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail, false);

                int newSequenceNo = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail, true);

                IDropDownLocationProviderService ddlProvider = (DropDownLocationProviderService)TDServiceDiscovery.Current[ServiceDiscoveryKey.DropDownLocationProvider];

                // Reset the tempDropDownDataScript
                TestHelper.SetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "tempDropDownDataScripts", new Dictionary<DropDownLocationType, string[]>());
                
                Object testValue = TestHelper.RunInstanceMethod(typeof(DropDownLocationProviderService), "LoadData", ddlProvider, new object[] { DropDownLocationType.Rail });

                List<DropDownLocation> testlist = (List<DropDownLocation>)testValue;

                // Create files to delete
                TestHelper.RunInstanceMethod(typeof(DropDownLocationProviderService), "CreateDataFile", ddlProvider, new object[] { DropDownLocationType.Rail, testlist });

                Dictionary<DropDownLocationType, string[]> private_tempDropDownDataScripts = (Dictionary<DropDownLocationType, string[]>)TestHelper.GetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "tempDropDownDataScripts");

                string[] scriptFiles = private_tempDropDownDataScripts[DropDownLocationType.Rail];

                UpdateSequenceNo(DropDownLocationType.Rail, true);

                foreach (string scriptFile in scriptFiles)
                {
                    Assert.IsTrue(ScriptFileExists(scriptFile));
                }

                // Update sequence no so the currently created files have sequence no to be deleted
                UpdateSequenceNo(DropDownLocationType.Rail, true);

                foreach (string scriptFile in scriptFiles)
                {
                    Assert.IsTrue(ScriptFileExists(scriptFile));
                }

                UpdateSequenceNo(DropDownLocationType.Rail, true);

                // Delete the files by calling the method
                TestHelper.RunInstanceMethod(typeof(DropDownLocationProviderService), "RemoveDataFiles", ddlProvider, new object[] { DropDownLocationType.Rail});

                // Make sure the files are deleted
                foreach (string scriptFile in scriptFiles)
                {
                    Assert.IsFalse(ScriptFileExists(scriptFile));
                }

                

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

           
        }

        #endregion

        /// <summary>
        /// Tests UseNewDropDownScripts method
        /// This unit test tests the method in a scenario when the FileUsingCount is not reach to web server count yet
        /// </summary>
        [Test]
        public void TestUseNewDropDownScriptsWithFileUsingCountLessThenWebServerCount()
        {
            //Setting fileCreated server count to mathch the web server count
            for (int i = 0; i < dropDownLocationhelper.WebServerCount; i++)
                dropDownLocationhelper.UpdateSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileCreated);

            // simulate that SyncLock is active and NewSequenceNo been incremented
            UpdateSyncLockStatus(DropDownLocationType.Rail, true);

            UpdateSequenceNo(DropDownLocationType.Rail, true);

            int currentFileUsingCount = GetActualServerSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileUsing);

            int currentSequenceNo = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail, false);

            int newSequenceNo = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail, true);

            // Check the newSequenceNo is greater than currentSequenceNo
            Assert.Greater(newSequenceNo, currentSequenceNo);

            IDropDownLocationProviderService ddlProvider = (DropDownLocationProviderService)TDServiceDiscovery.Current[ServiceDiscoveryKey.DropDownLocationProvider];

            // Reset the tempDropDownDataScript
            TestHelper.SetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "tempDropDownDataScripts", new Dictionary<DropDownLocationType, string[]>());

            // Load data
            Object testValue = TestHelper.RunInstanceMethod(typeof(DropDownLocationProviderService), "LoadData", ddlProvider, new object[] { DropDownLocationType.Rail });

            List<DropDownLocation> testlist = (List<DropDownLocation>)testValue;

            // Create files
            TestHelper.RunInstanceMethod(typeof(DropDownLocationProviderService), "CreateDataFile", ddlProvider, new object[] { DropDownLocationType.Rail, testlist });

            Dictionary<DropDownLocationType, string[]> private_tempDropDownDataScripts = (Dictionary<DropDownLocationType, string[]>)TestHelper.GetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "tempDropDownDataScripts");

            string[] tempscriptFiles = private_tempDropDownDataScripts[DropDownLocationType.Rail];

            // Call the UseNewDropDownScripts method
            TestHelper.RunInstanceMethod(typeof(DropDownLocationProviderService), "UseNewDropDownScripts", ddlProvider, new object[] { DropDownLocationType.Rail });

            // Test values after the call to UseNewDropDownScripts method
            Dictionary<DropDownLocationType, string[]> private_currentDropDownDataScripts = (Dictionary<DropDownLocationType, string[]>)TestHelper.GetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "currentDropDownDataScripts");

            string[] currentscriptFiles = private_currentDropDownDataScripts[DropDownLocationType.Rail];

            int newFileUsingCount = GetActualServerSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileUsing);

            // currentDropDownDataScripts values should have been replace by the tempDropDownDataScripts values for rail 
            Assert.AreEqual(tempscriptFiles.Length, currentscriptFiles.Length);

            for (int i = 0; i < tempscriptFiles.Length; i++)
            {
                Assert.AreEqual(tempscriptFiles[i], currentscriptFiles[i]);
            }

            // FileUsingCount should be incremented by 1
            Assert.AreEqual(currentFileUsingCount + 1, newFileUsingCount);

            // Sync lock should be still active as FileUsingCount is not reached to the WebServerCount
            Assert.IsTrue(GetSyncLockStatus(DropDownLocationType.Rail));

            // Reset the tempDropDownDataScript dictionary
            TestHelper.SetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "tempDropDownDataScripts", new Dictionary<DropDownLocationType, string[]>());

            // Reset the currentDropDownDataScript dictionary
            TestHelper.SetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "currentDropDownDataScripts", new Dictionary<DropDownLocationType, string[]>());

        }

        /// <summary>
        /// Tests UseNewDropDownScripts method
        /// This unit test tests the method in a scenario when the FileUsingCount reach to web server count
        /// </summary>
        [Test]
        public void TestUseNewDropDownScriptsWithFileUsingCountEqualsWebServerCount()
        {
            //Setting fileCreated server count to mathch the web server count
            for (int i = 0; i < dropDownLocationhelper.WebServerCount; i++)
                dropDownLocationhelper.UpdateSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileCreated);

            // simulate that SyncLock is active and NewSequenceNo been incremented
            UpdateSyncLockStatus(DropDownLocationType.Rail, true);

            UpdateSequenceNo(DropDownLocationType.Rail, true);

            int currentFileUsingCount = GetActualServerSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileUsing);

            int currentSequenceNo = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail, false);

            int newSequenceNo = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail, true);

            // Check the newSequenceNo is greater than currentSequenceNo
            Assert.Greater(newSequenceNo, currentSequenceNo);

            IDropDownLocationProviderService ddlProvider = (DropDownLocationProviderService)TDServiceDiscovery.Current[ServiceDiscoveryKey.DropDownLocationProvider];

            // Reset the tempDropDownDataScript
            TestHelper.SetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "tempDropDownDataScripts", new Dictionary<DropDownLocationType, string[]>());

            // Load data
            Object testValue = TestHelper.RunInstanceMethod(typeof(DropDownLocationProviderService), "LoadData", ddlProvider, new object[] { DropDownLocationType.Rail });

            List<DropDownLocation> testlist = (List<DropDownLocation>)testValue;

            // Create script files
            TestHelper.RunInstanceMethod(typeof(DropDownLocationProviderService), "CreateDataFile", ddlProvider, new object[] { DropDownLocationType.Rail, testlist });

            Dictionary<DropDownLocationType, string[]> private_tempDropDownDataScripts = (Dictionary<DropDownLocationType, string[]>)TestHelper.GetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "tempDropDownDataScripts");

            string[] tempscriptFiles = private_tempDropDownDataScripts[DropDownLocationType.Rail];

            
            int newFileUsingCount = GetActualServerSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileUsing);

            // immitating File Using Count reached to the count such that last server gets the change notification
            for (int i = newFileUsingCount; i < dropDownLocationhelper.WebServerCount - 1; i++)
                dropDownLocationhelper.UpdateSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileUsing);

            // Call to the UseNewDropDownScripts method, this will also switch the provider to use the new scripts created
            TestHelper.RunInstanceMethod(typeof(DropDownLocationProviderService), "UseNewDropDownScripts", ddlProvider, new object[] { DropDownLocationType.Rail });

            Dictionary<DropDownLocationType, string[]> private_currentDropDownDataScripts = (Dictionary<DropDownLocationType, string[]>)TestHelper.GetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "currentDropDownDataScripts");

            string[] currentscriptFiles = private_currentDropDownDataScripts[DropDownLocationType.Rail];

            
            Assert.AreEqual(tempscriptFiles.Length, currentscriptFiles.Length);

            for (int i = 0; i < tempscriptFiles.Length; i++)
            {
                Assert.AreEqual(tempscriptFiles[i], currentscriptFiles[i]);
            }

            newFileUsingCount = GetActualServerSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileUsing);

            int newFileCreatedCount = GetActualServerSyncCount(DropDownLocationType.Rail,ServerSyncCountType.FileCreated);

            // Test the values after the call to UseNewDropDownScripts method
            Assert.AreEqual(tempscriptFiles.Length, currentscriptFiles.Length);

            for (int i = 0; i < tempscriptFiles.Length; i++)
            {
                Assert.AreEqual(tempscriptFiles[i], currentscriptFiles[i]);
            }

            // FileUsingCount reach to web server count so FileUsing/FileCreated count will be reset to 0
            Assert.AreEqual(0, newFileUsingCount);

            Assert.AreEqual(0, newFileCreatedCount);

            int currentSequenceNoAfterReset = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail, false);

            int newSequenceNoAfterReset = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail, true);

            // After reset both current and new sequence no will be same as the new sequence no incremented by importe
            Assert.AreEqual(currentSequenceNoAfterReset, newSequenceNoAfterReset);

            Assert.AreEqual(newSequenceNo, newSequenceNoAfterReset);

            // Synch lock should have become inactive
            Assert.IsFalse(GetSyncLockStatus(DropDownLocationType.Rail));

        }

        /// <summary>
        /// Tests UseNewDropDownScripts method
        /// This unit test tests the method in a scenario when the FileUsingCount is reach to web server count
        /// but when reset been called The Sync lock found as inactive so the Exception been raised
        /// Error log should be checked to see errors after test
        /// </summary>
        [Test(Description = "This unit test tests the method in a error condition scenario" +
           "when the FileUsingCount is reach to web server count"
           + "but when reset been called The Sync lock found as inactive."
           + " Error log should be checked to see errors after test")]
        public void TestUseNewDropDownScriptsWithExceptionAsServerLockStatusIsFalse()
        {
            //Setting fileCreated server count to mathch the web server count
            for (int i = 0; i < dropDownLocationhelper.WebServerCount; i++)
                dropDownLocationhelper.UpdateSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileCreated);

            UpdateSequenceNo(DropDownLocationType.Rail, true);

            int currentFileUsingCount = GetActualServerSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileUsing);

            int currentSequenceNo = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail, false);

            int newSequenceNo = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail, true);

            Assert.Greater(newSequenceNo, currentSequenceNo);

            IDropDownLocationProviderService ddlProvider = (DropDownLocationProviderService)TDServiceDiscovery.Current[ServiceDiscoveryKey.DropDownLocationProvider];

            // Reset the tempDropDownDataScript
            TestHelper.SetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "tempDropDownDataScripts", new Dictionary<DropDownLocationType, string[]>());

            Object testValue = TestHelper.RunInstanceMethod(typeof(DropDownLocationProviderService), "LoadData", ddlProvider, new object[] { DropDownLocationType.Rail });

            List<DropDownLocation> testlist = (List<DropDownLocation>)testValue;

            TestHelper.RunInstanceMethod(typeof(DropDownLocationProviderService), "CreateDataFile", ddlProvider, new object[] { DropDownLocationType.Rail, testlist });

            Dictionary<DropDownLocationType, string[]> private_tempDropDownDataScripts = (Dictionary<DropDownLocationType, string[]>)TestHelper.GetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "tempDropDownDataScripts");

            string[] tempscriptFiles = private_tempDropDownDataScripts[DropDownLocationType.Rail];

            TestHelper.RunInstanceMethod(typeof(DropDownLocationProviderService), "UseNewDropDownScripts", ddlProvider, new object[] { DropDownLocationType.Rail });

            Dictionary<DropDownLocationType, string[]> private_currentDropDownDataScripts = (Dictionary<DropDownLocationType, string[]>)TestHelper.GetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "currentDropDownDataScripts");

            string[] currentscriptFiles = private_currentDropDownDataScripts[DropDownLocationType.Rail];

            int newFileUsingCount = GetActualServerSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileUsing);

            Assert.AreEqual(tempscriptFiles.Length, currentscriptFiles.Length);

            for (int i = 0; i < tempscriptFiles.Length; i++)
            {
                Assert.AreEqual(tempscriptFiles[i], currentscriptFiles[i]);
            }

            // Simulating File Using Count reached to the count such that last server gets the change notification
            for (int i = newFileUsingCount; i < dropDownLocationhelper.WebServerCount - 1; i++)
                dropDownLocationhelper.UpdateSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileUsing);


            // Simulate last server getting the change notification to use the new file and UseNewDropDownScripts method gets called
            TestHelper.RunInstanceMethod(typeof(DropDownLocationProviderService), "UseNewDropDownScripts", ddlProvider, new object[] { DropDownLocationType.Rail });

            // Checking the values after the method gets called
            // As we got error condition Reset should not change the values
            newFileUsingCount = GetActualServerSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileUsing);

            int newFileCreatedCount = GetActualServerSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileCreated);

            // Server is in inconsitant state so FileUsing/Created Count want reset back to 0
            Assert.AreNotEqual(0, newFileUsingCount);

            Assert.AreNotEqual(0, newFileCreatedCount);

            int currentSequenceNoAfterReset = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail, false);

            int newSequenceNoAfterReset = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail, true);

            // Current Sequence will not be equal to new sequence no
            Assert.AreNotEqual(currentSequenceNoAfterReset, newSequenceNoAfterReset);

            // Synch lock want be released and will be in active state
            Assert.IsFalse(GetSyncLockStatus(DropDownLocationType.Rail));
        }

        /// <summary>
        /// Tests UseNewDropDownScripts method
        /// This unit test tests the method in a error condition scenario when the FileCreatedCount is not reach to web server count
        /// Error log should be checked to see errors after test
        /// </summary>
        [Test(Description= "This unit test tests the method in a error condition scenario" +
            "when the FileCreatedCount is not reach to web server count."
            +" Error log should be checked to see errors after test")]
        public void TestUseNewDropDownScriptsWithFileCreatedCountNotEqualToWebServerCount()
        {

            UpdateSyncLockStatus(DropDownLocationType.Rail, true);

            UpdateSequenceNo(DropDownLocationType.Rail, true);

            int currentFileUsingCount = GetActualServerSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileUsing);

            int currentSequenceNo = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail, false);

            IDropDownLocationProviderService ddlProvider = (DropDownLocationProviderService)TDServiceDiscovery.Current[ServiceDiscoveryKey.DropDownLocationProvider];

            // Set random files to currentDropDownDataScript dictionary so we can check that the files not been changed after
            // call to UseNewDropDownScripts method
            string[] testCurrentFiles = new string[] { "a", "b", "c" };
            Dictionary<DropDownLocationType, string[]> testCurrDict =  new Dictionary<DropDownLocationType, string[]>();
            testCurrDict.Add(DropDownLocationType.Rail,testCurrentFiles);

            // Reset the currentDropDownDataScript dictionary
            TestHelper.SetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "currentDropDownDataScripts", testCurrDict);

            
            //Setting fileCreated server count so its not match to webservercount
            for (int i = 0; i < dropDownLocationhelper.WebServerCount-1; i++)
                dropDownLocationhelper.UpdateSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileCreated);

            
            // Reset the tempDropDownDataScript
            TestHelper.SetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "tempDropDownDataScripts", new Dictionary<DropDownLocationType, string[]>());

            // Load Data
            Object testValue = TestHelper.RunInstanceMethod(typeof(DropDownLocationProviderService), "LoadData", ddlProvider, new object[] { DropDownLocationType.Rail });

            List<DropDownLocation> testlist = (List<DropDownLocation>)testValue;

            // Create script files
            TestHelper.RunInstanceMethod(typeof(DropDownLocationProviderService), "CreateDataFile", ddlProvider, new object[] { DropDownLocationType.Rail, testlist });

            Dictionary<DropDownLocationType, string[]> private_tempDropDownDataScripts = (Dictionary<DropDownLocationType, string[]>)TestHelper.GetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "tempDropDownDataScripts");

            string[] tempscriptFiles = private_tempDropDownDataScripts[DropDownLocationType.Rail];

            // Call to UseNewDropDownScripts method
            TestHelper.RunInstanceMethod(typeof(DropDownLocationProviderService), "UseNewDropDownScripts", ddlProvider, new object[] { DropDownLocationType.Rail });

            Dictionary<DropDownLocationType, string[]> private_currentDropDownDataScripts = (Dictionary<DropDownLocationType, string[]>)TestHelper.GetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "currentDropDownDataScripts");

            Assert.IsNotEmpty(private_currentDropDownDataScripts);

            string[] currentscriptFiles = private_currentDropDownDataScripts[DropDownLocationType.Rail];

            // Test values after the call to UseNewDropDownScripts method
            Assert.AreEqual(tempscriptFiles.Length, currentscriptFiles.Length);

            // currentDropDownDataScript values should remain unchanged
            for (int i = 0; i < tempscriptFiles.Length; i++)
            {
                Assert.AreNotEqual(tempscriptFiles[i], currentscriptFiles[i]);
                Assert.AreEqual(testCurrentFiles[i], currentscriptFiles[i]);
            }

            
            int newFileUsingCount = GetActualServerSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileUsing);

            int newFileCreatedCount = GetActualServerSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileCreated);
            
            // FileUsing Count should remain same
            Assert.AreEqual(currentFileUsingCount, newFileUsingCount);

            // File created count should not be reset back to 0
            Assert.AreNotEqual(0, newFileCreatedCount);

            int currentSequenceNoAfterReset = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail, false);

            int newSequenceNoAfterReset = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail, true);

            // Current sequence no should be remain unchanged
            Assert.AreEqual(currentSequenceNoAfterReset, currentSequenceNo);

            Assert.AreEqual(currentSequenceNo + 1, newSequenceNoAfterReset);

            // Sync lock should remain active
            Assert.IsTrue(GetSyncLockStatus(DropDownLocationType.Rail));

        }

        #endregion

        #region Test Change Notification
        /// <summary>
        /// Tests change notifications
        /// </summary>
        [Test(Description="Change Notifications test")]
        public void TestChangeNotifications()
        {
            IDropDownLocationProviderService ddlProvider = (DropDownLocationProviderService)TDServiceDiscovery.Current[ServiceDiscoveryKey.DropDownLocationProvider];

            // Register data notification
            TestMockDataChangeNotification dataChangeNotification = (TestMockDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];

            // This emulates sync lock enabled by Naptan or alias load process
            UpdateSyncLockStatus(DropDownLocationType.Rail, true);

            // This emulates new sequence no generated by Naptan or alias load process
            UpdateSequenceNo(DropDownLocationType.Rail, true);

            
            //Manually raise change notification
            dataChangeNotification.RaiseChangedEvent("DropDownGazRail");

            // Check for current File Created Count which should be 1 
            int currentFileCreatedCount = GetActualServerSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileCreated);

            Assert.IsTrue(currentFileCreatedCount == 1);

            // Check for current File Using Count which should be still 0 (unchanged)
            int currentFileUsingCount = GetActualServerSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileUsing);

            Assert.IsTrue(currentFileUsingCount == 0);

            int currentSequenceNo = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail, false);

            int newSequenceNo = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail, true);

            Assert.Greater(newSequenceNo, currentSequenceNo);

            // Check files are generated and file names are stored in temp dictionary
            Dictionary<DropDownLocationType, string[]> private_tempDropDownDataScripts = (Dictionary<DropDownLocationType, string[]>)TestHelper.GetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "tempDropDownDataScripts");

            string[] tempscriptFiles = private_tempDropDownDataScripts[DropDownLocationType.Rail];

            Assert.IsNotNull(tempscriptFiles);

            Assert.AreEqual(dropDownLocationhelper.DropDownDataFileParts(DropDownLocationType.Rail), tempscriptFiles.Length);

            foreach (string scriptFile in tempscriptFiles)
            {
                Assert.IsTrue(ScriptFileExists(scriptFile));
            }

            // Imitate all the server apart from created the files so increase the FileCreatedCount to WebServerCount
            dropDownLocationhelper.UpdateSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileCreated);
            dropDownLocationhelper.UpdateSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileCreated);
            dropDownLocationhelper.UpdateSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileCreated);

            // Also update the FileUsingCount to WebServerCount minus 1 so we can test reset and release of lock
            dropDownLocationhelper.UpdateSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileUsing);
            dropDownLocationhelper.UpdateSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileUsing);
            dropDownLocationhelper.UpdateSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileUsing);

            //Manually raise change notification - The change notification received by the last server
            // This will cause server the raise DropDownGazSyncRail change notification
            // At the end of the notification newly generated script files will be put in use by updating current script file dictionary
            dataChangeNotification.RaiseChangedEvent("DropDownGazSyncRail");

            // Check files are generated and file names are stored in temp dictionary
            Dictionary<DropDownLocationType, string[]> private_currentDropDownDataScripts = (Dictionary<DropDownLocationType, string[]>)TestHelper.GetFieldValue(typeof(DropDownLocationProviderService), ddlProvider, "currentDropDownDataScripts");

            string[] scriptFiles = private_currentDropDownDataScripts[DropDownLocationType.Rail];

            Assert.IsNotNull(scriptFiles);

            Assert.AreEqual(dropDownLocationhelper.DropDownDataFileParts(DropDownLocationType.Rail), scriptFiles.Length);

            for (int i = 0; i < tempscriptFiles.Length; i++)
            {
                Assert.AreEqual(tempscriptFiles[i], scriptFiles[i]);
            }

            // After reset FileUsingCount and FileCreatedCount should be 0
            int newFileUsingCount = GetActualServerSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileUsing);

            int newFileCreatedCount = GetActualServerSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileCreated);

            Assert.AreEqual(0, newFileUsingCount);

            Assert.AreEqual(0, newFileCreatedCount);

            // After reset Current and New Sequence number should be same
            int currentSequenceNoAfterReset = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail, false);

            int newSequenceNoAfterReset = dropDownLocationhelper.GetDropDownDataSequenceNumber(DropDownLocationType.Rail, true);

            Assert.AreEqual(currentSequenceNoAfterReset, newSequenceNoAfterReset);

            // New Sequence number before and after reset should be same
            Assert.AreEqual(newSequenceNo, newSequenceNoAfterReset);

            // Sync lock should have been released after reset
            Assert.IsFalse(GetSyncLockStatus(DropDownLocationType.Rail));


        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Helper method to get sequence no by direct call to database
        /// </summary>
        /// <param name="ddlType"></param>
        /// <param name="isNew"></param>
        /// <returns></returns>
        private int GetActualSequenceNo(DropDownLocationType ddlType, bool isNew)
        {
            // Database
            SqlHelper sqlHelper = new SqlHelper();
            SqlHelperDatabase database = SqlHelperDatabase.AtosAdditionalDataDB;

            int seqNo = -1;

            try
            {

                string query = "SELECT SequenceNumNew FROM DropDownSyncStatus WHERE DropDownType = '" + ddlType.ToString() + "'";
                if (!isNew)
                    query = "SELECT SequenceNumCurrent FROM DropDownSyncStatus WHERE DropDownType = '" + ddlType.ToString() + "'";

                sqlHelper.ConnOpen(database);

                seqNo = (Int32)sqlHelper.GetScalar(query);

            }
            catch (Exception ex)
            {
                seqNo = -1;
                throw ex;
            }
            finally
            {
                if (sqlHelper.ConnIsOpen)
                    sqlHelper.ConnClose();
            }

            return seqNo;
        }

        /// <summary>
        /// Helper method to update sequence nos by direct call to database
        /// </summary>
        /// <param name="ddlType"></param>
        /// <param name="isNew"></param>
        private void UpdateSequenceNo(DropDownLocationType ddlType, bool isNew)
        {
            // Database
            SqlHelper sqlHelper = new SqlHelper();
            SqlHelperDatabase database = SqlHelperDatabase.AtosAdditionalDataDB;


            try
            {

                string query = "UPDATE DropDownSyncStatus SET "
                    + (isNew ? "SequenceNumNew = SequenceNumNew + 1" : "SequenceNumCurrent=SequenceNumCurrent + 1")
                    + " WHERE DropDownType = '" + ddlType.ToString() + "'";

                sqlHelper.ConnOpen(database);

                sqlHelper.Execute(query);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlHelper.ConnIsOpen)
                    sqlHelper.ConnClose();
            }

        }

        /// <summary>
        /// Reset sequence numbers to 0 using direct call to database
        /// </summary>
        private void ResetSequenceNo()
        {
            // Database
            SqlHelper sqlHelper = new SqlHelper();
            SqlHelperDatabase database = SqlHelperDatabase.AtosAdditionalDataDB;


            try
            {

                string query = "UPDATE DropDownSyncStatus SET SequenceNumCurrent=5000, SequenceNumNew = 5000";

                sqlHelper.ConnOpen(database);

                sqlHelper.Execute(query);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlHelper.ConnIsOpen)
                    sqlHelper.ConnClose();
            }
        }

        /// <summary>
        /// Reset File Using/Created counts to 0 using direct call to database
        /// </summary>
        private void ResetFileUsingAndCreatedCount()
        {
            // Database
            SqlHelper sqlHelper = new SqlHelper();
            SqlHelperDatabase database = SqlHelperDatabase.AtosAdditionalDataDB;


            try
            {

                string query = "UPDATE DropDownSyncStatus SET ServerSynchFileCreatedCount=0, ServerSynchFileUsingCount = 0";

                sqlHelper.ConnOpen(database);

                sqlHelper.Execute(query);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlHelper.ConnIsOpen)
                    sqlHelper.ConnClose();
            }
        }

        /// <summary>
        /// Get File Using/Created counts using direct call to database
        /// </summary>
        /// <param name="ddlType"></param>
        /// <param name="syncType"></param>
        /// <returns></returns>
        private int GetActualServerSyncCount(DropDownLocationType ddlType, ServerSyncCountType syncType)
        {
            // Database
            SqlHelper sqlHelper = new SqlHelper();
            SqlHelperDatabase database = SqlHelperDatabase.AtosAdditionalDataDB;

            int count = -1;

            try
            {

                string query = "SELECT ServerSynchFileCreatedCount FROM DropDownSyncStatus WHERE DropDownType = '" + ddlType.ToString() + "'";
                if (syncType == ServerSyncCountType.FileUsing)
                    query = "SELECT ServerSynchFileUsingCount FROM DropDownSyncStatus WHERE DropDownType = '" + ddlType.ToString() + "'";

                sqlHelper.ConnOpen(database);

                count = (Int32)sqlHelper.GetScalar(query);

            }
            catch (Exception ex)
            {
                count = -1;
                throw ex;
            }
            finally
            {
                if (sqlHelper.ConnIsOpen)
                    sqlHelper.ConnClose();
            }

            return count;
        }

        /// <summary>
        /// Gets the sync lock status using direct call to database
        /// </summary>
        /// <param name="ddlType"></param>
        /// <returns></returns>
        private bool GetSyncLockStatus(DropDownLocationType ddlType)
        {
            // Database
            SqlHelper sqlHelper = new SqlHelper();
            SqlHelperDatabase database = SqlHelperDatabase.AtosAdditionalDataDB;

            bool locked = false;

            try
            {

                string query = "SELECT UpdateLock FROM DropDownSyncStatus WHERE DropDownType = '" + ddlType.ToString() + "'";

                sqlHelper.ConnOpen(database);

                locked = (bool)sqlHelper.GetScalar(query);

            }
            catch (Exception ex)
            {
                locked = false;
                throw ex;
            }
            finally
            {
                if (sqlHelper.ConnIsOpen)
                    sqlHelper.ConnClose();
            }

            return locked;
        }

        /// <summary>
        /// Update the sync lock status using direct call to database
        /// </summary>
        /// <param name="ddlType"></param>
        /// <param name="locked"></param>
        private void UpdateSyncLockStatus(DropDownLocationType ddlType, bool locked)
        {
            // Database
            SqlHelper sqlHelper = new SqlHelper();
            SqlHelperDatabase database = SqlHelperDatabase.AtosAdditionalDataDB;


            try
            {

                string query = "UPDATE DropDownSyncStatus SET UpdateLock=" + (locked ? 1 : 0) + " WHERE DropDownType = '" + ddlType.ToString() + "'";

                sqlHelper.ConnOpen(database);

                sqlHelper.Execute(query);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlHelper.ConnIsOpen)
                    sqlHelper.ConnClose();
            }

        }

        /// <summary>
        /// Check if the script file generated by dropdownlocation provider exists
        /// </summary>
        /// <param name="scriptName"></param>
        /// <returns>true if the file with the script name exists in the tempScripts folder</returns>
        private bool ScriptFileExists(string scriptName)
        {
            string configFilePath = Properties.Current["ScriptRepositoryPath"];
            string browserSupport = "W3C_STYLE";

            // Create TempScript directory in case it does not exist
            Directory.CreateDirectory(Directory.GetParent(configFilePath).FullName + @"\tempscripts");
            DirectoryInfo[] asubDir = Directory.GetParent(configFilePath).GetDirectories("tempscripts");
            DirectoryInfo tempDir = asubDir[0];

            string strTempFolder = tempDir.FullName;
            string strTempFileName = scriptName + "_" + browserSupport + ".js";
            string strTempFileRef = Directory.GetParent(configFilePath).Name + "/"
                                        + tempDir.Name + "/"
                                            + strTempFileName;

            // Log error if no script exists
            FileInfo scriptInfo = new FileInfo(strTempFolder + @"\" + strTempFileName);

            return scriptInfo.Exists;
        }

        /// <summary>
        /// Clears the script file generated by dropdownlocation provider
        /// Deletes all the files resided in tempscripts folder
        /// </summary>
        private void ClearTempScripts()
        {
            string configFilePath = Properties.Current["ScriptRepositoryPath"];
            
            // Create TempScript directory in case it does not exist
            Directory.CreateDirectory(Directory.GetParent(configFilePath).FullName + @"\tempscripts");
            DirectoryInfo[] asubDir = Directory.GetParent(configFilePath).GetDirectories("tempscripts");
            DirectoryInfo tempDir = asubDir[0];

            string strTempFolder = tempDir.FullName;

            string[] files = Directory.GetFiles(strTempFolder);

            foreach (string fileToDelete in files)
            {
                File.Delete(fileToDelete);
            }

        }

        
        #endregion
    }
}
