// ***********************************************
// NAME 		: TestDummySessionManager.cs
// AUTHOR 		: Andrew Toner
// DATE CREATED : 26/09/2003
// DESCRIPTION 	: 
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/TestDummySessionManager.cs-arc  $
//
//   Rev 1.2   Sep 14 2009 10:55:08   apatel
//Stop Information page changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.1   Oct 13 2008 16:46:14   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.0   Jun 20 2008 14:59:14   mmodi
//Updated for cycle journey
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:24:48   mturner
//Initial revision.
//
//   Rev 1.50   Dec 07 2006 14:38:00   build
//Automatically merged from branch for stream4240
//
//   Rev 1.49.1.0   Nov 19 2006 10:48:44   mmodi
//Added JourneyEmissionsPageState
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.49   Oct 06 2006 10:58:08   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.48.1.1   Oct 03 2006 10:36:56   mmodi
//Added IsFromNearestCarPark
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4208: Car Parking: New Search a car park journey goes to Car route input
//
//   Rev 1.48.1.0   Aug 14 2006 10:55:08   esevern
//Added FindCarParkPageState
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.48   Feb 10 2006 12:07:46   tolomolaiye
//Merge of stream 3180
using System;
using System.Collections;
using System.Data;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.TravelNews;
using TransportDirect.UserPortal.CostSearch;
using TransportDirect.UserPortal.CyclePlannerControl;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
	/// <summary>
	/// Summary description for TestDummySessionManager.
	/// </summary>
	public class TestDummySessionManager : IServiceFactory, ITDSessionManager
	{
		private TypeSafeDictionary formShift = new TypeSafeDictionary();
		//		private ITDSession session; old
		TestDummySession session;

		private TDUser user;
		private string username = string.Empty;
		private string password = string.Empty;
		private int cacheParam;
		private bool authenticated = false;

		// Local instansiations of the deferrable objects
		private Hashtable deferableObjects = new Hashtable();
		private TDSessionSerializer deferManager = new TDSessionSerializer();		

		private ValidationError validationError;

		private ItineraryManagerMode itineraryManagerMode;

		/// <summary>
		/// Creates a TestDummySessionManager for usage in the ServiceDiscovery
		/// </summary>
		public TestDummySessionManager( string SessionID )
		{
			session = new TestDummySession( SessionID );
		}
		#region Service Discovery Methods
		public object Get()
		{
			return this;
		}
		#endregion

		#region ITDSessionManager Methods

		public void SaveData()
		{
			TDSessionSerializer ser = new TDSessionSerializer();
			TDSessionPartition part = Partition;

			foreach( IKey key in deferableObjects.Keys )
			{				
				if( key is ITDSessionPartionable )
				{
					ser.SerializeSessionObjectAndSave( this.Session.SessionID, part , key, deferableObjects[key] );
				}
				else
				{
					ser.SerializeSessionObjectAndSave( this.Session.SessionID, key, deferableObjects[key] );
				}
			}
		}

		public TDUser CurrentUser
		{
			get
			{
				return null;
			}
			set
			{
				object obj = value;
			}		
		}

		public TDUser UnsavedNewUser
		{
			get
			{
				return null;
			}
			set 
			{
				user = value;
			}
		}

		public string UnsavedUsername
		{
			get
			{
				return username;
			}
			set 
			{
				username = value;
			}
		}

		public string UnsavedPassword
		{
			get
			{
				return password;
			}
			set 
			{
				password = value;
			}
		}

		public int CacheParam
		{
			get
			{
				return cacheParam;
			}
			set
			{
				cacheParam = value;
			}
		}

		public object[] StoredMapViewState
		{
			get { return null; }
			set { }
		}

		#region Internal Helper methods
		private object GetData(IKey key)
		{
			object val = deferableObjects[key];
			if( val == null )
			{
				val = deferManager.RetrieveAndDeserializeSessionObject( this.Session.SessionID, TDSessionPartition.TimeBased, key ); // TDSessionSerialiser
				if( val != null )
					deferableObjects[key] = val;
			}
			return val;
		}
		#endregion

		public ITDSessionManager GetSessionManagerForPartition( TDSessionPartition partition )
		{
			return null;
		}

		public string GetOneUseKey( OneUseKey key)
		{
			return string.Empty;
		}

		public void SetOneUseKey( OneUseKey key, string val )
		{
		}


		public bool InitialiseJourneyParameters(FindAMode mode)
		{
			switch (mode)
			{
				case FindAMode.Flight:
					TDSessionManager.Current.Partition = TDSessionPartition.TimeBased;
					FindPageState = FindPageState.CreateInstance(mode);
					JourneyParameters = new TDJourneyParametersFlight();
					break;
				case FindAMode.Fare:
					TDSessionManager.Current.Partition = TDSessionPartition.CostBased;
					FindPageState = FindPageState.CreateInstance(mode);
					JourneyParameters = new CostSearchParams();
					break;
				case FindAMode.TrunkCostBased:
					TDSessionManager.Current.Partition = TDSessionPartition.CostBased;
					FindPageState = FindPageState.CreateInstance(mode);
					JourneyParameters = new CostSearchParams();
					break;
				default:
					TDSessionManager.Current.Partition = TDSessionPartition.TimeBased;
					FindPageState = FindPageState.CreateInstance(mode);
					JourneyParameters = new TDJourneyParametersMulti();
					break;
			}	

			return true;
		}

		public bool InitialiseJourneyParametersPageStates(FindAMode mode, bool forceReset)
		{
			//initialise page state
			switch (mode)
			{
				case FindAMode.Flight:
					TDSessionManager.Current.Partition = TDSessionPartition.TimeBased;
					FindPageState = FindPageState.CreateInstance(mode);
					JourneyParameters = new TDJourneyParametersFlight();
					break;
				case FindAMode.Fare:
					TDSessionManager.Current.Partition = TDSessionPartition.CostBased;
					FindPageState = FindPageState.CreateInstance(mode);
					JourneyParameters = new CostSearchParams();
					break;
				case FindAMode.TrunkCostBased:
					TDSessionManager.Current.Partition = TDSessionPartition.CostBased;
					FindPageState = FindPageState.CreateInstance(mode);
					JourneyParameters = new CostSearchParams();
					break;
				default:
					TDSessionManager.Current.Partition = TDSessionPartition.TimeBased;
					FindPageState = FindPageState.CreateInstance(mode);
					JourneyParameters = new TDJourneyParametersMulti();
					break;
			}	
	
			return false;
			
		}

		public bool ResultsValidForParametersType(FindAMode mode)
		{
			return true;
		}

		public bool PreventVolatilePropertyDeferral 
		{
			get { return false; }
			set { return; }
		}

		public void ClearDeferredData() 
		{
		}

		public CJPSessionInfo GetSessionInformation()
		{
			return new CJPSessionInfo();
		}

		/// <summary>
		/// Read only property that gets the user's authentication status.
		/// </summary>
		/// <value>True if authenticated, false otherwise.</value>
		public bool Authenticated
		{
			get
			{
				return authenticated;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public JourneyMapState JourneyMapState 
		{
			get 
			{
				return new JourneyMapState();
			}
			set 
			{
				return;
			}
		}

		/// <summary>
		/// Get the business links state object
		/// </summary>
		public BusinessLinkState BusinessLinkState
		{
			get
			{
				return new BusinessLinkState();
			}

			set
			{
				return;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public JourneyMapState ReturnJourneyMapState 
		{
			get 
			{
				return new JourneyMapState();
			}
			set 
			{
				return;
			}
		}
		/// <summary>
		/// Read only property that gets the Formshift data for the current session manager.
		/// </summary>
		public TypeSafeDictionary FormShift
		{
			get
			{	
				return formShift;
			}
		}

		/// <summary>
		/// Read only property that gets the Session data for the current session manager.
		/// </summary>
		public ITDSession Session
		{
			get
			{	
				return session;
			}
		}


		/// <summary>
		/// Read/Write property. Gets/Sets the current ItineraryManager mode. Setting
		/// the mode will create a new required itinerary manager of the correct type
		/// if ones does not exist.
		/// </summary>
		public ItineraryManagerMode ItineraryMode
		{
			get 
			{
				return itineraryManagerMode;
			}
			set
			{
				//Create (if required) a new Itinerary Manager for the suppled
				//mode. If one already extists then dont do anything. Otherwise
				//create a new one using the ItineraryManagerFactory and store
				//it in the session manager.
				if (value != itineraryManagerMode)
				{
					ItineraryManagerFactory factory = new ItineraryManagerFactory();

					//Store the created itinerary manager directly onto deffered storage. This
					//avoids providing a set operator on the itinerary manager property.
					//SaveData(TDSessionManager.KeyItineraryManager,factory.Get(value));
					deferableObjects[TDSessionManager.KeyItineraryManager] = factory.Get(value);
					
					//Update private value of the mode.
					itineraryManagerMode = value;
				}
			}
		}


		/// <summary>
		/// Ready Only. Gets the current itinerary manager.
		/// </summary>
		public TDItineraryManager ItineraryManager
		{
			get
			{
				TDItineraryManager manager = GetData(TDSessionManager.KeyItineraryManager) as TDItineraryManager;
				if ( manager == null)
				{
					//manager = (TDItineraryManager)(new ItineraryManagerFactory().Get(ItineraryManagerMode.None ));
					manager = new ExtendItineraryManager();
				}
				return manager;
			}
		}


		/// <summary>
		/// Get/Set JourneyRequest
		/// </summary>
		public ITDJourneyRequest JourneyRequest
		{
			get
			{
				ITDJourneyRequest req = (ITDJourneyRequest)GetData( TDSessionManager.KeyJourneyRequest );
				if( req == null )
				{
					req = new TDJourneyRequest();
					JourneyRequest = req;
				}
				return req;
			}
			set
			{
				deferableObjects[TDSessionManager.KeyJourneyRequest] = value;
			}

		}


		/// <summary>
		/// Get/Set JourneyResult
		/// </summary>
		public ITDJourneyResult JourneyResult
		{
			get
			{
				return (ITDJourneyResult)GetData( TDSessionManager.KeyJourneyResult );
			}
			set
			{ 
				deferableObjects[TDSessionManager.KeyJourneyResult] = value;
			}
		}


        /// <summary>
        /// Get/Set CycleRequest
        /// </summary>
        public ITDCyclePlannerRequest CycleRequest
        {
            get
            {
                ITDCyclePlannerRequest req = (ITDCyclePlannerRequest)GetData(TDSessionManager.KeyCycleRequest);
                if (req == null)
                {
                    req = new TDCyclePlannerRequest();
                    CycleRequest = req;
                }
                return req;
            }
            set
            {
                deferableObjects[TDSessionManager.KeyCycleRequest] = value;
            }
        }

        /// <summary>
        /// Get/Set CycleResult
        /// </summary>
        public ITDCyclePlannerResult CycleResult
        {
            get
            {
                return (ITDCyclePlannerResult)GetData(TDSessionManager.KeyCycleResult);
            }
            set
            {
                deferableObjects[TDSessionManager.KeyCycleResult] = value;
            }
        }

		/// <summary>
		/// Get/Set AmendedJourneyResult
		/// </summary>
		public ITDJourneyResult AmendedJourneyResult
		{
			get
			{
				return (ITDJourneyResult)GetData( TDSessionManager.KeyAmendedJourneyResult );
			}
			set
			{ 
				deferableObjects[TDSessionManager.KeyAmendedJourneyResult] = value;
			}        
		}

		/// <summary>
		/// Get/Set JourneyViewState
		/// </summary>
		public TDJourneyViewState JourneyViewState
		{
			get
			{
				TDJourneyViewState mapState = (TDJourneyViewState)GetData( TDSessionManager.KeyJourneyViewState );
				if( mapState == null )
				{
					mapState = new TDJourneyViewState();
					JourneyViewState = mapState;
				}
				return mapState;
			}

			set
			{
				deferableObjects[TDSessionManager.KeyJourneyViewState] = value;
			}
		}


		/// <summary>
		/// Get/Set CurrentAdjustState
		/// </summary>
		public TDCurrentAdjustState CurrentAdjustState
		{
			get
			{
				return (TDCurrentAdjustState)GetData( TDSessionManager.KeyCurrentAdjustState );
			}
			set
			{ 
				deferableObjects[TDSessionManager.KeyCurrentAdjustState] = value;
			}        
		}
        
		/// <summary>
		/// Get/Set JourneyPlanControlData
		/// </summary>
		public AsyncCallState AsyncCallState
		{
			get
			{
				return (AsyncCallState)GetData( TDSessionManager.KeyAsyncCallState );
			}
			set
			{ 
				deferableObjects[TDSessionManager.KeyAsyncCallState] = value;
			}        
		}	
		
		public TDJourneyParameters JourneyParameters
		{
			get
			{
				TDJourneyParameters param = (TDJourneyParameters)GetData( TDSessionManager.KeyJourneyParameters );
				if( param == null )
				{
					param = new TDJourneyParametersMulti();
					JourneyParameters = param;
				}
				return param;
			}
			set
			{ 
				deferableObjects[TDSessionManager.KeyJourneyParameters] = value;
			}        
		}

		public InputPageState InputPageState
		{
			get
			{
				InputPageState pageState = (InputPageState)GetData( TDSessionManager.KeyInputPageState );
				if( pageState == null )
				{
					pageState = new InputPageState();
					InputPageState = pageState;
				}
				return pageState;
			}
			set
			{
				deferableObjects[TDSessionManager.KeyInputPageState] = value;
			}

		}

		/// <summary>
		/// Get/sets the results page state
		/// </summary>
		public ResultsPageState ResultsPageState
		{
			get
			{
				ResultsPageState pageState = (ResultsPageState)GetData( TDSessionManager.KeyResultsPageState );
				if( pageState == null )
				{
					pageState = new ResultsPageState();
					ResultsPageState = pageState;
				}
				return pageState;
			}
			set
			{
				deferableObjects[TDSessionManager.KeyResultsPageState] = value;
			}

		}

		public FindStationPageState FindStationPageState
		{
			get
			{
				FindStationPageState pageState = (FindStationPageState)GetData( TDSessionManager.KeyFindStationPageState );
				if( pageState == null )
				{
					pageState = new FindStationPageState();
					FindStationPageState = pageState;
				}
				return pageState;
			}
			set
			{
				deferableObjects[TDSessionManager.KeyFindStationPageState] = value;
			}
		}

		public FindCarParkPageState FindCarParkPageState
		{
			get
			{
				FindCarParkPageState pageState = (FindCarParkPageState)GetData( TDSessionManager.KeyFindCarParkPageState );
				if( pageState == null )
				{
					pageState = new FindCarParkPageState();
					FindCarParkPageState = pageState;
				}
				return pageState;
			}
			set
			{
				deferableObjects[TDSessionManager.KeyFindCarParkPageState] = value;
			}
		}

		public FindPageState FindPageState
		{
			get
			{
				return (FindPageState)GetData( TDSessionManager.KeyFindPageState );
			}
			set
			{
				deferableObjects[TDSessionManager.KeyFindPageState] = value;
			}
		}

		public PricingRetailOptionsState PricingRetailOptions
		{
			get{return null;}
			set{}
		}

		public AmbiguityResolutionState AmbiguityResolution
		{
			get{return null;}
			set{}
		}

		public TravelNewsState TravelNewsState
		{
			get{return null;}
			set{}
		}

		/// <summary>
		/// This is a read/write property for FindCostBasedPageState data in
		/// the ASP session data.
		/// </summary>
		public FindCostBasedPageState FindCostBasedPageState 
		{
			get 
			{
				return null;
			}
			set 
			{				
			}
		}

		
		/// <summary>
		/// Get the Find A mode
		/// </summary>
		public FindAMode FindAMode 
		{
			get 
			{
				return FindAMode.None;
			}

		}

		/// <summary>
		/// Returns true if the session is currently using a Find A function
		/// </summary>
		public bool IsFindAMode
		{
			get 
			{
				return false;
			}
		}

		/// <summary>
		/// Returns if the session is currently using a Find Nearest Car Parks function
		/// </summary>
		public bool IsFromNearestCarParks
		{
			get 
			{
				return false;
			}
			set
			{
			}
		}

        /// <summary>
        /// Returns true if page is in Stop Information mode
        /// </summary>
        public bool IsStopInformationMode
        {
            get
            {
                return false;
            }
            set
            {
                deferableObjects[TDSessionManager.KeyStopInformationMode] = value;
            }
        }


		/// <summary>
		/// Gets/Sets which tab is currently selected.
		/// This value is 'None' until it is actually set by the header control.
		/// If the TabSectionChangeable property is set to false then setting
		/// this property has no effect. 
		/// </summary>
		public TabSection TabSection
		{
			get
			{
				return TabSection.Home;
			}
			set
			{
			}
		}

		/// <summary>
		/// Sets the tab currently selected irrespective of whether the
		/// TabSectionChangeable property has been set to false.
		/// </summary>
		public TabSection NewTabSection
		{
			set
			{
			}
		}

		/// <summary>
		/// Gets/Sets which tab is currently selected
		/// This value is 'None' until it is actually set by the header control
		/// </summary>
		public bool TabSectionChangeable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>
		/// Gets/Sets whether the user survey form has been shown in this session		
		/// </summary>
		public bool UserSurveyAlreadyShown
		{
			get
			{
				return false;
			}
			set
			{
			}
		}



		/// <summary>
		/// Get/Set ValidationError
		/// </summary>
		public ValidationError ValidationError
		{
			get
			{
				return validationError;
			}
			set
			{
				validationError = value;
			}
		}

		/// <summary>
		/// Get property to access the SessionID.
		/// </summary>
		public string SessionID
		{
			get { return session.SessionID; }
		}

		public bool HasTimeBasedJourneyResults
		{
			get {			return true; }
		}

		public bool HasCostBasedJourneyResults
		{
			get {			return true; }
		}

		public bool HasCostBasedFaresResults
		{
			get {			return true; }
		}
		
		public FindAMode TimeBasedFindAMode 
		{
			get 
			{
				return FindAMode.None;
			}
		}

		public TDSessionPartition Partition
		{
			get { return session.Partition; }
			set 
			{ 
				session.Partition = value; 
				// Cleanup any deferred data for partitions - this forces it to refetchning data
				// Both non-partitioned and partitioned data will be refetched when asked for
				deferableObjects.Clear();
				// The session data is handled automatically and should not be cleared.

			}
		}

		public FindAMode CostBasedFindAMode 
		{ 
			get {return FindAMode.None;}
		}

		public JourneyEmissionsPageState JourneyEmissionsPageState
		{
			get
			{
				JourneyEmissionsPageState pageState = (JourneyEmissionsPageState)GetData( TDSessionManager.KeyJourneyEmissionsPageState );
				if( pageState == null )
				{
					pageState = new JourneyEmissionsPageState();
					JourneyEmissionsPageState = pageState;
				}
				return pageState;
			}
			set
			{
				deferableObjects[TDSessionManager.KeyJourneyEmissionsPageState] = value;
			}
		}

		#endregion

		#region Lifecycle Events
		
		/// <summary>
		/// OnLoad event executes the first time the SessionManager 
		/// is requested via the property 'Current'
		/// </summary>
		public void OnLoad()
		{
		}
		
		/// <summary>
		/// OnFormShift event executes when shift of a form has occurred
		/// but only after the new page's OnLoad event has executed.
		/// </summary>
		public void OnFormShift()
		{
		}

		/// <summary>
		/// OnPreUnload event executes when the page renders.
		/// </summary>
		public void OnPreUnload()
		{
		}

		/// <summary>
		/// OnUnload is the last event to occur and any outside access 
		/// should be avoided at this point.
		/// </summary>
		public void OnUnload()
		{
		}
		#endregion

	}
}