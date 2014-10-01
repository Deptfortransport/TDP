// *********************************************** 
// NAME			: JourneyEmissionsCalculator.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 29/07/09
// DESCRIPTION	: Class to provide emissions calculator for journeys
//              : Code has been migrated from \Web2\Adapters\JourneyEmissionsHelper.cs with minor changes to 
//              : not use session objects
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyEmissions/JourneyEmissionsCalculator.cs-arc  $
//
//   Rev 1.5   Jan 22 2013 10:55:12   mmodi
//Updated to include emissions factor for Telecabine
//Resolution for 5884: CCN:677 - Telecabine modetype
//
//   Rev 1.4   Feb 26 2010 12:40:12   rbroddle
//Updated to use correct uplifted distance factors for international coach and air.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.3   Feb 22 2010 08:18:32   rbroddle
//Updated to use leg.distance if available instead of calculating it
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Feb 18 2010 19:13:42   rbroddle
//International planner amendments
//
//   Rev 1.1   Feb 17 2010 11:11:54   rbroddle
//Updates for international planner
//
//   Rev 1.0   Aug 04 2009 14:36:54   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MeasureConvert = System.Convert;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;

using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.JourneyEmissions
{
    /// <summary>
    /// JourneyEmissionsCalculator used to Calculate Fuel Costs, CO2 Emissions, and Scales for Journey pages
    /// </summary>
    public class JourneyEmissionsCalculator
    {
        #region Constants

        // Constants used to identify and retrieve the emission factor from database
        private const string FACTOR_AIR_DEFAULT = "AirDefault";
        private const string FACTOR_AIR_SMALL = "AirSmall";
        private const string FACTOR_AIR_MEDIUM = "AirMedium";
        private const string FACTOR_AIR_LARGE = "AirLarge";
        private const string FACTOR_BUS_DEFAULT = "BusDefault"; // Mode: Bus,Coach use the same factor
        private const string FACTOR_COACH_DEFAULT = "CoachDefault";
        private const string FACTOR_FERRY_DEFAULT = "FerryDefault";
        private const string FACTOR_LIGHTRAIL_DEFAULT = "LightRailDefault";
        private const string FACTOR_LIGHTRAIL = "LightRail"; // Appended with LightRail System to get specific factor
        private const string FACTOR_METRO_DEFAULT = "MetroDefault";
        private const string FACTOR_TRAM_DEFAULT = "TramDefault";
        private const string FACTOR_UNDERGROUND_DEFAULT = "UndergroundDefault";
        private const string FACTOR_RAIL_DEFAULT = "RailDefault";
        private const string FACTOR_TELECABINE_DEFAULT = "TelecabineDefault";
        private const string FACTOR_BUS_COACH_DISTANCE = "BusCoachDistance";
        private const string FACTOR_INTERNATIONAL_RAIL_DEFAULT = "RailIntl";
        private const string FACTOR_INTERNATIONAL_AIR_DEFAULT = "AirIntl";
        private const string FACTOR_INTERNATIONAL_COACH_DEFAULT = "CoachIntl";


        #endregion

        #region Variables

        private string originalCarSize;
        private string originalFuelType;
        private string originalFuelConsumptionEntered;
        private int originalFuelConsumptionUnit;
        private bool originalFuelConsumptionOption;
        private string originalFuelCostEntered;
        private bool originalFuelCostOption;

        private decimal originalFuelUsed;

        private CarCostCalculator calculator;
        private JourneyEmissionsFactor emissionsFactor;

        private decimal fuelCostScaleMax;
        private decimal fuelCostScaleMin;
        private decimal emissionScaleMax;
        private decimal emissionScaleMin;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public JourneyEmissionsCalculator()
        {
            LoadFromServiceDiscovery();
        }

        /// <summary>
        /// Constructor setting up internal values to allow Fuel Cost and CO2 Emissions to be calculated
        /// This overloaded constructor MUST be used if GetFuelCost and GetEmissions 
        /// for a PLANNED CAR JOURNEY are to be used
        /// </summary>
        /// <param name="originalCarFuelParameters">The Car fuel parameters for the original planned car journey. 
        /// These will form the base values for any other car size/type journey emission calculations wanted later.</param>
        /// <param name="totalFuelCost">totalFuelCost of journey (Outward and Return) to the 10000 pence</param>
        public JourneyEmissionsCalculator(CarFuelParameters originalCarFuelParameters, decimal totalFuelCost)
        {
            LoadFromServiceDiscovery();

            SetOriginalCarAndFuelValues(originalCarFuelParameters, totalFuelCost);

            // Calculate Scale's Max and Min values - these are based on the original journey values
            fuelCostScaleMax = CalculateFuelCostScaleMax();
            fuelCostScaleMin = CalculateFuelCostScaleMin();
            emissionScaleMax = CalculateEmissionsScaleMax();
            emissionScaleMin = CalculateEmissionsScaleMin();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method which loads the Journey emissions factor and Car cost calculator objects from Service Discovery
        /// </summary>
        private void LoadFromServiceDiscovery()
        {
            // Get tge JourneyEmissionsFactor from the service discovery
            emissionsFactor = (JourneyEmissionsFactor)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyEmissionsFactor];

            // Set up CarCostCalculator used by private methods in later calls, - for efficiency
            calculator = (CarCostCalculator)TDServiceDiscovery.Current[ServiceDiscoveryKey.CarCostCalculator];
        }

        /// <summary>
        /// Sets the originalFuelUsed value for the parameters provided
        /// </summary>
        /// <param name="roadJourney">Road Journey</param>
        private void SetOriginalCarAndFuelValues(CarFuelParameters carFuelParameters, decimal totalFuelCost)
        {
            // Set this object values from Journey parameters. These are needed so we can
            // calculate the emissions for the original car journey
            originalCarSize = carFuelParameters.CarSize;
            originalFuelType = carFuelParameters.CarFuelType;
            originalFuelConsumptionEntered = carFuelParameters.FuelConsumptionEntered;
            originalFuelConsumptionUnit = carFuelParameters.FuelConsumptionUnit;
            originalFuelConsumptionOption = carFuelParameters.FuelConsumptionOption;
            originalFuelCostEntered = carFuelParameters.FuelCostEntered;
            originalFuelCostOption = carFuelParameters.FuelCostOption;

            // Orignal fuel cost might be a user entered value, so call method to return appropriate value
            string fuelCost = GetOriginalFuelCost();
            
            // Calculate the original fuel used for the journey - used by the "Calculate" methods
            originalFuelUsed = CalculateFuelUsed(totalFuelCost, Convert.ToDecimal(fuelCost));
        }

        /// <summary>
        /// Returns the Orignal Fuel Consumption, performing translation if the user had 
        /// originally entered a value
        /// </summary>
        /// <returns></returns>
        private string GetOriginalFuelConsumption()
        {
            string fuelConsumption;

            // Determine if user had originally entered a fuelConsumption value, and convert as appropriate
            if (originalFuelConsumptionOption == false)
            {
                if (originalFuelConsumptionUnit == 1)
                {
                    // Translate from GallonsPerMile to LitresPerMeter
                    fuelConsumption = MeasurementConversion.Convert(MeasureConvert.ToDouble(originalFuelConsumptionEntered), ConversionType.MilesPerGallonToMetersPerLitre).ToString();
                }
                else
                {
                    // Translate from LitresPer100Kilometers to LitresPerMeter
                    fuelConsumption = MeasurementConversion.Convert((MeasureConvert.ToDouble(originalFuelConsumptionEntered)), ConversionType.LitresPer100KilometersToMetersPerLitre).ToString();
                }
            }
            else
            {
                if (originalFuelConsumptionEntered == null)
                {
                    fuelConsumption = calculator.GetFuelConsumption(originalCarSize, originalFuelType).ToString();
                }
                else
                {
                    fuelConsumption = originalFuelConsumptionEntered;
                }
            }

            return fuelConsumption;
        }

        /// <summary>
        /// Returns the Original journey Fuel Cost, performing translation if user had
        /// originally entered a fuel cost value
        /// </summary>
        /// <returns></returns>
        private string GetOriginalFuelCost()
        {
            string fuelCost;

            // Determine if user had originally entered a fuelcost value, and convert if required
            if (originalFuelCostOption == false)
            {
                fuelCost = MeasurementConversion.Convert((MeasureConvert.ToDouble(originalFuelCostEntered)), ConversionType.TenthOfPencePerLitre).ToString();
            }
            else
            {
                if (originalFuelCostEntered == null)
                {
                    fuelCost = calculator.GetFuelCost(originalFuelType).ToString();
                }
                else
                {
                    fuelCost = originalFuelCostEntered;
                }
            }

            return fuelCost;
        }

        /// <summary>
        /// Calculates the fuel used 
        /// </summary>
        /// <param name="totalFuelCost">total fuel cost of journey (1/10000 pence e.g. 5000000</param>
        /// <param name="fuelPrice">fuel cost (1/10 pence/litre e.g. 879)</param>
        /// <returns>Returns fuelUsed (1/1000 litre e.g. 5678.12345</returns>
        private decimal CalculateFuelUsed(decimal totalFuelCost, decimal fuelCost)
        {
            decimal fuelUsed = totalFuelCost / fuelCost;

            return fuelUsed;
        }

        /// <summary>
        /// Returns the new fuel used for the supplied preferences, using the original car journey parameters
        /// </summary>
        /// <param name="carSize">Car size</param>
        /// <param name="fuelType">Fuel type</param>
        /// <param name="fuelConsumptionOption">Fuel Consumption options, - using the Default or Specifed fuel consumption</param>
        /// <param name="fuelConsumptionEntered">Fuel Consumption entered - holds the Specified value</param>
        /// <param name="fuelConsumptionUnit">Fuel Consumption unit - MPG or Litres/KM</param>
        /// <returns>Returns fuelUsed (1/1000 litre e.g. 5678.12345</returns>
        private decimal CalculateNewFuelUsed(string carSize, string fuelType, bool fuelConsumptionOption, string fuelConsumptionEntered, int fuelConsumptionUnit)
        {
            // if user has specified a fuel consumption value, then disregard the Car size

            // If user has specified a new MPG (or LKM) value, we need to adjust the fuel used 
            // using a ratio of the original MPG (or LKM) with the new specified MPG (or LKM)
            string newFuelConsumption = "";

            if (fuelConsumptionOption == false)
            {
                if (fuelConsumptionUnit == 1)
                {
                    // Translate from GallonsPerMile to LitresPerMeter
                    newFuelConsumption = MeasurementConversion.Convert(MeasureConvert.ToDouble(fuelConsumptionEntered), ConversionType.MilesPerGallonToMetersPerLitre).ToString();
                }
                else if (fuelConsumptionUnit == 2)
                {
                    // Translate from LitresPer100Kilometers to LitresPerMeter
                    newFuelConsumption = MeasurementConversion.Convert((MeasureConvert.ToDouble(fuelConsumptionEntered)), ConversionType.LitresPer100KilometersToMetersPerLitre).ToString();
                }
                else
                {
                    // Translate from CO2 g per km to MPG, then convert to MetersPerLitre

                    // fuel factor is stored as 234 (for petrol), but we need it as 2.34 kg/l
                    double fuelFactor = calculator.GetFuelFactor(fuelType);
                    fuelFactor = fuelFactor / 100;

                    // Convert grams entered to Kg
                    double fuelConsumption = MeasureConvert.ToDouble(fuelConsumptionEntered) / 1000;

                    // Convert Kg/L to Km/L by dividing by Kg/Km
                    fuelConsumption = fuelFactor / fuelConsumption;

                    // Convert Km/L to MPG 
                    fuelConsumption = fuelConsumption * 2.8248105347;

                    newFuelConsumption = fuelConsumption.ToString();

                    // Translate from MilesPerGallon To MetersPerLitre
                    newFuelConsumption = MeasurementConversion.Convert(MeasureConvert.ToDouble(fuelConsumption), ConversionType.MilesPerGallonToMetersPerLitre).ToString();
                }
            }
            else
            {
                newFuelConsumption = calculator.GetFuelConsumption(carSize, fuelType).ToString();
            }

            // To avoid a divide by zero error when working out ratio below
            if (newFuelConsumption == string.Empty)
                newFuelConsumption = "1";

            // Work out the ratio
            decimal fuelConsumptionRatio = Convert.ToDecimal(GetOriginalFuelConsumption()) / Convert.ToDecimal(newFuelConsumption);

            // Determines fuel used based on the ratio calculated from the new consumption and original consumption
            decimal fuelUsed = originalFuelUsed * fuelConsumptionRatio;

            return fuelUsed;
        }

        /// <summary>
        /// Calculates the new fuel cost based on the preferences supplied
        /// </summary>
        /// <param name="carSize">Car size e.g. small, medium large</param>
        /// <param name="fuelType">Fuel type e.g. petrol or diesel</param>
        /// <param name="fuelConsumptionOption">bool - true indicates default fuel used</param>
        /// <param name="fuelConsumption">fuel consumption entered</param>
        /// <param name="fuelConsumptionUnit">fuel consumption unit, 1 indicates mpg</param>
        /// <param name="fuelCostOption">bool - true indicates default cost used</param>
        /// <param name="fuelCostEntered">fuel cost entered</param>
        /// <returns>fuel cost for the journey as a decimal rounded to the nearest whole number</returns>
        private decimal CalculateFuelCost(string carSize, string fuelType, bool fuelConsumptionOption,
            string fuelConsumption, int fuelConsumptionUnit,
            bool fuelCostOption, string fuelCostEntered)
        {

            // Before calculating the new Fuel Cost, need to ensure the Fuel Used for the journey is 
            // calculated with the updated preferences specified

            // new fuel used
            decimal fuelUsed = CalculateNewFuelUsed(carSize, fuelType, fuelConsumptionOption, fuelConsumption, fuelConsumptionUnit);

            // Only convert the value of fuel cost if the value is entered, 
            // since for the average option, the value is already in tenth of pence per litre 
            // in the database table
            string fuelPrice;
            if (fuelCostOption == false)
            {
                fuelPrice = MeasurementConversion.Convert((MeasureConvert.ToDouble(fuelCostEntered)), ConversionType.TenthOfPencePerLitre).ToString();
            }
            else
            {
                fuelPrice = calculator.GetFuelCost(fuelType).ToString();
            }

            // fuelCost value is in 1/10000 pence
            decimal fuelCost = fuelUsed * Convert.ToDecimal(fuelPrice);

            // fuelCost in pounds
            fuelCost = fuelCost / 1000000;

            // Round fuel cost not showing any decimal places
            // Commented out because we want to work with accurate values when creating speedos
            //fuelCost = Decimal.Round(fuelCost, 0);

            return fuelCost;
        }

        /// <summary>
        /// Calculates the Co2 emissions for a CAR JOURNEY based on the preferences supplied
        /// </summary>
        /// <param name="carSize">Car size e.g. small, medium large</param>
        /// <param name="fuelType">Fuel type e.g. petrol or diesel</param>
        /// <param name="fuelConsumptionOption">bool - true indicates default fuel used</param>
        /// <param name="fuelConsumption">fuel consumption entered</param>
        /// <param name="fuelConsumptionUnit">fuel consumption unit, 1 indicates mpg</param>
        /// <param name="fuelCostOption">bool - true indicates default cost used</param>
        /// <param name="fuelCostEntered">fuel cost entered</param>
        /// <returns>Co2 emissions as a decimal to 1 decimal place, with trailing zeros removed</returns>
        private decimal CalculateEmissions(string carSize, string fuelType, bool fuelConsumptionOption,
            string fuelConsumption, int fuelConsumptionUnit,
            bool fuelCostOption, string fuelCostEntered)
        {

            // Before calculating the Emissions, need to ensure the Fuel Used for the journey is 
            // calculated with the updated preferences specified

            // new fuel used
            decimal fuelUsed = CalculateNewFuelUsed(carSize, fuelType, fuelConsumptionOption, fuelConsumption, fuelConsumptionUnit);

            // multiply fuel used with the FuelFactor, this is the coversion factor which gives us the 
            // CO2 emissions
            decimal fuelFactor = calculator.GetFuelFactor(fuelType);

            // fuel factor is stored as 234 (for petrol), but we need it as 2.34 kg/l
            fuelFactor = fuelFactor / 100;

            // gives us emissions in 1/100 kg
            decimal emissions = (fuelUsed * fuelFactor) / 10;

            // convert to kg and round emissions value to 1 decimal place
            emissions = emissions / 100;
            emissions = Decimal.Round(emissions, 1);

            // remove any trailing zeros
            string emissionsString = emissions.ToString();
            emissions = Convert.ToDecimal(emissions);

            return emissions;
        }

        #region Scale calculations
        /// <summary>
        /// Calculates the fuel cost scale max based on the original user journey
        /// Uses a stored mpg value
        /// </summary>
        /// <returns></returns>
        private decimal CalculateFuelCostScaleMax()
        {
            // Get lowMpg value to set upper limit
            string mpg = Properties.Current["JourneyEmissions.LowMpg"];
            string petrol = Properties.Current["JourneyEmissions.FuelType.Petrol"];

            decimal scale = CalculateFuelCost(
                originalCarSize,
                petrol,
                false,
                mpg,
                1,
                originalFuelCostOption,
                originalFuelCostEntered);

            return scale;
        }

        /// <summary>
        /// Calculates the fuel cost scale min based on the original user journey
        /// Uses a stored mpg value
        /// </summary>
        /// <returns></returns>
        private decimal CalculateFuelCostScaleMin()
        {
            // Get highMPG value to set lower limit
            string mpg = Properties.Current["JourneyEmissions.HighMpg"];
            string diesel = Properties.Current["JourneyEmissions.FuelType.Diesel"];

            decimal scale = CalculateFuelCost(
                originalCarSize,
                diesel,
                false,
                mpg,
                1,
                originalFuelCostOption,
                originalFuelCostEntered);

            return scale;
        }

        /// <summary>
        /// Calculates the emissions scale max based on the original user journey
        /// Uses a stored mpg value
        /// </summary>
        /// <returns></returns>
        private decimal CalculateEmissionsScaleMax()
        {
            // Get LowMPG value to set upper limit
            string mpg = Properties.Current["JourneyEmissions.LowMpg"];
            string petrol = Properties.Current["JourneyEmissions.FuelType.Petrol"];

            decimal scale = CalculateEmissions(
                originalCarSize,
                petrol,
                false,
                mpg,
                1,
                originalFuelCostOption,
                originalFuelCostEntered);

            return scale;
        }

        /// <summary>
        /// Calculates the emissions scale max based on the original user journey
        /// Uses a stored mpg value
        /// </summary>
        /// <returns></returns>
        private decimal CalculateEmissionsScaleMin()
        {
            // Get HighMPG value to set lower limit
            string mpg = Properties.Current["JourneyEmissions.HighMpg"];
            string diesel = Properties.Current["JourneyEmissions.FuelType.Diesel"];

            decimal scale = CalculateEmissions(
                originalCarSize,
                diesel,
                false,
                mpg,
                1,
                originalFuelCostOption,
                originalFuelCostEntered);

            return scale;
        }

        #endregion

        /// <summary>
        /// This method will calculate the Emissions (and distance) for each leg in the Public journey
        /// calling the appropriate calculate Emission (and distance) method for leg ModeType.
        /// Takes a PublicJourney, and a Bool calculate emissions: set to true to return Journey 
        /// Emissions, or false to return Journey Distance.
        /// </summary>
        /// <param name="pj">Public Journey to calculate emissions (or distance for)</param>
        /// <param name="emissions">Emissions or distance</param>
        /// <returns>If emissions = true, will return total Emissions value, Else return total Distance value for PT journey</returns>
        private decimal CalculateEmissionsDistanceForPTJourney(JourneyControl.PublicJourney pj, bool calculateEmissions)
        {
            decimal distanceTotal = 0;
            decimal distanceForLeg = 0;
            decimal emissions = 0;

            // This method has the following processing:
            //	Loop through each leg of the PT Journey. 
            //	Check if the leg has a valid set of coordinates in Leg.Geometry.
            //	  If invalid, then create a temp Geometry containing the valid coordinates, 
            //	  then move on to next leg.
            //    If valid, check if there are any coordinates in the temp Geometry.
            //		If there are temp coodinates, then add those to the temp Geometry, determine 
            //		which mode to use, and call calc Distance,Emissions methods
            //		If there are no temp coordinates, then call calc Distance,Emissions methods using the 
            //		leg.Geometty value.
            //		Clear down the temp variables ready for next loop.

            // We need to do the above because some modes (e.g. Metro) can have a walk 
            // to a station which may not have a valid OSGR. This results in some legs having
            // a dramatically reduced distance, thus affecting the emissions value calculated (which
            // is based on the distance).

            // We're using the Geometry because this contains the coordinates from start to end of 
            // the leg, and all intermediate stops, calling points. 
            // So for train this will be the service start and end all stops inbetween, and 
            // for a coach this will just be the start and end

            PublicJourneyDetail leg;
            ArrayList al = new ArrayList();
            ModeType modeType = ModeType.Walk;
            int duration = 0;
            OSGridReference[] geometry;

            // Loop through all the legs
            for (int i = 0; i < pj.JourneyLegs.Length; i++)
            {
                leg = pj.JourneyLegs[i] as PublicJourneyDetail;
                if (leg.Distance != -1)
                {
                    //The leg has its distance so just use that (will be the case for international journeys)
                    //Coach and Air distanceFactors should still be applied, Rail distance will take intermediates 
                    //into account already so will just equal the leg distance
                    switch (leg.Mode)
                    {
                        case ModeType.Air:
                            distanceForLeg = (leg.Distance * Convert.ToDecimal(Properties.Current["JourneyEmissions.AirDistanceFactor"]));
                            break;
                        case ModeType.Coach:
                            distanceForLeg = (leg.Distance * Convert.ToDecimal(Properties.Current["JourneyEmissions.BusDistanceFactor"]));
                            break;
                        default:
                            distanceForLeg = leg.Distance;
                            break;
                    }
                    distanceTotal += distanceForLeg;
                    if (calculateEmissions)
                    {
                        emissions += CalculateEmissionsForLeg(leg, distanceForLeg);
                    }
                }
                else
                {
                    // Check if the leg we're currently working on has valid osgrs
                    if (HasValidCoordinates(leg.Geometry))
                    {
                        // This leg is valid, but check if previous leg we looked at 
                        // added osgrs to the the arraylist
                        if (al.Count > 0)
                        {
                            #region HaveItems
                            // Because there are items in arraylist, previous leg contained
                            // invalid coordinates. So we need to create a new Geometry of
                            // osgrs which contained the previous leg and this current leg's
                            for (int j = 0; j < leg.Geometry.Length; j++)
                            {
                                if (leg.Geometry[j].IsValid)
                                {
                                    al.Add(leg.Geometry[j]);
                                }
                            }

                            geometry = new OSGridReference[al.Count];
                            al.CopyTo(geometry); // new Geometry of coordinates created

                            // Determine which modeType we should use for this new Geometry.
                            // Check by comparing leg durations
                            if (leg.Duration >= duration)
                                modeType = leg.Mode;

                            // Calculate out the distance by using the overloaded calculate method
                            distanceForLeg = CalculateDistanceForOSGRs(geometry, modeType);
                            distanceTotal += distanceForLeg;

                            if (calculateEmissions)
                                emissions += GetEmissions(modeType, distanceForLeg);
                            #endregion
                        }
                        else // Previous leg was also valid so no need to worry about merging Geometry's
                        {
                            #region NoItems
                            distanceForLeg = CalculateDistanceForOSGRs(leg.Geometry, leg.Mode);
                            distanceTotal += distanceForLeg;

                            if (calculateEmissions)
                            {
                                emissions += CalculateEmissionsForLeg(leg, distanceForLeg);
                            }
                            #endregion
                        }

                        // Clear the arraylist to prevent any previous osgrs from being used again
                        al.Clear();
                        // Reset the duration for next loop iteration
                        duration = 0;
                    }
                    else
                    {
                        #region InvalidGeometry
                        // Because the current leg has invalid osgrs, copy all of
                        // the valid osgrs to the temp arraylist
                        for (int k = 0; k < leg.Geometry.Length; k++)
                        {
                            if (leg.Geometry[k].IsValid)
                            {
                                al.Add(leg.Geometry[k]);
                            }
                        }

                        // Set the duration and modeType, for determining the mode to use on next loop.
                        // First check the duration, because we may have two loops in succession which
                        // have invalid OSGRs. Duration is always set to 0 on a valid loop.
                        if (leg.Duration >= duration)
                        {
                            duration = leg.Duration;
                            modeType = leg.Mode;
                        }
                        #endregion
                    }
                }      
            } // End For loop

            // Tidy up. In case the last leg we looked at had invalid coordinates
            if (al.Count > 0)
            {
                geometry = new OSGridReference[al.Count];
                al.CopyTo(geometry); // new Geometry of coordinates created

                // Finally, work out the distance by using the overloaded calculate method
                distanceForLeg = CalculateDistanceForOSGRs(geometry, modeType);
                distanceTotal += distanceForLeg;

                if (calculateEmissions)
                    emissions += GetEmissions(modeType, distanceForLeg);
            }

            if (calculateEmissions)
                return emissions;
            else
                return distanceTotal;
        }

        #region Calculate CO2 Emissions for Transport Modes

        /// <summary>
        /// This method allows the potential to use additional details from a leg to calculate
        /// the emissions, e.g. for a Metro - using the NaPTAN to select a specific emissions factor.
        /// This method will need updating to handle the ModeType specifically required, and the 
        /// associated CalculateEmissionsForMODETYPE being updated or overloaded.
        /// </summary>
        /// <param name="leg">PublicJourneyDetail leg</param>
        /// <param name="distance">Distance as decimal</param>
        /// <returns>Emissions as decimal</returns>
        private decimal CalculateEmissionsForLeg(PublicJourneyDetail leg, Decimal distance)
        {
            decimal emissions = 0;
            bool emissionsCalculated = false;

            //If the origin and dest are different countries it must be the international  
            //trunk leg - different processing needed, country code could be null
            if ((leg.StartLocation.Country != null) && (leg.EndLocation.Country != null))
            {
                if (leg.StartLocation.Country.CountryCode != leg.EndLocation.Country.CountryCode)
                {
                    //If an international journey we want to call CalculateInternationalPTJourneyEmissions - can vary
                    //by operator code
                    string operatorCode = string.Empty;

                    if ((leg.Services != null) && (leg.Services.Length > 0))
                    {
                        ServiceDetails[] serviceDetails = leg.Services;
                        operatorCode = serviceDetails[0].OperatorCode;
                    }
                    emissions = RoundEmissions(CalculateInternationalPTJourneyEmissions(distance, operatorCode, leg.Mode));
                    emissionsCalculated = true;
                }
            }
            //If we did'nt already get emissions using international factors use domestic
            if (!emissionsCalculated)
            {
                // Because for some ModeTypes we want to provide additional params so a more accurate
                // emissions value can be calculated, we need to check the mode and call the 
                // appropriate calculate method
                switch (leg.Mode)
                {
                    case ModeType.Metro:
                    case ModeType.Tram:
                    case ModeType.Underground:
                        {
                            string naptan = string.Empty;
                            string operatorCode = string.Empty;

                            if (leg.LegStart.Location.NaPTANs.Length > 0)
                            {
                                TDNaptan[] naptans = leg.LegStart.Location.NaPTANs;
                                // use the first naptan as there should only be one
                                naptan = naptans[0].Naptan;
                            }

                            if ((leg.Services != null) && (leg.Services.Length > 0))
                            {
                                ServiceDetails[] serviceDetails = leg.Services;
                                operatorCode = serviceDetails[0].OperatorCode;
                            }
                            emissions = RoundEmissions(CalculateEmissionsForLightRail(distance, naptan, operatorCode, leg.Mode));
                        }
                        break;
                    default:
                        emissions = GetEmissions(leg.Mode, distance);
                        break;
                }
            }

            return emissions;
        }

        #region Individual mode calculations

        /// <summary>
        /// Calculates the CO2 Emissions for transport mode Air
        /// </summary>
        /// <param name="distance">Distance in metres</param>
        /// <returns>CO2 emission value as decimal</returns>
        private decimal CalculateEmissionsForAir(decimal distance)
        {
            decimal factor = 0;
            decimal distanceSmall = 0;
            decimal distanceMedium = 0;

            try
            {
                distanceSmall = Convert.ToDecimal(Properties.Current["JourneyEmissions.Distance.Air.Small"]) * 1000;
                distanceMedium = Convert.ToDecimal(Properties.Current["JourneyEmissions.Distance.Air.Medium"]) * 1000;
            }
            catch
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Info,
                    "Unable to load Property JourneyEmissions.Distance.Air."));
            }

            if (distance <= distanceSmall)
                factor = emissionsFactor.GetEmissionFactor(FACTOR_AIR_SMALL);
            else if (distance <= distanceMedium)
                factor = emissionsFactor.GetEmissionFactor(FACTOR_AIR_MEDIUM);
            else
                factor = emissionsFactor.GetEmissionFactor(FACTOR_AIR_DEFAULT);

            if (factor > 0)
                return Convert.ToDecimal((distance * factor));
            else
                return 0;
        }

        /// <summary>
        /// Calculates the CO2 Emissions for transport mode Bus
        /// </summary>
        /// <param name="distance">Distance in metres</param>
        /// <returns>CO2 emission value as decimal</returns>
        private decimal CalculateEmissionsForBus(decimal distance)
        {
            decimal factor = emissionsFactor.GetEmissionFactor(FACTOR_BUS_DEFAULT);

            if (factor > 0)
                return Convert.ToDecimal((distance * factor));
            else
                return 0;
        }

        /// <summary>
        /// Calculates the CO2 Emissions for transport mode Coach
        /// </summary>
        /// <param name="distance">Distance in metres</param>
        /// <returns>CO2 emission value as decimal</returns>
        private decimal CalculateEmissionsForCoach(decimal distance)
        {
            decimal factor = emissionsFactor.GetEmissionFactor(FACTOR_COACH_DEFAULT);

            if (factor > 0)
                return Convert.ToDecimal((distance * factor));
            else
                return 0;
        }

        /// <summary>
        /// Calculates the CO2 Emissions for transport mode Ferry
        /// </summary>
        /// <param name="distance">Distance in metres</param>
        /// <returns>CO2 emission value as decimal</returns>
        private decimal CalculateEmissionsForFerry(decimal distance)
        {
            decimal factor = emissionsFactor.GetEmissionFactor(FACTOR_FERRY_DEFAULT);

            if (factor > 0)
                return Convert.ToDecimal((distance * factor));
            else
                return 0;
        }

        #region Lightrail

        /// <summary>
        /// Calculates the CO2 Emissions for transport mode LightRail (Metro, Tram, Underground)
        /// </summary>
        /// <param name="distance">Distance in metres</param>
        /// <param name="naptan">Naptan as string</param>
        /// <param name="operatorCode">Operator code as string</param>
        /// <param name="mode">Mode as ModeType</param>
        /// <returns>CO2 emission value as decimal</returns>
        private decimal CalculateEmissionsForLightRail(decimal distance, string naptan, string operatorCode, ModeType mode)
        {
            decimal factor = -1;

            string NAPTAN_9400 = "9400";
            string NAPTAN_9100 = "9100";
            string naptanPrefix = string.Empty;

            // Get naptan prefix
            if (naptan.Length > 0)
                naptanPrefix = naptan.Substring(0, 4);

            // Get factor for specific Lightrail system, different method used to determine Lightrail code
            if (String.Compare(naptanPrefix, NAPTAN_9400) == 0)
            {
                // Code is the 7-8 characters of NaPTAN
                string lightrailSystem = naptan.Substring(6, 2);
                factor = emissionsFactor.GetEmissionFactor(FACTOR_LIGHTRAIL + lightrailSystem);
            }
            else if (String.Compare(naptanPrefix, NAPTAN_9100) == 0)
            {
                // Convert TOC code to a Lightrail code
                string lightrailSystem = emissionsFactor.GetLightRailSystemCode(operatorCode);
                factor = emissionsFactor.GetEmissionFactor(FACTOR_LIGHTRAIL + lightrailSystem);
            }

            // If no factor was found for a specific Lightrail system, then use the Mode default
            if (factor < 0)
            {
                switch (mode)
                {
                    case ModeType.Metro:
                        factor = emissionsFactor.GetEmissionFactor(FACTOR_METRO_DEFAULT);
                        break;
                    case ModeType.Tram:
                        factor = emissionsFactor.GetEmissionFactor(FACTOR_TRAM_DEFAULT);
                        break;
                    case ModeType.Underground:
                        factor = emissionsFactor.GetEmissionFactor(FACTOR_UNDERGROUND_DEFAULT);
                        break;
                    default:
                        factor = emissionsFactor.GetEmissionFactor(FACTOR_LIGHTRAIL_DEFAULT);
                        break;
                }
            }

            // If there is still no factor found by this point, use the lightrail default
            if (factor < 0)
            {
                factor = emissionsFactor.GetEmissionFactor(FACTOR_LIGHTRAIL_DEFAULT);
            }

            if (factor > 0)
                return Convert.ToDecimal((distance * factor));
            else
                return 0;
        }

        #endregion

        /// <summary>
        /// Calculates the CO2 Emissions for transport mode Rail
        /// </summary>
        /// <param name="distance">Distance in metres</param>
        /// <returns>CO2 emission value as decimal</returns>
        private decimal CalculateEmissionsForRail(decimal distance)
        {
            decimal factor = emissionsFactor.GetEmissionFactor(FACTOR_RAIL_DEFAULT);

            if (factor > 0)
                return Convert.ToDecimal((distance * factor));
            else
                return 0;
        }

        /// <summary>
        /// Calculates the CO2 Emissions for transport mode Telecabine
        /// </summary>
        /// <param name="distance">Distance in metres</param>
        /// <returns>CO2 emission value as decimal</returns>
        private decimal CalculateEmissionsForTelecabine(decimal distance)
        {
            decimal factor = emissionsFactor.GetEmissionFactor(FACTOR_TELECABINE_DEFAULT);

            if (factor > 0)
                return Convert.ToDecimal((distance * factor));
            else
                return 0;
        }

        /// <summary>
        /// Calculates the CO2 Emissions for transport mode Car
        /// </summary>
        /// <param name="distance">Distance in metres</param>
        /// <returns>CO2 emission value as decimal</returns>
        private decimal CalculateEmissionsForCar(decimal distance, string carSize, string fuelType)
        {
            // Calculating the emissions for car using thhe simple formula:
            // Fuel used = Fuel consumption x Journey distance(km) x Journey congestion factor
            // Emissions = Fuel used x Petrol or Diesel factor / 10

            try //place in try-catch for sanity as we're doing database calls
            {
                int fuelConsumption = calculator.GetFuelConsumption(carSize, fuelType);

                decimal congestionFactor = Convert.ToDecimal(Properties.Current["JourneyEmissions.CongestionAndUrbanDrivingFactor"]);

                // calculate fuel used
                decimal fuelUsed = (distance / fuelConsumption) * 1000; // multiply by 1000 to give us 1/1000th litre units

                // adjust by the Congestion Factor
                fuelUsed = fuelUsed * congestionFactor;

                // multiply fuel used with the FuelFactor, this is the coversion factor which gives us the 
                // CO2 emissions
                decimal fuelFactor = calculator.GetFuelFactor(fuelType);

                // fuel factor is stored as 234 (for petrol), but we need it as 2.34 kg/l
                fuelFactor = fuelFactor / 100;

                // gives us emissions in grams
                decimal emissions = (fuelUsed * fuelFactor);

                return emissions;
            }
            catch
            {
                return 0;
            }

        }

        #endregion

        #endregion


        #region International

        /// <summary>
        /// Calculates the CO2 Emissions for international transport modes and operators, uses 
        /// //international mode default if no data for the specific operator
        /// </summary>
        /// <param name="distance">Distance in metres</param>
        /// <param name="operatorCode">Operator code as string</param>
        /// <param name="mode">Mode as ModeType</param>
        /// <returns>CO2 emission value as decimal</returns>
        private decimal CalculateInternationalPTJourneyEmissions(decimal distance, string operatorCode, ModeType mode)
        {
            decimal factor = -1;

            // If no factor was found for a specific operator, then use the international mode default
            if (factor < 0)
            {
                switch (mode)
                {
                    case ModeType.Rail:
                        factor = emissionsFactor.GetEmissionFactor(FACTOR_INTERNATIONAL_RAIL_DEFAULT + operatorCode);
                        if (factor < 0) { factor = emissionsFactor.GetEmissionFactor(FACTOR_INTERNATIONAL_RAIL_DEFAULT); }
                        break;
                    case ModeType.Air:
                        factor = emissionsFactor.GetEmissionFactor(FACTOR_INTERNATIONAL_AIR_DEFAULT + operatorCode);
                        if (factor < 0) { factor = emissionsFactor.GetEmissionFactor(FACTOR_INTERNATIONAL_AIR_DEFAULT); }
                        break;
                    case ModeType.Coach:
                        factor = emissionsFactor.GetEmissionFactor(FACTOR_INTERNATIONAL_COACH_DEFAULT + operatorCode);
                        if (factor < 0) { factor = emissionsFactor.GetEmissionFactor(FACTOR_INTERNATIONAL_COACH_DEFAULT); }
                        break;
                    case ModeType.Transfer:
                        //emissions not counted for transfers
                        factor = 0;
                        break;
                    default:
                        //should never drop into this as only the above three modes are available for international planner
                        factor = 0;
                        break;
                }
            }

            if (factor > 0)
                return Convert.ToDecimal((distance * factor));
            else
                return 0;
        }

        #endregion



        #region Calculate Distance for Transport Modes

        /// <summary>
        /// Calculates the distance for a supplied journey
        /// </summary>
        /// <param name="journey">Journey</param>
        /// <returns>Distance as decimal</returns>
        private decimal CalculateJourneyDistance(Journey journey)
        {
            decimal distance = 0;

            // Determine the type of journey passed in and calculate distance as appropriate
            if ((journey.Type == TDJourneyType.PublicOriginal) || (journey.Type == TDJourneyType.PublicAmended))
            {
                #region Public Transport Journey
                JourneyControl.PublicJourney publicJourney = journey as JourneyControl.PublicJourney;

                distance = CalculateEmissionsDistanceForPTJourney(publicJourney, false);

                #endregion
            }
            else if (journey.Type == TDJourneyType.RoadCongested)
            {
                #region Road Journey
                // For Car journeys, only need to pass the distance from the CJP planned car journey
                RoadJourney roadJourney = journey as RoadJourney;

                distance = roadJourney.TotalDistance;

                #endregion
            }
            else if (journey.Type == TDJourneyType.Cycle)
            {
                #region Cycle Journey
                // For Cycle journeys, only need to pass the distance from the planned cycle journey
                CycleJourney cycleJourney = journey as CycleJourney;

                distance = cycleJourney.TotalDistance;

                #endregion
            }

            return distance;
        }

        /// <summary>
        /// Calculates the distance for the supplied OSGRs.
        /// </summary>
        /// <param name="osgrs">OSGRs as array of OSGridReference</param>
        /// <param name="modeType">modeType as ModeType</param>
        /// <returns>Distance as decimal</returns>
        private decimal CalculateDistanceForOSGRs(OSGridReference[] osgrs, ModeType modeType)
        {
            decimal distance = 0;

            if (osgrs.Length > 0)
            {
                // Dependent on the mode, use the appropriate calculate distance method
                switch (modeType)
                {	// update/add new methods if a specific mode requires a different distance calculation
                    case ModeType.Air:
                        distance = CalculateDistanceForAir(osgrs);
                        break;
                    case ModeType.Bus:
                    case ModeType.Coach:
                    case ModeType.Drt:
                        distance = CalculateDistanceForBus(osgrs);
                        break;
                    case ModeType.Ferry:
                    case ModeType.Taxi:
                    case ModeType.Transfer:
                    case ModeType.Walk:
                        distance = CalculateDistanceForFerry(osgrs);
                        break;
                    case ModeType.CheckIn:
                    case ModeType.CheckOut:
                        distance = 0;
                        break;
                    case ModeType.Metro:
                    case ModeType.Tram:
                    case ModeType.Underground:
                    case ModeType.RailReplacementBus:
                    case ModeType.Rail:
                    case ModeType.Telecabine:
                        distance = CalculateDistanceForRail(osgrs);
                        break;
                    default:
                        {
                            // Fall back is to calculate distance from start to end as "crow flies"							
                            OSGridReference osgrStart = osgrs[0];
                            OSGridReference osgrEnd = osgrs[osgrs.Length - 1];
                            distance += osgrEnd.DistanceFrom(osgrStart);
                        }
                        break;
                }
            }

            return distance;
        }

        #region Individual mode calculations

        /// <summary>
        /// Calculates the distance for a leg of ModeType Air
        /// </summary>
        /// <param name="journeyDetailLeg"></param>
        /// <returns>Distance in metres as decimal</returns>
        private decimal CalculateDistanceForAir(OSGridReference[] osgrs)
        {
            decimal distance = 0;

            OSGridReference[] geometry = osgrs;

            OSGridReference osgrStart;
            OSGridReference osgrEnd;

            for (int i = 0; i < geometry.Length - 1; i++)
            {
                osgrStart = geometry[i];
                osgrEnd = geometry[i + 1];

                // Only add the distance if both coordinates are valid
                // because some start points of a leg are -1,-1
                if ((osgrStart.IsValid) && (osgrEnd.IsValid))
                {
                    // the distance is a "crow flies" from start to end osgr
                    distance += osgrEnd.DistanceFrom(osgrStart);
                }
            }

            // adjust the distance based on a factor
            decimal airDistanceFactor = Convert.ToDecimal(Properties.Current["JourneyEmissions.AirDistanceFactor"]);

            distance = distance * airDistanceFactor;

            return distance;
        }

        /// <summary>
        /// Calculates the distance for a leg of ModeType Bus
        /// </summary>
        /// <param name="journeyDetailLeg"></param>
        /// <returns>Distance in metres as decimal</returns>
        private decimal CalculateDistanceForBus(OSGridReference[] osgrs)
        {
            decimal distance = 0;

            OSGridReference[] geometry = osgrs;

            OSGridReference osgrStart;
            OSGridReference osgrEnd;

            for (int i = 0; i < geometry.Length - 1; i++)
            {
                osgrStart = geometry[i];
                osgrEnd = geometry[i + 1];

                // Only add the distance if both coordinates are valid
                // because some start points of a leg are -1,-1
                if ((osgrStart.IsValid) && (osgrEnd.IsValid))
                {
                    // the distance is a "crow flies" from start to end osgr
                    distance += osgrEnd.DistanceFrom(osgrStart);
                }
            }

            // adjust the distance based on a factor
            decimal busDistanceFactor = Convert.ToDecimal(Properties.Current["JourneyEmissions.BusDistanceFactor"]);

            distance = distance * busDistanceFactor;

            return distance;
        }

        /// <summary>
        /// Calculates the distance for a leg of ModeType Ferry
        /// </summary>
        /// <param name="journeyDetailLeg"></param>
        /// <returns>Distance in metres as decimal</returns>
        private decimal CalculateDistanceForFerry(OSGridReference[] osgrs)
        {
            decimal distance = 0;

            OSGridReference[] geometry = osgrs;

            OSGridReference osgrStart;
            OSGridReference osgrEnd;

            for (int i = 0; i < geometry.Length - 1; i++)
            {
                osgrStart = geometry[i];
                osgrEnd = geometry[i + 1];

                // Only add the distance if both coordinates are valid
                // because some start points of a leg are -1,-1
                if ((osgrStart.IsValid) && (osgrEnd.IsValid))
                {
                    // the distance is a "crow flies" from start to end osgr
                    distance += osgrEnd.DistanceFrom(osgrStart);
                }
            }

            return distance;
        }

        /// <summary>
        /// Calculates the distance for a leg of ModeType Rail.This is the "crow files" 
        /// distance from start to end via all intermediate calling and passing points. 
        /// Therefore caller must ensure OSGRs array include all intermediate points
        /// </summary>
        /// <param name="journeyDetailLeg"></param>
        /// <returns>Distance in metres as decimal</returns>
        private decimal CalculateDistanceForRail(OSGridReference[] osgrs)
        {
            decimal distance = 0;

            OSGridReference[] geometry = osgrs;

            OSGridReference osgrStart;
            OSGridReference osgrEnd;

            for (int i = 0; i < geometry.Length - 1; i++)
            {
                osgrStart = geometry[i];
                osgrEnd = geometry[i + 1];

                // Only add the distance if both coordinates are valid
                // because some start points of a leg are -1,-1
                if ((osgrStart.IsValid) && (osgrEnd.IsValid))
                {
                    // the distance is a "crow flies" from start to end osgr
                    distance += osgrEnd.DistanceFrom(osgrStart);
                }
            }

            return distance;
        }

        #endregion

        #endregion

        #region Helper methods

        /// <summary>
        /// Checks if all OSGRs in the array are valid
        /// </summary>
        /// <param name="osgrs"></param>
        /// <returns></returns>
        private bool HasValidCoordinates(OSGridReference[] osgrs)
        {
            bool valid = true;

            foreach (OSGridReference osgr in osgrs)
            {
                if (!osgr.IsValid)
                    valid = false;
            }

            return valid;
        }

        /// <summary>
        /// Rounds emissions to one decimal place and converts from Grams to Kg's
        /// </summary>
        /// <param name="emissions"></param>
        /// <returns></returns>
        private decimal RoundEmissions(decimal emissions)
        {
            // convert to kg and round emissions value to 1 decimal place
            emissions = emissions / 1000;
            if (emissions >= Convert.ToDecimal(0.05))
                emissions = Decimal.Round(emissions, 1);
            else // Prevents the posibility of returning 0 emissions, when is actualy 0.04 or less
                emissions = Decimal.Round(emissions, 2);

            // remove any trailing zeros
            string emissionsString = emissions.ToString();
            emissions = Convert.ToDecimal(emissions);

            return emissions;
        }

        #endregion

        #endregion

        #region Public properties and methods

        /// <summary>
        /// Calculates the fuel used for a planned CAR JOURNEY based on the supplied preferences
        /// </summary>
        /// <returns>fuel cost for the journey as a decimal rounded to the nearest whole number</returns>
        public decimal GetFuelCost(CarFuelParameters carFuelParameters)
        {
            return CalculateFuelCost(
                carFuelParameters.CarSize,
                carFuelParameters.CarFuelType,
                carFuelParameters.FuelConsumptionOption,
                carFuelParameters.FuelConsumptionEntered,
                carFuelParameters.FuelConsumptionUnit,
                carFuelParameters.FuelCostOption,
                carFuelParameters.FuelCostEntered);
        }

        #region GetEmissions

        /// <summary>
        /// Calculates the CO2 emissions for the ORIGINAL CAR JOURNEY planned by user
        /// </summary>
        /// <returns>CO2 emissions as a decimal to 1 decimal place, with trailing zeros removed</returns>
        public decimal GetEmissions()
        {
            return CalculateEmissions(
                originalCarSize,
                originalFuelType,
                originalFuelConsumptionOption,
                originalFuelConsumptionEntered,
                originalFuelConsumptionUnit,
                originalFuelCostOption,
                originalFuelCostEntered);
        }

        /// <summary>
        /// Calculates the CO2 emissions for a planned CAR JOURNEY based on the supplied preferences
        /// </summary>
        /// <returns>CO2 emissions as a decimal to 1 decimal place, with trailing zeros removed</returns>
        public decimal GetEmissions(CarFuelParameters carFuelParameters)
        {
            return CalculateEmissions(
                carFuelParameters.CarSize,
                carFuelParameters.CarFuelType,
                carFuelParameters.FuelConsumptionOption,
                carFuelParameters.FuelConsumptionEntered,
                carFuelParameters.FuelConsumptionUnit,
                carFuelParameters.FuelCostOption,
                carFuelParameters.FuelCostEntered);
        }

        /// <summary>
        /// Calculates the CO2 emissions for the supplied Road Journey, using the Car size and Fuel type provided
        /// </summary>
        /// <returns>CO2 emissions as a decimal to 1 decimal place, with trailing zeros removed</returns>
        public decimal GetEmissions(string CarSize, string FuelType, RoadJourney journey, CarFuelParameters originalCarFuelParameters)
        {
            SetOriginalCarAndFuelValues(originalCarFuelParameters, journey.TotalFuelCost);

            return CalculateEmissions(
                CarSize,
                FuelType,
                originalFuelConsumptionOption,
                originalFuelConsumptionEntered,
                originalFuelConsumptionUnit,
                originalFuelCostOption,
                originalFuelCostEntered);
        }

        /// <summary>
        /// Returns the CO2 emissions for the supplied Journey, can be a PT or a Car journey
        /// </summary>
        /// <param name="journey">Journey</param>
        /// <param name="originalCarFuelParameters">Only provide if calculating emissions for car journey</param>
        /// <param name="yourCarFuelParameters">Only provide if calculating emissions for car journey</param>
        /// <returns>CO2 emissions as decimal</returns>
        public decimal GetEmissions(Journey journey, CarFuelParameters originalCarFuelParameters, CarFuelParameters yourCarFuelParameters)
        {
            decimal emissions = 0;

            // Determine the type of journey passed in and calculate emissions as appropriate
            if ((journey.Type == TDJourneyType.PublicOriginal) || (journey.Type == TDJourneyType.PublicAmended))
            {
                #region Public Transport Journey
                JourneyControl.PublicJourney publicJourney = journey as JourneyControl.PublicJourney;

                emissions = CalculateEmissionsDistanceForPTJourney(publicJourney, true);
                #endregion
            }
            else if (journey.Type == TDJourneyType.RoadCongested)
            {
                #region Road Journey
                // For Car journeys, need to calculate emissions using the car journey parameters
                // to return an accurate emissions value, which matches that seen on the Tickets/Costs
                // page or Journey Emissions page
                RoadJourney roadJourney = journey as RoadJourney;

                SetOriginalCarAndFuelValues(originalCarFuelParameters, roadJourney.TotalFuelCost);

                emissions = GetEmissions(yourCarFuelParameters);

                #endregion
            }
            else if (journey.Type == TDJourneyType.Cycle)
            {
                #region Cycle Journey
                CycleJourney cycleJourney = journey as CycleJourney;

                emissions = GetEmissions(ModeType.Cycle, Convert.ToDecimal(cycleJourney.TotalDistance));
                #endregion
            }

            return emissions;
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
            return GetEmissions(modeType, journeyDistance, string.Empty, string.Empty);
        }


        /// <summary>
        /// Returns the CO2 emissions for the parameters Mode Type and Distance.
        /// This method uses international factors if useInternationalFactors = true
        /// for a specified mode type and distance
        /// </summary>
        /// <param name="modeType">Mode type</param>
        /// <param name="journeyDistance">Distance in metres</param>
        /// <returns>CO2 emissions as decimal</returns>
        public decimal GetEmissions(ModeType modeType, decimal journeyDistance, bool useInternationalFactors)
        {
            if (useInternationalFactors)
            {
                return RoundEmissions(CalculateInternationalPTJourneyEmissions(journeyDistance, string.Empty, modeType));
            }
            else
            {
                return GetEmissions(modeType, journeyDistance, string.Empty, string.Empty);
            }

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
            // if distance is 0 then no emissions are produced
            if (journeyDistance <= 0)
            {
                return 0;
            }
            else
            {
                string carSize = carType;
                string fuelType = fuel;
                decimal emissions;

                if (carType != string.Empty)
                {
                    emissions = CalculateEmissionsForCar(journeyDistance, carSize, fuelType);

                }
                else
                {

                    // determine which Transport mode type was requested, and call apporpriate
                    // CalculateEmissions method
                    switch (modeType)
                    {
                        case ModeType.Air:
                            emissions = CalculateEmissionsForAir(journeyDistance);
                            break;
                        case ModeType.Bus:
                        case ModeType.Drt:
                        case ModeType.RailReplacementBus:
                            emissions = CalculateEmissionsForBus(journeyDistance);
                            break;
                        case ModeType.Coach:
                            emissions = CalculateEmissionsForCoach(journeyDistance);
                            break;
                        case ModeType.Car:
                            // Use the default car size/fuel type properties for Journey emissions for car
                            carSize = Properties.Current["JourneyEmissions.CarSize.Default"];
                            fuelType = Properties.Current["JourneyEmissions.FuelType.Default"];
                            emissions = CalculateEmissionsForCar(journeyDistance, carSize, fuelType);
                            break;
                        case ModeType.Ferry:
                            emissions = CalculateEmissionsForFerry(journeyDistance);
                            break;
                        case ModeType.Metro:
                        case ModeType.Tram:
                        case ModeType.Underground:
                            emissions = CalculateEmissionsForLightRail(journeyDistance, string.Empty, string.Empty, modeType);
                            break;
                        case ModeType.Rail:
                            emissions = CalculateEmissionsForRail(journeyDistance);
                            break;
                        case ModeType.Telecabine:
                            emissions = CalculateEmissionsForTelecabine(journeyDistance);
                            break;
                        case ModeType.Taxi:
                            // We use the Medium/Diesel car type for taxis
                            carSize = Properties.Current["JourneyEmissions.CarSize.Medium"];
                            fuelType = Properties.Current["JourneyEmissions.FuelType.Diesel"];
                            emissions = CalculateEmissionsForCar(journeyDistance, carSize, fuelType);
                            break;
                        case ModeType.CheckIn:
                        case ModeType.CheckOut:
                        case ModeType.Transfer:
                        case ModeType.Walk:
                        case ModeType.Cycle:
                            emissions = 0;
                            break;
                        default:
                            emissions = 0;
                            break;
                    }
                }

                emissions = RoundEmissions(emissions);

                return emissions;
            }
        }

        #endregion

        #region GetJourneyDistance

        /// <summary>
        /// Returns the distance for the provided 
        /// journey
        /// </summary>
        /// <param name="sessionManager">TDSessionManager</param>
        /// <returns>Total journey distance as decimal</returns>
        public decimal GetJourneyDistance(Journey journey)
        {
            return CalculateJourneyDistance(journey);
        }

        #endregion

        #region Scales for Journey Emissions Speedo Dials

        /// <summary>
        /// Returns the fuel cost scale max based on the original user journey
        /// </summary>
        public decimal GetFuelCostScaleMax()
        {
            return Convert.ToDecimal(fuelCostScaleMax);
        }

        /// <summary>
        /// Returns the fuel cost scale min based on the original user journey
        /// </summary>
        public decimal GetFuelCostScaleMin()
        {
            return Convert.ToDecimal(fuelCostScaleMin);
        }

        /// <summary>
        /// Returns the emissions scale max based on the original user journey
        /// </summary>
        public decimal GetEmissionsScaleMax()
        {
            return Convert.ToDecimal(emissionScaleMax);
        }

        /// <summary>
        /// Returns the emissions scale min based on the original user journey
        /// </summary>
        public decimal GetEmissionsScaleMin()
        {
            return Convert.ToDecimal(emissionScaleMin);
        }

        #endregion

        #endregion

        #region Static Public properties

        /// <summary>
        /// Static read-only property indicating whether the Journey Emissions PT functionality
        /// should be made available
        /// </summary>
        /// <returns>bool</returns>
        public static bool JourneyEmissionsPTAvailable
        {
            get
            {
                try
                {
                    return bool.Parse(Properties.Current["JourneyEmissions.PTAvailable"]);
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Static read-only property indicating whether the supplied Distance is
        /// within the acceptable range
        /// </summary>
        /// <param name="journeyDistance">Distance in metres</param>
        /// <returns>bool</returns>
        public static bool JourneyDistanceValid(double journeyDistance)
        {
            try
            {
                // The Property values should be in km but we need them as metres
                int minJourneyDistance = Convert.ToInt32(Properties.Current["JourneyEmissions.MinDistance"]);
                int maxJourneyDistance = Convert.ToInt32(Properties.Current["JourneyEmissions.MaxDistance"]);

                minJourneyDistance = minJourneyDistance * 1000;
                maxJourneyDistance = maxJourneyDistance * 1000;

                if ((journeyDistance >= minJourneyDistance) && (journeyDistance <= maxJourneyDistance))
                    return true;
                else
                    return false;

            }
            catch
            {
                return false;
            }
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
            decimal width;

            try
            {
                int maxWidth = Convert.ToInt32(Properties.Current["JourneyEmissions.EmissionBar.MaxWidth"]);

                width = (maxWidth * emissions) / maxEmissions;

                // just in case the width becomes larger than max width
                if (width > maxWidth)
                    width = maxWidth;
            }
            catch
            {
                width = 0;
            }

            return Convert.ToInt32(width);
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
            decimal width;

            try
            {
                int maxWidth = 178;

                width = (maxWidth * emissions) / maxEmissions;

                // just in case the width becomes larger than max width
                if (width > maxWidth)
                    width = maxWidth;
            }
            catch
            {
                width = 0;
            }

            return Convert.ToInt32(width);
        }

        /// <summary>
        /// Determines whether the Air transport mode should be shown, dependent on Distance
        /// </summary>
        /// <param name="journeyDistance">JourneyDistance in Metres</param>
        /// <returns>Bool</returns>
        public static bool ShowAir(decimal journeyDistance)
        {
            try
            {
                int minAirDistance = Convert.ToInt32(Properties.Current["JourneyEmissions.MinDistance.Air"]);

                // Becasue Air distance is stored as kms, convert to metres
                decimal minDistance = minAirDistance * 1000;

                if (journeyDistance >= minDistance)
                    return true;
                else
                    return false;
            }
            catch
            {  // default is to display rather than potentially having no emissions displayed
                return true;
            }
        }

        /// <summary>
        /// Determines whether the Coach transport mode should be shown, dependent on Distance.
        /// </summary>
        /// <param name="journeyDistance">JourneyDistance in metres</param>
        /// <returns>Bool</returns>
        public static bool ShowCoach(decimal journeyDistance)
        {
            try
            {
                int minCoachDistance = Convert.ToInt32(Properties.Current["JourneyEmissions.MinDistance.Coach"]);

                // Becasue Coach distance is stored as kms, convert to metres
                decimal minDistance = minCoachDistance * 1000;

                if (journeyDistance >= minDistance)
                    return true;
                else
                    return false;
            }
            catch
            {  // default is to display rather than potentially having no emissions displayed
                return true;
            }
        }
        
        /// <summary>
        /// Returns the modes used for the Journey
        /// </summary>
        /// <param name="journey">journey as JourneyControl.Journey</param>
        /// <returns>Array of ModeType</returns>
        public static ModeType[] GetJourneyModes(Journey journey)
        {
            // Determine the type of journey passed in and return the mode types
            if ((journey.Type == TDJourneyType.PublicOriginal) || (journey.Type == TDJourneyType.PublicAmended))
            {
                JourneyControl.PublicJourney publicJourney = journey as JourneyControl.PublicJourney;

                return publicJourney.GetUsedModes();
            }
            else if (journey.Type == TDJourneyType.RoadCongested)
            {
                RoadJourney roadJourney = journey as RoadJourney;

                return roadJourney.GetUsedModes();
            }
            else if (journey.Type == TDJourneyType.Cycle)
            {
                CycleJourney cycleJourney = journey as CycleJourney;

                return cycleJourney.GetUsedModes();
            }
            else
            {
                ModeType[] mt = new ModeType[0];
                return mt;
            }
        }

        #endregion
    }

    #region Struct

    /// <summary>
    /// Struct to hold car fuel parameters needed for calculating accurate Car CO2 emissions.
    /// These values should be populated from TDJourneyParametersMulti or JourneyEmissionsPageState as required
    /// </summary>
    public struct CarFuelParameters
    {
        public string CarSize;
        public string CarFuelType;
        public string FuelConsumptionEntered;
        public int FuelConsumptionUnit;
        public bool FuelConsumptionOption;
        public string FuelCostEntered;
        public bool FuelCostOption;

        public CarFuelParameters(string carSize, string carFuelType, string fuelConsumptionEntered,
            int fuelConsumptionUnit, bool fuelConsumptionOption, string fuelCostEntered, bool fuelCostOption)
        {
            this.CarSize = carSize;
            this.CarFuelType = carFuelType;
            this.FuelConsumptionEntered = fuelConsumptionEntered;
            this.FuelConsumptionUnit = fuelConsumptionUnit;
            this.FuelConsumptionOption = fuelConsumptionOption;
            this.FuelCostEntered = fuelCostEntered;
            this.FuelCostOption = fuelCostOption;
        }
    }

    #endregion
}
