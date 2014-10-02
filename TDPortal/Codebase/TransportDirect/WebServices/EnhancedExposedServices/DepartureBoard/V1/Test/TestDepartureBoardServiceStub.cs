// *********************************************** 
// NAME                 : TestDepartureBoardServiceStub.cs
// AUTHOR               : Russell Wilby
// DATE CREATED         : 13/02/2006 
// DESCRIPTION  		: DepartureBoardService Stub classes
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/DepartureBoard/V1/Test/TestDepartureBoardServiceStub.cs-arc  $ 
//
//   Rev 1.1   Feb 18 2010 12:38:00   RHopkins
//Pass OperatorCode in StopEvent request
//Resolution for 5403: Pass OperatorCode in StopEvents request
//
//   Rev 1.0   Nov 08 2007 13:51:56   mturner
//Initial revision.
//
//   Rev 1.0   Feb 13 2006 17:36:00   RWilby
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111

using System;
using System.Diagnostics;
using TransportDirect.UserPortal.DepartureBoardService;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;


namespace TransportDirect.EnhancedExposedServices.DepartureBoard.V1.Test
{
    public class TestDepartureBoardServiceFactory : IServiceFactory
    {
        private TestDepartureBoardService testDepartureBoardService;

        /// <summary>
        /// </summary>
        public TestDepartureBoardServiceFactory()
        {
            testDepartureBoardService = new TestDepartureBoardService();
        }

        #region IServiceFactory Members


        public object Get()
        {
            try
            {
                return testDepartureBoardService;
            }
            catch (Exception e)
            {
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, e.Message, e));
            }
            return null;
        }

        #endregion
    }


    public class TestDepartureBoardService : IDepartureBoardService
    {
        #region Class Members
        string serviceNumber = string.Empty;
        DBSActivityType dbsActivityType = DBSActivityType.Depart;
        DateTime arrivalTime = new DateTime(2006, 1, 17, 11, 10, 5);
        DateTime departTime = new DateTime(2006, 1, 17, 11, 11, 5);
        DateTime estimatedArrivalTime = new DateTime(2006, 1, 17, 11, 11, 5);
        DateTime estimatedDepartTime = new DateTime(2006, 1, 17, 11, 12, 5);
        DBSRealTimeType dbsRealTimeType = DBSRealTimeType.Estimated;

        string arrivalStopName = "EUSTON";
        string arrivalShortCode = "EUS";
        string arrivalNaptanId = "9100EUSTON";
        string departStopName = "MANCHESTER PIC";
        string departShortCode = "MAN";
        string departNaptanId = "9100MNCRPIC";
        CallingStopStatus callingStopStatus = CallingStopStatus.Unknown;
        DepartureBoardType departureBoardType = DepartureBoardType.Train;

        string operatorCode = "SL";
        string operatorName = "Silver link";
        string operatorServiceNumber = "SL1";

        bool circularRoute = false;
        bool cancelled = true;
        string cancellationReason = "UNKNOWN";
        string via = "Harrow Wealdstone";
        string lateReason = "signal failure";
        string falseDestination = "WATFORD";
        #endregion

        public DBSResult GetDepartureBoardTrip(
            string token,
            DBSLocation originLocation,
            DBSLocation destinationLocation,
            string operatorCode,
            string serviceNumber,
            DBSTimeRequest time,
            DBSRangeType rangeType,
            int range,
            bool showDepartures,
            bool showCallingStops)
        {

            DBSResult result = new DBSResult();

            DBSStopEvent[] dbsStopEvents = new DBSStopEvent[2];

            DBSStopEvent dbsStopEvent = new DBSStopEvent();
            dbsStopEvent.Arrival = CreateDBSEventInstance(true);
            dbsStopEvent.Departure = CreateDBSEventInstance(false);
            dbsStopEvent.Stop = CreateDBSEventInstance(false);
            dbsStopEvent.CallingStopStatus = callingStopStatus;
            dbsStopEvent.Mode = departureBoardType;
            dbsStopEvent.Service = CreateDBSServiceInstance();

            dbsStopEvents[0] = dbsStopEvent;
            dbsStopEvents[1] = CreateTrainStopEvent(true);

            dbsStopEvent.OnwardIntermediates = new DBSEvent[2];
            dbsStopEvent.OnwardIntermediates[0] = CreateDBSEventInstance(true);
            dbsStopEvent.OnwardIntermediates[1] = CreateDBSEventInstance(false);

            dbsStopEvent.PreviousIntermediates = new DBSEvent[2];
            dbsStopEvent.PreviousIntermediates[0] = CreateDBSEventInstance(true);
            dbsStopEvent.PreviousIntermediates[1] = CreateDBSEventInstance(true);

            result.StopEvents = dbsStopEvents;

            return result;
        }

        public DBSResult GetDepartureBoardStop(
            string token,
            DBSLocation stopLocation,
            string operatorCode,
            string serviceNumber,
            bool showDepartures,
            bool showCallingStops)
        {
            return null;
        }

        public TimeRequestType[] GetTimeRequestTypesToDisplay()
        {

            return null;
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
        /// <summary>
        /// Helper method to create the instance of DBSEvent
        /// </summary>
        /// <param name="arrivalType"></param>
        /// <returns></returns>
        private DBSEvent CreateDBSEventInstance(bool arrivalType)
        {

            DBSEvent dbsEvent = new DBSEvent();
            dbsEvent.ActivityType = dbsActivityType;
            dbsEvent.ArriveTime = arrivalTime;
            dbsEvent.DepartTime = departTime;
            dbsEvent.RealTime = new DBSRealTime();
            dbsEvent.RealTime.ArriveTime = estimatedArrivalTime;
            dbsEvent.RealTime.ArriveTimeType = dbsRealTimeType;
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

            return dbsEvent;
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
            trainStopEvent.OnwardIntermediates = new DBSEvent[2];
            trainStopEvent.OnwardIntermediates[0] = CreateDBSEventInstance(arrivalType);
            trainStopEvent.OnwardIntermediates[1] = CreateDBSEventInstance(!arrivalType);

            trainStopEvent.PreviousIntermediates = new DBSEvent[2];
            trainStopEvent.PreviousIntermediates[0] = CreateDBSEventInstance(arrivalType);
            trainStopEvent.PreviousIntermediates[1] = CreateDBSEventInstance(arrivalType);

            trainStopEvent.CircularRoute = circularRoute;
            trainStopEvent.Cancelled = cancelled;
            trainStopEvent.CancellationReason = cancellationReason;

            trainStopEvent.Via = via;
            trainStopEvent.LateReason = lateReason;
            trainStopEvent.FalseDestination = falseDestination;

            return trainStopEvent;
        }

    }
}
