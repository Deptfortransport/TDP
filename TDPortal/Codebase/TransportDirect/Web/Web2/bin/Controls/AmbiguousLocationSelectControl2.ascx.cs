// *********************************************** 
// NAME                 : AmbiguousLocationSelectControl2.ascx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 2/12/2003 
// DESCRIPTION  : New version for AmbiguousLocationSelectControl. Allows user to refine his choice from a list of different locations.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AmbiguousLocationSelectControl2.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:19:04   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:54   mturner
//Initial revision.
//
//   Rev 1.10   Feb 23 2006 19:16:18   build
//Automatically merged from branch for stream3129
//
//   Rev 1.9.1.0   Jan 10 2006 15:23:12   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.9   Feb 14 2005 15:39:48   PNorell
//Updated with the latest requirements for IR1905
//Resolution for 1905: Cumbria causes error when issued as a locality
//
//   Rev 1.8   Jul 22 2004 16:01:14   COwczarek
//Disable gazatteer radio buttons when on Find A Train or
//Find A Coach input pages
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.7   Jul 15 2004 13:36:54   jbroome
//IR 1216. Moved string initialisation to Populate() to ensure correct Culture setting.
//
//   Rev 1.6   May 19 2004 15:00:28   acaunt
//commandNewLocation visibility is now determined by the LocationSearch's LocationFixed property:
//Visibility = !LocationFixed.
//
//   Rev 1.5   Apr 08 2004 14:23:16   COwczarek
//Use correct data services data set in generating radio buttons
//by supplying value through Populate method. No
//longer display "greyed out" radio buttons - replace static table
//with table control to which required radio button images can be
//added dynamically.
//Resolution for 695: DEL 5.2 QA Changes - Ambiguity page
//
//   Rev 1.4   Mar 16 2004 10:09:32   asinclair
//Added Alt text for radiobutton images
//
//   Rev 1.3   Mar 12 2004 19:38:16   AWindley
//DEL5.2 Added label text for Screen Reader
//
//   Rev 1.2   Mar 03 2004 15:34:14   COwczarek
//There is now only one drop down list associated with this control that displays locations for the current drill down level. The map can no longer be selected for ambiguity resolution.
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.1   Dec 04 2003 13:09:06   passuied
//final version for del 5.1
//
//   Rev 1.0   Dec 02 2003 16:17:22   passuied
//Initial Revision



using System;using TransportDirect.Common.ResourceManager;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TransportDirect.UserPortal.Web;
using TransportDirect.Web.Support;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using System.Globalization;


namespace TransportDirect.UserPortal.Web.Controls
{
    

    
    /// <summary>
    ///     New version for AmbiguousLocationSelectControl. Allows user to refine his choice from a list of different locations.
    /// </summary>
    public partial  class AmbiguousLocationSelectControl2 : TDUserControl
    {
        #region declaration
        protected System.Web.UI.WebControls.Image Image9;
        protected System.Web.UI.WebControls.Image Image7;
        protected System.Web.UI.WebControls.Label labelLocationType2;
        protected System.Web.UI.WebControls.Label labelLocationType3;
        protected System.Web.UI.WebControls.ImageButton commandAddress;
        protected System.Web.UI.WebControls.Label labelAddress;
        protected System.Web.UI.WebControls.ImageButton commandCity;
        protected System.Web.UI.WebControls.Label labelCity;
        protected System.Web.UI.WebControls.ImageButton commandAttractions;
        protected System.Web.UI.WebControls.Label labelAttractions;
        protected System.Web.UI.WebControls.ImageButton commandMainStations;
        protected System.Web.UI.WebControls.Label labelMainStations;
        protected System.Web.UI.WebControls.ImageButton commandAllStations;
        protected System.Web.UI.WebControls.Label labelAllStations;

        private DataServiceType listType;
        private DataServices.DataServices populator;
        private LocationSearch thisSearch = null;
        private TDLocation thisLocation = null;
        private string moreOptionsText = string.Empty;
        private string possibleOptionsText = string.Empty;
        
        public event EventHandler NewSearchType;
        public event EventHandler NewLocation;

                
        #endregion

        #region Image Urls of radio button
        
        private string imageSelected = String.Empty;
		private string imageUnselected = String.Empty;

        #endregion

        #region Contructor and Page_Load
        /// <summary>
        /// Constructor
        /// </summary>
        protected AmbiguousLocationSelectControl2()
        {
            
            populator = (DataServices.DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

        }
        protected void Page_Load(object sender, System.EventArgs e)
        {

            // Initialise radio button labels and alternate text
            // Note that this initialisation must be performed on every page load
            // since the images representing radio buttons and corresponding labels
            // are created in OnInit method - they are not defined in the ASP page

            labelAddress.Text = populator.GetText(
                listType, Enum.GetName(typeof(SearchType), SearchType.AddressPostCode));
            labelAllStations.Text = populator.GetText(
                listType, Enum.GetName(typeof(SearchType), SearchType.AllStationStops));
            labelAttractions.Text = populator.GetText(
                listType, Enum.GetName(typeof(SearchType), SearchType.POI));
            labelMainStations.Text = populator.GetText(
                listType, Enum.GetName(typeof(SearchType), SearchType.MainStationAirport));
            labelCity.Text = populator.GetText(
                listType, Enum.GetName(typeof(SearchType), SearchType.Locality));
                        
            commandNewLocation.ImageUrl = Global.tdResourceManager.GetString(
                "AmbiguousLocationSelectControl2.commandNewLocation.ImageUrl",
                TDCultureInfo.CurrentUICulture);
            commandNewLocation.AlternateText = Global.tdResourceManager.GetString(
                "AmbiguousLocationSelectControl2.commandNewLocation.AlternateText",
                TDCultureInfo.CurrentUICulture);

			commandAddress.AlternateText = Global.tdResourceManager.GetString(
				"AmbiguousLocationSelectControl2.commandAddress.AlternateText",
				TDCultureInfo.CurrentUICulture);

			commandCity.AlternateText = Global.tdResourceManager.GetString(
				"AmbiguousLocationSelectControl2.commandCity.AlternateText",
				TDCultureInfo.CurrentUICulture);

			commandAttractions.AlternateText = Global.tdResourceManager.GetString(
				"AmbiguousLocationSelectControl2.commandAttractions.AlternateText",
				TDCultureInfo.CurrentUICulture);

			commandMainStations.AlternateText = Global.tdResourceManager.GetString(
				"AmbiguousLocationSelectControl2.commandMainStations.AlternateText",
				TDCultureInfo.CurrentUICulture);

			commandAllStations.AlternateText = Global.tdResourceManager.GetString(
				"AmbiguousLocationSelectControl2.commandAllStations.AlternateText",
				TDCultureInfo.CurrentUICulture);

        }

        #endregion

        #region Control Population
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

            possibleOptionsText = Global.tdResourceManager.GetString(
                "AmbiguousLocationSelectControl2.labelLocationTitle",
                TDCultureInfo.CurrentUICulture);

            moreOptionsText = Global.tdResourceManager.GetString(
                "AmbiguousLocationSelectControl2.moreOptions",
                TDCultureInfo.CurrentUICulture);

            labelLocationTitle.Text = string.Format(possibleOptionsText,locationText);

            // Since we maybe repopulating the same list, store the existing selected option 
            int selectedIndex = listLocations.SelectedIndex;
            // Clear existing list
            listLocations.Items.Clear();

            // Then repopulate
            LocationChoiceList choices = thisSearch.GetCurrentChoices(thisSearch.CurrentLevel);
            PopulateDropDown(choices, listLocations, selectedIndex, thisSearch.SupportHierarchic);
            SearchTypeSelection = thisSearch.SearchType;

            // Display gazetteer radio buttons but only on certain pages
            if (PageId == PageId.FindTrainInput || PageId == PageId.FindCoachInput) 
            {
                radioButtonTable.Visible = false;
            } 
            else 
            {
                showSearchTypeRadioButtons();
                radioButtonTable.Visible = true;
            }

			// Make commandNewLocation visible if the user is able to specify another location
			commandNewLocation.Visible = !thisSearch.LocationFixed;    
        }

        /// <summary>
        /// populate the dropdownlist with the description and the index of the choice in the list
        /// </summary>
        /// <param name="choices">choices</param>
        /// <param name="list">list to populate</param>
        /// <param name="index">the option to set as selected</param>
        /// <param name="isHierarchic">true if the drop down contains drillable options</param>
        private void PopulateDropDown( LocationChoiceList choices, DropDownList list, int selectedIndex, bool isHierarchic)
        {
            int i= 0;
            foreach (LocationChoice choice in choices)
            {
                ListItem item;
                // For hierarchic searches add options for drillable locations
                if (isHierarchic) {
                    // If option is not an admin area, add option to select the location.
                    // A choice which is an admin area should only appear as an option to drilldown
                    // otherwise it can appear as both a location to select and a location
                    // to drilldown to
                    if (!choice.IsAdminArea) {
                        item = new ListItem(choice.Description, i.ToString(CultureInfo.InvariantCulture));
                        list.Items.Add(item);
                    }
					// If location has children, add option to drilldown
					if (choice.HasChilden) 
					{
						item = new ListItem(moreOptionsText + " " +choice.Description +"...", "+" + i.ToString(CultureInfo.InvariantCulture));
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

        #endregion

        #region Public properties
        /// <summary>
        /// Drop down list containing locations
        /// </summary>
        /// <returns>Drop down list containing locations</returns>
        public DropDownList LocationsDropDownList
        {
            get {
                return listLocations;
            }
        }

        /// <summary>
        /// Search type according to searchtype value
        /// </summary>
        private SearchType SearchTypeSelection
        {
            set
            {
                switch (value)
                {
                    case SearchType.AddressPostCode:
                        commandAddress.ImageUrl = imageSelected;
                        commandAllStations.ImageUrl = imageUnselected;
                        commandAttractions.ImageUrl = imageUnselected;
                        commandCity.ImageUrl = imageUnselected;
                        commandMainStations.ImageUrl = imageUnselected;
                        break;
                    case SearchType.Locality:
                        commandAddress.ImageUrl = imageUnselected;
                        commandAllStations.ImageUrl = imageUnselected;
                        commandAttractions.ImageUrl = imageUnselected;
                        commandCity.ImageUrl = imageSelected;
                        commandMainStations.ImageUrl = imageUnselected;
                        break;
                    case SearchType.MainStationAirport:
                        commandAddress.ImageUrl = imageUnselected;
                        commandAllStations.ImageUrl = imageUnselected;
                        commandAttractions.ImageUrl = imageUnselected;
                        commandCity.ImageUrl = imageUnselected;
                        commandMainStations.ImageUrl = imageSelected;
                        break;
                    case SearchType.AllStationStops:
                        commandAddress.ImageUrl = imageUnselected;
                        commandAllStations.ImageUrl = imageSelected;
                        commandAttractions.ImageUrl = imageUnselected;
                        commandCity.ImageUrl = imageUnselected;
                        commandMainStations.ImageUrl = imageUnselected;
                        break;
                    case SearchType.POI:
                        commandAddress.ImageUrl = imageUnselected;
                        commandAllStations.ImageUrl = imageUnselected;
                        commandAttractions.ImageUrl = imageSelected;
                        commandCity.ImageUrl = imageUnselected;
                        commandMainStations.ImageUrl = imageUnselected;
                        break;
                }
                
            }
        }

        #endregion

        #region Control member access
        
        #endregion

        #region Initialisation methods

        public void ResetSelectedIndex()
        {
            listLocations.SelectedIndex = -1;
        }

        public void SetMembers(ref LocationSearch search, ref TDLocation location)
        {
            this.thisSearch = search;
            this.thisLocation = location;
        }

        /// <summary>
        /// Populates radioButtonTable with the radio button images / labels that are
        /// appropriate for the location type
        /// </summary>
        private void showSearchTypeRadioButtons()
        {

            int cellCount = 0;
            int rowCount = 0;

            // for each search type in data services data set add (previously created)
            // image and label controls to current table cell and advance to next cell
            ArrayList types = populator.GetList(listType);
            foreach (DSDropItem type in types)
            {
                TableRow currentRow = radioButtonTable.Rows[rowCount];
                TableCell currentCell = currentRow.Cells[cellCount];
                switch ((SearchType)Enum.Parse(typeof( SearchType), type.ItemValue))
                {
                    case SearchType.AddressPostCode:
                        currentCell.Controls.Add(commandAddress);
                        currentCell.Controls.Add(labelAddress);
                        cellCount++;
                        break;
                    case SearchType.AllStationStops:
                        currentCell.Controls.Add(commandAllStations);
                        currentCell.Controls.Add(labelAllStations);
                        cellCount++;
                        break;
                    case SearchType.Locality:
                        currentCell.Controls.Add(commandCity);
                        currentCell.Controls.Add(labelCity);
                        cellCount++;
                        break;
                    case SearchType.MainStationAirport:
                        currentCell.Controls.Add(commandMainStations);
                        currentCell.Controls.Add(labelMainStations);
                        cellCount++;
                        break;
                    case SearchType.POI:
                        currentCell.Controls.Add(commandAttractions);
                        currentCell.Controls.Add(labelAttractions);
                        cellCount++;
                        break;
                }
                if (cellCount > 2) 
                {
                    cellCount = 0;
                    rowCount++;
                }
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

            imageSelected = Global.tdResourceManager.GetString(
                "JourneyPlanner.imageButtonBlueRadioButtonChecked",
                TDCultureInfo.CurrentUICulture);

            imageUnselected = Global.tdResourceManager.GetString(
                "JourneyPlanner.imageButtonBlueRadioButtonUnchecked",
                TDCultureInfo.CurrentUICulture);

            // Create radio button images and associated label controls here.
            // These controls are not defined in the ASP page since they need to be
            // added to the radioButtonTable dynamically at the time the page is loaded

            labelAddress = new Label();
            labelAllStations = new Label();
            labelAttractions = new Label();
            labelCity = new Label();
            labelMainStations = new Label();

            commandAddress = new ImageButton();
            commandAllStations = new ImageButton();
            commandMainStations = new ImageButton();
            commandCity = new ImageButton();
            commandAttractions = new ImageButton();

            commandAddress.Click += new System.Web.UI.ImageClickEventHandler(this.CommandAddressClick);
            commandCity.Click += new System.Web.UI.ImageClickEventHandler(this.CommandCityClick);
            commandAttractions.Click += new System.Web.UI.ImageClickEventHandler(this.CommandAttractionsClick);
            commandMainStations.Click += new System.Web.UI.ImageClickEventHandler(this.CommandMainStationsClick);
            commandAllStations.Click += new System.Web.UI.ImageClickEventHandler(this.CommandAllStationsClick);
			
        }
        
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.commandNewLocation.Click += new System.Web.UI.ImageClickEventHandler(this.CommandNewLocationClick);

        }
        #endregion

        #region Public methods
        public void RefineLocation(int level)
        {

            // Determine if the selected option was to drill down
            bool drillable = listLocations.SelectedItem.Value.StartsWith("+");

            // Get the selected choice; this will be the same choice whether
            // the selected option was for a location or for a drill down into 
            // the location (the "+" will be ignored by the indexer)
            LocationChoice choice = (LocationChoice)thisSearch.GetCurrentChoices(level)
                [Convert.ToInt32(listLocations.SelectedItem.Value, CultureInfo.CurrentCulture.NumberFormat)]; 

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
        #endregion
        
        #region Event handlers
        private void CommandAddressClick(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            // don't do anything if already selected
            if (thisSearch.SearchType != SearchType.AddressPostCode)
            {
                thisSearch.SearchType = SearchType.AddressPostCode;
                if (NewSearchType != null)
                    NewSearchType(sender, e);
            }
        }

        private void CommandCityClick(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            // don't do anything if already selected
            if (thisSearch.SearchType != SearchType.Locality)
            {
                thisSearch.SearchType = SearchType.Locality;
                if (NewSearchType != null)
                    NewSearchType(sender, e);
            }

        }

        private void CommandAttractionsClick(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            // don't do anything if already selected
            if (thisSearch.SearchType != SearchType.POI)
            {
                thisSearch.SearchType = SearchType.POI;
                if (NewSearchType != null)
                    NewSearchType(sender, e);
            }
        
        }

        private void CommandMainStationsClick(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            // don't do anything if already selected
            if(thisSearch.SearchType != SearchType.MainStationAirport)
            {
                thisSearch.SearchType = SearchType.MainStationAirport;
                if (NewSearchType != null)
                    NewSearchType(sender, e);
            }
        }

        private void CommandAllStationsClick(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            // don't do anything if already selected
            if(thisSearch.SearchType != SearchType.AllStationStops)
            {
                thisSearch.SearchType = SearchType.AllStationStops;
                if (NewSearchType != null)
                    NewSearchType(sender, e);
            }
        }

        private void CommandNewLocationClick(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (NewLocation != null)
                NewLocation(sender, e);

        }
        #endregion

        
    }

}
