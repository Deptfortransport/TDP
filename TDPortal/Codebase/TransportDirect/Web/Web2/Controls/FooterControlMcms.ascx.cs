// *********************************************** 
// NAME                 : FooterControlMcms.aspx.cs 
// AUTHOR               : Andrew Sinclair 
// DATE CREATED         : 16/08/2004 
// DESCRIPTION			: A custom user control to 
// display the footer graphics and links for each page.
// This version is for the Soft Content pages, where MCMS
// is using one template for mulitple pages.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FooterControlMcms.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:20:58   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:14:38   mturner
//Initial revision.
//
//   Rev 1.8   Apr 19 2006 17:14:18   asinclair
//Removal of Inspirational Places link
//Resolution for 3942: DEL 8.1 CHANGE: Remove 'Inspirational Places' link from footer
//
//   Rev 1.7   Feb 23 2006 16:11:20   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.6   Jan 17 2006 16:28:32   esevern
//removed commented out code
//Resolution for 3334: Del 8: Remove carat symbol on About Us, FAQ and Relates Site pages
//
//   Rev 1.5   Jan 16 2006 14:29:04   esevern
//replaced '^' symbol on 'Top of page' with up arrow image
//Resolution for 3334: Del 8: Remove carat symbol on About Us, FAQ and Relates Site pages
//
//   Rev 1.4   Nov 03 2005 16:18:32   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.3.1.0   Oct 20 2005 17:10:18   rgreenwood
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.3   Nov 12 2004 15:12:44   esevern
//fix for 'top of page' link not converting to welsh
//
//   Rev 1.2   Oct 20 2004 10:19:28   jbroome
//Added Inspirational Places link to footer
//
//   Rev 1.1   Aug 16 2004 15:22:52   asinclair
//Updated to use FooterControlMcms
//
//   Rev 1.0   Aug 16 2004 13:29:42   asinclair
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;	
	using TransportDirect.Common.ResourceManager;
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

	/// <summary>
	///		Summary description for FooterControlMcms.
	/// </summary>
	public partial  class FooterControlMcms : TDUserControl
	{



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
			//this is used for the top of page href value
			string strChnNotChange = TDPage.EnglishChannelIndicator;
				
			// If the channel is null, assume not using Content Management Server			
			if (TDPage.SessionChannelName !=  null )
			{	//get ISO language code for this channel
				//string of the current CMS channel
				strChn = TDPage.SessionChannelName.ToString();
				strChnNotChange = TDPage.SessionChannelName.ToString();
			}			

			//string of the name of the current posting
			string postingName = string.Empty;

			//temp strings used for regular expression 
			string strFrm = string.Empty;
			string strTo = string.Empty;			

			//regular expression object
			Regex r;

            strFrm = TDPage.WelshChannelIndicator;// "/cy/"
            strTo = TDPage.EnglishChannelIndicator;// "/en/"
				
			//Create the regular expression
			r  = new Regex(strFrm);
			//replace "en" for "cy" and vice versa
			strChn  = r.Replace(strChn, strTo);
			//get posting name
			postingName = CmsHttpContext.Current.Posting.DisplayName;

			//Add each available language in it's
			string span = string.Empty;
			IEnumerator language = LanguageHandler.Languages.GetEnumerator();
			while ( language.MoveNext() )
			{
				HtmlAnchor anchor = new HtmlAnchor();
				anchor.Attributes.Add( "lang", ( string ) language.Current );
				anchor.Attributes.Add( "class", "BlueLink" );
				anchor.HRef = strChn.ToString();
				anchor.HRef += postingName;

				//Set the text
				anchor.InnerText = Global.tdResourceManager.GetString(
					"FooterControl1.LanguageLink." + (string) language.Current, TDCultureInfo.CurrentUICulture );

				//If this is the current language add it first
				if ( TDCultureInfo.CurrentUICulture.Name.Equals( (string) language.Current ) ) 
				{
					if ( LanguagePlaceHolder.Controls.Count > 0 ) 
					{
						//add a /
						Literal lit = new Literal();
						lit.Text = "/";
						LanguagePlaceHolder.Controls.AddAt(0, lit );
					}
					LanguagePlaceHolder.Controls.AddAt(0, anchor );
				} 
				else 
				{ 
					if ( LanguagePlaceHolder.Controls.Count > 0 ) 
					{
						//add a /
						Literal lit = new Literal();
						lit.Text = "/";
						LanguagePlaceHolder.Controls.Add( lit );
					}
					LanguagePlaceHolder.Controls.Add( anchor );
				}
			}

			//create the page url to add to the Top of Page link
			//TopofPage.HRef = strChnNotChange.ToString();
			//TopofPage.HRef += postingName+("#logoTop");
			//TopofPage.Title= Global.tdResourceManager.GetString("FooterControl1.TopofPage.Title", TDCultureInfo.CurrentUICulture);
			//TopofPage.InnerText = Global.tdResourceManager.GetString("JourneyPlanner.hyperLinkTopOfPage.Text", TDCultureInfo.CurrentUICulture);
			//hyperlinkTopOfPage.NavigateUrl = TopofPage.HRef;
			//imageTopOfPage.Src = GetResource("JourneyPlanner.hyperLinkImageOutwardJourneys");

			//Del5.2 - Get the String containing version number from properties database
			labelVersion.Text = Properties.Current["footer.versionnumber"];

			string HomeUrl = Global.tdResourceManager.GetString("FooterControl1.hrefHomeLinkButton", TDCultureInfo.CurrentUICulture);
			string HelpUrl = Global.tdResourceManager.GetString("FooterControl1.hrefHelpLinkButton", TDCultureInfo.CurrentUICulture);
			string AboutUrl = Global.tdResourceManager.GetString("FooterControl1.hrefAboutLinkButton", TDCultureInfo.CurrentUICulture);
			string ContactUrl = Global.tdResourceManager.GetString("FooterControl1.hrefContactUsLinkButton", TDCultureInfo.CurrentUICulture);
			string SitemapUrl = Global.tdResourceManager.GetString("FooterControl1.hrefSiteMapLinkButton", TDCultureInfo.CurrentUICulture);
			string RelatedUrl = Global.tdResourceManager.GetString("FooterControl1.hrefRelatedSitesLinkButton", TDCultureInfo.CurrentUICulture);
			string TermsUrl = Global.tdResourceManager.GetString("FooterControl1.hrefTermsConditions", TDCultureInfo.CurrentUICulture);
			string PrivacyUrl = Global.tdResourceManager.GetString("FooterControl1.hrefPrivacyPolicy", TDCultureInfo.CurrentUICulture);
			string DataUrl = Global.tdResourceManager.GetString("FooterControl1.hrefDataProviders", TDCultureInfo.CurrentUICulture);
			string AccessUrl = Global.tdResourceManager.GetString("FooterControl1.hrefAccess", TDCultureInfo.CurrentUICulture);
			
			
			HomeLinkButton.HRef = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + HomeUrl;
			HelpLinkButton.HRef = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + HelpUrl;
			AboutLinkButton.HRef = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + AboutUrl;
			ContactUsLinkButton.HRef = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + ContactUrl;
			SiteMapLinkButton.HRef = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + SitemapUrl;
			TermsConditions.HRef = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + TermsUrl;
			PrivacyPolicy.HRef = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + PrivacyUrl;
			DataProviders.HRef = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + DataUrl;
			RelatedSitesLinkButton.HRef = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + RelatedUrl;
			Accessibility.HRef = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + AccessUrl;


			// Display Viewer links depending on user's type
			bool userIsLoggedOn = TDSessionManager.Current.Authenticated;

			// Get the user's type
			int userType = userIsLoggedOn ? (int)TDSessionManager.Current.CurrentUser.UserType : (int)TDUserType.Standard;

			if (userType > 0)
			{
				CjpLoggingTable.Visible = true;
			}
			else
			{
				CjpLoggingTable.Visible = false;
			}

			buttonLogViewer.Text = resourceManager.GetString("FooterControlMcms1.buttonLogViewer.Text", TDCultureInfo.CurrentUICulture);
			buttonVersionViewer.Text = resourceManager.GetString("FooterControlMcms1.buttonVersionViewer.Text", TDCultureInfo.CurrentUICulture);
		}

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
			this.buttonLogViewer.Click += new EventHandler(this.buttonLogViewer_Click);
			this.buttonVersionViewer.Click += new EventHandler(this.buttonVersionViewer_Click);
		}
		#endregion

		#region Button Handlers
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
		#endregion			
	}
}
