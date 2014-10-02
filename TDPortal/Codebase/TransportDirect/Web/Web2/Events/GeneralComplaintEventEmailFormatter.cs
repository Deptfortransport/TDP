// *********************************************** 
// NAME                 : GeneralComplaintEventEmailFormatter.cs 
// AUTHOR               : Andrew Windley
// DATE CREATED         : 17/07/2003 
// DESCRIPTION          : A formatter that formats the GeneralComplaintEvent
// for e-mail publishing.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Events/GeneralComplaintEventEmailFormatter.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:23:52   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:58   mturner
//Initial revision.
//
//   Rev 1.1   Jul 28 2003 15:49:42   AWindley
//removed parameter incidentOccurred
//
//   Rev 1.0   Jul 24 2003 16:59:08   AWindley
//Initial Revision

using System;
using System.Text;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.Web.Events
{
	/// <summary>
	/// GeneralComplaintEventEmailFormatter to format user's complaint data.
	/// </summary>
	[Serializable]
	public class GeneralComplaintEventEmailFormatter : IEventFormatter
	{
		private const string SEPARATOR = "\r\n";
		
		/// <summary>
		/// Default constructor
		/// </summary>
		public GeneralComplaintEventEmailFormatter()
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
			
			if(logEvent is GeneralComplaintEvent)
			{
                GeneralComplaintEvent generalComplaintEvent =
					(GeneralComplaintEvent)logEvent;

				sb.Append("Name: " + generalComplaintEvent.FirstName + " " + generalComplaintEvent.LastName);
				sb.Append(SEPARATOR);
				sb.Append("Comment: " + generalComplaintEvent.Comment);
				sb.Append(SEPARATOR);
				sb.Append("Time: " + generalComplaintEvent.Time);
			}
			
			return sb.ToString();
		}
	}
}
