// *********************************************** 
// NAME                 : EmailPublisher.cs 
// AUTHOR               : Kenny Cheung/Gary Eaton
// DATE CREATED         : 07/07/2003 
// DESCRIPTION  : A publisher that publishes
// events via SMTP mail.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/EmailPublisher.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:00   mturner
//Initial revision.
//
//   Rev 1.5   Oct 29 2003 20:27:34   geaton
//Use formatter directly without testing for logEvent type.
//
//   Rev 1.4   Oct 07 2003 13:40:38   geaton
//Updates following introduction of TDExceptionIdentifier.
//
//   Rev 1.3   Aug 21 2003 17:33:22   geaton
//Store smtpserver as class attribute instead of initialising SMTP object in constructor. (In case SMTP object is used by another component.)
//
//   Rev 1.2   Jul 25 2003 14:14:28   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).
//
//   Rev 1.1   Jul 24 2003 18:27:30   geaton
//Added/updated comments

using System;
using System.Net.Mail;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Publishes events via SMTP mail.
	/// </summary>
	public class EmailPublisher : IEventPublisher
	{
		private string to;
		private string from;
		private string subject;
		private MailPriority priority;
		private string identifier;
		private string smtpServer;
		
		/// <summary>
		/// Gets the identifier.
		/// </summary>
		public string Identifier
		{
			get {return identifier;}
		}
		
		/// <summary>
		/// Create a publisher that sends event details via e-mail.
		/// It is assumed that all parameters have been pre-validated.
		/// </summary>
		/// <param name="identifier">Identifer</param>
		/// <param name="destination">E-mail address to send to.</param>
		/// <param name="from">E-mail address of sendee.</param>
		/// <param name="subject">Subject of the e-mail message.</param>
		/// <param name="priority">Prioirty of the e-mail message</param>
		/// <param name="smtpServer">Name of the SMTP Server in which to send messages from.</param>
		public EmailPublisher(string identifier, string destination, string from, string subject, MailPriority priority, string smtpServer)
		{
			this.to = destination;
			this.from = from;
			this.subject = subject;
			this.priority = priority;
			this.identifier = identifier;
			this.smtpServer = smtpServer;
		}

		/// <summary>
		/// Sends an email containing details of the given log event.
		/// </summary>
		/// <param name="logEvent"><c>LogEvent</c> to send details for.</param>
		/// <exception cref="TransportDirect.Common.TDException">Log Event was not successfully emailed.</exception>
		public void WriteEvent(LogEvent logEvent)
		{
            // set-up the e-mail message
            string formatString = String.Empty;
            formatString = logEvent.EmailFormatter.AsString(logEvent);
            MailMessage message = new MailMessage(from, to, subject, formatString);
            message.Priority = priority;

           	try
			{
                // send the message            
				SmtpClient client = new SmtpClient(smtpServer);
				client.Send(message);
			}
			catch(Exception e) // Catch Exception due to lack of MS documentation
			{
				string msg =
					String.Format(Messages.EmailPublisherWriteEvent,
					"to:" + to + "," + "from:" + 
					from + "," + "subject:" + subject + "," + "priority:" + priority + 
					"," + "identifier:" + identifier + "," + "smtpServer:" + 
					smtpServer + "," + "MessageBody:" + message.Body
					+ "," + "LogEventString:" + formatString);

				throw new TDException(msg, e, false, TDExceptionIdentifier.ELSEmailPublisherWritingEvent);
			}
		}
	}
}
