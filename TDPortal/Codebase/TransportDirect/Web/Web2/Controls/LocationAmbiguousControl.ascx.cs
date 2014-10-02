// ******************************************************************* 
// NAME         : LocationAmbiguousControl.ascx
// AUTHOR       : Tolu Olomolaiye
// DATE CREATED : 26/09/2005 
// DESCRIPTION  : Ambiguous control which is initially being used for Visit 
// Planner but will eventually replace AmbiguousLocationSelectControl2
// ******************************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/LocationAmbiguousControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Apr 02 2008 13:01:46   apatel
//added property to set visibility of new location button
//
//   Rev 1.2   Mar 31 2008 13:21:48   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:02   mturner
//Initial revision.
//
//   Rev 1.16   Nov 14 2006 10:07:28   rbroddle
//Merge for stream4220
//
//   Rev 1.15.1.1   Nov 12 2006 13:48:28   tmollart
//Modified logic of Populate method for FindTrainCostInput.
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.15.1.0   Nov 10 2006 10:35:04   tmollart
//Modified code to gaz selection not available for Rail Cost Input.
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.15   Oct 06 2006 15:37:24   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.14.1.0   Aug 14 2006 17:47:50   esevern
//Added check for FindCarParkInput page when hiding 'New location' button
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.14   Apr 12 2006 15:15:06   esevern
//Populate method amended to add PageId.ParkAndRide to the list of input pages for which the newLocationTable should be hidden.
//Resolution for 3804: DN058 Park and Ride Phase 2 - Ambiguity mode on input page should not display New Location button
//
//   Rev 1.13   Apr 05 2006 15:23:28   esevern
//Manual merge of stream0030
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.12   Apr 04 2006 15:11:28   AViitanen
//Added ExtendJourneyInput to list of pages where newlocationtable is not visible. 
//Resolution for 3730: DN068 Extend: New Location button not required on ambiguity page
//
//   Rev 1.11   Feb 23 2006 16:12:24   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.10   Jan 04 2006 10:06:24   tolomolaiye
//Updates folllowing Visit Planner code review
//
//   Rev 1.9   Dec 20 2005 15:04:20   jbroome
//Removed New Location button on Business Links page - extension of IR #3339 which was raised and closed whilst Business Links fucntionality was being developed.
//
//   Rev 1.8   Dec 07 2005 16:23:04   ralonso
//Fixed problem with New Location button
//Resolution for 3339: Problem with New Location button
//
//   Rev 1.7   Dec 01 2005 12:32:56   ralonso
//Fixed problem with dropdown list missing
//
//   Rev 1.6   Nov 30 2005 11:38:26   ralonso
//Fixed - New Location button does not appear in LocationAmbiguityControl
//Resolution for 3254: New Location button appear in Ambiguity page
//
//   Rev 1.5   Nov 10 2005 16:29:54   rgreenwood
//IR2979 Fix
//Resolution for 2979: UEE: Door to Door - locations entered are not displayed on ambiguity screen
//
//   Rev 1.4   Nov 10 2005 12:40:32   halkatib
//Correction to New Location handling
//
//   Rev 1.3   Nov 04 2005 13:35:32   NMoorhouse
//Manual merge of stream2816
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.2   Nov 01 2005 17:46:28   tolomolaiye
//Merge for stream 2638
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1.1.3   Nov 02 2005 11:17:04   rgreenwood
//TD089 ES020 Image button replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.1.1.2   Oct 25 2005 12:55:10   tolomolaiye
//Added NewLocation property
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1.1.1   Oct 07 2005 10:21:44   mtillett
//Hide new location button when gazetter hidden
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.1.1.0   Oct 07 2005 09:40:48   mtillett
//Add functionality to support use by all pages for UEE changes
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.1   Oct 05 2005 09:44:22   tolomolaiye
//Updates following code review and fxcop
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Sep 28 2005 15:20:16   tolomolaiye
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;	
	using TransportDirect.Common.ResourceManager;
	using System.Globalization;
	using System.Text;
	using System.Web;
	using System.Web.UI.WebControls;

	using TransportDirect.UserPortal.LocationService;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.Common;


	/// <summary>
	///		Summary description for LocationAmbiguousControl.
	/// </summary>
    public partial class LocationAmbiguousControl : TDUserControl
	{

		private DataServiceType listType;
		private TDLocation thisLocation;

		private string moreOptions = String.Empty;
		private string possibleOptions = String.Empty;

		private LocationSearch thisSearch;
		private string strAmbiguousLocation = String.Empty;
		private string gazetteerSelectedValue = String.Empty;
		private bool setDone = false;
		protected System.Web.UI.HtmlControls.HtmlTable newlocation;

		private const string UNREFINED_TEXT = "unrefined";

		public event EventHandler NewSearchType;
		public event EventHandler NewLocation;

        public bool newLocationVisible = true;

        /// <summary>
        /// Read/Write property to set visiblity of the NewLocation button
        /// </summary>
        public bool NewLocationVisible
        {
            get { return newLocationVisible; }
            set { newLocationVisible = value; }

        }


		/// <summary>
		/// Page load event 
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//set the text for some labels from the resource file
			labelChooseLocation.Text = GetResource("LocationAmbiguousControl.labelChooseLocation.Text");

			commandNewLocation.Text = Global.tdResourceManager.GetString(
				"AmbiguousLocationSelectControl2.commandNewLocation.Text");
		}

        /// <summary>
        /// Page prerender event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            commandNewLocation.Visible = newLocationVisible;
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
            this.StationTypesDropDown.SelectedIndexChanged 
                += new EventHandler(this.StationTypesDropDown_SelectedIndexChanged);
            this.commandNewLocation.Click += new EventHandler(this.CommandNewLocationClick);
		}
		#endregion

		/// <summary>
		/// Sets the index of the drop down to unselected (-1)
		/// </summary>
		public void ResetSelectedIndex()
		{
			SelectOptionDropDown.SelectedIndex = -1;
		}

		/// <summary>
		/// Initialisation method. Initialises the Search and Location variables
		/// </summary>
		/// <param name="search">Search associated with control</param>
		/// <param name="location">location associated with control</param>
		public void SetMembers(ref LocationSearch search, ref TDLocation location)
		{
			this.thisSearch = search;
			this.thisLocation = location;
		}

		/// <summary>
		/// Populates the Station Types drop down list
		/// </summary>
		private void PopulateStationTypesDropDown(DataServiceType listType)
		{
			if (!setDone)
			{
				//set selected value to keep
				gazetteerSelectedValue = StationTypesDropDown.SelectedValue;
				setDone = true;
			}

			IDataServices populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			populator.LoadListControl(listType, StationTypesDropDown, this.resourceManager);

			//make sure the correct item is selected
			SetStationTypesDropDownSelected();
		}
		private void SetStationTypesDropDownSelected()
		{
			StationTypesDropDown.SelectedIndex = -1;
			switch (thisSearch.SearchType)
			{
				case SearchType.AddressPostCode:
					if (StationTypesDropDown.Items.FindByValue("Address") != null)
					{
						StationTypesDropDown.Items.FindByValue("Address").Selected = true;
					}
					break;
				case SearchType.MainStationAirport:
					if (StationTypesDropDown.Items.FindByValue("Stations") != null)
					{
						StationTypesDropDown.Items.FindByValue("Stations").Selected = true;
					}
					break;
				case SearchType.City:
				case SearchType.Locality:
					if (StationTypesDropDown.Items.FindByValue("City") != null)
					{
						StationTypesDropDown.Items.FindByValue("City").Selected = true;
					}
					break;
				case SearchType.POI:
					if (StationTypesDropDown.Items.FindByValue("Attraction") != null)
					{
						StationTypesDropDown.Items.FindByValue("Attraction").Selected = true;
					}
					break;
				case SearchType.AllStationStops:
					if (StationTypesDropDown.Items.FindByValue("AllStops") != null)
					{
						StationTypesDropDown.Items.FindByValue("AllStops").Selected = true;
					}
					break;
				default:
					StationTypesDropDown.SelectedIndex = 0;
					break;
			}
		}

		/// <summary>
		/// Populate the control
		/// </summary>
		/// <param name="listType">The data service set to use in creating radio buttons that select search type</param>
		/// <param name="thisSearch">current LocationSearch object</param>
		/// <param name="indexes">Indexes of user choices</param>
		public void Populate(DataServiceType listType, ref LocationSearch thisSearch, ref TDLocation thisLocation)
		{
			SetMembers ( ref thisSearch, ref thisLocation);

			this.listType = listType;

			string locationText;
			if (thisSearch.CurrentLevel == 0) 
			{
				locationText = thisSearch.InputText;
			} 
			else 
			{
				locationText = thisSearch.GetQueryResult(thisSearch.CurrentLevel).ParentChoice.Description;
			}

			possibleOptions = GetResource("LocationAmbiguousControl.possibleOptions");
			
			moreOptions = GetResource("LocationAmbiguousControl.moreOptions");

			labelPossibleLocations.Text = string.Format(possibleOptions,locationText);

			// Since we maybe repopulating the same list, store the existing selected option 
			int selectedIndex = SelectOptionDropDown.SelectedIndex;
			// Clear existing list
			SelectOptionDropDown.Items.Clear();

			// Then repopulate
			LocationChoiceList choices = thisSearch.GetCurrentChoices(thisSearch.CurrentLevel);
			PopulateLocationDropDown(choices, SelectOptionDropDown, selectedIndex, thisSearch.SupportHierarchic);

            
			if (PageId == PageId.JourneyPlannerAmbiguity || 
				PageId == PageId.FindTrainInput ||
				PageId == PageId.FindTrainCostInput ||
				PageId == PageId.FindCarInput ||
				PageId == PageId.FindCoachInput ||
				PageId == PageId.FindStationInput ||
				PageId == PageId.VisitPlannerInput ||
				PageId == PageId.BusinessLinks ||
				PageId == PageId.FindBusInput ||
				PageId == PageId.ParkAndRideInput ||
				PageId == PageId.FindCarParkInput ||
				PageId == PageId.ExtendJourneyInput)
			{
				newlocationtable.Visible = false;
			}
			
			// Display gazetteer radio buttons but only on certain pages
            if (PageId == PageId.FindTrainInput || PageId == PageId.FindTrainCostInput || PageId == PageId.FindCoachInput)
            {
                gazetter.Visible = false;
			} 
            else 
            {
                gazetter.Visible = true;
				//populate the station types drop down list
				PopulateStationTypesDropDown(listType);
			}

			//commandNewLocation should not be visible when an Extend is in progress and the location is fixed,
			//it should be visible at all other times.
            commandNewLocation.Visible = !(thisSearch.LocationFixed && TDItineraryManager.Current.ExtendInProgress);
		}

		/// <summary>
		/// populate the dropdownlist with the description and the index of the choice in the list
		/// </summary>
		/// <param name="choices">choices</param>
		/// <param name="list">list to populate</param>
		/// <param name="index">the option to set as selected</param>
		/// <param name="isHierarchic">true if the drop down contains drillable options</param>
		private void PopulateLocationDropDown( LocationChoiceList choices, DropDownList list, int selectedIndex, bool isHierarchic)
		{
			int i= 0;
			ListItem item;
			StringBuilder itemText = new StringBuilder(string.Empty);
			StringBuilder itemValue = new StringBuilder(string.Empty);

			//the first item in the SelectOptionDropDown drop down list should be "please select"
			string itemPleaseSelect = GetResource("LocationAmbiguousControl.itemPleaseSelect");

			item = new ListItem(itemPleaseSelect, UNREFINED_TEXT);
			list.Items.Add(item);
			foreach (LocationChoice choice in choices)
			{
				// For hierarchic searches add options for drillable locations
				if (isHierarchic) 
				{
					// If option is not an admin area, add option to select the location.
					// A choice which is an admin area should only appear as an option to drilldown
					// otherwise it can appear as both a location to select and a location
					// to drilldown to
					if (!choice.IsAdminArea) 
					{
						item = new ListItem(choice.Description, i.ToString(CultureInfo.InvariantCulture));
						list.Items.Add(item);
					}
					// If location has children, add option to drilldown
					if (choice.HasChilden) 
					{
						itemText = new StringBuilder(string.Empty);
						itemText.Append(moreOptions);
						itemText.Append(" ");
						itemText.Append(choice.Description);
						itemText.Append("...");

						itemValue = new StringBuilder(string.Empty);
						itemValue.Append("+");
						itemValue.Append(i.ToString(CultureInfo.InvariantCulture));

						item = new ListItem(itemText.ToString(), itemValue.ToString());
						
						list.Items.Add(item);
					}
				} 
				else 
				{
					item = new ListItem(choice.Description, i.ToString(CultureInfo.InvariantCulture));
					list.Items.Add(item);
				}
				i++;
			}
			if (selectedIndex < list.Items.Count)
				list.SelectedIndex = selectedIndex;
		}

		/// <summary>
		/// Determine the value selectecd by the user
		/// </summary>
		public void RefineLocation(int level)
		{
			//exit if the current value is "unrefined"
			if (SelectOptionDropDown.SelectedItem.Value == UNREFINED_TEXT)
				return;

			// Determine if the selected option was to drill down
			bool drillable = SelectOptionDropDown.SelectedItem.Value.StartsWith("+");

			// Get the selected choice; this will be the same choice whether
			// the selected option was for a location or for a drill down into 
			// the location (the "+" will be ignored by the indexer)
			LocationChoice choice = (LocationChoice)thisSearch.GetCurrentChoices(level)
				[Convert.ToInt32(SelectOptionDropDown.SelectedItem.Value, CultureInfo.CurrentCulture.NumberFormat)]; 

			// Drill down if drill down option selected or
			// this is not a hierarchic search
			if (drillable || !thisSearch.SupportHierarchic) 
			{
    
				LocationSearchHelper.LocationDrillDown(
					level,  
					choice,
					ref thisSearch,
					ref thisLocation
					);

			}
				// Option selected is not a drill down option on
				// a hierarchic search
			else
			{
				LocationSearchHelper.GetLocationDetails(
					choice,
					ref thisSearch, 
					ref thisLocation);
			}

		}
		/// <summary>
		/// Event handler for the New location button click event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        protected void CommandNewLocationClick(object sender, EventArgs e)
        {
			if (NewLocation != null)
			{
				NewLocation(sender, e);
			}
        }
		/// <summary>
		/// Event handler for the change event of the gazetteer drop down list
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void StationTypesDropDown_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			switch (gazetteerSelectedValue)
			{
				case "Address":
					thisSearch.SearchType = SearchType.AddressPostCode;
					break;
				case "Stations":
					thisSearch.SearchType = SearchType.MainStationAirport;
					break;
				case "City":
					thisSearch.SearchType = SearchType.Locality;
					break;
				case "Attraction":
					thisSearch.SearchType = SearchType.POI;
					break;
				case "AllStops":
					thisSearch.SearchType = SearchType.AllStationStops;
					break;
				default:
					//don't change selection
					return;
			}
			//set the selected value
			SetStationTypesDropDownSelected();
			//raise event
			if (NewSearchType != null)
			{
				NewSearchType(sender, e);
			}
		}

	}
}
