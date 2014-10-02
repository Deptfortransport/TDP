// *********************************************** 
// NAME			: CarCostingFuelValidationHelper.cs
// AUTHOR		: R. Bamshad
// DATE CREATED	: 10.05.05
// DESCRIPTION	: Validates fuel consumption and fuel costs entered by the user
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/CarCostingFuelValidationHelper.cs-arc  $
//
//   Rev 1.3   Jan 10 2013 16:16:08   dlane
//Door to door input options
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.2   Mar 31 2008 12:58:42   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:12   mturner
//Initial revision.
//
//   Rev 1.4   Dec 15 2006 15:28:22   mmodi
//Corrected CO2 validation
//Resolution for 4324: CO2: Error when 0 enter for CO2 value
//
//   Rev 1.3   Dec 07 2006 14:37:36   build
//Automatically merged from branch for stream4240
//
//   Rev 1.2.1.3   Dec 05 2006 17:55:40   mmodi
//Amended validation of Fuel consumption for Advanced options
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.2.1.2   Dec 01 2006 11:07:40   mmodi
//Corrected validation problem
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.2.1.1   Nov 24 2006 11:43:14   mmodi
//Added MaxCO2 value
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.2.1.0   Nov 16 2006 17:40:30   mmodi
//Added validation methods for specific fuel cost and consumption values parameters
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.2   Feb 23 2006 19:16:02   build
//Automatically merged from branch for stream3129
//
//   Rev 1.1.1.1   Jan 30 2006 12:15:14   mdambrine
//add TDCultureInfo is now in the common namespace
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1.1.0   Jan 10 2006 15:17:28   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   May 10 2005 17:35:50   Ralavi
//Helper class for validating fuel consumption and fuel costs.
using System;using TransportDirect.Common.ResourceManager;
using System.Collections;
using System.Globalization;

using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;


using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TransportDirect.UserPortal.ScriptRepository;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Summary description for CarCostingFuelValidationHelper.
	/// </summary>
	public class CarCostingFuelValidationHelper
	{
		string minFuelConsumption;
		string maxConsumptionLitresPer100Km;
		string maxConsumptionMilesPerGallon;
		string maxConsumptionCO2PerKm;
		string minFuelCost;
		string maxFuelCost;

		public CarCostingFuelValidationHelper()
		{
			IPropertyProvider properties = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];
			
			// Get the configured values from properties
			minFuelConsumption = properties[ "CarCosting.MinFuelConsumption" ];
			maxConsumptionLitresPer100Km = properties[ "CarCosting.MaxConsumptionLitresPer100Km" ];
			maxConsumptionMilesPerGallon = properties[ "CarCosting.MaxConsumptionMilesPerGallon" ];
			maxConsumptionCO2PerKm = properties[ "CarCosting.MaxCO2PerKm" ];
			minFuelCost = properties[ "CarCosting.MinFuelCost" ];
			maxFuelCost = properties[ "CarCosting.MaxFuelCost" ];
		}
	
	
		public void FuelCostValidation(TDJourneyParametersMulti journeyParameters, FindCarPreferencesControl carPreferencesControl)
		{	// If values are entered by the user, ensure that they are greater than the minimum value allowed and smaller than the 
			// maximum values allowed
			if (carPreferencesControl.FuelCostOption == false)
			{
				if (carPreferencesControl.InputCostText.Length != 0) 
				{
					carPreferencesControl.FuelCostValid = true;
					journeyParameters.FuelCostValid = carPreferencesControl.FuelCostValid;
                        
					
					try
					{

						double.Parse(carPreferencesControl.InputCostText, System.Globalization.NumberStyles.AllowDecimalPoint);
							
						if (((Convert.ToDouble(carPreferencesControl.InputCostText)) < Convert.ToDouble(minFuelCost) ) || ((Convert.ToDouble(carPreferencesControl.InputCostText)) > Convert.ToDouble(maxFuelCost)))
						{
							throw new Exception();
						}
					}
						
					catch 
					{
							
						carPreferencesControl.FuelCostValid = false;
						journeyParameters.FuelCostValid = carPreferencesControl.FuelCostValid;
						carPreferencesControl.FuelCostValue = journeyParameters.FuelCostEntered;
					}
				}
				else
				{
					carPreferencesControl.FuelCostValid = false;
					journeyParameters.FuelCostValid = carPreferencesControl.FuelCostValid;
					carPreferencesControl.FuelCostValue = journeyParameters.FuelCostEntered;
				}
			}
			else
			{
				carPreferencesControl.FuelCostValid = true;
				journeyParameters.FuelCostValid = carPreferencesControl.FuelCostValid;
			}
		}

        public void FuelCostValidation(TDJourneyParametersMulti journeyParameters, D2DCarPreferencesControl carPreferencesControl)
        {	// If values are entered by the user, ensure that they are greater than the minimum value allowed and smaller than the 
            // maximum values allowed
            if (carPreferencesControl.FuelCostOption == false)
            {
                if (carPreferencesControl.InputCostText.Length != 0)
                {
                    carPreferencesControl.FuelCostValid = true;
                    journeyParameters.FuelCostValid = carPreferencesControl.FuelCostValid;


                    try
                    {

                        double.Parse(carPreferencesControl.InputCostText, System.Globalization.NumberStyles.AllowDecimalPoint);

                        if (((Convert.ToDouble(carPreferencesControl.InputCostText)) < Convert.ToDouble(minFuelCost)) || ((Convert.ToDouble(carPreferencesControl.InputCostText)) > Convert.ToDouble(maxFuelCost)))
                        {
                            throw new Exception();
                        }
                    }

                    catch
                    {

                        carPreferencesControl.FuelCostValid = false;
                        journeyParameters.FuelCostValid = carPreferencesControl.FuelCostValid;
                        carPreferencesControl.FuelCostValue = journeyParameters.FuelCostEntered;
                    }
                }
                else
                {
                    carPreferencesControl.FuelCostValid = false;
                    journeyParameters.FuelCostValid = carPreferencesControl.FuelCostValid;
                    carPreferencesControl.FuelCostValue = journeyParameters.FuelCostEntered;
                }
            }
            else
            {
                carPreferencesControl.FuelCostValid = true;
                journeyParameters.FuelCostValid = carPreferencesControl.FuelCostValid;
            }
        }

        public void FuelConsumptionValidation(TDJourneyParametersMulti journeyParameters, FindCarPreferencesControl carPreferencesControl)
		{
			// If values are entered by the user, ensure that they are greater than the minimum value allowed and smaller than the 
			// maximum values allowed. The maximum values allowed for fuel consumption are different depending on the unit used.

			if (carPreferencesControl.FuelConsumptionOption == false)
			{
				if (carPreferencesControl.InputConsumptionText.Length != 0) 
				{
					carPreferencesControl.FuelConsumptionValid = true;
					journeyParameters.FuelConsumptionValid = carPreferencesControl.FuelConsumptionValid;
			
					try
					{

						double.Parse(carPreferencesControl.InputConsumptionText, System.Globalization.NumberStyles.AllowDecimalPoint);

							// MPG
						if (carPreferencesControl.FuelConsumptionUnit == 1)
						{
							if ( 
								(Convert.ToDouble(carPreferencesControl.InputConsumptionText) < Convert.ToDouble(minFuelConsumption) )
								||
								(Convert.ToDouble(carPreferencesControl.InputConsumptionText) > Convert.ToDouble(maxConsumptionMilesPerGallon) )
								)
							{
								throw new Exception();
							}
						}
							//L KM
						else if (carPreferencesControl.FuelConsumptionUnit == 2)
						{
							if ( 
								( Convert.ToDouble(carPreferencesControl.InputConsumptionText) > Convert.ToDouble(maxConsumptionLitresPer100Km) )
								|| 
								( Convert.ToDouble(carPreferencesControl.InputConsumptionText) <= 0 )
								)
							{
								throw new Exception();
							}
						}
					}
						
					catch 
					{
							
						carPreferencesControl.FuelConsumptionValid = false;
						journeyParameters.FuelConsumptionValid = carPreferencesControl.FuelConsumptionValid;
						carPreferencesControl.FuelConsumptionValue = journeyParameters.FuelConsumptionEntered;
					}
				}
				else
				{
					carPreferencesControl.FuelConsumptionValid = false;
					journeyParameters.FuelConsumptionValid = carPreferencesControl.FuelConsumptionValid;
					carPreferencesControl.FuelConsumptionValue = journeyParameters.FuelConsumptionEntered;
				}
			}
			else
			{
				carPreferencesControl.FuelConsumptionValid = true;
				journeyParameters.FuelConsumptionValid = carPreferencesControl.FuelConsumptionValid;
			}

		}

        public void FuelConsumptionValidation(TDJourneyParametersMulti journeyParameters, D2DCarPreferencesControl carPreferencesControl)
        {
            // If values are entered by the user, ensure that they are greater than the minimum value allowed and smaller than the 
            // maximum values allowed. The maximum values allowed for fuel consumption are different depending on the unit used.

            if (carPreferencesControl.FuelConsumptionOption == false)
            {
                if (carPreferencesControl.InputConsumptionText.Length != 0)
                {
                    carPreferencesControl.FuelConsumptionValid = true;
                    journeyParameters.FuelConsumptionValid = carPreferencesControl.FuelConsumptionValid;

                    try
                    {

                        double.Parse(carPreferencesControl.InputConsumptionText, System.Globalization.NumberStyles.AllowDecimalPoint);

                        // MPG
                        if (carPreferencesControl.FuelConsumptionUnit == 1)
                        {
                            if (
                                (Convert.ToDouble(carPreferencesControl.InputConsumptionText) < Convert.ToDouble(minFuelConsumption))
                                ||
                                (Convert.ToDouble(carPreferencesControl.InputConsumptionText) > Convert.ToDouble(maxConsumptionMilesPerGallon))
                                )
                            {
                                throw new Exception();
                            }
                        }
                        //L KM
                        else if (carPreferencesControl.FuelConsumptionUnit == 2)
                        {
                            if (
                                (Convert.ToDouble(carPreferencesControl.InputConsumptionText) > Convert.ToDouble(maxConsumptionLitresPer100Km))
                                ||
                                (Convert.ToDouble(carPreferencesControl.InputConsumptionText) <= 0)
                                )
                            {
                                throw new Exception();
                            }
                        }
                    }

                    catch
                    {

                        carPreferencesControl.FuelConsumptionValid = false;
                        journeyParameters.FuelConsumptionValid = carPreferencesControl.FuelConsumptionValid;
                        carPreferencesControl.FuelConsumptionValue = journeyParameters.FuelConsumptionEntered;
                    }
                }
                else
                {
                    carPreferencesControl.FuelConsumptionValid = false;
                    journeyParameters.FuelConsumptionValid = carPreferencesControl.FuelConsumptionValid;
                    carPreferencesControl.FuelConsumptionValue = journeyParameters.FuelConsumptionEntered;
                }
            }
            else
            {
                carPreferencesControl.FuelConsumptionValid = true;
                journeyParameters.FuelConsumptionValid = carPreferencesControl.FuelConsumptionValid;
            }

        }

		public void AmbiguityFuelValidation(TDJourneyParametersMulti journeyParameters, FindCarPreferencesControl carPreferencesControl)
		{
			// If values are entered by the user, ensure that they are greater than the minimum value allowed and smaller than the 
			// maximum values allowed. This method is used by Ambiguity page only because it works a little different to FindCarInput and JourneyPlannerInput
			if ((carPreferencesControl.InputConsumptionText.Length != 0) && (carPreferencesControl.FuelConsumptionOption == journeyParameters.FuelConsumptionOption) && 
				(carPreferencesControl.FuelConsumptionOption == false))
			{
				
				journeyParameters.FuelConsumptionValid = true;
					
				try
				{

					double.Parse(carPreferencesControl.InputConsumptionText, System.Globalization.NumberStyles.AllowDecimalPoint);
							
					if (((Convert.ToDouble(carPreferencesControl.InputConsumptionText)) < Convert.ToDouble(minFuelConsumption) ) || (((Convert.ToDouble(carPreferencesControl.InputConsumptionText)) > Convert.ToDouble(maxConsumptionMilesPerGallon))&& (carPreferencesControl.FuelConsumptionUnit == 1)) ||
							(((Convert.ToDouble(carPreferencesControl.InputConsumptionText)) > Convert.ToDouble(maxConsumptionLitresPer100Km)&& (carPreferencesControl.FuelConsumptionUnit == 2))))
					{
						throw new Exception();
					}
							
				}
						
				catch 
				{
							
					carPreferencesControl.FuelConsumptionValid = false;
					journeyParameters.FuelConsumptionValid = carPreferencesControl.FuelConsumptionValid;
					carPreferencesControl.FuelConsumptionOption = journeyParameters.FuelConsumptionOption;
				}
			}

			if (carPreferencesControl.FuelConsumptionValue != "")
			{
				journeyParameters.FuelConsumptionEntered = carPreferencesControl.FuelConsumptionValue;
			}

			if ((carPreferencesControl.InputCostText.Length != 0) && (carPreferencesControl.FuelCostOption == journeyParameters.FuelCostOption) && 
				(carPreferencesControl.FuelCostOption == false))
			{
				
				journeyParameters.FuelCostValid = true;
					
				try
				{

					double.Parse(carPreferencesControl.InputCostText, System.Globalization.NumberStyles.AllowDecimalPoint);
							
					if (((Convert.ToDouble(carPreferencesControl.InputCostText)) < Convert.ToDouble(minFuelCost) ) || ((Convert.ToDouble(carPreferencesControl.InputCostText)) > Convert.ToDouble(maxFuelCost)))
					{
						throw new Exception();
					}
							
				}
						
				catch 
				{
							
					carPreferencesControl.FuelCostValid = false;
					journeyParameters.FuelCostValid = carPreferencesControl.FuelCostValid;
					carPreferencesControl.FuelCostOption = journeyParameters.FuelCostOption;
				}
			}

		}
	
		/// <summary>
		/// Validates the supplied fuel cost against the value from Properties datatable
		/// </summary>
		/// <param name="fuelCost">Fuel Cost as string</param>
		/// <returns>True or False</returns>
		public bool ValidateFuelCost(string fuelCost)
		{
			// If values are entered by the user, ensure that they are greater than the minimum value allowed and smaller than the 
			// maximum values allowed

			bool isValid = true;

				if (fuelCost.Length != 0) 
				{	
					try
					{
						double.Parse(fuelCost, System.Globalization.NumberStyles.AllowDecimalPoint);
							
						if (((Convert.ToDouble(fuelCost)) < Convert.ToDouble(minFuelCost) ) || ((Convert.ToDouble(fuelCost)) > Convert.ToDouble(maxFuelCost)))
						{
							throw new Exception();
						}
					}
					catch 
					{		
						isValid = false;
					}

					return isValid;
				}
				else
				{
					return false;
				}
		}

		/// <summary>
		/// Validates the supplied fuel consumption against the value from Properties datatable
		/// </summary>
		/// <param name="fuelConsumption">Fuel consumption as string</param>
		/// <param name="fuelConsumptionUnit">Fuel consumption unit as int. 1 = mpg, 2 = litres/100km, 3 = CO2 per km</param>
		/// <returns></returns>
		public bool ValidateFuelConsumption(string fuelConsumption, int fuelConsumptionUnit)
		{
			// If values are entered by the user, ensure that they are greater than the minimum value allowed and smaller than the 
			// maximum values allowed. The maximum values allowed for fuel consumption are different depending on the unit used.
            
			bool isValid = true;

			if (fuelConsumption.Length != 0) 
				{
					try
					{
						double.Parse(fuelConsumption, System.Globalization.NumberStyles.AllowDecimalPoint);
						
	                    // Consumption Unit 3 indicates we want to validate the CO2 emissions value entered
						if (fuelConsumptionUnit == 3)
						{	
							if (  (Convert.ToDouble(fuelConsumption)) >  (Convert.ToDouble(maxConsumptionCO2PerKm))
								|| 
								( Convert.ToDouble(fuelConsumption) <= 0) 
								)
							{
								throw new Exception();
							}
						}
							// MPG
						else if (fuelConsumptionUnit == 1)
						{
							if ( (Convert.ToDouble(fuelConsumption) < Convert.ToDouble(minFuelConsumption))
								||
									(Convert.ToDouble(fuelConsumption) > Convert.ToDouble(maxConsumptionMilesPerGallon))
								)
							{
								throw new Exception();
							}
						}
							//L KM
						else if (fuelConsumptionUnit == 2)
						{
							if ( ( Convert.ToDouble(fuelConsumption) > Convert.ToDouble(maxConsumptionLitresPer100Km))
								|| 
								( Convert.ToDouble(fuelConsumption) <= 0)
								)
							{
								throw new Exception();
							}
						}
					}						
					catch 
					{						
						isValid = false;						
					}

					return isValid;
				}
			else
			{
				return false;
			}
		}
	}

}
