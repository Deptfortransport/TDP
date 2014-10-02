// *********************************************** 
// NAME                 : UserFeedbackEvent.cs 
// AUTHOR               : Joe Morrissey
// DATE CREATED         : 16/07/2004 
// DESCRIPTION  : Defines a custom event for logging
// user feedback events
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/UserFeedbackEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:36   mturner
//Initial revision.
//
//   Rev 1.1   Jul 22 2004 14:08:56   jmorrissey
//Updated constructor so that this event stores the string representation of the feedbackType rather than its enum int value.
//
//   Rev 1.0   Jul 20 2004 15:29:48   jmorrissey
//Initial revision.

#region namespaces

using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;

#endregion

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Summary description for UserFeedbackEvent.
	/// </summary>
	[Serializable]
	public class UserFeedbackEvent : TDPCustomEvent	
	{

		private string feedbackType;
		private DateTime submittedTime;
		private DateTime acknowledgedTime;
		private bool acknowledgmentSent;
		private static UserFeedbackEventFileFormatter fileFormatter = new UserFeedbackEventFileFormatter();

		/// <summary>
		/// Constructor for UserFeedbackEvent class. 
		/// A UserFeedbackEvent is used to log when a user has submitted feedback 
		/// via the Contact Us functionality.
		/// This class must be serializable to allow logging to MSMQs.
		/// </summary>
		/// <param name="sessionId">The session id on which the page was entered.</param>
		/// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or not (false).</param>
		public UserFeedbackEvent(string feedbackType ,DateTime submittedTime, DateTime acknowledgedTime, bool acknowledgmentSent,
			string sessionId, bool userLoggedOn): base(sessionId, userLoggedOn)
		{
			this.feedbackType = feedbackType;			
			this.submittedTime = submittedTime;
			this.acknowledgedTime = acknowledgedTime;
			this.acknowledgmentSent = acknowledgmentSent;
		}	

		/// <summary>
		/// Returns the time when the event occurred
		/// </summary>
		public DateTime SubmittedTime
		{
			get {return submittedTime;}
		}

		/// <summary>
		/// Returns the time when the event occurred
		/// </summary>
		public DateTime AcknowledgedTime
		{
			get {return acknowledgedTime;}
		}

		/// <summary>
		/// Returns the time when the event occurred
		/// </summary>
		public bool AcknowledgmentSent
		{
			get {return acknowledgmentSent;}
		}

		/// <summary>
		/// Gets the user feedback event type classifier e.g. SiteProblem/IncorrectInformation/Suggestion
		/// </summary>
		public string FeedbackType
		{
			get{return feedbackType;}
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
