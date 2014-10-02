// ************************************************************************************************ 
// NAME                 : AccessibleTransportTypesControl.ascx.cs 
// AUTHOR               : David Lane
// DATE CREATED         : 30/11/2012 
// DESCRIPTION			: Displays a tick box for each mode of transport for accessible pages. 
// ************************************************************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AccessibleTransportTypesControl.ascx.cs-arc  $
//
//   Rev 1.6   Jan 17 2013 08:30:28   DLane
//Adding aria attributes
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.5   Jan 15 2013 13:29:48   mmodi
//Added switch to display localities transport type
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.4   Jan 09 2013 16:10:24   mmodi
//Error message display updates
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.3   Jan 04 2013 16:44:52   mmodi
//Find nearest accessible stop page updates for javascript disabled
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.2   Jan 04 2013 15:37:40   mmodi
//Display updates
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Dec 18 2012 16:54:36   dlane
//Accessible JP updates
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Dec 07 2012 16:00:26   dlane
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//

namespace TransportDirect.UserPortal.Web.Controls
{
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

	using ControlPopulator = TransportDirect.UserPortal.DataServices.DataServices;
    using TransportDirect.UserPortal.LocationService;
    using TransportDirect.Common.PropertyService.Properties;

	/// <summary>
	///		Summary description for TransportTypesContol.
	/// </summary>
	public partial class AccessibleTransportTypesControl :  TDUserControl
    {
        #region  Instance members

        protected System.Web.UI.WebControls.Panel transportOptionsPanel;

		private ControlPopulator populator;

        #endregion

        #region Constructor

        /// <summary>
		///  Default constructor, retreives and set data services populator
		/// </summary>
		public AccessibleTransportTypesControl()
		{
			populator = (ControlPopulator)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
        }

        #endregion

        #region Page_Init, Page_Load

        /// <summary>
        /// Page_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{
            SetupStopTypeControl();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupResources();
        }

        #endregion

        #region Public properties

        /// <summary>
		/// Read/write 
		/// Array of public transport modes.
		/// </summary>
		public TDStopType[] AccessibleModes
		{
            get
			{
                // Ensure transport types control is populated in case this property is called 
                // prior to the page load event
                SetupStopTypeControl();

				ArrayList stopTypes = new ArrayList();
					
				// Loop through each check box and build up list of 
				// associated stop types
                for (int i = 0; i < checklistModesAccessibleTransport.Items.Count; i++)
				{
					foreach ( TDStopType stopType in selectedStopTypes(i))
					{
                        stopTypes.Add(stopType);
					}
				}

				// Return list of all mode types for selected check boxes
                return (TDStopType[])stopTypes.ToArray(typeof(TDStopType));
			}

			set
			{
                checklistModesAccessibleTransport.SelectedIndex = -1;

				// Check all relevant check boxes associated with mode type array
                foreach (TDStopType type in value)
				{
                    populator.SelectInCheckBoxList(checklistModesAccessibleTransport, type.ToString());
				}
			}
        }

        /// <summary>
		/// Read/Write - Allows access to the checklistModesPublicTransport CheckBoxList
		/// </summary>
		public CheckBoxList ModesAccessibleTransport
		{
            get { return checklistModesAccessibleTransport; }
            set { checklistModesAccessibleTransport = value; }
		}

        /// <summary>
        /// Read only - Returns true if all accessible transport modes are checked
        /// </summary>
        public bool ModesAccessibleTransportAllChecked
        {
            get
            {
                foreach (ListItem item in checklistModesAccessibleTransport.Items)
                {
                    if (!item.Selected)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Read/Write - Allows access to the update button (used when javascript disabled)
        /// </summary>
        public TDButton UpdateButton
        {
            get { return btnUpdate; }
            set { btnUpdate = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialises and sets up the transport stop type accessible modes
        /// </summary>
        private void SetupStopTypeControl()
        {
            if (checklistModesAccessibleTransport.Items.Count == 0)
            {
                if (!Page.IsPostBack)
                {
                    // Populate Transport Types checkBoxes
                    populator.LoadListControl(DataServiceType.AccessibleTransportTypes, checklistModesAccessibleTransport);

                    // Remove any transport types if needed
                    UpdateAccessibleTransportList();

                    // Set the transportModes values - first pass use all as not one to one with ModeTypes
                    AccessibleModes = new TDStopType[] { 
                    TDStopType.Rail, TDStopType.Bus, TDStopType.Underground, 
                    TDStopType.LightRail, TDStopType.Ferry, TDStopType.DLR, TDStopType.Locality };
                }
            }

            checklistModesAccessibleTransport.RepeatColumns = (int)Math.Ceiling((double)checklistModesAccessibleTransport.Items.Count / 2);
        }

        /// <summary>
        /// Updates the transport type checkbox list, removes any types turned off
        /// </summary>
        private void UpdateAccessibleTransportList()
        {
            // Check if locality should be shown
            bool showLocality = false;
            if (!bool.TryParse(Properties.Current["AccessibleOptions.TransportTypes.Locality.Switch"], out showLocality))
                showLocality = false;

            if (!showLocality)
            {
                ListItem removeItem = null;
                foreach (ListItem item in checklistModesAccessibleTransport.Items)
                {
                    if (item.Value == TDStopType.Locality.ToString())
                    {
                        removeItem = item;
                    }
                }

                if (removeItem != null)
                {
                    // Remvoe the item
                    checklistModesAccessibleTransport.Items.Remove(removeItem);
                }
            }
        }

        /// <summary>
        /// Setsup resource strings
        /// </summary>
        private void SetupResources()
        {
            labelInstructions.Text = GetResource("AccessibleTransportTypesControl.FindTDAN");
            btnUpdate.Text = GetResource("AccessibleTransportTypesControl.Update");
        }

        /// <summary>
		///  Returns array of selected public transport modes.
		/// </summary>
		/// <param name="index">The index of the selected mode</param>
		/// <returns></returns>
		private TDStopType[] selectedStopTypes( int index)
		{
            ListItem item = checklistModesAccessibleTransport.Items[index];
		
			if (item.Selected)
			{
				// get stop type from DataService
				TDStopType type = (TDStopType)
					Enum.Parse(typeof(TDStopType), populator.GetValue
					(DataServiceType.AccessibleTransportTypes, item.Value));

				// Determine the mode(s) associated with the selected check box
				switch (type)
				{
					case TDStopType.Rail:		
						return new TDStopType[] { TDStopType.Rail };
                    case TDStopType.Bus:
                        return new TDStopType[] { TDStopType.Bus, TDStopType.Coach };
                    case TDStopType.Underground:
                        return new TDStopType[] { TDStopType.Underground };
                    case TDStopType.DLR:
                        return new TDStopType[] { TDStopType.DLR };
                    case TDStopType.Ferry:
                        return new TDStopType[] { TDStopType.Ferry };
                    case TDStopType.LightRail:
                        return new TDStopType[] { TDStopType.LightRail };
                    case TDStopType.Locality:
                        return new TDStopType[] { TDStopType.Locality };
					default:
                        return new TDStopType[0];

				}
			}
			else
				// If nothing selected, return empty array
				return new TDStopType[0];
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
	}
}
