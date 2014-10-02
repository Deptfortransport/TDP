// *********************************************** 
// NAME                 : MapBasicControl.ascx
// AUTHOR               : Amit Patel
// DATE CREATED         : 05/11/2009
// DESCRIPTION          : Basic map control used for Stop Information page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapBasicControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Apr 07 2010 13:31:56   pghumra
//Various updates for accessibility purposes
//Resolution for 5491: RS69801 CCN0551 Accessibility - Spaces TfL Map Note London Bus
//
//   Rev 1.2   Dec 01 2009 14:55:36   mmodi
//Remove the plan a journey links, and hide all symbols on the map
//
//   Rev 1.1   Nov 16 2009 15:28:02   mmodi
//Updated styles
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
// Amit forgot to add header details

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Basic map control used for Stop Information page
    /// </summary>
    public partial class MapBasicControl : TDUserControl
    {
        #region Private members

        private int mapHeight = 224;
        private int mapWidth = 389;

        private MapHelper mapHelper = new MapHelper();
        private MapLocationPoint[] mapLocationPoints = new MapLocationPoint[0];
        private MapParameters mapParameters = new MapParameters();

        private const string MAP_ALT_TEXT_STOPNAME_PATTERN = "{STOPNAME}";

        #endregion

        #region Public Properties

        /// <summary>
        /// Read only. Returns the ClientID of the MapControl
        /// </summary>
        public string MapID
        {
            get { return mapControl.MapID; }
        }

        /// <summary>
        /// Height of the map
        /// </summary>
        public int MapHeight
        {
            set { mapHeight = value; }
        }

        /// <summary>
        /// Width of the map
        /// </summary>
        public int MapWidth
        {
            set { mapWidth = value; }
        }

        #endregion

        #region Initialise

        /// <summary>
        /// Initialises the Map with  map location points
        /// </summary>
        /// <param name="mapLocationPoints">Array of map location points</param>
        public void Initialise(MapLocationPoint[] mapLocationPoints)
        {
            this.mapLocationPoints = mapLocationPoints;
        }

        /// <summary>
        /// Initialises the Map with scale and map location points
        /// </summary>
        /// <param name="scale">Scale of Map</param>
        /// <param name="mapLocationPoints">Array of map location points</param>
        public void Initialise(int scale, MapLocationPoint[] mapLocationPoints)
        {
           mapParameters.MapScale = scale;
           this.mapLocationPoints = mapLocationPoints;
        }

        /// <summary>
        /// Initialises the Map with scale and map location point and centers the map to the location point
        /// </summary>
        /// <param name="scale">Scale of Map</param>
        /// <param name="mapLocationPoints">Array of map location points</param>
        public void Initialise(int scale, MapLocationPoint mapLocationPoint)
        {
            mapParameters.MapScale = scale;
            if (mapLocationPoint.MapLocationOSGR.IsValid)
            {
                mapParameters.MapCentre = mapLocationPoint.MapLocationOSGR;
            }
            this.mapLocationPoints = new MapLocationPoint[] { mapLocationPoint };
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
            InitialiseControls();
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ResetPrintableSessionValues();

            RegisterJavaScripts();
        }

        #endregion

        #region Private methods

        private void SetMapAltText(string stopName)
        {
            string mapAltText = GetResource("MapBasicControl.MapAltText");
            if (!string.IsNullOrEmpty(mapAltText))
            {
                mapAltText = mapAltText.Replace(MAP_ALT_TEXT_STOPNAME_PATTERN, stopName);
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
            mapParameters.MapWidth = mapWidth;
            mapParameters.MapHeight = mapHeight;

            // Show no tools (pan...), or plan a journey links on the popup
            mapParameters.MapToolbarTools = new MapToolbarTool[0];
            mapParameters.MapLocationModes = new MapLocationMode[1] { MapLocationMode.None };

            mapControl.Initialise(mapParameters, mapLocationPoints);
            SetMapAltText(this.mapLocationPoints[0].MapLocationDescription);
            
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

        /// <summary>
        /// Method which adds the javascript to ensure no symbols are shown on the map
        /// </summary>
        private void RegisterJavaScripts()
        {
            // Determine if javascript is support and determine the JavascriptDom value
            ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

            // Script containing the javascript to do the toggle of map symbols
            string scriptName = "MapSymbolsSelectControl";
            string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];

            // Register the javascript file to toggle the map symbols on the page
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", scriptRepository.GetScript(scriptName, javaScriptDom));


            // Subscribe to the map event to call the hide all symbols method
            StringBuilder dojoHideSymbols = new StringBuilder();

            dojoHideSymbols.Append("dojo.subscribe(\"Map\",function(mapArgs){ ");
            dojoHideSymbols.Append("hideLayers('");
            dojoHideSymbols.Append(mapControl.MapID);
            dojoHideSymbols.Append("'); });");

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "dojoHideSymbolsScript", dojoHideSymbols.ToString(), true);

            string mapAltText = this.mapControl.MapAltText;
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MapAltText", "InitMapAltText('" + mapAltText + "');", true);
        }


        #endregion
    }
}