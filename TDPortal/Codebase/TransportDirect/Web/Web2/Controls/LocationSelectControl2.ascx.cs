// *********************************************** 
// NAME                 : LocationSelectControl2.ascx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 02/12/2003 
// DESCRIPTION  : new version of LocationSelect Control. Enables User to select a location using the gazetteer
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/LocationSelectControl2.ascx.cs-arc  $ 
//
//   Rev 1.13   Jun 18 2010 14:49:48   apatel
//Updated to handle the invalid Naptans from Auto-Complete DropDownGaz
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.12   Jun 16 2010 15:32:24   apatel
//Updated parameters passed to init autosuggest functionality
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.11   Jun 16 2010 10:21:10   apatel
//Updated to implement auto-suggest functionality
//
//   Rev 1.10   Apr 12 2010 16:58:26   pghumra
//Changed to avoid page chrashing when clear page and amend buttons are clicked
//Resolution for 5504: CODE FIX - NEW - Del 10.x - Page crash when using modify in flight
//
//   Rev 1.9   Dec 10 2009 15:52:50   mmodi
//Updated code to hide Find on map button when javascript is disabled
//
//   Rev 1.8   Nov 28 2009 11:26:48   apatel
//Travel News map enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.7   Nov 18 2009 16:22:32   apatel
//Updated to hide findonmap page when javascript is disabled
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.6   Oct 17 2008 11:55:24   build
//Automatically merged from branch for stream0093
//
//   Rev 1.5.1.0   Oct 09 2008 11:48:00   jfrank
//New message for address postcode gazetteer search failure
//Resolution for 5139: 10.4 - PO Box Postcode handling
//
//   Rev 1.5   May 19 2008 15:47:28   mmodi
//Exposed new location not
//Resolution for 4988: Del 10.1: Text displayed above location input box after amend journey
//
//   Rev 1.4   May 02 2008 14:32:36   mmodi
//Formatting improved.
//Resolution for 4929: Control alignments: Find car
//
//   Rev 1.3   May 01 2008 16:24:16   mmodi
//No change.
//Resolution for 4922: Control alignments: Door-to-door planner
//
//   Rev 1.2   Mar 31 2008 13:21:50   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:06   mturner
//Initial revision.
//
//   Rev 1.31   Nov 14 2006 10:08:34   rbroddle
//Merge for stream4220
//
//   Rev 1.30.1.0   Nov 07 2006 11:41:58   tmollart
//Updated for Rail Search By Price.
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.30   May 03 2006 10:29:30   asinclair
//Added ResetCar method 
//Resolution for 3962: DN068: Extend: Ambiguiy / Error messages on Extend Input page
//
//   Rev 1.29   Feb 23 2006 16:12:32   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.28   Dec 08 2005 14:42:08   rhopkins
//Apply red box to text field rather than cell, so that it does not become enlarged when the adjoining text wraps onto two lines.
//Resolution for 3256: UEE: Win98/IE5 - red error border on Ambiguity page is larger than From and To fields
//
//   Rev 1.27   Nov 03 2005 17:59:38   NMoorhouse
//Manual Merge of stream2816
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.26   Nov 01 2005 09:32:28   tolomolaiye
//Merge for stream 2638 (Visit Planner)
//Resolution for 2929: Visit Planner Merge Activity
//
//   Rev 1.24.2.3   Oct 20 2005 10:30:38   asinclair
//Updated to remove class attribute before adding it
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.24.2.2   Oct 13 2005 17:17:08   jbroome
//Fixed bug with null parameters
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.24.2.1.1.4   Oct 28 2005 10:34:54   mtillett
//Set the screenreader to visibility to match the associated control visibility
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.24.2.1.1.3   Oct 26 2005 15:03:16   rgreenwood
//Removed extreaneous code following Image Button replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.24.2.1.1.2   Oct 25 2005 19:40:00   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.24.2.1.1.1   Oct 13 2005 17:09:32   RGriffith
//Fixed the Search method to account for JourneyParameters being null.
//
//   Rev 1.24.2.1.1.0   Oct 07 2005 09:44:22   mtillett
//Update layout for UEE changes
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.24.2.1   Oct 05 2005 09:44:24   tolomolaiye
//Updates following code review and fxcop
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.24.2.0   Sep 29 2005 14:52:08   tolomolaiye
//Modified the populate method
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.25   Sep 29 2005 14:22:56   asinclair
//Merge for stream2673
//
//   Rev 1.24.1.1   Sep 09 2005 14:11:02   Schand
//DN079 UEE Enter Key.
//TakeDefaultAction modified to accept additional parameter.
//Resolution for 2756: DEL 8 Stream: Add default key functionality on Input pages to Next button
//
//   Rev 1.24.1.0   Sep 05 2005 18:02:00   Schand
//Added AddDefaultAction() for the textbox.
//Resolution for 2756: DEL 8 Stream: Add default key functionality on Input pages to Next button
//
//   Rev 1.24   Apr 13 2005 09:10:44   rscott
//DEL 7 Additional Tasks - IR1978 enhancements added - reject single word address.
//
//   Rev 1.23   Apr 07 2005 16:24:12   rscott
//Updated with DEL7 additional task outlined in IR1977.
//
//   Rev 1.22   Sep 01 2004 14:18:48   jmorrissey
//Removed SetDefaultListLocationTypeIndex as not needed since change to FindTrunkInputAdaptor
//
//   Rev 1.21   Aug 27 2004 17:02:48   RPhilpott
//Correct interaction between control hierarchy and LocationSearchHelper.
//Resolution for 1329: Both train and coach stops can be selected on a Find A Train or Find A Coach page
//
//   Rev 1.20   Aug 26 2004 16:51:42   COwczarek
//If on Find Train or Find Coach page, display different tip if no locations could be found for given input
//Resolution for 1421: Find a ambiguity pages (QA)
//
//   Rev 1.19   Aug 25 2004 09:38:38   RPhilpott
//Add StationType.Undetermined to LocationSearchHelper call.
//Resolution for 1329: Both train and coach stops can be selected on a Find A Train or Find A Coach page
//
//   Rev 1.18   Aug 24 2004 11:57:44   jmorrissey
//Fix for IR1327. Added method SetDefaultListLocationTypeIndex called in PageLoad to set the correct default for selected location type
//
//   Rev 1.17   Jul 22 2004 16:13:28   COwczarek
//Don't display gazetteer radio buttons on Find A Train or Coach input pages.
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.16   Jul 14 2004 12:03:56   jmorrissey
//Added overload of populate method that takes an additional parameter of type StationType.
//
//   Rev 1.15   May 26 2004 09:19:22   jgeorge
//Update for TDJourneyParameters changes
//
//   Rev 1.14   May 11 2004 14:18:22   passuied
//Change. When Controltype is default, reset visibility to false to all the components
//
//   Rev 1.13   Apr 27 2004 13:54:22   jbroome
//DEL 5.4 Merge. 
//Partial Postcode searching
//
//   Rev 1.12   Apr 14 2004 14:03:28   AWindley
//DEL5.2 QA Alt text: Following Rev 1.11, now uses Populate method to determine which Alt text to display for the Map button.
//
//   Rev 1.11   Apr 08 2004 14:35:38   COwczarek
//Populate method now accepts data services data set to use
//when generating radio buttons and location type (e.g. orgin,
//destination, via) to distinguish which prompt text to display.
//Resolution for 695: DEL 5.2 QA Changes - Ambiguity page
//
//   Rev 1.10   Mar 18 2004 15:26:00   AWindley
//DEL5.2 Modifications to label text following Resolution 649 changes
//
//   Rev 1.9   Mar 16 2004 18:13:34   COwczarek
//Location title (to, from, etc) text no longer part of control
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.8   Mar 12 2004 16:23:24   COwczarek
//SearchType property returns enum Map if no radio button selected. If SearchType property is set to Map, no radio button is set.
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.7   Mar 11 2004 11:57:56   AWindley
//DEL5.2 Added label text for Screen Reader
//
//   Rev 1.6   Mar 03 2004 15:39:42   COwczarek
//Map/OK button now only functions as Map button. OK button 
//functionality no longer responsibility of this control
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.5   Jan 06 2004 12:22:24   passuied
//Added safety check to avoid exception thrown in case of abnormal use of the planner
//Resolution for 575: NullReferenceException
//
//   Rev 1.4   Dec 18 2003 12:24:50   JHaydock
//Formatting changes for DEL 5.1
//
//   Rev 1.3   Dec 13 2003 14:05:32   passuied
//fix for location controls
//
//   Rev 1.2   Dec 12 2003 14:36:34   kcheung
//Journey Planner Location Map 5.1 Updates
//
//   Rev 1.1   Dec 04 2003 13:09:02   passuied
//final version for del 5.1
//
//   Rev 1.0   Dec 02 2003 16:17:26   passuied
//Initial Revision


using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.LocationService.DropDownLocationProvider;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Web.Support;

using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;





namespace TransportDirect.UserPortal.Web.Controls
{
    

    /// <summary>
    ///     new version of LocationSelect Control. Enables User to select a location using the gazetteer
    /// </summary>
    
    public partial  class LocationSelectControl2 : TDUserControl
    {
        #region Declarations

        private CurrentLocationType locationType;
        private DataServiceType listType;
        private DataServices.DataServices populator;
        private LocationSearch search = null;
        private TDLocation location = null;

        private string autoSuggestFilterControlIds = string.Empty;
        private bool autoSuggestGroupStations = true;

        // literal mechanism to show/hide extra column
        protected System.Web.UI.WebControls.Literal literalTd1Start;
		protected System.Web.UI.WebControls.Literal literalTd1Stop;


        public event EventHandler LocationEntered;

        #endregion

        #region Contructor, Page_Load & OnPreRender
        /// <summary>
        /// Constructor
        /// </summary>
        protected LocationSelectControl2()
        {
            populator = (DataServices.DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if ((true == true))
            {
                checkUnsureSpelling.Text = Global.tdResourceManager.GetString(
                    "LocationSelectControl.checkUnsureSpelling",
                    TDCultureInfo.CurrentUICulture);
            }

			// Adding script to the textbox
			AddDefaultAction();
        }

        /// <summary>
        /// Page pre render event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (commandMap.Visible)
            {
                // Map button is hidden by default using style, if javascript is enabled, then use 
                // javascript to the show the button - otherwise as AJAX maps do not work without 
                // javascript, then ok to keep it hidden.
                JavaScriptAdapter.InitialiseControlVisibility(commandMap, true);
            }

            TDPage page = this.Page as TDPage;

            if (page.IsJavascriptEnabled)
            {
                if (PageId == PageId.FindTrainInput || PageId == PageId.FindTrainCostInput)
                {
                    if (Visible && location != null)
                    {
                        RegisterAutoSuggestDropdowonJavascript();
                    }
                }
            }
        }


        #endregion

        #region Get/Set Control Selection Properties
        
        /// <summary>
        /// Gets/Sets if search is fuzzy
        /// </summary>
        public bool Fuzzy   
        {
            get
            {
                return checkUnsureSpelling.Checked;
            }
            set
            {
                checkUnsureSpelling.Checked = value;
            }
        }

        /// <summary>
        /// Gets/Sets input text
        /// </summary>
        public string InputText
        {
            get
            {
                return HttpUtility.HtmlDecode( textLocation.Text);
            }
            set
            {
                textLocation.Text = HttpUtility.HtmlEncode(value);
            }
        }

        /// <summary>
        /// Gets/Sets SearchType
        /// </summary>
        public SearchType SearchType
        {
            get
            {
                if (PageId == PageId.FindTrainInput || PageId == PageId.FindTrainCostInput || PageId == PageId.FindCoachInput) 
				{
                    return SearchType.MainStationAirport;
                } 
				else if (listLocationType.SelectedIndex == -1) 
				{
                    return SearchType.Map;
                } 
				else 
				{
                    string value = populator.GetValue(listType, listLocationType.SelectedItem.Value);
                    return (SearchType)Enum.Parse(typeof(SearchType), value, true);
                }
            }

            set
            {
                if (PageId == PageId.FindTrainInput || PageId == PageId.FindCoachInput || value == SearchType.Map) 
				{
                    listLocationType.SelectedIndex = -1;
                } 
				else 
				{
                    string searchType = Enum.GetName(typeof(SearchType), value);
                    string resourceId = populator.GetResourceId(listType, searchType);

                    for (int i=0; i< listLocationType.Items.Count ; i++) 
					{
                        ListItem item = listLocationType.Items[i];
                        if (resourceId == item.Value)
						{
                            listLocationType.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        
        }
		#endregion

        #region Properties giving access to the controls
        /// <summary>
        /// Read-only property returning the listLocationType control
        /// </summary>
        public RadioButtonList ControlLocationType
        {
            get
            {
                return listLocationType;
            }
        }
        /// <summary>
        /// Read-only property returning the textLocation control
        /// </summary>
        public TextBox ControlLocation
        {
            get
            {
                return textLocation;
            }
        }

        public TDButton ControlMap
        {
            get
            {
                return commandMap;
            }
        }
		
        /// <summary>
		/// Gets/Sets the labelSRLocation control
		/// </summary>
		public Label TypeInstruction
		{
			get
			{
				return labelSRLocation;
			}
			set
			{
				labelSRLocation = value;
			}
		}
		
        /// <summary>
		/// Gets/Sets the labelSRSelect control
		/// </summary>
		public Label SelectInstruction
		{
			get
			{
				return labelSRSelect;
			}
			set
			{
				labelSRSelect = value;
			}
		}

        /// <summary>
        /// Gets/Sets the visibility of the new location note
        /// </summary>
        public bool LabelNewLocationNoteVisible
        {
            get
            {
                return labelNewLocationNote.Visible;

            }
            set
            {
                labelNewLocationNote.Visible = value;
            }
        }

        
		#endregion

        #region Auto-Suggest properties
        /// <summary>
        /// Write only property allowing to set filter controls for auto-suggest 
        /// Setting the filter control Id enables auto-suggest functionality not to show entries 
        /// in dropdown which matches to the filter control text
        /// <remarks> The control Id should be the client Id of the filter control</remarks>
        /// </summary>
        public string AutoSuggestFilterControlIds
        {
            set { autoSuggestFilterControlIds = value; }
        }

        /// <summary>
        /// Write only boolean property if true allows group stations to be visible in auto-suggest list.
        /// </summary>
        public bool AutoSuggestGroupStations
        {
            set { autoSuggestGroupStations = value; }
        }

        #endregion

        #region Control Population

        /// <summary>
        /// Populate the control with a LocationSearch object
        /// </summary>
        /// <param name="listType">The data service set to use in creating radio buttons that select search type</param>
        /// <param name="locationType">The type of location this control is being used for, e.g. origin, destination, via</param>
        /// <param name="search">LocationSearch</param>
        /// <param name="location">location associated with control</param>
        /// <param name="disableMapSelection">true if map cannot be selected for associated location</param>
        public void Populate(DataServiceType listType, CurrentLocationType locationType, ref LocationSearch search, 
			ref TDLocation location, bool disableMapSelection) 
        {

            SetMembers(ref search, ref location);
            this.listType = listType;
            this.locationType = locationType;

            SearchType = search.SearchType;
            textLocation.Text = search.InputText;
            checkUnsureSpelling.Checked = search.FuzzySearch;

            // If javascript is disabled, then do not show map button (AJAX maps do not work when javascript is disabled)
            TDPage page = (TDPage)this.Page;
            commandMap.Visible = !disableMapSelection && page.IsJavascriptEnabled;
            commandMap.Text = resourceManager.GetString("LocationSelectControl2.commandMap.Text", TDCultureInfo.CurrentUICulture);

            listLocationType.Visible = !(PageId == PageId.FindTrainInput || PageId == PageId.FindTrainCostInput || PageId == PageId.FindCoachInput);
			labelSRSelect.Visible = listLocationType.Visible;

            switch (locationType) 
            {
                case CurrentLocationType.From:
                    labelNewLocationNote.Text = Global.tdResourceManager.GetString(
                        "LocationSelectControl2.labelNewLocationNoteFrom",
                        TDCultureInfo.CurrentUICulture);
					break;
                case CurrentLocationType.To:
                    labelNewLocationNote.Text = Global.tdResourceManager.GetString(
                        "LocationSelectControl2.labelNewLocationNoteTo",
                        TDCultureInfo.CurrentUICulture);
					break;
                case CurrentLocationType.PrivateVia:
                case CurrentLocationType.PublicVia:
                    labelNewLocationNote.Text = Global.tdResourceManager.GetString(
                        "LocationSelectControl2.labelNewLocationNoteVia",
                        TDCultureInfo.CurrentUICulture);
					break;
                case CurrentLocationType.None:
                    labelNewLocationNote.Text = Global.tdResourceManager.GetString(
                        "LocationSelectControl2.labelNewLocationNoteNone",
                        TDCultureInfo.CurrentUICulture);
                    break;

				case CurrentLocationType.VisitPlannerOrigin:
					labelNewLocationNote.Text = GetResource("LocationSelectControl2.labelNewLocationNoteVisitPlannerOrigin");
					break;

				case CurrentLocationType.VisitPlannerVisitPlace1:
					labelNewLocationNote.Text = GetResource("LocationSelectControl2.labelNewLocationNoteVisitPlannerVisitPlace1");
					break;

				case CurrentLocationType.VisitPlannerVisitPlace2:
					labelNewLocationNote.Text = GetResource("LocationSelectControl2.labelNewLocationNoteVisitPlannerVisitPlace2");
					break;
            }
        }

        /// <summary>
        /// Overloaded method. Populate the control with a LocationSearch object. Adds a stationType parameter.
        /// </summary>
        /// <param name="listType">The data service set to use in creating radio buttons that select search type</param>
        /// <param name="locationType">The type of location this control is being used for, e.g. origin, destination, via</param>
        /// <param name="search">LocationSearch</param>
        /// <param name="location">location associated with control</param>
        /// <param name="type"></param>
        /// <param name="disableMapSelection">true if map cannot be selected for associated location</param>
		/// <param name="stationType">The type of station this control is being used for, e.g. airport, rail, coach</param>
        public void Populate(DataServiceType listType, CurrentLocationType locationType, ref LocationSearch search,  
			ref TDLocation location, ControlType type, bool disableMapSelection, StationType stationType)
        {
            Populate(listType, locationType, ref search, ref location, disableMapSelection);

			//Check if the search is a nomatch and vague and display the appropriate message.
			bool vagueSearch = search.VagueSearch;
			bool singleWord = search.SingleWord;
			string strLabelNoMatchForStation = string.Empty;
			string strLabelNoMatchForLocation = string.Empty;
			string strLabelNoMatchForStationTip = string.Empty;
			string strLabelNoMatchForLocationTip = string.Empty;
            string strLabelNoMatchForAddressPostcode = string.Empty;

			if (vagueSearch)
			{
				strLabelNoMatchForStation = "LocationSelectControl2.labelVagueForStation";
				strLabelNoMatchForLocation = "LocationSelectControl2.labelVagueForLocation";
				strLabelNoMatchForStationTip = "LocationSelectControl2.labelVagueForStationTip";
				strLabelNoMatchForLocationTip = "LocationSelectControl2.labelVagueForLocationTip";
			}
			else if (singleWord)
			{
				strLabelNoMatchForStation = "LocationSelectControl2.labelSingleWordForStation";
				strLabelNoMatchForLocation = "LocationSelectControl2.labelSingleWordForLocation";
				strLabelNoMatchForStationTip = "LocationSelectControl2.labelSingleWordForStationTip";
				strLabelNoMatchForLocationTip = "LocationSelectControl2.labelSingleWordForLocationTip";			
			}
			else
			{
				strLabelNoMatchForStation = "LocationSelectControl2.labelNoMatchForStation";
				strLabelNoMatchForLocation = "LocationSelectControl2.labelNoMatchForLocation";
				strLabelNoMatchForStationTip = "LocationSelectControl2.labelNoMatchForStationTip";
				strLabelNoMatchForLocationTip = "LocationSelectControl2.labelNoMatchForLocationTip";
                strLabelNoMatchForAddressPostcode = "LocationSelectControl2.labelNoMatchForAddressPostcode";
			}

			//remove class attribute
			textLocation.Attributes.Remove("class");

			switch (type)
            {
                case ControlType.NoMatch:
                {

                    panelNoMatch.Visible = true;
                    panelNewLocation.Visible = false;
					//set the border of the table cell to red using a style sheet class
					textLocation.Attributes.Add("class", "alertboxerror");

                    if (PageId == PageId.FindTrainInput || PageId == PageId.FindTrainCostInput || PageId == PageId.FindCoachInput) 
                    {
                        labelNoMatchNote1.Text = String.Format(
                            GetResource(strLabelNoMatchForStation),
                            search.InputText);
						
						labelNoMatchNote2.Text = GetResource(strLabelNoMatchForStationTip);
                    } 
                    else 
                    {
                        string textSearchType = populator.GetText(
                            listType, 
                            Enum.GetName(typeof(SearchType), search.SearchType));

                        if (search.SearchType == SearchType.AddressPostCode && strLabelNoMatchForAddressPostcode != string.Empty)
                        {
                            labelNoMatchNote1.Text = String.Format(
                                GetResource(strLabelNoMatchForAddressPostcode),
                                search.InputText,
                                textSearchType);
                        }
                        else
                        {

                            labelNoMatchNote1.Text = String.Format(
                                GetResource(strLabelNoMatchForLocation),
                                search.InputText,
                                textSearchType);
                        }
						
						labelNoMatchNote2.Text = GetResource(strLabelNoMatchForLocationTip);
						
                    }
                    break;
                }
                case ControlType.NewLocation:
                    panelNewLocation.Visible = true;
                    panelNoMatch.Visible = false;
                    break;
                case ControlType.Default:
					panelNewLocation.Visible = false;
					panelNoMatch.Visible = false;
                    break;
            }
            
        }

        #endregion

        /// <summary>
        /// Reset the control with blank info
        /// </summary>
        public void Reset()
        {
            InputText = string.Empty;
            if (listLocationType.Items.Count > 0)
            {
                listLocationType.SelectedIndex = 0;
            }
            Fuzzy = false;
        }

		/// <summary>
		/// Reset the Car Via locations control with blank info
		/// </summary>
		public void ResetCar()
		{
			InputText = string.Empty;
			listLocationType.SelectedIndex = 1;
			Fuzzy = false;
		}
        
        #region Initialisation Methods
        /// <summary>
        /// Update the search and location objects with control values
        /// </summary>
        /// <param name="search">LocationSearch object</param>
        /// <param name="location">TDLocation object</param>
        public void UpdateLocationSearch( ref LocationSearch search, ref TDLocation location)
        {

            SetMembers(ref search, ref location);

            // if inputs have changed in the search, clear all the search
            // Then update the search
            if (search.SearchType != SearchType
                || search.InputText != InputText
                || search.FuzzySearch != Fuzzy
                )
            {
                search.ClearAll();
                location.Status = TDLocationStatus.Unspecified; // location has to be back to unspecified!!!!
                search.SearchType = SearchType;
                search.InputText =  InputText;
                search.FuzzySearch = Fuzzy;
            }

            if (!string.IsNullOrEmpty(hdnLocationId.Value) && !string.IsNullOrEmpty(textLocation.Text))
            {
                search.InputText = location.Description = InputText;
                try
                {
                    this.location.NaPTANs = PopulateNaptans(hdnLocationId.Value.Split(new char[] { ',' }));
                }
                catch 
                {
                    // Unable to populate the Naptans log the information and reset the location and serch
                    string message = string.Format("PopulateNaptans method failed to populate naptans for {0}",hdnLocationId.Value);
                    Logger.Write(new OperationalEvent(TDEventCategory.Business,TDTraceLevel.Verbose,message));
                    search.InputText = location.Description = string.Empty;
                    this.location.NaPTANs = new TDNaptan[0];
                }

                if (this.location.NaPTANs.Length > 0)
                {
                    OSGridReference osgr = this.location.NaPTANs[0].GridReference;
                    this.location.Locality = PopulateLocality(osgr);
                    this.location.GridReference = osgr;
                    this.location.SearchType = SearchType.MainStationAirport;
                    this.location.RequestPlaceType = RequestPlaceType.NaPTAN;
                    search.SearchType = SearchType.MainStationAirport;
                    this.location.Status = TDLocationStatus.Valid;
                }
            }
        }

        /// <summary>
        /// Initialisation method
        /// </summary>
        /// <param name="search">Search associated with control</param>
        /// <param name="location">location associated with control</param>
        public void SetMembers(ref LocationSearch search, ref TDLocation location)
        {
            this.search = search;
            this.location = location;   
        }

        

        #endregion

        #region Public methods
		/// <summary>
		/// Search method
		/// </summary>
		public void Search(bool acceptPostcodes, StationType stationType)
		{
			Search(acceptPostcodes, false, stationType);
		}

		/// <summary>
        /// Search method
        /// </summary>
		public void Search(bool acceptPostcodes, bool acceptPartPostcodes, StationType stationType)
		{
        
            TDJourneyParameters journeyParameters = TDSessionManager.Current.JourneyParameters;

            // For safety reason, if objects are null (abnormal use of site e.g. browser back button). Do nothing
            if (search==null || location== null)
                return;

			if ((journeyParameters is TDJourneyParametersFlight) || (journeyParameters==null))
			{
				LocationSearchHelper.SetupLocationParameters(
					search.InputText,
					search.SearchType,
					search.FuzzySearch,
					0,
					0,
					0,
					ref search,
					ref location,// check that it is passed as reference
					acceptPostcodes,
					acceptPartPostcodes,
					stationType
					);
			}
			else
			{
				LocationSearchHelper.SetupLocationParameters(
					search.InputText,
					search.SearchType,
					search.FuzzySearch,
					0,
					journeyParameters.MaxWalkingTime,
					journeyParameters.WalkingSpeed,
					ref search,
					ref location,// check that it is passed as reference
					acceptPostcodes,
					acceptPartPostcodes,
					stationType
					);
			}
        }

        #endregion

		#region Private Methods
		/// <summary>
		/// Add client side event and its handler to the given text box.
		/// </summary>
		private void AddDefaultAction()
		{
			UserExperienceEnhancementHelper.TakeDefaultAction(textLocation, this.Page,  this.PageId);    
		}

        /// <summary>
        /// Finds the Naptans that are closest to the provided naptan input
        /// </summary>
        /// <param name="naptan"></param>
        /// <returns>TDNaptan[] naptans</returns>
        private TDNaptan[] PopulateNaptans(string[] naptan)
        {
            TDNaptan[] naptans = new TDNaptan[naptan.Length];
            int i = 0;
            try
            {
                foreach (string tempNaptan in naptan)
                {
                    NaptanCacheEntry x = NaptanLookup.Get(tempNaptan.Trim(), "Naptan");

                    if (x.Found)
                    {
                        naptans[i] = new TDNaptan();
                        naptans[i].Naptan = x.Naptan;
                        naptans[i].GridReference = x.OSGR;
                        naptans[i].Name = x.Description;
                        i++;
                    }
                    else
                    {
                        //If any Naptans are not found
                        throw (new FormatException("Naptan code not found or invalid Naptan code used"));
                    }
                }
            }
            catch // Catch's any errors from NaptanLookup.Get, e.g. where "ABC" is the naptan submitted
            {
                throw (new FormatException("Naptan code not found or invalid Naptan code used"));
            }

            return naptans;
        }

        /// <summary>
        /// Populate locality data into relevant object hierarchy
        /// </summary>
        /// <param name="osgr"></param>
        /// <returns></returns>
        private string PopulateLocality(OSGridReference osgr)
        {
            IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

            return gisQuery.FindNearestLocality(osgr.Easting, osgr.Northing);
        }

        /// <summary>
        /// Registers javascript for auto suggest functionality with the location select control
        /// </summary>
        private void RegisterAutoSuggestDropdowonJavascript()
        {
            string autoSuggestJS = "AutoSuggestExtender";
            string locationJs = "Locations";

            // Determine if javascript is support and determine the JavascriptDom value
            ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

            DropDownLocationProviderService dropDownLocationProvider = (DropDownLocationProviderService)TDServiceDiscovery.Current[ServiceDiscoveryKey.DropDownLocationProvider];

            string[] autoSuggestDataScripts = dropDownLocationProvider.GetDropDownLocationDataScriptName(this.PageId);

            if (!dropDownLocationProvider.DropDownLocationEnabled(this.PageId) || autoSuggestDataScripts == null)
            {
                return;
            }

            string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];

            // Register the javascript file for location objects
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), locationJs, scriptRepository.GetScript(locationJs, javaScriptDom));

            // Register the javascript file for auto Suggest dropdown
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), autoSuggestJS, scriptRepository.GetScript(autoSuggestJS, javaScriptDom));

            foreach (string dropDowndatafile in autoSuggestDataScripts)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), dropDowndatafile, scriptRepository.GetScript(dropDowndatafile, javaScriptDom));
            }

            int dropDownTriggerLength = 0;
            if (!int.TryParse(Properties.Current["DropDownGaz.Characters.Minimum"], out dropDownTriggerLength))
            {
                dropDownTriggerLength = 0;
            }

            int maxResults = 0;
            if (!int.TryParse(Properties.Current["DropDownGaz.ShowNumberOfStations.Maximum"], out maxResults))
            {
                maxResults = 0;
            }

            string autoSuggestScript = string.Format("if (typeof initLocationDropDown =='function'){{ initLocationDropDown('{0}','{1}','{2}','{3}',{4},{5},{6});}}", ModeType.Rail, textLocation.ClientID, hdnLocationId.ClientID, autoSuggestFilterControlIds, autoSuggestGroupStations.ToString().ToLower(),dropDownTriggerLength, maxResults);

            Page.ClientScript.RegisterStartupScript(this.GetType(), this.UniqueID, autoSuggestScript, true);

            string unsureSpellingScript = string.Format("if (typeof AutoSuggestExtender.toggle =='function'){{ AutoSuggestExtender.toggle('{0}',!this.checked);}}", textLocation.ClientID);

            checkUnsureSpelling.Attributes["onclick"] = unsureSpellingScript;
        }

		#endregion

        #region Event handlers
        /// <summary>
        /// triggered when the text has changed and not null, then forward the event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InputTextChanged(object sender, System.EventArgs e)
        {
            if (LocationEntered != null && InputText.Length!=0)
                LocationEntered(this, new EventArgs());

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
        
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textLocation.TextChanged += new EventHandler(this.InputTextChanged);
		}
        #endregion
       

    }
}
