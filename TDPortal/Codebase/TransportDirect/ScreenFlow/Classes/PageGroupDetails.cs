// *********************************************** 
// NAME                 : PageGroupDetails.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 25/04/2008
// DESCRIPTION          : A class that holds the page group
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Classes/PageGroupDetails.cs-arc  $ 
//
//   Rev 1.0   May 01 2008 17:08:36   mmodi
//Initial revision.
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;

namespace TransportDirect.UserPortal.ScreenFlow
{
    public class PageGroupDetails
    {
        private PageId pageId;
        private PageGroup pageGroup;
        private string pageUrl;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="pageGroup"></param>
        /// <param name="pageUrl"></param>
        public PageGroupDetails(PageId pageId, PageGroup pageGroup, string pageUrl)
        {
            this.pageId = pageId;
            this.pageGroup = pageGroup;
            this.pageUrl = pageUrl;
        }


        /// <summary>
        /// Read Property. Returns the ID of the page.
        /// </summary>
        public PageId PageId
        {
            get { return pageId; }
        }

        /// <summary>
        /// Read Property. Returns the Group of the page
        /// </summary>
        public PageGroup PageGroup
        {
            get { return pageGroup; }
        }

        /// <summary>
        /// Read Property. Returns the Url of the page.
        /// </summary>
        public string PageUrl
        {
            get { return pageUrl; }
        }
    }
}
