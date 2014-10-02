// *********************************************** 
// NAME                 : TrackingControl.ascx.cs 
// AUTHOR               : Parvez Ghumra
// DATE CREATED         : 05/03/2010
// DESCRIPTION			: A custom control to add intellitracker's visitor tag 
//                        javascript on the page. Also, adds customised parameters 
//                        added through tracking control helper class.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TrackingControl.ascx.cs-arc  $
//
//   Rev 1.6   Apr 22 2010 11:53:56   pghumra
//Minor change to some logic following code review
//Resolution for 5459: Update soft content for Intellitacker tracking URL in tag
//
//   Rev 1.5   Mar 05 2010 06:03:52   apatel
//Added comments and header
//Resolution for 5402: Add Intellitracker tag to all TDP web pages

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Tracking Control
    /// </summary>
    public partial class TrackingControl : TDPrintableUserControl
    {
        #region Page Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            loadTrackingTag();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Checks properties to determin if visitor tag should be injected to page.
        /// If true gets the visitor tag content from the content database and 
        /// registers it through page's RegisterclientScriptBlock method
        /// </summary>
        private void loadTrackingTag()
        {
            string tracking = Properties.Current["TrackingControl.IncludeTag"];

            bool trackingEnabled = false;

            if (string.IsNullOrEmpty(tracking))
            {
                trackingEnabled = false;
            }
            else
            {
                if (!bool.TryParse(tracking, out trackingEnabled))
                {
                    trackingEnabled = false;
                }
            }

            if (trackingEnabled)
            {
                string tagContent = GetResource("TrackingControl.TagContent");
                tagContent = AddParamaters(tagContent);
                
                //Register the script
                if (!this.Page.ClientScript.IsClientScriptBlockRegistered("TrackingScript"))
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "TrackingScript", tagContent, false);
                }

            }
            else
            {
                this.Visible = false;
            }
            
        }

        /// <summary>
        /// Adds customised parameters added to the tracking control class during page cycle to intellitracker's visitor tag script
        /// </summary>
        /// <param name="tagContent">Intellitracker visitor tag script</param>
        /// <returns>Intellitracker visitor tag with customised parameters added to it</returns>
        private string AddParamaters(string tagContent)
        {
            TrackingControlHelper trackingHelper = new TrackingControlHelper();

            string parameterList = trackingHelper.GetTrackingParameterString();
            
            //clear out the tracking key value pair stored in session after replacing in the intellitracker script
            trackingHelper.ClearTrackingParameters();

            if (!string.IsNullOrEmpty(parameterList))
            {
                return tagContent.Replace("iAddPAR", parameterList);
            }
            else
            {
                return tagContent;
            }
        }

        #endregion
    }
}