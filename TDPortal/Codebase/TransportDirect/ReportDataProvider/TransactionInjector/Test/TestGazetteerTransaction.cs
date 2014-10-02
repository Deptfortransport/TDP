// *********************************************** 
// NAME			: TestStationInfoTransaction.cs
// AUTHOR		: Peter Norell
// DATE CREATED	: 07/01/2004 
// DESCRIPTION	: Tests Gazetteer transaction class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/TestGazetteerTransaction.cs-arc  $
//
//   Rev 1.1   Jul 16 2010 09:39:32   PScott
//IR 5561  -  Changes after review. Read injector machine name at load and persist through each group.
//Old way did it in part of object passed through to other servers and the webserver names were being picked up instead of injector name.
//Resolution for 5561: Capture All Transaction Injectors Data to file.
//
//   Rev 1.0   Nov 08 2007 12:40:04   mturner
//Initial revision.
//
//   Rev 1.5   May 24 2005 15:01:50   NMoorhouse
//Post Del7 NUnit Updates
//
//   Rev 1.4   Feb 08 2005 12:06:16   RScott
//Assertions changed to Asserts
//
//   Rev 1.3   Apr 23 2004 17:22:18   geaton
//IR827 - Allow timeout specification for transactions.
//
//   Rev 1.2   Mar 02 2004 09:17:32   geaton
//Updated test following removal of custom Transaction Injector File Publisher. (A standard File Publisher is now used instead.)
//
//   Rev 1.1   Feb 16 2004 17:26:06   geaton
//Incident 643. Store injection frequency of transactions within transaction classes.
//
//   Rev 1.0   Jan 08 2004 19:41:54   PNorell
//Initial Revision
using System;

using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Xml.Serialization;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Common;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Summary description for TestGazetteerTransaction.
	/// </summary>
	[TestFixture]
	public class TestGazetteerTransaction
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
		/// do not contain any LogBadEnd entries.
		/// </summary>
		[Test]
		public void TestSubmitTransaction()
		{
			ArrayList errors = new ArrayList();	
			bool attempted = false; // needed to signal call was attempted - since injector does not throw exceptions

			try
			{

				string testGazetteerSrcDir = @".\GazetteerData";

				MockGoodProperties properties = new MockGoodProperties();

				// Create a webservice object.
				TDTransactionServiceOverride webservice = new TDTransactionServiceOverride( properties[ Keys.TransactionInjectorWebService ] );

				GazetteerTransactionGroup grp = new GazetteerTransactionGroup();				

				ArrayList transList = new ArrayList();

                grp.CreateTransactions(testGazetteerSrcDir, transList, webservice, new TimeSpan(0, 0, 1, 0), new TimeSpan(0, 0, 30, 0), "INJ01");			
			
				foreach( GazetteerTransaction gt in transList)
				{
					gt.ExecuteTransaction();
				}	
		
				attempted = true;

				// Now check the result of the call to ExecuteTransaction.				

				// Make sure that the output files were created.
				string[] fileList = Directory.GetFiles( testRTEDir );
				Assert.IsTrue( fileList.Length > 0, "No output files created." );

				// Open the files and verify that there are correct number of entries									
				int rteCount = 0;
				foreach( string file in fileList )
				{					
					StreamReader re		= File.OpenText( file );
					string		 input	= null;					

					while( (input = re.ReadLine()) != null)
					{				
						rteCount++;
					}
					re.Close();					
				}		

				Assert.IsTrue(rteCount == transList.Count, "Incorrect number of events in output file" );
				
			}	
			finally
			{
				if (!attempted)
					Assert.IsTrue(false, "Failure.");
			}				
		}
	}
}
