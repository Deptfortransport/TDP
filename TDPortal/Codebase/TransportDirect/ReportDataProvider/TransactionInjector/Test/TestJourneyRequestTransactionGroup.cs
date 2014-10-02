// *********************************************** 
// NAME			: TestJourneyRequestUsingGazetteerTransactionGroup.cs
// AUTHOR		: Patrick assuied
// DATE CREATED	: 10/06/04 
// DESCRIPTION	: Implementation of the TestJourneyRequestUsingGazetteerTransactionGroup class.
// ************************************************ 
// $Log:

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
	/// Tests thst JourneyRequestTransactionGroup class.
	/// </summary>
	[TestFixture]
	public class TestJourneyRequestTransactionGroup
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
		/// Tests that the CreateTransactions method of the JourneyRequestTransactionGroup class,
		/// populates an ArrayList object with TDTransaction objects, given source XML files.		
		/// </summary>
		/// <remarks>
		/// Requires that one or more valid XML files exist in the directory
		/// .\JourneyRequestData.
		/// </remarks>
		[Test]
		public void TestCreateTransactions()
		{			
			try
			{
				ArrayList transactions	= new ArrayList();	
				TDTransactionServiceOverride webservice = new TDTransactionServiceOverride("http://localhost/TDPWebServices/TransactionWebService/TDTransactionService.asmx");
				JourneyRequestTransactionGroup journeyReqTransGrp = new JourneyRequestTransactionGroup();
                journeyReqTransGrp.CreateTransactions(@".\JourneyRequestUsingGazetteerData", transactions, webservice, new TimeSpan(0, 0, 1, 0), new TimeSpan(0, 0, 30, 0), "INJ01");
					
				Assert.IsTrue( transactions.Count > 0 );
			
			}
			catch (Exception)
			{
				Assert.IsTrue(false, "Exception thrown when creating journey request transaction group");
			}
		}
		

	}
}