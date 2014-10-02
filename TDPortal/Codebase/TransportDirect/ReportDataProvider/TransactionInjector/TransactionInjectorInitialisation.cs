// ***************************************************************************** 
// NAME			: TransactionInjectorInitialisation.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 03/09/2003 
// DESCRIPTION	: Implementation of the TransactionInjectorInitialisation class.
// ***************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/TransactionInjectorInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:40:14   mturner
//Initial revision.
//
//   Rev 1.9   Jun 21 2004 15:25:16   passuied
//Changes for del6-del5.4.1
//
//   Rev 1.8   Feb 16 2004 17:29:10   geaton
//Incident 643. Removed use of Transaction Injection Fil Publisher (this has now been retired - standard File Publisher will be used instead to reduce configuration complexity).
//
//   Rev 1.7   Dec 19 2003 13:47:42   PNorell
//Transaction injector patch.
//Resolution for 563: Formatting issue : Map page
//
//   Rev 1.6   Nov 07 2003 15:45:04   geaton
//Added exception handling.
//
//   Rev 1.5   Nov 06 2003 17:19:38   geaton
//Refactored following handover from JT to GE.

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

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Used by the TDServiceDiscovery class.
	/// </summary>
	public class TransactionInjectorInitialisation : IServiceInitialisation
	{
		private string serviceName;
		public TransactionInjectorInitialisation(string serviceName)
		{
			this.serviceName = serviceName;
			
		}
		/// <summary>
		/// Sets up TD Services and validates properties.
		/// </summary>
		/// <param name="serviceCache">Service cache to add services to.</param>
		/// <exception cref="TDException">
		/// A service fails to initialise.
		/// </exception>
		public void Populate( Hashtable serviceCache )
		{			
			// Add TD Property Services to cache.
			try
			{
				serviceCache.Add( ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory() );
			}
			catch (Exception exception)
			{
				throw new TDException(String.Format(Messages.Init_TDPropertiesServiceFailed, exception.Message), false, TDExceptionIdentifier.RDPTransactionInjectorPropertiesServiceFailed);		
			}

			IEventPublisher[] customPublishers = new IEventPublisher[0];

			// Create TD Logging service.
			ArrayList errors = new ArrayList();
			try
			{				
				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));							
				Trace.Listeners.Remove("System.Diagnostics.DefaultTraceListener");
			}
			catch (TDException tdException)
			{	
				// Create error message.
				StringBuilder message = new StringBuilder(100);
				foreach (string error in errors)
					message.Append(error + ",");			
				
				throw new TDException(String.Format(Messages.Init_TDTraceListenerFailed, tdException.Identifier.ToString("D"), message.ToString()), false, TDExceptionIdentifier.RDPTransactionInjectorTDTraceInitFailed);	
			}

			// Validate Transaction Injector Properties which are required by services.
			ArrayList propertyErrors = new ArrayList();
			TransactionInjectorPropertyValidator validator = new TransactionInjectorPropertyValidator(Properties.Current, serviceName);	
			validator.ValidateProperty(Keys.TransactionInjectorTransaction, propertyErrors);
			validator.ValidateProperty(Keys.TransactionInjectorFrequency, propertyErrors);
			validator.ValidateProperty(Keys.TransactionInjectorWebService, propertyErrors);
			validator.ValidateProperty(Keys.TransactionInjectorTemplateFileDirectory, propertyErrors);
			if (propertyErrors.Count != 0)
			{
				StringBuilder message = new StringBuilder(100);
				foreach (string error in propertyErrors)
					message.Append(error + ",");
			
				throw new TDException(String.Format(Messages.Init_InvalidPropertyKeys, message.ToString()), false, TDExceptionIdentifier.RDPTransactionInjectorInvalidProperties);
			}

		}
	}
}