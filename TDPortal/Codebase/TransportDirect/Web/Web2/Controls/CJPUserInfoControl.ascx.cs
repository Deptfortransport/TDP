// *********************************************** 
// NAME             : CJPUserInfoControl.ascx.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 26 Oct 2010
// DESCRIPTION  	: Control to provide CJP info for power users
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/CJPUserInfoControl.ascx.cs-arc  $
//
//   Rev 1.2   Oct 11 2012 14:05:50   mmodi
//Updated cjp user output information styling
//Resolution for 5859: Error message logging tidy-up
//
//   Rev 1.1   Oct 27 2010 11:16:34   apatel
//Updated to add Error handling for CJP power user additional information 
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.0   Oct 26 2010 14:31:46   apatel
//Initial revision.
//Resolution for 5623: Additional information available to CJP users
// 


using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Control to provide CJP info for power users
    /// </summary>
    public partial class CJPUserInfoControl : System.Web.UI.UserControl
    {
       
        #region Private Fields
        private CJPInfoType infoType = CJPInfoType.None;
        private CJPUserInfoHelper infoHelper = null;
        private string displayText = string.Empty;

        private bool visible = false;
        private bool newLineBefore = false;
        private bool newLineAfter = false;
        private bool showTip = true;
        #endregion

        #region Public Properties
        /// <summary>
        /// Read/Write property. Specifies type of CJP info to display
        /// </summary>
        public CJPInfoType InfoType
        {
            get { return infoType; }
            set { infoType = value; }
        }

        /// <summary>
        /// Read/Write property. CJP information to be displayed.
        /// If DisplayText provided it takes preference and displayed otherwise control tries to 
        /// get information using the CJP info type and CJP info helper
        /// </summary>
        public string DisplayText
        {
            get { return displayText; }
            set { displayText = value; }
        }

        /// <summary>
        /// Read/Write property. Specifies whether to render new line before CJP info
        /// </summary>
        public bool NewLineBefore
        {
            get { return newLineBefore; }
            set { newLineBefore = value; }
        }

        /// <summary>
        /// Read/Write property. Specifies whether to render new line after CJP info
        /// </summary>
        public bool NewLineAfter
        {
            get { return newLineAfter; }
            set { newLineAfter = value; }
        }

        /// <summary>
        /// Read/Write property. Specifies whether to render tooltip for CJP info
        /// </summary>
        public bool ShowAsToolTip
        {
            get { return showTip; }
            set { showTip = value; }
        }

        #endregion

        #region Initialise
        /// <summary>
        /// Initialise the CJPUserInfoControl 
        /// </summary>
        /// <param name="infoHelper"></param>
        public void Initialise(CJPUserInfoHelper infoHelper)
        {
            this.infoHelper = infoHelper;
        }
        #endregion


        #region Page Events
        /// <summary>
        /// Pre Render
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            try
            {
                visible = CJPUserInfoHelper.IsCJPInformationAvailable();

                if (visible)
                {
                    visible = infoType != CJPInfoType.None;
                }

                if (visible)
                {
                    visible = CJPUserInfoHelper.IsCJPInformationAvailableForType(infoType);

                    if (!visible)
                        return;

                    if (!string.IsNullOrEmpty(displayText))
                    {
                        cjpInfoLabel.Text = displayText;
                        if (showTip)
                            cjpInfoLabel.ToolTip = displayText;
                    }
                    else if (infoHelper != null)
                    {
                        cjpInfoLabel.Text = infoHelper.GetCJPInformationForType(infoType);
                        if (showTip)
                            cjpInfoLabel.ToolTip = cjpInfoLabel.Text;
                    }
                }

                if (visible && string.IsNullOrEmpty(cjpInfoLabel.Text))
                {
                    visible = false;
                }

                newLineBeforeLiteral.Visible = newLineBefore;
                newLineAfterLiteral.Visible = newLineAfter;

                if (newLineBefore)
                    newLineBeforeLiteral.Text = "<br />";
                if (newLineAfter)
                    newLineAfterLiteral.Text = "<br />";

                this.Visible = visible;
            }
            catch (Exception ex)
            {
                // Log the exception 
                string message = "CJPUserInfo - threw an exception while rendering CJP user information"
                    + " Exception: " + ex.Message
                    + " Stacktrace: " + ex.StackTrace;

                OperationalEvent oe = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, message);
                Logger.Write(oe);
            }
        }
        #endregion


    }
}