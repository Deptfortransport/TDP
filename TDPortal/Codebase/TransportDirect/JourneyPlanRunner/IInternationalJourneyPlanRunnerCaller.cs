// ****************************************************************
// NAME         : IInternationalJourneyPlanRunnerCaller.cs
// AUTHOR       : Amit Patel
// DATE CREATED : 02 Feb 2010
// DESCRIPTION  : An interface for InternationalJourneyPlanRunnerCaller class.
// ****************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/IInternationalJourneyPlanRunnerCaller.cs-arc  $
//
//   Rev 1.0   Feb 09 2010 09:37:04   apatel
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
    public interface IInternationalJourneyPlanRunnerCaller
    {
        ///<summary>
        ///Invoke the InternationalPlannerManager for a new international journey request
        ///</summary>
        void InvokeInternationalPlannerManager(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition, string lang,
            Guid internationalPlannerRequestId, TDJourneyParameters tdJourneyParameters, TDJourneyParametersMultiConverter converter, 
            TDDateTime outwardDateTime, TDDateTime returnDateTime);


        /// <summary>
        /// Determines if there is an International Journey possible between origin and destination
        /// </summary>
        /// <param name="tdJourneyParameters"></param>
        /// <returns></returns>
        bool IsPermittedInternationalJourney(TDJourneyParameters tdJourneyParameters);
    }
}
