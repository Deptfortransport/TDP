// *****************************************************
// NAME 		: TestZonalServicesImport.cs
// AUTHOR 		: Tolu Olomolaiye
// DATE CREATED : 15 Dec 2005
// DESCRIPTION 	: Test class for Zonal Services import. 
//	This class tests the import facility for the Zonal 
//	services comma-delimited (csv) file.
// *****************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ZonalServices/Test/TestZonalServicesImport.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 13:03:24   mturner
//Initial revision.
//
//   Rev 1.8   Apr 12 2006 14:25:40   mtillett
//This should work on the build machine, it just needed to XSD file added to the gateway folder
//Resolution for 3606: DN072 Zonal Services: 'With Effect From' dates not validated correctly
//
//   Rev 1.7   Feb 17 2006 17:21:40   halkatib
//Added exlplination string to the ignore attribute
//
//   Rev 1.6   Feb 17 2006 17:18:30   halkatib
//Placed ignor attribute on the TestDataImport Method.
//
//   Rev 1.5   Feb 09 2006 13:55:30   jbroome
//FX Cop update.
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.4   Feb 08 2006 17:05:08   jbroome
//Added test for null file name
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.3   Feb 06 2006 15:14:08   tolomolaiye
//Code review updates
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.2   Jan 05 2006 10:52:18   tolomolaiye
//Updates following code review
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.1   Dec 21 2005 09:32:10   tolomolaiye
//Work in progress
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.0   Dec 16 2005 09:32:10   tolomolaiye
//Initial revision.
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
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

namespace TransportDirect.UserPortal.ZonalServices.Test
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
	/// Class to test the zonal services data import.
	/// </summary>
	[TestFixture]
	public class TestZonalServicesImport
	{
		private string feed = string.Empty;
		private string params1 = string.Empty;
		private string params2 = string.Empty;
		private string utility = string.Empty;
		private readonly string processingDirectory = Directory.GetCurrentDirectory();
		const int compareCode = 0;
		const string currentProperty = "datagateway.sqlimport.zonalservices.feedname";

		/// <summary>
		/// Empty constructor to allow instantiation
		/// </summary>
		public TestZonalServicesImport()
		{}

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
				bool InitSuccessful = ExecuteSetupScript("ZonalServices/ZonalStopSetup.sql");
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
			bool CleanUpSuccessful = ExecuteSetupScript("ZonalServices/ZonalStopCleanUp.sql");
			Assert.AreEqual(true, CleanUpSuccessful, "Database clean up failed during Tear Down.");
		}

		/// <summary>
		/// Test that a valid Zonal Servcies .csv file can be correctly loaded into the application
		/// </summary>
		[Test]
		public void TestDataImport()
		{
			const string datafilename = "ZonalInformation.csv";

			ZonalServicesImportTask importZones = new ZonalServicesImportTask(Properties.Current[currentProperty], "", "", "", processingDirectory);
 
			int testResult = importZones.Run(datafilename);

			Assert.AreEqual(compareCode, testResult, "Import Failed");
		}

		/// <summary>
		/// Check if the Zonal Services .csv file is valid 
		/// </summary>
		[Test]
		public void TestInvalidCsvFile()
		{
			ZonalServicesImportTask importZones = new ZonalServicesImportTask(Properties.Current[currentProperty], "", "", "", processingDirectory);
			int testResult = importZones.Run("ZonalInformation.xls");
	
			Assert.IsFalse(compareCode == testResult, "Import process incorrectly processed an invalid file");			

			try
			{
				testResult = importZones.Run(null);
				Assert.Fail ("Import process incorrectly processed an invalid file");			
			}
			catch (System.NullReferenceException)
			{
				// Expected to throw an exception
			}

		}

		/// <summary>
		/// Test that the feed name sent to the import process is valid. The Zonal Services feed name is efw677
		/// </summary>
		[Test]
		public void TestInvalidFeedName()
		{
			const string datafilename = "ZonalInformation.csv";
			const string invalidFeedName = "azp";

			ZonalServicesImportTask importZones = new ZonalServicesImportTask(invalidFeedName, processingDirectory);
 
			int testResult = importZones.Run(datafilename);
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
