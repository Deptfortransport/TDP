// *********************************************** 
// NAME                 : UserPreferencesHelper.cs
// AUTHOR               : Peter Norell
// DATE CREATED         : 02/03/2005 
// DESCRIPTION			: Convience/Helper class for loading and storing preferences.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/UserPreferencesHelper.cs-arc  $
//
//   Rev 1.5   Nov 16 2012 14:00:50   DLane
//Setting walk parameters in the journey request
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.4   Nov 15 2012 14:07:10   dlane
//Addition of accessibility options to journey plan input and user profile
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.3   Oct 13 2008 16:41:28   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.1   Oct 10 2008 15:58:48   mmodi
//Updated to have avoid time based check
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.0   Sep 09 2008 13:20:08   mmodi
//Updated to loadsave cycle preferences
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 12:59:16   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:32   mturner
//Initial revision.
//
//   Rev 1.5   Feb 23 2006 17:17:46   RWilby
//Merged stream3129
//
//   Rev 1.4   Nov 25 2005 14:28:36   tolomolaiye
//Allow user preferences to be saved. Fix for IR 3176
//Resolution for 3176: Visit Planner  - When a logged-in user saves their Advanced preferences the options chosen are not saved

using System;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.PropertyService.Properties;
using ControlPopulator = TransportDirect.UserPortal.DataServices.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Convience/Helper class for loading and storing preferences between permanent user
	/// storage and session parameters.
	/// </summary>
	public class UserPreferencesHelper
	{
        private static ControlPopulator populator = (ControlPopulator)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

		/// <summary>
		/// Saves car preferences. It will presume there is a user that is logged on.
		/// </summary>
		/// <param name="parameters">The journey parameters to pick the settings from</param>
		public static void SaveCarPreferences( TDJourneyParametersMulti parameters)
		{
			TDUserJourneyPreferences prefs = TDSessionManager.Current.CurrentUser.JourneyPreferences;

			prefs.CarPreferencesSet = true;

			#region Car preferences
			prefs.DrivingType = parameters.PrivateAlgorithmType;
			prefs.DrivingSpeed = parameters.DrivingSpeed;
			prefs.DoNotUseMotorways = parameters.DoNotUseMotorways;

			prefs.CarSize =  parameters.CarSize;
			prefs.CarFuelType = parameters.CarFuelType;
			prefs.DefaultFuelConsumption = parameters.FuelConsumptionOption;
			if( !parameters.FuelConsumptionOption )
			{
				prefs.FuelConsumption = parameters.FuelConsumptionEntered;
				prefs.FuelConsumptionUnit = parameters.FuelConsumptionUnit;
			}
			prefs.FuelConsumption =  parameters.FuelConsumptionEntered;
			prefs.DefaultFuelCost = parameters.FuelCostOption;
			if( !parameters.FuelCostOption )
			{
				prefs.FuelCost = parameters.FuelCostEntered;
			}
			#endregion

		}

        /// <summary>
        /// Saves accessibility preferences. It will presume there is a user that is logged on.
        /// </summary>
        /// <param name="parameters">The journey parameters to pick the settings from</param>
        public static void SaveAccessibilityPreferences(TDJourneyParametersMulti parameters)
        {
            TDUserJourneyPreferences prefs = TDSessionManager.Current.CurrentUser.JourneyPreferences;

            if (bool.Parse(Properties.Current["AccessibleOptions.Visible.Switch"]))
            {
                prefs.AccessibilityPreferencesSet = true;

                #region Acccessiblity preferences
                prefs.RequireSpecialAssistance = parameters.RequireSpecialAssistance;
                prefs.RequireStepFreeAccess = parameters.RequireStepFreeAccess;
                prefs.RequireFewerChanges = parameters.RequireFewerInterchanges;

                // Set the accessible walking speed & time if acessible options specified 
                // and walking options are not default values
                if (prefs.RequireSpecialAssistance || prefs.RequireStepFreeAccess || prefs.RequireFewerChanges)
                {
                    if ((parameters.MaxWalkingTime != int.Parse(populator.GetDefaultListControlValue(DataServiceType.WalkingMaxTimeDrop)))
                        || (parameters.WalkingSpeed != int.Parse(populator.GetDefaultListControlValue(DataServiceType.WalkingSpeedDrop))))
                    {
                        prefs.AccessibleWalkSpeed = 0;
                        prefs.AccessibleWalkDistance = 0;
                    }
                    else
                    {
                        prefs.AccessibleWalkSpeed = int.Parse(Properties.Current["AccessibleOptions.WalkingSpeed.MetresPerMinute"]);
                        prefs.AccessibleWalkDistance = int.Parse(Properties.Current["AccessibleOptions.WalkingDistance.Metres"]);
                    }
                }
                else
                {
                    prefs.AccessibleWalkSpeed = 0;
                    prefs.AccessibleWalkDistance = 0;
                }
                #endregion
            }
            else
            {
                prefs.AccessibilityPreferencesSet = false;
                prefs.RequireSpecialAssistance = false;
                prefs.RequireStepFreeAccess = false;
                prefs.RequireFewerChanges = false;
                prefs.AccessibleWalkSpeed = 0;
                prefs.AccessibleWalkDistance = 0;
            }
        }

        /// <summary>
        /// Saves cycle preferences. It will presume there is a user that is logged on.
        /// </summary>
        /// <param name="parameters">The journey parameters to pick the settings from</param>
        public static void SaveCyclePreferences(TDJourneyParametersMulti parameters)
        {
            TDUserJourneyPreferences prefs = TDSessionManager.Current.CurrentUser.JourneyPreferences;

            prefs.CyclePreferencesSet = true;

            #region Cycle preferences
            prefs.CycleJourneyType = parameters.CycleJourneyType;
            
            prefs.CycleSpeedText = parameters.CycleSpeedText;
            prefs.CycleSpeedUnit = parameters.CycleSpeedUnit;

            prefs.CycleAvoidSteepClimbs = parameters.CycleAvoidSteepClimbs;
            prefs.CycleAvoidUnlitRoads = parameters.CycleAvoidUnlitRoads;
            prefs.CycleAvoidWalkingBike = parameters.CycleAvoidWalkingYourBike;
            prefs.CycleAvoidTimeBased = parameters.CycleAvoidTimeBased;
            
            #endregion

        }

		/// <summary>
		/// Save Public Transport Preferences for Visit Planner
		/// </summary>
		/// <param name="visitParameters">The Visit Planner parameter</param>
		public static void SavePublicTransportPreferences(TDJourneyParametersVisitPlan visitParameters)
		{
			TDUserJourneyPreferences prefs = TDSessionManager.Current.CurrentUser.JourneyPreferences;
			prefs.PublicTransportPreferencesSet = true;
			prefs.InterChangeSpeed = visitParameters.InterchangeSpeed;
			prefs.PublicTransportAlgorithm = visitParameters.PublicAlgorithmType;
			prefs.WalkingSpeed = visitParameters.WalkingSpeed;
			prefs.MaxWalkingTime = visitParameters.MaxWalkingTime;
		}

		/// <summary>
		/// Saves public transport preferences. It will presume there is a user that is logged on.
		/// </summary>
		/// <param name="parameters">The journey parameters to pick the settings from</param>
		public static void SavePublicTransportPreferences( TDJourneyParametersMulti parameters)
		{
			TDUserJourneyPreferences prefs = TDSessionManager.Current.CurrentUser.JourneyPreferences;

			prefs.PublicTransportPreferencesSet = true;

			#region Public transport preferences
			prefs.InterChangeSpeed = parameters.InterchangeSpeed;

			prefs.PublicTransportAlgorithm = parameters.PublicAlgorithmType;

			prefs.WalkingSpeed = parameters.WalkingSpeed;

			prefs.MaxWalkingTime = parameters.MaxWalkingTime;
			#endregion
		}

		/// <summary>
		/// Loads car preferences. Car preferences will only be loaded if there is a user logged on and
		/// the user has car preferences saved since before.
		/// </summary>
		/// <param name="parameters">The parameters where the preferences are loaded</param>
		/// <returns>true if any preferences where loaded</returns>
		public static bool LoadCarPreferences( TDJourneyParametersMulti parameters )
		{
			if( TDSessionManager.Current.Authenticated )
			{
				TDUserJourneyPreferences prefs = TDSessionManager.Current.CurrentUser.JourneyPreferences;
				if( prefs.CarPreferencesSet )
				{
					#region Car preferences
					parameters.PrivateAlgorithmType = prefs.DrivingType;
					parameters.DrivingSpeed = prefs.DrivingSpeed;
					parameters.DoNotUseMotorways = prefs.DoNotUseMotorways;


					if( prefs.CarSize != string.Empty )
					{
						parameters.CarSize = prefs.CarSize;
					}
					if( prefs.CarFuelType != string.Empty )
					{
						parameters.CarFuelType = prefs.CarFuelType;
					}
					parameters.FuelConsumptionOption = prefs.DefaultFuelConsumption;
					if( !prefs.DefaultFuelConsumption )
					{
						parameters.FuelConsumptionEntered = prefs.FuelConsumption;
						parameters.FuelConsumptionUnit = prefs.FuelConsumptionUnit;
					}
					parameters.FuelCostOption = prefs.DefaultFuelCost;
					if( !prefs.DefaultFuelCost )
					{
						parameters.FuelCostEntered = prefs.FuelCost;
					}
					#endregion

					parameters.DefaultValues = false;

					return true;
				}
			}
			return false;
		}

        /// <summary>
        /// Loads accessiblity preferences. Preferences will only be loaded if there is a user logged on and
        /// the user has accessiblity preferences saved.
        /// </summary>
        /// <param name="parameters">The parameters where the preferences are loaded to</param>
        /// <returns>true if any preferences where loaded</returns>
        public static bool LoadAccessiblityPreferences(TDJourneyParametersMulti parameters)
        {
            if (TDSessionManager.Current.Authenticated)
            {
                if (bool.Parse(Properties.Current["AccessibleOptions.Visible.Switch"]))
                {
                    TDUserJourneyPreferences prefs = TDSessionManager.Current.CurrentUser.JourneyPreferences;
                    if (prefs.AccessibilityPreferencesSet)
                    {
                        #region Accessibility preferences
                        parameters.RequireSpecialAssistance = prefs.RequireSpecialAssistance;
                        parameters.RequireStepFreeAccess = prefs.RequireStepFreeAccess;
                        parameters.RequireFewerInterchanges = prefs.RequireFewerChanges;
                        parameters.AccessibleWalkSpeed = prefs.AccessibleWalkSpeed;
                        parameters.AccessibleWalkDistance = prefs.AccessibleWalkDistance;
                        #endregion

                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Loads cycle preferences. Cycle preferences will only be loaded if there is a user logged on and
        /// the user has cycle preferences saved since before.
        /// </summary>
        /// <param name="parameters">The parameters where the preferences are loaded</param>
        /// <returns>true if any preferences where loaded</returns>
        public static bool LoadCyclePreferences(TDJourneyParametersMulti parameters)
        {
            if (TDSessionManager.Current.Authenticated)
            {
                TDUserJourneyPreferences prefs = TDSessionManager.Current.CurrentUser.JourneyPreferences;
                if (prefs.CyclePreferencesSet)
                {
                    #region Cycle preferences
                    
                    parameters.CycleJourneyType = prefs.CycleJourneyType;

                    parameters.CycleSpeedText = prefs.CycleSpeedText;
                    parameters.CycleSpeedUnit = prefs.CycleSpeedUnit;

                    parameters.CycleAvoidSteepClimbs = prefs.CycleAvoidSteepClimbs;
                    parameters.CycleAvoidUnlitRoads = prefs.CycleAvoidUnlitRoads;
                    parameters.CycleAvoidWalkingYourBike = prefs.CycleAvoidWalkingBike;
                    parameters.CycleAvoidTimeBased = prefs.CycleAvoidTimeBased;

                    #endregion

                    parameters.DefaultValues = false;

                    return true;
                }
            }
            return false;
        }

		/// <summary>
		/// Loads public transport preferences. Public transport preferences will only be loaded if there is a user logged on and
		/// the user has public transport preferences saved since before.
		/// </summary>
		/// <param name="parameters">The parameters where the preferences are loaded</param>
		/// <returns>true if any preferences where loaded</returns>
		public static bool LoadPublicTransportPreferences( TDJourneyParametersMulti parameters )
		{
			if( TDSessionManager.Current.Authenticated )
			{
				TDUserJourneyPreferences prefs = TDSessionManager.Current.CurrentUser.JourneyPreferences;
				if( prefs.PublicTransportPreferencesSet )
				{
					#region Public transport preferences
					parameters.InterchangeSpeed = prefs.InterChangeSpeed;

					parameters.PublicAlgorithmType = prefs.PublicTransportAlgorithm;

					// =< 0 in walking speed means the walking speed has not been set properly
					// earlier.
					if( prefs.WalkingSpeed > 0 )
					{
						parameters.WalkingSpeed = prefs.WalkingSpeed;
					}

					// =< 0 in walking time means that the walking time has not been set properly
					// earlier when preferences was saved.
					if( prefs.MaxWalkingTime > 0 )
					{
						parameters.MaxWalkingTime = prefs.MaxWalkingTime;
					}
					#endregion

					parameters.DefaultValues = false;

					return true;
				}			
			}
			return false;
		}

		/// <summary>
		/// Loads public transport preferences for VisitPlanner. Public transport preferences 
		/// will only be loaded if there is a user logged on and
		/// the user has public transport preferences saved since before.
		/// </summary>
		/// <param name="parameters">The parameters where the preferences are loaded</param>
		/// <returns>true if any preferences where loaded</returns>
		public static bool LoadPublicTransportPreferences( TDJourneyParametersVisitPlan visitParameters )
		{
			if( TDSessionManager.Current.Authenticated )
			{
				TDUserJourneyPreferences prefs = TDSessionManager.Current.CurrentUser.JourneyPreferences;
				if( prefs.PublicTransportPreferencesSet )
				{
					#region Public transport preferences
					visitParameters.InterchangeSpeed = prefs.InterChangeSpeed;

					visitParameters.PublicAlgorithmType = prefs.PublicTransportAlgorithm;

					// =< 0 in walking speed means the walking speed has not been set properly
					// earlier.
					if( prefs.WalkingSpeed > 0 )
					{
						visitParameters.WalkingSpeed = prefs.WalkingSpeed;
					}

					// =< 0 in walking time means that the walking time has not been set properly
					// earlier when preferences was saved.
					if( prefs.MaxWalkingTime > 0 )
					{
						visitParameters.MaxWalkingTime = prefs.MaxWalkingTime;
					}
					#endregion

					visitParameters.DefaultValues = false;

					return true;
				}			
			}
			return false;
		}

		
	}
}

