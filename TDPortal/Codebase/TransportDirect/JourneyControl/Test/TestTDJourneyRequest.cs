// *********************************************** 
// NAME			: TestTDJourneyRequest.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the TestTDJourneyRequest class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestTDJourneyRequest.cs-arc  $
//
//   Rev 1.2   Sep 06 2011 15:13:32   apatel
//Unit test update for real time information in car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.1   Sep 01 2011 10:43:30   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.0   Nov 08 2007 12:24:20   mturner
//Initial revision.
//
//   Rev 1.11   Nov 01 2005 15:01:50   build
//Automatically merged from branch for stream2638
//
//   Rev 1.10.1.0   Sep 15 2005 17:04:48   jbroome
//Added test for sequence property
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.10   Mar 23 2005 15:22:22   rhopkins
//Fixed FxCop "warnings"
//
//   Rev 1.9   Mar 23 2005 13:23:38   rhopkins
//...and I've changed the double to a string in the Assert so that the test succeeds.
//
//   Rev 1.8   Mar 15 2005 16:20:02   RAlavi
//Changed double to string
//
//   Rev 1.7   Feb 23 2005 16:41:22   rscott
//DEL7 Update - TDDateTime OutwardDateTime and ReturnDateTime changed to TDDateTime[] arrays.
//
//   Rev 1.6   Feb 23 2005 15:06:18   rscott
//DEL 7 Update - New properties added
//
//   Rev 1.5   Feb 01 2005 13:40:12   RScott
//DEL 7 Updated to include testing of PublicSoftViaLocations and PublicNotViaLocations
//
//   Rev 1.4   Jan 28 2005 18:29:30   ralavi
//Updated for car costing
//
//   Rev 1.3   Jan 20 2005 09:28:10   RScott
//DEL 7 - Updated to for addition of PublicViaLocations, PublicSoftViaLocations, PublicNotViaLocations.
//
//   Rev 1.2   Jan 18 2005 15:54:08   rhopkins
//Removed dependancy on other tests so that this test will work when TestCarCostCalculator is present.
//Also changed Assertion to Assert.
//
//   Rev 1.1   Sep 05 2003 15:29:06   passuied
//Deletion of TDLocation, LocationCodingType, OSGridReference and transfer to LocationService
//
//   Rev 1.0   Aug 20 2003 17:55:30   AToner
//Initial Revision
using System;
using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for TestTDJourneyRequest.
	/// </summary>
	[TestFixture]
	public class TestTDJourneyRequest
	{
		public TestTDJourneyRequest()
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
		/// Create a journey request and check the gets match the sets
		/// </summary>
		[Test]
		public void Create()
		{
			DateTime timeNow = new DateTime();
			TDJourneyRequest request = new TDJourneyRequest();

			timeNow = DateTime.Now;

			request.IsReturnRequired = true;
			request.OutwardArriveBefore = false;
			request.ReturnArriveBefore = true;
			request.OutwardDateTime = new TDDateTime[1];
			request.OutwardDateTime[0] = new TDDateTime( timeNow );
			request.ReturnDateTime = new TDDateTime[1];
			request.ReturnDateTime[0] = new TDDateTime( timeNow.AddMinutes(1) );
			request.InterchangeSpeed = 1;
			request.WalkingSpeed = 2;
			request.MaxWalkingTime = 3;
			request.DrivingSpeed = 4;
			request.AvoidMotorways = false;
			request.OriginLocation = new TDLocation();
			request.DestinationLocation = new TDLocation();

			request.PublicViaLocations = new TDLocation[1];
			request.PublicViaLocations[0] = new TDLocation();
			
			request.PublicSoftViaLocations = new TDLocation[1];
			request.PublicSoftViaLocations[0] = new TDLocation();

			request.PublicNotViaLocations = new TDLocation[1];
			request.PublicNotViaLocations[0] = new TDLocation();
			
			request.PrivateViaLocation = new TDLocation();
			request.AvoidRoads = new string[] {"A1", "A6"};
            request.AvoidToidsOutward = new string[] { "4000000009316750" };
            request.AvoidToidsReturn = new string[] { "4000000009316751" };
			request.AlternateLocations = new TDLocation[2];
			request.AlternateLocations[0] = new TDLocation();
			request.AlternateLocations[1] = new TDLocation();
			request.AlternateLocationsFrom = true;
			request.PrivateAlgorithm = PrivateAlgorithmType.MostEconomical;
			request.PublicAlgorithm = PublicAlgorithmType.Fastest;
			request.AvoidTolls = false;
			request.AvoidFerries = false;
			request.IncludeRoads = new string[] {"M25", "A1"};
			request.FuelPrice = "20.5";
			request.FuelConsumption = "10.85";

			request.RoutingPointNaptans = new string[1];
			request.RoutingPointNaptans[0] = "9100A";

			request.Sequence = 5;

            Assert.IsTrue(request.IsOutwardRequired, "IsOutwardRequired");
			Assert.IsTrue( request.IsReturnRequired, "IsReturnRequired" );
			Assert.AreEqual( false, request.OutwardArriveBefore, "OutwardArriveBefore" );
			Assert.IsTrue( request.ReturnArriveBefore, "ReturnArriveBefore" );
			Assert.AreEqual( timeNow, request.OutwardDateTime[0].GetDateTime(), "OutwardDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(1), request.ReturnDateTime[0].GetDateTime(), "ReturnDateTime");
			Assert.AreEqual( 1, request.InterchangeSpeed, "InterchangeSpeed");
			Assert.AreEqual( 2, request.WalkingSpeed, "WalkingSpeed");
			Assert.AreEqual( 3, request.MaxWalkingTime, "MaxWalkingTime");
			Assert.AreEqual( 4, request.DrivingSpeed, "DrivingSpeed");
			Assert.AreEqual( false, request.AvoidMotorways, "AvoidMotorways");
			Assert.IsNotNull( request.OriginLocation, "OriginLocation" );
			Assert.IsNotNull( request.DestinationLocation, "DestinationLocation" );

			Assert.IsNotNull( request.PublicViaLocations[0], "PublicViaLocations" );
			Assert.IsNotNull( request.PublicSoftViaLocations[0], "PublicSoftViaLocations" );
			Assert.IsNotNull( request.PublicNotViaLocations[0], "PublicNotViaLocations" );

			Assert.IsNotNull( request.PrivateViaLocation, "PrivateViaLocation" );
			Assert.AreEqual( "A6", request.AvoidRoads[1], "AvoidRoads");
            Assert.AreEqual("4000000009316750", request.AvoidToidsOutward[0], "AvoidToidsOutward");
            Assert.AreEqual("4000000009316751", request.AvoidToidsReturn[0], "AvoidToidsReturn");
			Assert.IsNotNull( request.AlternateLocations, "AlternateLocation" );
			Assert.AreEqual( 2, request.AlternateLocations.Length, "AlternateLocation Length" );
			Assert.IsTrue( request.AlternateLocationsFrom, "AlternateLocationFrom" );
			Assert.AreEqual( PrivateAlgorithmType.MostEconomical, request.PrivateAlgorithm, "PrivateAlgorithm" );
			Assert.AreEqual( PublicAlgorithmType.Fastest, request.PublicAlgorithm, "PublicAlgorithm" );
			Assert.AreEqual( false, request.AvoidTolls, "AvoidTolls" );
			Assert.AreEqual( false, request.AvoidFerries, "AvoidFerries" );
			Assert.AreEqual( "M25", request.IncludeRoads[0], "IncludeRoads" );
			Assert.AreEqual( "20.5", request.FuelPrice, "FuelPrice" );
			Assert.AreEqual( "10.85", request.FuelConsumption, "FuelConsumption" );

			Assert.AreEqual( "9100A", request.RoutingPointNaptans[0], "RoutingPointNaptans");

			Assert.AreEqual( 5, request.Sequence, "Sequence" );
		}
	}
}
