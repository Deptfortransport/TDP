// *********************************************** 
// NAME			: TestJourneyRequestPopulator.cs
// AUTHOR		: Richard Philpott
// DATE CREATED	: 2006-02-21
// DESCRIPTION	: NUnit tests for JourneyRequestPopulator 
//                and its specialised subclasses.  
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestJourneyRequestPopulator.cs-arc  $
//
//   Rev 1.3   Apr 02 2013 11:18:20   mmodi
//Unit test updates
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.2   Sep 06 2011 15:13:34   apatel
//Unit test update for real time information in car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.1   Sep 01 2011 10:43:28   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.0   Nov 08 2007 12:24:16   mturner
//Initial revision.
//
//   Rev 1.2   Mar 03 2006 15:51:12   RPhilpott
//Updated tests to include new secondaryModeFilter parameter on CJP request (then commented them out until new CJP interface received).
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.1   Feb 27 2006 12:20:08   RPhilpott
//Assign to IR 0018
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.0   Feb 27 2006 12:19:28   RPhilpott
//Initial revision.
//

using System;
using System.IO;
using System.Collections;
using System.Diagnostics;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;


namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for TestJourneyControl.
	/// </summary>
	[TestFixture]
	public class TestJourneyRequestPopulator
	{
		private DateTime outwardDateTime;
		private DateTime returnDateTime;
		private TestJourneyRequestData requestData;

		public TestJourneyRequestPopulator()
		{
		}

		[TestFixtureSetUp]
		public void SetUp()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestJRPInitialisation());			
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

			outwardDateTime = DateTime.Now;
			returnDateTime  = DateTime.Now + new TimeSpan(1, 0, 0);

			requestData = new TestJourneyRequestData(outwardDateTime, returnDateTime);

		}

		[TestFixtureTearDown]
		public void Cleanup()
		{
			Trace.Listeners.Remove("TDTraceListener");
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}

		[Test]
		public void TestPopulateMultimodalParameters()
		{
			// test 1 - outward only, depart after time, Naptan-Naptan search
			//			modes rail, car, bus, coach, no operator or service filters
			
			ITDJourneyRequest request = requestData.InitialiseDefaultRequest(true);

			JourneyRequestPopulator populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			CJPCall[] calls = populator.PopulateRequests(1234, 4321, "session", false, 0, "en-gb");
			
			
			Assert.AreEqual(calls.Length, 1, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-4321");

			JourneyRequest cjpRequest = calls[0].CJPRequest;

			Assert.IsTrue(cjpRequest.modeFilter.include);
			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 5, "No of modes");

			Assert.AreEqual(cjpRequest.origin.stops.Length, 3, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints.Length, 2, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "OriginName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00001234", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 2, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints.Length, 3, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "DestinationName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00004321", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");

			Assert.AreEqual(cjpRequest.origin.stops[0].timeDate,outwardDateTime,"Date/time");
			Assert.AreEqual(cjpRequest.origin.stops[1].timeDate,outwardDateTime,"Date/time");
			Assert.AreEqual(cjpRequest.origin.stops[2].timeDate,outwardDateTime,"Date/time");
			Assert.AreEqual(cjpRequest.origin.roadPoints[0].timeDate,outwardDateTime,"Date/time");
			Assert.AreEqual(cjpRequest.origin.roadPoints[1].timeDate,outwardDateTime,"Date/time");

			Assert.AreEqual(cjpRequest.operatorFilter, null, "Operators");
			Assert.AreEqual(cjpRequest.serviceFilter,  null, "Services");

			Assert.AreEqual(cjpRequest.publicParameters.interval, DateTime.MinValue, "Interval");
			Assert.AreEqual(cjpRequest.publicParameters.sequence, 4, "Sequence");
			Assert.AreEqual(cjpRequest.publicParameters.rangeType, RangeType.Sequence, "RangeType");
			Assert.AreEqual(cjpRequest.publicParameters.algorithm, PublicAlgorithmType.NoChanges, "PublicAlgorithm");
			
			Assert.AreEqual(cjpRequest.privateParameters.algorithm, PrivateAlgorithmType.Fastest, "PrivateAlgorithm");

			// test 2 - outward and return, arrive-before times, Coordinate-Locality search
			//			modes rail, bus, operator and service filters present (as includes)

			request = requestData.InitialiseDefaultRequest(false);

			request.Modes = new ModeType[] {ModeType.Rail, ModeType.Bus};

			request.SelectedOperators = new string[] { "XX", "YY", "ZZ" };
			request.UseOnlySpecifiedOperators = true;

			request.TrainUidFilterIsInclude = true;
			request.TrainUidFilter = new string[] { "C00001", "G00001", "Y54321", "Y11111", "G11111" };

			request.OriginLocation.RequestPlaceType = RequestPlaceType.Coordinate;
			request.OriginLocation.NaPTANs = new TDNaptan[0];
			request.DestinationLocation.RequestPlaceType = RequestPlaceType.Locality;
			request.DestinationLocation.NaPTANs = new TDNaptan[0];

			request.OutwardArriveBefore = true;
			request.ReturnArriveBefore = true;

			request.PublicViaLocations[0].Status = TDLocationStatus.Ambiguous;
			request.PublicNotViaLocations[0].Status = TDLocationStatus.Ambiguous;
			request.PublicSoftViaLocations[0].Status = TDLocationStatus.Ambiguous;

			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 321, "sesxxx", true, 2, "cy-gb");
			Assert.AreEqual(calls.Length, 2, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-0321");

			cjpRequest = calls[0].CJPRequest;

			Assert.AreEqual(cjpRequest.requestID, "0000-0000-0000-1234-0321");
			Assert.AreEqual(cjpRequest.language, "cy-gb");
			Assert.AreEqual(cjpRequest.sessionID, "sesxxx");
			Assert.IsTrue(cjpRequest.referenceTransaction);
			Assert.AreEqual(cjpRequest.userType, 2);

			Assert.IsTrue(cjpRequest.modeFilter.include);
			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 3, "No of modes");

			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "OriginName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00001234", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.Coordinate, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "DestinationName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00004321", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.Locality, "Destination location");

			Assert.AreEqual(cjpRequest.destination.stops[0].timeDate,outwardDateTime,"Date/time");

			Assert.IsTrue(cjpRequest.operatorFilter.include,"Operators");
			Assert.AreEqual(cjpRequest.operatorFilter.operatorCodes.Length,3,"Operators");

			Assert.IsTrue(cjpRequest.serviceFilter.include,"Services");
			Assert.AreEqual(cjpRequest.serviceFilter.services.Length,5,"Services");

			Assert.AreEqual(cjpRequest.publicParameters.interval,DateTime.MinValue,"Interval");
			Assert.AreEqual(cjpRequest.publicParameters.sequence,5,"Sequence");
			Assert.AreEqual(cjpRequest.publicParameters.rangeType,RangeType.Sequence,"RangeType");

			Assert.AreEqual(cjpRequest.publicParameters.vias.Length,0,"vias");
			Assert.AreEqual(cjpRequest.publicParameters.softVias.Length,0,"Redhill","softVias");
			Assert.AreEqual(cjpRequest.publicParameters.notVias.Length,0,"notVias");

			Assert.IsTrue(calls[1].IsReturnJourney);
			Assert.AreEqual(calls[1].RequestId, "0000-0000-0000-1234-0322");

			cjpRequest = calls[1].CJPRequest;

			Assert.AreEqual(cjpRequest.requestID, "0000-0000-0000-1234-0322");
			Assert.AreEqual(cjpRequest.language, "cy-gb");
			Assert.AreEqual(cjpRequest.sessionID, "sesxxx");
			Assert.IsTrue(cjpRequest.referenceTransaction);
			Assert.AreEqual(cjpRequest.userType, 2);

			Assert.IsTrue(cjpRequest.modeFilter.include);
			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 3, "No of modes");

			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "DestinationName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00004321", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.Locality, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "OriginName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00001234", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.Coordinate, "Destination location");

			Assert.AreEqual(cjpRequest.destination.stops[0].timeDate, returnDateTime, "Date/time");

			Assert.IsTrue(cjpRequest.operatorFilter.include, "Operators");
			Assert.AreEqual(cjpRequest.operatorFilter.operatorCodes.Length, 3, "Operators");

			Assert.IsTrue(cjpRequest.serviceFilter.include, "Services");
			Assert.AreEqual(cjpRequest.serviceFilter.services.Length, 5, "Services");

			Assert.AreEqual(cjpRequest.publicParameters.interval,DateTime.MinValue,"Interval");
			Assert.AreEqual(cjpRequest.publicParameters.sequence,5,"Sequence");
			Assert.AreEqual(cjpRequest.publicParameters.rangeType,RangeType.Sequence,"RangeType");

			Assert.AreEqual(cjpRequest.publicParameters.vias.Length,0,"vias");
			Assert.AreEqual(cjpRequest.publicParameters.softVias.Length,0,"Redhill","softVias");
			Assert.AreEqual(cjpRequest.publicParameters.notVias.Length,0,"notVias");

			// test 3 - outward and return, arrive-before and depart-after times, mode Car only,
			//			includes invalid private via location

			request = requestData.InitialiseDefaultRequest(false);

			request.Modes = new ModeType[] { ModeType.Car };
			request.OutwardArriveBefore = true;
			request.ReturnArriveBefore = false;
			request.PrivateViaLocation.Status = TDLocationStatus.Ambiguous;

			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 0, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 2, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-0000");

			cjpRequest = calls[0].CJPRequest;

			Assert.AreEqual(cjpRequest.requestID, "0000-0000-0000-1234-0000");
			Assert.AreEqual(cjpRequest.language, "en-gb");
			Assert.AreEqual(cjpRequest.sessionID, "session");
			Assert.IsFalse(cjpRequest.referenceTransaction);
			Assert.AreEqual(cjpRequest.userType, 0);

			Assert.IsTrue(cjpRequest.modeFilter.include);
			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1, "No of modes");
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Car, "Mode");

			Assert.AreEqual(cjpRequest.origin.stops, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints.Length, 2,"Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "OriginName", "Origin location");

			Assert.AreEqual(cjpRequest.destination.roadPoints[0].timeDate, outwardDateTime, "Date/time");
			Assert.AreEqual(cjpRequest.destination.roadPoints[1].timeDate, outwardDateTime, "Date/time");
			Assert.AreEqual(cjpRequest.destination.roadPoints[2].timeDate, outwardDateTime, "Date/time");
		
			Assert.AreEqual(cjpRequest.destination.stops, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints.Length, 3, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "DestinationName", "Destination location");

            Assert.AreEqual(cjpRequest.privateParameters.bannedTOIDs.Length, 2, "Destination location");

			Assert.IsTrue(calls[1].IsReturnJourney);
			Assert.AreEqual(calls[1].RequestId, "0000-0000-0000-1234-0001");

			cjpRequest = calls[1].CJPRequest;

			Assert.AreEqual(cjpRequest.requestID, "0000-0000-0000-1234-0001");
			Assert.AreEqual(cjpRequest.language, "en-gb");
			Assert.AreEqual(cjpRequest.sessionID, "session");
			Assert.IsFalse(cjpRequest.referenceTransaction);
			Assert.AreEqual(cjpRequest.userType, 0);

			Assert.IsTrue(cjpRequest.modeFilter.include);
			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1, "No of modes");
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Car, "Mode");

			Assert.AreEqual(cjpRequest.origin.stops, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints.Length, 3,"Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "DestinationName", "Origin location");

			Assert.AreEqual(cjpRequest.origin.roadPoints[0].timeDate, returnDateTime, "Date/time");
			Assert.AreEqual(cjpRequest.origin.roadPoints[1].timeDate, returnDateTime, "Date/time");
			Assert.AreEqual(cjpRequest.origin.roadPoints[2].timeDate, returnDateTime, "Date/time");
		
			Assert.AreEqual(cjpRequest.destination.stops, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints.Length, 2, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "OriginName", "Destination location");

			Assert.AreEqual(cjpRequest.privateParameters.vias.Length, 0, "vias");

            Assert.AreEqual(cjpRequest.privateParameters.bannedTOIDs.Length, 2, "Destination location");

		}

        /* Commented out as ITP is no longer used
		[Test]
		public void TestPopulateDirectAirParameters()
		{
		
			//
			// Test 1 - outward journey, leave after, current date, specified time (now) 
			//
			
			ITDJourneyRequest request = requestData.InitialiseDefaultFlightRequest(true);

			DateTime testOutwardDateTime = outwardDateTime;
			DateTime testReturnDateTime  = returnDateTime;

			request.DirectFlightsOnly = true;

			JourneyRequestPopulator populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			CJPCall[] calls = populator.PopulateRequests(1234, 0, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 1, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-0000");

			JourneyRequest cjpRequest = calls[0].CJPRequest;

			Assert.AreEqual(cjpRequest.requestID, "0000-0000-0000-1234-0000");
			Assert.AreEqual(cjpRequest.language, "en-gb");
			Assert.AreEqual(cjpRequest.sessionID, "session");
			Assert.IsFalse(cjpRequest.referenceTransaction);
			Assert.AreEqual(cjpRequest.userType, 0);

			Assert.AreEqual(cjpRequest.origin.stops.Length,3,"Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints,null,"Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName,"OriginName","Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length,2,"Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints,null,"Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName,"DestinationName","Destination location");

			Assert.IsTrue(cjpRequest.origin.stops[0].timeDate >= testOutwardDateTime, "Date/time");
			Assert.IsTrue(cjpRequest.origin.stops[1].timeDate >= testOutwardDateTime, "Date/time");
			Assert.IsTrue(cjpRequest.origin.stops[2].timeDate >= testOutwardDateTime, "Date/time");

			testOutwardDateTime = testOutwardDateTime.AddMinutes(1);

			Assert.IsTrue(cjpRequest.origin.stops[0].timeDate <= testOutwardDateTime,"Date/time");
			Assert.IsTrue(cjpRequest.origin.stops[1].timeDate <= testOutwardDateTime,"Date/time");
			Assert.IsTrue(cjpRequest.origin.stops[2].timeDate <= testOutwardDateTime,"Date/time");

			Assert.AreEqual(cjpRequest.operatorFilter,null,"Operators");
			Assert.IsTrue(cjpRequest.depart,"ArriveBefore");

			Assert.IsTrue(cjpRequest.publicParameters.interval <= (DateTime.MinValue + new TimeSpan(22, 1, 0)),"Interval");
			Assert.IsTrue(cjpRequest.publicParameters.interval >= (DateTime.MinValue + new TimeSpan(21, 59, 0)),"Interval");
			Assert.AreEqual(cjpRequest.publicParameters.sequence,0,"Sequence");
			Assert.AreEqual(cjpRequest.publicParameters.rangeType,RangeType.Interval,"RangeType");
			Assert.AreEqual(cjpRequest.publicParameters.algorithm, PublicAlgorithmType.NoChanges);
			Assert.AreEqual(cjpRequest.publicParameters.extraCheckInTime, DateTime.MinValue);

			//
			// Test 2 - return journey, leave after, current date, specified time (one hour in future) 
			//

			request = requestData.InitialiseDefaultFlightRequest(false);

			request.SelectedOperators = new string[] { "BA", "LH" };
			request.UseOnlySpecifiedOperators = true;

			request.DirectFlightsOnly = false;

			request.ExtraCheckinTime = new TDDateTime(DateTime.MinValue + new TimeSpan(0, 35, 0));

			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", true, 2, "cy-gb");
			Assert.AreEqual(calls.Length, 2, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-4321");
			Assert.IsTrue(calls[1].IsReturnJourney);
			Assert.AreEqual(calls[1].RequestId, "0000-0000-0000-1234-4322");

			cjpRequest = calls[1].CJPRequest;	// checking return only

			Assert.AreEqual(cjpRequest.requestID, "0000-0000-0000-1234-4322");
			Assert.AreEqual(cjpRequest.language, "cy-gb");
			Assert.AreEqual(cjpRequest.sessionID, "session");
			Assert.IsTrue(cjpRequest.referenceTransaction);
			Assert.AreEqual(cjpRequest.userType, 2);

			Assert.AreEqual(cjpRequest.origin.stops.Length,2,"Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints,null,"Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName,"DestinationName","Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length,3,"Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints,null,"Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName,"OriginName","Destination location");

			testReturnDateTime = DateTime.Now - new TimeSpan(0, 1, 0);

			Assert.IsTrue(cjpRequest.origin.stops[0].timeDate >= testReturnDateTime,"Date/time");
			Assert.IsTrue(cjpRequest.origin.stops[1].timeDate >= testReturnDateTime,"Date/time");

			testReturnDateTime = testReturnDateTime.AddMinutes(1);

			Assert.IsTrue(cjpRequest.origin.stops[0].timeDate <= testReturnDateTime,"Date/time");
			Assert.IsTrue(cjpRequest.origin.stops[1].timeDate <= testReturnDateTime,"Date/time");
	
			Assert.IsTrue(cjpRequest.operatorFilter.include,"Operators");
			Assert.AreEqual(cjpRequest.operatorFilter.operatorCodes.Length,2,"Operators");
			Assert.IsTrue(cjpRequest.depart,"ArriveBefore");

			Assert.IsTrue(cjpRequest.publicParameters.interval <= (DateTime.MinValue + new TimeSpan(23, 1, 0)),"Interval");
			Assert.IsTrue(cjpRequest.publicParameters.interval >= (DateTime.MinValue + new TimeSpan(22, 59, 0)),"Interval");
			Assert.AreEqual(cjpRequest.publicParameters.sequence,0,"Sequence");
			Assert.AreEqual(cjpRequest.publicParameters.rangeType,RangeType.Interval,"RangeType");
			Assert.AreEqual(cjpRequest.publicParameters.algorithm, PublicAlgorithmType.Default);
			Assert.AreEqual(cjpRequest.publicParameters.extraCheckInTime, DateTime.MinValue + new TimeSpan(0, 35, 0));


			//
			// Test 3 - outward journey, arrive before, current date, specified time (now) 
			//

			request = requestData.InitialiseDefaultFlightRequest(true);

			testOutwardDateTime = outwardDateTime;
			
			request.SelectedOperators = new string[] { "BA", "LH", "XX", "YY" };
			request.UseOnlySpecifiedOperators = false;
			request.OutwardArriveBefore = true;

			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 21, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 1, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-0021");

			cjpRequest = calls[0].CJPRequest;

			Assert.AreEqual(cjpRequest.origin.stops.Length,3,"Origin location");
			Assert.AreEqual(cjpRequest.destination.stops.Length,2,"Destination location");
			Assert.IsTrue(!cjpRequest.depart,"ArriveBefore");
			Assert.IsTrue(!cjpRequest.operatorFilter.include,"Operators");
			Assert.AreEqual(cjpRequest.operatorFilter.operatorCodes.Length,4,"Operators");
			
			testOutwardDateTime = testOutwardDateTime.AddHours(3);

			Assert.IsTrue(cjpRequest.destination.stops[0].timeDate >= testOutwardDateTime,"Date/time");

			testOutwardDateTime = testOutwardDateTime.AddMinutes(1);
			Assert.IsTrue(cjpRequest.destination.stops[0].timeDate <= testOutwardDateTime,"Date/time");

			Assert.IsTrue(cjpRequest.publicParameters.interval <= (DateTime.MinValue + new TimeSpan(3, 1, 0)),"Interval");
			Assert.IsTrue(cjpRequest.publicParameters.interval >= (DateTime.MinValue + new TimeSpan(2, 59, 0)),"Interval");
	
			//
			// Test 4 - return journey, arrive before, current date, specified time (one hour in future) 
			//
		
			request = requestData.InitialiseDefaultFlightRequest(false);

			testReturnDateTime  = returnDateTime;
			
			request.SelectedOperators = new string[0];
			request.UseOnlySpecifiedOperators = false;
			request.ReturnArriveBefore = true;

			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 2, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-4321");
			Assert.IsTrue(calls[1].IsReturnJourney);
			Assert.AreEqual(calls[1].RequestId, "0000-0000-0000-1234-4322");
			
			cjpRequest = calls[1].CJPRequest;

			Assert.AreEqual(cjpRequest.origin.stops.Length,2,"Origin location");
			Assert.AreEqual(cjpRequest.destination.stops.Length,3,"Destination location");
			Assert.IsTrue(!cjpRequest.depart,"ArriveBefore");
			
			testReturnDateTime = testReturnDateTime.AddHours(3);

			Assert.IsTrue(cjpRequest.destination.stops[0].timeDate >= testReturnDateTime,"Date/time");

			testReturnDateTime = testReturnDateTime.AddMinutes(1);
			Assert.IsTrue(cjpRequest.destination.stops[0].timeDate <= testReturnDateTime,"Date/time");

			Assert.AreEqual(cjpRequest.operatorFilter,null,"Operators");

			Assert.IsTrue(cjpRequest.publicParameters.interval <= (DateTime.MinValue + new TimeSpan(4, 1, 0)),"Interval");
			Assert.IsTrue(cjpRequest.publicParameters.interval >= (DateTime.MinValue + new TimeSpan(3, 59, 0)),"Interval");

			
			//
			// Test 5 - outward journey, leave after, current date, any time 
			//

			request = requestData.InitialiseDefaultFlightRequest(true);
			
			testOutwardDateTime = outwardDateTime;

			request.OutwardArriveBefore = false;
			request.OutwardAnyTime = true;
            
			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 1, "Number of CJPCall objects returned");

			cjpRequest = calls[0].CJPRequest;

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-4321");

			Assert.AreEqual(cjpRequest.origin.stops.Length,3,"Origin location");
			Assert.AreEqual(cjpRequest.destination.stops.Length,2,"Destination location");
			Assert.IsTrue(cjpRequest.depart,"ArriveBefore");
			
			Assert.IsTrue(cjpRequest.origin.stops[0].timeDate >= testOutwardDateTime,"Date/time");

			testOutwardDateTime = testOutwardDateTime.AddMinutes(1);
			Assert.IsTrue(cjpRequest.origin.stops[0].timeDate <= testOutwardDateTime,"Date/time");

			Assert.AreEqual(cjpRequest.publicParameters.interval,(DateTime.MinValue + new TimeSpan(23, 59, 59)),"Interval");
		

			//
			// Test 6 - return journey, leave after, current date, any time 
			//

			request = requestData.InitialiseDefaultFlightRequest(false);

			request.ReturnArriveBefore = false;
			request.ReturnAnyTime = true;
            
			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 2, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-4321");
			Assert.IsTrue(calls[1].IsReturnJourney);
			Assert.AreEqual(calls[1].RequestId, "0000-0000-0000-1234-4322");

			cjpRequest = calls[1].CJPRequest;

			Assert.AreEqual(cjpRequest.origin.stops.Length,2,"Origin location");
			Assert.AreEqual(cjpRequest.destination.stops.Length,3,"Destination location");
			Assert.IsTrue(cjpRequest.depart,"ArriveBefore");
			
			testReturnDateTime = DateTime.Now - new TimeSpan(0, 1, 0);

			Assert.IsTrue(cjpRequest.origin.stops[0].timeDate >= testReturnDateTime,"Date/time");

			testReturnDateTime = testReturnDateTime.AddMinutes(1);
			Assert.IsTrue(cjpRequest.origin.stops[0].timeDate <= testReturnDateTime,"Date/time");

			Assert.AreEqual(cjpRequest.publicParameters.interval,(DateTime.MinValue + new TimeSpan(23, 59, 59)),"Interval");
		

			//
			// Test 7 - outward journey, arrive before, current date, any time 
			//

			request = requestData.InitialiseDefaultFlightRequest(true);

			testOutwardDateTime = outwardDateTime;
			
			request.OutwardArriveBefore = true;
			request.OutwardAnyTime = true;
            
			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 1, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-4321");

			cjpRequest = calls[0].CJPRequest;

			Assert.AreEqual(cjpRequest.origin.stops.Length,3,"Origin location");
			Assert.AreEqual(cjpRequest.destination.stops.Length,2,"Destination location");
			Assert.IsTrue(!cjpRequest.depart,"ArriveBefore");
			
			testOutwardDateTime = testOutwardDateTime.AddHours(24);

			Assert.IsTrue(cjpRequest.destination.stops[0].timeDate >= testOutwardDateTime,"Date/time");

			testOutwardDateTime = testOutwardDateTime.AddMinutes(1);
			Assert.IsTrue(cjpRequest.destination.stops[0].timeDate <= testOutwardDateTime,"Date/time");

			Assert.AreEqual(cjpRequest.publicParameters.interval,(DateTime.MinValue + new TimeSpan(23, 59, 59)),"Interval");
			
			
			//
			// Test 8 - return journey, arrive before, current date, any time 
			//

			request = requestData.InitialiseDefaultFlightRequest(false);

			request.ReturnArriveBefore = true;
			request.ReturnAnyTime = true;
            
			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 2, "Number of CJPCall objects returned");

			Assert.IsTrue(calls[1].IsReturnJourney);
			Assert.AreEqual(calls[1].RequestId, "0000-0000-0000-1234-4322");

			cjpRequest = calls[1].CJPRequest;

			Assert.AreEqual(cjpRequest.origin.stops.Length,2,"Origin location");
			Assert.AreEqual(cjpRequest.destination.stops.Length,3,"Destination location");
			Assert.IsTrue(!cjpRequest.depart,"ArriveBefore");
			
			testReturnDateTime = DateTime.Now;
			testReturnDateTime = testReturnDateTime.AddHours(24);

			Assert.IsTrue(cjpRequest.destination.stops[0].timeDate >= testReturnDateTime,"Date/time");

			testReturnDateTime = testReturnDateTime.AddMinutes(1);
			Assert.IsTrue(cjpRequest.destination.stops[0].timeDate <= testReturnDateTime,"Date/time");

			Assert.AreEqual(cjpRequest.publicParameters.interval,(DateTime.MinValue + new TimeSpan(23, 59, 59)),"Interval");


			//
			// Test 9 - outward journey, leave after, future date, any time 
			//

			request = requestData.InitialiseDefaultFlightRequest(true);

			request.OutwardArriveBefore = false;
			request.OutwardAnyTime = true;
            
			testOutwardDateTime = DateTime.Now + new TimeSpan(10, 12, 0, 0, 0);
			request.OutwardDateTime = new TDDateTime[1];
			request.OutwardDateTime[0] = new TDDateTime(testOutwardDateTime);
	
			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 1, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-4321");

			cjpRequest = calls[0].CJPRequest;

			Assert.AreEqual(cjpRequest.origin.stops.Length,3,"Origin location");
			Assert.AreEqual(cjpRequest.destination.stops.Length,2,"Destination location");
			Assert.IsTrue(cjpRequest.depart,"ArriveBefore");

			DateTime testDateTime = new DateTime(testOutwardDateTime.Year, testOutwardDateTime.Month, testOutwardDateTime.Day, 0, 0, 0);

			Assert.AreEqual(cjpRequest.origin.stops[0].timeDate,testDateTime,"Date/time");
			Assert.AreEqual(cjpRequest.publicParameters.interval,(DateTime.MinValue + new TimeSpan(23, 59, 59)),"Interval");
		
			//
			// Test 10 - return journey, arrive before, future date, any time 
			//

			request = requestData.InitialiseDefaultFlightRequest(false);

			request.ReturnArriveBefore = true;
			request.ReturnAnyTime = true;
            
			testReturnDateTime = DateTime.Now + new TimeSpan(20, 23, 40, 0, 0);
			request.ReturnDateTime = new TDDateTime[1];
			request.ReturnDateTime[0] = new TDDateTime(testReturnDateTime);
	
			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 2, "Number of CJPCall objects returned");

			Assert.IsTrue(calls[1].IsReturnJourney);
			Assert.AreEqual(calls[1].RequestId, "0000-0000-0000-1234-4322");

			cjpRequest = calls[1].CJPRequest;

			Assert.AreEqual(cjpRequest.origin.stops.Length,2,"Origin location");
			Assert.AreEqual(cjpRequest.destination.stops.Length,3,"Destination location");
			Assert.IsTrue(!cjpRequest.depart,"ArriveBefore");

			testDateTime = new DateTime(testReturnDateTime.Year, testReturnDateTime.Month, testReturnDateTime.Day, 0, 0, 0);
			testDateTime = testDateTime.AddDays(1);

			Assert.AreEqual(cjpRequest.destination.stops[0].timeDate,testDateTime,"Date/time");
			Assert.AreEqual(cjpRequest.publicParameters.interval,(DateTime.MinValue + new TimeSpan(23, 59, 59)),"Interval");

		
			//
			// Test 11 - outward journey, leave after, future date, specified time 
			//

			request = requestData.InitialiseDefaultFlightRequest(true);
			
			request.OutwardArriveBefore = false;
			request.OutwardAnyTime = false;
            
			testOutwardDateTime = DateTime.Now + new TimeSpan(7, 6, 0, 0, 0);
			request.OutwardDateTime = new TDDateTime[1];
			request.OutwardDateTime[0] = new TDDateTime(testOutwardDateTime);
	
			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 1, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-4321");

			cjpRequest = calls[0].CJPRequest;

			Assert.AreEqual(cjpRequest.origin.stops.Length,3,"Origin location");
			Assert.AreEqual(cjpRequest.destination.stops.Length,2,"Destination location");
			Assert.IsTrue(cjpRequest.depart,"ArriveBefore");

			testDateTime = testOutwardDateTime.Subtract(new TimeSpan(2, 0, 0));

			Assert.AreEqual(cjpRequest.origin.stops[0].timeDate,testDateTime,"Date/time");
			Assert.AreEqual(cjpRequest.publicParameters.interval,(DateTime.MinValue + new TimeSpan(23, 59, 59)),"Interval");

		
			//
			// Test 12 - return journey, arrive before, future date, specified 
			//

			request = requestData.InitialiseDefaultFlightRequest(false);
			
			request.ReturnArriveBefore = true;
			request.ReturnAnyTime = false;
            
			testReturnDateTime = DateTime.Now + new TimeSpan(29, 23, 59, 0, 0);
			request.ReturnDateTime = new TDDateTime[1];
			request.ReturnDateTime[0] = new TDDateTime(testReturnDateTime);
	
			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 1, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 2, "Number of CJPCall objects returned");

			Assert.IsTrue(calls[1].IsReturnJourney);
			Assert.AreEqual(calls[1].RequestId, "0000-0000-0000-1234-0002");

			cjpRequest = calls[1].CJPRequest;

			Assert.AreEqual(cjpRequest.origin.stops.Length,2,"Origin location");
			Assert.AreEqual(cjpRequest.destination.stops.Length,3,"Destination location");
			Assert.IsTrue(!cjpRequest.depart,"ArriveBefore");

			testDateTime = testReturnDateTime.Subtract(new TimeSpan(21, 0, 0));
			testDateTime = testDateTime.AddDays(1);

			Assert.AreEqual(cjpRequest.destination.stops[0].timeDate,testDateTime,"Date/time");
			Assert.AreEqual(cjpRequest.publicParameters.interval,(DateTime.MinValue + new TimeSpan(23, 59, 59)),"Interval");
			Assert.AreEqual(cjpRequest.publicParameters.intermediateStops, IntermediateStopsType.None);

		}
        */

        /* Commented out as ITP no longer used
		[Test]
		public void TestPopulateTrunkParameters()
		{
			//
			// Test 1 - outward journey, leave after, current date, specified time (now), rail only 
			//
			
			ITDJourneyRequest request = requestData.InitialiseDefaultTrunkRequest(true);

			request.Modes = new ModeType[] { ModeType.Rail};
			request.PublicAlgorithm = PublicAlgorithmType.Max3Changes;	

			DateTime testOutwardDateTime = outwardDateTime;
			DateTime testReturnDateTime  = returnDateTime;

			JourneyRequestPopulator populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			CJPCall[] calls = populator.PopulateRequests(1234, 4321, "session", false, 1, "en-gb");
			Assert.AreEqual(calls.Length, 1, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-4321");

			JourneyRequest cjpRequest = calls[0].CJPRequest;

			Assert.AreEqual(cjpRequest.requestID, "0000-0000-0000-1234-4321");
			Assert.AreEqual(cjpRequest.language, "en-gb");
			Assert.AreEqual(cjpRequest.sessionID, "session");
			Assert.IsFalse(cjpRequest.referenceTransaction);
			Assert.AreEqual(cjpRequest.userType, 1);

			Assert.IsTrue(cjpRequest.modeFilter.include);
			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Rail);

			// input has 3 origin, 2 destn stations, of which 2/1 are rail, respectively 
			Assert.AreEqual(cjpRequest.origin.stops.Length,2,"Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints,null,"Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName,"OriginName","Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length,1,"Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints,null,"Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName,"DestinationName","Destination location");

			Assert.IsTrue(cjpRequest.origin.stops[0].timeDate >= testOutwardDateTime, "Date/time");
			Assert.IsTrue(cjpRequest.origin.stops[1].timeDate >= testOutwardDateTime, "Date/time");

			testOutwardDateTime = testOutwardDateTime.AddMinutes(1);

			Assert.IsTrue(cjpRequest.origin.stops[0].timeDate <= testOutwardDateTime,"Date/time");

			Assert.AreEqual(cjpRequest.operatorFilter,null,"Operators");
			Assert.IsTrue(cjpRequest.depart,"ArriveBefore");

			Assert.IsTrue(cjpRequest.publicParameters.interval <= (DateTime.MinValue + new TimeSpan(22, 1, 0)),"Interval");
			Assert.IsTrue(cjpRequest.publicParameters.interval >= (DateTime.MinValue + new TimeSpan(21, 59, 0)),"Interval");
			Assert.AreEqual(cjpRequest.publicParameters.sequence,0,"Sequence");
			Assert.AreEqual(cjpRequest.publicParameters.rangeType,RangeType.Interval,"RangeType");
			Assert.AreEqual(cjpRequest.publicParameters.intermediateStops, IntermediateStopsType.All);
			Assert.AreEqual(cjpRequest.publicParameters.algorithm, PublicAlgorithmType.Max3Changes);
		

			//
			// Test 2 - return journey, leave after, current date, specified time (one hour in future) 
			//

			request = requestData.InitialiseDefaultTrunkRequest(false);

			request.Modes = new ModeType[] { ModeType.Rail};
			
			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 2, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-4321");
			Assert.IsTrue(calls[1].IsReturnJourney);
			Assert.AreEqual(calls[1].RequestId, "0000-0000-0000-1234-4322");

			cjpRequest = calls[1].CJPRequest;

			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "DestinationName", "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length,2,"Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints,null,"Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName,"OriginName","Destination location");

			Assert.IsTrue(cjpRequest.modeFilter.include);
			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Rail);

			testReturnDateTime = DateTime.Now - new TimeSpan(0, 1, 0);

			Assert.IsTrue(cjpRequest.origin.stops[0].timeDate >= testReturnDateTime, "Date/time");

			testReturnDateTime = testReturnDateTime.AddMinutes(1);

			Assert.IsTrue(cjpRequest.origin.stops[0].timeDate <= testReturnDateTime, "Date/time");

			Assert.IsTrue(cjpRequest.depart, "ArriveBefore");

			Assert.IsTrue(cjpRequest.publicParameters.interval <= (DateTime.MinValue + new TimeSpan(23, 1, 0)), "Interval");
			Assert.IsTrue(cjpRequest.publicParameters.interval >= (DateTime.MinValue + new TimeSpan(22, 59, 0)), "Interval");
			Assert.AreEqual(cjpRequest.publicParameters.sequence, 0, "Sequence");
			Assert.AreEqual(cjpRequest.publicParameters.rangeType,RangeType.Interval, "RangeType");
			Assert.AreEqual(cjpRequest.publicParameters.intermediateStops, IntermediateStopsType.All);
			Assert.AreEqual(cjpRequest.publicParameters.algorithm, PublicAlgorithmType.NoChanges);

			//
			// Test 3 - outward journey, leave after, current date, specified time (now), coach only 
			//
			
			request = requestData.InitialiseDefaultTrunkRequest(true);

			request.Modes = new ModeType[] { ModeType.Coach};
			request.PublicAlgorithm = PublicAlgorithmType.Default;	

			testOutwardDateTime = outwardDateTime;
			testReturnDateTime  = returnDateTime;

			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", false, 1, "en-gb");
			Assert.AreEqual(calls.Length, 1, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-4321");

			cjpRequest = calls[0].CJPRequest;

			Assert.AreEqual(cjpRequest.requestID, "0000-0000-0000-1234-4321");
			Assert.AreEqual(cjpRequest.language, "en-gb");
			Assert.AreEqual(cjpRequest.sessionID, "session");
			Assert.IsFalse(cjpRequest.referenceTransaction);
			Assert.AreEqual(cjpRequest.userType, 1);

			Assert.IsTrue(cjpRequest.modeFilter.include);
			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 3);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Coach);
			Assert.AreEqual(cjpRequest.modeFilter.modes[1].mode, ModeType.Bus);
			Assert.AreEqual(cjpRequest.modeFilter.modes[2].mode, ModeType.Ferry);

			// input has 3 origin, 2 destn stations, of which 1/1 are coach
			Assert.AreEqual(cjpRequest.origin.stops.Length,1,"Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints,null,"Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName,"OriginName","Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length,1,"Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints,null,"Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName,"DestinationName","Destination location");

			Assert.IsTrue(cjpRequest.origin.stops[0].timeDate >= testOutwardDateTime, "Date/time");

			testOutwardDateTime = testOutwardDateTime.AddMinutes(1);

			Assert.IsTrue(cjpRequest.origin.stops[0].timeDate <= testOutwardDateTime,"Date/time");

			Assert.AreEqual(cjpRequest.operatorFilter,null,"Operators");
			Assert.IsTrue(cjpRequest.depart,"ArriveBefore");

			Assert.IsTrue(cjpRequest.publicParameters.interval <= (DateTime.MinValue + new TimeSpan(22, 1, 0)),"Interval");
			Assert.IsTrue(cjpRequest.publicParameters.interval >= (DateTime.MinValue + new TimeSpan(21, 59, 0)),"Interval");
			Assert.AreEqual(cjpRequest.publicParameters.sequence,0,"Sequence");
			Assert.AreEqual(cjpRequest.publicParameters.rangeType,RangeType.Interval,"RangeType");
			Assert.AreEqual(cjpRequest.publicParameters.intermediateStops, IntermediateStopsType.All , "RangeType");
			Assert.AreEqual(cjpRequest.publicParameters.algorithm, PublicAlgorithmType.Default);
		

			//
			// Test 5 - outward only, three modes (so three CJPCalls created) 
			//

			request = requestData.InitialiseDefaultTrunkRequest(true);

			request.Modes = new ModeType[] { ModeType.Rail, ModeType.Coach, ModeType.Air};
			request.PublicAlgorithm = PublicAlgorithmType.Default;	
			request.DirectFlightsOnly = true;
		
			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 3, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-4321");
			Assert.IsFalse(calls[1].IsReturnJourney);
			Assert.AreEqual(calls[1].RequestId, "0000-0000-0000-1234-4322");
			Assert.IsFalse(calls[2].IsReturnJourney);
			Assert.AreEqual(calls[2].RequestId, "0000-0000-0000-1234-4323");

			Assert.IsTrue(calls[0].CJPRequest.modeFilter.include);
			Assert.AreEqual(calls[0].CJPRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(calls[0].CJPRequest.modeFilter.modes[0].mode, ModeType.Rail);
			Assert.AreEqual(calls[0].CJPRequest.publicParameters.intermediateStops, IntermediateStopsType.All);
			Assert.AreEqual(calls[0].CJPRequest.publicParameters.algorithm, PublicAlgorithmType.Default);

			Assert.IsTrue(calls[1].CJPRequest.modeFilter.include);
			Assert.AreEqual(calls[1].CJPRequest.modeFilter.modes.Length, 3);
			Assert.AreEqual(calls[1].CJPRequest.modeFilter.modes[0].mode, ModeType.Coach);
			Assert.AreEqual(calls[1].CJPRequest.modeFilter.modes[1].mode, ModeType.Bus);
			Assert.AreEqual(calls[1].CJPRequest.modeFilter.modes[2].mode, ModeType.Ferry);
			Assert.AreEqual(calls[1].CJPRequest.publicParameters.intermediateStops, IntermediateStopsType.All);
			Assert.AreEqual(calls[1].CJPRequest.publicParameters.algorithm, PublicAlgorithmType.Default);

			Assert.IsTrue(calls[2].CJPRequest.modeFilter.include);
			Assert.AreEqual(calls[2].CJPRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(calls[2].CJPRequest.modeFilter.modes[0].mode, ModeType.Air);
			Assert.AreEqual(calls[2].CJPRequest.publicParameters.intermediateStops, IntermediateStopsType.None);
			Assert.AreEqual(calls[2].CJPRequest.publicParameters.algorithm, PublicAlgorithmType.NoChanges);

			//
			// Test 5 - outward and return, three modes (so six CJPCalls created) 
			//

			request = requestData.InitialiseDefaultTrunkRequest(false);

			request.Modes = new ModeType[] { ModeType.Rail, ModeType.Coach, ModeType.Air};
			
			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 0, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 6, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-0000");
			Assert.IsTrue(calls[1].IsReturnJourney);
			Assert.AreEqual(calls[1].RequestId, "0000-0000-0000-1234-0001");
			Assert.IsFalse(calls[2].IsReturnJourney);
			Assert.AreEqual(calls[2].RequestId, "0000-0000-0000-1234-0002");
			Assert.IsTrue(calls[3].IsReturnJourney);
			Assert.AreEqual(calls[3].RequestId, "0000-0000-0000-1234-0003");
			Assert.IsFalse(calls[4].IsReturnJourney);
			Assert.AreEqual(calls[4].RequestId, "0000-0000-0000-1234-0004");
			Assert.IsTrue(calls[5].IsReturnJourney);
			Assert.AreEqual(calls[5].RequestId, "0000-0000-0000-1234-0005");

			Assert.IsTrue(calls[0].CJPRequest.modeFilter.include);
			Assert.AreEqual(calls[0].CJPRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(calls[0].CJPRequest.modeFilter.modes[0].mode, ModeType.Rail);

			Assert.IsTrue(calls[1].CJPRequest.modeFilter.include);
			Assert.AreEqual(calls[1].CJPRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(calls[1].CJPRequest.modeFilter.modes[0].mode, ModeType.Rail);

			Assert.IsTrue(calls[2].CJPRequest.modeFilter.include);
			Assert.AreEqual(calls[2].CJPRequest.modeFilter.modes.Length, 3);
			Assert.AreEqual(calls[2].CJPRequest.modeFilter.modes[0].mode, ModeType.Coach);
			Assert.AreEqual(calls[2].CJPRequest.modeFilter.modes[1].mode, ModeType.Bus);
			Assert.AreEqual(calls[2].CJPRequest.modeFilter.modes[2].mode, ModeType.Ferry);

			Assert.IsTrue(calls[3].CJPRequest.modeFilter.include);
			Assert.AreEqual(calls[3].CJPRequest.modeFilter.modes.Length, 3);
			Assert.AreEqual(calls[3].CJPRequest.modeFilter.modes[0].mode, ModeType.Coach);
			Assert.AreEqual(calls[3].CJPRequest.modeFilter.modes[1].mode, ModeType.Bus);
			Assert.AreEqual(calls[3].CJPRequest.modeFilter.modes[2].mode, ModeType.Ferry);

			Assert.IsTrue(calls[4].CJPRequest.modeFilter.include);
			Assert.AreEqual(calls[4].CJPRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(calls[4].CJPRequest.modeFilter.modes[0].mode, ModeType.Air);

			Assert.IsTrue(calls[5].CJPRequest.modeFilter.include);
			Assert.AreEqual(calls[5].CJPRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(calls[5].CJPRequest.modeFilter.modes[0].mode, ModeType.Air);

			//
			// Test 6 - two outward and and three return dates, three modes
			//			(so total of 2x3 + 3x3 = 15 CJPCalls created) 
			//

			request = requestData.InitialiseDefaultTrunkRequest(false);

			request.Modes = new ModeType[] { ModeType.Rail, ModeType.Coach, ModeType.Air};
			
			request.OutwardDateTime = new TDDateTime[2];
			request.OutwardDateTime[0] = new TDDateTime(outwardDateTime);
			request.OutwardDateTime[1] = new TDDateTime(outwardDateTime + new TimeSpan(1, 0, 0, 0, 0));

			request.ReturnDateTime = new TDDateTime[3];
			request.ReturnDateTime[0] = new TDDateTime(returnDateTime);
			request.ReturnDateTime[1] = new TDDateTime(returnDateTime + new TimeSpan(1, 0, 0, 0, 0));
			request.ReturnDateTime[2] = new TDDateTime(returnDateTime + new TimeSpan(2, 0, 0, 0, 0));

			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 0, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 15, "Number of CJPCall objects returned");

			//
			// Test 7 - one outward and and three return dates, two modes
			//			(so total of 1x2 + 3x2 = 8 CJPCalls created) 
			//

			request = requestData.InitialiseDefaultTrunkRequest(false);

			request.Modes = new ModeType[] { ModeType.Rail, ModeType.Coach };
			
			request.OutwardDateTime = new TDDateTime[1];
			request.OutwardDateTime[0] = new TDDateTime(outwardDateTime);

			request.ReturnDateTime = new TDDateTime[3];
			request.ReturnDateTime[0] = new TDDateTime(returnDateTime);
			request.ReturnDateTime[1] = new TDDateTime(returnDateTime + new TimeSpan(1, 0, 0, 0, 0));
			request.ReturnDateTime[2] = new TDDateTime(returnDateTime + new TimeSpan(2, 0, 0, 0, 0));

			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 0, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 8, "Number of CJPCall objects returned");

		}
        */

		/// <summary>
		/// Tests PublicViaLocations, PublicSoftViaLocations and PublicNotViaLocation 
		/// in the cjpRequest for both Multimodal and Trunk requests.
		/// </summary>
		[Test]
		public void TestViaLocations()
		{
			ITDJourneyRequest request = requestData.InitialiseDefaultRequest(true);
			
			JourneyRequestPopulator populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			CJPCall[] calls = populator.PopulateRequests(1234, 4321, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 1, "Number of CJPCall objects returned");

			JourneyRequest cjpRequest = calls[0].CJPRequest;

			Assert.AreEqual(cjpRequest.publicParameters.vias.Length,1,"Vias array length != 1");
			Assert.AreEqual(cjpRequest.publicParameters.vias[0].givenName,"Reigate","Vias array length != 1");
			
			Assert.AreEqual(cjpRequest.publicParameters.softVias.Length,1,"softVias array length != 1");
			Assert.AreEqual(cjpRequest.publicParameters.softVias[0].givenName,"Redhill","softVias array length != 1");
		
			Assert.AreEqual(cjpRequest.publicParameters.notVias.Length,1,"notVias array length != 1");
			Assert.AreEqual(cjpRequest.publicParameters.notVias[0].givenName,"Bath","notVias array length != 1");

			request = requestData.InitialiseDefaultRequest(true);
			
			request.PublicViaLocations = new TDLocation[0];
			request.PublicSoftViaLocations = new TDLocation[0];
			request.PublicNotViaLocations = new TDLocation[0];

			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 1, "Number of CJPCall objects returned");

			cjpRequest = calls[0].CJPRequest;

			Assert.AreEqual(cjpRequest.publicParameters.vias.Length,0);
			Assert.AreEqual(cjpRequest.publicParameters.softVias.Length,0);
			Assert.AreEqual(cjpRequest.publicParameters.notVias.Length,0);

			request = requestData.InitialiseDefaultTrunkRequest(true);
			
			request.PublicViaLocations = new TDLocation[0];
			request.PublicSoftViaLocations = new TDLocation[0];
			request.PublicNotViaLocations = new TDLocation[0];

			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 1, "Number of CJPCall objects returned");

			cjpRequest = calls[0].CJPRequest;

			Assert.AreEqual(cjpRequest.publicParameters.vias.Length,0);
			Assert.AreEqual(cjpRequest.publicParameters.softVias.Length,0);
			Assert.AreEqual(cjpRequest.publicParameters.notVias.Length,0);

			request = requestData.InitialiseDefaultTrunkRequest(true);
			
			request.PublicViaLocations[0].Status = TDLocationStatus.Ambiguous;
			request.PublicSoftViaLocations[0].Status = TDLocationStatus.Ambiguous;
			request.PublicNotViaLocations[0].Status = TDLocationStatus.Ambiguous;

			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 1, "Number of CJPCall objects returned");

			cjpRequest = calls[0].CJPRequest;

			Assert.AreEqual(cjpRequest.publicParameters.vias.Length,0);
			Assert.AreEqual(cjpRequest.publicParameters.softVias.Length,0);
			Assert.AreEqual(cjpRequest.publicParameters.notVias.Length,0);

		}

		/// <summary>
		/// Tests population of the ServiceFilter and OperatorFilter.
		/// Note that the code that does this is common to both
		/// multimodal and trunk requests, so don't need to test both.  
		/// </summary>
		[Test]
		public void TestServiceAndOperatorFilters()
		{
			ITDJourneyRequest request = requestData.InitialiseDefaultRequest(true);

			request.SelectedOperators = new string[] { "XX", "YY", "ZZ" };
			request.UseOnlySpecifiedOperators = true;

			request.TrainUidFilterIsInclude = true;
			request.TrainUidFilter = new string[] { "C00001", "G00001", "Y54321", "Y11111", "G11111" };

			JourneyRequestPopulator populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			CJPCall[] calls = populator.PopulateRequests(1234, 4321, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 1, "Number of CJPCall objects returned");

			JourneyRequest cjpRequest = calls[0].CJPRequest;

			Assert.IsTrue(cjpRequest.serviceFilter.include, "TrainUidFilterInclude");
			Assert.AreEqual(cjpRequest.serviceFilter.services.Length, 5);
			Assert.AreEqual(((RequestServicePrivate)cjpRequest.serviceFilter.services[0]).privateID,"C00001", "serviceFilter.services populated incorrectly");
			Assert.AreEqual(((RequestServicePrivate)cjpRequest.serviceFilter.services[1]).privateID,"G00001", "serviceFilter.services populated incorrectly");
			Assert.AreEqual(((RequestServicePrivate)cjpRequest.serviceFilter.services[2]).privateID,"Y54321", "serviceFilter.services populated incorrectly");
			Assert.AreEqual(((RequestServicePrivate)cjpRequest.serviceFilter.services[3]).privateID,"Y11111", "serviceFilter.services populated incorrectly");
			Assert.AreEqual(((RequestServicePrivate)cjpRequest.serviceFilter.services[4]).privateID,"G11111", "serviceFilter.services populated incorrectly");

			Assert.IsTrue(cjpRequest.operatorFilter.include, "OperatorFilterInclude");
			Assert.AreEqual(cjpRequest.operatorFilter.operatorCodes.Length, 3);
			Assert.AreEqual(cjpRequest.operatorFilter.operatorCodes[0].operatorCode, "XX");
			Assert.AreEqual(cjpRequest.operatorFilter.operatorCodes[1].operatorCode, "YY");
			Assert.AreEqual(cjpRequest.operatorFilter.operatorCodes[2].operatorCode, "ZZ");

			request = requestData.InitialiseDefaultRequest(true);

			request.SelectedOperators = new string[] { "XX", "YY" };
			request.UseOnlySpecifiedOperators = false;

			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 1, "Number of CJPCall objects returned");

			cjpRequest = calls[0].CJPRequest;

			Assert.IsNull(cjpRequest.serviceFilter);

			Assert.IsFalse(cjpRequest.operatorFilter.include, "OperatorFilterInclude");
			Assert.AreEqual(cjpRequest.operatorFilter.operatorCodes.Length, 2);
			Assert.AreEqual(cjpRequest.operatorFilter.operatorCodes[0].operatorCode, "XX");
			Assert.AreEqual(cjpRequest.operatorFilter.operatorCodes[1].operatorCode, "YY");

			request = requestData.InitialiseDefaultRequest(true);

			request.TrainUidFilterIsInclude = false;
			request.TrainUidFilter = new string[] { "A00001", "B00001" };

			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", false, 0, "en-gb");
			Assert.AreEqual(calls.Length, 1, "Number of CJPCall objects returned");

			cjpRequest = calls[0].CJPRequest;

			Assert.IsNull(cjpRequest.operatorFilter);

			Assert.IsFalse(cjpRequest.serviceFilter.include, "TrainUidFilterInclude");
			Assert.AreEqual(cjpRequest.serviceFilter.services.Length, 2);
			Assert.AreEqual(((RequestServicePrivate)cjpRequest.serviceFilter.services[0]).privateID,"A00001", "serviceFilter.services populated incorrectly");
			Assert.AreEqual(((RequestServicePrivate)cjpRequest.serviceFilter.services[1]).privateID,"B00001", "serviceFilter.services populated incorrectly");

		}
	
	
		[Test]
		public void TestCityToCityParameters()
		{
			//
			// Test 1 - outward journey, leave after, current date, specified time (now)
			//			trunk rail, coach and air calls created, but no combined air, 
			//			because no direct flights between origin and destination
			
			ITDJourneyRequest request = requestData.InitialiseDefaultCityToCityRequest(true);

			request.Modes = new ModeType[] { ModeType.Rail, ModeType.Coach, ModeType.Air };

			DateTime testOutwardDateTime = outwardDateTime;
			DateTime testReturnDateTime  = returnDateTime;

			JourneyRequestPopulator populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			CJPCall[] calls = populator.PopulateRequests(1234, 4321, "session", false, 1, "en-gb");
			Assert.AreEqual(calls.Length, 3, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-4321");
			Assert.IsFalse(calls[1].IsReturnJourney);
			Assert.AreEqual(calls[1].RequestId, "0000-0000-0000-1234-4322");
			Assert.IsFalse(calls[2].IsReturnJourney);
			Assert.AreEqual(calls[2].RequestId, "0000-0000-0000-1234-4323");

			JourneyRequest cjpRequest = calls[0].CJPRequest;

			Assert.AreEqual(cjpRequest.requestID, "0000-0000-0000-1234-4321");
			Assert.AreEqual(cjpRequest.language, "en-gb");
			Assert.AreEqual(cjpRequest.sessionID, "session");
			Assert.IsFalse(cjpRequest.referenceTransaction);
			Assert.AreEqual(cjpRequest.userType, 1);

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Rail);
			
			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "OriginName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9100Naptan1", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00001234", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "DestinationName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9100Naptan3", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00004321", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");


			cjpRequest = calls[1].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 3);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Coach);
			Assert.AreEqual(cjpRequest.modeFilter.modes[1].mode, ModeType.Bus);
			Assert.AreEqual(cjpRequest.modeFilter.modes[2].mode, ModeType.Ferry);

			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "OriginName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9000Naptan2", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00001234", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "DestinationName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9000Naptan4", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00004321", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");


			cjpRequest = calls[2].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Air);

			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "OriginName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9200Naptan3", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00001234", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "DestinationName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9200Naptan2", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00004321", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");


			//
			// Test 2 - outward journey, leave after, current date, specified time (now)
			//			trunk rail coach and air calls created, and also combined air, 
			//			because there are direct flights between origin and destination
			
			request = requestData.InitialiseDefaultCityToCityRequest(true);

			request.Modes = new ModeType[] { ModeType.Rail, ModeType.Coach, ModeType.Air };
			request.OriginLocation.NaPTANs[2].Naptan = "9200LHR";   
			request.DestinationLocation.NaPTANs[1].Naptan = "9200INV";

			testOutwardDateTime = outwardDateTime;
			testReturnDateTime  = returnDateTime;

			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", false, 1, "en-gb");
			Assert.AreEqual(calls.Length, 4, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-4321");
			Assert.IsFalse(calls[1].IsReturnJourney);
			Assert.AreEqual(calls[1].RequestId, "0000-0000-0000-1234-4322");
			Assert.IsFalse(calls[2].IsReturnJourney);
			Assert.AreEqual(calls[2].RequestId, "0000-0000-0000-1234-4323");
			Assert.IsFalse(calls[3].IsReturnJourney);
			Assert.AreEqual(calls[3].RequestId, "0000-0000-0000-1234-4324");

			cjpRequest = calls[0].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Rail);
	
			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "OriginName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9100Naptan1", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00001234", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "DestinationName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9100Naptan3", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00004321", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");

		
			cjpRequest = calls[1].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 3);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Coach);
			Assert.AreEqual(cjpRequest.modeFilter.modes[1].mode, ModeType.Bus);
			Assert.AreEqual(cjpRequest.modeFilter.modes[2].mode, ModeType.Ferry);

			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "OriginName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9000Naptan2", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00001234", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "DestinationName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9000Naptan4", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00004321", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");


			cjpRequest = calls[2].CJPRequest;

            // Should be two modes as there is a CityInterchange with Rail specified on the Origin and Destination location
			Assert.AreEqual(2, cjpRequest.modeFilter.modes.Length);
            Assert.AreEqual(ModeType.Rail, cjpRequest.modeFilter.modes[0].mode);
            Assert.AreEqual(ModeType.Air, cjpRequest.modeFilter.modes[1].mode);
			
			// uncomment this when new CJP interface arrives
			// Assert.AreEqual(cjpRequest.secondaryModeFilter.modes.Length, 1);
			// Assert.AreEqual(cjpRequest.secondaryModeFilter.modes[0].mode, ModeType.Rail);

			Assert.AreEqual(cjpRequest.requestID, "0000-0000-0000-1234-4323");
			Assert.AreEqual(cjpRequest.language, "en-gb");
			Assert.AreEqual(cjpRequest.sessionID, "session");
			Assert.IsFalse(cjpRequest.referenceTransaction);
			Assert.AreEqual(cjpRequest.userType, 1);

			Assert.AreEqual(cjpRequest.publicParameters.vias.Length, 1);
			Assert.AreEqual(cjpRequest.publicParameters.vias[0].stops.Length, 4);
			Assert.AreEqual(cjpRequest.publicParameters.vias[0].stops[0].NaPTANID, "9200LHR1");
			Assert.AreEqual(cjpRequest.publicParameters.vias[0].type, RequestPlaceType.NaPTAN);
            Assert.AreEqual(cjpRequest.publicParameters.vias[0].stops[1].NaPTANID, "9200LHR2");
            Assert.AreEqual(cjpRequest.publicParameters.vias[0].stops[2].NaPTANID, "9200LHR3");
            Assert.AreEqual(cjpRequest.publicParameters.vias[0].stops[3].NaPTANID, "9200LHR4");

			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "OriginName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9100Naptan1", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00001234", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "DestinationName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9100Naptan3", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00004321", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");


			cjpRequest = calls[3].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Air);

			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "OriginName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9200LHR", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00001234", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "DestinationName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9200INV", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00004321", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");


			//
			// Test 3 - outward journey, leave after, current date, specified time (now)
			//			trunk coach calls created, but no combined air, or direct air
			//			because useCombined = false on origin and useDirect false on destination
			
			request = requestData.InitialiseDefaultCityToCityRequest(true);

			request.Modes = new ModeType[] { ModeType.Coach, ModeType.Air };
			request.OriginLocation.NaPTANs[2].Naptan = "9200LHR";   
			request.OriginLocation.CityInterchanges[2] = new CityAirport(new ModeType[] { ModeType.Rail },  true, false, request.OriginLocation.NaPTANs[2]);
			request.DestinationLocation.NaPTANs[1].Naptan = "9200INV";
			request.DestinationLocation.CityInterchanges[1] = new CityAirport(new ModeType[] { ModeType.Rail },  false, true, request.DestinationLocation.NaPTANs[1]);

			testOutwardDateTime = outwardDateTime;
			testReturnDateTime  = returnDateTime;

			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 1, "session", false, 1, "en-gb");
			Assert.AreEqual(calls.Length, 2, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-0001");

			cjpRequest = calls[0].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 3);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Coach);
			Assert.AreEqual(cjpRequest.modeFilter.modes[1].mode, ModeType.Bus);
			Assert.AreEqual(cjpRequest.modeFilter.modes[2].mode, ModeType.Ferry);
	
			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "OriginName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9000Naptan2", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00001234", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "DestinationName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9000Naptan4", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00004321", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");

		
			//
			// Test 4 - return journey, leave after, current date, specified time (now)
			//			trunk rail and air calls created, and also combined air for return, 
			//			because there are direct flights between destination and origin,
			//			but not for outward (no direct flights)	
			
            // ITP No longet used - so comment out and update tests no longer relevant
			request = requestData.InitialiseDefaultCityToCityRequest(false);

			request.Modes = new ModeType[] { ModeType.Rail, ModeType.Air };
			request.OriginLocation.NaPTANs[2].Naptan = "9200INV";   
			request.DestinationLocation.NaPTANs[1].Naptan = "9200LHR";

			testOutwardDateTime = outwardDateTime;
			testReturnDateTime  = returnDateTime;

			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", false, 1, "en-gb");
			Assert.AreEqual(calls.Length, 4, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-4321");
			Assert.IsTrue(calls[1].IsReturnJourney);
			Assert.AreEqual(calls[1].RequestId, "0000-0000-0000-1234-4322");
            //Assert.IsTrue(calls[2].IsReturnJourney);
            //Assert.AreEqual(calls[2].RequestId, "0000-0000-0000-1234-4323");
			Assert.IsFalse(calls[2].IsReturnJourney);
			Assert.AreEqual(calls[2].RequestId, "0000-0000-0000-1234-4323");
			Assert.IsTrue(calls[3].IsReturnJourney);
			Assert.AreEqual(calls[3].RequestId, "0000-0000-0000-1234-4324");


			cjpRequest = calls[0].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Rail);
	
			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "OriginName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9100Naptan1", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00001234", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "DestinationName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9100Naptan3", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00004321", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");

			
			cjpRequest = calls[1].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Rail);
	
			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "DestinationName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9100Naptan3", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00004321", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "OriginName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9100Naptan1", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00001234", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");

			
            //cjpRequest = calls[2].CJPRequest;

            //Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
            //Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Air);

            //// uncomment this when new CJP interface arrives
            //// Assert.AreEqual(cjpRequest.secondaryModeFilter.modes.Length, 1);
            //// Assert.AreEqual(cjpRequest.secondaryModeFilter.modes[0].mode, ModeType.Rail);

            //Assert.AreEqual(cjpRequest.requestID, "0000-0000-0000-1234-4323");
            //Assert.AreEqual(cjpRequest.language, "en-gb");
            //Assert.AreEqual(cjpRequest.sessionID, "session");
            //Assert.IsFalse(cjpRequest.referenceTransaction);
            //Assert.AreEqual(cjpRequest.userType, 1);

            //Assert.AreEqual(cjpRequest.publicParameters.vias.Length, 1);
            //Assert.AreEqual(cjpRequest.publicParameters.vias[0].stops.Length, 1);
            //Assert.AreEqual(cjpRequest.publicParameters.vias[0].stops[0].NaPTANID, "9200LHR");
            //Assert.AreEqual(cjpRequest.publicParameters.vias[0].type, RequestPlaceType.NaPTAN);

            //Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
            //Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
            //Assert.AreEqual(cjpRequest.origin.givenName, "DestinationName", "Origin location");
            //Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9100Naptan3", "Origin location");
            //Assert.AreEqual(cjpRequest.origin.locality, "E00004321", "Origin location");
            //Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
            //Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
            //Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
            //Assert.AreEqual(cjpRequest.destination.givenName, "OriginName", "Destination location");
            //Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9100Naptan1", "Destination location");
            //Assert.AreEqual(cjpRequest.destination.locality, "E00001234", "Destination location");
            //Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");

			cjpRequest = calls[2].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Air);

			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "OriginName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9200INV", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00001234", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "DestinationName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9200LHR", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00004321", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");

			cjpRequest = calls[3].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Air);

			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "DestinationName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9200LHR", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00004321", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "OriginName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9200INV", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00001234", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");

			//
			// Test 4 - return journey, arrive before, current date, specified time (4.5 hrs ahead)
			//			trunk rail call created but no direct air, and also combined air for out & return 

            // ITP No longer used - so comment out and update tests no longer relevant
            request = requestData.InitialiseDefaultCityToCityRequest(false);

			request.Modes = new ModeType[] { ModeType.Rail, ModeType.Air };
			request.OriginLocation.NaPTANs[2].Naptan = "9200EDB";   
			request.OriginLocation.CityInterchanges[2] = new CityAirport(new ModeType[] { ModeType.Rail },  false, true, request.OriginLocation.NaPTANs[2]);

			request.DestinationLocation.NaPTANs[1].Naptan = "9200LHR";
			request.DestinationLocation.CityInterchanges[1] = new CityAirport(new ModeType[] { ModeType.Coach },  false, true, request.DestinationLocation.NaPTANs[1]);

			request.OutwardArriveBefore = true;	
			request.ReturnArriveBefore  = true;	
				
			request.OutwardDateTime[0] = new TDDateTime(DateTime.Now + new TimeSpan(4, 15, 0));
			request.ReturnDateTime[0]  = new TDDateTime(DateTime.Now + new TimeSpan(4, 30, 0));

			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", false, 1, "en-gb");
			Assert.AreEqual(calls.Length, 4, "Number of CJPCall objects returned");

			Assert.IsFalse(calls[0].IsReturnJourney);
			Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-4321");
			Assert.IsTrue(calls[1].IsReturnJourney);
			Assert.AreEqual(calls[1].RequestId, "0000-0000-0000-1234-4322");
			Assert.IsFalse(calls[2].IsReturnJourney);
			Assert.AreEqual(calls[2].RequestId, "0000-0000-0000-1234-4323");
			Assert.IsTrue(calls[3].IsReturnJourney);
			Assert.AreEqual(calls[3].RequestId, "0000-0000-0000-1234-4324");

			cjpRequest = calls[0].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Rail);
	
			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "OriginName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9100Naptan1", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00001234", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "DestinationName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9100Naptan3", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00004321", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");

			
			cjpRequest = calls[1].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Rail);
	
			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "DestinationName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9100Naptan3", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00004321", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "OriginName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9100Naptan1", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00001234", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");

		
			cjpRequest = calls[2].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Air);

			// uncomment this when new CJP interface arrives
			// Assert.AreEqual(cjpRequest.secondaryModeFilter.modes.Length, 2);
			// Assert.AreEqual(cjpRequest.secondaryModeFilter.modes[0].mode, ModeType.Rail);
			// Assert.AreEqual(cjpRequest.secondaryModeFilter.modes[1].mode, ModeType.Coach);

			Assert.AreEqual(cjpRequest.requestID, "0000-0000-0000-1234-4323");
			Assert.AreEqual(cjpRequest.language, "en-gb");
			Assert.AreEqual(cjpRequest.sessionID, "session");
			Assert.IsFalse(cjpRequest.referenceTransaction);
			Assert.AreEqual(cjpRequest.userType, 1);

            //Assert.AreEqual(cjpRequest.publicParameters.vias.Length, 1);
            //Assert.AreEqual(cjpRequest.publicParameters.vias[0].stops.Length, 1);
            //Assert.AreEqual(cjpRequest.publicParameters.vias[0].stops[0].NaPTANID, "9200EDB");
            //Assert.AreEqual(cjpRequest.publicParameters.vias[0].type, RequestPlaceType.NaPTAN);

			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "OriginName", "Origin location");
			//Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9100Naptan1", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00001234", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "DestinationName", "Destination location");
			//Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9000Naptan4", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00004321", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");

			cjpRequest = calls[3].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Air);

			// uncomment this when new CJP interface arrives
			// Assert.AreEqual(cjpRequest.secondaryModeFilter.modes.Length, 2);
			// Assert.AreEqual(cjpRequest.secondaryModeFilter.modes[0].mode, ModeType.Rail);
			// Assert.AreEqual(cjpRequest.secondaryModeFilter.modes[1].mode, ModeType.Coach);

			Assert.AreEqual(cjpRequest.requestID, "0000-0000-0000-1234-4324");
			Assert.AreEqual(cjpRequest.language, "en-gb");
			Assert.AreEqual(cjpRequest.sessionID, "session");
			Assert.IsFalse(cjpRequest.referenceTransaction);
			Assert.AreEqual(cjpRequest.userType, 1);

            //Assert.AreEqual(cjpRequest.publicParameters.vias.Length, 1);
            //Assert.AreEqual(cjpRequest.publicParameters.vias[0].stops.Length, 1);
            //Assert.AreEqual(cjpRequest.publicParameters.vias[0].stops[0].NaPTANID, "9200LHR");
            //Assert.AreEqual(cjpRequest.publicParameters.vias[0].type, RequestPlaceType.NaPTAN);

			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "DestinationName", "Origin location");
			//Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9000Naptan4", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00004321", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "OriginName", "Destination location");
			//Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9100Naptan1", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00001234", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");


			//
			// Test 5 - return journey, depart after/arrive before, current date, specified time (1.5 hrs ahead)
			//			trunk coach and direct air calls, and also combined air for out & return 
			
			request = requestData.InitialiseDefaultCityToCityRequest(false);

			request.Modes = new ModeType[] { ModeType.Coach, ModeType.Air };

			request.OriginLocation = new TDLocation();
			request.OriginLocation.Description = "OriginName";
			request.OriginLocation.Locality = "E00001234";
			request.OriginLocation.NaPTANs = new TDNaptan[5];
			request.OriginLocation.NaPTANs[0] = new TDNaptan();
			request.OriginLocation.NaPTANs[1] = new TDNaptan();
			request.OriginLocation.NaPTANs[2] = new TDNaptan();
			request.OriginLocation.NaPTANs[3] = new TDNaptan();
			request.OriginLocation.NaPTANs[4] = new TDNaptan();
			request.OriginLocation.NaPTANs[0].Naptan = "9100Naptan1";
			request.OriginLocation.NaPTANs[0].Locality =  "E00001234";
			request.OriginLocation.NaPTANs[0].Name = "OriginName";
			request.OriginLocation.NaPTANs[1].Naptan = "9000Naptan2";
			request.OriginLocation.NaPTANs[1].Locality =  "E00001234";
			request.OriginLocation.NaPTANs[1].Name = "OriginName";
			request.OriginLocation.NaPTANs[2].Naptan = "9200LHR";
			request.OriginLocation.NaPTANs[2].Locality =  "E00001234";
			request.OriginLocation.NaPTANs[2].Name = "OriginName";
			request.OriginLocation.NaPTANs[3].Naptan = "9200GTW";
			request.OriginLocation.NaPTANs[3].Locality =  "E00001234";
			request.OriginLocation.NaPTANs[3].Name = "OriginName";
			request.OriginLocation.NaPTANs[4].Naptan = "9200LUT";
			request.OriginLocation.NaPTANs[4].Locality =  "E00001234";
			request.OriginLocation.NaPTANs[4].Name = "OriginName";

			
			request.OriginLocation.NaPTANs[0].GridReference = new OSGridReference(123, 456); 
			request.OriginLocation.NaPTANs[1].GridReference = new OSGridReference(1234, 4567); 
			request.OriginLocation.NaPTANs[2].GridReference = new OSGridReference(1234, 4567); 
			request.OriginLocation.NaPTANs[3].GridReference = new OSGridReference(1234, 4567); 
			request.OriginLocation.NaPTANs[4].GridReference = new OSGridReference(1234, 4567); 
			request.OriginLocation.Toid = new string[] { "1234", "5678"};

			request.OriginLocation.CityInterchanges = new CityInterchange[5];

			request.OriginLocation.CityInterchanges[0] = new CityRailStation(request.OriginLocation.NaPTANs[0]);
			request.OriginLocation.CityInterchanges[1] = new CityCoachStation(request.OriginLocation.NaPTANs[1]);
			request.OriginLocation.CityInterchanges[2] = new CityAirport(new ModeType[] { ModeType.Coach },  true, true, request.OriginLocation.NaPTANs[2]);
			request.OriginLocation.CityInterchanges[3] = new CityAirport(new ModeType[] { ModeType.Rail },  false, true, request.OriginLocation.NaPTANs[3]);
			request.OriginLocation.CityInterchanges[4] = new CityAirport(new ModeType[] { ModeType.Rail },  true, true, request.OriginLocation.NaPTANs[4]);

			request.DestinationLocation = new TDLocation();
			request.DestinationLocation.Description = "DestinationName";
			request.DestinationLocation.Locality = "E00004321";
			request.DestinationLocation.NaPTANs = new TDNaptan[5];
			request.DestinationLocation.NaPTANs[0] = new TDNaptan();
			request.DestinationLocation.NaPTANs[1] = new TDNaptan();
			request.DestinationLocation.NaPTANs[2] = new TDNaptan();
			request.DestinationLocation.NaPTANs[3] = new TDNaptan();
			request.DestinationLocation.NaPTANs[4] = new TDNaptan();
			request.DestinationLocation.NaPTANs[0].Naptan = "9100Naptan3";
			request.DestinationLocation.NaPTANs[0].Locality = "E00004321";
			request.DestinationLocation.NaPTANs[0].Name = "DestinationName";
			request.DestinationLocation.NaPTANs[1].Naptan = "9100Naptan4";
			request.DestinationLocation.NaPTANs[1].Locality = "E00004321";
			request.DestinationLocation.NaPTANs[1].Name = "DestinationName";
			request.DestinationLocation.NaPTANs[2].Naptan = "9000Naptan4";
			request.DestinationLocation.NaPTANs[2].Locality = "E00004321";
			request.DestinationLocation.NaPTANs[2].Name = "DestinationName";
			request.DestinationLocation.NaPTANs[3].Naptan = "9200EDB";
			request.DestinationLocation.NaPTANs[3].Locality = "E00004321";
			request.DestinationLocation.NaPTANs[3].Name = "DestinationName";
			request.DestinationLocation.NaPTANs[4].Naptan = "9200INV";
			request.DestinationLocation.NaPTANs[4].Locality = "E00004321";
			request.DestinationLocation.NaPTANs[4].Name = "DestinationName";
			request.DestinationLocation.NaPTANs[0].GridReference = new OSGridReference(123, 456); 
			request.DestinationLocation.NaPTANs[1].GridReference = new OSGridReference(1234, 4567); 
			request.DestinationLocation.NaPTANs[2].GridReference = new OSGridReference(1234, 4567); 
			request.DestinationLocation.NaPTANs[3].GridReference = new OSGridReference(1234, 4567); 
			request.DestinationLocation.NaPTANs[4].GridReference = new OSGridReference(1234, 4567); 
			request.DestinationLocation.Toid = new string[] { "ABCDE", "FGHIJ", "KLMNO"};

			request.DestinationLocation.CityInterchanges = new CityInterchange[5];

			request.DestinationLocation.CityInterchanges[0] = new CityRailStation(request.DestinationLocation.NaPTANs[0]);
			request.DestinationLocation.CityInterchanges[1] = new CityRailStation(request.DestinationLocation.NaPTANs[1]);
			request.DestinationLocation.CityInterchanges[2] = new CityCoachStation(request.DestinationLocation.NaPTANs[2]);
			request.DestinationLocation.CityInterchanges[3] = new CityAirport(new ModeType[] { ModeType.Rail },   true, true, request.DestinationLocation.NaPTANs[3]);
			request.DestinationLocation.CityInterchanges[4] = new CityAirport(new ModeType[] { ModeType.Coach },  true, true, request.DestinationLocation.NaPTANs[4]);
	
			request.OutwardArriveBefore = true;	
			request.ReturnArriveBefore  = true;	
				
			request.OutwardDateTime[0] = new TDDateTime(DateTime.Now + new TimeSpan(1, 15, 0));
			request.ReturnDateTime[0]  = new TDDateTime(DateTime.Now + new TimeSpan(1, 1, 30, 0, 0));

			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			calls = populator.PopulateRequests(1234, 4321, "session", false, 1, "en-gb");
			Assert.AreEqual(calls.Length, 4, "Number of CJPCall objects returned");

			// trunk coach calls
			//Assert.IsFalse(calls[0].IsReturnJourney);
			//Assert.AreEqual(calls[0].RequestId, "0000-0000-0000-1234-4321");
			//Assert.IsTrue(calls[1].IsReturnJourney);
			//Assert.AreEqual(calls[1].RequestId, "0000-0000-0000-1234-4322");

			// combined air calls
			//Assert.IsFalse(calls[2].IsReturnJourney);
			//Assert.AreEqual(calls[2].RequestId, "0000-0000-0000-1234-4323");
			//Assert.IsTrue(calls[3].IsReturnJourney);
			//Assert.AreEqual(calls[3].RequestId, "0000-0000-0000-1234-4324");
			//Assert.IsFalse(calls[4].IsReturnJourney);
			//Assert.AreEqual(calls[4].RequestId, "0000-0000-0000-1234-4325");
			//Assert.IsFalse(calls[5].IsReturnJourney);
			//Assert.AreEqual(calls[5].RequestId, "0000-0000-0000-1234-4326");

			// trunk air calls
			//Assert.IsFalse(calls[6].IsReturnJourney);
			//Assert.AreEqual(calls[6].RequestId, "0000-0000-0000-1234-4327");
			//Assert.IsTrue(calls[7].IsReturnJourney);
			//Assert.AreEqual(calls[7].RequestId, "0000-0000-0000-1234-4328");

            /* Commented out as ITP not longer used
			cjpRequest = calls[0].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 3);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Coach);
			Assert.AreEqual(cjpRequest.modeFilter.modes[1].mode, ModeType.Bus);
			Assert.AreEqual(cjpRequest.modeFilter.modes[2].mode, ModeType.Ferry);
	
			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "OriginName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9000Naptan2", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00001234", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "DestinationName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9000Naptan4", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00004321", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");

			
			cjpRequest = calls[1].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 3);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Coach);
			Assert.AreEqual(cjpRequest.modeFilter.modes[1].mode, ModeType.Bus);
			Assert.AreEqual(cjpRequest.modeFilter.modes[2].mode, ModeType.Ferry);
	
			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "DestinationName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9000Naptan4", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00004321", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "OriginName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9000Naptan2", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00001234", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");

		
			cjpRequest = calls[2].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Air);

			// uncomment this when new CJP interface arrives
			// Assert.AreEqual(cjpRequest.secondaryModeFilter.modes.Length, 2);
			// Assert.AreEqual(cjpRequest.secondaryModeFilter.modes[0].mode, ModeType.Rail);
			// Assert.AreEqual(cjpRequest.secondaryModeFilter.modes[1].mode, ModeType.Coach);

			Assert.AreEqual(cjpRequest.publicParameters.vias.Length, 1);
			Assert.AreEqual(cjpRequest.publicParameters.vias[0].stops.Length, 1);
			Assert.AreEqual(cjpRequest.publicParameters.vias[0].stops[0].NaPTANID, "9200LHR");
			Assert.AreEqual(cjpRequest.publicParameters.vias[0].type, RequestPlaceType.NaPTAN);

			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "OriginName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9000Naptan2", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00001234", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 2, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "DestinationName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9100Naptan3", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[1].NaPTANID, "9100Naptan4", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00004321", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");

			cjpRequest = calls[3].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Air);

			// uncomment this when new CJP interface arrives
			// Assert.AreEqual(cjpRequest.secondaryModeFilter.modes.Length, 2);
			// Assert.AreEqual(cjpRequest.secondaryModeFilter.modes[0].mode, ModeType.Rail);
			// Assert.AreEqual(cjpRequest.secondaryModeFilter.modes[1].mode, ModeType.Coach);

			Assert.AreEqual(cjpRequest.publicParameters.vias.Length, 1);
			Assert.AreEqual(cjpRequest.publicParameters.vias[0].stops.Length, 1);
			Assert.AreEqual(cjpRequest.publicParameters.vias[0].stops[0].NaPTANID, "9200EDB");
			Assert.AreEqual(cjpRequest.publicParameters.vias[0].type, RequestPlaceType.NaPTAN);

			Assert.AreEqual(cjpRequest.origin.stops.Length, 2, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "DestinationName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9100Naptan3", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[1].NaPTANID, "9100Naptan4", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00004321", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "OriginName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9000Naptan2", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00001234", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");


			cjpRequest = calls[4].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Air);

			// uncomment this when new CJP interface arrives
			// Assert.AreEqual(cjpRequest.secondaryModeFilter.modes.Length, 1);
			// Assert.AreEqual(cjpRequest.secondaryModeFilter.modes[0].mode, ModeType.Coach);

			Assert.AreEqual(cjpRequest.publicParameters.vias.Length, 1);
			Assert.AreEqual(cjpRequest.publicParameters.vias[0].stops.Length, 1);
			Assert.AreEqual(cjpRequest.publicParameters.vias[0].stops[0].NaPTANID, "9200LHR");
			Assert.AreEqual(cjpRequest.publicParameters.vias[0].type, RequestPlaceType.NaPTAN);

			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "OriginName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9000Naptan2", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00001234", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 1, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "DestinationName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9000Naptan4", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00004321", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");

			cjpRequest = calls[5].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Air);

			// uncomment this when new CJP interface arrives
			// Assert.AreEqual(cjpRequest.secondaryModeFilter.modes.Length, 1);
			// Assert.AreEqual(cjpRequest.secondaryModeFilter.modes[0].mode, ModeType.Rail);

			Assert.AreEqual(cjpRequest.publicParameters.vias.Length, 1);
			Assert.AreEqual(cjpRequest.publicParameters.vias[0].stops.Length, 1);
			Assert.AreEqual(cjpRequest.publicParameters.vias[0].stops[0].NaPTANID, "9200LUT");
			Assert.AreEqual(cjpRequest.publicParameters.vias[0].type, RequestPlaceType.NaPTAN);

			Assert.AreEqual(cjpRequest.origin.stops.Length, 1, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "OriginName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9100Naptan1", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00001234", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 2, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "DestinationName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9100Naptan3", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[1].NaPTANID, "9100Naptan4", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00004321", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");


			cjpRequest = calls[6].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Air);

			Assert.AreEqual(cjpRequest.origin.stops.Length, 2, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "OriginName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9200LHR", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[1].NaPTANID, "9200LUT", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00001234", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 2, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "DestinationName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9200EDB", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[1].NaPTANID, "9200INV", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00004321", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");


			cjpRequest = calls[7].CJPRequest;

			Assert.AreEqual(cjpRequest.modeFilter.modes.Length, 1);
			Assert.AreEqual(cjpRequest.modeFilter.modes[0].mode, ModeType.Air);

			Assert.AreEqual(cjpRequest.origin.stops.Length, 2, "Origin location");
			Assert.AreEqual(cjpRequest.origin.roadPoints, null, "Origin location");
			Assert.AreEqual(cjpRequest.origin.givenName, "DestinationName", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[0].NaPTANID, "9200EDB", "Origin location");
			Assert.AreEqual(cjpRequest.origin.stops[1].NaPTANID, "9200INV", "Origin location");
			Assert.AreEqual(cjpRequest.origin.locality, "E00004321", "Origin location");
			Assert.AreEqual(cjpRequest.origin.type, RequestPlaceType.NaPTAN, "Origin location");
		
			Assert.AreEqual(cjpRequest.destination.stops.Length, 2, "Destination location");
			Assert.AreEqual(cjpRequest.destination.roadPoints, null, "Destination location");
			Assert.AreEqual(cjpRequest.destination.givenName, "OriginName", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[0].NaPTANID, "9200LHR", "Destination location");
			Assert.AreEqual(cjpRequest.destination.stops[1].NaPTANID, "9200LUT", "Destination location");
			Assert.AreEqual(cjpRequest.destination.locality, "E00001234", "Destination location");
			Assert.AreEqual(cjpRequest.destination.type, RequestPlaceType.NaPTAN, "Destination location");
            */
		}
	}



	public class TestJRPInitialisation : IServiceInitialisation
	{
		public TestJRPInitialisation()
		{
		}

		public void Populate(Hashtable serviceCache)
		{
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			serviceCache.Add(ServiceDiscoveryKey.Cache, new TestJourneyControlMockCache());
			serviceCache.Add(ServiceDiscoveryKey.AirDataProvider, new TestMockAirDataProvider());
            serviceCache.Add(ServiceDiscoveryKey.GisQuery, new TestStubGisQuery());
		}
	}

}
