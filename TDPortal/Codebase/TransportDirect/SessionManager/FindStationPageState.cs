// *********************************************** 
// NAME                 : FindStationPageState.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 10/05/2003 
// DESCRIPTION  : Session state for the FindStation page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/FindStationPageState.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:48:26   mturner
//Initial revision.
//
//   Rev 1.16   Sep 06 2004 16:10:02   passuied
//changed default sorting in stationResults page to distance in ascending order
//Resolution for 1459: Find a Sorting order and arrows not working as requested by the DfT
//
//   Rev 1.15   Sep 04 2004 18:18:20   passuied
//Added new sortable and hiddable Transport column
//
//   Rev 1.14   Aug 13 2004 14:27:18   passuied
//Changes for FindA station distinct error message. Added a flag for NotAllStationTypesFound
//
//   Rev 1.13   Aug 02 2004 15:26:10   jbroome
//IR 1252 - Added IsShowingHidingMap property.
//
//   Rev 1.12   Aug 02 2004 11:20:32   jbroome
//IR 1252 - Added IsCheckedAll property.
//
//   Rev 1.11   Jul 27 2004 14:04:08   passuied
//Added Amend mode property
//
//   Rev 1.10   Jul 26 2004 20:17:52   passuied
//Added InstateAmendMode to get back to situation where user just typed an entry and hasn't validated
//
//   Rev 1.9   Jul 21 2004 10:52:36   passuied
//re work for find station 6.1
//
//   Rev 1.8   Jul 14 2004 13:01:26   passuied
//Changes in SessionManager for Del6.1. Compiles
//
//   Rev 1.7   Jul 12 2004 14:14:18   passuied
//added base class FindPageState
//
//   Rev 1.6   Jul 09 2004 15:54:00   passuied
//Initialisation changed to fix radiobutton SearchType display
//
//   Rev 1.5   Jun 04 2004 14:16:02   passuied
//removed PageMode init from initialisation as it's called by FindStationInput page when !Page.IsPostback.
//
//Page mode set by previous page which calls it
//
//   Rev 1.4   May 24 2004 14:16:28   passuied
//check in for others to compile
//
//   Rev 1.3   May 21 2004 15:50:34   passuied
//partly complete update. Check in for backup
//
//   Rev 1.2   May 11 2004 14:19:54   passuied
//working version of FindStationPage
//
//   Rev 1.1   May 10 2004 17:02:40   passuied
//added FindStationPageState
//
//   Rev 1.0   May 10 2004 15:34:30   passuied
//Initial Revision

using System;
using System.Data;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Session state for the FindStation page
	/// </summary>
	[CLSCompliant(false)]
	[Serializable()]
	public class FindStationPageState
	{
		private CurrentLocationType enumLocationType;

		private LocationSearch searchCurrent = null;
		private TDLocation locationCurrent = null;
		private LocationSearch toSearch = null;
		private TDLocation toLocation = null;
		private LocationSearch fromSearch = null;
		private TDLocation fromLocation = null;

		private TDJourneyParameters.LocationSelectControlType typeControlType;
		
		

		private DataTable tableResultsStation = null;
		private SortingType enumSortingType;
		private bool boolOptionSortingAsc;
		private bool boolStationSortingAsc;
		private bool boolDistanceSortingAsc;
		private bool boolTransportSortingAsc;
		private bool boolCheckedAll;
		private bool boolShowingHidingMap;

		private StationType[] arrayStationTypes = null;

		private bool boolAmendMode = false;
		private bool boolNotAllStationTypesFound = false;



		
		
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
			StationName,
			Distance,
			Transport
		}
		public FindStationPageState()
		{
			Initialise();
		}

		/// <summary>
		/// Initialise the object
		/// </summary>
		public void Initialise ()
		{
			
			InitialiseLocation(CurrentLocationType.From);
			InitialiseLocation(CurrentLocationType.To);
			InitialiseCurrentLocation();
			InitialiseTableState();
			enumLocationType = CurrentLocationType.From;
			arrayStationTypes = new StationType[]{StationType.Airport, StationType.Coach, StationType.Rail};
				
		}

		public void InitialiseCurrentLocation()
		{
			CurrentLocation = new TDLocation();
			searchCurrent = new LocationSearch();
			searchCurrent.SearchType = SearchType.Locality;
			typeControlType = new TDJourneyParameters.LocationSelectControlType(
				TDJourneyParameters.ControlType.Default);
			// security. Every time we initialise the current location, we make sure
			// the amend mode is disabled.
			boolAmendMode = false;
			boolNotAllStationTypesFound = false;

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
			boolStationSortingAsc = false;
			boolDistanceSortingAsc = true;
			boolTransportSortingAsc = false;
			boolOptionSortingAsc = false;
		}

		/// <summary>
		/// Method that create new locations and clear searches
		/// </summary>
		public void InstateAmendMode()
		{
			
			// We want to restore the state when the user typed something at the first iteration
			// but hasn't validated. However, the locations won't be deleted.
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
		/// Location type defining from where the FindStation has been called		/// 
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
		public DataTable StationResultsTable
		{
			get
			{
				return tableResultsStation;
			}
			set
			{
				tableResultsStation = value;
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
		/// Flag indicating if airport is sorted asc/desc
		/// </summary>
		public bool IsStationSortingAsc
		{
			get
			{
				return boolStationSortingAsc;
			}
			set
			{
				boolStationSortingAsc = value;
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
		/// flag indicating if transport is sorted asc/desc
		/// </summary>
		public bool IsTransportSortingAsc
		{
			get
			{
				return boolTransportSortingAsc;
			}
			set
			{
				boolTransportSortingAsc = value;
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
		/// Flag indicating if Tick All box is checked
		/// </summary>
		public bool IsCheckedAll
		{
			get
			{
				return boolCheckedAll;
			}
			set
			{
				boolCheckedAll = value;
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
		/// Read/Write property. Station types selected by user
		/// </summary>
		public StationType[] StationTypes
		{
			get
			{
				return arrayStationTypes;
			}
			set
			{
				arrayStationTypes = value;
			}
		}

		/// <summary>
		/// Read-only property. Indicates if in amend mode
		/// </summary>
		public bool AmendMode
		{
			get
			{
				return boolAmendMode;
			}
		}

		/// <summary>
		/// Read/Write property. Indicates if All searched for Station Types
		/// have been found.
		/// </summary>
		public bool NotAllStationTypesFound
		{
			get
			{
				return boolNotAllStationTypesFound;
			}
			set
			{
				boolNotAllStationTypesFound = value;
			}
		}
	}
}
