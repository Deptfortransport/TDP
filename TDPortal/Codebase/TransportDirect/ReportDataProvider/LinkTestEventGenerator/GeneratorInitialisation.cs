using System;
using System.Collections;
using System.Diagnostics;
using System.Web.Mail;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.ReportDataProvider.LinkTestEventGenerator
{
	/// <summary>
	/// Initalises the sample application.
	/// </summary>
	public class GeneratorInitialisation : IServiceInitialisation
	{
		public void Populate(Hashtable serviceCache)
		{
			// Enable PropertyService
			serviceCache.Add(ServiceDiscoveryKey.Crypto,  new CryptoFactory());
			serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			serviceCache.Add (ServiceDiscoveryKey.GisQuery, new GisQueryFactory());
			serviceCache.Add (ServiceDiscoveryKey.TDMapHandoff, new TDMapHandoffFactory());
			
			
			// Enable the Event Logging Service
			ArrayList errors = new ArrayList();
			
			try
			{
				// create custom email publisher
				IEventPublisher[] customPublishers = new IEventPublisher[1];	
				customPublishers[0] = 
					new CustomEmailPublisher("EMAIL",
											 Properties.Current["Logging.Publisher.Custom.EMAIL.Sender"],
											 MailPriority.Normal,
											 Properties.Current["Logging.Publisher.Custom.EMAIL.SMTPServer"],
											 Properties.Current["Logging.Publisher.Custom.EMAIL.WorkingDir"],
											 errors);

				Trace.Listeners.Add( new TDTraceListener(Properties.Current, customPublishers, errors ));
				Trace.Listeners.Remove("System.Diagnostics.DefaultTraceListener");
			}
			catch (TDException tdEx)
			{
				Console.WriteLine((tdEx.Identifier) + " : " + tdEx.Message);
				Trace.WriteLine((tdEx.Identifier) + " : " + tdEx.Message);
				foreach( string error in errors )
				{
					Trace.WriteLine(error);
					Console.WriteLine(error);
				}
				
				throw new TDException( "Exception raised by TDTraceListener : " + tdEx.Message, tdEx, false, TDExceptionIdentifier.RDPTraceListenerException);
			}
		}
	}
}

