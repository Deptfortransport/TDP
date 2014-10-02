// *********************************************** 
// NAME                 : TestCarParkInfo.cs
// AUTHOR               : Hassan Al Katib
// DATE CREATED         : 20/03/2005 
// DESCRIPTION  	    : Nunit class for testing the accessing of Car park info
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestCarParkInfo.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:44   mturner
//Initial revision.
//
//   Rev 1.1   Mar 28 2006 15:52:30   rgreenwood
//Newkirk: removed tests dependent on DataGateway/Manual setup/GazopsWebTest.asmx
//
//   Rev 1.0   Mar 20 2006 16:47:48   halkatib
//Initial revision.
//

using System;
using System.Collections; 
using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.Common.Logging;  
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery; 
using TransportDirect.UserPortal.ExternalLinkService;
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.DatabaseInfrastructure;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for TestCarParkInfo
	/// </summary>
	[TestFixture] 
	[Ignore("NEWKIRK: Requires Datagateway to import test data to DB")]
	public class TestCarParkInfo
	{
		// Data file paths and names.
		string dataFileDirectory = string.Empty; 
		//Directory.GetCurrentDirectory() + @"\\travelnews";
		string datafilename  = string.Empty ; 
		string xmlfilename = string.Empty;
		int testReturnCode=0;
		string InputFilepath = string .Empty ; 

		/// <summary>
		/// Initialisation in setup method called before every test method
		/// </summary>
		[TestFixtureSetUp]
		public void Init()
		{
			try
			{

				TDServiceDiscovery.ResetServiceDiscoveryForTest();
				// Initialise services
				TDServiceDiscovery.Init( new  CarParkTestInitialization() );	
	
				//Load Initial Park And Ride Test Data
				TestMockDataChangeNotification dataChangeNotification = (TestMockDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];
				LoadParkAndRideData("ParkAndRideTestData1.csv");
				dataChangeNotification.RaiseChangedEvent("ExternalLinks");

				// Put car parks data in
				bool InitSuccessful = ExecuteSetupScript("LocationService/TestCarParkImport.sql");
				Assert.AreEqual(true, InitSuccessful, "Database setup failed during initialisation. Unable to continue with tests.");


			}
			catch ( Exception e )
			{
				Assert.Fail( "Error in init: " + e.Message  );
				throw e;
			}
		}

		
		[TestFixtureTearDown]
		public void TearDown()
		{   
		}

		
		[Test] 
		public void TestChangedCarParkOsGridValues()
		{
			IServiceFactory parkAndRideFactory = new ParkAndRideCatalogueFactory();
			IParkAndRideCatalogue regions = (IParkAndRideCatalogue)parkAndRideFactory.Get();
			ParkAndRideInfo[] parkAndRideInfo;
			
			//Get All. Five Locations in total have Park And Rides Sites.
			parkAndRideInfo = regions.GetAll();

			OSGridReference osgr = parkAndRideInfo[0].GetCarParks()[0].GridReference;

			osgr.Easting = 999;
			osgr.Northing = 999;

			Assert.IsFalse(parkAndRideInfo[0].GetCarParks()[0].GridReference.Easting == 999,"Easting has been changed in the CarParkInfo object when it should not have been.");
			Assert.IsFalse(parkAndRideInfo[0].GetCarParks()[0].GridReference.Northing == 999, "Northing has been changed in the CarParkInfo object when it should not have been.");			
		}

		[Test] 
		public void TestChangedCarParkToids()
		{
			IServiceFactory parkAndRideFactory = new ParkAndRideCatalogueFactory();
			IParkAndRideCatalogue regions = (IParkAndRideCatalogue)parkAndRideFactory.Get();
			ParkAndRideInfo[] parkAndRideInfo;
			
			//Get All. Five Locations in total have Park And Rides Sites.
			parkAndRideInfo = regions.GetAll();

			string testToid = "TestToid";
			string[] testToidArray =  parkAndRideInfo[0].GetCarParks()[0].GetToids();

			testToidArray[0] = testToid; 

			Assert.IsFalse(parkAndRideInfo[0].GetCarParks()[0].GetToids()[0] == testToid, "CarPark toid element in the catalogue has been changed");
		}


		private string GetCurrentFolderPath()
		{   			
			string replaceVal = @"file:\";
			string folderPath = string.Empty;			
			folderPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
			folderPath = folderPath.Replace(replaceVal, "");			
			return folderPath;
			
		}

		/// <summary>
		/// Method used to copy input test file into the data gateway
		/// </summary>
		/// <param name="testFileName"></param>
		private void LoadParkAndRideData(string datafilename)
		{
			// Getting value from config
			dataFileDirectory = System.Configuration.ConfigurationManager.AppSettings["GatewayDataFolderPath"];
			xmlfilename = System.Configuration.ConfigurationManager.AppSettings["ImportXmlFileName"];

			InputFilepath = @"LocationService\" +	 datafilename;

			InputFilepath = System.IO.Path.Combine(GetCurrentFolderPath(), InputFilepath);  
			FileInfo file = new FileInfo(InputFilepath);
			File.SetAttributes( file.FullName, FileAttributes.Normal );

			//Create the input directory if it doesn't exist
			DirectoryInfo processingDir = new DirectoryInfo( dataFileDirectory );
				
			if ( !processingDir.Exists )
			{
				processingDir.Create();
			}

			//Copy the input file to the input directory for the test
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

			ParkAndRideImportTask import = new ParkAndRideImportTask(Properties.Current["datagateway.sqlimport.parkandride.feedname"], "", "", "", dataFileDirectory); 
			testReturnCode = (int)(import.Run(datafilename));
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

	/// <summary>
	/// Class to initialise services that are used by the tests.
	/// </summary>
	public class CarParkTestInitialization : IServiceInitialisation
	{	
       		
		/// <summary>
		/// Set up of Services and Logging
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
			serviceCache.Add(ServiceDiscoveryKey.DataChangeNotification, new TestMockDataChangeNotification());
			serviceCache.Add(ServiceDiscoveryKey.ExternalLinkService, new ExternalLinksFactory());
			serviceCache.Add (ServiceDiscoveryKey.GisQuery, new GisQueryFactory());
		}
	}
}
