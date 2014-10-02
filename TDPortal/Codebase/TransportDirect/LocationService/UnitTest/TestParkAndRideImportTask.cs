// *********************************************** 
// NAME                 : TestParkAndRideImportTask.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 02/08/2005 
// DESCRIPTION  	    : Test class for ParkAndRideImportTask
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestParkAndRideImportTask.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:48   mturner
//Initial revision.
//
//   Rev 1.6   Apr 25 2006 10:14:44   mtillett
//Fix up unit test
//
//   Rev 1.5   Mar 28 2006 15:52:42   rgreenwood
//Newkirk: removed tests dependent on DataGateway/Manual setup/GazopsWebTest.asmx
//
//   Rev 1.4   Aug 25 2005 10:38:40   Schand
//Additional change for Code review.
//
//   Rev 1.3   Aug 25 2005 10:10:26   Schand
//Added one more test TestDataImportInvalidFile().
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.2   Aug 15 2005 11:42:26   Schand
//Update for ExceptionIdentifier code
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.1   Aug 11 2005 18:50:46   Schand
//Updates for FxCop review.
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.0   Aug 03 2005 11:07:52   Schand
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


namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Nunit test class for ParkAndRideImportTask.
	/// </summary>
	[TestFixture] 
	public class TestParkAndRideImportTask
	{
		public TestParkAndRideImportTask()
		{   		
		}

		// Data file paths and names.
		string dataFileDirectory = string.Empty; 		
		string datafilename  = string.Empty ; 
		int testReturnCode=0;
		int compareCode = -1;
		string invalidCsvfileExtension = "ParkAndRideInValid.csv"; 

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
				dataFileDirectory = Directory.GetCurrentDirectory() + "\\LocationService";
				datafilename = System.Configuration.ConfigurationManager.AppSettings["ImportDataFileName"];
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
		}


		/// <summary>
		/// Test handling invalid source csv file name 
		/// </summary>
		[Test]
		public void TestImportCsvFileNotFound()
		{
			try 			
			{
				ParkAndRideImportTask import = new ParkAndRideImportTask(Properties.Current["datagateway.sqlimport.parkandride.feedname"], "", "", "", dataFileDirectory); 
				testReturnCode = (int)(import.Run("foo.csv")) ;
				compareCode = (int)TDExceptionIdentifier.PRDCsvConversionFailed;
				Assert.IsTrue((testReturnCode==compareCode), "No CSV file found ");
			}	
			catch ( TDException e )
			{
				Assert.AreEqual(TDExceptionIdentifier.PRDImportFailed , e.Identifier, 
					"Expected to receive TDExceptionIdentifier.PRDCsvConversionFailed");
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
				ParkAndRideImportTask import = new ParkAndRideImportTask("foo", "", "", "", dataFileDirectory); 

				testReturnCode = (int)(import.Run(datafilename));
				compareCode = (int)TDExceptionIdentifier.PRDDataFeedNameNotFound;
				Assert.IsTrue((testReturnCode == compareCode),
					" It passed instead of failed as given feed name doesn't exist. ");  
			}
			catch ( TDException e )
			{
				Assert.AreEqual(TDExceptionIdentifier.PRDImportFailed , e.Identifier,
					"Expected to receive TDExceptionIdentifier.PRDDataFeedNameNotFound");
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
				//must run the car park import, becuase of dependency
				TestCarParkImport car = new TestCarParkImport();
				car.TestDataImport();

				ParkAndRideImportTask import = new ParkAndRideImportTask(Properties.Current["datagateway.sqlimport.parkandride.feedname"], "", "", "", dataFileDirectory); 
				testReturnCode = (int)(import.Run(datafilename));
				compareCode = 0;

				Assert.AreEqual(compareCode, testReturnCode,
					" Data Import was not successful ");
			}
			catch ( TDException e )
			{
				string temp = e.Message;
				Assert.AreEqual(TDExceptionIdentifier.PRDImportFailed,
					"Expected to receive TDExceptionIdentifier.DGUnexpectedFeedName" + temp);
			}
		}


		/// <summary>
		/// Test whether data has push correctly or not.
		/// </summary>
		[Test]
		public void TestDataImportInvalidFile() 
		{
			try 
			{	
				ParkAndRideImportTask import = new ParkAndRideImportTask(Properties.Current["datagateway.sqlimport.parkandride.feedname"], "", "", "", dataFileDirectory); 
				testReturnCode = (int)(import.Run(invalidCsvfileExtension));
				compareCode = (int)TDExceptionIdentifier.PRDCsvConversionFailed;

				Assert.AreEqual(compareCode, testReturnCode,
					" Data Import was not successful ");
			}
			catch ( TDException e )
			{
				string temp = e.Message;
				Assert.AreEqual(TDExceptionIdentifier.PRDImportFailed,
					"Expected to receive TDExceptionIdentifier.DGUnexpectedFeedName" + temp);
			}
		}
	}


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
	
}
