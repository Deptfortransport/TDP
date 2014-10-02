// *************************************************************************** 
// NAME                 : LocationControlVia.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 27/07/2012
// DESCRIPTION			: Control allows user to specify a via location
// *************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/LocationControlVia.ascx.cs-arc  $
//
//   Rev 1.0   Aug 28 2012 10:23:50   mmodi
//Initial revision.
//Resolution for 5832: CCN Gaz

using System;
using TransportDirect.Common;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    ///	Summary description for LocationControlVia.
    /// </summary>
    public partial class LocationControlVia : TDUserControl
    {
        #region Private fields

        //private field for holding value for PageOptionsControl visiblility.
        private bool pageOptionsVisible = true;

        #endregion

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
        }

        /// <summary>
        /// Event handler for page pre-render event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetUpResources();

            pageOptionsControl.Visible = IsPageOptionsVisible;
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

        /// <summary>
        /// Read Only property allows access to PageOptionsControl contained within this control.
        /// </summary>
        public FindPageOptionsControl PageOptionsControl
        {
            get { return pageOptionsControl; }
        }

        /// <summary>
        /// Allows setting PageOptionsControl to be visible or not.
        /// </summary>
        public bool IsPageOptionsVisible
        {
            get { return pageOptionsVisible; }
            set { pageOptionsVisible = value; }
        }

        #endregion
    }
}