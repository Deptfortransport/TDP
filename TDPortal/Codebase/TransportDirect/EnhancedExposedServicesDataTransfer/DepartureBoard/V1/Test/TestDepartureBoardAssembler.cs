// *********************************************** 
// NAME                 : TestDepartureBoardAssembler.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 16/01/2006 
// DESCRIPTION  	    : Test class for Departure Board Service Assember
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/DepartureBoard/V1/Test/TestDepartureBoardAssembler.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:26   mturner
//Initial revision.
//
//   Rev 1.2   Mar 27 2006 12:53:16   mtillett
//Fix assignment of DepartureTime to DataTransfer type (unit test updated).
//Resolution for 3648: Kizoom mobile - No actual times shown for departures
//
//   Rev 1.1   Jan 27 2006 18:56:20   schand
//Corrected the namespace
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Jan 27 2006 16:30:54   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 25 2006 11:37:46   schand
//Code reviews
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Jan 17 2006 15:44:06   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111


using System;
using System.Text;
using NUnit.Framework;
using TransportDirect.EnhancedExposedServices;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;   
using TransportDirect.UserPortal.LocationService;
using TransportDirect.EnhancedExposedServices.DataTransfer.DepartureBoard.V1;
using TransportDirect.UserPortal.DepartureBoardService;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.DepartureBoard.V1.Test
{
	/// <summary>
	/// Test class for Departure Board Service Assember
	/// </summary>
	[TestFixture]
	public class TestDepartureBoardAssembler
	{
		#region Class Members
		string originCode = "EUS";
		string destinationCode = "MAN";
		int requestHour = 20;
		int requestMinute = 10;
		int range = 5;
		string serviceNumber = string.Empty;
		bool showDepartures = false;
		DepartureBoardServiceRangeType rangeType = DepartureBoardServiceRangeType.Sequence;  
		DepartureBoardServiceTimeRequestType timeRequestType = DepartureBoardServiceTimeRequestType.Now;
		DBSActivityType dbsActivityType = DBSActivityType.Depart;
		DateTime arrivalTime = new DateTime(2006, 1, 17, 11, 10, 5);
		DateTime departTime = new DateTime(2006, 1, 17, 11, 11, 5);
		DateTime estimatedArrivalTime = new DateTime(2006, 1, 17, 11, 11, 5);		
		DateTime estimatedDepartTime = new DateTime(2006, 1, 17, 11, 12, 5);
		DBSRealTimeType dbsRealTimeTypeEstimated = DBSRealTimeType.Estimated; 
		DBSRealTimeType dbsRealTimeTypeRecorded = DBSRealTimeType.Recorded; 
		
		string arrivalStopName = "EUSTON";
		string arrivalShortCode = "EUS";
		string arrivalNaptanId = "9100EUSTON";
		string departStopName = "MANCHESTER PIC";
		string departShortCode = "MAN";
		string departNaptanId = "9100MNCRPIC";
		CallingStopStatus callingStopStatus = CallingStopStatus.Unknown; 
		DepartureBoardType departureBoardType = DepartureBoardType.Train;

		string operatorCode = "SL";
		string	operatorName = "Silver link";
		string operatorServiceNumber = "SL1";

		bool circularRoute = false;
		bool cancelled = true;
		string cancellationReason = "UNKNOWN";
		string via = "Harrow Wealdstone"; 
		string lateReason = "signal failure";
		string falseDestination = "WATFORD";
		#endregion

	   
		#region Test Methods
		/// <summary>
		/// Test for CreateDepartureBoardServiceInternalRequest Method
		/// </summary>
		[Test]
		public void TestCreateDepartureBoardServiceInternalRequest()
		{
			
			 // creating the instance of dto object 
			DepartureBoardServiceRequest departureBoardServiceRequest = CreateDepartureBoardServiceRequestInstance();
		     
			// Getting internal object from dto object
		   DepartureBoardServiceInternalRequest internalRequest = DepartureBoardAssembler.CreateDepartureBoardServiceInternalRequest(departureBoardServiceRequest);
  
			// now testing each params
			Assert.IsTrue(internalRequest!=null);
			Assert.IsTrue(internalRequest.OriginLocation.Code == originCode);
			Assert.IsTrue(internalRequest.DestinationLocation.Code == destinationCode);
			Assert.IsTrue(internalRequest.JourneyTimeInformation.Type.ToString()   == timeRequestType.ToString());
			Assert.IsTrue(internalRequest.JourneyTimeInformation.Hour  == requestHour);
			Assert.IsTrue(internalRequest.JourneyTimeInformation.Minute  == requestMinute);
			Assert.IsTrue(internalRequest.Range  == range);
			Assert.IsTrue(internalRequest.RangeType.ToString()  == rangeType.ToString());
			Assert.IsTrue(internalRequest.ServiceNumber  == serviceNumber);
			Assert.IsTrue(internalRequest.ShowDepartures == showDepartures);
 
		}

		/// <summary>
		/// Test for CreateDepartureBoardServiceStopInformationArrayDT 
		/// </summary>
		[Test]
		public void TestCreateDepartureBoardServiceStopInformationArrayDT()
		{
			// creating the instance of domain object 
			 DBSStopEvent[] dbsStopEvents = new DBSStopEvent[2];
			 
			 DBSStopEvent dbsStopEvent = new DBSStopEvent();
			 dbsStopEvent.Arrival = CreateDBSEventInstance(true);
			 dbsStopEvent.Departure = CreateDBSEventInstance(false);
			 dbsStopEvent.Stop = CreateDBSEventInstance(false);
			 dbsStopEvent.CallingStopStatus = callingStopStatus;
			 dbsStopEvent.Mode = departureBoardType;
			 dbsStopEvent.Service = CreateDBSServiceInstance();
			 
			 dbsStopEvents[0] = dbsStopEvent;
			 dbsStopEvents[1]	= CreateTrainStopEvent(true); 
			 
			 dbsStopEvent.OnwardIntermediates  = new DBSEvent[2];
			dbsStopEvent.OnwardIntermediates[0]  = CreateDBSEventInstance(true);
			dbsStopEvent.OnwardIntermediates[1]  = CreateDBSEventInstance(false);

			 dbsStopEvent.PreviousIntermediates  = new DBSEvent[2];
			dbsStopEvent.PreviousIntermediates[0]  = CreateDBSEventInstance(true);
			dbsStopEvent.PreviousIntermediates[1]  = CreateDBSEventInstance(true);


			 
			// Getting DTO object from domain object
			 DepartureBoardServiceStopInformation[] departureBoardServiceStopInformations = DepartureBoardAssembler.CreateDepartureBoardServiceStopInformationArrayDT(dbsStopEvents);
  
			// now testing each params
			Assert.IsTrue(departureBoardServiceStopInformations.Length == 2);
				
  
			for (int i=0; i< departureBoardServiceStopInformations.Length; i++)
			{	
				DepartureBoardServiceStopInformation  departureBoardServiceStopInformation = departureBoardServiceStopInformations[i];
				Assert.IsTrue(departureBoardServiceStopInformation.Arrival != null);
				Assert.IsTrue(departureBoardServiceStopInformation.Departure != null);
				Assert.IsTrue(departureBoardServiceStopInformation.Stop != null);
				Assert.IsTrue(departureBoardServiceStopInformation.Service != null);
			
				Assert.IsTrue(departureBoardServiceStopInformation.Arrival.ActivityType.ToString()  == dbsActivityType.ToString());
				Assert.IsTrue(departureBoardServiceStopInformation.Arrival.ArriveTime  == arrivalTime);
				Assert.IsTrue(departureBoardServiceStopInformation.Arrival.DepartTime  == departTime);

				Assert.IsTrue(departureBoardServiceStopInformation.Arrival.RealTime.ArriveTime  == estimatedArrivalTime);
				Assert.IsTrue(departureBoardServiceStopInformation.Arrival.RealTime.ArriveTimeType.ToString()  == dbsRealTimeTypeEstimated.ToString());
				Assert.IsTrue(departureBoardServiceStopInformation.Arrival.RealTime.DepartTime  == estimatedDepartTime);
				Assert.IsTrue(departureBoardServiceStopInformation.Arrival.RealTime.DepartTimeType.ToString()  == dbsRealTimeTypeRecorded.ToString());

				Assert.IsTrue(departureBoardServiceStopInformation.Arrival.Stop   != null);
				Assert.IsTrue(departureBoardServiceStopInformation.Arrival.Stop.ShortCode == arrivalShortCode);
				Assert.IsTrue(departureBoardServiceStopInformation.Arrival.Stop.Name == arrivalStopName);
				Assert.IsTrue(departureBoardServiceStopInformation.Arrival.Stop.ShortCode == arrivalShortCode);

				Assert.IsTrue(departureBoardServiceStopInformation.Departure.Stop   != null);
				Assert.IsTrue(departureBoardServiceStopInformation.Departure.Stop.ShortCode == departShortCode);
				Assert.IsTrue(departureBoardServiceStopInformation.Departure.Stop.Name == departStopName);
				Assert.IsTrue(departureBoardServiceStopInformation.Departure.Stop.ShortCode == departShortCode);

				Assert.IsTrue(departureBoardServiceStopInformation.Service.OperatorCode == operatorCode);
				Assert.IsTrue(departureBoardServiceStopInformation.Service.OperatorName == operatorName);
				Assert.IsTrue(departureBoardServiceStopInformation.Service.ServiceNumber == operatorServiceNumber);
				Assert.IsTrue(departureBoardServiceStopInformation.OnwardIntermediates.Length  == 2);
				Assert.IsTrue(departureBoardServiceStopInformation.PreviousIntermediates.Length  == 2);

				// now checking train specific changes
				if (i == 0)
					Assert.IsTrue(departureBoardServiceStopInformation.HasTrainDetails == false);						
				else
					Assert.IsTrue(departureBoardServiceStopInformation.HasTrainDetails == true);						
				
				if (departureBoardServiceStopInformation.HasTrainDetails)
				{
					departureBoardServiceStopInformation.Cancelled = cancelled;
					departureBoardServiceStopInformation.CircularRoute  = circularRoute;
					departureBoardServiceStopInformation.CancellationReason   = cancellationReason;
					departureBoardServiceStopInformation.FalseDestination    = falseDestination;
					departureBoardServiceStopInformation.LateReason    = lateReason;
					departureBoardServiceStopInformation.Via    = via;
  
				}
			   
			}
 
		}

		/// <summary>
		/// Test for CreateDepartureBoardServiceTimeRequestTypeArrayDT
		/// </summary>
		[Test]
		public void TestCreateDepartureBoardServiceTimeRequestTypeArrayDT()
		{
			// creating the instance of domain object 
			   TimeRequestType[] timeRequestTypes = new TimeRequestType[5];

			   timeRequestTypes[0] = TimeRequestType.First;
			   timeRequestTypes[1] = TimeRequestType.Last;  
			   timeRequestTypes[2] = TimeRequestType.Now;   
			   timeRequestTypes[3] = TimeRequestType.TimeToday;   
			   timeRequestTypes[4] = TimeRequestType.TimeTomorrow;

			// Getting DTO object from domain object
			   DepartureBoardServiceTimeRequestType[] departureBoardServiceTimeRequestTypes = DepartureBoardAssembler.CreateDepartureBoardServiceTimeRequestTypeArrayDT(timeRequestTypes);
  
			// now testing each params
			Assert.IsTrue(departureBoardServiceTimeRequestTypes.Length == 5);
			Assert.IsTrue(departureBoardServiceTimeRequestTypes[0].ToString()  == TimeRequestType.First.ToString() );
			Assert.IsTrue(departureBoardServiceTimeRequestTypes[1].ToString()  == TimeRequestType.Last.ToString() );
			Assert.IsTrue(departureBoardServiceTimeRequestTypes[2].ToString()  == TimeRequestType.Now.ToString() );
			Assert.IsTrue(departureBoardServiceTimeRequestTypes[3].ToString()  == TimeRequestType.TimeToday.ToString() );
			Assert.IsTrue(departureBoardServiceTimeRequestTypes[4].ToString()  == TimeRequestType.TimeTomorrow.ToString());
		}


		#endregion

		#region Private Methods
		/// <summary>
		/// Helper method to create thge instance of  DepartureBoardServiceRequest
		/// </summary>
		/// <returns></returns>
		private DepartureBoardServiceRequest CreateDepartureBoardServiceRequestInstance()
		{
			DepartureBoardServiceRequest departureBoardServiceRequest = new DepartureBoardServiceRequest();

			DepartureBoardServiceLocation originLocation = new DepartureBoardServiceLocation(); 
			originLocation.Code = originCode;
			originLocation.Valid = false;

			DepartureBoardServiceLocation destinationLocation = new DepartureBoardServiceLocation(); 
			destinationLocation.Code = destinationCode;
			destinationLocation.Valid = false;

			int range = 5;
			DepartureBoardServiceTimeRequest timeRequest = new DepartureBoardServiceTimeRequest();
			timeRequest.Hour = requestHour;
			timeRequest.Minute = requestMinute;
			timeRequest.Type = timeRequestType; 

			departureBoardServiceRequest.OriginLocation = originLocation;
			departureBoardServiceRequest.DestinationLocation = destinationLocation;
			departureBoardServiceRequest.JourneyTimeInformation = timeRequest;
			departureBoardServiceRequest.Range = range;
			departureBoardServiceRequest.RangeType = rangeType;
			departureBoardServiceRequest.ServiceNumber = serviceNumber;
			departureBoardServiceRequest.ShowCallingStops = false;
			departureBoardServiceRequest.ShowDepartures = showDepartures;
			return departureBoardServiceRequest;
		}

		/// <summary>
		/// Helper method to create the instance of DBSEvent
		/// </summary>
		/// <param name="arrivalType"></param>
		/// <returns></returns>
		private DBSEvent CreateDBSEventInstance(bool arrivalType)
		{
			
			DBSEvent dbsEvent  = new DBSEvent();
			dbsEvent.ActivityType = dbsActivityType; 
			dbsEvent.ArriveTime = arrivalTime; 
			dbsEvent.DepartTime  = departTime;
			dbsEvent.RealTime = new DBSRealTime(); 
			dbsEvent.RealTime.ArriveTime = estimatedArrivalTime;
			dbsEvent.RealTime.ArriveTimeType = dbsRealTimeTypeEstimated;
			dbsEvent.RealTime.DepartTime = estimatedDepartTime;
			dbsEvent.RealTime.DepartTimeType = dbsRealTimeTypeRecorded;
			dbsEvent.Stop = new DBSStop();

			if (arrivalType)
			{
				dbsEvent.Stop.Name = arrivalStopName;
				dbsEvent.Stop.ShortCode = arrivalShortCode;
				dbsEvent.Stop.NaptanId = arrivalNaptanId;
			}
			else
			{
				dbsEvent.Stop.Name = departStopName;
				dbsEvent.Stop.ShortCode = departShortCode;
				dbsEvent.Stop.NaptanId = departNaptanId;
			}

			return	dbsEvent;

		}

		/// <summary>
		///  Helper method to create the instance TrainStopEvent
		/// </summary>
		/// <param name="arrivalType"></param>
		/// <returns></returns>
		private TrainStopEvent CreateTrainStopEvent(bool arrivalType)
		{
			
			TrainStopEvent trainStopEvent = new TrainStopEvent();
			trainStopEvent.Arrival = CreateDBSEventInstance(arrivalType);
			trainStopEvent.Departure = CreateDBSEventInstance(!arrivalType);
			trainStopEvent.Stop = CreateDBSEventInstance(!arrivalType);
			trainStopEvent.CallingStopStatus = callingStopStatus;
			trainStopEvent.Mode = departureBoardType;
			trainStopEvent.Service = CreateDBSServiceInstance();
			trainStopEvent.OnwardIntermediates  = new DBSEvent[2];
			trainStopEvent.OnwardIntermediates[0]  = CreateDBSEventInstance(arrivalType);
			trainStopEvent.OnwardIntermediates[1]  = CreateDBSEventInstance(!arrivalType);

			trainStopEvent.PreviousIntermediates  = new DBSEvent[2];
			trainStopEvent.PreviousIntermediates[0]  = CreateDBSEventInstance(arrivalType);
			trainStopEvent.PreviousIntermediates[1]  = CreateDBSEventInstance(arrivalType);

			trainStopEvent.CircularRoute = circularRoute;
			trainStopEvent.Cancelled  = cancelled;
			trainStopEvent.CancellationReason  = cancellationReason;

			trainStopEvent.Via  = via;
			trainStopEvent.LateReason  = lateReason;
			trainStopEvent.FalseDestination = falseDestination;

			return trainStopEvent;
		}

		/// <summary>
		///  Helper method to create the instance   DBSService
		/// </summary>
		/// <returns></returns>
		private DBSService CreateDBSServiceInstance()
		{
			DBSService dbsService = new DBSService();
			dbsService.OperatorCode = operatorCode;
			dbsService.OperatorName = operatorName;
			dbsService.ServiceNumber = operatorServiceNumber;
			return dbsService;
			
		}

		#endregion
	}
}
