// *********************************************** 
// NAME                 : Program.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Main entry point to the service
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/SiteStatusLoaderService/Program.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:37:14   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System.Configuration;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace AO.SiteStatusLoaderService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            string serviceName = ConfigurationManager.AppSettings["ServiceName"];

            ServiceBase[] ServicesToRun;

            // More than one user Service may run within the same process. To add
            // another service to this process, change the following line to
            // create a second service object. For example,
            //
            //   ServicesToRun = new ServiceBase[] {new Service1(), new MySecondUserService()};
            //
            ServicesToRun = new ServiceBase[] { new SiteStatusLoaderService(serviceName) };

            ServiceBase.Run(ServicesToRun);
        }
    }
}