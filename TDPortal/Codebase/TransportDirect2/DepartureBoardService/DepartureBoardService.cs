// *********************************************** 
// NAME             : DepartureBoardService.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2014
// DESCRIPTION  	: DepartureBoardService class providing departure boards
// ************************************************
// 

using System;
using System.IO;
using System.Xml.Serialization;
using TDP.Common;
using TDP.Common.EventLogging;
using TDP.Common.LocationService;
using TDP.UserPortal.DepartureBoardService.DepartureBoardServiceTypes;
using Logger = System.Diagnostics.Trace;

namespace TDP.UserPortal.DepartureBoardService
{
    /// <summary>
    /// DepartureBoardService class providing departure boards
    /// </summary>
    public class DepartureBoardService
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DepartureBoardService()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Station Board information
        /// </summary>
        /// <returns>DBSResult object holding DepartureBoard info</returns>
        public DBSResult GetStationBoard(
            string requestId,
            TDPStopLocation location,
            TDPStopLocation locationFilter,
            bool showDepartures,
            DateTime time,
            int numberOfRows,
            int durationMins)
        {

            // Init for logging
            DateTime logStart = DateTime.Now;

            DBSResult result = new DBSResult();

            // Validate 
            if (!ValidateRequest(ref result, location))
            {
                return result;
            }

            // Call appropriate provider
            switch (location.TypeOfLocationActual)
            {
                case TDPLocationType.StationRail:

                    // Call Live Departure Boards
                    using (LDBHandler handler = new LDBHandler())
                    {
                        // Call trip request method!
                        result = handler.GetDepartureBoardTrip(requestId, location, locationFilter, showDepartures, time, numberOfRows, durationMins, string.Empty);
                    }
                    break;
            }

            // TODO: Mitesh - ExposedServicesEvent event for DepartureBoardService request
            //bool success = (result.Messages == null || result.Messages.Length == 0);
            //LogEvent(logStart, ExposedServicesCategory.DepartureBoardServiceRTTI, success, token);

            if (TDPTraceSwitch.TraceVerbose)
            {
                // Log the call result as xml
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose,
                    string.Format("DBSResult: {0}", ConvertToXML(result))));
            }

            return result;
        }

        /// <summary>
        /// Service detail information
        /// </summary>
        /// <returns>DBSResult object holding Service detail info</returns>
        public DBSResult GetServiceDetails(
            string requestId,
            DateTime time,
            string serviceId)
        {

            // Init for logging
            DateTime logStart = DateTime.Now;

            DBSResult result = new DBSResult();

            // Validate 
            if (string.IsNullOrEmpty(serviceId))
            {
                result = GetInvalidDBSResult(TDPExceptionIdentifier.DBSRequestServiceIdInvalid, "Service Id is invalid");
                return result;
            }

            // Call appropriate provider
            // Call Live Departure Boards to retrieve the service details
            using (LDBHandler handler = new LDBHandler())
            {
                // Call service request method
                result = handler.GetDepartureBoardTrip(requestId, null, null, true, time, 0, 0, serviceId);
            }

            // TODO: Mitesh - ExposedServicesEvent event for DepartureBoardService request
            //bool success = (result.Messages == null || result.Messages.Length == 0);
            //LogEvent(logStart, ExposedServicesCategory.DepartureBoardServiceRTTI, success, token);

            if (TDPTraceSwitch.TraceVerbose)
            {
                // Log the call result as xml
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose,
                    string.Format("DBSResult: {0}", ConvertToXML(result))));
            }

            return result;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Validates the request
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private bool ValidateRequest(ref DBSResult result, TDPStopLocation location)
        {
            if (location == null)
            {
                // Null location request
                result = GetInvalidDBSResult(TDPExceptionIdentifier.DBSRequestLocationNull, "Location is null");
                return false;
            }

            switch (location.TypeOfLocationActual)
            {
                case TDPLocationType.StationRail:
                    // Rail location request, check there is a CRS code
                    if (string.IsNullOrEmpty(location.CodeCRS))
                    {
                        result = GetInvalidDBSResult(TDPExceptionIdentifier.DBSRequestLocationInvalid, "Location CRS code is invalid");
                        return false;
                    }
                    break;
                default:
                    // Unsupported location type
                    result = GetInvalidDBSResult(TDPExceptionIdentifier.DBSRequestLocationUnsupported,
                        string.Format("Unsupported location type[{0}]", location.TypeOfLocationActual.ToString()));
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Methods that creates a new DBSResult with error messages only following an invalid request
        /// </summary>
        internal static DBSResult GetInvalidDBSResult(TDPExceptionIdentifier messageCode, string messageDescription)
        {
            DBSResult result = new DBSResult();

            DBSMessage message = new DBSMessage();
            message.Code = (int)messageCode;
            message.Description = messageDescription;

            result.Messages = new DBSMessage[] { message };

            return result;
        }

        ///// <summary>
        ///// Private method used to log an ExposedServices Event of a specific category
        ///// </summary>
        ///// <param name="submitted">DateTime submitted</param>
        ///// <param name="category">Category of ExposedServicesEvent</param>
        ///// <param name="success">success of request</param>
        ///// <param name="token">authentication/identification token</param>
        //private void LogEvent(DateTime submitted, ExposedServicesCategory category, bool success, string token)
        //{
        //    Logger.Write(new ExposedServicesEvent(token, submitted, category, success));
        //}

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
                    string.Format("Error during XmlSerialize for object: {0} {1} {2} {3}", ex.Message, ex.StackTrace,
                    (ex.InnerException != null) ? ex.InnerException.Message : string.Empty,
                    (ex.InnerException != null) ? ex.InnerException.StackTrace : string.Empty)));
            }

            return string.Empty;
        }

        #endregion
    }
}
