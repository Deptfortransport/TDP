// *********************************************** 
// NAME			: TestTDMapHandoff.cs
// AUTHOR		: James Cotton
// DATE CREATED	: 29/08/2003 
// DESCRIPTION	: Implementation of the TestTDMapHandoff class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestTDMapHandoff.cs-arc  $
//
//   Rev 1.2   Dec 05 2012 14:12:50   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Oct 12 2009 09:11:00   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 12 2009 08:39:46   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Nov 08 2007 12:24:22   mturner
//Initial revision.
//
//   Rev 1.26   Mar 30 2006 15:47:54   mguney
//TDJourneyResult construction changed in TestAppendRoadJourney and in TestUpdateRoadJourney.
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.25   Mar 30 2006 13:33:16   mguney
//Manual merge for stream0018
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.24   Mar 13 2006 14:24:54   NMoorhouse
//Manual merge of stream3353 -> trunk
//
//   Rev 1.23   Feb 09 2006 17:49:04   jmcallister
//Project Newkirk
//
//   Rev 1.22.1.1   Mar 02 2006 17:40:40   NMoorhouse
//Extra param to hold private via location
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.22.1.0   Jan 26 2006 20:16:24   rhopkins
//Pass new attributes in constructor for TDJourneyResult
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.22   Aug 25 2005 10:19:20   rgreenwood
//Added Test Case 9 to AppendPublicJourney() method
//Resolution for 2662: DN062:  Map with non-consecutive nodes with no co-ordinates functioning incorrectly
//
//   Rev 1.21   Aug 25 2005 08:57:00   rgreenwood
//Updated existing test cases 1-4 in TestAppendPublicJourney(). Added 4 extra test cases for IR 2662 to same test method.
//Resolution for 2662: DN062:  Map with non-consecutive nodes with no co-ordinates functioning incorrectly
//
//   Rev 1.19   Aug 19 2005 14:04:34   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.18.1.0   Aug 01 2005 18:16:32   rgreenwood
//DD073 Map Details: Updated tests for modified AppendPublicJourney method in TDMapHandoff class
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.18   Mar 22 2005 10:28:00   jbroome
//Added TestPublicTransportResult test method
//
//   Rev 1.17   Jan 18 2005 15:56:32   rhopkins
//Removed dependancy on other tests so that this test will work when TestCarCostCalculator is present.
//Also changed Assertion to Assert.
//
//   Rev 1.16   Nov 26 2004 13:52:00   jbroome
//DEL6.3.1. Motorway Junctions enhancements
//
//   Rev 1.15   Oct 27 2004 11:16:30   jbroome
//Removed references to Toll property of CJP DriveSection class for DEL 7 CJP interface.
//
//   Rev 1.14   Sep 17 2004 15:13:06   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.13   Jul 28 2004 10:54:08   CHosegood
//Updated to compile against CJP 6.0.0.0
//NOT TESTED!!!
//
//   Rev 1.12   Jun 16 2004 14:44:08   CHosegood
//using TransportDirect.Common.DatabaseInfrastructure instead of TransportDirect.Common.DatabaseInfrastructure_Nunit
//
//   Rev 1.11   Apr 21 2004 11:42:10   CHosegood
//Added RailReplacementBus journey leg to test.
//
//   Rev 1.10   Nov 21 2003 16:29:40   PNorell
//IR305 - Unadjusted journeys saved with correct congestion number.
//
//   Rev 1.9   Nov 06 2003 16:27:32   PNorell
//Ensured test work properly.
//
//   Rev 1.8   Oct 15 2003 21:55:40   acaunt
//Destinations added to the leg data
//
//   Rev 1.7   Sep 25 2003 11:44:42   RPhilpott
//Map handoff and MI logging changes
//
//   Rev 1.6   Sep 23 2003 14:06:32   RPhilpott
//Changes to TDJourneyResult (ctor and referenceNumber)
//
//   Rev 1.5   Sep 10 2003 11:14:00   RPhilpott
//Changes to CJPMessage handling.
//
//   Rev 1.4   Sep 09 2003 13:37:52   RPhilpott
//Move Custom Events from TDPCustomEvents to JourneyControl to avoid circular dependency.
//
//   Rev 1.3   Sep 08 2003 15:57:14   jcotton
//Final integration with TDEventLogging Service
//
//   Rev 1.2   Sep 02 2003 12:43:28   kcheung
//Updated becase TDJourneyResult interface Road methods were updated
//
//   Rev 1.1   Sep 02 2003 09:11:44   jcotton
//Minor modifications to meet FXCop requirements
//
//   Rev 1.0   Sep 01 2003 16:29:34   jcotton
//Initial Revision
//

using NUnit.Framework;

using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.UserPortal.LocationService;

using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Test class which tests the TDMapHandoff class functionality
	/// </summary>
	[TestFixture]
	public class TestTDMapHandoff
	{
		private ITDMapHandoff TDMapHandoffToTest;
		const string stringCase1 = "<journey routenum=\"1\"><leg no=\"1\" mode=\"GREYEDOUT\"><geometry order=\"1\" easting=\"0\" northing=\"0\" /><geometry order=\"2\" easting=\"-1\" northing=\"-1\" /></leg></journey>";
		const string stringCase2 = "<journey routenum=\"1\"><leg no=\"1\" mode=\"Rail\"><geometry order=\"1\" easting=\"2\" northing=\"2\" /><geometry order=\"2\" easting=\"3\" northing=\"3\" /></leg><leg no=\"2\" mode=\"GREYEDOUT\"><geometry order=\"1\" easting=\"3\" northing=\"3\" /><geometry order=\"2\" easting=\"6\" northing=\"6\" /></leg><leg no=\"3\" mode=\"Rail\"><geometry order=\"1\" easting=\"6\" northing=\"6\" /><geometry order=\"2\" easting=\"7\" northing=\"7\" /></leg></journey>";
		const string stringCase3 = "<journey routenum=\"1\"><leg no=\"1\" mode=\"Rail\"><geometry order=\"1\" easting=\"2\" northing=\"2\" /><geometry order=\"2\" easting=\"3\" northing=\"3\" /></leg><leg no=\"2\" mode=\"Rail\"><geometry order=\"1\" easting=\"3\" northing=\"3\" /><geometry order=\"2\" easting=\"6\" northing=\"6\" /></leg><leg no=\"3\" mode=\"Rail\"><geometry order=\"1\" easting=\"6\" northing=\"6\" /><geometry order=\"2\" easting=\"7\" northing=\"7\" /></leg></journey>";
		const string stringCase4 = "<journey routenum=\"1\"><leg no=\"1\" mode=\"Rail\"><geometry order=\"1\" easting=\"2\" northing=\"2\" /><geometry order=\"2\" easting=\"3\" northing=\"3\" /></leg><leg no=\"2\" mode=\"Rail\"><geometry order=\"1\" easting=\"6\" northing=\"6\" /><geometry order=\"2\" easting=\"7\" northing=\"7\" /></leg></journey>";
		const string stringCase5 = "<journey routenum=\"1\"><leg no=\"1\" mode=\"Rail\"><geometry order=\"1\" easting=\"2\" northing=\"2\" /><geometry order=\"2\" easting=\"3\" northing=\"3\" /></leg><leg no=\"2\" mode=\"Rail\"><geometry order=\"1\" easting=\"6\" northing=\"6\" /><geometry order=\"2\" easting=\"7\" northing=\"7\" /></leg><leg no=\"3\" mode=\"Rail\"><geometry order=\"1\" easting=\"10\" northing=\"10\" /><geometry order=\"2\" easting=\"11\" northing=\"11\" /></leg></journey>";
		const string stringCase6 = "<journey routenum=\"1\"><leg no=\"1\" mode=\"Rail\"><geometry order=\"1\" easting=\"2\" northing=\"2\" /><geometry order=\"2\" easting=\"3\" northing=\"3\" /></leg><leg no=\"2\" mode=\"Rail\"><geometry order=\"1\" easting=\"3\" northing=\"3\" /><geometry order=\"2\" easting=\"7\" northing=\"7\" /></leg><leg no=\"3\" mode=\"Rail\"><geometry order=\"1\" easting=\"10\" northing=\"10\" /><geometry order=\"2\" easting=\"11\" northing=\"11\" /></leg></journey>";
		const string stringCase7 = "<journey routenum=\"1\"><leg no=\"1\" mode=\"Rail\"><geometry order=\"1\" easting=\"2\" northing=\"2\" /><geometry order=\"2\" easting=\"3\" northing=\"3\" /></leg><leg no=\"2\" mode=\"Rail\"><geometry order=\"1\" easting=\"6\" northing=\"6\" /><geometry order=\"2\" easting=\"10\" northing=\"10\" /></leg><leg no=\"3\" mode=\"Rail\"><geometry order=\"1\" easting=\"10\" northing=\"10\" /><geometry order=\"2\" easting=\"11\" northing=\"11\" /></leg></journey>";
		const string stringCase8 = "<journey routenum=\"1\"><leg no=\"1\" mode=\"Rail\"><geometry order=\"1\" easting=\"2\" northing=\"2\" /><geometry order=\"2\" easting=\"3\" northing=\"3\" /></leg><leg no=\"2\" mode=\"Rail\"><geometry order=\"1\" easting=\"3\" northing=\"3\" /><geometry order=\"2\" easting=\"10\" northing=\"10\" /></leg><leg no=\"3\" mode=\"Rail\"><geometry order=\"1\" easting=\"10\" northing=\"10\" /><geometry order=\"2\" easting=\"11\" northing=\"11\" /></leg></journey>";
		const string stringCase9 = "<journey routenum=\"1\"><leg no=\"1\" mode=\"Rail\"><geometry order=\"1\" easting=\"2\" northing=\"2\" /><geometry order=\"2\" easting=\"3\" northing=\"3\" /></leg><leg no=\"2\" mode=\"GREYEDOUT\"><geometry order=\"1\" easting=\"6\" northing=\"6\" /><geometry order=\"2\" easting=\"11\" northing=\"11\" /></leg></journey>";
		
		/// <summary>
		/// Constructor
		/// </summary>
		public TestTDMapHandoff()
		{

		}

		/// <summary>
		/// Sets up necessary services for use in tests
		/// </summary>
		[SetUp]
		public void Init()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestJourneyInitialisation());
			// Enable PropertyService
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

			// Enable the map hand off
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.TDMapHandoff, new TDMapHandoffFactory());

			// Store it locally
			TDMapHandoffToTest = (ITDMapHandoff)TDServiceDiscovery.Current[ServiceDiscoveryKey.TDMapHandoff];

			// create custom email publisher
			IEventPublisher[] customPublishers = new IEventPublisher[0];	

			Trace.Listeners.Remove("TDTraceListener");
			// create and add TDTraceListener instance to the listener collection	
			ArrayList errors = new ArrayList();
			Trace.Listeners.Add( new TDTraceListener(Properties.Current, customPublishers, errors ));
		}

		/// <summary>
		/// Public Journey, Append
		/// </summary>
		[Test]
		public void TestAppendPublicTransportJourney()
		{
			bool saveResult;
			TDJourneyResult TestResult = new TDJourneyResult(1234);
//			TestResult.AddResult(CreateCJPResult(true), true, null, "ssss");
			TestResult.AddResult(CreateCJPResult(true), true, null, null, null, null, "ssss", false, -1);
			PublicJourney testJourney;
            
			testJourney = TestResult.OutwardPublicJourney(0);
			outputOSgridData( testJourney );
			saveResult = TDMapHandoffToTest.SaveJourneyResult( "PT Append 1", testJourney );
			Assert.AreEqual(true, saveResult, "TDMapHandOff: SQL Error" );

			testJourney = TestResult.OutwardPublicJourney(1);
			outputOSgridData( testJourney );
			saveResult = TDMapHandoffToTest.SaveJourneyResult( "PT Append 2", testJourney );
			Assert.AreEqual(true, saveResult, "TDMapHandOff: SQL Error" );

		}

		/// <summary>
		/// Public Journey, update
		/// </summary>
		[Test]
		public void TestUpdatePublicTransportJourney()
		{
			bool saveResult;
			TDJourneyResult TestResult = new TDJourneyResult(1234);
			TestResult.AddResult(CreateCJPResult(true), true, null, null, null, null, "ssss", false, -1);
			PublicJourney testJourney;
            
			testJourney = TestResult.OutwardPublicJourney(0);
			saveResult = TDMapHandoffToTest.SaveJourneyResult( "PT Update 1", testJourney );
			Assert.AreEqual(true, saveResult, "TDMapHandOff: SQL Error" );

			testJourney = TestResult.OutwardPublicJourney(1);
			saveResult = TDMapHandoffToTest.SaveJourneyResult( "PT Update 2", testJourney );
			Assert.AreEqual(true, saveResult, "TDMapHandOff: SQL Error" );
		}

		/// <summary>
		/// Public journeys stored in TDJourneyResult
		/// </summary>
		[Test]
		public void TestPublicTransportResult()
		{
			bool saveResult;
			TDJourneyResult TestResult = new TDJourneyResult(1234, 9, new TDDateTime(), new TDDateTime(), false, false, false);
//			TestResult.AddResult(CreateCJPResult(true), true, null, "ssss");
			TestResult.AddResult(CreateCJPResult(true), true, null, null, null, null, "ssss", false, -1);
			// Test with one outward public journey
			saveResult = TDMapHandoffToTest.SaveJourneyResult(TestResult, "ssss");
			Assert.AreEqual(true, saveResult, "TDMapHandOff: SQL Error");

			// Add return public journey
//			TestResult.AddResult(CreateCJPResult(true), false, null, "ssss");
			TestResult.AddResult(CreateCJPResult(true), false, null, null, null, null, "ssss", false, -1);
			saveResult = TDMapHandoffToTest.SaveJourneyResult(TestResult, "ssss");
			Assert.AreEqual(true, saveResult, "TDMapHandOff: SQL Error");

			// Add an outward road journey - this will be ignored
//			TestResult.AddResult(CreateCJPResult(false), true, null, "ssss");
			TestResult.AddResult(CreateCJPResult(false), true, null, null, null, null, "ssss", false, -1);
			saveResult = TDMapHandoffToTest.SaveJourneyResult(TestResult, "ssss");
			Assert.AreEqual(true, saveResult, "TDMapHandOff: SQL Error");

			// Add an return road journey - this will be ignored
//			TestResult.AddResult(CreateCJPResult(false), false, null, "ssss");
			TestResult.AddResult(CreateCJPResult(false), false, null, null, null, null, "ssss", false, -1);
			saveResult = TDMapHandoffToTest.SaveJourneyResult(TestResult, "ssss");
			Assert.AreEqual(true, saveResult, "TDMapHandOff: SQL Error");

		}

		/// <summary>
		/// Road Journey, Append
		/// </summary>
		[Test]
		public void TestAppendRoadJourney()
		{
            TDJourneyResult TestResult = new TDJourneyResult(1234, 10, new TDDateTime(5, 5, 5), new TDDateTime(5, 5, 6), true, false, false);
			TestResult.AddResult(CreateCJPResult(false), true, null, null, null, null, "ssss", false, -1);
			RoadJourney TestJourney = TestResult.OutwardRoadJourney();
			ITNLink[] itnLinks = createITNLink();
			bool saveResult;
			saveResult = TDMapHandoffToTest.SaveJourneyResult( true, "RD Append 1", 
				TestJourney.RouteNum,
				itnLinks );
			Assert.AreEqual(true, saveResult, "TDMapHandOff: SQL Error" );

			saveResult = TDMapHandoffToTest.SaveJourneyResult( false, "RD Append 2", 
				TestJourney.RouteNum,
				itnLinks );
			Assert.AreEqual(true, saveResult, "TDMapHandOff: SQL Error" );
		}
		
		/// <summary>
		/// Road Journey, Update
		/// </summary>
		[Test]
		public void TestUpdateRoadJourney()
		{
            TDJourneyResult TestResult = new TDJourneyResult(1234, 10, new TDDateTime(5, 5, 5), new TDDateTime(5, 5, 6), true, false, false);
			TestResult.AddResult(CreateCJPResult(false), true, null, null, null, null, "ssss", false, -1);
			RoadJourney TestJourney = TestResult.OutwardRoadJourney();
			ITNLink[] itnLinks = createITNLink();
			bool saveResult;
			saveResult = TDMapHandoffToTest.SaveJourneyResult( true, "RD Update 1", 
				TestJourney.RouteNum,
				itnLinks );
			Assert.AreEqual(true, saveResult, "TDMapHandOff: SQL Error" );

			saveResult = TDMapHandoffToTest.SaveJourneyResult( false, "RD Update 1", 
				TestJourney.RouteNum,
				itnLinks );
			Assert.AreEqual(true, saveResult, "TDMapHandOff: SQL Error" );
		}
		
		/// <summary>
		/// Tests DD073 Changes for Map Details & IR 2662 fix/enhancement
		/// </summary>
		[Test]
		public void TestAppendPublicJourney()
		{
			//TEST CASE 1 - leg.HasInvalidCoordinates = true, greyedOut = false
			//Create invalid Leg
			Leg invalidLegCase1 = CreateInvalidLeg(1,1); //Valid start, invalid end. HasInvalidCoordinates = true, greyedOut = false

			//Create PublicJourney Object from the Details object
			PublicJourneyDetail[] pjdCase1 = new PublicJourneyDetail[1];
			pjdCase1[0] = PublicJourneyDetail.Create(invalidLegCase1, null);

			//Construct the PublicJourney
			PublicJourney pjCase1 = new PublicJourney(1, pjdCase1, TDJourneyType.PublicOriginal, 1);

			//Test with PublicJourney
			StringBuilder xmlCase1 = new StringBuilder();
			TDMapHandoffToTest.AppendPublicJourney(xmlCase1, pjCase1);

			//Test XML content in the stringbuilder class
			string s1 = xmlCase1.ToString();

			Assert.AreEqual(stringCase1, s1);

	
			//TEST CASE 2 - leg.HasInvalidCoordinates = true, greyedOut = true
			//Create invalid Leg
			Leg validLeg1 = CreateValidLeg(1,1); //Completely valid. HasInvalidCoordinates = false, greyedOut = false
			Leg invalidLeg1 = CreateInvalidLeg(0,0); //Completely invalid. HasInvalidCoordinates = true, greyedOut = false (from previous leg)
			Leg invalidLeg2 = CreateInvalidLeg(-4,-4); //Completely invalid. HasInvalidCoordinates = true, greyedOut = true (from previous leg)
			Leg validLeg2 = CreateValidLeg(5,5); //Completely valid. HasInvalidCoordinates = false, greyedOut = true (from previous leg)

			//Create PublicJourney Object from the Details object
			PublicJourneyDetail[] pjdCase2 = new PublicJourneyDetail[4];
			pjdCase2[0] = PublicJourneyDetail.Create(validLeg1, null);
			pjdCase2[1] = PublicJourneyDetail.Create(invalidLeg1, null);
			pjdCase2[2] = PublicJourneyDetail.Create(invalidLeg2, null);
			pjdCase2[3] = PublicJourneyDetail.Create(validLeg2, null);

			//Construct the PublicJourney
			PublicJourney pjCase2 = new PublicJourney(1, pjdCase2, TDJourneyType.PublicOriginal, 1);

			//Test with PublicJourney
			StringBuilder xmlCase2 = new StringBuilder();
			TDMapHandoffToTest.AppendPublicJourney(xmlCase2, pjCase2);

			//Test XML content in the stringbuilder class
			string s2 = xmlCase2.ToString();

			Assert.AreEqual(stringCase2, s2);
			

			//TEST CASE 3 - leg.HasInvalidCoordinates = false, greyedOut = true
			//Create valid single Leg(s)
			Leg validLeg4 = CreateValidLeg(1,1); //Completely valid. HasInvalidCoordinates = false, greyedOut = false
			Leg invalidLeg3 = CreateInvalidLeg(2,0); //Valid start point, invalid end. HasInvalidCoordinates = true, greyedOut = false
			Leg validLeg5 = CreateValidLeg(5,5); //Completely valid. so HasInvalidCoordinates = false, but greyedout = true (from previous leg)

			//Create PublicJourney Object from the Details object
			PublicJourneyDetail[] pjdCase3 = new PublicJourneyDetail[3];
			pjdCase3[0] = PublicJourneyDetail.Create(validLeg4, null);
			pjdCase3[1] = PublicJourneyDetail.Create(invalidLeg3, null);
			pjdCase3[2] = PublicJourneyDetail.Create(validLeg5, null);

			//Construct the PublicJourney
			PublicJourney pjCase3 = new PublicJourney(1, pjdCase3, TDJourneyType.PublicOriginal, 1);

			//Test with PublicJourney
			StringBuilder xmlCase3 = new StringBuilder();
			TDMapHandoffToTest.AppendPublicJourney(xmlCase3, pjCase3);

			//Test XML content in the stringbuilder class
			string s3 = xmlCase3.ToString();

			Assert.AreEqual(stringCase3, s3);


			//TEST CASE 4 - leg.HasInvalidCoordinates = false, greyedOut = false 
			//Create valid single Leg(s)
			Leg validLeg6 = CreateValidLeg(1,1); //Completely valid. HasInvalidCoordinates = false, greyedOut = false
			Leg validLeg7 = CreateValidLeg(5,5); //Completely valid. HasInvalidCoordinates = false, greyedOut = false

			//Create PublicJourney Object from the Details object
			PublicJourneyDetail[] pjdCase4 = new PublicJourneyDetail[2];
			pjdCase4[0] = PublicJourneyDetail.Create(validLeg6, null);
			pjdCase4[1] = PublicJourneyDetail.Create(validLeg7, null);

			//Construct the PublicJourney
			PublicJourney pjCase4 = new PublicJourney(1, pjdCase4, TDJourneyType.PublicOriginal, 1);

			//Test with PublicJourney
			StringBuilder xmlCase4 = new StringBuilder();
			TDMapHandoffToTest.AppendPublicJourney(xmlCase4, pjCase4);

			//Test XML content in the stringbuilder class
			string s4 = xmlCase4.ToString();

			Assert.AreEqual(stringCase4, s4);

			//TEST CASE 5 - IR2662
			//3-leg journey
			//Leg 1: Valid Leg (Journey) Start and Leg End grid ref
			//Leg 2: Valid Leg Start grid ref, valid Leg End grid ref
			//Leg 3: Valid Leg Start grid ref, valid Leg End grid ref
			//Expected outcome: Normal processing (do nothing), all grid refs are valid

			//Create the journey
			Leg variableLeg1 = CreateLegWithVaribleValidity(1,2,3,4); //Completely valid Leg Start and Leg End coordinates
			Leg variableLeg2 = CreateLegWithVaribleValidity(5,6,7,8); //Completely valid Leg Start and Leg End coordinates
			Leg variableLeg3 = CreateLegWithVaribleValidity(9,10,11,12); //Completely valid Leg Start and Leg End coordinates

			//Create PublicJourney Object from the Details object
			PublicJourneyDetail[] pjdCase5 = new PublicJourneyDetail[3];
			pjdCase5[0] = PublicJourneyDetail.Create(variableLeg1, null);
			pjdCase5[1] = PublicJourneyDetail.Create(variableLeg2, null);
			pjdCase5[2] = PublicJourneyDetail.Create(variableLeg3, null);

			//Construct the PublicJourney
			PublicJourney pjCase5 = new PublicJourney(1, pjdCase5, TDJourneyType.PublicOriginal, 1);

			//Test with PublicJourney
			StringBuilder xmlCase5 = new StringBuilder();
			TDMapHandoffToTest.AppendPublicJourney(xmlCase5, pjCase5);

			//Test XML content in the stringbuilder class
			string s5 = xmlCase5.ToString();

			Assert.AreEqual(stringCase5, s5);

			//TEST CASE 6 - IR2662
			//3-leg journey
			//Leg 1: Valid Leg (Journey) Start and Leg End grid ref
			//Leg 2: Invalid Leg Start grid ref, valid Leg End grid ref
			//Leg 3: Valid Leg Start grid ref, valid Leg End grid ref
			//Expected outcome: Substitute the invalid Leg 2 Leg Start grid ref with the valid Leg 1 Leg End grid ref

			//Create the journey
			Leg variableLeg4 = CreateLegWithVaribleValidity(1,2,3,4); //Completely valid Leg Start and Leg End coordinates
			Leg variableLeg5 = CreateLegWithVaribleValidity(5,0,7,8); //Invalid Leg Start (board), valid Leg End coordinates
			Leg variableLeg6 = CreateLegWithVaribleValidity(9,10,11,12); //Completely valid Leg Start and Leg End coordinates

			//Create PublicJourney Object from the Details object
			PublicJourneyDetail[] pjdCase6 = new PublicJourneyDetail[3];
			pjdCase6[0] = PublicJourneyDetail.Create(variableLeg4, null);
			pjdCase6[1] = PublicJourneyDetail.Create(variableLeg5, null);
			pjdCase6[2] = PublicJourneyDetail.Create(variableLeg6, null);

			//Construct the PublicJourney
			PublicJourney pjCase6 = new PublicJourney(1, pjdCase6, TDJourneyType.PublicOriginal, 1);

			//Test with PublicJourney
			StringBuilder xmlCase6 = new StringBuilder();
			TDMapHandoffToTest.AppendPublicJourney(xmlCase6, pjCase6);

			//Test XML content in the stringbuilder class
			string s6 = xmlCase6.ToString();

			Assert.AreEqual(stringCase6, s6);


			//TEST CASE 7 - IR2662
			//3-leg journey
			//Leg 1: Valid Leg (Journey) Start and Leg End grid ref
			//Leg 2: Valid Leg Start grid ref, invalid Leg End grid ref
			//Leg 3: Valid Leg Start grid ref, valid Leg End grid ref
			//Expected outcome: Substitute the invalid Leg 2 Leg End grid ref with the valid Leg 3 Leg Start grid ref

			//Create the journey
			Leg variableLeg7 = CreateLegWithVaribleValidity(1,2,3,4); //Completely valid Leg Start and Leg End coordinates
			Leg variableLeg8 = CreateLegWithVaribleValidity(5,6,0,8); //Valid Leg Start (board), invalid Leg End coordinates
			Leg variableLeg9 = CreateLegWithVaribleValidity(9,10,11,12); //Completely valid Leg Start and Leg End coordinates

			//Create PublicJourney Object from the Details object
			PublicJourneyDetail[] pjdCase7 = new PublicJourneyDetail[3];
			pjdCase7[0] = PublicJourneyDetail.Create(variableLeg7, null);
			pjdCase7[1] = PublicJourneyDetail.Create(variableLeg8, null);
			pjdCase7[2] = PublicJourneyDetail.Create(variableLeg9, null);

			//Construct the PublicJourney
			PublicJourney pjCase7 = new PublicJourney(1, pjdCase7, TDJourneyType.PublicOriginal, 1);

			//Test with PublicJourney
			StringBuilder xmlCase7 = new StringBuilder();
			TDMapHandoffToTest.AppendPublicJourney(xmlCase7, pjCase7);

			//Test XML content in the stringbuilder class
			string s7 = xmlCase7.ToString();

			Assert.AreEqual(stringCase7, s7);

			//TEST CASE 8 - IR2662
			//3-leg journey
			//Leg 1: Valid Leg (Journey) Start and Leg End grid ref
			//Leg 2: Invalid Leg Start grid ref, invalid Leg End grid ref
			//Leg 3: Valid Leg Start grid ref, valid Leg End grid ref
			//Expected outcome: Substitution not possible as both the necessary grid refs are invalid, so grey out both legs.

			//Create the journey
			Leg variableLeg10 = CreateLegWithVaribleValidity(1,2,3,4); //Completely valid Leg Start and Leg End coordinates
			Leg variableLeg11 = CreateLegWithVaribleValidity(5,0,0,8); //Valid Leg Start (board), invalid Leg End coordinates
			Leg variableLeg12 = CreateLegWithVaribleValidity(9,10,11,12); //Completely valid Leg Start and Leg End coordinates

			//Create PublicJourney Object from the Details object
			PublicJourneyDetail[] pjdCase8 = new PublicJourneyDetail[3];
			pjdCase8[0] = PublicJourneyDetail.Create(variableLeg10, null);
			pjdCase8[1] = PublicJourneyDetail.Create(variableLeg11, null);
			pjdCase8[2] = PublicJourneyDetail.Create(variableLeg12, null);

			//Construct the PublicJourney
			PublicJourney pjCase8 = new PublicJourney(1, pjdCase8, TDJourneyType.PublicOriginal, 1);

			//Test with PublicJourney
			StringBuilder xmlCase8 = new StringBuilder();
			TDMapHandoffToTest.AppendPublicJourney(xmlCase8, pjCase8);

			//Test XML content in the stringbuilder class
			string s8 = xmlCase8.ToString();

			Assert.AreEqual(stringCase8, s8);
			
			//TEST CASE 9 - IR2662
			//3-leg journey
			//Leg 1: Valid Leg (Journey) Start and Leg End grid ref
			//Leg 2: Valid Leg Start grid ref, invalid Leg End grid ref
			//Leg 3: Invalid Leg Start grid ref, valid Leg End grid ref
			//Expected outcome: Substitution not possible as both the necessary grid refs are invalid, so grey out both legs.

			//Create the journey
			Leg variableLeg13 = CreateLegWithVaribleValidity(1,2,3,4); //Completely valid Leg Start and Leg End coordinates
			Leg variableLeg14 = CreateLegWithVaribleValidity(5,6,0,8); //Valid Leg Start, invalid Leg End coordinates
			Leg variableLeg15 = CreateLegWithVaribleValidity(9,0,11,12); //Invalid Leg Start, valid Leg End coordinates

			//Create PublicJourney Object from the Details object
			PublicJourneyDetail[] pjdCase9 = new PublicJourneyDetail[3];
			pjdCase9[0] = PublicJourneyDetail.Create(variableLeg13, null);
			pjdCase9[1] = PublicJourneyDetail.Create(variableLeg14, null);
			pjdCase9[2] = PublicJourneyDetail.Create(variableLeg15, null);

			//Construct the PublicJourney
			PublicJourney pjCase9 = new PublicJourney(1, pjdCase9, TDJourneyType.PublicOriginal, 1);

			//Test with PublicJourney
			StringBuilder xmlCase9 = new StringBuilder();
			TDMapHandoffToTest.AppendPublicJourney(xmlCase9, pjCase9);

			//Test XML content in the stringbuilder class
			string s9 = xmlCase9.ToString();

			Assert.AreEqual(stringCase9, s9);
		}

		/// <summary>
		/// Creates a leg object with origin, board, alight and destination points, all with invalid coordinates (zero or less)
		/// </summary>
		/// <returns>CJP Leg object</returns>
		public Leg CreateInvalidLeg(int i, int j)
		{
			//Create the CJP legs, based upon test conditions
			int eastingCoordinate = i;
			int northingCoordinate = j;
			Leg invalidSingleLeg = new TimedLeg();
			DateTime timeNow = DateTime.Now;

			invalidSingleLeg.mode = ModeType.Rail;
			invalidSingleLeg.validated = true;

			invalidSingleLeg.origin = new Event();
			invalidSingleLeg.origin.activity = ActivityType.Depart;
			invalidSingleLeg.origin.departTime = timeNow;
			invalidSingleLeg.origin.stop  = new Stop();
			invalidSingleLeg.origin.stop.NaPTANID = "Board NAPTANID";
			invalidSingleLeg.origin.stop.name = "Board name";

			invalidSingleLeg.origin.stop.coordinate = new Coordinate();
			invalidSingleLeg.origin.stop.coordinate.easting = i;
			invalidSingleLeg.origin.stop.coordinate.northing = j;
			i--;
			j--;

			invalidSingleLeg.board = new Event();
			invalidSingleLeg.board.activity = ActivityType.Depart;
			invalidSingleLeg.board.departTime = timeNow;
			invalidSingleLeg.board.stop = new Stop();
			invalidSingleLeg.board.stop.NaPTANID = "Board NAPTANID";
			invalidSingleLeg.board.stop.name = "Board name";

			invalidSingleLeg.board.stop.coordinate = new Coordinate();
			invalidSingleLeg.board.stop.coordinate.easting = i;
			invalidSingleLeg.board.stop.coordinate.northing = j;
			i--;
			j--;

			invalidSingleLeg.alight = new Event();
			invalidSingleLeg.alight.activity = ActivityType.Arrive;
			invalidSingleLeg.alight.arriveTime = timeNow.AddMinutes(30);
			invalidSingleLeg.alight.stop = new Stop();
			invalidSingleLeg.alight.stop.NaPTANID = "Alight NAPTANID";
			invalidSingleLeg.alight.stop.name = "Alight name";

			invalidSingleLeg.alight.stop.coordinate = new Coordinate();
			invalidSingleLeg.alight.stop.coordinate.easting = i;
			invalidSingleLeg.alight.stop.coordinate.northing = j;
			i--;
			j--;

			invalidSingleLeg.destination = new Event();
			invalidSingleLeg.destination.activity = ActivityType.Arrive;
			invalidSingleLeg.destination.arriveTime = timeNow.AddMinutes(30);
			invalidSingleLeg.destination.stop = new Stop();
			invalidSingleLeg.destination.stop.NaPTANID = "Destination NAPTANID";
			invalidSingleLeg.destination.stop.name = "Destination name";

			invalidSingleLeg.destination.stop.coordinate = new Coordinate();
			invalidSingleLeg.destination.stop.coordinate.easting = i;
			invalidSingleLeg.destination.stop.coordinate.northing = j;

			return invalidSingleLeg;
		}

		/// <summary>
		/// Creates a leg object with origin, board, alight and destination points, all with invalid coordinates (zero or less)
		/// </summary>
		/// <returns>CJP Leg object</returns>
		public Leg CreateValidLeg(int i, int j)
		{
			//Create the CJP legs, based upon test conditions
			int eastingCoordinate = i;
			int northingCoordinate = j;
			Leg validLeg = new TimedLeg();
			DateTime timeNow = DateTime.Now;

			validLeg.mode = ModeType.Rail;
			validLeg.validated = true;

			validLeg.origin = new Event();
			validLeg.origin.activity = ActivityType.Depart;
			validLeg.origin.departTime = timeNow;
			validLeg.origin.stop  = new Stop();
			validLeg.origin.stop.NaPTANID = "Board NAPTANID";
			validLeg.origin.stop.name = "Board name";

			validLeg.origin.stop.coordinate = new Coordinate();
			validLeg.origin.stop.coordinate.easting = i;
			validLeg.origin.stop.coordinate.northing = j;
			i++;
			j++;

			validLeg.board = new Event();
			validLeg.board.activity = ActivityType.Depart;
			validLeg.board.departTime = timeNow;
			validLeg.board.stop = new Stop();
			validLeg.board.stop.NaPTANID = "Board NAPTANID";
			validLeg.board.stop.name = "Board name";

			validLeg.board.stop.coordinate = new Coordinate();
			validLeg.board.stop.coordinate.easting = i;
			validLeg.board.stop.coordinate.northing = j;
			i++;
			j++;

			validLeg.alight = new Event();
			validLeg.alight.activity = ActivityType.Arrive;
			validLeg.alight.arriveTime = timeNow.AddMinutes(30);
			validLeg.alight.stop = new Stop();
			validLeg.alight.stop.NaPTANID = "Alight NAPTANID";
			validLeg.alight.stop.name = "Alight name";

			validLeg.alight.stop.coordinate = new Coordinate();
			validLeg.alight.stop.coordinate.easting = i;
			validLeg.alight.stop.coordinate.northing = j;
			i++;
			j++;

			validLeg.destination = new Event();
			validLeg.destination.activity = ActivityType.Arrive;
			validLeg.destination.arriveTime = timeNow.AddMinutes(30);
			validLeg.destination.stop = new Stop();
			validLeg.destination.stop.NaPTANID = "Destination NAPTANID";
			validLeg.destination.stop.name = "Destination name";

			validLeg.destination.stop.coordinate = new Coordinate();
			validLeg.destination.stop.coordinate.easting = i;
			validLeg.destination.stop.coordinate.northing = j;
			i++;
			j++;

			return validLeg;
		}

		/// <summary>
		/// Creates a leg object with origin, board, alight and destination points.
		/// All points have separate values for their co-ordinates, hence this method takes 4 input int parameters
		/// </summary>
		/// <returns>CJP Leg object</returns>
		public Leg CreateLegWithVaribleValidity(int i, int j, int k, int l)
		{
			//Create the CJP legs, based upon test conditions
			int originCoordinate = i;
			int boardCoordinate = j;
			int alightCoordinate = k;
			int destinationCoordinate = l;
			Leg variableValidityLeg = new TimedLeg();
			DateTime timeNow = DateTime.Now;

			variableValidityLeg.mode = ModeType.Rail;
			variableValidityLeg.validated = true;

			variableValidityLeg.origin = new Event();
			variableValidityLeg.origin.activity = ActivityType.Depart;
			variableValidityLeg.origin.departTime = timeNow;
			variableValidityLeg.origin.stop  = new Stop();
			variableValidityLeg.origin.stop.NaPTANID = "Board NAPTANID";
			variableValidityLeg.origin.stop.name = "Board name";

			variableValidityLeg.origin.stop.coordinate = new Coordinate();
			variableValidityLeg.origin.stop.coordinate.easting = originCoordinate;
			variableValidityLeg.origin.stop.coordinate.northing = originCoordinate;

			variableValidityLeg.board = new Event();
			variableValidityLeg.board.activity = ActivityType.Depart;
			variableValidityLeg.board.departTime = timeNow;
			variableValidityLeg.board.stop = new Stop();
			variableValidityLeg.board.stop.NaPTANID = "Board NAPTANID";
			variableValidityLeg.board.stop.name = "Board name";

			variableValidityLeg.board.stop.coordinate = new Coordinate();
			variableValidityLeg.board.stop.coordinate.easting = boardCoordinate;
			variableValidityLeg.board.stop.coordinate.northing = boardCoordinate;

			variableValidityLeg.alight = new Event();
			variableValidityLeg.alight.activity = ActivityType.Arrive;
			variableValidityLeg.alight.arriveTime = timeNow.AddMinutes(30);
			variableValidityLeg.alight.stop = new Stop();
			variableValidityLeg.alight.stop.NaPTANID = "Alight NAPTANID";
			variableValidityLeg.alight.stop.name = "Alight name";

			variableValidityLeg.alight.stop.coordinate = new Coordinate();
			variableValidityLeg.alight.stop.coordinate.easting = alightCoordinate;
			variableValidityLeg.alight.stop.coordinate.northing = alightCoordinate;

			variableValidityLeg.destination = new Event();
			variableValidityLeg.destination.activity = ActivityType.Arrive;
			variableValidityLeg.destination.arriveTime = timeNow.AddMinutes(30);
			variableValidityLeg.destination.stop = new Stop();
			variableValidityLeg.destination.stop.NaPTANID = "Destination NAPTANID";
			variableValidityLeg.destination.stop.name = "Destination name";

			variableValidityLeg.destination.stop.coordinate = new Coordinate();
			variableValidityLeg.destination.stop.coordinate.easting = destinationCoordinate;
			variableValidityLeg.destination.stop.coordinate.northing = destinationCoordinate;

			return variableValidityLeg;
		}

		/// <summary>
		/// Create a CJPResult containing either private or public journeys
		/// </summary>
		/// <param name="Public">bool Public - true for a Public journey, false for a private journey</param>
		/// <returns>JourneyResult</returns>
		public JourneyResult CreateCJPResult( bool Public )
		{
			JourneyResult result = new JourneyResult();

			if( Public )
			{
				result.publicJourneys = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney[2];
				result.publicJourneys[0] = createPublicJourney();
				result.publicJourneys[1] = createRailReplacementPublicJourney();
			}
			else
			{
				result.privateJourneys = new PrivateJourney[1];
				result.privateJourneys[0] = createPrivateJourney();
			}
			
			return result;
		}
		/// <summary>
		/// Create a private journey.
		/// It will have a start, finish and drive section 
		/// </summary>
		/// <returns>PrivateJourney</returns>
		private PrivateJourney createPrivateJourney()
		{
			PrivateJourney result = new PrivateJourney();
			result.start = new StopoverSection();
			result.start = createStopoverSection( "Start" );
			result.finish = new StopoverSection();
			result.finish = createStopoverSection( "Finish" );
			result.sections = new Section[1];
			result.sections[0] = createDriveSection();

			return result;
		}

		/// <summary>
		/// Create a public journey
		/// It will have a board and alight leg
		/// </summary>
		/// <returns>PublicJourney</returns>
		private TransportDirect.JourneyPlanning.CJPInterface.PublicJourney createPublicJourney()
		{
			TransportDirect.JourneyPlanning.CJPInterface.PublicJourney result = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney();
			DateTime timeNow = DateTime.Now;

			result.legs = new Leg[1];

			result.legs[0] = new TimedLeg();
			
			result.legs[0].mode = ModeType.Tram;
			result.legs[0].validated = true;

			result.legs[0].board = new Event();
			result.legs[0].board.activity = ActivityType.Depart;
			result.legs[0].board.departTime = timeNow;
			//			result.legs[0].board.pass = false;
			result.legs[0].board.stop = new Stop();
			result.legs[0].board.stop.NaPTANID = "Board NAPTANID";
			result.legs[0].board.stop.name = "Board name";
			result.legs[0].board.geometry = new Coordinate[5];
			result.legs[0].board.geometry[0] = new Coordinate();
			result.legs[0].board.geometry[0].easting = 0 + DateTime.Now.Millisecond;
			result.legs[0].board.geometry[0].northing = 1 + DateTime.Now.Millisecond;
			result.legs[0].board.geometry[1] = new Coordinate();
			result.legs[0].board.geometry[1].easting = 1 + DateTime.Now.Millisecond;
			result.legs[0].board.geometry[1].northing = 2 + DateTime.Now.Millisecond;
			result.legs[0].board.geometry[2] = new Coordinate();
			result.legs[0].board.geometry[2].easting = 2 + DateTime.Now.Millisecond;
			result.legs[0].board.geometry[2].northing = 3 + DateTime.Now.Millisecond;
			result.legs[0].board.geometry[3] = new Coordinate();
			result.legs[0].board.geometry[3].easting = 3 + DateTime.Now.Millisecond;
			result.legs[0].board.geometry[3].northing = 4 + DateTime.Now.Millisecond;
			result.legs[0].board.geometry[4] = new Coordinate();
			result.legs[0].board.geometry[4].easting = 4 + DateTime.Now.Millisecond;
			result.legs[0].board.geometry[4].northing = 5 + DateTime.Now.Millisecond;

			result.legs[0].alight = new Event();
			result.legs[0].alight.activity = ActivityType.Arrive;
			result.legs[0].alight.arriveTime = timeNow.AddMinutes(30);
			//			result.legs[0].alight.pass = false;
			result.legs[0].alight.stop = new Stop();
			result.legs[0].alight.stop.NaPTANID = "Alight NAPTANID";
			result.legs[0].alight.stop.name = "Alight name";
			result.legs[0].alight.geometry = new Coordinate[5];
			result.legs[0].alight.geometry[0] = new Coordinate();
			result.legs[0].alight.geometry[0].easting = 1000 + DateTime.Now.Millisecond;
			result.legs[0].alight.geometry[0].northing = 60 + DateTime.Now.Millisecond;
			result.legs[0].alight.geometry[1] = new Coordinate();
			result.legs[0].alight.geometry[1].easting = 900 + DateTime.Now.Millisecond;
			result.legs[0].alight.geometry[1].northing = 50 + DateTime.Now.Millisecond;
			result.legs[0].alight.geometry[2] = new Coordinate();
			result.legs[0].alight.geometry[2].easting = 800 + DateTime.Now.Millisecond;
			result.legs[0].alight.geometry[2].northing = 40 + DateTime.Now.Millisecond;
			result.legs[0].alight.geometry[3] = new Coordinate();
			result.legs[0].alight.geometry[3].easting = 700 + DateTime.Now.Millisecond;
			result.legs[0].alight.geometry[3].northing = 30 + DateTime.Now.Millisecond;
			result.legs[0].alight.geometry[4] = new Coordinate();
			result.legs[0].alight.geometry[4].easting = 600 + DateTime.Now.Millisecond;
			result.legs[0].alight.geometry[4].northing = 20 + DateTime.Now.Millisecond;
			result.legs[0].alight.geometry[4].northing = 20 + DateTime.Now.Millisecond;

			result.legs[0].destination = new Event();
			result.legs[0].destination.activity = ActivityType.Arrive;
			result.legs[0].destination.arriveTime = timeNow.AddMinutes(30);
			//			result.legs[0].destination.pass = false;
			result.legs[0].destination.stop = new Stop();
			result.legs[0].destination.stop.NaPTANID = "Destination NAPTANID";
			result.legs[0].destination.stop.name = "Destination name";
			result.legs[0].destination.geometry = new Coordinate[5];
			result.legs[0].destination.geometry[0] = new Coordinate();
			result.legs[0].destination.geometry[0].easting = 1000 + DateTime.Now.Millisecond;
			result.legs[0].destination.geometry[0].northing = 60 + DateTime.Now.Millisecond;
			result.legs[0].destination.geometry[1] = new Coordinate();
			result.legs[0].destination.geometry[1].easting = 900 + DateTime.Now.Millisecond;
			result.legs[0].destination.geometry[1].northing = 50 + DateTime.Now.Millisecond;
			result.legs[0].destination.geometry[2] = new Coordinate();
			result.legs[0].destination.geometry[2].easting = 800 + DateTime.Now.Millisecond;
			result.legs[0].destination.geometry[2].northing = 40 + DateTime.Now.Millisecond;
			result.legs[0].destination.geometry[3] = new Coordinate();
			result.legs[0].alight.geometry[3].easting = 700 + DateTime.Now.Millisecond;
			result.legs[0].destination.geometry[3].northing = 30 + DateTime.Now.Millisecond;
			result.legs[0].destination.geometry[4] = new Coordinate();
			result.legs[0].destination.geometry[4].easting = 600 + DateTime.Now.Millisecond;
			result.legs[0].destination.geometry[4].northing = 20 + DateTime.Now.Millisecond;
			return result;
		}

		/// <summary>
		/// Create a public journey with a mode type RailReplacementBus
		/// It will have a board and alight leg
		/// </summary>
		/// <returns>PublicJourney with each leg having a mode of RailReplacementBus</returns>
		private TransportDirect.JourneyPlanning.CJPInterface.PublicJourney createRailReplacementPublicJourney()
		{
			TransportDirect.JourneyPlanning.CJPInterface.PublicJourney railReplacement = this.createPublicJourney();
			foreach (Leg leg in railReplacement.legs ) 
			{
				leg.mode = ModeType.RailReplacementBus;
			}
			return railReplacement;
		}

		/// <summary>
		/// Creates a stop over section with a given name
		/// For use with the private journey
		/// </summary>
		/// <param name="name">string - the name of the section</param>
		/// <returns>StopoverSection</returns>
		private StopoverSection createStopoverSection( string name )
		{
			DateTime timeNow = DateTime.Now;
			StopoverSection stopoverSection = new StopoverSection();

			stopoverSection.time = timeNow;
			stopoverSection.name = name + " Section Name";
			stopoverSection.node = new ITNNode();
			stopoverSection.node.TOID = name + " TOID";

			return stopoverSection;
		}
		/// <summary>
		/// Create a drive section
		/// For use with the private journey
		/// </summary>
		/// <returns>DriveSection</returns>
		private DriveSection createDriveSection()
		{
			DateTime timeNow = DateTime.Now;
			DriveSection driveSection = new DriveSection();

			driveSection.time = timeNow;
			driveSection.name = "Drive Section Name";
			driveSection.number = "Drive Section Number";
			driveSection.distance = 123;
			driveSection.turnCount = 3;
			driveSection.turnDirection = TurnDirection.MiniRoundaboutContinue;
			driveSection.turnAngle = TurnAngle.Continue;
			driveSection.roundabout = true;
			driveSection.throughRoute = true;
			driveSection.cost = 30;
			driveSection.heading = "Drive Section Heading";
			driveSection.links = new ITNLink[1];
			driveSection.links[0] = new ITNLink();
			driveSection.links[0].TOID = "Drive TOID";
			driveSection.links[0].congestion = 1;
			return driveSection;
		}
		/// <summary>
		/// Create a mock ITNLink array
		/// </summary>
		/// <returns>ITNLink[]</returns>
		private TransportDirect.JourneyPlanning.CJPInterface.ITNLink[] createITNLink()
		{
			TransportDirect.JourneyPlanning.CJPInterface.ITNLink[] itnLinks = new TransportDirect.JourneyPlanning.CJPInterface.ITNLink[10];

			for( int i=0 ; i<=itnLinks.GetUpperBound(0); i++)
			{
				itnLinks[i] = new TransportDirect.JourneyPlanning.CJPInterface.ITNLink();
				itnLinks[i].TOID = string.Format("Toid {0:F2}", (DateTime.Now.Millisecond / (i+1)));
				itnLinks[i].congestion = i * DateTime.Now.Millisecond;
			}
			return itnLinks;
		}

		/// <summary>
		/// Print each OSGridRefrence in journey to
		/// the console
		/// </summary>
		/// <param name="journey"></param>
		private void outputOSgridData( PublicJourney journey ) 
		{
			foreach( PublicJourneyDetail pjd in journey.Details )
			{
				foreach( OSGridReference osg in pjd.Geometry )
				{
					if( osg == null )
					{
						System.Console.Write("osg is null ");
					}
					else
					{
						System.Console.Write("osg = "+osg.Easting);
					}
				}
			}
		}

	
	}
}
