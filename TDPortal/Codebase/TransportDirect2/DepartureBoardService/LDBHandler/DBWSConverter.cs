// *********************************************** 
// NAME             : DBWSConvertor.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2014
// DESCRIPTION  	: DepartureBoardWebService request and result converter class
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.LocationService;
using DBWS = TDP.UserPortal.DepartureBoardService.DepartureBoardWebService;
using TDP.UserPortal.DepartureBoardService.DepartureBoardServiceTypes;
using Logger = System.Diagnostics.Trace;
using TDP.Common.EventLogging;
using TDP.Common.PropertyManager;
using TDP.Common;
using TDP.Common.Extenders;
using TDP.UserPortal.AdditionalDataModule;
using TDP.Common.ServiceDiscovery;
using TDP.UserPortal.Retail;

namespace TDP.UserPortal.DepartureBoardService
{
    /// <summary>
    /// DepartureBoardWebService request and result converter class
    /// </summary>
    internal class DBWSConverter
    {
        #region Private variables

        private AdditionalData additionalData;
        private OperatorCatalogue operatorCatalogue;
        
        private const int tiplocLength = 7;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DBWSConverter()
        {
            additionalData = TDPServiceDiscovery.Current.Get<AdditionalData>(ServiceDiscoveryKey.AdditionalData);
            operatorCatalogue = TDPServiceDiscovery.Current.Get<OperatorCatalogue>(ServiceDiscoveryKey.OperatorService);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Instantiate and populate a new DBWSRequest using the given parameters
        /// </summary>
        /// <param name="requestId">Request id</param>
        /// <param name="stopLocation">Location for station board</param>
        /// <param name="filterLocation">Location to filter station board services by</param>
        /// <param name="showDepartures">Departure service information required for station board</param>
        /// <param name="showArrivals">Arrival service information required for station board</param>
        /// <param name="numberOfRows">Number of rows to return on the station board</param>
        /// <param name="requestedDateTime">Requested date time for the station board, defaults to now</param>
        /// <param name="durationMins">Duration in mins of the station board window, e.g. up to 120 minutes from now</param>
        /// <param name="serviceId">Service id if a service details result is required</param>
        /// <returns></returns>
        public DBWS.DBWSRequest BuildDBWSRequest(
            string requestId,
            TDPStopLocation stopLocation, TDPStopLocation filterLocation,
            bool showDepartures, bool showArrivals,
            int numberOfRows, DateTime requestedDateTime, int durationMins,
            string serviceId)
        {
            // Initialise
            DBWS.DBWSRequest request = InitialiseRequest(requestId);

            // Station board location
            if (stopLocation != null)
                request.Location.LocationCRS = stopLocation.Code;

            // Departures or Arrivals service times to include
            request.ShowDepartures = showDepartures;
            request.ShowArrivals = showArrivals;

            // Filter station board with services from/to location
            if (filterLocation != null)
                request.LocationFilter.LocationCRS = filterLocation.Code;

            // Filter direction, e.g. Services at stop location to the filter location
            if (showDepartures)
                request.LocationFilterType = DBWS.DBWSFilterType.ServicesTo;
            else
                request.LocationFilterType = DBWS.DBWSFilterType.ServicesFrom;

            // Number of rows on station board, and time window duration
            request.NumberOfRows = numberOfRows;
            request.Duration = durationMins;
            request.RequestedTime = requestedDateTime;
            request.TimeOffset = 0; // Default to no offset, the DBWS will use the requested time value

            // Service if for a service details request
            request.ServiceId = serviceId;

            return request;
        }

        /// <summary>
        /// Populates a DBSResult with details from the DBWSResult
        /// </summary>
        /// <param name="dbwsResult">Result from the DepartureBoardWebService</param>
        /// <param name="requestedDate">Date the result is for, used to calculate service date and times</param>
        /// <param name="stopLocation">Location the DBWS result is for</param>
        /// <param name="filterLocation">Location the DBWS result services are filtered by</param>
        /// <param name="showDepartures">Include departures or arrival service datetimes</param>
        /// <param name="requestType">Indicates if DBWS result is a station board or train service</param>
        /// <returns>DBSResult contianing station board or train service details and/or message indicating success/failure</returns>
        public DBSResult BuildDBSResult(
            DBWS.DBWSResult dbwsResult,
            TDPLocation stopLocation, TDPLocation filterLocation,
            bool showDepartures, DateTime requestedDate,
            bool isStationBoard)
        {
            // Initialise the result object
            DBSResult result = new DBSResult();

            try
            {
                if (isStationBoard)
                {
                    // Build station board 
                    if (!ExtractDataForStationBoard(dbwsResult, result, stopLocation, requestedDate))
                    {
                        if (IsInvalidRequestError(dbwsResult))
                        {
                            // If result indicates its a invalid request, include the description why it is invalid
                            result = DepartureBoardService.GetInvalidDBSResult(TDPExceptionIdentifier.DBSLDBRequestInvalid, "LiveDepartureBoard request is invalid");

                            result.Messages[0].Description += string.Format(" {0}", dbwsResult.Messages[0].Description);
                        }
                        else
                            result = DepartureBoardService.GetInvalidDBSResult(TDPExceptionIdentifier.DBSLDBUnableToBuildDBSResult, "Unable to build DBSResult from LiveDepartureBoard result");

                    }
                }
                else
                {
                    // Build service detail
                    if (!ExtractDataForServiceDetail(dbwsResult, result, requestedDate))
                    {
                        if (IsInvalidRequestError(dbwsResult))
                        {
                            // If result indicates its a invalid request, include the description why it is invalid
                            result = DepartureBoardService.GetInvalidDBSResult(TDPExceptionIdentifier.DBSLDBRequestInvalid, "LiveDepartureBoard request is invalid");

                            result.Messages[0].Description += string.Format(" {0}", dbwsResult.Messages[0].Description);
                        }
                        else
                            result = DepartureBoardService.GetInvalidDBSResult(TDPExceptionIdentifier.DBSLDBUnableToBuildDBSResult, "Unable to build DBSResult from LiveDepartureBoard result");

                    }
                }
            }
            catch (Exception ex)
            {
                // Build error message 
                result = DepartureBoardService.GetInvalidDBSResult(TDPExceptionIdentifier.DBSLDBErrorBuildingDBSResult, "Error building DBSResult from LiveDepartureBoard result");

                string errMsg = "Error occured in BuildDBSResult " + ex.Message;
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, errMsg));
            }

            return result;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initialise the DBWSRequest with default values
        /// </summary>
        private DBWS.DBWSRequest InitialiseRequest(string requestId)
        {
            DBWS.DBWSRequest request = new DBWS.DBWSRequest();

            // Request id 
            request.RequestId = requestId;

            // Location
            request.Location = new DBWS.DBWSLocation();
            request.LocationFilter = new DBWS.DBWSLocation();
            request.LocationFilterType = DBWS.DBWSFilterType.ServicesTo;

            request.ShowDepartures = false;
            request.ShowArrivals = false;

            return request;
        }

        #region Station Board result

        /// <summary>
        /// Extract the data for a station board from the departure board web service result
        /// <returns></returns>
        private bool ExtractDataForStationBoard(DBWS.DBWSResult dbwsResult, DBSResult result,
            TDPLocation stopLocation, DateTime stopDate)
        {
            try
            {
                // Check there are station board results
                if (dbwsResult == null ||
                    dbwsResult.StationBoardServices == null ||
                    dbwsResult.StationBoardServices.Length == 0)
                {
                    return false;
                }

                #region Variables

                // Variables used to build up the station board result
                List<TrainStopEvent> trainStopEvents = new List<TrainStopEvent>();
                TrainStopEvent trainStopEvent;
                DBSEvent departure;
                DBSEvent stop;
                DBSEvent arrival;

                string requestedStationNaptan;

                #endregion

                #region Naptan of the requested station board

                // Identify naptan for requested stop
                if (dbwsResult.Location != null && !string.IsNullOrEmpty(dbwsResult.Location.LocationCRS))
                {
                    // Get NaptanCode for requested stop
                    requestedStationNaptan = GetNaptanForCRS(dbwsResult.Location.LocationCRS);
                }
                else
                    requestedStationNaptan = stopLocation.Naptan[0]; // Assume there is a naptan

                #endregion

                // Iterate through each station board service and extract the data,
                // assume number of results returned are no more than requested
                foreach (DBWS.DBWSService dbwsService in dbwsResult.StationBoardServices)
                {
                    // Initialise
                    trainStopEvent = new TrainStopEvent();
                    departure = new DBSEvent();
                    stop = new DBSEvent();
                    arrival = new DBSEvent();

                    // Only train service request has calling stops
                    trainStopEvent.CallingStopStatus = CallingStopStatus.NoCallingStops;
                    trainStopEvent.Mode = DepartureBoardType.Train;

                    // Build service
                    if (!BuildServiceEventInfo(dbwsService, trainStopEvent))
                        return false;

                    #region Service arrival/departure stop

                    // Stop - including arrival/departure times
                    if (!BuildDBSEvent(dbwsService, dbwsResult.Location, stopDate, DBSActivityType.ArriveDepart, stop))
                        return false;

                    #endregion

                    #region Service departure/origin location

                    // Service departure from location
                    if (dbwsService.OriginLocations != null && dbwsService.OriginLocations.Length > 0)
                    {
                        // Only take the first service origin location (as per RTTIXmlExtractor)
                        if (!BuildDBSEvent(dbwsService, dbwsService.OriginLocations[0], stopDate, DBSActivityType.Unavailable, departure))
                            return false;
                    }

                    #endregion

                    #region Service arrival/destination location

                    // Service arrival at location
                    if (dbwsService.DestinationLocations != null && dbwsService.DestinationLocations.Length > 0)
                    {
                        int lastIndex = dbwsService.DestinationLocations.Length - 1;

                        // Only take the last service destination location (as per RTTIXmlExtractor)
                        if (!BuildDBSEvent(dbwsService, dbwsService.DestinationLocations[lastIndex], stopDate, DBSActivityType.Unavailable, arrival))
                            return false;
                    }

                    #endregion

                    trainStopEvent.Stop = stop;
                    trainStopEvent.Departure = departure; // Where service originates from 
                    trainStopEvent.Arrival = arrival; // Where service is destined to

                    if (!string.IsNullOrEmpty(dbwsService.Platform))
                        trainStopEvent.Platform = dbwsService.Platform;

                    // Add to the station board result
                    trainStopEvents.Add(trainStopEvent);
                }

                // Add to the result
                result.StopEvents = trainStopEvents.ToArray();

                // Update the generate at value
                result.GeneratedAt = dbwsResult.GeneratedAt;

                return true;
            }
            catch (Exception ex)
            {
                // log the error 
                string message = "Error occured in ExtractDataForStationBoard " + ex.Message;
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, message));
                return false;
            }
        }

        /// <summary>
        /// Extracts operator info and service number and trip information like delayed, false destination etc.
        /// </summary>
        /// <returns>Returns true if managed to extract data sucessfully else false would be returned </returns>
        private bool BuildServiceEventInfo(DBWS.DBWSService service, TrainStopEvent trainStopEvent)
        {
            try
            {
                // Build the service details
                DBSService dbsService = new DBSService();

                // Service info
                dbsService.ServiceNumber = service.ServiceId;

                if (service.ServiceOperator != null)
                {
                    dbsService.OperatorCode = service.ServiceOperator.OperatorCode;
                    dbsService.OperatorName = GetOperatorName(dbsService.OperatorCode);
                }

                // Update the service
                trainStopEvent.Service = dbsService;

                trainStopEvent.CircularRoute = service.IsCircularRoute;
                trainStopEvent.FalseDestination = string.Empty;

                // Check for service cancelled
                if (IsCancelled(service.TimeOfArrivalScheduled)
                    || IsCancelled(service.TimeOfArrivalEstimated)
                    || IsCancelled(service.TimeOfDepartureScheduled)
                    || IsCancelled(service.TimeOfDepartureEstimated))
                {
                    trainStopEvent.Cancelled = true;
                }

                return true;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        /// <summary>
        /// Extracts operator info and service number and trip information like delayed, false destination etc.
        /// </summary>
        /// <returns>Returns true if managed to extract data sucessfully else false would be returned </returns>
        private bool BuildServiceEventInfo(DBWS.DBWSServiceDetail serviceDetail, TrainStopEvent trainStopEvent)
        {
            try
            {
                // Populate common Service details
                if (BuildServiceEventInfo((DBWS.DBWSService)serviceDetail, trainStopEvent))
                {
                    // And add information specific to the service detail
                    trainStopEvent.Cancelled = serviceDetail.IsCancelled;
                    trainStopEvent.CancellationReason = serviceDetail.DisruptionReason;
                    trainStopEvent.LateReason = serviceDetail.OverdueMessage;
                }

                return true;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        /// <summary>
        /// Extracts station name and deparure/arrival time info for a Station Board result
        /// </summary>
        /// <returns>Returns true if managed to extract data sucessfully else false would be returned</returns>
        private bool BuildDBSEvent(DBWS.DBWSService service, DBWS.DBWSLocation location, DateTime date,
            DBSActivityType dbsActivityType, DBSEvent dbsEvent)
        {
            try
            {
                dbsEvent.ActivityType = dbsActivityType;

                // DBSStop
                dbsEvent.Stop = BuildDBSStop(location.LocationCRS);

                #region Station board real time info for the service

                // Build real time object (if required)
                switch (dbsActivityType)
                {
                    case DBSActivityType.Depart:
                    case DBSActivityType.Arrive:
                    case DBSActivityType.ArriveDepart:
                        dbsEvent.RealTime = BuildTrainRealTime(
                            service.TimeOfArrivalScheduled, service.TimeOfArrivalEstimated, string.Empty,
                            service.TimeOfDepartureScheduled, service.TimeOfDepartureEstimated, string.Empty,
                            date);
                        dbsEvent.ArriveTime = ParseTime(service.TimeOfArrivalScheduled, date);
                        dbsEvent.DepartTime = ParseTime(service.TimeOfDepartureScheduled, date);
                        break;

                    case DBSActivityType.Unavailable:
                    default:
                        // If arrival/departure times not required, then empty time and real time details 
                        dbsEvent.RealTime = new TrainRealTime();
                        break;
                }

                #endregion

                // Check for service cancelled
                if (IsCancelled(service.TimeOfArrivalScheduled)
                    || IsCancelled(service.TimeOfArrivalEstimated)
                    || IsCancelled(service.TimeOfDepartureScheduled)
                    || IsCancelled(service.TimeOfDepartureEstimated))
                {
                    dbsEvent.Cancelled = true;
                }

                #region Service is a ServiceDetail update

                // If ServiceDetail object, then service will have a cancelled flag element
                if (service is DBWS.DBWSServiceDetail)
                {
                    DBWS.DBWSServiceDetail serviceDetail = (DBWS.DBWSServiceDetail)service;

                    dbsEvent.Cancelled = serviceDetail.IsCancelled;

                    // It may also have an actual time element
                    switch (dbsActivityType)
                    {
                        case DBSActivityType.Depart:
                        case DBSActivityType.Arrive:
                        case DBSActivityType.ArriveDepart:
                            dbsEvent.RealTime = BuildTrainRealTime(
                                service.TimeOfArrivalScheduled, service.TimeOfArrivalEstimated, serviceDetail.TimeOfArrivalActual,
                                service.TimeOfDepartureScheduled, service.TimeOfDepartureEstimated, serviceDetail.TimeOfDepartureActual,
                                date);
                            break;
                    }
                }

                #endregion

                return true;
            }
            catch (NullReferenceException nEx)
            {
                // log the error 
                string errMsg = "Error occured in BuildDBSEvent " + nEx.Message;
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, errMsg));
                return false;
            }
        }

        /// <summary>
        /// Extracts station name and deparure/arrival time info for a Service location calling point
        /// </summary>
        /// <returns>Returns true if managed to extract data sucessfully else false would be returned</returns>
        private bool BuildDBSEvent(DBWS.DBWSLocationCallingPoint locationCallingPoint, DateTime date,
            DBSActivityType dbsActivityType, bool isDepartureTimes, DBSEvent dbsEvent)
        {
            try
            {
                dbsEvent.ActivityType = dbsActivityType;

                // DBSStop
                dbsEvent.Stop = BuildDBSStop(locationCallingPoint.LocationCRS);

                #region Location calling point real time info for a service

                // Build real time object (if required)
                switch (dbsActivityType)
                {
                    case DBSActivityType.Depart:
                    case DBSActivityType.Arrive:
                    case DBSActivityType.ArriveDepart:
                        if (isDepartureTimes)
                        {
                            // Times in the location are for departing from location
                            dbsEvent.RealTime = BuildTrainRealTime(
                                string.Empty, string.Empty, string.Empty,
                                locationCallingPoint.TimeScheduled, locationCallingPoint.TimeEstimated, locationCallingPoint.TimeActual,
                                date);
                            dbsEvent.ArriveTime = DateTime.MinValue;
                            dbsEvent.DepartTime = ParseTime(locationCallingPoint.TimeScheduled, date);
                        }
                        else
                        {
                            // Times in the location are for arriving at location
                            dbsEvent.RealTime = BuildTrainRealTime(
                                locationCallingPoint.TimeScheduled, locationCallingPoint.TimeEstimated, locationCallingPoint.TimeActual,
                                string.Empty, string.Empty, string.Empty,
                                date);
                            dbsEvent.ArriveTime = ParseTime(locationCallingPoint.TimeScheduled, date);
                            dbsEvent.DepartTime = DateTime.MinValue;
                        }
                        break;

                    case DBSActivityType.Unavailable:
                    default:
                        // If arrival/departure times not required, then empty time and real time details 
                        dbsEvent.RealTime = new DBSRealTime();
                        break;
                }

                #endregion

                // Check for service cancelled
                if (IsCancelled(locationCallingPoint.TimeScheduled)
                    || IsCancelled(locationCallingPoint.TimeEstimated)
                    || IsCancelled(locationCallingPoint.TimeActual))
                {
                    dbsEvent.Cancelled = true;
                }

                return true;
            }
            catch (NullReferenceException nEx)
            {
                // log the error 
                string errMsg = "Error occured in BuildDBSEvent " + nEx.Message;
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, errMsg));
                return false;
            }
        }

        /// <summary>
        /// Builds a DBSStop for a crs code
        /// </summary>
        /// <param name="crs"></param>
        /// <returns></returns>
        private DBSStop BuildDBSStop(string crs)
        {
            // Build stop object		
            DBSStop dbsStop = new DBSStop();

            // Set naptan
            dbsStop.NaptanId = GetNaptanForCRS(crs);

            // Set station name
            dbsStop.Name = GetStationNameForNaptan(dbsStop.NaptanId);

            // Set short code (i.e. CRS in this case)
            dbsStop.ShortCode = GetCRSForNaptan(dbsStop.NaptanId);

            return dbsStop;
        }

        /// <summary>
        /// Extracts real time information
        /// </summary>
        /// <param name="sta">Scheduled arrival time</param>
        /// <param name="eta">Estimated arrival time</param>
        /// <param name="ata">Actual arrival time</param>
        /// <param name="std">Scheduled departure time</param>
        /// <param name="etd">Estimated departure time</param>
        /// <param name="atd">Actual departure time</param>
        private TrainRealTime BuildTrainRealTime(
            string sta, string eta, string ata,
            string std, string etd, string atd,
            DateTime date)
        {
            TrainRealTime trainRealTime = new TrainRealTime();

            // Get the times, this will create the datetime if value is a valid time,
            // otherwise DateTime.Min if time is text, e.g. "On time", "No report" or "Cancelled"
            DateTime scheduledArrival = ParseTime(sta, date);
            DateTime arrival = ParseTime(eta, date);
            DateTime scheduledDeparture = ParseTime(std, date);
            DateTime departure = ParseTime(etd, date);

            DBSRealTimeType arrivalTimeType = DBSRealTimeType.Estimated;
            DBSRealTimeType departureTimeType = DBSRealTimeType.Estimated;

            // If actual time supplied, then populating real time info for service that
            // may have passed through station
            if (!string.IsNullOrEmpty(ata))
            {
                arrival = ParseTime(ata, date);
                arrivalTimeType = DBSRealTimeType.Recorded;
            }
            if (!string.IsNullOrEmpty(atd))
            {
                departure = ParseTime(atd, date);
                departureTimeType = DBSRealTimeType.Recorded;
            }

            // Check if on time and update time if required
            if (arrival == DateTime.MinValue && (IsOnTime(eta) || IsOnTime(ata)))
                arrival = scheduledArrival;
            if (departure == DateTime.MinValue && (IsOnTime(etd) || IsOnTime(atd)))
                departure = scheduledDeparture;

            // Update real time
            trainRealTime.ArriveTime = arrival;
            trainRealTime.ArriveTimeType = arrivalTimeType;
            trainRealTime.DepartTime = departure;
            trainRealTime.DepartTimeType = departureTimeType;

            // Get train delayed 
            if (IsDelayed(sta)
                || IsDelayed(eta)
                || IsDelayed(std)
                || IsDelayed(etd))
            {
                trainRealTime.Delayed = true;
            }

            // Get train uncertain
            if (IsUncertain(sta)
                || IsUncertain(eta)
                || IsUncertain(std)
                || IsUncertain(etd))
            {
                trainRealTime.Uncertain = true;
            }

            return trainRealTime;
        }

        #endregion

        #region Service Detail result

        /// <summary>
        /// Extract the data for a service detail from the departure board web service result
        /// <returns></returns>
        private bool ExtractDataForServiceDetail(DBWS.DBWSResult dbwsResult, DBSResult result,
            DateTime stopDate)
        {
            try
            {
                // Check there is a service detail result
                if (dbwsResult == null ||
                    dbwsResult.ServiceDetail == null)
                {
                    return false;
                }

                DBWS.DBWSServiceDetail dbwsServiceDetail = dbwsResult.ServiceDetail;

                #region Variables

                // Variables used to build up the service detail result, should only be one service result 
                // which contains info about calling points
                List<TrainStopEvent> trainStopEvents = new List<TrainStopEvent>();
                TrainStopEvent trainStopEvent = new TrainStopEvent();
                DBSEvent departure = new DBSEvent();
                DBSEvent stop = new DBSEvent();
                DBSEvent arrival = new DBSEvent();
                DBSEvent intermediate = new DBSEvent();
                List<DBSEvent> previousCallingPoints = new List<DBSEvent>();
                List<DBSEvent> subsequentCallingPoints = new List<DBSEvent>();

                bool departureFound = false;
                bool arrivalFound = false;

                #endregion

                // Only train service request has calling stops
                trainStopEvent.CallingStopStatus = CallingStopStatus.HasCallingStops;
                trainStopEvent.Mode = DepartureBoardType.Train;

                // Build service
                if (!BuildServiceEventInfo(dbwsServiceDetail, trainStopEvent))
                    return false;

                #region Service arrival/departure for stop

                // Stop - including arrival/departure times
                if (!BuildDBSEvent(dbwsServiceDetail, dbwsResult.Location, stopDate, DBSActivityType.ArriveDepart, stop))
                    return false;

                #endregion

                #region Service previous calling point locations

                // Service previous calling point locations
                if (dbwsServiceDetail.PreviousCallingPointLocations != null && dbwsServiceDetail.PreviousCallingPointLocations.Length > 0)
                {
                    // Only take the first calling point locations list (as per RTTIXmlExtractor). This will be the 
                    // "through" train, any remaining lists will be the "association" calling points which are when the
                    // service has been created previously from multiple services
                    List<DBWS.DBWSLocationCallingPoint> callingPoints = new List<DBWS.DBWSLocationCallingPoint>(dbwsServiceDetail.PreviousCallingPointLocations[0]);

                    for (int i = 0; i < callingPoints.Count; i++)
                    {
                        // The calling point times will be either an arrival or departure time, depending on 
                        // whether it is in the previous or subsequent calling point list

                        DBWS.DBWSLocationCallingPoint callingPoint = callingPoints[i];

                        // First calling point is the service origin location
                        if (i == 0)
                        {
                            if (!BuildDBSEvent(callingPoint, stopDate, DBSActivityType.Depart, true, departure))
                                return false;

                            departureFound = true;
                        }
                        else
                        {
                            // Otherwise its an intermediate calling point
                            intermediate = new DBSEvent();

                            if (!BuildDBSEvent(callingPoint, stopDate, DBSActivityType.ArriveDepart, true, intermediate))
                                return false;

                            previousCallingPoints.Add(intermediate);
                        }
                    }
                }

                #endregion

                #region Service subsequent calling point locations

                // Service subsequent calling point locations
                if (dbwsServiceDetail.SubsequentCallingPointLocations != null && dbwsServiceDetail.SubsequentCallingPointLocations.Length > 0)
                {
                    // Only take the first calling point locations list (as per RTTIXmlExtractor). This will be the 
                    // "through" train, any remaining lists will be the "association" calling points which are when the
                    // service is to be split into multiple services
                    List<DBWS.DBWSLocationCallingPoint> callingPoints = new List<DBWS.DBWSLocationCallingPoint>(dbwsServiceDetail.SubsequentCallingPointLocations[0]);

                    for (int i = 0; i < callingPoints.Count; i++)
                    {
                        // The calling point times will be either an arrival or departure time, depending on 
                        // whether it is in the previous or subsequent calling point list

                        DBWS.DBWSLocationCallingPoint callingPoint = callingPoints[i];

                        // Last calling point is the service destination location
                        if (i == (callingPoints.Count - 1))
                        {
                            if (!BuildDBSEvent(callingPoint, stopDate, DBSActivityType.Arrive, false, arrival))
                                return false;

                            arrivalFound = true;
                        }
                        else
                        {
                            // Otherwise its an intermediate calling point
                            intermediate = new DBSEvent();

                            if (!BuildDBSEvent(callingPoint, stopDate, DBSActivityType.ArriveDepart, false, intermediate))
                                return false;

                            subsequentCallingPoints.Add(intermediate);
                        }
                    }
                }

                #endregion

                // Check if departure/arrival location has been populated, the service could be departing or arriving
                // from/at the station board stop
                if (!departureFound)
                {
                    if (dbwsServiceDetail.PreviousCallingPointLocations == null
                        || dbwsServiceDetail.PreviousCallingPointLocations.Length == 0)
                    {
                        // No previous calling points, therefore service departs from the station board stop
                        // Updates it activity type
                        stop.ActivityType = DBSActivityType.Depart;

                        departure = stop;
                    }
                }
                if (!arrivalFound)
                {
                    if (dbwsServiceDetail.SubsequentCallingPointLocations == null
                        || dbwsServiceDetail.SubsequentCallingPointLocations.Length == 0)
                    {
                        // No subsequent calling points, therefore service arrive at the station board stop
                        // Updates it activity type
                        stop.ActivityType = DBSActivityType.Arrive;

                        arrival = stop;
                    }
                }

                trainStopEvent.Stop = stop;
                trainStopEvent.Departure = departure; // Where service originates from 
                trainStopEvent.Arrival = arrival; // Where service is destined to
                trainStopEvent.PreviousIntermediates = previousCallingPoints.ToArray();
                trainStopEvent.OnwardIntermediates = subsequentCallingPoints.ToArray();

                // Add to the station board result
                trainStopEvents.Add(trainStopEvent);

                // Add to the result
                result.StopEvents = trainStopEvents.ToArray();

                // Update the generate at value
                result.GeneratedAt = dbwsResult.GeneratedAt;

                return true;
            }
            catch (Exception ex)
            {
                // log the error 
                string message = "Error occured in ExtractDataForStationBoard " + ex.Message;
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, message));
                return false;
            }
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Gets Naptan Id from given CRS code
        /// </summary>
        /// <param name="stationCode">Station CRS code</param>
        /// <returns>Returns Naptan Id as string </returns>
        private string GetNaptanForCRS(string stationCode)
        {
            string[] naptanIds = new string[0];
            
            naptanIds = additionalData.LookupNaptanForCode(stationCode, LookupType.CRS_Code);

            // Check length > 0 and pick up the first one
            if (naptanIds.Length > 0)
                return naptanIds[0].ToString();
            else
                return string.Empty;
        }

        /// <summary>
        /// Gets Naptan Id from given tiploc code
        /// </summary>
        /// <param name="stationTiploc">station Tiploc code</param>
        /// <returns>Returns NaptanId as string </returns>
        private string GetNaptanForTiploc(string stationTiploc)
        {
            if (stationTiploc == null || stationTiploc.Length == 0)
                return stationTiploc;

            // now checking for full tiploc (8 chars instead of 7 chars)
            // if full tiploc code then get rid of last character.
            if (stationTiploc.Length > tiplocLength)
                stationTiploc = stationTiploc.Substring(0, tiplocLength);

            string railPrefix = Properties.Current[TDP.Common.LocationService.Keys.NaptanPrefix_Rail];

            return railPrefix + stationTiploc.Trim();
        }

        /// <summary>
        /// Gets CRS code for the given Naptan Id
        /// </summary>
        /// <param name="naptanId">Station Naptan Id</param>
        /// <returns>Return CRS code for the given Naptan Id </returns>
        private string GetCRSForNaptan(string naptanId)
        {
            if (naptanId == null || naptanId.Length == 0)
                return string.Empty;
            else
                return additionalData.LookupCrsForNaptan(naptanId).ToString();
        }

        /// <summary>
        /// Gets station name for the given Naptan Id
        /// </summary>
        /// <param name="naptanId">Station Naptan Id</param>
        /// <returns>Return station name for given Naptan Id </returns>
        private string GetStationNameForNaptan(string naptanId)
        {
            if (naptanId == null || naptanId.Length == 0)
                return string.Empty;
            else
                return additionalData.LookupStationNameForNaptan(naptanId).TitleCase();
        }

        /// <summary>
        /// Gets Operator name for given operator code
        /// </summary>
        /// <param name="operatorCode">Operator Code</param>
        /// <returns>Returns Operator's code </returns>
        private string GetOperatorName(string operatorCode)
        {
            string operatorName = string.Empty;
            
            ServiceOperator serviceOperator = operatorCatalogue.GetOperator(operatorCode);

            if (serviceOperator != null)
                operatorName = serviceOperator.Name;

            return operatorName;
        }

        /// <summary>
        /// Returns the time parsed in to a DateTime
        /// </summary>
        /// <param name="timeString"></param>
        /// <returns></returns>
        private DateTime ParseTime(string time, DateTime date)
        {
            char[] delimitter = new char[] { ':' };
            int hour;
            int minute;
            DateTime outDateTime;

            try
            {
                if (string.IsNullOrEmpty(time))
                    return DateTime.MinValue;

                string[] timeSplit = time.Split(delimitter, 2);

                hour = int.Parse(timeSplit[0]);
                minute = int.Parse(timeSplit[1]);

                if (hour < Properties.Current["DepartureBoardService.RTTIManager.FirstServiceHour"].Parse(4))
                {
                    // Time is in the early hours therefore must be for day after requested date
                    DateTime tomorrow = date.AddDays(1);

                    outDateTime = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, hour, minute, 0, 0);
                }
                else
                {
                    outDateTime = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0, 0);
                }

                return outDateTime;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Returns true if the time represents on time
        /// </summary>
        /// <param name="timeString"></param>
        /// <returns></returns>
        private bool IsOnTime(string timeString)
        {
            string onTime = "on time"; // Should add as a property

            // Service arriving/departure time will indicate if it is cancelled by
            // having text "cancelled"
            if (!string.IsNullOrEmpty(timeString))
            {
                if (timeString.ToLower().Contains(onTime))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Returns true if the time represents Cancelled
        /// </summary>
        /// <param name="timeString"></param>
        /// <returns></returns>
        private bool IsCancelled(string timeString)
        {
            string cancelled = "cancelled"; // Should add as a property

            // Service arriving/departure time will indicate if it is cancelled by
            // having text "cancelled"
            if (!string.IsNullOrEmpty(timeString))
            {
                if (timeString.ToLower().Contains(cancelled))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Returns true if the time represents Delayed
        /// </summary>
        /// <param name="timeString"></param>
        /// <returns></returns>
        private bool IsDelayed(string timeString)
        {
            string delayed = "delay"; // Should add as a property

            // Service arriving/departure time will indicate if it is cancelled by
            // having text "delay" *** THIS HAS NOT BEEN CONFIRMED ***
            if (!string.IsNullOrEmpty(timeString))
            {
                if (timeString.ToLower().Contains(delayed))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Returns true if the time represents Uncertain
        /// </summary>
        /// <param name="timeString"></param>
        /// <returns></returns>
        private bool IsUncertain(string timeString)
        {
            string uncertain = "*"; // Should add as a property

            // Service arriving/departure time will indicate if it is cancelled by
            // having text "*"
            if (!string.IsNullOrEmpty(timeString))
            {
                if (timeString.ToLower().Contains(uncertain))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Returns true if the DBWSResult contains a message with Invalid Request message code
        /// </summary>
        /// <param name="dbwsResult"></param>
        /// <returns></returns>
        private bool IsInvalidRequestError(DBWS.DBWSResult dbwsResult)
        {
            if (dbwsResult != null
                && dbwsResult.Messages != null
                && dbwsResult.Messages.Length > 0)
            {
                if (dbwsResult.Messages[0].Code == (int)TDPExceptionIdentifier.DBSLDBRequestInvalid)
                    return true;
            }

            return false;
        }

        #endregion

        #endregion
    }
}