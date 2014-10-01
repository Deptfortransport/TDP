// *********************************************** 
// NAME			: AvailabilityDataMaintenanceInitialisation.cs
// AUTHOR		: James Broome
// DATE CREATED	: 26/01/2005
// DESCRIPTION	: Implementation of the AvailabilityDataMaintenanceInitialisation class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AvailabilityDataMaintenance/AvailabilityDataMaintenanceInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:52   mturner
//Initial revision.
//
//   Rev 1.2   Mar 21 2005 10:53:32   jbroome
//Minor updates after code review
//
//   Rev 1.1   Feb 17 2005 14:47:18   jbroome
//Added Custom Email Publisher for use in Export Product Profiles
//Resolution for 1923: DEV Code Review : Availability Estimator
//
//   Rev 1.0   Feb 08 2005 10:38:16   jbroome
//Initial revision.

using System;
using System.Net.Mail;
using System.Text;
using System.Collections;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.AvailabilityDataMaintenance
{
	/// <summary>
	/// Class initialises necessary services for application to run
	/// Inherits from IServiceInitialisation
	/// </summary>
	public class AvailabilityDataMaintenanceInitialisation : IServiceInitialisation
	{
		/// <summary>
		/// Initialises TD services and related properties.
		/// </summary>
		/// <param name="serviceCache">Service cache.</param>
		public void Populate(Hashtable serviceCache)
		{
		
			// Initialise .NET trace listener to record ONLY errors until TD TraceListener is intialised. 
			TextWriterTraceListener logTextListener = null;
			try
			{
                string logfilePath = ConfigurationManager.AppSettings["DefaultLogFilepath"];
				Stream logFile = File.Create(logfilePath);
				logTextListener = new TextWriterTraceListener(logFile);
				Trace.Listeners.Add(logTextListener);
				Trace.WriteLine(Messages.Init_InitialisationStarted);
			}
			catch (Exception exception) // catch all in this situation.
			{
				throw new TDException(String.Format(CultureInfo.CurrentCulture, Messages.Init_DotNETTraceListenerFailed, exception.Message), true, TDExceptionIdentifier.AEDefaultLoggerFailed);
			}

			// Add TD services to service cache that are needed to support TD Trace Listening.
			try
			{	
				serviceCache.Add(ServiceDiscoveryKey.Crypto,  new CryptoFactory());
				serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			}
			catch(Exception exception)
			{
				Trace.WriteLine(String.Format(CultureInfo.CurrentCulture, Messages.Init_TDServiceAddFailed, exception.Message));
				logTextListener.Flush();
				logTextListener.Close();
				throw new TDException(String.Format(CultureInfo.CurrentCulture, Messages.Init_TDServiceAddFailed, exception.Message), true, TDExceptionIdentifier.AETDServiceAddFailed);
			}

			// Create TD Logging service.
			ArrayList errors = new ArrayList();	

			//add CustomEmailPublisher		
			IEventPublisher[] customPublishers = new IEventPublisher[1];	

			string sender = Properties.Current["AvailabilityDataMaintenance.ProfilesExport.EMAIL.Sender"];
			string smtpServer = Properties.Current["Logging.Publisher.Custom.EMAIL.SMTPServer"];
			string directory = Properties.Current["Logging.Publisher.Custom.EMAIL.WorkingDir"];

			customPublishers[0] = 
				new CustomEmailPublisher("EMAIL",sender,MailPriority.Normal,smtpServer,directory,errors);

			try
			{		
				Trace.Listeners.Add( new TDTraceListener(Properties.Current, customPublishers, errors ));								
				Trace.Listeners.Remove("System.Diagnostics.DefaultTraceListener");
			}
			catch (TDException tdException)
			{	
				//throw exception
				StringBuilder message = new StringBuilder(100);
				foreach (string error in errors)
					message.Append(error + ",");			
				
				throw new TDException("Initialisation of TD Trace Listener failed" + 
					tdException.Identifier.ToString("D") + message.ToString(),false,
					TDExceptionIdentifier.USETraceListenerInitFailed);	
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
		}
	}
}
