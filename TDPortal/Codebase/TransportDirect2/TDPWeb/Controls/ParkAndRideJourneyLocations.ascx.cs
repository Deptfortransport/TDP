// *********************************************** 
// NAME             : ParkAndRideJourneyLocations.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 29 Apr 2011
// DESCRIPTION  	: Represents park and ride and blue badge venue car parks options
// ************************************************


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TDP.Common;
using TDP.Common.Extenders;
using TDP.Common.LocationService;
using TDP.Common.PropertyManager;
using TDP.Common.ServiceDiscovery;
using TDP.Common.Web;

namespace TDP.UserPortal.TDPWeb.Controls
{
    /// <summary>
    /// Represents park and ride and blue badge venue car parks options
    /// </summary>
    public partial class ParkAndRideJourneyLocations : System.Web.UI.UserControl
    {
        #region Private Fields

        private TDPVenueLocation venue;
        private DateTime dateTimeOutward = DateTime.MinValue;
        private DateTime dateTimeReturn = DateTime.MinValue;
        private List<TDPVenueCarPark> carParkList;
        private bool isBlueBadge;

        #endregion

        #region Public Properties

        /// <summary>
        /// venue location for which Park and Ride or Blue Badge car park options required
        /// </summary>
        public TDPVenueLocation Venue
        {
            get { return venue; }
            set { venue = value; }
        }

        /// <summary>
        /// Read/Write. Outward date time to use for filtering car parks
        /// </summary>
        public DateTime OutwardDateTime
        {
            get { return dateTimeOutward; }
            set { dateTimeOutward = value; }
        }

        /// <summary>
        /// Read/Write. Return date time to use for filtering car parks
        /// </summary>
        public DateTime ReturnDateTime
        {
            get { return dateTimeReturn; }
            set { dateTimeReturn = value; }
        }
                
        /// <summary>
        /// Read only property to represent venue location object with only selected car park.
        /// The property returns a clone of venue location with all the other car parks removed from the list.
        /// </summary>
        public TDPVenueCarPark SelectedCarPark
        {
            get
            {
                TDPVenueCarPark selectedCarPark = null;

                if (carParkList != null)
                {
                    selectedCarPark = carParkList.SingleOrDefault(cp => cp.ID == preferredParksOptions.SelectedValue);
                }

                return selectedCarPark;
            }
        }

        /// <summary>
        /// Read only property to return the selected outward date time updated with the time slot value 
        /// </summary>
        public DateTime SelectedOutwardDateTime
        {
            get
            {
                DateTime selectedOutwardDateTime = dateTimeOutward;

                // Update the outward time to be the selected time slot time
                if (selectedOutwardDateTime != DateTime.MinValue)
                    selectedOutwardDateTime = GetTimeSlotDateTime(false);

                return selectedOutwardDateTime;
            }
        }

        /// <summary>
        /// Read only property to return the selected return date time updated with the time slot value
        /// </summary>
        public DateTime SelectedReturnDateTime
        {
            get
            {
                // Update the return time to be the return datetime + any transit times from 
                // venue to the car park
                DateTime selectedReturnDateTime = dateTimeReturn;

                if (selectedReturnDateTime != DateTime.MinValue)
                    selectedReturnDateTime = selectedReturnDateTime.Add(GetTransitTime(true));

                return selectedReturnDateTime;
            }
        }

        /// <summary>
        /// Read only. Returns true is car parks found for venue
        /// </summary>
        public bool IsCarParksAvailable
        {
            get { return carParkList != null; }
        }

        /// <summary>
        /// Read/Write. IsBlueBadge
        /// </summary>
        public bool IsBlueBadge 
        {
            get { return isBlueBadge; }
            set { isBlueBadge = value; }
        }

        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SetupCarParks();

            DisplayMessages();
        }

        /// <summary>
        /// Page PreRender event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupResources();
            
            if (!IsPostBack)
            {
                SetupVenueMap();
                SetupVenueDropDown();
                SetupVenueCarParkTimeSlotDropDown();
            }

            // Only display dropdowns/labels if car parks found
            if (carParkList != null && carParkList.Count > 0)
            {
                pnlParkAndRideCarParks.Visible = true;
            }
            else
            {
                pnlParkAndRideCarParks.Visible = false;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets any messages to show based on validation of car parks and travel dates/times
        /// </summary>
        private void DisplayMessages()
        {
            TDPMessage tdpMessage = null;

            // Display message if outward and return dates are different
            if ((dateTimeOutward != DateTime.MinValue && dateTimeReturn != DateTime.MinValue) &&
                (!dateTimeOutward.Date.Equals(dateTimeReturn.Date)))
            {
                if (!isBlueBadge)
                {
                    tdpMessage = new TDPMessage("ParkAndRideJourneyLocations.ParkAndRideReturnDateDifferent.Text", TDPMessageType.Warning);
                }
                else
                {
                    tdpMessage = new TDPMessage("ParkAndRideJourneyLocations.BlueBadgeReturnDateDifferent.Text", TDPMessageType.Warning);
                }

                ((TDPWeb)this.Page.Master).DisplayMessage(tdpMessage);
            }

            // Display message if no car parks found, must be placed in Load method 
            // so Master page can display it
            if (venue != null && carParkList == null)
            {
                if (isBlueBadge)
                {
                    tdpMessage = new TDPMessage("ParkAndRideJourneyLocations.BlueBadgeNoneFound.Text", TDPMessageType.Error);
                }
                else
                {
                    tdpMessage = new TDPMessage("ParkAndRideJourneyLocations.ParkAndRideNoneFound.Text", TDPMessageType.Error);
                }
                ((TDPWeb)this.Page.Master).DisplayMessage(tdpMessage);
            }
            else if (venue != null && carParkList != null && carParkList.Count > 0)
            {
                bool validateTime = Properties.Current["ParkAndRideJourneyLocations.JourneyTime.Validate.Switch"].Parse(true);

                if (validateTime)
                {
                    bool isOpen = false;

                    #region Check open for outward time

                    // Check if there is at least one car park which is open for the outward arriving by time
                    foreach (TDPVenueCarPark carPark in carParkList)
                    {
                        if (carPark.IsOpenForTime(dateTimeOutward))
                        {
                            isOpen = true;
                            break;
                        }
                    }

                    if (!isOpen)
                    {
                        if (isBlueBadge)
                        {
                            tdpMessage = new TDPMessage("ParkAndRideJourneyLocations.BlueBadgeClosedArrivingAt.Text", TDPMessageType.Error);
                        }
                        else
                        {
                            tdpMessage = new TDPMessage("ParkAndRideJourneyLocations.ParkAndRideClosedArrivingAt.Text", TDPMessageType.Error);
                        }
                        ((TDPWeb)this.Page.Master).DisplayMessage(tdpMessage);
                    }

                    #endregion

                    #region Check open for return time

                    isOpen = false;

                    // Check if there is at least one car park which is open for the return leaving at time
                    foreach (TDPVenueCarPark carPark in carParkList)
                    {
                        if (carPark.IsOpenForTime(dateTimeReturn))
                        {
                            isOpen = true;
                            break;
                        }
                    }

                    if (!isOpen)
                    {
                        if (isBlueBadge)
                        {
                            tdpMessage = new TDPMessage("ParkAndRideJourneyLocations.BlueBadgeClosedLeavingFrom.Text", TDPMessageType.Error);
                        }
                        else
                        {
                            tdpMessage = new TDPMessage("ParkAndRideJourneyLocations.ParkAndRideClosedLeavingFrom.Text", TDPMessageType.Error);
                        }
                        ((TDPWeb)this.Page.Master).DisplayMessage(tdpMessage);
                    }

                    #endregion
                }
            }
        }

        /// <summary>
        /// Sets up resource content for the controls
        /// </summary>
        private void SetupResources()
        {
            TDPPage page = (TDPPage)Page;

            Language language = CurrentLanguage.Value;

            preferredParksHeading.Text = Global.TDPResourceManager.GetString(language, "ParkAndRideJourneyLocations.PreferredParksHeading.Text");
            parkAndRideTimeSlotHeading.Text = Global.TDPResourceManager.GetString(language, "ParkAndRideJourneyLocations.ParkAndRideTimeSlotHeading.Text");
            parkAndRideTimeSlotNote.Text = Global.TDPResourceManager.GetString(language, "ParkAndRideJourneyLocations.ParkAndRideTimeSlotNote.Text");

            if (dateTimeOutward == DateTime.MinValue)
            {
                // Don't display the timeslot as it only applies for the outward journey datetime
                parkAndRideTimeSlotHeading.Visible = false;
                parkAndRideTimeSlotNote.Visible = false;
            }

            parkAndRideNote.Text = isBlueBadge ?
                Global.TDPResourceManager.GetString(language, "ParkAndRideJourneyLocations.BlueBadgeNote.Text") :
                Global.TDPResourceManager.GetString(language, "ParkAndRideJourneyLocations.ParkAndRideNote.Text");

            parkAndRideBookingNote.Text = isBlueBadge ?
                Global.TDPResourceManager.GetString(language, "ParkAndRideJourneyLocations.BlueBadgeBookingNote.Text") :
                Global.TDPResourceManager.GetString(language, "ParkAndRideJourneyLocations.ParkAndRideBookingNote.Text");

            if (Properties.Current["ParkAndRideJourneyLocations.ParkAndRide.Booking.Switch"].Parse(false))
            {
                parkAndRideBookingURL.Text = isBlueBadge ?
                    Global.TDPResourceManager.GetString(language, "ParkAndRideJourneyLocations.BlueBadgeBookingURL.Text") :
                    Global.TDPResourceManager.GetString(language, "ParkAndRideJourneyLocations.ParkAndRideBookingURL.Text");

                parkAndRideBookingURL.NavigateUrl = isBlueBadge ?
                    Global.TDPResourceManager.GetString(language, "ParkAndRideJourneyLocations.BlueBadgeBookingURL.URL") :
                    Global.TDPResourceManager.GetString(language, "ParkAndRideJourneyLocations.ParkAndRideBookingURL.URL");
                parkAndRideBookingURL.Target = "_blank";

                openInNewWindow.ImageUrl = page.ImagePath + Global.TDPResourceManager.GetString(language, "OpenInNewWindow.Blue.URL");
                openInNewWindow.AlternateText = Global.TDPResourceManager.GetString(language, "OpenInNewWindow.AlternateText");
                openInNewWindow.ToolTip = Global.TDPResourceManager.GetString(language, "OpenInNewWindow.Text");
            }
            else
            {
                // Don't display booking link
                parkAndRideBooking.Visible = false;
            }
        }
        
        /// <summary>
        /// Sets up venue map url using resource content
        /// </summary>
        private void SetupVenueMap()
        {
            if (venue != null)
            {
                TDPPage page = (TDPPage)Page;

                Language language = CurrentLanguage.Value;

                string venueMapUrl_ResourceId = string.Format("JourneyLocations.{0}.ParkAndRide.Url", venue.Naptan[0]);
                string venueMapAlternateText_ResourceId = string.Format("JourneyLocations.{0}.ParkAndRide.AlternateText", venue.Naptan[0]);

                string venueMapURL = Global.TDPResourceManager.GetString(language, venueMapUrl_ResourceId);
                string venueMapAltText = Global.TDPResourceManager.GetString(language, venueMapAlternateText_ResourceId);

                // If venue has no map, then use parents map
                if (string.IsNullOrEmpty(venueMapURL))
                {
                    venueMapUrl_ResourceId = string.Format("JourneyLocations.{0}.ParkAndRide.Url", venue.Parent);
                    venueMapURL = Global.TDPResourceManager.GetString(language, venueMapUrl_ResourceId);
                }
                if (string.IsNullOrEmpty(venueMapAltText))
                {
                    venueMapAlternateText_ResourceId = string.Format("JourneyLocations.{0}.ParkAndRide.AlternateText", venue.Parent);
                    venueMapAltText = Global.TDPResourceManager.GetString(language, venueMapAlternateText_ResourceId);
                }

                if (!string.IsNullOrEmpty(venueMapURL))
                {
                    venueMap.ImageUrl = page.ImagePath + venueMapURL;
                    venueMap.AlternateText = venueMapAltText;
                    venueMap.ToolTip = venueMap.AlternateText;
                    
                    venueMapDiv.Visible = true;
                }
                else
                {
                    venueMapDiv.Visible = false;
                }
            }
        }

        /// <summary>
        /// Sets up the car park for the provided venue
        /// </summary>
        private void SetupCarParks()
        {
            // Venue should have been set by now
            if (venue != null)
            {
                // Check if the users journey travel dates should be used in determining validaty of car parks
                bool validateDate = Properties.Current["ParkAndRideJourneyLocations.JouneyDate.Validate.Switch"].Parse(true);
                if (!validateDate)
                {
                    dateTimeOutward = DateTime.MinValue;
                    dateTimeReturn = DateTime.MinValue;
                }

                // Get car parks
                LocationService locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);

                // Get car parks for the venue and its parent
                List<string> naptans = new List<string>(venue.Naptan);
                if (!string.IsNullOrEmpty(venue.Parent))
                {
                    naptans.Add(venue.Parent);
                }

                if (isBlueBadge)
                {
                    // Blue Badge car parks
                    carParkList = locationService.GetTDPVenueBlueBadgeCarParks(naptans, dateTimeOutward, dateTimeReturn);
                }
                else
                {
                    // Park and Ride car parks
                    carParkList = locationService.GetTDPVenueCarParks(naptans, dateTimeOutward, dateTimeReturn);
                }

                if (Properties.Current["ParkAndRideJourneyLocations.VenueMap.Clickable.Switch"].Parse(true))
                {
                    SetupClickableMapBullets(carParkList);
                }
            }
        }

        /// <summary>
        /// Sets up car park map bullet links
        /// The links rendered as absolute position style set in content database
        /// </summary>
        /// <param name="cycleParkList"></param>
        private void SetupClickableMapBullets(List<TDPVenueCarPark> carParkList)
        {
            if (carParkList != null)
            {
                Language language = CurrentLanguage.Value;

                mapBulletTarget.Value = preferredParksOptions.ClientID;

                foreach (TDPVenueCarPark carPark in carParkList)
                {
                    using (HtmlAnchor bullet = new HtmlAnchor())
                    {
                        string bulletStyle_ResourceId = string.Format("JourneyLocations.ParkAndRide.{0}.Style", carPark.ID);

                        string bulletStyle = Global.TDPResourceManager.GetString(language, bulletStyle_ResourceId);
                        if (!string.IsNullOrEmpty(bulletStyle))
                        {
                            bullet.Title = carPark.Name;
                            bullet.Attributes["style"] = bullet.Attributes["style"] + bulletStyle + "display:none";
                            bullet.Attributes["class"] = "bullet cycleBullet ";
                            bullet.HRef = "#";
                            bullet.InnerHtml = "&nbsp;";
                            bullet.Name = carPark.ID;

                            mapBullets.Controls.Add(bullet);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Binds venue car parks to preferredParkOptions dropdown
        /// </summary>
        private void SetupVenueDropDown()
        {
            List<TDPVenueCarPark> carParks = carParkList;

            if (carParks != null)
            {
                preferredParksOptions.DataSource = carParks;
                preferredParksOptions.DataTextField = "Name";
                preferredParksOptions.DataValueField = "ID";

                preferredParksOptions.DataBind();
            }
        }

        /// <summary>
        /// Sets the time slot dropdown list with times for the selected car park
        /// </summary>
        private void SetupVenueCarParkTimeSlotDropDown()
        {
            // Only need the timeslots if an outward datetime has been provided,
            // as it only applies for an outward journey datetime
            if (dateTimeOutward != DateTime.MinValue)
            {
                List<ListItem> timeSlotList = new List<ListItem>();

                #region Get start and end time slot ranges

                TimeSpan start = new TimeSpan(8, 0, 0);
                TimeSpan end = new TimeSpan(22, 0, 0);
                TimeSpan interval = new TimeSpan(0, 60, 0);

                try
                {
                    // Get start and end times for the time slot range
                    string startTime = isBlueBadge ?
                        Properties.Current["ParkAndRideJourneyLocations.BlueBadge.DropDownTimeSlot.StartTime"].Parse("0800") :
                        Properties.Current["ParkAndRideJourneyLocations.ParkAndRide.DropDownTimeSlot.StartTime"].Parse("0800");
                    string endTime = isBlueBadge ?
                        Properties.Current["ParkAndRideJourneyLocations.BlueBadge.DropDownTimeSlot.EndTime"].Parse("2200") :
                        Properties.Current["ParkAndRideJourneyLocations.ParkAndRide.DropDownTimeSlot.EndTime"].Parse("2200");
                    int intervalMins = isBlueBadge ?
                        Properties.Current["ParkAndRideJourneyLocations.BlueBadge.DropDownTimeSlot.IntervalMinutes"].Parse(60) :
                        Properties.Current["ParkAndRideJourneyLocations.ParkAndRide.DropDownTimeSlot.IntervalMinutes"].Parse(60);

                    int startHour = startTime.Substring(0, 2).Parse(8);
                    int startMin = startTime.Substring(2, 2).Parse(0);
                    int endHour = endTime.Substring(0, 2).Parse(22);
                    int endMin = endTime.Substring(2, 2).Parse(0);

                    // Set the start and end times, and interval
                    start = new TimeSpan(startHour, startMin, 0);
                    end = new TimeSpan(endHour, endMin, 0);
                    interval = new TimeSpan(0, intervalMins, 0);
                }
                catch
                {
                    // Ignore any exceptions, defaults already set
                }

                #endregion

                #region Build time slot list

                List<ListItem> times = new List<ListItem>();
                ListItem li = null;
                TimeSpan to = new TimeSpan(start.Hours, start.Minutes, 0);
                bool completed = false;
                int selectedIndex = 0;

                while (!completed)
                {
                    // Update the time for the end of the slot
                    to = to.Add(interval);

                    // Build time slot item, value is the start of the slot 
                    // (which will become the "Arrive by" time for the outward journey)
                    li = new ListItem(string.Format("{0:00}:{1:00} - {2:00}:{03:00}", start.Hours, start.Minutes, to.Hours, to.Minutes),
                                      string.Format("{0:00}:{1:00}", start.Hours, start.Minutes));

                    times.Add(li);

                    // Check if this item should be selected by default, using the provided outward datetime
                    if ((dateTimeOutward.TimeOfDay >= start) && (dateTimeOutward.TimeOfDay < to))
                    {
                        selectedIndex = times.Count - 1;
                    }

                    // Increment for next loop iteration
                    start = start.Add(interval);

                    if ((start >= end) || (start.Hours >= 24))
                    {
                        completed = true;
                    }
                }

                // Create time slot list of ListItems
                timeSlotList = times.ToList();

                #endregion

                // Add the time slot ListItems to the dropdown
                drpTimeSlotOptions.Items.AddRange(timeSlotList.ToArray());

                // Set default selected
                if (drpTimeSlotOptions.Items.Count > 0)
                {
                    drpTimeSlotOptions.Items[selectedIndex].Selected = true;
                }
            }
            else
            {
                drpTimeSlotOptions.Visible = false;
            }
        }

        /// <summary>
        /// Builds a DateTime object out of the time slot drop down and the Outward/Return date time
        /// </summary>
        /// <param name="isReturn"></param>
        /// <returns></returns>
        private DateTime GetTimeSlotDateTime(bool isReturn)
        {
            DateTime date = isReturn ? dateTimeReturn : dateTimeOutward;

            if ((drpTimeSlotOptions == null) || (drpTimeSlotOptions.Items.Count == 0))
            {
                SetupVenueCarParkTimeSlotDropDown();
            }

            DropDownList timeList = drpTimeSlotOptions;

            if (timeList != null && timeList.Items.Count > 0)
            {
                string[] timeparts = timeList.SelectedValue.Split(new char[] { ':' });
                int minute = timeparts[1].Parse(0);
                int hour = timeparts[0].Parse(0);

                return new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            }
            else
            {
                return date;
            }
        }

        /// <summary>
        /// Returns the total transit time between the Venue and Car park
        /// </summary>
        /// <returns></returns>
        private TimeSpan GetTransitTime(bool isReturn)
        {
            TimeSpan time = new TimeSpan();

            TDPVenueCarPark carPark = SelectedCarPark;

            if (carPark != null)
            {
                #region Car park to/from the Transit shuttle

                time = time.Add(new TimeSpan(0, carPark.InterchangeDuration, 0));

                #endregion

                LocationService locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);

                TDPVenueGate gate = null;
                TDPVenueGateCheckConstraint gateCheckConstraint = null;
                TDPVenueGateNavigationPath gateNavigationPath = null;

                #region Transit shuttle to/from venue

                if (carPark.TransitShuttles != null)
                {
                    foreach (TransitShuttle ts in carPark.TransitShuttles)
                    {
                        // Transit time from the car park to the venue
                        if (!isReturn && ts.ToVenue)
                        {
                            bool dateValid = false;
                            bool timeValid = false;

                            if(ts.Availability != null)
                            {
                                foreach (TDPParkAvailability availability in ts.Availability)
                                {
                                    List<DayOfWeek> daysValid = availability.GetDaysOfWeek();
                                
                                    dateValid = IsDateValid(daysValid, availability.FromDate, availability.ToDate, OutwardDateTime);
                                    timeValid = IsTimeValid(availability.DailyOpeningTime, availability.DailyClosingTime, OutwardDateTime);
                                
                                    if(dateValid && timeValid)
                                    {
                                        time = time.Add(new TimeSpan(0, ts.TransitDuration, 0));

                                        // Get the venue gate and path details for cycle park
                                        gate = locationService.GetTDPVenueGate(ts.VenueGateToUse);
                                        gateCheckConstraint = locationService.GetTDPVenueGateCheckConstraints(gate, !isReturn);
                                        gateNavigationPath = locationService.GetTDPVenueGateNavigationPaths(venue, gate, !isReturn);

                                        break;
                                    }
                                }
                            }
                            break;
                           
                        }
                        // Transit time from the venue to the car park
                        else if (isReturn && !ts.ToVenue)
                        {
                            bool dateValid = false;
                            bool timeValid = false;

                            if (ts.Availability != null)
                            {
                                foreach (TDPParkAvailability availability in ts.Availability)
                                {
                                    List<DayOfWeek> daysValid = availability.GetDaysOfWeek();

                                    dateValid = IsDateValid(daysValid, availability.FromDate, availability.ToDate, ReturnDateTime);
                                    timeValid = IsTimeValid(availability.DailyOpeningTime, availability.DailyClosingTime, ReturnDateTime);

                                    if (dateValid && timeValid)
                                    {
                                        time = time.Add(new TimeSpan(0, ts.TransitDuration, 0));

                                        // Get the venue gate and path details for cycle park
                                        gate = locationService.GetTDPVenueGate(ts.VenueGateToUse);
                                        gateCheckConstraint = locationService.GetTDPVenueGateCheckConstraints(gate, !isReturn);
                                        gateNavigationPath = locationService.GetTDPVenueGateNavigationPaths(venue, gate, !isReturn);
                                    }
                                }
                            }

                            break;
                        }
                    }
                }

                #endregion

                #region Venue gate and check constraints

                if (gateCheckConstraint != null)
                {
                    time = time.Add(gateCheckConstraint.AverageDelay);
                }

                if (gateNavigationPath != null)
                {
                    time = time.Add(gateNavigationPath.TransferDuration);
                }

                #endregion
            }

            return time;
        }

        /// <summary>
        /// Checks if the date supplied is valid for From and To date range and DaysOfWeek list
        /// </summary>
        private bool IsDateValid(List<DayOfWeek> daysValid, DateTime fromDate, DateTime toDate, DateTime journeyDate)
        {
            bool valid = false;

            // Outward and return journey check
            if (fromDate.Date <= journeyDate.Date &&
                toDate.Date >= journeyDate.Date)
            {
                if (daysValid.Contains(journeyDate.DayOfWeek))
                {
                    valid = true;
                }
            }

            return valid;
        }

        /// <summary>
        /// Checks if the Time supplied is valid for Start and End Times
        /// </summary>
        private bool IsTimeValid(TimeSpan starttime, TimeSpan endtime, DateTime journeyTime)
        {
            bool valid = false;

            if (starttime.Hours <= journeyTime.Hour && endtime.Hours >= journeyTime.Hour)
            {
                valid = true;
                
                if (starttime.Hours == journeyTime.Hour)
                {
                    if (starttime.Minutes > journeyTime.Minute)
                    {
                        valid = false;
                    }
                }

                if (endtime.Hours == journeyTime.Hour)
                {
                    if (endtime.Minutes < journeyTime.Minute)
                    {
                        valid = false;
                    }
                }
            }

            return valid;
        }
        #endregion
    }
}
