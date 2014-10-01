// *********************************************** 
// NAME			: InternationalJourneyResultConverter.cs
// AUTHOR		: Amit Patel
// DATE CREATED	: 08 Feb 2010
// DESCRIPTION	: Converts the International Planner result journeys so it can be added to TD result
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlannerControl/InternationalJourneyResultConverter.cs-arc  $
//
//   Rev 1.12   Mar 17 2010 15:25:52   mmodi
//Do not remove "Station" from the end of the stop name
//Resolution for 5465: TD Extra - "Station" should not be removed from the stop names displayed
//
//   Rev 1.11   Mar 17 2010 14:55:42   mmodi
//Corrected setting up the Service Detail object
//Resolution for 5464: TD Extra - London to Calais train shows incorrect destination in instruction
//
//   Rev 1.10   Feb 25 2010 14:32:16   mmodi
//Build location using the name description from hte international result if available
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.9   Feb 24 2010 14:47:04   mmodi
//Populate vehicle features properties of the detail
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.8   Feb 19 2010 12:11:26   mmodi
//Updated to build road journey
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.7   Feb 19 2010 10:34:52   rbroddle
//Added Distance in GetPublicJourneyDetail.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.6   Feb 18 2010 15:50:06   mmodi
//Corrected setting up legs and intermediate legs
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.5   Feb 16 2010 17:48:18   mmodi
//Updated convert logic
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.4   Feb 14 2010 10:33:50   apatel
//Set the region for air international journey
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.3   Feb 13 2010 08:30:02   apatel
//International Planner Code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Feb 12 2010 11:13:24   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.1   Feb 11 2010 08:53:12   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Feb 09 2010 09:33:50   apatel
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.UserPortal.InternationalPlanner;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.InternationalPlannerControl
{
    /// <summary>
    /// Converts the International Planner result journeys so it can be added to TD result
    /// </summary>
    public class InternationalJourneyResultConverter
    {
        #region Private Fields

        private IInternationalPlannerResult ipResult = null;
        private List<TransportDirect.UserPortal.JourneyControl.PublicJourney> publicJourneys = new List<TransportDirect.UserPortal.JourneyControl.PublicJourney>();
        private List<RoadJourney> roadJourneys = new List<RoadJourney>();

        #endregion

        #region Properties

        /// <summary>
        /// Public journeys found in the international result
        /// </summary>
        public List<TransportDirect.UserPortal.JourneyControl.PublicJourney> PublicJourneys
        {
            get
            {
                return publicJourneys;
            }
        }

        /// <summary>
        /// Road journeys found in the international result
        /// </summary>
        public List<RoadJourney> RoadJourneys
        {
            get
            {
                return roadJourneys;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor takes InternationalPlannerResult object
        /// </summary>
        /// <param name="result"></param>
        public InternationalJourneyResultConverter(IInternationalPlannerResult result)
        {
            this.ipResult = result;

            GetTDPublicJourneys();
            GetTDRoadJourneys();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Get all air, rail and coach journeys in the result and convert them to TD public journeys
        /// </summary>
        private void GetTDPublicJourneys()
        {
            foreach (InternationalJourney iJourney in ipResult.InternationalJourneys)
            {
                if(iJourney.ModeType == InternationalModeType.Air
                    || iJourney.ModeType == InternationalModeType.Coach
                    || iJourney.ModeType == InternationalModeType.Rail)
                {
                    TransportDirect.UserPortal.JourneyControl.PublicJourney pJourney = new TransportDirect.UserPortal.JourneyControl.PublicJourney();

                    List<PublicJourneyDetail> journeyDetail = new List<PublicJourneyDetail>();

                    foreach (InternationalJourneyDetail iJourneyDetail in iJourney.JourneyDetails)
                    {

                        PublicJourneyDetail publicJourneyDetail = GetPublicJourneyDetail(iJourneyDetail);

                        journeyDetail.Add(publicJourneyDetail);

                    }

                    pJourney.JourneyDate = new TDDateTime(iJourney.DepartureDateTime);
                    pJourney.Details = journeyDetail.ToArray();

                    // Set the Duration using the international journey calculated duration - this is 
                    // because the intl journey can go through different time zones, hence it calculates the 
                    // overall duration
                    pJourney.Duration = new TimeSpan(0, Convert.ToInt32(iJourney.DurationMinutes), 0);

                    publicJourneys.Add(pJourney);
                }
            }
        }

        /// <summary>
        /// Get the road journeys in the International journey result
        /// </summary>
        private void GetTDRoadJourneys()
        {
            // Build up the road journey. The International planner only returns a "token" journey
            // which has a start and end, duration, emissions, but no directions.
            // This is to allow the Journey Overview page to show a road journey comparison overview line,
            // and nothing more.
            foreach (InternationalJourney iJourney in ipResult.InternationalJourneys)
            {
                if (iJourney.ModeType == InternationalModeType.Car)
                {
                    if ((iJourney.JourneyDetails != null) && (iJourney.JourneyDetails.Length > 0))
                    {
                        RoadJourney rJourney = new RoadJourney();

                        // There should only be one journey detail
                        PrivateJourneyDetail privateJourneyDetail = GetPrivateJourneyDetail(iJourney.JourneyDetails[0]);

                        rJourney.PrivateJourneyDetails = new PrivateJourneyDetail[1] { privateJourneyDetail };

                        rJourney.TotalDuration = (long)iJourney.DurationMinutes * 60;
                        
                        rJourney.Emissions = RoundEmissions(iJourney.Emissions);
                        
                        roadJourneys.Add(rJourney);
                    }
                }
            }
        }

        #region Helpers

        /// <summary>
        /// Get the public journey detail for the International journey
        /// </summary>
        /// <param name="iJourneyDetail">InternationalJourneyDetail object</param>
        /// <returns>PublicJourneyDetail object</returns>
        private PublicJourneyDetail GetPublicJourneyDetail(InternationalJourneyDetail iJourneyDetail)
        {
            PublicJourneyTimedDetail journeyDetail = new PublicJourneyTimedDetail();
                        
            #region Leg start and Leg end of detail

            // Determine the calling point types for the Leg start and end
            PublicJourneyCallingPointType startType = PublicJourneyCallingPointType.OriginAndBoard;
            PublicJourneyCallingPointType endType = PublicJourneyCallingPointType.DestinationAndAlight;

            // If there are no intermediates, then start and end types are ok as above.
            // Assume our leg start and end naptans appear in the IntermediatesLeg array
            if (iJourneyDetail.IntermediatesBefore.Length > 0)
            {
                // This means the service origin is somewhere before we board
                startType = PublicJourneyCallingPointType.Board;
            }
            if (iJourneyDetail.IntermediatesAfter.Length > 0)
            {
                // This means the service destination is somewhere after we alight
                endType = PublicJourneyCallingPointType.Alight;
            }

            // Find the intermediate leg for the start and end stops, so the depart/arrive times can be correctly set.
            // These will exist in the IntermediateLegs array.
            // This is to ensure if the service details page is displayed, then the service times are correctly shown
            DateTime startlegArriveDateTime = DateTime.MinValue; 
            DateTime startlegDepartDateTime = iJourneyDetail.DepartureDateTime;
            DateTime endlegArriveDateTime = iJourneyDetail.ArrivalDateTime;
            DateTime endlegDepartDateTime = DateTime.MinValue;
            foreach (InternationalJourneyCallingPoint callingPoint in iJourneyDetail.IntermediatesLeg)
            {
                if (callingPoint.StopNaptan == iJourneyDetail.DepartureStopNaptan)
                {
                    startlegArriveDateTime = callingPoint.ArrivalDateTime;
                    startlegDepartDateTime = callingPoint.DepartureDateTime;
                }
                else if (callingPoint.StopNaptan == iJourneyDetail.ArrivalStopNaptan)
                {
                    endlegArriveDateTime = callingPoint.ArrivalDateTime;
                    endlegDepartDateTime = callingPoint.DepartureDateTime;
                }
            }

            // Start calling point, where we Board the service
            journeyDetail.LegStart = BuildPublicJourneyCallingPoint(iJourneyDetail.DepartureName, iJourneyDetail.DepartureStop, iJourneyDetail.DepartureCity, startlegDepartDateTime, startlegArriveDateTime, startType);
            
            // End calling point, where we Alight the service
            journeyDetail.LegEnd = BuildPublicJourneyCallingPoint(iJourneyDetail.ArrivalName, iJourneyDetail.ArrivalStop, iJourneyDetail.ArrivalCity, endlegDepartDateTime, endlegArriveDateTime, endType);

            #endregion

            #region Origin and Destination of the detail

            // The Origin is used to identify where the service actually starts from
            if (startType == PublicJourneyCallingPointType.OriginAndBoard)
            {
                journeyDetail.Origin = journeyDetail.LegStart;
            }
            else
            {
                // Otherwise service starts at another origin
                journeyDetail.Origin = BuildPublicJourneyCallingPoint(string.Empty,
                        iJourneyDetail.IntermediatesBefore[0].Stop, null, iJourneyDetail.IntermediatesBefore[0].DepartureDateTime, DateTime.MinValue, PublicJourneyCallingPointType.Origin);
            }

            // The Destination is used to identify where the service actually finishs at
            if (endType == PublicJourneyCallingPointType.DestinationAndAlight)
            {
                journeyDetail.Destination = journeyDetail.LegEnd;
            }
            else
            {
                // Otherwise service ends at another destination
                int lastIndex = iJourneyDetail.IntermediatesAfter.Length - 1;
                journeyDetail.Destination = BuildPublicJourneyCallingPoint(string.Empty,
                    iJourneyDetail.IntermediatesAfter[lastIndex].Stop, null, DateTime.MinValue, iJourneyDetail.IntermediatesAfter[lastIndex].ArrivalDateTime, PublicJourneyCallingPointType.Destination);
                
            }

            #endregion

            #region Check in/out, and duration times

            //Setting departure or arrival time for air journey only
            if (iJourneyDetail.DetailType == InternationalJourneyDetailType.TimedAir)
            {
                journeyDetail.FlightDepartDateTime = iJourneyDetail.DepartureDateTime;
                journeyDetail.FlightArriveDateTime = iJourneyDetail.ArrivalDateTime;
            }

            journeyDetail.CheckInTime = (iJourneyDetail.CheckInDateTime == DateTime.MinValue) ? null : new TDDateTime(iJourneyDetail.CheckInDateTime);

            journeyDetail.ExitTime = (iJourneyDetail.CheckOutDateTime == DateTime.MinValue) ? null : new TDDateTime(iJourneyDetail.CheckOutDateTime);

            // PublicJourneyTimedDetails requires duration in seconds 
            journeyDetail.Duration = Convert.ToInt32(iJourneyDetail.DurationMinutes * 60);

            #endregion

            #region Geometry

            // Build geometry
            List<OSGridReference> geometry = new List<OSGridReference>();

            if (iJourneyDetail.DepartureCity != null)
            {
                geometry.Add(new OSGridReference(iJourneyDetail.DepartureCity.CityOSGREasting, iJourneyDetail.DepartureCity.CityOSGRNorthing));
            }
            else if (iJourneyDetail.DepartureStop != null)
            {
                geometry.Add(new OSGridReference(iJourneyDetail.DepartureStop.StopOSGREasting, iJourneyDetail.DepartureStop.StopOSGRNorthing));
            }

            if (iJourneyDetail.ArrivalCity != null)
            {
                geometry.Add(new OSGridReference(iJourneyDetail.ArrivalCity.CityOSGREasting, iJourneyDetail.ArrivalCity.CityOSGRNorthing));
            }
            else if (iJourneyDetail.ArrivalStop != null)
            {
                geometry.Add(new OSGridReference(iJourneyDetail.ArrivalStop.StopOSGREasting, iJourneyDetail.ArrivalStop.StopOSGRNorthing));
            }

            journeyDetail.Geometry = geometry.ToArray();

            #endregion
                        
            #region Service details

            List<ServiceDetails> serviceDetails = new List<ServiceDetails>();

            // Get service details
            serviceDetails.Add(GetServiceDetail(iJourneyDetail));

            journeyDetail.Services = serviceDetails.ToArray();

            #endregion

            #region Service facilities

            if ((iJourneyDetail.ServiceFacilities != null) && (iJourneyDetail.ServiceFacilities.Length > 0))
            {
                journeyDetail.VehicleFeatures = iJourneyDetail.ServiceFacilities;
            }

            #endregion

            #region Mode

            switch (iJourneyDetail.DetailType)
            {
                case InternationalJourneyDetailType.TimedAir:
                    journeyDetail.Mode = ModeType.Air;
                    break;
                case InternationalJourneyDetailType.TimedRail:
                    journeyDetail.Mode = ModeType.Rail;
                    break;
                case InternationalJourneyDetailType.TimedCoach:
                    journeyDetail.Mode = ModeType.Coach;
                    break;
                default:
                    journeyDetail.Mode = ModeType.Transfer;
                    break;
            }
            
            #endregion

            #region Intermediate calling points for the service

            DateTime arriveTime = DateTime.MinValue;
            DateTime departTime = DateTime.MinValue;

            // Used to ignore the date part and keep time part only
            int year = DateTime.MinValue.Year;
            int month = DateTime.MinValue.Month;
            int day = DateTime.MinValue.Day;

            #region Before

            if ((iJourneyDetail.IntermediatesBefore != null) && (iJourneyDetail.IntermediatesBefore.Length > 0))
            {
                List<PublicJourneyCallingPoint> callingPoints = new List<PublicJourneyCallingPoint>();

                for (int i = 0; i < iJourneyDetail.IntermediatesBefore.Length; i++)
                {
                    // All international journey service points are a CallingPoint type
                    PublicJourneyCallingPointType type = PublicJourneyCallingPointType.CallingPoint;

                    if (iJourneyDetail.IntermediatesBefore[i].Type == CallingPointType.Origin)
                    {
                        // Do not add the origin calling point - this is in journeyDetail.Origin above
                        continue;
                    }
                    else if (iJourneyDetail.IntermediatesBefore[i].Type == CallingPointType.Destination)
                    {
                        // Do not add the destination calling point - this is in journeyDetail.Destination above
                        continue;
                    }
                    else
                    {
                        arriveTime = new DateTime(year, month, day,
                            iJourneyDetail.IntermediatesBefore[i].ArrivalDateTime.Hour,
                            iJourneyDetail.IntermediatesBefore[i].ArrivalDateTime.Minute,
                            0);
                        departTime = new DateTime(year, month, day,
                            iJourneyDetail.IntermediatesBefore[i].DepartureDateTime.Hour,
                            iJourneyDetail.IntermediatesBefore[i].DepartureDateTime.Minute,
                            0);
                    }

                    // Add the calling point
                    callingPoints.Add(
                        BuildPublicJourneyCallingPoint(string.Empty,
                        iJourneyDetail.IntermediatesBefore[i].Stop, null, departTime, arriveTime, type));
                }

                journeyDetail.IntermediatesBefore = callingPoints.ToArray();
            }
            else
            {
                journeyDetail.IntermediatesBefore = new PublicJourneyCallingPoint[0];
            }

            #endregion

            #region During legs

            if ((iJourneyDetail.IntermediatesLeg != null) && (iJourneyDetail.IntermediatesLeg.Length > 0))
            {
                List<PublicJourneyCallingPoint> callingPoints = new List<PublicJourneyCallingPoint>();

                for (int i = 0; i < iJourneyDetail.IntermediatesLeg.Length; i++)
                {
                    // All international journey service points are a CallingPoint type
                    PublicJourneyCallingPointType type = PublicJourneyCallingPointType.CallingPoint;

                    if (iJourneyDetail.IntermediatesLeg[i].Type == CallingPointType.Origin)
                    {
                        // Do not add the origin calling point - this is in journeyDetail.Origin above
                        continue;
                    }
                    else if (iJourneyDetail.IntermediatesLeg[i].Type == CallingPointType.Destination)
                    {
                        // Do not add the destination calling point - this is in journeyDetail.Destination above
                        continue;
                    }
                    else
                    {
                        arriveTime = new DateTime(year, month, day,
                            iJourneyDetail.IntermediatesLeg[i].ArrivalDateTime.Hour,
                            iJourneyDetail.IntermediatesLeg[i].ArrivalDateTime.Minute,
                            0);
                        departTime = new DateTime(year, month, day,
                            iJourneyDetail.IntermediatesLeg[i].DepartureDateTime.Hour,
                            iJourneyDetail.IntermediatesLeg[i].DepartureDateTime.Minute,
                            0);
                    }

                    // Add the calling point (only if its not stopping at our start or end leg),
                    // otherwise the service details page will incorrectly show it as a calling point
                    string stopNaptan = iJourneyDetail.IntermediatesLeg[i].StopNaptan;

                    if ((stopNaptan != iJourneyDetail.DepartureStopNaptan) &&
                        (stopNaptan != iJourneyDetail.ArrivalStopNaptan))
                    {
                        callingPoints.Add(
                            BuildPublicJourneyCallingPoint(string.Empty,
                            iJourneyDetail.IntermediatesLeg[i].Stop, null, departTime, arriveTime, type));
                    }
                }

                journeyDetail.IntermediatesLeg = callingPoints.ToArray();
            }
            else
            {
                journeyDetail.IntermediatesLeg = new PublicJourneyCallingPoint[0];
            }

            #endregion

            #region After

            if ((iJourneyDetail.IntermediatesAfter != null) && (iJourneyDetail.IntermediatesAfter.Length > 0))
            {
                List<PublicJourneyCallingPoint> callingPoints = new List<PublicJourneyCallingPoint>();

                for (int i = 0; i < iJourneyDetail.IntermediatesAfter.Length; i++)
                {
                    // All international journey service points are a CallingPoint type
                    PublicJourneyCallingPointType type = PublicJourneyCallingPointType.CallingPoint;

                    if (iJourneyDetail.IntermediatesAfter[i].Type == CallingPointType.Origin)
                    {
                        // Do not add the origin calling point - this is in journeyDetail.Origin above
                        continue;
                    }
                    else if (iJourneyDetail.IntermediatesAfter[i].Type == CallingPointType.Destination)
                    {
                        // Do not add the destination calling point - this is in journeyDetail.Destination above
                        continue;
                    }
                    else
                    {
                        arriveTime = new DateTime(year, month, day,
                            iJourneyDetail.IntermediatesAfter[i].ArrivalDateTime.Hour,
                            iJourneyDetail.IntermediatesAfter[i].ArrivalDateTime.Minute,
                            0);
                        departTime = new DateTime(year, month, day,
                            iJourneyDetail.IntermediatesAfter[i].DepartureDateTime.Hour,
                            iJourneyDetail.IntermediatesAfter[i].DepartureDateTime.Minute,
                            0);
                    }

                    // Add the calling point
                    callingPoints.Add(
                        BuildPublicJourneyCallingPoint(string.Empty,
                        iJourneyDetail.IntermediatesAfter[i].Stop, null, departTime, arriveTime, type));
                }

                journeyDetail.IntermediatesAfter = callingPoints.ToArray();
            }
            else
            {
                journeyDetail.IntermediatesAfter = new PublicJourneyCallingPoint[0];
            }

            #endregion

            #endregion

            #region Notes and transfer information

            if (iJourneyDetail.DetailType == InternationalJourneyDetailType.Transfer)
            {
                journeyDetail.TransferDescription = iJourneyDetail.TransferInfo;
            }

            #endregion

            #region Region 

            // Set the region, this is used to ensure any zonal operator links are shown for the location
            if (iJourneyDetail.DetailType == InternationalJourneyDetailType.TimedAir)
            {
                journeyDetail.Region = "air"; 
            }

            #endregion

            #region Via location

            // No via location in any of the details
            journeyDetail.IncludesVia = false;

            #endregion

            #region Distance

            journeyDetail.Distance = iJourneyDetail.Distance;

            #endregion
            
            return journeyDetail;
        }

        /// <summary>
        /// Extracts and builds ServiceDetail object out of the international journey detail
        /// </summary>
        /// <param name="iJourneyDetail">International journey detail object</param>
        /// <returns></returns>
        private ServiceDetails GetServiceDetail(InternationalJourneyDetail iJourneyDetail)
        {
            ServiceDetails serviceDetail = new ServiceDetails(
                iJourneyDetail.OperatorCode,
                null, 
                iJourneyDetail.ServiceNumber,
                null,
                null,
                iJourneyDetail.AircraftTypeCode,
                null);

            return serviceDetail;

        }

        /// <summary>
        /// Build public calling point for the international stop or city
        /// </summary>
        /// <param name="description">Description to use for name</param>
        /// <param name="internationalStop">International stop</param>
        /// <param name="internationalCity">International city</param>
        /// <param name="departueDateTime">departure time</param>
        /// <param name="arrivalDateTime">arrival time</param>
        /// <param name="callingPointType">type of calling point</param>
        /// <returns></returns>
        private PublicJourneyCallingPoint BuildPublicJourneyCallingPoint(string description, InternationalStop internationalStop, InternationalCity internationalCity, DateTime departueDateTime, DateTime arrivalDateTime, PublicJourneyCallingPointType callingPointType)
        {
            // Build the location to add to the calling point
            TDLocation location = BuildTDLocation(description, internationalStop, internationalCity);

            // And finally create the calling point to return
            PublicJourneyCallingPoint callingPoint = new PublicJourneyCallingPoint(location, new TDDateTime(arrivalDateTime), new TDDateTime(departueDateTime), callingPointType);

            if (internationalStop != null)
            {
                callingPoint.Information = internationalStop.StopInfoDesc;
                callingPoint.InformationURL = internationalStop.StopInfoURL;
                callingPoint.AccessibilityInfo = internationalStop.StopAccessDesc;
                callingPoint.AccessibilityURL = internationalStop.StopAccessURL;
                callingPoint.DepartureInfo = internationalStop.StopDeptDesc;
                callingPoint.DepartureURL = internationalStop.StopDeptURL;
                callingPoint.ArrivalInfo = internationalStop.StopDeptDesc;
                callingPoint.ArrivalURL = internationalStop.StopDeptURL;
            }

            callingPoint.CityInformationURL = (internationalCity == null) ? string.Empty : internationalCity.CityInfoURL;
                        
            return callingPoint;
        }

        /// <summary>
        /// Method to create a TDLocation using InternationalStop or InternationalCity data
        /// </summary>
        /// <param name="internationalStop"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        private TDLocation BuildTDLocation(string description, InternationalStop internationalStop, InternationalCity internationalCity)
        {
            TDLocation location = new TDLocation();

            // Build up using the stop first
            if (internationalStop != null)
            {
                // Test if this is an internal naptan, and use these details if found
                NaptanCacheEntry naptanCacheEntry = GetNaptan(internationalStop.StopNaptan, internationalStop.StopName);

                // Use the location UpdateLocationNaptan which will populate the naptan and if it 
                // is an airport naptan, then it will populate the actual naptan using the AirDataProvider
                if (naptanCacheEntry.Found)
                {
                    // This international stop has an internal TD recognised naptan
                    location.UpdateLocationNaptan(naptanCacheEntry.Naptan,
                        PublicJourneyDetail.TidyStationName(naptanCacheEntry.Description, false),
                        naptanCacheEntry.OSGR);

                    location.Locality = naptanCacheEntry.Locality;
                }
                else
                {
                    // Not found i.e. will be an external (e.g. a Paris station) naptan, so use the international 
                    // stop details
                    location.UpdateLocationNaptan(internationalStop.StopNaptan,
                        PublicJourneyDetail.TidyStationName(internationalStop.StopName, false),
                        new OSGridReference(internationalStop.StopOSGREasting, internationalStop.StopOSGRNorthing));
                }

                // Convert the international country in to a TDCountry
                location.Country = GetTDCountry(internationalStop.StopCountry);
            }
            else if (internationalCity != null)
            {
                // If description specified and not same as city name, use that
                if (!string.IsNullOrEmpty(description) && !description.Equals(internationalCity.CityName))
                {
                    location.Description = description;
                }
                else
                {
                    // Description becomes city name, with no changes needed
                    location.Description = internationalCity.CityName;
                }

                location.GridReference = new OSGridReference(internationalCity.CityOSGREasting, internationalCity.CityOSGRNorthing);

                // Convert the international country in to a TDCountry
                location.Country = GetTDCountry(internationalCity.CityCountry);
            }

            location.Status = TDLocationStatus.Valid;

            location.CityId = (internationalCity == null) ? string.Empty : internationalCity.CityID;

            return location;
        }

        /// <summary>
        /// Returns the naptan details for the specified naptan from the global cache
        /// </summary>
        /// <param name="naptan"></param>
        private NaptanCacheEntry GetNaptan(string naptan, string description)
        {
            // Lookup naptan in cache and/or GIS query ...
            NaptanCacheEntry naptanCacheEntry = NaptanLookup.Get(naptan, description);

            // If this (international) naptan is not found, force a dummy value to be added to the cache
            // to avoid repeated database calls to find it, and to stop Errors being logged. 
            // (Note - the NaptanLookup will always do the database call for a "not found" naptan because
            // it itself prevents the dummy "not found" naptan being added to the cache,
            // because the temp OSGR it creates for the dummy naptan is invalid!!!)
            if (!naptanCacheEntry.Found)
            {
                // Set description to be a value, otherwise the Lookup will call database again
                NaptanCacheEntry entry = new NaptanCacheEntry(naptanCacheEntry.Naptan, naptanCacheEntry.Locality,
                    "not found", naptanCacheEntry.OSGR, false);

                NaptanCache.Add(entry, true);
            }

            return naptanCacheEntry;
        }
        
        /// <summary>
        /// Converts an International Country into a TD Country
        /// </summary>
        /// <param name="iCountry"></param>
        /// <returns></returns>
        private TDCountry GetTDCountry(InternationalCountry iCountry)
        {
            TDCountry country = new TDCountry();
            country.CountryCode = iCountry.CountryCode;
            country.IANACode = iCountry.CountryCodeIANA;
            country.AdminCodeUIC = iCountry.AdminCodeUIC;
            country.TimeZone = iCountry.TimeZone;

            return country;
        }

        /// <summary>
        /// Gets a private journey detail for the International journey
        /// </summary>
        private PrivateJourneyDetail GetPrivateJourneyDetail(InternationalJourneyDetail iJourneyDetail)
        {
            TDLocation departLocation = BuildTDLocation(iJourneyDetail.DepartureName, iJourneyDetail.DepartureStop, iJourneyDetail.DepartureCity);
            TDLocation arriveLocation = BuildTDLocation(iJourneyDetail.ArrivalName, iJourneyDetail.ArrivalStop, iJourneyDetail.ArrivalCity);

            TDDateTime departDateTime = new TDDateTime(iJourneyDetail.DepartureDateTime);
            TDDateTime arriveDateTime = new TDDateTime(iJourneyDetail.ArrivalDateTime);

            PrivateJourneyDetail privateJourneyDetail = new PrivateJourneyDetail(
                ModeType.Car, departLocation, arriveLocation, departDateTime, arriveDateTime);

            return privateJourneyDetail;
        }

        /// <summary>
        /// Rounds emissions to one or two decimal place and converts from Grams to Kg's
        /// </summary>
        /// <param name="emissions"></param>
        /// <returns></returns>
        private double RoundEmissions(double emissions)
        {
            if (emissions > 0)
            {
                // convert to kg and round emissions value to 1 decimal place
                emissions = emissions / 1000;
                if (emissions >= Convert.ToDouble(0.05))
                    emissions = Math.Round(emissions, 1);
                else // Prevents the posibility of returning 0 emissions, when is actualy 0.04 or less
                    emissions = Math.Round(emissions, 2);

                // remove any trailing zeros
                string emissionsString = emissions.ToString();
                emissions = Convert.ToDouble(emissions);

                return emissions;
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #endregion
    }
}
