using System;
using System.Collections;
using System.Web.Mail;
using System.Diagnostics;

using DailyReportFramework;

namespace DailyReportFramework
{
    /// <summary>
    /// Summary description for SendEmail.
    /// </summary>
    public class SendEmail
    {

        private EventLog EventLogger;

        public SendEmail()
        {
            //
            // TODO: Add constructor logic here
            //
            EventLogger = new EventLog("Application");
            EventLogger.Source = "TD.Reporting";

        }
        public int SendFile(string senderAddress, string recipientAddress, string subject, string bodyText, string attachment, string smtpServer)
        {
            int statusCode = 0;

            MailMessage message = new MailMessage();
            string formatString = String.Empty;

            try
            {
                // set-up the e-mail message
                message.From = senderAddress;
                message.To = recipientAddress;
                message.Subject = subject;
                message.Priority = MailPriority.High;
                message.Body = bodyText;


                MailAttachment MyAttachment = new MailAttachment(attachment);
                message.Attachments.Add(MyAttachment);
                SmtpMail.SmtpServer = smtpServer;
                SmtpMail.Send(message);
            }
            catch (Exception e) // Catch Exception due to lack of MS documentation
            {
                string error = "Failure sending mail " + e.Message;
                EventLogger.WriteEntry(error, EventLogEntryType.Error);
                statusCode = 1008; // email failed
            }

            return statusCode;
        }
    }
}
