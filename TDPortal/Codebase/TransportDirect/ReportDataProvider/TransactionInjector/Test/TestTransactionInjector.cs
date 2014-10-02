// *********************************************** 
// NAME			: TestTransactionInjector.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 03/09/2003 
// DESCRIPTION	: Implementation of the TestTransactionInjector class.
// ************************************************ 
//$Log

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

using NUnit.Framework;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Tests the TransactionInjector class.
	/// </summary>
	[TestFixture]
	public class TestTransactionInjector
	{	
		private TransactionInjector transactionInjector = null;
		private DateTime startedInjecting;
		private DateTime stoppedInjecting;
		private int numTransactions;
		private Timer timer;
		private Timer timer2;

		public TestTransactionInjector()
		{
			TDServiceDiscovery.Init(new TransactionInjectorInitialisation("transactioninjector1"));
		}

		[SetUp]
		public void Init()
		{
				
		}

		[TearDown]
		public void CleanUp()
		{
			
		} 

		private void DisplayExpectedResults(Object o)
		{
			// Calculate true injection start time (Injector waits until start of next minute before injecting)
			startedInjecting.AddMinutes(1);
			DateTime trueStartedInjecting = new DateTime(startedInjecting.Year, startedInjecting.Month, startedInjecting.Day, startedInjecting.Hour, startedInjecting.Minute, 0);

			transactionInjector.Dispose(true);
			timer.Dispose();
			timer2.Dispose();

			// Determine expected transaction file entries
			TimeSpan injectionPeriod = stoppedInjecting - trueStartedInjecting;

			int expectedTransactions = injectionPeriod.Minutes * this.numTransactions;
			
			Console.WriteLine(String.Format("Expected [{0}] transactions. NB Please check the output file manually because data may not have been completely flushed to file prior to comparison.", expectedTransactions.ToString()));
		}


		private void StopInjecting(Object o)
		{
			transactionInjector.Stop();

			stoppedInjecting = DateTime.Now;

			Console.WriteLine("TestInjectionOverFiveMinutes: injection completed. Please wait a minute for expected results to be determined.");

			// wait for outstanding transactions to complete - allow 60s (since this is the typical frequency)
			this.timer2 = new Timer(new TimerCallback(this.DisplayExpectedResults),
								    null, 
								    new TimeSpan(0,1,0),
								    new TimeSpan(0,0,0));	
		}

		/// <summary>
		/// Performs injection over five minutes.
		/// </summary>
		[Test]
		[Ignore("This test must be run using the NUnit GUI and cannot be automated using NANT")]
		public void TestInjectionOverFiveMinutes()
		{
			transactionInjector = new TransactionInjector((IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService], "transactioninjector1");

			numTransactions = transactionInjector.TransactionCount;

			transactionInjector.Start();

			startedInjecting = DateTime.Now;

			this.timer = new Timer(new TimerCallback(this.StopInjecting),
													 null, 
													 new TimeSpan(0,6,0), // set to 6 so that a full five minutes worth of transactions are injected
													 new TimeSpan(0,0,0));
	
			Console.WriteLine("TestInjectionOverFiveMinutes: injection in progress - please wait. NB unit test will display green prior to test completing.");
		}

		/// <remarks>
		/// Manual setup:
		/// Preconditions:
		/// Valid .config and properties xml file exist.
		/// Properties xml configures event logging service to log 
		/// all reference transaction events using a File Publisher.
		/// NOTE: Test will take at least five minutes to complete!
		/// </remarks>
		[Test]
		[Ignore("Manual setup required")]
		public void ManualSetup()
		{}
		
	}	
}

