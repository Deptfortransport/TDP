// *********************************************** 
// NAME                 : StaticNoPrint.aspx.cs 
// AUTHOR               : Andrew Sinclair 
// DATE CREATED         : 17/07/2003 
// DESCRIPTION			: Webform containing the default
// template for the 'Static' pages -(the non-functional pages,
// Help, About TD, Feedback and Contact Us, and Site Map)
// Del 5.2 - Now with a 'PRINTER FRIENDLY PAGE' option! 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/StaticNoPrint.aspx.cs-arc  $
//
//   Rev 1.9   Mar 14 2013 15:09:08   dlane
//Change batch FAQ page to have generic left hand FAQ menu
//Resolution for 5903: FAQ page - left hand menu link to batch FAQ missing
//
//   Rev 1.8   Jan 11 2013 11:46:40   mmodi
//Added accessible journey help
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.7   Nov 25 2008 10:22:06   jfrank
//Fix to show correct LHM on FAQ 17
//Resolution for 5154: Welcome to Britain - Update VB FAQ & Related Links
//
//   Rev 1.6   Oct 30 2008 14:13:40   sangle
//Added Tourist Info for FAQ page link
//
//   Rev 1.5   Oct 16 2008 15:57:32   mmodi
//Updated to set left hand link for Cycle FAQ page
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   Jun 26 2008 14:04:00   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.3   May 06 2008 16:49:58   mmodi
//Corrected logged pages for FAQ
//Resolution for 4948: Incorrect page event for FAQ pages, as raised by Peter Armstrong
//
//   Rev 1.2   Mar 31 2008 13:26:26   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Feb 28 2008 13:39:00 apatel
//  Added Expandable left hand menu, added printer friendly link and updated layout of the page.
//
//   Rev 1.0   Nov 08 2007 13:31:28   mturner
//Initial revision.
//
//   Rev 1.15   Aug 06 2007 14:26:28   jfrank
//Fix for problem if Host other than transportdirect.info is used e.g. transportdirect.co.uk
//Resolution for 4471: URL domain issue - if .co.uk, .com, .org.uk are used.
//
//   Rev 1.14   Feb 24 2006 10:17:38   rgreenwood
//stream3129: Manual merge changes
//
//   Rev 1.13   Feb 10 2006 15:09:26   build
//Automatically merged from branch for stream3180
//
//   Rev 1.12.1.0   Dec 02 2005 14:17:08   NMoorhouse
//TD101 Home Page Phase 2 - Standard page changes
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.12   Nov 18 2005 19:20:10   rhopkins
//Override standard PrinterFirendlyButtonControl URL handling, by manually deriving printer friendly URL from current MCMS channel.
//Resolution for 3016: UUE: Printable Soft Content Pages - print button does not work
//
//   Rev 1.11   Nov 08 2005 11:43:00   ralonso
//Changes to the printer button
//
//   Rev 1.10   Jun 25 2004 11:16:50   jgeorge
//Removed reference to Welsh header control.
//
//   Rev 1.9   Feb 16 2004 13:54:18   asinclair
//Updated for Del 5.2 - PrinterFriendly functionality added
//
//   Rev 1.8   Feb 03 2004 16:46:44   asinclair
//Added PrinterFriendly Logos
//
//   Rev 1.7   Nov 26 2003 14:19:42   asinclair
//changed the  baseliteralURL value in the properties database to StaticNoPrint
//
//   Rev 1.6   Nov 21 2003 12:35:58   asinclair
//Added base URL literal to solve problems in MCMS if javascript disabled
//
//   Rev 1.5   Nov 17 2003 17:53:46   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.4   Sep 27 2003 15:54:10   asinclair
//Added code to page load event
//
//   Rev 1.3   Sep 22 2003 18:28:22   asinclair
//Added Page Id
//Resolution for 2: Session Manager - Deferred storage
//
//   Rev 1.2   Sep 18 2003 11:47:10   asinclair
//Changed placeholders for use with new stylesheet
//
//   Rev 1.1   Sep 10 2003 11:09:18   asinclair
//Change placeholder names to comply with design doc
//
//   Rev 1.0   Aug 29 2003 14:18:38   asinclair
//Initial Revision
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
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.Web
{
	/// <summary>
	/// Summary description for StaticNoPrint.
	/// </summary>
	public partial class StaticNoPrint : TDPage
	{
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl HeaderControl1;
		protected TransportDirect.UserPortal.Web.Controls.PrinterFriendlyPageButtonControl printerFriendlyPageButton;

        const string ScriptName = "Nifty";

		public StaticNoPrint() : base()
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

            //Here we change the page id to fix MIS, if required:
            ChangePageIdIfRequired();
            
			if (!Page.IsPostBack)			
			{	
				string baseliteralURL = Properties.Current["StaticNoPrint.baseurl"];

				int urlStart = baseliteralURL.IndexOf("http://")+7;
				int slash = baseliteralURL.IndexOf("/",urlStart);
			}

            //CCN 0427 Setting expandable menu

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
                    case "helpcycle":
                    case "helptouristinfo":
                    case "helpbatchjourneyplanner":
                    case "helpaccessiblejourneyplanning":
                        expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.FAQMenu);
                        expandableMenuControl.AddExpandedCategory("FAQ");
                        break;
                    default:
                        expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenu);
                        break;

                }


            }
        }

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

        private void ChangePageIdIfRequired()
        {
            string queryStringId = Request.QueryString["id"].ToLower();

            if (queryStringId.StartsWith("_web2_help_"))
            {
                pageId = PageId.Help;
            }
        }

		#region Properties
		/// <summary>
		/// Exposes the Print button control that links to a printer-friendly page.
		/// The Page ID of the printer-friendly page is the current page ID prefixed with "Printable".
		/// If no printer-friendly page is available then the Print button will not be shown.
		/// </summary>
		public TransportDirect.UserPortal.Web.Controls.PrinterFriendlyPageButtonControl PrinterFriendlyPageButton
		{
			get { return printerFriendlyPageButton; }
			set { printerFriendlyPageButton = value; }
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
