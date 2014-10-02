// *********************************************** 
// NAME                 : MapControl2.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 27/10/2009
// DESCRIPTION          : Map Web user control, using the ESRI AJAX Mapping components
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapControl2.ascx.cs-arc  $ 
//
//   Rev 1.15   Jan 22 2013 10:53:50   mmodi
//Corrected js error
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.14   Jan 17 2013 08:30:30   dlane
//Adding aria attributes
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.13   Jan 04 2013 15:43:16   mmodi
//Allow setting of a map AJAX control id hidden value
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.12   Jun 28 2010 10:28:32   apatel
//Updated to not html encode location point descript as it was showing '&amp;' in text displaying on map instead of '&'
//Resolution for 5563: Issue with map showing '&' characters as '&amp;'
//
//   Rev 1.11   Apr 07 2010 13:31:56   pghumra
//Various updates for accessibility purposes
//Resolution for 5491: RS69801 CCN0551 Accessibility - Spaces TfL Map Note London Bus
//
//   Rev 1.10   Mar 10 2010 15:19:12   apatel
//Updated to show Walkit links on map information popup window when user clicks on start location of walk leg
//Resolution for 5444: CCN 0568 Walkit page land - Phase 1b
//
//   Rev 1.9   Jan 19 2010 13:20:18   mmodi
//Updates for New Mapping RFC73 patch
//Resolution for 5376: Maps - ESRI RFC073 mandatory changes needed for patch
//
//   Rev 1.8   Dec 11 2009 14:37:42   mmodi
//Fixed display of location names which have a ' within the description
//
//   Rev 1.7   Nov 27 2009 13:15:34   mmodi
//Updated to add map journey
//
//   Rev 1.6   Nov 19 2009 12:04:10   mmodi
//Added language flag to mapping javascript
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.5   Nov 11 2009 16:52:50   MTurner
//Changes to the GetTravelNewsFilterText method
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Nov 09 2009 15:44:16   mmodi
//Updated to render custom symbols
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Nov 05 2009 14:56:22   apatel
//mapping enhancement code changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.2   Nov 04 2009 11:00:46   mmodi
//Added travel news filter
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.1   Nov 02 2009 17:50:40   mmodi
//Updated
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.0   Oct 28 2009 12:59:54   mmodi
//Initial revision.
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TransportDirect.Common.DatabaseInfrastructure.Content;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Map Web user control, using the ESRI AJAX Mapping components
    /// </summary>
    public partial class MapControl2 : TDUserControl
    {
        #region Private members

        // Mapping javascript repository files
        private const string scriptMappingScriptRepository = "MappingScriptRepository";

        private const string scriptMappingAPI = "MapAPI";

        // Attributes used for the map initialisation
        private const string MAP_PARAM_SCALE = "param_Scale";
        private const string MAP_PARAM_LEVEL = "param_Level";
        private const string MAP_PARAM_LOCATIONCENTREX = "param_LocationX";
        private const string MAP_PARAM_LOCATIONCENTREY = "param_LocationY";
        private const string MAP_PARAM_X_MIN = "param_XMin";
        private const string MAP_PARAM_Y_MIN = "param_YMin";
        private const string MAP_PARAM_X_MAX = "param_XMax";
        private const string MAP_PARAM_Y_MAX = "param_YMax";
        private const string MAP_PARAM_TEXT = "param_Text";
        private const string MAP_PARAM_TOOLS = "param_Tools";
        private const string MAP_PARAM_MODE = "param_Mode";
        private const string MAP_PARAM_SYMBOLS = "param_Symbols";
        private const string MAP_PARAM_HEIGHT = "param_Height";
        private const string MAP_PARAM_WIDTH = "param_Width";
        private const string MAP_PARAM_TRAVELNEWS = "param_TravelNews";
        private const string MAP_PARAM_ROUTES = "param_Routes";

        // Properties used for the map attributes
        private MapParameters mapParameters;
        private MapLocationPoint[] mapLocationPoint;
        private MapJourney[] mapJourney;
        private MapTravelNewsFilter mapTravelNewsFilter;
        private string mapAltText = string.Empty;
        private string mapAJAXControlId = string.Empty;

        
        #endregion

        #region Initialise

        /// <summary>
        /// Initialises a default map control
        /// </summary>
        public void Initialise()
        {
            ResetControl();
        }

        /// <summary>
        /// Initialises the map control with the properties specified
        /// </summary>
        /// <param name="mapParameters">Map parameters to initialise the map - Null for default map</param>
        /// <param name="mapLocationPoint">Location points to display on map - Null or empty array to show no points</param>
        public void Initialise(MapParameters mapParameters, MapLocationPoint[] mapLocationPoint)
        {
            ResetControl();

            this.mapParameters = mapParameters;
            this.mapLocationPoint = mapLocationPoint;
        }

        /// <summary>
        /// Initialises the map control with the properties specified
        /// </summary>
        /// <param name="mapParameters">Map parameters to initialise the map - Null for default map</param>
        /// <param name="mapLocationPoint">Location points to display on map - Null or empty array to show no points</param>
        /// <param name="mapJourney">Journeys to display on map - Null or empty array to show no journeys</param>
        public void Initialise(MapParameters mapParameters, MapLocationPoint[] mapLocationPoint, MapJourney[] mapJourney)
        {
            ResetControl();

            this.mapParameters = mapParameters;
            this.mapLocationPoint = mapLocationPoint;
            this.mapJourney = mapJourney;
        }

        /// <summary>
        /// Initialises the map control with the properties specified
        /// </summary>
        /// <param name="mapParameters">Map parameters to initialise the map - Null for default map</param>
        /// <param name="mapTravelNewsFilter">Map travel news filter to show travel news incidents - Null to show no travel news incidents</param>
        public void Initialise(MapParameters mapParameters, MapTravelNewsFilter mapTravelNewsFilter)
        {
            ResetControl();

            this.mapParameters = mapParameters;
            this.mapTravelNewsFilter = mapTravelNewsFilter;
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
            RegisterMapJavascript();
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupControlPageId();

            SetupControlAJAXId();

            UpdateMapAttributes();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Updates the map div attributes with the values defined in this controls properties
        /// to initialise the map
        /// </summary>
        private void UpdateMapAttributes()
        {
            if (mapControl != null)
            {
                ClearMapAttributes();

                // Only add attributes if valid
                #region Add attributes

                #region Map properties

                // Scale
                if ((mapParameters.MapScale >= 1) && (mapParameters.MapScale <= 10000000))
                {
                    mapControl.Attributes.Add(MAP_PARAM_SCALE, mapParameters.MapScale.ToString());
                }

                if ((mapParameters.MapScaleLevel >= 1) && (mapParameters.MapScaleLevel <= 13))
                {
                    mapControl.Attributes.Add(MAP_PARAM_LEVEL, mapParameters.MapScaleLevel.ToString());
                }

                // Centre/area
                if ((mapParameters.MapCentre != null) && (mapParameters.MapCentre.IsValid))
                {
                    mapControl.Attributes.Add(MAP_PARAM_LOCATIONCENTREX, mapParameters.MapCentre.Easting.ToString());
                    mapControl.Attributes.Add(MAP_PARAM_LOCATIONCENTREY, mapParameters.MapCentre.Northing.ToString());
                }

                if ((mapParameters.MapEnvelopeMin != null) && (mapParameters.MapEnvelopeMax != null) &&
                    (mapParameters.MapEnvelopeMin.IsValid && mapParameters.MapEnvelopeMax.IsValid))
                {
                    mapControl.Attributes.Add(MAP_PARAM_X_MIN, mapParameters.MapEnvelopeMin.Easting.ToString());
                    mapControl.Attributes.Add(MAP_PARAM_Y_MIN, mapParameters.MapEnvelopeMin.Northing.ToString());
                    mapControl.Attributes.Add(MAP_PARAM_X_MAX, mapParameters.MapEnvelopeMax.Easting.ToString());
                    mapControl.Attributes.Add(MAP_PARAM_Y_MAX, mapParameters.MapEnvelopeMax.Northing.ToString());
                }


                // Tools
                if ((mapParameters.MapToolbarTools != null) && (mapParameters.MapToolbarTools.Length > 0))
                {
                    string toolbarTools = GetMapToolbarText(mapParameters.MapToolbarTools);
                    
                    if (toolbarTools.Length > 0)
                    {
                        mapControl.Attributes.Add(MAP_PARAM_TOOLS, toolbarTools );
                    }
                }

                // Map modes
                if ((mapParameters.MapLocationModes != null) && (mapParameters.MapLocationModes.Length >= 0))
                {
                    string mapModes = GetMapModesText(mapParameters.MapLocationModes);

                    if (mapModes.Length >= 0)
                    {
                        mapControl.Attributes.Add(MAP_PARAM_MODE, mapModes);
                    }
                }

                // Info popup text
                if (!string.IsNullOrEmpty(mapParameters.MapInfoDisplayText))
                {
                    mapControl.Attributes.Add(MAP_PARAM_TEXT, mapParameters.MapInfoDisplayText);
                }

                // Dimensions
                if (mapParameters.MapHeight >= 1)
                {
                    mapControl.Attributes.Add(MAP_PARAM_HEIGHT, mapParameters.MapHeight.ToString());
                }

                if (mapParameters.MapWidth >= 1)
                {
                    mapControl.Attributes.Add(MAP_PARAM_WIDTH, mapParameters.MapWidth.ToString());
                }

                #endregion

                #region Map journeys

                // Journeys to display
                if ((mapJourney != null) && (mapJourney.Length > 0))
                {
                    string journeyRoutes = GetMapJourneyRouteText(mapJourney);

                    if (journeyRoutes.Length > 0)
                    {
                        mapControl.Attributes.Add(MAP_PARAM_ROUTES, journeyRoutes);
                    }
                }

                #endregion

                #region Map location symbols

                // Location point symbols
                if ((mapLocationPoint != null) && (mapLocationPoint.Length > 0))
                {
                    string locationPoints = GetMapLocationPointsText(mapLocationPoint);

                    if (locationPoints.Length > 0)
                    {
                        mapControl.Attributes.Add(MAP_PARAM_SYMBOLS, locationPoints);
                    }
                }

                #endregion

                #region Map travel news incidents 

                // Travel news filter
                if ((mapTravelNewsFilter != null) && (mapTravelNewsFilter.IsValid))
                {
                    string travelNewsFilter = GetTravelNewsFilterText(mapTravelNewsFilter);

                    if (travelNewsFilter.Length > 0)
                    {
                        mapControl.Attributes.Add(MAP_PARAM_TRAVELNEWS, travelNewsFilter);
                    }
                }

                #endregion

                #endregion
            }
        }

        /// <summary>
        /// Method registers the Mapping javascript repository needed on the page
        /// </summary>
        private void RegisterMapJavascript()
        {
            // Register the scripts needed only if user has Javascript enabled
            TDPage thePage = this.Page as TDPage;

            if (thePage != null && thePage.IsJavascriptEnabled)
            {
                // Get the global script repository
                ScriptRepository.ScriptRepository repository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

                // Register the mapping repository
                string mappingRepostitoryScript = repository.GetScript(scriptMappingScriptRepository, thePage.JavascriptDom);
                
                // If language is Welsh, need to append the Welsh indicator to the end of this mapping repository script
                switch (CurrentLanguage.Value)
                {
                    case Language.Welsh:
                        // Add the Welsh indicator
                        mappingRepostitoryScript = string.Format(mappingRepostitoryScript, "?language=cy");
                        break;
                    default:
                        // Default to English, no need to append any language indicator
                        mappingRepostitoryScript = string.Format(mappingRepostitoryScript, string.Empty);
                        break;
                }

                thePage.ClientScript.RegisterClientScriptBlock(this.GetType(), scriptMappingScriptRepository, mappingRepostitoryScript );

                // Register the mapping api call script
                thePage.ClientScript.RegisterClientScriptBlock(this.GetType(), "JQuery", repository.GetScript("JQuery", thePage.JavascriptDom));
                thePage.ClientScript.RegisterClientScriptBlock(this.GetType(), "Common", repository.GetScript("Common", thePage.JavascriptDom));
                thePage.ClientScript.RegisterClientScriptBlock(this.GetType(), scriptMappingAPI, repository.GetScript(scriptMappingAPI, thePage.JavascriptDom));
            }
        }

        /// <summary>
        /// Adds current page id as a hidden field
        /// The hidden current page id used by map api functions to pass current page id to map api web services
        /// </summary>
        private void SetupControlPageId()
        {
            HiddenField hdnPageId = new HiddenField();

            hdnPageId.ID = string.Format("{0}_PageId", mapControl.ID);

            hdnPageId.Value = ((TDPage)Page).PageId.ToString();

            this.Controls.Add(hdnPageId);
        }

        /// <summary>
        /// Adds ajax container id as a hidden field.
        /// The hiddent ajax id is used by the map api to display the map using javascript following an ajax postback
        /// </summary>
        private void SetupControlAJAXId()
        {
            if (!string.IsNullOrEmpty(mapAJAXControlId))
            {
                HiddenField hdnAJAXId = new HiddenField();

                hdnAJAXId.ID = string.Format("{0}_AJAXId", mapControl.ID);

                hdnAJAXId.Value = mapAJAXControlId;

                this.Controls.Add(hdnAJAXId);
            }
        }

        #region Helper methods

        /// <summary>
        /// Resets all control properties and map attributes
        /// </summary>
        private void ResetControl()
        {
            // Reset map parameters
            mapParameters = new MapParameters();
            mapLocationPoint = new MapLocationPoint[0];
            mapJourney = new MapJourney[0];
            mapTravelNewsFilter = new MapTravelNewsFilter();

            // Reset map control
            ClearMapAttributes();
        }

        /// <summary>
        /// Removes the map div attributes used for intialising the map
        /// </summary>
        private void ClearMapAttributes()
        {
            if (mapControl != null)
            {
                mapControl.Attributes.Remove(MAP_PARAM_SCALE);
                mapControl.Attributes.Remove(MAP_PARAM_LEVEL);
                mapControl.Attributes.Remove(MAP_PARAM_LOCATIONCENTREX);
                mapControl.Attributes.Remove(MAP_PARAM_LOCATIONCENTREY);
                mapControl.Attributes.Remove(MAP_PARAM_X_MIN);
                mapControl.Attributes.Remove(MAP_PARAM_Y_MIN);
                mapControl.Attributes.Remove(MAP_PARAM_X_MAX);
                mapControl.Attributes.Remove(MAP_PARAM_Y_MAX);
                mapControl.Attributes.Remove(MAP_PARAM_TEXT);
                mapControl.Attributes.Remove(MAP_PARAM_TOOLS);
                mapControl.Attributes.Remove(MAP_PARAM_MODE);
                mapControl.Attributes.Remove(MAP_PARAM_SYMBOLS);
                mapControl.Attributes.Remove(MAP_PARAM_HEIGHT);
                mapControl.Attributes.Remove(MAP_PARAM_WIDTH);
                mapControl.Attributes.Remove(MAP_PARAM_TRAVELNEWS);
                mapControl.Attributes.Remove(MAP_PARAM_ROUTES);
            }
        }

        /// <summary>
        /// Returns the Toolbar Tools text formatted for the map control
        /// </summary>
        /// <param name="tool"></param>
        /// <returns></returns>
        private string GetMapToolbarText(MapToolbarTool[] mapToolbarTools)
        {
            string tools = string.Empty;

            foreach (MapToolbarTool tool in mapToolbarTools)
            {
                switch (tool)
                {
                    case MapToolbarTool.SelectNearbyLocation:
                        tools += "selectNearby";
                        tools += ",";
                        break;
                    case MapToolbarTool.UserDefinedLocation:
                        tools += "userDefined";
                        tools += ",";
                        break;
                    case MapToolbarTool.Zoom:
                        tools += "zoom";
                        tools += ",";
                        break;
                    case MapToolbarTool.Pan:
                        tools += "pan";
                        tools += ",";
                        break;
                }
            }

            // If the tools text is set, remove the last ","
            if (tools.Length > 0)
            {
                tools = tools.TrimEnd(',');
            }

            return tools;
        }

        /// <summary>
        /// Returns the Modes text formatted for the map control
        /// </summary>
        /// <param name="tool"></param>
        /// <returns></returns>
        private string GetMapModesText(MapLocationMode[] mapLocationModes)
        {
            string modes = string.Empty;

            foreach (MapLocationMode mode in mapLocationModes)
            {
                switch (mode)
                {
                    case MapLocationMode.Start:
                        modes += "start";
                        modes += ",";
                        break;
                    case MapLocationMode.Via:
                        modes += "via";
                        modes += ",";
                        break;
                    case MapLocationMode.End:
                        modes += "end";
                        modes += ",";
                        break;
                    case MapLocationMode.None:
                        modes += "none";
                        modes += ",";
                        break;
                }
            }

            // If the modes text is set, remove the last ","
            if (modes.Length > 0)
            {
                modes = modes.TrimEnd(',');
            }

            return modes;
        }

        /// <summary>
        /// Returns the Location Point symbols text formatted for the map control
        /// </summary>
        /// <returns></returns>
        private string GetMapLocationPointsText(MapLocationPoint[] mapLocationPoints)
        {
            StringBuilder locationPoints = new StringBuilder();

            // Go through each location point building up the text required by map
            foreach (MapLocationPoint locationPoint in mapLocationPoints)
            {
                // Only add valid location points
                if (locationPoint.MapLocationOSGR.IsValid)
                {
                    locationPoints.Append("{ ");

                    // coordinate
                    locationPoints.Append("x:" + locationPoint.MapLocationOSGR.Easting.ToString() + ", ");
                    locationPoints.Append("y:" + locationPoint.MapLocationOSGR.Northing.ToString() + ", ");

                    // description
                    string description = locationPoint.MapLocationDescription;
                    // have to manually replace ' because HtmlEncode does not encode this, can't use &#39; because
                    // the & gets converted to &amp; when adding the attribute to the div - which therefore
                    // breaks the ' encoding. So have to escape the ' using \'
                    description = description.Replace("'", "\\'");
                    
                    locationPoints.Append("label:'" + description + "', ");

                    // symbol
                    switch (locationPoint.MapLocationSymbol)
                    {
                        case MapLocationSymbolType.Circle:
                            locationPoints.Append("type:'symbol', symbolKey:'CIRCLE'");
                            break;
                        case MapLocationSymbolType.Square:
                            locationPoints.Append("type:'symbol', symbolKey:'SQUARE'");
                            break;
                        case MapLocationSymbolType.Triangle:
                            locationPoints.Append("type:'symbol', symbolKey:'TRIANGLE'");
                            break;
                        case MapLocationSymbolType.Diamond:
                            locationPoints.Append("type:'symbol', symbolKey:'DIAMOND'");
                            break;
                        case MapLocationSymbolType.PushPin:
                            locationPoints.Append("type:'symbol', symbolKey:'PUSHPIN'");
                            break;
                        case MapLocationSymbolType.Ferry:
                            locationPoints.Append("type:'symbol', symbolKey:'FERRY'");
                            break;
                        case MapLocationSymbolType.Toll:
                            locationPoints.Append("type:'symbol', symbolKey:'TOLL'");
                            break;
                        case MapLocationSymbolType.Start:
                            locationPoints.Append("type:'start'");
                            break;
                        case MapLocationSymbolType.Via:
                            locationPoints.Append("type:'via'");
                            break;
                        case MapLocationSymbolType.End:
                            locationPoints.Append("type:'end'");
                            break;
                        case MapLocationSymbolType.Custom:
                            locationPoints.Append("type:'symbol', symbolKey:'" + locationPoint.MapSymbol + "'");
                            break;
                    }

                    // should the location point allow an info popup
                    if (locationPoint.MapInfoPopupRequired)
                    {
                        locationPoints.Append(", infoWindowRequired:true");

                        // set up and add custom content to be shown in the popup
                        if (!string.IsNullOrEmpty(locationPoint.Content))
                        {
                            locationPoints.Append(", content:'" + locationPoint.Content+ "'");
                        }

                        // display the info popup on the initial map
                        if (locationPoint.MapShowPopup)
                        {
                            locationPoints.Append(", main:true");
                        }
                    }
                    else
                    {
                        locationPoints.Append(", infoWindowRequired:false");
                    }
                    
                    locationPoints.Append(" },");
                }
            }
                        
            // If the location points text is set, remove the last "," and put in to the format
            // required by the map
            if (locationPoints.ToString().Length > 0)
            {
                string locations = locationPoints.ToString();

                return "[ " + locations.TrimEnd(',') + " ]";
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Returns the Journey routes text formatted to add to the map control
        /// </summary>
        /// <returns></returns>
        private string GetMapJourneyRouteText(MapJourney[] mapJourneys)
        {
            StringBuilder journeyRoutes = new StringBuilder();

            // Go through each journey building up the text required by map
            foreach (MapJourney mapJourney in mapJourneys)
            {
                journeyRoutes.Append("{");

                journeyRoutes.Append("sessionId:'" + mapJourney.SessionId + "'," );
                journeyRoutes.Append("routeNumber:" + mapJourney.RouteNumber + ",");
                journeyRoutes.Append("type:'" + GetMapJourneyTypeString(mapJourney.MapJourneyType) + "'");

                journeyRoutes.Append("},");
            }

            // If the text is set, remove the last "," and put in to the format
            // required by the map
            if (journeyRoutes.ToString().Length > 0)
            {
                string journeys = journeyRoutes.ToString();

                return "[ " + journeys.TrimEnd(',') + " ]";
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Returns the Travel new filter text formatted for the map control
        /// </summary>
        /// <param name="mapTravelNewsFilter"></param>
        /// <returns></returns>
        private string GetTravelNewsFilterText(MapTravelNewsFilter mapTravelNewsFilter)
        {
            StringBuilder travelNewsFilter = new StringBuilder();

            string transportType = string.Empty;
            string incidentType = string.Empty;
            string severity = string.Empty;
            string timePeriod = string.Empty;
            string dateTime = string.Empty;

            #region Convert travel news filter enums into strings

            switch (mapTravelNewsFilter.MapTransportType)
            {
                case MapTransportType.All:
                    transportType = "all";
                    break;
                case MapTransportType.None:
                    transportType = "none";
                    break;
                case MapTransportType.Public:
                    transportType = "public";
                    break;
                case MapTransportType.Road:
                    transportType = "road";
                    break;
            }

            switch (mapTravelNewsFilter.MapIncidentType)
            {
                case MapIncidentType.All:
                    incidentType = "all";
                    break;
                case MapIncidentType.Planned:
                    incidentType = "planned";
                    break;
                case MapIncidentType.Unplanned:
                    incidentType = "unplanned";
                    break;
            }

            switch (mapTravelNewsFilter.MapSeverity)
            {
                case MapSeverity.All:
                    severity = "all";
                    break;
                case MapSeverity.Major:
                    severity = "major";
                    break;
            }

            switch (mapTravelNewsFilter.MapTimePeriod)
            {
                case MapTimePeriod.Current:
                    timePeriod = "current";
                    break;
                case MapTimePeriod.Date:
                    timePeriod = "date";
                    break;
                case MapTimePeriod.DateTime:
                    timePeriod = "datetime";
                    break;
                case MapTimePeriod.Recent:
                    timePeriod = "recent";
                    break;
            }

            #endregion
            
            // Convert date time in to the required format
            if (mapTravelNewsFilter.MapDateTime != null)
            {
                dateTime = mapTravelNewsFilter.MapDateTime.ToString("dd/MM/yy HH:mm");
            }

            // Build the travel news filter string
            travelNewsFilter.Append("transportType:" + transportType + ",");
            travelNewsFilter.Append("incidentType:" + incidentType + ",");
            travelNewsFilter.Append("severity:" + severity + ",");
            travelNewsFilter.Append("timePeriod:" + timePeriod);

            // Date time is only needed if time period is set to Date or DateTime
            if ((!string.IsNullOrEmpty(dateTime))
                && ((mapTravelNewsFilter.MapTimePeriod == MapTimePeriod.Date) || 
                    (mapTravelNewsFilter.MapTimePeriod == MapTimePeriod.DateTime))
               ) 
            {
                travelNewsFilter.Append(",datetime:" + dateTime);
            }
            return travelNewsFilter.ToString();
        }
        
        #endregion

        #endregion

        #region Public Properties

        /// <summary>
        /// Read only. Returns the ClientID of the Map
        /// </summary>
        public string MapID
        {
            get { return mapControl.ClientID; }
        }

        /// <summary>
        /// Read/Write. The map parameters to use for intialising the map
        /// </summary>
        public MapParameters MapParameters
        {
            get { return mapParameters; }
            set { mapParameters = value; }
        }

        /// <summary>
        /// Read/Write. The map journeys to add on to the map
        /// </summary>
        public MapJourney[] MapJourneys
        {
            get { return mapJourney; }
            set { mapJourney = value; }
        }

        /// <summary>
        /// Read/Write. The map location points to add on to the map
        /// </summary>
        public MapLocationPoint[] MapLocationPoints
        {
            get { return mapLocationPoint; }
            set { mapLocationPoint = value; }
        }

        /// <summary>
        /// Read/Write. The map travel news filter to use on the map
        /// </summary>
        public MapTravelNewsFilter MapTravelNewsFilter
        {
            get { return mapTravelNewsFilter; }
            set { mapTravelNewsFilter = value; }
        }

        /// <summary>
        /// Read/Write. The map alt text
        /// </summary>
        public string MapAltText
        {
            get { return mapAltText; }
            set { mapAltText = value; }
        }

        /// <summary>
        /// Read/Write. Map AJAX control id.
        /// This is the control Id containing the parent map control, and is used where 
        /// AJAX page postbacks initialise a new map to be displayed. The javascript then
        /// checks for this container control before creating the map within it
        /// </summary>
        public string MapAJAXControlId
        {
            get { return mapAJAXControlId; }
            set { mapAJAXControlId = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to return the string used on the MapControl2 initialisation parameters for a MapJourneyType 
        /// </summary>
        /// <param name="mapJourneyType"></param>
        /// <returns></returns>
        public string GetMapJourneyTypeString(MapJourneyType mapJourneyType)
        {
            switch (mapJourneyType)
            {
                case MapJourneyType.Cycle:
                    return "Cycle";
                case MapJourneyType.PublicTransport:
                    return "PT";
                case MapJourneyType.Road:
                    return "Road";
            }

            return string.Empty;
        }

        #endregion
    }
}