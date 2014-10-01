// *********************************************** 
// NAME                 : ProcessRequest.cs 
// AUTHOR               : David Lane 
// DATE CREATED         : 28/02/2012 
// DESCRIPTION			: Processes an indivdual request line.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/BatchJourneyPlannerService/ProcessRequest.cs-arc  $
//
//   Rev 1.18   Apr 10 2013 17:50:14   dlane
//Fix to avoid roadpoint date issue (references overwriting each other)
//Resolution for 5922: Batch historic dates fix
//
//   Rev 1.17   Apr 10 2013 11:54:56   DLane
//Historic and future date validation
//Resolution for 5922: Batch historic dates fix
//
//   Rev 1.16   Mar 22 2013 10:47:28   dlane
//CCN0648c - Super Batch enhancements
//Resolution for 5907: CCN0648c - "Super Batch" enhancements
//
//   Rev 1.15   Mar 13 2013 14:19:02   DLane
//Log zero results as failures for MIS reporting
//Resolution for 5901: Batch MIS reporting - log zero results as failure
//
//   Rev 1.14   Feb 20 2013 16:37:20   DLane
//Update to only use all TOIDs for car journeys and to fill in the date time for all TOIDs used
//Resolution for 5888: Batch - use all available TOIDs in journey request
//Resolution for 5891: Failures in batch car planning
//
//   Rev 1.13   Feb 08 2013 11:08:18   DLane
//Get all TOIDs for journey requests
//Resolution for 5888: Batch - use all available TOIDs in journey request
//
//   Rev 1.12   Feb 07 2013 09:36:42   dlane
//Adding event logging
//Resolution for 5886: Batch back end usage logging
//
//   Rev 1.11   Aug 28 2012 17:20:40   DLane
//Batch phase 2 updates - first checkin
//Resolution for 5831: CCN667 - Batch phase 2
//
//   Rev 1.10   Jun 13 2012 16:05:22   DLane
//Batch emissions fix
//Resolution for 5817: Batch emissions fix
//
//   Rev 1.9   May 15 2012 10:39:38   DLane
//Matching up EES event GUIDs
//Resolution for 5808: Batch EES match up GUIDs
//
//   Rev 1.8   Apr 30 2012 13:29:24   DLane
//Updates for EES event recording in batch
//Resolution for 5805: Record batch EES events in the reporting db
//
//   Rev 1.7   Mar 15 2012 17:35:44   dlane
//Various fixes and enhancements to batch processing
//Resolution for 5787: Batch Journey Planner
//
//   Rev 1.6   Mar 02 2012 11:26:44   DLane
//Batch updates
//Resolution for 5787: Batch Journey Planner
//
//   Rev 1.5   Feb 28 2012 15:52:28   dlane
//Batch updates and code commentry
//Resolution for 5787: Batch Journey Planner
//

using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.EnhancedExposedServices.Common;
using TransportDirect.JourneyPlanning.CJP;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.JourneyEmissions;
using TransportDirect.UserPortal.CyclePlannerService;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.CommonWeb.Batch;
using TransportDirect.CommonWeb.Helpers;
using CycleService = TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService;
using CjpInterface = TransportDirect.JourneyPlanning.CJPInterface;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;

namespace BatchJourneyPlannerService
{
    /// <summary>
    /// Enum for the request type
    /// </summary>
    public enum RequestType
    {
        PTOutward,
        PTReturn,
        CarOutward,
        CarReturn,
        CycleOutward,
        CycleReturn
    }

    public class ProcessRequest
    {
        #region class vars and constants
        private EventWaitHandle threadFinish;
        private int requestId;
        private bool incStatistics;
        private bool incDetails;
        private bool incPT;
        private bool incCar;
        private bool incCycle;
        private string uniqueId;
        private string originType;
        private string origin;
        private string destType; 
        private string dest;
        private DateTime outDate;
        private TimeSpan outTime;
        private string outArrDep;
        private DateTime retDate;
        private TimeSpan retTime;
        private string retArrDep;

        private Modes ptModes = new Modes();
        private Modes carModes = new Modes();
        private ModeType[] ptModeTypes = new ModeType[0];
        private ModeType[] carModeTypes = new ModeType[0];
        private PrivateParameters privateParameters = new PrivateParameters();
        private PublicParameters publicParameters = new PublicParameters();
        private PublicParameters publicPTParameters = new PublicParameters();

        private PublicTransportSummary ptSummary = new PublicTransportSummary();
        private CarSummary carSummary = new CarSummary();
        private CycleSummary cycleSummary = new CycleSummary();
        private XmlDocument ptDetails = null;
        private string ptOutwardError = string.Empty;
        private string carOutwardError = string.Empty;
        private string cycleOutwardError = string.Empty;
        private string ptReturnError = string.Empty;
        private string carReturnError = string.Empty;
        private string cycleReturnError = string.Empty;
        private int ptOutwardStatus = (int)BatchDetailStatus.Submitted;
        private int carOutwardStatus = (int)BatchDetailStatus.Submitted;
        private int cycleOutwardStatus = (int)BatchDetailStatus.Submitted;
        private int ptReturnStatus = (int)BatchDetailStatus.Submitted;
        private int carReturnStatus = (int)BatchDetailStatus.Submitted;
        private int cycleReturnStatus = (int)BatchDetailStatus.Submitted;
        private bool[] preProcessValidationArray = new bool[18];
        private CjpInterface.JourneyRequest ptRequest = null;
        private CjpInterface.JourneyRequest carRequest = null;
        private CycleService.JourneyRequest cycleRequest = null;
        private CycleService.Journey[] cycleJourney = new CycleService.Journey[1];
        private CjpInterface.JourneyRequest ptReturnRequest = null;
        private CjpInterface.JourneyRequest carReturnRequest = null;
        private CycleService.JourneyRequest cycleReturnRequest = null;

        private const string PENALTYFUNCTION_PREFIX = "CyclePlanner.PlannerControl.PenaltyFunction.{0}.Prefix";
        private const string NUMBER_OF_PREFERENCES = "CyclePlanner.TDUserPreference.NumberOfPreferences";
        private const string PENALTYFUNCTION_FOLDER = "CyclePlanner.PlannerControl.PenaltyFunction.Folder";
        private const string PENALTYFUNCTION_DLL = "CyclePlanner.PlannerControl.PenaltyFunction.{0}.Dll";
        private const string DEFAULT_CYCLE_JOURNEY_TYPE = "CyclePlanner.PlannerControl.PenaltyFunction.Default";
        private const string TOID_PREFIX = "JourneyControl.ToidPrefix";
        private const string EES_PARTNER_ID = "Logging.PartnerId";
        private const string LANDINGPAGE_PARTNER_ID = "BatchJP";
        #endregion

        /// <summary>
        /// Constructor (used to instantiate an instance before using a thread to call process)
        /// </summary>
        /// <param name="threadFinish">wait handle to call when processing done</param>
        /// <param name="requestId">request ID</param>
        /// <param name="incStatistics">include stats</param>
        /// <param name="incDetails">include details</param>
        /// <param name="incPT">include pulic transport journey</param>
        /// <param name="incCar">include car journey</param>
        /// <param name="incCycle">include cycle journey</param>
        /// <param name="uniqueId">user supplied unique id</param>
        /// <param name="originType">origin type c/n/p</param>
        /// <param name="origin">origin</param>
        /// <param name="destType">destination type c/n/p</param>
        /// <param name="dest">destination</param>
        /// <param name="outDate">outward date</param>
        /// <param name="outTime">outward time</param>
        /// <param name="outArrDep">outward arrive/depart</param>
        /// <param name="retDate">return date</param>
        /// <param name="retTime">return time</param>
        /// <param name="retArrDep">return arrive/depart</param>
        /// <param name="preProcessValidationArray"> line prevalidation result - if not passed treat as failed line on all subsequent validation</param>

        public ProcessRequest(EventWaitHandle threadFinish, int requestId, bool incStatistics, bool incDetails, bool incPT, bool incCar, bool incCycle, string uniqueId, string originType, string origin, string destType, string dest, DateTime outDate, TimeSpan outTime, string outArrDep, DateTime retDate, TimeSpan retTime, string retArrDep, bool [] preProcessValidationArray) 
        {
            if (TDTraceSwitch.TraceVerbose)
            {
                OperationalEvent op = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, string.Format("Batch starting to process batch request row {0}", requestId));
                Logger.Write(op);
            }

            #region Transfer parameters
            this.threadFinish = threadFinish;
            this.requestId = requestId;
            this.incStatistics = incStatistics;
            this.incDetails = incDetails;
            this.incPT = incPT;
            this.incCar = incCar;
            this.incCycle = incCycle;
            this.uniqueId = uniqueId;
            this.originType = originType;
            this.origin = origin;
            this.destType = destType;
            this.dest = dest;
            this.outDate = outDate;
            this.outTime = outTime;
            this.outArrDep = outArrDep;
            this.retDate = retDate;
            this.retTime = retTime;
            this.retArrDep = retArrDep;
            this.preProcessValidationArray = preProcessValidationArray;
            #endregion

            #region Mode arrays
            ptModes.modes = new Mode[12];
            ptModeTypes = new ModeType[12];
            ptModes.modes[0] = new Mode();
            ptModes.modes[0].mode = ModeType.Bus;
            ptModeTypes[0] = ModeType.Bus;
            ptModes.modes[1] = new Mode();
            ptModes.modes[1].mode = ModeType.Coach;
            ptModeTypes[1] = ModeType.Coach;
            ptModes.modes[2] = new Mode();
            ptModes.modes[2].mode = ModeType.Drt;
            ptModeTypes[2] = ModeType.Drt;
            ptModes.modes[3] = new Mode();
            ptModes.modes[3].mode = ModeType.Ferry;
            ptModeTypes[3] = ModeType.Ferry;
            ptModes.modes[4] = new Mode();
            ptModes.modes[4].mode = ModeType.Metro;
            ptModeTypes[4] = ModeType.Metro;
            ptModes.modes[5] = new Mode();
            ptModes.modes[5].mode = ModeType.Rail;
            ptModeTypes[5] = ModeType.Rail;
            ptModes.modes[6] = new Mode();
            ptModes.modes[6].mode = ModeType.Air;
            ptModeTypes[6] = ModeType.Air;
            ptModes.modes[7] = new Mode();
            ptModes.modes[7].mode = ModeType.Taxi;
            ptModeTypes[7] = ModeType.Taxi;
            ptModes.modes[8] = new Mode();
            ptModes.modes[8].mode = ModeType.Tram;
            ptModeTypes[8] = ModeType.Tram;
            ptModes.modes[9] = new Mode();
            ptModes.modes[9].mode = ModeType.Telecabine;
            ptModeTypes[9] = ModeType.Telecabine;
            ptModes.modes[10] = new Mode();
            ptModes.modes[10].mode = ModeType.Underground;
            ptModeTypes[10] = ModeType.Underground;
            ptModes.modes[11] = new Mode();
            ptModes.modes[11].mode = ModeType.Walk;
            ptModeTypes[11] = ModeType.Walk;
            ptModes.include = true;

            carModes.modes = new Mode[1];
            carModeTypes = new ModeType[1];
            carModes.modes[0] = new Mode();
            carModes.modes[0].mode = ModeType.Car;
            carModeTypes[0] = ModeType.Car;
            carModes.include = true;
            #endregion

            #region public and private parameters
            privateParameters.algorithm = PrivateAlgorithmType.Fastest;
            privateParameters.avoidFerries = false;
            privateParameters.avoidMotorway = false;
            privateParameters.avoidRoads = null;
            privateParameters.avoidToll = false;
            privateParameters.banMotorway = false;
            privateParameters.banNamedAccessRestrictions = false;
            privateParameters.bannedTOIDs = null;
            privateParameters.flowType = FlowType.Congestion;
            privateParameters.notVias = null;
            privateParameters.useRoads = null;
            privateParameters.vehicleType = VehicleType.Car;
            privateParameters.vias = null;
            // Get default values
			IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			string defaultCarSize  = string.Empty;
			string defaultFuelType = string.Empty;
			defaultCarSize  = ds.GetDefaultListControlValue(DataServiceType.ListCarSizeDrop);
			defaultFuelType = ds.GetDefaultListControlValue(DataServiceType.ListFuelTypeDrop);
			CarCostCalculator costCalculator = (CarCostCalculator)TDServiceDiscovery.Current[ ServiceDiscoveryKey.CarCostCalculator ];	
			privateParameters.fuelConsumption = int.Parse(costCalculator.GetFuelConsumption(defaultCarSize, defaultFuelType).ToString(CultureInfo.InvariantCulture));
			privateParameters.fuelPrice = int.Parse(costCalculator.GetFuelCost(defaultFuelType).ToString(CultureInfo.InvariantCulture));
			privateParameters.maxSpeed = Convert.ToInt32(ds.GetDefaultListControlValue(DataServiceType.DrivingMaxSpeedDrop), TDCultureInfo.CurrentCulture);
            
            publicParameters.accessibilityOptions = new AccessibilityOptions();
            publicParameters.algorithm = PublicAlgorithmType.Fastest;
            publicParameters.dontForceCoach = true;
            publicParameters.extraCheckInTime = DateTime.MinValue;
            publicParameters.extraInterval = DateTime.MinValue;
            publicParameters.extraSequence = 0;
            //publicParameters.filtering = FilterOptionEnumeration.; Can't find any existing usage!!
            publicParameters.interchangeSpeed = Convert.ToInt32(ds.GetDefaultListControlValue(DataServiceType.ChangesSpeedDrop), CultureInfo.CurrentCulture);
            publicParameters.intermediateStops = IntermediateStopsType.All;
            publicParameters.interval = DateTime.MinValue;
            publicParameters.notVias = null;
            publicParameters.olympicRequest = false;
            publicParameters.rangeType = RangeType.Sequence;
            publicParameters.rejectNonRGCompliantJourneys = false;
            publicParameters.routeCodes = string.Empty;
            publicParameters.routingGuideInfluenced = false;
            publicParameters.routingStops = new string[0];
            publicParameters.sequence = 1;
            publicParameters.softVias = null;
            publicParameters.trunkPlan = false;
            publicParameters.vehicleFeatures = null;
            publicParameters.vias = new CjpInterface.RequestPlace[0];
            publicParameters.walkSpeed = Convert.ToInt32(ds.GetDefaultListControlValue(DataServiceType.WalkingSpeedDrop), CultureInfo.CurrentCulture);
            publicParameters.maxWalkDistance = publicParameters.walkSpeed * Convert.ToInt32(ds.GetDefaultListControlValue(DataServiceType.WalkingMaxTimeDrop), CultureInfo.CurrentCulture);

            publicPTParameters = (PublicParameters)publicParameters.Clone();
            if (incDetails)
            {
                publicPTParameters.sequence = 2;
            }
            else
            {
                publicPTParameters.sequence = 1;
            }
            #endregion
        }

        /// <summary>
        /// Method to call with the thread - processes the journeys
        /// </summary>
        public void Process()
        {


          string [] preValidationColumn = new string[18];
            preValidationColumn[0] = "requestId";
            preValidationColumn[1] = "incStatistics";
            preValidationColumn[2] = "incDetails";
            preValidationColumn[3] = "incPT";
            preValidationColumn[4] = "incCar";
            preValidationColumn[5] = "incCycle";
            preValidationColumn[6] = "Not Used";
            preValidationColumn[7] = "uniqueId";
            preValidationColumn[8] = "originType";
            preValidationColumn[9] = "origin";
            preValidationColumn[10] = "destType";
            preValidationColumn[11] = "dest";
            preValidationColumn[12] = "outDate";
            preValidationColumn[13] = "outTime";
            preValidationColumn[14] = "outArrDep";
            preValidationColumn[15] = "retDate";
            preValidationColumn[16] = "retTime";
            preValidationColumn[17] = "retArrDep";
            string preValidationErrorMsg = string.Empty;
            bool testMode = false;
            int totPreValidationFailures = 0;
            bool.TryParse(Properties.Current["BatchJourneyPlannerTestMode"], out testMode);
            for (int i = 0; i < 18; i++)
            {
                if (preProcessValidationArray[i] == false)
                { 
                    totPreValidationFailures++;
                }
            }
           

            if (testMode)
            {
                // Return stock answers to the db
                SetStockAnswers();

            }
               
            try
            {
                    // Make the calls to the CJP
                    if (totPreValidationFailures == 0 )   // no failures so ok to process
                    {
                        if (!testMode)  // But only process if its not test mode
                        {
                            CreateRequests();
                            PlanCJPandCycleJourneys();
                        }
                    }
                    else 
                    {
                       
                        if (totPreValidationFailures > 1)
                        {
                            
                            preValidationErrorMsg = "Failed Line Validation on multiple columns";
                        }
                        else
                        {
                            for (int i = 0; i < 18; i++)
                            {
                                if (preProcessValidationArray[i] == false)
                                {
                                    preValidationErrorMsg = "Failed line validation on column " + preValidationColumn[i];
                                }
                            }
                        } 
                        if (TDTraceSwitch.TraceError)
                            Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, preValidationErrorMsg));

                        ptOutwardStatus = (int)BatchDetailStatus.ValidationError;
                        ptOutwardError = preValidationErrorMsg;
                        ptReturnStatus = (int)BatchDetailStatus.ValidationError;
                        ptReturnError = preValidationErrorMsg;
                        ptReturnStatus = (int)BatchDetailStatus.ValidationError;
                        ptReturnError = preValidationErrorMsg;
                        carOutwardStatus = (int)BatchDetailStatus.ValidationError;
                        carOutwardError = preValidationErrorMsg;
                        carReturnStatus = (int)BatchDetailStatus.ValidationError;
                        carReturnError = preValidationErrorMsg;
                        cycleOutwardStatus = (int)BatchDetailStatus.ValidationError;
                        cycleOutwardError = preValidationErrorMsg;
                        cycleReturnStatus = (int)BatchDetailStatus.ValidationError;
                        cycleReturnError = preValidationErrorMsg;
                    }
            }
            catch (Exception ex)
            {
                    if (TDTraceSwitch.TraceError)
                        Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, string.Format("Journey planning failed batch request detail row {0}: {1}", requestId, ex.ToString())));

                    ptOutwardStatus = (int)BatchDetailStatus.Errored;
                    ptReturnStatus = (int)BatchDetailStatus.Errored;
                    carOutwardStatus = (int)BatchDetailStatus.Errored;
                    carReturnStatus = (int)BatchDetailStatus.Errored;
                    cycleOutwardStatus = (int)BatchDetailStatus.Errored;
                    cycleReturnStatus = (int)BatchDetailStatus.Errored;
            }
            

            try
            {
                // Update the database
                UpdateBatchDetailRecord();
            }
            catch (Exception ex)
            {
                if (TDTraceSwitch.TraceError)
                    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, string.Format("Updatnig failed for batch request detail row {0}: {1}", requestId, ex.ToString())));
            }

            if (TDTraceSwitch.TraceVerbose)
            {
                OperationalEvent op = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, string.Format("Finished processing batch request detail row {0}", requestId));
                Logger.Write(op);
            }

            // Must be last statement
            threadFinish.Set();
        }

        /// <summary>
        /// Method to set up the requests for the CJP
        /// </summary>
        private void CreateRequests()
        {
            #region Set up the JourneyRequests

            IGisQuery gq = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
            string toidPrefix = Properties.Current[TOID_PREFIX];

            #region Vars for all journeys

            // Set up origin and destination for both outward and return
            CjpInterface.RequestPlace outwardOrigin = new CjpInterface.RequestPlace();
            CjpInterface.RequestPlace outwardDestination = new CjpInterface.RequestPlace();
            outwardOrigin.coordinate = new CjpInterface.Coordinate();
            outwardDestination.coordinate = new CjpInterface.Coordinate();
            CjpInterface.RequestPlace carOutwardOrigin = new CjpInterface.RequestPlace();
            CjpInterface.RequestPlace carOutwardDestination = new CjpInterface.RequestPlace();
            carOutwardOrigin.coordinate = new CjpInterface.Coordinate();
            carOutwardDestination.coordinate = new CjpInterface.Coordinate();

            CjpInterface.RequestPlace returnOrigin = new CjpInterface.RequestPlace();
            CjpInterface.RequestPlace returnDestination = new CjpInterface.RequestPlace();
            returnOrigin.coordinate = new CjpInterface.Coordinate();
            returnDestination.coordinate = new CjpInterface.Coordinate();
            CjpInterface.RequestPlace carReturnOrigin = new CjpInterface.RequestPlace();
            CjpInterface.RequestPlace carReturnDestination = new CjpInterface.RequestPlace();
            carReturnOrigin.coordinate = new CjpInterface.Coordinate();
            carReturnDestination.coordinate = new CjpInterface.Coordinate();

            if (originType == "c")
            {
                // need locality
                outwardOrigin.coordinate.easting = int.Parse(origin.Split('|')[0]);
                outwardOrigin.coordinate.northing = int.Parse(origin.Split('|')[1]);
                outwardOrigin.givenName = "Grid reference";
                outwardOrigin.type = RequestPlaceType.Coordinate;
                outwardOrigin.locality = gq.FindNearestLocality(outwardOrigin.coordinate.easting, outwardOrigin.coordinate.northing);
                outwardOrigin.roadPoints = new CjpInterface.ITN[1];
                outwardOrigin.roadPoints = new CjpInterface.ITN[] { new CjpInterface.ITN() };
                QuerySchema gisResult = gq.FindNearestITNs(outwardOrigin.coordinate.easting, outwardOrigin.coordinate.northing);
                QuerySchema.ITNRow row = (QuerySchema.ITNRow)gisResult.ITN.Rows[0];
                outwardOrigin.roadPoints[0].TOID = toidPrefix + row.toid;

                carOutwardOrigin.coordinate = outwardOrigin.coordinate;
                carOutwardOrigin.givenName = "Grid reference";
                carOutwardOrigin.type = RequestPlaceType.Coordinate;
                carOutwardOrigin.locality = outwardOrigin.locality;
                carOutwardOrigin.roadPoints = GetITNs(carOutwardOrigin.coordinate.easting, carOutwardOrigin.coordinate.northing);

                returnDestination.coordinate = outwardOrigin.coordinate;
                returnDestination.givenName = "Grid reference";
                returnDestination.type = RequestPlaceType.Coordinate;
                returnDestination.locality = outwardOrigin.locality;
                returnDestination.roadPoints = new CjpInterface.ITN[] { new CjpInterface.ITN() };
                returnDestination.roadPoints[0].TOID = outwardOrigin.roadPoints[0].TOID;

                carReturnDestination.coordinate = carOutwardOrigin.coordinate;
                carReturnDestination.givenName = "Grid reference";
                carReturnDestination.type = RequestPlaceType.Coordinate;
                carReturnDestination.locality = returnDestination.locality;
                carReturnDestination.roadPoints = GetITNs(carReturnDestination.coordinate.easting, carReturnDestination.coordinate.northing);
            }
            else if (originType == "n")
            {
                // need locality
                NaptanCacheEntry entry = NaptanLookup.Get(origin.Trim().ToUpper(), "Naptan");
                outwardOrigin.coordinate.easting = entry.OSGR.Easting;
                outwardOrigin.coordinate.northing = entry.OSGR.Northing;
                outwardOrigin.givenName = entry.Description;
                outwardOrigin.type = RequestPlaceType.Coordinate;
                outwardOrigin.locality = entry.Locality;
                outwardOrigin.roadPoints = new CjpInterface.ITN[1];
                outwardOrigin.roadPoints = new CjpInterface.ITN[] { new CjpInterface.ITN() };
                QuerySchema gisResult = gq.FindNearestITNs(outwardOrigin.coordinate.easting, outwardOrigin.coordinate.northing);
                QuerySchema.ITNRow row = (QuerySchema.ITNRow)gisResult.ITN.Rows[0];
                outwardOrigin.roadPoints[0].TOID = toidPrefix + row.toid;

                carOutwardOrigin.coordinate = outwardOrigin.coordinate;
                carOutwardOrigin.givenName = entry.Description;
                carOutwardOrigin.type = RequestPlaceType.Coordinate;
                carOutwardOrigin.locality = entry.Locality;
                carOutwardOrigin.roadPoints = GetITNs(carOutwardOrigin.coordinate.easting, carOutwardOrigin.coordinate.northing);

                returnDestination.coordinate.easting = entry.OSGR.Easting;
                returnDestination.coordinate.northing = entry.OSGR.Northing;
                returnDestination.givenName = entry.Description;
                returnDestination.type = RequestPlaceType.Coordinate;
                returnDestination.locality = entry.Locality;
                returnDestination.roadPoints = new CjpInterface.ITN[] { new CjpInterface.ITN() };
                returnDestination.roadPoints[0].TOID = outwardOrigin.roadPoints[0].TOID;

                carReturnDestination.coordinate = carOutwardOrigin.coordinate;
                carReturnDestination.givenName = entry.Description;
                carReturnDestination.type = RequestPlaceType.Coordinate;
                carReturnDestination.locality = entry.Locality;
                carReturnDestination.roadPoints = GetITNs(carReturnDestination.coordinate.easting, carReturnDestination.coordinate.northing);
            }
            else
            {
                //Perform operation to return grid reference for postcode
                LocationChoiceList locationChoiceList = LocationSearch.FindPostcode(origin, "BatchJourneyPlanner", false);

                //If the locationChoiceList has 0 or more than 1 result throw a TDException
                //We excepted 1 result otherwise the postcode is ambiguous 
                if (locationChoiceList.Count != 1)
                {
                    throw new TDException("Postcode not found", false, TDExceptionIdentifier.ESSFindNearestServiceUniqueOSGridReferenceNotFoundForPostcode);
                }

                outwardOrigin.coordinate.easting = (locationChoiceList[0] as LocationChoice).OSGridReference.Easting;
                outwardOrigin.coordinate.northing = (locationChoiceList[0] as LocationChoice).OSGridReference.Northing;
                outwardOrigin.givenName = origin;
                outwardOrigin.type = RequestPlaceType.Coordinate;
                outwardOrigin.locality = gq.FindNearestLocality(outwardOrigin.coordinate.easting, outwardOrigin.coordinate.northing);
                outwardOrigin.roadPoints = GetPostcodeITNs(outwardOrigin.coordinate.easting, outwardOrigin.coordinate.northing, origin);

                carOutwardOrigin.coordinate = outwardOrigin.coordinate;
                carOutwardOrigin.givenName = origin;
                carOutwardOrigin.type = RequestPlaceType.Coordinate;
                carOutwardOrigin.locality = outwardOrigin.locality;
                carOutwardOrigin.roadPoints = outwardOrigin.roadPoints;

                returnDestination.coordinate = outwardOrigin.coordinate;
                returnDestination.givenName = origin;
                returnDestination.type = RequestPlaceType.Coordinate;
                returnDestination.locality = outwardOrigin.locality;
                returnDestination.roadPoints = outwardOrigin.roadPoints;

                carReturnDestination.coordinate = outwardOrigin.coordinate;
                carReturnDestination.givenName = origin;
                carReturnDestination.type = RequestPlaceType.Coordinate;
                carReturnDestination.locality = outwardOrigin.locality;
                carReturnDestination.roadPoints = outwardOrigin.roadPoints;
            }

            if (destType == "c")
            {
                // need locality
                outwardDestination.coordinate.easting = int.Parse(dest.Split('|')[0]);
                outwardDestination.coordinate.northing = int.Parse(dest.Split('|')[1]);
                outwardDestination.givenName = "Grid reference";
                outwardDestination.type = RequestPlaceType.Coordinate;
                outwardDestination.locality = gq.FindNearestLocality(outwardDestination.coordinate.easting, outwardDestination.coordinate.northing);
                outwardDestination.roadPoints = new CjpInterface.ITN[1];
                outwardDestination.roadPoints = new CjpInterface.ITN[] { new CjpInterface.ITN() };
                QuerySchema gisResult = gq.FindNearestITNs(outwardDestination.coordinate.easting, outwardDestination.coordinate.northing);
                QuerySchema.ITNRow row = (QuerySchema.ITNRow)gisResult.ITN.Rows[0];
                outwardDestination.roadPoints[0].TOID = toidPrefix + row.toid;

                carOutwardDestination.coordinate = outwardDestination.coordinate;
                carOutwardDestination.givenName = "Grid reference";
                carOutwardDestination.type = RequestPlaceType.Coordinate;
                carOutwardDestination.locality = outwardDestination.locality;
                carOutwardDestination.roadPoints = GetITNs(carOutwardDestination.coordinate.easting, carOutwardDestination.coordinate.northing);

                returnOrigin.coordinate = outwardDestination.coordinate;
                returnOrigin.givenName = "Grid reference";
                returnOrigin.type = RequestPlaceType.Coordinate;
                returnOrigin.locality = outwardDestination.locality;
                returnOrigin.roadPoints = new CjpInterface.ITN[] { new CjpInterface.ITN() };
                returnOrigin.roadPoints[0].TOID = outwardDestination.roadPoints[0].TOID;

                carReturnOrigin.coordinate = carOutwardDestination.coordinate;
                carReturnOrigin.givenName = "Grid reference";
                carReturnOrigin.type = RequestPlaceType.Coordinate;
                carReturnOrigin.locality = carOutwardDestination.locality;
                carReturnOrigin.roadPoints = GetITNs(carReturnOrigin.coordinate.easting, carReturnOrigin.coordinate.northing);
            }
            else if (destType == "n")
            {
                // need locality
                NaptanCacheEntry entry = NaptanLookup.Get(dest.Trim().ToUpper(), "Naptan");
                outwardDestination.coordinate.easting = entry.OSGR.Easting;
                outwardDestination.coordinate.northing = entry.OSGR.Northing;
                outwardDestination.givenName = entry.Description;
                outwardDestination.type = RequestPlaceType.Coordinate;
                outwardDestination.locality = entry.Locality;
                outwardDestination.roadPoints = new CjpInterface.ITN[1];
                outwardDestination.roadPoints = new CjpInterface.ITN[] { new CjpInterface.ITN() };
                QuerySchema gisResult = gq.FindNearestITNs(outwardDestination.coordinate.easting, outwardDestination.coordinate.northing);
                QuerySchema.ITNRow row = (QuerySchema.ITNRow)gisResult.ITN.Rows[0];
                outwardDestination.roadPoints[0].TOID = toidPrefix + row.toid;

                carOutwardDestination.coordinate = outwardDestination.coordinate;
                carOutwardDestination.givenName = entry.Description;
                carOutwardDestination.type = RequestPlaceType.Coordinate;
                carOutwardDestination.locality = outwardDestination.locality;
                carOutwardDestination.roadPoints = GetITNs(carOutwardDestination.coordinate.easting, carOutwardDestination.coordinate.northing);

                returnOrigin.coordinate.easting = entry.OSGR.Easting;
                returnOrigin.coordinate.northing = entry.OSGR.Northing;
                returnOrigin.givenName = entry.Description;
                returnOrigin.type = RequestPlaceType.Coordinate;
                returnOrigin.locality = entry.Locality;
                returnOrigin.roadPoints = new CjpInterface.ITN[] { new CjpInterface.ITN() };
                returnOrigin.roadPoints[0].TOID = outwardDestination.roadPoints[0].TOID;

                carReturnOrigin.coordinate = carOutwardDestination.coordinate;
                carReturnOrigin.givenName = entry.Description;
                carReturnOrigin.type = RequestPlaceType.Coordinate;
                carReturnOrigin.locality = carOutwardDestination.locality;
                carReturnOrigin.roadPoints = GetITNs(carReturnOrigin.coordinate.easting, carReturnOrigin.coordinate.northing);
            }
            else
            {
                //Perform operation to return grid reference for postcode
                LocationChoiceList locationChoiceList = LocationSearch.FindPostcode(dest, "BatchJourneyPlanner", false);

                //If the locationChoiceList has 0 or more than 1 result throw a TDException
                //We excepted 1 result otherwise the postcode is ambiguous 
                if (locationChoiceList.Count != 1)
                {
                    throw new TDException("Postcode not found", false, TDExceptionIdentifier.ESSFindNearestServiceUniqueOSGridReferenceNotFoundForPostcode);
                }

                outwardDestination.coordinate.easting = (locationChoiceList[0] as LocationChoice).OSGridReference.Easting;
                outwardDestination.coordinate.northing = (locationChoiceList[0] as LocationChoice).OSGridReference.Northing;
                outwardDestination.givenName = dest;
                outwardDestination.type = RequestPlaceType.Coordinate;
                outwardDestination.locality = gq.FindNearestLocality(outwardDestination.coordinate.easting, outwardDestination.coordinate.northing);
                outwardDestination.roadPoints = GetPostcodeITNs(outwardDestination.coordinate.easting, outwardDestination.coordinate.northing, dest);

                carOutwardDestination.coordinate = outwardDestination.coordinate;
                carOutwardDestination.givenName = dest;
                carOutwardDestination.type = RequestPlaceType.Coordinate;
                carOutwardDestination.locality = outwardDestination.locality;
                carOutwardDestination.roadPoints = outwardDestination.roadPoints;

                returnOrigin.coordinate = outwardDestination.coordinate;
                returnOrigin.givenName = dest;
                returnOrigin.type = RequestPlaceType.Coordinate;
                returnOrigin.locality = outwardDestination.locality;
                returnOrigin.roadPoints = outwardDestination.roadPoints;

                carReturnOrigin.coordinate = carOutwardDestination.coordinate;
                carReturnOrigin.givenName = dest;
                carReturnOrigin.type = RequestPlaceType.Coordinate;
                carReturnOrigin.locality = outwardDestination.locality;
                carReturnOrigin.roadPoints = outwardDestination.roadPoints;
            }

            RequestStop outStop = new RequestStop();
            DateTime outDateTime = outDate.Add(outTime);
            outStop.timeDate = outDateTime;
            outStop.NaPTANID = string.Empty;

            RequestStop retStop = new RequestStop();
            DateTime retDateTime = DateTime.MinValue;
            if (!string.IsNullOrEmpty(retArrDep))
            {
                retDateTime = retDate.Add(retTime);
                retStop.timeDate = retDateTime;
                retStop.NaPTANID = string.Empty;
            }

            #endregion

            #region Public Transport request

            if (incPT)
            {
                // landing page URL
                if (ptSummary.LandingUrL == string.Empty)
                {
                    GenerateDoorToDoorURL(outwardOrigin, originType, outwardDestination, destType);
                }

                // Get 2 PT journeys with force coach off
                ptRequest = new CjpInterface.JourneyRequest();
                ptRequest.requestID = requestId + "pto";
                ptRequest.origin = outwardOrigin;
                ptRequest.destination = outwardDestination;
                ptRequest.modeFilter = ptModes;
                ptRequest = AddStandardProperties(ptRequest);
                ptRequest.publicParameters = publicPTParameters;
                ptRequest.sessionID = "BatchJourneyPlannerPTO" + requestId.ToString();

                RequestStop emptyStop = new RequestStop();
                emptyStop.timeDate = DateTime.MinValue;
                emptyStop.NaPTANID = string.Empty;

                if (outArrDep == "d")
                {
                    ptRequest.depart = true;
                    ptRequest.origin.stops = new RequestStop[] { outStop };
                    ptRequest.destination.stops = new RequestStop[] { emptyStop };
                    ptRequest.origin.roadPoints[0].timeDate = outDateTime;
                    ptRequest.destination.roadPoints[0].timeDate = DateTime.MinValue;
                    foreach
                        (CjpInterface.ITN point in ptRequest.origin.roadPoints)
                    {
                        point.timeDate = outDateTime;
                    }
                    foreach (CjpInterface.ITN point in ptRequest.destination.roadPoints)
                    {
                        point.timeDate = DateTime.MinValue;
                    }
                }
                else
                {
                    ptRequest.depart = false;
                    ptRequest.destination.stops = new RequestStop[] { outStop };
                    ptRequest.origin.stops = new RequestStop[] { emptyStop };
                    foreach (CjpInterface.ITN point in ptRequest.origin.roadPoints)
                    {
                        point.timeDate = DateTime.MinValue;
                    }
                    foreach (CjpInterface.ITN point in ptRequest.destination.roadPoints)
                    {
                        point.timeDate = outDateTime;
                    }
                }

                if (!string.IsNullOrEmpty(retArrDep))
                {
                    ptReturnRequest = new CjpInterface.JourneyRequest();
                    ptReturnRequest.requestID = requestId.ToString() + "ptr";
                    ptReturnRequest.origin = returnOrigin;
                    ptReturnRequest.destination = returnDestination;
                    ptReturnRequest.modeFilter = ptModes;
                    ptReturnRequest = AddStandardProperties(ptReturnRequest);
                    ptReturnRequest.publicParameters = publicPTParameters;
                    ptReturnRequest.sessionID = "BatchJourneyPlannerPTR" + requestId.ToString();

                    RequestStop emptyStopRet = new RequestStop();
                    emptyStopRet.timeDate = DateTime.MinValue;
                    emptyStopRet.NaPTANID = string.Empty;

                    if (retArrDep == "d")
                    {
                        ptReturnRequest.depart = true;
                        ptReturnRequest.origin.stops = new RequestStop[] { retStop };
                        ptReturnRequest.destination.stops = new RequestStop[] { emptyStopRet };
                        foreach (CjpInterface.ITN point in ptReturnRequest.origin.roadPoints)
                        {
                            point.timeDate = retDateTime;
                        }
                        foreach (CjpInterface.ITN point in ptReturnRequest.destination.roadPoints)
                        {
                            point.timeDate = DateTime.MinValue;
                        }
                    }
                    else
                    {
                        ptReturnRequest.depart = false;
                        ptReturnRequest.destination.stops = new RequestStop[] { retStop };
                        ptReturnRequest.origin.stops = new RequestStop[] { emptyStopRet };
                        foreach (CjpInterface.ITN point in ptReturnRequest.origin.roadPoints)
                        {
                            point.timeDate = DateTime.MinValue;
                        }
                        foreach (CjpInterface.ITN point in ptReturnRequest.destination.roadPoints)
                        {
                            point.timeDate = retDateTime;
                        }
                    }
                }
            }

            #endregion

            #region Car request

            if (incCar)
            {
                // landing page URL
                if (ptSummary.LandingUrL == string.Empty)
                {
                    GenerateDoorToDoorURL(outwardOrigin, originType, outwardDestination, destType);
                }

                // Get a car journey plan
                carRequest = new CjpInterface.JourneyRequest();
                carRequest.requestID = requestId.ToString() + "co";
                carRequest.origin = carOutwardOrigin;
                carRequest.destination = carOutwardDestination;
                carRequest.modeFilter = carModes;
                carRequest = AddStandardProperties(carRequest);
                carRequest.publicParameters = publicParameters;
                carRequest.sessionID = "BatchJourneyPlannerCO" + requestId.ToString();

                RequestStop emptyStop = new RequestStop();
                emptyStop.timeDate = DateTime.MinValue;
                emptyStop.NaPTANID = string.Empty;

                if (outArrDep == "d")
                {
                    carRequest.depart = true;
                    carRequest.origin.stops = new RequestStop[] { outStop };
                    carRequest.destination.stops = new RequestStop[] { emptyStop };
                    foreach (CjpInterface.ITN point in carRequest.origin.roadPoints)
                    {
                        point.timeDate = outDateTime;
                    }
                    foreach (CjpInterface.ITN point in carRequest.destination.roadPoints)
                    {
                        point.timeDate = DateTime.MinValue;
                    }
                }
                else
                {
                    carRequest.depart = false;
                    carRequest.destination.stops = new RequestStop[] { outStop };
                    carRequest.origin.stops = new RequestStop[] { emptyStop };
                    foreach (CjpInterface.ITN point in carRequest.origin.roadPoints)
                    {
                        point.timeDate = DateTime.MinValue;
                    }
                    foreach (CjpInterface.ITN point in carRequest.destination.roadPoints)
                    {
                        point.timeDate = outDateTime;
                    }
                }

                if (!string.IsNullOrEmpty(retArrDep))
                {
                    carReturnRequest = new CjpInterface.JourneyRequest();
                    carReturnRequest.requestID = requestId.ToString() + "cr";
                    carReturnRequest.origin = carReturnOrigin;
                    carReturnRequest.destination = carReturnDestination;
                    carReturnRequest.modeFilter = carModes;
                    carReturnRequest = AddStandardProperties(carReturnRequest);
                    carReturnRequest.publicParameters = publicParameters;
                    carReturnRequest.sessionID = "BatchJourneyPlannerCR" + requestId.ToString();

                    RequestStop emptyStopRet = new RequestStop();
                    emptyStopRet.timeDate = DateTime.MinValue;
                    emptyStopRet.NaPTANID = string.Empty;

                    if (retArrDep == "d")
                    {
                        carReturnRequest.depart = true;
                        carReturnRequest.origin.stops = new RequestStop[] { retStop };
                        carReturnRequest.destination.stops = new RequestStop[] { emptyStopRet };
                        foreach (CjpInterface.ITN point in carReturnRequest.origin.roadPoints)
                        {
                            point.timeDate = retDateTime;
                        }
                        foreach (CjpInterface.ITN point in carReturnRequest.destination.roadPoints)
                        {
                            point.timeDate = DateTime.MinValue;
                        }
                    }
                    else
                    {
                        carReturnRequest.depart = false;
                        carReturnRequest.destination.stops = new RequestStop[] { retStop };
                        carReturnRequest.origin.stops = new RequestStop[] { emptyStopRet };
                        foreach (CjpInterface.ITN point in carReturnRequest.origin.roadPoints)
                        {
                            point.timeDate = DateTime.MinValue;
                        }
                        foreach (CjpInterface.ITN point in carReturnRequest.destination.roadPoints)
                        {
                            point.timeDate = retDateTime;
                        }
                    }
                }
            }

            #endregion

            #region Cycle request

            if (incCycle)
            {
                // landing page URL
                if (cycleSummary.LandingUrL == string.Empty)
                {
                    GenerateCycleURL(outwardOrigin, originType, outwardDestination, destType);
                }

                // Check journey length is within max cycle distance
                long east = outwardOrigin.coordinate.easting - outwardDestination.coordinate.easting;
                long north = outwardOrigin.coordinate.northing - outwardDestination.coordinate.northing;
                decimal distance = decimal.Parse(Math.Sqrt((east * east) + (north * north)).ToString());
                IPropertyProvider properties = Properties.Current;
                int maxDistance = int.Parse(properties["CyclePlanner.Planner.MaxJourneyDistance.Metres"]);

                if (distance > maxDistance)
                {
                    // error messages and return codes
                    cycleOutwardStatus = (int)BatchDetailStatus.Errored;
                    cycleOutwardError = "Outward cycle distance exceeds maximum distance. ";

                    if (!string.IsNullOrEmpty(retArrDep))
                    {
                        cycleReturnStatus = (int)BatchDetailStatus.Errored;
                        cycleReturnError = "Return cycle distance exceeds maximum distance. ";
                    }

                }
                else
                {
                    // Call the cycle journey planner
                    cycleRequest = new CycleService.JourneyRequest();
                    cycleJourney[0] = new CycleService.Journey();

                    CycleService.RequestPlace cycleOrigin = new CycleService.RequestPlace();
                    cycleOrigin.coordinate = new CycleService.Coordinate();
                    cycleOrigin.coordinate.easting = outwardOrigin.coordinate.easting;
                    cycleOrigin.coordinate.northing = outwardOrigin.coordinate.northing;
                    cycleOrigin.givenName = outwardOrigin.givenName;
                    cycleOrigin.roadPoints = new CycleService.ITN[0];
                    cycleRequest.origin = cycleOrigin;

                    CycleService.RequestPlace cycleDestination = new CycleService.RequestPlace();
                    cycleDestination.coordinate = new CycleService.Coordinate();
                    cycleDestination.coordinate.easting = outwardDestination.coordinate.easting;
                    cycleDestination.coordinate.northing = outwardDestination.coordinate.northing;
                    cycleDestination.givenName = outwardDestination.givenName;
                    cycleDestination.roadPoints = new CycleService.ITN[0];
                    cycleRequest.destination = cycleDestination;

                    cycleRequest.requestID = "CYO" + requestId.ToString();
                    cycleRequest.language = "en-GB";
                    cycleRequest.sessionID = "BatchJourneyPlannerCYO" + requestId.ToString(); ;
                    cycleRequest.vias = new CycleService.RequestPlace[0];
                    cycleRequest.userPreferences = GetDefaultUserPreferences();
                    cycleRequest.penaltyFunction = GetDefaultPenaltyFunction();

                    if (outArrDep == "d")
                    {
                        cycleRequest.depart = true;
                        cycleRequest.origin.timeDate = outStop.timeDate;
                    }
                    else
                    {
                        cycleRequest.depart = false;
                        cycleRequest.destination.timeDate = outStop.timeDate;
                    }

                    if (!string.IsNullOrEmpty(retArrDep))
                    {
                        cycleReturnRequest = new CycleService.JourneyRequest();
                        cycleReturnRequest.origin = cycleRequest.destination;
                        cycleReturnRequest.destination = cycleRequest.origin;

                        cycleReturnRequest.requestID = "CYR" + requestId.ToString();
                        cycleReturnRequest.language = "en-GB";
                        cycleReturnRequest.sessionID = "BatchJourneyPlannerCYR" + requestId.ToString();
                        cycleReturnRequest.vias = new CycleService.RequestPlace[0];
                        cycleReturnRequest.userPreferences = GetDefaultUserPreferences();
                        cycleReturnRequest.penaltyFunction = GetDefaultPenaltyFunction();

                        if (retArrDep == "d")
                        {
                            cycleReturnRequest.depart = true;
                            cycleReturnRequest.origin.timeDate = retStop.timeDate;
                        }
                        else
                        {
                            cycleReturnRequest.depart = false;
                            cycleReturnRequest.destination.timeDate = retStop.timeDate;
                        }
                    }
                }
            }

            #endregion

            #endregion
        }
        
        /// <summary>
        /// Method make calls to the CJP
        /// </summary>
        /// <param name="jr"></param>
        private void PlanCJPandCycleJourneys()
        {
            #region Date validation
            // Check the out and return dates are not before today and are 
            // before the last day of the month after next
            bool outDateOK = true;
            bool retDateOK = true;
            DateTime mustBeBefore;
            mustBeBefore = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            mustBeBefore = mustBeBefore.AddMonths(3);

            if (!string.IsNullOrEmpty(retArrDep))
            {
                if (retDate.CompareTo(DateTime.Today) < 0)
                {
                    retDateOK = false;
                    if (incPT)
                    {
                        ptReturnError = "Dates in the past cannot be processed. ";
                        ptReturnStatus = (int)BatchDetailStatus.Errored;
                    }
                    if (incCar)
                    {
                        carReturnError = "Dates in the past cannot be processed. ";
                        carReturnStatus = (int)BatchDetailStatus.Errored;
                    }
                    if (incCycle)
                    {
                        cycleReturnError = "Dates in the past cannot be processed. ";
                        cycleReturnStatus = (int)BatchDetailStatus.Errored;
                    }
                }
                else if (retDate.CompareTo(mustBeBefore) > -1)
                {
                    retDateOK = false;
                    if (incPT)
                    {
                        ptReturnError = "Dates too far in the future cannot be processed. ";
                        ptReturnStatus = (int)BatchDetailStatus.Errored;
                    }
                    if (incCar)
                    {
                        carReturnError = "Dates too far in the future cannot be processed. ";
                        carReturnStatus = (int)BatchDetailStatus.Errored;
                    }
                    if (incCycle)
                    {
                        cycleReturnError = "Dates too far in the future cannot be processed. ";
                        cycleReturnStatus = (int)BatchDetailStatus.Errored;
                    }
                }
            }

            if (outDate.CompareTo(DateTime.Today) < 0)
            {
                outDateOK = false;
                if (incPT)
                {
                    ptOutwardError = "Dates in the past cannot be processed. ";
                    ptOutwardStatus = (int)BatchDetailStatus.Errored;
                }
                if (incCar)
                {
                    carOutwardError = "Dates in the past cannot be processed. ";
                    carOutwardStatus = (int)BatchDetailStatus.Errored;
                }
                if (incCycle)
                {
                    cycleOutwardError = "Dates in the past cannot be processed. ";
                    cycleOutwardStatus = (int)BatchDetailStatus.Errored;
                }
            }
            else if (outDate.CompareTo(mustBeBefore) > -1)
            {
                outDateOK = false;
                if (incPT)
                {
                    ptOutwardError = "Dates too far in the future cannot be processed. ";
                    ptOutwardStatus = (int)BatchDetailStatus.Errored;
                }
                if (incCar)
                {
                    carOutwardError = "Dates too far in the future cannot be processed. ";
                    carOutwardStatus = (int)BatchDetailStatus.Errored;
                }
                if (incCycle)
                {
                    cycleOutwardError = "Dates too far in the future cannot be processed. ";
                    cycleOutwardStatus = (int)BatchDetailStatus.Errored;
                }
            }
            #endregion

            #region Invoke CJP and Cycle calls and record results

            #region PT

            // One request at a time 
            if ((ptRequest != null) && outDateOK)
            {
                CjpInterface.JourneyResult result = null;
                ExposedServiceContext context = CreateExposedServiceContext("TransportDirect_EnhancedExposedServices_JourneyPlanner_V1/BatchPublicJourneyOutward");

                try
                {
                    LogRequest(ptModeTypes, ptRequest.requestID, false, ptRequest.sessionID);
                    LogStartEvent(context);
                    TransportDirect.UserPortal.JourneyControl.CJPCall call = new TransportDirect.UserPortal.JourneyControl.CJPCall(ptRequest, false, 1, "BatchJourneyPlanner");

                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.CJP, TDTraceLevel.Verbose, ConvertToXML(ptRequest, ptRequest.requestID), TDTraceLevelOverride.User));
                    }

                    WaitHandle handle = call.InvokeCJP();
                    WaitHandle[] handleList = new WaitHandle[] { handle };
                    int index = WaitHandle.WaitTimeout;

                    while (index == WaitHandle.WaitTimeout)
                    {
                        index = WaitHandle.WaitAny(handleList, 250);
                    }

                    // process the result
                    result = call.CJPResult;
                    LogFinishEvent(context, call.IsSuccessful);
                    LogResponse(result, ptRequest.requestID, false, !call.IsSuccessful, ptRequest.sessionID);
                }
                catch (Exception ex)
                {
                    // log error
                    if (TDTraceSwitch.TraceError)
                        Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, string.Format("Public outward journey failed: {0}", ex.ToString())));

                    ptOutwardStatus = (int)BatchDetailStatus.Errored;
                    LogFinishEvent(context, false);
                    LogResponse(result, ptRequest.requestID, false, true, ptRequest.sessionID);
                }

                ProcessPTResult(RequestType.PTOutward, result);
            }

            if ((ptReturnRequest != null) && retDateOK)
            {
                CjpInterface.JourneyResult result = null;
                ExposedServiceContext context = CreateExposedServiceContext("TransportDirect_EnhancedExposedServices_JourneyPlanner_V1/BatchPublicJourneyReturn");

                try
                {
                    LogRequest(ptModeTypes, ptReturnRequest.requestID, false, ptReturnRequest.sessionID);
                    LogStartEvent(context);
                    TransportDirect.UserPortal.JourneyControl.CJPCall call = new TransportDirect.UserPortal.JourneyControl.CJPCall(ptReturnRequest, false, 2, "BatchJourneyPlanner");

                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.CJP, TDTraceLevel.Verbose, ConvertToXML(call.CJPRequest, call.RequestId), TDTraceLevelOverride.User));
                    }

                    WaitHandle handle = call.InvokeCJP();
                    WaitHandle[] handleList = new WaitHandle[] { handle };
                    int index = WaitHandle.WaitTimeout;

                    while (index == WaitHandle.WaitTimeout)
                    {
                        index = WaitHandle.WaitAny(handleList, 250);
                    }

                    // process the result
                    result = call.CJPResult;
                    LogFinishEvent(context, call.IsSuccessful);
                    LogResponse(result, ptReturnRequest.requestID, false, !call.IsSuccessful, ptReturnRequest.sessionID);
                }
                catch (Exception ex)
                {
                    // log error
                    if (TDTraceSwitch.TraceError)
                        Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, string.Format("Public return journey failed: {0}", ex.ToString())));

                    ptReturnStatus = (int)BatchDetailStatus.Errored;
                    LogFinishEvent(context, false);
                    LogResponse(result, ptReturnRequest.requestID, false, true, ptReturnRequest.sessionID);
                }

                ProcessPTResult(RequestType.PTReturn, result);
            }

            #endregion

            #region Car

            if ((carRequest != null) && outDateOK)
            {
                CjpInterface.JourneyResult result = null;
                ExposedServiceContext context = CreateExposedServiceContext("TransportDirect_EnhancedExposedServices_JourneyPlanner_V1/BatchCarJourneyOutward");

                try
                {
                    LogRequest(carModeTypes, carRequest.requestID, false, carRequest.sessionID);
                    LogStartEvent(context);
                    TransportDirect.UserPortal.JourneyControl.CJPCall call = new TransportDirect.UserPortal.JourneyControl.CJPCall(carRequest, false, 1, "BatchJourneyPlanner");

                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.CJP, TDTraceLevel.Verbose, ConvertToXML(call.CJPRequest, call.RequestId), TDTraceLevelOverride.User));
                    }

                    WaitHandle handle = call.InvokeCJP();
                    WaitHandle[] handleList = new WaitHandle[] { handle };
                    int index = WaitHandle.WaitTimeout;

                    while (index == WaitHandle.WaitTimeout)
                    {
                        index = WaitHandle.WaitAny(handleList, 250);
                    }

                    // process the result
                    result = call.CJPResult;
                    LogFinishEvent(context, call.IsSuccessful);
                    LogResponse(result, carRequest.requestID, false, !call.IsSuccessful, carRequest.sessionID);
                }
                catch (Exception ex)
                {
                    // log error
                    if (TDTraceSwitch.TraceError)
                        Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, string.Format("Car outward journey failed: {0}", ex.ToString())));

                    carOutwardStatus = (int)BatchDetailStatus.Errored;
                    LogFinishEvent(context, false);
                    LogResponse(result, carRequest.requestID, false, true, carRequest.sessionID);
                }

                ProcessCarResult(RequestType.CarOutward, result);
            }

            if ((carReturnRequest != null) && retDateOK)
            {
                CjpInterface.JourneyResult result = null;
                ExposedServiceContext context = CreateExposedServiceContext("TransportDirect_EnhancedExposedServices_JourneyPlanner_V1/BatchCarJourneyReturn");

                try
                {
                    LogRequest(carModeTypes, carReturnRequest.requestID, false, carReturnRequest.sessionID);
                    LogStartEvent(context);
                    TransportDirect.UserPortal.JourneyControl.CJPCall call = new TransportDirect.UserPortal.JourneyControl.CJPCall(carReturnRequest, false, 1, "BatchJourneyPlanner");

                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.CJP, TDTraceLevel.Verbose, ConvertToXML(call.CJPRequest, call.RequestId), TDTraceLevelOverride.User));
                    }

                    WaitHandle handle = call.InvokeCJP();
                    WaitHandle[] handleList = new WaitHandle[] { handle };
                    int index = WaitHandle.WaitTimeout;

                    while (index == WaitHandle.WaitTimeout)
                    {
                        index = WaitHandle.WaitAny(handleList, 250);
                    }

                    // process the result
                    result = call.CJPResult;
                    LogFinishEvent(context, call.IsSuccessful);
                    LogResponse(result, carReturnRequest.requestID, false, !call.IsSuccessful, carReturnRequest.sessionID);
                }
                catch (Exception ex)
                {
                    // log error
                    if (TDTraceSwitch.TraceError)
                        Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, string.Format("Car return journey failed: {0}", ex.ToString())));

                    carReturnStatus = (int)BatchDetailStatus.Errored;
                    LogFinishEvent(context, false);
                    LogResponse(result, carReturnRequest.requestID, false, true, carReturnRequest.sessionID);
                }

                ProcessCarResult(RequestType.CarReturn, result);
            }

            #endregion

            #region Cycle

            if ((cycleRequest != null) && outDateOK)
            {
                CyclePlannerResult result = null;
                ExposedServiceContext context = CreateExposedServiceContext("TransportDirect_EnhancedExposedServices_CycleJourneyPlannerSynchronous_V1/BatchCycleJourneyOutward");

                try
                {
                    LogCycleRequest(cycleRequest.requestID, false, cycleRequest.sessionID);
                    LogStartEvent(context);

                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.CJP, TDTraceLevel.Verbose, ConvertToXML(cycleRequest, cycleRequest.requestID), TDTraceLevelOverride.User));
                    }

                    CyclePlanner caller = new CyclePlanner();
                    result = caller.CycleJourneyPlan(cycleRequest);

                    LogFinishEvent(context, (result != null));
                    LogCycleResponse(result, cycleRequest.requestID, false, (result == null), cycleRequest.sessionID);
                }
                catch (Exception ex)
                {
                    // log error
                    if (TDTraceSwitch.TraceError)
                        Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, string.Format("Cycle outward journey failed: {0}", ex.ToString())));

                    cycleOutwardStatus = (int)BatchDetailStatus.Errored;
                    LogFinishEvent(context, false);
                    LogCycleResponse(result, cycleRequest.requestID, false, true, cycleRequest.sessionID);
                }

                ProcessCycleResult(RequestType.CycleOutward, result);
            }

            if ((cycleReturnRequest != null) && retDateOK)
            {
                CyclePlannerResult result = null;
                ExposedServiceContext context = CreateExposedServiceContext("TransportDirect_EnhancedExposedServices_CycleJourneyPlannerSynchronous_V1/BatchCycleJourneyReturn");

                try
                {
                    LogCycleRequest(cycleReturnRequest.requestID, false, cycleRequest.sessionID);
                    LogStartEvent(context);

                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.CJP, TDTraceLevel.Verbose, ConvertToXML(cycleReturnRequest, cycleReturnRequest.requestID), TDTraceLevelOverride.User));
                    }

                    CyclePlanner caller = new CyclePlanner();
                    result = caller.CycleJourneyPlan(cycleReturnRequest);
                    LogFinishEvent(context, true);
                    LogCycleResponse(result, cycleRequest.requestID, false, false, cycleRequest.sessionID);
                }
                catch (Exception ex)
                {
                    // log error
                    if (TDTraceSwitch.TraceError)
                        Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, string.Format("Cycle return journey failed: {0}", ex.ToString())));

                    cycleReturnStatus = (int)BatchDetailStatus.Errored;
                    LogFinishEvent(context, false);
                    LogCycleResponse(result, cycleRequest.requestID, false, true, cycleRequest.sessionID);
                }

                ProcessCycleResult(RequestType.CycleReturn, result);
            }

            #endregion

            #endregion
        }

        /// <summary>
        /// ITN population for postcodes (should only return one)
        /// </summary>
        /// <param name="easting"></param>
        /// <param name="northing"></param>
        /// <param name="postcode"></param>
        /// <returns>ITN array</returns>
        private CjpInterface.ITN[] GetPostcodeITNs(int easting, int northing, string postcode)
        {
            IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
            string toidPrefix = Properties.Current[TOID_PREFIX];

            QuerySchema gisResult = gisQuery.FindNearestITN(easting,
                                                            northing,
                                                            postcode,
                                                            true);

            // Size toid string array based on result of query.
            string[] toids = new string[gisResult.ITN.Rows.Count];

            // Loop through returned toids and update string array.
            for (int i = 0; i < gisResult.ITN.Rows.Count; i++)
            {
                QuerySchema.ITNRow row = (QuerySchema.ITNRow)gisResult.ITN.Rows[i];
                toids[i] = row.toid;
            }

            if (toidPrefix == null)
            {
                toidPrefix = string.Empty;
            }

            CjpInterface.ITN[] requestRoadPoints = new CjpInterface.ITN[toids.Length];

            for (int i = 0; i < toids.Length; i++)
            {
                requestRoadPoints[i] = new CjpInterface.ITN();

                if ((toidPrefix.Length > 0) && !(toids[i].StartsWith(toidPrefix)))
                {
                    requestRoadPoints[i].TOID = toidPrefix + toids[i];
                }
                else
                {
                    requestRoadPoints[i].TOID = toids[i];
                }

                requestRoadPoints[i].node = false;		// ESRI supplies us with links, not nodes
            }

            return requestRoadPoints;
        }
        
        /// <summary>
        /// Get all the TOIDs for a grid reference
        /// </summary>
        /// <param name="easting"></param>
        /// <param name="northing"></param>
        /// <returns></returns>
        private CjpInterface.ITN[] GetITNs(int easting, int northing)
        {
            IGisQuery gq = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
            string toidPrefix = Properties.Current[TOID_PREFIX];

            QuerySchema gisResult = gq.FindNearestITNs(easting, northing);

            // Resize toid string array based on result of query.
            string[] toids = new string[gisResult.ITN.Rows.Count];

            // Loop through returned toids and update string array.
            for (int i = 0; i < gisResult.ITN.Rows.Count; i++)
            {
                QuerySchema.ITNRow row = (QuerySchema.ITNRow)gisResult.ITN.Rows[i];
                toids[i] = row.toid;
            }

            // ESRI's queries often return large numbers of duplicate 
            // TOIDs for an OSGR - this code removes duplicates ...
            SortedList sortedToids = new SortedList();

            for (int i = 0; i < toids.Length; i++)
            {
                try
                {
                    sortedToids.Add(toids[i], toids[i]);
                }
                catch (ArgumentException)
                {
                    // nothing to do - just means it's a duplicate
                }
            }

            IList editedToidList = sortedToids.GetValueList();

            if (toidPrefix == null)
            {
                toidPrefix = string.Empty;
            }

            CjpInterface.ITN[] requestRoadPoints = new CjpInterface.ITN[editedToidList.Count];

            for (int i = 0; i < editedToidList.Count; i++)
            {
                string currentToid = (string)editedToidList[i];

                requestRoadPoints[i] = new CjpInterface.ITN();

                if ((toidPrefix.Length > 0) && !(currentToid.StartsWith(toidPrefix)))
                {
                    requestRoadPoints[i].TOID = toidPrefix + currentToid;
                }
                else
                {
                    requestRoadPoints[i].TOID = currentToid;
                }

                requestRoadPoints[i].node = false;		// ESRI supplies us with links, not nodes
            }

            return requestRoadPoints;
        }

        /// <summary>
        /// Generate a landing page url for the cycle journey
        /// </summary>
        /// <param name="outwardOrigin">origin</param>
        /// <param name="originType">origin type c/n/p</param>
        /// <param name="outwardDestination">destination</param>
        /// <param name="destType">destination type c/n/p</param>
        private void GenerateCycleURL(TransportDirect.JourneyPlanning.CJPInterface.RequestPlace outwardOrigin, string originType, TransportDirect.JourneyPlanning.CJPInterface.RequestPlace outwardDestination, string destType)
        {
            // Construct the URL
            StringBuilder targetUrl = new StringBuilder();
            targetUrl.Append("?");

            // Origin location
            string locationType = string.Empty;
            string locationData = string.Empty;
            string locationText = string.Empty;

            if (originType == "p")
            {
                locationType = LandingPageHelperConstants.ValueLocationTypePostcode;
                locationData = outwardOrigin.givenName;
                locationText = outwardOrigin.givenName;
            }
            else if (originType == "c")
            {
                locationType = LandingPageHelperConstants.ValueLocationTypeOSGR;
                locationData = outwardOrigin.coordinate.easting.ToString() + "," + outwardOrigin.coordinate.northing.ToString();
                locationText = "OS Grid Reference " + locationData;
            }
            else if (originType == "n")
            {
                locationType = LandingPageHelperConstants.ValueLocationTypeNaPTAN;
                locationData = origin.Trim().ToUpper();
                locationText = outwardOrigin.givenName;
            }
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginType, locationType, false);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginData, locationData, false);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginText, locationText, false);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterPartnerId, LANDINGPAGE_PARTNER_ID, false);

            // Destination location
            if (destType == "p")
            {
                locationType = LandingPageHelperConstants.ValueLocationTypePostcode;
                locationData = outwardDestination.givenName;
                locationText = outwardDestination.givenName;
            }
            else if (destType == "c")
            {
                locationType = LandingPageHelperConstants.ValueLocationTypeOSGR;
                locationData = outwardDestination.coordinate.easting.ToString() + "," + outwardDestination.coordinate.northing.ToString();
                locationText = "OS Grid Reference " + locationData;
            }
            else if (destType == "n")
            {
                locationType = LandingPageHelperConstants.ValueLocationTypeNaPTAN;
                locationData = dest.Trim().ToUpper();
                locationText = outwardDestination.givenName;
            }
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationType, locationType, false);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationData, locationData, false);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationText, locationText, false);

            // Miscellaneous other parameters
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOutwardDepartArrive, LandingPageHelperConstants.ValueDateTypeDepartAt, false);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeCycle, false);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterAutoPlan, LandingPageHelperConstants.ValueAutoPlanOff, true);

            cycleSummary.LandingUrL = targetUrl.ToString();
        }

        /// <summary>
        /// Generate a landing page URL for PT and car journeys
        /// </summary>
        /// <param name="outwardOrigin">origin</param>
        /// <param name="originType">origin type n/c/p</param>
        /// <param name="outwardDestination">destination</param>
        /// <param name="destType">destination type n/c/p</param>
        private void GenerateDoorToDoorURL(TransportDirect.JourneyPlanning.CJPInterface.RequestPlace outwardOrigin, string originType, TransportDirect.JourneyPlanning.CJPInterface.RequestPlace outwardDestination, string destType)
        {
            StringBuilder targetUrl = new StringBuilder();
            targetUrl.Append("?");

            // Add origin data along with partner Id in the string
            string locationType = string.Empty;
            string locationData = string.Empty;
            string locationText = string.Empty;

            if (originType == "p")
            {
                locationType = LandingPageHelperConstants.ValueLocationTypePostcode;
                locationData = outwardOrigin.givenName;
                locationText = outwardOrigin.givenName;
            }
            else if (originType == "c")
            {
                locationType = LandingPageHelperConstants.ValueLocationTypeOSGR;
                locationData = outwardOrigin.coordinate.easting.ToString() + "," + outwardOrigin.coordinate.northing.ToString();
                locationText = "OS Grid Reference " + locationData;
            }
            else if (originType == "n")
            {
                locationType = LandingPageHelperConstants.ValueLocationTypeNaPTAN;
                locationData = origin.Trim().ToUpper();
                locationText = outwardOrigin.givenName;
            }
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginType, locationType, false);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginData, locationData, false);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginText, locationText, false);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterPartnerId, LANDINGPAGE_PARTNER_ID, false);

            // Destination location
            if (destType == "p")
            {
                locationType = LandingPageHelperConstants.ValueLocationTypePostcode;
                locationData = outwardDestination.givenName;
                locationText = outwardDestination.givenName;
            }
            else if (destType == "c")
            {
                locationType = LandingPageHelperConstants.ValueLocationTypeOSGR;
                locationData = outwardDestination.coordinate.easting.ToString() + "," + outwardDestination.coordinate.northing.ToString();
                locationText = "OS Grid Reference " + locationData;
            }
            else if (destType == "n")
            {
                locationType = LandingPageHelperConstants.ValueLocationTypeNaPTAN;
                locationData = dest.Trim().ToUpper();
                locationText = outwardDestination.givenName;
            }
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationType, locationType, false);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationData, locationData, false);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationText, locationText, false);

            // Miscellaneous other parameters
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOutwardDepartArrive, LandingPageHelperConstants.ValueDateTypeDepartAt, false);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeMultimodal, false);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterAutoPlan, LandingPageHelperConstants.ValueAutoPlanOff, false);

            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterCarDefault, incCar.ToString(), true);

            ptSummary.LandingUrL = targetUrl.ToString();
        }

        /// <summary>
        /// Adds a querystring parameter to the URL contained in the string builder.
        /// </summary>
        /// <param name="url">StringBuilder containing the Url. This should currently end in either ? or &</param>
        /// <param name="parameter">The name of the parameter to add.</param>
        /// <param name="value">The value of the parameter to add.</param>
        /// <param name="isFinalParameter">Denotes whether is final parameter - if so no trailing amprasand is required</param>
        private void AddParameterToUrl(StringBuilder url, string parameter, string value, bool isFinalParameter)
        {
            url.Append(parameter);
            url.Append("=");
            string s = HttpUtility.UrlEncode(value);
            url.Append(s);

            if (!isFinalParameter)
            {
                url.Append("&amp;");
            }
        }

        /// <summary>
        /// Update the database with the journey results
        /// </summary>
        private void UpdateBatchDetailRecord()
        {
            #region Call DB
            try
            {
			    // Create hashtables containing parameters and data types for the stored procs
			    Hashtable parameterValues = new Hashtable(14);
			    Hashtable parameterTypes = new Hashtable(14);

                // The batch detail id
                parameterValues.Add("@BatchDetailId", requestId);
                parameterTypes.Add("@BatchDetailId", SqlDbType.Int);

                // The PT summary
                parameterValues.Add("@PublicJourneyResultSummary", ptSummary.ToSummaryRecord());
                parameterTypes.Add("@PublicJourneyResultSummary", SqlDbType.NVarChar);

                // The PT details
                if (ptDetails == null)
                {
                    parameterValues.Add("@PublicJourneyResultDetails", "<ROOT></ROOT>");
                }
                else
                {
                    parameterValues.Add("@PublicJourneyResultDetails", ptDetails.OuterXml);
                }
                parameterTypes.Add("@PublicJourneyResultDetails", SqlDbType.Xml);

                // The PT status
                parameterValues.Add("@PublicOutwardJourneyStatus", ptOutwardStatus);
                parameterTypes.Add("@PublicOutwardJourneyStatus", SqlDbType.Int);

                // The PT status
                parameterValues.Add("@PublicReturnJourneyStatus", ptReturnStatus);
                parameterTypes.Add("@PublicReturnJourneyStatus", SqlDbType.Int);

                // The Car summary
                parameterValues.Add("@CarResultSummary", carSummary.ToSummaryRecord());
                parameterTypes.Add("@CarResultSummary", SqlDbType.NVarChar);

                // The Car status
                parameterValues.Add("@CarOutwardStatus", carOutwardStatus);
                parameterTypes.Add("@CarOutwardStatus", SqlDbType.Int);

                // The Car status
                parameterValues.Add("@CarReturnStatus", carReturnStatus);
                parameterTypes.Add("@CarReturnStatus", SqlDbType.Int);

                // The Cycle summary
                parameterValues.Add("@CycleResultSummary", cycleSummary.ToSummaryRecord());
                parameterTypes.Add("@CycleResultSummary", SqlDbType.NVarChar);

                // The Cycle status
                parameterValues.Add("@CycleOutwardStatus", cycleOutwardStatus);
                parameterTypes.Add("@CycleOutwardStatus", SqlDbType.Int);

                // The Cycle status
                parameterValues.Add("@CycleReturnStatus", cycleReturnStatus);
                parameterTypes.Add("@CycleReturnStatus", SqlDbType.Int);

                // The PT outward error
                parameterValues.Add("@PublicOutwardErrorMessage", ptOutwardError);
                parameterTypes.Add("@PublicOutwardErrorMessage", SqlDbType.NVarChar);

                // The Car outward error
                parameterValues.Add("@CarOutwardErrorMessage", carOutwardError);
                parameterTypes.Add("@CarOutwardErrorMessage", SqlDbType.NVarChar);

                // The Cycle outward error
                parameterValues.Add("@CycleOutwardErrorMessage", cycleOutwardError);
                parameterTypes.Add("@CycleOutwardErrorMessage", SqlDbType.NVarChar);

                // The PT outward error
                parameterValues.Add("@PublicReturnErrorMessage", ptReturnError);
                parameterTypes.Add("@PublicReturnErrorMessage", SqlDbType.NVarChar);

                // The Car outward error
                parameterValues.Add("@CarReturnErrorMessage", carReturnError);
                parameterTypes.Add("@CarReturnErrorMessage", SqlDbType.NVarChar);

                // The Cycle outward error
                parameterValues.Add("@CycleReturnErrorMessage", cycleReturnError);
                parameterTypes.Add("@CycleReturnErrorMessage", SqlDbType.NVarChar);

                //Use the SQL Helper class
                using (SqlHelper sqlHelper = new SqlHelper())
                {
                    sqlHelper.ConnOpen(SqlHelperDatabase.BatchJourneyPlannerDB);
                    sqlHelper.Execute("UpdateBatchRequestDetail", parameterValues, parameterTypes);
                    sqlHelper.ConnClose();
                }
            }
            catch (Exception ex)
            {
                if (TDTraceSwitch.TraceError)
                    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, string.Format("Failed to update BatchRequest details, request id {0} with: {1}", requestId, ex.ToString())));
            }
            #endregion
        }

        #region Process results
        /// <summary>
        /// Process public transport result
        /// </summary>
        /// <param name="requestType">request type</param>
        /// <param name="result">Journey results</param>
        private void ProcessPTResult(RequestType requestType, CjpInterface.JourneyResult result)
        {
            try
            {
                if (result == null)
                {
                    // report the error
                    if (requestType == RequestType.PTOutward)
                    {
                        ptOutwardError = "No outward public transport journey found. ";
                        ptOutwardStatus = (int)BatchDetailStatus.Errored;
                    }
                    else
                    {
                        ptReturnError = "No return public transport journey found. ";
                        ptReturnStatus = (int)BatchDetailStatus.Errored;
                    }
                }
                else if (result.publicJourneys == null)
                {
                    // report the error
                    if (requestType == RequestType.PTOutward)
                    {
                        ptOutwardError = "No outward public transport journey found. ";
                        ptOutwardStatus = (int)BatchDetailStatus.Errored;
                    }
                    else
                    {
                        ptReturnError = "No return public transport journey found. ";
                        ptReturnStatus = (int)BatchDetailStatus.Errored;
                    }
                }
                else if (result.publicJourneys.Length == 0)
                {
                    // report the error
                    if (requestType == RequestType.PTOutward)
                    {
                        ptOutwardError = "No outward public transport journey found. ";
                        ptOutwardStatus = (int)BatchDetailStatus.Errored;
                    }
                    else
                    {
                        ptReturnError = "No return public transport journey found. ";
                        ptReturnStatus = (int)BatchDetailStatus.Errored;
                    }
                }
                else
                {
                    // always do statistics
                    JourneyEmissionsCalculator calc = new JourneyEmissionsCalculator();
                    decimal emissions = 0;
                    bool co2failed = false;
                    string modes = result.publicJourneys[0].legs[0].mode.ToString();
                    for (int i = 0; i < result.publicJourneys[0].legs.Length; i++)
                    {
                        if (!modes.Contains(result.publicJourneys[0].legs[i].mode.ToString()))
                        {
                            modes += "|" + result.publicJourneys[0].legs[i].mode.ToString();
                        }

                        // CO2 - work out distance
                        int eastDistance = 0;
                        int northDistance = 0;
                        
                        // Occasionally the traveline does not provide coordinate information
                        if (result.publicJourneys[0].legs[i].alight.stop.coordinate == null || result.publicJourneys[0].legs[i].board.stop.coordinate == null)
                        {
                            // If we have a NAPTAN try to get it from the cache
                            CjpInterface.Coordinate board = result.publicJourneys[0].legs[i].board.stop.coordinate;
                            if (board == null)
                            {
                                NaptanCacheEntry entry = NaptanLookup.Get(result.publicJourneys[0].legs[i].board.stop.NaPTANID, "Naptan");

                                if (entry != null)
                                {
                                    board = new CjpInterface.Coordinate();
                                    board.easting = entry.OSGR.Easting;
                                    board.northing = entry.OSGR.Northing;
                                }
                                else
                                {
                                    co2failed = true;
                                }
                            }

                            CjpInterface.Coordinate alight = result.publicJourneys[0].legs[i].alight.stop.coordinate;
                            if (alight == null && !co2failed)
                            {
                                NaptanCacheEntry entry = NaptanLookup.Get(result.publicJourneys[0].legs[i].alight.stop.NaPTANID, "Naptan");

                                if (entry != null)
                                {
                                    alight = new CjpInterface.Coordinate();
                                    alight.easting = entry.OSGR.Easting;
                                    alight.northing = entry.OSGR.Northing;
                                }
                                else
                                {
                                    co2failed = true;
                                }
                            }

                            if (!co2failed)
                            {
                                eastDistance = alight.easting - board.easting;
                                northDistance = alight.northing - board.northing;
                            }
                        }
                        else
                        {
                            eastDistance = result.publicJourneys[0].legs[i].alight.stop.coordinate.easting - result.publicJourneys[0].legs[i].board.stop.coordinate.easting;
                            northDistance = result.publicJourneys[0].legs[i].alight.stop.coordinate.northing - result.publicJourneys[0].legs[i].board.stop.coordinate.northing;
                        }

                        if (co2failed)
                        {
                            break;
                        }

                        try
                        {
                            long east = eastDistance;
                            long north = northDistance;
                            decimal distance = decimal.Parse(Math.Sqrt((east * east) + (north * north)).ToString());
                            emissions += calc.GetEmissions(result.publicJourneys[0].legs[i].mode, distance);
                        }
                        catch (Exception)
                        {
                            co2failed = true;
                            break;
                        }
                    }

                    if (requestType == RequestType.PTOutward)
                    {
                        ptSummary.OutwardModes = modes;
                        ptSummary.OutwardJourneyTime = new TimeSpan(result.publicJourneys[0].GetArriveTime().Ticks - result.publicJourneys[0].GetDepartTime().Ticks);

                        if (!co2failed)
                        {
                            ptSummary.OutwardCO2 = emissions.ToString();
                        }
                        else
                        {
                            ptOutwardError = "PT Outward CO2 could not be calculated. ";
                        }

                        // Count up the number of legs that are not walking legs
                        int changes = 0;
                        foreach (Leg leg in result.publicJourneys[0].legs)
                        {
                            if (leg.mode != ModeType.Walk)
                            {
                                changes++;
                            }
                        }

                        if (changes == 0)
                        {
                            ptSummary.OutwardChanges = "0";
                        }
                        else
                        {
                            ptSummary.OutwardChanges = (changes - 1).ToString();
                        }
                    }
                    else if (requestType == RequestType.PTReturn)
                    {
                        ptSummary.ReturnModes = modes;
                        ptSummary.ReturnJourneyTime = new TimeSpan(result.publicJourneys[0].GetArriveTime().Ticks - result.publicJourneys[0].GetDepartTime().Ticks);

                        if (!co2failed)
                        {
                            ptSummary.ReturnCO2 = emissions.ToString();
                        }
                        else
                        {
                            ptReturnError = "PT Return CO2 could not be calculated. ";
                        }

                        // Count up the number of legs that are not walking legs
                        int changes = 0;
                        foreach (Leg leg in result.publicJourneys[0].legs)
                        {
                            if (leg.mode != ModeType.Walk)
                            {
                                changes++;
                            }
                        }

                        if (changes == 0)
                        {
                            ptSummary.ReturnChanges = "0";
                        }
                        else
                        {
                            ptSummary.ReturnChanges = (changes - 1).ToString();
                        }
                    }

                    // details
                    if (incDetails)
                    {
                        if (ptDetails == null)
                        {
                            ptDetails = new XmlDocument();
                            XmlDeclaration dec = ptDetails.CreateXmlDeclaration("1.0", "utf-16", null);
                            XmlElement root = ptDetails.CreateElement("ROOT");
                            ptDetails.InsertBefore(dec, ptDetails.DocumentElement);
                            ptDetails.AppendChild(root);
                        }

                        // Expect 2 results but record whatever is returned
                        foreach (CjpInterface.PublicJourney journey in result.publicJourneys)
                        {
                            XmlElement journeyElement = ptDetails.CreateElement("Journey");
                            XmlAttribute direction = ptDetails.CreateAttribute("Direction");
                            if (requestType == RequestType.PTOutward)
                            {
                                direction.Value = "Outward";
                            }
                            else
                            {
                                direction.Value = "Return";
                            }
                            journeyElement.Attributes.Append(direction);

                            // Journey details
                            string date = string.Empty;
                            string time = string.Empty;
                            string arrDep = string.Empty;
                            if (requestType == RequestType.PTOutward)
                            {
                                date = outDate.ToString("yyyyMMdd");
                                DateTime temp = outDate.Add(outTime);
                                time = temp.ToString("HH:mm");
                                arrDep = outArrDep;
                            }
                            else
                            {
                                date = retDate.ToString("yyyyMMdd");
                                DateTime temp = retDate.Add(retTime);
                                time = temp.ToString("HH:mm");
                                arrDep = retArrDep;
                            }

                            if (time.Length == 4)
                            {
                                time = "0" + time;
                            }

                            XmlElement journeyDate = ptDetails.CreateElement("JourneyDate");
                            journeyDate.AppendChild(ptDetails.CreateTextNode(date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2)));
                            journeyElement.AppendChild(journeyDate);

                            XmlElement journeyTime = ptDetails.CreateElement("JourneyTime");
                            journeyTime.AppendChild(ptDetails.CreateTextNode(time.Substring(0, 2) + ":" + time.Substring(2, 2) + ":00"));
                            journeyElement.AppendChild(journeyTime);

                            XmlElement journeyDuration = ptDetails.CreateElement("JourneyDuration");
                            TimeSpan span = new TimeSpan(journey.legs[journey.legs.Length - 1].alight.arriveTime.Ticks - journey.legs[0].board.departTime.Ticks);
                            journeyDuration.AppendChild(ptDetails.CreateTextNode(span.Hours + ":" + span.Minutes));
                            journeyElement.AppendChild(journeyDuration);

                            XmlElement arriveDepart = ptDetails.CreateElement("ArriveDepart");
                            arriveDepart.AppendChild(ptDetails.CreateTextNode(arrDep == "a" ? "Arrive" : "Depart"));
                            journeyElement.AppendChild(arriveDepart);

                            for (int i = 0; i < journey.legs.Length; i++)
                            {
                                XmlElement legElement = ptDetails.CreateElement("Leg");

                                XmlElement from = ptDetails.CreateElement("LegOrigin");
                                from.AppendChild(ptDetails.CreateTextNode(journey.legs[i].board.stop.name));
                                legElement.AppendChild(from);

                                XmlElement to = ptDetails.CreateElement("LegDestination");
                                to.AppendChild(ptDetails.CreateTextNode(journey.legs[i].alight.stop.name));
                                legElement.AppendChild(to);

                                XmlElement mode = ptDetails.CreateElement("LegMode");
                                mode.AppendChild(ptDetails.CreateTextNode(journey.legs[i].mode.ToString()));
                                legElement.AppendChild(mode);

                                if (journey.legs[i] is TimedLeg)
                                {
                                    XmlElement depart = ptDetails.CreateElement("LegDepart");
                                    depart.AppendChild(ptDetails.CreateTextNode(journey.legs[i].board.departTime.ToString("HH:mm")));
                                    legElement.AppendChild(depart);
                                }
                                else if (journey.legs[i] is ContinuousLeg)
                                {
                                    // Continuous leg - only have a start time if it's the first leg
                                    if (i == 0)
                                    {
                                        XmlElement depart = ptDetails.CreateElement("LegDepart");
                                        depart.AppendChild(ptDetails.CreateTextNode(journey.legs[i].board.departTime.ToString("HH:mm")));
                                        legElement.AppendChild(depart);
                                    }
                                }
                                else if (journey.legs[i] is FrequencyLeg)
                                {
                                    // Frequency leg - only have frequency of departures
                                    XmlElement frequency = ptDetails.CreateElement("LegFrequency");
                                    frequency.AppendChild(ptDetails.CreateTextNode(((FrequencyLeg)journey.legs[i]).frequency.ToString()));
                                    legElement.AppendChild(frequency);
                                }
                                else
                                {
                                    XmlElement depart = ptDetails.CreateElement("LegDepart");
                                    depart.AppendChild(ptDetails.CreateTextNode(journey.legs[i].board.departTime.ToString("HH:mm")));
                                    legElement.AppendChild(depart);
                                }

                                XmlElement arrive = ptDetails.CreateElement("LegArrive");
                                if (journey.legs[i] is TimedLeg)
                                {
                                    arrive.AppendChild(ptDetails.CreateTextNode(journey.legs[i].alight.arriveTime.ToString("HH:mm")));
                                }
                                else if (journey.legs[i] is ContinuousLeg)
                                {
                                    // Continuous leg, only have an arrive time if it's the last leg
                                    if (i == journey.legs.Length - 1)
                                    {
                                        arrive.AppendChild(ptDetails.CreateTextNode(journey.legs[i].alight.arriveTime.ToString("HH:mm")));
                                    }
                                }
                                legElement.AppendChild(arrive);

                                XmlElement instructions = ptDetails.CreateElement("LegInstructions");
                                instructions.AppendChild(ptDetails.CreateTextNode(GetInstructions(journey.legs[i])));
                                legElement.AppendChild(instructions);

                                XmlElement duration = ptDetails.CreateElement("LegDuration");
                                if (journey.legs[i] is TimedLeg)
                                {
                                    duration.AppendChild(ptDetails.CreateTextNode(new TimeSpan(journey.legs[i].alight.arriveTime.Ticks - journey.legs[i].board.departTime.Ticks).TotalMinutes.ToString()));
                                }
                                else if (journey.legs[i] is ContinuousLeg)
                                {
                                    duration.AppendChild(ptDetails.CreateTextNode(((ContinuousLeg)journey.legs[i]).typicalDuration.ToString()));
                                }
                                else if (journey.legs[i] is FrequencyLeg)
                                {
                                    duration.AppendChild(ptDetails.CreateTextNode(((FrequencyLeg)journey.legs[i]).typicalDuration.ToString()));
                                }
                                legElement.AppendChild(duration);

                                journeyElement.AppendChild(legElement);
                            }

                            ptDetails.DocumentElement.AppendChild(journeyElement);
                        }
                    }

                    if (requestType == RequestType.PTOutward)
                    {
                        ptOutwardStatus = (int)BatchDetailStatus.Complete;
                    }
                    else
                    {
                        ptReturnStatus = (int)BatchDetailStatus.Complete;
                    }
                }
            }
            catch (Exception)
            {
                if (requestType == RequestType.PTOutward)
                {
                    ptOutwardError = "No outward public transport journey found. ";
                    ptOutwardStatus = (int)BatchDetailStatus.Errored;
                }
                else
                {
                    ptReturnError = "No return public transport journey found. ";
                    ptReturnStatus = (int)BatchDetailStatus.Errored;
                }
            }
        }

        /// <summary>
        /// Process car journey result
        /// </summary>
        /// <param name="requestType">request types</param>
        /// <param name="result">journey result</param>
        private void ProcessCarResult(RequestType requestType, CjpInterface.JourneyResult result)
        {
            try
            {
                if (result == null)
                {
                    // report the error
                    if (requestType == RequestType.CarOutward)
                    {
                        carOutwardError = "No outward car journey found. ";
                        carOutwardStatus = (int)BatchDetailStatus.Errored;
                    }
                    else
                    {
                        carReturnError = "No return car journey found. ";
                        carReturnStatus = (int)BatchDetailStatus.Errored;
                    }
                }
                else if (result.privateJourneys == null)
                {
                    // report the error
                    if (requestType == RequestType.CarOutward)
                    {
                        carOutwardError = "No outward car journey found. ";
                        carOutwardStatus = (int)BatchDetailStatus.Errored;
                    }
                    else
                    {
                        carReturnError = "No return car journey found. ";
                        carReturnStatus = (int)BatchDetailStatus.Errored;
                    }
                }
                else if (result.privateJourneys.Length == 0)
                {
                    // report the error
                    if (requestType == RequestType.CarOutward)
                    {
                        carOutwardError = "No outward car journey found. ";
                        carOutwardStatus = (int)BatchDetailStatus.Errored;
                    }
                    else
                    {
                        carReturnError = "No return car journey found. ";
                        carReturnStatus = (int)BatchDetailStatus.Errored;
                    }
                }
                else
                {
                    // only statistics required for car journeys
                    if (requestType == RequestType.CarOutward)
                    {
                        TimeSpan outwardTime = new TimeSpan();
                        int distance = 0;
                        foreach (CjpInterface.Section section in result.privateJourneys[0].sections)
                        {
                            outwardTime = outwardTime.Add(new TimeSpan(section.time.Ticks));

                            if (section is CjpInterface.DriveSection)
                            {
                                distance += ((CjpInterface.DriveSection)section).distance;
                            }
                        }
                        decimal kilometres = decimal.Round(decimal.Parse((distance / 1609.344).ToString()), 1);
                        carSummary.OutwardJourneyTime = outwardTime;
                        carSummary.OutwardDistance = kilometres.ToString();

                        // CO2
                        JourneyEmissionsCalculator calc = new JourneyEmissionsCalculator();
                        carSummary.OutwardCO2 = calc.GetEmissions(ModeType.Car, distance).ToString();
                    }
                    else if (requestType == RequestType.CarReturn)
                    {
                        int distance = 0;
                        TimeSpan returnTime = new TimeSpan();
                        foreach (CjpInterface.Section section in result.privateJourneys[0].sections)
                        {
                            returnTime = returnTime.Add(new TimeSpan(section.time.Ticks));

                            if (section is CjpInterface.DriveSection)
                            {
                                distance += ((CjpInterface.DriveSection)section).distance;
                            }
                        }
                        decimal miles = decimal.Round(decimal.Parse((distance / 1609.344).ToString()), 1);
                        carSummary.ReturnJourneyTime = returnTime;
                        carSummary.ReturnDistance = miles.ToString();

                        // CO2
                        JourneyEmissionsCalculator calc = new JourneyEmissionsCalculator();
                        carSummary.ReturnCO2 = calc.GetEmissions(ModeType.Car, distance).ToString();
                    }

                    if (requestType == RequestType.CarOutward)
                    {
                        carOutwardStatus = (int)BatchDetailStatus.Complete;
                    }
                    else
                    {
                        carReturnStatus = (int)BatchDetailStatus.Complete;
                    }
                }
            }
            catch (Exception)
            {
                if (requestType == RequestType.CarOutward)
                {
                    carOutwardError = "No outward car journey found. ";
                    carOutwardStatus = (int)BatchDetailStatus.Errored;
                }
                else
                {
                    carReturnError = "No return car journey found. ";
                    carReturnStatus = (int)BatchDetailStatus.Errored;
                }
            }
        }

        /// <summary>
        /// Process cycle journey result
        /// </summary>
        /// <param name="requestType">request type</param>
        /// <param name="result">journey result</param>
        private void ProcessCycleResult(RequestType requestType, CyclePlannerResult result)
        {
            try
            {
                CycleService.JourneyResult journeyResult;
                int distance = 0;

                try
                {
                    journeyResult = (CycleService.JourneyResult)result;
                }
                catch (Exception)
                {
                    if (requestType == RequestType.CycleOutward)
                    {
                        cycleOutwardError = "No outward cycle journey found. ";
                        cycleOutwardStatus = (int)BatchDetailStatus.Errored;
                    }
                    else
                    {
                        cycleReturnError = "No return cycle journey found. ";
                        cycleReturnStatus = (int)BatchDetailStatus.Errored;
                    }
                    return;
                }

                if (journeyResult == null)
                {
                    // report the error
                    if (requestType == RequestType.CycleOutward)
                    {
                        cycleOutwardError = "No outward cycle journey found. ";
                        cycleOutwardStatus = (int)BatchDetailStatus.Errored;
                    }
                    else
                    {
                        cycleReturnError = "No return cycle journey found. ";
                        cycleReturnStatus = (int)BatchDetailStatus.Errored;
                    }
                }
                else if (journeyResult.journeys == null)
                {
                    // report the error
                    if (requestType == RequestType.CycleOutward)
                    {
                        cycleOutwardError = "No outward cycle journey found. ";
                        cycleOutwardStatus = (int)BatchDetailStatus.Errored;
                    }
                    else
                    {
                        cycleReturnError = "No return cycle journey found. ";
                        cycleReturnStatus = (int)BatchDetailStatus.Errored;
                    }
                }
                else if (journeyResult.journeys.Length == 0)
                {
                    // report the error
                    if (requestType == RequestType.CycleOutward)
                    {
                        cycleOutwardError = "No outward cycle journey found. ";
                        cycleOutwardStatus = (int)BatchDetailStatus.Errored;
                    }
                    else
                    {
                        cycleReturnError = "No return cycle journey found. ";
                        cycleReturnStatus = (int)BatchDetailStatus.Errored;
                    }
                }
                else
                {
                    // only statistics required for cycle journeys
                    if (requestType == RequestType.CycleOutward)
                    {
                        TimeSpan outwardTime = new TimeSpan();
                        foreach (CycleService.Section section in journeyResult.journeys[0].sections)
                        {
                            outwardTime = outwardTime.Add(new TimeSpan(section.time.Ticks));

                            if (section is CycleService.DriveSection)
                            {
                                CycleService.DriveSection drive = (CycleService.DriveSection)section;
                                distance += drive.distance;
                            }
                        }
                        decimal kilometres = decimal.Round(decimal.Parse((distance / 1609.344).ToString()), 1);
                        cycleSummary.OutwardJourneyTime = outwardTime;
                        cycleSummary.OutwardDistance = kilometres.ToString();
                    }
                    else if (requestType == RequestType.CycleReturn)
                    {
                        TimeSpan returnTime = new TimeSpan();
                        foreach (CycleService.Section section in journeyResult.journeys[0].sections)
                        {
                            returnTime = returnTime.Add(new TimeSpan(section.time.Ticks));

                            if (section is CycleService.DriveSection)
                            {
                                CycleService.DriveSection drive = (CycleService.DriveSection)section;
                                distance += drive.distance;
                            }
                        }
                        decimal kilometres = decimal.Round(decimal.Parse((distance / 1609.344).ToString()), 1);
                        cycleSummary.ReturnJourneyTime = returnTime;
                        cycleSummary.ReturnDistance = kilometres.ToString();
                    }

                    if (requestType == RequestType.CycleOutward)
                    {
                        cycleOutwardStatus = (int)BatchDetailStatus.Complete;
                    }
                    else
                    {
                        cycleReturnStatus = (int)BatchDetailStatus.Complete;
                    }
                }
            }
            catch (Exception)
            {
                if (requestType == RequestType.CycleOutward)
                {
                    cycleOutwardError = "No outward cycle journey found. ";
                    cycleOutwardStatus = (int)BatchDetailStatus.Errored;
                }
                else
                {
                    cycleReturnError = "No return cycle journey found. ";
                    cycleReturnStatus = (int)BatchDetailStatus.Errored;
                }
            }
        }
        #endregion

        /// <summary>
        /// Produce instruction text for a leg
        /// </summary>
        /// <param name="leg">the leg</param>
        /// <returns>the instructions</returns>
        private string GetInstructions(Leg leg)
        {
            string description = String.Empty;

            if (leg != null)
            {
                // Currently do not have use cycle
                if (leg.mode == ModeType.Cycle)
                    description = String.Empty;

                // Check to see if walk leg
                if (leg.mode == ModeType.Walk)
                {
                    // Get resource strings
                    description = "Walk to " + leg.alight.stop.name;
                }

                if (leg.mode == ModeType.Bus || leg.mode == ModeType.Coach || leg.mode == ModeType.Drt || leg.mode == ModeType.Ferry || leg.mode == ModeType.Metro || leg.mode == ModeType.Rail || leg.mode == ModeType.RailReplacementBus || leg.mode == ModeType.Tram || leg.mode == ModeType.Underground)
                {
                    if (leg.services.Length > 0)
                    {
                        description = "Take " + leg.services[0].operatorName;

                        if ((leg.services[0].serviceNumber != null) && (leg.services[0].serviceNumber.Length > 0))
                        {
                            if (leg.mode != ModeType.Rail)
                            {
                                description += "/" + leg.services[0].serviceNumber;
                            }
                        }

                        if ((leg.services[0].destinationBoard != null) && (leg.services[0].destinationBoard.Length > 0))
                        {
                            description += " towards " + leg.services[0].destinationBoard;
                        }
                        else if ((leg.destination != null) && (leg.destination.stop != null)
                                    && (leg.destination.stop.name != null) && (leg.destination.stop.name.Length > 0))
                        {
                            description += " towards " + leg.destination.stop.name;
                        }
                    }
                }

                if (leg.mode == ModeType.Taxi)
                {
                    description = "Take a taxi to " + leg.alight.stop.name;
                }
            }

            // Now output the description 
            return description;

        }

        /// <summary>
        /// Add common properties to journey request
        /// </summary>
        /// <param name="request">the journey request</param>
        /// <returns>the journey request</returns>
        private CjpInterface.JourneyRequest AddStandardProperties(CjpInterface.JourneyRequest request)
        {
            request.language = "en-GB";
            request.parkNRide = false;
            request.privateParameters = privateParameters;
            request.operatorFilter = null;
            request.referenceTransaction = false;
            request.secondaryModeFilter = null;
            request.serviceFilter = null;
            request.userType = 0;

            return request;
        }

        /// <summary>
        /// Gets the Default values of cycle user preferences
        /// </summary>
        /// <returns></returns>
        private static UserPreference[] GetDefaultUserPreferences()
        {
            Dictionary<int, TransportDirect.UserPortal.CyclePlannerControl.TDCycleUserPreference> userPreferences = new Dictionary<int, TransportDirect.UserPortal.CyclePlannerControl.TDCycleUserPreference>();

            TransportDirect.UserPortal.CyclePlannerControl.TDCycleUserPreference tdUserPreference = null;

            // A property that denotes the size of the array of ser preferences expected by the Atkins CTP
            int numOfProperties = System.Convert.ToInt32(Properties.Current[NUMBER_OF_PREFERENCES]);

            // Build the actual array of user preferences from permanent portal properties
            // these are used in the request sent to the Atkins CTP.
            for (int i = 0; i < numOfProperties; i++)
            {

                tdUserPreference = new TransportDirect.UserPortal.CyclePlannerControl.TDCycleUserPreference(i.ToString(),
                    Properties.Current["CyclePlanner.TDUserPreference.Preference" + i.ToString()]);

                userPreferences.Add(i, tdUserPreference);
            }

            UserPreference[] userPreferencesReturn = new UserPreference[numOfProperties];

            foreach (TransportDirect.UserPortal.CyclePlannerControl.TDCycleUserPreference tdUserPref in userPreferences.Values)
            {
                UserPreference pref = new UserPreference();
                pref.parameterID = Convert.ToInt32(tdUserPref.PreferenceKey);
                pref.parameterValue = tdUserPref.PreferenceValue;
                userPreferencesReturn[pref.parameterID] = pref;
            }

            return userPreferencesReturn;
        }

        /// <summary>
        /// Gets the default penalty function for the cycle journey
        /// </summary>
        /// <returns></returns>
        private static string GetDefaultPenaltyFunction()
        {
            StringBuilder penaltyFunction = new StringBuilder();

            string defaultJourneyType = Properties.Current[DEFAULT_CYCLE_JOURNEY_TYPE];

            string penaltyFunctionPath = Properties.Current[PENALTYFUNCTION_FOLDER];

            penaltyFunction.Append("Call ");
            penaltyFunction.Append(penaltyFunctionPath);
            if (!penaltyFunctionPath.EndsWith("/"))
            {
                penaltyFunction.Append("/");
            }
            penaltyFunction.Append(Properties.Current[string.Format(PENALTYFUNCTION_DLL, defaultJourneyType)]);
            penaltyFunction.Append(", ");

            string strPenaltyFunction = Properties.Current[string.Format(PENALTYFUNCTION_PREFIX, defaultJourneyType)]
                + "."
                + defaultJourneyType;

            penaltyFunction.Append(strPenaltyFunction);

            return penaltyFunction.ToString();

        }

        #region Logging
        /// <summary>
        /// Log EES start event
        /// </summary>
        /// <param name="context">EES context</param>
        private void LogStartEvent(ExposedServiceContext context)
        {
            Logger.Write(new EnhancedExposedServiceStartEvent(true, context));
        }

        /// <summary>
        /// Log EES end event
        /// </summary>
        /// <param name="context">EES context</param>
        /// <param name="successful">success</param>
        private void LogFinishEvent(ExposedServiceContext context, bool successful)
        {
            Logger.Write(new EnhancedExposedServiceFinishEvent(successful, context));
        }

        /// <summary>
        /// Creates an ExposedServiceContext class using real data
        /// </summary>
        /// <returns>ExposedServiceContext object</returns>
        public ExposedServiceContext CreateExposedServiceContext(string action)
        {
            ExposedServiceContext exposedServiceContext;
            string partnerId = Properties.Current[EES_PARTNER_ID];
            //create context 
            exposedServiceContext = new ExposedServiceContext(partnerId, requestId.ToString(), "en-GB", action);
            return exposedServiceContext;
        }

        /// <summary>
        /// Log Request MIS event
        /// </summary>
        /// <param name="request"></param>
        /// <param name="requestId"></param>
        /// <param name="isLoggedOn"></param>
        /// <param name="sessionId"></param>
        private void LogRequest(ModeType[] modes, string requestId, bool isLoggedOn, string sessionId)
        {
            if (CustomEventSwitch.On("JourneyPlanRequestEvent"))
            {
                JourneyPlanRequestEvent jpre = new JourneyPlanRequestEvent(requestId,
                    modes,
                    isLoggedOn,
                    sessionId);
                Logger.Write(jpre);
            }
        }

        /// <summary>
        /// Log Response MIS event
        /// </summary>
        /// <param name="result"></param>
        /// <param name="requestId"></param>
        /// <param name="isLoggedOn"></param>
        /// <param name="cjpFailed"></param>
        /// <param name="sessionId"></param>
        private void LogResponse(CjpInterface.JourneyResult result, string requestId, bool isLoggedOn,
            bool cjpFailed, string sessionId)
        {
            if (CustomEventSwitch.On("JourneyPlanResultsEvent"))
            {
                JourneyPlanResponseCategory responseCategory;

                if (cjpFailed)
                {
                    responseCategory = JourneyPlanResponseCategory.Failure;
                }
                else
                {
                    if (result == null)
                    {
                        responseCategory = JourneyPlanResponseCategory.ZeroResults; 
                    }
                    else if ((result.privateJourneys == null || result.privateJourneys.Length == 0)
                        && (result.publicJourneys == null || result.publicJourneys.Length == 0)
                        && (result.parknRideJourneys == null || result.parknRideJourneys.Length == 0))
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
        /// Logs a Cycle Planner request event
        /// </summary>
        private void LogCycleRequest(string requestId, bool isLoggedOn, string sessionId)
        {
            // Log a request event.
            if (CustomEventSwitch.On("CyclePlannerRequestEvent"))
            {
                CyclePlannerRequestEvent cpre = new CyclePlannerRequestEvent(requestId,
                    isLoggedOn,
                    sessionId);
                Logger.Write(cpre);
            }
        }

        /// <summary>
        /// Logs a Cycle Planner result event
        /// </summary>
        private void LogCycleResponse(CyclePlannerResult journeyResult, string requestId, bool isLoggedOn,
            bool cyclePlannerFailed, string sessionId)
        {
            if (CustomEventSwitch.On("CyclePlannerResultEvent"))
            {
                JourneyPlanResponseCategory responseCategory;

                if (cyclePlannerFailed)
                {
                    responseCategory = JourneyPlanResponseCategory.Failure;
                }
                else
                {
                    CycleService.JourneyResult result;

                    try
                    {
                        result = (CycleService.JourneyResult)journeyResult;

                        if (result.journeys == null || result.journeys.Length == 0)
                        {
                            responseCategory = JourneyPlanResponseCategory.ZeroResults; 
                        }
                        else
                        {
                            responseCategory = JourneyPlanResponseCategory.Results;
                        }
                    }
                    catch (Exception)
                    {
                        // Assume this means a failure if can't cast
                        responseCategory = JourneyPlanResponseCategory.Failure;
                    }
                }

                CyclePlannerResultEvent cpre = new CyclePlannerResultEvent(requestId,
                    responseCategory,
                    isLoggedOn,
                    sessionId);
                Logger.Write(cpre);
            }
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

        /// <summary>
        /// Do not call journey planning and return the same result for all requests - for testing purposes
        /// </summary>
        private void SetStockAnswers()
        {
            ptSummary.LandingUrL = "?oo=p&amp;o=ch1+5ey&amp;on=ch1+5ey&amp;id=BatchJP&amp;do=p&amp;d=wa6+8pw&amp;dn=wa6+8pw&amp;da=d&amp;m=m&amp;p=0&amp;c=True";
            ptSummary.OutwardChanges = "3";
            ptSummary.OutwardCO2 = "2.1";
            ptSummary.OutwardJourneyTime = new TimeSpan(1, 37, 0);
            ptSummary.OutwardModes = "Walk|Bus";
            ptSummary.ReturnChanges = "2";
            ptSummary.ReturnCO2="3.6";
            ptSummary.ReturnJourneyTime = new TimeSpan(1, 51, 0);
            ptSummary.ReturnModes = "Walk|Bus";

            ptDetails = new XmlDocument();
            ptDetails.LoadXml("<ROOT><Journey Direction=\"Outward\"><JourneyDate>2013-03-12</JourneyDate><JourneyTime>09::0:00</JourneyTime><JourneyDuration>1:37</JourneyDuration><ArriveDepart>Arrive</ArriveDepart><Leg><LegOrigin>ch1 5ey</LegOrigin><LegDestination>Blacon, The Lodge (adj) (on Saughall Road) [SMS : chwdawj]</LegDestination><LegMode>Walk</LegMode><LegDepart>06:30</LegDepart><LegArrive /><LegInstructions>Walk to Blacon, The Lodge (adj) (on Saughall Road) [SMS : chwdawj]</LegInstructions><LegDuration>6</LegDuration></Leg><Leg><LegOrigin>Blacon, The Lodge (adj) (on Saughall Road) [SMS : chwdawj]</LegOrigin><LegDestination>Chester, Bus Exchange (Stand 1B) (on Princess Street) [SMS : chwpdam]</LegDestination><LegMode>Bus</LegMode><LegDepart>06:36</LegDepart><LegArrive>06:43</LegArrive><LegInstructions>Take Arriva Buses Wales/1A towards Chester</LegInstructions><LegDuration>7</LegDuration></Leg><Leg><LegOrigin>Chester, Bus Exchange (Stand 1B) (on Princess Street) [SMS : chwpdam]</LegOrigin><LegDestination>Chester, Pepper Street (Stand Z) (on Pepper Street) [SMS : chwagda]</LegDestination><LegMode>Walk</LegMode><LegArrive /><LegInstructions>Walk to Chester, Pepper Street (Stand Z) (on Pepper Street) [SMS : chwagda]</LegInstructions><LegDuration>7</LegDuration></Leg><Leg><LegOrigin>Chester, Pepper Street (Stand Z) (on Pepper Street) [SMS : chwagda]</LegOrigin><LegDestination>Chester, Railway Station (S4) (on City Road) [SMS : chwdjgd]</LegDestination><LegMode>Bus</LegMode><LegDepart>06:56</LegDepart><LegArrive>07:01</LegArrive><LegInstructions>Take Arriva Cymru/1 towards Chester</LegInstructions><LegDuration>5</LegDuration></Leg><Leg><LegOrigin>Chester, Railway Station (S4) (on City Road) [SMS : chwdjgd]</LegOrigin><LegDestination>Chester Rail Station</LegDestination><LegMode>Walk</LegMode><LegArrive /><LegInstructions>Walk to Chester Rail Station</LegInstructions><LegDuration>1</LegDuration></Leg><Leg><LegOrigin>Chester</LegOrigin><LegDestination>Frodsham</LegDestination><LegMode>Rail</LegMode><LegDepart>07:12</LegDepart><LegArrive>07:25</LegArrive><LegInstructions>Take Arriva Trains Wales towards MANCHESTER PICCADILLY</LegInstructions><LegDuration>13</LegDuration></Leg><Leg><LegOrigin>Frodsham Rail Station</LegOrigin><LegDestination>Frodsham, Bridge Stores (opp) (on Church Street) [SMS : chwapjw]</LegDestination><LegMode>Walk</LegMode><LegArrive /><LegInstructions>Walk to Frodsham, Bridge Stores (opp) (on Church Street) [SMS : chwapjw]</LegInstructions><LegDuration>1</LegDuration></Leg><Leg><LegOrigin>Frodsham, Bridge Stores (opp) (on Church Street) [SMS : chwapjw]</LegOrigin><LegDestination>Norley, Pytcheley Hollow (cnr) (on School Bank) [SMS : chwgtjp]</LegDestination><LegMode>Bus</LegMode><LegDepart>07:37</LegDepart><LegArrive>07:56</LegArrive><LegInstructions>Take GHA Coaches/48 towards Northwich</LegInstructions><LegDuration>19</LegDuration></Leg><Leg><LegOrigin>Norley, Pytcheley Hollow (cnr) (on School Bank) [SMS : chwgtjp]</LegOrigin><LegDestination>wa6 8pw</LegDestination><LegMode>Walk</LegMode><LegArrive>08:07</LegArrive><LegInstructions>Walk to wa6 8pw</LegInstructions><LegDuration>11</LegDuration></Leg></Journey><Journey Direction=\"Outward\"><JourneyDate>2013-03-12</JourneyDate><JourneyTime>09::0:00</JourneyTime><JourneyDuration>1:52</JourneyDuration><ArriveDepart>Arrive</ArriveDepart><Leg><LegOrigin>ch1 5ey</LegOrigin><LegDestination>Blacon, The Lodge (adj) (on Saughall Road) [SMS : chwdawj]</LegDestination><LegMode>Walk</LegMode><LegDepart>15:30</LegDepart><LegArrive /><LegInstructions>Walk to Blacon, The Lodge (adj) (on Saughall Road) [SMS : chwdawj]</LegInstructions><LegDuration>6</LegDuration></Leg><Leg><LegOrigin>Blacon, The Lodge (adj) (on Saughall Road) [SMS : chwdawj]</LegOrigin><LegDestination>Chester, Bus Exchange (Stand 1B) (on Princess Street) [SMS : chwpdam]</LegDestination><LegMode>Bus</LegMode><LegDepart>15:36</LegDepart><LegArrive>15:43</LegArrive><LegInstructions>Take Arriva Buses Wales/1A towards Chester</LegInstructions><LegDuration>7</LegDuration></Leg><Leg><LegOrigin>Chester, Bus Exchange (Stand 1B) (on Princess Street) [SMS : chwpdam]</LegOrigin><LegDestination>Chester, Bus Exchange (Stand 4B) (on Princess Street) [SMS : chwpdgt]</LegDestination><LegMode>Walk</LegMode><LegArrive /><LegInstructions>Walk to Chester, Bus Exchange (Stand 4B) (on Princess Street) [SMS : chwpdgt]</LegInstructions><LegDuration>1</LegDuration></Leg><Leg><LegOrigin>Chester, Bus Exchange (Stand 4B) (on Princess Street) [SMS : chwpdgt]</LegOrigin><LegDestination>Chester, Hoole Way (Stop Q) (on Hoole Way) [SMS : chwmgma]</LegDestination><LegMode>Bus</LegMode><LegDepart>15:50</LegDepart><LegArrive>15:52</LegArrive><LegInstructions>Take Stagecoach Merseyside &amp;/53 towards Kingsway Circular</LegInstructions><LegDuration>2</LegDuration></Leg><Leg><LegOrigin>Chester, Hoole Way (Stop Q) (on Hoole Way) [SMS : chwmgma]</LegOrigin><LegDestination>Chester Rail Station</LegDestination><LegMode>Walk</LegMode><LegArrive /><LegInstructions>Walk to Chester Rail Station</LegInstructions><LegDuration>6</LegDuration></Leg><Leg><LegOrigin>Chester</LegOrigin><LegDestination>Cuddington</LegDestination><LegMode>Rail</LegMode><LegDepart>16:07</LegDepart><LegArrive>16:27</LegArrive><LegInstructions>Take Northern Rail towards MANCHESTER PICCADILLY</LegInstructions><LegDuration>20</LegDuration></Leg><Leg><LegOrigin>Cuddington Rail Station</LegOrigin><LegDestination>Cuddington, Mill Lane (opp) (on Norley Road) [SMS : chwamtj]</LegDestination><LegMode>Walk</LegMode><LegArrive /><LegInstructions>Walk to Cuddington, Mill Lane (opp) (on Norley Road) [SMS : chwamtj]</LegInstructions><LegDuration>3</LegDuration></Leg><Leg><LegOrigin>Cuddington, Mill Lane (opp) (on Norley Road) [SMS : chwamtj]</LegOrigin><LegDestination>Cuddington, Springfields (opp) (on Hollow Oak Lane) [SMS : chwadpam]</LegDestination><LegMode>Bus</LegMode><LegDepart>16:59</LegDepart><LegArrive>17:03</LegArrive><LegInstructions>Take GHA Coaches/82B towards Chester</LegInstructions><LegDuration>4</LegDuration></Leg><Leg><LegOrigin>Cuddington, Springfields (opp) (on Hollow Oak Lane) [SMS : chwadpam]</LegOrigin><LegDestination>wa6 8pw</LegDestination><LegMode>Walk</LegMode><LegArrive>17:22</LegArrive><LegInstructions>Walk to wa6 8pw</LegInstructions><LegDuration>19</LegDuration></Leg></Journey><Journey Direction=\"Return\"><JourneyDate>2013-03-12</JourneyDate><JourneyTime>17::0:00</JourneyTime><JourneyDuration>1:51</JourneyDuration><ArriveDepart>Depart</ArriveDepart><Leg><LegOrigin>wa6 8pw</LegOrigin><LegDestination>Norley, West View Road (cnr) (on School Bank) [SMS : chwmwpt]</LegDestination><LegMode>Walk</LegMode><LegDepart>17:16</LegDepart><LegArrive /><LegInstructions>Walk to Norley, West View Road (cnr) (on School Bank) [SMS : chwmwpt]</LegInstructions><LegDuration>13</LegDuration></Leg><Leg><LegOrigin>Norley, West View Road (cnr) (on School Bank) [SMS : chwmwpt]</LegOrigin><LegDestination>Frodsham, Co-op (opp) (on High Street) [SMS : chwdtmp]</LegDestination><LegMode>Bus</LegMode><LegDepart>17:29</LegDepart><LegArrive>17:50</LegArrive><LegInstructions>Take GHA Coaches/48 towards Frodsham</LegInstructions><LegDuration>21</LegDuration></Leg><Leg><LegOrigin>Frodsham, Co-op (opp) (on High Street) [SMS : chwdtmp]</LegOrigin><LegDestination>Frodsham, Devonshire Bakery (o/s) (on High Street) [SMS : chwapjt]</LegDestination><LegMode>Walk</LegMode><LegArrive /><LegInstructions>Walk to Frodsham, Devonshire Bakery (o/s) (on High Street) [SMS : chwapjt]</LegInstructions><LegDuration>1</LegDuration></Leg><Leg><LegOrigin>Frodsham, Devonshire Bakery (o/s) (on High Street) [SMS : chwapjt]</LegOrigin><LegDestination>Chester, Bus Exchange (Stand 2C) (on Princess Street) [SMS : chwpdga]</LegDestination><LegMode>Bus</LegMode><LegDepart>18:05</LegDepart><LegArrive>18:39</LegArrive><LegInstructions>Take Arriva North West/X30 towards Chester</LegInstructions><LegDuration>34</LegDuration></Leg><Leg><LegOrigin>Chester, Bus Exchange (Stand 2C) (on Princess Street) [SMS : chwpdga]</LegOrigin><LegDestination>Chester, Bus Exchange (Stand 1A) (on Princess Street) [SMS : chwpdjm]</LegDestination><LegMode>Walk</LegMode><LegArrive /><LegInstructions>Walk to Chester, Bus Exchange (Stand 1A) (on Princess Street) [SMS : chwpdjm]</LegInstructions><LegDuration>1</LegDuration></Leg><Leg><LegOrigin>Chester, Bus Exchange (Stand 1A) (on Princess Street) [SMS : chwpdjm]</LegOrigin><LegDestination>Blacon, The Highfield (o/s) (on Western Avenue) [SMS : chwdgdm]</LegDestination><LegMode>Bus</LegMode><LegDepart>18:50</LegDepart><LegArrive>19:01</LegArrive><LegInstructions>Take Stagecoach Merseyside &amp;/1A towards Chester</LegInstructions><LegDuration>11</LegDuration></Leg><Leg><LegOrigin>Blacon, The Highfield (o/s) (on Western Avenue) [SMS : chwdgdm]</LegOrigin><LegDestination>ch1 5ey</LegDestination><LegMode>Walk</LegMode><LegArrive>19:07</LegArrive><LegInstructions>Walk to ch1 5ey</LegInstructions><LegDuration>6</LegDuration></Leg></Journey><Journey Direction=\"Return\"><JourneyDate>2013-03-12</JourneyDate><JourneyTime>17::0:00</JourneyTime><JourneyDuration>1:53</JourneyDuration><ArriveDepart>Depart</ArriveDepart><Leg><LegOrigin>wa6 8pw</LegOrigin><LegDestination>Cuddington, Springfields (opp) (on Hollow Oak Lane) [SMS : chwadpam]</LegDestination><LegMode>Walk</LegMode><LegDepart>07:28</LegDepart><LegArrive /><LegInstructions>Walk to Cuddington, Springfields (opp) (on Hollow Oak Lane) [SMS : chwadpam]</LegInstructions><LegDuration>20</LegDuration></Leg><Leg><LegOrigin>Cuddington, Springfields (opp) (on Hollow Oak Lane) [SMS : chwadpam]</LegOrigin><LegDestination>Cuddington, Mill Lane (cnr) (on Norley Road) [SMS : chwamtm]</LegDestination><LegMode>Bus</LegMode><LegDepart>07:48</LegDepart><LegArrive>07:53</LegArrive><LegInstructions>Take GHA Coaches/82B towards Northwich</LegInstructions><LegDuration>5</LegDuration></Leg><Leg><LegOrigin>Cuddington, Mill Lane (cnr) (on Norley Road) [SMS : chwamtm]</LegOrigin><LegDestination>Cuddington Rail Station</LegDestination><LegMode>Walk</LegMode><LegArrive /><LegInstructions>Walk to Cuddington Rail Station</LegInstructions><LegDuration>2</LegDuration></Leg><Leg><LegOrigin>Cuddington</LegOrigin><LegDestination>Chester</LegDestination><LegMode>Rail</LegMode><LegDepart>08:22</LegDepart><LegArrive>08:45</LegArrive><LegInstructions>Take Northern Rail towards CHESTER</LegInstructions><LegDuration>23</LegDuration></Leg><Leg><LegOrigin>Chester Rail Station</LegOrigin><LegDestination>Chester, Railway Station (S5) (on Station Road) [SMS : chwtmap]</LegDestination><LegMode>Walk</LegMode><LegArrive /><LegInstructions>Walk to Chester, Railway Station (S5) (on Station Road) [SMS : chwtmap]</LegInstructions><LegDuration>1</LegDuration></Leg><Leg><LegOrigin>Chester, Railway Station (S5) (on Station Road) [SMS : chwtmap]</LegOrigin><LegDestination>Chester, Bus Exchange (Stand 4A) (on Princess Street) [SMS : chwpdgp]</LegDestination><LegMode>Bus</LegMode><LegDepart>08:51</LegDepart><LegArrive>08:57</LegArrive><LegInstructions>Take First Chester &amp; The Wirral/16A towards Saltney</LegInstructions><LegDuration>6</LegDuration></Leg><Leg><LegOrigin>Chester, Bus Exchange (Stand 4A) (on Princess Street) [SMS : chwpdgp]</LegOrigin><LegDestination>Chester, Bus Exchange (Stand 1A) (on Princess Street) [SMS : chwpdjm]</LegDestination><LegMode>Walk</LegMode><LegArrive /><LegInstructions>Walk to Chester, Bus Exchange (Stand 1A) (on Princess Street) [SMS : chwpdjm]</LegInstructions><LegDuration>1</LegDuration></Leg><Leg><LegOrigin>Chester, Bus Exchange (Stand 1A) (on Princess Street) [SMS : chwpdjm]</LegOrigin><LegDestination>Blacon, St Chad's Road (opp) (on Blacon Point Road) [SMS : chwmdmt]</LegDestination><LegMode>Bus</LegMode><LegDepart>09:10</LegDepart><LegArrive>09:18</LegArrive><LegInstructions>Take Arriva Buses Wales/1 towards Chester</LegInstructions><LegDuration>8</LegDuration></Leg><Leg><LegOrigin>Blacon, St Chad's Road (opp) (on Blacon Point Road) [SMS : chwmdmt]</LegOrigin><LegDestination>ch1 5ey</LegDestination><LegMode>Walk</LegMode><LegArrive>09:21</LegArrive><LegInstructions>Walk to ch1 5ey</LegInstructions><LegDuration>3</LegDuration></Leg></Journey></ROOT>");

            ptOutwardStatus = (int)BatchDetailStatus.Complete;
            ptReturnStatus = (int)BatchDetailStatus.Complete;

            carSummary.OutwardCO2 = "4.3";
            carSummary.OutwardDistance = "15.5";
            carSummary.OutwardJourneyTime = new TimeSpan(0, 35, 0);
            carSummary.ReturnCO2 = "4.3";
            carSummary.ReturnDistance = "15.5";
            carSummary.ReturnJourneyTime = new TimeSpan(0, 36, 0);

            carOutwardStatus = (int)BatchDetailStatus.Complete;
            carReturnStatus = (int)BatchDetailStatus.Complete;

            cycleSummary.LandingUrL = "?oo=p&amp;o=ch1+5ey&amp;on=ch1+5ey&amp;id=BatchJP&amp;do=p&amp;d=wa6+8pw&amp;dn=wa6+8pw&amp;da=d&amp;m=b&amp;p=0";
            cycleSummary.OutwardDistance = "15.5";
            cycleSummary.OutwardJourneyTime = new TimeSpan(1, 34, 0);
            cycleSummary.ReturnDistance = "15.5";
            cycleSummary.ReturnJourneyTime = new TimeSpan(1,  34, 0);

            cycleOutwardStatus = (int)BatchDetailStatus.Complete;
            cycleReturnStatus = (int)BatchDetailStatus.Complete;
        }
    }
}
