// *********************************************** 
// NAME                 : SSTraceListener.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Class which adds the custom publishers to the Trace for logging purposes
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/SSTraceListener.cs-arc  $
//
//   Rev 1.1   Aug 23 2011 11:01:16   mmodi
//Moved EventStatus code into a seperate DLL to allow the monitor tool to be enhanced with a manual import feature
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.0   Apr 01 2009 13:27:20   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Text;

using AO.Common;
using AO.Properties;

using PropertyService = AO.Properties.Properties;

namespace AO.EventLogging
{
    #region Event classes
    public class TraceLevelEventArgs : EventArgs
    {
        private SSTraceLevel traceLevel;

        public TraceLevelEventArgs(SSTraceLevel traceLevel)
        {
            this.traceLevel = traceLevel;
        }

        /// <summary>
        /// Gets the trace level held in the event args instance.
        /// </summary>
        public SSTraceLevel TraceLevel
        {
            get { return traceLevel; }
        }
    }

    public class CustomLevelEventArgs : EventArgs
    {
        private CustomEventLevel customLevel;

        public CustomLevelEventArgs(CustomEventLevel customLevel)
        {
            this.customLevel = customLevel;
        }

        /// <summary>
        /// Gets the custom event level held in the event args instance.
        /// </summary>
        public CustomEventLevel CustomLevel
        {
            get { return customLevel; }
        }
    }

    public class CustomLevelsEventArgs : EventArgs
    {
        private Hashtable customLevels;

        public CustomLevelsEventArgs(Hashtable customLevels)
        {
            this.customLevels = customLevels;
        }

        /// <summary>
        /// Gets the custom event levels held in the event args instance.
        /// </summary>
        public Hashtable CustomLevels
        {
            get { return customLevels; }
        }
    }

    public class DefaultPublisherCalledEventArgs
    {
        LogEvent le = null;
        
        public DefaultPublisherCalledEventArgs(LogEvent le)
        {
            this.le = le;
        }

        public LogEvent LogEvent
        {
            get
            {
                return le;
            }
        }
    }

    #endregion

    delegate void PublisherDelegate(LogEvent le);

    public delegate void TraceLevelChangeEventHandler(object sender, TraceLevelEventArgs e);
    public delegate void CustomLevelChangeEventHandler(object sender, CustomLevelEventArgs e);
    public delegate void CustomLevelsChangeEventHandler(object sender, CustomLevelsEventArgs e);
    public delegate void DefaultPublisherCalledEventHandler(object sender, DefaultPublisherCalledEventArgs e);


    /// <summary>
    /// Class used to listen for logging output.
    /// An instance of this class should be added to the trace listener set.
    /// </summary>
    /// <example> 
    /// Trace.Listeners.Add(new SSTraceListener(properties,
    ///											customPublishers,
    ///											errors));
    /// </example>
    public class SSTraceListener : TraceListener
    {
        // Need declare events as static since subscribers (all of which are static)
        // may register for events before an instance of SSTraceListener is created.
        public static event TraceLevelChangeEventHandler OperationalTraceLevelChange;
        public static event CustomLevelChangeEventHandler CustomLevelChange;
        public static event CustomLevelsChangeEventHandler CustomLevelsChange;
        
        public event DefaultPublisherCalledEventHandler DefaultPublisherCalled;

        private int defaultPublisherIndex;

        private LoggingPropertyValidator validator;

        PublisherDelegate[][] eventPublisherMappings;
        Hashtable eventIndexTable;

        #region Constructor

        /// <summary>
		/// Constructor for SSTraceListener instances.
		/// </summary>
		/// <param name="properties">Properties used to create listener.</param>
		/// <param name="customPublishers">Array containing zero or more custom event publishers instances. 
		/// Note that this parameter cannot be null, though can be an empty array.</param>
		/// <param name="errors">Holds any errors that occurred during construction.</param>
		/// <exception cref="TransportDirect.Common.TDException">Unrecoverable error occured when creating the TDTraceListener instance.</exception>
        public SSTraceListener(PropertyService properties, IEventPublisher[] customPublishers, ArrayList errors)
            : base("SSTraceListener")
		{
			this.validator = new LoggingPropertyValidator(properties);

			PublisherGroup standardPublisherGroup = new EventPublisherGroup(properties);
			standardPublisherGroup.CreatePublishers(errors);

            if (customPublishers == null)
                errors.Add(Messages.SSTCustomPublisherArrayInvalid);

            if (errors.Count == 0)
                ValidateCustomPublishers(properties, customPublishers, errors);

			if (errors.Count == 0)
			{
                ValidateDefaultPublisher(properties, standardPublisherGroup.Publishers, customPublishers, errors);
                ValidateEventProperties(properties, standardPublisherGroup.Publishers, customPublishers, errors);
			}

			if (errors.Count == 0)
                AssignEventsToPublishers(properties, standardPublisherGroup.Publishers, customPublishers);
			
			//if (errors.Count == 0)
			//{
				// subscribe to ANY changes that may happen in properties
			//	properties.Superseded += new SupersededEventHandler(this.OnSuperseded);				
			//}

			if (errors.Count == 0)
			{
				// Force creation of any static classes that must subscribe to events 
				// generated by SSTraceListener.
				// This is necessary in case they have not yet been created.
				// (Since static classes are created on first use.)
				SSTraceSwitch ssTraceSwitch = new SSTraceSwitch();
				CustomEventSwitch customEventSwitch = new CustomEventSwitch();

				// Notify subscribers that SSTraceListener has been created successfully
				if (!NotifySubscribers(properties))
					errors.Add(Messages.SSTTraceListenerSubscriberNotificationFailed);
			}

			if (errors.Count != 0)
			{		
				throw new SSException(String.Format(Messages.SSTTraceListenerConstructor,
													ConfigurationManager.AppSettings["propertyservice.applicationid"],
													ConfigurationManager.AppSettings["propertyservice.groupid"]),
													false, SSExceptionIdentifier.ELSTraceListenerConstructor);
			}
        }

        #region Private methods

        private void ValidateDefaultPublisher(PropertyService properties,
            ArrayList standardPublishers,
            IEventPublisher[] customPublishers,
            ArrayList errors)
        {
            if (validator.ValidateProperty(Keys.DefaultPublisher, errors))
                ValidatePublisherExists(properties[Keys.DefaultPublisher],
                                        Keys.DefaultPublisher,
                                        standardPublishers, customPublishers, errors);
        }

        private void ValidateCustomPublishers(PropertyService properties,
            IEventPublisher[] publishers,
            ArrayList errors)
        {
            StringBuilder key = new StringBuilder(50);

            foreach (IEventPublisher publisher in publishers)
            {

                if ((publisher.Identifier.Length == 0) ||
                    (publisher.Identifier == null))
                {
                    errors.Add(String.Format(Messages.SSTCustomPublisherInvalidId,
                        publisher.GetType().Name,
                        publisher.Identifier));
                }
                else
                {
                    key.Length = 0;
                    key.Append(String.Format(Keys.CustomPublisherName, publisher.Identifier));

                    if (properties[key.ToString()] != publisher.GetType().Name)
                    {
                        errors.Add(String.Format(Messages.SSTCustomPublisherNameMismatch,
                            publisher.GetType().Name,
                            key.ToString()));
                    }
                }
            }

        }

        private bool ValidatePublisherExists(string publisherId,
            string key,
            ArrayList standardPublishers,
            IEventPublisher[] customPublishers,
            ArrayList errors)
        {
            bool found = false;

            foreach (IEventPublisher publisher in standardPublishers)
            {
                if (publisher.Identifier == publisherId)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                foreach (IEventPublisher publisher in customPublishers)
                {
                    if (publisher.Identifier == publisherId)
                    {
                        found = true;
                        break;
                    }
                }
            }

            if (!found)
            {
                errors.Add(String.Format(Messages.ELPVUndefinedPublisher, key, publisherId));
            }

            return found;
        }

        private void ValidatePublishersExist(PropertyService properties,
                                             string eventKey,
                                             ArrayList standardPublishers,
                                             IEventPublisher[] customPublishers,
                                             ArrayList errors)
        {

            string[] Ids = null;
            string publisherIds = properties[eventKey];

            if (publisherIds != null)
            {
                if (publisherIds.Length != 0)
                    Ids = publisherIds.Split(' ');
                else
                {
                    errors.Add(String.Format(Messages.ELPVEventHasZeroPublishers, eventKey));
                    Ids = new String[0];
                }
            }
            else
            {
                errors.Add(String.Format(Messages.ELPVEventHasZeroPublishers, eventKey));
                Ids = new String[0];
            }

            foreach (string id in Ids)
            {
                ValidatePublisherExists(id, eventKey, standardPublishers, customPublishers, errors);
            }
        }

        private void ValidateEventProperties(PropertyService properties,
            ArrayList standardPublishers,
            IEventPublisher[] customPublishers,
            ArrayList errors)
        {
            // validate trace levels
            validator.ValidateProperty(Keys.OperationalTraceLevel, errors);
            validator.ValidateProperty(Keys.CustomEventsLevel, errors);

            #region Custom events 

            // validate publisher property for all custom events
            string[] customEventIds = null;
            string customEvents = properties[Keys.CustomEvents];
            if (customEvents != null)
            {
                if (customEvents.Length != 0) // valid to have no custom events
                    customEventIds = customEvents.Split(' ');
                else
                    customEventIds = new String[0];
            }
            else
            {
                // NB validation of missing custom events key is performed elsewhere
                customEventIds = new String[0];
            }

            foreach (string customEventId in customEventIds)
            {
                ValidatePublishersExist(properties,
                                        String.Format(Keys.CustomEventPublishers, customEventId),
                                        standardPublishers,
                                        customPublishers, errors);
            }

            #endregion

            // validate publishers for operational events
            ValidatePublishersExist(properties,
                                    Keys.OperationalEventVerbosePublishers,
                                    standardPublishers,
                                    customPublishers, errors);

            ValidatePublishersExist(properties,
                                    Keys.OperationalEventInfoPublishers,
                                    standardPublishers,
                                    customPublishers, errors);

            ValidatePublishersExist(properties,
                                    Keys.OperationalEventErrorPublishers,
                                    standardPublishers, 
                                    customPublishers, errors);

            ValidatePublishersExist(properties,
                                    Keys.OperationalEventWarningPublishers,
                                    standardPublishers,
                                    customPublishers, errors);

            // validate "non-publisher properties" for all custom events
            validator.ValidateProperty(Keys.CustomEvents, errors);
        }


        private void AssignEventsToPublishers(PropertyService properties,
            ArrayList standardPublishers,
            IEventPublisher[] customPublishers)
        {
            // determine number of event types
            string[] customEventIds = null;
            string customEvents = properties[Keys.CustomEvents];
            if (customEvents.Length != 0) // valid to have no custom events
                customEventIds = customEvents.Split(' ');
            else
                customEventIds = new String[0];
            
            int numOfEvents = customEventIds.GetLength(0) +
                typeof(SSTraceLevel).GetFields().GetLength(0) - 2; // values for Undefined and Off are not used for events

            numOfEvents++; // add an extra row to hold the default publisher
            this.defaultPublisherIndex = numOfEvents - 1; // sub 1 since zero based array

            // create array to hold publisher writelog delegates
            eventPublisherMappings = new PublisherDelegate[numOfEvents][];

            // create hash table to store event index lookups
            eventIndexTable = new Hashtable();

            // initialise indexer used to for each event row in the array
            int eventArrayIndexer = 0;

            // Set up operational event mappings.
            // Need to use level property to make hash table indexer unique
            AssignEventToPublishers(properties,
                Keys.OperationalEventErrorPublishers,
                eventArrayIndexer,
                OperationalEvent.ClassNamePrefix + SSTraceLevel.Error.ToString(),
                standardPublishers, customPublishers);

            eventArrayIndexer++;

            AssignEventToPublishers(properties,
                Keys.OperationalEventWarningPublishers,
                eventArrayIndexer,
                OperationalEvent.ClassNamePrefix + SSTraceLevel.Warning.ToString(),
                standardPublishers, customPublishers);

            eventArrayIndexer++;

            AssignEventToPublishers(properties,
                Keys.OperationalEventVerbosePublishers,
                eventArrayIndexer,
                OperationalEvent.ClassNamePrefix + SSTraceLevel.Verbose.ToString(),
                standardPublishers, customPublishers);

            eventArrayIndexer++;

            AssignEventToPublishers(properties,
                Keys.OperationalEventInfoPublishers,
                eventArrayIndexer,
                OperationalEvent.ClassNamePrefix + SSTraceLevel.Info.ToString(),
                standardPublishers, customPublishers);

            eventArrayIndexer++;


            // Set up custom event publisher mappings
            foreach (string customEventId in customEventIds)
            {
                string ceName = properties[String.Format(Keys.CustomEventName, customEventId)];

                AssignEventToPublishers(properties,
                    String.Format(Keys.CustomEventPublishers, customEventId),
                    eventArrayIndexer,
                    ceName,
                    standardPublishers, customPublishers);

                eventArrayIndexer++;
            }

            
            // set up default publisher
            AssignEventToPublishers(properties,
                Keys.DefaultPublisher,
                this.defaultPublisherIndex,
                "Default", // irrelevent since not used at write time
                standardPublishers, customPublishers);

        }

        
        private void AssignEventToPublishers(PropertyService properties,
            string pubishersPropertyKey,
            int eventArrayIndexer,
            string eventHashIndexer,
            ArrayList standardPublishers,
            IEventPublisher[] customPublishers)
        {

            string[] publisherIds = null;
            string publishers = properties[pubishersPropertyKey];
            publisherIds = publishers.Split(' ');

            int publisherCount = 0;

            PublisherDelegate publisherDelegate = null;

            // allocate a row in the array to store each of the publishers delegates
            eventPublisherMappings[eventArrayIndexer]
                = new PublisherDelegate[publisherIds.GetLength(0)];

            // iterate through publishers to get a delagates
            foreach (string publisher in publisherIds)
            {
                // search for publisher instance in standard publishers
                foreach (IEventPublisher publisherInstance in standardPublishers)
                {
                    if (publisherInstance.Identifier == publisher)
                    {
                        publisherDelegate = new PublisherDelegate(publisherInstance.WriteEvent);
                        break;
                    }
                }

                // if publisher not found in standard publishers then search custom publishers
                if (publisherDelegate == null)
                {
                    foreach (IEventPublisher publisherInstance in customPublishers)
                    {
                        if (publisherInstance.Identifier == publisher)
                        {
                            publisherDelegate = new PublisherDelegate(publisherInstance.WriteEvent);
                            break;
                        }
                    }
                }

                // store delegate in array for this publisher
                eventPublisherMappings[eventArrayIndexer][publisherCount]
                    = publisherDelegate;

                // store array indexer in hash table - this is used for fast lookup into array
                eventIndexTable[eventHashIndexer] = eventArrayIndexer;

                publisherCount++;
                publisherDelegate = null;
            }
        }

        /// <summary>
        /// Notifies subscribers that one or more properties have changed.
        /// NB property values are forwarded regardless of whether they have changed
        /// Property values are validated before they are forwarded.
        /// The 'Current' properties are always used.
        /// </summary>
        /// <param name="properties">Latest properties values.</param>
        /// <returns>
        /// Result. True if subscribers were notified without errors, otherwise false if errors occurred. 
        /// </returns>
        private bool NotifySubscribers(PropertyService properties)
        {
            ArrayList errors = new ArrayList();
            bool result = true;

            if (validator.ValidateProperty(Keys.OperationalTraceLevel, errors))
            {
                string operationalTraceLevel = properties[Keys.OperationalTraceLevel];
                SSTraceLevel operationalTraceLevelValue
                    = (SSTraceLevel)PropertyValidator.StringToEnum(typeof(SSTraceLevel), operationalTraceLevel);

                if (OperationalTraceLevelChange != null)
                    OperationalTraceLevelChange(this, new TraceLevelEventArgs(operationalTraceLevelValue));
            }

            if (validator.ValidateProperty(Keys.CustomEventsLevel, errors))
            {
                string level = properties[Keys.CustomEventsLevel];
                CustomEventLevel levelValue
                    = (CustomEventLevel)PropertyValidator.StringToEnum(typeof(CustomEventLevel), level);

                if (CustomLevelChange != null)
                    CustomLevelChange(this, new CustomLevelEventArgs(levelValue));
            }

            if (validator.ValidateProperty(Keys.CustomEvents, errors))
            {
                Hashtable levels = new Hashtable();

                string[] eventIds = null;
                string events = properties[Keys.CustomEvents];
                if (events.Length != 0) // valid to have no custom events
                    eventIds = events.Split(' ');
                else
                    eventIds = new String[0];

                foreach (string eventId in eventIds)
                {
                    string key = properties[String.Format(Keys.CustomEventName, eventId)];
                    string level = properties[String.Format(Keys.CustomEventLevel, eventId)];

                    levels[key] = (CustomEventLevel)PropertyValidator.StringToEnum(typeof(CustomEventLevel), level);
                }

                if (CustomLevelsChange != null)
                    CustomLevelsChange(this, new CustomLevelsEventArgs(levels));
            }

            if (errors.Count != 0)
            {
                // Write any validation errors to default publisher
                // as operational events.
                // The error text is used as the message
                // of the operational event.
                for (int i = 0; i < errors.Count; i++)
                {
                    // create an operational event representing the error
                    OperationalEvent operationalEvent =
                        new OperationalEvent(SSEventCategory.Infrastructure,
                        SSTraceLevel.Error,
                        errors[i].ToString());

                    // write the operational event to the default publisher to ensure that is has a higher chance of being published.
                    eventPublisherMappings[defaultPublisherIndex][0](operationalEvent);
                }

                result = false;
            }

            return result;
        }

        #endregion

        #endregion

        #region Write

        /// <summary>
        /// Publishes a LogEvent object to the appropriate sink as specified in the 
        /// property provider used to create the SSTraceListener instance.
        /// </summary>
        /// <remarks>
        /// If the event cannot be published by it's configured publisher then an attempt
        /// is made to publish it using the default publisher.
        /// </remarks>
        /// <param name="o"><c>LogEvent</c> to publish.</param>
        /// <exception cref="TransportDirect.Common.TDException">An error condition prevented the <c>LogEvent</c> from being published by any publisher, including the default publisher.</exception>
        public override void Write(object o)
        {
            int eventArrayIndex = 0;
            LogEvent le = null;

            // Cast object being logged to a LogEvent class instance
            try
            {
                le = (LogEvent)o;
            }
            catch (InvalidCastException invalidCastException)
            {
                throw new SSException(Messages.SSTTraceListenerUnknownClass, invalidCastException, false, SSExceptionIdentifier.ELSTraceListenerUnknownObject);
            }
            catch (Exception exception) // catch undocumented exceptions
            {
                throw new SSException(Messages.SSTTraceListenerUnknownClass, exception, false, SSExceptionIdentifier.ELSTraceListenerUnknownObject);
            }

            // Check if event type should be logged before proceeding
            if (!le.Filter.ShouldLog(le))
                return;

            // Determine quick lookup index of event based on it's class name
            eventArrayIndex = (int)eventIndexTable[le.ClassName];


            // Loop through all publishers assigned to event, calling WriteEvent
            for (int publisher = 0;
                publisher < eventPublisherMappings[eventArrayIndex].GetLength(0);
                publisher++)
            {
                try
                {
                    // Call WriteEvent for this publisher
                    eventPublisherMappings[eventArrayIndex][publisher](le);


                    // Audit the fact that the event has been published.
                    //if (!le.AuditPublishersOff)
                    //{
                    //    le.PublishedBy = eventPublisherMappings[eventArrayIndex][publisher].Target.GetType().FullName;
                    //}


                }
                catch (SSException ex)
                {
                    // Log an error, if exception not already logged.
                    // Only log for custom events to prevent infinite recursion
                    // should the operational event fail to write.
                    if ((le is CustomEvent) && (!ex.Logged))
                    {
                        Trace.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error,
                            string.Format(Messages.SSTTConfiguredPublisherFailed, le.ClassName, eventPublisherMappings[eventArrayIndex][publisher].Target.GetType().FullName, ex.Message),
                            ex));
                    }

                    // Failed to log using configured publisher so 
                    // attempt to log event using the default publisher
                    try
                    {
                        if (DefaultPublisherCalled != null)
                            DefaultPublisherCalled(this, new DefaultPublisherCalledEventArgs(le));

                        eventPublisherMappings[this.defaultPublisherIndex][0](le);

                        // audit the fact that the event has been published by the default publisher
                        //if (!le.AuditPublishersOff)
                        //    le.PublishedBy = eventPublisherMappings[this.defaultPublisherIndex][0].Target.GetType().FullName;

                        // Log a warning to highlight that default publisher was used.
                        // Only report this for custom events to prevent infinite
                        // recursion should the operational event fail to write.
                        if (le is CustomEvent)
                        {
                            Trace.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Warning,
                                string.Format(Messages.SSTTraceListenerDefaultPublisherUsed, le.ClassName)));
                        }

                    }
                    catch (SSException ex2)
                    {
                        // All publishers failed to log! This exception will filter up to the trace listener client.
                        throw new SSException(Messages.SSTTraceListenerDefaultPublisherFailure, ex2, false, SSExceptionIdentifier.ELSDefaultPublisherFailed);
                    }
                }

            }

        }

        #region Unsupported methods

        /// <summary>
        /// This prototype is not supported by SSTraceListener.
        /// An SSException is thrown to highlight that it has been called.
        /// </summary>
        public override void Write(string s)
        {
            throw new SSException(
                string.Format(Messages.SSTTraceListenerUnsupportedPrototype, s),
                false, 
                SSExceptionIdentifier.ELSUnsupportedTraceListenerMethod);
        }

        /// <summary>
        /// This prototype is not supported by SSTraceListener.
        /// An SSException is thrown to highlight that it has been called.
        /// </summary>
        public override void Write(object o, string s)
        {
            throw new SSException(
                string.Format(Messages.SSTTraceListenerUnsupportedPrototype, s),
                false,
                SSExceptionIdentifier.ELSUnsupportedTraceListenerMethod);
        }

        /// <summary>
        /// This prototype is not supported by SSTraceListener.
        /// An SSException is thrown to highlight that it has been called.
        /// </summary>
        public override void Write(string message, string category)
        {
            throw new SSException(
                string.Format(Messages.SSTTraceListenerUnsupportedPrototype, message),
                false,
                SSExceptionIdentifier.ELSUnsupportedTraceListenerMethod);
        }

        /// <summary>
        /// This prototype is not supported by SSTraceListener.
        /// An SSException is thrown to highlight that it has been called.
        /// </summary>
        public override void WriteLine(string s)
        {
            throw new SSException(
                string.Format(Messages.SSTTraceListenerUnsupportedPrototype, s),
                false,
                SSExceptionIdentifier.ELSUnsupportedTraceListenerMethod);
        }

        /// <summary>
        /// This prototype is not supported by SSTraceListener.
        /// An SSException is thrown to highlight that it has been called.
        /// </summary>
        public override void WriteLine(object o, string s)
        {
            throw new SSException(
                string.Format(Messages.SSTTraceListenerUnsupportedPrototype, s),
                false,
                SSExceptionIdentifier.ELSUnsupportedTraceListenerMethod);
        }

        /// <summary>
        /// This prototype is not supported by SSTraceListener.
        /// An SSException is thrown to highlight that it has been called.
        /// </summary>
        public override void WriteLine(object o)
        {
            throw new SSException(
                string.Format(Messages.SSTTraceListenerUnsupportedPrototype, string.Empty),
                false,
                SSExceptionIdentifier.ELSUnsupportedTraceListenerMethod);
        }

        /// <summary>
        /// This prototype is not fully supported by SSTraceListener.
        /// An SSException is thrown to highlight that it has been called.
        /// </summary>
        public override void WriteLine(string message, string category)
        {
            throw new SSException(
                string.Format(Messages.SSTTraceListenerUnsupportedPrototype, message),
                false,
                SSExceptionIdentifier.ELSUnsupportedTraceListenerMethod);
        }

        #endregion

        #endregion

    }
}
