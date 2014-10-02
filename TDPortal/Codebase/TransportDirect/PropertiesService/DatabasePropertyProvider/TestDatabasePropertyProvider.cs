// ***********************************************
// NAME                 : TestDatabasePropertyProvider.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 2/07/2003
// DESCRIPTION  : The test class for DatabasePropertyProvider
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PropertiesService/DatabasePropertyProvider/TestDatabasePropertyProvider.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:38   mturner
//Initial revision.
//
//   Rev 1.9   Feb 08 2005 15:55:50   bflenk
//Changed Assertion to Assert
//
//   Rev 1.8   Jun 03 2004 11:42:24   acaunt
//Removed stray del54 comments.
//
//   Rev 1.7   Jun 03 2004 11:41:18   acaunt
//Test harness now installs the test data stored procedures itself.
//Test broken down into four individual tests.
//
//   Rev 1.6   Mar 18 2004 13:35:40   CHosegood
//Wrapped init() and CleanUp() method actions in a try/catch block
//
//   Rev 1.5   Oct 30 2003 14:52:52   PNorell
//Documented and updated test script.
//
//   Rev 1.4   Jul 23 2003 10:22:50   passuied
//Changes after PropertyService namespaces / dll renaming
//
//   Rev 1.3   Jul 17 2003 14:58:54   passuied
//updated
//
//   Rev 1.2   Jul 17 2003 12:43:40   passuied
//changes after code review


using System;
using System.Collections;
using System.Diagnostics;
using NUnit.Framework;
using TransportDirect.Common.PropertyService.DatabasePropertyProvider;
using TransportDirect.Common.PropertyService.Properties;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;




namespace TransportDirectNUnit
{
	/// <summary>
	/// Test class for the DatabasePropertyProvider class
	/// </summary>
	/// <TestPlan>
	/// This test tests the Load method
	/// It it in different parts:
	/// # get a property called "propertyservice.standard.message" that is on a			global level
	/// # Insert a new property of the same name but that is on a group level. Check that the properties has not been updated (version has not changed)
	/// # Change the version. Check that the added property is taken into account
	/// # Insert a new property of the same name but that is on an application level. Change the version. Check that the added property is taken into account
	///
	/// </TestPlan>
	/// <summary>
	/// Summary description for Initialisations.
	/// </summary>
	public class NunitTestInitilisation : IServiceInitialisation
	{
		public void Populate(Hashtable serviceCache)
		{
			// Add cryptographic scheme
			serviceCache.Add( ServiceDiscoveryKey.Crypto,  new CryptoFactory() );
		}
	}

	[TestFixture]
	public class TestDatabasePropertyProvider
	{
		private DatabasePropertyProvider myObject;
		private SqlConnection connection;

		public TestDatabasePropertyProvider()
		{

			connection = new SqlConnection( ConfigurationManager.AppSettings["propertyservice.providers.databaseprovider.connectionstring"] );
			// Add cryptographic scheme
			TDServiceDiscovery.Init( new NunitTestInitilisation() );

		}

		[SetUp]
		public void Init()
		{
            try
            {
                myObject = new DatabasePropertyProvider();
            }
            catch ( Exception e )
            {
                Trace.Write ( e.Message );
                throw e;
            }
           InstallProcedures();
		}


		[Test]
		public void TestLoad()
		{
			//Set the data on the database
			SqlCommand cm = new SqlCommand("DatabasePropertyProviderTestLoad1", connection);
			cm.CommandType =  CommandType.StoredProcedure;
			connection.Open();
			cm.ExecuteNonQuery();
			connection.Close();

			// Test the load with initial version
			myObject.Load();
			IPropertyProvider result = myObject;

			// check that you get all the properties
			Assert.AreEqual
				("1", (string)result["propertyservice.version"], "Incorrect value of  property propertyservice.version retrieved");
			Assert.AreEqual
				("1000", (string)result["propertyservice.refreshrate"], "Incorrect value of  property propertyservice.version retrieved");
			Assert.AreEqual
				("hello group",
				(string)result["test.propertyservice.standard.message"], "Incorrect value of  property propertyservice.version retrieved");

		}

		[Test]
		public void TestPropertyForDifferentGroup()
		{
			// Load the Properties
			TestLoad();

			// Re-add a property, but for a different group. This should make no different
			SqlCommand cm = new SqlCommand("DatabasePropertyProviderTestLoad2", connection);
			cm.CommandType =  CommandType.StoredProcedure;
			connection.Open();
			cm.ExecuteNonQuery();
			connection.Close();

			// Load
			IPropertyProvider result =  myObject.Load();
			if (result == null)
			{
				Assert.Fail("DatabasePropertyProvider.Load() returned a null property set");
			}

			// check that the value of test.propertyservice.standard.message hasn't changed
			Assert.AreEqual
				("hello group", (string)result["test.propertyservice.standard.message"],
				"Incorrect value of propertyservice.standard.message retrieved when the property is added for a different group");
		}

		[Test]
		public void TestOverrideGroupProperty()
		{
			// Load the Properties
			TestLoad();

			// Add a property, but with a matching application id. This should override the group value
			SqlCommand cm = new SqlCommand("DatabasePropertyProviderTestLoad3", connection);
			cm.CommandType =  CommandType.StoredProcedure;
			connection.Open();
			cm.ExecuteNonQuery();
			connection.Close();

			// Load
			IPropertyProvider result =  myObject.Load();
			if (result == null)
			{
				Assert.Fail("DatabasePropertyProvider.Load() returned a null property set");
			}

			// check that version property and message has changed
			Assert.AreEqual
				("hello application", (string)result["test.propertyservice.standard.message"],
				"Incorrect value of test.propertyservice.standard.message retrieved when the application level property is added");
		}

		[Test]
		public void TestUpdatePropertyValue()
		{
			// Load the Properties
			TestLoad();

			// Change the version number
			SqlCommand cm = new SqlCommand("DatabasePropertyProviderTestLoad4", connection);
			cm.CommandType =  CommandType.StoredProcedure;
			connection.Open();
			cm.ExecuteNonQuery();
			connection.Close();

			// Load
			IPropertyProvider result =  myObject.Load();
			if (result == null)
			{
				Assert.Fail("DatabasePropertyProvider.Load() returned a null property set");
			}

			// check that version property and message has changed
			Assert.AreEqual
				("2000", (string)result["propertyservice.refreshrate"],
				"Updated value of propertyservice.refreshrate was not retrieved");
		}


        [TearDown]
        public void CleanUp()
        {
            try
            {
                connection.Close();
                SqlCommand cm = new SqlCommand("DatabasePropertyProviderTidyUp", connection);
                cm.CommandType =  CommandType.StoredProcedure;

                connection.Open();
                cm.ExecuteNonQuery();
                connection.Close();
            }
            catch ( SqlException sqle )
            {
                Trace.Write ( sqle.Message );
                throw sqle;
            }
        }

		/// <summary>
		/// Use osql to load the stored procedures into the database, which are then called by the test fixtures
		/// </summary>
        public void InstallProcedures()
        {

			try
			{
				Process process = new Process();

				// Set up process attributes.
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.FileName = "osql";
				//p.StartInfo.WorkingDirectory = processingDirectory;
				process.StartInfo.Arguments = @"-E -i .\DatabasePropertyProvider\TestProcedures.sql";

				process.Start();
				process.WaitForExit();
			}
			catch(Exception e)
			{
				Assert.Fail("Unable to open stored procedure file to populate test data. "+e.Message);
			}

		}

	}
}
