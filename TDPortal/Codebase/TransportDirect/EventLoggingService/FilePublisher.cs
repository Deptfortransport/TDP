// *********************************************** 
// NAME                 : FilePublisher.cs 
// AUTHOR               : Kenny Cheung/Gary Eaton
// DATE CREATED         : 07/07/2003 
// DESCRIPTION  : A publisher that publishes
// events to files.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/FilePublisher.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:04   mturner
//Initial revision.
//
//   Rev 1.6   Oct 29 2003 20:41:34   geaton
//Use formatter without checking event type.
//
//   Rev 1.5   Oct 07 2003 13:40:42   geaton
//Updates following introduction of TDExceptionIdentifier.
//
//   Rev 1.4   Sep 03 2003 10:14:44   geaton
//Added thread safety measures and introduced applicationId into filepath format.
//
//   Rev 1.3   Jul 30 2003 08:40:20   geaton
//Added slashes when forming file path.
//
//   Rev 1.2   Jul 25 2003 14:14:32   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).
//
//   Rev 1.1   Jul 24 2003 18:27:34   geaton
//Added/updated comments

using System;
using System.IO;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Publishes events to files.
	/// </summary>
	public class FilePublisher : IEventPublisher
	{
		private string identifier;
		private int rotation;
		private int currentRecordCount;
		private string baseFilepath;
		private string rotationFilepath;
		private int rotationSequence;
		
		/// <summary>
		/// Gets identifier.
		/// </summary>
		public string Identifier
		{
			get {return identifier;}
		}

		/// <summary>
		/// Constructor for creating a publisher that writes event details to files.
		/// It is assumed that all parameters have been pre-validated.
		/// </summary>
		/// <param name="identifier">Identifier</param>
		/// <param name="rotation">Maximum number of records per file.</param>
		/// <param name="baseFilepath">Base filepath of file to publish to.</param>
		public FilePublisher(string identifier, int rotation, string baseFilepath)
		{
			this.identifier = identifier;
			this.rotation = rotation;
			this.baseFilepath = baseFilepath;
			this.rotationSequence = 0;
			UpdateRotationFilepath();
			currentRecordCount = 0;
		}

		/// <summary>
		/// Writes the given log event to the file.
		/// </summary>
		/// <param name="logEvent">Log Event to write details for.</param>
		/// <exception cref="TransportDirect.Common.TDException">Log Event was not successfully written to the file.</exception>
		public void WriteEvent(LogEvent logEvent)
		{
			
			lock (this)  // support multithreading on a single file by locking
			{
				string formatString = String.Empty;

				try
				{
					formatString = logEvent.FileFormatter.AsString(logEvent);
					WriteToFile(formatString);
				}
				catch(UnauthorizedAccessException uae)
				{
					// thrown if access to the file is denied

					String message = String.Format(
						Messages.FilePublisherWriteEvent, formatString, this.rotationFilepath);
		
					throw new TDException(message, uae, false, TDExceptionIdentifier.ELSFilePublisherWritingEvent);
				}
				catch(ArgumentNullException ane)
				{	
					// thrown if the path is null
				
					String message = String.Format(
						Messages.FilePublisherWriteEvent, formatString, this.rotationFilepath);
				
					throw new TDException(message, ane, false, TDExceptionIdentifier.ELSFilePublisherWritingEvent);
				}
				catch(DirectoryNotFoundException dnfe)
				{
					// thrown if the directory to write to is not found
				
					String message = String.Format(
						Messages.FilePublisherWriteEvent, formatString, this.rotationFilepath);
				
					throw new TDException(message, dnfe, false, TDExceptionIdentifier.ELSFilePublisherWritingEvent);
				}
				catch(PathTooLongException ptle)
				{
					String message = String.Format(
						Messages.FilePublisherWriteEvent, formatString, this.rotationFilepath);
				
					throw new TDException(message, ptle, false, TDExceptionIdentifier.ELSFilePublisherWritingEvent);
				}
				catch(System.Security.SecurityException se)
				{
					// Thrown if the caller does not have the required permission
					// to access the file
				
					String message = String.Format(
						Messages.FilePublisherWriteEvent, formatString, this.rotationFilepath);
				
					throw new TDException(message, se, false, TDExceptionIdentifier.ELSFilePublisherWritingEvent);
				}
				catch(ArgumentException ae)
				{
					// thrown if the path is empty
				
					String message = String.Format(
						Messages.FilePublisherWriteEvent, formatString, this.rotationFilepath);
				
					throw new TDException(message, ae, false, TDExceptionIdentifier.ELSFilePublisherWritingEvent);
				}
				catch(IOException ioe)
				{
					// thrown if the path includes an incorrect or invalid syntax 
					// for the file name, directory name, or volume label syntax.
					// or if an I/O error occurs, such as the stream being closed.

					String message = String.Format(
						Messages.FilePublisherWriteEvent, formatString, this.rotationFilepath);
				
					throw new TDException(message, ioe, false, TDExceptionIdentifier.ELSFilePublisherWritingEvent);
				}
				catch(ObjectDisposedException ode)
				{
					// thrown if an attempt to write to a closed stream is made

					String message = String.Format(
						Messages.FilePublisherWriteEvent, formatString, this.rotationFilepath);
				
					throw new TDException(message, ode, false, TDExceptionIdentifier.ELSFilePublisherWritingEvent);
				}
				catch(Exception e)
				{
					String message = String.Format(
						Messages.FilePublisherWriteEvent, formatString, this.rotationFilepath);
				
					throw new TDException(message, e, false, TDExceptionIdentifier.ELSFilePublisherWritingEvent);
				}
			}
		}

		/// <summary>
		/// Writes <c>stringToWrite</c> to the current file
		/// on a new line.
		/// </summary>
		/// <param name="fileEntry">String to write to the file.</param>
		private void WriteToFile(string fileEntry)
		{
			StreamWriter sw = new StreamWriter(rotationFilepath, true);
			
			sw.WriteLine(fileEntry);

			sw.Close();
			
			currentRecordCount++;

			if (currentRecordCount == rotation)
			{
				// rotation reached so update filepath
				UpdateRotationFilepath();
				currentRecordCount = 0;
			}
		}

		private void UpdateRotationFilepath()
		{
			this.rotationFilepath = String.Format("{0}-{1}.txt", 
												  this.baseFilepath,
												  this.rotationSequence);
			this.rotationSequence++;
		}
		
	}
}
