// *********************************************** 
// NAME                 : ReportDataImporterInitialisation
// AUTHOR               : Andy Lole
// DATE CREATED         : 12/09/2003 
// DESCRIPTION			: Initialisation class for ReportDataImporter applications
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/ReportDataImporter/ReportDataImporterInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:02   mturner
//Initial revision.
//
//   Rev 1.7   Jan 09 2004 14:58:14   geaton
//Flush exception message to default log file.
//
//   Rev 1.6   Nov 26 2003 13:56:08   geaton
//Removed need to use separate connection string (stored as property) for initialising link to Reporting database. - reuse string stored in another property.
//
//   Rev 1.5   Nov 23 2003 14:44:24   geaton
//Added timeout support.
//
//   Rev 1.4   Nov 21 2003 09:29:52   geaton
//Updated exception message.
//
//   Rev 1.3   Nov 20 2003 22:44:44   geaton
//Added extra exception handling and refactored.
//
//   Rev 1.2   Nov 18 2003 21:27:28   geaton
//Refactored.
//
//   Rev 1.1   Oct 07 2003 19:54:30   ALole
//Updated to Use the correct Stored Procedures to copy between ReportDataStaging DB and ReportData DB.
//To Test it must be possible to connect to another machine with acces to the ReportData DB.
//
//   Rev 1.0   Sep 24 2003 15:28:46   ALole
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


namespace TransportDirect.ReportDataProvider.ReportDataImporter
{
	/// <summary>
	/// Summary description for Initialisations.
	/// </summary>
	public class ReportDataImporterInitialisation : IServiceInitialisation
	{
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
				throw new TDException(String.Format(Messages.Init_DotNETTraceListenerFailed, exception.Message), true, TDExceptionIdentifier.RDPDataImporterDefaultLoggerFailed);
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
				throw new TDException(String.Format(Messages.Init_TDServiceAddFailed, exception.Message), true, TDExceptionIdentifier.RDPDataImporterTDServiceAddFailed);
			}

			// Enable the Event Logging Service
			ArrayList loggingErrors = new ArrayList();
			IEventPublisher[] customPublishers = new IEventPublisher[0];
			try
			{				
				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, loggingErrors));
			}
			catch (TDException tdException)
			{	
				// Create error message to log to default listener.
				Trace.WriteLine(String.Format(Messages.Init_TDTraceListenerFailed, "[" + tdException.Message + "]"));
				StringBuilder message = new StringBuilder(100);
				foreach (string error in loggingErrors)
					message.Append(error + ",");
				Trace.WriteLine(message.ToString());				
				
				throw new TDException(String.Format(Messages.Init_TDTraceListenerFailed, message.ToString()), true, TDExceptionIdentifier.RDPDataImporterTDTraceInitFailed);	
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

			// Validate report data importer specific properties.
			ReportDataImporterPropertyValidator validator = new ReportDataImporterPropertyValidator(Properties.Current);
			ArrayList importerErrors = new ArrayList();
			validator.ValidateProperty(Keys.CJPWebRequestWindow, importerErrors);
			validator.ValidateProperty(Keys.ReportDatabase, importerErrors);
			validator.ValidateProperty(Keys.ImportTimeout, importerErrors);

			if (importerErrors.Count > 0)
			{
				StringBuilder message = new StringBuilder(100);
				foreach (string error in importerErrors)
					message.Append(error + ",");			

				Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, String.Format(Messages.Init_BadProperties, message.ToString())));

				throw new TDException(String.Format(Messages.Init_BadProperties, message.ToString()), true, TDExceptionIdentifier.RDPDataImporterInvalidProperties);	
			}
		}
	}
}
