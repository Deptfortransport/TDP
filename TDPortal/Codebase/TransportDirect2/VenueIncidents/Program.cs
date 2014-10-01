using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common;
using Logger = System.Diagnostics.Trace;
using TDP.Common.ServiceDiscovery;

namespace TDP.VenueIncidents
{
    class Program
    {
        public static int Main(string[] args)
        {
            int returnCode = 0;

            try
            {
                TDPServiceDiscovery.Init(new VenueIncidentsInitialisation());
                VenueIncidents target = new VenueIncidents();
                target.GenerateTravelNewsAlertFile();
            }
            catch (TDPException tdpEx)
            {
                                // Log error (cannot assume that listener has been initialised)
                if (!tdpEx.Logged)
                {
                    Logger.Write(string.Format(Messages.Loader_Failed, tdpEx.Message, tdpEx.Identifier));
                }
                
                Console.WriteLine(string.Format(Messages.Loader_Failed, tdpEx.Message, tdpEx.Identifier));

                returnCode = (int)tdpEx.Identifier;
            }

            return returnCode;
        }
    }
}
