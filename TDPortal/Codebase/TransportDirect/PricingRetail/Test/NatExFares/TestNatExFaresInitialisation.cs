// *********************************************** 
// NAME			: TestNatExFaresInitialisation.cs
// AUTHOR		: James Broome
// DATE CREATED	: 02/03/2005
// DESCRIPTION	: Implementation of the TestNatExFaresInitialisation class
// ************************************************ 

using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Web.Mail;
using System.Configuration;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.AdditionalDataModule;
using TransportDirect.UserPortal.PricingRetail.RetailXmlHandoff;
using TransportDirect.UserPortal.PricingRetail.Domain;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
{
	/// <summary>
	/// Initialisation class to be included in the PricingRetail.NatExFares test harnesses
	/// </summary>
	public class TestNatExFaresInitialisation : IServiceInitialisation
	{
		// String used in mock CJP initialisation
		private string fileName = string.Empty;
		private const string LiveCJPManager = "LIVE";


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="fileName">Filename string used to initialise mock CJP</param>
		public TestNatExFaresInitialisation(string fileName)
		{
			this.fileName = fileName;
		}

		public void Populate(Hashtable serviceCache)
		{
			// Add cryptographic scheme
			serviceCache.Add( ServiceDiscoveryKey.Crypto,  new CryptoFactory() );

			// Enable PropertyService					
			serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

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
			}
			catch (TDException tdEx)
			{
				// create message string
				StringBuilder message = new StringBuilder(100);
				message.Append(tdEx.Message); // prepend with existing exception message

				// append all messages returned by TDTraceListener constructor
				foreach( string error in errors )
				{
					message.Append(error);
					message.Append(" ");	
				}

				// log message using .NET default trace listener
				Trace.WriteLine(tdEx.Message);			

				// rethrow exception - use the initial exception id as the id
				throw new Exception(message.ToString());
			} 

			if (fileName == LiveCJPManager)
			{
				// Enable Live CJPManager, using dummy results file
				serviceCache.Add (ServiceDiscoveryKey.Cjp, new MockCjpFactory(@"NatExFares\CoachResult1_out.xml", 0, @"NatExFares\CoachResult1_ret.xml", 0));
				serviceCache.Add (ServiceDiscoveryKey.CjpManager, new CjpManagerFactory());
			}
			else // Properties file name specified for mock NatEx CJP classes
			{
				// Enable Mock NatEx CJP and Mock NatEx CJPManager
				serviceCache.Add (ServiceDiscoveryKey.Cjp, new MockNatExCjpFactory(fileName));
				serviceCache.Add (ServiceDiscoveryKey.CjpManager, new MockNatExCjpManagerFactory());
			}

            // Enable Cache object
            serviceCache.Add( ServiceDiscoveryKey.Cache, new TestJourneyControlMockCache () );
			
			// Enable DataServices
			serviceCache.Add(ServiceDiscoveryKey.DataServices, new DataServicesFactory(null));

			// Enable GISQuery
			serviceCache.Add(ServiceDiscoveryKey.GisQuery, new TransportDirect.UserPortal.PricingRetail.Domain.TestStubGisQuery());

		}
	}
}
