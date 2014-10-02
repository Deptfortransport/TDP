// *********************************************** 
// NAME                 : Home(TipsTools).aspx.cs 
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 07/12/2005
// DESCRIPTION			: Webform containing the Mini-
//                        Homepage for Tips and Tools.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Tools/Home.aspx.cs-arc  $
//
//   Rev 1.4   Feb 07 2012 12:45:00   DLane
//Check in part 1 for  BatchJourneyPlanner - edited classes
//Resolution for 5787: Batch Journey Planner
//
//   Rev 1.3   Jul 28 2011 16:21:28   DLane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.2   Mar 31 2008 13:27:16   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Feb 15 2008 12:28:00 apatel
//  CCN 427 - Changes made to switch on or off various functional areas on page depending on
//  property set for that area.
//
//
//   Rev 1.0   Nov 08 2007 13:32:10   mturner
//Initial revision.
//
//   Rev 1.8   Mar 09 2007 16:17:14   mmodi
//Added DigiTv links
//Resolution for 4369: Add DigiTV link and icon to Tools homepage
//
//   Rev 1.7   Mar 06 2007 12:30:00   Build
//Automatically merged from branch for stream4350
//
//   Rev 1.6.1.0   Feb 20 2007 15:53:28   mmodi
//Added Journey Emissions Compare link
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.6   Jan 19 2007 15:40:38   mmodi
//Updated Feedback page to point to FeedbackPage
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.5   Feb 24 2006 12:20:32   AViitanen
//Merge for Enhanced Exposed Services (stream3129)
//
//   Rev 1.4   Feb 14 2006 17:39:34   tolomolaiye
//Fixed merge errors
//
//   Rev 1.3   Jan 03 2006 16:04:22   RGriffith
//Removal of "Keywords" and "Desc" meta tags.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.2   Dec 30 2005 12:06:50   NMoorhouse
//Updated following screen review
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.1   Dec 23 2005 15:44:16   RGriffith
//Changes to footer style and changes to use TDImage
//
//   Rev 1.0   Dec 22 2005 15:31:46   NMoorhouse
//Initial revision.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.6   Dec 21 2005 11:20:02   RGriffith
//Changes to Bookmark link
//
//   Rev 1.5   Dec 20 2005 16:27:32   NMoorhouse
//Updates for multi-browser support
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.4   Dec 20 2005 14:22:28   RGriffith
//Changes for using clientlinks on the homepages
//
//   Rev 1.3   Dec 19 2005 10:43:32   RGriffith
//Changes to use ExpandableMenu control for related links rather than SuggestionBoxControl
//
//   Rev 1.2   Dec 15 2005 17:36:22   NMoorhouse
//Updates to Mini homepages
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.1   Dec 14 2005 18:14:08   NMoorhouse
//Later version of progress pages
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.0   Dec 09 2005 12:20:28   NMoorhouse
//Initial revision.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
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
using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.Web.Templates.Tools
{
	/// <summary>
	/// Summary description for HomeTipsTools.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class Home : TDPage
	{
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
		protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl expandableMenuControl;
		protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl relatedLinksControl;
		protected TransportDirect.UserPortal.Web.Controls.ClientLinkControl clientLink;

		


		

	
		#region Constructor
		/// <summary>
		/// Constructor - sets the page id.
		/// </summary>
		public Home() : base()
		{
			pageId = PageId.HomeTipsTools;
		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			PopulatePage();
		}

		/// <summary>
		/// Assigns soft content to the page controls
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PopulatePage()
		{
			// Set up <Head> tag information
			PageTitle = GetResource("HomeTipsTools.PageTitle");
			
			// Set Page Labels
			literalPageHeading.Text = GetResource("HomeTipsTools.literalPageHeading");
			
			// Assign URLs to hyperlinks
			IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
			string baseChannel = String.Empty;
			string url = String.Empty;
			if (TDPage.SessionChannelName != null)
				baseChannel = getBookmarkBaseChannelURL(TDPage.SessionChannelName);
						
			// Transfer details for TDOnTheMoveInput
			PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.TDOnTheMove);
			url = baseChannel + pageTransferDetails.PageUrl;
			hyperlinkTDOnTheMove.NavigateUrl = url;

            // specific parner url for this link
            string partnerDefinedUrl = GetResource("HomeTipsTools.PartnerSpecific.hyperlinkTDOnTheMove.NavigateURL");
            if (!string.IsNullOrEmpty(partnerDefinedUrl))
            {
                hyperlinkTDOnTheMove.NavigateUrl = partnerDefinedUrl;
                hyperlinkTDOnTheMove.Target = GetResource("HomeTipsTools.PartnerSpecific.hyperlinkTDOnTheMove.Target");
            }

			// Transfer details for JourneyEmissionsPT
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.JourneyEmissionsCompare);
			url = baseChannel + pageTransferDetails.PageUrl;
			hyperlinkJourneyEmissionsPT.NavigateUrl = url;

			// Transfer details for LinkToUs
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.BusinessLinks);
			url = baseChannel + pageTransferDetails.PageUrl;
			hyperlinkLinkToUs.NavigateUrl = url;

			// Transfer details for ToolBarDownload
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.ToolbarDownload);
			url = baseChannel + pageTransferDetails.PageUrl;
			hyperlinkToolBarDownload.NavigateUrl = url;

			// Transfer details for BatchJourneyPlanner
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.BatchJourneyPlanner);
			url = baseChannel + pageTransferDetails.PageUrl;
            hyperlinkBatchJourneyPlanner.NavigateUrl = url;

			// Transfer details for FindAFare
			url = baseChannel + GetResource("HomeTipsTools.hrefRelatedSitesLinkButton");
			hyperlinkRelatedSites.NavigateUrl = url;

			// Transfer details for Help/Faq
			url = baseChannel + GetResource("HomeTipsTools.hrefHelpLinkButton");
			hyperlinkFaq.NavigateUrl = url;

			// Transfer details for DigiTv
			url = baseChannel + GetResource("HomeTipsTools.hrefDigiTv");
			hyperlinkDigiTv.NavigateUrl = url;

			// Assign images to image controls
			imageLinkToUs.ImageUrl = GetResource("HomeTipsTools.imageLinkToUs.ImageUrl");
			imageToolBarDownload.ImageUrl = GetResource("HomeTipsTools.imageToolbarDownload.ImageUrl");
			imageTDOnTheMove.ImageUrl = GetResource("HomeTipsTools.imageTDOnTheMove.ImageUrl");
			imageJourneyEmissionsPT.ImageUrl = GetResource("HomeTipsTools.imageJourneyEmissionsPT.ImageUrl");
			imageBatchJourneyPlanner.ImageUrl = GetResource("HomeTipsTools.imageBatchJourneyPlanner.ImageUrl");
			imageRelatedSites.ImageUrl = GetResource("HomeTipsTools.imageRelatedSites.ImageUrl");
			imageFaq.ImageUrl = GetResource("HomeTipsTools.imageFAQ.ImageUrl");
			imageDigiTv.ImageUrl = GetResource("HomeTipsTools.imageDigiTv.ImageUrl");

			// Setup gif resource for images (1 invisible image for all skip links)
			string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			imageSideNavigationSkipLink1.ImageUrl = skipLinkImageUrl;
			imageLinkToUsSkipLink.ImageUrl = skipLinkImageUrl;
			imageToolBarDownloadSkipLink.ImageUrl = skipLinkImageUrl;
			imageTDOnTheMoveSkipLink.ImageUrl = skipLinkImageUrl;
			imageJourneyEmissionsPTSkipLink.ImageUrl = skipLinkImageUrl;
			imageBatchJourneyPlannerSkipLink.ImageUrl = skipLinkImageUrl;
			imageRelatedSitesSkipLink.ImageUrl = skipLinkImageUrl;
			imageFaqSkipLink.ImageUrl = skipLinkImageUrl;
			imageDigiTvSkipLink.ImageUrl = skipLinkImageUrl;

			// Assign alt text to image controls
			imageSideNavigationSkipLink1.AlternateText = GetResource("HomeDefault.imageSideNavigationSkipLink1.AlternateText");
			imageLinkToUs.AlternateText = " ";
			imageToolBarDownload.AlternateText = " ";
			imageTDOnTheMove.AlternateText = " ";
			imageJourneyEmissionsPT.AlternateText = " ";
			imageBatchJourneyPlanner.AlternateText = " ";
			imageRelatedSites.AlternateText = " ";
			imageFaq.AlternateText = " ";
			imageDigiTv.AlternateText = " ";

			imageLinkToUsSkipLink.AlternateText = GetResource("HomeTipsTools.imageLinkToUsSkipLink.AlternateText");
			imageToolBarDownloadSkipLink.AlternateText = GetResource("HomeTipsTools.imageToolbarDownloadSkipLink.AlternateText");
			imageTDOnTheMoveSkipLink.AlternateText = GetResource("HomeTipsTools.imageTDOnTheMoveSkipLink.AlternateText");
			imageJourneyEmissionsPTSkipLink.AlternateText = GetResource("HomeTipsTools.imageJourneyEmissionsPTSkipLink.AlternateText");
			imageBatchJourneyPlannerSkipLink.AlternateText = GetResource("HomeTipsTools.imageBatchJourneyPlannerSkipLink.AlternateText");
			imageRelatedSitesSkipLink.AlternateText = GetResource("HomeTipsTools.imageRelatedSitesSkipLink.AlternateText");
			imageFaqSkipLink.AlternateText = GetResource("HomeTipsTools.imageFAQSkipLink.AlternateText");
			imageDigiTvSkipLink.AlternateText = GetResource("HomeTipsTools.imageDigiTvSkipLink.AlternateText");

			hyperlinkLinkToUs.ToolTip = GetResource("HomeTipsTools.imageLinkToUs.AlternateText");
			hyperlinkToolBarDownload.ToolTip = GetResource("HomeTipsTools.imageToolbarDownload.AlternateText");
			hyperlinkTDOnTheMove.ToolTip = GetResource("HomeTipsTools.imageTDOnTheMove.AlternateText");
			hyperlinkJourneyEmissionsPT.ToolTip = GetResource("HomeTipsTools.imageJourneyEmissionsPT.AlternateText");
			hyperlinkBatchJourneyPlanner.ToolTip = GetResource("HomeTipsTools.imageBatchJourneyPlanner.AlternateText");
			hyperlinkRelatedSites.ToolTip = GetResource("HomeTipsTools.imageRelatedSites.AlternateText");
			hyperlinkFaq.ToolTip = GetResource("HomeTipsTools.imageFAQ.AlternateText");
			hyperlinkDigiTv.ToolTip = GetResource("HomeTipsTools.imageDigiTv.AlternateText");

            // CCN 0427 Setting visibility fo the links
            hyperlinkLinkToUs.Visible = TipsAndToolsHelper.LinkToWebsiteAvailable;
            hyperlinkToolBarDownload.Visible = TipsAndToolsHelper.ToolbarDownloadAvailable;
            hyperlinkTDOnTheMove.Visible = TipsAndToolsHelper.MobileDemonstratorAvailable;
            hyperlinkJourneyEmissionsPT.Visible = TipsAndToolsHelper.CheckJourneyCO2Available;
            hyperlinkBatchJourneyPlanner.Visible = TipsAndToolsHelper.BatchJourneyPlannerAvailable;
            hyperlinkRelatedSites.Visible = TipsAndToolsHelper.RelatedSitesAvailable;
            hyperlinkFaq.Visible = TipsAndToolsHelper.FAQAvailable;
            hyperlinkDigiTv.Visible = TipsAndToolsHelper.DigitalTVInfoAvailable;


			// Assign text to labels
			literalLinkToUs.Text = GetResource("HomeTipsTools.lblLinkToUs");
			literalToolBarDownload.Text = GetResource("HomeTipsTools.lblToolbarDownload");
			literalTDOnTheMove.Text = GetResource("HomeTipsTools.lblTDOnTheMove");
			literalJourneyEmissionsPT.Text = GetResource("HomeTipsTools.lblJourneyEmissionsPT");
			literalBatchJourneyPlanner.Text = GetResource("HomeTipsTools.lblBatchJourneyPlanner");
			literalRelatedSites.Text = GetResource("HomeTipsTools.lblRelatedSites");
			literalFaq.Text = GetResource("HomeTipsTools.lblFAQ");
			literalDigiTv.Text = GetResource("HomeTipsTools.lblDigiTv");

			// Populate the left hand navigation menu
			expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuTipsAndTools);
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextHomeTools);
            expandableMenuControl.AddExpandedCategory("Related links");

			// Set up client link for bookmark on expandable menu
			clientLink.BookmarkTitle = GetResource("HomeTipsAndTools.clientLink.BookmarkTitle");
			clientLink.LinkText = GetResource("HomeTipsAndTools.clientLink.LinkText");

			// Determine url to save as bookmark
			string leftPartOfUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.HomeTipsTools);
			url = leftPartOfUrl + baseChannel + pageTransferDetails.PageUrl;
			clientLink.BookmarkUrl = url;

			// Determine if Journey Emissions PT should be shown
			if (!JourneyEmissionsHelper.JourneyEmissionsPTAvailable)
			{
				hyperlinkJourneyEmissionsPT.Visible = false;
				imageJourneyEmissionsPT.Visible = false;
				imageJourneyEmissionsPTSkipLink.Visible = false;
				literalJourneyEmissionsPT.Visible = false;
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
