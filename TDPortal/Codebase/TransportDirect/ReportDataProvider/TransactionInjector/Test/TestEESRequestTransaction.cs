// *********************************************** 
// NAME                 : TestEESRequestTransaction.cs
// AUTHOR               : Mark Turner
// DATE CREATED         : 13/01/2009 
// DESCRIPTION  	: NUnit test implmentation.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/TestEESRequestTransaction.cs-arc  $ 
//
//   Rev 1.2   Jul 16 2010 09:39:32   PScott
//IR 5561  -  Changes after review. Read injector machine name at load and persist through each group.
//Old way did it in part of object passed through to other servers and the webserver names were being picked up instead of injector name.
//Resolution for 5561: Capture All Transaction Injectors Data to file.
//
//   Rev 1.1   Jan 13 2009 10:23:38   mturner
//Further tech refresh changes
//
//   Rev 1.0   Jan 13 2009 09:57:02   mturner
//Initial revision.

using System;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Xml.Serialization;

using NUnit.Framework;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;  
using TransportDirect.UserPortal.AdditionalDataModule ;  
using TransportDirect.UserPortal.DepartureBoardService.StopEventManager;
using TransportDirect.ReportDataProvider.DatabasePublishers;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.UserPortal.DepartureBoardService;
using TransportDirect.UserPortal.DepartureBoardService.RTTIManager; 
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{

	/// <summary>
	/// Test class for EESRequestTransaction.
	/// </summary>
	[TestFixture]
	public class TestEESRequestTransaction
	{   
		private string testRTEDir;
		private string testOPEDir;

		[SetUp]
		public void Init()
		{
			// Initialise services.
			TDServiceDiscovery.Init(new TestInitialisation());	

			Trace.Listeners.Remove("TDTraceListener");

			MockGoodProperties testProperties = new MockGoodProperties();
			testRTEDir		 = testProperties.RTEDir;
			testOPEDir		 = testProperties.OPEDir;

			// Create the file publisher directories needed by logging service

			if( !Directory.Exists(testRTEDir) )
				Directory.CreateDirectory(testRTEDir );
			else
			{
				string[] currFileList = Directory.GetFiles(testRTEDir);
				foreach( string file in currFileList )
					File.Delete( file );
			}

			if( !Directory.Exists(testOPEDir) )
				Directory.CreateDirectory(testOPEDir);
			
			// Create TD Logging service.
			ArrayList errors = new ArrayList();
			IEventPublisher[] customPublishers = new IEventPublisher[0];
			
			Trace.Listeners.Add(new TDTraceListener(testProperties, customPublishers, errors));							
			Trace.Listeners.Remove("System.Diagnostics.DefaultTraceListener");

		}

		[TearDown]
		public void CleanUp()
		{
			Trace.Listeners.Remove("TDTraceListener");

			if( Directory.Exists( testRTEDir ) )
				Directory.Delete( testRTEDir, true );

			if( Directory.Exists( testOPEDir ) )
				Directory.Delete( testOPEDir, true );

		} 

		/// <summary>
		/// Calls the ExecuteTransaction method and tests that log files that are created,
		/// It should not contain any LogBadEnd entries.
		/// </summary>
		[Test]
		public void TestSubmitTransaction()
		{
			ArrayList errors = new ArrayList();	
		
			bool attempted = false; // needed to signal call was attempted - since injector does not throw exceptions

            try
            {
                string testRTTIInfoSrcDir = @".\TransactionRequestData\EESRequestData";

                MockGoodProperties properties = new MockGoodProperties();

                // Create a webservice object.
                TDTransactionServiceOverride webservice = new TDTransactionServiceOverride(properties[Keys.TransactionInjectorWebService]);
                EESRequestTransactionGroup grp = new EESRequestTransactionGroup();

                ArrayList transList = new ArrayList();

                grp.CreateTransactions(testRTTIInfoSrcDir, transList, webservice, new TimeSpan(0, 0, 1, 0), new TimeSpan(0, 0, 30, 0), "INJ01");

                for( int i=0; i < transList.Count; i++)
                {
                    EESRequestTransaction sit = (EESRequestTransaction)transList[i];

                    // Override the offset to ensure the transaction is submitted
                    sit.Offset = DateTime.Now.Minute % sit.Frequency;

                    sit.ExecuteTransaction();
                }

                // Now check the result of the call to ExecuteTransaction.				

                // Make sure that the output file was created.
                string[] fileList = Directory.GetFiles(testRTEDir);
                Assert.IsTrue(fileList.Length > 0, "No output files created.");

                // Open the files and verify that there are correct number of entries								
                int rteCount = 0;

                foreach (string file in fileList)
                {
                    StreamReader re = File.OpenText(file);
                    string input = null;

                    while ((input = re.ReadLine()) != null)
                    {
                        rteCount++;
                    }
                    re.Close();
                }

                Assert.IsTrue(rteCount == transList.Count, "Incorrect number of events in output file");

                attempted = true;
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, 
                    string.Format("Error: {0}, {1}", ex.Message, ex.StackTrace)));
            }
			finally
			{
				if (!attempted)
					Assert.IsTrue(false,"Failed to make a call");
			}				
		}	
	}
}
