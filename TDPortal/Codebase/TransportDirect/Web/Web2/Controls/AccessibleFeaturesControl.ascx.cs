// *********************************************** 
// NAME             : AccessibleFeaturesControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 21/11/2012
// DESCRIPTION  	: Control to display the series of accessible facilities
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AccessibleFeaturesControl.ascx.cs-arc  $
//
//   Rev 1.0   Dec 05 2012 14:27:16   mmodi
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Control to display the series of accessible facilities
    /// </summary>
    public partial class AccessibleFeaturesControl : TDUserControl
    {
        #region Private members

        private List<AccessibilityType> accessbilityFeatures = null;

        #endregion

        #region Page_Load, Page_PreRender

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            LocalResourceManager = TDResourceManager.JOURNEY_RESULTS_RM;
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            List<VehicleFeatureIcon> accessibleFeatureIcons = GetAccessibleFeatureIcons(accessbilityFeatures);

            //Check that there are icons for accessibility
            if (accessibleFeatureIcons.Count > 0)
            {
                //Set Repeater DataSource and DataBind to the array of icons
                rptrAccessibleFeatures.DataSource = accessibleFeatureIcons;
                rptrAccessibleFeatures.DataBind();
            }
            else
            {
                //No icons, hide the entire control
                this.Visible = false;
            }
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Event handler for the Accessible Features repeater item data bound event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptrAccessibleFeatures_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                VehicleFeatureIcon fi = (VehicleFeatureIcon)e.Item.DataItem;

                if (fi != null)
                {
                    #region Image

                    Image imgAccessibleFeature = (Image)e.Item.FindControl("imgAccessibleFeature");

                    if (imgAccessibleFeature != null)
                    {
                        // Set image values
                        imgAccessibleFeature.ImageUrl = GetResource(fi.ImageUrlResource);
                        imgAccessibleFeature.AlternateText = GetResource(fi.AltTextResource);
                        imgAccessibleFeature.ToolTip = GetResource(fi.ToolTipResource);
                                                
                        // Hide image if no image url exists
                        if (string.IsNullOrEmpty(imgAccessibleFeature.ImageUrl))
                        {
                            imgAccessibleFeature.Visible = false;
                        }
                    }

                    #endregion
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialise method
        /// </summary>
        public void Initialise(List<AccessibilityType> accessbilityFeatures)
        {
            this.accessbilityFeatures = accessbilityFeatures;
        }

        /// <summary>
        /// Read only. Returns the accessibility features for this control
        /// </summary>
        public List<AccessibilityType> Features
        {
            get { return accessbilityFeatures; }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Returns a list of FeatureIcons for Accessible features
        /// </summary>
        /// <param name="Icons"></param>
        /// <returns></returns>
        public List<VehicleFeatureIcon> GetAccessibleFeatureIcons(List<AccessibilityType> accessbilityTypes)
        {
            List<VehicleFeatureIcon> featureIcons = new List<VehicleFeatureIcon>();

            if (accessbilityTypes != null)
            {
                foreach (AccessibilityType at in accessbilityTypes)
                {
                    featureIcons.Add(new VehicleFeatureIcon(
                        string.Format("AccessibleFeaturesIcon.ImageURL.{0}", at.ToString()),
                        string.Format("AccessibleFeaturesIcon.AltTextToolTip.{0}", at.ToString()),
                        string.Format("AccessibleFeaturesIcon.AltTextToolTip.{0}", at.ToString()),
                        string.Format("AccessibleFeaturesIcon.AltTextToolTip.{0}", at.ToString())));
                }
            }

            return featureIcons;
        }

        #endregion
    }
}