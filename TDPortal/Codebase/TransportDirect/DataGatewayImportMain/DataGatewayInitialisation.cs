// *********************************************** 
// NAME                 : DataGatewayInitialisation.cs 
// AUTHOR               : JM and TK (Pair Programmers of the world unite)
// DATE CREATED         : 17/11/2003 
// DESCRIPTION  : DatagatewayInitialisation class. 
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayImportMain/DataGatewayInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:20:34   mturner
//Initial revision.
//
//   Rev 1.1   Jan 12 2004 17:25:14   jmorrissey
//Updated to use defined exceptions rather than TDExceptionIdentifier.Undefined
//
//   Rev 1.0   Nov 17 2003 15:47:16   JMorrissey
//Initial Revision

using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.DataImport
{
	/// <summary>
	/// Used by the TDServiceDiscovery class.
	/// </summary>
	public class DataGatewayInitialisation : IServiceInitialisation
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
				throw new TDException(String.Format("Crypto init failed.", exception.Message), false, TDExceptionIdentifier.DGCryptoInitFailed);
			}

			// Create Property Service.
			try
			{
				serviceCache.Add( ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory() );
			}
			catch(Exception exception)
			{
				throw new TDException(String.Format("Property service init failed.", exception.Message), false, TDExceptionIdentifier.DGPropertyInitFailed);
			}

			// Create TD Logging service.
			ArrayList errors = new ArrayList();
			try
			{				
				IEventPublisher[]	customPublishers = new IEventPublisher[0];	
		
				Trace.Listeners.Add( new TDTraceListener(Properties.Current, customPublishers, errors ));			;							
				Trace.Listeners.Remove("System.Diagnostics.DefaultTraceListener");
			}
			catch (TDException tdException)
			{	
				// Create error message.
				StringBuilder message = new StringBuilder(100);
				foreach (string error in errors)
					message.Append(error + ",");			
				
				throw new TDException("Initialisation of TD Trace Listener failed" + tdException.Identifier.ToString("D") + message.ToString(), false, TDExceptionIdentifier.DGTraceListenerInitFailed);	
			}

		}
	}
}

