// *********************************************** 
// NAME                 : FindCycleJourneyTypeControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 10 Jul 2008
// DESCRIPTION          : Displays cycle journey type dropdown 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindCycleJourneyTypeControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Sep 09 2008 13:17:48   mmodi
//Updated to load lists when setting dropdown value
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Jul 28 2008 13:09:54   mmodi
//Updates
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jul 18 2008 14:04:16   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Cycle journey type options
    /// </summary>
    public partial class FindCycleJourneyTypeControl : TDUserControl
    {
        #region Private variables

        private IDataServices populator;

        private GenericDisplayMode journeyTypeDisplayMode = GenericDisplayMode.Normal;

        #endregion

        #region Page)Init, Page_Load, PagePreRender

        /// <summary>
        /// Handler for the Init event. Sets up global variables and additional event handlers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, System.EventArgs e)
        {
            populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
        }

        /// <summary>
        /// Page_Load event handler
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadLists();
        }

        /// <summary>
		/// Sets visibility of controls according to the property values
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            LoadResources();

            UpdatePreferencesControl();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the lists
        /// </summary>
        private void LoadLists()
        {
            int listJourneyTypeIndex = listJourneyType.SelectedIndex;

            populator.LoadListControl(DataServiceType.CycleJourneyType, listJourneyType);

            listJourneyType.SelectedIndex = listJourneyTypeIndex;
        }

        /// <summary>
        /// Loads the text labels
        /// </summary>
        private void LoadResources()
        {
            labelTypeOfJourney.Text = GetResource("CyclePlanner.FindCycleJourneyTypeControl.labelTypeOfJourney");
            labelFind.Text = GetResource("CyclePlanner.FindCycleJourneyTypeControl.labelFind");
            labelJourneys.Text = GetResource("CyclePlanner.FindCycleJourneyTypeControl.labelJourneys");
        }

        /// <summary>
		/// Sets the state of the preferences controls.  
		/// </summary>
        private void UpdatePreferencesControl()
        {
            #region Journey Type
            switch (journeyTypeDisplayMode)
            {
                case GenericDisplayMode.ReadOnly:
                case GenericDisplayMode.Ambiguity:

                    labelFind.Visible = false;
                    labelJourneys.Visible = false;
                    listJourneyType.Visible = false;

                    if (this.PageId == PageId.FindCycleInput)
                    {
                        displayJourneyTypeLabel.Visible = true;

                        // e.g. "Quickest journey(s)
                        displayJourneyTypeLabel.Text =
                            populator.GetText(DataServiceType.CycleJourneyType, JourneyType)
                            + " "
                            + labelJourneys.Text;
                    }
                    break;

                case GenericDisplayMode.Normal:
                default:
                    displayJourneyTypeLabel.Visible = false;
                    labelFind.Visible = true;
                    labelJourneys.Visible = true;
                    listJourneyType.Visible = true;

                    break;
            }
            #endregion
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets/Sets the Journey type dropdown list
        /// </summary>
        public string JourneyType
        {
            get
            {
                if (listJourneyType.Items.Count <= 0)
                {
                    LoadLists();
                }

                return populator.GetValue(DataServiceType.CycleJourneyType, listJourneyType.SelectedItem.Value);
            }
            set
            {
                if (listJourneyType.Items.Count <= 0)
                {
                    LoadLists();
                }

                string listJourneyTypeId = populator.GetResourceId(DataServiceType.CycleJourneyType, value);
                populator.Select(listJourneyType, listJourneyTypeId);
            }
        }

        /// <summary>
        /// Sets the mode for the journey type drop down.
        /// Note that in this context, GenericDisplayMode.Ambiguity is treated
        /// the same way as GenericDisplayMode.Readonly.
        /// </summary>
        public GenericDisplayMode JourneyTypeDisplayMode
        {
            get { return journeyTypeDisplayMode; }
            set { journeyTypeDisplayMode = value; }
        }

        #endregion
    }
}