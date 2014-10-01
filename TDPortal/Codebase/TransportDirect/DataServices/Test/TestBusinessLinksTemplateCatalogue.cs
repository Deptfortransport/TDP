// ***********************************************
// NAME 		: TestBusinessLinksTemplateCatalogue.cs
// AUTHOR 		: Tolu Olomolaiye
// DATE CREATED : 25/11/05
// DESCRIPTION 	: Test functionality of the BusinessLinksTemplateCatalogue class.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataServices/Test/TestBusinessLinksTemplateCatalogue.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:20:56   mturner
//Initial revision.
//
//   Rev 1.1   Dec 16 2005 12:15:18   jbroome
//Added test for GetDefault()
//Resolution for 3143: DEL 8 stream: Business Links Development

using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Configuration;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using NUnit.Framework;

namespace TransportDirect.UserPortal.DataServices
{
	/// <summary>
	/// Summary description for TestBusinessLinksTemplateCatalogue.
	/// </summary>
	[TestFixture]
	public class TestBusinessLinksTemplateCatalogue
	{
		public TestBusinessLinksTemplateCatalogue()
		{}
		
		[Test]
		public void TestBusinessLinksCreation()
		{
			BusinessLinksTemplateCatalogue links = new BusinessLinksTemplateCatalogue();
			BusinessLinkTemplate[] templates = links.GetAll();
			BusinessLinkTemplate template;

			//clear current data
			bool cleardownSuccessful = ExecuteSetupScript("DataServices/ClearBusinessLinksTable.sql");
			Assert.IsTrue(cleardownSuccessful, "Failed to clear data");

			//insert 3 rows in the table and test
			bool insertSuccessful = ExecuteSetupScript("DataServices/PopulateBusinessLinksTable.sql");
			Assert.IsTrue(insertSuccessful, "Failed to insert rows");

			links = new BusinessLinksTemplateCatalogue();
			templates = links.GetAll();

			Assert.AreEqual(3, templates.Length, "Incorrect number of rows in column");

			// first, check for default template

			template = links.GetDefault();
			Assert.IsNotNull(template, "Default template not found");
			Assert.AreEqual(0, template.Id, "Default template id incorrect");

			//now check for individual rows
			
			//first row
			template = links.Get(0);
			Assert.IsNotNull(template, "First row not found");

			Assert.AreEqual(template.Html, "Test HTML1", "HTML invalid");
			Assert.AreEqual(template.ResourceId, "Test1", "ResourceId invalid");
			Assert.AreEqual(template.ImageUrl, "TestURL1", "ImageURL invalid");
			//second row
			template = links.Get(1);
			Assert.IsNotNull(template, "Second row not found");

			//third row
			template = links.Get(2);
			Assert.IsNotNull(template, "Third row not found");

			//null row
			template = links.Get(-1);
			Assert.IsNull(template, "Null row returned a row");
		}

		/// <summary>
		/// Test for when the catalgue table in the database is empty
		/// </summary>
		[Test]
		public void TestEmptyTemplateCatalogue()
		{
			//clear current data
			bool cleardownSuccessful = ExecuteSetupScript("DataServices/ClearBusinessLinksTable.sql");
			Assert.IsTrue(cleardownSuccessful, "Failed to clear data");

			BusinessLinksTemplateCatalogue links = new BusinessLinksTemplateCatalogue();
			BusinessLinkTemplate[] templates = links.GetAll();
			BusinessLinkTemplate template;

			//test for the business links objects when there is no data	
			Assert.AreEqual(0, templates.Length, "Array is not empty");

			template = links.Get(1);
			Assert.IsNull(template, "Row returned when there should be none");
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
			sql.StartInfo.Arguments = "-E -i \"" + System.IO.Directory.GetCurrentDirectory() + "\\" + scriptName + "\"";
			sql.StartInfo.WorkingDirectory = System.IO.Directory.GetCurrentDirectory();
			sql.Start();

			// Wait for it to finish
			while (!sql.HasExited)
				Thread.Sleep(1000);

			return (sql.ExitCode == 0);
		}

		[TestFixtureSetUp]
		public void Init()
		{		
			// Initialize Services
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestInitialization());

			try
			{

				// Enure database has been set up sucessfully, or do not continue
				bool InitSuccessful = ExecuteSetupScript("DataServices/BusinessLinksSetup.sql");
				Assert.AreEqual(true, InitSuccessful, "Database setup failed during initialisation. Unable to continue with tests.");
			}
			catch
			{
				throw new TDException("Set up failed for DataServices.", false, TDExceptionIdentifier.AETDTraceInitFailed);	
			}
		}

		/// <summary>
		/// Clean Up undoes DB changes performed during tests
		/// </summary>
		[TestFixtureTearDown]
		public void CleanUp()
		{
			// Remove test data from Database
			bool CleanUpSuccessful = ExecuteSetupScript("DataServices/BusinessLinksCleanUp.sql");
			Assert.AreEqual(true, CleanUpSuccessful, "Database clean up failed during Tear Down.");
		}

	}
}
