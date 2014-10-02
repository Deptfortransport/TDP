// *********************************************** 
// NAME                 : ResultsTableTitleControl.aspx.cs 
// AUTHOR               : Richard Hopkins
// DATE CREATED         : 17/03/05
// DESCRIPTION			: Used to display a title for the results table
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ResultsTableTitleControl.ascx.cs-arc  $
//
//   Rev 1.6   Oct 28 2010 11:59:26   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.5   Mar 30 2010 17:06:48   mmodi
//Ensure all Bank holiday labels and link are hidden to avoid screenreader issue
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.4   Nov 05 2008 09:51:54   rbroddle
//Actioning code review comments for CCN460 Better use of seasonal noticeboard
//Resolution for 5103: ATO585 CCN460 Better Use of Seasonal Noticeboard
//
//   Rev 1.3   Oct 13 2008 16:44:22   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.0   Oct 08 2008 14:01:46   rbroddle
//CCN460 Better Use of Seasonal Noticeboard  
//Resolution for 5103: ATO585 CCN460 Better Use of Seasonal Noticeboard
//
//   Rev 1.2   Mar 31 2008 13:22:42   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:22   mturner
//Initial revision.
//
//   Rev 1.4   Mar 13 2006 16:15:54   echan
//stream3353 manual merge
//
//   Rev 1.2.2.0   Feb 24 2006 10:47:50   pcross
//Allowed an override for the main title label text instead of derivation from journey state
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Feb 23 2006 19:17:04   build
//Automatically merged from branch for stream3129
//
//   Rev 1.2.1.0   Jan 10 2006 15:27:00   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Apr 15 2005 19:19:56   rhopkins
//Added "Open return {mode} journeys".
//Also improvements to the way the resource IDs are structured.
//
//   Rev 1.1   Mar 29 2005 14:48:06   rhopkins
//Minor corrections
//Resolution for 1927: DEV Code Review: FAF date selection
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.0   Mar 23 2005 16:58:58   rhopkins
//Initial revision.
//

namespace TransportDirect.UserPortal.Web.Controls
{
    using System;
    using TransportDirect.Common.ResourceManager;
    using System.Web.UI.WebControls;
    using TransportDirect.Common;
    using TransportDirect.Common.Logging;
    using TransportDirect.Common.ServiceDiscovery;
    using TransportDirect.Common.DatabaseInfrastructure.Content;
    using Logger = System.Diagnostics.Trace;
    using TransportDirect.UserPortal.DataServices;
    using TransportDirect.UserPortal.SessionManager;
    using TransportDirect.UserPortal.PricingRetail.Domain;
    using TransportDirect.UserPortal.Resource;

    // Different types of results that can be displayed
    public enum DisplayResultsType : int
    {
        Journeys,
        JourneysOpenReturn,
        ExtendedJourney,
        Extensions,
        FindACar,
        Fares,
        FaresOpenReturn
    }

    /// <summary>
    /// Used to display page title on a journey
    /// planner output page. Text is determined based on the current
    /// output page id and the state of the itinerary manager.
    /// </summary>
    public partial class ResultsTableTitleControl : TDUserControl
    {

        /// <summary>
        /// Enumerator used locally to indicate three possible result displays
        /// </summary>
        private enum DirectionType : int
        {
            SingleOutward,
            SingleInward,
            Return
        }

        /// <summary>
        /// Enumerator used locally to indicate whether a Bank Holiday has been found
        /// </summary>
        private enum BankHolidayMatch : int
        {
            None,
            England,
            Scotland,
            EnglandAndScotland
        }


        DataServices dataServices;

        private bool outward;
        private TDDateTime outwardFirstDate;
        private TDDateTime outwardLastDate;
        private bool outwardAnytime;
        private bool outwardArriveBefore;
        private TDDateTime inwardFirstDate;
        private TDDateTime inwardLastDate;
        private bool inwardAnytime;
        private bool inwardArriveBefore;
        private bool showTicketType;
        private bool showMode;
        private TicketTravelMode travelMode;
        private bool showTime;
        private bool showBankHoliday;
        private DisplayResultsType resultsType;

        private bool noDates;
        private DirectionType directionType;
        private BankHolidayMatch bankHolidayMatch;

        private string specialEventMessage;

        /// <summary>
        /// Used to store the labelText when LabelTextOverride property is set.
        /// The property overrides the derivation of labelText based on the attributes
        /// of the ResultsTableTitleControl instance.
        /// </summary>
        private string labelTextOverride = String.Empty;

        #region Public Properties

        /// <summary>
        /// Get/set what type of results are being displayed.
        /// </summary>
        public DisplayResultsType ResultsType
        {
            get { return resultsType; }
            set { resultsType = value; }
        }

        /// <summary>
        /// Get/set whether label is for Outward or Return table.
        /// Only required if no dates are supplied.
        /// </summary>
        public bool Outward
        {
            get { return outward; }
            set { outward = value; }
        }

        /// <summary>
        /// Get/set the first (or only) date for the outward part of the journey.
        /// Only to be supplied if table contains results that cover outward journeys.
        /// </summary>
        public TDDateTime OutwardFirstDate
        {
            get { return outwardFirstDate; }
            set { outwardFirstDate = value; }
        }

        /// <summary>
        /// Get/set the last date for the outward part of the journey.
        /// Only to be supplied if table contains results that cover outward journeys over a range of dates.
        /// </summary>
        public TDDateTime OutwardLastDate
        {
            get { return outwardLastDate; }
            set { outwardLastDate = value; }
        }

        /// <summary>
        /// Get/set that the User has requested outward journeys at any time of the day
        /// </summary>
        public bool OutwardAnytime
        {
            get { return outwardAnytime; }
            set { outwardAnytime = value; }
        }

        /// <summary>
        /// Get/set that the time component of the supplied OutwardDate is the time that the User wishes to arrive by
        /// </summary>
        public bool OutwardArriveBefore
        {
            get { return outwardArriveBefore; }
            set { outwardArriveBefore = value; }
        }

        /// <summary>
        /// Get/set the first (or only) date for the inward part of the journey.
        /// Only to be supplied if table contains results that cover inward journeys.
        /// </summary>
        public TDDateTime InwardFirstDate
        {
            get { return inwardFirstDate; }
            set { inwardFirstDate = value; }
        }

        /// <summary>
        /// Get/set the last date for the inward part of the journey.
        /// Only to be supplied if table contains results that cover inward journeys over a range of dates.
        /// </summary>
        public TDDateTime InwardLastDate
        {
            get { return inwardLastDate; }
            set { inwardLastDate = value; }
        }

        /// <summary>
        /// Get/set that the User has requested inward journeys at any time of the day
        /// </summary>
        public bool InwardAnytime
        {
            get { return inwardAnytime; }
            set { inwardAnytime = value; }
        }

        /// <summary>
        /// Get/set that the time component of the supplied InwardDate is the time that the User wishes to arrive by
        /// </summary>
        public bool InwardArriveBefore
        {
            get { return inwardArriveBefore; }
            set { inwardArriveBefore = value; }
        }

        /// <summary>
        /// Get/set that the outward and inward titles should be identified as "Singles (outward/inward)" if appropriate
        /// </summary>
        public bool ShowTicketType
        {
            get { return showTicketType; }
            set { showTicketType = value; }
        }

        /// <summary>
        /// Get/set that the transport mode should be included in the title
        /// If this is set to "true" then TravelMode must also be specified.
        /// </summary>
        public bool ShowMode
        {
            get { return showMode; }
            set { showMode = value; }
        }

        /// <summary>
        /// Get/set the Travel Mode that is being used.
        /// Only required if ShowMode is set to "true".
        /// </summary>
        public TicketTravelMode TravelMode
        {
            get { return travelMode; }
            set { travelMode = value; }
        }

        /// <summary>
        /// Get/set whether the time component of the supplied dates (or "any time" if appropriate) should be displayed
        /// </summary>
        public bool ShowTime
        {
            get { return showTime; }
            set { showTime = value; }
        }

        /// <summary>
        /// Get/set whether to display a note advising the User of the presence of a bank holiday in the supplied date ranges.
        /// </summary>
        public bool ShowBankHoliday
        {
            get { return showBankHoliday; }
            set { showBankHoliday = value; }
        }

        /// <summary>
        /// Get/set override labelText.
        /// The property overrides the derivation of labelText based on the attributes
        /// of the ResultsTableTitleControl instance.
        /// </summary>
        public string LabelTextOverride
        {
            get { return labelTextOverride; }
            set { labelTextOverride = value; }
        }

        #endregion Public Properties

        /// <summary>
        /// Event handler for page PreRender event fired by page
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            string labelText;

            dataServices = (DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

            DetermineTitleFormat();

            // Use the override value passed in, or if none, derive the label text from the attributes of
            // the ResultTableTitleControl instance
            if (labelTextOverride.Length > 0)
                labelText = labelTextOverride;
            else
                labelText = GetLabelText();

            if (showMode)
            {
                string modeText = GetResource("TransportModeLowerCase." + travelMode.ToString());
                if (modeText != null)
                {
                    mainLabel.Text = String.Format(labelText, modeText);
                }
                else
                {
                    mainLabel.Text = String.Empty;
                }
            }
            else
            {
                mainLabel.Text = labelText;
            }

            if (noDates)
            {
                // Only use the main label, make all others invisible
                dateLabel.Visible = false;
                divBankHoliday.Visible = false;
                bankHolidayLabel.Visible = false;
                bankHolidayLinkControl.Visible = false;

                divSpecialEvent.Visible = false;
                specialEventLabel.Visible = false;
                specialEventLabel.Text = String.Empty;
            }
            else
            {
                dateLabel.Visible = true;
                PopulateDateLabel();

                PopulateBankHolidayAndSpecialEvents();

                if (!showBankHoliday)
                {
                    divBankHoliday.Visible = false;
                    bankHolidayLinkControl.Visible = false;
                    bankHolidayLabel.Visible = false;
                }
                if (string.IsNullOrEmpty(specialEventMessage))
                {
                    divSpecialEvent.Visible = false;
                    specialEventLabel.Visible = false;
                    specialEventLabel.Text = String.Empty;
                }
            }
        }

        /// <summary>
        /// Determine what format of title to display, based on which properties have been supplied.
        /// </summary>
        private void DetermineTitleFormat()
        {
            outwardLastDate = LastDateLaterThanFirstDate(outwardFirstDate, outwardLastDate);
            inwardLastDate = LastDateLaterThanFirstDate(inwardFirstDate, inwardLastDate);

            noDates = false;
            if (outwardFirstDate != null)
            {
                if (inwardFirstDate != null)
                {
                    // Both Outward and Inward dates have been supplied so this table shows results for a Return
                    directionType = DirectionType.Return;
                }
                else
                {
                    // Only an Outward date has been suplied so the table only shows Outward results
                    directionType = DirectionType.SingleOutward;
                }
            }
            else
            {
                if (inwardFirstDate != null)
                {
                    // Only an Inward date has been suplied so the table only shows Inward results
                    directionType = DirectionType.SingleInward;
                }
                else
                {
                    // No dates have been supplied
                    if (outward)
                    {
                        directionType = DirectionType.SingleOutward;
                    }
                    else
                    {
                        directionType = DirectionType.SingleInward;
                    }
                    noDates = true;
                }
            }
        }

        /// <summary>
        /// If the last date is the same as the first date then set to null to simplify the logic for handling single dates / date ranges
        /// </summary>
        /// <param name="firstDate">The first date of the supposed range</param>
        /// <param name="lastDate">The last date of the supposed range</param>
        /// <returns>Returns null if the last date equals the first date, otherwise the supplied last date is returned unaltered</returns>
        private TDDateTime LastDateLaterThanFirstDate(TDDateTime firstDate, TDDateTime lastDate)
        {
            if ((firstDate != null) && (lastDate != null) && (lastDate.GetDateTime().Date.CompareTo(firstDate.GetDateTime().Date) > 0))
            {
                return lastDate;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the text using a resource ID contructed based on the attributes of the ResultTableTitleControl instance
        /// </summary>
        /// <returns></returns>
        private string GetLabelText()
        {
            string labelText;
            string modeResourcePart;
            string ticketTypeResourcePart;

            string baseResourceString = "ResultsTableTitle." + resultsType.ToString() + "." + directionType.ToString();

            if (showMode)
            {
                modeResourcePart = ".WithMode";
            }
            else
            {
                modeResourcePart = String.Empty;
            }

            if (showTicketType)
            {
                ticketTypeResourcePart = ".TicketType";
            }
            else
            {
                ticketTypeResourcePart = String.Empty;
            }

            // Try to get the text using the "ideal" resource ID
            // If we do not find any resource text then keep looking by removing parts of the resource ID, in order of importance

            labelText = GetResource(baseResourceString + ticketTypeResourcePart + modeResourcePart);

            if ((labelText != null) && (labelText != String.Empty))
            {
                return labelText;
            }

            if (showMode)
            {
                // We were looking for a resource for the mode, so look for one without the mode
                labelText = GetResource(baseResourceString + ticketTypeResourcePart);

                if ((labelText != null) && (labelText != String.Empty))
                {
                    return labelText;
                }
            }

            if (showTicketType)
            {
                // We were looking for a resource for the Ticket Type, so look for one without the Ticket Type
                labelText = GetResource(baseResourceString + modeResourcePart);

                if ((labelText != null) && (labelText != String.Empty))
                {
                    return labelText;
                }
            }

            if (showMode && showTicketType)
            {
                // We were looking for a resource for the mode and Ticket Type, so look for one without both
                labelText = GetResource(baseResourceString);

                if ((labelText != null) && (labelText != String.Empty))
                {
                    return labelText;
                }
            }

            return String.Empty;
        }

        /// <summary>
        ///	Format the supplied dates into the date label
        /// </summary>
        private void PopulateDateLabel()
        {
            // Get the boilerplate text
            string forString = GetResource("JourneyPlanner.labelFor");
            string returningString = GetResource("JourneyPlanner.labelReturning");

            // Combine the appropriate Outward and Inward dates with the boilerplate text
            switch (directionType)
            {
                case DirectionType.SingleOutward:
                    dateLabel.Text = forString + " " + FormatDate(outwardFirstDate, outwardLastDate, outwardAnytime, outwardArriveBefore);
                    break;
                case DirectionType.SingleInward:
                    dateLabel.Text = forString + " " + FormatDate(inwardFirstDate, inwardLastDate, inwardAnytime, inwardArriveBefore);
                    break;
                case DirectionType.Return:
                    dateLabel.Text = forString + " " + FormatDate(outwardFirstDate, outwardLastDate, outwardAnytime, outwardArriveBefore)
                        + returningString + " " + FormatDate(inwardFirstDate, inwardLastDate, inwardAnytime, inwardArriveBefore);
                    break;
                default:
                    dateLabel.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// Output a note about Bank Holidays if appropriate for the supplied dates.
        /// </summary>
        private void PopulateBankHolidayAndSpecialEvents()
        {
            TDDateTime firstDate;
            TDDateTime lastDate;
            int dayCount;
            bankHolidayMatch = BankHolidayMatch.None;

            // Search the Outward and Inward dates in turn, but as soon as we find holiday for "EnglandAndScotland"
            // then stop searching because that is the most holiday that we can report.

            // Test Outward date range for Public Holidays
            if (outwardFirstDate != null)
            {
                firstDate = new TDDateTime(outwardFirstDate.GetDateTime().Date);

                if (outwardLastDate == null)
                {
                    TestDateForBankHolidayOrSpecialEvent(firstDate);
                }
                else
                {
                    lastDate = new TDDateTime(outwardLastDate.GetDateTime().Date);
                    dayCount = lastDate.GetDifferenceDays(firstDate) + 1;
                    for (int i = 0; ((i < dayCount) && (bankHolidayMatch != BankHolidayMatch.EnglandAndScotland)); i++)
                    {
                        TestDateForBankHolidayOrSpecialEvent(firstDate.AddDays(i));
                    }
                }
            }

            // Test Inward date range for Public Holidays
            if ((inwardFirstDate != null) && (bankHolidayMatch != BankHolidayMatch.EnglandAndScotland))
            {
                firstDate = new TDDateTime(inwardFirstDate.Year, inwardFirstDate.Month, inwardFirstDate.Day);

                if (inwardLastDate == null)
                {
                    TestDateForBankHolidayOrSpecialEvent(firstDate);
                }
                else
                {
                    lastDate = new TDDateTime(inwardLastDate.GetDateTime().Date);
                    dayCount = lastDate.GetDifferenceDays(firstDate) + 1;
                    for (int i = 0; ((i < dayCount) && (bankHolidayMatch != BankHolidayMatch.EnglandAndScotland)); i++)
                    {
                        TestDateForBankHolidayOrSpecialEvent(firstDate.AddDays(i));
                    }
                }
            }

            //Get the text for the hyperlink as this does'nt change
            bankHolidayLinkControl.Text = GetResource("JourneyPlanner.hyperlinkBankHolidayInfo");
            divBankHoliday.Visible = true;
            bankHolidayLinkControl.Visible = true;
            bankHolidayLabel.Visible = true;

            switch (bankHolidayMatch)
            {
                case BankHolidayMatch.EnglandAndScotland:
                    bankHolidayLabel.Text = GetResource("JourneyPlanner.MessageHolidayInUK");
                    break;
                case BankHolidayMatch.England:
                    bankHolidayLabel.Text = GetResource("JourneyPlanner.MessageHolidayInEnglandWales");
                    break;
                case BankHolidayMatch.Scotland:
                    bankHolidayLabel.Text = GetResource("JourneyPlanner.MessageHolidayInScotland");
                    break;
                default:
                    // There are no Bank Holidays in the supplied date ranges
                    divBankHoliday.Visible = false;
                    bankHolidayLinkControl.Visible = false;
                    bankHolidayLabel.Visible = false;
                    break;
            }
            if (!string.IsNullOrEmpty(specialEventMessage))
            {
                divSpecialEvent.Visible = true;
                specialEventLabel.Visible = true;
                //show the print friendly version from content if in print friendly mode, else show normal one
                if (bankHolidayLinkControl.PrinterFriendly)
                {
                    specialEventLabel.Text = GetResource((specialEventMessage + ".PrintFriendly"));
                }
                else
                {
                    specialEventLabel.Text = GetResource(specialEventMessage);
                }
            }
            else
            {
                divSpecialEvent.Visible = false;
                specialEventLabel.Visible = false;
                specialEventLabel.Text = String.Empty;
            }
        }

        /// <summary>
        /// Render the date/time range in an appropriate format
        /// </summary>
        /// <param name="firstDate">Start of the date range</param>
        /// <param name="lastDate">End of the date range (may be null if only single date required)</param>
        /// <param name="anyTime">True if the User has indicated that they are prepared to travel at any time on the supplied date(s)</param>
        /// <param name="arriveBefore">
        ///		True if the User has indicated that they wish to Arrive Before the supplied time.
        ///		False if the User has indicated that they wish to Leave After the supplied time.
        ///	</param>
        /// <returns>String containing the formatted date range</returns>
        private string FormatDate(TDDateTime firstDate, TDDateTime lastDate, bool anyTime, bool arriveBefore)
        {
            string timeString = String.Empty;
            string lastTime = String.Empty;

            if (showTime)
            {
                if (anyTime)
                {
                    timeString = " " + GetResource("JourneyPlanner.labelAnyTime");
                }
                else
                {
                    string timeType = String.Empty;

                    if (arriveBefore)
                    {
                        timeType = " " + GetResource("JourneyPlanner.labelArrivingBefore");
                    }
                    else
                    {
                        timeType = " " + GetResource("JourneyPlanner.labelLeavingAfter");
                    }

                    timeString = timeType + " " + firstDate.ToString("HH:mm");
                }

            }

            if (lastDate != null)
            {
                return firstDate.ToString("ddd dd MMM yy") + " - " + lastDate.ToString("ddd dd MMM yy") + timeString;
            }
            else
            {
                return firstDate.ToString("ddd dd MMM yy") + timeString;
            }
        }

        /// <summary>
        /// Test whether the supplied date is a bank holiday in England/Wales and/or Scotland.
        /// Sets global bankHolidayMatch if the date is a holiday.
        /// Also check if the supplied date is a special event that may lead to transport disruption.
        /// Sets global specialEventMessage to relevant value to lookup from content if so
        /// </summary>
        /// <param name="dateToTest">Date to be tested for Bank Holiday or special event</param>
        private void TestDateForBankHolidayOrSpecialEvent(TDDateTime dateToTest)
        {
            bool england = dataServices.IsHoliday(DataServiceType.BankHolidays, dateToTest, DataServiceCountries.EnglandWales);
            bool scotland = dataServices.IsHoliday(DataServiceType.BankHolidays, dateToTest, DataServiceCountries.Scotland);
            
            //Get special event text
            try
            {
                specialEventMessage = dataServices.GetSpecialEventMessageName(DataServiceType.SpecialEvents, dateToTest);
            }
            catch
            {
                OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Error trying to get special event message");
                Logger.Write(oe);
                specialEventMessage = string.Empty;
            }

            if (england && scotland)
            {
                bankHolidayMatch = BankHolidayMatch.EnglandAndScotland;
                return;
            }
            else if (scotland)
            {
                if (bankHolidayMatch == BankHolidayMatch.England)
                {
                    bankHolidayMatch = BankHolidayMatch.EnglandAndScotland;
                }
                else
                {
                    bankHolidayMatch = BankHolidayMatch.Scotland;
                }
                return;
            }
            else if (england)
            {
                if (bankHolidayMatch == BankHolidayMatch.Scotland)
                {
                    bankHolidayMatch = BankHolidayMatch.EnglandAndScotland;
                }
                else
                {
                    bankHolidayMatch = BankHolidayMatch.England;
                }
                return;
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Event handler for click of the Bank Holiday Link - show Seasonal Noticeboard
        /// </summary>
        protected void bankHolidayLinkControl_Click(object sender, EventArgs e)
        {
            // Set page id in stack so we know where to come back to
            TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(PageId);

            // Navigate to the Seasonal Noticeboard page
            TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.SeasonalNoticeBoard;
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

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            bankHolidayLinkControl.link_Clicked += new EventHandler(bankHolidayLinkControl_Click);
        }

        #endregion

    }
}