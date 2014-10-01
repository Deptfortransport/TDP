// ***********************************************
// NAME 		: JourneyPlanRunner.cs
// AUTHOR 		: Callum
// DATE CREATED : 17/09/2003
// DESCRIPTION 	: 
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/IJourneyPlanRunner.cs-arc  $
//
//   Rev 1.1   Sep 01 2011 10:43:32   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.0   Nov 08 2007 12:24:42   mturner
//Initial revision.
//
//   Rev 1.3   Sep 19 2003 10:32:30   PNorell
//Changed interface to return boolean for run and validate. True is returned when it validates properly.
//Added check for alternative & origin/destination naptans has to exist if it is a public journey.
//
//   Rev 1.2   Sep 18 2003 11:05:08   PNorell
//Corrected spelling
//Corrected code according to the DD document
//Fixed reference to session manager.
//Fixed possible race-condition

using System;

using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
	/// <summary>
	/// Summary description for IJourneyPlanRunner.
	/// </summary>
	public interface IJourneyPlanRunner
	{
		bool ValidateAndRun( ITDSessionManager tdSessionManager, TDJourneyParameters tdJourneyParameters, string lang );
		bool ValidateAndRun( int referenceNumber, int lastSequenceNumber, ITDJourneyRequest journeyRequest, ITDSessionManager tdSessionManager, string lang );

        /// <summary>
        /// Call to plan a journey and then modify the exisiting journey result already in the session
        /// </summary>
        /// <param name="referenceNumber"></param>
        /// <param name="lastSequenceNumber"></param>
        /// <param name="journeyRequest"></param>
        /// <returns></returns>
        bool ValidateAndRun(ITDSessionManager tdSessionManager, int referenceNumber, int lastSequenceNumber, ITDJourneyRequest journeyRequest);
	}
}
