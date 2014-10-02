// *********************************************** 
// NAME			: TestJourneyRequestUsingGazetteerTransaction.cs
// AUTHOR		: Patrick Assuied
// DATE CREATED	: 10/06/04
// DESCRIPTION	: Implementation of the TestJourneyRequestUsingGazetteerTransaction class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/TestJourneyRequestTransaction.cs-arc  $
//
//   Rev 1.1   Mar 16 2009 12:24:04   build
//Automatically merged from branch for stream5215
//
//   Rev 1.0.1.0   Jan 12 2009 16:24:54   mturner
//Updated for tech refresh
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.0   Nov 08 2007 12:40:06   mturner
//Initial revision.
//
//   Rev 1.2   May 24 2005 15:05:44   NMoorhouse
//Post Del7 NUnit Updates
//
//   Rev 1.1   Feb 08 2005 12:06:18   RScott
//Assertions changed to Asserts
//
//   Rev 1.0   Jun 10 2004 17:08:20   passuied
//Initial Revision

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
	/// Tests that the JourneyRequestTransaction class functions correctly.	/// 
	/// </summary>
	[TestFixture]
	public class TestJourneyRequestTransaction
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
		/// Uses the "JourneyRequestTransaction01.xml" file from the "TestJourneyRequest" sub-folder,
		/// to initialise the JourneyRequest object with valid data.
		/// Performs calculation and checks that the results are as expeced.
		/// </summary>
		[Test]
		public void TestCalculateActualJourneyTime()
		{	

			// Test 1: 1 hour from now
			TDDateTime absolute1 = new TDDateTime(2003, 1, 2, 1, 1, 1);  // this should be ignored because spans have been set
			TDTimeSpan timeSpan1 = new TDTimeSpan(0, 1, 0, 0);  // one hour from now
			TDTimeSpan time1 = new TDTimeSpan(0, 0, 0, 0);  // no time specified
			DateTime result1 = TDTransaction.CalculateActualJourneyTime(absolute1, (int)DateTime.Now.DayOfWeek, time1); 

			DateTime expresult1 = DateTime.Now + new TimeSpan(0, 1, 0, 0);
			switch (expresult1.DayOfWeek)
			{
				case DayOfWeek.Friday:
					expresult1 = expresult1.AddDays(4);
					break;
				case DayOfWeek.Saturday:
					expresult1 = expresult1.AddDays(3);
					break;
				case DayOfWeek.Sunday:
					expresult1 = expresult1.AddDays(2);
					break;
				case DayOfWeek.Monday:
					expresult1 = expresult1.AddDays(1);
					break;
			}

			Assert.IsTrue(result1.Year==expresult1.Year && result1.Month==expresult1.Month && result1.Day==expresult1.Day && result1.Hour==expresult1.Hour && result1.Minute==expresult1.Minute);

			// Test 2: 10:00 in 8 days time
			TDDateTime absolute2 = new TDDateTime(2002, 2, 3, 4, 5, 6); // this should be ignored because spans have been set
			TDTimeSpan timeSpan2 = new TDTimeSpan(8, 0, 0, 0);  // 8 days from now
			TDTimeSpan time2 = new TDTimeSpan(0, 10, 0, 0);  // 10am
            DateTime result2 = TDTransaction.CalculateActualJourneyTime(absolute2, (int)DateTime.Now.DayOfWeek, time2); 

			DateTime expresult2 = DateTime.Now + new TimeSpan(8,0,0,0);
			switch (expresult2.DayOfWeek)
			{
				case DayOfWeek.Friday:
					expresult2 = expresult2.AddDays(4);
					break;
				case DayOfWeek.Saturday:
					expresult2 = expresult2.AddDays(3);
					break;
				case DayOfWeek.Sunday:
					expresult2 = expresult2.AddDays(2);
					break;
				case DayOfWeek.Monday:
					expresult2 = expresult2.AddDays(1);
					break;
			}
			Assert.IsTrue(result2.Year==expresult2.Year && result2.Month==expresult2.Month && result2.Day==expresult2.Day && result2.Hour==10 && result2.Minute==0);

			// Test 3: a specified date and time
			TDDateTime absolute3 = new TDDateTime(2003, 12, 25, 12, 0, 0);
			TDTimeSpan timeSpan3 = new TDTimeSpan(0, 0, 0, 0);
			TDTimeSpan time3 = new TDTimeSpan(0, 0, 0, 0);
            DateTime result3 = TDTransaction.CalculateActualJourneyTime(absolute3, (int)DateTime.Now.DayOfWeek, time3); 
			Assert.IsTrue(result3.Year==absolute3.Year && result3.Month==absolute3.Month && result3.Day==absolute3.Day && result3.Hour==absolute3.Hour && result3.Minute==absolute3.Minute);
		}
		
		[Test]
		public void TestSubmitTransactionWithTimeout()
		{
			ArrayList errors = new ArrayList();
			
			bool attempted = false; // needed to signal call was attempted - since injector does not throw exceptions

			string transpath = @".\JourneyRequestUsingGazetteerData\";
			try
			{
				MockGoodProperties properties = new MockGoodProperties();

				TDTransactionServiceOverride webservice = new TDTransactionServiceOverride( properties[ Keys.TransactionInjectorWebService ] );

				JourneyRequestTransactionGroup grp = new JourneyRequestTransactionGroup();				

				ArrayList transList = new ArrayList();

				// Pass a timeout of zero so that timeout will always occur.
                grp.CreateTransactions(transpath, transList, webservice, new TimeSpan(0, 0, 1, 0), new TimeSpan(0, 0, 0, 0), "INJ01");
			
				foreach( JourneyRequestTransaction jrt in transList)
				{
					jrt.ExecuteTransaction();
				}	
			
				attempted = true;

				// Make sure that the output file was created.
				string[] fileList = Directory.GetFiles(testRTEDir);
				Assert.IsTrue( fileList.Length > 0,"No RTE output files created." );

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

				Assert.IsTrue(rteCount == transList.Count, "Incorrect number of events in RTE output file" );
				
				// Make sure that the output file was created.
				string[] fileList2 = Directory.GetFiles(testOPEDir);
				Assert.IsTrue( fileList.Length > 0, "No OPE output files created." );

				// Open the file and verify that there is a timeout results message
				bool messageFound = false;
				string timeOutMsg = Messages.Injector_TransactionTimedOut;
				int varPos = timeOutMsg.IndexOf("{0}")-1;
				timeOutMsg = timeOutMsg.Substring(0, varPos);

				foreach( string file in fileList2 )
				{					
					StreamReader re		= File.OpenText( file );
					string		 input	= null;					

					while( (input = re.ReadLine()) != null)
					{			
						if (Regex.IsMatch(input, timeOutMsg, RegexOptions.IgnoreCase))
							messageFound = true;
					}
					re.Close();					
				}

				Assert.IsTrue(messageFound, "Timed out message not found in OPE output file." );
			
			}
			finally
			{
				
				if (!attempted)
					Assert.IsTrue(false, "Failure.");
			}
		}

		[Test]
		public void TestSubmitTransaction()
		{	
			ArrayList errors = new ArrayList();
			
			bool attempted = false; // needed to signal call was attempted - since injector does not throw exceptions

			string transpath = @".\JourneyRequestUsingGazetteerData\";
			try
			{
				MockGoodProperties properties = new MockGoodProperties();

				TDTransactionServiceOverride webservice = new TDTransactionServiceOverride( properties[ Keys.TransactionInjectorWebService ] );

				JourneyRequestTransactionGroup grp = new JourneyRequestTransactionGroup();				

				ArrayList transList = new ArrayList();

                grp.CreateTransactions(transpath, transList, webservice, new TimeSpan(0, 0, 1, 0), new TimeSpan(0, 0, 30, 0), "INJ01");
			
				foreach( JourneyRequestTransaction jrt in transList)
				{
					jrt.ExecuteTransaction();
				}	
			
				attempted = true;

				// Make sure that the output file was created.
				string[] fileList = Directory.GetFiles(testRTEDir);
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







