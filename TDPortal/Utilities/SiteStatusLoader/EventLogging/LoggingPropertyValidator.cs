// *********************************************** 
// NAME                 : LoggingPropertyValidator.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Property validator for logging properties
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/LoggingPropertyValidator.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:27:18   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Messaging;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

using AO.Common;
using AO.Properties;

using PropertyService = AO.Properties.Properties;

namespace AO.EventLogging
{
    /// <summary>
    /// Utility class used to validate logging specific properties.
    /// Derives from PropertyValidator which provides general
    /// validation methods.
    /// </summary>
    public class LoggingPropertyValidator : PropertyValidator
    {
        /// <summary>
        /// Constructor used to create class based on properties.
        /// </summary>
        /// <param name="properties">Properties that are used to perform validation against.</param>
        public LoggingPropertyValidator(PropertyService properties) : base(properties)
        { 
        }

        /// <summary>
        /// Used to validate a property with a given <c>key</c>.
        /// </summary>
        /// <param name="key">Key of property to validate.</param>
        /// <param name="errors">Holds validation errors if any.</param>
        /// <returns><c>true</c> if property is valid otherwise <c>false</c>.</returns>
        override public bool ValidateProperty(string key, ArrayList errors)
        {
            // A validation method is defined for every key that may be passed.

            if (key == Keys.QueuePublishers)
                return ValidateQueuePublishers(errors);
            else if (key == Keys.FilePublishers)
                return ValidateFilePublishers(errors);
            else if (key == Keys.EventLogPublishers)
                return ValidateEventLogPublishers(errors);
            else if (key == Keys.OperationalTraceLevel)
                return ValidateOperationalEventTraceLevel(errors);
            else if (key == Keys.DefaultPublisher)
                return ValidateDefaultPublisher(errors);
            else if (key == Keys.ConsolePublishers)
                return ValidateConsolePublishers(errors);
            else if (key == Keys.CustomEventsLevel)
                return ValidateCustomEventsLevel(errors);
            else if (key == Keys.CustomEvents)
                return ValidateCustomEvents(errors);
            else
            {
                string message = String.Format(Messages.ELPVLoggingPropertyValidatorKeyBad, key);
                throw new SSException(message, false, SSExceptionIdentifier.ELSLoggingPropertyValidatorUnknownKey);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        private bool ValidateOperationalEventTraceLevel(ArrayList errors)
        {
            int errorsBefore = errors.Count;

            // NB Can take any value in SSTraceLevel except Undefined
            ValidateEnumProperty(Keys.OperationalTraceLevel,
                                 typeof(SSTraceLevel), Optionality.Mandatory, errors);

            if (String.Compare(properties[Keys.OperationalTraceLevel], "Undefined") == 0)
            {
                errors.Add(String.Format(Messages.PVPropertyValueBad,
                                         Keys.OperationalTraceLevel,
                                         properties[Keys.OperationalTraceLevel],
                                         "Value cannot take value Undefined."));

            }

            return (errorsBefore == errors.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool QueueExists(string path)
        {
            // NB. Not possible to use the Exists method since will not work for remote private queues.
            // Instead use CanWrite after creating a queue object based on the path.
            bool valid = true;
            MessageQueue queue = null;

            try
            {
                queue = new MessageQueue(path);
            }
            catch (ArgumentException)
            {
                valid = false;
                queue = null;
            }

            if (queue != null)
            {
                try
                {
                    valid = queue.CanWrite;
                }
                catch (Exception)
                {
                    // MS do not document exact exceptions for CanWrite so catch all
                    valid = false;
                }
            }

            return valid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        private bool ValidateQueuePublishers(ArrayList errors)
        {
            if (!ValidateExistence(Keys.QueuePublishers, Optionality.Mandatory, errors))
                return false;

            string[] publisherIDs = null;
            string publishers = properties[Keys.QueuePublishers];
            if (publishers.Length != 0) // valid to have no publishers
                publisherIDs = publishers.Split(' ');
            else
                publisherIDs = new String[0];
            StringBuilder key = new StringBuilder(50);
            int errorsBefore = errors.Count;

            foreach (string publisherID in publisherIDs)
            {
                key.Length = 0;
                key.Append(String.Format(Keys.QueuePublisherPath, publisherID));

                if (ValidateExistence(key.ToString(), Optionality.Mandatory, errors))
                {
                    if (!QueueExists(properties[key.ToString()]))
                    {
                        errors.Add(String.Format(Messages.ELPVQueuePublisherMSMQBad,
                                                 properties[key.ToString()],
                                                 key.ToString()));
                    }
                }

                key.Length = 0;
                key.Append(String.Format(Keys.QueuePublisherPriority, publisherID));

                if (ValidateExistence(key.ToString(), Optionality.Mandatory, errors))
                    ValidateEnumProperty(key.ToString(),
                                         typeof(MessagePriority),
                                         Optionality.Mandatory, errors);




                key.Length = 0;
                key.Append(String.Format(Keys.QueuePublisherDelivery, publisherID));

                if (ValidateExistence(key.ToString(), Optionality.Mandatory, errors))
                {
                    if ((String.Compare(properties[key.ToString()], "Express") != 0) &&
                         (String.Compare(properties[key.ToString()], "Recoverable") != 0))
                    {
                        errors.Add(String.Format(Messages.ELPVQueuePublisherDeliveryBad,
                                                           properties[key.ToString()],
                                                           key.ToString()));
                    }
                }


            }

            return (errorsBefore == errors.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        private bool ValidateFilePublishers(ArrayList errors)
        {
            if (!ValidateExistence(Keys.FilePublishers, Optionality.Mandatory, errors))
                return false;

            string[] publisherIDs = null;
            string publishers = properties[Keys.FilePublishers];
            if (publishers.Length != 0) // valid to have no publishers
                publisherIDs = publishers.Split(' ');
            else
                publisherIDs = new String[0];

            StringBuilder key = new StringBuilder(50);
            int errorsBefore = errors.Count;

            foreach (string publisherID in publisherIDs)
            {
                key.Length = 0;
                key.Append(String.Format(Keys.FilePublisherDirectory, publisherID));

                if (ValidateExistence(key.ToString(), Optionality.Mandatory, errors))
                {
                    if (!Directory.Exists(properties[key.ToString()]))
                        errors.Add(String.Format(Messages.ELPVFilePublisherDirectoryBad, properties[key.ToString()], key.ToString()));
                }

                key.Length = 0;
                key.Append(String.Format(Keys.FilePublisherRotation, publisherID));

                if (ValidateExistence(key.ToString(), Optionality.Mandatory, errors))
                {
                    if (ValidateLength(key.ToString(), 1, errors))
                    {
                        CultureInfo cultureInfo = new CultureInfo("en-US");
                        string property = properties[key.ToString()];

                        int rotation = int.Parse(property, cultureInfo);

                        if (rotation <= 0)
                            errors.Add(String.Format(Messages.ELPVFilePublisherRotationBad, properties[key.ToString()], key.ToString()));

                    }
                }

            }

            return (errorsBefore == errors.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        private bool ValidateEventLogPublishers(ArrayList errors)
        {
            if (!ValidateExistence(Keys.EventLogPublishers, Optionality.Mandatory, errors))
                return false;

            string[] publisherIDs = null;
            string publishers = properties[Keys.EventLogPublishers];
            if (publishers.Length != 0) // valid to have no publishers
                publisherIDs = publishers.Split(' ');
            else
                publisherIDs = new String[0];
            StringBuilder key1 = new StringBuilder(50);
            StringBuilder key2 = new StringBuilder(50);
            bool nameValid = false;
            bool machineValid = false;
            int errorsBefore = errors.Count;

            foreach (string publisherID in publisherIDs)
            {
                nameValid = false;
                machineValid = false;

                key1.Length = 0;
                key1.Append(String.Format(Keys.EventLogPublisherName, publisherID));

                if (ValidateExistence(key1.ToString(), Optionality.Mandatory, errors))
                {
                    if (properties[key1.ToString()].Equals("Application"))
                    {
                        nameValid = true; // configured to use standard Application Event Log
                    }
                    else
                    {
                        if (ValidateLength(key1.ToString(), 1, 8, errors))
                            nameValid = true; // configured to use a custom event log
                    }
                }

                key2.Length = 0;
                key2.Append(String.Format(Keys.EventLogPublisherMachine, publisherID));

                if (ValidateExistence(key2.ToString(), Optionality.Mandatory, errors))
                {
                    if (ValidateLength(key2.ToString(), 1, errors))
                        machineValid = true;
                }

                if (machineValid && nameValid)
                {
                    try
                    {
                        if (!EventLog.Exists(properties[key1.ToString()], properties[key2.ToString()]))
                        {
                            errors.Add(String.Format(Messages.ELPVEventLogPublisherEventLogBad,
                                properties[key1.ToString()],
                                properties[key2.ToString()],
                                key1.ToString(), key2.ToString()));

                        }
                    }
                    catch (Exception exception)
                    {
                        // MS have not documented all possible exceptions so catch all	
                        errors.Add(String.Format(Messages.ELPVEventLogPublisherEventLogNotFound,
                                   exception.Message,
                                   key1.ToString(), key2.ToString()));
                    }
                }


                key1.Length = 0;
                key1.Append(String.Format(Keys.EventLogPublisherSource, publisherID));
                ValidateExistence(key1.ToString(), Optionality.Mandatory, errors);

            }

            return (errorsBefore == errors.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        private bool ValidateDefaultPublisher(ArrayList errors)
        {
            int errorsBefore = errors.Count;

            if (ValidateExistence(Keys.DefaultPublisher, Optionality.Mandatory, errors))
            {
                ValidateLength(Keys.DefaultPublisher, 1, errors);
            }


            return (errorsBefore == errors.Count);
        }

        /// <summary>
        /// Validates that the base class of a type matches a given type.
        /// </summary>
        private void ValidateBaseType(string key, Type requiredSubclass,
                                      Assembly assembly, ArrayList errors)
        {
            string classTypeName = properties[key];
            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                if (type.Name == classTypeName)
                {
                    if (!type.IsSubclassOf(requiredSubclass))
                    {
                        errors.Add(String.Format(Messages.ELPVIncorrectClassType,
                                   classTypeName,
                                   key,
                                   requiredSubclass.FullName));
                    }

                    break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        private bool ValidateConsolePublishers(ArrayList errors)
        {
            if (!ValidateExistence(Keys.ConsolePublishers, Optionality.Mandatory, errors))
                return false;

            string[] publisherIDs = null;
            string publishers = properties[Keys.ConsolePublishers];

            if (publishers.Length != 0) // valid to have no publishers
                publisherIDs = publishers.Split(' ');
            else
                publisherIDs = new String[0];
            StringBuilder key = new StringBuilder(50);
            int errorsBefore = errors.Count;

            foreach (string publisherID in publisherIDs)
            {
                key.Length = 0;
                key.Append(String.Format(Keys.ConsolePublisherStream, publisherID));

                string property = properties[key.ToString()];

                if (ValidateExistence(key.ToString(), Optionality.Mandatory, errors))
                {
                    if ((String.Compare(property, Keys.ConsolePublisherErrorStream) != 0) &&
                         (String.Compare(property, Keys.ConsolePublisherOutputStream) != 0))
                    {
                        string usage = Keys.ConsolePublisherErrorStream + "|" + Keys.ConsolePublisherOutputStream;

                        errors.Add(String.Format(Messages.PVPropertyValueBad,
                                                  key.ToString(), property, usage));

                    }
                }

            }

            return (errorsBefore == errors.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        private bool ValidateCustomEventsLevel(ArrayList errors)
        {
            int errorsBefore = errors.Count;

            if (ValidateExistence(Keys.CustomEventsLevel, Optionality.Mandatory, errors))
            {
                ValidateEnumProperty(Keys.CustomEventsLevel,
                                     typeof(CustomEventLevel),
                                     Optionality.Mandatory, errors);

                string property = properties[Keys.CustomEventsLevel];

                if (String.Compare(property, "Undefined") == 0)
                {
                    errors.Add(String.Format(Messages.PVPropertyValueBad,
                                             Keys.CustomEventsLevel, property,
                                             "Value cannot take value Undefined."));

                }
            }

            return (errorsBefore == errors.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        private bool ValidateCustomEvents(ArrayList errors)
        {
            int errorsBefore = errors.Count;

            if (ValidateExistence(Keys.CustomEvents, Optionality.Mandatory, errors))
            {
                string[] eventIds = null;
                string events = properties[Keys.CustomEvents];
                if (events.Length != 0) // valid to have no custom events
                    eventIds = events.Split(' ');
                else
                    eventIds = new String[0];

                foreach (string eventId in eventIds)
                    ValidateCustomEvent(eventId, errors);

            }

            return (errorsBefore == errors.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="errors"></param>
        private void ValidateCustomEvent(string eventId, ArrayList errors)
        {
            string nameKey = String.Format(Keys.CustomEventName, eventId);
            string assemblyKey = String.Format(Keys.CustomEventAssembly, eventId);
            string traceKey = String.Format(Keys.CustomEventLevel, eventId);
            bool nameValid = false;
            bool assemblyNameValid = false;

            if (ValidateExistence(nameKey, Optionality.Mandatory, errors))
            {
                if (ValidateLength(nameKey, 1, errors))
                {
                    nameValid = true;
                }
            }

            if (ValidateExistence(assemblyKey, Optionality.Mandatory, errors))
            {
                if (ValidateLength(assemblyKey, 1, errors))
                {
                    assemblyNameValid = true;
                }
            }

            if (nameValid && assemblyNameValid)
            {
                Assembly assembly = ValidateAssembly(assemblyKey, errors);

                if (assembly != null)
                {
                    if (ValidateClassExists(nameKey, assembly, errors))
                        ValidateBaseType(nameKey, typeof(CustomEvent), assembly, errors);
                }
            }

            if (ValidateExistence(traceKey, Optionality.Mandatory, errors))
            {
                string property = properties[traceKey];

                ValidateEnumProperty(traceKey,
                                     typeof(CustomEventLevel),
                                     Optionality.Mandatory, errors);

                if (String.Compare(property, "Undefined") == 0)
                {
                    errors.Add(String.Format(Messages.PVPropertyValueBad,
                                             traceKey, property,
                                             "Value cannot take value Undefined."));

                }
            }

        }
    }
}
