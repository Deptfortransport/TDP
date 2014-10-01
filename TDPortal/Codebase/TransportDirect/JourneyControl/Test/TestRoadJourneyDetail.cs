// *********************************************** 
// NAME			: TestRoadJourneyDetail.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the TestRoadJourneyDetail class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestRoadJourneyDetail.cs-arc  $
//
//   Rev 1.2   Sep 06 2011 11:20:34   apatel
//Updated for Real Time Information for Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.1   Sep 01 2011 10:43:30   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.0   Nov 08 2007 12:24:18   mturner
//Initial revision.
//
//   Rev 1.9   Mar 30 2006 12:17:10   build
//Automatically merged from branch for stream0018
//
//   Rev 1.8.1.0   Feb 27 2006 12:14:00   RPhilpott
//Integrated Air changes
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.8   Feb 09 2006 17:49:04   jmcallister
//Project Newkirk
//
//   Rev 1.7   Feb 07 2005 11:16:04   RScott
//Assertion changed to Assert
//
//   Rev 1.6   Nov 26 2004 13:58:14   jbroome
//Fixed minor error.
//
//   Rev 1.5   Nov 26 2004 13:52:00   jbroome
//DEL6.3.1. Motorway Junctions enhancements
//
//   Rev 1.4   Oct 27 2004 11:16:28   jbroome
//Removed references to Toll property of CJP DriveSection class for DEL 7 CJP interface.
//
//   Rev 1.3   Nov 06 2003 16:27:34   PNorell
//Ensured test work properly.
//
//   Rev 1.2   Oct 15 2003 21:55:36   acaunt
//No change.
//
//   Rev 1.1   Oct 15 2003 13:30:12   PNorell
//Updates to get the correct journey time to show.
//
//   Rev 1.0   Aug 20 2003 17:55:28   AToner
//Initial Revision

using System;
using NUnit.Framework;
using TransportDirect.JourneyPlanning.CJPInterface;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for TestRoadJourneyDetail.
	/// </summary>
	[TestFixture]
	public class TestRoadJourneyDetail
	{
		public TestRoadJourneyDetail()
		{
		}

		[SetUp]
		public void SetUp()
		{
			// Initialise services
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init( new TestJourneyInitialisation() );
		}

	
		/// <summary>
		/// Produce a RoadJourneyDetail from a DriveSection
		/// </summary>
		[Test]
		public void DriveSection()
		{
			DriveSection driveSection = new DriveSection();

			driveSection.time = new DateTime(1,1,1,0,0,0);
			driveSection.name = "Drive Section Name";
			driveSection.number = "Drive Section Number";
			driveSection.distance = 123;
			driveSection.turnCount = 3;
			driveSection.turnDirection = TurnDirection.MiniRoundaboutContinue;
			driveSection.turnAngle = TurnAngle.Continue;
			driveSection.roundabout = true;
			driveSection.throughRoute = true;
			driveSection.cost = 30;
			driveSection.heading = "Leeds";
			driveSection.links = new ITNLink[1];
			driveSection.links[0] = new ITNLink();
			driveSection.links[0].TOID = "Drive TOID";
			driveSection.links[0].congestion = 1;

			RoadJourneyDetail roadJourneyDetail = new RoadJourneyDetail( driveSection );

			Assert.AreEqual( "Drive Section Name", roadJourneyDetail.RoadName, "RoadName");
			Assert.AreEqual( "Drive Section Number", roadJourneyDetail.RoadNumber, "RoadNumber");
			Assert.AreEqual( 123, roadJourneyDetail.Distance, "Distance");
			Assert.AreEqual( 0, roadJourneyDetail.Duration, "Duration Should be 0");
			Assert.AreEqual( 3, roadJourneyDetail.TurnCount, "TurnCount");
			Assert.AreEqual( TurnDirection.MiniRoundaboutContinue, roadJourneyDetail.Direction, "TurnDirection");
			Assert.AreEqual( TurnAngle.Continue, roadJourneyDetail.Angle, "TurnAngle");
			Assert.AreEqual( true, roadJourneyDetail.Roundabout, "Roundabout");
			Assert.AreEqual( true, roadJourneyDetail.ThroughRoute, "ThroughRoute");
			Assert.IsFalse( roadJourneyDetail.CongestionLevel, "CongestionLevel");
			Assert.AreEqual( false, roadJourneyDetail.IsStopOver, "IsStopOver");
			Assert.AreEqual( "Leeds", roadJourneyDetail.PlaceName, "PlaceName");
			Assert.AreEqual( false, roadJourneyDetail.IsJunctionSection, "JunctionSection");
			Assert.AreEqual( false, roadJourneyDetail.IsSlipRoad,"SlipRoad");
			Assert.AreEqual( false, roadJourneyDetail.IsFerry,"Ferry");

		}
		/// <summary>
		/// Produce a RoadJourneyDetail from a StopoverSection
		/// </summary>
		[Test]
		public void StopOverSection()
		{
			StopoverSection stopoverSection = new StopoverSection();

			stopoverSection.time = new DateTime(1,1,1,0,0,0);
			stopoverSection.name = "Stopover Section Name";
			stopoverSection.node = new ITNNode();
			stopoverSection.node.TOID = "Stopover TOID";

			RoadJourneyDetail roadJourneyDetail = new RoadJourneyDetail( stopoverSection );

			Assert.AreEqual( "Stopover Section Name", roadJourneyDetail.RoadName, "RoadName");
			Assert.AreEqual( "Stopover TOID", roadJourneyDetail.nodeToid, "TOID");
			Assert.AreEqual( true, roadJourneyDetail.IsStopOver, "IsStopover");
			Assert.AreEqual( 0, roadJourneyDetail.Duration, "Duration Should be 0");

			Assert.IsNull( roadJourneyDetail.RoadNumber, "RoadNumber");
			Assert.AreEqual( 0, roadJourneyDetail.Distance, "Distance");
			Assert.AreEqual( 0, roadJourneyDetail.TurnCount, "TurnCount");
			Assert.AreEqual( false, roadJourneyDetail.Roundabout, "Roundabout");
			Assert.AreEqual( false, roadJourneyDetail.ThroughRoute, "ThroughRoute");
			Assert.IsFalse( roadJourneyDetail.CongestionLevel, "CongestionLevel");
			Assert.AreEqual( false, roadJourneyDetail.IsJunctionSection, "JunctionSection");
		}

		/// <summary>
		/// Produce a RoadJourneyDetail from a JunctionDriveSection
		/// </summary>
		[Test]
		public void JunctionDriveSection()
		{
			JunctionDriveSection driveSection = new JunctionDriveSection();

			driveSection.time = new DateTime(1,1,1,0,0,0);
			driveSection.name = "Junction Drive Section Name";
			driveSection.number = "Junction Drive Section Number";
			driveSection.distance = 123;
			driveSection.turnCount = 3;
			driveSection.turnDirection = TurnDirection.MiniRoundaboutContinue;
			driveSection.turnAngle = TurnAngle.Continue;
			driveSection.roundabout = true;
			driveSection.throughRoute = true;
			driveSection.cost = 30;
			driveSection.heading = "Leeds";
			driveSection.junctionNumber = "7";
			driveSection.type = JunctionType.Exit;
            driveSection.links = new ITNLink[1];
			driveSection.links[0] = new ITNLink();
			driveSection.links[0].TOID = "Drive TOID";
			driveSection.links[0].congestion = 1;

			RoadJourneyDetail roadJourneyDetail = new RoadJourneyDetail( driveSection );

			Assert.AreEqual( "Junction Drive Section Name", roadJourneyDetail.RoadName, "RoadName");
			Assert.AreEqual( "Junction Drive Section Number", roadJourneyDetail.RoadNumber, "RoadNumber");
			Assert.AreEqual( 123, roadJourneyDetail.Distance, "Distance");
			Assert.AreEqual( 0, roadJourneyDetail.Duration, "Duration Should be 0");
			Assert.AreEqual( 3, roadJourneyDetail.TurnCount, "TurnCount");
			Assert.AreEqual( TurnDirection.MiniRoundaboutContinue, roadJourneyDetail.Direction, "TurnDirection");
			Assert.AreEqual( TurnAngle.Continue, roadJourneyDetail.Angle, "TurnAngle");
			Assert.AreEqual( true, roadJourneyDetail.Roundabout, "Roundabout");
			Assert.AreEqual( true, roadJourneyDetail.ThroughRoute, "ThroughRoute");
			Assert.IsFalse( roadJourneyDetail.CongestionLevel, "CongestionLevel");
			Assert.AreEqual( false, roadJourneyDetail.IsStopOver, "IsStopOver");
			Assert.AreEqual( "Leeds", roadJourneyDetail.PlaceName, "PlaceName");
			Assert.AreEqual( true, roadJourneyDetail.IsJunctionSection, "JunctionSection");
			Assert.AreEqual( "7", roadJourneyDetail.JunctionNumber, "JunctionNumber");
			Assert.AreEqual( JunctionType.Exit, roadJourneyDetail.JunctionAction, "JunctionAction");
			Assert.AreEqual( true, roadJourneyDetail.IsSlipRoad, "SlipRoad");
			Assert.AreEqual( false, roadJourneyDetail.IsFerry, "Ferry");

		}

		/// <summary>
		/// Produce a RoadJourneyDetail from a JunctionDriveSection that contains no 
		/// junction number. Should be flagged as a junction section.
		/// </summary>
		[Test]
		public void OverrideJunctionDriveSection()
		{
			JunctionDriveSection driveSection = new JunctionDriveSection();

			driveSection.time = new DateTime(1,1,1,0,0,0);
			driveSection.name = "Junction Drive Section Name";
			driveSection.number = "Junction Drive Section Number";
			driveSection.distance = 123;
			driveSection.turnCount = 3;
			driveSection.turnDirection = TurnDirection.MiniRoundaboutContinue;
			driveSection.turnAngle = TurnAngle.Continue;
			driveSection.roundabout = true;
			driveSection.throughRoute = true;
			driveSection.cost = 30;
			driveSection.heading = "Leeds";
			driveSection.junctionNumber = "";
			driveSection.type = JunctionType.Exit;
			driveSection.links = new ITNLink[1];
			driveSection.links[0] = new ITNLink();
			driveSection.links[0].TOID = "Drive TOID";
			driveSection.links[0].congestion = 1;

			RoadJourneyDetail roadJourneyDetail = new RoadJourneyDetail( driveSection );

			Assert.AreEqual( false, roadJourneyDetail.IsJunctionSection, "JunctionSection" );
		}


		/// <summary>
		/// Produce a RoadJourneyDetail from a DriveSection that is a Ferry section
		/// </summary>
		[Test]
		public void DriveSectionFerry()
		{
			DriveSection driveSection = new DriveSection();

			driveSection.time = new DateTime(1,1,1,0,0,0);
			driveSection.name = "Drive Section Name";
			driveSection.number = "FERRY";
			driveSection.distance = 123;
			driveSection.turnCount = 3;
			driveSection.turnDirection = TurnDirection.Continue;
			driveSection.turnAngle = TurnAngle.Continue;
			driveSection.roundabout = true;
			driveSection.throughRoute = true;
			driveSection.cost = 0;
			driveSection.heading = "";
			driveSection.links = new ITNLink[1];
			driveSection.links[0] = new ITNLink();
			driveSection.links[0].TOID = "Drive TOID";
			driveSection.links[0].congestion = 1;

			RoadJourneyDetail roadJourneyDetail = new RoadJourneyDetail( driveSection );

			Assert.AreEqual( true, roadJourneyDetail.IsFerry, "Ferry");
		}

		/// <summary>
		/// Produce a RoadJourneyDetail from a DriveSection that is a slip road section
		/// </summary>
		[Test]
		public void DriveSectionSlipRoad()
		{
			DriveSection driveSection = new DriveSection();

			driveSection.time = new DateTime(1,1,1,0,0,0);
			driveSection.name = "Slip Road Drive Section Name";
			driveSection.number = "Drive Section Number";
			driveSection.distance = 123;
			driveSection.turnCount = 3;
			driveSection.turnDirection = TurnDirection.Continue;
			driveSection.turnAngle = TurnAngle.Continue;
			driveSection.roundabout = true;
			driveSection.throughRoute = true;
			driveSection.cost = 0;
			driveSection.heading = "";
			driveSection.links = new ITNLink[1];
			driveSection.links[0] = new ITNLink();
			driveSection.links[0].TOID = "Drive TOID";
			driveSection.links[0].congestion = 1;

			RoadJourneyDetail roadJourneyDetail = new RoadJourneyDetail( driveSection );

			Assert.AreEqual( true, roadJourneyDetail.IsSlipRoad, "SlipRoad" );
		}

        /// <summary>
        /// Produce a RoadJourneyDetail from a DriveSection
        /// </summary>
        [Test]
        public void DriveSectionTravelNewsIncidentIdsAndToids()
        {
            DriveSection driveSection = new DriveSection();

            driveSection.time = new DateTime(1, 1, 1, 0, 0, 0);
            driveSection.name = "Drive Section Name";
            driveSection.number = "Drive Section Number";
            driveSection.distance = 123;
            driveSection.turnCount = 3;
            driveSection.turnDirection = TurnDirection.MiniRoundaboutContinue;
            driveSection.turnAngle = TurnAngle.Continue;
            driveSection.roundabout = true;
            driveSection.throughRoute = true;
            driveSection.cost = 30;
            driveSection.heading = "Leeds";
            driveSection.links = new ITNLink[1];
            driveSection.links[0] = new ITNLink();
            driveSection.links[0].TOID = "Drive TOID";
            driveSection.links[0].congestion = 1;
            
            RoadJourneyDetail roadJourneyDetail = new RoadJourneyDetail(driveSection);
            roadJourneyDetail.TravelNewsIncidentIDs = new System.Collections.Generic.List<string>();
            roadJourneyDetail.TravelNewsIncidentIDs.Add("incidentId");
           
            Assert.AreEqual("Drive Section Name", roadJourneyDetail.RoadName, "RoadName");
            Assert.AreEqual("Drive Section Number", roadJourneyDetail.RoadNumber, "RoadNumber");
            Assert.AreEqual(123, roadJourneyDetail.Distance, "Distance");
            Assert.AreEqual(0, roadJourneyDetail.Duration, "Duration Should be 0");
            Assert.AreEqual(3, roadJourneyDetail.TurnCount, "TurnCount");
            Assert.AreEqual(TurnDirection.MiniRoundaboutContinue, roadJourneyDetail.Direction, "TurnDirection");
            Assert.AreEqual(TurnAngle.Continue, roadJourneyDetail.Angle, "TurnAngle");
            Assert.AreEqual(true, roadJourneyDetail.Roundabout, "Roundabout");
            Assert.AreEqual(true, roadJourneyDetail.ThroughRoute, "ThroughRoute");
            Assert.IsFalse(roadJourneyDetail.CongestionLevel, "CongestionLevel");
            Assert.AreEqual(false, roadJourneyDetail.IsStopOver, "IsStopOver");
            Assert.AreEqual("Leeds", roadJourneyDetail.PlaceName, "PlaceName");
            Assert.AreEqual(false, roadJourneyDetail.IsJunctionSection, "JunctionSection");
            Assert.AreEqual(false, roadJourneyDetail.IsSlipRoad, "SlipRoad");
            Assert.AreEqual(false, roadJourneyDetail.IsFerry, "Ferry");
            Assert.AreEqual(1, roadJourneyDetail.Toid.Length, "Toids");
            Assert.AreEqual(1, roadJourneyDetail.TravelNewsIncidentIDs.Count, "TravelNewsIncidents");

        }

	}
}
