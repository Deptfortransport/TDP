// *********************************************** 
// NAME                 : TestDatabaseOperationalEventLoader.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 01/07/2004
// DESCRIPTION  : Test class for DatabaseOperationalEventLoader
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/EventDataLoader/Test/TestDatabaseOperationalEventLoader.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:38:30   mturner
//Initial revision.
//
//   Rev 1.5   Feb 17 2005 10:02:02   bflenk
//Database used for OperationEvent changed from defaultDB to ReportStagingDB
//
//   Rev 1.4   Feb 08 2005 16:12:56   bflenk
//Changed Assertion to Assert
//
//   Rev 1.3   Jul 12 2004 11:40:32   jgeorge
//Modified for filter changes
//
//   Rev 1.2   Jul 02 2004 10:06:32   jgeorge
//Added tests
//
//   Rev 1.1   Jul 01 2004 17:16:26   jgeorge
//Interim check-in
//
//   Rev 1.0   Jul 01 2004 15:58:24   jgeorge
//Initial revision.

using System;
using NUnit.Framework;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;

namespace TransportDirect.ReportDataProvider.EventDataLoader
{
	/// <summary>
	/// Test class for DatabaseOperationalEventLoader
	/// </summary>
	[TestFixture]
	public class TestDatabaseOperationalEventLoader
	{
		/// <summary>
		/// Setup to be performed before each test
		/// </summary>
		[SetUp]
		public void Init()
		{
			TDServiceDiscovery.Init(new TestInitialisations());
		}

		/// <summary>
		/// Tests to ensure that an
		/// </summary>
		[Test]
		public void TestConstructor()
		{
			try
			{
				IOperationalEventLoader oel = new DatabaseOperationalEventLoader(SqlHelperDatabase.ReportStagingDB);
			}
			catch (Exception e)
			{
				Assert.Fail("An exception of type [" + e.GetType().Name + "] occurred when creating the loader - message follows: " + e.Message);
			}
		}

		/// <summary>
		/// Calls GetEvents without a filter
		/// </summary>
		[Test]
		public void TestWithoutFilter()
		{
			// Work out how many operational events are expected
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.ReportStagingDB);
			int numberOfEvents = (int)helper.GetScalar("select count(1) from OperationalEvent");
			helper.ConnClose();

			// Get them
			IOperationalEventLoader oel = new DatabaseOperationalEventLoader(SqlHelperDatabase.ReportStagingDB);
			LoadedOperationalEvent[] data = oel.GetEvents();

			Assert.AreEqual(numberOfEvents, data.Length, "Number of LoadedOperationalEvents returned not as expected");

		}

		/// <summary>
		/// Calls GetEvents with a filter
		/// </summary>
		[Test]
		public void TestWithFilter()
		{
			OperationalEventFilter filterCategory = new OperationalEventFilter(TDEventCategory.Business, OperationalEventFilterMethod.Or);
			OperationalEventFilter filterLevel = new OperationalEventFilter(TDTraceLevel.Error, OperationalEventFilterMethod.Or);

			OperationalEventFilter combined = new OperationalEventFilter( new OperationalEventFilter[] { filterCategory, filterLevel }, OperationalEventFilterMethod.Or);

			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.ReportStagingDB);
			int numberOfEvents = (int)helper.GetScalar("select count(1) from OperationalEvent where Category = 'Business' or Level = 'Error'");
			helper.ConnClose();

			// Get them
			IOperationalEventLoader oel = new DatabaseOperationalEventLoader(SqlHelperDatabase.ReportStagingDB);
			LoadedOperationalEvent[] data = oel.GetEvents(combined);

			Assert.AreEqual(numberOfEvents, data.Length, "Number of LoadedOperationalEvents returned not as expected");
		}

		[Test]
		public void TestConvertFiltersToSql()
		{
			string sqlExpected1 = "( Category = 'Business')";
			string sqlExpected2 = "(" + sqlExpected1 + " AND (NOT Level = 'Verbose'))";
			string sqlExpected3 = "(" + sqlExpected2 + " OR ( Message LIKE '%requestid%'))";
			string sqlExpected4 = "( Message = 'id''s')";

			// Operational event which filters on event category
			OperationalEventFilter filterCategory = new OperationalEventFilter(TDEventCategory.Business, OperationalEventFilterMethod.And);
			OperationalEventFilter filterLevel = new OperationalEventFilter(TDTraceLevel.Verbose, OperationalEventFilterMethod.AndNot);
			OperationalEventFilter filterMessage = new OperationalEventFilter("requestid", OperationalEventMatchField.MessageContains, false, OperationalEventFilterMethod.Or);
			OperationalEventFilter filterMessage2 = new OperationalEventFilter("id's", OperationalEventMatchField.MessageEquals, false, OperationalEventFilterMethod.Or);

			OperationalEventFilter combined1 = new OperationalEventFilter(new OperationalEventFilter[] { filterCategory, filterLevel }, OperationalEventFilterMethod.And);
			OperationalEventFilter combined2 = new OperationalEventFilter(new OperationalEventFilter[] { combined1, filterMessage }, OperationalEventFilterMethod.And);

			DatabaseOperationalEventLoader oel = new DatabaseOperationalEventLoader(SqlHelperDatabase.ReportStagingDB);

            
			// Test with a single, standard filter
			Assert.AreEqual(sqlExpected1, oel.ConvertFiltersToSql(filterCategory), "Sql statement not as expected");

			// Test with a complex filter
			Assert.AreEqual(sqlExpected2, oel.ConvertFiltersToSql(combined1), "Sql statement not as expected");

			// Test with an even more complex filter!
			// Test with a complex filter
			Assert.AreEqual(sqlExpected3, oel.ConvertFiltersToSql(combined2), "Sql statement not as expected" );

			// Test escaping of special characters
			Assert.AreEqual(sqlExpected4, oel.ConvertFiltersToSql(filterMessage2), "Sql statement not as expected" );
		}
	}
}
