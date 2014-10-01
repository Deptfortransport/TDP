// *********************************************** 
// NAME             : HeaderControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2012
// DESCRIPTION  	: Header control
// ************************************************
// 

using System;
using System.Web.UI.WebControls;
using TDP.Common;
using TDP.Common.Extenders;
using TDP.Common.Web;
using System.Collections.Generic;
using TDP.Common.PropertyManager;
using TDP.Common.ResourceManager;
using System.Web;
using TDP.UserPortal.ScreenFlow;

namespace TDP.UserPortal.TDPMobile.Controls
{
    #region Public Events

    // Delegate for back button click
    public delegate void OnNextClick(object sender, EventArgs e);

    // Delegate for next button click
    public delegate void OnBackClick(object sender, EventArgs e);
    
    #endregion

    /// <summary>
    /// Header Control
    /// </summary>
    public partial class HeaderControl : System.Web.UI.UserControl
    {
        #region Private members

        private bool displayNavigation = true;
        private bool displayBack = true;
        private bool displayBrowserBack = false;
        private bool displayNext = false;

        private string resourceBackText = string.Empty;
        private string resourceBackToolTip = string.Empty;
        private string resourceNextText = string.Empty;
        private string resourceNextToolTip = string.Empty;

        #endregion

        #region Public Events

        // Back button click event declaration
        public event OnBackClick BackClickHandler;

        // Next button click event declaration
        public event OnNextClick NextClickHandler;

        #endregion

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
            SetupLogoImage();

            SetupNavigation();

            SetupMenu();

            DisplayControls();
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Next button control
        /// </summary>
        public System.Web.UI.WebControls.Button ButtonBack
        {
            get { return backBtn; }
            set { backBtn = value; }
        }

        /// <summary>
        /// Next button control
        /// </summary>
        public System.Web.UI.WebControls.Button ButtonNext
        {
            get { return nextBtn; }
            set { nextBtn = value; }
        }

        /// <summary>
        /// Read/Write. Display the Navigation panel containing the Back and Next buttons. Default is true
        /// </summary>
        public bool DisplayNavigation
        {
            get { return displayNavigation; }
            set { displayNavigation = value; }
        }

        /// <summary>
        /// Read/Write. Display the Back button. Default is true
        /// </summary>
        public bool DisplayBack
        {
            get { return displayBack; }
            set { displayBack = value; }
        }

        /// <summary>
        /// Read/Write. Display the Back button with javascript to return user back to previous page. Default is false
        /// </summary>
        public bool DisplayBrowserBack
        {
            get { return displayBrowserBack; }
            set { displayBrowserBack = value; }
        }

        /// <summary>
        /// Read/Write. Display the Next button. Default is false
        /// </summary>
        public bool DisplayNext
        {
            get { return displayNext; }
            set { displayNext = value; }
        }

        /// <summary>
        /// Read/Write. Resource
        /// </summary>
        public string ResourceBackText
        {
            get { return resourceBackText; }
            set { resourceBackText = value; }
        }

        /// <summary>
        /// Read/Write. Resource
        /// </summary>
        public string ResourceBackToolTip
        {
            get { return resourceBackToolTip; }
            set { resourceBackToolTip = value; }
        }
        
        /// <summary>
        /// Read/Write. Resource
        /// </summary>
        public string ResourceNextText
        {
            get { return resourceNextText; }
            set { resourceNextText = value; }
        }
        
        /// <summary>
        /// Read/Write. Resource
        /// </summary>
        public string ResourceNextToolTip
        {
            get { return resourceNextToolTip; }
            set { resourceNextToolTip = value; }
        }

        #endregion

        #region Events

        /// <summary>
        /// Back button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void backBtn_Click(object sender, EventArgs e)
        {
            // Raise event to tell subscribers button has been clicked
            if (BackClickHandler != null)
            {
                BackClickHandler(sender, null);
            }
        }

        /// <summary>
        /// Next button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void nextBtn_Click(object sender, EventArgs e)
        {
            // Raise event to tell subscribers button has been clicked
            if (NextClickHandler != null)
            {
                NextClickHandler(sender, null);
            }
        }

        /// <summary>
        /// Menu repeater data bound event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Menu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem
                || e.Item.ItemType == ListItemType.Item
                || e.Item.ItemType == ListItemType.SelectedItem)
            {
                TDPPageMobile page = (TDPPageMobile)Page;

                PageId pageId = (PageId)e.Item.DataItem;

                HyperLink lnkMenuLink = e.Item.FindControlRecursive<HyperLink>("lnkMenuLink");

                string pageMenuText = page.GetResource(TDPResourceManager.GROUP_SITEMAP,
                    string.Format("Pages.{0}", pageId),
                    string.Format("Pages.{0}.Menu", pageId));

                // If menu text found
                if (!string.IsNullOrEmpty(pageMenuText))
                {
                    // Get the url details
                    PageTransferDetail transferDetail = page.GetPageTransferDetail(pageId);

                    lnkMenuLink.Text = pageMenuText;
                    lnkMenuLink.ToolTip = pageMenuText;
                    lnkMenuLink.NavigateUrl = transferDetail.PageUrl;

                    if (transferDetail.PageUrl.ToLower().StartsWith("http"))
                    {
                        // Assume its external
                        lnkMenuLink.Target = "_blank";
                    }
                }
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method to setup the logo
        /// </summary>
        private void SetupLogoImage()
        {
            // Currently not implmented
        }

        /// <summary>
        /// Sets up the navifation button
        /// </summary>
        private void SetupNavigation()
        {
            TDPPageMobile page = ((TDPPageMobile)Page);
            navigationTitle.InnerHtml = page.GetResourceMobile("JourneyInput.Back.Text");

            if (string.IsNullOrEmpty(navigationTitle.InnerHtml))
            {
                navigationTitle.Visible = false;
            }

            backBtn.Text = page.GetResourceMobile(resourceBackText);
            backBtn.ToolTip = page.GetResourceMobile(resourceBackToolTip);

            nextBtn.Text = page.GetResourceMobile(resourceNextText);
            nextBtn.ToolTip = page.GetResourceMobile(resourceNextToolTip);

            // Default
            if (string.IsNullOrEmpty(backBtn.Text))
                backBtn.Text = page.GetResourceMobile("JourneyInput.Back.Text");

            if (string.IsNullOrEmpty(backBtn.ToolTip))
                backBtn.ToolTip = page.GetResourceMobile("JourneyInput.Back.ToolTip");

            if (string.IsNullOrEmpty(nextBtn.Text))
                nextBtn.Text = page.GetResourceMobile("JourneyInput.Next.Text");

            if (string.IsNullOrEmpty(nextBtn.ToolTip))
                nextBtn.ToolTip = page.GetResourceMobile("JourneyInput.Next.ToolTip");

            // Set data for js to use when changing back behaviour
            backBtn.Attributes["data-backtext"] = backBtn.ToolTip;
        }

        /// <summary>
        /// Sets up the menu
        /// </summary>
        private void SetupMenu()
        {
            TDPPageMobile page = (TDPPageMobile)Page;

            // Set menu link
            lnkMenuWrap.Text = page.GetResourceMobile("HeaderControl.Menu.Link.Text");
            lnkMenuWrap.ToolTip = page.GetResourceMobile("HeaderControl.Menu.Link.ToolTip");
            lnkMenuWrap.Attributes["href"] = "#menunav";

            List<PageId> pageIds = new List<PageId>();

            // Get links for the menu
            string propPages = Properties.Current["Header.Menu.Pages"];

            if (!string.IsNullOrEmpty(propPages))
            {
                string[] propPageIds = propPages.Split(',');

                foreach (string pid in propPageIds)
                {
                    PageId pageId = pid.Parse(PageId.Empty);

                    if (pageId == PageId.Empty)
                        continue;

                    if (pageId == PageId.MobileTravelNews)
                    {
                        // If underground turned off, hide link
                        if (!Properties.Current["UndergroundNews.Enabled.Switch"].Parse(true))
                            continue;
                    }

                    pageIds.Add(pageId);
                }
            }
            
            rptMenu.DataSource = pageIds;
            rptMenu.DataBind();
        }

        /// <summary>
        /// Displays controls
        /// </summary>
        private void DisplayControls()
        {
            // Navigation
            backDiv.Visible = displayNavigation;
            topNavLeftDiv.Visible = displayBack;
            backBtn.Visible = displayBack;
            topNavRightDiv.Visible = displayNext;
            nextBtn.Visible = displayNext;

            // Add client back handler if required 
            // - this prevents the page postback from occuring and is simply a browser redirect
            if (displayBrowserBack)
            {
                backBtn.OnClientClick = "window.history.back(); return false;";
            }
        }

        #endregion

    }
}