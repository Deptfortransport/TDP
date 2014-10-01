// *********************************************** 
// NAME             : LocationServiceInitialisation.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 02 Apr 2011
// DESCRIPTION  	: LocationServiceInitialisation initialisation class
// ************************************************
// 

using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TDP.Common;
using TDP.Common.EventLogging;
using TDP.Common.PropertyManager;
using TDP.Common.ServiceDiscovery;

namespace TDP.TestProject.Common.LocationService
{
    /// <summary>
    /// LocationServiceInitialisation initialisation class. 
    /// This does not add the LocationService to the cache
    /// </summary>
    public class LocationServiceInitialisation : IServiceInitialisation
    {
        public void Populate(Dictionary<string, IServiceFactory> serviceCache)
        {
            serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

            #region Enable EventLogging
            // Enable Event Logging
            List<string> errors = new List<string>();
            try
            {
                // No custom publishers
                IEventPublisher[] customPublishers = new IEventPublisher[0];

                // Create and add TDPTraceListener instance to the listener collection	
                Trace.Listeners.Add(new TDPTraceListener(Properties.Current, customPublishers, errors));
            }
            catch (TDPException tdpEx)
            {
                #region Log and throw errors
                // Create message string
                StringBuilder message = new StringBuilder(100);
                message.Append(tdpEx.Message); // prepend with existing exception message

                // Append all messages returned by TDTraceListener constructor
                foreach (string error in errors)
                {
                    message.Append(error);
                    message.Append(" ");
                }

                // Log message using .NET default trace listener
                Trace.WriteLine(message.ToString() + "ExceptionID:" + tdpEx.Identifier.ToString());

                // rethrow exception - use the initial exception id as the id
                throw new TDPException(message.ToString(), tdpEx, false, tdpEx.Identifier);

                #endregion
            }
            #endregion

        }
    }
}
