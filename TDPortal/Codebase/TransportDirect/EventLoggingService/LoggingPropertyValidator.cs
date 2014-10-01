// *********************************************** 
// NAME                 : LoggingPropertyValidator.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Validates logging-related 
// properties 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/LoggingPropertyValidator.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:06   mturner
//Initial revision.
//
//   Rev 1.12   Nov 03 2003 15:45:10   geaton
//Removed length restriction when event log name is Application (since this is a standard log provided by Windows).
//
//   Rev 1.11   Oct 21 2003 15:14:30   geaton
//Changes resulting from removal of Event Log Entry Type from properties. (This is now derived from TDTraceLevel of event being logged.)
//
//   Rev 1.10   Oct 21 2003 11:36:38   geaton
//Added validation for Event Log Source.
//
//   Rev 1.9   Oct 21 2003 08:54:00   geaton
//Added validation of Event Log Source property which was missing.
//
//   Rev 1.8   Oct 07 2003 13:40:44   geaton
//Updates following introduction of TDExceptionIdentifier.
//
//   Rev 1.7   Sep 30 2003 11:01:08   geaton
//Temporarily removed custom event class validation until establised that IsSubclassOf copes with inheritance hierarchies.
//
//   Rev 1.6   Aug 19 2003 16:07:52   geaton
//Relaxed validation of Custom Event classes (allow to be a subclass rather than baseclass of CustomEvent). This will allow clients to use custom event class hierarchies rather than only custom events that derive directly from CustomEvent.
//
//   Rev 1.5   Jul 24 2003 18:27:40   geaton
//Added/updated comments
//
//   Rev 1.4   Jul 23 2003 10:23:14   passuied
//Changes after PropertyService namespaces / dll renaming
//
//   Rev 1.3   Jul 17 2003 14:52:40   passuied
//corrected header

using System;
using System.Collections;
using System.Text;
using System.Messaging;
using System.Reflection;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Utility class used to validate logging specific properties.
	/// Derives from PropertyValidator which provides general
	/// validation methods.
	/// </summary>
	public class LoggingPropertyValidator : PropertyValidator
	{
		/// <summary>
		/// Maximum character length of email subject lines
		/// </summary>
		public const int MaxSubjectLength = 35;

		/// <summary>
		/// Constructor used to create class based on properties.
		/// </summary>
		/// <param name="properties">Properties that are used to perform validation against.</param>
		public LoggingPropertyValidator(IPropertyProvider properties) :
			base(properties)
		{}

		/// <summary>
		/// Used to validate a property with a given <c>key</c>.
		/// </summary>
		/// <param name="key">Key of property to validate.</param>
		/// <param name="errors">Holds validation errors if any.</param>
		/// <returns><c>true</c> if property is valid otherwise <c>false</c>.</returns>
		override public bool ValidateProperty(string key, ArrayList errors)
		{
			// A validation method is defined for every key that may be passed.

			if (key == Keys.QueuePublishers)
				return ValidateQueuePublishers(errors);
			else if (key == Keys.EmailPublishers)
				return ValidateEmailPublishers(errors);
			else if (key == Keys.FilePublishers)
				return ValidateFilePublishers(errors);
			else if (key == Keys.EventLogPublishers)
				return ValidateEventLogPublishers(errors);
			else if (key == Keys.OperationalTraceLevel)
				return ValidateOperationalEventTraceLevel(errors);
			else if (key == Keys.CustomPublishers)
				return ValidateCustomPublishers(errors);
			else if (key == Keys.DefaultPublisher)
				return ValidateDefaultPublisher(errors);
			else if (key == Keys.CustomEventsLevel)
				return ValidateCustomEventsLevel(errors);
			else if (key == Keys.CustomEvents)
				return ValidateCustomEvents(errors);
			else if (key == Keys.ConsolePublishers)
				return ValidateConsolePublishers(errors);
			else
			{
				string message = String.Format(Messages.LoggingPropertyValidatorKeyBad, key);
				throw new TDException(message, false, TDExceptionIdentifier.ELSLoggingPropertyValidatorUnknownKey);
			}
		}

		private bool ValidateOperationalEventTraceLevel(ArrayList errors)
		{
			int errorsBefore = errors.Count;

			// NB Can take any value in TDTraceLevel except Undefined
			ValidateEnumProperty(Keys.OperationalTraceLevel,
								 typeof(TDTraceLevel), Optionality.Mandatory, errors);

			if (String.Compare(properties[Keys.OperationalTraceLevel], "Undefined") == 0)
			{
				errors.Add(String.Format(TransportDirect.Common.Messages.PropertyValueBad,
										 Keys.OperationalTraceLevel,
										 properties[Keys.OperationalTraceLevel],
										 "Value cannot take value Undefined."));
				
			}

			return (errorsBefore == errors.Count);
		}

		private bool QueueExists(string path)
		{
			// NB. Not possible to use the Exists method since will not work for remote private queues.
			// Instead use CanWrite after creating a queue object based on the path.
			bool valid = true;
			MessageQueue queue = null;

			try
			{
				queue = new MessageQueue(path);
			}
			catch (ArgumentException)
			{
				valid = false;
				queue = null;
			}

			if (queue != null)
			{
				try
				{
					valid = queue.CanWrite;
				}
				catch (Exception)
				{
					// MS do not document exact exceptions for CanWrite so catch all
					valid = false;
				}
			}
			
			return valid;
		}

		private bool ValidateQueuePublishers(ArrayList errors)
		{
			if (!ValidateExistence(Keys.QueuePublishers,Optionality.Mandatory, errors))
				return false;

			string[] publisherIDs = null;
			string publishers = properties[Keys.QueuePublishers];
			if (publishers.Length != 0) // valid to have no publishers
				publisherIDs = publishers.Split(' ');
			else
				publisherIDs = new String[0];
			StringBuilder key = new StringBuilder(50);
			int errorsBefore = errors.Count;

			foreach (string publisherID in publisherIDs)
			{
				key.Length = 0;
				key.Append(String.Format(Keys.QueuePublisherPath, publisherID));
				
				if (ValidateExistence(key.ToString(),Optionality.Mandatory, errors))
				{
					if (!QueueExists(properties[key.ToString()]))
					{
						errors.Add(String.Format(Messages.QueuePublisherMSMQBad, 
												 properties[key.ToString()],
												 key.ToString()));
					}
				}

				key.Length = 0;
				key.Append(String.Format(Keys.QueuePublisherPriority, publisherID));

				if (ValidateExistence(key.ToString(),Optionality.Mandatory, errors))
					ValidateEnumProperty(key.ToString(),
										 typeof(MessagePriority), 
										 Optionality.Mandatory, errors);
						
				
			

				key.Length = 0;
				key.Append(String.Format(Keys.QueuePublisherDelivery, publisherID));

				if (ValidateExistence(key.ToString(),Optionality.Mandatory, errors))
				{
					if ( (String.Compare(properties[key.ToString()], "Express") != 0) &&
						 (String.Compare(properties[key.ToString()], "Recoverable") != 0) )
					{
						errors.Add(String.Format(Messages.QueuePublisherDeliveryBad, 
														   properties[key.ToString()],
														   key.ToString()));
					}
				}
				
					
			}
			
			return (errorsBefore == errors.Count);
		}

		private bool ValidateEmailPublishers(ArrayList errors)
		{
			if (!ValidateExistence(Keys.EmailPublishers,Optionality.Mandatory, errors))
				return false;

			string[] publisherIDs = null;
			string publishers = properties[Keys.EmailPublishers];
			if (publishers.Length != 0) // valid to have no publishers
				publisherIDs = publishers.Split(' ');
			else
				publisherIDs = new String[0];
			StringBuilder key = new StringBuilder(50);
			EmailAddress emailAddress = new EmailAddress();
			int errorsBefore = errors.Count;

			foreach (string publisherID in publisherIDs)
			{
				key.Length = 0;
				key.Append(String.Format(Keys.EmailPublisherTo, publisherID));

				if (ValidateExistence(key.ToString(),PropertyValidator.Optionality.Mandatory, errors))
				{
					if (!emailAddress.Parse(properties[key.ToString()]))
						errors.Add(String.Format(Messages.EmailPublisherAddressBad, 
												 properties[key.ToString()],
												 key.ToString()));

				}
					

				key.Length = 0;
				key.Append(String.Format(Keys.EmailPublisherFrom, publisherID));

				if (ValidateExistence(key.ToString(),PropertyValidator.Optionality.Mandatory, errors))
				{
					if (!emailAddress.Parse(properties[key.ToString()]))
						errors.Add(String.Format(Messages.EmailPublisherAddressBad, 
												 properties[key.ToString()],
												 key.ToString()));

				}

				key.Length = 0;
				key.Append(String.Format(Keys.EmailPublisherSubject, publisherID));

				if (ValidateExistence(key.ToString(),PropertyValidator.Optionality.Mandatory, errors))
					ValidateLength(key.ToString(), 1, MaxSubjectLength, errors);

				key.Length = 0;
				key.Append(String.Format(Keys.EmailPublisherPriority, publisherID));

				if (ValidateExistence(key.ToString(),PropertyValidator.Optionality.Mandatory, errors))
					ValidateEnumProperty(key.ToString(), typeof(MailPriority), Optionality.Mandatory, errors);
			}

			if (publisherIDs.Length > 0)
			{
				key.Length = 0;
				key.Append(String.Format(Keys.EmailPublishersSmtpServer));
			
				if (ValidateExistence(key.ToString(),Optionality.Mandatory, errors))
					ValidateLength(key.ToString(), 1, errors);
			}

			return (errorsBefore == errors.Count);
		}

		private bool ValidateCustomPublishers(ArrayList errors)
		{
			if (!ValidateExistence(Keys.CustomPublishers,Optionality.Mandatory, errors))
				return false;

			string[] publisherIDs = null;
			string publishers = properties[Keys.CustomPublishers];
			if (publishers.Length != 0) // valid to have no publishers
				publisherIDs = publishers.Split(' ');
			else
				publisherIDs = new String[0];
	

			StringBuilder key = new StringBuilder(50);
			
			int errorsBefore = errors.Count;

			foreach (string publisherID in publisherIDs)
			{
				key.Length = 0;
				key.Append(String.Format(Keys.CustomPublisherName, publisherID));

				if ( ValidateExistence(key.ToString(), Optionality.Mandatory, errors)) 
					 ValidateLength(key.ToString(), 1, errors);
					

			}

			return (errorsBefore == errors.Count);
		}

		private bool ValidateFilePublishers(ArrayList errors)
		{
			if (!ValidateExistence(Keys.FilePublishers,Optionality.Mandatory, errors))
				return false;

			string[] publisherIDs = null;
			string publishers = properties[Keys.FilePublishers];
			if (publishers.Length != 0) // valid to have no publishers
				publisherIDs = publishers.Split(' ');
			else
				publisherIDs = new String[0];

			StringBuilder key = new StringBuilder(50);
			int errorsBefore = errors.Count;
			
			foreach (string publisherID in publisherIDs)
			{
				key.Length = 0;
				key.Append(String.Format(Keys.FilePublisherDirectory, publisherID));
			
				if (ValidateExistence(key.ToString(), Optionality.Mandatory, errors))
				{
					if (!Directory.Exists(properties[key.ToString()]))
						errors.Add(String.Format(Messages.FilePublisherDirectoryBad, properties[key.ToString()], key.ToString()));
				}

				key.Length = 0;
				key.Append(String.Format(Keys.FilePublisherRotation, publisherID));

				if (ValidateExistence(key.ToString(), Optionality.Mandatory, errors))
				{
					if (ValidateLength(key.ToString(), 1, errors))
					{
						CultureInfo cultureInfo = new CultureInfo("en-US");
						string property = properties[key.ToString()];

						int rotation = int.Parse(property, cultureInfo);

						if (rotation <= 0)
							errors.Add(String.Format(Messages.FilePublisherRotationBad, properties[key.ToString()], key.ToString()));
							
					}
				}

			}

			return (errorsBefore == errors.Count);
		}

		

		private bool ValidateEventLogPublishers(ArrayList errors)
		{
			if (!ValidateExistence(Keys.EventLogPublishers,Optionality.Mandatory, errors))
				return false;

			string[] publisherIDs = null;
			string publishers = properties[Keys.EventLogPublishers];
			if (publishers.Length != 0) // valid to have no publishers
				publisherIDs = publishers.Split(' ');
			else
				publisherIDs = new String[0];
			StringBuilder key1 = new StringBuilder(50);
			StringBuilder key2 = new StringBuilder(50);
			bool nameValid = false;
			bool machineValid = false;
			int errorsBefore = errors.Count;

			foreach (string publisherID in publisherIDs)
			{
				nameValid = false;
				machineValid = false;

				key1.Length = 0;
				key1.Append(String.Format(Keys.EventLogPublisherName, publisherID));
				
				if (ValidateExistence(key1.ToString(), Optionality.Mandatory, errors))
				{
					if (properties[key1.ToString()].Equals("Application"))
					{
						nameValid = true; // configured to use standard Application Event Log
					}
					else
					{
						if (ValidateLength(key1.ToString(), 1, 8, errors))
							nameValid = true; // configured to use a custom event log
					}
				}
			
				key2.Length = 0;
				key2.Append(String.Format(Keys.EventLogPublisherMachine, publisherID));

				if (ValidateExistence(key2.ToString(), Optionality.Mandatory, errors))
				{
					if (ValidateLength(key2.ToString(), 1, errors))
						machineValid = true;
				}

				if (machineValid && nameValid)
				{
					try
					{
						if (!EventLog.Exists(properties[key1.ToString()], properties[key2.ToString()]))
						{
							errors.Add(String.Format(Messages.EventLogPublisherEventLogBad,
								properties[key1.ToString()],
								properties[key2.ToString()],
								key1.ToString(), key2.ToString()) ); 
						
						}
					}
					catch (Exception exception)
					{
						// MS have not documented all possible exceptions so catch all	
						errors.Add(String.Format(Messages.EventLogPublisherEventLogNotFound,
								   exception.Message,
								   key1.ToString(), key2.ToString()) ); 
					}
				}
				
				
				key1.Length = 0;
				key1.Append(String.Format(Keys.EventLogPublisherSource, publisherID));
				ValidateExistence(key1.ToString(), Optionality.Mandatory, errors);
				
			}

			return (errorsBefore == errors.Count);
		}
		
		private bool ValidateDefaultPublisher(ArrayList errors)
		{
			int errorsBefore = errors.Count;

			if (ValidateExistence(Keys.DefaultPublisher, Optionality.Mandatory, errors))
			{
				ValidateLength(Keys.DefaultPublisher, 1, errors);
			}
			

			return (errorsBefore == errors.Count);
		}

		private bool ValidateCustomEventsLevel(ArrayList errors)
		{
			int errorsBefore = errors.Count;

			if (ValidateExistence(Keys.CustomEventsLevel, Optionality.Mandatory, errors))
			{
				ValidateEnumProperty(Keys.CustomEventsLevel,
									 typeof(CustomEventLevel),
									 Optionality.Mandatory, errors);

				string property = properties[Keys.CustomEventsLevel];

				if (String.Compare(property, "Undefined") == 0)
				{
					errors.Add(String.Format(TransportDirect.Common.Messages.PropertyValueBad,
											 Keys.CustomEventsLevel, property,
											 "Value cannot take value Undefined."));
				
				}
			}

			return (errorsBefore == errors.Count);
		}
		/// <summary>
		/// Validates that the base class of a type matches a given type.
		/// </summary>
		private void ValidateBaseType(string key, Type requiredSubclass, 
									  Assembly assembly, ArrayList errors)
		{
			string classTypeName = properties[key];
			Type[] types = assembly.GetTypes();
												
			foreach (Type type in types) 
			{
				if (type.Name == classTypeName)
				{	
					if (!type.IsSubclassOf(requiredSubclass))
					{
						errors.Add(String.Format(Messages.IncorrectClassType,
								   classTypeName,
								   key,
								   requiredSubclass.FullName));
					}

					break;
				}
			}
		}

		private void ValidateCustomEvent(string eventId, ArrayList errors)
		{
			string nameKey = String.Format(Keys.CustomEventName, eventId);
			string assemblyKey = String.Format(Keys.CustomEventAssembly, eventId);
			string traceKey = String.Format(Keys.CustomEventLevel, eventId);
			bool nameValid = false;
			bool assemblyNameValid = false;
			
			if (ValidateExistence(nameKey, Optionality.Mandatory, errors))
			{
				if (ValidateLength(nameKey, 1, errors))
				{
					nameValid = true;
				}
			}
					
			if (ValidateExistence(assemblyKey, Optionality.Mandatory, errors))
			{
				if (ValidateLength(assemblyKey, 1, errors))
				{
					assemblyNameValid = true;
				}
			}

			if (nameValid && assemblyNameValid)
			{
				Assembly assembly = ValidateAssembly(assemblyKey, errors);

				if (assembly != null)
				{
					if (ValidateClassExists(nameKey, assembly, errors))
						ValidateBaseType(nameKey, typeof(CustomEvent), assembly, errors);					
				}
			}

			if (ValidateExistence(traceKey, Optionality.Mandatory, errors))
			{
				string property = properties[traceKey];

				ValidateEnumProperty(traceKey,
									 typeof(CustomEventLevel),
									 Optionality.Mandatory, errors);

				if (String.Compare(property, "Undefined") == 0)
				{
					errors.Add(String.Format(TransportDirect.Common.Messages.PropertyValueBad,
											 traceKey, property,
											 "Value cannot take value Undefined."));
				
				}
			}

		}

		private bool ValidateCustomEvents(ArrayList errors)
		{
			int errorsBefore = errors.Count;
			
			if (ValidateExistence(Keys.CustomEvents, Optionality.Mandatory, errors))
			{
				string[] eventIds = null;
				string events = properties[Keys.CustomEvents];
				if (events.Length != 0) // valid to have no custom events
					eventIds = events.Split(' ');
				else
					eventIds = new String[0];
	
				foreach(string eventId in eventIds)
					ValidateCustomEvent(eventId, errors);

			}
			
			return (errorsBefore == errors.Count);
		}

		private bool ValidateConsolePublishers(ArrayList errors)
		{			
			if (!ValidateExistence(Keys.ConsolePublishers,Optionality.Mandatory, errors))
				return false;

			string[] publisherIDs = null;
			string publishers = properties[Keys.ConsolePublishers];

			if (publishers.Length != 0) // valid to have no publishers
				publisherIDs = publishers.Split(' ');
			else
				publisherIDs = new String[0];
			StringBuilder key = new StringBuilder(50);
			int errorsBefore = errors.Count;

			foreach (string publisherID in publisherIDs)
			{
				key.Length = 0;
				key.Append(String.Format(Keys.ConsolePublisherStream, publisherID));
				
				string property = properties[key.ToString()];

				if (ValidateExistence(key.ToString(),Optionality.Mandatory, errors))
				{
					if ( (String.Compare(property, Keys.ConsolePublisherErrorStream) != 0) &&
						 (String.Compare(property, Keys.ConsolePublisherOutputStream) != 0) )
					{
						string usage = Keys.ConsolePublisherErrorStream + "|" + Keys.ConsolePublisherOutputStream;

						errors.Add(String.Format(TransportDirect.Common.Messages.PropertyValueBad,
												  key.ToString(), property, usage));
				
					}	
				}				
					
			}
			
			return (errorsBefore == errors.Count);
		}

	}
}
