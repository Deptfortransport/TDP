// *********************************************** 
// NAME         : EventLogOutput.cs
// AUTHOR       : Mitesh Modi
// DATE CREATED : 02/06/2011
// DESCRIPTION  : Class to write events to the Event Log
// ************************************************ 

using System;
using System.Diagnostics;
using AO.HttpRequestValidatorCommon;
using Microsoft.Web.Administration;

namespace AO.HttpRequestValidator
{
    public class EventLogOutput
    {
        #region Private members

        private static EventLogOutput instance = null;

        private EventLog eventLog;
        private string messagePrefix;           // Prefix to append to messages added to the event log
        private bool eventLogSetUpOk = false;   // Indicates if event log was setup correctly

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        private EventLogOutput(string name, string source, string machine, string prefix)
        {
            SetupEventLog(name, source, machine, prefix);
        }

        #endregion

        #region Instance method

        /// <summary>
        /// Returns the singleton instance of the EventLogOutput object
        /// </summary>
        public static EventLogOutput Instance(string name, string source, string machine, string prefix)
        {
            if (instance == null)
            {
                instance = new EventLogOutput(name, source, machine, prefix);
            }

            return instance;
        }

        #endregion

        #region Public method

        /// <summary>
        /// Writes event to the event log
        /// </summary>
        public void WriteEvent(string message, EventLogEntryType eventType)
        {
            lock (this)
            {
                try
                {
                    if (eventLogSetUpOk)
                    {
                        EventLogEntryType entryType = eventType;
                        eventLog.WriteEntry(messagePrefix + message, entryType);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(String.Format(
                        "Exception occured when publishing the event to the Event Log:- Event Log Name:[{0}] Event Log Source:[{1}] Event Log Machine:[{2}] Exception[{3}]",
                        eventLog.Log,
                        eventLog.Source,
                        eventLog.MachineName,
                        e.Message + e.StackTrace));
                }
            }
        }

        #endregion

        #region Private method

        /// <summary>
        /// Sets up the file to write the status updates to
        /// </summary>
        private void SetupEventLog(string name, string source, string machine, string prefix)
        {
            try
            {
                // Create a source for the event log if it does not already have one.
                if (!EventLog.SourceExists(source, machine))
                {
                    EventSourceCreationData sourceData = new EventSourceCreationData(source, name);
                    EventLog.CreateEventSource(sourceData);
                }

                // Associate EventLog instance with publisher properties.
                eventLog = new EventLog(name, machine, source);

                if (!string.IsNullOrEmpty(prefix))
                {
                    messagePrefix = prefix + "\r\n";
                }
                else
                {
                    messagePrefix = string.Empty;
                }

                // Set the ok flag to allow future calls to update file to be done.
                eventLogSetUpOk = true;
            }
            catch
            {
                eventLogSetUpOk = false;

                // Throw so caller can handle, event log must be correctly setup
                throw;
            }
        }

        #endregion
    }
}
