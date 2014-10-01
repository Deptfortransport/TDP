// *********************************************** 
// NAME             : AdvancedOptionsControl.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 19 Jul 2013
// DESCRIPTION  	: AdvancedOptionsControl to capture journey advanced options
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TDP.Common.Extenders;
using TDP.Common.PropertyManager;
using TDP.Common.Web;
using TDP.Common;
using TDP.UserPortal.JourneyControl;
using System.Web.UI.HtmlControls;
using System.Text;

namespace TDP.UserPortal.TDPMobile.Controls
{
    /// <summary>
    /// AdvancedOptionsControl to capture journey advanced options
    /// </summary>
    public partial class AdvancedOptionsControl : System.Web.UI.UserControl
    {
        #region Private Fields

        private List<List<TDPModeType>> allModesGrouped = new List<List<TDPModeType>>();
        private List<TDPModeType> allModes = new List<TDPModeType>();
        private List<TDPModeType> selectedModes = new List<TDPModeType>();

        private List<TDPModeType> allSelectedDisplayModes = new List<TDPModeType>();


        private string validationMessage = string.Empty;
        
        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_Init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SetAllSelectedDisplayModes();

            SetupModesControl();
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupResources();

            SetControlVisibility();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Read/Write. Transport Modes selected
        /// </summary>
        public List<TDPModeType> Modes
        {
            get { return GetSelectedModes(false); }
            set { selectedModes = value; }
        }

        /// <summary>
        /// Read/Write. Accessible journey options control
        /// </summary>
        public AccessibleOptionsControl AccessibleOptions
        {
            get { return accessibleOptionsControl; }
            set { accessibleOptionsControl = value; }
        }

        /// <summary>
        /// Read only. Contains any error or validation messages
        /// </summary>
        public string ValidationMessage
        {
            get { return validationMessage; }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// JourneySummary repeater data bound event - populates the various controls with the journey leg details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TransportModes_DataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem
                || e.Item.ItemType == ListItemType.Item
                || e.Item.ItemType == ListItemType.SelectedItem)
            {
                List<TDPModeType> modes = (List<TDPModeType>)e.Item.DataItem;
                TDPPageMobile page = (TDPPageMobile)Page;

                #region Controls

                Label lblTransportMode = e.Item.FindControlRecursive<Label>("lblTransportMode");
                CheckBox chkTransportMode = e.Item.FindControlRecursive<CheckBox>("chkTransportMode");
                HiddenField hdnTransportMode = e.Item.FindControlRecursive<HiddenField>("hdnTransportMode");
                Image imgTransportMode = e.Item.FindControlRecursive<Image>("imgTransportMode");
                
                #endregion

                #region Set journey summary values

                StringBuilder modesText = new StringBuilder();
                StringBuilder modesValue = new StringBuilder();


                string displayModeText = string.Empty;

                foreach (TDPModeType mode in modes)
                {
                    // Try getting the advanced option specific text
                    displayModeText = page.GetResource("TransportMode.AdvancedOption." + mode.ToString());
                    // Otherwise use the default
                    if (string.IsNullOrEmpty(displayModeText))
                        displayModeText = page.GetResource("TransportMode." + mode.ToString());

                    modesText.Append(displayModeText);
                    modesText.Append(" / ");

                    modesValue.Append(mode.ToString());
                    modesValue.Append(',');

                    if (selectedModes.Contains(mode))
                        chkTransportMode.Checked = true;

                    // Use first mode for the image
                    if (string.IsNullOrEmpty(imgTransportMode.ImageUrl))
                    {
                        imgTransportMode.ImageUrl = (page.ImagePath + page.GetResource(
                            string.Format("TransportMode.{0}.Small.ImageUrl", mode.ToString())));
                        imgTransportMode.AlternateText = page.GetResource(
                            string.Format("TransportMode.{0}", mode.ToString()));
                    }

                    // Set javascript data attribute to indicate this image should be used 
                    // when displaying on the "all selected modes options" label
                    if (allSelectedDisplayModes.Contains(mode))
                    {
                        imgTransportMode.Attributes["data-showallmodes"] = "true";
                    }
                }

                lblTransportMode.Text = modesText.ToString().Trim().TrimEnd('/');
                chkTransportMode.Text = string.Empty;
                hdnTransportMode.Value = modesValue.ToString().TrimEnd(',');
                imgTransportMode.AlternateText = lblTransportMode.Text;
                imgTransportMode.ToolTip = lblTransportMode.Text;
                imgTransportMode.GenerateEmptyAlternateText = true;

                // Persist the initial selection, used by javascript to maintain selected modes
                if (chkTransportMode.Checked)
                    transportModesSelected.Value += chkTransportMode.ClientID + ",";

                #endregion
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialises the control
        /// </summary>
        /// <param name="plannerMode"></param>
        public void Initialise(TDPJourneyPlannerMode plannerMode)
        {
            SetSelectableModes(plannerMode);
        }

        /// <summary>
        /// Resets the control
        /// </summary>
        public void Reset(TDPJourneyPlannerMode plannerMode)
        {
            // Set up the default modes list
            SetSelectableModes(plannerMode);

            // Set the selected modes to be same as default
            selectedModes = new List<TDPModeType>();
            foreach (TDPModeType mode in allModes)
            {
                selectedModes.Add(mode);
            }
        }

        /// <summary>
        /// Validates the advanced options
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            // Valid if modes selected, accessible options has no validation
            bool modesValid = (GetSelectedModes(false).Count > 0);

            if (!modesValid)
            {
                TDPPageMobile page = (TDPPageMobile)Page;

                validationMessage = page.GetResource("JourneyPlannerInput.ValidationError.NoModesSelected.Text");
            }

            return modesValid;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets up resources 
        /// </summary>
        private void SetupResources()
        {
            TDPPageMobile page = (TDPPageMobile)Page;

            lblAdvancedOptionsSR.Text = page.GetResourceMobile("JourneyInput.AdvancedOptions.ScreenReader");
            hdgTransportModesHead.InnerText = page.GetResourceMobile("JourneyInput.TransportModesHeading.Text");
            
            btnAdvancedOptions.Text = page.GetResourceMobile("JourneyInput.AdvancedOptionsButton.Text");
            btnAdvancedOptions.ToolTip = page.GetResourceMobile("JourneyInput.AdvancedOptionsButton.ToolTip");

            btnOKAdvancedOptions.Text = page.GetResourceMobile("JourneyInput.AdvancedOptionsOKButton.Text");
            btnOKAdvancedOptions.ToolTip = page.GetResourceMobile("JourneyInput.AdvancedOptionsOKButton.ToolTip");

            closeinfodialog.ToolTip = page.GetResourceMobile("JourneyInput.Close.ToolTip");
            hdnPageBackText.Value = page.GetResourceMobile("JourneyInput.Back.MobileInput.ToolTip");

            // Set selected options text
            StringBuilder selectedOptions = new StringBuilder();
            
            // All or at least one transport mode selected
            List<TDPModeType> selectedModes = GetSelectedModes(false);
            if (selectedModes.Count == allModes.Count)
            {
                string images = GetModeImagesString(allSelectedDisplayModes);

                if (!string.IsNullOrEmpty(images))
                    selectedOptions.Append(images);
                else
                    //display text only
                    selectedOptions.Append(page.GetResourceMobile("JourneyInput.TransportModesSelectedAll.Text"));
            }
            else if (selectedModes.Count > 0)
            {
                string images = GetModeImagesString(GetSelectedModes(true));

                if (!string.IsNullOrEmpty(images))
                    selectedOptions.Append(images);
                else
                    //display text only
                    selectedOptions.Append(page.GetResourceMobile("JourneyInput.TransportModesSelected.Text"));
            }

            selectedOptions.Append("<br />");

            // At least one or no mobility option selected
            if (accessibleOptionsControl.Assistance || accessibleOptionsControl.StepFree)
            {
                selectedOptions.Append(page.GetResourceMobile("PublicJourneyOptions.MobiltyOptionSelected.Text"));
            }
            else
            {
                selectedOptions.Append(page.GetResourceMobile("PublicJourneyOptions.MobiltyOptionNotSelected.Text"));
            }

            lblAdvancedOptionsSelected.Text = selectedOptions.ToString();
            lblAdvancedOptionsSelected.ToolTip = btnAdvancedOptions.ToolTip;

            // Set attributes for javascript updated text
            lblAdvancedOptionsSelected.Attributes["data-txtallmodes"] = page.GetResourceMobile("JourneyInput.TransportModesSelectedAll.Text");
            lblAdvancedOptionsSelected.Attributes["data-txtmodes"] = page.GetResourceMobile("JourneyInput.TransportModesSelected.Text");
            lblAdvancedOptionsSelected.Attributes["data-txtmobility"] = page.GetResourceMobile("PublicJourneyOptions.MobiltyOptionSelected.Text");
            lblAdvancedOptionsSelected.Attributes["data-txtnomobility"] = page.GetResourceMobile("PublicJourneyOptions.MobiltyOptionNotSelected.Text");

            // Set hidden validation message used by javascript
            hdnValidationMessage.Value = page.GetResource("JourneyPlannerInput.ValidationError.NoModesSelected.Text");
        }

        /// <summary>
        /// Sets the modes available
        /// </summary>
        /// <param name="plannerMode"></param>
        private void SetSelectableModes(TDPJourneyPlannerMode plannerMode)
        {
            this.allModes.Clear();
            this.allModesGrouped.Clear();

            string propKey = string.Format("PublicJourneyOptions.TransportModes.{0}", plannerMode.ToString());
            // Try getting the modes to display from properties
            if (!string.IsNullOrEmpty(Properties.Current[propKey]))
            {
                // Build up a list of modes grouped together as per config,
                // so they are shown as one selectable option
                try
                {
                    string modeGroups = Properties.Current[propKey];
                    string[] modeGroupIds = modeGroups.Split(',');

                    foreach (string modeGroupId in modeGroupIds)
                    {
                        List<TDPModeType> lstModeGroup = new List<TDPModeType>();

                        string modes = Properties.Current[string.Format("{0}.{1}", propKey, modeGroupId)];

                        foreach (string mode in modes.Split(','))
                        {
                            TDPModeType modeType = mode.Parse(TDPModeType.Unknown);

                            if (modeType != TDPModeType.Unknown)
                            {
                                lstModeGroup.Add(modeType);
                                this.allModes.Add(modeType);
                            }
                        }

                        // Add
                        if (lstModeGroup.Count > 0)
                            this.allModesGrouped.Add(lstModeGroup);
                    }
                }
                catch
                {
                    // ignore exceptions and fall into hard-coded, config invalid
                }
            }

            // Use the hard coded set of modes
            if (this.allModesGrouped == null || this.allModesGrouped.Count == 0)
            {
                List<TDPModeType> modes = JourneyRequestHelper.PopulateModes(plannerMode, null);

                foreach (TDPModeType mode in modes)
                {
                    List<TDPModeType> lstModeGroup = new List<TDPModeType>();
                    lstModeGroup.Add(mode);
                    this.allModes.Add(mode);
                    this.allModesGrouped.Add(lstModeGroup);
                }
            }
        }

        /// <summary>
        /// Sets up the transport modes control
        /// </summary>
        private void SetupModesControl()
        {
            if (!Page.IsPostBack
                || rptTransportModes.Items.Count == 0)
            {
                rptTransportModes.DataSource = allModesGrouped;
                rptTransportModes.DataBind();
            }
        }

        /// <summary>
        /// Gets the selected transport modes
        /// </summary>
        private List<TDPModeType> GetSelectedModes(bool firstInModeGroupOnly)
        {
            if (rptTransportModes.Items.Count > 0)
            {
                selectedModes = new List<TDPModeType>();

                foreach (RepeaterItem item in rptTransportModes.Items)
                {
                    CheckBox chkTransportMode = item.FindControlRecursive<CheckBox>("chkTransportMode");

                    if (chkTransportMode.Checked)
                    {
                        HiddenField hdnTransportMode = item.FindControlRecursive<HiddenField>("hdnTransportMode");

                        string modes = hdnTransportMode.Value;

                        foreach (string mode in modes.Split(','))
                        {
                            TDPModeType modeType = mode.Parse(TDPModeType.Unknown);

                            if (modeType != TDPModeType.Unknown)
                                selectedModes.Add(modeType);

                            // If only the first is required (most likely set when icons are to be displayed)
                            if (firstInModeGroupOnly)
                                break;
                        }
                    }
                }
            }

            return selectedModes;
        }

        /// <summary>
        /// Returns TDPModeTypes as an image string
        /// </summary>
        /// <param name="modes"></param>
        /// <returns></returns>
        private string GetModeImagesString(List<TDPModeType> modes)
        {
            TDPPageMobile page = (TDPPageMobile)Page;
            StringBuilder sbImages = new StringBuilder();

            try
            {
                foreach (TDPModeType mode in modes)
                {
                    if (mode != TDPModeType.Unknown)
                    {
                        string imageUrl = ResolveClientUrl(page.ImagePath + page.GetResource(
                            string.Format("TransportMode.{0}.Small.ImageUrl", mode.ToString())));
                        string alternateText = page.GetResource(
                            string.Format("TransportMode.{0}", mode.ToString()));

                        // Create an image
                        string img = string.Format("<img src=\"{0}\" alt=\"{1}\" title=\"{2}\" />", imageUrl, alternateText, alternateText);

                        sbImages.Append(img);
                    }
                }
            }
            catch
            {
                // Ignore exceptions
            }

            return sbImages.ToString();
        }

        /// <summary>
        /// Sets the all selected display modes list
        /// </summary>
        private void SetAllSelectedDisplayModes()
        {
            if (allSelectedDisplayModes.Count == 0)
            {
                // If all modes selected then only want to display some icons
                string smodes = Properties.Current["PublicJourneyOptions.TransportModes.AllSelected.Order"];
                foreach (string smode in smodes.Split(','))
                {
                    allSelectedDisplayModes.Add(smode.Parse(TDPModeType.Unknown));
                }
            }
        }

        /// <summary>
        /// Sets the visibility of controls
        /// </summary>
        private void SetControlVisibility()
        {
            if (!Properties.Current["PublicJourneyOptions.TransportModeOptions.Visible"].Parse(false))
            {
                transportModesDiv.Visible = false;
            }

            if (!Properties.Current["PublicJourneyOptions.AccessibilityOptions.Visible"].Parse(false))
            {
                accessibleOptionsDiv.Visible = false;
            }
        }

        #endregion
    }
}