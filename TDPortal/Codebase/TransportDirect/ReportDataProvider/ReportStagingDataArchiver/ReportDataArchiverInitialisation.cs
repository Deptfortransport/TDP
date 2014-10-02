// **************************************************************************** 
// NAME			: ReportDataArchiverInitialisation.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 24/09/2003 
// DESCRIPTION	: Implementation of the ReportDataArchiverInitialisation class.
// ****************************************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/ReportStagingDataArchiver/ReportDataArchiverInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:12   mturner
//Initial revision.
//
//   Rev 1.7   May 20 2005 13:23:26   NMoorhouse
//Post Del7 NUnit Updates - Automate Data Loading
//
//   Rev 1.6   Jan 09 2004 14:58:04   geaton
//Flush exception message to default log file.
//
//   Rev 1.5   Nov 19 2003 11:38:28   geaton
//Refactored.

using System;
using System.Collections;
using System.Diagnostics;
using System.Configuration;
using System.IO;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.ReportDataProvider.ReportStagingDataArchiver
{
	/// <summary>
	/// Initialisation class for archiver component.
	/// </summary>
	public class ReportDataArchiverInitialisation : IServiceInitialisation
	{
		/// <summary>
		/// Adds services and validates properties.
		/// </summary>
		/// <param name="serviceCache">Cache to add services.</param>
		/// <exception>
		/// Thrown if initialisation fails.
		/// </exception>
		public void Populate( Hashtable serviceCache )
		{			
			// Initialise .NET trace listener to record ONLY errors until TD TraceListener is intialised. 
			TextWriterTraceListener logTextListener = null;
			try
			{
                string logfilePath = ConfigurationManager.AppSettings["propertyservice.defaultlogfilepath"];
                Stream logFile = File.Create(logfilePath + "\\" + ConfigurationManager.AppSettings["propertyservice.applicationid"] + ".txt");
				logTextListener = new TextWriterTraceListener(logFile);
				Trace.Listeners.Add(logTextListener);
				Trace.WriteLine(Messages.Init_InitialisationStarted);
			}
			catch (Exception exception) // catch all in this situation.
			{
				throw new TDException(String.Format(Messages.Init_DotNETTraceListenerFailed, exception.Message), true, TDExceptionIdentifier.RDPStagingArchiverDefaultLoggerFailed);
			}


			// Add TD services to service cache that are needed to support TD Trace Listening.
			try
			{	
				serviceCache.Add(ServiceDiscoveryKey.Crypto,  new CryptoFactory());
				serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			}
			catch(Exception exception)
			{
				Trace.WriteLine(String.Format(Messages.Init_TDServiceAddFailed, exception.Message));
				logTextListener.Flush();
				logTextListener.Close();
				throw new TDException(String.Format(Messages.Init_TDServiceAddFailed, exception.Message), true, TDExceptionIdentifier.RDPStagingArchiverTDServiceAddFailed);
			}

			// Enable the Event Logging Service
			ArrayList loggingErrors = new ArrayList();
			IEventPublisher[] customPublishers = new IEventPublisher[0];
			try
			{				
				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, loggingErrors));
			}
			catch (TDException)
			{	
				// Create error message to log to default listener.
				Trace.WriteLine(String.Format(Messages.Init_TDTraceListenerFailed, "Reasons follow."));
				StringBuilder message = new StringBuilder(100);
				foreach (string error in loggingErrors)
					message.Append(error + ",");
				Trace.WriteLine(message.ToString());				
				
				throw new TDException(String.Format(Messages.Init_TDTraceListenerFailed, message.ToString()), true, TDExceptionIdentifier.RDPStagingArchiverTDTraceInitFailed);	
			}
			finally
			{
				// Remove other listeners.
				logTextListener.Flush();
				logTextListener.Close();
				Trace.Listeners.Remove( logTextListener );
				logTextListener = null;
				Trace.Listeners.Remove("System.Diagnostics.DefaultTraceListener");
			}

			// Validate components' specific properties.
			ReportStagingDataArchiverPropertyValidator validator = new ReportStagingDataArchiverPropertyValidator(Properties.Current);
			ArrayList archiverErrors = new ArrayList();
			validator.ValidateProperty(Keys.ReportDataStagingDB, archiverErrors);
			
			if (archiverErrors.Count > 0)
			{
				StringBuilder message = new StringBuilder(100);
				foreach (string error in archiverErrors)
					message.Append(error + ",");			

				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Init_BadProperties, message.ToString())));

				throw new TDException(String.Format(Messages.Init_BadProperties, message.ToString()), true, TDExceptionIdentifier.RDPStagingArchiverInvalidProperties);	
			}
		}
	}
}
