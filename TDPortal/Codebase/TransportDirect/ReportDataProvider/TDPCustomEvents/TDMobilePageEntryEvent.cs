// *********************************************** 
// NAME                 : TDMobilePageEntryEvent.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 03/03/2005 
// DESCRIPTION  		: Defines a custom event for logging mobile page entry event data.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/TDMobilePageEntryEvent.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:39:34   mturner
//Initial revision.
//
//   Rev 1.1   Mar 08 2005 16:29:38   schand
//Mobile Page event logging class
//
//   Rev 1.0   Mar 07 2005 11:43:18   schand
//Initial revision.


using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Defines the class for capturing Page Entry Event data.
	/// </summary>
	[Serializable]
	public class TDMobilePageEntryEvent: BasePageEntryEvent
	{
		private TDMobilePageId mobilePage;
		private static TDMobilePageEntryEventFileFormatter fileFormatter = new TDMobilePageEntryEventFileFormatter();

		/// <summary>
		/// Constructor for a <c>MobilePageEntryEvent</c> class. 
		/// A <c>MobilePageEntryEvent</c> is used
		/// to log mobile page entry transaction data using the Event Service.
		/// This class must be serializable to allow logging to MSMQs.
		/// </summary>
		/// <param name="sessionId">The session id on which the page was entered.</param>		
		/// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or not (false).</param>
	    public TDMobilePageEntryEvent(TDMobilePageId  mobilePage,					  
			string sessionId, 
			bool userLoggedOn) : base(sessionId, userLoggedOn)
		{
			this.mobilePage = mobilePage;
		}


		/// <summary>
		/// Read-Only property to return the page name as string
		/// </summary>
		override public string PageName 
		{	
			get
			{					   
				return mobilePage.ToString() ;
			}
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
