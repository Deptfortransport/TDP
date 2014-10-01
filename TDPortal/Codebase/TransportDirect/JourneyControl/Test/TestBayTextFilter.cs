// *********************************************** 
// NAME			: TestBayTestFilter.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 2005/07/20
// DESCRIPTION	: Class testing the funcationality of BayTextFilter
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestBayTextFilter.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:12   mturner
//Initial revision.
//
//   Rev 1.1   Aug 09 2005 16:02:38   RWilby
//Added //$Log comment to file header
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
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

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for TestBayTextFilter.
	/// </summary>
	[TestFixture]
	public class TestBayTextFilter
	{
		public TestBayTextFilter()
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
			TDServiceDiscovery.Init(new BayTextFilterTestInitialisation());

			BayTextFilterTestHelper.BackupCurrentData();
			BayTextFilterTestHelper.LoadTestData(TestDataSet.TestData1);

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
			BayTextFilterTestHelper.RestoreOriginalData();
		}
		#endregion

		#region Tests
		/// <summary>
		/// Checks that FilterText method works correctly
		/// </summary>
		[Test]
		public void TestFilterTextMethod()
		{
			BayTextFilter bayTextFilter = new BayTextFilter();
		
			Assert.AreEqual(true,bayTextFilter.FilterText("EA"),"Filter Text for Traveline EA incorrect");
			Assert.AreEqual(false,bayTextFilter.FilterText("EM"),"Filter Text for Traveline EM incorrect");
			Assert.AreEqual(false,bayTextFilter.FilterText("L"),"Filter Text for Traveline L incorrect");
			Assert.AreEqual(true,bayTextFilter.FilterText("NE"),"Filter Text for Traveline NE incorrect");
			Assert.AreEqual(false,bayTextFilter.FilterText("NW"),"Filter Text for Traveline NW incorrect");
			Assert.AreEqual(true,bayTextFilter.FilterText("S"),"Filter Text for Traveline S incorrect");
			Assert.AreEqual(false,bayTextFilter.FilterText("SE"),"Filter Text for Traveline SE incorrect");
			Assert.AreEqual(false,bayTextFilter.FilterText("SW"),"Filter Text for Traveline SW incorrect");
			Assert.AreEqual(true,bayTextFilter.FilterText("W"),"Filter Text for Traveline W incorrect");
			Assert.AreEqual(false,bayTextFilter.FilterText("WM"),"Filter Text for Traveline WM incorrect");
		    Assert.AreEqual(true,bayTextFilter.FilterText("Y"),"Filter Text for Traveline Y incorrect");
		}


		/// <summary>
		/// Checks that FilerText method works correctly with Abnormal vales
		/// </summary>
		[Test]
		public void TestFilterTextMethodWithAbnormalValues()
		{
			BayTextFilter bayTextFilter = new BayTextFilter();
		
			Assert.AreEqual(true,bayTextFilter.FilterText("ea"),"Filter Text for Traveline ea incorrect");
			Assert.AreEqual(false,bayTextFilter.FilterText("Em"),"Filter Text for Traveline Em incorrect");
			Assert.AreEqual(true,bayTextFilter.FilterText(string.Empty),"Filter Text for Traveline string.Empty incorrect");
		}


		/// <summary>
		/// Checks that data is reloaded successfully when the notification event fires.
		/// </summary>
		[Test]
		public void TestDataReload()
		{
			TestMockDataChangeNotification dataChangeNotification = AddDataChangeNotification();
			
			BayTextFilter bayTextFilter = new BayTextFilter();

			// Check values prior to change
			Assert.AreEqual(true,bayTextFilter.FilterText("EA"),"Filter Text for Traveline EA incorrect");
			Assert.AreEqual(false,bayTextFilter.FilterText("EM"),"Filter Text for Traveline EM incorrect");
			Assert.AreEqual(false,bayTextFilter.FilterText("L"),"Filter Text for Traveline L incorrect");
			Assert.AreEqual(true,bayTextFilter.FilterText("NE"),"Filter Text for Traveline NE incorrect");
			Assert.AreEqual(false,bayTextFilter.FilterText("NW"),"Filter Text for Traveline NW incorrect");
			Assert.AreEqual(true,bayTextFilter.FilterText("S"),"Filter Text for Traveline S incorrect");
			Assert.AreEqual(false,bayTextFilter.FilterText("SE"),"Filter Text for Traveline SE incorrect");
			Assert.AreEqual(false,bayTextFilter.FilterText("SW"),"Filter Text for Traveline SW incorrect");
			Assert.AreEqual(true,bayTextFilter.FilterText("W"),"Filter Text for Traveline W incorrect");
			Assert.AreEqual(false,bayTextFilter.FilterText("WM"),"Filter Text for Traveline WM incorrect");
			Assert.AreEqual(true,bayTextFilter.FilterText("Y"),"Filter Text for Traveline Y incorrect");


			BayTextFilterTestHelper.LoadTestData(TestDataSet.TestData2);

			// Check that data hasn't changed too early
			Assert.AreEqual(true,bayTextFilter.FilterText("EA"),"Data changed too early - BayTextFilter incorrect");
			Assert.AreEqual(true,bayTextFilter.FilterText("S"),"Data changed too early - BayTextFilter incorrect");
			Assert.AreEqual(false,bayTextFilter.FilterText("WM"),"Data changed too early - BayTextFilter incorrect");


			// Cause the Changed event to be raised by the notification service
			dataChangeNotification.RaiseChangedEvent("BayTextFilter");

			// Check that the data has changed
			Assert.AreEqual(false,bayTextFilter.FilterText("EA"),"Filter Text for Traveline EA incorrect");
			Assert.AreEqual(false,bayTextFilter.FilterText("EM"),"Filter Text for Traveline EM incorrect");
			Assert.AreEqual(false,bayTextFilter.FilterText("L"),"Filter Text for Traveline L incorrect");
			Assert.AreEqual(false,bayTextFilter.FilterText("NE"),"Filter Text for Traveline NE incorrect");
			Assert.AreEqual(false,bayTextFilter.FilterText("NW"),"Filter Text for Traveline NW incorrect");
			Assert.AreEqual(false,bayTextFilter.FilterText("S"),"Filter Text for Traveline S incorrect");
			Assert.AreEqual(true,bayTextFilter.FilterText("SE"),"Filter Text for Traveline SE incorrect");
			Assert.AreEqual(true,bayTextFilter.FilterText("SW"),"Filter Text for Traveline SW incorrect");
			Assert.AreEqual(true,bayTextFilter.FilterText("W"),"Filter Text for Traveline W incorrect");
			Assert.AreEqual(true,bayTextFilter.FilterText("WM"),"Filter Text for Traveline WM incorrect");
			Assert.AreEqual(true,bayTextFilter.FilterText("Y"),"Filter Text for Traveline Y incorrect");
		}

		#endregion

		#region private helper methods
		/// <summary>
		/// Helper method. Adds the BayTextFilter service to the TDServiceDiscovery cache.
		/// </summary>
		/// <returns>The newly created instance of BayTextFilter</returns>
		private BayTextFilter AddBayTextFilter()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.BayTextFilter, new BayTextFilter());
			return (BayTextFilter)TDServiceDiscovery.Current[ServiceDiscoveryKey.BayTextFilter];
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
	public enum TestDataSet { TestData1, TestData2}

	#region Database helper class
	public sealed class BayTextFilterTestHelper
	{
		private const string tempTablePrefix = "tempTestBackup";

		private const string BayTextFilterTable = "baytextfilter";

		private BayTextFilterTestHelper()
		{
		}

		public static void BackupCurrentData()
		{
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.DefaultDB);

			BackupTable(BayTextFilterTable, helper);
		}

		public static void RestoreOriginalData()
		{
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.DefaultDB);

			ClearTableDown(BayTextFilterTable, helper);

			RestoreFromBackup(BayTextFilterTable, helper);
	
			RemoveBackupTable(BayTextFilterTable, helper);
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


		public static bool LoadTestData(TestDataSet testData)
		{
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.DefaultDB);

			ClearTableDown(BayTextFilterTable, helper);

			string insertBayTextFilter = "insert into baytextfilter (TravelineRegion,Displayable) values ('{0}', '{1}')";
	
			//Insert test data
			if(testData.Equals(TestDataSet.TestData1))
			{
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "EA", "Y"));
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "EM", "N"));
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "L", "N"));
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "NE", "Y"));
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "NW", "N"));
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "S", "Y"));
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "SE", "N"));
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "SW", "N"));
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "W", "Y"));
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "WM", "N"));
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "Y", "Y"));
			}
			else
			{
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "EA", "N"));
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "EM", "N"));
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "L", "N"));
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "NE", "N"));
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "NW", "N"));
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "S", "N"));
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "SE", "Y"));
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "SW", "Y"));
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "W", "Y"));
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "WM", "Y"));
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertBayTextFilter, "Y", "Y"));
			}

			helper.ConnClose();

			return true;
						
		}

	}

	#endregion

	#region Initialisation class

	/// <summary>
	/// Initialisation class for Bay Text Filter test
	/// </summary>
	public class BayTextFilterTestInitialisation : IServiceInitialisation
	{
		public BayTextFilterTestInitialisation()
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
