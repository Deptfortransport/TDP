// ***********************************************
// NAME                 : TestProperties.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 2/07/2003
// DESCRIPTION			: Test class for the Properties dll
// NOTE					: This test MUST be run on the td.common.propertyservice.properties.dll that sits in
//						  either the DatabasePropertyProvider or FilePropertyProvider bin folder.
//						  It won't work on the td.common.propertyservice.properties.dll that sits in the Properties bin folder.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PropertiesService/Properties/TestProperties.cs-arc  $
//
//   Rev 1.1   Mar 10 2008 15:22:58   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:37:54   mturner
//Initial revision.
//
//   Rev 1.9   Feb 23 2006 19:15:48   build
//Automatically merged from branch for stream3129
//
//   Rev 1.8.2.0   Dec 15 2005 10:18:40   schand
//Getting Partnet White Label changes for stream3129. 
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.8.1.0   Oct 07 2005 08:55:48   pcross
//Unit test for microsite
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2810: Del 8 White Labelling Phase 3 - Changes to Properties and Data services Components
//
//   Rev 1.8   Feb 07 2005 09:02:32   RScott
//Assertion changed to Assert
//
//   Rev 1.7   Jun 03 2004 11:43:16   acaunt
//Removed stray del54 comments
//
//   Rev 1.6   Jun 03 2004 11:37:42   acaunt
//Database and file manipulation actions moved out of tests into supporting methods. When testing against a database, inserts stored procedures before testing.
//
//   Rev 1.5   Oct 30 2003 14:53:44   PNorell
//Updated documentation.

using System;
using NUnit.Framework;
using TransportDirect.Common.PropertyService.Properties;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Xml;
using System.Configuration;
using TransportDirect.Common.ServiceDiscovery;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace TransportDirectNUnit
{
	public class UnitTestInit : IServiceInitialisation
	{
		public void Populate(Hashtable serviceCache)
		{
		}
	}

	/// <summary>
	/// Test for the Properties class
	/// </summary>
	/// <testPlan>
	/// !!!! YOU CAN TEST PROPERTIES EITHER WITH FilePropertyProvider OR WITH DatabasePropertyProvider! JUST RUN NUNIT ON THE properties.dll IN THE FilePropertyProvider BIN DIR OR IN THE DatabasePropertyProvider DIR (different config file)
	/// Test of the current Method
	/// Test of the Time Elapsed Method
	/// </testPlan>
	[TestFixture]
	public class TestProperties
	{
		private SqlConnection connection;
		private string filePath;
		private string mode ="";

		public TestProperties()
		{
			// Determine the appropriate mode and setup associated objects as necessaru
            string providerAssembly = ConfigurationManager.AppSettings
						["propertyservice.providerassembly"];
			if (providerAssembly =="td.common.propertyservice.databasepropertyprovider")
			{
				mode = "db";
				connection =
					new SqlConnection("Integrated Security=SSPI;Initial Catalog=PermanentPortal;Data Source=.;Connect Timeout=30");

			}
			else
			{
				mode ="file";
				filePath =
                    ConfigurationManager.AppSettings["propertyservice.providers.fileprovider.filepath"];
			}
		}

		/// <summary>
		/// Tests that you can do a basic read of the properties then update the properties and retrieve
		/// the updated value
		/// </summary>
		[Test]
		public void TestCurrent()
		{

			// check that you get all the properties set in the initial test data
			Assert.AreEqual("1",(string)Properties.Current["propertyservice.version"], "Error retrieving property1");
			Assert.AreEqual("1000",(string)Properties.Current["propertyservice.refreshrate"], "Error retrieving property2");
			Assert.AreEqual("hello group",(string)Properties.Current["test.propertyservice.standard.message"], "Error retrieving property3");

			// Amend properties by adding a new group property
			AddNewGroupProperty();

			// Check 1 has changed
			Assert.AreEqual("2",(string)Properties.Current["propertyservice.version"], "Error retrieving property4");
			Assert.AreEqual("1000",(string)Properties.Current["propertyservice.refreshrate"], "Error retrieving property5");
			Assert.AreEqual("hello group",(string)Properties.Current["test.propertyservice.standard.message"], "Error retrieving property6");

		}

		/// <summary>
		/// Tests that you can read properties for a specific partner.
		/// If it doesn't exist then reads the equivalent non-partner-specific property.
		/// </summary>
		[Test]
		public void TestPartnerSpecific()
		{

			// Set up the test data so there is a property with both partner and default value
			// and also a property with default value only
			AddPartnerSpecificProperties();

			// Check that the partner specific property overrides the default
			Assert.AreEqual("1",(string)Properties.Current["test.propertyservice.partnertest1", 1], "Error retrieving partner specific property");
			Assert.AreEqual("2",(string)Properties.Current["test.propertyservice.partnertest1"], "Error retrieving partner specific property");
			
			// Check that the default is returned even when passing in the partner ID, when there is no partner ID property available
			Assert.AreEqual("3",(string)Properties.Current["test.propertyservice.partnertest2", 1], "Error retrieving default property");
			Assert.AreEqual("3",(string)Properties.Current["test.propertyservice.partnertest2"], "Error retrieving default property");

		}

		[Test]
		public void TestSuperseded()
		{

			// Obtain a copy of the properties
			IPropertyProvider current = Properties.Current;
			// The properties for the application haven't changed so superseded should be false
			Assert.AreEqual(false, current.IsSuperseded, "The properties should not be superseded");

			// Update one of the properties associated with the application
			UpdateProperty();

			// The properties for the application have now changed so superseded should be true
			Assert.AreEqual(true, current.IsSuperseded, "The properties should be superseded");
		}

		[SetUp]
		public void Init()
		{
			// Set up and intialise the appropriate properties data source
			if (mode =="db")
			{
				//Install the required stored procedures
				InstallProcedures();
				//Set the data on the database
				connection.Open();

				SqlCommand cmSetup = new SqlCommand("DatabasePropertyProviderTestSetup", connection);
				cmSetup.CommandType =  CommandType.StoredProcedure;
				cmSetup.ExecuteNonQuery();

				SqlCommand cmLoad = new SqlCommand("DatabasePropertyProviderTestLoad1", connection);
				cmLoad.CommandType =  CommandType.StoredProcedure;
				cmLoad.ExecuteNonQuery();

				connection.Close();
			}
			else
			{
				// Ensure that the file is writable
				System.IO.File.SetAttributes(filePath, FileAttributes.Normal);
				// Set the Xml for tests
				XmlDocument xd = new XmlDocument();
				xd.Load(filePath);
				XmlNode nodeVersion = xd.SelectSingleNode("//property[@name=\"propertyservice.version\"]");
				nodeVersion.InnerText = "1";

				XmlNode nodeRefreshRate = xd.SelectSingleNode("//property[@name=\"propertyservice.refreshrate\"]");
				nodeRefreshRate.InnerText = "1000";

				// create a new property for standard.message GROUP
				XmlNode nodeMessage = nodeRefreshRate.Clone();
				nodeMessage.Attributes["name"].InnerText = "test.propertyservice.standard.message";
				nodeMessage.Attributes["GID"].InnerText = "1111";
				nodeMessage.Attributes["AID"].InnerText = "0000";
				
				// Create a new partner ID attribute if doesn't already exist
				if (nodeMessage.Attributes["PartnerId"] == null)
				{
					XmlAttribute attrPartnerId = xd.CreateAttribute("PartnerId");
					nodeMessage.Attributes.Append(attrPartnerId);
				}
				nodeMessage.Attributes["PartnerId"].InnerText = "";
				nodeMessage.InnerText = "hello group";
				xd.DocumentElement.AppendChild(nodeMessage);

				xd.Save(filePath);
			}

			// Initialise the Property Service
			TDServiceDiscovery.Init( new UnitTestInit() );
			TDServiceDiscovery.Current.SetServiceForTest( ServiceDiscoveryKey.PropertyService,new PropertyServiceFactory() );

		}

		[TearDown]
		public void CleanUp()
		{
			if (mode == "db")
			{
				SqlCommand cm = new SqlCommand("DatabasePropertyProviderTidyUp", connection);
				cm.CommandType =  CommandType.StoredProcedure;

				// The connection may already by open if an error was thrown while it was in use
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
				}
				cm.ExecuteNonQuery();
				connection.Close();
			}
			else
			{
				// Set the Xml for tests
				XmlDocument xd = new XmlDocument();
				xd.Load(filePath);
				XmlNode nodeVersion = xd.SelectSingleNode("//property[@name=\"propertyservice.version\"]");
				nodeVersion.InnerText = "1";

				XmlNode nodeRefreshRate = xd.SelectSingleNode("//property[@name=\"propertyservice.refreshrate\"]");
				nodeRefreshRate.InnerText = "1000";

				// Remove the message properties
				XmlNodeList messageNodes = xd.SelectNodes("//property[@name=\"test.propertyservice.standard.message\"]");

				foreach (XmlNode node in messageNodes)
				{
					xd.DocumentElement.RemoveChild(node);
				}

				// Remove the partnertest properties (I'm sure there must be a way to use test.propertyservice.partnertest
				// with a wildcard but don't know!)
				XmlNodeList partnerNodes1 = xd.SelectNodes("//property[@name=\"test.propertyservice.partnertest1\"]");

				foreach (XmlNode node in partnerNodes1)
				{
					xd.DocumentElement.RemoveChild(node);
				}

				XmlNodeList partnerNodes2 = xd.SelectNodes("//property[@name=\"test.propertyservice.partnertest2\"]");

				foreach (XmlNode node in partnerNodes2)
				{
					xd.DocumentElement.RemoveChild(node);
				}

				xd.Save(filePath);
			}
		}

		private void AddNewGroupProperty()
		{
			if (mode =="db")
			{
				// Add the group property
				SqlCommand cm = new SqlCommand("DatabasePropertyProviderTestLoad2", connection);
				cm.CommandType =  CommandType.StoredProcedure;
				connection.Open();
				cm.ExecuteNonQuery();
				connection.Close();
			}
			else
			{
				// Set the Xml for tests
				XmlDocument xd = new XmlDocument();
				xd.Load(filePath);

				// Update the version number
				XmlNode nodeVersion = xd.SelectSingleNode("//property[@name=\"propertyservice.version\"]");
				nodeVersion.InnerText = "2";

				XmlNode nodeMessage = xd.SelectSingleNode("//property[@name=\"test.propertyservice.standard.message\"]");

				// create a new property for standard.message GROUP
				XmlNode nodeMessage2 = nodeMessage.Clone();
				nodeMessage2.Attributes["GID"].InnerText = "2222";
				nodeMessage2.Attributes["AID"].InnerText = "0000";
				nodeMessage2.InnerText = "hello new group";
				xd.DocumentElement.AppendChild(nodeMessage2);

				xd.Save(filePath);
			}
			// Wait for twice the refresh period to ensure that the new values are picked up
			Thread.Sleep(2000);
		}

		private void UpdateProperty()
		{
			if (mode=="db")
			{

				// Change the version number in DB
				SqlCommand cm = new SqlCommand("DatabasePropertyProviderTestLoad3", connection);
				cm.CommandType =  CommandType.StoredProcedure;
				connection.Open();
				cm.ExecuteNonQuery();
				connection.Close();

			}
			else
			{
				// Set the Xml for tests
				XmlDocument xd = new XmlDocument();
				xd.Load(filePath);
				XmlNode nodeVersion = xd.SelectSingleNode("//property[@name=\"propertyservice.version\"]");
				nodeVersion.InnerText = "3";

				XmlNode nodeMessage = xd.SelectSingleNode("//property[@name=\"test.propertyservice.standard.message\"]");

				// create a new property for standard.message GROUP
				XmlNode nodeMessage2 = nodeMessage.Clone();
				nodeMessage2.Attributes["GID"].InnerText = "1111";
				nodeMessage2.Attributes["AID"].InnerText = "5678";
				nodeMessage2.InnerText = "hello application";
				xd.DocumentElement.AppendChild(nodeMessage2);

				xd.Save(filePath);

			}
			// Wait for twice the refresh period to ensure that the new values are picked up
			Thread.Sleep(2000);
		}

		/// <summary>
		/// Insert a partner specific property and an equivalent default property
		/// Insert a default property with no partner specific property
		/// </summary>
		private void AddPartnerSpecificProperties()
		{
			if (mode =="db")
			{
				// Add the group property
				SqlCommand cm = new SqlCommand("DatabasePropertyProviderTestLoad5", connection);
				cm.CommandType =  CommandType.StoredProcedure;
				connection.Open();
				cm.ExecuteNonQuery();
				connection.Close();
			}
			else
			{
				// Set the Xml for tests
				XmlDocument xd = new XmlDocument();
				xd.Load(filePath);

				XmlNode nodeVersion = xd.SelectSingleNode("//property[@name=\"propertyservice.version\"]");
				nodeVersion.InnerText = "5";

				XmlNode nodeMessage = xd.SelectSingleNode("//property[@name=\"test.propertyservice.standard.message\"]");

				// create a partner specific property and an equivalent default property
				XmlNode nodeMessage2 = nodeMessage.Clone();
				nodeMessage2.Attributes["name"].InnerText = "test.propertyservice.partnertest1";
				nodeMessage2.Attributes["GID"].InnerText = "1111";
				nodeMessage2.Attributes["AID"].InnerText = "5678";
				nodeMessage2.Attributes["PartnerId"].InnerText = "1";
				nodeMessage2.InnerText = "1";
				xd.DocumentElement.AppendChild(nodeMessage2);

				XmlNode nodeMessage3 = nodeMessage2.Clone();
				nodeMessage3.Attributes["name"].InnerText = "test.propertyservice.partnertest1";
				nodeMessage3.Attributes["GID"].InnerText = "1111";
				nodeMessage3.Attributes["AID"].InnerText = "5678";
				nodeMessage3.Attributes["PartnerId"].InnerText = "0";
				nodeMessage3.InnerText = "2";
				xd.DocumentElement.AppendChild(nodeMessage3);

				// create a default property with no partner specific property
				XmlNode nodeMessage4 = nodeMessage3.Clone();
				nodeMessage4.Attributes["name"].InnerText = "test.propertyservice.partnertest2";
				nodeMessage4.Attributes["GID"].InnerText = "1111";
				nodeMessage4.Attributes["AID"].InnerText = "5678";
				nodeMessage4.Attributes["PartnerId"].InnerText = "0";
				nodeMessage4.InnerText = "3";
				xd.DocumentElement.AppendChild(nodeMessage4);

				xd.Save(filePath);
			}
			// Wait for twice the refresh period to ensure that the new values are picked up
			Thread.Sleep(2000);
		}

		/// <summary>
		/// Use osql to load the stored procedures into the database, which are then called by the test fixtures
		/// </summary>
        private void InstallProcedures()
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
