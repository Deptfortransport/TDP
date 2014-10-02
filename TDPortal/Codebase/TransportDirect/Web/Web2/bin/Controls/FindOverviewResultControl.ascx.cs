// *********************************************** 
// NAME                 : FindOverviewResultControl.ascx
// AUTHOR               : Dan Gath
// DATE CREATED         : 11/01/2008
// DESCRIPTION			: Journey overview results
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindOverviewResultControl.ascx.cs-arc  $
//
//   Rev 1.13   Jan 20 2013 16:26:42   mmodi
//Updated for transport modetype Telecabine
//Resolution for 5884: CCN:XXX - Telecabine modetype
//
//   Rev 1.12   Feb 25 2010 11:01:52   mmodi
//Corrected display of lines for City to city
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.11   Feb 24 2010 11:08:12   mmodi
//Reset to show Emissions control in Diagram view when first shown
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.10   Feb 23 2010 15:24:30   mmodi
//Reset ShowCO2 panel when moving to Details page
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.9   Feb 23 2010 13:30:12   mmodi
//Updated to change order of Overview lines, and to hide More button based on FindAMode value
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.8   Feb 22 2010 08:23:00   rbroddle
//Updated to show " - " for car earliest arrive/depart times in TDExtra
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.7   Feb 17 2010 12:21:20   mmodi
//Added Transfer mode to allow international journerys to be shown Details page
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.6   Aug 04 2009 14:23:48   mmodi
//Updated following changes to the JourneyEmissionsHelper
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.5   Jan 12 2009 14:50:32   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.4   Jul 18 2008 11:47:46   mmodi
//Updated to get mode type from the journey
//Resolution for 5075: City to city - Coach journey icon sometimes not shown
//
//   Rev 1.3   Jun 18 2008 16:11:40   dgath
//fixed ITP issues
//Resolution for 5025: ITP: Workstream
//
//   Rev 1.2   Mar 31 2008 13:20:44   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory Mar 28 2008 14:00:00 mmodi
//Clear stored journey data before going to Journey Details to ensure it reloads data for the mode selected
//
//   Rev DevFactory Feb 20 2008 14:00:00 mmodi
//Amended to set outward and return journey type
//
//   Rev DevFactory Jan 20 2008 19:00:00 dgath
//CCN0382b City to City enhancements
//Initial revision.
//Displays JourneyOverviewLines in a repeater


using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.Resource;
using System.Collections.Generic;

namespace TransportDirect.UserPortal.Web.Controls
{
    public partial class FindOverviewResultControl : TDUserControl
    {
        #region Private members
        private bool printMode;
        private bool outward;
        private bool arriveBefore;
        private FindAMode findAMode;

        private JourneyOverviewLine[] journeyOverviewLines;
        #endregion

        #region Page_Load, PagePreRender, Initialise
        /// <summary>
        /// Page load. Binds data to the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Load the data and bind it to the grid. If this is not done, the events will not
            // be raised
            LoadOverviewData();

            BindGrid();
        }

        /// <summary>
        /// Page PreRender. Hides repeater if no journey lines
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Don't display control if we have no journeys
            if((journeyOverviewLines == null) || (journeyOverviewLines.Length == 0))
                overviewRepeater.Visible = false;
        }

        /// <summary>
        /// Initialise control
        /// </summary>
        /// <param name="printMode"></param>
        /// <param name="outward"></param>
        /// <param name="arriveBefore"></param>
        /// <param name="findAMode">FindAMode to allow specific formatting of control output</param>
        public void Initialise(bool printMode, bool outward, bool arriveBefore, FindAMode findAMode)
        {
            this.printMode = printMode;
            this.outward = outward;
            this.arriveBefore = arriveBefore;
            this.findAMode = findAMode;
        }
                
        #endregion

        #region Private methods

        /// <summary>
        /// Load the overview data
        /// </summary>
        /// <param name="forceLoad"></param>
        private void LoadOverviewData()
        {
            TDJourneyResult journeyResult = (TDJourneyResult)TDItineraryManager.Current.JourneyResult;

            if (outward)
                journeyOverviewLines = journeyResult.OutwardJourneyOverview(arriveBefore);
            else
                journeyOverviewLines = journeyResult.ReturnJourneyOverview(arriveBefore);

            journeyOverviewLines = SortJourneyOverviewLines(journeyOverviewLines);
        }

        /// <summary>
        /// Binds overlines to the repeater grid, dependent on print mode
        /// </summary>
        private void BindGrid()
        {
            if (printMode)
            {
                overviewRepeaterPrintable.DataSource = journeyOverviewLines;
                overviewRepeaterPrintable.DataBind();
                overviewRepeaterPrintable.Visible = true;
            }
            else
            {
                overviewRepeater.DataSource = journeyOverviewLines;
                overviewRepeater.DataBind();
                overviewRepeater.Visible = true;
                overviewRepeaterPrintable.Visible = false;
            }
        }

        /// <summary>
        /// Method to sort the overview lines, based on the FindAMode of this control
        /// </summary>
        /// <param name="jols"></param>
        /// <returns></returns>
        private JourneyOverviewLine[] SortJourneyOverviewLines(JourneyOverviewLine[] jols)
        {
            List<JourneyOverviewLine> sortedJols = new List<JourneyOverviewLine>();

            // For international planner, we want the order to be Rail, Air, Coach, Car
            if (findAMode == FindAMode.International)
            {
                sortedJols.AddRange(GetFilteredOverviewLines(ref jols, ModeType.Rail));
                sortedJols.AddRange(GetFilteredOverviewLines(ref jols, ModeType.Air));
                sortedJols.AddRange(GetFilteredOverviewLines(ref jols, ModeType.Coach));
                sortedJols.AddRange(GetFilteredOverviewLines(ref jols, ModeType.Car));

                // And add any remaining jols
                sortedJols.AddRange(jols);
            }
            else
            {
                // No Sorting required
                sortedJols.AddRange(jols);
            }

            return sortedJols.ToArray();
        }

        /// <summary>
        /// Returns the JourneyOverviewLines from the referenced array which match the specified ModeType. 
        /// The matching overview lines are then removed from the referenced array.
        /// </summary>
        private JourneyOverviewLine[] GetFilteredOverviewLines(ref JourneyOverviewLine[] jols, ModeType modeType)
        {
            List<JourneyOverviewLine> filteredJols = new List<JourneyOverviewLine>();
            List<JourneyOverviewLine> remainingJols = new List<JourneyOverviewLine>();

            foreach (JourneyOverviewLine jol in jols)
            {
                if (jol.Mode == modeType)
                {
                    filteredJols.Add(jol);
                }
                else
                {
                    remainingJols.Add(jol);
                }
            }

            // Update the reference original array
            jols = remainingJols.ToArray();

            return filteredJols.ToArray();
        }
        
        /// <summary>
        /// Returns the time as a string in the format HH:mm
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private string GetFormattedDateTime(TDDateTime time)
        {
            if (time.Second >= 30)
                time = time.AddMinutes(1);

            return time.ToString("HH:mm");
        }

        /// <summary>
        /// Return mode type as string for the JourneyOverviewLine
        /// </summary>
        /// <param name="jol"></param>
        /// <returns></returns>
        private string GetTransportMode(JourneyOverviewLine jol)
        {
            string transportMode = string.Empty;

            if (jol.Modes.Length > 0)
            {
                ArrayList journeyModes = new ArrayList();
                journeyModes.AddRange(jol.Modes);

                bool coachmode = ((journeyModes.Contains(ModeType.Coach)) || (journeyModes.Contains(ModeType.Bus)));
                bool trainmode = ((journeyModes.Contains(ModeType.Rail))
                           ||
                         (journeyModes.Contains(ModeType.RailReplacementBus))
                           ||
                         (journeyModes.Contains(ModeType.Metro))
                           ||
                         (journeyModes.Contains(ModeType.Tram))
                           ||
                         (journeyModes.Contains(ModeType.Underground)));

                bool airmode = journeyModes.Contains(ModeType.Air);

                // ITP journey
                if ((airmode && (coachmode || trainmode)) || (coachmode && trainmode && !airmode))
                    transportMode = string.Format("{0}_{1}_{2}", ModeType.Air.ToString(), ModeType.Coach.ToString(), ModeType.Rail.ToString());
                else
                {   // All other journeys
                    ModeType modeType = jol.Modes[0];

                    // If the modeType is not one of the case statements below, we should use the 
                    // Journey ModeType. This can be acheived using the fastest journey in the JOL
                    if (modeType != ModeType.Air && modeType != ModeType.Coach && modeType != ModeType.Rail
                        && modeType != ModeType.Car)
                    {
                        // Assume it is a public journey (as Car JOL will only have modeType of car)
                        TransportDirect.UserPortal.JourneyControl.PublicJourney pj = (TransportDirect.UserPortal.JourneyControl.PublicJourney)jol.FastestJourney;
                        modeType = pj.GetJourneyModeType();
                    }

                    switch (modeType)
                    {
                        case ModeType.Air:
                            transportMode = ModeType.Air.ToString();
                            break;

                        case ModeType.Coach:
                            transportMode = ModeType.Coach.ToString();
                            break;

                        case ModeType.Rail:
                            transportMode = ModeType.Rail.ToString();
                            break;

                        case ModeType.Car:
                            transportMode = ModeType.Car.ToString();
                            break;

                    }
                }
            }

            return transportMode;
        }

        /// <summary>
        /// Returns all the mode types for the provided modeType
        /// e.g. For Rail, this returns an array containing Rail, Underground, Metro...
        /// </summary>
        /// <param name="modeType"></param>
        /// <returns></returns>
        private ModeType[] GetModeTypeSelected(string modeType)
        {
            List<ModeType> modeTypes = new List<ModeType>();

            switch (modeType)
            {
                case "Rail":
                    modeTypes.Add(ModeType.Rail);
                    modeTypes.Add(ModeType.Metro);
                    modeTypes.Add(ModeType.RailReplacementBus);
                    modeTypes.Add(ModeType.Tram);
                    modeTypes.Add(ModeType.Underground);
                    modeTypes.Add(ModeType.Drt);
                    modeTypes.Add(ModeType.Transfer);
                    break;

                case "Coach":
                    modeTypes.Add(ModeType.Coach);
                    modeTypes.Add(ModeType.Bus);
                    modeTypes.Add(ModeType.Transfer);
                    break;

                case "Car":
                    modeTypes.Add(ModeType.Car);
                    break;

                case "Air":
                    modeTypes.Add(ModeType.Air);
                    modeTypes.Add(ModeType.CheckIn);
                    modeTypes.Add(ModeType.CheckOut);
                    modeTypes.Add(ModeType.Transfer);
                    break;

                case "Air_Coach_Rail":
                    modeTypes.Add(ModeType.Rail);
                    modeTypes.Add(ModeType.Metro);
                    modeTypes.Add(ModeType.RailReplacementBus);
                    modeTypes.Add(ModeType.Tram);
                    modeTypes.Add(ModeType.Underground);
                    modeTypes.Add(ModeType.Drt);
                    modeTypes.Add(ModeType.Coach);
                    modeTypes.Add(ModeType.Bus);
                    modeTypes.Add(ModeType.Air);
                    modeTypes.Add(ModeType.CheckIn);
                    modeTypes.Add(ModeType.CheckOut);
                    modeTypes.Add(ModeType.Transfer);
                    modeTypes.Add(ModeType.Telecabine);
                    break;
            }

            // Add these to all
            modeTypes.Add(ModeType.Ferry);
            modeTypes.Add(ModeType.Taxi);

            return modeTypes.ToArray();
        }

        /// <summary>
        /// Adds the event handlers to the controls in the bound repeater
        /// </summary>
        private void AddEventHandlers()
        {
            // Add handlers if appropriate
            if (!printMode)
            {
                // Add event handlers
                TDButton tdbutton;

                for (int i = 0; i < overviewRepeater.Items.Count; i++)
                {
                    tdbutton = (TDButton)overviewRepeater.Items[i].FindControl("buttonMore");
                    if (tdbutton != null)
                    {
                        tdbutton.Click += new EventHandler(buttonMore_Click);
                    }
                }
            }
        }



        #endregion

        #region Repeater public methods
        /// <summary>
        /// Returns the resource item for the column header text item
        /// </summary>
        public string HeaderItem(int column)
        {
            return Global.tdResourceManager.GetString("FindOverviewResultControl.HeaderItemText"
                    + column, TDCultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Used to set the Id attribute of the Html row in the
        /// item template of the repeater control.
        /// This can than be accessed in order to scroll it into view.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetItemRowId(int index)
        {
            return overviewRepeater.ClientID + "_itemRow_" + index.ToString(TDCultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Number of journeys, empty string returned as &nbsp
        /// </summary>
        /// <param name="jol"></param>
        /// <returns></returns>
        public string GetNumberOfJourneys(JourneyOverviewLine jol)
        {
            string numberOfJourneys = jol.NumberOfJourneys.ToString();
            
            // Space to ensure control displays number
            if (string.IsNullOrEmpty(numberOfJourneys))
                numberOfJourneys = "&nbsp;";
            
            return numberOfJourneys;
        }

        /// <summary>
        /// Returns journey duration as a formatted string, "2 hours, 22 mins"
        /// </summary>
        /// <param name="jol"></param>
        /// <returns></returns>
        public string GetFastestDuration(JourneyOverviewLine jol)
        {
            TimeSpan duration = jol.Duration;

            string durationReturn = String.Empty;

            #region resource strings
            string hoursString = Global.tdResourceManager.GetString(
                "JourneyDetailsTableControl.hoursString", TDCultureInfo.CurrentUICulture);

            string minutesString = Global.tdResourceManager.GetString(
                "JourneyDetailsTableControl.minutesString", TDCultureInfo.CurrentUICulture);

            string hourString = Global.tdResourceManager.GetString(
                "JourneyDetailsTableControl.hourString", TDCultureInfo.CurrentUICulture);

            string minuteString = Global.tdResourceManager.GetString(
                "JourneyDetailsTableControl.minuteString", TDCultureInfo.CurrentUICulture);

            string milesString = Global.tdResourceManager.GetString(
                "SummaryResultTable.labelMilesString", TDCultureInfo.CurrentUICulture);
            #endregion

            //Get duration hours and minutes
            int durationHours = 0;
            int durationMinutes = 0;

            //Greater than 24 hours case
            if (duration.Days > 0)
            {
                // For each day, there are 24 hours
                durationHours = 24 * duration.Days + duration.Hours;
            }
            else if (duration.Hours != 0)
            {
                durationHours = duration.Hours;
            }

            durationMinutes = duration.Minutes;

            // Round up if necessary for consistency with start/end times.
            if (duration.Seconds >= 30)
                durationMinutes += 1;

            //if the rounding up of minutes takes it to 60,
            //then increment hours by 1 and set mins to zero
            if (durationMinutes == 60)
            {
                durationHours++;
                durationMinutes = 0;
            }

            if (durationHours == 1)
                durationReturn += durationHours.ToString(TDCultureInfo.CurrentCulture.NumberFormat) + hourString;
            else if (durationHours > 1)
                durationReturn += durationHours.ToString(TDCultureInfo.CurrentCulture.NumberFormat) + hoursString;

            if (durationMinutes != 0)
            {
                // if hour was not equal to 0 then add a comma
                if (durationHours != 0)
                    durationReturn += ", ";

                // Check to see if minutes requires a 0 padding.
                // Pad with 0 only if an hour was present and minute is a single digit.
                if (durationMinutes < 10 & durationHours != 0)
                    durationReturn += "0";

                durationReturn += durationMinutes.ToString(TDCultureInfo.CurrentCulture.NumberFormat);

                if (durationMinutes > 1)
                    durationReturn += minutesString;
                else
                    durationReturn += minuteString;
            }
            else if (durationHours == 0 && durationMinutes == 0)
            {
                // This leg has 0 hours 0 minutes, e.g. a journey to itself.
                // Should never really happen, but still required otherwise
                // no duration will be displayed.
                durationReturn += "0";

                if (durationMinutes > 1)
                    durationReturn += minutesString;
                else
                    durationReturn += minuteString;
            }

            return durationReturn;
        }

        /// <summary>
        /// Returns emissions for the journey in the JourneyOverviewLine
        /// </summary>
        /// <param name="jol"></param>
        /// <returns></returns>
        public string GetEmissions(JourneyOverviewLine jol)
        {
            ITDSessionManager sessionManager = TDSessionManager.Current;
            
            // Journey overview line defaults the emissions to 0, so need to calculate emissions
            JourneyEmissionsHelper emissionsHelper = new JourneyEmissionsHelper();
            string emissions = string.Format(
                GetResource("JourneyEmissionsCompareControl.Emits"),
                emissionsHelper.GetEmissions(jol.FastestJourney, 
                    sessionManager.JourneyParameters as TDJourneyParametersMulti, 
                    sessionManager.JourneyEmissionsPageState).ToString());

            return emissions;
        }

        /// <summary>
        /// Returns a formatted string of the Earliest departure date time
        /// </summary>
        /// <param name="jol"></param>
        /// <returns></returns>
        public string GetEarliestDeparture(JourneyOverviewLine jol)
        {
            if (findAMode == FindAMode.International)
            {
                foreach (ModeType mode in jol.Modes)
                {
                    if (mode == ModeType.Car) { return " - "; }
                }
            }

            return GetFormattedDateTime(jol.EarliestDeparture);
        }

        /// <summary>
        /// Returns a formatted string of the Latest departure date time
        /// </summary>
        /// <param name="jol"></param>
        /// <returns></returns>
        public string GetLatestDeparture(JourneyOverviewLine jol)
        {
            if (findAMode == FindAMode.International)
            {
                foreach (ModeType mode in jol.Modes)
                {
                    if (mode == ModeType.Car) { return " - "; }
                }
            }

            return GetFormattedDateTime(jol.LatestDeparture);
        }

        /// <summary>
        /// Returns the image to be displayed for the mode
        /// </summary>
        /// <param name="jol"></param>
        /// <returns></returns>
        public string GetModeImage(JourneyOverviewLine jol)
        {
            string mode = GetTransportMode(jol);

            return GetResource(TDResourceManager.VISIT_PLANNER_RM, "TransportModesControl.image" + mode + "URL");
        }

        /// <summary>
        /// Returns the alt text for the mode image
        /// </summary>
        /// <param name="jol"></param>
        /// <returns></returns>
        public string GetModeImageAltText(JourneyOverviewLine jol)
        {
            string mode = GetTransportMode(jol);

            return GetResource(TDResourceManager.VISIT_PLANNER_RM, "TransportModesControl.image" + mode + "AltText");
        }

        /// <summary>
        /// Rethrns the title text for the mode image
        /// </summary>
        /// <param name="jol"></param>
        /// <returns></returns>
        public string GetModeImageTitle(JourneyOverviewLine jol)
        {
            string mode = GetTransportMode(jol);

            return GetResource(TDResourceManager.VISIT_PLANNER_RM, "TransportModesControl.image" + mode + "AltText");
        }

        #endregion

        #region Row Styles

        /// <summary>
        /// Returns the Css class that the row text should be rendered with.
        /// </summary>
        /// <param name="summary">Current item being rendered.</param>
        /// <returns>Css class string.</returns>
        public string GetBodyRowCssClass(int index)
        {
            string result = string.Empty;

            // append the mode css string, always Trunk for this page
            result += "t";

            result += ((index % 2) == 0 ? "g" : string.Empty);

            // If it is the last row in the table, then want to add a bottom border.
            // Done this way because we don't want the More bottom column to have the 
            // bottom border.
            if (index == (journeyOverviewLines.Length - 1))
            {
                result += "_lr";
            }

            return result;
        }

        /// <summary>
        /// Returns the Css class that the row text should be rendered with.
        /// </summary>
        /// <returns>Css class string.</returns>
        public string GetHeaderRowCssClass()
        {
            string result = string.Empty;

            // append the mode css string
            result += "t";

            return result;
        }

        /// <summary>
        /// Select button title
        /// </summary>
        /// <returns></returns>
        public string GetSelectTitle()
        {
            return GetResource("FindSummaryResultControl.HeaderItemText8");
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Click event handler for the More button
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void buttonMore_Click(object sender, EventArgs e)
        {
            TDButton buttonMoreClicked = (TDButton)sender;

            ITDSessionManager sessionManager = TDSessionManager.Current;

            //save mode type to session FindPageState
            sessionManager.FindPageState.ModeType = GetModeTypeSelected(buttonMoreClicked.CommandName.Trim());

            //added for setting ITP journey status
            if (buttonMoreClicked.CommandName.Trim() == "Air_Coach_Rail")
                sessionManager.FindPageState.ITPJourney = true;
            else
                sessionManager.FindPageState.ITPJourney = false;

            // Set the selected journey type
            TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;

            switch (sessionManager.FindPageState.ModeType[0])
            {
                case ModeType.Car:
                    if (outward)
                        viewState.SelectedOutwardJourneyType = TDJourneyType.RoadCongested;
                    else
                        viewState.SelectedReturnJourneyType = TDJourneyType.RoadCongested;
                    break;
                default:
                    if (outward)
                        viewState.SelectedOutwardJourneyType = TDJourneyType.PublicOriginal;
                    else
                        viewState.SelectedReturnJourneyType = TDJourneyType.PublicOriginal;
                    break;
            }

            // Reset showing map/directions value
            viewState.OutwardShowMap = false;
            viewState.OutwardShowDirections = false;
            viewState.ReturnShowMap = false;
            viewState.ReturnShowDirections = false;

            // Reset showing CO2 panel, and ensure it is displayed in Diagram view when first shown
            viewState.ShowCO2 = false;

            if (sessionManager.JourneyEmissionsPageState != null)
            {
                sessionManager.JourneyEmissionsPageState.JourneyEmissionsVisualMode = JourneyEmissionsVisualMode.Diagram;
            }


            // Set the one use key to force Journey page to reselect best journey match
            TDSessionManager.Current.SetOneUseKey(SessionKey.FirstViewingOfResults, "yes");

            // Clear the stored data to ensure page reloads journeys
            sessionManager.FindPageState.StoredSummaryDataOutward = null;
            sessionManager.FindPageState.StoredSummaryDataReturn = null;

            //Reset maps
            sessionManager.JourneyMapState.Initialise();
            sessionManager.ReturnJourneyMapState.Initialise();

            //when entering the details page, set the road units to miles
            sessionManager.InputPageState.Units = RoadUnitsEnum.Miles;

            //write to the session the TransitionEvent to go to the details page
            sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoJourneyDetails;
        }

        #endregion

        #region Repeater Events
        /// <summary>
        /// Overview repeater DataBound event. Assigns click event to the select Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void overviewRepeater_DataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.SelectedItem)
            {
                JourneyOverviewLine jol = e.Item.DataItem as JourneyOverviewLine;

                TDButton tdbutton = (TDButton)e.Item.FindControl("buttonMore");
                tdbutton.Text = GetResource("FindOverviewResultControl.MoreButton");
                tdbutton.CommandName = GetTransportMode(jol);

                if (tdbutton != null)
                {
                    // Do not show the More button for car, when in international mode
                    if ((jol.Mode == ModeType.Car) && (findAMode == FindAMode.International))
                    {
                        tdbutton.Visible = false;
                    }
                    else
                    {
                        tdbutton.Click += new EventHandler(buttonMore_Click);
                    }
                }
            }
        }
        #endregion
    }
}