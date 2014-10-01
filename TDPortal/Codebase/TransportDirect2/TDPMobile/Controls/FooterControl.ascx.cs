// *********************************************** 
// NAME             : FooterControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2012
// DESCRIPTION  	: Footer control
// ************************************************
// 

using System;
using TDP.Common;
using TDP.Common.Extenders;
using TDP.Common.Web;
using TDP.Common.ResourceManager;
using TDP.Common.PropertyManager;

namespace TDP.UserPortal.TDPMobile.Controls
{
    /// <summary>
    /// Footer control
    /// </summary>
    public partial class FooterControl : System.Web.UI.UserControl
    {
        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_Init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            lnkbtnLanguage.Click += new EventHandler(lnkbtnLanguage_Click);
            btnLanguage.Click += new EventHandler(lnkbtnLanguage_Click);
        }

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
            SetupLinks();

            SetControlVisibility();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Set the text and URL of the links
        /// </summary>
        private void SetupLinks()
        {
            TDPPageMobile page = (TDPPageMobile)Page;

            privacyLink.InnerText = page.GetResourceMobile("FooterControl.Privacy.Text");
            privacyLink.Attributes["title"] = page.GetResourceMobile("FooterControl.Privacy.ToolTip");
            privacyLink.HRef = ResolveClientUrl(page.GetPageTransferDetail(PageId.MobilePrivacy).PageUrl);

            fullsiteLink.InnerText = page.GetResourceMobile("FooterControl.FullSite.Text");
            fullsiteLink.Attributes["title"] = page.GetResourceMobile("FooterControl.FullSite.ToolTip");
            //href="http://www.transportdirect.info?dnr=true" 
            fullsiteLink.HRef = string.Format("{0}?{1}=true",
                ResolveClientUrl(page.GetPageTransferDetail(PageId.Homepage).PageUrl),
                QueryStringKey.DoNotRedirect);
                        


            lnkbtnLanguage.Text = page.GetResourceMobile("HeaderControl.Language.Link.Text");
            lnkbtnLanguage.ToolTip = page.GetResourceMobile("HeaderControl.Language.Link.ToolTip");
            btnLanguage.Text = lnkbtnLanguage.Text;
            btnLanguage.ToolTip = lnkbtnLanguage.ToolTip;
        }

        
        /// <summary>
        /// Method to display and hide controls
        /// </summary>
        private void SetControlVisibility()
        {
            // Display language link
            liLanguageLink.Visible = Properties.Current["Header.Link.Language.Visible.Switch"].Parse(true);
        }
                
        #endregion

        #region Event handlers

        /// <summary>
        /// Event handler for lnkbtnLanguage_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnLanguage_Click(object sender, EventArgs e)
        {
            //It is only possible to switch language by clicking the link, 
            //so perform a straight switch, but not here... goto TDPPage::OnInit
            CurrentLanguage.Value = (CurrentLanguage.Value == Language.English ? Language.Welsh : Language.English);

            Server.Transfer(Request.RawUrl);
        }
        
        #endregion
    }
}
