// *********************************************** 
// NAME                 : FindCoachTrainPageState.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 29.07.04
// DESCRIPTION  : Responsible for managing session state
// data relating to Find A coach and train pages.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/FindCoachTrainPageState.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:24   mturner
//Initial revision.
//
//   Rev 1.2   Jan 31 2005 16:58:26   tmollart
//Changed reinstatejourneyparameters and savejourneyparameters methods to use TDJourneyParams instead of TDJourneyParamsMulti.
//
//   Rev 1.1   Jul 29 2004 17:20:04   passuied
//addition of FindCarPageState and changes to avoid duplication of code
//
//   Rev 1.0   Jul 29 2004 11:14:30   COwczarek
//Initial revision.
//Resolution for 1202: Implement FindTrainInput page

using System;
using TransportDirect.UserPortal.LocationService;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
    /// Responsible for managing session state
    /// data relating to Find A coach and train pages.
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public abstract class FindCoachTrainPageState : FindPageState
	{

        #region Declaration
        private LocationSearch publicViaLocationSearch;
        private TDLocation publicViaLocation;
        private LocationSelectControlType publicViaType;
		#endregion
    
        /// <summary>
        /// Constructor.
        /// </summary>
        public FindCoachTrainPageState()
		{
		}

        /// <summary>
        /// Sets the journey parameters currently associated with the session to be those
        /// stored by this object, saved by previously calling SaveJourneyParameters()
        /// </summary>
        public override void ReinstateJourneyParameters(TDJourneyParameters journeyParameters) 
        {
			TDJourneyParametersMulti journeyParams = journeyParameters as TDJourneyParametersMulti;

            journeyParams.PublicVia = publicViaLocationSearch;
            journeyParams.PublicViaLocation = publicViaLocation;
            journeyParams.PublicViaType = publicViaType;

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

            publicViaLocationSearch = journeyParams.PublicVia;
            publicViaLocation = journeyParams.PublicViaLocation;
            publicViaType = journeyParams.PublicViaType;

			base.SaveJourneyParameters(journeyParameters);
        }
	}
}
