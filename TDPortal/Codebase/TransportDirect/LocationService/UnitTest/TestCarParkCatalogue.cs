// *********************************************** 
// NAME                 : TestCarParkCatalogue.cs
// AUTHOR               : Esther Severn
// DATE CREATED         : 15/08/2006 
// DESCRIPTION  	    : Nunit class for testing access
//						  to Car Park data
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestCarParkCatalogue.cs-arc  $ 
//
//   Rev 1.2   Mar 16 2010 15:41:02   mmodi
//Updated car park reference to pass the test
//Resolution for 5461: TD Extra - Code review changes
//
//   Rev 1.1   Oct 13 2008 16:46:18   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.0   Jun 20 2008 14:53:48   mmodi
//Updated for cycle journeys, query methods
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:25:42   mturner
//Initial revision.
//
//   Rev 1.3   Sep 18 2006 17:14:22   tmollart
//Modified test class for thread safety  issue.
//Resolution for 4190: Thread Safety Issue on Car Park Catalogue
//
//   Rev 1.2   Sep 12 2006 10:12:12   esevern
//Amendment inline with changes to car park catalogue (removed GetAll()). Only single car park will now be loaded.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.1   Aug 15 2006 15:49:50   esevern
//Interim check in for developer integration
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.0   Aug 15 2006 15:38:06   esevern
//Initial revision.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
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
	/// Summary description for TestCarParkCatalogue.
	/// </summary>
	[TestFixture]
//	[Ignore("NEWKIRK: Tests require access to Datagateway")] 
	public class TestCarParkCatalogue
	{
		
		// Data file paths and names.
		string dataFileDirectory = string.Empty; 
		string datafilename  = string.Empty ; 
		string xmlfilename = string.Empty;
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
				TDServiceDiscovery.Init( new CarParkingTestInitialization() );	
	
				//Load Initial Car Park Test Data
				TestMockDataChangeNotification dataChangeNotification = (TestMockDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];
				
				dataChangeNotification.RaiseChangedEvent("ExternalLinks");
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

		public void TestGetCarPark()
		{
			IServiceFactory carParkFactory = new CarParkCatalogueFactory();
			ICarParkCatalogue carParkCatalogue = (ICarParkCatalogue)carParkFactory.Get();
			string carParkRef = "107109"; //valid car park reference 

			CarPark carPark = carParkCatalogue.GetCarPark(carParkRef);
			
			Assert.IsNotNull(carPark, "No car park returned");
		}

		/// <summary>
		/// Checks that data is reloaded successfully when the notification event fires.
		/// </summary>
		[Test]
		public void TestReloadData()
		{
			
			IServiceFactory carParkFactory = new CarParkCatalogueFactory();
			TestMockDataChangeNotification dataChangeNotification = (TestMockDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];

			ICarParkCatalogue carParkCatalogue = (ICarParkCatalogue)carParkFactory.Get();
            string firstRef = carParkCatalogue.GetCarPark("1000").CarParkReference;

			// Cause the changed event to be raised by the notification service
			dataChangeNotification.RaiseChangedEvent("ExternalLinks");
			dataChangeNotification.RaiseChangedEvent("CarPark");

            string secondRef = carParkCatalogue.GetCarPark("1000").CarParkReference;

			Assert.AreEqual(firstRef, secondRef, "Car parks are different after reload");

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

		/// <summary>
		/// Method used to copy input test file into the data gateway
		/// </summary>
		/// <param name="testFileName"></param>
		private bool LoadCarParkingData(string datafilename)
		{
			// Getting value from config
			dataFileDirectory = System.Configuration.ConfigurationManager.AppSettings["CarParkingGatewayDataFolderPath"];
			xmlfilename = System.Configuration.ConfigurationManager.AppSettings["CarParkingImportXmlFileName"];

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

			return true;
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

	/// <summary>
	/// Class to initialise services that are used by the tests.
	/// </summary>
	public class CarParkingTestInitialization : IServiceInitialisation
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
