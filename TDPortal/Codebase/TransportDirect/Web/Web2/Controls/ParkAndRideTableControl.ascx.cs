// *********************************************** 
// NAME                 : ParkAndRideTableControl.ascx.cs 
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 03/08/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ParkAndRideTableControl.ascx.cs-arc  $
//
//   Rev 1.4   Sep 24 2009 10:57:58   apatel
//updated aspx page so alternate link works
//Resolution for 5326: Drive to Park and Ride links failing
//
//   Rev 1.3   Dec 17 2008 11:26:58   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:22:20   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:54   mturner
//Initial revision.
//
//   Rev 1.10   May 03 2006 16:14:46   tmollart
//Added code/resources for "Drive To" buttons for park and ride.
//Resolution for 4058: DN058 Park & Ride Phase 2: No Welsh text for 'Drive To' button
//
//   Rev 1.9   Apr 28 2006 12:11:18   esevern
//Set one use key for park and ride destination containing the selected park and ride scheme id
//Resolution for 4015: DN058 Park and Ride Phase 2 - Park & ride site on input page not automatically populated
//
//   Rev 1.8   Apr 26 2006 11:14:46   esevern
//removed initialisation of journey parameters - will only be done if there aren't any existing ones (otherwise it over-rides settings already done for FindCarInput)
//Resolution for 3952: DN058 Park & Ride Phase 2 - Ambiguity page error for Park and Ride schemes selected through Find a Car
//Resolution for 3997: DN058 Park & Ride Phase 2: Park and ride scheme planning error
//
//   Rev 1.7   Apr 20 2006 15:29:16   esevern
//Corrected 'driveTo_Command' event handler to redirect user to FindCarInput page, not ParkAndRideInput page.
//Resolution for 3952: DN058 Park & Ride Phase 2 - Ambiguity page error for Park and Ride schemes selected through Find a Car
//
//   Rev 1.6   Apr 07 2006 10:06:02   rgreenwood
//IR3790: Added logic to hide the column containing Drive To buttons when this control appears on a PrinterFriendly page
//Resolution for 3790: DN058 Park and Ride Phase 2 - Printer friendly buttons are links
//
//   Rev 1.5   Mar 23 2006 17:58:50   build
//Automatically merged from branch for stream0025
//
//   Rev 1.4.1.6   Mar 20 2006 19:02:48   tolomolaiye
//Updated with code review comments
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.4.1.5   Mar 20 2006 13:56:08   halkatib
//Set control type to default in order to set control to an input state
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.4.1.4   Mar 16 2006 16:12:52   halkatib
//Created an event handler for the repeater on the page and dynamically added event handlers to the buttons in the table rows.
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.4.1.3   Mar 15 2006 11:59:08   halkatib
//Apllied changes required by td locatio contructor redesign
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.4.1.2   Mar 14 2006 10:31:58   halkatib
//Changes made for park and ride phase 2
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.4.1.1   Mar 10 2006 16:47:20   halkatib
//changes for park and ride phase 2
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.4.1.0   Mar 07 2006 11:12:48   halkatib
//Changes made by ParkandRide phase 2
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.4   Feb 23 2006 19:17:00   build
//Automatically merged from branch for stream3129
//
//   Rev 1.3.1.0   Jan 10 2006 15:26:40   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Sep 28 2005 15:03:36   schand
//Fix for IR2806. 
//Repeater header updated.  
//No data message now appears the cenre of the empty box.
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.2   Sep 02 2005 15:11:16   NMoorhouse
//Updated following review comments (CR003)
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.1   Aug 23 2005 12:11:26   NMoorhouse
//Park And Ride - Changes following review comments
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.0   Aug 12 2005 13:17:34   NMoorhouse
//Initial revision.
//
namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.LocationService;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.Web.Support;
	using TransportDirect.Common;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.Resource;

	/// <summary>
	///	Summary description for ParkAndRideTableControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class ParkAndRideTableControl : TDPrintableUserControl
	{
		protected System.Web.UI.WebControls.Label labelLocation;
		protected System.Web.UI.WebControls.Label labelComments;
		protected TransportDirect.UserPortal.Web.Controls.TDButton driveToButton;
		protected TransportDirect.UserPortal.Web.Controls.TDButton driveToAltButton;

		private ITDSessionManager sessionManager = TDSessionManager.Current;
		private ParkAndRideInfo[] data;

		#region Control event handlers
		/// <summary>
		/// Handler for the load event. Sets up the controls.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//Define and hide the 'No Park and Ride' label.
			labelNoParkAndRides.Text = GetResource(
				"ParkAndRideTableControl.labelNoParkAndRides.Text");
			labelNoParkAndRides.Visible = false;

			//Display Park and Ride Data
			PopulateParkAndRideData();
		}

		/// <summary>
		/// PreRender event handler.
		/// Updates Park and Ride Data. This may have changed since Page Load if user has 
		/// changed selection.
		/// </summary>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			//Update Park and Ride Data
			PopulateParkAndRideData();
		}

		
		/// <summary>
		/// click event for the drive to and drive to alt buttons. 
		/// </summary>
		/// <param name="sender">The sender object</param>
		/// <param name="e">The command argument</param>
		private void driveTo_Command(object sender, CommandEventArgs e)
		{
			
			// If there have been no journey planner parameters set up then do so now
			if (TDSessionManager.Current.JourneyParameters == null)
			{
				sessionManager.InitialiseJourneyParameters(FindAMode.Car);
			}

			int selectedParkAndRideID = Convert.ToInt32(e.CommandName, TDCultureInfo.CurrentCulture.NumberFormat );			 
			
			// -1 passed to tdlocation constructor since the carparkid is not known.
			sessionManager.JourneyParameters.DestinationLocation = new TDLocation(selectedParkAndRideID, -1);
			sessionManager.JourneyParameters.Destination = new LocationSearch();
			sessionManager.JourneyParameters.Destination.SearchType = SearchType.ParkAndRide;

			//set location to fixed and valid in order for the control to appear resolved. 
			sessionManager.JourneyParameters.Destination.LocationFixed = true;
			sessionManager.JourneyParameters.DestinationLocation.Status = TDLocationStatus.Valid;
	
			//set one use key to park and ride and do a form shift to the park and ride input page
			sessionManager.SetOneUseKey(SessionKey.ParkAndRideDestination, e.CommandName);
			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.ParkAndRideInput;	
		}

		private void TableRowRepeaterItemCreated(object sender, RepeaterItemEventArgs e)
		{
			HtmlTableCell cellTag;

					// create command events for the drive to and drive to alt buttons
					switch (e.Item.ItemType)
					{
						case ListItemType.Item :
						case ListItemType.AlternatingItem :

						// Delete column if printerfriendly page is being viewed
						cellTag = (HtmlTableCell)e.Item.FindControl("buttonColumn");

							if (cellTag != null)
							{

								if (e.Item.FindControl("driveToAltButton") != null)
								{
									if (!PrinterFriendly)
									{
										driveToAltButton = e.Item.FindControl("driveToAltButton") as TDButton;
										driveToAltButton.Text = GetResource("ParkAndRideTableControl.DriveToButton.Text");
										driveToAltButton.Command += new CommandEventHandler(driveTo_Command);
									}
									else
									{
										//Hide the buttonColumn in PrinterFriendly mode
										cellTag.Visible = false;
									}
								}
								else if (e.Item.FindControl("driveToButton") != null)
								{
									if (!PrinterFriendly)
									{
										driveToButton = e.Item.FindControl("driveToButton") as TDButton;
										driveToButton.Text = GetResource("ParkAndRideTableControl.DriveToButton.Text");
										driveToButton.Command += new CommandEventHandler(driveTo_Command);
									}
									else
									{
										//Hide the buttonColumn in PrinterFriendly mode
										cellTag.Visible = false;
									}
								}
							}

								break;

						case ListItemType.Header:

							HtmlTableCell headerCellTag;

							// Delete headercolumn if printerfriendly page is being viewed
							headerCellTag = (HtmlTableCell)e.Item.FindControl("tnhButtonColumnHeader");;
						
							if (PrinterFriendly)
							{
								headerCellTag.Visible = false;
							}
							else
							{
								headerCellTag.Visible = true;
							}

							break;


						default:
							break;
					}
		}


		#endregion

		#region Methods
		/// <summary>
		/// Displays the park and ride data.
		/// </summary>
		private void PopulateParkAndRideData()
		{
			if (data != null)
			{	//Bind data to the Repeater
				repeaterResultTable.Visible = true;
				repeaterResultTable.DataSource = data;
				repeaterResultTable.DataBind();

				if (data.Length != 0)
				{
					labelNoParkAndRides.Visible = false;
				}
				else
				{
					//Hide Repeater and show 'No Park and Ride' Label
					labelNoParkAndRides.Visible = true;					
				}
			}
		}

		#endregion

		#region Public Methods		
		/// <summary>
		/// Returns the command name that should be associated with the map button.
		/// </summary>
		/// <param name="publicJourneyDetail">Current item being rendered.</param>
		/// <returns>Returns the command name.</returns>
		public string GetCommandName(ParkAndRideInfo parkAndRideInfo)
		{
			return parkAndRideInfo.ParkAndRideId.ToString(TDCultureInfo.CurrentUICulture);
		}
		#endregion

		#region Control Properties

		public ParkAndRideInfo[] Data
		{
			get { return data; }
			set { data = value; }
		}

		/// <summary>
		/// Gets the Resource Text for the label Location
		/// </summary>
		public string LabelLocationText
		{
			get
			{
				return GetResource(
					"ParkAndRideTableControl.labelLocation.Text");
			}
		}
		
		/// <summary>
		/// Gets the Resource Text for the label Comments
		/// </summary>
		public string LabelCommentText
		{
			get
			{
				return GetResource(
					"ParkAndRideTableControl.labelComments.Text");
			}
		}

		
		/// <summary>
		/// Method to get the Park And Ride Table summary
		/// </summary>
		/// <returns>Park And Ride Table summary Resource Text</returns>
		public string TableSummary()
		{
			return GetResource(
				"ParkAndRideTableControl.ParkAndRideTable.Summary");
		}

		/// <summary>
		/// Returns the specific location for Park and Ride
		/// </summary>
		/// <param name="containerDataItem">Park And Ride Data Item</param>
		/// <returns>Park And Ride Location</returns>
		public string LocationText(object containerDataItem)
		{
			return ((ParkAndRideInfo)containerDataItem).Location;
		}
		/// <summary>
		/// Returns the specific comment for Park and Ride
		/// </summary>
		/// <param name="containerDataItem">Park And Ride Data Item</param>
		/// <returns>Park And Ride Comments</returns>
		public string CommentsText(object containerDataItem)
		{
			return ((ParkAndRideInfo)containerDataItem).Comments;
		}
		/// <summary>
		/// Returns the specific url for Park and Ride
		/// </summary>
		/// <param name="containerDataItem">Park And Ride Data Item</param>
		/// <returns>Park And Ride URL</returns>
		public string LocationUrl(object containerDataItem)
		{
			if (!PrinterFriendly)
			{
				return ((ParkAndRideInfo)containerDataItem).UrlLink;
			}
			else
			{
				return null;
			}
		}
		/// <summary>
		/// Returns the url tool tip if Park And Ride was a URL
		/// </summary>
		/// <param name="containerDataItem">Park And Ride Data Item</param>
		/// <returns>URL Tool Tip </returns>
		public string UrlToolTip(object containerDataItem)
		{
			string urlLink = ((ParkAndRideInfo)containerDataItem).UrlLink;
			if (!PrinterFriendly && urlLink != null)
			{
				return GetResource(
					"ParkAndRideTableControl.ParkAndRideHyperlink.ToolTip");
			}
			else
			{
				return null;
			}
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
			this.repeaterResultTable.ItemCreated += new RepeaterItemEventHandler(TableRowRepeaterItemCreated);
		}
		#endregion


	}
}
