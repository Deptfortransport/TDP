//***********************************************
//NAME			: TestOperationalEventPublisher.cs
//AUTHOR		: Andy Lole
//DATE CREATED	: 02/09/2003
//DESCRIPTION	: NUnit test class for OperationalEventPublisher.cs
//***********************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/DatabasePublishers/Test/TestOperationalEventPublisher.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:24   mturner
//Initial revision.
//
//   Rev 1.10   Jan 24 2005 13:57:20   jgeorge
//Updates for NUnit 2.2
//
//   Rev 1.9   Apr 19 2004 20:21:38   geaton
//IR785. Updated to cope with new property of WorkloadEvent.
//
//   Rev 1.8   Nov 13 2003 22:00:06   geaton
//Use DefaultDB instead of ReportStagingDB which has been dropped.
//
//   Rev 1.7   Oct 30 2003 12:21:34   geaton
//Added test for publishing ReceivedOperationalEvents (which are logged by the Event Receiver)
//
//   Rev 1.6   Oct 10 2003 15:25:18   geaton
//Updated constructors to take target database property key. This enables validation at construction time.
//
//   Rev 1.5   Oct 07 2003 15:56:26   PScott
//publishers now called with id argument
//
//   Rev 1.4   Oct 07 2003 11:59:14   PScott
//Workload event now takes datetime
//
//   Rev 1.3   Oct 06 2003 15:18:06   geaton
//Changed TDException references to use TDExceptionIdentifier identifier.
//
//   Rev 1.2   Sep 17 2003 12:56:58   ALole
//Changed TestInitialization to only add PropertyService key if it's not already there.
//
//   Rev 1.1   Sep 16 2003 16:36:32   ALole
//Updated DatabasePublishers to use the new WorkloadEvent (minus URIStem and BytesSent).
//
//   Rev 1.0   Sep 04 2003 17:03:06   ALole
//Initial Revision

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.ReportDataProvider.TDPCustomEvents;

namespace TransportDirect.ReportDataProvider.DatabasePublishers
{
	/// <summary>
	/// This is required by the property service for this test module. It is instantiated once in the test constructor.
	/// </summary>
	public class TestInitialization : IServiceInitialisation
	{
		public void Populate(Hashtable serviceCache)
		{
			// Enable PropertyService
			if ( !serviceCache.Contains( ServiceDiscoveryKey.PropertyService ) )
			{
				//serviceCache.Add( ServiceDiscoveryKey.Crypto,  new CryptoFactory() );
				serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			}

		}
	}

	/// <summary>
	/// Summary description for TestOperationalEventPublisher.
	/// </summary>
	[TestFixture]
	public class TestOperationalEventPublisher
	{
		/// <summary>
		/// Initialisation for the fixture. Sets up ServiceDiscovery
		/// </summary>
		[TestFixtureSetUp]
		public void FixtureInit()
		{
			// Reset in case something else hasn't
			TDServiceDiscovery.ResetServiceDiscoveryForTest();

			// Initialise
			TDServiceDiscovery.Init(new TestInitialization());
		}

		/// <summary>
		/// TearDown for the fixture. Clears down ServiceDiscovery for the next test
		/// </summary>
		[TestFixtureTearDown]
		public void FixtureCleanUp()
		{
			// Reset to be nice to other fixtures
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}
		
		/// <summary>
		/// Check that Publisher baheaves correctly if an invalid object is passed in.
		/// </summary>
		[Test]
		public void TestOperationalEventWriteFailWrongType()
		{
			DateTime testDate = new DateTime( 2003, 1, 1, 0, 0, 0 );
			int testNumberRequested = 999;
			WorkloadEvent we = new WorkloadEvent( testDate, testNumberRequested );

			OperationalEventPublisher oep = new OperationalEventPublisher("OperationalEventPublisher", SqlHelperDatabase.ReportStagingDB);
			bool passCheck = false;

			try
			{
				oep.WriteEvent( we );
				passCheck = false;
			}
			catch ( TDException tdEx)
			{
				Assert.AreEqual( tdEx.Identifier, TDExceptionIdentifier.RDPUnsupportedDatabasePublisherEvent );
				passCheck = true;
			}

			Assert.IsTrue( passCheck );
		}

		/// <summary>
		/// Check that a row is written to the database and that now Exceptions are thrown.
		/// </summary>
		/// <remarks>
		/// Also checks that wrapped op events (ReceivedOperationalEvents) are published.
		/// </remarks>
		[Test]
		public void TestOperationalEventWritePass()
		{
			OperationalEvent oe = 
				new OperationalEvent(TDEventCategory.Infrastructure,
				TDTraceLevel.Warning,
				"Test Message");

			ReceivedOperationalEvent roe = new ReceivedOperationalEvent(oe);

			OperationalEventPublisher oep = new OperationalEventPublisher("OperationalEventPublisher", SqlHelperDatabase.ReportStagingDB);
			
			bool passCheck = false;

			try
			{
				oep.WriteEvent( oe );
				oep.WriteEvent( roe );
				passCheck = true;
			}
			catch ( TDException tdEx )
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );
		}
	}		
}

