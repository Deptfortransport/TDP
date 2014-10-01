// *********************************************** 
// NAME			: TestJourneyNoteFilter.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED : 19/03/2013 
// DESCRIPTION	: Class testing the funcationality of JourneyNoteFilter
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestJourneyNoteFilter.cs-arc  $
//
//   Rev 1.1   Apr 02 2013 11:18:22   mmodi
//Unit test updates
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.0   Mar 21 2013 10:15:18   mmodi
//Initial revision.
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//

using System;
using System.Collections;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Data.SqlClient;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.JourneyControl
{
    /// <summary>
    /// Class testing the funcationality of JourneyNoteFilter
    /// </summary>
    [TestFixture]
    public class TestJourneyNoteFilter
    {
        public TestJourneyNoteFilter()
		{
		}

		#region Setup/TearDown

		/// <summary>
		/// Initialisation in setup method called before every test method
		/// </summary>
		[SetUp]
		public void Init() 
		{ 
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
            TDServiceDiscovery.Init(new JourneyNoteFilterTestInitialisation());

            JourneyNoteFilterTestHelper.BackupCurrentData();
            JourneyNoteFilterTestHelper.LoadTestData(JourneyNoteFilterTestDataSet.TestData1);

			Trace.Listeners.Remove("TDTraceListener");
		
			IEventPublisher[] customPublishers = new IEventPublisher[0];																								
			ArrayList errors = new ArrayList();
				
			try
			{
				Trace.Listeners.Add( new TDTraceListener(Properties.Current, customPublishers, errors));
			}
			catch (TDException)
			{
				Assert.IsTrue(false);
			}
		}

		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TearDown]
		public void TearDown() 
		{
            JourneyNoteFilterTestHelper.RestoreOriginalData();
		}

		#endregion

		#region Tests
		/// <summary>
		/// Checks that FilterText method works correctly
		/// </summary>
		[Test]
		public void TestFilterTextMethod()
		{
            JourneyNoteFilter filter = new JourneyNoteFilter();

            // Notes should either be "displayed" or not dependent on the supplied note and the test data
            Assert.AreEqual(false, filter.DisplayNote(ModeType.Rail, "EA", false, "This is test note 1"));
            Assert.AreEqual(false, filter.DisplayNote(ModeType.Rail, "SW", false, "This is test note rail and SW"));
            Assert.AreEqual(true, filter.DisplayNote(ModeType.Coach, "EA", false, "This is test note rail with mode coach"));
            Assert.AreEqual(false, filter.DisplayNote(ModeType.Rail, "SW", false, "This is test note SW"));
            Assert.AreEqual(false, filter.DisplayNote(ModeType.Rail, "WM", false, "This is test note multi region"));
            Assert.AreEqual(false, filter.DisplayNote(ModeType.Ferry, "EM", false, "This is test note ferry EM"));
            Assert.AreEqual(true, filter.DisplayNote(ModeType.Ferry, "L", false, "This is test note ferry EM"));
            Assert.AreEqual(true, filter.DisplayNote(ModeType.Metro, "L", false, "This is test note with space at end")); // No space at end
            Assert.AreEqual(false, filter.DisplayNote(ModeType.Metro, "L", false, "This is test note with space at end "));
        }


		/// <summary>
		/// Checks that FilerText method works correctly with Abnormal vales
		/// </summary>
		[Test]
		public void TestFilterTextMethodWithAbnormalValues()
		{
            JourneyNoteFilter filter = new JourneyNoteFilter();

            // Notes should either be "displayed" or not dependent on the supplied note and the test data
            Assert.AreEqual(true, filter.DisplayNote(ModeType.Rail, "ea", false, string.Empty));
            Assert.AreEqual(false, filter.DisplayNote(ModeType.Rail, string.Empty, false, "This is test note 1"));
            Assert.AreEqual(false, filter.DisplayNote(ModeType.Rail, "ea", false, "This is test note 1"));
            Assert.AreEqual(false, filter.DisplayNote(ModeType.Ferry, "em", false, "THIS IS TEST NOTE FERRY EM"));
            Assert.AreEqual(true, filter.DisplayNote(ModeType.Ferry, "em", false, "This is test note ferry"));
        }


		/// <summary>
		/// Checks that data is reloaded successfully when the notification event fires.
		/// </summary>
		[Test]
		public void TestDataReload()
		{
            // Add change notification to TDServiceDiscovery
			TestMockDataChangeNotification dataChangeNotification = AddDataChangeNotification();

            // Add journey note filter to TDServiceDiscovery
            AddJourneyNoteFilter();

            // Get instance from TDServiceDiscovery
            JourneyNoteFilter filter = JourneyNoteFilter.Current;

			// Check values prior to change
            Assert.AreEqual(false, filter.DisplayNote(ModeType.Rail, "EA", false, "This is test note 1"));
			
			JourneyNoteFilterTestHelper.LoadTestData(JourneyNoteFilterTestDataSet.TestData2);

			// Check that data hasn't changed too early
            Assert.AreEqual(false, filter.DisplayNote(ModeType.Rail, "EA", false, "This is test note 1"), "Data changed too early");

			// Cause the Changed event to be raised by the notification service
            dataChangeNotification.RaiseChangedEvent("JourneyNoteFilter");

            // Get instance from TDServiceDiscovery
            filter = JourneyNoteFilter.Current;

			// Check that the data has changed
            Assert.AreEqual(true, filter.DisplayNote(ModeType.Rail, "EA", false, "This is test note 1"));
            Assert.AreEqual(false, filter.DisplayNote(ModeType.Rail, "EA", true, "This is test note 2"));
		}

		#endregion

		#region private helper methods

		/// <summary>
        /// Helper method. Adds the JourneyNoteFilter service to the TDServiceDiscovery cache.
		/// </summary>
        /// <returns>The newly created instance of JourneyNoteFilter</returns>
        private JourneyNoteFilter AddJourneyNoteFilter()
		{
            TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.JourneyNoteFilter, new JourneyNoteFilterFactory());
            return (JourneyNoteFilter)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyNoteFilter];
		}
		/// <summary>
		/// Adds the mock DataChangeNotification service to the cache
		/// </summary>
		/// <returns></returns>
		private TestMockDataChangeNotification AddDataChangeNotification()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.DataChangeNotification, new TestMockDataChangeNotification());
			return (TestMockDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];
		}

		#endregion
	}

	/// <summary>
	/// Enum for test data sets
	/// </summary>
    public enum JourneyNoteFilterTestDataSet { TestData1, TestData2 }

	#region Database helper class

	public sealed class JourneyNoteFilterTestHelper
    {
        #region Data helper methods

        private const string tempTablePrefix = "tempJourneyNoteFilterTestBackup";

		private const string JourneyNoteFilterTable = "JourneyNoteFilter";

        private JourneyNoteFilterTestHelper()
		{
		}

		public static void BackupCurrentData()
		{
            using (SqlHelper helper = new SqlHelper())
            {
                helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

                BackupTable(JourneyNoteFilterTable, helper);
            }
		}

		public static void RestoreOriginalData()
		{
            using (SqlHelper helper = new SqlHelper())
            {
                helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

                ClearTableDown(JourneyNoteFilterTable, helper);

                RestoreFromBackup(JourneyNoteFilterTable, helper);

                RemoveBackupTable(JourneyNoteFilterTable, helper);
            }
		}

		/// <summary>
		/// Drops the specified table.
		/// </summary>
		/// <param name="tableName">Name of the table to drop</param>
		/// <param name="connectedHelper"></param>
		private static void DropTable(string tableName, SqlHelper connectedHelper)
		{
			try
			{
				connectedHelper.Execute("drop table " + tableName);
			}
			catch (SqlException s)
			{
				// Allow a sql error with msg code 3701
				if (s.Number != 3701)
					throw;
			}
		}

		/// <summary>
		/// Backs up the specified table.
		/// </summary>
		/// <param name="tableName">Table to back up. Must not be temporary.</param>
		/// <param name="connectedHelper">An instance of SqlHelper with an open connection</param>
		private static void BackupTable(string tableName, SqlHelper connectedHelper)
		{
			string tempTableName = tempTablePrefix + tableName;
			DropTable(tempTableName, connectedHelper);
			connectedHelper.Execute(String.Format(CultureInfo.InvariantCulture, "select * into {0} from {1}", tempTableName, tableName));
		}

		/// <summary>
		/// Restores the data
		/// </summary>
		/// <param name="tableName">Table to back up. Must not be temporary.</param>
		/// <param name="connectedHelper">An instance of SqlHelper with an open connection</param>
		private static void RestoreFromBackup(string tableName, SqlHelper connectedHelper)
		{
			string tempTableName = tempTablePrefix + tableName;
			ClearTableDown(tableName, connectedHelper);
			connectedHelper.Execute(String.Format(CultureInfo.InvariantCulture, "insert into {0} select * from {1}", tableName, tempTableName));
		}

		/// <summary>
		/// Deletes the contents of the specified helper
		/// </summary>
		/// <param name="tableName">Table to back up. Must not be temporary.</param>
		/// <param name="connectedHelper">An instance of SqlHelper with an open connection</param>
		public static void ClearTableDown(string tableName, SqlHelper connectedHelper)
		{
			connectedHelper.Execute("delete " + tableName);
		}

		/// <summary>
		/// Restores the data
		/// </summary>
		/// <param name="tableName">Table to back up. Must not be temporary.</param>
		/// <param name="connectedHelper">An instance of SqlHelper with an open connection</param>
		private static void RemoveBackupTable(string tableName, SqlHelper connectedHelper)
		{
			string tempTableName = tempTablePrefix + tableName;
			DropTable(tempTableName, connectedHelper);
        }

        #endregion

        public static bool LoadTestData(JourneyNoteFilterTestDataSet testData)
		{
            using (SqlHelper helper = new SqlHelper())
            {
                helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

                ClearTableDown(JourneyNoteFilterTable, helper);

                string insertJourneyNoteFilter = "insert into JourneyNoteFilter (Mode,Region,AccessibleOnly,FilterText) values ('{0}', '{1}', {2}, '{3}')";

                //Insert test data
                if (testData.Equals(JourneyNoteFilterTestDataSet.TestData1))
                {
                    helper.Execute(String.Format(CultureInfo.InvariantCulture, insertJourneyNoteFilter, "All", "All", 0, "Test note 1"));
                    helper.Execute(String.Format(CultureInfo.InvariantCulture, insertJourneyNoteFilter, "Rail", "All", 0, "Test note rail"));
                    helper.Execute(String.Format(CultureInfo.InvariantCulture, insertJourneyNoteFilter, "Coach", "All", 0, "Test note coach"));
                    helper.Execute(String.Format(CultureInfo.InvariantCulture, insertJourneyNoteFilter, "All", "SW", 0, "Test note SW"));
                    helper.Execute(String.Format(CultureInfo.InvariantCulture, insertJourneyNoteFilter, "All", "SW|WM", 0, "Test note multi region"));
                    helper.Execute(String.Format(CultureInfo.InvariantCulture, insertJourneyNoteFilter, "Ferry", "EM", 0, "Test note ferry EM"));
                    helper.Execute(String.Format(CultureInfo.InvariantCulture, insertJourneyNoteFilter, "Metro", "All", 0, "Test note with space at end "));

                }
                else
                {
                    helper.Execute(String.Format(CultureInfo.InvariantCulture, insertJourneyNoteFilter, "All", "All", 1, "Test note 2"));
                }
            }

			return true;
						
		}

	}

	#endregion

	#region Initialisation class

	/// <summary>
	/// Initialisation class for Bay Text Filter test
	/// </summary>
	public class JourneyNoteFilterTestInitialisation : IServiceInitialisation
	{
        public JourneyNoteFilterTestInitialisation()
		{
		}

		// Enable PropertyService
		public void Populate(Hashtable serviceCache)
		{		
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
		}

	}

	#endregion
}
