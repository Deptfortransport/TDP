// *********************************************** 
// NAME                 : OpenJourneyPlannerAssembler.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: This class is responsible for converting between Data Transfer Object and domain
//                        objects specific to the Open Journey Planning Exposed Service.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/OpenJourneyPlannerAssembler.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:40   mturner
//Initial revision.
//
//   Rev 1.2   Apr 10 2006 16:36:10   COwczarek
//Code review rework
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//
//   Rev 1.1   Apr 07 2006 14:17:40   COwczarek
//Handle null NaPTAN ids. Add translation for notes returned by CJP.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//
//   Rev 1.0   Apr 06 2006 16:21:48   COwczarek
//Initial revision.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//

namespace TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1
{

using CJPInterface = TransportDirect.JourneyPlanning.CJPInterface;
using System;
using System.Collections;

    /// <summary>
    /// This class is responsible for converting between Data Transfer Object and domain objects 
    /// specific to the Open Journey Planning Exposed Service
    /// </summary>
	public class OpenJourneyPlannerAssembler
	{

        /// <summary>
        /// Private constructor to prevent instantiation
        /// </summary>
        private OpenJourneyPlannerAssembler() 
        {
        }

        /// <summary>
        /// This method converts a Request DTO to a JourneyRequest domain object.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
		public static CJPInterface.JourneyRequest CreateJourneyRequest(Request request, string language, string transactionId)
		{
            if (request != null) 
            {
                CJPInterface.JourneyRequest newRequest = new CJPInterface.JourneyRequest();

                newRequest.language = language;
                newRequest.modeFilter = CreateTransportModes(request.ModeFilter);
                newRequest.operatorFilter = CreateOperators(request.OperatorFilter);
                newRequest.referenceTransaction = false;
                newRequest.requestID = transactionId;
                newRequest.serviceFilter = CreateServiceFilter(request.ServiceFilter);
                newRequest.sessionID = transactionId;
                newRequest.userType = 0;
                newRequest.depart = request.Depart;
                newRequest.destination = CreateRequestPlace(request.Destination);
                newRequest.origin = CreateRequestPlace(request.Origin);
                newRequest.parkNRide = false;
                newRequest.privateParameters = null;
                newRequest.publicParameters = CreatePublicParameters(request.PublicParameters);

                return newRequest;

            } 
            else 
            {
                return null;
            }
		}

        /// <summary>
        /// This method converts a TransportModes DT object to a Modes domain object.
        /// </summary>
        /// <param name="transportModes"></param>
        /// <returns></returns>
		public static CJPInterface.Modes CreateTransportModes(TransportModes transportModes)
		{
            if (transportModes != null) 
            {
                CJPInterface.Modes newTransportModes = new CJPInterface.Modes();

                if (transportModes.Modes != null && transportModes.Modes.Length != 0) 
                {
                    // using a hashtable will allow easy addition of extra modes further on 
                    // without creating duplicate entries

                    Hashtable modeTable = new Hashtable(transportModes.Modes.Length);
                
                    for (int i=0; i < transportModes.Modes.Length; i++) 
                    {
                        modeTable.Add((CJPInterface.ModeType)Enum.
                            Parse(typeof(CJPInterface.ModeType), transportModes.Modes[i].Mode.ToString()),null);   
                    }

                    // If either bus or coach selected, ensure bus, coach and ferry selected
                    if (modeTable.ContainsKey(CJPInterface.ModeType.Bus) || 
                        modeTable.ContainsKey(CJPInterface.ModeType.Coach)) 
                    {
                        modeTable[CJPInterface.ModeType.Bus]= true;
                        modeTable[CJPInterface.ModeType.Coach] = true;
                        modeTable[CJPInterface.ModeType.Ferry] = true;
                    }

                    // If Drt is selected, ensure Bus is selected also
                    if (modeTable.ContainsKey(CJPInterface.ModeType.Drt)) 
                    {
                        modeTable[CJPInterface.ModeType.Bus]= true;
                    }

                    // convert hashtable to array

                    CJPInterface.Mode[] modesArray = new CJPInterface.Mode[modeTable.Keys.Count];
                    int j=0;
                    foreach(CJPInterface.ModeType modeType in modeTable.Keys) 
                    {
                        CJPInterface.Mode mode = new CJPInterface.Mode();
                        mode.mode = modeType;
                        modesArray[j++] = mode;
                    }

                    newTransportModes.modes = modesArray;                

                } 
                else 
                {
                    newTransportModes.modes = null;
                }

                newTransportModes.include = transportModes.Include;
                return newTransportModes;
            } 
            else 
            {
                return null;
            }
		}

        /// <summary>
        /// This method converts an Operators DT object to an Operators domain object.
        /// </summary>
        /// <param name="operators"></param>
        /// <returns></returns>
		public static CJPInterface.Operators CreateOperators(Operators operators)
		{
            if (operators != null) 
            {

                CJPInterface.Operators newOperators = new CJPInterface.Operators();

                if (operators.OperatorCodes != null && operators.OperatorCodes.Length != 0) 
                {
                    CJPInterface.OperatorCode[] operatorCodeArray = new CJPInterface.OperatorCode[operators.OperatorCodes.Length];
                
                    for (int i=0; i < operators.OperatorCodes.Length; i++) 
                    {
                        CJPInterface.OperatorCode operatorCode = new CJPInterface.OperatorCode();
                        operatorCode.operatorCode = operators.OperatorCodes[i].Code;
                        operatorCodeArray[i] = operatorCode;   
                    }

                    newOperators.operatorCodes = operatorCodeArray;

                } 
                else 
                {
                    newOperators.operatorCodes = null;
                }

                newOperators.include = operators.Include;
                
                return newOperators;

            } 
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// This method converts a ServiceFilter DT object to a ServiceFilter domain object.
        /// </summary>
        /// <param name="serviceFilter"></param>
        /// <returns></returns>
		public static CJPInterface.ServiceFilter CreateServiceFilter(ServiceFilter serviceFilter)
		{
            if (serviceFilter != null) 
            {

                CJPInterface.ServiceFilter newServiceFilter = new CJPInterface.ServiceFilter();


                if (serviceFilter.Services != null && serviceFilter.Services.Length != 0) 
                {
                    CJPInterface.RequestService[] requestServiceArray = 
                        new CJPInterface.RequestService[serviceFilter.Services.Length];
                
                    for (int i=0; i < serviceFilter.Services.Length; i++) 
                    {
                        CJPInterface.RequestService requestService;
                        if (serviceFilter.Services[i].Type == RequestServiceType.RequestServiceNumber)
                        {
                            requestService = new CJPInterface.RequestServiceNumber();
                            CJPInterface.RequestServiceNumber requestServiceNumber = 
                                requestService as CJPInterface.RequestServiceNumber;
                            requestServiceNumber.operatorCode = 
                                serviceFilter.Services[i].OperatorCode;
                            requestServiceNumber.serviceNumber = 
                                serviceFilter.Services[i].ServiceNumber;
                        }
                        else 
                        {
                            requestService = new CJPInterface.RequestServicePrivate();
                            ((CJPInterface.RequestServicePrivate)requestService).privateID = 
                                serviceFilter.Services[i].PrivateID;

                        }

                        requestService.direction = string.Empty;
                        requestServiceArray[i] = requestService;

                    }

                    newServiceFilter.services = requestServiceArray;

                } 
                else 
                {
                    newServiceFilter.services = null;
                }

                newServiceFilter.include = serviceFilter.Include;

                return newServiceFilter;
            } 
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// This method converts a RequestPlace DT object to a RequestPlace domain object.
        /// </summary>
        /// <param name="requestPlace"></param>
        /// <returns></returns>
		public static CJPInterface.RequestPlace CreateRequestPlace(RequestPlace requestPlace)
		{
            if (requestPlace != null) 
            {
                CJPInterface.RequestPlace newRequestPlace = new CJPInterface.RequestPlace();


                newRequestPlace.coordinate = CreateCoordinate(requestPlace.Coordinate);
                newRequestPlace.givenName = requestPlace.GivenName;
                newRequestPlace.locality = requestPlace.Locality;
                newRequestPlace.roadPoints = null;

                if (requestPlace.Stops != null && requestPlace.Stops.Length != 0)
                {

                    CJPInterface.RequestStop[] requestStopArray = new 
                        CJPInterface.RequestStop[requestPlace.Stops.Length];

                    for (int i=0; i<requestPlace.Stops.Length; i++) 
                    {
                        requestStopArray[i] = CreateRequestStop(requestPlace.Stops[i]);
                    }
                    newRequestPlace.stops = requestStopArray;

                } 
                else 
                {
                    newRequestPlace.stops = null;
                }

                newRequestPlace.type = (CJPInterface.RequestPlaceType)Enum.Parse(
                    typeof(CJPInterface.RequestPlaceType), requestPlace.Type.ToString()); 

                return newRequestPlace;

            } 
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// This method converts an array of RequestPlace DT objects to an array of RequestPlace domain objects.
        /// </summary>
        /// <param name="requestPlace"></param>
        /// <returns></returns>
        public static CJPInterface.RequestPlace[] CreateRequestPlaces(RequestPlace[] requestPlace)
        {
            if (requestPlace != null && requestPlace.Length != 0) 
            {

                CJPInterface.RequestPlace[] newRequestPlaceArray = new CJPInterface.RequestPlace[requestPlace.Length];

                for (int i=0; i < requestPlace.Length; i++) 
                {
                    newRequestPlaceArray[i] = CreateRequestPlace(requestPlace[i]);
                }

                return newRequestPlaceArray;
            } 
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// This method converts a RequestStop DT object to a RequestStop domain object.
        /// </summary>
        /// <param name="requestStop"></param>
        /// <returns></returns>
        public static CJPInterface.RequestStop CreateRequestStop(RequestStop requestStop)
        {
            if (requestStop != null) 
            {

                CJPInterface.RequestStop newRequestStop = new CJPInterface.RequestStop();

                newRequestStop.coordinate = CreateCoordinate(requestStop.Coordinate);

                // CJP does not like a null being passed for NaPTANID, so set to emtpy string
                if (requestStop.NaPTANID == null) 
                {
                    newRequestStop.NaPTANID = string.Empty;
                } 
                else
                {
                    newRequestStop.NaPTANID = requestStop.NaPTANID;
                }

                newRequestStop.seed = null;
                newRequestStop.timeDate = requestStop.TimeDate;

                return newRequestStop;
            } 
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// This method converts a PublicParameters DT object to a PublicParameters domain object.
        /// </summary>
        /// <param name="publicParameters"></param>
        /// <returns></returns>
		public static CJPInterface.PublicParameters CreatePublicParameters(PublicParameters publicParameters)
		{
            if (publicParameters != null) 
            {

                CJPInterface.PublicParameters newPublicParameters = new CJPInterface.PublicParameters();

                newPublicParameters.algorithm = (CJPInterface.PublicAlgorithmType)Enum.Parse(
                    typeof(CJPInterface.PublicAlgorithmType), publicParameters.Algorithm.ToString());
                newPublicParameters.extraCheckInTime = publicParameters.ExtraCheckInTime;
                newPublicParameters.extraInterval = System.DateTime.MinValue;
                newPublicParameters.extraSequence = 0;
                newPublicParameters.interchangeSpeed = publicParameters.InterchangeSpeed;
                newPublicParameters.intermediateStops = (CJPInterface.IntermediateStopsType)Enum.Parse(
                    typeof(CJPInterface.IntermediateStopsType), publicParameters.IntermediateStops.ToString()); 
                newPublicParameters.interval = publicParameters.Interval;
                newPublicParameters.maxWalkDistance = publicParameters.MaxWalkDistance;
                newPublicParameters.notVias = CreateRequestPlaces(publicParameters.NotVias);
                newPublicParameters.rangeType = (CJPInterface.RangeType)Enum.Parse(
                    typeof(CJPInterface.RangeType), publicParameters.RangeType.ToString());
                newPublicParameters.routingStops = null;
                newPublicParameters.sequence = publicParameters.Sequence;
                newPublicParameters.softVias = CreateRequestPlaces(publicParameters.SoftVias);
                newPublicParameters.trunkPlan = publicParameters.TrunkPlan;
                newPublicParameters.vehicleFeatures = null;
                newPublicParameters.vias = CreateRequestPlaces(publicParameters.Vias);
                newPublicParameters.walkSpeed = publicParameters.WalkSpeed;

                return newPublicParameters;
            } 
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// This method converts a Coordinate DT object to a Coordinate domain object.
        /// </summary>
        /// <param name="publicParameters"></param>
        /// <returns></returns>
        public static CJPInterface.Coordinate CreateCoordinate(Coordinate coordinate)
        {
            if (coordinate != null) 
            {

                CJPInterface.Coordinate newCoordinate = new CJPInterface.Coordinate();

                newCoordinate.easting = coordinate.Easting;
                newCoordinate.northing = coordinate.Northing;

                return newCoordinate;
            } 
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// This method converts a Result domain object to a Result DT object.
        /// </summary>
        /// <param name="journeyResult"></param>
        /// <returns></returns>
		public static Result CreateResultDT(CJPInterface.JourneyResult journeyResult)
		{
            if (journeyResult != null) 
            {
                Result newJourneyResult = new Result();

                newJourneyResult.Messages = CreateMessagesDT(journeyResult.messages);
                newJourneyResult.PublicJourneys = CreatePublicJourneysDT(journeyResult.publicJourneys);

                return newJourneyResult;
            } 
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// This method converts an array of Message domain objects to an array of Message DT objects.
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
		public static Message[] CreateMessagesDT(CJPInterface.Message[] messages)
		{
            if (messages != null && messages.Length != 0) 
            {

                Message[] newMessagesArray = new Message[messages.Length];

                for (int i=0; i < messages.Length; i++) 
                {

                    Message message = new Message();

                    message.Code = messages[i].code;
                    message.Description = messages[i].description;
                    message.Type = MessageType.JourneyPlannerMessage;

                    if (messages[i] is CJPInterface.TTBOMessage) 
                    {
                        message.SubCode = ((CJPInterface.TTBOMessage)messages[i]).subCode;
                        message.Type = MessageType.RailEngineMessage;
                    } 
                    else 
                    {
                        message.SubCode = 0;
                    }

                    if (messages[i] is CJPInterface.JourneyWebMessage) 
                    {
                        message.SubClass = ((CJPInterface.JourneyWebMessage)messages[i]).subClass;
                        message.Region = ((CJPInterface.JourneyWebMessage)messages[i]).region;
                        message.Type = MessageType.JourneyWebMessage;
                    } 
                    else 
                    {
                        message.SubClass = 0;
                        message.Region = null;
                    }

                    newMessagesArray[i] = message;

                }

                return newMessagesArray;

            } 
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// This method converts an array of PublicJourney domain objects to an array of PublicJourney DT objects.
        /// </summary>
        /// <param name="publicJourneys"></param>
        /// <returns></returns>
		public static PublicJourney[] CreatePublicJourneysDT(CJPInterface.PublicJourney[] publicJourneys)
		{
            if (publicJourneys != null && publicJourneys.Length !=0) 
            {

                PublicJourney[] newPublicJourneysArray = new PublicJourney[publicJourneys.Length];

                for (int i=0; i < publicJourneys.Length; i++) 
                {

                    PublicJourney publicJourney = new PublicJourney();

                    publicJourney.Legs = CreateLegsDT(publicJourneys[i].legs);
                    newPublicJourneysArray[i] = publicJourney;

                }

                return newPublicJourneysArray;

            } 
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// This method converts an array of Leg domain objects to an array of Leg DT objects.
        /// </summary>
        /// <param name="legs"></param>
        /// <returns></returns>
		public static Leg[] CreateLegsDT(CJPInterface.Leg[] legs)
		{
            if (legs != null && legs.Length !=0) 
            {
                Leg[] newLegsArray = new Leg[legs.Length];

                for (int i=0; i < legs.Length; i++) 
                {
                    Leg leg = new Leg();

                    leg.Alight = CreateEventDT(legs[i].alight);
                    leg.Board = CreateEventDT(legs[i].board);
                    leg.Description = legs[i].description;
                    leg.Destination = CreateEventDT(legs[i].destination);
                    leg.IntermediatesA = CreateEventsDT(legs[i].intermediatesA);
                    leg.IntermediatesB = CreateEventsDT(legs[i].intermediatesB);
                    leg.IntermediatesC = CreateEventsDT(legs[i].intermediatesC);
                    leg.Mode = (TransportModeType)Enum.Parse(
                        typeof(TransportModeType), legs[i].mode.ToString());
                    
                    if (legs[i].notes != null && legs[i].notes.Length != 0) 
                    {
                        leg.Notes = new string[legs[i].notes.Length];
                        for (int j=0; j<legs[i].notes.Length;j++) 
                        {
                            leg.Notes[j] = legs[i].notes[j].message;
                        }                        
                    } 
                    else 
                    {
                        leg.Notes = null;
                    }

                    leg.Origin = CreateEventDT(legs[i].origin);
                    leg.Services = CreateServicesDT(legs[i].services);
                    leg.Validated = legs[i].validated;

                    if (legs[i].vehicleFeatures != null && legs[i].vehicleFeatures.Length != 0) 
                    {
                        leg.VehicleFeatures = new int[legs[i].vehicleFeatures.Length];
                        for (int j=0; j<legs[i].vehicleFeatures.Length;j++) 
                        {
                            leg.VehicleFeatures[j] = legs[i].vehicleFeatures[j].id;
                        }
                    } 
                    else 
                    {
                        leg.VehicleFeatures = null;
                    }

                    leg.WindowOfOpportunity = null;
                    leg.TypicalDuration = 0;
                    leg.Type = LegType.TimedLeg;
                    leg.Frequency = 0;
                    leg.MaxDuration = 0;
                    leg.MinFrequency = 0;
                    leg.MaxFrequency = 0;
                    
                    if (legs[i] is CJPInterface.FrequencyLeg)
                    {
                        leg.WindowOfOpportunity = CreateWindowOfOpportunityDT(
                            ((CJPInterface.FrequencyLeg)legs[i]).windowOfOpportunity);
                        leg.TypicalDuration = ((CJPInterface.FrequencyLeg)legs[i]).typicalDuration;
                        leg.Type = LegType.FrequencyLeg;
                        leg.Frequency = ((CJPInterface.FrequencyLeg)legs[i]).frequency;
                        leg.MaxDuration = ((CJPInterface.FrequencyLeg)legs[i]).maxDuration;
                        leg.MinFrequency = ((CJPInterface.FrequencyLeg)legs[i]).minFrequency;
                        leg.MaxFrequency = ((CJPInterface.FrequencyLeg)legs[i]).maxFrequency;
                    } 
                    else if (legs[i] is CJPInterface.ContinuousLeg)
                    {
                        leg.WindowOfOpportunity = CreateWindowOfOpportunityDT(
                            ((CJPInterface.ContinuousLeg)legs[i]).windowOfOpportunity);
                        leg.TypicalDuration = ((CJPInterface.ContinuousLeg)legs[i]).typicalDuration;
                        leg.Type = LegType.ContinuousLeg;
                    }

                    newLegsArray[i] = leg;

                }

                return newLegsArray;
            } 
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// This method converts an Event domain object to an Event DT object.
        /// </summary>
        /// <param name="stopEvent"></param>
        /// <returns></returns>
		public static Event CreateEventDT(CJPInterface.Event stopEvent)
		{
            if (stopEvent != null) 
            {

                Event newEvent = new Event();

                newEvent.Activity = (ActivityType)Enum.Parse(
                    typeof(ActivityType), stopEvent.activity.ToString());

                newEvent.ArriveTime = DateTime.MinValue;
                newEvent.ArriveTimeSpecified = false;
                newEvent.DepartTime = DateTime.MinValue;
                newEvent.DepartTimeSpecified = false;

                if (newEvent.Activity == ActivityType.Arrive || 
                    newEvent.Activity == ActivityType.ArriveDepart || 
                    newEvent.Activity == ActivityType.Request) {

                    newEvent.ArriveTime = stopEvent.arriveTime;
                    newEvent.ArriveTimeSpecified = true;
                }

                if (newEvent.Activity == ActivityType.Depart || 
                    newEvent.Activity == ActivityType.ArriveDepart || 
                    newEvent.Activity == ActivityType.Request) 
                {
                    newEvent.DepartTime = stopEvent.departTime;
                    newEvent.DepartTimeSpecified = true;
                }

                newEvent.ConfirmedVia = stopEvent.confirmedVia;
                newEvent.Geometry = CreateCoordinatesDT(stopEvent.geometry);
                newEvent.Stop = CreateStopDT(stopEvent.stop);

                return newEvent;

            } 
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// This method converts an array of Event domain objects to an array of Event DT objects.
        /// </summary>
        /// <param name="stopEvents"></param>
        /// <returns></returns>
		public static Event[] CreateEventsDT(CJPInterface.Event[] stopEvents)
		{
            if (stopEvents != null && stopEvents.Length != 0) 
            {
                Event[] newStopEventsArray = new Event[stopEvents.Length];

                for (int i=0; i < stopEvents.Length; i++) 
                {
                    newStopEventsArray[i] = CreateEventDT(stopEvents[i]);
                }

                return newStopEventsArray;
            } 
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// This method converts an array of Service domain objects to an array of Service DT objects.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
		public static Service[] CreateServicesDT(CJPInterface.Service[] services)
		{
            if (services != null && services.Length !=0) 
            {
                Service[] newServicesArray = new Service[services.Length];

                for (int i=0; i < services.Length; i++) 
                {

                    Service service = new Service();

                    if (services[i].daysOfOperation != null && services[i].daysOfOperation.Length != 0) 
                    {
                        Days[] days = new Days[services[i].daysOfOperation.Length];
                        for (int j=0; j<services[i].daysOfOperation.Length;j++) 
                        {
                            days[j] = (Days)Enum.Parse(
                                typeof(Days), services[i].daysOfOperation[j].ToString());
                        }
                        service.DaysOfOperation = days;
                    } 
                    else 
                    {
                        service.DaysOfOperation = null;
                    }
                    
                    service.DestinationBoard = services[i].destinationBoard;
                    service.ExpiryDate = services[i].expiryDate;
                    service.FirstDateOfOperation = services[i].firstDateOfOperation;
                    service.OpenEnded = services[i].openEnded;
                    service.OperatorCode = services[i].operatorCode;
                    service.OperatorName = services[i].operatorName;
                    service.PrivateID = services[i].privateID;
                    service.RetailId = services[i].retailId;
                    service.ServiceNumber = services[i].serviceNumber;

                    if (services[i].timetableLink != null) 
                    {
                        service.TimetableLinkUrl = services[i].timetableLink.URL;
                    } 
                    else 
                    {
                        services[i].timetableLink = null;
                    }

                    newServicesArray[i] = service;

                }

                return newServicesArray;

            } 
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// This method converts a WindowOfOpportunity domain object to a WindowOfOpportunity DT object.
        /// </summary>
        /// <param name="windowsOfOpportunity"></param>
        /// <returns></returns>
		public static WindowOfOpportunity CreateWindowOfOpportunityDT(CJPInterface.WindowOfOpportunity windowOfOpportunity)
		{
            if (windowOfOpportunity != null) 
            {
                WindowOfOpportunity newWindowOfOpportunity = new WindowOfOpportunity();

                newWindowOfOpportunity.End = windowOfOpportunity.end;
                newWindowOfOpportunity.Start = windowOfOpportunity.start;

                return newWindowOfOpportunity;

            } 
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// This method converts a Coordinate domain object to a Coordinate DT object.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
		public static Coordinate CreateCoordinateDT(CJPInterface.Coordinate coordinate)
		{
            if (coordinate != null) 
            {
                Coordinate newCoordinate = new Coordinate();

                newCoordinate.Northing = coordinate.northing;
                newCoordinate.Easting = coordinate.easting;

                return newCoordinate;

            } 
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// This method converts an array of Coordinate domain objects to an array of Coordinate DT objects.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public static Coordinate[] CreateCoordinatesDT(CJPInterface.Coordinate[] coordinate)
        {
            if (coordinate != null && coordinate.Length != 0) 
            {

                Coordinate[] newCoordinateArray = new Coordinate[coordinate.Length];

                for (int i=0; i < coordinate.Length; i++) 
                {
                    newCoordinateArray[i] = CreateCoordinateDT(coordinate[i]);
                }

                return newCoordinateArray;
            
            } 
            else 
            {
                return null;
            }
        }

        /// <summary>
        /// This method converts a Stop domain object to a Stop DT object.
        /// </summary>
        /// <param name="stop"></param>
        /// <returns></returns>
		public static Stop CreateStopDT(CJPInterface.Stop stop)
		{
            if (stop != null) 
            {

                Stop newStop = new Stop();

                newStop.Bay = stop.bay;
                newStop.Coordinate = CreateCoordinateDT(stop.coordinate);
                newStop.Name = stop.name;
                newStop.NaPTANID = stop.NaPTANID;
                newStop.TimingPoint = stop.timingPoint;

                return newStop;
            } 
            else 
            {
                return null;
            }
        }

	}

}
