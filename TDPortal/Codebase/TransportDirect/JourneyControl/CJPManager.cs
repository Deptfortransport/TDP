// ***********************************************
// NAME         : CJPManager.cs
// AUTHOR       : Andrew Toner
// DATE CREATED : 10/08/2003
// DESCRIPTION  : Implementation of the CJPManager class
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/CJPManager.cs-arc  $
//
//   Rev 1.10   Jan 31 2013 14:41:42   mmodi
//Check for null accessible prefs before logging accessible event
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.9   Jan 28 2013 15:36:58   dlane
//Code correction
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.8   Jan 28 2013 15:28:18   DLane
//New event types
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.7   Jan 22 2013 16:48:46   DLane
//Accessible events
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.6   Dec 05 2012 14:16:30   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.5   Sep 21 2011 09:53:16   mmodi
//Corrected to show last car journey planned when no more routes using the replan to avoid incidents
//Resolution for 5739: Real Time In Car - Failed journey Replan does not display last journey
//
//   Rev 1.4   Sep 01 2011 10:43:16   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.3   Oct 12 2009 09:10:54   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.3   Oct 12 2009 08:42:52   apatel
//EBC Map and Printer Friendly pages related chages
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.3   Oct 12 2009 08:39:42   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Jun 20 2008 13:26:16   pscott
//SCR 5026 USD 2720245 - errormessages and screen display incorrect when outward journey has failed but the return journey hasn't.   Converse of this also applies.
//
//   Rev 1.1   Nov 15 2007 12:06:32   mturner
//Changed after bug fix for 'anytime' journeys for today.  Problem was caused by .Net 2 handling DateTime.Now() differently to 1.1.
//
//   Rev 1.0   Nov 08 2007 12:23:38   mturner
//Initial revision.
//
//   Rev 1.80   Apr 20 2006 17:25:38   RPhilpott
//Get ride of warnings caused by unused private variables.
//
//   Rev 1.79   Mar 30 2006 13:35:38   mguney
//Manual merge for stream0018
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.78   Mar 14 2006 10:40:42   RGriffith
//Manual merge for Stream3353 
//
//   Rev 1.77   Dec 21 2005 15:35:06   esevern
//IR3369: added additional cjp error message texts to journey result.
//Resolution for 3369: Update Error messages
//
//   Rev 1.76.1.1   Mar 02 2006 17:40:46   NMoorhouse
//Extra param to hold private via location
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.76.1.0   Jan 26 2006 20:00:58   rhopkins
//Additional properties passed to TDJourneyResult, used when creating RoadJourneys
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.76   Oct 31 2005 11:59:18   tmollart
//Merge with stream 2638.
//Resolution for 2929: Visit Planner Merge Activity
//
//   Rev 1.75   Oct 26 2005 16:04:42   kjosling
//Updates following code review
//
//   Rev 1.74   Oct 26 2005 14:03:40   kjosling
//Implemented workaround to combat thread pool corruption. 
//Resolution for 2752: Del 7.1 - For some journeys the wait page is displayed indefinitely
//
//   Rev 1.73   Oct 19 2005 13:28:22   PNorell
//Added the MTAThread attribute as fix corresponding to Microsofts change-request.
//Resolution for 2752: Del 7.1 - For some journeys the wait page is displayed indefinitely
//
//   Rev 1.72   Aug 24 2005 16:04:34   RPhilpott
//Changes to allow OSGR of first or last location to be replaced by OSGR of request origin/destination in rare case where firs tor last naptan in journey has no supplied OSGR and is not in Stops database.
//Resolution for 2662: DN062:  Map with non-consecutive nodes with no co-ordinates functioning incorrectly
//
//   Rev 1.71   Aug 19 2005 14:03:42   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.70.1.3   Aug 18 2005 12:11:40   RPhilpott
//Move DRT mode addition so that all modes appear in logged request.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.70.1.2   Jul 27 2005 10:13:46   RWilby
//Changes to comply with FxCop
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.70.1.1   Jul 25 2005 13:11:46   RWilby
//Changed PopulateCJPJourneyRequest method to Add ModeType.Drt to modes 
//array if modes includes ModeType.Bus
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.70.1.0   Jul 12 2005 10:29:00   RPhilpott
//Get all intermediate stations for d2d/trunk.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.70   May 17 2005 16:46:50   rscott
//Updated with changes from code review IR1936.
//PopulateMultiModalParameters - vias, notVias and softVias population rewritten. 
//
//   Rev 1.69   May 12 2005 11:10:58   rscott
//Changes made for code review IR1936
//
//   Rev 1.68   Apr 19 2005 18:06:38   jmorrissey
//Fixed bug whereby an attempt is made to evaluate 'request.OutwardDateTime.Length == 0' when a return is not required.
//
//   Rev 1.67   Apr 13 2005 12:18:40   Ralavi
//Rounding up decimal values from fuel consumption and fuel cost into integers
//
//   Rev 1.66   Apr 01 2005 13:36:56   Ralavi
//Changes to convert fuel cost to correct value before passing to CJP
//
//   Rev 1.65   Mar 30 2005 17:35:50   RAlavi
//Include roads have been added to pass to CJP
//
//   Rev 1.64   Mar 16 2005 19:30:00   abork
//CJP cannot use empty Service Filters
//
//   Rev 1.63   Mar 16 2005 10:47:48   RAlavi
//Passed fuel consumption and fuel cost values to CJP
//
//   Rev 1.62   Mar 09 2005 09:38:10   RAlavi
//Added code for car costing
//
//   Rev 1.61   Mar 01 2005 16:55:34   rscott
//Del 7 - Cost Based Search Incremental Design Changes
//
//   Rev 1.60   Feb 23 2005 16:40:34   rscott
//DEL7 Update - TDDateTime OutwardDateTime and ReturnDateTime changed to TDDateTime[] arrays.
//
//   Rev 1.59   Jan 19 2005 14:45:22   RScott
//DEL 7 - PublicViaLocation removed and PublicViaLocations[ ], PublicSoftViaLocations[ ], PublicNotViaLocations[] added.
//
//   Rev 1.58   Nov 25 2004 11:18:40   jgeorge
//Modification to PopulateFlightParameters method to set Public Algorithm according to direct flights only property of TDJourneyRequest object.
//Resolution for 1785: City to City should only plan direct flights
//
//   Rev 1.57   Nov 03 2004 16:15:52   jgeorge
//Bug fix - added code to populate request ID variable to ensure that the request ID is written in the JourneyPlanResultsEvent.
//Resolution for 1729: Problems with RoadPlanEvents after release 6.2
//
//   Rev 1.56   Sep 21 2004 17:50:06   COwczarek
//CallCJP method now takes isExtension parameter that
//indicates if journey request is for planning a journey extension.
//Error message text is now determined by whether journey
//request if for an extension or not.
//Resolution for 1263: Unhelpful user friendly error message for extend results
//
//   Rev 1.55   Sep 17 2004 15:12:58   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.54   Sep 14 2004 18:20:08   RPhilpott
//Correct bugs in CJP error handling, and include TTBO "no timetable found" in "normal" no journey conditions.
//Resolution for 1547: Why are these CJP error messages different?
//Resolution for 1557: Inappropriate error msgs when no air journeys found
//
//   Rev 1.53   Sep 13 2004 16:15:36   RHopkins
//IR1484 Add new attributes to the JourneyRequest for ReturnOriginLocation and ReturnDestinationLocation to allow Extensions to be made to/from Return locations that differ from the corresponding Outward location.
//
//   Rev 1.52   Sep 08 2004 15:48:18   jgeorge
//Bug fix - userType variable was never being set so the user type was not being passed through to the CJP, meaning that narrative logs were not produced. Added code to set the value correctly. 
//Resolution for 1546: CJP narrative logs are not being produced
//
//   Rev 1.51   Sep 03 2004 14:46:42   RPhilpott
//Change flightPlan to trunkPlan to allow for coach trunk planning.
//Resolution for 1418: Find Coach - support for interval searches
//
//   Rev 1.50   Sep 02 2004 10:09:44   RPhilpott
//No longer necessary to use "Error" level logging to force out CJP Request and Result OE's - reverted to Verbose, and added override.
//
//   Rev 1.49   Aug 31 2004 15:18:28   acaunt
//Comments added indicate that logging at Exception level is deliberate.
//
//   Rev 1.48   Aug 25 2004 17:43:36   RPhilpott
//Get minimum logging user type level from properties.
//Resolution for 1438: Enabling log viewing of CJP requests/results
//
//   Rev 1.47   Aug 25 2004 15:27:42   RPhilpott
//Enable user-type driven for userType > 0, and strip out leading spaces to conserve logging space and allow us to see more of message text.
//Resolution for 1438: Enabling log viewing of CJP requests/results
//
//   Rev 1.46   Aug 24 2004 16:36:10   RPhilpott
//1) First part of chnages to support for interval-based coach searches - add bus and ferry modes.
//
//2) Minor changes to CJP Request/Response logging.
//Resolution for 1418: Find Coach - support for interval searches
//
//   Rev 1.45   Aug 20 2004 12:39:48   RPhilpott
//1) Remove temporary locality fixes
//
//2) Ensure interval on trunk requests never exceeds 23:59:59.
//Resolution for 1330: CJP only returns 4 journeys for Find A journey plans
//Resolution for 1404: No locality passed to CJP for Find-A-Flight requests
//
//   Rev 1.44   Aug 18 2004 14:35:08   CHosegood
//Temporary fix so testing can view air journeys
//
//   Rev 1.43   Aug 17 2004 15:45:40   RPhilpott
//Short-term fix for system testing -- hard-coded localities passed to CJP for FindAFlight requests.
//
//   Rev 1.42   Aug 10 2004 19:46:02   RPhilpott
//Pass isVia to TDLocation.ToRequestPlace(), to support changing airport Naptan prefix for non-stopver vias on find-a-flight.
//
//   Rev 1.41   Aug 10 2004 17:57:44   RPhilpott
//Correct handling of trunk journey times for car journeys.
//
//   Rev 1.40   Aug 09 2004 13:47:12   RPhilpott
//1) Only pass required Naptans/TOIDs to CJP
//2) Small error in error handling
//
//   Rev 1.39   Aug 02 2004 14:19:34   RPhilpott
//For trunk journeys, only pass Naptans of appropriate statiion type for this mode to the CJP.
//
//   Rev 1.38   Jul 30 2004 17:05:36   RPhilpott
//Minor error handling bug
//
//   Rev 1.37   Jul 28 2004 15:47:00   RPhilpott
//Unit test error corrections.
//
//   Rev 1.36   Jul 28 2004 11:38:38   RPhilpott
//Major rewrite to support multiple parallel CJP calls for Del 6.1 trunk planning.
//
//   Rev 1.35   Jul 02 2004 13:39:14   jgeorge
//Changes for user type
//
//   Rev 1.34   Jun 25 2004 12:25:10   RPhilpott
//Make support for operator selection accessible to mult-modal journeys as well as flights.
//
//   Rev 1.33   Jun 23 2004 14:47:20   RPhilpott
//Completion of Find-A-Flight unit test.
//
//   Rev 1.32   Jun 18 2004 14:56:26   RPhilpott
//Find-A-Flight validation - interim check-in.
//
//   Rev 1.31   Jun 16 2004 17:59:30   RPhilpott
//Find-A-Flight changes - interim check-in.
//
//   Rev 1.30   May 25 2004 11:31:34   RPhilpott
//Remove "bodge congestion" handling - no congested journey now implies an error.
//Resolution for 915: Bodged congested journeys should no longer be created.
//
//   Rev 1.29   Nov 19 2003 15:21:42   PNorell
//Ensured that we have two of the CJP for our calls involving a return.
//
//   Rev 1.28   Nov 14 2003 19:44:06   kcheung
//Updated error message display.
//Resolution for 112: Results - Summary: Remove error message
//
//   Rev 1.27   Nov 04 2003 15:19:26   RPhilpott
//Add extra exception logging on EndInvoke calls.
//
//   Rev 1.26   Oct 22 2003 12:20:28   RPhilpott
//Improve CJP error handling
//
//   Rev 1.25   Oct 21 2003 18:15:26   RPhilpott
//Correct handling of arrive/depart times for return journeys.
//
//   Rev 1.24   Oct 16 2003 17:05:56   RPhilpott
//Avoid null reference in AdditionalLocations
//
//   Rev 1.23   Oct 15 2003 14:36:24   RPhilpott
//Correct handling of alternate locations
//
//   Rev 1.22   Oct 15 2003 13:30:08   PNorell
//Updates to get the correct journey time to show.
//
//   Rev 1.21   Oct 13 2003 12:18:40   RPhilpott
//Add request for intermediate locations (within leg) for P/T jpurney
//
//   Rev 1.20   Oct 10 2003 19:42:44   PNorell
//Fix for routenum to make it less likely to occur for the same user.
//
//   Rev 1.19   Oct 09 2003 10:20:54   RPhilpott
//Do not include null via locations in search
//
//   Rev 1.18   Oct 08 2003 19:27:02   PNorell
//Removed 119 in drive speed.
//
//   Rev 1.17   Oct 08 2003 17:56:02   RPhilpott
//Fixes for CJP input parameters
//
//   Rev 1.16   Sep 26 2003 20:59:18   RPhilpott
//CJPManager test harness for non-NUnit testing.
//
//   Rev 1.15   Sep 25 2003 20:32:04   RPhilpott
//Minor fixes.
//
//   Rev 1.14   Sep 25 2003 11:44:36   RPhilpott
//Map handoff and MI logging changes
//
//   Rev 1.13   Sep 24 2003 18:23:28   geaton
//Changes needed to support new version of JourneyPlanResultEvent class.
//
//   Rev 1.12   Sep 24 2003 17:34:30   RPhilpott
//More control over verbose logging, and fix origin/destn time bug
//
//   Rev 1.11   Sep 23 2003 15:28:30   RPhilpott
//Add some operational logging
//
//   Rev 1.10   Sep 23 2003 14:06:26   RPhilpott
//Changes to TDJourneyResult (ctor and referenceNumber)
//
//   Rev 1.9   Sep 20 2003 19:24:48   RPhilpott
//Support for passing OSGR's with NaPTAN's, various other fixes
//
//   Rev 1.8   Sep 19 2003 15:23:40   RPhilpott
//Move CjpStub to JourneyControl
//
//   Rev 1.7   Sep 10 2003 11:14:00   RPhilpott
//Changes to CJPMessage handling.
//
//   Rev 1.6   Sep 09 2003 13:37:52   RPhilpott
//Move Custom Events from TDPCustomEvents to JourneyControl to avoid circular dependency.
//
//   Rev 1.5   Sep 09 2003 10:03:48   RPhilpott
//Minor corrections.
//
//   Rev 1.4   Sep 08 2003 15:45:24   RPhilpott
//Split initial and amendment CallCJP calls into separate overloaded versions.
//
//   Rev 1.3   Aug 20 2003 17:55:42   AToner
//Work in progress


using System;
using System.IO;
using System.Threading;
using System.Collections;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;

using TransportDirect.JourneyPlanning.CJP;
using TransportDirect.JourneyPlanning.CJPInterface;

using TransportDirect.UserPortal.LocationService;
using TransportDirect.ReportDataProvider.TDPCustomEvents;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.JourneyControl
{
    /// <summary>
    /// Summary description for CJPManager.
    /// </summary>
    public class CJPManager : ICJPManager
    {
        #region Private members

        private bool publicRequired;
        private bool privateRequired;

        private int referenceNumber;
        private int lastSequenceNumber;
        private bool referenceTransaction;
        private string sessionId = string.Empty;
        private string language = string.Empty;
        private bool isExtension;

        private int userType;
        private bool loggedOn;

        #endregion 

        #region Constructor
        
        /// <summary>
        /// Constructor
        /// </summary>
        public CJPManager()
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// CallCJP handles the orchestration of the various calls to the Journey planner.
        /// This overloaded version handles initial requests.
        /// </summary>
        /// <param name="request">Encapsulates journey parameters</param>
        /// <param name="sessionID">Used for logging purposes only</param>
        /// <param name="referenceTransaction">True is this an SLA-monitoring transaction</param>
        /// <param name="loggedOn">True if the user is logged on (used for logging only)</param>
        /// <param name="language">Two-character ISO id ("en" or "cy") of the current UI language</param>
        /// <param name="isExtension">True if the request is for a journey extension, false otherwise</param>
        /// <returns>The results of the enquiry, including any error messages</returns>
        [MTAThread]
        public ITDJourneyResult CallCJP( ITDJourneyRequest  request,
            string sessionId,
            int userType,
            bool referenceTransaction,
            bool loggedOn,
            string language,
            bool isExtension)
        {
            return CallCJP(request,
                sessionId,
                userType,
                false,
                referenceTransaction,
                0,
                0,
                loggedOn,
                language,
                isExtension);
        }


        /// <summary>
        /// CallCJP handles the orchestration of the various calls to the Journey planner
        /// This overloaded version handles amendments to an existing journey.
        /// </summary>
        /// <param name="request">Encapsulates journey parameters</param>
        /// <param name="sessionID">Used for logging purposes only</param>
        /// <param name="referenceTransaction">True is this an SLA-monitoring transacation</param>
        /// <param name="referenceNumber">Returned by the initial enquiry</param>
        /// <param name="lastSequenceNumber">Incremented by calling code on each amendment request</param>
        /// <param name="loggedOn">True if the user is logged on (used for logging only)</param>
        /// <param name="language">Two-character ISO id ("en" or "cy") of the current UI language</param>
        /// <returns>The results of the enquiry, including any error messages</returns>
        [MTAThread]
        public ITDJourneyResult CallCJP( ITDJourneyRequest  request,
            string sessionId,
            int userType,
            bool referenceTransaction,
            int referenceNumber,
            int lastSequenceNumber,
            bool loggedOn,
            string language)
        {
            return CallCJP(request,
                sessionId,
                userType,
                true,
                referenceTransaction,
                referenceNumber,
                lastSequenceNumber,
                loggedOn,
                language,
                false);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Common private method called by both public overloads
        /// of CallCJP to do the real work of handling the CJP call.
        /// </summary>
        private ITDJourneyResult CallCJP( ITDJourneyRequest request,
            string sessionId,
            int userType,
            bool amendment,
            bool referenceTransaction,
            int referenceNumber,
            int lastSequenceNumber,
            bool loggedOn,
            string language,
            bool isExtension)
        {

            this.referenceNumber = referenceNumber;
            this.referenceTransaction = referenceTransaction;
            this.sessionId = sessionId;
            this.lastSequenceNumber = lastSequenceNumber;
            this.language = language;
            this.loggedOn = loggedOn;
            this.userType = userType;
            this.isExtension = isExtension;

            IPropertyProvider propertyProvider = Properties.Current;

            string requestId = string.Empty;

            bool cjpFailed = false;
            int  cjpFailureCount = 0;

            // These switches control operational logging of raw CJP inputs and outputs ...
            bool logAllRequests  = false;
            bool logAllResponses = false;

            // Since valid user types are 0, 1, 2 a high value here disables user-type driven
            //  logging (so logging of CJP requests controlled by trace level as normal)
            //  Not using the enumeration for this, because we don't want to introduce a
            //  dependency on SessionManager ...

            int minimumLoggingUser = 99;

			try
			{
				minimumLoggingUser = Int32.Parse(propertyProvider[JourneyControlConstants.MinLoggingUserType]);
			}
				// nothing to do here - just means property hasn't been set yet
			catch (ArgumentNullException)
			{
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}


            if  (userType < minimumLoggingUser)
            {
                logAllRequests  = (TDTraceSwitch.TraceVerbose) && (propertyProvider[JourneyControlConstants.LogAllRequests]  == "Y");
                logAllResponses = (TDTraceSwitch.TraceVerbose) && (propertyProvider[JourneyControlConstants.LogAllResponses] == "Y");
            }
            else
            {
                logAllRequests  = true;
                logAllResponses = true;
            }

            // if it is an amendment, the previous reference number
            //  will have been passed back in for reuse here, otherwise
            //  we need to get hold of a new one.

            if  (!amendment)
            {
                referenceNumber = SqlHelper.GetRefNumInt();
                this.referenceNumber = referenceNumber;
            }

			if  (ModeContains(request.Modes, ModeType.Car))
			{
				privateRequired = true;
			}
                        
			if  (request.Modes.Length > 1 || request.Modes[0] != ModeType.Car)
			{
				publicRequired = true;
			}

			JourneyRequestPopulator populator = JourneyRequestPopulatorFactory.GetPopulator(request);

			CJPCall[] cjpCallList = populator.PopulateRequests(referenceNumber, lastSequenceNumber, sessionId, 
				referenceTransaction, userType, language);			

            // Retrieve the formatted reference number from the first call object
            requestId = ((CJPCall)cjpCallList[0]).RequestId;

            WaitHandle[] wh = new WaitHandle[cjpCallList.Length];

            int callCount = 0;

            foreach (CJPCall cjpCall in cjpCallList)
            {
				LogRequest((TDJourneyRequest)request, cjpCall.RequestId, loggedOn, sessionId);

				if  (logAllRequests)
				{
					Logger.Write(new OperationalEvent(TDEventCategory.CJP,
						TDTraceLevel.Verbose,
						ConvertToXML(cjpCall.CJPRequest, cjpCall.RequestId),
						TDTraceLevelOverride.User));
				}

                wh[callCount] = cjpCall.InvokeCJP();

                if  (wh[callCount] == null)
                {
                    cjpFailed = true;
                }

                callCount++;
            }

            // This will force a user to have amend the same route 200 times before overflowing and
            // possibly have remnants of journey items from previous route when plotted on the map.
            TDJourneyResult result;
            if  (request.IsReturnRequired)
            {
                result = new TDJourneyResult(referenceNumber, referenceNumber * 200,
                    (request.OutwardDateTime.Length == 0 ? null : request.OutwardDateTime[0]),
                    (request.ReturnDateTime.Length == 0 ? null : request.ReturnDateTime[0]),
                    request.OutwardArriveBefore, request.ReturnArriveBefore,
                    (request.AccessiblePreferences != null ? request.AccessiblePreferences.Accessible : false));
			}
			else
			{
                result = new TDJourneyResult(referenceNumber, referenceNumber * 200,
                    (request.OutwardDateTime.Length == 0 ? null : request.OutwardDateTime[0]),
                    null,
                    request.OutwardArriveBefore, false,
                    (request.AccessiblePreferences != null ? request.AccessiblePreferences.Accessible : false));
			}

            int cjpTimeOut = Int32.Parse(propertyProvider[JourneyControlConstants.CJPTimeoutMillisecs]);

            // Wait for parallel CJP calls to finish.
            // This method will return when either:
            //  - all CJP calls have completed; or
            //  - the timeout period is exceeded.

            if  (!cjpFailed)
            {
				//KJ - Added code change for multithreaded journey planning. The code now uses WaitOne instead
				//of WaitAll for returning threads. The timeout is adjusted for remaining threads to 
				//ensure the overall timeout (cjpTimeOut) is enforced. For more information see Tracker
				//2752. This change is temporary until the pollution of the threadpool with STA threads can
				//be resolved. 

				string message = String.Empty;
				int startTime, endTime;

				if(TDTraceSwitch.TraceVerbose)
				{
					System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
					stringBuilder.Append("CJPManager Thread has returned, ref no ");
					stringBuilder.Append(referenceNumber.ToString());
					stringBuilder.Append(". Remaining Timeout is: ");
					message =  stringBuilder.ToString();
				}
				
				foreach(ManualResetEvent mh in wh)
				{
					startTime = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
					mh.WaitOne(cjpTimeOut, false);
					endTime = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
					cjpTimeOut = cjpTimeOut - (endTime - startTime);

					Logger.Write(new OperationalEvent
					    (TDEventCategory.Business, TDTraceLevel.Verbose, message + cjpTimeOut.ToString() ));
				}

				//(KJ) End of code change. The commented out code below represents the original code

                //WaitHandle.WaitAll(wh, cjpTimeOut, false);

                //Logger.Write(new OperationalEvent
                //    (TDEventCategory.Business, TDTraceLevel.Verbose, "CJPManager WaitAll has returned, ref no " + referenceNumber.ToString()));

                foreach (CJPCall cjpCall in cjpCallList)
                {
                    bool thisCjpCallFailed = true;

                    JourneyResult cjpResult = cjpCall.CJPResult;

                    if  (cjpCall.IsSuccessful)
                    {
                        if  (logAllResponses)
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.CJP,
                                TDTraceLevel.Verbose,
                                ConvertToXML(cjpResult, cjpCall.RequestId),
                                TDTraceLevelOverride.User));
                        }

						TDLocation publicViaLocation = null;
						TDLocation privateViaLocation = null;

						if  (request.PublicViaLocations != null && request.PublicViaLocations.Length > 0)
						{
							publicViaLocation = request.PublicViaLocations[0];
						}

						if (request.PrivateViaLocation != null)
						{
							privateViaLocation = request.PrivateViaLocation;
						}
                                              

						thisCjpCallFailed = !result.AddResult(cjpResult, !(cjpCall.IsReturnJourney), publicViaLocation, privateViaLocation,
                                                                request.OriginLocation, request.DestinationLocation, sessionId, request.IgnoreCongestion, request.CongestionValue);
                    }

                    if  (thisCjpCallFailed)
                    {
                        cjpFailureCount++;
                    }
                }
            }

            if  (publicRequired && privateRequired)
            {
				if (result.OutwardPublicJourneyCount == 0 && result.OutwardRoadJourneyCount > 0)
                {
                    if  (result.CJPValidError)
                    {
						if  (result.CJPMessages.Length == 0)
						{
							result.AddMessageToArray(string.Empty, JourneyControlConstants.GetCJPPartialReturnAmend(), 0, 0);
						}
                    }
					else
					{
						if  (result.CJPMessages.Length == 0)
						{
							result.AddMessageToArray(string.Empty, JourneyControlConstants.GetCJPPartialReturnPublic(), 0, 0);
						}
					}

                }
                else if (result.OutwardRoadJourneyCount == 0 && result.OutwardPublicJourneyCount > 0)
                {
					if(!result.CJPValidError)
					{
						if  (result.CJPMessages.Length == 0)
						{
                            result.AddMessageToArray(string.Empty, JourneyControlConstants.GetCJPPartialReturnRoad(), 0, 0);

                            // if private required and outward road journey not returned, check if the request is made to avoid 
                            // specific toids. If it is made to avoid specific toids in the road journey, display message specific to avoid toids in road journey
                            if (request.AvoidToidsOutward != null && request.AvoidToidsOutward.Length > 0)
                            {
                                result.AddMessageToArray(string.Empty, JourneyControlConstants.GetJourneyWebNoRoadResultsAvoidToids(), 0, 0);

                            }   
						}
					}
                }
            }
            else if  (publicRequired )
            {
                if  (result.OutwardPublicJourneyCount == 0)
                {
                    if  (result.CJPValidError)
                    {
                        if  (result.CJPMessages.Length == 0 )
                        {
                            if (result.ReturnPublicJourneyCount > 0 && request.IsReturnRequired)
                            {
                                result.AddMessageToArray(string.Empty, JourneyControlConstants.GetJourneyWebNoResultsOutward(isExtension), 0, 0);
                            }
                            else
                            {
                                result.AddMessageToArray(string.Empty, JourneyControlConstants.GetJourneyWebNoResults(isExtension), 0, 0);
                            }
                        }
                    }
                }
            }
            // if private required and outward road journey not returned, check if the request is made to avoid 
            // specific toids. If it is made to avoid specific toids in the road journey, display message specific to avoid toids in road journey
            else if (privateRequired)
            {
                if (result.OutwardRoadJourneyCount == 0)
                {
                    if (result.CJPMessages.Length == 0)
                    {
                        if (request.AvoidToidsOutward != null && request.AvoidToidsOutward.Length > 0)
                        {
                            result.AddMessageToArray(string.Empty, JourneyControlConstants.GetJourneyWebNoRoadResultsAvoidToids(), 0, 0);

                        }
                    }
                }
            }

            if  (request.IsReturnRequired)
            {
				
				if  (publicRequired && privateRequired)
				{
					if (result.ReturnPublicJourneyCount == 0 && result.ReturnRoadJourneyCount > 0)
					{
						if  (result.CJPValidError)
						{
							if  (result.CJPMessages.Length == 0)
							{
								result.AddMessageToArray(string.Empty, JourneyControlConstants.GetCJPPartialReturnAmend(), 0, 0);
							}
						}
						else 
						{
							if  (result.CJPMessages.Length == 0)
							{
								result.AddMessageToArray(string.Empty, JourneyControlConstants.GetCJPPartialReturnPublic(), 0, 0);
							}
						}
					}
					else if (result.ReturnRoadJourneyCount == 0 && result.ReturnPublicJourneyCount > 0)
					{
						if(!result.CJPValidError)
						{
							if  (result.CJPMessages.Length == 0)
							{
								result.AddMessageToArray(string.Empty, JourneyControlConstants.GetCJPPartialReturnRoad(), 0, 0);

                                // if private required and return road journey not returned, check if the request is made to avoid 
                                // specific toids. If it is made to avoid specific toids in the road journey, display message specific to avoid toids in road journey
                                if (request.AvoidToidsReturn != null && request.AvoidToidsReturn.Length > 0)
                                {
                                    result.AddMessageToArray(string.Empty, JourneyControlConstants.GetJourneyWebNoRoadResultsAvoidToids(), 0, 0);

                                }
							}
						}
					}
				}
				else if (publicRequired)
				{
					if  (result.ReturnPublicJourneyCount == 0)
					{
						if  (result.CJPValidError)
						{
							if  (result.CJPMessages.Length == 0)
							{
                                if (result.OutwardPublicJourneyCount > 0)
                                {
								    result.AddMessageToArray(string.Empty, JourneyControlConstants.GetJourneyWebNoResultsReturn(isExtension), 0, 0);
                                }
                                else
                                {
								    result.AddMessageToArray(string.Empty, JourneyControlConstants.GetJourneyWebNoResults(isExtension), 0, 0);
                                }
							}
						}
					}
				}
                // if private required and return road journey not returned, check if the request is made to avoid 
                // specific toids. If it is made to avoid specific toids in the road journey, display message specific to avoid toids in road journey
                else if (privateRequired)
                {
                    if (result.ReturnRoadJourneyCount == 0)
                    {
                        if (result.CJPValidError)
                        {
                            if (result.CJPMessages.Length == 0)
                            {
                                if (request.AvoidToidsReturn != null && request.AvoidToidsReturn.Length > 0)
                                {
                                    result.AddMessageToArray(string.Empty, JourneyControlConstants.GetJourneyWebNoRoadResultsAvoidToids(), 0, 0);

                                }
                            }
                        }
                    }
                }


            }

            if  (cjpFailed)
            {
                result.AddMessageToArray(string.Empty, JourneyControlConstants.CJPInternalError,
                    JourneyControlConstants.CjpCallError, 0);
            }
            else
            {
                if  (cjpFailureCount == cjpCallList.Length)
                {
                    if  (result.CJPMessages.Length == 0)
                    {
                        result.AddMessageToArray(string.Empty, JourneyControlConstants.CJPInternalError,
                            JourneyControlConstants.CjpCallError, 0);
                    }

                    cjpFailed = true;
                }
                else if (cjpFailureCount > 0)
                {
                    if  (result.CJPMessages.Length == 0)
                    {
                        result.AddMessageToArray(string.Empty, JourneyControlConstants.GetCJPPartialReturn(isExtension),
                            JourneyControlConstants.CjpCallError, 0);
                    }
                }
            }
            
            LogResponse(result, requestId, loggedOn, cjpFailed, sessionId);

            return result;
        }

        /// <summary>
        /// Log Request MIS event
        /// </summary>
        /// <param name="request"></param>
        /// <param name="requestId"></param>
        /// <param name="isLoggedOn"></param>
        /// <param name="sessionId"></param>
        private void LogRequest(TDJourneyRequest request, string requestId, bool isLoggedOn, string sessionId)
        {
            if  (CustomEventSwitch.On("JourneyPlanRequestEvent"))
            {
                JourneyPlanRequestEvent jpre = new JourneyPlanRequestEvent(requestId,
                    request.Modes,
                    isLoggedOn,
                    sessionId);
                Logger.Write(jpre);
            }

            if  (CustomEventSwitch.On("JourneyPlanRequestVerboseEvent"))
            {
                JourneyPlanRequestVerboseEvent jprve = new JourneyPlanRequestVerboseEvent(requestId,
                    request,
                    isLoggedOn,
                    sessionId);
                Logger.Write(jprve);
            }

            #region AccessibleEvent

            if (request.AccessiblePreferences != null &&
                (request.AccessiblePreferences.RequireStepFreeAccess || request.AccessiblePreferences.RequireSpecialAssistance))
            {
                AccessibleEventType aeType = AccessibleEventType.Unknown;

                if (request.AccessiblePreferences.RequireFewerInterchanges)
                {
                    if (request.AccessiblePreferences.RequireStepFreeAccess & request.AccessiblePreferences.RequireSpecialAssistance)
                    {
                        aeType = AccessibleEventType.StepFreeWithAssistanceFewerChanges;
                    }
                    else if (request.AccessiblePreferences.RequireStepFreeAccess)
                    {
                        aeType = AccessibleEventType.StepFreeFewerChanges;
                    }
                    else
                    {
                        aeType = AccessibleEventType.AssistanceFewerChanges;
                    }
                }
                else
                {
                    if (request.AccessiblePreferences.RequireStepFreeAccess & request.AccessiblePreferences.RequireSpecialAssistance)
                    {
                        aeType = AccessibleEventType.StepFreeWithAssistance;
                    }
                    else if (request.AccessiblePreferences.RequireStepFreeAccess)
                    {
                        aeType = AccessibleEventType.StepFree;
                    }
                    else
                    {
                        aeType = AccessibleEventType.Assistance;
                    }
                }

                AccessibleEvent ae = new AccessibleEvent(aeType, DateTime.Now, sessionId);
                Logger.Write(ae);
            }

            #endregion
        }

        /// <summary>
        /// Log Response MIS event
        /// </summary>
        /// <param name="result"></param>
        /// <param name="requestId"></param>
        /// <param name="isLoggedOn"></param>
        /// <param name="cjpFailed"></param>
        /// <param name="sessionId"></param>
        private void LogResponse(TDJourneyResult result, string requestId, bool isLoggedOn,
            bool cjpFailed, string sessionId)
        {

            if  (CustomEventSwitch.On("JourneyPlanResultsVerboseEvent"))
            {
                JourneyPlanResultsVerboseEvent jprve = new JourneyPlanResultsVerboseEvent(requestId,
                    result,
                    isLoggedOn,
                    sessionId);
                Logger.Write(jprve);
            }


            if  (CustomEventSwitch.On("JourneyPlanResultsEvent"))
            {

                JourneyPlanResponseCategory responseCategory;

                if  (cjpFailed)
                {
                    responseCategory = JourneyPlanResponseCategory.Failure;
                }
                else
                {
                    if  (result.OutwardPublicJourneyCount  == 0
                        && result.ReturnPublicJourneyCount == 0
                        && result.OutwardRoadJourneyCount  == 0
                        && result.ReturnRoadJourneyCount   == 0)
                    {
                        responseCategory = JourneyPlanResponseCategory.ZeroResults;
                    }
                    else
                    {
                        responseCategory = JourneyPlanResponseCategory.Results;
                    }
                }

                JourneyPlanResultsEvent jpre = new JourneyPlanResultsEvent(requestId,
                    responseCategory,
                    isLoggedOn,
                    sessionId);
                Logger.Write(jpre);
            }
        }

		/// <summary>
		/// Scan through an array of ModeTypes to determine 
		/// if a single specified ModeType is in the array
		/// </summary>
		/// <param name="modes">An array of ModeTypes</param>
		/// <param name="mode">ModeType to look for</param>
		/// <returns>True if the specified ModeType is found</returns>
		private bool ModeContains(ModeType[] modes, ModeType mode )
		{
			foreach (ModeType item in modes)
			{
				if  (item == mode)
				{
					return true;
				}
			}
			return false;
		}      

		/// <summary>
		/// Create an XML representtaion of the specified object,
		/// with leading whitespace trimmed, for logging purposes.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="requestId"></param>
		/// <returns>XML string, prefixed by request id</returns>
        private string ConvertToXML(object obj, string requestId)
        {
            XmlSerializer xmls = new XmlSerializer(obj.GetType());
            StringWriter sw = new StringWriter();
            xmls.Serialize(sw, obj);
            sw.Close();
            // strip out leading spaces to conserve space in logging ...
            Regex re = new Regex("\\r\\n\\s+");
            return (requestId + " " + re.Replace(sw.ToString(), "\r\n"));
        }

        #endregion
    }
}
