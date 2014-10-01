// *********************************************** 
// NAME             : TravelNews.aspx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 08 Mar 2012
// DESCRIPTION  	: Travel news page
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using TDP.Common;
using TDP.Common.DataServices;
using TDP.Common.Extenders;
using TDP.Common.LocationService;
using TDP.Common.PropertyManager;
using TDP.Common.ServiceDiscovery;
using TDP.Common.Web;
using TDP.Reporting.Events;
using TDP.UserPortal.SessionManager;
using TDP.UserPortal.TravelNews.SessionData;
using Logger = System.Diagnostics.Trace;
using TDP.UserPortal.ScreenFlow;
using TDP.UserPortal.TravelNews.TravelNewsData;

namespace TDP.UserPortal.TDPMobile
{
    /// <summary>
    /// Travel news page
    /// </summary>
    public partial class TravelNews : TDPPageMobile
    {
        #region Variables

        // Read from properties to control overall showing of news controls
        private bool showTravelNews = true;
        private bool showVenueNews = true;
        private bool showUndergroundStatus = true;

        private TravelNewsHelper tnHelper = null;
        private TravelNewsState currentNewsState = null;

        private NewsModeView newsModeView = NewsModeView.TravelNews; // Default

        /// <summary>
        /// Enum to define news mode to display by default
        /// </summary>
        private enum NewsModeView
        {
            TravelNews,
            VenueNews,
            UndergroundNews
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public TravelNews()
            : base(Global.TDPResourceManager)
        {
            pageId = PageId.MobileTravelNews;
        }

        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_Init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            tnHelper = new TravelNewsHelper();

            currentNewsState = GetTravelNewsState();

            InitialiseControls();

            if (!Page.IsPostBack)
            {
                SetupControls();

                SetupNewsControls(newsModeView);
            }
            else
            {
                if (((TDPMobile)Master).PageScriptManager.IsInAsyncPostBack)
                {
                    logPageEntry = false;
                }
                UpdateNewsItems();
            }

            AddJavascript("News.js");

            // Use the browser back in the header
            ((TDPMobile)Master).DisplayBrowserBack = true;
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupResources();

            // Save the update news state back in to session
            tnHelper.SetTravelNewsState(currentNewsState);

            SetupRefreshLink();

            SetupControlVisibility();

            if (((TDPMobile)Master).PageScriptManager.IsInAsyncPostBack)
            {
                // Log an event as a result of partial page update
                PageEntryEvent logPage = new PageEntryEvent(PageId.MobileTravelNewsPartialUpdate, TDPSessionManager.Current.Session.SessionID, false);
                Logger.Write(logPage);
            }
        }

        #endregion
        
        #region Event handlers

        /// <summary>
        /// tnModeDrp_SelectedIndexChanged event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tnModeDrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (newsModes.SelectedValue)
            {
                case TravelNewsHelper.NewsViewMode_LUL:
                    SetupNewsControls(NewsModeView.UndergroundNews);
                    break;
                case TravelNewsHelper.NewsViewMode_Venue:
                    SetupNewsControls(NewsModeView.VenueNews);
                    venueSelectControl.Populate(TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService), true);
                    break;
                default:
                    SetupNewsControls(NewsModeView.TravelNews);
                    break;
            }

            newsModeHeading.InnerHtml = newsModes.SelectedItem.Text;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialises travel news controls
        /// </summary>
        private void InitialiseControls()
        {
            showTravelNews = Properties.Current["TravelNews.Enabled.Switch"].Parse(true);
            showVenueNews = Properties.Current["VenueNews.Enabled.Switch"].Parse(true);
            showUndergroundStatus = Properties.Current["UndergroundNews.Enabled.Switch"].Parse(true);

            LocationHelper locationHelper = new LocationHelper();

            // If venues are not available then turn off venue news
            if (!locationHelper.VenueLocationsAvailable())
                showVenueNews = false;

            if (!showTravelNews && !showVenueNews && !showUndergroundStatus)
            {
                DisplayMessage(new TDPMessage("TravelNews.lblUnavailable.Text", TDPMessageType.Error));
            }
        }

        /// <summary>
        /// Loads resource strings for labels/controls
        /// </summary>
        private void SetupResources()
        {
            switch (newsModes.SelectedValue)
            {
                case TravelNewsHelper.NewsViewMode_LUL:
                    providedByLbl.Text = GetResourceMobile("TravelNews.LondonUnderground.ProvidedBy.Text");
                    break;
                case TravelNewsHelper.NewsViewMode_Venue:
                default:
                    providedByLbl.Text = GetResourceMobile("TravelNews.ProvidedBy.Text");
                    break;
            }

            newsModeOptionsLegend.InnerText = GetResourceMobile("TravelNews.NewsModeOptionsLegend.Text");
            tnFilterBtnNonJS.Text = GetResourceMobile("TravelNews.FilterButtonNonJS.Text"); 

            waitControl.LoadingMessageLabel.Text = GetResourceMobile("TravelNews.LoadingMessage.Text");
        }

        /// <summary>
        /// Setup the controls on the page
        /// </summary>
        private void SetupControls()
        {
            if (!IsPostBack)
            {
                // Populate the view news mode dropdown
                IDataServices dataServices = TDPServiceDiscovery.Current.Get<IDataServices>(ServiceDiscoveryKey.DataServices);

                dataServices.LoadListControl(DataServiceType.NewsViewMode, newsModes, Global.TDPResourceManager, CurrentLanguage.Value);
                newsModeHeading.InnerHtml = newsModes.SelectedItem.Text;

                // Populate the venues to select
                venueSelectControl.Populate(TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService), true);
            }

            #region Set default news mode view
            
            // Default news view mode should be All travel news if turned on, 
            if (showTravelNews)
            {
                newsModeView = NewsModeView.TravelNews;
            }
            // Underground news if turned on
            else if (showUndergroundStatus)
            {
                newsModeView = NewsModeView.UndergroundNews;
            }
            // Venue news if turned on
            else if (showVenueNews)
            {
                newsModeView = NewsModeView.VenueNews;
            }

            #endregion

            #region Override default view

            // Populate the selected venue naptans field
            if (showVenueNews &&
                currentNewsState.SelectedVenuesFlag && currentNewsState.SearchNaptans.Count > 0)
            {
                StringBuilder naptans = new StringBuilder();
                foreach (string naptan in currentNewsState.SearchNaptans)
                {
                    if (naptans.Length > 0)
                        naptans.Append(",");

                    naptans.Append(naptan);
                }

                // Set the hidden field to allow javascript and next postback to detect which naptans are selected
                venueNaptans.Value = naptans.ToString();

                newsModeView = NewsModeView.VenueNews;
            }

            // Check if query string indicates to show london underground lines
            if (!IsPostBack)
            {
                string newsMode = tnHelper.GetTravelNewsMode(true);

                // Only check for london undeground as default is travel news
                if (!string.IsNullOrEmpty(newsMode) && (newsMode.ToLower() == TravelNewsHelper.NewsViewMode_LUL.ToLower()))
                {
                    if (showUndergroundStatus)
                    {
                        newsModeView = NewsModeView.UndergroundNews;
                    }
                }
            }

            #endregion

            #region Set selected dropdown text

            try
            {
                switch (newsModeView)
                {
                    case NewsModeView.UndergroundNews:
                        newsModes.SelectedIndex = newsModes.Items.IndexOf(newsModes.Items.FindByValue(TravelNewsHelper.NewsViewMode_LUL));
                        newsModeHeading.InnerHtml = newsModes.SelectedItem.Text;
                        break;
                    case NewsModeView.VenueNews:
                        newsModes.SelectedIndex = newsModes.Items.IndexOf(newsModes.Items.FindByValue(TravelNewsHelper.NewsViewMode_Venue));
                        newsModeHeading.InnerHtml = newsModes.SelectedItem.Text;
                        break;
                    case NewsModeView.TravelNews:
                    default:
                        // Do nothing, drop down will already be travel news selected
                        break;
                }
            }
            catch
            {
                // Ignore exceptions, option may have been removed in config
                newsModeView = NewsModeView.TravelNews;
            }

            #endregion
        }

        /// <summary>
        /// Sets up the control visibility
        /// </summary>
        private void SetupControlVisibility()
        {
            // Default
            updatePanel.Visible = true;
            tnFilterDiv.Visible = true;

            // Hide the news panel and filter if all news modes turned off
            if (!showTravelNews && !showVenueNews && !showUndergroundStatus)
            {
                updatePanel.Visible = false;
                tnFilterDiv.Visible = false;
            }
            
            // Remove the new mode filters from the menu as required
            System.Web.UI.WebControls.ListItem item = null;

            if (!showTravelNews)
            {
                item = newsModes.Items.FindByValue(TravelNewsHelper.NewsViewMode_All);
                if (item != null)
                    newsModes.Items.Remove(item);
            }
            if (!showUndergroundStatus)
            {
                item = newsModes.Items.FindByValue(TravelNewsHelper.NewsViewMode_LUL);
                if (item != null)
                    newsModes.Items.Remove(item); 
            }
            
            if (!showVenueNews)
            {
                item = newsModes.Items.FindByValue(TravelNewsHelper.NewsViewMode_Venue);
                if (item != null)
                    newsModes.Items.Remove(item); 
            }

            if (newsModes.Items.Count <= 1)
            {
                tnFilterDiv.Visible = false;
            }
        }

        /// <summary>
        /// Sets up the auto-refresh link displayed on the page
        /// </summary>
        private void SetupRefreshLink()
        {
            if (Properties.Current["TravelNews.AutoRefresh.Enabled.Switch"].Parse(false))
            {
                refreshLinkDiv.Visible = Properties.Current["TravelNews.AutoRefresh.ShowRefreshLink.Switch"].Parse(false)
                    || DebugHelper.ShowDebug;

                // Build refresh url based on current filter selection, which should be reflected by the news state
                PageTransferDetail ptd = GetPageTransferDetail(PageId.MobileTravelNews);

                // Build the auto refresh url
                string refreshUrl = tnHelper.BuildTravelNewsUrl(ptd.PageUrl, currentNewsState, true, false, false, false, false, newsModes.SelectedValue);

                // Check if auto refresh is required for page by its existence in the request url,
                // and add to page
                if (tnHelper.GetTravelNewsAutoRefresh(true))
                {
                    int refreshSeconds = Properties.Current["TravelNews.AutoRefresh.Refresh.Seconds"].Parse(30);

                    // And add to the page for auto-refresh
                    this.AddAutoRefresh(refreshSeconds, refreshUrl);

                    // Set the refresh link to stop the auto-refresh
                    refreshUrl = tnHelper.BuildTravelNewsUrl(ptd.PageUrl, currentNewsState, false, false, false, false, false, newsModes.SelectedValue);

                    refreshLink.NavigateUrl = refreshUrl;
                    refreshLink.Text = GetResource("TravelNews.AutoRefreshLink.Stop.Text");
                    refreshLink.ToolTip = GetResource("TravelNews.AutoRefreshLink.Stop.ToolTip");
                }
                else
                {
                    // Auto-refresh currently not started
                    // Set the refresh link to start the auto-refresh
                    refreshLink.NavigateUrl = refreshUrl;
                    refreshLink.Text = GetResource("TravelNews.AutoRefreshLink.Start.Text");
                    refreshLink.ToolTip = GetResource("TravelNews.AutoRefreshLink.Start.ToolTip");
                }
            }
            else
            {
                // Hide the refresh link
                refreshLinkDiv.Visible = false;
            }
        }

        /// <summary>
        /// Sets up the news controls on the page
        /// </summary>
        private void UpdateNewsItems()
        {
            NewsModeView newsModeView = NewsModeView.TravelNews;
            
            switch (newsModes.SelectedValue)
            {
                case TravelNewsHelper.NewsViewMode_LUL:
                    newsModeView = NewsModeView.UndergroundNews;
                    break;
                case TravelNewsHelper.NewsViewMode_Venue:
                    newsModeView = NewsModeView.VenueNews;
                    break;
                default:
                    newsModeView = NewsModeView.TravelNews;
                    break;
            }

            newsModeHeading.InnerHtml = newsModes.SelectedItem.Text;

            SetupNewsControls(newsModeView);
        }

        /// <summary>
        /// Sets up the news controls on the page
        /// </summary>
        private void SetupNewsControls(NewsModeView newsModeView)
        {
            // Update the travel news state
            UpdateTravelNewsState();

            switch (newsModeView)
            {
                case NewsModeView.UndergroundNews:
                    travelNewsControl.Initialise(false, !showTravelNews, currentNewsState);
                    undergroundStatusControl.Initialise(true, !showUndergroundStatus);
                    break;
                case NewsModeView.TravelNews:
                case NewsModeView.VenueNews:
                default:
                    travelNewsControl.Initialise(true, !showTravelNews, currentNewsState);
                    undergroundStatusControl.Initialise(false, !showUndergroundStatus);
                    break;
            }
        }
        
        /// <summary>
        /// Returns the current travel news state
        /// </summary>
        /// <returns></returns>
        private TravelNewsState GetTravelNewsState()
        {
            TravelNewsState tns = null;
            
            // Get travel news state from session
            if (!IsPostBack)
            {
                tns = tnHelper.GetTravelNewsStateForMobile(true);
            }
            else
            {
                tns = tnHelper.GetTravelNewsStateForMobile(false);
            }

            return tns;
        }

        /// <summary>
        /// Udpates the travel news state with user parameters
        /// </summary>
        /// <returns></returns>
        private void UpdateTravelNewsState()
        {
            #region Venue naptans

            // Venue naptans
            selectedVenue.Text = string.Empty;

            try
            {
                // Only set search naptans if news view mode is for venue
                if (newsModes.SelectedValue == TravelNewsHelper.NewsViewMode_Venue)
                {
                    LocationService locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);
                    List<TDPLocation> venues = locationService.GetTDPVenueLocations();

                    // Specific venue(s) have been selected
                    if (!string.IsNullOrEmpty(venueNaptans.Value.Trim()))
                    {
                        currentNewsState.SearchNaptans = new List<string>(venueNaptans.Value.Split(','));
                        currentNewsState.SelectedVenuesFlag = true;

                        // Simple check for all venues, not accurate but should be ok
                        if (currentNewsState.SelectedAllVenuesFlag &&
                            tnHelper.GetVenueNaptans().Count == currentNewsState.SearchNaptans.Count)
                        {
                            currentNewsState.SelectedAllVenuesFlag = true;
                            selectedVenue.Text = GetResourceMobile("TravelNews.DisplayedFor.AllVenues");
                        }
                        else
                        {
                            currentNewsState.SelectedAllVenuesFlag = false;

                            LocationHelper locationHelper = new LocationHelper();
                            selectedVenue.Text = string.Format(GetResourceMobile("TravelNews.DisplayedFor.Venues"),
                                locationHelper.GetLocationNames(currentNewsState.SearchNaptans));
                        }
                    }
                    // No venues selected, so filter for all
                    else
                    {
                        currentNewsState.SearchNaptans = tnHelper.GetVenueNaptans();
                        currentNewsState.SelectedAllVenuesFlag = true;
                        currentNewsState.SelectedVenuesFlag = true;

                        selectedVenue.Text = GetResourceMobile("TravelNews.DisplayedFor.AllVenues");
                    }
                }
                else
                {
                    // Reset values which may have been set by page landing
                    currentNewsState.SearchNaptans = new List<string>();
                    currentNewsState.SelectedAllVenuesFlag = false;
                    currentNewsState.SelectedVenuesFlag = false;
                    currentNewsState.SelectedRegion = TravelNewsRegion.All.ToString();
                }
            }
            catch
            {
                // Ignore any exceptions
            }

            #endregion
        }

        /// <summary>
        /// Displays a message
        /// </summary>
        private void DisplayMessage(TDPMessage tdpMessage)
        {
            ((TDPMobile)this.Master).DisplayMessage(tdpMessage);
        }

        #endregion
    }
}
