// ************************************************************* 
// NAME                 : ForgotPasswordEventEmailFormatter.cs 
// AUTHOR               : Andrew Sinclair
// DATE CREATED         : 12/09/2003 
// DESCRIPTION          : An email formatter for the  
//                      ForgotPasswordEvent
// **************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Events/ForgotPasswordEventEmailFormatter.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:23:52   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:58   mturner
//Initial revision.
//
//   Rev 1.0   Sep 12 2003 15:18:20   asinclair
//Initial Revision
using System;
using System.Text;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.Web.Events
{
	/// <summary>
	/// ForgotPasswordEmailFormatter to format TODO.
	/// </summary>
	[Serializable]
	public class ForgotPasswordEventEmailFormatter : IEventFormatter
	{
		private const string SEPARATOR = "\r\n";
		
		/// <summary>
		/// Default constructor
		/// </summary>
		public ForgotPasswordEventEmailFormatter()
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
			
			if(logEvent is ForgotPasswordEvent)
			{
				ForgotPasswordEvent forgotPasswordEvent =
					(ForgotPasswordEvent)logEvent;

				sb.Append("Your Username is " + forgotPasswordEvent.username);
				sb.Append(SEPARATOR);
				sb.Append("Your Password is  " + forgotPasswordEvent.password);
				sb.Append(SEPARATOR);
				sb.Append("This is an automated e-mail, please do not reply directly to it");
			}
			return sb.ToString();
		}
	}
}

