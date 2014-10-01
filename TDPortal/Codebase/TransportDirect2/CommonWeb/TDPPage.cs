// *********************************************** 
// NAME             : TDPPage.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Feb 2011
// DESCRIPTION  	: A 'base page template' class derived from System.Web.UI.Page, 
//all other pages on a web site to derive from this class. This provides a 
//single place where behaviour can be altered for all pages on the web site
// ************************************************
// 


using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TDP.Common.EventLogging;
using TDP.Common.Extenders;
using TDP.Common.LocationService;
using TDP.Common.PropertyManager;
using TDP.Common.ResourceManager;
using TDP.Common.ServiceDiscovery;
using TDP.Reporting.Events;
using TDP.UserPortal.JourneyControl;
using TDP.UserPortal.ScreenFlow;
using TDP.UserPortal.SessionManager;
using Logger = System.Diagnostics.Trace;

namespace TDP.Common.Web
{
    /// <summary>
    /// Base class for all web pages.
    /// </summary>
    public class TDPPage : System.Web.UI.Page
    {
        #region Variables
                
        // All sub-classes must have this value set. It indicates the unique id of the page.
        protected PageId pageId = PageId.Empty;

        // Version folder name to use for styles, images, javascript
        protected string versionFolder;
        
        // List of style sheets to apply to the page
        // (stylesheets keyed on a media type or nomedia)
        protected Dictionary<string, List<string>> styleSheets = new Dictionary<string, List<string>>();
        protected const string mediaNone = "NOMEDIA";
        protected List<string> styleSheetsLondon2012 = new List<string>();

        // List of javascript files to apply to the page
        protected List<string> javascriptFiles = new List<string>();
        protected List<string> javascriptFilesLondon2012 = new List<string>();
        protected Dictionary<string, string> javascriptBlocks = new Dictionary<string, string>();

        // Refresh page tag
        protected bool autoRefresh = false;
        protected int autoRefreshSeconds = 0;
        protected string autoRefreshUrl = string.Empty;

        // Resource manager for this page
        protected TDPResourceManager resourceManager;

        // Used to hold all original query string values in a request
        protected NameValueCollection persistedQueryString = null;
        // Used to hold all specified query string values for a redirect
        protected NameValueCollection redirectQueryString = null;

        // Anchor link for navigating to the same page
        protected string anchorLink = string.Empty;

        // Process cookies to detect and raise visitor events
        protected CookieHelper cookieHelper = null;

        // Session timeout flag used for this page lifecycle only (read from SessionKey.IsSessionTimeout)
        protected bool sessionTimeoutFlag = false;

        // Page to redirect user to for Session Timeouts
        protected PageId sessionTimeoutPage = PageId.JourneyPlannerInput;

        // Pages which can be displayed if requested (i.e. excluded) when Session Timeout is detected
        protected List<PageId> sessionTimeoutExcludedPages = new List<PageId>(){PageId.TravelNews};

        // Page to redirect user to for Landing Page processing
        protected PageId landingProcessingPage = PageId.JourneyPlannerInput;
                
        // Pages which can be "bookmarked journey" landing pages
        protected List<PageId> bookmarkJourneyPageIds = new List<PageId>(){PageId.JourneyOptions};

        // Used to prevent a PageEntryEvent being logged (because the chile page itself logs it)
        protected bool logPageEntry = true;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TDPPage(TDPResourceManager resourceManager) : base()
        {
            // Set resource manager for use
            this.resourceManager = resourceManager;
        }

        #endregion

        #region OnInit, OnLoad, OnPreRender, OnUnload

        /// <summary>
        /// Overrides the base OnInit method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            // Capture query string values
            persistedQueryString = this.Request.QueryString;

            // Load the session manager
            TDPSessionManager.Current.OnLoad();

            PerformSiteRedirect();

            base.OnInit(e);

            // Detect for session timeouts and handle accordingly
            if (DetectSessionTimeout())
            {
                ProcessSessionTimeout();
            }
        }

        /// <summary>
        /// Overrides the base OnLoad method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            // Determine if Cookies are supported (used for Repeat visitor tracking)
            cookieHelper = new CookieHelper(pageId);
            cookieHelper.DetectCookies();

            // Setup/Recover request in session if needed
            ProcessRequestRecovery();

            // Check if site preferences specified through query string
            SetSitePreferencesFromQueryStringAndRedirect();
            
            base.OnLoad(e);
        }

        /// <summary>
        /// Overrides the base OnLoadComplete method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoadComplete(EventArgs e) 
        { 
            // Set the page's title using site map, if necessary 
            if (string.IsNullOrEmpty(Page.Title) || Page.Title == "Untitled Page") 
            { 
                // Is this page defined in the site map? 
                string newTitle = GetDefaultPageTitle();

                SiteMapNode current = SiteMap.CurrentNode; 
                if (current != null) 
                { 
                    string pageTitle = GetResource(TDPResourceManager.GROUP_SITEMAP, current.ResourceKey, current.ResourceKey + ".Title");

                    if (string.IsNullOrEmpty(pageTitle))
                    {
                        // Not found in resource, so use sitemap
                        pageTitle = current.Title;
                    }

                    newTitle = string.Format("{0}{1}{2}",
                        pageTitle,
                        GetResource(TDPResourceManager.GROUP_SITEMAP, "Pages", "Pages.Title.Seperator"),
                        GetDefaultPageTitle());
                }

                Page.Title = newTitle; 
            } 

            base.OnLoadComplete(e); 
        }

        /// <summary>
        /// Overrides the base OnPreRender method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            // Set session flags to indicate the user is up and running.
            // This should only be called here as it allows any session timeouts,
            // returning users, landing pages handling to have run
            UpdateSessionFlags();

            // Perform page redirect if required
            PerformPageTransfer();

            // Process the cookie, which raises Visitor events
            cookieHelper.ProcessPersistentCookie();

            // MIS Reporting
            LogPageEntryEvent();
            
            base.OnPreRender(e);

            // Setup the page head items
            AddMetaDetailsToPage();
            AddFavouriteIconToPage();
            AddCanonicalToPage();
            AddStyleSheetsToPage();
            AddJavascriptToPage();
            AddAnalyticsToPage();
        }

        /// <summary>
        /// Overrides the base OnPreRender method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRenderComplete(EventArgs e)
        {
            // Update cookie, place here to allow any child controls which need current cookie values 
            // before they are updated with latest site preferences. Cookie is primarily used for 
            // returning users and recovery
            cookieHelper.UpdateSitePreferencesToCookie();

            base.OnPreRenderComplete(e);
        }

        /// <summary>
        /// Overrides the base OnUnload method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUnload(EventArgs e)
        {
            // Unload the session manager
            TDPSessionManager.Current.OnPreUnload();
            TDPSessionManager.Current.OnUnload();

            base.OnUnload(e);
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read only. The PageId of this page
        /// </summary>
        public PageId PageId
        {
            get { return pageId; }
        }

        /// <summary>
        /// Read only. The site mode to display the site in, i.e. Olympics or Paralympics
        /// </summary>
        public SiteMode SiteModeDisplay
        {
            get
            {
                if (sessionTimeoutFlag)
                    return CurrentSite.SiteModeValueTimeout;
                else
                    return CurrentSite.SiteModeValue;
            }
        }

        /// <summary>
        /// Gets the style sheet path, including the version folder and WAI accessible (if applicable). 
        /// Contains leading and trailing slashes.
        /// e.g. ~/Version/Styles/
        /// The ~ is resolved by .NET to be the application root, use ResolveClientUrl(...) if path being 
        /// used directly in html added to page
        /// </summary>
        public string StyleSheetPath
        {
            get { return string.Format(@"~/{0}/Styles/", GetSiteVersion() );}
        }

        /// <summary>
        /// Gets the style sheet path for London2012 stylesheets, including the version folder and WAI accessible (if applicable). 
        /// Contains leading and trailing slashes.
        /// e.g. ~/Version/Styles/
        /// The ~ is resolved by .NET to be the application root, use ResolveClientUrl(...) if path being 
        /// used directly in html added to page
        /// </summary>
        public string StyleSheetPathLondon2012
        {
            get { return string.Format(@"~/{0}/London2012/Styles/", GetSiteVersion() ); }
        }

        /// <summary>
        /// Gets the image path, including the version folder and WAI accessible (if applicable). 
        /// Contains leading and trailing slashes.
        /// e.g. ~/Version/Images/
        /// The ~ is resolved by .NET to be the application root, use ResolveClientUrl(...) if path being 
        /// used directly in html added to page
        /// </summary>
        public string ImagePath
        {
            get { return string.Format(@"~/{0}/Images/", GetSiteVersion() ); }
        }

        /// <summary>
        /// Gets the image path for London2012, including the version folder and WAI accessible (if applicable). 
        /// Contains leading and trailing slashes.
        /// e.g. ~/Version/Images/
        /// The ~ is resolved by .NET to be the application root, use ResolveClientUrl(...) if path being 
        /// used directly in html added to page
        /// </summary>
        public string ImagePathLondon2012
        {
            get { return string.Format(@"~/{0}/London2012/Images/", GetSiteVersion() ); }
        }

        /// <summary>
        /// Gets the javascript files path, including the version folder. 
        /// Contains leading and trailing slashes.
        /// e.g. ~/Version/Scripts/
        /// The ~ is resolved by .NET to be the application root, use ResolveClientUrl(...) if path being 
        /// used directly in html added to page
        /// </summary>
        public string JavascriptPath
        {
            get { return string.Format(@"~/{0}/Scripts/", GetSiteVersion() ); }
        }

        /// <summary>
        /// Gets the javascript files path for London2012 files, including the version folder. 
        /// Contains leading and trailing slashes.
        /// e.g. ~/Version/Scripts/
        /// The ~ is resolved by .NET to be the application root, use ResolveClientUrl(...) if path being 
        /// used directly in html added to page
        /// </summary>
        public string JavascriptPathLondon2012
        {
            get { return string.Format(@"~/{0}/London2012/Scripts/", GetSiteVersion()); }
        }

        /// <summary>
        /// Gets the javascript path for the Locations data js files, including the version folder.
        /// Contains leading and trailing slashes.
        /// e.g. ~/Version/Scripts/
        /// The ~ is resolved by .NET to be the application root, use ResolveClientUrl(...) if path being 
        /// used directly in html added to page
        /// </summary>
        public string JavascriptPathLocationsData
        {
            get
            {
                string locationsDataFolder = Properties.Current["ScriptRepository.LocationSuggest.ScriptPath"];

                if (!string.IsNullOrEmpty(locationsDataFolder))
                {
                    if (Properties.Current["ScriptRepository.LocationSuggest.ScriptPath.IncludeVersion"].Parse(false))
                    {
                        return string.Format(locationsDataFolder, (GetSiteVersion() + "/"));
                    }
                    
                    return locationsDataFolder;
                }
                else
                {
                    return JavascriptPath;
                }
            }
        }

        /// <summary>
        /// Anchor link to navigate on the same page. 
        /// <remarks>The value assigned should be without '#' as the code adds when building url </remarks>
        /// </summary>
        public string AnchorLink
        {
            get { return anchorLink; }
            set { anchorLink = value; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Method to return the Version folder to use on the site
        /// </summary>
        /// <returns></returns>
        public string GetSiteVersion()
        {
            if (versionFolder == null)
            {
                // Get Version folder from properties service
                versionFolder = Properties.Current["Style.Version"];

                // Trim any slashes from the start and end
                versionFolder = versionFolder.Trim(new char[] { '/' });
            }

            return versionFolder;
        }

        #region Stylesheet and Javascript methods

        /// <summary>
        /// Method to add style sheet to page
        /// </summary>
        /// <param name="styleSheetName"></param>
        public void AddStyleSheet(string styleSheetName)
        {
            // Use default media type, and check if stylesheet already added before inserting
            if (!string.IsNullOrEmpty(styleSheetName))
            {
                if (!styleSheets.ContainsKey(mediaNone))
                {
                    styleSheets.Add(mediaNone, new List<string>());
                }

                if (!styleSheets[mediaNone].Contains(styleSheetName))
                {
                    styleSheets[mediaNone].Add(styleSheetName);
                }
            }
        }

        /// <summary>
        /// Method to add style sheet to page
        /// </summary>
        /// <param name="styleSheetName"></param>
        /// <param name="media">Media type applicable to stylesheet</param>
        public void AddStyleSheet(string styleSheetName, string media)
        {
            if (!string.IsNullOrEmpty(styleSheetName))
            {
                if (string.IsNullOrEmpty(media))
                {
                    AddStyleSheet(styleSheetName);
                }
                else
                {
                    if (!styleSheets.ContainsKey(media.ToLower()))
                    {
                        styleSheets.Add(media.ToLower(), new List<string>());
                    }

                    if (!styleSheets[media.ToLower()].Contains(styleSheetName))
                    {
                        styleSheets[media.ToLower()].Add(styleSheetName);
                    }
                }
            }
        }

        /// <summary>
        /// Method to add London2012 specific style sheet to page
        /// </summary>
        /// <param name="styleSheetName"></param>
        public void AddStyleSheetLondon2012(string styleSheetName)
        {
            if ((!string.IsNullOrEmpty(styleSheetName)) && (!styleSheetsLondon2012.Contains(styleSheetName)))
            {
                styleSheetsLondon2012.Add(styleSheetName);
            }
        }

        /// <summary>
        /// Method to add javascript file to page
        /// </summary>
        /// <param name="javascriptName">Javascript file name (not including path) to include on page</param>
        public void AddJavascript(string javascriptName)
        {
            if ((!string.IsNullOrEmpty(javascriptName)) && (!javascriptFiles.Contains(javascriptName)))
            {
                javascriptFiles.Add(javascriptName);
            }
        }

        /// <summary>
        /// Method to add London2012 specific javascript file to page
        /// </summary>
        /// <param name="javascriptName">Javascript file name (not including path) to include on page</param>
        public void AddJavascriptLondon2012(string javascriptName)
        {
            if ((!string.IsNullOrEmpty(javascriptName)) && (!javascriptFilesLondon2012.Contains(javascriptName)))
            {
                javascriptFilesLondon2012.Add(javascriptName);
            }
        }

        /// <summary>
        /// Method to directly register javascript file to page
        /// </summary>
        /// <param name="javascriptName">Javascript file name (not including path) to include on page</param>
        public void AddJavascriptToPage(string javascriptName)
        {
            string jsFilePath = ResolveClientUrl(JavascriptPath);

            Page.ClientScript.RegisterClientScriptInclude(javascriptName, jsFilePath + javascriptName);
        }

        /// <summary>
        /// Method to directly register javascript block to page. Javascript surrounding tags will be added
        /// </summary>
        /// <param name="javascriptKey">Key to use when adding javascript block</param>
        /// <param name="javascriptBlock">Javascript block to include on page</param>
        public void AddJavascriptBlockToPage(string javascriptKey, string javascriptBlock)
        {
            if (!javascriptBlocks.ContainsKey(javascriptKey))
            {
                javascriptBlocks.Add(javascriptKey, javascriptBlock);
            }
            else
            {
                javascriptBlocks[javascriptKey] = javascriptBlock;
            }
        }

        #endregion

        #region Resource methods

        /// <summary>
        /// Method that returns the resource associated with the given key from
        /// the default resource collection and group
        /// </summary>
        /// <param name="resourceKey">Resource key</param>
        /// <returns>Resource value</returns>
        public string GetResource(string resourceKey)
        {
            return resourceManager.GetString(CurrentLanguage.Value, resourceKey);
        }

        /// <summary>
        /// Method that returns the resource associated with the given key from 
        /// the specified resource collection, using the default group
        /// </summary>
        /// <param name="resourceCollection">Resource collection</param>
        /// <param name="resourceKey">Resource key</param>
        /// <returns>Resource value</returns>
        public string GetResource(string resourceCollection, string resourceKey)
        {
            return resourceManager.GetString(CurrentLanguage.Value, resourceCollection, resourceKey);
        }

        /// <summary>
        /// Method that returns the resource associated with the given key from 
        /// the specified resource collection and group
        /// </summary>
        /// <param name="resourceGroup">Resource group</param>
        /// <param name="resourceCollection">Resource collection</param>
        /// <param name="resourceKey">Resource key</param>
        /// <returns>Resource value</returns>
        public string GetResource(string resourceGroup, string resourceCollection, string resourceKey)
        {
            return resourceManager.GetString(CurrentLanguage.Value, resourceGroup, resourceCollection, resourceKey);
        }
                
        #endregion

        #region Page flow methods

        /// <summary>
        /// Returns the PageTransferDetail object for the specified PageId
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        public PageTransferDetail GetPageTransferDetail(PageId pageId)
        {
            try
            {
                // Get transfer details
                IPageController pageController = TDPServiceDiscovery.Current.Get<IPageController>(ServiceDiscoveryKey.PageController);

                PageTransferDetail transferDetail = pageController.GetPageTransferDetails(pageId);

                return transferDetail;
            }
            catch (Exception ex)
            {
                string message = string.Format("An error occurred retrieving PageTransferDetails for PageId[{0}]. Page has not been declared in the sitemap configuration.", pageId);
                
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, message, ex));

                // This is serious as a page/control is attempting to go to requested page and it hasn't been setup
                throw;
            }
        }

        /// <summary>
        /// Sets the page to transfer to next. 
        /// This value is read in the OnPreRender page event of the TDPPage and redirects to the transfer page 
        /// if it is different to the current page, no further processing occurs on the current page
        /// </summary>
        /// <param name="pageId"></param>
        public void SetPageTransfer(PageId pageId)
        {
            // Set next page, and reset transferred flag
            TDPSessionManager.Current.Session[SessionKey.NextPageId] = pageId;
            TDPSessionManager.Current.Session[SessionKey.Transferred] = false;
        }

        /// <summary>
        /// Adds the query string name and value pair to the page url. 
        /// These values are appended to the url when a page redirect occurs.
        /// </summary>
        /// <param name="name">Query string key name</param>
        /// <param name="value">Query string value. 
        /// If value is not string.empty and key name already exists, the key is updated.
        /// If value is string.empty and key exists, the key is removed.
        /// If value is null, it is ignored. </param>
        public void AddQueryString(string name, string value)
        {
            if (value != null)
            {
                if (redirectQueryString == null)
                {
                    redirectQueryString = new NameValueCollection();
                }

                // if valid value, add/update query string
                if (!string.IsNullOrEmpty(value))
                {
                    redirectQueryString[name] = value;
                }
                else if (value == string.Empty)
                {
                    // If string.empty, remove from the query string if it exists
                    if (redirectQueryString[name] != null)
                    {
                        redirectQueryString.Remove(name);
                    }
                }
            }
        }

        /// <summary>
        /// Adds query string values from session or request querystring tailored for the selected page. 
        /// This should be used to ensure consistency when moving between pages, and helps retain
        /// details accross page flow
        /// </summary>
        /// <param name="pageId"></param>
        public void AddQueryStringForPage(PageId pageId)
        {
            JourneyHelper journeyHelper = new JourneyHelper();
            LandingPageHelper landingHelper = new LandingPageHelper();
            SessionHelper sessionHelper = new SessionHelper();

            string journeyRequestHash = null;
            Journey journeyOutward = null;
            Journey journeyReturn = null;
            Dictionary<string, string> dictLandingPageJO = null;
            switch (pageId)
            {
                case PageId.JourneyOptions:
                case PageId.MobileSummary:
                    
                    // Journey Request
                    AddQueryString(QueryStringKey.JourneyRequestHash, journeyHelper.GetJourneyRequestHash());

                    // Landing Page
                    dictLandingPageJO = landingHelper.BuildJourneyRequestForQueryString(
                        sessionHelper.GetTDPJourneyRequest());

                    foreach (KeyValuePair<string, string> kvp in dictLandingPageJO)
                    {
                        AddQueryString(kvp.Key, kvp.Value);
                    }

                    break;

                case PageId.MobileDetail:

                    // Journey Request
                    AddQueryString(QueryStringKey.JourneyRequestHash, journeyHelper.GetJourneyRequestHash());

                    // Journeys Selected
                    journeyHelper.GetJourneys(out journeyRequestHash, out journeyOutward, out journeyReturn);

                    AddQueryString(QueryStringKey.JourneyIdOutward, (journeyOutward != null) ? journeyOutward.JourneyId.ToString() : null);
                    AddQueryString(QueryStringKey.JourneyIdReturn, (journeyReturn != null) ? journeyReturn.JourneyId.ToString() : null);

                    // Landing Page
                    dictLandingPageJO = landingHelper.BuildJourneyRequestForQueryString(
                        sessionHelper.GetTDPJourneyRequest());

                    foreach (KeyValuePair<string, string> kvp in dictLandingPageJO)
                    {
                        AddQueryString(kvp.Key, kvp.Value);
                    }

                    break;

                case PageId.JourneyLocations:
                case PageId.AccessibilityOptions:
                case PageId.MobileMap:

                    // No query string, page relies on session and we don't want user bookmarking/landing from 
                    // this page as it's halfway through a transaction
                    break;

                case PageId.MobileMapSummary:
                    
                    // Journey Request
                    AddQueryString(QueryStringKey.JourneyRequestHash, journeyHelper.GetJourneyRequestHash());
                    break;

                case PageId.Retailers:
                case PageId.MobileMapJourney:
                case PageId.MobileTravelNews:
                    
                    // Journey Request
                    AddQueryString(QueryStringKey.JourneyRequestHash, journeyHelper.GetJourneyRequestHash());

                    journeyHelper.GetJourneys(out journeyRequestHash, out journeyOutward, out journeyReturn);

                    // Journeys Selected
                    AddQueryString(QueryStringKey.JourneyIdOutward, (journeyOutward != null) ? journeyOutward.JourneyId.ToString() : null);
                    AddQueryString(QueryStringKey.JourneyIdReturn, (journeyReturn != null) ? journeyReturn.JourneyId.ToString() : null);

                    break;
                    
                case PageId.MobileStopInformation:
                    StopInformationHelper stopInfoHelper = new StopInformationHelper();

                    // Stop information parameters should be in session
                    TDPStopLocation location = stopInfoHelper.GetStopInformationLocation(false);
                    StopInformationMode mode = stopInfoHelper.GetStopInformationMode(false);

                    if (location != null)
                    {
                        AddQueryString(QueryStringKey.StopInfoOriginId, location.ID);
                        AddQueryString(QueryStringKey.StopInfoOriginType, TDPLocationTypeHelper.GetTDPLocationTypeQS(location.TypeOfLocation));
                        AddQueryString(QueryStringKey.StopInfoMode, StopInformationModeHelper.GetStopInformationModeQS(mode));
                    }
                    
                    break;
                    
                default:
                    break;
            }
        }

        /// <summary>
        /// Returns true if the page query string contains the key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool QueryStringContains(string key)
        {
            if (persistedQueryString != null)
            {
                return (!string.IsNullOrEmpty(persistedQueryString[key]));
            }

            return false;
        }

        /// <summary>
        /// Adds an auto refresh meta tag to the page for the specified number of seconds and refresh to url
        /// </summary>
        /// <param name="seconds"></param>
        public void AddAutoRefresh(int seconds, string url)
        {
            autoRefresh = true;

            if (seconds >= 0)
                autoRefreshSeconds = seconds;

            if (!string.IsNullOrEmpty(url))
                autoRefreshUrl = url;
        }

        #endregion

        #region Landing page

        /// <summary>
        /// Returns true if the session Landing Page flag is set to true
        /// </summary>
        /// <returns></returns>
        public bool IsLandingPageRequest()
        {
            return TDPSessionManager.Current.Session[SessionKey.IsLandingPage];
        }

        /// <summary>
        /// Returns true if the session Landing Page Auto Plan flag is set to true
        /// </summary>
        /// <returns></returns>
        public bool IsLandingPageAutoPlanRequest()
        {
            return TDPSessionManager.Current.Session[SessionKey.IsLandingPageAutoPlan];
        }
                
        /// <summary>
        /// Resets the landing page flags in session
        /// </summary>
        public void ClearLandingPageFlags()
        {
            TDPSessionManager.Current.Session[SessionKey.IsLandingPage] = false;
            TDPSessionManager.Current.Session[SessionKey.IsLandingPageAutoPlan] = false;
        }

        /// <summary>
        /// Returns true if the session page state contains error messages
        /// </summary>
        /// <returns></returns>
        public bool IsSessionMessages()
        {
            return TDPSessionManager.Current.PageState.Messages.Count > 0;
        }

        #endregion

        #endregion

        #region Private methods

        #region Add to Page Head methods

        /// <summary>
        /// Method to add the Meta details to the Page Head
        /// </summary>
        private void AddMetaDetailsToPage()
        {
            // Use SiteMap to determine meta details to add
            SiteMapNode node = SiteMap.CurrentNode;

            if (node != null)
            {
                string keyMetas = GetResource(TDPResourceManager.GROUP_SITEMAP, node.ResourceKey,
                    string.Format("{0}.Meta.MetaNames", node.ResourceKey));

                if (!string.IsNullOrEmpty(keyMetas))
                {
                    foreach (string key in keyMetas.Split(','))
                    {
                        string metaName = GetResource(TDPResourceManager.GROUP_SITEMAP, node.ResourceKey,
                            string.Format("{0}.Meta.{1}.Name", node.ResourceKey, key));
                        string metaContent = GetResource(TDPResourceManager.GROUP_SITEMAP, node.ResourceKey,
                            string.Format("{0}.Meta.{1}.Content", node.ResourceKey, key));

                        if (!string.IsNullOrEmpty(metaName) && !string.IsNullOrEmpty(metaContent))
                        {
                            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
                            Page.Header.Controls.Add(new MetaControl(metaName, metaContent));
                        }
                    }
                }
            }

            // Auto refresh
            if (autoRefresh)
            {
                using (HtmlMeta meta = new HtmlMeta())
                {
                    meta.HttpEquiv = "refresh";
                    meta.Content = string.Format("{0};{1}", autoRefreshSeconds, autoRefreshUrl);

                    Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
                    Page.Header.Controls.Add(meta);
                }
            }
        }

        /// <summary>
        /// Method to add the Favourite/Bookmark icon to Page Head
        /// </summary>
        private void AddFavouriteIconToPage()
        {
            string favIconPath = ImagePath + "Icons/";
            string favIconName = "favicon.ico";

            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
            Page.Header.Controls.Add(new FavouriteIconControl(favIconPath, favIconName));

            // Add any other icon links
            // Use SiteMap to determine meta details to add
            SiteMapNode node = SiteMap.CurrentNode;
            if (node != null)
            {
                string keyLinks = GetResource(TDPResourceManager.GROUP_SITEMAP, node.ResourceKey,
                    string.Format("{0}.Link.LinkNames", node.ResourceKey));

                string logoIconPath = ImagePath + "Logos/";

                if (!string.IsNullOrEmpty(keyLinks))
                {
                    foreach (string key in keyLinks.Split(','))
                    {
                        string rel = GetResource(TDPResourceManager.GROUP_SITEMAP, node.ResourceKey, 
                            string.Format("{0}.Link.{1}.Rel", node.ResourceKey, key));
                        string type = GetResource(TDPResourceManager.GROUP_SITEMAP, node.ResourceKey,
                            string.Format("{0}.Link.{1}.Type", node.ResourceKey, key));
                        string icon = GetResource(TDPResourceManager.GROUP_SITEMAP, node.ResourceKey,
                            string.Format("{0}.Link.{1}.Icon", node.ResourceKey, key));
                        string sizes = GetResource(TDPResourceManager.GROUP_SITEMAP, node.ResourceKey,
                            string.Format("{0}.Link.{1}.Sizes", node.ResourceKey, key));

                        Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
                        Page.Header.Controls.Add(new FavouriteIconControl(logoIconPath, icon, rel, type, sizes));
                    }
                }
            }
        }

        /// <summary>
        /// Method to add the Canonical tag to the Page Head
        /// </summary>
        private void AddCanonicalToPage()
        {
            if (Properties.Current["Canonical.Tag.Include.Switch"].Parse(false))
            {
                Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
                Page.Header.Controls.Add(new CanonicalControl(resourceManager));
            }
        }

        /// <summary>
        /// Method to add the Javascript files to the Page Head
        /// </summary>
        private void AddJavascriptToPage()
        {
            #region London2012 javascript

            string jsFilePath = ResolveClientUrl(JavascriptPathLondon2012);

            foreach (string jsFile in javascriptFilesLondon2012)
            {
                if (jsFile.StartsWith("http"))
                {
                    Page.ClientScript.RegisterClientScriptInclude(jsFile, jsFile);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptInclude(jsFile, jsFilePath + jsFile);
                }
            }

            #endregion

            #region javascript

            jsFilePath = ResolveClientUrl(JavascriptPath);

            foreach (string jsFile in javascriptFiles)
            {
                if (jsFile.StartsWith("http"))
                {
                    Page.ClientScript.RegisterClientScriptInclude(jsFile, jsFile);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptInclude(jsFile, jsFilePath + jsFile);
                }
            }

            #endregion

            #region Javascript blocks

            foreach (KeyValuePair<string, string> kvp in javascriptBlocks)
            {
                if (!this.Page.ClientScript.IsClientScriptBlockRegistered(kvp.Key))
                {
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), kvp.Key, kvp.Value, true);
                }
            }

            #endregion
        }

        /// <summary>
        /// Method to add the Analytics tag to the Page Head
        /// </summary>
        private void AddAnalyticsToPage()
        {
            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
            Page.Header.Controls.Add(new AnalyticsControl(resourceManager));
        }

        #region StyleSheets

        /// <summary>
        /// Adds the stylesheets to the page
        /// </summary>
        private void AddStyleSheetsToPage()
        {
            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));

            #region London2012 style sheets

            string styleSheetPath = StyleSheetPathLondon2012;

            foreach (string styleSheetName in styleSheetsLondon2012)
            {
                if (styleSheetName.StartsWith("http"))
                {
                    Page.Header.Controls.Add(new StyleSheetControl(styleSheetName, string.Empty));
                    Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
                }
                else if (StyleSheetExists(styleSheetPath, styleSheetName))
                {
                    Page.Header.Controls.Add(new StyleSheetControl(styleSheetPath, styleSheetName));
                    Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
                }
            }

            #endregion

            #region style sheets

            styleSheetPath = StyleSheetPath;

            foreach (KeyValuePair<string, List<string>> kvp in styleSheets)
            {
                if ((kvp.Key == mediaNone) && (kvp.Value != null))
                {
                    // Add no media specified stylesheets
                    foreach (string styleSheetName in kvp.Value)
                    {
                        if (styleSheetName.StartsWith("http"))
                        {
                            Page.Header.Controls.Add(new StyleSheetControl(styleSheetName, string.Empty));
                            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
                        }
                        else if (StyleSheetExists(styleSheetPath, styleSheetName))
                        {
                            Page.Header.Controls.Add(new StyleSheetControl(styleSheetPath, styleSheetName));
                            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
                        }
                    }
                }
                else if (kvp.Value != null)
                {
                    // Add media specified stylesheets
                    foreach (string styleSheetName in kvp.Value)
                    {
                        if (styleSheetName.StartsWith("http"))
                        {
                            Page.Header.Controls.Add(new StyleSheetControl(styleSheetName, string.Empty));
                            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
                        }
                        if (StyleSheetExists(styleSheetPath, styleSheetName))
                        {
                            Page.Header.Controls.Add(new StyleSheetControl(styleSheetPath, styleSheetName, kvp.Key));
                            Page.Header.Controls.Add(new LiteralControl(Environment.NewLine));
                        }
                    }
                }
            }

            #endregion

        }

        /// <summary>
        /// Method to check if the style sheet exists at the given path
        /// </summary>
        /// <param name="styleSheetPath"></param>
        /// <param name="styleSheetName"></param>
        /// <returns></returns>
        private bool StyleSheetExists(string styleSheetPath, string styleSheetName)
        {
            bool styleSheetFound = false;

            try
            {
                string path = HttpContext.Current.Server.MapPath(styleSheetPath + styleSheetName);
                styleSheetFound = File.Exists(path);
            }
            catch (Exception ex)
            {
                StringBuilder message = new StringBuilder();

                message.Append("An error occurred in the method TDPPage.StyleSheetExists(), detecting if the file[");
                message.Append(styleSheetPath + styleSheetName);
                message.Append("] existed in the styles folder. See inner exception for details.");

                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, message.ToString(), ex));
            }

            return styleSheetFound;
        }

        #endregion
                
        #endregion

        #region Site preference methods

        /// <summary>
        /// Method to set site style preferences to use if value provided in request query string
        /// </summary>
        private void SetSitePreferencesFromQueryStringAndRedirect()
        {
            bool performRedirect = false;

            #region Language

            string strRequestedLanguage = persistedQueryString[QueryStringKey.Language];

            if (!string.IsNullOrEmpty(strRequestedLanguage))
            {
                Language requestedLanguage = LanguageHelper.Default;

                // Parse the language requested
                try
                {
                    requestedLanguage = LanguageHelper.ParseLanguage(strRequestedLanguage);

                    performRedirect = true;
                }
                catch
                {
                    // Unknown language requested, use the default
                }

                CurrentLanguage.Value = requestedLanguage;
            }

            #endregion

            #region Font size

            string strRequestedFontSize = persistedQueryString[QueryStringKey.FontSize];

            if (!string.IsNullOrEmpty(strRequestedFontSize))
            {
                FontSize requestedFontSize = CurrentStyle.FontSizeDefault;

                // Parse the font size requested
                try
                {
                    requestedFontSize = CurrentStyle.ParseFontSize(strRequestedFontSize);

                    performRedirect = true;
                }
                catch
                {
                    // Unknown value requested, use the default
                }

                CurrentStyle.FontSizeValue = requestedFontSize;
            }

            #endregion

            #region Accessible style

            string strRequestedAccessibleStyle = persistedQueryString[QueryStringKey.AccessiblityStyle];

            if (!string.IsNullOrEmpty(strRequestedAccessibleStyle))
            {
                AccessibleStyle requestedAccessibleStyle = CurrentStyle.AccessibleStyleDefault;

                // Parse the accessible style requested
                try
                {
                    requestedAccessibleStyle = CurrentStyle.ParseAccessibleStyle(strRequestedAccessibleStyle);

                    performRedirect = true;
                }
                catch
                {
                    // Unknown value requested, use the default
                }

                CurrentStyle.AccessibleStyleValue = requestedAccessibleStyle;
            }

            #endregion

            #region Navigation (header and footer) preference

            string strNavigation = persistedQueryString[QueryStringKey.Navigation];

            if (!string.IsNullOrEmpty(strNavigation))
            {
                // Session default is false if key missing, but default is to show navigation

                // Check if navigation is required
                if (strNavigation.Trim() == "1")
                {
                    // Flag in session for the master page class to use
                    TDPSessionManager.Current.Session[SessionKey.IsNavigationNotRequired] = false;

                    performRedirect = true;
                }
                else if (strNavigation.Trim() == "0")
                {
                    // Flag in session for the master page class to use
                    TDPSessionManager.Current.Session[SessionKey.IsNavigationNotRequired] = true;

                    performRedirect = true;
                }
            }

            #endregion

            #region Site mode

            string strRequestedSiteMode = persistedQueryString[QueryStringKey.SiteMode];

            if (!string.IsNullOrEmpty(strRequestedSiteMode))
            {
                SiteMode requestedSiteMode = CurrentSite.SiteModeDefault;

                // Parse the site mode requested
                try
                {
                    requestedSiteMode = CurrentSite.ParseSiteMode(strRequestedSiteMode);

                    performRedirect = true;
                }
                catch
                {
                    // Unknown value requested, use the default
                }

                CurrentSite.SiteModeValue = requestedSiteMode;
            }

            #endregion

            #region Debug preference

            string strDebugMode = persistedQueryString[QueryStringKey.DebugMode];

            if (!string.IsNullOrEmpty(strDebugMode))
            {
                // Check if debug mode is required
                if (strDebugMode.Trim() == "1")
                {
                    // Flag in session for the DebugHelper class to use
                    TDPSessionManager.Current.Session[SessionKey.IsDebugMode] = true;

                    performRedirect = true;
                }
                else if (strDebugMode.Trim() == "0")
                {
                    // Flag in session for the DebugHelper class to use
                    TDPSessionManager.Current.Session[SessionKey.IsDebugMode] = false;

                    performRedirect = true;
                }
            }

            #endregion

            if (performRedirect)
            {
                URLHelper urlHelper = new URLHelper();
                string url = Request.RawUrl;

                // Remove preferences from the query string
                url = urlHelper.RemoveQueryStringPart(url, QueryStringKey.Language);
                url = urlHelper.RemoveQueryStringPart(url, QueryStringKey.FontSize);
                url = urlHelper.RemoveQueryStringPart(url, QueryStringKey.AccessiblityStyle);
                url = urlHelper.RemoveQueryStringPart(url, QueryStringKey.Navigation);
                url = urlHelper.RemoveQueryStringPart(url, QueryStringKey.SiteMode);
                url = urlHelper.RemoveQueryStringPart(url, QueryStringKey.DebugMode);

                // Can't use Server.Transfer as it seems to retain original query string values, 
                // redirect to avoid endless loop
                Response.Redirect(url, true);
            }
        }
        
        #endregion

        #region Page flow methods

        /// <summary>
        /// Performs a response redirect if next page set is different to current
        /// </summary>
        private void PerformPageTransfer()
        {
            // Look what the next page id has been set to
            PageId nextPageId = TDPSessionManager.Current.Session[SessionKey.NextPageId];
            bool tranferred = TDPSessionManager.Current.Session[SessionKey.Transferred];

            // If next page is different to current, and not already transferred
            if ((this.pageId != nextPageId) && (!tranferred) && (nextPageId != PageId.Empty))
            {
                // Set transferred flag to prevent redirect occurring again
                TDPSessionManager.Current.Session[SessionKey.Transferred] = true;

                // Get transfer details
                PageTransferDetail transferDetail = GetPageTransferDetail(nextPageId);

                string transferUrl = transferDetail.PageUrl;

                // Append any query string values set
                URLHelper urlHelper = new URLHelper();

                transferUrl = urlHelper.AddQueryStringParts(transferUrl, redirectQueryString);

                //Add anchor link to navigate on the same page
                if (!string.IsNullOrEmpty(anchorLink))
                {
                    transferUrl += string.Format("#{0}", anchorLink);
                }
                
                // Go to the next page
                Response.Redirect(transferUrl);
            }
        }

        #endregion

        #region Session (recovery, timeout, flags) methods

        /// <summary>
        /// Detects if the session has timed-out. If true, then the process session timeout
        /// logic is called
        /// </summary>
        private bool DetectSessionTimeout()
        {
            return SessionHelper.DetectSessionTimeout(Context, Session, Request);
        }

        /// <summary>
        /// Performs session timeout handling. Redirects user to appropriate page
        /// </summary>
        private void ProcessSessionTimeout()
        {
            // Flag timeout has occurred to allow recovery next time the input page (session timeout page) is displayed
            TDPSessionManager.Current.Session[SessionKey.IsSessionTimeout] = true;

            // Get url for the input page (session timeout page)
            PageTransferDetail ptd = GetPageTransferDetail(sessionTimeoutPage);
            string transferUrl = ptd.PageUrl;

            #region Retain landing page request details

            // Possible that a landing page request is being submitted for an expired session. 
            // Therefore do not want to lose the landing page details, add the query string values 
            // to the url.
            URLHelper urlHelper = new URLHelper();
            transferUrl = urlHelper.AddQueryStringParts(transferUrl, persistedQueryString);

            // Remove any parameters which could cause landing page functionality problems
            transferUrl = urlHelper.RemoveQueryStringPart(transferUrl, QueryStringKey.JourneyRequestHash);

            #endregion

            #region Add timeout message to display

            SessionHelper sessionHelper = new SessionHelper();

            // Where the querystring contains landing page parameters, then no need to show the timeout message
            LandingPageHelper landingHelper = new LandingPageHelper();
            if ((landingHelper.ContainsLandingPageParameters(persistedQueryString)) && (!Page.IsPostBack))
            {
                sessionHelper.ClearMessages();
            }
            // Where this is a direct request to the input page (session timeout page), 
            // then no need to show the timeout message
            else if ((pageId == sessionTimeoutPage) && (!Page.IsPostBack))
            {
                sessionHelper.ClearMessages();
            }
            // Where this is a direct request to a page in the session timeout excluded pages list,
            // then safe to go there without the timeout message displayed
            else if ((sessionTimeoutExcludedPages.Contains(pageId)) && (!Page.IsPostBack))
            {
                sessionHelper.ClearMessages();

                // Allow redirect to the requested page rather than the session timeout page
                transferUrl = GetPageTransferDetail(pageId).PageUrl;

                // Retain landing page request details if exists
                transferUrl = urlHelper.AddQueryStringParts(transferUrl, persistedQueryString);

                // Remove any parameters which could cause landing page functionality problems
                transferUrl = urlHelper.RemoveQueryStringPart(transferUrl, QueryStringKey.JourneyRequestHash);
            }
            // Where this is a postback request to the input page (session timeout page),
            // and we're not on the input page (session timeout page), 
            // then no need to show timeout message
            else if ((pageId != sessionTimeoutPage) && (Page.IsPostBack))
            {
                // Add timeout message to be displayed
                sessionHelper.AddMessage(new TDPMessage("SessionTimeout.Message.Text", TDPMessageType.Warning));

                // Check if the postback event should or shouldnt display a timeout message
                string postbackControlId = this.GetPostBackControlId();

                if (!sessionHelper.DisplayTimeoutMessage(pageId, postbackControlId))
                {
                    sessionHelper.ClearMessages();
                }
            }
            // Where this is a postback request on any other page, check if the postback event 
            // is allowed to continue for the control, e.g. if user on input page and selects plan journey
            // which is ok to continue (rather than re-displaying input page with values entered reset)
            else if (Page.IsPostBack)
            {
                // Add timeout message to be displayed
                sessionHelper.AddMessage(new TDPMessage("SessionTimeout.Message.Text", TDPMessageType.Warning));

                // Check if the postback event should be allowed to continue
                string postbackControlId = this.GetPostBackControlId();

                if (sessionHelper.AllowTimeoutEvent(pageId, postbackControlId))
                {
                    sessionHelper.ClearMessages();

                    // Do not redirect
                    return;
                }
            }
            else
            {
                // Add timeout message to be displayed
                sessionHelper.AddMessage(new TDPMessage("SessionTimeout.Message.Text", TDPMessageType.Warning));
            }

            #endregion

            // Redirect to the input page (session timeout page), must be done here because don't want any further 
            // processing to occur. This allows request recovery to be performed on the next page cycle
            Response.Redirect(transferUrl);
        }

        /// <summary>
        /// Performs recovery of the last known journey request using the cookie or Request query string
        /// </summary>
        private void ProcessRequestRecovery()
        {
            // Check if the journey request should be restored.

            // The journey request should only be restored for the following situations:
            // - a returning user (has no session/deferreddata)
            // - a user coming from error page (has session/deferreddata)
            // - a session timeout (may have deferreddata which could be invalid)
            // 
            // The journey request should not be restored for the following situations:
            // - a user continuing to use site (has session/deferreddata)
            // - a landing page request for a new user (has no session/deferreddata)
            // - a landing page request for a returning user (has session/deferreddata)

            ITDPSession session = TDPSessionManager.Current.Session;
            ITDPJourneyRequest tdpJourneyRequest = null;
            LandingPageHelper landingHelper = new LandingPageHelper(persistedQueryString, pageId.ToString().Contains("Mobile"));


            // Returning user's first page hit, or
            // Session timeout hit following a redirect to journey input page, or
            // Error page hit
            if ((!session[SessionKey.IsSessionInitialised]) ||
                (session[SessionKey.IsSessionTimeout]) ||
                (session[SessionKey.IsErrorPage]))
            {
                #region Load/Recover from query string

                // Load request from query string first

                // Check if this is a landing page request
                if (landingHelper.IsLandingPageRequest(pageId, bookmarkJourneyPageIds, false, false))
                {
                    tdpJourneyRequest = landingHelper.RetrieveJourneyRequestFromQueryString();

                    // Add flag to indicate landing page and auto plan
                    session[SessionKey.IsLandingPage] = true;
                    session[SessionKey.IsLandingPageAutoPlan] = landingHelper.IsAutoPlan();

                    // If current page isn't the landing processing page (e.g. JourneyPlannerInput), then must take them there
                    // to complete the landing page request
                    if (pageId != landingProcessingPage)
                    {
                        SetPageTransfer(landingProcessingPage);
                    }

                    // MIS Reporting
                    LogLandingPageEntryEvent(landingHelper.GetPartner());
                }

                #endregion

                #region Load/Recover from cookie

                if (tdpJourneyRequest == null)
                {
                    // Load request from cookie secondly
                    tdpJourneyRequest = cookieHelper.RetrieveJourneyRequestFromCookie();
                }

                #endregion

                // Set any internal flags required by any method for the remainder of this page lifecycle
                sessionTimeoutFlag = session[SessionKey.IsSessionTimeout];

                // Reset the Session flags
                session[SessionKey.IsSessionTimeout] = false;
                session[SessionKey.IsErrorPage] = false;
            }
            // User continuing to use the site, check for "new" landing page request
            else if (session[SessionKey.IsSessionInitialised])
            {
                #region Load/Recover from query string

                // Check if this is a landing page request
                if (landingHelper.IsLandingPageRequest(pageId, bookmarkJourneyPageIds, session[SessionKey.Transferred], Page.IsPostBack))
                {
                    tdpJourneyRequest = landingHelper.RetrieveJourneyRequestFromQueryString();

                    // Add flag to indicate landing page and auto plan
                    session[SessionKey.IsLandingPage] = true;
                    session[SessionKey.IsLandingPageAutoPlan] = landingHelper.IsAutoPlan();

                    // If current page isn't the landing processing page JourneyPlannerInput page, then must take them there
                    // to complete the landing page request
                    if (pageId != landingProcessingPage)
                    {
                        SetPageTransfer(landingProcessingPage);
                    }

                    // MIS Reporting
                    LogLandingPageEntryEvent(landingHelper.GetPartner());
                }

                #endregion
            }

            #region Update session with journey request

            // Update to session
            if (tdpJourneyRequest != null)
            {
                SessionHelper sessionHelper = new SessionHelper();

                sessionHelper.UpdateSession(tdpJourneyRequest);
            }

            #endregion
        }

        /// <summary>
        /// Update session flags to aid recovery 
        /// </summary>
        private void UpdateSessionFlags()
        {
            // Indicate session is now ready to go and no further recovery functionality should happen
            // for this user
            TDPSessionManager.Current.Session[SessionKey.IsSessionInitialised] = true;

            // If Error page, flag in session to allow recovery next time user goes to JourneyInputPage
            if (pageId == PageId.Error)
            {
                TDPSessionManager.Current.Session[SessionKey.IsErrorPage] = true;
            }
        }

        #endregion

        #region MIS Reporting

        /// <summary>
        /// Logs a PageEntryEvent for the current page
        /// </summary>
        private void LogPageEntryEvent()
        {
            if (logPageEntry)
            {
                // Log page entry event (done even if a postback as site is still "rendering" a page)
                PageEntryEvent logPage = new PageEntryEvent(pageId, TDPSessionManager.Current.Session.SessionID, false);
                Logger.Write(logPage);
            }
        }

        /// <summary>
        /// Logs a LandingPageEntryEvent
        /// </summary>
        private void LogLandingPageEntryEvent(string partner)
        {
            // JourneyOptions page would indicate this is a bookmarked journey landing page request
            LandingPageService lps = (bookmarkJourneyPageIds.Contains(pageId)) ? LandingPageService.TDPJourneyBookmark : LandingPageService.TDP;

            LandingPageEntryEvent lpee = new LandingPageEntryEvent(partner, lps, TDPSessionManager.Current.Session.SessionID, false);
            Logger.Write(lpee);

            if (DebugHelper.ShowDebug || TDPTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                                string.Format("LandingPage - {0}", Request.RawUrl)));
            }
        }

        #endregion

        #endregion

        #region Protected methods

        #region Resource

        /// <summary>
        /// Returns the default page title 
        /// </summary>
        /// <returns></returns>
        protected virtual string GetDefaultPageTitle()
        {
            return GetResource(TDPResourceManager.GROUP_SITEMAP, "Pages", "Pages.Title");
        }

        #endregion

        #region Mobile redirect
        /// <summary>
        /// Redirect to the mobile site if appropriate
        /// </summary>
        protected virtual void PerformSiteRedirect()
        {
            SiteRedirectHelper.PerformSiteRedirect(Request, Response, Context, Session, pageId);
        }
        #endregion

        #endregion
    }

    public static class PageExtenders
    {
        /// <summary>
        /// Gets the ID of the post back control.
        /// 
        /// See: http://geekswithblogs.net/mahesh/archive/2006/06/27/83264.aspx
        /// </summary>
        /// <param name = "page">The page.</param>
        /// <returns></returns>
        public static string GetPostBackControlId(this Page page)
        {
            if (!page.IsPostBack)
                return string.Empty;

            Control control = null;
            // first we will check the "__EVENTTARGET" because if post back made by the controls
            // which used "_doPostBack" function also available in Request.Form collection.
            string controlName = page.Request.Params["__EVENTTARGET"];
            if (!String.IsNullOrEmpty(controlName))
            {
                control = page.FindControl(controlName);
            }
            else
            {
                // if __EVENTTARGET is null, the control is a button type and we need to
                // iterate over the form collection to find it

                // ReSharper disable TooWideLocalVariableScope
                string controlId;
                Control foundControl;
                // ReSharper restore TooWideLocalVariableScope

                foreach (string ctl in page.Request.Form)
                {
                    // handle ImageButton they having an additional "quasi-property" 
                    // in their Id which identifies mouse x and y coordinates
                    if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                    {
                        controlId = ctl.Substring(0, ctl.Length - 2);
                        foundControl = page.FindControl(controlId);
                    }
                    else
                    {
                        foundControl = page.FindControl(ctl);
                    }

                    if (!(foundControl is Button || foundControl is ImageButton)) continue;

                    control = foundControl;
                    break;
                }
            }

            return control == null ? String.Empty : control.ID;
        }
    }
}
