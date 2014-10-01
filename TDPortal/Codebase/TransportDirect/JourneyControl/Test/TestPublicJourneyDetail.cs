// *********************************************** 
// NAME			: TestPublicJourneyDetail.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the TestPublicJourneyDetail class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestPublicJourneyDetail.cs-arc  $
//
//   Rev 1.1   Mar 19 2013 12:03:38   mmodi
//Test for accessible icons
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.0   Nov 08 2007 12:24:18   mturner
//Initial revision.
//
//   Rev 1.14   Aug 24 2005 16:06:52   RPhilpott
//Make tests consistent with changed TDJourneyResult.AddResult() and PublicJourney ctor. 
//Resolution for 2662: DN062:  Map with non-consecutive nodes with no co-ordinates functioning incorrectly
//
//   Rev 1.13   Aug 19 2005 14:04:32   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.12.1.2   Aug 16 2005 14:32:30   RPhilpott
//FxCop fixes.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.12.1.1   Jul 13 2005 16:58:24   RPhilpott
//Updated for additional intermediate station data.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.12.1.0   Jul 07 2005 16:14:22   rgreenwood
//DN062: Removed SettingVehicleFeatures() test, which is obsolete for this class.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.12   Mar 23 2005 15:22:22   rhopkins
//Fixed FxCop "warnings"
//
//   Rev 1.11   Mar 02 2005 14:33:08   rscott
//DEL 7 - updated to include new properties originLocation and originDateTime
//
//   Rev 1.10   Jan 20 2005 11:42:00   RScott
//Updated to test vehicleFeatures
//
//   Rev 1.9   Jan 20 2005 09:28:10   RScott
//DEL 7 - Updated to for addition of PublicViaLocations, PublicSoftViaLocations, PublicNotViaLocations.
//
//   Rev 1.8   Jan 18 2005 15:53:24   rhopkins
//Removed dependancy on other tests so that this test will work when TestCarCostCalculator is present.
//Also changed Assertion to Assert.
//
//   Rev 1.7   Jul 28 2004 10:54:08   CHosegood
//Updated to compile against CJP 6.0.0.0
//NOT TESTED!!!
//
//   Rev 1.6   Feb 19 2004 17:05:40   COwczarek
//Refactored PublicJourneyDetail into new class hierarchy representing different journey leg types (timed, continuous and frequency based)
//Resolution for 629: Frequency based Journeys
//
//   Rev 1.5   Nov 06 2003 16:26:46   PNorell
//Ensured tests works properly.
//
//   Rev 1.4   Oct 15 2003 21:55:34   acaunt
//Destinations added to the leg data
//
//   Rev 1.3   Sep 20 2003 19:24:46   RPhilpott
//Support for passing OSGR's with NaPTAN's, various other fixes
//
//   Rev 1.2   Sep 12 2003 16:28:54   PNorell
//Fixed test for geometry.
//
//   Rev 1.1   Sep 05 2003 15:29:04   passuied
//Deletion of TDLocation, LocationCodingType, OSGridReference and transfer to LocationService
//
//   Rev 1.0   Aug 20 2003 17:55:28   AToner
//Initial Revision
using System;
using NUnit.Framework;

using TransportDirect.JourneyPlanning.CJPInterface;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;

using ICJP = TransportDirect.JourneyPlanning.CJPInterface;
using System.Diagnostics;
using TransportDirect.Common.Logging;
using System.Collections;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for TestPublicJourneyDetail.
	/// </summary>
	[TestFixture]
	public class TestPublicJourneyDetail
	{
		private PublicJourneyDetail publicJourneyDetail;
		private DateTime timeNow = DateTime.Now;
		
		// Assortment of legs to cover significant permutations 
		//  [O = Origin, B = Board, A = Alight, D = Destn, I = intermediate(s)] 

		private TimedLeg O_B_A_D;
		private TimedLeg OB_AD;
		private TimedLeg OB_I_AD;
		private TimedLeg O_I_B_A_I_D;
		private TimedLeg O_I_B_I_A_I_D;
        private TimedLeg OB_AD_Accessible;


		public TestPublicJourneyDetail()
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

			CreateLegs();


		}
		
		/// <summary>
		/// Test the simple leg with board and alight events, different origin and destintaion
		/// </summary>
		[Test]
		public void BoardAndAlight1()
		{
			publicJourneyDetail = PublicJourneyDetail.Create( O_B_A_D, null );

			Assert.AreEqual( ModeType.Rail, publicJourneyDetail.Mode, "ModeType");
			Assert.AreEqual( true, publicJourneyDetail.IsValidated, "IsValidated" );
			Assert.AreEqual( "Origin NAPTANID", publicJourneyDetail.Origin.Location.NaPTANs[0].Naptan, "Origin" );
			Assert.AreEqual( "Destination NAPTANID", publicJourneyDetail.Destination.Location.NaPTANs[0].Naptan, "Destination" );
			Assert.AreEqual( "Board NAPTANID", publicJourneyDetail.LegStart.Location.NaPTANs[0].Naptan, "StartLocation" );
			Assert.AreEqual( "Alight NAPTANID", publicJourneyDetail.LegEnd.Location.NaPTANs[0].Naptan, "EndLocation" );
			Assert.AreEqual( timeNow.AddMinutes(0), publicJourneyDetail.Origin.DepartureDateTime.GetDateTime(), "Origin DepartureDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(29), publicJourneyDetail.LegStart.ArrivalDateTime.GetDateTime(), "Board ArrivalDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(31), publicJourneyDetail.LegStart.DepartureDateTime.GetDateTime(), "Board DepartDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(59), publicJourneyDetail.LegEnd.ArrivalDateTime.GetDateTime(), "Alight ArrivalDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(61), publicJourneyDetail.LegEnd.DepartureDateTime.GetDateTime(), "Alight DepartDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(90), publicJourneyDetail.Destination.ArrivalDateTime.GetDateTime(), "Destn ArrivalDateTime" );
			Assert.IsNull(publicJourneyDetail.Services, "Services");
			Assert.AreEqual(2, publicJourneyDetail.Geometry.Length, "Geometry");
			Assert.AreEqual(false, publicJourneyDetail.IncludesVia, "IncludesVia" );

			Assert.AreEqual(0, publicJourneyDetail.GetIntermediatesBefore().Length);
			Assert.AreEqual(0, publicJourneyDetail.GetIntermediatesAfter().Length);
			Assert.AreEqual(0, publicJourneyDetail.GetIntermediatesLeg().Length);

			Assert.AreEqual(PublicJourneyCallingPointType.Origin, publicJourneyDetail.Origin.Type);
			Assert.AreEqual(PublicJourneyCallingPointType.Board, publicJourneyDetail.LegStart.Type);
			Assert.AreEqual(PublicJourneyCallingPointType.Alight, publicJourneyDetail.LegEnd.Type);
			Assert.AreEqual(PublicJourneyCallingPointType.Destination, publicJourneyDetail.Destination.Type);

			// verify deprecated 'legacy' properties 
			// these tests can be removed once these 
			// properties are no longer in use ...

			Assert.AreEqual( "Origin NAPTANID", publicJourneyDetail.OriginLocation.NaPTANs[0].Naptan, "Origin");
			Assert.AreEqual( "Destination NAPTANID", publicJourneyDetail.Destination.Location.NaPTANs[0].Naptan, "Destination");
			Assert.AreEqual( "Board NAPTANID", publicJourneyDetail.StartLocation.NaPTANs[0].Naptan, "StartLocation" );
			Assert.AreEqual( "Alight NAPTANID", publicJourneyDetail.EndLocation.NaPTANs[0].Naptan, "EndLocation" );
			Assert.AreEqual( timeNow.AddMinutes(31), publicJourneyDetail.DepartDateTime.GetDateTime(), "Board DepartDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(59), publicJourneyDetail.ArriveDateTime.GetDateTime(), "Alight ArrivalDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(0), publicJourneyDetail.OriginDateTime.GetDateTime(), "Origin DepartureDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(90), publicJourneyDetail.DestinationDateTime.GetDateTime(), "Destn ArrivalDateTime" );
		}
		
		/// <summary>
		/// Test simple leg with board and alight events, where origin = board and dest = alight. 
		/// </summary>
		[Test]
		public void BoardAndAlight2()
		{
			publicJourneyDetail = PublicJourneyDetail.Create(OB_AD, null);

			Assert.AreEqual( ModeType.Rail, publicJourneyDetail.Mode, "ModeType");
			Assert.AreEqual( true, publicJourneyDetail.IsValidated, "IsValidated" );

			Assert.AreEqual( "Origin NAPTANID", publicJourneyDetail.Origin.Location.NaPTANs[0].Naptan, "Origin" );
			Assert.AreEqual( "Destination NAPTANID", publicJourneyDetail.Destination.Location.NaPTANs[0].Naptan, "Destination" );
			Assert.AreEqual( "Origin NAPTANID", publicJourneyDetail.LegStart.Location.NaPTANs[0].Naptan, "StartLocation" );
			Assert.AreEqual( "Destination NAPTANID", publicJourneyDetail.LegEnd.Location.NaPTANs[0].Naptan, "EndLocation" );
			Assert.AreEqual( timeNow.AddMinutes(0), publicJourneyDetail.Origin.DepartureDateTime.GetDateTime(), "Origin DepartureDateTime" );
			Assert.AreEqual( DateTime.MinValue, publicJourneyDetail.LegStart.ArrivalDateTime.GetDateTime(), "Board ArrivalDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(0), publicJourneyDetail.LegStart.DepartureDateTime.GetDateTime(), "Board DepartDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(90), publicJourneyDetail.LegEnd.ArrivalDateTime.GetDateTime(), "Alight ArrivalDateTime" );
			Assert.AreEqual( DateTime.MinValue, publicJourneyDetail.LegEnd.DepartureDateTime.GetDateTime(), "Alight DepartDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(90), publicJourneyDetail.Destination.ArrivalDateTime.GetDateTime(), "Destn ArrivalDateTime" );
			Assert.IsNull(publicJourneyDetail.Services, "Services");
			Assert.AreEqual(2, publicJourneyDetail.Geometry.Length, "Geometry");
			Assert.AreEqual(false, publicJourneyDetail.IncludesVia, "IncludesVia" );

			Assert.AreEqual(0, publicJourneyDetail.GetIntermediatesBefore().Length);
			Assert.AreEqual(0, publicJourneyDetail.GetIntermediatesAfter().Length);
			Assert.AreEqual(0, publicJourneyDetail.GetIntermediatesLeg().Length);

			Assert.AreEqual(PublicJourneyCallingPointType.OriginAndBoard, publicJourneyDetail.Origin.Type);
			Assert.AreEqual(PublicJourneyCallingPointType.OriginAndBoard, publicJourneyDetail.LegStart.Type);
			Assert.AreEqual(PublicJourneyCallingPointType.DestinationAndAlight, publicJourneyDetail.LegEnd.Type);
			Assert.AreEqual(PublicJourneyCallingPointType.DestinationAndAlight, publicJourneyDetail.Destination.Type);

			// verify deprecated 'legacy' properties 
			// these tests can be removed once these 
			// properties are no longer in use ...

			Assert.AreEqual( "Origin NAPTANID", publicJourneyDetail.OriginLocation.NaPTANs[0].Naptan, "Origin");
			Assert.AreEqual( "Destination NAPTANID", publicJourneyDetail.DestinationLocation.NaPTANs[0].Naptan, "Destination");
			Assert.AreEqual( "Origin NAPTANID", publicJourneyDetail.StartLocation.NaPTANs[0].Naptan, "StartLocation" );
			Assert.AreEqual( "Destination NAPTANID", publicJourneyDetail.EndLocation.NaPTANs[0].Naptan, "EndLocation" );
			Assert.AreEqual( timeNow.AddMinutes(0), publicJourneyDetail.DepartDateTime.GetDateTime(), "Board DepartDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(90), publicJourneyDetail.ArriveDateTime.GetDateTime(), "Alight ArrivalDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(0), publicJourneyDetail.OriginDateTime.GetDateTime(), "Origin DepartureDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(90), publicJourneyDetail.DestinationDateTime.GetDateTime(), "Destn ArrivalDateTime" );


		}

		/// <summary>
		/// Test leg with three intermediatesB, where origin = board and dest = alight. 
		/// </summary>
		[Test]
		public void IntermediatesBOnly()
		{
			publicJourneyDetail = PublicJourneyDetail.Create(OB_I_AD, null);

			Assert.AreEqual( ModeType.Rail, publicJourneyDetail.Mode, "ModeType");
			Assert.AreEqual( true, publicJourneyDetail.IsValidated, "IsValidated" );

			Assert.AreEqual( "Origin NAPTANID", publicJourneyDetail.Origin.Location.NaPTANs[0].Naptan, "Origin" );
			Assert.AreEqual( "Destination NAPTANID", publicJourneyDetail.Destination.Location.NaPTANs[0].Naptan, "Destination" );
			Assert.AreEqual( "Origin NAPTANID", publicJourneyDetail.LegStart.Location.NaPTANs[0].Naptan, "StartLocation" );
			Assert.AreEqual( "Destination NAPTANID", publicJourneyDetail.LegEnd.Location.NaPTANs[0].Naptan, "EndLocation" );
			Assert.AreEqual( timeNow.AddMinutes(0), publicJourneyDetail.Origin.DepartureDateTime.GetDateTime(), "Origin DepartureDateTime" );
			Assert.AreEqual( DateTime.MinValue, publicJourneyDetail.LegStart.ArrivalDateTime.GetDateTime(), "Board ArrivalDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(0), publicJourneyDetail.LegStart.DepartureDateTime.GetDateTime(), "Board DepartDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(90), publicJourneyDetail.LegEnd.ArrivalDateTime.GetDateTime(), "Alight ArrivalDateTime" );
			Assert.AreEqual( DateTime.MinValue, publicJourneyDetail.LegEnd.DepartureDateTime.GetDateTime(), "Alight DepartDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(90), publicJourneyDetail.Destination.ArrivalDateTime.GetDateTime(), "Destn ArrivalDateTime" );
			Assert.IsNull(publicJourneyDetail.Services, "Services");

			Assert.AreEqual(PublicJourneyCallingPointType.OriginAndBoard, publicJourneyDetail.Origin.Type);
			Assert.AreEqual(PublicJourneyCallingPointType.OriginAndBoard, publicJourneyDetail.LegStart.Type);
			Assert.AreEqual(PublicJourneyCallingPointType.DestinationAndAlight, publicJourneyDetail.LegEnd.Type);
			Assert.AreEqual(PublicJourneyCallingPointType.DestinationAndAlight, publicJourneyDetail.Destination.Type);
			
			Assert.AreEqual(5, publicJourneyDetail.Geometry.Length, "Geometry");
			Assert.AreEqual(false, publicJourneyDetail.IncludesVia, "IncludesVia" );

			Assert.AreEqual(0, publicJourneyDetail.GetIntermediatesBefore().Length);
			Assert.AreEqual(0, publicJourneyDetail.GetIntermediatesAfter().Length);
			Assert.AreEqual(3, publicJourneyDetail.GetIntermediatesLeg().Length);

			Assert.AreEqual("IntB1 NAPTANID", publicJourneyDetail.GetIntermediatesLeg()[0].Location.NaPTANs[0].Naptan);
			Assert.AreEqual("IntB2 NAPTANID", publicJourneyDetail.GetIntermediatesLeg()[1].Location.NaPTANs[0].Naptan);
			Assert.AreEqual("IntB3 NAPTANID", publicJourneyDetail.GetIntermediatesLeg()[2].Location.NaPTANs[0].Naptan);
			
			Assert.AreEqual(PublicJourneyCallingPointType.CallingPoint, publicJourneyDetail.GetIntermediatesLeg()[0].Type);
			Assert.AreEqual(PublicJourneyCallingPointType.CallingPoint, publicJourneyDetail.GetIntermediatesLeg()[1].Type);
			Assert.AreEqual(PublicJourneyCallingPointType.PassingPoint, publicJourneyDetail.GetIntermediatesLeg()[2].Type);


		}
	
		
		/// <summary>
		/// Test leg with three intermediatesA, zero intermediatesB, one intermediateC,
		///  where origin != board and dest != alight. 
		/// </summary>
		[Test]
		public void IntermediatesAandC()
		{
			publicJourneyDetail = PublicJourneyDetail.Create(O_I_B_A_I_D, null);

			Assert.AreEqual( ModeType.Rail, publicJourneyDetail.Mode, "ModeType");
			Assert.AreEqual( true, publicJourneyDetail.IsValidated, "IsValidated" );

			Assert.AreEqual( "Origin NAPTANID", publicJourneyDetail.Origin.Location.NaPTANs[0].Naptan, "Origin" );
			Assert.AreEqual( "Destination NAPTANID", publicJourneyDetail.Destination.Location.NaPTANs[0].Naptan, "Destination" );
			Assert.AreEqual( "Board NAPTANID", publicJourneyDetail.LegStart.Location.NaPTANs[0].Naptan, "StartLocation" );
			Assert.AreEqual( "Alight NAPTANID", publicJourneyDetail.LegEnd.Location.NaPTANs[0].Naptan, "EndLocation" );
			Assert.AreEqual( timeNow.AddMinutes(0), publicJourneyDetail.Origin.DepartureDateTime.GetDateTime(), "Origin DepartureDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(29), publicJourneyDetail.LegStart.ArrivalDateTime.GetDateTime(), "Board ArrivalDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(31), publicJourneyDetail.LegStart.DepartureDateTime.GetDateTime(), "Board DepartDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(59), publicJourneyDetail.LegEnd.ArrivalDateTime.GetDateTime(), "Alight ArrivalDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(61), publicJourneyDetail.LegEnd.DepartureDateTime.GetDateTime(), "Alight DepartDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(90), publicJourneyDetail.Destination.ArrivalDateTime.GetDateTime(), "Destn ArrivalDateTime" );
			Assert.IsNull(publicJourneyDetail.Services, "Services");

			Assert.AreEqual(PublicJourneyCallingPointType.Origin, publicJourneyDetail.Origin.Type);
			Assert.AreEqual(PublicJourneyCallingPointType.Board, publicJourneyDetail.LegStart.Type);
			Assert.AreEqual(PublicJourneyCallingPointType.Alight, publicJourneyDetail.LegEnd.Type);
			Assert.AreEqual(PublicJourneyCallingPointType.Destination, publicJourneyDetail.Destination.Type);
			
			Assert.AreEqual(2, publicJourneyDetail.Geometry.Length, "Geometry");
			Assert.AreEqual(false, publicJourneyDetail.IncludesVia, "IncludesVia" );

			Assert.AreEqual(3, publicJourneyDetail.GetIntermediatesBefore().Length);
			Assert.AreEqual(1, publicJourneyDetail.GetIntermediatesAfter().Length);
			Assert.AreEqual(0, publicJourneyDetail.GetIntermediatesLeg().Length);

			Assert.AreEqual("IntA1 NAPTANID", publicJourneyDetail.GetIntermediatesBefore()[0].Location.NaPTANs[0].Naptan);
			Assert.AreEqual("IntA2 NAPTANID", publicJourneyDetail.GetIntermediatesBefore()[1].Location.NaPTANs[0].Naptan);
			Assert.AreEqual("IntA3 NAPTANID", publicJourneyDetail.GetIntermediatesBefore()[2].Location.NaPTANs[0].Naptan);
			
			Assert.AreEqual("IntC1 NAPTANID", publicJourneyDetail.GetIntermediatesAfter()[0].Location.NaPTANs[0].Naptan);
			
			Assert.AreEqual(PublicJourneyCallingPointType.CallingPoint, publicJourneyDetail.GetIntermediatesBefore()[0].Type);
			Assert.AreEqual(PublicJourneyCallingPointType.PassingPoint, publicJourneyDetail.GetIntermediatesBefore()[1].Type);
			Assert.AreEqual(PublicJourneyCallingPointType.CallingPoint, publicJourneyDetail.GetIntermediatesBefore()[2].Type);
			
			Assert.AreEqual(PublicJourneyCallingPointType.CallingPoint, publicJourneyDetail.GetIntermediatesAfter()[0].Type);


		}	

		
		/// <summary>
		/// Test leg with two intermediatesA, three intermediatesB, one intermediateC,
		///  where origin != board and dest != alight. 
		/// </summary>
		[Test]
		public void IntermediatesAandBandC()
		{
			publicJourneyDetail = PublicJourneyDetail.Create(O_I_B_I_A_I_D, null);

			Assert.AreEqual( ModeType.Rail, publicJourneyDetail.Mode, "ModeType");
			Assert.AreEqual( true, publicJourneyDetail.IsValidated, "IsValidated" );

			Assert.AreEqual( "Origin NAPTANID", publicJourneyDetail.Origin.Location.NaPTANs[0].Naptan, "Origin" );
			Assert.AreEqual( "Destination NAPTANID", publicJourneyDetail.Destination.Location.NaPTANs[0].Naptan, "Destination" );
			Assert.AreEqual( "Board NAPTANID", publicJourneyDetail.LegStart.Location.NaPTANs[0].Naptan, "StartLocation" );
			Assert.AreEqual( "Alight NAPTANID", publicJourneyDetail.LegEnd.Location.NaPTANs[0].Naptan, "EndLocation" );
			Assert.AreEqual( timeNow.AddMinutes(0), publicJourneyDetail.Origin.DepartureDateTime.GetDateTime(), "Origin DepartureDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(29), publicJourneyDetail.LegStart.ArrivalDateTime.GetDateTime(), "Board ArrivalDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(31), publicJourneyDetail.LegStart.DepartureDateTime.GetDateTime(), "Board DepartDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(59), publicJourneyDetail.LegEnd.ArrivalDateTime.GetDateTime(), "Alight ArrivalDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(61), publicJourneyDetail.LegEnd.DepartureDateTime.GetDateTime(), "Alight DepartDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(90), publicJourneyDetail.Destination.ArrivalDateTime.GetDateTime(), "Destn ArrivalDateTime" );
			Assert.IsNull(publicJourneyDetail.Services, "Services");

			Assert.AreEqual(PublicJourneyCallingPointType.Origin, publicJourneyDetail.Origin.Type);
			Assert.AreEqual(PublicJourneyCallingPointType.Board, publicJourneyDetail.LegStart.Type);
			Assert.AreEqual(PublicJourneyCallingPointType.Alight, publicJourneyDetail.LegEnd.Type);
			Assert.AreEqual(PublicJourneyCallingPointType.Destination, publicJourneyDetail.Destination.Type);
			
			Assert.AreEqual(5, publicJourneyDetail.Geometry.Length, "Geometry");
			Assert.AreEqual(false, publicJourneyDetail.IncludesVia, "IncludesVia" );

			Assert.AreEqual(2, publicJourneyDetail.GetIntermediatesBefore().Length);
			Assert.AreEqual(1, publicJourneyDetail.GetIntermediatesAfter().Length);
			Assert.AreEqual(3, publicJourneyDetail.GetIntermediatesLeg().Length);

			Assert.AreEqual("IntA1 NAPTANID", publicJourneyDetail.GetIntermediatesBefore()[0].Location.NaPTANs[0].Naptan);
			Assert.AreEqual("IntA2 NAPTANID", publicJourneyDetail.GetIntermediatesBefore()[1].Location.NaPTANs[0].Naptan);

			Assert.AreEqual("IntB1 NAPTANID", publicJourneyDetail.GetIntermediatesLeg()[0].Location.NaPTANs[0].Naptan);
			Assert.AreEqual("IntB2 NAPTANID", publicJourneyDetail.GetIntermediatesLeg()[1].Location.NaPTANs[0].Naptan);
			Assert.AreEqual("IntB3 NAPTANID", publicJourneyDetail.GetIntermediatesLeg()[2].Location.NaPTANs[0].Naptan);
			
			Assert.AreEqual("IntC1 NAPTANID", publicJourneyDetail.GetIntermediatesAfter()[0].Location.NaPTANs[0].Naptan);
			
			Assert.AreEqual(PublicJourneyCallingPointType.CallingPoint, publicJourneyDetail.GetIntermediatesBefore()[0].Type);
			Assert.AreEqual(PublicJourneyCallingPointType.PassingPoint, publicJourneyDetail.GetIntermediatesBefore()[1].Type);

			Assert.AreEqual(PublicJourneyCallingPointType.CallingPoint, publicJourneyDetail.GetIntermediatesLeg()[0].Type);
			Assert.AreEqual(PublicJourneyCallingPointType.CallingPoint, publicJourneyDetail.GetIntermediatesLeg()[1].Type);
			Assert.AreEqual(PublicJourneyCallingPointType.PassingPoint, publicJourneyDetail.GetIntermediatesLeg()[2].Type);
			
			Assert.AreEqual(PublicJourneyCallingPointType.CallingPoint, publicJourneyDetail.GetIntermediatesAfter()[0].Type);

			// verify deprecated 'legacy' properties 
			// these tests can be removed once these 
			// properties are no longer in use ...

			Assert.AreEqual( "Origin NAPTANID", publicJourneyDetail.OriginLocation.NaPTANs[0].Naptan, "Origin");
			Assert.AreEqual( "Destination NAPTANID", publicJourneyDetail.DestinationLocation.NaPTANs[0].Naptan, "Destination");
			Assert.AreEqual( "Board NAPTANID", publicJourneyDetail.StartLocation.NaPTANs[0].Naptan, "StartLocation" );
			Assert.AreEqual( "Alight NAPTANID", publicJourneyDetail.EndLocation.NaPTANs[0].Naptan, "EndLocation" );
			Assert.AreEqual( timeNow.AddMinutes(31), publicJourneyDetail.DepartDateTime.GetDateTime(), "Board DepartDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(59), publicJourneyDetail.ArriveDateTime.GetDateTime(), "Alight ArrivalDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(0), publicJourneyDetail.OriginDateTime.GetDateTime(), "Origin DepartureDateTime" );
			Assert.AreEqual( timeNow.AddMinutes(90), publicJourneyDetail.DestinationDateTime.GetDateTime(), "Destn ArrivalDateTime" );


		}	

		/// <summary>
		/// Using the simple leg check for a via using the same NapTAN as the board event
		/// </summary>
		[Test]
		public void BoardAsVia()
		{
			TDLocation via = new TDLocation();
			via.NaPTANs = new TDNaptan[1];
			via.NaPTANs[0] = new TDNaptan();
			via.NaPTANs[0].Naptan = "Board NAPTANID";

			publicJourneyDetail = PublicJourneyDetail.Create(O_B_A_D, via);
			Assert.IsTrue(publicJourneyDetail.IncludesVia, "IncludesVia");
		}
		
		/// <summary>
		/// Add an intermediate to the leg and check for a via at the same NapTAN
		/// </summary>
		[Test]
		public void IntermediateAsVia()
		{
			TDLocation via = new TDLocation();
			via.NaPTANs = new TDNaptan[1];
			via.NaPTANs[0] = new TDNaptan();
			via.NaPTANs[0].Naptan = "IntB1 NAPTANID";

			publicJourneyDetail = PublicJourneyDetail.Create( O_I_B_I_A_I_D, via  );
			Assert.IsTrue(publicJourneyDetail.IncludesVia, "IncludesVia");
			Assert.AreEqual(5, publicJourneyDetail.Geometry.Length, "Geometry");
		}

        /// <summary>
        /// Test detail accessibility for board alight and service
        /// </summary>
        [Test]
        public void BoardAlightServiceAccessibility()
        {
            publicJourneyDetail = PublicJourneyDetail.Create(OB_AD_Accessible, null);

            Assert.AreEqual(ModeType.Rail, publicJourneyDetail.Mode, "ModeType");
            Assert.AreEqual(true, publicJourneyDetail.IsValidated, "IsValidated");

            Assert.AreEqual(PublicJourneyCallingPointType.OriginAndBoard, publicJourneyDetail.Origin.Type);
            Assert.AreEqual(PublicJourneyCallingPointType.OriginAndBoard, publicJourneyDetail.LegStart.Type);
            Assert.AreEqual(PublicJourneyCallingPointType.DestinationAndAlight, publicJourneyDetail.LegEnd.Type);
            Assert.AreEqual(PublicJourneyCallingPointType.DestinationAndAlight, publicJourneyDetail.Destination.Type);

            Assert.IsNotNull(publicJourneyDetail.BoardAccessibility, "BoardAccessibility");
            Assert.IsNotNull(publicJourneyDetail.AlightAccessibility, "AlightAccessibility");
            Assert.IsNotNull(publicJourneyDetail.ServiceAccessibility, "ServiceAccessibility");
            
            // Test for expected accessibly types
            Assert.IsTrue(!publicJourneyDetail.BoardAccessibility.Contains(AccessibilityType.EscalatorFreeAccess));
            Assert.IsTrue(!publicJourneyDetail.BoardAccessibility.Contains(AccessibilityType.LiftFreeAccess));
            Assert.IsTrue(!publicJourneyDetail.BoardAccessibility.Contains(AccessibilityType.MobilityImpairedAccess));
            Assert.IsTrue(!publicJourneyDetail.BoardAccessibility.Contains(AccessibilityType.StepFreeAccess));
            Assert.IsTrue(publicJourneyDetail.BoardAccessibility.Contains(AccessibilityType.WheelchairAccess));
            Assert.IsTrue(!publicJourneyDetail.BoardAccessibility.Contains(AccessibilityType.AccessStairsUp));
            Assert.IsTrue(!publicJourneyDetail.BoardAccessibility.Contains(AccessibilityType.AccessStairsDown));
            Assert.IsTrue(!publicJourneyDetail.BoardAccessibility.Contains(AccessibilityType.AccessPassage));

            Assert.IsTrue(!publicJourneyDetail.AlightAccessibility.Contains(AccessibilityType.WheelchairAccess));

            Assert.IsTrue(publicJourneyDetail.ServiceAccessibility.Contains(AccessibilityType.ServiceAssistanceBoarding));
            Assert.IsTrue(!publicJourneyDetail.ServiceAccessibility.Contains(AccessibilityType.ServiceAssistanceOther));
            Assert.IsTrue(!publicJourneyDetail.ServiceAccessibility.Contains(AccessibilityType.ServiceAssistancePorterage));
            Assert.IsTrue(publicJourneyDetail.ServiceAccessibility.Contains(AccessibilityType.ServiceAssistanceWheelchair));
            Assert.IsTrue(!publicJourneyDetail.ServiceAccessibility.Contains(AccessibilityType.ServiceAssistanceWheelchairBooked));
            
            // Test for service accessibility - different modes and accessible combinations
            ModeType resetModeType = OB_AD_Accessible.mode;
            ServiceAccessibility resetServiceAccessibility = OB_AD_Accessible.serviceAccessibility;

            ServiceAccessibility serviceAccessibility = new ServiceAccessibility();

            serviceAccessibility.lowFloor = true;
            serviceAccessibility.mobilityImpairedAccess = ICJP.AccessibilityType.True;
            serviceAccessibility.wheelchairBookingRequired = true;
            OB_AD_Accessible.mode = ModeType.Rail;
            OB_AD_Accessible.serviceAccessibility = serviceAccessibility;
            publicJourneyDetail = PublicJourneyDetail.Create(OB_AD_Accessible, null);
            Assert.IsTrue(publicJourneyDetail.ServiceAccessibility.Contains(AccessibilityType.ServiceWheelchairBookingRequired));

            
            serviceAccessibility.wheelchairBookingRequired = false;
            publicJourneyDetail = PublicJourneyDetail.Create(OB_AD_Accessible, null);
            Assert.IsTrue(publicJourneyDetail.ServiceAccessibility.Contains(AccessibilityType.ServiceMobilityImpairedAccess));

            serviceAccessibility.mobilityImpairedAccess = ICJP.AccessibilityType.False;
            publicJourneyDetail = PublicJourneyDetail.Create(OB_AD_Accessible, null);
            Assert.IsTrue(!publicJourneyDetail.ServiceAccessibility.Contains(AccessibilityType.ServiceWheelchairBookingRequired));
            Assert.IsTrue(!publicJourneyDetail.ServiceAccessibility.Contains(AccessibilityType.ServiceMobilityImpairedAccess));

            serviceAccessibility.mobilityImpairedAccess = ICJP.AccessibilityType.True;
            OB_AD_Accessible.mode = ModeType.Ferry;
            publicJourneyDetail = PublicJourneyDetail.Create(OB_AD_Accessible, null);
            Assert.IsTrue(publicJourneyDetail.ServiceAccessibility.Contains(AccessibilityType.ServiceMobilityImpairedAccessService));


            serviceAccessibility.lowFloor = true;
            serviceAccessibility.mobilityImpairedAccess = ICJP.AccessibilityType.True;
            OB_AD_Accessible.mode = ModeType.Bus;
            publicJourneyDetail = PublicJourneyDetail.Create(OB_AD_Accessible, null);
            Assert.IsTrue(publicJourneyDetail.ServiceAccessibility.Contains(AccessibilityType.ServiceLowFloor));

            serviceAccessibility.lowFloor = false;
            serviceAccessibility.mobilityImpairedAccess = ICJP.AccessibilityType.True;
            OB_AD_Accessible.mode = ModeType.Bus;
            publicJourneyDetail = PublicJourneyDetail.Create(OB_AD_Accessible, null);
            Assert.IsTrue(publicJourneyDetail.ServiceAccessibility.Contains(AccessibilityType.ServiceMobilityImpairedAccessBus));


            serviceAccessibility.lowFloor = true;
            serviceAccessibility.mobilityImpairedAccess = ICJP.AccessibilityType.True;
            OB_AD_Accessible.mode = ModeType.Tram;
            publicJourneyDetail = PublicJourneyDetail.Create(OB_AD_Accessible, null);
            Assert.IsTrue(publicJourneyDetail.ServiceAccessibility.Contains(AccessibilityType.ServiceLowFloorTram));
            
            serviceAccessibility.lowFloor = false;
            serviceAccessibility.mobilityImpairedAccess = ICJP.AccessibilityType.True;
            OB_AD_Accessible.mode = ModeType.Tram;
            publicJourneyDetail = PublicJourneyDetail.Create(OB_AD_Accessible, null);
            Assert.IsTrue(publicJourneyDetail.ServiceAccessibility.Contains(AccessibilityType.ServiceMobilityImpairedAccess));

            OB_AD_Accessible.mode = resetModeType;
            OB_AD_Accessible.serviceAccessibility = resetServiceAccessibility;
        }


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
            origin.stop.accessibility = new Accessibility();
            origin.stop.accessibility.escalatorFreeAccess = ICJP.AccessibilityType.True;
            origin.stop.accessibility.liftFreeAccess = ICJP.AccessibilityType.True;
            origin.stop.accessibility.mobilityImpairedAccess = ICJP.AccessibilityType.True;
            origin.stop.accessibility.stepFreeAccess = ICJP.AccessibilityType.True;
            origin.stop.accessibility.wheelchairAccess = ICJP.AccessibilityType.True;
            origin.stop.accessibility.accessSummary = new AccessSummary[2];
            origin.stop.accessibility.accessSummary[0] = new AccessSummary();
            origin.stop.accessibility.accessSummary[1] = new AccessSummary();
            origin.stop.accessibility.accessSummary[0].accessFeature = AccessFeature.stairs;
            origin.stop.accessibility.accessSummary[0].count = 1;
            origin.stop.accessibility.accessSummary[0].transition = Transition.up;
            origin.stop.accessibility.accessSummary[1].accessFeature = AccessFeature.passage;
            origin.stop.accessibility.accessSummary[1].count = 1;
            origin.stop.accessibility.accessSummary[1].transition = Transition.down;
                        
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
            destination.stop.accessibility = new Accessibility();
            destination.stop.accessibility.escalatorFreeAccess = ICJP.AccessibilityType.False;
            destination.stop.accessibility.liftFreeAccess = ICJP.AccessibilityType.False;
            destination.stop.accessibility.mobilityImpairedAccess = ICJP.AccessibilityType.False;
            destination.stop.accessibility.stepFreeAccess = ICJP.AccessibilityType.False;
            destination.stop.accessibility.wheelchairAccess = ICJP.AccessibilityType.False;
            destination.stop.accessibility.accessSummary = new AccessSummary[2];
            destination.stop.accessibility.accessSummary[0] = new AccessSummary();
            destination.stop.accessibility.accessSummary[1] = new AccessSummary();
            destination.stop.accessibility.accessSummary[0].accessFeature = AccessFeature.stairs;
            destination.stop.accessibility.accessSummary[0].count = 1;
            destination.stop.accessibility.accessSummary[0].transition = Transition.up;
            destination.stop.accessibility.accessSummary[1].accessFeature = AccessFeature.passage;
            destination.stop.accessibility.accessSummary[1].count = 1;
            destination.stop.accessibility.accessSummary[1].transition = Transition.down;

            ServiceAccessibility serviceAccessibility = new ServiceAccessibility();
            serviceAccessibility.lowFloor = true;
            serviceAccessibility.mobilityImpairedAccess = ICJP.AccessibilityType.True;
            serviceAccessibility.wheelchairBookingRequired = true;
            serviceAccessibility.assistanceServices = new AssistanceServiceType[2];
            serviceAccessibility.assistanceServices[0] = AssistanceServiceType.boarding;
            serviceAccessibility.assistanceServices[1] = AssistanceServiceType.wheelchair;
            
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


            OB_AD_Accessible = new TimedLeg();
            OB_AD_Accessible.mode = ModeType.Rail;
            OB_AD_Accessible.validated = true;

            OB_AD_Accessible.origin = origin;
            OB_AD_Accessible.board = origin;
            OB_AD_Accessible.alight = destination;
            OB_AD_Accessible.destination = destination;

            OB_AD_Accessible.serviceAccessibility = serviceAccessibility;
		}

	}
}
