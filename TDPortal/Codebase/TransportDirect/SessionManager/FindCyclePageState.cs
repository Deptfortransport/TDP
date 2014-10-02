// *********************************************** 
// NAME                 : FindCyclePageState.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 05 Jun 2008
// DESCRIPTION          : Class for the Find cycle page state.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/FindCyclePageState.cs-arc  $ 
//
//   Rev 1.4   Dec 01 2010 13:01:08   apatel
//Code updated to show aggregated name/number or cycle path type attribute name instead of  "unname path" and to show more details by default.
//Resolution for 5650: Cycle Planner - Path name updates
//
//   Rev 1.3   Oct 10 2008 15:47:56   mmodi
//Updated to have avoid time based check, and for cycle attributes
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Aug 22 2008 10:18:40   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Jul 28 2008 13:11:02   mmodi
//Updates
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 15:34:54   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.UserPortal.LocationService;

using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;

namespace TransportDirect.UserPortal.SessionManager
{
    [CLSCompliant(false)]
    [Serializable]
    public class FindCyclePageState : FindPageState
    {
        #region Variables
        
        // Cycle via locations
        private LocationSearch cycleViaLocationSearch;
        private TDLocation cycleViaLocation;
        private LocationSelectControlType cycleViaType;
        
        // Journey options
        private string cycleJourneyType;
        private string cycleSpeedMax;
        private string cycleSpeedUnit;
        private bool cycleAvoidSteepClimbs;
        private bool cycleAvoidUnlitRoads;
        private bool cycleAvoidWalkingYourBike;
        private bool cycleAvoidTimeBased;

        // Results
        private bool showAllDetailsOutward = true;
        private bool showAllDetailsReturn = true;
        
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public FindCyclePageState()
		{
            this.findMode = FindAMode.Cycle;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/write. Flag to indicate if user has selected to show all cycle details
        /// </summary>
        public bool ShowAllDetailsOutward
        {
            get { return showAllDetailsOutward; }
            set { showAllDetailsOutward = value; }
        }

        /// <summary>
        /// Read/write. Flag to indicate if user has selected to show all cycle details
        /// </summary>
        public bool ShowAllDetailsReturn
        {
            get { return showAllDetailsReturn; }
            set { showAllDetailsReturn = value; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Method initialising PageState components
        /// </summary>
        public override void Initialise()
        {
            base.Initialise();
        }

        /// <summary>
        /// Sets the journey parameters currently associated with the session to be those
        /// stored by this object, saved by previously calling SaveJourneyParameters()
        /// </summary>
        public override void ReinstateJourneyParameters(TDJourneyParameters journeyParameters)
        {

            TDJourneyParametersMulti journeyParams = journeyParameters as TDJourneyParametersMulti;

            journeyParams.CycleVia = cycleViaLocationSearch;
            journeyParams.CycleViaLocation = cycleViaLocation;
            journeyParams.CycleViaType = cycleViaType;

            journeyParams.CycleJourneyType = cycleJourneyType;
            journeyParams.CycleSpeedMax = cycleSpeedMax;
            journeyParams.CycleSpeedUnit = cycleSpeedUnit;
            journeyParams.CycleAvoidSteepClimbs = cycleAvoidSteepClimbs;
            journeyParams.CycleAvoidUnlitRoads = cycleAvoidUnlitRoads;
            journeyParams.CycleAvoidWalkingYourBike = cycleAvoidWalkingYourBike;
            journeyParams.CycleAvoidTimeBased = cycleAvoidTimeBased;
                        
            base.ReinstateJourneyParameters(journeyParameters);
        }

        /// <summary>
		/// Stores (references) of the journey parameters currently associated with the
		/// session so that they may be reinstated when switching from ambiguity mode
		/// back to input mode
		/// </summary>
        public override void SaveJourneyParameters(TDJourneyParameters journeyParameters)
        {
            TDJourneyParametersMulti journeyParams = journeyParameters as TDJourneyParametersMulti;

            cycleViaLocationSearch = journeyParams.CycleVia;
            cycleViaLocation = journeyParams.CycleViaLocation;
            cycleViaType = journeyParams.CycleViaType;

            cycleJourneyType = journeyParams.CycleJourneyType;
            cycleSpeedMax = journeyParams.CycleSpeedMax;
            cycleSpeedUnit = journeyParams.CycleSpeedUnit;
            cycleAvoidSteepClimbs = journeyParams.CycleAvoidSteepClimbs;
            cycleAvoidUnlitRoads = journeyParams.CycleAvoidUnlitRoads;
            cycleAvoidWalkingYourBike = journeyParams.CycleAvoidWalkingYourBike;
            cycleAvoidTimeBased = journeyParams.CycleAvoidTimeBased;

            base.SaveJourneyParameters(journeyParameters);
        }

        #endregion
    }
}
