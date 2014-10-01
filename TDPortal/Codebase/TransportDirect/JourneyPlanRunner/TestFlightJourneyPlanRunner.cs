// ***********************************************
// NAME 		: TestFlightJourneyPlanRunner.cs
// AUTHOR 		: Jonathan George
// DATE CREATED : 24/05/2004
// DESCRIPTION 	: Test the FlightJourneyPlanRunner object
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/TestFlightJourneyPlanRunner.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:48   mturner
//Initial revision.
//
//   Rev 1.18   Feb 10 2006 09:27:28   kjosling
//Turned off failing unit tests
//
//   Rev 1.17   Nov 09 2005 12:31:34   build
//Automatically merged from branch for stream2818
//
//   Rev 1.16.1.0   Oct 14 2005 15:10:44   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//
//   Rev 1.16   May 19 2005 15:08:48   rscott
//Updated for NUnit tests
//
//   Rev 1.15   May 17 2005 10:55:30   rscott
//Changes for IR1936 code review to get NUnit working
//
//   Rev 1.14   Mar 14 2005 15:54:42   COwczarek
//Remove references to Initialisation project - not required
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.13   Mar 01 2005 16:54:08   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.12   Feb 07 2005 12:10:24   RScott
//Assertion changed to Assert
//
//   Rev 1.11   Jan 19 2005 14:45:24   RScott
//DEL 7 - PublicViaLocation removed and PublicViaLocations[ ], PublicSoftViaLocations[ ], PublicNotViaLocations[] added.
//
//   Rev 1.10   Sep 14 2004 16:39:16   jmorrissey
//IR1507 - added test for CheckLocationsForOverlapping method
//
//   Rev 1.9   Sep 11 2004 18:08:04   RPhilpott
//Updated to reflect effect of changes to TDLocation.ToRequestPlace().
//Resolution for 1308: Find a coach - Can not find nearest coach stations
//Resolution for 1326: Find a variety of transport does not return any results
//Resolution for 1328: Find nearest stations/airports does not return any results
//
//   Rev 1.8   Aug 03 2004 16:09:58   RPhilpott
//Use new ITDSessionManager.IsFindAMode to determine if we are handling a trunk request.
//
//   Rev 1.7   Jul 28 2004 10:51:12   RPhilpott
//Removal of now-redundant FlightPlan parameter from ITDJourneyRequest (replaced by IsTRunkRequest for 6.1)
//
//   Rev 1.6   Jul 23 2004 18:26:38   RPhilpott
//DEL 6.1 Trunk Journey changes
//
//   Rev 1.5   Jun 24 2004 16:03:08   RPhilpott
//Fix Interval/Sequence bug
//
//   Rev 1.4   Jun 22 2004 11:47:14   RPhilpott
//Final completion of unit testing.
//
//   Rev 1.3   Jun 18 2004 14:56:46   RPhilpott
//Find-A-Flight validation - interim check-in.
//
//   Rev 1.2   Jun 16 2004 17:59:34   RPhilpott
//Find-A-Flight changes - interim check-in.
//
//   Rev 1.1   Jun 02 2004 13:59:26   jgeorge
//Interim check in

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
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.JourneyPlanning.CJPInterface;

using NUnit.Framework;


namespace TransportDirect.UserPortal.JourneyPlanRunner
{

	/// <summary>
	/// Summary description for TestFlightJourneyPlanRunner.
	/// </summary>
	[TestFixture]
	public class TestFlightJourneyPlanRunner
	{
		private ITDSessionManager tdSessionManager;

		private DateTime outwardTime;
		private DateTime returnTime;
		
		public TestFlightJourneyPlanRunner()
		{
		}
	
		[SetUp]
		public void SetUp()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init( new TestJourneyPlanRunnerInitialisation());
			IPropertyProvider property = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];
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
			
			// these "valid" tests use a Mock CJPManager that simply captures the 
			//  input TDJourneyRequest and then exposes it as a property, so that we can
			//  check that it has been correctly populated by our FlightJourneyPlanRunner ...

			TestMockCJPManager cjp = (TestMockCJPManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];

			TDJourneyParametersFlight jp = CreateDefaultParameters();

			jp.DirectFlightsOnly = true;
			Airport[] origins = new Airport[] { new Airport("LHR", "LHR", 4) };
			Airport[] dests   = new Airport[] { new Airport("EDB", "EDB", 1) };

			jp.SetOriginDetails(origins);
			jp.SetDestinationDetails(dests);

			FlightJourneyPlanRunner fjpr = new FlightJourneyPlanRunner(null);
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			bool result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");

			Assert.IsTrue(result,"Unexpected invalid journey");

			Thread.Sleep(1000);			// arbitrary length pause to allow asynchronous 
										//  call to CJPManager to complete first ...
			
			Assert.IsTrue(cjp.Request.IsTrunkRequest,"IsTrunkRequest should be true");
			Assert.IsTrue(cjp.Request.OriginLocation == jp.OriginLocation, "Origin location wrong");
			Assert.IsTrue(cjp.Request.DestinationLocation == jp.DestinationLocation, "Destination location wrong");
			Assert.IsTrue(cjp.Request.PublicViaLocations[0].Status == TDLocationStatus.Unspecified, "Via location wrong");
			Assert.IsTrue(cjp.Request.PublicViaLocations[0].NaPTANs.Length == 0, "Via location wrong");
			Assert.IsTrue(cjp.Request.SelectedOperators.Length == 0, "Selected operators wrong");
			Assert.IsTrue(cjp.Request.UseOnlySpecifiedOperators== jp.OnlyUseSpecifiedOperators, "Use selected operators wrong");
			Assert.IsTrue(cjp.Request.IsReturnRequired, "Return flag wrong");
			Assert.IsTrue(cjp.Request.OutwardAnyTime == jp.OutwardAnyTime, "Outward anytime");
			Assert.IsTrue(cjp.Request.ReturnAnyTime == jp.ReturnAnyTime, "Return anytime");
			Assert.IsTrue(cjp.Request.PublicAlgorithm == PublicAlgorithmType.NoChanges, "Algorithm");
			Assert.IsTrue(cjp.Request.Modes.Length == 1, "Modes");
			Assert.IsTrue(cjp.Request.Modes[0] == ModeType.Air, "Modes");

			// these three are dependent on property values ...
			Assert.IsTrue(cjp.Request.InterchangeSpeed == 0, "InterchangeSpeed");
			Assert.IsTrue(cjp.Request.WalkingSpeed == 80, "WalkingSpeed");
			Assert.IsTrue(cjp.Request.MaxWalkingTime == 20, "MaxWalkingTime");

			Assert.IsTrue((cjp.Request.OutwardDateTime[0].Year == outwardTime.Year
				&& cjp.Request.OutwardDateTime[0].Month == outwardTime.Month
				&& cjp.Request.OutwardDateTime[0].Day == outwardTime.Day
				&& cjp.Request.OutwardDateTime[0].Hour == outwardTime.Hour
				&& cjp.Request.OutwardDateTime[0].Minute == outwardTime.Minute), "OutwardDateTime");

			Assert.IsTrue((cjp.Request.ReturnDateTime[0].Year == returnTime.Year
				&& cjp.Request.ReturnDateTime[0].Month == returnTime.Month
				&& cjp.Request.ReturnDateTime[0].Day == returnTime.Day
				&& cjp.Request.ReturnDateTime[0].Hour == returnTime.Hour
				&& cjp.Request.ReturnDateTime[0].Minute == returnTime.Minute),"ReturnDateTime");

			Assert.IsTrue((cjp.Request.ExtraCheckinTime.Hour == 0 
				&& cjp.Request.ExtraCheckinTime.Minute == 0 
				&& cjp.Request.ExtraCheckinTime.Second == 0), "ExtraCheckinTime");

			Assert.IsTrue((cjp.Request.ViaLocationOutwardStopoverTime.Hour == 0 
				&& cjp.Request.ViaLocationOutwardStopoverTime.Minute == 0 
				&& cjp.Request.ViaLocationOutwardStopoverTime.Second == 0), "ViaLocationOutwardStopoverTime");
			
			Assert.IsTrue((cjp.Request.ViaLocationReturnStopoverTime.Hour == 0 
				&& cjp.Request.ViaLocationReturnStopoverTime.Minute == 0 
				&& cjp.Request.ViaLocationReturnStopoverTime.Second == 0), "ViaLocationReturnStopoverTime");

			jp = CreateDefaultParameters();

			jp.SetOriginDetails(origins);
			jp.SetDestinationDetails(dests);

			jp.OutwardAnyTime = false;
			jp.ReturnAnyTime = false;

			jp.DirectFlightsOnly = false;

			jp.ExtraCheckInTime = 48;
			jp.OutwardStopover = 2;
			jp.ReturnStopover = 3;

			TDLocation tempLoc = new TDLocation();
			TDNaptan tempNaptan = new TDNaptan("9200NCL", new OSGridReference(0, 0));
			tempLoc.NaPTANs = new TDNaptan[] { tempNaptan };
			tempLoc.Status = TDLocationStatus.Valid;

			jp.ViaLocation = tempLoc;
			jp.ViaLocation.Status = TDLocationStatus.Valid;
			
			fjpr = new FlightJourneyPlanRunner(null);
			jp.SelectedOperators = new String[] { "LH", "RA", "SA" };
			jp.OnlyUseSpecifiedOperators = true;

			fjpr = new FlightJourneyPlanRunner(null);
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");

			Assert.IsTrue(result, "Unexpected invalid journey");

			Thread.Sleep(1000);			// arbitrary length pause to allow asynchronous 
										//  call to CJPManager to complete first ...
			
			Assert.IsTrue(cjp.Request.IsTrunkRequest, "IsTrunkRequest should be true");
			Assert.IsTrue(cjp.Request.IsTrunkRequest, "Trunk Request should be true");
			Assert.IsTrue(cjp.Request.OriginLocation == jp.OriginLocation, "Origin location wrong");
			Assert.IsTrue(cjp.Request.DestinationLocation == jp.DestinationLocation, "Destination location wrong");

			Assert.IsTrue(cjp.Request.PublicViaLocations[0].Status == TDLocationStatus.Valid, "Via location wrong");
			Assert.IsTrue(cjp.Request.PublicViaLocations[0].NaPTANs.Length == 1, "Via location wrong");

			Assert.IsTrue(cjp.Request.SelectedOperators.Length == 3, "Selected operators wrong");
			Assert.IsTrue(cjp.Request.UseOnlySpecifiedOperators== jp.OnlyUseSpecifiedOperators, "Use selected operators wrong");

			Assert.IsTrue(cjp.Request.IsReturnRequired, "Return flag wrong");
			Assert.IsTrue(cjp.Request.OutwardAnyTime == jp.OutwardAnyTime, "Outward anytime");
			Assert.IsTrue(cjp.Request.ReturnAnyTime == jp.ReturnAnyTime, "Return anytime");
			Assert.IsTrue(cjp.Request.PublicAlgorithm == PublicAlgorithmType.Default, "Algorithm");
			Assert.IsTrue(cjp.Request.Modes.Length == 1, "Modes");
			Assert.IsTrue(cjp.Request.Modes[0] == ModeType.Air, "Modes");

			Assert.IsTrue((cjp.Request.ExtraCheckinTime.Hour == 0 
				&& cjp.Request.ExtraCheckinTime.Minute == 48
				&& cjp.Request.ExtraCheckinTime.Second == 0), "ExtraCheckinTime");

			Assert.IsTrue((cjp.Request.ViaLocationOutwardStopoverTime.Hour == 2 
				&& cjp.Request.ViaLocationOutwardStopoverTime.Minute == 0 
				&& cjp.Request.ViaLocationOutwardStopoverTime.Second == 0),"ViaLocationOutwardStopoverTime");
			
			Assert.IsTrue((cjp.Request.ViaLocationReturnStopoverTime.Hour == 3 
				&& cjp.Request.ViaLocationReturnStopoverTime.Minute == 0 
				&& cjp.Request.ViaLocationReturnStopoverTime.Second == 0), "ViaLocationReturnStopoverTime");
		}	


		[Test]
		public void TestSimpleOutwardDateValidation()		
		{
			TDJourneyParametersFlight jp = CreateDefaultParameters();

			FlightJourneyPlanRunner fjpr = new FlightJourneyPlanRunner(null);

			jp.OutwardHour = "";
			jp.OutwardMinute = "";

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			bool result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Outward date should have failed, no hour or minute");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.OutwardDateTimeInvalid, "Unexpected error msg");

			fjpr = new FlightJourneyPlanRunner(null);

			jp.OutwardHour = "25";

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Outward date should have failed, invalid hour");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.OutwardDateTimeInvalid, "Unexpected error msg");

			fjpr = new FlightJourneyPlanRunner(null);

			jp.OutwardHour = "18";
			jp.OutwardMinute = "98";

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Outward date should have failed, invalid minute");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.OutwardDateTimeInvalid, "Unexpected error msg");

			fjpr = new FlightJourneyPlanRunner(null);

			jp.OutwardDayOfMonth = "32";
			jp.OutwardMinute = "58";

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Outward date should have failed, invalid day");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.OutwardDateTimeInvalid, "Unexpected error msg");

			fjpr = new FlightJourneyPlanRunner(null);

			jp.OutwardDayOfMonth = "15";
			jp.OutwardMonthYear = "13 2004";

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Outward date should have failed, invalid month");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.OutwardDateTimeInvalid, "Unexpected error msg");
		}	


		[Test]
		public void TestSimpleReturnDateValidation()		
		{
			TDJourneyParametersFlight jp = CreateDefaultParameters();

			FlightJourneyPlanRunner fjpr = new FlightJourneyPlanRunner(null);

			jp.ReturnHour = "";
			jp.ReturnMinute = "";

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			bool result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Return date should have failed, no hour or minute");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.ReturnTimeMissing, "Unexpected error msg");

			fjpr = new FlightJourneyPlanRunner(null);

			jp.ReturnHour = "26";

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Return date should have failed, invalid hour");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.ReturnDateTimeInvalid, "Unexpected error msg");

			fjpr = new FlightJourneyPlanRunner(null);

			jp.ReturnHour = "08";
			jp.ReturnMinute = "61";

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Return date should have failed, invalid minute");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.ReturnDateTimeInvalid, "Unexpected error msg");

			fjpr = new FlightJourneyPlanRunner(null);

			jp.ReturnMonthYear = "06 2004";
			jp.ReturnDayOfMonth = "31";
			jp.ReturnMinute = "00";

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Return date should have failed, invalid day");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.ReturnDateTimeInvalid, "Unexpected error msg");

			fjpr = new FlightJourneyPlanRunner(null);

			jp.ReturnDayOfMonth = "30";
			jp.ReturnMonthYear = "13 2004";

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Return date should have failed, invalid month");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.ReturnDateTimeInvalid, "Unexpected error msg");
		
			fjpr = new FlightJourneyPlanRunner(null);

			jp.ReturnDayOfMonth = "";
			jp.ReturnMonthYear = "06 2004";
			jp.ReturnHour = "12";
			jp.ReturnMinute = "00";

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Return date should have failed, no day of month");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.ReturnDateMissing,"Unexpected error msg");
		}	


		[Test]
		[Ignore("ProjectNewkirk")]
		public void TestDateComparisonValidation()		
		{
			TDJourneyParametersFlight jp = CreateDefaultParameters();

			FlightJourneyPlanRunner fjpr = new FlightJourneyPlanRunner(null);

			DateTime now = DateTime.Now;

			DateTime dt = now.Subtract(new TimeSpan(1, 0, 0));  

			jp.OutwardHour = dt.Hour.ToString();
			jp.OutwardMinute = dt.Minute.ToString();
			jp.OutwardDayOfMonth = dt.Day.ToString();
			jp.OutwardMonthYear = dt.Month.ToString() + " " + dt.Year.ToString();

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			bool result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Outward date should have failed, in the past");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.OutwardDateTimeNotLaterThanNow, "Unexpected error msg");

			fjpr = new FlightJourneyPlanRunner(null);

			dt = now.Subtract(new TimeSpan(1, 0, 0, 0));
			
			jp.OutwardAnyTime = true;

			jp.OutwardHour = dt.Hour.ToString();
			jp.OutwardMinute = dt.Minute.ToString();
			jp.OutwardDayOfMonth = dt.Day.ToString();
			jp.OutwardMonthYear = dt.Month.ToString() + " " + dt.Year.ToString();

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Outward date should have failed, in the past");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.OutwardDateTimeInvalid, "Unexpected error msg");

			jp.OutwardAnyTime = false;

			dt = now.Subtract(new TimeSpan(0, 30, 0));   // assumes not running test bwtween 00.00 and 00.30 ... !

			jp.OutwardHour = dt.Hour.ToString();
			jp.OutwardMinute = dt.Minute.ToString();
			jp.OutwardDayOfMonth = dt.Day.ToString();
			jp.OutwardMonthYear = dt.Month.ToString() + " " + dt.Year.ToString();

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(result, "Unexpected invalid journey");
	
			dt = now.Subtract(new TimeSpan(1, 0, 0));

			jp.ReturnAnyTime = true;

			jp.ReturnHour = dt.Hour.ToString();
			jp.ReturnMinute = dt.Minute.ToString();
			jp.ReturnDayOfMonth = dt.Day.ToString();
			jp.ReturnMonthYear = dt.Month.ToString() + " " + dt.Year.ToString();

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.AreEqual(result, false, "Return date should have failed, in the past");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.ReturnDateTimeInvalid, "Unexpected error msg");

			fjpr = new FlightJourneyPlanRunner(null);

			dt = now.Subtract(new TimeSpan(1, 0, 0, 0));
			
			jp.ReturnAnyTime = false;

			jp.ReturnHour = dt.Hour.ToString();
			jp.ReturnMinute = dt.Minute.ToString();
			jp.ReturnDayOfMonth = dt.Day.ToString();
			jp.ReturnMonthYear = dt.Month.ToString() + " " + dt.Year.ToString();

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.AreEqual(result, false, "Return date should have failed, in the past");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.ReturnDateTimeNotLaterThanOutwardDateTime, "Unexpected error msg");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[1] == ValidationErrorID.OutwardAndReturnDateTimeNotLaterThanNow, "Unexpected error msg");

			dt = now.Subtract(new TimeSpan(1, 0, 0, 0));
			
			jp.ReturnAnyTime = true;
			jp.ReturnHour = dt.Hour.ToString();
			jp.ReturnMinute = dt.Minute.ToString();
			jp.ReturnDayOfMonth = dt.Day.ToString();
			jp.ReturnMonthYear = dt.Month.ToString() + " " + dt.Year.ToString();

			dt = now.Subtract(new TimeSpan(0, 30, 0));   // assumes not running test bwtween 00.00 and 00.30 ... !

			jp.OutwardAnyTime = true;
			jp.OutwardHour = dt.Hour.ToString();
			jp.OutwardMinute = dt.Minute.ToString();
			jp.OutwardDayOfMonth = dt.Day.ToString();
			jp.OutwardMonthYear = dt.Month.ToString() + " " + dt.Year.ToString();

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Return date should have failed, in the past");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.OutwardAndReturnDateTimeInvalid, "Unexpected error msg");

			jp.ReturnAnyTime = false;

			dt = now.Subtract(new TimeSpan(0, 30, 0));   // assumes not running test bwtween 00.00 and 00.30 ... !

			jp.ReturnHour = dt.Hour.ToString();
			jp.ReturnMinute = dt.Minute.ToString();
			jp.ReturnDayOfMonth = dt.Day.ToString();
			jp.ReturnMonthYear = dt.Month.ToString() + " " + dt.Year.ToString();

			jp.OutwardAnyTime = false;

			dt = now.Subtract(new TimeSpan(0, 15, 0));   // assumes not running test bwtween 00.00 and 00.30 ... !
			
			jp.OutwardHour = dt.Hour.ToString();
			jp.OutwardMinute = dt.Minute.ToString();
			jp.OutwardDayOfMonth = dt.Day.ToString();
			jp.OutwardMonthYear = dt.Month.ToString() + " " + dt.Year.ToString();

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(result, "Unexpected invalid journey");
	
		}	


		[Test]
		[Ignore("ProjectNewkirk")]
		public void TestRouteAndOperatorValidation()		
		{
			TDJourneyParametersFlight jp = CreateDefaultParameters();

			FlightJourneyPlanRunner fjpr = new FlightJourneyPlanRunner(null);

			jp.DirectFlightsOnly = true;
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			bool result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Should have failed, no routes found");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.NoValidRoutes, "Unexpected error msg");
		
			fjpr = new FlightJourneyPlanRunner(null);
			jp.DirectFlightsOnly = false;
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(result, "Should have passed, no route validation for indirect flights");
		
			fjpr = new FlightJourneyPlanRunner(null);
			jp.DirectFlightsOnly = true;
			jp.OnlyUseSpecifiedOperators = true;
			jp.SelectedOperators = new String[] { "RR" };
			Airport[] origins = new Airport[] { new Airport("LHR", "LHR", 4) };  // => operators BA, RA, LH, SA
			jp.SetOriginDetails(origins);
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Should have failed, no valid operators found");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.InvalidOperatorSelection, "Unexpected error msg");
		
			fjpr = new FlightJourneyPlanRunner(null);
			jp.SelectedOperators = new String[] { "RR", "SA" };
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(result, "Should have passed, selected operator in list for routes");
		
			fjpr = new FlightJourneyPlanRunner(null);
			jp.SelectedOperators = new String[] { "LH" };
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(result, "Should have passed, selected operators in list for routes");
	
			fjpr = new FlightJourneyPlanRunner(null);
			jp.SelectedOperators = new String[] { "LH", "SA" };
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(result, "Should have passed, selected operators in list for routes");
	
			fjpr = new FlightJourneyPlanRunner(null);
			jp.SelectedOperators = new String[] { "LH", "RA", "SA" };
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(result, "Should have passed, selected operators in list for routes");
	
			fjpr = new FlightJourneyPlanRunner(null);
			jp.OnlyUseSpecifiedOperators = false;
			jp.SelectedOperators = new String[] { "BA", "RA", "LH", "SA" };
			origins = new Airport[] { new Airport("LHR", "LHR", 4) };  // => operators BA, RA, LH, SA
			jp.SetOriginDetails(origins);
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Should have failed, all valid operators excluded");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.InvalidOperatorSelection, "Unexpected error msg");
		
			fjpr = new FlightJourneyPlanRunner(null);
			jp.OnlyUseSpecifiedOperators = false;
			jp.SelectedOperators = new String[] { "BA", "RA", "LH", "SA", "RR" };
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Should have failed, all valid operators excluded");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.InvalidOperatorSelection, "Unexpected error msg");
	
			fjpr = new FlightJourneyPlanRunner(null);
			jp.OnlyUseSpecifiedOperators = false;
			jp.SelectedOperators = new String[] { "BA", "RR", "LH", "SA" };
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(result, "Should have passed, not all valid operators excluded");

		}

		[Test]
		public void TestLocationValidation()		
		{
			TDJourneyParametersFlight jp = CreateDefaultParameters();

			jp.SetOriginDetails(null);

			TDLocation tempLoc = new TDLocation();
			TDNaptan tempNaptan = new TDNaptan("9200RGP", new OSGridReference(123456, 456789));
			tempLoc.NaPTANs = new TDNaptan[] { tempNaptan };
    
			jp.OriginLocation = tempLoc;

			FlightJourneyPlanRunner fjpr = new FlightJourneyPlanRunner(null);

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			bool result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Should have failed, invalid origin");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.OriginLocationInvalid, "Unexpected error msg");

			DateTime now = DateTime.Now;

			DateTime dt = now.Subtract(new TimeSpan(1, 0, 0));  

			jp.OutwardHour = dt.Hour.ToString();
			jp.OutwardMinute = dt.Minute.ToString();
			jp.OutwardDayOfMonth = dt.Day.ToString();
			jp.OutwardMonthYear = dt.Month.ToString() + " " + dt.Year.ToString();
		
			fjpr = new FlightJourneyPlanRunner(null);

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Should have failed, invalid origin");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs.Length == 2, "Unexpected no of error msgs");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.OutwardDateTimeNotLaterThanNow, "Unexpected error msg");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[1] == ValidationErrorID.OriginLocationInvalidAndOtherErrors, "Unexpected error msg");
		
			jp = CreateDefaultParameters();

			jp.SetDestinationDetails(null);
			jp.DestinationLocation = tempLoc;

			fjpr = new FlightJourneyPlanRunner(null);

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Should have failed, invalid destination");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.DestinationLocationInvalid, "Unexpected error msg");

			dt = now.Subtract(new TimeSpan(1, 0, 0));  

			jp.OutwardHour = dt.Hour.ToString();
			jp.OutwardMinute = dt.Minute.ToString();
			jp.OutwardDayOfMonth = dt.Day.ToString();
			jp.OutwardMonthYear = dt.Month.ToString() + " " + dt.Year.ToString();
		
			fjpr = new FlightJourneyPlanRunner(null);

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Should have failed, invalid destination");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs.Length == 2, "Unexpected no of error msgs");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.OutwardDateTimeNotLaterThanNow, "Unexpected error msg");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[1] == ValidationErrorID.DestinationLocationInvalidAndOtherErrors, "Unexpected error msg");
		
			jp = CreateDefaultParameters();

			jp.ViaLocation = null;
			jp.OutwardStopover = 90;

			fjpr = new FlightJourneyPlanRunner(null);

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result,"Should have failed, invalid via");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.PublicViaLocationInvalid, "Unexpected error msg");
			
			jp = CreateDefaultParameters();

			jp.ViaLocation = null;
			jp.ReturnStopover = 90;

			fjpr = new FlightJourneyPlanRunner(null);

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Should have failed, invalid via");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.PublicViaLocationInvalid, "Unexpected error msg");

			jp = CreateDefaultParameters();

			jp.ViaLocation = tempLoc;
			jp.OutwardStopover = 90;

			fjpr = new FlightJourneyPlanRunner(null);

			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(!result, "Should have failed, invalid via");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.PublicViaLocationInvalid, "Unexpected error msg");

		}

		//IR1507
		[Test]
		[Ignore("ProjectNewkirk")]
		public void TestCheckLocationsForOverlapping()
		{
			TDJourneyParametersFlight jp = CreateDefaultParameters();

			//TEST 1 - origin and destination overlap

			jp.SetOriginDetails(null);
			jp.SetDestinationDetails(null);
			jp.ViaLocation = null;

			jp.DirectFlightsOnly = true;
			Airport[] origins = new Airport[] { new Airport("LHR", "LHR", 4) };
			Airport[] dests   = new Airport[] { new Airport("LHR", "LHR", 4) };

			jp.SetOriginDetails(origins);
			jp.SetDestinationDetails(origins);			

			FlightJourneyPlanRunner fjpr = new FlightJourneyPlanRunner(null);	
			
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			bool result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");			
			Assert.AreEqual(result, false, "Should have failed, origin and destination locations are the same");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.OriginAndDestinationOverlap, "Unexpected error msg");


			//TEST 2 - origin and via overlap			

			//set via location to be the same as the origin and the destination to be different
			jp.SetOriginDetails(null);
			jp.SetDestinationDetails(null);
			jp.ViaLocation = null;
			jp.DirectFlightsOnly = false;

			origins = new Airport[] { new Airport("LHR", "LHR", 4) };
			dests   = new Airport[] { new Airport("EDB", "EDB", 1) };	

			jp.SetOriginDetails(origins);
			jp.SetDestinationDetails(dests);

			//set via to be the same as the origin airport
			jp.ViaLocation = jp.OriginLocation;			

			fjpr = new FlightJourneyPlanRunner(null);
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");		
			Assert.AreEqual(result, false, "Should have failed, origin and via locations are the same");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.OriginAndViaOverlap, "Unexpected error msg");


			//TEST 3 - destination and via overlap    
			jp.SetOriginDetails(null);
			jp.SetDestinationDetails(null);
			jp.ViaLocation = null;
			jp.ViaSelectedAirport = null;
			jp.DirectFlightsOnly = false;

			origins = new Airport[] { new Airport("LHR", "LHR", 4) };
			dests   = new Airport[] { new Airport("EDB", "EDB", 1) };	
			
			jp.SetOriginDetails(origins);
			jp.SetDestinationDetails(dests);
			

			//set via to be the same as the destination airport	
			jp.ViaLocation.NaPTANs = jp.DestinationLocation.NaPTANs;			

			fjpr = new FlightJourneyPlanRunner(null);
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.AreEqual(result, false, "Should have failed, destination and via locations are the same");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs[0] == ValidationErrorID.DestinationAndViaOverlap, "Unexpected error msg");

			//TEST 4 - should pass validation as origin and destination are valid and do not overlap
			jp.SetOriginDetails(null);
			jp.SetDestinationDetails(null);
			jp.ViaLocation = null;
			jp.ViaSelectedAirport = null;
			jp.DirectFlightsOnly = true;
		
			origins = new Airport[] { new Airport("LHR", "LHR", 4) };
			dests   = new Airport[] { new Airport("EDB", "EDB", 1) };

			jp.SetOriginDetails(origins);
			jp.SetDestinationDetails(dests);

			fjpr = new FlightJourneyPlanRunner(null);
			TDSessionManager.Current.AsyncCallState = new JourneyPlanState();
			result = fjpr.ValidateAndRun(tdSessionManager, jp, "en");
			Assert.IsTrue(result,"Should have passed, origin and destination locations are valid and different");
			Assert.IsTrue(tdSessionManager.ValidationError.ErrorIDs.Length == 0, "Unexpected error msg");

		}

		private TDJourneyParametersFlight CreateDefaultParameters()
		{

			outwardTime = DateTime.Now.AddMonths(1);
			returnTime = outwardTime.AddHours(6); 

			tdSessionManager.FindPageState = new FindFlightPageState();

			TDJourneyParametersFlight jp = new TDJourneyParametersFlight();

			jp.IsReturnRequired = true;

			jp.OutwardAnyTime = false;
			jp.ReturnAnyTime = false;

			jp.OutwardHour = outwardTime.Hour.ToString();
			jp.OutwardMinute = outwardTime.Minute.ToString();
			jp.OutwardDayOfMonth = outwardTime.Day.ToString();
			jp.OutwardMonthYear = outwardTime.Month.ToString() + " " + outwardTime.Year.ToString();

			jp.ReturnHour = returnTime.Hour.ToString();
			jp.ReturnMinute = returnTime.Minute.ToString();
			jp.ReturnDayOfMonth = returnTime.Day.ToString();
			jp.ReturnMonthYear = returnTime.Month.ToString() + " " + returnTime.Year.ToString();

			Airport[] origins = new Airport[] { new Airport("XXX", "XXX", 4) };
			Airport[] dests   = new Airport[] { new Airport("YYY", "YYY", 1) };

			jp.SetOriginDetails(origins);
			jp.SetDestinationDetails(dests);
			
			jp.DirectFlightsOnly = false;

			return jp;
		}

	}
}
