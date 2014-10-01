// *********************************************** 
// NAME             : EventDateControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 06 Feb 2012
// DESCRIPTION  	: Event Date User control
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using TDP.Common.Extenders;
using TDP.Common.PropertyManager;
using TDP.Common.Web;
using System.Web.UI.HtmlControls;

namespace TDP.UserPortal.TDPMobile.Controls
{
    /// <summary>
    /// Event Date User control
    /// </summary>
    public partial class EventDateControl : System.Web.UI.UserControl
    {
        #region Private Fields

        private const string DATE_FORMAT = "dd/MM/yyyy";
        private const string TIME_FORMAT = "HH:mm";

        private DateTime outwardDateTime = DateTime.MinValue;
        private bool forceUpdate = false;
        private bool validInputDateChange = true;
        private bool arriveBy = false;
        private bool isNow = false;
        private bool isToVenue = false;
        private bool resetTime = false;

        private string TXT_Today = string.Empty;

        #endregion

        #region Public Properties

        /// <summary>
        /// Read/Write. Outward datetime
        /// </summary>
        public DateTime OutwardDateTime
        {
            get { return outwardDateTime = GetEventDateTime(); }
            set { outwardDateTime = value; }
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
        /// Read/Write. Flag used to control if the date time is arrive by or leave at.
        /// Default is arrive by
        /// </summary>
        public bool ArriveBy
        {
            get
            {
                if (isNow)
                    return false;
                else
                    return arriveBy;
            }
            set { arriveBy = value; }
        }

        /// <summary>
        /// Read/Write. Flag to indicate if the event date is being shown for a ToVenue location input mode
        /// </summary>
        public bool IsToVenue
        {
            get { return isToVenue; }
            set { isToVenue = value; }
        }

        /// <summary>
        /// Read/Write. Indicates if the time part should be reset
        /// </summary>
        public bool ResetTime
        {
            get { return resetTime; }
            set { resetTime = true; }
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
            InitialiseResources();

            PopulateDateDropDowns();

            PopulateTimeDropDowns();

            InitTime();

            InitCalendar();

            // attempt to get the date time from the control, will be Date.MinValue if the control doesn't contain a date.
            outwardDateTime = GetEventDateTime();

            if (IsPostBack)
            {
                // Read flags.
                // Not reading isToVenueFlag as that is only required by the javascript as when
                // changing the date/time the now flag must be reset and the arrive by flag updated
                // depending on the location input "to venue" mode
                arriveBy = isArriveByFlag.Value.Parse(true);
                isNow = isNowFlag.Value.Parse(false);
            }
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupResourceStrings();

            SetupNowLink();

            SetupDateTimeStyle();

            SetJourneyDateTime();

            // Persist flags for next postback
            isArriveByFlag.Value = ArriveBy.ToString().ToLower();
            isNowFlag.Value = isNow.ToString().ToLower();
            isToVenueFlag.Value = isToVenue.ToString().ToLower();
        }

        #endregion

        #region Control Event Handlers
        
        /// <summary>
        /// Outward date input box text change event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OutwardDate_Changed(object sender, EventArgs e)
        {
            DateTime outDate = DateTime.MinValue;

            if (!DateTime.TryParse(outwardDate.Text.Trim(), out outDate)
                && (outwardDate.Text.Trim() != TXT_Today))
            {
                validInputDateChange = false;
            }
            else
            {
                outwardDateTime = GetEventDateTime();
            }
        }

        /// <summary>
        /// Now link click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void nowLink_Click(object sender, EventArgs e)
        {
            outwardDateTime = DateTimeHelper.GetRoundedTime(DateTime.Now);

            // Update flags, when now is selected, the date time is a leave at
            arriveBy = false;
            isNow = true;
        }
                
        #endregion

        #region Private Methods

        /// <summary>
        /// Initialises resource strings required for use throughout control
        /// </summary>
        private void InitialiseResources()
        {
            TDPPageMobile page = (TDPPageMobile)Page;

            TXT_Today = page.GetResourceMobile("JourneyInput.Date.Today.Text");
        }

        /// <summary>
        /// Sets resource strings
        /// </summary>
        private void SetupResourceStrings()
        {
            TDPPageMobile page = (TDPPageMobile)Page;

            outwardDateLabel.Text = page.GetResourceMobile("JourneyInput.Date.Outward.Text");
            outwardTimeLabel.Text = page.GetResourceMobile("JourneyInput.Time.Outward.Text");
            nowLink.Text = page.GetResourceMobile("JourneyInput.Now.Link.Text");
            nowLink.ToolTip = page.GetResourceMobile("JourneyInput.Now.Link.ToolTip");
            nowLinkNonJS.Text = nowLink.Text;
            nowLinkNonJS.ToolTip = nowLink.ToolTip;

            string arriveByText = page.GetResourceMobile("JourneyInput.ArriveBy.Text");
            string leaveAtText = page.GetResourceMobile("JourneyInput.LeaveAt.Text");

            btnLeaveAt.Text = leaveAtText;
            btnLeaveAt.ToolTip = leaveAtText;
            btnArriveBy.Text = arriveByText;
            btnArriveBy.ToolTip = arriveByText;

            btnOtherTimes.Text = page.GetResourceMobile("JourneyInput.Time.OtherTimes.Text");
            btnOtherTimes.ToolTip = page.GetResourceMobile("JourneyInput.Time.OtherTimes.ToolTip");
            btnOKOutwardTime.Text = page.GetResourceMobile("JourneyInput.Time.OK.Text");
            btnOKOutwardTime.ToolTip = page.GetResourceMobile("JourneyInput.Time.OK.ToolTip");

            
            string arriveOnText = page.GetResourceMobile("JourneyInput.ArriveOn.Text");
            string leaveOnText = page.GetResourceMobile("JourneyInput.LeaveOn.Text");

            // Show the correct text, the javascript may update displayed text as appropriate if it changes date times
            // for screen reader
            eventDateLabel.Text = ArriveBy ?
                string.Format("<span class=\"show\">{0}</span><span class=\"hide\">{1}</span>", arriveOnText, leaveOnText) :
                string.Format("<span class=\"show\">{0}</span><span class=\"hide\">{1}</span>", leaveOnText, arriveOnText);

            outwardTimeType.Text = ArriveBy ? arriveByText : leaveAtText;
            outwardTimeType.ToolTip = ArriveBy ?
                page.GetResourceMobile("JourneyInput.Time.ArrivalTime.ToolTip") :
                page.GetResourceMobile("JourneyInput.Time.DepartureTime.ToolTip");

            // Set up default display text for the date and time text boxes when not set
            if (string.IsNullOrEmpty(outwardDate.Text))
                outwardDate.Text = DateTime.Now.ToString(DATE_FORMAT);  //page.GetResourceMobile("JourneyInput.Date.SetDate.Text");
            outwardDate.ToolTip = page.GetResourceMobile("JourneyInput.Date.SetDate.ToolTip");
            btnOutwardDate.ToolTip = outwardDate.ToolTip;

            if (string.IsNullOrEmpty(outwardTime.Text))
                outwardTime.Text = DateTimeHelper.GetRoundedTime(DateTime.Now).ToString(TIME_FORMAT); //ArriveBy ? page.GetResourceMobile("JourneyInput.Time.ArrivalTime.Text") : page.GetResourceMobile("JourneyInput.Time.DepartureTime.Text");
            outwardTime.ToolTip = ArriveBy ?
                page.GetResourceMobile("JourneyInput.Time.ArrivalTime.ToolTip") :
                page.GetResourceMobile("JourneyInput.Time.DepartureTime.ToolTip");

            btnOutwardTime.ToolTip = outwardTime.ToolTip;

            // Apply data attribute for javascript use
            outwardDate.Attributes.Add("data-todaytext", TXT_Today);

            hdnPageBackText1.Value = page.GetResourceMobile("JourneyInput.Back.MobileInput.ToolTip");
            hdnPageBackText2.Value = page.GetResourceMobile("JourneyInput.Back.MobileInput.ToolTip");
        }

        /// <summary>
        /// Sets up the now date and time link
        /// </summary>
        private void SetupNowLink()
        {
            if (Properties.Current["EventDateControl.Now.Link.Switch"].Parse(false))
            {
                nowSelectDiv.Visible = true;
            }
            else
            {
                nowSelectDiv.Visible = false;
            }
        }

        /// <summary>
        /// Adds the depart or arrive style the date time inputs
        /// </summary>
        private void SetupDateTimeStyle()
        {
            if (ArriveBy)
            {
                outwardDate.CssClass = outwardDate.CssClass.Replace("arrivalDate", "");
                outwardDate.CssClass += outwardDate.CssClass.Contains(" departureDate") ? "" : " departureDate";
                outwardTime.CssClass = outwardTime.CssClass.Replace("arrivalTime", "");
                outwardTime.CssClass += outwardTime.CssClass.Contains(" departureTime") ? "" : " departureTime";

                btnLeaveAt.CssClass += btnLeaveAt.CssClass.Replace(" selected", "");
                btnArriveBy.CssClass += btnArriveBy.CssClass.Contains(" selected") ? "" : " selected";
            }
            else
            {
                outwardDate.CssClass = outwardDate.CssClass.Replace("departureDate", "");
                outwardDate.CssClass += outwardDate.CssClass.Contains(" arrivalDate") ? "" : " arrivalDate";
                outwardTime.CssClass = outwardTime.CssClass.Replace("departureTime", "");
                outwardTime.CssClass += outwardTime.CssClass.Contains(" arrivalTime") ? "" : " arrivalTime";

                btnLeaveAt.CssClass += btnLeaveAt.CssClass.Contains(" selected") ? "" : " selected";
                btnArriveBy.CssClass += btnArriveBy.CssClass.Replace(" selected", "");
            }
        }
        
        /// <summary>
        /// Builds a datetime object out of the date control and time dropdowns.
        /// </summary>
        /// <returns>Returns a DateTime object, set to MinValue if the control is not set</returns>
        private DateTime GetEventDateTime()
        {
            // If there was a landing page with auto plan true, then need to ensure this control is initialised 
            // (otherwise values can be empty and cause errors here)
            if (forceUpdate)
            {
                PopulateDateDropDowns();
                PopulateTimeDropDowns();
                SetJourneyDateTime();
            }

            // Get date/time for js or non-js input
            bool js = jsEnabled.Value.Parse(true);

            #region Date

            // Date
            DateTime date = DateTime.MinValue;
            string dateString = js ?
                    outwardDate.Text.Trim() :
                    string.Format("{0}/{1}", drpDayListNonJS.SelectedValue, drpMonthListNonJS.SelectedValue);
            
            if (dateString == TXT_Today)
            {
                // Today
                date = DateTime.Now.Date;
            }
            else
            {
                // Check date entered
                DateTime.TryParseExact(dateString,
                        DATE_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
            }

            #endregion

            #region Time

            // Time 
            int minute = 0;
            int hour = 0;
            string[] timeparts = new string[2];

            string timeString = js ?
                outwardTime.Text.Trim() :
                string.Format("{0}:{1}", drpHoursListNonJS.SelectedValue, drpMinutesListNonJS.SelectedValue);

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
        /// <param name="value"></param>
        /// <param name="isReturn"></param>
        private void SetJourneyDateTime()
        {
            if (ValidJourneyDateTime(outwardDateTime, forceUpdate))
            {
                // Set date text
                if (outwardDateTime.Date == DateTime.Now.Date)
                {
                    outwardDate.Text = TXT_Today;
                }
                else
                    outwardDate.Text = outwardDateTime.ToString(DATE_FORMAT);

                // Set time text
                int minuteInterval = Properties.Current["EventDateControl.DropDownTime.IntervalMinutes"].Parse(5);

                DateTime selectedTime = DateTimeHelper.GetRoundedTime(outwardDateTime, minuteInterval);

                outwardTime.Text = selectedTime.ToString(TIME_FORMAT);

                // Set date/time in dropdowns
                drpDayListNonJS.SelectedItem.Selected = false;
                drpMonthListNonJS.SelectedItem.Selected = false;
                drpHoursListNonJS.SelectedItem.Selected = false;
                drpMinutesListNonJS.SelectedItem.Selected = false;

                drpDayListNonJS.SelectedValue = outwardDateTime.ToString("dd");
                drpMonthListNonJS.SelectedValue = string.Format("{0}/{1}", outwardDateTime.ToString("MM"), outwardDateTime.ToString("yyyy"));
                drpHoursListNonJS.SelectedValue = string.Format("{0:00}", selectedTime.Hour);
                drpMinutesListNonJS.SelectedValue = string.Format("{0:00}", selectedTime.Minute);
            }
        }

        /// <summary>
        /// Validates the journey date times
        /// </summary>
        /// <param name="selectedOutwardDateTime"></param>
        /// <param name="forceUpdate">If true, the selected datetime is updated to now if it is in the past</param>
        /// <returns></returns>
        private bool ValidJourneyDateTime(DateTime selectedDateTime, bool forceUpdate)
        {
            bool validDate = true;

            if (selectedDateTime == DateTime.MinValue)
                validDate = false;
            // Date part could be invalid (01/01/0001)
            else if (selectedDateTime.Date == DateTime.MinValue.Date)
                validDate = false;
            // Caller might have reset time part only
            else if (resetTime && selectedDateTime.TimeOfDay == DateTime.MinValue.TimeOfDay)
            {
                validDate = false;

                // Ensure date input displays the date part
                if (selectedDateTime.Date == DateTime.Now.Date)
                {
                    outwardDate.Text = TXT_Today;
                }
                else
                    outwardDate.Text = selectedDateTime.ToString(DATE_FORMAT);

                drpDayListNonJS.SelectedValue = selectedDateTime.ToString("dd");
                drpMonthListNonJS.SelectedValue = string.Format("{0}/{1}", selectedDateTime.ToString("MM"), selectedDateTime.ToString("yyyy"));
            }


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

                outwardDateTime = validDateTime;
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
            DateTimeHelper.SetDefaultDateRange(ref startDate, ref endDate, false);
            
            // Hidden fields
            calendarStartDate.Value = startDate.ToString(DATE_FORMAT);
            calendarEndDate.Value = endDate.ToString(DATE_FORMAT);
            todayDate.Value = DateTime.Now.ToString(DATE_FORMAT);

            #endregion

            if (!IsPostBack)
            {
                DateTime calendarSelectedDate = outwardDateTime;

                #region Set the default selected datetimes
                
                // Set the initial selected outward date value
                if (calendarSelectedDate == DateTime.MinValue)
                {
                    DateTime outwardStartDate = DateTime.MinValue;

                    // Set the default time to use for the start date
                    DateTimeHelper.SetDefaultTimeForDate(startDate, ref outwardStartDate, false);

                    calendarSelectedDate = (DateTime.Now >= outwardStartDate) ? DateTime.Now : outwardStartDate;
                }

                #endregion

                // Setup the selectable calendar
                SetupCalendar(startDate, endDate, calendarSelectedDate);
            }
        }

        /// <summary>
        /// Sets up the selectable calendar dates
        /// </summary>
        private void SetupCalendar(DateTime start, DateTime end, DateTime selected)
        {
            DateTime currentMonth = start;

            List<object> monthList = new List<object>();

            // The following code builds up complete "months" with Mon to Sun dates, 
            // which can include dates from previous month and next which are "disabled"
            while (currentMonth <= end)
            {
                // Determine actual month start 
                DateTime monthStart = currentMonth.AddDays(1 - (int)currentMonth.DayOfWeek);

                // Fix for month start on sunday
                if (currentMonth.Day == 1 && currentMonth.DayOfWeek == DayOfWeek.Sunday)
                {
                    monthStart = currentMonth.AddDays(-6);
                }

                // Determine actual month end
                DateTime monthEnd = new DateTime(currentMonth.Year, currentMonth.Month, 1).AddMonths(1).AddDays(-1);

                List<object> dateList = new List<object>();

                if (monthEnd > end)
                {
                    monthEnd = end;
                }

                monthEnd = monthEnd.AddDays(7 - (int)monthEnd.DayOfWeek);

                DateTime currentDay = monthStart;
                DateTime today = DateTime.Now.Date;

                while (currentDay <= monthEnd)
                {
                    dateList.Add(new { date = currentDay.ToString("dd"), 
                                       month = currentDay.ToString("MM"),
                                       monthName = currentMonth.ToString("MMMM"),
                                       year = currentDay.ToString("yyyy"), 
                                       disabled = (((currentDay < today)
                                                    || (currentDay < currentMonth) 
                                                    || (currentDay.Month != currentMonth.Month) 
                                                    || (currentDay > end)) ? " disabled=\"disabled\" " : ""), 
                                       selected = ((currentDay.Date == selected.Date) ? "" : "") 
                                     });
                    currentDay = currentDay.AddDays(1);
                }

                monthList.Add(new { 
                    monthName = currentMonth.ToString("MMMM"), 
                    year = currentMonth.Year, 
                    dates = dateList });

                currentMonth = currentMonth.AddMonths(1).AddDays(1 - currentMonth.Day);
            }

            outwardDateMonths.DataSource = monthList;
            outwardDateMonths.DataBind();
        }

        /// <summary>
        /// Sets up time control default times
        /// </summary>
        private void InitTime()
        {
            #region Set times

            // Logic below determines the number of hours to show from a start datetime to end datetime,
            // where the start date is today and the end date is also today (i.e. no times into next day)

            int hourStart = Properties.Current["EventDateControl.Time.QuickPick.HourStart"].Parse(-1);
            int hoursToShow = Properties.Current["EventDateControl.Time.QuickPick.HourCount"].Parse(3);
            int minuteInterval = Properties.Current["EventDateControl.Time.QuickPick.IntervalMinutes"].Parse(15);

            DateTime now = DateTime.Now;
            DateTime tomorrow = DateTime.Now.AddDays(1);

            // Default to current hour
            DateTime startTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            DateTime endTime = startTime.AddMinutes(59);

            // If hour start specified
            if ((hourStart >= 0) && (hourStart <= 23))
            {
                startTime = new DateTime(now.Year, now.Month, now.Day, hourStart, 0, 0);
            }

            // Add the hours
            if ((hoursToShow > 0) && (hoursToShow <= 23))
                endTime = startTime.AddHours(hoursToShow);

            if (endTime.Date == tomorrow.Date)
            {
                endTime = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            }
                        
            #endregion

            if (!IsPostBack)
            {
                DateTime selectedTime = outwardDateTime;

                #region Set the default selected datetimes

                // Set the initial selected outward time value
                if (selectedTime == DateTime.MinValue)
                {
                    selectedTime = DateTimeHelper.GetRoundedTime(now);
                }
                else
                    selectedTime = DateTimeHelper.GetRoundedTime(outwardDateTime);

                #endregion

                // Setup the selectable times
                SetupTime(startTime, endTime, selectedTime, minuteInterval);
            }
        }

        /// <summary>
        /// Sets up the selectable times
        /// </summary>
        private void SetupTime(DateTime start, DateTime end, DateTime selected, int minuteIncrement)
        {
            DateTime currentHour = start;
            DateTime today = DateTime.Now;

            bool disableTimeTodayInPast = Properties.Current["EventDateControl.Time.QuickPick.Switch.DisableTimeTodayInThePast"].Parse(false);

            List<object> hourList = new List<object>();

            // The following code builds up time selections, 
            // which can include times (for the current hour) which are "disabled"
            while (currentHour < end)
            {
                // Determine actual month start 
                DateTime minuteStart = currentHour;
                DateTime minuteEnd = currentHour.AddHours(1);

                List<object> minuteList = new List<object>();

                if (minuteEnd > end)
                {
                    minuteEnd = end;
                }
                
                DateTime currentMinute = minuteStart;
                                
                while (currentMinute < minuteEnd)
                {
                    minuteList.Add(new
                    {
                        minuteId = string.Format("minute{0}", currentMinute.ToString("HHmm")),
                        minuteVal = currentMinute.ToString("HH:mm"),
                        minute = currentMinute.ToString("HH:mm"), // Display full time, not just the minute
                        disabled = ((disableTimeTodayInPast && currentMinute.TimeOfDay < today.TimeOfDay) ? " disabled=\"disabled\" " : ""),
                        selected = ((currentMinute.TimeOfDay == selected.TimeOfDay) ? " checked=\"checked\" " : "")
                    });
                    currentMinute = currentMinute.AddMinutes(minuteIncrement);
                }

                hourList.Add(new
                {
                    minutes = minuteList
                });

                currentHour = currentHour.AddHours(1);
            }

            lstHours.DataSource = hourList;
            lstHours.DataBind();
        }

        /// <summary>
        /// Populate the date and month dropdowns
        /// </summary>
        private void PopulateDateDropDowns()
        {
            if (!Page.IsPostBack)
            {
                #region Determine the start and end range and selected date 

                DateTime startDate = DateTime.Now.Date;
                DateTime endDate = DateTime.Now.Date.AddMonths(1);

                // Set the start to end dates to display for the calendar. This will be the period for which
                // journeys can be planned
                DateTimeHelper.SetDefaultDateRange(ref startDate, ref endDate, false);

                DateTime calendarSelectedDate = outwardDateTime;

                #region Set the default selected datetimes

                // Set the initial selected outward date value
                if (calendarSelectedDate == DateTime.MinValue)
                {
                    DateTime outwardStartDate = DateTime.MinValue;

                    // Set the default time to use for the start date
                    DateTimeHelper.SetDefaultTimeForDate(startDate, ref outwardStartDate, false);

                    calendarSelectedDate = (DateTime.Now >= outwardStartDate) ? DateTime.Now : outwardStartDate;
                }

                #endregion

                #endregion

                if (drpDayListNonJS.Items.Count == 0)
                {
                    List<ListItem> daysList = new List<ListItem>();

                    // Days
                    var days = from day in Enumerable.Range(1, 31) // each day to 31
                               // format the day and return the ListItem populated with it
                               select new ListItem(
                                   string.Format("{0}", day),
                                   string.Format("{0:00}", day));

                    daysList = days.ToList();

                    // Add the ListItems to the dropdown
                    drpDayListNonJS.Items.AddRange(daysList.ToArray());

                    drpDayListNonJS.SelectedValue = calendarSelectedDate.ToString("dd");
                }

                if (drpMonthListNonJS.Items.Count == 0)
                {
                    List<ListItem> monthsList = new List<ListItem>();

                    DateTime currentMonth = startDate;

                    while (currentMonth <= endDate)
                    {
                        string monthTxt = string.Format("{0} {1}", currentMonth.ToString("MMM"), currentMonth.ToString("yyyy"));
                        string monthVal = string.Format("{0}/{1}", currentMonth.ToString("MM"), currentMonth.ToString("yyyy"));

                        monthsList.Add(new ListItem(monthTxt, monthVal));

                        currentMonth = currentMonth.AddMonths(1);
                    }

                    // Add the ListItems to the dropdown
                    drpMonthListNonJS.Items.AddRange(monthsList.ToArray());

                    drpMonthListNonJS.SelectedValue = string.Format("{0}/{1}", calendarSelectedDate.ToString("MM"), calendarSelectedDate.ToString("yyyy"));
                }
            }
        }

        /// <summary>
        /// Populate the time dropdowns
        /// </summary>
        private void PopulateTimeDropDowns()
        {
            if (drpHoursListNonJS.Items.Count == 0)
            {
                List<ListItem> timeList = new List<ListItem>();

                // Hours
                var timeHours = from hour in Enumerable.Range(0, 24) // each hour in 0 -23
                                // format the time and return the ListItem populated with it
                                select new ListItem(string.Format("{0:00}", hour),
                                    string.Format("{0:00}", hour));

                timeList = timeHours.ToList();

                // Add the ListItems to the dropdowns
                drpHoursListNonJS.Items.AddRange(timeList.ToArray());

                drpHoursListNonJS.SelectedValue = string.Format("{0:00}", outwardDateTime.Hour);
            }

            if (drpMinutesListNonJS.Items.Count == 0)
            {
                int interval = Properties.Current["EventDateControl.DropDownTime.IntervalMinutes"].Parse(15);

                List<ListItem> timeList = new List<ListItem>();

                // Minutes
                var timeMinutes = from minute in Enumerable.Range(0, 59) // each minute in 0 - 59
                                  where minute % interval == 0 // get the minute with the interval required
                                  // format the time and return the ListItem populated with it
                                  select new ListItem(string.Format("{0:00}", minute),
                                    string.Format("{0:00}", minute));

                timeList = timeMinutes.ToList();

                // Add the ListItems to the dropdowns
                drpMinutesListNonJS.Items.AddRange(timeList.ToArray());

                drpHoursListNonJS.SelectedValue = string.Format("{0:00}", outwardDateTime.Minute);
            }
        }

        #endregion

        #endregion

        #region Protected methods

        /// <summary>
        /// Used by the selectable calendar to show as expanded for the currently selected date
        /// </summary>
        protected string GetCollapsed(object monthName)
        {
            if (outwardDateTime != DateTime.MinValue)
            {
                try
                {
                    DateTime month = DateTime.ParseExact((string)monthName, "MMMM", CultureInfo.InvariantCulture);

                    if (outwardDateTime.Month == month.Month)
                    {
                        // Expand the selected month 
                        return false.ToString().ToLower();
                    }
                }
                catch
                {
                    // Ignore exceptions
                }

            }

            // Collapse month by default
            return true.ToString().ToLower();
        }

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

        #endregion
    }
}
