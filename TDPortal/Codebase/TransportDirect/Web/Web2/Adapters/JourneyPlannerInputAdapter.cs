// *********************************************** 
// NAME                 : JourneyPlannerInputAdapter.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 31/10/2005 
// DESCRIPTION  		: The adapter class to handle Ambiguity search and validation. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/JourneyPlannerInputAdapter.cs-arc  $ 
//
//   Rev 1.4   Jan 09 2013 14:16:54   mmodi
//Update Validate method to accept transition events and return a success bool
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.3   Dec 18 2012 16:54:34   dlane
//Accessible JP updates
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.2   Mar 31 2008 12:59:04   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:24   mturner
//Initial revision.
//
//   Rev 1.7   Apr 05 2006 15:42:50   build
//Automatically merged from branch for stream0030
//
//   Rev 1.6.1.0   Mar 29 2006 11:15:30   RGriffith
//Wait Page Ehancement changes
//Resolution for 33: DEL 8.1 Workstream: Wait Pages
//
//   Rev 1.6   Feb 23 2006 17:06:12   RWilby
//Merged stream3129
//
//   Rev 1.5   Feb 10 2006 12:24:24   tolomolaiye
//Merge for stream 3180 - Homepage Phase 2
//
//   Rev 1.4   Jan 11 2006 10:31:40   mguney
//SetFuelCostConsumption method included.
//Resolution for 3439: Homepage: Mini Journey planner does not include car journeys
//
//   Rev 1.3   Nov 23 2005 18:45:42   RGriffith
//Code review changes to comments
//
//   Rev 1.2.1.1   Jan 11 2006 13:53:40   tmollart
//Updated after comments from code review. Removed redundant code.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.2.1.0   Dec 22 2005 10:18:18   tmollart
//Removed calls to now redudant SaveCurrentFindaMode.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.2   Nov 17 2005 15:37:52   pcross
//Merge fix
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.1   Nov 02 2005 18:31:50   schand
//Added additional function for FindAPlaceControl
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.0   Nov 01 2005 13:58:50   schand
//Initial revision.


using System;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.ResourceManager;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.JourneyPlanRunner;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.UserSupport;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Presentation.InteractiveMapping;
  
using Logger = System.Diagnostics.Trace;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;
using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
using ControlPopulator = TransportDirect.UserPortal.DataServices.DataServices;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// The adapter class to handle Ambiguity search and validation.
	/// </summary>
	public class JourneyPlannerInputAdapter   : TDWebAdapter
	{
		/// <summary>
		/// Empty constructor to allow initialisation
		/// </summary>
		public JourneyPlannerInputAdapter()
		{
		}
        
		/// <summary>
		/// Ambiguity Search Method
		/// </summary>
		/// <param name="journeyParametersLocation"></param>
		/// <param name="journeyParametersSearch"></param>
		/// <param name="acceptsPostcode"></param>
		/// <param name="acceptsPartPostcode"></param>
		public static void AmbiguitySearch(
			ref TDLocation journeyParametersLocation,
			ref LocationSearch journeyParametersSearch,
			TDJourneyParameters journeyParameters,
			bool acceptsPostcode,
			bool acceptsPartPostcode
			)
		{

			StationType stationType;

			if (journeyParametersLocation.Status == TDLocationStatus.Unspecified) 
			{

				TDLocation newLocation = new TDLocation();
				LocationSearch newSearch = new LocationSearch();

				newSearch.InputText = journeyParametersSearch.InputText;
				newSearch.SearchType = journeyParametersSearch.SearchType;
				newSearch.FuzzySearch = journeyParametersSearch.FuzzySearch;
				newSearch.NoGroup = journeyParametersSearch.NoGroup;

				//Added for DEL7 additional tasks
				//if the search is for the PTPreferencesControl then do not use grouping
				if (newSearch.NoGroup)
				{
					stationType = StationType.UndeterminedNoGroup;
				}
				else
				{
					stationType = StationType.Undetermined;
				}

				LocationSearchHelper.SetupLocationParameters(
					journeyParametersSearch.InputText,
					journeyParametersSearch.SearchType,
					journeyParametersSearch.FuzzySearch,
					0,
					journeyParameters.MaxWalkingTime,
					journeyParameters.WalkingSpeed,
					ref newSearch,
					ref newLocation,
					acceptsPostcode,
					acceptsPartPostcode,
					stationType
					);

				journeyParametersLocation = newLocation;
				journeyParametersSearch = newSearch;

			}
		}

        /// <summary>
		/// This method will create the instance if JourneyPlanControlData and call JourneyPlanRunner method
		/// </summary>
		/// <param name="stopoverValid">Stop over is valid</param>
        public bool ValidateAndSearch(bool stopoverValid, PageId pageId)
        {
            return ValidateAndSearch(stopoverValid, pageId, TransitionEvent.JourneyPlannerInputOK, TransitionEvent.JourneyPlannerInputErrors);
        }

		/// <summary>
		/// This method will create the instance if JourneyPlanControlData and call JourneyPlanRunner method
		/// </summary>
		/// <param name="stopoverValid">Stop over is valid</param>
		public bool ValidateAndSearch(bool stopoverValid, PageId pageId, 
            TransitionEvent validTransition, TransitionEvent errorTransition)
		{
			AsyncCallState jps = new JourneyPlanState();

			// Determine refresh interval and resource string for the wait page
			jps.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.DoorToDoor"]);
			jps.WaitPageMessageResourceFile = "langStrings";
			jps.WaitPageMessageResourceId = "WaitPageMessage.DoorToDoor";

            jps.ErrorPage = PageId.JourneyDetails;
            jps.DestinationPage = PageId.JourneyDetails;
			jps.AmbiguityPage = PageId.JourneyPlannerAmbiguity;
			TDSessionManager.Current.AsyncCallState = jps;
			
			// Journey Plan Runner
			JourneyPlanRunner.JourneyPlanRunner runner = new JourneyPlanRunner.JourneyPlanRunner(resourceManager);

			if (runner.ValidateAndRun(
				TDSessionManager.Current,
				TDSessionManager.Current.JourneyParameters,
				TDPage.GetChannelLanguage(TDPage.SessionChannelName),
				stopoverValid))
			{
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = validTransition;

                return true;
			}
			else
			{
                // If the only errors are that locations are not accessible then
                // redirect to the find nearest accessible stop page
                ValidationErrorID[] errors = TDSessionManager.Current.ValidationError.ErrorIDs;

                foreach (ValidationErrorID errorId in errors)
                {
                    if ((errorId != ValidationErrorID.OriginLocationNotAccessible) &&
                        (errorId != ValidationErrorID.DestinationLocationNotAccessible) &&
                        (errorId != ValidationErrorID.PublicViaLocationNotAccessible))
                    {
                        // not (just) accessible error
                        TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(pageId);
                        TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = errorTransition;
                        return false;
                    }
                }

                // Go to accessible stops page
                TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(pageId);
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindNearestAccessibleStop;
            }

            return false;
		}

		/// <summary>
		/// This method sets the transition for map.
		/// </summary>
		public static void SetTransitionForMap()
		{
			// set transition for Map section
			ITDSessionManager sessionManager 
				= (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];
			sessionManager.FormShift[SessionKey.TransitionEvent]= TransitionEvent.GoMap;            
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
		}

		/// <summary>
		/// Sets the fuel cost-consumption for the journey parameters.
		/// </summary>
		/// <param name="journeyParameters"></param>
		public static void SetFuelCostConsumption(TDJourneyParametersMulti journeyParameters)
		{
			IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];				
			CarCostCalculator costCalculator = (CarCostCalculator)TDServiceDiscovery.Current[ ServiceDiscoveryKey.CarCostCalculator ];
			string carSize = ds.GetDefaultListControlValue(DataServiceType.ListCarSizeDrop);
			string fuelType = ds.GetDefaultListControlValue(DataServiceType.ListFuelTypeDrop);
			// Get default fuel consumption
			journeyParameters.FuelConsumptionOption = true;
			journeyParameters.FuelConsumptionEntered = costCalculator.GetFuelConsumption(carSize, fuelType).ToString(CultureInfo.InvariantCulture);
			// Get default fuel cost
			journeyParameters.FuelCostOption = true;
			journeyParameters.FuelCostEntered = costCalculator.GetFuelCost(fuelType).ToString(CultureInfo.InvariantCulture);
		}
	}
}