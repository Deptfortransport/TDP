// *********************************************** 
// NAME             : DepartureBoards.asmx 
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 27 Nov 2013
// DESCRIPTION  	: DepartureBoards web service
// ************************************************
// 

using System;
using System.IO;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using TransportDirect.Common.Logging;
using TransportDirect.WebService.DepartureBoardWebService.DataTransfer;
using TransportDirect.WebService.DepartureBoardWebService.LDBManager;
using TransportDirect.WebService.DepartureBoardWebService.RDManager;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common;

namespace TransportDirect.WebService.DepartureBoardWebService
{
    /// <summary>
    /// DepartureBoards web service to provide departure/arrival boards for a station
    /// </summary>
    [WebService(Namespace = "http://www.transportdirect.info/DepartureBoardWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class DepartureBoards : System.Web.Services.WebService
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DepartureBoards()
		{
		}
	    
        #endregion

        #region Web methods

        /// <summary>
        /// Departures and Arrivals Station board for Rail stops 
        /// using the Live Departure Boards service.
        /// </summary>
        [WebMethod]
        public DBWSResult GetStationBoard(DBWSRequest request)
        {
            DBWSResult dbwsResult = new DBWSResult();

            try
            {
                LogMessage("GetStationBoard called with request: {0}", ConvertToXML(request));

                // Submit request if valid
                if (ValidateStationBoardRequest(request, ref dbwsResult))
                {
                    // Use LDBWebService
                    using (LDBServiceConsumer ldbService = new LDBServiceConsumer())
                    {
                        dbwsResult = ldbService.GetStationBoard(request);
                    }
                }
            }
            catch (TDException tdEx)
            {
                // If it is an invalid request, return a result with message
                if (tdEx.Identifier == TDExceptionIdentifier.DBWSLDBRequestInvalid)
                {
                    LDBResultHelper helper = new LDBResultHelper();

                    dbwsResult = helper.BuildResult((int)TDExceptionIdentifier.DBWSLDBRequestInvalid,
                        string.Format(Messages.LDB_StationBoardRequestInvalid, tdEx.Message));
                }
                else // throw a soap exception
                    throw ThrowSoapException(new Exception(tdEx.Message));
            }
            catch (Exception ex)
            {
                LogError(ex);

                // Throw the exception message to the caller only, prevents stack from being seen
                throw ThrowSoapException(ex);
            }

            LogMessage("GetStationBoard returning with result: {0}", ConvertToXML(dbwsResult));

            return dbwsResult;
        }

        /// <summary>
        /// Departures and Arrivals Station board for Rail stops 
        /// using the Enquiry Ports sockets service.
        /// Provided for legacy calls, please now use GetStationBoard
        /// </summary>
        [WebMethod]
        public string GetStationBoardSocketXml(DBWSRequest request)
        {
            string result = string.Empty;
            DBWSResult dbwsResult = new DBWSResult();

            try
            {
                LogMessage("GetStationBoardSocketXml called with request: {0}", ConvertToXML(request));
                
                // Submit request if valid
                if (ValidateStationBoardRequest(request, ref dbwsResult))
                {
                    // Use Enquiry Ports service
                    using (RDServiceConsumer rdService = new RDServiceConsumer())
                    {
                        result = rdService.GetArrivalDepartureForTrip(request.RequestId,
                            request.Location.LocationCRS,
                            request.LocationFilter.LocationCRS,
                            DateTime.Now, request.Duration, request.ShowDepartures);
                    }
                }
                else
                {
                    // Invalid request, throw exception 
                    throw new Exception(dbwsResult.Messages[0].Description);
                }
            }
            catch (Exception ex)
            {
                LogError(ex);

                // Throw the exception message to the caller only, prevents stack from being seen
                throw ThrowSoapException(ex);
            }

            LogMessage("GetStationBoardSocketXml returning with result: {0}", result);
            
            return result;
        }

        /// <summary>
        /// Service detail for service id from a station board
        /// using the Live Departure Boards service.
        /// </summary>
        [WebMethod]
        public DBWSResult GetServiceDetail(DBWSRequest request)
        {
            DBWSResult dbwsResult = new DBWSResult();

            try
            {
                LogMessage("GetServiceDetail called with request: {0}", ConvertToXML(request));
                
                // Submit request if valid
                if (ValidateServiceDetailRequest(request, ref dbwsResult))
                {
                    // Use LDBWebService
                    using (LDBServiceConsumer ldbService = new LDBServiceConsumer())
                    {
                        dbwsResult = ldbService.GetServiceDetail(request);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex);

                // Throw the exception message to the caller only, prevents stack from being seen
                throw ThrowSoapException(ex);
            }

            LogMessage("GetServiceDetail returning with result: {0}", ConvertToXML(dbwsResult));

            return dbwsResult;
        }

        /// <summary>
        /// Service detail for service id from a station board
        /// using the Enquiry Ports sockets service.
        /// Provided for legacy calls, please now use GetServiceDetail
        /// </summary>
        [WebMethod]
        public string GetServiceDetailSocketXml(DBWSRequest request)
        {
            string result = string.Empty;
            DBWSResult dbwsResult = new DBWSResult();

            try
            {
                LogMessage("GetServiceDetailSocketXml called with request: {0}", ConvertToXML(request));
                
                // Submit request if valid
                if (ValidateServiceDetailRequest(request, ref dbwsResult))
                {
                    // Use Enquiry Ports service
                    using (RDServiceConsumer rdService = new RDServiceConsumer())
                    {
                        result = rdService.GetArrivalDepartureByTrainRID(request.RequestId,
                            request.ServiceId, DateTime.Now);
                    }
                }
                else
                {
                    // Invalid request, throw exception 
                    throw new Exception(dbwsResult.Messages[0].Description);
                }
            }
            catch (Exception ex)
            {
                LogError(ex);

                // Throw the exception message to the caller only, prevents stack from being seen
                throw ThrowSoapException(ex);
            }

            LogMessage("GetServiceDetailSocketXml returning with result: {0}", result);
                        
            return result;
        }

        #endregion

        #region Test Web methods

        /// <summary>
        /// Used to test if the web service is running
        /// </summary>
        /// <returns>True if the web service is running</returns>
        [WebMethod]
        public bool TestActive()
        {
            return true;
        }

        /// <summary>
        /// Used to test the GetStationBoard web method
        /// </summary>
        [WebMethod]
        public DBWSResult TestGetStationBoard(string locationCRS, string filterLocationCRS, string numberOfRows, string timeOffsetMins, string timeWindowMins)
        {
            DBWSRequest request = new DBWSRequest();

            // Build a request
            request.RequestId = string.Format("TestGetDepartureBoard_{0}", DateTime.Now.ToString("yyyyMMddTHHmmss"));
            request.Location = new DBWSLocation();
            request.LocationFilter = new DBWSLocation();
            request.LocationFilterType = DBWSFilterType.ServicesTo;
            request.ShowArrivals = true;
            request.ShowDepartures = true;

            if (!string.IsNullOrEmpty(locationCRS))
                request.Location.LocationCRS = locationCRS;

            if (!string.IsNullOrEmpty(filterLocationCRS))
                request.LocationFilter.LocationCRS = filterLocationCRS;

            int rows = 0;
            if (!Int32.TryParse(numberOfRows, out rows))
                rows = 0;

            int duration = 0;
            if (!Int32.TryParse(timeWindowMins, out duration))
                duration = 0;

            int offset = 0;
            if (!Int32.TryParse(timeOffsetMins, out offset))
                offset = 0;

            request.NumberOfRows = rows;
            request.Duration = duration;
            request.TimeOffset = offset;

            // Make LDBWebService call
            // Not catching any exceptions as this method is for testing only
            return GetStationBoard(request);
        }

        /// <summary>
        /// Used to test the GetStationBoardSocketXml web method
        /// </summary>
        [WebMethod]
        public string TestGetStationBoardSocketXml(string locationCRS, string filterLocationCRS)
        {
            DBWSRequest request = new DBWSRequest();

            // Build a request
            request.RequestId = string.Format("TestGetStationBoardSocketXml");
            request.Location = new DBWSLocation();
            request.LocationFilter = new DBWSLocation();
            request.LocationFilterType = DBWSFilterType.ServicesTo;
            request.ShowArrivals = false;
            request.ShowDepartures = true;

            request.NumberOfRows = 10;
            request.Duration = 120;
            request.RequestedTime = DateTime.Now;

            if (!string.IsNullOrEmpty(locationCRS))
                request.Location.LocationCRS = locationCRS;

            if (!string.IsNullOrEmpty(filterLocationCRS))
                request.LocationFilter.LocationCRS = filterLocationCRS;

            // Make EnquiryPorts call
            // Not catching any exceptions as this method is for testing only
            return GetStationBoardSocketXml(request);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Validates the request for a station board
        /// </summary>
        /// <param name="request"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private bool ValidateStationBoardRequest(DBWSRequest request, ref DBWSResult result)
        {
            LDBResultHelper helper = new LDBResultHelper();

            // Request must exist
            if (request == null)
            {
                result = helper.BuildResult((int)TDExceptionIdentifier.DBWSLDBRequestInvalid,
                    string.Format(Messages.LDB_StationBoardRequestInvalid, Messages.LDB_MissingRequest));
                
                return false;
            }

            // Location must exist for a station board
            if (request.Location == null || string.IsNullOrEmpty(request.Location.LocationCRS))
            {
                result = helper.BuildResult((int)TDExceptionIdentifier.DBWSLDBRequestInvalid,
                    string.Format(Messages.LDB_StationBoardRequestInvalid, Messages.LDB_MissingLocation));

                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates the request for a station board
        /// </summary>
        /// <param name="request"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private bool ValidateServiceDetailRequest(DBWSRequest request, ref DBWSResult result)
        {
            LDBResultHelper helper = new LDBResultHelper();

            // Request must exist
            if (request == null)
            {
                result = helper.BuildResult((int)TDExceptionIdentifier.DBWSLDBRequestInvalid,
                    string.Format(Messages.LDB_ServiceDetailRequestInvalid, Messages.LDB_MissingRequest));

                return false;
            }

            // Service id must exist for a service detail
            if (string.IsNullOrEmpty(request.ServiceId))
            {
                result = helper.BuildResult((int)TDExceptionIdentifier.DBWSLDBRequestInvalid,
                    string.Format(Messages.LDB_ServiceDetailRequestInvalid, Messages.LDB_MissingServiceId));
                
                return false;
            }

            return true;
        }

        /// <summary>
        /// Logs a verbose message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageArg"></param>
        private void LogMessage(string message, string messageArg)
        {
            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(
                    new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    string.Format(message, messageArg)));
            }
        }

        /// <summary>
        /// Logs an Error message for exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageArg"></param>
        private void LogError(Exception ex)
        {
            // Log exception
            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                string.Format("{0}. Exception Message: {1}. Stack Trace: {2}", Messages.Service_InternalError, ex.Message, ex.StackTrace)));
        }

        /// <summary>
        /// Returns a SoapException for the exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private SoapException ThrowSoapException(Exception ex)
        {
            // Build SoapException containing the message
            SoapException se = new SoapException(
                string.Format("{0}. Exception Message: {1}", Messages.Service_InternalError, ex.Message),
                SoapException.ServerFaultCode);

            return se;
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