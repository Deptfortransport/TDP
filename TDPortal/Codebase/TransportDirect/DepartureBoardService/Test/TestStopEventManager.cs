// *********************************************** 
// NAME                 : TestStopEventManager.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 12/01/2005
// DESCRIPTION  : Test class for the StopEventManager
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/Test/TestStopEventManager.cs-arc  $
//
//   Rev 1.1   Feb 17 2010 16:42:30   RHopkins
//Pass OperatorCode in StopEvent request
//Resolution for 5403: Pass OperatorCode in StopEvents request
//
//   Rev 1.0   Nov 08 2007 12:21:50   mturner
//Initial revision.
//
//   Rev 1.1   Mar 14 2005 15:16:32   schand
//Changes for configurable switch between CJP and RTTI.
//Modified test to switch between CJP and RTTI for TrainResult
//
//   Rev 1.0   Feb 28 2005 17:17:50   passuied
//Initial revision.
//
//   Rev 1.7   Feb 16 2005 14:54:28   passuied
//Change in interface and behaviour of time Request
//
//possibility to plan in the past within configurable time window
//
//   Rev 1.6   Feb 11 2005 11:08:24   passuied
//changes to comply to the new cjp
//
//   Rev 1.5   Jan 19 2005 14:01:54   passuied
//added more validation + changed UT to allow destination to be optional
//
//   Rev 1.4   Jan 18 2005 17:36:28   passuied
//changed after update of CjpInterface
//
//   Rev 1.3   Jan 17 2005 14:49:24   passuied
//Unit tests OK!
//
//   Rev 1.2   Jan 14 2005 20:59:10   passuied
//back up of uni tests under construction
//
//   Rev 1.1   Jan 14 2005 10:22:02   passuied
//changes in interface
//
//   Rev 1.0   Jan 12 2005 14:50:00   passuied
//Initial revision.

using System;
using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties  ;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using TransportDirect.UserPortal.DepartureBoardService.StopEventManager;
using TransportDirect.JourneyPlanning.CJPInterface;


namespace TransportDirect.UserPortal.DepartureBoardService.Test
{
	/// <summary>
	/// Test class for the StopEventManager
	/// </summary>
	[TestFixture]
	public class TestStopEventManager
	{
		bool isTrainResultProviderCJP = false;

		public TestStopEventManager()
		{   			
		}

		[SetUp]
		public void Init()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init( new MockServiceInitialisation());
			isTrainResultProviderCJP = GetTrainResultFromCJP();
		}

		[Test]
		public void TestBuildStopEventRequestBase()
		{
			// test without service Number, show Departures, show CallingStops
			DBSLocation stop = new DBSLocation();
			stop.Locality = "LOCALITY";
			stop.NaptanIds = new string[]{"9000GLDRSA", "9000GLDRSB"};
            EventRequest request = StopEventConverter.BuildStopEventRequest(stop, string.Empty, string.Empty, true, true);

			Assert.AreEqual(typeof(StopEventRequest), request.GetType());
			StopEventRequest seReq = request as StopEventRequest;
			Assert.AreEqual( EventArriveDepartType.Depart, seReq.arriveDepart);
			Assert.AreEqual( 2, request.NaPTANIDs.Length);
			Assert.AreEqual("9000GLDRSA", request.NaPTANIDs[0]);
			Assert.AreEqual( "9000GLDRSB", request.NaPTANIDs[1]);
			Assert.AreEqual(IntermediateStopsType.All, request.intermediateStops);
			Assert.AreEqual( null, request.serviceFilter);  			
			CheckMode(request,stop );
			Assert.AreEqual(RangeType.Sequence, seReq.rangeType);
			Assert.AreEqual(false, request.referenceTransaction );
			Assert.AreEqual(DateTime.Now.ToString("dd/MM HH:mm"), seReq.startTime.ToString("dd/MM HH:mm"));

			Assert.IsNull(request.originFilter);
			Assert.IsNull(request.destinationFilter);

			// test with service Number, show Arrivals, don't show CallingStops
			stop = new DBSLocation();
			stop.Locality = "LOCALITY";
			stop.NaptanIds = new string[]{"9000GLDRSA", "9000GLDRSB"};
            request = StopEventConverter.BuildStopEventRequest(stop, string.Empty, "A6", false, false);

			Assert.AreEqual(typeof(StopEventRequest), request.GetType());
			seReq = request as StopEventRequest;
			Assert.AreEqual(EventArriveDepartType.Arrive, seReq.arriveDepart);
			Assert.AreEqual(2, request.NaPTANIDs.Length);
			Assert.AreEqual("9000GLDRSA", request.NaPTANIDs[0]);
			Assert.AreEqual("9000GLDRSB", request.NaPTANIDs[1]);
			Assert.AreEqual(IntermediateStopsType.None, request.intermediateStops);
			Assert.AreEqual(1, request.serviceFilter.services.Length);
			Assert.AreEqual("A6", ((RequestServiceNumber)request.serviceFilter.services[0]).serviceNumber); 
			CheckMode(request,stop );
			Assert.AreEqual(RangeType.Sequence, seReq.rangeType);
			Assert.AreEqual(false, request.referenceTransaction);
			Assert.AreEqual(DateTime.Now.ToString("dd/MM HH:mm"), seReq.startTime.ToString("dd/MM HH:mm"));

			Assert.IsNull(request.originFilter);
			Assert.IsNull(request.destinationFilter);



		}

		[Test]
		public void TestBuildStopEventRequestOverloaded()
		{
			// test without service Number, show Departures, show CallingStops with arrival filter,
			// starting now with range 10
			DBSLocation stop = new DBSLocation();
			stop.Locality = "LOCALITY";
			stop.NaptanIds = new string[]{"9000GLDRSA", "9000GLDRSB"};

			DBSLocation stopFilter = new DBSLocation();
			stopFilter.Locality = "LOCALITY2";
			stopFilter.NaptanIds = new string[]{"9000STND26", "9000STND27"};

			// change value of time for Unit tests!
			DBSTimeRequest time = new DBSTimeRequest();
			time.Type = TimeRequestType.Now;

            EventRequest request = StopEventConverter.BuildStopEventRequest(stop, string.Empty, string.Empty, true, true, stopFilter, time, DBSRangeType.Sequence, 10);

			Assert.AreEqual(typeof(StopEventRequest), request.GetType());
			StopEventRequest seReq = request as StopEventRequest;
			Assert.AreEqual(EventArriveDepartType.Depart, seReq.arriveDepart );
			Assert.AreEqual(2, request.NaPTANIDs.Length); 
			Assert.AreEqual("9000GLDRSA",request.NaPTANIDs[0]);
			Assert.AreEqual("9000GLDRSB",request.NaPTANIDs[1]);
			Assert.AreEqual(IntermediateStopsType.All, request.intermediateStops);
			Assert.IsNull(request.serviceFilter);

			CheckMode(request,stop );
			Assert.AreEqual(RangeType.Sequence, seReq.rangeType);
			Assert.AreEqual(false, request.referenceTransaction);
			Assert.AreEqual(DateTime.Now.ToString("dd/MM HH:mm"), seReq.startTime.ToString("dd/MM HH:mm"));
			
			Assert.AreEqual(10, seReq.sequence);
			
			Assert.AreEqual(null,request.originFilter);
			Assert.AreEqual(false, request.destinationFilter.actual);
			Assert.AreEqual(2, request.destinationFilter.NaPTANIDs.Length);
			Assert.AreEqual("9000STND26",request.destinationFilter.NaPTANIDs[0] );
			Assert.AreEqual("9000STND27", request.destinationFilter.NaPTANIDs[1] );


			// test with service Number, show arrivals, don't show CallingStops with origin filter,
			// starting at specific hour:minute  with range 10
			stop = new DBSLocation();
			stop.Locality = "LOCALITY";
			stop.NaptanIds = new string[]{"9000GLDRSA", "9000GLDRSB"};

			stopFilter = new DBSLocation();
			stopFilter.Locality = "LOCALITY2";
			stopFilter.NaptanIds = new string[]{"9000STND26", "9000STND27"};

			time = new DBSTimeRequest();
			time.Type = TimeRequestType.TimeToday;
			time.Hour = DateTime.Now.AddHours(1).Hour;
			time.Minute = 10;
			
			request = StopEventConverter.BuildStopEventRequest(
                                                stop,
                                                string.Empty, 
												"A6", 
												false, 
												false, 
												stopFilter, 
												time,
												DBSRangeType.Sequence,
												10);

			Assert.AreEqual(typeof(StopEventRequest), request.GetType());
			seReq = request as StopEventRequest;

			Assert.AreEqual(EventArriveDepartType.Arrive, seReq.arriveDepart);
			Assert.AreEqual(2, request.NaPTANIDs.Length );
			Assert.AreEqual("9000GLDRSA", request.NaPTANIDs[0] );
			Assert.AreEqual("9000GLDRSB", request.NaPTANIDs[1] );
			Assert.AreEqual(IntermediateStopsType.None, request.intermediateStops );
			Assert.AreEqual(1, request.serviceFilter.services.Length);
			Assert.AreEqual("A6", ((RequestServiceNumber)request.serviceFilter.services[0]).serviceNumber );
			
			CheckMode(request,stop );
			
			Assert.AreEqual(RangeType.Sequence, seReq.rangeType );
			Assert.AreEqual(false, request.referenceTransaction );
			DateTime expTime = DateTime.Today;
			expTime = expTime.AddHours(DateTime.Now.AddHours(1).Hour);
			expTime = expTime.AddMinutes(10);
			Assert.AreEqual(expTime.ToString("dd/MM HH:mm"), seReq.startTime.ToString("dd/MM HH:mm"));
			
			Assert.AreEqual(10, seReq.sequence);
			
			Assert.AreEqual(null, request.destinationFilter );
			Assert.AreEqual(false, request.originFilter.actual);
			Assert.AreEqual(2, request.originFilter.NaPTANIDs.Length);
			Assert.AreEqual("9000STND26", request.originFilter.NaPTANIDs[0] );
			Assert.AreEqual("9000STND27", request.originFilter.NaPTANIDs[1] );

			// test with service Number, show arrivals, don't show CallingStops with origin filter,
			// starting in 2 hours in past (==> tomorrow) with range 10
			stop = new DBSLocation();
			stop.Locality = "LOCALITY";
			stop.NaptanIds = new string[]{"9000GLDRSA", "9000GLDRSB"};

			stopFilter = new DBSLocation();
			stopFilter.Locality = "LOCALITY2";
			stopFilter.NaptanIds = new string[]{"9000STND26", "9000STND27"};

			time = new DBSTimeRequest();
			time.Type = TimeRequestType.TimeTomorrow;
			time.Hour = DateTime.Now.AddHours(-2).Hour;
			time.Minute = 10;

			request = StopEventConverter.BuildStopEventRequest(
				stop,
                string.Empty,
                "A6", 
				false, 
				false, 
				stopFilter, 
				time,
				DBSRangeType.Sequence,
				10);

			Assert.AreEqual(typeof(StopEventRequest), request.GetType());
			seReq = request as StopEventRequest;

			Assert.AreEqual( EventArriveDepartType.Arrive, seReq.arriveDepart);
			Assert.AreEqual(2, request.NaPTANIDs.Length );
			Assert.AreEqual("9000GLDRSA", request.NaPTANIDs[0] );
			Assert.AreEqual("9000GLDRSB", request.NaPTANIDs[1] );
			Assert.AreEqual(IntermediateStopsType.None, request.intermediateStops );
			
			Assert.AreEqual(1, request.serviceFilter.services.Length);
			Assert.AreEqual("A6", ((RequestServiceNumber)request.serviceFilter.services[0]).serviceNumber);
			
			CheckMode(request,stop );
			Assert.AreEqual(RangeType.Sequence, seReq.rangeType );
			Assert.AreEqual(false, request.referenceTransaction );

			expTime = DateTime.Today.AddDays(1);
			expTime = expTime.AddHours(DateTime.Now.AddHours(-2).Hour);
			expTime = expTime.AddMinutes(10);
			Assert.AreEqual( expTime.ToString("dd/MM HH:mm"), seReq.startTime.ToString("dd/MM HH:mm"));
			
			Assert.AreEqual(10, seReq.sequence );
			
			Assert.AreEqual(null, request.destinationFilter );
			Assert.AreEqual(false, request.originFilter.actual);
			Assert.AreEqual(2, request.originFilter.NaPTANIDs.Length);
			Assert.AreEqual("9000STND26", request.originFilter.NaPTANIDs[0] );
			Assert.AreEqual("9000STND27", request.originFilter.NaPTANIDs[1] );


			// test with service Number, show arrivals, don't show CallingStops with origin filter,
			// First service with range 10
			stop = new DBSLocation();
			stop.Locality = "LOCALITY";
			stop.NaptanIds = new string[]{"9000GLDRSA", "9000GLDRSB"};

			stopFilter = new DBSLocation();
			stopFilter.Locality = "LOCALITY2";
			stopFilter.NaptanIds = new string[]{"9000STND26", "9000STND27"};
		
			time = new DBSTimeRequest();
			time.Type = TimeRequestType.First;

			request = StopEventConverter.BuildStopEventRequest(
				stop,
                string.Empty,
                "A6", 
				false, 
				false, 
				stopFilter, 
				time,
				DBSRangeType.Sequence,
				10);

			Assert.AreEqual(typeof(FirstLastServiceRequest), request.GetType());
			FirstLastServiceRequest flReq = request as FirstLastServiceRequest;

		
			Assert.AreEqual(2, request.NaPTANIDs.Length );
			Assert.AreEqual("9000GLDRSA", request.NaPTANIDs[0] );
			Assert.AreEqual("9000GLDRSB", request.NaPTANIDs[1] );
			Assert.AreEqual(IntermediateStopsType.None, request.intermediateStops );
			Assert.AreEqual(1, request.serviceFilter.services.Length );
			Assert.AreEqual("A6", ((RequestServiceNumber)request.serviceFilter.services[0]).serviceNumber  );
			CheckMode(request,stop );
		
			Assert.AreEqual(false, request.referenceTransaction);
			
			// Assumes this test will always be run after 3AM
			Assert.AreEqual(DateTime.Today.AddDays(1).ToString("dd/MM HH:mm"), flReq.date.ToString("dd/MM HH:mm"));
			
			
			Assert.AreEqual(FirstLastServiceRequestType.First, flReq.type);

		
			
			Assert.AreEqual(null, request.destinationFilter );
			Assert.AreEqual(false, request.originFilter.actual);
			Assert.AreEqual(2, request.originFilter.NaPTANIDs.Length);
			Assert.AreEqual("9000STND26", request.originFilter.NaPTANIDs[0] );
			Assert.AreEqual("9000STND27", request.originFilter.NaPTANIDs[1] );


			// test with service Number, show arrivals, don't show CallingStops with origin filter,
			// Last service with range 10
			stop = new DBSLocation();
			stop.Locality = "LOCALITY";
			stop.NaptanIds = new string[]{"9000GLDRSA", "9000GLDRSB"};

			stopFilter = new DBSLocation();
			stopFilter.Locality = "LOCALITY2";
			stopFilter.NaptanIds = new string[]{"9000STND26", "9000STND27"};

			
			time = new DBSTimeRequest();
			time.Type = TimeRequestType.Last;

			request = StopEventConverter.BuildStopEventRequest(
				stop,
                string.Empty,
                "A6", 
				false, 
				false, 
				stopFilter, 
				time,
				DBSRangeType.Sequence,
				10);

			Assert.AreEqual(typeof(FirstLastServiceRequest), request.GetType());
			flReq = request as FirstLastServiceRequest;


			Assert.AreEqual(2, request.NaPTANIDs.Length );
			Assert.AreEqual("9000GLDRSA", request.NaPTANIDs[0] );
			Assert.AreEqual("9000GLDRSB", request.NaPTANIDs[1] );
			Assert.AreEqual(IntermediateStopsType.None, request.intermediateStops );
			Assert.AreEqual(1, request.serviceFilter.services.Length );
			Assert.AreEqual("A6", ((RequestServiceNumber)request.serviceFilter.services[0]).serviceNumber  );
			
			CheckMode(request,stop );
		
			Assert.AreEqual(false, request.referenceTransaction);
			
			// Assumes this test will always be run after 3AM
			Assert.AreEqual(DateTime.Today.ToString("dd/MM HH:mm"), flReq.date.ToString("dd/MM HH:mm"));
			
			Assert.AreEqual(FirstLastServiceRequestType.Last, flReq.type);


			
			Assert.AreEqual(null, request.destinationFilter);
			Assert.AreEqual(false, request.originFilter.actual);
			Assert.AreEqual(2, request.originFilter.NaPTANIDs.Length);
			Assert.AreEqual("9000STND26", request.originFilter.NaPTANIDs[0] );
			Assert.AreEqual("9000STND27", request.originFilter.NaPTANIDs[1] );


			// test with service Number, show departures, don't show CallingStops with no destination filter,
			// Last service with range 10
			stop = new DBSLocation();
			stop.Locality = "LOCALITY";
			stop.NaptanIds = new string[]{"9000GLDRSA", "9000GLDRSB"};

			
			
			time = new DBSTimeRequest();
			time.Type = TimeRequestType.Last;

			request = StopEventConverter.BuildStopEventRequest(
				stop,
                string.Empty,
                "A6", 
				true, 
				false, 
				null, 
				time,
				DBSRangeType.Sequence,
				10);

			Assert.AreEqual(typeof(FirstLastServiceRequest), request.GetType());
			flReq = request as FirstLastServiceRequest;

			
			Assert.AreEqual(2, request.NaPTANIDs.Length );
			Assert.AreEqual("9000GLDRSA", request.NaPTANIDs[0] );
			Assert.AreEqual("9000GLDRSB", request.NaPTANIDs[1] );
			Assert.AreEqual("LOCALITY", request.locality);
			Assert.AreEqual(IntermediateStopsType.None, request.intermediateStops );
			Assert.AreEqual(1, request.serviceFilter.services.Length );
			Assert.AreEqual("A6", ((RequestServiceNumber)request.serviceFilter.services[0]).serviceNumber  );
			
			CheckMode(request,stop );
			
			Assert.AreEqual(false, request.referenceTransaction );
			
			// Assumes this test will always be run after 3AM
			Assert.AreEqual(DateTime.Today.ToString("dd/MM HH:mm"), flReq.date.ToString("dd/MM HH:mm"));
			
			
			Assert.IsNull(request.destinationFilter);
			Assert.IsNull(request.originFilter);


			// test with service Number, show arrivals, don't show CallingStops with no destination filter,
			// Last service with range 10
			stop = new DBSLocation();
			stop.Locality = "LOCALITY";
			stop.NaptanIds = new string[]{"9000GLDRSA", "9000GLDRSB"};

			
			
			time = new DBSTimeRequest();
			time.Type = TimeRequestType.Last;

			request = StopEventConverter.BuildStopEventRequest(
				stop,
                string.Empty,
                "A6", 
				false, 
				false, 
				null, 
				time,
				DBSRangeType.Sequence,
				10);

			Assert.AreEqual(typeof(FirstLastServiceRequest), request.GetType());
			flReq = request as FirstLastServiceRequest;

			Assert.AreEqual(2, request.NaPTANIDs.Length );
			Assert.AreEqual("9000GLDRSA", request.NaPTANIDs[0] );
			Assert.AreEqual("9000GLDRSB", request.NaPTANIDs[1]);
			Assert.AreEqual("LOCALITY", request.locality);
			Assert.AreEqual(IntermediateStopsType.None, request.intermediateStops );
			Assert.AreEqual(1, request.serviceFilter.services.Length );
			Assert.AreEqual("A6", ((RequestServiceNumber)request.serviceFilter.services[0]).serviceNumber  );
			
			CheckMode(request,stop );

			Assert.AreEqual( false, request.referenceTransaction);
			
			// Assumes this test will always be run after 3AM
			Assert.AreEqual(DateTime.Today.ToString("dd/MM HH:mm"), flReq.date.ToString("dd/MM HH:mm"));
			
			
			Assert.IsNull(request.destinationFilter);
			Assert.IsNull(request.originFilter);


		}

		[Test]
		public void TestBuildDBSResult()
		{
		}


		private bool GetTrainResultFromCJP()
		{
			try
			{
				return  Convert.ToBoolean(Properties.Current[Keys.GetTrainInfoFromCJP].ToString())  ;				
			}
			catch(FormatException)
			{
				return false;
			} 
		}


		private void CheckMode(EventRequest request, DBSLocation stop )
		{   
			if (isTrainResultProviderCJP && stop.Type == LocationService.TDCodeType.CRS)		
			{
				Assert.AreEqual(1, request.modeFilter.modes.Length);				
				Assert.AreEqual(ModeType.Rail , request.modeFilter.modes[0].mode);				
			}
			else
			{
				Assert.AreEqual(2, request.modeFilter.modes.Length);				
				Assert.AreEqual(ModeType.Bus, request.modeFilter.modes[0].mode);
				Assert.AreEqual(ModeType.Coach, request.modeFilter.modes[1].mode);
			}
		}


	}
}
