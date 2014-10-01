// ***********************************************
// NAME 		: TDCurrentAdjustState.cs
// AUTHOR 		: Peter Norell
// DATE CREATED : 20/08/2003
// DESCRIPTION 	: State for adjusting a journey route.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/TDCurrentAdjustState.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:00   mturner
//Initial revision.
//
//   Rev 1.6   Mar 22 2006 17:08:54   rhopkins
//Use alternative fix for FxCop
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Mar 22 2006 16:29:02   rhopkins
//Minor FxCop fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Mar 14 2006 08:41:38   build
//Automatically merged from branch for stream3353
//
//   Rev 1.3.1.0   Feb 16 2006 10:01:06   pcross
//Additional state to be saved in the adjust state object:
//MinimumTime;
//SelectedAdjustTimingsDropdownValue;
//SelectedAdjustLocationsDropdownValue
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Sep 09 2003 15:03:40   PNorell
//Removed commented out section of the OriginalJourneyRequest setter.
//
//   Rev 1.2   Sep 05 2003 15:28:50   passuied
//Deletion of TDLocation, LocationCodingType, OSGridReference and transfer to LocationService
//
//   Rev 1.1   Aug 29 2003 10:43:10   kcheung
//Updated made after TDTimeSearchType was replaced by a boolean.
//
//   Rev 1.0   Aug 27 2003 10:50:10   PNorell
//Initial Revision
using System;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// State for adjusting a journey route.
	/// </summary>
	[Serializable()]
	public class TDCurrentAdjustState
	{
		#region Constructors
		/// <summary>
		/// Creates the current adjusted-state and associates it with the current journey request
		/// </summary>
		/// <param name="request">The original request to be used for adjusting</param>
		public TDCurrentAdjustState( ITDJourneyRequest request)
		{
			originalJourneyRequest = request;
		}
		#endregion


		#region Properties
		private int journeyReferenceSequence;
		/// <summary>
		/// Gets/sets the highest journey sequence number that has been used
		/// </summary>
		public int JourneyReferenceSequence
		{
			get
			{
				return journeyReferenceSequence;
			}
			set
			{
				journeyReferenceSequence = value;
			}
		}


		private ITDJourneyRequest originalJourneyRequest = null;
		/// <summary>
		/// Gets the original journey request
		/// </summary>
		public ITDJourneyRequest OriginalJourneyRequest
		{
			get
			{
				return originalJourneyRequest;
			}
		}


		private ITDJourneyRequest amendedJourneyRequest = null;
		/// <summary>
		/// Gets/sets the amended journey request
		/// </summary>
		public ITDJourneyRequest AmendedJourneyRequest
		{
			get
			{
				return amendedJourneyRequest;
			}
			set
			{
				amendedJourneyRequest = value;
			}
		}


		private PublicJourney amendedJourney = null;
		/// <summary>
		/// Gets/sets the amended journey request
		/// </summary>
		public PublicJourney AmendedJourney
		{
			get
			{
				return amendedJourney;
			}
			set
			{
				amendedJourney = value;
			}
		}

		private TDAmendmentType currentAmendmentType = TDAmendmentType.OutwardJourney;
		/// <summary>
		/// Gets/sets the current amendment type (default is OutwardJourney)
		/// </summary>
		public TDAmendmentType CurrentAmendmentType
		{
			get
			{
				return currentAmendmentType;
			}
			set
			{
				currentAmendmentType = value;
			}
		}

		private JourneyStatusType requestStatus = JourneyStatusType.Idle;
		/// <summary>
		/// Gets/sets the current request status (default is Idle)
		/// </summary>
		public JourneyStatusType RequestStatus
		{
			get
			{
				return requestStatus;
			}
			set
			{
				requestStatus = value;
			}
		}

		private PublicJourneyDetail[] remainingRouteSegment;
		/// <summary>
		/// Gets/sets the current remaining route segments
		/// </summary>
		public PublicJourneyDetail[] RemainingRouteSegment
		{
			get
			{
				return remainingRouteSegment;
			}
			set
			{
				remainingRouteSegment = value;
			}
		}


		private int selectedOutwardJourney;
		/// <summary>
		/// Gets/sets the selected outward journey that is being amended
		/// </summary>
		public int SelectedOutwardJourney
		{
			get
			{
				return selectedOutwardJourney;
			}
			set
			{
				selectedOutwardJourney = value;
			}
		}

		private int selectedReturnJourney;
		/// <summary>
		/// Gets/sets the selected return journey that is being amended
		/// </summary>
		public int SelectedReturnJourney
		{
			get
			{
				return selectedReturnJourney;
			}
			set
			{
				selectedReturnJourney = value;
			}
		}

		private uint selectedRouteNode;
		/// <summary>
		/// Gets/sets the index of the selected route node 
		/// </summary>
		public uint SelectedRouteNode 
		{
			get
			{
				return selectedRouteNode ;
			}
			set
			{
				selectedRouteNode  = value;
			}
		}
		
		private bool selectedRouteNodeSearchType = true;
		/// <summary>
		/// Gets/sets the search type for the route node
		/// </summary>
		public bool SelectedRouteNodeSearchType 
		{
			get
			{
				return selectedRouteNodeSearchType ;
			}
			set
			{
				selectedRouteNodeSearchType  = value;
			}
		}

		private int minimumTime;
		/// <summary>
		/// Gets/sets the minimum time requested to stay at a location on journey adjust
		/// </summary>
		public int MinimumTime
		{
			get
			{
				return minimumTime;
			}
			set
			{
				minimumTime = value;
			}
		}

		private string selectedAdjustTimingsDropdownValue;
		/// <summary>
		/// Gets/sets the selected value property of the adjustTimings dropdown
		/// </summary>
		public string SelectedAdjustTimingsDropdownValue
		{
			get
			{
				return selectedAdjustTimingsDropdownValue;
			}
			set
			{
				selectedAdjustTimingsDropdownValue = value;
			}
		}

		private string selectedAdjustLocationsDropdownValue;
		/// <summary>
		/// Gets/sets the selected value property of the adjustLocations dropdown
		/// </summary>
		public string SelectedAdjustLocationsDropdownValue
		{
			get
			{
				return selectedAdjustLocationsDropdownValue;
			}
			set
			{
				selectedAdjustLocationsDropdownValue = value;
			}
		}

		#endregion
	}
}
