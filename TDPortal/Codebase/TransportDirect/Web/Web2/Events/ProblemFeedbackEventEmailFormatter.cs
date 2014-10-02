// *********************************************** 
// NAME                 : ProblemFeedbackEventFormatter.cs 
// AUTHOR               : Andrew Windley
// DATE CREATED         : 25/09/2003 
// DESCRIPTION          : A formatter that formats the ProblemFeedbackEvent
// for e-mail publishing.
// ************************************************ 
// $Log:

using System;
using System.Text;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.Web.Events
{
	/// <summary>
	/// Summary description for ProblemFeedbackEmailFormatter.
	/// </summary>
	[Serializable]
	public class ProblemFeedbackEventEmailFormatter : IEventFormatter
	{
		private const string SEPARATOR = "\r\n";

		public ProblemFeedbackEventEmailFormatter()
		{
		}

		public string AsString(LogEvent logEvent)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("");
			
			if(logEvent is ProblemFeedbackEvent)
			{
				ProblemFeedbackEvent problemFeedbackEvent =
					(ProblemFeedbackEvent)logEvent;

				sb.Append("Ref: " + problemFeedbackEvent.Subject);
				sb.Append(SEPARATOR);
				sb.Append("Comment: " + problemFeedbackEvent.BodyText);
				sb.Append(SEPARATOR);
				sb.Append("Time: " + problemFeedbackEvent.Time);
			}
			
			return sb.ToString();
		}

	}
}
