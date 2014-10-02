// *********************************************** 
// NAME                 : GeneralComplaintEvent.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 11/07/2003 
// DESCRIPTION          : Custom LogEvent representing  
//                        user's submitted complaint data. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Events/GeneralComplaintEvent.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:23:52   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:58   mturner
//Initial revision.
//
//   Rev 1.5   Sep 17 2003 16:23:18   hahad
//Set file, eventlog and console formatters to 'null'
//
//   Rev 1.4   Aug 12 2003 15:31:56   AWindley
//Initialised strings using String.Empty
//
//   Rev 1.3   Aug 07 2003 14:28:46   AWindley
//added comments
//
//   Rev 1.2   Jul 29 2003 17:57:02   AWindley
//removed parameter 'user'; changed referenceNumber from long to string
//
//   Rev 1.1   Jul 28 2003 16:45:44   AWindley
//removed IncidentOccurred from custom event
//
//   Rev 1.0   Jul 24 2003 16:59:04   AWindley
//Initial Revision

using System;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.Web.Events
{
	/// <summary>
	/// GeneralComplaintEvent to represent user's complaint data.
	/// Extends CustomEvent.
	/// </summary>
	[Serializable]
	public class GeneralComplaintEvent : CustomEvent
	{
		private string referenceNumber = String.Empty;
		private string firstName = String.Empty;
		private string lastName = String.Empty;
		private string comment = String.Empty;
		
		private static IEventFormatter emailFormatter = new GeneralComplaintEventEmailFormatter();
		

		private static IEventFilter filter = new CustomEventFilter();

		/// <summary>
		/// Constructor for the GeneralComplaintEvent
		/// </summary>
		/// <param name="referenceNumber">Feedback reference</param>
		/// <param name="firstName">User's first name</param>
		/// <param name="lastName">User's last name</param>
		/// <param name="comment">User's comment</param>
		public GeneralComplaintEvent(string referenceNumber, 
									 string firstName, 
									 string lastName, 
									 string comment) : base()
		{
			this.referenceNumber = referenceNumber;
			this.firstName = firstName;
			this.lastName = lastName;
			this.comment = comment;
		}

		/// <summary>
		/// Get the email formatter (Read only)
		/// </summary>
		override public IEventFormatter EmailFormatter
		{
			get {return emailFormatter;}
		}

		/// <summary>
		/// Get the file formatter (Read only)
		/// </summary>
		override public IEventFormatter FileFormatter
		{
			// Use default formatter.
			// No requirement to publish EmailAttachmentEvent to files.

			get {return null;}
		}

		/// <summary>
		/// Get the event log formatter (Read only)
		/// </summary>
		override public IEventFormatter EventLogFormatter
		{
			// Use default formatter.
			// No requirement to publish EmailAttachmentEvent to files.

			get {return null;}
		}

		/// <summary>
		/// Get the console formatter (Read only)
		/// </summary>
		override public IEventFormatter ConsoleFormatter
		{
			// Use default formatter.
			// No requirement to publish EmailAttachmentEvent to files.

			get {return null;}
		}

		/// <summary>
		/// Get the event filter (Read only)
		/// </summary>
		override public IEventFilter Filter
		{
			get {return filter;}
		}

		/// <summary>
		/// Get the feedback reference number (Read only)
		/// </summary>
		public string ReferenceNumber
		{
			get{return referenceNumber;}
		}

		/// <summary>
		/// Get the user's first name (Read only)
		/// </summary>
		public string FirstName
		{
			get{return firstName;}
		}

		/// <summary>
		/// Get the user's last name (Read only)
		/// </summary>
		public string LastName
		{
			get{return lastName;}
		}

		/// <summary>
		/// Get the user's comments (Read only)
		/// </summary>
		public string Comment
		{
			get{return comment;}
		}
	}
}