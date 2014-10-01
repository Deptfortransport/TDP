// ***********************************************
// NAME 		: TDJourneyViewState.cs
// AUTHOR 		: Peter Norell
// DATE CREATED : 21/08/2003
// DESCRIPTION 	: View-state for journey results
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/TDJourneyViewState.cs-arc  $
//
//   Rev 1.4   Sep 01 2011 10:43:24   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.3   Feb 17 2010 11:20:06   rbroddle
//Updates for international planner
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Nov 29 2009 12:34:24   mmodi
//Added map state flags for maps page
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.1   Sep 25 2009 11:34:10   apatel
//Updated for Cycle journey 'arrive by' journeys - USD UK5688851
//Resolution for 5328: CTP - Arrive by journey results show depart after time in summary
//
//   Rev 1.0   Nov 08 2007 12:24:02   mturner
//Initial revision.
//
//   Rev 1.36   Oct 06 2006 10:48:14   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.35.1.0   Sep 20 2006 12:04:06   esevern
//Added property for selected car park index for use in the carParkResultsTableControl, so that radio buttons can be set correctly
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4165: Car Parking: Able to select more than one car park
//
//   Rev 1.35   Mar 14 2006 08:41:38   build
//Automatically merged from branch for stream3353
//
//   Rev 1.34.1.0   Feb 09 2006 16:29:12   RGriffith
//Addition of ConfirmationMode property for use in JourneyReplanInput page
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.34   Aug 04 2005 13:01:34   asinclair
//Change to Initialise array used to prevent double charge of congestion zone in this class.
//Resolution for 2639: DEl 7: Car Costing: Do not display Congestion Charge for return journeys
//
//   Rev 1.33   Aug 03 2005 21:05:28   asinclair
//Fix for IR 2639
//
//   Rev 1.32   Aug 02 2005 12:50:48   jmorrissey
//Fix for Vantive 3868786
//
//   Rev 1.31   Apr 25 2005 10:18:36   esevern
//showoutwardrunning and showreturnrunning removed again
//
//   Rev 1.30   Apr 23 2005 09:37:28   jmorrissey
//Temp fix for build error. Reinstated ShowRunningOutward and ShowRunningReturn build errors.
//
//   Rev 1.29   Apr 22 2005 18:00:38   esevern
//now doesn't distinguish between outward and return show running values
//
//   Rev 1.28   Apr 19 2005 20:00:02   esevern
//added showoutward and return running costs
//
//   Rev 1.27   Mar 18 2005 16:33:04   asinclair
//Added OutwardShowMap and ReturnShowMap
//
//   Rev 1.26   Mar 09 2005 13:56:44   rhopkins
//The SelectedTabIndex should be initialised to zero (i.e. "None")
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.25   Feb 23 2005 16:40:36   rscott
//DEL7 Update - TDDateTime OutwardDateTime and ReturnDateTime changed to TDDateTime[] arrays.
//
//   Rev 1.24   Sep 17 2004 15:13:04   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.23   Aug 27 2004 12:17:32   COwczarek
//Initialise maxResultsToShowOutward and
//maxResultsToShowReturn to a value that indicates the max
//results to show has not yet been derived
//Resolution for 1319: Extend Find a coach - Returning from extend does not display find a coach summary options page
//
//   Rev 1.22   Jul 19 2004 15:28:02   jgeorge
//Del 6.1 updates
//
//   Rev 1.21   Jun 17 2004 10:19:00   JHaydock
//Set default SelectedIntermediateItinerarySegment to -1 for uninitialised
//
//   Rev 1.20   Jun 16 2004 19:31:16   JHaydock
//Added SelectedIntermediateItinerarySegment for leg map display
//
//   Rev 1.19   May 28 2004 12:34:08   JHaydock
//Check in for PrintableFindSummary and Find pages post-addition to MCMS.
//
//   Rev 1.18   May 19 2004 14:34:12   RHopkins
//OriginalJourneyRequest has been put into TDJourneyState class to allow management by ItineraryManager
//
//   Rev 1.17   May 10 2004 15:04:28   RHopkins
//Extend Journey.
//Initial version of Itinerary Manager.
//
//   Rev 1.16   Feb 16 2004 14:28:26   esevern
//DEL5.2: added storing fave journey name and check box selection in viewstate to fix Redirect removing journey name
//
//   Rev 1.15   Dec 04 2003 18:38:10   kcheung
//Del 5.1 updates
//
//   Rev 1.14   Dec 01 2003 14:29:02   kcheung
//Set OutwardAdjustSelect and ReturnAdjustSelect booleans to TRUE by default so that the Adjusted route will be displayed by default.
//Resolution for 460: In the traffic map for a car journey, the adjusted route should be default
//
//   Rev 1.13   Nov 25 2003 11:04:34   passuied
//added property flag indicating if journey has been saved
//Resolution for 347: Strange behaviour on login control
//
//   Rev 1.12   Oct 24 2003 13:48:58   COwczarek
//Add properties for outward/return journey id and type
//
//   Rev 1.11   Oct 22 2003 19:23:28   passuied
//changes for printable output map page
//
//   Rev 1.10   Oct 16 2003 14:05:00   passuied
//minor changes
//
//   Rev 1.9   Sep 25 2003 18:06:40   PNorell
//Ensured everything is linked up together.
//Fixed various small bugs.
//
//   Rev 1.8   Sep 05 2003 15:28:56   passuied
//Deletion of TDLocation, LocationCodingType, OSGridReference and transfer to LocationService
//
//   Rev 1.7   Sep 04 2003 15:15:10   kcheung
//Added extra 'Adjust Selected' for return
//
//   Rev 1.6   Sep 02 2003 13:43:12   kcheung
//Added AdjustedSelected property.
//
//   Rev 1.5   Sep 01 2003 15:21:48   kcheung
//Added SelectedJourneyLeg property.
//
//   Rev 1.4   Aug 29 2003 10:43:20   kcheung
//Updated made after TDTimeSearchType was replaced by a boolean.
//
//   Rev 1.3   Aug 28 2003 17:59:26   kcheung
//Added tab index property
//
//   Rev 1.2   Aug 27 2003 17:22:40   kcheung
//As before but applied to the property headers.
//
//   Rev 1.1   Aug 27 2003 17:21:38   kcheung
//Updated selectedOutwardJourney & selectedReturnJourney from uint to int otherwise 0 would be selected by default.
//
//   Rev 1.0   Aug 27 2003 10:50:12   PNorell
//Initial Revision
using System;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Specifies a sortable data column (used by FormattedJourneySummaryLines)
	/// </summary>
	public enum JourneySummaryColumn
	{
		None,
		JourneyIndex,
		Origin,
		Destination,
		JourneyType,
		Mode,
		InterchangeCount,
		DepartureTime,
		ArrivalTime,
		RoadMiles,
		DisplayNumber,
		Duration,
		OperatorName
	}

	/// <summary>
	/// View-state for journey results
	/// </summary>
	[Serializable()]
	public class TDJourneyViewState
	{
		private PageId callingPageID = PageId.Empty;
		/// <summary>
		/// Gets/sets the calling page id
		/// </summary>
		public PageId CallingPageID
		{
			get
			{
				return callingPageID;
			}
			set
			{
				callingPageID = value;
			}
		}

		private bool saveChecked = false;
		/// <summary>
		/// Gets/sets the save favourite journey checkbox value
		/// </summary>
		public bool SaveFavouriteChecked
		{
			get
			{
				return saveChecked;
			}
			set
			{
				saveChecked = value;
			}
		}

		private string favouriteName = string.Empty;
		/// <summary>
		/// Gets/sets the save favourite journey checkbox value
		/// </summary>
		public string SaveFavouriteName
		{
			get
			{
				return favouriteName;
			}
			set
			{
				favouriteName = value;
			}
		}

		private TDJourneyState journeyState = new TDJourneyState();
		/// <summary>
		/// Gets/sets the all of the details of the current journey selection
		/// </summary>
		public TDJourneyState JourneyState 
		{
			get
			{
				return journeyState;
			}
			set
			{
				journeyState = value;
			}
		}

		/// <summary>
		/// Gets the original journey request
		/// </summary>
		public ITDJourneyRequest OriginalJourneyRequest
		{
			get
			{
				return journeyState.OriginalJourneyRequest;
			}
			set
			{
				journeyState.OriginalJourneyRequest = value;
			}
		}

		/// <summary>
		/// Gets/sets the selected outward journey index
		/// </summary>
		public int SelectedOutwardJourney 
		{
			get
			{
				return journeyState.SelectedOutwardJourney ;
			}
			set
			{
				journeyState.SelectedOutwardJourney  = value;
			}
		}

		/// <summary>
		/// Gets/sets the selected outward journey identifier
		/// </summary>
		public int SelectedOutwardJourneyID 
		{
			get
			{
				return journeyState.SelectedOutwardJourneyID;
			}
			set
			{
				journeyState.SelectedOutwardJourneyID = value;
			}
		}

		/// <summary>
		/// Gets/sets the selected outward journey type
		/// </summary>
		public TDJourneyType SelectedOutwardJourneyType 
		{
			get
			{
				return journeyState.SelectedOutwardJourneyType ;
			}
			set
			{
				journeyState.SelectedOutwardJourneyType  = value;
			}
		}

		/// <summary>
		/// Gets/sets the selected return journey index
		/// </summary>
		public int SelectedReturnJourney 
		{
			get
			{
				return journeyState.SelectedReturnJourney ;
			}
			set
			{
				journeyState.SelectedReturnJourney  = value;
			}
		}

		/// <summary>
		/// Gets/sets the selected return journey identifier
		/// </summary>
		public int SelectedReturnJourneyID 
		{
			get
			{
				return journeyState.SelectedReturnJourneyID ;
			}
			set
			{
				journeyState.SelectedReturnJourneyID = value;
			}
		}

		/// <summary>
		/// Gets/sets the selected return journey type
		/// </summary>
		public TDJourneyType SelectedReturnJourneyType 
		{
			get
			{
				return journeyState.SelectedReturnJourneyType ;
			}
			set
			{
				journeyState.SelectedReturnJourneyType  = value;
			}
		}

		private int selectedTabIndex = 0;
		/// <summary>
		/// Gets/sets the selected tab index
		/// </summary>
		public int SelectedTabIndex
		{
			get
			{
				return selectedTabIndex ;
			}
			set
			{
				selectedTabIndex = value;
			}
		}

		private TDJourneyType selectedRouteJourneyType = TDJourneyType.PublicOriginal;
		/// <summary>
		/// Gets/sets the selected journey type
		/// </summary>
		public TDJourneyType SelectedRouteJourneyType 
		{
			get
			{
				return selectedRouteJourneyType ;
			}
			set
			{
				selectedRouteJourneyType  = value;
			}
		}

		
		/// <summary>
		/// Gets/sets the date time for leaving
		/// </summary>
		public TDDateTime JourneyLeavingDateTime 
		{
			get
			{
				if ( OriginalJourneyRequest != null)
					return OriginalJourneyRequest.OutwardDateTime[0];
				else
					return null;
			}
			set
			{
				if (OriginalJourneyRequest != null)
					OriginalJourneyRequest.OutwardDateTime[0] = value;
			}
		}

		
		/// <summary>
		/// Gets/sets the type for leaving (True = Arrive Before, False =  Leave After)
		/// </summary>
		public bool JourneyLeavingTimeSearchType 
		{
			get
			{
				return OriginalJourneyRequest != null && OriginalJourneyRequest.OutwardArriveBefore;
			}
			set
			{
				if (OriginalJourneyRequest != null)
					OriginalJourneyRequest.OutwardArriveBefore = value;
			}
		}
		
		
		/// <summary>
		/// Gets/sets the date time for returning
		/// </summary>
		public TDDateTime JourneyReturningDateTime 
		{
			get
			{
				if (OriginalJourneyRequest != null)
					return OriginalJourneyRequest.ReturnDateTime[0];
				else
					return null;
			}
			set
			{
				if (OriginalJourneyRequest != null)
					OriginalJourneyRequest.ReturnDateTime[0] = value;
			}
		}


		
		/// <summary>
		/// Gets/sets the type for leaving (True = Arrive Before, False =  Leave After)
		/// </summary>
		public bool JourneyReturningTimeSearchType 
		{
			get
			{
				return OriginalJourneyRequest!= null && OriginalJourneyRequest.ReturnArriveBefore;
			}
			set
			{
				if (OriginalJourneyRequest != null)
					OriginalJourneyRequest.ReturnArriveBefore = value;
			}
		}

		private int selectedJourneyLeg;
		/// <summary>
		/// Gets/sets the selected journey leg
		/// </summary>
		public int SelectedJourneyLeg
		{
			get
			{
				return selectedJourneyLeg;
			}
			set
			{
				selectedJourneyLeg = value;
			}
		}

		private int selectedIntermediateItinerarySegment = -1;
		/// <summary>
		/// Gets/sets the selected journey leg
		/// </summary>
		public int SelectedIntermediateItinerarySegment
		{
			get
			{
				return selectedIntermediateItinerarySegment;
			}
			set
			{
				selectedIntermediateItinerarySegment = value;
			}
		}

		private bool boolOutwardShowDirections = false;
		/// <summary>
		/// Get/set property indicating if user has selected showDirections on Map page
		/// </summary>
		public bool OutwardShowDirections
		{
			get
			{
				return boolOutwardShowDirections;
			}
			set
			{
				boolOutwardShowDirections = value;
			}
		}
		private bool boolReturnShowDirections = false;
		/// <summary>
		/// Get/set property indicating if user has selected showDirections on Map page
		/// </summary>
		public bool ReturnShowDirections
		{
			get
			{
				return boolReturnShowDirections;
			}
			set
			{
				boolReturnShowDirections = value;
			}
		}

		private bool boolFavouriteJourneySaved = false;
		/// <summary>
		/// Gets/Sets flag indicating if a favourite journey has been saved
		/// </summary>
		public bool FavouriteJourneySaved
		{
			get
			{
				return boolFavouriteJourneySaved;
			}
			set
			{
				boolFavouriteJourneySaved = value;
			}
		}

		private bool confirmationMode = false;
		/// <summary>
		/// Gets/Sets flag indicating if journey replan input page is in confirmationMode
		/// </summary>
		public bool ConfirmationMode
		{
			get
			{
				return confirmationMode;
			}
			set
			{
				confirmationMode = value;
			}
		}
		
		private bool journeyDetailsOutwardDiagramMode = true;
		/// <summary>
		/// Gets/Sets flag indicating if a diagram should be displayed
		/// for the outward journey details.
		/// True = Show Diagram, False = Show Table.
		/// </summary>
		public bool ShowOutwardJourneyDetailsDiagramMode
		{
			get
			{
				return journeyDetailsOutwardDiagramMode;
			}
			set
			{
				journeyDetailsOutwardDiagramMode = value;
			}
		}

		private bool journeyDetailsReturnDiagramMode = true;
		/// <summary>
		/// Gets/Sets flag indicating if a diagram should be displayed
		/// for the outward journey details.
		/// True = Show Diagram, False = Show Table.
		/// </summary>
		public bool ShowReturnJourneyDetailsDiagramMode
		{
			get
			{
				return journeyDetailsReturnDiagramMode;
			}
			set
			{
				journeyDetailsReturnDiagramMode = value;
			}
		}

		private int outwardSortedColumnIndex = 0;
		/// <summary>
		/// Sorted results grid outward sorted column index
		/// </summary>
		public int OutwardSortedColumnIndex
		{
			get
			{
				return outwardSortedColumnIndex;
			}
			set
			{
				outwardSortedColumnIndex = value;
			}
		}

		private int returnSortedColumnIndex = 0;
		/// <summary>
		/// Sorted results grid return sorted column index
		/// </summary>
		public int ReturnSortedColumnIndex
		{
			get
			{
				return returnSortedColumnIndex;
			}
			set
			{
				returnSortedColumnIndex = value;
			}
		}

		private JourneySummaryColumn outwardSortedColumnID = JourneySummaryColumn.None;
		/// <summary>
		/// Sorted results grid outward sorted column enum identifier
		/// </summary>
		public JourneySummaryColumn OutwardSortedColumnID
		{
			get
			{
				return outwardSortedColumnID;
			}
			set
			{
				outwardSortedColumnID = value;
			}
		}

		private JourneySummaryColumn returnSortedColumnID = JourneySummaryColumn.None;
		/// <summary>
		/// Sorted results grid return sorted column enum identifier
		/// </summary>
		public JourneySummaryColumn ReturnSortedColumnID
		{
			get
			{
				return returnSortedColumnID;
			}
			set
			{
				returnSortedColumnID = value;
			}
		}

		private bool outwardSortedDescending;
		/// <summary>
		/// Whether the sorted column of the outward results grid is sorted descending
		/// </summary>
		public bool OutwardSortedDescending
		{
			get
			{
				return outwardSortedDescending;
			}
			set
			{
				outwardSortedDescending = value;
			}
		}

		private bool returnSortedDescending;
		/// <summary>
		/// Whether the sorted column of the return results grid is sorted descending
		/// </summary>
		public bool ReturnSortedDescending
		{
			get
			{
				return returnSortedDescending;
			}
			set
			{
				returnSortedDescending = value;
			}
		}

		private int outwardSelectedJourneyRowIndex;
		/// <summary>
		/// Results grid outward selected row index
		/// </summary>
		public int OutwardSelectedJourneyRowIndex
		{
			get
			{
				return outwardSelectedJourneyRowIndex;
			}
			set
			{
				outwardSelectedJourneyRowIndex = value;
			}
		}

		private int returnSelectedJourneyRowIndex;
		/// <summary>
		/// Results grid return selected row index
		/// </summary>
		public int ReturnSelectedJourneyRowIndex
		{
			get
			{
				return returnSelectedJourneyRowIndex;
			}
			set
			{
				returnSelectedJourneyRowIndex = value;
			}
		}

		/// <summary>
		/// Initial value for maxResultsToShowOutward and maxResultsToShowReturn
		/// to indicate the max results to show has not yet been derived
		/// </summary>
		public const int RESULTS_TO_SHOW_UNDEFINED = -1;

		private int maxResultsToShowOutward = RESULTS_TO_SHOW_UNDEFINED;
		/// <summary>
		/// The maximum number of results that should be displayed in the grid.
		/// This should only ever be set to a value that can be found in the values
		/// of DataServiceType.FSCResultsToDisplayDrop
		/// </summary>
		public int MaxResultsToShowOutward
		{
			get { return maxResultsToShowOutward; }
			set { maxResultsToShowOutward = value; }
		}

		private int maxResultsToShowReturn = RESULTS_TO_SHOW_UNDEFINED;
		/// <summary>
		/// The maximum number of results that should be displayed in the grid.
		/// This should only ever be set to a value that can be found in the values
		/// of DataServiceType.FSCResultsToDisplayDrop
		/// </summary>
		public int MaxResultsToShowReturn
		{
			get { return maxResultsToShowReturn; }
			set { maxResultsToShowReturn = value; }
		}

		private bool boolOutwardShowMap = false;
		/// <summary>
		/// Get/set property indicating if user has selected showMap on Details page
		/// </summary>
		public bool OutwardShowMap
		{
			get { return boolOutwardShowMap; }
			set { boolOutwardShowMap = value; }
		}
		
		private bool boolReturnShowMap = false;
		/// <summary>
		/// Get/set property indicating if user has selected showMap on Details page
		/// </summary>
		public bool ReturnShowMap
		{
            get { return boolReturnShowMap; }
			set	{ boolReturnShowMap = value; }
		}

        private bool boolOutwardMapSelected = false;
        /// <summary>
        /// Get/set property indicating if user has selected to view the Outward map on Maps page
        /// </summary>
        public bool OutwardMapSelected
        {
            get { return boolOutwardMapSelected; }
            set { boolOutwardMapSelected = value; }
        }

        private bool boolReturnMapSelected = false;
        /// <summary>
        /// Get/set property indicating if user has selected to vieww Return Map on Maps page
        /// </summary>
        public bool ReturnMapSelected
        {
            get { return boolReturnMapSelected; }
            set { boolReturnMapSelected = value; }
        }

		private bool showRunning = false;
		/// <summary>
		/// Read/write property indicating whether running costs should 
		/// be displayed for the journey
		/// </summary>
		public bool ShowRunning
		{
			get
			{
				return showRunning;
			}
			set
			{
				showRunning = value;
			}
		}

        private bool showCO2 = false;
        /// <summary>
        /// Read/write property indicating whether we are showing CO2 on details page
        /// for the journey
        /// </summary>
        public bool ShowCO2
        {
            get
            {
                return showCO2;
            }
            set
            {
                showCO2 = value;
            }
        }

		private bool congestionChargeAdded = false;
		/// <summary>
		/// Vantive 3868786 fix. 
		/// Read/write property indicating whether a congestion charge has already been applied 
		/// to the complete journey 
		/// </summary>
		public bool CongestionChargeAdded
		{
			get
			{
				return congestionChargeAdded;
			}
			set
			{
				congestionChargeAdded = value;
			}
		}

		private bool congestionCostAdded = false;
		/// <summary>
		/// Vantive 3868786 fix. 
		/// Read/write property indicating whether a congestion charge has already been applied 
		/// to the complete journey 
		/// </summary>
		public bool CongestionCostAdded
		{
			get
			{
				return congestionCostAdded;
			}
			set
			{
				congestionCostAdded = value;
			}
		}

		private ArrayList visitedcongestionCompany = new ArrayList();
		/// <summary>
		/// Vantive 3868786 fix. 
		/// Read/write property indicating whether a congestion charge company's charge has already  
		/// been applied to the complete journey 
		/// </summary>
		public ArrayList VisitedCongestionCompany
		{
			get
			{
				return visitedcongestionCompany;
			}
			set
			{
				visitedcongestionCompany = value;
			}

		}

		private int selectedCarParkIndex = 0;
		/// <summary>
		/// Gets/sets the selected car park index
		/// </summary>
		public int SelectedCarParkIndex
		{
			get
			{
				return selectedCarParkIndex;
			}
			set
			{
				selectedCarParkIndex = value;
			}
		}

        private bool cycleJourneyLeavingTimeSearchType = false;
        /// <summary>
        /// Gets/sets cycle journey leaving time search type (True = Arrive Before, False =  Leave After)
        /// </summary>
        public bool CycleJourneyLeavingTimeSearchType
        {
            get
            {
                return cycleJourneyLeavingTimeSearchType;
            }
            set
            {
                cycleJourneyLeavingTimeSearchType = value;
            }
        }

        private bool cycleJourneyReturningTimeSearchType = false;
        /// <summary>
        /// Gets/sets cycle journey returning time search type (True = Arrive Before, False =  Leave After)
        /// </summary>
        public bool CycleJourneyReturningTimeSearchType
        {
            get
            {
                return cycleJourneyReturningTimeSearchType;
            }
            set
            {
                cycleJourneyReturningTimeSearchType = value;
            }
        }

        private bool outwardRoadReplanned = false;

        /// <summary>
        /// Gets/sets the flag to determine if the outward road journey is replanned
        /// </summary>
        public bool OutwardRoadReplanned
        {
            get
            {
                return outwardRoadReplanned;
            }
            set
            {
                outwardRoadReplanned = value;
            }
        }

        private bool returnRoadReplanned = false;

        /// <summary>
        /// Gets/sets the flag to determine if the return road journey is replanned
        /// </summary>
        public bool ReturnRoadReplanned
        {
            get
            {
                return returnRoadReplanned;
            }
            set
            {
                returnRoadReplanned = value;
            }
        }

	}
}
