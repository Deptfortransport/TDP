// *********************************************** 
// NAME             : IJourneyPlanRunner.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 29 Mar 2011
// DESCRIPTION  	: Provides interface to work as mediater between UI and the Journey planning interfacer
// ************************************************
                
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.UserPortal.SessionManager;
using TDP.UserPortal.JourneyControl;
using TDP.Common;

namespace TDP.UserPortal.JourneyPlanRunner
{
    /// <summary>
    /// Provides interface to work as mediater between UI and the Journey planning interfacer
    /// </summary>
    public interface IJourneyPlanRunner
    {
        /// <summary>
        /// Validates the journey parameters and if valid calls journey control classes to initiate journey planning
        /// </summary>
        /// <param name="journeyRequest">ITDPJourneyRequest object</param>
        /// <param name="submitRequest">Indicates if the request should be submitted to the journey planners</param>
        /// <returns>true if the journey request is valid</returns>
        bool ValidateAndRun(ITDPJourneyRequest journeyRequest, string language, bool submitRequest);

        /// <summary>
        /// Messages raised by the validation
        /// </summary>
        List<TDPMessage> Messages { get; }
    }
}
