// *********************************************** 
// NAME                 : MapTravelNewsControl.ascx
// AUTHOR               : Mark Turner
// DATE CREATED         : 04/11/2009
// DESCRIPTION          : Map Web user control, using the ESRI AJAX Mapping components
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapTravelNewsControl.ascx.cs-arc  $ 
//
//   Rev 1.7   Jul 20 2010 11:38:18   apatel
//Updated to update the zoom to point method to correct assignment of the scale.
//Resolution for 5578: Travel news issue with single incident  clicked from home page
//
//   Rev 1.6   Apr 07 2010 13:32:02   pghumra
//Various updates for accessibility purposes
//Resolution for 5491: RS69801 CCN0551 Accessibility - Spaces TfL Map Note London Bus
//
//   Rev 1.5   Dec 11 2009 14:53:48   apatel
//Mapping enhancement for travelnews
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Dec 01 2009 13:13:20   mmodi
//Show pan and zoom toolbar, and hide the plan a journey links
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Nov 23 2009 13:20:46   MTurner
//Added key control and tidied up some member variables.
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.2   Nov 11 2009 16:45:42   MTurner
//Further changes for new mapping
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.1   Nov 10 2009 15:09:24   mturner
//Changes for mapping enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.0   Nov 04 2009 13:37:18   mturner
//Initial revision.
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.TravelNews;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Map Web user control, using the ESRI AJAX Mapping components
    /// </summary>
    public partial class MapTravelNewsControl : TDUserControl
    {
        #region Private members

        // Members of MapTravelNewsFilter
        MapTransportType mapTransportType = MapTransportType.All;
        MapIncidentType  mapIncidentType  = MapIncidentType.All;
        MapSeverity      mapSeverity      = MapSeverity.All;
        MapTimePeriod    mapTimePeriod    = MapTimePeriod.Current;
        DateTime         mapDateTime      = DateTime.Now;
        int              mapScale         = -1;
        int              mapScaleLevel    = -1;

        //Member of MapParameters
        OSGridReference mapEnvelopeMin = null;
        OSGridReference mapEnvelopeMax = null;
        OSGridReference mapCentre      = null;

        private const string MAP_ALT_TEXT_REGION_PATTERN = "{REGION}";

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
            RegisterMapAltTextJavaScript();
        }

        #endregion

        #region Initialise Controls
        /// <summary>
        /// Initialises the controls
        /// </summary>
        private void InitialiseControls()
        {

        }
        #endregion

        #region Private Methods

        private void RegisterMapAltTextJavaScript()
        {
            string mapAltText = this.mapControl.MapAltText;
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MapAltText", "InitMapAltText('" + mapAltText + "');", true);
        }

        private void SetMapAltText()
        {
            string mapAltText = GetResource("MapTravelNewsControl.MapAltText");
            if (!string.IsNullOrEmpty(mapAltText))
            {
                mapControl.MapAltText = mapAltText;
            }
            else
            {
                mapControl.MapAltText = string.Empty;
            }
        }

        #endregion
        
        #region Public Methods
        /// <summary>
        /// Sets a map envelope size based on supplied co-ordinates
        /// </summary>
        /// <param name="xMin">Easting for bottom left of map</param>
        /// <param name="yMin">Northing for bottom left of map</param>
        /// <param name="xMax">Easting for top right of map</param>
        /// <param name="yMax">Northing for top right of map</param>
        public void SetEnvelope(int xMin, int yMin, int xMax, int yMax)
        {
            mapEnvelopeMin = new OSGridReference(xMin, yMin);
            mapEnvelopeMax = new OSGridReference(xMax, yMax);

            // Any map centre and scale will override this envelope so set
            // map centre to null.
            mapCentre = null;
            mapScale  = -1;
        }

        /// <summary>
        /// Sets a time for which we want incidents
        /// </summary>
        /// <param name="filterTime">Date time object representing the users choice from the UI</param>
        public void SetIncidentsTimeFilter(DateTime filterTime)
        {
            MapDateTime = filterTime;
            MapTimePeriod = MapTimePeriod.Date;
        }

        /// <summary>
        /// Zooms a map to a given centre point and zoom level
        /// </summary>
        /// <param name="Easting">Easting that the map will be centered upon</param>
        /// <param name="Northing">Northing that the map will be centered upon</param>
        /// <param name="Scale">The scale to draw the map at</param>
        public void ZoomToPoint(int Easting, int Northing, int Scale)
        {
            //Set zoom level to -1
            mapScaleLevel = -1;


            mapScale = Scale;

            mapCentre = new OSGridReference(Easting, Northing);

            // A map centre will override an envelope so to avoid any doubt overwrite
            // any pre-existing envelope.
            mapEnvelopeMax = null;
            mapEnvelopeMin = null;
        }

        /// <summary>
        /// Uses the current filter and parameter values to generate fresh map
        /// </summary>
        public void RefreshMap()
        {
            MapToolbarTool[] mapToolbarTools = new MapToolbarTool[2] {MapToolbarTool.Pan, MapToolbarTool.Zoom};
            MapLocationMode[] mapLocationMode = new MapLocationMode[1] {MapLocationMode.None};

            MapTravelNewsFilter mapTravelNewsFilter = new MapTravelNewsFilter(mapTransportType, mapIncidentType, mapSeverity, mapTimePeriod, mapDateTime);
            MapParameters mapParameters = new MapParameters(mapScale, mapScaleLevel, mapCentre, mapEnvelopeMin, mapEnvelopeMax, null, mapToolbarTools, mapLocationMode);

            mapControl.Initialise(mapParameters, mapTravelNewsFilter);
            SetMapAltText();
        }
        #endregion

        #region Internal Properties
        /// <summary>
        /// Determines which transport types should be shown on the map
        /// </summary>
        internal MapTransportType MapTransportType
        {
            get{return mapTransportType;}
            set{mapTransportType = value;}
        }

        /// <summary>
        /// Determines which incidents should be shown on the map
        /// </summary>
        internal MapIncidentType MapIncidentType
        {
            get {return mapIncidentType; }
            set {mapIncidentType = value; }
        }

        /// <summary>
        /// Determines which severity types should be shown on the map
        /// </summary>
        internal MapSeverity MapSeverity
        {
            get {return mapSeverity; }
            set {mapSeverity = value; }
        }

        /// <summary>
        /// Determines which time period should be shown on the map
        /// </summary>
        internal MapTimePeriod MapTimePeriod
        {
            get {return mapTimePeriod; }
            set {mapTimePeriod = value; }
        }

        /// <summary>
        /// Determines which time should be used for the map
        /// </summary>
        internal DateTime MapDateTime
        {
            get { return mapDateTime; }
            set { mapDateTime = value; }
        }

        /// <summary>
        /// Id of the Map control generated 
        /// </summary>
        internal string MapID
        {
            get
            {
                return mapControl.MapID;
            }

        }
        #endregion
    }
}