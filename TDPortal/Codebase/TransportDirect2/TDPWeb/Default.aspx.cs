// *********************************************** 
// NAME             : Default.aspx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Feb 2011
// DESCRIPTION  	: Default page. Transfers user to actual "Home" page
// ************************************************
// 

using System;
using TDP.Common;
using TDP.Common.Web;
using TDP.UserPortal.ScreenFlow;

namespace TDP.UserPortal.TDPWeb
{
    /// <summary>
    /// Default page
    /// </summary>
    public partial class _Default : TDPPage
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public _Default()
            : base(Global.TDPResourceManager)
        {
            pageId = PageId.Default;
        }

        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Transfer use to the "Home" page. This is currently the journey input page
            PageTransferDetail transferDetail = GetPageTransferDetail(PageId.JourneyPlannerInput);

            // Use Response.Redirect instead of Server.Transfer as Server.Transfer
            // doesn' t change the sitemapnode on the page hance some issues get raised with getting resource strings
            // Server.Transfer(transferDetail.PageUrl,true);
            Response.Redirect(transferDetail.PageUrl);
        }
        
        #endregion
    }
}