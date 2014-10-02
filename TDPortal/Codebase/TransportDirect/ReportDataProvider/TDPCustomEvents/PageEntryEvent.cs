// *********************************************** 
// NAME                 : PageEntryEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 18/08/2003 
// DESCRIPTION  : Defines a custom event for logging
// page entry event data.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/PageEntryEvent.cs-arc  $
//
//   Rev 1.1   Mar 25 2008 09:46:58   pscott
//IR 4621 CCN 427 White Label Changes
//Make necessary changes to write theme id to page entryevents table
//
//   Rev 1.0   Nov 08 2007 12:39:28   mturner
//Initial revision.
//
//   Rev 1.5   Aug 23 2005 14:14:04   mdambrine
//Undid Changes from previous version as they did not belong in the trunk
//
//   Rev 1.4   Aug 17 2005 16:06:52   mdambrine
//Added a new property partnerID 
//
//   Rev 1.3   Mar 08 2005 16:26:44   schand
//Added properperty PageName
//
//   Rev 1.2   Jun 28 2004 15:39:34   passuied
//Fix for the Event Receiver
//
//   Rev 1.1   Sep 16 2003 11:09:30   geaton
//Added extra comments following code review.
//
//   Rev 1.0   Aug 20 2003 10:42:00   geaton
//Initial Revision

using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TD.ThemeInfrastructure;


namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	
	/// <summary>
	/// Defines the class for capturing Page Entry Event data.
	/// </summary>
	[Serializable]
	public class PageEntryEvent : BasePageEntryEvent	
	{
		private PageId page;
        private int themeId;
        
		private static PageEntryEventFileFormatter fileFormatter = new PageEntryEventFileFormatter();

		#region "Constructors"
		/// <summary>
		/// Constructor for a <c>PageEntryEvent</c> class. 
		/// A <c>PageEntryEvent</c> is used
		/// to log page entry transaction data using the Event Service.
		/// This class must be serializable to allow logging to MSMQs.
		/// </summary>
		/// <param name="sessionId">The session id on which the page was entered.</param>
		/// <param name="eventType">The page identifier of the page entered.</param>
		/// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or not (false).</param>
		public PageEntryEvent(PageId page,					  
							  string sessionId, 
							  bool userLoggedOn) : base(sessionId, userLoggedOn)
		{
            try
            {
                this.themeId = ThemeProvider.Instance.GetTheme().Id;
            }
            catch
            {
                this.themeId = 0;
            }

			this.page = page;
		}
		
		#endregion
		
		#region "Properties"

		/// <summary>
		/// Gets the page identifier
		/// </summary>
		public PageId Page
		{
			get{return page;}
		}

		
		/// <summary>
		/// Read-Only property to return the page name as string
		/// </summary>
		override public string PageName 
		{	
			get
			{					   
				return page.ToString() ;
			}
		}

		/// <summary>
		/// Provides an event formatter for publishing to files.
		/// </summary>
		override public IEventFormatter FileFormatter
		{
			get {return fileFormatter;}
		}

        /// <summary>
        /// Returns stuff
        /// </summary>
        override public int ThemeId
        {
            get { return themeId; }
        }
		
		#endregion
	}

}

