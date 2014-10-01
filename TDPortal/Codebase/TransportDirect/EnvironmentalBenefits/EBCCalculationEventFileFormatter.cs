// *********************************************** 
// NAME			: EBCCalculationEventFileFormatter.cs
// AUTHOR		: Mark Turner
// DATE CREATED	: 29/09/2009
// DESCRIPTION	: Class which defines a custom event for logging an EBC event
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnvironmentalBenefits/EBCCalculationEventFileFormatter.cs-arc  $
//
//   Rev 1.0   Oct 06 2009 13:58:42   mmodi
//Initial revision.
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.EnvironmentalBenefits
{
    /// <summary>
    /// Class which defines a custom event for logging an EBC event
    /// </summary>
    public class EBCCalculationEventFileFormatter : IEventFormatter
    {
        #region Private members

        // Custom datetime pattern based on ISO 8601, to resolution of milliseconds
        private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public EBCCalculationEventFileFormatter()
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

            if (logEvent is EBCCalculationEvent)
            {
                EBCCalculationEvent ebcce = (EBCCalculationEvent)logEvent;

                output.Append("TD-EBCCE\t");

                output.Append(ebcce.Time.ToString(dateTimeFormat) + "\t");

                if (ebcce.SessionId != OperationalEvent.SessionIdUnassigned)
                {
                    output.Append("SessionID[" + ebcce.SessionId + "]\t");
                    output.Append("LoggedOn[" + ebcce.UserLoggedOn + "]\t");
                }

                output.Append("Success[" + ebcce.Success + "]\t");
                output.Append("Started[" + ebcce.Submitted.ToString(dateTimeFormat) + "]\t");
            }

            return output.ToString();
        }

        #endregion
    }
}

