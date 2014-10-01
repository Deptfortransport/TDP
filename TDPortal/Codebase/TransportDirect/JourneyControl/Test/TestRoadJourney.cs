// *********************************************** 
// NAME			: TestRoadJourney.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the TestRoadJourney class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestRoadJourney.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:18   mturner
//Initial revision.
//
//   Rev 1.16   Mar 30 2006 15:27:48   mguney
//new RoadJourney was failing because of negative time. Added 5 days so that it will compensate.
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.15   Mar 30 2006 13:30:50   mguney
//Manual merge for stream0018
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.14   Mar 14 2006 10:41:10   RGriffith
//Manual merge for Stream3353
//
//   Rev 1.13   Feb 09 2006 17:49:04   jmcallister
//Project Newkirk
//
//   Rev 1.12.1.1   Mar 02 2006 17:40:44   NMoorhouse
//Extra param to hold private via location
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.12.1.0   Jan 26 2006 20:13:46   rhopkins
//Pass new properties in creator for RoadJourney
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.12   May 12 2005 11:10:34   rscott
//Changes made for NUnit testing and Code Review IR1936
//
//   Rev 1.11   Feb 07 2005 11:16:02   RScott
//Assertion changed to Assert
//
//   Rev 1.10   Nov 26 2004 13:52:00   jbroome
//DEL6.3.1. Motorway Junctions enhancements
//
//   Rev 1.9   Oct 27 2004 11:16:28   jbroome
//Removed references to Toll property of CJP DriveSection class for DEL 7 CJP interface.
//
//   Rev 1.8   Nov 15 2003 18:06:40   RPhilpott
//Leave stopover sections out of RoadJourneyDetails.
//Resolution for 189: Continuous Wait Screen
//
//   Rev 1.7   Nov 06 2003 16:27:38   PNorell
//Ensured test work properly.
//
//   Rev 1.6   Oct 15 2003 21:55:36   acaunt
//No change.
//
//   Rev 1.5   Oct 15 2003 13:30:12   PNorell
//Updates to get the correct journey time to show.
//
//   Rev 1.4   Oct 09 2003 19:57:44   RPhilpott
//Bodge to allow road journey display even when no congested journey returned.
//
//   Rev 1.3   Sep 01 2003 16:28:44   jcotton
//Updated: RouteNum
//
//   Rev 1.2   Aug 26 2003 17:14:28   kcheung
//Updated to reflect the new road journey constructor
//
//   Rev 1.1   Aug 20 2003 17:55:56   AToner
//Work in progress
using System;
using NUnit.Framework;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for TestRoadJourney.
	/// </summary>
	[TestFixture]
	public class TestRoadJourney
	{
		private RoadJourney roadJourney;
		public TestRoadJourney()
		{
		}

		[SetUp]
		public void SetUp()
		{
			// Initialise services
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init( new TestJourneyInitialisation() );
		}


		[Test]		
		public void RoadJourney()
		{
			//
			// This test constructs a PrivateJourney and with a start, a finish and two sections.
			// this private journey is then passed to the RoadJourney class.
			// The values in the PrivateJourney (and it's associated sections) should appear in the RoadJourney
			// object and it's associated RoadJourneyDetails.
			PrivateJourney pj = new PrivateJourney();
			DriveSection thisSection;
			
			pj.startTime = new DateTime(1,1,1,0,0,0);
			pj.congestion = true;

			pj.start = new StopoverSection();
			pj.start.time = new DateTime(1,1,1,0,0,0);
			pj.start.name = "Home";
			pj.start.node = new ITNNode();
			pj.start.node.TOID = "HomeToid";

			pj.finish = new StopoverSection();
			pj.finish.time = new DateTime(1,1,1,0,0,0 );
			pj.finish.name = "Work";
			pj.finish.node = new ITNNode();
			pj.finish.node.TOID = "WorkTOID";

			pj.sections = new Section[3];
			pj.sections[0] = new DriveSection();
			thisSection = pj.sections[0] as DriveSection;
			pj.sections[0].time = new DateTime(1,1,1,1,5,0);
			thisSection.name = "Drive Section 0";
			thisSection.number = "A6";
			thisSection.distance = 123;
			thisSection.turnCount = 5;
			thisSection.turnDirection = TurnDirection.Left;
			thisSection.turnAngle = TurnAngle.Turn;
			thisSection.roundabout = false;
			thisSection.throughRoute = false;
			thisSection.cost = 0;
			thisSection.heading = "Drive Section Heading";
			thisSection.links = new ITNLink[1];
			thisSection.links[0] = new ITNLink();
			thisSection.links[0].TOID = "Drive Section 0";
			thisSection.links[0].congestion = 50;
			pj.sections[0] = thisSection;

			pj.sections[1] = new StopoverSection();
			StopoverSection thisStopSection = pj.sections[1] as StopoverSection;
			thisStopSection.time = new DateTime(1,1,1,0,20,0);
			pj.sections[1] = thisStopSection;

			pj.sections[2] = new DriveSection();
			thisSection = pj.sections[2] as DriveSection;
			thisSection.time = new DateTime(1,1,1,0,15,0);
			thisSection.name = "Drive Section 1";
			thisSection.number = "A34";
			thisSection.distance = 5;
			thisSection.turnCount = 5;
			thisSection.turnDirection = TurnDirection.Right;
			thisSection.turnAngle = TurnAngle.Bear;
			thisSection.roundabout = false;
			thisSection.throughRoute = false;
			thisSection.cost = 0;
			thisSection.heading = "Drive Section Heading";
			thisSection.links = new ITNLink[1];
			thisSection.links[0] = new ITNLink();
			thisSection.links[0].TOID = "Drive Section 1";
			thisSection.links[0].congestion = 50;
			pj.sections[2] = thisSection;

			roadJourney = new RoadJourney( 0, pj, 0, pj.congestion, new TDLocation(), new TDLocation(), new TDLocation(), new DateTime(1,1,5,0,0,0), true );

			Assert.IsNotNull(roadJourney,"RoadJourney");
			Assert.IsNotNull(roadJourney.Details,"RoadJourneyDetails");

			// Assertion.AssertEquals( "StartDateTime", roadJourney.StartDateTime, new TDDateTime( pj.start.time ) );
			Assert.AreEqual(6000, roadJourney.TotalDuration,"TotalDuration");
			Assert.AreEqual(128, roadJourney.TotalDistance,"Distance");

			//
			// The details are testing in TestRoadJourneyDetail.  Just a quick test to see if two
			// RoadJourneyDetails have been created from the 2 sections.
			Assert.AreEqual(3, roadJourney.Details.Length,"Number of details");
			Assert.AreEqual("Drive Section 0", roadJourney.Details[0].RoadName,"Section 0 RoadName");
			Assert.AreEqual("Drive Section 1", roadJourney.Details[2].RoadName,"Section 1 RoadName");
		}
	}
}
