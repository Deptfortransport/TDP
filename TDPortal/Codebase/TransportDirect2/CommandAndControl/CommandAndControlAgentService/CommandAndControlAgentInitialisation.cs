// *********************************************** 
// NAME             : CommandAndControlAgentInitialisation.cs      
// AUTHOR           : Rich Broddle
// DATE CREATED     : 11th April 2011
// DESCRIPTION  	: Implementation of initialisation logic
// ************************************************
// 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using TDP.Common;
using TDP.Common.ServiceDiscovery;
using TDP.Common.PropertyManager;
using System.Diagnostics;
using TDP.Common.EventLogging;
using Logger = System.Diagnostics.Trace;
using TDP.Common.DatabaseInfrastructure;

namespace TDP.UserPortal.CCAgent
{
    public class CommandAndControlAgentInitialisation : IServiceInitialisation
    {
    
        #region Public Methods
        /// <summary>
        /// Method to initialise Service Discovery cache with relevant services
        /// </summary>
        /// <param name="serviceCache">Service cache to initialise</param>
        public void Populate(Dictionary<string, IServiceFactory> serviceCache)
        {
            try
            {
                serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
            }
            catch (Exception ex)
            {
                string message = string.Format("Initialisation failed : {0}", ex.StackTrace);

                Trace.Write(
                  new OperationalEvent(
                      TDPEventCategory.Business,
                      TDPTraceLevel.Error,
                      message));

                throw new TDPException(message, true, TDPExceptionIdentifier.CCAgentInitialisationFailed);
            }


            #region Enable EventLogging
            // Enable Event Logging
            List<string> errors = new List<string>();
            try
            {

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

            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Info, "INITIALISATION Started CCAgentInitialisation"));
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Added to service cache - Properties and Event Logging"));
            // Add other services dependent on Properties and EventLogging
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: DataChangeNotification"));
            serviceCache.Add(ServiceDiscoveryKey.DataChangeNotification, new DataChangeNotificationFactory());
        }
        #endregion
    
    }
}
