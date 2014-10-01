// ***********************************************
// NAME 		: TestDataChangeNotification.cs
// AUTHOR 		: Rob Greenwood
// DATE CREATED : 15/06/2004
// DESCRIPTION 	: Test functionality of the DataChangeNotification class.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataServices/Test/TestDataChangeNotification.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:20:56   mturner
//Initial revision.
//
//   Rev 1.6   Aug 05 2005 16:01:30   jgeorge
//Added code to set file to read/write before trying to update it.
//
//   Rev 1.5   Feb 07 2005 17:11:42   bflenk
//Changed Assertion to Assert
//
//   Rev 1.4   Jun 24 2004 13:33:50   rgreenwood
//Updated unit tests, all test now working.
//
//   Rev 1.3   Jun 18 2004 15:15:48   rgreenwood
//Work in progress.
//
//   Rev 1.2   Jun 16 2004 17:46:26   rgreenwood
//Work in progress
//
//   Rev 1.1   Jun 16 2004 09:24:08   CHosegood
//Updated tests
//
//   Rev 1.0   Jun 15 2004 14:57:18   CHosegood
//Initial revision.

using System;
using System.Collections;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Xml;
using NUnit.Framework;
using System.Configuration;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.DatabaseInfrastructure;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.DataServices
{
	/// <summary>
	/// Test functionality of the DataChangeNotification class.
	/// </summary>
	[TestFixture]
	public class TestDataChangeNotification
	{
        #region ChangeNotificationClient inner class
        /// <summary>
        /// A "pseudo" client of the data notification service
        /// </summary>
        private class ChangeNotificationClient
        {
            /// <summary>
            /// A list of groups that the listener
            /// </summary>
            private IList groups = null;

			private int eventRaisedCount = 0;

            /// <summary>
            /// 
            /// </summary>
            private DataChangeNotification dataNotification = null;

            /// <summary>
            /// Affected Groups
            /// </summary>
            public IList AffectedGroups 
            {
                get 
                {
                    //Create the list if it doesn't exist
                    if ( groups == null )
                        this.groups = new ArrayList();
                    return this.groups; 
                }
                set { this.groups = value; }
            }

			/// <summary>
			/// Returns the number of times the event has been raised since the object was created.
			/// </summary>
			public int EventRaisedCount
			{
				get { return eventRaisedCount; }
			}

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public void OnChange(object sender, ChangedEventArgs e) 
            {
                if ( TDTraceSwitch.TraceVerbose ) 
                {
                    Logger.WriteLine( new OperationalEvent( TDEventCategory.Database, TDTraceLevel.Verbose,
                        "Event received from" + sender.ToString() ));

                    Logger.WriteLine( new OperationalEvent( TDEventCategory.Database, TDTraceLevel.Verbose,
                        "Data notification event received for group: " + e.GroupId ));
                }

				eventRaisedCount++;

                //Create the list if it doesn't exist
                if ( groups == null )
                    groups = new ArrayList();

                //If we haven't already added this events groupd ID to the
                //list then add it
                if ( !groups.Contains( e.GroupId ) ) 
                {
                    if ( TDTraceSwitch.TraceVerbose ) 
                    {
                        Logger.WriteLine( new OperationalEvent( TDEventCategory.Database, TDTraceLevel.Verbose,
                            string.Format( "Adding group [{0}] to list of Data notification event groups received",e.GroupId) ));
                    }
                    groups.Add( e.GroupId );
                } 
                else 
                {
                    //Log that we have already received an event for this group
                    if ( TDTraceSwitch.TraceVerbose ) 
                    {
                        Logger.WriteLine( new OperationalEvent( TDEventCategory.Database, TDTraceLevel.Verbose,
                            "Received another Data notification event for group: " + e.GroupId ));
                    }
                }
            }


            /// <summary>
            /// Default constructor
            /// </summary>
            public ChangeNotificationClient() 
            {
                dataNotification = new DataChangeNotification();
                dataNotification.Changed += new ChangedEventHandler(this.OnChange);
            }
        }
    #endregion

        #region Static Members
        /// <summary>
        /// SQL statment used to change the test1 group
        /// </summary>
        public static readonly string SQL_INSERT = @"INSERT INTO ChangeNotification(Version, [Table]) VALUES(1,'Table1')";

        /// <summary>
        /// SQL statment used to restore the test1 group
        /// </summary>
        public static readonly string SQL_DELETE = @"DELETE * FROM ChangeNotification WHERE Version = 1";
        #endregion

        #region Setup/Teardown methods
        /// <summary>
        /// Responsible for performing setup tasks to complete before every
        /// test is run
        /// e.g.  Opening db connections, moving files...
        /// </summary>
        [TestFixtureSetUp]
        public void init() 
        {
			// Initialize Services
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestInitialization());
        }


        #endregion

        #region Constructor
        /// <summary>
        /// Default construcctor
        /// </summary>
		public TestDataChangeNotification()
		{ }

        #endregion

        #region Tests
        /// <summary>
        /// Modify the database and makes sure that the correct
        /// groups are notified
        /// </summary>
        [Test]
        public void TestDataChangedSuccess() 
        {

			// Setup Properties and Database with valid values, Table and Stored Proc
			TestDataChangeNotificationHelper.SetupTest6();

			// Set PropertyService
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			
			SqlHelper helper = null;
            ChangeNotificationClient notificationClient = null;


            try 
            {
                //Create a new "event handler"
                notificationClient = new ChangeNotificationClient();

                //Create a SqlHelper
                helper = new SqlHelper();

                //Open the database
                Logger.WriteLine( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    "Opening database connection" ) );
                //string[] databases = Properties.Current["DataServices.DataNotification.PollingInterval"].Split(',');
                //helper.ConnOpen( databases[0] );
                helper.ConnOpen( SqlHelperDatabase.DefaultDB );


                //modify some data
                Logger.WriteLine( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    "Execute sql insert" ) );
                helper.Execute( SQL_INSERT );

                Logger.WriteLine( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    "Closing database connection" ) );
                helper.ConnClose();

                Logger.WriteLine( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    "Thread about to sleep" ) );
                //Wait for twice the refresh period to ensure that the new values are picked up
                Thread.Sleep( Int32.Parse(Properties.Current["DataServices.DataNotification.PollingInterval"])
                *1000 *2 );
                Logger.WriteLine( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    "Thread awake" ) );

                ArrayList expectedAffectedGroups = new ArrayList();
                expectedAffectedGroups.Add( "TestGroup1" );

                //check event was fired
				Assert.AreEqual(1, notificationClient.EventRaisedCount,"The listener did not receive the expected number of events");

				//check groups notified
                Assert.AreEqual(expectedAffectedGroups.Count ,notificationClient.AffectedGroups.Count, "DataChangeNotification did not notify the correct groups");
				foreach (string curr in expectedAffectedGroups)
					Assert.IsTrue(notificationClient.AffectedGroups.Contains(curr), "DataChangeNotification did not notify the correct groups - " + curr + " - was missed");

			}
            catch ( Exception e ) 
            {
                Logger.WriteLine( new OperationalEvent( TDEventCategory.Database, TDTraceLevel.Error,
                    "Exception encountered:" + e.Message, e ));
                throw e;
            }
            finally 
            {
                if ( helper.ConnIsOpen ) 
                {
                    Logger.WriteLine( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                        "Closing database connection" ) );
                    helper.ConnClose();
                }
            }

			// Tidy up
			TestDataChangeNotificationHelper.TidyUp();
        }

        /// <summary>
        /// Modify the database and makes sure that the correct
        /// groups are notified
        /// </summary>
        [Test]
        public void TestDataNotChangedSuccess() 
        {
			// Setup Properties and Database with valid values, Table and Stored Proc
			TestDataChangeNotificationHelper.SetupTest6();

			// Initialize Services
			TDServiceDiscovery.Init(new TestInitialization());

			// Set PropertyService
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

            ChangeNotificationClient notificationClient = null;

            try 
            {
                //Create a new "event handler"
                notificationClient = new ChangeNotificationClient();

                Logger.WriteLine( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    "Thread about to sleep" ) );
                //Wait for twice the refresh period to ensure that the new values are picked up
                Thread.Sleep( Int32.Parse(Properties.Current["DataServices.DataNotification.PollingInterval"])
                    *1000 *2 );
                Logger.WriteLine( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    "Thread awake" ) );

                //check event was fired
				Assert.AreEqual(0, notificationClient.EventRaisedCount, "The listener did not receive the expected number of events");

				//check groups notified
				Assert.AreEqual(0, notificationClient.AffectedGroups.Count, "DataChangeNotification did not notify the correct groups");

            }
            catch ( Exception e ) 
            {
                Logger.WriteLine( new OperationalEvent( TDEventCategory.Database, TDTraceLevel.Error,
                    "Exception encountered:" + e.Message, e ));
                Assert.Fail("Exception encountered: " + e.Message);
            }

			// Tidy up
			TestDataChangeNotificationHelper.TidyUp();
        }

		#region Error-Handling Tests

		/// <summary>
		/// Tests that an ArgumentNullException is thrown by the DataChangeNotificaton
		/// class if it cannot find a PollingInterval property at initialisation.
		/// </summary>
		[Test]
		public void TestPollingIntervalNotFound()
		{
			bool errorRaised = false;

			// Setup Database so No PollingInterval, Groups, Database or Tables properties exist
			TestDataChangeNotificationHelper.SetupTest1();
			
			// Initialize Services
			TDServiceDiscovery.Init(new TestInitialization());

			// Set PropertyService
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

			try
			{
				// Initialise DataChangeNotification class
				TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.DataChangeNotification, new DataChangeNotificationFactory());

			}
			catch ( TDException e)
			{
				if (e.Identifier == TDExceptionIdentifier.DSPollingIntervalNotFound)
					errorRaised = true;
	
			}

			Assert.AreEqual(true, errorRaised, "Expected DSPollingIntervalNotFound TDException was not raised.");
		}

		
		/// <summary>
		/// Tests that an ArgumentNullException is thrown by the DataChangeNotificaton
		/// class if it cannot find a Groups property at initialisation.
		/// </summary>
		[Test]
		public void TestGroupsNotFound()
		{
			bool errorRaised = false;

			// Setup Database so valid PollingIntervalexists, but no Groups, Database or Tables properties exist
			TestDataChangeNotificationHelper.SetupTest2();

			// Initialise Services
			TDServiceDiscovery.Init(new TestInitialization());
			
			// Set PropertyService
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

			try
			{
				// Initialise DataChangeNotification class
				TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.DataChangeNotification, new DataChangeNotificationFactory());

			}
			catch ( TDException e)
			{
				if (e.Identifier == TDExceptionIdentifier.DSGroupsNotFound)
					errorRaised = true;
	
			}

			Assert.AreEqual(true, errorRaised, "Expected DSGroupsNotFound TDException was not raised.");
		}

        
		/// <summary>
		/// Tests that the error DSDatabaseNamePropertyNotValid is thrown if the Properties table does
		/// not contain a database name property.
        /// </summary>
		[Test]
		public void TestDatabaseNamePropertyNotPresent()
		{
			bool errorRaised = false;

			// Setup Database so valid PollingInterval & Groups exist but no Database or Tables properties exist
			TestDataChangeNotificationHelper.SetupTest3();
			
			// Initialize Services
			TDServiceDiscovery.Init(new TestInitialization());

			// Set PropertyService
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

			try
			{
				// Initialise DataChangeNotification class
				TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.DataChangeNotification, new DataChangeNotificationFactory());

			}
			catch ( TDException e)
			{
				if (e.Identifier == TDExceptionIdentifier.DSDatabaseNamePropertyNotPresent)
					errorRaised = true;
	
			}

			Assert.AreEqual( true, errorRaised, "Expected DSDatabaseNamePropertyNotPresent TDException was not raised.");

		}

		
		/// <summary> 
		/// Tests that the error DSDatabaseNamePropertyNotValid is thrown if the Properties table contains
		/// a database name property that cannot be parsed into a matching enum type held in SqlHelper.
		/// </summary>
		[Test]
		public void TestDSDatabaseNamePropertyNotValid()
		{
			bool errorRaised = false;

			// Setup Database so valid PollingInterval & Groups exist, invalid Database name exists, but no Tables properties exist
			TestDataChangeNotificationHelper.SetupTest4();
			
			// Initialize Services
			TDServiceDiscovery.Init(new TestInitialization());

			// Set PropertyService
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

			try
			{
				// Initialise DataChangeNotification class
				TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.DataChangeNotification, new DataChangeNotificationFactory());
			}
			catch ( TDException e)
			{
				if (e.Identifier == TDExceptionIdentifier.DSDatabaseNamePropertyNotValid)
					errorRaised = true;
			}

			Assert.AreEqual(true, errorRaised, "Expected DSDatabaseNamePropertyNotValid TDException was not raised.");
		}

		
	
		/// <summary>
		/// Tests that the DSStoredProcedureNotPresent error is thrown if the 'GetChangeTable'
		/// stored procedure is not present in the database.
		/// </summary>
		[Test]
        public void TestStoredProcedureNotPresent() 
        {
			bool errorRaised = false;

			// Setup Properties for valid PollingInterval, Groups, Database and Tables
			// Setup Database to ensure there is no Stored Proc called "GetChangeTables" present
			TestDataChangeNotificationHelper.SetupTest5();
			
			// Initialize Services
			TDServiceDiscovery.Init(new TestInitialization());

			// Set PropertyService
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());


			try
			{
				// Initialise DataChangeNotification class
				TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.DataChangeNotification, new DataChangeNotificationFactory());
			}
			catch ( TDException e)
			{
				if (e.Identifier == TDExceptionIdentifier.DSStoredProcedureNotPresent)
					errorRaised = true;
			}
			Assert.AreEqual(true, errorRaised, "Expected TDException was not raised.");

			// Tidy up
			TestDataChangeNotificationHelper.TidyUp();

        }

        #endregion
        #endregion



	}


	/// <summary>
	/// This sealed class enables the properties.xml file and database conditions required by these tests
	/// to be customised for each individual test, in order to exercise the DataChangeNotification error-handling code.
	/// Some tests can be carried out with just the xml file, others need database access to check for the
	/// presence of tables and stored procedures.
	/// </summary>
	public sealed class TestDataChangeNotificationHelper
	{
		private static XmlDocument getPropertiesXml()
		{
			string fileName = (string)ConfigurationManager.AppSettings["propertyservice.providers.fileprovider.filepath"];
			FileAttributes attribs = File.GetAttributes(fileName);
			if ((attribs & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
				File.SetAttributes(fileName, attribs - (int)FileAttributes.ReadOnly);

			XmlDocument propertiesDoc = new XmlDocument();
			propertiesDoc.Load(fileName);
			return propertiesDoc;
		}

		private static void savePropertiesXml(XmlDocument doc)
		{
			string fileName = (string)ConfigurationManager.AppSettings["propertyservice.providers.fileprovider.filepath"];
			doc.Save(fileName);
		}

		private static void RemoveChangeNotificationKeys(XmlDocument doc)
		{
			string matchString = "DataServices.DataNotification.";
			XmlNode lookup = doc.GetElementsByTagName("lookup")[0];
			ArrayList nodesToRemove = new ArrayList();
			foreach (XmlNode node in doc.GetElementsByTagName("property"))
				if (node.Attributes["name"].Value.StartsWith(matchString))
					nodesToRemove.Add(node);
			foreach (XmlNode node in nodesToRemove)
				lookup.RemoveChild(node);
		}

		private static void SetProperty(XmlDocument doc, string name, string aid, string gid, string val)
		{
			XmlNode node = doc.SelectSingleNode("//property[@name='" + name + "']");
			XmlNode lookup = doc.GetElementsByTagName("lookup")[0];
			if (node == null)
			{
				node = doc.CreateElement("property");
				lookup.AppendChild(node);
			}

			if (node.Attributes["name"] == null)
				node.Attributes.Append(doc.CreateAttribute("name"));
			node.Attributes["name"].Value = name;

			if (node.Attributes["AID"] == null)
				node.Attributes.Append(doc.CreateAttribute("AID"));
			node.Attributes["AID"].Value = aid;

			if (node.Attributes["GID"] == null)
				node.Attributes.Append(doc.CreateAttribute("GID"));
			node.Attributes["GID"].Value = gid;
			
			node.InnerText = val;
		}


		#region Custom Test Setup
		/// <summary>
		/// Ensures no properties exist in xml file, to test "No Polling Interval found".
		/// </summary>
		public static void SetupTest1()
		{
			XmlDocument properties = getPropertiesXml();
			RemoveChangeNotificationKeys(properties);
			savePropertiesXml(properties);
		}

		/// <summary>
		/// Ensures that a valid PollingInterval exists, to test "No Groups Found".
		/// </summary>
		public static void SetupTest2()
		{
			XmlDocument properties = getPropertiesXml();
			RemoveChangeNotificationKeys(properties);
			SetProperty(properties, "DataServices.DataNotification.PollingInterval", "", "", "1");
			savePropertiesXml(properties);
		}

		/// <summary>
		/// Ensures that valid PollingInterval and Groups exist.
		/// </summary>
		public static void SetupTest3()
		{
			XmlDocument properties = getPropertiesXml();
			RemoveChangeNotificationKeys(properties);
			SetProperty(properties, "DataServices.DataNotification.PollingInterval", "", "", "1");
			SetProperty(properties, "DataServices.DataNotification.Groups", "", "", "TestGroup1, TestGroup2, TestGroup3");
			savePropertiesXml(properties);
		}

		/// <summary>
		/// Ensures that valid PollingInterval & Groups exist, Database name is invalid and no tables exist.
		/// </summary>
		public static void SetupTest4()
		{
			XmlDocument properties = getPropertiesXml();
			RemoveChangeNotificationKeys(properties);
			SetProperty(properties, "DataServices.DataNotification.PollingInterval", "", "", "1");
			SetProperty(properties, "DataServices.DataNotification.Groups", "", "", "TestGroup1, TestGroup2, TestGroup3");
			SetProperty(properties, "DataServices.DataNotification.TestGroup1.Database", "", "", "TestInvalidDB1");
			SetProperty(properties, "DataServices.DataNotification.TestGroup2.Database", "", "", "TestInvalidDB2");
			SetProperty(properties, "DataServices.DataNotification.TestGroup3.Database", "", "", "TestInvalidDB3");
			savePropertiesXml(properties);
		}
		
		/// <summary>
		/// Ensures that valid PollingInterval, Groups, Database and Tables exist.
		/// Execute SQL script to remove "GetChangeTable" Stored Procedure in DefaultDB.
		/// </summary>
		public static bool SetupTest5()
		{
			XmlDocument properties = getPropertiesXml();
			RemoveChangeNotificationKeys(properties);
			SetProperty(properties, "DataServices.DataNotification.PollingInterval", "", "", "1");
			SetProperty(properties, "DataServices.DataNotification.Groups", "", "", "TestGroup1, TestGroup2, TestGroup3");
			SetProperty(properties, "DataServices.DataNotification.TestGroup1.Database", "", "", "DefaultDB");
			SetProperty(properties, "DataServices.DataNotification.TestGroup2.Database", "", "", "DefaultDB");
			SetProperty(properties, "DataServices.DataNotification.TestGroup3.Database", "", "", "DefaultDB");
			SetProperty(properties, "DataServices.DataNotification.TestGroup1.Tables", "", "", "TestGroup1Table1, TestGroup1Table2");
			SetProperty(properties, "DataServices.DataNotification.TestGroup2.Tables", "", "", "TestGroup2Table1, TestGroup2Table2");
			SetProperty(properties, "DataServices.DataNotification.TestGroup3.Tables", "", "", "TestGroup3Table1, TestGroup3Table2");
			savePropertiesXml(properties);

			if (ExecuteSetupScript("DropStoredProc.sql"))
				return true;
			else
				return false;

		}

		/// <summary>
		/// Ensures that valid PollingInterval, Groups, Database and Tables exist.
		/// Execute SQL script to create "ChangeNotification" Table
		/// and create "GetChangeTable" Stored Procedure in DefaultDB.
		/// </summary>
		public static bool SetupTest6()
		{
			bool createdTable = false;
			bool createdStoredProc = false;

			XmlDocument properties = getPropertiesXml();
			RemoveChangeNotificationKeys(properties);
			SetProperty(properties, "DataServices.DataNotification.PollingInterval", "", "", "1");
			SetProperty(properties, "DataServices.DataNotification.Groups", "", "", "TestGroup1");
			SetProperty(properties, "DataServices.DataNotification.TestGroup1.Database", "", "", "DefaultDB");
			SetProperty(properties, "DataServices.DataNotification.TestGroup1.Tables", "", "", "Table1");
			savePropertiesXml(properties);

			createdTable = ExecuteSetupScript("CreateStoredProc.sql");
			createdStoredProc = ExecuteSetupScript("CreateChangeNotificationTable.sql");


			if (createdTable && createdStoredProc)
				return true;
			else
				return false;


		}

		public static bool TidyUp()
		{
			if (ExecuteSetupScript("TidyUp.sql"))
				return true;
			else
				return false;
		}


		/// <summary>
		/// Enables Custom Test Setups to prepare the Database via scripts run in osql.exe
		/// </summary>
		/// <param name="scriptName"></param>
		/// <returns></returns>
		private static bool ExecuteSetupScript(string scriptName)
		{
			Process sql = new Process();
			sql.StartInfo.FileName = "osql.exe";
			sql.StartInfo.Arguments = "-E -i \"" + System.IO.Directory.GetCurrentDirectory() + "\\DataServices\\" + scriptName + "\"";
			sql.StartInfo.WorkingDirectory = System.IO.Directory.GetCurrentDirectory();
//			sql.StartInfo.RedirectStandardError = true;
//			sql.StartInfo.RedirectStandardOutput = true;
//			sql.StartInfo.UseShellExecute = false;
			sql.Start();

			// Wait for it to finish
			while (!sql.HasExited)
				Thread.Sleep(1000);

//			StreamReader stdOut = sql.StandardOutput;
//			StreamReader stdError = sql.StandardError;
            
//			string output = stdOut.ReadToEnd();
//			string error = stdError.ReadToEnd();

			return (sql.ExitCode == 0);
			
		}

		#endregion
	}

}