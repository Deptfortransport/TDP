// *********************************************** 
// NAME                 : CarJourneyDetailFormatter.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Class for creating car journey detail instructions.
//                      : This class has been copied from Web2/Adapters/CarJourneyDetailFormatter and modified 
//                      : to create driving instructions for the Car Exposed Service
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/CarJourneyDetailFormatter.cs-arc  $
//
//   Rev 1.1   Mar 14 2011 15:11:50   RPhilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.0   Aug 04 2009 14:41:12   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.PropertyService.Properties;

using cjpinterface = TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1
{
    public class CarJourneyDetailFormatter
    {
        #region Private members

        // Road journey to render.
        protected RoadJourney roadJourney;

        // Unit to use for calculating distances
        protected DistanceUnit distanceUnit;

        // Resource manager to get the text from
        protected TDResourceManager rm;

        // True if journey details are for outward journey, false if return
        protected bool outward;

        // Indicates if the return journey is on the same day, used in decision for displaying congestion charge
        protected bool sameDayReturn;

        #region Statics 

        // Constant representing space used in string formatting
        protected readonly static string space = " ";

        // Constant representing space used in string formatting
        protected readonly static string comma = ",";

        // Constant representing full stop used in string formatting
        protected readonly static string fullstop = ".";

        #endregion

        #region Strings that are used to generate the route text descriptions.

        protected string throughRoute = string.Empty;

        // string for roundabout exit
        protected string roundaboutExitOne = string.Empty;
        protected string roundaboutExitTwo = string.Empty;
        protected string roundaboutExitThree = string.Empty;
        protected string roundaboutExitFour = string.Empty;
        protected string roundaboutExitFive = string.Empty;
        protected string roundaboutExitSix = string.Empty;
        protected string roundaboutExitSeven = string.Empty;
        protected string roundaboutExitEight = string.Empty;
        protected string roundaboutExitNine = string.Empty;
        protected string roundaboutExitTen = string.Empty;


        protected string continueString = string.Empty;
        protected string continueMiniRoundabout = string.Empty;
        protected string leftMiniRoundabout = string.Empty;
        protected string rightMiniRoundabout = string.Empty;
        protected string uTurnMiniRoundabout = string.Empty;
        protected string leftMiniRoundabout2 = string.Empty;
        protected string rightMiniRoundabout2 = string.Empty;
        protected string uTurnMiniRoundabout2 = string.Empty;


        protected string immediatelyTurnLeft = string.Empty;
        protected string immediatelyTurnRight = string.Empty;

        protected string turnLeftOne = string.Empty;
        protected string turnLeftTwo = string.Empty;
        protected string turnLeftThree = string.Empty;
        protected string turnLeftFour = string.Empty;


        protected string turnRightOne = string.Empty;
        protected string turnRightTwo = string.Empty;
        protected string turnRightThree = string.Empty;
        protected string turnRightFour = string.Empty;


        protected string turnLeftInDistance = string.Empty;
        protected string turnRightInDistance = string.Empty;


        protected string bearLeft = string.Empty;
        protected string bearRight = string.Empty;

        protected string immediatelyBearLeft = string.Empty;
        protected string immediatelyBearRight = string.Empty;


        protected string arriveAt = string.Empty;
        protected string leaveFrom = string.Empty;
        protected string notApplicable = string.Empty;
        protected string localRoad = string.Empty;


        //motorway instructions
        protected string atJunctionLeave = string.Empty;
        protected string leaveMotorway = string.Empty;
        protected string untilJunction = string.Empty;
        protected string onTo = string.Empty;
        protected string towards = string.Empty;
        protected string continueFor = string.Empty;
        protected string miles = string.Empty;
        protected string turnLeftToJoin = string.Empty;
        protected string turnRightToJoin = string.Empty;
        protected string atJunctionJoin = string.Empty;
        protected string bearLeftToJoin = string.Empty;
        protected string bearRightToJoin = string.Empty;
        protected string join = string.Empty;
        protected string forText = string.Empty;
        protected string follow = string.Empty;
        protected string to = string.Empty;
        protected string routeTextFor = string.Empty;
        protected string continueText = string.Empty;


        //Del 7 Car Costing strings
        protected string enter = string.Empty;
        protected string congestionZone = string.Empty;
        protected string charge = string.Empty;
        protected string certainTimes = string.Empty;
        protected string certainTimesNoCharge = string.Empty;
        protected string board = string.Empty;
        protected string departingAt = string.Empty;
        protected string toll = string.Empty;
        protected string HighTraffic = string.Empty;
        protected string PlanStop = string.Empty;
        protected string FerryWait = string.Empty;
        protected string FerryCrossing = string.Empty;
        protected string viaArriveAt = string.Empty;
        protected string leaveFerry = string.Empty;
        protected string exit = string.Empty;
        protected string end = string.Empty;
        protected string UnspecifedFerryWait = string.Empty;
        protected string IntermediateFerryWait = string.Empty;
        protected string WaitAtTerminal = string.Empty;
        protected string notAvailable = string.Empty;

        
        protected string straightOn = string.Empty;
        protected string atMiniRoundabout = string.Empty;
        protected string immediatelyTurnRightOnto = string.Empty;
        protected string immediatelyTurnLeftOnto = string.Empty;
        protected string whereRoadSplits = string.Empty;

        
        // park and ride
        protected string parkAndRide = string.Empty;
        protected string carParkText = string.Empty;

        #endregion

        #region Internal variables

        // Road distance in metres within which "immediate" text should be 
        // prepended to journey direction
        protected int immediateTurnDistance;

        // Slip road distance limit parameter
        protected int slipRoadDistance;
        
        #endregion

        #region Viewstate variables

        // Indicating whether a congestion charge company's charge has already  
        // been applied to the complete journey 
        protected ArrayList visitedCongestionCompany = new ArrayList();

        // Indicating whether a congestion charge has already been applied 
        // to the complete journey 
        protected bool congestionChargeAdded = false;

        #endregion

        #region Journey instruction variables

        // Used to control rendering of the step number
        protected int stepNumber;

        // Keeps the accumulated distance of the journey currently being rendered
        protected int accumulatedDistance;

        // Keeps the accumulated arrival time (updated after each leg)
        protected TDDateTime currentArrivalTime;

        #region Temp values needed only for the duration of one detail

        // Keeps the current distance of the journey detail
        protected int currentDetailDistance;

        // Used to hold any cost/charge value associated with the journey detail
        protected double detailCost;

        // Used to hold the company name if it applies to the journey detail
        protected string companyName;

        // Used to hold the company url if it applies to the journey detail
        protected string companyUrl;

        #endregion

        #endregion

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CarJourneyDetailFormatter(RoadJourney roadJourney, bool outward, bool sameDayReturn, bool congestionChargeAdded, ArrayList visitedCongestionCompany, DistanceUnit distanceUnit, TDResourceManager rm )
        {
            this.roadJourney = roadJourney;
            this.outward = outward;
            this.sameDayReturn = sameDayReturn;
            this.congestionChargeAdded = congestionChargeAdded;
            this.visitedCongestionCompany = visitedCongestionCompany;
            this.distanceUnit = distanceUnit;
            this.rm = rm;

            InitialiseRouteTextDescriptionStrings();

            LoadProperties();
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

        /// <summary>
        /// The current journey detail distance in miles. The value of the
        /// current distance is calculated during the formatting of
        /// journey instructions in GetDirections and so should be called after this method
        /// </summary>
        protected virtual string CurrentDetailDistance
        {
            get
            {
                return (currentDetailDistance > 0 ? ConvertMetresToMileage(currentDetailDistance) : notApplicable);
            }
        }

        /// <summary>
        /// The accummulated distance of a journey in kms. The value of the
        /// current distance is calculated during the formatting of
        /// journey instructions in GetDirections and so should be called after this method
        /// </summary>
        protected virtual string CurrentDetailKmDistance
        {
            get
            {
                return (currentDetailDistance > 0 ? ConvertMetresToKm(currentDetailDistance) : notApplicable);
            }
        }

        /// <summary>
        /// Returns the cost or charge associated with this journey detail
        /// </summary>
        protected virtual string Cost
        {
            get { return detailCost.ToString(); }
        }
    
        /// <summary>
        /// Returns the company name text associated with this journey detail
        /// </summary>
        protected virtual string CompanyName
        {
            get { return companyName; }
        }

        /// <summary>
        /// Returns the company url text associated with this journey detail
        /// </summary>
        protected virtual string CompanyURL
        {
            get { return companyUrl; }
        }
        
        /// <summary>
        /// Returns if a congestion charge was added for this journey. Should only be read after
        /// GetJourneyDetails() method has been called
        /// </summary>
        public virtual bool CongestionChargeAdded
        {
            get { return congestionChargeAdded; }
        }

        /// <summary>
        /// Returns the list of congestion charge companies added for this journey. Should only be read 
        /// after GetJourneyDetails() method has been called
        /// </summary>
        public virtual ArrayList VisitedCongestionCompany
        {
            get { return visitedCongestionCompany; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns an ordered list of journey details. Each object in the
        /// list contains details for a single journey instruction. Each object
        /// is a string array of details (e.g, road name, distance, directions)
        /// </summary>
        /// <returns>Returns an ordered list of journey instructions where each 
        /// element is a string array of details (e.g, road name, distance, directions)</returns>
        public IList GetJourneyDetails()
        {
            return processRoadJourney();
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Returns an ordered list of journey instructions
        /// </summary>
        protected IList processRoadJourney()
        {
            ArrayList details = new ArrayList();

            #region Determine if congestion charge can be shown for this journey
            
            bool showCongestionCharge = true;

            if (outward)
            {   // Always show congestion charge for outward journey
                showCongestionCharge = true;
            }
            else if ((congestionChargeAdded) && (sameDayReturn))
            {   // Don't show it if its already been added for the journey on the same day
                showCongestionCharge = false;
            }
            else
            {   // All other scenarios, show the charge
                showCongestionCharge = true;
            }

            #endregion

            if ((roadJourney == null) || (roadJourney.Details.Length == 0))
            {
                return details;
            }
            else
            {
                InitFormatting();

                // String array for each detail has following order
                // 0 step number
                // 1 accumulated distance
                // 2 distance
                // 3 instruction text
                // 4 arrival time
                // 5 cost (e.g. toll cost)
                // 6 company name
                // 7 company url

                details.Add(addFirstDetailLine());

                for (int journeyDetailIndex = 0;
                    journeyDetailIndex < roadJourney.Details.Length;
                    journeyDetailIndex++)
                {
                    details.Add(processRoadJourneyDetail(journeyDetailIndex, showCongestionCharge));

                    ResetJourneyDetailValues();
                }

                details.Add(addLastDetailLine());

                return details;
            }
        }

        /// <summary>
        /// Initialises instance variables used during the formatting process.
        /// </summary>
        protected void InitFormatting()
        {
            stepNumber = 1;
            accumulatedDistance = 0;
            currentArrivalTime = roadJourney.DepartDateTime;

            ResetJourneyDetailValues();
        }

        /// <summary>
        /// Method which resets any temporary values set for each journey detail
        /// </summary>
        protected void ResetJourneyDetailValues()
        {
            currentDetailDistance = 0;
            detailCost = -1;
            companyName = string.Empty;
            companyUrl = string.Empty;
        }

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
        /// Method to process each road journey
        /// instruction. The returned string array should contain details for each 
        /// instruction (e.g, road name, distance, directions)
        /// </summary>
        /// <param name="journeyDetailIndex">Array index into roadJourney.Details</param>
        /// <returns>details for each instruction (e.g, road name, distance, directions)</returns>
        protected object[] processRoadJourneyDetail(int journeyDetailIndex, bool showCongestionCharge)
        {
            object[] details = new object[8];

            // Note that the Accumulated Distance is updated in GetDirections, therefore the TotalDistance
            // displayed is the total prior to the current instruction rather than after it	
            RoadJourneyDetail roaddetail = roadJourney.Details[journeyDetailIndex];
            RoadJourneyDetail previousroaddetail;

            if (journeyDetailIndex > 0)
            {
                previousroaddetail = roadJourney.Details[journeyDetailIndex - 1];
            }
            else
            {
                previousroaddetail = roadJourney.Details[journeyDetailIndex];
            }

            details[0] = GetCurrentStepNumber();

            // accumulated distance
            if (roaddetail.IsStopOver)
            {
                details[1] = "-";
            }
            else
            {
                details[1] = (distanceUnit == DistanceUnit.Miles) ? TotalDistance : TotalKmDistance;
            }

            //is the journey a return and was congestion charge shown for the outward already?
            details[3] = GetDirections(journeyDetailIndex, showCongestionCharge);
            details[4] = GetArrivalTime(journeyDetailIndex);
            
            //current distance
            if (roaddetail.IsStopOver)
            {
                details[2] = "-";
            }
            else
            {
                details[2] = (distanceUnit == DistanceUnit.Miles) ? CurrentDetailDistance : CurrentDetailKmDistance;
            }

            details[5] = Cost;
            details[6] = CompanyName;
            details[7] = CompanyURL;

            return details;
        }

        /// <summary>
        /// Method to add the first element to the 
        /// ordered list of journey instructions. The returned string array
        /// should contain details for the first instruction.
        /// </summary>
        /// <returns>details for first instruction</returns>
        protected object[] addFirstDetailLine()
        {
            StringBuilder stringDetails = new StringBuilder();
            object[] details = new object[8];

            //step number
            details[0] = GetCurrentStepNumber();


            //accumulated distance
            details[1] = string.Empty;
            details[2] = string.Empty;

            //instruction
            details[3] = leaveFrom + " " + roadJourney.OriginLocation.Description;
            
            //arrival time
            details[4] = string.Empty;
            
            //cost, company name, and url
            details[5] = string.Empty;
            details[6] = string.Empty;
            details[7] = string.Empty;

            return details;
        }

        /// <summary>
        /// Method to add the last element to the 
        /// ordered list of journey instructions. The returned string array
        /// should contain details for the last instruction.
        /// </summary>
        /// <returns>details for last instruction</returns>
        protected object[] addLastDetailLine()
        {
            StringBuilder stringDetails = new StringBuilder();
            object[] details = new object[8];

            int journeyDetailIndex = roadJourney.Details.Length - 1;

            //step number
            details[0] = GetCurrentStepNumber();

            //accumulated distance
            details[1] = (distanceUnit == DistanceUnit.Miles) ? TotalDistance : TotalKmDistance;
            details[2] = string.Empty;
            
            //instruction
            details[3] = arriveAt + roadJourney.DestinationLocation.Description;

            //arrival time
            details[4] = GetArrivalTime(journeyDetailIndex);

            details[5] = string.Empty;
            details[6] = string.Empty;
            details[7] = string.Empty;

            return details;			
        }

        #endregion

        #region Protected Virtual methods

        #region Get text methods

        /// <summary>
        /// Returns a formatted string of the arrival time for the current
        /// driving instruction in the format defined by the method FormatTime.
        /// The time is calculated by adding the current journey
        /// instruction duration to an accumulated journey time.
        /// </summary>
        /// <param name="detail">Array index into roadJourney.Details for journey instruction</param>
        /// <returns>Formatted arrival time</returns>
        protected virtual string GetArrivalTime(int journeyDetailIndex)
        {
            RoadJourneyDetail detail = roadJourney.Details[journeyDetailIndex];
            //If it is a Stopover (unless a wait) We don't want to add the time

            if (detail.ferryEntry)
            {
                TimeSpan span = new TimeSpan(0, 0, (int)detail.Duration);
                TDDateTime arrivalTime = currentArrivalTime.Subtract(span);
                return FormatTime(arrivalTime);
            }

            else if (detail.IsStopOver && !detail.wait)
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

        /// <summary>
		/// Returns a formatted string of the directions for the current
		/// driving instruction e.g. "Continue on to FOXHUNT GROVE".
		/// </summary>
		/// <param name="detail">Array index into roadJourney.Details for journey instruction</param>
		/// <returns>Formatted string of the directions</returns>
        protected virtual string GetDirections(int journeyDetailIndex, bool showCongestionCharge)
        {
            //temp variables for this method
            string routeText = String.Empty;

            //assign the detail to be formatted as an instruction
            RoadJourneyDetail detail = roadJourney.Details[journeyDetailIndex];
            //last Detail, which is also needed for formatting logic			
            RoadJourneyDetail lastDetail = roadJourney.Details[roadJourney.Details.Length - 1];

            bool nextDetailHasJunctionExitJunction = false;

            if (detail != lastDetail)
            {
                RoadJourneyDetail nextDetail = detail;
                nextDetail = roadJourney.Details[journeyDetailIndex + 1];
                nextDetailHasJunctionExitJunction = ((nextDetail.IsJunctionSection) &&
                    ((nextDetail.JunctionAction == cjpinterface.JunctionType.Exit) ||
                    (nextDetail.JunctionAction == cjpinterface.JunctionType.Merge)));
            }
            
            //Returns specific text depending on the StopoverSection
            if (detail.IsStopOver)
            {
                #region Stopover

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
                if (detail.FerryCheckIn != null)
                {
                    //We need to pass the previous detail so we can find out if it was 
                    //an UndefinedWait or not (as this changes the instruction passed to the user)
                    RoadJourneyDetail previousDetail = roadJourney.Details[journeyDetailIndex - 1];
                    routeText = FerryEntry(detail, previousDetail);
                }
                if (detail.TollEntry != null)
                {
                    routeText = TollEntry(detail);
                }
                if (detail.TollExit != null)
                {
                    routeText = TollExit(detail);
                }
                if (detail.viaLocation == true)
                {
                    routeText = ViaLocation();
                }
                if (detail.FerryCheckOut == true)
                {
                    routeText = FerryExit(detail);
                }
                if (detail.wait == true)
                {
                    RoadJourneyDetail previousDetail = roadJourney.Details[journeyDetailIndex - 1];
                    RoadJourneyDetail nextDetail = roadJourney.Details[journeyDetailIndex + 1];
                    routeText = WaitForFerry(detail, previousDetail, nextDetail);
                }
                if (detail.undefindedWait == true)
                {
                    RoadJourneyDetail previousDetail = roadJourney.Details[journeyDetailIndex - 1];
                    RoadJourneyDetail nextDetail = roadJourney.Details[journeyDetailIndex + 1];
                    routeText = UndefindedFerryWait(detail, previousDetail, nextDetail);
                }

                #endregion
            }
            else
            {
                //check turns and format accordingly			
                if (detail.Roundabout)
                    routeText = Roundabout(detail);
                else if (detail.ThroughRoute)
                    routeText = ThroughRoute(detail);
                else
                {
                    // Not a roundabout or a through route.  
                    if (detail.Angle == cjpinterface.TurnAngle.Continue)
                        routeText = TurnAngleContinue(journeyDetailIndex);
                    else if (detail.Angle == cjpinterface.TurnAngle.Bear)
                        routeText = TurnAngleBear(journeyDetailIndex);
                    else if (detail.Angle == cjpinterface.TurnAngle.Turn)
                        routeText = TurnAngleTurn(journeyDetailIndex);
                }

                //check if the current road journey detail is a motorway junction section
                if (detail.IsJunctionSection)
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
                    if ((detail.JunctionAction == cjpinterface.JunctionType.Entry) && detail.IsJunctionSection &&
                        detail.Roundabout && (detail.Distance < immediateTurnDistance) &&
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

                if (detail.IsJunctionSection &&
                    ((detail.JunctionAction == cjpinterface.JunctionType.Exit) ||
                    (detail.JunctionAction == cjpinterface.JunctionType.Merge)) &&
                    (detail.Distance > immediateTurnDistance))
                {
                    //don't add continue for
                }
                else
                {
                    //Add 'continue for' 
                    routeText = AddContinueFor(detail, nextDetailHasJunctionExitJunction, routeText);
                }

                routeText = AddLimitedAccessText(detail, routeText);

                //Add formatting place holder for where the road splits
                routeText += "{2}";

                //check the next Detail providing we are not on the last detail already 

                if (detail != lastDetail)
                {
                    //next Detail, which is needed for formatting logic	
                    RoadJourneyDetail nextDetail = roadJourney.Details[journeyDetailIndex + 1];

                    if (!((detail.JunctionAction == cjpinterface.JunctionType.Exit) ||
                        (detail.JunctionAction == cjpinterface.JunctionType.Merge)))
                    {
                        //amend the current instruction if necessary
                        routeText = CheckNextDetail(nextDetail, routeText);
                    }
                }
                //Add formatting place holder for where the road splits
                routeText += "{3}";

                if (detail.CongestionLevel == true)
                {
                    routeText = AddCongestionSymbol(routeText);
                }

                // Add the distance to the running count
                accumulatedDistance += detail.Distance;

                // Update the current detail distance
                currentDetailDistance = detail.Distance;

                
                //Add where the road splits to the appropriate place


                //Fille in the place holders for where the road splits for Motorway Entry
                if ((detail.JunctionAction == cjpinterface.JunctionType.Entry) &&
                    (detail.IsJunctionSection) && (detail.RoadSplits))
                {
                    if (detail.Roundabout)
                    {
                        //after place name
                        if (placeNameExists && (detail.Distance > immediateTurnDistance))
                        {
                            routeText = String.Format(routeText,
                                String.Empty, space + whereRoadSplits, String.Empty, String.Empty);
                        }
                        //after jno2
                        else if ((!placeNameExists) && (detail.Distance < immediateTurnDistance) &&
                            nextDetailHasJunctionExitJunction)
                        {
                            routeText = String.Format(routeText,
                                String.Empty, String.Empty, String.Empty, space + whereRoadSplits);
                        }
                        else routeText = String.Format(routeText, space + whereRoadSplits, String.Empty,
                                 String.Empty, String.Empty);
                    }
                    else
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
                }
                else routeText = String.Format(routeText, String.Empty,
                         String.Empty, String.Empty, String.Empty);
            }

            //return complete formatted instruction
            return routeText;
        }

        #endregion

        #region Text helpers

        #region Stopover sections

        /// <summary>
        /// Generates the route text for the given RoadJourneyDetail where
        /// the RoadJourneyDetail is a StopoverSection of type 'CongestionEntry'.
        /// </summary>
        /// <param name="detail">Road Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string CongestionEntry(RoadJourneyDetail detail, bool showCongestionCharge)
        {
            StringBuilder routeText = new StringBuilder(string.Empty);
            decimal pence = (detail.TollCost >= 0) ? (Convert.ToDecimal(detail.TollCost)) / 100 : detail.TollCost;
            string cost = string.Format("{0:C}", pence);

            //update the detail cost
            detailCost = Convert.ToDouble(pence);

            //update company name and url if it exists
            if (!string.IsNullOrEmpty(detail.CompanyUrl))
            {
                companyUrl = detail.CompanyUrl.Trim();
                companyName = detail.CongestionZoneEntry;
            }
            
            routeText.Append(enter);
            routeText.Append(space);
            routeText.Append(detail.CongestionZoneEntry);
            
            // If there is a toll charge
            // Check if we already have this C Charge in or 
            // there is a cost and showCongestionCharge
            if ((detail.TollCost > 0) && ((showCongestionCharge) ||
                (!visitedCongestionCompany.Contains(detail.CompanyUrl))))
            {
                //Contains this C Charge and we don't want to show it
                if (visitedCongestionCompany.Contains(detail.CompanyUrl) && (!showCongestionCharge))
                {
                    // No toll charge for this day/time
                    routeText.Append(fullstop);
                    routeText.Append(space);
                    routeText.Append(charge);
                    routeText.Append("£0.00");
                    routeText.Append(space);
                    routeText.Append(certainTimesNoCharge);

                    //update the detail cost
                    detailCost = Convert.ToDouble(0);
                }
                else //We want to show it
                {
                    visitedCongestionCompany.Add(detail.CompanyUrl);
                    routeText.Append(fullstop);
                    routeText.Append(space);
                    routeText.Append(charge);
                    routeText.Append(cost);
                    routeText.Append(space);
                    routeText.Append(certainTimes);

                    if (outward)
                        congestionChargeAdded = true;
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

                //update the detail cost
                detailCost = Convert.ToDouble(0);
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
        /// Generates the route text for the given RoadJourneyDetail where
        /// the RoadJourneyDetail is a StopoverSection of type 'CongestionExit'.
        /// </summary>
        /// <param name="detail">Road Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string CongestionExit(RoadJourneyDetail detail, bool showCongestionCharge)
        {
            StringBuilder routeText = new StringBuilder(string.Empty);
            decimal pence = (detail.TollCost >= 0) ? (Convert.ToDecimal(detail.TollCost)) / 100 : detail.TollCost;
            string cost = string.Format("{0:C}", pence);

            //update the detail cost
            detailCost = Convert.ToDouble(pence);

            //update company name and url if it exists
            if (!string.IsNullOrEmpty(detail.CompanyUrl))
            {
                companyUrl = detail.CompanyUrl.Trim();
                companyName = detail.CongestionZoneExit;
            }

            //add "Exit" to start of instruction						
            routeText.Append(exit);
            routeText.Append(space);
            routeText.Append(detail.CongestionZoneExit);
            
            // If there is a toll charge
            // Check if we already have this C Charge or 
            // there is a cost and showCongestionCharge
            if ((detail.TollCost > 0) && ((showCongestionCharge) ||
                (!visitedCongestionCompany.Contains(detail.CompanyUrl))))
            {
                //Contains this C Charge and we don't want to show it
                if (visitedCongestionCompany.Contains(detail.CompanyUrl) && (!showCongestionCharge))
                {
                    // No toll charge for this day/time
                    routeText.Append(fullstop);
                    routeText.Append(space);
                    routeText.Append(charge);
                    routeText.Append("£0.00");
                    routeText.Append(space);
                    routeText.Append(certainTimesNoCharge);

                    //update the detail cost
                    detailCost = Convert.ToDouble(0);
                }
                else //We want to show it
                {
                    visitedCongestionCompany.Add(detail.CompanyUrl);
                    routeText.Append(fullstop);
                    routeText.Append(space);
                    routeText.Append(charge);
                    routeText.Append(cost);
                    routeText.Append(space);
                    routeText.Append(certainTimes);

                    if (outward)
                        congestionChargeAdded = true;
                }
            }
            else if ((detail.TollCost == 0) || (!showCongestionCharge))
            {
                // No toll charge, so dont want to display the charge text

                //update the detail cost
                detailCost = Convert.ToDouble(0);
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
        /// Generates the route text for the given RoadJourneyDetail where
        /// the RoadJourneyDetail is a StopoverSection of type 'CongestionEnd'.
        /// </summary>
        /// <param name="detail">Road Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string CongestionEnd(RoadJourneyDetail detail, bool showCongestionCharge)
        {
            StringBuilder routeText = new StringBuilder(string.Empty);
            decimal pence = (detail.TollCost >= 0) ? (Convert.ToDecimal(detail.TollCost)) / 100 : detail.TollCost;
            string cost = string.Format("{0:C}", pence);

            //update the detail cost
            detailCost = Convert.ToDouble(pence);

            //update company name and url if it exists
            if (!string.IsNullOrEmpty(detail.CompanyUrl))
            {
                companyUrl = detail.CompanyUrl.Trim();
                companyName = detail.CongestionZoneEnd;
            }

            //add "End" to start of instruction						
            routeText.Append(end);
            routeText.Append(space);
            routeText.Append(detail.CongestionZoneEnd);
            
            // If there is a toll charge
            // Check if we already have this C Charge or 
            // there is a cost and showCongestionCharge
            if ((detail.TollCost > 0) && ((showCongestionCharge) ||
                (!visitedCongestionCompany.Contains(detail.CompanyUrl))))
            {
                //Contains this C Charge and we don't want to show it
                if (visitedCongestionCompany.Contains(detail.CompanyUrl) && (!showCongestionCharge))
                {
                    // No toll charge for this day/time
                    routeText.Append(fullstop);
                    routeText.Append(space);
                    routeText.Append(charge);
                    routeText.Append("£0.00");
                    routeText.Append(space);
                    routeText.Append(certainTimesNoCharge);

                    //update the detail cost
                    detailCost = Convert.ToDouble(0);
                }
                else //We want to show it
                {
                    visitedCongestionCompany.Add(detail.CompanyUrl);
                    routeText.Append(fullstop);
                    routeText.Append(space);
                    routeText.Append(charge);
                    routeText.Append(cost);
                    routeText.Append(space);
                    routeText.Append(certainTimes);

                    if (outward)
                        congestionChargeAdded = true;
                }
            }
            else if ((detail.TollCost == 0) || (!showCongestionCharge))
            {
                // No toll charge, so dont want to display the charge text

                //update the detail cost
                detailCost = Convert.ToDouble(0);
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
        /// Generates the route text for the given RoadJourneyDetail where
        /// the RoadJourneyDetail is a StopoverSection of type 'FerryEntry'.
        /// </summary>
        /// <param name="detail">Road Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string FerryEntry(RoadJourneyDetail detail, RoadJourneyDetail PreviousDetail)
        {
            bool previousInstruction = PreviousDetail.undefindedWait;

            StringBuilder routeText = new StringBuilder(string.Empty);
            decimal pence = (detail.TollCost >= 0) ? (Convert.ToDecimal(detail.TollCost)) / 100 : detail.TollCost;
            string cost = string.Format("{0:C}", pence);

            //update the detail cost
            detailCost = Convert.ToDouble(pence);

            TimeSpan span = new TimeSpan(0, 0, (int)detail.Duration);
            TDDateTime arrivalTime = currentArrivalTime;
            currentArrivalTime = currentArrivalTime.Add(span);
            string time = FormatDateTime(currentArrivalTime);

            //add "Board:" to start of instruction			
            //If previous instruction was an undefinedWait we don't want to display a time(as we don't know it)
            if (previousInstruction)
            {
                //update company name and url if it exists
                if (!string.IsNullOrEmpty(detail.CompanyUrl))
                {
                    companyUrl = detail.CompanyUrl.Trim();
                    companyName = detail.FerryCheckIn;
                }

                routeText.Append(board);
                routeText.Append(space);
                routeText.Append(detail.FerryCheckIn);

            }
            else
            {
                //update company name and url if it exists
                if (!string.IsNullOrEmpty(detail.CompanyUrl))
                {
                    companyUrl = detail.CompanyUrl.Trim();
                    companyName = detail.FerryCheckIn;
                }

                routeText.Append(board);
                routeText.Append(space);
                routeText.Append(detail.FerryCheckIn);
                routeText.Append(space);
                routeText.Append(departingAt);
                routeText.Append(space);
                routeText.Append(time);

            }

            //If price in pence is less than zero return the route text, otherwise
            //return route text inclusive of the charge.
            if (pence < 0)
            {
                //Added in message when charge is unknown "Charge: Not Available"
                routeText.Append(";");
                routeText.Append(space);
                routeText.Append(charge);
                routeText.Append(notAvailable);
            }
            else if (pence == 0)
            {
                //Added in message when charge is unknown "Charge: Not Available"
                routeText.Append(";");
                routeText.Append(space);
                routeText.Append(charge);
                routeText.Append("£0.00");
            }
            else
            {
                routeText.Append(";");
                routeText.Append(space);
                routeText.Append(charge);
                routeText.Append(cost);
            }

            return routeText.ToString();
        }

        /// <summary>
        /// Generates the route text for the given RoadJourneyDetail where
        /// the RoadJourneyDetail is a StopoverSection of type 'FerryExit'.
        /// </summary>
        /// <param name="detail">Road Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string FerryExit(RoadJourneyDetail detail)
        {
            TimeSpan span = new TimeSpan(0, 0, (int)detail.Duration);
            currentArrivalTime = currentArrivalTime.Add(span);

            string routeText = String.Empty;

            routeText = leaveFerry;

            return routeText;
        }
        
        /// <summary>
        /// Generates the route text for the given RoadJourneyDetail where
        /// the RoadJourneyDetail is a StopoverSection of type 'TollEntry'.
        /// </summary>
        /// <param name="detail">Road Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string TollEntry(RoadJourneyDetail detail)
        {
            StringBuilder routeText = new StringBuilder(string.Empty);

            //Convert TollCost value to pounds & pence
            decimal pence = (detail.TollCost >= 0) ? (Convert.ToDecimal(detail.TollCost)) / 100 : detail.TollCost;
            string cost = string.Format("{0:C}", pence);

            //update the detail cost
            detailCost = Convert.ToDouble(pence);

            //update company name and url if it exists
            if (!string.IsNullOrEmpty(detail.CompanyUrl))
            {
                companyUrl = detail.CompanyUrl.Trim();
                companyName = detail.TollEntry;
            }

            //add "Toll:" to start of instruction
            routeText.Append(toll);
            routeText.Append(space);
            routeText.Append(detail.TollEntry);

            //If price in pence is less than zero return the route text, otherwise
            //return route text inclusive of the charge.
            if (pence < 0)
            {
                //Added in message when charge is unknown "Charge: Not Available"
                routeText.Append(space);
                routeText.Append(charge);
                routeText.Append(notAvailable);
            }
            else if (pence == 0)
            {
                routeText.Append(space);
                routeText.Append(charge);
                routeText.Append("£0.00");
            }
            else
            {
                routeText.Append(space);
                routeText.Append(charge);
                routeText.Append(cost);
            }

            return routeText.ToString();
        }

        /// <summary>
        /// Generates the route text for the given RoadJourneyDetail where
        /// the RoadJourneyDetail is a StopoverSection of type 'TollEntry'.
        /// </summary>
        /// <param name="detail">Road Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string TollExit(RoadJourneyDetail detail)
        {
            StringBuilder routeText = new StringBuilder(string.Empty);

            //Convert TollCost value to pounds & pence
            decimal pence = (detail.TollCost >= 0) ? (Convert.ToDecimal(detail.TollCost)) / 100 : detail.TollCost;
            string cost = string.Format("{0:C}", pence);

            //update the detail cost
            detailCost = Convert.ToDouble(pence);

            //update company name and url if it exists
            if (!string.IsNullOrEmpty(detail.CompanyUrl))
            {
                companyUrl = detail.CompanyUrl.Trim();
                companyName = detail.TollExit;
            }

            //add "Toll:" to start of instruction	
            routeText.Append(toll);
            routeText.Append(space);
            routeText.Append(detail.TollExit);
            
            routeText.Append(space);
            routeText.Append(charge);

            return routeText.ToString();
        }

        /// <summary>
        /// Generates the route text for the given RoadJourneyDetail where
        /// the RoadJourneyDetail is a StopoverSection of type 'ViaLocation'.
        /// </summary>
        /// <param name="detail">Road Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string ViaLocation()
        {
            string routeText = String.Empty;

            //add "Arrive at" to start of instruction			
            routeText = viaArriveAt + space + roadJourney.RequestedViaLocation.Description;
            
            return routeText;
        }

        /// <summary>
        /// Generates the route text for the given RoadJourneyDetail where
        /// the RoadJourneyDetail is a StopoverSection of type 'Wait'.
        /// </summary>
        /// <param name="detail">Road Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string WaitForFerry(RoadJourneyDetail detail, RoadJourneyDetail previousDetail, RoadJourneyDetail nextDetail)
        {
            string routeText = String.Empty;

            if (nextDetail.RoadNumber == "FERRY" && previousDetail.RoadNumber == "FERRY")
            {
                routeText = IntermediateFerryWait;
            }
            else
            {
                if (previousDetail.FerryCheckOut)
                {
                    routeText = WaitAtTerminal;
                }
                else
                {
                    routeText = FerryWait;
                }
            }
            return routeText;
        }
        
        /// <summary>
        /// Generates the route text for the given RoadJourneyDetail where
        /// the RoadJourneyDetail is a StopoverSection of type 'UndefindedWait'.
        /// </summary>
        /// <param name="detail">Road Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string UndefindedFerryWait(RoadJourneyDetail detail, RoadJourneyDetail previousDetail, RoadJourneyDetail nextDetail)
        {
            string routeText = String.Empty;

            if (nextDetail.RoadNumber == "FERRY" && previousDetail.RoadNumber == "FERRY")
            {
                routeText = IntermediateFerryWait;
            }
            else
            {
                if (previousDetail.FerryCheckOut)
                {
                    routeText = WaitAtTerminal;
                }
                else
                {
                    routeText = UnspecifedFerryWait;
                }
            }
            return routeText;
        }

        #endregion

        /// <summary>
        /// Generates the route text for the given RoadJourney where
        /// the RoadJourney type is "Roundabout". This method assumes
        /// that the type is "Roundabout".
        /// </summary>
        /// <param name="detail">Road Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string Roundabout(RoadJourneyDetail detail)
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

            if (detail.RoadSplits && !detail.IsJunctionSection)
            {
                routeText += space + whereRoadSplits;
            }

            return routeText;
        }
        
        /// <summary>
        /// Generates the route text for the given RoadJourneyDetail where
        /// the RoadJourneyDetail type is "ThroughRoute". This method assumes
        /// that the type is "ThroughRoute".
        /// </summary>
        /// <param name="detail">Road Journey Detail to generate route text for.</param>
        /// <returns>Route text.</returns>
        protected virtual string ThroughRoute(RoadJourneyDetail detail)
        {
            string roadName = FormatRoadName(detail);
            string routeText = String.Empty;

            //add "Follow..." to start of instruction			
            routeText = follow + space + roadName;

            if (detail.RoadSplits)
            {
                routeText += space + whereRoadSplits;
            }
            return routeText;
        }

        /// <summary>
        /// Generate the text where the turn angle of the given
        /// RoadJourneyDetail is "Continue". This method assumes that
        /// the turn angle for the given detail is "Continue".
        /// </summary>
        /// <param name="journeyDetailIndex">Array index into roadJourney.Details</param>
        /// <returns>Route text.</returns>
        protected virtual string TurnAngleContinue(int journeyDetailIndex)
        {
            RoadJourneyDetail detail = roadJourney.Details[journeyDetailIndex];

            string nextRoad = FormatRoadName(detail);
            string routeText = String.Empty;
            bool addedWhereRoadSplits = false;


            //When the CJP produces a JunctionDriveSection of type Merge (or Exit) 
            //without a junction number (regardless of whether the junction is indicated as ambiguous) 
            //for a Motorway junction (including slip roads) then ignore the turn count 
            //and the immediate parameter. 			
            if (detail.JunctionDriveSectionWithoutJunctionNo &&
                (detail.JunctionAction == cjpinterface.JunctionType.Exit || detail.JunctionAction == cjpinterface.JunctionType.Merge))
            {
                string straightOnInstruction =
                    ((detail.PlaceName == null || detail.PlaceName.Length == 0) &&
                    (detail.Distance > immediateTurnDistance) && detail.RoadSplits) ?
                        String.Empty : space + straightOn;

                if (detail.Direction == cjpinterface.TurnDirection.Continue)
                    routeText = follow + space + nextRoad + straightOnInstruction;
                else if (detail.Direction == cjpinterface.TurnDirection.Right)
                    routeText = follow + space + nextRoad + straightOnInstruction;
                else if (detail.Direction == cjpinterface.TurnDirection.Left)
                    routeText = follow + space + nextRoad + straightOnInstruction;
            }
            else if (!detail.IsJunctionSection)
            {
                if ((detail.Direction == cjpinterface.TurnDirection.Continue) ||
                    (detail.Direction == cjpinterface.TurnDirection.Left) ||
                    (detail.Direction == cjpinterface.TurnDirection.Right))
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
                else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutContinue)
                {
                    routeText = atMiniRoundabout + follow.ToLower(TDCultureInfo.CurrentCulture) +
                        space + nextRoad + space + straightOn;
                }
                else
                {
                    // Special case if no place name, < immediate distance and road splits
                    if ((detail.PlaceName == null || detail.PlaceName.Length == 0) &&
                        (detail.Distance < immediateTurnDistance) && detail.RoadSplits)
                    {
                        if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutLeft)
                        {
                            routeText = leftMiniRoundabout2 + space + whereRoadSplits + space + onTo + space + nextRoad;
                        }
                        else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutRight)
                        {
                            routeText = rightMiniRoundabout2 + space + whereRoadSplits + space + onTo + space + nextRoad;
                        }
                        else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutReturn)
                        {
                            routeText = uTurnMiniRoundabout2 + space + whereRoadSplits + space + onTo + space + nextRoad;
                        }
                        // Set flag so dont add "where road splits" again at end of string
                        addedWhereRoadSplits = true;
                    }
                    else
                    {
                        if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutLeft)
                        {
                            routeText = leftMiniRoundabout + space + nextRoad;
                        }
                        else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutRight)
                        {
                            routeText =
                                rightMiniRoundabout + space + nextRoad;
                        }
                        else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutReturn)
                        {
                            routeText =
                                uTurnMiniRoundabout + space + nextRoad;
                        }

                    }
                }
            }
            else if (detail.IsJunctionSection)
            {
                //Motorway entry 
                if ((detail.JunctionAction == cjpinterface.JunctionType.Entry) &&
                    (detail.JunctionNumber != null) && (detail.JunctionNumber.Length > 0))
                {
                    routeText = join + space + nextRoad;
                    return routeText; //without where the road splits
                }

                if (detail.Direction == cjpinterface.TurnDirection.Continue)
                {
                    routeText = (detail.RoadSplits) ? follow + space + nextRoad + space + straightOn :
                        join + space + nextRoad;
                }
                else if (detail.Direction == cjpinterface.TurnDirection.Right)
                {
                    routeText = (detail.RoadSplits) ? follow + space + nextRoad + space + straightOn :
                        join + space + nextRoad;
                }
                else if (detail.Direction == cjpinterface.TurnDirection.Left)
                {
                    routeText = (detail.RoadSplits) ? follow + space + nextRoad + space + straightOn :
                        join + space + nextRoad;
                }
                else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutContinue)
                {
                    routeText = (detail.RoadSplits) ?
                        atMiniRoundabout + follow.ToLower(TDCultureInfo.CurrentCulture) +
                        space + nextRoad + space + straightOn :
                        continueMiniRoundabout + space + nextRoad;
                }
                else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutLeft)
                {
                    routeText = leftMiniRoundabout + space + nextRoad;
                }
                else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutRight)
                {
                    routeText = rightMiniRoundabout + space + nextRoad;
                }
                else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutReturn)
                {
                    routeText = uTurnMiniRoundabout + space + nextRoad;
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
        /// Generates the route text for the given RoadJourney where
        /// the TurnAngle for the given RoadJourney is "Bear".  This method
        /// assumes that the TurnAngle is "Bear".
        ///
        /// Note that for "counted turns" (TurnCount 1 - 4) the text is actually
        /// "Take the ..." which can be used for both "bear" and "turn".
        /// </summary>
        /// <param name="journeyDetailIndex">Array index into roadJourney.Details</param>
        /// <returns>Route text.</returns>
        protected virtual string TurnAngleBear(int journeyDetailIndex)
        {
            RoadJourneyDetail detail = roadJourney.Details[journeyDetailIndex];

            int previousDistance = 0;
            if (journeyDetailIndex > 0)
            {
                previousDistance = roadJourney.Details[journeyDetailIndex - 1].Distance;
            };

            string nextRoad = FormatRoadName(detail);
            string routeText = String.Empty;

            //When the CJP produces a JunctionDriveSection of type Merge (or Exit) 
            //without a junction number (regardless of whether the junction is indicated as ambiguous) 
            //for a Motorway junction (including slip roads) then ignore the turn count 
            //and the immediate parameter. 
            if (detail.JunctionDriveSectionWithoutJunctionNo &&
                (detail.JunctionAction == cjpinterface.JunctionType.Exit || detail.JunctionAction == cjpinterface.JunctionType.Merge))
            {
                if (detail.Direction == cjpinterface.TurnDirection.Continue)
                {
                    string straightOnInstruction = space + straightOn;

                    if (detail.Direction == cjpinterface.TurnDirection.Continue)
                        routeText = follow + space + nextRoad + straightOnInstruction;
                    else if (detail.Direction == cjpinterface.TurnDirection.Right)
                        routeText = follow + space + nextRoad + straightOnInstruction;
                    else if (detail.Direction == cjpinterface.TurnDirection.Left)
                        routeText = follow + space + nextRoad + straightOnInstruction;
                }
                else
                {
                    if (detail.Direction == cjpinterface.TurnDirection.Left)
                        routeText = bearLeft + space + nextRoad;
                    else if (detail.Direction == cjpinterface.TurnDirection.Right)
                        routeText = bearRight + space + nextRoad;
                }

                if (detail.RoadSplits)
                    routeText += space + whereRoadSplits;

                return routeText;
            }

            // Check to see if this detail is a junction drive section
            if (detail.IsJunctionSection)
            {
                if (detail.Direction == cjpinterface.TurnDirection.Left)
                {
                    //Motorway entry 
                    if ((detail.JunctionAction == cjpinterface.JunctionType.Entry) &&
                        (detail.JunctionNumber != null) && (detail.JunctionNumber.Length > 0))
                    {
                        routeText = bearLeftToJoin;
                    }

                }
                else if (detail.Direction == cjpinterface.TurnDirection.Right)
                {
                    //Motorway entry 
                    if ((detail.JunctionAction == cjpinterface.JunctionType.Entry) &&
                        (detail.JunctionNumber != null) && (detail.JunctionNumber.Length > 0))
                    {
                        routeText = bearRightToJoin;
                    }

                }
                else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutLeft)
                    routeText = leftMiniRoundabout;
                else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutRight)
                    routeText = rightMiniRoundabout;
                else if (detail.Direction == cjpinterface.TurnDirection.Continue)
                    //check flag for ambiguous junction
                    routeText = (detail.RoadSplits) ? continueString : join;
                else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutReturn)
                    routeText = uTurnMiniRoundabout;

                // Append the next road to the route text
                routeText += space + nextRoad;
            }
            //this detail is a drive section
            else
            {
                if (detail.Direction == cjpinterface.TurnDirection.Left)
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
                                    routeText = turnLeftOne;
                            }
                            else
                            {
                                routeText = turnLeftOne;
                            }

                            break;

                        case 2: routeText = turnLeftTwo; break;
                        case 3: routeText = turnLeftThree; break;
                        case 4: routeText = turnLeftFour; break;

                        default: routeText = bearLeft; break;
                    }
                }
                else if (detail.Direction == cjpinterface.TurnDirection.Right)
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
                                    routeText = turnRightOne;
                            }
                            else
                            {
                                routeText = turnRightOne;
                            }
                            break;

                        case 2: routeText = turnRightTwo; break;
                        case 3: routeText = turnRightThree; break;
                        case 4: routeText = turnRightFour; break;

                        default: routeText = bearRight; break;
                    }
                }
                else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutContinue)
                    routeText = atMiniRoundabout + ChangeFirstCharacterCapitalisation(follow, false);
                else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutLeft)
                    routeText = leftMiniRoundabout;
                else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutRight)
                    routeText = rightMiniRoundabout;
                else if (detail.Direction == cjpinterface.TurnDirection.Continue)
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
                else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutReturn)
                    routeText = uTurnMiniRoundabout;

                // Append the next road to the route text
                routeText += space + nextRoad;

                if ((detail.Direction == cjpinterface.TurnDirection.Continue) ||
                    (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutContinue))
                    routeText += space + straightOn;

                if (detail.RoadSplits)
                {
                    if ((detail.TurnCount > 4) &&
                        ((detail.Direction == cjpinterface.TurnDirection.Left) || (detail.Direction == cjpinterface.TurnDirection.Right)))
                        routeText = ChangeFirstCharacterCapitalisation(whereRoadSplits, true) + space +
                            ChangeFirstCharacterCapitalisation(routeText, false);
                    else routeText += space + whereRoadSplits;
                }

            }

            return routeText;
        }

        /// <summary>
        /// Generates the route text for the given RoadJourney where
        /// the TurnAngle for the given RoadJourney is "Turn".  This method
        /// assumes that the TurnAngle is "Turn".
        ///
        /// Note that for "counted turns" (TurnCount 1 - 4) the text is actually
        /// "Take the ..." which can be used for both "bear" and "turn".
        /// </summary>
        /// <param name="journeyDetailIndex">Array index into roadJourney.Details</param>
        /// <returns>Route text.</returns>
        protected virtual string TurnAngleTurn(int journeyDetailIndex)
        {

            RoadJourneyDetail detail = roadJourney.Details[journeyDetailIndex];

            int previousDistance = 0;
            if (journeyDetailIndex > 0)
            {
                previousDistance = roadJourney.Details[journeyDetailIndex - 1].Distance;
            };

            string routeText = String.Empty;
            string nextRoad = FormatRoadName(detail);

            //When the CJP produces a JunctionDriveSection of type Merge (or Exit) 
            //without a junction number (regardless of whether the junction is indicated as ambiguous) 
            //for a Motorway junction (including slip roads) then ignore the turn count 
            //and the immediate parameter. 
            if (detail.JunctionDriveSectionWithoutJunctionNo &&
                (detail.JunctionAction == cjpinterface.JunctionType.Exit || detail.JunctionAction == cjpinterface.JunctionType.Merge))
            {
                if (detail.Direction == cjpinterface.TurnDirection.Continue)
                {
                    string straightOnInstruction = space + straightOn;

                    if (detail.Direction == cjpinterface.TurnDirection.Continue)
                        routeText = follow + space + nextRoad + straightOnInstruction;
                    else if (detail.Direction == cjpinterface.TurnDirection.Right)
                        routeText = follow + space + nextRoad + straightOnInstruction;
                    else if (detail.Direction == cjpinterface.TurnDirection.Left)
                        routeText = follow + space + nextRoad + straightOnInstruction;

                    if (detail.RoadSplits)
                        routeText += space + whereRoadSplits;
                }
                else
                {
                    if (detail.Direction == cjpinterface.TurnDirection.Left)
                        routeText = turnLeftInDistance + space + nextRoad;
                    else if (detail.Direction == cjpinterface.TurnDirection.Right)
                        routeText = turnRightInDistance + space + nextRoad;

                    if (detail.RoadSplits)
                        routeText = ChangeFirstCharacterCapitalisation(whereRoadSplits, true) + space +
                            ChangeFirstCharacterCapitalisation(routeText, false);
                }

                return routeText;
            }

            // Check to see if this detail is a junction drive section
            if (detail.IsJunctionSection)
            {
                if (detail.Direction == cjpinterface.TurnDirection.Left)
                {
                    //Motorway entry 
                    if ((detail.JunctionAction == cjpinterface.JunctionType.Entry) &&
                        (detail.JunctionNumber != null) && (detail.JunctionNumber.Length > 0))
                    {
                        routeText = turnLeftToJoin;
                    }

                }
                else if (detail.Direction == cjpinterface.TurnDirection.Right)
                {
                    //Motorway entry 
                    if ((detail.JunctionAction == cjpinterface.JunctionType.Entry) &&
                        (detail.JunctionNumber != null) && (detail.JunctionNumber.Length > 0))
                    {
                        routeText = turnRightToJoin;
                    }
                }
                else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutLeft)
                {
                    routeText = leftMiniRoundabout;
                }
                else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutRight)
                {
                    routeText = rightMiniRoundabout;
                }
                else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutReturn)
                {
                    routeText = uTurnMiniRoundabout;
                }
                else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutContinue)
                {
                    routeText = (detail.RoadSplits) ?
                        atMiniRoundabout + follow.ToLower(TDCultureInfo.CurrentCulture) +
                        space + nextRoad + space + straightOn :
                        continueString;
                }
                else if (detail.Direction == cjpinterface.TurnDirection.Continue)
                {
                    routeText = continueString;
                }

                // Append the next road to the route text.
                routeText += space + nextRoad;
            }
            //this detail is a drive section
            else
            {

                if (detail.Direction == cjpinterface.TurnDirection.Left)
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
                                    routeText = turnLeftOne;
                            }
                            else
                            {
                                routeText = turnLeftOne;
                            }

                            break;

                        case 2: routeText = turnLeftTwo; break;
                        case 3: routeText = turnLeftThree; break;
                        case 4: routeText = turnLeftFour; break;

                        // Greater than 4 - assuming that the turn count is never 0 when
                        // turn angle is Turn.
                        default: routeText = turnLeftInDistance; break;
                    }
                }
                else if (detail.Direction == cjpinterface.TurnDirection.Right)
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
                                    routeText = turnRightOne;
                            }
                            else
                            {
                                routeText = turnRightOne;
                            }

                            break;

                        case 2: routeText = turnRightTwo; break;
                        case 3: routeText = turnRightThree; break;
                        case 4: routeText = turnRightFour; break;

                        // Greater than 4 - assuming that the turn count is never 0 when
                        // turn angle is Turn.
                        default: routeText = turnRightInDistance; break;
                    }
                }
                else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutLeft)
                {
                    routeText = leftMiniRoundabout;
                }
                else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutRight)
                {
                    routeText = rightMiniRoundabout;
                }
                else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutReturn)
                {
                    routeText = uTurnMiniRoundabout;
                }
                else if (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutContinue)
                {
                    routeText = atMiniRoundabout + ChangeFirstCharacterCapitalisation(follow, false);
                }
                else if (detail.Direction == cjpinterface.TurnDirection.Continue)
                {
                    routeText = follow;
                }

                // Append the next road to the route text.
                routeText += space + nextRoad;

                if ((detail.Direction == cjpinterface.TurnDirection.Continue) ||
                    (detail.Direction == cjpinterface.TurnDirection.MiniRoundaboutContinue))
                    routeText += space + straightOn;

                if (detail.RoadSplits)
                {
                    if ((detail.TurnCount > 4) &&
                        ((detail.Direction == cjpinterface.TurnDirection.Left) || (detail.Direction == cjpinterface.TurnDirection.Right)))
                        routeText = ChangeFirstCharacterCapitalisation(whereRoadSplits, true) + space +
                            ChangeFirstCharacterCapitalisation(routeText, false);
                    else routeText += space + whereRoadSplits;
                }
            }

            return routeText;
        }

        /// <summary>
        /// checks if a place name exists and formats the instruction accordingly
        /// </summary>
        /// <param name="detail">the RoadJourneyDetail being formatted </param>
        /// <param name="routeText">the existing instruction text </param>
        /// <returns>updated formatted string of the directions</returns>
        protected virtual string AddPlaceName(RoadJourneyDetail detail, string routeText)
        {
            //add "towards {placename}" to the end of the instruction 
            routeText = routeText + space + towards + " " + detail.PlaceName;

            return routeText;
        }

        /// <summary>
        /// Add continue for text
        /// </summary>
        protected virtual string AddContinueFor(RoadJourneyDetail detail,
            bool nextDetailHasJunctionExitJunction, string routeText)
        {
            //convert metres to miles
            string distance = string.Empty;
            string distanceInKm = ConvertMetresToKm(detail.Distance);
            string distanceInMiles = ConvertMetresToMileage(detail.Distance);

            //Switches the default display of either Miles or Kms depending on roadUnits
            if (distanceUnit == DistanceUnit.Miles)
            {
                distance = distanceInMiles + space + miles;
            }
            else
            {
                distance = distanceInKm + space + "km";
            }
            
            //check if this text should be added
            if (detail.IsFerry)
            {
                //no need to add the "continues for..." message in these situations
                routeText = FerryCrossing;

                return routeText;
            }

            else if (
                (detail.Distance <= immediateTurnDistance) &&

                !((detail.JunctionAction == cjpinterface.JunctionType.Entry) &&
                detail.IsJunctionSection && detail.Roundabout &&
                (detail.PlaceName != null && detail.PlaceName.Length > 0) &&
                nextDetailHasJunctionExitJunction) &&

                !(detail.IsJunctionSection &&
                ((detail.JunctionAction == cjpinterface.JunctionType.Exit) ||
                (detail.JunctionAction == cjpinterface.JunctionType.Merge)))
                )
            {
                //no need to add the "continues for..." message in these situations
                return routeText;

            }

            else if ((detail.IsSlipRoad) && (detail.Distance < slipRoadDistance) &&

           !((detail.JunctionAction == cjpinterface.JunctionType.Entry) &&
                detail.IsJunctionSection && detail.Roundabout &&
                (detail.PlaceName != null && detail.PlaceName.Length > 0) &&
                nextDetailHasJunctionExitJunction) &&

                !(detail.IsJunctionSection &&
                ((detail.JunctionAction == cjpinterface.JunctionType.Exit) ||
                (detail.JunctionAction == cjpinterface.JunctionType.Merge)) &&
                ((detail.JunctionNumber != null) && (detail.JunctionNumber.Length > 0))))
            {
                //no need to add the "continues for..." message in these situations
                return routeText;
            }
            else
            {
                //in all other cases add "continues for..." to the end of the instruction 
                routeText = routeText + continueFor + space + distance;
            }

            return routeText;
        }

        /// <summary>
        /// Add limited access text
        /// </summary>
        protected virtual string AddLimitedAccessText(RoadJourneyDetail detail, string routeText)
        {
            if (!string.IsNullOrEmpty(detail.LimitedAccessText))
            {
                routeText += fullstop + space + detail.LimitedAccessText;
            }
            return routeText;
        }


        /// <summary>
        /// Checks the subsequent RoadJourneyDetail
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="routeText"></param>
        /// <returns></returns>
        protected virtual string CheckNextDetail(RoadJourneyDetail nextDetail, string routeText)
        {
            //add "until junction number {no}" to end of current instruction
            if ((nextDetail.IsJunctionSection) && ((nextDetail.JunctionAction == cjpinterface.JunctionType.Exit) 
                || (nextDetail.JunctionAction == cjpinterface.JunctionType.Merge)))
            {
                routeText = routeText + space + untilJunction + " " + nextDetail.JunctionNumber;
            }
            return routeText;
        }

        /// <summary>
        /// Return a congestion symbol next to the route text.
        /// </summary>
        /// <param name="routeText">Route directions</param>
        /// <returns>Congestion symbol</returns>
        protected virtual string AddCongestionSymbol(string routeText)
        {
            // Not showing warning images in the web service output
            //routeText += Global.tdResourceManager.GetString("CarCostingDetails.highTrafficSymbol");

            return routeText;
        }
		
        #endregion

        #region Convert helpers and formatters

        /// <summary>
        /// Return a formatted string by converting the given metres
        /// to a mileage (only 1 decimal place will be returned in the string).
        /// </summary>
        /// <param name="metres">Metres to convert</param>
        /// <returns>Formatted string</returns>
        protected virtual string ConvertMetresToMileage(int metres)
        {
            string strResult = MeasurementConversion.Convert((double)metres, ConversionType.MetresToMileage);

            // In case we attempt to convert a really small metre value e.g. 6
            if (string.IsNullOrEmpty(strResult))
            {
                strResult = "0.00";
            }

            double result = Convert.ToDouble(strResult);

            // Return the result
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
        /// Returns formatted string of the road name for the supplied
        /// instruction
        /// </summary>
        /// <param name="detail">Details of journey instruction</param>
        /// <returns>The road name</returns>
        protected virtual string FormatRoadName(RoadJourneyDetail detail)
        {
            string roadName = detail.RoadName;
            string roadNumber = detail.RoadNumber;
            string result = String.Empty;

            if (string.IsNullOrEmpty(roadName) &&
                string.IsNullOrEmpty(roadNumber))
            {
                return localRoad;
            }

            if (!string.IsNullOrEmpty(roadNumber))
            {
                result += roadNumber;
            }

            if (!string.IsNullOrEmpty(roadName))
            {
                // Check to see if road number was empty and if so
                // then need to bracket the road name.
                if (!string.IsNullOrEmpty(roadNumber))
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
        /// processes the junction number and junction action values
        /// </summary>
        protected virtual string FormatMotorwayJunction(RoadJourneyDetail detail, string routeText)
        {
            string junctionText = String.Empty;

            //apply the junction number rules 
            switch (detail.JunctionAction)
            {
                case cjpinterface.JunctionType.Entry:

                    //add a join motorway message to the instructional text
                    routeText = routeText + space + atJunctionJoin + " " + detail.JunctionNumber;

                    break;

                case cjpinterface.JunctionType.Exit:

                    //replace the normal instructional text with a leave motorway message
                    routeText = atJunctionLeave + " " + detail.JunctionNumber + " " + leaveMotorway;

                    break;

                case cjpinterface.JunctionType.Merge:

                    //replace the normal instructional text with a leave motorway message
                    routeText = atJunctionLeave + " " + detail.JunctionNumber + " " + leaveMotorway;

                    break;

                default:

                    //no action required
                    return routeText;
            }

            //return amended route text
            return routeText;
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
            firstChar = upper ? firstChar.ToUpper(TDCultureInfo.CurrentCulture)
                : firstChar.ToLower(TDCultureInfo.CurrentCulture);

            string newString = firstChar + originalString.Substring(1);

            return newString;
        }

        #endregion

        #endregion

        #region Private methods

        /// <summary>
		/// Initialises all language sensitive text strings using
		/// the resource manager.
		/// </summary>
        private void InitialiseRouteTextDescriptionStrings()
        {
            throughRoute = rm.GetString
                ("RouteText.ThroughRoute", TDCultureInfo.CurrentUICulture, true);

            // string for roundabout exit
            roundaboutExitOne = rm.GetString
                ("RouteText.RoundaboutExitOne", TDCultureInfo.CurrentUICulture, true);
            roundaboutExitTwo = rm.GetString
                ("RouteText.RoundaboutExitTwo", TDCultureInfo.CurrentUICulture, true);
            roundaboutExitThree = rm.GetString
                ("RouteText.RoundaboutExitThree", TDCultureInfo.CurrentUICulture, true);
            roundaboutExitFour = rm.GetString
                ("RouteText.RoundaboutExitFour", TDCultureInfo.CurrentUICulture, true);
            roundaboutExitFive = rm.GetString
                ("RouteText.RoundaboutExitFive", TDCultureInfo.CurrentUICulture, true);
            roundaboutExitSix = rm.GetString
                ("RouteText.RoundaboutExitSix", TDCultureInfo.CurrentUICulture, true);
            roundaboutExitSeven = rm.GetString
                ("RouteText.RoundaboutExitSeven", TDCultureInfo.CurrentUICulture, true);
            roundaboutExitEight = rm.GetString
                ("RouteText.RoundaboutExitEight", TDCultureInfo.CurrentUICulture, true);
            roundaboutExitNine = rm.GetString
                ("RouteText.RoundaboutExitNine", TDCultureInfo.CurrentUICulture, true);
            roundaboutExitTen = rm.GetString
                ("RouteText.RoundaboutExitTen", TDCultureInfo.CurrentUICulture, true);

            continueString = rm.GetString
                ("RouteText.Continue", TDCultureInfo.CurrentUICulture, true);
            continueMiniRoundabout = rm.GetString
                ("RouteText.ContinueMiniRoundabout", TDCultureInfo.CurrentUICulture, true);
            leftMiniRoundabout = rm.GetString
                ("RouteText.LeftMiniRoundabout", TDCultureInfo.CurrentUICulture, true);
            rightMiniRoundabout = rm.GetString
                ("RouteText.RightMiniRoundabout", TDCultureInfo.CurrentUICulture, true);
            uTurnMiniRoundabout = rm.GetString
                ("RouteText.UTurnMiniRoundabout", TDCultureInfo.CurrentUICulture, true);
            leftMiniRoundabout2 = rm.GetString
                ("RouteText.LeftMiniRoundabout2", TDCultureInfo.CurrentUICulture, true);
            rightMiniRoundabout2 = rm.GetString
                ("RouteText.RightMiniRoundabout2", TDCultureInfo.CurrentUICulture, true);
            uTurnMiniRoundabout2 = rm.GetString
                ("RouteText.UTurnMiniRoundabout2", TDCultureInfo.CurrentUICulture, true);

            immediatelyTurnLeft = rm.GetString
                ("RouteText.ImmediatelyTurnLeft", TDCultureInfo.CurrentUICulture, true);
            immediatelyTurnRight = rm.GetString
                ("RouteText.ImmediatelyTurnRight", TDCultureInfo.CurrentUICulture, true);

            turnLeftOne = rm.GetString
                ("RouteText.TurnLeftOne", TDCultureInfo.CurrentUICulture, true);
            turnLeftTwo = rm.GetString
                ("RouteText.TurnLeftTwo", TDCultureInfo.CurrentUICulture, true);
            turnLeftThree = rm.GetString
                ("RouteText.TurnLeftThree", TDCultureInfo.CurrentUICulture, true);
            turnLeftFour = rm.GetString
                ("RouteText.TurnLeftFour", TDCultureInfo.CurrentUICulture, true);

            turnRightOne = rm.GetString
                ("RouteText.TurnRightOne", TDCultureInfo.CurrentUICulture, true);
            turnRightTwo = rm.GetString
                ("RouteText.TurnRightTwo", TDCultureInfo.CurrentUICulture, true);
            turnRightThree = rm.GetString
                ("RouteText.TurnRightThree", TDCultureInfo.CurrentUICulture, true);
            turnRightFour = rm.GetString
                ("RouteText.TurnRightFour", TDCultureInfo.CurrentUICulture, true);

            turnLeftInDistance = rm.GetString
                ("RouteText.TurnLeftInDistance", TDCultureInfo.CurrentUICulture, true);
            turnRightInDistance = rm.GetString
                ("RouteText.TurnRightInDistance", TDCultureInfo.CurrentUICulture, true);

            bearLeft = rm.GetString
                ("RouteText.BearLeft", TDCultureInfo.CurrentUICulture, true);
            bearRight = rm.GetString
                ("RouteText.BearRight", TDCultureInfo.CurrentUICulture, true);

            immediatelyBearLeft = rm.GetString
                ("RouteText.ImmediatelyBearLeft", TDCultureInfo.CurrentUICulture, true);
            immediatelyBearRight = rm.GetString
                ("RouteText.ImmediatelyBearRight", TDCultureInfo.CurrentUICulture, true);


            arriveAt = rm.GetString
                ("RouteText.ArriveAt", TDCultureInfo.CurrentUICulture, true);

            leaveFrom = rm.GetString
                ("RouteText.Leave", TDCultureInfo.CurrentUICulture, true);

            notApplicable = rm.GetString
                ("RouteText.NotApplicable", TDCultureInfo.CurrentUICulture, true);

            localRoad = rm.GetString
                ("RouteText.LocalRoad", TDCultureInfo.CurrentUICulture, true);

            //motorway instructions
            atJunctionLeave = rm.GetString
                ("RouteText.AtJunctionLeave", TDCultureInfo.CurrentUICulture, true);

            leaveMotorway = rm.GetString
                ("RouteText.LeaveMotorway", TDCultureInfo.CurrentUICulture, true);

            untilJunction = rm.GetString
                ("RouteText.UntilJunction", TDCultureInfo.CurrentUICulture, true);

            onTo = rm.GetString
                ("RouteText.OnTo", TDCultureInfo.CurrentUICulture, true);

            towards = rm.GetString
                ("RouteText.Towards", TDCultureInfo.CurrentUICulture, true);

            continueFor = rm.GetString
                ("RouteText.ContinueFor", TDCultureInfo.CurrentUICulture, true);

            miles = rm.GetString
                ("RouteText.Miles", TDCultureInfo.CurrentUICulture, true);

            turnLeftToJoin = rm.GetString
                ("RouteText.TurnLeftToJoin", TDCultureInfo.CurrentUICulture, true);

            turnRightToJoin = rm.GetString
                ("RouteText.TurnRightToJoin", TDCultureInfo.CurrentUICulture, true);

            atJunctionJoin = rm.GetString
                ("RouteText.AtJunctionJoin", TDCultureInfo.CurrentUICulture, true);

            bearLeftToJoin = rm.GetString
                ("RouteText.BearLeftToJoin", TDCultureInfo.CurrentUICulture, true);

            bearRightToJoin = rm.GetString
                ("RouteText.BearRightToJoin", TDCultureInfo.CurrentUICulture, true);

            join = rm.GetString
                ("RouteText.Join", TDCultureInfo.CurrentUICulture, true);

            forText = rm.GetString
                ("RouteText.For", TDCultureInfo.CurrentUICulture, true);

            follow = rm.GetString
                ("RouteText.Follow", TDCultureInfo.CurrentUICulture, true);

            to = rm.GetString
                ("RouteText.To", TDCultureInfo.CurrentUICulture, true);

            routeTextFor = rm.GetString
                ("RouteText.For", TDCultureInfo.CurrentUICulture, true);

            continueText = rm.GetString
                ("RouteText.Continue", TDCultureInfo.CurrentUICulture, true);

            //Del 7 Car Costing strings
            enter = rm.GetString
                ("RouteText.Enter", TDCultureInfo.CurrentUICulture, true);
            congestionZone = rm.GetString
                ("RouteText.CongestionCharge", TDCultureInfo.CurrentUICulture, true);
            charge = rm.GetString
                ("RouteText.Charge", TDCultureInfo.CurrentUICulture, true);
            certainTimes = rm.GetString
                ("RouteText.CertainTimes", TDCultureInfo.CurrentUICulture, true);
            certainTimesNoCharge = rm.GetString
                ("RouteText.CertainTimesNoCharge", TDCultureInfo.CurrentUICulture, true);
            board = rm.GetString
                ("RouteText.Board", TDCultureInfo.CurrentUICulture, true);
            departingAt = rm.GetString
                ("RouteText.DepartingAt", TDCultureInfo.CurrentUICulture, true);
            toll = rm.GetString
                ("RouteText.Toll", TDCultureInfo.CurrentUICulture, true);
            HighTraffic = rm.GetString
                ("RouteText.HighTraffic", TDCultureInfo.CurrentUICulture, true);
            PlanStop = rm.GetString
                ("RouteText.PlanStop", TDCultureInfo.CurrentUICulture, true);
            FerryWait = rm.GetString
                ("RouteText.WaitForFerry", TDCultureInfo.CurrentUICulture, true);
            FerryCrossing = rm.GetString
                ("RouteText.FerryCrossing", TDCultureInfo.CurrentUICulture, true);
            viaArriveAt = rm.GetString
                ("RouteText.ViaArriveAt", TDCultureInfo.CurrentUICulture, true);
            leaveFerry = rm.GetString
                ("RouteText.LeaveFerry", TDCultureInfo.CurrentUICulture, true);
            exit = rm.GetString
                ("RouteText.Exit", TDCultureInfo.CurrentUICulture, true);
            end = rm.GetString
                ("RouteText.End", TDCultureInfo.CurrentUICulture, true);
            UnspecifedFerryWait = rm.GetString
                ("RouteText.UnspecifedWaitForFerry", TDCultureInfo.CurrentUICulture, true);
            IntermediateFerryWait = rm.GetString
                ("RouteText.IntermediateFerry", TDCultureInfo.CurrentUICulture, true);
            WaitAtTerminal = rm.GetString
                ("RouteText.WaitAtTerminal", TDCultureInfo.CurrentUICulture, true);
            notAvailable = rm.GetString
                ("RouteText.NotAvailable", TDCultureInfo.CurrentUICulture, true);

            straightOn = rm.GetString
                ("RouteText.StraightOn", TDCultureInfo.CurrentUICulture, true);
            atMiniRoundabout = rm.GetString
                ("RouteText.AtMiniRoundabout", TDCultureInfo.CurrentUICulture, true);
            immediatelyTurnRightOnto = rm.GetString
                ("RouteText.ImmediatelyTurnRightOnto", TDCultureInfo.CurrentUICulture, true);
            immediatelyTurnLeftOnto = rm.GetString
                ("RouteText.ImmediatelyTurnLeftOnto", TDCultureInfo.CurrentUICulture, true);
            whereRoadSplits = rm.GetString
                ("RouteText.WhereRoadSplits", TDCultureInfo.CurrentUICulture, true);

            // park and ride
            parkAndRide = rm.GetString
                ("ParkAndRide.Suffix", TDCultureInfo.CurrentUICulture, true);
            carParkText = rm.GetString
                ("ParkAndRide.CarkPark.Suffix", TDCultureInfo.CurrentUICulture, true);
        }

        /// <summary>
        /// Read values from properties service
        /// </summary>
        private void LoadProperties()
        {
            // Get road distance in metres within which "immediate" text should be 
            // prepended to journey direction
            immediateTurnDistance = Convert.ToInt32(Properties.Current["Web.CarJourneyDetailsControl.ImmediateTurnDistance"], TDCultureInfo.CurrentUICulture.NumberFormat);

            // Get slip road distance limit parameter
            slipRoadDistance = Convert.ToInt32(Properties.Current["Web.CarJourneyDetailsControl.SlipRoadDistance"], TDCultureInfo.CurrentUICulture.NumberFormat);
        }
                
        #endregion
    }
}
