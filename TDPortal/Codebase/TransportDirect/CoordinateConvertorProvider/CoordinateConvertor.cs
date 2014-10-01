// *********************************************** 
// NAME                 : CoordinateConvertor.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 28/05/2009
// DESCRIPTION  		: Class used to call CoordinateConvertor web service
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CoordinateConvertorProvider/CoordinateConvertor.cs-arc  $
//
//   Rev 1.1   Oct 01 2009 10:53:14   pghumra
//Applied changes for cycle planner landing page, latitude longitude coordinates in landing page and find nearest car park functionality
//Resolution for 5316: CCN537 Cycle Planning Page Landing
//Resolution for 5317: CCNxxx Lat Long Coordinates in Page Landing
//
//   Rev 1.0   Jun 03 2009 11:09:22   mmodi
//Initial revision.
//Resolution for 5293: Cycle Planner - Coordinate convertor (Quest InGrid) to be added as a Web Service

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Services.Protocols;

using TransportDirect.Common;
using TransportDirect.Common.Logging;

using TransportDirect.UserPortal.CoordinateConvertorProvider.CoordinateConvertorWebService;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.CoordinateConvertorProvider
{
    public class CoordinateConvertor : ICoordinateConvertor, IDisposable
    {
        #region Private members

        private CoordinateConvertorWebService.CoordinateConvertor webServiceCoordinateConvertor = null;

        #endregion

        #region constructor
        
        /// <summary>
        /// Constructor
        /// </summary>
        public CoordinateConvertor()
        {
            //initialise the services
            webServiceCoordinateConvertor = new TransportDirect.UserPortal.CoordinateConvertorProvider.CoordinateConvertorWebService.CoordinateConvertor();

            //get the corresponding webservice override url from the database
            webServiceCoordinateConvertor.Url = TransportDirect.Common.PropertyService.Properties.Properties.Current["CoordinateConvertor.WebService.URL"];
            webServiceCoordinateConvertor.Timeout = Convert.ToInt32(TransportDirect.Common.PropertyService.Properties.Properties.Current["CoordinateConvertor.WebService.TimeoutMillisecs"]);
        }

        #endregion

        #region ICoordinateConvertor methods

        /// <summary>
        /// Convert a LatitudeLongitude into an OSGR by calling the CoordinateConverter web service
        /// </summary>
        /// <param name="latlong"></param>
        /// <returns></returns>
        public OSGridReference GetOSGridReference(LatitudeLongitude latlong)
        {
            LatitudeLongitude[] latlongs = new LatitudeLongitude[1];
            latlongs[0] = latlong;

            OSGridReference[] osgrs = GetOSGridReference(latlongs);

            return osgrs[0];
        }

        /// <summary>
        /// Converts an OSGR into a LatitudeLongitude by calling the CoordinateConvertor web service 
        /// </summary>
        /// <param name="osgr"></param>
        /// <returns></returns>
        public LatitudeLongitude GetLatitudeLongitude(OSGridReference osgr)
        {
            // Place OSGR into an array
            OSGridReference[] osgrs = new OSGridReference[1];
            osgrs[0] = osgr;
            
            // Call web service
            LatitudeLongitude[] latlongs = GetLatitudeLongitude(osgrs);

            // Assume if all went well, there will be only one latitudelongitude to return. 
            // Any exceptions will have been logged and get thrown to caller.
            return latlongs[0];
        }

        /// <summary>
        /// Converts the LatitudeLongitudes into OSGRs by calling the CoordinateConverter web service
        /// </summary>
        /// <param name="latlongs"></param>
        /// <returns></returns>
        public OSGridReference[] GetOSGridReference(LatitudeLongitude[] latlongs)
        {
            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    "CoordinateConverter - Web service call to ConvertLatitudeLongitudetoOSGR started."));
            }
            OSGridReference[] result = null;

            DateTime callStartTime = DateTime.UtcNow;
            bool successful = false;
            try
            {
                result = webServiceCoordinateConvertor.ConvertLatitudeLongitudetoOSGR(latlongs);
                successful = true;
            }
            catch (SoapException se)
            {
                LogSoapException("ConvertLatitudeLongitudetoOSGR", se);
            }
            catch (WebException we)
            {
                LogWebException("ConvertLatitudeLongitudetoOSGR", we);
            }
            finally
            {
                LogCallEvent("ConvertLatitudeLongitudetoOSGR", callStartTime, successful);
            }
            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    "CoordinateConvertor - Web service call to ConvertLatitudeLongitudetoOSGR completed."));
            }
            return result;
        }

        /// <summary>
        /// Converts the OSGRs into LatitudeLongitudes by calling the CoordinateConvertor web service
        /// </summary>
        /// <param name="osgr"></param>
        /// <returns></returns>
        public LatitudeLongitude[] GetLatitudeLongitude(OSGridReference[] osgrs)
        {
            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    "CoordinateConvertor - Web service call to ConvertOSGRtoLatitudeLongitude started."));
            }

            //declare the output object
            LatitudeLongitude[] result = null;

            DateTime callStartTime = DateTime.UtcNow;
            bool successful = false;

            try
            {
                // Call webservice
                result = webServiceCoordinateConvertor.ConvertOSGRtoLatitudeLongitude(osgrs);

                // Set succesful here if we want to note down that the call produced successful results
                successful = true;
            }
            catch (SoapException se)
            {
                LogSoapException("ConvertOSGRtoLatitudeLongitude", se);
            }
            catch (WebException we)
            {
                LogWebException("ConvertOSGRtoLatitudeLongitude", we);
            }
            finally
            {
                //log the callevent
                LogCallEvent("ConvertOSGRtoLatitudeLongitude", callStartTime, successful);
            }

            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    "CoordinateConvertor - Web service call to ConvertOSGRtoLatitudeLongitude completed."));
            }

            return result;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method that logs a SOAP exception if one is detected when calling the web service
        /// </summary>
        /// <param name="se">Soap exception</param>
        /// <returns>TD exception</returns>
        private void LogSoapException(string webServiceMethod, SoapException se)
        {
            string message = "CoordinateConvertor - A SOAP exception has occured when trying to call the Web service: "
                + webServiceMethod
                + " Exception: "
                + se.Message;

            OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message);
            Logger.Write(oe);

            //log an operational event and create new TDException using the soap exception 
            throw new TDException(message, true, TDExceptionIdentifier.CCSoapException, se);
        }

        /// <summary>
        /// Method that logs a Web exception if one is detected when calling the web service
        /// </summary>
        /// <param name="we">Web exception</param>
        /// <returns>TD exception</returns>
        private void LogWebException(string webServiceMethod, WebException we)
        {
            string message = string.Empty;

            if (we.Status == WebExceptionStatus.Timeout)
            {
                message = "CoordinateConvertor - A Timeout Web exception has occured when trying to call the Web service: "
                    + webServiceMethod
                    + " Exception: "
                    + we.Message;
            }
            else
            {
                message = "CoordinateConvertor - A Web exception has occured when trying to call the Web service: "
                    + webServiceMethod
                    + " Exception: "
                    + we.Message;
            }

            OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message);
            Logger.Write(oe);

            //log an operational event and create new TDException using the web exception 
            throw new TDException(message, true, TDExceptionIdentifier.CCWebException, we);
        }

        /// <summary>
        /// Method that logs the web service call time, and whether it was successfule
        /// </summary>
        /// <param name="callStartTime"></param>
        /// <param name="successful"></param>
        private void LogCallEvent(string webServiceMethod, DateTime callStartTime, bool successful)
        {
            string message = "CoordinateConvertor - Web service call made to: " + webServiceMethod
                + " at startTime: " + callStartTime
                + " successful: " + successful;

            OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, message);
            Logger.Write(oe);
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
        ~CoordinateConvertor()
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
                if (webServiceCoordinateConvertor != null)
                {
                    webServiceCoordinateConvertor.Dispose();
                    webServiceCoordinateConvertor = null;
                }

            }
        }

        #endregion
    }
}
