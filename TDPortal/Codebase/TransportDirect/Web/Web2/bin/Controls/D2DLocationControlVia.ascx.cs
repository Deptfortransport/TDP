// *************************************************************************** 
// NAME                 : D2DLocationControlVia.ascx
// AUTHOR               : David Lane
// DATE CREATED         : 09/01/2013
// DESCRIPTION			: Control allows user to specify a via location
// *************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/D2DLocationControlVia.ascx.cs-arc  $
//
//   Rev 1.4   Jan 30 2013 13:47:32   mmodi
//Fixed showing advanced options on ambiguity page
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.1   Jan 17 2013 09:45:48   mmodi
//Updates to D2D advanced options for better js and non-js behaviour
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Jan 10 2013 16:33:54   dlane
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//

using System;
using TransportDirect.Common;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    ///	Summary description for D2DLocationControlVia.
    /// </summary>
    public partial class D2DLocationControlVia : TDUserControl
    {
        #region Public events

        /// <summary>
        /// Event fired to signal new location button has been clicked
        /// </summary>
        public event EventHandler NewLocation;
        public event EventHandler MapClick;

        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Event handler for page initialisation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            locationControl.MapLocationClick += new EventHandler(OnMapClick);
            locationControl.NewLocationClick += new EventHandler(OnNewLocation);
        }

        /// <summary>
        /// Page Load method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            labelJsQuestion.Text = GetResource("LocationViaControl.Question");
            labelOptionsSelected.Text = GetResource("LocationViaControl.OptionsSelected");
        }

        /// <summary>
        /// Event handler for page pre-render event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetUpResources();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Event handler to handle the event that signals new location button has been clicked on nested
        /// TriStateLocationControl instance.
        /// </summary>
        /// <param name="sender">event originator</param>
        /// <param name="e">event arguments</param>
        private void OnNewLocation(object sender, EventArgs e)
        {
            if (NewLocation != null)
                NewLocation(sender, e);
        }

        /// <summary>
        /// Event handler for map button click on location control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMapClick(object sender, EventArgs e)
        {
            if (MapClick != null)
                MapClick(sender, e);
        }

        /// <summary>
        /// Show button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShow_Click(object sender, EventArgs e)
        {
            UpdateOptionsVisibility(true);

            btnShow.Visible = false;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialise method
        /// </summary>
        public void Initialise(
            TDLocation location,
            LocationSearch search,
            DataServiceType locationListType,
            bool allowAutoSuggest,
            bool allowAmbiguity,
            bool allowAmbiguityReset,
            bool allowUnsureSpelling,
            bool allowFindOnMap,
            bool allowLocationTypesListRadio,
            bool allowLocationTypesListDropDown,
            bool showValidLocationFixed,
            bool showMoreOptionsExpanded
            )
        {
            locationControl.Initialise(location, search, locationListType, 
                allowAutoSuggest, allowAmbiguity, allowAmbiguityReset,
                allowUnsureSpelling, allowFindOnMap, allowLocationTypesListRadio, allowLocationTypesListDropDown,
                showValidLocationFixed, showMoreOptionsExpanded);
        }

        /// <summary>
        /// Resets location
        /// </summary>
        public void Reset()
        {
            locationControl.Reset();

            UpdateOptionsVisibility(false);

            btnShow.Visible = true;
        }

        /// <summary>
        /// Validates the control by resolving the location
        /// </summary>
        /// <returns>True if the location is resolved</returns>
        public bool Validate(
            TDJourneyParameters journeyParameters,
            bool allowGroupLocations,
            bool acceptsPostcode,
            bool acceptsPartPostcode,
            StationType stationType)
        {
            return locationControl.Validate(journeyParameters, allowGroupLocations, acceptsPostcode, acceptsPartPostcode, stationType);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// set controls visible according to which Find a page this control is on
        /// </summary>
        private void SetUpResources()
        {
            //journey options text
            labelJourneyOptions.Text = Global.tdResourceManager.GetString("FindViaLocationControl.labelJourneyOptions", TDCultureInfo.CurrentUICulture);

            btnShow.Text = GetResource("AdvancedOptions.Show.Text");

            //evaluate the containing page
            switch (this.PageId)
            {
                //Find a coach input page
                case TransportDirect.Common.PageId.FindCoachInput:
                    instructionLabel.Text = Global.tdResourceManager.GetString("FindViaLocationControl.instructionLabelCoach", TDCultureInfo.CurrentUICulture);
                    break;

                //Find a car journey input page
                case TransportDirect.Common.PageId.FindCarInput:
                    instructionLabel.Text = Global.tdResourceManager.GetString("FindViaLocationControl.instructionLabelCar", TDCultureInfo.CurrentUICulture);
                    break;

                //JourneyPlannerInput page
                case TransportDirect.Common.PageId.JourneyPlannerInput:
                case TransportDirect.Common.PageId.FindBusInput:
                    instructionLabel.Text = Global.tdResourceManager.GetString("FindViaLocationControl.instructionLabelCar", TDCultureInfo.CurrentUICulture);
                    break;

                //JourneyPlannerAmbiguity page
                case TransportDirect.Common.PageId.JourneyPlannerAmbiguity:
                    instructionLabel.Text = Global.tdResourceManager.GetString("FindViaLocationControl.instructionLabelCar", TDCultureInfo.CurrentUICulture);
                    break;

                //default, hide the label
                default:
                    instructionLabel.Text = String.Empty;
                    instructionLabel.Visible = false;

                    break;
            }

            locationControl.LocationInputDescription.Text = GetResource("LocationControl.LocationInputDescription.PTVia.Text");
            locationControl.LocationTypeDescription.Text = GetResource("FindStationInput.labelSRLocation");
            locationControl.LocationInput.ToolTip = GetResource("LocationControl.LocationInput.PTVia.ToolTip");
        }

        /// <summary>
        /// Updates the display class of the options content
        /// </summary>
        private void UpdateOptionsVisibility(bool showExpanded)
        {
            if (showExpanded)
            {
                if (!optionContentRow.Attributes["class"].Contains("show"))
                    optionContentRow.Attributes["class"] = string.Format("{0} show",
                        optionContentRow.Attributes["class"].Replace("hide", string.Empty));
            }
            else
            {
                if (!optionContentRow.Attributes["class"].Contains("hide"))
                    optionContentRow.Attributes["class"] = string.Format("{0} hide",
                        optionContentRow.Attributes["class"].Replace("show", string.Empty));
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Read/Write property indicates if the location should be resolved when Validate is called.
        /// This is to allow a pre-resolved location set during a landing page to bypass resolution when
        /// autoplan is in effect (as the control will not have gone through its life cycle and therefore not 
        /// fully setup).
        /// Default is true.
        /// </summary>
        public bool ResolveLocation
        {
            get { return locationControl.ResolveLocation; }
            set { locationControl.ResolveLocation = value; }
        }

        /// <summary>
        /// Get/Sets the LocationSearch represented by the location control
        /// </summary>
        public LocationSearch Search
        {
            get { return locationControl.Search; }
            set { locationControl.Search = value; }
        }

        /// <summary>
        /// Get/Sets the TDLocation represented by the location control
        /// </summary>
        public TDLocation Location
        {
            get { return locationControl.Location; }
            set { locationControl.Location = value; }
        }

        #endregion
    }
}