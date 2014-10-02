// ***********************************************
// NAME 		: TestMockSessionManager.cs
// AUTHOR 		: Peter Norell
// DATE CREATED : 18/08/2003
// DESCRIPTION 	: Handles the life-cycle and allocation of MockSessionManager objects
// through the service discovery when using NUnit.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/test/TestMockSessionManager.cs-arc  $
//
//   Rev 1.2   Sep 14 2009 10:31:46   apatel
//Stop Information page related changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.1   Oct 13 2008 16:46:40   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.0   Jun 20 2008 14:46:42   mmodi
//Updated to expose cycle results
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:49:02   mturner
//Initial revision.
//
//   Rev 1.65   Dec 07 2006 14:37:54   build
//Automatically merged from branch for stream4240
//
//   Rev 1.64.1.0   Nov 16 2006 17:33:28   mmodi
//Added JourneyEmissionsPageState
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.64   Oct 06 2006 13:42:56   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.63.1.1   Oct 03 2006 10:26:40   mmodi
//Added IsFromNearestCarParks
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4208: Car Parking: New Search a car park journey goes to Car route input
//
//   Rev 1.63.1.0   Aug 10 2006 14:08:10   esevern
//added FindCarParkPageState
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.63   Mar 14 2006 11:26:08   tmollart
//Manual merge of stream 3353.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.62   Feb 10 2006 12:07:52   tolomolaiye
//Merge of stream 3180
//
//   Rev 1.61   Dec 13 2005 11:41:16   asinclair
//Merge for stream3143
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.60.1.0   Nov 22 2005 16:45:22   tolomolaiye
//Added Business link state
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.60.2.1   Dec 22 2005 10:13:30   tmollart
//General updates.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.60.2.0   Dec 12 2005 17:22:52   tmollart
//Updated to reflect changes in Session Manager. Removed InitialiseJourneyParametersPageStates and replaced with InitialiseJourneyParameters.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.60   Nov 09 2005 18:58:26   RPhilpott
//Merge with stream2818 - corrections.
//
//   Rev 1.59   Nov 09 2005 16:11:58   RPhilpott
//Merge for stream2818
//
//   Rev 1.58   Nov 01 2005 15:12:12   build
//Automatically merged from branch for stream2638
//
//   Rev 1.57.1.5   Oct 10 2005 18:03:22   tmollart
//Updated GetSessionInformation method to return session information.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.57.1.4   Sep 27 2005 14:32:36   jbroome
//Previous check-in incorrect
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.57.1.2   Sep 27 2005 08:56:02   pcross
//Add reference to ValidationError class in session
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.57.1.1   Sep 21 2005 18:46:14   pcross
//Updates to support unit testing
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.57.1.0   Sep 13 2005 10:18:30   tmollart
//Modifications for VisitPlanner.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.57   Jul 05 2005 13:53:18   asinclair
//Merge for Stream2557
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.56.1.0   Jun 15 2005 12:19:08   asinclair
//Updated to use GetSessionInformation()
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.56   May 17 2005 14:22:56   PNorell
//Updated for code-review
//Resolution for 1954: Dev Code Review: PT - Session Partitioning
//
//   Rev 1.55   Apr 20 2005 11:21:42   tmollart
//Implemented CostBasedFindMode method.
//Resolution for 2147: PT - Cost based city to city planner automatically jumps to previously planned find a fare results when selected.
//
//   Rev 1.54   Apr 01 2005 16:44:16   tmollart
//Added HasCostBasedFaresResults method.
//
//   Rev 1.53   Mar 24 2005 15:49:28   COwczarek
//Add TimeBasedFindAMode property.
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.52   Mar 21 2005 13:41:56   jgeorge
//Added extra functionality to facilitate testing
//
//   Rev 1.51   Mar 15 2005 08:41:40   tmollart
//Implemented CostSearchWaitControlData and CostSearchWaitStateData properties.
//
//   Rev 1.50   Mar 10 2005 15:24:20   COwczarek
//Add ClearDeferredData method that clears the deferred data cache
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.49   Feb 04 2005 11:25:02   RScott
//Updated GetData method to use the simpleSession
//
//   Rev 1.48   Feb 02 2005 17:11:14   RScott
//Updated OnPreUnload and SaveData methods so that deferred data population is actually tested.
//
//   Rev 1.47   Jan 31 2005 16:50:30   PNorell
//Changes for SessionManager to include support for getting a sessionmanager for opposing partition.
//Also updated cost based check.
//
//   Rev 1.46   Jan 26 2005 15:53:14   PNorell
//Support for partitioning the session information.
//
//   Rev 1.45   Jan 07 2005 15:53:02   jmorrissey
//Added FindCostBasedPageState property
//
//   Rev 1.44   Oct 15 2004 12:32:06   jgeorge
//Added JourneyPlanStateData and PreventVolatileObjectDeferral.
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.43   Oct 08 2004 12:23:30   jmorrissey
//Change for User Survey functionality.
//
//   Rev 1.42   Sep 23 2004 13:36:10   passuied
//updated SessionManager Interface
//Resolution for 1626: Extend Journey: inconsistency when using header tabs and coming back
//
//   Rev 1.41   Sep 06 2004 21:08:56   JHaydock
//Major update to travel news
//
//   Rev 1.40   Aug 19 2004 13:15:02   COwczarek
//Add NewTabSection property
//Resolution for 1318: When you submit a Find  a journey the Journey Planner header displayed
//
//   Rev 1.39   Aug 17 2004 09:09:30   COwczarek
//Add SaveCurrentFindAMode method and OldFindAMode property
//Resolution for 1306: Find a Train extends a journey incorrectly.
//
//   Rev 1.38   Aug 06 2004 14:45:02   JHaydock
//Added TabSelection methods to TDSessionManager which is updated by each individual header control's load method and used within the HelpFullJP page to display the correct header on help pages.
//
//   Rev 1.37   Aug 03 2004 11:48:58   COwczarek
//Add interface implementations for IsFindAMode and FindAMode
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.36   Jul 22 2004 15:29:58   jgeorge
//Find a... updates
//
//   Rev 1.35   Jul 14 2004 13:01:30   passuied
//Changes in SessionManager for Del6.1. Compiles
//
//   Rev 1.34   Jul 13 2004 15:57:46   rgreenwood
//Added CacheParam for IR1063
//
//   Rev 1.33   Jun 28 2004 14:11:34   jgeorge
//Added ResultsValidForParametersType
//
//   Rev 1.32   Jun 24 2004 14:48:00   jgeorge
//Added ChangeJourneyParametersType method
//
//   Rev 1.31   Jun 15 2004 14:37:32   COwczarek
//Create ItineraryManager if not deserialized
//Resolution for 867: Add extend journey functionality to summary and details pages
//
//   Rev 1.30   May 13 2004 12:39:50   jgeorge
//Added FindFlightPageState
//
//   Rev 1.29   May 10 2004 17:02:42   passuied
//added FindStationPageState
//
//   Rev 1.28   May 10 2004 15:11:26   RHopkins
//Extend Journey.
//Initial version of Itinerary Manager.
//
//   Rev 1.27   Apr 28 2004 16:19:52   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.26   Mar 26 2004 10:08:00   COwczarek
//Add AmbiguityResolution property
//Resolution for 662: Back Button from the Ambiguity Page does not unconfirm a confirmed location
//
//   Rev 1.25   Mar 16 2004 09:26:22   PNorell
//Support for outward and return map added.
//
//   Rev 1.24   Mar 10 2004 18:52:28   PNorell
//Added another map-view state to indicate return journeys.
//
//   Rev 1.23   Mar 10 2004 15:53:20   PNorell
//Updated for IR498 to be included in 5.2
//
//   Rev 1.22   Mar 01 2004 15:47:20   CHosegood
//Added JourneyMapState
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.21   Feb 12 2004 15:05:06   esevern
//DEL5.2 - seperation of login and registration
//
//   Rev 1.20   Jan 21 2004 13:26:40   PNorell
//Updates for 5.2
//
//   Rev 1.19   Oct 21 2003 17:31:56   JMorrissey
//updated property TravelNewsData which is now of type TravelNewsState
//
//   Rev 1.18   Oct 17 2003 16:14:50   JMorrissey
//added test for TDSessionManager.TravelNewsData property
//
//   Rev 1.17   Oct 10 2003 09:57:08   COwczarek
//Implement PricingRetailOptions in ITDSessionManager to allow compilation
//
//   Rev 1.16   Oct 01 2003 15:36:06   AToner
//Added support for the ClaimData object
//
//   Rev 1.15   Sep 18 2003 12:05:56   passuied
//Changed to follow design + Initialisation
//
//   Rev 1.14   Sep 18 2003 11:06:00   PNorell
//Fixed interfaces/concrete impl  to use interfaces for external methods.
//Updated SessionManager lifecycle
//Corrected spelling
//Corrected code according to the DD document
//
//   Rev 1.13   Sep 17 2003 16:19:54   PNorell
//Updated to support proper deferred storage.
//
//   Rev 1.12   Sep 17 2003 11:38:32   cshillan
//First implementation of JourneyPlanRunner
//Design doc ref: DV/DD014 Data Capture - Validate and Run
//
//   Rev 1.11   Sep 12 2003 09:51:28   jcotton
//To resolve compilation errors on main solutions
//
//   Rev 1.10   Sep 11 2003 12:05:36   jcotton
//Added bool HasRealSession to constructor to enable the test to use a real session object or a simple mock session object
//
//   Rev 1.9   Sep 11 2003 11:34:04   jcotton
//Test for saving Deferred data to session database. Code compiles but unit testing is still in progress.  Checkin is to enable solution syncronisation.
//
//   Rev 1.8   Sep 11 2003 11:04:26   cshillan
//Included SessionID property with GET accessor
//
//   Rev 1.7   Sep 11 2003 10:52:22   jcotton
//Additions for deferred data testing
//
//   Rev 1.6   Sep 10 2003 11:52:08   cshillan
//Change ValidationError to an array
//
//   Rev 1.5   Sep 10 2003 11:29:48   cshillan
//Included ValidationError.cs in the SessionManager project
//
//   Rev 1.4   Sep 09 2003 14:16:06   passuied
//make the solution compile!
//
//   Rev 1.3   Aug 27 2003 16:54:00   kcheung
//Added ViewState to comply with the updated interface.
//
//   Rev 1.2   Aug 26 2003 10:04:04   passuied
//update
//
//   Rev 1.1   Aug 20 2003 15:51:46   PNorell
//Fixed header
using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.SessionState;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.TravelNews;
using TransportDirect.UserPortal.CyclePlannerControl;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Summary description for MockSessionManager.
	/// </summary>
	[CLSCompliant(false)]	
	public class TestMockSessionManager : IServiceFactory, ITDSessionManager
	{

		public const string NULL_UNSAVED = "::null_unsaved::";

		private bool authenticated = true;
		private TypeSafeDictionary formShift = new TypeSafeDictionary();
		private ITDSession session;
		private TestMockSimpleSession simpleSession;
		private TDUser user = null;
		private string username = string.Empty;
		private string password = string.Empty;
		private int cacheParam;
		private TDSessionPartition partition = TDSessionPartition.TimeBased;
		/// <summary>
		/// Used to keep track of the current itinerary manager mode
		/// </summary>
		private ItineraryManagerMode itineraryManagerMode;

		//private string sessionID;

		// Local instansiations of the deferrable objects

		private Hashtable deferableObjects = new Hashtable();
		private TDSessionSerializer deferManager = new TDSessionSerializer();

		ValidationError validationError;

		#region Internal Helper methods
		private object GetData(IKey key)
		{
			object val = deferableObjects[key];
			if( val == null )
			{
				val = deferManager.RetrieveAndDeserializeSessionObject( (simpleSession != null) ? simpleSession.SessionID : session.SessionID, Partition, key ); // TDSessionSerialiser
				if( val != null )
					deferableObjects[key] = val;
			}
			return val;
		}

		#endregion


		/// <summary>
		/// Set correct data to be saved to deferred storage.
		/// This is set in the cache and not serialised until end of session or save-data is called.
		/// </summary>
		/// <param name="key">The key to use</param>
		/// <param name="ob">The object to store</param>
		private void SetData(IKey key, object ob)
		{
			if( ob == null )
			{
				deferableObjects[key] = NULL_UNSAVED;
			}
			else
			{
				if (ob is ITDSessionAware)
					((ITDSessionAware)ob).IsDirty = true;

				deferableObjects[key] = ob;
			}
		}


		/// <summary>
		/// Creates a MockSessionManager for usage in the ServiceDiscovery
		/// </summary>
		/// <param name="Auth">If the session manager should indicate the user is logged on as a registrered user</param>
		public TestMockSessionManager( bool Auth, string Sesh, bool HasRealSession )
		{
			authenticated = Auth;
			if( HasRealSession ) 
			{
				session = new TestMockSession();
				deferableObjects = new Hashtable();
			}
			else 
			{
				simpleSession = new TestMockSimpleSession();
				simpleSession.SessionID = Sesh;
				deferableObjects = new Hashtable();
			}
			
		}

		#region Service Discovery Methods
		public object Get()
		{
			return this;
		}
		#endregion

		#region Properties and methods

		public ITDSessionManager GetSessionManagerForPartition( TDSessionPartition partition )
		{
			return null;
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
			get { return partition; }
			set { partition = value; }
		}

		public void SaveData()
		{

			TDSessionSerializer ser = new TDSessionSerializer();
			TDSessionPartition part = Partition;

			foreach( IKey key in deferableObjects.Keys )
			{				
				if( key is ITDSessionPartionable )
				{
					ser.SerializeSessionObjectAndSave( (simpleSession != null) ? simpleSession.SessionID : session.SessionID, part , key, deferableObjects[key] );
				}
				else
				{
					ser.SerializeSessionObjectAndSave( (simpleSession != null) ? simpleSession.SessionID : session.SessionID, key, deferableObjects[key] );
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

		public string GetOneUseKey( OneUseKey key)
		{
			return string.Empty;
		}

		public void SetOneUseKey( OneUseKey key, string val )
		{
		}

		public bool InitialiseJourneyParameters(FindAMode mode)
		{
			return true;
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
			CJPSessionInfo result = new CJPSessionInfo();
			result.SessionId = (simpleSession != null) ? simpleSession.SessionID : session.SessionID;
			result.IsLoggedOn = this.authenticated;
			result.Language = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();
			result.OriginAppDomainFriendlyName = AppDomain.CurrentDomain.FriendlyName;
			result.UserType = this.Authenticated ? (int)TDSessionManager.Current.CurrentUser.UserType : (int)TDUserType.Standard;
			return result;
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
		/// 
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
		/// Read only property that gets the Session data for the current session manager.
		/// </summary>
		public TestMockSimpleSession MockSimpleSession
		{
			get
			{	
				return simpleSession;
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
					manager = (TDItineraryManager)(new ItineraryManagerFactory().Get(ItineraryManagerMode.None ));
					SetData( TDSessionManager.KeyItineraryManager, manager );
				}
				return manager;
			}
			set
			{
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
			get
			{
				return (PricingRetailOptionsState)GetData( TDSessionManager.KeyPricingRetailOptions );
			}
			set
			{ 
				deferableObjects[TDSessionManager.KeyPricingRetailOptions] = value;
			}        
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
				return (FindCostBasedPageState)HttpContext.Current.Session["findCostBasedPageState"];
			}
			set 
			{
				HttpContext.Current.Session["findCostBasedPageState"] = value;
			}
		}

		
		/// <summary>
		/// Get the Find A mode
		/// </summary>
		public FindAMode FindAMode 
		{
			get 
			{
				FindPageState pageState = FindPageState;
				if (pageState == null) 
				{
					return FindAMode.None;
				} 
				else 
				{
					return pageState.Mode;
				}
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
				deferableObjects[TDSessionManager.KeyNearestCarParksMode] = value;
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
				return (TabSection)HttpContext.Current.Session["tabSection"];
			}
			set
			{
				if (TabSectionChangeable)
					HttpContext.Current.Session["tabSection"] = value;
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
				HttpContext.Current.Session["tabSection"] = value;
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
				if (HttpContext.Current.Session["tabSectionChangeable"] == null)
					HttpContext.Current.Session["tabSectionChangeable"] = true;

				return (bool)HttpContext.Current.Session["tabSectionChangeable"];
			}
			set
			{
				HttpContext.Current.Session["tabSectionChangeable"] = value;
			}
		}

		/// <summary>
		/// Gets/Sets whether the user survey form has been shown in this session		
		/// </summary>
		public bool UserSurveyAlreadyShown
		{
			get
			{
				if (HttpContext.Current.Session["userSurveyAlreadyShown"] == null)
					HttpContext.Current.Session["userSurveyAlreadyShown"] = false;

				return (bool)HttpContext.Current.Session["userSurveyAlreadyShown"];
			}
			set
			{
				HttpContext.Current.Session["userSurveyAlreadyShown"] = value;
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
			get { return HttpContext.Current.Session.SessionID; }
		}

		public FindAMode CostBasedFindAMode 
		{ 
			get {return FindAMode.None;}
		}

		public JourneyEmissionsPageState JourneyEmissionsPageState
		{
			get
			{
				JourneyEmissionsPageState pageState = (JourneyEmissionsPageState)GetData( TDSessionManager.KeyJourneyEmissionsPageState);
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
			// Write deferred data to db
			SaveData();
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

	/// <summary>
	/// TDSession forwards all calls to System.Web.Session
	/// </summary>
	public class TestMockSession : ITDSession
	{
		private TypeSafeDictionary Dict = new TypeSafeDictionary();
		private string _sessionID = string.Empty;

		#region ITDSession interface
		/// <summary>
		/// This is a read/write property that adds data of type int32 to
		/// the ASP session data.
		/// </summary>
		/// <value>Int32</value>
		public int this[IntKey key]
		{
			get
			{
				return Dict[key];
			}
			set
			{
				Dict[key] = value;
			}
		}
		
		/// <summary>
		/// This is a read/write property that adds data of type string to
		/// the ASP session data.
		/// </summary>
		/// <value>String</value>
		public string this[StringKey key]
		{
			get
			{
				return Dict[key];
			}
			set
			{
				Dict[key] = value;
			}
		}
		
		/// <summary>
		/// This is a read/write property that adds data of type Double to
		/// the ASP session data.
		/// </summary>
		/// <value>double</value>
		public double this[DoubleKey key]
		{
			get
			{
				return Dict[key];
			}
			set
			{
				Dict[key] = value;
			}
		}

		/// <summary>
		/// This is a read/write property that adds data of type DateTime to
		/// the ASP session data.
		/// </summary>
		/// <value>DateTime</value>
		public DateTime this[DateKey key]
		{
			get
			{
				return Dict[key];
			}
			set
			{
				Dict[key] = value;
			}
		}

		/// <summary>
		/// This is a read/write property that adds data of type bool to
		/// the ASP session data.
		/// </summary>
		/// <value>bool</value>
		public bool this[BoolKey key]
		{
			get
			{
				return Dict[key];
			}
			set
			{
				Dict[key] = value;
			}
		}

		/// <summary>
		/// This is a read/write property that adds data of type PageID to
		/// the ASP session data.
		/// </summary>
		/// <value>PageId</value>
		public PageId this[PageIdKey key]
		{
			get
			{
				return (PageId)Dict[key];
			}
			set
			{
				Dict[key] = value;
			}
		}

		public string SessionID
		{
			get { return _sessionID; }
			set { _sessionID = value; }
		}
		#endregion


	}

	/// <summary>
	/// Simple System.Web.Session emulator
	/// </summary>
	public class TestMockSimpleSession
	{
		string _SessionID = string.Empty;
		PageId _PageID;

		public TestMockSimpleSession() {}

		/// <summary>
		/// This is a read/write property for a mock ASP session ID.
		/// </summary>
		/// <value>string</value>
		public string SessionID
		{
			get
			{
				return _SessionID;
			}
			set
			{
				_SessionID = value;
			}
		}
	
		/// <summary>
		/// This is a read/write property for a mock PageID.
		/// </summary>
		/// <value>PageId</value>
		public PageId PageID
		{
			get
			{
				return _PageID;
			}
			set
			{
				_PageID = value;
			}
		}

		
	}
}