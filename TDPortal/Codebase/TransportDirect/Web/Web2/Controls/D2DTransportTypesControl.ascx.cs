// ************************************************************************************************ 
// NAME                 : D2DTransportTypesControl.ascx.cs 
// AUTHOR               : David Lane 
// DATE CREATED         : 09/01/2013 
// DESCRIPTION			: Displays a tick box for each mode of transport for Input pages. 
// ************************************************************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/D2DTransportTypesControl.ascx.cs-arc  $
//
//   Rev 1.5   Jan 30 2013 13:47:52   mmodi
//Fixed showing advanced options on ambiguity page
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.3   Jan 20 2013 16:26:30   mmodi
//Updated for transport modetype Telecabine
//Resolution for 5884: CCN:XXX - Telecabine modetype
//
//   Rev 1.1   Jan 17 2013 09:46:06   mmodi
//Updates to D2D advanced options for better js and non-js behaviour
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Jan 10 2013 16:34:10   dlane
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

using ControlPopulator = TransportDirect.UserPortal.DataServices.DataServices;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///		Summary description for TransportTypesContol.
	/// </summary>
	public partial class D2DTransportTypesControl :  TDUserControl
    {
        #region Private members

        protected System.Web.UI.WebControls.Panel transportOptionsPanel;

		private ControlPopulator populator;

        #endregion

        /// <summary>
		///  Default constructor, retreives and set data services populator
		/// </summary>
		public D2DTransportTypesControl()
		{
			populator = (ControlPopulator)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
		}
                
		/// <summary>
		/// Method sets label text using resource strings
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
        }

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
        /// Resets the control and its state
        /// </summary>
        public void Reset()
        {
            ModeType[] publicModes = new ModeType[]
				{
					ModeType.Air,
					ModeType.Bus,
					ModeType.Car,
					ModeType.Coach,
					ModeType.Ferry,
					ModeType.Metro,
					ModeType.Rail,
                    ModeType.Telecabine,
					ModeType.Tram,
					ModeType.Underground,
					ModeType.Walk
				};

            PublicModes = publicModes;

            UpdateOptionsVisibility(false);

            btnShow.Visible = true;
        }

        #endregion

        #region Public properties

        /// <summary>
		/// Read/write 
		/// Array of public transport modes.
		/// </summary>
		public ModeType[] PublicModes
		{
			get
			{
				ArrayList modes = new ArrayList();

                // Loop through each check box and build up list of 
                // associated mode types
                for (int i = 0; i < checklistModesPublicTransport.Items.Count; i++)
                {
                    foreach (ModeType mode in SelectedModes(i))
                    {
                        modes.Add(mode);
                    }
                }

				// Return list of all mode types for selected check boxes
				return (ModeType[])modes.ToArray(typeof(ModeType));
			}

			set
			{
				checklistModesPublicTransport.SelectedIndex = -1;

				// Check all relevant check boxes associated with mode type array
				foreach (ModeType type in value)
				{
					string resourceId = populator.GetResourceId(
						DataServiceType.PublicTransportsCheck, 
						Enum.GetName(typeof(ModeType),type));
					switch (type)
					{
						case ModeType.Air:
							populator.SelectInCheckBoxList(checklistModesPublicTransport, resourceId);	
							break;
						case ModeType.Bus:
							populator.SelectInCheckBoxList(checklistModesPublicTransport, resourceId);	
							break;
						case ModeType.Ferry:
							populator.SelectInCheckBoxList(checklistModesPublicTransport, resourceId);	
							break;
						case ModeType.Rail:
							populator.SelectInCheckBoxList(checklistModesPublicTransport, resourceId);	
							break;
                        case ModeType.Telecabine:
                            populator.SelectInCheckBoxList(checklistModesPublicTransport, resourceId);
                            break;
						case ModeType.Tram:
							populator.SelectInCheckBoxList(checklistModesPublicTransport, resourceId);	
							break;
						case ModeType.Underground:
							populator.SelectInCheckBoxList(checklistModesPublicTransport, resourceId);	
							break;
					}
				}
			}
        }

        /// <summary>
        /// Read/Write - Allows access to the checklistModesPublicTransport CheckBoxList
        /// </summary>
        public CheckBoxList ModesPublicTransport
        {
            get { return checklistModesPublicTransport; }
            set { checklistModesPublicTransport = value; }
        }

        /// <summary>
        /// Read only. Returns true if non-default options have been selected
        /// </summary>
        public bool IsOptionSelected
        {
            get
            {
                return !ModesIsDefault();
            }
        }

        #endregion

        #region Private methods

        /// <summary>
		///  Returns array of selected public transport modes.
		/// </summary>
		/// <param name="index">The index of the selected mode</param>
		/// <returns></returns>
		private ModeType[] SelectedModes( int index)
		{
		
			ListItem item = checklistModesPublicTransport.Items[index];
		
			if (item.Selected)
			{

				// get ModeType from DataService
				ModeType type = (ModeType)
					Enum.Parse(typeof(ModeType), populator.GetValue
					(DataServiceType.PublicTransportsCheck, item.Value));

				// Determine the mode(s) associated with the selected check box
				switch (type)
				{
					case ModeType.Rail:		
						return new ModeType[]{ModeType.Rail};
					case ModeType.Bus:
						return new ModeType[]{ModeType.Bus, ModeType.Coach};
					case ModeType.Underground:
						return new ModeType[]{ModeType.Metro, ModeType.Underground};
                    case ModeType.Telecabine:
                        return new ModeType[] { ModeType.Telecabine};
					case ModeType.Tram:
						return new ModeType[]{ModeType.Tram};
					case ModeType.Ferry:
						return new ModeType[]{ModeType.Ferry};
					case ModeType.Air:
						return new ModeType[]{ModeType.Air};
					default:
						return new ModeType[0];

				}
			}
			else
				// If nothing selected, return empty array
				return new ModeType[0];
		}

        /// <summary>
        /// Setsup resources and content
        /// </summary>
        private void SetupResources()
        {
            labelPublicModesNote.Text = GetResource("transportTypesPanel.labelPublicModesNote");
            labelType.Text = GetResource("TransportTypesControl.Type");
            labelTickAllTypes.Text = GetResource("TransportTypesControl.TickAll");

            labelJsQuestion.Text = GetResource("TransportTypesControl.Question");
            labelOptionsSelected.Text = GetResource("TransportTypesControl.OptionsSelected");

            btnShow.Text = GetResource("AdvancedOptions.Show.Text");
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
        /// Returns true if the selected transport modes are default
        /// </summary>
        /// <returns></returns>
        private bool ModesIsDefault()
        {
            if (checklistModesPublicTransport.Items.Count > 0)
            {
                foreach (ListItem item in checklistModesPublicTransport.Items)
                {
                    if (!item.Selected)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion
    }
}
