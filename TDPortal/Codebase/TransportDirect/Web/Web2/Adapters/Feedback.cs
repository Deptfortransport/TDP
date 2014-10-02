// *********************************************** 
// NAME			: Feedback.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 14/01/2007
// DESCRIPTION	: Class which holds data for a Feedback record
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/Feedback.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 12:58:56   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:16   mturner
//Initial revision.
//
//   Rev 1.1   Jan 17 2007 18:00:22   mmodi
//Added properties for Options, Details, and Email
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.0   Jan 14 2007 17:25:58   mmodi
//Initial revision.
//Resolution for 4332: Contact Us Improvements - workstream
//

using System;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Summary description for Feedback.
	/// </summary>
	[Serializable()]
	public class Feedback
	{
		#region Private properties
		
		private int feedbackId;
		private string sessionId;			
		private DateTime sessionCreated; 
		private DateTime sessionExpires; 
		private DateTime emailSubmittedTime; //Time sent to Help desk
		private DateTime emailAcknowledgedTime; //Time sent to User email
		private bool acknowledgementSent; // Acknowledgement sent to User
		private bool userLoggedOn;
		private DateTime timeLogged;
		private string vantiveId;
		private string feedbackStatus;
		private bool deleteFlag;
		private string options; // Feedback options selected by user during submit - as a string
		private string details;
		private string email;

		#endregion

		#region Constructor
		/// <summary>
		/// Default constructor
		/// </summary>
		public Feedback()
		{
		}

		/// <summary>
		/// Creates Feedback object with supplied parameters
		/// </summary>
		/// <param name="feedbackId"></param>
		/// <param name="sessionId"></param>
		/// <param name="sessionCreated"></param>
		/// <param name="sessionExpires"></param>
		/// <param name="emailSubmittedTime"></param>
		/// <param name="emailAcknowledgedTime"></param>
		/// <param name="acknowledgementSent"></param>
		/// <param name="userLoggedOn"></param>
		/// <param name="timeLogged"></param>
		/// <param name="vantiveId"></param>
		/// <param name="feedbackStatus"></param>
		/// <param name="deleteFlag"></param>
		public Feedback(
			int feedbackId,
			string sessionId,			
			DateTime sessionCreated,
			DateTime sessionExpires, 
			DateTime emailSubmittedTime,
			DateTime emailAcknowledgedTime,
			bool acknowledgementSent,
			bool userLoggedOn,
			DateTime timeLogged,
			string vantiveId,
			string feedbackStatus,
			bool deleteFlag
			)
		{
			this.feedbackId = feedbackId;
			this.sessionId = sessionId;
			this.sessionCreated = sessionCreated;
			this.sessionExpires = sessionExpires;
			this.emailSubmittedTime = emailSubmittedTime;
			this.emailAcknowledgedTime = emailAcknowledgedTime;
			this.acknowledgementSent = acknowledgementSent;
			this.userLoggedOn = userLoggedOn;
			this.timeLogged = timeLogged;
			this.vantiveId = vantiveId;
			this.feedbackStatus = feedbackStatus;
			this.deleteFlag = deleteFlag;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Read only property. Get the feedbackId
		/// </summary>
		public int FeedbackId
		{
			get {return feedbackId;}
		}

		/// <summary>
		/// Read only property. Get the sessionId
		/// </summary>
		public string SessionId
		{
			get {return sessionId;}
		}

		/// <summary>
		/// Read only property. Get the sessionCreated
		/// </summary>
		public DateTime SessionCreated
		{
			get {return sessionCreated;}
		}

		/// <summary>
		/// Read only property. Get the sessionExpires
		/// </summary>
		public DateTime SessionExpires
		{
			get {return sessionExpires;}
		}

		/// <summary>
		/// Read only property. Get the emailSubmittedTime
		/// </summary>
		public DateTime EmailSubmittedTime
		{
			get {return emailSubmittedTime;}
		}

		/// <summary>
		/// Read only property. Get the emailAcknowledgedTime
		/// </summary>
		public DateTime EmailAcknowledgedTime
		{
			get {return emailAcknowledgedTime;}
		}

        /// <summary>
		/// Read only property. Get the acknowledgementSent
		/// </summary>
		public bool AcknowledgementSent
		{
			get {return acknowledgementSent;}
		}

		/// <summary>
		/// Read only property. Get the userLoggedOn
		/// </summary>
		public bool UserLoggedOn
		{
			get {return userLoggedOn;}
		}
		/// <summary>
		/// Read only property. Get the timeLogged
		/// </summary>
		public DateTime TimeLogged
		{
			get {return timeLogged;}
		}

		/// <summary>
		/// Read/Write property. Get/Set the vantiveId
		/// </summary>
		public string VantiveId
		{
			get {return vantiveId;}
			set {vantiveId = value;}
		}

		/// <summary>
		/// Read/Write property. Get/Set the feedbackStatus
		/// </summary>
		public string FeedbackStatus
		{
			get {return feedbackStatus;}
			set {feedbackStatus = value;}
		}

		/// <summary>
		/// Read/Write property. Get/Set the deleteFlag
		/// </summary>
		public bool DeleteFlag
		{
			get {return deleteFlag;}
			set {deleteFlag = value;}
		}

		/// <summary>
		/// Read/Write property. Get/Set the feedback options
		/// </summary>
		public string FeedbackOptions
		{
			get {return options;}
			set {options = value;}
		}

		/// <summary>
		/// Read/Write property. Get/Set the feedback details
		/// </summary>
		public string FeedbackDetails
		{
			get {return details;}
			set {details = value;}
		}

		/// <summary>
		/// Read/Write property. Get/Set the email contact
		/// </summary>
		public string Email
		{
			get {return email;}
			set {email = value;}
		}

		#endregion
		
	}
}
