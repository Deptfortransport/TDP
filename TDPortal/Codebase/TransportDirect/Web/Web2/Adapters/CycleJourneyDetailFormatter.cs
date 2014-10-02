// *********************************************** 
// NAME			: CycleJourneyDetailFormatter.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 05 Aug 2008
// DESCRIPTION	: Wraps a CycleJourney object to provide easier formatting of cycle journey 
// details for presentation purposes. Subclasses will implement formatting for specific purposes e.g.
// web page output, email output and so on.
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/CycleJourneyDetailFormatter.cs-arc  $
//
//   Rev 1.22   Dec 07 2010 09:42:20   apatel
//updated to get first agregated way name
//Resolution for 5653: Cycle path name update issue - showing the full aggregated path name
//
//   Rev 1.21   Dec 06 2010 12:43:44   apatel
//Updated to show name of the first aggregated way found instead of showing name of the aggregated way
//Resolution for 5653: Cycle path name update issue - showing the full aggregated path name
//
//   Rev 1.20   Dec 01 2010 13:01:08   apatel
//Code updated to show aggregated name/number or cycle path type attribute name instead of  "unname path" and to show more details by default.
//Resolution for 5650: Cycle Planner - Path name updates
//
//   Rev 1.19   Jul 28 2010 11:16:46   mmodi
//Corrected logic for setting Path flag and text, to include checking the Interesting attributes
//Resolution for 5587: Cycling - Towpaths are not being shown as Unnamed path instructions
//
//   Rev 1.18   Jul 12 2010 12:05:06   mmodi
//Updated logic for detecting Path attribute using the Joining attributes list only
//Resolution for 5569: Cycle - DCS issue with 'Private Roads appear as paths?'
//
//   Rev 1.17   May 14 2010 10:11:14   mmodi
//Added Title attribute to cycle images to display hover over text in firefox
//Resolution for 5536: Del 10.10 - Accessability Issues from testing - update
//
//   Rev 1.16   Sep 29 2009 15:24:28   nrankin
//Accessibility - opens in new window
//Resolution for 5320: Accessibility - Opens in new window
//
//   Rev 1.15   Dec 10 2008 16:14:04   mmodi
//Updated to not show any other attribute text when it is a Crossing attribute detail
//Resolution for 5198: Cycle Planner - Crossing attribtue text should not display any other directions or attributes
//
//   Rev 1.14   Dec 09 2008 14:25:20   mmodi
//Updated to not show Ferry link on printable page
//Resolution for 5193: Cycle Planner - Ferry link is active on Printer friendly
//
//   Rev 1.13   Dec 05 2008 13:32:54   mmodi
//Updated to add up distance for Crossing direction text
//Resolution for 5198: Cycle Planner - Crossing attribtue text should not display any other directions or attributes
//
//   Rev 1.12   Nov 28 2008 10:59:02   mmodi
//Updated to check if cycle route number exists
//Resolution for 5190: Cycle Planner - Cycle route name no longer contains a number
//
//   Rev 1.11   Nov 26 2008 14:54:28   mmodi
//Updated to display Roundabout drive instructions
//Resolution for 5168: Cycle Planner - Roundabouts changes needed
//
//   Rev 1.10   Nov 25 2008 15:12:10   mmodi
//Crossing attribute text is now shown instead of the Detail direction instruction
//Resolution for 5182: Cycle Planner - Crossing instruction section should not display its drive direction
//
//   Rev 1.9   Nov 11 2008 15:21:38   mturner
//Added code to populate the CyclePlanner.CycleJourneyDetailsControl.ImmediateTurnDistance property and to use it accordingly.
//
//   Rev 1.8   Nov 05 2008 15:02:16   mturner
//Updated to display U-Turn text when the TurnAngle is "Return"
//
//   Rev 1.7   Oct 28 2008 17:08:10   mmodi
//Updated to display complex manoeuvres
//
//   Rev 1.6   Oct 20 2008 16:03:44   mmodi
//Added Charge "adult and cycle" text
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.5   Oct 20 2008 14:31:08   mmodi
//Updated attribute text code and displayed attribute value for CJP user
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   Oct 10 2008 15:35:00   mmodi
//Updated for Cycle attributes
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   Sep 15 2008 10:46:40   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Aug 22 2008 10:22:54   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Aug 08 2008 12:05:44   mmodi
//Updated as part of workstream
//
//   Rev 1.0   Aug 06 2008 14:57:18   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;

using TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService;

namespace TransportDirect.UserPortal.Web.Adapters
{
    public abstract class CycleJourneyDetailFormatter
    {
        #region Member Variables

        #region Variables
        // Used to control rendering of the step number in the table
        protected int stepNumber;

        // Keeps the accumulated distance of the journey currently being rendered.
        protected int accumulatedDistance;

        // Keeps the accumulated arrival time (updated after each leg)
        protected TDDateTime currentArrivalTime;

        // Constant representing space used in string formatting
        protected readonly static string space = " ";

        // Constant representing space used in string formatting
        protected readonly static string comma = ",";

        // Constant representing full stop used in string formatting
        protected readonly static string fullstop = ".";

        // Constant representing seperator used in string formatting
        protected readonly static string seperator = ", ";

        // Cycle journey to render
        protected CycleJourney cycleJourney;

        // Options pertaining to journey
        protected TDJourneyViewState journeyViewState;

        // True if journey details are for outward journey, false if return
        protected bool outward;

        // Road distance in metres within which "immediate" text should be 
        // prepended to journey direction
        protected int immediateTurnDistance;

        // slip road distance limit parameter
        protected int slipRoadDistance;

        // unit to display the distance in
        protected RoadUnitsEnum roadUnits;

        // flag to change display depending on if this is a printable page
        protected bool printable;

        // flag to set if the attributes are set to visible or hidden by default
        protected bool showAllDetails;

        // flag to set if the cycle instruction text is displayed
        protected bool showInstructionText;

        // flag to show the cycle attribute number returned
        protected bool showAttributeNumber;

        #endregion

        #region Strings that are used to generate the route text descriptions.

        protected string roundaboutExitOne = String.Empty;  //Take first available exit off roundabout on to
        protected string roundaboutExitTwo = String.Empty;
        protected string roundaboutExitThree = String.Empty;
        protected string roundaboutExitFour = String.Empty;
        protected string roundaboutExitFive = String.Empty;
        protected string roundaboutExitSix = String.Empty;
        protected string roundaboutExitSeven = String.Empty;
        protected string roundaboutExitEight = String.Empty;
        protected string roundaboutExitNine = String.Empty;
        protected string roundaboutExitTen = String.Empty;

        protected string continueString = String.Empty;

        protected string turnLeftOne = String.Empty;    //Take first available left on to
        protected string turnLeftTwo = String.Empty;
        protected string turnLeftThree = String.Empty;
        protected string turnLeftFour = String.Empty;
        protected string turnLeftOne2 = String.Empty;   //Take first available left
        protected string turnLeftTwo2 = String.Empty;
        protected string turnLeftThree2 = String.Empty;
        protected string turnLeftFour2 = String.Empty;

        protected string turnRightOne = String.Empty;   //Take first available right on to
        protected string turnRightTwo = String.Empty;
        protected string turnRightThree = String.Empty;
        protected string turnRightFour = String.Empty;
        protected string turnRightOne2 = String.Empty;  //Take first available right
        protected string turnRightTwo2 = String.Empty;
        protected string turnRightThree2 = String.Empty;
        protected string turnRightFour2 = String.Empty;

        protected string turnLeftInDistance = String.Empty; //Turn left on to
        protected string turnRightInDistance = String.Empty;

        protected string bearLeft = String.Empty;   //Bear left on to
        protected string bearRight = String.Empty;

        protected string immediatelyBearLeft = String.Empty;    //Immediately bear left on to
        protected string immediatelyBearRight = String.Empty;

        protected string leaveFrom = String.Empty;  //Starting from
        protected string arriveAt = String.Empty;   //Arrive at 
        protected string notApplicable = String.Empty; // -

        protected string localRoad = String.Empty;  //local road
        protected string localPath = String.Empty;  //unnamed path
        protected string street = String.Empty;     //steet
        protected string path = String.Empty;       //path

        //route text for motorway junctions
        protected string atJunctionLeave = String.Empty;
        protected string leaveMotorway = String.Empty;
        protected string towards = String.Empty;
        protected string continueFor = String.Empty;
        protected string miles = String.Empty;
        protected string turnLeftToJoin = String.Empty;
        protected string turnRightToJoin = String.Empty;
        protected string atJunctionJoin = String.Empty;
        protected string bearLeftToJoin = String.Empty;
        protected string bearRightToJoin = String.Empty;
        protected string join = String.Empty;
        protected string follow = String.Empty;
        protected string to = String.Empty;
        protected string untilJunction = String.Empty;
               
        protected string enter = String.Empty;
        protected string exit = String.Empty;
        protected string end = String.Empty;
        protected string congestionZone = String.Empty;
        protected string charge = String.Empty;
        protected string chargeAdultAndCycle = String.Empty;
        protected string certainTimes = String.Empty;
        protected string certainTimesNoCharge = String.Empty;
        protected string board = String.Empty;
        protected string departingAt = String.Empty;
        protected string toll = String.Empty;
        protected string notAvailable = String.Empty;

        protected string ferryWait = String.Empty;
        protected string unspecifedFerryWait = String.Empty;
        protected string intermediateFerryWait = String.Empty;
        protected string waitAtTerminal = String.Empty;

        protected string viaArriveAt = String.Empty;
        protected string leaveFerry = String.Empty;

        //Ambiguity text
        protected string straightOn = string.Empty;     //straight on
        protected string atMiniRoundabout = string.Empty;   //At mini-roundabout 
        protected string atMiniRoundabout2 = string.Empty;   //at mini-roundabout 
        protected string immediatelyTurnRightOnto = string.Empty;   //Immediately turn right on to
        protected string immediatelyTurnLeftOnto = string.Empty;
        protected string whereRoadSplits = string.Empty;    //where the road splits
        protected string uTurn = string.Empty;   // U-Turn
        protected string onto = string.Empty;   //on to

        // string for through route
        protected string throughRoute = String.Empty;   //Follow the road on to

        // open new window icon
        protected string openNewWindowImageUrl = string.Empty;

        #endregion

        #region Temporary variables used to track across multiple CycleJourneyDetails
        private bool currentIsCycleRoute = false;
        private bool previousIsCycleRoute = false;

        private bool previousCycleInfrastructureImageShown = false;
        private bool previousRecommendedRouteImageShown = false;
        
        private bool joiningShowCycleImage = false;
        private bool leavingShowCycleImage = false;
        private bool joiningShowRecommendedImage = false;
        private bool leavingShowRecommendedImage = false;

        private bool stopoverManoeuvreImage = false;

        private bool isRoundabout = false;
        private bool isMiniRoundabout = false;
        private bool isPath = false;
        private bool isCrossing = false;

        private string currentITNRoadType = string.Empty;

        private string divOpenOutwardHide = "<div class=\"cycleJourneyDetailsAttributeHideOut\">";
        private string divOpenReturnHide = "<div class=\"cycleJourneyDetailsAttributeHideRet\">";

        private string divOpenOutwardShow = "<div class=\"cycleJourneyDetailsAttributeShowOut\">";
        private string divOpenReturnShow = "<div class=\"cycleJourneyDetailsAttributeShowRet\">";

        private string divClose = "</div>";

        #endregion

        #region Private variables

        // strings used to hold the cycle attribute text details
        private string attributeCrossingText;
        private string attributeTypeText;
        private string attributeBarrierText;
        private string attributeCharacteristicText;
        private string attributeManoeuvreText;

        private List<CycleAttribute> cycleTypeAttributes = new List<CycleAttribute>();
        #endregion

        #endregion Member Variables

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        protected CycleJourneyDetailFormatter()
        {
        }

        /// <summary>
        /// Constructs a cycle journey formatter
        /// </summary>
        /// <param name="cycleJourney">The specific cycle journey to display</param>
        /// <param name="journeyViewState">The related journey view state, used specifically for congestion zone</param>
        /// <param name="outward">Indicates if this is an outward journey</param>
        /// <param name="roadUnits">The road units to display the journey distance in</param>
        /// <param name="print">Indicates if the instructions are displayed on a printer friendly page</param>
        /// <param name="showAllDetails">Indicates if the attributes are set to visible or hidden by default</param>
        /// <param name="isCJPUser">When true, outputs additional direction values</param>
        protected CycleJourneyDetailFormatter(CycleJourney cycleJourney, TDJourneyViewState journeyViewState, bool outward, RoadUnitsEnum roadUnits,
            bool print, bool showAllDetails, bool isCJPUser)
        {
            this.cycleJourney = cycleJourney;
            this.journeyViewState = journeyViewState;
            this.outward = outward;
            this.roadUnits = roadUnits;
            this.printable = print;
            this.showAllDetails = showAllDetails;

            this.showAttributeNumber = isCJPUser;

            InitialiseRouteTextDescriptionStrings();

            // Used to prepend the word 'Immediately' to instructions with short distances between them
            immediateTurnDistance = Convert.ToInt32(Properties.Current["CyclePlanner.CycleJourneyDetailsControl.ImmediateTurnDistance"], TDCultureInfo.CurrentUICulture.NumberFormat);

            // Used to display the additional instruction text returned by the planner
            this.showInstructionText = bool.Parse(Properties.Current["CyclePlanner.Display.AdditionalInstructionText"]);
        }

        #endregion

        #region Properties

        /// <summary>
        /// The accummulated distance of a journey in miles. The value of the
        /// accummulated distance is calculated during the formatting of
        /// journey instructions and is incremented by GetDistance.
        /// </summary>
        protected virtual string TotalDistance
        {
            get
            {
                return (accumulatedDistance > 0 ? ConvertMetresToMileage(accumulatedDistance) : notApplicable);
            }
        }

        /// <summary>
        /// The accummulated distance of a journey in kms. The value of the
        /// accummulated distance is calculated during the formatting of
        /// journey instructions and is incremented by GetDistance.
        /// </summary>
        protected virtual string TotalKmDistance
        {
            get
            {
                return (accumulatedDistance > 0 ? ConvertMetresToKm(accumulatedDistance) : notApplicable);
            }
        }
        #endregion Properties

        #region Public methods

        /// <summary>
        /// Returns an ordered list of journey details. Each object in the
        /// list contains details for a single journey instruction. Each object
        /// is a string array of details (e.g, road name, distance, directions)
        /// The contents of the list and each string array element is
        /// determined by methods in subclasses that produce specific results
        /// dependant on purpose (e.g. output for web page, email, and so on).
        /// </summary>
        /// <returns>Returns an ordered list of journey instructions where each 
        /// element is a string array of details (e.g, road name, distance, directions)</returns>
        public virtual IList GetJourneyDetails()
        {
            return ProcessCycleJourney(-1, -1);
        }

        /// <summary>
        /// Returns an ordered list of journey details. Each object in the
        /// list contains details for a single journey instruction. Each object
        /// is a string array of details (e.g, road name, distance, directions)
        /// The contents of the list and each string array element is
        /// determined by methods in subclasses that produce specific results
        /// dependant on purpose (e.g. output for web page, email, and so on).
        /// </summary>
        /// <param name="startIndex">The journey instruction index to start the directions at</param>
        /// <param name="endIndex">The journey intstrunction index to end the directions at</param>
        /// <returns>Returns an ordered list of journey instructions where each 
        /// element is a string array of details (e.g, road name, distance, directions)</returns>
        public virtual IList GetJourneyDetails(int startIndex, int endIndex)
        {
            return ProcessCycleJourney(startIndex, endIndex);
        }

        /// <summary>
        /// Returns culture specific strings that are used to identify
        /// the elements of the string arrays returned by GetJourneyDetails. 
        /// </summary>
        /// <returns>Heading labels identifying elements of the string arrays
        /// returned by GetJourneyDetails</returns>
        public abstract string[] GetDetailHeadings();

        #endregion

        #region Protected methods

        /// <summary>
        /// The is a template method that controls the format process. It calls
        /// into hook methods to generate specific output defined by 
        /// subclasses.
        /// </summary>
        /// <returns>Returns an ordered list of journey instructions where each 
        /// element is a string array of details (e.g, road name, distance, directions)</returns>
        protected virtual IList ProcessCycleJourney(int startIndex, int endIndex)
        {
            ArrayList details = new ArrayList();

            if ((cycleJourney == null) || (cycleJourney.Details.Length == 0))
            {
                return details;
            }
            else
            {
                initFormatting();
                
                details.Add(AddFirstDetailLine());

                for (int journeyDetailIndex = 0;
                    journeyDetailIndex < cycleJourney.Details.Length;
                    journeyDetailIndex++)
                {
                    // Do the pre-processing. This sets up all image flags, attribute text strings needed by the 
                    // various cycle text methods
                    PreProcessCycleJourneyDetail(journeyDetailIndex);

                    details.Add(ProcessCycleJourneyDetail(journeyDetailIndex, false));

                    // Do the post-processing. This resets any temporary flags (e.g. image flags) ready for the next detail
                    PostProcessCycleJourneyDetail();
                }

                details.Add(AddLastDetailLine());

                // Now that we have all the details, if a start and end index have been supplied,
                // filter the details to only include those instructions
                if ((startIndex >= 0) && (endIndex >= 0))
                {
                    return FilterCycleJourneyDetails(details, startIndex, endIndex);
                }

                return details;
            }
        }

        /// <summary>
        /// A hook method called by ProcessCycleJourney to process each cycle journey
        /// instruction. The returned string array should contain details for each 
        /// instruction (e.g, road name, distance, directions)
        /// </summary>
        /// <param name="journeyDetailIndex">Array index into cycleJourney.Details</param>
        /// <returns>details for each instruction (e.g, road name, distance, directions)</returns>
        protected abstract object[] ProcessCycleJourneyDetail(int journeyDetailIndex, bool showCongestionCharge);

        /// <summary>
        /// A hook method called by ProcessCycleJourney to add the first element to the 
        /// ordered list of journey instructions. The returned string array
        /// should contain details for the first instruction.
        /// </summary>
        /// <returns>details for first instruction</returns>
        protected abstract object[] AddFirstDetailLine();

        /// <summary>
        /// A hook method called by ProcessCycleJourney to add the last element to the 
        /// ordered list of journey instructions. The returned string array
        /// should contain details for the last instruction.
        /// </summary>
        /// <returns>details for last instruction</returns>
        protected abstract object[] AddLastDetailLine();

        /// <summary>
        /// 
        /// </summary>
        protected abstract string AddContinueFor(CycleJourneyDetail detail, bool nextDetailHasJunctionExitJunction, string routeText);

        /// <summary>
        /// A hook method called by ProcessCycleJourney to filter the details returned to only include
        /// the instructions at the start and end index
        /// </summary>
        /// <returns>Returns an ordered list of journey instructions where each 
        /// element is a string array of details (e.g, road name, distance, directions), filtered 
        /// to only include instructions between the start and end indexs</returns>
        protected abstract IList FilterCycleJourneyDetails(IList details, int startIndex, int endIndex);
        

        #region Helper methods
        /// <summary>
        /// Returns the current instruction step number. Getting the instruction step 
        /// number also increments it
        /// </summary>
        protected virtual string GetCurrentStepNumber()
        {
            string currentStepNumberString = stepNumber.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
            stepNumber++;
            return currentStepNumberString;
        }

        /// <summary>
        /// Called by the template method ProcessRoadJourney prior to 
        /// formatting. Initialises instance variables used during the
        /// formatting process.
        /// </summary>
        protected virtual void initFormatting()
        {
            accumulatedDistance = 0;
            stepNumber = 1;
            currentArrivalTime = cycleJourney.DepartDateTime;
        }

        /// <summary>
        /// Returns formatted string of the road name for the supplied
        /// instruction.
        /// </summary>
        /// <param name="detail">Array index into cycleJourney.Details for journey instruction</param>
        /// <returns>The road name</returns>
        protected virtual string GetRoadName(int journeyDetailIndex)
        {
            CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];
            string result = FormatRoadName(detail);
            return result;
        }

        /// <summary>
        /// Returns a formatted string (defined by the ConvertMetresToMileage
        /// method) of the distance for the current cycling instruction. 
        /// Adds the distance to the instance variable holding accumulated 
        /// journey distance.
        /// </summary>
        /// <param name="detail">Array index into cycleJourney.Details for journey instruction</param>
        /// <returns>Formatted distance in miles</returns>
        protected virtual string GetDistance(int journeyDetailIndex)
        {
            CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];

            // Add the distance to the running count
            accumulatedDistance += detail.Distance;

            return (ConvertMetresToMileage(detail.Distance));
        }

        /// <summary>
        /// Returns a formatted string (defined by FormatDuration method)
        /// of the duration of the current cycling instruction.
        /// </summary>
        /// <param name="detail">Array index into cycleJourney.Details for journey instruction</param>
        /// <returns>Formatted string of the duration.</returns>
        protected virtual string GetDuration(int journeyDetailIndex)
        {
            CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];
            return FormatDuration(detail.Duration);
        }

        /// <summary>
        /// Returns a formatted string of the arrival time for the current
        /// cycling instruction in the format defined by the method FormatTime.
        /// The time is calculated by adding the current journey
        /// instruction duration to an accumulated journey time.
        /// </summary>
        /// <param name="detail">Array index into cycleJourney.Details for journey instruction</param>
        /// <returns>Formatted arrival time</returns>
        protected virtual string GetArrivalTime(int journeyDetailIndex)
        {
            CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];
            //If it is a Stopover (unless a wait) We don't want to add the time

            if (detail.FerryCheckIn)
            {
                TimeSpan span = new TimeSpan(0, 0, (int)detail.Duration);
                TDDateTime arrivalTime = currentArrivalTime.Subtract(span);
                return FormatTime(arrivalTime);
            }

            else if (detail.StopoverSection && !detail.WaitSection)
            {
                return FormatTime(currentArrivalTime);
            }
            else
            {
                TimeSpan span = new TimeSpan(0, 0, (int)detail.Duration);
                TDDateTime arrivalTime = currentArrivalTime;
                currentArrivalTime = currentArrivalTime.Add(span);
                return FormatTime(arrivalTime);
            }
        }

        #region Cycle text

        #region Pre and Post process CycleJourneyDetail
        /// <summary>
        /// Performs pre-processing of the Cycle Journey Detail. 
        /// This method sets up the various flags and text values needed before any of the cycle detail text
        /// methods are called.
        /// </summary>
        /// <param name="journeyDetailIndex"></param>
        private void PreProcessCycleJourneyDetail(int journeyDetailIndex)
        {
            // Get the cycle infrastructure attributes
            ICycleAttributes cycleAttributesService = (ICycleAttributes)TDServiceDiscovery.Current[ServiceDiscoveryKey.CycleAttributes];

            // Get the cycle detail
            CycleJourneyDetail currentDetail = cycleJourney.Details[journeyDetailIndex];

            #region Set up "is a Cycle route" flags

            // Check if current and previous detail have a Cycle Route
            currentIsCycleRoute = ((currentDetail.CycleRoutes != null) && (currentDetail.CycleRoutes.Count > 0));

            #endregion

            #region Set up Cycle image attribute variables
            // Check if any of the attributes are for specific cycling infrastructure

            // Determine if the joining attributes has an attribute to Show the cycle image
            if ((currentDetail.JoiningSignificantLinkAttributes != null)
                && (currentDetail.JoiningSignificantLinkAttributes.Length > 0))
            {
                for (int i = 0; i < currentDetail.JoiningSignificantLinkAttributes.Length; i++)
                {
                    CycleAttributeGroup cycleAttributeGroup = GetCycleAttributeGroup(i);

                    CycleAttribute[] cycleInfrastructureArray = cycleAttributesService.GetCycleInfrastructureAttributes(cycleAttributeGroup);
                    CycleAttribute[] cycleRecommendedArray = cycleAttributesService.GetCycleRecommendedAttributes(cycleAttributeGroup);

                    uint attributeValue = currentDetail.JoiningSignificantLinkAttributes[i];

                    // Cycle path icon
                    foreach (CycleAttribute cycleAttribute in cycleInfrastructureArray)
                    {
                        // Is cycle attribute bit flag true
                        if ((cycleAttribute.CycleAttributeMask & attributeValue) > 0)
                        {
                            joiningShowCycleImage = true;
                            break;
                        }
                    }

                    // Cycle recommended route icon
                    foreach (CycleAttribute cycleAttribute in cycleRecommendedArray)
                    {
                        if ((cycleAttribute.CycleAttributeMask & attributeValue) > 0)
                        {
                            joiningShowRecommendedImage = true;
                            break;
                        }
                    }
                }
            }

            // Determine if the leaving attribute has an attribute to Show the cycle image
            if ((currentDetail.LeavingSignificantLinkAttributes != null)
                && (currentDetail.LeavingSignificantLinkAttributes.Length > 0))
            {
                for (int i = 0; i < currentDetail.LeavingSignificantLinkAttributes.Length; i++)
                {
                    CycleAttributeGroup cycleAttributeGroup = GetCycleAttributeGroup(i);

                    CycleAttribute[] cycleInfrastructureArray = cycleAttributesService.GetCycleInfrastructureAttributes(cycleAttributeGroup);
                    CycleAttribute[] cycleRecommendedArray = cycleAttributesService.GetCycleRecommendedAttributes(cycleAttributeGroup);

                    uint attributeValue = currentDetail.LeavingSignificantLinkAttributes[i];

                    // Cycle path icon
                    foreach (CycleAttribute cycleAttribute in cycleInfrastructureArray)
                    {
                        // Is cycle attribute bit flag true
                        if ((cycleAttribute.CycleAttributeMask & attributeValue) > 0)
                        {
                            leavingShowCycleImage = true;
                            break;
                        }
                    }

                    // Cycle recommended route icon
                    foreach (CycleAttribute cycleAttribute in cycleRecommendedArray)
                    {
                        if ((cycleAttribute.CycleAttributeMask & attributeValue) > 0)
                        {
                            leavingShowRecommendedImage = true;
                            break;
                        }
                    }
                }
            }

            #endregion

            #region Set up Complex manoeuvre image attribute

            // Determine if this is a manoeuvre and there is a stopover manoeuvre attribute. If both are true,
            // then the manoeuvre image should be shown
            if ( (currentDetail.ComplexManoeuvre)
                && (currentDetail.SectionFeatureAttributes != null)
                && (currentDetail.SectionFeatureAttributes.Length > 0))
            {
                for (int i = 0; i < currentDetail.SectionFeatureAttributes.Length; i++)
                {
                    CycleAttributeGroup cycleAttributeGroup = GetCycleAttributeGroup(i);

                    CycleAttribute[] cycleAttributesArray = cycleAttributesService.GetCycleAttributes(CycleAttributeType.Stopover, cycleAttributeGroup);

                    uint attributeValue = currentDetail.SectionFeatureAttributes[i];

                    // Specifically check for the mini roundabout attribute
                    foreach (CycleAttribute cycleAttribute in cycleAttributesArray)
                    {
                        // Is cycle attribute bit flag true
                        if ((cycleAttribute.CycleAttributeMask & attributeValue) > 0)
                        {
                            this.stopoverManoeuvreImage = true;
                            break;
                        }
                    }
                }
            }

            #endregion

            #region Set up "is a mini roundabout" instruction attribute

            // Determine if the node attributes has an attribute or Miniroundabouts
            if ((currentDetail.SignificantNodeAttributes != null)
                && (currentDetail.SignificantNodeAttributes.Length > 0))
            {
                for (int i = 0; i < currentDetail.SignificantNodeAttributes.Length; i++)
                {
                    CycleAttributeGroup cycleAttributeGroup = GetCycleAttributeGroup(i);

                    CycleAttribute[] cycleNodeAttributesArray = cycleAttributesService.GetCycleAttributes(CycleAttributeType.Node, cycleAttributeGroup);

                    uint attributeValue = currentDetail.SignificantNodeAttributes[i];

                    // Specifically check for the mini roundabout attribute
                    foreach (CycleAttribute cycleAttribute in cycleNodeAttributesArray)
                    {
                        // Is cycle attribute bit flag true
                        if (((cycleAttribute.CycleAttributeMask & attributeValue) > 0)
                            &&
                            (cycleAttribute.CycleAttributeResourceName.Trim().ToLower().Equals("cycleattribute.miniroundabout")))
                        {
                            this.isMiniRoundabout = true;
                            break;
                        }
                    }
                }
            }

            #endregion

            #region Set up the type of path text and "is a path" attribute
            
            // Assumption:
            // Only the joining attributes will contain the type of path attribute,
            // and the interesting will also continue to contain the path attribute
            // e.g. join on to a cycle path, then following instructions no longer have the
            // attribute in the join, but will have it in the interesting.

            if ((currentDetail.JoiningSignificantLinkAttributes != null)
                && (currentDetail.JoiningSignificantLinkAttributes.Length > 0))
            {
                SetITNRoadType(currentDetail.JoiningSignificantLinkAttributes, CycleAttributeType.Link);
            }

            if ((currentDetail.InterestingLinkAttributes != null)
                && (currentDetail.InterestingLinkAttributes.Length > 0))
            {
                SetITNRoadType(currentDetail.InterestingLinkAttributes, CycleAttributeType.Link);
            }

            #endregion

            #region Set up the "is a roundabout" instruction attribuute

            // Determine if this instruction has the Roundabout attribute.
            // Roundabout attributes are in the LeavingSignificant array
            if ((currentDetail.LeavingSignificantLinkAttributes != null)
                && (currentDetail.LeavingSignificantLinkAttributes.Length > 0))
            {
                for (int i = 0; i < currentDetail.LeavingSignificantLinkAttributes.Length; i++)
                {
                    CycleAttributeGroup cycleAttributeGroup = GetCycleAttributeGroup(i);

                    CycleAttribute[] cycleAttributesArray = cycleAttributesService.GetCycleAttributes(CycleAttributeType.Link, cycleAttributeGroup);

                    // Get the cycle attribute
                    uint attributeValue = currentDetail.LeavingSignificantLinkAttributes[i];

                    // Specifically check for the Roundabout attributes
                    foreach (CycleAttribute cycleAttribute in cycleAttributesArray)
                    {
                        if (cycleAttribute.CycleAttributeCategory == CycleAttributeCategory.Roundabout)
                        {
                            // Is cycle attribute bit flag true
                            if ((cycleAttribute.CycleAttributeMask & attributeValue) > 0)
                            {
                                this.isRoundabout = true;
                                break;
                            }
                        }
                    }
                }
            }

            #endregion

            #region Set up the "is a crossing" instruction attribuute

            // Determine if there are any Crossing attributes for this cycle detail.
            // Crossing attributes are in the JoiningSignificant array
            //
            // Assumption: If this is a crossing Cycle detail, then it will only ever have a crossing attribute and no other attributes
            if ((currentDetail.JoiningSignificantLinkAttributes != null)
                && (currentDetail.JoiningSignificantLinkAttributes.Length > 0))
            {
                for (int i = 0; i < currentDetail.JoiningSignificantLinkAttributes.Length; i++)
                {
                    CycleAttributeGroup cycleAttributeGroup = GetCycleAttributeGroup(i);

                    CycleAttribute[] cycleAttributesArray = cycleAttributesService.GetCycleAttributes(CycleAttributeType.Link, cycleAttributeGroup);

                    // Get the cycle attribute
                    uint attributeValue = currentDetail.JoiningSignificantLinkAttributes[i];

                    // Specifically check for the Crossing attributes
                    foreach (CycleAttribute cycleAttribute in cycleAttributesArray)
                    {
                        if (cycleAttribute.CycleAttributeCategory == CycleAttributeCategory.Crossing)
                        {
                            // Is cycle attribute bit flag true
                            if ((cycleAttribute.CycleAttributeMask & attributeValue) > 0)
                            {
                                this.isCrossing = true;
                                break;
                            }
                        }
                    }
                }
            }

            #endregion

            // Finally set up the cycle attribute text.
            // We make the call here, bceause for some scenarios (e.g. a Crossing), we want to display the 
            // Attribute instruction in the place of the Direction instruction
            SetupCycleAttributeDetails(journeyDetailIndex);
        }

        /// <summary>
        /// Performs post-processing of the Cycle Journey Detail.
        /// This method resets any cycle specific flags and text values in readyness for processing the 
        /// next cycle journey detail.
        /// </summary>
        private void PostProcessCycleJourneyDetail()
        {
            // Persist flags to be used by the next loop
            this.previousIsCycleRoute = this.currentIsCycleRoute;

            // Reset flags to be used by the next call to PreProcess
            this.joiningShowCycleImage = false;
            this.leavingShowCycleImage = false;
            this.joiningShowRecommendedImage = false;
            this.leavingShowRecommendedImage = false;

            this.stopoverManoeuvreImage = false;

            this.isRoundabout = false;
            this.isMiniRoundabout = false;
            this.isPath = false;
            this.isCrossing = false;

            this.currentITNRoadType = string.Empty;

            this.attributeBarrierText = string.Empty;
            this.attributeCharacteristicText = string.Empty;
            this.attributeCrossingText = string.Empty;
            this.attributeManoeuvreText = string.Empty;
            this.attributeTypeText = string.Empty;

            this.cycleTypeAttributes = new List<CycleAttribute>();
        }

        #endregion

        /// <summary>
        /// Returns a formatted html image string if the cycling detail is on cycle infrastructure
        /// </summary>
        /// <param name="detail">Array index into cycleJourney.Details for journey instruction</param>
        /// <returns>image html string</returns>
        protected virtual string GetCyclePathImage(int journeyDetailIndex)
        {
            // Assumes PreProcessCycleJourneyDetail has already been called

            string cycleImage = string.Empty;

            #region Determine which cycle icon to display
            if (joiningShowCycleImage)
            {
                // joiningShowCycleImage indicates this link is still on cycleinfrastructure, show image
                cycleImage = GetCycleImageString(true);

                this.previousCycleInfrastructureImageShown = true;
                this.previousRecommendedRouteImageShown = false;
            }
            
            else if (previousCycleInfrastructureImageShown && !leavingShowCycleImage)
            {
                // no cycle infrastructure attribute detected, and because we were previously showing the image,
                // continue to display it
                cycleImage = GetCycleImageString(true);

                this.previousCycleInfrastructureImageShown = true;
                this.previousRecommendedRouteImageShown = false;
            }

                // no cycle infrastructure icon, so now check if there was a recommended route or cycle route name
            else if ((currentIsCycleRoute) || (joiningShowRecommendedImage))
            {   
                // indicates this detail is on a recommended route, show image
                cycleImage = GetCycleImageString(false);

                this.previousRecommendedRouteImageShown = true;
            }
                // we were previously showing the recommended icon, continue to display
            else if (previousRecommendedRouteImageShown && !leavingShowRecommendedImage)
            {
                cycleImage = GetCycleImageString(false);

                this.previousRecommendedRouteImageShown = true;
            }

            #endregion

            // Finally update flag if we're leaving cycle infrastructure
            if (previousCycleInfrastructureImageShown && leavingShowCycleImage)
            {
                // previously showing the image and leaving cycle infrastructure attribute detected, show 
                // leaving infrastructure text

                // No need to set anything here as this text will be picked up by the GetCyclePathName, so
                // that it is displayed in the correct place in the UI

                this.previousCycleInfrastructureImageShown = false;
            }

            // Finally update flag if we're leaving cycle recommended route and not on a cycle route
            if ((previousRecommendedRouteImageShown && leavingShowRecommendedImage && !currentIsCycleRoute)
                ||
                (!previousRecommendedRouteImageShown && !leavingShowRecommendedImage && !currentIsCycleRoute))
            {
                this.previousRecommendedRouteImageShown = false;
            }

            #region Determine whether the complex manoeuvre icon should be displayed

            if (stopoverManoeuvreImage)
            {
                if (!string.IsNullOrEmpty(cycleImage))
                {
                    // Already showing a cycle image, so place a line break
                    cycleImage += "<br />";
                }

                cycleImage += GetManoeuvreImageString();
            }

            #endregion

            return cycleImage;
        }
        
        /// <summary>
        /// Returns a formatted string of the cycle path name.
        /// </summary>
        /// <param name="detail">Array index into cycleJourney.Details for journey instruction</param>
        /// <returns>Formatted string of the cycle path name and number</returns>
        protected virtual string GetCyclePathName(int journeyDetailIndex)
        {
            // Assumes PreProcessCycleJourneyDetail has already been called

            CycleJourneyDetail currentDetail = cycleJourney.Details[journeyDetailIndex];
            CycleJourneyDetail previousDetail = null;

            if (journeyDetailIndex > 0)
                previousDetail = cycleJourney.Details[journeyDetailIndex - 1];

            StringBuilder cyclePathText = new StringBuilder();

            // First time user enters the cycle route, so ok to display it
            if ((!previousIsCycleRoute) && (currentIsCycleRoute))
            {
                foreach (KeyValuePair<string, string> cycleRoute in currentDetail.CycleRoutes)
                {
                    #region Construct cycle route name
                    string cycleRouteName = cycleRoute.Value.ToString();

                    // If we have a cycle route number, also append this
                    int cycleRouteNumber = Convert.ToInt32(cycleRoute.Key);
                    if (cycleRouteNumber >= 0)
                    {
                        cycleRouteName = " " + cycleRoute.Key.ToString();
                    }
                    #endregion

                    cyclePathText.Append(
                        string.Format(Global.tdResourceManager.GetString("CycleRouteText.RouteJoins", CultureInfo.CurrentUICulture),
                            cycleRouteName));
                    
                    cyclePathText.Append("<br />");
                }
            }
                // User is still on a cycle route, if it is the same route don't display the text
            else if ((previousIsCycleRoute) && (currentIsCycleRoute))
            {
                bool canShowRoute = false;

                #region Determine if route text can be shown
                foreach (KeyValuePair<string, string> cycleRoute in currentDetail.CycleRoutes)
                {
                    canShowRoute = true;

                    // Go through all the previous detail cycle routes and make sure there are no matches
                    foreach (KeyValuePair<string, string> previousCycleRoute in previousDetail.CycleRoutes)
                    {
                        if (cycleRoute.Value == previousCycleRoute.Value)
                        {
                            // This cycle route was in the previous detail, so can't display it
                            canShowRoute = false;
                            break;
                        }
                    }

                    if (canShowRoute)
                    {
                        #region Construct cycle route name
                        string cycleRouteName = cycleRoute.Value.ToString();

                        // If we have a cycle route number, also append this
                        int cycleRouteNumber = Convert.ToInt32(cycleRoute.Key);
                        if (cycleRouteNumber >= 0)
                        {
                            cycleRouteName = " " + cycleRoute.Key.ToString();
                        }
                        #endregion

                        cyclePathText.Append(
                            string.Format(Global.tdResourceManager.GetString("CycleRouteText.RouteJoins", CultureInfo.CurrentUICulture),
                            cycleRouteName));

                        cyclePathText.Append("<br />");
                    }
                }
                #endregion
            }
                // Display any leaving cycle infrastructure text if required.
                // These flags are set when determining whether to display cycle icons (PreProcessCycleJourneyDetail)
            else if (previousCycleInfrastructureImageShown && leavingShowCycleImage && !leavingShowRecommendedImage)
            {
                // No longer need to show the CycleRouteText.RouteLeavesCycleInfrastructure text
            }

            return cyclePathText.ToString();
        }

        /// <summary>
        /// Returns a formatted string of the cycle path name.
        /// </summary>
        /// <param name="currentDetail">Current cycle journey detail</param>
        /// <returns>Formatted string of the cycle path name and number</returns>
        protected virtual string GetCyclePathName(CycleJourneyDetail currentDetail)
        {
            return GetCyclePathName(currentDetail, true);
        }

        /// <summary>
        /// Returns a formatted string of the cycle path name.
        /// </summary>
        /// <param name="currentDetail">Current cycle journey detail</param>
        /// <returns>Formatted string of the cycle path name and number</returns>
        protected virtual string GetCyclePathName(CycleJourneyDetail currentDetail, bool appendPrefix)
        {
            StringBuilder cyclePathText = new StringBuilder();

            if (currentDetail.CycleRoutes != null)
            {
                foreach (KeyValuePair<string, string> cycleRoute in currentDetail.CycleRoutes)
                {
                    #region Construct cycle route name
                    string cycleRouteName = cycleRoute.Value.ToString();

                    // If we have a cycle route number, also append this
                    int cycleRouteNumber = Convert.ToInt32(cycleRoute.Key);
                    if (cycleRouteNumber >= 0)
                    {
                        cycleRouteName = " " + cycleRoute.Key.ToString();
                    }
                    #endregion

                    if (appendPrefix)
                    {
                        cyclePathText.Append(
                            string.Format(Global.tdResourceManager.GetString("CycleRouteText.RouteJoins", CultureInfo.CurrentUICulture),
                                cycleRouteName));
                    }
                    else
                    {
                        cyclePathText.Append(cycleRouteName);
                    }
                }
            }

            return cyclePathText.ToString();
        }

        /// <summary>
        /// Returns a formatted string of any specific cycle instruction text.
        /// </summary>
        /// <param name="detail">Array index into cycleJourney.Details for journey instruction</param>
        /// <returns>Formatted string of the specific cycle instruction text</returns>
        protected virtual string GetCycleInstructionDetails(int journeyDetailIndex)
        {
            if (this.showInstructionText)
            {
                CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];

                StringBuilder instructionText = new StringBuilder();

                // Append any specific instructions
                if (!string.IsNullOrEmpty(detail.InstructionText))
                {
                    if (outward)
                        instructionText.Append(showAllDetails ? divOpenOutwardShow : divOpenOutwardHide);
                    else
                        instructionText.Append(showAllDetails ? divOpenReturnShow : divOpenReturnHide);

                    instructionText.Append(Global.tdResourceManager.GetString("CycleRouteText.ForThisManoeuvre", CultureInfo.CurrentUICulture));
                    instructionText.Append(space);
                    instructionText.Append(detail.InstructionText);
                    instructionText.Append(divClose);
                }

                return instructionText.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Returns a formatted string if there are specific cycling attribute details for the current
        /// cycle instruction
        /// </summary>
        /// <param name="detail">Array index into cycleJourney.Details for journey instruction</param>
        /// <returns>Formatted string of the specific cycling attribute details</returns>
        protected virtual string GetCycleAttributeDetails(int journeyDetailIndex)
        {
            StringBuilder attributeText = new StringBuilder();

            // If this is a crossing CycleJourneyDetail, then we don't show any other attribute text
            if (!this.isCrossing)
            {
                #region Create the combined attribute text

                if (!string.IsNullOrEmpty(attributeManoeuvreText))
                {
                    if (outward)
                        attributeText.Append(showAllDetails ? divOpenOutwardShow : divOpenOutwardHide);
                    else
                        attributeText.Append(showAllDetails ? divOpenReturnShow : divOpenReturnHide);

                    attributeText.Append(attributeManoeuvreText);
                    attributeText.Append(divClose);
                }

                // The crossing attribute text is now shown in place of the Detail direction text, see method GetDirections()
                /*
                if (!string.IsNullOrEmpty(attributeCrossingText))
                {
                    if (outward)
                        attributeText.Append(showAllDetails ? divOpenOutwardShow : divOpenOutwardHide);
                    else
                        attributeText.Append(showAllDetails ? divOpenReturnShow : divOpenReturnHide);

                    attributeText.Append(attributeCrossingText);
                    attributeText.Append(divClose);
                }
                */

                if (!string.IsNullOrEmpty(attributeTypeText))
                {
                    if (outward)
                        attributeText.Append(showAllDetails ? divOpenOutwardShow : divOpenOutwardHide);
                    else
                        attributeText.Append(showAllDetails ? divOpenReturnShow : divOpenReturnHide);

                    attributeText.Append(attributeTypeText);
                    attributeText.Append(divClose);
                }

                if (!string.IsNullOrEmpty(attributeBarrierText))
                {
                    if (outward)
                        attributeText.Append(showAllDetails ? divOpenOutwardShow : divOpenOutwardHide);
                    else
                        attributeText.Append(showAllDetails ? divOpenReturnShow : divOpenReturnHide);

                    attributeText.Append(attributeBarrierText);
                    attributeText.Append(divClose);
                }

                if (!string.IsNullOrEmpty(attributeCharacteristicText))
                {
                    if (outward)
                        attributeText.Append(showAllDetails ? divOpenOutwardShow : divOpenOutwardHide);
                    else
                        attributeText.Append(showAllDetails ? divOpenReturnShow : divOpenReturnHide);

                    attributeText.Append(attributeCharacteristicText);
                    attributeText.Append(divClose);
                }

                #endregion

                return attributeText.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        #region Cycle helper methods
        /// <summary>
        /// Returns the cycle path image text.
        /// </summary>
        /// <param name="cycleInfrastructureIcon">true = Cycle Infrastructure icon, false = Recommended Cycle Route icon</param>
        /// <returns></returns>
        private string GetCycleImageString(bool cycleInfrastructureIcon)
        {
            StringBuilder cycleImage = new StringBuilder();

            cycleImage.Append("<img src=\"");

            if (cycleInfrastructureIcon)
                cycleImage.Append(Global.tdResourceManager.GetString("CyclePlanner.CycleJourneyDetailsTableControl.Instruction.CyclePathImage"));
            else
                cycleImage.Append(Global.tdResourceManager.GetString("CyclePlanner.CycleJourneyDetailsTableControl.Instruction.CycleRouteImage"));

            cycleImage.Append("\" align=\"middle\" alt=\"");

            if (cycleInfrastructureIcon)
                cycleImage.Append(Global.tdResourceManager.GetString("CyclePlanner.CycleJourneyDetailsTableControl.Instruction.CyclePathImage.AltText"));
            else
                cycleImage.Append(Global.tdResourceManager.GetString("CyclePlanner.CycleJourneyDetailsTableControl.Instruction.CycleRouteImage.AltText"));

            cycleImage.Append("\" title=\"");

            if (cycleInfrastructureIcon)
                cycleImage.Append(Global.tdResourceManager.GetString("CyclePlanner.CycleJourneyDetailsTableControl.Instruction.CyclePathImage.AltText"));
            else
                cycleImage.Append(Global.tdResourceManager.GetString("CyclePlanner.CycleJourneyDetailsTableControl.Instruction.CycleRouteImage.AltText"));

            cycleImage.Append("\" />");

            return cycleImage.ToString();
        }

        /// <summary>
        /// Returns the manoeuvre image path and image text.
        /// </summary>
        /// <returns></returns>
        private string GetManoeuvreImageString()
        {
            StringBuilder image = new StringBuilder();

            image.Append("<img src=\"");

            image.Append(Global.tdResourceManager.GetString("CyclePlanner.CycleJourneyDetailsTableControl.Instruction.ManoeuvreImage"));
            

            image.Append("\" align=\"middle\" alt=\"");

            image.Append(Global.tdResourceManager.GetString("CyclePlanner.CycleJourneyDetailsTableControl.Instruction.ManoeuvreImage.AltText"));

            image.Append("\" title=\"");

            image.Append(Global.tdResourceManager.GetString("CyclePlanner.CycleJourneyDetailsTableControl.Instruction.ManoeuvreImage.AltText"));

            image.Append("\" />");

            return image.ToString();
        }

        /// <summary>
        /// Method which uses the Index 0 in the attributes array to obtain the ITN road type, and sets the global
        /// roadtype string to be used later when constructing attribute display text.
        /// The method assumes the attribute integer matches to only one attribute, and therefore exits after the 
        /// first match.
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="cycleAttributeList"></param>
        private void SetITNRoadType(uint[] attributes, CycleAttributeType cycleAttributeType)
        {
            // Set the default value
            if (string.IsNullOrEmpty(currentITNRoadType))
            {
                this.currentITNRoadType = street; // "street"
            }

            if ((attributes != null) && (attributes.Length > 0) && (attributes[0] != 0)) // 0 indicates no attributes defined)
            {
                // This is the road/street/path type attribute value. All we want to set here is the roadtype value
                // used later when constructing the characteristics attribute text

                // Get the cycle attributes we're allowed to display from service discovery
                ICycleAttributes cycleAttributesService = (ICycleAttributes)TDServiceDiscovery.Current[ServiceDiscoveryKey.CycleAttributes];

                // Get the attributes we can display for this list type
                CycleAttribute[] cycleAttributesArray = cycleAttributesService.GetCycleAttributes(cycleAttributeType, CycleAttributeGroup.ITN);

                // The attribute value we're comparing
                Int64 attributeValue = (Int64)attributes[0];

                foreach (CycleAttribute cycleAttribute in cycleAttributesArray)
                {
                    // Bit & because CycleService returns an integer value whose bits represent flags for 
                    // which cycle attributes are true
                    if ((cycleAttribute.CycleAttributeMask & attributeValue) > 0)
                    {
                        // Add the cycle type attributes to cycletype attribute list to determine path name later
                        cycleTypeAttributes.Add(cycleAttribute);

                        // Assumption:
                        // Only 1 attribute will be true for this 

                        this.currentITNRoadType = Global.tdResourceManager.GetString(
                            cycleAttribute.CycleAttributeResourceName, CultureInfo.CurrentUICulture);

                        // Assumption:
                        // Only the (cycle) path attributes are configured to be returned in the GetCycleAttributes(... CycleAttributeGroup.ITN)
                        // call. Therefore can assume, if there is a match, then we are on a path.
                        this.isPath = true;

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Method which sets up the class cycle attribute text strings for the current
        /// cycle instruction
        /// </summary>
        /// <param name="detail">Array index into cycleJourney.Details for journey instruction</param>
        private void SetupCycleAttributeDetails(int journeyDetailIndex)
        {
            CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];

            // Assumption:
            // Each Type of attribute set has 4 integers, each representing the attributes for a particular Group
            // [0] is ITN  // This is checked in the PreProcess method
            // [1] is User0
            // [2] is User1
            // [3] is User2
            // 
            // We only want to set the specific text strings if the attribute occurs in a Type as defined below

            StringBuilder attributeCrossing = new StringBuilder();       // Only set by Joining Link
            StringBuilder attributeType = new StringBuilder();           // Only set by Joining Link
            StringBuilder attributeBarrier = new StringBuilder();        // Only set by Interesting Link
            StringBuilder attributeCharacteristic = new StringBuilder(); // Only set by Interesting Link
            StringBuilder attributeManoeuvre = new StringBuilder();     // Only set by SectionFeatures Stopover

            StringBuilder attributeTemp = new StringBuilder(); // used as a dummy attribute text string, this is cleared and never displayed

            #region Joining Link

            if ((detail.JoiningSignificantLinkAttributes != null)
                && (detail.JoiningSignificantLinkAttributes.Length > 0))
            {
                // Go through each Group, and setup the Attribute Text for each Category

                GetAttributeTextForAttributeNumber(detail.JoiningSignificantLinkAttributes, 1, CycleAttributeType.Link,
                    ref attributeCrossing, ref attributeType, ref attributeTemp, ref attributeTemp, ref attributeTemp);

                GetAttributeTextForAttributeNumber(detail.JoiningSignificantLinkAttributes, 2, CycleAttributeType.Link,
                    ref attributeCrossing, ref attributeType, ref attributeTemp, ref attributeTemp, ref attributeTemp);

                GetAttributeTextForAttributeNumber(detail.JoiningSignificantLinkAttributes, 3, CycleAttributeType.Link,
                    ref attributeCrossing, ref attributeType, ref attributeTemp, ref attributeTemp, ref attributeTemp);
            }

            attributeTemp = new StringBuilder();
            #endregion

            #region Leaving Link
            /* Commented out because we currently do not want to set any attribute text using the Leaving Link attributes array
             * 
            if ((detail.LeavingSignificantLinkAttributes != null) 
                && (detail.LeavingSignificantLinkAttributes.Length > 0))
            {
                // Go through each Group, and setup the Attribute Text for each Category

                GetAttributeTextForAttributeNumber(detail.LeavingSignificantLinkAttributes, 1, CycleAttributeType.Link,
                    ref attributeCrossing, ref attributeType, ref attributeBarrierText, ref attributeCharacteristicText, ref attributeTemp);

                GetAttributeTextForAttributeNumber(detail.LeavingSignificantLinkAttributes, 2, CycleAttributeType.Link,
                    ref attributeCrossing, ref attributeType, ref attributeBarrierText, ref attributeCharacteristicText, ref attributeTemp);

                GetAttributeTextForAttributeNumber(detail.LeavingSignificantLinkAttributes, 3, CycleAttributeType.Link,
                    ref attributeCrossing, ref attributeType, ref attributeBarrierText, ref attributeCharacteristicText, ref attributeTemp);
            }

            attributeTemp = new StringBuilder(); */
            #endregion

            #region Interesting Link

            if ((detail.InterestingLinkAttributes != null)
                && (detail.InterestingLinkAttributes.Length > 0))
            {
                // Go through each Group, and setup the Attribute Text for each Category

                GetAttributeTextForAttributeNumber(detail.InterestingLinkAttributes, 1, CycleAttributeType.Link,
                    ref attributeTemp, ref attributeTemp, ref attributeBarrier, ref attributeCharacteristic, ref attributeTemp);

                GetAttributeTextForAttributeNumber(detail.InterestingLinkAttributes, 2, CycleAttributeType.Link,
                    ref attributeTemp, ref attributeTemp, ref attributeBarrier, ref attributeCharacteristic, ref attributeTemp);

                GetAttributeTextForAttributeNumber(detail.InterestingLinkAttributes, 3, CycleAttributeType.Link,
                    ref attributeTemp, ref attributeTemp, ref attributeBarrier, ref attributeCharacteristic, ref attributeTemp);
            }

            attributeTemp = new StringBuilder();
            #endregion

            #region SectionFeature Stopover

            if ((detail.SectionFeatureAttributes != null)
                && (detail.SectionFeatureAttributes.Length > 0))
            {
                // Go through each Group, and setup the Attribute Text for each Category

                GetAttributeTextForAttributeNumber(detail.SectionFeatureAttributes, 1, CycleAttributeType.Stopover,
                    ref attributeTemp, ref attributeTemp, ref attributeTemp, ref attributeTemp, ref attributeManoeuvre);

                GetAttributeTextForAttributeNumber(detail.SectionFeatureAttributes, 2, CycleAttributeType.Stopover,
                    ref attributeTemp, ref attributeTemp, ref attributeTemp, ref attributeTemp, ref attributeManoeuvre);

                GetAttributeTextForAttributeNumber(detail.SectionFeatureAttributes, 3, CycleAttributeType.Stopover,
                    ref attributeTemp, ref attributeTemp, ref attributeTemp, ref attributeTemp, ref attributeManoeuvre);
            }

            attributeTemp = new StringBuilder();
            #endregion

            // Finally tidyup the text and assign to the class level variables
            this.attributeCrossingText = TidyUpAttributeText(attributeCrossing);
            this.attributeTypeText = TidyUpAttributeText(attributeType);
            this.attributeBarrierText = TidyUpAttributeText(attributeBarrier);
            this.attributeCharacteristicText = TidyUpAttributeText(attributeCharacteristic);
            this.attributeManoeuvreText = TidyUpAttributeText(attributeManoeuvre);

        }

        /// <summary>
        /// Method which helps to set up the call to get the Attribute text for the specified index in the 
        /// attributes array. The CycleAttributeType specified is used to get the attributes allowed 
        /// array from the ServiceDiscovery.
        /// The method expects index values from 0 to 3. The index is also used to determine which 
        /// CycleAttributeGroup array to load, e.g. 0 is CycleAttributeGroup.ITN, 1 is CycleAttributeGroup.User0
        /// </summary>
        private void GetAttributeTextForAttributeNumber(uint[] attributes, int index, 
                                        CycleAttributeType cycleAttributeType,
                                        ref StringBuilder attributeCrossingText,
                                        ref StringBuilder attributeTypeText,
                                        ref StringBuilder attributeBarrierText,
                                        ref StringBuilder attributeCharacteristicText,
                                        ref StringBuilder attributeManoeuvreText)
        {

            if ((attributes.Length > index)
                    && (attributes[index] != 0)) // 0 indicates no attributes defined)
            {
                //Determine the Group this attribute integer belongs to
                CycleAttributeGroup cycleAttributeGroup = GetCycleAttributeGroup(index);
               
                // Get the cycle attributes we're allowed to display from service discovery
                ICycleAttributes cycleAttributesService = (ICycleAttributes)TDServiceDiscovery.Current[ServiceDiscoveryKey.CycleAttributes];

                // Get the attributes we can display for this list type
                CycleAttribute[] cycleAttributesArray = cycleAttributesService.GetCycleAttributes(cycleAttributeType, cycleAttributeGroup);

                // The attribute value we're comparing
                Int64 attributeValue = (Int64)attributes[index];

                // Set the text
                GetAttributeText(cycleAttributesArray, attributeValue, ref attributeCrossingText, ref attributeTypeText,
                    ref attributeBarrierText, ref attributeCharacteristicText, ref attributeManoeuvreText);
            }
        }

        /// <summary>
        /// Returns the CycleAttributeGroup (corresponding to the attribute "integer mask" group) for the supplied
        /// attribute integer array index
        /// </summary>
        /// <param name="attributeIndex"></param>
        /// <returns></returns>
        private CycleAttributeGroup GetCycleAttributeGroup(int attributeIndex)
        {
            //Determine the Group this attribute integer belongs to
            switch (attributeIndex)
            {
                case 1:
                    return CycleAttributeGroup.User0;
                    
                case 2:
                    return CycleAttributeGroup.User1;
                    
                case 3:
                    return CycleAttributeGroup.User2;
                    
                default:
                    return CycleAttributeGroup.ITN;                    
            }
        }

        /// <summary>
        /// Checks if the attributeValue Bit flags matches any in the cycleAttributesArray. If any do,
        /// they are added to the appropriate text string depending on the CycleAttribute.Category.
        /// </summary>
        private void GetAttributeText(  CycleAttribute[] cycleAttributesArray,
                                        Int64 attributeValue,
                                        ref StringBuilder attributeCrossingText,
                                        ref StringBuilder attributeTypeText,
                                        ref StringBuilder attributeBarrierText,
                                        ref StringBuilder attributeCharacteristicText,
                                        ref StringBuilder attributeManoeuvreText)
        {
            // Loop through the attributes we're allowed to display and set text accordingly
            foreach (CycleAttribute cycleAttribute in cycleAttributesArray)
            {
                // Bit & because CycleService returns an integer value whose bits represent flags for 
                // which cycle attributes are true
                if ((cycleAttribute.CycleAttributeMask & attributeValue) > 0)
                {
                    switch (cycleAttribute.CycleAttributeCategory)
                    {
                        case CycleAttributeCategory.Crossing:
                            // Check for sentence prefix
                            if (attributeCrossingText.Length == 0)
                            {
                                attributeCrossingText.Append(Global.tdResourceManager.GetString("CycleRouteText.PleaseCross", CultureInfo.CurrentUICulture));
                                attributeCrossingText.Append(space);
                            }

                            AddAttributeNumber(cycleAttribute, attributeValue, ref attributeCrossingText);
                            
                            attributeCrossingText.Append(
                                Global.tdResourceManager.GetString(
                                cycleAttribute.CycleAttributeResourceName, CultureInfo.CurrentUICulture));
                            
                            attributeCrossingText.Append(seperator);

                            break;

                        case CycleAttributeCategory.Type:
                            // Check for sentence prefix
                            if (attributeTypeText.Length == 0)
                            {
                                attributeTypeText.Append(Global.tdResourceManager.GetString("CycleRouteText.RouteUses", CultureInfo.CurrentUICulture));
                                attributeTypeText.Append(space);
                            }

                            cycleTypeAttributes.Add(cycleAttribute);

                            AddAttributeNumber(cycleAttribute, attributeValue, ref attributeTypeText);

                            attributeTypeText.Append(
                                Global.tdResourceManager.GetString(
                                cycleAttribute.CycleAttributeResourceName, CultureInfo.CurrentUICulture));

                            attributeTypeText.Append(seperator);

                            break;

                        case CycleAttributeCategory.Barrier:
                            // Check for sentence prefix
                            if (attributeBarrierText.Length == 0)
                            {
                                attributeBarrierText.Append(Global.tdResourceManager.GetString("CycleRouteText.PleaseNote", CultureInfo.CurrentUICulture));
                                attributeBarrierText.Append(space);
                            }

                            AddAttributeNumber(cycleAttribute, attributeValue, ref attributeBarrierText);

                            attributeBarrierText.Append(
                                Global.tdResourceManager.GetString(
                                cycleAttribute.CycleAttributeResourceName, CultureInfo.CurrentUICulture));

                            attributeBarrierText.Append(seperator);

                            break;

                        case CycleAttributeCategory.Characteristic:
                            // Check for sentence prefix
                            if (attributeCharacteristicText.Length == 0)
                            {
                                attributeCharacteristicText.Append(string.Format(
                                    Global.tdResourceManager.GetString("CycleRouteText.TheStreetIs", CultureInfo.CurrentUICulture),
                                    this.currentITNRoadType));
                                attributeCharacteristicText.Append(space);
                            }

                            AddAttributeNumber(cycleAttribute, attributeValue, ref attributeCharacteristicText);

                            attributeCharacteristicText.Append(
                                Global.tdResourceManager.GetString(
                                cycleAttribute.CycleAttributeResourceName, CultureInfo.CurrentUICulture));

                            attributeCharacteristicText.Append(seperator);

                            break;

                        case CycleAttributeCategory.Manoeuvrability:

                            AddAttributeNumber(cycleAttribute, attributeValue, ref attributeManoeuvreText);

                            attributeManoeuvreText.Append(
                                Global.tdResourceManager.GetString(
                                cycleAttribute.CycleAttributeResourceName, CultureInfo.CurrentUICulture));

                            attributeManoeuvreText.Append(seperator);

                            break;

                        default:
                            // Don't append any text
                            break;
                    }

                }
            }
        }

        /// <summary>
        /// Appends the Attribute number (returned by the Cycle Planner) and the CycleAttribute.Id value to the attributeText string
        /// </summary>
        /// <param name="attributeText"></param>
        private void AddAttributeNumber(CycleAttribute cycleAttribute, Int64 attributeValue, ref StringBuilder attributeText)
        {
            if (this.showAttributeNumber)
            {
                attributeText.Append("<span class=\"txterror\">");
                attributeText.Append(attributeValue.ToString());
                attributeText.Append(" ");
                attributeText.Append(cycleAttribute.CycleAttributeId.ToString());
                attributeText.Append(" </span>");
            }
        }

        /// <summary>
        /// Removes any trailing whitespace and commas, and replaces final comma with "and".
        /// e.g. Transforms "The path is partially lit, may be rough, can be wet, " 
        /// into "The path is partially lit, may be rough and can be wet" 
        /// </summary>
        /// <param name="attributeText"></param>
        /// <returns></returns>
        private string TidyUpAttributeText(StringBuilder attributeText)
        {
            if (string.IsNullOrEmpty(attributeText.ToString()))
            {
                return string.Empty;
            }
            else
            {
                // Remove the end comma
                string formattedText = attributeText.ToString().Trim().TrimEnd(',');

                int lastCommaIndex = formattedText.LastIndexOf(',');

                if (lastCommaIndex > 0)
                {
                    // Remove and replace the last comma with an "and"
                    formattedText = formattedText.Remove(lastCommaIndex, 1);
                    formattedText = formattedText.Insert(lastCommaIndex, " " + Global.tdResourceManager.GetString("CycleRouteText.And"));
                }

                return formattedText;
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// Returns a formatted string of the directions for the current
        /// cycling instruction e.g. "Continue on to FOXHUNT GROVE".
        /// </summary>
        /// <param name="detail">Array index into cycleJourney.Details for journey instruction</param>
        /// <returns>Formatted string of the directions</returns>
        protected virtual string GetDirections(int journeyDetailIndex, bool showCongestionCharge)
        {
            //temp variables for this method
            string routeText = String.Empty;

            #region Setup current, last, next, previous CycleJourneyDetail objects
            //assign the detail to be formatted as an instruction
            CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];
            
            //last Detail, which is also needed for formatting logic			
            CycleJourneyDetail lastDetail = cycleJourney.Details[cycleJourney.Details.Length - 1];

            //next Detail, which is also needed for formatting logic 
            CycleJourneyDetail nextDetail = null;
            if (detail != lastDetail)
            {
                nextDetail = cycleJourney.Details[journeyDetailIndex + 1];
            }
            else
            {
                nextDetail = lastDetail;
            }

            //previous Detail, which is also needed for formatting logic
            CycleJourneyDetail previousDetail = null;
            if (journeyDetailIndex > 0)
            {
                previousDetail = cycleJourney.Details[journeyDetailIndex - 1];
            }
            else
            {
                previousDetail = detail;
            }
            #endregion

            bool nextDetailHasJunctionExitJunction = false;
            if (detail != lastDetail)
            {
                nextDetailHasJunctionExitJunction = ((nextDetail.JunctionSection) &&
                    ((nextDetail.JunctionType == JunctionType.Exit) ||
                    (nextDetail.JunctionType == JunctionType.Merge)));
            }

            // if this drive section has a crossing cycle attribute, then we don't want to display the 
            // drive instruction, and instead just want to show the crossing attribute text
            if (this.isCrossing)
            {
                routeText = attributeCrossingText;

                // Add the distance to the running count
                accumulatedDistance += detail.Distance;
            }
            //Returns specific text depending on the StopoverSection
            else if (detail.StopoverSection)
            {
                #region Stopover section

                if (detail.ComplexManoeuvre)
                {
                    routeText = ComplexManoeuvre(detail);
                }
                if (detail.NamedAccessRestriction)
                {
                    routeText = NamedAccessRestriction(detail);
                }
                if (detail.NamedTime)
                {
                    routeText = NamedTime(detail);
                }
                if (detail.CongestionZoneEntry != null)
                {
                    routeText = CongestionEntry(detail, showCongestionCharge);
                }
                if (detail.CongestionZoneExit != null)
                {
                    routeText = CongestionExit(detail, showCongestionCharge);
                }
                if (detail.CongestionZoneEnd != null)
                {
                    routeText = CongestionEnd(detail, showCongestionCharge);
                }
                if (detail.FerryCheckInName != null)
                {
                    //We need to pass the previous detail so we can find out if it was 
                    //an UndefinedWait or not (as this changes the instruction passed to the user)
                    routeText = FerryEntry(detail, previousDetail);
                }
                if (detail.TollEntryName != null)
                {
                    routeText = TollEntry(detail);
                }
                if (detail.TollExitName != null)
                {
                    routeText = TollExit(detail);
                }
                if (detail.ViaLocation == true)
                {
                    routeText = ViaLocation();
                }
                if (detail.FerryCheckOut == true)
                {
                    routeText = FerryExit(detail);
                }
                if (detail.WaitSection == true)
                {
                    routeText = WaitForFerry(detail, previousDetail, nextDetail);
                }
                if (detail.UndefinedWaitSection == true)
                {
                    routeText = UndefindedFerryWait(detail, previousDetail, nextDetail);
                }

                #endregion
            }
            else
            {
                #region Drive section
                //check turns and format accordingly
                if (this.isRoundabout)
                {
                    routeText = Roundabout(detail);
                }
                else
                {
                    if (detail.TurnAngle == TurnAngle.Continue)
                        routeText = TurnAngleContinue(journeyDetailIndex);
                    else if (detail.TurnAngle == TurnAngle.Bear)
                        routeText = TurnAngleBear(journeyDetailIndex);
                    else if (detail.TurnAngle == TurnAngle.Turn)
                        routeText = TurnAngleTurn(journeyDetailIndex);
                    else if (detail.TurnAngle == TurnAngle.Return)
                        routeText = TurnAngleReturn(journeyDetailIndex);
                }
                
                //check if the current cycle journey detail is a motorway junction section
                if (detail.JunctionSection)
                {
                    //add junction number to the instruction
                    routeText = FormatMotorwayJunction(detail, routeText);
                }

                //Add formatting place holder for where the road splits
                routeText += "{0}";

                //check if place name was returned
                bool placeNameExists = false;
                if (detail.PlaceName != null && detail.PlaceName.Length > 0)
                {
                    if ((detail.JunctionType == JunctionType.Entry) && detail.JunctionSection &&
                        (detail.Distance < immediateTurnDistance) &&
                        nextDetailHasJunctionExitJunction)
                    {
                        //don't add place name
                    }
                    else
                    {
                        routeText = AddPlaceName(detail, routeText);
                    }
                    placeNameExists = true;
                }

                //Add formatting place holder for where the road splits
                routeText += "{1}";

                if (detail.JunctionSection &&
                    ((detail.JunctionType == JunctionType.Exit) ||
                    (detail.JunctionType == JunctionType.Merge)) &&
                    (detail.Distance > immediateTurnDistance))
                {
                    //don't add continue for
                }
                else
                {
                    //Add 'continue for' 
                    routeText = AddContinueFor(detail, nextDetailHasJunctionExitJunction, routeText);
                }

                //Add formatting place holder for where the road splits
                routeText += "{2}";

                //check the next Detail providing we are not on the last detail already 
                if (detail != lastDetail)
                {
                    if (!((detail.JunctionType == JunctionType.Exit) ||
                        (detail.JunctionType == JunctionType.Merge)))
                    {
                        //amend the current instruction if necessary
                        routeText = CheckNextDetail(nextDetail, routeText);
                    }
                }

                //Add formatting place holder for where the road splits
                routeText += "{3}";

                if (detail.Congestion == true)
                {
                    routeText = AddCongestionSymbol(routeText);
                }

                // Add the distance to the running count
                accumulatedDistance += detail.Distance;

                
                //Add where the road splits to the appropriate place
                
                //Fill in the place holders for where the road splits for Motorway Entry
                if ((detail.JunctionType == JunctionType.Entry) &&
                    (detail.JunctionSection) && (detail.RoadSplits))
                {

                    if (nextDetailHasJunctionExitJunction && (!placeNameExists) &&
                        (detail.Distance < immediateTurnDistance))
                    {
                        routeText = String.Format(routeText,
                            String.Empty, String.Empty, String.Empty, space + whereRoadSplits);
                    }
                    else routeText = String.Format(routeText, space + whereRoadSplits,
                             String.Empty, String.Empty, String.Empty);

                }
                else routeText = String.Format(routeText, String.Empty,
                         String.Empty, String.Empty, String.Empty);
                #endregion
            }

            //return complete formatted instruction
            return routeText;
        }       

        #region Text Generators

        /// <summary>
        /// Return a congestion symbol next to the route text.
        /// </summary>
        /// <param name="routeText">Route directions</param>
        /// <returns>Congestion symbol</returns>
        protected virtual string AddCongestionSymbol(string routeText)
        {
            routeText += Global.tdResourceManager.GetString("CarCostingDetails.highTrafficSymbol");
            return routeText;
        }

        /// <summary>
        /// Generates the route text for the given CycleJourney where
        /// the CycleJourney goes over a "Roundabout".
        /// </summary>
        /// <param name="detail">Cycle Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string Roundabout(CycleJourneyDetail detail)
        {
            string routeText = String.Empty;
            string thisRoad = FormatRoadName(detail);

            switch (detail.TurnCount)
            {
                case 1: routeText = roundaboutExitOne; break;
                case 2: routeText = roundaboutExitTwo; break;
                case 3: routeText = roundaboutExitThree; break;
                case 4: routeText = roundaboutExitFour; break;
                case 5: routeText = roundaboutExitFive; break;
                case 6: routeText = roundaboutExitSix; break;
                case 7: routeText = roundaboutExitSeven; break;
                case 8: routeText = roundaboutExitEight; break;
                case 9: routeText = roundaboutExitNine; break;
                case 10: routeText = roundaboutExitTen; break;
            }

            routeText += space + thisRoad;

            if (detail.RoadSplits && !detail.JunctionSection)
            {
                routeText += space + whereRoadSplits;
            }

            return routeText;
        }

        /// <summary>
        /// Generates the route text for the given CycleJourneyDetail where
        /// the CycleJourneyDetail is a StopoverSection of type 'TollEntry'.
        /// </summary>
        /// <param name="detail">Cycle Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string TollEntry(CycleJourneyDetail detail)
        {
            StringBuilder routeText = new StringBuilder(string.Empty);

            //Convert TollCost value to pounds & pence
            decimal pence = (Convert.ToDecimal(detail.TollCost)) / 100;
            string cost = string.Format("{0:C}", pence);

            //display company url as hyperplink if it exists
            if (detail.CompanyUrl.Length != 0 && !printable)
            {
                //add "Toll:" to start of instruction
                routeText.Append(toll);
                routeText.Append(space);
                routeText.Append("<a href=\"");
                routeText.Append("http://");
                routeText.Append(detail.CompanyUrl.Trim());
                routeText.Append("\" target=\"_blank\">");
                routeText.Append(detail.TollEntryName);
                routeText.Append(space);
                routeText.Append(openNewWindowImageUrl);
                routeText.Append("</a>");
            }
            else
            {
                routeText.Append(toll);
                routeText.Append(space);
                routeText.Append(detail.TollEntryName);
            }


            //If price in pence is less than zero return the route text, otherwise
            //return route text inclusive of the charge.
            if (pence < 0)
            {
                //when charge is unknown "Charge: Not Available"
                routeText.Append(space);
                routeText.Append("<b>");
                routeText.Append(chargeAdultAndCycle);
                routeText.Append(notAvailable);
                routeText.Append("</b>");
            }
            else if (pence == 0)
            {
                routeText.Append(space);
                routeText.Append("<b>");
                routeText.Append(chargeAdultAndCycle);
                routeText.Append("£0.00");
                routeText.Append("</b>");
            }
            else
            {
                routeText.Append(space);
                routeText.Append("<b>");
                routeText.Append(chargeAdultAndCycle);
                routeText.Append(cost);
                routeText.Append("</b>");
            }
            return routeText.ToString();
        }

        /// <summary>
        /// Generates the route text for the given CycleJourneyDetail where
        /// the CycleJourneyDetail is a StopoverSection of type 'TollEntry'.
        /// </summary>
        /// <param name="detail">Cycle Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string TollExit(CycleJourneyDetail detail)
        {
            StringBuilder routeText = new StringBuilder(string.Empty);

            //Convert TollCost value to pounds & pence
            decimal pence = (Convert.ToDecimal(detail.TollCost)) / 100;
            string cost = string.Format("{0:C}", pence);

            //display company url as hyerplink if it exists
            if (detail.CompanyUrl.Length != 0 && !printable)
            {
                //add "Toll:" to start of instruction	
                routeText.Append(toll);
                routeText.Append(space);
                routeText.Append("<a href=\"");
                routeText.Append("http://");
                routeText.Append(detail.CompanyUrl.Trim());
                routeText.Append("\" target=\"_blank\">");
                routeText.Append(detail.TollExitName);
                routeText.Append(space);
                routeText.Append(openNewWindowImageUrl);
                routeText.Append("</a>");
            }
            else
            {
                routeText.Append(toll);
                routeText.Append(space);
                routeText.Append(detail.TollExitName);
            }

            routeText.Append(space);
            routeText.Append("<b>");
            routeText.Append(charge);

            return routeText.ToString();
        }

        /// <summary>
        /// Generates the route text for the given CycleJourneyDetail where
        /// the CycleJourneyDetail is a StopoverSection of type 'FerryEntry'.
        /// </summary>
        /// <param name="detail">Cycle Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string FerryEntry(CycleJourneyDetail detail, CycleJourneyDetail previousDetail)
        {

            bool previousInstruction = previousDetail.UndefinedWaitSection;

            StringBuilder routeText = new StringBuilder(string.Empty);
            decimal pence = (Convert.ToDecimal(detail.TollCost)) / 100;
            string cost = string.Format("{0:C}", pence);

            TimeSpan span = new TimeSpan(0, 0, (int)detail.Duration);
            TDDateTime arrivalTime = currentArrivalTime;
            currentArrivalTime = currentArrivalTime.Add(span);
            string time = FormatDateTime(currentArrivalTime);

            //add "Board:" to start of instruction			
            //If previous instruction was an undefinedWait we don't want to display a time(as we don't know it)
            if (previousInstruction)
            {
                //display company url as hyperplink if it exists
                if (detail.CompanyUrl.Length != 0 && !printable)
                {
                    routeText.Append(board);
                    routeText.Append(space);
                    routeText.Append("<a href=\"");
                    routeText.Append("http://");
                    routeText.Append(detail.CompanyUrl.Trim());
                    routeText.Append("\" target=\"_blank\">");
                    routeText.Append(detail.FerryCheckInName);
                    routeText.Append(space);
                    routeText.Append(openNewWindowImageUrl);
                    routeText.Append("</a>");
                }
                else
                {
                    routeText.Append(board);
                    routeText.Append(space);
                    routeText.Append(detail.FerryCheckInName);
                }
            }
            else
            {
                //display company url as hyperplink if it exists
                if (detail.CompanyUrl.Length != 0 && !printable)
                {
                    routeText.Append(board);
                    routeText.Append(space);
                    routeText.Append("<a href=\"");
                    routeText.Append("http://");
                    routeText.Append(detail.CompanyUrl.Trim());
                    routeText.Append("\" target=\"_blank\">");
                    routeText.Append(detail.FerryCheckInName);
                    routeText.Append(space);
                    routeText.Append(openNewWindowImageUrl);
                    routeText.Append("</a>");
                    routeText.Append(space);
                    routeText.Append(departingAt);
                    routeText.Append(space);
                    routeText.Append(time);
                }
                else
                {
                    routeText.Append(board);
                    routeText.Append(space);
                    routeText.Append(detail.FerryCheckInName);
                    routeText.Append(space);
                    routeText.Append(departingAt);
                    routeText.Append(space);
                    routeText.Append(time);
                }
            }

            //If price in pence is less than zero return the route text, otherwise
            //return route text inclusive of the charge.
            if (pence < 0)
            {
                //when charge is unknown "Charge: Not Available"
                routeText.Append(";");
                routeText.Append(space);
                routeText.Append("<b>");
                routeText.Append(chargeAdultAndCycle);
                routeText.Append(notAvailable);
                routeText.Append("</b>");
            }
            else if (pence == 0)
            {
                //when charge is unknown "Charge: Not Available"
                routeText.Append(";");
                routeText.Append(space);
                routeText.Append("<b>");
                routeText.Append(chargeAdultAndCycle);
                routeText.Append("£0.00");
                routeText.Append("</b>");
            }
            else
            {
                routeText.Append(";");
                routeText.Append(space);
                routeText.Append("<b>");
                routeText.Append(chargeAdultAndCycle);
                routeText.Append(cost);
                routeText.Append("</b>");
            }
            return routeText.ToString();
        }

        /// <summary>
        /// Generates the route text for the given CycleJourneyDetail where
        /// the CycleJourneyDetail is a StopoverSection of type 'FerryExit'.
        /// </summary>
        /// <param name="detail">Cycle Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string FerryExit(CycleJourneyDetail detail)
        {
            TimeSpan span = new TimeSpan(0, 0, (int)detail.Duration);
            currentArrivalTime = currentArrivalTime.Add(span);

            string routeText = String.Empty;

            routeText = leaveFerry;
            return routeText;
        }

        /// <summary>
        /// Generates the route text for the given CycleJourneyDetail where
        /// the CycleJourneyDetail is a StopoverSection of type 'Wait'.
        /// </summary>
        /// <param name="detail">Cycle Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string WaitForFerry(CycleJourneyDetail detail, CycleJourneyDetail previousDetail, CycleJourneyDetail nextDetail)
        {
            string routeText = String.Empty;

            if (nextDetail.RoadNumber == "FERRY" && previousDetail.RoadNumber == "FERRY")
            {
                routeText = intermediateFerryWait;
            }
            else
            {
                if (previousDetail.FerryCheckOut)
                {
                    routeText = waitAtTerminal;
                }
                else
                {
                    routeText = ferryWait;
                }
            }
            return routeText;
        }

        /// <summary>
        /// Generates the route text for the given CycleJourneyDetail where
        /// the CycleJourneyDetail is a StopoverSection of type 'UndefindedWait'.
        /// </summary>
        /// <param name="detail">Cycle Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string UndefindedFerryWait(CycleJourneyDetail detail, CycleJourneyDetail previousDetail, CycleJourneyDetail nextDetail)
        {
            string routeText = String.Empty;

            if (nextDetail.RoadNumber == "FERRY" && previousDetail.RoadNumber == "FERRY")
            {
                routeText = intermediateFerryWait;
            }
            else
            {
                if (previousDetail.FerryCheckOut)
                {
                    routeText = waitAtTerminal;
                }
                else
                {
                    routeText = unspecifedFerryWait;
                }
            }
            return routeText;
        }

        /// <summary>
        /// Generates the complex manoeuvre text for the given CycleJourneyDetail where
        /// the CycleJourneyDetail is a StopoverSection of type 'ComplexManoeuvre'.
        /// </summary>
        /// <param name="detail">Cycle Journey Detail to generate complex manoeuvre text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string ComplexManoeuvre(CycleJourneyDetail detail)
        {
            string routeText = detail.ManoeuvreText;
            
            return routeText;
        }

        /// <summary>
        /// Generates the Named access restriction text for the given CycleJourneyDetail where
        /// the CycleJourneyDetail is a StopoverSection of type 'NamedAccessRestriction'.
        /// </summary>
        /// <param name="detail">Cycle Journey Detail to generate named access restriction text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string NamedAccessRestriction(CycleJourneyDetail detail)
        {
            string routeText = Global.tdResourceManager.GetString
                ("CycleRouteText.TimeBasedAccessRestriction", CultureInfo.CurrentUICulture);
                
            routeText += detail.NamedAccessRestrictionText;
            
            return routeText;
        }

        /// <summary>
        /// Generates the Named access restriction text for the given CycleJourneyDetail where
        /// the CycleJourneyDetail is a StopoverSection of type 'NamedAccessRestriction'.
        /// </summary>
        /// <param name="detail">Cycle Journey Detail to generate named access restriction text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string NamedTime(CycleJourneyDetail detail)
        {
            string routeText = Global.tdResourceManager.GetString
                ("CycleRouteText.NamedTime", CultureInfo.CurrentUICulture);

            routeText += detail.NamedTimeText;

            return routeText;
        }
        /// <summary>
        /// Generates the route text for the given CycleJourneyDetail where
        /// the CycleJourneyDetail is a StopoverSection of type 'CongestionEntry'.
        /// </summary>
        /// <param name="detail">Cycle Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string CongestionEntry(CycleJourneyDetail detail, bool showCongestionCharge)
        {
            StringBuilder routeText = new StringBuilder(string.Empty);
            decimal pence = (Convert.ToDecimal(detail.TollCost)) / 100;
            string cost = string.Format("{0:C}", pence);

            //display company name as hyperplink if it exists
            if (detail.CompanyUrl.Length != 0 && !printable)
            {
                routeText.Append(enter);
                routeText.Append(space);
                routeText.Append("<a href=\"");
                routeText.Append("http://");
                routeText.Append(detail.CompanyUrl.Trim());
                routeText.Append("\" target=\"_blank\">");
                routeText.Append(detail.CongestionZoneEntry);
                routeText.Append(space);
                routeText.Append(openNewWindowImageUrl);
                routeText.Append("</a>");
            }
            else
            {
                routeText.Append(enter);
                routeText.Append(space);
                routeText.Append(detail.CongestionZoneEntry);
            }

            // If there is a toll charge
            // Check if we already have this C Charge in ViewState or 
            // there is a cost and showCongestionCharge
            if ((detail.TollCost > 0) && ((showCongestionCharge) ||
                (!journeyViewState.VisitedCongestionCompany.Contains(detail.CompanyUrl))))
            {
                //Viewstate contains this C Charge and we don't want to show it
                if (journeyViewState.VisitedCongestionCompany.Contains(detail.CompanyUrl) && (!showCongestionCharge))
                {
                    // No toll charge for this day/time
                    routeText.Append(fullstop);
                    routeText.Append(space);
                    routeText.Append("<b>");
                    routeText.Append(charge);
                    routeText.Append("£0.00");
                    routeText.Append("</b>");
                    routeText.Append(space);
                    routeText.Append(certainTimesNoCharge);
                }
                else

                //We want to show it
                {
                    journeyViewState.VisitedCongestionCompany.Add(detail.CompanyUrl);
                    routeText.Append(fullstop);
                    routeText.Append(space);
                    routeText.Append("<b>");
                    routeText.Append(charge);
                    routeText.Append(cost);
                    routeText.Append("</b>");
                    routeText.Append(space);
                    routeText.Append(certainTimes);

                    if (outward)
                        journeyViewState.CongestionChargeAdded = true;
                }
            }

            else if ((detail.TollCost == 0) || (!showCongestionCharge))
            {
                // No toll charge for this day/time
                routeText.Append(fullstop);
                routeText.Append(space);
                routeText.Append("<b>");
                routeText.Append(charge);
                routeText.Append("£0.00");
                routeText.Append("</b>");
                routeText.Append(space);
                routeText.Append(certainTimesNoCharge);
            }
            else
            {
                //in the event the charge is unavailable - IR2499
                routeText.Append(space);
                routeText.Append("<b>");
                routeText.Append(charge);
                routeText.Append(notAvailable);
                routeText.Append("</b>");
            }

            return routeText.ToString();
        }

        /// <summary>
        /// Generates the route text for the given CycleJourneyDetail where
        /// the CycleJourneyDetail is a StopoverSection of type 'CongestionExit'.
        /// </summary>
        /// <param name="detail">Cycle Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string CongestionExit(CycleJourneyDetail detail, bool showCongestionCharge)
        {
            StringBuilder routeText = new StringBuilder(string.Empty);
            decimal pence = (Convert.ToDecimal(detail.TollCost)) / 100;
            string cost = string.Format("{0:C}", pence);

            if (detail.CompanyUrl.Length != 0 && !printable)
            {
                //add "Exit" to start of instruction						
                routeText.Append(exit);
                routeText.Append(space);
                routeText.Append("<a href=\"");
                routeText.Append("http://");
                routeText.Append(detail.CompanyUrl.Trim());
                routeText.Append("\" target=\"_blank\">");
                routeText.Append(detail.CongestionZoneExit);
                routeText.Append(space);
                routeText.Append(openNewWindowImageUrl);
                routeText.Append("</a>");
            }
            else
            {
                //add "Exit" to start of instruction						
                routeText.Append(exit);
                routeText.Append(space);
                routeText.Append(detail.CongestionZoneExit);
            }


            // If there is a toll charge
            // Check if we already have this C Charge in ViewState or 
            // there is a cost and showCongestionCharge
            if ((detail.TollCost > 0) && ((showCongestionCharge) ||
                (!journeyViewState.VisitedCongestionCompany.Contains(detail.CompanyUrl))))
            {
                //Viewstate contains this C Charge and we don't want to show it
                if (journeyViewState.VisitedCongestionCompany.Contains(detail.CompanyUrl) && (!showCongestionCharge))
                {
                    // No toll charge for this day/time
                    routeText.Append(fullstop);
                    routeText.Append(space);
                    routeText.Append("<b>");
                    routeText.Append(charge);
                    routeText.Append("£0.00");
                    routeText.Append("</b>");
                    routeText.Append(space);
                    routeText.Append(certainTimesNoCharge);
                }
                else
                //We want to show it
                {
                    journeyViewState.VisitedCongestionCompany.Add(detail.CompanyUrl);
                    routeText.Append(fullstop);
                    routeText.Append(space);
                    routeText.Append("<b>");
                    routeText.Append(charge);
                    routeText.Append(cost);
                    routeText.Append("</b>");
                    routeText.Append(space);
                    routeText.Append(certainTimes);

                    if (outward)
                        journeyViewState.CongestionChargeAdded = true;

                }
            }
            else if ((detail.TollCost == 0) || (!showCongestionCharge))
            {
                // No toll charge, so dont want to display the charge text
            }
            else
            {
                //in the event the charge is unavailable - IR2499
                routeText.Append(space);
                routeText.Append("<b>");
                routeText.Append(charge);
                routeText.Append(notAvailable);
                routeText.Append("</b>");
            }

            return routeText.ToString();
        }

        /// <summary>
        /// Generates the route text for the given CycleJourneyDetail where
        /// the CycleJourneyDetail is a StopoverSection of type 'CongestionEnd'.
        /// </summary>
        /// <param name="detail">Cycle Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string CongestionEnd(CycleJourneyDetail detail, bool showCongestionCharge)
        {
            StringBuilder routeText = new StringBuilder(string.Empty);
            decimal pence = (Convert.ToDecimal(detail.TollCost)) / 100;
            string cost = string.Format("{0:C}", pence);

            if (detail.CompanyUrl.Length != 0 && !printable)
            {
                //add "End" to start of instruction						
                routeText.Append(end);
                routeText.Append(space);
                routeText.Append("<a href=\"");
                routeText.Append("http://");
                routeText.Append(detail.CompanyUrl.Trim());
                routeText.Append("\" target=\"_blank\">");
                routeText.Append(detail.CongestionZoneEnd);
                routeText.Append(space);
                routeText.Append(openNewWindowImageUrl);
                routeText.Append("</a>");
            }
            else
            {
                //add "End" to start of instruction						
                routeText.Append(end);
                routeText.Append(space);
                routeText.Append(detail.CongestionZoneEnd);
            }


            // If there is a toll charge
            // Check if we already have this C Charge in ViewState or 
            // there is a cost and showCongestionCharge
            if ((detail.TollCost > 0) && ((showCongestionCharge) ||
                (!journeyViewState.VisitedCongestionCompany.Contains(detail.CompanyUrl))))
            {
                //Viewstate contains this C Charge and we don't want to show it
                if (journeyViewState.VisitedCongestionCompany.Contains(detail.CompanyUrl) && (!showCongestionCharge))
                {
                    // No toll charge for this day/time
                    routeText.Append(fullstop);
                    routeText.Append(space);
                    routeText.Append("<b>");
                    routeText.Append(charge);
                    routeText.Append("£0.00");
                    routeText.Append("</b>");
                    routeText.Append(space);
                    routeText.Append(certainTimesNoCharge);
                }
                else
                //We want to show it
                {
                    journeyViewState.VisitedCongestionCompany.Add(detail.CompanyUrl);
                    routeText.Append(fullstop);
                    routeText.Append(space);
                    routeText.Append("<b>");
                    routeText.Append(charge);
                    routeText.Append(cost);
                    routeText.Append("</b>");
                    routeText.Append(space);
                    routeText.Append(certainTimes);

                    if (outward)
                        journeyViewState.CongestionChargeAdded = true;

                }
            }
            else if ((detail.TollCost == 0) || (!showCongestionCharge))
            {
                // No toll charge, so dont want to display the charge text
            }
            else
            {
                //in the event the charge is unavailable
                routeText.Append(space);
                routeText.Append("<b>");
                routeText.Append(charge);
                routeText.Append(notAvailable);
                routeText.Append("</b>");
            }

            return routeText.ToString();
        }

        /// <summary>
        /// Generates the route text for the given CycleJourneyDetail where
        /// the CycleJourneyDetail is a StopoverSection of type 'ViaLocation'.
        /// </summary>
        /// <param name="detail">Cyle Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string ViaLocation()
        {
            string routeText = String.Empty;

            //add "Arrive at" to start of instruction			
            routeText = viaArriveAt + space + "<b>" + cycleJourney.RequestedViaLocation.Description + "</b>";
            return routeText;
        }

        /// <summary>
        /// Generate the text where the turn angle of the given
        /// CycleJourneyDetail is "Continue". This method assumes that
        /// the turn angle for the given detail is "Continue".
        /// </summary>
        /// <param name="journeyDetailIndex">Array index into cycleJourney.Details</param>
        /// <returns>Route text.</returns>
        protected virtual string TurnAngleContinue(int journeyDetailIndex)
        {
            CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];

            string nextRoad = FormatRoadName(detail);
            string routeText = String.Empty;
            bool addedWhereRoadSplits = false;


            //When the CyclePlanner produces a JunctionDriveSection of type Merge (or Exit) 
            //without a junction number (regardless of whether the junction is indicated as ambiguous) 
            //for a Motorway junction (including slip roads) then ignore the turn count 
            //and the immediate parameter. 			
            if (detail.JunctionDriveSectionWithoutJunctionNo &&
                (detail.JunctionType == JunctionType.Exit || detail.JunctionType == JunctionType.Merge))
            {
                string straightOnInstruction =
                    ((detail.PlaceName == null || detail.PlaceName.Length == 0) &&
                    (detail.Distance > immediateTurnDistance) && detail.RoadSplits) ?
                        String.Empty : space + straightOn;

                if (detail.TurnDirection == TurnDirection.Continue)
                    routeText = follow + space + nextRoad + straightOnInstruction;
                else if (detail.TurnDirection == TurnDirection.Right)
                    routeText = follow + space + nextRoad + straightOnInstruction;
                else if (detail.TurnDirection == TurnDirection.Left)
                    routeText = follow + space + nextRoad + straightOnInstruction;
            }
            else if (!detail.JunctionSection)
            {
                if ((detail.TurnDirection == TurnDirection.Continue) ||
                    (detail.TurnDirection == TurnDirection.Left) ||
                    (detail.TurnDirection == TurnDirection.Right))
                {

                    if ((detail.PlaceName == null || detail.PlaceName.Length == 0) &&
                        (detail.Distance > immediateTurnDistance) && detail.RoadSplits)
                    {
                        routeText = follow + space + nextRoad;
                    }
                    else
                    {
                        routeText = follow + space + nextRoad + space + straightOn;

                    }
                }               
            }
            else if (detail.JunctionSection)
            {
                //Motorway entry 
                if ((detail.JunctionType == JunctionType.Entry) &&
                    (detail.JunctionNumber != null) && (detail.JunctionNumber.Length > 0))
                {
                    routeText = join + space + nextRoad;
                    return routeText; //without where the road splits
                }

                if (detail.TurnDirection == TurnDirection.Continue)
                {
                    routeText = (detail.RoadSplits) ? follow + space + nextRoad + space + straightOn :
                        join + space + nextRoad;
                }
                else if (detail.TurnDirection == TurnDirection.Right)
                {
                    routeText = (detail.RoadSplits) ? follow + space + nextRoad + space + straightOn :
                        join + space + nextRoad;
                }
                else if (detail.TurnDirection == TurnDirection.Left)
                {
                    routeText = (detail.RoadSplits) ? follow + space + nextRoad + space + straightOn :
                        join + space + nextRoad;
                }
            }
            //if detail does not include a motorway junction

            if (detail.RoadSplits && !addedWhereRoadSplits)
            {
                routeText += space + whereRoadSplits;
            }

            return routeText;
        }

        /// <summary>
        /// Generates the route text for the given CycleJourney where
        /// the TurnAngle for the given CycleJourney is "Bear".  This method
        /// assumes that the TurnAngle is "Bear".
        ///
        /// Note that for "counted turns" (TurnCount 1 - 4) the text is actually
        /// "Take the ..." which can be used for both "bear" and "turn".
        /// </summary>
        /// <param name="journeyDetailIndex">Array index into cycleJourney.Details</param>
        /// <returns>Route text.</returns>
        protected virtual string TurnAngleBear(int journeyDetailIndex)
        {
            CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];

            int previousDistance = 0;
            if (journeyDetailIndex > 0)
            {
                previousDistance = cycleJourney.Details[journeyDetailIndex - 1].Distance;
            };

            string nextRoad = FormatRoadName(detail);
            string routeText = String.Empty;

            //When the CyclePlanner produces a JunctionDriveSection of type Merge (or Exit) 
            //without a junction number (regardless of whether the junction is indicated as ambiguous) 
            //for a Motorway junction (including slip roads) then ignore the turn count 
            //and the immediate parameter. 
            if (detail.JunctionDriveSectionWithoutJunctionNo &&
                (detail.JunctionType == JunctionType.Exit || detail.JunctionType == JunctionType.Merge))
            {
                if (detail.TurnDirection == TurnDirection.Continue)
                {
                    string straightOnInstruction = space + straightOn;

                    if (detail.TurnDirection == TurnDirection.Continue)
                        routeText = follow + space + nextRoad + straightOnInstruction;
                    else if (detail.TurnDirection == TurnDirection.Right)
                        routeText = follow + space + nextRoad + straightOnInstruction;
                    else if (detail.TurnDirection == TurnDirection.Left)
                        routeText = follow + space + nextRoad + straightOnInstruction;
                }
                else
                {
                    if (detail.TurnDirection == TurnDirection.Left)
                        routeText = bearLeft + space + nextRoad;
                    else if (detail.TurnDirection == TurnDirection.Right)
                        routeText = bearRight + space + nextRoad;
                }

                if (detail.RoadSplits)
                    routeText += space + whereRoadSplits;

                return routeText;
            }

            // Check to see if this detail is a junction drive section
            if (detail.JunctionSection)
            {
                if (detail.TurnDirection == TurnDirection.Left)
                {
                    //Motorway entry 
                    if ((detail.JunctionType == JunctionType.Entry) &&
                        (detail.JunctionNumber != null) && (detail.JunctionNumber.Length > 0))
                    {
                        routeText = bearLeftToJoin;
                    }
                }
                else if (detail.TurnDirection == TurnDirection.Right)
                {
                    //Motorway entry 
                    if ((detail.JunctionType == JunctionType.Entry) &&
                        (detail.JunctionNumber != null) && (detail.JunctionNumber.Length > 0))
                    {
                        routeText = bearRightToJoin;
                    }
                }
                else if (detail.TurnDirection == TurnDirection.Continue)
                    //check flag for ambiguous junction
                    routeText = (detail.RoadSplits) ? continueString : join;
                
                // Append the next road to the route text
                routeText += space + nextRoad;
            }
            //this detail is a drive section
            else
            {
                if (detail.TurnDirection == TurnDirection.Left)
                {
                    switch (detail.TurnCount)
                    {
                        case 1:

                            // Check to see if the distance of this leg is less than the
                            // immediate turn distance.
                            if (journeyDetailIndex > 0)
                            {
                                if (previousDistance < immediateTurnDistance)
                                    routeText = immediatelyBearLeft;
                                else
                                    routeText = isMiniRoundabout ? turnLeftOne2 + atMiniRoundabout2 + onto : turnLeftOne;
                            }
                            else
                            {
                                routeText = isMiniRoundabout ? turnLeftOne2 + atMiniRoundabout2 + onto : turnLeftOne;
                            }

                            break;

                        case 2: routeText = isMiniRoundabout ? turnLeftTwo2 + atMiniRoundabout2 + onto : turnLeftTwo; break;
                        case 3: routeText = isMiniRoundabout ? turnLeftThree2 + atMiniRoundabout2 + onto : turnLeftThree; break;
                        case 4: routeText = isMiniRoundabout ? turnLeftFour2 + atMiniRoundabout2 + onto : turnLeftFour; break;

                        default: routeText = bearLeft; break;
                    }
                }
                else if (detail.TurnDirection == TurnDirection.Right)
                {
                    switch (detail.TurnCount)
                    {
                        case 1:

                            // Check to see if the distance of this leg is less than the
                            // immediate turn distance.
                            if (journeyDetailIndex > 0)
                            {
                                if (previousDistance < immediateTurnDistance)
                                    routeText = immediatelyBearRight;
                                else
                                    routeText = isMiniRoundabout ? turnRightOne2 + atMiniRoundabout2 + onto : turnRightOne;
                            }
                            else
                            {
                                routeText = isMiniRoundabout ? turnRightOne2 + atMiniRoundabout2 + onto : turnRightOne;
                            }
                            break;

                        case 2: routeText = isMiniRoundabout ? turnRightTwo2 + atMiniRoundabout2 + onto : turnRightTwo; break;
                        case 3: routeText = isMiniRoundabout ? turnRightThree2 + atMiniRoundabout2 + onto : turnRightThree; break;
                        case 4: routeText = isMiniRoundabout ? turnRightFour2 + atMiniRoundabout2 + onto : turnRightFour; break;

                        default: routeText = bearRight; break;
                    }
                }
                
                else if (detail.TurnDirection == TurnDirection.Continue)
                {
                    if ((detail.PlaceName == null || detail.PlaceName.Length == 0) &&
                        (detail.Distance > immediateTurnDistance) && detail.RoadSplits)
                    {
                        routeText = follow + space + to;
                    }
                    else
                    {
                        routeText = follow;
                    }
                }
                
                // Append the next road to the route text
                routeText += space + nextRoad;

                if (detail.TurnDirection == TurnDirection.Continue)
                    routeText += space + straightOn;

                if (detail.RoadSplits)
                {
                    if ((detail.TurnCount > 4) &&
                        ((detail.TurnDirection == TurnDirection.Left) || (detail.TurnDirection == TurnDirection.Right)))
                        routeText = ChangeFirstCharacterCapitalisation(whereRoadSplits, true) + space +
                            ChangeFirstCharacterCapitalisation(routeText, false);
                    else routeText += space + whereRoadSplits;
                }
            }

            return routeText;
        }

        /// <summary>
        /// Generates the route text for the given CycleJourney where
        /// the TurnAngle for the given CycleJourney is "Turn".  This method
        /// assumes that the TurnAngle is "Turn".
        ///
        /// Note that for "counted turns" (TurnCount 1 - 4) the text is actually
        /// "Take the ..." which can be used for both "bear" and "turn".
        /// </summary>
        /// <param name="journeyDetailIndex">Array index into cycleJourney.Details</param>
        /// <returns>Route text.</returns>
        protected virtual string TurnAngleTurn(int journeyDetailIndex)
        {
            CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];

            int previousDistance = 0;
            if (journeyDetailIndex > 0)
            {
                previousDistance = cycleJourney.Details[journeyDetailIndex - 1].Distance;
            };

            string routeText = String.Empty;
            string nextRoad = FormatRoadName(detail);

            //When the CyclePlanner produces a JunctionDriveSection of type Merge (or Exit) 
            //without a junction number (regardless of whether the junction is indicated as ambiguous) 
            //for a Motorway junction (including slip roads) then ignore the turn count 
            //and the immediate parameter. 
            if (detail.JunctionDriveSectionWithoutJunctionNo &&
                (detail.JunctionType == JunctionType.Exit || detail.JunctionType == JunctionType.Merge))
            {
                if (detail.TurnDirection == TurnDirection.Continue)
                {
                    string straightOnInstruction = space + straightOn;

                    if (detail.TurnDirection == TurnDirection.Continue)
                        routeText = follow + space + nextRoad + straightOnInstruction;
                    else if (detail.TurnDirection == TurnDirection.Right)
                        routeText = follow + space + nextRoad + straightOnInstruction;
                    else if (detail.TurnDirection == TurnDirection.Left)
                        routeText = follow + space + nextRoad + straightOnInstruction;

                    if (detail.RoadSplits)
                        routeText += space + whereRoadSplits;
                }
                else
                {
                    if (detail.TurnDirection == TurnDirection.Left)
                        routeText = turnLeftInDistance + space + nextRoad;
                    else if (detail.TurnDirection == TurnDirection.Right)
                        routeText = turnRightInDistance + space + nextRoad;

                    if (detail.RoadSplits)
                        routeText = ChangeFirstCharacterCapitalisation(whereRoadSplits, true) + space +
                            ChangeFirstCharacterCapitalisation(routeText, false);
                }

                return routeText;
            }

            // Check to see if this detail is a junction drive section
            if (detail.JunctionSection)
            {
                if (detail.TurnDirection == TurnDirection.Left)
                {
                    //Motorway entry 
                    if ((detail.JunctionType == JunctionType.Entry) &&
                        (detail.JunctionNumber != null) && (detail.JunctionNumber.Length > 0))
                    {
                        routeText = turnLeftToJoin;
                    }

                }
                else if (detail.TurnDirection == TurnDirection.Right)
                {
                    //Motorway entry 
                    if ((detail.JunctionType == JunctionType.Entry) &&
                        (detail.JunctionNumber != null) && (detail.JunctionNumber.Length > 0))
                    {
                        routeText = turnRightToJoin;
                    }
                }
                
                else if (detail.TurnDirection == TurnDirection.Continue)
                {
                    routeText = continueString;
                }

                // Append the next road to the route text.
                routeText += space + nextRoad;
            }
            //this detail is a drive section
            else
            {

                if (detail.TurnDirection == TurnDirection.Left)
                {
                    switch (detail.TurnCount)
                    {
                        case 1:

                            // Check to see if the distance of this leg is less than the
                            // immediate turn distance.
                            if (journeyDetailIndex > 0)
                            {
                                if (previousDistance < immediateTurnDistance)
                                    routeText = immediatelyTurnLeftOnto;
                                else
                                    routeText = isMiniRoundabout ? turnLeftOne2 + atMiniRoundabout2 + onto : turnLeftOne;
                            }
                            else
                            {
                                routeText = isMiniRoundabout ? turnLeftOne2 + atMiniRoundabout2 + onto : turnLeftOne;
                            }

                            break;

                        case 2: routeText = isMiniRoundabout ? turnLeftTwo2 + atMiniRoundabout2 + onto : turnLeftTwo; break;
                        case 3: routeText = isMiniRoundabout ? turnLeftThree2 + atMiniRoundabout2 + onto : turnLeftThree; break;
                        case 4: routeText = isMiniRoundabout ? turnLeftFour2 + atMiniRoundabout2 + onto : turnLeftFour; break;

                        // Greater than 4 - assuming that the turn count is never 0 when
                        // turn angle is Turn.
                        default: routeText = turnLeftInDistance; break;
                    }
                }
                else if (detail.TurnDirection == TurnDirection.Right)
                {
                    switch (detail.TurnCount)
                    {
                        case 1:

                            // Check to see if the distance of this leg is less than the
                            // immediate turn distance.
                            if (journeyDetailIndex > 0)
                            {
                                if (previousDistance < immediateTurnDistance)
                                    routeText = immediatelyTurnRightOnto;
                                else
                                    routeText = isMiniRoundabout ? turnRightOne2 + atMiniRoundabout2 + onto : turnRightOne;
                            }
                            else
                            {
                                routeText = isMiniRoundabout ? turnRightOne2 + atMiniRoundabout2 + onto : turnRightOne;
                            }

                            break;

                        case 2: routeText = isMiniRoundabout ? turnRightTwo2 + atMiniRoundabout2 + onto : turnRightTwo; break;
                        case 3: routeText = isMiniRoundabout ? turnRightThree2 + atMiniRoundabout2 + onto : turnRightThree; break;
                        case 4: routeText = isMiniRoundabout ? turnRightFour2 + atMiniRoundabout2 + onto : turnRightFour; break;

                        // Greater than 4 - assuming that the turn count is never 0 when
                        // turn angle is Turn.
                        default: routeText = turnRightInDistance; break;
                    }
                }
                
                else if (detail.TurnDirection == TurnDirection.Continue)
                {
                    routeText = follow;
                }

                // Append the next road to the route text.
                routeText += space + nextRoad;

                if (detail.TurnDirection == TurnDirection.Continue)
                    routeText += space + straightOn;

                if (detail.RoadSplits)
                {
                    if ((detail.TurnCount > 4) &&
                        ((detail.TurnDirection == TurnDirection.Left) || (detail.TurnDirection == TurnDirection.Right)))
                        routeText = ChangeFirstCharacterCapitalisation(whereRoadSplits, true) + space +
                            ChangeFirstCharacterCapitalisation(routeText, false);
                    else routeText += space + whereRoadSplits;
                }
            }

            return routeText;
        }

        /// <summary>
        /// Generate the text where the turn angle of the given
        /// CycleJourneyDetail is "Return". This method assumes that
        /// the turn angle for the given detail is "Return".
        /// </summary>
        /// <param name="journeyDetailIndex">Array index into cycleJourney.Details</param>
        /// <returns>Route text.</returns>
        protected virtual string TurnAngleReturn(int journeyDetailIndex)
        {
            CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];

            string nextRoad = FormatRoadName(detail);
            string routeText = String.Empty;
           
            routeText = space + uTurn + space + nextRoad;
            
            return routeText;
        }

        /// <summary>
        /// checks if a place name exists and formats the instruction accordingly
        /// </summary>
        /// <param name="detail">the RoadJourneyDetail being formatted </param>
        /// <param name="routeText">the existing instruction text </param>
        /// <returns>updated formatted string of the directions</returns>
        protected virtual string AddPlaceName(CycleJourneyDetail detail, string routeText)
        {
            //add "towards {placename}" to the end of the instruction 
            routeText = routeText + space + towards + " <b>" + detail.PlaceName + "</b>";

            return routeText;
        }

        /// <summary>
        /// Checks the subsequent RoadJourneyDetail
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="routeText"></param>
        /// <returns></returns>
        protected virtual string CheckNextDetail(CycleJourneyDetail nextDetail, string routeText)
        {
            //add "until junction number {no}" to end of current instruction
            if ((nextDetail.JunctionSection) && ((nextDetail.JunctionType == JunctionType.Exit) || (nextDetail.JunctionType == JunctionType.Merge)))
            {
                routeText = routeText + space + untilJunction + " <b>" + nextDetail.JunctionNumber + "</b>";
            }
            return routeText;
        }

        #endregion

        #region Formatters

        /// <summary>
        /// Returns formatted string of the road name for the supplied
        /// instruction
        /// </summary>
        /// <param name="detail">Details of journey instruction</param>
        /// <returns>The road name</returns>
        protected virtual string FormatRoadName(CycleJourneyDetail detail)
        {
            string roadName = detail.RoadName;
            string roadNumber = detail.RoadNumber;
            string result = String.Empty;

            string cycleRouteName = GetFirstAggregatedWayName(detail);

            bool isTowPath = false;
            bool isCyclePath = false;
            bool isBridlePath = false;
            bool isFootPath = false;

            if ((roadName == null || roadName.Length == 0) &&
                (roadNumber == null || roadNumber.Length == 0))
            {
                // If we have no name or number, then return "local road" or "unnamed path" depending on the
                // type of road this direction is on
                if (this.isPath)
                {
                    if (!string.IsNullOrEmpty(currentITNRoadType))
                    {
                        string footpathString = Global.tdResourceManager.GetString(
                                       "CycleAttribute.Footpath", CultureInfo.CurrentUICulture);

                        string cyclepathString = Global.tdResourceManager.GetString(
                                                    "CycleAttribute.Cyclepath", CultureInfo.CurrentUICulture);

                        string footpathOnlyString = Global.tdResourceManager.GetString(
                                                    "CycleAttribute.FootpathOnly", CultureInfo.CurrentUICulture);

                        string cyclepathOnlyString = Global.tdResourceManager.GetString(
                                                    "CycleAttribute.CyclesOnly", CultureInfo.CurrentUICulture);

                        string towpathString = Global.tdResourceManager.GetString(
                                                    "CycleAttribute.Towpath", CultureInfo.CurrentUICulture);

                        string sharedUseFootpathString = Global.tdResourceManager.GetString(
                                                    "CycleAttribute.SharedUseFootpath", CultureInfo.CurrentUICulture);

                        string bridlepathString = Global.tdResourceManager.GetString(
                                                   "CycleAttribute.Bridlepath", CultureInfo.CurrentUICulture);

                        // Check whether the type of path is cyclepath, bridlepath, towpath or footpath
                        foreach (CycleAttribute attribute in cycleTypeAttributes)
                        {
                            if ((attribute.CycleAttributeResourceName == "CycleAttribute.Cyclepath")
                                || (attribute.CycleAttributeResourceName == "CycleAttribute.CyclesOnly")
                                || (attribute.CycleAttributeResourceName == "CycleAttribute.SharedUseFootpath"))
                            {
                                isCyclePath = true;
                            }
                            else if (attribute.CycleAttributeResourceName == "CycleAttribute.Bridlepath")
                            {
                                isBridlePath = true;
                            }
                            else if (attribute.CycleAttributeResourceName == "CycleAttribute.Towpath")
                            {
                                isTowPath = true;
                            }
                            else if ((attribute.CycleAttributeResourceName == "CycleAttribute.Footpath")
                                        || (attribute.CycleAttributeResourceName == "CycleAttribute.FootpathOnly"))
                            {
                                isFootPath = true;
                            }


                        }

                        // If the path is part of the aggregated way use 1st aggregated way found
                        if (!string.IsNullOrEmpty(cycleRouteName))
                        {
                            return cycleRouteName.ToUpper();
                        }
                        else // make the name of the path the path type attribute in towpath, cyclepath, bridlepath, footpath order of priority
                        {
                            if (currentITNRoadType == footpathString
                                || currentITNRoadType == cyclepathString
                                || currentITNRoadType == footpathOnlyString
                                || currentITNRoadType == cyclepathOnlyString
                                || currentITNRoadType == towpathString
                                || currentITNRoadType == sharedUseFootpathString
                                || currentITNRoadType == bridlepathString)
                            {
                                if (isTowPath)
                                {
                                    return towpathString.ToUpper();
                                }
                                else if (isCyclePath)
                                {
                                    return cyclepathString.ToUpper();
                                }
                                else if (isBridlePath)
                                {
                                    return bridlepathString.ToUpper();
                                }
                                else if (isFootPath)
                                {
                                    return footpathString.ToUpper();
                                }
                                else 
                                    // if we got the road types which is other than towpath, cyclepath, bridlepath, and footpath
                                    // show the current ITN road type
                                {
                                    if(!string.IsNullOrEmpty(currentITNRoadType))
                                        return currentITNRoadType.ToUpper();
                                }
                            }
                        }
                    }
                    return localPath;
                }
                else
                {
                     return localRoad;
                }
            }

            if (roadNumber != null && roadNumber.Length != 0)
            {
                result += roadNumber;
            }

            if (roadName != null && roadName.Length != 0)
            {
                // Check to see if road number was empty and if so
                // then need to bracket the road name.

                if (roadNumber != null && roadNumber.Length != 0)
                {
                    result += space + "(" + roadName + ")";
                }
                else
                {
                    // No road number exists so just concatinate the road name
                    result += roadName;
                }
            }
            return result;
        }

        /// <summary>
        /// If the path is part of the aggregated way get the name of the 1st aggregated way found
        /// </summary>
        /// <param name="detail">Cycle journey detail object</param>
        /// <returns></returns>
        private string GetFirstAggregatedWayName(CycleJourneyDetail detail)
        {
            string cycleRouteName = string.Empty;

            try
            {
                if (detail.CycleRoutes != null)
                {
                    if (detail.CycleRoutes.Count > 0)
                    {
                        foreach (KeyValuePair<string, string> cycleRoute in detail.CycleRoutes)
                        {

                            if (cycleRouteName == string.Empty)
                            {
                                cycleRouteName = cycleRoute.Value.ToString();
                                
                            }

                        }
                    }
                }
            }
            catch
            {
                cycleRouteName = string.Empty;
            }

            return cycleRouteName;
        }

        
        /// <summary>
        /// processes the junction number and junction action values
        /// </summary>
        protected virtual string FormatMotorwayJunction(CycleJourneyDetail detail, string routeText)
        {
            string junctionText = String.Empty;

            //apply the junction number rules 
            switch (detail.JunctionType)
            {
                case JunctionType.Entry:

                    //add a join motorway message to the instructional text
                    routeText = routeText + space + atJunctionJoin + " <b>"
                        + detail.JunctionNumber + "</b>";

                    break;

                case JunctionType.Exit:

                    //replace the normal instructional text with a leave motorway message
                    routeText = atJunctionLeave + " <b>" + detail.JunctionNumber + "</b> " + leaveMotorway;

                    break;

                case JunctionType.Merge:

                    //replace the normal instructional text with a leave motorway message
                    routeText = atJunctionLeave + " <b>" + detail.JunctionNumber + "</b> " + leaveMotorway;

                    break;

                default:

                    //no action required
                    return routeText;
            }

            //return amended route text
            return routeText;
        }

        /// <summary>
        /// Formats a duration (expressed in seconds) into a 
        /// string in the form "hh:mm". If the duration is
        /// less than 30 seconds then a "less 30 secs" string is returned.
        /// The returned time is rounded to the nearest minute.
        /// </summary>
        /// <param name="durationInSeconds">Duration in seconds.</param>
        /// <returns>Formatted string of the duration.</returns>
        protected virtual string FormatDuration(long durationInSeconds)
        {

            // Get the minutes
            double durationInMinutes = (double)durationInSeconds / 60.0;

            // Check to see if less than 30 seconds
            if (durationInMinutes / 60.0 < 1.00 &&
                durationInMinutes % 60.0 < 0.5)
            {
                string secondsString = Global.tdResourceManager.GetString(
                    "CarJourneyDetailsTableControl.labelDurationSeconds", TDCultureInfo.CurrentUICulture);

                return "< 30 " + secondsString;
            }
            else
            {
                // Duration is greater than 30 seconds

                // Round to the nearest minute
                durationInMinutes = Round(durationInMinutes);

                // Calculate the number of hours in the minute
                int hours = (int)durationInMinutes / 60;

                // Get the minutes (afer the hours has been subracted so always < 60)
                int minutes = (int)durationInMinutes % 60;

                return String.Format("{0:D2}:{1:D2}", hours, minutes);

            }
        }

        /// <summary>
        /// Formats the given time for display.
        /// Seconds are rounded to the nearest minute
        /// </summary>
        /// <param name="time">Time to format</param>
        /// <returns>Formatted string of the time in the form hh:mm</returns>
        protected virtual string FormatTime(TDDateTime time)
        {
            if (time.Second >= 30)
            {
                return time.AddMinutes(1).ToString("HH:mm");
            }
            else
            {
                return time.ToString("HH:mm");
            }
        }

        /// <summary>
        /// Formats the given time and date for display.
        /// Seconds are rounded to the nearest minute
        /// </summary>
        /// <param name="time">Time to format</param>
        /// <returns>Formatted string of the time in the form hh:mm (dd/mm/yy)</returns>
        protected virtual string FormatDateTime(TDDateTime time)
        {
            if (time.Second >= 30)
            {
                return time.AddMinutes(1).ToString("HH:mm (dd/MM)");
            }
            else
            {
                return time.ToString("HH:mm (dd/MM)");
            }
        }

        /// <summary>
        /// Return a formatted string by converting the given metres
        /// to a mileage (only 1 decimal place will be returned in the string).
        /// </summary>
        /// <param name="metres">Metres to convert</param>
        /// <returns>Formatted string</returns>
        protected virtual string ConvertMetresToMileage(int metres)
        {
            string resultMileage = MeasurementConversion.Convert((double)metres, ConversionType.MetresToMileage);
            if (string.IsNullOrEmpty(resultMileage))
                resultMileage = "0";

            double result = Convert.ToDouble(resultMileage);
            
            return result.ToString("F1", TDCultureInfo.CurrentUICulture.NumberFormat);
        }

        /// <summary>
        /// Return a formatted string by converting the given metres
        /// to km.
        /// </summary>
        /// <param name="metres">Metres to convert</param>
        /// <returns>Formatted string</returns>
        protected virtual string ConvertMetresToKm(int metres)
        {
            double result = (double)metres / 1000;

            // Return the result
            return result.ToString("F1", TDCultureInfo.CurrentUICulture.NumberFormat);
        }

        /// <summary>
        /// Rounds the given double to the nearest int.
        /// If double is 0.5, then rounds up.
        /// Using this instead of Math.Round because Math.Round
        /// ALWAYS returns the even number when rounding a .5 -
        /// this is not behaviour we want.
        /// </summary>
        /// <param name="valueToRound">Value to round.</param>
        /// <returns>Nearest integer</returns>
        protected static int Round(double valueToRound)
        {
            // Get the decimal point
            double valueFloored = Math.Floor(valueToRound);
            double remain = valueToRound - valueFloored;

            if (remain >= 0.5)
                return (int)Math.Ceiling(valueToRound);
            else
                return (int)Math.Floor(valueToRound);
        }

        /// <summary>
        /// Gets corresponding capitalised string.
        /// </summary>
        /// <param name="upper"></param>
        /// <param name="originalString"></param>
        /// <returns></returns>
        private string ChangeFirstCharacterCapitalisation(string originalString, bool upper)
        {
            string firstChar = originalString[0].ToString();
            firstChar = upper ? firstChar.ToUpper(CultureInfo.CurrentCulture)
                : firstChar.ToLower(CultureInfo.CurrentCulture);

            string newString = firstChar + originalString.Substring(1);

            return newString;
        }

        #endregion

        #endregion

        #endregion

        #region Private methods

        /// <summary>
        /// Initialises all language sensitive text strings using
        /// the resource manager.
        /// </summary>
        private void InitialiseRouteTextDescriptionStrings()
        {
            throughRoute = Global.tdResourceManager.GetString
                ("RouteText.ThroughRoute", CultureInfo.CurrentUICulture);
            
            continueString = Global.tdResourceManager.GetString
                ("RouteText.Continue", CultureInfo.CurrentUICulture);

            roundaboutExitOne = Global.tdResourceManager.GetString
                ("RouteText.RoundaboutExitOne", CultureInfo.CurrentUICulture);
            roundaboutExitTwo = Global.tdResourceManager.GetString
                ("RouteText.RoundaboutExitTwo", CultureInfo.CurrentUICulture);
            roundaboutExitThree = Global.tdResourceManager.GetString
                ("RouteText.RoundaboutExitThree", CultureInfo.CurrentUICulture);
            roundaboutExitFour = Global.tdResourceManager.GetString
                ("RouteText.RoundaboutExitFour", CultureInfo.CurrentUICulture);
            roundaboutExitFive = Global.tdResourceManager.GetString
                ("RouteText.RoundaboutExitFive", CultureInfo.CurrentUICulture);
            roundaboutExitSix = Global.tdResourceManager.GetString
                ("RouteText.RoundaboutExitSix", CultureInfo.CurrentUICulture);
            roundaboutExitSeven = Global.tdResourceManager.GetString
                ("RouteText.RoundaboutExitSeven", CultureInfo.CurrentUICulture);
            roundaboutExitEight = Global.tdResourceManager.GetString
                ("RouteText.RoundaboutExitEight", CultureInfo.CurrentUICulture);
            roundaboutExitNine = Global.tdResourceManager.GetString
                ("RouteText.RoundaboutExitNine", CultureInfo.CurrentUICulture);
            roundaboutExitTen = Global.tdResourceManager.GetString
                ("RouteText.RoundaboutExitTen", CultureInfo.CurrentUICulture);

            turnLeftOne = Global.tdResourceManager.GetString
                ("RouteText.TurnLeftOne", CultureInfo.CurrentUICulture);
            turnLeftTwo = Global.tdResourceManager.GetString
                ("RouteText.TurnLeftTwo", CultureInfo.CurrentUICulture);
            turnLeftThree = Global.tdResourceManager.GetString
                ("RouteText.TurnLeftThree", CultureInfo.CurrentUICulture);
            turnLeftFour = Global.tdResourceManager.GetString
                ("RouteText.TurnLeftFour", CultureInfo.CurrentUICulture);

            turnRightOne = Global.tdResourceManager.GetString
                ("RouteText.TurnRightOne", CultureInfo.CurrentUICulture);
            turnRightTwo = Global.tdResourceManager.GetString
                ("RouteText.TurnRightTwo", CultureInfo.CurrentUICulture);
            turnRightThree = Global.tdResourceManager.GetString
                ("RouteText.TurnRightThree", CultureInfo.CurrentUICulture);
            turnRightFour = Global.tdResourceManager.GetString
                ("RouteText.TurnRightFour", CultureInfo.CurrentUICulture);

            turnLeftOne2 = Global.tdResourceManager.GetString
                ("RouteText.TurnLeftOne2", CultureInfo.CurrentUICulture);
            turnLeftTwo2 = Global.tdResourceManager.GetString
                ("RouteText.TurnLeftTwo2", CultureInfo.CurrentUICulture);
            turnLeftThree2 = Global.tdResourceManager.GetString
                ("RouteText.TurnLeftThree2", CultureInfo.CurrentUICulture);
            turnLeftFour2 = Global.tdResourceManager.GetString
                ("RouteText.TurnLeftFour2", CultureInfo.CurrentUICulture);

            turnRightOne2 = Global.tdResourceManager.GetString
                ("RouteText.TurnRightOne2", CultureInfo.CurrentUICulture);
            turnRightTwo2 = Global.tdResourceManager.GetString
                ("RouteText.TurnRightTwo2", CultureInfo.CurrentUICulture);
            turnRightThree2 = Global.tdResourceManager.GetString
                ("RouteText.TurnRightThree2", CultureInfo.CurrentUICulture);
            turnRightFour2 = Global.tdResourceManager.GetString
                ("RouteText.TurnRightFour2", CultureInfo.CurrentUICulture);

            turnLeftInDistance = Global.tdResourceManager.GetString
                ("RouteText.TurnLeftInDistance", CultureInfo.CurrentUICulture);
            turnRightInDistance = Global.tdResourceManager.GetString
                ("RouteText.TurnRightInDistance", CultureInfo.CurrentUICulture);

            bearLeft = Global.tdResourceManager.GetString
                ("RouteText.BearLeft", CultureInfo.CurrentUICulture);
            bearRight = Global.tdResourceManager.GetString
                ("RouteText.BearRight", CultureInfo.CurrentUICulture);

            immediatelyBearLeft = Global.tdResourceManager.GetString
                ("RouteText.ImmediatelyBearLeft", CultureInfo.CurrentUICulture);
            immediatelyBearRight = Global.tdResourceManager.GetString
                ("RouteText.ImmediatelyBearRight", CultureInfo.CurrentUICulture);


            arriveAt = Global.tdResourceManager.GetString
                ("RouteText.ArriveAt", CultureInfo.CurrentUICulture);

            leaveFrom = Global.tdResourceManager.GetString
                ("RouteText.Leave", CultureInfo.CurrentUICulture);

            notApplicable = Global.tdResourceManager.GetString
                ("RouteText.NotApplicable", CultureInfo.CurrentUICulture);

            localRoad = Global.tdResourceManager.GetString
                ("RouteText.LocalRoad", CultureInfo.CurrentUICulture);

            localPath = Global.tdResourceManager.GetString
                ("RouteText.LocalPath", CultureInfo.CurrentUICulture);

            street = Global.tdResourceManager.GetString
                ("RouteText.Street", CultureInfo.CurrentUICulture);

            path = Global.tdResourceManager.GetString
                ("RouteText.Path", CultureInfo.CurrentUICulture);

            //motorway instructions
            atJunctionLeave = Global.tdResourceManager.GetString
                ("RouteText.AtJunctionLeave", CultureInfo.CurrentUICulture);

            leaveMotorway = Global.tdResourceManager.GetString
                ("RouteText.LeaveMotorway", CultureInfo.CurrentUICulture);

            untilJunction = Global.tdResourceManager.GetString
                ("RouteText.UntilJunction", CultureInfo.CurrentUICulture);

            towards = Global.tdResourceManager.GetString
                ("RouteText.Towards", CultureInfo.CurrentUICulture);

            continueFor = Global.tdResourceManager.GetString
                ("RouteText.ContinueFor", CultureInfo.CurrentUICulture);

            miles = Global.tdResourceManager.GetString
                ("RouteText.Miles", CultureInfo.CurrentUICulture);

            turnLeftToJoin = Global.tdResourceManager.GetString
                ("RouteText.TurnLeftToJoin", CultureInfo.CurrentUICulture);

            turnRightToJoin = Global.tdResourceManager.GetString
                ("RouteText.TurnRightToJoin", CultureInfo.CurrentUICulture);

            atJunctionJoin = Global.tdResourceManager.GetString
                ("RouteText.AtJunctionJoin", CultureInfo.CurrentUICulture);

            bearLeftToJoin = Global.tdResourceManager.GetString
                ("RouteText.BearLeftToJoin", CultureInfo.CurrentUICulture);

            bearRightToJoin = Global.tdResourceManager.GetString
                ("RouteText.BearRightToJoin", CultureInfo.CurrentUICulture);

            join = Global.tdResourceManager.GetString
                ("RouteText.Join", CultureInfo.CurrentUICulture);

            follow = Global.tdResourceManager.GetString
                ("RouteText.Follow", CultureInfo.CurrentUICulture);

            to = Global.tdResourceManager.GetString
                ("RouteText.To", CultureInfo.CurrentUICulture);

            enter = Global.tdResourceManager.GetString
                ("RouteText.Enter", CultureInfo.CurrentUICulture);
            congestionZone = Global.tdResourceManager.GetString
                ("RouteText.CongestionCharge", CultureInfo.CurrentUICulture);
            charge = Global.tdResourceManager.GetString
                ("RouteText.Charge", CultureInfo.CurrentUICulture);
            chargeAdultAndCycle = Global.tdResourceManager.GetString
                ("RouteText.ChargeAdultAndCycle", CultureInfo.CurrentUICulture);
            certainTimes = Global.tdResourceManager.GetString
                ("RouteText.CertainTimes", CultureInfo.CurrentUICulture);
            certainTimesNoCharge = Global.tdResourceManager.GetString
                ("RouteText.CertainTimesNoCharge", CultureInfo.CurrentUICulture);
            board = Global.tdResourceManager.GetString
                ("RouteText.Board", CultureInfo.CurrentUICulture);
            departingAt = Global.tdResourceManager.GetString
                ("RouteText.DepartingAt", CultureInfo.CurrentUICulture);
            toll = Global.tdResourceManager.GetString
                ("RouteText.Toll", CultureInfo.CurrentUICulture);
            ferryWait = Global.tdResourceManager.GetString
                ("RouteText.WaitForFerry", CultureInfo.CurrentUICulture);
            viaArriveAt = Global.tdResourceManager.GetString
                ("RouteText.ViaArriveAt", CultureInfo.CurrentUICulture);
            leaveFerry = Global.tdResourceManager.GetString
                ("RouteText.LeaveFerry", CultureInfo.CurrentUICulture);
            exit = Global.tdResourceManager.GetString
                ("RouteText.Exit", CultureInfo.CurrentUICulture);
            end = Global.tdResourceManager.GetString
                ("RouteText.End", CultureInfo.CurrentUICulture);
            unspecifedFerryWait = Global.tdResourceManager.GetString
                ("RouteText.UnspecifedWaitForFerry", CultureInfo.CurrentUICulture);
            intermediateFerryWait = Global.tdResourceManager.GetString
                ("RouteText.IntermediateFerry", CultureInfo.CurrentUICulture);
            waitAtTerminal = Global.tdResourceManager.GetString
                ("RouteText.WaitAtTerminal", CultureInfo.CurrentUICulture);
            notAvailable = Global.tdResourceManager.GetString
                ("RouteText.NotAvailable", CultureInfo.CurrentUICulture);

            straightOn = Global.tdResourceManager.GetString
                ("RouteText.StraightOn", CultureInfo.CurrentUICulture);
            atMiniRoundabout = Global.tdResourceManager.GetString
                ("RouteText.AtMiniRoundabout", CultureInfo.CurrentUICulture);
            atMiniRoundabout2 = Global.tdResourceManager.GetString
                ("RouteText.AtMiniRoundabout2", CultureInfo.CurrentUICulture);
            immediatelyTurnRightOnto = Global.tdResourceManager.GetString
                ("RouteText.ImmediatelyTurnRightOnto", CultureInfo.CurrentUICulture);
            immediatelyTurnLeftOnto = Global.tdResourceManager.GetString
                ("RouteText.ImmediatelyTurnLeftOnto", CultureInfo.CurrentUICulture);
            whereRoadSplits = Global.tdResourceManager.GetString
                ("RouteText.WhereRoadSplits", CultureInfo.CurrentUICulture);
            onto = Global.tdResourceManager.GetString
                ("RouteText.OnTo", CultureInfo.CurrentUICulture);
            uTurn = Global.tdResourceManager.GetString
                ("RouteText.UTurn", CultureInfo.CurrentUICulture);

            // open new window icon
            openNewWindowImageUrl = Global.tdResourceManager.GetString
                ("ExternalLinks.OpensNewWindowImage", CultureInfo.CurrentUICulture);
        }

        #endregion
    }
}
