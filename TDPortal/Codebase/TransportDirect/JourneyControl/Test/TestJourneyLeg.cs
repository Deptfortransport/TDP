// *********************************************** 
// NAME			: TestJourneyLeg.cs
// AUTHOR		: Richard Hopkins
// DATE CREATED	: 22/03/2006
// DESCRIPTION	: Nunit tests for JourneyLeg class
//
//  These tests only cover the concrete properties/methods of the abstract class JourneyLeg.
//  Other properties/methods that are implemented in the classes that extend JourneyLeg should
//  be tested in the Nunit test classes for those subclasses.
//
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestJourneyLeg.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:14   mturner
//Initial revision.
//
//   Rev 1.0   Mar 22 2006 15:32:54   rhopkins
//Initial revision.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//

using System;
using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.ServiceDiscovery;
//using System.Collections;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Nunit tests for JourneyLeg class
	/// </summary>
	[TestFixture]
	public class TestJourneyLeg
	{
		// Assortment of legs to cover significant permutations 
		//  [O = Origin, B = Board, A = Alight, D = Destn, I = intermediate(s)] 
		private TimedLeg O_B_A_D;
		private TimedLeg OB_AD;
		private TimedLeg OB_I_AD;
		private TimedLeg O_I_B_A_I_D;
		private TimedLeg O_I_B_I_A_I_D;

		DateTime timeNow;

		public TestJourneyLeg()
		{
		}

		/// <summary>
		/// Create a single leg with board and alight events
		/// </summary>
		[SetUp]
		public void Setup()
		{
			// Initialise services
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init( new TestJourneyInitialisation() );

			timeNow = new DateTime();

			CreateLegs();
		}

		/// <summary>
		/// Create a PrivateJourneyDetail object and check the properties/methods
		/// that are implemented by JourneyLeg.
		/// </summary>
		[Test]
		public void TestPrivateJourneyLeg()
		{
			TDLocation originLocation = new TDLocation();
			originLocation.Description = "Origin Location";
			TDLocation destinationLocation = new TDLocation();
			destinationLocation.Description = "Destination Location";
			TDDateTime startTime = new TDDateTime(timeNow);
			TDDateTime endTime = new TDDateTime(timeNow.AddSeconds(5400));

			JourneyLeg journeyLeg = new PrivateJourneyDetail(ModeType.Car, originLocation, destinationLocation, startTime, endTime);

			Assert.AreEqual(ModeType.Car, journeyLeg.Mode, "Private Leg mode is incorrect");

			Assert.AreEqual("Origin Location", journeyLeg.LegStart.Location.Description, "Private LegStart location is incorrect");
			Assert.AreEqual("Destination Location", journeyLeg.LegEnd.Location.Description, "Private LegEnd location is incorrect");

			Assert.AreEqual(startTime, journeyLeg.StartTime, "Private LegStart departure time is incorrect");
			Assert.AreEqual(endTime, journeyLeg.EndTime, "Private LegEnd arrival time is incorrect");
			Assert.AreEqual(5400, journeyLeg.Duration, "Private Leg duration is incorrect");

			journeyLeg.LegStart.Location.GridReference = new OSGridReference(467888, 348766);
			journeyLeg.LegEnd.Location.GridReference = new OSGridReference(376347, 767656);
			Assert.IsFalse(journeyLeg.HasInvalidCoordinates, "Public Leg coordinates should not be invalid");

			journeyLeg.LegStart.Location.GridReference = new OSGridReference(467888, 348766);
			journeyLeg.LegEnd.Location.GridReference = new OSGridReference(0, 0);
			Assert.IsTrue(journeyLeg.HasInvalidCoordinates, "Public Leg coordinates should be invalid");

			journeyLeg.LegStart.Location.GridReference = new OSGridReference(0, 0);
			journeyLeg.LegEnd.Location.GridReference = new OSGridReference(376347, 767656);
			Assert.IsTrue(journeyLeg.HasInvalidCoordinates, "Public Leg coordinates should be invalid");
		}


		/// <summary>
		/// Create a PublicJourneyDetail object  and check the properties/methods
		/// that are implemented by JourneyLeg.
		/// </summary>
		[Test]
		public void TestPublicJourneyLeg()
		{
			TDDateTime startTime = new TDDateTime(timeNow.AddMinutes(31));
			TDDateTime endTime = new TDDateTime(timeNow.AddMinutes(59));

			JourneyLeg journeyLeg = new PublicJourneyTimedDetail(O_B_A_D, null, O_I_B_A_I_D, OB_AD);

			Assert.AreEqual(ModeType.Rail, journeyLeg.Mode, "Public Leg mode is incorrect");

			Assert.AreEqual("Board name", journeyLeg.LegStart.Location.Description, "Public LegStart location is incorrect");
			Assert.AreEqual("Alight name", journeyLeg.LegEnd.Location.Description, "Public LegEnd location is incorrect");

			Assert.AreEqual(startTime, journeyLeg.StartTime, "Public LegStart departure time is incorrect");
			Assert.AreEqual(endTime, journeyLeg.EndTime, "Public LegEnd arrival time is incorrect");
			Assert.AreEqual((59-31)*60, journeyLeg.Duration, "Public Leg duration is incorrect");

			journeyLeg.LegStart.Location.GridReference = new OSGridReference(467888, 348766);
			journeyLeg.LegEnd.Location.GridReference = new OSGridReference(376347, 767656);
			Assert.IsFalse(journeyLeg.HasInvalidCoordinates, "Public Leg coordinates should not be invalid");

			journeyLeg.LegStart.Location.GridReference = new OSGridReference(467888, 348766);
			journeyLeg.LegEnd.Location.GridReference = new OSGridReference(0, 0);
			Assert.IsTrue(journeyLeg.HasInvalidCoordinates, "Public Leg coordinates should be invalid");

			journeyLeg.LegStart.Location.GridReference = new OSGridReference(0, 0);
			journeyLeg.LegEnd.Location.GridReference = new OSGridReference(376347, 767656);
			Assert.IsTrue(journeyLeg.HasInvalidCoordinates, "Public Leg coordinates should be invalid");
		}


		/// <summary>
		/// Create PT legs
		/// </summary>
		private void CreateLegs()
		{
			Event origin = new Event();
			origin.activity = ActivityType.Depart;
			origin.departTime = timeNow;
			origin.stop = new Stop();
			origin.stop.NaPTANID = "Origin NAPTANID";
			origin.stop.name = "Origin name";
			origin.geometry = new Coordinate[1];
			origin.geometry[0] = new Coordinate();
			origin.geometry[0].easting = 11;
			origin.geometry[0].northing = 11 ;
			origin.stop.coordinate = new Coordinate();
			origin.stop.coordinate.easting = 11;
			origin.stop.coordinate.northing = 11 ;


			Event intA1 = new Event();
			intA1.activity = ActivityType.ArriveDepart;
			intA1.arriveTime = timeNow.AddMinutes(9);
			intA1.departTime = timeNow.AddMinutes(11);
			intA1.stop = new Stop();
			intA1.stop.NaPTANID = "IntA1 NAPTANID";
			intA1.stop.name = "IntA1 name";
			intA1.geometry = new Coordinate[1];
			intA1.geometry[0] = new Coordinate();
			intA1.geometry[0].easting = 22;
			intA1.geometry[0].northing = 22 ;
			intA1.stop.coordinate = new Coordinate();
			intA1.stop.coordinate.easting = 22;
			intA1.stop.coordinate.northing = 22 ;

			Event intA2 = new Event();
			intA2.activity = ActivityType.Pass;
			intA2.departTime = timeNow.AddMinutes(15);
			intA2.stop = new Stop();
			intA2.stop.NaPTANID = "IntA2 NAPTANID";
			intA2.stop.name = "IntA2 name";
			intA2.geometry = new Coordinate[1];
			intA2.geometry[0] = new Coordinate();
			intA2.geometry[0].easting = 33;
			intA2.geometry[0].northing = 33 ;
			intA2.stop.coordinate = new Coordinate();
			intA2.stop.coordinate.easting = 33;
			intA2.stop.coordinate.northing = 33 ;

			Event intA3 = new Event();
			intA3.activity = ActivityType.ArriveDepart;
			intA3.arriveTime = timeNow.AddMinutes(19);
			intA3.departTime = timeNow.AddMinutes(21);
			intA3.stop = new Stop();
			intA3.stop.NaPTANID = "IntA3 NAPTANID";
			intA3.stop.name = "IntA3 name";
			intA3.geometry = new Coordinate[1];
			intA3.geometry[0] = new Coordinate();
			intA3.geometry[0].easting = 44;
			intA3.geometry[0].northing = 44 ;
			intA3.stop.coordinate = new Coordinate();
			intA3.stop.coordinate.easting = 44;
			intA3.stop.coordinate.northing = 44;

			Event board = new Event();
			board.activity = ActivityType.Depart;
			board.arriveTime = timeNow.AddMinutes(29);
			board.departTime = timeNow.AddMinutes(31);
			board.stop = new Stop();
			board.stop.NaPTANID = "Board NAPTANID";
			board.stop.name = "Board name";
			board.geometry = new Coordinate[1];
			board.geometry[0] = new Coordinate();
			board.geometry[0].easting = 55;
			board.geometry[0].northing = 55 ;
			board.stop.coordinate = new Coordinate();
			board.stop.coordinate.easting = 55;
			board.stop.coordinate.northing = 55;

			Event intB1 = new Event();
			intB1.activity = ActivityType.ArriveDepart;
			intB1.arriveTime = timeNow.AddMinutes(39);
			intB1.departTime = timeNow.AddMinutes(41);
			intB1.stop = new Stop();
			intB1.stop.NaPTANID = "IntB1 NAPTANID";
			intB1.stop.name = "IntB1 name";
			intB1.geometry = new Coordinate[1];
			intB1.geometry[0] = new Coordinate();
			intB1.geometry[0].easting = 66;
			intB1.geometry[0].northing = 66 ;
			intB1.stop.coordinate = new Coordinate();
			intB1.stop.coordinate.easting = 66;
			intB1.stop.coordinate.northing = 66;

			Event intB2 = new Event();
			intB2.activity = ActivityType.ArriveDepart;
			intB2.arriveTime = timeNow.AddMinutes(44);
			intB2.departTime = timeNow.AddMinutes(46);
			intB2.stop = new Stop();
			intB2.stop.NaPTANID = "IntB2 NAPTANID";
			intB2.stop.name = "IntB2 name";
			intB2.geometry = new Coordinate[1];
			intB2.geometry[0] = new Coordinate();
			intB2.geometry[0].easting = 77;
			intB2.geometry[0].northing = 77 ;
			intB2.stop.coordinate = new Coordinate();
			intB2.stop.coordinate.easting = 77;
			intB2.stop.coordinate.northing = 77;

			Event intB3 = new Event();
			intB3.activity = ActivityType.Pass;
			intB3.departTime = timeNow.AddMinutes(50);
			intB3.stop = new Stop();
			intB3.stop.NaPTANID = "IntB3 NAPTANID";
			intB3.stop.name = "IntB3 name";
			intB3.geometry = new Coordinate[1];
			intB3.geometry[0] = new Coordinate();
			intB3.geometry[0].easting = 88;
			intB3.geometry[0].northing = 88 ;
			intB3.stop.coordinate = new Coordinate();
			intB3.stop.coordinate.easting = 88;
			intB3.stop.coordinate.northing = 88;

			Event alight = new Event();
			alight.activity = ActivityType.Arrive;
			alight.arriveTime = timeNow.AddMinutes(59);
			alight.departTime = timeNow.AddMinutes(61);
			alight.stop = new Stop();
			alight.stop.NaPTANID = "Alight NAPTANID";
			alight.stop.name = "Alight name";
			alight.geometry = new Coordinate[1];
			alight.geometry[0] = new Coordinate();
			alight.geometry[0].easting = 99;
			alight.geometry[0].northing = 99 ;
			alight.stop.coordinate = new Coordinate();
			alight.stop.coordinate.easting = 99;
			alight.stop.coordinate.northing = 99;

			Event intC1 = new Event();
			intC1.activity = ActivityType.ArriveDepart;
			intC1.arriveTime = timeNow.AddMinutes(79);
			intC1.departTime = timeNow.AddMinutes(81);
			intC1.stop = new Stop();
			intC1.stop.NaPTANID = "IntC1 NAPTANID";
			intC1.stop.name = "intC1 name";
			intC1.geometry = new Coordinate[1];
			intC1.geometry[0] = new Coordinate();
			intC1.geometry[0].easting = 1010;
			intC1.geometry[0].northing = 1010 ;
			intC1.stop.coordinate = new Coordinate();
			intC1.stop.coordinate.easting = 1010;
			intC1.stop.coordinate.northing = 1010;

			Event destination = new Event();
			destination.activity = ActivityType.Arrive;
			destination.arriveTime = timeNow.AddMinutes(90);
			destination.stop = new Stop();
			destination.stop.NaPTANID = "Destination NAPTANID";
			destination.stop.name = "Destination name";
			destination.geometry = new Coordinate[1];
			destination.geometry[0] = new Coordinate();
			destination.geometry[0].easting = 1111;
			destination.geometry[0].northing = 1111;
			destination.stop.coordinate = new Coordinate();
			destination.stop.coordinate.easting = 1111;
			destination.stop.coordinate.northing = 1111;

			O_B_A_D = new TimedLeg();
			O_B_A_D.mode = ModeType.Rail;
			O_B_A_D.validated = true;

			O_B_A_D.origin = origin;
			O_B_A_D.board = board;
			O_B_A_D.alight = alight;
			O_B_A_D.destination = destination;

			OB_AD = new TimedLeg();
			OB_AD.mode = ModeType.Rail;
			OB_AD.validated = true;

			OB_AD.origin = origin;
			OB_AD.board  = origin;
			OB_AD.alight = destination;
			OB_AD.destination = destination;

			OB_I_AD = new TimedLeg();
			OB_I_AD.mode = ModeType.Rail;
			OB_I_AD.validated = true;

			OB_I_AD.origin = origin;
			OB_I_AD.board  = origin;
			OB_I_AD.alight = destination;
			OB_I_AD.destination = destination;

			OB_I_AD.intermediatesB = new Event[3];
			OB_I_AD.intermediatesB[0] = intB1;
			OB_I_AD.intermediatesB[1] = intB2;
			OB_I_AD.intermediatesB[2] = intB3;

			O_I_B_A_I_D = new TimedLeg();
			O_I_B_A_I_D.mode = ModeType.Rail;
			O_I_B_A_I_D.validated = true;

			O_I_B_A_I_D.origin = origin;
			O_I_B_A_I_D.board  = board;
			O_I_B_A_I_D.alight = alight;
			O_I_B_A_I_D.destination = destination;

			O_I_B_A_I_D.intermediatesA = new Event[3];
			O_I_B_A_I_D.intermediatesA[0] = intA1;
			O_I_B_A_I_D.intermediatesA[1] = intA2;
			O_I_B_A_I_D.intermediatesA[2] = intA3;

			O_I_B_A_I_D.intermediatesC = new Event[1];
			O_I_B_A_I_D.intermediatesC[0] = intC1;

			O_I_B_I_A_I_D = new TimedLeg();
			O_I_B_I_A_I_D.mode = ModeType.Rail;
			O_I_B_I_A_I_D.validated = true;

			O_I_B_I_A_I_D.origin = origin;
			O_I_B_I_A_I_D.board  = board;
			O_I_B_I_A_I_D.alight = alight;
			O_I_B_I_A_I_D.destination = destination;

			O_I_B_I_A_I_D.intermediatesA = new Event[2];
			O_I_B_I_A_I_D.intermediatesA[0] = intA1;
			O_I_B_I_A_I_D.intermediatesA[1] = intA2;

			O_I_B_I_A_I_D.intermediatesB = new Event[3];
			O_I_B_I_A_I_D.intermediatesB[0] = intB1;
			O_I_B_I_A_I_D.intermediatesB[1] = intB2;
			O_I_B_I_A_I_D.intermediatesB[2] = intB3;

			O_I_B_I_A_I_D.intermediatesC = new Event[1];
			O_I_B_I_A_I_D.intermediatesC[0] = intC1;
		}
	}
}
