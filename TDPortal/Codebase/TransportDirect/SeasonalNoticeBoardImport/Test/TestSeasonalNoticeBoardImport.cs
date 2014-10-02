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



namespace TransportDirect.UserPortal.SeasonalNoticeBoardImport
{

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
	/// <summary>
	/// Summary description for TestSeasonalNoticeBoardImport.
	/// </summary>
	
	[TestFixture] 
	public class TestSeasonalNoticeBoardImport
	{


		// Data file paths and names.
		string dataFileDirectory = string.Empty; 
			//Directory.GetCurrentDirectory() + @"\\travelnews";
		string datafilename  = "SeasonalInformation.csv";
		string xmlfilename = "SeasonalInformation.xml";
		int testReturnCode=0;
		int compareCode = -1;
		string InputFilepath = @"SeasonalNoticeBoardImport\SeasonalInformation.csv"; 


		public TestSeasonalNoticeBoardImport()
		{	
		}

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

				//Create the input directory if it doesn't exist
				//DirectoryInfo processingDir = new DirectoryInfo( Properties.Current["datagateway.data.live.directory"] );
				DirectoryInfo processingDir = new DirectoryInfo( dataFileDirectory );
				
				if ( !processingDir.Exists )
				{
					processingDir.Create();
				}

				//Copy the input file to the input directory for the test
				InputFilepath = System.IO.Path.Combine(GetCurrentFolderPath(), InputFilepath);  
				FileInfo file = new FileInfo(InputFilepath);
				File.SetAttributes( file.FullName, FileAttributes.Normal );
				if ( file.Exists )
				{
					string copyName = processingDir.FullName + @"\" + datafilename ;
					file.CopyTo( copyName, true );
					File.SetAttributes( copyName, FileAttributes.Normal );
				}
				else
				{
					throw new Exception( file.FullName + " does not exist" );
				}
			}
			catch ( Exception e )
			{
				Assert.Fail( "Error in init: " + e.Message  );
				throw e;
			}
		}



		
		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TearDown]
		public void TearDown()
		{
			//DirectoryInfo processingDir = new DirectoryInfo( Properties.Current["datagateway.data.live.directory"] );
			DirectoryInfo processingDir = new DirectoryInfo( dataFileDirectory );

			//Remove the csv file.
			FileInfo file = new FileInfo( processingDir.FullName + @"\" + datafilename );
			if ( file.Exists )
			{
				file.Refresh();
				file.Delete();
			}

			//Remove the xml file.
			file = new FileInfo( processingDir.FullName + @"\" + xmlfilename );
			if ( file.Exists )
			{
				file.Refresh();
				file.Delete();
			}
		}


		/// <summary>
		/// Test handling invalid source csv file name 
		/// </summary>
		[Test]
		public void TestImportCsvFileNotFound()
		{
			try
			{
				SeasonalNoticeBoardImport.SeasonalNoticeBoardDataImport  import = new SeasonalNoticeBoardImport.SeasonalNoticeBoardDataImport(Properties.Current["datagateway.sqlimport.seasonalInformation.feedname"], "", "", "", dataFileDirectory); 				
				testReturnCode = (int)(import.Run("foo.csv")) ;
				compareCode = (int)TDExceptionIdentifier.SNBCsvConversionFailed;
				//Assertion.Assert("No CSV file found " , (testReturnCode==7000));
				Assert.IsTrue((testReturnCode==compareCode), "No CSV file found ");
				
				  
			}	
			catch ( TDException e )
			{
				Assert.AreEqual(TDExceptionIdentifier.SNBImportFailed , e.Identifier, 
					"Expected to receive TDExceptionIdentifier.SNBCsvConversionFailed");
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
				SeasonalNoticeBoardImport.SeasonalNoticeBoardDataImport  import = new SeasonalNoticeBoardImport.SeasonalNoticeBoardDataImport("foo", "", "", "", "foo"); 
				//Assertion.Assert( " It passed instead of failed as given feedname doesn't exist. "  , (import.Run(datafilename) !=0   ));
				testReturnCode = (int)(import.Run(datafilename));
				compareCode = (int)TDExceptionIdentifier.SNBDataFeedNameNotFound;
				Assert.IsTrue((testReturnCode == compareCode),
					" It passed instead of failed as given feedname doesn't exist. ");

			}
			catch ( TDException e )
			{
				//Assertion.AssertEquals("Expected to receive TDExceptionIdentifier.DGUnexpectedFeedName", TDExceptionIdentifier.DGUnexpectedFeedName , e.Identifier );
				Assert.AreEqual(TDExceptionIdentifier.SNBImportFailed , e.Identifier,
					"Expected to receive TDExceptionIdentifier.SNBDataFeedNameNotFound");
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
				SeasonalNoticeBoardImport.SeasonalNoticeBoardDataImport  import = new SeasonalNoticeBoardImport.SeasonalNoticeBoardDataImport(Properties.Current["datagateway.sqlimport.seasonalInformation.feedname"], "", "", "", dataFileDirectory); 
				testReturnCode = (int)(import.Run(datafilename));
				compareCode = 0;

				Assert.IsTrue( testReturnCode == compareCode,
					" Data Import was not sucessfull ");
			}
			catch ( TDException e )
			{
				string temp = e.Message;
				Assert.AreEqual(TDExceptionIdentifier.SNBImportFailed,
					"Expected to receive TDExceptionIdentifier.DGUnexpectedFeedName");
			}
		}

		private string GetCurrentFolderPath()
		{   			
			string replaceVal = @"file:\";
			string folderPath = string.Empty;			
			folderPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
			folderPath = folderPath.Replace(replaceVal, "");			
			return folderPath;
			
		}
	}
}
