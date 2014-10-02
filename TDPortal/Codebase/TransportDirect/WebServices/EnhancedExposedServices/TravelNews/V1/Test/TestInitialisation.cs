// *********************************************** 
// NAME                 : TestInitialisation.cs
// AUTHOR               : Murat Guney
// DATE CREATED         : 20/02/2006
// DESCRIPTION  		: Mock ServiceInitialisation class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/TravelNews/V1/Test/TestInitialisation.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 13:52:30   mturner
//Initial revision.
//
//   Rev 1.1   Feb 28 2006 15:31:20   RWilby
//Removed unneeded services to speedup unit test
//
//   Rev 1.0   Feb 21 2006 10:10:00   mguney
//Initial revision.

using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.UserPortal.AdditionalDataModule;
using TransportDirect.Partners;  
using TransportDirect.UserPortal.TravelNews;
using TransportDirect.UserPortal.DepartureBoardService.StopEventManager;
using TransportDirect.UserPortal.DepartureBoardService.RTTIManager;
using TransportDirect.UserPortal.DepartureBoardService;
using TransportDirect.UserPortal.LocationService;   
using TransportDirect.EnhancedExposedServices.Helpers;
using TransportDirect.UserPortal.JourneyPlannerService;   

namespace TransportDirect.EnhancedExposedServices.TravelNews.V1.Test
{
	/// <summary>
	/// ServiceInitialisation class
	/// </summary>
	public class TestInitialisation: IServiceInitialisation  
	{
		/// <summary>
		/// Blank contructor 
		/// </summary>
		public TestInitialisation()
		{
		}		

		/// <summary>
		/// Implementation of Populate method for unit testing
		/// </summary>
		/// <param name="serviceCache"></param>
		public void Populate(Hashtable serviceCache)
		{			
			TextWriterTraceListener logTextListener = null;
			ArrayList errors = new ArrayList();

			try
			{				
				// Add cryptographic scheme
				serviceCache.Add( ServiceDiscoveryKey.Crypto,  new CryptoFactory() );

				// initialise properties service
				serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
							
				// initialise logging service	
				IEventPublisher[]	customPublishers = new IEventPublisher[0];			
				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
			
			}
			catch (TDException tdException)
			{	
				// create message string
				StringBuilder message = new StringBuilder(100);
				message.Append(tdException.Message);

				// append error messages, if any
				foreach( string error in errors )
				{
					message.Append(error);
					message.Append(" ");	
				}
				string logMessage = "{0} ExceptionID: {1}";
				
				Trace.WriteLine(string.Format(logMessage,  message.ToString(), tdException.Identifier.ToString("D")));		
				throw new TDException(message.ToString(), tdException, false, tdException.Identifier);
			}
			catch (Exception exception)
			{
				Trace.WriteLine(exception.Message);
				throw exception;
			}
			finally
			{
				if( logTextListener != null )
				{
					logTextListener.Flush();
					logTextListener.Close();
					Trace.Listeners.Remove(logTextListener);
				}
			}

			// Attention! here the DataServices component is loaded passing a null ResourceManager.
			// This is because it is used specifically within the WebService which doesn't use resources.
			serviceCache.Add(ServiceDiscoveryKey.DataServices, new DataServicesFactory(null));
            
			// initialise TravelNews Handler
			serviceCache.Add (ServiceDiscoveryKey.TravelNews, new TestTravelNewsHandlerFactory());
		}
	}
}
