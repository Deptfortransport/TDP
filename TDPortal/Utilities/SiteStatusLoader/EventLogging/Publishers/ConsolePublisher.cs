// *********************************************** 
// NAME                 : ConsolePublisher.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Publishes events to the console
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/Publishers/ConsolePublisher.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:30:18   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;
using AO.Common;

namespace AO.EventLogging
{
    /// <summary>
    /// Publishes events to the console.
    /// </summary>
    public class ConsolePublisher : IEventPublisher
    {
        private string identifier;
        private string streamSetting;

        /// <summary>
        /// Gets the identifier of the ConsolePublisher.
        /// </summary>
        public string Identifier
        {
            get { return identifier; }
        }

        /// <summary>
        /// Create a publisher that sends event details to the console.
        /// It is assumed that all parameters have been pre-validated.
        /// </summary>
        /// <param name="identifier">Unique identifier for console publishers.</param>
        /// <param name="streamSetting">Takes values 'Error' or 'Out' to indicate type of console stream to publish to.</param>
        public ConsolePublisher(string identifier, string streamSetting)
        {
            this.identifier = identifier;
            this.streamSetting = streamSetting;
        }

        /// <summary>
        /// Writes a log event to the console.
        /// </summary>
        /// <param name="logEvent">Log Event to write details for.</param>
        public void WriteEvent(LogEvent logEvent)
        {
            try
            {
                string formatString = logEvent.ConsoleFormatter.AsString(logEvent);

                if (streamSetting == Keys.ConsolePublisherOutputStream)
                {
                    Console.Out.WriteLine(formatString);
                }
                else if (streamSetting == Keys.ConsolePublisherErrorStream)
                {
                    Console.Error.WriteLine(formatString);
                }
            }
            catch (System.IO.IOException ioe)
            {
                string message = Messages.ELConsolePublisherWriteEvent;
                throw new SSException(message, ioe, false, SSExceptionIdentifier.ELSConsolePublisherWritingEvent);
            }
            catch (Exception e)
            {
                string message = Messages.ELConsolePublisherWriteEvent;
                throw new SSException(message, e, false, SSExceptionIdentifier.ELSConsolePublisherWritingEvent);
            }

        }
    }
}
