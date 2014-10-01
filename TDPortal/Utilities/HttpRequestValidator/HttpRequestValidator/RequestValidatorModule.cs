// *********************************************** 
// NAME         : RequestValidatorModule.cs
// AUTHOR       : Mitesh Modi
// DATE CREATED : 24/01/2011
// DESCRIPTION  : HTTPModule class to call a validator for requests
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/HttpRequestValidator/HttpRequestValidator/RequestValidatorModule.cs-arc  $
//
//   Rev 1.0   Feb 03 2011 10:14:54   mmodi
//Initial revision.
//
//   Rev 1.0   Jan 27 2011 16:24:20   mmodi
//Initial revision.
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using AO.HttpRequestValidatorCommon;
using Microsoft.Web.Administration;

namespace AO.HttpRequestValidator
{
    /// <summary>
    /// RequestValidatorModule class.
    /// </summary>
    public class RequestValidatorModule : IHttpModule
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
        /// if validators are enabled or not
        /// </summary>
        private static bool validatorSwitchConnections;
        private static bool validatorSwitchSession;
        private static bool validatorSwitchDomainRedirect;

        /// <summary>
        /// response page to redirect to if the application forces the request to end
        /// </summary>
        private static string responseErrorPage = string.Empty;

        /// <summary>
        /// response status code to apply if the application forces the request to end
        /// </summary>
        private static string responseErrorStatus = string.Empty;

        /// <summary>
        /// request file extensions to be checked by this validator
        /// </summary>
        private static List<string> fileExtensions = new List<string>();

        #endregion

        #region IHttpModule Members

        /// <summary>
        /// Initializes the module, and registers for application events.
        /// </summary>
        /// <param name="application">
        /// The System.Web.HttpApplication instance exposing application events.
        /// </param>
        public void Init(HttpApplication application)
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

                        // Validator switches
                        validatorSwitchConnections = (bool)section.GetAttribute(HttpRequestValidatorKeys.SwitchConnections).Value;
                        validatorSwitchSession = (bool)section.GetAttribute(HttpRequestValidatorKeys.SwitchSession).Value;
                        validatorSwitchDomainRedirect = (bool)section.GetAttribute(HttpRequestValidatorKeys.SwitchDomainRedirect).Value;
                        
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

                        string extensions = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Extensions).GetAttribute(HttpRequestValidatorKeys.URL_Extensions).Value;
                        if (!string.IsNullOrEmpty(extensions))
                        {
                            foreach (string extension in extensions.Split(','))
                            {
                                if (!fileExtensions.Contains(extension))
                                {
                                    fileExtensions.Add(extension.Trim().ToLower());
                                }
                            }
                        }

                        #region Remove existing flag files

                        // Clear any existing flag files as the app has reset
                        ConfigurationSection section2 = Microsoft.Web.Administration.WebConfigurationManager.GetSection(HttpRequestValidatorKeys.ConfigurationSection);
                        string flagFile = section2.GetChildElement(HttpRequestValidatorKeys.Element_MessageRedirector).GetAttribute(HttpRequestValidatorKeys.MessageRedirector_FileLocation).ToString();

                        if (flagFile != string.Empty)
                        {
                            try
                            {
                                File.Delete(flagFile);
                            }
                            catch { }
                        }

                        flagFile = section2.GetChildElement(HttpRequestValidatorKeys.Element_CoolDown).GetAttribute(HttpRequestValidatorKeys.CoolDown_FileLocation).ToString();

                        if (flagFile != string.Empty)
                        {
                            try
                            {
                                File.Delete(flagFile);
                            }
                            catch { }
                        }

                        #endregion

                        staticValuesSet = true;
                    }
                }
            }

            #endregion

            // register for events
            application.BeginRequest += new EventHandler(application_BeginRequest);
            application.EndRequest += new EventHandler(application_EndRequest);
        }
                
        /// <summary>
        /// Disposes of the resources (other than memory) used by the module.
        /// </summary>
        public void Dispose()
        {
        }

        #endregion

        #region Module Event Handlers

        /// <summary>
        /// Application BeginRequest event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void application_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            HttpContext context = app.Context;

            #region Message redirector

            RequestMessageRedirectorValidator rmrv = new RequestMessageRedirectorValidator();

            if (!rmrv.IsValid(context))
            {
                // redirect location already set
                context.Response.Status = responseErrorStatus;
                context.Response.End();
                return;
            }

            #endregion

            #region Cool down

            RequestCoolDownValidator rcdv = new RequestCoolDownValidator();

            if (!rcdv.IsValid(context))
            {
                // redirect location already set
                context.Response.Status = responseErrorStatus;
                context.Response.End();
                return;
            }

            #endregion

            #region Domain redirect

            if (validatorSwitchDomainRedirect)
            {
                // Do redirect (include querystring if appropriate)
                RequestDomainRedirectValidator rdrv = new RequestDomainRedirectValidator();

                if (!rdrv.IsValid(context))
                {
                    // redirect location already set
                    context.Response.Status = responseErrorStatus;
                    context.Response.End();
                    return;
                }
            }

            #endregion            

            // Perform other validations if turned on
            if ((validatorSwitchConnections) || (validatorSwitchSession) || (validatorSwitchDomainRedirect))
            {
                bool valid = true;

                // Allow through to the response error page
                if (!context.Request.RawUrl.ToLower().Contains(responseErrorPage))
                {
                    foreach (string extension in fileExtensions)
                    {
                        if (context.Request.RawUrl.Contains(extension))
                        {
                            // Validate requests
                            #region Request connection validation

                            if (validatorSwitchConnections)
                            {
                                RequestConnectionValidator rcv = new RequestConnectionValidator();

                                valid = rcv.IsValid(context);
                            }

                            #endregion

                            #region Request action validation

                            if (valid && validatorSwitchSession)
                            {
                                RequestActionValidator rav = new RequestActionValidator();
                               
                                valid = rav.IsValid(context);
                            }

                            #endregion

                            break;
                        }
                    }
                }

                // Terminate request if not valid
                if (!valid)
                {
                    context.Response.Status = responseErrorStatus;
                    context.Response.RedirectLocation = responseErrorPage;
                    context.Response.End();
                }
            }
        }

        /// <summary>
        /// Application EndRequest event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void application_EndRequest(object sender, EventArgs e)
        {
        }

        #endregion
    }
}
