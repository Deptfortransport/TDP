// *********************************************** 
// NAME                 : JourneyEmissionsHelper.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/11/2006 
// DESCRIPTION			: Class providing helper methods for Journey Emissions.
//                      : All of the emissions calculation logic has been moved in to JourneyEmissions.JourneyEmissionsCalculator().
//                      : This class is now used as a wrapper which accepts session objects and wraps/passes to the session independent
//                      : calculator class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/JourneyEmissionsHelper.cs-arc  $ 
//
//   Rev 1.9   Feb 19 2010 12:09:12   mmodi
//Updated to check if journey alread has emissions calculated and return that value
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.8   Feb 18 2010 19:12:20   rbroddle
//International planner amendments
//
//   Rev 1.7   Feb 17 2010 10:22:56   rbroddle
//Updates for international planner
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.6   Aug 04 2009 14:22:28   mmodi
//Updated to wrap calls to the JourneyEmissionsCalculator
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.5   Oct 14 2008 11:27:44   mmodi
//Manual merge for stream5014
//
//   Rev 1.4   Sep 10 2008 12:01:34   mmodi
//Corrected use of Coach emission factor
//Resolution for 5109: CO2 Emissions: Coach factor is not used for coach mode journey
//
//   Rev 1.3.1.0   Jul 30 2008 11:06:44   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   May 21 2008 16:16:16   mmodi
//Check for null services
//Resolution for 4997: Find a flight modify and CO2 errors
//
//   Rev 1.2   Mar 31 2008 12:59:04   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:24   mturner
//Initial revision.
//
//   Rev 1.19   Sep 21 2007 14:06:58   asinclair
//Using Originalfuel values rather than page state as the user may have changed them
//
//   Rev 1.18   Sep 13 2007 19:56:42   asinclair
//Added GetFuelUsed() for CO2 Phase 2
//
//   Rev 1.17   Aug 31 2007 16:20:04   build
//Automatically merged from branch for stream4474
//
//   Rev 1.16.1.0   Aug 30 2007 18:04:18   asinclair
//Major updates for CO2 phase 2
//Resolution for 4474: DEL 9.7 Stream : Public Transport C02
//
//   Rev 1.16   May 14 2007 14:12:02   mmodi
//Changed Car emissions to use values from Properties
//Resolution for 4407: CO2: Change default car in PT CO2 emissions to be a database parameter
//
//   Rev 1.15   Apr 10 2007 15:54:52   mmodi
//Updated GetEmissions for ModeType Car to use session car type parameters
//Resolution for 4382: CO2: Car emision factor used does not use the car type selected by user
//
//   Rev 1.14   Apr 04 2007 14:55:56   mmodi
//Changed Fuel types used in calculating scale values
//Resolution for 4378: CO2: CO2 emissions speedos scale max and min vary with the input fuel type
//
//   Rev 1.13   Apr 03 2007 17:44:58   mmodi
//Added GetEmissions to return value for "YourJourney" method
//Resolution for 4374: CO2: Mpg value is not used when viewing PT Emissions from Car Emissions
//
//   Rev 1.12   Mar 21 2007 14:20:12   asinclair
//Now calling GetEmissions() without passing in the pageState value
//
//   Rev 1.11   Mar 09 2007 16:25:30   mmodi
//Updated to use specific Air emission factors
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.10   Mar 06 2007 12:29:52   Build
//Automatically merged from branch for stream4350
//
//   Rev 1.9.1.4   Mar 05 2007 17:23:58   mmodi
//Check for valid journey before calculating distance
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.9.1.3   Mar 02 2007 16:41:50   mmodi
//Updated bus distance calculation
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.9.1.2   Feb 28 2007 15:46:06   mmodi
//Updates are part of development work stream (journey modes, rounding, validating distance, car congestion factor)
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.9.1.1   Feb 26 2007 11:52:40   mmodi
//Corrected emissions rounding
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.9.1.0   Feb 20 2007 17:14:24   mmodi
//Added methods to calculate emissions for PT
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.9   Jan 02 2007 12:36:14   mmodi
//Included check for Null journey params
//Resolution for 4327: Error when viewing Costs for Modified Find a Flight journey
//
//   Rev 1.8   Dec 15 2006 11:03:30   mmodi
//Updated code to use decimal values for Scale values
//Resolution for 4321: CO2: Use accurate Scales when calculating angle
//
//   Rev 1.7   Dec 05 2006 15:50:12   mmodi
//Removed Adjust Scales method and removed rounding of values returned
//Resolution for 4240: CO2 Emissions
//
//	 Rev 1.6   Dec 05 2006 11:57:16   mturner
//Corrected calculation used to translate g of CO2 per litre to MPG.
//
//   Rev 1.5   Dec 01 2006 16:47:10   mmodi
//Convert Gramms of CO2 value to Kg
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.4   Nov 29 2006 15:10:00   mmodi
//Added code to set Max scale value to 5 is less than 5
//Resolution for 4240: CO2 Emissions
//Resolution for 4279: CO2: Fuel costs less than £1 not shown correctly
//
//   Rev 1.3   Nov 26 2006 17:44:32   mmodi
//Corrected error when viewing tickets/costs for aModified journey
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.2   Nov 25 2006 14:06:16   mmodi
//Updated conversion of original journey consumption and cost values
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.1   Nov 24 2006 11:23:10   mmodi
//Code updates, populating methods
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.0   Nov 19 2006 10:43:32   mmodi
//Initial revision.
//Resolution for 4240: CO2 Emissions
//

using System;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.JourneyEmissions;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Web.Support;

using Logger = System.Diagnostics.Trace;


namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// JourneyEmissionsHelper used to Calculate Fuel Costs, CO2 Emissions, and Scales for Journey Emissions page.
    /// All of the emissions calculation logic has been moved in to JourneyEmissions.JourneyEmissionsCalculator().
    /// This class is now used as a wrapper which accepts session objects and wraps/passes to the session independent
    /// calculator class.
	/// </summary>
	public class JourneyEmissionsHelper
	{
        JourneyEmissionsCalculator emissionsCalculator;

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public JourneyEmissionsHelper()
		{
            emissionsCalculator = new JourneyEmissionsCalculator();
		}

		/// <summary>
		/// Constructor setting up internal values to allow Fuel Cost and CO2 Emissions to be calculated
		/// This overloaded constructor MUST be used if GetFuelCost and GetEmissions 
		/// for a PLANNED CAR JOURNEY are to be used
		/// </summary>
		/// <param name="journeyParams">journeyParams for the original Car Journey planned by user</param>
		/// <param name="totalFuelCost">totalFuelCost of journey (Outward and Return) to the 10000 pence</param>
		public JourneyEmissionsHelper(TDJourneyParametersMulti journeyParams, decimal totalFuelCost)
		{
            CarFuelParameters carFuelParameters = GetCarFuelParameters(journeyParams);

            // Set up an instance of the emissions calculator
            emissionsCalculator = new JourneyEmissionsCalculator(carFuelParameters, totalFuelCost);	
		}

		#endregion

        #region Private methods

        /// <summary>
        /// Returns CarFuelParameters populated from TDJourneyParametersMulti
        /// </summary>
        /// <param name="journeyParams"></param>
        /// <returns></returns>
        public CarFuelParameters GetCarFuelParameters(TDJourneyParametersMulti journeyParams)
        {
            // IR4327: If unsuccessful retrieve of params, then force a reset. This scenario only occurs if original
            // journey was Flight only, and then Replanned to use Car
            if (journeyParams == null)
            {
                journeyParams = new TDJourneyParametersMulti();
                journeyParams.Initialise();
            }

            CarFuelParameters carFuelParameters = new CarFuelParameters(
                journeyParams.CarSize, journeyParams.CarFuelType, journeyParams.FuelConsumptionEntered,
                journeyParams.FuelConsumptionUnit, journeyParams.FuelConsumptionOption,
                journeyParams.FuelCostEntered, journeyParams.FuelCostOption);

            return carFuelParameters;
        }

        /// <summary>
        /// Returns CarFuelParameters populated from JourneyEmissionsPageState
        /// </summary>
        /// <param name="journeyParams"></param>
        /// <returns></returns>
        public CarFuelParameters GetCarFuelParameters(JourneyEmissionsPageState pageState, bool yourCar)
        {
            if (yourCar)
            {
                return new CarFuelParameters(
                pageState.YourCarSize, pageState.YourCarFuelType, pageState.YourCarFuelConsumptionEntered,
                pageState.YourCarFuelConsumptionUnit, pageState.YourCarFuelConsumptionOption,
                pageState.YourCarFuelCostEntered, pageState.YourCarFuelCostOption);
            }
            else
            {
                return new CarFuelParameters(
                pageState.CarSize, pageState.CarFuelType, pageState.FuelConsumptionEntered,
                pageState.FuelConsumptionUnit, pageState.FuelConsumptionOption,
                pageState.FuelCostEntered, pageState.FuelCostOption);
            }
        }

        #endregion

        #region Public properties and methods

        /// <summary>
		/// Calculates the fuel used for a planned CAR JOURNEY based on the supplied preferences
		/// </summary>
		/// <param name="pageState">pageState containing journey emissions input preferences</param>
		/// <returns>fuel cost for the journey as a decimal rounded to the nearest whole number</returns>
		public decimal GetFuelCost(JourneyEmissionsPageState pageState)
		{
            CarFuelParameters carFuelParameters = GetCarFuelParameters(pageState, false);

			return emissionsCalculator.GetFuelCost(carFuelParameters);
		}

		#region GetEmissions	
		/// <summary>
		/// Calculates the CO2 emissions for the ORIGINAL CAR JOURNEY planned by user
		/// </summary>
		/// <returns>Co2 emissions as a decimal to 1 decimal place, with trailing zeros removed</returns>
		public decimal GetEmissions ()
		{
            return emissionsCalculator.GetEmissions();
		}

		/// <summary>
		/// Calculates the CO2 emissions for a planned CAR JOURNEY based on the supplied preferences
		/// </summary>
		/// <param name="pageState">pageState containing journey emissions input preferences</param>
		/// <returns>Co2 emissions as a decimal to 1 decimal place, with trailing zeros removed</returns>
		public decimal GetEmissions (JourneyEmissionsPageState pageState)
		{
            CarFuelParameters carFuelParameters = GetCarFuelParameters(pageState, false);

            return emissionsCalculator.GetEmissions(carFuelParameters);
		}

		/// <summary>
		/// Calculates the CO2 emissions for a planned CAR JOURNEY based on the supplied preferences.
		/// Bool to indicate whether its for "YourCar" or "CompareCar" (this is the description used 
		/// for the pointers on the Speedo Dials on the JourneyEmissions page)
		/// </summary>
		/// <param name="pageState">pageState containing journey emissions input preferences</param>
		/// <param name="yourCar">bool indicating to use the YourCar input values</param>
		/// <returns>Co2 emissions as a decimal to 1 decimal place, with trailing zeros removed</returns>
		public decimal GetEmissions (JourneyEmissionsPageState pageState, bool yourCar)
		{
            CarFuelParameters carFuelParameters = GetCarFuelParameters(pageState, yourCar);

            return emissionsCalculator.GetEmissions(carFuelParameters);
		}

        /// <summary>
        /// Calculates the CO2 emissions for the supplied Road Journey, using the Car size and Fuel type provided
        /// </summary>
        /// <returns>CO2 emissions as a decimal to 1 decimal place, with trailing zeros removed</returns>
		public decimal GetEmissions (TDJourneyParametersMulti journeyParams, string CarSize, string FuelType, RoadJourney journey)
		{
            CarFuelParameters originalCarFuelParameters = GetCarFuelParameters(journeyParams);

            return emissionsCalculator.GetEmissions(CarSize, FuelType, journey, originalCarFuelParameters);
		}

		/// <summary>
		/// Returns the CO2 emissions for the supplied Journey, can be a PT or a Car journey.
        /// If the Journey has a CO2 value associated with it, then that is returned rather than 
        /// performing the calculation logic
		/// </summary>
		/// <param name="journey">Journey</param>
        /// <param name="journeyParams">TDJourneyParametersMulti is only needed when providing a Car journey</param>
        /// <param name="pageState">JourneyEmissionsPageState is only needed when providing a Car journey</param>
		/// <returns>CO2 emissions as decimal</returns>
		public decimal GetEmissions(Journey journey, TDJourneyParametersMulti journeyParams, JourneyEmissionsPageState pageState)
		{
            // Check if journey parameters were provided
            if (journeyParams == null)
            {
                journeyParams = new TDJourneyParametersMulti();
                journeyParams.Initialise();
            }
            // Check if the pageState has a car size and fuel type set, otherwise initialise
            // to use default values - prevents errors in emission calculations
            if ((pageState == null) || (pageState.CarSize == null) || (pageState.CarFuelType == null))
            {
                pageState = new JourneyEmissionsPageState();
                pageState.Initialise();
            }

            CarFuelParameters originalCarFuelParameters = GetCarFuelParameters(journeyParams);
            CarFuelParameters currentCarFuelParameters = GetCarFuelParameters(pageState, true);

            // Has journey already has emissions calculated for it
            if (journey.Emissions > -1)
                return Convert.ToDecimal(journey.Emissions);
            else
                return emissionsCalculator.GetEmissions(journey, originalCarFuelParameters, currentCarFuelParameters);
		}

		/// <summary>
		/// Returns the CO2 emissions for the parameters Mode Type and Distance.
		/// This method uses default parameter values in returning the emissions 
		/// for a specified mode type and distance
		/// </summary>
		/// <param name="modeType">Mode type</param>
		/// <param name="journeyDistance">Distance in metres</param>
		/// <returns>CO2 emissions as decimal</returns>
		public decimal GetEmissions(ModeType modeType, decimal journeyDistance)
		{
            return emissionsCalculator.GetEmissions(modeType, journeyDistance);
		}


		/// <summary>
		/// Returns the CO2 emissions for the parameters Mode Type and Distance.
        /// This method takes an additional parameter indicating if international CO2 
        /// factors are to be used.
		/// </summary>
		/// <param name="modeType">Mode type</param>
		/// <param name="journeyDistance">Distance in metres</param>
        /// <param name="useInternationalFactors">True if international CO2 factors are to be used</param>
		/// <returns>CO2 emissions as decimal</returns>
		public decimal GetEmissions(ModeType modeType, decimal journeyDistance, bool useInternationalFactors)
		{
            return emissionsCalculator.GetEmissions(modeType, journeyDistance, useInternationalFactors);
		}

		/// <summary>
		/// Returns the CO2 emissions for the parameters Mode Type, Distance, Car Type and Fuel Type.
		/// If Car Type and Fuel Type are null then the default parameter values are
		/// used to return the emissions for a specified mode type and distance.
		/// </summary>
		/// <param name="modeType">Mode type</param>
		/// <param name="journeyDistance">Distance in metres</param>
		/// <param name="carType">Car Type</param>
		/// <param name="fuelType">Fuel Type</param>
		/// <returns>CO2 emissions as decimal</returns>
		public decimal GetEmissions(ModeType modeType, decimal journeyDistance, string carType, string fuel)
		{
            return emissionsCalculator.GetEmissions(modeType, journeyDistance, carType, fuel);
		}
		#endregion

		#region GetJourneyDistance

		/// <summary>
		/// Returns the total distance (outward and return) for the Selected 
		/// journey in the supplied Session manager
		/// </summary>
		/// <param name="sessionManager">TDSessionManager</param>
		/// <returns>Total journey distance as decimal</returns>
		public decimal GetJourneyDistance(ITDSessionManager sessionManager)
		{
			decimal journeyDistance = 0;

			// Determine the journey we need to work with
            Journey outwardJourney = GetRequiredJourney(sessionManager.JourneyResult, sessionManager.CycleResult, sessionManager.JourneyViewState, false);

			if (outwardJourney != null)
				journeyDistance = emissionsCalculator.GetJourneyDistance(outwardJourney);

			// If the user has specified a return journey then calculate distance for this too
            if (((sessionManager.JourneyResult != null) && (sessionManager.JourneyResult.ReturnPublicJourneyCount > 0 || sessionManager.JourneyResult.ReturnRoadJourneyCount > 0))
                ||
                 ((sessionManager.CycleResult != null) && (sessionManager.CycleResult.ReturnCycleJourneyCount > 0))
               )
            {
                Journey returnJourney = GetRequiredJourney(sessionManager.JourneyResult, sessionManager.CycleResult, sessionManager.JourneyViewState, true); 
                if (returnJourney != null)
                    journeyDistance += emissionsCalculator.GetJourneyDistance(returnJourney);
			}

			return journeyDistance;
		}


		/// <summary>
		/// Returns the total distance for the journey (outward and return) 
		/// in the itinerary manager
		/// </summary>
		/// <param name="itineraryManager">TDItineraryManager</param>
		/// <returns>Total journey distance as decimal</returns>
		public decimal GetJourneyDistance(TDItineraryManager itineraryManager)
		{
			decimal journeyDistance = 0;

			if (itineraryManager.Length == 0)
			{
				// No journeys, so return 0
				return journeyDistance;
			}
			else
			{
				// Journeys exist, so get each Outward (and Return) leg and calculate distance
				if (itineraryManager.OutwardLength > 0)
				{
					foreach (Journey outwardJourney in itineraryManager.OutwardJourneyItinerary)
					{
                        journeyDistance += emissionsCalculator.GetJourneyDistance(outwardJourney);
					}
				}

				if (itineraryManager.ReturnLength > 0)
				{
					foreach (Journey returnJourney in itineraryManager.ReturnJourneyItinerary)
					{
                        journeyDistance += emissionsCalculator.GetJourneyDistance(returnJourney);
					}
				}
			}

			return journeyDistance;
		}

		#endregion

		#region Scales for Journey Emissions Speedo Dials

		/// <summary>
		/// Returns the fuel cost scale max based on the original user journey
		/// </summary>
		public decimal GetFuelCostScaleMax()
		{
            return emissionsCalculator.GetFuelCostScaleMax();
		}

		/// <summary>
		/// Returns the fuel cost scale min based on the original user journey
		/// </summary>
		public decimal GetFuelCostScaleMin()
		{
            return emissionsCalculator.GetFuelCostScaleMin();
		}

		/// <summary>
		/// Returns the emissions scale max based on the original user journey
		/// </summary>
		public decimal GetEmissionsScaleMax()
		{
            return emissionsCalculator.GetEmissionsScaleMax();
		}

		/// <summary>
		/// Returns the emissions scale min based on the original user journey
		/// </summary>
		public decimal GetEmissionsScaleMin()
		{
            return emissionsCalculator.GetEmissionsScaleMin();
		}

		#endregion

		/// <summary>
		/// Returns required (user selected) public/road journey as a Journey object. Takes 
		/// </summary>
		/// <param name="result">Journey result to use</param>
		/// <param name="viewState">Journey view state</param>
		/// <param name="returnRequired">Examine return journey</param>
		/// <returns></returns>
        public Journey GetRequiredJourney(ITDJourneyResult result, ITDCyclePlannerResult cycleResult,
            TDJourneyViewState viewState, bool returnRequired)
		{
			if (!returnRequired)
			{
				if ( viewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal )
				{
					return result.OutwardPublicJourney(viewState.SelectedOutwardJourneyID);
				}
				else if (viewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended)
				{
					return result.AmendedOutwardPublicJourney;
				}
                else if (viewState.SelectedOutwardJourneyType == TDJourneyType.Cycle)
                {
                    return cycleResult.OutwardCycleJourney();
                }
				else
				{
					return result.OutwardRoadJourney();
				}
			}
			else
			{
				if ( viewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal )
				{
					return result.ReturnPublicJourney(viewState.SelectedReturnJourneyID);
				}
				else if (viewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended)
				{
					return result.AmendedReturnPublicJourney;
				}
                else if (viewState.SelectedReturnJourneyType == TDJourneyType.Cycle)
                {
                    return cycleResult.ReturnCycleJourney();
                }
				else
				{
					return result.ReturnRoadJourney();
				}
			}
		}

		#endregion

		#region Static Public properties

		/// <summary>
		/// Static read-only property indicating whether the Journey Emissions PT functionality
		/// should be made available
		/// </summary>
		/// <returns>bool</returns>
        public static bool JourneyEmissionsPTAvailable
        {
            get { return JourneyEmissionsCalculator.JourneyEmissionsPTAvailable; }
        }

		/// <summary>
		/// Static read-only  indicating whether the supplied Distance is
		/// within the acceptable range
		/// </summary>
		/// <param name="journeyDistance">Distance in metres</param>
		/// <returns>bool</returns>
		public static bool JourneyDistanceValid(double journeyDistance)
		{
            return JourneyEmissionsCalculator.JourneyDistanceValid(journeyDistance);
		}

		/// <summary>
		/// Returns a width based on the Emissions, and Max Emissions value. 
		/// Uses a Max Width from Properties to calculate width
		/// </summary>
		/// <param name="emissions">CO2 Emissions</param>
		/// <param name="maxEmissions">Max CO2 Emissions value</param>
		/// <returns>Width as integer</returns>
		public static int EmissionsBarWidth(decimal emissions, decimal maxEmissions)
		{
            return JourneyEmissionsCalculator.EmissionsBarWidth(emissions, maxEmissions);
		}

		/// <summary>
		/// Returns a width based on the Emissions, and Max Emissions value. 
		/// Uses a Max Width from Properties to calculate width
		/// </summary>
		/// <param name="emissions">CO2 Emissions</param>
		/// <param name="maxEmissions">Max CO2 Emissions value</param>
		/// <returns>Width as integer</returns>
		public static int NewEmissionsBarWidth(decimal emissions, decimal maxEmissions)
		{
            return JourneyEmissionsCalculator.NewEmissionsBarWidth(emissions, maxEmissions);
        }

		/// <summary>
		/// Determines whether the Air transport mode should be shown, dependent on Distance
		/// </summary>
		/// <param name="journeyDistance">JourneyDistance in Metres</param>
		/// <returns>Bool</returns>
		public static bool ShowAir(decimal journeyDistance)
		{
            return JourneyEmissionsCalculator.ShowAir(journeyDistance);
		}

		/// <summary>
		/// Determines whether the Coach transport mode should be shown, dependent on Distance.
		/// </summary>
		/// <param name="journeyDistance">JourneyDistance in metres</param>
		/// <returns>Bool</returns>
		public static bool ShowCoach(decimal journeyDistance)
		{
            return JourneyEmissionsCalculator.ShowCoach(journeyDistance);
		}


		/// <summary>
		/// Returns the modes used for the Journey
		/// </summary>
		/// <param name="journey">journey as JourneyControl.Journey</param>
		/// <returns>Array of ModeType</returns>
		public static ModeType[] GetJourneyModes(Journey journey)
		{
            return JourneyEmissionsCalculator.GetJourneyModes(journey);
		}

		#endregion
	}
}
