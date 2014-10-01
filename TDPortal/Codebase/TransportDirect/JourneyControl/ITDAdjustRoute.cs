// ***********************************************
// NAME 		: ITDAdjustRoute.cs
// AUTHOR 		: Peter Norell
// DATE CREATED : 20/08/2003
// DESCRIPTION 	: Interface for objects handling route adjustment.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/ITDAdjustRoute.cs-arc  $
//
//   Rev 1.1   Feb 09 2009 15:16:30   mmodi
//Updated code to apply Routing Guide Sections logic to the adjusted Public Journey
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.0   Nov 08 2007 12:23:42   mturner
//Initial revision.
//
//   Rev 1.0   Aug 27 2003 10:50:08   PNorell
//Initial Revision
using System;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Interface for objects handling route adjustment.
	/// </summary>
	public interface ITDAdjustRoute
	{
		/// <summary>
		/// Builds a journey request and executes that request to the CJP. 
		/// Also updates the sequence number to ensure no wrong journeys are used.
		/// </summary>
		/// <param name="adjustState">The current adjustment state</param>
		/// <returns>the new request being used</returns>
		ITDJourneyRequest BuildJourneyRequest( TDCurrentAdjustState adjustState );

		/// <summary>
		/// Splices the recieved journey with the original into one adjusted journey.
		/// </summary>
        /// <param name="originalPublicJourney">The original public journey which was used for adjust</param>
		/// <param name="adjustedResult">The recieved results</param>
		/// <param name="adjustState">The state of adjustment</param>
		/// <returns>The spliced public journey</returns>
		PublicJourney BuildAmendedJourney( PublicJourney originalPublicJourney, ITDJourneyResult results, TDCurrentAdjustState adjustState);
	}
}
