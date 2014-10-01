// *********************************************** 
// NAME             : SideBarLeftControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Feb 2011
// DESCRIPTION  	: Side bar left control
// ************************************************
// 

using System;
using System.Web.UI.WebControls;
using TDP.Common;
using TDP.Common.Web;

namespace TDP.UserPortal.TDPWeb.Controls
{
    /// <summary>
    /// Side bar left control
    /// </summary>
    public partial class SideBarLeftControl : System.Web.UI.UserControl
    {
        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupNavigation();
        }

        #endregion

        #region Private methods
        
        /// <summary>
        /// Method to setup the correct navigation
        /// </summary>
        private void SetupNavigation()
        {
        }

        #endregion
    }
}