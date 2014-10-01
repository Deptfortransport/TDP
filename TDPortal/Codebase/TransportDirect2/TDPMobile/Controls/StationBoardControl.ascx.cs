// *********************************************** 
// NAME             : StationBoardControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Feb 2014
// DESCRIPTION  	: Control to display station boards
// ************************************************
// 

using System;
using System.Web.UI.WebControls;
using TDP.Common.Extenders;
using TDP.Common.LocationService;
using TDP.Common.PropertyManager;
using TDP.Common.ServiceDiscovery;
using TDP.Common.Web;
using TDP.UserPortal.DepartureBoardService.DepartureBoardServiceTypes;
using TDP.UserPortal.SessionManager;
using DBS = TDP.UserPortal.DepartureBoardService;

namespace TDP.UserPortal.TDPMobile.Controls
{
    #region Public Events

    // Delegate for show station board
    public delegate void OnShowStationBoard(object sender, StationBoardEventArgs e);
    
    #region Public Event classes

    /// <summary>
    /// EventsArgs class for passing station board OnShowStationBoard event
    /// </summary>
    public class StationBoardEventArgs : EventArgs
    {
        private readonly bool showDepartures;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="journeyId"></param>
        public StationBoardEventArgs(bool showDepartures)
        {
            this.showDepartures = showDepartures;
        }

        /// <summary>
        /// Show Departures
        /// </summary>
        public bool ShowDepartures
        {
            get { return showDepartures; }
        }
    }
    
    #endregion

    #endregion

    /// <summary>
    /// Control to display station boards
    /// </summary>
    public partial class StationBoardControl : System.Web.UI.UserControl
    {
        #region Public Events

        // Show station board direction event declaration
        public event OnShowStationBoard ShowDirectionHandler;

        #endregion

        #region Private members

        private DBSResult stationBoardResult = null;
        private TDPStopLocation location = null;
        private bool showDepartures = true;

        private StopInformationHelper helper = null;
        private TDPPageMobile page = null;

        private const string timeFormat = "HH:mm"; // "dd/MM/yyyy HH:mm"

        /// <summary>
        /// Enum indicating type of service time report
        /// </summary>
        private enum ServiceReportType
        {
            OnTime,
            Late,
            Delayed,
            Cancelled,
            Unknown
        }

        #endregion

        #region Page_Load, Page_PreRender

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            helper = new StopInformationHelper();
            page = (TDPPageMobile)Page;
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupControls();

            BindStationBoard();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Initialise method
        /// </summary>
        public void Initialise(TDPStopLocation location, bool showDepartures, DBSResult stationBoardResult)
        {
            this.location = location;
            this.showDepartures = showDepartures;
            this.stationBoardResult = stationBoardResult;
        }

        /// <summary>
        /// Refresh method to force update of the station board
        /// </summary>
        public void Refresh()
        {
            BindStationBoard();
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Station board repeater ItemDataBound event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void stationBoardRptr_DataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                #region Header row

                #region Controls

                Label serviceTimeScheduledLbl = e.Item.FindControlRecursive<Label>("serviceTimeScheduledLbl");
                Label serviceStationLbl = e.Item.FindControlRecursive<Label>("serviceStationLbl");
                Label servicePlatformLbl = e.Item.FindControlRecursive<Label>("servicePlatformLbl");
                                
                #endregion

                // Heading labels
                if (showDepartures)
                {
                    serviceTimeScheduledLbl.Text = page.GetResource("StopInformation.StationBoard.Time.Departing");
                    serviceStationLbl.Text = page.GetResource("StopInformation.StationBoard.Location.Departing");
                }
                else
                {
                    serviceTimeScheduledLbl.Text = page.GetResource("StopInformation.StationBoard.Time.Arriving");
                    serviceStationLbl.Text = page.GetResource("StopInformation.StationBoard.Location.Arriving");
                }

                servicePlatformLbl.Text = page.GetResource("StopInformation.StationBoard.Platform"); ;

                #endregion
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem
                || e.Item.ItemType == ListItemType.SelectedItem)
            {
                #region Item row

                DBSStopEvent dse = e.Item.DataItem as DBSStopEvent;
                TrainStopEvent tse = null;

                if (dse is TrainStopEvent)
                    tse = (TrainStopEvent)dse;

                #region Station board view

                #region Controls

                HyperLink showDetailsLink = e.Item.FindControlRecursive<HyperLink>("showDetailsLink");

                HiddenField serviceId = e.Item.FindControlRecursive<HiddenField>("serviceId");
                Label serviceTimeScheduledLbl = e.Item.FindControlRecursive<Label>("serviceTimeScheduledLbl");
                Label serviceTimeActualLbl = e.Item.FindControlRecursive<Label>("serviceTimeActualLbl");
                Label serviceStationLbl = e.Item.FindControlRecursive<Label>("serviceStationLbl");
                Label servicePlatformLbl = e.Item.FindControlRecursive<Label>("servicePlatformLbl");
                Label serviceReportLbl = e.Item.FindControlRecursive<Label>("serviceReportLbl");

                #endregion

                // Set the station board labels
                serviceId.Value = (dse.Service != null) ? dse.Service.ServiceNumber : string.Empty;

                #region Time labels

                DateTime scheduledTime = DateTime.MinValue;
                DateTime actualTime = DateTime.MinValue;

                if (showDepartures)
                {
                    scheduledTime = dse.Stop.DepartTime;
                    actualTime = dse.Stop.RealTime.DepartTime;
                }
                else
                {
                    scheduledTime = dse.Stop.ArriveTime;
                    actualTime = dse.Stop.RealTime.ArriveTime;
                }

                if (scheduledTime != DateTime.MinValue)
                    serviceTimeScheduledLbl.Text = scheduledTime.ToString(timeFormat);

                if (actualTime != DateTime.MinValue)
                    serviceTimeActualLbl.Text = actualTime.ToString(timeFormat);
                else
                    serviceTimeActualLbl.Visible = false;

                #endregion

                #region Station label

                serviceStationLbl.Text = showDepartures ?
                    // Service station label is where the service ends at (its Arrival stop)
                    dse.Arrival.Stop.Name :
                    // Service station label is where the service starts from (its Departure stop)
                    dse.Departure.Stop.Name;

                #endregion

                #region Report label

                ServiceReportType serviceReportType = GetServiceTimeReport(dse, showDepartures);
                bool addLateStyle = false;

                switch (serviceReportType)
                {
                    case ServiceReportType.Late:
                        // If it is late, display a late report
                        int minutesLate = ServiceLateMinutes(dse, showDepartures);
                        if (minutesLate > 0)
                        {
                            if (minutesLate == 1)
                                serviceReportLbl.Text = string.Format(page.GetResource("StopInformation.StationBoard.Late.Minute"),
                                    minutesLate);
                            else
                                serviceReportLbl.Text = string.Format(page.GetResource("StopInformation.StationBoard.Late.Minutes"),
                                    minutesLate);

                            // Only apply late style if its over the minimum minutes late
                            if (minutesLate >= Properties.Current["StopInformation.StationBoard.MinutesLateErrorStyle.Minimum"].Parse(5))
                                addLateStyle = true;
                        }
                        break;
                    case ServiceReportType.Cancelled:
                        if (tse != null && !string.IsNullOrEmpty(tse.CancellationReason))
                            serviceReportLbl.Text = tse.CancellationReason;
                        else
                            serviceReportLbl.Text = page.GetResource("StopInformation.StationBoard.Cancelled");
                        addLateStyle = true;
                        break;
                    case ServiceReportType.Delayed:
                        serviceReportLbl.Text = page.GetResource("StopInformation.StationBoard.Delayed");
                        addLateStyle = true;
                        break;
                    case ServiceReportType.Unknown:
                        serviceReportLbl.Text = page.GetResource("StopInformation.StationBoard.NoReport");
                        addLateStyle = true;
                        break;
                    case ServiceReportType.OnTime:
                    default:
                        serviceReportLbl.Text = page.GetResource("StopInformation.StationBoard.OnTime");

                        // If service is "On Time", then hide the actual time label
                        serviceTimeActualLbl.Visible = false;
                        break;
                }

                // Update styles to indicate late
                if (addLateStyle)
                {
                    // Update the report label style to indicate not on time
                    if (!serviceReportLbl.CssClass.Contains("reportLate"))
                        serviceReportLbl.CssClass = string.Format("{0} reportLate", serviceReportLbl.CssClass);

                    // Update the actual time style to indicate not on time
                    if (!serviceTimeActualLbl.CssClass.Contains("timeActualLate"))
                        serviceTimeActualLbl.CssClass = string.Format("{0} timeActualLate", serviceTimeActualLbl.CssClass);
                }

                #endregion

                #region Platform label

                // TrainStopEvent specific details
                if (tse != null)
                {
                    if (!string.IsNullOrEmpty(tse.Platform))
                        servicePlatformLbl.Text = tse.Platform;
                    else
                        servicePlatformLbl.Visible = false;
                }

                #endregion

                // Service details link
                showDetailsLink.ToolTip = page.GetResource("StopInformation.StationBoard.ShowService.ToolTip");
                showDetailsLink.NavigateUrl = helper.BuildStopInformationDetailUrl(page, serviceId.Value);

                #endregion

                #endregion
            }
        }

        #region Click events

        /// <summary>
        /// Handler for the show departures button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDepartures_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                showDepartures = true;

                // Raise event to tell subscribers that show station board direction button has been selected
                if (ShowDirectionHandler != null)
                {
                    ShowDirectionHandler(sender, new StationBoardEventArgs(true));
                }
            }
        }

        /// <summary>
        /// Handler for the show arrivals button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnArrivals_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                showDepartures = false;

                // Raise event to tell subscribers that show station board direction button has been selected
                if (ShowDirectionHandler != null)
                {
                    ShowDirectionHandler(sender, new StationBoardEventArgs(false));
                }
            }
        }

        #endregion

        #endregion

        #region Private methods

        /// <summary>
        /// Setup controls on the page
        /// </summary>
        private void SetupControls()
        {
            if (location != null)
            {
                // Departure/Arrivals buttons
                btnDepartures.Text = page.GetResource("StopInformation.StationBoard.ShowDepartures.Text");
                btnDepartures.ToolTip = string.Format(page.GetResource("StopInformation.StationBoard.ShowDepartures.ToolTip"), location.DisplayName);

                btnArrivals.Text = page.GetResource("StopInformation.StationBoard.ShowArrivals.Text");
                btnArrivals.ToolTip = string.Format(page.GetResource("StopInformation.StationBoard.ShowArrivals.ToolTip"), location.DisplayName);

                // Update the selected direction button style
                if (showDepartures)
                {
                    if (!directionDepartureDiv.Attributes["class"].Contains("directionSelected"))
                        directionDepartureDiv.Attributes["class"] = string.Format("{0} directionSelected", directionDepartureDiv.Attributes["class"]);
                    if (directionArrivalDiv.Attributes["class"].Contains("directionSelected"))
                        directionArrivalDiv.Attributes["class"] = directionArrivalDiv.Attributes["class"].Replace("directionSelected", string.Empty);
                }
                else
                {
                    if (!directionArrivalDiv.Attributes["class"].Contains("directionSelected"))
                        directionArrivalDiv.Attributes["class"] = string.Format("{0} directionSelected", directionArrivalDiv.Attributes["class"]);
                    if (directionDepartureDiv.Attributes["class"].Contains("directionSelected"))
                        directionDepartureDiv.Attributes["class"] = directionDepartureDiv.Attributes["class"].Replace("directionSelected", string.Empty);
                }

                // Update link
                stationBoardUpdateLink.Text = page.GetResource("StopInformation.StationBoard.Update.Text");
                stationBoardUpdateLink.ToolTip = string.Format(page.GetResource("StopInformation.StationBoard.Update.ToolTip"), location.DisplayName);

                stationBoardUpdateLink.NavigateUrl = helper.BuildStopInformationUrl(
                    page, 
                    location, 
                    showDepartures ? StopInformationMode.StationBoardDeparture : StopInformationMode.StationBoardArrival);
            }
        }
        
        /// <summary>
        /// Gets the station board using current stop location and binds it to the station board repeater
        /// </summary>
        private void BindStationBoard()
        {
            bool resultsAvailable = false;

            if (stationBoardResult != null && stationBoardResult.StopEvents != null && stationBoardResult.StopEvents.Length > 0)
            {
                // Station board result available
                stationBoardUnavailableDiv.Visible = false;
                stationBoardDiv.Visible = true;

                // Bind station board
                stationBoardRptr.DataSource = stationBoardResult.StopEvents;
                stationBoardRptr.DataBind();

                stationBoardLastUpdatedLbl.Text = string.Format(
                    page.GetResource("StopInformation.StationBoard.LastUpdated.Text"),
                    stationBoardResult.GeneratedAt.ToString(timeFormat));

                resultsAvailable = true;
            }

            if (!resultsAvailable)
            {
                // Station board result unavailable for this location
                stationBoardUnavailableDiv.Visible = true;
                stationBoardDiv.Visible = false;

                stationBoardUnavailableLbl.Text = string.Format(
                    page.GetResource("StopInformation.StationBoard.Unavailable"),
                    location.DisplayName);
            }
        }

        /// <summary>
        /// Method returns true if the service is on time (scheduled and actual times are the same)
        /// </summary>
        /// <param name="dbsStopEvent"></param>
        /// <param name="isDeparture"></param>
        /// <returns></returns>
        private ServiceReportType GetServiceTimeReport(DBSStopEvent dbsStopEvent, bool isDeparture)
        {
            ServiceReportType serviceReportType = ServiceReportType.OnTime;

            if (dbsStopEvent != null)
            {
                TrainStopEvent trainStopEvent = null;

                if (dbsStopEvent is TrainStopEvent)
                    trainStopEvent = (TrainStopEvent)dbsStopEvent;

                // Check if train is cancelled marker
                if (trainStopEvent != null && trainStopEvent.Cancelled)
                    serviceReportType = ServiceReportType.Cancelled;
                else
                {
                    TrainRealTime trainRealTime = null;

                    if (dbsStopEvent.Stop.RealTime is TrainRealTime)
                        trainRealTime = (TrainRealTime)dbsStopEvent.Stop.RealTime;

                    if (trainRealTime != null && (trainRealTime.Uncertain || trainRealTime.Delayed))
                    {
                        // Check if train has delayed or uncertain markers
                        if (trainRealTime.Uncertain)
                            serviceReportType = ServiceReportType.Unknown;
                        else if (trainRealTime.Delayed)
                            serviceReportType = ServiceReportType.Delayed;
                    }
                    else
                    {
                        DateTime scheduledTime = DateTime.MinValue;
                        DateTime actualTime = DateTime.MinValue;

                        if (isDeparture)
                        {
                            scheduledTime = dbsStopEvent.Stop.DepartTime;
                            actualTime = dbsStopEvent.Stop.RealTime.DepartTime;
                        }
                        else
                        {
                            scheduledTime = dbsStopEvent.Stop.ArriveTime;
                            actualTime = dbsStopEvent.Stop.RealTime.ArriveTime;
                        }

                        // Assume there is always a scheduled time
                        if (actualTime == DateTime.MinValue && scheduledTime == DateTime.MinValue)
                        {
                            // Both times haven't been set, something probably wrong
                            serviceReportType = ServiceReportType.Unknown;
                        }
                        else if (actualTime != DateTime.MinValue)
                        {
                            TimeSpan timeDifference = actualTime.Subtract(scheduledTime);

                            if (timeDifference.TotalMinutes <= 0)
                            {
                                // Scheduled and actual times are same (or service is early), therefore "on time"
                                serviceReportType = ServiceReportType.OnTime;
                            }
                            else
                            {
                                serviceReportType = ServiceReportType.Late;
                            }
                        }
                    }
                }
            }

            return serviceReportType;
        }

        /// <summary>
        /// Method returns the number of minutes late a service is (if early then value will be negative)
        /// </summary>
        /// <param name="dbsStopEvent"></param>
        /// <param name="isDeparture"></param>
        /// <returns></returns>
        private int ServiceLateMinutes(DBSStopEvent dbsStopEvent, bool isDeparture)
        {
            int minutes = 0;

            if (dbsStopEvent != null)
            {
                DateTime scheduledTime = DateTime.MinValue;
                DateTime actualTime = DateTime.MinValue;

                if (isDeparture)
                {
                    scheduledTime = dbsStopEvent.Stop.DepartTime;
                    actualTime = dbsStopEvent.Stop.RealTime.DepartTime;
                }
                else
                {
                    scheduledTime = dbsStopEvent.Stop.ArriveTime;
                    actualTime = dbsStopEvent.Stop.RealTime.ArriveTime;
                }

                TimeSpan timeDifference = actualTime.Subtract(scheduledTime);

                minutes = Convert.ToInt32(timeDifference.TotalMinutes);
            }

            return minutes;
        }
        
        #endregion
    }
}
