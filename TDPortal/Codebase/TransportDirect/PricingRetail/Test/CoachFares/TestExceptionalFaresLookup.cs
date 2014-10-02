//********************************************************************************
//NAME         : TestExceptionalFaresLookup.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 12/10/2005
//DESCRIPTION  : Test class for TestExceptionalFaresLookup. 
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/CoachFares/TestExceptionalFaresLookup.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:20   mturner
//Initial revision.
//
//   Rev 1.0   Nov 28 2005 16:17:48   mguney
//Initial revision.

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

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// Summary description for TestExceptionalFaresLookup.
	/// </summary>
	[TestFixture]
	public class TestExceptionalFaresLookup
	{
		private ExceptionalFaresLookup exceptionalFaresLookup;
		internal const string EXCEPTIONALFARE1 = "route 60 1";
		internal const string EXCEPTIONALFARE2 = "route 60 2";
		internal const string EXCEPTIONALFARE3 = "route 60 3";
		internal const string EXCEPTIONALFARE4 = "day return 1";
		internal const string EXCEPTIONALFARE5 = "day return 2";
		internal const string EXCEPTIONALFARE6 = "day return 3";


		public TestExceptionalFaresLookup()
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
			TDServiceDiscovery.Init(new ExceptionalFaresLookupTestInitialisation());
			TDServiceDiscovery.Current.SetServiceForTest(
				ServiceDiscoveryKey.DataChangeNotification, new TestMockDataChangeNotification());

			ExceptionalFaresLookupTestHelper.BackupCurrentData();							
		}

		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TearDown]
		public void TearDown() 
		{ 
			ExceptionalFaresLookupTestHelper.RestoreOriginalData();
		}
		#endregion

		#region Tests
		
		/// <summary>
		/// This method is going to be used in multiple test methods.
		/// </summary>
		[Explicit]
		private void TestDataSet1()
		{
			ExceptionalFaresLookup exceptionalFaresLookup = GetExceptionalFaresLookup();			
			//check actions for fare types
			Assert.AreEqual(ExceptionalFaresAction.Exclude,
				exceptionalFaresLookup.GetExceptionalFaresAction(EXCEPTIONALFARE1),
				"Invalid action for " + EXCEPTIONALFARE1);
			Assert.AreEqual(ExceptionalFaresAction.Exclude,
				exceptionalFaresLookup.GetExceptionalFaresAction(EXCEPTIONALFARE2),
				"Invalid action for " + EXCEPTIONALFARE2);
			Assert.AreEqual(ExceptionalFaresAction.Exclude,
				exceptionalFaresLookup.GetExceptionalFaresAction(EXCEPTIONALFARE3),
				"Invalid action for " + EXCEPTIONALFARE3);
			Assert.AreEqual(ExceptionalFaresAction.DayReturn,
				exceptionalFaresLookup.GetExceptionalFaresAction(EXCEPTIONALFARE4),
				"Invalid action for " + EXCEPTIONALFARE4);
			Assert.AreEqual(ExceptionalFaresAction.DayReturn,
				exceptionalFaresLookup.GetExceptionalFaresAction(EXCEPTIONALFARE5),
				"Invalid action for " + EXCEPTIONALFARE5);
			Assert.AreEqual(ExceptionalFaresAction.DayReturn,
				exceptionalFaresLookup.GetExceptionalFaresAction(EXCEPTIONALFARE6),
				"Invalid action for " + EXCEPTIONALFARE6);
		}

		/// <summary>
		/// Checks that GetExceptionalFaresAction method works correctly for TEST1 dataset
		/// </summary>
		[Test]
		public void TestGetExceptionalFaresAction()
		{
			//Load Data set 1 and notify
			ExceptionalFaresLookupTestHelper.LoadTestData(TestDataSet.TestData1);			
			TestMockDataChangeNotification dataChangeNotification = GetDataChangeNotification();
			dataChangeNotification.RaiseChangedEvent(ExceptionalFaresLookup.DATACHANGENOTIFICATIONGROUP);
			//Test
			TestDataSet1();
		}


		/// <summary>
		/// Checks that GetExceptionalFaresAction method works correctly with Abnormal vales
		/// </summary>
		[Test]
		public void TestGetExceptionalFaresActionWithAbnormalValues()
		{
			ExceptionalFaresLookup exceptionalFaresLookup = GetExceptionalFaresLookup();
			//Load Data set 1 and notify
			ExceptionalFaresLookupTestHelper.LoadTestData(TestDataSet.TestData1);			
			TestMockDataChangeNotification dataChangeNotification = GetDataChangeNotification();
			dataChangeNotification.RaiseChangedEvent(ExceptionalFaresLookup.DATACHANGENOTIFICATIONGROUP);
						
			Assert.AreEqual(ExceptionalFaresAction.NotFound,
				exceptionalFaresLookup.GetExceptionalFaresAction("????"),
				"Invalid action for ????.");
			Assert.AreEqual(ExceptionalFaresAction.NotFound,
				exceptionalFaresLookup.GetExceptionalFaresAction(string.Empty),
				"Invalid action for empty string.");
		}


		/// <summary>
		/// Checks that data is reloaded successfully when the notification event fires.
		/// </summary>
		[Test]
		public void TestDataReload()
		{
			ExceptionalFaresLookup exceptionalFaresLookup = GetExceptionalFaresLookup();
			//Load Data set 1 and notify
			ExceptionalFaresLookupTestHelper.LoadTestData(TestDataSet.TestData1);			
			TestMockDataChangeNotification dataChangeNotification = GetDataChangeNotification();
			dataChangeNotification.RaiseChangedEvent(ExceptionalFaresLookup.DATACHANGENOTIFICATIONGROUP);
			//check values prior to change
			TestDataSet1();//includes assert statements
			//Load new data
			ExceptionalFaresLookupTestHelper.LoadTestData(TestDataSet.TestData2);
			// Check that data hasn't changed too early
			TestDataSet1(); //This method should still raise no errors as the change event hasn't been raised			
			// Cause the Changed event to be raised by the notification service
			dataChangeNotification.RaiseChangedEvent(ExceptionalFaresLookup.DATACHANGENOTIFICATIONGROUP);

			// Check that the data has changed
			//check actions for fare types
			Assert.AreEqual(ExceptionalFaresAction.DayReturn,
				exceptionalFaresLookup.GetExceptionalFaresAction(EXCEPTIONALFARE1),
				"Invalid action for " + EXCEPTIONALFARE1);
			Assert.AreEqual(ExceptionalFaresAction.Exclude,
				exceptionalFaresLookup.GetExceptionalFaresAction(EXCEPTIONALFARE2),
				"Invalid action for " + EXCEPTIONALFARE2);
			Assert.AreEqual(ExceptionalFaresAction.DayReturn,
				exceptionalFaresLookup.GetExceptionalFaresAction(EXCEPTIONALFARE3),
				"Invalid action for " + EXCEPTIONALFARE3);
			Assert.AreEqual(ExceptionalFaresAction.Exclude,
				exceptionalFaresLookup.GetExceptionalFaresAction(EXCEPTIONALFARE4),
				"Invalid action for " + EXCEPTIONALFARE4);
			Assert.AreEqual(ExceptionalFaresAction.DayReturn,
				exceptionalFaresLookup.GetExceptionalFaresAction(EXCEPTIONALFARE5),
				"Invalid action for " + EXCEPTIONALFARE5);
			Assert.AreEqual(ExceptionalFaresAction.Exclude,
				exceptionalFaresLookup.GetExceptionalFaresAction(EXCEPTIONALFARE6),
				"Invalid action for " + EXCEPTIONALFARE6);			
		}

		#endregion

		#region private helper methods

		/// <summary>
		/// Returns the current exceptional fares lookup.
		/// </summary>
		/// <returns>Current ExceptionalFaresLookup</returns>
		private ExceptionalFaresLookup GetExceptionalFaresLookup()
		{
			if (exceptionalFaresLookup == null)
				exceptionalFaresLookup = new ExceptionalFaresLookup(); 
			return exceptionalFaresLookup;
		}

		/// <summary>
		/// Gets the mock DataChangeNotification service from the cache
		/// </summary>
		/// <returns></returns>
		private TestMockDataChangeNotification GetDataChangeNotification()
		{			
			return (TestMockDataChangeNotification)
				TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];
		}
		#endregion
	}


	#region Database helper class
	public sealed class ExceptionalFaresLookupTestHelper
	{
		private const string tempTablePrefix = "tempTestBackup";
		private const string ExceptionalFaresTable = "ExceptionalFares";
		

		private ExceptionalFaresLookupTestHelper()
		{
		}

		public static void BackupCurrentData()
		{
			SqlHelper helper = new SqlHelper();

			helper.ConnOpen(SqlHelperDatabase.DefaultDB);

			BackupTable(ExceptionalFaresTable, helper);			

			helper.ConnClose();
		}

		public static void RestoreOriginalData()
		{
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.DefaultDB);

			ClearTableDown(ExceptionalFaresTable, helper);			

			RestoreFromBackup(ExceptionalFaresTable, helper);			
			
			RemoveBackupTable(ExceptionalFaresTable, helper);

			helper.ConnClose();
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
				//Cannot drop, it doesn't exist
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

		private static void InsertIntoExceptionalFares(SqlHelper helper,string fareType,string action)
		{
			string exceptionalFaresTableInsertSql = 
				"insert into {0} (FareType,Action) values ('{1}', '{2}')";
			helper.Execute(String.Format(CultureInfo.InvariantCulture, exceptionalFaresTableInsertSql, 
				ExceptionalFaresTable,fareType, action));
		}		

		public static bool LoadTestData(TestDataSet testData)
		{
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.DefaultDB);

			ClearTableDown(ExceptionalFaresTable, helper);					
	
			//Insert test data
			if(testData.Equals(TestDataSet.TestData1))
			{
				InsertIntoExceptionalFares(helper,TestExceptionalFaresLookup.EXCEPTIONALFARE1,
					ExceptionalFaresLookup.ACTIONEXCLUDE);
				InsertIntoExceptionalFares(helper,TestExceptionalFaresLookup.EXCEPTIONALFARE2,
					ExceptionalFaresLookup.ACTIONEXCLUDE);
				InsertIntoExceptionalFares(helper,TestExceptionalFaresLookup.EXCEPTIONALFARE3,
					ExceptionalFaresLookup.ACTIONEXCLUDE);
				InsertIntoExceptionalFares(helper,TestExceptionalFaresLookup.EXCEPTIONALFARE4,
					ExceptionalFaresLookup.ACTIONDAYRETURN);
				InsertIntoExceptionalFares(helper,TestExceptionalFaresLookup.EXCEPTIONALFARE5,
					ExceptionalFaresLookup.ACTIONDAYRETURN);
				InsertIntoExceptionalFares(helper,TestExceptionalFaresLookup.EXCEPTIONALFARE6,
					ExceptionalFaresLookup.ACTIONDAYRETURN);
			}
			else
			{
				InsertIntoExceptionalFares(helper,TestExceptionalFaresLookup.EXCEPTIONALFARE1,
					ExceptionalFaresLookup.ACTIONDAYRETURN);
				InsertIntoExceptionalFares(helper,TestExceptionalFaresLookup.EXCEPTIONALFARE2,
					ExceptionalFaresLookup.ACTIONEXCLUDE);
				InsertIntoExceptionalFares(helper,TestExceptionalFaresLookup.EXCEPTIONALFARE3,
					ExceptionalFaresLookup.ACTIONDAYRETURN);
				InsertIntoExceptionalFares(helper,TestExceptionalFaresLookup.EXCEPTIONALFARE4,
					ExceptionalFaresLookup.ACTIONEXCLUDE);
				InsertIntoExceptionalFares(helper,TestExceptionalFaresLookup.EXCEPTIONALFARE5,
					ExceptionalFaresLookup.ACTIONDAYRETURN);
				InsertIntoExceptionalFares(helper,TestExceptionalFaresLookup.EXCEPTIONALFARE6,
					ExceptionalFaresLookup.ACTIONEXCLUDE);												
			}

			helper.ConnClose();

			return true;
						
		}

	}

	#endregion

	#region Initialisation class

	/// <summary>
	/// Initialisation class for ExceptionalFaresLookup test
	/// </summary>
	public class ExceptionalFaresLookupTestInitialisation : IServiceInitialisation
	{
		public ExceptionalFaresLookupTestInitialisation()
		{
		}

		// Enable PropertyService
		public void Populate(Hashtable serviceCache)
		{		
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			

			ArrayList errors = new ArrayList();
			
			try
			{
				// create custom email publisher
				IEventPublisher[] customPublishers = new IEventPublisher[0];	
				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
			}
			catch (TDException tdEx)
			{
				// create message string
				StringBuilder message = new StringBuilder(100);
				message.Append(tdEx.Message); // prepend with existing exception message

				// append all messages returned by TDTraceListener constructor
				foreach( string error in errors )
				{
					message.Append(error);
					message.Append(" ");	
				}

				// log message using .NET default trace listener
				Trace.WriteLine(tdEx.Message);			

				// rethrow exception - use the initial exception id as the id
				throw new Exception(message.ToString());
			}
			
		}

	}

	#endregion
}


