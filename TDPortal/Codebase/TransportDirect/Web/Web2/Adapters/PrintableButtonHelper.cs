// *************************************************************************** 
// NAME                 : PrintableButtonHelper.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 05/11/2009
// DESCRIPTION			: Helper class to generate client script to save map url for printer friendly
// *************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/PrintableButtonHelper.cs-arc  $
//
//   Rev 1.6   Feb 02 2010 16:38:02   pghumra
//Fixed multiple page refresh issues for printer friendly maps.
//Resolution for 5395: CODE FIX - INITIAL - DEL 10.x - Del 10.9.1 Bug printer friendly
//
//   Rev 1.5   Dec 10 2009 11:05:12   mmodi
//Corrected Cycle print javascript
//
//   Rev 1.4   Nov 16 2009 17:07:00   apatel
//Updated for mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Nov 11 2009 13:51:34   mmodi
//Removed redundant variable and check for empty outward map
//
//   Rev 1.2   Nov 11 2009 11:01:04   apatel
//updated comments
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.1   Nov 11 2009 10:52:12   apatel
//updated to pass mapsymbols control id and mapviewtype dropdown id in client script
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.0   Nov 05 2009 15:02:58   apatel
//Initial revision.
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps

using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.Web.Adapters
{
    public class PrintableButtonHelper
    {
        #region Private Fields
        private int mapHeight = 500;
        private int mapWidth = 754;
        private string mapIdOutward;
        private string mapIdReturn;
        private string mapSymbolControlIdOutward = string.Empty;
        private string mapSymbolControlIdReturn = string.Empty;
        private string mapViewTypeCtrlOutward = string.Empty;
        private string mapViewTypeCtrlReturn = string.Empty;
       
        #endregion

        #region Constructors
        /// <summary>
        /// PrintableButtonHelper constructor initializing helper with only outward map id
        /// </summary>
        /// <param name="mapIdOutward">Map client id</param>
        public PrintableButtonHelper(string mapIdOutward)
            : this(mapIdOutward, null)
        {
        }

        /// <summary>
        /// PrintableButtonHelper constructor initializing helper with outward map id and outward symbol control id
        /// </summary>
        /// <param name="mapIdOutward">Map client id</param>
        /// <param name="mapSymbolControlIdOutward">Map symbol control client id</param>
        public PrintableButtonHelper(string mapIdOutward, string mapSymbolControlIdOutward)
            : this(mapIdOutward, null, mapSymbolControlIdOutward, null, null,null)
        {
        }

        /// <summary>
        /// PrintableButtonHelper constructor initializing helper with outward and return map id with symbol control ids and view type control ids
        /// </summary>
        /// <param name="mapIdOutward">Outward map client id</param>
        /// <param name="mapIdReturn">Return map client id</param>
        /// <param name="mapSymbolControlIdOutward">Outward map symbol control client id</param>
        /// <param name="mapSymbolControlIdReturn">Return map symbol control client id</param>
        /// <param name="mapViewTypeCtrlOutward">Outward map view type control client id</param>
        /// <param name="mapViewTypeCtrlReturn">Return map view type control client id</param>
        public PrintableButtonHelper(string mapIdOutward, string mapIdReturn, string mapSymbolControlIdOutward, string mapSymbolControlIdReturn, string mapViewTypeCtrlOutward, string mapViewTypeCtrlReturn)
        {
            this.mapIdOutward = mapIdOutward;
            this.mapIdReturn = mapIdReturn;
            this.mapSymbolControlIdOutward = string.IsNullOrEmpty(mapSymbolControlIdOutward) ? string.Empty : mapSymbolControlIdOutward;
            this.mapSymbolControlIdReturn = string.IsNullOrEmpty(mapSymbolControlIdReturn) ? string.Empty : mapSymbolControlIdReturn;
            this.mapViewTypeCtrlOutward = string.IsNullOrEmpty(mapViewTypeCtrlOutward) ? string.Empty : mapViewTypeCtrlOutward;
            this.mapViewTypeCtrlReturn = string.IsNullOrEmpty(mapViewTypeCtrlReturn) ? string.Empty : mapViewTypeCtrlReturn;
        }

        /// <summary>
        /// PrintableButtonHelper constructor initializing helper with only outward map id, outward symbol control id, 
        /// outward view type control id together with map height and width
        /// </summary>
        /// <param name="mapIdOutward">Map client id</param>
        /// <param name="mapSymbolControlIdOutward">Outward map symbol control client id</param>
        /// <param name="mapViewTypeCtrlOutward">Outward map view type control client id</param>
        /// <param name="mapHeight">Map height</param>
        /// <param name="mapWidth">Map width</param>
        public PrintableButtonHelper(string mapIdOutward, string mapSymbolControlIdOutward, string mapViewTypeCtrlOutward, int mapHeight, int mapWidth)
            : this(mapIdOutward, null, mapSymbolControlIdOutward, null, mapViewTypeCtrlOutward, null, mapHeight, mapWidth)
        {
        }

        /// <summary>
        /// PrintableButtonHelper constructor initializing helper with  outward  and return map id and map height and width
        /// </summary>
        /// <param name="mapIdOutward">Outward map client id</param>
        /// <param name="mapIdReturn">Return map client id</param>
        /// <param name="mapSymbolControlIdOutward">Outward map symbol control client id</param>
        /// <param name="mapSymbolControlIdReturn">Return map symbol control client id</param>
        /// <param name="mapViewTypeCtrlOutward">Outward map view type control client id</param>
        /// <param name="mapViewTypeCtrlReturn">Return map view type control client id</param>
        /// <param name="mapHeight">Map height</param>
        /// <param name="mapWidth">Map width</param>
        public PrintableButtonHelper(string mapIdOutward, string mapIdReturn, string mapSymbolControlIdOutward, string mapSymbolControlIdReturn, string mapViewTypeCtrlOutward, string mapViewTypeCtrlReturn, int mapHeight, int mapWidth)
            : this(mapIdOutward, mapIdReturn, mapSymbolControlIdOutward, mapSymbolControlIdReturn, mapViewTypeCtrlOutward, mapViewTypeCtrlReturn)
        {
            this.mapHeight = mapHeight;
            this.mapWidth = mapWidth;

        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns a client script for printable button to save map state
        /// </summary>
        /// <returns></returns>
        public string GetClientScript()
        {
            string outwardScript = string.Empty;

            string returnScript = string.Empty;

            int imageResolution = Convert.ToInt32(Properties.Current["InteractiveMapping.MapImageResolution"]);

            if (!string.IsNullOrEmpty(mapIdOutward))
            {
                outwardScript = string.Format("setMapViewState('{0}',{1},{2},{3},'{4}','{5}',true);", mapIdOutward, mapWidth, mapHeight, imageResolution, mapSymbolControlIdOutward, mapViewTypeCtrlOutward);
            }

            if (!string.IsNullOrEmpty(mapIdReturn))
            {
                returnScript = string.Format("setMapViewState('{0}',{1},{2},{3},'{4}','{5}',false);", mapIdReturn, mapWidth, mapHeight, imageResolution, mapSymbolControlIdReturn, mapViewTypeCtrlReturn);
            }

            return string.Format("{0}{1}", outwardScript, returnScript);

        }

        /// <summary>
        /// Returns a client script for printable button to save map state for cycle journey
        /// The map tiles return is based on the scale calculated based on no of tiles nearer to target no of tiles.
        /// </summary>
        /// <param name="defaultTileScale">default tile scale</param>
        /// <param name="targetNoOfTiles">number of map tiles we want to aim for</param>
        /// <returns></returns>
        public string GetCycleMapClientScript(double defaultTileScale, int targetNoOfTiles, bool outward)
        {
            string outwardScript = string.Empty;

            string returnScript = string.Empty;

            int imageResolution = Convert.ToInt32(Properties.Current["InteractiveMapping.MapImageResolution"]);

            if ((outward) && (!string.IsNullOrEmpty(mapIdOutward)))
            {
                outwardScript = string.Format("setMapTileViewState('{0}',{1},{2},{3},{4},{5},'{6}','{7}',true);", mapIdOutward, mapWidth, mapHeight, imageResolution, defaultTileScale, targetNoOfTiles, mapSymbolControlIdOutward, mapViewTypeCtrlOutward);
            }

            if ((!outward) && (!string.IsNullOrEmpty(mapIdReturn)))
            {
                returnScript = string.Format("setMapTileViewState('{0}',{1},{2},{3},{4},{5},'{6}','{7}',false);", mapIdReturn, mapWidth, mapHeight, imageResolution, defaultTileScale, targetNoOfTiles, mapSymbolControlIdReturn, mapViewTypeCtrlReturn);
            }

            return string.Format("{0}{1}", outwardScript, returnScript);

        }
        #endregion
    }
}
