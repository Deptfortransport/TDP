//***********************************************
//NAME			: TestCJPCustomEventPublisher.cs
//AUTHOR		: Andy Lole
//DATE CREATED	: 02/09/2003
//DESCRIPTION	: NUnit test class for CJPCustomEventPublisher.cs
//***********************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/DatabasePublishers/Test/TestCJPCustomEventPublisher.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:22   mturner
//Initial revision.
//
//   Rev 1.15   Jan 24 2005 13:56:56   jgeorge
//Updates for modifications to JourneyWebRequestEvent and InternalRequestEvent
//
//   Rev 1.14   Jul 12 2004 14:17:18   jgeorge
//Updated with InternalRequestEvent
//
//   Rev 1.13   Apr 19 2004 20:21:42   geaton
//IR785. Updated to cope with new property of WorkloadEvent.
//
//   Rev 1.12   Nov 13 2003 22:00:12   geaton
//Use DefaultDB instead of ReportStagingDB which has been dropped.
//
//   Rev 1.11   Oct 10 2003 15:25:26   geaton
//Updated constructors to take target database property key. This enables validation at construction time.
//
//   Rev 1.10   Oct 07 2003 15:56:24   PScott
//publishers now called with id argument
//
//   Rev 1.9   Oct 07 2003 11:59:18   PScott
//Workload event now takes datetime
//
//   Rev 1.8   Oct 06 2003 15:18:14   geaton
//Changed TDException references to use TDExceptionIdentifier identifier.
//
//   Rev 1.7   Sep 17 2003 12:56:04   ALole
//Changed TestJourneyWebRequestEventWritePass submitted date to include milliseconds.
//
//   Rev 1.6   Sep 16 2003 16:36:34   ALole
//Updated DatabasePublishers to use the new WorkloadEvent (minus URIStem and BytesSent).
//
//   Rev 1.5   Sep 15 2003 14:44:30   geaton
//Changed TDDateTime params to DateTime
//
//   Rev 1.4   Sep 12 2003 14:42:22   ALole
//Added A Comment to the Fail routine
//
//   Rev 1.3   Sep 11 2003 16:54:56   ALole
//Updated TestCJPCustomEventWriteFail method
//
//   Rev 1.2   Sep 11 2003 16:43:24   ALole
//Updated TestCJPCustomEventWriteFail method
//
//   Rev 1.1   Sep 10 2003 11:26:46   ALole
//Added SessionId to JourneyWebRequestEvent
//
//   Rev 1.0   Sep 04 2003 17:03:04   ALole
//Initial Revision

using System;
using System.Diagnostics;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.ReportDataProvider.CJPCustomEvents;
using TransportDirect.ReportDataProvider.TDPCustomEvents;

namespace TransportDirect.ReportDataProvider.DatabasePublishers
{

	/// <summary>
	/// Summary description for TestOperationalEventPublisher.
	/// </summary>
	[TestFixture]
	public class TestCJPCustomEventPublisher
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
		/// Attempt to log an event that the publisher cannot publish.
		/// </summary>
		[Test]
		public void TestCJPCustomEventWriteFail()
		{
			DateTime testDate = new DateTime( 2003, 1, 1, 0, 0, 0 );
			int testNumberRequested = 999;
			WorkloadEvent we = new WorkloadEvent( testDate, testNumberRequested );

			CJPCustomEventPublisher cep = new CJPCustomEventPublisher("CJPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			bool passCheck = false;

			try
			{
				cep.WriteEvent( we );
				passCheck = false;
			}
			catch ( TDException tdEx)
			{
				Assert.AreEqual( tdEx.Identifier, TDExceptionIdentifier.RDPUnsupportedDatabasePublisherEvent );
				passCheck = true;
			}

			Assert.IsTrue( passCheck );
		}

		[Test]
		public void TestJourneyWebRequestEventWritePass()
		{
			DateTime testDate = new DateTime( 2003, 12, 1, 1, 1, 1, 12 );

			JourneyWebRequestEvent ce = new JourneyWebRequestEvent
													( "TestId",
													 "TestSessionId",
													 testDate, //DateTime.Now,
													 JourneyWebRequestType.Journey,
													 "TestRegionCode",
													 true,
													 false );

			CJPCustomEventPublisher cep = new CJPCustomEventPublisher("CJPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			bool passCheck = false;

			try
			{
				cep.WriteEvent( ce );
				passCheck = true;
			}
			catch ( TDException tdEx )
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );
		}

		[Test]
		public void TestLocationRequestEventWritePass()
		{
			LocationRequestEvent ce = new LocationRequestEvent
										( "TestId",
										 JourneyPrepositionCategory.FirstLastService,
										 "TestAdminAreaCode",
										 "TestRegionCode" );

			CJPCustomEventPublisher cep = new CJPCustomEventPublisher("CJPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			bool passCheck = false;

			try
			{
				cep.WriteEvent( ce );
				passCheck = true;
			}
			catch ( TDException tdEx )
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );
		}

		[Test]
		public void TestInternalRequestEventWritePass()
		{
			InternalRequestEvent ir = new InternalRequestEvent( "SessionId", "InternalRequestId", DateTime.Now, InternalRequestType.AirTTBO, "JW", true, false);
			CJPCustomEventPublisher cep = new CJPCustomEventPublisher("CJPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			try
			{
				cep.WriteEvent( ir );
			}
			catch (TDException tdEx)
			{
				Assert.Fail(String.Format("Error trying to write event to database. Exception code [{0}], message [{1}]", tdEx.Identifier.ToString(), tdEx.Message));
			}
				

		}
	}		
}

