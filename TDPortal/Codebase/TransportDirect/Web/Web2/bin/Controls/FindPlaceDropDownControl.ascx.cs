// *********************************************** 
// NAME                 : FindPlaceDropDownControl.ascx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 01/11/2004 
// DESCRIPTION  : Control displaying cities/places in a dropdown and returning location
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindPlaceDropDownControl.ascx.cs-arc  $ 
//
//   Rev 1.8   Jul 28 2011 16:19:02   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.7   Oct 27 2010 14:10:48   RBroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.6   Apr 23 2010 16:16:26   mmodi
//Added code to use a Scriptable drop down list and attach javascript to use it
//Resolution for 5521: TD Extra - Drop Down List Change - CCN0575
//
//   Rev 1.5   Feb 16 2010 14:49:12   mmodi
//Updated to get location from International gazetteer
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.4   Feb 11 2010 14:34:26   rbroddle
//Updated to use international Gaz
//
//   Rev 1.3   Feb 11 2010 08:53:34   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Mar 31 2008 13:20:46   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 05 2008 13:00:00   mmodi
//Added method to set the drop down list to the current location
//
//   Rev 1.0   Nov 08 2007 13:14:24   mturner
//Initial revision.
//
//   Rev 1.8   Feb 23 2006 19:16:40   build
//Automatically merged from branch for stream3129
//
//   Rev 1.7.1.0   Jan 10 2006 15:24:50   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.7   Feb 15 2005 16:14:42   tmollart
//Added ResetDropDownLocation method which will reset the place control item back to the first item in its list.
//
//   Rev 1.6   Jan 17 2005 16:51:10   tmollart
//Del 7 - Updated so that control will populate when PageId is FindFareInput.
//
//   Rev 1.5   Nov 18 2004 15:42:12   passuied
//Populate control whenever !Postback... Less restriction than before.			
//Resolution for 1748: City to city amend search - Selected From location is reverted back to default when new location is selected in To section or vice versa
//
//   Rev 1.4   Nov 03 2004 10:47:30   passuied
//Removed dummy text in search call after update of locationservice
//
//   Rev 1.3   Nov 02 2004 16:53:12   passuied
//Cleaning the code!
//
//   Rev 1.2   Nov 02 2004 16:14:04   passuied
//minor changes to fix display bugs
//
//   Rev 1.1   Nov 01 2004 18:04:52   passuied
//working version of the control


namespace TransportDirect.UserPortal.Web.Controls
{
    using System;
    using System.Collections;
    using System.Web.UI.WebControls;
    using TransportDirect.Common;
    using TransportDirect.Common.ServiceDiscovery;
    using TransportDirect.UserPortal.LocationService;
    using TransportDirect.UserPortal.SessionManager;
	
	/// <summary>
	///	Control displaying cities/places in a dropdown and returning location
	/// </summary>
	
	public partial  class FindPlaceDropDownControl : TDUserControl
	{
		#region Declaration

		private CurrentLocationType myLocationType = CurrentLocationType.From;
		private LocationSearch mySearch = null;
		private TDLocation myLocation = null;

        // Variables needed for using the scriptable dropdown
        private PageId scriptablePageId = PageId.Empty;
        private bool useScriptableList = false;
        private string targetControlName = string.Empty;
        private bool updateTargetControl = false;
        private bool useJavaScript = false;
        private ArrayList restrictLocations = new ArrayList();
        
		private static readonly string noValueKey = string.Empty;
		private const string listInstructionFromKey = "FindPlaceDropDownControl.listInstructionFrom";
		private const string listInstructionToKey = "FindPlaceDropDownControl.listInstructionTo";
		private const string listInstructionAmbiguousKey = "FindPlaceDropDownControl.listInstructionAmbiguous";
        private const string listInternationalInstructionAmbiguousKey = "FindPlaceDropDownControl.listInternationalInstructionAmbiguous";
        private const string listInternationalInstructionFromKey = "FindPlaceDropDownControl.listInternationalInstructionFrom";
        private const string listInternationalInstructionToKey = "FindPlaceDropDownControl.listInternationalInstructionTo";
	
		#endregion

		#region Public Properties
		/// <summary>
		/// Read-write property. Indicates what location type (from/to) the control 
		/// is associated to
		/// </summary>
		public CurrentLocationType LocationType
		{
			get
			{
				return myLocationType;
			}
			set
			{
				myLocationType = value;
			}

		}

		/// <summary>
		/// Read-Write property. The read property returns a new location retrieved from the new City or 
        /// International gazetteer.
		/// The Write property is for the control to be able to check the status of the current location
		/// </summary>
		public TDLocation Location
		{
			get
			{
				TDLocation location = new TDLocation();
				// Populate location with call to CityGazetteer GetLocationDetails
				
				// If nothing selected, don't do anything
                string selectedValue = noValueKey;

                if ((!useScriptableList) && listPlaces.SelectedItem.Value != noValueKey)
                {
                    selectedValue = listPlaces.SelectedItem.Value;
                }
                else if ((useScriptableList) && listPlacesScriptable.SelectedItem.Value != noValueKey)
                {
                    selectedValue = listPlacesScriptable.SelectedItem.Value;
                }

                if (selectedValue != noValueKey)
				{
                    // The gazetteer needs a LocationChoice object in param
                    // Build a fake one with default params.
                    // Use the selected item value for picklistvalue
                    LocationChoice choice = new LocationChoice(
                        string.Empty,
                        false,
                        string.Empty,
                        selectedValue,
                        new OSGridReference(),
                        string.Empty,
                        0,
                        string.Empty,
                        string.Empty,
                        false);
					
                    if (PageId == TransportDirect.Common.PageId.FindInternationalInput)
                    {
                        #region Get location for International gazetteer
                        // Security. If currentlevel <0, means search was reset and needs to be 
                        // restarted! Happens when New Location clicked...
                        if (mySearch.CurrentLevel < 0)
                        {
                            mySearch.StartSearch(string.Empty,
                                SearchType.International,
                                false,
                                0,
                                string.Empty,
                                false);
                        }

                        mySearch.GetLocationDetails(ref location, choice);

                        #endregion
                    }
                    else
                    {
                        #region Get location for City gazetteer

                        // Security. If currentlevel <0, means search was reset and needs to be 
                        // restarted! Happens when New Location clicked...
                        if (mySearch.CurrentLevel < 0)
                        {
                            mySearch.StartSearch(string.Empty,
                                SearchType.City,
                                false,
                                0,
                                string.Empty,
                                false);
                        }

                        mySearch.GetLocationDetails(ref location, choice);

                        #endregion
                    }
                    
					// Set LocationFixed = true so the search type is not
					// displayed when location is valid
					mySearch.LocationFixed = true;

				}
				return location;

			}
			set
			{
				myLocation = value;
			}
		}
		
		/// <summary>
		/// Read-Write property. LocationSearch object the control is associated with.
		/// </summary>
		public LocationSearch Search
		{
			get
			{
				return mySearch;
			}
			set
			{
				mySearch = value;
			}
		}

		/// <summary>
		/// Read-only property. Returns the appropriate instruction resource key
		/// to use according the current Location type
		/// </summary>
		private string InstructionKey
		{
			get
			{
				return (myLocationType == CurrentLocationType.From)? listInstructionFromKey : listInstructionToKey;
			}
        }

        #region Scriptable Dropdown properties

        /// <summary>
        /// Read/write. Indicates if the scriptable drop down list should be used
        /// <seealso cref="DropDownScriptablePageId"/>
        /// </summary>
        public bool UseScriptableList
        {
            get { return useScriptableList; }
            set { useScriptableList = value; }
        }

        /// <summary>
        /// Read/write. Whether or not to use JavaScript. The Get method ORs the specified value with 
        /// the boolean value representing whether or not JavaScript is enabled on the client.
        /// </summary>
        public bool UseJavaScript
        {
            get
            {
                //Javascript support needs to be true, because the jscript functionality is needed immediately.
                //Can't wait for the post-back.
                bool jsSupport = true;

                switch (scriptablePageId)
                {
                    case PageId.FindInternationalInput:
                        // Check if javascript file is available for this page
                        jsSupport = ((InternationalPlanner.InternationalPlannerData)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerDataFactory]).ScriptGenerated;
                        break;
                    default:
                        jsSupport = false;
                        break;
                }

                return useJavaScript && jsSupport;
            }
            set { useJavaScript = value; }
        }

        /// <summary>
        /// Read/write. Scriptable dropdown list PageId. Used to determine which javascript to attach to
        /// the scriptable dropdown list.
        /// This is only needed when using the Scriptable Dropdown control.
        /// <seealso cref="UpdateTargetControl"/>
        /// </summary>
        public PageId DropDownScriptablePageId
        {
            get { return scriptablePageId; }
            set { scriptablePageId = value; }
        }

        /// <summary>
        /// Allows specification of a control that will be treated as the "other end"
        /// of the route started or ended by this control. If UpdateTargetControl is set
        /// to true, the TargetControl will be updated with valid destinations whenever 
        /// the value in this control is changed.
        /// This is only needed when using the Scriptable Dropdown control.
        /// <seealso cref="UpdateTargetControl"/>
        /// </summary>
        public string TargetControlName
        {
            get { return targetControlName; }
            set { targetControlName = value; }
        }

        /// <summary>
        /// Whether or not to update the control specified by TargetControlName when this
        /// control changes.
        /// This is only needed when using the Scriptable Dropdown control (currently only
        /// set for PageId.FindInternationalInput automatically) 
        /// <seealso cref="TargetControlName"/>
        /// </summary>
        public bool UpdateTargetControl
        {
            get { return updateTargetControl; }
            set { updateTargetControl = value; }
        }

        /// <summary>
        /// Returns the full id of the dropdown list, as it will be written out in the
        /// client HTML.
        /// </summary>
        public string DropDownScriptableClientID
        {
            get { return listPlacesScriptable.ClientID; }
        }

        #endregion

        #endregion

        #region Private Event Handlers
        /// <summary>
		/// Event Handler for Page_Load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            // Populate the control (list especially) only if not a postback (otherwise losing selection),
			// AND if in trunkInput page (performance)
			if ((!IsPostBack) && (PageId == Common.PageId.FindTrunkInput || PageId == Common.PageId.FindFareInput))
			{
                labelInstructionDropDown.Text = GetResource(listInstructionAmbiguousKey);
				string instruction = GetResource(InstructionKey);
				
				// Populate list with Citygazetteer call to FindLocation
				// set Item value to TDPGID code
				LocationChoiceList choices =
					mySearch.StartSearch(string.Empty, 
					SearchType.City,
					false,
					0,
					string.Empty,
					false);

                // Ignore any scriptable settings and just use the normal dropdown list. This can be changed 
                // in the future is needed.
                useScriptableList = false;

				// Each item in list is populated with
				// description as text
				// picklist value as value
                listPlaces.Items.Add(new ListItem(instruction, noValueKey));
                foreach (LocationChoice choice in choices)
				{
					listPlaces.Items.Add(new ListItem(choice.Description, choice.PicklistValue));
				}
			}
            else if ((!IsPostBack) && (PageId == TransportDirect.Common.PageId.FindInternationalInput))
            {
                PopulateInternationalCityDropdown(useScriptableList);
            }
		}

		/// <summary>
		/// Event Handler for Page_PreRender event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			// set the style of control when in ambiguity mode or not
			if (TDSessionManager.Current.FindPageState.AmbiguityMode)
			{
				divDropDown.Attributes.Clear();
				divDropDown.Attributes.Add("class", "FindPlace_Dropdown_area");
				labelInstructionDropDown.Visible = true;
			}
			else
			{
				divDropDown.Attributes.Clear();
				divDropDown.Attributes.Add("class", "");

                if (PageId == Common.PageId.FindTrunkInput)
                {
                    labelInstructionDropDown.Visible = true;
                    labelInstructionDropDown.CssClass = "screenreader";
                }
                else
                {
                    labelInstructionDropDown.Visible = false;
                }
			}

            // Ensure correct dropdown list is displayed
            SetDropdownListVisible();

            // Set the javascript if using the scriptable dropdown lsit
            if (useScriptableList)
            {
                SetupDropdownListJavascript();

                RestrictDropdownListLocations();
            }
		}
		#endregion

        #region Private Methods

        /// <summary>
        /// Method which populates the Places dropdown list with International Cities
        /// </summary>
        /// <returns></returns>
        private void PopulateInternationalCityDropdown(bool useScriptableList)
        {
            labelInstructionDropDown.Text = GetResource(listInternationalInstructionAmbiguousKey);
            string instruction = GetResource((myLocationType == CurrentLocationType.From) ? listInternationalInstructionFromKey : listInternationalInstructionToKey);

            // Populate list with Internationalgazetteer call to FindLocation
            // set Item value to TDPGID code
            LocationChoiceList choices = mySearch.StartSearch(string.Empty,
                    SearchType.International,
                    false,
                    0,
                    string.Empty,
                    false);

            if (!useScriptableList)
            {
                #region Populate normal dropdown list

                listPlaces.Items.Clear();

                listPlaces.Items.Add(new ListItem(instruction, noValueKey));

                // Each item in list is populated with
                // description as text
                // picklist value as value
                foreach (LocationChoice choice in choices)
                {
                    listPlaces.Items.Add(new ListItem(choice.Description, choice.PicklistValue));
                }

                #endregion
            }
            else
            {
                #region Populate scriptable dropdown list

                listPlacesScriptable.Items.Clear();

                // Populate the scriptable list
                listPlacesScriptable.Items.Add(new ListItem(instruction, noValueKey));

                foreach (LocationChoice choice in choices)
                {
                    listPlacesScriptable.Items.Add(new ListItem(choice.Description, choice.PicklistValue));
                }

                #endregion
            }
        }

        /// <summary>
        /// Determines if the normal dropdown list or the scriptable dropdown list should be used
        /// </summary>
        private void SetDropdownListVisible()
        {
            if (useScriptableList)
            {
                // Use the scriptable dropdown list
                listPlaces.Visible = false;
                listPlacesScriptable.Visible = true;
            }
            else
            {
                // Use the normal dropdown list
                listPlaces.Visible = true;
                listPlacesScriptable.Visible = false;
            }

            // Default is to not attach javascript, this is updated if we can attach the script 
            // in SetupDropdownListJavascript() called later
            listPlacesScriptable.EnableClientScript = false;
        }

        /// <summary>
        /// Method to add the javascript to the scriptable drop down list
        /// </summary>
        private void SetupDropdownListJavascript()
        {
            // Ensure javascript is registered and can be used
            if (UseJavaScript)
            {
                string dropdownScriptName = string.Empty;
                string dropdownAction = string.Empty;
                string dropdownDataScriptName = string.Empty;

                // Set up the javascript to attach
                switch (scriptablePageId)
                {
                    case PageId.FindInternationalInput:
                        dropdownScriptName = "InternationalCitySelection";
                        dropdownDataScriptName = InternationalPlanner.InternationalPlannerData.ScriptName;

                        // The action to fire when user changes selection on dropdown list
                        dropdownAction = string.Format(
                            "handleInternationalCitySelectionChanged('{0}', '{1}', {2});",
                            listPlacesScriptable.ClientID,
                            targetControlName,
                            updateTargetControl.ToString().ToLower());

                        break;
                }

                if ((!string.IsNullOrEmpty(dropdownScriptName)) && (!string.IsNullOrEmpty(dropdownDataScriptName)))
                {
                    listPlacesScriptable.EnableClientScript = UseJavaScript;

                    // Attach javascript to the dropdown list
                    listPlacesScriptable.ScriptName = dropdownScriptName;
                    listPlacesScriptable.Action = dropdownAction;

                    // Checking for Javascript support
                    string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];
                    ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

                    Page.ClientScript.RegisterStartupScript(typeof(FindPlaceDropDownControl), dropdownDataScriptName, scriptRepository.GetScript(dropdownDataScriptName, javaScriptDom));
                    Page.ClientScript.RegisterStartupScript(typeof(FindPlaceDropDownControl), listPlacesScriptable.ScriptName, scriptRepository.GetScript(listPlacesScriptable.ScriptName, javaScriptDom));
                }
            }
        }

        /// <summary>
        /// Method which removes any locations from the scriptable dropdown list 
        /// not contained in the restrict locations array
        /// </summary>
        private void RestrictDropdownListLocations()
        {
            if ((restrictLocations != null) && (restrictLocations.Count > 0))
            {
                // Reset the drop down list, so it has all the possible locations before restricting
                PopulateInternationalCityDropdown(useScriptableList);

                ListItem li;
                
                // Remove specified locations from the list
                int listIndex = listPlacesScriptable.Items.Count - 1;
                for (; listIndex > 0; listIndex--)
                {
                    li = listPlacesScriptable.Items[listIndex];
                    
                    if (!string.IsNullOrEmpty(li.Value) && li.Value != noValueKey)
                    {
                        if (!restrictLocations.Contains(li.Value))
                        {
                            listPlacesScriptable.Items.Remove(li);
                        }
                    }
                }
            }
        }

        #endregion

        #region Public Methods
        /// <summary>
		/// Resets the location drop down so that the first list item is displayed.
		/// This can be used to reset the current selected location to the initial
		/// help text displayed by the control.
		/// </summary>
		public void ResetLocationDropDown()
		{
			if (listPlaces.Items.Count > 0)
			{
				listPlaces.SelectedIndex = 0;
			}

            if (listPlacesScriptable.Items.Count > 0)
            {
                listPlacesScriptable.SelectedIndex = 0;
            }
		}

        /// <summary>
        /// Sets the list drop down so that the selected item is the current location.
        /// </summary>
        public void SetLocationDropDown()
        {
            if (listPlaces.Items.Count > 0)
            {
                // default to first item
                listPlaces.SelectedIndex = 0;

                if ((myLocation != null) && (!string.IsNullOrEmpty(myLocation.Description)))
                {
                    // Find the item using the location Description value and select it in the list
                    ListItem item = listPlaces.Items.FindByText(myLocation.Description);

                    if (item != null)
                        listPlaces.SelectedValue = item.Value;
                }
            }

            if (listPlacesScriptable.Items.Count > 0)
            {
                // default to first item
                listPlacesScriptable.SelectedIndex = 0;

                if ((myLocation != null) && (!string.IsNullOrEmpty(myLocation.Description)))
                {
                    // Find the item using the location Description value and select it in the list
                    ListItem item = listPlacesScriptable.Items.FindByText(myLocation.Description);

                    if (item != null)
                        listPlacesScriptable.SelectedValue = item.Value;
                }
            }
        }

        /// <summary>
        /// Applies a restriction to the data shown in the list. Only locations contained
        /// in the parameter are retained. Or if null/empty then all locations in the list are retained
        /// </summary>
        /// <param name="locations">The string id of the locations</param>
        public void Restrict(ArrayList locations)
        {
            if (locations == null)
            {
                locations = new ArrayList();
            }

            restrictLocations = locations;
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
