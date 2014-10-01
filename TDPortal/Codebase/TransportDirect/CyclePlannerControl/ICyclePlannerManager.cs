// *********************************************** 
// NAME			: ICyclePlannerManager.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10 Jun 2008
// DESCRIPTION	: Definition of the ICyclePlannerManager interface
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/ICyclePlannerManager.cs-arc  $
//
//   Rev 1.2   Sep 29 2010 11:26:14   apatel
//EES WebServices for Cycle Code changes
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.1   Sep 08 2008 15:45:48   mmodi
//Updated following change to Cycle journey maps processing
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 15:42:00   mmodi
//Initial revision.
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    /// <summary>
    /// Interface for the CyclePlannerManager class
    /// </summary>
    public interface ICyclePlannerManager
    {
        /// <summary>
        /// CallCyclePlanner handles the orchestration of the various calls to the Cycle planner.
        /// This overloaded version handles INITIAL requests.
        /// </summary>
        /// <param name="request">Encapsulates cycle journey parameters</param>
        /// <param name="sessionID">Used for logging purposes only</param>
        /// <param name="userType">Used to determine level of logging</param>
        /// <param name="referenceTransaction">True is this an SLA-monitoring transaction</param>
        /// <param name="loggedOn">True if the user is logged on (used for logging only)</param>
        /// <param name="language">Two-character ISO id ("en" or "cy") of the current UI language</param>
        /// <param name="polylinesTransformXslt">The path of the xslt file used to transform the Cycle jouney 
        /// xml in to xml passed to Mapping</param>
        /// <returns>The results of the enquiry, including any error messages</returns>
        ITDCyclePlannerResult CallCyclePlanner(ITDCyclePlannerRequest request,
            string sessionId,
            int userType,
            bool referenceTransaction,
            bool loggedOn,
            string language,
            string polylinesTransformXslt);
        

        /// <summary>
        /// CallCyclePlanner handles the orchestration of the various calls to the Cycle planner
        /// This overloaded version handles AMENDMENTS to an existing journey.
        /// </summary>
        /// <param name="request">Encapsulates journey parameters</param>
        /// <param name="sessionID">Used for logging purposes only</param>
        /// <param name="referenceTransaction">True is this an SLA-monitoring transacation</param>
        /// <param name="referenceNumber">Returned by the initial enquiry</param>
        /// <param name="lastSequenceNumber">Incremented by calling code on each amendment request</param>
        /// <param name="loggedOn">True if the user is logged on (used for logging only)</param>
        /// <param name="language">Two-character ISO id ("en" or "cy") of the current UI language</param>
        /// <param name="polylinesTransformXslt">The path of the xslt file used to transform the Cycle jouney 
        /// xml in to xml passed to Mapping</param>
        /// <returns>The results of the enquiry, including any error messages</returns>
        ITDCyclePlannerResult CallCyclePlanner(ITDCyclePlannerRequest request,
            string sessionId,
            int userType,
            bool referenceTransaction,
            int referenceNumber,
            int lastSequenceNumber,
            bool loggedOn,
            string language,
            string polylinesTransformXslt);


        /// <summary>
        /// CallCyclePlanner handles the orchestration of the various calls to the Cycle planner
        /// This overloaded version handles AMENDMENTS to an existing journey.
        /// </summary>
        /// <param name="request">Encapsulates journey parameters</param>
        /// <param name="sessionID">Used for logging purposes only</param>
        /// <param name="referenceTransaction">True is this an SLA-monitoring transacation</param>
        /// <param name="referenceNumber">Returned by the initial enquiry</param>
        /// <param name="lastSequenceNumber">Incremented by calling code on each amendment request</param>
        /// <param name="loggedOn">True if the user is logged on (used for logging only)</param>
        /// <param name="language">Two-character ISO id ("en" or "cy") of the current UI language</param>
        /// <param name="polylinesTransformXslt">The path of the xslt file used to transform the Cycle jouney 
        /// xml in to xml passed to Mapping</param>
        /// <param name="eesRequest">True if the request is from Enhanced Exposed Services</param>
        /// <returns>The results of the enquiry, including any error messages</returns>
        ITDCyclePlannerResult CallCyclePlanner(ITDCyclePlannerRequest request,
            string sessionId,
            int userType,
            bool referenceTransaction,
            bool loggedOn,
            string language,
            string polylinesTransformXslt,
            bool eesRequest);
        
    }
}
