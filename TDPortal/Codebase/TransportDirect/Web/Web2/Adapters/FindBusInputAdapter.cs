// *********************************************** 
// NAME                 : FindBusInputAdapter
// AUTHOR               : Esther Severn
// DATE CREATED         : 20/03/2006
// DESCRIPTION			: Adapter for FindBusInput
//						  page, containing helper
//						  methods specific to Find
//						  a Bus
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/FindBusInputAdapter.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 12:59:00   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:16   mturner
//Initial revision.
//
//   Rev 1.9   Apr 05 2006 11:00:02   esevern
//added methods to update location and public mode options from journey parameters
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.8   Mar 31 2006 11:51:42   mdambrine
//removed the extra code in initviaparameters method as it was repopulating the field on clear page action
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.7   Mar 31 2006 10:20:42   mdambrine
//Fxcop fixes
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.6   Mar 29 2006 17:10:50   esevern
//added UpdatePreferencesControls to be called when user preferences are loaded on FindBusInput page after login
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.5   Mar 29 2006 11:42:26   RGriffith
//Wait Page Ehancement changes
//Resolution for 33: DEL 8.1 Workstream: Wait Pages
//
//   Rev 1.4   Mar 28 2006 12:52:28   esevern
//added setting of origin and destination SearchType (default gazeteer should be 'station/airport' for Find a Bus)
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.3   Mar 24 2006 11:46:02   esevern
//Added population of public via location on via location initialisation
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.2   Mar 23 2006 16:15:08   esevern
//corrected AmbiguitySearch()
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.1   Mar 22 2006 11:58:30   esevern
//interim check-in for handover
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.0   Mar 20 2006 14:53:40   esevern
//Initial revision.
//Resolution for 29: DEL8.1 Workstream - Find a Bus


using System;
using System.Globalization;

using TransportDirect.Web.Support;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;


namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Adapter for FindBusInput page
	/// </summary>
	public class FindBusInputAdapter : FindJourneyInputAdapter
	{
		IDataServices populator;

		
		/// <summary>
		/// Constructor for FindBusInputAdapter class
		/// </summary>
		/// <param name="journeyParams">Journey parameters for Find Bus journey planning</param>
		/// <param name="pageState">Page state for Find Bus input pages</param>
		/// <param name="inputPageState">Variables indicating state of page, return stack for navigation etc.</param>
		public FindBusInputAdapter(TDJourneyParametersMulti journeyParams, TransportDirect.UserPortal.SessionManager.FindBusPageState pageState, InputPageState inputPageState) : 
			base(pageState, TDSessionManager.Current, inputPageState, journeyParams)
		{
			populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
		}

		#region Public Methods
		
		/// <summary>
		/// Initialises journey parameters with default values for Find A Bus 
		/// journey planning.
		/// </summary>
		public override void InitJourneyParameters() 
		{
			base.InitJourneyParameters();

			journeyParams.Origin.SearchType = GetDefaultSearchType(DataServiceType.FromToDrop);
			journeyParams.Destination.SearchType = GetDefaultSearchType(DataServiceType.FromToDrop);

			journeyParams.PublicViaType = new TDJourneyParameters.LocationSelectControlType(
													TDJourneyParameters.ControlType.Default);
			journeyParams.PublicVia = new LocationSearch();

			// default values for no. and speed of interchanges
			string defaultItemValue = populator.GetDefaultListControlValue(
												DataServiceType.ChangesSpeedDrop);
			journeyParams.InterchangeSpeed = Convert.ToInt32(defaultItemValue, TDCultureInfo.CurrentCulture.NumberFormat);

			defaultItemValue = populator.GetDefaultListControlValue(
												DataServiceType.ChangesFindDrop);
			journeyParams.PublicAlgorithmType = (PublicAlgorithmType)Enum.Parse(
												typeof(PublicAlgorithmType),defaultItemValue);

			// default values for walking time and speed
			journeyParams.MaxWalkingTime = Convert.ToInt32(populator.GetDefaultListControlValue( 
															DataServiceType.WalkingMaxTimeDrop ), 
															CultureInfo.CurrentCulture );
			journeyParams.WalkingSpeed = Convert.ToInt32(populator.GetDefaultListControlValue(
														DataServiceType.WalkingSpeedDrop), 
														CultureInfo.CurrentCulture);
			
			// private journey not required
			journeyParams.PrivateRequired = false;

			// set up modes required - bus, ferry, tram
			journeyParams.PublicModes = new ModeType[] {ModeType.Bus}; 

			LoadTravelDetails();
		}


		/// <summary>
		/// Initiates a new search on to, from and public via locations  
		/// using the original user's input values. 
		/// </summary>
		/// <param name="preferencesControl">PtPreferencesControl travel preferences</param>
		/// <param name="locationsControl">FindToFromLocationsControl the origin and destination locations</param>
		public void AmbiguitySearch(FindToFromLocationsControl locationsControl, 
									PtPreferencesControl preferencesControl )
		{
			// perform searches on origin and destination tri state locations
			base.AmbiguitySearch(locationsControl);

			// perform search on public via location
			if (preferencesControl.ViaLocationControl.TheLocation.Status == TDLocationStatus.Unspecified)
			{
				preferencesControl.ViaLocationControl.TheLocation = journeyParams.PublicViaLocation;
				preferencesControl.ViaLocationControl.TheSearch = journeyParams.PublicVia;
				preferencesControl.ViaLocationControl.Search();
			
				journeyParams.PublicViaLocation = preferencesControl.ViaLocationControl.TheLocation;
				journeyParams.PublicVia = preferencesControl.ViaLocationControl.TheSearch;
			} 
		}
	
		/// <summary>
		/// Retrieves a JourneyPlanState object for Bus plans
		/// </summary>
		/// <returns></returns>
		public override void InitialiseAsyncCallState()
		{
			AsyncCallState acs = new JourneyPlanState();

			// Determine refresh interval and resource string for the wait page
			acs.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.FindABus"], TDCultureInfo.CurrentUICulture.NumberFormat);
			acs.WaitPageMessageResourceFile = "langStrings";
			acs.WaitPageMessageResourceId = "WaitPageMessage.FindABus";

			acs.AmbiguityPage = PageId.FindBusInput;
			acs.DestinationPage = PageId.JourneyDetails;
            acs.ErrorPage = PageId.JourneyDetails;
			acs.Status = AsyncCallStatus.None;
			tdSessionManager.AsyncCallState = acs;
		}


		/// <summary>
		/// Initialises location control for specifying public via locations. 
		/// Sets the location, search and location type refs from journey parameter values
		/// </summary>
		public void InitViaLocationControl(PtPreferencesControl preferencesControl)
		{
			preferencesControl.ViaLocationControl.LocationControlType = journeyParams.PublicViaType;
			preferencesControl.ViaLocationControl.LocationType = CurrentLocationType.PublicVia;
			preferencesControl.ViaLocationControl.TheLocation = journeyParams.PublicViaLocation;
			preferencesControl.ViaLocationControl.TheSearch = journeyParams.PublicVia;			
		}

		/// <summary>
		/// Initialises preferences visibility and options controls with journey 
		/// parameters and page state values
		/// </summary>
		public void InitPreferencesControl(PtPreferencesControl preferencesControl)
		{
			// set public transport preferences for walking and interchanges
			preferencesControl.JourneyChangesOptionsControl.ChangesSpeed = journeyParams.InterchangeSpeed;
			preferencesControl.JourneyChangesOptionsControl.Changes = journeyParams.PublicAlgorithmType;
			preferencesControl.WalkingSpeedOptionsControl.WalkingDuration = journeyParams.MaxWalkingTime;
			preferencesControl.WalkingSpeedOptionsControl.WalkingSpeed = journeyParams.WalkingSpeed;	
			
			// set visibility of preferences control
			preferencesControl.PreferencesVisible = pageState.TravelDetailsVisible;

			InitViaLocationControl(preferencesControl);
		}

		/// <summary>
		/// Initialises location and preference controls used on page
		/// </summary>
		/// <param name="preferencesControl">PtPreferencesControl travel preferences</param>
		/// <param name="locationsControl">FindToFromLocationsControl the origin and destination locations</param>
		public void InitialiseControls(PtPreferencesControl preferencesControl, 
										FindToFromLocationsControl locationsControl) 
		{
			InitLocationsControl(locationsControl);
			InitPreferencesControl(preferencesControl);

			if (pageState.AmbiguityMode) 
			{
				preferencesControl.JourneyChangesOptionsControl.ChangesSpeedDisplayMode = GenericDisplayMode.ReadOnly; 
				preferencesControl.JourneyChangesOptionsControl.ChangesDisplayMode = GenericDisplayMode.ReadOnly;
				preferencesControl.WalkingSpeedOptionsControl.WalkingSpeedDisplayMode = GenericDisplayMode.ReadOnly;
				preferencesControl.WalkingSpeedOptionsControl.WalkingDurationDisplayMode = GenericDisplayMode.ReadOnly;
			} 
			else 
			{
				preferencesControl.JourneyChangesOptionsControl.ChangesSpeedDisplayMode = GenericDisplayMode.Normal; 
				preferencesControl.JourneyChangesOptionsControl.ChangesDisplayMode = GenericDisplayMode.Normal;
				preferencesControl.WalkingSpeedOptionsControl.WalkingSpeedDisplayMode = GenericDisplayMode.Normal;
				preferencesControl.WalkingSpeedOptionsControl.WalkingDurationDisplayMode = GenericDisplayMode.Normal;
			}
		}


		/// <summary>
		/// Updates the Origin, Destination and Via location controls with  
		/// journey parameter values
		/// </summary>
		/// <param name="locationsControl"></param>
		/// <param name="preferencesControl">Public transport preferences control</param>
		/// <param name="checkInput"></param>
		public void UpdateLocationsControls(FindToFromLocationsControl locationsControl, PtPreferencesControl preferencesControl)
		{
			// via location control
			preferencesControl.ViaLocationControl.LocationControlType = journeyParams.PublicViaType;
			preferencesControl.ViaLocationControl.LocationType = CurrentLocationType.PublicVia;
			preferencesControl.ViaLocationControl.TheLocation = journeyParams.PublicViaLocation;

			// origin location
			locationsControl.OriginLocation = journeyParams.OriginLocation;
			locationsControl.OriginSearch = journeyParams.Origin;
			locationsControl.FromLocationControl.LocationControlType = journeyParams.OriginType;
			
			// destination location
			locationsControl.DestinationLocation = journeyParams.DestinationLocation;
			locationsControl.DestinationSearch = journeyParams.Destination;
			locationsControl.ToLocationControl.LocationControlType = journeyParams.DestinationType;
		}

		/// <summary>
		/// Updates the public mode transport types from journey parameter values
		/// </summary>
		/// <param name="transportTypesControl">Find Bus transport types display control</param>
		public void UpdateTransportTypes(TransportTypesControl transportTypesControl) 
		{
			transportTypesControl.PublicModes = journeyParams.PublicModes;
		}

		/// <summary>
		/// Updates the advanced travel option controls (PtJourneyChangesOptionsControl and 
		/// PtWalkingSpeedOptionsControl) with any user preferences retrieved when logged in.
		/// </summary>
		public void UpdatePreferencesControls(PtPreferencesControl preferencesControl) 
		{
			preferencesControl.WalkingSpeedOptionsControl.WalkingSpeed = journeyParams.WalkingSpeed;
			preferencesControl.WalkingSpeedOptionsControl.WalkingDuration = journeyParams.MaxWalkingTime;
			preferencesControl.JourneyChangesOptionsControl.ChangesSpeed = journeyParams.InterchangeSpeed;
			preferencesControl.JourneyChangesOptionsControl.Changes = journeyParams.PublicAlgorithmType;
		}

		#endregion Public Methods

		#region Protected Methods

		/// <summary>
		/// Updates journey parameters with values supplied in users travel preferences.
		/// </summary>
		/// <param name="travelPreferences"></param>
		protected override void loadModeSpecificPreferences(TDProfile travelPreferences) 
		{
			UserPreferencesHelper.LoadPublicTransportPreferences((TDJourneyParametersMulti)journeyParams);
		}


		/// <summary>
		/// Updates travel preferences with bus related preferences to save.
		/// </summary>
		/// <param name="journeyParameters"></param>
		protected override void saveModeSpecificPreferences(TDProfile travelPreferences) 
		{
			UserPreferencesHelper.SavePublicTransportPreferences( (TDJourneyParametersMulti)journeyParams );
		}

		/// <summary>
		/// Template method that returns a station type to be used to filter 
		/// major stations gazetteer searches, returning StationType.Undetermined  
		/// for Find Bus.
		/// </summary>
		protected override StationType GetStationTypeFilter()
		{
			return StationType.Undetermined;
		}


		/// <summary>
		/// Returns the default SearchType for the provided DataService list type,
		/// e.g. default SearchType for DataServiceType.FromToDrop is MainStationAirport
		/// </summary>
		/// <param name="listType"></param>
		/// <returns></returns>
		protected SearchType GetDefaultSearchType(DataServiceType listType)
		{
			DataServices.IDataServices ds = (DataServices.IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			string defaultItemValue = ds.GetDefaultListControlValue(listType);
			return (SearchType) Enum.Parse(typeof(SearchType), defaultItemValue);			
		}

		#endregion Protected Methods


	}
}
