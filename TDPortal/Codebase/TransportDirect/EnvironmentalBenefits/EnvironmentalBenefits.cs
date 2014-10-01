// *********************************************** 
// NAME			: EnvironmentalBenefits.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 21/09/2009
// DESCRIPTION	: Class to hold a group of environmental benefit objects for a RoadJourney
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnvironmentalBenefits/EnvironmentalBenefits.cs-arc  $
//
//   Rev 1.2   Oct 28 2009 15:48:24   mmodi
//Total benefit using rounded pound value instead of accurate pence value.
//Resolution for 5332: EBC - Pounds totals are incorrectly added up
//
//   Rev 1.1   Oct 15 2009 13:23:04   mmodi
//Updated to only return distances in metres
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Oct 06 2009 13:58:44   mmodi
//Initial revision.
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;

namespace TransportDirect.UserPortal.EnvironmentalBenefits
{
    /// <summary>
    /// Class to hold a group of environmental benefit objects for a RoadJourney
    /// </summary>
    [Serializable]
    public class EnvironmentalBenefits
    {
        #region Private members

        private bool isValid = false;
        private int roadJourneyRouteNum = -1;
        private ArrayList environmentalBenefitArray = new ArrayList();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public EnvironmentalBenefits()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roadJourneyRouteNum"></param>
        /// <param name="environmentalBenefitArray"></param>
        public EnvironmentalBenefits(int roadJourneyRouteNum, EnvironmentalBenefit[] environmentalBenefitArray)
        {
            if (environmentalBenefitArray != null)
            {
                this.environmentalBenefitArray.AddRange(environmentalBenefitArray);
            }

            this.roadJourneyRouteNum = roadJourneyRouteNum;
            this.isValid = true;
        }
                
        #endregion

        #region Public properties

        /// <summary>
        /// Read/write. Flag indicating if the EnvironmentalBenefits object is valid and ok to use
        /// </summary>
        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }

        /// <summary>
        /// Read only. Returns the road journey route number this EnvironmentalBenefits applies to
        /// </summary>
        public int RoadJourneyRouteNumber
        {
            get { return roadJourneyRouteNum; }
        }

        /// <summary>
        /// Read only. Returns the array of EnvironmentalBenefit associated with this object
        /// </summary>
        public EnvironmentalBenefit[] EnvironmentalBenefitArray
        {
            get
            {
                if (environmentalBenefitArray != null)
                {
                    return (EnvironmentalBenefit[])environmentalBenefitArray.ToArray(typeof(EnvironmentalBenefit));
                }
                else
                {
                    return new EnvironmentalBenefit[0];
                }
            }
        }
        
        #endregion

        #region Public methods

        /// <summary>
        /// Returns the environmental benefit amount for the specified road category and country.
        /// Supplying EBCRoadCategory.None will provide total for specified EBCCountry.
        /// Supplying EBCCountry.None will provide total for specified EBCRoadCategory.
        /// Supplying both as None will provide a grand total
        /// </summary>
        /// <param name="ebcRoadCategory"></param>
        /// <param name="ebcCountry"></param>
        /// <returns>Environmental benefit in pounds</returns>
        public double GetTotalBenefitAmount(EBCRoadCategory ebcRoadCategory, EBCCountry ebcCountry)
        {
            double totalBenefitAmount = 0;
            double totalBenefitAmountRoadCountry = 0;
            double totalBenefitAmountRoad = 0;
            double totalBenefitAmountCountry = 0;

            if ((environmentalBenefitArray != null) && (environmentalBenefitArray.Count > 0))
            {
                // Loop through each EnvironmentalBenefit and update the total if matches the supplied values
                for (int i = 0; i < environmentalBenefitArray.Count; i++)
                {
                    EnvironmentalBenefit eb = (EnvironmentalBenefit)environmentalBenefitArray[i];

                    double benefitAmount = eb.BenefitAmount;

                    // Convert to pounds, and round to 2.d.p before adding.
                    // Ensures grand totals are correct against individual values shown on page, 
                    // and not different due to adding and then rounding
                    if (benefitAmount > 0)
                    {
                        // Convert to pounds
                        benefitAmount = benefitAmount / (double)100;

                        // Round to 2dp
                        benefitAmount = Math.Round(benefitAmount, 2);
                    }

                    // Update totals
                    totalBenefitAmount += benefitAmount;

                    if ((eb.RoadCategory == ebcRoadCategory) && (eb.Country == ebcCountry))
                    {
                        totalBenefitAmountRoadCountry += benefitAmount;
                    }

                    if (eb.RoadCategory == ebcRoadCategory)
                    {
                        totalBenefitAmountRoad += benefitAmount;
                    }

                    if (eb.Country == ebcCountry)
                    {
                        totalBenefitAmountCountry += benefitAmount;
                    }
                }
            }

            // Return the appropriate total
            if ((ebcRoadCategory == EBCRoadCategory.None) && (ebcCountry == EBCCountry.None))
            {
                return totalBenefitAmount;
            }
            else if (ebcRoadCategory == EBCRoadCategory.None)
            {
                return totalBenefitAmountCountry;
            }
            else if (ebcCountry == EBCCountry.None)
            {
                return totalBenefitAmountRoad;
            }
            else
            {
                return totalBenefitAmountRoadCountry;
            }
        }

        /// <summary>
        /// Returns the environmental benefit distance for the specified road category, and country.
        /// Supplying EBCRoadCategory.None will provide total for specified EBCCountry.
        /// Supplying EBCCountry.None will provide total for specified EBCRoadCategory.
        /// Supplying both as None will provide a grand total
        /// </summary>
        /// <param name="ebcRoadCategory"></param>
        /// <param name="ebcCountry"></param>
        /// <returns>Environmental benefit distance</returns>
        public double GetTotalBenefitDistance(EBCRoadCategory ebcRoadCategory, EBCCountry ebcCountry)
        {
            double totalDistance = 0;
            double totalDistanceRoadCountry = 0;
            double totalDistanceRoad = 0;
            double totalDistanceCountry = 0;

            if ((environmentalBenefitArray != null) && (environmentalBenefitArray.Count > 0))
            {
                // Loop through each EnvironmentalBenefit and update the total if matches the supplied values
                for (int i = 0; i < environmentalBenefitArray.Count; i++)
                {
                    EnvironmentalBenefit eb = (EnvironmentalBenefit)environmentalBenefitArray[i];

                    // Update totals
                    totalDistance += eb.DistanceMetres;

                    if ((eb.RoadCategory == ebcRoadCategory) && (eb.Country == ebcCountry))
                    {
                        totalDistanceRoadCountry += eb.DistanceMetres;
                    }

                    if (eb.RoadCategory == ebcRoadCategory)
                    {
                        totalDistanceRoad += eb.DistanceMetres;
                    }

                    if (eb.Country == ebcCountry)
                    {
                        totalDistanceCountry += eb.DistanceMetres;
                    }
                }
            }

            // Return the appropriate total
            if ((ebcRoadCategory == EBCRoadCategory.None) && (ebcCountry == EBCCountry.None))
            {
                return totalDistance;
            }
            else if (ebcRoadCategory == EBCRoadCategory.None)
            {
                return totalDistanceCountry;
            }
            else if (ebcCountry == EBCCountry.None)
            {
                return totalDistanceRoad;
            }
            else
            {
                return totalDistanceRoadCountry;
            }
        }

        #endregion

    }
}
