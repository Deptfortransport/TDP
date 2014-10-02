// *********************************************** 
// NAME                 : FooterControl.aspx.cs 
// AUTHOR               : Andrew Sinclair 
// DATE CREATED         : 15/07/2003 
// DESCRIPTION			: A custom user control to 
// display the footer graphics and links for each page.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FooterControl.ascx.cs-arc  $
//
//   Rev 1.5   Oct 24 2011 10:46:58   mmodi
//Updated to display travel news toids for CJP user
//Resolution for 5758: Real Time in Car - Display TOIDs on incident popup for CJP user
//
//   Rev 1.4   Oct 27 2010 15:26:56   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.3   Mar 30 2010 09:13:50   apatel
//Cycle planner white label changes
//Resolution for 5488: Cycle Planner white label changes
//
//   Rev 1.2   Mar 31 2008 13:20:56   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:14:38   mturner
//Initial revision.
//
//  devfactory Feb 01 2008 sbarker
//Edited to allow language to be switched via clicking.
//
//   Rev 1.44   Jan 14 2007 17:22:48   mmodi
//Added FeedbackViewer
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.43   Apr 19 2006 17:13:48   asinclair
//Removal of Inspirational Places link
//Resolution for 3942: DEL 8.1 CHANGE: Remove 'Inspirational Places' link from footer
//
//   Rev 1.42   Apr 12 2006 16:55:50   mtillett
//Update footer to set Text for hyperlinks explicitly, using langstring file.
//Resolution for 3615: Homepage Phase 2: Display of footer links on Win2000/Netscape7.2
//
//   Rev 1.41   Mar 22 2006 13:10:42   AViitanen
//Undid previous change. 
//
//   Rev 1.40   Mar 16 2006 15:54:00   AViitanen
//Added 'v' to version label. 
//Resolution for 3620: Homepage phase 2: Bottom align version number
//
//   Rev 1.39   Feb 23 2006 19:16:44   build
//Automatically merged from branch for stream3129
//
//   Rev 1.38.1.0   Jan 10 2006 15:25:04   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.38   Nov 03 2005 17:01:32   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.37.1.0   Oct 20 2005 17:09:02   rgreenwood
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.37   Sep 29 2005 12:47:38   build
//Automatically merged from branch for stream2673
//
//   Rev 1.36.1.0   Sep 23 2005 10:03:58   rgreenwood
//UEE Removed "Top of Page" link.
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.36   May 24 2005 11:42:50   pcross
//Made footer independent of LanguageHandler class.
//Resolution for 2367: DEL 7 Welsh text missing
//
//   Rev 1.35   May 04 2005 09:04:58   jgeorge
//Removed CJP switching functionality
//
//   Rev 1.34   Apr 12 2005 14:28:04   jgeorge
//Hide Log Viewer and Version Viewer buttons for the retailer handoff page, as they cause an error due to page structure.
//
//   Rev 1.33   Apr 11 2005 16:46:50   jgeorge
//Re-wired events for log and version viewer buttons
//
//   Rev 1.32   Apr 08 2005 14:28:06   jgeorge
//Correction to hide the temporary switching buttons when on the retailer handoff page
//
//   Rev 1.31   Apr 06 2005 17:27:18   jgeorge
//Added buttons to switch between real and stub Cjp
//
//   Rev 1.30   Oct 20 2004 10:19:26   jbroome
//Added Inspirational Places link to footer
//
//   Rev 1.29   Aug 16 2004 15:23:52   asinclair
//Fix for IR 1180.  Top of Page link no longer server control
//
//   Rev 1.28   Aug 02 2004 15:50:38   AWindley
//Readded ClickEventHandlers lost in rev 1.27
//
//   Rev 1.27   Jul 28 2004 10:06:18   asinclair
//Fix for IR1177
//
//   Rev 1.26   Jul 22 2004 14:13:22   jbroome
//IR 1243. Removal of extra vertical space at bottom of footer.
//
//   Rev 1.25   Jul 06 2004 09:47:54   AWindley
//Interim update for CCN098: CJP Logging
//
//   Rev 1.24   Jun 30 2004 15:49:22   CHosegood
//Now displays language links in their own language.
//Resolution for 1057: Identify Natural language of the document
//
//   Rev 1.23   Apr 28 2004 16:20:04   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.22   Mar 11 2004 17:10:40   asinclair
//Added ToolTip for Top of Page link
//
//   Rev 1.21   Feb 16 2004 11:58:28   asinclair
//Updated after running FXCop
//
//   Rev 1.20   Feb 16 2004 11:36:28   asinclair
//Del 5.2 - Added version number
//
//   Rev 1.19   Nov 27 2003 10:52:10   passuied
//changed footer control link to #logoTop
//Resolution for 293: Javascript disabled browser have trouble with the soft-content
//
//   Rev 1.18   Nov 14 2003 13:40:02   asinclair
//Added code to set the Href value of the top of page link at runtime
//
//   Rev 1.17   Nov 06 2003 10:24:18   asinclair
//Fixed an error, some links were pointing to the wrong page URL in the page load event
//
//   Rev 1.16   Nov 05 2003 09:34:54   asinclair
//Updated Page load to fix incident id 48
//
//   Rev 1.15   Oct 08 2003 12:53:50   JMorrissey
//added check for when TDPage.SessionChannelName is null
//
//   Rev 1.14   Oct 03 2003 11:48:32   JMorrissey
//Removed postingID temp string
//
//   Rev 1.13   Oct 03 2003 11:30:24   JMorrissey
//Changed Language Link button to be a hyperlink
//
//   Rev 1.12   Oct 01 2003 15:32:34   asinclair
//Added link for Data Providers
//
//   Rev 1.11   Sep 27 2003 15:49:08   asinclair
//Added links for TCs and Privacy Policy
//
//   Rev 1.10   Sep 27 2003 13:30:14   JMorrissey
//Removed use of hard coded strings where possible
//
//   Rev 1.9   Sep 26 2003 11:38:22   JMorrissey
//Linked up click event for LanguageLink button
//
//   Rev 1.8   Sep 16 2003 16:49:38   asinclair
//Names ofthe linkbuttons needed updating. 
//
//   Rev 1.7   Sep 16 2003 16:38:10   asinclair
//Added TC and Privacy as Hyperlinks and applied newstylesheet.  Still work TODO on this control
//
//   Rev 1.6   Sep 04 2003 10:40:32   JMorrissey
//Added code to LanguageLinkButton_Click to enable switching between English and Welsh pages. Updated page navigation hyperlinks so that their values are not hard coded but are instead retrieved fromn a resource file at run time. 
//
//   Rev 1.5   Sep 01 2003 11:05:26   asinclair
//Changed links from HTML hyperlinks to Web Form Link Button controls 
//
//   Rev 1.4   Aug 15 2003 14:31:14   asinclair
//Changed design of control.
//
//   Rev 1.3   Aug 04 2003 17:16:54   asinclair
//Added draft hyperlinks
//
//   Rev 1.2   Jul 15 2003 16:52:30   asinclair
//Updated Control
namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
    using System.Collections;
    using System.Data;
	using System.Drawing;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;	
	using System.Web.UI.HtmlControls;


	
	using TransportDirect.Common;
    using TransportDirect.Common.PropertyService.Properties;
    using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.LocationService;
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.UserPortal.SessionManager;
    using TransportDirect.UserPortal.Web;
    using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.Web.Support;
    using CmsMockObject.Objects;
    using TransportDirect.Common.DatabaseInfrastructure.Content;
    using System.Text;
    using TransportDirect.UserPortal.Web.Code;
    using TransportDirect.UserPortal.Web.Adapters;
    using TransportDirect.UserPortal.ScreenFlow;
    using TransportDirect.Common.Logging;
    using Logger = System.Diagnostics.Trace;

	/// <summary>
	///	A custom user control to display the footer graphics and links for each page.
	/// </summary>
	/// <remarks>
	/// ILanguageHandlerIndependent marker added so that the control doesn't get included
	/// in the recursive calling of SetTextOnControls function (see note above call to
	/// LocalSetTextOnControls function for further explanation)
	/// </remarks>
	public partial  class FooterControl : TDUserControl, ILanguageHandlerIndependent
	{
		protected System.Web.UI.HtmlControls.HtmlAnchor TopofPage;

        #region Page_Init, Page_Load,Page_PreRender

        /// <summary>
		/// Wires additional events not taken care of by Visual Studio
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Init(object sender, System.EventArgs e)
		{
			buttonLogViewer.Click += new EventHandler(buttonLogViewer_Click);
			buttonLogViewer.Text = resourceManager.GetString("FooterControl1.buttonLogViewer.Text", TDCultureInfo.CurrentUICulture);
			buttonVersionViewer.Click += new EventHandler(buttonVersionViewer_Click);
			buttonVersionViewer.Text = resourceManager.GetString("FooterControl1.buttonVersionViewer.Text", TDCultureInfo.CurrentUICulture);
			buttonFeedbackViewer.Click += new EventHandler(buttonFeedbackViewer_Click);
			buttonFeedbackViewer.Text = resourceManager.GetString("FooterControl1.buttonFeedbackViewer.Text", TDCultureInfo.CurrentUICulture);
		}

		/// <summary>
		/// the page load checks the current channel and sets the English/Welsh 
		/// hyperlink url to point to the matching page in the other channel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{

			//default channel to English	
			string strChn = TDPage.EnglishChannelIndicator;
				
			// If the channel is null, assume not using Content Management Server			
			if (TDPage.SessionChannelName !=  null )
			{	//get ISO language code for this channel
				//string of the current CMS channel
				strChn = TDPage.SessionChannelName.ToString();
			}			

			//string of the name of the current posting
			string postingName = string.Empty;

			//temp strings used for regular expression 
			string strFrm = string.Empty;
			string strTo = string.Empty;			

			//regular expression object
			Regex r;

			//toggle the language channel
            //switch (strChn.Substring(17, 2))
            //{				
            //        //switch language from welsh to english
            //    case "cy":			
            //        strFrm = TDPage.WelshChannelIndicator;// "/cy/"
            //        strTo = TDPage.EnglishChannelIndicator;// "/en/"
            //        break;

            //        //switch language from english to welsh
            //    case "en":
            //        strFrm = TDPage.EnglishChannelIndicator;// "/en/"
            //        strTo = TDPage.WelshChannelIndicator; // "/cy/"
            //        break;

            //        //the default should always be English 
            //    default:
            //        strFrm = TDPage.WelshChannelIndicator;// "/cy/"
            //        strTo = TDPage.EnglishChannelIndicator;// "/en/"
            //        break;
            //}	
				
			//Create the regular expression
			r  = new Regex(strFrm);
			//replace "en" for "cy" and vice versa
			strChn  = r.Replace(strChn, strTo);
			//get posting name
			postingName = CmsHttpContext.Current.Posting.DisplayName;

            // Marker interface ILanguageHandlerIndependent is implemented so that the class opts
			// out of using LanguageHandler.SetTextOnControls function to populate its text. This is necessary
			// as the parent page may have a different resource manager. The page's resource manager is cascaded
			// down to each control on the page for population of its text. Using a different resource manager
			// on the main page therefore ends up in the wrong one being used for this footer control. The footer
			// control should always use the default resource manager (as all it's entries are in the default langString file).
			// The LanguageHandler.LocalSetTextOnControls allows the correct resource manager to be set and only
			// runs for the controls on the page that is passed as a parameter (there is no recursiveness).
			LanguageHandler.LocalSetTextOnControls(this, Global.tdResourceManager);

			//create the page url to add to the Top of Page link
			//			TopofPage.HRef = strChnNotChange.ToString();
			//			TopofPage.HRef += postingName+("#logoTop");

			//Del5.2 - Get the String containing version number from properties database
			labelVersion.Text = Properties.Current["footer.versionnumber"];
						
			string HomeUrl = Global.tdResourceManager.GetString("FooterControl1.hrefHomeLinkButton", TDCultureInfo.CurrentUICulture);
			string HelpUrl = Global.tdResourceManager.GetString("FooterControl1.hrefHelpLinkButton", TDCultureInfo.CurrentUICulture);
			string AboutUrl = Global.tdResourceManager.GetString("FooterControl1.hrefAboutLinkButton", TDCultureInfo.CurrentUICulture);
			string ContactUrl = Global.tdResourceManager.GetString("FooterControl1.hrefContactUsLinkButton", TDCultureInfo.CurrentUICulture);
			string SitemapUrl = Global.tdResourceManager.GetString("FooterControl1.hrefSiteMapLinkButton", TDCultureInfo.CurrentUICulture);
			string RelatedUrl = Global.tdResourceManager.GetString("FooterControl1.hrefRelatedSitesLinkButton", TDCultureInfo.CurrentUICulture);
            string MobileUrl = GetPageURL(Common.PageId.MobileDefault);
            string TermsUrl = Global.tdResourceManager.GetString("FooterControl1.hrefTermsConditions", TDCultureInfo.CurrentUICulture);
			string PrivacyUrl = Global.tdResourceManager.GetString("FooterControl1.hrefPrivacyPolicy", TDCultureInfo.CurrentUICulture);
			string DataUrl = Global.tdResourceManager.GetString("FooterControl1.hrefDataProviders", TDCultureInfo.CurrentUICulture);
			string AccessUrl = Global.tdResourceManager.GetString("FooterControl1.hrefAccess", TDCultureInfo.CurrentUICulture);

			//HomeLinkButton.HRef = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + HomeUrl;
			HelpLinkButton.HRef = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + HelpUrl;
			AboutLinkButton.HRef = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + AboutUrl;
			ContactUsLinkButton.HRef = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + ContactUrl;
			SiteMapLinkButton.HRef = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + SitemapUrl;
            MobileSite.HRef = MobileUrl;
            TermsConditions.HRef = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + TermsUrl;
			PrivacyPolicy.HRef = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + PrivacyUrl;
			DataProviders.HRef = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + DataUrl;
			RelatedSitesLinkButton.HRef = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + RelatedUrl;
			Accessibility.HRef = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + AccessUrl;

			//set text
			//HomeLinkButton.InnerText = this.GetResource("FooterControl1.HomeLinkButton");
			HelpLinkButton.InnerText = this.GetResource("FooterControl1.HelpLinkButton");
			AboutLinkButton.InnerText = this.GetResource("FooterControl1.AboutLinkButton");
			ContactUsLinkButton.InnerText = this.GetResource("FooterControl1.ContactUsLinkButton");
			SiteMapLinkButton.InnerText = this.GetResource("FooterControl1.SiteMapLinkButton");
            MobileSite.InnerText = this.GetResource("FooterControl1.MobileSite");
            TermsConditions.InnerText = this.GetResource("FooterControl1.TermsConditions");
			PrivacyPolicy.InnerText = this.GetResource("FooterControl1.PrivacyPolicy");
			DataProviders.InnerText = this.GetResource("FooterControl1.DataProviders");
			RelatedSitesLinkButton.InnerText = this.GetResource("FooterControl1.RelatedSitesLinkButton");
			Accessibility.InnerText = this.GetResource("FooterControl1.Accessibility");

			// Display Viewer links depending on user's type
			bool userIsLoggedOn = TDSessionManager.Current.Authenticated;

			// Get the user's type
            int userType = userIsLoggedOn ? (int)TDSessionManager.Current.CurrentUser.UserType : (int)TDUserType.Standard;

			if (userType > 0)
			{
				// Show the table, unless we are on the retailer handoff page in which case
				// we can't show it as it causes problems
				CjpLoggingTable.Visible = (this.PageId != PageId.TicketRetailersHandOff);

                // Set the user type on the page, to allow any javascript functions to detect users level
                if (CJPUserInfoHelper.IsCJPInformationAvailable())
                {
                    hdnUserLevel.Value = userType.ToString();
                }
			}
			else
			{
				CjpLoggingTable.Visible = false;
			}
		}

        /// <summary>
        /// Calls the DataBind method to populate the "Top of page" link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Page_PreRender(object sender, EventArgs e)
        {
            //Update the english/welsh link at the bottom with the correct text for the context:
            SetLinkText();
            //			this.DataBind();

            SetupControlsVisibility();
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
		}
		#endregion
        		
        #region Public methods

        public string TopofPageLabel()
		{
			return Global.tdResourceManager.GetString("JourneyPlanner.hyperLinkTopOfPage.Text", TDCultureInfo.CurrentUICulture);

		}

		public string TopofPageTitleText()
		{
			return Global.tdResourceManager.GetString("FooterControl1.TopofPage.Title", TDCultureInfo.CurrentUICulture);
		}

        #endregion

        #region Event Handlers

        /// <summary>
		/// Handler for the VersionViewer image button.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonVersionViewer_Click(object sender, EventArgs e)
		{
			// Write TransitionEvent GoVersionViewer to the session
			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoVersionViewer;
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();

		}

		/// <summary>
		/// Handler for the LogViewer image button
		/// </summary>
		private void buttonLogViewer_Click(object sender, EventArgs e)
		{
			// Write TransitionEvent GoLogViewer to the session
			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoLogViewer;
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
		}

		/// <summary>
		/// Handler for the FeedbackViewer image button.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonFeedbackViewer_Click(object sender, EventArgs e)
		{
			// Write TransitionEvent GoVersionViewer to the session
			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoFeedbackViewer;
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
		}
        
        protected void lnkLanguageSwitch_Click(object sender, EventArgs e)
        {
            //It is only possible to switch language by clicking the link, 
            //so perform a straight switch, but not here... goto TDPAge::OnInit
            CurrentLanguage.Value = (CurrentLanguage.Value == Language.English ? Language.Welsh : Language.English);
            Server.Transfer(Request.RawUrl);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Gets property value for the property
        /// This property should be used for the properties which have boolean values defined
        /// </summary>
        /// <param name="propertyName">Name of the property</param>
        /// <returns>Boolean value of property</returns>
        private bool GetPropertyValue(string propertyName)
        {
            bool propertyValue = false;

            string propertyValueString = Properties.Current[propertyName];

            if (!string.IsNullOrEmpty(propertyValueString))
            {
                if (!bool.TryParse(propertyValueString, out propertyValue))
                {
                    propertyValue = false;
                }
            }

            return propertyValue;
        }

        private void SetLinkText()
        {
            //Create a template for the resource key name:
            string resourceKeyTemplate = "FooterControl1.LanguageLink.{0}";
            CultureInfo currentCulture;

            //Check what the current language is and set the current culture:
            switch (CurrentLanguage.Value)
            {
                default:
                case Language.English:
                    currentCulture = TDCultureInfo.CreateSpecificCulture("en-GB");
                    break;
                case Language.Welsh:
                    currentCulture = TDCultureInfo.CreateSpecificCulture("cy-GB");
                    break;
            }

            StringBuilder linkText = new StringBuilder();

            linkText.Append(Global.tdResourceManager.GetString(string.Format(resourceKeyTemplate, "en-GB"), currentCulture));
            linkText.Append("/");
            linkText.Append(Global.tdResourceManager.GetString(string.Format(resourceKeyTemplate, "cy-GB"), currentCulture));

            lnkLanguageSwitch.CssClass = "TDLanguageLink";
            lnkLanguageSwitch.CssClassMouseOver = "TDLanguageLinkMouseOver";
            lnkLanguageSwitch.Text = linkText.ToString();

            //hyperlinkTopOfPage.Text = TopofPageLabel();
            //imageTopOfPage.ImageUrl = ImageUrlHelper.GetAlteredImageUrl(GetResource("JourneyPlanner.hyperLinkImageOutwardJourneys"));
        }

        /// <summary>
        /// Returns the mobile site url
        /// </summary>
        /// <returns></returns>
        private string GetPageURL(PageId targetPageId)
        {
            // Get the page transfer details
            string url = string.Empty;
            try
            {
                // Get transfer details
                IPageController pageController = (IPageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
                PageTransferDetails transferDetail = pageController.GetPageTransferDetails(targetPageId);
                url = ResolveClientUrl(transferDetail.PageUrl);
            }
            catch (Exception ex)
            {
                string message = string.Format("An error occurred retrieving PageTransferDetails for PageId[{0}]. Page has not been declared in the sitemap configuration.", targetPageId);
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message, ex));

                // This is serious as a page/control is attempting to go to requested page and it hasn't been setup
                throw;
            }

            return url;
        }

        /// <summary>
        /// Sets Footer Control's Navigation link visiblity
        /// </summary>
        private void SetupControlsVisibility()
        {
            helpLink.Visible = GetPropertyValue("FooterControl.HelpLink.Visible");
            aboutLink.Visible = GetPropertyValue("FooterControl.AboutLink.Visible");
            contactUsLink.Visible = GetPropertyValue("FooterControl.ContactUsLink.Visible");
            siteMapLink.Visible = GetPropertyValue("FooterControl.SiteMapLink.Visible");
            languageSwitchLink.Visible = GetPropertyValue("FooterControl.LanguageSwitchLink.Visible");
            relatedSitesLink.Visible = GetPropertyValue("FooterControl.RelatedSitesLink.Visible");
            mobileSiteLink.Visible = GetPropertyValue("FooterControl.MobileSiteLink.Visible");
            termsConditionsLink.Visible = GetPropertyValue("FooterControl.TermsConditionsLink.Visible");
            privacyPolicyLink.Visible = GetPropertyValue("FooterControl.PrivacyPolicyLink.Visible");
            dataProvidersLink.Visible = GetPropertyValue("FooterControl.DataProvidersLink.Visible");
            accessibilityLink.Visible = GetPropertyValue("FooterControl.AccessibilityLink.Visible");
        }

        #endregion
    }
}
