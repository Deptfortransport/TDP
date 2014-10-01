// *********************************************** 
// NAME                 : PublicTransportSummary.cs 
// AUTHOR               : David Lane 
// DATE CREATED         : 28/02/2012 
// DESCRIPTION			: Data structure describing the PT journeys.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/BatchJourneyPlannerService/PublicTransportSummary.cs-arc  $
//
//   Rev 1.3   Feb 28 2012 15:52:30   DLane
//Batch updates and code commentry
//Resolution for 5787: Batch Journey Planner
//

using System;
using System.Collections.Generic;
using System.Text;

namespace BatchJourneyPlannerService
{
    public class PublicTransportSummary
    {
        private TimeSpan outwardJourneyTime = TimeSpan.MinValue;
        private string outwardChanges = string.Empty;
        private string outwardModes = string.Empty;
        private string outwardCO2 = string.Empty;
        private TimeSpan returnJourneyTime = TimeSpan.MinValue;
        private string returnChanges = string.Empty;
        private string returnModes = string.Empty;
        private string returnCO2 = string.Empty;
        private string landingUrL = string.Empty;

        #region Property accessors
        /// <summary>
        /// Property acessor
        /// </summary>
        public string LandingUrL
        {
            get { return landingUrL; }
            set { landingUrL = value; }
        }

        /// <summary>
        /// Property acessor
        /// </summary>
        public TimeSpan OutwardJourneyTime
        {
            get { return outwardJourneyTime; }
            set { outwardJourneyTime = value; }
        }

        /// <summary>
        /// Property acessor
        /// </summary>
        public string OutwardChanges
        {
            get { return outwardChanges; }
            set { outwardChanges = value; }
        }

        /// <summary>
        /// Property acessor
        /// </summary>
        public string OutwardModes
        {
            get { return outwardModes; }
            set { outwardModes = value; }
        }

        /// <summary>
        /// Property acessor
        /// </summary>
        public string OutwardCO2
        {
            get { return outwardCO2; }
            set { outwardCO2 = value; }
        }

        /// <summary>
        /// Property acessor
        /// </summary>
        public TimeSpan ReturnJourneyTime
        {
            get { return returnJourneyTime; }
            set { returnJourneyTime = value; }
        }

        /// <summary>
        /// Property acessor
        /// </summary>
        public string ReturnChanges
        {
            get { return returnChanges; }
            set { returnChanges = value; }
        }

        /// <summary>
        /// Property acessor
        /// </summary>
        public string ReturnModes
        {
            get { return returnModes; }
            set { returnModes = value; }
        }

        /// <summary>
        /// Property acessor
        /// </summary>
        public string ReturnCO2
        {
            get { return returnCO2; }
            set { returnCO2 = value; }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public PublicTransportSummary()
        {
        }

        /// <summary>
        /// The csv summary record
        /// </summary>
        /// <returns>Comma separated results</returns>
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
            record.Append(outwardChanges);
            record.Append(",");
            record.Append(outwardModes);
            record.Append(",");
            record.Append(outwardCO2);
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
            record.Append(returnChanges);
            record.Append(",");
            record.Append(returnModes);
            record.Append(",");
            record.Append(returnCO2);
            record.Append(",");
            record.Append(landingUrL);

            return record.ToString();
        }
    }
}
