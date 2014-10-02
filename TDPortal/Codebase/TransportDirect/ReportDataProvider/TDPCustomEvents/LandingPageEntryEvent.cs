// *********************************************** 
// NAME                 : LandingPageEntryEvent.cs 
// AUTHOR               : Jamie McAllister / Tim Mollart
// DATE CREATED         : 22/07/2005 
// DESCRIPTION  : Defines a custom event for logging
// landing page entry event data.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/LandingPageEntryEvent.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:39:24   mturner
//Initial revision.
//
//   Rev 1.2   Sep 26 2005 17:25:56   halkatib
//Added $Log 
//Resolution for 2610: DEL 8 Stream: Landing page
//
using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	
	/// <summary>
	/// Defines the class for capturing Page Entry Event data.
	/// </summary>
	[Serializable]
	public class LandingPageEntryEvent : TDPCustomEvent	
	{
		private string partnerID;
		private LandingPageService serviceID;

		private static LandingPageEntryEventFileFormatter fileFormatter = new LandingPageEntryEventFileFormatter();

		/// <summary>
		/// Constructor for a <c>LandingPageEntryEvent</c> class. 
		/// A <c>LandingPageEntryEvent</c> is used
		/// to log page entry transaction data using the Event Service.
		/// This class must be serializable to allow logging to MSMQs.
		/// </summary>
		/// <param name="partnerId">The partner id of the client making requests to the landing page.</param>
		/// <param name="eventType">The page identifier of the page entered.</param>
		/// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or not (false).</param>
		public LandingPageEntryEvent(string partnerId, LandingPageService serviceId, string sessionID, bool userLoggedOn):base(sessionID, userLoggedOn)
		{
			this.partnerID = partnerId;
			this.serviceID = serviceId;
		}
		
		/// <summary>
		/// Read only - Gets the Partner ID
		/// </summary>
		public string PartnerID
		{
			get{return partnerID;}
		}

		/// <summary>
		/// Read only - Gets the Service ID
		/// </summary>
		public LandingPageService ServiceID
		{
			get{return serviceID;}
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