// *********************************************** 
// NAME                 : TestCoachRoutesQuotaFaresImportTask.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 05/10/2005 
// DESCRIPTION  		: Test class for  CoachRoutesQuotaFaresImportTask
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/CoachRoutes/TestCoachRoutesQuotaFaresImportTask.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:37:24   mturner
//Initial revision.
//
//   Rev 1.0   Oct 26 2005 09:56:34   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5   Oct 25 2005 18:00:56   schand
//Removed harcoded value for TestDataFile folder to config file.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.4   Oct 19 2005 13:04:32   schand
//Modified check for InValid error code.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.3   Oct 19 2005 12:16:42   schand
//Added region block
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.2   Oct 19 2005 11:49:22   schand
//Added one more Test function for data import with blank rows.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 11 2005 11:08:12   schand
//Added more tests
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 10 2005 14:36:54   schand
//Initial revision.


using System;
using TransportDirect.Common;
using System.Collections; 
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using TransportDirect.Datagateway;
using TransportDirect.Datagateway.Framework;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.PricingRetail.CoachRoutes
{
	/// <summary>
	/// Nunit test class for for CoachRoutesQuotaFaresImportTask.
	/// </summary>
	[TestFixture] 
	public class TestCoachRoutesQuotaFaresImportTask
	{
		#region Test constructor
		public TestCoachRoutesQuotaFaresImportTask()
		{   			
		}

		#endregion
		
		#region Variable Declarations
		string dataFileDirectory = string.Empty; 		
		string datafilename  = string.Empty ; 
		string xmlfilename = string.Empty;
		string additionalDatafilename  = string.Empty ; 
		string additionalXmlfilename = string.Empty;
		int testReturnCode=0;
		int compareCode = -1;
		string inputFilepath = string .Empty ;
		string inValidFileName;
		string blankRowsFileName;
		string additionalInValidFileName ;
		string additionalInputFilepath = string .Empty ;
		string invalidCsvfileExtension = "InValid.csv";
		string blankRowsCsvfileExtension = "WithSomeBlankRows.csv";
		string importFeedname;
		string projectDataFolderName;
		#endregion

 		
		#region NUnit Set up Methods 
		/// <summary>
		/// Initialisation in setup method called before every test method
		/// </summary>
		[SetUp]
		public void Init()
		{
			try
			{
				// Initialise services
				TDServiceDiscovery.Init( new  TestInitialization() );
				
				// Getting value from config
				dataFileDirectory = System.Configuration.ConfigurationManager.AppSettings["GatewayDataFolderPath"];
				datafilename = System.Configuration.ConfigurationManager.AppSettings["ImportDataFileName"];
				xmlfilename = System.Configuration.ConfigurationManager.AppSettings["ImportXmlFileName"];
				
				additionalDatafilename = System.Configuration.ConfigurationManager.AppSettings["AdditionalImportDataFileName"];
				additionalXmlfilename = System.Configuration.ConfigurationManager.AppSettings["AdditionalImportXmlFileName"];
				projectDataFolderName = System.Configuration.ConfigurationManager.AppSettings["ImportTestFilesFolderName"];

				inputFilepath = projectDataFolderName +	 datafilename;
				additionalInputFilepath = projectDataFolderName +	 additionalDatafilename;

				//Create the input directory if it doesn't exist				
				DirectoryInfo processingDir = new DirectoryInfo( dataFileDirectory );
				
				if ( !processingDir.Exists )
				{
					processingDir.Create();
				}

				//Copy the input file to the input directory for the test
				inputFilepath = System.IO.Path.Combine(GetCurrentFolderPath(), inputFilepath);  
				CopyTestFile(processingDir,inputFilepath);
				
				inValidFileName =  datafilename.Replace(".csv", invalidCsvfileExtension);
				CopyTestFile(processingDir,inputFilepath.Replace(".csv", invalidCsvfileExtension) , inValidFileName);

				additionalInputFilepath = System.IO.Path.Combine(GetCurrentFolderPath(), additionalInputFilepath);  
				CopyTestFile(processingDir,additionalInputFilepath,additionalDatafilename);
				
				additionalInValidFileName = additionalDatafilename.Replace(".csv", invalidCsvfileExtension);				
				CopyTestFile(processingDir,additionalInputFilepath.Replace(".csv", invalidCsvfileExtension) , additionalInValidFileName);

				blankRowsFileName =  datafilename.Replace(".csv", blankRowsCsvfileExtension);
				CopyTestFile(processingDir,inputFilepath.Replace(".csv", blankRowsCsvfileExtension) , blankRowsFileName);



				importFeedname =  Properties.Current["datagateway.sqlimport.coachroutesquotafares.feedname"];

			}
			catch ( Exception e )
			{
				Assert.Fail( "Error in init: " + e.Message  );				
			}
		}

	


		
		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TearDown]
		public void TearDown()
		{
			FileInfo file;
			//DirectoryInfo processingDir = new DirectoryInfo( Properties.Current["datagateway.data.live.directory"] );
			DirectoryInfo processingDir = new DirectoryInfo( dataFileDirectory );

			//Remove the csv file.
			file = new FileInfo( processingDir.FullName + @"\" + datafilename );
			DeleteTestFile(file); 

			file = new FileInfo( processingDir.FullName + @"\" + additionalDatafilename);
			DeleteTestFile(file); 

			//Remove the xml file.
			file = new FileInfo( processingDir.FullName + @"\" + xmlfilename );
			DeleteTestFile(file);

			//Remove the xml file.
			file = new FileInfo( processingDir.FullName + @"\" + additionalXmlfilename);
			DeleteTestFile(file);

			//Remove the invalid csv file.
			file = new FileInfo( processingDir.FullName + @"\" + datafilename.Replace(".csv", invalidCsvfileExtension));
			DeleteTestFile(file);

			file = new FileInfo( processingDir.FullName + @"\" + additionalDatafilename.Replace(".csv", invalidCsvfileExtension));
			DeleteTestFile(file);

			file = new FileInfo( processingDir.FullName + @"\" + datafilename.Replace(".csv", blankRowsCsvfileExtension));
			DeleteTestFile(file);
			
		}

		#endregion


		#region NUnit Test Methods
		/// <summary>
		/// Test handling invalid source csv file name 
		/// </summary>
		[Test]
		public void TestImportCsvFileNotFound()
		{
			try 			
			{
				CoachRoutesQuotaFaresImportTask import = new CoachRoutesQuotaFaresImportTask(importFeedname, "", "", "", dataFileDirectory); 
				testReturnCode = (int)(import.Run("foo.csv")) ;
				compareCode = (int)TDExceptionIdentifier.SBPPrimaryFileNotFound;				
				Assert.IsTrue((testReturnCode==compareCode), "No CSV file found ");
			}	
			catch ( TDException e )
			{
				Assert.AreEqual(TDExceptionIdentifier.SBPImportFailed , e.Identifier, 
					"Expected to receive TDExceptionIdentifier.SBPCsvConversionFailed");
			}
		}


		
		/// <summary>
		/// Test handling of invalid feed name.
		/// </summary>
		[Test]
		public void TestInvalidFeedName() 
		{
			try 
			{   				
				CoachRoutesQuotaFaresImportTask  import = new CoachRoutesQuotaFaresImportTask("foo", "", "", "", dataFileDirectory); 

				//Assertion.Assert( " It passed instead of failed as given feedname doesn't exist. "  , (import.Run(datafilename) !=0   ));
				testReturnCode = (int)(import.Run(datafilename));
				compareCode = (int)TDExceptionIdentifier.SBPDataFeedNameNotFound;
				Assert.IsTrue((testReturnCode == compareCode),
					" It passed instead of failed as the given feed name doesn't exist. ");  
			}
			catch ( TDException e )
			{
				//Assertion.AssertEquals("Expected to receive TDExceptionIdentifier.DGUnexpectedFeedName", TDExceptionIdentifier.DGUnexpectedFeedName , e.Identifier );
				Assert.AreEqual(TDExceptionIdentifier.SBPImportFailed , e.Identifier,
					"Expected to receive TDExceptionIdentifier.SBPDataFeedNameNotFound");
			}
		}

		
		/// <summary>
		/// Test whether data has push correctly or not.
		/// </summary>
		[Test]
		public void TestDataImport() 
		{
			try 
			{
				CoachRoutesQuotaFaresImportTask import = new CoachRoutesQuotaFaresImportTask(importFeedname, "", "", "", dataFileDirectory); 
				testReturnCode = (int)(import.Run(datafilename));
				compareCode = 0;

				Assert.IsTrue( testReturnCode == compareCode, " Data Import was not successful ");

				DirectoryInfo processingDir = new DirectoryInfo( dataFileDirectory );

				//Remove the csv file.
				FileInfo file = new FileInfo( processingDir.FullName + @"\" + datafilename );
				DeleteTestFile(file); 

			}
			catch ( TDException e )
			{
				string temp = e.Message;
				Assert.AreEqual(TDExceptionIdentifier.SBPImportFailed,
					"Expected to receive TDExceptionIdentifier.DGUnexpectedFeedName" + temp);
			}
		}


		/// <summary>
		/// Test whether data has push correctly or not for an invalid file.
		/// </summary>
		[Test]
		public void TestDataImportInvalidFile() 
		{
			try 
			{	
				// deleting existing file if any 
				DirectoryInfo processingDir = new DirectoryInfo( dataFileDirectory );

				//Remove the csv file.
				FileInfo file = new FileInfo( processingDir.FullName + @"\" + datafilename );
				DeleteTestFile(file);
				
				string aliasfile = inputFilepath.Replace(".csv", invalidCsvfileExtension);
				CopyTestFile(processingDir,aliasfile);  

				CoachRoutesQuotaFaresImportTask import = new CoachRoutesQuotaFaresImportTask(importFeedname, "", "", "", dataFileDirectory); 
				testReturnCode = (int)(import.Run(datafilename));
				compareCode = (int)TDExceptionIdentifier.SBPPrimaryXmlValidationFails;

				Assert.IsTrue( testReturnCode == compareCode,
					" Data Import was not successful ");
			}
			catch ( TDException e )
			{
				string temp = e.Message;
				Assert.AreEqual(TDExceptionIdentifier.SBPImportFailed,
					"Expected to receive TDExceptionIdentifier.DGUnexpectedFeedName" + temp);
			}
		}


		/// <summary>
		/// Test whether data has push correctly or not for an invalid file.
		/// </summary>
		[Test]
		public void TestDataImportDataWithBlankRows() 
		{
			try 
			{	
				// deleting existing file if any 
				DirectoryInfo processingDir = new DirectoryInfo( dataFileDirectory );

				//Remove the csv file.
				FileInfo file = new FileInfo( processingDir.FullName + @"\" + datafilename );
				DeleteTestFile(file);
				
				string aliasfile = inputFilepath.Replace(".csv", blankRowsCsvfileExtension);
				CopyTestFile(processingDir,aliasfile);  

				CoachRoutesQuotaFaresImportTask import = new CoachRoutesQuotaFaresImportTask(importFeedname, "", "", "", dataFileDirectory); 
				testReturnCode = (int)(import.Run(datafilename));
				compareCode = (int)TDExceptionIdentifier.SBPPrimaryXmlValidationFails;

				Assert.IsTrue( testReturnCode == compareCode,
					" Data Import was not successful ");
			}
			catch ( TDException e )
			{
				string temp = e.Message;
				Assert.AreEqual(TDExceptionIdentifier.SBPImportFailed,
					"Expected to receive TDExceptionIdentifier.DGUnexpectedFeedName" + temp);
			}
		}


		#endregion
		
		
		#region Test Helper Methods
		/// <summary>
		/// Helper function to get the current execution path
		/// </summary>
		/// <returns>execution location path</returns>
		private string GetCurrentFolderPath()
		{   			
			string replaceVal = @"file:\";
			string folderPath = string.Empty;			
			folderPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
			folderPath = folderPath.Replace(replaceVal, "");			
			return folderPath;
			
		}


		
		/// <summary>
		/// Helper function to copy the file 
		/// </summary>
		/// <param name="processingDir">Destination directory</param>
		/// <param name="filepath">Input file path</param>
		private void CopyTestFile(DirectoryInfo processingDir, string filepath)
		{
			FileInfo file = new FileInfo(filepath);
			File.SetAttributes( file.FullName, FileAttributes.Normal );
			if ( file.Exists )
			{
				string copyName = processingDir.FullName + @"\" + datafilename ;
				file.CopyTo( copyName, true );
				File.SetAttributes( copyName, FileAttributes.Normal );
			}
			else
			{   				
				Assert.Fail(file.FullName + " does not exist");  
			}
		}


		/// <summary>
		/// Helper function to copy the file 
		/// </summary>
		/// <param name="processingDir">Destination directory</param>
		/// <param name="filepath">Input file path</param>
		/// <param name="aliasDataFileName">Name of the output file</param>
		private void CopyTestFile(DirectoryInfo processingDir, string filepath, string aliasDataFileName)
		{
			FileInfo file = new FileInfo(filepath);
			File.SetAttributes( file.FullName, FileAttributes.Normal );
			if ( file.Exists )
			{
				string copyName = processingDir.FullName + @"\" + aliasDataFileName ;
				file.CopyTo( copyName, true );
				File.SetAttributes( copyName, FileAttributes.Normal );
			}
			else
			{
				Assert.Fail( file.FullName + " does not exist" );
			}
		}


		/// <summary>
		/// Helper function to delete the file
		/// </summary>
		/// <param name="file">Input file</param>
		private static void DeleteTestFile(FileInfo file)
		{
			try
			{
				if ( file.Exists )
				{
					file.Refresh();
					file.Delete();
				}
			}
			catch
			{
				// do nothing
			}
		}


		#endregion
	}

	#region Test Initialisation class
	/// <summary>
	/// Class to initialise services that are used by the tests.
	/// </summary>
	public class TestInitialization : IServiceInitialisation
	{

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
}
