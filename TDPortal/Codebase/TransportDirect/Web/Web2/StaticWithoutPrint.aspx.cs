// *********************************************** 
// NAME                 : StaticWithoutPrint.aspx.cs 
// AUTHOR               : Richard Hopkins
// DATE CREATED         : 18/11/2005
// DESCRIPTION			: Webform containing the default
//						template for the 'Static' pages
//						that DO NOT include a Printer Friendly button
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/StaticWithoutPrint.aspx.cs-arc  $
//
//   Rev 1.3   Jun 26 2008 14:04:02   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.2   Mar 31 2008 13:26:28   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Feb 28 2008 13:39:00 apatel
//  Added Expandable left hand menu, added printer friendly link and updated layout of the page.
//
//   Rev 1.0   Nov 08 2007 13:31:32   mturner
//Initial revision.
//
//   Rev 1.4   Aug 06 2007 14:27:10   jfrank
//Fix for problem if Host other than transportdirect.info is used e.g. transportdirect.co.uk
//Resolution for 4471: URL domain issue - if .co.uk, .com, .org.uk are used.
//
//   Rev 1.3   Feb 24 2006 12:34:42   AViitanen
//Merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.2   Feb 15 2006 10:08:00   aviitanen
//Changed the value that was being set for the baseURL to use the value for StaticNoPrint.baseurl. 
//Resolution for 3577: DEL 8.0 : Related Sites and Site Map - Headerclick with no javascript causes error
//
//   Rev 1.1   Feb 10 2006 15:09:28   build
//Automatically merged from branch for stream3180
//
//   Rev 1.0.1.0   Dec 02 2005 14:17:14   NMoorhouse
//TD101 Home Page Phase 2 - Standard page changes
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.0   Nov 21 2005 12:02:58   rhopkins
//Initial revision.
//
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web
{
	/// <summary>
	/// Summary description for StaticWithoutPrint.
	/// </summary>
	public partial class StaticWithoutPrint : TDPage
	{
        const string ScriptName = "Nifty";

	
		public StaticWithoutPrint() : base()
		{
			pageId = PageId.Links;
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
            if (!LoadDatabaseContentIfNecessary())
                Server.Transfer("~/home.aspx");
            else
            {
                // CCN 0427 Setting Printer friendly link
                string url = "staticprinterfriendly.aspx?" + Request.QueryString.ToString();

                // set the hyperlinks
                PrinterFriendlyLink.NavigateUrl = url;
                //setting the tool tip for the printer friendly
                PrinterFriendlyLink.ToolTip = GetResource("PrinterFriendlyButton.Tooltip");
                hyperlinkText.Text = GetResource("PrinterFriendlyPageButton.Text"); ;
            }
            
            if (!Page.IsPostBack)			
			{		
				string baseliteralURL = Properties.Current["StaticNoPrint.baseurl"];

				int urlStart = baseliteralURL.IndexOf("http://")+7;
				int slash = baseliteralURL.IndexOf("/",urlStart);

//				baseliteral.Text = baseliteralURL.Substring(0, urlStart) + Request.Url.Host 
//					+ baseliteralURL.Substring(slash, baseliteralURL.Length - slash);
			}

			//string of the current CMS channel
			string strChn = TDPage.SessionChannelName.ToString();
			string language = GetChannelLanguage(strChn);

            SetExpandableMenu();

		}

        /// <summary>
        /// Sets the context for the expandable menu
        /// </summary>
        private void SetExpandableMenu()
        {
            string id = Request.QueryString["id"] == null ? "" : Request.QueryString["id"].ToString();
            if (id != "")
            {

                string[] idarray = id.Split(new char[] { '_' });

                string idpage = idarray[idarray.Length - 1];
                switch (idpage)
                {

                    case "termsconditions":
                        expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.TermsConditionsMenu);
                        expandableMenuControl.AddExpandedCategory("TermsAndPolicy");
                        break;
                    case "privacypolicy":
                        expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.PrivacyPolicyMenu);
                        expandableMenuControl.AddExpandedCategory("TermsAndPolicy");
                        break;
                    case "dataproviders":
                        expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.DataProvidersMenu);
                        expandableMenuControl.AddExpandedCategory("TermsAndPolicy");
                        break;
                    case "accessibility":
                        expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.AccessibilityMenu);
                        expandableMenuControl.AddExpandedCategory("TermsAndPolicy");
                        break;
                    case "aboutus":
                        expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.AboutUsMenu);
                        expandableMenuControl.AddExpandedCategory("AboutUs");
                        break;
                    case "relatedsites":
                        expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedSitesMenu);
                        expandableMenuControl.AddExpandedCategory("RelatedSites");
                        PrinterFriendlyLink.Visible = false;
                        break;
                    case "newhelp":
                    case "helpinfo":
                    case "helpjplan":
                    case "helptrain":
                    case "helproad":
                    case "helptaxi":
                    case "helpparking":
                    case "helpair":
                    case "helpbus":
                    case "helplivetravel":
                    case "helpmaps":
                    case "helpcosts":
                    case "helpcarbon":
                    case "helpusing":
                    case "helpmobile":
                    case "helpcomm":
                        expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.FAQMenu);
                        expandableMenuControl.AddExpandedCategory("FAQ");
                        break;

                }


            }
        }

        // CCN 0427 registering Nifty.js to do top left content rounded corner
        protected override void OnPreRender(EventArgs e)
        {
            // Register the Nifty.js script, that actually does the work.
            if (!this.ClientScript.IsClientScriptBlockRegistered(ScriptName))
            {
                ScriptRepository.ScriptRepository repository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), ScriptName, repository.GetScript(ScriptName, this.JavascriptDom));
                base.OnPreRender(e);
            }

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
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
