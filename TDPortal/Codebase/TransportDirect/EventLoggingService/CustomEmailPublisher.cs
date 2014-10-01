// ******************************************************* 
// NAME                 : CustomEmailPublisher.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 17/09/2003 
// DESCRIPTION  : Custom publisher used to publish 
// exclusively CustomEmailEvents.
// This custom publisher is provided as part of the core event 
// logging service since it is potentially reusable across
// many functional areas.
// *******************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/CustomEmailPublisher.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:58   mturner
//Initial revision.
//
//   Rev 1.7   May 27 2004 11:51:58   COwczarek
//Modification to last fix.
//Resolution for 726: Server error message when sending email to friend (Maps) (DEL 6.0)
//
//   Rev 1.6   May 26 2004 17:05:06   COwczarek
//Attachment can be specified as a stream or file path. Copy of attachment to local directory is dependant on which has been specified. No events logged if source of attachement cannot be found.
//Resolution for 726: Server error message when sending email to friend (Maps) (DEL 6.0)
//
//   Rev 1.5   Jan 15 2004 10:58:00   asinclair
//Change the string used in catching an error in the write event, so the real email address passed in is used and not the one from the database.
//
//   Rev 1.4   Jan 07 2004 11:41:12   JHaydock
//Changed email subject to "<user email> sent you a message" and email from address to that of the currently logged in user
//
//   Rev 1.3   Oct 29 2003 20:55:18   geaton
//Added validation of from.
//
//   Rev 1.2   Oct 07 2003 13:40:34   geaton
//Updates following introduction of TDExceptionIdentifier.
//
//   Rev 1.1   Sep 22 2003 17:19:26   geaton
//Removed code to throw TDException on directory exceptions - since it is possible that event was successful published - instead just log error.
//
//   Rev 1.0   Sep 18 2003 11:29:18   geaton
//Initial Revision

using System;
using System.Net.Mail;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Custom publisher for sending emails (stored as CustomEmailEvent)
	/// with or without an attachment to one or more recipients.
	/// </summary>
	public class CustomEmailPublisher : IEventPublisher
	{
		private string identifier;
		private string from;
		private System.Net.Mail.MailPriority priority;
		private string smtpServer;
		private string workingDirectoryPath;
		
		/// <summary>
		/// Gets the identifier associated with this publisher.
		/// This identifier is used to tie events with this publisher.
		/// This identifier must match with that used in the configuration properties
		/// </summary>
		public string Identifier
		{
			get {return identifier;}
		}

        /// <summary>
        /// Creates a new directory and in it creates a new file from the supplied stream
        /// giving it the supplied name.
        /// Deletion of the newly created directory and file is the responsibility of the
        /// caller.
        /// </summary>
        /// <param name="attachmentStream">File source</param>
        /// <param name="fileName">Filename to use for newly created file</param>
        /// <param name="destinationFilePath">Full path of destination file, returned 
        /// irrespective of whether file creation was successfull.</param>
		private void CreateImageFileFromStream(Stream attachmentStream, string fileName, out string destinationFilePath)
		{
			// generate unique dirpath using a GUID
		    string dirPath = Path.Combine(workingDirectoryPath,Guid.NewGuid().ToString());
            destinationFilePath = Path.Combine(dirPath,fileName);

            Directory.CreateDirectory(dirPath);
			Bitmap bitmap = new Bitmap(attachmentStream);
			bitmap.Save(destinationFilePath);
		}

        /// <summary>
        /// Copies the specified file to a new directory and gives it the supplied name.
        /// Deletion of the newly created directory and file is the responsibility of the
        /// caller.
        /// </summary>
        /// <param name="attachmentFilePath">Full path of file to copy</param>
        /// <param name="fileName">Filename to use for newly created file</param>
        /// <param name="destinationFilePath">Full path of destination file, returned 
        /// irrespective of whether file creation was successfull.</param>
        private void CopyImageFile(string attachmentFilePath, string fileName, out string destinationFilePath)
        {
            // generate unique dirpath using a GUID
            string dirPath = Path.Combine(workingDirectoryPath,Guid.NewGuid().ToString());
            destinationFilePath = Path.Combine(dirPath,fileName);

            Directory.CreateDirectory(dirPath);
            File.Copy(attachmentFilePath,destinationFilePath);
        }


		/// <summary>
		/// Publishes the event <c>LogEvent</c> passed.
		/// </summary>
		/// <remarks>
		/// This publisher is only able to publish custom events
		/// of type <c>EmailAttachmentPublisher</c>.
		/// </remarks>
		/// <exception cref="TDException">
		/// Thrown if an unsupported <c>LogEvent</c> type is passed 
		/// or an error occurs publishing the event passed.
		/// </exception>
		/// <param name="logEvent">Event to be published.</param>
		public void WriteEvent(LogEvent logEvent)
		{
			if (logEvent is CustomEmailEvent)
			{
				CustomEmailEvent emailEvent = (CustomEmailEvent)logEvent;

                //If override of from address supplied by
				//CustomEmailEvent, then use that one instead

                string mailFrom = string.Empty;
				if (emailEvent.From == String.Empty)
					mailFrom = this.from;
				else
					mailFrom = emailEvent.From;

                emailEvent.To.Replace(' ',';');
				
                MailMessage message = new MailMessage(mailFrom,emailEvent.To,emailEvent.Subject,emailEvent.BodyText);
                message.Priority = this.priority;

                string attachmentFilePath = string.Empty;

				try
				{

                    // Create attachment file

                    try 
                    {

                        if (emailEvent.HasAttachmentStream)
                        {
                            CreateImageFileFromStream(emailEvent.AttachmentStream, emailEvent.AttachmentName, out attachmentFilePath);
                            // An attachment file has been created, attach it to the email
                            message.Attachments.Add(new Attachment(attachmentFilePath));
                        } 
                        else if (emailEvent.HasAttachmentFilePath) 
                        {
                            CopyImageFile(emailEvent.AttachmentFilePath, emailEvent.AttachmentName, out attachmentFilePath);
                            // An attachment file has been created, attach it to the email
                            message.Attachments.Add(new Attachment(attachmentFilePath));
                        }

                    }
                    catch (System.IO.FileNotFoundException) 
                    {
                        // Fail quietly in this case. A user may have attempted to send email
                        // after GIS housekeeping has removed the image file (if they took too
                        // long to click send)
                    }
                    catch (Exception e) // exceptions not documented so catch all
                    {
                        // log error and throw exception
                        OperationalEvent opEvent = 
                            new OperationalEvent(TDEventCategory.Infrastructure,
                            TDTraceLevel.Error,
                            String.Format(Messages.CustomEmailPublisherSaveFailed, attachmentFilePath),
                            e);

                        Trace.Write(opEvent);

                        throw new TDException(String.Format(
                            Messages.CustomEmailPublisherSaveFailed, 
                            attachmentFilePath), 
                            true, 
                            TDExceptionIdentifier.ELSCustomEmailPublisherSavingStream);
                    }
				
					SmtpClient client = new SmtpClient(this.smtpServer);
                    client.Send(message);
				}
				catch(Exception exception) // Catch ALL Exceptions due to lack of MS documentation on Smtp
				{
					string msg = String.Format(Messages.CustomEmailPublisherWriteEventFailed, 
											   this.identifier,
											   emailEvent.To,
											   message.From,
											   emailEvent.Subject,
											   this.priority, 
											   this.smtpServer,
											   emailEvent.BodyText);

					throw new TDException(msg, exception, false, TDExceptionIdentifier.ELSCustomEmailPublisherWritingEvent);
				}
				finally
				{

					if (emailEvent.HasAttachment)
					{
						// delete file attachment and directory. Do not throw exception if delete fails since this signifies that event was not published - this may not be the case.
						try
						{
							Directory.Delete(Path.GetDirectoryName(attachmentFilePath), true);
						}
						catch (SecurityException securityException)
						{
							OperationalEvent oe = 
								new OperationalEvent(TDEventCategory.Infrastructure,
								TDTraceLevel.Error,
								String.Format(Messages.CustomEmailPublisherFileDeleteFailed, attachmentFilePath),
								securityException);

							Trace.Write(oe);
						}
						catch (ArgumentException argumentException)
						{
							OperationalEvent oe = 
								new OperationalEvent(TDEventCategory.Infrastructure,
								TDTraceLevel.Error,
								String.Format(Messages.CustomEmailPublisherFileDeleteFailed, attachmentFilePath),
								argumentException);

							Trace.Write(oe);
						}
						catch (IOException ioException)
						{
								
							OperationalEvent oe = 
								new OperationalEvent(TDEventCategory.Infrastructure,
								TDTraceLevel.Error,
								String.Format(Messages.CustomEmailPublisherFileDeleteFailed, attachmentFilePath),
								ioException);

							Trace.Write(oe);
						}
					}
				}
			}
			else
			{
				throw new TDException(Messages.CustomEmailPublisherUnsupportedEventType, false, TDExceptionIdentifier.ELSCustomEmailPublisherUnsupportedEvent);
			}
		}

		/// <summary>
		/// Constructor for creating an Custom Email Publisher that sends event data via e-mail.
		/// Constructor will perform validation of parameters.
		/// </summary>
		/// <param name="identifier">Identifier associated with this publisher and that used in the configuration properties.</param>
		/// <param name="from">Default e-mail address of sendee.</param>
		/// <param name="priority">Priority of the e-mail message</param>
		/// <param name="smtpServer">Name of the SMTP Server in which to send messages from.</param>
		/// <param name="workingDirectoryPath">Path to a working directory in which publisher can temporarily construct attachment files.</param>
		/// <param name="errors">Used to store validation errors.</param>
		/// /// <exception cref="TDException">
		/// Thrown if one or more of the constructor parameters are invalid.
		/// </exception>
		public CustomEmailPublisher(string identifier, string from, MailPriority priority, string smtpServer, string workingDirectoryPath, ArrayList errors)
		{
			int errorCountStart = errors.Count;

			if (identifier.Length == 0)
				errors.Add(String.Format(Messages.CustomPublisherInvalidId, "EmailAttachmentPublisher", identifier));
		
			if (from.Length == 0)
				errors.Add(String.Format(Messages.CustomEmailPublisherPublisherFromAddressBad, from));
			else
			{
				EmailAddress emailAddress = new EmailAddress();
				if (!emailAddress.Parse(from))
					errors.Add(String.Format(Messages.CustomEmailPublisherPublisherFromAddressBad, from));
			}

			if (smtpServer.Length == 0)
				errors.Add(String.Format(Messages.CustomEmailPublisherPublisherSmtpServerBad, smtpServer));			
				
			if (!Directory.Exists(workingDirectoryPath))
				errors.Add(String.Format(Messages.CustomEmailPublisherWorkingDirMissing, workingDirectoryPath));

			if (errors.Count == errorCountStart)
			{
				this.identifier = identifier;
				this.from = from;
				this.priority = priority;
				this.identifier = identifier;
				this.smtpServer = smtpServer;
				this.workingDirectoryPath = workingDirectoryPath;
			}
			else
			{
				throw new TDException(Messages.CustomEmailPublisherConstructor, false, TDExceptionIdentifier.ELSCustomEmailPublisherConstructor);
			}
		}
	}
}

