// *********************************************** 
// NAME                 : TDTraceListener.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Listener class for monitoring
// logging output on TransportDirect.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/TDTraceListener.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:10   mturner
//Initial revision.
//
//   Rev 1.24   Jul 05 2004 11:44:56   passuied
//changes for Eventreceiver recovery
//Resolution for 1106: Event Receiver Recovery del6
//
//   Rev 1.23   Jul 05 2004 11:20:36   passuied
//changes for EventReceiver Recovery
//Resolution for 1048: EventReceiver failure Del6
//
//   Rev 1.22   Nov 27 2003 11:32:12   geaton
//Downgraded operational event level from error to warning when unsupported TDTraceListener prototpye is called.
//
//   Rev 1.21   Nov 21 2003 09:44:32   geaton
//Added AID and GID to exception message.
//
//   Rev 1.20   Nov 14 2003 11:08:54   geaton
//Added message of exception caught to logged op event.
//
//   Rev 1.19   Nov 10 2003 18:00:46   geaton
//Updated OnSuperseded to use Properties.Current. This method was originally using the PP passed in the argument which was the old PP.
//
//   Rev 1.18   Oct 20 2003 13:23:44   geaton
//Change to support PropertyValidator.StringToEnum becoming static method.
//
//   Rev 1.17   Oct 10 2003 15:17:24   geaton
//Removed logging of Operational events that may cause recursion.
//
//   Rev 1.16   Oct 09 2003 13:11:38   geaton
//Support for error handling should an unknown event class be logged.
//
//   Rev 1.15   Oct 07 2003 13:40:48   geaton
//Updates following introduction of TDExceptionIdentifier.
//
//   Rev 1.14   Sep 22 2003 17:07:30   geaton
//Log failed write events as separate op events.
//
//   Rev 1.13   Sep 18 2003 12:12:22   geaton
//Log Op Event for info when property changes occur at runtime.
//
//   Rev 1.12   Sep 16 2003 15:05:56   geaton
//Corrected validation of custom event publishers. One or more valid publisher ids must be defined for each custom event.
//
//   Rev 1.11   Sep 04 2003 18:52:02   geaton
//Log Op Events on unsupported prototypes. This allows use of standard listeners during development without exceptions being thrown.
//
//   Rev 1.10   Aug 22 2003 10:16:58   geaton
//Added code to log operational event warning if configured publisher fails and default publisher used instead. Also refactored Write() to remove some reflection calls to improve performance.
//
//   Rev 1.9   Jul 30 2003 18:45:08   geaton
//Removed redundant constructor from OperationalEvent class and updated references to use alternative constructor.
//
//   Rev 1.8   Jul 30 2003 12:03:06   geaton
//Updated comments.
//
//   Rev 1.7   Jul 29 2003 17:31:42   geaton
//Added TestOperationalEvent. Removed referencenumber property from LogEvent and changed OperationalEvent constructors
//
//   Rev 1.6   Jul 28 2003 18:18:32   geaton
//Added overrides for Write and WriteLine - these are not implemented but throw a TDException if called by clients. This is done to make clients aware that trace info is not being logged should they call any prototype other than Write(Object).
//
//   Rev 1.5   Jul 25 2003 14:14:42   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).
//
//   Rev 1.4   Jul 24 2003 18:27:54   geaton
//Added/updated comments


using System;
using System.Diagnostics;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.Common.Logging
{
	public class TraceLevelEventArgs : EventArgs
	{
		public TraceLevelEventArgs(TDTraceLevel traceLevel)
		{
			this.traceLevel = traceLevel;
		}

		private TDTraceLevel traceLevel;
		/// <summary>
		/// Gets the trace level held in the event args instance.
		/// </summary>
		public TDTraceLevel TraceLevel
		{
			get{return traceLevel;}
		}
	}

	public class CustomLevelEventArgs : EventArgs
	{
		public CustomLevelEventArgs(CustomEventLevel customLevel)
		{
			this.customLevel = customLevel;
		}

		private CustomEventLevel customLevel;
		/// <summary>
		/// Gets the custom event level held in the event args instance.
		/// </summary>
		public CustomEventLevel CustomLevel
		{
			get{return customLevel;}
		}
	}

	public class CustomLevelsEventArgs : EventArgs
	{
		public CustomLevelsEventArgs(Hashtable customLevels)
		{
			this.customLevels = customLevels;
		}

		private Hashtable customLevels;
		/// <summary>
		/// Gets the custom event levels held in the event args instance.
		/// </summary>
		public Hashtable CustomLevels
		{
			get{return customLevels;}
		}
	}


	delegate void PublisherDelegate(LogEvent le);

	public delegate void TraceLevelChangeEventHandler(object sender, TraceLevelEventArgs e);
	public delegate void CustomLevelChangeEventHandler(object sender, CustomLevelEventArgs e);
	public delegate void CustomLevelsChangeEventHandler(object sender, CustomLevelsEventArgs e);

	public delegate void DefaultPublisherCalledEventHandler(object sender, DefaultPublisherCalledEventArgs e);
	public class DefaultPublisherCalledEventArgs
	{
		LogEvent le = null;
		public DefaultPublisherCalledEventArgs(LogEvent le)
		{
			this.le = le;
		}

		public LogEvent LogEvent
		{
			get
			{
				return le;
			}
		}
	}
	/// <summary>
	/// Class used to listen for logging output.
	/// An instance of this class should be added to the trace listener set.
	/// </summary>
	/// <example> 
	/// Trace.Listeners.Add(new TDTraceListener(properties,
	///											customPublishers,
	///											errors));
	/// </example>
	public class TDTraceListener : TraceListener
	{
	
		// Need declare events as static since subscribers (all of which are static)
		// may register for events before an instance of TDTraceListener is created.
		public static event TraceLevelChangeEventHandler OperationalTraceLevelChange;
		public static event CustomLevelChangeEventHandler CustomLevelChange;
		public static event CustomLevelsChangeEventHandler CustomLevelsChange;

		private int defaultPublisherIndex;
		
		private LoggingPropertyValidator validator;

		PublisherDelegate [][] eventPublisherMappings;
		Hashtable eventIndexTable;
	
		/// <summary>
		/// Called by properties service to notify changes in property values/
		/// Note that the property changes may or may not be relevent to those used by the Event Logging Service.
		/// </summary>
		/// <param name="sender">Reference to the old (superseded) property provider.</param>
		/// <param name="s">Not currently used.</param>
		private void OnSuperseded(object sender, System.EventArgs s)
		{
			if (TDTraceSwitch.TraceInfo)
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure,
												 TDTraceLevel.Info,
												 Messages.TDTraceListenerPropertyChange));
			
			NotifySubscribers(Properties.Current);
			
			// Re-register handler with the (new) latest properties.
			Properties.Current.Superseded += new SupersededEventHandler(this.OnSuperseded);
		}
		
		/// <summary>
		/// Constructor for TDTraceListener instances.
		/// </summary>
		/// <param name="properties">Properties used to create listener.</param>
		/// <param name="customPublishers">Array containing zero or more custom event publishers instances. 
		/// Note that this parameter cannot be null, though can be an empty array.</param>
		/// <param name="errors">Holds any errors that occurred during construction.</param>
		/// <exception cref="TransportDirect.Common.TDException">Unrecoverable error occured when creating the TDTraceListener instance.</exception>
		public TDTraceListener(IPropertyProvider properties,
			IEventPublisher[] customPublishers,
			ArrayList errors) : base( "TDTraceListener" )
		{
			this.validator = new LoggingPropertyValidator(properties);

			PublisherGroup standardPublisherGroup = new EventPublisherGroup(properties);
			standardPublisherGroup.CreatePublishers(errors);

			if (customPublishers == null)
				errors.Add(Messages.CustomPublisherArrayBad);
			
			if (errors.Count == 0)
				ValidateCustomPublishers(properties, customPublishers, errors);
			
			if (errors.Count == 0)
			{
				ValidateDefaultPublisher(properties,standardPublisherGroup.Publishers, customPublishers, errors); 
				ValidateEventProperties(properties, standardPublisherGroup.Publishers, customPublishers, errors);
			}

			if (errors.Count == 0)
				AssignEventsToPublishers(properties, standardPublisherGroup.Publishers, customPublishers);
			
			if (errors.Count == 0)
			{
				// subscribe to ANY changes that may happen in properties
				properties.Superseded += new SupersededEventHandler(this.OnSuperseded);				
			}

			if (errors.Count == 0)
			{
				// Force creation of any static classes that must subscribe to events 
				// generated by TDTraceListener.
				// This is necessary in case they have not yet been created.
				// (Since static classes are created on first use.)
				TDTraceSwitch tdTraceSwitch = new TDTraceSwitch();
				CustomEventSwitch customEventSwitch = new CustomEventSwitch();

				// Notify subscribers that TDTraceListener has been created successfully
				if (!NotifySubscribers(properties))
					errors.Add(Messages.TDTraceListenerSubscriberNotificationFailed);
			}

			if (errors.Count != 0)
			{		
				throw new TDException(String.Format(Messages.TDTraceListenerConstructor,
													ConfigurationManager.AppSettings["propertyservice.applicationid"],
													ConfigurationManager.AppSettings["propertyservice.groupid"]),
													false, TDExceptionIdentifier.ELSTDTraceListenerConstructor);
			}
		}

		/// <summary>
		/// Notifies subscribers that one or more properties have changed.
		/// NB property values are forwarded regardless of whether they have changed
		/// Property values are validated before they are forwarded.
		/// The 'Current' properties are always used.
		/// </summary>
		/// <param name="properties">Latest properties values.</param>
		/// <returns>
		/// Result. True if subscribers were notified without errors, otherwise false if errors occurred. 
		/// </returns>
		private bool NotifySubscribers(IPropertyProvider properties)
		{
			ArrayList errors = new ArrayList();
			bool result = true;

			if (validator.ValidateProperty(Keys.OperationalTraceLevel, errors))
			{
				string operationalTraceLevel = properties[Keys.OperationalTraceLevel];
				TDTraceLevel operationalTraceLevelValue 
					= (TDTraceLevel)PropertyValidator.StringToEnum(typeof(TDTraceLevel), operationalTraceLevel);
			
				if (OperationalTraceLevelChange != null)
					OperationalTraceLevelChange(this, new TraceLevelEventArgs(operationalTraceLevelValue));
			}

			if (validator.ValidateProperty(Keys.CustomEventsLevel, errors))
			{
				string level = properties[Keys.CustomEventsLevel];
				CustomEventLevel levelValue 
					= (CustomEventLevel)PropertyValidator.StringToEnum(typeof(CustomEventLevel), level);
			
				if (CustomLevelChange != null)
					CustomLevelChange(this, new CustomLevelEventArgs(levelValue));
			}

			if (validator.ValidateProperty(Keys.CustomEvents, errors))
			{
				Hashtable levels = new Hashtable();
				
				string[] eventIds = null;
				string events = properties[Keys.CustomEvents];
				if (events.Length != 0) // valid to have no custom events
					eventIds = events.Split(' ');
				else
					eventIds = new String[0];
	
				foreach(string eventId in eventIds)
				{
					string key = properties[String.Format(Keys.CustomEventName, eventId)];
					string level = properties[String.Format(Keys.CustomEventLevel, eventId)];

					levels[key] = (CustomEventLevel)PropertyValidator.StringToEnum(typeof(CustomEventLevel), level);
				}

				if (CustomLevelsChange != null)
					CustomLevelsChange(this, new CustomLevelsEventArgs(levels));
			}
			
			if (errors.Count != 0)
			{
				// Write any validation errors to default publisher
				// as operational events.
				// The error text is used as the message
				// of the operational event.
				for(int i=0; i<errors.Count; i++)
				{
					// create an operational event representing the error
					OperationalEvent operationalEvent = 
						new OperationalEvent(TDEventCategory.Infrastructure,
						TDTraceLevel.Error,
						errors[i].ToString());

					// write the operational event to the default publisher to ensure that is has a higher chance of being published.
					eventPublisherMappings[defaultPublisherIndex][0](operationalEvent);
				}

				result = false;
			}

			return result;
		}		

		public event DefaultPublisherCalledEventHandler DefaultPublisherCalled;
		private void AssignEventToPublishers(IPropertyProvider properties, 
			string pubishersPropertyKey,
			int eventArrayIndexer,
			string eventHashIndexer,
			ArrayList standardPublishers,
			IEventPublisher[] customPublishers)
		{

			string[] publisherIds = null;
			string publishers = properties[pubishersPropertyKey];
			publisherIds = publishers.Split(' ');
			
			int publisherCount = 0;

			PublisherDelegate publisherDelegate = null;

			// allocate a row in the array to store each of the publishers delegates
			eventPublisherMappings[eventArrayIndexer] 
				= new PublisherDelegate[publisherIds.GetLength(0)];

			// iterate through publishers to get a delagates
			foreach (string publisher in publisherIds)
			{	
				// search for publisher instance in standard publishers
				foreach (IEventPublisher publisherInstance in standardPublishers)
				{
					if (publisherInstance.Identifier == publisher)
					{
						publisherDelegate = new PublisherDelegate(publisherInstance.WriteEvent);
						break;
					}
				}

				// if publisher not found in standard publishers then search custom publishers
				if (publisherDelegate == null)
				{
					foreach (IEventPublisher publisherInstance in customPublishers)
					{
						if (publisherInstance.Identifier == publisher)
						{
							publisherDelegate = new PublisherDelegate(publisherInstance.WriteEvent);
							break;	
						}
					}
				}

				// store delegate in array for this publisher
				eventPublisherMappings[eventArrayIndexer][publisherCount] 
					= publisherDelegate;

				// store array indexer in hash table - this is used for fast lookup into array
				eventIndexTable[eventHashIndexer] = eventArrayIndexer;

				publisherCount++;
				publisherDelegate = null;

			}

		}

		private void AssignEventsToPublishers(IPropertyProvider properties,
			ArrayList standardPublishers,
			IEventPublisher[] customPublishers)
		{
			// determine number of event types
			string[] customEventIds = null;
			string customEvents = properties[Keys.CustomEvents];
			if (customEvents.Length != 0) // valid to have no custom events
				customEventIds = customEvents.Split(' ');
			else
				customEventIds = new String[0];

			int numOfEvents = customEventIds.GetLength(0) + 
				typeof(TDTraceLevel).GetFields().GetLength(0) - 2; // values for Undefined and Off are not used for events
	
			numOfEvents++; // add an extra row to hold the default publisher
			this.defaultPublisherIndex = numOfEvents - 1; // sub 1 since zero based array

			// create array to hold publisher writelog delegates
			eventPublisherMappings = new PublisherDelegate[numOfEvents][];

			// create hash table to store event index lookups
			eventIndexTable = new Hashtable();

			// initialise indexer used to for each event row in the array
			int eventArrayIndexer = 0;
			
			// Set up operational event mappings.
			// Need to use level property to make hash table indexer unique
			AssignEventToPublishers(properties, 
				Keys.OperationalEventErrorPublishers,					
				eventArrayIndexer,
				OperationalEvent.ClassNamePrefix + TDTraceLevel.Error.ToString(),
				standardPublishers, customPublishers);

			eventArrayIndexer++;

			AssignEventToPublishers(properties, 
				Keys.OperationalEventWarningPublishers,
				eventArrayIndexer,
				OperationalEvent.ClassNamePrefix + TDTraceLevel.Warning.ToString(),
				standardPublishers, customPublishers);

			eventArrayIndexer++;

			AssignEventToPublishers(properties, 
				Keys.OperationalEventVerbosePublishers,
				eventArrayIndexer,
				OperationalEvent.ClassNamePrefix + TDTraceLevel.Verbose.ToString(),
				standardPublishers, customPublishers);

			eventArrayIndexer++;
			
			AssignEventToPublishers(properties, 
				Keys.OperationalEventInfoPublishers,
				eventArrayIndexer,
				OperationalEvent.ClassNamePrefix + TDTraceLevel.Info.ToString(),
				standardPublishers, customPublishers);

			eventArrayIndexer++;

			
			// Set up custom event publisher mappings
			foreach(string customEventId in customEventIds)
			{
				string ceName = properties[String.Format(Keys.CustomEventName, customEventId)];

				AssignEventToPublishers(properties, 
					String.Format(Keys.CustomEventPublishers, customEventId),
					eventArrayIndexer,
					ceName,
					standardPublishers, customPublishers);

				eventArrayIndexer++;
			}

			// set up default publisher
			AssignEventToPublishers(properties, 
				Keys.DefaultPublisher,
				this.defaultPublisherIndex,
				"Default", // irrelevent since not used at write time
				standardPublishers, customPublishers);

		}

		/// <summary>
		/// Publishes a LogEvent object to the appropriate sink as specified in the 
		/// property provider used to create the TDTraceListener instance.
		/// </summary>
		/// <remarks>
		/// If the event cannot be published by it's configured publisher then an attempt
		/// is made to publish it using the default publisher.
		/// </remarks>
		/// <param name="o"><c>LogEvent</c> to publish.</param>
		/// <exception cref="TransportDirect.Common.TDException">An error condition prevented the <c>LogEvent</c> from being published by any publisher, including the default publisher.</exception>
		public override void Write(object o)
		{
			int eventArrayIndex = 0;
			LogEvent le = null;

			// Cast object being logged to a LogEvent class instance
			try
			{
				le = (LogEvent)o;
			}
			catch (InvalidCastException invalidCastException)
			{
				throw new TDException(Messages.TDTraceListenerUnknownClass, invalidCastException, false, TDExceptionIdentifier.ELSTDTraceListenerUnknownObject);
			}
			catch (Exception exception) // catch undocumented exceptions
			{
				throw new TDException(Messages.TDTraceListenerUnknownClass, exception, false, TDExceptionIdentifier.ELSTDTraceListenerUnknownObject);
			}

			// Check if event type should be logged before proceeding
			if (!le.Filter.ShouldLog(le))
				return;	

			// Determine quick lookup index of event based on it's class name
			eventArrayIndex = (int)eventIndexTable[le.ClassName];


			// Loop through all publishers assigned to event, calling WriteEvent
			for (int publisher = 0; 
				publisher < eventPublisherMappings[eventArrayIndex].GetLength(0);
				publisher++)
			{
				try
				{
					// Call WriteEvent for this publisher
					eventPublisherMappings[eventArrayIndex][publisher](le);

					
					// Audit the fact that the event has been published.
					if (!le.AuditPublishersOff)
					{
						le.PublishedBy = eventPublisherMappings[eventArrayIndex][publisher].Target.GetType().FullName;
					}


				}
				catch (TDException tdException1)
				{

					// Log an error, if exception not already logged.
					// Only log for custom events to prevent infinite recursion
					// should the operational event fail to write.
					if ((le is CustomEvent) && (!tdException1.Logged))
					{
						OperationalEvent error = 
							new OperationalEvent( TDEventCategory.Infrastructure,
												  TDTraceLevel.Error,
												  String.Format(Messages.ConfiguredPublisherFailed, le.ClassName, eventPublisherMappings[eventArrayIndex][publisher].Target.GetType().FullName, tdException1.Message),
												  tdException1);

						Trace.Write(error);
					}

					// Failed to log using configured publisher so 
					// attempt to log event using the default publisher
					try
					{
						if (DefaultPublisherCalled != null)
							DefaultPublisherCalled(this, new DefaultPublisherCalledEventArgs(le));

						eventPublisherMappings[this.defaultPublisherIndex][0](le);
						
						// audit the fact that the event has been published by the default publisher
						if (!le.AuditPublishersOff)
							le.PublishedBy = eventPublisherMappings[this.defaultPublisherIndex][0].Target.GetType().FullName;

						// Log a warning to highlight that default publisher was used.
						// Only report this for custom events to prevent infinite
						// recursion should the operational event fail to write.
						if (le is CustomEvent)
						{
							OperationalEvent warning = 
								new OperationalEvent(TDEventCategory.Infrastructure,
													 TDTraceLevel.Warning,
													 String.Format(Messages.TDTraceListenerDefaultPublisherUsed, le.ClassName));

							Trace.Write(warning);
						}

					}
					catch (TDException tdException2)
					{
						// All publishers failed to log! This exception will filter up to the td trace listener client.
						throw new TDException(Messages.TDTraceListenerDefaultPublisherFailure, tdException2, false, TDExceptionIdentifier.ELSDefaultPublisherFailed);
					}
				}

			}

		}

		/// <summary>
		/// This prototype is not supported by TDTraceListener.
		/// An OperationalEvent is logged to highlight that it has been called.
		/// </summary>
		public override void Write(string s)
		{
			Write(new OperationalEvent(TDEventCategory.Infrastructure,
									   TDTraceLevel.Warning,
									   String.Format(Messages.TDTraceListenerUnsupportedPrototype, s)));
		}

		/// <summary>
		/// This prototype is not supported by TDTraceListener.
		/// An OperationalEvent is logged to highlight that it has been called.
		/// </summary>
		public override void Write(object o, string s)
		{
			this.Write(String.Format("{0},{1}", s, o.ToString()));
		}

		/// <summary>
		/// This prototype is not supported by TDTraceListener.
		/// An OperationalEvent is logged to highlight that it has been called.
		/// </summary>
		public override void Write(string message, string category)
		{
			this.Write(String.Format("{0},{1}", message, category));
		}

		/// <summary>
		/// This prototype is not supported by TDTraceListener.
		/// An OperationalEvent is logged to highlight that it has been called.
		/// </summary>
		public override void WriteLine(string s)
		{
			this.Write(s);
		}

		/// <summary>
		/// This prototype is not supported by TDTraceListener.
		/// An OperationalEvent is logged to highlight that it has been called.
		/// </summary>
		public override void WriteLine(object o, string s)
		{
			this.Write(String.Format("{0},{1}", s, o.ToString()));
		}

		/// <summary>
		/// This prototype is not supported by TDTraceListener.
		/// An OperationalEvent is logged to highlight that it has been called.
		/// </summary>
		public override void WriteLine(object o)
		{
			this.Write(String.Format(o.ToString()));
		}

		/// <summary>
		/// This prototype is not fully supported by TDTraceListener.
		/// An OperationalEvent is logged to highlight that it has been called.
		/// </summary>
		public override void WriteLine(string message, string category)
		{
			this.Write(String.Format("{0},{1}", message, category));
		}


		private void ValidateCustomPublishers(IPropertyProvider properties,
			IEventPublisher[] publishers,
			ArrayList errors)
		{
			StringBuilder key = new StringBuilder(50);

			foreach (IEventPublisher publisher in publishers)
			{
			
				if ((publisher.Identifier.Length == 0) ||
					(publisher.Identifier == null))
				{
					errors.Add(String.Format(Messages.CustomPublisherInvalidId, 
						publisher.GetType().Name,
						publisher.Identifier));
				}
				else
				{			
					key.Length = 0;
					key.Append(String.Format(Keys.CustomPublisherName, publisher.Identifier));

					if (properties[key.ToString()] != publisher.GetType().Name)
					{
						errors.Add(String.Format(Messages.CustomPublisherNameMismatch, 
							publisher.GetType().Name,
							key.ToString() ));
					
					}

				}
				

			}
			
		}

		private bool ValidatePublisherExists(string publisherId,
			string key,
			ArrayList standardPublishers,
			IEventPublisher[] customPublishers,
			ArrayList errors)
		{
			bool found = false;

			foreach (IEventPublisher publisher in standardPublishers)
			{
				if (publisher.Identifier == publisherId)
				{
					found = true;
					break;
				}
			}

			if (!found)
			{
				foreach (IEventPublisher publisher in customPublishers)
				{
					if (publisher.Identifier == publisherId)
					{
						found = true;
						break;
					}
				}
			}

			if (!found)
			{
				errors.Add(String.Format(Messages.UndefinedPublisher, key, publisherId));
			}

			return found;
		}

		private void ValidatePublishersExist(IPropertyProvider properties, 
											 string eventKey, 
											 ArrayList standardPublishers,
											 IEventPublisher[] customPublishers,
											 ArrayList errors)
		{
			
			string[] Ids = null;
			string publisherIds = properties[eventKey];

			if (publisherIds != null)
			{
				if (publisherIds.Length != 0)
					Ids = publisherIds.Split(' ');
				else
				{
					errors.Add(String.Format(Messages.EventHasZeroPublishers, eventKey));
					Ids = new String[0];
				}
			}
			else
			{
				errors.Add(String.Format(Messages.EventHasZeroPublishers, eventKey));
				Ids = new String[0];
			}

			foreach (string id in Ids)
			{
				ValidatePublisherExists(id, eventKey, standardPublishers, customPublishers, errors);	
			}
		}

		private void ValidateEventProperties(IPropertyProvider properties, 
			ArrayList standardPublishers,
			IEventPublisher[] customPublishers,
			ArrayList errors)
		{
			// validate trace levels
			validator.ValidateProperty(Keys.OperationalTraceLevel, errors);
			validator.ValidateProperty(Keys.CustomEventsLevel, errors);
			
			// validate publisher property for all custom events
			string[] customEventIds = null;
			string customEvents = properties[Keys.CustomEvents];
			if (customEvents != null)
			{
				if (customEvents.Length != 0) // valid to have no custom events
					customEventIds = customEvents.Split(' ');
				else
					customEventIds = new String[0];
			}
			else
			{
				// NB validation of missing custom events key is performed elsewhere
				customEventIds = new String[0];
			}

			foreach (string customEventId in customEventIds)
			{	
				ValidatePublishersExist(properties,
										String.Format(Keys.CustomEventPublishers, customEventId),
										standardPublishers, 
										customPublishers, errors);
			}

			// validate publishers for operational events
			ValidatePublishersExist(properties,
									Keys.OperationalEventVerbosePublishers,
									standardPublishers, customPublishers, errors);

			ValidatePublishersExist(properties,
								    Keys.OperationalEventInfoPublishers,
									standardPublishers, customPublishers, errors);

			ValidatePublishersExist(properties,
									Keys.OperationalEventErrorPublishers,
									standardPublishers, customPublishers, errors);

			ValidatePublishersExist(properties,
									Keys.OperationalEventWarningPublishers,
									standardPublishers, customPublishers, errors);

			
			// validate "non-publisher properties" for all custom events
			validator.ValidateProperty(Keys.CustomEvents, errors);

		}

		private void ValidateDefaultPublisher(IPropertyProvider properties, 
			ArrayList standardPublishers,
			IEventPublisher[] customPublishers,
			ArrayList errors)
		{
			if (validator.ValidateProperty(Keys.DefaultPublisher, errors))
				ValidatePublisherExists(properties[Keys.DefaultPublisher],
										Keys.DefaultPublisher,
										standardPublishers, customPublishers, errors);

					
		}

	}
}
