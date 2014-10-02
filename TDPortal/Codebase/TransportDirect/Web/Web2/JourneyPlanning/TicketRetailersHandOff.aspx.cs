// *********************************************** 
// NAME                 : TicketRetailersHandOff.aspx.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 23/10/2003
// DESCRIPTION			: Hand-off page to ticket retailers.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/TicketRetailersHandOff.aspx.cs-arc  $
//
//   Rev 1.10   Mar 29 2010 16:39:18   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.9   Oct 12 2009 10:14:26   mmodi
//Removed unused variable
//
//   Rev 1.8   Jul 16 2008 12:33:34   apatel
//modify to make redirection work when javascript is disabled.
//Resolution for 5067: TicketRetailersHandOffFinal page fails to redirect when javascript is disabled
//
//   Rev 1.7   Jul 15 2008 08:57:54   apatel
//Ticket Retailer hand off page back button and printerfriendly buttons made working
//Resolution for 5061: Ticket Retailer Handoff back button and printer friendly button issue
//
//   Rev 1.6   Jul 14 2008 12:51:18   mmodi
//Issues with Retailer handoff popup resolve by putting the same functionality as printer friendly buttons.
//Resolution for 5035: Ticket Retailers Hand off page popup blocker issue
//Resolution for 5059: Ticket Retailer Handoff popup issue
//
//   Rev 1.5   Jul 03 2008 09:15:22   apatel
//Changed the TicketRetailersHandOff.aspx page url from hardcoded to come from the pagetransferdetails
//
//   Rev 1.4   Jul 02 2008 13:16:28   apatel
//fix the issue with popup blocker blocking the page on Ticket Retailers Hand off page. 
//Resolution for 5035: Ticket Retailers Hand off page popup blocker issue
//
//   Rev 1.3   Jun 26 2008 14:04:28   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.2   Mar 31 2008 13:25:48   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:48   mturner
//Initial revision.
//
//   Rev 1.37   Sep 06 2006 13:24:40   jfrank
//Changed code so that when a coach retail handoff is performed only coach discount cards are included in the handoff and likewise for rail.
//Resolution for 4154: NX Retail Handoff Discounts
//
//   Rev 1.36   Feb 24 2006 10:17:32   rgreenwood
//stream3129: Manual merge changes
//
//   Rev 1.35   Feb 10 2006 15:09:32   build
//Automatically merged from branch for stream3180
//
//   Rev 1.34.1.0   Dec 06 2005 10:10:50   AViitanen
//Changed to use the new HeaderControl and HeadElementControl as part of Homepage phase 2
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.34   Nov 17 2005 17:15:06   NMoorhouse
//Button Changes
//Resolution for 3077: UEE: Buy Tickets - Continue page - buttons do not use new styles
//
//   Rev 1.33   Nov 09 2005 16:01:08   ECHAN
//updates for code review
//
//   Rev 1.32   Nov 09 2005 15:19:38   ECHAN
//update for code review
//
//   Rev 1.31   Nov 09 2005 15:05:10   ECHAN
//Fix for code review comments #4
//
//   Rev 1.30   Nov 03 2005 16:00:18   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.29.1.0   Oct 12 2005 15:59:24   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.29   May 06 2005 10:38:38   jgeorge
//Retrieve data from TDItineraryManager instead of TDSessionManager
//Resolution for 2444: Ticket Purchase .NET Error Clicking On Trainline.com Buy Button
//
//   Rev 1.28   Mar 30 2005 15:42:22   jgeorge
//Changes to hand off
//
//   Rev 1.27   Mar 22 2005 08:53:32   jgeorge
//FxCop changes
//
//   Rev 1.26   Mar 08 2005 17:00:12   jgeorge
//Modifications after QA
//
//   Rev 1.25   Mar 04 2005 09:34:04   jgeorge
//Interim check in
//
//   Rev 1.24   Jan 20 2005 16:26:58   asinclair
//PublicViaLocation removed and replaced with PublicViaLocatios[] array
//
//   Rev 1.23   Jul 02 2004 15:07:32   RHopkins
//Corrected to work with ItineraryManager (including printable pages)
//
//   Rev 1.22   Jun 17 2004 21:24:52   RHopkins
//Corrected handling of Itinerary journeys
//
//   Rev 1.21   Apr 28 2004 16:20:28   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.20   Apr 15 2004 12:41:48   AWindley
//DEL5.2 QA Changes: Resolution for 729
//
//   Rev 1.19   Apr 06 2004 13:43:44   AWindley
//DEL5.2 QA Changes: Resolution for 729
//
//   Rev 1.18   Apr 02 2004 12:09:02   ESevern
//Removed users email from xml generation
//
//   Rev 1.17   Mar 22 2004 17:18:00   AWindley
//DEL5.2 Retailers Hand Off UI changes
//
//   Rev 1.16   Mar 17 2004 14:30:32   CHosegood
//Now uses jpstd.css instead of jp.css
//
//   Rev 1.15   Feb 05 2004 11:45:02   COwczarek
//Pass via location into GenerateXml method
//Resolution for 622: Via locations not being include in RetailHandoffXML
//
//   Rev 1.14   Jan 28 2004 14:27:54   COwczarek
//Rollback SCR#601 and add logging of event in page load event handler. Change hidden field id from RetailerXML to RetailXML
//Resolution for 619: Fix retailer handoff event logging & hidden field name
//
//   Rev 1.12   Dec 11 2003 15:12:22   COwczarek
//Change alt text for retailer logo to be actual retailer name not "retailer name"
//Resolution for 531: UI changes to retailer pages as a result of QA
//
//   Rev 1.11   Nov 28 2003 15:39:00   COwczarek
//Namespace change for RetailXmlDocument class
//Resolution for 451: Retail Handoff does not need to read XML schema for each request
//
//   Rev 1.10   Nov 18 2003 16:00:06   COwczarek
//SCR#247 : Complete adding comments to existing code and add $Log: for PVCS history
//
//   Rev 1.9   Nov 18 2003 10:30:16   hahad
//Changed Title Text
//
//   Rev 1.8   Nov 12 2003 11:25:24   COwczarek
//FxCop fixes applied
//
//   Rev 1.7   Nov 07 2003 17:27:54   COwczarek
//FxCop fixes
//
//   Rev 1.6   Nov 05 2003 16:54:54   COwczarek
//Work in progress
//
//   Rev 1.5   Nov 04 2003 11:59:56   COwczarek
//Work in progress. Includes mods to HTML to bring in line with standards.
//
//   Rev 1.4   Oct 31 2003 12:34:28   COwczarek
//Work in progress
//
//   Rev 1.3   Oct 31 2003 10:04:22   COwczarek
//Work in progress
//
//   Rev 1.2   Oct 24 2003 15:46:06   kcheung
//Removed two redundant variables.
//
//   Rev 1.1   Oct 24 2003 15:34:58   kcheung
//Working version

using System;
using System.Globalization;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Xml;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.PricingRetail.RetailXmlHandoff;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Web.Support;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.UserPortal.Web.Controls;

using Logger = System.Diagnostics.Trace;
using TDPublicJourney = TransportDirect.UserPortal.JourneyControl.PublicJourney;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Hand off page to Ticket Retailers.
	/// </summary>
	public partial class TicketRetailersHandOff : TDPage
	{
		#region Controls and private members

		protected System.Web.UI.WebControls.Panel handoffForm;

		protected HeaderControl headerControl;
		protected RetailerHandoffHeadingControl handoffHeading;
		protected RetailerHandoffDetailControl handoffDetail;
		protected RetailerInformationControl offlineRetailerInformation;

		private const string resourceKeyOnlineListHeading = "TicketRetailersHandOff.Online.List.{0}.Heading";
		private const string resourceKeyOnlineListItem = "TicketRetailersHandOff.Online.List.{0}.{1}";
	
		private string strMode = string.Empty;
				
		/// <summary>
		/// The retailer that was selected from the ticket retailers page
		/// </summary>
		private Retailer selectedRetailer;
		protected System.Web.UI.WebControls.HyperLink linkBack3;

		/// <summary>
		/// The retail unit that was selected from the ticket retailers page
		/// </summary>
		private RetailUnit selectedRetailUnit;

        /// <summary>
        /// Name of script (in script repository) used for opening window
        /// </summary>
        private const string JAVASCRIPT_FILE = "ErrorAndTimeoutLinkHandler";


		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor. Sets PageId and LocalResourceManager
		/// </summary>
		public TicketRetailersHandOff() : base()
		{
			pageId = PageId.TicketRetailersHandOff;
			LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
		}

		#endregion

        #region Properties
                
        /// <summary>
        /// Returns the text for the continue button
        /// </summary>
        public string ContinueButtonText 
        {
            get { return GetResource( "TicketRetailersHandOff.ContinueButton.Text"); }
        }
		
        #endregion Properties
        
		#region Event handlers

        /// <summary>
        /// Page load event handler
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, System.EventArgs e)
		{
            getRetailerDetails();
			

            initialiseControls();

            nextButton.ScriptName = GetScriptName();
            nextButton.EnableClientScript = ((TDPage)Page).IsJavascriptEnabled;

            this.PageTitle = GetResource("TicketRetailersHandOff.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");
            
            //Added for white labelling:
            ConfigureLeftMenu("TicketRetailersHandOff.clientLink.BookmarkTitle", "FindCoachInput.clientLink.LinkText", clientLink, expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextTicketRetailersHandOff);
            expandableMenuControl.AddExpandedCategory("Related links");
		
        
        }

		/// <summary>
		/// Page unload event handler.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Unload(object sender, System.EventArgs e) 
		{
			//Reset the tab selection override
			TDSessionManager.Current.TabSectionChangeable = true;
		}

        /// <summary>
        /// On Pre Render Method.		
        /// </summary>
        protected override void OnPreRender(System.EventArgs e)
        {

            // Set up the target URL
            string targetURL = string.Empty;

            IPageController pageController =
                (PageController)TDServiceDiscovery.Current
                [ServiceDiscoveryKey.PageController];

            // Get the URL for a PageID
            PageTransferDetails pageTransferDetails =
                pageController.GetPageTransferDetails(PageId.TicketRetailersHandOffFinal);

            if (pageTransferDetails != null)
            {
                if (TDPage.SessionChannelName != null)
                {
                    targetURL = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + pageTransferDetails.PageUrl;
                }
                else
                {
                    targetURL = pageTransferDetails.PageUrl;
                }
            }

            if (!string.IsNullOrEmpty(targetURL))
            {
                //Before we set up the target URL, we must correct the 
                //base URL:
                //Get the page and subfolder from the base URL it is the 
                //last section:
                //We assume that the printable page always exists in a sub 
                //folder:
                string[] sections = targetURL.Split('/');
                string page = sections[sections.Length - 1];
                string subFolder = sections[sections.Length - 2];
                targetURL = string.Format("{0}/{1}/{2}", Request.ApplicationPath, subFolder, page);

                // Set up the HTML buton
                nextButton.Action = GetAction(targetURL);

                // Use the hyperlink
                hyperlinkNext.NavigateUrl = targetURL;

                JavaScriptAdapter.InitialiseControlVisibility(nextButton, true);
            }

            base.OnPreRender(e);
        }



		/// <summary>
		/// Handler for the Click event of both normal sized back buttons.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void back_Click(object sender, EventArgs e)
		{
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoTicketRetailers;
		}

       

		#endregion 

		#region Private methods

       

        /// <summary>
        /// Gets details of retailer selected on ticket retailers page
        /// </summary>
        private void getRetailerDetails() 
        {
			PricingRetailOptionsState options = TDItineraryManager.Current.PricingRetailOptions;
			
			selectedRetailer = options.LastRetailerSelection;
			
			bool isForReturn = options.LastRetailerSelectionIsForReturn;

			if (isForReturn)
				selectedRetailUnit = options.SelectedReturnRetailUnit;
			else
				selectedRetailUnit = options.SelectedOutwardRetailUnit;

        }

		/// <summary>
		/// Initialises all controls
		/// </summary>
		private void initialiseControls()
		{	
			LoadStaticLabels(selectedRetailer.isHandoffSupported);
			UpdateFaresHeader();
			UpdateData();

			if (selectedRetailer.isHandoffSupported)
			{
				panelHandoff.Visible = true;
				panelOfflineInformation.Visible = false;

			}
			else
			{
				panelHandoff.Visible = false;
				panelOfflineInformation.Visible = true;
				offlineRetailerInformation.RetailerDetails = selectedRetailer;
			}
		}

		/// <summary>
		/// Populates the journey details from the session
		/// </summary>
		private void UpdateFaresHeader()
		{
			PricingRetailOptionsState pricingOptions = TDItineraryManager.Current.PricingRetailOptions;
			IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

			handoffHeading.CoachCardName = ds.GetText(DataServiceType.DiscountCoachCardDrop, pricingOptions.Discounts.CoachDiscount);
			handoffHeading.RailCardName = ds.GetText(DataServiceType.DiscountRailCardDrop, pricingOptions.Discounts.RailDiscount);
		}

		/// <summary>
		/// Updates the detail control
		/// </summary>
		private void UpdateData()
		{
			PricingRetailOptionsState pricingOptions = TDItineraryManager.Current.PricingRetailOptions;
			DiscountCardCatalogue discountCards = (DiscountCardCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.DiscountCardCatalogue];

			handoffDetail.AdultsTravelling = pricingOptions.AdultPassengers;
			handoffDetail.ChildrenTravelling = pricingOptions.ChildPassengers;
			handoffDetail.SelectedTickets = selectedRetailUnit.Tickets;
			
			if (pricingOptions.Discounts.RailDiscount.Length != 0)
				handoffDetail.RailCard = discountCards.GetDiscountCard(ModeType.Rail, pricingOptions.Discounts.RailDiscount);

			if (pricingOptions.Discounts.CoachDiscount.Length != 0)
				handoffDetail.CoachCard = discountCards.GetDiscountCard(ModeType.Coach, pricingOptions.Discounts.CoachDiscount);

			// Only show the discount disclaimer if we have discount cards specifed
            panelDiscountCardDisclaimer.Visible = (handoffDetail.RailCard != null) || (handoffDetail.CoachCard != null);
		}


		/// <summary>
		/// Loads data into the labels on the page. Different labels are populated for an online retailer than
		/// for an offline one.
		/// </summary>
		/// <param name="online"></param>
		private void LoadStaticLabels(bool online)
		{
			// Set the page title
			labelPageName.Text = GetResource("TicketRetailersHandOff.PageTitle");
			labelDiscountDisclaimer.Text = GetResource("TicketRetailersHandOff.DiscountNote");

            nextButton.Text = ContinueButtonText;
            nextButton.ToolTip = GetResource("TicketRetailersHandOff.ContinueButton.Tooltip");
			back1.Text = GetResource("TicketRetailersHandOff.BackButton.Text");

            hyperlinkNext.Text = ContinueButtonText;
            hyperlinkNext.ToolTip = GetResource("TicketRetailersHandOff.ContinueButton.Tooltip");

			IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
			PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.TicketRetailers);
			string url = pageTransferDetails.PageUrl;
			if (TDPage.SessionChannelName !=  null )
				url = getBaseChannelURL(TDPage.SessionChannelName) + url;

			if (online)
			{
				labelClickContinue.Text = string.Format( TDCultureInfo.InvariantCulture, GetResource("TicketRetailersHandOff.Online.ClickContinue"), selectedRetailer.Name );

				labelList1Heading.Text = GetResource( string.Format( TDCultureInfo.InvariantCulture, resourceKeyOnlineListHeading, "1") );
				labelList1Item1.Text = string.Format( TDCultureInfo.InvariantCulture, GetResource( string.Format( TDCultureInfo.InvariantCulture, resourceKeyOnlineListItem, "1", "1") ), selectedRetailer.Name );
				labelList1Item2.Text = string.Format( TDCultureInfo.InvariantCulture, GetResource( string.Format( TDCultureInfo.InvariantCulture, resourceKeyOnlineListItem, "1", "2") ), selectedRetailer.Name );
				labelList1Item3.Text = GetResource( string.Format( TDCultureInfo.InvariantCulture, resourceKeyOnlineListItem, "1", "3") );

				labelList2Heading.Text = GetResource( string.Format( TDCultureInfo.InvariantCulture, resourceKeyOnlineListHeading, "2") );
				labelList2Item1.Text = GetResource( string.Format( TDCultureInfo.InvariantCulture, resourceKeyOnlineListItem, "2", "1") );
				labelList2Item2.Text = GetResource( string.Format( TDCultureInfo.InvariantCulture, resourceKeyOnlineListItem, "2", "2") );

				labelList3Heading.Text = GetResource( string.Format( TDCultureInfo.InvariantCulture, resourceKeyOnlineListHeading, "3") );
				labelList3Item1.Text = string.Format( TDCultureInfo.InvariantCulture, GetResource( string.Format( TDCultureInfo.InvariantCulture, resourceKeyOnlineListItem, "3", "1") ), selectedRetailer.Name );
				labelList3Item2.Text = GetResource( string.Format( TDCultureInfo.InvariantCulture, resourceKeyOnlineListItem, "3", "2") );
				labelList3Item3.Text = GetResource( string.Format( TDCultureInfo.InvariantCulture, resourceKeyOnlineListItem, "3", "3") );
				labelList3Item4.Text = GetResource( string.Format( TDCultureInfo.InvariantCulture, resourceKeyOnlineListItem, "3", "4") );
				labelList3Item5.Text = GetResource( string.Format( TDCultureInfo.InvariantCulture, resourceKeyOnlineListItem, "3", "5") );
				labelList3Item6.Text = GetResource( string.Format( TDCultureInfo.InvariantCulture, resourceKeyOnlineListItem, "3", "6") );
			}
			else
			{
				labelOfflineInstructions.Text = GetResource( "TicketRetailersHandOff.Offline.Instructions" );
			}
				
		}

        #endregion Private methods
        
   		#region Web Form Designer generated code
   		
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
           
			//Ensure that the tab selection isn't changed because this page has each one of
			//the tab headers and when each one loads, it will overwrite the current one
			TDSessionManager.Current.TabSectionChangeable = false;

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
			back1.Click += new EventHandler(this.back_Click);
            
		}
        
		#endregion

        #region methods to make popup not blocked by popupblockers
        /// <summary>
        /// Returns the Javascript function to execute when the button is clicked
        /// </summary>
        /// <returns>string</returns>
        /// <param name="url">url</param>
        protected string GetAction(string url)
        {
            return "return OpenWindow('" + url + "');";
        }

        /// <summary>
        /// Returns the name of the Javascript file containing the code to execute when the 
        /// button is clicked
        /// </summary>
        /// <returns></returns>
        protected string GetScriptName()
        {
            return JAVASCRIPT_FILE;
        }

        #endregion
    }

    // Class used to open ticket retailer page in new window as nested forms are not
    // allowed in asp.net 2.0
    public partial class RemotePost
    {
        private System.Collections.Specialized.NameValueCollection Inputs = new System.Collections.Specialized.NameValueCollection();


        public string Url = "";
        public string Method = "post";
        public string FormName = "form1";
        public string targetType = "_self";

        private string jsDisabledText = string.Empty;

        /// <summary>
        /// setting the target for the remote post
        /// set _blank to open the remote site in new window
        /// default is set to _self which will open the remote posting site in same window
        /// </summary>
        public string TargetType
        {
            get { return targetType; }
            set { targetType = value; }
        }

        /// <summary>
        /// This is the text which will be displayed when javascript is disabled.
        /// </summary>
        public string JSDisabledText
        {
            get { return jsDisabledText; }
            set { jsDisabledText = value; }
        }


        public void Add(string name, string value)
        {
            Inputs.Add(name, value);
        }

        public void Post()
        {
            System.Web.HttpContext.Current.Response.Clear();

            System.Web.HttpContext.Current.Response.Write("<html><head><style>.button{background:#fff; border: none;cursor: pointer;cursor: hand;color: #0000EE;}.button:hover{color: #8800CC; background-color:  #FFFFFF;text-decoration: underline;}</style>");

            System.Web.HttpContext.Current.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
            System.Web.HttpContext.Current.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" target=\"{3}\" >", FormName, Method, Url, targetType));
            for (int i = 0; i < Inputs.Keys.Count; i++)
            {
                System.Web.HttpContext.Current.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", Inputs.Keys[i], Inputs[Inputs.Keys[i]]));
            }
            System.Web.HttpContext.Current.Response.Write("<noscript>");
            System.Web.HttpContext.Current.Response.Write(string.Format("<input type=\"submit\" class=\"button\" value=\"{0}\" />", jsDisabledText));

            System.Web.HttpContext.Current.Response.Write("</noscript>");
            System.Web.HttpContext.Current.Response.Write("</form>");
            System.Web.HttpContext.Current.Response.Write("</body></html>");

            System.Web.HttpContext.Current.Response.End();
        }
    }

}
