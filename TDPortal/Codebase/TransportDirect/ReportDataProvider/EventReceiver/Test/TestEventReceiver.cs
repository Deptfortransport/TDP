// *********************************************** 
// NAME                 : TestEventReceiver.cs 
// AUTHOR               : Jatinder S. Toor
// DATE CREATED         : 18/08/2003 
// DESCRIPTION  : Implements tests for the EventReceiver class. 
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/EventReceiver/TestEventReceiver.cs-arc  $
//
//   Rev 1.1   Apr 30 2012 13:29:26   DLane
//Updates for EES event recording in batch
//Resolution for 5805: Record batch EES events in the reporting db
//
//   Rev 1.0   Nov 08 2007 12:38:38   mturner
//Initial revision.
//
//   Rev 1.8   Feb 08 2005 16:09:34   RScott
//Assertions changed to asserts
//
//   Rev 1.7   Feb 07 2005 10:05:24   RScott
//Assertion changed to Assert
//
//   Rev 1.6   Apr 01 2004 16:49:42   geaton
//Changes following unit test refactoring exercise.
//
//   Rev 1.5   Oct 30 2003 12:25:50   geaton
//Added test for ReceivedOperationalEvents.
//
//   Rev 1.4   Oct 10 2003 15:22:52   geaton
//Updated error handling and validation.
//
//   Rev 1.3   Oct 09 2003 12:33:44   geaton
//Tidied up error handling and added verbose messages to assist in debugging.
//
//   Rev 1.2   Sep 05 2003 09:49:32   jtoor
//Changes made to comply with Code Review.
//
//   Rev 1.1   Aug 29 2003 11:11:56   jtoor
// 
//
//   Rev 1.0   Aug 22 2003 11:49:44   jtoor
//Initial Revision

using System;
using System.IO;
using System.Messaging;
using System.Collections;
using System.Diagnostics;
using System.Threading;

using Logger = TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;


namespace TransportDirect.ReportDataProvider.EventReceiver
{
	using NUnit.Framework;

	/// <summary>
	/// Tests for the EventReceiver class.
	/// </summary>
	[TestFixture]
	public class TestEventReceiver
	{	
		/// <summary>
		/// Tests that when a single message is written to a specific queue,
		/// that the message is picked up by EventReceiver and written to a test output file.
		/// </summary>
		[Test]
		public void TestSingleEvent()
		{	
			string[] files = null;

			// Create our mock properties object to be used by the live code.
			MockPropertiesEventWrite properties = new MockPropertiesEventWrite();

			// Get the directory path for the log file.
			string outdir = properties["Logging.Publisher.File.F1.Directory"];

			// Create the directory if it doesn't exist,
			// and empty it if it does.
			if( !Directory.Exists( outdir ) )			
				Directory.CreateDirectory( outdir );			
			else
			{	
				files = Directory.GetFiles( outdir );				
				foreach( string s in files ) File.Delete( s );
			}

			// Setup the trace listener usinging the mock properties.
			ArrayList errors = new ArrayList();			
			Logger.IEventPublisher[] customPublishers = new Logger.IEventPublisher[0];
			Trace.Listeners.Add( new Logger.TDTraceListener( properties, customPublishers, errors ) );			

			// Get the name of the test queue, and check if it exists.
			// If it doesn't create it.
			string queue  = properties[string.Format(Keys.ReceiverQueuePath, 1)];

			MessageQueue mq = null;

			if( !MessageQueue.Exists( queue ) )
				mq = MessageQueue.Create( queue );
			else
				mq = new MessageQueue( queue );

			mq.Formatter = new BinaryMessageFormatter();

			// Write an event to the queue.
			Logger.OperationalEvent oe = new Logger.OperationalEvent(	Logger.TDEventCategory.Business, 
																		Logger.TDTraceLevel.Info, "This is a test."		);			
			mq.Send( oe, "test" ); 

			// Setup the eventreceiver to process any events on,
			// the queues it monitors.
			EventReceiver er = new EventReceiver();

			er.Run( properties );

			// Give the eventreceiver a chance to process any receive events.
			Thread.Sleep( 2000 );

			// Open the log file and check if the test message,
			// was written to the file.
			files = Directory.GetFiles( outdir );				
			StreamReader re = File.OpenText( files[0] );
			string input	= null;
			int    evtCount = 0;

			while( (input = re.ReadLine()) != null)
			{				
				// Operational Events should be logged as ReceivedOperationalEvents
				if( input.IndexOf( ( "ROE" )) > 0 )
					evtCount++;
			}
			re.Close();
			
			try
			{					
				Assert.IsTrue( evtCount == 1 );
			}
			finally
			{
				// Clean-Up.

				files = Directory.GetFiles( outdir );				
				foreach( string s in files ) File.Delete( s );
				Directory.Delete( outdir );	
		
				MessageQueue.Delete( queue );
			}
		}	

		/// <summary>
		/// Test initialisation of the event receiver service
		/// </summary>
        [Test]
        public void TestInitialisation()
        {
            TDServiceDiscovery.Init(new EventReceiverInitialisation());
        }

        /// <summary>
		/// Writes multiple events to a queue, specified by the properties.
		/// Checks that all the events are picked up by the EventReceiver,
		/// and written to a test output file.
		/// </summary>
		[Test]
		public void TestMultipleEvents()
		{	
			string[] files = null;

			// Create our mock properties object to be used by the live code.
			MockPropertiesEventWrite properties = new MockPropertiesEventWrite();

			// Get the directory path for the log file.
			string outdir = properties["Logging.Publisher.File.F1.Directory"];

			// Create the directory if it doesn't exist,
			// and empty it if it does.
			if( !Directory.Exists( outdir ) )			
				Directory.CreateDirectory( outdir );			
			else
			{	
				files = Directory.GetFiles( outdir );				
				foreach( string s in files ) File.Delete( s );
			}

			// Setup the trace listener usinging the mock properties.
			ArrayList errors = new ArrayList();			
			Logger.IEventPublisher[] customPublishers = new Logger.IEventPublisher[0];
			Trace.Listeners.Add( new Logger.TDTraceListener( properties, customPublishers, errors ) );			

			// Get the name of the test queue, and check if it exists.
			// If it doesn't create it.
			string queue  = properties[string.Format(Keys.ReceiverQueuePath, 1)];

			MessageQueue mq = null;

			if( !MessageQueue.Exists( queue ) )
				mq = MessageQueue.Create( queue );
			else
				mq = new MessageQueue( queue );

			mq.Formatter = new BinaryMessageFormatter();

			// Create an event and write it twice to the queue.
			Logger.OperationalEvent oe = new Logger.OperationalEvent(	Logger.TDEventCategory.Business, 
																		Logger.TDTraceLevel.Info, "This is a test."		);
			
			mq.Send( oe, "test" ); 
			mq.Send( oe, "test" ); 

			// Setup the eventreceiver to process any events on,
			// the queues it monitors.
			EventReceiver er = new EventReceiver();

			er.Run( properties );

			// Give the eventreceiver a chance to process any receive events.
			Thread.Sleep( 2000 );

			// Open the log file and check if the test message,
			// was written to the file.
			files = Directory.GetFiles( outdir );				
			StreamReader re = File.OpenText( files[0] );
			string input	= null;
			int    evtCount	= 0;

			while( (input = re.ReadLine()) != null)
			{
				// Operational Events should be logged as ReceivedOperationalEvents
				if( input.IndexOf( "ROE" ) > 0 )
					evtCount++;
			}
			re.Close();
			
			try
			{						
				// Assert if the event was not written to the log file.
				Assert.IsTrue( evtCount == 2 );
			}
			finally
			{
				// Clean-Up.

				files = Directory.GetFiles( outdir );				
				foreach( string s in files ) File.Delete( s );
				Directory.Delete( outdir );	
		
				MessageQueue.Delete( queue );
			}
		}

		/// <summary>
		/// Test that when no events are written to the queue,
		/// that no unexpected messages are written to the test output file.
		/// </summary>
		[Test]
		public void TestZeroEvent()
		{	
			string[] files = null;

			// Create our mock properties object to be used by the live code.
			MockPropertiesEventWrite properties = new MockPropertiesEventWrite();

			// Get the directory path for the log file.
			string outdir = properties["Logging.Publisher.File.F1.Directory"];

			// Create the directory if it doesn't exist,
			// and empty it if it does.
			if( !Directory.Exists( outdir ) )			
				Directory.CreateDirectory( outdir );			
			else
			{	
				files = Directory.GetFiles( outdir );				
				foreach( string s in files ) File.Delete( s );
			}

			// Setup the trace listener usinging the mock properties.
			ArrayList errors = new ArrayList();			
			Logger.IEventPublisher[] customPublishers = new Logger.IEventPublisher[0];
			Trace.Listeners.Add( new Logger.TDTraceListener( properties, customPublishers, errors ) );			

			// Get the name of the test queue, and check if it exists.
			// If it doesn't create it.
			string queue  = properties[string.Format(Keys.ReceiverQueuePath, 1)];

			MessageQueue mq = null;

			if( !MessageQueue.Exists( queue ) )
				mq = MessageQueue.Create( queue );
			else
				mq = new MessageQueue( queue );

			mq.Formatter = new BinaryMessageFormatter();

			EventReceiver er = new EventReceiver();

			er.Run( properties );

			Thread.Sleep( 2000 );
		
			files = Directory.GetFiles( outdir );
			
			// Check that no files have been created.			
			try
			{								
				Assert.IsTrue( files.Length == 0 );
			}
			finally
			{
				// Clean-Up.

				files = Directory.GetFiles( outdir );				
				foreach( string s in files ) File.Delete( s );

				Directory.Delete( outdir );	
		
				MessageQueue.Delete( queue );
			}
		}	

		[TearDown]
		public void Cleanup()
		{
			Trace.Listeners.Remove("TDTraceListener");
		}

		[SetUp]
		public void SetUp()
		{
			Trace.Listeners.Remove("TDTraceListener");
		}

		/// <remarks>
		/// Manual setup:
		/// MSMQ must be installed on test machine.
		/// </remarks>
		[Test]
		[Ignore("Manual setup required")]
		public void ManualSetup()
		{}
	
	}
	
}


