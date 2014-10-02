// *********************************************** 
// NAME                 : FindCarParksResultsTableControl.ascx
// AUTHOR               : Esther Severn
// DATE CREATED         : 09/08/2006 
// DESCRIPTION  : Control displaying stations sorted by airport name or distance
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindCarParksResultsTableControl.ascx.cs-arc  $ 
//
//   Rev 1.10   Jun 09 2010 09:04:44   apatel
//Updated to remove javascript eschape characters "'" and "/"
//Resolution for 5550: FindNearest and FindCarPark break with "'" in the content
//
//   Rev 1.9   Mar 11 2010 12:35:50   mmodi
//Pass in a content string to show in a pop for may location symbols
//Resolution for 5440: Maps - Issue 16 ESRI fixes
//
//   Rev 1.8   Dec 07 2009 11:22:06   mmodi
//Corrected scroll to map issue in IE
//
//   Rev 1.7   Nov 24 2009 14:01:14   mmodi
//Corrected zooming in to stop
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.6   Nov 20 2009 09:54:46   apatel
//added property for default level
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.5   Nov 20 2009 09:26:24   apatel
//updated for map enhancement when javascript is disabled
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Jan 15 2009 09:59:22   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Apr 14 2008 10:00:08   mmodi
//Updated results table columns and styles
//Resolution for 4857: Del 10 - Car park results screen column alignment
//
//   Rev 1.2   Mar 31 2008 13:20:28   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Feb 20 2008 17:00:00 mmodi
//Updated sort by alt text values
//
//  Rev DevFactory Feb 15 2008 18:00:00 mmodi
//Added Refresh to page load to correct display issue when a map click event is triggered
//on the car park results page
//
//  Rev DevFactory Feb 06 2008 22:08:00 apatel
//  Changes to front end to add runat attributes to the image controls.
//  
//
//
//DEVFACTORY   JAN 08 2008 12:30:00   psheldrake
//Changes to conform to CCN426 : Car Park Usability Updates
//
//   Rev 1.0   Nov 08 2007 13:14:00   mturner
//Initial revision.
//
//   Rev 1.17   Sep 21 2007 14:09:04   asinclair
//Updated code for PrinterFriendly display
//
//   Rev 1.16   Jun 01 2007 11:51:26   asinclair
//Fixed issues with layout and printerfriendly page formatting
//Resolution for 4436: Car parks: Find nearest car park results alignment issues
//
//   Rev 1.15   Oct 27 2006 11:19:26   mmodi
//Updated radio button alt text for car park results table
//Resolution for 4233: Car Parking: Radio buttons require Alt text for Screenreader use
//
//   Rev 1.14   Oct 16 2006 16:13:32   mmodi
//Updated to check for Null results table before setting resutls row styles
//Resolution for 4226: Car Parking: Server error when no car parks found
//
//   Rev 1.13   Oct 12 2006 11:38:38   mmodi
//Updated layout and code to ensure scroll bar is displayed when more than 10 car parks are found
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4216: Car Parking: Scroll bar is not shown when more than 10 car parks configured to be displayed
//
//   Rev 1.12   Sep 28 2006 16:38:38   mmodi
//Added code to retain selected car park radio button when return from CarParkInformation
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4206: Car Parking: Selected car park radio button defaults to first option
//
//   Rev 1.11   Sep 20 2006 14:35:12   esevern
//removed calls to CarParkCatalogue.LoadData
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.10   Sep 20 2006 10:38:40   esevern
//Removed setting of radio button image from html page to code behind - when clicking a radio button, incorrect image was being set, allowing for more than one radio button to appear selected
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4165: Car Parking: Able to select more than one car park
//
//   Rev 1.9   Sep 18 2006 17:22:58   tmollart
//Modified method calls to retrieve a car park from the catalogue.
//Resolution for 4190: Thread Safety Issue on Car Park Catalogue
//
//   Rev 1.8   Sep 12 2006 15:35:18   mmodi
//Added method to set printable status of hyperlinks
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4166: Car Parking: Printable page of Find nearest car parks shows links to info page
//
//   Rev 1.7   Sep 08 2006 14:46:36   esevern
//Amended call to CarParkCatalogue.LoadData - now only loads data on specific car park selected.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.6   Sep 05 2006 11:05:40   esevern
//changes to update data to bind depending on sort order selected 
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.5   Sep 04 2006 11:47:20   mmodi
//Added hyperlink to information for car park
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.4   Sep 01 2006 13:10:50   esevern
//added storing of car park selection details
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.3   Aug 31 2006 14:37:46   MModi
//Added check to make table visible
//
//   Rev 1.2   Aug 30 2006 17:11:00   mmodi
//Updated Car Park Title to point to correct resource string
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.1   Aug 21 2006 13:07:06   esevern
//added commenting for properties
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.0   Aug 11 2006 13:56:14   esevern
//Initial revision.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//

using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.ScriptRepository;
using TransportDirect.Common.ServiceDiscovery;
using System.Text;

namespace TransportDirect.UserPortal.Web.Controls
{


	/// <summary>
	///		Summary description for FindCarParksResultsTableControl.
	/// </summary>
	public partial class FindCarParksResultsTableControl : TDUserControl
	{

		#region Declaration

		private FindCarParkPageState carParkPageState;
		private InputPageState inputPageState;
		private string sortAscUrl = "/web2/images/gifs/SoftContent/TravelNewsSortAscending.gif";
		private string sortDescUrl = "/web2/images/gifs/SoftContent/TravelNewsSortDescending.gif";
		private string sortAscAlternateText;
		private string sortDescAlternateText;
		private bool boolPrintablePage = false;

		public event SelectionChangedEventHandler SelectionChanged;// Event to fire when the selection has changed
		private string uncheckedRadioButtonImageUrl = String.Empty;
		private string checkedRadioButtonImageUrl = String.Empty;

		private string altTextRadioButtonChecked = String.Empty;
		private string altTextRadioButtonUnChecked = String.Empty;

		// used to set the visible area for the number of car parks
		private int maxNumberOfCarParkRows = 10;

        private bool showingIsSecure = false;
        private bool showingDisabledSpaces = false;
        private bool showingNumberOfSpaces = false;

        //used when control displays with the map
        private bool rowSelectLinkVisible = false;
        private string mapClientID = string.Empty;
        private string mapScrollToID = string.Empty;

        private MapHelper mapHelper = new MapHelper();

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor for FindCarParkResultsTableControl. 
		/// </summary>
		public FindCarParksResultsTableControl()
		{
			carParkPageState = TDSessionManager.Current.FindCarParkPageState;
			inputPageState = TDSessionManager.Current.InputPageState;
		}
		#endregion

		#region Page_Load
		/// <summary>
		/// Sets alternate text
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">System.EventArgs</param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            SetupControls();

            if (!Page.IsPostBack)
            {
                // Only display the table if car parks found
                if (carParkPageState.CarParkFound == true)
                {
                    repeaterResultTable.Visible = true;

                    Refresh();
                    StoreDefaultSelection(); // if only one car park found or user keeps default selection

                    // Set clickable status of all the hyperlinks in the results table.
                    // Prevents the hyperlink from being clickable on Printer Friendly page
                    PrintableHyperlink();
                }
                else
                {
                    repeaterResultTable.Visible = false;
                }
            }
        }

		#endregion

		#region Private methods
		/// <summary>
		/// Refreshes the table appreance, sorting the results displayed based on 
		/// current sort type (name, distance or option number)
		/// </summary>
		private void Refresh()
		{
			switch (carParkPageState.CurrentSortingType)
			{
                case FindCarParkPageState.SortingType.IsSecure:
                {
                    repeaterResultTable.DataSource = FindCarParkHelper.SortResultsDataTableByIsSecure(
                        carParkPageState.ResultsTable, carParkPageState.IsIsSecureAsc);
                    break;
                }
                case FindCarParkPageState.SortingType.TotalSpaces:
                {
                    repeaterResultTable.DataSource = FindCarParkHelper.SortResultsDataTableByTotalSpaces(
                        carParkPageState.ResultsTable, carParkPageState.IsTotalSpacesAsc);
                    break;
                }
                case FindCarParkPageState.SortingType.HasDisabledSpaces:
                {
                    repeaterResultTable.DataSource = FindCarParkHelper.SortResultsDataTableByHasDisabledSpaces(
                        carParkPageState.ResultsTable, carParkPageState.IsHasDisabledSpacesAsc);
                    break;
                }
				case FindCarParkPageState.SortingType.CarParkName:
				{
					repeaterResultTable.DataSource = FindCarParkHelper.SortResultsDataTableByName(
						carParkPageState.ResultsTable, carParkPageState.IsCarParkSortingAsc);
					break;
				}
				case FindCarParkPageState.SortingType.Distance:
				{	
					repeaterResultTable.DataSource =  FindCarParkHelper.SortResultsDataTableByDistance(
						carParkPageState.ResultsTable, carParkPageState.IsDistanceSortingAsc);
					break;
				}
				case FindCarParkPageState.SortingType.Option:
				{	
					repeaterResultTable.DataSource =  FindCarParkHelper.SortResultsDataTableByOption(
						carParkPageState.ResultsTable, carParkPageState.IsOptionSortingAsc);
					break;
				}
			}

			repeaterResultTable.DataBind();
		}

		/// <summary>
		/// Sets image urls and alternate text for control
		/// </summary>
		private void SetupControls() 
		{
			sortAscAlternateText = GetResource("FindCarParkResultsTable.sortAscAlternateText");
			sortDescAlternateText = GetResource("FindCarParkResultsTable.sortDescAlternateText");

            showingIsSecure = Convert.ToBoolean(Properties.Current["FindCarParkResults.ShowIsSecure"]); ;
            showingNumberOfSpaces = Convert.ToBoolean(Properties.Current["FindCarParkResults.ShowNumberOfSpaces"]); ;
            showingDisabledSpaces = Convert.ToBoolean(Properties.Current["FindCarParkResults.ShowDisabledSpaces"]); ;

            // set radio button image
			SetRadioButtonImage();

		}

		/// <summary>
		/// Sets up the radio button images and alt text, checked and unchecked
		/// </summary>
		private void SetRadioButtonImage()
		{
			uncheckedRadioButtonImageUrl = Global.tdResourceManager.GetString(
				"SummaryResultTableControl.imageButton.SummaryRadioButtonUnchecked", TDCultureInfo.CurrentUICulture);

			checkedRadioButtonImageUrl = Global.tdResourceManager.GetString(
				"SummaryResultTableControl.imageButton.SummaryRadioButtonChecked", TDCultureInfo.CurrentUICulture);

			altTextRadioButtonChecked = Global.tdResourceManager.GetString("FindCarParkResultsTable.imageButton.RadioButtonChecked.AltText", TDCultureInfo.CurrentUICulture);
			altTextRadioButtonUnChecked = Global.tdResourceManager.GetString("FindCarParkResultsTable.imageButton.RadioButtonUnChecked.AltText", TDCultureInfo.CurrentUICulture);

			for (int i=0; i < repeaterResultTable.Items.Count; i++)
			{
				if((ImageButton)repeaterResultTable.Items[i].FindControl("radioButtonImage") != null)
				{
					((ImageButton)repeaterResultTable.Items[i].FindControl("radioButtonImage")).ImageUrl 
						= GetButtonImageUrl(i);

					// Set the alt text
					if (IsSelectedIndex(i))
						((ImageButton)repeaterResultTable.Items[i].FindControl("radioButtonImage")).AlternateText
							= altTextRadioButtonChecked;
					else
						((ImageButton)repeaterResultTable.Items[i].FindControl("radioButtonImage")).AlternateText
							= altTextRadioButtonUnChecked;

				}
			}
		}

		/// <summary>
		/// Handler for the Radio Button Click.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void RadioButtonClick(object sender, System.Web.UI.ImageClickEventArgs e)
		{	
	
			ImageButton sendingButton = (ImageButton)sender;

			// Find the index number of the button that was pressed
			int index;
			for (index=0;index<repeaterResultTable.Items.Count;++index) 
			{
				ImageButton ib = ((ImageButton)repeaterResultTable.Items[index].FindControl("radioButtonImage"));
				if (ib == sendingButton)
				{
					string carParkRef = ib.CommandName; 
					
					// store the car park ref
					StoreSelection(carParkRef);

					// set the selected index
					TDSessionManager.Current.JourneyViewState.SelectedCarParkIndex = index;

					break;
				}
			}

			UpdateData();

			OnSelectionChanged(new EventArgs());
		}

		/// <summary>
		/// Method to fire off the Selection Changed Event.
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSelectionChanged(EventArgs e)
		{
			if(SelectionChanged != null)
			{
				// Invoke the delegate
				SelectionChanged(this, e);
			}
		}

		/// <summary>
		/// Sets the printable state of all car park hyperlinks in table
		/// </summary>
		private void PrintableHyperlink()
		{
			// for each row in repeater, find the car park hyperlinke. 
			// Then set its printable state to the page printable state
			for (int i=0; i < repeaterResultTable.Items.Count; i++)
			{
				if	((HyperlinkPostbackControl)repeaterResultTable.Items[i].FindControl("carParkInfoLinkControl") != null)
				{
					((HyperlinkPostbackControl)repeaterResultTable.Items[i].FindControl("carParkInfoLinkControl")).PrinterFriendly = this.boolPrintablePage;
				}
			}
		}

		/// <summary>
		/// Finds the current selected index from TDSessionManager.
		/// </summary>
		/// <returns>The current selected index.</returns>
		private int GetSelectedIndex()
		{
			return TDSessionManager.Current.JourneyViewState.SelectedCarParkIndex;
		}

		/// <summary>
		/// Determines if an index is the currently selected index specified by TDSessionManager
		/// </summary>
		/// <param name="index">The candidate index</param>
		/// <returns>true if the index is the selected index, false otherwise</returns>
		private bool IsSelectedIndex(int index)
		{
			return index == GetSelectedIndex();
		}

		/// <summary>
		/// Obtains the car park reference number for the default
		/// selected car park in the table.
		/// </summary>
		private void StoreDefaultSelection()
		{
			// ensures previously selected row index is retained when coming back to the results page
			if (TDSessionManager.Current.JourneyViewState.SelectedCarParkIndex != 0)
			{
				SetSelectedCarParkRow();
			}
			else
			{
				// store the car park ref of the first row in the table 
				RepeaterItem item = repeaterResultTable.Items[0];
				DataRow sourceRow = ((DataRow[])repeaterResultTable.DataSource)[item.ItemIndex];
				int stateRowIndex = (int)sourceRow[FindCarParkHelper.columnIndex];

				// store the selected car park row index
				TDSessionManager.Current.JourneyViewState.SelectedCarParkIndex = stateRowIndex;

				// then select pageState row to update
				DataRow stateRow = carParkPageState.ResultsTable.Rows[stateRowIndex];
				string carParkRef = GetCarParkRef(stateRow);
				TDSessionManager.Current.InputPageState.CarParkReference = carParkRef;
			}
		}

		/// <summary>
		/// Sets the selected row radio button to that in the SessionManager
		/// </summary>
		private void SetSelectedCarParkRow()
		{
			int index = GetSelectedIndex();

			ImageButton ib = ((ImageButton)repeaterResultTable.Items[index].FindControl("radioButtonImage"));
			string carParkRef = ib.CommandName;
			StoreSelection(carParkRef);
		}

		/// <summary>
		/// Obtains the car park reference number and OSGrid reference for the 
		/// currently selected car park in the table.
		/// </summary>
		/// <param name="carParkRef">car park reference of selected row</param>
		private void StoreSelection(string carParkRef)
		{
			TDSessionManager.Current.InputPageState.CarParkReference = carParkRef;
		}
		#endregion

		#region Public Methods for Repeater
		
		/// <summary>
		/// Returns the index number of the current table row as a string
		/// </summary>
		/// <param name="row">DataRow</param>
		/// <returns>string</returns>
		public string GetRowIndex(DataRow row)
		{
			int index = (int)row[FindCarParkHelper.columnIndex] +1;
			return index.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
		}

        /// <summary>
        /// Generates and return a script to associate with a link to show car park information on map
        /// </summary>
        /// <param name="row">data row object</param>
        /// <returns>javascript string</returns>
        public string GetShowOnMapScript(DataRow row)
        {
            string linkScript = string.Empty;

            if (IsRowSelectLinkVisible && !string.IsNullOrEmpty(mapClientID) && !string.IsNullOrEmpty(mapScrollToID))
            {
                StringBuilder showOnMapScript = new StringBuilder(string.Format("scrollToElement('{0}');", mapScrollToID));

                string mapLevel = Properties.Current["MapControl.FindNearest.DefaultMapLevel"];

                if (string.IsNullOrEmpty(mapLevel))
                {
                    mapLevel = "9";
                }

                // Build up the content to be shown in the popup
                string carparkName = row[FindCarParkHelper.columnCarParkName].ToString();
                string carparkInfoLink = mapHelper.GetCarParkInformationLink(row[FindCarParkHelper.columnCarParkRef].ToString());
                // Removed "'" and "\" from car park name as they causing problems when rendered in the esri map api
                string content = "<b>" + carparkName.Replace("\\", "").Replace("\'", "\\\'") + "</b><br />" + carparkInfoLink;

                showOnMapScript.Append("try { ");
                showOnMapScript.AppendFormat("ESRIUKTDPAPI.zoomToLevelAndPoint('{0}',{1},{2},{3},{4},'{5}','{6}');", 
                    mapClientID,
                    (int)row[FindCarParkHelper.columnIndex] + 1,
                    ((OSGridReference)row[FindCarParkHelper.columnGridRef]).Easting.ToString(),
                    ((OSGridReference)row[FindCarParkHelper.columnGridRef]).Northing.ToString(), 
                    mapLevel, 
                    " ",
                    Server.HtmlEncode(content));
                showOnMapScript.Append(" }catch(err){}return false;");

                linkScript = showOnMapScript.ToString();
            }

            return linkScript;
        }

		/// <summary>
		/// Returns the car park name of the current table row
		/// </summary>
		/// <param name="row">DataRow</param>
		/// <returns>string</returns>
		public string GetCarParkName(DataRow row)
		{
			return Server.HtmlEncode((string)row[FindCarParkHelper.columnCarParkName]);
		}

		/// <summary>
		/// Returns the distance for the current data row as a string
		/// </summary>
		/// <param name="row">DataRow</param>
		/// <returns>string</returns>
		public string GetDistance(DataRow row)
		{
			string miles = Global.tdResourceManager.GetString("FindStation.miles", TDCultureInfo.CurrentUICulture);

			return String.Format("{0:F1} {1}", (double)((int)row[FindCarParkHelper.columnDistance])/1609, miles);
		}


        /// <summary>
        /// Returns the width to use for the car park name column
        /// </summary>
        /// <returns></returns>
        public string GetColumnCount
        {
            get
            {
                int columnCount = 4;

                if (showingNumberOfSpaces)
                    columnCount += 1;

                if (showingIsSecure)
                    columnCount += 1;

                if (showingDisabledSpaces)
                    columnCount += 1;

                return columnCount.ToString();
            }
        }

        /// <summary>
        /// Returns the width to use for the car park name column
        /// </summary>
        /// <returns></returns>
        public string GetNameColumnWidth
        {
            get
            {
                int carParkNameColumnWidth = 170;

                if (!showingNumberOfSpaces)
                    carParkNameColumnWidth += 110;

                if (!showingIsSecure)
                    carParkNameColumnWidth += 70;

                if (!showingDisabledSpaces)
                    carParkNameColumnWidth += 70;

                return carParkNameColumnWidth.ToString();
            }
        }

        /// <summary>
        /// Returns the width to use for the car park name header
        /// </summary>
        /// <returns></returns>
        public string GetNameHeaderWidth
        {
            get
            {
                int carParkNameHeaderWidth = 170;

                if (!showingNumberOfSpaces)
                    carParkNameHeaderWidth += 110;

                if (!showingIsSecure)
                    carParkNameHeaderWidth += 70;

                if (!showingDisabledSpaces)
                    carParkNameHeaderWidth += 70;

                return carParkNameHeaderWidth.ToString();
            }
        }
        /// <summary>
        /// Returns whether the isSecure column is being shown
        /// </summary>
        /// <returns></returns>
        public bool GetShowingIsSecure
        {
            get
            {
                return showingIsSecure;
            }
        }

        /// <summary>
        /// Returns whether the NumberOfSpaces column is being shown
        /// </summary>
        /// <returns></returns>
        public bool GetShowingNumberOfSpaces
        {
            get
            {
                return showingNumberOfSpaces;
            }
        }

        /// <summary>
        /// Returns whether the DisabledSpaces column is being shown
        /// </summary>
        /// <returns></returns>
        public bool GetShowingDisabledSpaces
        {
            get
            {
                return showingDisabledSpaces;
            }
        }

        /// <summary>
        /// returns the number of disabled spaces for the current datarow as a string
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string GetHasDisabledSpaces(DataRow row)
        {
            switch (Convert.ToInt32(row[FindCarParkHelper.columnHasDisabledSpaces]))
            {
                case -1:
                    return "&nbsp;";
                default:
                    return "";
            }
            //this.fscDisabledImage.Visible = GetHasDisabledSpaces(row) == "Y" ? true : false;
        }
        /// <summary>
        /// returns the the IsSecure PMSPA mark indicator as a bool
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string GetIsSecure(DataRow row)        
        {
            switch (Convert.ToInt32(row[FindCarParkHelper.columnisSecure]))
            {
                case -1:
                    return "&nbsp;";
                default:
                    return "";
            }
            //this.fscSecureImage.Visible = GetIsSecure(row) == "Y" ? true : false;
        }
        /// <summary>
        /// Gets whether the issecure tick should be shown
        /// </summary>
        /// <param name="row"></param>
        /// <returns>boolean value indicating disabledspaces image should be visible or not</returns>
        public bool GetHasDisabledSpacesVisible(DataRow row)
        {
            switch (Convert.ToInt32(row[FindCarParkHelper.columnHasDisabledSpaces]))
            {
                case -1:
                    return false;
                case 0:
                    return false;
                default:
                    return true;
            }
            //this.fscSecureImage.Visible = GetIsSecure(row) == "Y" ? true : false;
        }
        /// <summary>
        /// Gets whether the issecure tick should be shown
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool GetIsSecureVisible(DataRow row)
        {
            switch (Convert.ToInt32(row[FindCarParkHelper.columnisSecure]))
            {
                case -1:
                    return false;
                case 0:
                    return false;
                case 1:
                    return true;
                default:
                    return true;
            }
            //this.fscSecureImage.Visible = GetIsSecure(row) == "Y" ? true : false;
        }
        /// <summary>
        /// returns the number of spaces for the current datarow as a string
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public string GetNumberOfSpaces(DataRow row)
        {
            if ((int)row[FindCarParkHelper.columnNumberOfSpaces] > 0)
            {
                return row[FindCarParkHelper.columnNumberOfSpaces].ToString();
            }
            else
            {
                return "-";
            }
        }

        /// <summary>
		/// Returns the Url to the radio button image.
		/// </summary>
		/// <param name="summary">The index of the line being rendered</param>
		/// <returns>Url string to the radio button image.</returns>
		public string GetButtonImageUrl(int index)
		{
			
			if(IsSelectedIndex(index))
			{
				return checkedRadioButtonImageUrl;
			}
			else 
			{
				return uncheckedRadioButtonImageUrl;
			}
		}

		/// <summary>
		/// Returns the car park reference of the current table row as a string
		/// </summary>
		/// <param name="row">DataRow</param>
		/// <returns>string</returns>
		public string GetCarParkRef(DataRow row)
		{
			return (string)row[FindCarParkHelper.columnCarParkRef];
		}

		/// <summary>
		/// Returns the OSGridReference of the current table row
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		public OSGridReference GetGridRef(DataRow row)
		{
			return (OSGridReference)row[FindCarParkHelper.columnGridRef];
		}

		/// <summary>
		/// Returns the Css class that the row text should be rendered with.
		/// </summary>
		/// <param name="summary">Current item being rendered.</param>
		/// <returns>Css class string.</returns>
		public string GetTextCssClass(int index)
		{
			// If there is only one result then no rows should
			// be highlighed. Check to see if this is the case.
			if(carParkPageState.ResultsTable.Rows.Count == 1)
			{
				return string.Empty;
			}
			else 
			{
				return (index % 2) == 0 ? "g":"";
			}
		}

		/// <summary>
		/// Used to set the Id attribute of the Html row in the
		/// item template of the repeater control.
		/// This can than be accessed in order to scroll it into view.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public string GetItemRowId(int index)
		{
			return repeaterResultTable.ClientID + "_itemRow_" + index.ToString(TDCultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Read only property returning the style of the header for the
		/// car park name column
		/// </summary>
		public string CarParkNameHeaderStyle
		{
			get
			{
				if ((carParkPageState.CurrentSortingType == FindCarParkPageState.SortingType.CarParkName)&& !boolPrintablePage)
					return "p";
				else
					return string.Empty;
			}
		}


		/// <summary>
		/// Read only property returning the style of the header for the
		/// row option column
		/// </summary>
		public string OptionHeaderStyle
		{
			get
			{
				if ((carParkPageState.CurrentSortingType == FindCarParkPageState.SortingType.Option)&& !boolPrintablePage)
					return "p";
				else
					return string.Empty;
			}
		}

		/// <summary>
		/// Read only property to indicate if the sort option should be displayed
		/// </summary>
		public bool SortVisible
		{
			get
			{
				if (boolPrintablePage)
					return false;
				else
					return true;
			}
		}

		/// <summary>
		/// Read only property returning the style of the header for the
		/// distance column
		/// </summary>
		public string DistanceHeaderStyle
		{
			get
			{
				if ((carParkPageState.CurrentSortingType == FindCarParkPageState.SortingType.Distance)&& !boolPrintablePage)
					return "p";
				else
					return string.Empty;
			}
		}

        /// <summary>
        /// Read only property returning the style of the header for the
        /// NumberOfSpaces column
        /// </summary>
        public string NumberOfSpacesHeaderStyle
        {
            get
            {
                if ((carParkPageState.CurrentSortingType == FindCarParkPageState.SortingType.TotalSpaces) && !boolPrintablePage)
                    return "p";
                else
                    return string.Empty;
            }
        }

        public string HasDisabledSpacesHeaderStyle
        {
            get
            {
                if ((carParkPageState.CurrentSortingType == FindCarParkPageState.SortingType.HasDisabledSpaces) && !boolPrintablePage)
                    return "p";
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// Read only property returning the style of the header for the
        /// NumberOfSpaces column
        /// </summary>
        public string PMSPAHeaderStyle
        {
            get
            {
                if ((carParkPageState.CurrentSortingType == FindCarParkPageState.SortingType.IsSecure) && !boolPrintablePage)
                    return "p";
                else
                    return string.Empty;
            }
        }

        /// <summary>
		/// Returns an overflow style
		/// This ensures a scroll bar is displayed when we have more than 10 car parks
		/// </summary>
		public string NumberOfRowsToDisplayStyle
		{
			get
			{
				if (carParkPageState.ResultsTable != null && !boolPrintablePage)
				{
                    if (carParkPageState.ResultsTable.Rows.Count > maxNumberOfCarParkRows)
                        return "fscResultsItemsDivOverflow"; 
                    else
                        return "fscResultsItemsDivNoOverflow"; 
				}
				else
				{
                    return "fscResultsItemsDivNoOverflow"; 
				}
			}
		}

		/// <summary>
		/// Returns a Table Width attribute
		/// </summary>
		public string GetTableWidth
		{
			get
			{
				if (carParkPageState.ResultsTable != null)
				{
                    // Always return 100%, scrollbars added by stylesheet to surrounding div if needed
                    return "100%";
				}
				else
				{
					return string.Empty;
				}
			}
		}

		private const string displayNoneStyle = "display: none";

		/// <summary>
		/// Read only property returning the style of the visible
		/// selection
		/// </summary>
		public string IsSelectVisible
		{
			get
			{
				string style = boolPrintablePage? displayNoneStyle : String.Empty;
				return  style;
			}
		}

		/// <summary>
		/// Read only property returning the URL of the sort by option image
		/// </summary>
		public string SortByOptionUrl
		{
			get
			{
				return GetResource("FindCarParkResultsTable.commandSortByOption.ImageUrl");
			}
		}

		/// <summary>
		/// Read only property returning the alternate text of the sort by option
		/// </summary>
		public string SortByOptionAlternateText
		{
			get
			{
				return GetResource("FindCarParkResultsTable.commandSortByOption.AlternateText");
			}
		}

		/// <summary>
		/// Read only property returning the URL of the sort by car park name image
		/// </summary>
		public string SortByCarParkNameUrl
		{
			get
			{
				return GetResource("FindCarParkResultsTable.commandSortByCarParkName.ImageUrl");
			}
		}

		/// <summary>
		/// Read only property returning the alternate text of the sort by car park name
		/// </summary>
		public string SortByCarParkNameAlternateText
		{
			get
			{
				return GetResource("FindCarParkResultsTable.commandSortByCarParkName.AlternateText");
			}
		}

		/// <summary>
		/// Read only property returning the URL of the sort by distance image
		/// </summary>
		public string SortByDistanceUrl
		{
			get
			{
				return GetResource("FindCarParkResultsTable.commandSortByDistance.ImageUrl");
			}
		}

		/// <summary>
		/// Read only property returning the alternate text of the sort by distance
		/// </summary>
		public string SortByDistanceAlternateText
		{
			get
			{
				return GetResource("FindCarParkResultsTable.commandSortByDistance.AlternateText");
			}
		}

		/// <summary>
		/// Read only property returning the URL of the sort by option number symbol
		/// </summary>
		public string OptionSortSymbolUrl
		{
			get
			{
				return carParkPageState.IsOptionSortingAsc?
				sortAscUrl : sortDescUrl;
			}
		}

		/// <summary>
		/// Read only property returning the alternate text of the sort by option symbol
		/// </summary>
		public string OptionSortSymbolAlternateText
		{
			get
			{
				return carParkPageState.IsOptionSortingAsc ? sortAscAlternateText : sortDescAlternateText;
				
			}
		}
		
		/// <summary>
		/// Read only property returning the URL of the sort by car park name symbol
		/// </summary>
		public string CarParkNameSortSymbolUrl
		{
			get
			{
				return carParkPageState.IsCarParkSortingAsc ? sortAscUrl : sortDescUrl;
			}
		}

        /// <summary>
        /// readonly property returning the url of the sort by number of spaces symbol
        /// </summary>
        public string NumberOfSpacesSortSymbolUrl
        {
            get
            {
                return carParkPageState.IsTotalSpacesAsc ? sortAscUrl : sortDescUrl;
            }
        }
        /// <summary>
        /// Read only property returning the alternate text of the sort by total spaces symbol
        /// </summary> 
        public string SortByNumberOfSpacesAlternateText
        {
            get
            {
                return GetResource("FindCarParkResultsTable.commandSortByNumberOfSpaces.AlternateText");
            }
        }

        /// <summary>
        /// Read only property returning the alternate text of the sort by disabled spaces symbol
        /// </summary>
        public string SortByHasDisabledSpacesAlternateText
        {
            get
            {
                return GetResource("FindCarParkResultsTable.commandSortByHasDisabledSpaces.AlternateText");
            }
        }

        /// <summary>
        /// readonly property returning the url of the sort by secure symbol
        /// </summary>
        public string PMSPASortSymbolUrl
        {
            get
            {
                return carParkPageState.IsIsSecureAsc ? sortAscUrl : sortDescUrl;
            }
        }
        /// <summary>
        /// readonly property returning the url of the sort by secure symbol
        /// </summary>
        public string HasDisabledSpacesSymbolUrl
        {
            get
            {
                return carParkPageState.IsHasDisabledSpacesAsc ? sortAscUrl : sortDescUrl;
            }
        }
        /// <summary>
        /// readonly property to return the alt text for the is secure column
        /// </summary>
        public string SortByPMSPAAlternateText
        {
            get
            {
                return GetResource("FindCarParkResultsTable.commandSortBySecureCarPark.AlternateText");
            }
        }


		/// <summary>
		/// Read only property returning the alternate text of the sort by car park symbol
		/// </summary>
		public string CarParkNameSortSymbolAlternateText
		{
			get
			{
				return carParkPageState.IsCarParkSortingAsc ? sortAscAlternateText : sortDescAlternateText;
			}
		}

		/// <summary>
		/// Read only property returning the URL of the sort by distance symbol
		/// </summary>
		public string DistanceSortSymbolUrl
		{
			get
			{
				return carParkPageState.IsDistanceSortingAsc ? sortAscUrl : sortDescUrl;
			}
		}

		/// <summary>
		/// Read only property returning the alternate text of the sort by distance symbol
		/// </summary>
		public string DistanceSortSymbolAlternateText
		{
			get
			{
				return carParkPageState.IsDistanceSortingAsc ? sortAscAlternateText : sortDescAlternateText;
			}
		}

		/// <summary>
		/// Writeable only property setting the printable status of the page - true if control 
		/// is on a printable page 
		/// </summary>
		public bool Printable
		{
			set
			{
				boolPrintablePage = value;
			}
		}

		/// <summary>
		/// Read only property returning the title of the table
		/// </summary>
		public string CarParkTitle
		{
			get
			{
				return GetResource("FindCarParkResultsTable.labelCarParkNameTitle");
			}
		}

		/// <summary>
		/// Read only property returning the title of the distance column of the table
		/// </summary>
		public string DistanceTitle
		{
			get
			{
				return GetResource("FindStationResultsTable.labelDistanceTitle");
			}
		}

        /// <summary>
        /// Read only property returning the title of the select column of the table
        /// </summary>
        public string NumberOfSpacesTitle
        {
            get
            {
                return GetResource("FindStationResultsTable.TotalSpacesCaption");
            }
        }

        /// <summary>
        /// Read only property returning the title of the select column of the table
        /// </summary>
        public string PMSPATitle
        {
            get
            {
                return GetResource("FindStationResultsTable.SecureCarParkCaption");
            }
        }

        public string HasDisabledSpacesTitle
        {
            get
            {
                return GetResource("FindStationResultsTable.HasDisabledSpacesTitle");
            }
        }
        
        /// <summary>
		/// Read only property returning the title of the select column of the table
		/// </summary>
		public string SelectTitle
		{
			get
			{
				return GetResource("FindStationResultsTable.labelSelectTitle");
			}
		}

		/// <summary>
		/// Read only property returning the title of the option column of the table
		/// </summary>
		public string OptionTitle
		{
			get
			{
				return GetResource("FindStationResultsTable.labelOptionTitle");
			}
		}

        /// <summary>
        /// Read/Write property to determin if the row index should show as link
        /// </summary>
        public bool IsRowSelectLinkVisible
        {
            get
            {
                TDPage page = (TDPage)this.Page;
                return rowSelectLinkVisible && !boolPrintablePage && page.IsJavascriptEnabled;
            }
            set
            {
                rowSelectLinkVisible = value;
            }
        }

        /// <summary>
        /// Map client side id to generate script to show car parks when they get clicked
        /// </summary>
        public string MapClientID
        {
            set
            {
                mapClientID = value;
            }
        }

        /// <summary>
        /// Map scroll to id to allow page to scroll to the first map element
        /// </summary>
        public string MapScrollToID
        {
            set { mapScrollToID = value; }
        }

		/// <summary>
		/// Handler for the Car Park name sorting button click event. Reverse sorting (asc/desc)
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">ImageClickEventArgs</param>
		public  void CommandSortByCarParkName_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (!boolPrintablePage)
			{
				if (carParkPageState.CurrentSortingType != FindCarParkPageState.SortingType.CarParkName)
				{
					carParkPageState.CurrentSortingType = FindCarParkPageState.SortingType.CarParkName;
					carParkPageState.IsDistanceSortingAsc = false;
					carParkPageState.IsOptionSortingAsc = false;
					carParkPageState.IsCarParkSortingAsc = true;
                    carParkPageState.IsTotalSpacesAsc = false;
                    carParkPageState.IsIsSecureAsc = false;
                    carParkPageState.IsHasDisabledSpacesAsc = false;
				}
				else
				{
					carParkPageState.IsCarParkSortingAsc = !carParkPageState.IsCarParkSortingAsc;
				}

				Refresh();
			}
		}

		/// <summary>
		/// Handler for the Distance sorting button click event. Reverse sorting (asc/desc)
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">EventArgs</param>
		public void CommandSortByDistance_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (!boolPrintablePage)
			{
				if (carParkPageState.CurrentSortingType != FindCarParkPageState.SortingType.Distance)
				{
					carParkPageState.CurrentSortingType = FindCarParkPageState.SortingType.Distance;
					carParkPageState.IsDistanceSortingAsc = true;
					carParkPageState.IsOptionSortingAsc = false;
					carParkPageState.IsCarParkSortingAsc = false;
                    carParkPageState.IsTotalSpacesAsc = false;
                    carParkPageState.IsIsSecureAsc = false;
                    carParkPageState.IsHasDisabledSpacesAsc = false;
				}
				else
				{
					carParkPageState.IsDistanceSortingAsc = !carParkPageState.IsDistanceSortingAsc;
				}
				Refresh();
			}

		}

        /// <summary>
        /// Handler for the Distance sorting button click event. Reverse sorting (asc/desc)
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        public void CommandSortByDisabled_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (!boolPrintablePage)
            {
                if (carParkPageState.CurrentSortingType != FindCarParkPageState.SortingType.HasDisabledSpaces)
                {
                    carParkPageState.CurrentSortingType = FindCarParkPageState.SortingType.HasDisabledSpaces;
                    carParkPageState.IsDistanceSortingAsc = false;
                    carParkPageState.IsOptionSortingAsc = false;
                    carParkPageState.IsCarParkSortingAsc = false;
                    carParkPageState.IsTotalSpacesAsc = false;
                    carParkPageState.IsIsSecureAsc = false;
                    carParkPageState.IsHasDisabledSpacesAsc = false; // Set to false because we want disabled to go Yes->No on first sort click
                }
                else
                {
                    carParkPageState.IsHasDisabledSpacesAsc = !carParkPageState.IsHasDisabledSpacesAsc;
                }
                Refresh();
            }

        }
        
        /// <summary>
		/// Handler for the when Option sorting button click event. Reverse sorting (asc/desc)
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">EventArgs</param>
		public void CommandSortByOption_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (!boolPrintablePage)
			{
				if (carParkPageState.CurrentSortingType != FindCarParkPageState.SortingType.Option)
				{
					carParkPageState.CurrentSortingType = FindCarParkPageState.SortingType.Option;
					carParkPageState.IsDistanceSortingAsc = false;
					carParkPageState.IsOptionSortingAsc = true;
					carParkPageState.IsCarParkSortingAsc = false;
                    carParkPageState.IsTotalSpacesAsc = false;
                    carParkPageState.IsIsSecureAsc = false;
                    carParkPageState.IsHasDisabledSpacesAsc = false;
				}
				else
				{
					carParkPageState.IsOptionSortingAsc = !carParkPageState.IsOptionSortingAsc;
				}
				Refresh();
			}

		}

        /// <summary>
        /// Handler for the when Option sorting button click event. Reverse sorting (asc/desc)
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        public void CommandSortByNumberOfSpaces_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (!boolPrintablePage)
            {
                if (carParkPageState.CurrentSortingType != FindCarParkPageState.SortingType.TotalSpaces)
                {
                    carParkPageState.CurrentSortingType = FindCarParkPageState.SortingType.TotalSpaces;
                    carParkPageState.IsDistanceSortingAsc = false;
                    carParkPageState.IsOptionSortingAsc = false;
                    carParkPageState.IsCarParkSortingAsc = false;
                    carParkPageState.IsTotalSpacesAsc = false; // Set to false because we want spaces to go high->low on first sort click
                    carParkPageState.IsIsSecureAsc = false;
                    carParkPageState.IsHasDisabledSpacesAsc = false;
                }
                else
                {
                    carParkPageState.IsTotalSpacesAsc = !carParkPageState.IsTotalSpacesAsc;
                }
                Refresh();
            }

        }

        /// <summary>
        /// Handler for the when Option sorting button click event. Reverse sorting (asc/desc)
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        public void CommandSortBySecure_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (!boolPrintablePage)
            {
                if (carParkPageState.CurrentSortingType != FindCarParkPageState.SortingType.IsSecure)
                {
                    carParkPageState.CurrentSortingType = FindCarParkPageState.SortingType.IsSecure;
                    carParkPageState.IsDistanceSortingAsc = false;
                    carParkPageState.IsOptionSortingAsc = false;
                    carParkPageState.IsCarParkSortingAsc = false;
                    carParkPageState.IsTotalSpacesAsc = false;
                    carParkPageState.IsIsSecureAsc = false; // Set to false because we want secure to go Yes->No on first sort click
                    carParkPageState.IsHasDisabledSpacesAsc = false;
                }
                else
                {
                    carParkPageState.IsIsSecureAsc = !carParkPageState.IsIsSecureAsc;
                }
                Refresh();
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
		}

		/// <summary>
		/// Hides the map.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{	
			carParkPageState.IsShowingHidingMap = false;

            // Reload the data to ensure if user has triggered another event on the page
            // the results table is rendered correctly
            if ((Page.IsPostBack) && (repeaterResultTable.Visible))
            {
                Refresh();
            }

			SetupControls();

			base.OnPreRender(e);
		}

		/// <summary>
		/// Sets-up event handlers 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e)
		{
            // Get resources
            sortAscUrl = GetResource("CarParking.Ascending.ImageURL");
            sortDescUrl = GetResource("CarParking.Descending.ImageURL");

			UpdateData();
			AddEventHandlers();
			base.OnLoad(e);
		}

		/// <summary>
		/// Checks TDSessionManager to find the data that should be rendered,
		/// sets it as the datasource to the repeater and binds.
		/// </summary>
		private void UpdateData()
		{
			if( carParkPageState.ResultsTable == null )
			{
				repeaterResultTable.DataSource = new Object[0];
				repeaterResultTable.DataBind();
				return;
			}

			Refresh();
		}

		#endregion

		#region Event handlers
		/// <summary>
		/// Method to add the event handlers to add dynamically generated buttons
		/// </summary>
		private void AddEventHandlers()
		{

			repeaterResultTable.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler (repeaterResultTable_ItemDatabound);

			for (int i=0; i < repeaterResultTable.Items.Count; i++)
			{
				if	((HyperlinkPostbackControl)repeaterResultTable.Items[i].FindControl("carParkInfoLinkControl") != null)
				{
					((HyperlinkPostbackControl)repeaterResultTable.Items[i].FindControl("carParkInfoLinkControl")).link_Clicked +=
						new System.EventHandler(this.CarParkInformationLinkClick);
				}
				if((ImageButton)repeaterResultTable.Items[i].FindControl("radioButtonImage") != null)
				{
					((ImageButton)repeaterResultTable.Items[i].FindControl("radioButtonImage")).Click +=
						new System.Web.UI.ImageClickEventHandler(this.RadioButtonClick);
				}
			}
		}

		/// <summary>
		/// Event Handler for the information hyperlink.
		/// </summary>
		public void CarParkInformationLinkClick(object sender, EventArgs e)
		{
			// User has clicked the information hyperlink for a particular carpark.

			HyperlinkPostbackControl link = (HyperlinkPostbackControl)sender;

			string carParkRef =  link.CommandArgument;

			TDSessionManager.Current.InputPageState.CarParkReference = carParkRef;

			// This is how we 'return'
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(((TDPage)Page).PageId);

			// Show the information page for the selected location.
			// Write the Transition Event
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.FindCarParkResultsInfo;
		}

		/// <summary>
		/// The event handler for the repeaterResultTable Item Data Bound event
		/// A call to the ScrollManager is added to scroll the correct row into view.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void repeaterResultTable_ItemDatabound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			// If dealing with selected item
			if ((e.Item.ItemIndex > 0)&&(IsSelectedIndex(e.Item.ItemIndex)))
			{
				//bool scrollPointExceeded = (journeySummaryLines.Count > scrollPoint);
				//if (  ((MaxResultsToShow == 0) || (MaxResultsToShow == TDJourneyViewState.RESULTS_TO_SHOW_UNDEFINED) ) && scrollPointExceeded)
					((TDPage)this.Page).ScrollManager.ScrollElementToView(GetItemRowId(e.Item.ItemIndex));
			}
		}
		#endregion

	}
}
