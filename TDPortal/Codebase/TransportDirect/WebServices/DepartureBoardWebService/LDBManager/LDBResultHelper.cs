// *********************************************** 
// NAME             : LDBResultHelper.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 29 Nov 2013
// DESCRIPTION  	: LDBResultHelper class to provide methods to convert an LDB result into DBWS objects
// ************************************************
// 

using System;
using System.Collections.Generic;
using TransportDirect.WebService.DepartureBoardWebService.DataTransfer;
using LDB = TransportDirect.WebService.DepartureBoardWebService.LDBWebService;
using TransportDirect.Common;

namespace TransportDirect.WebService.DepartureBoardWebService.LDBManager
{
    /// <summary>
    /// LDBResultHelper class to provide methods to convert an LDB result into DBWS objects
    /// </summary>
    public class LDBResultHelper
    {
        #region Private variables

        private DBWSRequest request = null;
        private LDB.StationBoard stationBoard = null;
        private LDB.ServiceDetails serviceDetail = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public LDBResultHelper()
        {
        }

        /// <summary>
        /// Constructor. StationBoard
        /// </summary>
        public LDBResultHelper(DBWSRequest request, LDB.StationBoard stationBoard)
        {
            this.request = request;
            this.stationBoard = stationBoard;
        }

        /// <summary>
        /// Constructor. ServiceDetail
        /// </summary>
        public LDBResultHelper(DBWSRequest request, LDB.ServiceDetails serviceDetail)
        {
            this.request = request;
            this.serviceDetail = serviceDetail;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns a DBWSResult with the specified message
        /// </summary>
        public DBWSResult BuildResult(int messageCode, string messageText)
        {
            DBWSResult result = new DBWSResult();

            DBWSMessage message = new DBWSMessage(messageCode, messageText);

            result.Messages.Add(message);
            result.GeneratedAt = DateTime.Now;
            
            return result;
        }

        /// <summary>
        /// Returns a DBWSResult for the LDB StationBoard result
        /// </summary>
        public DBWSResult BuildStationBoardResult()
        {
            DBWSResult result = new DBWSResult();

            if (stationBoard != null)
            {
                // Station board result OK
                result = BuildResult(Messages.LDB_Code_StationBoardOK, Messages.LDB_StationBoardOK);

                // Common result details
                result.RequestId = request.RequestId;
                result.Location = GetLocation(stationBoard.locationName, stationBoard.crs, null);
                result.GeneratedAt = stationBoard.generatedAt;
                result.PlatformAvailable = stationBoard.platformAvailable;
                
                result.StationBoardMessages = GetMessages(stationBoard.nrccMessages);
                result.StationBoardServices = GetServices(stationBoard.trainServices);
            }
            else
            {
                // No station board result, return empty result object with message 
                // indicating unable to retrieve station board
                result = BuildResult((int)TDExceptionIdentifier.DBWSLDBNullStationBoardResult,
                    string.Format(Messages.LDB_NullStationBoardResult, request.Location.LocationCRS));

                // Common result details
                result.RequestId = request.RequestId;
                result.Location = request.Location;
            }

            return result;
        }

        /// <summary>
        /// Returns a DBWSResult for the LDB ServiceDetail result
        /// </summary>
        public DBWSResult BuildServiceDetailResult()
        {
            DBWSResult result = new DBWSResult();

            if (serviceDetail != null)
            {
                // Station board result OK
                result = BuildResult(Messages.LDB_Code_ServiceDetailOK, Messages.LDB_ServiceDetailOK);

                // Common result details
                result.RequestId = request.RequestId;
                result.Location = GetLocation(serviceDetail.locationName, serviceDetail.crs, null);
                result.GeneratedAt = serviceDetail.generatedAt;
                
                result.ServiceDetail = GetServiceDetail(request.ServiceId, serviceDetail);

                result.PlatformAvailable = !string.IsNullOrEmpty(result.ServiceDetail.Platform);
            }
            else
            {
                // No service details, return empty result object with message 
                // indicating unable to retrieve service details
                result = BuildResult((int)TDExceptionIdentifier.DBWSLDBNullServiceDetailResult,
                    string.Format(Messages.LDB_NullServiceDetailResult, request.ServiceId));

                // Common result details
                result.RequestId = request.RequestId;
            }

            return result;
        }
        
        #endregion

        #region Private methods

        /// <summary>
        /// Returns a location
        /// </summary>
        private DBWSLocation GetLocation(string name, string crs, string nameVia)
        {
            DBWSLocation location = new DBWSLocation();

            if (string.IsNullOrEmpty(nameVia))
                nameVia = string.Empty;

            location.LocationName = name;
            location.LocationCRS = crs;
            location.LocationNameVia = nameVia;

            return location;
        }

        /// <summary>
        /// Returns a location
        /// </summary>
        private DBWSLocation GetLocation(LDB.ServiceLocation ldbLocation)
        {
            if (ldbLocation == null)
                return null;

            return GetLocation(ldbLocation.locationName, ldbLocation.crs, ldbLocation.via);
        }

        /// <summary>
        /// Returns a location calling point
        /// </summary>
        /// <param name="ldbCallingPoint"></param>
        /// <returns></returns>
        private DBWSLocationCallingPoint GetLocation(LDB.CallingPoint ldbCallingPoint)
        {
            if (ldbCallingPoint == null)
                return null;

            // Get standard DBWSLocation and cast as calling point
            DBWSLocationCallingPoint callingPoint = new DBWSLocationCallingPoint();
            
            callingPoint.LocationName = ldbCallingPoint.locationName;
            callingPoint.LocationCRS = ldbCallingPoint.crs;
            callingPoint.LocationNameVia = string.Empty;

            callingPoint.TimeScheduled = ldbCallingPoint.st;
            callingPoint.TimeEstimated = ldbCallingPoint.et;
            callingPoint.TimeActual = ldbCallingPoint.at;
            
            if (ldbCallingPoint.adhocAlerts != null)
            {
                callingPoint.AdhocAlerts = new List<string>(ldbCallingPoint.adhocAlerts);
            }

            return callingPoint;
        }

        /// <summary>
        /// Returns a location list
        /// </summary>
        private List<DBWSLocation> GetLocations(LDB.ServiceLocation[] ldbLocations)
        {
            List<DBWSLocation> locations = new List<DBWSLocation>();

            if (ldbLocations != null && ldbLocations.Length > 0)
            {
                foreach (LDB.ServiceLocation ldbLocation in ldbLocations)
                {
                    locations.Add(GetLocation(ldbLocation));
                }
            }

            return locations;
        }

        /// <summary>
        /// Returns a location calling points list
        /// </summary>
        private List<DBWSLocationCallingPoint> GetLocations(LDB.CallingPoint[] ldbCallingPoints)
        {
            // New list of calling points
            List<DBWSLocationCallingPoint> serviceCallingPoints = new List<DBWSLocationCallingPoint>();

            if (ldbCallingPoints != null && ldbCallingPoints.Length > 0)
            {
                foreach (LDB.CallingPoint callingPoint in ldbCallingPoints)
                {
                    DBWSLocationCallingPoint location = GetLocation(callingPoint);

                    if (location != null)
                    {
                        serviceCallingPoints.Add(location);
                    }
                }
            }

            return serviceCallingPoints;
        }

        /// <summary>
        /// Returns a messages list
        /// </summary>
        private List<DBWSMessage> GetMessages(LDB.NRCCMessage[] ldbMessages)
        {
            List<DBWSMessage> messages = new List<DBWSMessage>();

            if (ldbMessages == null || ldbMessages.Length == 0)
                return messages;

            foreach (LDB.NRCCMessage ldbMessage in ldbMessages)
            {
                messages.Add(new DBWSMessage(
                    (int)TDExceptionIdentifier.DBWSLDBStationBoardMessage,
                    ldbMessage.Value)); // May need html decoding as contains tags, e.g. &lt;P&gt;This is a test alert 2&amp;nbsp;. &lt;A href="http://
            }

            return messages;
        }

        /// <summary>
        /// Returns a services list
        /// </summary>
        private List<DBWSService> GetServices(LDB.ServiceItem[] ldbServices)
        {
            List<DBWSService> services = new List<DBWSService>();

            if (ldbServices == null || ldbServices.Length == 0)
                return services;

            DBWSService service = null;

            foreach (LDB.ServiceItem ldbService in ldbServices)
            {
                service = new DBWSService();

                service.ServiceId = ldbService.serviceID;
                service.ServiceOperator = new DBWSOperator(ldbService.operatorCode, ldbService.@operator);
                service.Platform = (string.IsNullOrEmpty(ldbService.platform)) ? string.Empty : ldbService.platform;
                service.IsCircularRoute = ldbService.isCircularRoute;

                #region Origin/Destination locations

                // Check the current live origins first. Where these exist
                // then the service may have started at a different station instead of its original origin, 
                // e.g. because of disruption/cancellation.
                // We are losing the original location information so this behaviour could be changed
                // in future if we require it
                service.OriginLocations.AddRange(GetLocations(ldbService.currentOrigins));

                if (service.OriginLocations.Count == 0)
                {
                    service.OriginLocations.AddRange(GetLocations(ldbService.origin));
                }


                service.DestinationLocations.AddRange(GetLocations(ldbService.currentDestinations));

                if (service.DestinationLocations.Count == 0)
                {
                    service.DestinationLocations.AddRange(GetLocations(ldbService.destination));
                }

                #endregion

                #region Arrival/Departure times

                service.TimeOfArrivalScheduled = (string.IsNullOrEmpty(ldbService.sta)) ? string.Empty : ldbService.sta;
                service.TimeOfArrivalEstimated = (string.IsNullOrEmpty(ldbService.eta)) ? string.Empty : ldbService.eta;
                service.TimeOfDepartureScheduled = (string.IsNullOrEmpty(ldbService.std)) ? string.Empty : ldbService.std;
                service.TimeOfDepartureEstimated = (string.IsNullOrEmpty(ldbService.etd)) ? string.Empty : ldbService.etd;

                #endregion

                services.Add(service);
            }

            return services;
        }

        /// <summary>
        /// Returns a service detail
        /// </summary>
        private DBWSServiceDetail GetServiceDetail(string serviceId, LDB.ServiceDetails ldbServiceDetail)
        {
            DBWSServiceDetail service = new DBWSServiceDetail();

            service.ServiceId = serviceId;
            service.ServiceOperator = new DBWSOperator(ldbServiceDetail.operatorCode, ldbServiceDetail.@operator);
            service.Platform = ldbServiceDetail.platform;
            service.IsCancelled = ldbServiceDetail.isCancelled;

            #region Messages

            service.OverdueMessage = ldbServiceDetail.overdueMessage;
            service.DisruptionReason = ldbServiceDetail.disruptionReason;

            if (ldbServiceDetail.adhocAlerts != null)
            {
                service.AdhocAlerts = new List<string>(ldbServiceDetail.adhocAlerts);
            }

            #endregion

            #region Previous/Subsequent calling point locations

            // Calling points are in a list of a list of calling points, to allow services which have previously
            // joined to form this service to have their calling points defined.

            if (ldbServiceDetail.previousCallingPoints != null)
            {
                foreach (LDB.ArrayOfCallingPoints callingPoints in ldbServiceDetail.previousCallingPoints)
                {
                    // Get the calling points
                    List<DBWSLocationCallingPoint> serviceCallingPoints = GetLocations(callingPoints.callingPoint);

                    // Assign to the service detail
                    if (serviceCallingPoints.Count > 0)
                        service.PreviousCallingPointLocations.Add(serviceCallingPoints);
                }
            }

            if (ldbServiceDetail.subsequentCallingPoints != null)
            {
                foreach (LDB.ArrayOfCallingPoints callingPoints in ldbServiceDetail.subsequentCallingPoints)
                {
                    // Get the calling points
                    List<DBWSLocationCallingPoint> serviceCallingPoints = GetLocations(callingPoints.callingPoint);

                    // Assign to the service detail
                    if (serviceCallingPoints.Count > 0)
                        service.SubsequentCallingPointLocations.Add(serviceCallingPoints);
                }
            }

            #endregion

            #region Arrival/Departure times

            service.TimeOfArrivalScheduled = (string.IsNullOrEmpty(ldbServiceDetail.sta)) ? string.Empty : ldbServiceDetail.sta;
            service.TimeOfArrivalEstimated = (string.IsNullOrEmpty(ldbServiceDetail.eta)) ? string.Empty : ldbServiceDetail.eta;
            service.TimeOfArrivalActual = (string.IsNullOrEmpty(ldbServiceDetail.ata)) ? string.Empty : ldbServiceDetail.ata;
            service.TimeOfDepartureScheduled = (string.IsNullOrEmpty(ldbServiceDetail.std)) ? string.Empty : ldbServiceDetail.std;
            service.TimeOfDepartureEstimated = (string.IsNullOrEmpty(ldbServiceDetail.etd)) ? string.Empty : ldbServiceDetail.etd;
            service.TimeOfDepartureActual = (string.IsNullOrEmpty(ldbServiceDetail.atd)) ? string.Empty : ldbServiceDetail.atd;
            
            #endregion

            return service;
        }

        #endregion
    }
}