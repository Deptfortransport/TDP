﻿// *********************************************** 
// NAME             : VehicleFeaturesControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 21 Apr 2011
// DESCRIPTION  	: Control to display the series of on-board facilities for a given vehicle type
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TDP.Common.ResourceManager;
using TDP.Common.Web;

namespace TDP.UserPortal.TDPWeb.Controls
{
    /// <summary>
    /// Control to display the series of on-board facilities for a given vehicle type
    /// </summary>
    public partial class VehicleFeaturesControl : System.Web.UI.UserControl
    {
        #region Private members

        private TDPPage page = null;
        private List<int> vehicleFeatures = null;

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
            FeaturesControlAdapter fca = new FeaturesControlAdapter();

            List<FeatureIcon> vehicleFeatureIcons = fca.GetRailVehicleFeatureIcons(vehicleFeatures);

            //Check that there are icons for this vehicle
            if (vehicleFeatureIcons.Count > 0)
            {
                //Set Repeater DataSource and DataBind to the array of icons
                rptrVehicleFeatures.DataSource = vehicleFeatureIcons;
                rptrVehicleFeatures.DataBind();
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
        /// Event handler for the Vehicle Features repeater item data bound event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptrVehicleFeatures_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                FeatureIcon fi = (FeatureIcon)e.Item.DataItem;

                if (fi != null)
                {
                    if (page == null)
                        page = (TDPPage)Page;

                    #region Image

                    Image imgVehicleFeature = (Image)e.Item.FindControl("imgVehicleFeature");

                    if (imgVehicleFeature != null)
                    {
                        // Set image values
                        imgVehicleFeature.ImageUrl = page.ImagePath + page.GetResource(TDPResourceManager.GROUP_JOURNEYOUTPUT, TDPResourceManager.COLLECTION_JOURNEY, fi.ImageUrlResource);
                        imgVehicleFeature.AlternateText = page.GetResource(TDPResourceManager.GROUP_JOURNEYOUTPUT, TDPResourceManager.COLLECTION_JOURNEY, fi.AltTextResource);
                        imgVehicleFeature.ToolTip = page.GetResource(TDPResourceManager.GROUP_JOURNEYOUTPUT, TDPResourceManager.COLLECTION_JOURNEY, fi.ToolTipResource);

                        if (DebugHelper.ShowDebug)
                            imgVehicleFeature.ToolTip += string.Format(" RailVehicleFeature[{0}]", fi.FeatureType);
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
        public void Initialise(List<int> vehicleFeatures)
        {
            this.vehicleFeatures = vehicleFeatures;
        }

        #endregion
        
    }
}