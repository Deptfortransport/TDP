// *********************************************** 
// NAME                 : Home.aspx.cs 
// AUTHOR               : Andrew Sinclair 
// DATE CREATED         : 17/07/2003 
// DESCRIPTION			: Webform containing the template
// for the HomePage.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Home.aspx.cs-arc  $
//
//   Rev 1.7   Aug 28 2009 15:47:30   mmodi
//Reinserted Cycle icon, and updated size of other icons
//Resolution for 5313: Cycle Planner - Left hand link, Icon, and Soft content
//
//   Rev 1.6   Feb 06 2009 12:01:16   apatel
//Search Engine Optimisation Changes
//Resolution for 5229: Search Engin Optimisation Changes -  CCN624
//
//   Rev 1.5   Feb 02 2009 12:28:50   apatel
//Search Engine Optimisation changes
//Resolution for 5229: Search Engin Optimisation Changes -  CCN624
//
//   Rev 1.4   Nov 10 2008 12:11:38   mturner
//Changed to remove FindCylceInput Icon and Link from Icon bar
//
//   Rev 1.3   Oct 13 2008 16:40:40   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.2   Sep 22 2008 14:58:54   mmodi
//Updated cycle journey icon
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.1   Sep 15 2008 10:44:52   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.0   Aug 01 2008 16:27:56   mmodi
//Added Cycle planner link
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:23:58   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 21 2008 13:00:00   mmodi
//Added travel news javascript file
//
//   Rev DevFactory   Feb 15 2008 13:00:00   mmodi
//Updated Find car park url
//
//DEVFACTORY   JAN 08 2008 12:30:00   psheldrake
//Changes to conform to CCN426 : Car Park Usability Updates
//
//  Rev Devfactory Jan 11 2008 14:08:15 apatel
//  Added SetupResourcesInClientScript() method to register resources used by
//  travel news headline control popup javascript. Modified the Page Load method
//  to call this method on page load.
//
//   Rev 1.0   Nov 08 2007 13:29:48   mturner
//Initial revision.
//
//   Rev 1.35   Sep 07 2007 15:14:00   asinclair
//Changed City to City link to Find a Car Park link
//
//   Rev 1.34   Jul 11 2006 11:21:56   PScott
//4318685 - Amend Spotlight tags
//
//   Rev 1.33   Apr 26 2006 12:15:04   RPhilpott
//Manual merge of stream 35
//
//   Rev 1.32   Mar 27 2006 14:20:56   rgreenwood
//IR3646 Re-added code to hide FindAFare link if functionality is not available (see IR3349)
//Resolution for 3646: Find a Fare switch
//
//   Rev 1.31   Feb 24 2006 10:00:18   AViitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.30   Feb 14 2006 17:35:08   RPhilpott
//Fixed following 3180 merger
//
//   Rev 1.29   Feb 10 2006 10:48:04   jmcallister
//Manual Merge of Homepage 2. IR3180
//
//   Rev 1.28   Dec 19 2005 15:10:22   jgeorge
//Removed 1.26 changes as these break Find a Fare. No code should set ITDSessionManager.JourneyParameters directly - it should be done through InitialiseJourneyParametersPageStates.
//Resolution for 3370: Navigation Error in Find a Fare
//
//   Rev 1.27   Dec 08 2005 14:42:32   mtillett
//Add switch to allow find a fare quick link to be shown/hidden based on the Available database property
//Resolution for 3349: Home page switch to turn off find a fare
//
//   Rev 1.26   Dec 05 2005 18:18:36   ralonso
//Problem fixed with door to door control on the home page.
//Resolution for 3269: UEE - Homepage: date field on journey planner not refreshed when returning to homepage
//Resolution for 3283: UEE Hompage:  Incorrect default time on the Homepage miniplanner
//
//   Rev 1.25   Nov 24 2005 10:24:44   pcross
//Post code review changes
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.24   Nov 17 2005 12:16:24   pcross
//Manual merge of stream2880
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.22.1.5   Nov 15 2005 16:51:22   pcross
//Removing unused hyperlink
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.22.1.4   Nov 14 2005 13:11:54   pcross
//Fix to ensure link from Plan a Journey More... always goes to Door to Door (not a FindA page)
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.22.1.3   Nov 11 2005 14:20:22   pcross
//Alt text for Firefox
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.22.1.2   Nov 11 2005 11:49:48   pcross
//Updates after FXCop
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.22.1.1   Nov 10 2005 17:54:00   pcross
//Added meta tags, skip links, updated images, and other minor
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.22.1.0   Nov 07 2005 19:05:04   pcross
//Merged latest stream2880 with latest trunk to create new stream2880.
//Note that in this case no actual code from trunk changes have been applied as the functionality in stream2880 completely supersedes the previous homepage.
//A new stream2880 branch has been created from the trunk however to be consistent with the rest of the merge exercise.
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.21.2.3   Nov 07 2005 16:18:24   pcross
//Replaced html where it will be supplied by CMS entries
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.21.2.2   Nov 07 2005 15:55:44   RGriffith
//PlanAJourneyControl added to Home Page
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.21.2.1   Nov 07 2005 10:34:32   pcross
//Homepage update. Not yet complete but file needed by someone Sanjeev.
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.21.2.0   Oct 21 2005 15:14:18   pcross
//New homepage design
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.21.1.0   Oct 12 2005 10:23:44   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.22   Oct 11 2005 15:55:00   RGriffith
//Replacing the image button with HTML button
//
//   Rev 1.21   Sep 29 2005 12:52:28   build
//Automatically merged from branch for stream2673
//
//   Rev 1.20.1.0   Sep 13 2005 16:34:22   NMoorhouse
//DN079 UEE, TD092 Login and Register enhancements
//Resolution for 2757: DEL 8 stream: Login functionality available on every page
//
//   Rev 1.20   Jul 26 2005 15:57:44   jgeorge
//Changed layout when Find A Fare is turned off.
//Resolution for 2623: Quick planner icons on homepage
//
//   Rev 1.19   Mar 24 2005 14:20:06   rgeraghty
//Temporary switch implemented for costbasedsearching
//Resolution for 1972: Front End Switch for Find A Fare/City to City Cost Based Search
//
//   Rev 1.18   Feb 25 2005 10:37:26   PNorell
//Event wireup had dissapeared. Corrected.
//
//   Rev 1.17   Feb 23 2005 10:02:02   bflenk
//Removed use of text in properties table for disclaimer and advertising message properties database - CMS HTML Placeholders used for information instead.
//
//   Rev 1.16   Nov 08 2004 16:17:18   Schand
//// Added code for SeasonalNoticeboard link
//
//   Rev 1.15   Nov 01 2004 15:32:58   passuied
//Re-enabled city to city link + added setOneUseKey with Classic Mode when clicked on City-to-city image button
//
//   Rev 1.14   Oct 05 2004 14:20:04   passuied
//removed link to FindTrunkInput
//
//   Rev 1.13   Sep 29 2004 17:10:50   jmorrissey
//Added ImageButtonLiveTravelDetails_Click event. Also removed some unnecessary code that was retrieving langStrings resources in the PageLoad. This is repeating code that runs in LanguageHandler.cs.
//Resolution for 1642: Amendment to live travel information link on home page
//
//   Rev 1.12   Sep 16 2004 15:31:52   asinclair
//FindTrain Click event not Initialized correctly in last check-in
//
//   Rev 1.11   Sep 16 2004 10:14:50   asinclair
//Added new label for QA changes
//
//   Rev 1.10   Sep 03 2004 14:27:44   asinclair
//Updated for new Del 6.1 design
//
//   Rev 1.9   May 26 2004 10:22:14   jgeorge
//IR954 fix
//
//   Rev 1.8   Apr 06 2004 18:02:38   CHosegood
//Removed the setting of the disclaimer message to be out of the postback.
//
//   Rev 1.7   Apr 05 2004 10:27:10   asinclair
//Disclaimer Icon now only displayed if message in database
//
//   Rev 1.6   Mar 23 2004 15:00:08   AWindley
//DEL5.2 Updates for Alt text
//
//   Rev 1.5   Mar 03 2004 15:59:16   asinclair
//lblDisclaimer.text value is now pulled from the database to allow easier updating
//
//   Rev 1.4   Feb 26 2004 15:56:16   esevern
//DEL 5.2 - added disclaimer notice 
//
//   Rev 1.3   Jan 09 2004 14:11:32   asinclair
//Added a label to tell Welsh users that the live news is only in English.  See IR 328
//
//   Rev 1.2   Dec 01 2003 11:08:12   asinclair
//Made the Live travel news heading a label so that the text can be changed for Welsh
//
//   Rev 1.1   Nov 20 2003 15:40:48   asinclair
//added to comments to show file name change
//
//   Rev 1.0   Nov 20 2003 15:22:36   asinclair
//Initial Revision - Renamed 'HomeDefault' 'Home' so that it would be the same as the MCMS
//posting that uses this template.  Beacuse the file has been renamed the Revision numbers
//have become messed up, see below for the details of changes made before the name change
//
//   Rev 1.19   Nov 18 2003 10:13:36   asinclair
//Removed the Welsh header from the page as it is no longer needed
//
//   Rev 1.18   Nov 16 2003 12:49:32   hahad
//Changed Title Tag so that it uses asp:literal
//
//   Rev 1.17   Nov 07 2003 10:24:16   JMorrissey
//Updated more link's navigate url and set its div html class "textsevenb " 
//
//   Rev 1.16   Nov 04 2003 16:03:44   kcheung
//Fixed ImageButtonMap icon click so that it writes the correct stuff to session before writing the transition event.
//
//   Rev 1.15   Oct 13 2003 10:34:52   JMorrissey
//Updated method used to populate the travel news headlines box.
//
//   Rev 1.14   Oct 10 2003 16:12:54   JMorrissey
//LiveTeavelNews headlines now returned as a formatted string 
//
//Footer control and MCMS console added back onto page...oops!
//
//   Rev 1.13   Oct 09 2003 17:56:42   JHaydock
//Formatted live travel news display
//
//   Rev 1.12   Oct 09 2003 14:56:54   JMorrissey
//Added Travel News Headlines text box
//
//   Rev 1.11   Sep 30 2003 20:05:46   asinclair
//Removed Welsh header as no longer needed
//
//   Rev 1.10   Sep 27 2003 11:29:00   asinclair
//Updated page load event to switch between english and welsh headers
//
//   Rev 1.9   Sep 27 2003 11:14:14   asinclair
//changed page load event
//
//   Rev 1.8   Sep 26 2003 14:02:20   asinclair
//Updated HTML
//
//   Rev 1.7   Sep 25 2003 19:39:44   asinclair
//Updated Image buttons to allow for click event
//
//   Rev 1.6   Sep 25 2003 17:48:50   asinclair
//Fixed problem of links at top of page.
//
//   Rev 1.5   Sep 25 2003 16:59:18   asinclair
//Updated placeholders
//
//   Rev 1.4   Sep 25 2003 10:25:04   asinclair
//Fixed problem of gaps between image buttons
//
//   Rev 1.3   Sep 22 2003 18:28:20   asinclair
//Added Page Id
//Resolution for 2: Session Manager - Deferred storage
//
//   Rev 1.2   Sep 12 2003 16:19:48   asinclair
//Updated Templates

	#region using

	using System;
	using System.Web;
	using TransportDirect.Common;
	using TransportDirect.Common.PropertyService.Properties;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.Web;
    using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.ScreenFlow;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.Common.Logging;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.LocationService;
	using TransportDirect.UserPortal.Resource;
	using Logger = System.Diagnostics.Trace;
	using System.Globalization;
	using System.Text;

    #endregion 

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for HomeDefault.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class HomeDefault : TDPage
    {
        #region control declarations

        protected TransportDirect.UserPortal.Web.Controls.PlanAJourney planAJourneyControl;
		protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl expandableMenuControl;
		protected TransportDirect.UserPortal.Web.Controls.ClientLinkControl clientLink;
		protected TransportDirect.UserPortal.Web.Controls.RoundedCornerControl mainbit;
		
		#endregion

		#region constructor

		/// <summary>
		/// Homepage class constructor. Sets page ID.
		/// </summary>
		public HomeDefault() : base()
		{
			pageId = PageId.Home;
		}

		#endregion constructor
		
		#region page load

		/// <summary>
		/// Homepage load routine. Sets default values for page.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        /// 
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Clear the current cache.
			ClearCacheHelper helper = new ClearCacheHelper();
			helper.ClearCache();
			
			SetupMapMode();

			PopulateControls();

			//Calling DataBind ensures that any reference in the html to code in the code-behind file is bound
			DataBind();

			if (FindInputAdapter.FindAFareAvailable)
			{
				findAFareQuickLink.Visible = true;
			}
			else
			{
				findAFareQuickLink.Visible = false;
			}

            //added for white-labelling Related link part of side menu
            relatedLinksControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextHome);
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

		#region private methods

		/// <summary>
		/// Assigns soft content to the page controls
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PopulateControls()
		{
            // Set up <Head> tag information
            PageTitle = GetResource("Homepage.PageTitle");
            literalPageHeading.Text = GetResource("Homepage.literalPageHeading");
            headElementControl.Desc = GetResource("HomeDefault.MetaContent.PageDescription");
            headElementControl.Keywords = GetResource("HomeDefault.MetaContent.Keywords");

            // Populate the left hand navigation menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenu);

            // Assign URLs to hyperlinks
            IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
            string baseChannel = string.Empty;
            string url = string.Empty;
            if (TDPage.SessionChannelName != null)
                baseChannel = getBookmarkBaseChannelURL(TDPage.SessionChannelName);

            PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindTrainInput);

            // Transfer details for FindTrainInput
            url = baseChannel + pageTransferDetails.PageUrl;
            hyperLinkFindTrain.NavigateUrl = url;

            // Transfer details for FindFlightInput
            pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindFlightInput);
            url = baseChannel + pageTransferDetails.PageUrl;
            hyperLinkFindFlight.NavigateUrl = url;

            // Transfer details for FindCarInput
            pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindCarInput);
            url = baseChannel + pageTransferDetails.PageUrl;
            hyperLinkFindCar.NavigateUrl = url;

            // Transfer details for FindCoachInput
            pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindCoachInput);
            url = baseChannel + pageTransferDetails.PageUrl;
            hyperLinkFindCoach.NavigateUrl = url;

            // Transfer details for FindFareInput
            pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindFareInput);
            url = baseChannel + pageTransferDetails.PageUrl;
            hyperLinkFindAFare.NavigateUrl = url;

            // Transfer details for Find Car Park
            pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindCarParkInput);
            url = baseChannel + pageTransferDetails.PageUrl + "?FindNearest=true";
            hyperLinkCarPark.NavigateUrl = url;
            
            // Transfer details for FindCycleInput
            pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindCycleInput);
            url = baseChannel + pageTransferDetails.PageUrl;
            hyperLinkFindCycle.NavigateUrl = url;

            // Transfer details for Plan a journey More... link
            pageTransferDetails = pageController.GetPageTransferDetails(PageId.HomePlanAJourney);
            url = baseChannel + pageTransferDetails.PageUrl;
            hyperLinkPlanAJourneyMore.NavigateUrl = url;
            hyperLinkPlanAJourney.NavigateUrl = url;

            // Transfer details for Live Travel More... link
            // CCN 0421 change the More link to point to travel news page rather than travel news mini home page
            pageTransferDetails = pageController.GetPageTransferDetails(PageId.TravelNews); //HomeTravelInfo);
            url = baseChannel + pageTransferDetails.PageUrl;
            hyperLinkLiveTravelMore.NavigateUrl = url;
            hyperLinkLiveTravel.NavigateUrl = url;

            // Transfer details for Find A Place More... link
            pageTransferDetails = pageController.GetPageTransferDetails(PageId.HomeFindAPlace);
            url = baseChannel + pageTransferDetails.PageUrl;
            hyperLinkFindAPlaceMore.NavigateUrl = url;
            hyperLinkFindAPlace.NavigateUrl = url;

            // Assign images to image controls
            imageFindTrain.ImageUrl = GetResource("HomeDefault.imageFindTrain.ImageUrl");
            imageFindFlight.ImageUrl = GetResource("HomeDefault.imageFindFlight.ImageUrl");
            imageFindCar.ImageUrl = GetResource("HomeDefault.imageFindCar.ImageUrl");
            imageFindCoach.ImageUrl = GetResource("HomeDefault.imageFindCoach.ImageUrl");
            imageFindAFare.ImageUrl = GetResource("HomeDefault.imageFindAFare.ImageUrl");
            imageFindCarPark.ImageUrl = GetResource("HomeDefault.imageFindCarPark.ImageUrl");
            imageFindCycle.ImageUrl = GetResource("HomeDefault.imageFindCycle.ImageUrl");

            // Setup gif resource for images (1 invisible image for all skip links)
            string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
            imageSideNavigationSkipLink1.ImageUrl = skipLinkImageUrl;
            imageQuickPlannersSkipLink1.ImageUrl = skipLinkImageUrl;
            imagePlanAJourneySkipLink1.ImageUrl = skipLinkImageUrl;
            imageFindAPlaceSkipLink1.ImageUrl = skipLinkImageUrl;
            imageTravelNewsSkipLink1.ImageUrl = skipLinkImageUrl;
            imageTipsAndToolsSkipLink1.ImageUrl = skipLinkImageUrl;

            // Assign alt text to image controls
            imageFindTrain.AlternateText = GetResource("HomeDefault.imageFindTrain.AlternateText");
            imageFindFlight.AlternateText = GetResource("HomeDefault.imageFindFlight.AlternateText");
            imageFindCar.AlternateText = GetResource("HomeDefault.imageFindCar.AlternateText");
            imageFindCoach.AlternateText = GetResource("HomeDefault.imageFindCoach.AlternateText");
            imageFindAFare.AlternateText = GetResource("HomeDefault.imageFindAFare.AlternateText");
            imageFindCarPark.AlternateText = GetResource("HomeDefault.imageFindCarPark.AlternateText");
            imageFindCycle.AlternateText = GetResource("HomeDefault.imageFindCycle.AlternateText");

            hyperLinkLiveTravelMore.ToolTip = GetResource("HomeDefault.hyperLinkLiveTravelMore.AlternateText");
            hyperLinkPlanAJourneyMore.ToolTip = GetResource("HomeDefault.hyperLinkPlanAJourneyMore.AlternateText");
            hyperLinkFindAPlaceMore.ToolTip = GetResource("HomeDefault.hyperLinkFindAPlaceMore.AlternateText");

            hyperLinkLiveTravel.ToolTip = GetResource("HomeDefault.hyperLinkLiveTravelMore.AlternateText");
            hyperLinkPlanAJourney.ToolTip = GetResource("HomeDefault.hyperLinkPlanAJourneyMore.AlternateText");
            hyperLinkFindAPlace.ToolTip = GetResource("HomeDefault.hyperLinkFindAPlaceMore.AlternateText");

            imageSideNavigationSkipLink1.AlternateText = GetResource("HomeDefault.imageSideNavigationSkipLink1.AlternateText");
            imageQuickPlannersSkipLink1.AlternateText = GetResource("HomeDefault.imageQuickPlannersSkipLink1.AlternateText");
            imagePlanAJourneySkipLink1.AlternateText = GetResource("HomeDefault.imagePlanAJourneySkipLink1.AlternateText");
            imageFindAPlaceSkipLink1.AlternateText = GetResource("HomeDefault.imageFindAPlaceSkipLink1.AlternateText");
            imageTravelNewsSkipLink1.AlternateText = GetResource("HomeDefault.imageTravelNewsSkipLink1.AlternateText");
            imageTipsAndToolsSkipLink1.AlternateText = GetResource("HomeDefault.imageTipsAndToolsSkipLink1.AlternateText");

            hyperLinkFindTrain.ToolTip = GetResource("HomeDefault.imageFindTrain.AlternateText");
            hyperLinkFindFlight.ToolTip = GetResource("HomeDefault.imageFindFlight.AlternateText");
            hyperLinkFindCar.ToolTip = GetResource("HomeDefault.imageFindCar.AlternateText");
            hyperLinkFindCoach.ToolTip = GetResource("HomeDefault.imageFindCoach.AlternateText");
            hyperLinkFindAFare.ToolTip = GetResource("HomeDefault.imageFindAFare.AlternateText");
            hyperLinkCarPark.ToolTip = GetResource("HomeDefault.imageFindACarPark.AlternateText");
            hyperLinkFindCycle.ToolTip = GetResource("HomeDefault.imageFindCycle.AlternateText");

            // CCN 0427 Setting Visibility of the links at top
            hyperLinkFindTrain.Visible = FindInputAdapter.FindATrainAvailable;
            hyperLinkFindFlight.Visible = FindInputAdapter.FindAFlightAvailable;
            hyperLinkFindCar.Visible = FindInputAdapter.FindACarAvailable;
            hyperLinkFindCoach.Visible = FindInputAdapter.FindACoachAvailable;
            hyperLinkFindAFare.Visible = FindInputAdapter.FindAFareAvailable;
            hyperLinkCarPark.Visible = FindCarParkHelper.CarParkingAvailable;
            hyperLinkFindCycle.Visible = FindInputAdapter.FindACycleAvailable;

            // Assign text to labels
            literalFindTrain.Text = GetResource("HomeDefault.lblTrain");
            literalFindFlight.Text = GetResource("HomeDefault.lblFlight");
            literalFindCar.Text = GetResource("HomeDefault.lblCar");
            literalFindCoach.Text = GetResource("HomeDefault.lblCoach");
            literalFindAFare.Text = GetResource("HomeDefault.lblFindAFare");
            literalFindCarPark.Text = GetResource("HomeDefault.lblFindCarPark");
            literalFindCycle.Text = GetResource("HomeDefault.lblCycle");

            hyperLinkPlanAJourneyMore.Text = GetResource("HomeDefault.labelMore.Text");
            hyperLinkLiveTravelMore.Text = GetResource("HomeDefault.labelMore.Text");
            hyperLinkFindAPlaceMore.Text = GetResource("HomeDefault.labelMore.Text");

            labelPlanAJourney.Text = GetResource("HomeDefault.labelPlanAJourney.Text");
            labelFindAPlace.Text = GetResource("HomeDefault.labelFindAPlace.Text");
            labelLiveTravel.Text = GetResource("HomeDefault.labelLiveTravel.Text");
            labelStatusAt.Text = GetResource("HomeDefault.labelStatusAt.Text");
            labelCurrentTime.Text = GetCurrentDateTimeString();

            // Set up client link for bookmark on expandable menu
            clientLink.BookmarkTitle = GetResource("Home.clientLink.BookmarkTitle");
            clientLink.LinkText = GetResource("Home.clientLink.LinkText");

            // CCN 0427 Set visibility of the 4 functional areas in center of the page 
            // 1. Door-to-Door Journey box
            // 2. live Travel news box
            // 3. Find a place box
            // 4. Tips and tools box
            if (!FindInputAdapter.DoorToDoorJourneyAvailable)
            {
                planAJourneyControl.Visible = false;
                labelPlanAJourney.Visible = false;
                hyperLinkPlanAJourneyMore.Visible = false;
                PlanAJourneyHeader.Visible = false;
                PlanAJourneyDetail.Visible = false;
            }

            if(!FindAPlaceAdapter.JourneyPlannerLocationMapAvailable)
            {
                findAPlaceControl1.Visible = false;
                labelFindAPlace.Visible = false;
                hyperLinkFindAPlaceMore.Visible = false;
                FindAPlaceHeader.Visible = false;
                FindAPlaceDetail.Visible = false;
            }

            if (!TravelNewsHelper.TravelNewsAvailable)
            {
                labelLiveTravel.Visible = false;
                hyperLinkLiveTravelMore.Visible = false;
                travelNewsHeadlines.Visible = false;
                labelStatusAt.Visible = false;
                labelCurrentTime.Visible = false;
                TravelNewsHeader.Visible = false;
                TravelNewsDetail.Visible = false;
            }

            if (!TipsAndToolsHelper.TipsAndToolsAvailable)
            {
                TDTipsHtmlPlaceholderDefinition.Visible = false;
            }


            // Determine url to save as bookmark
            string leftPartOfUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            pageTransferDetails = pageController.GetPageTransferDetails(PageId.Home);
            url = leftPartOfUrl + baseChannel + pageTransferDetails.PageUrl;
            clientLink.BookmarkUrl = url;
		}

		/// <summary>
		/// Outputs meta tags with soft content. Read only.
		/// </summary>
		/// <returns>Meta Tag html strings</returns>
		/// <notes>Can't have meta tags in page and just get content - method is ignored. Therefore we output whole meta tags here.</notes>
		protected string GetSoftContentMetaTags
		{
			get
			{
				StringBuilder sb = new StringBuilder(1000);

				sb.Append("<meta name=\"desc\" content=\"");
				sb.Append(GetResource("HomeDefault.MetaContent.PageDescription"));
				sb.Append("\" />\n");
				sb.Append("<meta name=\"keywords\" content=\"");
				sb.Append(GetResource("HomeDefault.MetaContent.Keywords"));
				sb.Append("\" />");

				return sb.ToString();

			}
		}


		/// <summary>
		/// If Find a place >> More... is called then we want to show a blank Location Map screen.
		/// Reset the InputPageState variables that control this so that if we have, earlier in the session,
		/// used a different map mode (and perhaps got a map result) we won't see these.
		/// </summary>
		private void SetupMapMode()
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;
			
			sessionManager.InputPageState.MapMode = CurrentMapMode.TravelBoth;
			sessionManager.InputPageState.MapType = CurrentLocationType.None;
			sessionManager.InputPageState.MapLocationSearch = new LocationSearch();
			sessionManager.InputPageState.MapLocation = new TDLocation();
		}

		/// <summary>
		/// Returns the current date in the desired format
		/// </summary>
		private string GetCurrentDateTimeString()
		{
			return DisplayFormatAdapter.StandardDateAndTimeFormat(TDDateTime.Now);
		}

		#endregion private methods

    }
}
