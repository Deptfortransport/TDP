//********************************************************************************
//NAME         : TestCoachOperatorLookup.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 12/10/2005
//DESCRIPTION  : Test class for TestCoachOperatorLookup. 
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFares/TestCoachOperatorLookup.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:38   mturner
//Initial revision.
//
//   Rev 1.5   Feb 09 2006 11:31:56   RWilby
//Updated to fix unit test
//
//   Rev 1.4   Nov 07 2005 13:20:56   mguney
//Singleton structure included for local coachOperatorLookup.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Nov 01 2005 17:20:50   mguney
//Constant names changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Oct 14 2005 09:55:54   mguney
//FareProviderURL changed to FareProviderUrl
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 14 2005 08:52:14   mguney
//Associated SCR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 14 2005 08:51:14   mguney
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
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// Summary description for TestCoachOperatorLookup.
	/// </summary>
	[TestFixture]
	public class TestCoachOperatorLookup
	{
		private CoachOperatorLookup coachOperatorLookup;

		public TestCoachOperatorLookup()
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
			TDServiceDiscovery.Init(new CoachOperatorLookupTestInitialisation());
			TDServiceDiscovery.Current.SetServiceForTest(
				ServiceDiscoveryKey.DataChangeNotification, new TestMockDataChangeNotification());

			CoachOperatorLookupTestHelper.BackupCurrentData();							
		}

		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TearDown]
		public void TearDown() 
		{ 
			CoachOperatorLookupTestHelper.RestoreOriginalData();
		}
		#endregion

		#region Tests
		
		/// <summary>
		/// This method is going to be used in multiple test methods.
		/// </summary>
		[Explicit]
		public void TestDataSet1()
		{
			CoachOperatorLookup coachOperatorLookup = GetCoachOperatorLookup();			
			//check operator codes
			Assert.AreEqual("NX",coachOperatorLookup.GetOperatorDetails("AB").OperatorCode,"Invalid lookup for operator codes...");
			Assert.AreEqual("NX",coachOperatorLookup.GetOperatorDetails("CD").OperatorCode,"Invalid lookup for operator codes...");
			Assert.AreEqual("SC",coachOperatorLookup.GetOperatorDetails("IJ").OperatorCode,"Invalid lookup for operator codes...");
			Assert.AreEqual("SC",coachOperatorLookup.GetOperatorDetails("KL").OperatorCode,"Invalid lookup for operator codes...");
			//check interfaces
			Assert.AreEqual(CoachFaresInterfaceType.ForRoute,coachOperatorLookup.GetOperatorDetails("AB").InterfaceType,"Invalid lookup for interfaces...");
			Assert.AreEqual(CoachFaresInterfaceType.ForJourney,coachOperatorLookup.GetOperatorDetails("KL").InterfaceType,"Invalid lookup for interfaces...");
			//check urls
			Assert.AreEqual("http://TESTNX",coachOperatorLookup.GetOperatorDetails("AB").FareProviderUrl,"Invalid lookup for url...");
			Assert.AreEqual(string.Empty,coachOperatorLookup.GetOperatorDetails("KL").FareProviderUrl,"Invalid lookup for url...");
		}

		/// <summary>
		/// Checks that GetOperatorDetails method works correctly for TEST1 dataset
		/// </summary>
		[Test]
		public void TestGetOperatorDetails()
		{
			//Load Data set 1 and notify
			CoachOperatorLookupTestHelper.LoadTestData(TestDataSet.TestData1);			
			TestMockDataChangeNotification dataChangeNotification = GetDataChangeNotification();
			dataChangeNotification.RaiseChangedEvent("CoachOperatorLookup");
			//Test
			TestDataSet1();
		}


		/// <summary>
		/// Checks that GetOperatorDetails method works correctly with Abnormal vales
		/// </summary>
		[Test]
		public void TestGetOperatorDetailsWithAbnormalValues()
		{
			CoachOperatorLookup coachOperatorLookup = GetCoachOperatorLookup();
			//Load Data set 1 and notify
			CoachOperatorLookupTestHelper.LoadTestData(TestDataSet.TestData1);			
			TestMockDataChangeNotification dataChangeNotification = GetDataChangeNotification();
			dataChangeNotification.RaiseChangedEvent("CoachOperatorLookup");
			
			try
			{
				Assert.AreEqual("NX",coachOperatorLookup.GetOperatorDetails("ab").OperatorCode,"Invalid lookup for operator codes...");
				Assert.AreEqual(CoachFaresInterfaceType.ForJourney,coachOperatorLookup.GetOperatorDetails("kl").InterfaceType,"Invalid lookup for interfaces...");
				Assert.AreEqual("http://TESTNX",coachOperatorLookup.GetOperatorDetails("cd").FareProviderUrl,"Invalid lookup for url...");

				Assert.IsNull(coachOperatorLookup.GetOperatorDetails("zz"));
				Assert.IsNull(coachOperatorLookup.GetOperatorDetails(string.Empty));
			}			
			catch (System.NullReferenceException)
			{
				Assert.Fail("Coach operator lookup failed for invalid entry.");
			}
		}


		/// <summary>
		/// Checks that data is reloaded successfully when the notification event fires.
		/// </summary>
		[Test]
		public void TestDataReload()
		{
			CoachOperatorLookup coachOperatorLookup = GetCoachOperatorLookup();
			//Load Data set 1 and notify
			CoachOperatorLookupTestHelper.LoadTestData(TestDataSet.TestData1);			
			TestMockDataChangeNotification dataChangeNotification = GetDataChangeNotification();
			dataChangeNotification.RaiseChangedEvent("CoachOperatorLookup");
			//check values prior to change
			TestDataSet1();//includes assert statements
			//Load new data
			CoachOperatorLookupTestHelper.LoadTestData(TestDataSet.TestData2);
			// Check that data hasn't changed too early
			TestDataSet1(); //This method should still raise no errors as the change event hasn't been raised			
			// Cause the Changed event to be raised by the notification service
			dataChangeNotification.RaiseChangedEvent("CoachOperatorLookup");

			// Check that the data has changed
			//check operator codes
			Assert.AreEqual("SC",coachOperatorLookup.GetOperatorDetails("AB").OperatorCode,"Invalid lookup for operator codes...");
			Assert.AreEqual("NX",coachOperatorLookup.GetOperatorDetails("CD").OperatorCode,"Invalid lookup for operator codes...");
			Assert.AreEqual("SC",coachOperatorLookup.GetOperatorDetails("IJ").OperatorCode,"Invalid lookup for operator codes...");
			Assert.AreEqual("NX",coachOperatorLookup.GetOperatorDetails("KL").OperatorCode,"Invalid lookup for operator codes...");
			//check interfaces
			Assert.AreEqual(CoachFaresInterfaceType.ForRoute,coachOperatorLookup.GetOperatorDetails("AB").InterfaceType,"Invalid lookup for interfaces...");
			Assert.AreEqual(CoachFaresInterfaceType.ForJourney,coachOperatorLookup.GetOperatorDetails("KL").InterfaceType,"Invalid lookup for interfaces...");
			//check urls
			Assert.AreEqual("http://TESTSC",coachOperatorLookup.GetOperatorDetails("AB").FareProviderUrl,"Invalid lookup for url...");
			Assert.AreEqual(string.Empty,coachOperatorLookup.GetOperatorDetails("KL").FareProviderUrl,"Invalid lookup for url...");			
		}

		#endregion

		#region private helper methods

		/// <summary>
		/// Returns the current operator lookup.
		/// </summary>
		/// <returns>Current CoachOperatorLookup</returns>
		private CoachOperatorLookup GetCoachOperatorLookup()
		{
			if (coachOperatorLookup == null)
				coachOperatorLookup = new CoachOperatorLookup(); //(CoachOperatorLookup)TDServiceDiscovery.Current[ServiceDiscoveryKey.CoachOperatorLookup];
			return coachOperatorLookup;
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

	/// <summary>
	/// Enum for test data sets
	/// </summary>
	public enum TestDataSet { TestData1, TestData2}

	#region Database helper class
	public sealed class CoachOperatorLookupTestHelper
	{
		private const string tempTablePrefix = "tempTestBackup";
		private const string CoachOperatorCodesTable = "CoachOperatorCodes";
		private const string CoachOperatorDetailsTable = "CoachOperatorDetails";

		private CoachOperatorLookupTestHelper()
		{
		}

		public static void BackupCurrentData()
		{
			SqlHelper helper = new SqlHelper();

			helper.ConnOpen(SqlHelperDatabase.DefaultDB);

			BackupTable(CoachOperatorCodesTable, helper);
			BackupTable(CoachOperatorDetailsTable, helper);

			helper.ConnClose();
		}

		public static void RestoreOriginalData()
		{
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.DefaultDB);

			ClearTableDown(CoachOperatorCodesTable, helper);
			ClearTableDown(CoachOperatorDetailsTable, helper);

			RestoreFromBackup(CoachOperatorDetailsTable, helper);
			RestoreFromBackup(CoachOperatorCodesTable, helper);
			
			RemoveBackupTable(CoachOperatorCodesTable, helper);
			RemoveBackupTable(CoachOperatorDetailsTable, helper);

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

		private static void InsertIntoOperatorCodes(SqlHelper helper,string cjpOperatorCode,string tdOperatorCode)
		{
			string coachOperatorCodesTable = 
				"insert into CoachOperatorCodes (CJPOperatorCode,TDOperatorCode) values ('{0}', '{1}')";
			helper.Execute(String.Format(CultureInfo.InvariantCulture, coachOperatorCodesTable, 
				cjpOperatorCode, tdOperatorCode));
		}

		private static void InsertIntoOperatorDetails(SqlHelper helper,string tdOperatorCode,
			string interfaceType,string operatorName,string url)
		{
			string coachOperatorDetailsTable = 
				"insert into CoachOperatorDetails (TDOperatorCode,InterfaceType,OperatorName,FareProviderURL) " +
				"values ('{0}', '{1}', '{2}', '{3}')";
			helper.Execute(String.Format(CultureInfo.InvariantCulture, coachOperatorDetailsTable, 
				tdOperatorCode,interfaceType,operatorName,url));
		}

		public static bool LoadTestData(TestDataSet testData)
		{
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.DefaultDB);

			ClearTableDown(CoachOperatorCodesTable, helper);
			ClearTableDown(CoachOperatorDetailsTable, helper);			
	
			//Insert test data
			if(testData.Equals(TestDataSet.TestData1))
			{
				InsertIntoOperatorDetails(helper,"NX","R","Natioanal Express","http://TESTNX");
				InsertIntoOperatorDetails(helper,"SC","J","Scottish City Link","");

				InsertIntoOperatorCodes(helper,"AB","NX");
				InsertIntoOperatorCodes(helper,"CD","NX");
				InsertIntoOperatorCodes(helper,"EF","NX");
				InsertIntoOperatorCodes(helper,"GH","NX");
				InsertIntoOperatorCodes(helper,"IJ","SC");
				InsertIntoOperatorCodes(helper,"KL","SC");
				InsertIntoOperatorCodes(helper,"NX","NX");
				InsertIntoOperatorCodes(helper,"SC","SC");								
			}
			else
			{
				InsertIntoOperatorDetails(helper,"NX","J","Natioanal Express","");
				InsertIntoOperatorDetails(helper,"SC","R","Scottish City Link","http://TESTSC");
				InsertIntoOperatorDetails(helper,"PM","J","P M","");

				InsertIntoOperatorCodes(helper,"AB","SC");
				InsertIntoOperatorCodes(helper,"CD","NX");
				InsertIntoOperatorCodes(helper,"EF","SC");
				InsertIntoOperatorCodes(helper,"GH","NX");
				InsertIntoOperatorCodes(helper,"IJ","SC");
				InsertIntoOperatorCodes(helper,"KL","NX");
				InsertIntoOperatorCodes(helper,"MN","PM");
				InsertIntoOperatorCodes(helper,"OP","PM");
				InsertIntoOperatorCodes(helper,"NX","NX");
				InsertIntoOperatorCodes(helper,"SC","SC");
				InsertIntoOperatorCodes(helper,"PM","PM");												
			}

			helper.ConnClose();

			return true;
						
		}

	}

	#endregion

	#region Initialisation class

	/// <summary>
	/// Initialisation class for Coach Operator Lookup test
	/// </summary>
	public class CoachOperatorLookupTestInitialisation : IServiceInitialisation
	{
		public CoachOperatorLookupTestInitialisation()
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

			//serviceCache.Add(ServiceDiscoveryKey.CoachOperatorLookup, new CoachOperatorLookup());
		}

	}

	#endregion
}


