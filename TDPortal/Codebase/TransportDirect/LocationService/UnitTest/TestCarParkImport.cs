// ******************************************************* 
// NAME                 : TestCarParkImport.cs
// AUTHOR               : Tolu Olomolaiye
// DATE CREATED         : 14 March 2006 
// DESCRIPTION  	    : Test class for Car Park Import
// ******************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestCarParkImport.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:42   mturner
//Initial revision.
//
//   Rev 1.4   Apr 25 2006 09:54:48   mtillett
//Fix unit test
//
//   Rev 1.3   Mar 28 2006 15:52:16   rgreenwood
//Newkirk: removed tests dependent on DataGateway/Manual setup/GazopsWebTest.asmx
//
//   Rev 1.2   Mar 20 2006 19:02:44   tolomolaiye
//Updated with code review comments
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.1   Mar 15 2006 13:51:26   tolomolaiye
//Further updates
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.0   Mar 14 2006 17:16:54   tolomolaiye
//Initial revision.
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2

using System;
using System.Collections;
using System.IO;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;
using NUnit.Framework;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Nunit test class for Car Park Import
	/// </summary>
	[TestFixture]
	public class TestCarParkImport
	{
		private const string currentProperty = "datagateway.sqlimport.carpark.feedname";
		private readonly string processingDirectory = Directory.GetCurrentDirectory();
		private int testReturnCode = 0;		
		private int compareCode = -1;
		private string invalidCsvfileExtension = "CarParkInValid.csv"; 
		private string datafilename  = string.Empty ; 
		private string dataFileDirectory = string.Empty; 		

		/// <summary>
		/// Empty constructor to allow instantiation
		/// </summary>
		public TestCarParkImport()
		{}

		/// <summary>
		/// Setup Initialisation method for all NUnit test scripts
		/// </summary>
		[TestFixtureSetUp]
		public void Init()
		{	
			try
			{
				// Initialise services
				TDServiceDiscovery.Init( new  TestInitialisation() );

				// Get config values
				dataFileDirectory = Directory.GetCurrentDirectory() + "\\LocationService";
				datafilename = System.Configuration.ConfigurationManager.AppSettings["ImportDataFileName"];
			}
			catch
			{
				throw new TDException("Set up failed for DataServices.", false, TDExceptionIdentifier.AETDTraceInitFailed);	
			}
		}

		/// <summary>
		/// Test that a valid Car Park .csv file can be correctly loaded into the application
		/// </summary>
		[Test]
		public void TestDataImport()
		{
			const string datafilename = "LocationService/CarPark.csv";
			const int importReturnCode = 0;

			CarParkImportTask importCarPark = new CarParkImportTask(Properties.Current[currentProperty], "", "", "", processingDirectory);
 
			int testResult = importCarPark.Run(datafilename);

			Assert.AreEqual(importReturnCode, testResult, "Car park Data Import Failed");
		}
		/// <summary>
		/// Test handling invalid source csv file name 
		/// </summary>
		[Test]
		public void TestImportCsvFileNotFound()
		{
			try 			
			{
				CarParkImportTask importCarPark = new CarParkImportTask(Properties.Current["datagateway.sqlimport.carpark.feedname"], string.Empty, string.Empty, string.Empty, dataFileDirectory); 
				testReturnCode = (int)(importCarPark.Run("foo.csv")) ;
				compareCode = (int)TDExceptionIdentifier.CPCsvConversionFailed;
				Assert.IsTrue((testReturnCode == compareCode), "No CSV file found ");
			}	
			catch ( TDException tdEx )
			{
				Assert.AreEqual(TDExceptionIdentifier.CPImportFailed , tdEx.Identifier, 
					"Expected to receive TDExceptionIdentifier.CPImportFailed");
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
				CarParkImportTask importCarPark = new CarParkImportTask("foo", string.Empty, string.Empty, string.Empty, dataFileDirectory); 

				testReturnCode = (int)(importCarPark.Run(datafilename));
				compareCode = (int)TDExceptionIdentifier.CPDataFeedNameNotFound;
				Assert.IsTrue((testReturnCode == compareCode),
					" Test passed even though feed name could not be found. ");  
			}
			catch ( TDException tdEx )
			{
				Assert.AreEqual(TDExceptionIdentifier.CPImportFailed , tdEx.Identifier,
					"Expected to receive TDExceptionIdentifier.CPDataFeedNameNotFound");
			}
		}

		/// <summary>
		/// Tests that an invalid file cannot be loaded into the application
		/// </summary>
		[Test]
		public void TestDataImportInvalidFile() 
		{
			try 
			{	
				CarParkImportTask importCarPark = new CarParkImportTask(Properties.Current["datagateway.sqlimport.carpark.feedname"], string.Empty, string.Empty, string.Empty, dataFileDirectory); 
				testReturnCode = (int)(importCarPark.Run(invalidCsvfileExtension));
				compareCode = (int)TDExceptionIdentifier.CPImportFailed;

				Assert.AreEqual(compareCode,testReturnCode,
					" Data Import was not successful ");
			}
			catch ( TDException tdEx )
			{
				Assert.AreEqual(TDExceptionIdentifier.PRDImportFailed,
					"Expected to receive TDExceptionIdentifier.DGUnexpectedFeedName: " + tdEx.Message.ToString());
			}
		}

		#region Initialisation Class
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
}