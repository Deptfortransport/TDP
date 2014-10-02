// *********************************************** 
// NAME                 : HeaderControl.aspx.cs 
// AUTHOR               : Andrew Sinclair 
// DATE CREATED         : 15/07/2003 
// DESCRIPTION			: A custom user control to 
// display the header logo and links for each page.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/HeaderControl.ascx.cs-arc  $
//
//   Rev 1.15   Aug 11 2011 14:19:14   dlane
//Removing IE9 directive; adding IE7 hack for focus for skip link and nav buttons
//
//   Rev 1.14   Jul 28 2011 16:19:04   DLane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.13   Jul 20 2010 11:56:12   mmodi
//Removed Login control from header as no longer used
//Resolution for 5010: Cannot submit new user registration details using "Enter" key
//
//   Rev 1.12   Apr 19 2010 15:55:20   apatel
//Accessibility issues - ie7 keyboard focus indicator - Resoving XHTML compliance issues
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.11   Apr 07 2010 14:39:56   apatel
//Accessibility issues - IE7 keyboard focus indicator: Comment added to the refactored code
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.10   Apr 07 2010 13:08:28   apatel
//Accessibility issues - IE7 Keyboard focus indicator: Code changes
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.9   Apr 07 2010 12:52:54   apatel
//Accessibility issues - IE7 keybord focus Indicator : Code changes
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.8   Mar 30 2010 09:13:52   apatel
//Cycle planner white label changes
//Resolution for 5488: Cycle Planner white label changes
//
//   Rev 1.7   Oct 13 2008 16:41:32   build
//Automatically merged from branch for stream5014
//
//   Rev 1.6.1.0   Sep 15 2008 11:02:54   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.6   Jul 21 2008 14:48:14   mmodi
//Updated navigation tabs display, and removed redundant code
//Resolution for 5081: Navigation tabs - Weird display on background when clicked
//
//   Rev 1.5   Jul 15 2008 16:42:30   mmodi
//Save the querystring
//Resolution for 5065: Log in issues - Find a map, and Help pages
//
//   Rev 1.4   Jun 26 2008 14:04:14   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.3   May 01 2008 15:06:50   apatel
//Modify to add back button functionality on login/logout.
//Resolution for 4920: Login/Logout page issues
//
//   Rev 1.2   Mar 31 2008 13:21:00   mturner
//Drop3 from Dev Factory
//
//
//  Rev devfactory Feb 07 2008 12:12:13 dsawe
//  Chaged the home page buttons look after white labelling change
//
//   Rev 1.0   Nov 08 2007 13:14:40   mturner
//Initial revision.
//
//   Rev 1.26   Jul 14 2006 12:50:46   mturner
//Fixed error with Login control introduced in revision 1.25
//
//   Rev 1.25   Jul 11 2006 11:15:06   PScott
//4318658 - Amend Spotlight tags
//
//   Rev 1.24   Apr 20 2006 11:07:10   rwilby
//Added properties UseTDPLogoURL and TDPLogoURL. 
//Added logic in the homeImageButton_Click event handler method to redirect to the specified 'TDPLogoURL' url if the boolean UseTDPLogoURL is true.
//Resolution for 3851: Error and Timeout pages - new wording and layout
//
//   Rev 1.23   Feb 24 2006 12:59:20   AViitanen
//Post-merge fix for stream3129.
//
//   Rev 1.22   Feb 23 2006 16:11:24   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.21   Feb 10 2006 15:04:42   build
//Automatically merged from branch for stream3180
//
//   Rev 1.20.1.13   Jan 11 2006 13:54:40   tmollart
//Updated after comments from code review. Adjusted some of the TabSection names.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.20.1.12   Jan 09 2006 15:55:36   RGriffith
//Changes made in light of code review comments
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.20.1.11   Dec 30 2005 12:00:54   NMoorhouse
//Updated following screen review
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.20.1.10   Dec 23 2005 15:23:06   RGriffith
//FxCop suggested changes
//
//   Rev 1.20.1.9   Dec 23 2005 14:26:12   RGriffith
//Changes to use TDImage
//
//   Rev 1.20.1.8   Dec 23 2005 12:10:44   RGriffith
//FxCop change
//
//   Rev 1.20.1.7   Dec 16 2005 18:41:12   AViitanen
//Changed default language to English. Changed SetJavaScriptHyperlink so link is not visible on certain pages.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.20.1.6   Dec 12 2005 17:15:14   tmollart
//Added code to clear the cache.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.20.1.5   Dec 09 2005 14:25:52   NMoorhouse
//Added transitions to new mini-homepages to tab selections
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.20.1.4   Dec 07 2005 08:58:32   RGriffith
//Changes for visibility of tab images and login controls
//
//   Rev 1.20.1.3   Dec 02 2005 11:34:30   RGriffith
//Minor Changes to the use of javasriptDetection
//
//   Rev 1.20.1.2   Dec 01 2005 12:11:50   RGriffith
//Minor changes in navigation and addition of the calling of the ClearCache method in the tab button click events
//
//   Rev 1.20.1.1   Nov 30 2005 18:27:54   RGriffith
//Put the setting of image URL's and Alternate text into code behind rather than the HTML page
//
//   Rev 1.20.1.0   Nov 29 2005 14:47:26   RGriffith
//New Header Control for HomePage phase2
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.20   Jun 01 2005 14:09:16   pcross
//Alt text for banner image on header controls
//Resolution for 2512: DEL 7 - WAI changes - following Bobby review
//
//   Rev 1.19   May 26 2005 09:24:04   pcross
//Mobile tab didn't show Welsh text on Related Sites page
//Resolution for 2526: DEL 7 Updated Soft Content Missing
//
//   Rev 1.18   May 20 2005 11:49:52   ralavi
//Ensuring that tab section will be in Map mode only when map tab is selected.
//
//   Rev 1.17   Apr 15 2005 12:47:50   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.16   Mar 29 2005 13:28:44   rgeraghty
//Switched order of live travel and TD on the move tabs
//
//   Rev 1.15   Mar 17 2005 17:14:50   rscott
//Added TDOnTheMove Tab
//
//   Rev 1.14   Aug 19 2004 12:50:44   COwczarek
//Use GetHeaderTransitionEventFromMode to redirect to appropriate Find A input page
//Resolution for 1345: Clicking Find A tab should display page for current Find A mode
//
//   Rev 1.13   Aug 06 2004 14:45:04   JHaydock
//Added TabSelection methods to TDSessionManager which is updated by each individual header control's load method and used within the HelpFullJP page to display the correct header on help pages.
//
//   Rev 1.12   May 25 2004 10:04:42   jgeorge
//Added Find A... tab
//
//   Rev 1.11   Mar 10 2004 10:06:52   COwczarek
//default action button event handler now overrides base class implementation
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.10   Mar 08 2004 17:24:32   COwczarek
//Event handlers for tab buttons now clear the back button return stack rather than fire a HeaderClick event (which has been removed)
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.9   Nov 26 2003 12:36:12   COwczarek
//SCR#404: Image in header controls has invalid URL
//Resolution for 404: Image in header controls has invalid URL
//
//   Rev 1.8   Nov 24 2003 15:11:40   asinclair
//Added Alt tags and created link to homepage
//
//   Rev 1.7   Nov 18 2003 09:48:34   asinclair
//Commented code
//
//   Rev 1.6   Nov 12 2003 19:40:10   PNorell
//Corrected bug with home-page link.
//Added default action button and changed inheritance.
//
//   Rev 1.5   Sep 30 2003 20:10:18   asinclair
//Added code to Page load to switch div between English and Welsh
//
//   Rev 1.4   Sep 23 2003 19:29:30   asinclair
//Updated Top Nav Buttons for screen flow stuff
//
//   Rev 1.3   Sep 04 2003 13:57:52   asinclair
//Changed some images to Image Buttons
//
//   Rev 1.2   Jul 15 2003 16:52:04   asinclair
//Updated Control
namespace TransportDirect.UserPortal.Web.Controls
{
	using System;	
	using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.Web.Support;
    using TransportDirect.UserPortal.Web.Adapters;
    using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.Common;
	using TransportDirect.UserPortal.ScreenFlow;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.Resource;
    using TransportDirect.Common.PropertyService.Properties;
    using System.Text;

	/// <summary>
	///		Summary description for HeaderControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial  class HeaderControl : TDUserControl
	{		
		//Private members for properties
		private string tdpLogoURL;
		private bool useTDPLogoURL;
        private bool showNavigation = true;
        
		// TabSelectionManager
		TabSelectionManager tabSelectionManager;

		/// <summary>
		/// The page load checks the current channel and sets the English/Welsh html 
		/// div id of the tab buttons, as the Welsh tabs are larger than the English tabs
		/// therefore the div needs to switch depending on the channel.
		/// Page load also sets up the tab images and determines if the javascript disabled link is disabled.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Set up TabSelectionManager to control tabs
			tabSelectionManager = new TabSelectionManager();

			//this literal value is the start of the div tag we want to set at runtime
			TabsStartLiteral.Text = MyHeaderLang;

			// Set up the tab images and alternateText properties
			SetTabImages();

			// Set the javascript hyperlink up
			SetJavascriptHyperLink();

            // Set the powered by mode
            PoweredByControl.PoweredByControlMode = PoweredByControl.PoweredByMode.LogoHeader;

		}
        
        /// <summary>
        /// Page pre render event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Show navigation tabs bar
            panelNavigation.Visible = showNavigation;

            // Show/hide navigation tabs in the navigation tab bar
            navigationTabs.Visible = showNavigation && NavigationTabVisibile();

            //Enable td logo at top as link to home page
            bool tdLogoAsLink = true;
            if (!bool.TryParse(Properties.Current["HeaderControl.headerHomepageLink.Enabled"], out tdLogoAsLink))
            {
                tdLogoAsLink = true;
            }
            defaultActionButton.Enabled = tdLogoAsLink;
            headerHomepageLink.Enabled = tdLogoAsLink;

            if (navigationTabs.Visible)
            {
                CheckAndSetupIEMenu();
            }

            if (tdLogoAsLink)
            {
                CheckAndSetupHomePageLogo();
            }

            AddFocusHack();
        }


		#region Properties

		/// <summary>
		///Write property to specify the redirect url for the TD Logo.
		/// </summary>
		public string TDLogoURL
		{
			set
			{
				tdpLogoURL = value;
			}
		}

		/// <summary>
		/// Write property to specify if the TDLogoURL property should be used.
		/// </summary>
		public bool UseTDPLogoURL
		{
			set
			{
				useTDPLogoURL = value;
			}
		}

        /// <summary>
		/// Readonly property that contains the link to the home page for each individual language
		/// </summary>
		private string HomePageUrl
		{
			get
			{
				if (TDPage.SessionChannelName !=  null )
				{
					return TDPage.getBaseChannelURL(TDPage.SessionChannelName); 
				}
				
				return string.Empty;
			}
		}

		/// <summary>
		/// Readonly property to determine the language used on the page
		/// English by default
		/// </summary>
		private string MyHeaderLang
		{
			get
			{
				if(TDPage.SessionChannelName != null )
				{
					//gets the current language channel
					string strChn = TDPage.SessionChannelName.ToString();
					string language = TDPage.GetChannelLanguage(strChn);
				
					//If Welsh, return the welsh div, else return English div
					if(language == "cy-GB")
					{ 
						return "<div id=\"menubuttonsw\">";
					}
					else
					{
						return "<div id=\"menubuttons2\">";
					}
				}
				return "<div id=\"menubuttons2\">";
			}
		}


        /// <summary>
        /// Read/write property to show the Navigation tabs bar
        /// </summary>
        public bool ShowNavigation
        {
            get { return showNavigation; }
            set { showNavigation = value; }
        }

        #endregion

		#region Private methods
        /// <summary>
        /// Adds css hack for IE7 use of focus
        /// </summary>
        private void AddFocusHack()
        {
            // For each of the nav buttons add a border when focussed
            homeImageButton.Attributes["onfocus"] = "this.className='HackFocusTDNavButton'";
            homeImageButton.Attributes["onblur"] = "this.className='TDNavButton'";

            planAJourneyImageButton.Attributes["onfocus"] = "this.className='HackFocusTDNavButton'";
            planAJourneyImageButton.Attributes["onblur"] = "this.className='TDNavButton'";

            findAImageButton.Attributes["onfocus"] = "this.className='HackFocusTDNavButton'";
            findAImageButton.Attributes["onblur"] = "this.className='TDNavButton'";

            travelInfoImageButton.Attributes["onfocus"] = "this.className='HackFocusTDNavButton'";
            travelInfoImageButton.Attributes["onblur"] = "this.className='TDNavButton'";

            tipsAndToolsImageButton.Attributes["onfocus"] = "this.className='HackFocusTDNavButton'";
            tipsAndToolsImageButton.Attributes["onblur"] = "this.className='TDNavButton'";

            loginAndRegisterImageButton.Attributes["onfocus"] = "this.className='HackFocusTDNavButton'";
            loginAndRegisterImageButton.Attributes["onblur"] = "this.className='TDNavButton'";

            // Include active nav styling where appropriate
            switch (TDSessionManager.Current.TabSection)
            {
                case TabSection.Home:
                    homeImageButton.Attributes["onfocus"] = "this.className='HackFocusTDNavButton TDNavButtonActive'";
                    homeImageButton.Attributes["onblur"] = "this.className='TDNavButton TDNavButtonActive'";
                    break;
                case TabSection.PlanAJourney:
                    planAJourneyImageButton.Attributes["onfocus"] = "this.className='HackFocusTDNavButton TDNavButtonActive'";
                    planAJourneyImageButton.Attributes["onblur"] = "this.className='TDNavButton TDNavButtonActive'";
                    break;
                case TabSection.FindAPlace:
                    findAImageButton.Attributes["onfocus"] = "this.className='HackFocusTDNavButton TDNavButtonActive'";
                    findAImageButton.Attributes["onblur"] = "this.className='TDNavButton TDNavButtonActive'";
                    break;
                case TabSection.TravelInfo:
                    travelInfoImageButton.Attributes["onfocus"] = "this.className='HackFocusTDNavButton TDNavButtonActive'";
                    travelInfoImageButton.Attributes["onblur"] = "this.className='TDNavButton TDNavButtonActive'";
                    break;
                case TabSection.TipsAndTools:
                    tipsAndToolsImageButton.Attributes["onfocus"] = "this.className='HackFocusTDNavButton TDNavButtonActive'";
                    tipsAndToolsImageButton.Attributes["onblur"] = "this.className='TDNavButton TDNavButtonActive'";
                    break;
                case TabSection.LoginRegister:
                    loginAndRegisterImageButton.Attributes["onfocus"] = "this.className='HackFocusTDNavButton TDNavButtonActive'";
                    loginAndRegisterImageButton.Attributes["onblur"] = "this.className='TDNavButton TDNavButtonActive'";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
		/// SetTabImages
		/// </summary>
        private void SetTabImages()
        {
            SetTabVisibility();

            // Set Non-tab header image Urls/alternate text
            //defaultActionButton.ImageUrl = GetResource("HeaderControl.defaultActionButton.ImageUrl");
            //defaultActionButton.AlternateText = GetResource("HeaderControl.defaultActionButton.AlternateText");
            //headerHomepageLink.ImageUrl = GetResource("HeaderControl.headerHomepageLink.ImageUrl");
            //headerHomepageLink.AlternateText = GetResource("HeaderControl.headerHomepageLink.AlternateText");
            TDSmallBannerImage.ImageUrl = GetResource("HeaderControl.TDSmallBannerImage.Src");
            TDSmallBannerImage.AlternateText = " ";
            TDSmallBannerImage.ToolTip = GetResource("HeaderControl.TDSmallBannerImage.AlternateText");
                        
            //changed to new images after white labelling change
            // Set all tabs to unselected images
            homeImageLinkText.Text = homeImageButton.Text = GetResource("HeaderControl.homeImageButton.AlternateText");
            planAJourneyImageLinkText.Text = planAJourneyImageButton.Text = GetResource("HeaderControl.planAJourneyImageButton.AlternateText");
            findAImageLinkText.Text = findAImageButton.Text = GetResource("HeaderControl.findAImageButton.AlternateText");
            travelInfoImageLinkText.Text = travelInfoImageButton.Text = GetResource("HeaderControl.travelInfoImageButton.AlternateText");
            tipsAndToolsImageLinkText.Text = tipsAndToolsImageButton.Text = GetResource("HeaderControl.tipsAndToolsImageButton.AlternateText");
            if (TDSessionManager.Current.Authenticated)
            {
                loginAndRegisterImageLinkText.Text = loginAndRegisterImageButton.Text = GetResource("HeaderControl.Web2.logoutAndUpdateImageButton.AlternateText");
            }
            else
            {
                loginAndRegisterImageLinkText.Text = loginAndRegisterImageButton.Text = GetResource("HeaderControl.Web2.loginAndRegisterImageButton.AlternateText");
            }

            if (!loginAndRegisterImageButton.Visible)
                tipsAndToolsImageButton.CssClass += string.Format(" {0}", " TDNavButtonLast");

            //changed to new images after white labelling change
            // Then set the selected tab to the appropriate selectedTab image 
            // & set star tags visible if Homepage or TravelInfo page
            switch (TDSessionManager.Current.TabSection)
            {
                case TabSection.Home:
                    homeImageButton.Text = GetResource("HeaderControl.homeImageButton.AlternateText");
                    homeImageButton.CssClass += string.Format(" {0}", "TDNavButtonActive");
                    homeImageButton.CssClassMouseOver += string.Format(" {0}", "TDNavButtonActive");

                    homeImageLink.CssClass += string.Format(" {0}", "TDNavButtonActive");
                    break;
                case TabSection.PlanAJourney:
                    planAJourneyImageButton.Text = GetResource("HeaderControl.planAJourneyImageButton.AlternateText");
                    planAJourneyImageButton.CssClass += string.Format(" {0}", "TDNavButtonActive");
                    planAJourneyImageButton.CssClassMouseOver += string.Format(" {0}", "TDNavButtonActive");

                    planAJourneyImageLink.CssClass += string.Format(" {0}", "TDNavButtonActive");
                    break;
                case TabSection.FindAPlace:
                    findAImageButton.Text = GetResource("HeaderControl.findAImageButton.AlternateText");
                    findAImageButton.CssClass += string.Format(" {0}", "TDNavButtonActive");
                    findAImageButton.CssClassMouseOver += string.Format(" {0}", "TDNavButtonActive");

                    findAImageLink.CssClass += string.Format(" {0}", "TDNavButtonActive");
                    break;
                case TabSection.TravelInfo:
                    travelInfoImageButton.Text = GetResource("HeaderControl.travelInfoImageButton.AlternateText");
                    travelInfoImageButton.CssClass += string.Format(" {0}", "TDNavButtonActive");
                    travelInfoImageButton.CssClassMouseOver += string.Format(" {0}", "TDNavButtonActive");

                    travelInfoImageLink.CssClass += string.Format(" {0}", "TDNavButtonActive");
                    break;
                case TabSection.TipsAndTools:
                    tipsAndToolsImageButton.Text = GetResource("HeaderControl.tipsAndToolsImageButton.AlternateText");
                    tipsAndToolsImageButton.CssClass += string.Format(" {0}", "TDNavButtonActive");
                    tipsAndToolsImageButton.CssClassMouseOver += string.Format(" {0}", "TDNavButtonActive");

                    tipsAndToolsImageLink.CssClass += string.Format(" {0}", "TDNavButtonActive");
                    break;
                case TabSection.LoginRegister:
                    if (TDSessionManager.Current.Authenticated)
                    {
                        loginAndRegisterImageButton.Text = GetResource("HeaderControl.Web2.logoutAndUpdateSelectedImageButton.AlternateText");
                    }
                    else
                    {
                        loginAndRegisterImageButton.Text = GetResource("HeaderControl.Web2.loginAndRegisterSelectedImageButton.AlternateText");
                    }
                    loginAndRegisterImageButton.CssClass += string.Format(" {0}", "TDNavButtonActive");
                    loginAndRegisterImageButton.CssClassMouseOver += string.Format(" {0}", "TDNavButtonActive");

                    loginAndRegisterImageLink.CssClass += string.Format(" {0}", "TDNavButtonActive");
                    break;
                default:
                    break;
            }

            // Initialise alternate text attribute
            if (TDSessionManager.Current.Authenticated)
            {
                loginAndRegisterImageLinkText.Text = loginAndRegisterImageButton.Text = GetResource("HeaderControl.logoutAndUpdateImageButton.AlternateText");
            }
            else
            {
                loginAndRegisterImageLinkText.Text = loginAndRegisterImageButton.Text = GetResource("HeaderControl.loginAndRegisterImageButton.AlternateText");
            }

            SetToolTips();
        }

        /// <summary>
        /// Sets tool tips
        /// </summary>
        private void SetToolTips()
        {
            // Initialise tool tip attribute
            homeImageButton.ToolTip = homeImageButton.Text;
            planAJourneyImageButton.ToolTip = planAJourneyImageButton.Text;
            findAImageButton.ToolTip = findAImageButton.Text;
            travelInfoImageButton.ToolTip = travelInfoImageButton.Text;
            tipsAndToolsImageButton.ToolTip = tipsAndToolsImageButton.Text;
            loginAndRegisterImageButton.ToolTip = loginAndRegisterImageButton.Text;

            // Initialise tool tip attribute
            homeImageLink.ToolTip = homeImageButton.Text;
            planAJourneyImageLink.ToolTip = planAJourneyImageButton.Text;
            findAImageLink.ToolTip = findAImageButton.Text;
            travelInfoImageLink.ToolTip = travelInfoImageButton.Text;
            tipsAndToolsImageLink.ToolTip = tipsAndToolsImageButton.Text;
            loginAndRegisterImageLink.ToolTip = loginAndRegisterImageButton.Text;
        }

        /// <summary>
        /// Sets visibility of tabs
        /// </summary>
        private void SetTabVisibility()
        {
            // boolean to determine if navigation is enabled on the page
            bool boolNavigationEnabled;

            // Set Tab section appropriately using TabSelectionManager if navigation is enabled
            if (tabSelectionManager.NavigationEnabledForPage(this.PageId))
            {
                TDSessionManager.Current.TabSection =
                    tabSelectionManager.SelectedTab(TDSessionManager.Current.TabSection, this.PageId);

                boolNavigationEnabled = true;
            }
            else
            {
                boolNavigationEnabled = false;
            }

            // Set tab images visibility - (disabled for pages where navigation is not enabled)
            homeImageButton.Visible = homeImageLink.Visible = boolNavigationEnabled;
            planAJourneyImageButton.Visible = planAJourneyImageLink.Visible = boolNavigationEnabled;
            findAImageButton.Visible = findAImageLink.Visible = boolNavigationEnabled;
            travelInfoImageButton.Visible = travelInfoImageLink.Visible = boolNavigationEnabled;
            tipsAndToolsImageButton.Visible = tipsAndToolsImageLink.Visible = boolNavigationEnabled;
            loginAndRegisterImageButton.Visible = loginAndRegisterImageLink.Visible = boolNavigationEnabled;

            // set tab images visibility as required by white label partners
            homeImageButton.Visible = homeImageLink.Visible = FindInputAdapter.HomeImageButtonAvailable;
            planAJourneyImageButton.Visible = planAJourneyImageLink.Visible = FindInputAdapter.PlanAJourneyImageButtonAvailable;
            findAImageButton.Visible = findAImageLink.Visible = FindInputAdapter.FindAPlaceImageButtonAvailable;
            travelInfoImageButton.Visible = travelInfoImageLink.Visible = FindInputAdapter.LiveTravelImageButtonAvailable;
            tipsAndToolsImageButton.Visible = tipsAndToolsImageLink.Visible = FindInputAdapter.TipsAndToolsImageButtonAvailable;
            loginAndRegisterImageButton.Visible = loginAndRegisterImageLink.Visible = FindInputAdapter.LoginRegisterImageButtonAvailable;
        }

        /// <summary>
        /// Following method enables the ie specific tab to be visible in case 
        /// browser is IE and its major version is less than 8.
        /// The reason behind this is to resolve the issue raised by accessibility audit 
        /// about the tab key indicator not visible when user tabs on the top horizontal tab menu.
        /// Sadly this is due to bug in IE7 which doesn't render tab key indicators when the button
        /// doesn't have the borders. We removed the borders of the top buttons to look more like menu.
        /// 
        /// The solution to this defect is implemented below by having link buttons when the browser is less than IE8 
        /// and javascript enabled. When Javascript disabled this solution want work so in such case following solution 
        /// renders border around the buttons which degrades the visible look a bit.
        /// </summary>
        private void CheckAndSetupIEMenu()
        {
            // Make default tab visible by default
            defaultTab.Visible = true;
            ieTab.Visible = false;

            //Property to check if specific IE tab should display

            bool ieTabVisibleCheck = false; // make it visible false by default

            if (!bool.TryParse(Properties.Current["HeaderControl.CheckBrowser.IE7.TabVisible"], out ieTabVisibleCheck))
            {
                ieTabVisibleCheck = false; // unable to parse the property, fallback to default tab
            }

            if (ieTabVisibleCheck)
            {
                // If browser is IE and major version is less than 8
                if (Request.Browser.Browser.Trim() == "IE" && Request.Browser.MajorVersion < 8)
                {
                    TDPage tdPage = (TDPage)this.Page;

                    // tabs
                    defaultTab.Visible = true;
                    ieTab.Visible = true;


                    if (!IsPostBack)
                    {
                        // when the page loads for first time
                        // by default make default tab to be visible and hide the ieTab using css display attribute
                        defaultTab.Style["display"] = "block";
                        ieTab.Style["display"] = "none";

                        // apply the borders to buttons for non javascript page
                        homeImageButton.CssClass += " TDNavButtonBorder";
                        planAJourneyImageButton.CssClass += " TDNavButtonBorder";
                        findAImageButton.CssClass += " TDNavButtonBorder";
                        travelInfoImageButton.CssClass += " TDNavButtonBorder";
                        tipsAndToolsImageButton.CssClass += " TDNavButtonBorder";
                        loginAndRegisterImageButton.CssClass += " TDNavButtonBorder";

                        StringBuilder ieScript = new StringBuilder();

                        // Build a javascript to run on page load 
                        // if javascript enabled this will make ie specific tab to be visible and hide the default tab.
                        // if javascript is disabled this script will not run making the default tab buttons with border to show
                        ieScript.AppendFormat("{0}.style.display='block'; {1}.style.display='none';", ieTab.ClientID, defaultTab.ClientID);


                        Page.ClientScript.RegisterStartupScript(this.GetType(), "IEScript", ieScript.ToString(), true);
                    }
                    else
                    {
                        // following postbacks
                        if (tdPage.IsJavascriptEnabled)
                        {
                            //if javascript enabled show the ie tab 
                            ieTab.Style["display"] = "block";
                            ieTab.Visible = true;
                            defaultTab.Visible = false;
                        }
                        else
                        {
                            // javascript is disabled so show the tab buttons with border
                            homeImageButton.CssClass += " TDNavButtonBorder";
                            planAJourneyImageButton.CssClass += " TDNavButtonBorder";
                            findAImageButton.CssClass += " TDNavButtonBorder";
                            travelInfoImageButton.CssClass += " TDNavButtonBorder";
                            tipsAndToolsImageButton.CssClass += " TDNavButtonBorder";
                            loginAndRegisterImageButton.CssClass += " TDNavButtonBorder";
                        }
                    }


                }
            }
        }

        /// <summary>
        /// Check and setup home page logo as hyper link in case the user browser is IE and major version is 7 or below
        /// </summary>
        private void CheckAndSetupHomePageLogo()
        {
            // Make default tab visible by default
            headerHomepageLink.Visible = true; // the button
            headerLink.Visible = false; // the hyperlink

            headerLink.NavigateUrl = GetResource("HeaderControl.headerLink.Url");

            //Property to check if specific IE logo hyperlink should display

            bool ieLogoVisibleCheck = false; // make it visible false by default

            if (!bool.TryParse(Properties.Current["HeaderControl.Homepage.IE7.LogoVisible"], out ieLogoVisibleCheck))
            {
                ieLogoVisibleCheck = false; // unable to parse the property, fallback to default td button logo
            }

            if (ieLogoVisibleCheck)
            {
                // If browser is IE and major version is less than 8
                if (Request.Browser.Browser.Trim() == "IE" && Request.Browser.MajorVersion < 8)
                {
                    TDPage tdPage = (TDPage)this.Page;

                    // tabs
                    headerHomepageLink.Visible = false; // the button
                    headerLink.Visible = true; // the link button
                }
            }
        }

		/// <summary>
		/// Method to set properties of the javascript hyperlink which can be used 
		/// (when javascript setting is not known) to reload the current page with javascript disabled.
		/// </summary>
		private void SetJavascriptHyperLink()
		{
			TDPage page = (TDPage)this.Page;
			if ((page.IsJavaScriptSettingKnown == false) && (tabSelectionManager.LoginControlsVisibleForPage(this.PageId)))
			{
				// Make the javascript hyperlink visible if setting is not detected yet
				javascriptUnknownPlaceholder.Visible = true;
				
				// Get text for the javascript hyperlink value
				noJavaScriptHyperlink.Text = GetResource("HeaderControl.noJavaScriptHyperlink.Text");

				// Set up page controller
				IPageController pageController =
					(PageController)TDServiceDiscovery.Current
					[ServiceDiscoveryKey.PageController];

				// Set up page transfer details to be able to determine the current page URL
				PageTransferDetails pageTransferDetails =
					pageController.GetPageTransferDetails(this.PageId);

				// Perform the response redirect
				string url = HomePageUrl + pageTransferDetails.PageUrl;

				// Add the hdnSet tag to disable javascript
				if (url.IndexOf("?") > 0)
				{
					url += "&" + "hdnSet=false";
				}
				else 
				{
					url += "?" + "hdnSet=false";
				}

				// Set up navigation hyperlink for non javascript users
				noJavaScriptHyperlink.NavigateUrl = url;
			} 
			else
			{
				// Hide the javascript hyperlink if setting is detected yet
				javascriptUnknownPlaceholder.Visible = false;
			}
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Method called for all tab and logo navigation, from the approriate event handlers.
		/// Clears appropriate session data.
		/// </summary>
		private void ClearCache()
		{
			//Create a new clear cache helper and call method on it.
			ClearCacheHelper helper = new ClearCacheHelper();
			helper.ClearCache();
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// Top row image buttons
			//this.defaultActionButton.Click += new System.Web.UI.ImageClickEventHandler(this.defaultActionButton_Click);
            this.defaultActionButton.Click += new EventHandler(this.defaultActionButton_Click);
			this.headerHomepageLink.Click += new EventHandler(this.homeImageButton_Click);
			
			// Tab image buttons
            this.homeImageButton.Click += new EventHandler(this.homeImageButton_Click);
            this.planAJourneyImageButton.Click += new EventHandler(this.planAJourneyImageButton_Click);
            this.findAImageButton.Click += new EventHandler(this.findAImageButton_Click);
            this.travelInfoImageButton.Click += new EventHandler(this.travelInfoImageButton_Click);
            this.tipsAndToolsImageButton.Click += new EventHandler(this.tipsAndToolsImageButton_Click);
            this.loginAndRegisterImageButton.Click += new EventHandler(this.loginAndRegisterImageButton_Click);

            // Tab image links
            this.homeImageLink.Click += new EventHandler(this.homeImageButton_Click);
            this.planAJourneyImageLink.Click += new EventHandler(this.planAJourneyImageButton_Click);
            this.findAImageLink.Click += new EventHandler(this.findAImageButton_Click);
            this.travelInfoImageLink.Click += new EventHandler(this.travelInfoImageButton_Click);
            this.tipsAndToolsImageLink.Click += new EventHandler(this.tipsAndToolsImageButton_Click);
            this.loginAndRegisterImageLink.Click += new EventHandler(this.loginAndRegisterImageButton_Click);
		}
		#endregion

		#region Click Event Handlers
		/// <summary>
		/// The default action event
		/// </summary>
		public event EventHandler DefaultActionEvent;
        
		/// <summary>
		/// Handles the "default action" click for the page. Overrides
		/// base class implementation to also clear the navigation
		/// return stack.
		/// </summary>
		/// <param name="sender">event originator</param>
		/// <param name="e">event parameters</param>
		/*protected void defaultActionButton_Click(object sender, System.Web.UI.ImageClickEventArgs e) 
		{
			// Default action -> propogate to any other control interested in an event
			if( DefaultActionEvent != null )
			{
				DefaultActionEvent( sender , e);
			}
		}*/

        /// <summary>
        /// Handles the "default action" click for the page. Overrides
        /// base class implementation to also clear the navigation
        /// return stack.
        /// </summary>
        /// <param name="sender">event originator</param>
        /// <param name="e">event parameters</param>
        protected void defaultActionButton_Click(object sender, EventArgs e)
        {
            // Default action -> propogate to any other control interested in an event
            if (DefaultActionEvent != null)
            {
                DefaultActionEvent(sender, e);
            }
        }

		/// <summary>
		/// Event handler to handle clicks on the Home tab
		/// </summary>
		/// <param name="sender">event originator</param>
		/// <param name="e">event parameters</param>
		private void homeImageButton_Click(object sender, EventArgs e)
		{
			// Set up session manager
			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			//IR3851 - Enhancement IR: To allow the Error and Timeout pages 
			//to specify the redirect url
			if (useTDPLogoURL && tdpLogoURL != null)
			{
				Response.Redirect(tdpLogoURL);
			}
			else
			{
				// Navigate to the homepage
				sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoHome;
			}
	
			// Clear the appropriate session variables to reset pages (and search results) to consistent state
			ClearCache();
		}

		/// <summary>
		/// Event handler to handle clicks on the Plan a journey tab
		/// </summary>
		/// <param name="sender">event originator</param>
		/// <param name="e">event parameters</param>
		private void planAJourneyImageButton_Click(object sender, EventArgs e)
		{
			// Set up session manager
			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			// Navingate to new Mini Homepage: Plan a Journey
			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.PlanAJourneyTab;

			// Clear the appropriate session variables to reset pages (and search results) to consistent state
			ClearCache();
		}

		/// <summary>
		/// Event handler to handle clicks on the Find a place tab
		/// </summary>
		/// <param name="sender">event originator</param>
		/// <param name="e">event parameters</param>
		private void findAImageButton_Click(object sender, EventArgs e)
		{
			// Set up session manager
			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			// Navingate to new Mini Homepage: Find A...
			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindAPlaceTab;

			// Clear the appropriate session variables to reset pages (and search results) to consistent state
			ClearCache();
		}

		/// <summary>
		/// Event handler to handle clicks on the Live travel tab
		/// </summary>
		/// <param name="sender">event originator</param>
		/// <param name="e">event parameters</param>
		private void travelInfoImageButton_Click(object sender, EventArgs e)
		{
			// Set up session manager
			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			// Navingate to new Mini Homepage: Travel Info
			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.TravelInfoTab;

			// Clear the appropriate session variables to reset pages (and search results) to consistent state
			ClearCache();
		}

		/// <summary>
		/// Event handler to handle clicks on the Tips and tools tab
		/// </summary>
		/// <param name="sender">event originator</param>
		/// <param name="e">event parameters</param>
		private void tipsAndToolsImageButton_Click(object sender, EventArgs e)
		{
			// Set up session manager
			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			// Navingate to new Mini Homepage: Tips and Tools
			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.TipsToolsTab;
				
			// Clear the appropriate session variables to reset pages (and search results) to consistent state
			ClearCache();
		}

        /// <summary>
        /// Event handler to handle clicks on the Login/Register tab
        /// </summary>
        /// <param name="sender">event originator</param>
        /// <param name="e">event parameters</param>
        private void loginAndRegisterImageButton_Click(object sender, EventArgs e)
        {
            // Set up session manager
            ITDSessionManager sessionManager =
                (ITDSessionManager)TDServiceDiscovery.Current
                [ServiceDiscoveryKey.SessionManager];
           
            // Navigate to LoginAndRegister Page
            sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.LoginRegister;

            //Removed the ClearCache method as new back button functionality added to loginregister page.

            // Save the querystring if we've come from a help page (as we need to know which help page to go 
            // back to.)
            if (((TDPage)Page).PageId == PageId.HelpFullJP)
            {
                TDSessionManager.Current.InputPageState.JourneyInputQueryString = Request.QueryString.ToString();
            }
            else
            {
                TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
            }

            TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(((TDPage)Page).PageId);

        }

        #region Navigation Tab in header control
        /// <summary>
        /// Sets visiblity of the navigation tabs showing in header control on page
        /// </summary>
        private bool NavigationTabVisibile()
        {
            bool navigationTabVisible = true;
            TDPage tdPage = (TDPage)Page;
            string headerTabProperty = string.Format("{0}.HeaderTab.Visible", tdPage.PageId);
            if (!bool.TryParse(Properties.Current[headerTabProperty], out navigationTabVisible))
            {
                navigationTabVisible = true;
            }

            return navigationTabVisible;

        }
        #endregion

		#endregion
    }
}
