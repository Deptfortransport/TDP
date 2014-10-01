// *********************************************** 
// NAME             : LDBHandler.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Dec 2013
// DESCRIPTION  	: LDBHandler class for handling data from the DepartureBoardWebService 
//                  : using the LiveDepartureBoard web service method calls
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using DBWS = TransportDirect.UserPortal.DepartureBoardService.DepartureBoardWebService;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using System.Xml.Serialization;
using System.IO;

namespace TransportDirect.UserPortal.DepartureBoardService.RTTIManager
{
    /// <summary>
    /// LDBHandler class for handling data from the DepartureBoardWebService 
    /// using the LiveDepartureBoard web service method calls
    /// </summary>
    public class LDBHandler : IRDHandler
    {
        #region Private variables
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public LDBHandler()
        {
        }

        #endregion

        #region IRDHandler interface - Public methods

        /// <summary>
        /// Station level request method for DepartureBoard information.
        /// THIS METHOD IS NO LONGER USED - USE GetDepartureBoardTrip
        /// </summary>
        public DBSResult GetDepartureBoardStop(DBSLocation stopLocation, string operatorCode, string serviceNumber, bool showDepartures, bool showCallingStops)
        {
            // MModi 09/01/2014 - I cannot find any reference (other than tests) to this method in RDHandler
            // being used within the application, therefore it has not been implemented in LDBHandler

            // Log error indicating this unsupported method has been called 
            string message = string.Format("LDBHandler GetDepartureBoardStop called and is not supported. This needs to investigated.");
            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message));

            DBSResult trainDbsResult = new DBSResult();

            RTTIUtilities.BuildRTTIError(Messages.RTTIUnknownError, trainDbsResult);

            return trainDbsResult;
        }

        /// <summary>
        /// Station level request method for DepartureBoard information
        /// </summary>
        public DBSResult GetDepartureBoardTrip(DBSLocation originLocation, DBSLocation destinationLocation, 
            string operatorCode, string serviceNumber,  DBSTimeRequest requestedTime, 
            DBSRangeType rangeType, int range, bool showDepartures, bool showCallingStops)
        {
            // LDB ignores:
            // operatorCode - No filtering of services by operator
            // showCallingStops - Calling stops always returned when doing a Train Service detail request

            // Result object
            DBSResult trainDbsResult = new DBSResult();

            try
            {
                // Requested datetime 
                DateTime requestedDateTime = GetDateTime(requestedTime);

                // Determine if this is a station board or for a train service,
                // abscence of a serviceNumber indicates this (replicated from RDHandler logic!)
                if (string.IsNullOrEmpty(serviceNumber))
                {
                    #region Station board request

                    // Only do a call if a location for station board has supplied, in this case 
                    // origin location is mandatory
                    if (originLocation != null)
                    {
                        // DBSRangeType.Interval - time duration window
                        int durationMins = GetDuration(requestedTime.Type, rangeType, range);

                        // DBSRangeType.Sequence - number of rows
                        // If sequence, then range indicates the number of rows to return in the departure board
                        int numberOfRows = 0;
                        if (rangeType == DBSRangeType.Sequence)
                            numberOfRows = range;
                                                
                        // Get the LDB response
                        DBWS.DBWSResult dbwsResult = GetDepartureBoardResult(TemplateRequestType.TripRequestByCRS,
                            originLocation, destinationLocation, showDepartures, numberOfRows, requestedDateTime, durationMins, string.Empty);

                        //Check response 
                        if (dbwsResult != null)
                        {
                            DBWSConverter dbwsConverter = new DBWSConverter();

                            // Build the result
                            trainDbsResult = dbwsConverter.BuildDBSResult(dbwsResult,
                                originLocation, destinationLocation,
                                showDepartures, requestedDateTime.Date, 
                                TemplateRequestType.TripRequestByCRS);
                        }
                        else
                        {
                            // Null DBWS result
                            RTTIUtilities.BuildRTTIError(Messages.RTTIResponseInValid, trainDbsResult);
                        }
                    }
                    else
                    {
                        // Null location request
                        RTTIUtilities.BuildRTTIError(Messages.NullLocationRequest, trainDbsResult);
                    }

                    #endregion
                }
                else
                {
                    #region Train service request

                    // Get the LDB response
                    DBWS.DBWSResult dbwsResult = GetDepartureBoardResult(TemplateRequestType.TrainRequestByRID,
                        null, null, true, 0, requestedDateTime, 0, serviceNumber);

                    //Check response 
                    if (dbwsResult != null)
                    {
                        DBWSConverter dbwsConverter = new DBWSConverter();

                        // Build the result
                        trainDbsResult = dbwsConverter.BuildDBSResult(dbwsResult,
                            null, null, false, requestedDateTime.Date,
                            TemplateRequestType.TrainRequestByRID);
                    }
                    else
                    {
                        // Null DBWS result
                        RTTIUtilities.BuildRTTIError(Messages.RTTIResponseInValid, trainDbsResult);
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("Error occured in LDBHandler GetDepartureBoardTrip. Error: {0}. {1}" + ex.Message, ex.StackTrace);
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message));
                
                // Include error meesage in return result
                RTTIUtilities.BuildRTTIError(Messages.RTTIUnknownError, trainDbsResult);
            }

            return trainDbsResult;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Calculates Datetime for DepartureBoards request
        /// </summary>
        /// <param name="dbsTimeRequest">DBSTimeRequest object representing the time for the departure board</param>
        /// <returns>Datetime object</returns>
        private DateTime GetDateTime(DBSTimeRequest dbsTimeRequest)
        {
            // Default, this will be treated as date time of Now
            DateTime datetime = DateTime.MinValue;

            if (dbsTimeRequest == null)
                return datetime;

            switch (dbsTimeRequest.Type)
            {
                case TimeRequestType.Now:
                    datetime = DateTime.Now;
                    break;

                case TimeRequestType.TimeToday:
                    datetime = DateTime.Today;
                    datetime = datetime.AddHours(dbsTimeRequest.Hour);
                    datetime = datetime.AddMinutes(dbsTimeRequest.Minute);
                    break;

                case TimeRequestType.TimeTomorrow:
                    datetime = DateTime.Today.AddDays(1);
                    datetime = datetime.AddHours(dbsTimeRequest.Hour);
                    datetime = datetime.AddMinutes(dbsTimeRequest.Minute);
                    break;

                // When request is First, the DepartureBoardService.cs updates the requested time to
                // be first service of the day time and repeatedly calls get departures amending the time
                // until a result is returned, so no need to change the times here
                case TimeRequestType.First:
                case TimeRequestType.FirstToday:
                    // handle First and FirstToday the same
                    datetime = DateTime.Today;
                    datetime = datetime.AddHours(dbsTimeRequest.Hour);
                    datetime = datetime.AddMinutes(dbsTimeRequest.Minute);
                    break;

                case TimeRequestType.FirstTomorrow:
                    datetime = DateTime.Today.AddDays(1);
                    datetime = datetime.AddHours(dbsTimeRequest.Hour);
                    datetime = datetime.AddMinutes(dbsTimeRequest.Minute);
                    break;

                // When request is Last, the DepartureBoardService.cs updates the requested time to
                // be last service of the day time and repeatedly calls get departures amending the time
                // until a result is returned, so no need to change the times here
                case TimeRequestType.Last:
                case TimeRequestType.LastToday:
                    // handle Last and LastToday the same
                    datetime = DateTime.Today;
                    datetime = datetime.AddHours(dbsTimeRequest.Hour);
                    datetime = datetime.AddMinutes(dbsTimeRequest.Minute);
                    break;

                case TimeRequestType.LastTomorrow:
                    datetime = DateTime.Today.AddDays(1);
                    datetime = datetime.AddHours(dbsTimeRequest.Hour);
                    datetime = datetime.AddMinutes(dbsTimeRequest.Minute);
                    break;

                default:
                    // Default, this will be treated as date time of Now
                    datetime = DateTime.MinValue;
                    break;
            }

            return datetime;
        }

        /// <summary>
        /// Calculates Time Duration window for DepartureBoards request
        /// </summary>
        private int GetDuration(TimeRequestType requestType, DBSRangeType rangeType, int range)
        {
            int durationMins = -1;
            
            // DBSRangeType.Interval - time duration window
            if (rangeType == DBSRangeType.Interval)
            {
                // Set duration to the requested value
                durationMins = range;

                switch (requestType)
                {
                    case TimeRequestType.First:
                    case TimeRequestType.FirstToday:
                    case TimeRequestType.FirstTomorrow:
                        // If First request, then increase the duration to allow for attempting to 
                        // find the first service of the day
                        durationMins = int.Parse(Properties.Current[Keys.RTTIFirstServiceDuration]) ; ;
                        break;

                    case TimeRequestType.Last:
                    case TimeRequestType.LastToday:
                    case TimeRequestType.LastTomorrow:
                        // If Last request, then increase the duration to allow for attempting to 
                        // find the last service of the day
                        durationMins = int.Parse(Properties.Current[Keys.RTTILastServiceDuartion]);
                        break;
                }
            }

            return durationMins;
        }

        /// <summary>
        /// Method to call the DepartureBoardWebService
        /// </summary>
        private DBWS.DBWSResult GetDepartureBoardResult(TemplateRequestType requestType,
            DBSLocation originStation, DBSLocation destinationStation,
            bool showDepartures, int numberOfRows, DateTime requestedDatetime, int durationMins,
            string serviceId)
        {
            DBWS.DBWSResult result = null;

            // Get a unique request id
            string requestId = Guid.NewGuid().ToString();
             
            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    string.Format("DepartureBoardService - Web service call to DepartureBoardWebService started. RequestId[{0}]", requestId)));
            }

            try
            {
                // Use the DepartureBoardWebService to get the xml response using the NRE LDB web service.
                using (DBWS.DepartureBoards webServiceDepartureBoards = new DBWS.DepartureBoards())
                {
                    // Webservice override url from properties
                    webServiceDepartureBoards.Url = Properties.Current["DepartureBoardService.DepartureBoardWebService.URL"];
                    webServiceDepartureBoards.Timeout = Convert.ToInt32(TransportDirect.Common.PropertyService.Properties.Properties.Current["DepartureBoardService.DepartureBoardWebService.TimeoutMillisecs"]);

                    #region Call web service

                    DBWSConverter dbwsConverter = new DBWSConverter();

                    // Build a request for the web service.
                    // DBWSRequest requires a location for the station board, and optionally a filter location 
                    // to filter services (going to or coming from).
                    // LDBHandler uses the NRE LDB web service call, and to maintain consistency with the legacy RDHandler,
                    // the origin and destination locations are set here to determine which is the main and filter locations:
                    //   showDepartures true flag indicates departures from origin location, optionally going to destination location.
                    //   showDepartures false flag indicates arrivals at destination location (or origin if destination null), optionally coming from origin location
                    // Note: See GetXMlResponse GetXMlResponse for how it handles it's call to the DBWS for NRE EnquirePorts call

                    // Assume station board is for origin location
                    DBSLocation stopLocation = originStation;
                    DBSLocation filterLocation = destinationStation;

                    #region Update locations for arrivals request
                    // If service arrivals are required, then update the stop and filter locations to 
                    // get the station board for
                    if (!showDepartures)
                    {
                        if (destinationStation != null && !string.IsNullOrEmpty(destinationStation.Code))
                        {
                            // destination location now becomes the station board location
                            stopLocation = destinationStation;

                            if (originStation != null && !string.IsNullOrEmpty(originStation.Code))
                                // And origin location becomes the filter location
                                filterLocation = originStation;
                            else
                                // Reset filter to prevent stop and filter both being the destinationLocation
                                filterLocation = null;
                        }
                        // Else the station board is still for the origin location
                    }
                    #endregion

                    DBWS.DBWSRequest request = dbwsConverter.BuildDBWSRequest(
                        requestId,
                        stopLocation,
                        filterLocation,
                        showDepartures,
                        !showDepartures,
                        numberOfRows,
                        requestedDatetime,
                        durationMins,
                        serviceId);

                    #region Call web service

                    switch (requestType)
                    {
                        case TemplateRequestType.TripRequestByCRS:
                            Logger.Write(new OperationalEvent(TDEventCategory.ThirdParty, TDTraceLevel.Verbose,
                                string.Format("Calling GetStationBoard: RequestId[{0}] OriginCode[{1}] DestinationCode[{2}] ShowDepartures[{3}]",
                                    requestId,
                                    (originStation != null) ? originStation.Code : string.Empty,
                                    (destinationStation != null) ? destinationStation.Code : string.Empty,
                                    showDepartures)));

                            result = webServiceDepartureBoards.GetStationBoard(request);
                            break;

                        case TemplateRequestType.TrainRequestByRID:
                            Logger.Write(new OperationalEvent(TDEventCategory.ThirdParty, TDTraceLevel.Verbose,
                                string.Format("Calling GetServiceDetail: RequestId[{0}] ServiceId[{1}]",
                                    requestId, serviceId)));

                            result = webServiceDepartureBoards.GetServiceDetail(request);
                            break;
                    }

                    #endregion

                    #endregion
                }
            }
            catch (Exception ex)
            {
                // log the error 
                string message = string.Format("Error occured in Web service call to DepartureBoardWebService for RequestId[{0}]: {1} {2}", requestId, ex.Message, ex.StackTrace);
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message));
            }

            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    string.Format("DepartureBoardService - Web service call to DepartureBoardWebService completed. RequestId[{0}]", requestId)));

                // Log the call result as xml
                Logger.Write(new OperationalEvent(TDEventCategory.ThirdParty, TDTraceLevel.Verbose,
                    string.Format("DepartureBoardWebService result: {0}", ConvertToXML(result))));
            }
            
            return result;
        }

        /// <summary>
        /// Create an XML representtaion of the specified object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="requestId"></param>
        /// <returns>XML string, prefixed by request id</returns>
        private string ConvertToXML(object obj)
        {
            // Placing in a try catch because we're not confident all objects can be XmlSerialized,
            try
            {
                if (obj != null)
                {
                    XmlSerializer xmls = new XmlSerializer(obj.GetType());
                    StringWriter sw = new StringWriter();
                    xmls.Serialize(sw, obj);
                    sw.Close();

                    return sw.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                    string.Format("Error during XmlSerialize for object: {0} {1}", ex.Message, ex.StackTrace)));
            }

            return string.Empty;
        }

        #endregion
    }
}
