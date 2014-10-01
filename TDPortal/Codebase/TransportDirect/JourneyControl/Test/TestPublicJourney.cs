// *********************************************** 
// NAME			: TestPublicJourney.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the TestPublicJourney class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestPublicJourney.cs-arc  $
//
//   Rev 1.1   Dec 05 2012 14:12:50   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Nov 08 2007 12:24:18   mturner
//Initial revision.
//
//   Rev 1.12   Mar 30 2006 12:17:10   build
//Automatically merged from branch for stream0018
//
//   Rev 1.11.1.0   Feb 27 2006 12:14:00   RPhilpott
//Integrated Air changes
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.11   Aug 24 2005 16:06:52   RPhilpott
//Make tests consistent with changed TDJourneyResult.AddResult() and PublicJourney ctor. 
//Resolution for 2662: DN062:  Map with non-consecutive nodes with no co-ordinates functioning incorrectly
//
//   Rev 1.10   Mar 23 2005 15:22:22   rhopkins
//Fixed FxCop "warnings"
//
//   Rev 1.9   Feb 23 2005 15:06:18   rscott
//DEL 7 Update - New properties added
//
//   Rev 1.8   Jan 18 2005 14:45:22   rhopkins
//Removed dependancy on other tests so that this test will work when TestCarCostCalculator is present.
//
//   Rev 1.7   Jul 28 2004 10:54:06   CHosegood
//Updated to compile against CJP 6.0.0.0
//NOT TESTED!!!
//
//   Rev 1.6   Oct 15 2003 21:55:20   acaunt
//Destinations added to the leg data
//
//   Rev 1.5   Sep 20 2003 19:24:46   RPhilpott
//Support for passing OSGR's with NaPTAN's, various other fixes
//
//   Rev 1.4   Sep 05 2003 15:29:04   passuied
//Deletion of TDLocation, LocationCodingType, OSGridReference and transfer to LocationService
//
//   Rev 1.3   Sep 01 2003 16:28:44   jcotton
//Updated: RouteNum
//
//   Rev 1.2   Aug 27 2003 09:25:58   kcheung
//Updated call to Public Journey constructor because TDJourneyType was added to Public Journey constructor.
//
//   Rev 1.1   Aug 26 2003 17:14:00   kcheung
//Updated to reflect the new public journey constructor
//
//   Rev 1.0   Aug 20 2003 17:55:28   AToner
//Initial Revision

using NUnit.Framework;

using System;

using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for TestPublicJourney.
	/// </summary>
	[TestFixture]
	public class TestPublicJourney
	{
		private TransportDirect.JourneyPlanning.CJPInterface.PublicJourney cjpPublicJourney;
		private DateTime timeNow;

		public TestPublicJourney()
		{
		}


		[SetUp]
		public void PublicJourney()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestJourneyInitialisation());

			cjpPublicJourney = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney();
			timeNow = DateTime.Now;
			//
			// Create a CJPPublic journey with two legs.  One timed and one a frequency.
			cjpPublicJourney.legs = new Leg[2];
			cjpPublicJourney.legs[0] = ATimedLeg( timeNow );
			cjpPublicJourney.legs[1] = AFrequencyLeg( timeNow.AddMinutes(15));
		}

		[Test]
		public void TwoLegs()
		{
			// very basic test of PublicJourney creation ...

			// The frequency leg is a via point
			TDLocation via = new TDLocation();
			via.NaPTANs = new TDNaptan[1];
			via.NaPTANs[0] = new TDNaptan();
			via.NaPTANs[0].Naptan = "Freq Board NAPTANID";

            PublicJourney publicJourney = new PublicJourney(0, cjpPublicJourney, via, null, null, TDJourneyType.PublicOriginal, false, 0);

			// Are two legs in the details and is the frequency leg cosidered a VIA point?
			Assert.AreEqual(2, publicJourney.Details.Length ,  "Number of details");
			Assert.AreEqual(false, publicJourney.Details[0].IncludesVia ,  "VIA not present");
			Assert.AreEqual(true, publicJourney.Details[1].IncludesVia ,  "VIA present");

			Assert.IsTrue(publicJourney.Details[0].LegStart.DepartureDateTime.GetDateTime() < publicJourney.Details[1].LegStart.DepartureDateTime.GetDateTime() ,  "Leg order");

			Assert.IsNotNull(publicJourney.JourneyDate,"journeyDate is null");
		}

		/// <summary>
		/// Create a timed leg
		/// </summary>
		/// <returns>A timed leg</returns>
		private Leg ATimedLeg( DateTime time )
		{
			Leg leg = new TimedLeg();

			leg.mode = ModeType.Tram;
			leg.validated = true;

			leg.board = new Event();
			leg.board.activity = ActivityType.Depart;
			leg.board.departTime = time;
			leg.board.stop = new Stop();
			leg.board.stop.NaPTANID = "Timed Board NAPTANID";
			leg.board.stop.name = "Timed Board name";

			leg.alight = new Event();
			leg.alight.activity = ActivityType.Arrive;
			leg.alight.arriveTime = time.AddMinutes(30);
			leg.alight.stop = new Stop();
			leg.alight.stop.NaPTANID = "Timed Alight NAPTANID";
			leg.alight.stop.name = "Timed Alight name";

			leg.destination = new Event();
			leg.destination.activity = ActivityType.Arrive;
			leg.destination.arriveTime = time.AddMinutes(30);
			leg.destination.stop = new Stop();
			leg.destination.stop.NaPTANID = "Timed Destination NAPTANID";
			leg.destination.stop.name = "Timed Destination name";

			return leg;
		}


		/// <summary>
		/// Create a frequency leg
		/// </summary>
		/// <returns>A frequency leg</returns>
		private Leg AFrequencyLeg( DateTime time )
		{
			FrequencyLeg leg = new FrequencyLeg();

			leg.mode = ModeType.Tram;
			leg.validated = true;

			leg.frequency = 1;
			leg.typicalDuration = 2;
			leg.maxDuration = 3;
			leg.minFrequency = 4;
			leg.maxFrequency = 5;

			leg.board = new Event();
			leg.board.activity = ActivityType.Depart;
			leg.board.departTime = time;
			leg.board.stop = new Stop();
			leg.board.stop.NaPTANID = "Freq Board NAPTANID";
			leg.board.stop.name = "Freq Board name";

			leg.alight = new Event();
			leg.alight.activity = ActivityType.Arrive;
			leg.alight.arriveTime = time.AddMinutes(30);
			leg.alight.stop = new Stop();
			leg.alight.stop.NaPTANID = "Freq Alight NAPTANID";
			leg.alight.stop.name = "Freq Alight name";

			leg.destination = new Event();
			leg.destination.activity = ActivityType.Arrive;
			leg.destination.arriveTime = time.AddMinutes(30);
			leg.destination.stop = new Stop();
			leg.destination.stop.NaPTANID = "Freq Destination NAPTANID";
			leg.destination.stop.name = "Freq Destination name";

			return leg;
		}
	}
}
