// *********************************************** 
// NAME                 : TestLoadedOperationalEvent.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 01/07/2004
// DESCRIPTION  : Test class for LoadedOperationalEvent
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/EventDataLoader/Test/TestLoadedOperationalEvent.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:38:32   mturner
//Initial revision.
//
//   Rev 1.3   Feb 08 2005 16:13:24   bflenk
//Changed Assertion to Assert
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
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using NUnit.Framework;
namespace TransportDirect.ReportDataProvider.EventDataLoader
{
	/// <summary>
	/// Test class for LoadedOperationalEvent
	/// </summary>
	[TestFixture]
	public class TestLoadedOperationalEvent
	{
		/// <summary>
		/// Setup to be performed before each test
		/// </summary>
		[SetUp]
		public void Init()
		{
		}

		/// <summary>
		/// Tests that the object is created successfully when all parameters are as
		/// expected
		/// </summary>
		[Test]
		public void TestConstructorWithValidData()
		{
			DateTime time = DateTime.Now;
			string sessionId = "fghdhjd832738djj282";
			string message = "This is the message";
			TDEventCategory category = TDEventCategory.Database;
			TDTraceLevel level = TDTraceLevel.Verbose;
			string machineName = "localhost";
			string typeName = "TransportDirect.ReportDataProvider.EventDataLoader.OperationalEventFilter";
			string methodName = "IsMatch";
			string assemblyName = "td.reportdataprovider.eventdataloader";
			
			LoadedOperationalEvent oe = new LoadedOperationalEvent(time, sessionId, message, category.ToString(), level.ToString(), machineName, typeName, methodName, assemblyName);

			Assert.AreEqual(time, oe.Time, "oe.Time not as expected");
			Assert.AreEqual(sessionId, oe.SessionId, "oe.SessionId not as expected");
			Assert.AreEqual(message, oe.Message, "oe.Message not as expected");
			Assert.AreEqual(category, oe.Category, "oe.Category not as expected");
			Assert.AreEqual(level, oe.Level, "oe.Level not as expected");
			Assert.AreEqual(machineName, oe.MachineName, "oe.MachineName not as expected");
			Assert.AreEqual(typeName, oe.TypeName, "oe.TypeName not as expected");
			Assert.AreEqual(methodName, oe.MethodName, "oe.MethodName not as expected");
			Assert.AreEqual(assemblyName, oe.AssemblyName, "oe.AssemblyName not as expected");

		}

		/// <summary>
		/// Tests constructor when called with an invalid category
		/// </summary>
		[Test]
		public void TestConstructorWithInvalidCategory()
		{
			DateTime time = DateTime.Now;
			string sessionId = string.Empty;
			string message = string.Empty;
			string category = string.Empty;
			string level = string.Empty;
			string machineName = string.Empty;
			string typeName = string.Empty;
			string methodName = string.Empty;
			string assemblyName = string.Empty;
			
			try
			{
				LoadedOperationalEvent oe = new LoadedOperationalEvent(time, sessionId, message, category, level, machineName, typeName, methodName, assemblyName);
			}
			catch (TDException tde)
			{
				// Expect to see identifier TDExceptionIdentifier.EDLFailedConvertingTDEventCategory
				Assert.AreEqual(TDExceptionIdentifier.EDLFailedConvertingTDEventCategory, tde.Identifier, "Unexpected exception thrown");
			}
		}

		/// <summary>
		/// Tests constructor when called with an invalid level
		/// </summary>
		[Test]
		public void TestConstructorWithInvalidLevel()
		{
			DateTime time = DateTime.Now;
			string sessionId = string.Empty;
			string message = string.Empty;
			string category = TDEventCategory.Business.ToString();
			string level = string.Empty;
			string machineName = string.Empty;
			string typeName = string.Empty;
			string methodName = string.Empty;
			string assemblyName = string.Empty;
			
			try
			{
				LoadedOperationalEvent oe = new LoadedOperationalEvent(time, sessionId, message, category, level, machineName, typeName, methodName, assemblyName);
			}
			catch (TDException tde)
			{
				// Expect to see identifier TDExceptionIdentifier.EDLFailedConvertingTDEventCategory
				Assert.AreEqual(TDExceptionIdentifier.EDLFailedConvertingTDTraceLevel, tde.Identifier, "Unexpected exception thrown");
			}
		}

	}
}
