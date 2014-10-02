// *********************************************** 
// NAME                 : TDPCustomEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 18/08/2003 
// DESCRIPTION  : Defines a base custom event class
// from which RDP TDP Custom Events can derive.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/TDPCustomEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:34   mturner
//Initial revision.
//
//   Rev 1.5   Oct 29 2003 19:58:58   geaton
//Define instance of DefaultFormatter - this is used rather than passing nulls which is bad.
//
//   Rev 1.4   Oct 02 2003 10:04:30   geaton
//Added Serializable attribute to allow logging to MSMQs.
//
//   Rev 1.3   Sep 16 2003 11:09:50   geaton
//Added extra comments following code review.
//
//   Rev 1.2   Aug 21 2003 11:51:36   geaton
//Made class abstract to prevent logging of instances of this class.
//
//   Rev 1.1   Aug 20 2003 10:39:12   geaton
//updated comments and fields

using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;


namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Defines a base class for all TDPCustomEvents using
	/// by Report Data Provider components.
	/// This class defines event data that is common across
	/// all TDP Custom Events.
	/// </summary>
	[Serializable]
	public abstract class TDPCustomEvent : CustomEvent
	{
		private bool userLoggedOn;
		private string sessionId;
		private static DefaultFormatter formatter = new DefaultFormatter();

		/// <summary>
		/// Gets the session identifier
		/// </summary>
		public string SessionId
		{
			get{return sessionId;}
		}

		/// <summary>
		/// Gets the user logged on flag
		/// </summary>
		public bool UserLoggedOn
		{
			get{return userLoggedOn;}
		}
		
		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <remarks>
		/// Instances of this class will never be logged. 
		/// Only subclasses may be loggged.
		/// </remarks>
		/// <param name="sessionId">Session identifier that identifies session under which event was logged.</param>
		/// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or an unregistered user is logged on (false)</param>
		protected TDPCustomEvent(string sessionId, bool userLoggedOn) : base()
		{
			this.sessionId = sessionId;
			this.userLoggedOn = userLoggedOn;
		}

		/// <summary>
		/// Provides an event formatter for publishing to files.
		/// </summary>
		override public IEventFormatter FileFormatter
		{
			get {return formatter;}
		}

		/// <summary>
		/// Provides an event formatting for publishing to email.
		/// </summary>
		override public IEventFormatter EmailFormatter
		{
			get {return formatter;}
		}

		/// <summary>
		/// Provides an event formatter for publishing to event logs
		/// </summary>
		override public IEventFormatter EventLogFormatter
		{
			get {return formatter;}
		}

		/// <summary>
		/// Provides an event formatter for publishing to console.
		/// </summary>
		override public IEventFormatter ConsoleFormatter
		{
			get {return formatter;}
		}
	}
}
