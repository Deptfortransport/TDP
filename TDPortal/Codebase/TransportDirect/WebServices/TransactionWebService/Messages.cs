// *********************************************** 
// NAME                 : Messages.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 4/11/2003 
// DESCRIPTION			: Container for messages
// used by classes TD Transaction Service.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/TransactionWebServices/Messages.cs-arc  $
//
//   Rev 1.2   Mar 16 2009 12:24:08   build
//Automatically merged from branch for stream5215
//
//   Rev 1.1.1.1   Jan 20 2009 12:16:16   mturner
//Added new message for CTC journeys
//
//   Rev 1.1.1.0   Jan 14 2009 18:00:40   mturner
//Updates for tech refresh
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.1   Oct 14 2008 11:18:34   build
//Merge for stream 5014
//
//   Rev 1.0.1.0   Aug 04 2008 16:56:14   mturner
//Added support for Cycle Injections
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 13:55:26   mturner
//Initial revision.
//
//   Rev 1.9   Apr 09 2005 15:03:10   schand
//Added RTTI Messages
//
//   Rev 1.8   Jun 10 2004 17:09:34   passuied
//messages for new WebMethods
//
//   Rev 1.7   Nov 13 2003 17:16:42   geaton
//Improved exception message.
//
//   Rev 1.6   Nov 13 2003 16:52:20   geaton
//Added message to record start of initialisation.
//
//   Rev 1.5   Nov 12 2003 16:10:38   geaton
//Added messages to support pricing and station info transactions.
//
//   Rev 1.4   Nov 11 2003 15:48:44   geaton
//Rephrased messages.
//
//   Rev 1.3   Nov 10 2003 12:09:24   geaton
//Added message.
//
//   Rev 1.2   Nov 06 2003 17:08:36   geaton
//Added message for remoting config error.
//
//   Rev 1.1   Nov 06 2003 16:28:30   geaton
//Restructured messages.
//
//   Rev 1.0   Nov 05 2003 09:56:20   geaton
//Initial Revision

using System;

namespace TransportDirect.ReportDataProvider.TransactionWebService
{
	public class Messages
	{
		// Initialisation Messages
		public const string Init_TDTraceListenerFailed = "Failed to initialise the TD Trace Listener class: {0}";
		public const string Init_DotNETTraceListenerFailed = "Failed to initialise a default .NET trace listener. Reason:[{0}].";
		public const string Init_InitialisationStarted = "Initialisation of TD Transaction Service started.";
		public const string Init_CJPConfigFileMissing = "Failed to initialise remoting for CJP - CJP configuration file not found at the path [{0}].";
		public const string Init_Completed = "Initialisation of TD Transaction Service completed successfully.";
		public const string Init_UnknownPropertyKey = "Attempt to validate unknown property key: [{0}].";
		public const string Init_InvalidPropertyKeys = "Missing or invalid TD Transaction Service property keys found on initialisation: [{0}].";
		public const string Init_CJPConfigFailed = "Failed to configure the CJP: [{0}]";
		public const string Init_TDServiceAddFailed = "Failed to add a TD service to the cache: [{0}].";

		// Journey Request Messages
		public const string Journey_ResultData = "Journey Request Transaction result data: [SessionId={0} JourneyRefNumber={1} OutwardRoadCount={2} ReturnRoadCount={3} OutwardPublicCount={4} ReturnPublicCount={5}]";
        public const string CtCJourney_ResultData = "City to City Journey Request Transaction result data: [SessionId={0} JourneyRefNumber={1} OutwardRoadCount={2} ReturnRoadCount={3} OutwardPublicCount={4} ReturnPublicCount={5} RailFound={6} CoachFound={7}]";
		public const string Journey_Failed = "Journey Request Transaction failed: [{0}]";	

		// EES Messages
		public const string EESRTTI_Failed = "EES RTTI Transaction failed: [{0}]";
		public const string EESRTTI_ResultData = "EES RTTI result data: [DBSResult count={0}] and Stop is {1}";
        public const string EESFindNearest_Failed = "EES Find Nearest Transaction failed: [{0}]";
        public const string EESJourneyPlan_Failed = "EES Journey Plan Transaction failed: [{0}]";

        // Cycle Planner Messages
        public const string CycleJourney_ResultData = "Cycle Journey Request Transaction result data: [SessionId={0} JourneyRefNumber={1} OutwardCycleCount={2} ReturnCycleCount={3}]";
        public const string CycleJourney_Failed = "Cycle Journey Request Transaction failed: [{0}]";

        // Map Messages
        public const string Map_Failed = "Map Transaction failed: [{0}]";

        // Map Messages
        public const string Gaz_Failed = "Gazetteer Transaction failed: [{0}]";

        // Travel News Messages
        public const string TravelNews_Failed = "Travel News Transaction failed: [{0}]";
	}
}
