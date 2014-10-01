// *********************************************** 
// NAME			: TestAvailabilityDataMaintenance.cs
// AUTHOR		: James Broome
// DATE CREATED	: 26/01/2005
// DESCRIPTION	: Test implementation of the AvailabilityDataMaintenance class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AvailabilityDataMaintenance/Test/TestAvailabilityDataMaintenance.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:58   mturner
//Initial revision.
//
//   Rev 1.7   Apr 22 2005 11:14:50   jbroome
//Added error handling for process hanging.
//
//   Rev 1.6   Apr 21 2005 14:39:00   jbroome
//Added error handling during initialisation
//
//   Rev 1.5   Apr 15 2005 14:32:52   jbroome
//Restructed test folders
//
//   Rev 1.4   Apr 15 2005 13:48:28   jbroome
//Updated test code as ImportProductProfiles now uses a .csv file for import.
//
//   Rev 1.3   Mar 21 2005 10:55:58   jbroome
//Minor updates after code review
//
//   Rev 1.2   Feb 17 2005 14:55:32   jbroome
//Updated test fixtures as main classes have changed
//Resolution for 1924: DEV Code Review : Availability Data Maintenance
//
//   Rev 1.1   Feb 08 2005 13:54:18   jbroome
//Updated application name
//
//   Rev 1.0   Feb 08 2005 10:39:32   jbroome
//Initial revision.

using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Configuration;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.ServiceDiscovery;

using NUnit.Framework;


namespace TransportDirect.UserPortal.AvailabilityDataMaintenance
{
	/// <summary>
	/// Test class which tests the functionality of the 
	/// AvailabilityDataMaintenance class.
	/// </summary>
	[TestFixture]
	public class TestAvailabilityDataMaintenance
	{
		
		#region Private Members
		
		private const string appName = "td.userportal.availabilitydatamaintenance.exe";
		
		#endregion

		#region Initialisation and constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public TestAvailabilityDataMaintenance()
		{
		
		}

		/// <summary>
		/// Initialisation sets up DB for tests to run
		/// </summary>
		[TestFixtureSetUp]
		public void Init()
		{			
			try
			{
				// Initialise property service etc.
				TDServiceDiscovery.Init(new TestAvailabilityDataMaintenanceInitialisation());

				// Enure database has been set up sucessfully, or do not continue
				bool InitSuccessful = ExecuteSetupScript("AvailabilityDataMaintenance/SetUp.sql");
				Assert.AreEqual(true, InitSuccessful, "Database setup failed during initialisation. Unable to continue with tests.");
			}
			catch
			{
				throw new TDException("Set up failed for TestAvailabilityDataMaintenance.", false, TDExceptionIdentifier.AETDTraceInitFailed);	
			}
		}

		/// <summary>
		/// Clean Up undoes DB changes performed during tests
		/// </summary>
		[TestFixtureTearDown]
		public void CleanUp()
		{
			// Remove test data from Database
			bool CleanUpSuccessful = ExecuteSetupScript("AvailabilityDataMaintenance/CleanUp.sql");
			Assert.AreEqual(true, CleanUpSuccessful, "Database clean up failed during Tear Down.");
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

		# endregion

		#region TestMain

		/// <summary>
		/// Tests the Main entry point, and that arguments are
		/// handled appropriately
		/// </summary>
		public void TestMain()
		{
			// Create date time 2 minutes from now to kill process if problem
			TDDateTime endProcessTime = new TDDateTime();
			endProcessTime = endProcessTime.AddMinutes(2);

			string outputText = string.Empty;

			Process p = new Process();
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.CreateNoWindow = true;
			p.StartInfo.FileName = (appName);

			//Test running application with:
			
			// #1 No arguments specified
			p.StartInfo.Arguments = "";
			p.Start();
			outputText = p.StandardOutput.ReadToEnd();
			Assert.IsTrue(outputText.IndexOf(Messages.NoArgs + Messages.HelpMessage) > 0, "Error in test #1 with message output");
			while (!p.HasExited)
			{
				if (new TDDateTime() > endProcessTime)
				{
					p.Kill();
					Assert.Fail("Process failed to exit sucessfully within 2 minutes so was killed.");
				}
				Thread.Sleep(1000);
				// If still within 2 minutes, wait for process to exit
			}
			Assert.AreEqual((int)TDExceptionIdentifier.AENoArguments,  p.ExitCode , "The application did not exit with a the correct exception code");	

			// #2 Invalid arguments specified
			p.StartInfo.Arguments = @"/invalid";
			p.Start();
			outputText = p.StandardOutput.ReadToEnd();
			Assert.IsTrue(outputText.IndexOf(Messages.InvalidArgs + Messages.HelpMessage) > 0, "Error in test #2 with message output");	
			while (!p.HasExited)
			{
				if (new TDDateTime() > endProcessTime)
				{
					p.Kill();
					Assert.Fail("Process failed to exit sucessfully within 2 minutes so was killed.");
				}
				Thread.Sleep(1000);
				// If still within 2 minutes, wait for process to exit
			}
			Assert.AreEqual((int)TDExceptionIdentifier.AEInvalidArguments,  p.ExitCode , "The application did not exit with the correct exception code");	

			// #3 Help argument specified
			p.StartInfo.Arguments = @"/help";
			p.Start();
			outputText = p.StandardOutput.ReadToEnd();
			Assert.IsTrue(outputText.IndexOf(Messages.HelpMessage) > 0, "Error in test #3 with message output");	
			while (!p.HasExited)
			{
				if (new TDDateTime() > endProcessTime)
				{
					p.Kill();
					Assert.Fail("Process failed to exit sucessfully within 2 minutes so was killed.");
				}
				Thread.Sleep(1000);
				// If still within 2 minutes, wait for process to exit
			}
			Assert.AreEqual(0,  p.ExitCode , "The application did not exit with a return code of 0");	
		}

		#endregion
	
		#region TestArchiveAvailabilityHistory

		/// <summary>
		/// Tests the ArchiveAvailabilityHistory maintenance routine
		/// </summary>
		[Test]
		public void TestArchiveAvailabilityHistory()
		{
			// Create date time 2 minutes from now to kill process if problem
			TDDateTime endProcessTime = new TDDateTime();
			endProcessTime = endProcessTime.AddMinutes(2);

			// Test that AvailabilityHistory table contains data
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			int records = (int)helper.GetScalar("SELECT COUNT(*) FROM AvailabilityHistory");
			helper.ConnClose();
			Assert.IsTrue(records==10, "Error in TestArchiveAvailabilityHistory. Incorrect number of records in table");
			
			string filePath = Properties.Current["AvailabilityDataMaintenance.AvailabilityHistory.ArchiveDirectory"];
			
			#region Clear archive file directory
			// If files already exist in directory then then delete them
			string [] fileNames = Directory.GetFiles(filePath);
			if (fileNames.Length > 0)
			{
				try
				{
					foreach (string fileName in fileNames)
					{
						File.Delete(fileName);
					}
				}
				catch
				{
					Assert.Fail("Error in TestArchiveAvailabilityHistory. Files already exist in directory but cannot not be deleted");
				}
			}
			#endregion

            // Start and run the console app with correct args
			Process p = new Process();
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.CreateNoWindow = true;
			p.StartInfo.FileName = (appName);
			p.StartInfo.Arguments = @"/arc_hist";
			p.Start();
									
			while (!p.HasExited)
			{
				if (new TDDateTime() > endProcessTime)
				{
					p.Kill();
					Assert.Fail("Process failed to exit sucessfully within 2 minutes so was killed.");
				}
				Thread.Sleep(1000);
				// If still within 2 minutes, wait for process to exit
			}
			// Test that application has returned 0 exitcode
			Assert.AreEqual(0,  p.ExitCode , "Error in TestArchiveAvailabilityHistory. The application did not exit with a return code of 0");	

			// Test #4 Check that text file has been created successfully
			// Cannot access filename directly as created with a timestamp
			fileNames = Directory.GetFiles(filePath);
			Assert.AreEqual(1, fileNames.Length, "Error in test #4. Archive file not created in directory");
			
			// Test that records have been deleted from the database successfully
			helper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			records = (int)helper.GetScalar("SELECT COUNT(*) FROM AvailabilityHistory");
			helper.ConnClose();
			Assert.IsTrue(records==0, "Error in TestArchiveAvailabilityHistory. Not all records have been deleted");

			// Test #6 Test that process runs smoothly if no data present
			p.Start();
			while (!p.HasExited)
			{
				if (new TDDateTime() > endProcessTime)
				{
					p.Kill();
					Assert.Fail("Process failed to exit sucessfully within 2 minutes so was killed.");
				}
				Thread.Sleep(1000);
				// If still within 2 minutes, wait for process to exit
			}
			// Test that application has returned 0 exitcode
			Assert.AreEqual(0,  p.ExitCode , "Error in TestArchiveAvailabilityHistory. The application did not exit with a return code of 0");	

			// Test #7 Check that text file has been created successfully
			// even when no data present. Should now be 2 files
			fileNames = Directory.GetFiles(filePath);
			Assert.AreEqual(2, fileNames.Length, "Error in test #7. Archive file not created in directory");
		}

		#endregion

		#region TestDeleteUnavailableProducts

		/// <summary>
		/// Tests the DeleteUnavailableProducts maintenance routine
		/// </summary>
		[Test]
		public void TestDeleteUnavailableProducts()
		{
			// Create date time 2 minutes from now to kill process if problem
			TDDateTime endProcessTime = new TDDateTime();
			endProcessTime = endProcessTime.AddMinutes(2);

			// Test that UnavailableProducts table contains data
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			int records = (int)helper.GetScalar("SELECT COUNT(*) FROM UnavailableProducts");
			helper.ConnClose();
			Assert.IsTrue(records==9, "Error in TestDeleteUnavailableProducts. No records in table");

			// Start and run the console app with correct args
			Process p = new Process();
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.CreateNoWindow = true;
			p.StartInfo.FileName = (appName);
			p.StartInfo.Arguments = @"/del_unav";
			p.Start();
									
			while (!p.HasExited)
			{
				if (new TDDateTime() > endProcessTime)
				{
					p.Kill();
					Assert.Fail("Process failed to exit sucessfully within 2 minutes so was killed.");
				}
				Thread.Sleep(1000);
				// If still within 2 minutes, wait for process to exit
			}
			Assert.AreEqual(0,  p.ExitCode , "Error in TestDeleteUnavailableProducts. The application did not exit with a return code of 0");	

			// Test that all historic UnavailableProducts have been deleted
			// (Where TravelDate is in the past)
			helper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			records = (int)helper.GetScalar("SELECT COUNT(*) FROM UnavailableProducts");
			helper.ConnClose();
			Assert.IsTrue(records==5, "Error in TestDeleteUnavailableProducts.");

		}

		#endregion

		#region TestProductProfilesExport

		/// <summary>
		/// Tests the ProductProfilesExport maintenance routine
		/// </summary>
		[Test]
		public void TestProductProfilesExport()
		{
			// Create date time 2 minutes from now to kill process if problem
			TDDateTime endProcessTime = new TDDateTime();
			endProcessTime = endProcessTime.AddMinutes(2);

			string attachmentFile = Properties.Current["AvailabilityDataMaintenance.ProfilesExport.AttachmentFile"];
			
			// If file already exists in directory then then delete it
			if (File.Exists(attachmentFile))
			{
				try
				{
					File.Delete(attachmentFile);
				}
				catch
				{
					Assert.Fail("Error in TestProductProfilesExport. Attachment file already exists but cannot not be deleted");
				}
			}

			Process p = new Process();
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.CreateNoWindow = true;
			p.StartInfo.FileName = (appName);
			p.StartInfo.Arguments = @"/exp_prof";
			p.Start();
												
			while (!p.HasExited)
			{
				if (new TDDateTime() > endProcessTime)
				{
					p.Kill();
					Assert.Fail("Process failed to exit sucessfully within 2 minutes so was killed.");
				}
				Thread.Sleep(1000);
				// If still within 2 minutes, wait for process to exit
			}
			// Test that application has returned 0 exitcode
			Assert.AreEqual(0,  p.ExitCode , "Error in TestProductProfilesExport. The application did not exit with a return code of 0");	

			// Test #3 Check that xml attachment has been created successfully
			Assert.AreEqual(true, File.Exists(attachmentFile), "Error in test #3. Attachment file not created");

			// ********Manual Testing needed***********
			//
			// A manual test should now be made to check that 
			// an email has been received at the address specified in 
			// AvailabilityDataMaintenance.ProfilesExport.EmailAdddressTo
			// which contains the xml file as an attachment.
			//
			// ********Manual Testing needed***********

		}

		#endregion

		#region TestProductProfilesImport

		/// <summary>
		/// Tests the ProductProfilesImport maintenance routine
		/// This is a datagateway importer class and cannot be accessed via
		/// the console application
		/// </summary>
		public void TestProductProfilesImport()
		{			
			// Set up importer
			string importFileName = Properties.Current["AvailabilityDataMaintenance.TestImportFile"];

			// Test that incorrect parameters will cause importer to throw an error
			try
			{
				ProductProfilesImport invalidImporter = new  ProductProfilesImport(Properties.Current["Invalid feed name"], null, null, null, null);
				Assert.Fail("Error with TestProductProfilesImport. Invalid data feed name did not cause an exception");
			}
			catch
			{// expected - do nothing				
			}

			ProductProfilesImport importer = new  ProductProfilesImport(Properties.Current["datagateway.sqlimport.productavailability.feedname"], null, null, null, @"C:\TDPortal\codebase\TransportDirect\AvailabilityDataMaintenance\bin\debug\AvailabilityDataMaintenance\TestImport");

			// Test that importer runs successfully
			int result = importer.Run(importFileName);
			Assert.AreEqual(0, result, "Error with TestProductProfilesImport. Data importer did not return exit code of 0.");

			// Test that data was loaded
			SqlHelper sqlHelper = new SqlHelper();
			sqlHelper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			int recordCount = (int)sqlHelper.GetScalar("SELECT COUNT (*) FROM ProductProfile");
			Assert.AreEqual(8, recordCount, "Error with TestProductProfilesImport. Product profiles were not imported into database.");
			sqlHelper.ConnClose();
		}

		#endregion
	
	}
}
