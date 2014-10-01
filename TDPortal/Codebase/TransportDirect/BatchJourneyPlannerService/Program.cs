// *********************************************** 
// NAME                 : Program.cs 
// AUTHOR               : David Lane 
// DATE CREATED         : 28/02/2012 
// DESCRIPTION			: Runs the service.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/BatchJourneyPlannerService/Program.cs-arc  $
//
//   Rev 1.2   Feb 28 2012 15:52:30   DLane
//Batch updates and code commentry
//Resolution for 5787: Batch Journey Planner
//

using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace BatchJourneyPlannerService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new BatchJourneyPlannerService() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
