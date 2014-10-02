//***********************************************
//NAME			: TestTDPCustomEventPublisher.cs
//AUTHOR		: Andy Lole
//DATE CREATED	: 02/09/2003
//DESCRIPTION	: NUnit test class for CJPCustomEventPublisher.cs
//***********************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/DatabasePublishers/Test/TestTDPCustomEventPublisher.cs-arc  $
//
//   Rev 1.6   Feb 07 2013 09:02:52   mmodi
//Fixed AccessibleEvent test failing, Dave Lane clearly has not tested it when he wrote the code
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.5   Jan 28 2013 15:59:26   DLane
//New event types
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.4   Jan 22 2013 16:48:48   DLane
//Accessible events
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.3   Jan 14 2013 14:41:56   mmodi
//Added GISQueryEvent
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.2   Jun 28 2010 14:07:46   PScott
//SCR 5561 - write MachineName to reference transactions
//
//   Rev 1.1   May 14 2008 16:39:30   mmodi
//Added repeat visitor test
//Resolution for 4889: Del 10.1 - Repeat Visitor Cookies
//
//   Rev 1.0   Nov 08 2007 12:38:24   mturner
//Initial revision.
//
//   Rev 1.24   Mar 27 2006 16:57:16   tmollart
//Modified test so that assert statements are correct - expected then actual not actual then expected on the row counts which was incorrect.
//
//   Rev 1.23   Feb 23 2006 19:24:24   build
//Merge for stream3129 (automatic merge failed for this file)
//
//   Rev 1.22.2.7   Jan 10 2006 17:37:14   schand
//Passing corrected language parameter
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.22.2.6   Jan 10 2006 17:29:48   schand
//Replace TestMethod operation with RequestContextData
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.22.2.5   Jan 10 2006 16:54:52   schand
//Changes for OperationType
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.22.2.4   Dec 22 2005 14:58:38   halkatib
//Changes requested by Chris o made
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.22.2.3   Nov 28 2005 14:10:44   rgreenwood
//TD106 FX Cop changes
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.22.2.2   Nov 25 2005 14:47:14   rgreenwood
//TD106 FXCop changes
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.22.2.1   Nov 25 2005 14:20:46   rgreenwood
//TD106 FXCop: Extended Namespace for EnhancedExposedServicesCommon
//
//   Rev 1.22.2.0   Nov 22 2005 16:44:16   rgreenwood
//TD106 Enhanced Exposed Web Services Framework: Added TestEnhancedExposedServicesEvents
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.22   Mar 08 2005 16:48:46   schand
//Added test for MobilePageEventEntry
//
//   Rev 1.21   Feb 07 2005 14:37:54   passuied
//added code + storedproc + tables for ExposedServices Event
//
//   Rev 1.20   Feb 03 2005 11:22:42   passuied
//used RequestType string value (not int) + updated test
//
//   Rev 1.19   Jan 31 2005 13:38:04   schand
//Added test method for RTTIEvent
//
//   Rev 1.18   Jan 24 2005 13:57:20   jgeorge
//Updates for NUnit 2.2
//
//   Rev 1.17   Jul 22 2004 13:58:12   jmorrissey
//Added TestUserFeedbackEventWritePass for testing that new UserFeedbackEvent is successfully published.
//
//   Rev 1.16   Jul 15 2004 18:04:40   acaunt
//Modified so that GazetteerEvent is now passed submitted time.
//
//   Rev 1.15   Apr 19 2004 20:21:40   geaton
//IR785. Updated to cope with new property of WorkloadEvent.
//
//   Rev 1.14   Dec 02 2003 20:05:48   geaton
//Updates following addition of Successful column on ReferenceTransactionEvent table.
//
//   Rev 1.13   Nov 13 2003 22:00:10   geaton
//Use DefaultDB instead of ReportStagingDB which has been dropped.
//
//   Rev 1.12   Nov 10 2003 12:31:42   geaton
//Removed reference to TDTransactionCategory - a string will be used instead to allow new categories to be added at runtime.
//
//   Rev 1.11   Oct 10 2003 15:25:22   geaton
//Updated constructors to take target database property key. This enables validation at construction time.
//
//   Rev 1.10   Oct 07 2003 15:56:30   PScott
//publishers now called with id argument
//
//   Rev 1.9   Oct 07 2003 11:59:16   PScott
//Workload event now takes datetime
//
//   Rev 1.8   Oct 06 2003 15:18:10   geaton
//Changed TDException references to use TDExceptionIdentifier identifier.
//
//   Rev 1.7   Oct 03 2003 10:43:44   JMorrissey
//Updated parameters used for Login Event
//
//   Rev 1.6   Sep 25 2003 12:00:36   ALole
//Updated Publishers to include code review comments.
//Added JourneyPlanRequestVerboseEvent, JourneyPlanResultsVerboseEvent and uncommented JourneyPlanResultsEvent.
//Added Properties service to TDPCUstomEventPublisher for VerboseEvent support.
//
//   Rev 1.5   Sep 16 2003 16:36:32   ALole
//Updated DatabasePublishers to use the new WorkloadEvent (minus URIStem and BytesSent).
//
//   Rev 1.4   Sep 15 2003 17:05:00   geaton
//ReferenceTransactionEvent takes DateTime instead of TDDateTime.
//
//   Rev 1.3   Sep 15 2003 16:41:54   geaton
//MapEvent changed to DateTime instead of TDDateTime.
//
//   Rev 1.2   Sep 08 2003 16:36:40   ALole
//Commented out JourneyPlanResultsEvent code aas this is not currently needed.
//Changed JourneyPlanRequestsEvent to use the new version of the event to include specific infor in the db.
//
//   Rev 1.1   Sep 05 2003 10:15:22   ALole
//Updated to Reflect changes to MapOverlayEvent and MapEvent
//
//   Rev 1.0   Sep 04 2003 17:03:08   ALole
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
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.EnhancedExposedServices.Common;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.ReportDataProvider.DatabasePublishers
{

	/// <summary>
	/// Summary description for TestOperationalEventPublisher.
	/// </summary>
	[TestFixture]
	public class TestTDPCustomEventPublisher
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
		/// Test To ensure that the Publisher fails correctly if an incorrtect event is passed in
		/// </summary>
		[Test]
		public void TestTDPCustomEventWriteFail()
		{
			OperationalEvent oe = 
				new OperationalEvent(TDEventCategory.Infrastructure,
				TDTraceLevel.Warning,
				"Test Message");

			TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			bool passCheck = false;

			try
			{
				cep.WriteEvent( oe );
				passCheck = false;
			}
			catch ( TDException tdEx)
			{
				// Check that the error code returned is correct
				Assert.AreEqual( tdEx.Identifier, TDExceptionIdentifier.RDPUnsupportedDatabasePublisherEvent );
				passCheck = true;
			}

			// Check that the exception has been caught
			Assert.IsTrue( passCheck );
		}

		/// <summary>
		/// Test that Publisher behaves correctly on being passed a valid object of the correct type
		/// All following tests follow the same syntax - passing in a valid event.
		/// </summary>
		[Test]
		public void TestWorkloadEventWritePass()
		{
			DateTime testDate = new DateTime( 2003, 1, 1, 1, 1, 1);
			int testNumberRequested = 1234;
			WorkloadEvent we = new WorkloadEvent( testDate, testNumberRequested );


			TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			bool passCheck = false;

			try
			{
				cep.WriteEvent( we );
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );
		}

		[Test]
		public void TestRetailerHandoffEventWritePass()
		{
			RetailerHandoffEvent ce = new RetailerHandoffEvent( "TestId", "TestSessionId", false );

			TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			bool passCheck = false;

			try
			{
				cep.WriteEvent( ce );
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );
		}

		[Test]
		public void TestReferenceTransactionEventWritePass()
		{
			ReferenceTransactionEvent ce = new ReferenceTransactionEvent( "ComplexJourneyRequest", false, DateTime.Now, "TestSessionId", true ,"INJ01");

			TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			bool passCheck = false;

			try
			{
				cep.WriteEvent( ce );
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );
		}

		[Test]
		public void TestPageEntryEventWritePass()
		{
			PageEntryEvent ce = new PageEntryEvent( PageId.Empty, "TestSessionId", false );

			TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			bool passCheck = false;

			try
			{
				cep.WriteEvent( ce );
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );
		}

        [Test]
        public void TestRepeatVisitorEventWritePass()
        {
            RepeatVisitorEvent ce = new RepeatVisitorEvent(RepeatVisitorType.VisitorRepeat, "sessionOld", "sessionNew", DateTime.Now,
                PageId.JourneyPlannerInput.ToString(), "localhost", "userAgent");

            TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
            bool passCheck = false;

            try
            {
                cep.WriteEvent(ce);
                passCheck = true;
            }
            catch (TDException tdEx)
            {
                Console.WriteLine(tdEx.Message);
                passCheck = false;
            }

            Assert.IsTrue(passCheck);
        }

		[Test]
		public void TestMobilePageEntryEventWritePass()
		{
			TDMobilePageEntryEvent ce = new TDMobilePageEntryEvent( TDMobilePageId.MobileUnknown , "TestSessionId", false );
			TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);

			bool passCheck = false;

			try
			{
				cep.WriteEvent( ce );
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );
		}


		[Test]
		public void TestMapEventWritePass()
		{
			MapEvent ce = new MapEvent( MapEventCommandCategory.MapOverlay, DateTime.Now, MapEventDisplayCategory.MiniScale, "TestSessionId", false );

			TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			bool passCheck = false;

			try
			{
				cep.WriteEvent( ce );
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );
		}

		[Test]
		public void TestLoginEventWritePass()
		{
			LoginEvent ce = new LoginEvent( "TestSessionId" );

			TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			bool passCheck = false;

			try
			{
				cep.WriteEvent( ce );
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );
		}

		[Test]
		public void TestJourneyPlanResultsEventWritePass()
		{
			JourneyPlanResultsEvent ce = new JourneyPlanResultsEvent( "TestJourneyRequestId", JourneyPlanResponseCategory.Results, false, "TestSessionId" );

			TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			bool passCheck = false;

			try
			{
				cep.WriteEvent( ce );
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );
		}

		[Test]
		public void TestJourneyPlanRequestEventWritePass()
		{
			ModeType[] modes = { ModeType.Air, ModeType.Car, ModeType.Cycle };
			JourneyPlanRequestEvent ce = new JourneyPlanRequestEvent( "TestJourneyId", modes , false, "TestSessionId" );

			TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			bool passCheck = false;

			try
			{
				cep.WriteEvent( ce );
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );
		}

		[Test]
		public void TestGazetteerEventWritePass()
		{
			GazetteerEvent ce = new GazetteerEvent( GazetteerEventCategory.GazetteerPointOfInterest, DateTime.Now, "TestSessionId", false );

			TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			bool passCheck = false;

			try
			{
				cep.WriteEvent( ce );
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );
		}

		private int DBRowCount( string tableName )
		{
			SqlHelper sqlHelper = new SqlHelper();
			SqlDataReader data = null;
			int rowCount = 0;

			try
			{
				sqlHelper.ConnOpen(SqlHelperDatabase.ReportStagingDB);
				string sqlStat = "select count(*) from " + tableName;
				data = sqlHelper.GetReader( sqlStat );
				
				while ( data.Read() )
				{
					rowCount = data.GetInt32(0);
				}
			}
			catch ( Exception e )
			{
				Console.WriteLine( e.Message );
			}
			finally
			{
				if ( data != null)
				{
					data.Close();
				}
				sqlHelper.ConnClose();
			}
			return rowCount;
		}

		[Test]
		public void TestJourneyPlanResultsVerboseEventWritePass()
		{
			JourneyPlanResultsVerboseEvent ce1 = new JourneyPlanResultsVerboseEvent( "TestJourneyPlanResultsVerboseEventId", new TDJourneyResult(), false, "TestSessionId" );
			JourneyPlanResultsVerboseEvent ce2 = new JourneyPlanResultsVerboseEvent( "TestJourneyPlanResultsVerboseEventId2", new TDJourneyResult(), false, "TestSessionId" );

			TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			bool passCheck = false;

			int rowCount = DBRowCount( "JourneyPlanResultsVerboseEvent" );

			try
			{
				cep.WriteEvent( ce2 );
				passCheck = true;
			}
			catch ( Exception tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.AreEqual( DBRowCount( "JourneyPlanResultsVerboseEvent" ), rowCount + 1 );

			try
			{
				cep.WriteEvent( ce1 );
				passCheck = true;
			}
			catch ( Exception tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.AreEqual( DBRowCount( "JourneyPlanResultsVerboseEvent" ), rowCount + 2 );
	
			Assert.IsTrue( passCheck );
		}

		[Test]
		public void TestJourneyPlanRequestVerboseEventWritePass()
		{
			JourneyPlanRequestVerboseEvent ce1 = new JourneyPlanRequestVerboseEvent( "TestJourneyPlanRequestVerboseEventId", new TDJourneyRequest(), false, "TestSessionId" );
			JourneyPlanRequestVerboseEvent ce2 = new JourneyPlanRequestVerboseEvent( "TestJourneyPlanRequestVerboseEventId2", new TDJourneyRequest(), false, "TestSessionId" );

			TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			bool passCheck = false;

			int rowCount = DBRowCount( "JourneyPlanRequestVerboseEvent" );

			try
			{
				cep.WriteEvent( ce2 );
				passCheck = true;
			}
			catch ( Exception tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.AreEqual( rowCount + 1, DBRowCount( "JourneyPlanRequestVerboseEvent" ));

			try
			{
				cep.WriteEvent( ce1 );
				passCheck = true;
			}
			catch ( Exception tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.AreEqual(rowCount + 2, DBRowCount( "JourneyPlanRequestVerboseEvent" ));
	
			Assert.IsTrue( passCheck );
		}

		/// <summary>
		/// tests that a UserFeedbackEvent is successfully published
		/// </summary>
		[Test]
		public void TestUserFeedbackEventWritePass()
		{
			
			UserFeedbackEvent ufe = new UserFeedbackEvent("UserFeedbackSiteProblem",DateTime.MinValue,DateTime.MinValue,true,"TestSessionId", false );

			TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			bool passCheck = false;

			try
			{
				cep.WriteEvent(ufe);
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );
		}

		
		/// <summary>
		/// tests that a UserFeedbackEvent is successfully published
		/// </summary>
		[Test]
		public void TestRTTIEvent()
		{				
			RTTIEvent rEvent = new RTTIEvent(DateTime.Now , DateTime.Now, false);   

			TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			bool passCheck = false;

			try
			{
				cep.WriteEvent(rEvent);
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );
		}

		/// <summary>
		/// tests that a StopEventRequestEvent is successfully published
		/// </summary>
		[Test]
		public void TestStopEventRequestEvent()
		{				
			StopEventRequestEvent se = new StopEventRequestEvent(Guid.NewGuid().ToString(), DateTime.Now, StopEventRequestType.First, true);

			TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			bool passCheck = false;

			try
			{
				cep.WriteEvent(se);
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );

			se = new StopEventRequestEvent(Guid.NewGuid().ToString(), DateTime.Now, StopEventRequestType.Last, true);
			try
			{
				cep.WriteEvent(se);
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );

			se = new StopEventRequestEvent(Guid.NewGuid().ToString(), DateTime.Now, StopEventRequestType.Time, true);
			try
			{
				cep.WriteEvent(se);
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );
		}

		/// <summary>
		/// tests that a StopEventRequestEvent is successfully published
		/// </summary>
		[Test]
		public void TestExposedServicesEvent()
		{				
			ExposedServicesEvent ese = new ExposedServicesEvent("token", DateTime.Now, ExposedServicesCategory.CodeService, true);

			TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			bool passCheck = false;

			try
			{
				cep.WriteEvent(ese);
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );

			ese = new ExposedServicesEvent("token", DateTime.Now, ExposedServicesCategory.DepartureBoardServiceRTTI, true);

			cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			passCheck = false;

			try
			{
				cep.WriteEvent(ese);
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );

			ese = new ExposedServicesEvent("token", DateTime.Now, ExposedServicesCategory.DepartureBoardServiceStopEvent, true);

			cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			passCheck = false;

			try
			{
				cep.WriteEvent(ese);
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );

			ese = new ExposedServicesEvent("token", DateTime.Now, ExposedServicesCategory.TaxiInfoService, true);

			cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			passCheck = false;

			try
			{
				cep.WriteEvent(ese);
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );

			ese = new ExposedServicesEvent("token", DateTime.Now, ExposedServicesCategory.TravelNews, true);

			cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			passCheck = false;

			try
			{
				cep.WriteEvent(ese);
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );
		}

		/// <summary>
		/// tests that both EnhancedExposedServiceStartEvent and EnhancedExposedServiceFinishEvent are successfully published
		/// </summary>
		[Test]
		public void TestEnhancedExposedServicesEvents()
		{
			//Instance of ExposedServiceContext object for instantiation of the Start/Finish Event
			ExposedServiceContext serviceContext = new ExposedServiceContext("1", "TestTranxID", "EN" ,"TransportDirect.EnhancedExposedServices.TestWebService/RequestContextData");

			//Start event
			EnhancedExposedServiceStartEvent eStart = new EnhancedExposedServiceStartEvent(true, serviceContext);

			TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);

			bool passCheck = false;

			try
			{
				cep.WriteEvent(eStart);
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );

			//Finish Event
			EnhancedExposedServiceFinishEvent eFinish = new EnhancedExposedServiceFinishEvent(true, serviceContext);

			cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
			passCheck = false;

			try
			{
				cep.WriteEvent(eFinish);
				passCheck = true;
			}
			catch ( TDException tdEx)
			{
				Console.WriteLine( tdEx.Message );
				passCheck = false;
			}

			Assert.IsTrue( passCheck );

		}

        /// <summary>
        /// Tests a GISQueryEvent is successfully published
        /// </summary>
        [Test]
        public void TestGISQueryEventWritePass()
        {
            TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
            bool passCheck = false;

            try
            {
                GISQueryEvent ce = null;

                foreach (GISQueryType gqt in Enum.GetValues(typeof(GISQueryType)))
                {
                    ce = new GISQueryEvent(gqt, DateTime.Now, "TestSessionId");
                    cep.WriteEvent(ce);
                }

                passCheck = true;
            }
            catch (TDException tdEx)
            {
                Console.WriteLine(tdEx.Message);
                passCheck = false;
            }

            Assert.IsTrue(passCheck);
        }

        [Test]
        public void TestAccessibleEventWritePass()
        {
            AccessibleEvent ae1 = new AccessibleEvent(AccessibleEventType.StepFreeWithAssistance, DateTime.Now, "SessionId");
            AccessibleEvent ae2 = new AccessibleEvent(AccessibleEventType.StepFree, DateTime.Now, "SessionId");
            AccessibleEvent ae3 = new AccessibleEvent(AccessibleEventType.Assistance, DateTime.Now, "SessionId");
            AccessibleEvent ae4 = new AccessibleEvent(AccessibleEventType.StepFreeWithAssistanceFewerChanges, DateTime.Now, "SessionId");
            AccessibleEvent ae5 = new AccessibleEvent(AccessibleEventType.StepFreeFewerChanges, DateTime.Now, "SessionId");
            AccessibleEvent ae6 = new AccessibleEvent(AccessibleEventType.AssistanceFewerChanges, DateTime.Now, "SessionId");
            TDPCustomEventPublisher cep = new TDPCustomEventPublisher("TDPCustomEventPublisher", SqlHelperDatabase.ReportStagingDB);
            try
            {
                cep.WriteEvent(ae1);
                cep.WriteEvent(ae2);
                cep.WriteEvent(ae3);
                cep.WriteEvent(ae4);
                cep.WriteEvent(ae5);
                cep.WriteEvent(ae6);
            }
            catch (TDException tdEx)
            {
                Assert.Fail(String.Format("Error trying to write accessible event to database. Exception code [{0}], message [{1}]", tdEx.Identifier.ToString(), tdEx.Message));
            }
        }
    }		
}

