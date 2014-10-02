// *********************************************** 
// NAME                 : FindCarParkResults.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 01/08/2006 
// DESCRIPTION			: Results page for nearest
//						  car parks found for a 
//						  specified location.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindCarParkResults.aspx.cs-arc  $ 
//
//   Rev 1.7   Mar 29 2010 16:39:24   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.6   Oct 01 2009 16:25:14   apatel
//Updates for Social Bookmark links
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.5   Sep 30 2009 16:25:50   apatel
//Social book marking added to Find Car Park Results
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.4   May 08 2008 11:41:18   mmodi
//Changes made to multiple files to fix the find a car park feature of the city-to-city trunk mode. Testing performed to ensure that the trunk car park features has not broken the drive to car park mode.
//Resolution for 4954: Include 'Drive to Car Park' functionality in City to City
//
//   Rev 1.3   Apr 03 2008 15:27:50   apatel
//updated to show different text in drive to mode.
//
//   Rev 1.2   Mar 31 2008 13:24:24   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Mar 02 2008 17:45:00 apatel
//  Set the help button 
//
//  Rev DevFactory Feb 05 2008 09:33:00 apatel
//  Removed the driveTo and drive from buttons from top. Change layout of the controls.
//  Move the hide button, back button, search and amend buttons.
//  Moved the help button and the title. labelResultsTableTitle label removed.
//
//   Rev DevFactory   Feb 01 2008 17:00:00   mmodi
//Additional logic for New Search, Amend, and Back buttons for Car Park Usability changes
//
//   Rev 1.0   Nov 08 2007 13:29:26   mturner
//Initial revision.
//
//   Rev 1.12   Jul 05 2007 10:28:18   mmodi
//Updated link to PDF
//Resolution for 4457: DEL 9.7 - Amendments to car parks
//
//   Rev 1.11   Jun 26 2007 16:17:06   mmodi
//Updated to use error string containing PDF link
//Resolution for 4457: 9.7 - Amendments to car parks
//
//   Rev 1.10   Oct 19 2006 10:53:14   mmodi
//Amended code to display Car park input when Back selected
//Resolution for 4231: Car Parking: Back button navigation issue on results page
//
//   Rev 1.9   Sep 27 2006 12:59:46   mmodi
//Added code to assign skip link text
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4185: Car Parking: Screen reader
//
//   Rev 1.8   Sep 05 2006 11:32:06   mmodi
//Added transition to Findcarinput
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.7   Sep 04 2006 11:47:08   esevern
//added call to store current car park selection in the table control
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.6   Aug 31 2006 14:30:32   MModi
//Resolved display and error message issues
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.5   Aug 22 2006 14:58:26   esevern
//Removed 'next' button references - not required for FindNearestCarPark
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.4   Aug 15 2006 11:33:10   esevern
//removed redundant switch statement
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.3   Aug 14 2006 11:18:48   esevern
//Interim check-in for developer build. Added FindCarParkResults Location and Table controls
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.2   Aug 04 2006 12:03:36   mmodi
//Added labels and button controls
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.1   Aug 03 2006 11:30:40   esevern
//Added PageId to default constructor
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.0   Aug 02 2006 10:36:44   esevern
//Initial revision.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2


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
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for FindCarParkResults.
	/// </summary>
	public partial class FindCarParkResults : TDPage
	{
		#region Controls declaration

		protected TransportDirect.UserPortal.Web.Controls.FindCarParksResultsTableControl resultsTableControl;
		protected ErrorDisplayControl errorDisplayControl;
		protected PrinterFriendlyPageButtonControl printerFriendlyPageButton;

	
		#endregion

		#region Resource keys declaration

		private const string RES_TITLE_CARPARKMODE_VALID	= "FindCarParkResult.labelTitle.CarParkMode.Valid";
		private const string RES_NOTE_CARPARKMODE_VALID		= "FindCarParkResult.labelNote.CarParkMode.Valid";
		public const string RES_ERRORMESSAGE_CARPARKMODE	= "FindCarParkResult.ErrorMessage.CarParkMode";
		private const string CARPARKPROVIDERS_DOCUMENT		= "FindCarParkResult.CarParkProivders.PDF";
        private const string RES_NOTE_CARPARKDRIVETOMODE_VALID = "FindCarParkResult.labelNote.CarParkDriveToMode.Valid";

		#endregion

		private const string HEADER_URL_PARAM	= "?NotFindAMode=true";
		private FindCarParkPageState carParkPageState;
		private FindPageState pageState;

		#region Constructor and Page_Load

		protected void Page_Load(object sender, System.EventArgs e)
		{
			carParkPageState = TDSessionManager.Current.FindCarParkPageState;
			pageState = TDSessionManager.Current.FindPageState;

			if  (Session.IsNewSession)
			{
				// Get the baseChannel URL
				string channelName = getBaseChannelURL(TDPage.SessionChannelName);
				// Get the currentpage URL and add the specific constant required.
				IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
				PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindCarParkInput);
				string url = pageTransferDetails.PageUrl + HEADER_URL_PARAM;
				// Redirect to the new url
				Response.Redirect(channelName + url);			
			}

			// Skip link
			imageMainContentSkipLink1.ImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			imageMainContentSkipLink1.AlternateText = GetResource("FindCarParkResults.SkipLink_CarPark.AlternateText");

			LoadResources();
			SetControlVisibility();

            //Added for white labelling:
            #region Determine context based on car parks mode
            TransportDirect.UserPortal.SuggestionLinkService.Context context;

            if (carParkPageState.CurrentFindMode == FindCarParkPageState.FindCarParkMode.Default)
                context = TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuFindAPlace;
            else
                context = TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney;
            #endregion

            ConfigureLeftMenu("ClientLinks.DoorToDoor.LinkText", "", null, expandableMenuControl, context);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFindCarParkResults);
            expandableMenuControl.AddExpandedCategory("Related links");

            headElementControl.ImageSource = GetResource("HomeDefault.imageFindCarPark.ImageUrl");
            socialBookMarkLinkControl.BookmarkDescription = headElementControl.Desc = resultsLocationControl.ToString();

		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public FindCarParkResults()
		{
			pageId = PageId.FindCarParkResults;
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Set visibility of controls in page
		/// </summary>
		private void SetControlVisibility()
		{
			// in case there is no car park returned, leave only buttons to go Back, 
			// New Search, and Amend and display error message

            bool showDriveFromButtons = TDSessionManager.Current.FindCarParkPageState.CurrentFindMode != FindCarParkPageState.FindCarParkMode.DriveTo;
			if (carParkPageState.CarParkFound == false)
			{
                commandDriveFrom2.Visible = false;
				commandDriveTo2.Visible = false;
				commandShowMap.Visible = false;
				labelNote.Visible = false;
				commandNewSearch.Visible = true;
				commandAmendSearch.Visible = true;
				printerFriendlyPageButton.Visible = false;
				errorDisplayControl.Visible = true;
                panelErrorDisplay.Visible = true;
			}
			else
			{
				commandNewSearch.Visible = true;
				commandAmendSearch.Visible = true;
				
				// if no location defined, show travel from / Travel to
				if (carParkPageState.LocationFrom.Status == TDLocationStatus.Valid
					|| carParkPageState.LocationTo.Status == TDLocationStatus.Valid)
				{
                    
                    commandDriveFrom2.Visible = showDriveFromButtons;
					commandDriveTo2.Visible = true;
				}

                if (!showDriveFromButtons)
                {
                    labelNote.Text = GetResource(RES_NOTE_CARPARKDRIVETOMODE_VALID);
                }
			}

            //Note: we always want to hide the New Location button, since this functionality
            //has now been replaced using the New Search button.
            resultsLocationControl.SetNewLocationButtonVisibility(false);
		}

		/// <summary>
		/// Load page resources
		/// </summary>
		private void LoadResources()
		{
            helpIconSelect.HelpLabelControl = resultsLocationControl.HelpLabel;
			SetText();
			SetErrorControl();
		}

		/// <summary>
		/// Set Text for controls in page
		/// </summary>
		private void SetText()
		{
            this.PageTitle = GetResource("FindCarParkResult.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

			#region Static Setting

			commandBack.Text		= GetResource("FindCarParkResult.commandBack.Text");
			commandDriveFrom2.Text	= GetResource("FindCarParkResult.commandDriveFrom2.Text");
			commandDriveTo2.Text	= GetResource("FindCarParkResult.commandDriveTo2.Text");
			commandShowMap.Text		= GetResource("FindCarParkResult.commandShowMap.Text");
			commandNewSearch.Text	= GetResource("FindCarParkResult.commandNewSearch.Text");
			commandAmendSearch.Text = GetResource("FindCarParkResult.commandAmendSearch.Text");
            // CCN 0427 - moved help label to top right corner
            helpIconSelect.AlternateText = GetResource("FindCarParkResults.AlternateText");
            
			#endregion

			
			labelNote.Text = GetResource(RES_NOTE_CARPARKMODE_VALID);
		}

		/// <summary>
		/// Set Error control
		/// </summary>
		private void SetErrorControl()
		{
			errorDisplayControl.Type = ErrorsDisplayType.Error;
			
			// English and Welsh version of doc available, therefore obtain appropriate one
			string carParkProvidersProperty = GetResource(CARPARKPROVIDERS_DOCUMENT);
			string text = String.Format(GetResource(RES_ERRORMESSAGE_CARPARKMODE), Properties.Current[carParkProvidersProperty]);
			
			ArrayList errorsList = new ArrayList();
			errorsList.Add(text);

			errorDisplayControl.ErrorStrings = (string[])errorsList.ToArray(typeof(string));

			errorDisplayControl.Visible = false;
            panelErrorDisplay.Visible = false;
		}

        /// <summary>
        /// Sets the session journey parameters ready for a new search
        /// </summary>
        private void SetupForJourneyNewSearch(ITDSessionManager sessionManager, TDItineraryManager itineraryManager)
        {
            //Reset all journey related
            itineraryManager.NewSearch();

            // Flag new search button being clicked so that redirect page can perform any necessary initialisation
            sessionManager.SetOneUseKey(SessionKey.NewSearch, string.Empty);

            // invalidate the current journey result. Set the mode for which the results pertain to as being
            // none so that clicking the Find A tab will then redirect to the default find A input page.
            if (sessionManager.JourneyResult != null)
            {
                sessionManager.JourneyResult.IsValid = false;
            }
        }

        /// <summary>
        /// Sets the session journey parameters ready for an amend
        /// </summary>
        /// <param name="sessionManager"></param>
        private void SetupForJourneyAmend(ITDSessionManager sessionManager)
        {
            // If the results have been added to the Itinerary then we need to get them back out again
            ExtendItineraryManager itineraryManager = TDItineraryManager.Current as ExtendItineraryManager;

            if ((itineraryManager != null) && (itineraryManager.Length > 0) && !itineraryManager.ExtendInProgress)
            {
                if (itineraryManager.Length == 1)
                {
                    // The Initial journey is the only journey in the Itinerary
                    itineraryManager.ResetToInitialJourney();
                }
                else
                {
                    itineraryManager.ResetLastExtension();
                }
            }

            // invalidate the current journey result. Set the mode for which the results pertain to as being
            // none so that clicking the Find A tab will then redirect to the default find A input page.
            if (sessionManager.JourneyResult != null)
            {
                sessionManager.JourneyResult.IsValid = false;
            }
        }
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			AddExtraWireEvent();
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Extra event subscription
		/// </summary>
		private void AddExtraWireEvent()
		{
			// Setting up the Button Event handlers
			this.commandBack.Click += new EventHandler(this.CommandBackClick);
			this.commandShowMap.Click += new EventHandler(this.CommandShowMapClick);
			this.commandDriveFrom2.Click += new EventHandler(this.CommandTravelFromClick);
			this.commandDriveTo2.Click += new EventHandler(this.CommandTravelToClick);
			this.commandNewSearch.Click += new EventHandler(this.CommandNewSearchClick);
			this.commandAmendSearch.Click += new EventHandler(this.CommandAmendSearchClick);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		#region Event handler methods
		/// <summary>
		/// Click event for the ShowMap button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void CommandShowMapClick(object sender, EventArgs e)
		{
			carParkPageState.IsShowingHidingMap = true;
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarParkResultsShowMap;
		}

		/// <summary>
		/// click event for the TravelFrom button
		/// This button is displayed for CarPark Mode, when no location is valid
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void CommandTravelFromClick(object sender, EventArgs e)
		{
			FindCarParkHelper.ResultsTravelFrom();

			// Set the Return Stack so Find Car route can check its come from this page
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(((TDPage)Page).PageId);
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarInputDefault;
		}

		/// <summary>
		/// click event for the TravelTo button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void CommandTravelToClick(object sender, EventArgs e)
		{
			FindCarParkHelper.ResultsTravelTo();

			// Set the Return Stack so Find Car route/Door to door input can check its come from this page
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(((TDPage)Page).PageId);

            if (carParkPageState.IsFromDoorToDoor)
            {
                FindCarParkHelper.SetupCarParkLocationForDoorToDoor(false);

                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputDefault;
            }
            else if (carParkPageState.IsFromCityToCity)
            {
                TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(this.PageId);
                FindCarParkHelper.SetupCarParkLocationForCityToCity(false);
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindTrunkInputDefault;
            }
            else
            {
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarInputDefault;
            }
		}

		/// <summary>
		/// Click event for the back button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void CommandBackClick(object sender, EventArgs e)
		{
            ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];

            // Determine mode of page and send to appropriate page
            if (sessionManager.FindCarParkPageState.CurrentFindMode == FindCarParkPageState.FindCarParkMode.DriveTo)
            {
                #region Door to door, Find car route, city to city
                // User has come from the Car Journey Details page if in DriveTo mode. So return them back there
                #region Set for going to Details page
                // Reset car park state
                carParkPageState.Initialise();

                // Reset for going to Details page
                sessionManager.JourneyMapState.Initialise();
                sessionManager.ReturnJourneyMapState.Initialise();

                //When entering the Details page, set the Road Units to Miles
                sessionManager.InputPageState.Units = RoadUnitsEnum.Miles;

                // Specific handling for Door to door
                if (carParkPageState.IsFromDoorToDoor)
                {
                    carParkPageState.IsFromDoorToDoor = false;
                    sessionManager.FindPageState = null;
                }
                else if (carParkPageState.IsFromCityToCity)
                {
                    carParkPageState.IsFromCityToCity = false;
                }

                #endregion

                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoJourneyDetails;
                #endregion
            }
            else
            {
                #region Car park input
                if ((pageState.Mode == FindAMode.CarPark) || (pageState.Mode == FindAMode.Car))
                    carParkPageState.InstateAmendMode();

                // Set the mode the find car park input page should be displayed in
                carParkPageState.CarParkFindMode = carParkPageState.CurrentFindMode;

                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = FindInputAdapter.GetTransitionEventFromMode(FindAMode.CarPark);
                #endregion
            }
		}

		/// <summary>
		/// Click event for the NewSearch button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CommandNewSearchClick(object sender, EventArgs e)
		{
            ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];
            TDItineraryManager itineraryManager = TDItineraryManager.Current;

            // Reset car park state
            carParkPageState.Initialise();

            // Determine where we've come from and send to appropriate input page
            if (carParkPageState.IsFromDoorToDoor)
            {
                #region Door to door
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputDefault;

                carParkPageState.IsFromDoorToDoor = false;
                sessionManager.FindPageState = null;

                SetupForJourneyNewSearch(sessionManager, itineraryManager);
                #endregion
            }
            else if (carParkPageState.IsFromCityToCity)
            {
                #region City to city
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindTrunkInputDefault;

                carParkPageState.IsFromCityToCity = false;
                
                SetupForJourneyNewSearch(sessionManager, itineraryManager);
                #endregion
            }
            else if (sessionManager.FindPageState.Mode == FindAMode.Car)
            {
                #region Find car route
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarInputDefault;

                SetupForJourneyNewSearch(sessionManager, itineraryManager);
                #endregion
            }
            else
            {
                #region Car park input
                // Set the mode the find car park input page should be displayed in
                carParkPageState.CarParkFindMode = carParkPageState.CurrentFindMode;

                // User started in Nearest car park mode, send back to Find car park input
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarParkInputDefault;
                #endregion
            }
		}
		
		/// <summary>
		/// Click event for the AmendSearch button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CommandAmendSearchClick(object sender, EventArgs e)
		{
            ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];

            // Determine where we've come from and send to appropriate input page
            if (carParkPageState.IsFromDoorToDoor)
            {
                #region Door to door
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputDefault;

                carParkPageState.IsFromDoorToDoor = false;
                sessionManager.FindPageState = null;

                SetupForJourneyAmend(sessionManager);
                #endregion
            }
            else if (carParkPageState.IsFromCityToCity)
            {
                #region City to city
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindTrunkInputDefault;

                carParkPageState.IsFromCityToCity = false;
                
                SetupForJourneyAmend(sessionManager);
                #endregion
            }
            else if (sessionManager.FindPageState.Mode == FindAMode.Car)
            {
                #region Find car route
                sessionManager.FindPageState.PrepareForAmendJourney();

                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarInputDefault;

                SetupForJourneyAmend(sessionManager);
                #endregion
            }
            else
            {
                #region Car park input
                // Clear the searches and locations in session then set the AmendKey in session
                // to indicate to the input page not to initialise it
                carParkPageState.InstateAmendMode();

                // Set the mode the find car park input page should be displayed in
                carParkPageState.CarParkFindMode = carParkPageState.CurrentFindMode;

                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarParkInputDefault;
                #endregion
            }
		}
		
		#endregion

	}
}
