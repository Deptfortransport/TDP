// *********************************************** 
// NAME			: TestLocationInformationImport.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 18/10/07
// DESCRIPTION 	: Test class for Location Information import. This class tests the 
// import facility for the Location information (Airport Links) comma-delimited (csv) file.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationInformationService/Test/TestLocationInformationImport.cs-arc  $
//
//   Rev 1.0   Nov 28 2007 14:56:50   mturner
//Initial revision.
//
//   Rev 1.0   Oct 25 2007 15:40:42   mmodi
//Initial revision.
//Resolution for 4518: Del 9.8 - Air Departure Boards
//

using System;
using System.IO;
using System.Collections;
using System.Threading;
using System.Diagnostics;
using NUnit.Framework;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.LocationInformationService.Test
{
	#region Test Initialisation Class
	/// <summary>
	/// Class to initialise services that are used by the tests.
	/// </summary>
	public class TestInitialisation : IServiceInitialisation
	{
		/// <summary>
		/// Populates a hashtables with values from a config file.
		/// </summary>
		/// <param name="serviceCache"></param>
		public void Populate(Hashtable serviceCache)
		{
			// Enable PropertyService
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			
			// Enable logging service.
			ArrayList errors = new ArrayList();
			try
			{    
				IEventPublisher[] customPublishers = new IEventPublisher[0];
				Logger.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
			}
			catch (TDException tdEx)
			{
				foreach(string error in errors)
				{
					Console.WriteLine(error);
				}
				throw tdEx;
			}
		}
	}
	#endregion

	/// <summary>
	/// Summary description for TestLocationInformationImport.
	/// </summary>
	[TestFixture]
	public class TestLocationInformationImport
	{
		private string feed = string.Empty;
		private string params1 = string.Empty;
		private string params2 = string.Empty;
		private string utility = string.Empty;
		private readonly string processingDirectory = Directory.GetCurrentDirectory();
		const int compareCode = 0;
		const string currentProperty = "datagateway.sqlimport.airportlinks.feedname";

		/// <summary>
		/// Empty constructor to allow instantiation
		/// </summary>
		public TestLocationInformationImport()
		{
		}

		/// <summary>
		/// Setup Initialisation method for all NUnit test scripts
		/// </summary>
		[TestFixtureSetUp]
		public void Init()
		{	
			// Initialise services
			TDServiceDiscovery.Init( new  TestInitialisation() );

			try
			{
				// Enure database has been set up sucessfully, or do not continue
				bool InitSuccessful = ExecuteSetupScript("LocationInformation/LocationInformationSetup.sql");
				Assert.AreEqual(true, InitSuccessful, "Database setup failed during initialisation. Unable to continue with tests.");
			}
			catch
			{
				throw new TDException("Set up failed for DataServices.", false, TDExceptionIdentifier.AETDTraceInitFailed);	
			}
		}

		/// <summary>
		/// Clean Up method for test script
		/// </summary>
		[TestFixtureTearDown]
		public void CleanUp()
		{
			// Remove test data from Database
			bool CleanUpSuccessful = ExecuteSetupScript("LocationInformation/LocationInformationCleanUp.sql");
			Assert.AreEqual(true, CleanUpSuccessful, "Database clean up failed during Tear Down.");
		}

		/// <summary>
		/// Test that a valid Airport Links .csv file can be correctly loaded into the application
		/// </summary>
		[Test]
		public void TestAirportLinksDataImport()
		{
			const string datafilename = "AirportLinks.csv";

			LocationInformationImportTask importLocs = new LocationInformationImportTask(Properties.Current[currentProperty], "", "", "", processingDirectory);
 
			int testResult = importLocs.Run(datafilename);

			Assert.AreEqual(compareCode, testResult, "Import Failed");
		}

		/// <summary>
		/// Check if the AirportLinks .csv file is valid 
		/// </summary>
		[Test]
		public void TestInvalidAirportLinksCsvFile()
		{
			LocationInformationImportTask importLocs = new LocationInformationImportTask(Properties.Current[currentProperty], "", "", "", processingDirectory);
			
			int testResult = importLocs.Run("AirportLinks.xls");
	
			Assert.IsFalse(compareCode == testResult, "Import process incorrectly processed an invalid file");			

			try
			{
				testResult = importLocs.Run(null);
				Assert.Fail ("Import process incorrectly processed an invalid file");			
			}
			catch (System.NullReferenceException)
			{
				// Expected to throw an exception
			}
		}

		/// <summary>
		/// Test that the feed name sent to the import process is valid. The Airport Links feed name is pik611
		/// </summary>
		[Test]
		public void TestInvalidAirportLinksFeedName()
		{
			const string datafilename = "AirportLinks.csv";
			const string invalidFeedName = "m17xSh";

			LocationInformationImportTask importLocs = new LocationInformationImportTask(invalidFeedName, processingDirectory);
 
			int testResult = importLocs.Run(datafilename);
			Assert.AreEqual((int)TDExceptionIdentifier.DGUnexpectedFeedName, testResult, "System processed an invalid feed name");
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
	}
}
