// *********************************************** 
// NAME                 : AnalyticsControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 10/09/2012
// DESCRIPTION          : Control to add analytics tag
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AnalyticsControl.ascx.cs-arc  $ 
//
//   Rev 1.0   Sep 10 2012 14:20:14   mmodi
//Initial revision.
//Resolution for 5846: Analytics - Update Google Analytics tag to use async version
//

using System;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Control to add analytics tag
    /// </summary>
    public partial class AnalyticsControl : TDUserControl
    {
        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            LoadAnalyticsTag();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method to load the analytics tag
        /// </summary>
        private void LoadAnalyticsTag()
        {
            // Get tag content specific for page
            string analyticsTagContent = GetResource(string.Format("Analytics.Tag.{0}", PageId));

            if (string.IsNullOrEmpty(analyticsTagContent))
            {
                // No tag specific for page, load default tag
                analyticsTagContent = GetResource("Analytics.Tag");
            }

            // Add tag onto the page
            if (!string.IsNullOrEmpty(analyticsTagContent))
            {
                analyticsTag.Text = analyticsTagContent;
            }
            else
            {
                // No tag, hide this control
                this.Visible = false;
            }
        }

        #endregion
    }
}