// ******************************************************* 
// NAME                 : CustomEmailEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 17/09/2003 
// DESCRIPTION  : Custom email event that is published
// exclusively by the custom publisher CustomEmailPublisher.
// THIS EVENT CANNOT BE PUBLISHED BY ANY OTHER PUBLISHER
// INCLUDING THE STANDARD PUBLISHERS.
// This custom event is provided as part of the core event 
// logging service since it is potentially reusable across
// many functional areas.
// *******************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/CustomEmailEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:58   mturner
//Initial revision.
//
//   Rev 1.2   May 26 2004 16:57:38   COwczarek
//Allow for an attachment to be specified as a file path as well as a stream
//Resolution for 726: Server error message when sending email to friend (Maps) (DEL 6.0)
//
//   Rev 1.1   Jan 07 2004 11:41:12   JHaydock
//Changed email subject to "<user email> sent you a message" and email from address to that of the currently logged in user
//
//   Rev 1.0   Sep 18 2003 11:29:14   geaton
//Initial Revision

using System;
using System.IO;

namespace TransportDirect.Common.Logging
{
	
	public class CustomEmailEvent : CustomEvent
	{
		private string from = String.Empty;
		private string to;
		private string subject;
		private Stream attachmentStream;
        private string attachmentFilePath;
        private string bodyText;
		private string attachmentName;
		private bool hasAttachment;

		/// <summary>
		/// Constructor method for class.
		/// </summary>
		/// <remarks>
		/// The constructor does ***NOT*** perform validation of parameters.
		/// </remarks>
		/// <param name="destination">One or more space separated email addresses to which event data should be published.</param>
		/// <param name="bodyText">Text that should be published in email body.</param>
		/// <param name="subject">Subject of the e-mail message.</param>
		public CustomEmailEvent(string destination, string bodyText, string subject) : base()
		{
			this.to = destination;
			this.bodyText = bodyText;
			this.subject = subject;
			this.hasAttachment = false;
		}

		/// <summary>
		/// Constructor method for class that allows an attachment to be provided as a stream.
		/// </summary>
		/// <remarks>
		/// The constructor does ***NOT*** perform validation of parameters.
		/// </remarks>
		/// <param name="destination">One or more space separated email addresses to which event data should be published.</param>
		/// <param name="bodyText">Text that should be published in email body.</param>
		/// <param name="subject">Subject of the e-mail message.</param>
		/// <param name="attachment">Stream that should be published as a file attachment with email.</param>
		/// <param name="attachmentName">File name (including extension) that should be used when attachment is attached.</param>
		public CustomEmailEvent(string destination, string bodyText, string subject, Stream attachment, string attachmentName) : base()
		{
			this.to = destination;
			this.attachmentStream = attachment;
			this.bodyText = bodyText;
			this.attachmentName = attachmentName;
			this.subject = subject;
			this.hasAttachment = true;
		}

        /// <summary>
        /// Constructor method for class that allows an attachment to be provided as a file path.
        /// </summary>
        /// <remarks>
        /// The constructor does ***NOT*** perform validation of parameters.
        /// </remarks>
        /// <param name="destination">One or more space separated email addresses to which event data should be published.</param>
        /// <param name="bodyText">Text that should be published in email body.</param>
        /// <param name="subject">Subject of the e-mail message.</param>
        /// <param name="attachment">filepath of file that should be published as a file attachment with email.</param>
        /// <param name="attachmentName">File name (including extension) that should be used when attachment is attached.</param>
        public CustomEmailEvent(string destination, string bodyText, string subject, string attachment, string attachmentName) : base()
        {
            this.to = destination;
            this.attachmentFilePath = attachment;
            this.bodyText = bodyText;
            this.attachmentName = attachmentName;
            this.subject = subject;
            this.hasAttachment = true;
        }

		/// <summary>
		/// Constructor method for class.
		/// </summary>
		/// <remarks>
		/// The constructor does ***NOT*** perform validation of parameters.
		/// </remarks>
		/// <param name="origin">Sender's email address shown on the sent email.</param>
		/// <param name="destination">One or more space separated email addresses to which event data should be published.</param>
		/// <param name="bodyText">Text that should be published in email body.</param>
		/// <param name="subject">Subject of the e-mail message.</param>
		public CustomEmailEvent(string origin, string destination, string bodyText, string subject) : base()
		{
			this.from = origin;
			this.to = destination;
			this.bodyText = bodyText;
			this.subject = subject;
			this.hasAttachment = false;
		}

		/// <summary>
		/// Constructor method for class that allows an attachment to be provided as a stream.
		/// </summary>
		/// <remarks>
		/// The constructor does ***NOT*** perform validation of parameters.
		/// </remarks>
		/// <param name="origin">Sender's email address shown on the sent email.</param>
		/// <param name="destination">One or more space separated email addresses to which event data should be published.</param>
		/// <param name="bodyText">Text that should be published in email body.</param>
		/// <param name="subject">Subject of the e-mail message.</param>
		/// <param name="attachment">Stream that should be published as a file attachment with email.</param>
		/// <param name="attachmentName">File name (including extension) that should be used when attachment is attached.</param>
		public CustomEmailEvent(string origin, string destination, string bodyText, string subject, Stream attachment, string attachmentName) : base()
		{
			this.from = origin;
			this.to = destination;
			this.attachmentStream = attachment;
			this.bodyText = bodyText;
			this.attachmentName = attachmentName;
			this.subject = subject;
			this.hasAttachment = true;
		}

        /// <summary>
        /// Constructor method for class that allows an attachment to be provided as a file path.
        /// </summary>
        /// <remarks>
        /// The constructor does ***NOT*** perform validation of parameters.
        /// </remarks>
        /// <param name="origin">Sender's email address shown on the sent email.</param>
        /// <param name="destination">One or more space separated email addresses to which event data should be published.</param>
        /// <param name="bodyText">Text that should be published in email body.</param>
        /// <param name="subject">Subject of the e-mail message.</param>
        /// <param name="attachment">File path of file that should be published as a file attachment with email.</param>
        /// <param name="attachmentName">File name (including extension) that should be used when attachment is attached.</param>
        public CustomEmailEvent(string origin, string destination, string bodyText, string subject, string attachment, string attachmentName) : base()
        {
            this.from = origin;
            this.to = destination;
            this.attachmentFilePath = attachment;
            this.bodyText = bodyText;
            this.attachmentName = attachmentName;
            this.subject = subject;
            this.hasAttachment = true;
        }

		/// <summary>
		/// Gets the email address shown as the sender's address on the email
		/// </summary>
		public string From
		{
			get {return from;}
		} 

		/// <summary>
		/// Gets the space separated email addresses to which to publish the event data to
		/// </summary>
		public string To
		{
			get {return to;}
		} 

		/// <summary>
		/// Gets a boolean indicator that specifies whether the event has a file attachment.
		/// </summary>
		public bool HasAttachment
		{
			get {return hasAttachment;}
		} 

        /// <summary>
        /// Gets a boolean indicator that specifies whether the event has a file attachment
        /// associated as a file path
        /// </summary>
        public bool HasAttachmentFilePath
        {
            get {return attachmentFilePath != null;}
        } 

        /// <summary>
        /// Gets a boolean indicator that specifies whether the event has a file attachment
        /// associated as a stream
        /// </summary>
        public bool HasAttachmentStream
        {
            get {return attachmentStream != null;}
        } 

		/// <summary>
		/// Gets the attachment stream that must be published with the email
		/// </summary>
		public Stream AttachmentStream
		{
			get {return attachmentStream;}
		}

        /// <summary>
        /// Gets the attachment file path that must be published with the email
        /// </summary>
        public string AttachmentFilePath
        {
            get {return attachmentFilePath;}
        }

		/// <summary>
		/// Gets the attachment name that must be given to the file attachment.
		/// </summary>
		public string AttachmentName
		{
			get {return attachmentName;}
		}

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

