// *********************************************** 
// NAME                 : Global.asax
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 28/05/2009
// DESCRIPTION  		: Global.asax class for methods which occur at Application start and end
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/CoordinateConvertorService/Global.asax.cs-arc  $
//
//   Rev 1.0   Jun 03 2009 11:34:14   mmodi
//Initial revision.
//Resolution for 5293: Cycle Planner - Coordinate convertor (Quest InGrid) to be added as a Web Service

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.WebService.CoordinateConvertorService
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Initialise TD Services required by Coordinate Convertor Service.
            TDServiceDiscovery.Init(new CoordinateConvertorInitialisation());
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}