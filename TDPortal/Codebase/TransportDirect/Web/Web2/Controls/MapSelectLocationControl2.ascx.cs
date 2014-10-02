// *********************************************** 
// NAME                 : MapSelectLocationControl2.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 24/11/2009
// DESCRIPTION          : Map select location control used for Find a map. 
//                      : Control is shown and locations list populated using javascript following an action
//                      : on the MapControl2
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapSelectLocationControl2.ascx.cs-arc  $
//
//   Rev 1.2   Nov 27 2009 15:44:08   mmodi
//Removed runat server for dropdownlist
//
//   Rev 1.1   Nov 27 2009 13:20:26   mmodi
//Updated button click javascript
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.0   Nov 25 2009 15:06:48   mmodi
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
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Map select location control used for Find a map
    /// </summary>
    public partial class MapSelectLocationControl2 : TDUserControl
    {
        #region Private members

        // Client ID of the map control to update
        private string mapId = "map";

        // Orignal location to zoom back to if cancel selected on select new location
        private MapLocationPoint mapLocationPoint = null;

        #endregion

        #region Public Properties
        
        /// <summary>
        /// Write only. Sets the Client ID of the map control to allow the 
        /// map select location control to update the map
        /// </summary>
        public string MapId
        {
            set { mapId = value; }
        }

        /// <summary>
        /// Write only. Sets the original location point to be displayed on the map if cancel is clicked
        /// </summary>
        public MapLocationPoint MapLocation
        {
            set { mapLocationPoint = value; }
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

            RegisterJavaScripts();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads resources needed by this control
        /// </summary>
        private void LoadResources()
        {
            labelSelectLocationTitle.Text = GetResource("MapLocationControl.labelSelectLocation.Text");
            labelSelectInstructions.Text = GetResource("MapSelectLocationControl.labelSelectLocation.Text");
            labelSelectLocationInfo.Text = GetResource("MapSelectLocationControl.labelSelectLocationInfo.Text");
            labelSelectLocationError.Text = GetResource("MapSelectLocationControl.labelSelectLocationError.Text");

            buttonOK.ToolTip = GetResource("MapSelectLocationControl.buttonOK.AlternateText");
            buttonOK.Text = GetResource("MapSelectLocationControl.buttonOK.Text");

            buttonCancel.ToolTip = GetResource("MapSelectLocationControl.buttonSelectCancel.AlternateText");
            buttonCancel.Text = GetResource("MapSelectLocationControl.buttonSelectCancel.Text");

            buttonErrorCancel.ToolTip = GetResource("MapSelectLocationControl.buttonSelectCancel.AlternateText");
            buttonErrorCancel.Text = GetResource("MapSelectLocationControl.buttonSelectCancel.Text");
        }

        /// <summary>
        /// Method which adds the javascript to the controls
        /// </summary>
        private void RegisterJavaScripts()
        {
            // Add the button clicks javascript to fire

            // Ok button
            StringBuilder buttonOKClick = new StringBuilder();
            buttonOKClick.Append("return mapSelectLocationOK('" + mapId + "', '");
            buttonOKClick.Append("selectLocationDropDownList" + "', '");
            buttonOKClick.Append(panelSelectLocationInfo.ClientID + "', '");
            buttonOKClick.Append(panelSelectLocationList.ClientID + "', '");
            buttonOKClick.Append(panelSelectLocationError.ClientID);
            buttonOKClick.Append("');");
            buttonOK.OnClientClick = buttonOKClick.ToString();

            // Cancel button
            StringBuilder buttonCancelClick = new StringBuilder();
            buttonCancelClick.Append("return mapSelectLocationCancel('" + mapId + "', '");
            buttonCancelClick.Append("selectLocationDropDownList" + "', '");
            buttonCancelClick.Append(panelSelectLocationInfo.ClientID + "', '");
            buttonCancelClick.Append(panelSelectLocationList.ClientID + "', '");
            buttonCancelClick.Append(panelSelectLocationError.ClientID);

            if (mapLocationPoint != null)
            {
                buttonCancelClick.Append("', '" + mapLocationPoint.MapLocationOSGR.Easting);
                buttonCancelClick.Append("', '" + mapLocationPoint.MapLocationOSGR.Northing);
                buttonCancelClick.Append("', '" + mapLocationPoint.MapLocationDescription);
            }
            
            //buttonCancelClick.Append("');");

            // Slightly different function for cancel and error cancel
            buttonCancel.OnClientClick = buttonCancelClick.ToString() + ("', false);");
            buttonErrorCancel.OnClientClick = buttonCancelClick.ToString() + ("', true);");

            // Determine if javascript is support and determine the JavascriptDom value
            ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

            string scriptName = "MapSelectLocationControl2";

            string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];

            // Register the javascript file
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", scriptRepository.GetScript(scriptName, javaScriptDom));

        }

        #endregion
    }
}