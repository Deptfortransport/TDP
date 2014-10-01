// *********************************************** 
// NAME             : FilePublisher.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 10 Feb 2011
// DESCRIPTION  	: Publisher to publish events to files
// ************************************************

using System;
using System.IO;

namespace TDP.Common.EventLogging
{
    /// <summary>
    /// Publishes events to files.
    /// </summary>
    public class FilePublisher : IEventPublisher
    {
        #region Private Fields
        private string identifier;
        private int rotation;
        private int currentRecordCount;
        private string baseFilepath;
        private string rotationFilepath;
        private int rotationSequence;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for creating a publisher that writes event details to files.
        /// It is assumed that all parameters have been pre-validated.
        /// </summary>
        /// <param name="identifier">Identifier</param>
        /// <param name="rotation">Maximum number of records per file.</param>
        /// <param name="baseFilepath">Base filepath of file to publish to.</param>
        public FilePublisher(string identifier, int rotation, string baseFilepath)
        {
            this.identifier = identifier;
            this.rotation = rotation;
            this.baseFilepath = baseFilepath;
            this.rotationSequence = 0;
            UpdateRotationFilepath();
            currentRecordCount = 0;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets identifier.
        /// </summary>
        public string Identifier
        {
            get { return identifier; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Writes the given log event to the file.
        /// </summary>
        /// <param name="logEvent">Log Event to write details for.</param>
        /// <exception cref="TDP.Common.TDPException">Log Event was not successfully written to the file.</exception>
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

                    String message = String.Format(
                        Messages.FilePublisherWriteEvent, formatString, this.rotationFilepath);

                    throw new TDPException(message, uae, false, TDPExceptionIdentifier.ELSFilePublisherWritingEvent);
                }
                catch (ArgumentNullException ane)
                {
                    // thrown if the path is null

                    String message = String.Format(
                        Messages.FilePublisherWriteEvent, formatString, this.rotationFilepath);

                    throw new TDPException(message, ane, false, TDPExceptionIdentifier.ELSFilePublisherWritingEvent);
                }
                catch (DirectoryNotFoundException dnfe)
                {
                    // thrown if the directory to write to is not found

                    String message = String.Format(
                        Messages.FilePublisherWriteEvent, formatString, this.rotationFilepath);

                    throw new TDPException(message, dnfe, false, TDPExceptionIdentifier.ELSFilePublisherWritingEvent);
                }
                catch (PathTooLongException ptle)
                {
                    String message = String.Format(
                        Messages.FilePublisherWriteEvent, formatString, this.rotationFilepath);

                    throw new TDPException(message, ptle, false, TDPExceptionIdentifier.ELSFilePublisherWritingEvent);
                }
                catch (System.Security.SecurityException se)
                {
                    // Thrown if the caller does not have the required permission
                    // to access the file

                    String message = String.Format(
                        Messages.FilePublisherWriteEvent, formatString, this.rotationFilepath);

                    throw new TDPException(message, se, false, TDPExceptionIdentifier.ELSFilePublisherWritingEvent);
                }
                catch (ArgumentException ae)
                {
                    // thrown if the path is empty

                    String message = String.Format(
                        Messages.FilePublisherWriteEvent, formatString, this.rotationFilepath);

                    throw new TDPException(message, ae, false, TDPExceptionIdentifier.ELSFilePublisherWritingEvent);
                }
                catch (IOException ioe)
                {
                    // thrown if the path includes an incorrect or invalid syntax 
                    // for the file name, directory name, or volume label syntax.
                    // or if an I/O error occurs, such as the stream being closed.

                    String message = String.Format(
                        Messages.FilePublisherWriteEvent, formatString, this.rotationFilepath);

                    throw new TDPException(message, ioe, false, TDPExceptionIdentifier.ELSFilePublisherWritingEvent);
                }
                catch (ObjectDisposedException ode)
                {
                    // thrown if an attempt to write to a closed stream is made

                    String message = String.Format(
                        Messages.FilePublisherWriteEvent, formatString, this.rotationFilepath);

                    throw new TDPException(message, ode, false, TDPExceptionIdentifier.ELSFilePublisherWritingEvent);
                }
                catch (Exception e)
                {
                    String message = String.Format(
                        Messages.FilePublisherWriteEvent, formatString, this.rotationFilepath);

                    throw new TDPException(message, e, false, TDPExceptionIdentifier.ELSFilePublisherWritingEvent);
                }
            }
        }

        #endregion

        #region Private Methods
        
        /// <summary>
        /// Writes <c>stringToWrite</c> to the current file
        /// on a new line.
        /// </summary>
        /// <param name="fileEntry">String to write to the file.</param>
        private void WriteToFile(string fileEntry)
        {
            using (StreamWriter sw = new StreamWriter(rotationFilepath, true))
            {

                sw.WriteLine(fileEntry);
                
                currentRecordCount++;

                if (currentRecordCount == rotation)
                {
                    // rotation reached so update filepath
                    UpdateRotationFilepath();
                    currentRecordCount = 0;
                }
            }
        }

        /// <summary>
        /// Updates file path for rotational file
        /// </summary>
        private void UpdateRotationFilepath()
        {
            this.rotationFilepath = String.Format("{0}-{1}.txt",
                                                  this.baseFilepath,
                                                  this.rotationSequence);
            this.rotationSequence++;
        }
        #endregion

    }
}
