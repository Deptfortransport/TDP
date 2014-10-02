// *********************************************** 
// NAME                 : FeedbackEventFormatter.cs 
// AUTHOR               : Andrew Windley
// DATE CREATED         : 17/07/2003 
// DESCRIPTION          : A formatter that formats the FeedbackEvent
// for e-mail publishing.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Events/FeedbackEventEmailFormatter.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:23:50   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:56   mturner
//Initial revision.
//
//   Rev 1.1   Jul 28 2003 15:49:36   AWindley
//removed parameter incidentOccurred
//
//   Rev 1.0   Jul 24 2003 16:59:00   AWindley
//Initial Revision

using System;
using System.Text;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.Web.Events
{
	/// <summary>
	/// FeedbackEventFormatter to format user's complaint data.
	/// </summary>
	[Serializable]
	public class FeedbackEventEmailFormatter : IEventFormatter
	{
		private const string SEPARATOR = "\r\n";
		
		/// <summary>
		/// Default constructor
		/// </summary>
		public FeedbackEventEmailFormatter()
		{
		}

		/// <summary>
		/// Formats the given log event.
		/// </summary>
		/// <param name="logEvent">Log event to format.</param>
		/// <returns>A formatted string representing the log event.</returns>
		public string AsString(LogEvent logEvent)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("");
			
			if(logEvent is FeedbackEvent)
			{
                FeedbackEvent feedbackEvent =
					(FeedbackEvent)logEvent;

				sb.Append("Name: " + feedbackEvent.FirstName + " " + feedbackEvent.LastName);
				sb.Append(SEPARATOR);
				sb.Append("Comment: " + feedbackEvent.Comment);
				sb.Append(SEPARATOR);
				sb.Append("Time: " + feedbackEvent.Time);
			}
			
			return sb.ToString();
		}
	}
}
