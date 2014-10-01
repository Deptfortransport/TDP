// *********************************************** 
// NAME             : LanguageLinkControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 16 Mar 2011
// DESCRIPTION  	: Control containing a language link
// ************************************************
// 

using System;
using TDP.Common;
using TDP.Common.ResourceManager;
using TDP.Common.Web;

namespace TDP.UserPortal.TDPWeb.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class LanguageLinkControl : System.Web.UI.UserControl
    {
        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_Init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            lnkbtnLanguage.Click += new EventHandler(lnkbtnLanguage_Click);
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SetLanguageLinkText();
        }
        #endregion

        #region Private methods

        /// <summary>
        /// Method to set the language link based on the current culture
        /// </summary>
        private void SetLanguageLinkText()
        {
            // Set the text
            Language language = CurrentLanguage.Value;

            if (language == Language.English)
            {
                lnkbtnLanguage.Text = string.Format("<span lang=\"en\">{0}</span>",
                    Global.TDPResourceManager.GetString(
                    language, TDPResourceManager.GROUP_DEFAULT, TDPResourceManager.COLLECTION_DEFAULT,
                    "Header.Language.Link.Text"));
            }
            else
            {
                lnkbtnLanguage.Text = string.Format("<span lang=\"fr\">{0}</span>",
                    Global.TDPResourceManager.GetString(
                    language, TDPResourceManager.GROUP_DEFAULT, TDPResourceManager.COLLECTION_DEFAULT,
                    "Header.Language.Link.Text"));
            }
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Event handler for lnkbtnLanguage_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnLanguage_Click(object sender, EventArgs e)
        {
            //It is only possible to switch language by clicking the link, 
            //so perform a straight switch, but not here... goto TDPPage::OnInit
            CurrentLanguage.Value = (CurrentLanguage.Value == Language.English ? Language.Welsh : Language.English);

            Server.Transfer(Request.RawUrl);
        }

        #endregion
    }
}