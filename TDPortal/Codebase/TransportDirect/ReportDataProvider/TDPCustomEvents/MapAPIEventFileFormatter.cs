// *********************************************** 
// NAME             : MapAPIEventFileFormatter.cs
// AUTHOR           : Amit Patel
// DATE CREATED     : 12/11/2009 
// DESCRIPTION      : Formats map api events for publishing by file.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/MapAPIEventFileFormatter.cs-arc  $ 
//
//   Rev 1.1   Mar 19 2010 13:00:40   mmodi
//Added file Headers
//
// Added header as original author had failed to add

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
    /// <summary>
	/// Formats map api events for publishing by file.
	/// </summary>
    public class MapAPIEventFileFormatter : IEventFormatter
    {
        /// <summary>
		/// Default constructor.
		/// </summary>
		public MapAPIEventFileFormatter()
		{
		}

		/// <summary>
		/// Formats the given log event.
		/// </summary>
		/// <param name="logEvent">Log Event to format.</param>
		/// <returns>A formatted string representing the log event.</returns>
		public string AsString(LogEvent logEvent)
		{
			string output = String.Empty;

			if(logEvent is MapAPIEvent)
			{
				MapAPIEvent theMapEvent = (MapAPIEvent)logEvent;
				string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";
				string outputFormat = "TD-MAE {2}\tTimeSubmitted:[{0}]\tCommandCat:[{1}]";
				
				output = String.Format(outputFormat, theMapEvent.Submitted.ToString(dateTimeFormat), theMapEvent.CommandCategory.ToString(), theMapEvent.Time.ToString(dateTimeFormat));
			}
			return output;
		}
    }
}
