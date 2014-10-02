// *********************************************** 
// NAME                 : LoginEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 18/08/2003 
// DESCRIPTION  : Defines a custom event for logging
// login event data.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/LoginEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:24   mturner
//Initial revision.
//
//   Rev 1.3   Jun 28 2004 15:39:32   passuied
//Fix for the Event Receiver
//
//   Rev 1.2   Oct 02 2003 17:31:36   geaton
//Removed loggedOn parameter from LoginEvent since by definition the user is logged on!
//
//   Rev 1.1   Sep 16 2003 11:09:26   geaton
//Added extra comments following code review.
//
//   Rev 1.0   Aug 20 2003 10:41:56   geaton
//Initial Revision

using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;


namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	
	/// <summary>
	/// Defines the class for capturing Login Event data.
	/// </summary>
	[Serializable]
	public class LoginEvent : TDPCustomEvent	
	{
		private static LoginEventFileFormatter fileFormatter = new LoginEventFileFormatter();
		
		/// <summary>
		/// Constructor for a <c>LoginEvent</c> class. 
		/// A <c>LoginEvent</c> is used
		/// to log login event data using the Event Service.
		/// This class must be serializable to allow logging to MSMQs.
		/// </summary>
		/// <param name="sessionId">The session id on which the page was entered.</param>
		public LoginEvent(string sessionId) : base(sessionId, true)
		{}

		/// <summary>
		/// Provides an event formatter for publishing to files.
		/// </summary>
		override public IEventFormatter FileFormatter
		{
			get {return fileFormatter;}
		}
	
	}
}


