// *********************************************** 
// NAME                 : FindFareTravelDatesBaseControl.ascx.cs 
// AUTHOR               : Richard Hopkins
// DATE CREATED         : 16/02/2005
// DESCRIPTION			: A base custom control that holds code that is common to
//						the FindFare...TravelDatesControls that display a list of 
//						possible fares for a range of dates and travel modes
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindFareTravelDatesBaseControl.cs-arc  $
//
//   Rev 1.3   Dec 17 2008 13:09:18   devfactory
//XHTML Compliance changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:20:42   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:14:18   mturner
//Initial revision.
//
//   Rev 1.12   Jul 16 2007 18:24:00   asinclair
//Added code to set header label text
//Resolution for 4452: 9.6 - WAI Improvements to Find a Fare Tables
//
//   Rev 1.11   Jul 10 2007 13:24:08   asinclair
//Display the return day and date in the same call
//Resolution for 4452: 9.6 - WAI Improvements to Find a Fare Tables
//
//   Rev 1.10   Jun 26 2007 11:57:40   asinclair
//Altered ColumnHeader method, removed methods for image button clicks - replaced with tdButton click events
//Resolution for 4452: 9.6 - WAI Improvements to Find a Fare Tables
//
//   Rev 1.9   Feb 23 2006 19:16:40   build
//Automatically merged from branch for stream3129
//
//   Rev 1.8.1.0   Jan 10 2006 15:24:44   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.8   Apr 29 2005 21:05:46   rhopkins
//Changes to column styles, and some columns combined together
//Resolution for 2109: PT - Find a fare layout - Select date/transport option page
//Resolution for 2141: PT - Incorrect text on Date/Transport screen for Cheaper Fares
//Resolution for 2329: PT - Find Fare Date Selection column widths
//
//   Rev 1.7   Apr 20 2005 12:28:16   rhopkins
//Handle possibility that query may return no TravelDates.
//Resolution for 2100: PT - Coach - Find a Fare not correctly handling missing fare information
//
//   Rev 1.6   Apr 14 2005 16:00:38   tmollart
//Modified CreateTravelDateTable method so that scroll bar is not displayed when page is in Printer Friendly mode.
//Resolution for 2137: PT - Printer friendly page for date/transport options includes scrollbar
//
//   Rev 1.5   Apr 13 2005 13:59:56   rhopkins
//Maintain selected travel date and sort option when display options are changed
//Resolution for 2053: PT: Sort Order issue on Find Fare Date Selection
//Resolution for 2054: PT: Selected row number doesn't change on Find Fare Date Selection
//Resolution for 2055: PT: Actual sort order doesn't match what table is indicating
//
//   Rev 1.4   Apr 08 2005 20:01:22   rhopkins
//Display resolved FistDate and LastDate, instead of requested dates
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.3   Mar 30 2005 16:22:28   rhopkins
//Correct event handling for TravelDate table
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.2   Mar 29 2005 14:51:30   rhopkins
//Changed to use ResultsTableTitleControl
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.1   Mar 17 2005 14:42:14   rhopkins
//Change the way that the sort click events are handled
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.0   Feb 25 2005 12:37:46   rhopkins
//Initial revision.
//

using System;using TransportDirect.Common.ResourceManager;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Base header contains functionality to add and remove listeners for default event
	/// </summary>
	public class FindFareTravelDatesBaseControl : TDPrintableUserControl, ILanguageHandlerIndependent
	{
		protected System.Web.UI.WebControls.Repeater travelDateRepeater;
		protected ResultsTableTitleControl resultsTableTitleControl;

		#region Constants

		private const string ROW_HIGHLIGHTER_SCRIPT = "RowHighlighter";

		private const string ROWPREFIX_CSSCLASS = "fftdbdyrow";

		#endregion Constants

		protected string OutwardDayNameCssClass = String.Empty;
		protected string OutwardDayMonthCssClass = String.Empty;
		protected string ReturnDayNameCssClass = String.Empty;
		protected string ReturnDayMonthCssClass = String.Empty;
		protected string TravelModeCssClass = String.Empty;
		protected string FareRangeFareRangeCssClass = String.Empty;
		protected string LowestProbableFareCssClass = String.Empty;
		protected string RadioButtonCssClass = String.Empty;
		protected string RadioButtonCssClassScroll = String.Empty;
		protected string RadioButtonCssClassNoScroll = String.Empty;

		protected DisplayableTravelDates displayableTravelDates;

		protected System.Web.UI.WebControls.Label labelSROutwardDate;
		protected System.Web.UI.WebControls.Label labelSRReturnDate;
		protected System.Web.UI.WebControls.Label labelSRTransport;
		protected System.Web.UI.WebControls.Label labelSRFareRange;
		protected System.Web.UI.WebControls.Label labelSRAvailable;
		protected System.Web.UI.WebControls.Label labelSRSelect;



		#region Protected Members

		protected bool showReturnTickets = false;

		#endregion Protected Members


		#region Private Members

		private FindCostBasedPageState pageState;
		private CostSearchParams searchParams;
		private int scrollPoint = int.MaxValue;
		private string fixedHeight = string.Empty;
		private int selectedIndex = 0;
		private bool scrollRequired = false;

		private FindFareCostFacadeAdapter costFacade;

		private string javaScriptDom = String.Empty;
		private ScriptRepository.ScriptRepository scriptRepository;

		#endregion Private Members

		// Event to fire when the selection has changed
		public event EventHandler SelectionChanged;

		#region Public Properties

		public int SelectedIndex
		{
			get
			{
				selectedIndex = GetSelectedTravelDateIndex();
				return selectedIndex;
			}
			set
			{
				selectedIndex = value;
				SetSelectedTravelDateIndex();
			}
		}

		public bool ShowSortableLinks
		{
			get
			{
				return (!this.PrinterFriendly && (displayableTravelDates.Length > 1));
			}
		}

		/// <summary>
		/// Page State for current Find a Fare
		/// </summary>
		public FindCostBasedPageState PageState
		{
			get { return pageState; }
			set { pageState = value; }
		}

		/// <summary>
		/// Search Params for current Find a Fare
		/// </summary>
		public CostSearchParams SearchParams
		{
			get { return searchParams; }
			set { searchParams = value; }
		}

		/// <summary>
		/// Indicates whether adult or child fares are visible
		/// </summary>
		public bool ShowChildFares
		{
			get {return pageState.ShowChild;}
		}

		#endregion Public Properties

		#region Public Methods

		/// <summary>
		/// Initialise the control
		/// </summary>
		protected virtual void Initialise()
		{
			if (this.Visible)
			{
				LocalResourceManager = TDResourceManager.FIND_A_FARE_RM;
				javaScriptDom = (string) Session[((TDPage)Page).Javascript_Dom];
				scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

				// Get the facade for accessing the Cost Search data
				costFacade = new FindFareCostFacadeAdapter(pageState);

				// Get table height and scroll threshold from the property provider
				string scrollPointString = Properties.Current["FindFareDateSelection.Scrollpoint"];
				if (scrollPointString != null && scrollPointString.Length > 0)
				{
					scrollPoint = Convert.ToInt32(scrollPointString);
				}
				else
				{
					scrollPoint = int.MaxValue;
				}

				fixedHeight = Properties.Current["FindFareDateSelection.FixedHeight"];
				if (fixedHeight == null)
				{
					fixedHeight = string.Empty;
				}

				// Attach event that is used to create the rows for the table
				travelDateRepeater.ItemCreated += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.item_Created);

				PopulateTravelDatesTable();
			}
		}

		private void InitialiseTableTitle()
		{
			if (pageState.SelectedTicketType == TicketType.OpenReturn)
			{
				resultsTableTitleControl.ResultsType = DisplayResultsType.FaresOpenReturn;
			}
			else
			{
				resultsTableTitleControl.ResultsType = DisplayResultsType.Fares;
			}
			resultsTableTitleControl.ShowTicketType = true;

			resultsTableTitleControl.OutwardFirstDate = pageState.SearchRequest.SearchOutwardStartDate;
			resultsTableTitleControl.OutwardLastDate = pageState.SearchRequest.SearchOutwardEndDate;

			if (showReturnTickets)
			{
				resultsTableTitleControl.InwardFirstDate = pageState.SearchRequest.SearchReturnStartDate;
				resultsTableTitleControl.InwardLastDate = pageState.SearchRequest.SearchReturnEndDate;
			}
		}

		/// <summary>
		/// Handler for the prerender event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void Page_PreRender(object sender, EventArgs e)
		{
			InitialiseTableTitle();

			// If JavaScript currently supported then apply clientside script to scroll to selected item
			if ( scrollRequired && ((TDPage)Page).IsJavascriptEnabled )
			{
				foreach (RepeaterItem item in travelDateRepeater.Items)
				{
					if ((item.ItemIndex > 0) && IsSelectedIndex(item.ItemIndex))
					{
						TableRow tableRow = (TableRow)item.FindControl(GetItemRowId(item.ItemIndex));

						((TDPage)this.Page).ScrollManager.ScrollElementToView(tableRow.ClientID);
					}
				}
			}
		}

		/// <summary>
		/// Populates and renders the repeater to display the TravelDate table
		/// </summary>
		protected virtual void CreateTravelDateTable()
		{
			scrollRequired = !this.PrinterFriendly && (displayableTravelDates.Length > scrollPoint);

			if (scrollRequired)
			{
				RadioButtonCssClass = RadioButtonCssClassScroll;
			}
			else
			{
				RadioButtonCssClass = RadioButtonCssClassNoScroll;
			}

			travelDateRepeater.DataSource = displayableTravelDates;
			travelDateRepeater.DataBind();

			Page.ClientScript.RegisterStartupScript(typeof(FindFareTravelDatesBaseControl),this.ID, CreateRowHighlightStartupScript());

			SelectedIndex = pageState.SelectedTravelDateIndex;
		}

		/// <summary>
		/// Populates the travel dates table repeater
		/// </summary>
		public virtual void PopulateTravelDatesTable()
		{
			if (this.Visible)
			{
				displayableTravelDates = costFacade.GetTicketSummary();
				if ( (displayableTravelDates != null) && (displayableTravelDates.Length > 0) )
				{
					CreateSortedTravelDateTable();
				}
			}
		}

		/// <summary>
		/// Get header text for the specified column
		/// </summary>
		public string HeaderText(string headerID)
		{
			return GetResource("FindFareDateSelectTable.headerText" + headerID);
		}


		/// <summary>
		/// Get header text for the Fare Range column depending on whether showing Child or Adult and whether Discount Cards are selected
		/// </summary>
		public string HeaderFareRangeText()
		{
			if ( ((searchParams.CoachDiscountedCard != null) && (searchParams.CoachDiscountedCard != String.Empty))
				|| ((searchParams.RailDiscountedCard != null) && (searchParams.RailDiscountedCard != String.Empty)) )
			{
				if (pageState.ShowChild)
				{
					return GetResource("FindFareDateSelectTable.headerTextDiscountChildFareRange");
				}
				else
				{
					return GetResource("FindFareDateSelectTable.headerTextDiscountAdultFareRange");
				}
			}
			else
			{
				if (pageState.ShowChild)
				{
					return GetResource("FindFareDateSelectTable.headerTextChildFareRange");
				}
				else
				{
					return GetResource("FindFareDateSelectTable.headerTextAdultFareRange");
				}
			}
		}

		/// <summary>
		/// Populate the table header with the appropriate images/text
		/// </summary>
		/// <param name="headerID">ResourceID used for the header</param>
		/// <param name="headerLabel">Label control for the header</param>
		/// <param name="headerButton">Button control for the header</param>
		/// <param name="headerArrow">Image control for the header sort arrow</param>
		public void ColumnHeader(string headerID, System.Web.UI.WebControls.Label headerLabel,
			System.Web.UI.WebControls.Image headerArrow)
		{
			if (ShowSortableLinks)
			{
				string columnID;
				if (headerID == "SingleDate")
				{
					columnID = DisplayableTravelDateColumn.OutwardDate.ToString();
				}
				else
				{
					columnID = headerID;
				}

				headerLabel.Visible = false;
				headerArrow.Visible = (columnID == displayableTravelDates.SortColumn.ToString());

				headerArrow.ImageUrl = HeaderImageIcon(columnID);
				headerArrow.AlternateText = HeaderAltIcon(columnID);
			}
			else
			{
				headerLabel.Visible = true;
				headerArrow.Visible = false;

				headerLabel.Text = HeaderText(headerID);
			}
		}

		/// <summary>
		/// Populate the table header with the appropriate text
		/// </summary>
		/// <param name="headerID">ResourceID used for the header</param>
		/// <param name="headerLabel">Label control for the header</param>
		public void ColumnHeader(string headerID, System.Web.UI.WebControls.Label headerLabel)
		{
			headerLabel.Text = HeaderText(headerID);
		}

		/// <summary>
		/// Get header image for the specified column
		/// </summary>
		public string HeaderImage(string headerID)
		{
			return GetResource("FindFareDateSelectTable.headerImage" + headerID);
		}

		/// <summary>
		/// Get header image for the specified column depending on whether showing Child or Adult
		/// </summary>
		public string HeaderChildAdultImage(string headerID)
		{
			if (pageState.ShowChild)
			{
				return GetResource("FindFareDateSelectTable.headerImageChild" + headerID);
			}
			else
			{
				return GetResource("FindFareDateSelectTable.headerImageAdult" + headerID);
			}
		}

		/// <summary>
		/// Get header alt text for the specified column
		/// </summary>
		public string HeaderAlt(string headerID)
		{
			return GetResource("FindFareDateSelectTable.headerAlt" + headerID);
		}

		/// <summary>
		/// Get header alt text for the specified column depending on whether showing Child or Adult
		/// </summary>
		public string HeaderChildAdultAlt(string headerID)
		{
			if (pageState.ShowChild)
			{
				return GetResource("FindFareDateSelectTable.headerAltChild" + headerID);
			}
			else
			{
				return GetResource("FindFareDateSelectTable.headerAltAdult" + headerID);
			}
		}

		/// <summary>
		/// Get the header sort icon for the specified column
		/// </summary>
		public string HeaderImageIcon(string headerID)
		{
			if (headerID == displayableTravelDates.SortColumn.ToString())
			{
				if (displayableTravelDates.SortOrderAscending)
				{
					return GetResource("FindFareDateSelectTable.headerImageSortIconAscending");
				}
				else
				{
					return GetResource("FindFareDateSelectTable.headerImageSortIconDescending");
				}
			}
			else
			{
				return "";
			}
		}

		/// <summary>
		/// Get the header sort icon for the specified column
		/// </summary>
		public string HeaderAltIcon(string headerID)
		{
			if (headerID == displayableTravelDates.SortColumn.ToString())
			{
				if (displayableTravelDates.SortOrderAscending)
				{
					return GetResource("FindFareDateSelectTable.headerAltSortIconAscending");
				}
				else
				{
					return GetResource("FindFareDateSelectTable.headerAltSortIconDescending");
				}
			}
			else
			{
				return "";
			}
		}


		/// <summary>
		/// Get the header sort icon for the specified column
		/// </summary>
		public bool HeaderIconVisible(string headerID)
		{
			return (ShowSortableLinks && (headerID == displayableTravelDates.SortColumn.ToString()));
		}

		/// <summary>
		/// Used to set the Id attribute of the Html row in the
		/// item template of the repeater control.
		/// </summary>
		/// <param name="index">item index of the repeater being rendered</param>
		/// <returns>row ID string </returns>
		public string GetItemRowId(int index)
		{
			return this.ID + "_fftdtbl" + "_itemRow_" + index.ToString();
		}

		/// <summary>
		/// Used to set the Id attribute of the Html table in the control
		/// </summary>
		/// <returns>Table Id string</returns>
		public string GetTableId()
		{
			return this.ID + "_fftdtbl";
		}

		public string GetTableSummary()
		{
			return GetResource("FindFareDateSelection.TravelDatesTableSummary");
		}

		/// <summary>
		/// Returns none, one or both of an overflow attribute and a height attribute
		/// </summary>
		/// <returns></returns>
		public string GetScrollStyle()
		{
			if (scrollRequired)
			{
				return "OVERFLOW: auto; HEIGHT: " + fixedHeight;
			}
			else
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Returns the correct css class for the screenreader header for single fares
		/// </summary>
		/// <returns></returns>
		public string HeadingSingleScrollClass()
		{
			if (scrollRequired)
			{
				return "fftdbdysingleselectscroll";
			}
			else
			{
				return "fftdbdysingleselectnoscroll";
			}
		}

		/// <summary>
		/// Returns the correct css class for the screenreader header for return fares
		/// </summary>
		/// <returns></returns>
		public string HeadingReturnScrollClass()
		{
			if (scrollRequired)
			{
				return "fftdbdyreturnselectscroll";
			}
			else
			{
				return "fftdbdyreturnselectnoscroll";
			}
		}

		/// <summary>
		/// Returns the Css class that the text should be rendered with.
		/// </summary>
		/// <param name="index">item being rendered.</param>
		/// <returns>Css class string.</returns>
		public string GetCssClassSuffix(int index)
		{
			// If there is only one result then no rows should
			// be highlighted. Check to see if this is the case.
			if(displayableTravelDates.Length == 1)
			{
				return "";
			}

			return (index % 2) == 0 ? "g":"";
		}

		/// <summary>
		/// Used to set the radio group name of the radio buttons in the 
		/// item and footer template of the repeater control.
		/// </summary>
		/// <returns>Radio (group) name string</returns>
		public string GroupName()
		{
			return this.ID + "_fftdtbl";
		}

		#endregion Public Methods


		#region Private Methods

		/// <summary>
		/// Item Created event handler for the repeaters
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		private void item_Created(object sender, RepeaterItemEventArgs  e) 
		{

			//Create the row containing the Header information for the table
			if(e.Item.ItemType == ListItemType.Header)
			{
				Label labelSROutwardDate = e.Item.FindControl("labelSROutwardDate") as Label;
				labelSROutwardDate.Text = GetResource("FindFareTravelDatesControl.labelSRDate");
				Label labelSRReturnDate = e.Item.FindControl("labelSRReturnDate") as Label;
				labelSRReturnDate.Text = GetResource("FindFareTravelDatesControl.labelSRReturnDate");
				Label labelSRTransport = e.Item.FindControl("labelSRTransport") as Label;
				labelSRTransport.Text = GetResource("FindFareTravelDatesControl.labelSRTransport");
				Label labelSRFareRange = e.Item.FindControl("labelSRFareRange") as Label;
				labelSRFareRange.Text = HeaderFareRangeText();
				Label labelSRAvailable = e.Item.FindControl("labelSRAvailable") as Label;
				labelSRAvailable.Text = GetResource("FindFareTravelDatesControl.labelSRAvailable");
				Label labelSRSelect = e.Item.FindControl("labelSRSelect") as Label;
				labelSRSelect.Text = GetResource("FindFareTravelDatesControl.labelSRSelect.Text");
				




			}

			//ensure that a row is added only for item types and not for the header or footer
			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType==ListItemType.AlternatingItem))
			{
				TableRow row = CreateItemRow( displayableTravelDates[e.Item.ItemIndex], e.Item.ItemIndex );
				e.Item.Controls.Add( row );

				if(e.Item.ItemType != ListItemType.Header  && e.Item.ItemType != ListItemType.Footer)
				{
					for	(int i = 0; i < row.Cells.Count; i++)
					{
						row.Cells[i].Attributes["headers"] = "header"+ i;
					
					}
				} 	
			}
		}


		/// <summary>
		/// Creates a table row for the provided Travel Date
		/// </summary>
		/// <param name="repeatedTravelDate">The Travel Date to display</param>
		/// <param name="index">The index of the Travel Date</param>
		/// <returns>A table row populated with the details of the provided Travel Date</returns>
		private TableRow CreateItemRow( DisplayableTravelDate repeatedTravelDate, int index )
		{
			TableCell cell;
			TableRow row = new TableRow();
			row.CssClass = ROWPREFIX_CSSCLASS + GetCssClassSuffix(index);
			row.ID= GetItemRowId(index);

			cell= new TableCell();
			cell.CssClass = OutwardDayNameCssClass;
			cell.Text = repeatedTravelDate.OutwardDayName + " " + repeatedTravelDate.OutwardDayMonth;
			row.Cells.Add( cell );

			if (showReturnTickets)
			{
				cell= new TableCell();
				cell.CssClass = ReturnDayNameCssClass;
				cell.Text = repeatedTravelDate.ReturnDayName + " " + repeatedTravelDate.ReturnDayMonth;
				row.Cells.Add( cell );

			}

			cell= new TableCell();
			cell.CssClass = TravelModeCssClass;
			cell.Text = GetResource("FindFare.TransportMode." + repeatedTravelDate.TravelMode);
			row.Cells.Add( cell );

			cell= new TableCell();
			cell.CssClass = FareRangeFareRangeCssClass;
			cell.Text = repeatedTravelDate.FareRange;
			row.Cells.Add( cell );

			cell= new TableCell();
			cell.CssClass = LowestProbableFareCssClass;
			cell.Text = repeatedTravelDate.LowestProbableFare;
			row.Cells.Add( cell );

			if (!this.PrinterFriendly)
			{
				cell = CreateTravelDateSelectorCell ( row.ID,index);
				row.Cells.Add( cell );
			}

			return row;
		}

		/// <summary>
		/// Creates a table cell for the RadioButton column
		/// </summary>
		/// <param name="rowID">The id of the row of the radiobutton</param>
		/// <param name="index">The index of the TravelDate</param>
		/// <returns>A table cell populated with the selector radiobutton for the row's Travel Date</returns>
		private TableCell CreateTravelDateSelectorCell (string rowID, int index)
		{
			TableCell cell = new TableCell();
			cell.CssClass = RadioButtonCssClass;

			//cell for radio select button
			ScriptableGroupRadioButton sgr = new ScriptableGroupRadioButton();
			sgr.CheckedChanged +=new EventHandler(sgr_CheckedChanged);
			sgr.ID="travelDateRadioButton";
			sgr.GroupName = GroupName();
			sgr.Checked = IsSelectedIndex(index);
			sgr.EnableClientScript = ((TDPage)Page).IsJavascriptEnabled;

			// If JavaScript currently supported
			if (sgr.EnableClientScript)
			{
				//function highlightSelectedItem( tableId, rowClass, altRowClass, selectedRowClass )
				sgr.Action= "return highlightSelectedItem('" + GetTableId() + "', '" + ROWPREFIX_CSSCLASS + "g', '" + ROWPREFIX_CSSCLASS + "', '" + ROWPREFIX_CSSCLASS + "y');";
				sgr.ScriptName = ROW_HIGHLIGHTER_SCRIPT;

				// Output reference to necessary JavaScript file from the ScriptRepository
				Page.ClientScript.RegisterClientScriptBlock(typeof(FindFareTravelDatesBaseControl), this.ID, scriptRepository.GetScript(ROW_HIGHLIGHTER_SCRIPT, javaScriptDom));
			}

			cell.Controls.Add(sgr);

			return cell;

		}

		/// <summary>
		/// Determines if an index is the currently selected index specified by FindCostBasedPageState
		/// </summary>
		/// <param name="index">The candidate index</param>
		/// <returns>true if the index is the selected index, false otherwise</returns>
		private bool IsSelectedIndex(int index)
		{
			return index == pageState.SelectedTravelDateIndex;
		}

		/// <summary>
		/// Returns the currently selected TravelDate index
		/// </summary>
		private int GetSelectedTravelDateIndex()
		{
			foreach (RepeaterItem item in travelDateRepeater.Items)
			{
				ScriptableGroupRadioButton travelDateRadioButton = item.FindControl("travelDateRadioButton") as ScriptableGroupRadioButton;

				if (travelDateRadioButton!=null && travelDateRadioButton.Checked)
				{
					//update the selected TravelDate index
					return item.ItemIndex;
				}
			}

			// no TravelDate has been selected
			return -1;
		}

		private void SetSelectedTravelDateIndex()
		{
			foreach (RepeaterItem item in travelDateRepeater.Items)
			{
				ScriptableGroupRadioButton travelDateRadioButton = item.FindControl("travelDateRadioButton") as ScriptableGroupRadioButton;

				if (travelDateRadioButton != null)
				{
					travelDateRadioButton.Checked = (item.ItemIndex == selectedIndex);
				}

			}
		}

		/// <summary>
		/// Create a startup script to call the function to highlight the selected row when the page loads
		/// </summary>
		/// <returns></returns>
		private string CreateRowHighlightStartupScript()
		{
			// Ensure that the client-side function has been registered
			Page.ClientScript.RegisterClientScriptBlock(typeof(FindFareTravelDatesBaseControl), this.ID, scriptRepository.GetScript(ROW_HIGHLIGHTER_SCRIPT, javaScriptDom));

			return
                "<script language=\"javascript\" type=\"text/javascript\">" +
				"highlightSelectedItem('" + GetTableId() + "', '" + ROWPREFIX_CSSCLASS + "g', '" + ROWPREFIX_CSSCLASS + "', '" + ROWPREFIX_CSSCLASS + "y');" +
				"</script>";
		}

		#endregion Private Methods

		#region EventHandlers

		/// <summary>
		/// CheckedChanged event code for the scriptablegroupradiobuttons
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void sgr_CheckedChanged(object sender, EventArgs e)
		{
			//find the selected row index
			selectedIndex = GetSelectedTravelDateIndex();
			pageState.SelectedTravelDateIndex = selectedIndex;

			if (SelectionChanged != null)
				SelectionChanged(this, EventArgs.Empty);
		}

		/// <summary>
		/// Click event handler for the single Date column header
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void HeaderImageSingleDate_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			UpdateSortOrder(DisplayableTravelDateColumn.OutwardDate);
		}

		/// <summary>
		/// Click event handler for the single Date column header
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void HeaderButtonSingleDate_Click(object sender,  EventArgs e)
		{
			UpdateSortOrder(DisplayableTravelDateColumn.OutwardDate);
		}


		/// <summary>
		/// Click event handler for the Outward Date column header
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void HeaderButtonOutwardDate_Click(object sender, EventArgs e)
		{
			UpdateSortOrder(DisplayableTravelDateColumn.OutwardDate);
		}
		
		/// <summary>
		/// Click event handler for the Return Date column header
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void HeaderButtonReturnDate_Click(object sender, EventArgs e)
		{
			UpdateSortOrder(DisplayableTravelDateColumn.ReturnDate);
		}
		
		/// <summary>
		/// Click event handler for the Return Date column header
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void HeaderImageReturnDate_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			UpdateSortOrder(DisplayableTravelDateColumn.ReturnDate);
		}


		/// <summary>
		/// Click event handler for the Transport Mode column header
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void HeaderButtonTransportMode_Click(object sender, EventArgs e)
		{
			UpdateSortOrder(DisplayableTravelDateColumn.TransportMode);
		}

		/// <summary>
		/// Click event handler for the Lowest Fare column header
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void HeaderImageLowestFare_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			UpdateSortOrder(DisplayableTravelDateColumn.LowestFare);
		}

		/// <summary>
		/// Click event handler for the Lowest Fare column header
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void HeaderLinkLowestFare_Click(object sender, EventArgs e)
		{
			UpdateSortOrder(DisplayableTravelDateColumn.LowestFare);
		}

		/// <summary>
		/// Sets the sort order to the specified column, or changes it if it's already
		/// selected
		/// </summary>
		/// <param name="column"></param>
		private void UpdateSortOrder(DisplayableTravelDateColumn column)
		{
			if (displayableTravelDates.SortColumn == column)
			{
				displayableTravelDates.SortOrderAscending = !displayableTravelDates.SortOrderAscending;
			}
			else
			{
				displayableTravelDates.SortColumn = column;
				displayableTravelDates.SortOrderAscending = true;
			}

			CreateSortedTravelDateTable();
		}

		/// <summary>
		/// Sorts the TravelDates and displays them in the repeater
		/// </summary>
		private void CreateSortedTravelDateTable()
		{
			TravelDate selectedDate = pageState.SelectedTravelDate.TravelDate;

			DisplayableTravelDateSortOption sortOption = new DisplayableTravelDateSortOption(displayableTravelDates.SortColumn, displayableTravelDates.SortOrderAscending);

			DisplayableTravelDateComparer dateComparer = new DisplayableTravelDateComparer(sortOption);

			displayableTravelDates.Sort(dateComparer);

			pageState.SelectedTravelDateIndex = displayableTravelDates.GetTravelDateIndex(selectedDate, pageState.SearchResult);

			CreateTravelDateTable();
		}

		#endregion EventHandlers
	}
}
