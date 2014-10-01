// *********************************************** 
// NAME			: JourneyControlConstants.cs
// AUTHOR		: Richard Philpott
// DATE CREATED	: 2003-09-10 
// DESCRIPTION	: Constant strings and property keys
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/JourneyControlConstants.cs-arc  $
//
//   Rev 1.6   Dec 05 2012 14:16:20   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.5   Sep 01 2011 10:43:18   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.4   Oct 07 2008 14:40:20   PScott
//SCR5121 USD C1182414  CCN 471
//
//Link to find nearest for inaccessible PT journey requests
//addition of error message type
//Resolution for 5121: CCN471 Link to Find Nearest for inaccessible PT journey requests
//
//   Rev 1.3   Jun 20 2008 13:25:54   pscott
//SCR 5026 USD 2720245 - errormessages and screen display incorrect when outward journey has failed but the return journey hasn't.   Converse of this also applies.
//
// 
//
//   Rev 1.2   Mar 10 2008 15:17:48   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev DevFactory Jan 31 2008 14:00:00 mmodi
//Added UseCarsInCityToCity const property string
//
//   Rev 1.0   Nov 08 2007 12:23:46   mturner
//Initial revision.
//
//   Rev 1.20   Mar 30 2006 12:17:10   build
//Automatically merged from branch for stream0018
//
//   Rev 1.19.1.0   Feb 27 2006 12:11:12   RPhilpott
//Integrated Air and fxCop changes.
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.19   Dec 21 2005 15:32:50   esevern
//IR3369: added resource identifiers for new partial return error messages
//Resolution for 3369: Update Error messages
//
//   Rev 1.18   Aug 19 2005 14:03:58   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.17.1.0   Jul 25 2005 14:03:12   RWilby
//Added constant for DRTIsRequired
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.17   Sep 21 2004 15:04:04   COwczarek
//Add constants and methods to support CJP errors related to journey extensions
//Resolution for 1263: Unhelpful user friendly error message for extend results
//
//   Rev 1.16   Sep 17 2004 15:13:00   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.15   Sep 14 2004 18:20:10   RPhilpott
//Correct bugs in CJP error handling, and include TTBO "no timetable found" in "normal" no journey conditions.
//Resolution for 1547: Why are these CJP error messages different?
//Resolution for 1557: Inappropriate error msgs when no air journeys found
//
//   Rev 1.14   Aug 25 2004 17:27:06   RPhilpott
//Add MinLoggingUserType
//Resolution for 1438: Enabling log viewing of CJP requests/results
//
//   Rev 1.13   Jul 28 2004 11:39:40   RPhilpott
//Changes to support rewrite of CJPManager for del 6.1 trunk planning.
//
//   Rev 1.12   Jul 26 2004 15:57:54   RPhilpott
//Use TOID prefix from Properties service to determine what and if to chop from the beginning of TOID's returned by the CJP. 
//Resolution for 1152: Technical errors in road requests to CJP
//
//   Rev 1.11   Jun 15 2004 20:04:44   RPhilpott
//Add strings for Flight Search Interval property names.
//
//   Rev 1.10   May 25 2004 11:30:14   RPhilpott
//Add expected road journey count (==2)
//Resolution for 915: Bodged congested journeys should no longer be created.
//
//   Rev 1.9   Mar 12 2004 09:36:20   PNorell
//Updated text for adjusted journeys.
//
//   Rev 1.8   Nov 17 2003 17:10:54   kcheung
//Added another constant to check for error message dispaly
//
//   Rev 1.7   Nov 14 2003 19:42:30   kcheung
//Added code 18 for CJP
//
//   Rev 1.6   Oct 22 2003 12:20:30   RPhilpott
//Improve CJP error handling
//
//   Rev 1.5   Sep 24 2003 17:34:28   RPhilpott
//More control over verbose logging, and fix origin/destn time bug
//
//   Rev 1.4   Sep 23 2003 16:38:36   PNorell
//Added new constants.
//
//   Rev 1.3   Sep 23 2003 15:28:28   RPhilpott
//Add some operational logging
//
//   Rev 1.2   Sep 22 2003 19:35:24   RPhilpott
//CJPMessage enhancements following receipt of new interface from Atkins
//
//   Rev 1.1   Sep 20 2003 19:24:40   RPhilpott
//Support for passing OSGR's with NaPTAN's, various other fixes
//
//   Rev 1.0   Sep 10 2003 14:20:56   RPhilpott
//Initial Revision

using System;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Constant strings and property keys used by JourneyControl component.
	/// </summary>
    public sealed class JourneyControlConstants
    {
	
		#region PropertyKeys
		
		public const string DrtIsRequired			= "JourneyControl.DrtIsRequired";
        public const string LogNoJourneyResponses	= "JourneyControl.LogNoJourneyResponses";
        public const string LogJourneyWebFailures	= "JourneyControl.LogJourneyWebFailures";
        public const string LogTTBOFailures			= "JourneyControl.LogTTBOFailures";
        public const string LogCJPFailures			= "JourneyControl.LogCJPFailures";
        public const string LogRoadEngineFailures	= "JourneyControl.LogRoadEngineFailures";
        public const string LogDisplayableMessages	= "JourneyControl.LogDisplayableMessages";

        public const string LogAllRequests			= "JourneyControl.LogAllRequests";
        public const string LogAllResponses			= "JourneyControl.LogAllResponses";

        public const string MinLoggingUserType		= "JourneyControl.MinUserTypeCJPLogging";
		
        public const string NumberOfPublicJourneys	= "JourneyControl.NumberOfPublicJourneys";
        public const string CJPTimeoutMillisecs		= "JourneyControl.CJPTimeoutMillisecs";
		
        public const string WaitPageTimeoutSeconds	= "JourneyControl.WaitPageTimeoutSeconds";
        public const string WaitPageRefreshSeconds	= "JourneyControl.WaitPageRefreshSeconds";

        public const string TrunkSearchInterval1	= "JourneyControl.TrunkSearchInterval1.{0}";
        public const string TrunkSearchInterval2	= "JourneyControl.TrunkSearchInterval2.{0}";
        public const string TrunkSearchInterval3	= "JourneyControl.TrunkSearchInterval3.{0}";
        public const string TrunkSearchInterval4	= "JourneyControl.TrunkSearchInterval4.{0}";

		public const string UseCombinedAir			= "JourneyControl.UseCombinedAirPlanning";
        public const string UseCarsInCityToCity     = "JourneyControl.UseCarsInCityToCity";

		#endregion


		#region TextStrings

        // these literals are used for "internal" logging messages
        //  so no point in putting them in resource file ...
        public const string JourneyWebText	= "JourneyWeb";
        public const string CJPText			= "CJP";
        public const string TTBOText		= "TTBO";
        public const string RoadEngineText	= "Road Engine";

		#endregion

		#region ErrorCodes

        // error code numbers returned by the CJP ...
        public const int CjpOK						= 0;
        public const int CjpNoPublicJourney			= 18;
        public const int CjpJourneysRejected		= 19;
        public const int CjpRoadEngineOK			= 100;
        public const int CjpRoadEngineMin			= 100;
        public const int CjpRoadEngineMax			= 199;
		
        public const int JourneyWebMajorNoResults	= 1;
        public const int JourneyWebMinorPast		= 1;
        public const int JourneyWebMinorFuture		= 2;
        public const int JourneyWebMajorGeneral		= 9;
        public const int JourneyWebMinorDisplayable	= 2;

        public const int TTBOMajorOK				= 0;
        public const int TTBOMinorOK				= 1;
        public const int TTBOMinorNoResults			= 1;
        public const int TTBONoTimetableServiceFound = 302;

        // error code numbers created by the CJPManager itself ...
        public const int CjpCallError = 999;

		#endregion

		#region MiscellaneousMagicNumbers

        // error code numbers returned by the CJP ...
        public const int ExpectedRoadJourneyCount = 1;
		
		#endregion

		#region ResourceIdentifiers

        public const string CJPInternalError				= "JourneyPlannerOutput.CJPInternalError";
        public const string CJPPartialReturn			    = "JourneyPlannerOutput.CJPPartialReturn";
		public const string CJPPartialReturnForExtension    = "JourneyPlannerOutput.CJPPartialReturnForExtension";
		public const string CJPPartialReturnAmend		    = "JourneyPlannerOutput.CJPPartialReturnAmend";
        public const string CJPPartialReturnAmendFindNearest = "JourneyPlannerOutput.CJPPartialReturnAmendFindNearest";
        public const string CJPPartialReturnPublic = "JourneyPlannerOutput.CJPPartialReturnPublic";
		public const string CJPPartialReturnRoad		    = "JourneyPlannerOutput.CJPPartialReturnRoad";
        public const string JourneyWebNoResults = "JourneyPlannerOutput.JourneyWebNoResults";
        public const string JourneyWebNoResultsOutward = "JourneyPlannerOutput.JourneyWebNoResultsOutward";
        public const string JourneyWebNoResultsReturn = "JourneyPlannerOutput.JourneyWebNoResultsReturn";

		public const string JourneyWebNoResultsForExtension = "JourneyPlannerOutput.JourneyWebNoResultsForExtension";
        public const string JourneyWebTooFarAhead		    = "JourneyPlannerOutput.JourneyWebTooFarAhead";
        public const string AdjustJourneyNoEarlierTime      = "JourneyPlannerOutput.AdjustJourneyNoEarlierTime";
        public const string AdjustJourneyNoLaterTime	    = "JourneyPlannerOutput.AdjustJourneyNoLaterTime";

        public const string JourneyWebNoRoadResultsForAvoidToids = "JourneyPlannerOutput.JourneyWebNoRoadResultsForAvoidToids";

        public const string AccessibleJourneyNoResults = "JourneyPlannerOutput.AccessibleJourneyNoResults";

		#endregion

        /// <summary>
        /// Class contains static readonly members only - never instantiated
        /// </summary>
        private JourneyControlConstants()
        {
        }

        /// <summary>
        /// Convenience method to return correct error text resource id depending
        /// on value of supplied parameter. 
        /// </summary>
        /// <param name="isExtension">True if journey request was for a journey extension, false otherwise</param>
        /// <returns>Either JourneyPlannerOutput.JourneyWebNoResults or JourneyPlannerOutputForExtention</returns>
        public static string GetJourneyWebNoResults(bool isExtension) 
        {
            if (isExtension) 
            {
                return (JourneyWebNoResultsForExtension);
            } 
            else 
            {
                return (JourneyWebNoResults);
            }
        }
        /// <summary>
        /// Convenience method to return correct error text resource id depending
        /// on value of supplied parameter. 
        /// </summary>
        /// <param name="isExtension">True if journey request was for a journey extension, false otherwise</param>
        /// <returns>Either JourneyPlannerOutput.JourneyWebNoResults or JourneyPlannerOutputForExtention</returns>
        public static string GetJourneyWebNoResultsReturn(bool isExtension)
        {
            if (isExtension)
            {
                return (JourneyWebNoResultsForExtension);
            }
            else
            {
                return (JourneyWebNoResultsReturn);
            }
        }

        /// <summary>
        /// Convenience method to return correct error text resource id depending
        /// on value of supplied parameter. 
        /// </summary>
        /// <param name="isExtension">True if journey request was for a journey extension, false otherwise</param>
        /// <returns>Either JourneyPlannerOutput.JourneyWebNoResults or JourneyPlannerOutputForExtention</returns>
        public static string GetJourneyWebNoResultsOutward(bool isExtension)
        {
            if (isExtension)
            {
                return (JourneyWebNoResultsForExtension);
            }
            else
            {
                return (JourneyWebNoResultsOutward);
            }
        }


        /// <summary>
        /// Convenience method to return correct error text resource id depending
        /// on value of supplied parameter. 
        /// </summary>
        /// <param name="isExtension">True if journey request was for a journey extension, false otherwise</param>
        /// <returns>Either JourneyPlannerOutput.CJPPartialReturn or JourneyPlannerOutput.CJPPartialReturnForExtention</returns>
        public static string GetCJPPartialReturn(bool isExtension) 
        {
			if (isExtension) 
            {
                return (CJPPartialReturnForExtension);
            } 
            else 
            {
                return (CJPPartialReturn);
            }
        }

		
		/// <summary>
		/// Convenience method to return correct error text resource id
		/// </summary>
		/// <returns>JourneyPlannerOutput.CJPPartialReturnAmend</returns>
		public static string GetCJPPartialReturnAmend() 
		{
			return (CJPPartialReturnAmend);
		}


        /// <summary>
        /// Convenience method to return correct error text resource id
        /// </summary>
        /// <returns>JourneyPlannerOutput.CJPPartialReturnAmendFindNearest</returns>
        public static string GetCJPPartialReturnAmendFindNearest()
        {
            return (CJPPartialReturnAmendFindNearest);
        }

		/// <summary>
		/// Convenience method to return correct error text resource id
		/// </summary>
		/// <returns>JourneyPlannerOutput.CJPPartialReturnPublic</returns>
		public static string GetCJPPartialReturnPublic() 
		{
			return (CJPPartialReturnPublic);
		}

		/// <summary>
		/// Convenience method to return correct error text resource id
		/// </summary>
		/// <returns>JourneyPlannerOutput.CJPPartialReturnRoad</returns>
		public static string GetCJPPartialReturnRoad() 
		{
			return (CJPPartialReturnRoad);
		}

        /// <summary>
		/// Convenience method to return correct error text resource id
		/// </summary>
        /// <returns>JourneyPlannerOutput.JourneyWebNoRoadResultsAvoidToids</returns>
        public static string GetJourneyWebNoRoadResultsAvoidToids() 
		{
            return (JourneyWebNoRoadResultsForAvoidToids);
		}
        

	}
}
