// *********************************************** 
// NAME             : EventReceiverInitialisation.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 18 Apr 2011
// DESCRIPTION  	: EventReceiver Initialisation class
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TDP.Common;
using TDP.Common.DatabaseInfrastructure;
using TDP.Common.EventLogging;
using TDP.Common.PropertyManager;
using TDP.Common.ServiceDiscovery;
using TDP.Reporting.EventPublishers;

namespace TDP.Reporting.EventReceiver
{
    /// <summary>
    /// EventReceiver Initialisation class
    /// </summary>
    public class EventReceiverInitialisation : IServiceInitialisation
    {
        #region Interface members

        /// <summary>
        /// IServiceInitialisation Populate method
        /// </summary>
        /// <param name="serviceCache"></param>
        public void Populate(Dictionary<string, IServiceFactory> serviceCache)
        {
            // Enable PropertyService
            try
            {
                serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
            }
            catch (Exception exception)
            {
                throw new TDPException(String.Format(Messages.Init_PropertyServiceFailed, exception.Message), false, TDPExceptionIdentifier.RDPEventReceiverInitFailed);
            }

            #region Enable EventLogging
            // Enable Event Logging
            List<string> errors = new List<string>();
            try
            {
                // Create custom publisher
                IEventPublisher[] customPublishers = new IEventPublisher[3];

                customPublishers[0] = new TDPCustomEventPublisher("TDPDB", SqlHelperDatabase.ReportStagingDB);
                customPublishers[1] = new TDPOperationalEventPublisher("OPDB", SqlHelperDatabase.ReportStagingDB);
                customPublishers[2] = new CJPCustomEventPublisher("CJPDB", SqlHelperDatabase.ReportStagingDB);
                
                // Create and add TDPTraceListener instance to the listener collection	
                Trace.Listeners.Add(new TDPTraceListener(Properties.Current, customPublishers, errors));
                Trace.Listeners.Remove("System.Diagnostics.DefaultTraceListener");
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
                throw new TDPException(string.Format(Messages.Init_TraceListenerFailed, tdpEx.Identifier.ToString("D"), message.ToString()), false, TDPExceptionIdentifier.RDPEventReceiverInitFailed);	

                #endregion
            }
            #endregion

            // Validate Properties which are required by services.
            List<string> propertyErrors = new List<string>();
            EventReceiverPropertyValidator validator = new EventReceiverPropertyValidator(Properties.Current);
            validator.ValidateProperty(Keys.ReceiverQueue, propertyErrors);
            if (propertyErrors.Count != 0)
            {
                StringBuilder message = new StringBuilder(100);
                foreach (string error in propertyErrors)
                    message.Append(error + ",");

                throw new TDPException(String.Format(Messages.Init_InvalidPropertyKeys, message.ToString()), true, TDPExceptionIdentifier.RDPEventReceiverInitFailed);
            }
        }

        #endregion
    }
}
