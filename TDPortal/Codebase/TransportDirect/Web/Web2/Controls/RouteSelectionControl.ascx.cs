// **************************************************** 
// NAME                 : RouteSelectionControl.ascx.cs 
// AUTHOR               : Tolu Olomolaiye 
// DATE CREATED         : 25/08/2005
// DESCRIPTION			: Displays a route selection
// **************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/RouteSelectionControl.ascx.cs-arc  $
//
//   Rev 1.4   Dec 17 2008 11:27:06   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Apr 02 2008 10:28:56   apatel
//RouteSelectionRepeater_ItemDataBound modified to show transport types as words instead of text
//
//   Rev 1.2   Mar 31 2008 13:22:54   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:38   mturner
//Initial revision.
//
//   Rev 1.21   Feb 23 2006 16:13:34   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.20   Jan 12 2006 14:05:00   RGriffith
//Fix fo IR3446 - Alternating background colour should not depend on wether or not javascript is enabled
//Resolution for 3446: Visit planner route selection table background colour
//
//   Rev 1.19   Nov 30 2005 14:09:40   jbroome
//Fixed missing table summary bug
//
//   Rev 1.18   Nov 29 2005 17:01:34   tolomolaiye
//Fix for IR 3241
//Resolution for 3241: Visit Planner - Selected journey not highlighted on printer friendly page
//
//   Rev 1.17   Nov 23 2005 17:06:56   jbroome
//Reverted back to previous version as this caused another problem. Row highlighting now handled by a change in VisitPlannerIntineraryManager
//
//   Rev 1.15   Nov 10 2005 10:17:42   jbroome
//Updated to use journeyindex value to set selected journeys
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.14   Nov 09 2005 15:31:28   rgreenwood
//TD089 ES020 Code review actions
//
//   Rev 1.13   Nov 09 2005 15:02:44   tolomolaiye
//Added styles
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.12   Oct 29 2005 15:49:04   jbroome
//Sorted out some layout issues with control and added new property
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.11   Oct 28 2005 17:24:08   jbroome
//Label in header no dependent on segment index
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.10   Oct 28 2005 14:59:22   tolomolaiye
//Changes from code review and running fxcop
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.9   Oct 20 2005 10:20:04   tolomolaiye
//Aligned some controls
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.8   Oct 18 2005 18:11:34   jbroome
//Re bind repeater in Pre Render
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.7   Oct 18 2005 14:50:22   tolomolaiye
//Replaced back and next image buttons with hyperlinks
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.6   Oct 11 2005 17:47:32   tolomolaiye
//Updates to drop down lists
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.5   Oct 05 2005 09:44:24   tolomolaiye
//Updates following code review and fxcop
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.4   Sep 22 2005 16:53:18   MTillett
//Fix the yellow highlighting of selected row (JavaScript)
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.3   Sep 16 2005 16:12:32   tolomolaiye
//Check in for review
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Sep 14 2005 11:25:34   tolomolaiye
//Work in progress
//
//   Rev 1.1   Sep 05 2005 17:51:24   tolomolaiye
//Check-in for review
//
//   Rev 1.0   Sep 02 2005 10:54:00   tolomolaiye
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;
    using TransportDirect.Common.PropertyService.Properties;


	/// <summary>
	///		Displays a route with associated earlier and later buttons
	/// </summary>
	public partial class RouteSelectionControl : TDPrintableUserControl
	{

		private const string RADIO_BUTTON = "routeRadioButton";
		private const string GROUP_NAME = "routeType";

		private const string ROW_HIGHLIGHTER_SCRIPT = "RowHighlighter";
		private const string SELECT_ROW = "selectRow";
		private const string ODD_ROW = "oddRow";
		private const string EVEN_ROW = "evenRow";


		// Enable navigation links by default
		private bool blnNavigationEnabled = true;

		#region Properties and Events
		
		private int CommandSegment;
		private int itemIndex ;
		private int itemJourneyIndex;
		private bool hasSelectionChanged;
		protected HyperlinkPostbackControl earlierHyperlink;
		protected HyperlinkPostbackControl laterHyperlink;
		private JourneySummaryLine[] dataSource; 

		public event CommandEventHandler EarlierCommand;

		public event CommandEventHandler LaterCommand;

		public event CommandEventHandler SelectionChangedEvent;

		/// <summary>
		/// Read only property. Returns the table summary
		/// </summary>
		/// <returns></returns>
		public string GetTableSummary
		{
			get {return GetResource(string.Format(TDCultureInfo.CurrentUICulture, "VisitPlannerJourneyOptionsControl.ctlRouteSelection{0}.Summary", CommandSegment+1));}	
		}

		/// <summary>
		/// Read/write property. get/sets the segment index for all controls
		/// </summary>
		public int CommandArgumentSegmentIndex
		{
			get { return CommandSegment; }
			set { CommandSegment = value; }
		}
		
		/// <summary>
		/// Read only property. Set the index of the scriptable radio button
		/// </summary>
		public int SelectedItemIndex
		{
			get { return itemIndex; }
		}

		/// <summary>
		/// Read/write property. Gets the journeyIndex of the 
		/// selected journey in the list. This is potentially 
		/// different from its index in the list.
		/// </summary>
		public int SelectedItemJourneyIndex
		{
			get { return itemJourneyIndex; }
			set { itemJourneyIndex = value; }
		}


		/// <summary>
		/// Read/write property. Enable/Disable the navigation buttons
		/// </summary>
		public bool EnableNavigation
		{
			get { return blnNavigationEnabled; }
			set { blnNavigationEnabled = value;	}		
		}

		/// <summary>
		/// Read-only property to determine if the user has selected another route
		/// </summary>
		public bool NewSelection
		{
			get{return hasSelectionChanged;}
		}

		#endregion

		/// <summary>
		/// Contructor for control
		/// </summary>
		public RouteSelectionControl()
		{
			this.LocalResourceManager = TDResourceManager.VISIT_PLANNER_RM;
		}

		/// <summary>
		/// Page Load event used to set up the scripts for scriptable radio button
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (((TDPage)Page).IsJavascriptEnabled && !PrinterFriendly) 
			{
				ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];				
				// Output reference to necessary JavaScript file from the ScriptRepository
				Page.ClientScript.RegisterClientScriptBlock(typeof(RouteSelectionControl), ROW_HIGHLIGHTER_SCRIPT, scriptRepository.GetScript(ROW_HIGHLIGHTER_SCRIPT, ((TDPage)Page).JavascriptDom));
			}

			// Show/hide navigation links
			if (blnNavigationEnabled && !PrinterFriendly)
			{
				earlierHyperlink.Text = GetResource("RouteSelectionControl.earlierHyperlink.Text");
				earlierHyperlink.Visible = true;

				laterHyperlink.Text = GetResource("RouteSelectionControl.laterHyperlink.Text");
				laterHyperlink.Visible = true;
			}
			else
			{
				earlierHyperlink.Visible = false;
				laterHyperlink.Visible = false;
			}

			RouteSelectionRepeater.DataSource = dataSource;
			RouteSelectionRepeater.DataBind();
		}

		/// <summary>
		/// Pre Render method re-binds repeater.
		/// </summary>
		private void OnPreRender(object sender, System.EventArgs e)
		{
			// DataBind repeater
			RouteSelectionRepeater.DataSource = dataSource;
			RouteSelectionRepeater.DataBind();
		}

		/// <summary>
		/// Property to set the datasource for the control
		/// </summary>
		public JourneySummaryLine[] DataSource
		{
			get {return dataSource;}
			set {dataSource = value;}
		}

		/// <summary>
		/// Returns the Javascript function to execute when the radio button is clicked
		/// </summary>
		/// <returns>Javascript to execute when the radio button is clicked</returns>
		protected string GetAction
		{
			get
			{
				return "return highlightSelectedItem('" + GetTableId + "', '" + ODD_ROW + "','" + EVEN_ROW + "','" + SELECT_ROW + "');";
			}
		}

		/// <summary>
		/// Used to set the Id attribute of the Html table in the control		
		/// </summary>
		/// <returns>Table Id string</returns>
		protected string GetTableId
		{
			get
			{
				return RouteSelectionRepeater.ClientID;	
			}
		}	

		/// <summary>
		/// get the index of the currently selected item
		/// </summary>
		protected void SelectionChanged(object sender, System.EventArgs e) 
		{
			foreach (RepeaterItem  item in RouteSelectionRepeater.Items)
			{
				ScriptableGroupRadioButton sgrButton = 
					(ScriptableGroupRadioButton)item.FindControl(RADIO_BUTTON);	

				if (sgrButton != null && sgrButton.Checked)
				{
					// Set the index of the journey in the list
					itemIndex = item.ItemIndex;
					// Set the journey index of the selected item
					itemJourneyIndex = dataSource[item.ItemIndex].JourneyIndex;
					hasSelectionChanged = true;
					break;
				}
			}
			CommandEventArgs selectionCommand = new CommandEventArgs("SelectionChanged", CommandSegment);
			SelectionChangedEvent(sender, selectionCommand);
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
			this.RouteSelectionRepeater.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.RouteSelectionRepeater_ItemDataBound);
			this.PreRender += new EventHandler(this.OnPreRender);
			this.earlierHyperlink.link_Clicked += new EventHandler(EarlierLinkClicked);
			this.laterHyperlink.link_Clicked += new EventHandler(LaterLinkClicked);
		}

		#endregion

		/// <summary>
		/// Handle the event raised when the earlier hyperlink is clicked
		/// </summary>
		public void EarlierLinkClicked(object sender, System.EventArgs e)
		{
			CommandEventArgs buttonArgs = new CommandEventArgs("EalierLink", CommandSegment);
			
			EarlierCommand(sender, buttonArgs);
		}


		/// <summary>
		/// Handle the event raised when the later hyperlink is clicked
		/// </summary>
		public void LaterLinkClicked(object sender, System.EventArgs e)
		{
			CommandEventArgs buttonArgs = new CommandEventArgs("LaterLink", CommandSegment);
			
			LaterCommand(sender, buttonArgs);
		}


		/// <summary>
		/// Sets the value of the group name and the index of the ScriptableRadioButton
		/// </summary>
		private void RouteSelectionRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			Label labelTag;

			switch(e.Item.ItemType)       
			{         
				case ListItemType.Item:
				case ListItemType.AlternatingItem:

					ScriptableGroupRadioButton sgrRoute = (ScriptableGroupRadioButton)e.Item.FindControl(RADIO_BUTTON);

					if (sgrRoute != null)
					{
						// Radio buttons aren't shown on printer page
						if (PrinterFriendly)
						{
							sgrRoute.Visible = false;
						}
						else
						{
							sgrRoute.CheckedChanged += new System.EventHandler(this.SelectionChanged);
							sgrRoute.GroupName = GROUP_NAME + "_" + CommandArgumentSegmentIndex.ToString(TDCultureInfo.CurrentUICulture);
						}
							
						//check the journey index to determine if item should be checked
						if (((JourneySummaryLine)dataSource[e.Item.ItemIndex]).JourneyIndex == itemJourneyIndex)							
						{
							sgrRoute.Checked = !PrinterFriendly;
							
							//set highlight on row if JavaScript enabled
							if (((TDPage)Page).IsJavascriptEnabled) 
							{
								HtmlTableRow row = (HtmlTableRow)e.Item.FindControl("row");
								row.Attributes["class"] = SELECT_ROW;
							}
							else
							{
								HtmlTableRow row = (HtmlTableRow)e.Item.FindControl("row");
								row.Attributes["class"] = ((e.Item.ItemIndex % 2) == 0) ? EVEN_ROW : ODD_ROW;
							}
						}
						else
						{
							HtmlTableRow row = (HtmlTableRow)e.Item.FindControl("row");
							row.Attributes["class"] = ((e.Item.ItemIndex % 2) == 0) ? EVEN_ROW : ODD_ROW;
						}
					}

					JourneySummaryLine journeyLine = (JourneySummaryLine)e.Item.DataItem;
						
					// Changed transport type to show as text instead of icons

                    double conversionFactor =
					Convert.ToDouble(Properties.Current["Web.Controls.MileageConverter"], TDCultureInfo.CurrentCulture.NumberFormat);

                    FormattedJourneySummaryLine formattedJourney = new FormattedJourneySummaryLine(journeyLine, 0, conversionFactor);

                    labelTag = (Label)e.Item.FindControl("labelTrasportMode");
                    if (labelTag != null)
                    {
                        labelTag.Text = formattedJourney.GetTransportModes();
                    }


					labelTag = (Label)e.Item.FindControl("labelDepart");
					if (labelTag != null) 
					{
						labelTag.Text = journeyLine.DepartureDateTime.ToString("HH:mm");// journeyLine.GetDepartureTime();
					}

					labelTag = (Label)e.Item.FindControl("labelArrive");
					if (labelTag != null) 
					{
						labelTag.Text = journeyLine.ArrivalDateTime.ToString("HH:mm"); //journeyLine.GetArrivalTime();
					}

					break;

				case ListItemType.Header:
					//get the text for the items in the header - this is dynamic according to segment index
					labelTag = (Label)e.Item.FindControl("labelSelectRoute");
					if (labelTag != null)
					{
						labelTag.Text = GetResource(string.Format(TDCultureInfo.CurrentUICulture, "RouteSelectionControl.labelSelectRoute{0}.Text", CommandSegment.ToString(TDCultureInfo.CurrentUICulture))); 
					}
                    
					labelTag = (Label)e.Item.FindControl("labelDepTime");
					if (labelTag != null)
					{
						labelTag.Text = GetResource("RouteSelectionControl.labelDepTime.Text");
					}

					labelTag = (Label)e.Item.FindControl("labelArrTime");
					if (labelTag != null)
					{
						labelTag.Text = GetResource("RouteSelectionControl.labelArrTime.Text");
					}
					break;

				default:            
					break;      
			} 	
		}
	}
}
