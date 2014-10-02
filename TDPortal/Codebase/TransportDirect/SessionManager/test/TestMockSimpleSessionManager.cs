// ***********************************************
// NAME 		: TestMockSimpleSessionManager.cs
// AUTHOR 		: Jon George
// DATE CREATED : 18/08/2003
// DESCRIPTION 	: Simple mock session manager with limited
//				  functionality.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/test/TestMockSimpleSessionManager.cs-arc  $
//
//   Rev 1.2   Sep 14 2009 10:31:48   apatel
//Stop Information page related changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.1   Oct 13 2008 16:46:40   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.0   Jun 20 2008 14:46:40   mmodi
//Updated to expose cycle results
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:49:02   mturner
//Initial revision.
//
//   Rev 1.5   Dec 07 2006 14:37:54   build
//Automatically merged from branch for stream4240
//
//   Rev 1.4.1.0   Nov 16 2006 17:34:16   mmodi
//Added JourneyEmissionsPageState
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.4   Oct 06 2006 13:45:02   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.3.1.1   Oct 03 2006 10:27:14   mmodi
//Added IsFromNearestCarParks
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4208: Car Parking: New Search a car park journey goes to Car route input
//
//   Rev 1.3.1.0   Aug 10 2006 14:08:10   esevern
//added FindCarParkPageState
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.3   Feb 10 2006 17:38:12   kjosling
//Implemented ITDSessionManager.BusinessLinkState. Fix for build
//
//   Rev 1.1   Feb 10 2006 16:03:42   kjosling
//Manually merged for stream 3180
//
//   Rev 1.0.1.2   Jan 11 2006 13:50:48   tmollart
//Updated after comments from code review.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2

using System;
using TransportDirect.Common.ServiceDiscovery;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.TravelNews;
using TransportDirect.UserPortal.CyclePlannerControl;

namespace TransportDirect.UserPortal.SessionManager
{
	[CLSCompliant(false)]
	public class TestMockSimpleSessionManager : ITDSessionManager, IServiceFactory
	{
		private Hashtable properties;
		private Hashtable oneUseKeys;
		private ITDSession testSession;
		private TabSection tabSection;
		private TabSection newTabSection;
		private bool tabSectionChangable;
		private TDSessionPartition partition;

		public TestMockSimpleSessionManager()
		{
			properties = new Hashtable();
			oneUseKeys = new Hashtable();
			testSession = new TestMockSession();
			((TestMockSession)testSession).SessionID = Guid.NewGuid().ToString();

		}

		#region ITDSessionManager Members

		public string GetOneUseKey(TransportDirect.UserPortal.Resource.OneUseKey key)
		{
			return (string)oneUseKeys[key];
		}

		public void SetOneUseKey(TransportDirect.UserPortal.Resource.OneUseKey key, string val)
		{
			oneUseKeys[key] = val;
		}

		public void SaveCurrentFindAMode()
		{
		}

		public void SaveData()
		{
			foreach (object curr in properties.Values)
			{
				ITDSessionAware item = curr as ITDSessionAware;
				if (item != null)
					item.IsDirty = false;
			}
		}

		public void ClearDeferredData()
		{
			// Do nothing
		}

		public ITDSessionManager GetSessionManagerForPartition(TransportDirect.UserPortal.SessionManager.TDSessionPartition partition)
		{
			return this;
		}

		public BusinessLinkState BusinessLinkState
		{
			get
			{
				return new BusinessLinkState();
			}

			set
			{
			}
		}

		public CJPSessionInfo GetSessionInformation()
		{
			CJPSessionInfo result = new CJPSessionInfo();
			result.Language = System.Globalization.CultureInfo.CurrentUICulture.Name;
			result.OriginAppDomainFriendlyName = string.Empty;
			result.SessionId = testSession.SessionID;
			result.IsLoggedOn = false;
			return result;
		}

		public TDItineraryManager ItineraryManager
		{
			get
			{
				if ( !properties.ContainsKey(TDSessionManager.KeyItineraryManager))
					properties[TDSessionManager.KeyItineraryManager] = new ExtendItineraryManager();

				return (TDItineraryManager)properties[TDSessionManager.KeyItineraryManager];
			}
			set
			{
				properties[TDSessionManager.KeyItineraryManager] = value;
			}
		}

		public TransportDirect.UserPortal.JourneyControl.ITDJourneyRequest JourneyRequest
		{
			get { return properties[TDSessionManager.KeyJourneyRequest] as ITDJourneyRequest; }
			set { properties[TDSessionManager.KeyJourneyRequest] = value; }
		}

		public TransportDirect.UserPortal.JourneyControl.ITDJourneyResult JourneyResult
		{
			get { return properties[TDSessionManager.KeyJourneyResult] as ITDJourneyResult; }
			set { properties[TDSessionManager.KeyJourneyResult] = value; }
		}

		public ITDJourneyResult AmendedJourneyResult
		{
			get { return properties[TDSessionManager.KeyAmendedJourneyResult] as ITDJourneyResult; }
			set { properties[TDSessionManager.KeyAmendedJourneyResult] = value; }
		}

		public TDJourneyViewState JourneyViewState
		{
			get { return properties[TDSessionManager.KeyJourneyViewState] as TDJourneyViewState; }
			set { properties[TDSessionManager.KeyJourneyViewState] = value; }
		}

		public TDCurrentAdjustState CurrentAdjustState
		{
			get { return properties[TDSessionManager.KeyCurrentAdjustState] as TDCurrentAdjustState; }
			set { properties[TDSessionManager.KeyCurrentAdjustState] = value; }
		}

		public AsyncCallState AsyncCallState
		{
			get { return properties[TDSessionManager.KeyAsyncCallState] as AsyncCallState; }
			set { properties[TDSessionManager.KeyAsyncCallState] = value; }
		}

		public JourneyMapState JourneyMapState
		{
			get 
			{ 
				if (properties[TDSessionManager.KeyJourneyMapState] == null)
				{
					properties[TDSessionManager.KeyJourneyMapState] = new JourneyMapState();
				}
				return properties[TDSessionManager.KeyJourneyMapState] as JourneyMapState; 
			}
			set { properties[TDSessionManager.KeyJourneyMapState] = value; }
		}

		public JourneyMapState ReturnJourneyMapState
		{
			get 
			{
				if (properties[TDSessionManager.KeyReturnJourneyMapState] == null)
				{
					properties[TDSessionManager.KeyReturnJourneyMapState] = new JourneyMapState();
				}
				return properties[TDSessionManager.KeyReturnJourneyMapState] as JourneyMapState; 
			}
			set { properties[TDSessionManager.KeyReturnJourneyMapState] = value; }
		}

        public TransportDirect.UserPortal.CyclePlannerControl.ITDCyclePlannerRequest CycleRequest
		{
			get { return properties[TDSessionManager.KeyCycleRequest] as ITDCyclePlannerRequest; }
			set { properties[TDSessionManager.KeyCycleRequest] = value; }
		}

        public TransportDirect.UserPortal.CyclePlannerControl.ITDCyclePlannerResult CycleResult
        {
            get { return properties[TDSessionManager.KeyCycleResult] as ITDCyclePlannerResult; }
            set { properties[TDSessionManager.KeyCycleResult] = value; }
        }

		public ValidationError ValidationError
		{
			get { return properties["ValidationError"] as ValidationError; }
			set { properties["ValidationError"] = value; }
		}

		public bool Authenticated
		{
			get { return false; }
		}

		public TDUser CurrentUser
		{
			get { return null; }
			set {  } // Do nothing
		}

		public TDUser UnsavedNewUser
		{
			get { return null; }
			set {  } // Do nothing
		}

		public string UnsavedUsername
		{
			get { return null; }
			set {  } // Do nothing
		}

		public string UnsavedPassword
		{
			get { return null; }
			set {  } // Do nothing
		}

		public TypeSafeDictionary FormShift
		{
			get 
			{ 
				if ( !properties.ContainsKey("FormShift"))
					properties["FormShift"] = new TypeSafeDictionary();

				return (TypeSafeDictionary)properties["FormShift"];
			}
		}

		public ITDSession Session
		{
			get { return testSession; }
		}

		private int cacheParam;

		public int CacheParam
		{
			get { return cacheParam; }
			set { cacheParam = value; }
		}

		public TDJourneyParameters JourneyParameters
		{
			get { return properties[TDSessionManager.KeyJourneyParameters] as TDJourneyParameters; }
			set { properties[TDSessionManager.KeyJourneyParameters] = value; }
		}

		public InputPageState InputPageState
		{
			get 
			{ 
				if (properties[TDSessionManager.KeyInputPageState] == null)
				{
					properties[TDSessionManager.KeyInputPageState] = new InputPageState();
				}
				return properties[TDSessionManager.KeyInputPageState] as InputPageState; 
			}
			set { properties[TDSessionManager.KeyInputPageState] = value; }
		}

		public FindStationPageState FindStationPageState
		{
			get { return properties[TDSessionManager.KeyFindStationPageState] as FindStationPageState; }
			set { properties[TDSessionManager.KeyFindStationPageState] = value; }
		}

		public FindCarParkPageState FindCarParkPageState
		{
			get { return properties[TDSessionManager.KeyFindCarParkPageState] as FindCarParkPageState; }
			set { properties[TDSessionManager.KeyFindCarParkPageState] = value; }
		}

		public FindPageState FindPageState
		{
			get { return properties[TDSessionManager.KeyFindPageState] as FindPageState; }
			set { properties[TDSessionManager.KeyFindPageState] = value; }
		}

		public PricingRetailOptionsState PricingRetailOptions
		{
			get { return properties[TDSessionManager.KeyPricingRetailOptions] as PricingRetailOptionsState; }
			set { properties[TDSessionManager.KeyPricingRetailOptions] = value; }
		}

		public AmbiguityResolutionState AmbiguityResolution
		{
			get { return properties[TDSessionManager.KeyAmbiguityResolution] as AmbiguityResolutionState; }
			set { properties[TDSessionManager.KeyAmbiguityResolution] = value; }
		}

		public TravelNewsState TravelNewsState
		{
			get { return properties[TDSessionManager.KeyTravelNewsState] as TravelNewsState; }
			set { properties[TDSessionManager.KeyTravelNewsState] = value; }
		}

		public object[] StoredMapViewState
		{
			get { return properties[TDSessionManager.KeyStoredMapViewState] as object[]; }
			set { properties[TDSessionManager.KeyStoredMapViewState] = value; }
		}

		public FindAMode FindAMode
		{
			get { 
				FindPageState pageState = this.FindPageState;
				if (pageState == null)
					return FindAMode.None;
				else
					return pageState.Mode;
			}
		}

		public bool IsFindAMode
		{
			get { return this.FindAMode != FindAMode.None; }
		}

		public bool IsFromNearestCarParks
		{
			get 
			{
				return false;
			}
						set
			{
				properties[TDSessionManager.KeyNearestCarParksMode] = value;
			}
		}

        public bool IsStopInformationMode
        {
            get
            {
                return false;
            }
            set
            {
                properties[TDSessionManager.KeyStopInformationMode] = value;
            }
        }

		public TabSection TabSection
		{
			get { return tabSection; }
			set { tabSection = value; }
		}

		public TabSection NewTabSection
		{
			set { newTabSection = value; }
		}

		public bool TabSectionChangeable
		{
			get { return tabSectionChangable; }
			set { tabSectionChangable = value; }
		}

		public bool UserSurveyAlreadyShown
		{
			get { return false; }
			set { } // Do nothing
		}

		public TDSessionPartition Partition
		{
			get { return partition; }
			set { partition = value; }
		}

		public bool HasTimeBasedJourneyResults
		{
			get
			{
				if (Partition == TDSessionPartition.TimeBased)
				{
					if (ItineraryManager.Length > 0) 
						return true;
					else 
						return JourneyResult != null && JourneyResult.IsValid;
				}
				else
				{
					if (ItineraryManager != null && (ItineraryManager.Length > 0)) 
						return true;
					else 
						return JourneyResult != null && JourneyResult.IsValid;
				}

			}		
		}

		public bool HasCostBasedJourneyResults
		{
			get
			{
				if (Partition == TDSessionPartition.CostBased)
				{

					if (ItineraryManager.Length > 0) 
						return true;
					else 
						return JourneyResult != null && JourneyResult.IsValid;

				}
				else
				{
					if (ItineraryManager != null && (ItineraryManager.Length > 0)) 
						return true;
					else 
						return JourneyResult != null && JourneyResult.IsValid;
				}

			}		
		}

		public bool HasCostBasedFaresResults
		{
			get
			{
				object state;

				state = this.FindPageState;

				FindCostBasedPageState findCostBasedPageState = state as FindCostBasedPageState;
				//Check returned state is not null and there are travel dates on the search				
				return (findCostBasedPageState != null) && (findCostBasedPageState.SearchResult != null) &&
					(findCostBasedPageState.SearchResult.GetAllTravelDates().Length > 0);
			}
		}

		public TransportDirect.UserPortal.SessionManager.FindAMode TimeBasedFindAMode
		{
			get
			{
				if( Partition == TDSessionPartition.TimeBased )
					return ItineraryManager.BaseJourneyFindAMode;
				else
				{
					if (ItineraryManager != null) 
						return ItineraryManager.TimeBasedBaseJourneyFindAMode;
					else 
						return FindAMode.None;
				}
			}
		}

		public TransportDirect.UserPortal.SessionManager.FindAMode CostBasedFindAMode
		{
			get { return TimeBasedFindAMode; }
		}

		public void OnLoad()
		{
		}

		public void OnFormShift()
		{
		}

		public void OnPreUnload()
		{
		}

		public void OnUnload()
		{
		}

		public bool InitialiseJourneyParameters(FindAMode mode)
		{
			return true;
		}

		public ItineraryManagerMode ItineraryMode
		{
			get {return ItineraryManagerMode.None;}
			set {}
		}

		public ResultsPageState ResultsPageState
		{
			get {return null;}
			set {}
		}

		public JourneyEmissionsPageState JourneyEmissionsPageState
		{
			get { return properties[TDSessionManager.KeyJourneyEmissionsPageState] as JourneyEmissionsPageState; }
			set { properties[TDSessionManager.KeyJourneyEmissionsPageState] = value; }
		}

		#endregion

		#region IServiceFactory Members

		public object Get()
		{
			return this;
		}

		#endregion
	}
}
