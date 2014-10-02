﻿// *********************************************** 
// NAME                 : CarSummary.cs 
// AUTHOR               : David Lane 
// DATE CREATED         : 28/02/2012 
// DESCRIPTION			: Data structure for car result.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/BatchJourneyPlannerService/CarSummary.cs-arc  $
//
//   Rev 1.2   Feb 28 2012 15:52:26   dlane
//Batch updates and code commentry
//Resolution for 5787: Batch Journey Planner
//

using System;
using System.Collections.Generic;
using System.Text;

namespace BatchJourneyPlannerService
{
    public class CarSummary
    {
        private TimeSpan outwardJourneyTime = TimeSpan.MinValue;
        private string outwardCO2 = string.Empty;
        private string outwardDistance = string.Empty;
        private TimeSpan returnJourneyTime = TimeSpan.MinValue;
        private string returnCO2 = string.Empty;
        private string returnDistance = string.Empty;

        #region Property accessors
        /// <summary>
        /// Accessor
        /// </summary>
        public TimeSpan OutwardJourneyTime
        {
            get { return outwardJourneyTime; }
            set { outwardJourneyTime = value; }
        }

        /// <summary>
        /// Accessor
        /// </summary>
        public string OutwardCO2
        {
            get { return outwardCO2; }
            set { outwardCO2 = value; }
        }

        /// <summary>
        /// Accessor
        /// </summary>
        public string OutwardDistance
        {
            get { return outwardDistance; }
            set { outwardDistance = value; }
        }

        /// <summary>
        /// Accessor
        /// </summary>
        public TimeSpan ReturnJourneyTime
        {
            get { return returnJourneyTime; }
            set { returnJourneyTime = value; }
        }

        /// <summary>
        /// Accessor
        /// </summary>
        public string ReturnCO2
        {
            get { return returnCO2; }
            set { returnCO2 = value; }
        }

        /// <summary>
        /// Accessor
        /// </summary>
        public string ReturnDistance
        {
            get { return returnDistance; }
            set { returnDistance = value; }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public CarSummary()
        {
        }

        /// <summary>
        /// Outputs the summary results comma separated for use in csv
        /// </summary>
        /// <returns>Comma separated result</returns>
        public string ToSummaryRecord()
        {
            StringBuilder record = new StringBuilder();

            if (outwardJourneyTime != TimeSpan.MinValue)
            {
                switch (outwardJourneyTime.Hours.ToString().Length)
                {
                    case 0:
                        record.Append("00");
                        break;
                    case 1:
                        record.Append("0");
                        record.Append(outwardJourneyTime.Hours.ToString());
                        break;
                    case 2:
                        record.Append(outwardJourneyTime.Hours.ToString());
                        break;
                }
                record.Append(":");
                switch (outwardJourneyTime.Minutes.ToString().Length)
                {
                    case 0:
                        record.Append("00");
                        break;
                    case 1:
                        record.Append("0");
                        record.Append(outwardJourneyTime.Minutes.ToString());
                        break;
                    case 2:
                        record.Append(outwardJourneyTime.Minutes.ToString());
                        break;
                }
            }
 
            record.Append(",");
            record.Append(outwardCO2);
            record.Append(",");
            record.Append(outwardDistance);
            record.Append(",");

            if (returnJourneyTime != TimeSpan.MinValue)
            {
                switch (returnJourneyTime.Hours.ToString().Length)
                {
                    case 0:
                        record.Append("00");
                        break;
                    case 1:
                        record.Append("0");
                        record.Append(returnJourneyTime.Hours.ToString());
                        break;
                    case 2:
                        record.Append(returnJourneyTime.Hours.ToString());
                        break;
                }
                record.Append(":");
                switch (returnJourneyTime.Minutes.ToString().Length)
                {
                    case 0:
                        record.Append("00");
                        break;
                    case 1:
                        record.Append("0");
                        record.Append(returnJourneyTime.Minutes.ToString());
                        break;
                    case 2:
                        record.Append(returnJourneyTime.Minutes.ToString());
                        break;
                }
            }

            record.Append(",");
            record.Append(returnCO2);
            record.Append(",");
            record.Append(returnDistance);
            record.Append(",");
            
            return record.ToString();
        }
    }
}