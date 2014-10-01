// *********************************************** 
// NAME                 : CarCost.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Class to hold the costs associated with a car journey
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/CarCost.cs-arc  $
//
//   Rev 1.0   Aug 04 2009 14:41:10   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1
{
    /// <summary>
    /// Class to hold the costs associated with a car journey
    /// </summary>
    [System.Serializable]
    public class CarCost
    {
        private CarCostType carCostType;
        private double costValue;
        private string description;
        private string companyName;
        private string companyURL;

        /// <summary>
        /// Constructor
        /// </summary>
        public CarCost()
        {
        }

        /// <summary>
        /// The car cost type for this cost item
        /// </summary>
        public CarCostType CostType
        {
            get { return carCostType; }
            set { carCostType = value; }
        }

        /// <summary>
        /// The cost value for this cost item
        /// </summary>
        public double Cost
        {
            get { return costValue; }
            set { costValue = value; }
        }

        /// <summary>
        /// The description for this cost item
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// The company name associated with this cost
        /// </summary>
        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }

        /// <summary>
        /// The company URL associated with this cost
        /// </summary>
        public string CompanyURL
        {
            get { return companyURL; }
            set { companyURL = value; }
        }
    }
}
