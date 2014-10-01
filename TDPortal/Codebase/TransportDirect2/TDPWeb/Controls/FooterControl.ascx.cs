// *********************************************** 
// NAME             : FooterControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Feb 2011
// DESCRIPTION  	: Footer control
// ************************************************
// 

using System;
using TDP.Common;
using TDP.Common.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using TDP.Common.ResourceManager;

namespace TDP.UserPortal.TDPWeb.Controls
{
    /// <summary>
    /// Footer control
    /// </summary>
    public partial class FooterControl : System.Web.UI.UserControl
    {
        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Page PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            #region Footer html from resource manager

            // Load footer html content
            using (LiteralControl footer = new LiteralControl())
            {
                TDPPage page =  (TDPPage)Page;
                
                footer.Text = page.GetResource(
                    TDPResourceManager.GROUP_HEADERFOOTER, 
                    TDPResourceManager.COLLECTION_DEFAULT,
                    (page.SiteModeDisplay == SiteMode.Olympics ? "Footer.Olympics.Html" : "Footer.Paralympics.Html"));

                // Try default mode text
                if (string.IsNullOrEmpty(footer.Text))
                {
                    footer.Text = page.GetResource(
                    TDPResourceManager.GROUP_HEADERFOOTER,
                    TDPResourceManager.COLLECTION_DEFAULT,
                    "Footer.Html");
                }

                pnlFooter.Controls.Add(footer);
            }

            #endregion
        }

        #endregion
    }
}