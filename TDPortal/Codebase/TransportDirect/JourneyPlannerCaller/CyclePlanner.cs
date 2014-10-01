// ***********************************************
// NAME 		: CyclePlanner.cs
// AUTHOR 		: Mitesh Modi
// DATE CREATED : 19/04/2010
// DESCRIPTION 	: This class is contains methods to call the CyclePlanner web service
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerCaller/CyclePlanner.cs-arc  $
//
//   Rev 1.0   Apr 20 2010 16:39:18   mmodi
//Initial revision.
//Resolution for 5515: Utility - Application to send request directly to CJP
//
//   Rev 1.0   Apr 20 2010 15:41:24   mmodi
//Initial revision.
//Resolution for 5515: Utility - Application to send request directly to CJP

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Services.Description;
using System.Web.Services.Protocols;

using JourneyPlannerCaller.CyclePlannerWebService;

namespace JourneyPlannerCaller
{
    /// <summary>
    /// This class contains web services for CyclePlanner
    /// </summary>
    public class CyclePlanner : IDisposable
    {
        #region Private members
        
        private CyclePlannerWebService.CyclePlannerService webServiceCyclePlanner = null;
        
        #endregion

        #region constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CyclePlanner(int timeoutMilliseconds)
        {
            //initialise the services
            webServiceCyclePlanner = new CyclePlannerWebService.CyclePlannerService();
            
            //set the timeout
            webServiceCyclePlanner.Timeout = timeoutMilliseconds;
        }
        #endregion

        #region CyclePlanner methods

        /// <summary>
        /// Method makes the call to the Cycle planner web service
        /// </summary>
        public CyclePlannerResult CycleJourneyPlan(CyclePlannerRequest cycleJourneyRequest)
        {
            FileLogger.LogMessage(string.Format(
                "CyclePlanner web service call started for RequestID[{0}]",
                cycleJourneyRequest.requestID));
            
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

            FileLogger.LogMessage(string.Format(
                "CyclePlanner web service call completed for RequestId[{0}]", 
                cycleJourneyRequest.requestID));

            return (CyclePlannerResult)result;
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
            string message = string.Format(
                "A SOAP exception has occured when trying to call the Web service: [{0}] Exception: [{1}]",
                webServiceMethod,
                se.Message);

            FileLogger.LogMessage(message);
            
            //Create new Exception using the soap exception 
            throw new Exception(message, se);
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
                message = string.Format(
                    "A Timeout Web exception has occured when trying to call the Web service:[{0}] Exception:[{1}]",
                    webServiceMethod,
                    we.Message);
            }
            else
            {
                message = string.Format(
                    "A Web exception has occured when trying to call the Web service:[{0}] Exception:[{1}]",
                    webServiceMethod,
                    we.Message);
            }
            
            FileLogger.LogMessage(message);

            //Create new Exception using the web exception 
            throw new Exception(message, we);
        }

        /// <summary>
        /// Method that logs the web service call time, and whether it was successfule
        /// </summary>
        /// <param name="callStartTime"></param>
        /// <param name="successful"></param>
        private void LogCallEvent(string webServiceMethod, DateTime callStartTime, bool successful)
        {
            string message = string.Format(
                "CyclePlanner - Web service call made to:[{0}] at startTime:[{1}] successful:[{2}]",
                webServiceMethod,
                callStartTime,
                successful);

            FileLogger.LogMessage(message);
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
            }
        }

        #endregion
    }
}
