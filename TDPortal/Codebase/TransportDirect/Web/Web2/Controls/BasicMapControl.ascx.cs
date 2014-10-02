// *********************************************** 
// NAME                 : BasicMapControl.ascx.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 10/09/2009 
// DESCRIPTION  		: Basic Map control class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/BasicMapControl.ascx.cs-arc  $
//
//   Rev 1.1   Sep 14 2009 15:14:44   apatel
//updated the header logging
//Resolution for 5315: CCN526 Stop Information Page Landing

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common;
using System.Collections;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Basic map user control class
    /// </summary>
    public partial class BasicMapControl : TDUserControl
    {

        #region Page Events

        /// <summary>
        /// Prerender handler. Sets the alternate text for the image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            
            theMap.AlternateText = Global.tdResourceManager.GetString("MapControl.imageMap.AlternateText", TDCultureInfo.CurrentUICulture);

        }

        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            theMap.ServiceName = Properties.Current["InteractiveMapping.Map.ServiceName"];
            theMap.ServerName = Properties.Current["InteractiveMapping.Map.ServerName"];
            base.OnInit(e);
        }

        #endregion

        #region Public properties


        /// <summary>
        /// Write Only property - sets the height of the map
        /// </summary>
        public Unit Height
        {
            set
            {
                theMap.Height = value;
            }
        }

        /// <summary>
        /// Write Only property - sets the width of the map
        /// </summary>
        public Unit Width
        {
            set
            {
                theMap.Width = value;
            }
        }

        /// <summary>
        /// Read/Write property - sets the Stops to dispaly on the map 
        /// </summary>
        public Hashtable StopsVisible
        {
            get
            {
                return theMap.StopsVisible;
            }
            set
            {
                theMap.StopsVisible = value;
            }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Zooms the map to given point and scales it to given scale
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="scale"></param>
        public void ZoomToPointAndScale(double x, double y, int scale)
        {
            theMap.ZoomToPoint(x, y, scale);
        }

        /// <summary>
        /// Adds text to map with they symbol defined
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="mapSymbol">symbol to show</param>
        /// <param name="mapText">Text to display</param>
        public void AddTextToMap(double x, double y, string mapSymbol, string mapText)
        {
            string symbolToShow = "CIRCLE";

            string railPostFix = Properties.Current["Gazetteerpostfix.rail"];
            string coachPostFix = Properties.Current["Gazetteerpostfix.coach"];
            string railcoachPostFix = Properties.Current["Gazetteerpostfix.railcoach"];
            
            if (!string.IsNullOrEmpty(mapSymbol))
            {
                symbolToShow = mapSymbol;
            }

            // Clear any current locations on the map.
            theMap.ClearAddedSymbols();

            // Clear Start/End/Via points.
            theMap.ClearStartEndPoints();

            // Strip out any sub strings denoting pseudo locations
            System.Text.StringBuilder strName = new System.Text.StringBuilder(mapText);
            strName.Replace(railPostFix, "");
            strName.Replace(coachPostFix, "");
            strName.Replace(railcoachPostFix, "");
            mapText = strName.ToString();

            theMap.AddSymbolPoint(x, y, symbolToShow, mapText);

            theMap.Refresh();
        }

        #endregion
    }
}