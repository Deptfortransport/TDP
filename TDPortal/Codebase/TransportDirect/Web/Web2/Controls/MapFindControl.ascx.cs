// *********************************************** 
// NAME                 : MapFindControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 29/10/2009
// DESCRIPTION          : Map control used for Find a map
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapFindControl.ascx.cs-arc  $ 
//
//   Rev 1.11   Apr 09 2010 15:15:28   mmodi
//Updated to only show stop information link when there is only one naptan associated with the location
//Resolution for 5440: Maps - Issue 16 ESRI fixes
//
//   Rev 1.10   Apr 07 2010 13:31:56   pghumra
//Various updates for accessibility purposes
//Resolution for 5491: RS69801 CCN0551 Accessibility - Spaces TfL Map Note London Bus
//
//   Rev 1.9   Mar 19 2010 11:33:34   mmodi
//Add StopInformation link to info popup
//Resolution for 5440: Maps - Issue 16 ESRI fixes
//
//   Rev 1.8   Jan 19 2010 13:20:22   mmodi
//Updates for New Mapping RFC73 patch
//Resolution for 5376: Maps - ESRI RFC073 mandatory changes needed for patch
//
//   Rev 1.7   Nov 27 2009 15:46:04   mmodi
//Updated map toolbar tools shown
//
//   Rev 1.6   Nov 25 2009 15:06:16   mmodi
//Updated with new select location drop down list control
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.5   Nov 18 2009 11:20:38   apatel
//Updated for Journey input planner mapping enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Nov 11 2009 11:51:32   apatel
//updated to return map symbol control client id
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Nov 11 2009 09:55:26   mmodi
//Reset printable session values
//
//   Rev 1.2   Nov 05 2009 14:56:24   apatel
//mapping enhancement code changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.1   Nov 04 2009 11:01:50   mmodi
//Added map symbols control
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.0   Nov 02 2009 17:51:38   mmodi
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
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Map control used for Find a map
    /// </summary>
    public partial class MapFindControl : TDUserControl
    {
        #region Private members

        private TDLocation tdLocation;
        private LocationSearch locationSearch;

        private MapHelper mapHelper = new MapHelper();
        private MapLocationMode[] mapLocationModes =  new MapLocationMode[2] { MapLocationMode.Start, MapLocationMode.End };

        private const string MAP_ALT_TEXT_LOCATION_PATTERN = "{LOCATION}";

        #endregion

        #region Public Properties
        public string MapID
        {
            get
            {
                return mapControl.MapID;
            }

        }

        /// <summary>
        /// Returns client id generated for map symbols select control
        /// </summary>
        public string MapSymbolsSelectID
        {
            get
            {
                return mapSymbolsSelectControl.ClientID;
            }
        }
        #endregion

        #region Initialise

        /// <summary>
        /// Initialises the map find control with the properties specified
        /// This method initialises map with default start and end map location modes
        /// </summary>
        public void Initialise(TDLocation tdLocation, LocationSearch locationSearch)
        {
            this.tdLocation = tdLocation;
            this.locationSearch = locationSearch;
        }

        /// <summary>
        /// Initialises the map find control with the properties specified
        /// Map will be initialised with the map location modes passed
        /// </summary>
        public void Initialise(TDLocation tdLocation, LocationSearch locationSearch, MapLocationMode[] mapLocationModes)
        {
            this.tdLocation = tdLocation;
            this.locationSearch = locationSearch;
            this.mapLocationModes = mapLocationModes;
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
            RegisterMapAltTextJavaScript();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initialises the controls
        /// </summary>
        private void InitialiseControls()
        {
            if ((tdLocation != null) && (tdLocation.Status == TDLocationStatus.Valid))
            {
                // Set the map paramters
                MapParameters mapParameters = mapHelper.GetMapParametersForLocation(tdLocation, locationSearch);
                mapParameters.MapInfoDisplayText = GetResource("MapControl.PlanAJourney.Text");
                mapParameters.MapLocationModes =mapLocationModes;
                mapParameters.MapToolbarTools = new MapToolbarTool[4] { MapToolbarTool.Pan, MapToolbarTool.Zoom, MapToolbarTool.UserDefinedLocation, MapToolbarTool.SelectNearbyLocation };
                
                // Set the location symbols to display
                MapLocationPoint mapLocationPoint = mapHelper.GetMapLocationPoint(tdLocation, MapLocationSymbolType.PushPin, true, true);

                // Add the content for the stop information link to be displayed, but only if there is one stop,
                // because a group station (e.g. London) can display too many links to fit nicely on the 
                // visible area
                if ((tdLocation.NaPTANs != null) && (tdLocation.NaPTANs.Length == 1))
                {
                    string content = mapHelper.GetStopInformationLinks(tdLocation);

                    if (!string.IsNullOrEmpty(content))
                    {
                        mapLocationPoint.Content = content;
                    }
                }

                // Initialise map
                mapControl.Initialise(mapParameters, new MapLocationPoint[1] {mapLocationPoint});
                SetMapAltText(mapLocationPoint.MapLocationDescription);
                // Initialise map symbols
                mapSymbolsSelectControl.MapId = mapControl.MapID;

                // Initialise the select new location
                mapSelectLocationControl.MapId = mapControl.MapID;
                mapSelectLocationControl.MapLocation = mapLocationPoint;
            }
            else
            {
                // No valid location, so initialise a default map
                mapControl.Initialise();

                // Initialise the select new location
                mapSelectLocationControl.MapId = mapControl.MapID;
            }
        }

        private void SetMapAltText(string location)
        {
            string mapAltText = GetResource("MapFindControl.MapAltText");
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