// *********************************************** 
// NAME                 : CycleSummaryControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 15/06/2008
// DESCRIPTION          : Control to display the summary for a Cycle journey (distance, duration, type of journey...)
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/CycleSummaryControl.ascx.cs-arc  $ 
//
//   Rev 1.6   Aug 24 2012 16:04:12   RBroddle
//Added label for display of cycle journey calorie count, and logic to populate it.
//Resolution for 5828: CCN - RFC ATO666 CYCLE CALORIE COUNTER
//
//   Rev 1.5   Oct 10 2008 15:41:46   mmodi
//Added cycle avoid value
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   Oct 07 2008 16:24:30   mmodi
//Display speed in kph
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//Resolution for 5133: Cycle Planner - Cycling speed is not displayed in 'kmph', when the unit is changed to 'km'
//
//   Rev 1.3   Sep 15 2008 10:54:24   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Aug 22 2008 10:27:12   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Jul 28 2008 13:04:34   mmodi
//Updates
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jul 18 2008 13:27:02   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

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

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.SessionManager;
using System.Text;


namespace TransportDirect.UserPortal.Web.Controls
{
    public partial class CycleSummaryControl : TDUserControl
    {
        #region Private members

        //Populator to load the strings for the DropDown list
        private TransportDirect.UserPortal.DataServices.DataServices populator;
        private bool printable = false;        
        private bool outward = false;
        private RoadUnitsEnum roadUnits;
        private CycleJourney cycleJourney = null;
        private TDJourneyParametersMulti journeyParameters = null;

        #endregion

        #region Initialise
        /// <summary>
        /// Initialises this control with a specific cycle journey
        /// </summary>
        public void Initialise(CycleJourney cycleJourney, bool outward, TDJourneyParametersMulti journeyParameters)
        {
            this.outward = outward;
            this.cycleJourney = cycleJourney;
            this.journeyParameters = journeyParameters;
        }

        #endregion

        #region Page_Load, Page_PreRender
        /// <summary>
        /// Page_Load
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            SetUpControls();

            AlignServerWithClient();
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            // Ensures controls on ascx are evaluated, i.e. the <# #> 
            this.DataBind();

            //Only want to do this if not on the nonprintable page.
            if (!printable)
            {
                EnableScriptableObjects();

                string PageName = this.PageId.ToString();
                dropdownlistCycleSummary.Action = "ChangeUnits('" + GetHiddenInputId + "', '" + PageName + "', this)";
            }

            LoadResources();

            LoadSummary();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the text and lists on the control
        /// </summary>
        private void LoadResources()
        {
            labelSummaryOfDirections.Text = GetResource("CyclePlanner.CycleSummaryControl.labelSummaryOfDirections.Text");
            labelTotalDistance.Text = GetResource("CyclePlanner.CycleSummaryControl.labelTotalDistance.Text");
            labelTotalDuration.Text = GetResource("CyclePlanner.CycleSummaryControl.labelTotalDuration.Text");
            labelDistanceUnits.Text = GetResource("CyclePlanner.CycleSummaryControl.labelDistanceUnits.Text");
        }

        /// <summary>
        /// Loads the lists on the control
        /// </summary>
        private void SetUpControls()
        {
            //Populates the drop down list control with the allowed values from DataServices
            populator = (TransportDirect.UserPortal.DataServices.DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

            // Load lists
            // retain index when populating dropdown
            int index = dropdownlistCycleSummary.SelectedIndex;

            populator.LoadListControl(DataServiceType.UnitsDrop, dropdownlistCycleSummary, Global.tdResourceManager);

            dropdownlistCycleSummary.SelectedIndex = index;
        }

        /// <summary>
        /// Loads the cycle journey summary values
        /// </summary>
        private void LoadSummary()
        {
            #region distance
            
            string miles = GetResource("CarSummaryControl.labelMilesString");
            
            string distanceInMiles = ConvertMetresToMileage(cycleJourney.TotalDistance) + " " + miles;
            string distanceInKm = ConvertMetersToKm(cycleJourney.TotalDistance) + " " + "km";

            //switches the total distance between miles and kms
            if (roadUnits == RoadUnitsEnum.Miles)
            {
                labelTotalDistanceNumber.Text = 
                        "<span class=\"milesshow\">" + distanceInMiles + "</span>"
                      + "<span class=\"kmshide\">" + distanceInKm + "</span>";
            }
            else
            {
                labelTotalDistanceNumber.Text = 
                        "<span class=\"mileshide\">" + distanceInMiles + "</span>"
                      + "<span class=\"kmsshow\">" + distanceInKm + "</span>";
            }

            #endregion

            #region duration
            
            string hoursString = GetResource("CarSummaryControl.labelHourString");
            string minutesString = GetResource("CarSummaryControl.labelMinuteString");

            double totalDuration = (double)cycleJourney.TotalDuration / 60.0;
            totalDuration = Round(totalDuration);
            long hours = (long)totalDuration / 60;
            long minutes = (long)totalDuration % 60;

            labelTotalDurationNumber.Text = hours + " " + hoursString + " " + minutes + " " + minutesString;

            #endregion

            #region options

            if (journeyParameters != null)
            {
                StringBuilder journeyOptions = new StringBuilder();

                #region Type of journey

                journeyOptions.Append(populator.GetText(DataServiceType.CycleJourneyType, journeyParameters.CycleJourneyType));
                journeyOptions.Append(" ");
                journeyOptions.Append(GetResource("CyclePlanner.CycleSummaryControl.labelJourneyOptions.Journey"));
                journeyOptions.Append(". ");

                #endregion

                #region MaxSpeed

                decimal speedInMiles = Convert.ToDecimal(MeasurementConversion.Convert(Convert.ToDouble(journeyParameters.CycleSpeedMax) / 1000, ConversionType.KilometresToMiles));;
                decimal speedInKm = Convert.ToDecimal(journeyParameters.CycleSpeedMax) / 1000;

                string speedInMilesText = Decimal.Round(speedInMiles, 0) + "mph";
                string speedInKmText = Decimal.Round(speedInKm, 0) + "kph";

                string speedDisplayText;
                //switches the total distance between miles and kms
                if (roadUnits == RoadUnitsEnum.Miles)
                {
                    speedDisplayText =
                        "<span class=\"milesshow\">" + speedInMilesText + "</span>"
                      + "<span class=\"kmshide\">" + speedInKmText + "</span>";
                }
                else
                {
                    speedDisplayText =
                        "<span class=\"mileshide\">" + speedInMilesText + "</span>"
                      + "<span class=\"kmsshow\">" + speedInKmText + "</span>";
                }

                StringBuilder maxSpeed = new StringBuilder();
                maxSpeed.Append(GetResource("CyclePlanner.CycleSummaryControl.labelJourneyOptions.Speed"));
                maxSpeed.Append(" ");
                maxSpeed.Append(speedDisplayText);
                maxSpeed.Append(". ");

                journeyOptions.Append(maxSpeed.ToString());

                #endregion

                #region Avoid

                if ((journeyParameters.CycleAvoidSteepClimbs) || (journeyParameters.CycleAvoidUnlitRoads)
                    || (journeyParameters.CycleAvoidWalkingYourBike) || (journeyParameters.CycleAvoidTimeBased))
                {
                    string avoidSteepClimbs = GetResource("CyclePlanner.FindCyclePreferencesControl.checkboxAvoidSteepClimbs");
                    string avoidUnlitRoads = GetResource("CyclePlanner.FindCyclePreferencesControl.checkboxAvoidUnlitRoads");
                    string avoidWalkingYourBike = GetResource("CyclePlanner.FindCyclePreferencesControl.checkboxAvoidWalkingYourBike");
                    string avoidTimeBased = GetResource("CyclePlanner.FindCyclePreferencesControl.checkboxAvoidTimeBased");

                    string avoid = GetResource("CyclePlanner.CycleSummaryControl.labelJourneyOptions.Avoid") + " ";

                    if (journeyParameters.CycleAvoidSteepClimbs)
                    {
                        avoid += " " + avoidSteepClimbs + ",";
                    }

                    if (journeyParameters.CycleAvoidUnlitRoads)
                    {
                        avoid += " " + avoidUnlitRoads + ",";
                    }

                    if (journeyParameters.CycleAvoidWalkingYourBike)
                    {
                        avoid += " " + avoidWalkingYourBike + ",";
                    }

                    if (journeyParameters.CycleAvoidTimeBased)
                    {
                        avoid += " " + avoidTimeBased + ",";
                    }

                    // strip off last comma
                    avoid = avoid.TrimEnd(',');
                    // Replace the last comma with an "and"
                    int replace = avoid.LastIndexOf(',');
                    if (replace > 0)
                    {
                        avoid = avoid.Remove(replace, 1);
                        avoid = avoid.Insert(replace, " " + GetResource("CyclePlanner.CycleSummaryControl.labelJourneyOptions.AndLowerCase"));
                    }

                    avoid += ".";

                    journeyOptions.Append(avoid);
                    journeyOptions.Append(" ");
                }
                #endregion

                labelJourneyOptions.Text = journeyOptions.ToString();

                #region Display Addtional Options

                // Only display the additional option values if user is logged on as a CJP user
                if (IsCJPUser())
                {
                    divJourneyAdditionalOptions.Visible = true;

                    StringBuilder journeyAddtionalOptions = new StringBuilder();

                    journeyAddtionalOptions.Append(GetResource("CyclePlanner.CycleSummaryControl.labelJourneyAdvancedOptions.PenaltyFunction"));
                    journeyAddtionalOptions.Append(" ");
                    journeyAddtionalOptions.Append(journeyParameters.CyclePenaltyFunction);

                    labelJourneyAdditionalOptions.Text = journeyAddtionalOptions.ToString();
                }
                
                #endregion

                #region Display Calorie Count

                //Only display calorie count if switched on and >= zero
                if (Properties.Current["CyclePlanner.IncludeCalorieCount"] != null &&
                    bool.Parse(Properties.Current["CyclePlanner.IncludeCalorieCount"]) && cycleJourney.CalorieCount >= 0)
                {
                    labelCalorieCount.Visible = true;
                    string calCountText = GetResource("CyclePlanner.CycleSummaryControl.labelCalorieCount.Text");
                    labelCalorieCount.Text = string.Format(calCountText, cycleJourney.CalorieCount);
                }
                else
                {
                    labelCalorieCount.Visible = false;
                }

                #endregion
            }

            #endregion
        }

        #region Calculation helper methods

        /// <summary>
        /// Return a formatted string from converting the given metres
        /// to a mileage (only 1 decimal place will be returned in the string).
        /// </summary>
        /// <param name="metres">Metres to convert.</param>
        /// <returns>Milage string</returns>
        private string ConvertMetresToMileage(int metres)
        {
            // Retrieve the conversion factor from the Properties Service.
            double conversionFactor =
                Convert.ToDouble(Properties.Current["Web.Controls.MileageConverter"], TDCultureInfo.CurrentCulture.NumberFormat);

            double result = (double)metres / conversionFactor;

            // Only want the string to have 1 decimal place - chop off everything
            // after that.

            return result.ToString("F1", TDCultureInfo.CurrentCulture.NumberFormat);
        }

        /// <summary>
        /// Return a formatted string from converting the given metres
        /// to a km (only 1 decimal place will be returned in the string).
        /// </summary>
        /// <param name="metres"></param>
        /// <returns></returns>
        private string ConvertMetersToKm(int metres)
        {
            double result = (double)metres / 1000;

            // Return the result
            return result.ToString("F1", TDCultureInfo.CurrentUICulture.NumberFormat);
        }

        /// <summary>
        /// Rounds the given double to the nearest int.
        /// If double is 0.5, then rounds up.
        /// Using this instead of Math.Round because Math.Round
        /// ALWAYS returns the even number when rounding a .5 -
        /// this is not behaviour we want.
        /// </summary>
        /// <param name="valueToRound">Value to round.</param>
        /// <returns>Nearest integer</returns>
        private static int Round(double valueToRound)
        {
            // Get the decimal point
            double valueFloored = Math.Floor(valueToRound);
            double remain = valueToRound - valueFloored;

            if (remain >= 0.5)
                return (int)Math.Ceiling(valueToRound);
            else
                return (int)Math.Floor(valueToRound);
        }

        #endregion

        #region Javascript

        ///<summary>
        /// The EnableClientScript property of a scriptable control is set so that they
        /// output an action attribute when appropriate.
        /// If JavaScript is enabled then appropriate script blocks are added to the page.
        ///</summary>
        protected void EnableScriptableObjects()
        {
            bool javaScriptSupported = bool.Parse((string)Session[((TDPage)Page).Javascript_Support]);
            string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];

            if (javaScriptSupported)
            {
                dropdownlistCycleSummary.Visible = true;
                labelDistanceUnits.Visible = true;
                ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
                dropdownlistCycleSummary.EnableClientScript = true;
                Page.ClientScript.RegisterStartupScript(typeof(CycleSummaryControl), dropdownlistCycleSummary.ScriptName, scriptRepository.GetScript(dropdownlistCycleSummary.ScriptName, javaScriptDom));
            }
            else
            {
                dropdownlistCycleSummary.Visible = false;
                labelDistanceUnits.Visible = false;
            }
        }

        ///<summary>
        /// The client may have changed things through JavaScript so need to update server state.  Used to set the units for cycle journeys.
        ///</summary>
        private void AlignServerWithClient()
        {
            string HiddenUnitsField = "";
            if (this.ClientID != null && this.ClientID != "cycleAllDetailsControlReturn_cycleSummaryControl")
            {
                HiddenUnitsField = this.ClientID;
            }
            else
            {
                HiddenUnitsField = "cycleAllDetailsControlReturn_cycleSummaryControl";
            }

            if (Request.Params[HiddenUnitsField + "_hdnUnitsState"] != null)
            {
                roadUnits = (RoadUnitsEnum)Enum.Parse(typeof(RoadUnitsEnum), Request.Params[HiddenUnitsField + "_hdnUnitsState"], true);
            }
            else
            {
                if (HiddenUnitsField == "cycleAllDetailsControlReturn_cycleSummaryControl")
                {
                    if (Request.Params["cycleAllDetailsControlOutward_cycleSummaryControl_hdnUnitsState"] != null)
                    {
                        roadUnits = (RoadUnitsEnum)Enum.Parse(typeof(RoadUnitsEnum), Request.Params["cycleAllDetailsControlOutward_cycleSummaryControl_hdnUnitsState"], true);
                    }
                }
                else
                {
                    if (Request.Params["cycleAllDetailsControlReturn_cycleSummaryControl_hdnUnitsState"] != null)
                    {
                        roadUnits = (RoadUnitsEnum)Enum.Parse(typeof(RoadUnitsEnum), Request.Params["cycleAllDetailsControlReturn_cycleSummaryControl_hdnUnitsState"], true);
                    }
                }
            }

            // Save to server
            TDSessionManager.Current.InputPageState.Units = roadUnits;

            if (roadUnits == RoadUnitsEnum.Miles)
            {
                dropdownlistCycleSummary.SelectedIndex = 0;
            }
            else
            {
                dropdownlistCycleSummary.SelectedIndex = 1;
            }
        }


        /////<summary>
        ///// The client and server need to be kept in sync. Used in postbacks to set the units for road journeys
        /////</summary>
        //private void AlignClientWithServer()
        //{
        //    RoadUnitsEnum serverUnits = roadUnits;

        //    if (serverUnits == RoadUnitsEnum.Kms)
        //    {
        //        TDSessionManager.Current.InputPageState.Units = serverUnits;

        //    }
        //}

        #endregion

        /// <summary>
        /// Method which returns true if user is a higher-level (e.g. CJP) user 
        /// </summary>
        private bool IsCJPUser()
        {
            bool userIsLoggedOn = TDSessionManager.Current.Authenticated;

            // Get the user's type
            int userType = userIsLoggedOn ? (int)TDSessionManager.Current.CurrentUser.UserType : (int)TDUserType.Standard;

            return (userType > 0);
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/write. Printable mode of control
        /// </summary>
        public bool Printable
        {
            get { return printable; }
            set { printable = value; }
        }

        #endregion

        #region Public control properties

        /// <summary>
        /// Returns the id of the hidden control
        /// </summary>
        public string GetHiddenInputId
        {
            get
            {
                return this.ClientID + "_hdnUnitsState";
            }
        }

        /// <summary>
        /// Returns the current unit value in the session
        /// </summary>
        public string UnitsState
        {
            get
            {
                return TDSessionManager.Current.InputPageState.Units.ToString();
            }
        }

        /// <summary>
        /// Road units, Miles/Km
        /// </summary>
        public RoadUnitsEnum RoadUnits
        {
            get { return roadUnits; }
            set { roadUnits = value; }
        }

        #endregion
    }
}