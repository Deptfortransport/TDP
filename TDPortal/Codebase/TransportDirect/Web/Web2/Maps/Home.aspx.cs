// *********************************************** 
// NAME                 : Home(FindAPlace).aspx.cs 
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 07/12/2005
// DESCRIPTION			: Webform containing the Mini-
//                        Homepage for Find a Place.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Maps/Home.aspx.cs-arc  $
//
//   Rev 1.8   Jul 28 2011 16:21:12   DLane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.7   Mar 24 2011 11:43:06   PScott
//Mini homepage Altext changes
//Resolution for 5683: 10.16 Fixes from initial testing on SITest
//
//   Rev 1.6   Feb 11 2011 10:51:48   PScott
//IR 5674  CCN664 Updates to mini homepages
//Resolution for 5674: Updates to mini home-pages
//
//   Rev 1.5   Nov 17 2009 11:32:20   mmodi
//Take user to the new map page
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Feb 03 2009 14:27:38   devfactory
//No change.
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Jan 29 2009 13:47:56   apatel
//No change.
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:26:02   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 15 2008 13:00:00   mmodi
//Updated Find car park url
//
//  Rev DevFactory Feb 15 2008 09:13:00 apatel
//  CCN 427 - Changes made to switch on or off various functional areas on page depending on
//  property set for that area.
//
//   Rev 1.0   Nov 08 2007 13:32:08   mturner
//Initial revision.
//
//   Rev 1.5   Oct 06 2006 17:02:08   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.4.1.2   Sep 22 2006 16:56:42   esevern
//Added check to only display car parking image and link if the properties table CarParkingAvailable value is true
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4191: Car Parking: Configurable switch should be available to display/hide car parking functionality
//
//   Rev 1.4.1.1   Aug 14 2006 10:50:38   esevern
//correct findCarParkInput page url
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.4.1.0   Aug 07 2006 15:13:52   esevern
//added image link and hyperlink for find nearest car park
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.4   Feb 24 2006 12:24:48   AViitanen
//Merge for Enhanced Exposed Services (stream3129)
//
//   Rev 1.3   Jan 03 2006 16:04:00   RGriffith
//Removal of "Keywords" and "Desc" meta tags.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.2   Dec 30 2005 12:06:48   NMoorhouse
//Updated following screen review
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.1   Dec 23 2005 15:43:44   RGriffith
//Changes to footer style and changes to use TDImage
//
//   Rev 1.0   Dec 22 2005 15:31:16   NMoorhouse
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
//   Rev 1.2   Dec 15 2005 17:36:20   NMoorhouse
//Updates to Mini homepages
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.1   Dec 14 2005 18:14:04   NMoorhouse
//Later version of progress pages
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.0   Dec 09 2005 12:20:24   NMoorhouse
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
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.Web.Templates.Maps
{
	/// <summary>
	/// Summary description for Home
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
			pageId = PageId.HomeFindAPlace;
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
			PageTitle = GetResource("HomeFindAPlace.PageTitle");
			
			// Set Page Labels
			literalPageHeading.Text = GetResource("HomeFindAPlace.literalPageHeading");
			
			// Assign URLs to hyperlinks
			IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
			string baseChannel = String.Empty;
			string url = String.Empty;
			if (TDPage.SessionChannelName != null)
				baseChannel = getBookmarkBaseChannelURL(TDPage.SessionChannelName);
						
			// Transfer details for FindAPlace
			PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindMapInput);
			url = baseChannel + pageTransferDetails.PageUrl;
			hyperlinkFindAPlace.NavigateUrl = url;
            hyperLinkFindAPlaceMore.NavigateUrl = url;

			// Transfer details for FindAStationInput (a.k.a. FindNearest)
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindStationInput);
			url = baseChannel + pageTransferDetails.PageUrl;
			hyperlinkFindAStation.NavigateUrl = url + "?NotFindAMode=true";

			// Transfer details for TrafficMaps
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.TrafficMap);
			url = baseChannel + pageTransferDetails.PageUrl;
			hyperlinkTrafficMaps.NavigateUrl = url;

			// Transfer details for NetworkMaps
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.NetworkMaps);
			url = baseChannel + pageTransferDetails.PageUrl;
			hyperlinkNetworkMaps.NavigateUrl = url;

			// Assign images to image controls
			imageFindAPlace.ImageUrl = GetResource("HomeFindAPlace.imageFindAPlace.ImageUrl");
            imageFindAStation.ImageUrl = GetResource("HomeFindAPlace.imageFindAStation.ImageUrl");
			imageTrafficMaps.ImageUrl = GetResource("HomeFindAPlace.imageTrafficMaps.ImageUrl");
			imageNetworkMaps.ImageUrl = GetResource("HomeFindAPlace.imageNetworkMaps.ImageUrl");

			// Setup gif resource for images (1 invisible image for all skip links)
			string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			imageSideNavigationSkipLink1.ImageUrl = skipLinkImageUrl;
			imageFindAPlaceSkipLink.ImageUrl = skipLinkImageUrl;
			imageFindAStationSkipLink.ImageUrl = skipLinkImageUrl;
			imageTrafficMapsSkipLink.ImageUrl = skipLinkImageUrl;
			imageNetworkMapsSkipLink.ImageUrl = skipLinkImageUrl;

			// Assign alt text to image controls
			imageFindAPlace.AlternateText = " ";
			imageFindAStation.AlternateText = " ";
			imageTrafficMaps.AlternateText = " ";
			imageNetworkMaps.AlternateText = " ";

			imageSideNavigationSkipLink1.AlternateText = GetResource("HomeDefault.imageSideNavigationSkipLink1.AlternateText");
			imageFindAPlaceSkipLink.AlternateText = GetResource("HomeFindAPlace.imageFindAPlaceSkipLink.AlternateText");
			imageFindAStationSkipLink.AlternateText = GetResource("HomeFindAPlace.imageFindAStationSkipLink.AlternateText");
			imageTrafficMapsSkipLink.AlternateText = GetResource("HomeFindAPlace.imageTrafficMapsSkipLink.AlternateText");
			imageNetworkMapsSkipLink.AlternateText = GetResource("HomeFindAPlace.imageNetworkMapsSkipLink.AlternateText");

			hyperlinkFindAPlace.ToolTip = GetResource("HomeFindAPlace.imageFindAPlace.AlternateText");
			hyperlinkFindAStation.ToolTip = GetResource("HomeFindAPlace.imageFindAStation.AlternateText");
			hyperlinkTrafficMaps.ToolTip = GetResource("HomeFindAPlace.imageTrafficMaps.AlternateText");
			hyperlinkNetworkMaps.ToolTip = GetResource("HomeFindAPlace.imageNetworkMaps.AlternateText");
            hyperLinkFindAPlaceMore.ToolTip = GetResource("HomeFindAPlace.hyperLinkFindAPlaceMore.AlternateText");

            // CCN 0427 Setting visibilities of the links
            hyperlinkFindAPlace.Visible = FindAPlaceAdapter.JourneyPlannerLocationMapAvailable;
            hyperlinkFindAStation.Visible = FindInputAdapter.FindAStationAvailable; 
            hyperlinkTrafficMaps.Visible = FindAPlaceAdapter.TrafficMapsAvailable; 
            hyperlinkNetworkMaps.Visible = FindAPlaceAdapter.NetworkMapsAvailable; 

			// Assign text to labels

            labelFindAPlace.Text = GetResource("HomeDefault.labelFindAPlace.Text");
			literalFindAPlace.Text = GetResource("HomeFindAPlace.lblFindAPlace");
			literalFindAStation.Text = GetResource("HomeFindAPlace.lblFindAStation");
			literalTrafficMaps.Text = GetResource("HomeFindAPlace.lblTrafficMaps");
			literalNetworkMaps.Text = GetResource("HomeFindAPlace.lblNetworkMaps");
            hyperLinkFindAPlaceMore.Text = GetResource("HomeDefault.labelMore.Text");
            // Populate the left hand navigation menu
			expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuFindAPlace);
			// Populate the right hand related links
			expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextHomeMaps);
            expandableMenuControl.AddExpandedCategory("Related links");

			// Set up client link for bookmark on expandable menu
			clientLink.BookmarkTitle = GetResource("HomeFindAPlace.clientLink.BookmarkTitle");
			clientLink.LinkText = GetResource("HomeFindAPlace.clientLink.LinkText");

			// Determine url to save as bookmark
			string leftPartOfUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.HomeFindAPlace);
			url = leftPartOfUrl + baseChannel + pageTransferDetails.PageUrl;
			clientLink.BookmarkUrl = url;

			SetupCarParkingControls();
		}

		/// <summary>
		/// Checks the carparking available property of the database
		/// and sets car parking page transfer details, images, 
		/// text, skip links and hyperlink values if availability is true.
		/// </summary>
		private void SetupCarParkingControls()
		{
			
			if(FindCarParkHelper.CarParkingAvailable)
			{
				IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
				string baseChannel = string.Empty;

				if (TDPage.SessionChannelName != null)
					baseChannel = getBaseChannelURL(TDPage.SessionChannelName);
				
				PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindCarParkInput);
				string url = baseChannel + pageTransferDetails.PageUrl;

                hyperlinkFindCarPark.NavigateUrl = url + "?NotFindAMode=true&FindNearest=true";
				hyperlinkFindCarPark.ToolTip = GetResource("HomeFindAPlace.imageFindCarPark.AlternateText");
                imageFindCarPark.ImageUrl = GetResource("HomeFindAPlace.imageFindCarPark.ImageUrl");
				imageFindCarPark.AlternateText = " ";
				
				imageFindCarParkSkipLink.ImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
				imageFindCarParkSkipLink.AlternateText = GetResource("HomeFindAPlace.imageFindCarParkSkipLink.AlternateText");

				literalFindCarPark.Text = GetResource("HomePlanAJourney.lblFindCarPark");
			}
			else 
			{
				hyperlinkFindCarPark.Visible = false;
				imageFindCarPark.Visible = false;
				literalFindCarPark.Visible = false;
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
