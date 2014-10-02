// *********************************************** 
// NAME                 : TicketFeedInitialisation.cs
// AUTHOR               : Steve Tsang
// DATE CREATED         : 26/06/2008
// DESCRIPTION			: Initialise the XML Ticket Feed's cache
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/XMLRailTicketTypeFeed/TransportDirectXmlTicketFeed/TicketFeedInitialisation.cs-arc  $ 
//
//   Rev 1.1   Oct 13 2008 16:45:02   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.0   Sep 19 2008 16:30:12   mmodi
//Updated to enable logging and return status code
//Resolution for 5118: RTTI XML Ticket Type feed - Logging updates

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.XmlTicketFeed
{
    public class TicketFeedInitialisation : IServiceInitialisation
    {
        #region IServiceInitialisation Members

        public void Populate(Hashtable serviceCache)
        {
            // Create cryptographic scheme
            try
            {
                serviceCache.Add(ServiceDiscoveryKey.Crypto, new CryptoFactory());
            }
            catch (Exception exception)
            {
                throw new TDException(String.Format(Messages.Init_CryptographicServiceFailed, exception.Message), false, TDExceptionIdentifier.XMLRTTInitCryptoFailed);
            }


            // Create Property Service.
            try
            {
                serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
            }
            catch (Exception exception)
            {
                throw new TDException(String.Format(Messages.Init_PropertyServiceFailed, exception.Message), false, TDExceptionIdentifier.XMLRTTInitPropertiesFailed);
            }


            // Create TD Logging service.
            ArrayList errors = new ArrayList();
            try
            {
                IEventPublisher[] customPublishers = new IEventPublisher[0];

                Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors)); ;
                Trace.Listeners.Remove("System.Diagnostics.DefaultTraceListener");
            }
            catch (TDException tdException)
            {
                // Create error message.
                StringBuilder message = new StringBuilder();

                message.Append(String.Format(Messages.Init_TDTraceListenerFailed, tdException.Identifier, tdException.Message));
                message.Append("\r\nErrors: ");

                foreach (string error in errors)
                    message.Append(error + ", \n");

                throw new TDException(message.ToString(), false, TDExceptionIdentifier.XMLRTTInitTraceListenerFailed);
            }
        }

        #endregion
    }
}
