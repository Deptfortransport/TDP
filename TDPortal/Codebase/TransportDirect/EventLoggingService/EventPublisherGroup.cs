// *********************************************** 
// NAME                 : EventPublisherGroup.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Used to create a group of
// publishers that publish events.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/EventPublisherGroup.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:04   mturner
//Initial revision.
//
//   Rev 1.7   Oct 21 2003 15:13:46   geaton
//Changes resulting from removal of Event Log Entry Type from properties. (This is now derived from TDTraceLevel of event being logged.)
//
//   Rev 1.6   Oct 20 2003 13:23:46   geaton
//Change to support PropertyValidator.StringToEnum becoming static method.
//
//   Rev 1.5   Sep 03 2003 10:14:46   geaton
//Added thread safety measures and introduced applicationId into filepath format.
//
//   Rev 1.4   Jul 25 2003 14:14:30   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).
//
//   Rev 1.3   Jul 24 2003 18:27:34   geaton
//Added/updated comments

using System;
using System.Collections;
using System.Text;
using System.Net.Mail;
using System.Diagnostics;
using System.Messaging;
using System.IO;
using System.Globalization;
using System.Reflection;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Factory class that creates a group of event publishers.
	/// </summary>
	class EventPublisherGroup : PublisherGroup
	{
	
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="properties">Properties used to create publishers.</param>
		public EventPublisherGroup(IPropertyProvider properties) 
			: base(properties)
		{}

		/// <summary>
		/// Main factory method for creating publishers based on properties.
		/// </summary>
		/// <param name="errors">Stores errors occuring during validation and creation.</param>
		/// <exception cref="TransportDirect.Common.TDException">Unrecoverable error occured when creating publishing group.</exception>
		override public void CreatePublishers(ArrayList errors)
		{
			int errorsBeforeValidation = errors.Count;

			validator.ValidateProperty(Keys.QueuePublishers, errors);
			validator.ValidateProperty(Keys.EmailPublishers, errors);
			validator.ValidateProperty(Keys.FilePublishers, errors);
			validator.ValidateProperty(Keys.EventLogPublishers, errors);
			validator.ValidateProperty(Keys.CustomPublishers, errors);
			validator.ValidateProperty(Keys.ConsolePublishers, errors);

			if (errors.Count == errorsBeforeValidation)
			{
				CreateEmailPublishers(errors);
				CreateQueuePublishers(errors);
				CreateFilePublishers(errors);
				CreateEventLogPublishers(errors);
				CreateConsolePublishers(errors);
			}
		}
		
		private void CreateEventLogPublishers(ArrayList errors)
		{
			string[] ids = null;
			string publisherids = properties[Keys.EventLogPublishers];
			if (publisherids.Length != 0) // valid to have no publishers
				ids = publisherids.Split(' ');
			else
				ids = new String[0];
			
			try
			{
				foreach (string id in ids)
				{		
					publishers.Add(
						new EventLogPublisher(id,
						properties[String.Format(Keys.EventLogPublisherName, id)],
						properties[String.Format(Keys.EventLogPublisherSource, id)],
						properties[String.Format(Keys.EventLogPublisherMachine, id)]
											  )
						);
				}
			}
			catch (TDException tdException)
			{
				errors.Add(tdException.Message);
				throw tdException;
			}

		}

		private void CreateConsolePublishers(ArrayList errors)
		{
			string[] ids = null;
			string publisherids = properties[Keys.ConsolePublishers];
			if (publisherids.Length != 0) // valid to have no publishers
				ids = publisherids.Split(' ');
			else
				ids = new String[0];

			try
			{
				foreach (string id in ids)
				{		
					publishers.Add(new ConsolePublisher(id, properties[String.Format(Keys.ConsolePublisherStream, id)] ));
				}
			}
			catch (TDException tdException)
			{
				errors.Add(tdException.Message);
				throw tdException;
			}

		}

		/// <summary>
		/// Generates a unique filepath based on the filename 
		/// format {ApplicationId}-{YYYYMMDD}{Time}
		/// Filename does not include an extension.
		/// </summary>
		/// <param name="directoryPath">Path to directory that will hold file.</param>
		/// <returns>Filepath.</returns>
		private string GenerateFilepath(string directoryPath)
		{
			string date = DateTime.Now.ToString
				("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
			string time = DateTime.Now.ToString
				("HHmmssfffff", System.Globalization.CultureInfo.InvariantCulture);
			string applicationId = this.properties.ApplicationID;

			return (String.Format("{0}\\{1}-{2}{3}", directoryPath, applicationId, date, time));
		}

		private void CreateFilePublishers(ArrayList errors)
		{
			string[] ids = null;
			string publisherids = properties[Keys.FilePublishers];
			if (publisherids.Length != 0) // valid to have no publishers
				ids = publisherids.Split(' ');
			else
				ids = new String[0];

			CultureInfo cultureInfo = new CultureInfo("en-US");

			try
			{
				foreach (string id in ids)
				{		
					int rotation = int.Parse(properties[String.Format(Keys.FilePublisherRotation, id)], cultureInfo);
					string filePath = GenerateFilepath(properties[String.Format(Keys.FilePublisherDirectory, id)]);

					publishers.Add(new FilePublisher(id, rotation, filePath));
				}
			}
			catch (TDException tdException)
			{
				errors.Add(tdException.Message);
				throw tdException;
			}

		}

		private void CreateEmailPublishers(ArrayList errors)
		{
			string[] ids = null;
			string publisherids = properties[Keys.EmailPublishers];
			if (publisherids.Length != 0) // valid to have no publishers
				ids = publisherids.Split(' ');
			else
				ids = new String[0];

			try
			{
				foreach (string id in ids)
				{		
					publishers.Add(
						new EmailPublisher(id, 
						properties[String.Format(Keys.EmailPublisherTo, id)],
						properties[String.Format(Keys.EmailPublisherFrom, id)],
						properties[String.Format(Keys.EmailPublisherSubject, id)],
						(MailPriority)PropertyValidator.StringToEnum(typeof(MailPriority), 
															 properties[String.Format(Keys.EmailPublisherPriority, id)]),
						properties[Keys.EmailPublishersSmtpServer] ) 
								 );
				}
			}
			catch (TDException tdException)
			{
				errors.Add(tdException.Message);
				throw tdException;
			}
		}

		private void CreateQueuePublishers(ArrayList errors)
		{
			bool recoverable;
			string[] ids = null;
			string publisherids = properties[Keys.QueuePublishers];
			if (publisherids.Length != 0) // valid to have no publishers
				ids = publisherids.Split(' ');
			else
				ids = new String[0];
			
			try
			{
				foreach (string id in ids)
				{		
					if (properties[String.Format(Keys.QueuePublisherDelivery, id)] == "Express")
						recoverable = false;
					else
						recoverable = true;

					publishers.Add(
						new QueuePublisher(id,
						(MessagePriority)PropertyValidator.StringToEnum(typeof(MessagePriority),
						properties[String.Format(Keys.QueuePublisherPriority, id)]),
						properties[String.Format(Keys.QueuePublisherPath, id)],
						recoverable
						)
						);
				}
			}
			catch (TDException tdException)
			{
				errors.Add(tdException.Message);
				throw tdException;
			}

		}

	}
}
