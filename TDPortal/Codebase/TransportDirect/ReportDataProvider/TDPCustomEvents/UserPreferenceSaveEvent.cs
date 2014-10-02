// ************************************************* 
// NAME                 : UserPreferenceSaveEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 18/09/2003 
// DESCRIPTION  : Defines a custom event for logging
// user preference save event data.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/UserPreferenceSaveEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:38   mturner
//Initial revision.
//
//   Rev 1.1   Jun 28 2004 15:39:34   passuied
//Fix for the Event Receiver
//
//   Rev 1.0   Sep 18 2003 16:40:50   geaton
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
	public class UserPreferenceSaveEvent : TDPCustomEvent	
	{
		private UserPreferenceSaveEventCategory eventCategory;

		private static UserPreferenceSaveEventFileFormatter fileFormatter = new UserPreferenceSaveEventFileFormatter();
		
		/// <summary>
		/// Constructor for a <c>UserPreferenceSaveEvent</c> class. 
		/// A <c>UserPreferenceSaveEvent</c> is used
		/// to log User Preference Save data using the Event Service.
		/// This class must be serializable to allow logging to MSMQs.
		/// </summary>
		/// <param name="sessionId">The session id used to perform map event.</param>
		/// <param name="UserPreferenceSaveEventCategory">The event category that uniquely identifies the type of save event.</param>
		public UserPreferenceSaveEvent(UserPreferenceSaveEventCategory eventCategory,					  
									   string sessionId) : base(sessionId, true)
		{
			this.eventCategory = eventCategory;
		}

		
		/// <summary>
		/// Gets the save preference category.
		/// </summary>
		public UserPreferenceSaveEventCategory EventCategory
		{
			get{return eventCategory;}
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

