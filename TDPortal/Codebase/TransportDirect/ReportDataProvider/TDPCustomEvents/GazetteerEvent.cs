// *********************************************** 
// NAME                 : GazetteerEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 18/08/2003 
// DESCRIPTION  : Defines a custom event for logging
// gazetteer event data.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/GazetteerEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:22   mturner
//Initial revision.
//
//   Rev 1.4   Jul 15 2004 18:04:40   acaunt
//Modified so that GazetteerEvent is now passed submitted time.
//
//   Rev 1.3   Jun 28 2004 16:08:52   passuied
//Fix for EventReceiver
//Resolution for 1049: EventReceiver failure del5.4
//
//   Rev 1.3   Jun 28 2004 15:39:36   passuied
//Fix for the Event Receiver
//
//   Rev 1.2   Sep 16 2003 11:09:34   geaton
//Added extra comments following code review.
//
//   Rev 1.1   Aug 28 2003 17:05:34   geaton
//Moved enumeration  category to separate file to follow standards.
//
//   Rev 1.0   Aug 20 2003 10:41:26   geaton
//Initial Revision

using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Defines the class for capturing Gazetteer Event data.
	/// </summary>
	[Serializable]
	public class GazetteerEvent : TDPCustomEvent	
	{
		private GazetteerEventCategory eventCategory;

		private DateTime submitted;

		private static GazetteerEventFileFormatter fileFormatter = new GazetteerEventFileFormatter();


		/// <summary>
		/// Constructor for a <c>GazetteerEvent</c> class. 
		/// A <c>GazetteerEvent</c> is used
		/// to log Gazetteer transaction data using the Event Service.
		/// This class must be serializable to allow logging to MSMQs.
		/// </summary>
		/// <param name="sessionId">The session id used to perform map event.</param>
		/// <param name="eventCategory">The event category that uniquely identifies the type of Gazetteer event.</param>
		/// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or not (false).</param>
		public GazetteerEvent(GazetteerEventCategory eventCategory,	
							  DateTime submitted,				  
							  string sessionId, 
							  bool userLoggedOn) : base(sessionId, userLoggedOn)
		{
			this.eventCategory = eventCategory;
			this.submitted = submitted;
		}

		/*
		public GazetteerEvent(GazetteerEventCategory eventCategory,	
			string sessionId, 
			bool userLoggedOn) : this(eventCategory, DateTime.Now, sessionId, userLoggedOn)
		{
		}
		*/	
		/// <summary>
		/// Gets the gazetteer event type classifier
		/// </summary>
		public GazetteerEventCategory EventCategory
		{
			get{return eventCategory;}
		}

		/// <summary>
		/// Returns the time when the event was submitted to the Gazetteer
		/// </summary>
		public DateTime Submitted
		{
			get {return submitted;}
		}

		/// <summary>
		/// Provides an event formatter for publishing to files.
		/// </summary>
		override public IEventFormatter FileFormatter
		{
			get {return fileFormatter;}
		}
		
	}

}
