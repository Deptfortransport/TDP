// *********************************************** 
// NAME                 : WebLogReaderInitialisation
// AUTHOR               : Andy Lole
// DATE CREATED         : 18/08/2003 
// DESCRIPTION			: Initialisation class for WebLogReader applications
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/WebLogReader/WebLogReaderInitialisation.cs-arc  $
//
//   Rev 1.1   Apr 29 2008 16:37:56   mturner
//Fixes to allow the web log reader to process host names from IIS logs.  This is to resolve IR4904/USD2517876
//
//   Rev 1.0   Nov 08 2007 12:40:58   mturner
//Initial revision.
//
//   Rev 1.13   Nov 11 2004 17:48:20   passuied
//Part of changes to enable WebLogReaders to read from multiple folders.
//
//   Rev 1.12   Aug 03 2004 13:43:10   passuied
//changed database used import events from
//
//   Rev 1.11   Apr 19 2004 20:35:16   geaton
//IR785.
//
//   Rev 1.10   Jan 09 2004 14:57:52   geaton
//Flush exception message to default log file.
//
//   Rev 1.9   Dec 15 2003 17:30:32   geaton
//Added support for filtering out log entries based on client IP address/es.
//
//   Rev 1.8   Nov 17 2003 20:15:50   geaton
//Refactored.
//
//   Rev 1.7   Nov 14 2003 17:37:06   geaton
//Added crypto init and tidied up error handling.
//
//   Rev 1.6   Oct 13 2003 18:18:24   PScott
//added traces
//
//   Rev 1.5   Oct 09 2003 10:42:44   ALole
//Updated WebLogReaderMain not to give any textual output on completion/failure.
//Updated W3CWebLogReader to correctly handle non GMT time on local machine.
//
//   Rev 1.4   Oct 07 2003 16:05:00   ALole
//Updated  TDExceptionIdentifier references
//
//   Rev 1.3   Oct 07 2003 11:56:42   PScott
//Added enums to exceptions and adjusted date time for gmt/bst change
//
//   Rev 1.2   Sep 04 2003 12:06:02   ALole
//Added a loop to write all the errors raised to the trace log.
//
//   Rev 1.1   Sep 03 2003 16:31:30   ALole
//Changed the catch block to write to the .Net Trace rather than the TDTrace.
//Added a call to remove the .net DefaultTraceListener
//
//   Rev 1.0   Aug 28 2003 13:35:22   ALole
//Initial Revision


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
using TransportDirect.ReportDataProvider.DatabasePublishers;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Partners;

namespace TransportDirect.ReportDataProvider.WebLogReader
{
	/// <summary>
	/// Performs web log reader initialisation.
	/// </summary>
	public class WebLogReaderInitialisation : IServiceInitialisation
	{
		/// <summary>
		/// Populates sevice cache with services needed by web log reader component.
		/// Also validates properties needed by web log reader services.
		/// </summary>
		/// <param name="serviceCache">Cache to populate.</param>
		/// <exception cref="TDException">
		/// Thrown if errors occur when populating or if invalid properties found.
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
				throw new TDException(String.Format(Messages.Init_DotNETTraceListenerFailed, exception.Message), true, TDExceptionIdentifier.RDPWebLogReaderDefaultLoggerFailed);
			}


			// Add TD services to service cache that are needed to support TD Trace Listening.
			try
			{	
				serviceCache.Add(ServiceDiscoveryKey.Crypto,  new CryptoFactory());
				serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
                serviceCache.Add(ServiceDiscoveryKey.PartnerCatalogue, new PartnerCatalogueFactory());
			}
			catch(Exception exception)
			{
				Trace.WriteLine(String.Format(Messages.Init_TDServiceAddFailed, exception.Message));
				logTextListener.Flush();
				logTextListener.Close();
				throw new TDException(String.Format(Messages.Init_TDServiceAddFailed, exception.Message), true, TDExceptionIdentifier.RDPWebLogReaderTDServiceAddFailed);
			}

			// Enable the Event Logging Service
			ArrayList loggingErrors = new ArrayList();
			IEventPublisher[] customPublishers = new IEventPublisher[2];
			try
			{
				// allow potential configuration to publish directly to database rather than via MSMQ
				customPublishers[0] = new TDPCustomEventPublisher("TDPDB", SqlHelperDatabase.ReportStagingDB);
				customPublishers[1] = new OperationalEventPublisher("OPDB", SqlHelperDatabase.ReportStagingDB);

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
				
				throw new TDException(String.Format(Messages.Init_TDTraceListenerFailed, message.ToString()), true, TDExceptionIdentifier.RDPWebLogReaderTDTraceInitFailed);	
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

			// Validate web log reader specific properties.
			WebLogReaderPropertyValidator validator = new WebLogReaderPropertyValidator(Properties.Current);
			ArrayList readerErrors = new ArrayList();
			bool isWebLogFolders = validator.ValidateProperty(Keys.WebLogReaderWebLogFolders, readerErrors);
			if (isWebLogFolders)
			{
			
			validator.ValidateProperty(Keys.WebLogReaderArchiveDirectory, readerErrors);
			validator.ValidateProperty(Keys.WebLogReaderLogDirectory, readerErrors);

			}

			validator.ValidateProperty(Keys.WebLogReaderNonPageMinimumBytes, readerErrors);
			validator.ValidateProperty(Keys.WebLogReaderWebPageExtensions, readerErrors);
			validator.ValidateProperty(Keys.WebLogReaderClientIPExcludes, readerErrors);

			if (readerErrors.Count > 0)
			{
				StringBuilder message = new StringBuilder(100);
				foreach (string error in readerErrors)
					message.Append(error + ",");			

				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Init_ReaderProperties, message.ToString())));

				throw new TDException(String.Format(Messages.Init_ReaderProperties, message.ToString()), true, TDExceptionIdentifier.RDPWebLogReaderInvalidProperties);	
			}
			
		}
	}
}
