// *********************************************** 
// NAME                 : ResultsViewSelectionControl.ascx.cs 
// AUTHOR               : Paul Cross
// DATE CREATED         : 15/12/2005
// DESCRIPTION			: A user control to provide a dropdown list of pages that can be
//						  accessed from the current page
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ResultsViewSelectionControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:22:44   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:22   mturner
//Initial revision.
//
//   Rev 1.4   Mar 20 2006 18:07:12   pcross
//Code review updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Mar 14 2006 13:20:08   pcross
//Added header, headelementcontrol, resource manager reference
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Feb 07 2006 18:45:16   NMoorhouse
//Updated so OK button text is always displayed
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Jan 26 2006 16:40:04   AViitanen
//Minor Fxcop changes
//
//   Rev 1.0   Dec 21 2005 19:33:22   pcross
//Initial revision.
//

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.Common.ResourceManager;

	[System.Runtime.InteropServices.ComVisible(false)]
	/// <summary>
	///		Summary description for ResultsViewSelectionControl.
	/// </summary>
	public partial class ResultsViewSelectionControl : TDPrintableUserControl
	{

		

		#region Properties and Events
		
		private IDataServices populator;
		private DataServiceType listType;
		
		/// <summary>
		/// Read/write property to handle the list type to use to obtain the list
		/// of possible options for the dropdown list
		/// </summary>
		public DataServiceType ListType
		{
			get { return listType; }
			set { listType = value; }
		}

		/// <summary>
		/// Read only property exposing the dropdown list control
		/// </summary>
		public DropDownList ViewSelection
		{
			get {return this.viewSelectionDropDown; }
		}
		
		/// <summary>
		/// Read only property exposing the OK button
		/// </summary>
		public TDButton OKButton
		{
			get {return this.okButton; }
		}


		#endregion
		

		#region Constructor

		/// <summary>
		/// Contructor for control
		/// </summary>
		public ResultsViewSelectionControl()
		{
			// Set the resource file for the control
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
		}

		#endregion


		#region Page Initialisation

		/// <summary>
		/// Load control data
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			PopulateControls();
		
			okButton.Text = GetResource("ResultsViewSelectionControl.OKButton.Text");
		}

		#endregion


		/// <summary>
		/// Populate the controls with default data
		/// </summary>
		private void PopulateControls()
		{
            int index = viewSelectionDropDown.SelectedIndex;
			selectionLabel.Text = GetResource("ResultsViewSelectionControl.SelectionLabel.Text");

			// Update the dropdown list with the data specified by the supplied list type
			populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			populator.LoadListControl(listType, viewSelectionDropDown, this.resourceManager);
            viewSelectionDropDown.SelectedIndex = index;
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
	}
}
