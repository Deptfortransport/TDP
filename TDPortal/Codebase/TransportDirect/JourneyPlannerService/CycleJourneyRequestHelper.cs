// *********************************************** 
// NAME             : CycleJourneyRequestHelper.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: This class represents the functionality required by 
//                  : Cycle Journey Planner synchronous service
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerService/CycleJourneyRequestHelper.cs-arc  $
//
//   Rev 1.3   Nov 05 2010 16:00:52   apatel
//Updated to resolve the EES Cycle web service issues
//Resolution for 5632: EES for Cycle issues
//
//   Rev 1.2   Nov 05 2010 14:35:24   apatel
//Updated to resolve the EES Cycle web service issues
//Resolution for 5632: EES for Cycle issues
//
//   Rev 1.1   Oct 26 2010 14:37:32   apatel
//Code updated to  accept multiple cycle algorithm function dll's
//Resolution for 5622: Update CTP to accept multiple function dlls (Doc Ref: ATO687)
//
//   Rev 1.0   Sep 29 2010 10:46:00   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.EnhancedExposedServices.Common;

using CommonDTO = TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;

using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.Logging;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.PropertyService.Properties;
using System.Collections;
using TransportDirect.Presentation.InteractiveMapping;

namespace TransportDirect.UserPortal.JourneyPlannerService
{
    /// <summary>
    /// This class represents the functionality required by Cycle Journey Planner synchronous service
    /// </summary>
	public class CycleJourneyRequestHelper
	{
        #region Private members

        private ExposedServiceContext context;
		private JourneyRequest dtoCycleRequestJourney;
		private ITDCyclePlannerRequest request;		

        #endregion

        #region constructor
        /// <summary>
        /// Constructor for JourneyRequestHelper (Cycle journey mode)
        /// </summary>
        /// <param name="context">The exposed service context</param>
        /// <param name="request">The actual request</param>
        /// <param name="dtoRequestJourney">The data transformation request for the cycle journey</param>
        public CycleJourneyRequestHelper(ExposedServiceContext context,
                                   ITDCyclePlannerRequest request,
                                   JourneyRequest dtoCycleRequestJourney)
        {
            this.context = context;
            this.request = request;
            this.dtoCycleRequestJourney = dtoCycleRequestJourney;
        }

		#endregion

		#region properties
		/// <summary>
		/// Read-only property to write cleaner code.
		/// </summary>
		public TDResourceManager ResourceManager
		{
			get
			{
                return TDResourceManager.GetResourceManagerFromCache(TDResourceManager.JOURNEY_PLANNER_SERVICE_RM);
			}			
		}		

		#endregion

		#region public methods
		/// <summary>
		/// This method performs the necessary processing to convert the location text supplied 
		/// by the External System to actual location data that can be used for journey planning
		/// </summary>
		public void ResolveLocations()
		{
            
            #region Resolve CycleJourneyRequest

            request.OriginLocation = ResolveLocation(dtoCycleRequestJourney.OriginLocation);
            request.DestinationLocation = ResolveLocation(dtoCycleRequestJourney.DestinationLocation);

            if (dtoCycleRequestJourney.ViaLocation != null && dtoCycleRequestJourney.ViaLocation.GridReference != null
                && !string.IsNullOrEmpty(dtoCycleRequestJourney.ViaLocation.Description))
            {
                if (dtoCycleRequestJourney.ViaLocation.GridReference.Easting != 0
                    && dtoCycleRequestJourney.ViaLocation.GridReference.Northing != 0)
                {
                    TDLocation viaLocation = ResolveLocation(dtoCycleRequestJourney.ViaLocation);

                    if (viaLocation != null)
                    {
                        request.CycleViaLocations = new TDLocation[1];

                        request.CycleViaLocations[0] = viaLocation;

                    }
                    else
                    {
                        request.CycleViaLocations = new TDLocation[0];
                    }
                }
            }
            #endregion

            // Check locations for duplicate
            CheckLocationsForDuplication();

            // Check Locations for overlap
            CheckLocationsForOverlapping();

            // Check for the distance between origin and destination/via location
            // withing the maximum distance allowable.
            CheckLocationsDistance();

            // Check cycle journey locations requested are in the same cycle data area
            CheckLocationsInSameCycleDataArea();
            
		}

        

		/// <summary>
		/// This method performs validation on the supplied request object in addition to the schema 
		/// validation already performed on the request
		/// </summary>
		public void Validate()
		{			
			TDDateTime outwardDateTime = (TDDateTime) request.OutwardDateTime[0];
			
			//validate if the time happens in the past
			if (outwardDateTime < TDDateTime.Now)
			{											   
				string validationError = "Outward date time in the past, date time={0}";									

				ThrowError(validationError, 
						   outwardDateTime.ToString(),
						   TDExceptionIdentifier.JPOutwardDateTimeInPast);
            }

		}

        /// <summary>
		/// This method submits a request to the Cycle planner through the CyclePlannerControl 
		/// component to commence cycle journey planning
		/// </summary>
		/// <returns>ITDJourneyresult from the CJP</returns>
		public ITDCyclePlannerResult CallCyclePlanner()
		{
			// Get a Cycle Planner Manager from the service discovery
            ICyclePlannerManager cyclePlannerManager = (ICyclePlannerManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.CyclePlannerManager];

            ITDCyclePlannerResult tdCycleJourneyResult = cyclePlannerManager.CallCyclePlanner(request, 
																  context.InternalTransactionId, 
																  (int) TDUserType.Standard, 
																  false,
																  false, 
																  context.Language, 
																  string.Empty,
                                                                  true);

			if( TDTraceSwitch.TraceVerbose )
			{
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
					"CyclePlannerManager has returned - Internal Transaction ID = " + context.InternalTransactionId));
			}

			// Check if any return journey times overlap with outward journey times
            if (tdCycleJourneyResult.IsValid) 
			{
                
				//check if any journeys returned start in the past 
                if (tdCycleJourneyResult.CheckForJourneyStartInPast(request))
				{
                    tdCycleJourneyResult.AddMessageToArray(ResourceManager.GetString("JourneyPlannerOutput.JourneyTimeInPast"),
													  "JourneyPlannerOutput.JourneyTimeInPast",
													  0,
													  0,
													  TransportDirect.UserPortal.CyclePlannerControl.ErrorsType.Warning);
				}
			}

            return tdCycleJourneyResult;

		}

	
		#region throw errors and events
        /// <summary>
        /// Throws an TD exception error an logs an operational event
        /// </summary>
        /// <param name="errorMessage">Message to log</param>
        /// <param name="parameters">the innerparameter to fill into the message</param>
        /// <param name="identifier">unique identifier to this error</param>
        public void ThrowError(string errorMessage,
                               TDExceptionIdentifier identifier)
        {
            ThrowError(errorMessage, new string[0], identifier);
        }

        /// <summary>
        /// Throws an TD exception error an logs an operational event
        /// </summary>
        /// <param name="errorMessage">Message to log</param>
        /// <param name="parameters">the innerparameter to fill into the message</param>
        /// <param name="identifier">unique identifier to this error</param>
        public void ThrowError(string errorMessage,
                               string parameter,
                               TDExceptionIdentifier identifier)
        {
            string[] parameters = new string[1];
            parameters[0] = parameter;

            ThrowError(errorMessage, parameters, identifier);
        }

		/// <summary>
		/// Throws an TD exception error an logs an operational event
		/// </summary>
		/// <param name="errorMessage">Message to log</param>
		/// <param name="parameters">the innerparameters to fill into the message</param>
		/// <param name="identifier">unique identifier to this error</param>
		public void ThrowError(string errorMessage,
							   string[] parameters,
							   TDExceptionIdentifier identifier)
		{												
			ThrowError(errorMessage, parameters, identifier, null);
		}

		/// <summary>
		/// Throws an TD exception error an logs an operational event
		/// </summary>
		/// <param name="errorMessage">Message to log</param>
		/// <param name="parameters">the innerparameters to fill into the message</param>
		/// <param name="identifier">unique identifier to this error</param>
		/// <param name="cjpMessages">message coming from the cjp</param>
		public void ThrowError(string errorMessage,
							   string[] parameters,
							   TDExceptionIdentifier identifier,
							   CJPMessage[] cjpMessages)
		{												
			//replace the number of parameters
			if (parameters != null && parameters.Length > 0)
			{
				for(int i=0; i<parameters.Length; i++)			
					errorMessage = errorMessage.Replace("{" + i + "}", parameters[i]);			
			}

			Logger.Write(new OperationalEvent(TDEventCategory.Business, 
											  TDTraceLevel.Error, 
											  errorMessage, 
										      this,
											  context.InternalTransactionId));

			if (cjpMessages != null)
				throw new TDException(errorMessage, false, identifier, cjpMessages);
			else
				throw new TDException(errorMessage, false, identifier);

		}		

		/// <summary>
		/// public method to log the end of the request
		/// </summary>
		/// <param name="enhanceExpServiceType">Enhanced Exposed Service Type Request</param>
		/// <param name="externalTransactionId">Reference transaction Id provided by client</param>
		/// <param name="callSucessful">Indicates whether call was sucessfull or not</param>
		public void LogFinishEvent(bool callSuccessful)
		{
			if (context != null)
			{
				Logger.Write(new EnhancedExposedServiceFinishEvent(callSuccessful, context));						
			}
		}
		#endregion

		#endregion

		#region Private methods

        /// <summary>
        /// Resolves and returns a TDLocation for a DataTransfer.CycleJourneyPlanner.V1.RequestLocation
        /// </summary>
        /// <param name="dtoCarRequestJourney"></param>
        /// <returns></returns>
        private TDLocation ResolveLocation(RequestLocation requestLocation)
        {
            // Resolve location based on the type specified
            switch (requestLocation.Type)
            {
                case LocationType.Coordinate:
                    // Check if the coordiantes for location can be resolved
                    OSGridReference osgr = new OSGridReference(
                        requestLocation.GridReference.Easting,
                        requestLocation.GridReference.Northing);

                    if (!osgr.IsValid)
                    {

                        string validationError = string.Format("Coordinate for location is invalid [{0},{1}]", 
                            requestLocation.GridReference.Easting.ToString(), requestLocation.GridReference.Northing.ToString());

                        ThrowError(validationError,
                                   string.Empty,
                                   TDExceptionIdentifier.JPResolveCoordinateFailed);
                    }

                    return ResolveLocation(osgr, requestLocation.Description);
                    
                default:
                    string validationError_Invalid_LocationType = string.Format("Invalid type of location supplied, Cycle Planner only accepts coordinate as location type.");
                    ThrowError(validationError_Invalid_LocationType,
                                   string.Empty,
                                   TDExceptionIdentifier.JPResolveCoordinateFailed);
                    break;
                
            }

            return null;
        }

        /// <summary>
        /// This method will resolve a coordinate location for Cycle journey planning
        /// </summary>
        /// <param name="osgr"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        private TDLocation ResolveLocation(OSGridReference osgr, string description)
        {
            // Check bounds of coordinates
            int maxEasting = int.Parse(Properties.Current["Coordinate.Validation.Easting.Max"]);
            int maxNorthing = int.Parse(Properties.Current["Coordinate.Validation.Northing.Max"]);

            if (osgr.Easting < 0 || osgr.Easting > maxEasting)
            {
                ThrowError("Coordinate value invalid", new string[0], TDExceptionIdentifier.JPResolveCoordinateFailed);
            }
            else if (osgr.Northing < 0 || osgr.Northing > maxNorthing)
            {
                ThrowError("Coordinate value invalid", new string[0], TDExceptionIdentifier.JPResolveCoordinateFailed);
            }

            // Create a new TDLocation and populate the name and coordiante
            TDLocation location = new TDLocation();

            location.Description = description;
            location.GridReference = osgr;
            location.RequestPlaceType = TransportDirect.JourneyPlanning.CJPInterface.RequestPlaceType.Coordinate;
            
            // Populate the Toids to be used for the cycle journey
            location.PopulateToids();
            
            // Populate the points which will be same as OSGRs
            location.PopulatePoint(false);
            location.Locality = PopulateLocality(osgr);

            // Location is assumed to be valid
            location.Status = TDLocationStatus.Valid;

            return location;
        }

        /// <summary>
        /// Checks the distance between the locations to a configured value
        /// </summary>
        private void CheckLocationsDistance()
        {
            int maxJourneyDistanceMetres = Convert.ToInt32(Properties.Current["CyclePlanner.Planner.MaxJourneyDistance.Metres"]);

            TDLocation fromLocation = request.OriginLocation;
            TDLocation toLocation = request.DestinationLocation;

            // cycle journey request dto contains only one location
            TDLocation[] cycleViaLocations = request.CycleViaLocations == null ? new TDLocation[0] : request.CycleViaLocations ;

            if (cycleViaLocations.Length == 0)
            {
                // Check distance betweeen from and to only
                if (fromLocation.GridReference.DistanceFrom(toLocation.GridReference) > maxJourneyDistanceMetres)
                {
                    #region Log
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        string logMsg = "CyclePlanner - CycleJourneyPlanRunner. Distance between cycle locations "
                            + fromLocation.Description + " and " + toLocation.Description
                            + " is greater than " + maxJourneyDistanceMetres + " metres";

                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg));
                    }
                    #endregion

                    string validationError = "Distance between cycle locations {0} and {1} is greater than {2} metres";

                    ThrowError(validationError,
                           new string[] { fromLocation.Description, toLocation.Description, maxJourneyDistanceMetres.ToString() },
                           TDExceptionIdentifier.EESCycleJourneyPlannerDistanceBetweenLocationsTooGreat);
                }
            }
            else
            {
                // Check distance between locations and via location
                if ((cycleViaLocations[0].GridReference.DistanceFrom(fromLocation.GridReference) > maxJourneyDistanceMetres)
                    ||
                    (cycleViaLocations[0].GridReference.DistanceFrom(toLocation.GridReference) > maxJourneyDistanceMetres))
                {
                    #region Log
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        string logMsg = "CyclePlanner - CycleJourneyPlanRunner. Distance between cycle locations Origin/Destination"
                            + fromLocation.Description + " / " + toLocation.Description
                            + " and the Via location " + cycleViaLocations[0].Description
                            + " is greater than " + maxJourneyDistanceMetres + " metres";

                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg));
                    }
                    #endregion

                    string validationError = "Distance between cycle locations Origin/Destination "
                            + fromLocation.Description + " / " + toLocation.Description
                            + " and the Via location " + cycleViaLocations[0].Description
                            + " is greater than " + maxJourneyDistanceMetres + " metres";


                    ThrowError(validationError,
                           TDExceptionIdentifier.EESCycleJourneyPlannerDistanceBetweenLocationsTooGreat);
                }
            }

        }

        /// <summary>
        /// Validated locations specified in the cycle journey request are in same cycle data areas
        /// </summary>
        private void CheckLocationsInSameCycleDataArea()
        {
            #region Valiate Points in Cycle Data areas

            bool validatePoints = bool.Parse(Properties.Current["CyclePlanner.Planner.PointValidation.Switch"]);

            if (validatePoints)
            {
                // Get all the points
                ArrayList pointsArray = new ArrayList();
                pointsArray.Add(request.OriginLocation.Point);
                pointsArray.Add(request.DestinationLocation.Point);

                if (request.CycleViaLocations != null && request.CycleViaLocations.Length > 0)
                {
                    TDLocation cycleViaLocation = request.CycleViaLocations[0];
                    if (cycleViaLocation.Status == TDLocationStatus.Valid)
                    {
                        pointsArray.Add(cycleViaLocation.Point);
                    }
                }

                Point[] points = (Point[])pointsArray.ToArray(typeof(Point));

                // First check: make sure the Points are in valid cycle data areas
                IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

                if (!gisQuery.IsPointsInCycleDataArea(points, false))
                {
                    StringBuilder validationError = new StringBuilder();
                    validationError.Append(" Location points are not in a valid cycle data area: ");
                    foreach (Point point in points)
                    {
                        validationError.Append(point.X);
                        validationError.Append(",");
                        validationError.Append(point.Y);
                        validationError.Append(" ");
                    }

                    #region Log
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        StringBuilder logMsg = new StringBuilder();
                        logMsg.AppendFormat("EES CyclePlanner - CycleJourneyRequestHelper. {0}", validationError.ToString());

                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg.ToString()));
                    }
                    #endregion

                    ThrowError(validationError.ToString(),
                           TDExceptionIdentifier.EESCycleJourneyPlannerDistanceBetweenLocationsTooGreat);

                }
                else
                {
                    // Second check: if plan in Same area flag
                    bool planInSameCycleArea = bool.Parse(Properties.Current["CyclePlanner.Planner.PointValidation.PlanSameAreaOnly"]);

                    if ((planInSameCycleArea) && (!gisQuery.IsPointsInCycleDataArea(points, true)))
                    {

                        StringBuilder validationError = new StringBuilder();
                        validationError.Append("EES CyclePlanner - CycleJourneyRequestHelper. Location points are not in the same cycle data area: ");
                        foreach (Point point in points)
                        {
                            validationError.Append(point.X);
                            validationError.Append(",");
                            validationError.Append(point.Y);
                            validationError.Append(" ");
                        }

                        #region Log
                        if (TDTraceSwitch.TraceVerbose)
                        {
                            StringBuilder logMsg = new StringBuilder();
                            logMsg.AppendFormat("EES CyclePlanner - CycleJourneyRequestHelper. {0}", validationError.ToString());

                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg.ToString()));
                        }
                        #endregion

                        ThrowError(validationError.ToString(),
                               TDExceptionIdentifier.EESCycleJourneyPlannerDistanceBetweenLocationsTooGreat);
 
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// validates the from, to and via locations for overlapping 
        /// </summary>
       protected void CheckLocationsForOverlapping()
        {
            //location objects used for the origin and destination
            TDLocation fromLocation = null;
            TDLocation toLocation = null;
            TDLocation viaLocation = null;

            //assign the start and destination locations 
            fromLocation = request.OriginLocation;
            toLocation = request.DestinationLocation;
            if (request.CycleViaLocations != null && request.CycleViaLocations.Length > 0)
            {
                viaLocation = request.CycleViaLocations[0]; // Use the via location specified
            }

            // if an overlap occurs between the from, to or via location,
            // then throw error
            
            //check origin and destination locations for overlapping naptans
            if (fromLocation.Intersects(toLocation, StationType.Undetermined))
            {
                #region Log
                if (TDTraceSwitch.TraceVerbose)
                {
                    string logMsg = "EES CyclePlanner - CycleJourneyRequestHelper. There are overlapping naptans for the origin and destination locations. Origin location: " +
                                    request.OriginLocation + " Destination location: "
                                    + request.DestinationLocation;

                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg));
                }
                #endregion

                string validationError = "The 'From' and 'To' locations are located too close together.";

                ThrowError(validationError,
                       TDExceptionIdentifier.JPJourneyParametersParseError);


            }

            if (viaLocation != null)
            {
                //check origin and via locations for overlapping naptans
                if (fromLocation.Intersects(viaLocation, StationType.Undetermined))
                {
                    #region Log
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        string logMsg = "EES CyclePlanner - CycleJourneyRequestHelper. There are overlapping naptans for the origin and destination locations. Origin location: " +
                                        request.OriginLocation + " Destination location: "
                                        + request.DestinationLocation;

                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg));
                    }
                    #endregion

                    string validationError = "The 'From' and 'Via' locations are located too close together.";

                    ThrowError(validationError,
                           new string[] { fromLocation.Description, toLocation.Description },
                           TDExceptionIdentifier.JPJourneyParametersParseError);

                }

                //check destination and via locations for overlapping naptans
                if (toLocation.Intersects(viaLocation, StationType.Undetermined))
                {
                    #region Log
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        string logMsg = "EES CyclePlanner - CycleJourneyRequestHelper. There are overlapping naptans for the origin and destination locations. Origin location: " +
                                        request.OriginLocation + " Destination location: "
                                        + request.DestinationLocation;

                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg));
                    }
                    #endregion

                    string validationError = "The 'Via' and 'To' locations are located too close together.";

                    ThrowError(validationError,
                           new string[] { fromLocation.Description, toLocation.Description },
                           TDExceptionIdentifier.JPJourneyParametersParseError);

                }
            }
        }

        /// <summary>
        /// validates the from, to and via locations and throws eror if duplicate location
        /// </summary>
        protected void CheckLocationsForDuplication()
        {
            //location objects used for the origin and destination
            TDLocation fromLocation = null;
            TDLocation toLocation = null;
            TDLocation viaLocation = null;

            //assign the start and destination locations 
            fromLocation = request.OriginLocation;
            toLocation = request.DestinationLocation;
            if (request.CycleViaLocations != null && request.CycleViaLocations.Length > 0)
            {
                viaLocation = request.CycleViaLocations[0]; // Use the via location specified
            }
            

            if ((fromLocation.GridReference.Easting == toLocation.GridReference.Easting &&
                fromLocation.GridReference.Northing == toLocation.GridReference.Northing))
            {
                //if any duplicates were found output an error message to the user.
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    "The 'From' and 'To' locations are identical."));

                string validationError = "The 'From' and 'To' locations are identical.";

                ThrowError(validationError,
                       new string[] { fromLocation.Description, toLocation.Description },
                       TDExceptionIdentifier.JPJourneyParametersParseError);
            }

            if (viaLocation != null)
            {

                if ((fromLocation.GridReference.Easting == viaLocation.GridReference.Easting &&
                    fromLocation.GridReference.Northing == viaLocation.GridReference.Northing))
                {
                    //if any duplicates were found output an error message to the user.
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "The 'From' and 'Via' locations are identical."));

                    string validationError = "The 'From' and 'Via' locations are identical.";

                    ThrowError(validationError,
                           new string[] { fromLocation.Description, toLocation.Description },
                           TDExceptionIdentifier.JPJourneyParametersParseError);
                }

                if ((toLocation.GridReference.Easting == viaLocation.GridReference.Easting &&
                    toLocation.GridReference.Northing == viaLocation.GridReference.Northing))
                {
                    //if any duplicates were found output an error message to the user.
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "The 'To' and 'Via' locations are identical."));

                    string validationError = "The 'Via' and 'To' locations are identical.";

                    ThrowError(validationError,
                           new string[] { fromLocation.Description, toLocation.Description },
                           TDExceptionIdentifier.JPJourneyParametersParseError);
                }
            }

        }


        
        /// <summary>
        /// Populate locality data into relevant object hierarchy
        /// </summary>
        /// <param name="osgr"></param>
        /// <returns></returns>
        private string PopulateLocality(OSGridReference osgr)
        {
            IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

            return gisQuery.FindNearestLocality(osgr.Easting, osgr.Northing);
        }

		#endregion
	}
}
