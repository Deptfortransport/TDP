// *********************************************** 
// NAME             : AcessibleOptionsControl.ascx.cs      
// AUTHOR           : David Lane
// DATE CREATED     : 10 Mar 2012
// DESCRIPTION  	: Accessible stops control
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TDP.Common;
using TDP.Common.Extenders;
using TDP.Common.DataServices;
using TDP.Common.DataServices.NPTG;
using TDP.Common.LocationService;
using TDP.Common.ServiceDiscovery;
using TDP.Common.Web;
using TDP.Common.PropertyManager;
using TDP.UserPortal.JourneyControl;
using TDP.Common.LocationService.GIS;
using TransportDirect.Presentation.InteractiveMapping;
using TDP.Common.EventLogging;
using Logger = System.Diagnostics.Trace;
using TDP.UserPortal.SessionManager;
using TDP.Common.ResourceManager;

namespace TDP.UserPortal.TDPMobile.Controls
{
    #region Public Event Definition

    /// <summary>
    /// EventsArgs class for displaying messages
    /// </summary>
    public class DisplayMessageEventArgs : EventArgs
    {
        private TDPMessage message;
        private PageId redirectPageId;

        /// <summary>
        /// Constructor
        /// </summary>
        public DisplayMessageEventArgs(TDPMessage message, PageId redirectPageId)
        {
            this.message = message;
            this.redirectPageId = redirectPageId;
        }

        /// <summary>
        /// TDPJourneyPlannerMode
        /// </summary>
        public TDPMessage Message
        {
            get { return message; }
        }

        /// <summary>
        /// PageId
        /// </summary>
        public PageId RedirectPageId
        {
            get { return redirectPageId; }
        }
    }

    /// <summary>
    /// Delegate for displaying message
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void DisplayMessage(object sender, DisplayMessageEventArgs e);

    #endregion

    /// <summary>
    /// Accessible mobility options control
    /// </summary>
    public partial class AccessibleStopsControl : System.Web.UI.UserControl
    {
        private NPTGData nptgData = null;
        private TDPAccessiblePreferences accessiblePreferences = null;
        private ITDPJourneyRequest journeyRequest = null;
        private TDPJourneyPlannerMode plannerMode = TDPJourneyPlannerMode.PublicTransport;
        private TDPLocation requestOriginLocation;
        private TDPLocation requestDestinationLocation;
        private TDPLocation originLocation;
        private TDPLocation destinationLocation;
        public const string DEFAULT_ITEM = "Default";


        #region Public Events

        public event PlanJourney OnPlanJourney;
        public event DisplayMessage OnDisplayMessage;

        #endregion

        #region Public Properties

        /// <summary>
        /// Read only, the origin location 
        /// </summary>
        public TDPLocation OriginLocation
        {
            get { return originLocation; }
        }

        /// <summary>
        /// Read only, the destination location 
        /// </summary>
        public TDPLocation DestinationLocation
        {
            get { return destinationLocation; }
        }

        /// <summary>
        /// Read/Write. Determines the options and functionality enabled in this control
        /// </summary>
        public TDPJourneyPlannerMode PlannerMode
        {
            get { return plannerMode; }
            set
            {
                plannerMode = value;

                // Mobile only supports PT and Cycle
                switch (plannerMode)
                {
                    case TDPJourneyPlannerMode.Cycle:
                    case TDPJourneyPlannerMode.PublicTransport:
                        break;
                    default:
                        plannerMode = TDPJourneyPlannerMode.PublicTransport;
                        break;
                }
            }
        }

        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_Load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            nptgData = TDPServiceDiscovery.Current.Get<NPTGData>(ServiceDiscoveryKey.NPTGData);
            
            SessionHelper sessionHelper = new SessionHelper();
            journeyRequest = sessionHelper.GetTDPJourneyRequest();

            if (journeyRequest != null)
            {
                // Set request locations
                if (journeyRequest.Origin != null)
                    requestOriginLocation = journeyRequest.Origin;

                if (journeyRequest.Destination != null)
                    requestDestinationLocation = journeyRequest.Destination;
            
                // Set accessible preferences 
                accessiblePreferences = journeyRequest.AccessiblePreferences;
            }

            SetupStopLists();
        }

        /// <summary>
        /// Page_PreRender 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupResources();
        }

        #endregion

        #region Control Event Handlers

        /// <summary>
        /// Event handler for stop post back event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Stop_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Event handler for plan journey button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void planJourneyBtn_Click(object sender, EventArgs e)
        {
            bool allOK = true;

            if (requestOriginLocation != null && requestOriginLocation.Accessible)
            {
                // Set the selected location
                originLocation = requestOriginLocation;
            }
            else
            {
                if (originStopList.SelectedValue.Trim().Length > 0)
                {
                    // Get the location

                    // If nothing selected, don't do anything
                    // get the location from the list
                    if (originStopList.SelectedIndex <= 0)
                    {
                        originLocation = null;
                    }
                    else
                    {
                        originLocation = GetLocation(originStopList);
                    }                
                }
                else
                {
                    allOK = false;
                }
            }


            if (requestDestinationLocation != null && requestDestinationLocation.Accessible)
            {
                // Set the selected location
                destinationLocation = requestDestinationLocation;
            }
            else
            {
                if (destinationStopList.SelectedValue.Trim().Length > 0)
                {
                    // Get the location

                    // If nothing selected, don't do anything
                    // get the location from the list
                    if (destinationStopList.SelectedIndex <= 0)
                    {
                        destinationLocation = null;
                    }
                    else
                    {
                        destinationLocation = GetLocation(destinationStopList);
                    }
                }
                else
                {
                    allOK = false;
                }
            }

            if (allOK)
            {
                // Raise event to let page submit journey
                if (OnPlanJourney != null)
                {
                    OnPlanJourney(this, new PlanJourneyEventArgs(this.plannerMode));
                }
            }
            else
            {
                // No stop selected
                DisplayMessage(new TDPMessage("AccessiblilityOptions.NoStopSelected.Text", TDPMessageType.Error), PageId.Empty);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns the location selected from the stops drop down list
        /// </summary>
        /// <param name="stopList"></param>
        /// <returns></returns>
        private TDPLocation GetLocation(DropDownList stopList)
        {
            LocationService locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);
            TDPLocation location = new TDPLocation();
            LocationSearch search = new LocationSearch();

            string locationId = string.Empty, locationName = string.Empty;
            TDPLocationType locationType = TDPLocationType.Unknown;

            GetAccessibleDropDownValues(stopList, ref locationId, ref locationType, ref locationName);

            search.SearchText = locationName;
            search.SearchId = locationId;
            search.SearchType = locationType;

            // Use the LocationService to resolve the location - the dropdown will contain the location id and type
            location = locationService.ResolveLocation(ref search, true, TDPSessionManager.Current.Session.SessionID);
            location.Accessible = true;

            return location;
        }

        /// <summary>
        /// Retrieves the selected location values from the Accessible location dropdown
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="locationType"></param>
        /// <param name="locationName"></param>
        private void GetAccessibleDropDownValues(DropDownList stopList, ref string locationId, ref TDPLocationType locationType, ref string locationName)
        {
            if (stopList.SelectedIndex > 0)
            {
                try
                {
                    string[] values = stopList.SelectedItem.Value.Split('|');

                    locationId = values[0];
                    locationType = (TDPLocationType)Enum.Parse(typeof(TDPLocationType), values[1]);
                    locationName = values[2];
                }
                catch
                {
                    // Ignore exception, this is a server controlled list, and any tampering should be rejected!
                }
            }
        }
        
        /// <summary>
        /// Loads resource strings for labels/controls
        /// </summary>
        private void SetupResources()
        {
            TDPPageMobile page = (TDPPageMobile)Page;

            // Accessible stops title
            accessibleStopsHeading.InnerText = page.GetResourceMobile("AccessibilityOptions.Heading.Text");

            if (string.IsNullOrEmpty(accessibleStopsHeading.InnerText))
            {
                accessibleStopsHeading.Visible = false;
            }

            string message = string.Empty;
            string journeytype = string.Empty;

            if (!(!originAccessibleStopDiv.Visible && !destinationAccessibleStopDiv.Visible))
            {
                // Info message
                message = page.GetResourceMobile("AccessibilityOptions.Message");
                string origindest = page.GetResourceMobile("AccessibilityOptions.Message.OriginDestination");

                if (requestOriginLocation != null && requestDestinationLocation != null)
                {
                    if (!requestOriginLocation.Accessible && !requestDestinationLocation.Accessible)
                    {
                        origindest = page.GetResourceMobile("AccessibilityOptions.Message.OriginDestination");
                    }
                    else if (!requestOriginLocation.Accessible)
                    {
                        origindest = page.GetResourceMobile("AccessibilityOptions.Message.Origin");
                    }
                    else // Assume its destination
                    {
                        origindest = page.GetResourceMobile("AccessibilityOptions.Message.Destination");
                    }
                }

                if (accessiblePreferences.RequireSpecialAssistance && accessiblePreferences.RequireStepFreeAccess)
                {
                    journeytype = page.GetResourceMobile("AccessibilityOptions.Message.StepFreeAndAssistance");
                }
                else if (accessiblePreferences.RequireSpecialAssistance)
                {
                    journeytype = page.GetResourceMobile("AccessibilityOptions.Message.Assistance");
                }
                else
                {
                    journeytype = page.GetResourceMobile("AccessibilityOptions.Message.StepFree");
                }

                message = string.Format(message, journeytype, origindest);
            }

            accessibleStopsInfo.Text = message;

            if (requestOriginLocation != null && requestDestinationLocation != null)
            {
                // Drop down labels
                originHeading.Text = string.Format(page.GetResourceMobile("AccessibilityOptions.Origin.Label"), requestOriginLocation.DisplayName);
                destinationHeading.Text = string.Format(page.GetResourceMobile("AccessibilityOptions.Destination.Label"), requestDestinationLocation.DisplayName);

                // End point labels
                originLabel.Text = string.Format(page.GetResourceMobile("AccessibilityOptions.OriginLabel.Text"), requestOriginLocation.DisplayName);
                destinationLabel.Text = string.Format(page.GetResourceMobile("AccessibilityOptions.DestinationLabel.Text"), requestDestinationLocation.DisplayName);
            }

            // Plan journey button
            planJourneyBtn.Text = Server.HtmlDecode(page.GetResourceMobile("JourneyInput.PlanJourney.Text"));
            planJourneyBtn.ToolTip = Server.HtmlDecode(page.GetResourceMobile("JourneyInput.PlanJourney.ToolTip"));
            planJourneyBtnNonJS.Text = Server.HtmlDecode(page.GetResourceMobile("JourneyInput.PlanJourney.Text"));
            planJourneyBtnNonJS.ToolTip = Server.HtmlDecode(page.GetResourceMobile("JourneyInput.PlanJourney.ToolTip"));
        }
                
        /// <summary>
        /// Displays a message
        /// </summary>
        private void DisplayMessage(TDPMessage tdpMessage, PageId redirectPageId)
        {
            if (OnDisplayMessage != null)
            {
                OnDisplayMessage(this, new DisplayMessageEventArgs(tdpMessage, redirectPageId));
            }
        }

        /// <summary>
        /// Populates the stops drop down list
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="stops"></param>
        private void PopulateStopList(bool origin, List<TDPLocation> stops)
        {
            TDPPageMobile page = (TDPPageMobile)Page;

            DropDownList stopList;
            if (origin)
            {
                stopList = originStopList;
            }
            else
            {
                stopList = destinationStopList;
            }

            int selectedIndex = stopList.SelectedIndex;

            // Clear existing list
            stopList.Items.Clear();

            // The first item in the drop down list should be "please select"
            ListItem item;
            string itemPleaseSelect = string.Empty;

            if (origin)
            {
                itemPleaseSelect = page.GetResourceMobile("AccessibleStops.ItemPleaseSelect.Origin");
            }
            else
            {
                itemPleaseSelect = page.GetResourceMobile("AccessibleStops.ItemPleaseSelect.Destination");
            }

            item = new ListItem(itemPleaseSelect);
            stopList.Items.Add(item);

            int i = 0;

            foreach (TDPLocation location in stops)
            {
                i++;

                // Display text is set to location name (upto 40 chars), with number and distance values 
                // to allow user to view on map and provide context from their chosen location
                string id = location.ID;
                if (string.IsNullOrEmpty(id))
                {
                    if (location.Naptan.Count > 0)
                    {
                        id = location.Naptan[0];
                    }
                    else
                    {
                        // locality
                        id = location.Locality;
                    }
                }

                item = new ListItem(
                    string.Format("{0}. {1} ({2} {3})",
                        i,
                        (location.Name.Length <= 40) ? location.Name : location.Name.Substring(0, 40),
                        GetMilesDistance(location.DistanceFromSearchOSGR),
                        page.GetResource(TDPResourceManager.GROUP_JOURNEYOUTPUT, TDPResourceManager.COLLECTION_JOURNEY, "RouteText.Miles")),
                    string.Format("{0}|{1}|{2}", id, location.TypeOfLocation.ToString(), location.Name)
                    );
                stopList.Items.Add(item);
            }

            if (selectedIndex < stopList.Items.Count)
                stopList.SelectedIndex = selectedIndex;
        }

        /// <summary>
        /// Returns the miles distance as string
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        private string GetMilesDistance(double distance)
        {
            double miles = distance / 1609;

            string milesRounded = Math.Round(miles, 1, MidpointRounding.AwayFromZero).ToString();

            return milesRounded;
        }

        /// <summary>
        /// Populate the find nearest lists
        /// </summary>
        private void SetupStopLists()
        {
            // Hide both divs by default
            originLabelDiv.Visible = false;
            originAccessibleStopDiv.Visible = false;
            destinationLabelDiv.Visible = false;
            destinationAccessibleStopDiv.Visible = false;

            // Origin
            if (requestOriginLocation != null)
            {
                if (requestOriginLocation.Accessible)
                {
                    originLabelDiv.Visible = true;
                }
                else
                {
                    // Find nearest stops and localities
                    List<TDPLocation> stops = GetStopList(true);
                    PopulateStopList(true, stops);
                    
                    if (stops.Count > 0)
                    {
                        originAccessibleStopDiv.Visible = true;
                    }
                }
            }

            // Destination
            if (requestDestinationLocation != null)
            {
                if (requestDestinationLocation.Accessible)
                {
                    destinationLabelDiv.Visible = true;
                }
                else
                {
                    // Find nearest stops and localities
                    List<TDPLocation> stops = GetStopList(false);
                    PopulateStopList(false, stops);

                    if (stops.Count > 0)
                    {
                        destinationAccessibleStopDiv.Visible = true;
                    }
                }
            }

            // No stops for origin and destination, disable plan journey and show error
            if (!originAccessibleStopDiv.Visible && !destinationAccessibleStopDiv.Visible)
            {
                planJourneyBtn.Enabled = false;
                DisplayMessage(new TDPMessage("AccessibilityOptions.Message.NoStops", TDPMessageType.Error), Common.PageId.MobileInput);
            }
        }

        /// <summary>
        /// Filters the stop list based on the user input
        /// </summary>
        /// <param name="isOrigin">if for origin location</param>
        /// <returns></returns>
        private List<TDPLocation> GetStopList(bool isOrigin)
        {
            // Get nearest stops
            List<TDPLocation> results = new List<TDPLocation>();
            TDPLocation location;
            if (isOrigin)
            {
                location = requestOriginLocation;
            }
            else
            {
                location = requestDestinationLocation;
            }

            if (location != null)
            {
                try
                {
                    int searchDistanceMetresStops = 10000;
                    int searchDistanceMetresLocalities = 10000;
                    int maxResultStops = 10;
                    int maxResultLocalities = 3;

                    Int32.TryParse(Properties.Current["AccessibleOptions.FindNearestLocations.Stops.SearchDistance.Metres"], out searchDistanceMetresStops);
                    Int32.TryParse(Properties.Current["AccessibleOptions.FindNearestLocations.Localities.SearchDistance.Metres"], out searchDistanceMetresLocalities);
                    Int32.TryParse(Properties.Current["AccessibleOptions.FindNearestLocations.Stops.Count.Max"], out maxResultStops);
                    Int32.TryParse(Properties.Current["AccessibleOptions.FindNearestLocations.Localities.Count.Max"], out maxResultLocalities);

                    LocationService locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);
                    
                    List<string> stopTypes = GetAccessibleStopTypes();

                    #region Stops search

                    // Check if Stops query should be performed,
                    // assume more than 1 stop type
                    if (((stopTypes.Count > 1))
                        && (maxResultStops > 0))
                    {
                        // Find accessible locations for the location coordinate
                        List<TDPLocation> accessibleStops = locationService.FindNearestAccessibleStops(
                            location.GridRef.Easting, location.GridRef.Northing,
                            searchDistanceMetresStops, maxResultStops,
                            journeyRequest.OutwardDateTime, accessiblePreferences.RequireStepFreeAccess, 
                            accessiblePreferences.RequireSpecialAssistance, stopTypes.ToArray());

                        if (accessibleStops != null && accessibleStops.Count > 0)
                        {
                            results.AddRange(accessibleStops);
                        }
                    }

                    #endregion

                    #region Localities search

                    if (maxResultLocalities > 0)
                    {
                        // Find accessible localities for the location coordinate
                        List<TDPLocation> accessibleLocalities = locationService.FindNearestAccessibleLocalities(
                            location.GridRef.Easting, location.GridRef.Northing,
                            searchDistanceMetresLocalities, maxResultLocalities,
                            accessiblePreferences.RequireStepFreeAccess, accessiblePreferences.RequireSpecialAssistance);

                        if (accessibleLocalities != null && accessibleLocalities.Count > 0)
                        {
                            results.AddRange(accessibleLocalities);
                        }
                    }

                    #endregion

                    #region Sort

                    results.Sort(
                        delegate(TDPLocation loc1, TDPLocation loc2)
                        {
                            return loc1.DistanceFromSearchOSGR.CompareTo(loc2.DistanceFromSearchOSGR);
                        });

                    #endregion
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error attempting to find accessible locations for [{0} {1},{2}]. Exception: {3}. {4}",
                        location.Naptan, location.GridRef.Easting, location.GridRef.Northing,
                        ex.Message, ex.StackTrace);
                    Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, message));
                }
            }

            return results;
        }

        /// <summary>
        /// Returns a string list for stop types to pass into the method to FindNearestAccessibleStops
        /// </summary>
        /// <param name="tdStopTypes"></param>
        /// <returns></returns>
        private List<string> GetAccessibleStopTypes()
        {
            List<String> stopTypes = new List<string>();

            // As per DN do not use the stop types selected by the user (!)
            stopTypes.Add(TDPModeType.Air.ToString());
            stopTypes.Add(TDPModeType.Coach.ToString());
            stopTypes.Add(TDPModeType.Ferry.ToString());
            stopTypes.Add(TDPModeType.Rail.ToString());
            stopTypes.Add("dlr"); // DLR
            stopTypes.Add("lightrail");
            stopTypes.Add("lulmetro"); // underground

            return stopTypes;
        }

        #endregion
    }
}