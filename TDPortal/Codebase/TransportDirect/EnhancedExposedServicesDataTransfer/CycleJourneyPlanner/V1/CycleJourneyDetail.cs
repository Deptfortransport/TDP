// *********************************************** 
// NAME             : CycleJourneyDetail.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Class for CycleJourneyDetail returned in the CycleJourneyResult
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/CycleJourneyDetail.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:39:42   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// Class for CycleJourneyDetail returned in the CycleJourneyResult
    /// </summary>
    [Serializable]
    public class CycleJourneyDetail
    {
        #region Private Fields
        private int instructionNumber;
        private string instructionText;
        private string cumulativeDistance;
        private string arrivalTime;
        private CycleCost cost;
        private string geometry;
        private bool isRecommendedCycleRoute;
        private bool isCycleInfrastructure;
        private bool isPath;
        private bool isBridgeTunnel;
        private string cycleRouteName;
        private uint[] joiningSignificantLinkAttributes;
        private uint[] leavingSignificantLinkAttributes;
        private uint[] interestingLinkAttributes;
        private uint[] significantNodeAttributes;
        private uint[] sectionFeatureAttributes;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CycleJourneyDetail() { }
        #endregion

        #region Public Properties
        /// <summary>
        /// The number of the cycle instruction
        /// </summary>
        public int InstructionNumber
        {
            get { return instructionNumber; }
            set { instructionNumber = value; }
        }

        /// <summary>
        /// Language sensitive text that provides journey instructions for the leg
        /// </summary>
        public string InstructionText
        {
            get { return instructionText; }
            set { instructionText = value; }
        }

        /// <summary>
        /// The cumulative distance up to this instruction
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
        /// The cost item associated with this instruction e.g. ferry charges
        /// </summary>
        public CycleCost Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        /// <summary>
        /// The geometry(polyline) string of the OSGR coordinates this detail travels
        /// The coordinate points are separated as specified in the request
        /// </summary>
        public string Geometry
        {
            get { return geometry; }
            set { geometry = value; }
        }

        /// <summary>
        /// Indicates if the detail is on a recommended cycle route
        /// </summary>
        public bool IsRecommendedCycleRoute
        {
            get { return isRecommendedCycleRoute; }
            set { isRecommendedCycleRoute = value; }
        }

        /// <summary>
        /// Indicates if the detail is on a cycle infrastructure road
        /// </summary>
        public bool IsCycleInfrastructure
        {
            get { return isCycleInfrastructure; }
            set { isCycleInfrastructure = value; }
        }

        /// <summary>
        /// Indicates if the detail is on a path
        /// </summary>
        public bool IsPath
        {
            get { return isPath; }
            set { isPath = value; }
        }

        /// <summary>
        /// Indicates if the detail is on a bridge/tunnel
        /// </summary>
        public bool IsBridgeTunnel
        {
            get { return isBridgeTunnel; }
            set { isBridgeTunnel = value; }
        }

        /// <summary>
        /// The cycle route name this detail applies to
        /// </summary>
        public string CycleRouteName
        {
            get { return cycleRouteName; }
            set { cycleRouteName = value; }
        }

        /// <summary>
        /// Represents the significant link cycle attributes applicable joined at the start of
        /// this detail compared to the previous detail
        /// </summary>
        /// <value>Array of 4 unsigned int values</value>
        public uint[] JoiningSignificantLinkAttributes
        {
            get { return joiningSignificantLinkAttributes; }
            set { joiningSignificantLinkAttributes = value; }
        }

        /// <summary>
        /// Represents the significant link cycle attributes this detail leaves at its start
        /// compared to the previous detail
        /// </summary>
        /// <value>Array of 4 unsigned int values</value>
        public uint[] LeavingSignificantLinkAttributes
        {
            get { return leavingSignificantLinkAttributes; }
            set { leavingSignificantLinkAttributes = value; }
        }

        /// <summary>
        /// Represents the interesting link cycle attributes applicable for this detail
        /// </summary>
        /// <value>Array of 4 unsigned int values</value>
        public uint[] InterestingLinkAttributes
        {
            get { return interestingLinkAttributes; }
            set { interestingLinkAttributes = value; }
        }

        /// <summary>
        /// Represents the significant node cycle attributes applicable for this detail
        /// </summary>
        /// <value>Array of 4 unsigned int values</value>
        public uint[] SignificantNodeAttributes
        {
            get { return significantNodeAttributes; }
            set { significantNodeAttributes = value; }
        }

        /// <summary>
        /// Represents the section cycle attributes applicable for this detail,
        /// used when the detail represents a Stopover section
        /// </summary>
        /// <value>Array of 4 unsigned int values</value>
        public uint[] SectionFeatureAttributes
        {
            get { return sectionFeatureAttributes; }
            set { sectionFeatureAttributes = value; }
        }

        #endregion


    }
}
