// *********************************************** 
// NAME                 : MapInputControl.ascx
// AUTHOR               : Amit Patel
// DATE CREATED         : 05/11/2009
// DESCRIPTION          : Map control used for find input pages
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapInputControl.ascx.cs-arc  $ 
//
//   Rev 1.6   Jul 29 2010 16:12:10   mmodi
//Changes to page layout and styles to be exactly consistent for all input pages in the Portal
//Resolution for 4760: IE7-find a car route check boxes
//
//   Rev 1.5   Apr 07 2010 13:31:58   pghumra
//Various updates for accessibility purposes
//Resolution for 5491: RS69801 CCN0551 Accessibility - Spaces TfL Map Note London Bus
//
//   Rev 1.4   Nov 30 2009 09:58:02   apatel
//input page find on map workflow changed
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Nov 27 2009 15:46:32   mmodi
//Added Map select locaiton control
//
//   Rev 1.2   Nov 16 2009 15:28:06   mmodi
//Updated styles
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.1   Nov 10 2009 12:40:18   apatel
//Added comments
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.0   Nov 10 2009 11:35:28   apatel
//Initial revision.
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps


using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Map control used for Find a map
    /// </summary>
    public partial class MapInputControl : TDUserControl
    {
        #region Private members

        private SearchType searchType;
        private MapLocationMode[] mapModes = new MapLocationMode[2] { MapLocationMode.Start, MapLocationMode.End };
        private MapLocationPoint[] mapLocationPoints = new MapLocationPoint[0];
        private MapParameters mapParameters = new MapParameters();
        private MapHelper mapHelper = new MapHelper();
        private int mapHeight = 500;
        private int mapWidth = 608;
        private OSGridReference mapCenter = new OSGridReference();

        private const string MAP_ALT_TEXT_LOCATION_PATTERN = "{LOCATION}";

        #endregion

        #region Public Properties
        /// <summary>
        /// Id of the Map control generated 
        /// </summary>
        public string MapID
        {
            get
            {
                return mapControl.MapID;
            }

        }

        /// <summary>
        /// Height of the map
        /// </summary>
        public int MapHeight
        {
            set
            {
                mapHeight = value; 
            }
        }

        /// <summary>
        /// Width of the map
        /// </summary>
        public int MapWidth
        {
            set
            {
                mapWidth = value;
            }
        }

        public OSGridReference MapCenter
        {
            set
            {
                mapCenter = value;
            }
        }

             
        #endregion

        #region Initialise
        /// <summary>
        /// Initialises the Map with location search type to setup the scale of map
        /// </summary>
        /// <param name="searchType">Search type</param>
        public void Initialise(SearchType searchType)
        {
            this.searchType = searchType;
            Initialise(searchType, new MapLocationPoint[0]);
                        
        }

        /// <summary>
        /// Initialises the Map with location search type and map location points
        /// Map scale gets set up based on the search type
        /// </summary>
        /// <param name="searchType">Search type</param>
        /// <param name="mapLocationPoints">Array of map location points</param>
        public void Initialise(SearchType searchType, MapLocationPoint[] mapLocationPoints)
        {
            mapParameters.MapScale = mapHelper.GetScaleForSearchType(searchType);
            this.mapLocationPoints = mapLocationPoints;
            
           
        }

        /// <summary>
        /// Initialises the Map with location search type, map location points and map location modes
        /// Used to initialise the Map when via mode needs to be displayed as link in popup control.
        /// By default only start and end mode links displayed on the map
        /// Map scale gets set up based on the search type
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="mapLocationPoints"></param>
        /// <param name="mapModes"></param>
        public void Initialise(SearchType searchType, MapLocationPoint[] mapLocationPoints, MapLocationMode[] mapModes)
        {
            Initialise(searchType, mapLocationPoints);
            this.mapModes = mapModes;
           
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
            InitialiseControls();

            RegisterJavaScripts();
        }

        #endregion

        #region Private methods

        private void SetMapAltText(string location)
        {
            string mapAltText = GetResource("MapInputControl.MapAltText");
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
        /// Initialises the controls
        /// </summary>
        private void InitialiseControls()
        {
            // Set the common map paramters
            mapParameters.MapInfoDisplayText = GetResource("MapControl.PlanAJourney.Text");
            mapParameters.MapLocationModes = mapModes;
            mapParameters.MapToolbarTools = new MapToolbarTool[4] { MapToolbarTool.Pan, MapToolbarTool.Zoom, MapToolbarTool.UserDefinedLocation, MapToolbarTool.SelectNearbyLocation };
            mapParameters.MapWidth = mapWidth;
            mapParameters.MapHeight = mapHeight;
            mapParameters.MapLocationModes = mapModes;

            if (mapCenter.IsValid)
            {
                mapParameters.MapCentre = mapCenter;
            }

            mapControl.Initialise(mapParameters, mapLocationPoints);

            // Initialise the select new location
            mapSelectLocationControl.MapId = mapControl.MapID;
            mapSelectLocationControl.MapLocation = null;
            if (mapLocationPoints.Length > 0)
            {
                SetMapAltText(mapLocationPoints[0].MapLocationDescription);
            }
            
        }

        /// <summary>
        /// Method which adds the javascript to the controls
        /// </summary>
        private void RegisterJavaScripts()
        {
            TDPage page = (TDPage)this.Page;

            if (page.IsJavascriptEnabled)
            {
                string symbolsOnMap = Properties.Current["MapInputControl.TransportSymbols"];

                string dojoScript = "dojo.subscribe(\"Map\",function(mapArgs){ setTransportSymbolsOnMap('" + mapControl.MapID + "','" + symbolsOnMap + "', mapArgs); });";

                // Determine if javascript is support and determine the JavascriptDom value
                ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

                string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "dojoScript", dojoScript, true);

                string scriptName = "MapInputControl";

                // Register the javascript file to toggle the map symbols on the page
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", scriptRepository.GetScript(scriptName, javaScriptDom));

                string mapAltText = this.mapControl.MapAltText;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MapAltText", "InitMapAltText('" + mapAltText + "');", true);
            }
        }

        #endregion
    }
}