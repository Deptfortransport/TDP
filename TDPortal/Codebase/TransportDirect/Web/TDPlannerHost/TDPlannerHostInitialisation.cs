// ***********************************************
// NAME 		: TDPlannerHostInitialisation.cs.cs
// AUTHOR 		: Rich Broddle
// DATE CREATED : 17/02/2010
// DESCRIPTION 	: This implements the IService Initialisation
// interface and initialises any services required by the JourneyPlanRunnerCaller
// for internal and third party planners other than Atkins CJP.
// Currently only used for international planner
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TDPlannerHost/TDPlannerHostInitialisation.cs-arc  $ 
//
//   Rev 1.0   Feb 22 2010 15:44:14   RBroddle
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1

using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.InternationalPlanner;
using TransportDirect.UserPortal.InternationalPlannerControl;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.JourneyEmissions;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.TDPlannerHost
{
	/// <summary>
	/// Summary description for TDPlannerHostInitialisation.
	/// </summary>
	public class TDPlannerHostInitialisation : IServiceInitialisation	
	{
		private const string DefaultLogFilename = "td.UserPortal.TDPlannerHost.log";

		#region IServiceInitialisation Members

		public void Populate(System.Collections.Hashtable serviceCache)
		{

			TextWriterTraceListener logTextListener = null;
			ArrayList errors = new ArrayList();

			try
			{
				// initialise .NET file trace listener for use prior to TDTraceListener
				string logfilePath = ConfigurationManager.AppSettings["DefaultLogFilepath"];
				Stream logFile = File.Create(logfilePath + "\\" + DefaultLogFilename);
				logTextListener = new System.Diagnostics.TextWriterTraceListener(logFile);
				Trace.Listeners.Add(logTextListener);
				
				// Add cryptographic scheme
				serviceCache.Add( ServiceDiscoveryKey.Crypto,  new CryptoFactory() );

				// initialise properties service
				serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

				// initialise logging service	
				IEventPublisher[]	customPublishers = new IEventPublisher[0];			
				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Started - Populate method"));


                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: GisQuery, GazetteerFactory, PlaceDataProvider"));

				//Add Location Search
				serviceCache.Add (ServiceDiscoveryKey.GisQuery, new GisQueryFactory());
				serviceCache.Add (ServiceDiscoveryKey.GazetteerFactory, new TDGazetteerFactory());
				serviceCache.Add (ServiceDiscoveryKey.PlaceDataProvider, new PlaceDataProviderFactory());

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: Cache, DataChangeNotification, DataServices"));

				//Add Cache object
				serviceCache.Add( ServiceDiscoveryKey.Cache, new TDCache() );

				serviceCache.Add( ServiceDiscoveryKey.DataChangeNotification, new DataChangeNotificationFactory() );

				// Attention! here the DataServices component is loaded passing a null ResourceManager.
				// This is because it is used specifically within the WebService which doesn't use resources.
				serviceCache.Add(ServiceDiscoveryKey.DataServices, new DataServicesFactory(null));

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: JourneyEmissionsFactor"));

                // Enable JourneyEmissionsFactor
                serviceCache.Add(ServiceDiscoveryKey.JourneyEmissionsFactor, new JourneyEmissionsFactorFactory());

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: InternationalPlanner"));

                // Add International Planner
                serviceCache.Add(ServiceDiscoveryKey.InternationalPlannerFactory, new InternationalPlannerFactory());

                // Add International Planner Data
                serviceCache.Add(ServiceDiscoveryKey.InternationalPlannerDataFactory, new InternationalPlannerDataFactory());

                // Enable InternationalPlannerManagerFactory
                serviceCache.Add(ServiceDiscoveryKey.InternationalPlannerManager, new InternationalPlannerManagerFactory());
                                
			}

			catch ( TDException tdException)
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

				Trace.WriteLine(message.ToString() + "ExceptionID: " + tdException.Identifier.ToString("D"));		
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
		}

		#endregion
	}
}
