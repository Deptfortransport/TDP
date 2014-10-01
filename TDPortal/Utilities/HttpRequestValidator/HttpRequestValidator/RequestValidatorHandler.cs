// *********************************************** 
// NAME         : RequestValidatorHandler.cs
// AUTHOR       : Mitesh Modi
// DATE CREATED : 24/01/2011
// DESCRIPTION  : HTTPHandler class to call a validator for requests
//
//  WARNING - This HttpHandler class does not fully work, the code below seems not to function correctly.
//  
//          IHttpHandler handler = PageParser.GetCompiledPageInstance(context.Request.Path, context.Request.PhysicalPath, context);
//          handler.ProcessRequest(context);
//
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/HttpRequestValidator/HttpRequestValidator/RequestValidatorHandler.cs-arc  $
//
//   Rev 1.0   Feb 03 2011 10:14:54   mmodi
//Initial revision.
//
//   Rev 1.0   Jan 27 2011 16:24:20   mmodi
//Initial revision.
//

using System;
using System.Web;
using System.Web.UI;
using Microsoft.Web.Administration;
using AO.HttpRequestValidatorCommon;

namespace AO.HttpRequestValidator
{
    /// <summary>
    /// RequestValidatorHandler class
    /// </summary>
    public class RequestValidatorHandler : IHttpHandler
    {
        #region Private members

        /// <summary>
        /// This is used to record whether the CustomApplicationStart method has fired.
        /// </summary>
        private static bool staticValuesSet = false;
        
        /// <summary>
        /// A lock to prevent the customApplicationStartFired variable from being accessed by more
        /// than one thread at a time.
        /// </summary>
        private static readonly object staticValuesSetLock = new object();

        /// <summary>
        /// if validator is enabled or not
        /// </summary>
        private static bool validatorSwitch;

        /// <summary>
        /// response page to redirect to if the application forces the request to end
        /// </summary>
        private static string responseErrorPage = string.Empty;

        /// <summary>
        /// response status code to apply if the application forces the request to end
        /// </summary>
        private static string responseErrorStatus = string.Empty;

        #endregion

        #region IHttpHandler Members

        /// <summary>
        /// Read. IsReusable, indicates if the class can be reused by adding to the app pool
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }

        /// <summary>
        /// ProcessRequest method. Calls validator to perform request test. 
        /// Redirects to an error page if fails validation
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            #region Set static values

            //Here we set the static values if required.
            //Note that we use locks to ensure that this can
            //only be run by one thread at a time.
            if (!staticValuesSet)
            {
                lock (staticValuesSetLock)
                {
                    if (!staticValuesSet)
                    {
                        ConfigurationSection section = Microsoft.Web.Administration.WebConfigurationManager.GetSection(HttpRequestValidatorKeys.ConfigurationSection);

                        // Validator switch
                        validatorSwitch = (bool)section.GetAttribute(HttpRequestValidatorKeys.SwitchSession).Value;

                        // Set the response end error page
                        string errorPage = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Response).GetAttribute(HttpRequestValidatorKeys.ErrorPage_URL).Value;
                        if (!string.IsNullOrEmpty(errorPage))
                        {
                            responseErrorPage = errorPage.ToLower();
                        }

                        string errorStatus = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Response).GetAttribute(HttpRequestValidatorKeys.ResponseStatus).Value;
                        if (!string.IsNullOrEmpty(errorStatus))
                        {
                            responseErrorStatus = errorStatus;
                        }
                        
                        staticValuesSet = true;
                    }
                }
            }

            #endregion

            // Perform validation if turned on
            if (validatorSwitch)
            {
                string path = context.Request.Path.ToLower();
                if (!path.Contains(responseErrorPage))
                {
                    // Check for invalid requests
                    RequestActionValidator validator = new RequestActionValidator();
                    if (!validator.IsValid(context))
                    {
                        context.Response.Status = responseErrorStatus;
                        context.Response.RedirectLocation = responseErrorPage;
                        context.Response.End();
                    }
                }
            }

            // Ok to proceed to site
            IHttpHandler handler = PageParser.GetCompiledPageInstance(context.Request.Path, context.Request.PhysicalPath, context);
            handler.ProcessRequest(context);
        }

        #endregion
    }
}
