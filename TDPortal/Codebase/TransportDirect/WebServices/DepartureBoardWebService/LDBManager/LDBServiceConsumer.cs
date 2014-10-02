// *********************************************** 
// NAME             : LDBServiceConsumer.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: LDBServiceConsumer class for sending request and reciving response from NRE Live Departure Board Web Service
// ************************************************
// 

using System;
using System.IO;
using System.ServiceModel;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.WebService.DepartureBoardWebService.DataTransfer;
using LDB = TransportDirect.WebService.DepartureBoardWebService.LDBWebService;
using Logger = System.Diagnostics.Trace;
using TransportDirect.ReportDataProvider.TDPCustomEvents;

namespace TransportDirect.WebService.DepartureBoardWebService.LDBManager
{
    /// <summary>
    /// LDBServiceConsumer class for sending request and reciving response from NRE Live Departure Board Web Service
    /// </summary>
    public class LDBServiceConsumer : IDisposable
    {
        #region Private variables

        private LDBWebService.LDBServiceSoapClient ldbWebService = null;

        private bool useStubMode = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public LDBServiceConsumer()
        {
            #region Initialise LDB Web Service

            if (ldbWebService == null)
            {
                // Read configuration properites
                string serviceAddress = Properties.Current["DepartureBoards.LDBWebService.URL"];
                string serviceBindingName = Properties.Current["DepartureBoards.LDBWebService.ServiceBindingName"];
                
                if (string.IsNullOrEmpty(serviceAddress) || string.IsNullOrEmpty(serviceBindingName))
                {
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Business, string.Empty, TDTraceLevel.Error,
                        "Unable to access the LDBWebService url and/or service binding property");
                    Logger.Write(oe);

                    throw new TDException(oe.Message, true, TDExceptionIdentifier.DBWSLDBWebServiceUrlUnavailable);
                }

                //initialise the service
                EndpointAddress serviceEndpoint = new EndpointAddress(serviceAddress);

                ldbWebService = new LDBWebService.LDBServiceSoapClient(serviceBindingName, serviceEndpoint);
            }

            #endregion

            // Check if test mode is required
            if (!bool.TryParse(Properties.Current["DepartureBoards.LDBWebService.Stub.Switch"], out useStubMode))
            {
                useStubMode = false;
            }
		}

		#endregion

        #region Public methods

        /// <summary>
        /// Method calls the LDBWebService returning the station board result for requested location
		/// </summary>
        public DBWSResult GetStationBoard(DBWSRequest request)
        {
            // LDBWebService interface:
            // numRows - Maximum number of rows that should be returned in the station board, 
            // crs - CRS code of the location that the station board is for
            // filterCrs - (optional) CRS code for which station the returned station board is filtered by
            // filterType - (optional) If the board is filtered by services either "from" or "to" (default) the filterCrs location
            // timeOffset - (optional) Offset-times in minutes for defining the service (arrival/departure) staring times filter (default 0 mins)
            // timeWindow - (optional) Service duration time window filter from 1 to 120 minutes (default 120mins)

            LDB.StationBoard ldbStationBoardResult = null;

            // RTTIEvent logging
            DateTime startDateTime = DateTime.Now;
            bool success = false;

            // Parameters for call
            LDB.AccessToken accessToken = GetLDBAccessToken();
            ushort numberOfRows = GetNumberOfRows(request.NumberOfRows);
            string locationCRS = request.Location.LocationCRS;
            string filterCRS = request.LocationFilter.LocationCRS;
            LDB.FilterType filterType = GetLDBFilterType(request.LocationFilterType);
            int timeOffset = GetTimeOffset(request.RequestedTime, request.TimeOffset);
            int timeWindow = GetDuration(request.Duration);

            #region Call LDB web service

            try
            {
                // Update RTTI log time to be just before submitting to LDB Web Service
                startDateTime = DateTime.Now;

                if (!useStubMode)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                        string.Format("Calling LDBWebService station boards for RequestId[{0}] with LocationCRS[{1}] FilterCRS[{2}] FilterType[{3}] TimeOffset[{4}] TimeWindow[{5}]",
                        request.RequestId,
                        locationCRS,
                        filterCRS,
                        filterType,
                        timeOffset,
                        timeWindow)));

                    // Call the correct LDB web service method to return departure/arrival board train services
                    if (request.ShowDepartures && request.ShowArrivals)
                    {
                        ldbStationBoardResult = ldbWebService.GetArrivalDepartureBoard(
                            accessToken, numberOfRows, locationCRS, filterCRS, filterType, timeOffset, timeWindow);
                    }
                    else if (request.ShowDepartures)
                    {
                        ldbStationBoardResult = ldbWebService.GetDepartureBoard(
                            accessToken, numberOfRows, locationCRS, filterCRS, filterType, timeOffset, timeWindow);
                    }
                    else if (request.ShowArrivals)
                    {
                        ldbStationBoardResult = ldbWebService.GetArrivalBoard(
                            accessToken, numberOfRows, locationCRS, filterCRS, filterType, timeOffset, timeWindow);
                    }
                }
                else
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    string.Format("Loading test stub station board for RequestId[{0}] with LocationCRS[{1}]",
                        request.RequestId,
                        locationCRS)));

                    // Load test station board
                    ldbStationBoardResult = LoadTestStationBoard(locationCRS);
                }

                if (ldbStationBoardResult != null)
                {
                    // Log web service call result xml
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                            string.Format("LDBWebService has returned StationBoard result for RequestId[{0}] with LocationCRS[{1}]", request.RequestId, request.Location.LocationCRS)));
                        Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                            ConvertToXML(ldbStationBoardResult)));
                    }

                    // Indicate success for call made to LDBService, 
                    // even if there maybe no services in the station board result returned
                    success = true;
                }
            }
            catch (SoapException spEx)
            {
                // Handle soap exceptions and rethrow
                string message = string.Format("{0}. SoapException Message: {1}. StackTrace: {2}", Messages.Service_SoapException, spEx.Message, spEx.StackTrace);

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message));

                throw new TDException(string.Format("{0}. SoapException Message: {1}", Messages.Service_SoapException, spEx.Message), true, TDExceptionIdentifier.DBWSLDBWebServiceSoapException);
            }
            catch (FaultException ftEx)
            {
                // Handle fault exceptions and rethrow
                string message = string.Format("{0}. FaultException Message: {1}. StackTrace: {2}", Messages.Service_FaultException, ftEx.Message, ftEx.StackTrace);

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message));

                throw new TDException(string.Format("{0}. FaultException Message: {1}", Messages.Service_FaultException, ftEx.Message), true, TDExceptionIdentifier.DBWSLDBWebServiceFaultException);
            }
            finally
            {
                // Log the RTTI Event always (replicates the behaviour in RDServiceConsumer)
                LogRTTIEvent(startDateTime, DateTime.Now, success);

                // Log the RTTI Internal Event always (replicates the behaviour in SocketClient)
                LogRTTIInternalEvent(startDateTime, DateTime.Now, 0, success);
            }

            #endregion

            // Process the StationBoard result
            LDBResultHelper helper = new LDBResultHelper(request, ldbStationBoardResult);

            DBWSResult result = helper.BuildStationBoardResult();
           
            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                string.Format("Finished LDBWebService station boards for RequestId[{0}] with LocationCRS[{1}]. Success[{2}]", request.RequestId, request.Location.LocationCRS, success)));

            return result;
        }

        /// <summary>
        /// Method calls the LDBWebService returning the service detail result for requested service id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DBWSResult GetServiceDetail(DBWSRequest request)
        {
            // LDBWebService interface:
            // serviceID - ServiceId of a service from a station board result.

            LDB.ServiceDetails ldbServiceDetailResult = null;

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    string.Format("Calling LDBWebService service details for RequestId[{0}] with ServiceId[{1}]", request.RequestId, request.ServiceId)));

            // RTTIEvent logging
            DateTime startDateTime = DateTime.Now;
            bool success = false;

            #region Call LDB web service

            try
            {
                // Parameters for call
                LDB.AccessToken accessToken = GetLDBAccessToken();
                string serviceId = request.ServiceId;

                // Update RTTI log time to be just before submitting to LDB Web Service
                startDateTime = DateTime.Now;

                // Call the LDB web service method to return service details
                ldbServiceDetailResult = ldbWebService.GetServiceDetails(accessToken, serviceId);

                if (ldbServiceDetailResult != null)
                {
                    // Log web service call result xml
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                            string.Format("LDBWebService has returned ServiceDetails result for RequestId[{0}] with ServiceId[{1}]", request.RequestId, request.ServiceId)));
                        Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                            ConvertToXML(ldbServiceDetailResult)));
                    }

                    // Indicate success for call made to LDBService, 
                    // even if there maybe no service details in the result returned
                    success = true;
                }
            }
            catch (SoapException spEx)
            {
                // Handle soap exceptions and rethrow
                string message = string.Format("{0}. SoapException Message: {1}. StackTrace: {2}", Messages.Service_SoapException, spEx.Message, spEx.StackTrace);

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message));

                throw new TDException(string.Format("{0}. SoapException Message: {1}", Messages.Service_SoapException, spEx.Message), true, TDExceptionIdentifier.DBWSLDBWebServiceSoapException);
            }
            catch (FaultException ftEx)
            {
                // Handle fault exceptions and rethrow
                string message = string.Format("{0}. FaultException Message: {1}. StackTrace: {2}", Messages.Service_FaultException, ftEx.Message, ftEx.StackTrace);

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message));

                throw new TDException(string.Format("{0}. FaultException Message: {1}", Messages.Service_FaultException, ftEx.Message), true, TDExceptionIdentifier.DBWSLDBWebServiceFaultException);
            }
            finally
            {
                // Log the RTTI Event always (replicates the behaviour in RDServiceConsumer)
                LogRTTIEvent(startDateTime, DateTime.Now, success);

                // Log the RTTI Internal Event always (replicates the behaviour in SocketClient)
                LogRTTIInternalEvent(startDateTime, DateTime.Now, 0, success);
            }

            #endregion

            // Process the StationBoard result
            LDBResultHelper helper = new LDBResultHelper(request, ldbServiceDetailResult);

            DBWSResult result = helper.BuildServiceDetailResult();

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                string.Format("Finished LDBWebService service details for RequestId[{0}] with ServiceId[{1}]. Success[{2}]", request.RequestId, request.ServiceId, success)));

            return result;
        }

        #endregion

        #region Private methods
        
        /// <summary>
        /// Method to return an LDB AccessToken required for the LDB service
        /// </summary>
        /// <returns></returns>
        private LDB.AccessToken GetLDBAccessToken()
        {
            LDB.AccessToken ldbAccessToken = new LDB.AccessToken();

            ldbAccessToken.TokenValue = Properties.Current["DepartureBoards.LDBWebService.Service.AccessToken"];

            return ldbAccessToken;
        }

        /// <summary>
        /// Method to return the number of rows value
        /// </summary>
        /// <returns></returns>
        private ushort GetNumberOfRows(int numberOfRows)
        {
            ushort rows = 0;

            // Set default/max
            ushort rowsDefault = 0;
            ushort rowsMax = 0;

            string rowsDefaultString = Properties.Current["DepartureBoards.LDBWebService.Service.NumberOfRows.Default"];
            string rowsMaxString = Properties.Current["DepartureBoards.LDBWebService.Service.NumberOfRows.Max"];

            if (!ushort.TryParse(rowsDefaultString, out rowsDefault))
                rowsDefault = 10; // Default

            if (!ushort.TryParse(rowsMaxString, out rowsMax))
                rowsMax = 20; // Max

            // Convert requested number of rows to required int type
            if (!UInt16.TryParse(numberOfRows.ToString(), out rows))
                rows = rowsDefault;

            // Validate number of rows
            if (rows <= 0)
                rows = rowsDefault;

            if (rows > rowsMax)
                rows = rowsMax;

            return rows;
        }

        /// <summary>
        /// Method to return the time offset in minutes
        /// </summary>
        /// <returns></returns>
        private int GetTimeOffset(DateTime requestedTime, int requestedTimeOffset)
        {
            // Following logic calculates the timeOffset based on the requestedTime and timeOffset values (if supplied)
            
            // Time offset result
            int timeOffsetMins = 0;
            
            #region Set validation params

            DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);

            // Set default/max
            int timeOffsetDefault = 0;
            int timeOffsetMax = 0;
            int timeOffsetMin = 0;

            string timeOffsetDefaultString = Properties.Current["DepartureBoards.LDBWebService.Service.TimeOffset.Minutes.Default"];
            string timeOffsetMaxString = Properties.Current["DepartureBoards.LDBWebService.Service.TimeOffset.Minutes.Max"];
            string timeOffsetMinString = Properties.Current["DepartureBoards.LDBWebService.Service.TimeOffset.Minutes.Min"];

            if (!int.TryParse(timeOffsetDefaultString, out timeOffsetDefault))
                timeOffsetDefault = 0; // Default

            if (!int.TryParse(timeOffsetMaxString, out timeOffsetMax))
                timeOffsetMax = 119; // Max

            if (!int.TryParse(timeOffsetMinString, out timeOffsetMin))
                timeOffsetMin = 120; // Min

            // Set the min and max time the time can be for
            DateTime timeMin = now.Add(new TimeSpan(0, timeOffsetMin, 0));
            DateTime timeMax = now.Add(new TimeSpan(0, timeOffsetMax, 0));

            // Default offset
            timeOffsetMins = timeOffsetDefault;

            #endregion

            // Assume time is for now
            DateTime time = now;
            DateTime timeOffset = now;

            try
            {
                // Requested time was specified, ignore the seconds to simplyfy working out the offset
                if (requestedTime != DateTime.MinValue)
                    time = new DateTime(requestedTime.Year, requestedTime.Month, requestedTime.Day, requestedTime.Hour, requestedTime.Minute, 0);

                // Update the time with the offset value supplied
                timeOffset = time.Add(new TimeSpan(0, requestedTimeOffset, 0));
                                
                // Validate the time offset is in the allowed time range
                if (timeMin <= timeOffset && timeOffset <= timeMax)
                {
                    // Now work out the offset based on the time requested from now
                    TimeSpan timeDifference = timeOffset.Subtract(now);

                    timeOffsetMins = Convert.ToInt32(timeDifference.TotalMinutes);
                }
                else
                {
                    // Outside of time range, raise exception to return validation reason back to service
                    throw new Exception();
                }
            }
            catch
            {
                // Any exceptions then assume the requested date time is outside of the supported time range
                throw new TDException(
                    string.Format("Requested Time[{0}]{1} is outside of the supported time range Min[{2}] Max[{3}]",
                        time,
                        (requestedTimeOffset == 0) ? string.Empty : string.Format(" Offset[{0}]", requestedTimeOffset), 
                        timeMin, 
                        timeMax),
                    false,
                    TDExceptionIdentifier.DBWSLDBRequestInvalid
                    );
            }
            
            return timeOffsetMins;
        }

        /// <summary>
        /// Method to return the duration in minutes
        /// </summary>
        /// <returns></returns>
        private int GetDuration(int duration)
        {
            // Requested duration
            int durationMins = duration;
            
            // Set default/max
            int durationDefault = 120;
            int durationMax = 0;

            string durationDefaultString = Properties.Current["DepartureBoards.LDBWebService.Service.Duration.Minutes.Default"];
            string durationMaxString = Properties.Current["DepartureBoards.LDBWebService.Service.Duration.Minutes.Max"];

            if (!int.TryParse(durationDefaultString, out durationDefault))
                durationDefault = 120; // Default

            if (!int.TryParse(durationMaxString, out durationMax))
                durationMax = 120; // Max
                        
            // Validate duration
            if (durationMins <= 0)
                durationMins = durationDefault;

            if (durationMins > durationMax)
                durationMins = durationDefault;

            return durationMins;
        }

        /// <summary>
        /// Method to return a LDB FilterType
        /// </summary>
        /// <param name="filterType"></param>
        /// <returns></returns>
        private LDB.FilterType GetLDBFilterType(DBWSFilterType filterType)
        {
            LDB.FilterType ldbFilterType = LDB.FilterType.to;

            switch (filterType)
            {
                case DBWSFilterType.ServicesFrom:
                    ldbFilterType = LDB.FilterType.from;
                    break;
                case DBWSFilterType.ServicesTo:
                default:
                    ldbFilterType = LDB.FilterType.to;
                    break;
            }
            
            return ldbFilterType;
        }

        /// <summary>
        /// Log RTTIEvent 
        /// </summary>
        private void LogRTTIEvent(DateTime startTime, DateTime finishTime, bool dataReceived)
        {
            try
            {
                RTTIEvent rEvent = new RTTIEvent(startTime, finishTime, dataReceived);
                Logger.Write(rEvent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Log RTTIEvent 
        /// </summary>
        private void LogRTTIInternalEvent(DateTime startTime, DateTime finishTime, int numberOfRetries, bool dataReceived)
        {
            try
            {
                RTTIInternalEvent rEvent = new RTTIInternalEvent(startTime, finishTime, numberOfRetries, dataReceived);
                Logger.Write(rEvent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        /// <summary>
        /// Loads a test station board from file
        /// </summary>
        /// <param name="locationCRS"></param>
        /// <returns></returns>
        private LDB.StationBoard LoadTestStationBoard(string locationCRS)
        {
            string filepath = string.Empty;
            LDB.StationBoard stationBoard = null;

            try
            {
                string path = Properties.Current["DepartureBoards.LDBWebService.Stub.Directory"];
                string file = Properties.Current["DepartureBoards.LDBWebService.Stub.File"];

                if (string.IsNullOrEmpty(path))
                    path = @"D:\Temp";
                if (string.IsNullOrEmpty(file))
                    file = "StationBoard{0}.xml";

                // Location CRS specific file
                filepath = string.Format("{0}\\{1}", path, string.Format(file, locationCRS));

                if (!File.Exists(filepath))
                {
                    // Location CRS file doesnt exist, try generic station board
                    filepath = string.Format("{0}\\{1}", path, string.Format(file, string.Empty));
                }

                using (TextReader reader = new StreamReader(filepath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(LDB.StationBoard));

                    stationBoard = (LDB.StationBoard)serializer.Deserialize(reader);

                    // Manuall overide some values
                    stationBoard.generatedAt = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                    string.Format("LDBWebService has failed to load test stub station board for locationCRS[{0}] with file[{1}]. Exception: {2}. {3}",
                        locationCRS, filepath, ex.Message, ex.StackTrace)));
                // Do not throw exception, this is only a test stub
            }

            return stationBoard;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // NOTE: Leave out the finalizer altogether if this class doesn't 
        // own unmanaged resources itself, but leave the other methods
        // exactly as they are. 
        ~LDBServiceConsumer()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (ldbWebService != null)
                {
                    ldbWebService.Close();
                    ldbWebService = null;
                }

            }
        }

        #endregion
    }
}