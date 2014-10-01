﻿// *********************************************** 
// NAME             : IStopEventManager.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 26 Apr 2011
// DESCRIPTION  	: Interface for the StopEventManager
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.UserPortal.JourneyControl
{
    /// <summary>
    /// Interface for the StopEventManager
    /// </summary>
    public interface IStopEventManager
    {
        /// <summary>
        /// CallCJP handles the orchestration of the various calls to the Stop Event service.
        /// </summary>
        /// <param name="request">Encapsulates journey parameters</param>
        /// <param name="sessionID">Used for logging purposes only</param>
        /// <param name="referenceTransaction">True is this an SLA-monitoring transacation</param>
        /// <param name="language">Two-character ISO id ("en" or "cy") of the current UI language</param>
        /// <returns>The results of the enquiry, including any error messages</returns>
        ITDPJourneyResult CallCJP(ITDPJourneyRequest request, string sessionId, bool referenceTransaction, string language);
        
    }
}
