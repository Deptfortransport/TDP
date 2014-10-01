// *********************************************** 
// NAME             : ServiceDetailsControl.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 17 Feb 2014
// DESCRIPTION  	: ServiceDetailsControl to display calling points for a service 
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TDP.Common.Extenders;
using TDP.Common.LocationService;
using TDP.Common.PropertyManager;
using TDP.Common.ResourceManager;
using TDP.Common.Web;
using TDP.UserPortal.DepartureBoardService.DepartureBoardServiceTypes;
using TDP.UserPortal.JourneyControl;

namespace TDP.UserPortal.TDPMobile.Controls
{
    public partial class ServiceDetailsControl : System.Web.UI.UserControl
    {
        #region Private Fields

        private string serviceId = null;
        private DBSResult serviceResult = null;
        private List<TrainCallingPoint> serviceCallingPoints = new List<TrainCallingPoint>();
        private TDPPageMobile page;

        private const string timeFormat = "HH:mm"; // "dd/MM/yyyy HH:mm"

        #region TrainCallingPoint class

        /// <summary>
        /// Class to store Train calling point details for display
        /// </summary>
        private class TrainCallingPoint : JourneyCallingPoint
        {
            #region Private variables

            private bool isArrived = false;
            private bool isDeparted = false;
            private bool isOnTime = false;
            private bool isLate = false;
            private bool isDelayed = false;
            private bool isUncertain = false;
            private bool isCancelled = false;
            private TimeSpan timeDifference = new TimeSpan(0, 0, 0);

            private DateTime timeActual = DateTime.MinValue;

            #endregion

            #region Constructor

            /// <summary>
            /// Constructor
            /// </summary>
            public TrainCallingPoint()
            {
            }

            #endregion

            #region Public properties

            /// <summary>
            /// Read/Write. Is arrived flag at calling point
            /// </summary>
            public bool IsArrived { get { return isArrived; } set { isArrived = value; } }

            /// <summary>
            /// Read/Write. Is departed flag from calling point
            /// </summary>
            public bool IsDeparted { get { return isDeparted; } set { isDeparted = value; } }

            /// <summary>
            /// Read/Write. Is on time flag
            /// </summary>
            public bool IsOnTime { get { return isOnTime; } set { isOnTime = value; } }

            /// <summary>
            /// Read/Write. Is late flag
            /// </summary>
            public bool IsLate { get { return isLate; } set { isLate = value; } }

            /// <summary>
            /// Read/Write. Is delayed flag for calling point
            /// </summary>
            public bool IsDelayed { get { return isDelayed; } set { isDelayed = value; } }

            /// <summary>
            /// Read/Write. Is uncertain flag for calling point
            /// </summary>
            public bool IsUncertain { get { return isUncertain; } set { isUncertain = value; } }

            /// <summary>
            /// Read/Write. Is cancelled flag from calling point
            /// </summary>
            public bool IsCancelled { get { return isCancelled; } set { isCancelled = value; } }

            /// <summary>
            /// Read/Write. Difference timespan from scheduled departure time
            /// </summary>
            public TimeSpan TimeDifference { get { return timeDifference; } set { timeDifference = value; } }

            /// <summary>
            /// Read/Write. Actual time to display for late trains
            /// </summary>
            public DateTime TimeActual { get { return timeActual; } set { timeActual = value; } }

            #endregion
        }

        #endregion

        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
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
        }

        #endregion

        #region Control Event Handlers

        /// <summary>
        /// Service leg repeater data bound event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void serviceLegRptr_DataBound(object sender, RepeaterItemEventArgs e)
        {
            TrainCallingPoint callingPoint = null;

            #region Controls

            Label pointScheduledTime = e.Item.FindControlRecursive<Label>("pointScheduledTime");
            Label pointActualTime = e.Item.FindControlRecursive<Label>("pointActualTime");
            Label pointLocation = e.Item.FindControlRecursive<Label>("pointLocation");
            Label pointReport = e.Item.FindControlRecursive<Label>("pointReport");
            Image legNodeImage = e.Item.FindControlRecursive<Image>("legNodeImage");
            Image pointImage = e.Item.FindControlRecursive<Image>("pointImage");

            HtmlGenericControl columnNode = e.Item.FindControlRecursive<HtmlGenericControl>("columnNode");

            #endregion
            
            if ( e.Item.ItemType == ListItemType.AlternatingItem
                || e.Item.ItemType == ListItemType.Item
                || e.Item.ItemType == ListItemType.SelectedItem)
            {

                callingPoint = e.Item.DataItem as TrainCallingPoint;
            }

            if (callingPoint != null)
            {
                #region Location

                pointLocation.Text = callingPoint.Location.DisplayName;

                #endregion

                #region Time scheduled

                pointScheduledTime.Text = (callingPoint.DepartureDateTime != DateTime.MinValue) ?
                    callingPoint.DepartureDateTime.ToString(timeFormat) :
                    callingPoint.ArrivalDateTime.ToString(timeFormat);

                #endregion

                #region Report label

                int minutesLate = Convert.ToInt32((callingPoint.TimeDifference != null) ? callingPoint.TimeDifference.TotalMinutes : 0);
                int minutesLateMin = Properties.Current["StopInformation.StationBoard.MinutesLateErrorStyle.Minimum"].Parse(5);

                if (callingPoint.IsOnTime && callingPoint.IsDeparted)
                {
                    pointReport.Text = page.GetResource("StopInformation.ServiceDetail.OnTime.Departed");
                }
                else if (callingPoint.IsOnTime && callingPoint.IsArrived)
                {
                    pointReport.Text = page.GetResource("StopInformation.ServiceDetail.OnTime.Arrived"); 
                }
                else if (callingPoint.IsLate && callingPoint.IsDeparted)
                {
                    pointReport.Text = string.Format(
                        (minutesLate == 1) ?
                        page.GetResource("StopInformation.ServiceDetail.Late.Minute.Departed") :
                        page.GetResource("StopInformation.ServiceDetail.Late.Minutes.Departed"),
                        minutesLate);

                    // Only apply late style if its over the minimum minutes late
                    if (minutesLate >= minutesLateMin)
                        pointReport.CssClass = string.Format("{0} reportLate", pointReport.CssClass);
                }
                else if (callingPoint.IsLate && !callingPoint.IsDeparted && callingPoint.IsArrived
                    && callingPoint.TimeActual != DateTime.MinValue)
                {
                    pointReport.Text = string.Format(
                        (minutesLate == 1) ?
                        page.GetResource("StopInformation.ServiceDetail.Late.Minute.Arrived") :
                        page.GetResource("StopInformation.ServiceDetail.Late.Minutes.Arrived"),
                        minutesLate);

                    // Only apply late style if its over the minimum minutes late
                    if (minutesLate >= minutesLateMin)
                        pointReport.CssClass = string.Format("{0} reportLate", pointReport.CssClass);
                }
                else if (callingPoint.IsLate && !callingPoint.IsDeparted && !callingPoint.IsArrived
                    && callingPoint.TimeActual != DateTime.MinValue)
                {
                    pointActualTime.Text = callingPoint.TimeActual.ToString(timeFormat);

                    // Only apply late style if its over the minimum minutes late
                    if (callingPoint.TimeDifference.TotalMinutes >= minutesLateMin)
                        pointActualTime.CssClass = string.Format("{0} timeActualLate", pointActualTime.CssClass);
                }
                else if (callingPoint.IsCancelled)
                {
                    pointReport.Text = page.GetResource("StopInformation.ServiceDetail.Cancelled");

                    pointReport.CssClass = string.Format("{0} reportLate", pointReport.CssClass);
                }
                else if (callingPoint.IsDelayed)
                {
                    pointReport.Text = page.GetResource("StopInformation.ServiceDetail.Delayed");

                    pointReport.CssClass = string.Format("{0} reportLate", pointReport.CssClass);
                }

                if (!callingPoint.IsOnTime && !callingPoint.IsLate && !callingPoint.IsCancelled 
                    && !callingPoint.IsDelayed)
                {
                    pointReport.Text = page.GetResource("StopInformation.ServiceDetail.NoReport");
                }

                if (callingPoint.IsUncertain)
                {
                    if (!string.IsNullOrEmpty(pointReport.Text))
                        pointReport.Text = string.Format("{0}{1}", 
                            pointReport.Text,
                            page.GetResource("StopInformation.ServiceDetail.Uncertain"));
                }

                // If report label has no text, hide it
                if (string.IsNullOrEmpty(pointReport.Text))
                {
                    pointReport.Visible = false;

                    // Also update style on location label
                    pointLocation.CssClass = string.Format("{0} stationOnly", pointLocation.CssClass);
                }

                // If actual time not displayed, hide
                if (string.IsNullOrEmpty(pointActualTime.Text))
                {
                    pointActualTime.Visible = false;

                    // Also update style on time label
                    pointScheduledTime.CssClass = string.Format("{0} timeScheduledOnly", pointScheduledTime.CssClass);
                }

                #endregion

                #region Image node

                // Used for placing image of which calling point the service is at
                TrainCallingPoint previousCallingPoint = null;
                
                string imageUrlResourceId = string.Empty;
                string altTextResourceId = string.Empty;

                // Set the departed flag based on this and subsequent calling points as this may be a no report
                bool hasDeparted = HasDeparted(e.Item.ItemIndex); 

                if (e.Item.ItemIndex == 0)
                {
                    // Service origin
                    imageUrlResourceId = hasDeparted ?
                        "JourneyOutput.Image.CallingPoint.Start.Departed.ImageURL" :
                        "JourneyOutput.Image.CallingPoint.Start.ImageURL";
                    altTextResourceId = "JourneyOutput.Image.CallingPoint.Start.AltText";

                    columnNode.Attributes["class"] = hasDeparted ?
                        string.Format("{0} columnNodeStartDeparted", columnNode.Attributes["class"]) :
                        string.Format("{0} columnNodeStart", columnNode.Attributes["class"]);
                }
                else if (e.Item.ItemIndex == serviceCallingPoints.Count - 1)
                {
                    // Service destination
                    imageUrlResourceId = callingPoint.IsArrived ?
                        "JourneyOutput.Image.CallingPoint.End.Arrived.ImageURL" :
                        "JourneyOutput.Image.CallingPoint.End.ImageURL";
                    altTextResourceId = "JourneyOutput.Image.CallingPoint.End.AltText";

                    columnNode.Attributes["class"] = callingPoint.IsArrived ?
                        string.Format("{0} columnNodeEndArrived", columnNode.Attributes["class"]) :
                        string.Format("{0} columnNodeEnd", columnNode.Attributes["class"]);

                    previousCallingPoint = serviceCallingPoints[e.Item.ItemIndex - 1];
                }
                else
                {
                    // Service calling point
                    imageUrlResourceId = hasDeparted ?
                        "JourneyOutput.Image.CallingPoint.Departed.ImageURL" :
                        callingPoint.IsArrived ? "JourneyOutput.Image.CallingPoint.Arrived.ImageURL" : "JourneyOutput.Image.CallingPoint.ImageURL";

                    altTextResourceId = "JourneyOutput.Image.CallingPoint.AltText";

                    if (callingPoint.IsArrived && !hasDeparted)
                    {
                        columnNode.Attributes["class"] = string.Format("{0} columnNodeArrived", columnNode.Attributes["class"]);
                    }
                    else if (hasDeparted)
                    {
                        columnNode.Attributes["class"] = string.Format("{0} columnNodeDeparted", columnNode.Attributes["class"]);
                    }

                    previousCallingPoint = serviceCallingPoints[e.Item.ItemIndex - 1];
                }

                legNodeImage.ImageUrl = page.ImagePath + page.GetResource(TDPResourceManager.GROUP_JOURNEYOUTPUT, TDPResourceManager.COLLECTION_JOURNEY, imageUrlResourceId);
                legNodeImage.AlternateText = page.GetResource(TDPResourceManager.GROUP_JOURNEYOUTPUT, TDPResourceManager.COLLECTION_JOURNEY, altTextResourceId);
                legNodeImage.ToolTip = legNodeImage.AlternateText;

                // Point image, this is the image indicating where the service currently is
                pointImage.Visible = false;
                if (!callingPoint.IsArrived && !hasDeparted && previousCallingPoint != null && previousCallingPoint.IsDeparted)
                {
                    // Service has departed the previous calling point but has not arrived
                    pointImage.Visible = true;

                    pointImage.CssClass = string.Format("{0} pointImageDeparted", pointImage.CssClass);
                }
                else if (callingPoint.IsArrived && !callingPoint.IsDeparted)
                {
                    // Service is at the calling point
                    pointImage.Visible = true;
                }
                
                pointImage.ImageUrl = page.ImagePath + page.GetResource(TDPResourceManager.GROUP_JOURNEYOUTPUT, TDPResourceManager.COLLECTION_JOURNEY,
                        "JourneyOutput.Image.CallingPoint.Point.ImageURL");
                pointImage.GenerateEmptyAlternateText = true;
                pointImage.ToolTip = string.Empty;

                #endregion
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialise method
        /// </summary>
        public void Initialise(string serviceId, DBSResult serviceResult)
        {
            this.serviceId = serviceId;
            this.serviceResult = serviceResult;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method which setup the controls
        /// </summary>
        private void SetupControls()
        {
            bool resultsAvailable = false;

            if (serviceResult != null && serviceResult.StopEvents != null && serviceResult.StopEvents.Length > 0)
            {
                // There should only be one stop event for a service detail result
                DBSStopEvent stopEvent = serviceResult.StopEvents[0];

                // Setup the service calling points
                serviceCallingPoints = BuildServiceCallingPoints(stopEvent);

                // Service result available
                serviceUnavailableDiv.Visible = false;
                serviceDiv.Visible = true;

                // Bind service details
                serviceLegRptr.DataSource = serviceCallingPoints;
                serviceLegRptr.DataBind();

                // Title
                string title = string.Format(page.GetResource("StopInformation.ServiceDetail.Title"),
                    stopEvent.Departure.DepartTime.ToString(timeFormat),
                    stopEvent.Departure.Stop.Name,
                    stopEvent.Arrival.Stop.Name);
                serviceTitle.Text = title;

                serviceLastUpdatedLbl.Text = string.Format(page.GetResource("StopInformation.ServiceDetail.LastUpdated.Text"),
                    serviceResult.GeneratedAt.ToString(timeFormat));

                // Message
                if (stopEvent is TrainStopEvent)
                {
                    TrainStopEvent trainStopEvent = (TrainStopEvent)stopEvent;

                    if (!string.IsNullOrEmpty(trainStopEvent.CancellationReason))
                        serviceMessageLbl.Text = trainStopEvent.CancellationReason;
                    else if (!string.IsNullOrEmpty(trainStopEvent.LateReason))
                        serviceMessageLbl.Text = trainStopEvent.LateReason;
                }

                if (string.IsNullOrEmpty(serviceMessageLbl.Text))
                {
                    serviceMessageDiv.Visible = false;
                }

                // Operator
                if (stopEvent.Service != null)
                {
                    serviceOperatedLbl.Text = string.Format(page.GetResource("StopInformation.ServiceDetail.OperatedBy.Text"),
                        string.IsNullOrEmpty(stopEvent.Service.OperatorName) ?
                            stopEvent.Service.OperatorCode :
                            stopEvent.Service.OperatorName);
                }

                // Update link
                serviceUpdateLink.Text = page.GetResource("StopInformation.ServiceDetail.Update.Text");
                serviceUpdateLink.ToolTip = string.Format(page.GetResource("StopInformation.ServiceDetail.Update.ToolTip"));

                StopInformationHelper helper = new StopInformationHelper();

                serviceUpdateLink.NavigateUrl = helper.BuildStopInformationDetailUrl(
                    page,
                    serviceId);

                resultsAvailable = true;
            }
            

            if (!resultsAvailable)
            {
                // Service result unavailable for this service id
                serviceUnavailableDiv.Visible = true;
                serviceDiv.Visible = false;

                serviceUnavailableLbl.Text = page.GetResource("StopInformation.ServiceDetail.Unavailable");
            }
        }

        #region Service helpers

        /// <summary>
        /// Builds a list of train calling points
        /// </summary>
        /// <param name="stopEvent"></param>
        /// <returns></returns>
        private List<TrainCallingPoint> BuildServiceCallingPoints(DBSStopEvent stopEvent)
        {
            List<TrainCallingPoint> callingPoints = new List<TrainCallingPoint>();

            TrainCallingPoint callingPoint = null;
            TrainCallingPoint previousCallingPoint = null;

            // Service origin
            callingPoint = BuildCallingPoint(stopEvent.Departure, true, false, true, false, previousCallingPoint);
            callingPoints.Add(callingPoint);
            previousCallingPoint = callingPoint;
            
            if (stopEvent.PreviousIntermediates != null)
            {
                foreach (DBSEvent stop in stopEvent.PreviousIntermediates)
                {
                    callingPoint = BuildCallingPoint(stop, false, false, true, false, previousCallingPoint);
                    callingPoints.Add(callingPoint);
                    previousCallingPoint = callingPoint;
                }
            }

            // Service stop relating to the station board this service was retrieved for
            if (stopEvent.Stop != null)
            {
                // Check if the station board stop is either the departure/arrival station,
                // if it is then do not add it otherwise there will be a duplicate station in the output
                if (stopEvent.Stop.Stop.NaptanId != stopEvent.Departure.Stop.NaptanId
                    && stopEvent.Stop.Stop.NaptanId != stopEvent.Arrival.Stop.NaptanId)
                {
                    callingPoint = BuildCallingPoint(stopEvent.Stop, false, false, true, true, previousCallingPoint);
                    callingPoints.Add(callingPoint);
                    previousCallingPoint = callingPoint;
                }
            }

            if (stopEvent.OnwardIntermediates != null)
            {
                foreach (DBSEvent stop in stopEvent.OnwardIntermediates)
                {
                    callingPoint = BuildCallingPoint(stop, false, false, false, true, previousCallingPoint);
                    callingPoints.Add(callingPoint);
                    previousCallingPoint = callingPoint;
                }
            }

            // Service destination
            callingPoint = BuildCallingPoint(stopEvent.Arrival, false, true, false, true, previousCallingPoint);
            callingPoints.Add(callingPoint);
                        
            return callingPoints;
        }

        /// <summary>
        /// Builds a train calling point
        /// </summary>
        /// <param name="stop"></param>
        /// <param name="isOrigin"></param>
        /// <param name="isDestination"></param>
        /// <param name="isPrevious"></param>
        /// <param name="isSubsequent"></param>
        /// <returns></returns>
        private TrainCallingPoint BuildCallingPoint(DBSEvent stop, bool isOrigin, bool isDestination,
            bool isPrevious, bool isSubsequent, TrainCallingPoint callingPointPrevious)
        {
            TrainCallingPoint callingPoint = new TrainCallingPoint();

            // Calling point location details
            callingPoint.Location = new TDPLocation(stop.Stop.Name, TDPLocationType.Station, TDPLocationType.StationRail, stop.Stop.NaptanId);
            
            if (isOrigin)
                callingPoint.Type = JourneyCallingPointType.Origin;
            else if (isDestination)
                callingPoint.Type = JourneyCallingPointType.Destination;
            else
                callingPoint.Type = JourneyCallingPointType.CallingPoint;

            // Scheduled times, if it is before the Stop of the station board this service applies to, 
            // then there wil be a departure time, otherwise an arrival time.
            // The stop of the station board will have both departure and arrival
            callingPoint.DepartureDateTime = stop.DepartTime; 
            callingPoint.ArrivalDateTime = stop.ArriveTime;

            // Departures (before station board stop)
            if (stop.RealTime.DepartTime != DateTime.MinValue)
            {
                #region Real time - departure

                // (Default) As the depart time is still estimated, then not sure it has arrived or departed
                callingPoint.IsArrived = false;
                callingPoint.IsDeparted = false;

                if (stop.RealTime.DepartTimeType == DBSRealTimeType.Recorded)
                {
                    // Recorded depart time indicates train has left stop (so it would have arrived)
                    callingPoint.IsArrived = true;
                    callingPoint.IsDeparted = true;
                }
                
                // On time or late departing
                if (stop.RealTime.DepartTime == stop.DepartTime)
                {
                    callingPoint.IsOnTime = true;
                }
                else if (stop.RealTime.DepartTime > stop.DepartTime)
                {
                    callingPoint.IsLate = true;
                    callingPoint.TimeActual = stop.RealTime.DepartTime;
                    callingPoint.TimeDifference = (stop.RealTime.DepartTime.Subtract(stop.DepartTime));
                }

                if (stop.RealTime.ArriveTime != DateTime.MinValue)
                {
                    // Could be for the station board stop which may also have an arrival time, if
                    // recorded then its arrived and awaiting departure
                    if (stop.RealTime.ArriveTimeType == DBSRealTimeType.Recorded)
                        callingPoint.IsArrived = true;
                }

                #endregion
            }
        
            // Arrivals (after station board stop)
            if (stop.RealTime.ArriveTime != DateTime.MinValue)
            {
                #region Real time - arrival

                // Departure time could exist so the arrived/departed flag may have already been set above
                if (!callingPoint.IsArrived)
                    callingPoint.IsArrived = false;
                if (!callingPoint.IsDeparted)
                    callingPoint.IsDeparted = false;

                if (stop.RealTime.ArriveTimeType == DBSRealTimeType.Recorded)
                {
                    // Recorded arrival time indicates the train has arrived at the stop
                    callingPoint.IsArrived = true;

                    // If the calling point is after the station board stop, and the arrival time is recorded,
                    // then assume train has also departed (as there will be no departure time) 
                    // and only if the previous calling point also has a recorded departure
                    if (isSubsequent && !isDestination)
                    {
                        if (callingPointPrevious != null && callingPointPrevious.IsDeparted)
                            callingPoint.IsDeparted = true;
                    }
                }

                if (stop.RealTime.ArriveTime <= stop.ArriveTime)
                {
                    // Check if train is late because departed late from previous station late (set above),
                    // if it is departing late then can't also be ontime
                    if (!callingPoint.IsLate)
                        callingPoint.IsOnTime = true;

                    if (callingPoint.TimeActual == DateTime.MinValue)
                        callingPoint.TimeActual = stop.RealTime.ArriveTime;
                }
                else if (stop.RealTime.ArriveTime > stop.ArriveTime)
                {
                    callingPoint.IsLate = true;
                    callingPoint.TimeActual = stop.RealTime.ArriveTime;
                    callingPoint.TimeDifference = (stop.RealTime.ArriveTime.Subtract(stop.ArriveTime));
                }

                #endregion
            }
            
            if (stop.RealTime is TrainRealTime)
            {
                // Check for delayed and uncertain
                callingPoint.IsDelayed = ((TrainRealTime)stop.RealTime).Delayed;
                callingPoint.IsUncertain = ((TrainRealTime)stop.RealTime).Uncertain;
            }

            if (stop.Cancelled)
                callingPoint.IsCancelled = true;

            return callingPoint;
        }

        /// <summary>
        /// Method returns if the calling point has departed when it has a no report,
        /// by checking the subsequent calling points for a departed status
        /// </summary>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        private bool HasDeparted(int currentIndex)
        {
            if (serviceCallingPoints == null || serviceCallingPoints.Count <= 0 || serviceCallingPoints.Count < currentIndex)
            {
                // No calling points to check
                return false;
            }

            TrainCallingPoint callingPoint = serviceCallingPoints[currentIndex];

            // Flag indicates it has departed
            if (callingPoint.IsDeparted)
                return true;

            // Flag indicates it has arrived, but not departed
            if (callingPoint.IsArrived)
                return false;

            // Otherwise, if there is no other indication of a report
            if (!callingPoint.IsOnTime && !callingPoint.IsLate && !callingPoint.IsCancelled 
                && !callingPoint.IsDelayed)
            {
                TrainCallingPoint nextCallingPoint = null;

                // Check the subsequent calling points for an arrive/depart flag
                for (int i = currentIndex + 1; i < serviceCallingPoints.Count; i++)
                {
                    nextCallingPoint = serviceCallingPoints[i];

                    if (nextCallingPoint.IsArrived)
                        return true;
                    if (nextCallingPoint.IsDeparted)
                        return true;
                }
            }

            return false;
        }

        #endregion

        #endregion
    }
}