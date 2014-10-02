// *********************************************** 
// NAME                 : FindCarInputAdapter
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 29/07/2004 
// DESCRIPTION  : Adapter for FindCarInput page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/FindCarInputAdapter.cs-arc  $ 
//
//   Rev 1.4   Aug 28 2012 10:20:46   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.3   Mar 14 2011 15:12:02   rphilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.2   Mar 31 2008 12:59:00   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:16   mturner
//Initial revision.
//
//   Rev 1.28   Apr 20 2006 15:55:52   tmollart
//Removed DisableLocailityQuery method calls.
//Resolution for 3840: DN068 Replan:  JavaScript - No results returned when replanning a Car journey using find a car
//
//   Rev 1.27   Apr 05 2006 15:23:22   esevern
//Manual merge of stream0030
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.26   Mar 29 2006 16:53:44   tolomolaiye
//Removed Park and Ride changes. The changes are now in ParkAndRideInputAdapter
//
//   Rev 1.24   Feb 23 2006 19:16:08   build
//Automatically merged from branch for stream3129
//
//   Rev 1.23.1.1   Jan 30 2006 12:15:20   mdambrine
//add TDCultureInfo is now in the common namespace
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.23.1.0   Jan 10 2006 15:17:34   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.23   Nov 09 2005 12:31:54   build
//Automatically merged from branch for stream2818
//
//   Rev 1.22.1.0   Oct 14 2005 15:28:22   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.22   Apr 26 2005 09:03:22   Ralavi
//Setting AvoidRoadDisplayMode and UseRoadDisplayMode to readonly or normal depending on whether they are ambiguous.
//
//   Rev 1.21   Mar 23 2005 11:14:36   RAlavi
//Removed extra car preferences initialisation for car costing.
//
//   Rev 1.20   Mar 16 2005 10:58:40   PNorell
//Updated to take advantage of the newer UserPreferencesHelper.
//Resolution for 1958: DEV CODE Review: Preferences (Car and Public)
//
//   Rev 1.19   Mar 04 2005 16:29:46   RAlavi
//doNotUseMotorways issues resolved.
//
//   Rev 1.18   Mar 02 2005 15:22:34   RAlavi
//added code for ambiguity pages
//
//   Rev 1.17   Mar 02 2005 11:07:44   esevern
//removed setting of avoid roads from initJourneyParameters
//
//   Rev 1.16   Feb 24 2005 11:37:26   PNorell
//Updated for favourite details.
//
//   Rev 1.15   Feb 23 2005 16:30:10   RAlavi
//Changed for car costing
//
//   Rev 1.14   Feb 21 2005 13:48:02   esevern
//changed use/avoid road array lists to TDRoad arrays
//
//   Rev 1.13   Feb 18 2005 16:54:44   esevern
//Car costing - added arraylists for avoid/use roads
//
//   Rev 1.12   Jan 28 2005 18:41:24   ralavi
//Updated for car costing
//
//   Rev 1.11   Jan 25 2005 12:14:02   rhopkins
//Refactor ...InputAdapter classes to allow FindInputAdapter to be inherited by FindFareInputAdapter.
//
//   Rev 1.10   Oct 15 2004 12:36:18   jgeorge
//Added GetJourneyPlanControlData method
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.9   Oct 01 2004 11:03:44   COwczarek
//Make behaviour consistent for all Find A input pages when in ambiguity mode.
//Resolution for 1562: Find A input page in ambiguity mode always shows travel details
//
//   Rev 1.8   Sep 10 2004 19:14:34   RPhilpott
//Call DisableLocalityQuery() to prevent unnecessary GIS Query calls in gazetteers.
//Resolution for 1570: Find-A-Car  --  unnecessary calls to FindNearestLocality()
//
//   Rev 1.7   Sep 02 2004 15:56:52   passuied
//Modified Load Preferences to be able to load eitherway if the data is stored as object or string  and modified Save Preferences (when needed) to save data as objects (not as strings)
//
//   Rev 1.6   Aug 27 2004 17:02:48   RPhilpott
//Correct interaction between control hierarchy and LocationSearchHelper.
//Resolution for 1329: Both train and coach stops can be selected on a Find A Train or Find A Coach page
//
//   Rev 1.5   Aug 24 2004 18:05:36   RPhilpott
//Pass StationType to LocationSerachHelper to allow filtering by MajorStations gazetteer.
//Resolution for 1329: Both train and coach stops can be selected on a Find A Train or Find A Coach page
//
//   Rev 1.4   Aug 23 2004 13:11:42   passuied
//Added overloaded version of AmbiguitySearch which specifies if must accept postcodes or not.
//
//   Rev 1.3   Aug 17 2004 09:12:48   COwczarek
//Initialise PublicModes property to zero length array
//Resolution for 1351: Find a car does not return any journeys from City/Town/Suburb
//
//   Rev 1.2   Aug 03 2004 11:40:54   passuied
//Replaced Preferences keys to use the Common ProfileKeys
//
//   Rev 1.1   Aug 02 2004 15:37:22   passuied
//working verson of FindCarInput page + changes to some adapters, controls...
//
//   Rev 1.0   Jul 29 2004 15:10:14   passuied
//Initial Revision


using System;using TransportDirect.Common.ResourceManager;
using System.Collections;

using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common;


namespace TransportDirect.UserPortal.Web.Adapters
{
	
	/// <summary>
	/// Adapter for FindCarInput page
	/// </summary>
	public class FindCarInputAdapter : FindJourneyInputAdapter
	{
		
		IDataServices populator = null;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="journeyParams">Journey parameters for Find Car journey planning</param>
		/// <param name="pageState">Page state for Find Car input pages</param>
		/// <param name="inputPageState">Variables indicating state of page, return stack for navigation etc.</param>
		public FindCarInputAdapter(TDJourneyParametersMulti journeyParams, FindCarPageState pageState, InputPageState inputPageState) : 
			base(pageState, TDSessionManager.Current, inputPageState, journeyParams)
		{
			populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			
		}
		
		#region Protected Methods

		/// <summary>
		/// Updates travelPreferences with coach and train preferences to save.
		/// </summary>
		/// <param name="travelPreferences">Object to update with preferences that will be saved</param>
		protected override void saveModeSpecificPreferences(TDProfile travelPreferences) 
		{
			// Time to hit it.
			UserPreferencesHelper.SaveCarPreferences( (TDJourneyParametersMulti)journeyParams );
		}

		/// <summary>
		/// Updates journey parameters with values supplied in travelPreferences.
		/// </summary>
		/// <param name="travelPreferences">Object containing loaded user preferences</param>
		protected override void loadModeSpecificPreferences(TDProfile travelPreferences) 
		{
			TDJourneyParametersMulti jpm = (TDJourneyParametersMulti)journeyParams;
			UserPreferencesHelper.LoadCarPreferences( jpm );
		}


		#endregion Protected Methods

		#region Public Methods

		/// <summary>
		/// Retrieves a AsyncCallState object for Car plans
		/// </summary>
		/// <returns></returns>
		public override void InitialiseAsyncCallState()
		{
			AsyncCallState acs = new JourneyPlanState();

			// Determine refresh interval and resource string for the wait page from parameters passed in
			acs.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.FindACar"]);
			acs.WaitPageMessageResourceFile = "langStrings";
			acs.WaitPageMessageResourceId = "WaitPageMessage.FindACar";

			acs.AmbiguityPage = PageId.FindCarInput;
            acs.DestinationPage = PageId.JourneyDetails;
            acs.ErrorPage = PageId.JourneyDetails;
			acs.Status = AsyncCallStatus.None;
			tdSessionManager.AsyncCallState = acs;
		}

		/// <summary>
		/// Creates new location and search objects for each "to", "from" and "public via" locations and 
		/// assigns those to the journey parameters. Then initiates a new search on those objects using
		/// the original user's input values. 
		/// </summary>
		/// <param name="locationsControl"></param>
		/// <param name="journeyOptionsControl"></param>
		public void AmbiguitySearch(
			FindToFromLocationsControl locationsControl, 
			FindCarJourneyOptionsControl journeyOptionsControl )
		{


			base.AmbiguitySearch(locationsControl);

			if (journeyOptionsControl.TheLocation.Status == TDLocationStatus.Unspecified)
			{
				journeyOptionsControl.Search();
			} 
		}

		/// <summary>
		/// Initialises journey parameters with default values for Find A Car journey planning.
		/// Also loads user preferences into journey parameters.
		/// </summary>
		public override void InitJourneyParameters() 
		{
			base.InitJourneyParameters();

			// Also Initialise the ValidationError object
			if (TDSessionManager.Current.ValidationError != null)
				TDSessionManager.Current.ValidationError.Initialise();

			journeyParams.Origin.SearchType = SearchType.Locality;
			journeyParams.Destination.SearchType = SearchType.Locality;

			journeyParams.PrivateViaType = new 	TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);
			journeyParams.PrivateVia = new LocationSearch();

			journeyParams.PrivateVia.SearchType = SearchType.Locality;

			journeyParams.AvoidMotorWays = false;
			journeyParams.AvoidFerries = false;
			journeyParams.AvoidTolls = false;
			journeyParams.DoNotUseMotorways = false;
            journeyParams.BanUnknownLimitedAccess = false;

			string defaultItemValue = populator.GetDefaultListControlValue(DataServiceType.DrivingFindDrop);
			journeyParams.PrivateAlgorithmType = (PrivateAlgorithmType)Enum.Parse(typeof(PrivateAlgorithmType),defaultItemValue);

			journeyParams.DrivingSpeed = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.DrivingMaxSpeedDrop), TDCultureInfo.CurrentCulture);
			journeyParams.PrivateRequired = true;
			journeyParams.PublicRequired = false;
			journeyParams.PublicModes = new ModeType[0];
			journeyParams.CarSize = populator.GetDefaultListControlValue(DataServiceType.ListCarSizeDrop);
			journeyParams.CarFuelType = populator.GetDefaultListControlValue(DataServiceType.ListFuelTypeDrop);
			journeyParams.FuelConsumptionUnit = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.UnitsDrop), TDCultureInfo.CurrentCulture);

			LoadTravelDetails();
		}

		/// <summary>
		/// Initialises controls used on page
		/// </summary>
        public void InitialiseControls(
            FindCarPreferencesControl preferencesControl, FindToFromLocationsControl locationsControl,
            FindCarJourneyOptionsControl journeyOptionsControl)
        {
            InitLocationsControl(locationsControl);
            InitViaLocationsControl(journeyOptionsControl);
            InitPreferencesControl(preferencesControl, journeyOptionsControl);
            InitPreferencesDisplayMode(preferencesControl, journeyOptionsControl);
        }
        
        /// <summary>
        /// Initialises the locations control
        /// </summary>
        /// <param name="locationsControl">The control to initialise</param>
        public void InitLocationsControl(LocationControl originLocationControl, LocationControl destinationLocationControl)
        {
            originLocationControl.Initialise(journeyParams.OriginLocation, journeyParams.Origin, DataServiceType.FindCarLocationDrop, true, true, false, true, true, true, false, pageState.AmbiguityMode, false);
            destinationLocationControl.Initialise(journeyParams.DestinationLocation, journeyParams.Destination, DataServiceType.FindCarLocationDrop, true, true, false, true, true, true, false, pageState.AmbiguityMode, false);
        }

        /// <summary>
        /// Initialises location control for specifying private via locations
        /// </summary>
        public void InitViaLocationsControl(FindCarJourneyOptionsControl journeyOptionsControl)
        {
            InitViaLocationsControl(journeyOptionsControl, false);
        }

        /// <summary>
        /// Initialises location control for specifying private via locations
        /// </summary>
        public void InitViaLocationsControl(FindCarJourneyOptionsControl journeyOptionsControl, bool autoSuggest)
        {
            if (autoSuggest)
            {
                journeyOptionsControl.LocationControl.Initialise(journeyParams.PrivateViaLocation, journeyParams.PrivateVia, DataServiceType.CarViaDrop, true, true, false, true, true, true, false, pageState.AmbiguityMode, false);

                // Show the (auto suggest) via location and hide the standard via control
                journeyOptionsControl.ShowLocationControlAutoSuggest = true;
            }
            else
            {
                journeyOptionsControl.LocationControlType = journeyParams.PrivateViaType;
                journeyOptionsControl.LocationType = CurrentLocationType.PrivateVia;
                journeyOptionsControl.TheLocation = journeyParams.PrivateViaLocation;
                journeyOptionsControl.TheSearch = journeyParams.PrivateVia;
            }
        }

        /// <summary>
        /// Initialises preferences and options controls with journey parameters and page state values
        /// </summary>
        public void InitPreferencesControl(FindCarPreferencesControl preferencesControl, FindCarJourneyOptionsControl journeyOptionsControl)
        {
            preferencesControl.PreferencesVisible = pageState.TravelDetailsVisible;
        }

        /// <summary>
        /// Initialises the display mode of the preferences based on ambiguity mode
        /// </summary>
        /// <param name="preferencesControl"></param>
        /// <param name="journeyOptionsControl"></param>
        public void InitPreferencesDisplayMode(FindCarPreferencesControl preferencesControl, FindCarJourneyOptionsControl journeyOptionsControl)
        {
            if (pageState.AmbiguityMode)
            {
                preferencesControl.TypeJourneyDisplayMode = GenericDisplayMode.ReadOnly;
                preferencesControl.SpeedChangeDisplayMode = GenericDisplayMode.ReadOnly;
                preferencesControl.CarSizeDisplayMode = GenericDisplayMode.ReadOnly;
                preferencesControl.FuelUseUnitDisplayMode = GenericDisplayMode.ReadOnly;
                preferencesControl.FuelTypeDisplayMode = GenericDisplayMode.ReadOnly;
                preferencesControl.FuelConsumptionOptionMode = GenericDisplayMode.ReadOnly;
                preferencesControl.FuelCostOptionMode = GenericDisplayMode.ReadOnly;
                journeyOptionsControl.AvoidRoadDisplayMode = GenericDisplayMode.ReadOnly;
                journeyOptionsControl.UseRoadDisplayMode = GenericDisplayMode.ReadOnly;
            }
            else
            {
                preferencesControl.TypeJourneyDisplayMode = GenericDisplayMode.Normal;
                preferencesControl.SpeedChangeDisplayMode = GenericDisplayMode.Normal;
                preferencesControl.CarSizeDisplayMode = GenericDisplayMode.Normal;
                preferencesControl.FuelUseUnitDisplayMode = GenericDisplayMode.Normal;
                preferencesControl.FuelTypeDisplayMode = GenericDisplayMode.Normal;
                preferencesControl.FuelConsumptionOptionMode = GenericDisplayMode.Normal;
                preferencesControl.FuelCostOptionMode = GenericDisplayMode.Normal;
                journeyOptionsControl.AvoidRoadDisplayMode = GenericDisplayMode.Normal;
                journeyOptionsControl.UseRoadDisplayMode = GenericDisplayMode.Normal;
            }
        }

		#endregion Public Methods

	}


}
