// ***********************************************
// NAME 		: Initialisation.cs
// AUTHOR 		: Amit Patel
// DATE CREATED : 30-Jun-2010
// DESCRIPTION 	: Initialisation class to initialise property service for CJPConfigDataImporter
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CJPConfigDataImporter/Initialisation.cs-arc  $
//
//   Rev 1.0   Aug 06 2010 15:32:12   rbroddle
//Initial revision.
//
//   Rev 1.0   Jul 01 2010 12:38:44   apatel
//Initial revision.
//Resolution for 5565: Departure board stop service page fails for stations with 2 Tiplocs or 2 CRS code

using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.CJPConfigDataImporter
{
    /// <summary>
    /// CJP Importer initialisation class
    /// </summary>
    class Initialisation : IServiceInitialisation
    {
        /// <summary>
        /// Initialises TD services and related properties.
        /// </summary>
        /// <param name="serviceCache">Service cache.</param>
        public void Populate(Hashtable serviceCache)
        {
            // Add TD Property Services to cache.
            try
            {
                serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
            }
            catch (Exception exception)
            {
                throw new TDException(String.Format("Failed to initialise the TD Properties Service: [{0}]", exception.Message), false, TDExceptionIdentifier.CCDInitialisationFailed);
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

                throw new TDException(String.Format("Failed to initialise the TD Trace Listener class. Exception ID: [{0}]. Reason: [{1}].", tdException.Identifier.ToString("D"), message.ToString()), false, TDExceptionIdentifier.CCDTDTraceInitFailed);
            }

            

        }
    }
}

