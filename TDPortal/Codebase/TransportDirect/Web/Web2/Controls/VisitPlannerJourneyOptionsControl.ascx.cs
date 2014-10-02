// ***************************************************************** 
// NAME                 : VisitPlannerJourneyOptionsControl.ascx.cs 
// AUTHOR               : Tolu Olomolaiye 
// DATE CREATED         : 31/08/2005
// DESCRIPTION			: A container control for the JourneyLineControl
// and RouteSelectionControl controls
// ***************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/VisitPlannerJourneyOptionsControl.ascx.cs-arc  $
//
//   Rev 1.3   Dec 17 2008 11:27:06   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:23:38   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:42   mturner
//Initial revision.
//
//   Rev 1.12   Feb 23 2006 16:14:30   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.11   Jan 25 2006 12:08:14   pcross
//Removed unnecessary tooltip
//Resolution for 3505: UEE: Inconsistency in use of tooltips
//
//   Rev 1.10   Nov 14 2005 12:13:02   halkatib
//Modification for IR3011 to change the image buttons to tdbuttons used in the UEE changes
//Resolution for 3011: UEE: Image Buttons still on VP Results page
//
//   Rev 1.9   Nov 10 2005 14:04:34   tolomolaiye
//Set EnableViewState = false for controls that do not need it
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.8   Oct 29 2005 15:53:12   jbroome
//Removed header row and exposed OK button as public
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.7   Oct 28 2005 14:59:38   tolomolaiye
//Changes from code review and running fxcop
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.6   Oct 18 2005 14:54:08   tolomolaiye
//Removed redundant code
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.5   Oct 11 2005 17:47:36   tolomolaiye
//Updates to drop down lists
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.4   Oct 05 2005 09:44:28   tolomolaiye
//Updates following code review and fxcop
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.3   Sep 16 2005 16:12:38   tolomolaiye
//Check in for review
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Sep 14 2005 11:26:02   tolomolaiye
//Work in progress
//
//   Rev 1.1   Sep 05 2005 17:51:18   tolomolaiye
//Check-in for review
//
//   Rev 1.0   Sep 02 2005 10:55:34   tolomolaiye
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using TransportDirect.Common.ResourceManager;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.Common.ServiceDiscovery;

	/// <summary>
	///		Container for JourneyLineControl and RouteSelectionControl
	/// </summary>
	public partial class VisitPlannerJourneyOptionsControl : TDUserControl
	{
		protected JourneyLineControl journeyLineVisitPlanner;
		protected RouteSelectionControl routeSelectionControl1;
		protected RouteSelectionControl routeSelectionControl2;
		protected RouteSelectionControl routeSelectionControl3;

		private IDataServices populator;
		public event CommandEventHandler OKCommand;

		/// <summary>
		/// Constructor method for VisitPlannerJourneyOptionsControl
		/// </summary>
		public VisitPlannerJourneyOptionsControl()
		{
			this.LocalResourceManager = TDResourceManager.VISIT_PLANNER_RM;
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
            int indexShowSelection = ShowSelection.SelectedIndex;
			// Put user code to initialize the page here
						
			//get the string for the "Show" label
			labelShow.Text = GetResource("VisitPlannerJourneyOptionsControl.labelShow.Text");
			OK.Text = GetResource("VisitPlannerJourneyOptionsControl.commandOK.Text");			
			OK.Command += OKCommand;
			
			populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			populator.LoadListControl(DataServiceType.VisitPlannerRouteSelection, ShowSelection, this.resourceManager);
            ShowSelection.SelectedIndex = indexShowSelection;
		}

		#region Properties for route selection control

		/// <summary>
		/// Read only. Get the details of the JourneyLineControl
		/// </summary>
		public JourneyLineControl JourneyLine
		{
			get { return journeyLineVisitPlanner; }
		}
		
		/// <summary>
		/// Read only. Exposes the drop down list as a property
		/// </summary>
		public DropDownList ShowSelection
		{
			get {return ShowSelectionDropDown;}
		}

		/// <summary>
		/// Read/write property. Exposes the RouteSelectionControl SelectionControl1 as a property
		/// </summary>
		public RouteSelectionControl SelectionControl1
		{
			get {return routeSelectionControl1;}
			set {routeSelectionControl1 = value;}
		}


		/// <summary>
		/// Read/write property. Exposes the RouteSelectionControl SelectionControl2 as a property
		/// </summary>
		public RouteSelectionControl SelectionControl2
		{
			get {return routeSelectionControl2;}
			set {routeSelectionControl2 = value;}
		}

		/// <summary>
		/// Read/write property. Exposes the RouteSelectionControl SelectionControl3 as a property
		/// </summary>
		public RouteSelectionControl SelectionControl3
		{
			get {return routeSelectionControl3;}
			set {routeSelectionControl3 = value;}
		}

		/// <summary>
		/// Read only property. Exposes the OK button as a property
		/// </summary>
		public TDButton OkButton
		{
			get { return OK; }
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
