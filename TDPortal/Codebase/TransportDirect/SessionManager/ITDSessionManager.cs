// ***********************************************
// NAME 		: ITDSessionManager.cs
// AUTHOR 		: Mark Turner
// DATE CREATED : 02/07/2003
// DESCRIPTION 	: An interface that lists the Properties and 
// methods that must be provided by the TDSessionManager class.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/ITDSessionManager.cs-arc  $
//
//   Rev 1.2   Sep 14 2009 10:31:46   apatel
//Stop Information page related changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.1   Oct 13 2008 16:46:38   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.0   Jun 20 2008 14:48:54   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:48:30   mturner
//Initial revision.
//
//   Rev 1.58   Dec 07 2006 14:37:56   build
//Automatically merged from branch for stream4240
//
//   Rev 1.57.1.0   Nov 16 2006 17:35:12   mmodi
//Added JourneyEmissionsPageState
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.57   Oct 06 2006 13:36:34   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.56.1.1   Oct 03 2006 10:24:02   mmodi
//Added IsFromNearestCarParks property
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4208: Car Parking: New Search a car park journey goes to Car route input
//
//   Rev 1.56.1.0   Aug 14 2006 11:02:00   esevern
//added FindCarParkPageState property
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.56   Feb 10 2006 12:07:48   tolomolaiye
//Merge of stream 3180
//
//   Rev 1.55   Dec 13 2005 11:39:32   asinclair
//Merge for stream3143
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.54.2.2   Jan 10 2006 16:36:36   tmollart
//Updated after code review comments.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.54.2.1   Dec 22 2005 09:33:32   tmollart
//Removed reference to OldJourneyParameters.
//Removed redundant methods:
//- SaveCurrentFindaMode
//- SaveCurrentFindaMode(overloaded)
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.54.2.0   Dec 12 2005 17:34:04   tmollart
//Removed InitialiseJourneyParametersPageStates and replaced with InitialiseJourneyParameters.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.54.1.0   Nov 22 2005 16:48:08   tolomolaiye
//Added Business Links
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.54   Nov 09 2005 18:17:22   RPhilpott
//Merge of stream2818 - corrections.
//
//   Rev 1.53   Nov 09 2005 15:12:40   RPhilpott
//Merage for stream 2818.
//
//   Rev 1.52   Nov 01 2005 15:12:16   build
//Automatically merged from branch for stream2638
//
//   Rev 1.51.1.1   Sep 27 2005 14:05:08   jbroome
//Added ResultsPageState property
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.51.1.0   Sep 13 2005 09:42:58   tmollart
//Updated signature for  ItineraryManager.
//Added new signature for ItineraryMode.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.51   Jul 05 2005 13:51:18   asinclair
//Merge for stream2557
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.50.1.0   Jun 15 2005 12:26:58   asinclair
//Added GetSessionInformation 
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.50   May 17 2005 14:22:52   PNorell
//Updated for code-review
//Resolution for 1954: Dev Code Review: PT - Session Partitioning
//
//   Rev 1.49   Apr 20 2005 11:21:40   tmollart
//Implemented CostBasedFindMode method.
//Resolution for 2147: PT - Cost based city to city planner automatically jumps to previously planned find a fare results when selected.
//
//   Rev 1.48   Apr 01 2005 16:47:44   tmollart
//Implemented HasCostBasedFaresResults property.
//
//   Rev 1.47   Mar 24 2005 15:49:26   COwczarek
//Add TimeBasedFindAMode property.
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.46   Mar 15 2005 08:40:52   tmollart
//Implemented CostSearchWaiteControlData and CostSearchWaitStateData properties.
//
//   Rev 1.45   Mar 10 2005 15:24:36   COwczarek
//Add ClearDeferredData method that clears the deferred data cache
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.44   Jan 27 2005 12:20:50   jmorrissey
//Removed FindCostBasedPageState after change to design
//
//   Rev 1.43   Jan 26 2005 15:53:06   PNorell
//Support for partitioning the session information.
//
//   Rev 1.42   Jan 06 2005 11:02:40   jmorrissey
//Added FindCostBasedPageState, that holds all session data pertaining to cost based searching
//
//   Rev 1.41   Oct 15 2004 12:31:24   jgeorge
//Added JourneyPlanStateData and modifications to the serialization process.
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.40   Oct 08 2004 12:23:08   jmorrissey
//Change for User Survey functionality.
//
//   Rev 1.39   Sep 23 2004 13:36:04   passuied
//updated SessionManager Interface
//Resolution for 1626: Extend Journey: inconsistency when using header tabs and coming back
//
//   Rev 1.38   Sep 06 2004 21:08:30   JHaydock
//Major update to travel news
//
//   Rev 1.37   Aug 19 2004 13:15:46   COwczarek
//Add NewTabSection property
//Resolution for 1318: When you submit a Find  a journey the Journey Planner header displayed
//
//   Rev 1.36   Aug 17 2004 08:55:36   COwczarek
//Add SaveCurrentFindAMode method and OldFindAMode property
//Resolution for 1306: Find a Train extends a journey incorrectly.
//
//   Rev 1.35   Aug 06 2004 14:45:02   JHaydock
//Added TabSelection methods to TDSessionManager which is updated by each individual header control's load method and used within the HelpFullJP page to display the correct header on help pages.
//
//   Rev 1.34   Aug 03 2004 11:49:46   COwczarek
//Add IsFindAMode and FindAMode
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.33   Jul 22 2004 15:30:14   jgeorge
//Find a... updates
//
//   Rev 1.32   Jul 14 2004 13:01:26   passuied
//Changes in SessionManager for Del6.1. Compiles
//
//   Rev 1.31   Jul 13 2004 15:48:32   rgreenwood
//Added CacheParam for IR1063
//
//   Rev 1.30   Jun 28 2004 14:11:16   jgeorge
//Updated ChangeJourneyParametersType and added ResultsValidForParametersType
//
//   Rev 1.29   Jun 24 2004 14:47:46   jgeorge
//Added ChangeJourneyParametersType method
//
//   Rev 1.28   May 13 2004 12:39:42   jgeorge
//Added FindFlightPageState
//
//   Rev 1.27   May 10 2004 17:02:14   passuied
//added FindStationPageState
//
//   Rev 1.26   May 10 2004 14:57:42   RHopkins
//Extend Journey.
//Initial version of Itinerary Manager.
//
//   Rev 1.25   Apr 28 2004 16:19:48   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.24   Mar 26 2004 10:05:48   COwczarek
//Fix comment
//Resolution for 662: Back Button from the Ambiguity Page does not unconfirm a confirmed location
//
//   Rev 1.23   Mar 26 2004 10:01:26   COwczarek
//Add AmbiguityResolution property
//Resolution for 662: Back Button from the Ambiguity Page does not unconfirm a confirmed location
//
//   Rev 1.22   Mar 16 2004 09:26:12   PNorell
//Support for outward and return map added.
//
//   Rev 1.21   Mar 10 2004 18:52:16   PNorell
//Added another map-view state to indicate return journeys.
//
//   Rev 1.20   Mar 10 2004 15:53:14   PNorell
//Updated for IR498 to be included in 5.2
//
//   Rev 1.19   Mar 01 2004 15:45:58   CHosegood
//Added JourneyMapState
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.18   Feb 12 2004 15:05:04   esevern
//DEL5.2 - seperation of login and registration
//
//   Rev 1.17   Jan 21 2004 11:35:08   PNorell
//Updates for 5.2
//
//   Rev 1.16   Oct 21 2003 17:32:58   JMorrissey
//updated property TravelNewsData which is now of type TravelNewsState
//
//   Rev 1.15   Oct 17 2003 16:13:34   JMorrissey
//added ITDSessionManager.TravelNewsData property
//
//   Rev 1.14   Oct 10 2003 10:05:12   COwczarek
//Added PricingRetailOptions property
//
//   Rev 1.13   Oct 01 2003 15:36:04   AToner
//Added the ClaimData object
//
//   Rev 1.12   Sep 18 2003 12:05:56   passuied
//Changed to follow design + Initialisation
//
//   Rev 1.11   Sep 18 2003 11:05:58   PNorell
//Fixed interfaces/concrete impl  to use interfaces for external methods.
//Updated SessionManager lifecycle
//Corrected spelling
//Corrected code according to the DD document
//
//   Rev 1.10   Sep 17 2003 16:19:50   PNorell
//Updated to support proper deferred storage.
//
//   Rev 1.9   Sep 17 2003 11:38:28   cshillan
//First implementation of JourneyPlanRunner
//Design doc ref: DV/DD014 Data Capture - Validate and Run
//
//   Rev 1.8   Sep 10 2003 11:52:08   cshillan
//Change ValidationError to an array
//
//   Rev 1.7   Sep 10 2003 11:29:48   cshillan
//Included ValidationError.cs in the SessionManager project
//
//   Rev 1.6   Sep 09 2003 14:16:00   passuied
//make the solution compile!
//
//   Rev 1.5   Sep 09 2003 13:22:34   cshillan
//Included JourneyPlanControlData and JPCallStatus
//
//   Rev 1.4   Aug 27 2003 16:53:46   kcheung
//Added TDJourneyViewState and CurrentAdjustState properties
//
//   Rev 1.3   Aug 26 2003 10:03:58   passuied
//update
//
//   Rev 1.2   Jul 17 2003 15:32:32   mturner
//Documentation comments added after code review
//
//   Rev 1.1   Jul 07 2003 17:21:56   AWindley
//Added property ITDSession
//
//   Rev 1.0   Jul 03 2003 17:31:26   AWindley
//Initial Revision

using System;
using System.Data;

using TransportDirect.Common;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.TravelNews;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// The ITDSessionManager interface is implemented by the TDSessionManager class located
	/// in TDSessionManager.cs
	/// </summary>
	[CLSCompliant(false)]
	public interface ITDSessionManager 
	{

		#region Public methods
		
		/// <summary>
		/// Gets a session value that is only around for one page-request.
		/// </summary>
		/// <param name="key">The key to set</param>
		/// <returns>The value or null if no value exists</returns>
		string GetOneUseKey( OneUseKey key);

		/// <summary>
		/// Sets session value that is only around for one page-request.
		/// If the value is null, then the key-value is removed.
		/// </summary>
		/// <param name="key">The key</param>
		/// <param name="val">The value to be set or null to remove</param>
		void SetOneUseKey( OneUseKey key, string val );

		/// <summary>
		/// Initialises the journey parameters for the supplied mode. Checks if
		/// a reset of the parameters is needed. Contains exceptions for some 
		/// mode combinations.
		/// </summary>
		/// <param name="mode">Requested new mode.</param>
		/// <returns>If a reset of the parameters took place.</returns>
		bool InitialiseJourneyParameters(FindAMode mode);

		/// <summary>
		/// Forces the session manager to save all data outside the normal lifecycle.
		/// </summary>
		void SaveData();

		/// <summary>
		/// Clears the deferred data cache
		/// </summary>
		void ClearDeferredData();

		/// <summary>
		/// Creates a read only session-manager for a given partition.
		/// </summary>
		/// <param name="partition">The partition to be used</param>
		/// <returns>The read only session manager</returns>
		ITDSessionManager GetSessionManagerForPartition( TDSessionPartition partition );

		/// <summary>
		/// This creates and returns a new instance of CJPSessionInfo with the current session information.
		/// </summary>
		/// <returns>A new instance of CJPSessionInfo</returns>
		CJPSessionInfo GetSessionInformation();
		
		#endregion

		#region Properties

		/// <summary>
		/// Get/Set property to access the Itinerary Manager.
		/// </summary>
		TDItineraryManager ItineraryManager{ get;}

		/// <summary>
		/// Get/Set property to access the Journey request.
		/// </summary>
		ITDJourneyRequest JourneyRequest{get;set;}

		/// <summary>
		/// Get/Set property to access the Journey results.
		/// </summary>
		ITDJourneyResult JourneyResult{get;set;}

		/// <summary>
		/// Get/Set property to access the amended journey results.
		/// </summary>
		ITDJourneyResult AmendedJourneyResult{get;set;}

		/// <summary>
		/// Get/Set property to access the Journey View State.
		/// </summary>
		TDJourneyViewState JourneyViewState { get; set; }
		
		/// <summary>
		/// Get/Set property to access the Current Adjust State.
		/// </summary>
		TDCurrentAdjustState CurrentAdjustState { get; set; }

		/// <summary>
		/// Get/Set property to access the AsyncCallState.
		/// </summary>
		AsyncCallState AsyncCallState { get; set; }

		/// <summary>
		/// Get/Set property to access the Journey Map State for outward or unspecified journeys.
		/// </summary>
		JourneyMapState JourneyMapState {get; set;}

		/// <summary>
		/// Get/Set property to access the Journey Map State for return journeys only.
		/// </summary>
		JourneyMapState ReturnJourneyMapState {get; set;}

		/// <summary>
		/// Get/Set property to access the Validation Error.
		/// </summary>
		ValidationError ValidationError { get; set; }

        /// <summary>
        /// Get/Set property to access the Cycle journey request.
        /// </summary>
        ITDCyclePlannerRequest CycleRequest { get;set;}

        /// <summary>
        /// Get/Set property to access the Cycle journey results.
        /// </summary>
        ITDCyclePlannerResult CycleResult { get;set;}

		/// <summary>
		/// Read only property that returns a bool with a value of true if the user is 
		/// Authenticated or false otherwise.
		/// </summary>
		bool Authenticated{get;}

		/// <summary>
		/// Retreives (or sets) the user if authenticated, otherwise returns null.
		/// </summary>
		TDUser CurrentUser{get;set;}

		/// <summary>
		/// Retrieves (or sets) the unsaved new user.
		/// </summary>
		TDUser UnsavedNewUser{get; set;}

		/// <summary>
		/// Retrieves (or sets) the unsaved new username.
		/// </summary>
		string UnsavedUsername{get; set;}

		/// <summary>
		/// Retrieves (or sets) the unsaved new user password.
		/// </summary>
		string UnsavedPassword{get; set;}

		/// <summary>
		/// Read only property that returns the FormShift data of the current session.
		/// </summary>
		TypeSafeDictionary FormShift{get;}

		/// <summary>
		/// Read only property that returns the SessionID of the current session.
		/// </summary>
		ITDSession Session{get;}

		/// <summary>
		/// Retreives (or sets) the CacheParameter for the page
		/// </summary>
		int CacheParam{get; set;}

		/// <summary>
		/// Gets/sets the journey parameters used to create a journey request
		/// </summary>
		TDJourneyParameters JourneyParameters
		{
			get;
			set;
		}

		/// <summary>
		/// Get/sets the input page state.
		/// </summary>
		InputPageState InputPageState
		{
			get;
			set;
		}

		/// <summary>
		/// Get/sets the find station page-state
		/// </summary>
		FindStationPageState FindStationPageState
		{
			get;
			set;
		}

		/// <summary>
		/// Get/sets the find nearest car parks page-state
		/// </summary>
		FindCarParkPageState FindCarParkPageState
		{
			get;
			set;
		}

		/// <summary>
		/// Get/sets the finder page-state
		/// </summary>
		FindPageState FindPageState
		{
			get;
			set;
		}

		/// <summary>
		/// Gets/sets the results page state
		/// </summary>
		ResultsPageState ResultsPageState
		{
			get;
			set;
		}

		/// <summary>
		/// Get/Set property to access the Pricing and Retail user options.
		/// </summary>
		PricingRetailOptionsState PricingRetailOptions
		{
			get;
			set;
		}

		/// <summary>
		/// Holds journey parameter state that may require reinstating if ambiguity decisions
		/// are backed out
		/// </summary>
		AmbiguityResolutionState AmbiguityResolution
		{
			get;
			set;
		}

		/// <summary>
		/// Get/Set property to access the TravelNews DataSet
		/// </summary>
		TravelNewsState TravelNewsState
		{
			get;
			set;
		}

		/// <summary>
		/// Get/set the stored map viewstate
		/// </summary>
		object[] StoredMapViewState
		{
			get;
			set;
		}

		/// <summary>
		/// Get the Find A mode
		/// </summary>
		FindAMode FindAMode 
		{
			get;
		}

		/// <summary>
		/// Returns true if the session is currently using a Find A function
		/// </summary>
		bool IsFindAMode
		{
			get;
		}

		/// <summary>
		/// Returns if the session is currently using the Find Nearest Car Parks function
		/// </summary>
		bool IsFromNearestCarParks
		{
			get;
			set;
		}

        /// <summary>
        /// Returns true if the page transited from the Stop Information page
        /// </summary>
        bool IsStopInformationMode
        {
            get;
            set;
        }

		/// <summary>
		/// Gets/Sets which tab is currently selected.
		/// This value is 'None' until it is actually set by the header control.
		/// If the TabSectionChangeable property is set to false then setting
		/// this property has no effect. 
		/// </summary>
		TabSection TabSection
		{
			get;
			set;
		}

		/// <summary>
		/// Sets the tab currently selected irrespective of whether the
		/// TabSectionChangeable property has been set to false.
		/// </summary>
		TabSection NewTabSection
		{
			set;
		}

		/// <summary>
		/// Gets/Sets which tab is currently selected
		/// This value should be 'None' until it is actually set by the header control
		/// </summary>
		bool TabSectionChangeable
		{
			get;
			set;
		}

		/// <summary>
		/// Gets/Sets whether the user survey form has been shown in this session
		/// </summary>
		bool UserSurveyAlreadyShown
		{
			get;
			set;
		}			

		/// <summary>
		/// Get/set property controlling which partition is currently in use.
		/// </summary>
		TDSessionPartition Partition { get; set; }

		/// <summary>
		/// Checks regardless of partition if there is timebased journey results available
		/// </summary>
		/// <returns>True if there are timebased journey results that are ok</returns>
		bool HasTimeBasedJourneyResults { get; }

		/// <summary>
		/// Checks regardless of partition if there is a timebased journey results available
		/// </summary>
		/// <returns>True if there are costbased journey results that are ok</returns>
		bool HasCostBasedJourneyResults { get; }

		/// <summary>
		/// Checks regardless of partition if any valid travel dates exist for
		/// current cost search results.
		/// </summary>
		bool HasCostBasedFaresResults{ get; }

		/// <summary>
		/// Read only property that returns the Find A mode used to plan the results held in the 
		/// time based session partition. If an itinerary exists, the Find A mode for the initial
		/// journey is returned.
		/// </summary>
		FindAMode TimeBasedFindAMode { get; }

		/// <summary>
		/// Read only property that returns the Find A mode used to plan the results held in the 
		/// cost based session partition. If an itinerary exists, the Find A mode for the initial
		/// journey is returned.
		/// </summary>
		FindAMode CostBasedFindAMode { get; }


		/// <summary>
		/// Read/Write property. Gets/Sets the current ItineraryManager mode. Setting
		/// the mode will create a new required itinerary manager of the correct type
		/// if ones does not exist.
		/// </summary>
		ItineraryManagerMode ItineraryMode{ get; set; }

		BusinessLinkState BusinessLinkState{get; set;}

		/// <summary>
		/// Get/sets the Journey emissions page-state
		/// </summary>
		JourneyEmissionsPageState JourneyEmissionsPageState
		{
			get;
			set;
		}

		#endregion
	
		#region LifeCycle Events

		/// <summary>
		/// OnLoad event executes the first time the SessionManager 
		/// is requested via the property 'Current'
		/// </summary>
		void OnLoad();

		/// <summary>
		/// OnFormShift event executes when shift of a form has occurred
		/// but only after the new page's OnLoad event has executed.
		/// </summary>
		/// </summary>
		void OnFormShift();
		
		/// <summary>
		/// OnPreUnload event executes when the page renders.
		/// </summary>
		void OnPreUnload();

		/// <summary>
		/// OnUnload is the last event to occur and any outside access 
		/// should be avoided at this point.
		/// </summary>
		void OnUnload();

		#endregion
	}
}