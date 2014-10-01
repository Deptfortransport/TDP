// *********************************************** 
// NAME                 : EventPublisherGroup.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Creates the group of publishers
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/Publishers/EventPublisherGroup.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:30:18   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Messaging;
using System.Text;

using AO.Properties;

using PropertyService = AO.Properties.Properties;

namespace AO.EventLogging
{
    /// <summary>
    /// Factory class that creates a group of event publishers.
    /// </summary>
    class EventPublisherGroup : PublisherGroup
    {

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="properties">Properties used to create publishers.</param>
        public EventPublisherGroup(PropertyService properties)
            : base(properties)
        { }

        /// <summary>
        /// Main factory method for creating publishers based on properties.
        /// </summary>
        /// <param name="errors">Stores errors occuring during validation and creation.</param>
        override public void CreatePublishers(ArrayList errors)
        {
            int errorsBeforeValidation = errors.Count;

            validator.ValidateProperty(Keys.QueuePublishers, errors);
            validator.ValidateProperty(Keys.FilePublishers, errors);
            validator.ValidateProperty(Keys.EventLogPublishers, errors);
            validator.ValidateProperty(Keys.ConsolePublishers, errors);

            if (errors.Count == errorsBeforeValidation)
            {
                CreateQueuePublishers(errors);
                CreateFilePublishers(errors);
                CreateEventLogPublishers(errors);
                CreateConsolePublishers(errors);
            }
        }

        /// <summary>
        /// Setup the EventLogPublisher
        /// </summary>
        /// <param name="errors"></param>
        private void CreateEventLogPublishers(ArrayList errors)
        {
            string[] ids = null;
            string publisherids = properties[Keys.EventLogPublishers];
            if (publisherids.Length != 0) // valid to have no publishers
                ids = publisherids.Split(' ');
            else
                ids = new String[0];

            try
            {
                foreach (string id in ids)
                {
                    publishers.Add(
                        new EventLogPublisher(id,
                        properties[String.Format(Keys.EventLogPublisherName, id)],
                        properties[String.Format(Keys.EventLogPublisherSource, id)],
                        properties[String.Format(Keys.EventLogPublisherMachine, id)]
                                              )
                        );
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                throw ex;
            }

        }

        /// <summary>
        /// Setup the ConsolePublisher
        /// </summary>
        /// <param name="errors"></param>
        private void CreateConsolePublishers(ArrayList errors)
        {
            string[] ids = null;
            string publisherids = properties[Keys.ConsolePublishers];
            if (publisherids.Length != 0) // valid to have no publishers
                ids = publisherids.Split(' ');
            else
                ids = new String[0];

            try
            {
                foreach (string id in ids)
                {
                    publishers.Add(new ConsolePublisher(id, properties[String.Format(Keys.ConsolePublisherStream, id)]));
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                throw ex;
            }

        }

        /// <summary>
        /// Generates a unique filepath based on the filename 
        /// format {ApplicationId}-{YYYYMMDD}{Time}
        /// Filename does not include an extension.
        /// </summary>
        /// <param name="directoryPath">Path to directory that will hold file.</param>
        /// <returns>Filepath.</returns>
        private string GenerateFilepath(string directoryPath)
        {
            string date = DateTime.Now.ToString
                ("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            string time = DateTime.Now.ToString
                ("HHmmss", System.Globalization.CultureInfo.InvariantCulture);
            string applicationId = this.properties.ApplicationID;

            return (String.Format("{0}\\{1}-{2}{3}", directoryPath, applicationId, date, time));
        }

        /// <summary>
        /// Setup the FilePublisher
        /// </summary>
        /// <param name="errors"></param>
        private void CreateFilePublishers(ArrayList errors)
        {
            string[] ids = null;
            string publisherids = properties[Keys.FilePublishers];
            if (publisherids.Length != 0) // valid to have no publishers
                ids = publisherids.Split(' ');
            else
                ids = new String[0];

            CultureInfo cultureInfo = new CultureInfo("en-US");

            try
            {
                foreach (string id in ids)
                {
                    int rotation = int.Parse(properties[String.Format(Keys.FilePublisherRotation, id)], cultureInfo);

                    // Generate the initial file path, this will be for today and the FilePublisher.cs will create
                    // a new filepath when the Day changes
                    string directoryPath = properties[String.Format(Keys.FilePublisherDirectory, id)];
                    string filePath = GenerateFilepath(directoryPath);
                    string applicationId = this.properties.ApplicationID;

                    publishers.Add(new FilePublisher(id, rotation, filePath, directoryPath, applicationId, DateTime.Now));
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                throw ex;
            }

        }

        /// <summary>
        /// Setup the QueuePublisher
        /// </summary>
        /// <param name="errors"></param>
        private void CreateQueuePublishers(ArrayList errors)
        {
            bool recoverable;
            string[] ids = null;
            string publisherids = properties[Keys.QueuePublishers];
            if (publisherids.Length != 0) // valid to have no publishers
                ids = publisherids.Split(' ');
            else
                ids = new String[0];

            try
            {
                foreach (string id in ids)
                {
                    if (properties[String.Format(Keys.QueuePublisherDelivery, id)] == "Express")
                        recoverable = false;
                    else
                        recoverable = true;

                    publishers.Add(
                        new QueuePublisher(id,
                        (MessagePriority)PropertyValidator.StringToEnum(typeof(MessagePriority),
                        properties[String.Format(Keys.QueuePublisherPriority, id)]),
                        properties[String.Format(Keys.QueuePublisherPath, id)],
                        recoverable
                        )
                        );
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                throw ex;
            }

        }

    }
}
