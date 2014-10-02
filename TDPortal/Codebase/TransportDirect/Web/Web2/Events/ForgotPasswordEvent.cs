// *********************************************** 
// NAME                 : ForgotPasswordEvent.cs 
// AUTHOR               : Andrew Sinclair
// DATE CREATED         : 12/09/2003 
// DESCRIPTION          : Custom Event representing  
//                      a user's forgotten password 
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Events/ForgotPasswordEvent.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:23:52   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:58   mturner
//Initial revision.
//
//   Rev 1.1   Sep 17 2003 16:23:18   hahad
//Set file, eventlog and console formatters to 'null'
//
//   Rev 1.0   Sep 12 2003 15:18:08   asinclair
//Initial Revision
using System;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.Web.Events
{
	/// <summary>
	/// Summary description for ForgotPasswordEvent.
	/// </summary>
	[Serializable]
	public class ForgotPasswordEvent : CustomEvent
	{
		private string Username = string.Empty;
		private string Password = string.Empty;

		private static IEventFormatter emailFormatter = new ForgotPasswordEventEmailFormatter();
		
		
		private static IEventFilter filter = new CustomEventFilter();

		/// <summary>
		/// Constructor for the ForgotPasswordEvent.
		/// </summary>
		public ForgotPasswordEvent(string Username, string Password): base()
		{
			this.Username = Username;
			this.Password = Password;
		}

		/// <summary>
		/// Get the email formatter (Read only)
		/// </summary>
		override public IEventFormatter EmailFormatter
		{
			get {return emailFormatter;}
		}

		/// <summary>
		/// Get the file formatter (Read only)
		/// </summary>
		override public IEventFormatter FileFormatter
		{
			// Use default formatter.
			// No requirement to publish EmailAttachmentEvent to files.

			get {return null;}
		}

		/// <summary>
		/// Get the event log formatter (Read only)
		/// </summary>
		override public IEventFormatter EventLogFormatter
		{
			// Use default formatter.
			// No requirement to publish EmailAttachmentEvent to files.

			get {return null;}
		}

		/// <summary>
		/// Get the console formatter (Read only)
		/// </summary>
		override public IEventFormatter ConsoleFormatter
		{
			// Use default formatter.
			// No requirement to publish EmailAttachmentEvent to files.

			get {return null;}
		}

		/// <summary>
		/// Get the event filter (Read only)
		/// </summary>
		override public IEventFilter Filter
		{
			get {return filter;}
		}

		/// <summary>
		/// Get the feedback reference number (Read only)
		/// </summary>
		public string username
		{
			get{return Username;}
		}

		/// <summary>
		/// Get the user's first name (Read only)
		/// </summary>
		public string password
		{
			get{return Password;}
		}

	}
}
