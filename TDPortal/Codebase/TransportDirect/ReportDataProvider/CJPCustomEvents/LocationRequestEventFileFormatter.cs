// *********************************************************************** 
// NAME                 : LocationRequestEventFileFormatter.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/09/2003
// DESCRIPTION  : Defines a file formatter for formatting
// LocationRequest events using the core Event Service File Publisher.
// Typically this file formatter will not be used in production, but
// instead LocationRequest events will be published to database via MSMQ
// This file formatter may be useful during development for debugging.
// *********************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/CJPCustomEvents/LocationRequestEventFileFormatter.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:08   mturner
//Initial revision.
//
//   Rev 1.1   Oct 07 2003 09:38:40   geaton
//Datetime format updated to resolution of milliseconds.
//
//   Rev 1.0   Sep 12 2003 11:33:34   geaton
//Initial Revision

using System;
using System.Globalization;

using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.CJPCustomEvents
{
	/// <summary>
	/// Formats LocationRequest events for publishing by a file publisher.
	/// </summary>
	public class LocationRequestEventFileFormatter : IEventFormatter
	{	

		// Custom datetime pattern based on ISO 8601, to resolution of milliseconds
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		/// <summary>
		/// Default constructor.
		/// </summary>
		public LocationRequestEventFileFormatter()
		{}

		/// <summary>
		/// Formats the given log event.
		/// </summary>
		/// <param name="logEvent">Log Event to format.</param>
		/// <returns>A formatted string representing the log event.</returns>
		public string AsString(LogEvent logEvent)
		{
			string output = String.Empty;

			if(logEvent is LocationRequestEvent)
			{
				LocationRequestEvent e = (LocationRequestEvent)logEvent;

				output = String.Format(Messages.LocationRequestEventFileFormat,
									   e.Time.ToString(dateTimeFormat),
									   e.JourneyPlanRequestId,
									   e.AdminAreaCode,
									   e.RegionCode,
									   e.PrepositionCategory.ToString());									  
			}
			return output;
		}
	}
}


