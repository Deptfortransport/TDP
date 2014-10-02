// *********************************************** 
// NAME                 : MapNearestControl.ascx
// AUTHOR               : Amit Patel
// DATE CREATED         : 12/11/2009
// DESCRIPTION          : Map control used on find nearest result Map pages
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapNearestControl.ascx.cs-arc  $
//
//   Rev 1.8   Jan 29 2013 13:02:24   mmodi
//Display select this stop link in the accessible stops map 
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.7   Jan 04 2013 15:40:50   mmodi
//Updates for Find nearest accessible stops page display and logic
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.6   Apr 07 2010 13:32:00   pghumra
//Various updates for accessibility purposes
//Resolution for 5491: RS69801 CCN0551 Accessibility - Spaces TfL Map Note London Bus
//
//   Rev 1.5   Mar 11 2010 12:35:08   mmodi
//Hide all overlay symbols, as it should be prior to mapping enhancements
//Resolution for 5440: Maps - Issue 16 ESRI fixes
//
//   Rev 1.4   Dec 07 2009 11:22:08   mmodi
//Corrected scroll to map issue in IE
//
//   Rev 1.3   Nov 29 2009 12:40:52   mmodi
//Updated tools to show on map
//
//   Rev 1.2   Nov 17 2009 18:00:22   mmodi
//Updated to set the selected symbols based on nearest control type
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.1   Nov 12 2009 13:38:48   mmodi
//Updated
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
// Amit forgot to add header details

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Map control used on find nearest result Map pages
    /// </summary>
    public partial class MapNearestControl : TDUserControl
    {
        #region Private members

        private MapHelper mapHelper = new MapHelper();

        private string mapLocationTitle = string.Empty;
        private OSGridReference mapEnvelopeMin;
        private OSGridReference mapEnvelopeMax;
        private MapLocationPoint[] mapLocationPoints = new MapLocationPoint[0];
        private MapLocationMode[] mapLocationModes = new MapLocationMode[0];
        private string mapAJAXControlId = string.Empty;

        private bool isForNearestStation = true;
        private bool isForNearestAccessible = false;

        // Used for Nearest accessible page
        private int mapHeight = 500;
        private int mapWidth = 603;

        private const string MAP_ALT_TEXT_LOCATION_PATTERN = "{LOCATION}";


        #endregion

        #region Public Properties
        /// <summary>
        /// Id of the rendered map control
        /// </summary>
        public string MapId
        {
            get { return mapControl.MapID; }

        }

        /// <summary>
        /// Read only. Returns the first element Id on this control (to allow scrolling to on page refresh)
        /// </summary>
        public string FirstElementId
        {
            get { return scrollToMap.ClientID; }
        }

        /// <summary>
        /// Read only property exposes map symbols select control
        /// </summary>
        public MapSymbolsSelectControl MapSymbolsControl
        {
            get { return mapSymbolsSelectControl; }
        }

        /// <summary>
        /// Read only. Exposes the hide map  button
        /// </summary>
        public TDButton HideMapButton
        {
            get { return commandHideMap; }
        }

        #endregion

        #region Initialise
        
        /// <summary>
        /// Initialises MapNearestControl with map envelope min and max grid refrences and map location points to display on map
        /// </summary>
        /// <param name="mapEnvelopeMin">Min OSGR of map envelop</param>
        /// <param name="mapEnvelopeMax">Max OSGR of map envelop</param>
        /// <param name="mapLocationPoints">Location points to display on map</param>
        public void Initialise(OSGridReference mapEnvelopeMin, OSGridReference mapEnvelopeMax, MapLocationPoint[] mapLocationPoints)
        {
            this.mapEnvelopeMin = mapEnvelopeMin;
            this.mapEnvelopeMax = mapEnvelopeMax;
            this.mapLocationPoints = mapLocationPoints;
        }

        /// <summary>
        /// Initialises MapNearestControl with map envelope min and max grid refrences and map location points to display on map
        /// </summary>
        /// <param name="mapEnvelopeMin">Min OSGR of map envelop</param>
        /// <param name="mapEnvelopeMax">Max OSGR of map envelop</param>
        /// <param name="mapLocationPoints">Location points to display on map</param>
        /// <param name="mapLocationTitle">Map location title, shows the title panel if set</param>
        /// <param name="isForNearestStation">If true, then Station map symbols are shown by default. False then
        /// Car park symbols are shown by default</param>
        public void Initialise(OSGridReference mapEnvelopeMin, OSGridReference mapEnvelopeMax, MapLocationPoint[] mapLocationPoints, 
            string mapLocationTitle, bool isForNearestStation)
        {
            this.Initialise(mapEnvelopeMin, mapEnvelopeMax, mapLocationPoints, null, mapLocationTitle, isForNearestStation, 
                false, string.Empty);
        }

        /// <summary>
        /// Initialises MapNearestControl with map envelope min and max grid refrences and map location points to display on map
        /// </summary>
        /// <param name="mapEnvelopeMin">Min OSGR of map envelop</param>
        /// <param name="mapEnvelopeMax">Max OSGR of map envelop</param>
        /// <param name="mapLocationPoints">Location points to display on map</param>
        /// <param name="mapLocationTitle">Map location title, shows the title panel if set</param>
        /// <param name="isForNearestStation">If true, then Station map symbols are shown by default. False then
        /// <param name="isForNearestAccessible">If true, then smaller map is displayed</param>
        /// Car park symbols are shown by default</param>
        public void Initialise(OSGridReference mapEnvelopeMin, OSGridReference mapEnvelopeMax, 
            MapLocationPoint[] mapLocationPoints, MapLocationMode[] mapLocationModes,
            string mapLocationTitle, bool isForNearestStation, bool isForNearestAccessible, string mapAJAXControlId)
        {
            this.Initialise(mapEnvelopeMin, mapEnvelopeMax, mapLocationPoints);

            this.mapLocationModes = mapLocationModes;
            this.mapLocationTitle = mapLocationTitle;
            this.isForNearestStation = isForNearestStation;
            this.isForNearestAccessible = isForNearestAccessible;
            this.mapAJAXControlId = mapAJAXControlId;
        }

        #endregion

        #region Page_Load, Page_PreRender

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            LoadResources();

            InitialiseControls();

            ResetPrintableSessionValues();

            RegisterMapAltTextJavaScript();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads resources needed by the control
        /// </summary>
        private void LoadResources()
        {
            commandHideMap.Text = GetResource("MapNearestControl.commandHideMap.Text");
        }

        /// <summary>
        /// Initialises the controls
        /// </summary>
        private void InitialiseControls()
        {
            InitialiseMap();

            InitialiseMapSymbolsControl();

            InitialiseMapLocationTitle();
        }

        /// <summary>
        /// Initialises the map control
        /// </summary>
        private void InitialiseMap()
        {
            MapHelper mapHelper = new MapHelper();

            // Set the map paramters
            MapParameters mapParameters = new MapParameters();

            if ((mapEnvelopeMin != null && mapEnvelopeMax != null) &&
                (mapEnvelopeMin.IsValid && mapEnvelopeMax.IsValid))
            {
                mapParameters.MapEnvelopeMax = mapEnvelopeMax;
                mapParameters.MapEnvelopeMin = mapEnvelopeMin;
                
                mapParameters.MapToolbarTools = new MapToolbarTool[2] { MapToolbarTool.Pan, MapToolbarTool.Zoom };

                // Only allow the stop information page link to be shown, unless overridden 
                if (mapLocationModes != null && mapLocationModes.Length > 0)
                    mapParameters.MapLocationModes = mapLocationModes;
                else
                    mapParameters.MapLocationModes = new MapLocationMode[1] { MapLocationMode.None };


                if (isForNearestAccessible)
                {
                    mapParameters.MapHeight = mapHeight;
                    mapParameters.MapWidth = mapWidth;
                }

                // Initialise map with map envelope and location points
                mapControl.Initialise(mapParameters, mapLocationPoints);

                // Set the ajax container id, map control will handle it
                mapControl.MapAJAXControlId = mapAJAXControlId;
            }
            else
            {
                // No valid location, so initialise a default map
                mapControl.Initialise();
            }
        }

        /// <summary>
        /// Initialises the map symbols control
        /// </summary>
        private void InitialiseMapSymbolsControl()
        {
            // Initialise map symbols
            mapSymbolsSelectControl.MapId = mapControl.MapID;

            if (isForNearestStation)
            {
                mapSymbolsSelectControl.UncheckTransportSectionSymbols();
            }
            else
            {
                mapSymbolsSelectControl.UncheckTransportSectionSymbols();
            }
        }

        /// <summary>
        /// Initialise the map location title
        /// </summary>
        private void InitialiseMapLocationTitle()
        {
            // Initialise the location text
            if (!string.IsNullOrEmpty(mapLocationTitle))
            {
                labelLocationName.Text = mapLocationTitle;
                panelMapLocationTitle.Visible = true;
                SetMapAltText(mapLocationTitle);
            }
            else
            {
                panelMapLocationTitle.Visible = false;
            }
        }

        private void SetMapAltText(string location)
        {
            string mapAltText = GetResource("MapNearestControl.MapAltText");
            if (!string.IsNullOrEmpty(mapAltText))
            {
                mapAltText = mapAltText.Replace(MAP_ALT_TEXT_LOCATION_PATTERN, location);
                mapAltText = mapAltText.Replace("'", @"\'");
                mapControl.MapAltText = mapAltText;
            }
            else
            {
                mapControl.MapAltText = string.Empty;
            }
        }

        /// <summary>
        /// Resets the session variables used by the printable version of map, used by 
        /// the Printer Friendly page.
        /// </summary>
        private void ResetPrintableSessionValues()
        {
            if (!Page.IsPostBack)
            {
                mapHelper.ResetPrintableMapSessionValues(true, string.Empty);
                mapHelper.ResetPrintableMapSessionValues(false, string.Empty);
            }
        }

        private void RegisterMapAltTextJavaScript()
        {
            string mapAltText = this.mapControl.MapAltText;
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MapAltText", "InitMapAltText('" + mapAltText + "');", true);
        }

        #endregion
    }
}