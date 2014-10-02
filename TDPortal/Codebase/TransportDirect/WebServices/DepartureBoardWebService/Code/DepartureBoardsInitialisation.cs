// *********************************************** 
// NAME             : DepartureBoardsInitialisation.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 27 Nov 2013
// DESCRIPTION  	: Initialisation class for DepartureBoards web service
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Web;
using TransportDirect.Common.ServiceDiscovery;
using System.Collections;
using System.Diagnostics;
using System.IO;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using System.Text;
using System.Configuration;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.WebService.DepartureBoardWebService
{
    /// <summary>
    /// Used by the TDServiceDiscovery class to initialise TD services for this web service.
    /// </summary>
    public class DepartureBoardsInitialisation : IServiceInitialisation
    {
        /// <summary>
        /// Sets up TD Services.
        /// </summary>
        /// <param name="serviceCache">Service cache to add services to.</param>
        /// <exception cref="TDException">
        /// One or more services fail to initialise.
        /// </exception>
        public void Populate(Hashtable serviceCache)
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
                    Trace.WriteLine("Initialisation of Departure Board Web Service started.");
                }
                catch (Exception exception) // catch all in this situation.
                {
                    throw new TDException(String.Format("Failed to initialise a default .NET trace listener. Error:[{0}].", exception.Message),
                        true, TDExceptionIdentifier.DBWSInitialisationFailed);
                }
                #endregion

                try
                {
                    serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
                }
                catch (Exception ex)
                {
                    string message = string.Format("Failed to add a TD service to the cache: [{0}]. StackTrace: {1}", ex.Message, ex.StackTrace);

                    Trace.WriteLine(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, message));

                    logTextListener.Flush();

                    throw new TDException(message, true, TDExceptionIdentifier.DBWSInitialisationFailed);
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
                        false, TDExceptionIdentifier.DBWSInitialisationFailed);

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

            if (TDTraceSwitch.TraceVerbose)
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                    "Initialisation of Departure Board Web Service completed successfully."));
        }
    }
}