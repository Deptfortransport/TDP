// *********************************************** 
// NAME                 : Home(TravelInfo).aspx.cs 
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 07/12/2005
// DESCRIPTION			: Webform containing the Mini-
//                        Homepage for Travel Info.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/LiveTravel/Home.aspx.cs-arc  $
//
//   Rev 1.5   Jul 28 2011 16:21:02   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.4   Mar 24 2011 11:43:04   PScott
//Mini homepage Altext changes
//Resolution for 5683: 10.16 Fixes from initial testing on SITest
//
//   Rev 1.3   Feb 11 2011 10:51:46   PScott
//IR 5674  CCN664 Updates to mini homepages
//Resolution for 5674: Updates to mini home-pages
//
//   Rev 1.2   Mar 31 2008 13:25:56   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Feb 15 2008 11:11:00 apatel
//  CCN 427 - Changes made to switch on or off various functional areas on page depending on
//  property set for that area.
//
//   Rev 1.0   Nov 08 2007 13:32:08   mturner
//Initial revision.
//
//   Rev 1.4   Feb 24 2006 12:23:04   AViitanen
//Merge for Enhanced Exposed Services (stream3129)
//
//   Rev 1.3   Jan 03 2006 16:03:32   RGriffith
//Removal of "Keywords" and "Desc" meta tags.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.2   Dec 30 2005 12:06:48   NMoorhouse
//Updated following screen review
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.1   Dec 23 2005 15:43:12   RGriffith
//Changes to footer style and changes to use TDImage
//
//   Rev 1.0   Dec 22 2005 15:30:42   NMoorhouse
//Initial revision.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.5   Dec 21 2005 11:20:02   RGriffith
//Changes to Bookmark link
//
//   Rev 1.4   Dec 20 2005 14:22:28   RGriffith
//Changes for using clientlinks on the homepages
//
//   Rev 1.3   Dec 19 2005 10:43:30   RGriffith
//Changes to use ExpandableMenu control for related links rather than SuggestionBoxControl
//
//   Rev 1.2   Dec 15 2005 17:36:24   NMoorhouse
//Updates to Mini homepages
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.1   Dec 14 2005 18:14:10   NMoorhouse
//Later version of progress pages
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.0   Dec 09 2005 12:20:30   NMoorhouse
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
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Templates.LiveTravel
{
	/// <summary>
	/// Summary description for HomeTravelInfo.
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
			pageId = PageId.HomeTravelInfo;
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
			PageTitle = GetResource("HomeTravelInfo.PageTitle");
			
			// Set Page Labels
			literalPageHeading.Text = GetResource("HomeTravelInfo.literalPageHeading");
			//labelRelatedLinks.Text = GetResource("HomeDefault.labelRelatedLinks.Text");
			
			// Assign URLs to hyperlinks
			IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
			string baseChannel = String.Empty;
			string url = String.Empty;
			if (TDPage.SessionChannelName != null)
                baseChannel = getBookmarkBaseChannelURL(TDPage.SessionChannelName);
						
			// Transfer details for TravelNews
			PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.TravelNews);
			url = baseChannel + pageTransferDetails.PageUrl;
//			hyperlinkTravelNews.NavigateUrl = url;
            hyperLinkLiveTravelMore.NavigateUrl = url;
            hyperLinkLiveTravel.NavigateUrl = url;

			// Transfer details for DepartureBoard
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.DepartureBoards);
			url = baseChannel + pageTransferDetails.PageUrl;
			hyperlinkDepartureBoards.NavigateUrl = url + "?NotFindAMode=true";

			// Assign images to image controls
//			imageTravelNews.ImageUrl = GetResource("HomeTravelInfo.imageTravelNews.ImageUrl");
			imageDepartureBoards.ImageUrl = GetResource("HomeTravelInfo.imageDepartureBoards.ImageUrl");

			// Setup gif resource for images (1 invisible image for all skip links)
			string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			imageSideNavigationSkipLink1.ImageUrl = skipLinkImageUrl;
			imageTravelNewsSkipLink.ImageUrl = skipLinkImageUrl;
			imageDepartureBoardsSkipLink.ImageUrl = skipLinkImageUrl;

			// Assign alt text to image controls
//			imageTravelNews.AlternateText = GetResource("HomeTravelInfo.imageTravelNews.AlternateText");
			imageDepartureBoards.AlternateText = " ";

			imageSideNavigationSkipLink1.AlternateText = GetResource("HomeDefault.imageSideNavigationSkipLink1.AlternateText");
			imageTravelNewsSkipLink.AlternateText = GetResource("HomeTravelInfo.imageTravelNewsSkipLink.AlternateText");
			imageDepartureBoardsSkipLink.AlternateText = GetResource("HomeTravelInfo.imageDepartureBoardsSkipLink.AlternateText");

//			hyperlinkTravelNews.ToolTip = GetResource("HomeTravelInfo.imageTravelNews.AlternateText");
			hyperlinkDepartureBoards.ToolTip = GetResource("HomeTravelInfo.imageDepartureBoards.AlternateText");
            hyperLinkLiveTravelMore.ToolTip = GetResource("HomeTravel.info.hyperLinkLiveTravelMore.AlternateText");

            // Setting visiblities of the Travel News links
//          hyperlinkTravelNews.Visible = TravelNewsHelper.TravelNewsAvailable;
            hyperlinkDepartureBoards.Visible = TravelNewsHelper.DepartureBoardsAvailable;


			// Assign text to labels
//			literalTravelNews.Text = GetResource("HomeTravelInfo.lblTravelNews");
            labelLiveTravel.Text = GetResource("HomeDefault.labelLiveTravel.Text");
            hyperLinkLiveTravelMore.Text = GetResource("HomeDefault.labelMore.Text");
 
            labelStatusAt.Text = GetResource("HomeDefault.labelStatusAt.Text");
            labelCurrentTime.Text = GetCurrentDateTimeString();
			literalDepartureBoards.Text = GetResource("HomeTravelInfo.lblDepartureBoards");

			// Populate the left hand navigation menu
			expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuLiveTravel);
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextHomeLiveTravel);
            expandableMenuControl.AddExpandedCategory("Related links");

			// Set up client link for bookmark on expandable menu
			clientLink.BookmarkTitle = GetResource("HomeTravelInfo.clientLink.BookmarkTitle");
			clientLink.LinkText = GetResource("HomeTravelInfo.clientLink.LinkText");

			// Determine url to save as bookmark
			string leftPartOfUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.HomeTravelInfo);
			url = leftPartOfUrl + baseChannel + pageTransferDetails.PageUrl;
			clientLink.BookmarkUrl = url;
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




        /// <summary>
        /// Returns the current date in the desired format
        /// </summary>
        private string GetCurrentDateTimeString()
        {
            return DisplayFormatAdapter.StandardDateAndTimeFormat(TDDateTime.Now);
        }


		#endregion
	}
}
