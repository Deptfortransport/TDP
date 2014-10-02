// *********************************************** 
// NAME                 : BasePageEntryEvent.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 03/03/2005 
// DESCRIPTION  		: Base class for Page EntryEvent and Mobile page entry event. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/BasePageEntryEvent.cs-arc  $ 
//
//   Rev 1.2   Apr 29 2008 16:37:54   mturner
//Fixes to allow the web log reader to process host names from IIS logs.  This is to resolve IR4904/USD2517876
//
//   Rev 1.1   Mar 25 2008 09:46:56   pscott
//IR 4621 CCN 427 White Label Changes
//Make necessary changes to write theme id to page entryevents table
//
//   Rev 1.0   Nov 08 2007 12:39:18   mturner
//Initial revision.
//
//   Rev 1.3   Aug 23 2005 14:14:04   mdambrine
//Undid Changes from previous version as they did not belong in the trunk
//
//   Rev 1.2   Aug 17 2005 16:06:52   mdambrine
//Added a new property partnerID 
//
//   Rev 1.1   Mar 08 2005 16:34:22   schand
//New base class for MobilePageEvent and PageEvent
//
//   Rev 1.0   Mar 07 2005 11:43:20   schand
//Initial revision.


using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;


namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Defines the class for capturing Base Page Entry Event data.
	/// </summary>
	[Serializable]
	public class BasePageEntryEvent: TDPCustomEvent
	{
		private static DefaultFormatter formatter = new DefaultFormatter();		
		private static object page =null;
        private static int themeId = 0;
        private string partnerName = string.Empty;
 
		public BasePageEntryEvent(string sessionId, bool userLoggedOn):base(sessionId, userLoggedOn)
		{   			

		}

        /// <summary>
        /// OverLoaded Constructor for the BasePageEntryEvent
        /// </summary>
        /// <param name="sessionId">Id of the users session</param>
        /// <param name="userLoggedOn">boolean</param>
        /// <param name="partnerName">DisplayName of the Partner</param>
        public BasePageEntryEvent(string sessionId, bool userLoggedOn, string partnerName): base(sessionId, userLoggedOn)
        {
            this.partnerName = partnerName;
        }	
	
		/// <summary>
		/// Read-Only property to return the page name as string
		/// </summary>
		virtual public string PageName 
		{	
			get
			{					   
				return page.ToString();
			}
		}

        virtual public int ThemeId
        {
            get { return themeId; }
        }


		/// <summary>
		/// Provides an event formatter for publishing to files.
		/// </summary>
		override public IEventFormatter FileFormatter
		{
			get {return formatter;}
		}

        /// <summary>
        /// read-only property returning the partner's displayname
        /// </summary>
        public string PartnerName
        {
            get
            {
                return partnerName;
            }
        }
	
	}
}
