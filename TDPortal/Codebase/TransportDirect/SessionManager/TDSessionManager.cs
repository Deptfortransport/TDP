// ***********************************************
// NAME         : TDSessionManager.cs
// AUTHOR       : Andrew Windley
// DATE CREATED : 02/07/2003
// DESCRIPTION  : The TD session manager allows TD web pages
// to store data in a type safe manner, by use of unique Keys.  
// This can then be retrieved at a latter timeby use of the same Key. 
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TDSessionManager.cs-arc  $
//
//   Rev 1.7   Sep 06 2011 11:20:38   apatel
//Updated for Real Time Information for Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.6   Sep 01 2011 10:43:54   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.5   Feb 11 2010 08:53:16   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.4   Jan 29 2010 14:43:56   mmodi
//Updated test for journey result in HasTimeBasedJourneyResults, as errors when moving from Cycle planner to FindTrainCost
//Resolution for 5388: Cycle Planner - Server error when planning Door to door after a cycle journey
//
//   Rev 1.3   Nov 24 2009 09:31:52   mmodi
//Added Key to be used by ESRI for mapping
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.2   Sep 14 2009 13:13:00   apatel
//IsStopInformationMode property added
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.1   Oct 13 2008 16:46:38   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.0   Jun 20 2008 14:48:52   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:48:42   mturner
//Initial revision.
//
//   Rev 1.106   Dec 07 2006 13:04:30   mturner
//Manual merge of stream4240
//
//   Rev 1.105   Nov 20 2006 11:22:28   PScott
//Vantive 4489192 - fix a problem with the IsFromNearestCarPark  property
//
//   Rev 1.104   Nov 14 2006 09:49:36   rbroddle
//Merge for stream4220
//
//   Rev 1.103.1.0   Nov 07 2006 11:23:44   tmollart
//Updated for Rail Search By Price.
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.103   Oct 06 2006 13:41:12   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.102.1.1   Oct 03 2006 10:25:22   mmodi
//Added IsFromNearestCarParks property
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4208: Car Parking: New Search a car park journey goes to Car route input
//
//   Rev 1.102.1.0   Aug 14 2006 11:02:18   esevern
//added FindCarParkPageState property
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.102   May 03 2006 11:37:28   RPhilpott
//Move PricingRetailOptionsState to partition-specific deferred storage.
//Resolution for 4005: DD075: Discount card entered in Find Cheaper retained if switch back to search by time
//Resolution for 4040: DD075: City-to-city shows return fares if change mode and causes an error
//
//   Rev 1.101   Apr 05 2006 15:17:44   mdambrine
//Manual merge from stream0030
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.100   Mar 30 2006 10:38:46   halkatib
//if the request is from the landing page then we need to force a reset since we need a new session every time it is invoked. 
//
//   Rev 1.99   Mar 14 2006 11:25:28   tmollart
//Manual merge of stream 3353.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.98   Feb 22 2006 09:58:48   mtillett
//Tidy up code following IR fix
//Resolution for 3572: Del 8.1 - H2 Merge - Problems with JourneySummary, Journey Details, JourneyMaps, and JourneyTickets pages.
//
//   Rev 1.97   Feb 17 2006 16:18:26   tolomolaiye
//Fix for IR 3572. Fixed error that occured when planning a Door-to-Door journey immediately after planning a Vist Planner (Day Trip Planner) journey.
//Resolution for 3572: Del 8.1 - H2 Merge - Problems with JourneySummary, Journey Details, JourneyMaps, and JourneyTickets pages.
//
//   Rev 1.96   Feb 10 2006 12:07:50   tolomolaiye
//Merge of stream 3180
//
//   Rev 1.95   Dec 13 2005 11:40:32   asinclair
//Merge for stream3143
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.94.2.4   Jan 17 2006 19:00:54   tmollart
//Updated with comments from code review. Default action is to go to HomePage for landing pages and not TabSection.None.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.94.2.3   Jan 11 2006 13:50:00   tmollart
//Removed TabSection enumeration into its own file.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.94.2.2   Dec 22 2005 09:44:56   tmollart
//Removed session manager key for OldJourneyParameters.
//Removed references to OldJourneyParameters.
//Added comments to InitialiseJourneyParameters method.
//Modified above method so that it correctly deals with journeys planned via Station/Airport mode.
//Removed redundant methods:
//- SaveCurrentFindaMode
//- SaveCurrentFindaMode(overloaded)
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.94.2.1   Dec 12 2005 17:43:58   tmollart
//Removed deferable key for OldFindAMode. Removed method InitialiseJourneyParamtersPageStates and replaced with new method called InitaliseJourneyParameters.
//Removed IsValidCombinationMethods.
//Removed references to OldFindAMode.
//Removed OldFindAMode property.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.94.2.0   Nov 29 2005 18:26:32   RGriffith
//Temporary changes to Enum for Homepage phase 2 - remove the existing enum entries once all pages have been changed to use the new HeaderControl
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.94.1.0   Nov 22 2005 16:46:44   tolomolaiye
//Added Business link state property and key
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.94   Nov 09 2005 18:18:08   RPhilpott
//Merge of stream 2818 - corrections.
//
//   Rev 1.93   Nov 09 2005 16:20:32   RPhilpott
//Merge for stream2818.
//
//   Rev 1.92   Nov 09 2005 14:20:32   RPhilpott
//Merge for stream 2818.
//
//   Rev 1.91   Oct 31 2005 14:06:30   tmollart
//Merge with stream 2638. 
//Resolution for 2929: Visit Planner Merge Activity
//
//   Rev 1.90   Sep 26 2005 09:22:06   MTurner
//Resolution for IR 2776 - Amended handling of TabSection to return TabSection.Null rahter than null if this has not already been set by another page.
//
//   Rev 1.89   Aug 04 2005 18:37:58   jgeorge
//Set IsDirty to true when setting JourneyRequest property to new object to ensure that the new value is deferred correctly.
//Resolution for 2641: Del 7: Extend Journey Crash when returning back
//
//   Rev 1.88   Jul 05 2005 13:50:32   asinclair
//Merge for stream2557
//
//   Rev 1.87.1.1   Jun 17 2005 09:53:16   jgeorge
//Added code to populate OriginAppDomainFriendlyName property of CJPSessionInfo
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.87.1.0   Jun 15 2005 12:22:12   asinclair
//Added GetSessionInformation()
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.87   May 17 2005 14:22:54   PNorell
//Updated for code-review
//Resolution for 1954: Dev Code Review: PT - Session Partitioning
//
//   Rev 1.86   Apr 23 2005 15:56:32   COwczarek
//Add constants for new deferred array keys
//Resolution for 2290: Session data for cost based searching - coach
//
//   Rev 1.85   Apr 22 2005 14:30:42   tmollart
//Changed IsValidCombination method so that if journey parameters have not been defined yet it will return FALSE meaning that new parameters have to be created. Changed method calls to match new signature.
//Changed journey parameters property so that it does not automatically create paramters if they currently do not exist.
//Resolution for 2251: Door-to-door Logged In With Do Not Use Motorways Selected Cannot Unselect
//
//   Rev 1.84   Apr 20 2005 11:24:12   tmollart
//Modified IsValidCombination method to fix bug which meant combinations of parameters where not assessed correctly.
//Added method CostBasedFindAMode
//Resolution for 2147: PT - Cost based city to city planner automatically jumps to previously planned find a fare results when selected.
//
//   Rev 1.83   Apr 16 2005 11:21:54   jgeorge
//Removed static instance of TDSessionSerializer and added code to create new instances as required.
//Resolution for 2143: Del 7 - Code error causes intermittent SI Test hanging
//
//   Rev 1.82   Apr 15 2005 12:47:28   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.81   Apr 13 2005 14:46:30   rgeraghty
//IsDirty flag set on TDJourneyResult, JourneyPlanStateData and CostSearchWaitStateData
//Resolution for 1982: Null reference errors after accessing Extend Journey
//
//   Rev 1.80   Apr 03 2005 15:00:36   COwczarek
//Fix InitialiseJourneyParameterPageStates method so that
//forceReset parameter works for cost based results also.
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.79   Apr 01 2005 16:47:18   tmollart
//Modified InitialiseJourneyParametersPageStates so that it correctly deals with time/cost partitions.
//Added HasCostBasedFaresResults method.
//
//   Rev 1.78   Mar 29 2005 17:26:22   rhopkins
//Handle null for CostSearchWaitStateData when checking for HasCostBasedJourneyResults
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.77   Mar 24 2005 15:45:28   COwczarek
//Add TimeBasedFindAMode property. Switching partitions now 
//saves data in current partition before switching.
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.76   Mar 18 2005 08:15:50   rscott
//added code for TDOnTheMove
//
//   Rev 1.75   Mar 16 2005 14:08:06   tmollart
//Added condition around trace statement outputting current find modes etc as a FindPageState may not always be available and this was causing an error to occur.
//
//   Rev 1.74   Mar 15 2005 08:40:54   tmollart
//Implemented CostSearchWaiteControlData and CostSearchWaitStateData properties.
//
//   Rev 1.73   Mar 11 2005 11:02:44   tmollart
//Added log output to show current session manager modes etc.
//
//   Rev 1.72   Mar 10 2005 15:24:28   COwczarek
//Add ClearDeferredData method that clears the deferred data cache
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.71   Mar 09 2005 10:28:32   PNorell
//Fix for nullable deferred objects.
//
//   Rev 1.70   Mar 01 2005 18:16:46   tmollart
//Changed so when partition is changed, check is made so that if requested partition is the same as current no action is taken.
//
//   Rev 1.69   Feb 25 2005 10:32:18   tmollart
//Updated IsValidCombination method so that it can work with FindFare journey parameters.
//
//   Rev 1.68   Feb 18 2005 14:31:42   tmollart
//Fixed bug which meant that a TDJourneyParamsMulti object was not being created in the correct session partition for none cost based find a modes.
//
//   Rev 1.67   Feb 11 2005 14:58:34   tmollart
//Changed initialiseJourneyParametersPageStates method so that is switches session partitions between Cost and Time based page states. Method now creates the correct journey paremers for the find a mode.
//
//   Rev 1.66   Jan 31 2005 16:50:24   PNorell
//Changes for SessionManager to include support for getting a sessionmanager for opposing partition.
//Also updated cost based check.
//
//   Rev 1.65   Jan 27 2005 12:21:00   jmorrissey
//Removed FindCostBasedPageState after change to design
//
//   Rev 1.64   Jan 26 2005 15:53:12   PNorell
//Support for partitioning the session information.
//
//   Rev 1.63   Jan 06 2005 11:05:42   jmorrissey
//Added FindCostBasedPageState, that holds all session data pertaining to cost based searching
//
//   Rev 1.62   Nov 03 2004 12:54:44   passuied
//Changes to enable a new FindAMode TrunkStation similar to Trunk but with differences...
//
//   Rev 1.61   Oct 15 2004 12:31:16   jgeorge
//Added JourneyPlanStateData and modifications to the serialization process.
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.60   Oct 08 2004 12:22:50   jmorrissey
//Change for User Survey functionality.
//
//   Rev 1.59   Sep 27 2004 17:00:30   passuied
//Reset the PrincingRequestOption object in the session when doing a reset of JourneyParameters and Page States
//Resolution for 1638: Find a coach - Train fare is displayed in Find a coach
//
//   Rev 1.58   Sep 23 2004 13:35:58   passuied
//Overriden method SaveCurrentFindAMode to force to save the mode we require (Extended journey. Mode always none)
//Resolution for 1626: Extend Journey: inconsistency when using header tabs and coming back
//
//   Rev 1.57   Sep 23 2004 13:22:28   passuied
//Added extra case in InitialiseJourneyParameters... to allow reinstating the oldJourneyParameters when Extendi is in Progress.
//Resolution for 1626: Extend Journey: inconsistency when using header tabs and coming back
//
//   Rev 1.56   Sep 09 2004 17:21:32   RHopkins
//IR1559 Changed InitialiseJourneyParametersPageStates() so that it sets NewTabSection to FindA if the page state has become FindA.
//
//   Rev 1.55   Sep 06 2004 21:08:30   JHaydock
//Major update to travel news
//
//   Rev 1.54   Aug 19 2004 13:13:42   COwczarek
//Add NewTabSection property
//Resolution for 1318: When you submit a Find  a journey the Journey Planner header displayed
//
//   Rev 1.53   Aug 17 2004 09:00:04   COwczarek
//Rework InitialiseJourneyParametersPageStates method
//Resolution for 1306: Find a Train extends a journey incorrectly.
//
//   Rev 1.52   Aug 06 2004 14:45:02   JHaydock
//Added TabSelection methods to TDSessionManager which is updated by each individual header control's load method and used within the HelpFullJP page to display the correct header on help pages.
//
//   Rev 1.51   Aug 05 2004 14:57:44   COwczarek
//Use new FindAMode method to get current Find A mode.
//Resolution for 1202: Implement FindTrainInput and FindCoachInput pages
//
//   Rev 1.50   Aug 03 2004 11:49:24   COwczarek
//Add interface implementations for IsFindAMode and FindAMode
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.49   Jul 30 2004 12:08:38   passuied
//added extra car mode in InitialiseJourneyParametersPageStates method
//
//   Rev 1.48   Jul 28 2004 12:02:02   passuied
//Changed the initialisation order in InitialiseJourneyParametersPageState. Create instance of FindPageState before instantiation journeyParameters
//
//   Rev 1.47   Jul 22 2004 15:30:16   jgeorge
//Find a... updates
//
//   Rev 1.46   Jul 16 2004 10:09:44   COwczarek
//Fix expression in IsValidCombination
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.45   Jul 15 2004 12:03:46   rgreenwood
//IR1063 - Cache Param method returns 0 if null
//
//   Rev 1.44   Jul 14 2004 16:37:00   passuied
//Changes for del6.1. FindFlight functionality working after SessionManager changes.
//
//   Rev 1.43   Jul 14 2004 13:01:26   passuied
//Changes in SessionManager for Del6.1. Compiles
//
//   Rev 1.42   Jul 13 2004 15:43:18   rgreenwood
//Added CacheParam property for IR 1063
//
//   Rev 1.41   Jul 02 2004 14:29:18   jgeorge
//Bug fixes
//
//   Rev 1.40   Jun 28 2004 14:11:14   jgeorge
//Updated ChangeJourneyParametersType and added ResultsValidForParametersType
//
//   Rev 1.39   Jun 24 2004 14:47:46   jgeorge
//Added ChangeJourneyParametersType method
//
//   Rev 1.38   May 19 2004 16:09:56   RHopkins
//Corrected instantiation of ItineraryManager so that Serialisation works correctly.
//
//   Rev 1.37   May 13 2004 12:39:48   jgeorge
//Added FindFlightPageState
//
//   Rev 1.36   May 10 2004 17:02:40   passuied
//added FindStationPageState
//
//   Rev 1.35   May 10 2004 15:11:22   RHopkins
//Extend Journey.
//Initial version of Itinerary Manager.
//
//   Rev 1.34   Apr 28 2004 16:19:50   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.33   Apr 01 2004 19:11:58   PNorell
//Updated for keeping the location and other things where they should after going to help page and/or location information.
//
//   Rev 1.32   Mar 29 2004 17:23:20   PNorell
//Fix for IR683
//
//   Rev 1.31   Mar 26 2004 10:06:46   COwczarek
//Add AmbiguityResolution property
//Resolution for 662: Back Button from the Ambiguity Page does not unconfirm a confirmed location
//
//   Rev 1.30   Mar 16 2004 09:26:14   PNorell
//Support for outward and return map added.
//
//   Rev 1.29   Mar 10 2004 18:52:16   PNorell
//Added another map-view state to indicate return journeys.
//
//   Rev 1.28   Mar 10 2004 15:53:16   PNorell
//Updated for IR498 to be included in 5.2
//
//   Rev 1.27   Mar 01 2004 15:46:16   CHosegood
//Added JourneyMapState
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.26   Feb 12 2004 15:05:04   esevern
//DEL5.2 - seperation of login and registration
//
//   Rev 1.25   Feb 06 2004 17:29:44   acaunt
//Fixed casting error
//
//   Rev 1.24   Jan 21 2004 11:35:12   PNorell
//Updates for 5.2
//
//   Rev 1.23   Oct 21 2003 17:33:38   JMorrissey
//updated property TravelNewsData which is now of type TravelNewsState
//
//   Rev 1.22   Oct 17 2003 16:14:18   JMorrissey
//added TDSessionManager.TravelNewsData property
//
//   Rev 1.21   Oct 10 2003 10:05:12   COwczarek
//Added PricingRetailOptions property
//
//   Rev 1.20   Oct 01 2003 15:35:54   AToner
//Added support for the ClaimData object
//
//   Rev 1.19   Sep 25 2003 18:06:56   PNorell
//Ensured everything is linked up together.
//Fixed various small bugs.
//
//   Rev 1.18   Sep 18 2003 11:05:58   PNorell
//Fixed interfaces/concrete impl  to use interfaces for external methods.
//Updated SessionManager lifecycle
//Corrected spelling
//Corrected code according to the DD document
//
//   Rev 1.17   Sep 17 2003 18:24:06   passuied
//Added InputPageState class and Properties for JourneyParameters and InputPageState
//
//   Rev 1.16   Sep 17 2003 16:19:48   PNorell
//Updated to support proper deferred storage.
//
//   Rev 1.15   Sep 17 2003 11:38:30   cshillan
//First implementation of JourneyPlanRunner
//Design doc ref: DV/DD014 Data Capture - Validate and Run
//
//   Rev 1.14   Sep 12 2003 09:38:52   jcotton
//Removed Read only property string SessionID now in TDSession, ITDSession
//
//   Rev 1.13   Sep 12 2003 09:29:04   jcotton
//Read only property string SessionID
//
//   Rev 1.12   Sep 11 2003 11:34:04   jcotton
//Test for saving Deferred data to session database. Code compiles but unit testing is still in progress.  Checkin is to enable solution syncronisation.
//
//   Rev 1.11   Sep 10 2003 11:52:06   cshillan
//Change ValidationError to an array
//
//   Rev 1.10   Sep 10 2003 11:29:46   cshillan
//Included ValidationError.cs in the SessionManager project
//
//   Rev 1.9   Sep 10 2003 10:53:52   jcotton
//Start of work for Saving to Deferred DB
//
//   Rev 1.8   Sep 09 2003 13:22:38   cshillan
//Included JourneyPlanControlData and JPCallStatus
//
//   Rev 1.7   Sep 08 2003 16:25:16   cshillan
//Included property for JourneyPlanControlData
//
//   Rev 1.6   Aug 27 2003 16:42:24   kcheung
//Added CurrentAdjustState and JourneyViewState properties
//
//   Rev 1.5   Aug 27 2003 13:49:38   passuied
//initialised private variables
//
//   Rev 1.4   Aug 26 2003 10:04:02   passuied
//update
//
//   Rev 1.3   Aug 19 2003 10:53:10   PNorell
//Added support for service discovery.
//
//   Rev 1.2   Jul 17 2003 15:42:56   mturner
//Added documentation comments after code review
//
//   Rev 1.1   Jul 07 2003 17:20:28   AWindley
//Added local static variables of type TypeSafeDictionary and TDSession
//
//   Rev 1.0   Jul 03 2003 17:31:28   AWindley
//Initial Revision

using System;
using System.Collections;
using System.Web;
using System.Data;
using System.Diagnostics;
using System.Threading;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.TravelNews;
using TransportDirect.UserPortal.CostSearch;
using TransportDirect.UserPortal.CyclePlannerControl;

namespace TransportDirect.UserPortal.SessionManager 
{
	/// <summary>
	/// The TDSessionManager manages session data using three 
	/// storage areas: Form shift, Session-connected and Deferrable.
	/// </summary>
	[CLSCompliant(false)]
	public class TDSessionManager : ITDSessionManager
	{
		#region Private variables declaration
		/// <summary>
		/// The session factory used to create sessions
		/// </summary>
		private ISessionFactory sessionFactory;

		/// <summary>
		/// The type safe dictionary used for the form-shift area
		/// </summary>
		private TypeSafeDictionary TSDictionary = new TypeSafeDictionary();

		/// <summary>
		/// The session "decorator".
		/// </summary>
		private TDSession tds = new TDSession();

		/// <summary>
		/// Handles the deferable objects
		/// </summary>
		private Hashtable deferableObjects = new Hashtable();

		/// <summary>
		/// Used to keep track when it can safely be unloaded.
		/// </summary>
		private int references;
		#endregion

		#region External keys

		public static readonly PartionableDeferredKey KeyItineraryManager = new PartionableDeferredKey("ItineraryManager");
		public static readonly PartionableDeferredKey KeyJourneyRequest = new PartionableDeferredKey("JourneyRequest");
		public static readonly PartionableDeferredKey KeyJourneyMapState = new PartionableDeferredKey("JourneyMapState");
		public static readonly PartionableDeferredKey KeyReturnJourneyMapState = new PartionableDeferredKey("ReturnJourneyMapState");
		public static readonly PartionableDeferredKey KeyJourneyResult = new PartionableDeferredKey("JourneyResult");
		public static readonly PartionableDeferredKey KeyAmendedJourneyResult = new PartionableDeferredKey("AmendedJourneyResult");
		public static readonly PartionableDeferredKey KeyJourneyViewState = new PartionableDeferredKey("JourneyViewState");
		public static readonly PartionableDeferredKey KeyCurrentAdjustState = new PartionableDeferredKey("CurrentAdjustState");
		public static readonly PartionableDeferredKey KeyAsyncCallState = new PartionableDeferredKey("AsyncCallState");
		public static readonly PartionableDeferredKey KeyJourneyParameters = new PartionableDeferredKey("JourneyParameters");
		public static readonly PartionableDeferredKey KeyFindAMode = new PartionableDeferredKey("FindAMode");
		public static readonly PartionableDeferredKey KeyNearestCarParksMode = new PartionableDeferredKey("NearestCarParksMode");
		public static readonly PartionableDeferredKey KeyJourneyEmissionsPageState = new PartionableDeferredKey("JourneyEmissionsPageState");
        public static readonly PartionableDeferredKey KeyCycleRequest = new PartionableDeferredKey("CycleRequest");
        public static readonly PartionableDeferredKey KeyCycleResult = new PartionableDeferredKey("CycleResult");

		public static readonly PartionableDeferredKey KeyInputPageState = new PartionableDeferredKey("InputPageState");
		public static readonly PartionableDeferredKey KeyResultsPageState = new PartionableDeferredKey("ResultsPageState");
		public static readonly PartionableDeferredKey KeyFindStationPageState = new PartionableDeferredKey("FindStationPageState");
		public static readonly PartionableDeferredKey KeyFindCarParkPageState = new PartionableDeferredKey("FindCarParkPageState");
		public static readonly PartionableDeferredKey KeyFindPageState = new PartionableDeferredKey("FindPageState");
		public static readonly PartionableDeferredKey KeyPricingRetailOptions = new PartionableDeferredKey("PricingRetailOptions");

		public static readonly PartionableDeferredKey KeyAmbiguityResolution = new PartionableDeferredKey("AmbiguityResolution");
		public static readonly DeferredKey KeyTravelNewsState = new DeferredKey("TravelNewsState");
		public static readonly DeferredKey KeyBusinessLinksState = new DeferredKey("KeyBusinessLinksState");
		public static readonly PartionableDeferredKey KeyStoredMapViewState = new PartionableDeferredKey("StoredMapViewState");

		public static readonly PartionableDeferredArrayKey KeyCostSearchOutwardJourneys = new PartionableDeferredArrayKey("CostSearchOutwardJourneys");
		public static readonly PartionableDeferredArrayKey KeyCostSearchReturnJourneys = new PartionableDeferredArrayKey("CostSearchReturnJourneys");

		public static readonly PartionableDeferredArrayKey KeyPricingRetailOptionsArray = new PartionableDeferredArrayKey("PricingRetailOptionsArray");

        public static readonly PartionableDeferredKey KeyStopInformationMode = new PartionableDeferredKey("StopInformationMode");

        /// <summary>
        /// Specific for use by ESRI mapping only - Do not use in TDP
        /// </summary>
        public static readonly PartionableDeferredKey KeyEsriMapState = new PartionableDeferredKey("EsriMapState");

		public const string NULL_SAVED = "::null_saved::";
		public const string NULL_UNSAVED = "::null_unsaved::";
		#endregion

		#region Constructor and Public Methods
        
		/// <summary>
		/// Constructor for the TDSessionManager that is used through the ServiceDiscovery
		/// </summary>
		/// <param name="sessFactory">The session factory to be used</param>
		public TDSessionManager(ISessionFactory sessFactory)
		{
			sessionFactory = sessFactory;
		}
        
		/// <summary>
		/// Used for getting a partition that is not locked to a session.
		/// </summary>
		/// <param name="partition">The partition that wants to be inspected</param>
		private TDSessionManager(TDSessionPartition partition)
		{
			tds = new TDSessionLockedPartition( partition );
		}

		/// <summary>
		/// Gets a session value that is only around for one page-request.
		/// </summary>
		/// <param name="key">The key to set</param>
		/// <returns>The value or null if no value exists</returns>
		public string GetOneUseKey( OneUseKey key)
		{
			string s = (string)HttpContext.Current.Session[ key.ID ];
			if( s == null )
			{
				s = FormShift[ key ];
			}
			return s;
		}

		/// <summary>
		/// Sets session value that is only around for one page-request.
		/// If the value is null, then the key-value is removed.
		/// </summary>
		/// <param name="key">The key</param>
		/// <param name="val">The value to be set or null to remove</param>
		public void SetOneUseKey( OneUseKey key, string val )
		{
			if( val == null )
			{
				HttpContext.Current.Session.Remove( key.ID );
			}
			else
			{
				// Store it in the session pool if it is updated
				HttpContext.Current.Session[ key.ID ] = val;
			}
		}

		
		/// <summary>
		/// Initialises the journey parameters for the supplied mode. Checks if
		/// a reset of the parameters is needed. Contains exceptions for some 
		/// mode combinations.
		/// </summary>
		/// <param name="mode">Requested new mode.</param>
		/// <returns>If a reset of the parameters took place.</returns>
		public bool InitialiseJourneyParameters(FindAMode mode)
		{

			bool forceReset;

			if (this.GetOneUseKey(SessionKey.NewSearch) != null || JourneyParameters == null)
			{
				// If the user has requested a new search then the parameters
				// should be reset.
				forceReset = true;
			}
			else
			{
				// Test if we need to reset the parameters
				if (mode == FindAMode.Trunk && this.FindAMode == FindAMode.TrunkStation)
				{
					// If the requested mode is Trunk and the current find a mode is
					// TrunkStation then no reset of paramters is required. This will happen
					// when the user has planned a journey from find station.
					forceReset = false;
				}
				else
				{										
					// Otherwise, if the supplied mode is different from the
					// current find a mode a reset is required.
					forceReset = (mode != this.FindAMode);
				}
			}

			//need to force a reset in the case of a landing page request since we need a new session 
			//for every landing page request
			if (TDSessionManager.Current.Session[ SessionKey.LandingPageCheck ])
			{
				forceReset = true;
			}

			// If a reset is required then set the correct parameters for the
			// required mode.
			if (forceReset)
			{
				switch (mode)
				{
					case FindAMode.Flight:
						TDSessionManager.Current.Partition = TDSessionPartition.TimeBased;
						FindPageState = FindPageState.CreateInstance(mode);
						JourneyParameters = new TDJourneyParametersFlight();
						break;
					case FindAMode.Fare:
					case FindAMode.RailCost:
						TDSessionManager.Current.Partition = TDSessionPartition.CostBased;
						FindPageState = FindPageState.CreateInstance(mode);
						JourneyParameters = new CostSearchParams();
						break;
					case FindAMode.TrunkCostBased:
						TDSessionManager.Current.Partition = TDSessionPartition.CostBased;
						FindPageState = FindPageState.CreateInstance(mode);
						JourneyParameters = new CostSearchParams();
						break;
                    case FindAMode.International:
                        TDSessionManager.Current.Partition = TDSessionPartition.TimeBased;
                        FindPageState = FindPageState.CreateInstance(mode);
                        JourneyParameters = new TDJourneyParametersMulti();
                        TransportDirect.JourneyPlanning.CJPInterface.ModeType[] publicModes = new TransportDirect.JourneyPlanning.CJPInterface.ModeType[3];
                        publicModes[0] = TransportDirect.JourneyPlanning.CJPInterface.ModeType.Air;
                        publicModes[1] = TransportDirect.JourneyPlanning.CJPInterface.ModeType.Rail;
                        publicModes[2] = TransportDirect.JourneyPlanning.CJPInterface.ModeType.Coach;
                        JourneyParameters.PublicModes = publicModes;
                        ItineraryMode = ItineraryManagerMode.None;
                        break;
					default:
						TDSessionManager.Current.Partition = TDSessionPartition.TimeBased;
						FindPageState = FindPageState.CreateInstance(mode);
						JourneyParameters = new TDJourneyParametersMulti();
						ItineraryMode = ItineraryManagerMode.None;
						break;
				}
                
			}
            else
            {
                // If force reset is false Journey Parameters will not get reset
                // In this event if the journey parameters are of type multi,
                // reset the avoid toids list for outward and return journey
                if (JourneyParameters is TDJourneyParametersMulti)
                {
                    TDJourneyParametersMulti journeyParameters = JourneyParameters as TDJourneyParametersMulti;
                    journeyParameters.ResetToidListToAvoid(true);
                    journeyParameters.ResetToidListToAvoid(false);
                }
            }

			//Log output for testing purposes only.
			if (TDTraceSwitch.TraceVerbose)
			{
				string logOutput = "InitJourneyParams: " +
					"Current FindAMode: " + mode.ToString() + ", " +
					"Current partition: " + TDSessionManager.Current.Partition.ToString() + ", " +
					"Journey params (typeof): " + JourneyParameters.GetType().Name + ", ";

				//Dependant on mode Find Page state not always available so use the find journey
				//page state instead.
				if (FindPageState != null)
					logOutput += "Page state (typeof): " + FindPageState.GetType().Name;
				else
					logOutput += "Page state (typeof): FindJourneyPageState";

				Trace.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, logOutput));
			}

			// Return if a reset was completed.
			return forceReset;
		}

		/// <summary>
		/// Saves all data to the deferred storage.
		/// Note: This will only save data according to the rules (ITDSessionAware and Partition).
		/// </summary>
		public void SaveData()
		{
			TDSessionSerializer ser = new TDSessionSerializer();
			TDSessionPartition part = Partition;

			foreach( IKey key in deferableObjects.Keys )
			{				
				if( key is ITDSessionPartionable )
				{
					ser.SerializeSessionObjectAndSave( this.Session.SessionID, part , key, deferableObjects[key] );
				}
				else
				{
					ser.SerializeSessionObjectAndSave( this.Session.SessionID, key, deferableObjects[key] );
				}
			}
		}


		/// <summary>
		/// Gives a session manager which will not be attached to the regular request scope and thus not be
		/// part of the regular life-cycle.
		/// This should not be used for switching partitions, it should only be used to look at values in a given partition if needed.
		/// </summary>
		/// <param name="partition">The partition</param>
		/// <returns>An instance of a session manager for a given partition</returns>
		public ITDSessionManager GetSessionManagerForPartition( TDSessionPartition partition )
		{
			return new TDSessionManager( partition );
		}

		/// <summary>
		/// This creates and returns a new instance of CJPSessionInfo with the current session information.
		/// </summary>
		/// <returns>A new instance of CJPSessionInfo with the current session information</returns>
		public CJPSessionInfo GetSessionInformation()
		{
			CJPSessionInfo info = new CJPSessionInfo();
			info.SessionId = this.Session.SessionID;
			info.IsLoggedOn = this.Authenticated;
			info.UserType = this.Authenticated ? (int)TDSessionManager.Current.CurrentUser.UserType : (int)TDUserType.Standard;
			info.Language =  Thread.CurrentThread.CurrentUICulture.ToString();
			info.OriginAppDomainFriendlyName = AppDomain.CurrentDomain.FriendlyName;

			return info;
		}


		/// <summary>
		/// Clears the deferred data cache
		/// </summary>
		public void ClearDeferredData() 
		{
			this.deferableObjects.Clear();
		}

		#endregion 

		#region Internal Helper methods
		/// <summary>
		/// Checks if the ob is null according to session specification.
		/// </summary>
		/// <param name="ob">The object to check if it is null</param>
		/// <returns>true if the object should be treated as null</returns>
		private static bool IsNull(object ob)
		{
			if( ob == null ) 
			{
				return true;
			}
			string test = ob as string;
			return NULL_SAVED == test || NULL_UNSAVED == test;
		}


		/// <summary>
		/// Gets data from the deferred storage and caches it.
		/// Further calls to this method will fetch it from the cache rather than
		/// the database.
		/// </summary>
		/// <param name="key">The key to fetch</param>
		/// <returns>The correct object or null if it is missing</returns>
		private object GetData(IKey key)
		{
			TDSessionSerializer ser = new TDSessionSerializer();
			object val = deferableObjects[key];
			TDSessionPartition part = Partition;
			if( val == null )
			{
				if( key is ITDSessionPartionable )
				{
					val = ser.RetrieveAndDeserializeSessionObject( this.Session.SessionID, part, key ); 
				}
				else
				{
					val = ser.RetrieveAndDeserializeSessionObject( this.Session.SessionID, key ); 
				}
				if( val != null )
					deferableObjects[key] = val;
			}
			if( IsNull(val) )
			{
				return null;
			}
			return val;
		}

		/// <summary>
		/// Set correct data to be saved to deferred storage.
		/// This is set in the cache and not serialised until end of session or save-data is called.
		/// </summary>
		/// <param name="key">The key to use</param>
		/// <param name="ob">The object to store</param>
		private void SetData(IKey key, object ob)
		{
			if( ob == null )
			{
				deferableObjects[key] = NULL_UNSAVED;
			}
			else
			{
				if (ob is ITDSessionAware)
					((ITDSessionAware)ob).IsDirty = true;

				deferableObjects[key] = ob;
			}
		}

		#endregion
        
		#region Properties	

		/// <summary>
		/// Get/set property controlling which partition is currently in use.
		/// </summary>
		public TDSessionPartition Partition
		{
			get { return tds.Partition; }
			set 
			{ 
				//If the required partition is the same as the current one
				//then there is no need to switch the partitions which
				//will clear the deferable objects.
				if (tds.Partition != value)
				{
					// First save any deferred data for current partition
					SaveData();

					tds.Partition = value; 
					// Cleanup any deferred data for partitions - this forces it to refetch data
					// Both non-partitioned and partitioned data will be refetched when asked for
					deferableObjects.Clear();
					// The session data is handled automatically and should not be cleared.
				}
			}
		}
				
		/// <summary>
		/// Ready Only. Gets the current itinerary manager.
		/// </summary>
		public TDItineraryManager ItineraryManager
		{
			get
			{
				TDItineraryManager manager = GetData(KeyItineraryManager) as TDItineraryManager;
				if ( manager == null)
				{
					manager = (TDItineraryManager)(new ItineraryManagerFactory().Get(ItineraryManagerMode.None ));
					SetData( KeyItineraryManager, manager );
				}
				return manager;
			}
		}
         

		/// <summary>
		/// Read/Write property. Gets/Sets the current ItineraryManager mode. Setting
		/// the mode will create a new required itinerary manager of the correct type
		/// if ones does not exist.
		/// </summary>
		public ItineraryManagerMode ItineraryMode
		{
			get 
			{
				return this.ItineraryManager.ItineraryMode;
			}
			set
			{
				//Create (if required) a new Itinerary Manager for the suppled
				//mode. If one already extists then dont do anything. Otherwise
				//create a new one using the ItineraryManagerFactory and store
				//it in the session manager.
				if (value != ItineraryMode)
				{
					ItineraryManagerFactory factory = new ItineraryManagerFactory();

					//Store the created itinerary manager directly onto deffered storage. This
					//avoids providing a set operator on the itinerary manager property.
					SetData(KeyItineraryManager,factory.Get(value));
				}
			}
		}

		/// <summary>
		/// Get/Set JourneyRequest
		/// </summary>
		public ITDJourneyRequest JourneyRequest
		{
			get
			{
				ITDJourneyRequest req = (ITDJourneyRequest)GetData( KeyJourneyRequest );
				if( req == null )
				{
					req = new TDJourneyRequest();
					JourneyRequest = req;
				}
				return req;
			}
			set
			{
				SetData(KeyJourneyRequest,value);
			}

		}


		/// <summary>
		/// Get/Set the JourneyMapState
		/// </summary>
		public JourneyMapState JourneyMapState 
		{
			get
			{
				JourneyMapState mapState = (JourneyMapState)GetData( KeyJourneyMapState );
				if( mapState == null )
				{
					mapState = new JourneyMapState();
					JourneyMapState = mapState;
				}
				return mapState;
			}

			set
			{
				SetData(KeyJourneyMapState,value);
			}
		}

		/// <summary>
		/// Get/Set the JourneyMapState
		/// </summary>
		public JourneyMapState ReturnJourneyMapState 
		{
			get
			{
				JourneyMapState mapState = (JourneyMapState)GetData( KeyReturnJourneyMapState );
				if( mapState == null )
				{
					mapState = new JourneyMapState();
					JourneyMapState = mapState;
				}
				return mapState;
			}

			set
			{
				SetData(KeyReturnJourneyMapState,value);
			}        
		}

		/// <summary>
		/// Get/Set JourneyResult
		/// </summary>
		public ITDJourneyResult JourneyResult
		{
			get
			{
				return (ITDJourneyResult)GetData( KeyJourneyResult );
			}
			set
			{
				SetData(KeyJourneyResult,value);
			}
		}

		/// <summary>
		/// Get/Set AmendedJourneyResult
		/// </summary>
		public ITDJourneyResult AmendedJourneyResult
		{
			get
			{
				return (ITDJourneyResult)GetData( KeyAmendedJourneyResult );
			}
			set
			{ 
				SetData(KeyAmendedJourneyResult,value);
			}        
		}

		/// <summary>
		/// Get/Set JourneyViewState
		/// </summary>
		public TDJourneyViewState JourneyViewState
		{
			get
			{
				TDJourneyViewState mapState = (TDJourneyViewState)GetData( KeyJourneyViewState );
				if( mapState == null )
				{
					mapState = new TDJourneyViewState();
					JourneyViewState = mapState;
				}
				return mapState;
			}

			set
			{
				SetData(KeyJourneyViewState,value);
			}
		}

		/// <summary>
		/// Get/Set CurrentAdjustState
		/// </summary>
		public TDCurrentAdjustState CurrentAdjustState
		{
			get
			{
				return (TDCurrentAdjustState)GetData( KeyCurrentAdjustState );
			}
			set
			{ 
				SetData(KeyCurrentAdjustState,value);
			}        
		}


        /// <summary>
        /// Get/Set TDCyclePlannerRequest
        /// </summary>
        public ITDCyclePlannerRequest CycleRequest
        {
            get
            {
                ITDCyclePlannerRequest req = (ITDCyclePlannerRequest)GetData(KeyCycleRequest);
                if (req == null)
                {
                    req = new TDCyclePlannerRequest();
                    CycleRequest = req;
                }
                return req;
            }
            set
            {
                SetData(KeyCycleRequest, value);
            }

        }

        /// <summary>
        /// Get/Set TDCyclePlannerResult
        /// </summary>
        public ITDCyclePlannerResult CycleResult
        {
            get
            {
                return (ITDCyclePlannerResult)GetData(KeyCycleResult);
            }
            set
            {
                SetData(KeyCycleResult, value);
            }
        }


		/// <summary>
		/// get/set BusinessLinksState
		/// </summary>
		public BusinessLinkState BusinessLinkState
		{
			get
			{
				return (BusinessLinkState)GetData(KeyBusinessLinksState);
			}

			set
			{
				SetData(KeyBusinessLinksState, value);
			}
		}
    
		/// <summary>
		/// Get/Set AsyncCallState
		/// </summary>
		public AsyncCallState AsyncCallState
		{
			get
			{
				return (AsyncCallState)GetData( KeyAsyncCallState );
			}
			set
			{ 
				SetData(KeyAsyncCallState,value);
			}        
		}

		/// <summary>
		/// Get/Set ValidationError
		/// </summary>
		public ValidationError ValidationError
		{
			get
			{
				return (ValidationError)HttpContext.Current.Session["validationError"];
			}
			set
			{
				HttpContext.Current.Session["validationError"] = value;
			}
		}
        
		/// <summary>
		/// Read only property that gets the user's authentication status.
		/// </summary>
		/// <value>True if authenticated, false otherwise.</value>
		public bool Authenticated
		{
			get
			{
				bool returnValue = false;
				object authenticated = HttpContext.Current.Session["authenticated"];
				if ( authenticated != null && authenticated is Boolean )
				{
					returnValue = (bool) authenticated;
				}
				return (returnValue);           
			}
		}
        
		/// <summary>
		/// Read only property that gets the user's session manager instance. 
		/// This is a convienince method for getting it from the ServiceDiscovery.
		/// </summary>
		public static ITDSessionManager Current
		{
			get
			{
				return (ITDSessionManager)TDServiceDiscovery.Current[ ServiceDiscoveryKey.SessionManager ];
			}
		}

		/// <summary>
		/// Read only property that gets the Formshift data for the current session manager.
		/// </summary>
		public TypeSafeDictionary FormShift
		{
			get
			{   
				return TSDictionary;
			}
		}

		/// <summary>
		/// Read only property that gets the Session data for the current session manager.
		/// </summary>
		public ITDSession Session
		{
			get
			{   
				return tds;
			}
		}

		/// <summary>
		/// Gets/sets the journey parameters used to create a journey request
		/// </summary>
		public TDJourneyParameters JourneyParameters
		{
			get
			{
				TDJourneyParameters param = (TDJourneyParameters)GetData( KeyJourneyParameters );
				return param;
			}
			set
			{ 
				SetData(KeyJourneyParameters,value);
			}        
		}

		/// <summary>
		/// Get/sets the page-state for input pages
		/// </summary>
		public InputPageState InputPageState
		{
			get
			{
				InputPageState pageState = (InputPageState)GetData( KeyInputPageState );
				if( pageState == null )
				{
					pageState = new InputPageState();
					InputPageState = pageState;
				}
				return pageState;
			}
			set
			{
				SetData(KeyInputPageState,value);
			}

		}


		/// <summary>
		/// Get/sets the find-station page state
		/// </summary>
		public FindStationPageState FindStationPageState
		{
			get
			{
				FindStationPageState pageState = (FindStationPageState)GetData( KeyFindStationPageState );
				if( pageState == null )
				{
					pageState = new FindStationPageState();
					FindStationPageState = pageState;
				}
				return pageState;
			}
			set
			{
				SetData(KeyFindStationPageState,value);
			}
		}

		/// <summary>
		/// Get/sets the nearest car parks page state
		/// </summary>
		public FindCarParkPageState FindCarParkPageState
		{
			get
			{
				FindCarParkPageState pageState = (FindCarParkPageState)GetData( KeyFindCarParkPageState );
				if( pageState == null )
				{
					pageState = new FindCarParkPageState();
					FindCarParkPageState = pageState;
				}
				return pageState;
			}
			set
			{
				SetData(KeyFindCarParkPageState,value);
			}
		}

		/// <summary>
		/// Read-write property
		/// Used to determine the page state
		/// for the results page
		/// </summary>
		public ResultsPageState ResultsPageState
		{
			get 
			{
				ResultsPageState pageState = (ResultsPageState)GetData( KeyResultsPageState );
				if( pageState == null )
				{
					pageState = new ResultsPageState();
					ResultsPageState = pageState;
				}
				return pageState;
			}
			set
			{
				SetData(KeyResultsPageState,value);
			}
		}

		/// <summary>
		/// Get/sets the finder page state
		/// </summary>
		public FindPageState FindPageState
		{
			get
			{
				return (FindPageState)GetData( KeyFindPageState );
			}
			set
			{
				SetData(KeyFindPageState,value);
			}
		}

		/// <summary>
		/// Get/Set property to access the Pricing and Retail user options.
		/// </summary>
		public PricingRetailOptionsState PricingRetailOptions
		{
			get
			{
				return (PricingRetailOptionsState)GetData( KeyPricingRetailOptions );
			}
			set
			{
				SetData(KeyPricingRetailOptions,value);
			}
		}

		/// <summary>
		/// Holds journey parameter state that may require reinstating if ambiguity decisions
		/// are backed out
		/// </summary>
		public AmbiguityResolutionState AmbiguityResolution
		{
			get
			{
				return (AmbiguityResolutionState)GetData( KeyAmbiguityResolution );
			}
			set
			{
				SetData(KeyAmbiguityResolution,value);
			}

		}


		/// <summary>
		/// Get/Set property to access the dataset used to populate the TravelNewsPage
		/// </summary>
		public TravelNewsState TravelNewsState
		{
			get
			{
				return (TravelNewsState)GetData( KeyTravelNewsState );
			}
			set
			{
				SetData(KeyTravelNewsState,value);
			}
		}


		/// <summary>
		/// The user key for the current user
		/// </summary>
		private UserKey currentUserKey = new UserKey("currentuser");
        
		/// <summary>
		/// Gets the current user or null if not authenticated
		/// </summary>
		public TDUser CurrentUser
		{
			get
			{
				if( !Authenticated )
				{
					return null;
				}
				TDUser user = (TDUser)FormShift[ currentUserKey ];
				if( user == null )
				{
					string userID = (string) this.Session[SessionKey.Username];
					user = new TDUser();
					user.FetchUser( userID );
					FormShift[ currentUserKey ] = user;
				}
				return user;

			}
			set
			{
				FormShift[ currentUserKey ] = value;
			}           
		}

		/// <summary>
		/// Get/set property for a newly registered, unsaved user password
		/// </summary>
		public string UnsavedUsername
		{
			get
			{
				return (string) HttpContext.Current.Session["unsavedUsername"];
			}
			set
			{
				HttpContext.Current.Session["unsavedUsername"] = value;
			}
		}

		/// <summary>
		/// Get/set property for a newly registered, unsaved username
		/// </summary>
		public string UnsavedPassword
		{
			get
			{
				return (string) HttpContext.Current.Session["unsavedPassword"];
			}
			set
			{
				HttpContext.Current.Session["unsavedPassword"] = value;
			}
		}


		/// <summary>
		/// Get/set property for a newly registered, unsaved user
		/// </summary>
		public TDUser UnsavedNewUser
		{
			get
			{
				return (TDUser) HttpContext.Current.Session["unsavedNewUser"];
			}
			set
			{
				HttpContext.Current.Session["unsavedNewUser"] = value;
			}
		}

		/// <summary>
		/// Get/set property for a page Cache Parameter
		/// for browser back button
		/// </summary>
		public int CacheParam
		{
			get
			{
				if (HttpContext.Current.Session["cacheParam"] == null)
					return 0;
				else
					return (int) HttpContext.Current.Session["cacheParam"];
			}
			set
			{
				HttpContext.Current.Session["cacheParam"] = value;
			}
		}

		public const int OUTWARDMAP = 0;
		public const int RETURNMAP = 1;

		/// <summary>
		/// Used for storing the outward and return maps viewstate.
		/// </summary>
		public object[] StoredMapViewState
		{
			get
			{
				object[] val = (object[])GetData(KeyStoredMapViewState);
				if( val == null )
				{
					val = new object[2];
					StoredMapViewState = val;
				}
				return val;
			}
			set
			{
				SetData(KeyStoredMapViewState,value);
			}
		}

		/// <summary>
		/// Get the Find A mode
		/// </summary>
		public FindAMode FindAMode 
		{
			get 
			{
				FindPageState pageState = FindPageState;
				if (pageState == null) 
				{
					return FindAMode.None;
				} 
				else 
				{
					return pageState.Mode;
				}
			}

		}

		/// <summary>
		/// Returns true if the session is currently using a Find A function
		/// </summary>
		public bool IsFindAMode
		{
			get 
			{
				return (FindPageState != null && FindPageState.Mode != FindAMode.Bus);
			}
		}


		/// <summary>
		/// Returns if the session is currently using the Find Nearest Car Park function
		/// </summary>
		public bool IsFromNearestCarParks
		{
			get
			{
				object val = (object)GetData(KeyNearestCarParksMode);
				if( val == null )
				{
					SetData(KeyNearestCarParksMode,false);
					return false;
				}
				else
				{
					return (bool)GetData( KeyNearestCarParksMode );
				}
			}
			set
			{ 
				SetData(KeyNearestCarParksMode,value);
			}  
		}

        /// <summary>
        /// Returns if the session is currently set to Stop Information Mode
        /// </summary>
        public bool IsStopInformationMode
        {
            get
            {
                object val = (object)GetData(KeyStopInformationMode);
                if (val == null)
                {
                    SetData(KeyStopInformationMode, false);
                    return false;
                }
                else
                {
                    return (bool)GetData(KeyStopInformationMode);
                }
            }
            set
            {
                SetData(KeyStopInformationMode, value);
            }
        }


		/// <summary>
		/// Gets/Sets which tab is currently selected.
		/// This value is 'None' until it is actually set by the header control.
		/// If the TabSectionChangeable property is set to false then setting
		/// this property has no effect. 
		/// </summary>
		public TabSection TabSection
		{
			get
			{
				if (HttpContext.Current.Session["tabSection"]!= null)
				{
					return (TabSection)HttpContext.Current.Session["tabSection"];
				}
				else
				{
					// If no tab section is present then go to home page.
					return  TabSection.Home;
				}
			}
			set
			{
				if (TabSectionChangeable)
					HttpContext.Current.Session["tabSection"] = value;
			}
		}

		/// <summary>
		/// Sets the tab currently selected irrespective of whether the
		/// TabSectionChangeable property has been set to false.
		/// </summary>
		public TabSection NewTabSection
		{
			set
			{
				HttpContext.Current.Session["tabSection"] = value;
			}
		}

		/// <summary>
		/// Gets/Sets which tab is currently selected
		/// This value is 'None' until it is actually set by the header control
		/// </summary>
		public bool TabSectionChangeable
		{
			get
			{
				if (HttpContext.Current.Session["tabSectionChangeable"] == null)
					return true;
				return false;
			}
			set
			{
				if( value )
				{
					HttpContext.Current.Session.Remove("tabSectionChangeable");
				} 
				else 
				{
					HttpContext.Current.Session["tabSectionChangeable"] = value;
				}
			}
		}

		/// <summary>
		/// Gets/Sets whether the user survey form has been shown in this session       
		/// </summary>
		public bool UserSurveyAlreadyShown
		{
			get
			{
				if (HttpContext.Current.Session["userSurveyAlreadyShown"] == null)
					HttpContext.Current.Session["userSurveyAlreadyShown"] = false;

				return (bool)HttpContext.Current.Session["userSurveyAlreadyShown"];
			}
			set
			{
				HttpContext.Current.Session["userSurveyAlreadyShown"] = value;
			}
		}       


		/// <summary>
		/// Checks regardless of partition if there are valid time based journey results available
		/// </summary>
		/// <returns>True if there are time based journey results that are valid, false otherwise</returns>
		public bool HasTimeBasedJourneyResults
		{
			get
			{
				if (Partition == TDSessionPartition.TimeBased)
				{

					if (ItineraryManager.Length > 0) 
					{
						return true;
					} 
					else 
					{
						return JourneyResult != null && JourneyResult.IsValid;
					}

				}
				else
				{
					TDSessionSerializer ser = new TDSessionSerializer();
					TDItineraryManager itineraryManager = (TDItineraryManager)ser.RetrieveAndDeserializeSessionObject(this.Session.SessionID, TDSessionPartition.TimeBased, KeyItineraryManager);

					if (itineraryManager != null && (itineraryManager.Length > 0)) 
					{
						return true;
					} 
					else 
					{
                        Object journeyResultObject = ser.RetrieveAndDeserializeSessionObject(this.Session.SessionID, TDSessionPartition.TimeBased, KeyJourneyResult);
                        return ((journeyResultObject != null) && (journeyResultObject is TDJourneyResult) && (((TDJourneyResult)journeyResultObject).IsValid));
					}
				}

			}
		}

		/// <summary>
		/// Checks regardless of partition if there are valid cost based journey results available
		/// </summary>
		/// <returns>True if there are cost based journey results that are valid, false otherwise</returns>
		public bool HasCostBasedJourneyResults
		{
			get
			{
				if (Partition == TDSessionPartition.CostBased)
				{

					if (ItineraryManager.Length > 0) 
					{
						return true;
					} 
					else 
					{
						return JourneyResult != null && JourneyResult.IsValid;
					}

				}
				else
				{
					TDSessionSerializer ser = new TDSessionSerializer();
					Object itineraryManagerObject = ser.RetrieveAndDeserializeSessionObject(this.Session.SessionID, TDSessionPartition.CostBased, KeyItineraryManager);

					if (itineraryManagerObject != null && (itineraryManagerObject is TDItineraryManager) && (((TDItineraryManager)itineraryManagerObject).Length > 0))
					{
						return true;
					} 
					else 
					{
						Object journeyResultObject = ser.RetrieveAndDeserializeSessionObject(this.Session.SessionID, TDSessionPartition.CostBased, KeyJourneyResult);
						return ((journeyResultObject != null) && (journeyResultObject is TDJourneyResult) && (((TDJourneyResult)journeyResultObject).IsValid));
					}
				}

			}
		}

		/// <summary>
		/// Checks regardless of partition if any valid fares results exist for
		/// current cost search results.
		/// </summary>
		public bool HasCostBasedFaresResults
		{
			get
			{
				object state;

				// If we are in cost based partition get the page state.
				// Otherwise deserialise the object from the deferred storage.
				if( Partition == TDSessionPartition.CostBased)
				{
					state = this.FindPageState;
				}
				else
				{
					TDSessionSerializer ser = new TDSessionSerializer();
					state = ser.RetrieveAndDeserializeSessionObject(this.Session.SessionID, TDSessionPartition.CostBased, KeyFindPageState);
				}

				FindCostBasedPageState findCostBasedPageState = state as FindCostBasedPageState;
				//Check returned state is not null and there are travel dates on the search				
				return (findCostBasedPageState != null) && (findCostBasedPageState.SearchResult != null) &&
					(findCostBasedPageState.SearchResult.GetAllTravelDates().Length > 0);
			}
		}

		/// <summary>
		/// Read only property that returns the Find A mode used to plan the results held in the 
		/// time based session partition. If an itinerary exists, the Find A mode for the initial
		/// journey is returned.
		/// </summary>
		public FindAMode TimeBasedFindAMode 
		{
			get 
			{
				if( Partition == TDSessionPartition.TimeBased )
				{
					return ItineraryManager.BaseJourneyFindAMode;
				}
				else
				{
					TDSessionSerializer ser = new TDSessionSerializer();
					TDItineraryManager itineraryManager = (TDItineraryManager)ser.RetrieveAndDeserializeSessionObject(this.Session.SessionID, TDSessionPartition.TimeBased, KeyItineraryManager);

					if (itineraryManager != null) 
					{
						return itineraryManager.TimeBasedBaseJourneyFindAMode;
					} 
					else 
					{
						object findAMode = ser.RetrieveAndDeserializeSessionObject(this.Session.SessionID, TDSessionPartition.TimeBased , KeyFindAMode);
						if (findAMode == null)
							return FindAMode.None;
						else
							return (FindAMode)findAMode;
					}
				}
			}
		}

		/// <summary>
		/// Read only property that returns the Find A mode used to plan the results held in the 
		/// cost based session partition. If an itinerary exists, the Find A mode for the initial
		/// journey is returned.
		/// </summary>
		public FindAMode CostBasedFindAMode 
		{
			get 
			{
				if( Partition == TDSessionPartition.CostBased )
				{
					return ItineraryManager.BaseJourneyFindAMode;
				}
				else
				{
					TDSessionSerializer ser = new TDSessionSerializer();
					TDItineraryManager itineraryManager = (TDItineraryManager)ser.RetrieveAndDeserializeSessionObject(this.Session.SessionID, TDSessionPartition.CostBased, KeyItineraryManager);

					if (itineraryManager != null) 
					{
						return itineraryManager.CostBasedBaseJourneyFindAMode;
					} 
					else 
					{
						object findAMode = ser.RetrieveAndDeserializeSessionObject(this.Session.SessionID, TDSessionPartition.CostBased , KeyFindAMode);
						if (findAMode == null)
							return FindAMode.None;
						else
							return (FindAMode)findAMode;
					}
				}
			}
		}

		/// <summary>
		/// Get/sets the journey emissions page state
		/// </summary>
		public JourneyEmissionsPageState JourneyEmissionsPageState
		{
			get
			{
				JourneyEmissionsPageState pageState = (JourneyEmissionsPageState)GetData( KeyJourneyEmissionsPageState );
				if( pageState == null )
				{
					pageState = new JourneyEmissionsPageState();
					JourneyEmissionsPageState = pageState;
				}
				return pageState;
			}
			set
			{
				SetData(KeyJourneyEmissionsPageState,value);
			}
		}

		#endregion

		#region Lifecycle Events
        
		/// <summary>
		/// OnLoad event executes the first time the SessionManager 
		/// is requested via the property 'Current'
		/// </summary>
		public void OnLoad()
		{
			ArrayList al = null;
			foreach( string key in HttpContext.Current.Session.Keys)
			{
				if( key.StartsWith( OneUseKey.PREFIX ) ) 
				{
					if( al == null )
					{
						al = new ArrayList();
					}
					al.Add( key );
				}
			}
			if( al != null )
			{
				foreach( string key in al)
				{
					string val = (string)HttpContext.Current.Session[key];
					HttpContext.Current.Session.Remove( key );
					// HttpContext.Current.Session[key] = null;
					FormShift[ new OneUseKey( key, true ) ] = val;
				}
			}
			this.references++;
		}
        
		/// <summary>
		/// OnFormShift event executes when shift of a form has occurred
		/// but only after the new page's OnLoad event has executed.
		/// </summary>
		public void OnFormShift()
		{
			// Current implementation does not need to do anything here
		}

		/// <summary>
		/// OnPreUnload event executes when the page renders.
		/// </summary>
		public void OnPreUnload()
		{
			// Update user profile, if needed
			if ( CurrentUser != null )
			{
				CurrentUser.Update();
			}
			SaveData();
		}

		/// <summary>
		/// OnUnload is the last event to occur and any outside access 
		/// should be avoided at this point.
		/// </summary>
		public void OnUnload()
		{
			this.references--;
			if( references < 1 )
			{
				sessionFactory.Remove();
			}
		}
		#endregion
	}
}