// *********************************************** 
// NAME                 : EBCCalculationFormatter.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 24/09/2009
// DESCRIPTION          : Class to return formatted environmental benefits details to be displayed 
//                        from an EnvironmentalBenefits object
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/EBCCalculationFormatter.cs-arc  $
//
//   Rev 1.4   Oct 28 2009 15:50:10   mmodi
//Update pound formatter following change in EBC returning benefit as pound
//Resolution for 5332: EBC - Pounds totals are incorrectly added up
//
//   Rev 1.3   Oct 26 2009 10:07:02   mmodi
//Display distances to 2dp
//
//   Rev 1.2   Oct 15 2009 13:18:46   mmodi
//Updated to format distances from metres value
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 11 2009 12:40:16   mmodi
//Added pence per mile in output
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Oct 06 2009 14:15:50   mmodi
//Initial revision.
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.EnvironmentalBenefits;
using TransportDirect.UserPortal.SessionManager;

using EB = TransportDirect.UserPortal.EnvironmentalBenefits;

namespace TransportDirect.UserPortal.Web.Adapters
{
    public class EBCCalculationFormatter
    {
        #region Private members

        private EB.EnvironmentalBenefitsCalculator calculator;

        private EB.EnvironmentalBenefits environmentalBenefits;

        private RoadUnitsEnum roadUnits;

        // int to determine how many decimal places should be used for the distance display
        private int distanceDecimalPlaces = 2;

        #region Display strings

        private string miles;
        private string km;
        private string perMile;
        private string perKm;

        private string total;
        private string grandTotal;

        private string roadCategory;
        private string roadCategoryHighMotorway;
        private string roadCategoryStandardMotorway;
        private string roadCategoryStandardARoad;
        private string roadCategoryOtherRoads;

        private string countryEngland;
        private string countryScotland;
        private string countryWales;

        private string space;
        private string dash;
        private string linebreak;

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="environmentalBenefits"></param>
        public EBCCalculationFormatter(EB.EnvironmentalBenefits environmentalBenefits, RoadUnitsEnum roadUnits)
        {
            this.environmentalBenefits = environmentalBenefits;
            this.roadUnits = roadUnits;

            this.calculator = (EnvironmentalBenefitsCalculator)TDServiceDiscovery.Current[ServiceDiscoveryKey.EnvironmentalBenefitsCalculator];

            LoadResources();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the resource strings needed by the formatter
        /// </summary>
        private void LoadResources()
        {
            miles = Global.tdResourceManager.GetString("EBCCalculationFormatter.Miles");
            km = Global.tdResourceManager.GetString("EBCCalculationFormatter.Km");
            perMile = Global.tdResourceManager.GetString("EBCCalculationFormatter.PerMile");
            perKm = Global.tdResourceManager.GetString("EBCCalculationFormatter.PerKm");

            total = Global.tdResourceManager.GetString("EBCCalculationFormatter.Total");
            grandTotal = Global.tdResourceManager.GetString("EBCCalculationFormatter.GrandTotal");

            roadCategory = Global.tdResourceManager.GetString("EBCCalculationFormatter.RoadCategory");
            roadCategoryHighMotorway = Global.tdResourceManager.GetString("EBCCalculationFormatter.RoadCategoryHighMotorway");
            roadCategoryStandardMotorway = Global.tdResourceManager.GetString("EBCCalculationFormatter.RoadCategoryStandardMotorway");
            roadCategoryStandardARoad = Global.tdResourceManager.GetString("EBCCalculationFormatter.RoadCategoryStandardARoad");
            roadCategoryOtherRoads = Global.tdResourceManager.GetString("EBCCalculationFormatter.RoadCategoryOtherRoads");

            countryEngland = Global.tdResourceManager.GetString("EBCCalculationFormatter.CountryEngland");
            countryScotland = Global.tdResourceManager.GetString("EBCCalculationFormatter.CountryScotland");
            countryWales = Global.tdResourceManager.GetString("EBCCalculationFormatter.CountryWales");

            space = " ";
            dash = "-";
            linebreak = "<br />";
        }

        /// <summary>
        /// Generates the formatted array of environmental benefits details
        /// </summary>
        /// <returns></returns>
        private IList ProcessEnvironmentalBenefits()
        {
            ArrayList details = new ArrayList();

            if (environmentalBenefits == null)
            {
                return details;
            }
            else
            {
                // Create an array containing a row for each EBCRoadCategory type. 
                // Each row then contains the EB data for the 3 different countries

                foreach (int value in Enum.GetValues(typeof(EBCRoadCategory)))
                {
                    EBCRoadCategory ebcRoadCategory = (EBCRoadCategory)value;

                    if (ebcRoadCategory != EBCRoadCategory.None)
                    {
                        details.Add(ProcessEnvironmentalBenefitsForRoadCategory(ebcRoadCategory));
                    }
                }
                
                return details;
            }
        }

        /// <summary>
        /// Creates a string array containing the Environmental Benefit data for the specified EBCRoadCategory.
        /// </summary>
        /// <param name="ebcRoadCategory"></param>
        /// <returns></returns>
        private string[] ProcessEnvironmentalBenefitsForRoadCategory(EBCRoadCategory ebcRoadCategory)
        {
            // Create a string array in the following format

            // Position/values
            // 0 = EBCRoadCategory
            // 1 = distance mile/km England
            // 2 = distance mile/km Scotland
            // 3 = distance mile/km Wales
            // 4 = distance mile/km total
            // 5 = road category factor (pence per mile/km)
            // 6 = benefit amount England
            // 7 = benefit amount Scotland
            // 8 = benefit amount Wales
            // 9 = benefit amount total

            string[] details = new string[10];
            
            #region Do the work

            double distanceMetres = 0;

            // England
            distanceMetres = environmentalBenefits.GetTotalBenefitDistance(ebcRoadCategory, EBCCountry.England);

            string distanceTextEngland = GetDistanceDisplayText(distanceMetres, roadUnits);

            // Scotland
            distanceMetres = environmentalBenefits.GetTotalBenefitDistance(ebcRoadCategory, EBCCountry.Scotland);

            string distanceTextScotland = GetDistanceDisplayText(distanceMetres, roadUnits);

            // Wales
            distanceMetres = environmentalBenefits.GetTotalBenefitDistance(ebcRoadCategory, EBCCountry.Wales);

            string distanceTextWales = GetDistanceDisplayText(distanceMetres, roadUnits);
            
            // Total
            distanceMetres = environmentalBenefits.GetTotalBenefitDistance(ebcRoadCategory, EBCCountry.None);

            string distanceTextTotal = GetDistanceDisplayText(distanceMetres, roadUnits);

            // Road category factor (Pence per mile/km)(always show England value)
            double pencePerMile = calculator.EBCData.GetRoadCategoryCost(ebcRoadCategory, EBCCountry.England);
            double pencePerKm = 0;
            if (pencePerMile > 0)
            {
                // Convert to km
                pencePerKm = pencePerMile / (double)1.609;
            }

            string roadCategoryFactor = GetRoadCategoryBenefitFactorDisplayText(pencePerMile, pencePerKm, roadUnits);

            #endregion

            details[0] = GetEBCRoadCategoryDisplayText(ebcRoadCategory);
            details[1] = distanceTextEngland;
            details[2] = distanceTextScotland;
            details[3] = distanceTextWales;
            details[4] = distanceTextTotal;
            details[5] = roadCategoryFactor;
            details[6] = GetPoundsDisplayText(environmentalBenefits.GetTotalBenefitAmount(ebcRoadCategory, EBCCountry.England));
            details[7] = GetPoundsDisplayText(environmentalBenefits.GetTotalBenefitAmount(ebcRoadCategory, EBCCountry.Scotland));
            details[8] = GetPoundsDisplayText(environmentalBenefits.GetTotalBenefitAmount(ebcRoadCategory, EBCCountry.Wales));
            details[9] = GetPoundsDisplayText(environmentalBenefits.GetTotalBenefitAmount(ebcRoadCategory, EBCCountry.None));
            
            return details;
        }

        #region Helpers

        /// <summary>
        /// Returns the display text for the specified EBCRoadCategory
        /// </summary>
        /// <param name="ebcRoadCategory"></param>
        /// <returns></returns>
        private string GetEBCRoadCategoryDisplayText(EBCRoadCategory ebcRoadCategory)
        {
            switch (ebcRoadCategory)
            {
                case EBCRoadCategory.MotorwayHigh:
                    return roadCategoryHighMotorway;
                case EBCRoadCategory.MotorwayStandard:
                    return roadCategoryStandardMotorway;
                case EBCRoadCategory.RoadStandardA:
                    return roadCategoryStandardARoad;
                case EBCRoadCategory.RoadOther:
                    return roadCategoryOtherRoads;
                default:
                    return dash;
            }
        }

        /// <summary>
        /// Returns the display text for the distance, in the format "10 miles" or "10 km". 
        /// The distances are wrapped with hidden or show div tags/classes dependent on the units to show value.
        /// </summary>
        /// <param name="distanceMetres"></param>
        /// <param name="roadUnitToShow"></param>
        /// <returns></returns>
        private string GetDistanceDisplayText(double distanceMetres, RoadUnitsEnum roadUnitToShow)
        {
            string strDistanceMiles = dash;
            string strDistanceKms = dash;

            string distanceFormat = GetDistanceFormat();

            // Check distance is over 0
            if (distanceMetres > 0)
            {
                double distanceMiles = 0;
                double distanceKms = 0;

                #region Convert to miles

                // Retrieve the conversion factor from the Properties Service.
                double conversionFactor =
                    Convert.ToDouble(Properties.Current["Web.Controls.MileageConverter"], TDCultureInfo.CurrentUICulture.NumberFormat);

                distanceMiles = (double)distanceMetres / conversionFactor;

                #endregion

                #region Convert to kms

                distanceKms = (double)distanceMetres / 1000;

                #endregion

                strDistanceMiles = distanceMiles.ToString(distanceFormat, TDCultureInfo.CurrentUICulture.NumberFormat)
                    + space + linebreak + miles;
                
                strDistanceKms = distanceKms.ToString(distanceFormat, TDCultureInfo.CurrentUICulture.NumberFormat)
                    + space + linebreak + km;
            }

            // Create the display text
            if (roadUnitToShow == RoadUnitsEnum.Miles)
            {
                return 
                    "<span class=\"milesshow\">" + strDistanceMiles + "</span>"
                  + "<span class=\"kmshide\">" + strDistanceKms + "</span>";
            }
            else
            {
                return 
                    "<span class=\"mileshide\">" + strDistanceMiles + "</span>"
                  + "<span class=\"kmsshow\">" + strDistanceKms + "</span>";
            }
        }

        /// <summary>
        /// Returns the benefit amount display text, formatted as £1.23
        /// </summary>
        /// <param name="pounds"></param>
        /// <returns></returns>
        private string GetPoundsDisplayText(double pounds)
        {
            string strBenefitAmountText = dash;

            if (pounds > 0)
            {
                // Round to 2dp
                pounds = Math.Round(pounds, 2);

                // Add the £ symbol
                strBenefitAmountText = TDCultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol
                    + pounds.ToString("##0.00");
            }

            return strBenefitAmountText;
        }

        /// <summary>
        /// Returns the display text for the pence per mile/km, in the format "0.34 per mile" or "0.45 per km". 
        /// The pence values are wrapped with hidden or show div tags/classes dependent on the units to show value.
        /// </summary>
        /// <param name="pencePerMile"></param>
        /// <param name="pencePerKm"></param>
        /// <param name="roadUnitToShow"></param>
        /// <returns></returns>
        private string GetRoadCategoryBenefitFactorDisplayText(double pencePerMile, double pencePerKm, RoadUnitsEnum roadUnitToShow)
        {
            string strPencePerMile = dash;
            string strPencePerKm = dash;

            // Check distance is over 0
            if (pencePerMile > 0)
            {
                // Convert to pounds
                pencePerMile = pencePerMile / (double)100;

                strPencePerMile = GetPoundsDisplayText(pencePerMile) + space + perMile;
            }

            if (pencePerKm > 0)
            {
                // Convert to pounds
                pencePerKm = pencePerKm / (double)100;

                strPencePerKm = GetPoundsDisplayText(pencePerKm) + space + perKm;
            }

            // Create the display text
            if (roadUnitToShow == RoadUnitsEnum.Miles)
            {
                return
                    "<span class=\"milesshow\">" + strPencePerMile + "</span>"
                  + "<span class=\"kmshide\">" + strPencePerKm + "</span>";
            }
            else
            {
                return
                    "<span class=\"mileshide\">" + strPencePerMile + "</span>"
                  + "<span class=\"kmsshow\">" + strPencePerKm + "</span>";
            }
        }

        /// <summary>
        /// Method to return the text format of a distance to be converted to string.
        /// The distanceDecimalPlaces value is used to determine number decimal places
        /// </summary>
        /// <returns></returns>
        private string GetDistanceFormat()
        {
            string numberFormat = "F";

            // determine the format based on number of decimal places, 3 or less (this can be changed in future as its only set to avoid large numbers)
            if ((distanceDecimalPlaces >= 0) && (distanceDecimalPlaces <= 3))
            {
                numberFormat += distanceDecimalPlaces.ToString();
            }
            else
            {
                numberFormat += "1";
            }

            return numberFormat;
        }

        #endregion

        #endregion

        #region Public methods

        /// <summary>
        /// Returns the header strings for the formatted environmental benefits details
        /// </summary>
        /// <returns></returns>
        public string[] GetHeaders()
        {
            string[] headers = new string[5];

            headers[0] = roadCategory;
            headers[1] = countryEngland;
            headers[2] = countryScotland;
            headers[3] = countryWales;
            headers[4] = total;

            return headers;
        }

        /// <summary>
        /// Returns the footer strings for the formatted environmental benefits details
        /// </summary>
        /// <returns></returns>
        public string[] GetFooters()
        {
            // Use the existing process method with a EBCRoadCategory.None, this will return the totals
            string[] footers = ProcessEnvironmentalBenefitsForRoadCategory(EBCRoadCategory.None);

            // Override the EBCRoadCategory display text
            footers[0] = grandTotal;

            // Ensure pence per mile is not shown
            footers[5] = space;

            return footers;
        }

        /// <summary>
        /// Returns an array of the formatted environmental benefits
        /// </summary>
        /// <returns></returns>
        public IList GetDetails()
        {
            return ProcessEnvironmentalBenefits();
        }

        #endregion
    }
}
