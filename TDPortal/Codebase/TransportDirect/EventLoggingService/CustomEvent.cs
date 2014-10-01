// *********************************************** 
// NAME                 : CustomEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Abstract class that defines
// a custom event.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/CustomEvent.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:00   mturner
//Initial revision.
//
//   Rev 1.6   Sep 16 2003 15:56:46   geaton
//Determine class name directly using object property.
//
//   Rev 1.5   Aug 22 2003 10:19:28   geaton
//Moved ClassName property up to base class LogEvent since this is common to all event types.
//
//   Rev 1.4   Aug 21 2003 15:55:38   geaton
//Removed 'fields' attribute. This type of data should be added in classes deriving from CustomEvent.
//
//   Rev 1.3   Jul 29 2003 17:31:18   geaton
//Added TestOperationalEvent. Removed referencenumber property from LogEvent and changed OperationalEvent constructors
//
//   Rev 1.2   Jul 25 2003 14:14:26   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).
//
//   Rev 1.1   Jul 24 2003 18:27:26   geaton
//Added/updated comments

using System;
using System.Text;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Abstract class that is used by clients to define their own event types.
	/// </summary>
	[Serializable]
	public abstract class CustomEvent : LogEvent
	{
		
		/// <summary>
		/// The default formatter. This is used should clients of this class not provide
		/// a custom formatter. Note that the default formatter will not format
		/// any custom data that clients may include in their custom event classes.
		/// </summary>
		private static IEventFormatter defaultFormatter = new DefaultFormatter();
		
		/// <summary>
		/// The filter class that is used to determine whether the custom event should
		/// be logged or not.
		/// </summary>
		private static IEventFilter filter = new CustomEventFilter();


		/// <summary>
		/// Gets the default event formatter.
		/// </summary>
		public IEventFormatter DefaultFormatter
		{
			get {return defaultFormatter;}
		}

		/// <summary>
		/// Gets the event filter.
		/// </summary>
		override public IEventFilter Filter
		{
			get {return filter;}
		}

		/// <summary>
		/// Construct a new CustomEvent
		/// </summary>
		protected CustomEvent() : base()
		{
			// set class name - used as an id to associate events to publishers, and also in config properties
			this.ClassName = this.GetType().Name;
		}

	}
}
