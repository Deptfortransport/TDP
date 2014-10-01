#region Amendment history
// *********************************************** 
// NAME			: $Workfile:   CupInitialisation.cs  $
// AUTHOR		: Peter Norell
// DATE CREATED	: 01/11/2007
// REVISION		: $Revision:   1.0  $
// DESCRIPTION	: Initialisation class for the Cache up service
// ************************************************ 
// $Log:   P:\archives\Codebase\WebTIS\CacheUpService\CupInitialisation.cs-arc  $ 
//
//   Rev 1.0   Nov 02 2007 15:13:08   p.norell
//Initial Revision
//
#endregion
#region Imports
using System;
using System.Collections.Generic;
using System.Text;
using WT.Common;
using WT.Properties.Concrete;
using WT.Properties;
using WT.Common.Logging;
using System.Collections;
using System.Diagnostics;
#endregion

namespace WT.CacheUpService
{
    /// <summary>
    /// Initialisation class for the Cache Up Service
    /// </summary>
    public class CupInitialisation : IServiceInitialisation
    {
        /// <summary>
        /// Initialises WT services and related properties.
        /// </summary>
        /// <param name="serviceMap">Service cache.</param>
        public void Populate(Dictionary<string, IServiceFactory> serviceMap)
        {
            // Create Property Service.
            try
            {
                serviceMap.Add(ServiceDiscovery.PropertyService, new PropertyServiceFactory());
            }
            catch (Exception exception)
            {
                EventLog.WriteEntry("CacheUpService", "Property service load failed with message " + exception.Message, EventLogEntryType.Error);
            }


            IPropertyProvider propertyProvider = (IPropertyProvider)ServiceDiscovery.Current[ServiceDiscovery.PropertyService];

            // Create WT Logging service.
            ArrayList errors = new ArrayList();
            try
            {
                IEventPublisher[] customPublishers = new IEventPublisher[0];

                // Create custom database publishers which will be used to publish 
                // events received by the eventreceiver. Note: ids passed in constructors
                // must match those defined in the properties.

                Trace.Listeners.Add(new WTTraceListener(propertyProvider, customPublishers, errors));
                Trace.Listeners.Remove("System.Diagnostics.DefaultTraceListener");
            }
            catch (WTException tdException)
            {
                StringBuilder message = new StringBuilder(100);

                if (errors.Count > 0)
                {
                    foreach (string error in errors)
                        message.Append(error + ",");
                }
                else
                {
                    message.Append(tdException.Message);
                }
                EventLog.WriteEntry("CacheUpService", "Logging service load failed with message " + message.ToString(), EventLogEntryType.Error);
            }
        }
    }
}    
