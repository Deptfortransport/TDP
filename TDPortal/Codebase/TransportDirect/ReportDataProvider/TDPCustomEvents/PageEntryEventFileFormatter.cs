// *********************************************** 
// NAME                 : PageEntryEventFileFormatter
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 28/06/2004 
// DESCRIPTION  : File formatter for the PageEntryEvent
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/PageEntryEventFileFormatter.cs-arc  $ 
//
//   Rev 1.1   Mar 25 2008 09:46:58   pscott
//IR 4621 CCN 427 White Label Changes
//Make necessary changes to write theme id to page entryevents table
//
//   Rev 1.0   Nov 08 2007 12:39:28   mturner
//Initial revision.
//
//   Rev 1.0   Jun 28 2004 15:41:16   passuied
//Initial Revision

using System;
using System.Globalization;
using TD.ThemeInfrastructure;

using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// File formatter for the PageEntryEvent
	/// </summary>
	public class PageEntryEventFileFormatter : IEventFormatter
	{
		// Custom datetime pattern based on ISO 8601, to resolution of milliseconds
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		/// <summary>
		/// default constructor
		/// </summary>
		public PageEntryEventFileFormatter()
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

			if(logEvent is PageEntryEvent)
			{
				PageEntryEvent pee = (PageEntryEvent)logEvent;
               //int themeId = ThemeProvider.Instance.GetTheme().Id;
                output =
                    "TD-PEE\t" +
                    pee.Time.ToString(dateTimeFormat) + "\t" +
                    pee.Page + "\t" +
                    pee.UserLoggedOn + "\t" +
                    pee.ThemeId;

				

				if(pee.SessionId != OperationalEvent.SessionIdUnassigned)
				{
					output += ("\t" + pee.SessionId);
				}
			}
			return output;
		}
	}
}
