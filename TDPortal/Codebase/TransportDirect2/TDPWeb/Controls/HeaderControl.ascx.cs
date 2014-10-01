// *********************************************** 
// NAME             : HeaderControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Feb 2011
// DESCRIPTION  	: Header control
// ************************************************
// 

using System;
using TDP.Common;
using TDP.Common.Extenders;
using TDP.Common.PropertyManager;
using TDP.Common.Web;
using System.Web.UI;
using TDP.Common.ResourceManager;

namespace TDP.UserPortal.TDPWeb.Controls
{
    /// <summary>
    /// Header Control
    /// </summary>
    public partial class HeaderControl : System.Web.UI.UserControl
    {

        #region Page_Init, Page_Load, Page_PreRender
        
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupResources();

            DisplayControls();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Sets up control resources
        /// </summary>
        private void SetupResources()
        {
            TDPPage page = (TDPPage)Page;

            accessibiliyMenuSR.InnerText = "Accessiblity menu";
            lnkCookies.Text = "Cookies policy";
            lnkAccessibility.Text = "Accessibility";
            lblStyleLinks.Text = "Change Style Sheet";

            fontNormalLnk.ToolTip = "Switch to Normal text";
            fontNormalLnk.Text = string.Format("<span class=\"screenReaderOnly\">{0}</span><span class=\"fontNormalText\">{1}</span>", "Normal text", "A");
            fontNormalLnk.Target = "#";

            fontLargerLnk.ToolTip = "Switch to Larger text";
            fontLargerLnk.Text = string.Format("<span class=\"screenReaderOnly\">{0}</span><span class=\"fontLargerText\">{1}</span>", "Large text text", "A");
            fontLargerLnk.Target = "#";

            fontLargestLnk.ToolTip = "Switch to Largest text";
            fontLargestLnk.Text = string.Format("<span class=\"screenReaderOnly\">{0}</span><span class=\"fontLargestText\">{1}</span>", "Largest text", "A");
            fontLargestLnk.Target = "#";

            styleNormalLink.ToolTip = "Switch to Normal style sheet";
            styleNormalLink.Text = string.Format("<span class=\"screenReaderOnly\">{0}</span><span class=\"styleNormalText\">{1}</span>", "Normal style sheet", "A");
            styleNormalLink.Target = "#";

            styleDyslexiaLink.ToolTip = "Switch to Dyslexia style sheet";
            styleDyslexiaLink.Text = string.Format("<span class=\"screenReaderOnly\">{0}</span><span class=\"styleDyslexiaText\">{1}</span>", "Dyslexia style sheet", "A");
            styleDyslexiaLink.Target = "#";

            styleVisibilityLink.ToolTip = "Switch to High Visibility style sheet";
            styleVisibilityLink.Text = string.Format("<span class=\"screenReaderOnly\">{0}</span><span class=\"styleVisibilityText\">{1}</span>", "High visibility style sheet", "A");
            styleVisibilityLink.Target = "#";
        }

        /// <summary>
        /// Displays or hides controls
        /// </summary>
        private void DisplayControls()
        {
            IPropertyProvider pp = Properties.Current;
            TDPPage page = (TDPPage)Page;

            // Set link visibility
            liSkipToContent.Visible = pp["Header.Link.SkipToContent.Visible.Switch"].Parse(true);
            liLanguage.Visible = pp["Header.Link.Language.Visible.Switch"].Parse(true);
            
            // Display cookie link if required
            DateTime cookieLinkDateTime = Properties.Current["Cookie.CookiePolicy.Hyperlink.VisibleFrom.Date"].Parse(DateTime.Now.AddYears(1));
            DateTime todayDate = DateTime.Now.Date;

            liCookies.Visible = todayDate >= cookieLinkDateTime;
            
            #region Load navigation html from resource manager

            // Load header (primary) html content
            using (LiteralControl headerPrimary = new LiteralControl())
            {
                headerPrimary.Text = page.GetResource(
                    TDPResourceManager.GROUP_HEADERFOOTER,
                    TDPResourceManager.COLLECTION_DEFAULT,
                    (page.SiteModeDisplay == SiteMode.Olympics ?
                        "Header.Olympics.PrimaryContainer.Html" : "Header.Paralympics.PrimaryContainer.Html"));

                // Try default html
                if (string.IsNullOrEmpty(headerPrimary.Text))
                {
                    headerPrimary.Text = page.GetResource(
                    TDPResourceManager.GROUP_HEADERFOOTER,
                    TDPResourceManager.COLLECTION_DEFAULT,
                    "Header.PrimaryContainer.Html");
                }

                if (!string.IsNullOrEmpty(headerPrimary.Text))
                {
                    pnlHeaderPrimaryContainer.Controls.Add(headerPrimary);
                }
                else
                    pnlHeaderPrimaryContainer.Visible = false;
            }

            // Load header (secondary) html content
            using (LiteralControl headerSecondary = new LiteralControl())
            {
                headerSecondary.Text = page.GetResource(
                    TDPResourceManager.GROUP_HEADERFOOTER,
                    TDPResourceManager.COLLECTION_DEFAULT,
                    (page.SiteModeDisplay == SiteMode.Olympics ?
                        "Header.Olympics.SecondaryContainer.Html" : "Header.Paralympics.SecondaryContainer.Html"));

                // Try default html
                if (string.IsNullOrEmpty(headerSecondary.Text))
                {
                    headerSecondary.Text = page.GetResource(
                    TDPResourceManager.GROUP_HEADERFOOTER,
                    TDPResourceManager.COLLECTION_DEFAULT,
                    "Header.SecondaryContainer.Html");
                }

                if (!string.IsNullOrEmpty(headerSecondary.Text))
                {
                    pnlHeaderSecondaryContainer.Controls.Add(headerSecondary);
                }
                else
                    pnlHeaderSecondaryContainer.Visible = false;
            }

            #endregion
        }

        #endregion
    }
}