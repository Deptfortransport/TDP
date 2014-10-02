// *********************************************** 
// NAME                 : TestParkAndRideCatalogue.cs
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 22/07/2005 
// DESCRIPTION  	    : Nunit class for testing the accessing of Park and Ride info
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestParkAndRideCatalogue.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:48   mturner
//Initial revision.
//
//   Rev 1.4   Mar 28 2006 15:52:42   rgreenwood
//Newkirk: removed tests dependent on DataGateway/Manual setup/GazopsWebTest.asmx
//
//   Rev 1.3   Mar 23 2006 17:58:34   build
//Automatically merged from branch for stream0025
//
//   Rev 1.2.1.1   Mar 20 2006 16:48:48   halkatib
//added methods to test immutability of the parkandride infor object
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.2.1.0   Mar 15 2006 13:59:32   tolomolaiye
//Addition of Car park data
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.2   Aug 23 2005 16:05:16   NMoorhouse
//Fix problem with data notifications
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.1   Aug 12 2005 11:14:06   NMoorhouse
//DN058 Park And Ride, end of CUT
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.0   Aug 03 2005 10:21:20   NMoorhouse
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
	/// Summary description for TestParkAndRideCatalogue.
	/// </summary>
	[TestFixture]
	[Ignore("NEWKIRK: Tests require access to Datagateway")]
	public class TestParkAndRideCatalogue
	{
		// Data file paths and names.
		string dataFileDirectory = string.Empty; 
		//Directory.GetCurrentDirectory() + @"\\travelnews";
		string datafilename  = string.Empty ; 
		string xmlfilename = string.Empty;
		int testReturnCode=0;
		int expectedParkAndRides;
		int actualParkAndRides;
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
				TDServiceDiscovery.Init( new ParkAndRideTestInitialization() );	
	
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
		public void TestGetAll()
		{
			IServiceFactory parkAndRideFactory = new ParkAndRideCatalogueFactory();
			IParkAndRideCatalogue regions = (IParkAndRideCatalogue)parkAndRideFactory.Get();
			ParkAndRideInfo[] parkAndRideInfo;
			expectedParkAndRides=56;

			//Get All. Five Locations in total have Park And Rides Sites.
			parkAndRideInfo = regions.GetAll();
			actualParkAndRides = parkAndRideInfo.Length;

			Assert.AreEqual(expectedParkAndRides, actualParkAndRides, "Incorrect number of Regions");
		}

		[Test] 
		public void TestGetRegion()
		{
			//Init for test
			IServiceFactory parkAndRideFactory = new ParkAndRideCatalogueFactory();
			IParkAndRideCatalogue regions = (IParkAndRideCatalogue)parkAndRideFactory.Get();
			ParkAndRideInfo[] parkAndRideInfo;
			expectedParkAndRides=5;

			//Get North West Region. Two Locations should have Park And Rides Sites.
			parkAndRideInfo = regions.GetRegion("8");
			actualParkAndRides = parkAndRideInfo.Length;

			Assert.AreEqual(expectedParkAndRides, actualParkAndRides, "Incorrect number of Regions");
		}

		/// <summary>
		/// Checks that data is reloaded successfully when the notification event fires.
		/// </summary>
		[Test]
		public void TestReloadData()
		{
			
			IServiceFactory parkAndRideFactory = new ParkAndRideCatalogueFactory();
			TestMockDataChangeNotification dataChangeNotification = (TestMockDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];
			ParkAndRideInfo[] parkAndRideInfo;
			expectedParkAndRides=5;


			//Get Region 10 (Scotland). One Locations should have Park And Rides Sites first time
			IParkAndRideCatalogue regions = (IParkAndRideCatalogue)parkAndRideFactory.Get();
			parkAndRideInfo = regions.GetRegion("10");
			actualParkAndRides = parkAndRideInfo.Length;

			Assert.AreEqual(expectedParkAndRides, actualParkAndRides, "Incorrect number of Regions");

			//Load second set of Park And Ride Test Data
			LoadParkAndRideData("ParkAndRideTestData2.csv");
			expectedParkAndRides=6;
			// Cause the changed event to be raised by the notification service
			dataChangeNotification.RaiseChangedEvent("ExternalLinks");
			dataChangeNotification.RaiseChangedEvent("ParkAndRide");

			//Get Region 10 again.  Locations should have Park And Rides Sites second time
			IParkAndRideCatalogue regions2 = (IParkAndRideCatalogue)parkAndRideFactory.Get();
			parkAndRideInfo = regions2.GetRegion("10");
			actualParkAndRides = parkAndRideInfo.Length;

			Assert.AreEqual(expectedParkAndRides, actualParkAndRides, "Incorrect number of Regions after reload");

		}

		/// <summary>
		/// Test the park info brought back is correct
		/// </summary>
		[Test]
		public void TestGetScheme()
		{
			IServiceFactory parkAndRideFactory = new ParkAndRideCatalogueFactory();
			IParkAndRideCatalogue regions = (IParkAndRideCatalogue)parkAndRideFactory.Get();
			ParkAndRideInfo[] parkAndRideInfo;

			//Get All. Five Locations in total have Park And Rides Sites.
			parkAndRideInfo = regions.GetAll();

			//save the first item brought back
			ParkAndRideInfo parkInfo1 = parkAndRideInfo[0];
			
			//use the getscheme method to read in the data of parkInfo1
			ParkAndRideInfo parkInfo = regions.GetScheme(parkInfo1.ParkAndRideId);

			Assert.IsNotNull(parkInfo, "Null ParkAndRideInfo object returned");

			//check easting and northing
			Assert.AreEqual(parkInfo.SchemeGridReference.Easting, parkInfo1.SchemeGridReference.Easting);
			Assert.AreEqual(parkInfo.SchemeGridReference.Northing, parkInfo1.SchemeGridReference.Northing);
			Assert.AreEqual(parkInfo.UrlLink, parkInfo1.UrlLink);
		}

		[Test] 
		public void TestChangedOsGridValues()
		{
			IServiceFactory parkAndRideFactory = new ParkAndRideCatalogueFactory();
			IParkAndRideCatalogue regions = (IParkAndRideCatalogue)parkAndRideFactory.Get();
			ParkAndRideInfo[] parkAndRideInfo;
			
			//Get All. Five Locations in total have Park And Rides Sites.
			parkAndRideInfo = regions.GetAll();

			OSGridReference osgr = parkAndRideInfo[0].SchemeGridReference;

			osgr.Easting = 999;
			osgr.Northing = 999;

			Assert.IsFalse(parkAndRideInfo[0].SchemeGridReference.Easting == 999,"Easting has been changed in catalogue when it should not have been.");
			Assert.IsFalse(parkAndRideInfo[0].SchemeGridReference.Northing == 999, "Northing has been changed in catalogue when it should not have been.");			
		}

		[Test] 
		public void TestChangedCarParkInfo()
		{
			IServiceFactory parkAndRideFactory = new ParkAndRideCatalogueFactory();
			IParkAndRideCatalogue regions = (IParkAndRideCatalogue)parkAndRideFactory.Get();
			ParkAndRideInfo[] parkAndRideInfo;
			
			//Get All. Five Locations in total have Park And Rides Sites.
			parkAndRideInfo = regions.GetAll();

			CarParkInfo cpi = new CarParkInfo(-1, "testCarParkName", "LinkId", 0, "comments", 999, 999);
			CarParkInfo[] cpiArray = parkAndRideInfo[0].GetCarParks();

			cpiArray[0] = cpi; 


			Assert.IsFalse(parkAndRideInfo[0].GetCarParks()[0].CarParkName == "testCarParkName", "CarPark array element in the catalogue has been changed");
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
	public class ParkAndRideTestInitialization : IServiceInitialisation
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
