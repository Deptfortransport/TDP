// *********************************************** 
// NAME                 : TestTravelineCheckerTransaction.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 16/11/2004 
// DESCRIPTION  : Test for TravelineCheckerTransaction class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/TestTravelineCheckerTransaction.cs-arc  $ 
//
//   Rev 1.1   Jul 16 2010 09:39:34   PScott
//IR 5561  -  Changes after review. Read injector machine name at load and persist through each group.
//Old way did it in part of object passed through to other servers and the webserver names were being picked up instead of injector name.
//Resolution for 5561: Capture All Transaction Injectors Data to file.
//
//   Rev 1.0   Nov 08 2007 12:40:10   mturner
//Initial revision.
//
//   Rev 1.2   May 24 2005 15:09:32   NMoorhouse
//Post Del7 NUnit Updates
//
//   Rev 1.1   Feb 08 2005 13:16:46   RScott
//Assertion changed to Assert
//
//   Rev 1.0   Nov 17 2004 11:06:50   passuied
//Initial revision.


using System;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

using NUnit.Framework;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Test for TravelineCheckerTransaction class
	/// </summary>
	[TestFixture]
	public class TestTravelineCheckerTransaction
	{
		public TestTravelineCheckerTransaction()
		{
		
		}

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
 
		[Test]
		public void TestSubmitTransaction()
		{	
			ArrayList errors = new ArrayList();
			
			bool attempted = false; // needed to signal call was attempted - since injector does not throw exceptions

			string transpath = @".\TravelineCheckerData\";
			try
			{
				MockGoodProperties properties = new MockGoodProperties();

				TDTransactionServiceOverride webservice = new TDTransactionServiceOverride( properties[ Keys.TransactionInjectorWebService ] );

				TravelineCheckerTransactionGroup grp = new TravelineCheckerTransactionGroup();			

				ArrayList transList = new ArrayList();

                grp.CreateTransactions(transpath, transList, webservice, new TimeSpan(0, 0, 1, 0), new TimeSpan(0, 0, 30, 0), "INJ01");			
			
				foreach( TravelineCheckerTransaction tct in transList)
				{
					tct.ExecuteTransaction();
				}	
			
				attempted = true;

				// Make sure that the output file was created.
				string[] fileList = Directory.GetFiles(testRTEDir);
				Assert.IsTrue( fileList.Length > 0 , "No output files created.");

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
					Assert.IsTrue(false,"Failure.");
			}
		}

	}
}
