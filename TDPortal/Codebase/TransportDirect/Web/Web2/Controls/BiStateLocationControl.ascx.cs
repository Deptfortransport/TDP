// *********************************************** 
// NAME         : BiStateLocationControl.ascx
// AUTHOR       : C.M. Owczarek
// DATE CREATED : 14.03.04
// DESCRIPTION  : Used by the journey planner input page to display
// either the location search control or the location display control
// for "from","to","public via" and "private via" locations.
// The location search control is displayed for locations which have 
// not been specified or have been resolved. The location display control
// is displayed for locations resolved from the map, in which case the
// display will indicate the location was "selected from map".
// Based on TriStateLocationControl2.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/BiStateLocationControl.ascx.cs-arc  $
//
//   Rev 1.3   Dec 04 2009 08:48:58   apatel
//input page mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.2   Mar 31 2008 13:19:26   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:22   mturner
//Initial revision.
//
//   Rev 1.12   Oct 06 2006 14:18:04   mturner
//Merge for stream SCR-4143
//
//   Rev 1.11.1.0   Sep 22 2006 14:10:30   mmodi
//Exposed NewLocation event
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4196: Car Parking: New location button retains Car Park location
//
//   Rev 1.11   Feb 23 2006 19:16:22   build
//Automatically merged from branch for stream3129
//
//   Rev 1.10.1.0   Jan 10 2006 15:23:36   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.10   Nov 04 2005 13:35:10   NMoorhouse
//Manual merge of stream2816
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.9   Nov 02 2005 18:33:44   kjosling
//Automatically merged from branch for stream2877
//
//   Rev 1.8.2.0   Oct 28 2005 16:36:28   halkatib
//changes made for landing page phase 2
//Resolution for 2877: Del 8  Landing Page Phase2
//
//   Rev 1.8.1.0   Oct 26 2005 10:17:58   RGriffith
//TD089 ES020 Image Button Replacement
//
//   Rev 1.8   Sep 30 2004 13:13:06   jbroome
//Extend Journey additional changes (DD02101d)
//
//   Rev 1.7   Jun 04 2004 13:50:26   RPhilpott
//Use DataServices to get default location type, instead of random hard-coding. 
//
//   Rev 1.6   May 24 2004 17:35:42   ESevern
//added check for fixed location (extend journey display)
//
//   Rev 1.5   May 19 2004 15:01:46   acaunt
//LocationSearch object passed to LocationDisplayControl.Populate in the Refresh() method
//
//   Rev 1.4   Apr 08 2004 14:28:46   COwczarek
//Pass location type (e.g. origin, destination) through Populate
//method so that purpose of control can be used to distinguish
//correct prompt text to display
//Resolution for 695: DEL 5.2 QA Changes - Ambiguity page
//
//   Rev 1.3   Mar 26 2004 11:33:06   COwczarek
//Populate method now takes a checkInput parameter to allow caller to choose if input form values should be checked against session parameters, updating the session parameters if different. Redundant Search method removed.
//Resolution for 662: Back Button from the Ambiguity Page does not unconfirm a confirmed location
//
//   Rev 1.2   Mar 19 2004 16:37:42   COwczarek
//Complete implementation
//Resolution for 649: Changes to the way ambiguous locations are resolved

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

namespace TransportDirect.UserPortal.Web.Controls
{
    
    /// <summary>
    /// Used by the journey planner input page to display
    /// either the location search control or the location display control
    /// for "from","to","public via" and "private via" locations.
    /// The location search control is displayed for locations which have 
    /// not been specified or have been resolved. The location display control
    /// is displayed for locations resolved from the map, in which case the
    /// display will indicate the location was "selected from map".
    /// Based on TriStateLocationControl2.
    /// </summary>
    public partial  class BiStateLocationControl : TDUserControl
    {
        #region Declaration
        
        protected LocationDisplayControl locationValid;
        protected LocationSelectControl2 locationUnspecified;

        private DataServices.DataServices populator;
        private LocationSearch search;
        private TDLocation location;
        private bool disableMapSelection;
        private bool acceptPostcodes;
        protected System.Web.UI.WebControls.Label nothingChosen;
        private DataServiceType listType;
        private CurrentLocationType locationType;

        public event EventHandler MapClick;
		public event EventHandler NewLocationClick;

        #endregion

        #region Contructor and Page_Load

        /// <summary>
        /// Constructor
        /// </summary>
        protected BiStateLocationControl()
        {
            populator = (DataServices.DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
        }

        /// <summary>
        /// Page load event handler
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event parameters</param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            locationValid.NewLocation += new EventHandler(onNewLocation);
            locationUnspecified.ControlMap.Click += new EventHandler(onMapClick);
        }
        
        #endregion

        #region Control population

        /// <summary>
        /// Populates the control. 
        /// </summary>
        /// <param name="listType">gives DataServiceType to call when using DataServices</param>
        /// <param name="locationType">The type of location this control is being used for, e.g. origin, destination, via</param>
        /// <param name="thisSearch">Search to populate with</param>
        /// <param name="thisLocation">Location to populate with</param>
        /// <param name="disableMapSelection">True if map cannot be selected supplied location</param>
        /// <param name="acceptPostcodes">True if location search text is allowed to contain a postcode</param>
        /// <param name="checkInput">True if form input values should be compared with current session 
        /// state values, updating the session state values if different</param>
        public void Populate(
            DataServiceType listType,
            CurrentLocationType locationType,
            ref LocationSearch thisSearch, 
            ref TDLocation thisLocation, 
            bool disableMapSelection,
            bool acceptPostcodes,
            bool checkInput)
        {
            location = thisLocation;
            search = thisSearch;
            this.disableMapSelection = disableMapSelection;
            this.listType = listType;
            this.locationType = locationType;
            this.acceptPostcodes = acceptPostcodes;

            if (checkInput) 
            {
                if (search.SearchType != SearchType.Map)
                    // the following method should only be called if the location search control
                    // is currently visible which is the case if the search type is not 'map'
                    locationUnspecified.UpdateLocationSearch(ref search, ref location);
            }

            locationUnspecified.SetMembers(ref thisSearch, ref thisLocation);

            refresh();
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
        public void Populate(
            DataServiceType listType, 
            CurrentLocationType locationType,
            ref LocationSearch thisSearch, 
            ref TDLocation thisLocation, 
            bool disableMapSelection,
            bool acceptPostcodes
            )
        {
            this.Populate(
                listType,
                locationType,
                ref thisSearch,
                ref thisLocation,
                disableMapSelection,
                acceptPostcodes,
                Page.IsPostBack
                );
        }

        /// <summary>
        /// Refreshes either the nested location search control or location 
        /// display control with values from the location search object
        /// associated with this control and makes that control visible.
        /// </summary>
        private void refresh()
        {
            // Only make the location display control visible if the location has 
            // resolved and it was resolved using a map
            if ( (search.LocationFixed) || ((location.Status == TDLocationStatus.Valid) && (search.SearchType == SearchType.Map)) )
			{
				locationValid.Visible = true;
                locationUnspecified.Visible = false;
                locationValid.Populate(listType, search, location, returnFromDifferentLocation());
            } else {
                locationUnspecified.Visible = true;
                locationValid.Visible = false;
                populator.LoadListControl(listType, locationUnspecified.ControlLocationType);                    
                locationUnspecified.Populate(listType, locationType, ref search, ref location, disableMapSelection);
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
        /// The control allowing text entry of a location
        /// </summary>
        public LocationSelectControl2 LocationUnspecified
        {
            get {
                return locationUnspecified;
            }
        }

        /// <summary>
        /// Status of location associated with this control.
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

        #endregion

        #region Event handlers

        /// <summary>
        /// Map click event handler 
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event parameters</param>
        private void onMapClick ( object sender, EventArgs e)
        {
            MapClick(sender, e);
        }

        /// <summary>
        /// Event triggered when the 'New Location' button is clicked
        /// Resets the search object associated with this control.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event parameters</param>
        private void onNewLocation(object sender, EventArgs e)
        {
			if (NewLocationClick != null)
				NewLocationClick(sender, e);

            // For safety reason, if objects are null (abnormal use of site e.g. browser back button). Do nothing
            if (search==null || location== null)
                return;

			location.ClearAll();			
			search.ClearAll();			
			
			// change due to landing page phase 2:
			// set location fixed to false so that the refresh method will allow the locationunspecified 
			// control to be displayed. 
			search.LocationFixed = false;

            if (search.SearchType == SearchType.Map) 
			{
				search.SearchType = GetDefaultSearchType(locationType);
            }

            refresh();

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
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }

        #endregion

		#region Helper methods

		/// <summary>
		/// Convenience method for getting the default gazetteer for this location type.
		/// </summary>
		/// <param name="key">The key for the text</param>
		/// <returns>The resource string</returns>
		private SearchType GetDefaultSearchType(CurrentLocationType locationType)
		{
			DataServices.DataServiceType searchTypes = DataServices.DataServiceType.LocationTypeDrop;

			switch (locationType)
			{
				case CurrentLocationType.From:
					searchTypes = DataServices.DataServiceType.FromToDrop;
					break;
				case CurrentLocationType.To:
					searchTypes = DataServices.DataServiceType.FromToDrop;
					break;
				case CurrentLocationType.PrivateVia:
					searchTypes = DataServices.DataServiceType.CarViaDrop;
					break;
				case CurrentLocationType.PublicVia:
					searchTypes = DataServices.DataServiceType.PTViaDrop;
					break;
				case CurrentLocationType.Alternate1:
					searchTypes = DataServices.DataServiceType.AltFromToDrop;
					break;
				case CurrentLocationType.Alternate2:
					searchTypes = DataServices.DataServiceType.AltFromToDrop;
					break;
			}

			DataServices.IDataServices ds = (DataServices.IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			
			string defaultItemValue = ds.GetDefaultListControlValue(searchTypes);
			return (SearchType) (Enum.Parse(typeof(SearchType), defaultItemValue));
		}

		#endregion

    }
}