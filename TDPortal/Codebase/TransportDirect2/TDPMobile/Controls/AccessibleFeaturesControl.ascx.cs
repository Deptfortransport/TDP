// *********************************************** 
// NAME             : AccessibleFeaturesControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 21 Mar 2011
// DESCRIPTION  	: Control to display the series of accessible facilities
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TDP.Common.ResourceManager;
using TDP.Common.Web;
using TDP.UserPortal.JourneyControl;

namespace TDP.UserPortal.TDPMobile.Controls
{
    /// <summary>
    /// Control to display the series of accessible facilities
    /// </summary>
    public partial class AccessibleFeaturesControl : System.Web.UI.UserControl
    {
        #region Private members

        private TDPPageMobile page = null;
        private List<TDPAccessibilityType> accessbilityFeatures = null;

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

            List<FeatureIcon> accessibleFeatureIcons = fca.GetAccessibleFeatureIcons(accessbilityFeatures);

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
                FeatureIcon fi = (FeatureIcon)e.Item.DataItem;

                if (fi != null)
                {
                    if (page == null)
                        page = (TDPPageMobile)Page;

                    #region Image

                    Image imgAccessibleFeature = (Image)e.Item.FindControl("imgAccessibleFeature");

                    if (imgAccessibleFeature != null)
                    {
                        // Set image values
                        imgAccessibleFeature.ImageUrl = page.ImagePath + page.GetResource(TDPResourceManager.GROUP_JOURNEYOUTPUT, TDPResourceManager.COLLECTION_JOURNEY, fi.ImageUrlResource);
                        imgAccessibleFeature.AlternateText = page.GetResource(TDPResourceManager.GROUP_JOURNEYOUTPUT, TDPResourceManager.COLLECTION_JOURNEY, fi.AltTextResource);
                        imgAccessibleFeature.ToolTip = page.GetResource(TDPResourceManager.GROUP_JOURNEYOUTPUT, TDPResourceManager.COLLECTION_JOURNEY, fi.ToolTipResource);

                        if (DebugHelper.ShowDebug)
                            imgAccessibleFeature.ToolTip += string.Format(" TDPAccessibilityType[{0}]", fi.FeatureType);

                        // Hide image if no image url exists
                        if (string.IsNullOrEmpty(page.GetResource(TDPResourceManager.GROUP_JOURNEYOUTPUT, TDPResourceManager.COLLECTION_JOURNEY, fi.ImageUrlResource)))
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
        public void Initialise(List<TDPAccessibilityType> accessbilityFeatures)
        {
            this.accessbilityFeatures = accessbilityFeatures;
        }

        #endregion
    }
}