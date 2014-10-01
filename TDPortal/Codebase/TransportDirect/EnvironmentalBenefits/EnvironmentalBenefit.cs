// *********************************************** 
// NAME			: EnvironmentalBenefit.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 21/09/2009
// DESCRIPTION	: Class to hold an individual environmental benefit object
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnvironmentalBenefits/EnvironmentalBenefit.cs-arc  $
//
//   Rev 1.2   Oct 15 2009 13:22:34   mmodi
//Updated to only store distance in metres
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 11 2009 12:50:24   mmodi
//Updated
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Oct 06 2009 13:58:44   mmodi
//Initial revision.
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.EnvironmentalBenefits
{
    /// <summary>
    /// Class to hold an individual environmental benefit object
    /// </summary>
    [Serializable]
    public class EnvironmentalBenefit
    {
        #region Private members

        private EBCRoadCategory roadCategory;
        private EBCCountry country;
        private double distanceMetres;
        private double benefitAmount;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public EnvironmentalBenefit()
        {
            roadCategory = EBCRoadCategory.RoadOther;
            country = EBCCountry.England;
            distanceMetres = 0;
            benefitAmount = 0;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roadCategory">Road category of this object</param>
        /// <param name="country">Country value for this object</param>
        /// 
        /// <param name="benefitAmount">Benefit amount in pence for this object</param>
        public EnvironmentalBenefit(EBCRoadCategory roadCategory, EBCCountry country,
            double distanceMetres, double benefitAmount)
        {
            this.roadCategory = roadCategory;
            this.country = country;
            this.distanceMetres = distanceMetres;
            this.benefitAmount = benefitAmount;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. The road category for this environmental benefit object
        /// </summary>
        public EBCRoadCategory RoadCategory
        {
            get { return roadCategory; }
            set { roadCategory = value; }
        }

        /// <summary>
        /// Read/Write. The country for this environmental benefit object
        /// </summary>
        public EBCCountry Country
        {
            get { return country; }
            set { country = value; }
        }

        /// <summary>
        /// Read/Write. The distance in metres for this environmental benefit object
        /// </summary>
        public double DistanceMetres
        {
            get { return distanceMetres; }
            set { distanceMetres = value; }
        }

        /// <summary>
        /// Read/Write. The amoount of benefit (in pence) for this environmental benefit object
        /// </summary>
        public double BenefitAmount
        {
            get { return benefitAmount; }
            set { benefitAmount = value; }
        }

        #endregion

        #region Public method

        /// <summary>
        /// Returns the EnvironmentalBenefit as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format(
                "EBCRoadCategory[{0}]\t EBCCountry[{1}]\t Distance[{2}]\t Benefit[{3}]\t",
                roadCategory.ToString().PadRight(16),
                country.ToString().PadRight(8),
                distanceMetres.ToString().PadLeft(7),
                benefitAmount.ToString());
        }

        #endregion
    }
}
