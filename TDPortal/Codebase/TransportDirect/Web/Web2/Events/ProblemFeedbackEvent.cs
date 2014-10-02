// *********************************************** 
// NAME                 : ProblemFeedbackEvent.cs 
// AUTHOR               : Halim Ahad
// DATE CREATED         : 24/09/2003 
// DESCRIPTION          : Custom LogEvent representing  
//                        user's submitted feedback data. 
// ************************************************ 
// $Log:

using System;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.Web.Events
{
	/// <summary>
	/// Summary description for ProblemFeedbackEvent.
	/// </summary>
	[Serializable]
	public class ProblemFeedbackEvent : CustomEvent
	{
		private string to = String.Empty;
		private string subject = String.Empty;
		//private string refNumber = String.Empty;
		private string bodyText = String.Empty;
		//private bool hasAttachment;
		//private string attachmentName = String.Empty;
	
		

		public ProblemFeedbackEvent(string destination, string bodyText, string subject): base()
		{
			this.to = destination;
			this.bodyText = bodyText;
			this.subject = subject;
			//this.hasAttachment = false;
			
		}

		public string To
		{
			get {return to;}
		} 

		/// <summary>
		/// Gets a boolean indicator that specifies whether the event has a file attachment.
		/// </summary>
//		public bool HasAttachment
//		{
//			get {return hasAttachment;}
//		} 
		//Screenflow deployment is not working that is the problem
		//thou should not dispare.Text() when thou hast made the solution disappeared
		//these are the times of our lives

		/// <summary>
		/// Gets the body text that must be published with the email.
		/// </summary>
		public string BodyText
		{
			get {return bodyText;}
		}

		/// <summary>
		/// Gets the subject line that must be used with the email.
		/// </summary>
		public string Subject
		{
			get {return subject;}
		}


		/// <summary>
		/// Provides an event formatter for publishing to files.
		/// </summary>
		override public IEventFormatter FileFormatter
		{
			// Use default formatter.
			// No requirement to publish CustomEmailEvent to files.

			get {return null;}
		}

		/// <summary>
		/// Provides an event formatting for publishing to email.
		/// </summary>
		override public IEventFormatter EmailFormatter
		{
			// Provide default formatter.
			// No requirement to publish CustomEmailEvent using the standard email publisher.
			
			get {return null;}
		}

		/// <summary>
		/// Provides an event formatter for publishing to event logs
		/// </summary>
		override public IEventFormatter EventLogFormatter
		{
			// Provide default formatter.
			// No requirement to publish CustomEmailEvent to event logs.
			
			get {return null;}
		}

		/// <summary>
		/// Provides an event formatter for publishing to console.
		/// </summary>
		override public IEventFormatter ConsoleFormatter
		{
			// Use default formatter.
			// No requirement to publish CustomEmailEvent to console.
			
			get {return null;}
		}
	}
}

	

