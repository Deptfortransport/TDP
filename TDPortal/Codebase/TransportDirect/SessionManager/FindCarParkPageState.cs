// *********************************************** 
// NAME                 : FindCarParkPageState.cs
// AUTHOR               : Esther Severn
// DATE CREATED         : 31/07/2006 
// DESCRIPTION			: Session state for the Find
//						: Nearest Car Park page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/FindCarParkPageState.cs-arc  $ 
//
//   Rev 1.8   May 08 2008 11:40:46   mmodi
//Changes made to multiple files to fix the find a car park feature of the city-to-city trunk mode. Testing performed to ensure that the trunk car park features has not broken the drive to car park mode.
//Resolution for 4954: Include 'Drive to Car Park' functionality in City to City
//
//   Rev 1.7   Mar 31 2008 12:27:42   mturner
//Drop3 from Dev Factory
//
//DEVFACTORY   JAN 08 2008 12:30:00   psheldrake
//Changes to conform to CCN426 : Car Park Usability Updates
//
//   Rev 1.2   Nov 29 2007 12:41:08   mturner
//Changes to remove .Net2 compiler warnings
//
//   Rev 1.1   Nov 29 2007 11:34:56   build
//Updated for Del 9.8
//
//   Rev 1.3   Nov 08 2007 14:24:36   mmodi
//Property to specify max number of car parks
//Resolution for 4524: Del 9.8 - Car Parking Page Landing
//
//   Rev 1.2   Aug 31 2006 14:42:54   MModi
//Added Car Park Found check
//
//   Rev 1.1   Aug 14 2006 14:00:48   esevern
//added car park name ascending sort
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.0   Aug 07 2006 10:53:14   esevern
//Initial revision.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//

using System;
using System.Data;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.SessionManager
{
		
	/// <summary>
	/// Session state for the FindCarPark page
	/// </summary>
	[CLSCompliant(false)]
	[Serializable()]
	public class FindCarParkPageState : FindPageState
	{
        private FindCarParkMode enumFindMode;

        // Used by New Search, Amend, Back button to go to Find Car Park Input in a specific mode
        private FindCarParkMode carParkFindMode; 

        // Used to track mode when switching languages
        private FindCarParkMode carParkFindModePrevious;

        // Used to determine if Find nearest car parks is accessed from Door to door
        // because FindAMode = None by default. When in DoorToDoor mode, FindNearestCarParks can't tell
        private bool isFromDoorToDoor = false;
        private bool isFromCityToCity = false;

        private CurrentLocationType enumLocationType;
		private LocationSearch searchCurrent = null;
		private TDLocation locationCurrent = null;
		private LocationSearch toSearch = null;
		private TDLocation toLocation = null;
		private LocationSearch fromSearch = null;
		private TDLocation fromLocation = null;
		private TDJourneyParameters.LocationSelectControlType typeControlType;
		private DataTable tableResults = null;
		private SortingType enumSortingType;
		private bool boolOptionSortingAsc;
		private bool boolDistanceSortingAsc;
		private bool boolShowingHidingMap;
		private bool boolAmendMode = false;
		private bool boolCarParkSortingAsc;
        private bool boolTotalSpacesAsc;
        private bool boolIsSecureAsc;
        private bool boolHasDisabledSpacesAsc;
		private bool boolCarParkFound = false;
		private int intMaxNumberOfCarParks;
        private string pageLanguage;

		/// <summary>
		/// Location type defining from where the FindStation has been called
		/// </summary>
		public enum CurrentLocationType
		{
			From,
			To
		}

		public enum SortingType
		{
			Option,
			CarParkName,
			Distance,
            TotalSpaces,
            IsSecure,
            HasDisabledSpaces
		}

        public enum FindCarParkMode
        {
            Default,
            DriveFromAndTo,
            DriveTo
        }

		public FindCarParkPageState()
		{
			base.findMode = FindAMode.CarPark;
			Initialise();
		}

		/// <summary>
		/// Initialise the object
		/// </summary>
		new public void Initialise ()
		{
			InitialiseLocation(CurrentLocationType.From);
			InitialiseLocation(CurrentLocationType.To);
			InitialiseCurrentLocation();
			InitialiseTableState();
			enumLocationType = CurrentLocationType.From;
            //enumFindMode = FindCarParkMode.Default;
			intMaxNumberOfCarParks = -1;
		}

		public void InitialiseCurrentLocation()
		{
			CurrentLocation = new TDLocation();
			searchCurrent = new LocationSearch();
			searchCurrent.SearchType = SearchType.Locality;
			typeControlType = new TDJourneyParameters.LocationSelectControlType(
				TDJourneyParameters.ControlType.Default);
			boolAmendMode = false;
		}
		/// <summary>
		/// Initialise one of the locations
		/// </summary>
		/// <param name="type">Location type (from/to)</param>
		public void InitialiseLocation( CurrentLocationType type)
		{
			switch (type)
			{
				case CurrentLocationType.From:
					fromSearch = new LocationSearch();
					fromSearch.SearchType = SearchType.Locality;
					fromLocation = new TDLocation();
					break;
				case CurrentLocationType.To:
					toLocation = new TDLocation();
					toSearch = new LocationSearch();
					toSearch.SearchType = SearchType.Locality;
					break;
			}
		}

		/// <summary>
		/// Initialise table view state in session
		/// </summary>
		public void InitialiseTableState()
		{
			enumSortingType = SortingType.Distance;
			boolCarParkSortingAsc = false;
			boolDistanceSortingAsc = true;
			boolOptionSortingAsc = false;
		}

		/// <summary>
		/// Method that create new locations and clear searches
		/// </summary>
		public void InstateAmendMode()
		{
			
			// We want to restore the state when the user typed something at the first 
			// iteration but hasn't validated. However, the locations won't be deleted.
			if (LocationFrom.Status == TDLocationStatus.Valid)
			{
				CurrentLocation = LocationFrom;
				CurrentSearch = SearchFrom;

				LocationFrom = new TDLocation();
				SearchFrom = new LocationSearch();
			}
			else
			{
				if (LocationTo.Status == TDLocationStatus.Valid)
				{
					CurrentLocation = LocationTo;
					CurrentSearch = SearchTo;

					LocationTo = new TDLocation();
					SearchTo = new LocationSearch();
				}
			}

			CurrentSearch.ClearSearch();
			LocationControlType.Type = TDJourneyParameters.ControlType.Default;
			
			InitialiseTableState();

			boolAmendMode = true;
		}

		/// <summary>
		/// Resets the amend mode boolean to default value
		/// </summary>
		public void DisableAmendMode()
		{
			boolAmendMode = false;
		}
		

		/// <summary>
		/// Current LocationSearch object associated with the page
		/// </summary>
		public LocationSearch CurrentSearch
		{
			get
			{
				return searchCurrent;
			}
			set
			{
				searchCurrent = value;
			}
		}

		/// <summary>
		/// Current TDLocation object associated with the page
		/// </summary>
		public TDLocation CurrentLocation
		{
			get
			{
				return locationCurrent;
			}
			set
			{
				locationCurrent = value;
			}
		}

		/// <summary>
		/// Current LocationSearch object associated with the page
		/// </summary>
		public LocationSearch SearchTo
		{
			get
			{
				return toSearch;
			}
			set
			{
				toSearch = value;
			}
		}

		/// <summary>
		/// Current TDLocation object associated with the page
		/// </summary>
		public TDLocation LocationTo
		{
			get
			{
				return toLocation;
			}
			set
			{
				toLocation = value;
			}
		}

		/// <summary>
		/// Current LocationSearch object associated with the page
		/// </summary>
		public LocationSearch SearchFrom
		{
			get
			{
				return fromSearch;
			}
			set
			{
				fromSearch = value;
			}
		}

		/// <summary>
		/// Current TDLocation object associated with the page
		/// </summary>
		public TDLocation LocationFrom
		{
			get
			{
				return fromLocation;
			}
			set
			{
				fromLocation = value;
			}
		}

		/// <summary>
		/// Location type defining from where the nearest car park has been called	
		/// </summary>
		public CurrentLocationType LocationType
		{
			get
			{
				return enumLocationType;
			}
			set
			{
				enumLocationType = value;
			}
		}

		/// <summary>
		/// Defines the type of the location Control. 
		/// </summary>
		public TDJourneyParameters.LocationSelectControlType LocationControlType
		{
			get
			{
				return typeControlType;
			}
			set
			{
				typeControlType = value;
			}
		}

		

		/// <summary>
		/// DataTable used to display airports on the page
		/// </summary>
		public DataTable ResultsTable
		{
			get
			{
				return tableResults;
			}
			set
			{
				tableResults = value;
			}
		}

		/// <summary>
		/// Enum indication the sorting type of the result table.
		/// </summary>
		public SortingType CurrentSortingType
		{

			get
			{
				return enumSortingType;
			}
			set
			{
				enumSortingType = value;

			}
		}

        /// <summary>
        /// Current Find a Car Park mode - Used by the Find Car Park input page to track mode its in
        /// </summary>
        public FindCarParkMode CurrentFindMode
        {
            get
            {
                return enumFindMode;
            }
            set
            {
                enumFindMode = value;
            }
        }

        /// <summary>
        /// Find a Car Park mode - This is set to force the Find Car Park input page to be loaded in a mode
        /// </summary>
        public FindCarParkMode CarParkFindMode
        {
            get
            {
                return carParkFindMode;
            }
            set
            {
                carParkFindMode = value;
            }
        }

        /// <summary>
        /// Find a Car Park mode - This is used to track the mode of Find Car Park input when switch languages
        /// </summary>
        public FindCarParkMode CarParkFindModePrevious
        {
            get
            {
                return carParkFindModePrevious;
            }
            set
            {
                carParkFindModePrevious = value;
            }
        }

        /// <summary>
        /// Find a Mode - This is set to let the Car park results page to return to the correct planner
        /// </summary>
        public bool IsFromDoorToDoor
        {
            get
            {
                return isFromDoorToDoor;
            }
            set
            {
                isFromDoorToDoor = value;
            }
        }

        /// <summary>
        /// Logs whether we got here from planning a city-to-city (trunk) journey.
        /// </summary>
        public bool IsFromCityToCity
        {
            get
            {
                return isFromCityToCity;
            }
            set
            {
                isFromCityToCity = value;
            }
        }

        /// <summary>
        /// Flag indicating if Is Secure is sorted asc/desc
        /// </summary>
        public bool IsIsSecureAsc
        {
            get
            {
                return boolIsSecureAsc;
            }
            set
            {
                boolIsSecureAsc = value;
            }
        }
        /// <summary>
        /// Flag indicating if Total Spaces is sorted asc/desc
        /// </summary>
        public bool IsTotalSpacesAsc
        {
            get
            {
                return boolTotalSpacesAsc;
            }
            set
            {
                boolTotalSpacesAsc = value;
            }
        }
        /// <summary>
        /// Flag indicating if Has Disabled Spaces is sorted asc/desc
        /// </summary>
        public bool IsHasDisabledSpacesAsc
        {
            get
            {
                return boolHasDisabledSpacesAsc;
            }
            set
            {
                boolHasDisabledSpacesAsc = value;
            }
        }
        /// <summary>
		/// Flag indicating if car park is sorted asc/desc
		/// </summary>
		public bool IsCarParkSortingAsc
		{
			get
			{
				return boolCarParkSortingAsc;
			}
			set
			{
				boolCarParkSortingAsc = value;
			}
		}

		/// <summary>
		/// flag indicating if distance is sorted asc/desc
		/// </summary>
		public bool IsDistanceSortingAsc
		{
			get
			{
				return boolDistanceSortingAsc;
			}
			set
			{
				boolDistanceSortingAsc = value;
			}
		}

		/// <summary>
		/// flag indicating if option is sorted asc/desc
		/// </summary>
		public bool IsOptionSortingAsc
		{
			get
			{
				return boolOptionSortingAsc;
			}
			set
			{
				boolOptionSortingAsc = value;
			}
		}

		/// <summary>
		/// Read/Write property
		/// Flag indicating if show / hide map button
		/// has been clicked.
		/// </summary>
		public bool IsShowingHidingMap
		{
			get 
			{
				return boolShowingHidingMap;
			}
			set
			{
				boolShowingHidingMap = value;
			}
		}

		/// <summary>
		/// Read-only property. Indicates if in amend mode
		/// </summary>
		new public bool AmendMode
		{
			get
			{
				return boolAmendMode;
			}
		}

		/// <summary>
		/// Read/Write property
		/// Flag indicating if car park results found.
		/// </summary>
		public bool CarParkFound
		{
			get 
			{
				return boolCarParkFound;
			}
			set
			{
				boolCarParkFound = value;
			}
		}

		/// <summary>
		/// Read/Write property
		/// Value indicating the max number of car parks to be returned.
		/// </summary>
		public int MaxNumberOfCarParks
		{
			get 
			{
				return intMaxNumberOfCarParks;
			}
			set
			{
				intMaxNumberOfCarParks = value;
			}
		}

        /// <summary>
        /// Read Write property. Holds the page language
        /// used to prevent default state being intialised when switching language
        /// </summary>
        public string PageLanguage
        {
            get { return pageLanguage; }
            set { pageLanguage = value; }
        }
	}
}
