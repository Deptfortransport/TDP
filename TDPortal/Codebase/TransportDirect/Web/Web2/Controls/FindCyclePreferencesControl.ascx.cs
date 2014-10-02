// *********************************************** 
// NAME                 : FindCyclePreferencesControl.ascx.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 06 Jun 2008
// DESCRIPTION          : Displays input of cycle specific preferences   
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindCyclePreferencesControl.ascx.cs-arc  $ 
//
//   Rev 1.9   Aug 28 2012 10:21:08   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.8   Oct 20 2008 11:10:40   mmodi
//Updated to allow override location coordinates to be used for cycle journey
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.7   Oct 10 2008 15:53:26   mmodi
//Updated to have avoid time based check
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.6   Oct 07 2008 15:45:52   mmodi
//Updated validation of preferences
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//Resolution for 5129: Cycle Planner - There is no limit on Cycling Speed entered by user on 'Cycle Journey Options' page
//
//   Rev 1.5   Sep 09 2008 13:17:46   mmodi
//Updated to load lists when setting dropdown value
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   Sep 02 2008 10:54:12   mmodi
//Updated for PenaltyFunction
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   Aug 22 2008 10:32:50   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Jul 28 2008 13:09:56   mmodi
//Updates
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Jul 18 2008 14:03:34   mmodi
//Updates for cycle planner
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 15:25:36   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Web.Support;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;


namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Preferences for Cycle journeys
    /// </summary>
    public partial class FindCyclePreferencesControl : TDUserControl
    {
        #region Private variables

        private string PENALTYFUNCTION_LOCATION = "CyclePlanner.PlannerControl.PenaltyFunction.Location";
        private string PENALTYFUNCTION_PREFIX = "CyclePlanner.PlannerControl.PenaltyFunction.Prefix";

        private IDataServices populator;

        private GenericDisplayMode speedDisplayMode = GenericDisplayMode.Normal;
        private GenericDisplayMode avoidListDisplayMode = GenericDisplayMode.Normal;
        private GenericDisplayMode viaLocationDisplayMode = GenericDisplayMode.Normal;
        private GenericDisplayMode penaltyFunctionMode = GenericDisplayMode.Normal;
        private GenericDisplayMode locationOverrideMode = GenericDisplayMode.Normal;

        private bool validSpeedText = true;
        private bool validLocationOriginOverride = true;
        private bool validLocationDestinationOverride = true;
        private bool newLocationClicked;
        
        /// Event fired to signal new location button has been clicked
        /// </summary>
        public event EventHandler NewLocation;

        #endregion

        #region Event keys

        private static readonly object PreferencesVisibleChangedEventKey = new object();
        private static readonly object OnSpeedTextChangedKey = new object();

        #endregion

        #region page init, load, render
        /// <summary>
		/// Handler for the Init event. Sets up global variables and additional event handlers.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        protected void Page_Init(object sender, System.EventArgs e)
        {
            populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

            textSpeed.TextChanged += new EventHandler(this.OnSpeedTextChanged);

            pageOptionsControl.ShowAdvancedOptions += new EventHandler(this.OnShowPreferences);
            pageOptionsControl.HideAdvancedOptions += new EventHandler(this.OnHidePreferences);

            locationControl.NewLocationClick += new EventHandler(OnNewLocation);
        }

        /// <summary>
        /// Page_Load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadLists();

            LoadResources();
        }

        /// <summary>
		/// Sets visibility of controls according to the property values
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            UpdateControls();
            UpdateAdditionalPreferences();
            UpdatePreferencesControl(); // This sets the visibility and display mode of the preferences

            // Set state of the preferences control
            if (TDSessionManager.Current.Authenticated)
            {
                loginSaveOption.LoggedInDisplay();
            }
            else
            {
                loginSaveOption.LoggedOutDisplay();
            }

            pageOptionsControl.AllowBack = false;
            pageOptionsControl.AllowShowAdvancedOptions = !AmbiguityMode && !PreferencesVisible;
            pageOptionsControl.AllowHideAdvancedOptions = !AmbiguityMode && PreferencesVisible;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the lists
        /// </summary>
        private void LoadLists()
        {          
            int listSpeedUnitIndex = listSpeedUnit.SelectedIndex;

            populator.LoadListControl(DataServiceType.UnitsSpeedDrop, listSpeedUnit);

            listSpeedUnit.SelectedIndex = listSpeedUnitIndex;
        }

        /// <summary>
        /// Loads the text labels
        /// </summary>
        private void LoadResources()
        {
            labelCycleJourneyOptions.Text = GetResource("CyclePlanner.FindCyclePreferencesControl.labelCycleJourneyOptions");

            labelIPrefer.Text = GetResource("CyclePlanner.FindCyclePreferencesControl.labelIPrefer");
            labelSpeedMax.Text = GetResource("CyclePlanner.FindCyclePreferencesControl.labelSpeedMax");
            labelTravelVia.Text = GetResource("CyclePlanner.FindCyclePreferencesControl.labelTravelVia");

            checkboxAvoidSteepClimbs.Text = GetResource("CyclePlanner.FindCyclePreferencesControl.checkboxAvoidSteepClimbs");
            checkboxAvoidUnlitRoads.Text = GetResource("CyclePlanner.FindCyclePreferencesControl.checkboxAvoidUnlitRoads");
            checkboxAvoidWalkingYourBike.Text = GetResource("CyclePlanner.FindCyclePreferencesControl.checkboxAvoidWalkingYourBike");
            checkboxAvoidTimeBased.Text = GetResource("CyclePlanner.FindCyclePreferencesControl.checkboxAvoidTimeBased");

            textSpeed.CssClass = string.Empty;
        }

        /// <summary>
        /// Method which loads and updates the visibility of the controls specific to higher-level (CJP)
        /// logged on users
        /// </summary>
        private void UpdateAdditionalPreferences()
        {
            if (IsCJPUser())
            {
                // Show the additional preferences
                divAdditionalPreferences.Visible = true;

                // load the controls
                labelPenaltyFunctionOverride.Text = GetResource("CyclePlanner.FindCyclePreferencesControl.labelPenaltyFunctionOverride");
                labelPenaltyFunctionOverrideHelp.Text = GetResource("CyclePlanner.FindCyclePreferencesControl.labelPenaltyFunctionOverrideHelp");
                //labelPenaltyFunctionOverrideCall.Text = GetResource("CyclePlanner.FindCyclePreferencesControl.labelPenaltyFunctionOverrideCall");
                labelPenaltyFunctionOverrideCall.Text = "e.g. Call " 
                        + Properties.Current[PENALTYFUNCTION_LOCATION] + ", "
                        + Properties.Current[PENALTYFUNCTION_PREFIX] + ".Fastest";

                // load the location override fields
                labelLocationOverride.Text = "Location Coordinates override";
                labelOriginOverride.Text = "Origin";
                labelDestinationOverride.Text = "Destination";
                labelOriginEasting.Text = "E";
                labelOriginNorthing.Text = "N";
                labelDestinationEasting.Text = "E";
                labelDestinationNorthing.Text = "N";
            }
            else
            {
                divAdditionalPreferences.Visible = false;
            }
        }

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

        /// <summary>
        /// Updates the state of nested controls with this object's property values
        /// </summary>
        private void UpdateControls()
        {
            // Don't show the via location control if in ambiguous mode and 
            // no location has been entered
            // However if in ambiguous mode and user clicks new location, this location will be empty and
            // we do need to make the control visible so use newLocationClicked to determine this case.
            // Don't do this if the control is called from door to door ambiguity page because this is handled
            // in the ambiguity page.
            if (PageId != PageId.JourneyPlannerAmbiguity)
            {
                if (AmbiguityMode && locationControl.Search.InputText.Length == 0 && !newLocationClicked)
                    locationControl.Visible = false;
                else
                    locationControl.Visible = true;
            }
        }

        /// <summary>
		/// Sets the state of the preferences controls.  
        /// NB: the display label text should be 
		/// set from the container page (requires journey parameter values entered by user)
		/// </summary>
        private void UpdatePreferencesControl()
        {
            bool showCycleDetailsPanel = false;
            bool showCycleSpeed = !CycleSpeedIsDefault();
            bool showAvoidList = !AvoidListIsDefault();
            bool showViaLocation = !ViaLocationIsDefault();
            bool showPenaltyFunction = !PenaltyFunctionIsDefault();
            bool showLocationOverride = !LocationOverrideIsDefault();
            
            #region Speed
            switch (speedDisplayMode)
            {
                case GenericDisplayMode.ReadOnly:
                case GenericDisplayMode.Ambiguity:

                    ValidateSpeed();

                    if (!SpeedTextValid)
                    {
                        textSpeed.CssClass = "alertboxerror";
                        displaySpeedErrorLabel.Visible = true;

                        int speedMaxMetresPerHour = Convert.ToInt32(Properties.Current["CyclePlanner.Planner.CyclingMaxSpeed.MetresPerHour"]);
                        int speedMinMetresPerHour = Convert.ToInt32(Properties.Current["CyclePlanner.Planner.CyclingMinSpeed.MetresPerHour"]);

                        double speedMinMPH = Convert.ToDouble(MeasurementConversion.Convert(speedMinMetresPerHour, ConversionType.MetresToMileage));
                        double speedMinKPH = speedMinMetresPerHour * 0.001;
                        double speedMaxMPH = Convert.ToDouble(MeasurementConversion.Convert(speedMaxMetresPerHour, ConversionType.MetresToMileage));
                        double speedMaxKPH = speedMaxMetresPerHour * 0.001;

                        if (listSpeedUnit.SelectedIndex == 0)
                        {
                            displaySpeedErrorLabel.Text = string.Format(
                                    GetResource("CyclePlanner.FindCyclePreferencesControl.SpeedError"),
                                    speedMinMPH.ToString("F0", TDCultureInfo.CurrentCulture.NumberFormat) + "mph",
                                    speedMaxMPH.ToString("F0", TDCultureInfo.CurrentCulture.NumberFormat) + "mph" );
                        }
                        else
                        {
                            displaySpeedErrorLabel.Text = string.Format(
                                    GetResource("CyclePlanner.FindCyclePreferencesControl.SpeedError"),
                                    speedMinKPH.ToString("F0", TDCultureInfo.CurrentCulture.NumberFormat) + "kph",
                                    speedMaxKPH.ToString("F0", TDCultureInfo.CurrentCulture.NumberFormat) + "kph");
                        }

                        displaySpeedErrorLabel.Text += "<br />";

                        showCycleDetailsPanel = true;
                    }
                    else
                    {
                        labelSpeedMax.Visible = false;
                        textSpeed.Visible = false;
                        listSpeedUnit.Visible = false;

                        if (this.PageId == PageId.FindCycleInput)
                        {
                            displaySpeedDetailsLabel.Visible = showCycleSpeed;

                            if (showCycleSpeed)
                            {
                                displaySpeedDetailsLabel.Text = GetResource("CyclePlanner.FindCyclePreferencesControl.labelSpeedMax") + " " 
                                    + SpeedText + " "
                                    + populator.GetText(DataServiceType.UnitsSpeedDrop, SpeedUnit);

                                showCycleDetailsPanel = true;
                            }
                        }
                    }

                    divSpeed.Visible = showCycleSpeed;
                    
                    break;
                case GenericDisplayMode.Normal:
                default:
                    labelSpeedMax.Visible = true;
                    textSpeed.Visible = true;
                    listSpeedUnit.Visible = true;
                    showCycleDetailsPanel = true;

                    divSpeed.Visible = true;

                    break;
            }
            #endregion

            #region Avoid List

            switch (avoidListDisplayMode)
            {
                case GenericDisplayMode.ReadOnly:
                case GenericDisplayMode.Ambiguity:

                    displayIPreferDetailsLabel.Visible = showAvoidList;
                    if (showAvoidList)
                    {
                        StringBuilder avoidText = new StringBuilder();
                        avoidText.Append(GetResource("CyclePlanner.FindCyclePreferencesControl.labelIPrefer"));
                        avoidText.Append(" ");
                        avoidText.Append((checkboxAvoidUnlitRoads.Checked ? checkboxAvoidUnlitRoads.Text + ", " : string.Empty));
                        avoidText.Append((checkboxAvoidWalkingYourBike.Checked ? checkboxAvoidWalkingYourBike.Text + ", " : string.Empty));
                        avoidText.Append((checkboxAvoidSteepClimbs.Checked ? checkboxAvoidSteepClimbs.Text + ", " : string.Empty));
                        avoidText.Append((checkboxAvoidTimeBased.Checked ? checkboxAvoidTimeBased.Text + ", " : string.Empty));

                        displayIPreferDetailsLabel.Text = avoidText.ToString().Trim().TrimEnd(new char[]{','});

                        divIPreferTitle.Visible = false;
                        divIPreferCheckBoxes.Visible = false;

                        showCycleDetailsPanel = true;
                    }

                    labelIPrefer.Visible = false;
                    checkboxAvoidSteepClimbs.Visible = false;
                    checkboxAvoidUnlitRoads.Visible = false;
                    checkboxAvoidWalkingYourBike.Visible = false;
                    checkboxAvoidTimeBased.Visible = false;

                    divIPrefer.Visible = showAvoidList;

                    break;

                case GenericDisplayMode.Normal:
                default:
                    labelIPrefer.Visible = true;
                    checkboxAvoidSteepClimbs.Visible = true;
                    checkboxAvoidUnlitRoads.Visible = true;
                    checkboxAvoidWalkingYourBike.Visible = true;
                    checkboxAvoidTimeBased.Visible = true;
                    displayIPreferDetailsLabel.Visible = false;
                    divIPrefer.Visible = true;
                    divIPreferTitle.Visible = true;
                    divIPreferCheckBoxes.Visible = true;
                    showCycleDetailsPanel = true;
                    break;
            }
            #endregion

            #region ViaLocation

            switch (viaLocationDisplayMode)
            {
                case GenericDisplayMode.ReadOnly:
                case GenericDisplayMode.Ambiguity:

                    // If user has entered a location, and its ambiguous then want to continue displaying
                    // via location input
                    if ((locationControl.Search != null 
                        && locationControl.Search.InputText.Length > 0 )
                        && (locationControl.Location.Status == TDLocationStatus.Unspecified || 
                            locationControl.Location.Status == TDLocationStatus.Ambiguous))
                    {
                        labelTravelVia.Visible = true;
                        viaLocationDisplayMode = GenericDisplayMode.Normal;
                        showCycleDetailsPanel = true;
                    }
                    else
                    {   // via is valid and/or not displayed
                        if (locationControl.Location.Status == TDLocationStatus.Valid)
                        {
                            showCycleDetailsPanel = true;
                            labelTravelVia.Visible = true;
                            labelTravelVia.CssClass = "txtsevenb"; // Make it display in bold
                        }
                        else
                        {
                            labelTravelVia.Visible = false;
                        }
                    }
                                        
                    divViaLocation.Visible = showViaLocation;
                    break;

                case GenericDisplayMode.Normal:
                default:
                    labelTravelVia.Visible = true;
                    divViaLocation.Visible = true;
                    showCycleDetailsPanel = true;
                    break;
            }


            #endregion

            #region Penalty Function, Location override

            if (IsCJPUser())
            {
                switch (penaltyFunctionMode)
                {
                    case GenericDisplayMode.ReadOnly:
                    case GenericDisplayMode.Ambiguity:

                        labelPenaltyFunctionOverride.Visible = false;                        
                            labelPenaltyFunctionOverrideHelp.Visible = false;
                            labelPenaltyFunctionOverrideCall.Visible = false;
                            textboxPenaltyFunctionOverride.Visible = false;

                            if (this.PageId == PageId.FindCycleInput)
                            {
                                displayPenaltyFunction.Visible = showPenaltyFunction;

                                if (showPenaltyFunction)
                                {

                                    displayPenaltyFunction.Text = GetResource("CyclePlanner.FindCyclePreferencesControl.labelPenaltyFunctionOverride") + ": "
                                        + PenaltyFunctionOverride;

                                    showCycleDetailsPanel = true;
                                }
                            }

                            divAdditionalPreferences.Visible = showPenaltyFunction;

                        break;
                    case GenericDisplayMode.Normal:
                    default:
                        labelPenaltyFunctionOverride.Visible = true;
                        labelPenaltyFunctionOverrideHelp.Visible = true;
                        labelPenaltyFunctionOverrideCall.Visible = true;
                        textboxPenaltyFunctionOverride.Visible = true;
                        displayPenaltyFunction.Visible = false;

                        showCycleDetailsPanel = true;

                        divAdditionalPreferences.Visible = true;

                        break;
                }

                switch (locationOverrideMode)
                {
                    case GenericDisplayMode.ReadOnly:
                    case GenericDisplayMode.Ambiguity:

                        ValidateLocationOverride();

                        if (!validLocationOriginOverride || !validLocationDestinationOverride)
                        {
                            if (!validLocationOriginOverride)
                            {
                                textOriginEasting.CssClass = "alertboxerror";
                                textOriginNorthing.CssClass = "alertboxerror";
                            }

                            if (!validLocationDestinationOverride)
                            {
                                textDestinationEasting.CssClass = "alertboxerror";
                                textDestinationNorthing.CssClass = "alertboxerror";
                            }

                            
                            showCycleDetailsPanel = true;
                        }
                        else
                        {

                            labelOriginEasting.Visible = false;
                            labelOriginNorthing.Visible = false;
                            labelDestinationEasting.Visible = false;
                            labelDestinationNorthing.Visible = false;
                            textOriginEasting.Visible = false;
                            textOriginNorthing.Visible = false;
                            textDestinationEasting.Visible = false;
                            textDestinationNorthing.Visible = false;

                            if (this.PageId == PageId.FindCycleInput)
                            {
                                labelOriginOverride.Visible = showLocationOverride;
                                labelDestinationOverride.Visible = showLocationOverride;

                                if (showLocationOverride)
                                {
                                    if (LocationOriginOverride.IsValid)
                                    {
                                        labelOriginOverride.Text = "Origin " + LocationOriginOverride.Easting.ToString() + "," + LocationOriginOverride.Northing.ToString();
                                    }
                                    else
                                    {
                                        labelOriginOverride.Visible = false;
                                    }

                                    if (LocationDestinationOverride.IsValid)
                                    {
                                        labelDestinationOverride.Text = "Destination " + LocationDestinationOverride.Easting.ToString() + "," + LocationDestinationOverride.Northing.ToString();
                                    }
                                    else
                                    {
                                        labelDestinationOverride.Visible = false;
                                    }

                                    showCycleDetailsPanel = true;
                                }
                            }
                        }

                        divAdditionalPreferences.Visible = showLocationOverride;

                        break;
                    case GenericDisplayMode.Normal:
                    default:
                        labelOriginOverride.Visible = true;
                        labelDestinationOverride.Visible = true;
                        labelOriginEasting.Visible = true;
                        labelOriginNorthing.Visible = true;
                        labelDestinationEasting.Visible = true;
                        labelDestinationNorthing.Visible = true;
                        textOriginEasting.Visible = true;
                        textOriginNorthing.Visible = true;
                        textDestinationEasting.Visible = true;
                        textDestinationNorthing.Visible = true;

                        textOriginEasting.CssClass = "txtseven";
                        textOriginNorthing.CssClass = "txtseven";
                        textDestinationEasting.CssClass = "txtseven";
                        textDestinationNorthing.CssClass = "txtseven";

                        showCycleDetailsPanel = true;

                        divAdditionalPreferences.Visible = true;

                        break;
                }
            }

            #endregion

            panelCycleDetails.Visible = showCycleDetailsPanel;

            // Hide the login message when in ambiguity mode
            if ((showCycleDetailsPanel) && (AmbiguityMode))
                loginSaveOption.Visible = false;
            else
                loginSaveOption.Visible = true;

        }

        #region Validation helpers

        /// <summary>
        /// Validates the speed text to ensure it is between the min and max
        /// </summary>
        private void ValidateSpeed()
        {
            if (!CycleSpeedIsDefault())
            {
                int speedMaxMetresPerHour = Convert.ToInt32(Properties.Current["CyclePlanner.Planner.CyclingMaxSpeed.MetresPerHour"]);
                int speedMinMetresPerHour = Convert.ToInt32(Properties.Current["CyclePlanner.Planner.CyclingMinSpeed.MetresPerHour"]);

                // Get the speed text entered
                double speedEntered = Convert.ToDouble(SpeedMetresPerHour);

                if ((speedEntered <= speedMinMetresPerHour) || (speedEntered >= speedMaxMetresPerHour))
                {
                    validSpeedText = false;
                }
                else
                {
                    validSpeedText = true;
                }
            }
            else
            {
                // speed is valid if not entered by user, as it is the default
                validSpeedText = true;
            }
        }

        /// <summary>
        /// Validates the location coordinates entered
        /// </summary>
        private void ValidateLocationOverride()
        {
            validLocationOriginOverride = true;
            validLocationDestinationOverride = true;

            if (!LocationOverrideIsDefault())
            {
                OSGridReference osgr = LocationOriginOverride;
                
                if ((!osgr.IsValid) && 
                    (!string.IsNullOrEmpty(textOriginEasting.Text) || !string.IsNullOrEmpty(textOriginNorthing.Text)) )
                    validLocationOriginOverride = false;

                osgr = LocationDestinationOverride;
                if ((!osgr.IsValid) &&
                    (!string.IsNullOrEmpty(textDestinationEasting.Text) || !string.IsNullOrEmpty(textDestinationNorthing.Text)))
                    validLocationDestinationOverride = false;
            }
        }

        #endregion

        /// <summary>
        /// Returns true if the entered cycle speed is the default value, false otherwise
        /// </summary>
        /// <returns>true if no cycle speed is entered, false otherwise</returns>
        private bool CycleSpeedIsDefault()
        {
            return SpeedText == string.Empty;
        }

        /// <summary>
        /// Returns true if the user has not selected any options to avoid, false otherwise (which
        /// indicates user has selected an avoid option)
        /// </summary>
        /// <returns></returns>
        private bool AvoidListIsDefault()
        {
            return (!checkboxAvoidSteepClimbs.Checked) && (!checkboxAvoidUnlitRoads.Checked) 
                && (!checkboxAvoidWalkingYourBike.Checked) && (!checkboxAvoidTimeBased.Checked);
        }

        /// <summary>
        /// Returns true if user has not entered/selected a via location, false otherwise
        /// </summary>
        /// <returns></returns>
        private bool ViaLocationIsDefault()
        {
            // if the user has selected new location, then we want to force display of via location input
            if (newLocationClicked)
                return false;
            else
            {
                return (locationControl.Search != null && locationControl.Search.InputText.Length == 0);
            }
        }

        /// <summary>
        /// Returns true if user has not entered a penalty function override value
        /// </summary>
        /// <returns></returns>
        private bool PenaltyFunctionIsDefault()
        {
            return (PenaltyFunctionOverride == string.Empty);
        }

        /// <summary>
        /// Returns true if user has not entered any location override values
        /// </summary>
        /// <returns></returns>
        private bool LocationOverrideIsDefault()
        {
            return ( string.IsNullOrEmpty(textOriginEasting.Text)
                && string.IsNullOrEmpty(textOriginNorthing.Text)
                && string.IsNullOrEmpty(textDestinationEasting.Text)
                && string.IsNullOrEmpty(textDestinationNorthing.Text));
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Returns true if the control is being displayed in ambiguity mode. This is determined
        /// from the values selected/entered
        /// </summary>
        public bool AmbiguityMode
        {
            get
            {
                return ((SpeedDisplayMode != GenericDisplayMode.Normal)
                          || (AvoidListDisplayMode != GenericDisplayMode.Normal)
                          || (ViaLocationDisplayMode != GenericDisplayMode.Normal)
                          || (PenaltyFunctionDisplayMode != GenericDisplayMode.Normal)
                          || (LocationOverrideDisplayMode != GenericDisplayMode.Normal));
            }
        }

        /// <summary>
        /// Read only. Returns true if all options on this control are default, i.e. user hasn't changed anything
        /// </summary>
        public bool PreferencesDefault
        {
            get { return CycleSpeedIsDefault() && AvoidListIsDefault() 
                && ViaLocationIsDefault() && PenaltyFunctionIsDefault() && LocationOverrideIsDefault(); }
        }

        /// <summary>
        /// Controls whether or not the preferences panel is visible
        /// </summary>
        public bool PreferencesVisible
        {
            get { return preferencesPanel.Visible; }
            set
            {
                if (preferencesPanel.Visible != value)
                {
                    preferencesPanel.Visible = value;
                    RaiseEvent(PreferencesVisibleChangedEventKey);
                }
            }
        }
        
        /// <summary>
        /// Returns true if the user is logged in and has elected to save their
        /// travel details
        /// Read only.
        /// </summary>
        public bool SavePreferences
        {
            get { return loginSaveOption.SaveDetails; }
        }

        /// <summary>
        /// Sets the mode for the speed text box.
        /// Note that in this context, GenericDisplayMode.Ambiguity is treated
        /// the same way as GenericDisplayMode.Readonly.
        /// </summary>
        public GenericDisplayMode SpeedDisplayMode
        {
            get { return speedDisplayMode; }
            set { speedDisplayMode = value; }
        }

        /// <summary>
        /// Sets the mode for the avoid list check boxes.
        /// Note that in this context, GenericDisplayMode.Ambiguity is treated
        /// the same way as GenericDisplayMode.Readonly.
        /// </summary>
        public GenericDisplayMode AvoidListDisplayMode
        {
            get { return avoidListDisplayMode; }
            set { avoidListDisplayMode = value; }
        }

        /// <summary>
        /// Sets the mode for the via location.
        /// Note that in this context, GenericDisplayMode.Ambiguity is treated
        /// the same way as GenericDisplayMode.Readonly.
        /// </summary>
        public GenericDisplayMode ViaLocationDisplayMode
        {
            get { return viaLocationDisplayMode; }
            set { viaLocationDisplayMode = value; }
        }

        /// <summary>
        /// Gets/Sets input speed text
        /// </summary>
        public string SpeedText
        {
            get { return HttpUtility.HtmlDecode(textSpeed.Text); }
            set { textSpeed.Text = HttpUtility.HtmlEncode(value); }
        }

        /// <summary>
        /// Returns true if the speed value entered is valid
        /// </summary>
        public bool SpeedTextValid
        {
            get { return validSpeedText; }
            set { validSpeedText = value; }
        }

        /// <summary>
        /// Gets/Sets the Speed unit dropdown list
        /// </summary>
        public string SpeedUnit
        {
            get
            {
                if (listSpeedUnit.Items.Count <= 0)
                {
                    LoadLists();
                }

                return populator.GetValue(DataServiceType.UnitsSpeedDrop, listSpeedUnit.SelectedItem.Value);
            }
            set
            {
                if (listSpeedUnit.Items.Count <= 0)
                {
                    LoadLists();
                }

                string listSpeedUnitId = populator.GetResourceId(DataServiceType.UnitsSpeedDrop, value);
                populator.Select(listSpeedUnit, listSpeedUnitId);
            }
        }

        /// <summary>
        /// Get. Returns whether a user has entered a speed value, true if none entered.
        /// </summary>
        public bool SpeedIsDefault
        {
            get { return CycleSpeedIsDefault(); }
        }

        /// <summary>
        /// Gets the speed entered in metres per hour 
        /// </summary>
        public string SpeedMetresPerHour
        {
            get
            {
                // Get the speed value entered
                double speed = 0;
                bool speedOk = double.TryParse(SpeedText, out speed);

                // Convert the speed to metres
                if (speed == 0)
                {
                    return speed.ToString();
                }
                else if (speed > 0)
                {
                    if (listSpeedUnit.SelectedIndex == 0)
                    {
                        // miles selected
                        decimal speedMetres = Convert.ToDecimal(MeasurementConversion.Convert(speed, ConversionType.MileageToMetres));
                        return speedMetres.ToString();
                    }
                    else if (listSpeedUnit.SelectedIndex == 1)
                    {
                        // km selected
                        double speedKm = speed * 1000;
                        return speedKm.ToString();
                    }
                }

                return "0";
            }
        }

        /// <summary>
        /// Read/write. Avoid steep climbs preference
        /// </summary>
        public bool AvoidSteepClimbs
        {
            get { return checkboxAvoidSteepClimbs.Checked; }
            set { checkboxAvoidSteepClimbs.Checked = value; }
        }

        /// <summary>
        /// Read/write. Avoid unlit roads preference
        /// </summary>
        public bool AvoidUnlitRoads
        {
            get { return checkboxAvoidUnlitRoads.Checked; }
            set { checkboxAvoidUnlitRoads.Checked = value; }
        }

        /// <summary>
        /// Read/write. Avoid walking your bike preference
        /// </summary>
        public bool AvoidWalkingYourBike
        {
            get { return checkboxAvoidWalkingYourBike.Checked; }
            set { checkboxAvoidWalkingYourBike.Checked = value; }
        }

        /// <summary>
        /// Read/write. Avoid time based restrictions preference
        /// </summary>
        public bool AvoidTimeBased
        {
            get { return checkboxAvoidTimeBased.Checked; }
            set { checkboxAvoidTimeBased.Checked = value; }
        }

        /// <summary>
        /// Sets the mode for the penalty function text box.
        /// Note that in this context, GenericDisplayMode.Ambiguity is treated
        /// the same way as GenericDisplayMode.Readonly.
        /// </summary>
        public GenericDisplayMode PenaltyFunctionDisplayMode
        {
            get { return penaltyFunctionMode; }
            set { penaltyFunctionMode = value; }
        }

        /// <summary>
        /// Gets/Sets the PenaltyFunction override text
        /// </summary>
        public string PenaltyFunctionOverride
        {
            get { return HttpUtility.HtmlDecode(textboxPenaltyFunctionOverride.Text); }
            set { textboxPenaltyFunctionOverride.Text = HttpUtility.HtmlEncode(value); }
        }

        /// <summary>
        /// Get. Returns whether a user has entered any location override values, true if none entered.
        /// </summary>
        public bool LocationsIsDefault
        {
            get { return LocationOverrideIsDefault(); }
        }

        /// <summary>
        /// Sets the mode for the location override text boxes.
        /// Note that in this context, GenericDisplayMode.Ambiguity is treated
        /// the same way as GenericDisplayMode.Readonly.
        /// </summary>
        public GenericDisplayMode LocationOverrideDisplayMode
        {
            get { return locationOverrideMode; }
            set { locationOverrideMode = value; }
        }

        /// <summary>
        /// Gets/Sets the Origin Location override OSGR coordination
        /// </summary>
        public OSGridReference LocationOriginOverride
        {
            get
            {
                try
                {
                    int easting = Convert.ToInt32(HttpUtility.HtmlDecode(textOriginEasting.Text));
                    int northing = Convert.ToInt32(HttpUtility.HtmlDecode(textOriginNorthing.Text));

                    return new OSGridReference(easting,northing);
                }
                catch
                {
                    return new OSGridReference(-1, -1);
                }
            }

            set
            {
                OSGridReference osgr = value;
                if ((osgr != null) && (osgr.Easting > 0) && (osgr.Northing > 0))
                {
                    textOriginEasting.Text = HttpUtility.HtmlEncode(osgr.Easting.ToString());
                    textOriginNorthing.Text = HttpUtility.HtmlEncode(osgr.Northing.ToString());
                }
                else
                {
                    textOriginEasting.Text = string.Empty;
                    textOriginNorthing.Text = string.Empty;
                }
            }
        }

            /// <summary>
            /// Gets/Sets the Destination Location override OSGR coordination
            /// </summary>
        public OSGridReference LocationDestinationOverride
        {
            get
            {
                try
                {
                    int easting = Convert.ToInt32(HttpUtility.HtmlDecode(textDestinationEasting.Text));
                    int northing = Convert.ToInt32(HttpUtility.HtmlDecode(textDestinationNorthing.Text));

                    return new OSGridReference(easting, northing);
                }
                catch
                {
                    return new OSGridReference(-1, -1);
                }
            }

            set
            {
                OSGridReference osgr = value;
                if ((osgr != null) && (osgr.Easting > 0) && (osgr.Northing > 0))
                {
                    textDestinationEasting.Text = HttpUtility.HtmlEncode(osgr.Easting.ToString());
                    textDestinationNorthing.Text = HttpUtility.HtmlEncode(osgr.Northing.ToString());
                }
                else
                {
                    textDestinationEasting.Text = string.Empty;
                    textDestinationNorthing.Text = string.Empty;
                }
            }
        }
        #endregion

        #region Public controls

        /// <summary>
        /// Allows access to the FindPageOptionsControl contained within
        /// this control. This is provided so that event handlers can be
        /// attached to the events that it raises.
        /// The following should be considered when using this property:
        /// <list type="bullet">
        ///     <item><description>DO NOT handle the ShowPreferences event in order to set the PreferencesVisible property of this control. This will be done internally, and the PreferencesVisibilityChanged event will be raised to indicate that this has happened.</description></item>
        ///     <item><description>Take care when setting the AllowShowPreferences property. The visibility of this button is normally dependent on whether or not the preferences are visible, as well as the AmbiguityMode property of this control. Changing it could cause the control to appear in an inconsistant state.</description></item>
        ///     <item><description>Take care when setting the AllowBack property. This is normally controlled by the AmbiguityMode property of this control. Changing it could cause the control to appear in an inconsistant state.</description></item>
        /// </list>
        /// Read only.
        /// </summary>
        public FindPageOptionsControl PageOptionsControl
        {
            get { return pageOptionsControl; }
        }

        /// <summary>
        /// Read/Write to allow access to the LocationControl (auto suggest) contained within this control
        /// </summary>
        public LocationControl LocationControl
        {
            get { return locationControl; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Method which calls validate methods for the preferences on this control. This includes the following 
        /// validation: Speed, Location Override
        /// </summary>
        public void ValidatePreferences()
        {
            ValidateSpeed();
            ValidateLocationOverride();
        }

        #endregion

        #region Events raise and handle

        /// <summary>
        /// Retrieves the delegate attached to an event handler from the Events
        /// list and calls it.
        /// </summary>
        /// <param name="key"></param>
        private void RaiseEvent(object key)
        {
            EventHandler theDelegate = Events[key] as EventHandler;
            if (theDelegate != null)
                theDelegate(this, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs when the user clicks the show preferences or hide preferences buttons
        /// </summary>
        public event EventHandler PreferencesVisibleChanged
        {
            add { this.Events.AddHandler(PreferencesVisibleChangedEventKey, value); }
            remove { this.Events.RemoveHandler(PreferencesVisibleChangedEventKey, value); }
        }

        /// <summary>
        /// Handles the ShowPreferences event of the FindPageOptionsControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnShowPreferences(object sender, EventArgs e)
        {
            PreferencesVisible = true;
        }

        /// <summary>
        /// Handles the ShowPreferences event of the FindPageOptionsControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHidePreferences(object sender, EventArgs e)
        {
            PreferencesVisible = false;
        }

        /// <summary>
        /// Handles the event indicating that the new location button has been clicked
        /// on the nested via location control
        /// </summary>
        /// <param name="sender">event originator</param>
        /// <param name="e">event arguments</param>
        private void OnNewLocation(object sender, EventArgs e)
        {
            newLocationClicked = true;

            if (NewLocation != null)
                NewLocation(sender, e);
        }

        /// <summary>
        /// Occurs when the user changes the speed text entered value
        /// </summary>
        public event EventHandler SpeedTextChanged
        {
            add { this.Events.AddHandler(OnSpeedTextChangedKey, value); }
            remove { this.Events.RemoveHandler(OnSpeedTextChangedKey, value); }
        }

        /// <summary>
        /// Handles the Changed event of the Speed text and raises
        /// the event on to the client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSpeedTextChanged(object sender, EventArgs e)
        {
            RaiseEvent(OnSpeedTextChangedKey);
        }

        #endregion
    }
}