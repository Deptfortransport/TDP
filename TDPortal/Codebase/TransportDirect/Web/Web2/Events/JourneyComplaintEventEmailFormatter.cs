// *********************************************** 
// NAME                 : JourneyComplaintEmailEventFormatter.cs 
// AUTHOR               : Andrew Windley
// DATE CREATED         : 17/07/2003 
// DESCRIPTION          : A formatter that formats the JourneyComplaintEvent
// for e-mail publishing.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Events/JourneyComplaintEventEmailFormatter.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:23:52   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:58   mturner
//Initial revision.
//
//   Rev 1.2   Jul 29 2003 16:21:12   AWindley
//removed JPlanRef from email formatter - should not be visible to user
//
//   Rev 1.1   Jul 28 2003 15:49:46   AWindley
//removed parameter incidentOccurred
//
//   Rev 1.0   Jul 24 2003 16:59:14   AWindley
//Initial Revision

using System;
using System.Text;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.Web.Events
{
	/// <summary>
	/// JourneyComplaintEventEmailFormatter to format user's complaint data.
	/// </summary>
	[Serializable]
	public class JourneyComplaintEventEmailFormatter : IEventFormatter
	{
		private const string SEPARATOR = "\r\n";
		
		/// <summary>
		/// Default constructor
		/// </summary>
		public JourneyComplaintEventEmailFormatter()
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
			
			if(logEvent is JourneyComplaintEvent)
			{
                JourneyComplaintEvent journeyComplaintEvent =
					(JourneyComplaintEvent)logEvent;

				sb.Append("Name: " + journeyComplaintEvent.FirstName + " " + journeyComplaintEvent.LastName);
				sb.Append(SEPARATOR);
				sb.Append("Comment: " + journeyComplaintEvent.Comment);
				sb.Append(SEPARATOR);
				sb.Append("Time: " + journeyComplaintEvent.Time);
				
			}
			
			return sb.ToString();
		}
	}
}
