// ****************************************************** 
// NAME                 : RBOServiceInitialisation.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 13/10/2003 
// DESCRIPTION  : Performs initialisation of TD services.
// ******************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/RBOServiceInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:14   mturner
//Initial revision.
//
//   Rev 1.7   Mar 22 2005 20:49:50   RPhilpott
//Supplement filtering using DataServices
//
//   Rev 1.6   Feb 10 2005 17:38:18   RScott
//Updated to include Reservation and Supplement Business Objects (RVBO, SBO)
//
//   Rev 1.5   Oct 31 2003 13:20:04   CHosegood
//Added Security Cache
//
//   Rev 1.4   Oct 28 2003 20:05:00   geaton
//Changes to support housekeeping and timeout functionality.
//
//   Rev 1.3   Oct 17 2003 13:11:12   geaton
//Added initialisation of LBO and exception handling.
//
//   Rev 1.2   Oct 17 2003 10:48:46   geaton
//Added call to initialise RBO.
//
//   Rev 1.1   Oct 15 2003 21:35:38   geaton
//Moved pool initialisation to block after logging intialisation to allow logging using tdtracelistener.
//
//   Rev 1.0   Oct 15 2003 14:40:20   geaton
//Initial Revision

using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Configuration;
using System.IO;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;

using TransportDirect.UserPortal.DataServices;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Used by the TDServiceDiscovery class.
	/// </summary>
	public class RBOServiceInitialisation : IServiceInitialisation
	{
		// This filename is not configurable - it is documented in deployment guide
		private const string DefaultLogFilename = "td.UserPortal.RetailBusinessObjects.log";

		/// <summary>
		/// Sets up the the Properties object, and the trace listener.
		/// </summary>
		/// <param name="serviceCache">Service cache.</param>
		public void Populate( Hashtable serviceCache )
		{	
			TextWriterTraceListener logTextListener = null;
			ArrayList errors = new ArrayList();

			try
			{
				// initialise .NET file trace listener for use prior to TDTraceListener
				string logfilePath = ConfigurationSettings.AppSettings[Keys.DefaultLogPath];
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

				// Enable DataServices
				// TODO: passing null for the ResourceManager parameter is a nasty bodge that
				//       will undoubtedly break something eventually -- needs fixing!
				serviceCache.Add (ServiceDiscoveryKey.DataServices, new DataServicesFactory(null));

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


			try
			{
				// Initialise BO pools (by requesting instance of pool - since use Singleton pattern)
				// This is performed here to check properties are valid and that intialisation succeeds.
				FBOPool.GetFBOPool();
				RBOPool.GetRBOPool();
				LBOPool.GetLBOPool();
				RVBOPool.GetRVBOPool();
				SBOPool.GetSBOPool();
			}
			catch (TDException tdException)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Init_PoolCreationFailed, tdException.Message)));
				throw new TDException(String.Format(Messages.Init_PoolCreationFailed, tdException.Message), tdException, true, TDExceptionIdentifier.PRHBOPoolCreationFailed);
			}
		}
	}
}
