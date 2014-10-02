// *********************************************** 
// NAME                 : ModalPopupMessage.ascx.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 21/02/2010
// DESCRIPTION			: Control to display simple modal popup on the page
//                      : This control can be extended in future to make more advance popup
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ModalPopupMessage.ascx.cs-arc  $
//
//   Rev 1.4   Aug 17 2012 10:55:28   dlane
//Cycle walk links
//Resolution for 5827: CCN Cycle Walk links
//
//   Rev 1.3   Feb 24 2010 12:11:14   apatel
//Updated to display Modal popup correctly and removed the javascript error on printer friendly page
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Feb 23 2010 12:26:38   apatel
//Changes made to make ModalPopupMessage control work correctly
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.1   Feb 22 2010 07:44:54   apatel
//popup script changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Feb 21 2010 23:36:54   apatel
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// ModalPopupMessage code behind class
    /// </summary>
    public partial class ModalPopupMessage : TDPrintableUserControl
    {
        #region Private Fields
        private Control targetControl = null;
        private string okButtonText = string.Empty;
        private string cancelButtonText = string.Empty;
        private string popupMessage = string.Empty;
        private string cssClass = "modalPopup";
        #endregion

        #region Public Properties
        /// <summary>
        /// Write only property - Control which requires a popup
        /// </summary>
        public Control TargetControl
        {
            set
            {
                targetControl = value;
            }
        }

        /// <summary>
        /// Read/write - Text to display for popup ok button
        /// </summary>
        public string OkButtonText
        {
            get
            {
                return okButtonText;
            }
            set
            {
                okButtonText = value;
            }
        }

        /// <summary>
        /// Read/write - Text to display for popup cancel button
        /// </summary>
        public string CancelButtonText
        {
            get
            {
                return cancelButtonText;
            }
            set
            {
                cancelButtonText = value;
            }

        }

        /// <summary>
        /// Read/write - Message to be shown in popup
        /// </summary>
        public string PopupMessage
        {
            get
            {
                return popupMessage;
            }
            set
            {
                popupMessage = value;
            }
        }

        
        /// <summary>
        /// Read/write - CSS class for the control
        /// </summary>
        public string CssClass
        {
            get
            {
                return cssClass;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    cssClass = value;
                }
            }
        }

        #endregion

        #region Page handlers
        /// <summary>
        /// Page load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// Page prerender event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupControls();
            RegisterJavaScripts();
            SetupVisibility();
        }
       
        #endregion

        #region Private methods
        /// <summary>
        /// Sets up child controls' text
        /// </summary>
        private void SetupControls()
        {
            OKButton.Text = okButtonText;
            CancelButton.Text = cancelButtonText;
            ModalPopupMessageDiv.InnerHtml = popupMessage;

            
        }

        /// <summary>
        /// Sets up visibility of the control and child controls
        /// </summary>
        private void SetupVisibility()
        {
            TDPage page = (TDPage)this.Page;

            this.Visible = page.IsJavascriptEnabled && !this.PrinterFriendly;
        }

        /// <summary>
        /// Register Javascript
        /// Sets up the popup and ok and cancel button's scripts
        /// </summary>
        private void RegisterJavaScripts()
        {
            TDPage page = (TDPage)this.Page;

            string attachPopupClientScript = string.Empty;

            if(page.IsJavascriptEnabled && !this.PrinterFriendly)
            {
                // set up show popup script
                attachPopupClientScript = string.Format("attachPopupMessage('{0}','{1}');", this.ClientID, targetControl.ClientID);

                // Determine if javascript is support and determine the JavascriptDom value
                ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

                string scriptName = "ModalPopupMessage";
                string scriptWindowOnLoadManager = "WindowOnLoadManager";

                string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];

                // Register the javascript file
                page.ClientScript.RegisterStartupScript(this.GetType(), "", scriptRepository.GetScript(scriptName, javaScriptDom));

                // Register the WindowOnLoadManager script, so we can add our necessary Chart functions to fire
                page.ClientScript.RegisterStartupScript(this.GetType(), scriptWindowOnLoadManager, scriptRepository.GetScript(scriptWindowOnLoadManager, page.JavascriptDom));
             

                string hidePopupScript = string.Format("hidePopupMessage('{0}',false,'{1}');", this.ClientID, targetControl.ClientID);

                // hide the popup at the page load and setup the popup when page loads
                string functionName = "hidePopup" + this.ClientID;
                string onLoadScript = "function " + functionName + "(){" + hidePopupScript + attachPopupClientScript + "}";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "HidePopupOnLoad" + this.ClientID, onLoadScript, true);

                onLoadScript = "womAdd('" + functionName + "()');womOn();";

                Page.ClientScript.RegisterStartupScript(this.GetType(), "OnLoadScript" + this.ClientID, onLoadScript, true);

                // Cancel button client script
                CancelButton.OnClientClick = hidePopupScript + "return false;";

                string hideOKPopupScript = string.Format("hidePopupMessage('{0}',true,'{1}');return false;", this.ClientID, targetControl.ClientID);

                // Ok button client script
                OKButton.OnClientClick = hideOKPopupScript;

                OKButton.UseSubmitBehavior = false;
                
            }

        }
        #endregion
    }
}