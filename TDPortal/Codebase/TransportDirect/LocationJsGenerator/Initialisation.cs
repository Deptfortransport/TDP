// ************************************************ 
// NAME                 : Initialisation.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 06/08/2012
// DESCRIPTION          : Service Discovery cache initialisation.
// ************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationJsGenerator/Initialisation.cs-arc  $
//
//   Rev 1.0   Aug 28 2012 10:35:32   mmodi
//Initial revision.
//Resolution for 5832: CCN Gaz

using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.LocationJsGenerator
{
    /// <summary>
    /// Initialisation class for service discovery
    /// Initialises services required for LocationJsGenerator
    /// </summary>
    class Initialisation : IServiceInitialisation
    {
        #region Public Methods

        /// <summary>
        /// Method to initialise Service Discovery cache with relevant services
        /// </summary>
        /// <param name="serviceCache">Service cache to initialise</param>
        public void Populate(System.Collections.Hashtable serviceCache)
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
                    Trace.WriteLine("Initialisation started");
                }
                catch (Exception exception) // catch all in this situation.
                {
                    throw new TDException(String.Format("Failed to initialise a default .NET trace listener. Message:[{0}].", exception.Message),
                        true, TDExceptionIdentifier.LJSGenInitialisationFailed);
                }
                #endregion

                try
                {
                    serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
                }
                catch (Exception ex)
                {
                    string message = string.Format("Initialisation failed: {0}", ex.StackTrace);

                    Trace.WriteLine(
                      new OperationalEvent(
                          TDEventCategory.Business,
                          TDTraceLevel.Error,
                          message));

                    logTextListener.Flush();

                    throw new TDException(message, true, TDExceptionIdentifier.LJSGenInitialisationFailed);
                }

                #region Enable EventLogging

                ArrayList errors = new ArrayList();
                try
                {
                    // Create custom publisher
                    IEventPublisher[] customPublishers = new IEventPublisher[0];

                    Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
                }
                catch (TDException tdEx)
                {
                    #region Log and throw errors
                    // Create message string
                    StringBuilder message = new StringBuilder(100);
                    message.Append(tdEx.Message); // prepend with existing exception message

                    // Append all messages returned by TraceListener constructor
                    foreach (string error in errors)
                    {
                        message.Append(error);
                        message.Append(" ");
                    }

                    // Log message using .NET default trace listener
                    Trace.WriteLine(message.ToString() + "ExceptionID:" + tdEx.Identifier.ToString());

                    // Rethrow exception - use the initial exception id as the id
                    throw new TDException(string.Format("Failed to initialise the Trace Listener class: {0} Message: {1}", tdEx.Identifier.ToString("D"), message.ToString()),
                        false, TDExceptionIdentifier.LJSGenInitialisationFailed);

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
