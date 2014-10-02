// *********************************************** 
// NAME             : Global.asax      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 27 Nov 2013
// DESCRIPTION  	: Global.asax class for methods which occur at Application start and end
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.WebService.DepartureBoardWebService
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // Initialise TD Services required by Coordinate Convertor Service.
            TDServiceDiscovery.Init(new DepartureBoardsInitialisation());
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}