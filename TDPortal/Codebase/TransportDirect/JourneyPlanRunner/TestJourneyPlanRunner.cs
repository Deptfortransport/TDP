// ***********************************************
// NAME 		: TestJourneyPlanRunner.cs
// AUTHOR 		: Andrew Toner
// DATE CREATED : 26/09/2003
// DESCRIPTION 	: 
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/TestJourneyPlanRunner.cs-arc  $
//
//   Rev 1.2   Sep 02 2011 12:46:08   apatel
//Real Time Car Unit Test code update
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.1   Sep 01 2011 10:43:38   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.0   Nov 08 2007 12:24:48   mturner
//Initial revision.
//
//   Rev 1.17   Feb 10 2006 09:30:56   kjosling
//Turned off failing unit tests
//
//   Rev 1.16   Feb 07 2006 12:01:26   mtillett
//Add mock cache to fix 3 unit tests
//
//   Rev 1.15   Nov 09 2005 12:31:34   build
//Automatically merged from branch for stream2818
//
//   Rev 1.14.1.0   Oct 14 2005 15:10:42   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//
//   Rev 1.14   Jul 06 2005 12:06:28   asinclair
//Merge for stream2557
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.13.1.0   Jun 21 2005 17:34:26   asinclair
//Added ServiceDiscoveryKey.JourneyPlanRunnerCaller
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.13   May 19 2005 15:08:44   rscott
//Updated for NUnit tests
//
//   Rev 1.12   May 17 2005 10:55:30   rscott
//Changes for IR1936 code review to get NUnit working
//
//   Rev 1.11   Mar 23 2005 12:10:28   jgeorge
//Removed reference to LocationService.UnitTest namespace
//
//   Rev 1.10   Mar 14 2005 15:54:32   COwczarek
//Remove references to Initialisation project - not required
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.9   Feb 07 2005 12:19:24   RScott
//Assertion changed to Assert
//
//   Rev 1.8   Sep 15 2004 16:29:22   jmorrissey
//IR 1527 - added TestCheckLocationsForOverlapping
//
//   Rev 1.7   Jul 23 2004 18:26:54   RPhilpott
//DEL 6.1 Trunk Journey changes
//
//   Rev 1.6   Jun 15 2004 13:26:22   RPhilpott
//Refactored for Find-A-Flight additions. 
//
//   Rev 1.5   Jun 10 2004 21:00:06   RPhilpott
//Add nasty bodge to make date validation tests independent of the date format on the machine on which they are run.
//
//   Rev 1.4   May 26 2004 09:04:02   jgeorge
//Update for TDJourneyParameters changes
//
//   Rev 1.3   Mar 31 2004 16:19:08   jgeorge
//Changed namespace in line with unit test guidelines. Added TestMockDataService to service initialisation.
//
//   Rev 1.2   Nov 07 2003 11:04:38   passuied
//fixed Unit tests
//
//   Rev 1.1   Oct 03 2003 13:38:30   PNorell
//Updated the new exception identifier.
//
//   Rev 1.0   Sep 29 2003 16:38:04   AToner
//Initial Revision
//

using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Threading;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.JourneyPlanRunner;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.DataServices;

using TransportDirect.JourneyPlanning.CJPInterface;

using Logger = System.Diagnostics.Trace;

using NUnit.Framework;


namespace TransportDirect.UserPortal.JourneyPlanRunner
{

	public class TestJourneyPlanRunnerInitialisation : IServiceInitialisation
	{
		public void Populate(Hashtable serviceCache)
		{

			// nasty bodge to make date validation checks independent of the 
			//  date format of the user/machine the tests are run on ...
			CultureInfo ci = new CultureInfo("en-GB");
			DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
			dtfi.ShortDatePattern = "dd MM yyyy";
			ci.DateTimeFormat = dtfi;
			Thread.CurrentThread.CurrentCulture = ci;

			serviceCache.Add(ServiceDiscoveryKey.Crypto, new CryptoFactory() );
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			serviceCache.Add(ServiceDiscoveryKey.DataServices, new TestMockDataServicesFactory());
			serviceCache.Add(ServiceDiscoveryKey.SessionManager, new TestDummySessionManager("sessionID"));
			serviceCache.Add(ServiceDiscoveryKey.AirDataProvider, new TestMockAirDataProvider());
			serviceCache.Add(ServiceDiscoveryKey.CjpManager, new TestMockCJPManager());
			serviceCache.Add(ServiceDiscoveryKey.GisQuery, new TestMockGisQuery());
			serviceCache.Add(ServiceDiscoveryKey.JourneyPlanRunnerCaller, new JourneyPlanRunnerCallerFactory());
			serviceCache.Add(ServiceDiscoveryKey.Cache, new TestMockCache());
			
		}
	}

	/// <summary>
	/// Summary description for TestJourneyPlanRunner.
	/// </summary>
	[TestFixture]
	public class TestJourneyPlanRunner
	{
		private ITDSessionManager tdSessionManager;
		private TDJourneyParametersMulti tdJourneyParameters;

		public TestJourneyPlanRunner()
		{
		}
	
		[SetUp]
		public void SetUp()
		{
			// Initialise the service discovery
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestJourneyPlanRunnerInitialisation( ));
			IPropertyProvider property = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];

			// Setup session manager defaults
			tdSessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];

			Trace.Listeners.Remove("TDTraceListener");
		
			IEventPublisher[] customPublishers = new IEventPublisher[0];																								
			ArrayList errors = new ArrayList();
				
			try
			{
				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
			}
			catch (TDException)
			{
				Assert.IsTrue(false);
			}

			// Setup Journey Parameters
			// Outward time is one month from now
			DateTime outwardTime = DateTime.Now.AddMonths(1);

			// return time is 5.5 hours after the outward time
			DateTime returnTime = outwardTime.AddSeconds(5.5 * 3600);  

			tdJourneyParameters = new TDJourneyParametersMulti();
			tdJourneyParameters.OutwardHour = outwardTime.Hour.ToString();
			tdJourneyParameters.OutwardMinute = outwardTime.Minute.ToString();
			tdJourneyParameters.OutwardDayOfMonth = outwardTime.Day.ToString();
			tdJourneyParameters.OutwardMonthYear = outwardTime.Month.ToString() + " " + outwardTime.Year.ToString();

			tdJourneyParameters.ReturnHour = returnTime.Hour.ToString();
			tdJourneyParameters.ReturnMinute = returnTime.Minute.ToString();
			tdJourneyParameters.ReturnDayOfMonth = returnTime.Day.ToString();
			tdJourneyParameters.ReturnMonthYear = returnTime.Month.ToString() + " " + returnTime.Year.ToString();

			tdJourneyParameters.OriginLocation.Status = TDLocationStatus.Valid;
			tdJourneyParameters.DestinationLocation.Status = TDLocationStatus.Valid;
			tdJourneyParameters.OriginLocation.NaPTANs = new TDNaptan[1];
			tdJourneyParameters.DestinationLocation.NaPTANs = new TDNaptan[1];

			tdJourneyParameters.OriginLocation.NaPTANs[0] = new TDNaptan("naptan1", new OSGridReference(0,0));
			tdJourneyParameters.DestinationLocation.NaPTANs[0] = new TDNaptan("naptan2", new OSGridReference(0,0));

			tdJourneyParameters.OriginLocation.Locality = "locality";
			tdJourneyParameters.DestinationLocation.Locality = "locality";
		}

		[TearDown]
		public void Cleanup()
		{
			Trace.Listeners.Remove("TDTraceListener");
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}

		[Test]
		[Ignore("ProjectNewkirk")]
		public void ValidateAndRunValidJourney()
		{
			//Ensure the origin and destination naptans do not overlap
			tdJourneyParameters.OriginLocation.NaPTANs[0] = new TDNaptan("naptan1", new OSGridReference(0,1));
			tdJourneyParameters.DestinationLocation.NaPTANs[0] = new TDNaptan("naptan2", new OSGridReference(0,0));

			tdJourneyParameters.OriginLocation.Locality = "locality1";
			tdJourneyParameters.DestinationLocation.Locality = "locality2";

			// A normal, valid journey
			JourneyPlanRunner result = new JourneyPlanRunner( null );

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			bool pass = result.ValidateAndRun( tdSessionManager, tdJourneyParameters, "en-UK" );
			Assert.IsTrue( pass, "Valid journey" );

		} 

		[Test]
		public void ValidateAndRunDateTimeError()
		{
			//
			// Set up a bad return hour
			tdJourneyParameters.ReturnHour = "25";

			JourneyPlanRunner result = new JourneyPlanRunner( null );
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			bool pass = result.ValidateAndRun( tdSessionManager, tdJourneyParameters, "en-UK" );
			Assert.IsTrue( !pass, "Invalid journey test" );
			Assert.AreEqual( ValidationErrorID.ReturnDateTimeInvalid, tdSessionManager.ValidationError.ErrorIDs[0], "Return time should be invalid" );
		}

		[Test]
		public void ValidateAndRunReturnBeforeOutward()
		{
			//
			// Set up a bad return (Return before outward)
			tdJourneyParameters.ReturnDayOfMonth = (Convert.ToInt32(tdJourneyParameters.OutwardDayOfMonth)-1).ToString();

			JourneyPlanRunner result = new JourneyPlanRunner( null );
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			bool pass = result.ValidateAndRun( tdSessionManager, tdJourneyParameters, "en-UK" );
			Assert.IsTrue( !pass, "Invalid journey test" );
			Assert.AreEqual( ValidationErrorID.ReturnDateTimeNotLaterThanOutwardDateTime, tdSessionManager.ValidationError.ErrorIDs[0], "Return time should be before outward" );
		}

		[Test]
		public void ValidateAndRunOutwardBeforeNow()
		{
			//
			// Set up a bad return (Outward before now)
			DateTime now = DateTime.Now.AddMonths(-1);
			tdJourneyParameters.OutwardDayOfMonth = now.Day.ToString();
			tdJourneyParameters.OutwardMonthYear = now.Month.ToString() + " " + now.Year.ToString();

			JourneyPlanRunner result = new JourneyPlanRunner( null );
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			bool pass = result.ValidateAndRun( tdSessionManager, tdJourneyParameters, "en-UK" );
			Assert.IsTrue( !pass, "Invalid journey test" );
			Assert.AreEqual( ValidationErrorID.OutwardDateTimeNotLaterThanNow, tdSessionManager.ValidationError.ErrorIDs[0], "Outward time should be before now" );
		}

		[Test]
		public void ValidateAndRunOriginError()
		{
			//
			// Set the origin location to be invalid
			tdJourneyParameters.OriginLocation.Status = TDLocationStatus.Unspecified;

			JourneyPlanRunner result = new JourneyPlanRunner( null );
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			bool pass = result.ValidateAndRun( tdSessionManager, tdJourneyParameters, "en-UK" );
			Assert.IsTrue( !pass, "Invalid journey test" );
			Assert.AreEqual( ValidationErrorID.OriginLocationInvalid, tdSessionManager.ValidationError.ErrorIDs[0], "Origin Location should be invalid" );
		}

		[Test]
		public void ValidateAndRunDestinationError()
		{
			//
			// Set the distination to be invalid
			tdJourneyParameters.DestinationLocation.Status = TDLocationStatus.Ambiguous;

			JourneyPlanRunner result = new JourneyPlanRunner( null );
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			bool pass = result.ValidateAndRun( tdSessionManager, tdJourneyParameters, "en-UK" );
			Assert.IsTrue( !pass, "Invalid journey test" );
			Assert.AreEqual( ValidationErrorID.AmbiguousDestinationLocation, tdSessionManager.ValidationError.ErrorIDs[0], "Destination Location should be invalid" );
		}

		[Test]
		public void ValidateAndRunAdjust()
		{
			//
			// Call the Adjust ValidateAndRun
			TDJourneyRequest journeyRequest = new TDJourneyRequest();

			JourneyPlanRunner result = new JourneyPlanRunner( null );
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			bool pass = result.ValidateAndRun(  1, 2, journeyRequest, tdSessionManager, "en-UK" );
		}

		[Test]
		[Ignore("ProjectNewkirk")]
		public void TestCheckLocationsForCompleteness()
		{
			JourneyPlanRunner result = new JourneyPlanRunner(null);
	
			// note that if either origin or destination lacks naptans,
			// the subsequent validation will fail and ValidateAndRun 
			// returns false
		
			// make this a trunk rail request
			// neither origin nor destination naptans need populating 
			tdSessionManager.FindPageState = new FindTrainPageState();
			tdJourneyParameters.PublicModes = new ModeType[] { ModeType.Rail }; 

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			bool pass = result.ValidateAndRun( tdSessionManager, tdJourneyParameters, "en" );
			Assert.IsTrue(pass, "Valid journey");

			result = new JourneyPlanRunner(null);

			// origin naptans need populating 

			tdJourneyParameters.OriginLocation.Status = TDLocationStatus.Valid;
			tdJourneyParameters.OriginLocation.NaPTANs[0] = new TDNaptan("naptan1", new OSGridReference(524000,186500));
			tdJourneyParameters.OriginLocation.Locality = "locality1";

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			pass = result.ValidateAndRun( tdSessionManager, tdJourneyParameters, "en" );
			Assert.IsTrue(pass, "Valid journey");

			// destination naptans need populating 
			tdJourneyParameters.DestinationLocation.Status = TDLocationStatus.Valid;
			tdJourneyParameters.DestinationLocation.NaPTANs[0] = new TDNaptan("naptan2", new OSGridReference(524000,186510));
			tdJourneyParameters.DestinationLocation.Locality = "locality2";

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			pass = result.ValidateAndRun( tdSessionManager, tdJourneyParameters, "en" );
			Assert.IsTrue(pass, "Valid journey");

			// both origin and destination naptans need populating 
			tdJourneyParameters.OriginLocation.NaPTANs[0] = new TDNaptan("naptan1", new OSGridReference(524000,186500));
			tdJourneyParameters.DestinationLocation.NaPTANs[0] = new TDNaptan("naptan2", new OSGridReference(524000,186510));

			tdJourneyParameters.OriginLocation.Locality = "locality1";
			tdJourneyParameters.DestinationLocation.Locality = "locality2";

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			pass = result.ValidateAndRun( tdSessionManager, tdJourneyParameters, "en" );
			Assert.IsTrue(pass, "Valid journey");

		}

		[Test]
		[Ignore("ProjectNewkirk")]
		public void TestCheckLocationsForOverlapping()
		{

			// first 3 tests are for a Find A Train request			
			tdSessionManager.FindPageState = new FindTrainPageState();
			tdJourneyParameters.PublicModes = new ModeType[] { ModeType.Rail }; 

			//TEST 1 - origin and destination overlap					

			//origin is "St Albans (Rail)"			
			tdJourneyParameters.OriginLocation.NaPTANs[0].Naptan = "9100STALBCY";			
			tdJourneyParameters.OriginLocation.NaPTANs[0].GridReference.Easting = 515600;
			tdJourneyParameters.OriginLocation.NaPTANs[0].GridReference.Northing = 206900;

			//destination is "St Albans (Rail)"					
			tdJourneyParameters.DestinationLocation.NaPTANs[0].Naptan = "9100STALBCY";			
			tdJourneyParameters.DestinationLocation.NaPTANs[0].GridReference.Easting = 515600;
			tdJourneyParameters.DestinationLocation.NaPTANs[0].GridReference.Northing = 206900;

			//this should fail because the origin and destination have the same naptans
			JourneyPlanRunner jp = new JourneyPlanRunner(null);
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			bool result = jp.ValidateAndRun( tdSessionManager, tdJourneyParameters, "en" );			

			Assert.IsTrue(!result, "Should have failed, origin and destination locations are the same");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.OriginAndDestinationOverlap, "Unexpected error msg");

			//TEST 2 - origin and via overlap

			//set via to be the same as the origin location but change the destination 		
			
			//origin is "St Albans (Rail)"			
			tdJourneyParameters.OriginLocation.NaPTANs[0].Naptan = "9100STALBCY";			
			tdJourneyParameters.OriginLocation.NaPTANs[0].GridReference.Easting = 515600;
			tdJourneyParameters.OriginLocation.NaPTANs[0].GridReference.Northing = 206900;

			//via is "St Albans (Rail)"		
			tdJourneyParameters.PublicViaLocation.Status = TDLocationStatus.Valid;			
			tdJourneyParameters.PublicViaLocation.NaPTANs = new TDNaptan[1];
			tdJourneyParameters.PublicViaLocation.NaPTANs[0] = new TDNaptan("9100STALBCY", new OSGridReference(515600,206900));
			tdJourneyParameters.PublicViaLocation.Locality = "locality";			

			//destination is "Radlett (Rail)"				
			tdJourneyParameters.DestinationLocation.NaPTANs[0].Naptan = "9100RADLETT";
			tdJourneyParameters.DestinationLocation.NaPTANs[0].GridReference.Easting = 516400;
			tdJourneyParameters.DestinationLocation.NaPTANs[0].GridReference.Northing = 199800;

			//this should fail because the origin and via have the same naptans
			jp = new JourneyPlanRunner(null);
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = jp.ValidateAndRun(tdSessionManager, tdJourneyParameters, "en");		
			Assert.IsTrue(!result, "Should have failed, origin and via locations are the same");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.OriginAndViaOverlap, "Unexpected error msg");


			//TEST 3 - destination and via overlap
			//set via to be the same as the destination location

			//origin is "St Albans (Rail)"			
			tdJourneyParameters.OriginLocation.NaPTANs[0].Naptan = "9100STALBCY";			
			tdJourneyParameters.OriginLocation.NaPTANs[0].GridReference.Easting = 515600;
			tdJourneyParameters.OriginLocation.NaPTANs[0].GridReference.Northing = 206900;

			//via is "Radlett (Rail)"							
			tdJourneyParameters.PublicViaLocation.NaPTANs[0].Naptan = "9100RADLETT";
			tdJourneyParameters.PublicViaLocation.NaPTANs[0].GridReference.Easting = 516400;
			tdJourneyParameters.PublicViaLocation.NaPTANs[0].GridReference.Northing = 199800;	

			//destination is "Radlett (Rail)"				
			tdJourneyParameters.DestinationLocation.NaPTANs[0].Naptan = "9100RADLETT";
			tdJourneyParameters.DestinationLocation.NaPTANs[0].GridReference.Easting = 516400;
			tdJourneyParameters.DestinationLocation.NaPTANs[0].GridReference.Northing = 199800;	

			jp = new JourneyPlanRunner(null);
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = jp.ValidateAndRun(tdSessionManager, tdJourneyParameters, "en");		
			Assert.IsTrue(!result, "Should have failed, destination and via locations are the same");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.DestinationAndViaOverlap, "Unexpected error msg");

			//TEST 4 - City-to-city (trunk..variety..) request where all naptans for all modes overlap			
			tdSessionManager.FindPageState = new FindTrunkPageState();

			tdJourneyParameters.PublicModes = new ModeType[] { ModeType.Rail, ModeType.Coach, ModeType.Air }; 			

			//origin (some of the 25 Naptans for St Albans location..) 			
			tdJourneyParameters.OriginLocation.NaPTANs = new TDNaptan[3];
			//rail naptan - Radlett station
			tdJourneyParameters.OriginLocation.NaPTANs[0] = new TDNaptan("9100RADLETT", new OSGridReference(516400,199800));
			//air naptan - Luton Airport
			tdJourneyParameters.OriginLocation.NaPTANs[1] = new TDNaptan("9200LTN", new OSGridReference(511950,221450));
			//coach naptan - Welwyn GC Bus Station
			tdJourneyParameters.OriginLocation.NaPTANs[2] = new TDNaptan("900054019", new OSGridReference(523895,213059));
		
			//destination (some of the 25 Naptans for Radlett location..) 				
			tdJourneyParameters.DestinationLocation.NaPTANs = new TDNaptan[3];
			//rail naptan - Radlett station
			tdJourneyParameters.DestinationLocation.NaPTANs[0] = new TDNaptan("9100RADLETT", new OSGridReference(516400,199800));
			//air naptan - Luton Airport
			tdJourneyParameters.DestinationLocation.NaPTANs[1] = new TDNaptan("9200LTN", new OSGridReference(511950,221450));
			//coach naptan - Welwyn GC Bus Station
			tdJourneyParameters.DestinationLocation.NaPTANs[2] = new TDNaptan("900054019", new OSGridReference(523895,213059));

			//this should fail because the origin and via have the same naptans
			jp = new JourneyPlanRunner(null);
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = jp.ValidateAndRun(tdSessionManager, tdJourneyParameters, "en");		
			Assert.IsTrue(!result, "Should have failed, origin and destination locations are the same");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.OriginAndDestinationOverlap, "Unexpected error msg");

			//TEST 5 - City-to-city (trunk..variety..) request where naptans for rail and coach modes overlap. 
					
			//Heathrow Airport
			tdJourneyParameters.OriginLocation.NaPTANs = new TDNaptan[1];
			tdJourneyParameters.OriginLocation.NaPTANs[0] = new TDNaptan("9200LHR", new OSGridReference(507728,176038));

			tdJourneyParameters.DestinationLocation.NaPTANs = new TDNaptan[1];
			tdJourneyParameters.DestinationLocation.NaPTANs[0] = new TDNaptan("9200EDB", new OSGridReference(0,0));			
	
			
			//This should pass as the air mode naptans are different, even though rail and coach are still the same
			jp = new JourneyPlanRunner(null);
			//via is "Radlett (Rail)"							
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = jp.ValidateAndRun(tdSessionManager, tdJourneyParameters, "en");		
			Assert.IsTrue(result, "Should have passed, origin and destination locations have different air naptans");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs.Length == 0, "Unexpected error msg");
			
		}

        /// <summary>
        /// Tests PerformToidsValidations method used to validate toids at origin and destination
        /// </summary>
        [Test]
        public void TestPerformToidsValidations()
        {
            //Ensure the origin and destination naptans do not overlap
            tdJourneyParameters.OriginLocation.NaPTANs[0] = new TDNaptan("naptan1", new OSGridReference(0, 1));
            tdJourneyParameters.DestinationLocation.NaPTANs[0] = new TDNaptan("naptan2", new OSGridReference(0, 0));

            tdJourneyParameters.OriginLocation.Locality = "locality1";
            tdJourneyParameters.DestinationLocation.Locality = "locality2";

            tdJourneyParameters.OriginLocation.Toid = new string[] {"Drive Toid 1"};
            tdJourneyParameters.DestinationLocation.Toid = new string[] { "Drive Toid 3" };

            tdJourneyParameters.IsOutwardRequired = true;
            tdJourneyParameters.IsReturnRequired = false;

            TDJourneyRequest journeyRequest = new TDJourneyRequest();

            journeyRequest.OriginLocation = tdJourneyParameters.OriginLocation;
            journeyRequest.DestinationLocation = tdJourneyParameters.DestinationLocation;
            
            journeyRequest.AvoidToidsOutward = new string[] { "Drive Toid 1" };
            journeyRequest.AvoidToidsReturn = new string[] { "Drive Toid 3" };

            JourneyPlanRunner result = new JourneyPlanRunner(null);
            TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
            bool pass = result.ValidateAndRun(tdSessionManager,1, 2, journeyRequest);

            Assert.IsTrue(!pass, "Invalid journey test");
            Assert.AreEqual(ValidationErrorID.RouteAffectedByClosuresErrors, tdSessionManager.ValidationError.ErrorIDs[0], "Origin Location should be invalid");

        }
	}
}
