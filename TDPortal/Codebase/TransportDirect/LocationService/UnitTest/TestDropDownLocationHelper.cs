// *********************************************** 
// NAME                 : TestDropDownLocationHelper.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 14/06/2010 
// DESCRIPTION  	    : Unit test class containing unit tests for DropDownLocationHelper class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestDropDownLocationHelper.cs-arc  $ 
//
//   Rev 1.1   Jun 14 2010 12:49:14   apatel
//Added Comments
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

namespace TransportDirect.UserPortal.LocationService
{
    [TestFixture]
    public class TestDropDownLocationHelper
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
        /// <summary>
        /// Sets up the required resources for unit tests
        /// Prepares database for unit testing
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

            dropDownLocationhelper = new DropDownLocationHelper();
        }

        /// <summary>
        /// Releases the Resources, Sets the database back to its original state.
        /// </summary>
        [TestFixtureTearDown]
        public void TearDown()
        {
            dropDownLocationhelper = null;
            // Put database data back to what it was
            tm.ClearData();
            TDServiceDiscovery.ResetServiceDiscoveryForTest();
        }
        #endregion

        #region Property Provider helper methods
        /// <summary>
        /// Tests IsDropDownEnabled private method of DropDownLocationHelper class
        /// </summary>
        [Test]
        public void TestIsDropDownEnabled()
        {
            Object testValue = TestHelper.RunInstanceMethod(typeof(DropDownLocationHelper),"IsDropDownEnabled",dropDownLocationhelper,new object[0]);
            Assert.IsTrue((bool)testValue);

        }

        /// <summary>
        /// Tests IsDropDownEnabled overloaded private method of DropDownLocationHelper class
        /// to check if auto-suggest enabled for specific page id
        /// </summary>
        [Test]
        public void TestIsDropDownEnabledForPageId()
        {
            Object testValue = TestHelper.RunInstanceMethod(typeof(DropDownLocationHelper), "IsDropDownEnabled", dropDownLocationhelper, new object[] {PageId.FindTrainInput});
            Assert.IsTrue((bool)testValue);

        }

        /// <summary>
        /// Tests IsDropDownEnabled overloaded private method of DropDownLocationHelper class 
        /// to test with a PageId for which auto-suggest is disabled
        /// </summary>
        [Test]
        public void TestIsDropDownDisabledForPageId()
        {
            Object testValue = TestHelper.RunInstanceMethod(typeof(DropDownLocationHelper), "IsDropDownEnabled", dropDownLocationhelper, new object[] { PageId.FindCoachInput });
            Assert.IsFalse((bool)testValue);

        }
        #endregion

        #region Database Calls
        /// <summary>
        /// Tests GetDropDownDataSequenceNumber method
        /// </summary>
        [Test]
        public void TestGetDropDownDataSequenceNumber()
        {
            try
            {
                // get the new sequence value using DropDownLocationHelper method
                int testNewSequenceValue = (Int32)TestHelper.RunInstanceMethod(typeof(DropDownLocationHelper), "GetDropDownDataSequenceNumber", dropDownLocationhelper, new object[] { DropDownLocationType.Rail, true });

                // get the old sequence value using DropDownLocationHelper method
                int testOldSequenceValue = (Int32)TestHelper.RunInstanceMethod(typeof(DropDownLocationHelper), "GetDropDownDataSequenceNumber", dropDownLocationhelper, new object[] { DropDownLocationType.Rail, false });

                // get the actual values from database call 
                int actualNewSequenceValue = GetActualSequenceNo(DropDownLocationType.Rail, true);

                int actualOldSequenceValue = GetActualSequenceNo(DropDownLocationType.Rail, false);

                Assert.AreEqual(actualNewSequenceValue, testNewSequenceValue);

                Assert.AreEqual(actualOldSequenceValue, testOldSequenceValue);
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            
        }

        /// <summary>
        /// Tests GetDropDownData method
        /// </summary>
        [Test]
        public void TestGetDropDownData()
        {
            int alias = 0;
            int group = 0;
            try
            {
                Object testValue = TestHelper.RunInstanceMethod(typeof(DropDownLocationHelper), "GetDropDownData", dropDownLocationhelper, new object[] { DropDownLocationType.Rail });

                Assert.IsInstanceOfType(typeof(List<DropDownLocation>), testValue);

                List<DropDownLocation> testlist = (List<DropDownLocation>) testValue;

                Assert.IsNotEmpty(testlist);

                Assert.AreEqual(5, testlist.Count);

                foreach (DropDownLocation ddl in testlist)
                {
                    if (ddl.IsGroup) group++;

                    if (ddl.IsAlias) alias++;
                }

                Assert.AreEqual(1, group);
                Assert.AreEqual(1, alias);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Tests GetServerSyncCount
        /// </summary>
        [Test]
        public void TestGetServerSyncCount()
        {
            try
            {
                int testFileCreatedCount = (Int32)TestHelper.RunInstanceMethod(typeof(DropDownLocationHelper), "GetServerSyncCount", dropDownLocationhelper, new object[] { DropDownLocationType.Rail, ServerSyncCountType.FileCreated });

                int testFileUsingCount = (Int32)TestHelper.RunInstanceMethod(typeof(DropDownLocationHelper), "GetServerSyncCount", dropDownLocationhelper, new object[] { DropDownLocationType.Rail, ServerSyncCountType.FileUsing });

                int actualFileCreatedCount = GetActualServerSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileCreated);

                int actualFileUsingCount = GetActualServerSyncCount(DropDownLocationType.Rail, ServerSyncCountType.FileUsing);

                Assert.AreEqual(actualFileCreatedCount, testFileCreatedCount);

                Assert.AreEqual(actualFileUsingCount, testFileUsingCount);

                
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Tests UpdateSyncCount method
        /// </summary>
        [Test]
        public void TestUpdateSyncCount()
        {
            try
            {
                // Get the current sync lock values
                int currentFileCreatedCount = (Int32)TestHelper.RunInstanceMethod(typeof(DropDownLocationHelper), "GetServerSyncCount", dropDownLocationhelper, new object[] { DropDownLocationType.Rail, ServerSyncCountType.FileCreated });

                int currentFileUsingCount = (Int32)TestHelper.RunInstanceMethod(typeof(DropDownLocationHelper), "GetServerSyncCount", dropDownLocationhelper, new object[] { DropDownLocationType.Rail, ServerSyncCountType.FileUsing });

                // Update the sync lock values
                TestHelper.RunInstanceMethod(typeof(DropDownLocationHelper), "UpdateSyncCount", dropDownLocationhelper, new object[] { DropDownLocationType.Rail, ServerSyncCountType.FileCreated });

                TestHelper.RunInstanceMethod(typeof(DropDownLocationHelper), "UpdateSyncCount", dropDownLocationhelper, new object[] { DropDownLocationType.Rail, ServerSyncCountType.FileUsing });

                int testFileCreatedCount = (Int32)TestHelper.RunInstanceMethod(typeof(DropDownLocationHelper), "GetServerSyncCount", dropDownLocationhelper, new object[] { DropDownLocationType.Rail, ServerSyncCountType.FileCreated });

                int testFileUsingCount = (Int32)TestHelper.RunInstanceMethod(typeof(DropDownLocationHelper), "GetServerSyncCount", dropDownLocationhelper, new object[] { DropDownLocationType.Rail, ServerSyncCountType.FileUsing });

                Assert.Greater(testFileCreatedCount,0);

                Assert.Greater(testFileUsingCount,0);

                Assert.AreEqual(currentFileCreatedCount + 1, testFileCreatedCount);

                Assert.AreEqual(currentFileUsingCount + 1, testFileUsingCount);


            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        /// <summary>
        /// Tests ResetServerSyncStatus method
        /// </summary>
        [Test]
        public void TestResetServerSyncStatus()
        {
            try
            {
                bool locked = GetSyncLockStatus(DropDownLocationType.Rail);

                // set SyncLockStatus to true
                UpdateSyncLockStatus(DropDownLocationType.Rail, true);

                // Update SyncCount for FileCreated and FileUsing
                TestHelper.RunInstanceMethod(typeof(DropDownLocationHelper), "UpdateSyncCount", dropDownLocationhelper, new object[] { DropDownLocationType.Rail, ServerSyncCountType.FileCreated });

                TestHelper.RunInstanceMethod(typeof(DropDownLocationHelper), "UpdateSyncCount", dropDownLocationhelper, new object[] { DropDownLocationType.Rail, ServerSyncCountType.FileUsing });

                // Call ResetServerSyncStatus method
                TestHelper.RunInstanceMethod(typeof(DropDownLocationHelper), "ResetServerSyncStatus", dropDownLocationhelper, new object[] { DropDownLocationType.Rail });

                int testFileCreatedCount = (Int32)TestHelper.RunInstanceMethod(typeof(DropDownLocationHelper), "GetServerSyncCount", dropDownLocationhelper, new object[] { DropDownLocationType.Rail, ServerSyncCountType.FileCreated });

                int testFileUsingCount = (Int32)TestHelper.RunInstanceMethod(typeof(DropDownLocationHelper), "GetServerSyncCount", dropDownLocationhelper, new object[] { DropDownLocationType.Rail, ServerSyncCountType.FileUsing });

                Assert.AreEqual(0, testFileCreatedCount);

                Assert.AreEqual(0, testFileUsingCount);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }



        #endregion

        #region Helper Methods
        /// <summary>
        /// Helper method to get Sequence no from database direct
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
                if(!isNew)
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
        /// Helper method to update sequence numbers in the database
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
        /// Gets ServerSyncCounts from the database
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
        /// Gets the current server sync lock status from the database
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
        /// Updates the server sync lock status
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

                string query = "UPDATE DropDownSyncStatus SET UpdateLock=" + (locked?1:0) +" WHERE DropDownType = '" + ddlType.ToString() + "'";
               
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
        #endregion


    }
}
