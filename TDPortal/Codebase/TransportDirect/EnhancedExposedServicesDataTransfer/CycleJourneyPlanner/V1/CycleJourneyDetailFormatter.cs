// *********************************************** 
// NAME             : CycleJourneyDetailFormatter.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Creates cycle journey detail instructions and other cycle journey details 
///                 : and assembles them in CycleJourneyDetail dto objects
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/CycleJourneyDetailFormatter.cs-arc  $
//
//   Rev 1.6   Dec 05 2012 14:17:32   mmodi
//Fixed compiler warnings
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.5   Dec 07 2010 09:42:02   apatel
//updated to get first agregated way name
//Resolution for 5653: Cycle path name update issue - showing the full aggregated path name
//
//   Rev 1.4   Dec 06 2010 12:44:06   apatel
//Updated to show name of the first aggregated way found instead of showing name of the aggregated way
//Resolution for 5653: Cycle path name update issue - showing the full aggregated path name
//
//   Rev 1.3   Dec 01 2010 13:01:06   apatel
//Code updated to show aggregated name/number or cycle path type attribute name instead of  "unname path" and to show more details by default.
//Resolution for 5650: Cycle Planner - Path name updates
//
//   Rev 1.2   Nov 05 2010 14:35:22   apatel
//Updated to resolve the EES Cycle web service issues
//Resolution for 5632: EES for Cycle issues
//
//   Rev 1.1   Oct 13 2010 11:39:04   apatel
//Updated GetGeometry method to add a null check for Geometry
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.0   Sep 29 2010 10:39:44   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.ResourceManager;

using CyclePlannerControl = TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;
using System.Globalization;
using TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// Creates cycle journey detail instructions and other cycle journey details 
    /// and assemble them in CycleJourneyDetail dto object
    /// </summary>
    public class CycleJourneyDetailFormatter
    {
        #region Member Variables

        #region Variables
        // Used to control rendering of the step number in the table
        private int stepNumber;

        // Keeps the accumulated distance of the journey currently being rendered.
        private int accumulatedDistance;

        // Keeps the accumulated arrival time (updated after each leg)
        private TDDateTime currentArrivalTime;

        // Constant representing space used in string formatting
        private readonly static string space = " ";

        // Constant representing space used in string formatting
        private readonly static char comma = ',';

        // Constant representing full stop used in string formatting
        private readonly static string fullstop = ".";

        // Constant representing seperator used in string formatting
        private readonly static string seperator = ", ";

        // Cycle journey to render
        private CyclePlannerControl.CycleJourney cycleJourney;

        // Cycle journey result settings
        private ResultSettings resultSettings;

        // Resource manger to access content 
        private TDResourceManager rm;
                
        // Road distance in metres within which "immediate" text should be 
        // prepended to journey direction
        private int immediateTurnDistance;

        // slip road distance limit parameter
        private int slipRoadDistance;

        // flag to set if the cycle instruction text is displayed
        private bool showInstructionText;

        // flag to show the cycle attribute number returned
        private bool showAttributeNumber;

        
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
                

        #endregion

        #region Temporary variables used to track across multiple CycleJourneyDetails
        private bool currentIsCycleRoute = false;
        private bool previousIsCycleRoute = false;

        private bool previousCycleInfrastructure = false;
        private bool previousRecommendedRoute = false;

        private bool joiningCycleInfrastructure = false;
        private bool leavingCycleInfrastructure = false;
        private bool joiningRecommendedCycleRoute = false;
        private bool leavingRecommendedCycleRoute = false;

        private bool isRoundabout = false;
        private bool isMiniRoundabout = false;
        private bool isBridgeTunnel = false;
        private bool isPath = false;
        private bool isCrossing = false;
        private bool isRecommendedCycleRoute = false;
        private bool isCycleInfrastructure = false;


        private string currentITNRoadType = string.Empty;

        #endregion

        #region Private variables

        // strings used to hold the cycle attribute text details
        private string attributeCrossingText;
        private string attributeTypeText;
        private string attributeBarrierText;
        private string attributeCharacteristicText;
        private string attributeManoeuvreText;

        private List<CyclePlannerControl.CycleAttribute> cycleTypeAttributes = new List<CyclePlannerControl.CycleAttribute>();
        #endregion

        #endregion Member Variables

        #region Constructors
                
        /// <summary>
        /// Constructs a cycle journey formatter
        /// </summary>
        /// <param name="cycleJourney">The specific cycle journey to display</param>
        public CycleJourneyDetailFormatter(CyclePlannerControl.CycleJourney cycleJourney, ResultSettings resultSettings, TDResourceManager rm)
        {
            this.cycleJourney = cycleJourney;
            this.resultSettings = resultSettings;
            this.rm = rm;
            this.showAttributeNumber = false; // May be added in future

            InitialiseRouteTextDescriptionStrings();

            // Used to prepend the word 'Immediately' to instructions with short distances between them
            immediateTurnDistance = Convert.ToInt32(Properties.Current["CyclePlanner.CycleJourneyDetailsControl.ImmediateTurnDistance"], TDCultureInfo.CurrentUICulture.NumberFormat);

            // Get slip road distance limit parameter
            slipRoadDistance = Convert.ToInt32(Properties.Current["Web.CarJourneyDetailsControl.SlipRoadDistance"], TDCultureInfo.CurrentUICulture.NumberFormat);

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
        protected string TotalDistance
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
        protected string TotalKmDistance
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
        public virtual List<CycleJourneyDetail> GetJourneyDetails()
        {
            return ProcessCycleJourney();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// The is a template method that controls the format process. It calls
        /// into hook methods to generate specific output defined by 
        /// subclasses.
        /// </summary>
        /// <returns>Returns an ordered list of journey instructions where each 
        /// element is a string array of details (e.g, road name, distance, directions)</returns>
        protected virtual List<CycleJourneyDetail> ProcessCycleJourney()
        {
            List<CycleJourneyDetail> details = new List<CycleJourneyDetail>();

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

                return details;
            }
        }

        /// <summary>
        /// A hook method called by ProcessCycleJourney to process each cycle journey
        /// instruction. The returned string array contains formatted details for
        /// step number, total distance, directions and arrival time in that order
        /// </summary>
        /// <param name="journeyDetailIndex">Array index into cycleJourney.Details</param>
        /// <returns>details for each instruction</returns>
        private CycleJourneyDetail ProcessCycleJourneyDetail(int journeyDetailIndex, bool showCongestionCharge)
        {
            CycleJourneyDetail detail = new CycleJourneyDetail();

            // Note that the Accumulated Distance is updated in GetDirections, therefore the TotalDistance
            // displayed is the total prior to the current instruction rather than after it	
            CyclePlannerControl.CycleJourneyDetail cycleDetail = cycleJourney.Details[journeyDetailIndex];

            detail.InstructionNumber = GetCurrentStepNumber();

            if (cycleDetail.ViaLocation == true || cycleDetail.WaitSection == true)
            {
                //accumulated distance
                if (resultSettings.DistanceUnit == DistanceUnit.Miles)
                {
                    detail.CumulativeDistance = TotalDistance;
                }
                else
                {
                    detail.CumulativeDistance = TotalKmDistance;
                }
            }
            else if (cycleDetail.StopoverSection)
            {
                detail.CumulativeDistance = "-";
            }
            else
            {
                //accumulated distance
                if (resultSettings.DistanceUnit == DistanceUnit.Miles)
                {
                    detail.CumulativeDistance = TotalDistance;
                }
                else
                {
                    detail.CumulativeDistance = TotalKmDistance;
                }
            }

            detail.InstructionText = GetDirections(journeyDetailIndex, showCongestionCharge);
            detail.CycleRouteName = GetCyclePathName(journeyDetailIndex);

            if (cycleDetail.Geometry != null)
            {
                detail.Geometry = GetGeometry(cycleDetail.Geometry);
            }

            //calculate cycle other costs
            detail.Cost = GetCycleOtherCost(cycleDetail);

            detail.IsBridgeTunnel = isBridgeTunnel;
            detail.IsPath = isPath;
            detail.IsRecommendedCycleRoute = isRecommendedCycleRoute;
            detail.IsCycleInfrastructure = isCycleInfrastructure;

            detail.ArrivalTime = GetArrivalTime(journeyDetailIndex);

            #region Significant/Interesting link/node/section attributes
            detail.JoiningSignificantLinkAttributes = cycleDetail.JoiningSignificantLinkAttributes;
            detail.LeavingSignificantLinkAttributes = cycleDetail.LeavingSignificantLinkAttributes;
            detail.InterestingLinkAttributes = cycleDetail.InterestingLinkAttributes;
            detail.SignificantNodeAttributes = cycleDetail.SignificantNodeAttributes;
            detail.SectionFeatureAttributes = cycleDetail.SectionFeatureAttributes;
            #endregion

            return detail;
        }

        /// <summary>
        /// Gets the cost of type Other for the cycle journey detail specified
        /// </summary>
        /// <param name="detail">Section/route of the cycle journey (Cycle Journey Detail)</param>
        /// <returns>CycleCost object</returns>
        private CycleCost GetCycleOtherCost(CyclePlannerControl.CycleJourneyDetail detail)
        {
            CycleCost cycleCost = new CycleCost();

            cycleCost.CostType = CycleCostType.OtherCost;

            decimal cost = 0;
            string companyName = string.Empty;
            
            string description = string.Empty;

            if (detail.StopoverSection)
            {
                
                #region Stopover section

                if (detail.CongestionEntry
                    || detail.CongestionEnd
                    || detail.CongestionExit)
                {
                    cost += detail.CongestionZoneCost;

                    if (detail.CongestionEntry)
                        description = StopoverSectionType.CongestionZoneEntry.ToString();
                    else if (detail.CongestionExit)
                        description = StopoverSectionType.CongestionZoneExit.ToString();
                    else
                        description = StopoverSectionType.CongestionZoneEnd.ToString();
                }
                if (detail.FerryCheckIn
                    || detail.FerryCheckOut)
                {
                    cost += detail.FerryCost;
                    companyName = detail.FerryCheckInName;

                    description = detail.FerryCheckIn ? StopoverSectionType.FerryCheckIn.ToString()
                        : StopoverSectionType.FerryCheckOut.ToString(); 
                }
                if (detail.TollEntry || detail.TollExit)
                {
                    cost += detail.TollCost;

                    companyName = detail.TollEntry ? detail.TollEntryName : detail.TollExitName;
                    
                    description = detail.TollEntry ? StopoverSectionType.TollEntry.ToString()
                        : StopoverSectionType.TollExit.ToString(); 
                }

                #endregion
            }

            cycleCost.CompanyURL = detail.CompanyUrl;
            cycleCost.Description = description;
            cycleCost.CompanyName = companyName;

            return cycleCost;

        }

        

        
        
        /// <summary>
        /// A hook method called by ProcessCycleJourney to add the first element to the ordered list 
        /// of journey instructions. The returned string array contains step number, a "Leave from ..." 
        /// instruction and "not applicable" indicators for accumulated distance and arrival time.
        /// </summary>
        /// <returns>details for first instruction</returns>
        private CycleJourneyDetail AddFirstDetailLine()
        {
            CycleJourneyDetail detail = new CycleJourneyDetail();

            //step number
            detail.InstructionNumber = GetCurrentStepNumber();

            //accumulated distance
            if (resultSettings.DistanceUnit == DistanceUnit.Miles)
            {
                detail.CumulativeDistance =  TotalDistance ;
            }
            else
            {
                detail.CumulativeDistance =  TotalKmDistance ;
            }

            //instruction
            detail.InstructionText = leaveFrom + " " + cycleJourney.OriginLocation.Description;

            //cycle path name
            detail.CycleRouteName = string.Empty;

            // cycle arrival time
            detail.ArrivalTime = string.Empty;

            // The geometry string of the OSGR coordinates this detail travels
            detail.Geometry = string.Empty;

            
            return detail;
        }

        /// <summary>
        /// A hook method called by ProcessCycleJourney to add the last element to the 
        /// ordered list of journey instructions. The returned string array contains step number,
        /// an "arrive at ..." instruction and arrival time, and an empty string for accumulated distance
        /// </summary>
        /// <returns>details for last instruction</returns>
        protected CycleJourneyDetail AddLastDetailLine()
        {
            CycleJourneyDetail detail = new CycleJourneyDetail();

            int journeyDetailIndex = cycleJourney.Details.Length - 1;

            //step number
            detail.InstructionNumber = GetCurrentStepNumber();

            //accumulated distance
            if (resultSettings.DistanceUnit == DistanceUnit.Miles)
            {
                detail.CumulativeDistance = TotalDistance;
            }
            else
            {
                detail.CumulativeDistance = TotalKmDistance;
            }

            //instruction
            detail.InstructionText = arriveAt + " " + cycleJourney.DestinationLocation.Description;

            //cycle path name
            detail.CycleRouteName = string.Empty;

            // cycle arrival time
            detail.ArrivalTime = GetArrivalTime(journeyDetailIndex);

            // The geometry string of the OSGR coordinates this detail travels
            detail.Geometry = string.Empty;

            return detail;
        }

        /// <summary>
        /// Determines if Continute for... messages needs adding
        /// </summary>
        /// <param name="detail">Cycle Journey Details object</param>
        /// <param name="nextDetailHasJunctionExitJunction"></param>
        /// <param name="routeText"></param>
        /// <returns></returns>
        private string AddContinueFor(CyclePlannerControl.CycleJourneyDetail detail,
            bool nextDetailHasJunctionExitJunction, string routeText)
        {
            //convert metres to miles
            string milesDistance = ConvertMetresToMileage(detail.Distance);
            string distanceInKm = ConvertMetresToKm(detail.Distance);
            string distanceInMiles = string.Empty;
            string kmDistance = string.Empty;
            string ferryCrossing = rm.GetString("RouteText.FerryCrossing", TDCultureInfo.CurrentUICulture);

            //Switches the default display of either Miles or Kms depending on roadUnits
            if (resultSettings.DistanceUnit == DistanceUnit.Miles)
            {
                distanceInMiles = milesDistance + space + miles ;
                kmDistance = distanceInKm + space + "km" ;
            }
            else
            {
                distanceInMiles = milesDistance + space + miles;
                kmDistance = distanceInKm + space + "km";
            }

            //check if this text should be added
            if (detail.Ferry)
            {
                //no need to add the "continues for..." message in these situations
                routeText = ferryCrossing;

                return routeText;
            }

            else if (
                (detail.Distance <= immediateTurnDistance) &&

                !((detail.JunctionType == JunctionType.Entry) &&
                detail.JunctionSection &&
                (detail.PlaceName != null && detail.PlaceName.Length > 0) &&
                nextDetailHasJunctionExitJunction) &&

                !(detail.JunctionSection &&
                ((detail.JunctionType == JunctionType.Exit) ||
                (detail.JunctionType == JunctionType.Merge)))
                )
            {
                //no need to add the "continues for..." message in these situations
                return routeText;

            }

            else if ((detail.SlipRoad) && (detail.Distance < slipRoadDistance) &&

                !((detail.JunctionType == JunctionType.Entry) &&
                detail.JunctionSection &&
                (detail.PlaceName != null && detail.PlaceName.Length > 0) &&
                nextDetailHasJunctionExitJunction) &&

                !(detail.JunctionSection &&
                ((detail.JunctionType == JunctionType.Exit) ||
                (detail.JunctionType == JunctionType.Merge)) &&
                ((detail.JunctionNumber != null) && (detail.JunctionNumber.Length > 0))))
            {
                //no need to add the "continues for..." message in these situations
                return routeText;
            }
            else
            {
                //in all other cases add "continues for..." to the end of the instruction 
                if (resultSettings.DistanceUnit == DistanceUnit.Miles)
                {
                    routeText = routeText + continueFor + space + distanceInMiles;
                }
                else
                {
                    routeText = routeText + continueFor + space + kmDistance;
                }

            }

            return routeText;
        }

        
        #region Helper methods
        /// <summary>
        /// Returns the current instruction step number. Getting the instruction step 
        /// number also increments it
        /// </summary>
        private int GetCurrentStepNumber()
        {
            int currentStepNumber = stepNumber;
            stepNumber++;
            return currentStepNumber;
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
            CyclePlannerControl.CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];
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
            CyclePlannerControl.CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];

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
            CyclePlannerControl.CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];
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
            CyclePlannerControl.CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];
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
            CyclePlannerControl.ICycleAttributes cycleAttributesService = (CyclePlannerControl.ICycleAttributes)TDServiceDiscovery.Current[ServiceDiscoveryKey.CycleAttributes];

            // Get the cycle detail
            CyclePlannerControl.CycleJourneyDetail currentDetail = cycleJourney.Details[journeyDetailIndex];

            #region Set up "is a Cycle route" flags

            // Check if current and previous detail have a Cycle Route
            currentIsCycleRoute = ((currentDetail.CycleRoutes != null) && (currentDetail.CycleRoutes.Count > 0));

            #endregion

            #region Set up cycle route type ( Cycle Infrastructure/Recommended Cycle Route)
            // Check if any of the attributes are for specific cycling infrastructure

            // Determine if the joining attributes has an attribute to Show the cycle image
            if ((currentDetail.JoiningSignificantLinkAttributes != null)
                && (currentDetail.JoiningSignificantLinkAttributes.Length > 0))
            {
                for (int i = 0; i < currentDetail.JoiningSignificantLinkAttributes.Length; i++)
                {
                    CyclePlannerControl.CycleAttributeGroup cycleAttributeGroup = GetCycleAttributeGroup(i);

                    CyclePlannerControl.CycleAttribute[] cycleInfrastructureArray = cycleAttributesService.GetCycleInfrastructureAttributes(cycleAttributeGroup);
                    CyclePlannerControl.CycleAttribute[] cycleRecommendedArray = cycleAttributesService.GetCycleRecommendedAttributes(cycleAttributeGroup);

                    uint attributeValue = currentDetail.JoiningSignificantLinkAttributes[i];

                    // Cycle path icon
                    foreach (CyclePlannerControl.CycleAttribute cycleAttribute in cycleInfrastructureArray)
                    {
                        // Is cycle attribute bit flag true
                        if ((cycleAttribute.CycleAttributeMask & attributeValue) > 0)
                        {
                            joiningCycleInfrastructure = true;
                            break;
                        }
                    }

                    // Cycle recommended route icon
                    foreach (CyclePlannerControl.CycleAttribute cycleAttribute in cycleRecommendedArray)
                    {
                        if ((cycleAttribute.CycleAttributeMask & attributeValue) > 0)
                        {
                            joiningRecommendedCycleRoute = true;
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
                    CyclePlannerControl.CycleAttributeGroup cycleAttributeGroup = GetCycleAttributeGroup(i);

                    CyclePlannerControl.CycleAttribute[] cycleInfrastructureArray = cycleAttributesService.GetCycleInfrastructureAttributes(cycleAttributeGroup);
                    CyclePlannerControl.CycleAttribute[] cycleRecommendedArray = cycleAttributesService.GetCycleRecommendedAttributes(cycleAttributeGroup);

                    uint attributeValue = currentDetail.LeavingSignificantLinkAttributes[i];

                    // Cycle path icon
                    foreach (CyclePlannerControl.CycleAttribute cycleAttribute in cycleInfrastructureArray)
                    {
                        // Is cycle attribute bit flag true
                        if ((cycleAttribute.CycleAttributeMask & attributeValue) > 0)
                        {
                            leavingCycleInfrastructure = true;
                            break;
                        }
                    }

                    // Cycle recommended route icon
                    foreach (CyclePlannerControl.CycleAttribute cycleAttribute in cycleRecommendedArray)
                    {
                        if ((cycleAttribute.CycleAttributeMask & attributeValue) > 0)
                        {
                            leavingRecommendedCycleRoute = true;
                            break;
                        }
                    }
                }
            }

            // Set whether cycle route is cycle infrastructure or is recommended cycle route
            SetCycleRouteType(journeyDetailIndex);

            #endregion

            #region Set up "is a mini roundabout" instruction attribute

            // Determine if the node attributes has an attribute or Miniroundabouts
            if ((currentDetail.SignificantNodeAttributes != null)
                && (currentDetail.SignificantNodeAttributes.Length > 0))
            {
                for (int i = 0; i < currentDetail.SignificantNodeAttributes.Length; i++)
                {
                    CyclePlannerControl.CycleAttributeGroup cycleAttributeGroup = GetCycleAttributeGroup(i);

                    CyclePlannerControl.CycleAttribute[] cycleNodeAttributesArray = cycleAttributesService.GetCycleAttributes(CyclePlannerControl.CycleAttributeType.Node, cycleAttributeGroup);

                    uint attributeValue = currentDetail.SignificantNodeAttributes[i];

                    // Specifically check for the mini roundabout attribute
                    foreach (CyclePlannerControl.CycleAttribute cycleAttribute in cycleNodeAttributesArray)
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

            #region Set up IsBridgeTunnel attribute

            // Determine if the node attributes has an attribute of bridge or tunnel
            if ((currentDetail.SignificantNodeAttributes != null)
                && (currentDetail.SignificantNodeAttributes.Length > 0))
            {
                for (int i = 0; i < currentDetail.SignificantNodeAttributes.Length; i++)
                {
                    CyclePlannerControl.CycleAttributeGroup cycleAttributeGroup = GetCycleAttributeGroup(i);

                    CyclePlannerControl.CycleAttribute[] cycleNodeAttributesArray = cycleAttributesService.GetCycleAttributes(CyclePlannerControl.CycleAttributeType.Link, cycleAttributeGroup);

                    uint attributeValue = currentDetail.SignificantNodeAttributes[i];

                    // Specifically check for the mini roundabout attribute
                    foreach (CyclePlannerControl.CycleAttribute cycleAttribute in cycleNodeAttributesArray)
                    {
                        // Is cycle attribute bit flag true
                        if ((cycleAttribute.CycleAttributeMask & attributeValue) > 0)
                        {
                            if ((cycleAttribute.CycleAttributeResourceName.Trim().ToLower().Equals("cycleattribute.bridge"))
                                || (cycleAttribute.CycleAttributeResourceName.Trim().ToLower().Equals("cycleattribute.tunnel")))
                            {
                                this.isBridgeTunnel = true;
                                break;
                            }
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
                SetITNRoadType(currentDetail.JoiningSignificantLinkAttributes, CyclePlannerControl.CycleAttributeType.Link);
            }

            if ((currentDetail.InterestingLinkAttributes != null)
                && (currentDetail.InterestingLinkAttributes.Length > 0))
            {
                SetITNRoadType(currentDetail.InterestingLinkAttributes, CyclePlannerControl.CycleAttributeType.Link);
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
                    CyclePlannerControl.CycleAttributeGroup cycleAttributeGroup = GetCycleAttributeGroup(i);

                    CyclePlannerControl.CycleAttribute[] cycleAttributesArray = cycleAttributesService.GetCycleAttributes(CyclePlannerControl.CycleAttributeType.Link, cycleAttributeGroup);

                    // Get the cycle attribute
                    uint attributeValue = currentDetail.LeavingSignificantLinkAttributes[i];

                    // Specifically check for the Roundabout attributes
                    foreach (CyclePlannerControl.CycleAttribute cycleAttribute in cycleAttributesArray)
                    {
                        if (cycleAttribute.CycleAttributeCategory == CyclePlannerControl.CycleAttributeCategory.Roundabout)
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
                    CyclePlannerControl.CycleAttributeGroup cycleAttributeGroup = GetCycleAttributeGroup(i);

                    CyclePlannerControl.CycleAttribute[] cycleAttributesArray = cycleAttributesService.GetCycleAttributes(CyclePlannerControl.CycleAttributeType.Link, cycleAttributeGroup);

                    // Get the cycle attribute
                    uint attributeValue = currentDetail.JoiningSignificantLinkAttributes[i];

                    // Specifically check for the Crossing attributes
                    foreach (CyclePlannerControl.CycleAttribute cycleAttribute in cycleAttributesArray)
                    {
                        if (cycleAttribute.CycleAttributeCategory == CyclePlannerControl.CycleAttributeCategory.Crossing)
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
            this.joiningCycleInfrastructure = false;
            this.leavingCycleInfrastructure = false;
            this.joiningRecommendedCycleRoute = false;
            this.leavingRecommendedCycleRoute = false;

            this.isRoundabout = false;
            this.isMiniRoundabout = false;
            this.isBridgeTunnel = false;
            this.isPath = false;
            this.isCrossing = false;
            this.isCycleInfrastructure = false;
            this.isRecommendedCycleRoute = false;

            this.currentITNRoadType = string.Empty;

            this.attributeBarrierText = string.Empty;
            this.attributeCharacteristicText = string.Empty;
            this.attributeCrossingText = string.Empty;
            this.attributeManoeuvreText = string.Empty;
            this.attributeTypeText = string.Empty;

            this.cycleTypeAttributes = new List<CyclePlannerControl.CycleAttribute>();
        }

        #endregion

        /// <summary>
        /// Returns a formatted html image string if the cycling detail is on cycle infrastructure
        /// </summary>
        /// <param name="detail">Array index into cycleJourney.Details for journey instruction</param>
        /// <returns>image html string</returns>
        private void SetCycleRouteType(int journeyDetailIndex)
        {
            // Assumes PreProcessCycleJourneyDetail has already been called

            
            #region Determine which cycle icon to display
            if (joiningCycleInfrastructure)
            {
                // joiningShowCycleImage indicates this link is still on cycleinfrastructure, show image
                this.isCycleInfrastructure = true;
                this.isRecommendedCycleRoute = false;

                this.previousCycleInfrastructure = true;
                this.previousRecommendedRoute = false;
            }

            else if (previousCycleInfrastructure && !leavingCycleInfrastructure)
            {
                // no cycle infrastructure attribute detected, and because we were previously showing the image,
                // continue to display it

                this.isCycleInfrastructure = true;
                this.isRecommendedCycleRoute = false;

                this.previousCycleInfrastructure = true;
                this.previousRecommendedRoute = false;
            }

                // no cycle infrastructure icon, so now check if there was a recommended route or cycle route name
            else if ((currentIsCycleRoute) || (joiningRecommendedCycleRoute))
            {
                // indicates this detail is on a recommended route, show image
               this.isRecommendedCycleRoute = true;
                this.previousRecommendedRoute = true;
            }
            // we were previously showing the recommended icon, continue to display
            else if (previousRecommendedRoute && !leavingRecommendedCycleRoute)
            {
                this.isRecommendedCycleRoute = true;
                this.previousRecommendedRoute = true;
            }

            #endregion

            // Finally update flag if we're leaving cycle infrastructure
            if (previousCycleInfrastructure && leavingCycleInfrastructure)
            {
                // previously showing the image and leaving cycle infrastructure attribute detected, show 
                // leaving infrastructure text

                // No need to set anything here as this text will be picked up by the GetCyclePathName, so
                // that it is displayed in the correct place in the UI
                this.isCycleInfrastructure = false;
                this.previousCycleInfrastructure = false;
            }

            // Finally update flag if we're leaving cycle recommended route and not on a cycle route
            if ((previousRecommendedRoute && leavingRecommendedCycleRoute && !currentIsCycleRoute)
                ||
                (!previousRecommendedRoute && !leavingRecommendedCycleRoute && !currentIsCycleRoute))
            {
                this.isRecommendedCycleRoute = false;
                this.previousRecommendedRoute = false;
            }

        }
        

        /// <summary>
        /// Returns a formatted string of the cycle path name.
        /// </summary>
        /// <param name="detail">Array index into cycleJourney.Details for journey instruction</param>
        /// <returns>Formatted string of the cycle path name and number</returns>
        private string GetCyclePathName(int journeyDetailIndex)
        {
            // Assumes PreProcessCycleJourneyDetail has already been called

            CyclePlannerControl.CycleJourneyDetail currentDetail = cycleJourney.Details[journeyDetailIndex];
            CyclePlannerControl.CycleJourneyDetail previousDetail = null;

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
                        string.Format(rm.GetString("CycleRouteText.RouteJoins", CultureInfo.CurrentUICulture),
                            cycleRouteName));

                    cyclePathText.AppendLine();
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
                            string.Format(rm.GetString("CycleRouteText.RouteJoins", CultureInfo.CurrentUICulture),
                            cycleRouteName));

                        cyclePathText.AppendLine();
                    }
                }
                #endregion
            }
            // Display any leaving cycle infrastructure text if required.
            // These flags are set when determining whether to display cycle icons (PreProcessCycleJourneyDetail)
            else if (previousCycleInfrastructure && leavingCycleInfrastructure && !leavingRecommendedCycleRoute)
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
        protected virtual string GetCyclePathName(CyclePlannerControl.CycleJourneyDetail currentDetail)
        {
            return GetCyclePathName(currentDetail, true);
        }

        /// <summary>
        /// Returns a formatted string of the cycle path name.
        /// </summary>
        /// <param name="currentDetail">Current cycle journey detail</param>
        /// <returns>Formatted string of the cycle path name and number</returns>
        protected virtual string GetCyclePathName(CyclePlannerControl.CycleJourneyDetail currentDetail, bool appendPrefix)
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
                            string.Format(rm.GetString("CycleRouteText.RouteJoins", CultureInfo.CurrentUICulture),
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
        private string GetCycleInstructionDetails(int journeyDetailIndex)
        {
            if (this.showInstructionText)
            {
                CyclePlannerControl.CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];

                StringBuilder instructionText = new StringBuilder();

                // Append any specific instructions
                if (!string.IsNullOrEmpty(detail.InstructionText))
                {
                    instructionText.Append(rm.GetString("CycleRouteText.ForThisManoeuvre", CultureInfo.CurrentUICulture));
                    instructionText.Append(space);
                    instructionText.Append(detail.InstructionText);
                   
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
                    attributeText.Append(attributeManoeuvreText);
                   
                }

                if (!string.IsNullOrEmpty(attributeTypeText))
                {
                    attributeText.Append(attributeTypeText);
                    
                }

                if (!string.IsNullOrEmpty(attributeBarrierText))
                {
                    attributeText.Append(attributeBarrierText);
                }

                if (!string.IsNullOrEmpty(attributeCharacteristicText))
                {
                    attributeText.Append(attributeCharacteristicText);
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
        /// Method which uses the Index 0 in the attributes array to obtain the ITN road type, and sets the global
        /// roadtype string to be used later when constructing attribute display text.
        /// The method assumes the attribute integer matches to only one attribute, and therefore exits after the 
        /// first match.
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="cycleAttributeList"></param>
        private void SetITNRoadType(uint[] attributes, CyclePlannerControl.CycleAttributeType cycleAttributeType)
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
                CyclePlannerControl.ICycleAttributes cycleAttributesService = (CyclePlannerControl.ICycleAttributes)TDServiceDiscovery.Current[ServiceDiscoveryKey.CycleAttributes];

                // Get the attributes we can display for this list type
                CyclePlannerControl.CycleAttribute[] cycleAttributesArray = cycleAttributesService.GetCycleAttributes(cycleAttributeType, CyclePlannerControl.CycleAttributeGroup.ITN);

                // The attribute value we're comparing
                Int64 attributeValue = (Int64)attributes[0];

                foreach (CyclePlannerControl.CycleAttribute cycleAttribute in cycleAttributesArray)
                {
                    // Bit & because CycleService returns an integer value whose bits represent flags for 
                    // which cycle attributes are true
                    if ((cycleAttribute.CycleAttributeMask & attributeValue) > 0)
                    {
                        // Add the cycle type attributes to cycletype attribute list to determine path name later
                        cycleTypeAttributes.Add(cycleAttribute);
                        // Assumption:
                        // Only 1 attribute will be true for this 

                        this.currentITNRoadType = rm.GetString(
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
            CyclePlannerControl.CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];

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

                GetAttributeTextForAttributeNumber(detail.JoiningSignificantLinkAttributes, 1, CyclePlannerControl.CycleAttributeType.Link,
                    ref attributeCrossing, ref attributeType, ref attributeTemp, ref attributeTemp, ref attributeTemp);

                GetAttributeTextForAttributeNumber(detail.JoiningSignificantLinkAttributes, 2, CyclePlannerControl.CycleAttributeType.Link,
                    ref attributeCrossing, ref attributeType, ref attributeTemp, ref attributeTemp, ref attributeTemp);

                GetAttributeTextForAttributeNumber(detail.JoiningSignificantLinkAttributes, 3, CyclePlannerControl.CycleAttributeType.Link,
                    ref attributeCrossing, ref attributeType, ref attributeTemp, ref attributeTemp, ref attributeTemp);
            }

            attributeTemp = new StringBuilder();
            #endregion

            #region Interesting Link

            if ((detail.InterestingLinkAttributes != null)
                && (detail.InterestingLinkAttributes.Length > 0))
            {
                // Go through each Group, and setup the Attribute Text for each Category

                GetAttributeTextForAttributeNumber(detail.InterestingLinkAttributes, 1, CyclePlannerControl.CycleAttributeType.Link,
                    ref attributeTemp, ref attributeTemp, ref attributeBarrier, ref attributeCharacteristic, ref attributeTemp);

                GetAttributeTextForAttributeNumber(detail.InterestingLinkAttributes, 2, CyclePlannerControl.CycleAttributeType.Link,
                    ref attributeTemp, ref attributeTemp, ref attributeBarrier, ref attributeCharacteristic, ref attributeTemp);

                GetAttributeTextForAttributeNumber(detail.InterestingLinkAttributes, 3, CyclePlannerControl.CycleAttributeType.Link,
                    ref attributeTemp, ref attributeTemp, ref attributeBarrier, ref attributeCharacteristic, ref attributeTemp);
            }

            attributeTemp = new StringBuilder();
            #endregion

            #region SectionFeature Stopover

            if ((detail.SectionFeatureAttributes != null)
                && (detail.SectionFeatureAttributes.Length > 0))
            {
                // Go through each Group, and setup the Attribute Text for each Category

                GetAttributeTextForAttributeNumber(detail.SectionFeatureAttributes, 1, CyclePlannerControl.CycleAttributeType.Stopover,
                    ref attributeTemp, ref attributeTemp, ref attributeTemp, ref attributeTemp, ref attributeManoeuvre);

                GetAttributeTextForAttributeNumber(detail.SectionFeatureAttributes, 2, CyclePlannerControl.CycleAttributeType.Stopover,
                    ref attributeTemp, ref attributeTemp, ref attributeTemp, ref attributeTemp, ref attributeManoeuvre);

                GetAttributeTextForAttributeNumber(detail.SectionFeatureAttributes, 3, CyclePlannerControl.CycleAttributeType.Stopover,
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
                                        CyclePlannerControl.CycleAttributeType cycleAttributeType,
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
                CyclePlannerControl.CycleAttributeGroup cycleAttributeGroup = GetCycleAttributeGroup(index);

                // Get the cycle attributes we're allowed to display from service discovery
                CyclePlannerControl.ICycleAttributes cycleAttributesService = (CyclePlannerControl.ICycleAttributes)TDServiceDiscovery.Current[ServiceDiscoveryKey.CycleAttributes];

                // Get the attributes we can display for this list type
                CyclePlannerControl.CycleAttribute[] cycleAttributesArray = cycleAttributesService.GetCycleAttributes(cycleAttributeType, cycleAttributeGroup);

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
        private CyclePlannerControl.CycleAttributeGroup GetCycleAttributeGroup(int attributeIndex)
        {
            //Determine the Group this attribute integer belongs to
            switch (attributeIndex)
            {
                case 1:
                    return CyclePlannerControl.CycleAttributeGroup.User0;

                case 2:
                    return CyclePlannerControl.CycleAttributeGroup.User1;

                case 3:
                    return CyclePlannerControl.CycleAttributeGroup.User2;

                default:
                    return CyclePlannerControl.CycleAttributeGroup.ITN;
            }
        }

        /// <summary>
        /// Checks if the attributeValue Bit flags matches any in the cycleAttributesArray. If any do,
        /// they are added to the appropriate text string depending on the CycleAttribute.Category.
        /// </summary>
        private void GetAttributeText(CyclePlannerControl.CycleAttribute[] cycleAttributesArray,
                                        Int64 attributeValue,
                                        ref StringBuilder attributeCrossingText,
                                        ref StringBuilder attributeTypeText,
                                        ref StringBuilder attributeBarrierText,
                                        ref StringBuilder attributeCharacteristicText,
                                        ref StringBuilder attributeManoeuvreText)
        {
            // Loop through the attributes we're allowed to display and set text accordingly
            foreach (CyclePlannerControl.CycleAttribute cycleAttribute in cycleAttributesArray)
            {
                // Bit & because CycleService returns an integer value whose bits represent flags for 
                // which cycle attributes are true
                if ((cycleAttribute.CycleAttributeMask & attributeValue) > 0)
                {
                    switch (cycleAttribute.CycleAttributeCategory)
                    {
                        case CyclePlannerControl.CycleAttributeCategory.Crossing:
                            // Check for sentence prefix
                            if (attributeCrossingText.Length == 0)
                            {
                                attributeCrossingText.Append(rm.GetString("CycleRouteText.PleaseCross", CultureInfo.CurrentUICulture));
                                attributeCrossingText.Append(space);
                            }

                            AddAttributeNumber(cycleAttribute, attributeValue, ref attributeCrossingText);

                            attributeCrossingText.Append(
                                rm.GetString(
                                cycleAttribute.CycleAttributeResourceName, CultureInfo.CurrentUICulture));

                            attributeCrossingText.Append(seperator);

                            break;

                        case CyclePlannerControl.CycleAttributeCategory.Type:
                            // Check for sentence prefix
                            if (attributeTypeText.Length == 0)
                            {
                                attributeTypeText.Append(rm.GetString("CycleRouteText.RouteUses", CultureInfo.CurrentUICulture));
                                attributeTypeText.Append(space);
                            }

                            cycleTypeAttributes.Add(cycleAttribute);

                            AddAttributeNumber(cycleAttribute, attributeValue, ref attributeTypeText);

                            attributeTypeText.Append(
                                rm.GetString(
                                cycleAttribute.CycleAttributeResourceName, CultureInfo.CurrentUICulture));

                            attributeTypeText.Append(seperator);

                            break;

                        case CyclePlannerControl.CycleAttributeCategory.Barrier:
                            // Check for sentence prefix
                            if (attributeBarrierText.Length == 0)
                            {
                                attributeBarrierText.Append(rm.GetString("CycleRouteText.PleaseNote", CultureInfo.CurrentUICulture));
                                attributeBarrierText.Append(space);
                            }

                            AddAttributeNumber(cycleAttribute, attributeValue, ref attributeBarrierText);

                            attributeBarrierText.Append(
                                rm.GetString(
                                cycleAttribute.CycleAttributeResourceName, CultureInfo.CurrentUICulture));

                            attributeBarrierText.Append(seperator);

                            break;

                        case CyclePlannerControl.CycleAttributeCategory.Characteristic:
                            // Check for sentence prefix
                            if (attributeCharacteristicText.Length == 0)
                            {
                                attributeCharacteristicText.Append(string.Format(
                                    rm.GetString("CycleRouteText.TheStreetIs", CultureInfo.CurrentUICulture),
                                    this.currentITNRoadType));
                                attributeCharacteristicText.Append(space);
                            }

                            AddAttributeNumber(cycleAttribute, attributeValue, ref attributeCharacteristicText);

                            attributeCharacteristicText.Append(
                                rm.GetString(
                                cycleAttribute.CycleAttributeResourceName, CultureInfo.CurrentUICulture));

                            attributeCharacteristicText.Append(seperator);

                            break;

                        case CyclePlannerControl.CycleAttributeCategory.Manoeuvrability:

                            AddAttributeNumber(cycleAttribute, attributeValue, ref attributeManoeuvreText);

                            attributeManoeuvreText.Append(
                                rm.GetString(
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
        private void AddAttributeNumber(CyclePlannerControl.CycleAttribute cycleAttribute, Int64 attributeValue, ref StringBuilder attributeText)
        {
            if (this.showAttributeNumber)
            {
                attributeText.Append(attributeValue.ToString());
                attributeText.Append(" ");
                attributeText.Append(cycleAttribute.CycleAttributeId.ToString());
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
                string formattedText = attributeText.ToString().Trim().TrimEnd(comma);

                int lastCommaIndex = formattedText.LastIndexOf(comma);

                if (lastCommaIndex > 0)
                {
                    // Remove and replace the last comma with an "and"
                    formattedText = formattedText.Remove(lastCommaIndex, 1);
                    formattedText = formattedText.Insert(lastCommaIndex, " " + rm.GetString("CycleRouteText.And"));
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
            CyclePlannerControl.CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];

            //last Detail, which is also needed for formatting logic			
            CyclePlannerControl.CycleJourneyDetail lastDetail = cycleJourney.Details[cycleJourney.Details.Length - 1];

            //next Detail, which is also needed for formatting logic 
            CyclePlannerControl.CycleJourneyDetail nextDetail = null;
            if (detail != lastDetail)
            {
                nextDetail = cycleJourney.Details[journeyDetailIndex + 1];
            }
            else
            {
                nextDetail = lastDetail;
            }

            //previous Detail, which is also needed for formatting logic
            CyclePlannerControl.CycleJourneyDetail previousDetail = null;
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


                //if (detail.Congestion == true)
                //{
                //    routeText = AddCongestionSymbol(routeText);
                //}

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
        /// Generates the route text for the given CycleJourney where
        /// the CycleJourney goes over a "Roundabout".
        /// </summary>
        /// <param name="detail">Cycle Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string Roundabout(CyclePlannerControl.CycleJourneyDetail detail)
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
        protected virtual string TollEntry(CyclePlannerControl.CycleJourneyDetail detail)
        {
            StringBuilder routeText = new StringBuilder(string.Empty);

            //Convert TollCost value to pounds & pence
            decimal pence = (Convert.ToDecimal(detail.TollCost)) / 100;
            string cost = string.Format("{0:C}", pence);

            //display company url as hyperplink if it exists
            if (detail.CompanyUrl.Length != 0)
            {
                //add "Toll:" to start of instruction
                routeText.Append(toll);
                routeText.Append(space);
                routeText.Append(detail.TollEntryName);
                routeText.Append(space);
                routeText.Append("http://");
                routeText.Append(detail.CompanyUrl.Trim());
               
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
                routeText.Append(chargeAdultAndCycle);
                routeText.Append(notAvailable);
               
            }
            else if (pence == 0)
            {
                routeText.Append(space);
                routeText.Append(chargeAdultAndCycle);
                routeText.Append("£0.00");
                
            }
            else
            {
                routeText.Append(space);
                routeText.Append(chargeAdultAndCycle);
                routeText.Append(cost);
                
            }
            return routeText.ToString();
        }

        /// <summary>
        /// Generates the route text for the given CycleJourneyDetail where
        /// the CycleJourneyDetail is a StopoverSection of type 'TollEntry'.
        /// </summary>
        /// <param name="detail">Cycle Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string TollExit(CyclePlannerControl.CycleJourneyDetail detail)
        {
            StringBuilder routeText = new StringBuilder(string.Empty);

            //Convert TollCost value to pounds & pence
            decimal pence = (Convert.ToDecimal(detail.TollCost)) / 100;
            string cost = string.Format("{0:C}", pence);

            //display company url as hyerplink if it exists
            if (detail.CompanyUrl.Length != 0)
            {
                //add "Toll:" to start of instruction	
                routeText.Append(toll);
                routeText.Append(space);
                routeText.Append(detail.TollExitName);
                routeText.Append(space);
                routeText.Append("http://");
                routeText.Append(detail.CompanyUrl.Trim());
                
            }
            else
            {
                routeText.Append(toll);
                routeText.Append(space);
                routeText.Append(detail.TollExitName);
            }

            routeText.Append(space);
            routeText.Append(charge);

            return routeText.ToString();
        }

        /// <summary>
        /// Generates the route text for the given CycleJourneyDetail where
        /// the CycleJourneyDetail is a StopoverSection of type 'FerryEntry'.
        /// </summary>
        /// <param name="detail">Cycle Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string FerryEntry(CyclePlannerControl.CycleJourneyDetail detail, CyclePlannerControl.CycleJourneyDetail previousDetail)
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
                if (detail.CompanyUrl.Length != 0)
                {
                    routeText.Append(board);
                    routeText.Append(space);
                    routeText.Append(detail.FerryCheckInName);
                    routeText.Append(space);
                    routeText.Append("http://");
                    routeText.Append(detail.CompanyUrl.Trim());
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
                if (detail.CompanyUrl.Length != 0)
                {
                    routeText.Append(board);
                    routeText.Append(space);
                    routeText.Append(detail.FerryCheckInName);
                    routeText.Append(space);
                    routeText.Append("http://");
                    routeText.Append(detail.CompanyUrl.Trim());
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
                routeText.Append(chargeAdultAndCycle);
                routeText.Append(notAvailable);
            }
            else if (pence == 0)
            {
                //when charge is unknown "Charge: Not Available"
                routeText.Append(";");
                routeText.Append(space);
                routeText.Append(chargeAdultAndCycle);
                routeText.Append("£0.00");
            }
            else
            {
                routeText.Append(";");
                routeText.Append(space);
                routeText.Append(chargeAdultAndCycle);
                routeText.Append(cost);
            }
            return routeText.ToString();
        }

        /// <summary>
        /// Generates the route text for the given CycleJourneyDetail where
        /// the CycleJourneyDetail is a StopoverSection of type 'FerryExit'.
        /// </summary>
        /// <param name="detail">Cycle Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string FerryExit(CyclePlannerControl.CycleJourneyDetail detail)
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
        protected virtual string WaitForFerry(CyclePlannerControl.CycleJourneyDetail detail, CyclePlannerControl.CycleJourneyDetail previousDetail, CyclePlannerControl.CycleJourneyDetail nextDetail)
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
        protected virtual string UndefindedFerryWait(CyclePlannerControl.CycleJourneyDetail detail, CyclePlannerControl.CycleJourneyDetail previousDetail, CyclePlannerControl.CycleJourneyDetail nextDetail)
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
        protected virtual string ComplexManoeuvre(CyclePlannerControl.CycleJourneyDetail detail)
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
        protected virtual string NamedAccessRestriction(CyclePlannerControl.CycleJourneyDetail detail)
        {
            string routeText = rm.GetString
                ("CycleRouteText.TimeBasedAccessRestriction", CultureInfo.CurrentUICulture);

            routeText += detail.NamedAccessRestrictionText;

            return routeText;
        }

        /// <summary>
        /// Generates the Named time text for the given CycleJourneyDetail where
        /// the CycleJourneyDetail is a StopoverSection of type 'NamedTime'.
        /// </summary>
        /// <param name="detail">Cycle Journey Detail to generate named time text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string NamedTime(CyclePlannerControl.CycleJourneyDetail detail)
        {
            string routeText = rm.GetString
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
        protected virtual string CongestionEntry(CyclePlannerControl.CycleJourneyDetail detail, bool showCongestionCharge)
        {
            StringBuilder routeText = new StringBuilder(string.Empty);
            decimal pence = (Convert.ToDecimal(detail.TollCost)) / 100;
            string cost = string.Format("{0:C}", pence);

            //display company name as hyperplink if it exists
            if (detail.CompanyUrl.Length != 0)
            {
                routeText.Append(enter);
                routeText.Append(space);
                routeText.Append(detail.CongestionZoneEntry);
                routeText.Append(space);
                routeText.Append("http://");
                routeText.Append(detail.CompanyUrl.Trim());
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
            if ((detail.TollCost > 0) && ((showCongestionCharge)))
            {
                //Viewstate contains this C Charge and we don't want to show it
                if (!showCongestionCharge)
                {
                    // No toll charge for this day/time
                    routeText.Append(fullstop);
                    routeText.Append(space);
                    routeText.Append(charge);
                    routeText.Append("£0.00");
                    routeText.Append(space);
                    routeText.Append(certainTimesNoCharge);
                }
                else

                //We want to show it
                {
                    routeText.Append(fullstop);
                    routeText.Append(space);
                    routeText.Append(charge);
                    routeText.Append(cost);
                    routeText.Append(space);
                    routeText.Append(certainTimes);
                }
            }

            else if ((detail.TollCost == 0) || (!showCongestionCharge))
            {
                // No toll charge for this day/time
                routeText.Append(fullstop);
                routeText.Append(space);
                routeText.Append(charge);
                routeText.Append("£0.00");
                routeText.Append(space);
                routeText.Append(certainTimesNoCharge);
            }
            else
            {
                //in the event the charge is unavailable - IR2499
                routeText.Append(space);
                routeText.Append(charge);
                routeText.Append(notAvailable);
            }

            return routeText.ToString();
        }

        /// <summary>
        /// Generates the route text for the given CycleJourneyDetail where
        /// the CycleJourneyDetail is a StopoverSection of type 'CongestionExit'.
        /// </summary>
        /// <param name="detail">Cycle Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string CongestionExit(CyclePlannerControl.CycleJourneyDetail detail, bool showCongestionCharge)
        {
            StringBuilder routeText = new StringBuilder(string.Empty);
            decimal pence = (Convert.ToDecimal(detail.TollCost)) / 100;
            string cost = string.Format("{0:C}", pence);

            if (detail.CompanyUrl.Length != 0)
            {
                //add "Exit" to start of instruction						
                routeText.Append(exit);
                routeText.Append(space);
                routeText.Append(detail.CongestionZoneExit);
                routeText.Append(space);
                routeText.Append("http://");
                routeText.Append(detail.CompanyUrl.Trim());
                
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
            if ((detail.TollCost > 0) && ((showCongestionCharge)))
            {
                //Viewstate contains this C Charge and we don't want to show it
                if (!showCongestionCharge)
                {
                    // No toll charge for this day/time
                    routeText.Append(fullstop);
                    routeText.Append(space);
                    routeText.Append(charge);
                    routeText.Append("£0.00");
                    routeText.Append(space);
                    routeText.Append(certainTimesNoCharge);
                }
                else
                //We want to show it
                {
                    routeText.Append(fullstop);
                    routeText.Append(space);
                    routeText.Append(charge);
                    routeText.Append(cost);
                    routeText.Append(space);
                    routeText.Append(certainTimes);
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
                routeText.Append(charge);
                routeText.Append(notAvailable);
            }

            return routeText.ToString();
        }

        /// <summary>
        /// Generates the route text for the given CycleJourneyDetail where
        /// the CycleJourneyDetail is a StopoverSection of type 'CongestionEnd'.
        /// </summary>
        /// <param name="detail">Cycle Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string CongestionEnd(CyclePlannerControl.CycleJourneyDetail detail, bool showCongestionCharge)
        {
            StringBuilder routeText = new StringBuilder(string.Empty);
            decimal pence = (Convert.ToDecimal(detail.TollCost)) / 100;
            string cost = string.Format("{0:C}", pence);

            if (detail.CompanyUrl.Length != 0)
            {
                //add "End" to start of instruction						
                routeText.Append(end);
                routeText.Append(space);
                routeText.Append(detail.CongestionZoneEnd);
                routeText.Append(space);
                routeText.Append("http://");
                routeText.Append(detail.CompanyUrl.Trim());
                
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
            if ((detail.TollCost > 0) && ((showCongestionCharge)))
            {
                //Viewstate contains this C Charge and we don't want to show it
                if (!showCongestionCharge)
                {
                    // No toll charge for this day/time
                    routeText.Append(fullstop);
                    routeText.Append(space);
                    routeText.Append(charge);
                    routeText.Append("£0.00");
                    routeText.Append(space);
                    routeText.Append(certainTimesNoCharge);
                }
                else
                //We want to show it
                {
                    routeText.Append(fullstop);
                    routeText.Append(space);
                    routeText.Append(charge);
                    routeText.Append(cost);
                    routeText.Append(space);
                    routeText.Append(certainTimes);
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
                routeText.Append(charge);
                routeText.Append(notAvailable);
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
            routeText = viaArriveAt + space + cycleJourney.RequestedViaLocation.Description;
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
            CyclePlannerControl.CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];

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
            CyclePlannerControl.CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];

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
            CyclePlannerControl.CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];

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
            CyclePlannerControl.CycleJourneyDetail detail = cycleJourney.Details[journeyDetailIndex];

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
        protected virtual string AddPlaceName(CyclePlannerControl.CycleJourneyDetail detail, string routeText)
        {
            //add "towards {placename}" to the end of the instruction 
            routeText = routeText + space + towards + detail.PlaceName;

            return routeText;
        }

        /// <summary>
        /// Checks the subsequent RoadJourneyDetail
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="routeText"></param>
        /// <returns></returns>
        protected virtual string CheckNextDetail(CyclePlannerControl.CycleJourneyDetail nextDetail, string routeText)
        {
            //add "until junction number {no}" to end of current instruction
            if ((nextDetail.JunctionSection) && ((nextDetail.JunctionType == JunctionType.Exit) || (nextDetail.JunctionType == JunctionType.Merge)))
            {
                routeText = routeText + space + untilJunction + nextDetail.JunctionNumber;
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
        protected virtual string FormatRoadName(CyclePlannerControl.CycleJourneyDetail detail)
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
                        string footpathString = rm.GetString("CycleAttribute.Footpath", CultureInfo.CurrentUICulture);

                        string cyclepathString = rm.GetString("CycleAttribute.Cyclepath", CultureInfo.CurrentUICulture);

                        string footpathOnlyString = rm.GetString("CycleAttribute.FootpathOnly", CultureInfo.CurrentUICulture);

                        string cyclepathOnlyString = rm.GetString("CycleAttribute.CyclesOnly", CultureInfo.CurrentUICulture);

                        string towpathString = rm.GetString("CycleAttribute.Towpath", CultureInfo.CurrentUICulture);

                        string sharedUseFootpathString = rm.GetString("CycleAttribute.SharedUseFootpath", CultureInfo.CurrentUICulture);

                        string bridlepathString = rm.GetString("CycleAttribute.Bridlepath", CultureInfo.CurrentUICulture);

                        // Check whether the type of path is cyclepath, bridlepath, towpath or footpath
                        foreach (CyclePlannerControl.CycleAttribute attribute in cycleTypeAttributes)
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
                                    if (!string.IsNullOrEmpty(currentITNRoadType))
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
        private string GetFirstAggregatedWayName(CyclePlannerControl.CycleJourneyDetail detail)
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
        protected virtual string FormatMotorwayJunction(CyclePlannerControl.CycleJourneyDetail detail, string routeText)
        {
            string junctionText = String.Empty;

            //apply the junction number rules 
            switch (detail.JunctionType)
            {
                case JunctionType.Entry:

                    //add a join motorway message to the instructional text
                    routeText = routeText + space + atJunctionJoin 
                        + detail.JunctionNumber;

                    break;

                case JunctionType.Exit:

                    //replace the normal instructional text with a leave motorway message
                    routeText = atJunctionLeave + detail.JunctionNumber + leaveMotorway;

                    break;

                case JunctionType.Merge:

                    //replace the normal instructional text with a leave motorway message
                    routeText = atJunctionLeave + detail.JunctionNumber + leaveMotorway;

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
                string secondsString = rm.GetString(
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

        #region Geometry
        /// <summary>
        /// Converts OSGR co-ordinates this detail travels to geometry(polyline) string of the OSGR coordinates 
        /// The coordinate points are separated as specified in the result settings
        /// </summary>
        /// <param name="osGridReferences"></param>
        /// <returns></returns>
        private string GetGeometry(Dictionary<int, OSGridReference[]> osGridReferences)
        {
            StringBuilder geometryBuilder = new StringBuilder();
            bool first = true;

            if (osGridReferences != null)
            {
                foreach (KeyValuePair<int, OSGridReference[]> gridReferences in osGridReferences)
                {
                    foreach (OSGridReference gridReference in gridReferences.Value)
                    {
                        if (!first)
                        {
                            geometryBuilder.Append(Convert.ToChar(resultSettings.PointSeparator));
                        }
                        else
                        {
                            first = false;
                        }

                        geometryBuilder.AppendFormat("{0}{1}{2}",
                            gridReference.Easting.ToString(),
                            Convert.ToChar(resultSettings.EastingNorthingSeparator),
                            gridReference.Northing.ToString());
                    }
                }
            }

            return geometryBuilder.ToString();

        }

        #endregion

       

        /// <summary>
        /// Initialises all language sensitive text strings using
        /// the resource manager.
        /// </summary>
        private void InitialiseRouteTextDescriptionStrings()
        {
            throughRoute = rm.GetString
                ("RouteText.ThroughRoute", CultureInfo.CurrentUICulture);

            continueString = rm.GetString
                ("RouteText.Continue", CultureInfo.CurrentUICulture);

            roundaboutExitOne = rm.GetString
                ("RouteText.RoundaboutExitOne", CultureInfo.CurrentUICulture);
            roundaboutExitTwo = rm.GetString
                ("RouteText.RoundaboutExitTwo", CultureInfo.CurrentUICulture);
            roundaboutExitThree = rm.GetString
                ("RouteText.RoundaboutExitThree", CultureInfo.CurrentUICulture);
            roundaboutExitFour = rm.GetString
                ("RouteText.RoundaboutExitFour", CultureInfo.CurrentUICulture);
            roundaboutExitFive = rm.GetString
                ("RouteText.RoundaboutExitFive", CultureInfo.CurrentUICulture);
            roundaboutExitSix = rm.GetString
                ("RouteText.RoundaboutExitSix", CultureInfo.CurrentUICulture);
            roundaboutExitSeven = rm.GetString
                ("RouteText.RoundaboutExitSeven", CultureInfo.CurrentUICulture);
            roundaboutExitEight = rm.GetString
                ("RouteText.RoundaboutExitEight", CultureInfo.CurrentUICulture);
            roundaboutExitNine = rm.GetString
                ("RouteText.RoundaboutExitNine", CultureInfo.CurrentUICulture);
            roundaboutExitTen = rm.GetString
                ("RouteText.RoundaboutExitTen", CultureInfo.CurrentUICulture);

            turnLeftOne = rm.GetString
                ("RouteText.TurnLeftOne", CultureInfo.CurrentUICulture);
            turnLeftTwo = rm.GetString
                ("RouteText.TurnLeftTwo", CultureInfo.CurrentUICulture);
            turnLeftThree = rm.GetString
                ("RouteText.TurnLeftThree", CultureInfo.CurrentUICulture);
            turnLeftFour = rm.GetString
                ("RouteText.TurnLeftFour", CultureInfo.CurrentUICulture);

            turnRightOne = rm.GetString
                ("RouteText.TurnRightOne", CultureInfo.CurrentUICulture);
            turnRightTwo = rm.GetString
                ("RouteText.TurnRightTwo", CultureInfo.CurrentUICulture);
            turnRightThree = rm.GetString
                ("RouteText.TurnRightThree", CultureInfo.CurrentUICulture);
            turnRightFour = rm.GetString
                ("RouteText.TurnRightFour", CultureInfo.CurrentUICulture);

            turnLeftOne2 = rm.GetString
                ("RouteText.TurnLeftOne2", CultureInfo.CurrentUICulture);
            turnLeftTwo2 = rm.GetString
                ("RouteText.TurnLeftTwo2", CultureInfo.CurrentUICulture);
            turnLeftThree2 = rm.GetString
                ("RouteText.TurnLeftThree2", CultureInfo.CurrentUICulture);
            turnLeftFour2 = rm.GetString
                ("RouteText.TurnLeftFour2", CultureInfo.CurrentUICulture);

            turnRightOne2 = rm.GetString
                ("RouteText.TurnRightOne2", CultureInfo.CurrentUICulture);
            turnRightTwo2 = rm.GetString
                ("RouteText.TurnRightTwo2", CultureInfo.CurrentUICulture);
            turnRightThree2 = rm.GetString
                ("RouteText.TurnRightThree2", CultureInfo.CurrentUICulture);
            turnRightFour2 = rm.GetString
                ("RouteText.TurnRightFour2", CultureInfo.CurrentUICulture);

            turnLeftInDistance = rm.GetString
                ("RouteText.TurnLeftInDistance", CultureInfo.CurrentUICulture);
            turnRightInDistance = rm.GetString
                ("RouteText.TurnRightInDistance", CultureInfo.CurrentUICulture);

            bearLeft = rm.GetString
                ("RouteText.BearLeft", CultureInfo.CurrentUICulture);
            bearRight = rm.GetString
                ("RouteText.BearRight", CultureInfo.CurrentUICulture);

            immediatelyBearLeft = rm.GetString
                ("RouteText.ImmediatelyBearLeft", CultureInfo.CurrentUICulture);
            immediatelyBearRight = rm.GetString
                ("RouteText.ImmediatelyBearRight", CultureInfo.CurrentUICulture);


            arriveAt = rm.GetString
                ("RouteText.ArriveAt", CultureInfo.CurrentUICulture);

            leaveFrom = rm.GetString
                ("RouteText.Leave", CultureInfo.CurrentUICulture);

            notApplicable = rm.GetString
                ("RouteText.NotApplicable", CultureInfo.CurrentUICulture);

            localRoad = rm.GetString
                ("RouteText.LocalRoad", CultureInfo.CurrentUICulture);

            localPath = rm.GetString
                ("RouteText.LocalPath", CultureInfo.CurrentUICulture);

            street = rm.GetString
                ("RouteText.Street", CultureInfo.CurrentUICulture);

            path = rm.GetString
                ("RouteText.Path", CultureInfo.CurrentUICulture);

            //motorway instructions
            atJunctionLeave = rm.GetString
                ("RouteText.AtJunctionLeave", CultureInfo.CurrentUICulture);

            leaveMotorway = rm.GetString
                ("RouteText.LeaveMotorway", CultureInfo.CurrentUICulture);

            untilJunction = rm.GetString
                ("RouteText.UntilJunction", CultureInfo.CurrentUICulture);

            towards = rm.GetString
                ("RouteText.Towards", CultureInfo.CurrentUICulture);

            continueFor = rm.GetString
                ("RouteText.ContinueFor", CultureInfo.CurrentUICulture);

            miles = rm.GetString
                ("RouteText.Miles", CultureInfo.CurrentUICulture);

            turnLeftToJoin = rm.GetString
                ("RouteText.TurnLeftToJoin", CultureInfo.CurrentUICulture);

            turnRightToJoin = rm.GetString
                ("RouteText.TurnRightToJoin", CultureInfo.CurrentUICulture);

            atJunctionJoin = rm.GetString
                ("RouteText.AtJunctionJoin", CultureInfo.CurrentUICulture);

            bearLeftToJoin = rm.GetString
                ("RouteText.BearLeftToJoin", CultureInfo.CurrentUICulture);

            bearRightToJoin = rm.GetString
                ("RouteText.BearRightToJoin", CultureInfo.CurrentUICulture);

            join = rm.GetString
                ("RouteText.Join", CultureInfo.CurrentUICulture);

            follow = rm.GetString
                ("RouteText.Follow", CultureInfo.CurrentUICulture);

            to = rm.GetString
                ("RouteText.To", CultureInfo.CurrentUICulture);

            enter = rm.GetString
                ("RouteText.Enter", CultureInfo.CurrentUICulture);
            congestionZone = rm.GetString
                ("RouteText.CongestionCharge", CultureInfo.CurrentUICulture);
            charge = rm.GetString
                ("RouteText.Charge", CultureInfo.CurrentUICulture);
            chargeAdultAndCycle = rm.GetString
                ("RouteText.ChargeAdultAndCycle", CultureInfo.CurrentUICulture);
            certainTimes = rm.GetString
                ("RouteText.CertainTimes", CultureInfo.CurrentUICulture);
            certainTimesNoCharge = rm.GetString
                ("RouteText.CertainTimesNoCharge", CultureInfo.CurrentUICulture);
            board = rm.GetString
                ("RouteText.Board", CultureInfo.CurrentUICulture);
            departingAt = rm.GetString
                ("RouteText.DepartingAt", CultureInfo.CurrentUICulture);
            toll = rm.GetString
                ("RouteText.Toll", CultureInfo.CurrentUICulture);
            ferryWait = rm.GetString
                ("RouteText.WaitForFerry", CultureInfo.CurrentUICulture);
            viaArriveAt = rm.GetString
                ("RouteText.ViaArriveAt", CultureInfo.CurrentUICulture);
            leaveFerry = rm.GetString
                ("RouteText.LeaveFerry", CultureInfo.CurrentUICulture);
            exit = rm.GetString
                ("RouteText.Exit", CultureInfo.CurrentUICulture);
            end = rm.GetString
                ("RouteText.End", CultureInfo.CurrentUICulture);
            unspecifedFerryWait = rm.GetString
                ("RouteText.UnspecifedWaitForFerry", CultureInfo.CurrentUICulture);
            intermediateFerryWait = rm.GetString
                ("RouteText.IntermediateFerry", CultureInfo.CurrentUICulture);
            waitAtTerminal = rm.GetString
                ("RouteText.WaitAtTerminal", CultureInfo.CurrentUICulture);
            notAvailable = rm.GetString
                ("RouteText.NotAvailable", CultureInfo.CurrentUICulture);

            straightOn = rm.GetString
                ("RouteText.StraightOn", CultureInfo.CurrentUICulture);
            atMiniRoundabout = rm.GetString
                ("RouteText.AtMiniRoundabout", CultureInfo.CurrentUICulture);
            atMiniRoundabout2 = rm.GetString
                ("RouteText.AtMiniRoundabout2", CultureInfo.CurrentUICulture);
            immediatelyTurnRightOnto = rm.GetString
                ("RouteText.ImmediatelyTurnRightOnto", CultureInfo.CurrentUICulture);
            immediatelyTurnLeftOnto = rm.GetString
                ("RouteText.ImmediatelyTurnLeftOnto", CultureInfo.CurrentUICulture);
            whereRoadSplits = rm.GetString
                ("RouteText.WhereRoadSplits", CultureInfo.CurrentUICulture);
            onto = rm.GetString
                ("RouteText.OnTo", CultureInfo.CurrentUICulture);
            uTurn = rm.GetString
                ("RouteText.UTurn", CultureInfo.CurrentUICulture);

           
        }

        #endregion
    }
}
