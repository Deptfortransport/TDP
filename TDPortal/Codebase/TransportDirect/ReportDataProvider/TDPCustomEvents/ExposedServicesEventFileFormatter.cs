// *********************************************** 
// NAME                 : ExposedServicesEventFormatter.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 07/02/2005
// DESCRIPTION  : File formatter for ExposedServices event.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/ExposedServicesEventFileFormatter.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:22   mturner
//Initial revision.
//
//   Rev 1.0   Feb 08 2005 11:05:46   passuied
//Initial revision.

using System;

using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// File formatter for ExposedServices event.
	/// </summary>
	public class ExposedServicesEventFileFormatter : IEventFormatter
	{
		// Custom datetime pattern based on ISO 8601, to resolution of milliseconds
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		public ExposedServicesEventFileFormatter()
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

			if(logEvent is ExposedServicesEvent)
			{
				ExposedServicesEvent e = (ExposedServicesEvent)logEvent;

				output = String.Format(Messages.ExposedServicesEventFileFormat,
					e.Submitted.ToString(dateTimeFormat),				   
					e.Time.ToString(dateTimeFormat),
					e.Token,
					e.Category.ToString(),
					e.Successful);
									  
			}
			return output;
		}

		
	}
}
