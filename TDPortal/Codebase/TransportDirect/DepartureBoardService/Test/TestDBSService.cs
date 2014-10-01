// *********************************************** 
// NAME                 : TestDBSService.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 10/01/2005
// DESCRIPTION  : Test class for DBSService
// 
// ************************************************ 
//
// IMPORTANT NOTE: The socket listner need to be runnig in order for this test to work. This can be started by going to 
// C:\TDPortal\CodeBase\TransportDirect\DepartureBoardService\Test\MockListener and clicking on the SocketListner.exe
// file. 
// 
// If unit test still fails check the PermanentPortal Database. In the Properties table the value 
// DepartureBoardService.GetTrainInfoFromCJP should be set to 'true'.
//
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/Test/TestDBSService.cs-arc  $
//
//   Rev 1.1   Feb 17 2010 16:42:28   RHopkins
//Pass OperatorCode in StopEvent request
//Resolution for 5403: Pass OperatorCode in StopEvents request
//
//   Rev 1.0   Nov 08 2007 12:21:46   mturner
//Initial revision.
//
//   Rev 1.2   Feb 28 2006 11:54:34   halkatib
//Updated as fix for stream3129 merge. Advised tester of need to start SockListener.exe
//
//   Rev 1.1   Mar 31 2005 19:14:36   schand
//Fixed test script for new the changes (DBSValidation). Fix for 4.4, 4.5
//
//   Rev 1.0   Feb 28 2005 17:17:50   passuied
//Initial revision.
//
//   Rev 1.9   Feb 08 2005 16:10:02   RScott
//assertions changed to asserts
//
//   Rev 1.8   Jan 27 2005 11:01:12   passuied
//added ability to show callings stops in mock SE manager + updated test scripts
//
//   Rev 1.7   Jan 26 2005 10:50:26   passuied
//enhanced the stopevent mock manager and updated test scripts
//
//   Rev 1.6   Jan 19 2005 16:58:14   passuied
//added extra test for if optional dest is null!
//
//   Rev 1.5   Jan 19 2005 16:28:40   schand
//integration of RTTI + SE manager!
//
//   Rev 1.4   Jan 18 2005 17:36:22   passuied
//changed after update of CjpInterface
//
//   Rev 1.3   Jan 17 2005 14:49:18   passuied
//Unit tests OK!
//
//   Rev 1.2   Jan 14 2005 20:59:36   passuied
//back up of unit test. under construction
//
//   Rev 1.1   Jan 14 2005 10:22:02   passuied
//changes in interface
//
//   Rev 1.0   Jan 10 2005 16:37:10   passuied
//Initial revision.

using System;
using NUnit.Framework;

using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.DepartureBoardService.Test
{
	/// <summary>
	/// Test class for DBSService
	/// </summary>
	[TestFixture]
	public class TestDBSService
	{
		private IDepartureBoardService service = null;
		public TestDBSService()
		{
			
		}

		[SetUp]
		public void Init()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init( new TestInitialization());
			service = new DepartureBoardService();

			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockCodeGazetteerEmpty());
		}

		[Test]
		public void TestGetDepartureBoardStopInvalidRequest()
		{

			
			// Build an invalid Location request
			
			//location without CRS
			DBSLocation stop = new DBSLocation();
			DBSResult result = service.GetDepartureBoardStop(
								string.Empty,
								stop,
                                string.Empty,
								string.Empty,
								true,
								true);

			Assert.IsTrue(result.Messages.Length != 0);
			Assert.IsTrue(result.Messages[0].Code == (int)DBSMessageIdentifier.UserInvalidRequestLocationForOrigin);

			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CodeGazetteer, new MockCodeGazetteerEmpty());

			// location with CRS in wrong format
			stop.Code = "HKKLJK";
			result = service.GetDepartureBoardStop(
				string.Empty,
				stop,
                string.Empty,
                string.Empty,
				true,
				true);
			
			Assert.IsTrue(result.Messages.Length != 0);
			Assert.IsTrue(result.Messages[0].Code == (int)DBSMessageIdentifier.UserInvalidRequestLocationForOrigin);

			// location with CRS in correct format
			stop.Code = "EUS";
			stop.Type = TDCodeType.CRS;
			stop.NaptanIds = new string[]{"9100EUS1", "9100EUS1"};
			stop.Valid = true;

			result = service.GetDepartureBoardStop(
				string.Empty,
				stop,
                string.Empty,
				string.Empty,
				true,
				true);
			
			Assert.IsTrue(result.Messages.Length == 0);
			Assert.IsTrue(result.StopEvents.Length != 0);
		
			

			// build an invalid buscoach request
			
			// locality empty, Naptanid filled
			stop = new DBSLocation();
			stop.Type = TDCodeType.SMS;
			stop.NaptanIds = new string[]{"9000VCTR"};
			stop.Valid = true;
			result = service.GetDepartureBoardStop(
				string.Empty,
				stop,
                string.Empty,
				string.Empty,
				true,
				true);

			Assert.IsTrue(result.Messages.Length != 0);
			Assert.IsTrue(result.Messages[0].Code == (int)DBSMessageIdentifier.UserInvalidRequestLocationForOrigin);

			// locality filled, NaptanID empty
			stop = new DBSLocation();
			stop.Type = TDCodeType.SMS;
			stop.Valid = true;
			stop.Locality = "LOCALITY";

			result = service.GetDepartureBoardStop(
				string.Empty,
				stop,
                string.Empty,
				string.Empty,
				true,
				true);

			Assert.IsTrue(result.Messages.Length != 0);
			Assert.IsTrue(result.Messages[0].Code == (int)DBSMessageIdentifier.UserInvalidRequestLocationForOrigin);

			// buscoach request in good format
			stop = new DBSLocation();
			stop.Code = "default";
			stop.Type = TDCodeType.SMS;
			stop.Valid = true;
			stop.Locality = "LOCALITY";
			stop.NaptanIds = new string[]{"9000VCTR"};

			result = service.GetDepartureBoardStop(
				string.Empty,
				stop,
                string.Empty,
				string.Empty,
				true,
				false);

			Assert.IsTrue(result.Messages.Length == 0);
			Assert.IsTrue(result.StopEvents.Length != 0);

		}

		[Test]
		public void TestGetDepartureBoardTripInvalidRequest()
		{
			
			// Build an invalid Train request
			
			//location without CRS
			DBSLocation origin = new DBSLocation();
			DBSLocation destination = new DBSLocation();
			destination.Type = TDCodeType.CRS;
			origin.Type = TDCodeType.CRS;
			DBSTimeRequest time = new DBSTimeRequest();
			DBSResult result = service.GetDepartureBoardTrip(
				string.Empty,
				origin,
				destination,
                string.Empty,
				string.Empty,
				time,
				DBSRangeType.Sequence,
				5,
				true,
				true);

			Assert.IsTrue(result.Messages.Length != 0);
			Assert.IsTrue(result.Messages[0].Code == (int)DBSMessageIdentifier.UserInvalidRequestLocationForOrigin);

			// location with CRS in wrong format
			origin.Code = "HKKLJK";
			destination.Code = "EUS";
			destination.Type = TDCodeType.CRS;
			origin.Type = TDCodeType.CRS;
			result = service.GetDepartureBoardTrip(
				string.Empty,
				origin,
				destination,
                string.Empty,
				string.Empty,
				time,
				DBSRangeType.Sequence,
				5,
				true,
				true);
			
			Assert.IsTrue(result.Messages.Length != 0);
			Assert.IsTrue(result.Messages[0].Code == (int)DBSMessageIdentifier.UserInvalidRequestLocationForOriginAndDestination);

			// location with CRS in correct format
			origin.Code = "EUS";
			destination.Code = "MAN";
			destination.Type = TDCodeType.CRS;
			origin.Type = TDCodeType.CRS;
			origin.NaptanIds = new string[]{"9100EUS1", "9100EUS1"};
			destination.NaptanIds = new string[]{"9100EUS1", "9100EUS1"};
			origin.Valid = true;
			destination.Valid = true;

			
			result = service.GetDepartureBoardTrip(
				string.Empty,
				origin,
				destination,
                string.Empty,
                string.Empty,
				time,
				DBSRangeType.Sequence,
				5,
				true,
				true);
			
			Assert.IsTrue(result.Messages.Length == 0);
			Assert.IsTrue(result.StopEvents.Length != 0);
		
			

			// build an invalid buscoach request
			
			// locality empty, Naptanid filled
			origin = new DBSLocation();
			destination = new DBSLocation();
			origin.Type = TDCodeType.SMS;
			destination.Type = TDCodeType.SMS;
			origin.NaptanIds = new string[]{"9000VCTR"};
			destination.NaptanIds = new string[]{"9000WTRL"};
			origin.Valid = true;
			destination.Valid = true;
			result = service.GetDepartureBoardTrip(
				string.Empty,
				origin,
				destination,
                string.Empty,
                string.Empty,
				time,
				DBSRangeType.Sequence,
				5,
				true,
				true);

			Assert.IsTrue(result.Messages.Length != 0);
			Assert.IsTrue(result.Messages[0].Code == (int)DBSMessageIdentifier.UserInvalidRequestLocationForOrigin);

			// locality filled, NaptanID empty
			origin = new DBSLocation();
			destination = new DBSLocation();

			origin.Type = TDCodeType.SMS;
			destination.Type = TDCodeType.SMS;

			origin.Valid = true;
			destination.Valid = true;
			origin.Locality = "LOCALITY";
			destination.Locality = "LOCALITY";
			result = service.GetDepartureBoardTrip(
				string.Empty,
				origin,
				destination,
                string.Empty,
                string.Empty,
				time,
				DBSRangeType.Sequence,
				5,
				true,
				true);

			Assert.IsTrue(result.Messages.Length != 0);
			Assert.IsTrue(result.Messages[0].Code == (int)DBSMessageIdentifier.UserInvalidRequestLocationForOrigin);

			// buscoach request in good format
			origin = new DBSLocation();
			destination = new DBSLocation();

			origin.Code = "GLDSGLDS";
			destination.Code = "STNDSTND";
			origin.Locality = "LOCALITY";
			destination.Locality = "LOCALITY";
			origin.Type = TDCodeType.SMS;
			destination.Type = TDCodeType.SMS;
			origin.NaptanIds = new string[]{"9000VCTR"};
			destination.NaptanIds = new string[]{"9000WTRL"};
			origin.Valid = true;
			destination.Valid = true;

			result = service.GetDepartureBoardTrip(
				string.Empty,
				origin,
				destination,
                string.Empty,
                string.Empty,
				time,
				DBSRangeType.Sequence,
				5,
				true,
				false);

			Assert.IsTrue(result.Messages.Length == 0);
			Assert.IsTrue(result.StopEvents.Length != 0);
		}

		/// <summary>
		/// Test method that checks that when null given locations, it returns a message with Invalid request.
		/// </summary>
		[Test]
		public void TestNullLocationRequest()
		{
			DBSResult result = service.GetDepartureBoardStop(
				string.Empty,
				null,
                string.Empty,
				string.Empty,
				true,
				true);

				Assert.IsTrue(result.Messages.Length != 0);
				Assert.IsTrue(result.Messages[0].Code == (int)DBSMessageIdentifier.UserInvalidRequestLocationForOrigin);

			result = service.GetDepartureBoardStop(
				string.Empty,
				null,
                string.Empty,
				string.Empty,
				true,
				true);

			Assert.IsTrue(result.Messages.Length != 0);
			Assert.IsTrue(result.Messages[0].Code == (int)DBSMessageIdentifier.UserInvalidRequestLocationForOrigin );

			result = service.GetDepartureBoardTrip(
				string.Empty,
				null,
				null,
                string.Empty,
                string.Empty,
				new DBSTimeRequest(),
				DBSRangeType.Sequence,
				5,
				true,
				true);

			Assert.IsTrue(result.Messages.Length != 0);
			Assert.IsTrue(result.Messages[0].Code == (int)DBSMessageIdentifier.UserInvalidRequestLocationForOrigin);

			result = service.GetDepartureBoardTrip(
				string.Empty,
				null,
				null,
                string.Empty,
                string.Empty,
				new DBSTimeRequest(),
				DBSRangeType.Sequence,
				5,
				true,
				true);

			Assert.IsTrue(result.Messages.Length != 0);
			Assert.IsTrue(result.Messages[0].Code == (int)DBSMessageIdentifier.UserInvalidRequestLocationForOrigin );


			// Test that if only dest null. Accepted by system ==> no error messages
			
			DBSLocation origin = new DBSLocation();
			origin.Code = "default";
			origin.Locality = "LOCALITY";
			origin.Type = TDCodeType.SMS;
			origin.NaptanIds = new string[]{"9000VCTR"};
			origin.Valid = true;
		
			result = service.GetDepartureBoardTrip(
				string.Empty,
				origin,
				null,
                string.Empty,
                string.Empty,
				new DBSTimeRequest(),
				DBSRangeType.Sequence,
				5,
				true,
				false);

			Assert.IsTrue(result.Messages.Length == 0);
		}

		[Test]
		public void TestInvalidTimeRequest()
		{
			DBSLocation origin = new DBSLocation();
			DBSLocation destination = new DBSLocation();
			DBSResult result;

			origin = new DBSLocation();
			destination = new DBSLocation();

			origin.Type = TDCodeType.SMS;
			destination.Type = TDCodeType.SMS;

			origin.Valid = true;
			destination.Valid = true;
			origin.Locality = "LOCALITY";
			destination.Locality = "LOCALITY";
			result = service.GetDepartureBoardTrip(
				string.Empty,
				origin,
				destination,
                string.Empty,
                string.Empty,
				null,
				DBSRangeType.Sequence,
				5,
				true,
				true);

			Assert.IsTrue(result.Messages.Length != 0);
			Assert.IsTrue(result.Messages[0].Code == (int)DBSMessageIdentifier.UserInvalidRequestTime);

		}
	}
}
