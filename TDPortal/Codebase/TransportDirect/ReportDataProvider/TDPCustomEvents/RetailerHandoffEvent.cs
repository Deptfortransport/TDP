// *********************************************** 
// NAME                 : RetailerHandoffEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 18/08/2003 
// DESCRIPTION  : Defines a custom event for logging
// retailer handoff event data.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/RetailerHandoffEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:30   mturner
//Initial revision.
//
//   Rev 1.2   Jun 28 2004 15:39:34   passuied
//Fix for the Event Receiver
//
//   Rev 1.1   Sep 16 2003 11:09:32   geaton
//Added extra comments following code review.
//
//   Rev 1.0   Aug 20 2003 10:42:02   geaton
//Initial Revision

using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;


namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	
	/// <summary>
	/// Defines the class for capturing Retailer Handoff Event data.
	/// </summary>
	[Serializable]
	public class RetailerHandoffEvent : TDPCustomEvent	
	{
		private string retailerId;

		private static RetailerHandoffEventFileFormatter fileFormatter = new RetailerHandoffEventFileFormatter();

		/// <summary>
		/// Constructor for a <c>RetailerHandoffEvent</c> class. 
		/// A <c>RetailerHandoffEvent</c> is used
		/// to log retailer handoff event data using the Event Service.
		/// This class must be serializable to allow logging to MSMQs.
		/// </summary>
		/// <param name="sessionId">The session id on which the page was entered.</param>
		/// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or not (false).</param>
		public RetailerHandoffEvent(string retailerId,
									string sessionId, 
									bool userLoggedOn): base(sessionId, userLoggedOn)
		{
			this.retailerId = retailerId;
		}

		
		/// <summary>
		/// Gets the retailer id
		/// </summary>
		public string RetailerId
		{
			get{return retailerId;}
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



