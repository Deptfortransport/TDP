// *********************************************** 
// NAME             : TDPPageMobile.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Feb 2011
// DESCRIPTION  	: A 'base page template' class derived from TDPPage (System.Web.UI.Page), 
//all other pages on the mobile web site derive from this class. This provides a 
//single place where behaviour can be altered for all pages on the mobile web site
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.ResourceManager;

namespace TDP.Common.Web
{
    /// <summary>
    /// Base class for all mobile web pages
    /// </summary>
    public class TDPPageMobile : TDPPage
    {
        #region Private members

        private static string Mobile_ResourceGroup = TDPResourceManager.GROUP_MOBILE;
        private static string Mobile_ResourceCollection = TDPResourceManager.COLLECTION_DEFAULT;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TDPPageMobile(TDPResourceManager resourceManager)
            : base(resourceManager)
        {
            // Override the mobile page specific values
            this.sessionTimeoutPage = PageId.MobileInput;
            this.sessionTimeoutExcludedPages.Add(PageId.MobileDefault);
            this.sessionTimeoutExcludedPages.Add(PageId.MobileTravelNews);
            this.sessionTimeoutExcludedPages.Add(PageId.MobileStopInformation);
            this.landingProcessingPage = PageId.MobileInput;
            this.bookmarkJourneyPageIds = new List<PageId>() { PageId.MobileSummary, PageId.MobileDetail };
        }

        #endregion

        #region OnInit, OnLoad, OnPreRender, OnUnload

        /// <summary>
        /// Overrides the base OnPreRender method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            AddUserAgentStyleSheet();

            base.OnPreRender(e);
        }
        
        #endregion

        #region Public methods

        #region Resource methods

        /// <summary>
        /// Method that returns the resource associated with the given key from
        /// the mobile resource collection and group
        /// </summary>
        /// <param name="resourceKey">Resource key</param>
        /// <returns>Resource value</returns>
        public string GetResourceMobile(string resourceKey)
        {
            return base.GetResource(Mobile_ResourceGroup, Mobile_ResourceCollection, resourceKey);
        }
        
        #endregion

        #endregion

        #region Private methods

        /// <summary>
        /// Adds stylesheet based on user agent
        /// </summary>
        private void AddUserAgentStyleSheet()
        {
            string userAgent = this.Request.UserAgent.ToLower();

            if (!string.IsNullOrEmpty(userAgent))
            {
                string css = string.Empty;

                #region Android
                if (userAgent.Contains("android"))
                {
                    css = "device/android-320.css"; // default

                    if (userAgent.Contains("gt-i9300"))
                    { // samsung galaxy s3
                        css = "device/android-360.css";
                    }
                    else if (userAgent.Contains("gt-i9500"))
                    { // samsung galaxy s4
                        css = "device/android-360.css";
                    }
                    else if (userAgent.Contains("c5303"))
                    { // sony xperia sp
                        css = "device/android-360.css";
                    }
                    else if (userAgent.Contains("st26i"))
                    { // sony xperia j
                        css = "device/android-360.css";
                    }
                    else if (userAgent.Contains("htc_wildfires"))
                    { // htc wildfire s
                        css = "device/android-320.css";
                    }
                    else if (userAgent.Contains("htc magic"))
                    { // htc magic
                        css = "device/android-320.css";
                    }
                    else if (userAgent.Contains("shw-m110s"))
                    { // samsung galaxy s
                        css = "device/android-320.css";
                    }
                    else if (userAgent.Contains("s5830"))
                    { // samsung galaxy ace s5830
                        css = "device/android-320.css";
                    }
                    else if (userAgent.Contains("e10i"))
                    { // sonyericsson x10 mini/e10i
                        css = "device/android-240.css";
                    }
                }
                #endregion
                #region Ipad
                else if (userAgent.Contains("ipad"))
                {
                    css = "device/ipad.css";
                }
                #endregion
                #region Iphone
                else if (userAgent.Contains("iphone"))
                {
                    css = "device/iphone.css";
                }
                #endregion
                #region Blackberry
                else if (userAgent.Contains("blackberry"))
                {
                    css = "device/android-320.css"; // default

                    if (userAgent.Contains("9300"))
                    { // blackberry curve 9300
                        css = "device/android-320.css";
                    }
                    else if (userAgent.Contains("8520"))
                    { // blackberry curve 8520
                        css = "device/android-320.css";
                    }
                }
                #endregion
                #region Windows phone
                else if (userAgent.Contains("windows phone"))
                {
                    css = "device/windows-phone.css"; // default

                    if (userAgent.Contains("lumia 800"))
                    { // nokia lumia 800
                        css = "device/windows-phone.css";
                    }
                    else if (userAgent.Contains("lg-e900"))
                    { // lg e900
                        css = "device/windows-phone.css";
                    }
                }
                #endregion

                if (!string.IsNullOrEmpty(css))
                {
                    AddStyleSheet(css);
                }
            }
        }

        #endregion

        #region Protected methods

        #region Resource

        /// <summary>
        /// Returns the default page title 
        /// </summary>
        /// <returns></returns>
        protected override string GetDefaultPageTitle()
        {
            return GetResource(TDPResourceManager.GROUP_SITEMAP, "Pages", "Pages.Title.Mobile");
        }

        #endregion

        #region Mobile redirect
        /// <summary>
        /// Override so as not to perform from the mobile site
        /// </summary>
        protected override void PerformSiteRedirect()
        {
            // don't redirect from the mobile site
        }
        #endregion

        #endregion
    }
}
