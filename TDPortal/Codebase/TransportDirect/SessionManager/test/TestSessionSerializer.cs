using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.IO;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.UserPortal.StateServer;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.Resource;
using System.Xml;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Data;
using log4net;
using System.Reflection;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.SessionManager
{
    [TestFixture]
    public class TestSessionSerializer
    {
        #region Private members

        // Test Data
        private string TEST_DATA = Directory.GetCurrentDirectory() + "\\SessionManager\\JourneyResult1.xml";
        private TDStateServer tdStateServer;
        private TDSessionSerializer sessionStateSerializer;
        private TDSessionSerializer dbSessionSerializer;
        private string defferedKey = "resultStoreKey";
        private string testSessionID = "upgl0l55hohboh2evw5ew0br";
        private SqlConnection sqlConn;
        private SqlCommand sqlCmdRemoveASPSession;
        private ILog log;
        #endregion

        #region SetUp and TearDown
        /// <summary>
        /// Sets up the required resources for unit tests
        /// </summary>
        [TestFixtureSetUp]
        public void SetUp()
        {
            log = LogManager.GetLogger(typeof(TestSessionSerializer));
          

            // Intialise property service and td logging service. In production this will be performed in global.asax.
            // NB RBOServiceInitialisation will only be called once per test suite run despite being in SetUp
            TDServiceDiscovery.ResetServiceDiscoveryForTest();
            TDServiceDiscovery.Init(new TestSessionManagerInitialisation());

           
            tdStateServer = new TDStateServer("SessionSerializerTest");

            sessionStateSerializer = new TDSessionSerializer(tdStateServer);

            dbSessionSerializer = new TDSessionSerializer();

        }

        /// <summary>
        /// Releases the Resources.
        /// </summary>
        [TestFixtureTearDown]
        public void TearDown()
        {
            tdStateServer.DeleteFromStateServer(new string[] { defferedKey }, testSessionID);
            tdStateServer = null;
            sessionStateSerializer = null;
            dbSessionSerializer = null;

        }

        #endregion

        /// <summary>
        /// Test writing and reading data with session state
        /// </summary>
        [Test]
        public void TestSessionStateSerializerReadWrite()
        {
            long before = 0, after = 0;

            try
            {
                JourneyResult result = GetJourneyResult();
                DeferredKey dKey = new DeferredKey(defferedKey);

                System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();

                int iteration = int.Parse(Properties.Current["SessionSerializer.Iterations"]);

                System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcessesByName("soss_svr");

                System.Diagnostics.Process proc = null;

                if (procs.Length > 0)
                    proc = procs[0];
                before = proc.PrivateMemorySize64;
                for (int i = 0; i < iteration; i++)
                {
                    
                    
                    sessionStateSerializer.SerializeSessionObjectAndSave(GetSessionId(i), dKey, result);

                    sessionStateSerializer.RetrieveAndDeserializeSessionObject(GetSessionId(i), dKey);

                    
                }
                proc.Refresh();
                after = proc.PrivateMemorySize64;

                watch.Stop();

                this.LogInfo(log, MethodBase.GetCurrentMethod().Name, string.Format("sessionId,{0},key,{1},size,{2},time,{3},before,{4},after,{5}", testSessionID, defferedKey, "---", watch.ElapsedMilliseconds,before,after));


            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            Assert.IsTrue(true);
        }

        /// <summary>
        /// Tests writing and reading data to database session state
        /// </summary>
        [Test]
        public void TestDBSessionSerializerReadWrite()
        {
            long before = 0, after = 0;
            try
            {
                CreateDBSessions();

                JourneyResult result = GetJourneyResult();
                DeferredKey dKey = new DeferredKey(defferedKey);

                System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();

                int iteration = int.Parse(Properties.Current["SessionSerializer.Iterations"]);

                System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcessesByName("sqlservr");

                System.Diagnostics.Process proc = null;

                if (procs.Length > 0)
                    proc = procs[0];

                before = proc.PrivateMemorySize64;
                for (int i = 0; i < iteration; i++)
                {
                    

                    dbSessionSerializer.SerializeSessionObjectAndSave(GetSessionId(i), dKey, result);

                    dbSessionSerializer.RetrieveAndDeserializeSessionObject(GetSessionId(i), dKey);

                    
                }
                proc.Refresh();
                after = proc.PrivateMemorySize64;

                watch.Stop();

                this.LogInfo(log, MethodBase.GetCurrentMethod().Name, string.Format("sessionId,{0},key,{1},size,{2},time,{3},before,{4},after,{5}", testSessionID, defferedKey, "---", watch.ElapsedMilliseconds,before,after));


            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            Assert.IsTrue(true);
        }

        /// <summary>
        /// Create session id
        /// </summary>
        /// <param name="increment"></param>
        /// <returns></returns>
        private string GetSessionId(int increment)
        {
            string sessionId = testSessionID + increment.ToString("00000000");
            return sessionId;
        }

        /// <summary>
        /// Creates Session entries in database session
        /// </summary>
        private void CreateDBSessions()
        {
            int iterations = int.Parse(Properties.Current["SessionSerializer.Iterations"]);

            for (int i = 0; i < iterations; i++)
            {

                // Open connection
                sqlConn = new SqlConnection();
                sqlConn.ConnectionString = "data source=127.0.0.1;Trusted_Connection=Yes;Initial Catalog=ASPState";

                try
                {
                    sqlConn.Open();

                    // Ensure that data to be entered is not already present.
                    sqlCmdRemoveASPSession = new SqlCommand("DELETE FROM ASPStateTempSessions"
                        + " WHERE SessionId = '" + GetSessionId(i) + "'", sqlConn);
                    sqlCmdRemoveASPSession.CommandType = CommandType.Text;
                    sqlCmdRemoveASPSession.ExecuteNonQuery();

                    // Simulate entry into ASPStateTempSessions which would otherwise
                    // be done automatically by ASP.NET
                    SqlCommand sqlCmdAddASPSession = new SqlCommand("INSERT INTO ASPStateTempSessions"
                        + "(SessionId, Created, Expires, LockDate, LockDateLocal, LockCookie, Timeout, Locked) VALUES"
                        + "('" + GetSessionId(i) + "', getdate(), getdate(), getdate()+5, getdate(), 1, 10, 0 )", sqlConn);
                    sqlCmdAddASPSession.CommandType = CommandType.Text;
                    sqlCmdAddASPSession.ExecuteNonQuery();
                }
                finally
                {
                    sqlConn.Close();
                }
            }

        }

        /// <summary>
        /// Gets the Journey Result from the xml file
        /// </summary>
        /// <returns></returns>
        private JourneyResult GetJourneyResult()
        {
            XmlTextReader reader = new XmlTextReader(TEST_DATA);

            XmlSerializer serializer = new XmlSerializer(typeof(JourneyResult));

            return (JourneyResult)serializer.Deserialize(reader);
        }

        /// <summary>
        /// Logs the message to log4net
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="methodName"></param>
        /// <param name="infoMessage"></param>
        private void LogInfo(ILog logger, string methodName, string infoMessage)
        {
            if (logger.IsInfoEnabled)
            {
                if (!string.IsNullOrEmpty(infoMessage))
                {
                    logger.Info(string.Format("{0} :: {1}", methodName, infoMessage));
                }
                else
                {
                    logger.Info(string.Format("{0}", methodName));
                }
            }
        }





    }
}
