// *********************************************** 
// NAME			: FindCoachTrainInputAdapter.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 26.07.04
// DESCRIPTION	: Extends/modifies common functionality of base
// class to provide specific behaviour for Find A Coach and Train input pages
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/FindCoachTrainInputAdapter.cs-arc  $
//
//   Rev 1.5   Feb 02 2009 17:04:36   mmodi
//Populate Routing Guide properties	
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.4   May 22 2008 16:40:44   mmodi
//Undid previous change, Amendmode is no longer set here
//Resolution for 4998: "Find Nearest" functionality on Find Train / Find Coach input not working
//Resolution for 5000: Find Train - when ambiguity page is diplayed, resolved locations still editable
//Resolution for 5002: Amend Find a train cost does not use the new location
//
//   Rev 1.3   Apr 30 2008 15:40:40   apatel
//make the location control box to be amendable when amend button clicked.
//
//   Rev 1.2   Mar 31 2008 12:59:00   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:18   mturner
//Initial revision.
//
//   Rev 1.13   Apr 05 2006 15:42:50   build
//Automatically merged from branch for stream0030
//
//   Rev 1.12.1.0   Mar 29 2006 11:15:32   RGriffith
//Wait Page Ehancement changes
//Resolution for 33: DEL 8.1 Workstream: Wait Pages
//
//   Rev 1.12   Feb 23 2006 19:16:08   build
//Automatically merged from branch for stream3129
//
//   Rev 1.11.1.1   Jan 30 2006 12:15:20   mdambrine
//add TDCultureInfo is now in the common namespace
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.11.1.0   Jan 10 2006 15:17:34   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.11   Nov 09 2005 12:31:54   build
//Automatically merged from branch for stream2818
//
//   Rev 1.10.1.0   Oct 14 2005 15:28:12   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.10   Jan 25 2005 12:14:04   rhopkins
//Refactor ...InputAdapter classes to allow FindInputAdapter to be inherited by FindFareInputAdapter.
//
//   Rev 1.9   Oct 15 2004 12:36:18   jgeorge
//Added GetJourneyPlanControlData method
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.8   Oct 01 2004 11:03:44   COwczarek
//Make behaviour consistent for all Find A input pages when in ambiguity mode.
//Resolution for 1562: Find A input page in ambiguity mode always shows travel details
//
//   Rev 1.7   Sep 02 2004 15:56:50   passuied
//Modified Load Preferences to be able to load eitherway if the data is stored as object or string  and modified Save Preferences (when needed) to save data as objects (not as strings)
//Resolution for 1473: Error message displayed after logging in and selecting travel preferences on journey planner page
//
//   Rev 1.6   Aug 27 2004 17:02:48   RPhilpott
//Correct interaction between control hierarchy and LocationSearchHelper.
//Resolution for 1329: Both train and coach stops can be selected on a Find A Train or Find A Coach page
//
//   Rev 1.5   Aug 24 2004 18:05:36   RPhilpott
//Pass StationType to LocationSerachHelper to allow filtering by MajorStations gazetteer.
//Resolution for 1329: Both train and coach stops can be selected on a Find A Train or Find A Coach page
//
//   Rev 1.4   Aug 20 2004 10:47:04   passuied
//Included setting of public modes in InitJourneyParameters. Addition of 2 sub-classes (1 for coach, 1 for train) inheriting from FindCoachTrainInputAdapter
//Resolution for 1348: Not possible to plan Find A Train journey using Find A Station/Airport
//
//   Rev 1.3   Aug 05 2004 14:56:28   COwczarek
//Remove mode initialisation from InitJourneyParameters (now done by page)
//Resolution for 1202: Implement FindTrainInput and FindCoachInput pages
//
//   Rev 1.2   Aug 02 2004 15:37:20   passuied
//working verson of FindCarInput page + changes to some adapters, controls...
//
//   Rev 1.1   Aug 02 2004 14:33:38   COwczarek
//Use constants in ProfileKeys rather than hardcoded key values
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.0   Jul 29 2004 11:18:48   COwczarek
//Initial revision.
//Resolution for 1202: Implement FindTrainInput page

using System;using TransportDirect.Common.ResourceManager;
using System.Globalization;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.Web.Adapters
{

    /// <summary>
    /// Extends/modifies common functionality of base class to provide
    /// specific behaviour for Find A Coach and Train input pages
    /// </summary>
    public abstract class FindCoachTrainInputAdapter : FindJourneyInputAdapter
    {

        IDataServices populator = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="journeyParams">Journey parameters for Find A coach and train journey planning</param>
		/// <param name="pageState">Page state for Find A coach and train input pages</param>
		/// <param name="inputPageState">Variables indicating state of page, return stack for navigation etc.</param>
		public FindCoachTrainInputAdapter(TDJourneyParametersMulti journeyParams, FindCoachTrainPageState pageState, InputPageState inputPageState) : 
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
            
            travelPreferences.Properties[ProfileKeys.INTERCHANGE_LIMIT].Value = 
                ((TDJourneyParametersMulti)journeyParams).PublicAlgorithmType;
            travelPreferences.Properties[ProfileKeys.INTERCHANGE_SPEED].Value = 
                ((TDJourneyParametersMulti)journeyParams).InterchangeSpeed;
        }

        /// <summary>
        /// Updates journey parameters with values supplied in travelPreferences.
        /// </summary>
        /// <param name="travelPreferences">Object containing loaded user preferences</param>
        protected override void loadModeSpecificPreferences(TDProfile travelPreferences) 
        {
            ProfileProperties curr;

			// IR1473 : Incompatibility between FindA and JourneyPlanner
			// Cater for mixity of format by checking what type of object was stored
			// To get to ONE unique kind of storage, The save preferences will store all data as objects (NOT STRING)
            curr = travelPreferences.Properties[ProfileKeys.INTERCHANGE_LIMIT];
            if (curr != null && curr.Value != null) 
            {
				if (curr.Value is PublicAlgorithmType)
					((TDJourneyParametersMulti)journeyParams).PublicAlgorithmType = 
						(PublicAlgorithmType)curr.Value;
				else
				{	int enumValue = int.Parse((string)curr.Value);
					((TDJourneyParametersMulti)journeyParams).PublicAlgorithmType = (PublicAlgorithmType)enumValue;
				}
            }

            curr = travelPreferences.Properties[ProfileKeys.INTERCHANGE_SPEED];
            if (curr != null && curr.Value != null) 
            {
				if (curr.Value is int)
					((TDJourneyParametersMulti)journeyParams).InterchangeSpeed =(int)curr.Value;
				else
	                ((TDJourneyParametersMulti)journeyParams).InterchangeSpeed = int.Parse((string)curr.Value);
            }
        }

        #endregion Protected Methods

        #region Public Methods

        /// <summary>
        /// Initiates a new search on to, from and via locations  
        /// using the original user's input values. 
        /// </summary>
        /// <param name="locationsControl"></param>
        /// <param name="preferencesControl"></param>
        public void AmbiguitySearch(
            FindToFromLocationsControl locationsControl, 
            FindCoachTrainPreferencesControl preferencesControl )
        {

            base.AmbiguitySearch(locationsControl);

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
        /// Initialises journey parameters with default values for Find A Train journey planning.
        /// Also loads user preferences into journey parameters.
        /// </summary>
        public override void InitJourneyParameters() 
        {
            base.InitJourneyParameters();

            journeyParams.PublicViaType = new 
                TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);
            journeyParams.PublicVia = new LocationSearch();

            string defaultItemValue = populator.GetDefaultListControlValue(DataServiceType.ChangesSpeedDrop);
            journeyParams.InterchangeSpeed = Convert.ToInt32(defaultItemValue);

            defaultItemValue = populator.GetDefaultListControlValue(DataServiceType.ChangesFindDrop);
            journeyParams.PublicAlgorithmType = (PublicAlgorithmType)Enum.Parse(typeof(PublicAlgorithmType),defaultItemValue);

            journeyParams.PrivateRequired = false;

            LoadTravelDetails();
        }

        /// <summary>
        /// Initialises location control for specifying public via locations
        /// </summary>
        public void InitViaLocationsControl(FindCoachTrainPreferencesControl preferencesControl)
        {
            preferencesControl.ViaLocationControl.LocationControlType = journeyParams.PublicViaType;
            preferencesControl.ViaLocationControl.LocationType = CurrentLocationType.PublicVia;
            preferencesControl.ViaLocationControl.TheLocation = journeyParams.PublicViaLocation;
            preferencesControl.ViaLocationControl.TheSearch = journeyParams.PublicVia;
        }

        /// <summary>
        /// Initialises preferences control with journey parameters and page state values
        /// </summary>
        public void InitPreferencesControl(FindCoachTrainPreferencesControl preferencesControl)
        {
            InitViaLocationsControl(preferencesControl);
            preferencesControl.ChangesSpeed = journeyParams.InterchangeSpeed;
            preferencesControl.Changes = journeyParams.PublicAlgorithmType;
            preferencesControl.PreferencesVisible = pageState.TravelDetailsVisible;
        }


        /// <summary>
        /// Initialises controls used on page
        /// </summary>
        public void InitialiseControls(
                FindCoachTrainPreferencesControl preferencesControl, FindToFromLocationsControl locationsControl) 
        {

            InitLocationsControl(locationsControl);
            InitPreferencesControl(preferencesControl);

            if (pageState.AmbiguityMode) 
            {
                preferencesControl.ChangesDisplayMode = GenericDisplayMode.ReadOnly;
                preferencesControl.ChangesSpeedDisplayMode = GenericDisplayMode.ReadOnly;
            } 
            else 
            {
                preferencesControl.ChangesSpeedDisplayMode = GenericDisplayMode.Normal;
                preferencesControl.ChangesDisplayMode = GenericDisplayMode.Normal;
            }

        }

        #endregion Public Methods

    }

	public class FindCoachInputAdapter : FindCoachTrainInputAdapter
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="journeyParams">Journey parameters for Find A coach and train journey planning</param>
		/// <param name="pageState">Page state for Find A coach and train input pages</param>
		public FindCoachInputAdapter(TDJourneyParametersMulti journeyParams, FindCoachTrainPageState pageState, InputPageState inputPageState) : 
		base(journeyParams, pageState, inputPageState)
		{
		}

		/// <summary>
		/// Initialises journey parameters with default values for Find A Train journey planning.
		/// Also loads user preferences into journey parameters.
		/// </summary>
		public override void InitJourneyParameters()
		{
			base.InitJourneyParameters();
			journeyParams.PublicModes = new ModeType[] {ModeType.Coach};
		}

		/// <summary>
		/// Returns station type to be used to filter major stations gazetteer searches.
		/// </summary>
		protected override StationType GetStationTypeFilter()
		{
			return StationType.Coach;
		}

		/// <summary>
		/// Retrieves a JourneyPlanControlData object for Coach plans
		/// </summary>
		/// <returns></returns>
		public override void InitialiseAsyncCallState()
		{
			AsyncCallState acs = new JourneyPlanState();

			// Determine refresh interval and resource string for the wait page
			acs.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.FindACoach"]);
			acs.WaitPageMessageResourceFile = "langStrings";
			acs.WaitPageMessageResourceId = "WaitPageMessage.FindACoach";

			acs.AmbiguityPage = PageId.FindCoachInput;
            acs.DestinationPage = PageId.JourneyDetails;
            acs.ErrorPage = PageId.JourneyDetails;
			acs.Status = AsyncCallStatus.None;
			tdSessionManager.AsyncCallState = acs;
		}

	}

	public class FindTrainInputAdapter : FindCoachTrainInputAdapter
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="journeyParams">Journey parameters for Find A coach and train journey planning</param>
		/// <param name="pageState">Page state for Find A coach and train input pages</param>
		public FindTrainInputAdapter(TDJourneyParametersMulti journeyParams, FindCoachTrainPageState pageState, InputPageState inputPageState) : 
		base(journeyParams, pageState, inputPageState)
		{
		}
		
		/// <summary>
		/// Initialises journey parameters with default values for Find A Train journey planning.
		/// Also loads user preferences into journey parameters.
		/// </summary>
		public override void InitJourneyParameters()
		{
			base.InitJourneyParameters();
			journeyParams.PublicModes = new ModeType[] {ModeType.Rail};

            // Set routing guide flags
            journeyParams.RoutingGuideInfluenced = bool.Parse(Properties.Current["RoutingGuide.FindATrain.RoutingGuideInfluenced"]);
            journeyParams.RoutingGuideCompliantJourneysOnly = bool.Parse(Properties.Current["RoutingGuide.FindATrain.RoutingGuideCompliantJourneysOnly"]);
		}

		/// <summary>
		/// Returns station type to be used to filter major stations gazetteer searches.
		/// </summary>
		protected override StationType GetStationTypeFilter()
		{
			return StationType.Rail;
		}

		/// <summary>
		/// Retrieves a JourneyPlanControlData object for Train plans
		/// </summary>
		/// <returns></returns>
		public override void InitialiseAsyncCallState()
		{
			AsyncCallState acs = new JourneyPlanState();

			// Determine refresh interval and resource string for the wait page
			acs.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.FindATrain"]);
			acs.WaitPageMessageResourceFile = "langStrings";
			acs.WaitPageMessageResourceId = "WaitPageMessage.FindATrain";

			acs.AmbiguityPage = PageId.FindTrainInput;
            acs.DestinationPage = PageId.JourneyDetails;
            acs.ErrorPage = PageId.JourneyDetails;
			acs.Status = AsyncCallStatus.None;
			tdSessionManager.AsyncCallState = acs;
		}

	}
}
