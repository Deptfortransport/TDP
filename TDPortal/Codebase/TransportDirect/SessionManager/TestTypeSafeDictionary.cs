// ***********************************************
// NAME 		: TestTypeSafeDictionary.cs
// AUTHOR 		: Andrew Windley
// DATE CREATED : 07/07/2003
// DESCRIPTION 	: NUnit test for TypeSafeDictionary class.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TestTypeSafeDictionary.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:46   mturner
//Initial revision.
//
//   Rev 1.6   Mar 21 2005 17:11:06   jgeorge
//FxCop changes
//
//   Rev 1.5   Feb 08 2005 11:34:58   RScott
//Assertions changes to Asserts
//
//   Rev 1.4   May 10 2004 15:11:30   RHopkins
//Extend Journey.
//Initial version of Itinerary Manager.
//
//   Rev 1.3   Apr 05 2004 12:14:00   jgeorge
//Updated to use TestMockSessionManager instead of the real thing, as the real thing requires an Http Session
//
//   Rev 1.2   Sep 17 2003 16:19:26   PNorell
//Updated test and interface.
//
//   Rev 1.1   Jul 17 2003 13:29:44   MTurner
//Added teardown method that forces garbage collection.
//
//   Rev 1.0   Jul 08 2003 16:06:24   AWindley
//Initial Revision

using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using NUnit.Framework;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// NUnit test for TypeSafeDictionary class.
	/// </summary>
	/// <TestPlan>
	/// Test to ensure that FormShift data can be stored and retrieved.
	/// </TestPlan>
	[TestFixture]
	public class TestTypeSafeDictionary
	{
		private SqlConnection sqlConn;
		private SqlCommand sqlCmdRemoveASPSession;
		private TestMockSessionManager testSessionManager = null;
		private string TestSessionID = "upgl0l55hohboh2evw5ew0br00000001";
		private bool TestAuth = true;
		private bool HasRealSession = false;


		/// <summary>
		/// Instantiate a test TDSessionManager
		/// </summary>
		[SetUp] 
		public void Init()
		{ 
			try 
			{
				// Intialise property service and td logging service. In production this will be performed in global.asax.
				// NB RBOServiceInitialisation will only be called once per test suite run despite being in SetUp
				TDServiceDiscovery.Init(new TestSessionManagerInitialisation() );

				// Open connection
				sqlConn = new SqlConnection();
				sqlConn.ConnectionString = "data source=127.0.0.1;Trusted_Connection=Yes;Initial Catalog=ASPState";
				sqlConn.Open();

				// Ensure that data to be entered is not already present.
				sqlCmdRemoveASPSession = new SqlCommand("DELETE FROM ASPStateTempSessions"
					+ " WHERE SessionId = '" + TestSessionID + "'", sqlConn);
				sqlCmdRemoveASPSession.CommandType = CommandType.Text;
				sqlCmdRemoveASPSession.ExecuteNonQuery();

				// Simulate entry into ASPStateTempSessions which would otherwise
				// be done automatically by ASP.NET
				SqlCommand sqlCmdAddASPSession = new SqlCommand("INSERT INTO ASPStateTempSessions"
					+ "(SessionId, Created, Expires, LockDate, LockCookie, Timeout, Locked) VALUES"
					+ "('" + TestSessionID + "', getdate(), getdate()+5, getdate(), 1, 10, 0 )", sqlConn);
				sqlCmdAddASPSession.CommandType = CommandType.Text;
				sqlCmdAddASPSession.ExecuteNonQuery();

				//TestSessionID
				// Build the mock session manager.
				TestMockSessionBuilder testSessionBuilder = new TestMockSessionBuilder( TestAuth, TestSessionID, HasRealSession );

				// Assign the mock session manager.
				testSessionManager = testSessionBuilder.GetTestMockSessionManager; 
			} 
			catch ( SqlException sqle ) 
			{
				Trace.WriteLine( sqle.Message );
			}
		}
 
		/// <summary>
		/// Cleans up the instantiated test TDSessionManager by removing all data
		/// that had been added to the hashtable and then forcing Garbage Collection.
		/// </summary>
		[TearDown]
		public void CleanUp()
		{
			// Remove data inserted into ASPState 
			// - automatically deletes related row in DeferredData
			try 
			{
				testSessionManager.FormShift.Clear();
				System.GC.Collect();

				sqlCmdRemoveASPSession = new SqlCommand("DELETE FROM ASPStateTempSessions"
					+ " WHERE SessionId = '" + TestSessionID + "'", sqlConn);
				sqlCmdRemoveASPSession.CommandType = CommandType.Text;
				sqlCmdRemoveASPSession.ExecuteNonQuery();

				// Close connection
				sqlConn.Close();
			} 
			catch ( SqlException sqle ) 
			{
				Trace.WriteLine ( sqle.Message );
			}
		}
    
		/// <summary>
		/// Test that each of the currently available key types
		/// can be correctly stored and retrieved via the 
		/// TypeSafeDictionary in TDSessionManager.
		/// </summary>
		[Test]
		public void InsertRetrieveFormShiftData()
		{ 

			// Instantiate a test key for each key type available
			IntKey TestIntKey = new IntKey("TestIntKey");
			StringKey TestStrKey = new StringKey("TestStrKey");
			DoubleKey TestDblKey = new DoubleKey("TestDblKey");
			DateKey TestDteKey = new DateKey("TestDteKey");
						
			// For each key type store data in FormShift
			testSessionManager.FormShift[TestIntKey] = 10;
			testSessionManager.FormShift[TestStrKey] = "Test string";
			testSessionManager.FormShift[TestDblKey] = 123.45;
			testSessionManager.FormShift[TestDteKey] = DateTime.MaxValue;
						
			// Asserts
			Assert.AreEqual(10, testSessionManager.FormShift[TestIntKey]);
			Assert.AreEqual("Test string", testSessionManager.FormShift[TestStrKey]);
			Assert.AreEqual(123.45, testSessionManager.FormShift[TestDblKey]);
			Assert.AreEqual(DateTime.MaxValue, testSessionManager.FormShift[TestDteKey]);
		}
	}
}