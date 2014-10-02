// ***********************************************
// NAME 		: TestTDSessionManager.cs
// AUTHOR 		: Andrew Windley
// DATE CREATED : 02/07/2003
// DESCRIPTION 	: NUnit test for TDSessionManager class.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TestTDSessionManager.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:46   mturner
//Initial revision.
//
//   Rev 1.9   Mar 21 2005 17:11:06   jgeorge
//FxCop changes
//
//   Rev 1.8   Feb 04 2005 11:22:12   RScott
//Updated Assertion to Assert
//
//   Rev 1.7   May 10 2004 15:11:28   RHopkins
//Extend Journey.
//Initial version of Itinerary Manager.
//
//   Rev 1.6   Mar 17 2004 17:58:22   CHosegood
//Added TestSessionManagerInitialisation
//
//   Rev 1.5   Sep 16 2003 16:13:28   PNorell
//Removed initialisation that did not belong there.
//Removed old test-code currently obsolete.
//
//   Rev 1.4   Sep 12 2003 09:51:28   jcotton
//To resolve compilation errors on main solutions
//
//   Rev 1.3   Sep 11 2003 12:05:34   jcotton
//Added bool HasRealSession to constructor to enable the test to use a real session object or a simple mock session object
//
//   Rev 1.2   Sep 11 2003 11:34:02   jcotton
//Test for saving Deferred data to session database. Code compiles but unit testing is still in progress.  Checkin is to enable solution syncronisation.
//
//   Rev 1.1   Jul 08 2003 15:58:28   AWindley
//Added comments
//
//   Rev 1.0   Jul 03 2003 17:31:30   AWindley
//Initial Revision

using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Resource;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// NUnit test for TDSessionManager class.
	/// </summary>
	/// <TestPlan>
	/// Test the stored procedures used to interact with ASPState..DeferredData table.
	/// </TestPlan>
	[TestFixture]
	public class TestTDSessionManager
	{
		private SqlConnection sqlConn;
		private SqlCommand sqlCmdRemoveASPSession;
		//private SqlDataReader rdr;
		private byte[] dataInsert = {84,67,69,82,73,68,32,84,82,79,80,83,78,65,82,84}; // TCERID TROPSNART
		private byte[] dataUpdate = {84,82,65,78,83,80,79,82,84,32,68,73,82,69,67,84}; // TRANSPORT DIRECT
		private byte[] dataOut = new Byte[16]; 
		private TestMockSessionManager testSessionManager = null;
		private string TestSessionID = "upgl0l55hohboh2evw5ew0br00000001";
		private bool TestAuth = true;
		private bool HasRealSession = false;

		/// <summary>
		/// Open SqlConnection and setup test data in database ASPState.
		/// </summary>
		[TestFixtureSetUp] 
		public void Init()
		{
            try 
            {
                // Intialise property service and td logging service. In production this will be performed in global.asax.
                // NB RBOServiceInitialisation will only be called once per test suite run despite being in SetUp
                TDServiceDiscovery.ResetServiceDiscoveryForTest();
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
		/// Remove test data from database ASPState and close SqlConnection 
		/// </summary>
		[TestFixtureTearDown] 
		public void Dispose()
		{ 
			
			// Remove data inserted into ASPState 
			// - automatically deletes related row in DeferredData
            try 
            {
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

			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}

		/// <summary>
		/// Tests that OnPreUnload persists Deferred Data. (Test Data Insert)
		/// </summary>
		[Test]
		public void TestPreUnload()
		{
			testSessionManager.OnPreUnload();
			string sSQL = "SELECT Count([SessionId]) FROM [ASPState].[dbo].[DeferredData] WHERE [SessionId] = '" + TestSessionID + "'";
			Assert.IsTrue((CheckDatabase( sSQL, QueryType.Scaler ) > 0), "Records written by TestPreUnload to the Deferred database, were not found.");
		}

		/// <summary>
		/// Test Data Update
		/// </summary>
		[Test]
		public void TestDeferredDataUpdate()
		{
			testSessionManager.OnPreUnload();
			testSessionManager.OnPreUnload();
			string sSQL = "SELECT Count([SessionId]) FROM [ASPState].[dbo].[DeferredData] WHERE [SessionId] = '" + TestSessionID + "'";
			Assert.AreEqual(2, CheckDatabase( sSQL, QueryType.Scaler ), "The wrong number of records were found.");
		}


		private int CheckDatabase( string currentSQL, QueryType queryMode )
		{
			int result = -1;

			sqlConn = new SqlConnection();
			sqlConn.ConnectionString = "data source=127.0.0.1;Trusted_Connection=Yes;Initial Catalog=ASPState";
		
			SqlCommand sqlCmd = new SqlCommand( currentSQL, sqlConn);
			sqlCmd.CommandType = CommandType.Text;

			// Open connection and insert
			sqlConn.Open();
			
			switch( queryMode )
			{
				case QueryType.NonQuery:
					sqlCmd.ExecuteNonQuery();
					break;
				
				case QueryType.Reader:
					sqlCmd.ExecuteReader();
					break;
				
				case QueryType.Scaler:
					result = (int) sqlCmd.ExecuteScalar();
					break;
			}
			return result;
		}

		public enum QueryType
		{
			NonQuery,
			Reader,
			Scaler
		}
	}
	
}
