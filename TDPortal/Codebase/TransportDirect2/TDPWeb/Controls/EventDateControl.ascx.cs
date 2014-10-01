// *********************************************** 
// NAME             : EventDateControl.ascx.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 31 Mar 2011
// DESCRIPTION  	: Represents Event date selection user control on journey input page
// ************************************************


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using TDP.Common.Extenders;
using TDP.Common.PropertyManager;
using TDP.Common.Web;

namespace TDP.UserPortal.TDPWeb.Controls
{
    /// <summary>
    /// Represents Event date selection user control on journey input page
    /// </summary>
    public partial class EventDateControl : System.Web.UI.UserControl
    {
        #region Private Fields

        private const string DATE_FORMAT = "dd/MM/yyyy";
        private const string TIME_FORMAT = "HH:mm";

        private bool forceUpdate = false;
        private DateTime selectedOutwardDateTime = DateTime.MinValue;
        private DateTime selectedReturnDateTime = DateTime.MinValue;
        private bool validInputDateChange = true;
        private bool isOutwardRequired = true;

        #endregion

        #region Public Properties

        /// <summary>
        /// Read/Write. Outward datetime
        /// </summary>
        public DateTime OutwardDateTime
        {
            get { return selectedOutwardDateTime = GetEventDateTime(false); }
            set { selectedOutwardDateTime = value; }
        }

        /// <summary>
        /// Read/Write. Return datetime
        /// </summary>
        /// <remarks>
        /// Only time of the return date time will be considered when setting the datetime
        /// as the event date should be the single day the outward journey date will be considered always 
        /// </remarks>
        public DateTime ReturnDateTime
        {
            get { return selectedReturnDateTime = GetEventDateTime(true); }
            set { selectedReturnDateTime = value; }
        }

        /// <summary>
        /// Read/Write property indicates if the datetime should be updated
        /// (e.g. if in the past)
        /// </summary>
        public bool ForceUpdate
        {
            get { return forceUpdate; }
            set { forceUpdate = value; }
        }
        
        /// <summary>
        /// Read/Write property determines wether the outward journey required or not
        /// </summary>
        public bool IsOutwardRequired
        {
            get { return isOutwardRequired; }
            set { isOutwardRequired = value; }
        }

        /// <summary>
        /// Read/Write property determines wether the return journey required or not
        /// </summary>
        public bool IsReturnRequired
        {
            get { return (isReturnJourney.Checked || !isOutwardRequired); }
            set { isReturnJourney.Checked = value; }
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
            PopulateTimeDropDowns();
            
            InitCalendar();

            InitOutwardRequired();
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupResourceStrings();

            SetJourneyDateTime();
        }

        #endregion

        #region Control Event Handlers

        /// <summary>
        /// Calendar date change event handler
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected void Calendar_DateChange(object source, EventArgs args)
        {
            selectedOutwardDateTime = GetEventDateTime(false);
            selectedReturnDateTime = GetEventDateTime(true);

            // Ensure return date is not before outward
            if (isOutwardRequired && (selectedOutwardDateTime > selectedReturnDateTime))
            {
                selectedReturnDateTime = selectedOutwardDateTime;
            }
        }

        /// <summary>
        /// Outward date input box text change event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OutwardDate_Changed(object sender, EventArgs e)
        {
            DateTime outDate = DateTime.MinValue;

            if (!DateTime.TryParse(outwardDate.Text.Trim(), out outDate))
            {
                validInputDateChange = false;
            }
            else
            {
                selectedOutwardDateTime = GetEventDateTime(false);
            }
        }

        /// <summary>
        /// Return date input box text change event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ReturnDate_Changed(object sender, EventArgs e)
        {
            DateTime retDate = DateTime.MinValue;

            if (!DateTime.TryParse(returnDate.Text.Trim(), out retDate))
            {
                if (isReturnJourney.Checked || !isOutwardRequired)
                {
                    validInputDateChange = false;
                }
            }
            else
            {
                selectedReturnDateTime = GetEventDateTime(true);
            }
        }
        #endregion

        #region Private Methods
        
        /// <summary>
        /// Sets Control Culture aware resource strings
        /// </summary>
        private void SetupResourceStrings()
        {
            TDPPage page = (TDPPage)Page;

            outwardDateLabel.Text = page.GetResource("EventDateControl.outwardDateLabel.Text");
            returnDateLabel.Text = page.GetResource("EventDateControl.returnDateLabel.Text");
            arriveTimeLabel.Text = page.GetResource("EventCalendar.arriveTimeLabel.Text");
            leaveTimeLabel.Text = page.GetResource("EventCalendar.leaveTimeLabel.Text");
            isReturnJourney.Text = page.GetResource("EventCalendar.isReturnJourney.Text");

            outward_Information.ImageUrl = page.ImagePath + page.GetResource("EventDateControl.OutwardInformation.ImageUrl");
            outward_Information.AlternateText = page.GetResource("EventDateControl.OutwardInformation.AlternateText");
            outward_Information.ToolTip = page.GetResource("EventDateControl.OutwardInformation.ToolTip");

            return_Information.ImageUrl = page.ImagePath + page.GetResource("EventDateControl.ReturnInformation.ImageUrl");
            return_Information.AlternateText = page.GetResource("EventDateControl.ReturnInformation.AlternateText");
            return_Information.ToolTip = page.GetResource("EventDateControl.ReturnInformation.ToolTip");
            
            tooltip_information_outward.Title = page.GetResource("EventDateControl.OutwardInformation.ToolTip");
            tooltip_information_return.Title = page.GetResource("EventDateControl.ReturnInformation.ToolTip");
        }

        /// <summary>
        /// Sets the outward required flag in control
        /// </summary>
        private void InitOutwardRequired()
        {
            // Read the hidden value, this will be empty on first page load, 
            // and should only be set on a postbacks if set by parent
            if (!string.IsNullOrEmpty(returnOnly.Value))
            {
                isOutwardRequired = !returnOnly.Value.Parse(true);
            }
        }

        /// <summary>
        /// Builds a datetime object out of the calendar control and time dropdowns
        /// </summary>
        /// <param name="isReturn"></param>
        /// <returns></returns>
        private DateTime GetEventDateTime(bool isReturn)
        {
            // If there was a landing page with auto plan true, then need to ensure this control is initialised 
            // (otherwise values can be empty and cause errors here)
            if (forceUpdate)
            {
                PopulateTimeDropDowns();
                SetJourneyDateTime();
            }

            #region Date

            DateTime date = DateTime.MinValue;
            string dateString = isReturn ?
                returnDate.Text.Trim() :
                outwardDate.Text.Trim();

            // Check date entered
            DateTime.TryParseExact(dateString,
                DATE_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

            #endregion

            #region Time

            // Time 
            int minute = 0;
            int hour = 0;
            string[] timeparts = new string[2];

            string timeString = isReturn ?
                leaveTime.SelectedValue : arriveTime.SelectedValue;

            if (!string.IsNullOrEmpty(timeString))
            {
                if (timeString.Contains(":"))
                {
                    timeparts = timeString.Split(new char[] { ':' });

                    if (timeparts.Length != 2)
                        timeparts = new string[2];
                }
                else if (timeString.Length == 4)
                {
                    timeparts[0] = timeString.Substring(0, 2);
                    timeparts[1] = timeString.Substring(2, 2);
                }
            }

            #endregion

            if (int.TryParse(timeparts[0], out hour) && int.TryParse(timeparts[1], out minute))
            {
                date = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
            }

            return date;
        }

        /// <summary>
        /// Sets the date control and time dropdown to represent the journey date
        /// </summary>
        private void SetJourneyDateTime()
        {
            if (ValidJourneyDateTime(selectedOutwardDateTime, true, forceUpdate))
            {
                // Set date text
                outwardDate.Text = selectedOutwardDateTime.ToString(DATE_FORMAT);

                // Set time
                int minuteInterval = Properties.Current["EventDateControl.DropDownTime.IntervalMinutes"].Parse(15);

                DateTime selectedTime = DateTimeHelper.GetRoundedTime(selectedOutwardDateTime, minuteInterval);

                arriveTime.SelectedItem.Selected = false;
                arriveTime.SelectedValue = selectedTime.ToString(TIME_FORMAT);
            }

            if (ValidJourneyDateTime(selectedReturnDateTime, false, forceUpdate))
            {
                // Set date text
                returnDate.Text = selectedReturnDateTime.ToString(DATE_FORMAT);

                // Set time
                int minuteInterval = Properties.Current["EventDateControl.DropDownTime.IntervalMinutes"].Parse(15);

                DateTime selectedTime = DateTimeHelper.GetRoundedTime(selectedReturnDateTime, minuteInterval);

                leaveTime.SelectedItem.Selected = false;
                leaveTime.SelectedValue = selectedTime.ToString(TIME_FORMAT);
            }
        }

        /// <summary>
        /// Validates the journey date times
        /// </summary>
        /// <param name="selectedOutwardDateTime"></param>
        /// <param name="forceUpdate">If true, the selected datetime is updated to now if it is inthe past</param>
        /// <returns></returns>
        private bool ValidJourneyDateTime(DateTime selectedDateTime, bool isOutward, bool forceUpdate)
        {
            bool validDate = true;

            if (selectedDateTime == DateTime.MinValue)
                validDate = false;
            // Date part could be invalid (01/01/0001)
            else if (selectedDateTime.Date == DateTime.MinValue.Date)
                validDate = false;

            if (!validDate && forceUpdate)
            {
                // Date time is not valid.
                // If in the past, update it to now if required
                bool noTimeTodayInPast = Properties.Current["JourneyPlanner.Validate.Switch.TimeTodayInThePast"].Parse(false);

                // Try and retain date/time parts if that is valid
                DateTime validDateTime = selectedDateTime;
                DateTime now = DateTime.Now;

                if (validDateTime.Date < now.Date)
                {
                    validDateTime = new DateTime(now.Year, now.Month, now.Day, validDateTime.Hour, validDateTime.Minute, 0);
                }

                // Update time if necessary
                if ((selectedDateTime == DateTime.MinValue)
                    || (noTimeTodayInPast && validDateTime.Date == now.Date && validDateTime.TimeOfDay < now.TimeOfDay))
                {
                    validDateTime = new DateTime(validDateTime.Year, validDateTime.Month, validDateTime.Day, now.Hour, now.Minute, 0);

                    // Round up to the nearest minute interval
                    validDateTime = DateTimeHelper.GetRoundedTime(validDateTime);
                }

                if (isOutward)
                    selectedOutwardDateTime = validDateTime;
                else
                    selectedReturnDateTime = validDateTime;

                validDate = true;
            }

            return validDate;
        }

        #region Initialise/Populate Dates and Time controls

        /// <summary>
        /// Sets up caledar control default dates
        /// </summary>
        private void InitCalendar()
        {
            #region Set calendar dates

            // Defaults
            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = DateTime.Now.Date.AddMonths(1);

            // Set the start to end dates to display for the calendar. This will be the period for which
            // journeys can be planned
            DateTimeHelper.SetDefaultDateRange(ref startDate, ref endDate, true);

            // Hidden fields
            calendarStartDate.Value = startDate.ToString(DATE_FORMAT);
            calendarEndDate.Value = endDate.ToString(DATE_FORMAT);
            
            #endregion

            if (!IsPostBack)
            {
                #region Set the default selected datetimes

                // Set the initial selected outward date value
                if (selectedOutwardDateTime == DateTime.MinValue)
                {
                    DateTime outwardStartDate = DateTime.MinValue;

                    // Set the default time to use for the start date
                    DateTimeHelper.SetDefaultTimeForDate(startDate, ref outwardStartDate, false);
                    
                    selectedOutwardDateTime = (DateTime.Now >= outwardStartDate) ? DateTime.Now : outwardStartDate;
                }
                // Else the selectedOutwardDateTime has already been populated
                
                if (selectedReturnDateTime == DateTime.MinValue)
                {
                    DateTime returnStartDate = DateTime.MinValue;

                    // Set the default time to use for the start date
                    DateTimeHelper.SetDefaultTimeForDate(startDate, ref returnStartDate, true);

                    selectedReturnDateTime = (DateTime.Now >= returnStartDate) ? DateTime.Now : returnStartDate;
                }
                // Else the selectedReturnDateTime has already been populated

                #endregion

                // Check if need to update the return date time based on the outward date time.
                // If landing page, then the return date time value may have been set already - this needs to be 
                // validated and updated here

                #region Validate and update return datetime

                // Get the default return datetime, e.g. 24/05/2010 17:00
                DateTime returnDateTime = GetEventDateTime(true);
                if (returnDateTime <= selectedOutwardDateTime)
                {
                    // The return time might have been set to be later than the 
                    // default return time, so retain that part

                    // Update the date part, retaining the time part and check again
                    DateTime updatedReturnDateTime = new DateTime(
                        selectedOutwardDateTime.Year, selectedOutwardDateTime.Month, selectedOutwardDateTime.Day,
                        returnDateTime.Hour, returnDateTime.Minute, 0);

                    if (updatedReturnDateTime < selectedOutwardDateTime)
                    {
                        // Update both the date part and time part and check again, because the 
                        // return datetime may not have been set in the landing page
                        updatedReturnDateTime = new DateTime(
                            selectedOutwardDateTime.Year, selectedOutwardDateTime.Month, selectedOutwardDateTime.Day,
                            returnDateTime.Hour, returnDateTime.Minute, 0);

                        if (updatedReturnDateTime < selectedOutwardDateTime)
                        {
                            selectedReturnDateTime = selectedOutwardDateTime.AddHours(Properties.Current["EventDateControl.DropDownTime.OutwardReturnIntervalHours"].Parse(1));
                        }
                        else
                        {
                            selectedReturnDateTime = updatedReturnDateTime;
                        }
                    }
                    else
                    {
                        selectedReturnDateTime = updatedReturnDateTime;
                    }
                }

                #endregion
            }
        }

        /// <summary>
        /// Populate the time dropdowns
        /// </summary>
        private void PopulateTimeDropDowns()
        {
            if ((arriveTime.Items.Count == 0) || (leaveTime.Items.Count == 9))
            {
                List<ListItem> arriveTimeList = new List<ListItem>();
                List<ListItem> leaveTimeList = new List<ListItem>();
                DateTime startTime = DateTime.Now.Date;

                int interval = Properties.Current["EventDateControl.DropDownTime.IntervalMinutes"].Parse(15);

                var times = from hour in Enumerable.Range(0, 24) // each hour in 0 -23
                            from minute in Enumerable.Range(0, 59) // each minute in 0 - 59
                            where minute % interval == 0 // get the minute with the interval required
                            // format the time and return the ListItem populated with it
                            select new ListItem(string.Format("{0:00}:{1:00}", hour, minute),
                                string.Format("{0:00}:{1:00}", hour, minute));

                arriveTimeList = times.ToList();
                leaveTimeList = times.ToList();

                // Add the ListItems to the dropdowns
                arriveTime.Items.AddRange(arriveTimeList.ToArray());
                leaveTime.Items.AddRange(leaveTimeList.ToArray());

                arriveTime.SelectedValue = string.Format("{0:00}:{1:00}", selectedOutwardDateTime.Hour, selectedOutwardDateTime.Minute);
                leaveTime.SelectedValue = string.Format("{0:00}:{1:00}", selectedReturnDateTime.Hour, selectedReturnDateTime.Minute);
            }
        }

        #endregion
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Validates the outward and return times
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            return validInputDateChange;
        }

        /// <summary>
        /// Switches the date control into showing/hiding the outward date entry
        /// </summary>
        /// <param name="showReturnDateOnly"></param>
        /// <returns></returns>
        public void ShowReturnDateOnly(bool showReturnDateOnly)
        {
            isOutwardRequired = !showReturnDateOnly;
            
            // Show/hide the outward date controls and the return date checkbox
            outwardDateDiv.Visible = !showReturnDateOnly;
            returnDateCheckBoxDiv.Visible = !showReturnDateOnly;

            // Update hidden field for javascript
            returnOnly.Value = showReturnDateOnly.ToString();
        }

        #endregion
    }
}
