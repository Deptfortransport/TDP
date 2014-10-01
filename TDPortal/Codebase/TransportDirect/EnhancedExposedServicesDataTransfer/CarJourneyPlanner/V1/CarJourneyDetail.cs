// *********************************************** 
// NAME                 : CarJourneyDetail.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Class for CarJourneyDetail returned in the CarJourneyResult
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/CarJourneyDetail.cs-arc  $
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
    /// Class for CarJourneyDetail returned in the CarJourneyResult
    /// </summary>
    [System.Serializable]
    public class CarJourneyDetail
    {
        private int instructionNumber;
        private string instructionText;
        private string cumulativeDistance;
        private string arrivalTime;
        private CarCost carCost;

        /// <summary>
        /// Constructor
        /// </summary>
        public CarJourneyDetail()
        {
        }

        #region Public properties

        /// <summary>
        /// The number of the car instruction
        /// </summary>
        public int InstructionNumber
        {
            get { return instructionNumber; }
            set { instructionNumber = value; }
        }

        /// <summary>
        /// The instruction text for this car journey detail
        /// </summary>
        public string InstructionText
        {
            get { return instructionText; }
            set { instructionText = value; }
        }

        /// <summary>
        /// The cumulative distance up to and including this instruction
        /// </summary>
        public string CumulativeDistance
        {
            get { return cumulativeDistance; }
            set { cumulativeDistance = value; }
        }

        /// <summary>
        /// The arrival time for this instruction
        /// </summary>
        public string ArrivalTime
        {
            get { return arrivalTime; }
            set { arrivalTime = value; }
        }

        /// <summary>
        /// The car cost object associated with this instruction, if it exists
        /// </summary>
        public CarCost Cost
        {
            get { return carCost; }
            set { carCost = value; }
        }
        #endregion
    }
}
