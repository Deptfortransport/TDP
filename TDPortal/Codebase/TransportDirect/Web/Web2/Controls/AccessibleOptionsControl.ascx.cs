// ************************************************************************************************ 
// NAME                 : AccessibleOptionsControl.ascx.cs 
// AUTHOR               : David Lane
// DATE CREATED         : 13/11/2012 
// DESCRIPTION			: Displays a list of accessible options. 
// ************************************************************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AccessibleOptionsControl.ascx.cs-arc  $
//
//   Rev 1.13   Feb 08 2013 08:43:08   mmodi
//W3 Validator corrections
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.12   Jan 30 2013 13:47:16   mmodi
//Fixed showing advanced options on ambiguity page
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.11   Jan 24 2013 15:44:00   mmodi
//Reset checked option when setting to prevent multiple selected
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.10   Jan 23 2013 15:24:40   DLane
//Styling changes and adding FAQ link
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.9   Jan 17 2013 15:24:06   DLane
//Aria tags for expanding divs
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.8   Jan 17 2013 09:45:34   mmodi
//Updates to D2D advanced options for better js and non-js behaviour
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.7   Jan 15 2013 15:42:12   mmodi
//Page landing updates for accessible locations
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.6   Jan 15 2013 14:10:38   DLane
//Accessiblity updates
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.5   Jan 10 2013 16:16:08   dlane
//Door to door input options
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.4   Jan 07 2013 18:12:14   dlane
//JS versions of options
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.3   Dec 11 2012 17:28:46   mmodi
//Updated to display accessible option on ambiguity page
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.2   Dec 05 2012 13:51:08   mmodi
//Only populate list on first load
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Nov 15 2012 16:19:52   mmodi
//Null check for accessible options property switch
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Nov 15 2012 15:44:36   DLane
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//

using System;
using TransportDirect.Common.ResourceManager;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;

using ControlPopulator = TransportDirect.UserPortal.DataServices.DataServices;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///		Summary description for TransportTypesContol.
	/// </summary>
	public partial class AccessibleOptionsControl :  TDUserControl
    {
        #region Private members

        private ControlPopulator populator;
        private GenericDisplayMode displayMode;

        #endregion

        #region Constructor

        /// <summary>
		///  Default constructor, retreives and set data services populator
		/// </summary>
		public AccessibleOptionsControl()
		{
			populator = (ControlPopulator)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
        }

        #endregion

        #region Page_Load

        /// <summary>
		/// Method sets label text using resource strings
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.LocalResourceManager = TDResourceManager.LANG_STRINGS;

            bool showAccessibleOptions = false;
            bool.TryParse(Properties.Current["AccessibleOptions.Visible.Switch"], out showAccessibleOptions);

            if (showAccessibleOptions)
            {
                pnlAccessibleOptions.Visible = true;
                pnlAccessibleOptionsReadonly.Visible = false;
            }
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupResources();

            UpdateControlDisplayMode();
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Show button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShow_Click(object sender, EventArgs e)
        {
            UpdateOptionsVisibility(true);
            
            btnShow.Visible = false;
        }

        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

        #region Public methods

        /// <summary>
        /// Initialises the controls
        /// </summary>
        /// <param name="requireStepFreeAccess"></param>
        /// <param name="requireSpecialAssistance"></param>
        /// <param name="requireFewerInterchanges"></param>
        public void Initialise(bool requireStepFreeAccess, bool requireSpecialAssistance, bool requireFewerInterchanges)
        {
            // Accessible option
            if (requireStepFreeAccess && requireSpecialAssistance)
            {
                SelectedAccessibilityOption = AccessibleOptionsType.WheelchairAndAssistance;
            }
            else if (requireStepFreeAccess)
            {
                SelectedAccessibilityOption = AccessibleOptionsType.Wheelchair;
            }
            else if (requireSpecialAssistance)
            {
                SelectedAccessibilityOption = AccessibleOptionsType.Assistance;
            }
            else
            {
                SelectedAccessibilityOption = AccessibleOptionsType.NoRequirement;
            }

            // Fewest changes
            FewestChanges.Checked = requireFewerInterchanges;
        }

        /// <summary>
        /// Resets the control and its state
        /// </summary>
        public void Reset()
        {
            SelectedAccessibilityOption = AccessibleOptionsType.NoRequirement;

            UpdateOptionsVisibility(false);

            btnShow.Visible = true;
        }

        #endregion

        #region Public properties

        /// <summary>
        ///  Returns the selected accessibility type.
        /// </summary>
        /// <returns></returns>
        public AccessibleOptionsType SelectedAccessibilityOption
        {
            get
            {
                if (stepFree.Checked)
                    return AccessibleOptionsType.Wheelchair;
                else if (stepFreeAndAssistance.Checked)
                    return AccessibleOptionsType.WheelchairAndAssistance;
                else if (assistance.Checked)
                    return AccessibleOptionsType.Assistance;
                else
                    return AccessibleOptionsType.NoRequirement;
            }
            set
            {
                // Reset all
                stepFree.Checked = false;
                stepFreeAndAssistance.Checked = false;
                assistance.Checked = false;
                noRequirement.Checked = false;

                switch (value)
                {
                    case AccessibleOptionsType.Wheelchair:
                        stepFree.Checked = true;
                        break;
                    case AccessibleOptionsType.WheelchairAndAssistance:
                        stepFreeAndAssistance.Checked = true;
                        break;
                    case AccessibleOptionsType.Assistance:
                        assistance.Checked = true;
                        break;
                    case AccessibleOptionsType.NoRequirement:
                        noRequirement.Checked = true;
                        break;
                }
            }
        }

		/// <summary>
		/// Read - Allows access to the FewestChanges CheckBox
		/// </summary>
		public CheckBox FewestChanges
		{
            get { return checkFewestChanges; }
		}

        /// <summary>
        /// Returns true if the user is logged in and has elected to save their
        /// travel details
        /// Read only.
        /// </summary>
        public bool SavePreferences
        {
            get { return loginSaveOption.SaveDetails; }
        }

        /// <summary>
        /// Sets the display mode for accessibility options, default state is input
        /// </summary>
        public GenericDisplayMode DisplayMode
        {
            get { return displayMode; }
            set { displayMode = value; }
        }

        /// <summary>
        /// Read only. Returns true if non-default options have been selected
        /// </summary>
        public bool IsOptionSelected
        {
            get { return SelectedAccessibilityOption != AccessibleOptionsType.NoRequirement; }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the accessible options resources
        /// </summary>
        private void SetupResources()
        {
            //labelType.Text = GetResource("AccessibleOptionsControl.Type");
            //labelAccessibleOptions.Text = GetResource("AccessibleOptionsControl.ScreenReader");
            lblAccessibleOptionReadOnly.Text = GetResource("AccessibleOptionsControl.Type");
            labelJsQuestion.Text = GetResource("AccessibleOptionsControl.Question");
            labelOptionsSelected.Text = GetResource("AccessibleOptionsControl.OptionsSelected");
            labelTitle.Text = GetResource("AccessibleOptionsControl.Title");
            btnShow.Text = GetResource("AdvancedOptions.Show.Text");
            
            // Accessible options
            stepFree.Text = GetResource("DataServices.AccessibleOptionsRadio.Wheelchair");
            stepFreeAndAssistance.Text = GetResource("DataServices.AccessibleOptionsRadio.WheelchairAndAssistance");
            assistance.Text = GetResource("DataServices.AccessibleOptionsRadio.Assistance");
            noRequirement.Text = GetResource("DataServices.AccessibleOptionsRadio.NoRequirement");
            
            checkFewestChanges.Text = GetResource("AcesssibleOptions.FewestChanges");

            // Info tooltips
            stepFreeAnchor.Title = GetResource("AcesssibleOptions.Wheelchair.Info");
            stepFreeAndAssistanceAnchor.Title = GetResource("AcesssibleOptions.WheelchairAndAssistance.Info");
            assistanceAnchor.Title = GetResource("AcesssibleOptions.Assistance.Info");
            noRequirementAnchor.Title = GetResource("AcesssibleOptions.NoRequirement.Info");
            checkFewestChangesAnchor.Title = GetResource("AcesssibleOptions.FewerChanges.Info"); 

            imgStepFree.ImageUrl = GetResource("AcesssibleOptions.Assistance.InfoIcon");
            imgStepFreeAndAssistance.ImageUrl = GetResource("AcesssibleOptions.Assistance.InfoIcon");
            imgAssistance.ImageUrl = GetResource("AcesssibleOptions.Assistance.InfoIcon");
            imgNoRequirement.ImageUrl = GetResource("AcesssibleOptions.NoRequirement.InfoIcon");
            imgCheckFewestChanges.ImageUrl = GetResource("AcesssibleOptions.FewerChanges.InfoIcon");

            imgStepFree.AlternateText = "i";
            imgStepFreeAndAssistance.AlternateText = "i";
            imgAssistance.AlternateText = "i";
            imgNoRequirement.AlternateText = "i";
            imgCheckFewestChanges.AlternateText = "i";

            stepFreeAnchor.Visible = !string.IsNullOrEmpty(stepFreeAnchor.Title);
            stepFreeAndAssistanceAnchor.Visible = !string.IsNullOrEmpty(stepFreeAndAssistanceAnchor.Title);
            assistanceAnchor.Visible = !string.IsNullOrEmpty(assistanceAnchor.Title);
            noRequirementAnchor.Visible = !string.IsNullOrEmpty(noRequirementAnchor.Title);
            checkFewestChangesAnchor.Visible = !string.IsNullOrEmpty(checkFewestChangesAnchor.Title);

            accessibleFaq.InnerHtml = GetResource("AccessibleOptionsControl.FaqLink.Text");
            accessibleFaq.HRef = GetResource("AccessibleOptionsControl.FaqLink.Href");
        }

        /// <summary>
        /// Updates the display class of the options content
        /// </summary>
        private void UpdateOptionsVisibility(bool showExpanded)
        {
            if (showExpanded)
            {
                if (!optionContentRow.Attributes["class"].Contains("show"))
                    optionContentRow.Attributes["class"] = string.Format("{0} show",
                        optionContentRow.Attributes["class"].Replace("hide", string.Empty));
            }
            else
            {
                if (!optionContentRow.Attributes["class"].Contains("hide"))
                    optionContentRow.Attributes["class"] = string.Format("{0} hide",
                        optionContentRow.Attributes["class"].Replace("show", string.Empty));
            }
        }

        /// <summary>
        /// Updates the display state of the control
        /// </summary>
        private void UpdateControlDisplayMode()
        {
            switch (displayMode)
            {
                case GenericDisplayMode.Ambiguity:
                case GenericDisplayMode.ReadOnly:
                    if (SelectedAccessibilityOption != AccessibleOptionsType.NoRequirement)
                    {
                        // Set and display the selected accessible text
                        lblAccessibleOptionSelected.Text =
                            populator.GetText(DataServiceType.AccessibleOptionsRadio, 
                                ((int)(SelectedAccessibilityOption) + 1).ToString());

                        if (checkFewestChanges.Checked)
                            lblAccessibleOptionSelected.Text = string.Format("{0}<br />{1}",
                                lblAccessibleOptionSelected.Text, checkFewestChanges.Text);

                        pnlAccessibleOptionsReadonly.Visible = true;
                        pnlAccessibleOptions.Visible = false;
                    }
                    else
                    {
                        // Hide both
                        pnlAccessibleOptionsReadonly.Visible = false;
                        pnlAccessibleOptions.Visible = false;
                    }
                    break;
                case GenericDisplayMode.Normal:
                default:
                    pnlAccessibleOptions.Visible = true;
                    pnlAccessibleOptionsReadonly.Visible = false;
                    break;
            }
        }

        #endregion
    }
}
