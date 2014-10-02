// *********************************************** 
// NAME                 : Home(PlanAJourney).aspx.cs 
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 07/12/2005
// DESCRIPTION			: Webform containing the Mini-
//                        Homepage for PlanAJourney.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/Home.aspx.cs-arc  $
//
//   Rev 1.9   Aug 28 2012 10:21:42   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.8   Mar 24 2011 11:43:08   PScott
//Mini homepage Altext changes
//Resolution for 5683: 10.16 Fixes from initial testing on SITest
//
//   Rev 1.7   Feb 11 2011 10:51:44   PScott
//IR 5674  CCN664 Updates to mini homepages
//Resolution for 5674: Updates to mini home-pages
//
//   Rev 1.6   Aug 28 2009 14:28:38   mmodi
//Reinserted Find a cycle icon
//Resolution for 5313: Cycle Planner - Left hand link, Icon, and Soft content
//
//   Rev 1.5   Nov 10 2008 12:32:20   mturner
//Updated to remove Find a cycle icon from this page
//
//   Rev 1.4   Oct 14 2008 14:27:38   mmodi
//Manual merge for stream5014
//
//   Rev 1.3   Jul 01 2008 15:21:50   rbroddle
//Set imageFindCarParkSkipLink.AlternateText and  imageFindBusSkipLink.AlternateText 
//Resolution for 5016: WAI WCAG level A compliance faults - Missing Alt text
//
//   Rev 1.2.1.2   Sep 22 2008 14:58:10   mmodi
//Added Cycle journey icon
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.1   Sep 15 2008 11:11:12   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.0   Aug 22 2008 10:36:00   mmodi
//Updated for cycle planner
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:24:42   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Feb 25 2008 13:00:00 mmodi
//CCN0433, added Find a train cost link
//
//  Rev DevFactory Feb 15 2008 09:13:00 apatel
//  CCN 427 - Changes made to switch on or off various functional areas on page depending on
//  property set for that area.
//
//
//DEVFACTORY   JAN 08 2008 12:30:00   psheldrake
//Changes to conform to CCN426 : Car Park Usability Updates
//
//   Rev 1.0   Nov 08 2007 13:32:06   mturner
//Initial revision.
//
//   Rev 1.9   Apr 26 2006 12:15:08   RPhilpott
//Manual merge of stream 35
//
//   Rev 1.8   Apr 05 2006 15:18:18   esevern
//Manual merge of stream0030
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.7   Mar 31 2006 09:34:04   tolomolaiye
//Ammended to use the new park and ride image
//
//   Rev 1.6   Mar 27 2006 14:24:56   rgreenwood
//IR3646 Re-added FindAFare switch to disable link on homepage when functionality unavailable (see IR3349)
//Resolution for 3646: Find a Fare switch
//
//   Rev 1.5   Mar 23 2006 17:54:24   build
//Automatically merged from branch for stream0025
//
//   Rev 1.4.1.2   Mar 21 2006 12:20:42   halkatib
//Applied changes resluting from code review
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.4.1.1   Mar 14 2006 10:33:04   halkatib
//Changes made for park and ride phase 2
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.4.1.0   Mar 10 2006 16:44:30   halkatib
//Changes made for park and ride phase 2
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.4   Feb 24 2006 12:19:18   AViitanen
//Merge for Enhanced Exposed Services (stream3129)
//
//   Rev 1.3   Jan 03 2006 16:03:00   RGriffith
//Removal of "Keywords" and "Desc" meta tags.
//
//   Rev 1.2   Dec 30 2005 12:06:46   NMoorhouse
//Updated following screen review
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.1   Dec 23 2005 15:39:12   RGriffith
//Changes to footer style and changes to use TDImage
//
//   Rev 1.0   Dec 22 2005 15:29:56   NMoorhouse
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
//   Rev 1.2   Dec 15 2005 17:36:22   NMoorhouse
//Updates to Mini homepages
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.1   Dec 14 2005 18:14:08   NMoorhouse
//Later version of progress pages
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.0   Dec 09 2005 12:20:26   NMoorhouse
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

namespace TransportDirect.UserPortal.Web.Templates.JourneyPlanning
{
	/// <summary>
	/// Summary description for HomeFindAJourney.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class Home : TDPage
	{
		protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl relatedLinksControl;				
		protected TransportDirect.UserPortal.Web.Controls.ClientLinkControl clientLink;
        protected TransportDirect.UserPortal.Web.Controls.PlanAJourney planAJourneyControl;

		#region Constructor
		/// <summary>
		/// Constructor - sets the page id.
		/// </summary>
		public Home() : base()
		{
			pageId = PageId.HomePlanAJourney;
		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			PopulatePage();

			if (FindInputAdapter.FindAFareAvailable)
			{
				findAFareQuickLink.Visible = true;
				FindFareSkipLink.Visible = true;
			}
			else
			{
				findAFareQuickLink.Visible = false;
				FindFareSkipLink.Visible = false;
			}
            //Added for white labelling:
            ConfigureLeftMenu("HomePlanAJourney.clientLink.BookmarkTitle", "HomePlanAJourney.clientLink.LinkText", clientLink, expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextHomeJourneyPlanning);
            expandableMenuControl.AddExpandedCategory("Related links");
		}

		/// <summary>
		/// Assigns soft content to the page controls
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PopulatePage()
		{


            if (!FindInputAdapter.DoorToDoorJourneyAvailable)
            {
                planAJourneyControl.Visible = false;
            }

			// Set up <Head> tag information
			PageTitle = GetResource("HomePlanAJourney.PageTitle");
            
			
			// Set Page Labels
			literalPageHeading.Text = GetResource("HomePlanAJourney.literalPageHeading");
			//labelRelatedLinks.Text = GetResource("HomeDefault.labelRelatedLinks.Text");
			
			// Assign URLs to hyperlinks
			IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
			string baseChannel = String.Empty;
			string url = String.Empty;
			if (TDPage.SessionChannelName != null)
				baseChannel = getBaseChannelURL(TDPage.SessionChannelName);
						
			// Transfer details for DoorToDoor
            PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.JourneyPlannerInput);
			url = baseChannel + pageTransferDetails.PageUrl;
			hyperlinkDoorToDoor.NavigateUrl = url + "?DoorToDoor=true";
           
            // Transfer details for Plan a journey More... link
            pageTransferDetails = pageController.GetPageTransferDetails(PageId.JourneyPlannerInput);
            url = baseChannel + pageTransferDetails.PageUrl;
            hyperLinkPlanAJourneyMore.NavigateUrl = url + "?DoorToDoor=true";
          
			// Transfer details for FindTrainInput
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindTrainInput);
			url = baseChannel + pageTransferDetails.PageUrl;
			hyperlinkFindTrain.NavigateUrl = url;

			// Transfer details for FindFlightInput
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindFlightInput);
			url = baseChannel + pageTransferDetails.PageUrl;
			hyperlinkFindFlight.NavigateUrl = url;

			// Transfer details for FindCarInput
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindCarInput);
			url = baseChannel + pageTransferDetails.PageUrl;
			hyperlinkFindCar.NavigateUrl = url;

			// Transfer details for FindCoachInput
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindCoachInput);
			url = baseChannel + pageTransferDetails.PageUrl;
			hyperlinkFindCoach.NavigateUrl = url;

            // Transfer details for FindTrainCostInput
            pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindTrainCostInput);
            url = baseChannel + pageTransferDetails.PageUrl;
            hyperlinkFindTrainCost.NavigateUrl = url;

			// Transfer details for FindFareInput
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindFareInput);
			url = baseChannel + pageTransferDetails.PageUrl;
			hyperlinkFindAFare.NavigateUrl = url;

			// Transfer details for FindTrunkInput
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindTrunkInput);
			url = baseChannel + pageTransferDetails.PageUrl;
			hyperlinkCompareCityToCity.NavigateUrl = url + "?ClassicMode=true";

			// Transfer details for VisitPlannerInput
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.VisitPlannerInput);
			url = baseChannel + pageTransferDetails.PageUrl;
			hyperlinkVisitPlanner.NavigateUrl = url;

			// Transfer details for Park and Ride
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.ParkAndRideInput);
			url = baseChannel + pageTransferDetails.PageUrl;
			hyperlinkParkAndRide.NavigateUrl = url;

			//Transfer details for Find a Bus
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindBusInput);
			url = baseChannel + pageTransferDetails.PageUrl;
			hyperlinkFindBus.NavigateUrl = url;

            // Transfer details for FindCarPark
            pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindCarParkInput);
            url = baseChannel + pageTransferDetails.PageUrl + "?DriveFromTo=true";
            hyperlinkFindCarPark.NavigateUrl = url;

            // Transfer details for Find a Cycle Journey
            pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindCycleInput);
            url = baseChannel + pageTransferDetails.PageUrl;
            hyperlinkFindCycle.NavigateUrl = url;

			// Assign images to image controls
			imageDoorToDoor.ImageUrl = GetResource("HomeDefault.imageDoorToDoor.ImageUrl");
			imageFindTrain.ImageUrl = GetResource("HomeDefault.imageFindTrain.ImageUrl");
			imageFindFlight.ImageUrl = GetResource("HomeDefault.imageFindFlight.ImageUrl");
			imageFindCar.ImageUrl = GetResource("HomeDefault.imageFindCar.ImageUrl");
            imageFindCarPark.ImageUrl = GetResource("HomeDefault.imageFindACarPark.ImageURL");
			imageFindCoach.ImageUrl = GetResource("HomeDefault.imageFindCoach.ImageUrl");
            imageFindTrainCost.ImageUrl = GetResource("HomeDefault.imageFindTrainCost.ImageUrl");
			imageFindAFare.ImageUrl = GetResource("HomeDefault.imageFindAFare.ImageUrl");
			imageCompareCityToCity.ImageUrl = GetResource("HomeDefault.imageCompareCityToCity.ImageUrl");
			imageVisitPlanner.ImageUrl = GetResource("HomePlanAJourney.imageVisitPlanner.ImageUrl");
			imageParkAndRide.ImageUrl = GetResource("HomePlanAJourney.imageParkAndRide.ImageUrl");
			imageFindBus.ImageUrl = GetResource("HomePlanAJourney.imageFindBus.ImageUrl");
            imageFindCycle.ImageUrl = GetResource("HomeDefault.imageFindCycle.ImageUrl");

			// Setup gif resource for images (1 invisible image for all skip links)
			string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			imageSideNavigationSkipLink1.ImageUrl = skipLinkImageUrl;
			imageDoorToDoorSkipLink.ImageUrl = skipLinkImageUrl;
			imageFindTrainSkipLink.ImageUrl = skipLinkImageUrl;
			imageFindFlightSkipLink.ImageUrl = skipLinkImageUrl;
			imageFindCarSkipLink.ImageUrl = skipLinkImageUrl;
			imageFindCoachSkipLink.ImageUrl = skipLinkImageUrl;
            imageFindTrainCostSkipLink.ImageUrl = skipLinkImageUrl;
			imageFindAFareSkipLink.ImageUrl = skipLinkImageUrl;
			imageCompareCityToCitySkipLink.ImageUrl = skipLinkImageUrl;
			imageVisitPlannerSkipLink.ImageUrl = skipLinkImageUrl;
			imageParkAndRideSkipLink.ImageUrl = skipLinkImageUrl;
			imageFindBusSkipLink.ImageUrl = skipLinkImageUrl;
            imageFindCarParkSkipLink.ImageUrl = skipLinkImageUrl;
            imageFindCycleSkipLink.ImageUrl = skipLinkImageUrl;

			// Assign alt text to image controls
			imageDoorToDoor.AlternateText = GetResource("HomeDefault.imageDoorToDoor.AlternateText");
			imageFindTrain.AlternateText = GetResource("HomeDefault.imageFindTrain.AlternateText");
			imageFindFlight.AlternateText = GetResource("HomeDefault.imageFindFlight.AlternateText");
			imageFindCar.AlternateText = GetResource("HomeDefault.imageFindCar.AlternateText");
            imageFindCarPark.AlternateText = GetResource("HomeDefault.imageFindCarPark.AlternateText");
			imageFindCoach.AlternateText = GetResource("HomeDefault.imageFindCoach.AlternateText");
            imageFindTrainCost.AlternateText = GetResource("HomeDefault.imageFindTrainCost.AlternateText");
			imageFindAFare.AlternateText = GetResource("HomeDefault.imageFindAFare.AlternateText");
			imageVisitPlanner.AlternateText = GetResource("HomePlanAJourney.imageVisitPlanner.AlternateText");
			imageCompareCityToCity.AlternateText = GetResource("HomeDefault.imageCompareCityToCity.AlternateText");
			imageParkAndRide.AlternateText = GetResource("HomePlanAJourney.imageParkAndRide.AlternateText");
			imageFindBus.AlternateText = GetResource("HomeDefault.imageFindBus.AlternateText");
            imageFindCycle.AlternateText = GetResource("HomeDefault.imageFindCycle.AlternateText");

			imageSideNavigationSkipLink1.AlternateText = GetResource("HomeDefault.imageSideNavigationSkipLink1.AlternateText");
			imageDoorToDoorSkipLink.AlternateText = GetResource("HomePlanAJourney.imageDoorToDoorSkipLink.AlternateText");
			imageFindTrainSkipLink.AlternateText = GetResource("HomePlanAJourney.imageFindTrainSkipLink.AlternateText");
			imageFindFlightSkipLink.AlternateText = GetResource("HomePlanAJourney.imageFindFlightSkipLink.AlternateText");
			imageFindCarSkipLink.AlternateText = GetResource("HomePlanAJourney.imageFindCarSkipLink.AlternateText");
			imageFindCoachSkipLink.AlternateText = GetResource("HomePlanAJourney.imageFindCoachSkipLink.AlternateText");
            imageFindTrainCostSkipLink.AlternateText = GetResource("HomePlanAJourney.imageFindTrainCostSkipLink.AlternateText");
			imageFindAFareSkipLink.AlternateText = GetResource("HomePlanAJourney.imageFindAFareSkipLink.AlternateText");
			imageCompareCityToCitySkipLink.AlternateText = GetResource("HomePlanAJourney.imageCompareCityToCitySkipLink.AlternateText");
			imageVisitPlannerSkipLink.AlternateText = GetResource("HomePlanAJourney.imageVisitPlannerSkipLink.AlternateText");
			imageParkAndRideSkipLink.AlternateText = GetResource("HomePlanAJourney.imageParkAndRideSkipLink.AlternateText");
            imageFindBusSkipLink.AlternateText = GetResource("HomePlanAJourney.imageFindBusSkipLink.AlternateText");
            imageFindCarParkSkipLink.AlternateText = GetResource("HomePlanAJourney.imageFindCarParkSkipLink.AlternateText");
            imageFindCycleSkipLink.AlternateText = GetResource("HomePlanAJourney.imageFindCycleSkipLink.AlternateText");

			hyperlinkDoorToDoor.ToolTip = GetResource("HomeDefault.imageDoorToDoor.AlternateText");
			hyperlinkFindTrain.ToolTip = GetResource("HomeDefault.imageFindTrain.AlternateText");
			hyperlinkFindFlight.ToolTip = GetResource("HomeDefault.imageFindFlight.AlternateText");
			hyperlinkFindCar.ToolTip = GetResource("HomeDefault.imageFindCar.AlternateText");
            hyperlinkFindCarPark.ToolTip = GetResource("HomeDefault.imageFindCarPark.AlternateText");
			hyperlinkFindCoach.ToolTip = GetResource("HomeDefault.imageFindCoach.AlternateText");
            hyperlinkFindTrainCost.ToolTip = GetResource("HomeDefault.imageFindTrainCost.AlternateText");
			hyperlinkFindAFare.ToolTip = GetResource("HomeDefault.imageFindAFare.AlternateText");
			hyperlinkCompareCityToCity.ToolTip = GetResource("HomeDefault.imageCompareCityToCity.AlternateText");
			hyperlinkVisitPlanner.ToolTip = GetResource("HomePlanAJourney.imageVisitPlanner.AlternateText");
			hyperlinkParkAndRide.ToolTip = GetResource("HomePlanAJourney.imageParkAndRide.AlternateText");
			hyperlinkFindBus.ToolTip = GetResource("HomeDefault.imageFindBus.AlternateText");
            hyperlinkFindCycle.ToolTip = GetResource("HomeDefault.imageFindCycle.AlternateText");
            hyperLinkPlanAJourneyMore.ToolTip = GetResource("HomePlanAJourney.hyperLinkPlanAJourneyMore.AlternateText");

			// Assign text to labels
            labelPlanAJourney.Text = GetResource("HomeDefault.labelPlanAJourney.Text");
			literalDoorToDoor.Text = GetResource("HomePlanAJourney.lblDoorToDoor");
			literalFindTrain.Text = GetResource("HomePlanAJourney.lblTrain");
			literalFindFlight.Text = GetResource("HomePlanAJourney.lblFlight");
			literalFindCar.Text = GetResource("HomePlanAJourney.lblCar");
            literalFindCarPark.Text = GetResource("HomePlanAJourney.lblCarPark");
			literalFindCoach.Text = GetResource("HomePlanAJourney.lblCoach");
            literalFindTrainCost.Text = GetResource("HomePlanAJourney.lblTrainCost");
			literalFindAFare.Text = GetResource("HomePlanAJourney.lblFindAFare");
			literalCompareCityToCity.Text = GetResource("HomePlanAJourney.lblCitytoCity");
			literalVisitPlanner.Text = GetResource("HomePlanAJourney.lblVisitPlanner");
			literalParkAndRide.Text = GetResource("HomePlanAJourney.lblParkAndRide");
			literalFindBus.Text = GetResource("HomePlanAJourney.lblFindBus");
            literalFindCycle.Text = GetResource("HomePlanAJourney.lblFindCycle");
            hyperLinkPlanAJourneyMore.Text = GetResource("HomeDefault.labelMore.Text");

            // CCN 0427 Setting visibility of the hyperlinks
            hyperlinkDoorToDoor.Visible = FindInputAdapter.DoorToDoorJourneyAvailable;
            hyperlinkFindTrain.Visible = FindInputAdapter.FindATrainAvailable;
            hyperlinkFindFlight.Visible = FindInputAdapter.FindAFlightAvailable;
            hyperlinkFindCar.Visible = FindInputAdapter.FindACarAvailable;
            hyperlinkFindCarPark.Visible = FindCarParkHelper.CarParkingAvailable;
            hyperlinkFindCoach.Visible = FindInputAdapter.FindACoachAvailable;
            hyperlinkFindTrainCost.Visible = FindInputAdapter.FindATrainCostAvailable;
            hyperlinkFindAFare.Visible = FindInputAdapter.FindAFareAvailable;
            hyperlinkCompareCityToCity.Visible = FindInputAdapter.CompareCityToCityJourneyAvailable;
            hyperlinkVisitPlanner.Visible = FindInputAdapter.PlanADayTripAvailable;
            hyperlinkParkAndRide.Visible = FindInputAdapter.PlanToParkAndRideAvailable;
            hyperlinkFindBus.Visible = FindInputAdapter.FindABusAvailable;
            hyperlinkFindCycle.Visible = FindInputAdapter.FindACycleAvailable;
            hyperLinkPlanAJourneyMore.Visible = false; // Hide on this mini-homepage as the plan a journey control has a more button

			//expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenu);
			// Populate the right hand related links
            // Populate the left hand navigation menu
			//relatedLinksControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePlanAJourney);

			// Set up client link for bookmark on expandable menu
			clientLink.BookmarkTitle = GetResource("HomePlanAJourney.clientLink.BookmarkTitle");
			clientLink.LinkText = GetResource("HomePlanAJourney.clientLink.LinkText");

			// Determine url to save as bookmark
			string leftPartOfUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
			pageTransferDetails = pageController.GetPageTransferDetails(PageId.HomePlanAJourney);
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
		#endregion

	}
}
