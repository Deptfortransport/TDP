// *********************************************** 
// NAME			: IInternationalPlannerManager.cs
// AUTHOR		: Amit Patel
// DATE CREATED	: 02 Feb 2010
// DESCRIPTION	: Definition of the IInternationalPlannerManager interface
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlannerControl/IInternationalPlannerManager.cs-arc  $
//
//   Rev 1.1   Feb 09 2010 10:46:22   apatel
//Updated IsPermitted journey method
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Feb 09 2010 09:33:48   apatel
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.InternationalPlannerControl
{
    /// <summary>
    /// Interface for the InternationalPlannerManager class
    /// </summary>
    public interface IInternationalPlannerManager
    {
        /// <summary>
        /// CallInternationalPlanner handles the orchestration of the various calls to the International planner.
        /// This overloaded version handles INITIAL requests.
        /// </summary>
        /// <param name="request">Encapsulates international planner journey parameters</param>
        /// <param name="sessionID">Used for logging purposes only</param>
        /// <param name="userType">Used to determine level of logging</param>
        /// <param name="referenceTransaction">True is this an SLA-monitoring transaction</param>
        /// <param name="loggedOn">True if the user is logged on (used for logging only)</param>
        /// <param name="language">Two-character ISO id ("en" or "cy") of the current UI language</param>
        /// <returns>The results of the enquiry, including any error messages</returns>
        ITDJourneyResult CallInternationalPlanner(ITDJourneyRequest request,
            string sessionId,
            int userType,
            bool referenceTransaction,
            bool loggedOn,
            string language);

        /// <summary>
        /// Validate if the journey permitted between origin country and destination city
        /// </summary>
        /// <param name="originCountry">Origin country code</param>
        /// <param name="destinationCountry">Destination country code</param>
        /// <returns></returns>
        bool IsPermittedInternationalJourney(string originCountry, string destinationCountry);
        
    }
}
