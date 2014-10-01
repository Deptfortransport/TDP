// *********************************************** 
// NAME             : CycleCost.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Represents the details for a cost item associated with the cycle journey
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/CycleCost.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:39:38   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// Represents the details for a cost item associated with the cycle journey
    /// </summary>
    [Serializable]
    public class CycleCost
    {
        #region Private Fields
        private CycleCostType costType;
        private double cost;
        private string description;
        private string companyName;
        private string companyURL;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CycleCost() { }
        #endregion

        #region Public Properties
        /// <summary>
        /// The cycle cost type for this cost item
        /// </summary>
        public CycleCostType CostType
        {
            get { return costType; }
            set { costType = value; }
        }

        /// <summary>
        /// The cost value for the cost item, in pounds and pence
        /// </summary>
        public double Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        /// <summary>
        /// Description for this cost item
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
        /// The companry URL associated with this cost
        /// </summary>
        public string CompanyURL
        {
            get { return companyURL; }
            set { companyURL = value; }
        }
        #endregion
    }

}
