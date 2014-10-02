// *********************************************** 
// NAME                 : TestCoachRoutesQuotaFaresProvider.cs
// AUTHOR               : Russell Wilby
// DATE CREATED         : 10/10/2005 
// DESCRIPTION  		: Test class for  TestCoachRoutesQuotaFaresImportTask
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/CoachRoutes/TestCoachRoutesQuotaFaresProvider.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:37:24   mturner
//Initial revision.
//
//   Rev 1.1   Nov 06 2005 14:00:50   RPhilpott
//Compilation warnings.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 26 2005 09:56:34   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Oct 19 2005 18:13:50   RWilby
//Updated to comply with FxCop
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.2   Oct 19 2005 14:51:46   RWilby
//Corrected spelling of  "FindRoutesAndQuotaFares" method
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 16:41:54   RWilby
//Updated to re-import initial data set on TearDown
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 10:02:00   RWilby
//Initial revision.
//Resolution for 2818: DEL 7.3 Stream: Search by Price



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
using TransportDirect.UserPortal.DataServices;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.PricingRetail.CoachRoutes
{
	/// <summary>
	/// Nunit test class for for CoachRoutesQuotaFaresImportTask.
	/// </summary>
	[TestFixture] 
	public class TestCoachRoutesQuotaFaresProvider
	{
		#region private members
		// Data file paths and names.
		string dataFileDirectory = string.Empty; 		
		string datafile1name  = string.Empty ; 
		string datafile2name  = string.Empty ; 
		string xmlfilename = string.Empty;
		string additionalDatafile1name  = string.Empty ; 
		string additionalDatafile2name  = string.Empty ;
		string additionalXmlfilename = string.Empty;
		string inputFilepath = string .Empty ;
		string additionalInputFilepath = string .Empty ; 
		string importFeedname;
		const string COACHROUTES_FILENAME = "CoachRoutesData.csv";
		const string QUOTAFARE_FILENAME = "QuotaFares.csv";
		string projectDataFolderName = @"CoachRoutes\";
		#endregion

		#region Setup/TearDown
		/// <summary>
		/// Initialisation in setup method called before every test method
		/// </summary>
		[SetUp]
		public void Init()
		{
			try
			{
				// Initialise services
				TDServiceDiscovery.Init( new  TestCoachRoutesQuotaFaresProviderTestInitialization() );

				TestMockDataChangeNotification dataChangeNotification = AddDataChangeNotification();
				
				// Getting value from config
				dataFileDirectory = System.Configuration.ConfigurationManager.AppSettings["GatewayDataFolderPath"];
                datafile1name = System.Configuration.ConfigurationManager.AppSettings["ImportTestDataFile1Name"];
                datafile2name = System.Configuration.ConfigurationManager.AppSettings["ImportTestDataFile2Name"];
                xmlfilename = System.Configuration.ConfigurationManager.AppSettings["ImportXmlFileName"];

                additionalDatafile1name = System.Configuration.ConfigurationManager.AppSettings["ImportTestAdditionalDataFile1Name"];
                additionalDatafile2name = System.Configuration.ConfigurationManager.AppSettings["ImportTestAdditionalDataFile2Name"];
                additionalXmlfilename = System.Configuration.ConfigurationManager.AppSettings["AdditionalImportXmlFileName"];

				//Create the input directory if it doesn't exist				
				DirectoryInfo processingDir = new DirectoryInfo( dataFileDirectory );
				
				if ( !processingDir.Exists )
				{
					processingDir.Create();
				}
				importFeedname =  Properties.Current["datagateway.sqlimport.coachroutesquotafares.feedname"];

			}
			catch ( Exception e )
			{
				Assert.Fail( "Error in init: " + e.Message  );				
			}
		}

		/// <summary>
		/// Imports test data set 1
		/// </summary>
		/// <returns></returns>
		private int ImportDataSet1()
		{
			DeleteDataFiles();

			DirectoryInfo processingDir = new DirectoryInfo( dataFileDirectory );

			//Copy Test data file 1
			inputFilepath = projectDataFolderName +	 datafile1name;
			inputFilepath = System.IO.Path.Combine(GetCurrentFolderPath(), inputFilepath);  
			CopyTestFile(processingDir,inputFilepath,COACHROUTES_FILENAME);

			//Copy Test additional data file 1
			additionalInputFilepath = projectDataFolderName +	 additionalDatafile1name;
			additionalInputFilepath = System.IO.Path.Combine(GetCurrentFolderPath(), additionalInputFilepath);  
			CopyTestFile(processingDir,additionalInputFilepath,QUOTAFARE_FILENAME);
			
			CoachRoutesQuotaFaresImportTask import = new CoachRoutesQuotaFaresImportTask(importFeedname, "", "", "", dataFileDirectory); 
			return (int)(import.Run(COACHROUTES_FILENAME));
		}

		/// <summary>
		/// Imports test data data 2
		/// </summary>
		/// <returns></returns>
		private int ImportDataSet2()
		{
			DeleteDataFiles();
			
			DirectoryInfo processingDir = new DirectoryInfo( dataFileDirectory );

			//Copy Test data file 2
			inputFilepath = projectDataFolderName +	 datafile2name;
			inputFilepath = System.IO.Path.Combine(GetCurrentFolderPath(), inputFilepath);  
			CopyTestFile(processingDir,inputFilepath,COACHROUTES_FILENAME);

			//Copy Test additional data file 2
			additionalInputFilepath = projectDataFolderName +	 additionalDatafile2name;
			additionalInputFilepath = System.IO.Path.Combine(GetCurrentFolderPath(), additionalInputFilepath);  
			CopyTestFile(processingDir,additionalInputFilepath,QUOTAFARE_FILENAME);


			CoachRoutesQuotaFaresImportTask import = new CoachRoutesQuotaFaresImportTask(importFeedname, "", "", "", dataFileDirectory); 
			return (int)(import.Run(COACHROUTES_FILENAME));
		}

		/// <summary>
		/// Delete temp test files
		/// </summary>
		private void DeleteDataFiles()
		{
			FileInfo file;
			DirectoryInfo processingDir = new DirectoryInfo( dataFileDirectory );

			//Remove the csv files.
			file = new FileInfo( processingDir.FullName + @"\" + COACHROUTES_FILENAME );
			DeleteTestFile(file); 

			file = new FileInfo( processingDir.FullName + @"\" + QUOTAFARE_FILENAME );
			DeleteTestFile(file); 

			//Remove the XML files that are produced my the gateway process
			file = new FileInfo( processingDir.FullName + @"\" + xmlfilename );
			DeleteTestFile(file); 

			file = new FileInfo( processingDir.FullName + @"\" + additionalXmlfilename );
			DeleteTestFile(file); 
		}

		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TearDown]
		public void TearDown()
		{
			//Import the inital data set
			int testReturnCode = ImportDataSet1();

			DeleteDataFiles();
		}

		#endregion

		#region The Tests
		/// <summary>
		/// Tests Coach Routes with exchange points
		/// </summary>
		[Test]
		public void TestCoachRoutesWithExchangePoints()
		{
			try 
			{
				int testReturnCode = ImportDataSet1();

				Assert.IsTrue( testReturnCode == 0,
					" Data Set1 Import was not successful ");


				//The Test
				ICoachRoutesQuotaFaresProvider coachRoutesQuotaFaresProvider = new CoachRoutesQuotaFareProvider();
			
				//London to Dundee. We expect 3 Routes each with 2 Legs.
				RouteList routeList = coachRoutesQuotaFaresProvider.FindRoutesAndQuotaFares("900057366","900090097");

				Assert.AreEqual(3, routeList.Count,"Incorrect number of routes");

				//Test Route 1
				Assert.AreEqual(routeList[0].LegList.Count,2,"Incorrect number of route leg - should contain 2");
	
				Assert.AreEqual("NX",routeList[0].LegList[0].CoachOperatorCode,"Incorrect CoachOperatorCode for Leg");
				Assert.AreEqual("900057366",routeList[0].LegList[0].StartNaPTAN,"Incorrect StartNaPTAN for Leg");
				Assert.AreEqual("900067157",routeList[0].LegList[0].EndNaPTAN,"Incorrect EndNaPTAN for Leg");

				Assert.AreEqual("SCL",routeList[0].LegList[1].CoachOperatorCode,"Incorrect CoachOperatorCode for Leg");
				Assert.AreEqual("900067157",routeList[0].LegList[1].StartNaPTAN,"Incorrect StartNaPTAN for Leg");
				Assert.AreEqual("900090097",routeList[0].LegList[1].EndNaPTAN,"Incorrect EndNaPTAN for Leg");

				//Test Route 2
				Assert.AreEqual(routeList[1].LegList.Count,2,"Incorrect number of route leg - should contain 2");

				Assert.AreEqual("NX",routeList[1].LegList[0].CoachOperatorCode,"Incorrect CoachOperatorCode for Leg");
				Assert.AreEqual("900057366",routeList[1].LegList[0].StartNaPTAN,"Incorrect StartNaPTAN for Leg");
				Assert.AreEqual("900066154",routeList[1].LegList[0].EndNaPTAN,"Incorrect EndNaPTAN for Leg");

				Assert.AreEqual("SCL",routeList[1].LegList[1].CoachOperatorCode,"Incorrect CoachOperatorCode for Leg");
				Assert.AreEqual("900066154",routeList[1].LegList[1].StartNaPTAN,"Incorrect StartNaPTAN for Leg");
				Assert.AreEqual("900090097",routeList[1].LegList[1].EndNaPTAN,"Incorrect EndNaPTAN for Leg");
				
				//Test Route 3
				Assert.AreEqual(routeList[2].LegList.Count,2,"Incorrect number of route leg - should contain 2");
				
				Assert.AreEqual("NX",routeList[2].LegList[0].CoachOperatorCode,"Incorrect CoachOperatorCode for Leg");
				Assert.AreEqual("900057366",routeList[2].LegList[0].StartNaPTAN,"Incorrect StartNaPTAN for Leg");
				Assert.AreEqual("900066157",routeList[2].LegList[0].EndNaPTAN,"Incorrect EndNaPTAN for Leg");

				Assert.AreEqual("SCL",routeList[2].LegList[1].CoachOperatorCode,"Incorrect CoachOperatorCode for Leg");
				Assert.AreEqual("900066157",routeList[2].LegList[1].StartNaPTAN,"Incorrect StartNaPTAN for Leg");
				Assert.AreEqual("900090097",routeList[2].LegList[1].EndNaPTAN,"Incorrect EndNaPTAN for Leg");

			}
			catch ( TDException e )
			{
				string temp = e.Message;
				Assert.AreEqual(TDExceptionIdentifier.SBPImportFailed,
					"Expected to receive TDExceptionIdentifier.DGUnexpectedFeedName" + temp);
			}
			finally
			{
				DeleteDataFiles();
			}
		}


		/// <summary>
		/// Tests Coach Routes with no exchange points
		/// </summary>
		[Test]
		public void TestCoachRoutesWithNoExchangePoints()
		{
			try 
			{
				int testReturnCode = ImportDataSet1();

				Assert.IsTrue( testReturnCode == 0,
					" Data Set1 Import was not successful ");


				//The Test
				ICoachRoutesQuotaFaresProvider coachRoutesQuotaFaresProvider = new CoachRoutesQuotaFareProvider();
			
				//Oxford to Leeds.
				RouteList routeList = coachRoutesQuotaFaresProvider.FindRoutesAndQuotaFares("900049073","900076052");

				Assert.AreEqual(1, routeList.Count,"Incorrect number of routes");


				//Test Route 1
				Assert.AreEqual(routeList[0].LegList.Count,1,"Incorrect number of route leg - should contain 1");
	
				Assert.AreEqual("NX",routeList[0].LegList[0].CoachOperatorCode,"Incorrect CoachOperatorCode for Leg");
				Assert.AreEqual("900049073",routeList[0].LegList[0].StartNaPTAN,"Incorrect StartNaPTAN for Leg");
				Assert.AreEqual("900076052",routeList[0].LegList[0].EndNaPTAN,"Incorrect EndNaPTAN for Leg");
				
				//Test foreach block
				foreach (Route r in routeList)
				{
					foreach (Leg l in r.LegList)
					{
						Assert.AreEqual("NX",l.CoachOperatorCode,"Incorrect CoachOperatorCode for Leg in foreach block");
						Assert.AreEqual("900049073",l.StartNaPTAN,"Incorrect StartNaPTAN for Leg in foreach block");
						Assert.AreEqual("900076052",l.EndNaPTAN,"Incorrect EndNaPTAN for Leg in foreach block");
						Assert.AreEqual(0,l.QuotaFareList.Count,"Incorrect EndNaPTAN for Leg in foreach block");
					}
				
				}


			}
			catch ( TDException e )
			{
				string temp = e.Message;
				Assert.AreEqual(TDExceptionIdentifier.SBPImportFailed,
					"Expected to receive TDExceptionIdentifier.DGUnexpectedFeedName" + temp);
			}
			finally
			{
				DeleteDataFiles();
			}
		}

		/// <summary>
		/// Tests Coach Routes with invalid operators
		/// </summary>
		[Test]
		public void TestCoachRoutesWithInvalidOperators()
		{
			try 
			{
				int testReturnCode = ImportDataSet2();

				Assert.IsTrue( testReturnCode == 0,
					" Data Set2 Import was not successful ");


				//The Test
				ICoachRoutesQuotaFaresProvider coachRoutesQuotaFaresProvider = new CoachRoutesQuotaFareProvider();
			
				//Bristol to Inverness.
				//We expect 2 routes as the SCL Glasgow naptan has an NX Invalid operator.
				RouteList routeList = coachRoutesQuotaFaresProvider.FindRoutesAndQuotaFares("900041065","900090147");

				Assert.AreEqual(2, routeList.Count,"Incorrect number of routes");

			}
			catch ( TDException e )
			{
				string temp = e.Message;
				Assert.AreEqual(TDExceptionIdentifier.SBPImportFailed,
					"Expected to receive TDExceptionIdentifier.DGUnexpectedFeedName" + temp);
			}
			finally
			{
				DeleteDataFiles();
			}
		}

		/// <summary>
		/// Tests Coach Route Quota Fares
		/// </summary>
		[Test]
		public void TestCoachRouteQuotaFares()
		{
			try 
			{
				int testReturnCode = ImportDataSet2();

				Assert.IsTrue( testReturnCode == 0,
					" Data Set2 Import was not successful ");

				ICoachRoutesQuotaFaresProvider coachRoutesQuotaFaresProvider = new CoachRoutesQuotaFareProvider();
				
				RouteList routeList;

				//Bedfors to Durham.
				//We expect 2 Quota fares
				routeList = coachRoutesQuotaFaresProvider.FindRoutesAndQuotaFares("900052444","900070022");
			
				Assert.AreEqual(routeList[0].LegList[0].QuotaFareList[0].TicketType,"FunFare","Incorrect TicketType");
				Assert.AreEqual(routeList[0].LegList[0].QuotaFareList[0].Fare,25000,"Incorrect Fare");
				
				Assert.AreEqual(routeList[0].LegList[0].QuotaFareList[1].TicketType,"YoungPersons","Incorrect TicketType");
				Assert.AreEqual(routeList[0].LegList[0].QuotaFareList[1].Fare,18000,"Incorrect Fare");
				
				//Dundee to Dumfries.
				//We expect 1 Quota fare
				routeList = coachRoutesQuotaFaresProvider.FindRoutesAndQuotaFares("900090097","900067407");

				Assert.AreEqual(routeList[0].LegList[0].QuotaFareList[0].TicketType,"SCLFunFare","Incorrect TicketType");
				Assert.AreEqual(routeList[0].LegList[0].QuotaFareList[0].Fare,120000,"Incorrect Fare");


				//Dundee to Dumfries.
				//We expect 1 Quota fare
				routeList = coachRoutesQuotaFaresProvider.FindRoutesAndQuotaFares("900090097","900067407");

				Assert.AreEqual(routeList[0].LegList[0].QuotaFareList[0].TicketType,"SCLFunFare","Incorrect TicketType");
				Assert.AreEqual(routeList[0].LegList[0].QuotaFareList[0].Fare,120000,"Incorrect Fare");



				//London to Dundee. We expect 2 Routes each with 2 Legs.
				 routeList = coachRoutesQuotaFaresProvider.FindRoutesAndQuotaFares("900057366","900090097");

				Assert.AreEqual("FunFare",routeList[0].LegList[0].QuotaFareList[0].TicketType,"Incorrect TicketType");
				Assert.AreEqual(250000,routeList[0].LegList[0].QuotaFareList[0].Fare,"Incorrect Fare");

				Assert.AreEqual("SCLFunFare",routeList[0].LegList[1].QuotaFareList[0].TicketType,"Incorrect TicketType");
				Assert.AreEqual(350000,routeList[0].LegList[1].QuotaFareList[0].Fare,"Incorrect Fare");

			}
			catch ( TDException e )
			{
				string temp = e.Message;
				Assert.AreEqual(TDExceptionIdentifier.SBPImportFailed,
					"Expected to receive TDExceptionIdentifier.DGUnexpectedFeedName" + temp);
			}
			finally
			{
				DeleteDataFiles();
			}
		}

		/// <summary>
		/// Tests DataChangeNotification
		/// </summary>
		[Test]
		public void TestDataChangeNotification()
		{
			try 
			{
				TestMockDataChangeNotification dataChangeNotification = AddDataChangeNotification();
				
				int testReturnCode = 1;
				RouteList routeList;

				testReturnCode = ImportDataSet1();

				Assert.IsTrue( testReturnCode == 0,
					" Data Set1 Import was not successful ");

				ICoachRoutesQuotaFaresProvider coachRoutesQuotaFaresProvider = new CoachRoutesQuotaFareProvider();
			
				//Bristol to Inverness.
				//We expect 3 routes.
				 routeList = coachRoutesQuotaFaresProvider.FindRoutesAndQuotaFares("900041065","900090147");

				Assert.AreEqual(3, routeList.Count,"Data Set1 Import was not successful");

				//Load new data
				testReturnCode = ImportDataSet2();

				Assert.IsTrue( testReturnCode == 0,
					" Data Set2 Import was not successful ");

				//Raise ChangeNotification event
				dataChangeNotification.RaiseChangedEvent("CoachRoutesQuotaFare");

				//Bristol to Inverness.
				//We expect 2 routes as the SCL Glasgow naptan has an NX Invalid operator in DataSet2.
				 routeList = coachRoutesQuotaFaresProvider.FindRoutesAndQuotaFares("900041065","900090147");

				Assert.AreEqual(2, routeList.Count,"Data Change Notification Failed");

			}
			catch ( TDException e )
			{
				string temp = e.Message;
				Assert.AreEqual(TDExceptionIdentifier.SBPImportFailed,
					"Expected to receive TDExceptionIdentifier.DGUnexpectedFeedName" + temp);
			}
			finally
			{
				DeleteDataFiles();
			}
		
		}

		#endregion
		
		#region private helper methods

		/// <summary>
		/// Adds the mock DataChangeNotification service to the cache
		/// </summary>
		/// <returns></returns>
		private TestMockDataChangeNotification AddDataChangeNotification()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.DataChangeNotification, new TestMockDataChangeNotification());
			return (TestMockDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];
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
				string copyName = string.Empty;
				copyName = processingDir.FullName + @"\" + datafile1name ;
				file.CopyTo( copyName, true );
				File.SetAttributes( copyName, FileAttributes.Normal );
				
				copyName = processingDir.FullName + @"\" + datafile2name ;
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

	/// <summary>
	/// Class to initialise services that are used by the tests.
	/// </summary>
	public class TestCoachRoutesQuotaFaresProviderTestInitialization : IServiceInitialisation
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
