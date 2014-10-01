// *********************************************** 
// NAME         : TDJourneyResult.cs
// AUTHOR       : Andrew Toner
// DATE CREATED : 10/08/2003 
// DESCRIPTION  : Implementation of the TDJourneyResult class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/TDJourneyResult.cs-arc  $
//
//   Rev 1.20   Dec 05 2012 14:14:00   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.19   Sep 06 2012 11:23:18   DLane
//Cycle walk links - reducing GIS calls
//Resolution for 5827: CCN667 Cycle Walk links
//
//   Rev 1.18   Sep 21 2011 09:53:20   mmodi
//Corrected to show last car journey planned when no more routes using the replan to avoid incidents
//Resolution for 5739: Real Time In Car - Failed journey Replan does not display last journey
//
//   Rev 1.17   Sep 15 2011 12:11:30   apatel
//Updated to resolve the issue with maps are not updating for real time information in car
//Resolution for 5737: CCN 0548 - Real Time Information in Car issues
//
//   Rev 1.16   Sep 14 2011 14:30:36   apatel
//Updated to resolve the issues with Page broken when planning the return journey and replanning the outward journey
//Resolution for 5737: CCN 0548 - Real Time Information in Car issues
//
//   Rev 1.15   Sep 06 2011 11:20:30   apatel
//Updated for Real Time Information for Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.14   Sep 01 2011 10:43:24   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.13   Feb 25 2010 14:58:42   mmodi
//Added flag to ignore Transfer legs when generating orign and destination names on SummaryLines for the journeys
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.12   Feb 24 2010 17:32:56   mmodi
//Pass in the duration in to the journey summary line
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.11   Feb 23 2010 13:31:04   mmodi
//Updated to set an overall ModeType for a JourneyOverviewLine
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.10   Feb 19 2010 12:08:24   mmodi
//New AddRoadJourney method to allow international planner to add a road journey
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.9   Feb 17 2010 15:32:24   mmodi
//Updated to retain journey duration
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.8   Feb 17 2010 11:52:42   mmodi
//Updated fastest journey check
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.7   Feb 12 2010 11:13:26   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.6   Oct 12 2009 09:10:58   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.6   Oct 12 2009 08:42:52   apatel
//EBC Map and Printer Friendly pages related chages
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.6   Oct 12 2009 08:39:44   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.5   Feb 02 2009 16:42:16   mmodi
//Null check for request location, to prevent Pricing retail nunit from erroring
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.4   Jun 18 2008 16:04:54   dgath
//GetJourneyOverview method updated to fix the ITP issues
//Resolution for 5025: ITP: Workstream
//
//   Rev 1.3   Mar 31 2008 12:07:14   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory  Mar 24 2007 18:00:00   mmodi
//Check searchtype as well when copy CarParking location to journey result
//
//   Rev 1.0   Nov 08 2007 12:24:00   mturner
//Initial revision.
//
//   Rev DevFactory Jan 20 2008 19:00:00    dgath
//CCN0382b City to City enhancements:
//Updated to return an array of JourneyOverviewLine containing 
//details to be displayed on new JourneyOverview page
//Updated OutwardJourneySummary to accept a modeType, with GetJourneySummary filtering journeys
//based on this modeType array
//
//   Rev 1.88   Oct 16 2007 13:46:28   mmodi
//Added fare request ID increment property
//Resolution for 4448: Logging: Request ID is not included when Coach fares request made
//
//   Rev 1.87   Jun 01 2007 16:50:48   rbroddle
//Minor amendment to fix for IR4435 previously checked in.
//Resolution for 4435: Inappropriate "too far in future" messages displayed for CJP / Journeyweb errors.
//
//   Rev 1.86   May 30 2007 17:03:08   rbroddle
//Amended AddMessages function to add appropriate error messages.
//Resolution for 4435: Inappropriate "too far in future" messages displayed for CJP / Journeyweb errors.
//
//   Rev 1.85   Apr 20 2007 15:27:00   tmollart
//Modified code so that any journey web error message and any messages that fall outside of the current message translation are given a user friendly error message.
//Resolution for 4391: CJP Portal Error Message Changes
//
//   Rev 1.84   Jan 04 2007 13:54:42   mmodi
//Added methods to update the road journey
//Resolution for 4308: CO2: Find detailed journey costs should replan journey
//
//   Rev 1.83   Oct 06 2006 10:44:34   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.82.1.1   Sep 21 2006 12:34:58   esevern
//corrected setting of request origin/destination when its a return journey
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4163: Car Parking: Car park is not shown as a link in Details view
//
//   Rev 1.82.1.0   Sep 20 2006 16:52:08   esevern
//Adds the CarPark to the leg TDLocation if car park is leg start/end location
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4163: Car Parking: Car park is not shown as a link in Details view
//
//   Rev 1.82   Apr 20 2006 17:25:40   RPhilpott
//Get ride of warnings caused by unused private variables.
//
//   Rev 1.81   Apr 06 2006 17:14:00   mdambrine
//The merge of plan adjust also reapplied a bug that was fixed in version 1.74 to 1.75. This has been fixed now
//Resolution for 3819: Del 8.1 Send to a friend: Car details provided in e-mail when train option is selected
//
//   Rev 1.80   Apr 04 2006 15:53:42   RGriffith
//IR3701 Fix: Moved changes to Web/Adapters/FormattedJourneySummaryLine
//
//   Rev 1.79   Apr 03 2006 16:41:00   RGriffith
//IR3701 Fix: Return Car start/end locations were incorrectly populated
//
//   Rev 1.78   Mar 30 2006 13:46:32   mguney
//Manual merge for stream0018
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.77   Mar 14 2006 15:17:34   NMoorhouse
//fix stream3353 merge issue
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.76   Mar 14 2006 11:19:02   tmollart
//Manual merge of stream 3353.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.75   Jan 10 2006 15:25:12   mguney
//AddResult method changed to set the correct journey index values for the road journeys.
//Resolution for 3433: DEL 8.0 BBP: Car is incorrectly numbered in the details view
//
//   Rev 1.74   Dec 06 2005 18:35:20   pcross
//Added new methods:
//
//GetSelectedOutwardJourneyIndex
//GetSelectedReturnJourneyIndex
//
//which get the index of the default journey where the index tracks chronological journeys from the default journey selection from the index that tracks journeys in order added to result set
//Resolution for 3263: Visit Planner: Selecting Earlier or Later leaves the Journey selected different to the Details displayed
//
//   Rev 1.73   Nov 10 2005 10:07:10   jbroome
//Exposed OutwardJourneyIndex as public read/write property
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.72   Nov 01 2005 15:12:56   build
//Automatically merged from branch for stream2638
//
//   Rev 1.71.1.1   Oct 18 2005 14:58:26   tolomolaiye
//Changed GetModes to GetAllModes
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.71.1.0   Sep 21 2005 10:50:38   asinclair
//New branch for 2638 with Del 7.1
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.71   Sep 02 2005 12:00:18   RPhilpott
//Correct overlap calculation.
//Resolution for 2751: Regression  - Unnecessary error message about leaving destination before arriving
//
//   Rev 1.70   Aug 31 2005 14:46:00   RPhilpott
//Fix origin/destn bug for return journeys in previous code change.
//Resolution for 2743: DN62:  Return map results for journeys using train stations with Zero Coordinate stations
//
//   Rev 1.69   Aug 24 2005 16:04:36   RPhilpott
//Changes to allow OSGR of first or last location to be replaced by OSGR of request origin/destination in rare case where firs tor last naptan in journey has no supplied OSGR and is not in Stops database.
//Resolution for 2662: DN062:  Map with non-consecutive nodes with no co-ordinates functioning incorrectly
//
//   Rev 1.68   Aug 19 2005 18:39:48   RPhilpott
//Do not add legless CJP journeys to the result.
//Resolution for 2655: Error Message - Cardiff to Cardiff Central
//
//   Rev 1.67   Aug 19 2005 14:04:22   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.66.1.0   Jul 27 2005 10:33:40   asinclair
//Overload AddMessageToArray method
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.66   May 17 2005 14:22:26   PNorell
//Updated according to code-review.
//Resolution for 1954: Dev Code Review: PT - Session Partitioning
//
//   Rev 1.65   May 05 2005 16:48:12   jbroome
//Made JourneyReferenceNumber a writable property
//Resolution for 2414: Coach Find A fare: Selecting next day then one fare causes out of bound exception
//
//   Rev 1.64   Apr 11 2005 17:03:54   RPhilpott
//Do not reindex public journeys after one has been deleted.
//Resolution for 2070: PT: Incorrect trains displayed for specific date/fare.
//
//   Rev 1.63   Mar 22 2005 11:12:24   jbroome
//Made OutwardPublicJourneys and ReturnPublicJourneys read-only properties.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.62   Mar 01 2005 16:55:38   rscott
//Del 7 - Cost Based Search Incremental Design Changes
//
//   Rev 1.61   Feb 23 2005 16:40:34   rscott
//DEL7 Update - TDDateTime OutwardDateTime and ReturnDateTime changed to TDDateTime[] arrays.
//
//   Rev 1.60   Jan 28 2005 18:28:24   ralavi
//Updated for car costing
//
//   Rev 1.59   Jan 27 2005 12:31:06   jmorrissey
//Added OutwardPublicJourneys and ReturnPublicJourneys properties
//
//   Rev 1.58   Jan 26 2005 15:52:38   PNorell
//Support for partitioning the session information.
//
//   Rev 1.57   Jan 19 2005 14:45:22   RScott
//DEL 7 - PublicViaLocation removed and PublicViaLocations[ ], PublicSoftViaLocations[ ], PublicNotViaLocations[] added.
//
//   Rev 1.56   Nov 26 2004 13:50:20   jbroome
//DEL6.3.1. Motorway Junctions enhancements
//
//   Rev 1.55   Sep 17 2004 15:13:02   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.54   Sep 14 2004 18:20:04   RPhilpott
//Correct bugs in CJP error handling, and include TTBO "no timetable found" in "normal" no journey conditions.
//Resolution for 1547: Why are these CJP error messages different?
//Resolution for 1557: Inappropriate error msgs when no air journeys found
//
//   Rev 1.53   Sep 02 2004 17:48:18   RPhilpott
//Correct indexing problem -- make journeyIndex instance, not local variable.
//Resolution for 1477: Find a choice of transport strange results behaviour
//
//   Rev 1.52   Aug 31 2004 10:23:06   jgeorge
//Changed to use check in/exit times in summary when available.
//
//   Rev 1.51   Jul 28 2004 15:46:58   RPhilpott
//Unit test error corrections.
//
//   Rev 1.50   Jul 28 2004 11:39:42   RPhilpott
//Changes to support rewrite of CJPManager for del 6.1 trunk planning.
//
//   Rev 1.49   Jul 22 2004 17:08:38   jgeorge
//Modified Comparer classes to ensure consistent sorting
//
//   Rev 1.48   Jun 30 2004 11:39:28   jmorrissey
//Updated CheckForJourneyStartInPast method to return true as soon as an error condition is met
//
//   Rev 1.47   Jun 29 2004 17:11:14   jmorrissey
//Added CheckForJourneyStartInPast method
//
//   Rev 1.46   Jun 15 2004 14:30:20   COwczarek
//Add new method to check for overlapping outward/return journey times. Also call new method on RoadJourney to calc start and end times of road journey.
//Resolution for 867: Add extend journey functionality to summary and details pages
//
//   Rev 1.45   Jun 10 2004 17:13:56   RHopkins
//Added methods OutwardDisplayNumber and ReturnDisplayNumber
//
//   Rev 1.44   Jun 09 2004 15:09:48   JHaydock
//Update/fix for TDJourneyResult and unit test regarding GetOperatorNames method
//
//   Rev 1.43   May 25 2004 11:31:36   RPhilpott
//Remove "bodge congestion" handling - no congested journey now implies an error.
//Resolution for 915: Bodged congested journeys should no longer be created.
//
//   Rev 1.42   May 20 2004 18:03:18   JHaydock
//Intermediary check-in for FindSummary with FindSummaryResultControl operational
//
//   Rev 1.41   May 10 2004 15:04:26   RHopkins
//Extend Journey.
//Initial version of Itinerary Manager.
//
//   Rev 1.40   Mar 19 2004 17:52:04   PNorell
//Forgot the exclamation point for the logging.
//
//   Rev 1.39   Mar 12 2004 09:36:22   PNorell
//Updated text for adjusted journeys.
//
//   Rev 1.38   Nov 21 2003 16:29:36   PNorell
//IR305 - Unadjusted journeys saved with correct congestion number.
//
//   Rev 1.37   Nov 17 2003 17:10:52   kcheung
//Added another constant to check for error message dispaly
//
//   Rev 1.36   Nov 17 2003 16:34:02   kcheung
//Fixed bug where -1 is displayed for interchange count if only walking is used.
//
//   Rev 1.35   Nov 15 2003 13:09:06   PNorell
//Updated for ensuring no error occurs if it uses stop over sections.
//
//   Rev 1.34   Nov 14 2003 19:44:38   kcheung
//Updated error messages.
//Resolution for 112: Results - Summary: Remove error message
//
//   Rev 1.33   Nov 14 2003 16:18:38   kcheung
//Removed AddMessageToArray call for generic error.
//
//   Rev 1.32   Nov 06 2003 17:02:48   kcheung
//Updated so that error messages are cleared once they have been displayed.
//
//   Rev 1.31   Oct 22 2003 12:20:28   RPhilpott
//Improve CJP error handling
//
//   Rev 1.30   Oct 20 2003 16:48:24   PNorell
//Fixed exception in with return car journeys (and bodge congestion).
//
//   Rev 1.29   Oct 20 2003 15:29:58   kcheung
//Fixed DepartAfter Comparer to get Arrival date time of last leg rather than first leg.
//
//   Rev 1.28   Oct 16 2003 17:54:14   kcheung
//Fixed so that the Car is number 1 instead of 2
//
//   Rev 1.27   Oct 15 2003 13:30:10   PNorell
//Updates to get the correct journey time to show.
//
//   Rev 1.26   Oct 14 2003 18:28:38   kcheung
//Fixed so that the correct number of interchanges is calculated.
//
//   Rev 1.25   Oct 10 2003 19:42:44   PNorell
//Fix for routenum to make it less likely to occur for the same user.
//
//   Rev 1.24   Oct 09 2003 19:57:42   RPhilpott
//Bodge to allow road journey display even when no congested journey returned.
//
//   Rev 1.23   Oct 08 2003 17:56:00   RPhilpott
//Fixes for CJP input parameters
//
//   Rev 1.22   Sep 25 2003 11:44:40   RPhilpott
//Map handoff and MI logging changes
//
//   Rev 1.21   Sep 24 2003 17:34:28   RPhilpott
//More control over verbose logging, and fix origin/destn time bug
//
//   Rev 1.20   Sep 23 2003 19:46:12   RPhilpott
//More logging
//
//   Rev 1.19   Sep 23 2003 14:06:28   RPhilpott
//Changes to TDJourneyResult (ctor and referenceNumber)
//
//   Rev 1.18   Sep 22 2003 19:35:24   RPhilpott
//CJPMessage enhancements following receipt of new interface from Atkins
//
//   Rev 1.17   Sep 18 2003 13:25:02   RPhilpott
//Add IsValid flag
//
//   Rev 1.16   Sep 11 2003 16:34:16   jcotton
//Made Class Serializable
//
//   Rev 1.15   Sep 10 2003 14:39:48   RPhilpott
//Still work in progress ... 
//
//   Rev 1.14   Sep 10 2003 11:13:56   RPhilpott
//Changes to CJPMessage handling.
//
//   Rev 1.13   Sep 09 2003 16:01:48   RPhilpott
//Add journey counts to result interface.
//
//   Rev 1.12   Sep 05 2003 15:28:56   passuied
//Deletion of TDLocation, LocationCodingType, OSGridReference and transfer to LocationService
//
//   Rev 1.11   Sep 02 2003 12:43:08   kcheung
//Updated OutwardRoadJourney and ReturnRoadJourney
//
//   Rev 1.10   Sep 01 2003 16:28:42   jcotton
//Updated: RouteNum
//
//   Rev 1.9   Aug 29 2003 11:45:28   kcheung
//Added test for null to the Summary Line methods before a sort is made.
//
//   Rev 1.8   Aug 28 2003 17:58:14   kcheung
//Updated property to return Journey - does not return array anymore - but just single journey given a journeyIndex as specified in design doc
//
//   Rev 1.7   Aug 27 2003 11:01:38   kcheung
//OutwardJourneySummary and ReturnedJourneySummary unit tested and working.
//
//   Rev 1.6   Aug 26 2003 17:36:26   kcheung
//Updated GetSummary
//
//   Rev 1.5   Aug 26 2003 17:20:58   kcheung
//Updated to include Journey Summary Line stuff.  Constructors for the journeys have also changed - now include the Journey Index.
//
//   Rev 1.4   Aug 26 2003 11:22:08   kcheung
//Updated OutwardJourney and ReturnJourney Summary header
//
//   Rev 1.3   Aug 20 2003 17:55:54   AToner
//Work in progress

using System;
using System.Text;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;

using Logger = System.Diagnostics.Trace;
using System.Collections.Generic;

namespace TransportDirect.UserPortal.JourneyControl
{
    /// <summary>
    /// Summary description for TDJourneyResult.
    /// </summary>
    [Serializable()]
    public class TDJourneyResult : ITDJourneyResult, ITDSessionAware
    {
        #region Private members

        private ArrayList messageList = new ArrayList();
        
        private int journeyReferenceNumber;
        private int lastReferenceSequence;
		private int lastFareRequestNumber;
        private ArrayList outwardRoadJourney    = new ArrayList(2);
        private ArrayList returnRoadJourney     = new ArrayList(2);
        private ArrayList outwardPublicJourney  = new ArrayList(5);
        private ArrayList returnPublicJourney   = new ArrayList(5);
        private PublicJourney amendedOutwardPublicJourney;
        private PublicJourney amendedReturnPublicJourney;
        private TDDateTime timeOutward;
        private TDDateTime timeReturn;
		private bool arriveBeforeOutward;
		private bool arriveBeforeReturn;
        private int routeNum;
        private bool valid;
        private bool correctReturnNoPublicJourney;
        private int outwardJourneyIndex;
        private int returnJourneyIndex;
        private bool cycleAlternativeCheckDone;
        private bool cycleAlternativeAvailable;
        private bool accessible = false;

        private bool dirty = true;

        #endregion

        #region Constructors

        /// <summary>
        /// Default contructor
        /// </summary>
        public TDJourneyResult()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="journeyReferenceNumber"></param>
        public TDJourneyResult(int journeyReferenceNumber)
        {
            this.journeyReferenceNumber = journeyReferenceNumber;
        }
        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accessible">Accessible flag indicates if the journey was planned with accessible paramters in the request</param>
		public TDJourneyResult(int journeyReferenceNumber, int startRouteNum, 
            TDDateTime timeO, TDDateTime timeR, bool arriveBeforeOutward, bool arriveBeforeReturn,
            bool accessible) 
            : this( journeyReferenceNumber )
		{
			this.timeOutward = timeO;
			this.timeReturn = timeR;
			this.arriveBeforeOutward = arriveBeforeOutward;
			this.arriveBeforeReturn = arriveBeforeReturn;
            this.accessible = accessible;
			this.routeNum = startRouteNum;
		}

        #endregion

        #region Public methods - Add Result and Journeys

        /// <summary>
        /// Add the JourneyResult to this TDResult.  The outward flag determines where the information will
        /// be added.
        /// </summary>
        /// <param name="cjpResult">The result from the CJP</param>
        /// <param name="outward">Should this go into the outward or return arrays</param>
        /// <param name="publicVia">A location for use when searching if any of the locations used are a via point</param>
        /// <param name="privateVia">A location private journey is searching via</param>
        /// <param name="sessionId">The current session id (used in logging)</param>
        /// <returns>true if at least one journey was found, false otherwise</returns>
		public bool AddResult(JourneyResult cjpResult, bool outward, TDLocation publicVia, TDLocation privateVia, TDLocation requestOrigin, TDLocation requestDestination, string sessionId, bool ignoreCongestion, int congestionValue)        
		{
            if  (cjpResult == null)
            {
                OperationalEvent oe = new OperationalEvent(TDEventCategory.Business, 
                    TDTraceLevel.Error, 
                    "Unexpected null JourneyResult, ref = " + journeyReferenceNumber.ToString());
                Logger.Write(oe);
                return false;
            }

            dirty = true;

            // Check for a private or public journey result            

            #region Public journey

            if  (cjpResult.publicJourneys != null )
            {
                for (int i = 0; i < cjpResult.publicJourneys.Length; i++ )
                {
                    if	(cjpResult.publicJourneys[i].legs != null && cjpResult.publicJourneys[i].legs.Length > 0)

					if  (outward)
                    {
                        PublicJourney pj = new PublicJourney(outwardJourneyIndex++, cjpResult.publicJourneys[i], 
                            publicVia, requestOrigin, requestDestination, TDJourneyType.PublicOriginal, accessible,
                            GetRouteNum());
						if	(!IsPublicJourneyAlreadyStored(pj, true))
                        {
                            #region Car parking check

                            //DEL 9 Car Parking - check for car park as the start/end location 
							if((pj.JourneyLegs[0].LegStart.Location.Description.EndsWith("car park")) ||
                                ( (requestOrigin != null) && requestOrigin.SearchType == SearchType.CarPark))
							{
								if(requestOrigin.CarParking != null)
								{
									pj.JourneyLegs[0].LegStart.Location.CarParking = requestOrigin.CarParking;
                                    pj.JourneyLegs[0].LegStart.Location.SearchType = requestOrigin.SearchType;
								}
							}
							
							// check the final journey leg destination
							if((pj.JourneyLegs[pj.JourneyLegs.Length - 1].LegEnd.Location.Description.EndsWith("car park")) ||
                                ( (requestDestination != null) && requestDestination.SearchType == SearchType.CarPark))
							{
								if(requestDestination.CarParking != null)
								{
									pj.Details[pj.Details.Length - 1].LegEnd.Location.CarParking = requestDestination.CarParking;
                                    pj.Details[pj.Details.Length - 1].LegEnd.Location.SearchType = requestDestination.SearchType;
								}
                            }

                            #endregion

                            outwardPublicJourney.Add(pj);
						}
						else
						{
							outwardJourneyIndex--;
						}

                    }
                    else
                    {
                        PublicJourney pj = new PublicJourney(returnJourneyIndex++, cjpResult.publicJourneys[i], 
                            publicVia, requestDestination, requestOrigin, TDJourneyType.PublicOriginal, accessible,
                            GetRouteNum());
						if	(!IsPublicJourneyAlreadyStored(pj, false))
                        {
                            #region Car parking check

                            //DEL 9 Car Parking - check for car park as the start/end location 
							if((pj.JourneyLegs[0].LegStart.Location.Description.EndsWith("car park")) ||
                                ((requestDestination != null) && requestDestination.SearchType == SearchType.CarPark))
							{
								if(requestDestination.CarParking != null)
								{
									pj.JourneyLegs[0].LegStart.Location.CarParking = requestDestination.CarParking;
                                    pj.JourneyLegs[0].LegStart.Location.SearchType = requestDestination.SearchType;
								}
							}

							// check the final journey leg destination
							if((pj.JourneyLegs[pj.JourneyLegs.Length - 1].LegEnd.Location.Description.EndsWith("car park")) ||
                                ((requestOrigin != null) && requestOrigin.SearchType == SearchType.CarPark))
							{
								if(requestOrigin.CarParking != null)
								{
									pj.Details[pj.Details.Length - 1].LegEnd.Location.CarParking = requestOrigin.CarParking;
                                    pj.Details[pj.Details.Length - 1].LegEnd.Location.SearchType = requestOrigin.SearchType;
								}
                            }

                            #endregion

                            returnPublicJourney.Add(pj);
						}
						else
						{
							returnJourneyIndex--;
						}
                    }
                }
            }

            #endregion

            #region Road journey

            if  (cjpResult.privateJourneys != null && cjpResult.privateJourneys.Length > 0)
			{
				if (cjpResult.privateJourneys.Length == JourneyControlConstants.ExpectedRoadJourneyCount)
				{
					for (int i = 0; i < cjpResult.privateJourneys.Length; i++ )
					{
						int newRouteNum = GetRouteNum();

						
						if  (outward)
						{
							RoadJourney rj = new RoadJourney(outwardJourneyIndex++, cjpResult.privateJourneys[i], newRouteNum, cjpResult.privateJourneys[i].congestion, requestOrigin, requestDestination, privateVia, timeOutward, arriveBeforeOutward);
							outwardRoadJourney.Add(rj); 
						}
						else
						{   
							RoadJourney rj = new RoadJourney(returnJourneyIndex++, cjpResult.privateJourneys[i], newRouteNum, cjpResult.privateJourneys[i].congestion, requestDestination, requestOrigin, privateVia, timeReturn, arriveBeforeReturn);
							returnRoadJourney.Add(rj);
						}

						HandOffJourneyToEsriMap(cjpResult.privateJourneys[i], newRouteNum, sessionId, ignoreCongestion, congestionValue);
					}
				}
				else
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
						"Unexpected number of private journeys returned = " + cjpResult.privateJourneys.Length + "; - all road journeys discarded"));
				}
            }

            #endregion

            AddMessages(cjpResult.messages, outward);

            bool thisPartValid = false;
        
            if  (outward)
            {
                if  (outwardPublicJourney.Count > 0 || outwardRoadJourney.Count > 0)
                {
                    valid = true;
                    thisPartValid = true;
                }
            }
            else
            {
                if  (returnPublicJourney.Count > 0 || returnRoadJourney.Count > 0)
                {
                    valid = true;
                    thisPartValid = true;
                }
            }
            return thisPartValid;
        }

        /// <summary>
        /// Add a PublicJourney to a TDResult.  The outward flag determines where the information will
        /// be added.
        /// </summary>
        /// <param name="PublicJourney">The input PublicJouney</param>
        /// <param name="outward">Should this go into the outward or return arrays</param>
        public void AddPublicJourney(PublicJourney publicJourney, bool outward)
        {
            if (publicJourney != null)
            {
                dirty = true;

                //Add PublicJouney to approapriate arraylist
                if (outward)
                {
                    PublicJourney pj = new PublicJourney(outwardJourneyIndex++, publicJourney.Details, publicJourney.Type, GetRouteNum());
                    if (!IsPublicJourneyAlreadyStored(pj, true))
                    {
                        outwardPublicJourney.Add(pj);
                    }
                    else
                    {
                        outwardJourneyIndex--;   // restore to previous value
                    }
                }
                else
                {
                    PublicJourney pj = new PublicJourney(returnJourneyIndex++, publicJourney.Details, publicJourney.Type, GetRouteNum());
                    if (!IsPublicJourneyAlreadyStored(pj, false))
                    {
                        returnPublicJourney.Add(pj);
                    }
                    else
                    {
                        returnJourneyIndex--;
                    }
                }
            }
        }

        /// <summary>
        /// Add a PublicJourney to a TDResult.  The outward flag determines where the information will
        /// be added.
        /// </summary>
        /// <param name="PublicJourney">The input PublicJouney</param>
        /// <param name="outward">Should this go into the outward or return arrays</param>
        public void AddPublicJourney(PublicJourney publicJourney, bool outward, bool retainJourneyDate, bool retainJourneyDuration)
        {
            if  (publicJourney != null )
            {
                dirty = true;

                //Add PublicJouney to approapriate arraylist
                if  (outward)
                {

                    PublicJourney pj = null;
                    
                    if(retainJourneyDate)
                        pj = new PublicJourney(outwardJourneyIndex++, publicJourney.Details, publicJourney.Type, GetRouteNum(), publicJourney.JourneyDate);
                    else
                        pj = new PublicJourney(outwardJourneyIndex++, publicJourney.Details, publicJourney.Type, GetRouteNum());

                    if (retainJourneyDuration)
                    {
                        pj.Duration = publicJourney.Duration;
                    }

					if	(!IsPublicJourneyAlreadyStored(pj, true))
					{
						outwardPublicJourney.Add(pj);
					}
					else
					{
						outwardJourneyIndex--;   // restore to previous value
					}
                }
                else
                {
                    PublicJourney pj = null;

                    if (retainJourneyDate)
                        pj = new PublicJourney(returnJourneyIndex++, publicJourney.Details, publicJourney.Type, GetRouteNum(), publicJourney.JourneyDate);
                    else
                        pj = new PublicJourney(returnJourneyIndex++, publicJourney.Details, publicJourney.Type, GetRouteNum());

                    if (retainJourneyDuration)
                    {
                        pj.Duration = publicJourney.Duration;
                    }

					if	(!IsPublicJourneyAlreadyStored(pj, false))
					{
						returnPublicJourney.Add(pj);
					}
					else
					{
						returnJourneyIndex--; 
					}
                }
            }   
        }


		/// <summary>
		/// Adds a public jounrey to a TDJourneyResult. 
		/// </summary>
		/// <param name="publicJourney">The journey to add</param>
		/// <param name="outward">Should this go into the outward or return arrays</param>
		/// <param name="index">Index journey should take in array list</param>
		public void AddPublicJourney(PublicJourney publicJourney, bool outward, int index)
		{
			//Add new journey at index position
			if (outward)
			{
				PublicJourney pj = new PublicJourney(outwardJourneyIndex++, publicJourney.Details, publicJourney.Type, GetRouteNum());
				if	(!IsPublicJourneyAlreadyStored(pj, true))
				{
					outwardPublicJourney.Insert(index, pj);
				}
				else
				{
					outwardJourneyIndex--;   // restore to previous value
				}
			}
			else
			{
				PublicJourney pj = new PublicJourney(returnJourneyIndex++, publicJourney.Details, publicJourney.Type, GetRouteNum());
				if	(!IsPublicJourneyAlreadyStored(pj, false))
				{
					returnPublicJourney.Insert(index, pj);
				}
				else
				{
					returnJourneyIndex--;   
				}
			}
		}

		/// <summary>
		/// Checks if a newly created PublicJourney already  
		/// exists as a part of the current TDJourneyResult
		/// (used to prevent addition of duplicates)
		/// </summary>
		/// <param name="newPj">the newly created PublicJourney</param>
		/// <param name="outward">true if outward, false if return</param>
		/// <returns>true if the journey already exists, false otherwise</returns>
		private bool IsPublicJourneyAlreadyStored(PublicJourney newPj, bool outward)
		{
			foreach (PublicJourney pj in (outward ? outwardPublicJourney : returnPublicJourney))
			{
				if	(pj.IsSameJourney(newPj))
				{
					return true;
				}
			}
			return false;
		}

        /// <summary>
        /// Remove a PublicJourney from a TDResult.  The outward flag determines where the information will
        /// be added.
        /// If the routenum of the current PublicJourney matches 
        /// that of the passed in public journey then remove that PublicJourney
        /// from the outward or return PublicJourney arraylist.
        /// </summary>
        /// <param name="PublicJourney">The input PublicJouney</param>
        /// <param name="outward">Should this go into the outward or return arrays</param>
        public void RemovePublicJourney(PublicJourney publicJourney, bool outward)
        {

            dirty = true;

            if  (outward)
            {
                //remove matching PublicJourney
                foreach (PublicJourney pj in outwardPublicJourney)
                {
                    if  (pj.RouteNum == publicJourney.RouteNum)
                    {
                        outwardPublicJourney.Remove(pj);
                        break;
                    }
                }
            }
            else
            {
                //remove matching PublicJourney
                foreach (PublicJourney pj in returnPublicJourney)
                {
                    if  (pj.RouteNum == publicJourney.RouteNum)
                    {
                        returnPublicJourney.Remove(pj);
                        break;
                    }   
                }
            }
        }

        /// <summary>
        /// Add a RoadJourney to a TDResult.  The outward flag determines where the information will
        /// be added.
        /// </summary>
        public void AddRoadJourney(RoadJourney roadJourney, bool outward, bool isNewRouteNumber)
        {
            if (roadJourney != null)
            {
                dirty = true;

                int newRouteNum = isNewRouteNumber ? GetRouteNum() : roadJourney.RouteNum;

                //Add RoadJouney to approapriate arraylist - assumes there can only be one road journey in this TDJourneyResult
                if (outward)
                {
                    // Get index of existing road journey, and delete it
                    int index = 0;
                    if (outwardRoadJourney.Count > 0)
                    {
                        RoadJourney existingRj = (RoadJourney)outwardRoadJourney[0];
                        index = existingRj.JourneyIndex;

                        outwardRoadJourney.Clear();
                    }
                    else
                    {
                        // No road journeys, get an index
                        index = outwardJourneyIndex++;
                    }

                    RoadJourney rj = new RoadJourney(index, newRouteNum, roadJourney.Details, roadJourney.PrivateJourneyDetails, roadJourney.TotalDistance, roadJourney.TotalDuration, roadJourney.TotalFuelCost, 
                        roadJourney.Emissions, roadJourney.Congestion, roadJourney.AllowReplan);
                                        
                    outwardRoadJourney.Add(rj);
                }
                else
                {
                    // Get index of existing road journey, and delete it
                    int index = 0;
                    if (returnRoadJourney.Count > 0)
                    {
                        RoadJourney existingRj = (RoadJourney)returnRoadJourney[0];
                        index = existingRj.JourneyIndex;

                        returnRoadJourney.Clear();
                    }
                    else
                    {
                        // No road journeys, get an index
                        index = returnJourneyIndex++;
                    }

                    RoadJourney rj = new RoadJourney(index, newRouteNum, roadJourney.Details, roadJourney.PrivateJourneyDetails, roadJourney.TotalDistance, roadJourney.TotalDuration, roadJourney.TotalFuelCost, 
                        roadJourney.Emissions, roadJourney.Congestion, roadJourney.AllowReplan);

                    returnRoadJourney.Add(rj);
                }
            }
        }

        #endregion

        #region Public methods - Journey display numbers

        /// <summary>
        /// Gets the display number for the selected Outward journey
        /// </summary>
        public string OutwardDisplayNumber(int journeyIndex)
        {
            bool found = false;
            int index = 0;

            if (outwardPublicJourney != null)
            {
                for ( ; index < OutwardPublicJourneyCount; index++)
                {
                    found = (((PublicJourney)outwardPublicJourney[index]).JourneyIndex == journeyIndex);
                    if (found)
                    {
                        break;
                    }
                }
            }

            if ( !found && (outwardRoadJourney != null))
            {
                for (int i=0; i < OutwardRoadJourneyCount; i++, index++)
                {
                    found = (((RoadJourney)outwardRoadJourney[i]).JourneyIndex == journeyIndex);
                    if (found)
                    {
                        break;
                    }
                }
            }

            if (found)
            {
                return (index + 1).ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Gets the display number for the selected Return journey
        /// </summary>
        public string ReturnDisplayNumber(int journeyIndex)
        {
            bool found = false;
            int index = 0;

            if (returnPublicJourney != null)
            {
                for ( ; index < ReturnPublicJourneyCount; index++)
                {
                    found = (((PublicJourney)returnPublicJourney[index]).JourneyIndex == journeyIndex);
                    if (found)
                    {
                        break;
                    }
                }
            }

            if ( !found && (returnRoadJourney != null))
            {
                for (int i=0; i < ReturnRoadJourneyCount; i++, index++)
                {
                    found = (((RoadJourney)returnRoadJourney[i]).JourneyIndex == journeyIndex);
                    if (found)
                    {
                        break;
                    }
                }
            }

            if (found)
            {
                return (index + 1).ToString();
            }
            else
            {
                return "";
            }
        }

        #endregion

        #region Public methods - Journey summary lines

        /// <summary>
        /// Constructs a Journey Summary Lines for the outward journeys.
        /// </summary>
        /// <param name="arriveBefore">Indicates if results were calculated using
        /// "Arrive Before" or "Depart After".</param>
        /// <returns>A Journey Summary Line array for the outward journeys.</returns>
        public JourneySummaryLine[] OutwardJourneySummary(bool arriveBefore)
        {
            ModeType[] modeType=null; 
            return OutwardJourneySummary(arriveBefore, modeType, false, false);
        }

        /// <summary>
        /// Constructs a Journey Summary Lines for the outward journeys.
        /// </summary>
        /// <param name="arriveBefore">Indicates if results were calculated using
        /// "Arrive Before" or "Depart After".</param>
        /// <param name="modeType">Indicates transport modes to be used</param>
        /// <returns>A Journey Summary Line array for the outward journeys.</returns>
        public JourneySummaryLine[] OutwardJourneySummary(bool arriveBefore, ModeType[] modeType)
        {
            return OutwardJourneySummary(arriveBefore, modeType, false, false);
        }

        /// <summary>
        /// Constructs a Journey Summary Lines for the outward journeys.
        /// </summary>
        /// <param name="arriveBefore">Indicates if results were calculated using
        /// "Arrive Before" or "Depart After".</param>
        /// <param name="modeType">Indicates transport modes to be used</param>
        /// <returns>A Journey Summary Line array for the outward journeys.</returns>
        public JourneySummaryLine[] OutwardJourneySummary(bool arriveBefore, ModeType[] modeType, bool ignoreStartLegTransferMode, bool ignoreEndLegTransferMode)
        {
            
            // Even thou this sorts the outward journeys, the sorting is only important
            // to the GetJourneySummary call (which is a private function). 
            // All accesses to get individual journeys will always be fetched with the
            // journey ID and not the index in the sorted array.
            // Thus this change does not need to be serialized to the database and therefore
            // this does not place the object into a dirty-state.
        
            // Check that the outwardPublicJourney is not null otherwise null exception will
            // be thrown if an attempt to sort a null object is made.
            if  (outwardPublicJourney != null && outwardPublicJourney.Count > 0)
            {
                outwardPublicJourney.Sort(arriveBefore ? (IComparer)(new ArriveBeforeComparer()) : (IComparer)(new DepartAfterComparer()));
            }

            return GetJourneySummary(outwardPublicJourney, outwardRoadJourney, amendedOutwardPublicJourney, timeOutward, arriveBefore, modeType, ignoreStartLegTransferMode, ignoreEndLegTransferMode);
        }

        /// <summary>
        /// Constructs a Journey Summary Lines for the return journeys.
        /// </summary>
        /// <param name="arriveBefore">Indicates if results were calculated using
        /// "Arrive Before" or "Depart After".</param>
        /// <returns>A Journey Summary Line array for the return journeys.</returns>
        public JourneySummaryLine[] ReturnJourneySummary(bool arriveBefore)
        {
            ModeType[] modeType=null;
            return ReturnJourneySummary(arriveBefore, modeType);
        }

        /// <summary>
        /// Constructs a Journey Summary Lines for the return journeys.
        /// </summary>
        /// <param name="arriveBefore">Indicates if results were calculated using
        /// "Arrive Before" or "Depart After".</param>
        /// <param name="modeType">Indicates the modes of transport to be used</param>
        /// <returns>A Journey Summary Line array for the return journeys.</returns>
        public JourneySummaryLine[] ReturnJourneySummary(bool arriveBefore, ModeType[] modeType)
        {
            return ReturnJourneySummary(arriveBefore, modeType, false, false);
        }

        /// <summary>
        /// Constructs a Journey Summary Lines for the return journeys.
        /// </summary>
        /// <param name="arriveBefore">Indicates if results were calculated using
        /// "Arrive Before" or "Depart After".</param>
        /// <param name="modeType">Indicates the modes of transport to be used</param>
        /// <returns>A Journey Summary Line array for the return journeys.</returns>
        public JourneySummaryLine[] ReturnJourneySummary(bool arriveBefore,ModeType[] modeType, bool ignoreStartLegTransferMode, bool ignoreEndLegTransferMode)
        {

            // Even thou this sorts the outward journeys, the sorting is only important
            // to the GetJourneySummary call (which is a private function). 
            // All accesses to get individual journeys will always be fetched with the
            // journey ID and not the index in the sorted array.
            // Thus this change does not need to be serialized to the database and therefore
            // this does not place the object into a dirty-state.

            // Check that the returnPublicJourney is not null otherwise null exception will
            // be thrown if an attempt to sort a null object is made.
            if  (returnPublicJourney != null && returnPublicJourney.Count > 0)
            {
                returnPublicJourney.Sort(arriveBefore ? (IComparer)(new ArriveBeforeComparer()) : (IComparer)(new DepartAfterComparer()));
            }

            return
                GetJourneySummary
                (returnPublicJourney, returnRoadJourney, amendedReturnPublicJourney, timeReturn, arriveBefore, modeType, ignoreStartLegTransferMode, ignoreEndLegTransferMode);
        }
        
        /// <summary>
        /// Creates a Journey Summary Lines.
        /// </summary>
        /// <param name="publicJourneys">Public Journeys to create summary lines for.</param>
        /// <param name="roadJourneys">Road Journeys to create summary lines for.</param>
        /// <param name="amendedJourney">Amended Journeys to create summary line for.</param>
        /// <returns>A Journey Summary Line array.</returns>
        private JourneySummaryLine[] GetJourneySummary
            (ArrayList publicJourneys, ArrayList roadJourneys,
            PublicJourney amendedJourney, TDDateTime time, bool arriveBefore,ModeType[] modeType,
            bool ignoreStartLegTransferMode, bool ignoreEndLegTransferMode)
        {
            int numberOfResults = 0;
            int amendedJourneyIndex = -1;
            
            // Set numberOfResults to consist only of journeys we want to return
            if  (publicJourneys != null)
            {
                foreach (PublicJourney journey in publicJourneys)
                {
                    if (UseJourney(journey, modeType))
                    {
                        numberOfResults ++;
                    }
                }
            }
            
            if (roadJourneys != null)
            {
                // increment only if journey type is congested.
                foreach (RoadJourney journey in roadJourneys)
                {
                    if (UseJourney(journey, modeType))
                    {
                        if (journey.Type == TDJourneyType.RoadCongested)
                        {
                            numberOfResults++;
                        }
                    }
                }
            }

            if  (amendedJourney != null)
            {
                if (UseJourney(amendedJourney, modeType))
                {
                    numberOfResults ++;
                }
                
                amendedJourneyIndex = amendedJourney.JourneyIndex;
            }

            // Create the Journey Summary Line array.
            JourneySummaryLine[] result = new JourneySummaryLine[numberOfResults];

            // Build the array.
            
            int index = 0;

            // Use a seperate count for display number becase it does not need to
            // be incremented when adding an amended public journey unlike the index
            // which needs to be incremented at each addition.
            int displayNumber = 1;

            if (publicJourneys != null)
            {
                TDDateTime departDateTime;
                TDDateTime arrivalDateTime;

                foreach (PublicJourney journey in publicJourneys)
                {
                    // Get the depart and arrival times for this journey
                    // Use CheckInTime or ExitTime over DepartDateTime and ArriveDateTime
                    // respectively.
                    if (journey.Details[0].CheckInTime != null)
                        departDateTime = journey.Details[0].CheckInTime;
                    else
                        departDateTime = journey.Details[0].LegStart.DepartureDateTime;

                    if (journey.Details[journey.Details.Length-1].ExitTime != null)
                        arrivalDateTime = journey.Details[journey.Details.Length-1].ExitTime;
                    else
                        arrivalDateTime = journey.Details[journey.Details.Length-1].LegEnd.ArrivalDateTime;

                    // Does this Public journey use only the modes in the modeType array
                        if (UseJourney(journey,modeType))
                        {
                            // Get the detail for the origin and destination location text
                            PublicJourneyDetail pjdStart = journey.Details[0];
                            PublicJourneyDetail pjdEnd = journey.Details[journey.Details.Length - 1];

                            #region Get the detail ignoring the Transfer leg if specified

                            if ((ignoreStartLegTransferMode) && (journey.Details.Length > 0))
                            {
                                for (int i=0; i < journey.Details.Length; i++)
                                {
                                    PublicJourneyDetail pjd = journey.Details[i];
                                
                                    // First instance of a non Transfer mode leg
                                    if (pjd.Mode != ModeType.Transfer)
                                    {
                                        pjdStart = pjd;
                                        break;
                                    }
                                }
                            }

                            if ((ignoreEndLegTransferMode) && (journey.Details.Length > 0))
                            {
                                for (int i = (journey.Details.Length -1); i >= 0; i--)
                                {
                                    PublicJourneyDetail pjd = journey.Details[i];

                                    // Last instance of a non Transfer mode leg
                                    if (pjd.Mode != ModeType.Transfer)
                                    {
                                        pjdEnd = pjd;
                                        break;
                                    }
                                }
                            }

                            #endregion

                            result[index++] = new JourneySummaryLine(
                            journey.JourneyIndex,
                            pjdStart.LegStart.Location.Description,
                            pjdEnd.LegEnd.Location.Description,
                            journey.Type,
                            GetAllModes(journey),
                            GetInterchangeCount(journey),
                            departDateTime,
                            arrivalDateTime,
                            journey.Duration,
                            0,
                            displayNumber.ToString(),
                            GetOperatorNames(journey));

                        }
                    // Check to see if an amended journey exists for the current journey
                    if  (amendedJourneyIndex == journey.JourneyIndex)
                    {
                        if (amendedJourney.Details[0].CheckInTime != null)
                            departDateTime = amendedJourney.Details[0].CheckInTime;
                        else
                            departDateTime = amendedJourney.Details[0].LegStart.DepartureDateTime;

                        if (amendedJourney.Details[amendedJourney.Details.Length-1].ExitTime != null)
                            arrivalDateTime = amendedJourney.Details[amendedJourney.Details.Length-1].ExitTime;
                        else
                            arrivalDateTime = amendedJourney.Details[amendedJourney.Details.Length-1].LegEnd.ArrivalDateTime;

                        // Amended journey exists for this journey so create a summary line for it.
                        result[index++] = new JourneySummaryLine(
                            amendedJourney.JourneyIndex,
                            amendedJourney.Details[0].LegStart.Location.Description,
                            amendedJourney.Details[amendedJourney.Details.Length - 1].LegEnd.Location.Description,
                            amendedJourney.Type,
                            GetAllModes(amendedJourney),
                            GetInterchangeCount(amendedJourney),
                            departDateTime,
                            arrivalDateTime,
                            journey.Duration,
                            0,
                            displayNumber.ToString() + "a",
                            GetOperatorNames(journey));
                    }

                    // Update the display number
                    displayNumber++;
                } // foreach
            }

            // Now add the Road journey to the Summary Line.
            // Only the RoadCongested route should be added to the Summary Line,
            // the RoadFreeflow route is ignored.
            if (roadJourneys != null)
            {
                foreach (RoadJourney journey in roadJourneys)
                {
                    
					if  ((journey.Type == TDJourneyType.RoadCongested)
                        &&
                        (UseJourney(journey, modeType)))
                    {
                        ModeType[] car = new ModeType[] { ModeType.Car };
                        TDDateTime start = journey.CalculateStartTime(time,arriveBefore);
                        TDDateTime end = journey.CalculateEndTime(time,arriveBefore);

						result[index++] = new JourneySummaryLine(
							journey.JourneyIndex, journey.Type, car,
							0, start, end, journey.TotalDistance, displayNumber.ToString());

                        displayNumber++;
                    }   
                }
            }

            return result;
        }

        #endregion

        #region UseJourney
        /// <summary>
        /// Returns true if journey contains only those modes specified in modeType array
        /// </summary>
        /// <param name="journey"></param>
        /// <param name="modeType"></param>
        /// <returns></returns>
        private bool UseJourney(PublicJourney journey, ModeType[] modeType)
        {
            return UseJourneyHelper(journey.GetUsedModes(), modeType);
        }
        
        /// <summary>
        /// Returns true if journey contains only those modes specified in modeType array
        /// </summary>
        /// <param name="journey"></param>
        /// <param name="modeType"></param>
        /// <returns></returns>
        private bool UseJourney(RoadJourney journey, ModeType[] modeType)
        {
            return UseJourneyHelper(journey.GetUsedModes(), modeType);
        }

        public static bool UseJourneyHelper(ModeType[] journeyModes, ModeType[] modeType)
        {
            bool useJourney = true;

            // Compare journey modes to the modes asked for
            if (modeType != null)
            {
                foreach (ModeType jm in journeyModes)
                {
                    if (jm != ModeType.Walk)
                    {
                        bool isJourneyModeInModeTypes = false;

                        foreach (ModeType mt in modeType)
                        {
                            if (jm == mt)
                            {
                                isJourneyModeInModeTypes = true;
                            }
                        }

                        if (isJourneyModeInModeTypes)
                            useJourney = true;
                        else
                        {
                            useJourney = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                useJourney = true;
            }

            return useJourney;
        }

        #endregion

        #region Public methods - Journey overview lines

        /// <summary>
        /// Returns the fastest journey and sets the earliest and latest departure values for the public journeys provided.
        /// </summary>
        /// <param name="publicJourneys">An ArrayList of public journeys.</param>
        /// <param name="duration">A ref TimeSpan for the duration of the shortest/fastest journey.</param>
        /// <param name="earliestDeparture">A ref TDDateTime for earliest departure.</param>
        /// <param name="latestDeparture">A ref TDDateTime for the latest departure.</param>
        /// <returns>Fastest PublicJourney.</returns>
        private PublicJourney GetFastestJourney(ArrayList publicJourneys, ref TimeSpan duration, ref TDDateTime earliestDeparture, ref TDDateTime latestDeparture)
        {
            PublicJourney fastestJourney = null;

            foreach (PublicJourney pj in publicJourneys)
            {
                // Ensure we dont check any durations below zero, as this could be an invalid timespan
                if (((duration == null) || (duration > pj.Duration)) && (pj.Duration > TimeSpan.Zero))
                {
                    duration = pj.Duration;
                    fastestJourney = pj;
                }

                if ((earliestDeparture == null) || (earliestDeparture > pj.JourneyDate))
                    earliestDeparture = pj.JourneyDate;

                if ((latestDeparture == null) || (latestDeparture < pj.JourneyDate))
                    latestDeparture = pj.JourneyDate;
            }

            return fastestJourney;
        }

        /// <summary>
        /// Returns an ArrayList of public journeys which are of the ModeType specified
        /// </summary>
        /// <param name="publicJourneys"></param>
        /// <param name="modeType"></param>
        /// <returns></returns>
        private ArrayList GetPublicJourneys(ArrayList publicJourneys, ModeType modeType)
        {
            ArrayList journeys = new ArrayList();

            foreach (PublicJourney pj in publicJourneys)
            {
                ArrayList journeyModes = new ArrayList();
                journeyModes.AddRange(pj.GetUsedModes());

                bool coachmode = ((journeyModes.Contains(ModeType.Coach)) || (journeyModes.Contains(ModeType.Bus)));
                bool trainmode = ((journeyModes.Contains(ModeType.Rail))
                           ||
                         (journeyModes.Contains(ModeType.RailReplacementBus))
                           ||
                         (journeyModes.Contains(ModeType.Metro))
                           ||
                         (journeyModes.Contains(ModeType.Tram))
                           ||
                         (journeyModes.Contains(ModeType.Underground)));

                bool airmode = journeyModes.Contains(ModeType.Air);

                if (coachmode && !airmode && !trainmode && modeType == ModeType.Coach)
                    journeys.Add(pj);

                if (trainmode && !airmode && !coachmode && modeType == ModeType.Rail)
                    journeys.Add(pj);

                if (airmode && !coachmode && !trainmode && modeType == ModeType.Air)
                    journeys.Add(pj);
            }

            return journeys;
        }

        /// <summary>
        /// Returns the JourneyOverviewLine for the public journeys provided.
        /// Determines the JourneyOverviewLine ModeType based on the first journey in the publicJourneys array.
        /// </summary>
        /// <param name="publicJourneys"></param>
        /// <returns></returns>
        private JourneyOverviewLine GetJourneyOverviewLine(ArrayList publicJourneys, ModeType journeyModeType, bool isForITP)
        {
            if (publicJourneys.Count > 0)
            {
                //get ModeType of first journey in array
                PublicJourney pj = (PublicJourney)publicJourneys[0];
                ModeType[] modeType = pj.GetUsedModes();
                
                TDJourneyType journeyType = TDJourneyType.PublicOriginal;

                string numberOfJourneys = publicJourneys.Count.ToString();

                TimeSpan duration = new TimeSpan(9, 9, 9, 9);
                TDDateTime earliestDeparture = null;
                TDDateTime latestDeparture = null;
                PublicJourney fastestJourney = GetFastestJourney(publicJourneys, ref duration, ref earliestDeparture, ref latestDeparture);
                float emissions = 0;

                JourneyOverviewLine jol = new JourneyOverviewLine(journeyType, journeyModeType, isForITP, modeType, numberOfJourneys, duration, emissions, earliestDeparture, latestDeparture, fastestJourney);

                return jol;
            }
            //return null where no public journeys are provided.
            return null;
        }

        
        /// <summary>
        /// Returns a journey overview line array for each mode of transport (air, rail, coach and car). 
        /// (for use on the city to city overview page)
        /// </summary>
        /// <param name="publicJourneys"></param>
        /// <param name="roadJourneys"></param>
        /// <param name="amendedJourney"></param>
        /// <param name="time"></param>
        /// <param name="arriveBefore"></param>
        /// <returns></returns>
        private JourneyOverviewLine[] GetJourneyOverview(ArrayList publicJourneys, ArrayList roadJourneys, PublicJourney amendedJourney, TDDateTime time, bool arriveBefore)
        {
            #region Filter Public journeys
            ArrayList railPublicJourneys = GetPublicJourneys(publicJourneys, ModeType.Rail);
            ArrayList coachPublicJourneys = GetPublicJourneys(publicJourneys, ModeType.Coach);
            ArrayList airPublicJourneys = GetPublicJourneys(publicJourneys, ModeType.Air);
            ArrayList itpPublicJourneys = GetCombinedPublicJourneys(publicJourneys);

            // If there is an amended journey, determine its modetype and add it to appropriate
            // public journey arraylist

            if (amendedJourney != null)
            {
                switch (amendedJourney.GetJourneyModeType())
                {
                    case ModeType.Rail:
                        railPublicJourneys.Add(amendedJourney);
                        break;

                    case ModeType.Coach:
                        coachPublicJourneys.Add(amendedJourney);
                        break;

                    case ModeType.Air:
                        airPublicJourneys.Add(amendedJourney);
                        break;

                    default:
                        // Where its not any of the above modes, ignore the journey because we're 
                        // only concerned about the above modes
                        break;
                }
            }

            #endregion

            #region Create and Overview lines
            List<JourneyOverviewLine> journeyOverviewList = new List<JourneyOverviewLine>();

            JourneyOverviewLine jol = GetJourneyOverviewLine(railPublicJourneys, ModeType.Rail, false);
            if (jol != null)
                journeyOverviewList.Add(jol);

            #region Overview line for Car journey
            if (roadJourneys.Count > 0)
            {
                RoadJourney rj = (RoadJourney)roadJourneys[0];
                ModeType[] modeType = { ModeType.Car };
                TimeSpan duration = new TimeSpan(0, 0, (int)rj.TotalDuration);
                float emissions = 0;

                TDDateTime earliestDeparture = new TDDateTime(rj.DepartDateTime.Year, rj.DepartDateTime.Month, rj.DepartDateTime.Day, 0, 1, 0);
                TDDateTime latestDeparture = new TDDateTime(rj.DepartDateTime.Year, rj.DepartDateTime.Month, rj.DepartDateTime.Day, 23, 59, 0);

                JourneyOverviewLine carJourneyOverviewLine = new JourneyOverviewLine(TDJourneyType.RoadCongested, ModeType.Car, false, modeType, string.Empty, duration, emissions, earliestDeparture, latestDeparture, rj);

                if (carJourneyOverviewLine != null)
                    journeyOverviewList.Add(carJourneyOverviewLine);
            }
            #endregion

            jol = GetJourneyOverviewLine(coachPublicJourneys, ModeType.Coach, false);
            if (jol != null)
                journeyOverviewList.Add(jol);

            jol = GetJourneyOverviewLine(airPublicJourneys, ModeType.Air, false);
            if (jol != null)
                journeyOverviewList.Add(jol);

            jol = GetJourneyOverviewLine(itpPublicJourneys, ModeType.Walk, true);
            if (jol != null)
                journeyOverviewList.Add(jol);

            #endregion

            return journeyOverviewList.ToArray();
        }

        /// <summary>
        /// Returns the array list of ITP journeys
        /// </summary>
        /// <param name="publicJourneys">All public journeys</param>
        /// <returns></returns>
        private ArrayList GetCombinedPublicJourneys(ArrayList publicJourneys)
        {
            ArrayList journeys = new ArrayList();

            foreach (PublicJourney pj in publicJourneys)
            {
                ArrayList journeyModes = new ArrayList();
                journeyModes.AddRange(pj.GetUsedModes());

                bool coachmode = ((journeyModes.Contains(ModeType.Coach)) || (journeyModes.Contains(ModeType.Bus)));
                bool trainmode = ((journeyModes.Contains(ModeType.Rail))
                           ||
                         (journeyModes.Contains(ModeType.RailReplacementBus))
                           ||
                         (journeyModes.Contains(ModeType.Metro))
                           ||
                         (journeyModes.Contains(ModeType.Tram))
                           ||
                         (journeyModes.Contains(ModeType.Underground)));

                bool airmode = journeyModes.Contains(ModeType.Air);

                if (airmode && (coachmode || trainmode))
                    journeys.Add(pj);
                if (coachmode && trainmode && !airmode)
                    journeys.Add(pj);


            }

            return journeys;

        }

        /// <summary>
        /// Constructs a journey overview line for the outward journeys.
        /// </summary>
        /// <param name="arriveBefore"></param>
        /// <returns></returns>
        public JourneyOverviewLine[] OutwardJourneyOverview(bool arriveBefore)
        {
            return GetJourneyOverview(outwardPublicJourney, outwardRoadJourney, amendedOutwardPublicJourney, timeOutward, arriveBefore);
        }

        /// <summary>
        /// Constructs a journey overview line for the return journeys.
        /// </summary>
        /// <param name="arriveBefore"></param>
        /// <returns></returns>
        public JourneyOverviewLine[] ReturnJourneyOverview(bool arriveBefore)
        {
            return GetJourneyOverview(returnPublicJourney, returnRoadJourney,amendedReturnPublicJourney, timeReturn, arriveBefore);
        }
        
        #endregion

        #region Public static methods

        /// <summary>
		/// Returns the number of interchanges in the given journey.
		/// </summary>
		/// <param name="journey">Journey to find the number for interchanges for.</param>
		/// <returns>Number of interchanges</returns>
		public static int GetInterchangeCount(Journey journey)
		{
			// Different actions required if journey is a road
			// or public transport journey.
			if (journey is RoadJourney)
			{
				// If journey is a road journey return zero.
				return 0;
			}
			else
			{
				int numberOfPublicTransportLegs = 0;
				bool vehicleUsedInCurrentLeg = false;

				// Find the number of public transport legs
				foreach( PublicJourneyDetail detail in (PublicJourneyDetail[])journey.JourneyLegs )
				{
					vehicleUsedInCurrentLeg = VehicleUsed(detail);

					if(vehicleUsedInCurrentLeg) numberOfPublicTransportLegs++;
				}

				int finalCount = numberOfPublicTransportLegs -1;

				// Return the count if not negative otherwise return 0
				return finalCount < 0 ? 0 : finalCount;
			}
		}

		/// <summary>
		/// Finds all the modes used in the given public journey.
		/// </summary>
		/// <param name="journey">Journey to find modes for.</param>
		/// <returns>Array containing modes found.</returns>
		public static ModeType[] GetAllModes(Journey journey)
		{
			ArrayList modeTypes = new ArrayList();

			foreach ( JourneyLeg leg in journey.JourneyLegs )
			{
				modeTypes.Add( leg.Mode );
			}

			return (ModeType[])modeTypes.ToArray(typeof(ModeType));
		}

		/// <summary>
		/// Finds all the operators used in the given public journey.
		/// </summary>
		/// <param name="journey">Public journey to find modes for.</param>
		/// <returns>An array containing all the distinct operators found.</returns>
		public static string[] GetOperatorNames(Journey journey)
		{
			ArrayList operatorNames = new ArrayList();
			
			PublicJourney publicJourney = journey as PublicJourney;

			if (publicJourney != null)
			{
				string operatorName = null;

				foreach (PublicJourneyDetail detail in publicJourney.Details)
				{
					if (detail.Services != null)
					{
						foreach (ServiceDetails service in detail.Services)
						{
							operatorName = (string)service.OperatorName;

							if (operatorName != null
								&& operatorName.Length > 0
								&& !operatorNames.Contains(operatorName))
								operatorNames.Add(operatorName);
						}
					}
				}

			}

			return (string[])operatorNames.ToArray(typeof(string));
		}

        /// <summary>
        /// Determines if a vehicle was used given the details of a leg.
        /// </summary>
        /// <param name="detail">Details of a leg.</param>
        /// <returns>True if a vehicle was used, false otherwise.</returns>
        private static bool VehicleUsed( PublicJourneyDetail detail )
        {
            return ((detail.Mode != ModeType.Cycle) && (detail.Mode != ModeType.Walk));
        }

        #endregion

        public CJPMessage[] CJPMessages
        {
            get 
            {
                return (CJPMessage[]) messageList.ToArray(typeof(CJPMessage));
            }
        }

        #region Public methods - Index, Sequence, and Count values

        /// <summary>
		/// Read-write property
		/// Exposes the last-used outward 
		/// journey index within the result
		/// </summary>
		public int OutwardJourneyIndex
		{
			get { return outwardJourneyIndex; }
			set { outwardJourneyIndex = value; }
		}

        public int JourneyReferenceNumber
        {
            get { return journeyReferenceNumber; }
            set { journeyReferenceNumber = value; }
        }

        public int LastReferenceSequence
        {
            get { return lastReferenceSequence; }
            set 
            {
                dirty = true;
                lastReferenceSequence = value; 
            }
        }

		public int LastFareRequestNumber
		{
			get { return lastFareRequestNumber; }
			set 
			{
				dirty = true;
				lastFareRequestNumber = value; 
			}
		}

        public int OutwardPublicJourneyCount 
        { 
            get
            {
                return (outwardPublicJourney == null ? 0 : outwardPublicJourney.Count);
            } 
        }
        
        public int ReturnPublicJourneyCount
        { 
            get
            {
                return (returnPublicJourney == null ? 0 : returnPublicJourney.Count);
            } 
        }

        public int OutwardRoadJourneyCount
        { 
            get
            {
                return (outwardRoadJourney == null ? 0 : outwardRoadJourney.Count);
            } 
        }

        public int ReturnRoadJourneyCount
        { 
            get
            {
                return (returnRoadJourney == null ? 0 : returnRoadJourney.Count);
            }
        }

        #endregion

        public bool IsValid 
        {
            get 
            {
                return (valid);
            }
            set 
            {
                dirty = true;
                valid = value;
            }
        }

        public bool CycleAlternativeCheckDone
        {
            get
            {
                return cycleAlternativeCheckDone;
            }
            set
            {
                dirty = true;
                cycleAlternativeCheckDone = value;
            }
        }

        public bool CycleAlternativeAvailable
        {
            get
            {
                return cycleAlternativeAvailable;
            }
            set
            {
                dirty = true;
                cycleAlternativeAvailable = value;
            }
        }

        public bool CJPValidError
        {
            get
            {
                return correctReturnNoPublicJourney;
            }
        }

        #region Public methods - Get Journeys 

        /// <summary>
        /// Returns the outward public journey with the given journey index.
        /// </summary>
        /// <param name="journeyIndex">Journey Index to search for.</param>
        /// <returns>Public journey or null if no match was found.</returns>
        public PublicJourney OutwardPublicJourney(int journeyIndex)
        {
            int i = GetSelectedOutwardJourneyIndex(journeyIndex);

            if (i >=0)
            {
                return (PublicJourney)outwardPublicJourney[i];
            }
            else
            {
                return null;
            }
        }

		/// <summary>
		/// Returns the SelectedOutwardJourneyIndex of the outward public journey specified by a JourneyIndex.
		/// JourneyIndex is the index given by the order the journey was added as a result.
		/// SelectedOutwardJourneyIndex is the index given by the chronological order in result set.
		/// </summary>
		/// <param name="journeyIndex">Journey Index to search for.</param>
		/// <returns>Index or -1 if no match was found.</returns>
		public int GetSelectedOutwardJourneyIndex(int journeyIndex)
		{
			int i = 0;
			bool found = false;

			for (i = 0; i < outwardPublicJourney.Count; i++)
			{
				found = (((PublicJourney)outwardPublicJourney[i]).JourneyIndex == journeyIndex);

				if  (found)
				{
					break;
				}
			}

			if  (found)
			{
				return i;
			}
			else
			{
				return -1;
			}
		}

        /// <summary>
        /// Read-only property giving access to all outward public journeys for this journey result
        /// </summary>
        public ArrayList OutwardPublicJourneys
        {
            get 
            {
                return outwardPublicJourney;
            }
        }

        /// <summary>
        /// Read-only property giving access to all return public journeys for this journey result
        /// </summary>
        public ArrayList ReturnPublicJourneys
        {
            get 
            {
                return returnPublicJourney;
            }
        }   

        /// <summary>
        /// Returns the return public journey with the given journey index.
        /// </summary>
        /// <param name="journeyIndex">Journey Index to search for.</param>
        /// <returns>Public journey or null if no match was found.</returns>
        public PublicJourney ReturnPublicJourney(int journeyIndex)
        {
            int i = GetSelectedReturnJourneyIndex(journeyIndex);

			if  (i >= 0)
            {
                return (PublicJourney)returnPublicJourney[i];
            }
            else
            {
                return null;
            }
        }

		/// <summary>
		/// Returns the SelectedReturnJourneyIndex of the return public journey specified by a JourneyIndex.
		/// JourneyIndex is the index given by the order the journey was added as a result.
		/// SelectedReturnJourneyIndex is the index given by the chronological order in result set.
		/// </summary>
		/// <param name="journeyIndex">Journey Index to search for.</param>
		/// <returns>Index or -1 if no match was found.</returns>
		public int GetSelectedReturnJourneyIndex(int journeyIndex)
		{
			int i = 0;
			bool found = false;

			for (i = 0; i < returnPublicJourney.Count; i++)
			{
				found = (((PublicJourney)returnPublicJourney[i]).JourneyIndex == journeyIndex);

				if  (found)
				{
					break;
				}
			}

			if  (found)
			{
				return i;
			}
			else
			{
				return -1;
			}
		}

        /// <summary>
        /// Returns the congested/freeflow outward road journey.
        /// </summary>
        /// <param name="congested">Indiciates if the the congested road
        /// journey or if the freeflow road journey should be returned.</param>
        /// <returns>A RoadJourney if it exists otherwise null.</returns>
        public RoadJourney OutwardRoadJourney()
        {

            RoadJourney roadJourneyToReturn = null;

            if  (outwardRoadJourney == null)
            {
                return null;
            }

            // find the congested outward road journey
            foreach (RoadJourney roadJourney in outwardRoadJourney)
            {
                if  (roadJourney.Type == TDJourneyType.RoadCongested)
                {
                    roadJourneyToReturn = roadJourney;
                    break;
                }
            }

            return roadJourneyToReturn;
        }

        /// <summary>
        /// Returns the congested/freeflow return road journey.
        /// </summary>
        /// <param name="congested">Indiciates if the the congested road
        /// journey or if the freeflow road journey should be returned.</param>
        /// <returns>A RoadJourney if it exists otherwise null.</returns>
        public RoadJourney ReturnRoadJourney()
        {
            RoadJourney roadJourneyToReturn = null;

            if  (returnRoadJourney == null)
                return null;

            // find the congested return road journey
            foreach (RoadJourney roadJourney in returnRoadJourney)
            {
                if  (roadJourney.Type == TDJourneyType.RoadCongested)
                {
                    roadJourneyToReturn = roadJourney;
                    break;
                }
            }

            return roadJourneyToReturn;
        }

        #endregion

        #region Public methods - Update road journey

        /// <summary>
		/// Updates the congested outward road journey with a RoadJourney object
		/// </summary>
		/// <param name="rj">Updated road journey object to replace</param>
		public void UpdateOutwardRoadJourney(RoadJourney rj)
		{
			if (rj != null)
			{
				dirty = true;

				for (int i = 0; i < outwardRoadJourney.Count; i++)
				{
					RoadJourney roadJourney = (RoadJourney)outwardRoadJourney[i];
					
					if  (roadJourney.Type == TDJourneyType.RoadCongested)
					{
						outwardRoadJourney[i] = rj;
						break;
					}
				}
			}
		}

		/// <summary>
		/// Updates the congested return road journey with a RoadJourney object
		/// </summary>
		/// <param name="rj">Updated road journey object to replace</param>
		public void UpdateReturnRoadJourney(RoadJourney rj)
		{
			if (rj != null)
			{
				dirty = true;

				for (int i = 0; i < returnRoadJourney.Count; i++)
				{
					RoadJourney roadJourney = (RoadJourney)returnRoadJourney[i];
					
					if  (roadJourney.Type == TDJourneyType.RoadCongested)
					{
						returnRoadJourney[i] = rj;
						break;
					}
				}
			}
        }

        #endregion

        public PublicJourney AmendedOutwardPublicJourney
        {
            get 
            { 
                return amendedOutwardPublicJourney; 
            }
            set 
            { 
                dirty = true;
                amendedOutwardPublicJourney = value;
                amendedOutwardPublicJourney.RouteNum = GetRouteNum();
            }
        }

        public PublicJourney AmendedReturnPublicJourney
        {
            get 
            { 
                return amendedReturnPublicJourney; 
            }
            set 
            { 
                dirty = true;
                amendedReturnPublicJourney = value; 
                amendedReturnPublicJourney.RouteNum = GetRouteNum(); 
            }
        }

        #region Public methods - Date times

        /// <summary>
        /// Read only property determining the return time of the journey
        /// </summary>
        public TDDateTime TimeReturn
        {
            get { return timeReturn; }
        }

        /// <summary>
        /// Read only property determining the outward time of the journey
        /// </summary>
        public TDDateTime TimeOutward
        {
            get { return timeOutward; }
        }

        /// <summary>
        /// Read only boolean value to determine if outward journey should arrive before the outward time
        /// </summary>
        public bool ArriveBeforeOutward
        {
            get { return arriveBeforeOutward; }
        }

        /// <summary>
        /// Read only boolean value to determine if return journey should arrive before the return time
        /// </summary>
        public bool ArriveBeforeReturn
        {
            get { return arriveBeforeReturn; }
        }

        #endregion

        /// <summary>
        /// Method to update the journey reference no and journey times when modifying the journey result already available.
        /// This method is used to update the replanned journey result in Real Time Information In Car.
        /// </summary>
        /// <param name="journeyReferenceNumber"></param>
        /// <param name="startRouteNum"></param>
        /// <param name="timeO"></param>
        /// <param name="timeR"></param>
        /// <param name="arriveBeforeOutward"></param>
        /// <param name="arriveBeforeReturn"></param>
        public void UpdateJourneyReferenceNoAndTime(int journeyReferenceNumber, TDDateTime timeO, TDDateTime timeR, bool arriveBeforeOutward, bool arriveBeforeReturn)
        {
            this.journeyReferenceNumber = journeyReferenceNumber;
            this.timeOutward = timeO;
            this.timeReturn = timeR;
            this.arriveBeforeOutward = arriveBeforeOutward;
            this.arriveBeforeReturn = arriveBeforeReturn;
        }

        /// <summary>
        /// Tests if the end time for any outward journey (public or private) exceeds the
        /// start time of any return journey (public or private)
        /// </summary>
        /// <param name="journeyRequest">The original journey request</param>
        /// <returns>True if any outward journey end time exceeds the start of any
        /// return journey start time or false otherwise</returns>
        public bool CheckForReturnOverlap(ITDJourneyRequest journeyRequest) 
        {
            bool overlap = false;
            
            if (journeyRequest != null) 
            {
                // The following algorithm compares all outward end journey times against
                // all return journey start times. If any end time is greater than
                // any start time, overlap is set to true.

                // Check the start time of every public return journey against the end time
                // of every outward public journey and the outward private journey
                for (int retIndex = 0; retIndex < returnPublicJourney.Count && !overlap; retIndex++) 
                {
                    PublicJourneyDetail[] retLegs = ((PublicJourney)returnPublicJourney[retIndex]).Details;
                    
                    // First check all outward public journeys against current return public journey
                    for (int outIndex = 0; outIndex < outwardPublicJourney.Count && !overlap; outIndex++) 
                    {
                        PublicJourneyDetail[] outLegs = ((PublicJourney)outwardPublicJourney[outIndex]).Details;
                        overlap = (outLegs[outLegs.Length-1].LegEnd.ArrivalDateTime > retLegs[0].LegStart.DepartureDateTime);
                    }
                    // Next check the outward private journey against current return public journey
                    if (!overlap) 
                    {
                        RoadJourney outwardRoadJourney = OutwardRoadJourney();
                        if (outwardRoadJourney != null)
                        {
                            TDDateTime endTime = outwardRoadJourney.CalculateEndTime(journeyRequest.OutwardDateTime[0], journeyRequest.OutwardArriveBefore);
                            overlap = endTime > retLegs[0].LegStart.DepartureDateTime;
                        }
                    }
                }

                // Check the start time of the return private journey against the end time
                // of every outward public journey
                RoadJourney returnRoadJourney = ReturnRoadJourney();
                
                if (returnRoadJourney != null) 
                {
                    for (int outIndex = 0; outIndex < outwardPublicJourney.Count && !overlap; outIndex++) 
                    {
                        PublicJourneyDetail[] outLegs = ((PublicJourney)outwardPublicJourney[outIndex]).Details;
                        TDDateTime startTime = returnRoadJourney.CalculateStartTime(journeyRequest.ReturnDateTime[0], journeyRequest.ReturnArriveBefore);
                        overlap = (outLegs[outLegs.Length-1].LegEnd.ArrivalDateTime > startTime);
                    }
                }

                // Finally check the start time of the return private journey against the
                // end time of the outward private journey
                if (!overlap) 
                {
                    RoadJourney outRoadJourney = OutwardRoadJourney();
                    RoadJourney retRoadJourney = ReturnRoadJourney();
                    
                    if (outRoadJourney != null && retRoadJourney != null) 
                    {
                        TDDateTime outEndTime = outRoadJourney.CalculateEndTime(journeyRequest.OutwardDateTime[0], journeyRequest.OutwardArriveBefore);
                        TDDateTime retStartTime = retRoadJourney.CalculateStartTime(journeyRequest.ReturnDateTime[0], journeyRequest.ReturnArriveBefore);
                        overlap = outEndTime > retStartTime;
                    }
                }
            }
            return overlap;
        }

        /// <summary>
        /// Tests if any of the journeys returned start in the past
        /// </summary>
        /// <param name="journeyRequest">The original journey request</param>
        /// <returns>True if any journey start time is in the past</returns>                
        public bool CheckForJourneyStartInPast(ITDJourneyRequest journeyRequest)
        {
    
            //date time variable to hold current date and time
            TDDateTime timeNow = TDDateTime.Now;            

            if (journeyRequest != null) 
            {
                //do the following checks for a journey that starts in the past             
                //check all outward public journeys 
                for (int outIndex = 0; outIndex < outwardPublicJourney.Count; outIndex++) 
                {
                    PublicJourneyDetail[] outLegs = ((PublicJourney)outwardPublicJourney[outIndex]).Details;
                    
                    //check depart time of first leg
                    if (outLegs[0].LegStart.DepartureDateTime < timeNow)
                    {                               
                        return true;            
                    }
                }               

                //check all return public journeys 
                for (int retIndex = 0; retIndex < returnPublicJourney.Count; retIndex++) 
                {
                    PublicJourneyDetail[] retLegs = ((PublicJourney)returnPublicJourney[retIndex]).Details;
                    
                    //check depart time of first leg
                    if (retLegs[0].LegStart.DepartureDateTime < timeNow)
                    {
                        return true;    
                    }
                }

                //check outward road journey
                RoadJourney outwardRoadJourney = OutwardRoadJourney();
                
                if (outwardRoadJourney != null)
                {
                    TDDateTime outStartTime = outwardRoadJourney.CalculateStartTime(journeyRequest.OutwardDateTime[0], journeyRequest.OutwardArriveBefore);
                    if (outStartTime < timeNow)
                    {
                        return true;                        
                    }
                }

                //check return road journey
                RoadJourney retRoadJourney = ReturnRoadJourney();
                
                if (retRoadJourney != null) 
                {
                    TDDateTime retStartTime = retRoadJourney.CalculateStartTime(journeyRequest.ReturnDateTime[0], journeyRequest.ReturnArriveBefore);
                    if (retStartTime < timeNow)
                    {
                        return true;                    
                    }
                }               
            }
            return false;
        }

        /// <summary>
        /// Creates a unique route number for each route within a TDJourneyResult.
        /// </summary>
        /// <returns>The next integer a unary series, starting with 1</returns>
        private int GetRouteNum()
        {
            return ++routeNum;
        }

        #region Public methods - Messages

        /// <summary>
        /// Parses the array of messages returned by the CJP and logs them all 
        /// (configurable via a property), add those that are displayable 
        /// to an array for eventual presentation to the user. Note that  
        /// messages indicating a technical faiure by the CJP do not need
        /// anything adding to the message array here, because the absence 
        /// of any journeys will cause CJPManager to add an "unable to find 
        /// journeys at this time" msg to the array.  
        /// </summary>
        private void AddMessages(Message[] messagesFromCjp, bool outward)
        {
            
            if  (messagesFromCjp == null || messagesFromCjp.Length == 0)
            {
                return;
            }

            IPropertyProvider pp = Properties.Current;

            bool logNoJourneyResponses  = (pp[JourneyControlConstants.LogNoJourneyResponses]  == "Y");
            bool logJourneyWebFailures  = (pp[JourneyControlConstants.LogJourneyWebFailures]  == "Y");
            bool logTTBOFailures        = (pp[JourneyControlConstants.LogTTBOFailures]        == "Y");
            bool logCJPFailures         = (pp[JourneyControlConstants.LogCJPFailures]         == "Y");
            bool logRoadEngineFailures  = (pp[JourneyControlConstants.LogRoadEngineFailures]  == "Y");
            bool logDisplayableMessages = (pp[JourneyControlConstants.LogDisplayableMessages] == "Y");
            
            foreach (Message msg in messagesFromCjp)
            {
                if  (msg.code == JourneyControlConstants.CjpOK 
                        || msg.code == JourneyControlConstants.CjpRoadEngineOK)     // OK - no need to do anything
                {
                    continue;
                }

                if  (msg.code == JourneyControlConstants.CjpNoPublicJourney  ||
                     msg.code == JourneyControlConstants.CjpJourneysRejected ||
                     msg.code == JourneyControlConstants.TTBONoTimetableServiceFound)
                {
                    // Flag that no public journeys were found by the CJP,
                    //  but also that this is not necessarily an error ...
                    this.correctReturnNoPublicJourney = true;
                }
                
				JourneyWebMessage journeyWebMessage = msg as JourneyWebMessage; 
				
				if (journeyWebMessage != null)
				{

					if  (msg.code == JourneyControlConstants.JourneyWebMajorNoResults 
						&& journeyWebMessage.subClass == JourneyControlConstants.JourneyWebMinorFuture)
					{
						if  (logDisplayableMessages)
						{
							LogReturnedMessage(JourneyControlConstants.JourneyWebText, msg.description, msg.code, journeyWebMessage.subClass);
						}

						AddMessageToArray(string.Empty, JourneyControlConstants.JourneyWebTooFarAhead, msg.code, journeyWebMessage.subClass);
					}
					else if (msg.code == JourneyControlConstants.JourneyWebMajorGeneral 
						&& journeyWebMessage.subClass == JourneyControlConstants.JourneyWebMinorDisplayable)
					{
						if  (logDisplayableMessages)
						{
							LogReturnedMessage(JourneyControlConstants.JourneyWebText, msg.description, msg.code, journeyWebMessage.subClass);
						}

						AddMessageToArray(string.Empty, JourneyControlConstants.JourneyWebNoResults	, msg.code, journeyWebMessage.subClass);

					}
					else
					{
						if  (logJourneyWebFailures)
						{
							LogReturnedMessage(JourneyControlConstants.JourneyWebText, msg.description,  msg.code, journeyWebMessage.subClass);
						}
					}
				}
				else 
				{
					TTBOMessage ttboMessage = msg as TTBOMessage;		
					
					if (ttboMessage != null)
					{
						if  (logTTBOFailures)
						{
							LogReturnedMessage(JourneyControlConstants.TTBOText, msg.description, msg.code, ttboMessage.subCode);
						}
					}
					else
					{
						if  (msg.code < JourneyControlConstants.CjpRoadEngineMin 
							|| msg.code > JourneyControlConstants.CjpRoadEngineMax) 
						{
							if  (logCJPFailures)
							{
								if  (!this.correctReturnNoPublicJourney || logNoJourneyResponses)
								{
									LogReturnedMessage(JourneyControlConstants.CJPText, msg.description, msg.code, -1);
								}
							}
						}
						else if (logRoadEngineFailures)
						{
							LogReturnedMessage(JourneyControlConstants.RoadEngineText, msg.description, msg.code, -1);
						}   
					}
				}


			}
		}


        // Array is of displayable messages, so we don't 
        //   want multiple messages with the same text ...
		public void AddMessageToArray(string description, string resourceId, int majorCode, int minorCode)
		{
			dirty = true;
			foreach (CJPMessage msg in messageList)
			{
				if  (msg.MessageText == description && msg.MessageResourceId == resourceId) 
				{
					return;
				}
			}

			messageList.Add(new CJPMessage(description, resourceId, majorCode, minorCode));
		}

        public void AddMessageToArray(string description, string resourceId, int majorCode, int minorCode, ErrorsType type)
        {
            dirty = true;
            foreach (CJPMessage msg in messageList)
            {
                if  (msg.MessageText == description && msg.MessageResourceId == resourceId) 
                {
                    return;
                }
            }

            messageList.Add(new CJPMessage(description, resourceId, majorCode, minorCode, type));
        }

        /// <summary>
        /// Clears the current list of messages.
        /// </summary>
        public void ClearMessages()
        {
            dirty = true;
            messageList.Clear();
        }

        private void LogReturnedMessage(string type, string originalMessage, int majorCode, int minorCode)
        {
            StringBuilder logMsg = new StringBuilder();
 
            logMsg.Append("CJP reported " + type + " message - ");
            logMsg.Append("text = " + originalMessage);
            logMsg.Append(", code = " + majorCode.ToString());
            
            if  (minorCode >= 0) 
            {
                logMsg.Append("." + minorCode.ToString());
            }

            logMsg.Append(", ref = " + journeyReferenceNumber.ToString());
            
            OperationalEvent operationalEvent = new OperationalEvent
                (TDEventCategory.CJP, TDTraceLevel.Error, logMsg.ToString());
            
            Logger.Write(operationalEvent);
        }

        #endregion

        #region Private methods

        private void HandOffJourneyToEsriMap(PrivateJourney journey, int routeNum, string sessionId, bool ignoreCongestion, int congestionValue)
        {
            ITDMapHandoff handoff = (ITDMapHandoff)TDServiceDiscovery.Current[ServiceDiscoveryKey.TDMapHandoff];

            ArrayList linkList = new ArrayList();
            
            foreach (Section section in journey.sections)
            {
                // Stop over sections are nodes and not added to the journey 
                // to draw.

                //Drive Section
                if  (section.GetType() == typeof(DriveSection))
                {
                    DriveSection ds = section as DriveSection;
                    if( ds != null )
                    {
                        linkList.AddRange(ds.links);    
                    }
                }   // Junction Drive Section
                else if (section.GetType() == typeof(JunctionDriveSection))
                {
                    JunctionDriveSection jds = section as JunctionDriveSection;
                    if ( jds != null ) 
                    {
                        linkList.AddRange(jds.links);
                    }
                }
            }

            ITNLink[] links = (ITNLink[])linkList.ToArray(typeof(ITNLink));

            if (ignoreCongestion)
            {
                handoff.SaveJourneyResult(false, sessionId, routeNum, links, congestionValue);
                
            }
            else
            {
                handoff.SaveJourneyResult(journey.congestion, sessionId, routeNum, links);
            }
        }

        #endregion

        #region ITDSessionAware implementation
        public bool IsDirty
        {
            get { return dirty; }
            set { dirty = value; }
        }
        #endregion

    }

    #region Comparers

    /// <summary>
    /// Comparer for sorting a PublicJourney array.  Sorts by reverse order of
    /// departime time (latest departure first).  If two journeys have the same
    /// departure time, then the duration is of the journey is used to distinguish
    /// between the two journeys (shortest first).
    /// </summary>
    public sealed class ArriveBeforeComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            PublicJourney journeyOne = (PublicJourney)x;
            PublicJourney journeyTwo = (PublicJourney)y;
 
            if(journeyOne.Details[0].LegStart.DepartureDateTime.CompareTo(journeyTwo.Details[0].LegStart.DepartureDateTime) > 0)
                return -1;
            else if(journeyOne.Details[0].LegStart.DepartureDateTime.CompareTo(journeyTwo.Details[0].LegStart.DepartureDateTime) < 0)
                return 1;
            else
            {
                // Add the duration for each of the legs in the both journeys for comparison
                long journeyOneDuration = 0;
                long journeyTwoDuration = 0;

                foreach(PublicJourneyDetail detail in journeyOne.Details)
                    journeyOneDuration += detail.Duration;

                foreach(PublicJourneyDetail detail in journeyTwo.Details)
                    journeyTwoDuration += detail.Duration;

                // If durations are the same, return the value of comparing the JourneyIndexes,
                // as these are definately going to differ. This will ensure there is always a
                // defined sort order.
                int compareResult = journeyOneDuration.CompareTo(journeyTwoDuration);
                if (compareResult == 0)
                    return journeyOne.JourneyIndex.CompareTo(journeyTwo.JourneyIndex);
                else
                    return compareResult;
            }
        }
    }

    /// <summary>
    /// Comparer for sorting a PublicJourney array.  Sorts by order of
    /// arrival time (earliest arrival time first).  If two journeys have the same
    /// arrival time, then the duration is of the journey is used to distinguish
    /// between the two journeys (shortest first).
    /// </summary>
    public sealed class DepartAfterComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            PublicJourney journeyOne = (PublicJourney)x;
            PublicJourney journeyTwo = (PublicJourney)y;

            // Get the index of the last leg of journey one
            int journeyOneLastIndex = journeyOne.Details.Length - 1;
            int journeyTwoLastIndex = journeyTwo.Details.Length - 1;

            if (journeyOne.Details[journeyOneLastIndex].LegEnd.ArrivalDateTime.CompareTo(journeyTwo.Details[journeyTwoLastIndex].LegEnd.ArrivalDateTime) > 0)
                return 1;
            else if (journeyOne.Details[journeyOneLastIndex].LegEnd.ArrivalDateTime.CompareTo(journeyTwo.Details[journeyTwoLastIndex].LegEnd.ArrivalDateTime) < 0)
                return -1;
            else
            {
                // Add the duration for each of the legs in the both journeys for comparison
                long journeyOneDuration = 0;
                long journeyTwoDuration = 0;

                foreach (PublicJourneyDetail detail in journeyOne.Details)
                    journeyOneDuration += detail.Duration;

                foreach (PublicJourneyDetail detail in journeyTwo.Details)
                    journeyTwoDuration += detail.Duration;

                // Compare the durations
                int compareResult = journeyOneDuration.CompareTo(journeyTwoDuration);

                // If durations are the same, return the value of comparing the JourneyIndexes,
                // as these are definately going to differ. This will ensure there is always a
                // defined sort order.
                if (compareResult == 0)
                    return journeyOne.JourneyIndex.CompareTo(journeyTwo.JourneyIndex);
                else
                    return compareResult;
            }
        }
    }

    #endregion
}
