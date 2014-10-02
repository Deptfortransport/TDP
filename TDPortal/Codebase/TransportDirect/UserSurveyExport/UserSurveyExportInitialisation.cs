// *********************************************** 
// NAME                 : UserSurveyExportInitialisation.cs 
// AUTHOR               : Joe Morrissey
// DATE CREATED         : 19/10/2004 
// DESCRIPTION  : Service Initialisation class. 
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/UserSurveyExport/UserSurveyExportInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:50:54   mturner
//Initial revision.
//
//   Rev 1.2   Nov 04 2004 12:47:42   jmorrissey
//Change of database property name 
//
//   Rev 1.1   Nov 02 2004 16:27:36   jmorrissey
//Completed Nunit testing
//
//   Rev 1.0   Oct 20 2004 17:53:10   jmorrissey
//Initial revision.

using System;
using System.Net.Mail;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Configuration;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserSurveyExport
{
	/// <summary>
	/// Used by the TDServiceDiscovery class.
	/// </summary>
	public class UserSurveyExportInitialisation : IServiceInitialisation
	{
		/// <summary>
		/// Initialises TD services and related properties.
		/// </summary>
		/// <param name="serviceCache">Service cache.</param>
		public void Populate( Hashtable serviceCache )
		{				
			// Create cryptographic scheme
			try
			{
				serviceCache.Add(ServiceDiscoveryKey.Crypto,  new CryptoFactory());				
			}
			catch(Exception exception)
			{
				throw new TDException(String.Format("Crypto init failed.", exception.Message), false, 
					TDExceptionIdentifier.USECryptoInitFailed);
			}

			// Create Property Service.
			try
			{
				serviceCache.Add( ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory() );
			}
			catch(Exception exception)
			{
				throw new TDException(String.Format("Property service init failed.", exception.Message), 
					false, TDExceptionIdentifier.USEPropertyServiceInitFailed);
			}

			// Create TD Logging service.
			ArrayList errors = new ArrayList();	

			//add CustomEmailPublisher		
			IEventPublisher[] customPublishers = new IEventPublisher[1];	

			string sender = ExportMain.ReadProperty("UserSurvey.EMAIL.Sender");
			string smtpServer = ExportMain.ReadProperty("Logging.Publisher.Custom.EMAIL.SMTPServer");
			string directory = ExportMain.ReadProperty("Logging.Publisher.Custom.EMAIL.WorkingDir");

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
		}
	}
}

