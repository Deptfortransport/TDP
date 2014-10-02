// *********************************************** 
// NAME                 : TestOperationalEventFilter.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 01/07/2004
// DESCRIPTION  : Test class for OperationalEventFilter
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/EventDataLoader/Test/TestOperationalEventFilter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:38:32   mturner
//Initial revision.
//
//   Rev 1.4   Feb 08 2005 16:45:08   bflenk
//Changed Assertions to Asserts
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
using TransportDirect.Common.Logging;
using NUnit.Framework;

namespace TransportDirect.ReportDataProvider.EventDataLoader
{
	/// <summary>
	/// Test class for OperationalEventFilter
	/// </summary>
	[TestFixture]
	public class TestOperationalEventFilter
	{
		/// <summary>
		/// Setup to be performed before each test
		/// </summary>
		[SetUp]
		public void Init()
		{
		}

		/// <summary>
		/// Tests filtering using a time period
		/// </summary>
		[Test]
		public void TestMatchAgainstTimePeriod()
		{
			//First try using maximum possible time range - should be a match
			OperationalEventFilter filter = new OperationalEventFilter(DateTime.MinValue, DateTime.MaxValue, OperationalEventFilterMethod.And);
			LoadedOperationalEvent loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "localhost", "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(filter.IsMatch(loe), "IsMatch returned false when maximum date/time range tested for");

			DateTime startTime = new DateTime(2004, 1, 2, 1, 0, 0);
			DateTime endTime = new DateTime(2004, 1, 2, 2, 0, 0);
			DateTime validTime = new DateTime(2004, 1, 2, 1, 30, 0);
			DateTime beforeStart = new DateTime(2004, 1, 1, 21, 0, 0);
			DateTime afterEnd = new DateTime(2004, 1, 2, 3, 0, 0);

			filter = new OperationalEventFilter(startTime, endTime, OperationalEventFilterMethod.And);

			// Test with valid time
			loe = new LoadedOperationalEvent(validTime, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "localhost", "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(filter.IsMatch(loe), "IsMatch failed when using time range and valid time");

			// Test on start time boundary
			loe = new LoadedOperationalEvent(startTime, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "localhost", "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(filter.IsMatch(loe), "IsMatch failed when using time range and valid time equal to start time");

			// Test on end time boundary
			loe = new LoadedOperationalEvent(endTime, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "localhost", "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(filter.IsMatch(loe), "IsMatch failed when using time range and valid time equal to end time");

			// Test with invalid time (prior to start)
			loe = new LoadedOperationalEvent(beforeStart, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "localhost", "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(!filter.IsMatch(loe), "IsMatch failed when using time range and invalid time before start time");

			// Test with invalid time (after end)
			loe = new LoadedOperationalEvent(afterEnd, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "localhost", "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(!filter.IsMatch(loe), "IsMatch failed when using time range and invalid time after end time");

		}

		/// <summary>
		/// Tests filtering against assembly names
		/// </summary>
		[Test]
		public void TestMatchAgainstAssemblyNames()
		{
			string filterAssemblyName = "Assembly1";
			string validAssemblyName = filterAssemblyName.ToLower(); // Test case insensitivity
			string invalidAssemblyName1 = "";
			string invalidAssemblyName2 = "TestAssembly";

			OperationalEventFilter filter = new OperationalEventFilter(filterAssemblyName, OperationalEventMatchField.AssemblyNameEquals, false, OperationalEventFilterMethod.And);

			LoadedOperationalEvent loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "localhost", "TypeName", "MethodName", filterAssemblyName);
			Assert.IsTrue(filter.IsMatch(loe), "IsMatch failed using assembly array and valid assembly");

			loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "localhost", "TypeName", "MethodName", validAssemblyName);
			Assert.IsTrue(filter.IsMatch(loe), "IsMatch failed case sensitivity test using assembly array");

			loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "localhost", "TypeName", "MethodName", invalidAssemblyName1);
			Assert.IsTrue(!filter.IsMatch(loe), "IsMatch failed test using assembly array and blank assembly name in LoadedOperationalEvent");

			loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "localhost", "TypeName", "MethodName", invalidAssemblyName2);
			Assert.IsTrue(!filter.IsMatch(loe), "IsMatch failed test using assembly array and invalid assembly name in LoadedOperationalEvent");
		}

		/// <summary>
		/// Tests matching against categories
		/// </summary>
		[Test]
		public void TestMatchAgainstCategories()
		{
			TDEventCategory filterCategory = TDEventCategory.Database;
			string validCategory1 = filterCategory.ToString(); // Basic test
			string validCategory2 = filterCategory.ToString().ToLower(); // Basic test
			string invalidCategory = TDEventCategory.ThirdParty.ToString();

			//First try using maximum possible time range - should be a match
			OperationalEventFilter filter = new OperationalEventFilter(filterCategory, OperationalEventFilterMethod.And);

			LoadedOperationalEvent loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", validCategory1, TDTraceLevel.Error.ToString(), "localhost", "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(filter.IsMatch(loe), "IsMatch failed using categories array and valid category");

			loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", validCategory2, TDTraceLevel.Error.ToString(), "localhost", "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(filter.IsMatch(loe), "IsMatch failed using categories array and valid category");

			loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", invalidCategory, TDTraceLevel.Error.ToString(), "localhost", "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(!filter.IsMatch(loe), "IsMatch failed using categories array and invalid category");
		}

		/// <summary>
		/// Tests filtering with trace levels
		/// </summary>
		[Test]
		public void TestMatchAgainstLevels()
		{
			TDTraceLevel filterLevel = TDTraceLevel.Error;
			string validLevel1 = filterLevel.ToString(); // Basic test
			string validLevel2 = filterLevel.ToString().ToLower(); // Basic test
			string invalidLevel = TDTraceLevel.Info.ToString();

			//First try using maximum possible time range - should be a match
			OperationalEventFilter filter = new OperationalEventFilter(filterLevel, OperationalEventFilterMethod.And);

			LoadedOperationalEvent loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), validLevel1, "localhost", "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(filter.IsMatch(loe), "IsMatch failed using categories array and valid category");

			loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), validLevel2, "localhost", "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(filter.IsMatch(loe), "IsMatch failed using categories array and valid category");

			loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), invalidLevel, "localhost", "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(!filter.IsMatch(loe), "IsMatch failed using categories array and invalid category");
		}

		/// <summary>
		/// Tests filtering using machine names
		/// </summary>
		[Test]
		public void TestMatchAgainstMachineNames()
		{
			string filterMachineName = "Machine1";
			string validMachineName1 = filterMachineName; // Basic test
			string validMachineName2 = filterMachineName.ToLower(); // Test case insensitivity
			string invalidMachineName1 = "";
			string invalidMachineName2 = "TestMachine";

			OperationalEventFilter filter = new OperationalEventFilter(filterMachineName, OperationalEventMatchField.MachineNameEquals, false, OperationalEventFilterMethod.And);

			LoadedOperationalEvent loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), validMachineName1, "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(filter.IsMatch(loe), "IsMatch failed using Machine array and valid Machine");

			loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), validMachineName2, "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(filter.IsMatch(loe), "IsMatch failed case sensitivity test using Machine array");

			loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), invalidMachineName1, "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(!filter.IsMatch(loe), "IsMatch failed test using Machine array and blank Machine name in LoadedOperationalEvent");

			loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), invalidMachineName2, "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(!filter.IsMatch(loe),"IsMatch failed test using Machine array and invalid Machine name in LoadedOperationalEvent");
		}

		/// <summary>
		/// Tests filtering using strings to search for in the message field of the event
		/// </summary>
		[Test]
		public void TestMatchAgainstMessageMatches()
		{
			string filterMessageMatchString = "request";
			string validMessage1 = filterMessageMatchString; // Basic test
			string validMessage3 = "some text including the word " + filterMessageMatchString + " in the message";
			string invalidMessage1 = "";
			string invalidMessage2 = "this text includes none of the specified words";

			OperationalEventFilter filter = new OperationalEventFilter(filterMessageMatchString, OperationalEventMatchField.MessageContains, false, OperationalEventFilterMethod.And);

			LoadedOperationalEvent loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", validMessage1, TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "MachineName", "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(filter.IsMatch(loe), "IsMatch failed using messages array and exact match in message string");

			loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", validMessage3, TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "MachineName", "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(filter.IsMatch(loe), "IsMatch failed using messages array and match as part of message string");

			loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", invalidMessage1, TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "MachineName", "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(!filter.IsMatch(loe), "IsMatch failed using messages array and empty message string");

			loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", invalidMessage2, TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "MachineName", "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(!filter.IsMatch(loe), "IsMatch failed using messages array and message string containing no matches");
		}

		/// <summary>
		/// Tests filtering using method names
		/// </summary>
		[Test]
		public void TestMatchAgainstMethodNames()
		{
			string filterMethodName = "Method1";
			string validMethodName1 = filterMethodName; // Basic test
			string validMethodName2 = filterMethodName.ToLower(); // Test case insensitivity
			string invalidMethodName1 = "";
			string invalidMethodName2 = "TestMethod";

			OperationalEventFilter filter = new OperationalEventFilter(filterMethodName, OperationalEventMatchField.MethodNameEquals, false, OperationalEventFilterMethod.And);

			LoadedOperationalEvent loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "MachineName", "TypeName", validMethodName1, "AssemblyName");
			Assert.IsTrue(filter.IsMatch(loe), "IsMatch failed using Method array and valid Method");

			loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "MachineName", "TypeName", validMethodName2, "AssemblyName");
			Assert.IsTrue(filter.IsMatch(loe), "IsMatch failed case sensitivity test using Method array");

			loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "MachineName", "TypeName", invalidMethodName1, "AssemblyName");
			Assert.IsTrue(!filter.IsMatch(loe), "IsMatch failed test using Method array and blank Method name in LoadedOperationalEvent");

			loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "MachineName", "TypeName", invalidMethodName2, "AssemblyName");
			Assert.IsTrue(!filter.IsMatch(loe), "IsMatch failed test using Method array and invalid Method name in LoadedOperationalEvent");
		}

		/// <summary>
		/// Tests filtering using session Ids
		/// </summary>
		[Test]
		public void TestMatchAgainstSessionIds()
		{
			string filterSessionId = "zuwrbb45p32fp3rs5ha1tnaj";
			string validSessionId1 = filterSessionId; // Basic test
			string validSessionId2 = filterSessionId.ToLower(); // Test case insensitivity
			string invalidSessionId1 = "";
			string invalidSessionId2 = "aasdab45p32fp3rs5ha1tnaj";

			OperationalEventFilter filter = new OperationalEventFilter(filterSessionId, OperationalEventMatchField.SessionIdEquals, false, OperationalEventFilterMethod.And);

			LoadedOperationalEvent loe = new LoadedOperationalEvent(DateTime.Now, validSessionId1, "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "MachineName", "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(filter.IsMatch(loe), "IsMatch failed using session Id array and valid session Id");

			loe = new LoadedOperationalEvent(DateTime.Now, validSessionId2, "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "MachineName", "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(filter.IsMatch(loe), "IsMatch failed case sensitivity test using session Id array");

			loe = new LoadedOperationalEvent(DateTime.Now, invalidSessionId1, "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "MachineName", "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(!filter.IsMatch(loe), "IsMatch failed test using session Id array and blank session Id in LoadedOperationalEvent");

			loe = new LoadedOperationalEvent(DateTime.Now, invalidSessionId2, "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "MachineName", "TypeName", "MethodName", "AssemblyName");
			Assert.IsTrue(!filter.IsMatch(loe), "IsMatch failed test using session Id array and invalid session Id in LoadedOperationalEvent");
		}

		/// <summary>
		/// Tests filtering using type names
		/// </summary>
		[Test]
		public void TestMatchAgainstTypeNames()
		{
			string filterTypeName = "Type1";
			string validTypeName1 = filterTypeName; // Basic test
			string validTypeName2 = filterTypeName.ToLower(); // Test case insensitivity
			string invalidTypeName1 = "";
			string invalidTypeName2 = "TestType";

			OperationalEventFilter filter = new OperationalEventFilter(filterTypeName, OperationalEventMatchField.TypeNameEquals, false, OperationalEventFilterMethod.And);

			LoadedOperationalEvent loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "MachineName", validTypeName1, "MethodName", "AssemblyName");
			Assert.IsTrue(filter.IsMatch(loe), "IsMatch failed using Type array and valid Type");

			loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "MachineName", validTypeName2, "MethodName", "AssemblyName");
			Assert.IsTrue(filter.IsMatch(loe), "IsMatch failed case sensitivity test using Type array");

			loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "MachineName", invalidTypeName1, "MethodName", "AssemblyName");
			Assert.IsTrue(!filter.IsMatch(loe), "IsMatch failed test using Type array and blank Type name in LoadedOperationalEvent");

			loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "MachineName", invalidTypeName2, "MethodName", "AssemblyName");
			Assert.IsTrue(!filter.IsMatch(loe), "IsMatch failed test using Type array and invalid Type name in LoadedOperationalEvent");
		}

		[Test]
		public void TestMatchWithSubFilters()
		{
			LoadedOperationalEvent loe = new LoadedOperationalEvent(DateTime.Now, "kjhfks898ujhfkjh", "This is the message", TDEventCategory.Business.ToString(), TDTraceLevel.Error.ToString(), "MachineName", "TypeName", "MethodName", "AssemblyName");
			
			// Operational event which filters on event category
			OperationalEventFilter filterCategory = new OperationalEventFilter(TDEventCategory.Business, OperationalEventFilterMethod.And);
			OperationalEventFilter filterLevel = new OperationalEventFilter(TDTraceLevel.Verbose, OperationalEventFilterMethod.AndNot);

			OperationalEventFilter combined = new OperationalEventFilter(new OperationalEventFilter[] { filterCategory, filterLevel }, OperationalEventFilterMethod.And);
            
			Assert.IsTrue(combined.IsMatch(loe), "IsMatch failed test using multiple filters");
		}

	}
}
