// ********************************************************* 
// NAME                 : RealTimeRoadEventFileFormatter.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 31/08/2011 
// DESCRIPTION  : File formatter class for real time car custom event
// ********************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/RealTimeRoadEventFileFormatter.cs-arc  $
//
//   Rev 1.0   Sep 02 2011 10:34:52   apatel
//Initial revision.
//Resolution for 5731: CCN 0548 - Real Time Information in Car

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
    /// <summary>
    /// File formatter class for real time car custom event
    /// </summary>
    public class RealTimeRoadEventFileFormatter: IEventFormatter
    {
        #region Private members

        // Custom datetime pattern based on ISO 8601, to resolution of milliseconds
        private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public RealTimeRoadEventFileFormatter()
        {

        }

        #endregion

        #region Public methods
        /// <summary>
        /// Formats the given log event.
        /// </summary>
        /// <param name="logEvent">Log Event to format.</param>
        /// <returns>A formatted string representing the log event.</returns>
        public string AsString(LogEvent logEvent)
        {
            StringBuilder output = new StringBuilder();

            if (logEvent is RealTimeRoadEvent)
            {
                RealTimeRoadEvent rtrce = (RealTimeRoadEvent)logEvent;

                output.Append("TD-RTRCE\t");

                output.Append(rtrce.Time.ToString(dateTimeFormat) + "\t");

                if (rtrce.SessionId != OperationalEvent.SessionIdUnassigned)
                {
                    output.Append("SessionID[" + rtrce.SessionId + "]\t");
                    output.Append("LoggedOn[" + rtrce.UserLoggedOn + "]\t");
                    output.Append("Type[" + rtrce.RealTimeRoadTypeOfEvent + "]\t");
                    output.Append("Found[" + rtrce.RealTimeFound + "]\t");
                }

                output.Append("Success[" + rtrce.Success + "]\t");
                output.Append("Started[" + rtrce.Submitted.ToString(dateTimeFormat) + "]\t");
            }

            return output.ToString();
        }

        #endregion
    }
}
