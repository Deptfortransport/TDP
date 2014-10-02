// *********************************************** 
// NAME                 : JourneyDetailsShowControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 08/11/2009
// DESCRIPTION          : Control used on a map page to display buttons to allow journey details to be shown.
//                      : Elements have been moved from the JourneyMapControl to this control, for use by the 
//                      : mapping enhancements
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyDetailsShowControl.ascx.cs-arc  $ 
//
//   Rev 1.0   Nov 09 2009 15:42:42   mmodi
//Initial revision.
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Control used on a map page to display buttons to allow journey details to be shown
    /// </summary>
    public partial class JourneyDetailsShowControl : TDUserControl
    {
        #region Private members

        private bool outward = true;
        private bool show = true;
        private bool directionsVisible = false;
        
        RoadJourney roadJourney = null;
        CycleJourney cycleJourney = null;

        #endregion

        #region Initialise

        /// <summary>
        /// Initialises the journey details show buttons control with the properties specified
        /// </summary>
        /// <param name="outward">true for outward journey</param>
        /// <param name="show">whether to show the control or not</param>
        public void Initialise(bool outward, bool show, Journey journey)
        {
            this.outward = outward;
            this.show = show;

            // Parse the journey into the correct variable
            if (journey == null)
            {
                this.roadJourney = null;
                this.cycleJourney = null;
            }
            else if (journey is RoadJourney)
            {
                this.roadJourney = (RoadJourney)journey;
            }
            else if (journey is CycleJourney)
            {
                this.cycleJourney = (CycleJourney)journey;
            }
        }

        #endregion

        #region Page_Load, Page_PreRender

        /// <summary>
        /// Page_Init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            buttonDirectionsRoad.Click += new EventHandler(buttonDirectionsRoad_Click);
            buttonDirectionsCycle.Click += new EventHandler(buttonDirectionsCycle_Click);
        }
                
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadResources();

            if (!IsPostBack)
            {
                // Ensure the directions are not printed out by default
                SetPrinterDirectionsVisible(false);
                directionsVisible = false;
            }
            else
            {
                directionsVisible = GetPrinterDirectionsVisible();
            }
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            InitialiseControls();

            SetControlVisibility();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method to load the resources needed the control
        /// </summary>
        private void LoadResources()
        {
            buttonDirectionsRoad.Text = GetResource("JourneyMapControl.ButtonDirectionsShow.Text");
            buttonDirectionsCycle.Text = GetResource("JourneyMapControl.ButtonDirectionsShow.Text");

            labelTotalDistance.Text = GetResource("JourneyMapControl.labelTotalDistance");
            labelTotalDuration.Text = GetResource("JourneyMapControl.labelTotalDuration");
        }

        /// <summary>
        /// Intialises the controls
        /// </summary>
        private void InitialiseControls()
        {
            if (roadJourney != null)
            {
                InitialiseControlsForRoadJourney();
            }
            else if (cycleJourney != null)
            {
                InitialiseControlsForCycleJourney();
            }
        }

        /// <summary>
        /// Initialises the controls for a road journey
        /// </summary>
        private void InitialiseControlsForRoadJourney()
        {
            int distance = roadJourney.TotalDistance;

            // Convert the distance to miles using the conversion factor

            // Retrieve the conversion factor from the Properties Service.
            double conversionFactor =
                Convert.ToDouble(Properties.Current["Web.Controls.MileageConverter"], TDCultureInfo.CurrentCulture.NumberFormat);

            double result = (double)distance / conversionFactor;

            // Only want the string to have 1 decimal place - chop off everything
            // after that.
            labelTotalMiles.Text = result.ToString("F1", TDCultureInfo.CurrentCulture.NumberFormat)
                + " " +  GetResource("JourneyMapControl.labelMiles");

            // Now get the duration
            double time = (double)roadJourney.TotalDuration / 60.0; // in minutes
            time = Round(time);
            
            // Convert to hours and minutes for display
            long hours = (long)time / 60;
            long minutes = (long)time % 60;

            labelTotalTime.Text = hours.ToString(TDCultureInfo.CurrentUICulture.NumberFormat)
                + " " + GetResource("JourneyMapControl.labelHours")
                + " " + minutes.ToString(TDCultureInfo.CurrentUICulture.NumberFormat)
                + " " + GetResource("JourneyMapControl.labelMinutes");
        }

        /// <summary>
        /// Initialises the controls for a cycle journey
        /// </summary>
        private void InitialiseControlsForCycleJourney()
        {
            int distance = cycleJourney.TotalDistance;

            // Convert the distance to miles using the conversion factor
            string resultMileage = MeasurementConversion.Convert((double)distance, ConversionType.MetresToMileage);
            if (string.IsNullOrEmpty(resultMileage))
                resultMileage = "0";

            double result = Convert.ToDouble(resultMileage);

            // Only want the string to have 1 decimal place - chop off everything
            // after that.
            labelTotalMiles.Text = result.ToString("F1", TDCultureInfo.CurrentCulture.NumberFormat)
                + " " + GetResource("JourneyMapControl.labelMiles");

            // Now get the duration
            double time = (double)cycleJourney.TotalDuration / 60.0; // in minutes
            time = Round(time);

            // Convert to hours and minutes for display
            long hours = (long)time / 60;
            long minutes = (long)time % 60;

            labelTotalTime.Text = hours.ToString(TDCultureInfo.CurrentUICulture.NumberFormat)
                + " " + GetResource("JourneyMapControl.labelHours")
                + " " + minutes.ToString(TDCultureInfo.CurrentUICulture.NumberFormat)
                + " " + GetResource("JourneyMapControl.labelMinutes");
        }

        /// <summary>
        /// Method to set the visibility of the controls
        /// </summary>
        private void SetControlVisibility()
        {
            panelTravelInfo.Visible = show;
            
            buttonDirectionsRoad.Visible = (roadJourney != null);
            buttonDirectionsCycle.Visible = (cycleJourney != null);
        }

        /// <summary>
        /// Returns what the car/cycle directions flag in the session
        /// is set to. 
        /// If the directions are visible on screen, they should be visible
        /// on the printer friendly page.  If the directions are not visible
        /// on screen, then it should not be visible on the printer friendly
        /// page.
        /// </summary>
        /// <returns></returns>
        private bool GetPrinterDirectionsVisible()
        {
            // saves if user selected show directions/not in session for print page
            TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
            if (viewState != null)
            {
                if (outward)
                    return viewState.OutwardShowDirections;
                else
                    return viewState.ReturnShowDirections;
            }

            return false;
        }

        /// <summary>
        /// Determines what the car/cycle directions flag in the session
        /// should be set to.  This is used for the printer friendly page.
        /// If the directions are visible on screen, they should be visible
        /// on the printer friendly page.  If the directions are not visible
        /// on screen, then it should not be visible on the printer friendly
        /// page.
        /// </summary>
        private void SetPrinterDirectionsVisible(bool visible)
        {
            // saves if user selected show directions/not in session for print page
            TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
            if (viewState != null)
            {
                if (outward)
                    viewState.OutwardShowDirections = visible;
                else
                    viewState.ReturnShowDirections = visible;
            }
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

        #region Public properties

        /// <summary>
        /// Read/write. Sets the outward flag
        /// </summary>
        public bool Outward
        {
            get { return outward; }
            set { outward = value; }
        }

        /// <summary>
        /// Read/write. Sets the show control flag
        /// </summary>
        public bool Show
        {
            get { return show; }
            set { show = value; }
        }

        /// <summary>
        /// Read/write. Identifies if the directions are visible or not
        /// </summary>
        public bool DirectionsVisible
        {
            get { return directionsVisible; }
            set { directionsVisible = value; }
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Click event handler for the Directions Road button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDirectionsRoad_Click(object sender, EventArgs e)
        {
            // Check to see if directions are currently showing
            if (directionsVisible)
            {
                directionsVisible = false;
                
                //Update the text of the image to the 'Show' version
                buttonDirectionsRoad.Text = GetResource("JourneyMapControl.ButtonDirectionsShow.Text");
            }
            else
            {
                directionsVisible = true;

                //Update the text of the image to the 'Hide' version
                buttonDirectionsRoad.Text = GetResource("JourneyMapControl.ButtonDirectionsHide.Text");
            }

            // saves if user selected show directions/not in session for print page
            SetPrinterDirectionsVisible(directionsVisible);
        }

        /// <summary>
        /// Click event handler for the Directions Cycle button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDirectionsCycle_Click(object sender, EventArgs e)
        {
            // Check to see if directions are currently showing
            if (directionsVisible)
            {
                directionsVisible = false;

                //Update the text of the image to the 'Show' version
                buttonDirectionsCycle.Text = GetResource("JourneyMapControl.ButtonDirectionsShow.Text");
            }
            else
            {
                directionsVisible = true;

                //Update the text of the image to the 'Hide' version
                buttonDirectionsCycle.Text = GetResource("JourneyMapControl.ButtonDirectionsHide.Text");
            }

            // saves if user selected show directions/not in session for print page
            SetPrinterDirectionsVisible(directionsVisible);
        }

        #endregion
    }
}