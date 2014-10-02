// ************************************************************ 
// NAME                 : UserUpdateUtilityInitialisation
// AUTHOR               : Jonathan George
// DATE CREATED         : 18/08/2003 
// DESCRIPTION			: Initialisation class for the UserTypeUtility
// ************************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/UserTypeUtility/UserTypeUtilityInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:51:04   mturner
//Initial revision.
//
//   Rev 1.1   Jul 06 2004 15:46:00   jgeorge
//Updated commenting

using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.SessionManager.UserTypeUtility
{
	/// <summary>
	/// Summary description for UserUpdateUtilityInitialisation.
	/// </summary>
	public class UserUpdateUtilityInitialisation : IServiceInitialisation
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
                Stream logFile = File.Create(logfilePath + "\\" + ConfigurationManager.AppSettings["propertyservice.applicationid"]);
				logTextListener = new TextWriterTraceListener(logFile);
				Trace.Listeners.Add(logTextListener);
				Trace.WriteLine(Messages.Init_InitialisationStarted);
			}
			catch (Exception exception) // catch all in this situation.
			{
				throw new TDException(String.Format(Messages.Init_DotNETTraceListenerFailed, exception.Message), true, TDExceptionIdentifier.SMUserTypeUtilityDefaultLoggerFailed);
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
				throw new TDException(String.Format(Messages.Init_TDServiceAddFailed, exception.Message), true, TDExceptionIdentifier.SMUserTypeUtilityTDServiceAddFailed);
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
				
				throw new TDException(String.Format(Messages.Init_TDTraceListenerFailed, message.ToString()), true, TDExceptionIdentifier.SMUserTypeUtilityTDTraceInitFailed);	
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
