// *********************************************** 
// NAME             : CJPUserInfoHelper.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Oct 2010
// DESCRIPTION  	: Helper class to provide various type of CJP info to power users
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/CJPUserInfoHelper.cs-arc  $
//
//   Rev 1.5   Apr 23 2013 10:41:40   mmodi
//Code review comments
//
//   Rev 1.4   Mar 21 2013 10:13:02   mmodi
//Updates for journey display notes filtering
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.3   Mar 19 2013 12:05:38   mmodi
//Updates to accessible icons and display of debug info from PublicJourneyDetail
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.2   Dec 10 2012 12:10:04   mmodi
//Add service detail info
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Oct 27 2010 11:16:34   apatel
//Updated to add Error handling for CJP power user additional information 
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.0   Oct 26 2010 14:33:00   apatel
//Initial revision.
//Resolution for 5623: Additional information available to CJP users
// 


using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;

using Logger = System.Diagnostics.Trace;



namespace TransportDirect.UserPortal.Web.Adapters
{

    /// <summary>
    /// Helper class to provide various type of CJP info to power users
    /// </summary>
    public class CJPUserInfoHelper
    {
        #region Constants
        private const string CJP_INFO_ALL_SWITCH = "CJPInfoControl.Visible";
        private const string CJP_INFO_TYPE_SWITCH = "CJPInfoControl.{0}.Visible";
        #endregion

        #region Private Fields
        private string displayText = string.Empty;
        private Journey journey = null;
        private JourneyLeg journeyLeg = null;
        private TDLocation tdLocation = null;
        private int legIndex = -1;
        private ITDJourneyRequest journeyRequest;
        private RoadJourney roadJourney = null;
        private RoadJourneyDetail roadJourneyDetail = null;
        private RoadUnitsEnum roadUnits;
        private TDJourneyParametersMulti journeyParams;
        private TDJourneyParametersVisitPlan journeyParamsVisitPlan;
        private ServiceDetails serviceDetail = null;

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor to initialise helper for journey and journey leg info
        /// </summary>
        /// <param name="journey"></param>
        /// <param name="journeyLeg"></param>
        /// <param name="legIndex"></param>
        public CJPUserInfoHelper(Journey journey, JourneyLeg journeyLeg, int legIndex)
        {
            this.journey = journey;
            this.journeyLeg = journeyLeg;
            this.legIndex = -legIndex;
        }

        /// <summary>
        /// Constructor initialising helper with journey request for trunk exchange points
        /// </summary>
        /// <param name="journeyRequest"></param>
        public CJPUserInfoHelper(ITDJourneyRequest journeyRequest)
        {
            this.journeyRequest = journeyRequest;
        }

        /// <summary>
        /// Constructor to initialise helper with journey parameters
        /// </summary>
        /// <param name="journeyParams"></param>
        public CJPUserInfoHelper(TDJourneyParametersMulti journeyParams)
        {
            this.journeyParams = journeyParams;
        }

        /// <summary>
        /// Constructor to initialise helper with journey parameters for visit planner
        /// </summary>
        /// <param name="journeyParams"></param>
        public CJPUserInfoHelper(TDJourneyParametersVisitPlan journeyParams)
        {
            this.journeyParamsVisitPlan = journeyParams;
        }

        /// <summary>
        /// Constructor to initialise helper for location info
        /// </summary>
        /// <param name="location"></param>
        public CJPUserInfoHelper(TDLocation location)
        {
            this.tdLocation = location;
        }

        /// <summary>
        /// Constructor to initialise helper for service detail info
        /// </summary>
        /// <param name="location"></param>
        public CJPUserInfoHelper(ServiceDetails serviceDetail)
        {
            this.serviceDetail = serviceDetail;
        }

        /// <summary>
        /// Constructor to initialise helper for road journey information
        /// </summary>
        /// <param name="roadJourney"></param>
        /// <param name="roadJourneyDetail"></param>
        /// <param name="roadUnits"></param>
        public CJPUserInfoHelper(RoadJourney roadJourney, RoadJourneyDetail roadJourneyDetail, RoadUnitsEnum roadUnits)
        {
            this.roadJourney = roadJourney;
            this.roadJourneyDetail = roadJourneyDetail;
            this.roadUnits = roadUnits;
        }
        #endregion

        #region Public Methods
        #region CJPInfo Availability
        /// <summary>
        /// Determines if CJP information available for all types of CJP info
        /// </summary>
        /// <returns></returns>
        public static bool IsCJPInformationAvailable()
        {
            return IsCJPUser() && GetBoolPropertyValue(CJP_INFO_ALL_SWITCH,true);
        }

        /// <summary>
        /// Determines if the CJP information available for specific CJP info type
        /// </summary>
        /// <param name="infoType"></param>
        /// <returns></returns>
        public static bool IsCJPInformationAvailableForType(CJPInfoType infoType)
        {
            return IsCJPInformationAvailable() && GetBoolPropertyValue(string.Format(CJP_INFO_TYPE_SWITCH, infoType), true);
        }
        #endregion

        
        /// <summary>
        /// Builds and returns CJP information for power user for specific CJP info type
        /// </summary>
        /// <param name="infoType">CJPInfoType enum value</param>
        /// <returns></returns>
        public string GetCJPInformationForType(CJPInfoType infoType)
        {
            string infoString = string.Empty;
            try
            {
                switch (infoType)
                {
                    case CJPInfoType.NaPTAN:
                        infoString = GetLoactionNaptan();
                        break;
                    case CJPInfoType.Coordinate:
                        infoString = GetLocationCoordinates();
                        break;
                    case CJPInfoType.WalkLength:
                        infoString = GetWalkLegLength();
                        break;
                    case CJPInfoType.InterchangeTime:
                        infoString = GetInterchangeTime();
                        break;
                    case CJPInfoType.DataSource:
                        infoString = GetJourneyDataSource();
                        break;
                    case CJPInfoType.TrunkExchangePoint:
                        infoString = GetTrunkExchangePoint();
                        break;
                    case CJPInfoType.ServiceDetail:
                        infoString = GetServiceDetail();
                        break;
                    case CJPInfoType.JourneyDisplayNotes:
                        infoString = GetJourneyDisplayNotes();
                        break;
                    case CJPInfoType.LegDebugInfo:
                        infoString = GetJourneyLegDebugInfo();
                        break;
                }
            }
            catch (Exception ex)
            {
                // Log the exception 
                string message = "CJPUserInfoHelper threw an exception attempting to get CJP user information data."
                    + " Exception: " + ex.Message
                    + " Stacktrace: " + ex.StackTrace;

                OperationalEvent oe = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, message);
                Logger.Write(oe);
            }
            

            return infoString;

        }
        #endregion

        #region Private Methods
        #region Private helper method - CJP User status

        /// <summary>
        /// Method which returns true if user is a higher-level (e.g. CJP) user 
        /// </summary>
        private static bool IsCJPUser()
        {
            bool userIsLoggedOn = TDSessionManager.Current.Authenticated;

            // Get the user's type
            int userType = userIsLoggedOn ? (int)TDSessionManager.Current.CurrentUser.UserType : (int)TDUserType.Standard;

            return (userType > 0);
        }

        #endregion

        /// <summary>
        /// Parse the boolean property and returns boolean value
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static bool GetBoolPropertyValue(string propertyName, bool defaultValue)
        {
            bool propValue = defaultValue;

            if (!bool.TryParse(Properties.Current[propertyName], out propValue))
            {
                propValue = defaultValue;
            }
            return propValue;
        }

        /// <summary>
        /// Gets NaPTANs for the location
        /// </summary>
        /// <returns>string of NaPTANs</returns>
        private string GetLoactionNaptan()
        {
            
            if (tdLocation != null)
            {
                StringBuilder naptans = new StringBuilder();

                foreach (TDNaptan naptan in tdLocation.NaPTANs)
                {
                    naptans.AppendFormat("{0} ", naptan.Naptan);
                }

                if(!string.IsNullOrEmpty(naptans.ToString()))
                    return string.Format("NaPTANs : {0}", naptans.ToString());
            }

            return string.Empty;
           
        }

        /// <summary>
        /// Gets Coordinates of the location
        /// </summary>
        /// <returns>Coordinates as string</returns>
        private string GetLocationCoordinates()
        {
            if (tdLocation != null && tdLocation.GridReference != null
                && tdLocation.GridReference.IsValid)
            {
                string osgr = tdLocation.GridReference.Easting + "," + tdLocation.GridReference.Northing;
                
                return string.Format("Coordinates : {0}", osgr);
            }

            return string.Empty;
            
        }

        /// <summary>
        /// Gets Locality of the location
        /// </summary>
        /// <returns></returns>
        private string GetLocationLocality()
        {
            if (tdLocation != null && !string.IsNullOrEmpty(tdLocation.Locality))
            {
                return string.Format("Locality : {0}", tdLocation.Locality);
            }

            return string.Empty;

        }

        /// <summary>
        /// Calculates and returns a lengh of walk leg as string
        /// </summary>
        /// <returns></returns>
        private string GetWalkLegLength()
        {
            if (journeyLeg != null)
            {
                if (journeyLeg.Mode == TransportDirect.JourneyPlanning.CJPInterface.ModeType.Walk)
                {
                    if (!journeyLeg.HasInvalidCoordinates)
                    {
                        int distanceInMetres = journeyLeg.LegStart.Location.GridReference.DistanceFrom(journeyLeg.LegEnd.Location.GridReference);

                        return string.Format("Walk Length (In metres) : {0}", distanceInMetres);
                    }
                }

            }

            return string.Empty;
        }

        /// <summary>
        /// Gets Interchange time for the journey
        /// </summary>
        /// <returns></returns>
        private string GetInterchangeTime()
        {
            if (journeyRequest != null)
            {
                return string.Format("Interchange time : {0}", journeyRequest.InterchangeSpeed);
            }
            else if (journeyParamsVisitPlan != null)
            {
                return string.Format("Interchange time : {0}", journeyParamsVisitPlan.InterchangeSpeed);
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets speed used for section of a journey
        /// </summary>
        /// <returns></returns>
        private string GetJourneySpeed()
        {
            if (roadJourney != null && roadJourneyDetail != null)
            {
                double distance = 0;
                if (roadUnits == RoadUnitsEnum.Miles)
                {
                    distance = ConvertMetresToMileage(roadJourneyDetail.Distance);
                }
                else
                {
                    distance = ConvertMetresToKm(roadJourneyDetail.Distance);
                }

                double speed = (distance * 3600) / (double)roadJourneyDetail.Duration;

                return string.Format("Speed : {0}", speed);
            }

            return string.Empty;
        }
                
        /// <summary>
        /// Gets source of data used in public transport
        /// </summary>
        /// <returns></returns>
        private string GetJourneyDataSource()
        {
            if (journey != null && journey is PublicJourney)
            {
                if (journeyLeg != null && journeyLeg is PublicJourneyDetail)
                {
                    PublicJourneyDetail publicJourneyLeg = journeyLeg as PublicJourneyDetail;

                    string region = publicJourneyLeg.Region;

                    if (!string.IsNullOrEmpty(region))
                    {
                        return string.Format("Data source : {0}", region);
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets Trunk Exchange Points used to calculate the journey
        /// </summary>
        /// <returns></returns>
        private string GetTrunkExchangePoint()
        {
            StringBuilder tepBuilder = new StringBuilder();
            TDLocation origin = null;
            TDLocation destination = null;
            List<TDLocation> vias = new List<TDLocation>();

            if (journeyRequest != null)
            {
                origin = journeyRequest.OriginLocation;
                destination = journeyRequest.DestinationLocation;

                if(journeyRequest.PrivateViaLocation != null)
                    vias.Add(journeyRequest.PrivateViaLocation);

                if (journeyRequest.PublicViaLocations != null)
                    vias.AddRange(journeyRequest.PublicViaLocations);

            }
            else if (journeyParamsVisitPlan != null)
            {
                vias.Add(journeyParamsVisitPlan.GetLocation(0));
                vias.Add(journeyParamsVisitPlan.GetLocation(1));
                vias.Add(journeyParamsVisitPlan.GetLocation(2));
            }



            if (journeyParams != null)
            {
                origin = journeyParams.OriginLocation;
                destination = journeyParams.DestinationLocation;

                if (journeyParams.PrivateViaLocation != null)
                    vias.Add(journeyParams.PrivateViaLocation);

                if (journeyParams.PublicViaLocation != null)
                    vias.Add(journeyParams.PublicViaLocation);

                if (journeyParams.CycleViaLocation != null)
                    vias.Add(journeyParams.CycleViaLocation);
            }

            if(origin != null  || destination != null || journeyParamsVisitPlan != null)
            {
                tepBuilder.AppendFormat("Trunk Exchange Points<br/>");
                string locationInfo = string.Empty;

                if (origin != null)
                {
                    tdLocation = origin;
                    locationInfo = GetLocationInfo();
                    if(!string.IsNullOrEmpty(locationInfo))
                    {
                        tepBuilder.Append("<pre>\tOrigin:</pre>");
                        tepBuilder.Append(locationInfo);
                       
                    }
                }

                if (destination != null)
                {
                    tdLocation = destination;
                    locationInfo = GetLocationInfo();
                    if (!string.IsNullOrEmpty(locationInfo))
                    {
                        tepBuilder.Append("<pre>\tDestination:</pre>");
                        tepBuilder.Append(locationInfo);
                        
                    }
                }

                if (vias != null && vias.Count > 0)
                {
                    int count = 1;
                    foreach (TDLocation location in vias)
                    {
                        tdLocation = location;
                        locationInfo = GetLocationInfo();
                        if (!string.IsNullOrEmpty(locationInfo))
                        {
                            if (journeyParamsVisitPlan != null && journeyRequest == null)
                            {
                                tepBuilder.AppendFormat("<pre>\tLocation {0}:</pre>", count);
                            }
                            else
                            {
                                tepBuilder.Append("<pre>\tVia:</pre>");
                            }
                            tepBuilder.Append(locationInfo);
                            
                        }
                        count++;
                    }
                }

            }
            

            return tepBuilder.ToString();
        }

        /// <summary>
        /// Gets Location Information
        /// </summary>
        /// <returns></returns>
        private string GetLocationInfo()
        {
            StringBuilder infoBuilder = new StringBuilder();
            if (tdLocation != null)
            {
                string description = tdLocation.Description;
                string naptan = GetLoactionNaptan();
                string coordinates = GetLocationCoordinates();
                string locality = GetLocationLocality();

                if (!string.IsNullOrEmpty(description))
                {
                    infoBuilder.Append("<pre>\t\t");
                    infoBuilder.AppendFormat("Description : {0}", tdLocation.Description);
                    infoBuilder.Append("</pre>");
                }
                if (!string.IsNullOrEmpty(naptan))
                {
                    infoBuilder.Append("<pre>\t\t");
                    infoBuilder.AppendFormat("{0}", naptan);
                    infoBuilder.Append("</pre>");
                }
                if (!string.IsNullOrEmpty(coordinates))
                {
                    infoBuilder.Append("<pre>\t\t");
                    infoBuilder.AppendFormat("Coordinates :{0}", coordinates);
                    infoBuilder.Append("</pre>");
                }
                if (!string.IsNullOrEmpty(locality))
                {
                    infoBuilder.Append("<pre>\t\t");
                    infoBuilder.AppendFormat("Locality :{0}", locality);
                    infoBuilder.Append("</pre>");
                }
            }

            return infoBuilder.ToString();
        }

        /// <summary>
        /// Gets Service detail
        /// </summary>
        /// <returns></returns>
        private string GetServiceDetail()
        {
            StringBuilder infoBuilder = new StringBuilder();
            if (serviceDetail != null)
            {
                if (!string.IsNullOrEmpty(serviceDetail.OperatorCode))
                {
                    infoBuilder.AppendFormat("OperCode: {0} ", serviceDetail.OperatorCode);
                }
                if (!string.IsNullOrEmpty(serviceDetail.OperatorName))
                {
                    infoBuilder.AppendFormat("OperName: {0} ", serviceDetail.OperatorName);
                }
                if (!string.IsNullOrEmpty(serviceDetail.ServiceNumber))
                {
                    infoBuilder.AppendFormat("ServiceNum: {0} ", serviceDetail.ServiceNumber);
                }
                if (!string.IsNullOrEmpty(serviceDetail.RetailTrainId))
                {
                    infoBuilder.AppendFormat("TrainId: {0} ", serviceDetail.RetailTrainId);
                }
                if (!string.IsNullOrEmpty(serviceDetail.PrivateId))
                {
                    infoBuilder.AppendFormat("PrivateId: {0} ", serviceDetail.PrivateId);
                }
            }

            return infoBuilder.ToString();
        }

        /// <summary>
        /// Gets the "non-displayable" journey display notes information from a PublicJourneyDetail,
        /// to allow it to be displayed as debug info for a CJP (Type 2) logged on user
        /// </summary>
        /// <returns></returns>
        private string GetJourneyDisplayNotes()
        {
            if (journey != null && journey is PublicJourney)
            {
                if (journeyLeg != null && journeyLeg is PublicJourneyDetail)
                {
                    PublicJourneyDetail publicJourneyLeg = journeyLeg as PublicJourneyDetail;

                    NotesDisplayAdapter notesDisplayAdapter = new NotesDisplayAdapter();

                    // Find any journey notes which have been supressed/filtered out from display, to allow these
                    // to be shown for a CJP (Type 2) user
                    return notesDisplayAdapter.GetNonDisplayableNotes(journey, publicJourneyLeg); 
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the debug information from a PublicJourneyDetail
        /// </summary>
        /// <returns></returns>
        private string GetJourneyLegDebugInfo()
        {
            if (journey != null && journey is PublicJourney)
            {
                if (journeyLeg != null && journeyLeg is PublicJourneyDetail)
                {
                    PublicJourneyDetail publicJourneyLeg = journeyLeg as PublicJourneyDetail;

                    if (publicJourneyLeg.Debug != null && publicJourneyLeg.Debug.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();

                        foreach (string s in publicJourneyLeg.Debug)
                        {
                            sb.Append(s);
                            sb.Append("<br />");
                        }

                        return sb.ToString();
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Converts the given metres to a mileage
        /// </summary>
        /// <param name="metres">Metres to convert</param>
        protected virtual double ConvertMetresToMileage(int metres)
        {
            // Retrieve the conversion factor from the Properties Service.
            double conversionFactor =
                Convert.ToDouble(Properties.Current["Web.Controls.MileageConverter"], TDCultureInfo.CurrentUICulture.NumberFormat);

            return (double)metres / conversionFactor;
        }

        /// <summary>
        /// Converts the given metres to km.
        /// </summary>
        /// <param name="metres">Metres to convert</param>
        protected virtual double ConvertMetresToKm(int metres)
        {
            return (double)metres / 1000;
        }


        #endregion


    }
}
