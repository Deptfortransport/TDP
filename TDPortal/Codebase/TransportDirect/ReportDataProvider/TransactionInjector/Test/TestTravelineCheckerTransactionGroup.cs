// *********************************************** 
// NAME                 : TestTravelineCheckerTransactiongroup.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 16/11/2004 
// DESCRIPTION  : Test for TravelineCheckerTransactionGroup class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/TestTravelineCheckerTransactionGroup.cs-arc  $ 
//
//   Rev 1.1   Jul 16 2010 09:39:36   PScott
//IR 5561  -  Changes after review. Read injector machine name at load and persist through each group.
//Old way did it in part of object passed through to other servers and the webserver names were being picked up instead of injector name.
//Resolution for 5561: Capture All Transaction Injectors Data to file.
//
//   Rev 1.0   Nov 08 2007 12:40:10   mturner
//Initial revision.
//
//   Rev 1.2   May 24 2005 15:10:04   NMoorhouse
//Post Del7 NUnit Updates
//
//   Rev 1.1   Feb 08 2005 13:16:46   RScott
//Assertion changed to Assert
//
//   Rev 1.0   Nov 17 2004 11:07:42   passuied
//Initial revision.


using System;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Xml.Serialization;

using NUnit.Framework;

using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.Logging;


namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Test for TravelineCheckerTransactionGroup class
	/// </summary>
	[TestFixture]
	public class TestTravelineCheckerTransactionGroup
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
		/// Tests that the CreateTransactions method of the TravelineRequestTransactionGroup class,
		/// populates an ArrayList object with TDTransaction objects, given source XML files.		
		/// </summary>
		/// <remarks>
		/// Requires that one or more valid XML files exist in the directory
		/// .\TravelineRequestData.
		/// </remarks>
		[Test]
		public void TestCreateTransactions()
		{			
			try
			{
				ArrayList transactions	= new ArrayList();	
				TDTransactionServiceOverride webservice = new TDTransactionServiceOverride("http://localhost/TDPWebServices/TransactionWebService/TDTransactionService.asmx");
				TravelineCheckerTransactionGroup tctg = new TravelineCheckerTransactionGroup();
				tctg.CreateTransactions(@".\TravelineCheckerData", transactions, webservice, new TimeSpan(0,0,1,0), new TimeSpan(0,0,300,0),"INJ01");
					
				Assert.IsTrue( transactions.Count > 0 );
			
			}
			catch (Exception)
			{
				Assert.IsTrue(false, "Exception thrown when creating journey request transaction group");
			}
		}
	}
}
