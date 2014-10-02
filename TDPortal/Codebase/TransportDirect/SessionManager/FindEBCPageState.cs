// *********************************************** 
// NAME                 : FindEBCPageState.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 05 Jun 2008
// DESCRIPTION          : Class for the Find EBC page state.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/FindEBCPageState.cs-arc  $ 
//
//   Rev 1.1   Oct 06 2009 14:09:20   mmodi
//Added EnvironmentalBenefits object
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Sep 21 2009 15:05:00   mmodi
//Initial revision.
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.UserPortal.EnvironmentalBenefits;

using EB = TransportDirect.UserPortal.EnvironmentalBenefits;

namespace TransportDirect.UserPortal.SessionManager
{
    [CLSCompliant(false)]
    [Serializable]
    public class FindEBCPageState : FindPageState
    {
        #region Private members

        private EB.EnvironmentalBenefits environmentalBenefits;

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public FindEBCPageState()
		{
            this.findMode = FindAMode.EnvironmentalBenefits;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/write. EnvironmentalBenefits object containing the benefits for a RoadJourney.
        /// The FindEBCPageState.EnvironmentalBenefitsIsValid property should be checked before using.
        /// The EnvironmentalBenefits.RoadJourneyRouteNumber should be checked to identify which RoadJourney
        /// it is for.
        /// </summary>
        public EB.EnvironmentalBenefits EnvironmentalBenefits
        {
            get { return environmentalBenefits; }
            set { environmentalBenefits = value; }
        }

        /// <summary>
        /// Read only. Returns the validity of the EnvironmentalBenefits object in this page state.
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (environmentalBenefits != null)
                {
                    return environmentalBenefits.IsValid;
                }

                return false;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Method initialising PageState components
        /// </summary>
        public override void Initialise()
        {
            environmentalBenefits = new EB.EnvironmentalBenefits();

            base.Initialise();
        }

        /// <summary>
        /// Sets the journey parameters currently associated with the session to be those
        /// stored by this object, saved by previously calling SaveJourneyParameters()
        /// </summary>
        public override void ReinstateJourneyParameters(TDJourneyParameters journeyParameters)
        {
            base.ReinstateJourneyParameters(journeyParameters);
        }

        /// <summary>
        /// Stores (references) of the journey parameters currently associated with the
        /// session so that they may be reinstated when switching from ambiguity mode
        /// back to input mode
        /// </summary>
        public override void SaveJourneyParameters(TDJourneyParameters journeyParameters)
        {
            base.SaveJourneyParameters(journeyParameters);
        }

        #endregion
    }
}
