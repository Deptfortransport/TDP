// *********************************************** 
// NAME                 : CoordinateConvertorInitialisation.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 28/05/2009
// DESCRIPTION  		: Initialisation class for CoordinateConvertor web service
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/CoordinateConvertorService/CoordinateConvertorInitialisation.cs-arc  $
//
//   Rev 1.0   Jun 03 2009 11:34:12   mmodi
//Initial revision.
//Resolution for 5293: Cycle Planner - Coordinate convertor (Quest InGrid) to be added as a Web Service

using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;


namespace TransportDirect.WebService.CoordinateConvertorService
{
    /// <summary>
    /// Used by the TDServiceDiscovery class to initialise TD services for this web service.
    /// </summary>
    public class CoordinateConvertorInitialisation : IServiceInitialisation
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
            // Initialise .NET trace listener to record ONLY errors until TD TraceListener is intialised. 
            TextWriterTraceListener logTextListener = null;
            try
            {
                string logfilePath = ConfigurationManager.AppSettings["DefaultLogFilepath"];
                Stream logFile = File.Create(logfilePath + "\\" + ConfigurationManager.AppSettings["propertyservice.applicationid"] + ".txt");
                logTextListener = new TextWriterTraceListener(logFile);
                Trace.Listeners.Add(logTextListener);
                Trace.WriteLine(Messages.Init_InitialisationStarted);
            }
            catch (Exception exception) // catch all in this situation.
            {
                throw new TDException(String.Format(Messages.Init_DotNETTraceListenerFailed, exception.Message), false, TDExceptionIdentifier.CCServiceTDTraceInitFailed);
            }

            // Add TD services to service cache that are needed to support TD Trace Listening.
            try
            {
                serviceCache.Add(ServiceDiscoveryKey.Crypto, new CryptoFactory());
                serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
            }
            catch (Exception exception)
            {
                Trace.WriteLine(String.Format(Messages.Init_TDServiceAddFailed, exception.Message));
                logTextListener.Flush();
                logTextListener.Close();
                throw new TDException(String.Format(Messages.Init_TDServiceAddFailed, exception.Message), true, TDExceptionIdentifier.CCServiceTDServiceAddFailed);
            }

            // Create TD Logging service.
            ArrayList errors = new ArrayList();
            IEventPublisher[] customPublishers = new IEventPublisher[0];
            try
            {
                Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
            }
            catch (TDException)
            {
                // Create error message to log to default listener.
                Trace.WriteLine(String.Format(Messages.Init_TDTraceListenerFailed, "Reasons follow."));
                StringBuilder message = new StringBuilder(100);
                foreach (string error in errors)
                    message.Append(error + ",");
                Trace.WriteLine(message.ToString());

                throw new TDException(String.Format(Messages.Init_TDTraceListenerFailed, message.ToString()), true, TDExceptionIdentifier.CCServiceTDTraceInitFailed);
            }
            finally
            {
                // Remove other listeners.
                logTextListener.Flush();
                logTextListener.Close();
                Trace.Listeners.Remove(logTextListener);
                logTextListener = null;
                Trace.Listeners.Remove("System.Diagnostics.DefaultTraceListener");
            }
            
            if (TDTraceSwitch.TraceVerbose)
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, Messages.Init_Completed));
        }
    }
}
