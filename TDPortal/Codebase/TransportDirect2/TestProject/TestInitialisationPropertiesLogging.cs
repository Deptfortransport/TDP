// *********************************************** 
// NAME             : TestInitialisationPropertiesLogging.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 09 Sep 2013
// DESCRIPTION  	: Initialisation for Test projects with only Properties and Logging
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.ServiceDiscovery;
using TDP.Common.PropertyManager;
using TDP.Common.EventLogging;
using TDP.Reporting.EventPublishers;
using System.Diagnostics;
using TDP.Common;
using Logger = System.Diagnostics.Trace;
using TDP.Common.DatabaseInfrastructure;

namespace TDP.TestProject
{
    /// <summary>
    /// Initialisation for Test projects with only Properties and Logging
    /// </summary>
    class TestInitialisationPropertiesLogging : IServiceInitialisation
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
                IEventPublisher[] customPublishers = new IEventPublisher[2];

                // Create custom database publishers which will be used to publish 
                // custom events Note: id passed in constructors
                // must match those defined in the properties.
                customPublishers[0] = new TDPCustomEventPublisher("TDPDB", SqlHelperDatabase.ReportStagingDB);
                customPublishers[1] = new TDPOperationalEventPublisher("OPDB", SqlHelperDatabase.ReportStagingDB);

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
                        
            try
            {
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "TestInitialisationPropertiesLogging completed"));
            }
            catch
            {
                // Ignore
            }
        }
    }
}
