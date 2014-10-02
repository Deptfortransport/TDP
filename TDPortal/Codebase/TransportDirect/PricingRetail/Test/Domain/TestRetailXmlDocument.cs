//********************************************************************************
//NAME         : TestRetailXmlDocument.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : This file is redundant and has been replaced by the file with the same name
//				 under ../RetailXmlHandoff
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/Domain/TestRetailXmlDocument.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:28   mturner
//Initial revision.
//
//   Rev 1.2   Mar 23 2004 17:03:44   ACaunt
//This file is redundant and has been replaced by the file with the same name under ../RetailXmlHandoff
//


using System;
using System.Diagnostics;
using System.Xml;

using NUnit.Framework;

using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;


namespace TransportDirect.UserPortal.PricingRetail.RetailXmlDocument
{
	[TestFixture]
	public class TestRetailXmlDocument
	{
		private JourneyControl.PublicJourney publicJourney = null;
		private TransportDirect.JourneyPlanning.CJPInterface.PublicJourney cjpPublicJourney;
		private DateTime timeNow;

		[SetUp]
		public void Setup()
		{
			cjpPublicJourney = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney();
			timeNow = DateTime.Now;
			//
			// Create a CJPPublic journey with two legs.  One timed and one a frequency.
			cjpPublicJourney.legs = new Leg[2];
			cjpPublicJourney.legs[0] = ATimedLeg( timeNow );
			cjpPublicJourney.legs[1] = AFrequencyLeg( timeNow.AddMinutes(15));
			//
			// The frequency leg is a via point
			TDLocation via = new TDLocation();
			via.NaPTANs = new TDNaptan[1];
			via.NaPTANs[0] = new TDNaptan();
			via.NaPTANs[0].Naptan = "Freq Board NAPTANID";

			publicJourney = new JourneyControl.PublicJourney( 0, cjpPublicJourney, via, TDJourneyType.PublicOriginal, 0  );
		}

		[Test]
		public void Test1()
		{
			Itinerary itinerary = new Itinerary( publicJourney, null );
			RetailXmlDocument rxd = new RetailXmlDocument();

			XmlDocument xml = rxd.GenerateXml( itinerary, ItineraryType.Return );
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
			leg.board.pass = false;
			leg.board.stop = new Stop();
			leg.board.stop.NaPTANID = "Timed Board NAPTANID";
			leg.board.stop.name = "Timed Board name";

			leg.alight = new Event();
			leg.alight.activity = ActivityType.Arrive;
			leg.alight.arriveTime = time.AddMinutes(30);
			leg.alight.pass = false;
			leg.alight.stop = new Stop();
			leg.alight.stop.NaPTANID = "Timed Alight NAPTANID";
			leg.alight.stop.name = "Timed Alight name";

			leg.destination = new Event();
			leg.destination.activity = ActivityType.Arrive;
			leg.destination.arriveTime = time.AddMinutes(30);
			leg.destination.pass = false;
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
			leg.board.pass = false;
			leg.board.stop = new Stop();
			leg.board.stop.NaPTANID = "Freq Board NAPTANID";
			leg.board.stop.name = "Freq Board name";

			leg.alight = new Event();
			leg.alight.activity = ActivityType.Arrive;
			leg.alight.arriveTime = time.AddMinutes(30);
			leg.alight.pass = false;
			leg.alight.stop = new Stop();
			leg.alight.stop.NaPTANID = "Freq Alight NAPTANID";
			leg.alight.stop.name = "Freq Alight name";

			leg.destination = new Event();
			leg.destination.activity = ActivityType.Arrive;
			leg.destination.arriveTime = time.AddMinutes(30);
			leg.destination.pass = false;
			leg.destination.stop = new Stop();
			leg.destination.stop.NaPTANID = "Freq Destination NAPTANID";
			leg.destination.stop.name = "Freq Destination name";

			return leg;
		}
	}

}
