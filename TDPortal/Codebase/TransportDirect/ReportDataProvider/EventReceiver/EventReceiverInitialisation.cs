// *********************************************** 
// NAME                 : EventReceiverInitialisation.cs 
// AUTHOR               : Jatinder S. Toor
// DATE CREATED         : 18/08/2003 
// DESCRIPTION  : Event receiver initialisation class. 
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/EventReceiver/EventReceiverInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:34   mturner
//Initial revision.
//
//   Rev 1.19   Aug 03 2004 13:42:38   passuied
//changed database used to publish events
//Resolution for 1275: Move the ReportStagingDatabase to another machine - DEL6.0
//
//   Rev 1.18   Nov 14 2003 11:24:38   geaton
//Improved error message.
//
//   Rev 1.17   Nov 13 2003 21:22:06   geaton
//Configure db publishers to use default db - previously they published to a separate db called reporting staging.
//
//   Rev 1.16   Nov 07 2003 08:56:46   geaton
//Added crypto initialisation.
//
//   Rev 1.15   Nov 06 2003 19:54:18   geaton
//Removed redundant key.
//
//   Rev 1.14   Oct 10 2003 15:22:46   geaton
//Updated error handling and validation.
//
//   Rev 1.13   Oct 10 2003 08:33:34   geaton
//Removed reference to data gatewat project
//
//   Rev 1.12   Oct 09 2003 20:04:36   geaton
//Updated trace messages.
//
//   Rev 1.11   Oct 09 2003 17:09:36   PScott
//Updated trace output
//
//   Rev 1.10   Oct 09 2003 12:33:38   geaton
//Tidied up error handling and added verbose messages to assist in debugging.
//
//   Rev 1.9   Oct 09 2003 09:34:04   pscott
//.
//
//   Rev 1.8   Oct 08 2003 14:39:58   JTOOR
//Fixed problem with errors not being written to log, because of exception.
//
//   Rev 1.7   Oct 08 2003 14:02:58   pscott
//.
//
//   Rev 1.6   Oct 08 2003 12:05:18   JTOOR
//Error handling to write to log file.
//
//   Rev 1.5   Oct 08 2003 09:45:48   PScott
//.
//
//   Rev 1.4   Oct 08 2003 09:34:18   JTOOR
//CustomEventPublisher inserted.
//
//   Rev 1.3   Oct 08 2003 09:21:36   JTOOR
//Logging support implemented.
//
//   Rev 1.2   Oct 08 2003 08:52:24   PScott
//added custom publiser lines
//
//   Rev 1.1   Sep 05 2003 09:49:28   jtoor
//Changes made to comply with Code Review.
//
//   Rev 1.0   Aug 22 2003 11:49:34   jtoor
//Initial Revision

using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.ReportDataProvider.DatabasePublishers;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.ReportDataProvider.TDPCustomEvents;

namespace TransportDirect.ReportDataProvider.EventReceiver
{
	/// <summary>
	/// Used by the TDServiceDiscovery class.
	/// </summary>
	public class EventReceiverInitialisation : IServiceInitialisation
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
				serviceCache.Add( ServiceDiscoveryKey.Crypto,  new CryptoFactory() );
			}
			catch(Exception exception)
			{
				throw new TDException(String.Format(Messages.Init_CryptographicServiceFailed, exception.Message), false, TDExceptionIdentifier.RDPEventReceiverInitFailed);
			}

			// Create Property Service.
			try
			{
				serviceCache.Add( ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory() );
			}
			catch(Exception exception)
			{
				throw new TDException(String.Format(Messages.Init_PropertyServiceFailed, exception.Message), false, TDExceptionIdentifier.RDPEventReceiverInitFailed);
			}

			// Create TD Logging service.
			ArrayList errors = new ArrayList();
			try
			{				
				IEventPublisher[]	customPublishers = new IEventPublisher[3];	

				// Create custom database publishers which will be used to publish 
				// events received by the eventreceiver. Note: ids passed in constructors
				// must match those defined in the properties.
				customPublishers[0] = new TDPCustomEventPublisher("TDPDB", SqlHelperDatabase.ReportStagingDB);
				customPublishers[1] = new CJPCustomEventPublisher("CJPDB", SqlHelperDatabase.ReportStagingDB);
				customPublishers[2] = new OperationalEventPublisher("OPDB", SqlHelperDatabase.ReportStagingDB);
						
				Trace.Listeners.Add( new TDTraceListener(Properties.Current, customPublishers, errors ));							
				Trace.Listeners.Remove("System.Diagnostics.DefaultTraceListener");
			}
			catch (TDException tdException)
			{	
				StringBuilder message = new StringBuilder(100);

				if (errors.Count > 0)
				{		
					foreach (string error in errors)
						message.Append(error + ",");
				}
				else
				{
					message.Append(tdException.Message);
				}
				
				throw new TDException(String.Format(Messages.Init_TDTraceListenerFailed, tdException.Identifier.ToString("D"), message.ToString()), false, TDExceptionIdentifier.RDPEventReceiverInitFailed);	
			}

			// Validate Properties which are required by services.
			ArrayList propertyErrors = new ArrayList();
			EventReceiverPropertyValidator validator = new EventReceiverPropertyValidator( Properties.Current );
			validator.ValidateProperty(Keys.ReceiverQueue, errors);
			if (propertyErrors.Count != 0)
			{
				StringBuilder message = new StringBuilder(100);
				foreach (string error in propertyErrors)
					message.Append(error + ",");
		
				throw new TDException(String.Format(Messages.Init_InvalidPropertyKeys, message.ToString()), true, TDExceptionIdentifier.RDPEventReceiverInitFailed);
			}

			// Ensure that properties include configuration for logging ReceivedOperationalEvents.
			try
			{
				Trace.Write( new ReceivedOperationalEvent(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Test message issued by Event Receiver component to ensure configured correctly.")));
			}
			catch (TDException)
			{
				throw new TDException(Messages.Init_EventConfigMissing, false, TDExceptionIdentifier.RDPEventReceiverInitFailed);
			}

		}
	}
}
