// ***********************************************
// NAME 		: CyclePlanner.cs
// AUTHOR 		: Mitesh Modi
// DATE CREATED : 10/06/2008
// DESCRIPTION 	: This class is a concrete implementation of ICyclePlanner
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerService/CyclePlanner.cs-arc  $
//
//   Rev 1.4   Aug 08 2008 12:05:46   mmodi
//Updated as part of workstream
//
//   Rev 1.3   Aug 06 2008 14:50:04   mmodi
//Updated as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Aug 01 2008 16:49:16   mmodi
//Updated to use actual web service
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Jul 18 2008 13:40:08   mmodi
//Updates as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 16:20:42   mmodi
//Initial revision.
//

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Services.Description;
using System.Web.Services.Protocols;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService;
using TransportDirect.Common.PropertyService.Properties;

using TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService;
using TransportDirect.UserPortal.CyclePlannerService.GradientProfilerWebService;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.CyclePlannerService
{
    /// <summary>
    /// This class is the concrete implementation of ICyclePlanner.
    /// Contains web services for CyclePlanner and GradientProfile
    /// </summary>
    public class CyclePlanner : ICyclePlanner, IDisposable
    {
        #region Private members
        
        private CyclePlannerWebService.CyclePlannerService webServiceCyclePlanner = null;
        private GradientProfilerWebService.GradientProfilerService webServiceGradientProfiler = null;

        #endregion

        #region constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CyclePlanner()
        {
            //initialise the services
            webServiceCyclePlanner = new TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService.CyclePlannerService();
            webServiceGradientProfiler = new GradientProfilerService();

            //get the corresponding webservice override urls from the database
            webServiceCyclePlanner.Url = TransportDirect.Common.PropertyService.Properties.Properties.Current["CyclePlanner.WebService.URL"];
            webServiceCyclePlanner.Timeout = Convert.ToInt32(TransportDirect.Common.PropertyService.Properties.Properties.Current["CyclePlanner.WebService.TimeoutMillisecs"]);

            webServiceGradientProfiler.Url = TransportDirect.Common.PropertyService.Properties.Properties.Current["GradientProfiler.WebService.URL"];
            webServiceGradientProfiler.Timeout = Convert.ToInt32(TransportDirect.Common.PropertyService.Properties.Properties.Current["GradientProfiler.WebService.TimeoutMillisecs"]);
        }
        #endregion

        #region ICyclePlanner methods

        /// <summary>
        /// Method makes the call to the Cycle planner web service
        /// </summary>
        public CyclePlannerResult CycleJourneyPlan(CyclePlannerRequest cycleJourneyRequest)
        {
            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    "CyclePlanner - Web service CycleJounreyPlan call started. RequestID = " + cycleJourneyRequest.requestID));
            }

            //declare the output object
            CyclePlannerResult result = null;

            DateTime callStartTime = DateTime.UtcNow;
            bool successful = false;

            try
            {
                // Call webservice
                result = webServiceCyclePlanner.JourneyPlan(cycleJourneyRequest);

                // Set succesful here if we want to note down that the call produced successful results
                successful = true;
            }
            catch (SoapException se)
            {
                LogSoapException("CycleJourneyPlan", se);
            }
            catch (WebException we)
            {
                LogWebException("CycleJourneyPlan", we);
            }
            finally
            {
                //log the callevent
                LogCallEvent("CycleJourneyPlan", callStartTime, successful);
            }

            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    "CyclePlanner - Web service CycleJourneyPlan call completed. RequestId = " + cycleJourneyRequest.requestID));
            }

            return (CyclePlannerResult)result;
        }

        /// <summary>
        /// Method makes the call to the Gradient profiler web service
        /// </summary>
        public GradientProfileResult GradientProfiler(GradientProfileRequest gradientProfileRequest)
        {
            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    "CyclePlanner - Web service GradientProfiler call started. RequestId = " + gradientProfileRequest.requestID));
            }

            GradientProfileResult result = null;
            DateTime callStartTime = DateTime.UtcNow;
            bool successful = false;

            try
            {
                // Call webservice
                result = webServiceGradientProfiler.GetGradientProfile(gradientProfileRequest);

                // Set the successful here if we want to note down the call produced successful results
                successful = true;
            }
            catch (SoapException se)
            {
                LogSoapException("GradientProfiler", se);
            }
            catch (WebException we)
            {
                LogWebException("GradientProfiler", we);
            }
            finally
            {
                LogCallEvent("GradientProfiler", callStartTime, successful);
            }

            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    "CyclePlanner - Web service GradientProfiler call completed. RequestId = " + gradientProfileRequest.requestID));
            }

            return (GradientProfileResult)result;
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
            string message = "CyclePlanner - A SOAP exception has occured when trying to call the Web service: "
                + webServiceMethod 
                + " Exception: "
                + se.Message;
            
            OperationalEvent oe = new OperationalEvent(TDEventCategory.CJP, TDTraceLevel.Error, message);
            Logger.Write(oe);
            
            //log an operational event and create new TDException using the soap exception 
            throw new TDException(message, true, TDExceptionIdentifier.CYSoapException, se);
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
                message = "CyclePlanner - A Timeout Web exception has occured when trying to call the Web service: "
                    + webServiceMethod
                    + " Exception: "
                    + we.Message;
            }
            else
            {
                message = "CyclePlanner - A Web exception has occured when trying to call the Web service: "
                    + webServiceMethod
                    + " Exception: "
                    + we.Message;
            }
            
            OperationalEvent oe = new OperationalEvent(TDEventCategory.CJP, TDTraceLevel.Error, message);
            Logger.Write(oe);

            //log an operational event and create new TDException using the web exception 
            throw new TDException(message, true, TDExceptionIdentifier.CYWebException, we);
        }

        /// <summary>
        /// Method that logs the web service call time, and whether it was successfule
        /// </summary>
        /// <param name="callStartTime"></param>
        /// <param name="successful"></param>
        private void LogCallEvent(string webServiceMethod, DateTime callStartTime, bool successful)
        {
            string message = "CyclePlanner - Web service call made to: " + webServiceMethod 
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
        ~CyclePlanner()
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
                if (webServiceCyclePlanner != null)
                {
                    webServiceCyclePlanner.Dispose();
                    webServiceCyclePlanner = null;
                }

                if (webServiceGradientProfiler != null)
                {
                    webServiceGradientProfiler.Dispose();
                    webServiceGradientProfiler = null;
                }
            }
        }

        #endregion
    }
}
