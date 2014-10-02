// *********************************************** 
// NAME			: PricingRetailTestInitialisation.cs
// AUTHOR		: Alistair Caunt
// DATE CREATED	: 26/09/03
// DESCRIPTION	: Implementation of the PricingRetailTestInitialisation class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/Domain/TestPricingRetailInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:26   mturner
//Initial revision.
//
//   Rev 1.10   May 25 2007 16:22:16   build
//Automatically merged from branch for stream4401
//
//   Rev 1.9.1.0   May 09 2007 14:51:52   mmodi
//Added Coach fares lookup
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.9   Nov 29 2005 14:51:10   mguney
//TestMockExceptionalFaresLookup included.
//Resolution for 3230: DN040: Route60 and DayReturn fares should be handled in a different manner.
//
//   Rev 1.8   Nov 09 2005 12:31:42   build
//Automatically merged from branch for stream2818
//
//   Rev 1.7.1.5   Nov 04 2005 16:19:50   RPhilpott
//NUnit fixes.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.7.1.4   Oct 29 2005 13:55:42   RPhilpott
//Add test stub factory for time-based fares
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.7.1.3   Oct 28 2005 16:26:48   RPhilpott
//Remove redundant PriceSupplier stub
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.7.1.2   Oct 28 2005 15:43:22   mguney
//CoachFares referral included.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.7.1.1   Oct 28 2005 15:12:18   mguney
//TestMockCoachOperatorLookup included.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.7.1.0   Oct 25 2005 15:35:32   mguney
//CoachFaresInterface and TimeBasedFareSupplier factories included.
//Resolution for 2818: DEL 8 Stream: Search by Price

using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Net.Mail;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.AdditionalDataModule;
using TransportDirect.UserPortal.PricingRetail.RetailXmlHandoff;
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;
using TransportDirect.UserPortal.PricingRetail.CoachFares;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Initialisation class to be included in the PricingRetail test harnesses
	/// </summary>
	public class TestPricingRetailInitialisation : IServiceInitialisation
	{
		public TestPricingRetailInitialisation()
		{
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

            // Enable Cache object
            serviceCache.Add( ServiceDiscoveryKey.Cache, new TestJourneyControlMockCache () );
			
            // Enable DataServices
			serviceCache.Add(ServiceDiscoveryKey.DataServices, new DataServicesFactory(null));

			// Enable RetailerCatalogue
			serviceCache.Add (ServiceDiscoveryKey.RetailerCatalogue, new RetailerCatalogueFactory());

			// Enable GISQuery
			serviceCache.Add(ServiceDiscoveryKey.GisQuery, new TestStubGisQuery());

			// Enable AdditionalDataModule
			serviceCache.Add(ServiceDiscoveryKey.AdditionalData, new TestStubAdditionalData());

			// Enable PriceSupplierFactory
			serviceCache.Add(ServiceDiscoveryKey.TimeBasedFareSupplier, new TestStubTimeBasedFareSupplierFactory());

            // Enable RetailXmlSchema
            serviceCache.Add(ServiceDiscoveryKey.RetailXmlSchema, new RetailXmlSchemaFactory());

			// Add Mock Factory for Coach Fare Interfaces
			serviceCache.Add(ServiceDiscoveryKey.CoachFaresInterface,new TestCoachFaresInterfaceFactory());

			// Add Factory for TestMockCoachOperatorLookup
			serviceCache.Add(ServiceDiscoveryKey.CoachOperatorLookup,new TestMockCoachOperatorLookup());

			//Enable Excepional Fares Lookup
			serviceCache.Add(ServiceDiscoveryKey.ExceptionalFaresLookup, new TestMockExceptionalFaresLookup());

			//Enable Coach Fares Lookup
			serviceCache.Add(ServiceDiscoveryKey.CoachFaresLookup, new TestMockCoachFaresLookup());
		}
	}
}
