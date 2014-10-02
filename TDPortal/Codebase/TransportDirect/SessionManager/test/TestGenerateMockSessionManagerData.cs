using System;
using TransportDirect.Common;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Summary description for TestMockSessionBuilder.
	/// </summary>
	[CLSCompliant(false)]	
	public class TestMockSessionBuilder
	{
		private TestMockSessionManager mockSession;

		public TestMockSessionManager GetTestMockSessionManager { get { return mockSession; } }

		public TestMockSessionBuilder( bool Auth, string Sesh, bool HasRealSession )
		{
			// Instanciate the mockSession object
			mockSession  = new TestMockSessionManager( Auth, Sesh, HasRealSession );

			// Fill in the test data
			InitialiseTDSessionManager();
		}
		
		private void InitialiseTDSessionManager()
		{
			
			// Initialise TDJourneyViewState
			mockSession.JourneyViewState = new TDJourneyViewState();
			
			// Update the mock request by populating some of the fields
			// required for the JourneysSearchedForControl
			PopulateTDJourneyViewState2();

			// Create a mock TDJourneyResult
			TDJourneyResult result = new TDJourneyResult(1);
			result.AddResult(CreateCJPResult2(), true, null, null, null, null, "ssss", false, -1);
			
			// Create a return CJP Result
			JourneyResult returnResult = CreateCJPResult();

			// add the return result
			result.AddResult(returnResult, false, null, null, null, null, "ssss", false, -1);

			// Now point the reference in TDSessionManager to the result
			mockSession.JourneyResult = result;

			// Initialise indexes
			mockSession.JourneyViewState.SelectedOutwardJourney = 0;
			mockSession.JourneyViewState.SelectedReturnJourney = 0;

			mockSession.JourneyViewState.JourneyLeavingTimeSearchType = false;
			mockSession.JourneyViewState.JourneyReturningTimeSearchType = true;


		}

		#region Methods to create Test Data to populate session with
		
		/// <summary>
		/// Create a single CJPResult public journey. (Used for summary line testing)
		/// </summary>
		/// <returns></returns>
		private TransportDirect.JourneyPlanning.CJPInterface.PublicJourney CreatePublicJourney2()
		{
			TransportDirect.JourneyPlanning.CJPInterface.PublicJourney result = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney();

			result.legs = new Leg[1];

			result.legs[0] = new TimedLeg();

			result.legs[0].mode = ModeType.Walk;
			result.legs[0].validated = true;

			result.legs[0].board = new Event();
			result.legs[0].board.activity = ActivityType.Depart;
			result.legs[0].board.departTime = new DateTime(2003, 6, 5, 3, 4, 0);
//			result.legs[0].board.pass = false;
			result.legs[0].board.stop = new Stop();
			result.legs[0].board.stop.NaPTANID = "Board NAPTANID";
			result.legs[0].board.stop.name = "16 Goulden Road, London, SW11 2JL";
			result.legs[0].services = new Service[2];
			result.legs[0].services[0] = new Service();
			result.legs[0].services[0].destinationBoard = "Kings Cross St. Pancras";
			result.legs[0].services[0].operatorCode = "HJD";
			result.legs[0].services[0].operatorName = "Greater London Buses";
			result.legs[0].services[0].serviceNumber = "96";
			result.legs[0].services[1] = new Service();
			result.legs[0].services[1].destinationBoard = "Farringdon";
			result.legs[0].services[1].operatorCode = "HJD";
			result.legs[0].services[1].operatorName = "StageCoach";
			result.legs[0].services[1].serviceNumber = "32";

			result.legs[0].alight = new Event();
			result.legs[0].alight.activity = ActivityType.Arrive;
			result.legs[0].alight.arriveTime = new DateTime(2003, 6, 5, 3, 14, 0);
//			result.legs[0].alight.pass = false;
			result.legs[0].alight.stop = new Stop();
			result.legs[0].alight.stop.NaPTANID = "Alight NAPTANID";
			result.legs[0].alight.stop.name = "Kings Cross Station";

			return result;
		}

		private JourneyResult CreateCJPResult()
		{
			JourneyResult result = new JourneyResult();

			result.publicJourneys = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney[2];
			
			result.publicJourneys[0] = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney();
			result.publicJourneys[0].legs = new Leg[2];

			result.publicJourneys[0].legs[0] = new TimedLeg();
			result.publicJourneys[0].legs[0].mode = ModeType.Bus;
			result.publicJourneys[0].legs[0].validated = true;

			result.publicJourneys[0].legs[0].board = new Event();
			result.publicJourneys[0].legs[0].board.activity = ActivityType.Depart;
			result.publicJourneys[0].legs[0].board.departTime = new DateTime(2003, 9, 17, 15, 12, 0);
//			result.publicJourneys[0].legs[0].board.pass = false;
			result.publicJourneys[0].legs[0].board.stop = new Stop();
			result.publicJourneys[0].legs[0].board.stop.NaPTANID = "Board NAPTANID";
			result.publicJourneys[0].legs[0].board.stop.name = "16 Hill Street";

			result.publicJourneys[0].legs[0].alight = new Event();
			result.publicJourneys[0].legs[0].alight.activity = ActivityType.Arrive;
			result.publicJourneys[0].legs[0].alight.arriveTime = new DateTime (2003, 9, 17, 15, 42, 0);
//			result.publicJourneys[0].legs[0].alight.pass = false;
			result.publicJourneys[0].legs[0].alight.stop = new Stop();
			result.publicJourneys[0].legs[0].alight.stop.NaPTANID = "Alight NAPTANID";
			result.publicJourneys[0].legs[0].alight.stop.name = "Cowcaddens Underground Station";

			result.publicJourneys[0].legs[0].services = new Service[1];
			result.publicJourneys[0].legs[0].services[0] = new Service();
			result.publicJourneys[0].legs[0].services[0].destinationBoard = "Stanmore";
			result.publicJourneys[0].legs[0].services[0].operatorCode = "GU";
			result.publicJourneys[0].legs[0].services[0].operatorName = "Strathclyde Transport";
			//result.publicJourneys[0].legs[0].services[0].serviceNumber = "";

			result.publicJourneys[0].legs[1] = new TimedLeg();
			result.publicJourneys[0].legs[1].mode = ModeType.Coach;
			result.publicJourneys[0].legs[1].validated = true;

			result.publicJourneys[0].legs[1].board = new Event();
			result.publicJourneys[0].legs[1].board.activity = ActivityType.Depart;
			result.publicJourneys[0].legs[1].board.departTime = new DateTime(2003, 9, 17, 15, 42, 0);
//			result.publicJourneys[0].legs[1].board.pass = false;
			result.publicJourneys[0].legs[1].board.stop = new Stop();
			result.publicJourneys[0].legs[1].board.stop.NaPTANID = "Board NAPTANID";
			result.publicJourneys[0].legs[1].board.stop.name = "Cowcaddens Underground Station";

			result.publicJourneys[0].legs[1].alight = new Event();
			result.publicJourneys[0].legs[1].alight.activity = ActivityType.Arrive;
			result.publicJourneys[0].legs[1].alight.arriveTime = new DateTime (2003, 9, 17, 15, 48, 0);
//			result.publicJourneys[0].legs[1].alight.pass = false;
			result.publicJourneys[0].legs[1].alight.stop = new Stop();
			result.publicJourneys[0].legs[1].alight.stop.NaPTANID = "Alight NAPTANID";
			result.publicJourneys[0].legs[1].alight.stop.name = "12345XYZ Station";

			result.publicJourneys[0].legs[1].services = new Service[1];
			result.publicJourneys[0].legs[1].services[0] = new Service();
			result.publicJourneys[0].legs[1].services[0].destinationBoard = "Birmingham";
			result.publicJourneys[0].legs[1].services[0].operatorCode = "ZZZZ";
			result.publicJourneys[0].legs[1].services[0].operatorName = "Birmingham Transport";
			//result.legs[0].services[0].serviceNumber = "96";

			// ----

			result.publicJourneys[1] = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney();
			result.publicJourneys[1].legs = new Leg[3];

			result.publicJourneys[1].legs[0] = new TimedLeg();
			result.publicJourneys[1].legs[0].mode = ModeType.Bus;
			result.publicJourneys[1].legs[0].validated = true;

			result.publicJourneys[1].legs[0].board = new Event();
			result.publicJourneys[1].legs[0].board.activity = ActivityType.Depart;
			result.publicJourneys[1].legs[0].board.departTime = new DateTime(2003, 9, 16, 13, 30, 0);
//			result.publicJourneys[1].legs[0].board.pass = false;
			result.publicJourneys[1].legs[0].board.stop = new Stop();
			result.publicJourneys[1].legs[0].board.stop.NaPTANID = "Board NAPTANID";
			result.publicJourneys[1].legs[0].board.stop.name = "Reading Station";

			result.publicJourneys[1].legs[0].alight = new Event();
			result.publicJourneys[1].legs[0].alight.activity = ActivityType.Arrive;
			result.publicJourneys[1].legs[0].alight.arriveTime = new DateTime (2003, 9, 16, 13, 44, 0);
//			result.publicJourneys[1].legs[0].alight.pass = false;
			result.publicJourneys[1].legs[0].alight.stop = new Stop();
			result.publicJourneys[1].legs[0].alight.stop.NaPTANID = "Alight NAPTANID";
			result.publicJourneys[1].legs[0].alight.stop.name = "Edinburgh Waverly";

			result.publicJourneys[1].legs[0].services = new Service[1];
			result.publicJourneys[1].legs[0].services[0] = new Service();
			result.publicJourneys[1].legs[0].services[0].destinationBoard = "Edinburgh Waverly";
			result.publicJourneys[1].legs[0].services[0].operatorCode = "AAA";
			result.publicJourneys[1].legs[0].services[0].operatorName = "Scottish Railway";
			result.publicJourneys[1].legs[0].services[0].serviceNumber = "S123";

			result.publicJourneys[1].legs[1] = new TimedLeg();
			result.publicJourneys[1].legs[1].mode = ModeType.Metro;
			result.publicJourneys[1].legs[1].validated = true;

			result.publicJourneys[1].legs[1].board = new Event();
			result.publicJourneys[1].legs[1].board.activity = ActivityType.Depart;
			result.publicJourneys[1].legs[1].board.departTime = new DateTime(2003, 9, 16, 13, 45, 0);
//			result.publicJourneys[1].legs[1].board.pass = false;
			result.publicJourneys[1].legs[1].board.stop = new Stop();
			result.publicJourneys[1].legs[1].board.stop.NaPTANID = "Board NAPTANID";
			result.publicJourneys[1].legs[1].board.stop.name = "Edinburgh Waverly";

			result.publicJourneys[1].legs[1].alight = new Event();
			result.publicJourneys[1].legs[1].alight.activity = ActivityType.Arrive;
			result.publicJourneys[1].legs[1].alight.arriveTime = new DateTime (2003, 9, 16, 14, 48, 25);
//			result.publicJourneys[1].legs[1].alight.pass = false;
			result.publicJourneys[1].legs[1].alight.stop = new Stop();
			result.publicJourneys[1].legs[1].alight.stop.NaPTANID = "Alight NAPTANID";
			result.publicJourneys[1].legs[1].alight.stop.name = "Edinburgh High Street";

			result.publicJourneys[1].legs[1].services = new Service[1];
			result.publicJourneys[1].legs[1].services[0] = new Service();
			result.publicJourneys[1].legs[1].services[0].destinationBoard = "Hill Street via Smiths Avenue";
			result.publicJourneys[1].legs[1].services[0].operatorCode = "AAA";
			result.publicJourneys[1].legs[1].services[0].operatorName = "Strathclyde Buses";
			result.publicJourneys[1].legs[1].services[0].serviceNumber = "59";

			//-- 

			result.publicJourneys[1].legs[2] = new TimedLeg();
			result.publicJourneys[1].legs[2].mode = ModeType.Bus;
			result.publicJourneys[1].legs[2].validated = true;

			result.publicJourneys[1].legs[2].board = new Event();
			result.publicJourneys[1].legs[2].board.activity = ActivityType.Depart;
			result.publicJourneys[1].legs[2].board.departTime = new DateTime(2003, 9, 16, 14, 55, 25);
//			result.publicJourneys[1].legs[2].board.pass = false;
			result.publicJourneys[1].legs[2].board.stop = new Stop();
			result.publicJourneys[1].legs[2].board.stop.NaPTANID = "Board NAPTANID";
			result.publicJourneys[1].legs[2].board.stop.name = "Edinburgh High Street";

			result.publicJourneys[1].legs[2].alight = new Event();
			result.publicJourneys[1].legs[2].alight.activity = ActivityType.Arrive;
			result.publicJourneys[1].legs[2].alight.arriveTime = new DateTime (2003, 9, 16, 15, 18, 25);
//			result.publicJourneys[1].legs[2].alight.pass = false;
			result.publicJourneys[1].legs[2].alight.stop = new Stop();
			result.publicJourneys[1].legs[2].alight.stop.NaPTANID = "Alight NAPTANID";
			result.publicJourneys[1].legs[2].alight.stop.name = "Glasgow";

			result.publicJourneys[1].legs[2].services = new Service[1];
			result.publicJourneys[1].legs[2].services[0] = new Service();
			result.publicJourneys[1].legs[2].services[0].destinationBoard = "Rovers Place";
			result.publicJourneys[1].legs[2].services[0].operatorCode = "AAA";
			result.publicJourneys[1].legs[2].services[0].operatorName = "Strathclyde Buses";
			result.publicJourneys[1].legs[2].services[0].serviceNumber = "99";
			
			// create private journeys
			result.privateJourneys = new TransportDirect.JourneyPlanning.CJPInterface.PrivateJourney[2];
			result.privateJourneys[0] = new TransportDirect.JourneyPlanning.CJPInterface.PrivateJourney();

			string name = "name";

			result.privateJourneys[0].startTime = new DateTime(2003, 7, 2, 16, 0, 0);
			result.privateJourneys[0].congestion = true;
			result.privateJourneys[0].start = new StopoverSection();
			result.privateJourneys[0].start.time = new DateTime(2003, 7, 2, 16, 0, 0);
			result.privateJourneys[0].start.name = name + " Section Name";
			result.privateJourneys[0].start.node = new ITNNode();
			result.privateJourneys[0].start.node.TOID = name + " TOID";

			result.privateJourneys[0].finish = new StopoverSection();
			result.privateJourneys[0].finish.time = new DateTime(2003, 7, 2, 19, 00, 0);;
			result.privateJourneys[0].finish.name = name + " Section Name";
			result.privateJourneys[0].finish.node = new ITNNode();
			result.privateJourneys[0].finish.node.TOID = name + " TOID";

			DriveSection driveSection = new DriveSection();

			// roundabout section
			driveSection.time = new DateTime(2003, 7, 2, 16, 0, 0);
			driveSection.name = "Gonzer Road";
			driveSection.distance = 1230;
			driveSection.turnCount = 3;
			driveSection.turnDirection = TurnDirection.Continue;
			driveSection.turnAngle = TurnAngle.Continue;
			driveSection.roundabout = true;
			driveSection.throughRoute = false;
			driveSection.cost = 30;
			driveSection.links = new ITNLink[1];
			driveSection.links[0] = new ITNLink();
			driveSection.links[0].TOID = "Drive TOID";
			driveSection.links[0].congestion = 1;

			DriveSection driveSection2 = new DriveSection();

			// through route section
			driveSection2.time = new DateTime(2003, 7, 2, 16, 25, 0);
			driveSection2.name = "St. Pauls Avenue";
			driveSection2.number = "A23";
			driveSection2.distance = 333;
			driveSection2.turnCount = 3;
			driveSection2.turnDirection = TurnDirection.Left;
			driveSection2.turnAngle = TurnAngle.Bear;
			driveSection2.roundabout = false;
			driveSection2.throughRoute = true;
			driveSection2.cost = 30;
			driveSection2.links = new ITNLink[1];
			driveSection2.links[0] = new ITNLink();
			driveSection2.links[0].TOID = "Drive TOID";
			driveSection2.links[0].congestion = 1;

			DriveSection driveSection3 = new DriveSection();

			// continue - continue section
			driveSection3.time = new DateTime(2003, 7, 2, 16, 34, 0);
			driveSection3.name = "Walm Lane";
			driveSection3.distance = 1524;
			driveSection3.turnCount = 3;
			driveSection3.turnDirection = TurnDirection.Continue;
			driveSection3.turnAngle = TurnAngle.Continue;
			driveSection3.roundabout = false;
			driveSection3.throughRoute = false;
			driveSection3.cost = 30;
			driveSection3.links = new ITNLink[1];
			driveSection3.links[0] = new ITNLink();
			driveSection3.links[0].TOID = "Drive TOID";
			driveSection3.links[0].congestion = 1;

			DriveSection driveSection4 = new DriveSection();

			// continue - left
			driveSection4.time = new DateTime(2003, 7, 2, 16, 50, 0);
			driveSection4.number = "M8";
			driveSection4.distance = 3000;
			driveSection4.turnCount = 3;
			driveSection4.turnDirection = TurnDirection.Left;
			driveSection4.turnAngle = TurnAngle.Continue;
			driveSection4.roundabout = false;
			driveSection4.throughRoute = false;
			driveSection4.cost = 30;
			driveSection4.links = new ITNLink[1];
			driveSection4.links[0] = new ITNLink();
			driveSection4.links[0].TOID = "Drive TOID";
			driveSection4.links[0].congestion = 1;

			
			DriveSection driveSection5 = new DriveSection();

			// continue - right
			driveSection5.time = new DateTime(2003, 7, 2, 16, 55, 0);
			driveSection5.name = "123 Smith Street";
			driveSection5.number = "A3";
			driveSection5.distance = 50;
			driveSection5.turnCount = 3;
			driveSection5.turnDirection = TurnDirection.Right;
			driveSection5.turnAngle = TurnAngle.Continue;
			driveSection5.roundabout = false;
			driveSection5.throughRoute = false;
			driveSection5.cost = 30;
			driveSection5.links = new ITNLink[1];
			driveSection5.links[0] = new ITNLink();
			driveSection5.links[0].TOID = "Drive TOID";
			driveSection5.links[0].congestion = 1;


			DriveSection driveSection6 = new DriveSection();

			// continue - mini roundabout continue
			driveSection6.time = new DateTime(2003, 7, 2, 16, 57, 0);
			driveSection6.name = "123 Smith Avenue";
			driveSection6.distance = 502;
			driveSection6.turnCount = 3;
			driveSection6.turnDirection = TurnDirection.MiniRoundaboutContinue;
			driveSection6.turnAngle = TurnAngle.Continue;
			driveSection6.roundabout = false;
			driveSection6.throughRoute = false;
			driveSection6.cost = 30;
			driveSection6.links = new ITNLink[1];
			driveSection6.links[0] = new ITNLink();
			driveSection6.links[0].TOID = "Drive TOID";
			driveSection6.links[0].congestion = 1;

			DriveSection driveSection7 = new DriveSection();

			// continue - mini roundabout left
			driveSection7.time = new DateTime(2003, 7, 2, 17, 00, 0);
			driveSection7.name = "6 HorseFerry Road";
			driveSection7.distance = 90;
			driveSection7.turnCount = 3;
			driveSection7.turnDirection = TurnDirection.MiniRoundaboutLeft;
			driveSection7.turnAngle = TurnAngle.Continue;
			driveSection7.roundabout = false;
			driveSection7.throughRoute = false;
			driveSection7.cost = 30;
			driveSection7.links = new ITNLink[1];
			driveSection7.links[0] = new ITNLink();
			driveSection7.links[0].TOID = "Drive TOID";
			driveSection7.links[0].congestion = 1;

			DriveSection driveSection8 = new DriveSection();

			// continue - mini roundabout right
			driveSection8.time = new DateTime(2003, 7, 2, 17, 05, 0);
			driveSection8.name = "3 Great Western Road";
			driveSection8.distance = 910;
			driveSection8.turnCount = 3;
			driveSection8.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection8.turnAngle = TurnAngle.Continue;
			driveSection8.roundabout = false;
			driveSection8.throughRoute = false;
			driveSection8.cost = 30;
			driveSection8.links = new ITNLink[1];
			driveSection8.links[0] = new ITNLink();
			driveSection8.links[0].TOID = "Drive TOID";
			driveSection8.links[0].congestion = 1;

			DriveSection driveSection9 = new DriveSection();

			// bear left - 1
			driveSection9.time = new DateTime(2003, 7, 2, 17, 10, 0);
			driveSection9.name = "2A Byres Road";
			driveSection9.distance = 500;
			driveSection9.turnCount = 1;
			driveSection9.turnDirection = TurnDirection.Left;
			driveSection9.turnAngle = TurnAngle.Bear;
			driveSection9.roundabout = false;
			driveSection9.throughRoute = false;
			driveSection9.cost = 30;
			driveSection9.links = new ITNLink[1];
			driveSection9.links[0] = new ITNLink();
			driveSection9.links[0].TOID = "Drive TOID";
			driveSection9.links[0].congestion = 1;

			DriveSection driveSection10 = new DriveSection();

			// bear right - 1
			driveSection10.time = new DateTime(2003, 7, 2, 18, 18, 0);
			driveSection10.name = "Kings Road";
			driveSection10.distance = 5587;
			driveSection10.turnCount = 1;
			driveSection10.turnDirection = TurnDirection.Right;
			driveSection10.turnAngle = TurnAngle.Bear;
			driveSection10.roundabout = false;
			driveSection10.throughRoute = false;
			driveSection10.cost = 30;
			driveSection10.links = new ITNLink[1];
			driveSection10.links[0] = new ITNLink();
			driveSection10.links[0].TOID = "Drive TOID";
			driveSection10.links[0].congestion = 1;


			DriveSection driveSection11 = new DriveSection();

			// bear - mini roundabout left
			driveSection11.time = new DateTime(2003, 7, 2, 18, 28, 0);
			driveSection11.name = "Queens Road";
			driveSection11.distance = 87;
			driveSection11.turnCount = 3;
			driveSection11.turnDirection = TurnDirection.MiniRoundaboutLeft;
			driveSection11.turnAngle = TurnAngle.Bear;
			driveSection11.roundabout = false;
			driveSection11.throughRoute = false;
			driveSection11.cost = 30;
			driveSection11.links = new ITNLink[1];
			driveSection11.links[0] = new ITNLink();
			driveSection11.links[0].TOID = "Drive TOID";
			driveSection11.links[0].congestion = 1;

			DriveSection driveSection12 = new DriveSection();

			// bear - mini roundabout right
			driveSection12.time = new DateTime(2003, 7, 2, 18, 34, 0);
			driveSection12.name = "Prince Street";
			driveSection12.distance = 817;
			driveSection12.turnCount = 3;
			driveSection12.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection12.turnAngle = TurnAngle.Bear;
			driveSection12.roundabout = false;
			driveSection12.throughRoute = false;
			driveSection12.cost = 30;
			driveSection12.links = new ITNLink[1];
			driveSection12.links[0] = new ITNLink();
			driveSection12.links[0].TOID = "Drive TOID";
			driveSection12.links[0].congestion = 1;

			DriveSection driveSection13 = new DriveSection();
			// roundabout - exit 1
			driveSection13.time = new DateTime(2003, 7, 2, 18, 36, 0);
			driveSection13.name = "Prince Street1";
			driveSection13.distance = 142;
			driveSection13.turnCount = 1;
			driveSection13.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection13.turnAngle = TurnAngle.Bear;
			driveSection13.roundabout = true;
			driveSection13.throughRoute = false;
			driveSection13.cost = 30;
			driveSection13.links = new ITNLink[1];
			driveSection13.links[0] = new ITNLink();
			driveSection13.links[0].TOID = "Drive TOID";
			driveSection13.links[0].congestion = 1;

			DriveSection driveSection14 = new DriveSection();
			// roundabout - exit 2
			driveSection14.time = new DateTime(2003, 7, 2, 18, 37, 0);
			driveSection14.name = "Prince Street2";
			driveSection14.distance = 10;
			driveSection14.turnCount = 2;
			driveSection14.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection14.turnAngle = TurnAngle.Bear;
			driveSection14.roundabout = true;
			driveSection14.throughRoute = false;
			driveSection14.cost = 30;
			driveSection14.links = new ITNLink[1];
			driveSection14.links[0] = new ITNLink();
			driveSection14.links[0].TOID = "Drive TOID";
			driveSection14.links[0].congestion = 1;

			DriveSection driveSection15 = new DriveSection();
			// roundabout - exit 3
			driveSection15.time = new DateTime(2003, 7, 2, 18, 38, 0);
			driveSection15.name = "Prince Street3";
			driveSection15.distance = 8;
			driveSection15.turnCount = 3;
			driveSection15.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection15.turnAngle = TurnAngle.Bear;
			driveSection15.roundabout = true;
			driveSection15.throughRoute = false;
			driveSection15.cost = 30;
			driveSection15.links = new ITNLink[1];
			driveSection15.links[0] = new ITNLink();
			driveSection15.links[0].TOID = "Drive TOID";
			driveSection15.links[0].congestion = 1;

			DriveSection driveSection16 = new DriveSection();
			// roundabout - exit 4
			driveSection16.time = new DateTime(2003, 7, 2, 18, 39, 0);
			driveSection16.name = "Prince Street4";
			driveSection16.distance = 12;
			driveSection16.turnCount = 4;
			driveSection16.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection16.turnAngle = TurnAngle.Bear;
			driveSection16.roundabout = true;
			driveSection16.throughRoute = false;
			driveSection16.cost = 30;
			driveSection16.links = new ITNLink[1];
			driveSection16.links[0] = new ITNLink();
			driveSection16.links[0].TOID = "Drive TOID";
			driveSection16.links[0].congestion = 1;

			DriveSection driveSection17 = new DriveSection();
			// roundabout - exit 5
			driveSection17.time = new DateTime(2003, 7, 2, 18, 40, 0);
			driveSection17.name = "Prince Street5";
			driveSection17.distance = 25;
			driveSection17.turnCount = 5;
			driveSection17.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection17.turnAngle = TurnAngle.Bear;
			driveSection17.roundabout = true;
			driveSection17.throughRoute = false;
			driveSection17.cost = 30;
			driveSection17.links = new ITNLink[1];
			driveSection17.links[0] = new ITNLink();
			driveSection17.links[0].TOID = "Drive TOID";
			driveSection17.links[0].congestion = 1;

			DriveSection driveSection18 = new DriveSection();
			// roundabout - exit 6
			driveSection18.time = new DateTime(2003, 7, 2, 18, 46, 0);
			driveSection18.name = "Prince Street6";
			driveSection18.distance = 8;
			driveSection18.turnCount = 6;
			driveSection18.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection18.turnAngle = TurnAngle.Bear;
			driveSection18.roundabout = true;
			driveSection18.throughRoute = false;
			driveSection18.cost = 30;
			driveSection18.links = new ITNLink[1];
			driveSection18.links[0] = new ITNLink();
			driveSection18.links[0].TOID = "Drive TOID";
			driveSection18.links[0].congestion = 1;

			DriveSection driveSection19 = new DriveSection();
			// roundabout - exit 7
			driveSection19.time = new DateTime(2003, 7, 2, 18, 54, 0);
			driveSection19.name = "Prince Street7";
			driveSection19.distance = 8100;
			driveSection19.turnCount = 7;
			driveSection19.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection19.turnAngle = TurnAngle.Bear;
			driveSection19.roundabout = true;
			driveSection19.throughRoute = false;
			driveSection19.cost = 30;
			driveSection19.links = new ITNLink[1];
			driveSection19.links[0] = new ITNLink();
			driveSection19.links[0].TOID = "Drive TOID";
			driveSection19.links[0].congestion = 1;

			DriveSection driveSection20 = new DriveSection();
			// roundabout - exit 8
			driveSection20.time = new DateTime(2003, 7, 2, 18, 55, 0);
			driveSection20.name = "Prince Street8";
			driveSection20.distance = 54;
			driveSection20.turnCount = 8;
			driveSection20.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection20.turnAngle = TurnAngle.Bear;
			driveSection20.roundabout = true;
			driveSection20.throughRoute = false;
			driveSection20.cost = 30;
			driveSection20.links = new ITNLink[1];
			driveSection20.links[0] = new ITNLink();
			driveSection20.links[0].TOID = "Drive TOID";
			driveSection20.links[0].congestion = 1;

			DriveSection driveSection21 = new DriveSection();
			// roundabout - exit 9
			driveSection21.time = new DateTime(2003, 7, 2, 18, 58, 0);
			driveSection21.name = "Prince Street9";
			driveSection21.distance = 8;
			driveSection21.turnCount = 9;
			driveSection21.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection21.turnAngle = TurnAngle.Bear;
			driveSection21.roundabout = true;
			driveSection21.throughRoute = false;
			driveSection21.cost = 30;
			driveSection21.links = new ITNLink[1];
			driveSection21.links[0] = new ITNLink();
			driveSection21.links[0].TOID = "Drive TOID";
			driveSection21.links[0].congestion = 1;

			DriveSection driveSection22 = new DriveSection();
			// roundabout - exit 10
			driveSection22.time = new DateTime(2003, 7, 2, 19, 00, 0);
			driveSection22.name = "Prince Street10";
			driveSection22.distance =15;
			driveSection22.turnCount = 10;
			driveSection22.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection22.turnAngle = TurnAngle.Bear;
			driveSection22.roundabout = true;
			driveSection22.throughRoute = false;
			driveSection22.cost = 30;
			driveSection22.links = new ITNLink[1];
			driveSection22.links[0] = new ITNLink();
			driveSection22.links[0].TOID = "Drive TOID";
			driveSection22.links[0].congestion = 1;

			result.privateJourneys[0].sections = new DriveSection[6];
			result.privateJourneys[0].sections[0] = driveSection;
			result.privateJourneys[0].sections[1] = driveSection2;
			result.privateJourneys[0].sections[2] = driveSection3;
			result.privateJourneys[0].sections[3] = driveSection4;
			result.privateJourneys[0].sections[4] = driveSection5;
			result.privateJourneys[0].sections[5] = driveSection6;



			// ---------------

			// create a private journey
			result.privateJourneys[1] = new TransportDirect.JourneyPlanning.CJPInterface.PrivateJourney();

			result.privateJourneys[1].startTime = new DateTime(2003, 7, 2, 14, 1, 0);
			result.privateJourneys[1].congestion = false;
			result.privateJourneys[1].start = new StopoverSection();
			result.privateJourneys[1].start.time = new DateTime(2003, 7, 2, 14, 1, 0);
			result.privateJourneys[1].start.name = name + " Section Name";
			result.privateJourneys[1].start.node = new ITNNode();
			result.privateJourneys[1].start.node.TOID = name + " TOID";

			result.privateJourneys[1].finish = new StopoverSection();
			result.privateJourneys[1].finish.time = new DateTime(2003, 7, 2, 17, 18, 0);
			result.privateJourneys[1].finish.name = name + " Section Name";
			result.privateJourneys[1].finish.node = new ITNNode();
			result.privateJourneys[1].finish.node.TOID = name + " TOID";

			DriveSection section1 = new DriveSection();

			// Turn - simple turn - left
			section1.time = new DateTime(2003, 7, 2, 14, 1, 0);
			section1.name = "18B Buccleuch Street, G3 6SJ, Glasgow";
			section1.distance = 101;
			section1.turnCount = 1;
			section1.turnAngle = TurnAngle.Turn;
			section1.turnDirection = TurnDirection.Left;
			section1.roundabout = false;
			section1.throughRoute = false;
			section1.cost = 30;
			section1.links = new ITNLink[1];
			section1.links[0] = new ITNLink();
			section1.links[0].TOID = "Drive TOID";
			section1.links[0].congestion = 1;

			DriveSection section1a = new DriveSection();
			// Turn - simple turn - right
			section1a.time = new DateTime(2003, 7, 2, 14, 2, 0);
			section1a.name = "West Street";
			section1a.distance = 101;
			section1a.turnCount = 1;
			section1a.turnAngle = TurnAngle.Turn;
			section1a.turnDirection = TurnDirection.Right;
			section1a.roundabout = false;
			section1a.throughRoute = false;
			section1a.cost = 30;
			section1a.links = new ITNLink[1];
			section1a.links[0] = new ITNLink();
			section1a.links[0].TOID = "Drive TOID";
			section1a.links[0].congestion = 1;

			DriveSection section2 = new DriveSection();

			// Turn - immediate turn right - assumes the value in properties is 100
			section2.time = new DateTime(2003, 7, 2, 14,  4, 0);
			section2.name = "Rose Street";
			section2.distance = 99;
			section2.turnCount = 1;
			section2.turnAngle = TurnAngle.Turn;
			section2.turnDirection = TurnDirection.Right;
			section2.roundabout = false;
			section2.throughRoute = false;
			section2.cost = 30;
			section2.links = new ITNLink[1];
			section2.links[0] = new ITNLink();
			section2.links[0].TOID = "Drive TOID";
			section2.links[0].congestion = 1;

			DriveSection section2a = new DriveSection();

			// Turn - immediate turn left - assumes the value in properties is 100
			section2a.time = new DateTime(2003, 7, 2, 14,  5, 0);
			section2a.name = "Rose Street";
			section2a.distance = 99;
			section2a.turnCount = 1;
			section2a.turnAngle = TurnAngle.Turn;
			section2a.turnDirection = TurnDirection.Left;
			section2a.roundabout = false;
			section2a.throughRoute = false;
			section2a.cost = 30;
			section2a.links = new ITNLink[1];
			section2a.links[0] = new ITNLink();
			section2a.links[0].TOID = "Drive TOID";
			section2a.links[0].congestion = 1;

			DriveSection section3 = new DriveSection();

			// Turn - counted turn 2 left
			section3.time = new DateTime(2003, 7, 2, 14,  6, 0);
			section3.name = "West Graham Street";
			section3.distance = 1258;
			section3.turnCount = 2;
			section3.turnAngle = TurnAngle.Turn;
			section3.turnDirection = TurnDirection.Left;
			section3.roundabout = false;
			section3.throughRoute = false;
			section3.cost = 30;
			section3.links = new ITNLink[1];
			section3.links[0] = new ITNLink();
			section3.links[0].TOID = "Drive TOID";
			section3.links[0].congestion = 1;

			DriveSection section3a = new DriveSection();

			// Turn - counted turn 2 right
			section3a.time = new DateTime(2003, 7, 2, 14,  7, 0);
			section3a.name = "West Graham Street";
			section3a.distance = 1258;
			section3a.turnCount = 2;
			section3a.turnAngle = TurnAngle.Turn;
			section3a.turnDirection = TurnDirection.Right;
			section3a.roundabout = false;
			section3a.throughRoute = false;
			section3a.cost = 30;
			section3a.links = new ITNLink[1];
			section3a.links[0] = new ITNLink();
			section3a.links[0].TOID = "Drive TOID";
			section3a.links[0].congestion = 1;

			DriveSection section4 = new DriveSection();

			// Turn - counted turn 3 - right
			section4.time = new DateTime(2003, 7, 2, 14,  16, 0);
			section4.name = "West Road";
			section4.distance = 1524;
			section4.turnCount = 3;
			section4.turnAngle = TurnAngle.Turn;
			section4.turnDirection = TurnDirection.Right;
			section4.roundabout = false;
			section4.throughRoute = false;
			section4.cost = 30;
			section4.links = new ITNLink[1];
			section4.links[0] = new ITNLink();
			section4.links[0].TOID = "Drive TOID";
			section4.links[0].congestion = 1;

			DriveSection section4a = new DriveSection();

			// Turn - counted turn 3 - left
			section4a.time = new DateTime(2003, 7, 2, 14,  17, 0);
			section4a.name = "West Road";
			section4a.distance = 1524;
			section4a.turnCount = 3;
			section4a.turnAngle = TurnAngle.Turn;
			section4a.turnDirection = TurnDirection.Left;
			section4a.roundabout = false;
			section4a.throughRoute = false;
			section4a.cost = 30;
			section4a.links = new ITNLink[1];
			section4a.links[0] = new ITNLink();
			section4a.links[0].TOID = "Drive TOID";
			section4a.links[0].congestion = 1;

			DriveSection section5 = new DriveSection();

			// Turn - counted turn 4 - left
			section5.time = new DateTime(2003, 7, 2, 14,  22, 0);
			section5.name = "Great Western Road";
			section5.distance = 2124;
			section5.turnCount = 4;
			section5.turnAngle = TurnAngle.Turn;
			section5.turnDirection = TurnDirection.Left;
			section5.roundabout = false;
			section5.throughRoute = false;
			section5.cost = 30;
			section5.links = new ITNLink[1];
			section5.links[0] = new ITNLink();
			section5.links[0].TOID = "Drive TOID";
			section5.links[0].congestion = 1;


			DriveSection section5a = new DriveSection();

			// Turn - counted turn 4 - right
			section5a.time = new DateTime(2003, 7, 2, 14,  23, 0);
			section5a.name = "Great Western Road";
			section5a.distance = 2124;
			section5a.turnCount = 4;
			section5a.turnAngle = TurnAngle.Turn;
			section5a.turnDirection = TurnDirection.Right;
			section5a.roundabout = false;
			section5a.throughRoute = false;
			section5a.cost = 30;
			section5a.links = new ITNLink[1];
			section5a.links[0] = new ITNLink();
			section5a.links[0].TOID = "Drive TOID";
			section5a.links[0].congestion = 1;

			DriveSection section6 = new DriveSection();

			// Turn - Uncounted turn (assumes the Web.CarJourneyDetailsControl.UncountedTurnValue in properties is set to 4)
			// Right
			section6.time = new DateTime(2003, 7, 2, 16, 57, 0);
			section6.number = "M8";
			section6.distance = 15877;
			section6.turnCount = 5;
			section6.turnAngle = TurnAngle.Turn;
			section6.turnDirection = TurnDirection.Right;
			section6.roundabout = false;
			section6.throughRoute = false;
			section6.cost = 30;
			section6.links = new ITNLink[1];
			section6.links[0] = new ITNLink();
			section6.links[0].TOID = "Drive TOID";
			section6.links[0].congestion = 1;

			DriveSection section6a = new DriveSection();

			// Turn - Uncounted turn (assumes the Web.CarJourneyDetailsControl.UncountedTurnValue in properties is set to 4)
			// Left
			section6a.time = new DateTime(2003, 7, 2, 16, 58, 0);
			section6a.number = "M8";
			section6a.distance = 15877;
			section6a.turnCount = 5;
			section6a.turnAngle = TurnAngle.Turn;
			section6a.turnDirection = TurnDirection.Left;
			section6a.roundabout = false;
			section6a.throughRoute = false;
			section6a.cost = 30;
			section6a.links = new ITNLink[1];
			section6a.links[0] = new ITNLink();
			section6a.links[0].TOID = "Drive TOID";
			section6a.links[0].congestion = 1;

			DriveSection section7 = new DriveSection();

			// Turn - Left mini-roundabout
			section7.time = new DateTime(2003, 7, 2, 17, 05, 0);
			section7.name = "Green Road";
			section7.number = "A31";
			section7.distance = 57;
			section7.turnCount = 1;
			section7.turnAngle = TurnAngle.Turn;
			section7.turnDirection = TurnDirection.MiniRoundaboutLeft;
			section7.roundabout = false;
			section7.throughRoute = false;
			section7.cost = 30;
			section7.links = new ITNLink[1];
			section7.links[0] = new ITNLink();
			section7.links[0].TOID = "Drive TOID";
			section7.links[0].congestion = 1;

			DriveSection section8 = new DriveSection();

			// Turn - Right mini-roundabout
			section8.time = new DateTime(2003, 7, 2, 17, 08, 0);
			section8.name = "Oakfield Avenue";
			section8.distance = 2000;
			section8.turnCount = 1;
			section8.turnAngle = TurnAngle.Turn;
			section8.turnDirection = TurnDirection.MiniRoundaboutRight;
			section8.roundabout = false;
			section8.throughRoute = false;
			section8.cost = 30;
			section8.links = new ITNLink[1];
			section8.links[0] = new ITNLink();
			section8.links[0].TOID = "Drive TOID";
			section8.links[0].congestion = 1;

			DriveSection section9 = new DriveSection();

			// Turn - U-turn  mini-roundabout
			section9.time = new DateTime(2003, 7, 2, 17, 18, 0);
			section9.name = "Hill Street";
			section9.distance = 10;
			section9.turnCount = 1;
			section9.turnAngle = TurnAngle.Turn;
			section9.turnDirection = TurnDirection.MiniRoundaboutReturn;
			section9.roundabout = false;
			section9.throughRoute = false;
			section9.cost = 30;
			section9.links = new ITNLink[1];
			section9.links[0] = new ITNLink();
			section9.links[0].TOID = "Drive TOID";
			section9.links[0].congestion = 1;


			result.privateJourneys[1].sections = new DriveSection[6];
			result.privateJourneys[1].sections[0] = section1;
			result.privateJourneys[1].sections[1] = section1a;
			result.privateJourneys[1].sections[2] = section2;
			result.privateJourneys[1].sections[3] = section2a;
			result.privateJourneys[1].sections[4] = section3;
			result.privateJourneys[1].sections[5] = section3a;

			

			return result;
		}

		/// <summary>
		/// Create CJP result containing public and private journeys. (Used for summary line testing)
		/// </summary>
		/// <returns></returns>
		private JourneyResult CreateCJPResult2()
		{
			JourneyResult result = new JourneyResult();

			result.publicJourneys = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney[3];
			
			result.publicJourneys[0] = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney();
			result.publicJourneys[0].legs = new Leg[2];

			result.publicJourneys[0].legs[0] = new TimedLeg();
			result.publicJourneys[0].legs[0].mode = ModeType.Tram;
			result.publicJourneys[0].legs[0].validated = true;

			result.publicJourneys[0].legs[0].board = new Event();
			result.publicJourneys[0].legs[0].board.activity = ActivityType.Depart;
			result.publicJourneys[0].legs[0].board.departTime = new DateTime(2003, 8, 17, 13, 12, 9);
//			result.publicJourneys[0].legs[0].board.pass = false;
			result.publicJourneys[0].legs[0].board.stop = new Stop();
			result.publicJourneys[0].legs[0].board.stop.NaPTANID = "Board NAPTANID";
			result.publicJourneys[0].legs[0].board.stop.name = "16 Goulden Road, London, SW11 2JL";

			result.publicJourneys[0].legs[0].alight = new Event();
			result.publicJourneys[0].legs[0].alight.activity = ActivityType.Arrive;
			result.publicJourneys[0].legs[0].alight.arriveTime = new DateTime (2003, 8, 17, 13, 42, 19);
//			result.publicJourneys[0].legs[0].alight.pass = false;
			result.publicJourneys[0].legs[0].alight.stop = new Stop();
			result.publicJourneys[0].legs[0].alight.stop.NaPTANID = "Alight NAPTANID";
			result.publicJourneys[0].legs[0].alight.stop.name = "Kings Cross Station";

			result.publicJourneys[0].legs[0].services = new Service[1];
			result.publicJourneys[0].legs[0].services[0] = new Service();
			result.publicJourneys[0].legs[0].services[0].destinationBoard = "Kings Cross St. Pancras";
			result.publicJourneys[0].legs[0].services[0].operatorCode = "HJD";
			result.publicJourneys[0].legs[0].services[0].operatorName = "Greater London Buses";
			result.publicJourneys[0].legs[0].services[0].serviceNumber = "96";

			result.publicJourneys[0].legs[1] = new TimedLeg();
			result.publicJourneys[0].legs[1].mode = ModeType.Rail;
			result.publicJourneys[0].legs[1].validated = true;

			result.publicJourneys[0].legs[1].board = new Event();
			result.publicJourneys[0].legs[1].board.activity = ActivityType.Depart;
			result.publicJourneys[0].legs[1].board.departTime = new DateTime(2003, 8, 17, 13, 45, 19);
//			result.publicJourneys[0].legs[1].board.pass = false;
			result.publicJourneys[0].legs[1].board.stop = new Stop();
			result.publicJourneys[0].legs[1].board.stop.NaPTANID = "Board NAPTANID";
			result.publicJourneys[0].legs[1].board.stop.name = "Kings Cross Station";

			result.publicJourneys[0].legs[1].alight = new Event();
			result.publicJourneys[0].legs[1].alight.activity = ActivityType.Arrive;
			result.publicJourneys[0].legs[1].alight.arriveTime = new DateTime (2003, 8, 17, 14, 48, 25);
//			result.publicJourneys[0].legs[1].alight.pass = false;
			result.publicJourneys[0].legs[1].alight.stop = new Stop();
			result.publicJourneys[0].legs[1].alight.stop.NaPTANID = "Alight NAPTANID";
			result.publicJourneys[0].legs[1].alight.stop.name = "Reading Station";

			result.publicJourneys[0].legs[1].services = new Service[1];
			result.publicJourneys[0].legs[1].services[0] = new Service();
			result.publicJourneys[0].legs[1].services[0].destinationBoard = "Reading";
			result.publicJourneys[0].legs[1].services[0].operatorCode = "HJD";
			result.publicJourneys[0].legs[1].services[0].operatorName = "ThamesLink Services";
			//result.legs[0].services[0].serviceNumber = "96";

			// ----

			result.publicJourneys[1] = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney();
			result.publicJourneys[1].legs = new Leg[3];

			result.publicJourneys[1].legs[0] = new TimedLeg();
			result.publicJourneys[1].legs[0].mode = ModeType.Bus;
			result.publicJourneys[1].legs[0].validated = true;

			result.publicJourneys[1].legs[0].board = new Event();
			result.publicJourneys[1].legs[0].board.activity = ActivityType.Depart;
			result.publicJourneys[1].legs[0].board.departTime = new DateTime(2003, 9, 16, 13, 12, 9);
//			result.publicJourneys[1].legs[0].board.pass = false;
			result.publicJourneys[1].legs[0].board.stop = new Stop();
			result.publicJourneys[1].legs[0].board.stop.NaPTANID = "Board NAPTANID";
			result.publicJourneys[1].legs[0].board.stop.name = "Reading Station";

			result.publicJourneys[1].legs[0].alight = new Event();
			result.publicJourneys[1].legs[0].alight.activity = ActivityType.Arrive;
			result.publicJourneys[1].legs[0].alight.arriveTime = new DateTime (2003, 9, 16, 13, 42, 19);
//			result.publicJourneys[1].legs[0].alight.pass = false;
			result.publicJourneys[1].legs[0].alight.stop = new Stop();
			result.publicJourneys[1].legs[0].alight.stop.NaPTANID = "Alight NAPTANID";
			result.publicJourneys[1].legs[0].alight.stop.name = "Edinburgh Waverly";

			result.publicJourneys[1].legs[0].services = new Service[1];
			result.publicJourneys[1].legs[0].services[0] = new Service();
			result.publicJourneys[1].legs[0].services[0].destinationBoard = "Edinburgh Waverly";
			result.publicJourneys[1].legs[0].services[0].operatorCode = "AAA";
			result.publicJourneys[1].legs[0].services[0].operatorName = "Scottish Railway";
			result.publicJourneys[1].legs[0].services[0].serviceNumber = "S123";

			result.publicJourneys[1].legs[1] = new TimedLeg();
			result.publicJourneys[1].legs[1].mode = ModeType.Metro;
			result.publicJourneys[1].legs[1].validated = true;

			result.publicJourneys[1].legs[1].board = new Event();
			result.publicJourneys[1].legs[1].board.activity = ActivityType.Depart;
			result.publicJourneys[1].legs[1].board.departTime = new DateTime(2003, 9, 16, 13, 45, 19);
//			result.publicJourneys[1].legs[1].board.pass = false;
			result.publicJourneys[1].legs[1].board.stop = new Stop();
			result.publicJourneys[1].legs[1].board.stop.NaPTANID = "Board NAPTANID";
			result.publicJourneys[1].legs[1].board.stop.name = "Edinburgh Waverly";

			result.publicJourneys[1].legs[1].alight = new Event();
			result.publicJourneys[1].legs[1].alight.activity = ActivityType.Arrive;
			result.publicJourneys[1].legs[1].alight.arriveTime = new DateTime (2003, 9, 16, 14, 48, 25);
//			result.publicJourneys[1].legs[1].alight.pass = false;
			result.publicJourneys[1].legs[1].alight.stop = new Stop();
			result.publicJourneys[1].legs[1].alight.stop.NaPTANID = "Alight NAPTANID";
			result.publicJourneys[1].legs[1].alight.stop.name = "Edinburgh High Street";

			result.publicJourneys[1].legs[1].services = new Service[1];
			result.publicJourneys[1].legs[1].services[0] = new Service();
			result.publicJourneys[1].legs[1].services[0].destinationBoard = "Hill Street via Smiths Avenue";
			result.publicJourneys[1].legs[1].services[0].operatorCode = "AAA";
			result.publicJourneys[1].legs[1].services[0].operatorName = "Strathclyde Buses";
			result.publicJourneys[1].legs[1].services[0].serviceNumber = "59";

			//-- 

			result.publicJourneys[1].legs[2] = new TimedLeg();
			result.publicJourneys[1].legs[2].mode = ModeType.Bus;
			result.publicJourneys[1].legs[2].validated = true;

			result.publicJourneys[1].legs[2].board = new Event();
			result.publicJourneys[1].legs[2].board.activity = ActivityType.Depart;
			result.publicJourneys[1].legs[2].board.departTime = new DateTime(2003, 9, 16, 14, 55, 25);
//			result.publicJourneys[1].legs[2].board.pass = false;
			result.publicJourneys[1].legs[2].board.stop = new Stop();
			result.publicJourneys[1].legs[2].board.stop.NaPTANID = "Board NAPTANID";
			result.publicJourneys[1].legs[2].board.stop.name = "Edinburgh High Street";

			result.publicJourneys[1].legs[2].alight = new Event();
			result.publicJourneys[1].legs[2].alight.activity = ActivityType.Arrive;
			result.publicJourneys[1].legs[2].alight.arriveTime = new DateTime (2003, 9, 16, 15, 18, 25);
//			result.publicJourneys[1].legs[2].alight.pass = false;
			result.publicJourneys[1].legs[2].alight.stop = new Stop();
			result.publicJourneys[1].legs[2].alight.stop.NaPTANID = "Alight NAPTANID";
			result.publicJourneys[1].legs[2].alight.stop.name = "Glasgow";

			result.publicJourneys[1].legs[2].services = new Service[2];
			result.publicJourneys[1].legs[2].services[0] = new Service();
			result.publicJourneys[1].legs[2].services[0].destinationBoard = "Rovers Place";
			result.publicJourneys[1].legs[2].services[0].operatorCode = "AAA";
			result.publicJourneys[1].legs[2].services[0].operatorName = "Strathclyde Buses";
			result.publicJourneys[1].legs[2].services[0].serviceNumber = "99";

			result.publicJourneys[1].legs[2].services[1] = new Service();
			result.publicJourneys[1].legs[2].services[1].destinationBoard = "Beckett Avenue";
			result.publicJourneys[1].legs[2].services[1].operatorCode = "AAA";
			result.publicJourneys[1].legs[2].services[1].operatorName = "Strathclyde Buses";
			result.publicJourneys[1].legs[2].services[1].serviceNumber = "69";

			// ----------

			result.publicJourneys[2] = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney();
			result.publicJourneys[2].legs = new Leg[1];
			result.publicJourneys[2].legs[0] = new TimedLeg();
			result.publicJourneys[2].legs[0].mode = ModeType.Ferry;
			result.publicJourneys[2].legs[0].validated = true;

			result.publicJourneys[2].legs[0].board = new Event();
			result.publicJourneys[2].legs[0].board.activity = ActivityType.Depart;
			result.publicJourneys[2].legs[0].board.departTime = new DateTime(2003, 8, 29, 13, 12, 9);
//			result.publicJourneys[2].legs[0].board.pass = false;
			result.publicJourneys[2].legs[0].board.stop = new Stop();
			result.publicJourneys[2].legs[0].board.stop.NaPTANID = "Board NAPTANID";
			result.publicJourneys[2].legs[0].board.stop.name = "Board name";

			result.publicJourneys[2].legs[0].alight = new Event();
			result.publicJourneys[2].legs[0].alight.activity = ActivityType.Arrive;
			result.publicJourneys[2].legs[0].alight.arriveTime = new DateTime (2003, 8, 29, 13, 42, 19);
//			result.publicJourneys[2].legs[0].alight.pass = false;
			result.publicJourneys[2].legs[0].alight.stop = new Stop();
			result.publicJourneys[2].legs[0].alight.stop.NaPTANID = "Alight NAPTANID";
			result.publicJourneys[2].legs[0].alight.stop.name = "Alight name";

			result.publicJourneys[2].legs[0].services = new Service[1];
			result.publicJourneys[2].legs[0].services[0] = new Service();
			result.publicJourneys[2].legs[0].services[0].destinationBoard = "South Beach";
			result.publicJourneys[2].legs[0].services[0].operatorCode = "AAA";
			result.publicJourneys[2].legs[0].services[0].operatorName = "Glasgow Ferry Services";
			result.publicJourneys[2].legs[0].services[0].serviceNumber = "6A";



			
			// create private journeys
			result.privateJourneys = new TransportDirect.JourneyPlanning.CJPInterface.PrivateJourney[2];
			result.privateJourneys[0] = new TransportDirect.JourneyPlanning.CJPInterface.PrivateJourney();

			string name = "name";

			result.privateJourneys[0].startTime = new DateTime(2003, 7, 2, 16, 0, 0);
			result.privateJourneys[0].congestion = true;
			result.privateJourneys[0].start = new StopoverSection();
			result.privateJourneys[0].start.time = new DateTime(2003, 7, 2, 16, 0, 0);
			result.privateJourneys[0].start.name = name + " Section Name";
			result.privateJourneys[0].start.node = new ITNNode();
			result.privateJourneys[0].start.node.TOID = name + " TOID";

			result.privateJourneys[0].finish = new StopoverSection();
			result.privateJourneys[0].finish.time = new DateTime(2003, 7, 2, 19, 00, 0);;
			result.privateJourneys[0].finish.name = name + " Section Name";
			result.privateJourneys[0].finish.node = new ITNNode();
			result.privateJourneys[0].finish.node.TOID = name + " TOID";

			DriveSection driveSection = new DriveSection();

			// roundabout section
			driveSection.time = new DateTime(2003, 7, 2, 16, 0, 0);
			driveSection.name = "Grove Road";
			driveSection.distance = 1230;
			driveSection.turnCount = 3;
			driveSection.turnDirection = TurnDirection.Continue;
			driveSection.turnAngle = TurnAngle.Continue;
			driveSection.roundabout = true;
			driveSection.throughRoute = false;
			driveSection.cost = 30;
			driveSection.links = new ITNLink[1];
			driveSection.links[0] = new ITNLink();
			driveSection.links[0].TOID = "Drive TOID";
			driveSection.links[0].congestion = 1;

			DriveSection driveSection2 = new DriveSection();

			// through route section
			driveSection2.time = new DateTime(2003, 7, 2, 16, 25, 0);
			driveSection2.name = "St. Pauls Avenue";
			driveSection2.number = "A23";
			driveSection2.distance = 333;
			driveSection2.turnCount = 3;
			driveSection2.turnDirection = TurnDirection.Left;
			driveSection2.turnAngle = TurnAngle.Bear;
			driveSection2.roundabout = false;
			driveSection2.throughRoute = true;
			driveSection2.cost = 30;
			driveSection2.links = new ITNLink[1];
			driveSection2.links[0] = new ITNLink();
			driveSection2.links[0].TOID = "Drive TOID";
			driveSection2.links[0].congestion = 1;

			DriveSection driveSection3 = new DriveSection();

			// continue - continue section
			driveSection3.time = new DateTime(2003, 7, 2, 16, 34, 0);
			driveSection3.name = "Walm Lane";
			driveSection3.distance = 1524;
			driveSection3.turnCount = 3;
			driveSection3.turnDirection = TurnDirection.Continue;
			driveSection3.turnAngle = TurnAngle.Continue;
			driveSection3.roundabout = false;
			driveSection3.throughRoute = false;
			driveSection3.cost = 30;
			driveSection3.links = new ITNLink[1];
			driveSection3.links[0] = new ITNLink();
			driveSection3.links[0].TOID = "Drive TOID";
			driveSection3.links[0].congestion = 1;

			DriveSection driveSection4 = new DriveSection();

			// continue - left
			driveSection4.time = new DateTime(2003, 7, 2, 16, 50, 0);
			driveSection4.number = "M8";
			driveSection4.distance = 3000;
			driveSection4.turnCount = 3;
			driveSection4.turnDirection = TurnDirection.Left;
			driveSection4.turnAngle = TurnAngle.Continue;
			driveSection4.roundabout = false;
			driveSection4.throughRoute = false;
			driveSection4.cost = 30;
			driveSection4.links = new ITNLink[1];
			driveSection4.links[0] = new ITNLink();
			driveSection4.links[0].TOID = "Drive TOID";
			driveSection4.links[0].congestion = 1;

			
			DriveSection driveSection5 = new DriveSection();

			// continue - right
			driveSection5.time = new DateTime(2003, 7, 2, 16, 55, 0);
			driveSection5.name = "123 Smith Street";
			driveSection5.number = "A3";
			driveSection5.distance = 50;
			driveSection5.turnCount = 3;
			driveSection5.turnDirection = TurnDirection.Right;
			driveSection5.turnAngle = TurnAngle.Continue;
			driveSection5.roundabout = false;
			driveSection5.throughRoute = false;
			driveSection5.cost = 30;
			driveSection5.links = new ITNLink[1];
			driveSection5.links[0] = new ITNLink();
			driveSection5.links[0].TOID = "Drive TOID";
			driveSection5.links[0].congestion = 1;


			DriveSection driveSection6 = new DriveSection();

			// continue - mini roundabout continue
			driveSection6.time = new DateTime(2003, 7, 2, 16, 57, 0);
			driveSection6.name = "123 Smith Avenue";
			driveSection6.distance = 502;
			driveSection6.turnCount = 3;
			driveSection6.turnDirection = TurnDirection.MiniRoundaboutContinue;
			driveSection6.turnAngle = TurnAngle.Continue;
			driveSection6.roundabout = false;
			driveSection6.throughRoute = false;
			driveSection6.cost = 30;
			driveSection6.links = new ITNLink[1];
			driveSection6.links[0] = new ITNLink();
			driveSection6.links[0].TOID = "Drive TOID";
			driveSection6.links[0].congestion = 1;

			DriveSection driveSection7 = new DriveSection();

			// continue - mini roundabout left
			driveSection7.time = new DateTime(2003, 7, 2, 17, 00, 0);
			driveSection7.name = "6 HorseFerry Road";
			driveSection7.distance = 90;
			driveSection7.turnCount = 3;
			driveSection7.turnDirection = TurnDirection.MiniRoundaboutLeft;
			driveSection7.turnAngle = TurnAngle.Continue;
			driveSection7.roundabout = false;
			driveSection7.throughRoute = false;
			driveSection7.cost = 30;
			driveSection7.links = new ITNLink[1];
			driveSection7.links[0] = new ITNLink();
			driveSection7.links[0].TOID = "Drive TOID";
			driveSection7.links[0].congestion = 1;

			DriveSection driveSection8 = new DriveSection();

			// continue - mini roundabout right
			driveSection8.time = new DateTime(2003, 7, 2, 17, 05, 0);
			driveSection8.name = "3 Great Western Road";
			driveSection8.distance = 910;
			driveSection8.turnCount = 3;
			driveSection8.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection8.turnAngle = TurnAngle.Continue;
			driveSection8.roundabout = false;
			driveSection8.throughRoute = false;
			driveSection8.cost = 30;
			driveSection8.links = new ITNLink[1];
			driveSection8.links[0] = new ITNLink();
			driveSection8.links[0].TOID = "Drive TOID";
			driveSection8.links[0].congestion = 1;

			DriveSection driveSection9 = new DriveSection();

			// bear left - 1 (simple)
			driveSection9.time = new DateTime(2003, 7, 2, 17, 10, 0);
			driveSection9.name = "2A Byres Road";
			driveSection9.distance = 500;
			driveSection9.turnCount = 1;
			driveSection9.turnDirection = TurnDirection.Left;
			driveSection9.turnAngle = TurnAngle.Bear;
			driveSection9.roundabout = false;
			driveSection9.throughRoute = false;
			driveSection9.cost = 30;
			driveSection9.links = new ITNLink[1];
			driveSection9.links[0] = new ITNLink();
			driveSection9.links[0].TOID = "Drive TOID";
			driveSection9.links[0].congestion = 1;

			DriveSection driveSection10 = new DriveSection();

			// bear right - 1 (simple)
			driveSection10.time = new DateTime(2003, 7, 2, 18, 18, 0);
			driveSection10.name = "Kings Road";
			driveSection10.distance = 5587;
			driveSection10.turnCount = 1;
			driveSection10.turnDirection = TurnDirection.Right;
			driveSection10.turnAngle = TurnAngle.Bear;
			driveSection10.roundabout = false;
			driveSection10.throughRoute = false;
			driveSection10.cost = 30;
			driveSection10.links = new ITNLink[1];
			driveSection10.links[0] = new ITNLink();
			driveSection10.links[0].TOID = "Drive TOID";
			driveSection10.links[0].congestion = 1;


			DriveSection driveSection11 = new DriveSection();

			// bear - mini roundabout left
			driveSection11.time = new DateTime(2003, 7, 2, 18, 28, 0);
			driveSection11.name = "Queens Road";
			driveSection11.distance = 87;
			driveSection11.turnCount = 3;
			driveSection11.turnDirection = TurnDirection.MiniRoundaboutLeft;
			driveSection11.turnAngle = TurnAngle.Bear;
			driveSection11.roundabout = false;
			driveSection11.throughRoute = false;
			driveSection11.cost = 30;
			driveSection11.links = new ITNLink[1];
			driveSection11.links[0] = new ITNLink();
			driveSection11.links[0].TOID = "Drive TOID";
			driveSection11.links[0].congestion = 1;

			DriveSection driveSection12 = new DriveSection();

			// bear - mini roundabout right
			driveSection12.time = new DateTime(2003, 7, 2, 18, 34, 0);
			driveSection12.name = "Prince Street";
			driveSection12.distance = 817;
			driveSection12.turnCount = 3;
			driveSection12.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection12.turnAngle = TurnAngle.Bear;
			driveSection12.roundabout = false;
			driveSection12.throughRoute = false;
			driveSection12.cost = 30;
			driveSection12.links = new ITNLink[1];
			driveSection12.links[0] = new ITNLink();
			driveSection12.links[0].TOID = "Drive TOID";
			driveSection12.links[0].congestion = 1;

			DriveSection driveSection13 = new DriveSection();
			// roundabout - exit 1
			driveSection13.time = new DateTime(2003, 7, 2, 18, 36, 0);
			driveSection13.name = "Prince Street1";
			driveSection13.distance = 142;
			driveSection13.turnCount = 1;
			driveSection13.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection13.turnAngle = TurnAngle.Bear;
			driveSection13.roundabout = true;
			driveSection13.throughRoute = false;
			driveSection13.cost = 30;
			driveSection13.links = new ITNLink[1];
			driveSection13.links[0] = new ITNLink();
			driveSection13.links[0].TOID = "Drive TOID";
			driveSection13.links[0].congestion = 1;

			DriveSection driveSection14 = new DriveSection();
			// roundabout - exit 2
			driveSection14.time = new DateTime(2003, 7, 2, 18, 37, 0);
			driveSection14.name = "Prince Street2";
			driveSection14.distance = 10;
			driveSection14.turnCount = 2;
			driveSection14.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection14.turnAngle = TurnAngle.Bear;
			driveSection14.roundabout = true;
			driveSection14.throughRoute = false;
			driveSection14.cost = 30;
			driveSection14.links = new ITNLink[1];
			driveSection14.links[0] = new ITNLink();
			driveSection14.links[0].TOID = "Drive TOID";
			driveSection14.links[0].congestion = 1;

			DriveSection driveSection15 = new DriveSection();
			// roundabout - exit 3
			driveSection15.time = new DateTime(2003, 7, 2, 18, 38, 0);
			driveSection15.name = "Prince Street3";
			driveSection15.distance = 8;
			driveSection15.turnCount = 3;
			driveSection15.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection15.turnAngle = TurnAngle.Bear;
			driveSection15.roundabout = true;
			driveSection15.throughRoute = false;
			driveSection15.cost = 30;
			driveSection15.links = new ITNLink[1];
			driveSection15.links[0] = new ITNLink();
			driveSection15.links[0].TOID = "Drive TOID";
			driveSection15.links[0].congestion = 1;

			DriveSection driveSection16 = new DriveSection();
			// roundabout - exit 4
			driveSection16.time = new DateTime(2003, 7, 2, 18, 39, 0);
			driveSection16.name = "Prince Street4";
			driveSection16.distance = 12;
			driveSection16.turnCount = 4;
			driveSection16.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection16.turnAngle = TurnAngle.Bear;
			driveSection16.roundabout = true;
			driveSection16.throughRoute = false;
			driveSection16.cost = 30;
			driveSection16.links = new ITNLink[1];
			driveSection16.links[0] = new ITNLink();
			driveSection16.links[0].TOID = "Drive TOID";
			driveSection16.links[0].congestion = 1;

			DriveSection driveSection17 = new DriveSection();
			// roundabout - exit 5
			driveSection17.time = new DateTime(2003, 7, 2, 18, 40, 0);
			driveSection17.name = "Prince Street5";
			driveSection17.distance = 25;
			driveSection17.turnCount = 5;
			driveSection17.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection17.turnAngle = TurnAngle.Bear;
			driveSection17.roundabout = true;
			driveSection17.throughRoute = false;
			driveSection17.cost = 30;
			driveSection17.links = new ITNLink[1];
			driveSection17.links[0] = new ITNLink();
			driveSection17.links[0].TOID = "Drive TOID";
			driveSection17.links[0].congestion = 1;

			DriveSection driveSection18 = new DriveSection();
			// roundabout - exit 6
			driveSection18.time = new DateTime(2003, 7, 2, 18, 46, 0);
			driveSection18.name = "Prince Street6";
			driveSection18.distance = 8;
			driveSection18.turnCount = 6;
			driveSection18.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection18.turnAngle = TurnAngle.Bear;
			driveSection18.roundabout = true;
			driveSection18.throughRoute = false;
			driveSection18.cost = 30;
			driveSection18.links = new ITNLink[1];
			driveSection18.links[0] = new ITNLink();
			driveSection18.links[0].TOID = "Drive TOID";
			driveSection18.links[0].congestion = 1;

			DriveSection driveSection19 = new DriveSection();
			// roundabout - exit 7
			driveSection19.time = new DateTime(2003, 7, 2, 18, 54, 0);
			driveSection19.name = "Prince Street7";
			driveSection19.distance = 8100;
			driveSection19.turnCount = 7;
			driveSection19.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection19.turnAngle = TurnAngle.Bear;
			driveSection19.roundabout = true;
			driveSection19.throughRoute = false;
			driveSection19.cost = 30;
			driveSection19.links = new ITNLink[1];
			driveSection19.links[0] = new ITNLink();
			driveSection19.links[0].TOID = "Drive TOID";
			driveSection19.links[0].congestion = 1;

			DriveSection driveSection20 = new DriveSection();
			// roundabout - exit 8
			driveSection20.time = new DateTime(2003, 7, 2, 18, 55, 0);
			driveSection20.name = "Prince Street8";
			driveSection20.distance = 54;
			driveSection20.turnCount = 8;
			driveSection20.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection20.turnAngle = TurnAngle.Bear;
			driveSection20.roundabout = true;
			driveSection20.throughRoute = false;
			driveSection20.cost = 30;
			driveSection20.links = new ITNLink[1];
			driveSection20.links[0] = new ITNLink();
			driveSection20.links[0].TOID = "Drive TOID";
			driveSection20.links[0].congestion = 1;

			DriveSection driveSection21 = new DriveSection();
			// roundabout - exit 9
			driveSection21.time = new DateTime(2003, 7, 2, 18, 58, 0);
			driveSection21.name = "Prince Street9";
			driveSection21.distance = 8;
			driveSection21.turnCount = 9;
			driveSection21.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection21.turnAngle = TurnAngle.Bear;
			driveSection21.roundabout = true;
			driveSection21.throughRoute = false;
			driveSection21.cost = 30;
			driveSection21.links = new ITNLink[1];
			driveSection21.links[0] = new ITNLink();
			driveSection21.links[0].TOID = "Drive TOID";
			driveSection21.links[0].congestion = 1;

			DriveSection driveSection22 = new DriveSection();
			// roundabout - exit 10
			driveSection22.time = new DateTime(2003, 7, 2, 19, 00, 0);
			driveSection22.name = "Prince Street10";
			driveSection22.distance =15;
			driveSection22.turnCount = 10;
			driveSection22.turnDirection = TurnDirection.MiniRoundaboutRight;
			driveSection22.turnAngle = TurnAngle.Bear;
			driveSection22.roundabout = true;
			driveSection22.throughRoute = false;
			driveSection22.cost = 30;
			driveSection22.links = new ITNLink[1];
			driveSection22.links[0] = new ITNLink();
			driveSection22.links[0].TOID = "Drive TOID";
			driveSection22.links[0].congestion = 1;

			DriveSection driveSection23 = new DriveSection();
			// bear left - 2
			driveSection23.time = new DateTime(2003, 7, 2, 19, 01, 0);
			driveSection23.name = "Prince Street11";
			driveSection23.distance = 112;
			driveSection23.turnCount = 2;
			driveSection23.turnDirection = TurnDirection.Left;
			driveSection23.turnAngle = TurnAngle.Bear;
			driveSection23.roundabout = false;
			driveSection23.throughRoute = false;
			driveSection23.cost = 30;
			driveSection23.links = new ITNLink[1];
			driveSection23.links[0] = new ITNLink();
			driveSection23.links[0].TOID = "Drive TOID";
			driveSection23.links[0].congestion = 1;

			DriveSection driveSection24 = new DriveSection();
			// bear left - 3
			driveSection24.time = new DateTime(2003, 7, 2, 19, 05, 0);
			driveSection24.name = "Prince Street12";
			driveSection24.distance = 5;
			driveSection24.turnCount = 3;
			driveSection24.turnDirection = TurnDirection.Left;
			driveSection24.turnAngle = TurnAngle.Bear;
			driveSection24.roundabout = false;
			driveSection24.throughRoute = false;
			driveSection24.cost = 30;
			driveSection24.links = new ITNLink[1];
			driveSection24.links[0] = new ITNLink();
			driveSection24.links[0].TOID = "Drive TOID";
			driveSection24.links[0].congestion = 1;

			DriveSection driveSection25 = new DriveSection();
			// bear left - 4
			driveSection25.time = new DateTime(2003, 7, 2, 19, 10, 0);
			driveSection25.name = "Prince Street13";
			driveSection25.distance = 15;
			driveSection25.turnCount = 4;
			driveSection25.turnDirection = TurnDirection.Left;
			driveSection25.turnAngle = TurnAngle.Bear;
			driveSection25.roundabout = false;
			driveSection25.throughRoute = false;
			driveSection25.cost = 30;
			driveSection25.links = new ITNLink[1];
			driveSection25.links[0] = new ITNLink();
			driveSection25.links[0].TOID = "Drive TOID";
			driveSection25.links[0].congestion = 1;

			DriveSection driveSection26 = new DriveSection();
			// bear left > 4
			driveSection26.time = new DateTime(2003, 7, 2, 19, 11, 0);
			driveSection26.name = "Prince Street14";
			driveSection26.distance = 15;
			driveSection26.turnCount = 5;
			driveSection26.turnDirection = TurnDirection.Left;
			driveSection26.turnAngle = TurnAngle.Bear;
			driveSection26.roundabout = false;
			driveSection26.throughRoute = false;
			driveSection26.cost = 30;
			driveSection26.links = new ITNLink[1];
			driveSection26.links[0] = new ITNLink();
			driveSection26.links[0].TOID = "Drive TOID";
			driveSection26.links[0].congestion = 1;

			DriveSection driveSection27 = new DriveSection();
			// bear right - 2
			driveSection27.time = new DateTime(2003, 7, 2, 19, 15, 0);
			driveSection27.name = "Prince Street15";
			driveSection27.distance = 15;
			driveSection27.turnCount = 2;
			driveSection27.turnDirection = TurnDirection.Right;
			driveSection27.turnAngle = TurnAngle.Bear;
			driveSection27.roundabout = false;
			driveSection27.throughRoute = false;
			driveSection27.cost = 30;
			driveSection27.links = new ITNLink[1];
			driveSection27.links[0] = new ITNLink();
			driveSection27.links[0].TOID = "Drive TOID";
			driveSection27.links[0].congestion = 1;

			DriveSection driveSection28 = new DriveSection();
			// bear right - 3
			driveSection28.time = new DateTime(2003, 7, 2, 19, 20, 0);
			driveSection28.name = "Prince Street16";
			driveSection28.distance = 15;
			driveSection28.turnCount = 3;
			driveSection28.turnDirection = TurnDirection.Right;
			driveSection28.turnAngle = TurnAngle.Bear;
			driveSection28.roundabout = false;
			driveSection28.throughRoute = false;
			driveSection28.cost = 30;
			driveSection28.links = new ITNLink[1];
			driveSection28.links[0] = new ITNLink();
			driveSection28.links[0].TOID = "Drive TOID";
			driveSection28.links[0].congestion = 1;

			DriveSection driveSection29 = new DriveSection();
			// bear right - 4
			driveSection29.time = new DateTime(2003, 7, 2, 19, 40, 0);
			driveSection29.name = "Prince Street17";
			driveSection29.distance = 15;
			driveSection29.turnCount = 4;
			driveSection29.turnDirection = TurnDirection.Right;
			driveSection29.turnAngle = TurnAngle.Bear;
			driveSection29.roundabout = false;
			driveSection29.throughRoute = false;
			driveSection29.cost = 30;
			driveSection29.links = new ITNLink[1];
			driveSection29.links[0] = new ITNLink();
			driveSection29.links[0].TOID = "Drive TOID";
			driveSection29.links[0].congestion = 1;

			DriveSection driveSection30 = new DriveSection();
			// bear right > 4
			driveSection30.time = new DateTime(2003, 7, 2, 19, 45, 0);
			driveSection30.name = "Prince Street18";
			driveSection30.distance = 15;
			driveSection30.turnCount = 5;
			driveSection30.turnDirection = TurnDirection.Right;
			driveSection30.turnAngle = TurnAngle.Bear;
			driveSection30.roundabout = false;
			driveSection30.throughRoute = false;
			driveSection30.cost = 30;
			driveSection30.links = new ITNLink[1];
			driveSection30.links[0] = new ITNLink();
			driveSection30.links[0].TOID = "Drive TOID";
			driveSection30.links[0].congestion = 1;

			DriveSection driveSection31 = new DriveSection();
			// bear right - 1 (immediate case)
			driveSection31.time = new DateTime(2003, 7, 2, 19, 50, 0);
			driveSection31.name = "Prince Street19";
			driveSection31.distance = 15;
			driveSection31.turnCount = 1;
			driveSection31.turnDirection = TurnDirection.Right;
			driveSection31.turnAngle = TurnAngle.Bear;
			driveSection31.roundabout = false;
			driveSection31.throughRoute = false;
			driveSection31.cost = 30;
			driveSection31.links = new ITNLink[1];
			driveSection31.links[0] = new ITNLink();
			driveSection31.links[0].TOID = "Drive TOID";
			driveSection31.links[0].congestion = 1;

			DriveSection driveSection32 = new DriveSection();
			// bear left - 1 (immediate case)
			driveSection32.time = new DateTime(2003, 7, 2, 19, 55, 0);
			driveSection32.name = "Prince Street20";
			driveSection32.distance = 15;
			driveSection32.turnCount = 1;
			driveSection32.turnDirection = TurnDirection.Right;
			driveSection32.turnAngle = TurnAngle.Bear;
			driveSection32.roundabout = false;
			driveSection32.throughRoute = false;
			driveSection32.cost = 30;
			driveSection32.links = new ITNLink[1];
			driveSection32.links[0] = new ITNLink();
			driveSection32.links[0].TOID = "Drive TOID";
			driveSection32.links[0].congestion = 1;

			result.privateJourneys[0].sections = new DriveSection[32];
			result.privateJourneys[0].sections[0] = driveSection;
			result.privateJourneys[0].sections[1] = driveSection2;
			result.privateJourneys[0].sections[2] = driveSection3;
			result.privateJourneys[0].sections[3] = driveSection4;
			result.privateJourneys[0].sections[4] = driveSection5;
			result.privateJourneys[0].sections[5] = driveSection6;
			result.privateJourneys[0].sections[6] = driveSection7;
			result.privateJourneys[0].sections[7] = driveSection8;
			result.privateJourneys[0].sections[8] = driveSection9;
			result.privateJourneys[0].sections[9] = driveSection10;
			result.privateJourneys[0].sections[10] = driveSection11;
			result.privateJourneys[0].sections[11] = driveSection12;
			result.privateJourneys[0].sections[12] = driveSection13;
			result.privateJourneys[0].sections[13] = driveSection14;
			result.privateJourneys[0].sections[14] = driveSection15;
			result.privateJourneys[0].sections[15] = driveSection16;
			result.privateJourneys[0].sections[16] = driveSection17;
			result.privateJourneys[0].sections[17] = driveSection18;
			result.privateJourneys[0].sections[18] = driveSection19;
			result.privateJourneys[0].sections[19] = driveSection20;
			result.privateJourneys[0].sections[20] = driveSection21;
			result.privateJourneys[0].sections[21] = driveSection22;
			result.privateJourneys[0].sections[22] = driveSection23;
			result.privateJourneys[0].sections[23] = driveSection24;
			result.privateJourneys[0].sections[24] = driveSection25;
			result.privateJourneys[0].sections[25] = driveSection26;
			result.privateJourneys[0].sections[26] = driveSection27;
			result.privateJourneys[0].sections[27] = driveSection28;
			result.privateJourneys[0].sections[28] = driveSection29;
			result.privateJourneys[0].sections[29] = driveSection30;
			result.privateJourneys[0].sections[30] = driveSection31;
			result.privateJourneys[0].sections[31] = driveSection32;


			// ---------------

			// create a private journey
			result.privateJourneys[1] = new TransportDirect.JourneyPlanning.CJPInterface.PrivateJourney();

			result.privateJourneys[1].startTime = new DateTime(2003, 7, 2, 14, 1, 0);
			result.privateJourneys[1].congestion = false;
			result.privateJourneys[1].start = new StopoverSection();
			result.privateJourneys[1].start.time = new DateTime(2003, 7, 2, 14, 1, 0);
			result.privateJourneys[1].start.name = name + " Section Name";
			result.privateJourneys[1].start.node = new ITNNode();
			result.privateJourneys[1].start.node.TOID = name + " TOID";

			result.privateJourneys[1].finish = new StopoverSection();
			result.privateJourneys[1].finish.time = new DateTime(2003, 7, 2, 17, 18, 0);
			result.privateJourneys[1].finish.name = name + " Section Name";
			result.privateJourneys[1].finish.node = new ITNNode();
			result.privateJourneys[1].finish.node.TOID = name + " TOID";

			DriveSection section1 = new DriveSection();

			// Turn - simple turn - left
			section1.time = new DateTime(2003, 7, 2, 14, 1, 0);
			section1.name = "15B Buccleuch Street, G3 6SJ, Glasgow";
			section1.distance = 101;
			section1.turnCount = 1;
			section1.turnAngle = TurnAngle.Turn;
			section1.turnDirection = TurnDirection.Left;
			section1.roundabout = false;
			section1.throughRoute = false;
			section1.cost = 30;
			section1.links = new ITNLink[1];
			section1.links[0] = new ITNLink();
			section1.links[0].TOID = "Drive TOID";
			section1.links[0].congestion = 1;

			DriveSection section1a = new DriveSection();
			// Turn - simple turn - right
			section1a.time = new DateTime(2003, 7, 2, 14, 2, 0);
			section1a.name = "West Street";
			section1a.distance = 101;
			section1a.turnCount = 1;
			section1a.turnAngle = TurnAngle.Turn;
			section1a.turnDirection = TurnDirection.Right;
			section1a.roundabout = false;
			section1a.throughRoute = false;
			section1a.cost = 30;
			section1a.links = new ITNLink[1];
			section1a.links[0] = new ITNLink();
			section1a.links[0].TOID = "Drive TOID";
			section1a.links[0].congestion = 1;

			DriveSection section2 = new DriveSection();

			// Turn - immediate turn right - assumes the value in properties is 100
			section2.time = new DateTime(2003, 7, 2, 14,  4, 0);
			section2.name = "Rose Street";
			section2.distance = 99;
			section2.turnCount = 1;
			section2.turnAngle = TurnAngle.Turn;
			section2.turnDirection = TurnDirection.Right;
			section2.roundabout = false;
			section2.throughRoute = false;
			section2.cost = 30;
			section2.links = new ITNLink[1];
			section2.links[0] = new ITNLink();
			section2.links[0].TOID = "Drive TOID";
			section2.links[0].congestion = 1;

			DriveSection section2a = new DriveSection();

			// Turn - immediate turn left - assumes the value in properties is 100
			section2a.time = new DateTime(2003, 7, 2, 14,  5, 0);
			section2a.name = "Rose Street";
			section2a.distance = 99;
			section2a.turnCount = 1;
			section2a.turnAngle = TurnAngle.Turn;
			section2a.turnDirection = TurnDirection.Left;
			section2a.roundabout = false;
			section2a.throughRoute = false;
			section2a.cost = 30;
			section2a.links = new ITNLink[1];
			section2a.links[0] = new ITNLink();
			section2a.links[0].TOID = "Drive TOID";
			section2a.links[0].congestion = 1;

			DriveSection section3 = new DriveSection();

			// Turn - counted turn 2 left
			section3.time = new DateTime(2003, 7, 2, 14,  6, 0);
			section3.name = "West Graham Street";
			section3.distance = 1258;
			section3.turnCount = 2;
			section3.turnAngle = TurnAngle.Turn;
			section3.turnDirection = TurnDirection.Left;
			section3.roundabout = false;
			section3.throughRoute = false;
			section3.cost = 30;
			section3.links = new ITNLink[1];
			section3.links[0] = new ITNLink();
			section3.links[0].TOID = "Drive TOID";
			section3.links[0].congestion = 1;

			DriveSection section3a = new DriveSection();

			// Turn - counted turn 2 right
			section3a.time = new DateTime(2003, 7, 2, 14,  7, 0);
			section3a.name = "West Graham Street";
			section3a.distance = 1258;
			section3a.turnCount = 2;
			section3a.turnAngle = TurnAngle.Turn;
			section3a.turnDirection = TurnDirection.Right;
			section3a.roundabout = false;
			section3a.throughRoute = false;
			section3a.cost = 30;
			section3a.links = new ITNLink[1];
			section3a.links[0] = new ITNLink();
			section3a.links[0].TOID = "Drive TOID";
			section3a.links[0].congestion = 1;

			DriveSection section4 = new DriveSection();

			// Turn - counted turn 3 - right
			section4.time = new DateTime(2003, 7, 2, 14,  16, 0);
			section4.name = "West Road";
			section4.distance = 1524;
			section4.turnCount = 3;
			section4.turnAngle = TurnAngle.Turn;
			section4.turnDirection = TurnDirection.Right;
			section4.roundabout = false;
			section4.throughRoute = false;
			section4.cost = 30;
			section4.links = new ITNLink[1];
			section4.links[0] = new ITNLink();
			section4.links[0].TOID = "Drive TOID";
			section4.links[0].congestion = 1;

			DriveSection section4a = new DriveSection();

			// Turn - counted turn 3 - left
			section4a.time = new DateTime(2003, 7, 2, 14,  17, 0);
			section4a.name = "West Road";
			section4a.distance = 1524;
			section4a.turnCount = 3;
			section4a.turnAngle = TurnAngle.Turn;
			section4a.turnDirection = TurnDirection.Left;
			section4a.roundabout = false;
			section4a.throughRoute = false;
			section4a.cost = 30;
			section4a.links = new ITNLink[1];
			section4a.links[0] = new ITNLink();
			section4a.links[0].TOID = "Drive TOID";
			section4a.links[0].congestion = 1;

			DriveSection section5 = new DriveSection();

			// Turn - counted turn 4 - left
			section5.time = new DateTime(2003, 7, 2, 14,  22, 0);
			section5.name = "Great Western Road";
			section5.distance = 2124;
			section5.turnCount = 4;
			section5.turnAngle = TurnAngle.Turn;
			section5.turnDirection = TurnDirection.Left;
			section5.roundabout = false;
			section5.throughRoute = false;
			section5.cost = 30;
			section5.links = new ITNLink[1];
			section5.links[0] = new ITNLink();
			section5.links[0].TOID = "Drive TOID";
			section5.links[0].congestion = 1;


			DriveSection section5a = new DriveSection();

			// Turn - counted turn 4 - right
			section5a.time = new DateTime(2003, 7, 2, 14,  23, 0);
			section5a.name = "Great Western Road";
			section5a.distance = 2124;
			section5a.turnCount = 4;
			section5a.turnAngle = TurnAngle.Turn;
			section5a.turnDirection = TurnDirection.Right;
			section5a.roundabout = false;
			section5a.throughRoute = false;
			section5a.cost = 30;
			section5a.links = new ITNLink[1];
			section5a.links[0] = new ITNLink();
			section5a.links[0].TOID = "Drive TOID";
			section5a.links[0].congestion = 1;

			DriveSection section6 = new DriveSection();

			// Turn - Uncounted turn (assumes the Web.CarJourneyDetailsControl.UncountedTurnValue in properties is set to 4)
			// Right
			section6.time = new DateTime(2003, 7, 2, 16, 57, 0);
			section6.number = "M8";
			section6.distance = 15877;
			section6.turnCount = 5;
			section6.turnAngle = TurnAngle.Turn;
			section6.turnDirection = TurnDirection.Right;
			section6.roundabout = false;
			section6.throughRoute = false;
			section6.cost = 30;
			section6.links = new ITNLink[1];
			section6.links[0] = new ITNLink();
			section6.links[0].TOID = "Drive TOID";
			section6.links[0].congestion = 1;

			DriveSection section6a = new DriveSection();

			// Turn - Uncounted turn (assumes the Web.CarJourneyDetailsControl.UncountedTurnValue in properties is set to 4)
			// Left
			section6a.time = new DateTime(2003, 7, 2, 16, 58, 0);
			section6a.number = "M8";
			section6a.distance = 15877;
			section6a.turnCount = 5;
			section6a.turnAngle = TurnAngle.Turn;
			section6a.turnDirection = TurnDirection.Left;
			section6a.roundabout = false;
			section6a.throughRoute = false;
			section6a.cost = 30;
			section6a.links = new ITNLink[1];
			section6a.links[0] = new ITNLink();
			section6a.links[0].TOID = "Drive TOID";
			section6a.links[0].congestion = 1;

			DriveSection section7 = new DriveSection();

			// Turn - Left mini-roundabout
			section7.time = new DateTime(2003, 7, 2, 17, 05, 0);
			section7.name = "Green Road";
			section7.number = "A31";
			section7.distance = 57;
			section7.turnCount = 1;
			section7.turnAngle = TurnAngle.Turn;
			section7.turnDirection = TurnDirection.MiniRoundaboutLeft;
			section7.roundabout = false;
			section7.throughRoute = false;
			section7.cost = 30;
			section7.links = new ITNLink[1];
			section7.links[0] = new ITNLink();
			section7.links[0].TOID = "Drive TOID";
			section7.links[0].congestion = 1;

			DriveSection section8 = new DriveSection();

			// Turn - Right mini-roundabout
			section8.time = new DateTime(2003, 7, 2, 17, 08, 0);
			section8.name = "Oakfield Avenue";
			section8.distance = 2000;
			section8.turnCount = 1;
			section8.turnAngle = TurnAngle.Turn;
			section8.turnDirection = TurnDirection.MiniRoundaboutRight;
			section8.roundabout = false;
			section8.throughRoute = false;
			section8.cost = 30;
			section8.links = new ITNLink[1];
			section8.links[0] = new ITNLink();
			section8.links[0].TOID = "Drive TOID";
			section8.links[0].congestion = 1;

			DriveSection section9 = new DriveSection();

			// Turn - U-turn  mini-roundabout
			section9.time = new DateTime(2003, 7, 2, 17, 18, 0);
			section9.name = "Hill Street";
			section9.distance = 10;
			section9.turnCount = 1;
			section9.turnAngle = TurnAngle.Turn;
			section9.turnDirection = TurnDirection.MiniRoundaboutReturn;
			section9.roundabout = false;
			section9.throughRoute = false;
			section9.cost = 30;
			section9.links = new ITNLink[1];
			section9.links[0] = new ITNLink();
			section9.links[0].TOID = "Drive TOID";
			section9.links[0].congestion = 1;


			result.privateJourneys[1].sections = new DriveSection[15];
			result.privateJourneys[1].sections[0] = section1;
			result.privateJourneys[1].sections[1] = section1a;
			result.privateJourneys[1].sections[2] = section2;
			result.privateJourneys[1].sections[3] = section2a;
			result.privateJourneys[1].sections[4] = section3;
			result.privateJourneys[1].sections[5] = section3a;
			result.privateJourneys[1].sections[6] = section4;
			result.privateJourneys[1].sections[7] = section4a;
			result.privateJourneys[1].sections[8] = section5;
			result.privateJourneys[1].sections[9] = section5a;
			result.privateJourneys[1].sections[10] = section6;
			result.privateJourneys[1].sections[11] = section6a;
			result.privateJourneys[1].sections[12] = section7;
			result.privateJourneys[1].sections[13] = section8;
			result.privateJourneys[1].sections[14] = section9;
			

			return result;

		}

		//		/// <summary>
		//		/// Populates TDJourneyViewState with dummy data.
		//		/// </summary>
		//		private void PopulateTDJourneyViewState1()
		//		{
		//			// Create a dummy TDJourneyRequest
		//			// No alternate locations
		//
		//			mockSession.JourneyViewState.OriginalJourneyRequest.OriginLocation = new TDLocation();
		//			mockSession.JourneyViewState.OriginalJourneyRequest.OriginLocation.Locality = "16 Goulden Road, London, SW12 2JL";
		//			mockSession.JourneyViewState.OriginalJourneyRequest.DestinationLocation = new TDLocation();
		//			mockSession.JourneyViewState.OriginalJourneyRequest.DestinationLocation.Locality = "Edinburgh Waverly Railway station, Scotland";
		//		}

		private void PopulateTDJourneyViewState2()
		{
			// Create a dummy TDJourneyRequest
			// Alternative destination location.

			mockSession.JourneyViewState.OriginalJourneyRequest = new TDJourneyRequest();

			mockSession.JourneyViewState.OriginalJourneyRequest.OriginLocation = new TDLocation();
			mockSession.JourneyViewState.OriginalJourneyRequest.OriginLocation.Locality = "16 Goulden Road, London, SW12 2JL";
			mockSession.JourneyViewState.OriginalJourneyRequest.DestinationLocation = new TDLocation();
			mockSession.JourneyViewState.OriginalJourneyRequest.DestinationLocation.Locality = "Edinburgh Waverly Railway station, Scotland";

			mockSession.JourneyViewState.OriginalJourneyRequest.AlternateLocationsFrom = false;

			mockSession.JourneyViewState.OriginalJourneyRequest.AlternateLocations = new TDLocation[1];
			mockSession.JourneyViewState.OriginalJourneyRequest.AlternateLocations[0] = new TDLocation();
			mockSession.JourneyViewState.OriginalJourneyRequest.AlternateLocations[0].Locality = "Dunbar Central Railway station, Dunbar, Scotland";

			mockSession.JourneyViewState.OriginalJourneyRequest.OutwardDateTime = new TDDateTime[1];
			mockSession.JourneyViewState.OriginalJourneyRequest.OutwardDateTime[0] = new TDDateTime(DateTime.Now);
			
			mockSession.JourneyViewState.OriginalJourneyRequest.ReturnDateTime = new TDDateTime[1];
			mockSession.JourneyViewState.OriginalJourneyRequest.ReturnDateTime[0] = new TDDateTime(DateTime.Now.AddDays(2));

			mockSession.JourneyViewState.OriginalJourneyRequest.IsReturnRequired = true;

		}

		//		private void PopulateTDJourneyViewState3()
		//		{
		//			// Create a dummy TDJourneyRequest
		//			// Alternative origin location.
		//
		//			mockSession.JourneyViewState.OriginalJourneyRequest.OriginLocation = new TDLocation();
		//			mockSession.JourneyViewState.OriginalJourneyRequest.OriginLocation.Locality = "16 Goulden Road, London, SW12 2JL";
		//			mockSession.JourneyViewState.OriginalJourneyRequest.DestinationLocation = new TDLocation();
		//			mockSession.JourneyViewState.OriginalJourneyRequest.DestinationLocation.Locality = "Edinburgh Waverly Railway station, Scotland";
		//
		//			mockSession.JourneyViewState.OriginalJourneyRequest.AlternateLocationsFrom = true;
		//
		//			mockSession.JourneyViewState.OriginalJourneyRequest.AlternateLocations = new TDLocation[1];
		//			mockSession.JourneyViewState.OriginalJourneyRequest.AlternateLocations[0] = new TDLocation();
		//			mockSession.JourneyViewState.OriginalJourneyRequest.AlternateLocations[0].Locality = "Dunbar Central Railway station, Dunbar, Scotland";
		//
		//		}

		#endregion 
	}
}
