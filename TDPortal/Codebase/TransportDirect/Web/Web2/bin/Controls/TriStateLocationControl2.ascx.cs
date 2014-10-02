// *********************************************** 
// NAME                 : TriStateLocationControl2.ascx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 2/12/2003 
// DESCRIPTION  : New version for TriStateLocationControl. 
// User control that display either Valid/unspecified/Ambiguous Location according to a locationSearch and a TDLocation
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TriStateLocationControl2.ascx.cs-arc  $ 
//
//   Rev 1.11   Dec 09 2009 11:31:34   mmodi
//Correct issue with populating an "empty" errored location with valid location seletced from map.
//
//   Rev 1.10   Nov 18 2009 16:22:30   apatel
//Updated to hide findonmap page when javascript is disabled
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.9   Nov 11 2009 15:21:50   apatel
//Updated for find on map button to hide only when showfindonmap property is false and button is visible.
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.8   Nov 10 2009 11:29:52   apatel
//Find Input pages mapping enhancement changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.7   Jun 26 2008 14:04:20   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.6   May 22 2008 16:39:54   mmodi
//Updated to show FindNearest locations as resolved
//Resolution for 4998: "Find Nearest" functionality on Find Train / Find Coach input not working
//Resolution for 5000: Find Train - when ambiguity page is diplayed, resolved locations still editable
//Resolution for 5002: Amend Find a train cost does not use the new location
//
//   Rev 1.5   May 19 2008 15:48:10   mmodi
//Updated to ensure new location text is not shown when in amend mode
//Resolution for 4988: Del 10.1: Text displayed above location input box after amend journey
//
//   Rev 1.4   Apr 30 2008 15:39:24   apatel
//make the location control box to be amendable when amend button clicked.
//Resolution for 4908: From and To fields on train and coach input pages are not amendable when amend button clicked
//
//   Rev 1.3   Apr 02 2008 13:00:54   apatel
//Added property to expose LocationAmbiguousControl
//
//   Rev 1.2   Mar 31 2008 13:23:32   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:34   mturner
//Initial revision.
//
//   Rev 1.34   Jun 05 2006 12:45:06   mmodi
//IR4112 - Amended Refresh method to keep location control in valid/non-editable state when user selected location from Map
//Resolution for 4112: Del 8.2 - Missing gazetteer for location on Find a car
//
//   Rev 1.33   May 03 2006 10:30:42   asinclair
//Added ResetCar method
//Resolution for 3962: DN068: Extend: Ambiguiy / Error messages on Extend Input page
//
//   Rev 1.32   Feb 23 2006 19:17:14   build
//Automatically merged from branch for stream3129
//
//   Rev 1.31.1.0   Jan 10 2006 15:28:00   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.31   Nov 09 2005 14:03:34   NMoorhouse
//TD93 - UEE Input Pages - Update Visit Planner
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.30   Nov 03 2005 17:59:48   NMoorhouse
//Manual Merge of stream2816
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.29   Nov 01 2005 15:11:34   build
//Automatically merged from branch for stream2638
//
//   Rev 1.28.2.1   Oct 25 2005 20:03:14   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.28.2.0   Oct 07 2005 09:47:58   mtillett
//Update layout for UEE chnage to use new location ambiguous control
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.28.1.4   Oct 25 2005 12:57:56   tolomolaiye
//Added GetLocation property
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.28.1.2   Oct 20 2005 10:54:38   asinclair
//Added ResetSelectedIndex()
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.28.1.1   Oct 05 2005 09:44:28   tolomolaiye
//Updates following code review and fxcop
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.28.1.0   Sep 29 2005 14:10:34   tolomolaiye
//Added LocationAmbiguousControl
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.28   May 13 2005 14:53:12   passuied
//Fix that set the amend mode to false if submit is called (and amend was true) then forces the locations to refresh. This is to avoid that since they are init before, they don't reflect the fact amend mode is set to false.
//Resolution for 2468: Location is described as not found even though the ambiguity page has displayed a resolution for the location
//
//   Rev 1.27   Apr 16 2005 11:25:10   rscott
//DEL 7 Changes for IR1980
//
//   Rev 1.26   Sep 30 2004 13:13:08   jbroome
//Extend Journey additional changes (DD02101d)
//
//   Rev 1.25   Aug 27 2004 17:03:28   RPhilpott
//Correct interaction between control hierarchy and LocationSearchHelper.
//Resolution for 1329: Both train and coach stops can be selected on a Find A Train or Find A Coach page
//
//   Rev 1.24   Aug 11 2004 11:01:54   jbroome
//IR 1144 - Exposed NewSearchType event handler. 
//
//   Rev 1.23   Jul 27 2004 10:00:56   passuied
//changes in population. We Update the locationSearch only if the control is visible. Historically, it was done when the location was unspecified but we changed in previous check in because it's not compatible with the amend mode, where the unspecified control is displayed but the location is valid.
//Now we restrict it to when the unspecified control is visible because, it satisifies these 2 cases. when location is unspecified, and when we are in amend mode, the unspecified control is visible and we want to check for users changes
//
//   Rev 1.22   Jul 26 2004 20:23:52   passuied
//Changes to implement AmendSeach Functionality. Created and Amend mode in the tristate to enable the display of a valid location inside the locationUnspecified control.
//We send this mode when a one use session key has been set by a click on AmendSearch button.
//Also tweak in toFromLocationControl to display the to and from location/station controls correctly
//
//   Rev 1.21   Jul 22 2004 16:13:54   COwczarek
//Remove redundant code.
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.20   Jul 14 2004 11:57:08   jmorrissey
//Added overload of populate method that takes an additional parameter of type StationType
//
//   Rev 1.19   Jul 13 2004 16:48:06   jmorrissey
//When this is a FindACoach or FindATrain page, then hide the radio buttons on the locationUnspecified control. Del 6.1 update. 
//
//   Rev 1.18   May 26 2004 09:29:14   jgeorge
//Update for TDJourneyParameters changes
//
//   Rev 1.17   May 19 2004 15:01:48   acaunt
//LocationSearch object passed to LocationDisplayControl.Populate in the Refresh() method
//
//   Rev 1.16   Apr 27 2004 13:53:42   jbroome
//DEL 5.4 Merge. 
//Partial Postcode searching
//
//   Rev 1.15   Apr 08 2004 14:40:12   COwczarek
//Populate method now accepts location type. Location type and
//data service data set are now passed down to nested controls
//(through their Populate methods) so that correct radio buttons /
//prompt text can be generated.
//Resolution for 695: DEL 5.2 QA Changes - Ambiguity page
//
//   Rev 1.14   Apr 02 2004 16:40:42   COwczarek
//Remove firing of redundant StyleChange event.
//Resolution for 695: DEL 5.2 QA Changes - Ambiguity page
//
//   Rev 1.13   Mar 26 2004 11:36:18   COwczarek
//Populate method now takes a checkInput parameter to allow caller to choose if input form values should be checked against session parameters, updating the session parameters if different. Redundant Populate method removed. OnNewLocation event handler now fires an event to indate new location has been entered and performs no other processing.
//Resolution for 662: Back Button from the Ambiguity Page does not unconfirm a confirmed location
//
//   Rev 1.12   Mar 19 2004 10:48:18   ESevern
//DEL5.2 amended styleclass to return boxtypetwo on ambiguity (boxtypeambiguous is too wide)
//
//   Rev 1.11   Mar 16 2004 18:16:06   COwczarek
//Set search type to address/postcode in new location event handler
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.10   Mar 03 2004 15:46:04   COwczarek
//This control no longer needs to handle events raised by the AmbiguousLocationSelectControl2 for the three drop down lists showing hierarchic search results (only one drop down now used).
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.9   Jan 06 2004 12:22:22   passuied
//Added safety check to avoid exception thrown in case of abnormal use of the planner
//Resolution for 575: NullReferenceException
//
//   Rev 1.8   Dec 18 2003 11:19:56   passuied
//minor changes
//
//   Rev 1.7   Dec 18 2003 11:08:02   passuied
//added New Location functionality for valid locations
//Resolution for 557: JP Ambiguity: Formatting
//
//   Rev 1.6   Dec 17 2003 16:54:02   passuied
//Valid location in blue box
//
//   Rev 1.5   Dec 13 2003 14:05:30   passuied
//fix for location controls
//
//   Rev 1.4   Dec 12 2003 19:31:46   passuied
//changes in style + add refresh taken off by mistake
//
//   Rev 1.3   Dec 12 2003 14:36:26   kcheung
//Journey Planner Location Map 5.1 Updates
//
//   Rev 1.2   Dec 04 2003 15:01:26   passuied
//fixed problem of different styles of LocationSelectControl inside TriState
//
//   Rev 1.1   Dec 04 2003 13:09:00   passuied
//final version for del 5.1
//
//   Rev 1.0   Dec 02 2003 16:17:32   passuied
//Initial Revision

using System;using TransportDirect.Common.ResourceManager;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.SessionManager;
using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;

namespace TransportDirect.UserPortal.Web.Controls
{
    

    /// <summary>
    ///     New version for TriStateLocationControl. 
    ///     User control that display either Valid/unspecified/Ambiguous Location according to a locationSearch and a TDLocation
    /// </summary>
    public partial  class TriStateLocationControl2 : TDUserControl
    {
        #region declaration
        
        protected LocationDisplayControl locationValid;
        protected LocationSelectControl2 locationUnspecified;
		protected LocationAmbiguousControl locationAmbiguous;

        private DataServices.DataServices populator;
        private LocationSearch search;
        private TDLocation location;
        private bool disableMapSelection;
        private bool acceptPostcodes;
		private bool acceptPartPostcodes;
        private DataServiceType listType;
        private CurrentLocationType locationType;
		private StationType stationType;
        private LocationSelectControlType theLocationUnspecifiedType;
		private bool isAmendMode = false;
        private bool showFindOnMap = true;
    

        public event EventHandler MapClick;
        public event EventHandler ValidLocation;
        public event EventHandler NewLocation;
		public event EventHandler NewSearchType;

		/// <summary>
		/// Public property to expose the location
		/// </summary>
		public TDLocation GetLocation
		{
			get {return location;}
		}

        #endregion

        #region Contructor and Page_Load
        /// <summary>
        /// Constructor
        /// </summary>
        protected TriStateLocationControl2()
        {
            populator = (DataServices.DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            locationAmbiguous.NewLocation += new EventHandler(OnNewLocation);
            locationValid.NewLocation += new EventHandler(OnNewLocation);
            locationAmbiguous.NewSearchType += new EventHandler(OnNewSearchType);

            locationUnspecified.ControlMap.Click += new EventHandler(OnMapClick);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            TDPage page = (TDPage)this.Page;

            if (locationUnspecified.ControlMap.Visible)
            {
                locationUnspecified.ControlMap.Visible = showFindOnMap & page.IsJavascriptEnabled;
            }

            
        }
        
        #endregion

        #region Control Population

		/// <summary>
        /// Populates the control
        /// </summary>
        /// <param name="listType">Gives DataServiceType to call when using DataServices</param>
        /// <param name="locationType">The type of location this control is being used for, e.g. origin, destination, via</param>
        /// <param name="thisSearch">Search to populate with</param>
        /// <param name="thisLocation">Location to populate with</param>
        /// <param name="locationUnspecifiedType">Location unspecified control type</param>
        /// <param name="disableMapSelection">True if map cannot be selected supplied location</param>
        /// <param name="acceptPostcodes">True if location search text is allowed to contain a postcode</param>
		/// <param name="acceptPartPostcodes">True if location search text is allowed to be a partial postcode</param>
		/// <param name="checkInput">True if form input values should be compared with current session 
        /// state values, updating the session state values if different</param>
        public void Populate(
            DataServiceType listType, 
            CurrentLocationType locationType,
            ref LocationSearch thisSearch, 
            ref TDLocation thisLocation, 
            ref LocationSelectControlType locationUnspecifiedType,
            bool disableMapSelection,
            bool acceptPostcodes,
			bool acceptPartPostcodes,			
            bool checkInput
			)
        {
			//Check if the result should not be grouped
			if (thisSearch.NoGroup)
			{
				stationType = StationType.UndeterminedNoGroup;
			}
			else
			{
				stationType = StationType.Undetermined;
			}

			Populate(listType, 
				locationType,
				ref thisSearch, 
				ref thisLocation, 
				ref locationUnspecifiedType,
				disableMapSelection,
				acceptPostcodes,
				acceptPartPostcodes,			
				stationType,
				checkInput
				);        
        }

		/// <summary>
		/// Override of the Populate method that takes an additional parameter of StationType.
		/// </summary>
		/// <param name="listType">Gives DataServiceType to call when using DataServices</param>
		/// <param name="locationType">The type of location this control is being used for, e.g. origin, destination, via</param>
		/// <param name="thisSearch">Search to populate with</param>
		/// <param name="thisLocation">Location to populate with</param>
		/// <param name="locationUnspecifiedType">Location unspecified control type</param>
		/// <param name="disableMapSelection">True if map cannot be selected supplied location</param>
		/// <param name="acceptPostcodes">True if location search text is allowed to contain a postcode</param>
		/// <param name="acceptPartPostcodes">True if location search text is allowed to be a partial postcode</param>
		/// <param name="stationType">The type of station this control is being used for, e.g. airport, rail, coach</param>
		/// <param name="checkInput">True if form input values should be compared with current session 
		public void Populate(
			DataServiceType listType, 
			CurrentLocationType locationType,
			ref LocationSearch thisSearch, 
			ref TDLocation thisLocation, 
			ref LocationSelectControlType locationUnspecifiedType,
			bool disableMapSelection,
			bool acceptPostcodes,
			bool acceptPartPostcodes,
			StationType stationType,
			bool checkInput
			)
		{
			location = thisLocation;
			search = thisSearch;
			this.disableMapSelection = disableMapSelection;
			this.listType = listType;
			this.locationType = locationType;
			this.theLocationUnspecifiedType = locationUnspecifiedType;
			this.acceptPostcodes = acceptPostcodes;
			this.acceptPartPostcodes = acceptPartPostcodes;
			
			
			this.stationType = stationType;
			
			if (checkInput)
			{
				// We only check for user changes in the locationUnspecified control 
				// if it's visible.
				// Indeed, because of the introduction of this amend mode, which displays
				// a valid location in the location unspecified control, we cannot check
				// for the location itself as it can happen to be valid but we still
				// want to check if the user has made changes to its selection.
				if (locationUnspecified.Visible)
				{
                    // Fix for when a location is selected from the Journey Input page Map, using 
                    // the "Plan a journey" link. The location will be resolved, so no need to reset 
                    // the locations and searches - prevents problem where an error "no options found" location
                    // input is not populated with the location selected from the map.
                    if (!((search.SearchType == SearchType.Map) && (location.Status == TDLocationStatus.Valid)))
                    {
                        locationUnspecified.UpdateLocationSearch(ref search, ref location);
                    }
				}
			}            

			locationUnspecified.SetMembers(ref thisSearch, ref thisLocation);
			locationAmbiguous.SetMembers (ref thisSearch, ref thisLocation);

			Refresh();            
		}

        /// <summary>
        /// Populates the control. If called when Page.IsPostBack is true, form input values
        /// are compared with current session state values, updating the state values if 
        /// different.
        /// </summary>
        /// <param name="listType">Gives DataServiceType to call when using DataServices</param>
        /// <param name="locationType">The type of location this control is being used for, e.g. origin, destination, via</param>
        /// <param name="thisSearch">Search to populate with</param>
        /// <param name="thisLocation">Location to populate with</param>
        /// <param name="disableMapSelection">True if map cannot be selected supplied location</param>
        /// <param name="acceptPostcodes">True if location search text is allowed to contain a postcode</param>
		/// <param name="acceptPartPostcodes">True if location search text is allowed to be a partial postcode</param>
		/// <param name="stationType">The type of station this control is being used for, e.g. airport, rail, coach</param>
		public void Populate(
            DataServiceType listType, 
            CurrentLocationType locationType,
            ref LocationSearch thisSearch, 
            ref TDLocation thisLocation, 
            ref LocationSelectControlType locationUnspecifiedType,
            bool disableMapSelection,
            bool acceptPostcodes,
			bool acceptPartPostcodes			
            )
        {


			//Check if the result should not be grouped
			if (thisSearch.NoGroup)
			{
				stationType = StationType.UndeterminedNoGroup;
			}
			else
			{
				stationType = StationType.Undetermined;
			}

            this.Populate(
                listType,
                locationType,
                ref thisSearch,
                ref thisLocation,
                ref locationUnspecifiedType,
                disableMapSelection,
                acceptPostcodes, 
				acceptPartPostcodes,
				stationType,
                Page.IsPostBack
                );
        }
		
        /// <summary>
        /// Reset the controls needed to be reset when this method is called
        /// </summary>
        public void Reset()
        {
            locationUnspecified.Reset();
        }

		/// <summary>
		/// Reset the controls needed to be reset when this method is called
		/// </summary>
		public void ResetCar()
		{
			locationUnspecified.ResetCar();
		}

		/// <summary>
		/// Method to use in extreme cases when a refresh is really needed on tristatecontrol
		/// and one cannot do otherwise.
		/// </summary>
		public void ForceRefresh()
		{
			Refresh();
		}

        /// <summary>
        /// Refresh Control
        /// </summary>
        private void Refresh()
        {
            switch (location.Status)
            {
                case TDLocationStatus.Unspecified:
                {
    
                    locationUnspecified.Visible = true;
                    locationValid.Visible = false;
                    locationAmbiguous.Visible = false;

                    populator.LoadListControl(listType, locationUnspecified.ControlLocationType);

                    if (location.Status == TDLocationStatus.Unspecified)
                        locationUnspecified.Populate(listType, locationType, ref search, ref location, LocationUnspecifiedType.Type, disableMapSelection, stationType);

                    if (location.Status == TDLocationStatus.Valid)
                    {
                        locationUnspecified.Populate(listType, locationType, ref search, ref location, ControlType.NewLocation, disableMapSelection, stationType);

                        // Prevent the "Select the kind of location.." from being displayed when we're amending 
                        if (isAmendMode)
                            locationUnspecified.LabelNewLocationNoteVisible = false;
                    }
					
                }
                    break;

                case TDLocationStatus.Ambiguous:
                {                    
					locationAmbiguous.Visible = true;
                    locationValid.Visible = false;
                    locationUnspecified.Visible = false;
                    locationAmbiguous.Populate(listType, ref search, ref location);					
                }
                    break;
                case TDLocationStatus.Valid:
                {
					// Special case here. If we are in Amend mode, we want to display
					// the Unspecified control with the typed in data.
					if (AmendMode)
					{
						// Only show the Unspecified control if the original location did
						// not use the Map - Select New Location control 
						if ((location.SearchType != SearchType.Map) && (location.SearchType != SearchType.FindNearest))
						{
							goto case TDLocationStatus.Unspecified;
						}
					}
					locationValid.Visible = true;
                    locationAmbiguous.Visible = false;
                    locationUnspecified.Visible = false;
                    locationValid.Populate(listType, search, location, returnFromDifferentLocation());

                }
                    break;
            }
        }
        
		/// <summary>
		/// Private method which returns a string value of a location description 
		/// if the return location is different from the outward.
		/// i.e. Leave from London Gatwick and return to London Heathrow.
		/// This is passed into the LocationDisplay control so that it can be 
		/// presented to the user.
		/// </summary>
		/// <returns>Description of different location</returns>
		private string returnFromDifferentLocation()
		{
			string differentReturnLocation = string.Empty;
			TDItineraryManager itinerary = TDItineraryManager.Current;
			if (itinerary.ExtendInProgress)
			{
				if (itinerary.ExtendEndOfItinerary)
				{
					// If location has been set in the JourneyParameters by the 
					// ItineraryManager then the location is different.
					if (itinerary.JourneyParameters.ReturnDestinationLocation != null)
						differentReturnLocation = itinerary.JourneyParameters.ReturnDestinationLocation.Description;
				}
				else
				{
					if (itinerary.JourneyParameters.ReturnOriginLocation != null)
						differentReturnLocation = itinerary.JourneyParameters.ReturnOriginLocation.Description;
				}
			}
						
			return differentReturnLocation ;
		}

        #endregion

        #region Public properties
        
		/// <summary>
		/// Read/Write Property.
		/// Indicates if the control must be used in amend mode.
		/// The amend mode is used in FindStation functionality
		/// when AmendSearch is clicked and we want to display a valid
		/// location in the unspecified control.
		/// </summary>
		public bool AmendMode
		{
			get
			{
				return isAmendMode;
			}
			set
			{
				isAmendMode = value;
			}
		}
        /// <summary>
        /// Gets the LocationControl currently displayed
        /// </summary>
        public TDUserControl LocationControl
        {
            get
            {
                if (locationValid.Visible)
                    return locationValid;
                if (locationUnspecified.Visible)
                    return locationUnspecified;
                if (locationAmbiguous.Visible)
                    return locationAmbiguous;
                else return null;
            }
        }

        // Gets the LocationAmbiguousControl 
        public LocationAmbiguousControl LocationAmbiguousControl
        {
            get { return locationAmbiguous; }
        }

        // Gets the locationUnspecified Control
        public LocationSelectControl2 LocationUnspecifiedControl
        {
            get { return locationUnspecified; }
        }

        public bool ShowFindOnMap
        {
            get
            {
                return showFindOnMap;
            }
            set
            {
                showFindOnMap = value;
            }
        }

        /// <summary>
        /// Returns the location status
        /// </summary>
        public TDLocationStatus Status
        {
            get
            {
                if (location !=null)
                    return location.Status;
                else
                    return TDLocationStatus.Unspecified;
            }
        }

        /// <summary>
        /// Read-only Property. Returns the style to use for the div wrapping the control
        /// </summary>
        public string StyleClass
        {
            get
            {
                // case when Nothing specified for via locations
                if (location == null)
                    return "boxtypevia";

                if (location.Status == TDLocationStatus.Unspecified)
                    return "boxtypetwo";
                
                else
                    if (location.Status == TDLocationStatus.Valid)
                        return "boxtypetwo";
                    else
                        return "boxtypetwo";
            }
        }

        private LocationSelectControlType LocationUnspecifiedType
        {
            get
            {
                return theLocationUnspecifiedType;
            }
            set
            {
                theLocationUnspecifiedType = value;             
            }
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Map click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMapClick ( object sender, EventArgs e)
        {
            MapClick(sender, e);
        }

        /// <summary>
        /// Event triggered when one of the Search type ACTIVE radio buttons is clicked
        /// Clear the search and reset all variables
        /// Then do a new search!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNewSearchType(object sender, EventArgs e)
        {
            // For safety reason, if objects are null (abnormal use of site e.g. browser back button). Do nothing
            if (search==null || location== null)
                return;

            location.Status = TDLocationStatus.Unspecified;
            search.ClearSearch();
            locationAmbiguous.ResetSelectedIndex();
            theLocationUnspecifiedType.Type = ControlType.NoMatch;
            Search(false);
			if (NewSearchType != null)	
				NewSearchType(sender, e);

        }

        /// <summary>
        /// Event handler triggered when the 'New Location' button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNewLocation(object sender, EventArgs e)
        {
            if (NewLocation != null)
                NewLocation(sender, e);

        }
        #endregion

        #region Public methods
        /// <summary>
        /// Search for a location using the appropriate control depending on the location status
        /// </summary>
        /// <param name="isValidNaptan"></param>
        public void Search(bool isValidNaptan)
        {
       
            TDJourneyParameters journeyParameters = TDSessionManager.Current.JourneyParameters;
            
            // For safety reason, if objects are null (abnormal use of site e.g. browser back button). Do nothing
            if (search==null || location== null)
                return;

            if (location.Status == TDLocationStatus.Ambiguous)
            {
                locationAmbiguous.RefineLocation(search.CurrentLevel);
                locationAmbiguous.ResetSelectedIndex();
            }
            
            if (location.Status == TDLocationStatus.Unspecified && LocationControl != null)
            {

                theLocationUnspecifiedType.Type = ControlType.NoMatch;
				// Can use acceptPostcodes for allowing Postcodes and Part Postcodes,
				// as when calling from map screen the only time postcodes are not 
				// allowed when looking for Public Via, in which case Part Postcodes
				// are not allowed either.
				locationUnspecified.Search(acceptPostcodes, acceptPartPostcodes, stationType); 
            }

            if (location.Status == TDLocationStatus.Valid && !isValidNaptan)
            {
				if (journeyParameters is TDJourneyParametersMulti)
					LocationSearchHelper.RefreshLocationDetails(
						ref search,
						ref location,
						((TDJourneyParametersMulti)journeyParameters).MaxWalkingTime,
						((TDJourneyParametersMulti)journeyParameters).WalkingSpeed);
				else
					LocationSearchHelper.RefreshLocationDetails(
						ref search,
						ref location,
						0,
						0);

            }


            Refresh();

            //triggers valid location event
            if (location.Status == TDLocationStatus.Valid && ValidLocation != null)
                ValidLocation(this, new EventArgs());

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

		}
        #endregion

    }
}
