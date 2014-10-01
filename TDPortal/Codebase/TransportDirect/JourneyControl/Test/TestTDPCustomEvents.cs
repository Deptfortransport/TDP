// *********************************************** 
// NAME                 : TestTDPCustomEvents.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : NUnit tests for testing
// TDP Custom Event classes defined with the 
// Journey Control project.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestTDPCustomEvents.cs-arc  $
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
//   Rev 1.13   Mar 14 2006 08:41:38   build
//Automatically merged from branch for stream3353
//
//   Rev 1.12.1.0   Mar 02 2006 17:48:06   NMoorhouse
//extra param to hold private via location
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.12   Aug 24 2005 16:06:54   RPhilpott
//Make tests consistent with changed TDJourneyResult.AddResult() and PublicJourney ctor. 
//Resolution for 2662: DN062:  Map with non-consecutive nodes with no co-ordinates functioning incorrectly
//
//   Rev 1.11   Mar 23 2005 15:22:24   rhopkins
//Fixed FxCop "warnings"
//
//   Rev 1.10   Feb 23 2005 16:41:36   rscott
//DEL7 Update - TDDateTime OutwardDateTime and ReturnDateTime changed to TDDateTime[] arrays.
//
//   Rev 1.9   Jan 31 2005 15:31:50   RScott
//DEL 7 - Updated to for addition of PublicViaLocations, PublicSoftViaLocations, PublicNotViaLocations.
//
//   Rev 1.8   Jan 20 2005 09:28:12   RScott
//DEL 7 - Updated to for addition of PublicViaLocations, PublicSoftViaLocations, PublicNotViaLocations.
//
//   Rev 1.7   Jan 18 2005 15:56:32   rhopkins
//Removed dependancy on other tests so that this test will work when TestCarCostCalculator is present.
//Also changed Assertion to Assert.
//
//   Rev 1.6   Sep 25 2003 11:44:44   RPhilpott
//Map handoff and MI logging changes
//
//   Rev 1.5   Sep 24 2003 18:51:24   RPhilpott
//Clear warning
//
//   Rev 1.4   Sep 24 2003 18:24:16   geaton
//Test data added to support JourneyPlanResultsVerboseEvent class.
//
//   Rev 1.3   Sep 23 2003 14:06:34   RPhilpott
//Changes to TDJourneyResult (ctor and referenceNumber)
//
//   Rev 1.2   Sep 19 2003 15:23:46   RPhilpott
//Move CjpStub to JourneyControl
//
//   Rev 1.1   Sep 18 2003 17:58:10   geaton
//Removed TDTraceListener before/after tests so tests do not interfere with other tests run before/after.
//
//   Rev 1.0   Sep 12 2003 12:42:14   geaton
//Initial Revision

using NUnit.Framework;

using System;
using System.Collections;
using System.Diagnostics;
using System.IO;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.JourneyControl
{

	[TestFixture]
	public class TestTdpCustomEvents
	{
		/// <summary>
		/// Tests that all TDP Custom Events can be published successfully.
		/// </summary>
		[Test]
		public void PublishEvents()
		{
			IPropertyProvider mockProperties = new MockProperties();
			IEventPublisher[] customPublishers = new IEventPublisher[0];																								
			ArrayList errors = new ArrayList();
				
			try
			{
				Trace.Listeners.Add(new TDTraceListener(mockProperties, customPublishers, errors));
			}
			catch (TDException)
			{
				Assert.Fail();
			}
	
			// create instances of all events

			ModeType[] modes = new ModeType[] { ModeType.Rail, ModeType.Car };
			JourneyPlanRequestEvent req = new JourneyPlanRequestEvent("123", modes, false, "456"); 
			
			ITDJourneyRequest request = new TDJourneyRequest();
			DateTime timeNow = DateTime.Now;
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
			request.PrivateViaLocation = new TDLocation();
			request.AvoidRoads = new string[] {"A1", "A6"};
			request.AlternateLocations = new TDLocation[2];
			request.AlternateLocations[0] = new TDLocation();
			request.AlternateLocations[1] = new TDLocation();
			request.AlternateLocationsFrom = true;
			request.PrivateAlgorithm = PrivateAlgorithmType.MostEconomical;
			request.PublicAlgorithm = PublicAlgorithmType.Fastest;
			request.Modes = new ModeType[] { ModeType.Rail, ModeType.Car };

			JourneyPlanRequestVerboseEvent reqv = new JourneyPlanRequestVerboseEvent("666", (TDJourneyRequest)request, true, "999");


			// use other test fixture class to generate results data
			TestTDJourneyResult testResult = new TestTDJourneyResult();
			JourneyResult cjpResult = testResult.CreateCJPResult( true );
			
			TDJourneyResult result = new TDJourneyResult(1234);
			result.AddResult(cjpResult, true, null, null, null, null, "ssss",false, -1);
			JourneyPlanResultsVerboseEvent resv = new JourneyPlanResultsVerboseEvent("555",result,true,"53"); 

			JourneyPlanResultsEvent res = new JourneyPlanResultsEvent("222", JourneyPlanResponseCategory.Results, true, "42"); 


			// turn auditing on to check that event was published
			req.AuditPublishersOff = false;
			reqv.AuditPublishersOff = false;
			resv.AuditPublishersOff = false;
			res.AuditPublishersOff = false;
		

			// publish events
			try
			{
				Trace.Write(req);
				Trace.Write(reqv);
				Trace.Write(resv);
				Trace.Write(res);
			}
			catch (TDException)
			{
				Assert.Fail();
			}

			// check that they were published. NB manual check required for events that are published to file in production
			Assert.IsTrue(req.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");
			Assert.IsTrue(reqv.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");
			Assert.IsTrue(resv.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");
			Assert.IsTrue(res.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");

		}

		[SetUp]
		public void SetUp()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestJourneyInitialisation());

			Trace.Listeners.Remove("TDTraceListener");
		}


		[TearDown]
		public void Cleanup()
		{
			Trace.Listeners.Remove("TDTraceListener");
		}
	}
}


