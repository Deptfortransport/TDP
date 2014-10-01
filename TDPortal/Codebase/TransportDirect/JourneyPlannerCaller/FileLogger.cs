// *********************************************** 
// NAME                 : FileLogger.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 19/04/2010
// DESCRIPTION          : Static Class to allow logging messages to a file
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerCaller/FileLogger.cs-arc  $ 
//
//   Rev 1.0   Apr 20 2010 16:39:16   mmodi
//Initial revision.
//Resolution for 5515: Utility - Application to send request directly to CJP
//
//   Rev 1.0   Apr 19 2010 15:17:06   mmodi
//Initial revision.
//Resolution for 5515: Utility - Application to send request directly to CJP

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace JourneyPlannerCaller
{
    /// <summary>
    /// Singleton class to log messages to a file
    /// </summary>
    public class FileLogger
    {
        #region Private Static Fields

        private static readonly object instanceLock = new object();
        private static FileLogger instance = null;

        /// <summary>
        /// Gets the sinlgeton instance
        /// </summary>
        private static FileLogger Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new FileLogger();
                    }

                    return instance;
                }
            }
        }

        #endregion

        #region Public Static Fields

        /// <summary>
        /// Logs a message to the log file
        /// </summary>
        public static void LogMessage(string logMessage)
        {
            FileLogger logger = FileLogger.Instance;

            logger.Write(logMessage);
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// File path to log to
        /// </summary>
		private string baseFilepath;

		private int rotation;
		private int currentRecordCount;
		private string rotationFilepath;
		private int rotationSequence;

        /// <summary>
        /// Overall flag of whether to write to log file or not
        /// </summary>
        private bool logMessageSwitch = false;

        private const string FilePublisherWriteEvent =
            "Exception occured while attempting to write the log event [{0}] to the file [{1}]. Error message [{2}]";
        
        #endregion

        #region Constructor

        /// <summary>
        /// Singleton private constructor
        /// </summary>
        private FileLogger()
        {
            // Get the value where to load the Themes from, default is from Database
            string logMessageSwitch = ConfigurationManager.AppSettings["Logging.Switch"];
            string fileName = ConfigurationManager.AppSettings["Logging.File.Name"];
            string directoryPath = ConfigurationManager.AppSettings["Logging.File.Directory"];
            string rotationString = ConfigurationManager.AppSettings["Logging.File.Rotation"];

            #region Set log switch 

            bool log = false;

            if (Boolean.TryParse(logMessageSwitch, out log))
            {
                this.logMessageSwitch = log;
            }
            
            #endregion

            #region Set file path

            this.baseFilepath = GenerateFilepath(fileName, directoryPath);

            #endregion

            #region Set rotation

            int rotationInt = 0;
            this.rotation = 10000;
            
            if (Int32.TryParse(rotationString, out rotationInt))
            {
                // Make sure its a sensible value
                if (rotation > 10)
                {
                    this.rotation = rotationInt;
                }
            }

            this.rotationSequence = 0;
            UpdateRotationFilepath();
            this.currentRecordCount = 0;

            #endregion
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Generates a unique filepath based on the filename 
        /// format {ApplicationId}-{YYYYMMDD}{Time}
        /// Filename does not include an extension.
        /// </summary>
        /// <param name="directoryPath">Path to directory that will hold file.</param>
        /// <returns>Filepath.</returns>
        private string GenerateFilepath(string fileName, string directoryPath)
        {
            string date = DateTime.Now.ToString
                ("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            string time = DateTime.Now.ToString
                ("HHmmssfffff", System.Globalization.CultureInfo.InvariantCulture);
            
            return (String.Format("{0}\\{1}-{2}{3}", directoryPath, fileName, date, time));
        }

        /// <summary>
        /// Updates the filepath rotation
        /// </summary>
        private void UpdateRotationFilepath()
        {
            this.rotationFilepath = String.Format("{0}-{1}.txt",
                                                  this.baseFilepath,
                                                  this.rotationSequence);
            this.rotationSequence++;
        }

        /// <summary>
        /// Writes a string to the current file
        /// on a new line.
        /// </summary>
        /// <param name="fileEntry">String to write to the file.</param>
        private void WriteToFile(string fileEntry)
        {
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

        #endregion

        #region Public Methods

        /// <summary>
        /// Writes the message to the log file, if switch flag is set to true
        /// </summary>
        private void Write(string logMessage)
        {
            lock (this)  // support multithreading on a single file by locking
            {
                string formattedMessage = logMessage;

                try
                {
                    if (logMessageSwitch)
                    {
                        // Add the date time to the message
                        string datetime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                        
                        formattedMessage = string.Format("{0}\t{1}", datetime, logMessage);

                        WriteToFile(formattedMessage);
                    }
                }
                catch (UnauthorizedAccessException uae)
                {
                    // thrown if access to the file is denied

                    String message = String.Format(
                        FilePublisherWriteEvent, formattedMessage, this.rotationFilepath, uae.Message);

                    Console.WriteLine(message);
                }
                catch (ArgumentNullException ane)
                {
                    // thrown if the path is null

                    String message = String.Format(
                        FilePublisherWriteEvent, formattedMessage, this.rotationFilepath, ane.Message);

                    Console.WriteLine(message);
                }
                catch (DirectoryNotFoundException dnfe)
                {
                    // thrown if the directory to write to is not found

                    String message = String.Format(
                        FilePublisherWriteEvent, formattedMessage, this.rotationFilepath, dnfe.Message);

                    Console.WriteLine(message);
                }
                catch (PathTooLongException ptle)
                {
                    String message = String.Format(
                        FilePublisherWriteEvent, formattedMessage, this.rotationFilepath, ptle.Message);

                    Console.WriteLine(message);
                }
                catch (System.Security.SecurityException se)
                {
                    // Thrown if the caller does not have the required permission
                    // to access the file

                    String message = String.Format(
                        FilePublisherWriteEvent, formattedMessage, this.rotationFilepath, se.Message);

                    Console.WriteLine(message);
                }
                catch (ArgumentException ae)
                {
                    // thrown if the path is empty

                    String message = String.Format(
                        FilePublisherWriteEvent, formattedMessage, this.rotationFilepath, ae.Message);

                    Console.WriteLine(message);
                }
                catch (IOException ioe)
                {
                    // thrown if the path includes an incorrect or invalid syntax 
                    // for the file name, directory name, or volume label syntax.
                    // or if an I/O error occurs, such as the stream being closed.

                    String message = String.Format(
                        FilePublisherWriteEvent, formattedMessage, this.rotationFilepath, ioe.Message);

                    Console.WriteLine(message);
                }
                catch (ObjectDisposedException ode)
                {
                    // thrown if an attempt to write to a closed stream is made

                    String message = String.Format(
                        FilePublisherWriteEvent, formattedMessage, this.rotationFilepath, ode.Message);

                    Console.WriteLine(message);
                }
                catch (Exception e)
                {
                    String message = String.Format(
                        FilePublisherWriteEvent, formattedMessage, this.rotationFilepath, e.Message);

                    Console.WriteLine(message);
                }
            }
        }

        #endregion

    }
}
