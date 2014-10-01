// *********************************************** 
// NAME             : AcessibleOptionsControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 15 Mar 2012
// DESCRIPTION  	: Accessible mobility options control
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TDP.Common.Extenders;
using TDP.Common.Web;
using TDP.Common.PropertyManager;
using TDP.Common;
using TDP.Common.ResourceManager;

namespace TDP.UserPortal.TDPMobile.Controls
{
    /// <summary>
    /// Accessible mobility options control
    /// </summary>
    public partial class AccessibleOptionsControl : System.Web.UI.UserControl
    {      
        #region Public Properties

        /// <summary>
        /// Read/Write property to determine if journey planning should exclude under grounds
        /// </summary>
        public bool ExcludeUnderGround
        {
            get { return false; }
            set { // do nothing
            }
        }

        /// <summary>
        /// Read/Write property to determine if special assistance required during journey
        /// </summary>
        public bool Assistance
        {
            get { return chkAssistance.Checked; }
            set { chkAssistance.Checked = value; }
        }

        /// <summary>
        /// Read/Write property determining if step free required in journey
        /// </summary>
        public bool StepFree
        {
            get { return chkStepFree.Checked; }
            set { chkStepFree.Checked = value; }
        }

        /// <summary>
        /// Read/Write property determining if step free and assistance required in journey
        /// </summary>
        public bool StepFreeAndAssistance
        {
            get { return Assistance && StepFree; }
            set { StepFree = value; Assistance = value; }
        }

        /// <summary>
        /// Read/Write property determining is fewest changes is required
        /// </summary>
        public bool FewestChanges
        {
            get
            {   // Only true if assistance or step free checked
                if (Assistance || StepFree)
                {
                    return chkFewestChanges.Checked;
                }
                else
                    return false;
            }
            set { chkFewestChanges.Checked = value; }
        }

        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_Load event handler
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
            SetupResources();

            // Accessible options functionality turned off
            if (!Properties.Current["PublicJourneyOptions.AccessibilityOptions.Visible"].Parse(true))
            {
                accessibilityOptions.Visible = false;

                ExcludeUnderGround = false;
                StepFree = false;
                Assistance = false;
            }

            SetupHiddenValues();
        }

        #endregion
        
        #region Private Methods

        /// <summary>
        /// Loads resource strings for labels/controls
        /// </summary>
        private void SetupResources()
        {
            TDPPageMobile page = (TDPPageMobile)Page;

            hdgMobilityOptionsHead.InnerText = page.GetResourceMobile("PublicJourneyOptions.MobiltyOptionsHeading.Text");

            lblStepFree.Text = page.GetResourceMobile("PublicJourneyOptions.MobiltyOption.StepFree.Text");
            lblStepFreeInfo.Text = page.GetResourceMobile("PublicJourneyOptions.MobiltyOption.StepFree.Info.Text");
            chkStepFree.Text = string.Empty;

            lblAssistance.Text = page.GetResourceMobile("PublicJourneyOptions.MobiltyOption.Assistance.Text");
            lblAssistanceInfo.Text = page.GetResourceMobile("PublicJourneyOptions.MobiltyOption.Assistance.Info.Text");
            chkAssistance.Text = string.Empty;

            lblFewestChanges.Text = page.GetResourceMobile("PublicJourneyOptions.MobiltyOption.FewestChanges.Text");
            chkFewestChanges.Text = string.Empty;

            accessibleNotesLinkSF.InnerText = page.GetResourceMobile("PublicJourneyOptions.MobiltyOption.StepFree.InfoLink.Text");
            accessibleNotesLinkSF.Title = page.GetResourceMobile("PublicJourneyOptions.MobiltyOption.StepFree.InfoLink.ToolTip");
            accessibleNotesLinkAss.InnerText = page.GetResourceMobile("PublicJourneyOptions.MobiltyOption.Assistance.InfoLink.Text");
            accessibleNotesLinkAss.Title = page.GetResourceMobile("PublicJourneyOptions.MobiltyOption.Assistance.InfoLink.ToolTip");
        }

        /// <summary>
        /// Sets the hidden value contianing the selected options
        /// </summary>
        private void SetupHiddenValues()
        {
            mobilityOptionsSelected.Value = string.Empty;

            if (chkStepFree.Checked)
            {
                mobilityOptionsSelected.Value += chkStepFree.ClientID + ",";
            }

            if (chkAssistance.Checked)
            {
                mobilityOptionsSelected.Value += chkAssistance.ClientID;
            }

            if (chkFewestChanges.Checked)
            {
                mobilityOptionsSelected.Value += chkFewestChanges.ClientID;
            }

            mobilityOptionsSelected.Value = mobilityOptionsSelected.Value.TrimEnd(',');
        }

        #endregion
    }
}