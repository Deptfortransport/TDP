// *********************************************** 
// NAME                 : StaticDefaut.aspx.cs 
// AUTHOR               : Andrew Sinclair 
// DATE CREATED         : 17/07/2003 
// DESCRIPTION			: Webform containing the default
// template for the 'Static' pages - (the non-functional pages,
// Help, About TD, Feedback and Contact Us, and Site Map) - with a
// 'Printer Friendly' option. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/StaticDefault.aspx.cs-arc  $
//
//   Rev 1.4   Jun 26 2008 14:03:58   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.3   Apr 09 2008 11:38:04   apatel
//updated to hava FAQ link to navigate to main help page
//Resolution for 4835: Del 10 - (all themes) the home button on FAQ 'entry page' does not send the user to the homepage.
//
//   Rev 1.2   Mar 31 2008 13:26:24   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Feb 28 2008 13:39:00 apatel
//  Added Expandable left hand menu, added printer friendly link and updated layout of the page.
//
//   Rev 1.0   Nov 08 2007 13:31:28   mturner
//Initial revision.
//
//   Rev 1.2   Feb 24 2006 09:27:12   AViitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.1   Feb 10 2006 15:09:24   build
//Automatically merged from branch for stream3180
//
//   Rev 1.0.1.0   Dec 02 2005 14:17:08   NMoorhouse
//TD101 Home Page Phase 2 - Standard page changes
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.0   Nov 25 2003 15:41:30   TKarsan
//Initial Revision
//
//   Rev 1.0   Nov 19 2003 11:02:20   hahad
//Initial Revision
//
//   Rev 1.8   Nov 17 2003 17:41:22   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.7   Sep 01 2003 14:45:14   asinclair
//Changed Page Load event code to comply with FXCop
//
//   Rev 1.6   Aug 29 2003 14:19:52   asinclair
//Added code to Page Load for the Printer hyperlink
//
//   Rev 1.5   Aug 19 2003 17:31:22   asinclair
//Updated with changes to Hyperlink for print page.  Still to be finished.
//
//   Rev 1.4   Aug 18 2003 16:18:40   asinclair
//added control on page
//
//   Rev 1.3   Aug 15 2003 15:56:34   asinclair
//Changed header control
//
//   Rev 1.2   Aug 13 2003 17:25:24   asinclair
//Created
//
//   Rev 1.1   Jul 24 2003 12:13:08   asinclair
//Removed the reference to the the RobotMetaTag control
//
//   Rev 1.0   Jul 24 2003 11:34:56   asinclair
//Initial Revision
//
//   Rev 1.1   Jul 22 2003 10:52:44   asinclair
//Changed Comment Block

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
using TransportDirect.Web.Support;
using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure.Content;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.Web
{
	/// <summary>
	/// Base class for the default template for the 'Static' pages 
	/// </summary>
	public partial class StaticDefault : TDPage
	{
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
        const string ScriptName = "Nifty";


        public StaticDefault() : base()
        {
            pageId = PageId.Help;
        }
        
        /// <summary>
		/// Establishes the template of the posting, and finds its connected template.
		/// It then sets up the path of this conntected posting as the URL for the Hyperlink 
		/// for printer friendly pages.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!LoadDatabaseContentIfNecessary())
                Server.Transfer("~/home.aspx");
            else
            {
                // CCN 0427 Setting Printer friendly link
                string url = "staticprinterfriendly.aspx?" + Request.QueryString.ToString();

                if (PrinterFriendlyLink != null)
                {
                    // set the hyperlinks
                    PrinterFriendlyLink.NavigateUrl = url;
                    //setting the tool tip for the printer friendly
                    PrinterFriendlyLink.ToolTip = GetResource("PrinterFriendlyButton.Tooltip");
                    hyperlinkText.Text = GetResource("PrinterFriendlyPageButton.Text"); ;
                }
            }

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

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

        // CCN 0427 registering Nifty.js to do top left content rounded corner
        protected override void OnPreRender(EventArgs e)
        {
            // Register the Nifty.js script, that actually does the work.
            if (!this.ClientScript.IsClientScriptBlockRegistered(ScriptName))
            {
                ScriptRepository.ScriptRepository repository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
                this.ClientScript.RegisterClientScriptBlock(this.GetType(), ScriptName, repository.GetScript(ScriptName, this.JavascriptDom));
            }
            base.OnPreRender(e);
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




	

		


