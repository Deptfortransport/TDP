// *********************************************** 
// NAME			: PartnerCatalogueTestInitialisation.cs
// AUTHOR		: Alistair Caunt
// DATE CREATED	: 26/09/03
// DESCRIPTION	: Implementation of the PartnerCatalogueTestInitialisation class
// ************************************************ 

using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;
using System.Web.Mail;

namespace TransportDirect.Partners
{
	/// <summary>
	/// Initialisation class to be included in the PartnerCatalogue test harnesses
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public class TestPartnerCatalogueInitialisation : IServiceInitialisation
	{
		public TestPartnerCatalogueInitialisation()
		{
		}

		public void Populate(Hashtable serviceCache)
		{
//			// Add cryptographic scheme
//			serviceCache.Add( ServiceDiscoveryKey.Crypto,  new CryptoFactory() );
//
//			// Enable PropertyService					
			serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());	
			
			// Add cryptographic scheme
			serviceCache.Add( ServiceDiscoveryKey.Crypto,  new CryptoFactory() );		
//
//			ArrayList errors = new ArrayList();
//			
//			try
//			{
//				// create custom email publisher
//				IEventPublisher[] customPublishers = new IEventPublisher[1];	
//				customPublishers[0] = 
//					new CustomEmailPublisher("EMAIL",
//					Properties.Current["Logging.Publisher.Custom.EMAIL.Sender"],
//					MailPriority.Normal,
//					Properties.Current["Logging.Publisher.Custom.EMAIL.SMTPServer"],
//					Properties.Current["Logging.Publisher.Custom.EMAIL.WorkingDir"],
//					errors);
//				Trace.Listeners.Add( new TDTraceListener(Properties.Current, customPublishers, errors ));
//			}
//			catch (TDException tdEx)
//			{
//				// create message string
//				StringBuilder message = new StringBuilder(100);
//				message.Append(tdEx.Message); // prepend with existing exception message
//
//				// append all messages returned by TDTraceListener constructor
//				foreach( string error in errors )
//				{
//					message.Append(error);
//					message.Append(" ");	
//				}
//
//				// log message using .NET default trace listener
//				Trace.WriteLine(tdEx.Message);			
//
//				// rethrow exception - use the initial exception id as the id
//				throw new Exception(message.ToString());
//			} 
//
//			
//            // Enable DataServices
//			serviceCache.Add(ServiceDiscoveryKey.DataServices, new DataServicesFactory(null));

			// Enable RetailerCatalogue
			serviceCache.Add (ServiceDiscoveryKey.PartnerCatalogue, new PartnerCatalogueFactory());
            			
			
			

		}
	}
}
