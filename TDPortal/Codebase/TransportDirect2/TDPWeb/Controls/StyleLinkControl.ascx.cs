// *********************************************** 
// NAME             : StyleLinkControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Apr 2011
// DESCRIPTION  	: Control containing a style link
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TDP.Common.Web;
using TDP.Common;

namespace TDP.UserPortal.TDPWeb.Controls
{
    /// <summary>
    /// Control containing a style link
    /// </summary>
    public partial class StyleLinkControl : System.Web.UI.UserControl
    {
        #region Public control mode enum
        
        /// <summary>
        /// Enum defining the mode this control should be rendered as
        /// </summary>
        public enum StyleLinkMode
        {
            FontSmall,
            FontMedium,
            FontLarge,
            AccessibleNormal,
            AccessibleDyslexia,
            AccessibleHighVis
        }

        #endregion

        #region Private members

        private StyleLinkMode styleMode = StyleLinkMode.FontSmall;

        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_Init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            lnkbtnStyle.Click += new EventHandler(lnkbtnStyle_Click);
        }

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
            SetLinkText();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method to set the link text based on the current control mode
        /// </summary>
        private void SetLinkText()
        {
            Language language = CurrentLanguage.Value;

            // Set the text
            switch (styleMode)
            {   
                    // Accessible links display text in a label (as London2012 does)
                case StyleLinkMode.AccessibleDyslexia:
                    lblStyle.Text = Global.TDPResourceManager.GetString(language, "Header.Style.Link.AccessibleDyslexia.Text");
                    lblStyle.ToolTip = Global.TDPResourceManager.GetString(language, "Header.Style.Link.AccessibleDyslexia.ToolTip");
                    lblStyle.CssClass = "skin-switch";
                    lblStyleHidden.Text = Global.TDPResourceManager.GetString(language, "Header.Style.Link.AccessibleDyslexia.Hidden.Text");
                    lblStyleHidden.CssClass = "dyslexic-style hidden";
                    lblStyle.Visible = true;
                    lblStyleHidden.Visible = true;
                    lnkbtnStyle.Visible = true;
                    break;

                case StyleLinkMode.AccessibleHighVis:
                    lblStyle.Text = Global.TDPResourceManager.GetString(language, "Header.Style.Link.AccessibleHighVis.Text");
                    lblStyle.ToolTip = Global.TDPResourceManager.GetString(language, "Header.Style.Link.AccessibleHighVis.ToolTip");
                    lblStyle.CssClass = "skin-switch";
                    lblStyleHidden.Text = Global.TDPResourceManager.GetString(language, "Header.Style.Link.AccessibleHighVis.Hidden.Text");
                    lblStyleHidden.CssClass = "high-vis-style hidden";
                    lblStyle.Visible = true;
                    lblStyleHidden.Visible = true;
                    lnkbtnStyle.Visible = true;
                    break;

                case StyleLinkMode.AccessibleNormal:
                    lblStyle.Text = Global.TDPResourceManager.GetString(language, "Header.Style.Link.AccessibleNormal.Text");
                    lblStyle.ToolTip = Global.TDPResourceManager.GetString(language, "Header.Style.Link.AccessibleNormal.ToolTip");
                    lblStyle.CssClass = "skin-switch";
                    lblStyleHidden.Text = Global.TDPResourceManager.GetString(language, "Header.Style.Link.AccessibleNormal.Hidden.Text");
                    lblStyleHidden.CssClass = "normal-style hidden";
                    lblStyle.Visible = true;
                    lblStyleHidden.Visible = true;
                    lnkbtnStyle.Visible = true;
                    break;

                    // Font links display text in the hyperlink itself (as London2012 does)
                case StyleLinkMode.FontLarge:
                    lnkbtnStyle.Text = string.Format("<span class=\"hidden\">{0}</span>{1}",
                        Global.TDPResourceManager.GetString(language, "Header.Style.Link.FontSmall.Hidden.Text"),
                        Global.TDPResourceManager.GetString(language, "Header.Style.Link.FontLarge.Text"));
                    lnkbtnStyle.ToolTip = Global.TDPResourceManager.GetString(language, "Header.Style.Link.FontLarge.ToolTip");
                    lnkbtnStyle.CssClass = "font-size";
                    lnkbtnStyle.Visible = true;
                    break;

                case StyleLinkMode.FontMedium:
                    lnkbtnStyle.Text = string.Format("<span class=\"hidden\">{0}</span>{1}",
                        Global.TDPResourceManager.GetString(language, "Header.Style.Link.FontSmall.Hidden.Text"),
                        Global.TDPResourceManager.GetString(language, "Header.Style.Link.FontMedium.Text"));
                    lnkbtnStyle.ToolTip = Global.TDPResourceManager.GetString(language, "Header.Style.Link.FontMedium.ToolTip");
                    lnkbtnStyle.CssClass = "font-size";
                    lnkbtnStyle.Visible = true;
                    break;

                case StyleLinkMode.FontSmall:
                default:
                    lnkbtnStyle.Text = string.Format("<span class=\"hidden\">{0}</span>{1}",
                        Global.TDPResourceManager.GetString(language, "Header.Style.Link.FontSmall.Hidden.Text"),
                        Global.TDPResourceManager.GetString(language, "Header.Style.Link.FontSmall.Text"));
                    lnkbtnStyle.ToolTip = Global.TDPResourceManager.GetString(language, "Header.Style.Link.FontSmall.ToolTip");
                    lnkbtnStyle.CssClass = "font-size";
                    lnkbtnStyle.Visible = true;
                    break;
            }
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Event handler for lnkbtnStyle_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnStyle_Click(object sender, EventArgs e)
        {
            // Set the appropriate value dependent on style link mode
            switch (styleMode)
            {
                case StyleLinkMode.AccessibleDyslexia:
                    CurrentStyle.AccessibleStyleValue = AccessibleStyle.Dyslexia;
                    break;
                case StyleLinkMode.AccessibleHighVis:
                    CurrentStyle.AccessibleStyleValue = AccessibleStyle.HighVis;
                    break;
                case StyleLinkMode.AccessibleNormal:
                    CurrentStyle.AccessibleStyleValue = AccessibleStyle.Normal;
                    break;
                case StyleLinkMode.FontLarge:
                    CurrentStyle.FontSizeValue = TDP.Common.Web.FontSize.Large;
                    break;
                case StyleLinkMode.FontMedium:
                    CurrentStyle.FontSizeValue = TDP.Common.Web.FontSize.Medium;
                    break;
                case StyleLinkMode.FontSmall:
                default:
                    CurrentStyle.FontSizeValue = TDP.Common.Web.FontSize.Normal;
                    break;

            }

            //It is only possible to switch style by clicking the link, 
            //so perform a straight switch, but not here... goto TDPPage::OnInit
            Server.Transfer(Request.RawUrl);
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. The StyleLinkMode this control should be displayed as
        /// </summary>
        public StyleLinkMode StyleMode
        {
            get { return styleMode; }
            set { styleMode = value; }
        }

        #endregion
    }
}