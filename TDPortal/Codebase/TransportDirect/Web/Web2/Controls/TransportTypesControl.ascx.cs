// ************************************************************************************************ 
// NAME                 : TransportTypesControl.ascx.cs 
// AUTHOR               : Andrew Sinclair 
// DATE CREATED         : 22/09/2005 
// DESCRIPTION			: Displays a tick box for each mode of transport for Input pages. 
// ************************************************************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TransportTypesControl.ascx.cs-arc  $
//
//   Rev 1.3   Jan 20 2013 16:27:08   mmodi
//Updated for transport modetype Telecabine
//Resolution for 5884: CCN:XXX - Telecabine modetype
//
//   Rev 1.2   Mar 31 2008 13:23:22   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:20   mturner
//Initial revision.
//
//   Rev 1.3   Feb 23 2006 16:14:08   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.2   Dec 22 2005 11:02:56   jbroome
//Added/updated comments after code review
//
//   Rev 1.1   Nov 03 2005 17:04:26   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.0.1.0   Nov 01 2005 16:13:32   mtillett
//Set the resource manager to use
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.0   Oct 19 2005 17:35:48   asinclair
//Initial revision.
//
//   Rev 1.3   Oct 12 2005 16:17:18   asinclair
//Added comments
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Oct 12 2005 11:20:56   asinclair
//Added properties
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Oct 10 2005 12:38:02   asinclair
//Added labels needed in control
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Sep 28 2005 14:27:22   asinclair
//Initial revision.

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

	/// <summary>
	///		Summary description for TransportTypesContol.
	/// </summary>
	public partial class TransportTypesControl :  TDUserControl
	{
		protected System.Web.UI.WebControls.Panel transportOptionsPanel;

		private ControlPopulator populator;
		

		/// <summary>
		///  Default constructor, retreives and set data services populator
		/// </summary>
		public TransportTypesControl()
		{
			populator = (ControlPopulator)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
		}



		/// <summary>
		/// Method sets label text using resource strings
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.LocalResourceManager = TDResourceManager.LANG_STRINGS;
			labelPublicModesNote.Text = GetResource ("transportTypesPanel.labelPublicModesNote");
			labelType.Text = GetResource ("TransportTypesControl.Type");
			labelTickAllTypes.Text = GetResource ("TransportTypesControl.TickAll");
		}

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
				for (int i = 0; i< checklistModesPublicTransport.Items.Count; i++)
				{
					foreach ( ModeType mode in selectedModes(i))
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
		///  Returns array of selected public transport modes.
		/// </summary>
		/// <param name="index">The index of the selected mode</param>
		/// <returns></returns>
		private ModeType[] selectedModes( int index)
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
                        return new ModeType[] { ModeType.Telecabine };
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
		/// Read/Write - Allows access to the checklistModesPublicTransport CheckBoxList
		/// </summary>
		public CheckBoxList ModesPublicTransport
		{
			get {return checklistModesPublicTransport;}
			set {checklistModesPublicTransport = value;}
		}
	
	}
}
