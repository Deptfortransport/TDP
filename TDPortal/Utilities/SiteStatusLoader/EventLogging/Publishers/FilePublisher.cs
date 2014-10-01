// *********************************************** 
// NAME                 : FilePublisher.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Publishes events to the file log
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/Publishers/FilePublisher.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:30:18   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using AO.Common;

namespace AO.EventLogging
{
    /// <summary>
    /// Publishes events to files.
    /// </summary>
    public class FilePublisher : IEventPublisher
    {
        private string identifier;
        private int rotation;
        private int currentRecordCount;
        private string baseFilepath;
        private string rotationFilepath;
        private int rotationSequence;

        private string fileDirectory;
        private string applicationId;
        private DateTime datetimeCreated;

        /// <summary>
        /// Gets identifier.
        /// </summary>
        public string Identifier
        {
            get { return identifier; }
        }

        /// <summary>
        /// Constructor for creating a publisher that writes event details to files.
        /// It is assumed that all parameters have been pre-validated.
        /// </summary>
        /// <param name="identifier">Identifier</param>
        /// <param name="rotation">Maximum number of records per file.</param>
        /// <param name="baseFilepath">Base filepath of file to publish to.</param>
        public FilePublisher(string identifier, int rotation, string baseFilepath, string directory, string applicationId, DateTime datetimeCreated)
        {
            this.identifier = identifier;
            this.rotation = rotation;
            this.baseFilepath = baseFilepath;
            this.rotationSequence = 0;
            UpdateRotationFilepath();
            currentRecordCount = 0;

            // variables used to generate a new filename
            this.fileDirectory = directory;
            this.applicationId = applicationId;
            this.datetimeCreated = datetimeCreated;
        }

        /// <summary>
        /// Writes the given log event to the file.
        /// </summary>
        /// <param name="logEvent">Log Event to write details for.</param>
        /// <exception cref="TransportDirect.Common.TDException">Log Event was not successfully written to the file.</exception>
        public void WriteEvent(LogEvent logEvent)
        {

            lock (this)  // support multithreading on a single file by locking
            {
                string formatString = String.Empty;

                try
                {
                    formatString = logEvent.FileFormatter.AsString(logEvent);
                    WriteToFile(formatString);
                }
                catch (UnauthorizedAccessException uae)
                {
                    // thrown if access to the file is denied

                    String message = String.Format(Messages.ELFilePublisherWriteEvent, formatString, this.rotationFilepath);

                    throw new SSException(message, uae, false, SSExceptionIdentifier.ELSFilePublisherWritingEvent);
                }
                catch (ArgumentNullException ane)
                {
                    // thrown if the path is null

                    String message = String.Format(Messages.ELFilePublisherWriteEvent, formatString, this.rotationFilepath);

                    throw new SSException(message, ane, false, SSExceptionIdentifier.ELSFilePublisherWritingEvent);
                }
                catch (DirectoryNotFoundException dnfe)
                {
                    // thrown if the directory to write to is not found

                    String message = String.Format(Messages.ELFilePublisherWriteEvent, formatString, this.rotationFilepath);

                    throw new SSException(message, dnfe, false, SSExceptionIdentifier.ELSFilePublisherWritingEvent);
                }
                catch (PathTooLongException ptle)
                {
                    String message = String.Format(Messages.ELFilePublisherWriteEvent, formatString, this.rotationFilepath);

                    throw new SSException(message, ptle, false, SSExceptionIdentifier.ELSFilePublisherWritingEvent);
                }
                catch (System.Security.SecurityException se)
                {
                    // Thrown if the caller does not have the required permission
                    // to access the file

                    String message = String.Format(Messages.ELFilePublisherWriteEvent, formatString, this.rotationFilepath);

                    throw new SSException(message, se, false, SSExceptionIdentifier.ELSFilePublisherWritingEvent);
                }
                catch (ArgumentException ae)
                {
                    // thrown if the path is empty

                    String message = String.Format(Messages.ELFilePublisherWriteEvent, formatString, this.rotationFilepath);

                    throw new SSException(message, ae, false, SSExceptionIdentifier.ELSFilePublisherWritingEvent);
                }
                catch (IOException ioe)
                {
                    // thrown if the path includes an incorrect or invalid syntax 
                    // for the file name, directory name, or volume label syntax.
                    // or if an I/O error occurs, such as the stream being closed.

                    String message = String.Format(Messages.ELFilePublisherWriteEvent, formatString, this.rotationFilepath);

                    throw new SSException(message, ioe, false, SSExceptionIdentifier.ELSFilePublisherWritingEvent);
                }
                catch (ObjectDisposedException ode)
                {
                    // thrown if an attempt to write to a closed stream is made

                    String message = String.Format(Messages.ELFilePublisherWriteEvent, formatString, this.rotationFilepath);

                    throw new SSException(message, ode, false, SSExceptionIdentifier.ELSFilePublisherWritingEvent);
                }
                catch (Exception e)
                {
                    String message = String.Format(Messages.ELFilePublisherWriteEvent, formatString, this.rotationFilepath);

                    throw new SSException(message, e, false, SSExceptionIdentifier.ELSFilePublisherWritingEvent);
                }
            }
        }

        /// <summary>
        /// Writes <c>stringToWrite</c> to the current file
        /// on a new line.
        /// </summary>
        /// <param name="fileEntry">String to write to the file.</param>
        private void WriteToFile(string fileEntry)
        {
            // Check if we've rolled over in to the next day, and create a new log file if so
            if (IsNewDay())
            {
                UpdateFilepath();
            }


            StreamWriter sw = new StreamWriter(rotationFilepath, true);

            sw.WriteLine(fileEntry);

            sw.Close();

            currentRecordCount++;

            if (currentRecordCount == rotation)
            {
                // rotation reached so update filepath
                UpdateRotationFilepath();
                currentRecordCount = 0;
            }
        }

        /// <summary>
        /// Method which increments the log file when it contains a configured number of reecords
        /// </summary>
        private void UpdateRotationFilepath()
        {
            this.rotationFilepath = String.Format("{0}-{1}.txt",
                                                  this.baseFilepath,
                                                  this.rotationSequence);
            this.rotationSequence++;
        }

        /// <summary>
        /// Method which creates a new File when the day changes
        /// </summary>
        private void UpdateFilepath()
        {
            string date = DateTime.Now.ToString
                ("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            string time = DateTime.Now.ToString
                ("HHmmss", System.Globalization.CultureInfo.InvariantCulture);

            // Reset the rotation sequence
            this.rotationSequence = 0;

            // Reset the current number of records in the file
            this.currentRecordCount = 0;

            // Commit the new filepath name
            this.rotationFilepath = (String.Format("{0}\\{1}-{2}{3}-{4}.txt", 
                this.fileDirectory, this.applicationId, date, time, this.rotationSequence));
        }

        /// <summary>
        /// Checks if midnight has passed since the file was created, if yes then updates datetTimeCreated value 
        /// and returns true
        /// </summary>
        private bool IsNewDay()
        {
            bool newDay = false;

            DateTime createdDay = new DateTime(datetimeCreated.Year, datetimeCreated.Month, datetimeCreated.Day);
            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            if (createdDay != today)
            {
                newDay = true;

                // Reset the datetime created
                this.datetimeCreated = DateTime.Now;
            }
            
            return newDay;
        }
    }
}
