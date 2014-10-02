// *********************************************** 
// NAME                 : HelpFullJP.aspx.cs 
// AUTHOR               : Andrew Sinclair 
// DATE CREATED         : 27/09/2003 
// DESCRIPTION			: MCMS template for the
// Full help pages.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/HelpFullJP.aspx.cs-arc  $:
//
//   Rev 1.8   Oct 29 2010 09:24:42   apatel
//Updated to correct the left hand menu for log in page
//Resolution for 5625: Users not able to extend their session timeout
//
//   Rev 1.7   Sep 21 2009 14:56:46   mmodi
//Updated for Environmental Benefits Calculator (EBC)
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.6   Feb 19 2009 14:44:28   Build
//Removed old CMS reference
//Resolution for 5255: Remove old CMS links from code
//
//   Rev 1.5   Oct 15 2008 11:43:04   mmodi
//Updated for xhtml compliance
//
//   Rev 1.4   Oct 14 2008 10:03:08   mmodi
//Manual merge for stream5014
//
//   Rev 1.3   Jun 26 2008 14:03:46   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.2.1.0   Jul 31 2008 11:23:34   mmodi
//Updated for cycle planner, and corrected Help text loading for PrinterFriendly page
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:23:56   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:29:44   mturner
//Initial revision.
//
//   Rev 1.15   Aug 06 2007 14:18:26   jfrank
//Fix for problem if Host other than transportdirect.info is used e.g. transportdirect.co.uk
//Resolution for 4471: URL domain issue - if .co.uk, .com, .org.uk are used.
//
//   Rev 1.14   Feb 23 2006 17:53:42   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.13   Feb 10 2006 15:09:14   build
//Automatically merged from branch for stream3180
//
//   Rev 1.12.1.0   Dec 06 2005 17:54:10   NMoorhouse
//TD101 Home Page Phase 2 - Standard page changes
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.12   Nov 04 2005 17:13:44   rhopkins
//Explicitly set the URL for printable version of page
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.11   Nov 03 2005 16:03:08   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.10.1.0   Oct 25 2005 20:20:16   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.10   Mar 22 2005 08:32:06   rscott
//Nowrap added for incorporation of TDOnTheMove Tab Image Button
//
//   Rev 1.9   Aug 06 2004 14:45:06   JHaydock
//Added TabSelection methods to TDSessionManager which is updated by each individual header control's load method and used within the HelpFullJP page to display the correct header on help pages.
//
//   Rev 1.8   Mar 18 2004 17:56:10   asinclair
//Added code to restore map viewstate
//
//   Rev 1.7   Mar 01 2004 13:41:14   asinclair
//Removed Printer hyperlink as not needed in Del 5.2
//
//   Rev 1.6   Feb 16 2004 13:51:02   asinclair
//UPdated for Del 5.2 - Printer Firendly links added
//
//   Rev 1.5   Nov 27 2003 20:40:32   asinclair
//Added BaseURL reference to pages
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
using TransportDirect.UserPortal.Resource;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for HelpFullJP.
	/// </summary>
	public partial class HelpFullJP : TDPage
	{
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
		protected TransportDirect.UserPortal.Web.Controls.PrinterFriendlyPageButtonControl printerFriendlyPageButton;

		public HelpFullJP() : base()
		{
			pageId = PageId.HelpFullJP;
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
            // Put user code to initialize the page here
            if (!LoadDatabaseContentIfNecessary())
                Server.Transfer("~/home.aspx");
            else
            {
                // CCN 0427 Setting Printer friendly link
                string printerQueryString = Request.QueryString.ToString().ToLower().Replace("_help_", "_printer_");
                string uri = "HelpFullJPrinter.aspx?" + printerQueryString;

                // set the hyperlinks
                PrinterFriendlyLink.NavigateUrl = uri;
                hyperlinkText.Text = GetResource("PrinterFriendlyPageButton.Text");
                //setting the tool tip for the printer friendly
                PrinterFriendlyLink.ToolTip = GetResource("PrinterFriendlyButton.Tooltip");
                HelpLabelTitle.Text = GetResource("HelpFullJP.HelpLabelTitle.Text");
            }

             //Get the path without the filename:
                Uri url = Request.Url;
                string fileName = url.Segments[url.Segments.Length - 1];
                string baseUrlText = url.ToString().Replace(fileName, "");

                basehelpliteral.Text = string.Format(@"<base href=""{0}"" />", baseUrlText);
			
            //Do not show back button if HelpToolbar page
            string id = Request.QueryString["id"] == null ? "" : Request.QueryString["id"].ToString();
            if (id == "_web2_help_helptoolbar")
            {
                buttonBack1.Visible = false;
            }
			
			buttonBack1.Text = GetResource("HelpFullJP.buttonBack1.Text");
			

            ConfigureLeftHandMenu();
		}

		override protected void OnUnload(EventArgs e)
		{
			base.OnUnload(e);
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
			ExtraEventWireUp();
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

		/// <summary>
		/// Extra event subscription
		/// </summary>
		private void ExtraEventWireUp()
		{
			buttonBack1.Click += new EventHandler(this.buttonBack1_Click);
			
		}
		#endregion

		private void buttonBack1_Click(object sender, EventArgs e)
		{
			TDSessionManager.Current.SetOneUseKey( SessionKey.IndirectLocationPostBack, string.Empty );
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.HelpFullJPBack; // default one

		}

        private void ConfigureLeftHandMenu()
        {
            string currentPageId = (Request.QueryString["id"] ?? "").ToLower();

            string[] idarray = currentPageId.Split(new char[] { '_' });

            string idpage = idarray[idarray.Length - 1];

            
            switch (idpage)
            {
                case "helpjourneyplannerinput":
                case "helpfindatraininput":
                case "helpfindaflightinput":
                case "helpfindacarinput":
                case "helpfindacoachinput":
                case "helpcitytocityinput":
                case "helpvisitplannerinput":
                case "helpfindabusinput":
                case "helprefinejourney":
                case "helpextensionresultssummary":
                case "helprefinedetails":
                case "helprefinetickets":
                case "helpjourneyemissionscomparejourney":
                case "helpreplanfullitinerarysummary":
                case "helpvisitplannerresults":
                case "helpambiguitygeneral":
                case "helpfindatraincoachambiguity":
                case "helpfindaflightambiguity":
                case "helpcitytocityambiguity":
                case "helpplantoparkandride":
                case "helpadjustfullitinerarysummary":
                case "helpfindacycleinput":
                    expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
                    expandableMenuControl.AddExpandedCategory("Plan a journey");
                    break;
                case "helpfindcarparkinput":
                case "helpfindcarparkambiguity":
                case "helpfindastationinput":
                    expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuFindAPlace);
                    expandableMenuControl.AddExpandedCategory("Find a place");
                    break;
                case "helpjourneyemissionscompare":
                case "helpfindebcinput":
                case "helpebcjourneydetails":
                case "helpebcjourneymap":
                    expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuTipsAndTools);
                    expandableMenuControl.AddExpandedCategory("Tips and tools");
                    break;
                case "helploginregister":
                    if (TDSessionManager.Current.Authenticated)
                    {
                        expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuLoggedIn);
                        expandableMenuControl.AddExpandedCategory("UserLoggedIn");
                    }
                    else
                    {
                        expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuLoginRegister);
                        expandableMenuControl.AddExpandedCategory("LoginRegister");
                    }
                    break;

                default:
                    expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenu);
                    expandableMenuControl.AddExpandedCategory("Plan a journey");
                    expandableMenuControl.AddExpandedCategory("Find a place");
                    expandableMenuControl.AddExpandedCategory("Live travel");
                    expandableMenuControl.AddExpandedCategory("Tips and tools");
                    break;                    
            }
        }
	}
}
