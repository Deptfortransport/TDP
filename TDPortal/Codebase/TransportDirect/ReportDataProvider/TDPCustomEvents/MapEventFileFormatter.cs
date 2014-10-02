// ********************************************************* 
// NAME                 : MapEventFileFormatter.cs
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 18/12/2003 
// DESCRIPTION  : File formatter class for map custom
// events.
// ********************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/MapEventFileFormatter.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:28   mturner
//Initial revision.
//
//   Rev 1.1   Dec 18 2003 16:03:18   kcheung
//Corrected the Creation Date in Header.
//
//   Rev 1.0   Dec 18 2003 15:45:12   kcheung
//Initial Revision

using System;
using System.Globalization;

using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Formats map events for publishing by file.
	/// </summary>
	public class MapEventFileFormatter : IEventFormatter
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public MapEventFileFormatter()
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

			if(logEvent is MapEvent)
			{
				MapEvent theMapEvent = (MapEvent)logEvent;
				string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";
				string outputFormat = "TD-ME {3}\tTimeSubmitted:[{0}]\tCommandCat:[{1}]\tDisplayCat:[{2}]";
				
				output = String.Format(outputFormat, theMapEvent.Submitted.ToString(dateTimeFormat), theMapEvent.CommandCategory.ToString(), theMapEvent.DisplayCategory.ToString(), theMapEvent.Time.ToString(dateTimeFormat));
			}
			return output;
		}
	}
}
