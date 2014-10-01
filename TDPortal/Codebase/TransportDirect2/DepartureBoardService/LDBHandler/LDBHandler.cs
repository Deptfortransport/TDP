// *********************************************** 
// NAME             : LDBHandler.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2014
// DESCRIPTION  	: LDBHandler class for handling data from the DepartureBoardWebService 
//                  : using the LiveDepartureBoard web service method calls
// ************************************************
// 

using System;
using System.IO;
using System.ServiceModel;
using System.Xml.Serialization;
using TDP.Common;
using TDP.Common.EventLogging;
using TDP.Common.Extenders;
using TDP.Common.LocationService;
using TDP.Common.PropertyManager;
using TDP.UserPortal.DepartureBoardService.DepartureBoardServiceTypes;
using DBWS = TDP.UserPortal.DepartureBoardService.DepartureBoardWebService;
using Logger = System.Diagnostics.Trace;

namespace TDP.UserPortal.DepartureBoardService
{
    /// <summary>
    /// LDBHandler class for handling data from the DepartureBoardWebService 
    /// using the LiveDepartureBoard web service method calls
    /// </summary>
    public class LDBHandler : IDisposable
    {
        #region Private variables

        private DBWS.DepartureBoardsSoapClient webServiceDepartureBoards = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public LDBHandler()
        {
            // Read configuration properites
            string serviceAddress = Properties.Current["DepartureBoards.WebService.URL"];
            int serviceTimeout = Properties.Current["DepartureBoards.WebService.Timeout.Seconds"].Parse<int>(60); // Default to 60 seconds

            if (string.IsNullOrEmpty(serviceAddress))
            {
                throw new TDPException("DepartureBoardService - Property[DepartureBoards.WebService.URL] is missing or invalid, unable to initialise DepartureBoardsWebService",
                    false, TDPExceptionIdentifier.DBSWebServiceUrlNotValid);
            }

            //initialise the service
            BasicHttpBinding serviceBinding = new BasicHttpBinding();
            serviceBinding.SendTimeout = new TimeSpan(0, 0, serviceTimeout);
            serviceBinding.ReceiveTimeout = new TimeSpan(0, 0, serviceTimeout);
            serviceBinding.MaxReceivedMessageSize = Properties.Current["DepartureBoards.WebService.MaxReceivedMessageSize"].Parse(1000000);
            
            EndpointAddress serviceEndpoint = new EndpointAddress(serviceAddress);

            webServiceDepartureBoards = new DBWS.DepartureBoardsSoapClient(serviceBinding, serviceEndpoint); 
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Station level request method for DepartureBoard information
        /// </summary>
        public DBSResult GetDepartureBoardTrip(
            string requestId,
            TDPStopLocation location,
            TDPStopLocation locationFilter,
            bool showDepartures,
            DateTime time,
            int numberOfRows,
            int durationMins,
            string serviceNumber)
        {
            // Result object
            DBSResult trainDbsResult = new DBSResult();

            try
            {
                // Requested datetime 
                DateTime requestedDateTime = time;

                // Determine if this is a station board or for a train service,
                // abscence of a serviceNumber indicates this (replicated from RDHandler logic!)
                if (string.IsNullOrEmpty(serviceNumber))
                {
                    #region Station board request

                    // Only do a call if a location for station board has supplied, in this case 
                    // origin location is mandatory
                    if (location != null)
                    {                           
                        // Get the LDB response
                        DBWS.DBWSResult dbwsResult = GetDepartureBoardResult(
                            true,
                            location, locationFilter, 
                            showDepartures, numberOfRows, requestedDateTime, durationMins, string.Empty);

                        //Check response 
                        if (dbwsResult != null)
                        {
                            DBWSConverter dbwsConverter = new DBWSConverter();

                            // Build the result
                            trainDbsResult = dbwsConverter.BuildDBSResult(dbwsResult,
                                location, locationFilter,
                                showDepartures, requestedDateTime.Date, 
                                true);
                        }
                        else
                        {
                            // Null DBWS result
                            trainDbsResult = DepartureBoardService.GetInvalidDBSResult(TDPExceptionIdentifier.DBSLDBResponseInvalid, "LiveDepartureBoard response is invalid");
                        }
                    }
                    else
                    {
                        // Null location request
                        trainDbsResult = DepartureBoardService.GetInvalidDBSResult(TDPExceptionIdentifier.DBSRequestLocationNull, "Location is null");
                    }

                    #endregion
                }
                else
                {
                    #region Train service request

                    // Get the LDB response
                    DBWS.DBWSResult dbwsResult = GetDepartureBoardResult(false,
                        null, null, true, 0, requestedDateTime, 0, serviceNumber);

                    //Check response 
                    if (dbwsResult != null)
                    {
                        DBWSConverter dbwsConverter = new DBWSConverter();

                        // Build the result
                        trainDbsResult = dbwsConverter.BuildDBSResult(dbwsResult,
                            null, null, false, requestedDateTime.Date,
                            false);
                    }
                    else
                    {
                        // Null DBWS result
                        trainDbsResult = DepartureBoardService.GetInvalidDBSResult(TDPExceptionIdentifier.DBSLDBResponseInvalid, "LiveDepartureBoard response is invalid");
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("Error occured in LDBHandler GetDepartureBoardTrip. Error: {0}. {1}" + ex.Message, ex.StackTrace);
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, message));
                
                // Include error meesage in return result
                trainDbsResult = DepartureBoardService.GetInvalidDBSResult(TDPExceptionIdentifier.DBSLDBUnknownError, message);
            }

            return trainDbsResult;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method to call the DepartureBoardWebService
        /// </summary>
        private DBWS.DBWSResult GetDepartureBoardResult(bool isStationBoard,
            TDPStopLocation location, TDPStopLocation locationFilter,
            bool showDepartures, int numberOfRows, DateTime requestedDatetime, int durationMins,
            string serviceId)
        {
            DBWS.DBWSResult result = null;

            // Get a unique request id
            string requestId = Guid.NewGuid().ToString();
             
            if (TDPTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose,
                    string.Format("DepartureBoardService - Web service call to DepartureBoardWebService started. RequestId[{0}]", requestId)));
            }

            try
            {
                // Use the DepartureBoardWebService to get the xml response using the NRE LDB web service.
                #region Call web service

                    DBWSConverter dbwsConverter = new DBWSConverter();

                    // Build a request for the web service.
                    // DBWSRequest requires a location for the station board, and optionally a filter location 
                    // to filter services (going to or coming from).
                    
                    DBWS.DBWSRequest request = dbwsConverter.BuildDBWSRequest(
                        requestId,
                        location,
                        locationFilter,
                        showDepartures,
                        !showDepartures,
                        numberOfRows,
                        requestedDatetime,
                        durationMins,
                        serviceId);

                    #region Call web service

                    if (isStationBoard)
                    {
                            Logger.Write(new OperationalEvent(TDPEventCategory.ThirdParty, TDPTraceLevel.Verbose,
                                string.Format("Calling GetStationBoard: RequestId[{0}] OriginCode[{1}] DestinationCode[{2}] ShowDepartures[{3}]",
                                    requestId,
                                    (location != null) ? location.Code : string.Empty,
                                    (locationFilter != null) ? locationFilter.Code : string.Empty,
                                    showDepartures)));

                            result = webServiceDepartureBoards.GetStationBoard(request);
                    }
                    else
                    {
                            Logger.Write(new OperationalEvent(TDPEventCategory.ThirdParty, TDPTraceLevel.Verbose,
                                string.Format("Calling GetServiceDetail: RequestId[{0}] ServiceId[{1}]",
                                    requestId, serviceId)));

                            result = webServiceDepartureBoards.GetServiceDetail(request);
                    }

                    #endregion

                    #endregion
                
            }
            catch (Exception ex)
            {
                // log the error 
                string message = string.Format("Error occured in Web service call to DepartureBoardWebService for RequestId[{0}]: {1} {2}", requestId, ex.Message, ex.StackTrace);
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, message));
            }

            if (TDPTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose,
                    string.Format("DepartureBoardService - Web service call to DepartureBoardWebService completed. RequestId[{0}]", requestId)));

                // Log the call result as xml
                Logger.Write(new OperationalEvent(TDPEventCategory.ThirdParty, TDPTraceLevel.Verbose,
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
                    using (StringWriter sw = new StringWriter())
                    {
                        xmls.Serialize(sw, obj);
                        sw.Flush();
                        
                        return sw.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error,
                    string.Format("Error during XmlSerialize for object: {0} {1}", ex.Message, ex.StackTrace)));
            }

            return string.Empty;
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
        ~LDBHandler()
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
                if (webServiceDepartureBoards != null)
                {
                    webServiceDepartureBoards.Close();
                    webServiceDepartureBoards = null;
                }

            }
        }

        #endregion
    }
}