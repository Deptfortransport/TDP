// *********************************************** 
// NAME             : DataLoaderInitialisation.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 29 Feb 2012
// DESCRIPTION  	: Initialisation class for DataLoader application
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.ServiceDiscovery;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using TDP.Common;
using TDP.Common.PropertyManager;
using TDP.Common.EventLogging;

namespace TDP.DataLoader
{
    /// <summary>
    /// Initialisation class for DataLoader application
    /// </summary>
    public class DataLoaderInitialisation : IServiceInitialisation
    {
        #region Interface members

        /// <summary>
        /// Populates sevice cache with services needed by data loader component.
        /// </summary>
        /// <param name="serviceCache"></param>
        public void Populate(Dictionary<string, IServiceFactory> serviceCache)
        {
            string logPath = ConfigurationManager.AppSettings["DefaultLogFilepath"];
            string logFilePath = logPath + "\\" + ConfigurationManager.AppSettings["propertyservice.applicationid"] + ".txt";

            using (Stream logFile = File.Create(logFilePath))
            {
                #region Initialise .NET trace listener
                // Initialise .NET trace listener to record ONLY errors until TraceListener is intialised. 
                TextWriterTraceListener logTextListener = null;
                try
                {
                    logTextListener = new TextWriterTraceListener(logFile);
                    Trace.Listeners.Add(logTextListener);
                    Trace.WriteLine(Messages.Init_InitialisationStarted);
                }
                catch (Exception exception) // catch all in this situation.
                {
                    throw new TDPException(String.Format(Messages.Init_DotNETTraceListenerFailed, exception.Message), true,
                        TDPExceptionIdentifier.DLDataLoaderDefaultLoggerFailed);
                }
                #endregion

                // Add services to service cache that are needed to support TD Trace Listening.
                try
                {
                    serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(String.Format(Messages.Init_ServiceAddFailed, exception.Message));
                    logTextListener.Flush();
                    throw new TDPException(String.Format(Messages.Init_ServiceAddFailed, exception.Message), true,
                        TDPExceptionIdentifier.DLDataLoaderServiceAddFailed);
                }

                #region Enable EventLogging
                // Enable Event Logging
                List<string> errors = new List<string>();
                try
                {
                    // Create custom publisher
                    IEventPublisher[] customPublishers = new IEventPublisher[0];

                    Trace.Listeners.Add(new TDPTraceListener(Properties.Current, customPublishers, errors));
                }
                catch (TDPException tdpEx)
                {
                    #region Log and throw errors
                    // Create message string
                    StringBuilder message = new StringBuilder(100);
                    message.Append(tdpEx.Message); // prepend with existing exception message

                    // Append all messages returned by TraceListener constructor
                    foreach (string error in errors)
                    {
                        message.Append(error);
                        message.Append(" ");
                    }

                    // Log message using .NET default trace listener
                    Trace.WriteLine(message.ToString() + "ExceptionID:" + tdpEx.Identifier.ToString());

                    // rethrow exception - use the initial exception id as the id
                    throw new TDPException(
                        string.Format(Messages.Init_TraceListenerFailed,
                            tdpEx.Identifier.ToString("D"), message.ToString()),
                        false, TDPExceptionIdentifier.DLDataLoaderTraceInitFailed);

                    #endregion
                }
                finally
                {
                    // Remove other listeners.
                    logTextListener.Flush();
                    Trace.Listeners.Remove(logTextListener);
                    logTextListener = null;
                    Trace.Listeners.Remove("System.Diagnostics.DefaultTraceListener");
                }

                #endregion
            }

            // If reached here, then initialisation successful, so delete the temp default log file
            try
            {
                File.Delete(logFilePath);
            }
            catch
            {
                // Ignore any exceptions, this is only for helpful tidy up
            }

        }

        #endregion
    }
}
