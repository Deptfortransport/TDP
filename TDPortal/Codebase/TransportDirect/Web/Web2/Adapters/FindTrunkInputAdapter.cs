// *********************************************** 
// NAME                 : FindTrunkInputAdapter.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 29/07/2004 
// DESCRIPTION		: Extends/modifies common functionality of base
// class to provide specific behaviour for Find A Trunk input pages
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/FindTrunkInputAdapter.cs-arc  $ 
//
//   Rev 1.3   Feb 02 2009 17:04:38   mmodi
//Populate Routing Guide properties	
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.2   Mar 31 2008 12:59:02   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Jan 31 2008 14:00:00 mmodi
//Updated InitJourneyParameters() method to only populate for a City to City Car request
//if database property value is set to true
//
//   Rev 1.0   Nov 08 2007 13:11:20   mturner
//Initial revision.
//
//   Rev DevFactory Jan 20 2008 19:00:00 dgath
//CCN0382b City to City enhancements:
//InitJourneyParameters() method updated to check for a city to city request
//(FindAMode.Trunk), and if so set the PrivateRequested value to true, 
//as well as set private (car) journey parameters to default values
//
//   Rev 1.11   Apr 05 2006 15:42:50   build
//Automatically merged from branch for stream0030
//
//   Rev 1.10.1.0   Mar 29 2006 11:15:30   RGriffith
//Wait Page Ehancement changes
//Resolution for 33: DEL 8.1 Workstream: Wait Pages
//
//   Rev 1.10   Feb 23 2006 19:16:10   build
//Automatically merged from branch for stream3129
//
//   Rev 1.9.1.0   Jan 10 2006 15:17:38   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.9   Nov 09 2005 12:31:56   build
//Automatically merged from branch for stream2818
//
//   Rev 1.8.1.0   Oct 14 2005 15:28:20   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.8   Jan 25 2005 12:14:04   rhopkins
//Refactor ...InputAdapter classes to allow FindInputAdapter to be inherited by FindFareInputAdapter.
//
//   Rev 1.7   Nov 25 2004 11:17:16   jgeorge
//Added code to set Direct Flights only option according to configurable properties
//Resolution for 1785: City to City should only plan direct flights
//
//   Rev 1.6   Nov 02 2004 15:55:14   passuied
//Changes in Title, error messages and instruction for FindTrunkInput
//
//   Rev 1.5   Oct 15 2004 12:36:18   jgeorge
//Added GetJourneyPlanControlData method
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.4   Aug 27 2004 14:19:32   passuied
//Little fix set the SearchType to Locality when Initialising journeyParameters
//Resolution for 1444: Find A Car : Missing Back button at top of page
//
//   Rev 1.3   Aug 20 2004 14:02:24   RPhilpott
//Set PrivateRequired = false in initialisation.
//Resolution for 1411: Car journey request being formatted for Find Trunk
//
//   Rev 1.2   Aug 20 2004 10:46:20   passuied
//Included setting of public modes in InitJourneyParameters.
//Resolution for 1348: Not possible to plan Find A Train journey using Find A Station/Airport
//
//   Rev 1.1   Aug 02 2004 15:52:18   esevern
//added inputpagestate param
//
//   Rev 1.0   Jul 29 2004 16:16:40   esevern
//Initial revision.

using System;
using TransportDirect.Common.ResourceManager;
using System.Web.UI.WebControls;

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
	/// Summary description for FindTrunkInputAdapter.
	/// </summary>
	public class FindTrunkInputAdapter : FindJourneyInputAdapter
	{
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="journeyParams">Journey parameters for Find A Trunk journey planning</param>
		/// <param name="pageState">Page state for Find A Trunk input pages</param>
		/// <param name="inputPageState">Variables indicating state of page, return stack for navigation etc.</param>
		public FindTrunkInputAdapter(TDJourneyParametersMulti journeyParams, FindTrunkPageState pageState, InputPageState inputPageState) : 
			base(pageState, TDSessionManager.Current, inputPageState, journeyParams)
		{
		}

		#region Public Methods

		/// <summary>
        /// Updates supplied label with message to correct errors. Message depends on errors found in supplied
        /// errors object. The visibility of panelErrorMessage is set to true if the label is set with a message
        /// or false if not.
        /// </summary>
        /// <param name="panelErrorMessage">Panel to set visibility of</param>
        /// <param name="labelErrorMessages">Label to update with message</param>
        /// <param name="errors">errors to search</param>
        public override void UpdateErrorMessages(Panel panelErrorMessage, Label labelErrorMessages, ValidationError errors)
        {
			// Replace the resource key for when locations are unspecified 
			// Use a special one for FindTrunk Input
			LocationsUnspecifiedKey = "ValidateAndRun.UpdateHighlighted";

			base.UpdateErrorMessages(panelErrorMessage, labelErrorMessages, errors);
                
        }
		/// <summary>
		/// Initialises journey parameters with default values for Find A Train journey planning.
		/// Also loads user preferences into journey parameters.
		/// </summary>
        public override void InitJourneyParameters()
        {
            base.InitJourneyParameters();

            #region Common parameters
            journeyParams.Origin.SearchType = SearchType.Locality;
            journeyParams.Destination.SearchType = SearchType.Locality;

            journeyParams.PublicModes = new ModeType[] { ModeType.Rail, ModeType.Air, ModeType.Coach };
            journeyParams.Origin.DisableGisQuery();
            journeyParams.Destination.DisableGisQuery();

            if (pageState is FindTrunkStationPageState)
            {
                // Find Station/Airport should search for direct and indirect flights
                journeyParams.DirectFlightsOnly = bool.Parse(Properties.Current["FindTrunk.DirectFlightsOnly.StationAirport"]);
            }
            else
            {
                // Find City-to-City should only search for direct flights
                journeyParams.DirectFlightsOnly = bool.Parse(Properties.Current["FindTrunk.DirectFlightsOnly.CityToCity"]);
            }
            #endregion

            // Setup Car required parameters, if Trunk mode
            journeyParams.PrivateRequired = false;

            if (pageState.Mode == FindAMode.Trunk)
            {
                // Get property value do determine if we need car journey
                bool useCar = false;

                try
                {
                    useCar = (bool.Parse(Properties.Current[JourneyControlConstants.UseCarsInCityToCity]));
                }
                catch 
                {
                    useCar = false;
                }

                if (useCar)
                {
                    #region Car options
                    IDataServices populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

                    journeyParams.AvoidMotorWays = false;
                    journeyParams.AvoidFerries = false;
                    journeyParams.AvoidTolls = false;
                    journeyParams.DoNotUseMotorways = false;

                    string defaultItemValue = populator.GetDefaultListControlValue(DataServiceType.DrivingFindDrop);

                    journeyParams.PrivateAlgorithmType = (PrivateAlgorithmType)Enum.Parse(typeof(PrivateAlgorithmType), defaultItemValue);

                    journeyParams.DrivingSpeed = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.DrivingMaxSpeedDrop), TDCultureInfo.CurrentCulture);

                    journeyParams.CarSize = populator.GetDefaultListControlValue(DataServiceType.ListCarSizeDrop);
                    journeyParams.CarFuelType = populator.GetDefaultListControlValue(DataServiceType.ListFuelTypeDrop);
                    journeyParams.FuelConsumptionUnit = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.UnitsDrop), TDCultureInfo.CurrentCulture);

                    // Set the default Fuel Cost and Fuel Consumption
                    CarCostCalculator costCalculator = (CarCostCalculator)TDServiceDiscovery.Current[ServiceDiscoveryKey.CarCostCalculator];

                    journeyParams.FuelConsumptionEntered = string.Format(costCalculator.GetFuelConsumption(journeyParams.CarSize, journeyParams.CarFuelType).ToString());

                    journeyParams.FuelCostEntered = string.Format(costCalculator.GetFuelCost(journeyParams.CarFuelType).ToString());

                    #endregion

                    journeyParams.PrivateRequired = true;
                }
            }

            // Set routing guide flags
            if (pageState.Mode == FindAMode.Trunk)
            {
                journeyParams.RoutingGuideInfluenced = bool.Parse(Properties.Current["RoutingGuide.CityToCity.RoutingGuideInfluenced"]);
                journeyParams.RoutingGuideCompliantJourneysOnly = bool.Parse(Properties.Current["RoutingGuide.CityToCity.RoutingGuideCompliantJourneysOnly"]);
            }
            else if (pageState.Mode == FindAMode.TrunkStation)
            {
                journeyParams.RoutingGuideInfluenced = bool.Parse(Properties.Current["RoutingGuide.FindNearestStation.RoutingGuideInfluenced"]);
                journeyParams.RoutingGuideCompliantJourneysOnly = bool.Parse(Properties.Current["RoutingGuide.FindNearestStation.RoutingGuideCompliantJourneysOnly"]);
            }
        }

		/// <summary>
		/// Initialises FindToFromLocationsControl used on page
		/// </summary>
		public void InitialiseControls(FindToFromLocationsControl locationsControl) 
		{
			InitLocationsControl(locationsControl);
			
			// The location search object used to resolve location should call 
			// DisableGisQuery to prevent a nearest Naptan and TOIUD lookup.
			locationsControl.OriginSearch.DisableGisQuery();
			locationsControl.DestinationSearch.DisableGisQuery();


		}

		/// <summary>
		/// Retrieves a AsyncCallState object for Trunk plans
		/// </summary>
		/// <returns></returns>
		public override void InitialiseAsyncCallState()
		{
			AsyncCallState acs = new JourneyPlanState();

			// Determine refresh interval and resource string for the wait page
			acs.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.CityToCity"]);
			acs.WaitPageMessageResourceFile = "langStrings";
			acs.WaitPageMessageResourceId = "WaitPageMessage.CityToCity";

			acs.AmbiguityPage = PageId.FindTrunkInput;
			acs.Status = AsyncCallStatus.None;

            // Depending on Trunk mode, send to JourneyOverview or JourneySummary
            if (tdSessionManager.FindAMode == FindAMode.Trunk)
            {
                acs.DestinationPage = PageId.JourneyOverview;
                acs.ErrorPage = PageId.JourneyOverview;
            }
            else
            {
                acs.DestinationPage = PageId.JourneyDetails;
                acs.ErrorPage = PageId.JourneyDetails;
            }

			tdSessionManager.AsyncCallState = acs;
		}

        #endregion Public Methods

	}
}
