// ***********************************************
// NAME 		: JourneyMapState.cs
// AUTHOR 		: Atos Origin
// DATE CREATED : 01/03/2004
// DESCRIPTION 	: State object for the Map pages
//
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/JourneyMapState.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:32   mturner
//Initial revision.
//
//   Rev 1.5   Sep 20 2004 11:53:58   jbroome
//IR 1533 - Added SelectedJourneySegment property. Used to store the selected segment in the map drop down.
//
//   Rev 1.4   Sep 15 2004 09:28:24   jbroome
//Removal of redundant StateEnum values after redesign of Map tools screen flow.
//
//   Rev 1.3   Mar 16 2004 10:03:20   CHosegood
//Del 5.2 map changes.
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.2   Mar 11 2004 11:13:22   CHosegood
//Added Location and LoctaionSearch
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.1   Mar 08 2004 19:56:06   CHosegood
//Removed Location
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.0   Mar 01 2004 15:51:44   CHosegood
//Initial Revision

using System;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.SessionManager
{
    /// <summary>
    /// Different allowable states of the MapLocationControl
    /// </summary>
    public enum StateEnum
    {
        Default,
        Plan,
        Select_Option,
        Select,
        FindInformation
    }


    /// <summary>
    /// State object for the Map pages.
    /// </summary>
    [Serializable()]
    public class JourneyMapState
    {

        #region private properites
        private CurrentLocationType locationType;
        private CurrentMapMode mapMode;
        private StateEnum state;
        private bool selectEnabled;
        private TDLocation location;
        private LocationSearch locationSearch;
		private int selectedSegmentIndex = -1;
        #endregion

        #region Public properites

        /// <summary>
        /// Current Map location type
        /// </summary>
        public CurrentLocationType LocationType 
        {
            get { return locationType; }
            set { locationType = value; }
        }

        /// <summary>
        /// If the zoom level is within the set limits then the user
        /// may select locations on the map instead of just zoom in.
        /// </summary>
        public bool SelectEnabled 
        {
            get { return this.selectEnabled; }
            set { this.selectEnabled = value; }
        }

        /// <summary>
        /// The mode of the map
        /// </summary>
        public CurrentMapMode MapMode 
        {
            get { return mapMode; }
            set { mapMode = value; }
        }

        /// <summary>
        /// The current state of the Map
        /// </summary>
        public StateEnum State 
        {
            get { return state; }
            set { state = value; }
        }

		/// <summary>
		/// The selected segment in the map dropdown.
		/// </summary>
		public int SelectedJourneySegment
		{
			get { return selectedSegmentIndex; }
			set { selectedSegmentIndex = value; }
		}

        /// <summary>
        /// 
        /// </summary>
        [CLSCompliant(false)]
        public TDLocation Location 
        {
            get { return location; }
            set { this.location = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [CLSCompliant(false)]
        public LocationSearch LocationSearch
        {
            get { return locationSearch; }
            set { this.locationSearch = value; }
        }

        #endregion

        /// <summary>
        /// Reset the Map State
        /// </summary>
        public void Initialise() 
        {
            locationType = CurrentLocationType.None;
            mapMode = CurrentMapMode.FromJourneyInput;
            state = StateEnum.Default;
            selectEnabled = false;
            location = new TDLocation();
            locationSearch = new LocationSearch();
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public JourneyMapState()
        {
            Initialise();
        }
    }

}
