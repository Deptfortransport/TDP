// ***********************************************
// NAME 		: TestAmendRoute.cs
// AUTHOR 		: Peter Norell
// DATE CREATED : 21/08/2003
// DESCRIPTION 	: Unit test for amend-adjusting a route.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestAdjustRoute.cs-arc  $
//
//   Rev 1.8   Apr 02 2013 11:18:20   mmodi
//Unit test updates
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.7   Sep 06 2012 11:23:20   DLane
//Cycle walk links - reducing GIS calls
//Resolution for 5827: CCN667 Cycle Walk links
//
//   Rev 1.6   Sep 06 2011 11:20:34   apatel
//Updated for Real Time Information for Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.5   Sep 01 2011 10:43:26   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.4   Feb 25 2010 14:48:22   mmodi
//Updates following change to Interface for gettting SummaryLines
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.3   Feb 09 2009 15:17:48   mmodi
//Updated code to apply Routing Guide Sections logic to the adjusted Public Journey
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.2   Mar 10 2008 15:18:02   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:24:12   mturner
//Initial revision.
//
//   Rev 1.37   Oct 16 2007 13:47:16   mmodi
//Added fare request ID increment property
//Resolution for 4448: Logging: Request ID is not included when Coach fares request made
//
//   Rev 1.36   Jan 04 2007 14:01:14   mmodi
//Added UpdateRoadJourney methods following interface change
//Resolution for 4308: CO2: Find detailed journey costs should replan journey
//
//   Rev 1.35   May 04 2006 10:45:00   kjosling
//Removed assertions on PublicViaLocations which are no longer present in replanned journey object graph
//
//   Rev 1.34   Mar 22 2006 14:58:26   rhopkins
//Corrections to Unit test
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.33   Dec 06 2005 18:31:50   pcross
//Updated the class to include the new methods on TDJourneyResult:
//
//GetSelectedOutwardJourneyIndex
//GetSelectedReturnJourneyIndex
//Resolution for 3263: Visit Planner: Selecting Earlier or Later leaves the Journey selected different to the Details displayed
//
//   Rev 1.32   Nov 10 2005 10:07:36   jbroome
//Exposed OutwardJourneyIndex as public read/write property
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.31   Nov 01 2005 15:12:54   build
//Automatically merged from branch for stream2638
//
//   Rev 1.30.1.0   Sep 21 2005 10:51:18   asinclair
//New branch for 2638 with Del 7.1
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.30   Aug 24 2005 16:06:52   RPhilpott
//Make tests consistent with changed TDJourneyResult.AddResult() and PublicJourney ctor. 
//Resolution for 2662: DN062:  Map with non-consecutive nodes with no co-ordinates functioning incorrectly
//
//   Rev 1.29   Aug 19 2005 14:04:28   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.28.1.0   Jul 28 2005 09:58:14   asinclair
//Added new AddMessageToArray with ErrorsType
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.28   May 05 2005 16:49:00   jbroome
//Made JourneyReferenceNumber a writable property, as per updates to interface.
//Resolution for 2414: Coach Find A fare: Selecting next day then one fare causes out of bound exception
//
//   Rev 1.27   Mar 22 2005 11:13:28   jbroome
//Added OutwardPublicJourneys and ReturnPublicJourneys properties as per interface.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.26   Feb 23 2005 16:41:22   rscott
//DEL7 Update - TDDateTime OutwardDateTime and ReturnDateTime changed to TDDateTime[] arrays.
//
//   Rev 1.25   Feb 23 2005 15:11:46   rscott
//Updated Test Setup to provide longer Naptan Prefixes - NUnit Test fail resolution
//
//   Rev 1.24   Feb 07 2005 10:55:52   RScott
//Assertion changed to Assert
//
//   Rev 1.23   Jan 20 2005 09:28:10   RScott
//DEL 7 - Updated to for addition of PublicViaLocations, PublicSoftViaLocations, PublicNotViaLocations.
//
//   Rev 1.22   Sep 17 2004 15:13:04   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.21   Jul 28 2004 10:54:08   CHosegood
//Updated to compile against CJP 6.0.0.0
//NOT TESTED!!!
//
//   Rev 1.20   Jun 29 2004 17:11:30   jmorrissey
//Added CheckForJourneyStartInPast method
//
//   Rev 1.19   Jun 15 2004 14:31:52   COwczarek
//Implement new method added to interface
//Resolution for 867: Add extend journey functionality to summary and details pages
//
//   Rev 1.18   Jun 10 2004 14:10:24   RHopkins
//Added methods OutwardDisplayNumber() and ReturnDisplayNumber()
//
//   Rev 1.17   Feb 19 2004 17:05:38   COwczarek
//Refactored PublicJourneyDetail into new class hierarchy representing different journey leg types (timed, continuous and frequency based)
//Resolution for 629: Frequency based Journeys
//
//   Rev 1.16   Nov 24 2003 15:05:58   kcheung
//Added method to class because interface for journey result has been updated.
//
//   Rev 1.15   Nov 06 2003 17:02:58   kcheung
//Updated so that error messages are cleared once they have been displayed.
//
//   Rev 1.14   Nov 06 2003 16:26:52   PNorell
//Ensured test work properly.
//
//   Rev 1.13   Oct 15 2003 21:55:42   acaunt
//Destinations added to the leg data
//
//   Rev 1.12   Sep 23 2003 14:06:30   RPhilpott
//Changes to TDJourneyResult (ctor and referenceNumber)
//
//   Rev 1.11   Sep 20 2003 19:24:42   RPhilpott
//Support for passing OSGR's with NaPTAN's, various other fixes
//
//   Rev 1.10   Sep 18 2003 13:25:02   RPhilpott
//Add IsValid flag
//
//   Rev 1.9   Sep 12 2003 16:29:32   PNorell
//Fixed the timing checks as the default is to roll the time backward or forward depending on the type of search.
//
//   Rev 1.8   Sep 10 2003 14:39:42   RPhilpott
//Still work in progress ... 
//
//   Rev 1.7   Sep 09 2003 16:01:50   RPhilpott
//Add journey counts to result interface.
//
//   Rev 1.6   Sep 05 2003 16:51:12   RPhilpott
//Remove unused fields.
//
//   Rev 1.5   Sep 05 2003 15:29:00   passuied
//Deletion of TDLocation, LocationCodingType, OSGridReference and transfer to LocationService
//
//   Rev 1.4   Sep 02 2003 12:42:52   kcheung
//Updated becase TDJourneyResult interface Road methods were updated
//
//   Rev 1.3   Sep 01 2003 16:28:42   jcotton
//Updated: RouteNum
//
//   Rev 1.2   Aug 29 2003 10:44:04   kcheung
//Updated made after TDTimeSearchType was replaced by a boolean.
//
//   Rev 1.1   Aug 28 2003 17:57:18   kcheung
//Updated to take into account that ITDJourneyResult does not have a property to return the array of journeys any more.
//
//   Rev 1.0   Aug 27 2003 10:50:14   PNorell
//Initial Revision
using System;
using System.Collections;
using NUnit.Framework;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;
using System.Diagnostics;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.JourneyControl
{
	public enum TestLocations : int
	{
		A,B,C,D,E,F
	}

	/// <summary>
	/// NUnit test class for testing the AdjustRoute
	/// </summary>
	[TestFixture]
	public class TestAdjustRoute
	{
		
		// If these changes ensure enum is updated accordingly
		private string[] locationNames = {
											 "9100A", "Ashington",
											 "9100B", "Bangor",
											 "9100C", "Chester",
											 "9100D", "Derby",
											 "9100E", "Epping",
											 "9100F", "Farringdon"
										 };
		private TDLocation[] locations;

		[SetUp]
		public void Setup()
		{
			// Initialise services
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

			// Create all locations A, B, C, D, E, F
			locations = new TDLocation[ locationNames.Length / 2 ];
			for( int i = 0; i < locationNames.Length; i+=2)
			{
				TDLocation a = new TDLocation();
				a.NaPTANs = new TDNaptan[1];
				a.NaPTANs[0] = new TDNaptan();
				a.NaPTANs[0].Naptan = locationNames[i];
				a.Description = locationNames[i+1];
				locations[i/2] = a;
			}
		}


		/// <summary>
		/// Tests the building of an amend journey request
		/// The original request should have the following characteristics
		/// <list type="disc">
		/// <item>route { A-B B-C C-D }</item>
		/// <item>route time is current time + 30 minutes for each leg and 30 minutes wait between each leg.</item>
		/// <item>it was planned to arrive before current time * route-length + 10 minutes</item>
		/// <item>Modes used is pure rail</item>
		/// <item>No alternative locations</item>
		/// <item>Walking time is 20</item>
		/// <item>Journey has private via</item>
		/// </list>
		/// The adjusted state has catered for the following changes:
		/// <list type="disc">
		/// <item>Node 2 is selected (C)</item>
		/// <item>The change should be arrive before from the selected node</item>
		/// <item>The reference sequence is currently 5</item>
		/// </list>
		/// </summary>
		[Test]
		public void BuildJourneyRequestArriveBefore()
		{
			// Original journey looks like
			//
			//  ___               
			// | A |               Replan split point[2]
			//  ---  +  30 depart    | - Unknown depart
			//   |                   | 
			//   |                   |
			//  _|_  +  60 arrive    |
			// | B |                 |
			//  ---  +  90 depart    |
			//   |                   |
			//   |                   |
			//  _|_  + 120 arrive    | - Arrive before +120
			// | C | 
			//  ---  + 150 depart
			//   |
			//   |
			//  _|_  + 180 arrive
			// | D |				   -> Has alternative location E
			//  ---  
			// 


			// Build the original journey request
			ITDJourneyRequest orgRequest = new TDJourneyRequest();

			// Original journey (start and end)
			TestLocations[] startRoute = { TestLocations.A, TestLocations.B, TestLocations.C, TestLocations.D };
			orgRequest.OriginLocation = getTDLoc( startRoute[0] );
			orgRequest.DestinationLocation = getTDLoc( startRoute[ startRoute.Length - 1] );

			// Time to head off
			DateTime time = DateTime.Now;
			// Date given is for depart later
			orgRequest.OutwardArriveBefore = false;
			// The date that should be used
			orgRequest.OutwardDateTime = new TDDateTime[1];
			orgRequest.OutwardDateTime[0] = new TDDateTime( time.AddMinutes( 30 * startRoute.Length + 10) );
	
			// Alternative locations
			orgRequest.AlternateLocations = new TDLocation[] { getTDLoc( TestLocations.E ) }; 
			orgRequest.AlternateLocationsFrom = false; // It is end

			// Vias
			// Private vias should not be taken into consideration
			orgRequest.PrivateViaLocation = getTDLoc( TestLocations.B );


			// Add a mode type
			orgRequest.Modes = new ModeType[] { ModeType.Rail };

			// Walking time
			orgRequest.MaxWalkingTime = 20;

			// Build the original journey response
			// Original journey: A-B-C-D == length is 3 as in A-B B-C and C-D
			PublicJourneyDetail[] originalDetails = new PublicJourneyDetail[ startRoute.Length - 1 ]; 

			DateTime start = time;
			for(int i = 0; i < originalDetails.Length; i++)
			{
				originalDetails[i] = createLeg( ModeType.Rail, getTDLoc( startRoute[i] ), getTDLoc( startRoute[i+1] ), start, 30);
				start = start.AddMinutes( 30 );
			}

			PublicJourney pj = new PublicJourney(0,originalDetails,TDJourneyType.PublicOriginal, 0);
			// Create the original results
			// MockJourneyResult orgResults = new MockJourneyResult( );

			// Build the adjusted state with the original request
			TDCurrentAdjustState adjustState = new TDCurrentAdjustState(orgRequest);

			// Assign the journey response as the one to be amended
			adjustState.AmendedJourney = pj;

			// Set the amendment type we should be working with
			adjustState.CurrentAmendmentType = TDAmendmentType.OutwardJourney;

			// The current reference sequence set to 5
			adjustState.JourneyReferenceSequence = 5;

			// Set the selected node to appropriate number to indicate where the split point is
			adjustState.SelectedRouteNode = 2;

			// Ensure the route we have selected should leave after
			adjustState.SelectedRouteNodeSearchType = true;

			// Ensure that we spend "at least X minutes at" location
			adjustState.SelectedAdjustTimingsDropdownValue = "2";

			AdjustRoute adjustRoute = new AdjustRoute();

			ITDJourneyRequest request = adjustRoute.BuildJourneyRequest( adjustState );

			// Alternative locations should not be considered since they are not included
			// in the replan start or end location
			Assert.AreEqual(null , request.AlternateLocations, "Alternative locations");
			Assert.AreEqual(false , request.AlternateLocationsFrom, "Alternate locations from");
			// Locations
			Assert.AreEqual(orgRequest.OriginLocation.NaPTANs[0].Naptan, request.OriginLocation.NaPTANs[0].Naptan, "Origin location");
			Assert.AreEqual(getTDLoc( startRoute[2] ).NaPTANs[0].Naptan, request.DestinationLocation.NaPTANs[0].Naptan, "Destination location");
			
			// Time
			
			Assert.AreEqual(true , request.OutwardArriveBefore, "Arrive before for outward journey");
			Assert.IsTrue(pj.Details[1].LegEnd.ArrivalDateTime.GetDateTime() > request.OutwardDateTime[0].GetDateTime(), "Outward time (arrive before)");

			// Same number of modes (1) and the same mode
			Assert.AreEqual(orgRequest.Modes.Length , request.Modes.Length,"Modes length");
			Assert.AreEqual(orgRequest.Modes[0] , request.Modes[0], "Modes");

			// Vias
			Assert.AreEqual(null , request.PrivateViaLocation, "Private via locations");
			Assert.AreEqual(null , request.PublicViaLocations, "Public via locations");

			// Walking information -> should be kept to the original
			Assert.AreEqual(orgRequest.WalkingSpeed , request.WalkingSpeed, "Walking speed");
			Assert.AreEqual(orgRequest.MaxWalkingTime , request.MaxWalkingTime, "Max walking time");

			// Return is never needed for an amend request
			Assert.AreEqual(false , request.IsReturnRequired, "Is return required"); // should always be false
			Assert.AreEqual(false , request.ReturnArriveBefore, "Return arrive before "); // should always be false
			Assert.AreEqual(null , request.ReturnDateTime, "Return date time"); // should always be null

			Assert.IsTrue(adjustState.JourneyReferenceSequence != 5, "Sequence number");

			// Should not be affected by since they are not related to public transport journeys
			Assert.AreEqual(false , request.AvoidMotorways, "Avoid motorways"); // not used
			Assert.AreEqual(null , request.AvoidRoads, "Avoid roads"); // not used
			Assert.AreEqual(0 , request.DrivingSpeed, "Driving speed"); // not used
			Assert.AreEqual(0 , request.InterchangeSpeed, "Driving speed"); // not used

		}
		
		/// <summary>
		/// Tests the building of an amend journey request
		/// The original request should have the following characteristics
		/// <list type="disc">
		/// <item>route { A-B B-C C-D }</item>
		/// <item>route time is current time + 30 minutes for each leg and 30 minutes wait between each leg.</item>
		/// <item>it was planned to arrive before current time * route-length + 10 minutes</item>
		/// <item>Modes used is pure rail</item>
		/// <item>No alternative locations</item>
		/// <item>Walking speed is 10</item>
		/// <item>Journey has public via inside the journey</item>
		/// </list>
		/// The adjusted state has catered for the following changes:
		/// <list type="disc">
		/// <item>Node 2 is selected (C)</item>
		/// <item>The change should be depart later from the selected node</item>
		/// <item>The reference sequence is currently 5</item>
		/// </list>
		/// </summary>
		[Test]
		public void BuildJourneyRequestDepartLater()
		{
			// Original journey looks like
			//
			//  ___  
			// | A |				   -> Has alternative location E
			//  ---  +  30 depart
			//   |
			//   |
			//  _|_  +  60 arrive
			// | B | 
			//  ---  +  90 depart
			//   |
			//   |
			//  _|_  + 120 arrive      
			// | C |               Replan split point[2]
			//  ---  + 150 depart    | - Depart later than +150
			//   |                   |
			//   |                   |
			//  _|_  + 180 arrive    | - Arrive unknown
			// | D | 
			//  ---  
			// 

			// Build the original journey request
			ITDJourneyRequest orgRequest = new TDJourneyRequest();

			// Original journey (start and end)
			TestLocations[] startRoute = { TestLocations.A, TestLocations.B, TestLocations.C, TestLocations.D };
			orgRequest.OriginLocation = getTDLoc( startRoute[0] );
			orgRequest.DestinationLocation = getTDLoc( startRoute[ startRoute.Length - 1] );

			// Time to head off
			DateTime time = DateTime.Now;
			// Date given is for depart later
			orgRequest.OutwardArriveBefore = false;
			// The date that should be used
			orgRequest.OutwardDateTime = new TDDateTime[1];
			orgRequest.OutwardDateTime[0] = new TDDateTime( time.AddMinutes( 30 * startRoute.Length + 10) );
	
			// Alternative locations
			orgRequest.AlternateLocations = new TDLocation[] { getTDLoc( TestLocations.E ) }; 
			orgRequest.AlternateLocationsFrom = true; // It is start 

			// Add a mode type
			orgRequest.Modes = new ModeType[] { ModeType.Rail };

			// Walking speed
			orgRequest.WalkingSpeed = 10;

			// Vias
			// Public journey via within the kept part
			orgRequest.PublicViaLocations = new TDLocation[1];
			orgRequest.PublicViaLocations[0] = getTDLoc( TestLocations.B );

			// Build the original journey response
			// Original journey: A-B-C-D == length is 3 as in A-B B-C and C-D
			PublicJourneyDetail[] originalDetails = new PublicJourneyDetail[ startRoute.Length - 1 ]; 

			DateTime start = time;
			for(int i = 0; i < originalDetails.Length; i++)
			{
				originalDetails[i] = createLeg( ModeType.Rail, getTDLoc( startRoute[i] ), getTDLoc( startRoute[i+1] ), start, 30);
				start = start.AddMinutes( 30 );
			}

			PublicJourney pj = new PublicJourney(0,originalDetails,TDJourneyType.PublicOriginal, 0);
			// Create the original results
			// MockJourneyResult orgResults = new MockJourneyResult( );

			// Build the adjusted state with the original request
			TDCurrentAdjustState adjustState = new TDCurrentAdjustState(orgRequest);

			// Assign the journey response as the one to be amended
			adjustState.AmendedJourney = pj;
			// Set the amendment type we should be working with
			adjustState.CurrentAmendmentType = TDAmendmentType.OutwardJourney;

			// The current reference sequence set to 5
			adjustState.JourneyReferenceSequence = 5;

			// Set the selected node to appropriate number to indicate where the split point is
			adjustState.SelectedRouteNode = 2;

			// Ensure the route we have selected should leave after
			adjustState.SelectedRouteNodeSearchType = false;

			AdjustRoute adjustRoute = new AdjustRoute();

			ITDJourneyRequest request = adjustRoute.BuildJourneyRequest( adjustState );

			// Alternative locations should not be considered since they are not included
			// in the replan start or end location
			Assert.AreEqual(null , request.AlternateLocations, "Alternative locations");
			Assert.AreEqual(false , request.AlternateLocationsFrom, "Alternate locations from");

			// Locations
			Assert.AreEqual(orgRequest.DestinationLocation.NaPTANs[0].Naptan  , request.DestinationLocation.NaPTANs[0].Naptan, "Destination location");
			Assert.AreEqual(getTDLoc( startRoute[2] ).NaPTANs[0].Naptan  , request.OriginLocation.NaPTANs[0].Naptan, "Origin location");

			// Same number of modes (1) and the same mode
			Assert.AreEqual(orgRequest.Modes.Length , request.Modes.Length, "Modes length");
			Assert.AreEqual(orgRequest.Modes[0] , request.Modes[0], "Modes");

			// Times
			Assert.AreEqual(false , request.OutwardArriveBefore,"Arrive before for outward journey");
			Assert.IsTrue(pj.Details[2].LegStart.DepartureDateTime.GetDateTime() < request.OutwardDateTime[0].GetDateTime(), "Outward time (leave after)");

			// vias
			Assert.AreEqual(null , request.PrivateViaLocation,"Private via locations");
			Assert.AreEqual(null , request.PublicViaLocations,"Public via locations");
			
			// Walking information
			Assert.AreEqual(orgRequest.WalkingSpeed , request.WalkingSpeed,"Walking speed");
			Assert.AreEqual(orgRequest.MaxWalkingTime , request.MaxWalkingTime,"Max walking time");
			

			Assert.IsTrue(adjustState.JourneyReferenceSequence != 5, "Sequence number");

			// Return is never needed for an amend request
			Assert.AreEqual(false , request.IsReturnRequired, "Is return required"); // should always be false
			Assert.AreEqual(false , request.ReturnArriveBefore, "Return arrive before "); // should always be false
			Assert.AreEqual(null , request.ReturnDateTime, "Return date time"); // should always be null

			// Should not be affected by since they are not related to public transport journeys
			Assert.AreEqual(false , request.AvoidMotorways, "Avoid motorways"); // not used
			Assert.AreEqual(null , request.AvoidRoads, "Avoid roads"); // not used
			Assert.AreEqual(0 , request.DrivingSpeed, "Driving speed"); // not used
			Assert.AreEqual(0 , request.InterchangeSpeed, "Interchange speed"); // not used

		}

		/// <summary>
		/// Tests the building of an amend journey request
		/// The original request should have the following characteristics
		/// <list type="disc">
		/// <item>route { A-B B-C C-D D-E}</item>
		/// <item>route time is current time + 30 minutes for each leg and 30 minutes wait between each leg.</item>
		/// <item>it was planned to arrive before current time * route-length + 10 minutes</item>
		/// <item>Modes used is pure rail</item>
		/// <item>No alternative locations</item>
		/// <item>Via is D</item>
		/// </list>
		/// The adjusted state has catered for the following changes:
		/// <list type="disc">
		/// <item>Node 2 is selected (C)</item>
		/// <item>The change should be depart later from the selected node</item>
		/// <item>The reference sequence is currently 5</item>
		/// </list>
		/// </summary>
		[Test]
		public void BuildJourneyRequestDepartLaterWithAlternativeDestinationLocations()
		{
			// Original journey looks like
			//
			//  ___  
			// | A |				
			//  ---  +  30 depart
			//   |
			//   |
			//  _|_  +  60 arrive
			// | B | 
			//  ---  +  90 depart
			//   |
			//   |
			//  _|_  + 120 arrive
			// | C |               Replan split point[2]
			//  ---  + 150 depart    | - Depart later than +150
			//   |                   |
			//   |                   |
			//  _|_  + 180 arrive    | 
			// | D |				 | -> Is the via
			//  ---  + 210 arrive    |
			//   |                   |
			//   |                   |
			//  _|_  + 240 arrive    | - Arrive unknown
			// | E |				  -> Has alternative locations [E]
			//  ---  
			// 

			// Build the original journey request
			ITDJourneyRequest orgRequest = new TDJourneyRequest();

			// Original journey (start and end)
			TestLocations[] startRoute = { TestLocations.A, TestLocations.B, TestLocations.C, TestLocations.D, TestLocations.E};
			orgRequest.OriginLocation = getTDLoc( startRoute[0] );
			orgRequest.DestinationLocation = getTDLoc( startRoute[ startRoute.Length - 1] );

			// Time to head off
			DateTime time = DateTime.Now;
			// Date given is for depart later
			orgRequest.OutwardArriveBefore = false;
			// The date that should be used
			orgRequest.OutwardDateTime = new TDDateTime[1];
			orgRequest.OutwardDateTime[0] = new TDDateTime( time.AddMinutes( 30 * startRoute.Length + 10) );
	
			// Alternative locations
			orgRequest.AlternateLocations = new TDLocation[] { getTDLoc( TestLocations.E ) }; 
			orgRequest.AlternateLocationsFrom = false; // It is end

			// Add a mode type
			orgRequest.Modes = new ModeType[] { ModeType.Rail };

			// Via
			// Has an via in the included journey
			orgRequest.PublicViaLocations = new TDLocation[1];
			orgRequest.PublicViaLocations[0] = getTDLoc( TestLocations.D );

			// Build the original journey response
			// Original journey: A-B-C-D-E == length is 4 as in A-B B-C C-D and D-E
			PublicJourneyDetail[] originalDetails = new PublicJourneyDetail[ startRoute.Length - 1 ]; 

			DateTime start = time;
			for(int i = 0; i < originalDetails.Length; i++)
			{
				originalDetails[i] = createLeg( ModeType.Rail, getTDLoc( startRoute[i] ), getTDLoc( startRoute[i+1] ), start, 30, orgRequest.PublicViaLocations[0]);
				start = start.AddMinutes( 30 );
			}

			PublicJourney pj = new PublicJourney(0,originalDetails,TDJourneyType.PublicOriginal, 0);
			// Create the original results
			// MockJourneyResult orgResults = new MockJourneyResult( );

			// Build the adjusted state with the original request
			TDCurrentAdjustState adjustState = new TDCurrentAdjustState(orgRequest);

			// Assign the journey response as the one to be amended
			adjustState.AmendedJourney = pj;
			// Set the amendment type we should be working with
			adjustState.CurrentAmendmentType = TDAmendmentType.OutwardJourney;

			// The current reference sequence set to 5
			adjustState.JourneyReferenceSequence = 5;

			// Set the selected node to appropriate number to indicate where the split point is
			adjustState.SelectedRouteNode = 2;

			// Ensure the route we have selected should leave after
			adjustState.SelectedRouteNodeSearchType = false;

			AdjustRoute adjustRoute = new AdjustRoute();

			ITDJourneyRequest request = adjustRoute.BuildJourneyRequest( adjustState );

			// Alternative locations
			Assert.AreEqual(orgRequest.AlternateLocations , request.AlternateLocations,"Alternative locations");
			Assert.AreEqual(false , request.AlternateLocationsFrom,"Alternate locations from");

			// Locations
			Assert.AreEqual(orgRequest.DestinationLocation.NaPTANs[0].Naptan  , request.DestinationLocation.NaPTANs[0].Naptan,"Destination location");
			Assert.AreEqual(getTDLoc( startRoute[2] ).NaPTANs[0].Naptan  , request.OriginLocation.NaPTANs[0].Naptan,"Origin location");

			// Time
			Assert.AreEqual(false , request.OutwardArriveBefore,"Arrive before for outward journey");
			Assert.IsTrue(pj.Details[2].LegStart.DepartureDateTime.GetDateTime() < request.OutwardDateTime[0].GetDateTime(),"Outward time (leave after)");
			
			// Modes
			Assert.AreEqual(orgRequest.Modes.Length , request.Modes.Length,"Modes length");
			Assert.AreEqual(orgRequest.Modes[0] , request.Modes[0],"Modes");

			// Via
			Assert.AreEqual(null , request.PrivateViaLocation,"Private via locations");

			// Walking
			Assert.AreEqual(orgRequest.WalkingSpeed , request.WalkingSpeed,"Walking speed");
			Assert.AreEqual(orgRequest.MaxWalkingTime , request.MaxWalkingTime,"Max walking time");

			Assert.IsTrue(adjustState.JourneyReferenceSequence != 5 ,"Sequence number");

			// No return journey needed for amending
			Assert.AreEqual(false , request.IsReturnRequired,"Is return required"); // should always be false
			Assert.AreEqual(false , request.ReturnArriveBefore,"Return arrive before "); // should always be false
			Assert.AreEqual(null , request.ReturnDateTime,"Return date time"); // should always be null

			// Should not be affected by since they are not related to public transport journeys
			Assert.AreEqual(false , request.AvoidMotorways,"Avoid motorways"); // not used
			Assert.AreEqual(null , request.AvoidRoads,"Avoid roads"); // not used
			Assert.AreEqual(0 , request.DrivingSpeed,"Driving speed"); // not used
			Assert.AreEqual(0 , request.InterchangeSpeed,"Interchange speed"); // not used

		}

		/// <summary>
		/// Tests of building an amended journey where they are to depart later from the split point
		/// The original request should have the following characteristics
		/// <list type="disc">
		/// <item>route { A-B B-C C-D }</item>
		/// <item>route time is current time + 30 minutes for each leg and 30 minutes wait between each leg.</item>
		/// <item>it was planned to arrive before current time * route-length + 10 minutes</item>
		/// <item>Modes used is pure rail</item>
		/// <item>No alternative locations</item>
		/// </list>
		/// The adjusted state has catered for the following changes:
		/// <list type="disc">
		/// <item>Node 2 is selected (C)</item>
		/// <item>The change should be arrive earlier from the selected node</item>
		/// </list>
		/// </summary>
		[Test]
		public void BuildAmendedJourneyArriveBefore()
		{
			// Original journey looks like
			//
			//  ___               
			// | A |              Replan split point[2]
			//  ---  +  30 depart   |
			//   |                  | 
			//   |                  |
			//  _|_  +  60 arrive   |
			// | B |                |
			//  ---  +  90 depart   |
			//   |                  |
			//   |                  |
			//  _|_  + 120 arrive   |
			// | C | 
			//  ---  + 150 depart
			//   |
			//   |
			//  _|_  + 180 arrive
			// | D | 
			//  ---  
			// 

			// Arrive before or depart later
			bool arriveBefore = true;

			// Original journey (start and end)
			TestLocations[] startLocations = { TestLocations.A, TestLocations.B, TestLocations.C, TestLocations.D };
			// Split point
			int splitPoint = 2; // For TestLocation.C

			// The replan route we would like
			// Journey result becomes: A-E E-F F-B B-C
			TestLocations[] replanedRoute = { TestLocations.A, TestLocations.E, TestLocations.F, TestLocations.B, TestLocations.C };

			PublicJourney endJourney = BuildAmendedJourney(startLocations, splitPoint, replanedRoute, arriveBefore );

			// Verification journey
			// Correct result should be:
			// Legs: A-B B-C C-E E-F F-D
			TestLocations[] correctLegLocations = { 
													  TestLocations.A, TestLocations.E, 
													  TestLocations.E, TestLocations.F,
													  TestLocations.F, TestLocations.B,
													  TestLocations.B, TestLocations.C,
													  TestLocations.C, TestLocations.D
												  };

			// New journey looks like (timings are not correct)
			//
			//  ___  
			// | A | 
			//  ---  -  90 depart
			//   |
			//   |
			//  _|_  -  60 arrive
			// | E | 
			//  ---  -  30 depart
			//   |
			//   |
			//  _|_  +  00 arrive
			// | F | 
			//  ---  +  30 depart
			//   |
			//   |
			//  _|_  +  60 arrive
			// | B | 
			//  ---  +  90 depart
			//   |
			//   |
			//  _|_  + 120 arrive
			// | C | 
			//  ---  + 150 depart
			//   |
			//   |
			//  _|_  + 180 arrive
			// | D | 
			//  ---  
			// 

			// Assert adjusted journey is correct/uncorrect - Ensure locations are in the correct order
			int index = 0;
			foreach( PublicJourneyDetail pjd in endJourney.Details )
			{
				Assert.AreEqual(getTDLoc( correctLegLocations[index] ).NaPTANs[0].Naptan  ,  pjd.LegStart.Location.NaPTANs[0].Naptan, "Location "+correctLegLocations[index]+" does not match");
				index++;
				
				Assert.AreEqual(getTDLoc( correctLegLocations[index] ).NaPTANs[0].Naptan  ,  pjd.LegEnd.Location.NaPTANs[0].Naptan, "Location "+ correctLegLocations[index]+" does not match");
				index++;
			}

		}

		/// <summary>
		/// Builds an amended journey where they are to arrive earlier from the split point
		/// The original request should have the following characteristics
		/// <list type="disc">
		/// <item>route { A-B B-C C-D }</item>
		/// <item>route time is current time + 30 minutes for each leg and 30 minutes wait between each leg.</item>
		/// <item>it was planned to arrive before current time * route-length + 10 minutes</item>
		/// <item>Modes used is pure rail</item>
		/// <item>No alternative locations</item>
		/// </list>
		/// The adjusted state has catered for the following changes:
		/// <list type="disc">
		/// <item>Node 2 is selected (C)</item>
		/// <item>The change should be depart later from the selected node</item>
		/// </list>
		/// </summary>
		[Test]
		public void BuildAmendedJourneyDepartLater()
		{
			// Original journey looks like
			//
			//  ___  
			// | A | 
			//  ---  +  30 depart
			//   |
			//   |
			//  _|_  +  60 arrive
			// | B | 
			//  ---  +  90 depart
			//   |
			//   |
			//  _|_  + 120 arrive
			// | C |               Replan split point[2]
			//  ---  + 150 depart    |
			//   |                   |
			//   |                   |
			//  _|_  + 180 arrive    |
			// | D | 
			//  ---  
			// 

			// Arrive before or depart later
			bool arriveBefore = false;

			// Original journey (start and end)
			TestLocations[] startLocations = { TestLocations.A, TestLocations.B, TestLocations.C, TestLocations.D };
			// Split point
			int splitPoint = 2; // For TestLocation.C

			// The replan route we would like
			// Journey result becomes: C-E E-F F-D
			TestLocations[] replanedRoute = { TestLocations.C, TestLocations.E, TestLocations.F, TestLocations.D };

			PublicJourney endJourney = BuildAmendedJourney(startLocations, splitPoint, replanedRoute, arriveBefore );

			// Verification journey
			// Correct result should be:
			// Legs: A-B B-C C-E E-F F-D
			TestLocations[] correctLegLocations = { 
													  TestLocations.A, TestLocations.B, 
													  TestLocations.B, TestLocations.C,
													  TestLocations.C, TestLocations.E,
													  TestLocations.E, TestLocations.F,
													  TestLocations.F, TestLocations.D
												  };


			// New journey looks like
			//
			//  ___  
			// | A | 
			//  ---  +  30 depart
			//   |
			//   |
			//  _|_  +  60 arrive
			// | B | 
			//  ---  +  90 depart
			//   |
			//   |
			//  _|_  + 120 arrive
			// | C |              
			//  ---  + 150 depart - In theory depart should be +151 or greater
			//   |
			//   |
			//  _|_  + 180 arrive 
			// | E | 
			//  ---  + 210 depart
			//   |
			//   |
			//  _|_  + 240 arrive 
			// | F | 
			//  ---  + 270 depart
			//   |
			//   |
			//  _|_  + 300 arrive 
			// | D | 
			//  ---  
			// 

			// Assert adjusted journey is correct/uncorrect - Ensure locations are in the correct order
			int index = 0;
			foreach( PublicJourneyDetail pjd in endJourney.Details )
			{
				Assert.AreEqual(getTDLoc( correctLegLocations[index] ).NaPTANs[0].Naptan  ,  pjd.LegStart.Location.NaPTANs[0].Naptan, "Location "+correctLegLocations[index]+" does not match");
				index++;
				
				Assert.AreEqual(getTDLoc( correctLegLocations[index] ).NaPTANs[0].Naptan  ,  pjd.LegEnd.Location.NaPTANs[0].Naptan, "Location "+ correctLegLocations[index]+" does not match");
				index++;
			}
		}

		/// <summary>
		/// Convience method for build an amended journey from a given start location, splitpoint, replanned route and if arrive before or after in the split point
		/// </summary>
		/// <param name="startRoute">The start route</param>
		/// <param name="splitPoint">Split point is where inside the start location array we should split the journey</param>
		/// <param name="replanedRoute"></param>
		/// <param name="arriveBefore">True if it should be </param>
		private PublicJourney BuildAmendedJourney(TestLocations[] startRoute, int splitPoint, TestLocations[] replanedRoute, bool arriveBefore)
		{
			// Note all comments are based upon the following restrictions
			// Original journey will always be running from TestLocation.A enumeration and in order to end location
			
			// All example comments has the following information
			// Original journey is A-B B-C C-D. Split is at C and end journey will be
			// A-B B-C C-E E-F F-D


			// Create adjuster
			AdjustRoute adjustRoute = new AdjustRoute();

			TestLocations startLocIndex = startRoute[0];
			TestLocations stopLocIndex = startRoute[ startRoute.Length - 1];
			// TestLocations selectedNode = startLocation[splitPoint];

			// Create original journey request
			TDJourneyRequest request = new TDJourneyRequest();
			// Start
			request.OriginLocation = getTDLoc(startLocIndex);
			// Stop
			request.DestinationLocation = getTDLoc(stopLocIndex);

			// Create journey response for the original (only details are needed)
			// Original journey: A-B-C-D == length is 3 as in A-B B-C and C-D
			PublicJourneyDetail[] originalDetails = new PublicJourneyDetail[ startRoute.Length - 1 ]; 

			DateTime start = DateTime.Now;
			for(int i = 0; i < originalDetails.Length; i++)
			{
				originalDetails[i] = createLeg( ModeType.Rail, getTDLoc( startRoute[i] ), getTDLoc( startRoute[i+1] ), start, 30);
				start = start.AddMinutes( 30 );
			}

            PublicJourney originalPJ = new PublicJourney(0, originalDetails, TDJourneyType.PublicOriginal, 0);

			// Ie: A-B B-C will be the remaining journey, ie length should be equal to the splitPoint
			// int copyDirectio
			int offset = arriveBefore ? splitPoint: 0;
			int length = arriveBefore ? originalDetails.Length - offset - 1 : splitPoint;

			PublicJourneyDetail[] remainingDetails = new PublicJourneyDetail[length]; 
			System.Array.Copy( originalDetails, offset, remainingDetails, 0, length);

			// Create journey response for the new planned route
			
			// Details should be the replanned number of nodes - 1.
			PublicJourneyDetail[] replannedDetails = new PublicJourneyDetail[replanedRoute.Length - 1]; 

			// Fetch the time for the departure time at split point (ie: leg C-D)
			start = originalDetails[offset].LegStart.DepartureDateTime.GetDateTime();
			for(int i = 0; i < replannedDetails.Length; i++)
			{
				replannedDetails[ i ] = createLeg( ModeType.Rail, getTDLoc( replanedRoute[i]), getTDLoc( replanedRoute[i+1]), start, 30);
				start = start.AddMinutes( 30 );
			}


			PublicJourney pj = new PublicJourney(0,replannedDetails, TDJourneyType.PublicOriginal, 0 );

			ITDJourneyResult tdjReplannedResults = new MockJourneyResult(pj);
			// Create adjust state
			TDCurrentAdjustState adjustState = new TDCurrentAdjustState(request);

			adjustState.AmendedJourney = new PublicJourney( 0, originalDetails, TDJourneyType.PublicAmended, 0 );
			// Assign the remaining details from the original Request
			adjustState.RemainingRouteSegment = remainingDetails;
			// Assign the correct route plan (in this case depart later)
			adjustState.SelectedRouteNodeSearchType = arriveBefore ? true : false;
			// Split point: C (2)
			adjustState.SelectedRouteNode = (uint)splitPoint;

			// Splice together the journey data
			return adjustRoute.BuildAmendedJourney(originalPJ, tdjReplannedResults, adjustState );
		}

		/// <summary>
		/// Convience method fpr translating TestLocation into TDLocation
		/// </summary>
		/// <param name="loc">The TestLocation to be translated into a TDLocation</param>
		/// <returns>Appropriate TDLocation</returns>
		private TDLocation getTDLoc( TestLocations loc )
		{
			return locations[ (int)loc ];
		}

		/// <summary>
		/// Creates a public journey detail item
		/// </summary>
		/// <param name="mode">The modetype that is used for this leg</param>
		/// <param name="startLoc">The starting location as a TDLocation object</param>
		/// <param name="stopLoc">The stop location as a TDLocation object</param>
		/// <param name="start">The start time</param>
		/// <param name="minutes">The minutes given as travelling time (between start and stop)</param>
		/// <returns>Public journey details corresponding to the data</returns>
		private PublicJourneyDetail createLeg( ModeType mode, TDLocation startLoc, TDLocation stopLoc, DateTime start, int minutes)
		{
			return createLeg( mode, startLoc, stopLoc, start, minutes, null);
		}

		/// <summary>
		/// Creates a public journey detail item
		/// </summary>
		/// <param name="mode">The modetype that is used for this leg</param>
		/// <param name="startLoc">The starting location as a TDLocation object</param>
		/// <param name="stopLoc">The stop location as a TDLocation object</param>
		/// <param name="start">The start time</param>
		/// <param name="minutes">The minutes given as travelling time (between start and stop)</param>
		/// <param name="via">The via location</param>
		/// <returns>Public journey details corresponding to the data</returns>
		private PublicJourneyDetail createLeg( ModeType mode, TDLocation startLoc, TDLocation stopLoc, DateTime start, int minutes, TDLocation via)
		{
			Leg leg = new TimedLeg();

			leg.mode = ModeType.Tram;
			leg.validated = true;

			leg.board = new Event();
			leg.board.activity = ActivityType.Depart;
			leg.board.departTime = start;
			//leg.board.pass = false;
			leg.board.stop = new Stop();
			leg.board.stop.NaPTANID = startLoc.NaPTANs[0].Naptan;
			leg.board.stop.name = startLoc.Description;

			leg.alight = new Event();
			leg.alight.activity = ActivityType.Arrive;
			leg.alight.arriveTime = start.AddMinutes( minutes );
			//			leg.alight.pass = false;
			leg.alight.stop = new Stop();
			leg.alight.stop.NaPTANID = stopLoc.NaPTANs[0].Naptan;
			leg.alight.stop.name = stopLoc.Description;

			leg.destination = new Event();
			leg.destination.activity = ActivityType.Arrive;
			leg.destination.arriveTime = start.AddMinutes( minutes );
			//			leg.destination.pass = false;
			leg.destination.stop = new Stop();
			leg.destination.stop.NaPTANID = stopLoc.NaPTANs[0].Naptan;
			leg.destination.stop.name = stopLoc.Description;

			// leg.

			return PublicJourneyDetail.Create(leg, via);
		}
	}

	public class MockJourneyResult : ITDJourneyResult
	{
		private int lastReferenceSequence = 0;
		private int lastFareRequestNumber = 0;
		private int journeyReferenceNumber = 0;
		private int outwardJourneyIndex = 0;
		private PublicJourney [] outwardPublicJourney = null;
		private PublicJourney amendedOutwardPublicJourney;
		private PublicJourney amendedReturnPublicJourney;
		private ArrayList outwardPublicJourneys = new ArrayList();
		private ArrayList returnPublicJourneys = new ArrayList();
        private bool cycleAlternativeCheckDone;
        private bool cycleAlternativeAvailable;
		
		public MockJourneyResult()
		{

		}
		public MockJourneyResult(PublicJourney journey)
		{
			outwardPublicJourney = new PublicJourney[1];
			outwardPublicJourney[0] = journey;
		}

		public MockJourneyResult(string referenceNumber, PublicJourney journey)
		{
			outwardPublicJourney = new PublicJourney[1];
			outwardPublicJourney[0] = journey;
		}

        public bool CycleAlternativeCheckDone
        {
            get
            {
                return cycleAlternativeCheckDone;
            }
            set
            {
                cycleAlternativeCheckDone = value;
            }
        }

        public bool CycleAlternativeAvailable
        {
            get
            {
                return cycleAlternativeAvailable;
            }
            set
            {
                cycleAlternativeAvailable = value;
            }
        }
        
        public bool CheckForReturnOverlap(ITDJourneyRequest journeyRequest) 
		{
			return false;
		}

		/// <summary>
		/// Read-write property
		/// Exposes the last-used outward 
		/// journey index within the result
		/// </summary>
		public int OutwardJourneyIndex
		{
			get { return outwardJourneyIndex; }
			set { outwardJourneyIndex = value; } 
		}

		public bool CheckForJourneyStartInPast(ITDJourneyRequest journeyRequest) 
		{
			return false;
		}

		/// <summary>
		/// Gets the display number for the selected Outward journey
		/// </summary>
		public string OutwardDisplayNumber(int journeyIndex)
		{
			return "1";
		}

		/// <summary>
		/// Gets the display number for the selected Return journey
		/// </summary>
		public string ReturnDisplayNumber(int journeyIndex)
		{
			return "1";
		}

		public JourneySummaryLine[] OutwardJourneySummary(bool arriveBefore)
		{
			int journeyInxed = outwardPublicJourney[0].JourneyIndex;
			ModeType[] modeTypes = new ModeType[outwardPublicJourney[0].Details.GetUpperBound(0)];
			for( int i = 0; i<=modeTypes.GetUpperBound(0); i++) 
			{ 
				modeTypes[i] = outwardPublicJourney[0].Details[i].Mode; 
			}
			
			JourneySummaryLine jsl = new JourneySummaryLine(outwardPublicJourney[0].JourneyIndex, 
				TDJourneyType.PublicAmended,
				modeTypes,
				outwardPublicJourney[0].Details.GetUpperBound(0) - 1,
				outwardPublicJourney[0].Details[0].LegStart.DepartureDateTime,
				outwardPublicJourney[0].Details[0].LegEnd.ArrivalDateTime,
				0, outwardPublicJourney[0].JourneyIndex.ToString());

			return new JourneySummaryLine[] { jsl };
		}

        public JourneySummaryLine[] OutwardJourneySummary(bool arriveBefore, ModeType[] modeType)
        {
            return null;
        }

        public JourneySummaryLine[] OutwardJourneySummary(bool arriveBefore, ModeType[] modeType, bool ignoreStartLegTransferMode, bool ignoreEndLegTransferMode)
        {
            return null;
        }

		public JourneySummaryLine[] ReturnJourneySummary(bool arriveBefore)
		{
			return null;
		}

        public JourneySummaryLine[] ReturnJourneySummary(bool arriveBefore, ModeType[] modeType)
        {
            return null;
        }

        public JourneySummaryLine[] ReturnJourneySummary(bool arriveBefore, ModeType[] modeType, bool ignoreStartLegTransferMode, bool ignoreEndLegTransferMode)
        {
            return null;
        }

		public CJPMessage[] CJPMessages
		{
			get { return null; }
		}

		public int JourneyReferenceNumber
		{
			get { return journeyReferenceNumber; }
			set { journeyReferenceNumber = value; }
		}

		public int LastReferenceSequence
		{
			get { return lastReferenceSequence; }
			set { lastReferenceSequence = value; }
		}

		public int LastFareRequestNumber
		{
			get { return lastFareRequestNumber; }
			set { lastFareRequestNumber = value; }
		}

		public PublicJourney OutwardPublicJourney(int journeyIndex)
		{
			return outwardPublicJourney[journeyIndex];
		}

		public int GetSelectedOutwardJourneyIndex(int journeyIndex)
		{
			return journeyIndex;
		}

		public ArrayList OutwardPublicJourneys
		{
			get {return null;}
		}

		public PublicJourney ReturnPublicJourney(int journeyIndex)
		{
			return null;
		}

		public int GetSelectedReturnJourneyIndex(int journeyIndex)
		{
			return -1;
		}

		public ArrayList ReturnPublicJourneys
		{
			get {return null;}
		}

		public RoadJourney OutwardRoadJourney()
		{
			return null;
		}

		public RoadJourney ReturnRoadJourney()
		{
			return null;
		}

		public PublicJourney AmendedOutwardPublicJourney
		{
			get { return amendedOutwardPublicJourney; }
			set { amendedOutwardPublicJourney = value; }
		}

		public PublicJourney AmendedReturnPublicJourney
		{
			get { return amendedReturnPublicJourney; }
			set { amendedReturnPublicJourney= value; }
		}

		public int OutwardPublicJourneyCount 
		{ 
			get
			{
				return 0;
			} 
		}
		
		public int ReturnPublicJourneyCount
		{ 
			get
			{
				return 0;
			} 
		}

		public int OutwardRoadJourneyCount
		{ 
			get
			{
				return 0;
			} 
		}

		public int ReturnRoadJourneyCount
		{ 
			get
			{
				return 0;
			} 
		}

		public bool IsValid
		{
			get 
			{
				return true;
			}
			set
			{
			}
		}

		public void ClearMessages()
		{
		}

		public void AddMessageToArray(string description, string resourceId, int majorCode, int minorCode)
		{
		}

		public void AddMessageToArray(string description, string resourceId, int majorCode, int minorCode, ErrorsType type)
		{
		}

		public void AddPublicJourney(PublicJourney publicJourney, bool outward)
		{
		}

		public void AddPublicJourney(PublicJourney publicJourney, bool outward, int index)
		{
		}

        /// <summary>
        /// Add a PublicJourney to a TDResult.  The outward flag determines where the information will
        /// be added.
        /// </summary>
        /// <param name="PublicJourney">The input PublicJouney</param>
        /// <param name="outward">Should this go into the outward or return arrays</param>
        public void AddPublicJourney(PublicJourney publicJourney, bool outward, bool retainJourneyDate, bool retainJourneyDuration)
        {
        }

        /// <summary>
        /// Empty stub method for add road journey to the result
        /// </summary>
        /// <param name="roadJourney"></param>
        /// <param name="outward"></param>
        /// <param name="isNewRouteNumber"></param>
        public void AddRoadJourney(RoadJourney roadJourney, bool outward, bool isNewRouteNumber)
        {
        }

		public void UpdateOutwardRoadJourney(RoadJourney roadJourney)
		{
		}

		public void UpdateReturnRoadJourney(RoadJourney roadJourney)
		{
		}
	}
}