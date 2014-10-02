// *********************************************** 
// NAME                 : PrintableEBCJourneyDetails.aspx.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 15/10/2009 
// DESCRIPTION  		: Printable EBC journey details page
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableEBCJourneyDetails.aspx.cs-arc  $
//
//   Rev 1.2   Oct 22 2009 10:51:22   apatel
//Added header info
//Resolution for 5323: CCN539 Environmental Benefit Calculator

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Common.DatabaseInfrastructure;
using System.Collections;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.LocationService;

using Logger = System.Diagnostics.Trace;
using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
using MapPoint = TransportDirect.Presentation.InteractiveMapping.Point;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.Web.JourneyPlanning
{
    public partial class PrintableEBCJourneyDetails : TDPrintablePage, INewWindowPage
    {
        #region Private Fields
        
        // Session variables
        private ITDSessionManager sessionManager;

        // State of results
        /// <summary>
        ///  True if there is an outward trip for the current selection
        /// </summary>
        private bool outwardExists = false;

        // Constants used when adding location points to the map.
        private const int STARTPOINT = 1;
        private const int ENDPOINT = 2;
        private const int VIAPOINT = 3;

        private MapHelper mapHelper = new MapHelper();

        private const string TOID_PREFIX = "JourneyControl.ToidPrefix";
		private const string MAP_ZOOM = "JourneyDetailsCarSection.Scale";

        IPropertyProvider properties = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];

        #endregion

        #region Constructor
        /// <summary>
		/// Constructor - sets the Page Id.
		/// </summary>
        public PrintableEBCJourneyDetails()
		{
            pageId = PageId.PrintableEBCJourneyDetails;
        }
        #endregion

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the session values needed by this page
            sessionManager = TDSessionManager.Current;

            InitialiseControls();

            LoadResources();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            map.Visible = false;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Establish whether we have any results
        /// </summary>
        private void DetermineStateOfResults()
        {
            //check for road journey result
            ITDJourneyResult result = sessionManager.JourneyResult;
            if (result != null)
            {
                outwardExists = ((result.OutwardRoadJourneyCount) > 0) && result.IsValid;
            }
        }

        /// <summary>
        /// This method draws road route on the map, refreshes the map and saves it to view state
        /// </summary>
        private void DrawRoadRouteAndSaveToViewState()
        {
            RoadJourney roadJourney = TDSessionManager.Current.JourneyResult.OutwardRoadJourney();
            AddRoadJourneyDataToMap();
            try
            {
                // "Map of entire journey" option has been selected.
                map.ZoomRoadRoute(Session.SessionID, roadJourney.RouteNum);
            }
            catch (PropertiesNotSetException pnse)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + pnse.Message);

                Logger.Write(operationalEvent);
            }
            catch (MapNotStartedException mnse)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + mnse.Message);

                Logger.Write(operationalEvent);
            }
            catch (ScaleOutOfRangeException soore)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + soore.Message);

                Logger.Write(operationalEvent);
            }
            catch (ScaleZeroOrNegativeException szone)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + szone.Message);

                Logger.Write(operationalEvent);
            }
            catch (RouteInvalidException rie)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + rie.Message);

                Logger.Write(operationalEvent);
            }
            catch (NoPreviousExtentException npee)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + npee.Message);

                Logger.Write(operationalEvent);
            }
            catch (MapExceptionGeneral meg)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + meg.Message);

                Logger.Write(operationalEvent);
            }
            
            this.map.Refresh();
            
            object mapObj = this.map.ExtractViewState();
            TDSessionManager.Current.StoredMapViewState[this.outwardExists ? TDSessionManager.OUTWARDMAP : TDSessionManager.RETURNMAP] = mapObj;
        }



        /// <summary>
        /// Loads text and images on the page
        /// </summary>
        private void LoadResources()
        {
            labelPrinterFriendly.Text = Global.tdResourceManager.GetString(
                "EBCPlanner.StaticPrinterFriendly.labelPrinterFriendly", TDCultureInfo.CurrentUICulture);

            labelInstructions.Visible = false;

        }

        /// <summary>
        /// Initialises controls on page with page and journey details
        /// </summary>
        private void InitialiseControls()
        {
            DetermineStateOfResults();

            if (string.IsNullOrEmpty(mapHelper.GetHighResolutionMapImageUrl(outwardExists).ImageUrl))
            {
                DrawRoadRouteAndSaveToViewState();
            }
            
            journeysSearchedForControl.UseRouteFoundForHeading = true;

            string UrlQueryString = string.Empty;
            
            //The Query params is set using javascript on the non-printable page
            UrlQueryString = Request.Params["units"];
            if (UrlQueryString == "kms")
            {
                mapOutward.CarJourneyDetails.RoadUnits = RoadUnitsEnum.Kms;
                carSummaryControl.RoadUnits = RoadUnitsEnum.Kms;
                ebcCalculationDetailsTableControl.RoadUnits = RoadUnitsEnum.Kms;
            }
            else
            {
                mapOutward.CarJourneyDetails.RoadUnits = RoadUnitsEnum.Miles;
                carSummaryControl.RoadUnits = RoadUnitsEnum.Miles;
                ebcCalculationDetailsTableControl.RoadUnits = RoadUnitsEnum.Miles;
            }


            TDItineraryManager itineraryManager = TDItineraryManager.Current;

            
            TDJourneyViewState viewState = itineraryManager.JourneyViewState;
            ITDJourneyResult result = sessionManager.JourneyResult;

            if (result != null)
            {
                labelReference.Text = result.JourneyReferenceNumber.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
            }

            // Map outward is visible only if outward results exist.
            if (outwardExists)
            {
                panelMapOutward.Visible = true;

                carSummaryControl.Initialise(result.OutwardRoadJourney(), outwardExists);

                FindEBCPageState findEBCPageState = (FindEBCPageState)sessionManager.FindPageState;
                ebcCalculationDetailsTableControl.Initialise( findEBCPageState.EnvironmentalBenefits);

                
                mapOutward.Populate(true, false, TDSessionManager.Current.IsFindAMode);

                labelReferenceTitle.Visible = (result != null);
                labelReference.Visible = (result != null);

                labelDateTime.Text = TDDateTime.Now.ToString("G");

                labelUsernameTitle.Visible = sessionManager.Authenticated;
                labelUsername.Visible = sessionManager.Authenticated;
                if (sessionManager.Authenticated)
                {
                    labelUsername.Text = sessionManager.CurrentUser.Username;
                }
            }
        }

        /// <summary>
        /// Handles the "Adjusted route" drop down for Car Journeys.
        /// </summary>
        private void AddRoadJourneyDataToMap()
        {
            ITDJourneyResult result = TDItineraryManager.Current.JourneyResult;
            TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;

            try
            {
                // Clear any existing routes on the map
                map.ClearPTRoutes();
                map.ClearRoadRoutes();
                map.ClearCycleRoute();

                RoadJourney roadJourney = result.OutwardRoadJourney();

                int startEasting = 0;
                int startNorthing = 0;
                int endEasting = 0;
                int endNorthing = 0;
                string startDescription = string.Empty;
                string endDescription = string.Empty;
                int startType = 0;
                int endType = 0;
                
                if (outwardExists)
                {
                    // Update the Mapping component on what should be displayed so
                    // that on the next OnPreRender the map is approriately refreshed.
                    

                    //Expand the routes for use
                    SqlHelper sqlHelper = new SqlHelper();
                    sqlHelper.ConnOpen(SqlHelperDatabase.EsriDB);
                    Hashtable htParameters = new Hashtable(2);
                    htParameters.Add("@SessionID", Session.SessionID);
                    htParameters.Add("@RouteNum", roadJourney.RouteNum);
                    sqlHelper.Execute("usp_ExpandRoutes", htParameters);

                    map.AddRoadRoute(Session.SessionID, roadJourney.RouteNum);
                    map.ZoomRoadRoute(Session.SessionID, roadJourney.RouteNum);

                    JourneyLeg journeyLeg = roadJourney.JourneyLegs[0];
                    JourneyLeg journeyLegEnd = roadJourney.JourneyLegs[roadJourney.JourneyLegs.Length - 1];

                    if (journeyLeg.LegStart.Location.GridReference != null
                        && journeyLeg.LegStart.Location.GridReference.Easting > 0
                        && journeyLeg.LegStart.Location.GridReference.Northing > 0)
                    {
                        startEasting = journeyLeg.LegStart.Location.GridReference.Easting;
                        startNorthing = journeyLeg.LegStart.Location.GridReference.Northing;
                        startDescription = journeyLeg.LegStart.Location.Description;
                    }
                    if (journeyLegEnd.LegEnd.Location.GridReference != null
                        && journeyLegEnd.LegEnd.Location.GridReference.Easting > 0
                        && journeyLegEnd.LegEnd.Location.GridReference.Northing > 0)
                    {
                        endEasting = journeyLegEnd.LegEnd.Location.GridReference.Easting;
                        endNorthing = journeyLegEnd.LegEnd.Location.GridReference.Northing;
                        endDescription = journeyLegEnd.LegEnd.Location.Description;
                    }

                    //Park and Ride outward journey
                    if (journeyLeg.LegEnd.Location.ParkAndRideScheme != null)
                    {
                        // Add location points to map, for start or via
                        startType = STARTPOINT;
                        AddStartEndViaPoint(startEasting, startNorthing, startDescription, startType);

                        //get CarPark details for journey TOID
                        ParkAndRideInfo parkAndRideInfo = journeyLeg.LegEnd.Location.ParkAndRideScheme;
                        journeyLeg.LegEnd.Location.CarPark = parkAndRideInfo.MatchCarPark(roadJourney.Details[roadJourney.Details.Length - 1].Toid);

                        if (journeyLeg.LegEnd.Location.CarPark != null)
                        {
                            //Add location points to map, for end or via
                            endType = ENDPOINT;
                            AddStartEndViaPoint(journeyLeg.LegEnd.Location.CarPark.GridReference.Easting, journeyLeg.LegEnd.Location.CarPark.GridReference.Northing, journeyLeg.LegEnd.Location.CarPark.CarParkName, endType);
                        }
                       
                        map.ZoomRoadRoute(Session.SessionID, roadJourney.RouteNum);

                        drawParkAndRideJourney(journeyLeg.LegStart.Location, journeyLeg.LegEnd.Location, roadJourney.RequestedViaLocation);
                    }
                    //Park and Ride return journey
                    else if (journeyLeg.LegStart.Location.ParkAndRideScheme != null)
                    {
                        //get CarPark details for journey TOID
                        ParkAndRideInfo parkAndRideInfo = journeyLeg.LegStart.Location.ParkAndRideScheme;
                        journeyLeg.LegStart.Location.CarPark = parkAndRideInfo.MatchCarPark(roadJourney.Details[0].Toid);

                        // Add location points to map, for start or via
                        startType = STARTPOINT;
                        AddStartEndViaPoint(journeyLeg.LegStart.Location.CarPark.GridReference.Easting, journeyLeg.LegStart.Location.CarPark.GridReference.Northing, journeyLeg.LegStart.Location.CarPark.CarParkName, startType);

                        if (journeyLeg.LegStart.Location.CarPark != null)
                        {
                            //Add location points to map, for end or via
                            endType = ENDPOINT;
                            AddStartEndViaPoint(journeyLeg.LegEnd.Location.GridReference.Easting, journeyLeg.LegEnd.Location.GridReference.Northing, journeyLeg.LegEnd.Location.Description, endType);
                        }

                        map.ZoomRoadRoute(Session.SessionID, roadJourney.RouteNum);

                        drawParkAndRideJourney(journeyLeg.LegStart.Location, journeyLeg.LegEnd.Location, roadJourney.RequestedViaLocation);
                    }
                    //Car Park outward or return journey
                    else if ((journeyLeg.LegEnd.Location.CarParking != null)
                        || (journeyLeg.LegStart.Location.CarParking != null))
                    {
                        // Car Park journey can use Map, Entrance, or Exit coordinates to 
                        // plan a journey From/To. We always want to display the Map coordinate 
                        // as the Start/End point on the map

                        // Add location points to map, for start or via
                        startType = STARTPOINT;

                        //Scenario where user has planned From a Car Park
                        if (journeyLeg.LegStart.Location.CarParking != null)
                        {
                            CarPark carPark = journeyLeg.LegStart.Location.CarParking;
                            OSGridReference gridreference = new OSGridReference();
                            gridreference = carPark.GetMapGridReference();

                            startDescription = FindCarParkHelper.GetCarParkName(carPark);

                            AddStartEndViaPoint(gridreference.Easting, gridreference.Northing, startDescription, startType);
                        }
                        else
                        {
                            AddStartEndViaPoint(startEasting, startNorthing, startDescription, startType);
                        }

                        //Add location points to map, for end or via
                        endType = ENDPOINT;

                        //Scenario where user has planned To a Car Park
                        if (journeyLeg.LegEnd.Location.CarParking != null)
                        {
                            CarPark carPark = journeyLeg.LegEnd.Location.CarParking;
                            OSGridReference gridreference = new OSGridReference();
                            gridreference = carPark.GetMapGridReference();

                            endDescription = FindCarParkHelper.GetCarParkName(carPark);

                            AddStartEndViaPoint(gridreference.Easting, gridreference.Northing, endDescription, endType);
                        }
                        else
                        {
                            AddStartEndViaPoint(endEasting, endNorthing, endDescription, endType);
                        }

                        map.ZoomRoadRoute(Session.SessionID, roadJourney.RouteNum);

                    }
                    else
                    {
                        // Add location points to map.
                        //IR3966:Add Start/End/Via Point to map using same logic for outward and return journeys 
                        startType = STARTPOINT;
                        AddStartEndViaPoint(startEasting, startNorthing, startDescription, startType);

                        //IR3966:Add Start/End/Via Point to map using same logic for outward and return journeys 
                        endType = ENDPOINT;
                        AddStartEndViaPoint(endEasting, endNorthing, endDescription, endType);

                        map.ZoomRoadRoute(Session.SessionID, roadJourney.RouteNum);
                    }


                }
                
            }
            catch (PropertiesNotSetException pnse)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + pnse.Message);

                Logger.Write(operationalEvent);
            }
            catch (MapNotStartedException mnse)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + mnse.Message);

                Logger.Write(operationalEvent);
            }
            catch (ScaleOutOfRangeException soore)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + soore.Message);

                Logger.Write(operationalEvent);
            }
            catch (RouteInvalidException rie)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + rie.Message);

                Logger.Write(operationalEvent);
            }
            catch (NoPreviousExtentException npee)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + npee.Message);

                Logger.Write(operationalEvent);
            }
            catch (MapExceptionGeneral meg)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + meg.Message);

                Logger.Write(operationalEvent);
            }

        }

        /// <summary>
        /// Adds start/end/via points on the map for given coordinates.
        /// </summary>
        /// <param name="easting">Easting of location</param>
        /// <param name="northing">Northing of location</param>
        /// <param name="description">Description of location</param>
        private void AddStartEndViaPoint(double easting, double northing, string description, int type)
        {
            string railPostFix = properties["Gazetteerpostfix.rail"];
            string coachPostFix = properties["Gazetteerpostfix.coach"];
            string railcoachPostFix = properties["Gazetteerpostfix.railcoach"];

            // Strip out any sub strings denoting pseudo locations
            System.Text.StringBuilder strName = new System.Text.StringBuilder(description);
            strName.Replace(railPostFix, "");
            strName.Replace(coachPostFix, "");
            strName.Replace(railcoachPostFix, "");
            description = strName.ToString();
            try
            {
                switch (type)
                {
                    case STARTPOINT:
                        if (easting > 0)
                            map.AddStartPoint(easting, northing, description);
                        break;
                    case ENDPOINT:
                        if (easting > 0)
                            map.AddEndPoint(easting, northing, description);
                        break;
                    case VIAPOINT:
                        if (easting > 0)
                            map.AddViaPoint(easting, northing, description);
                        break;
                }
            }
            catch (PropertiesNotSetException pnse)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + pnse.Message);

                Logger.Write(operationalEvent);
            }
            catch (MapNotStartedException mnse)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + mnse.Message);

                Logger.Write(operationalEvent);
            }
            catch (MapExceptionGeneral meg)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + meg.Message);

                Logger.Write(operationalEvent);
            }

        }

        /// <summary>
        /// Method to draw the symbol represtenting the Park and Ride destination and to 
        /// Create a map envelope around the entire journey. 
        /// </summary>
        /// <param name="originLocation">used to provide the grid reference of the origin</param>
        /// <param name="destinationLocation">used to provide the grid reference of the destination</param>
        /// <param name="privateViaLocation">used to provide the grid reference of the via point</param>
        private void drawParkAndRideJourney(TDLocation originLocation, TDLocation destinationLocation, TDLocation privateViaLocation)
        {
            if (outwardExists)
            {
                //Origin journey: create the symbol for the destination of the park and ride scheme. 
                map.AddSymbolPoint(
                    destinationLocation.ParkAndRideScheme.SchemeGridReference.Easting,
                    destinationLocation.ParkAndRideScheme.SchemeGridReference.Northing,
                    "CIRCLE",
                    destinationLocation.ParkAndRideScheme.Location.ToString()
                    );

                OSGridReference[] osGridArray = {originLocation.GridReference,
													 destinationLocation.CarPark.GridReference,
													 destinationLocation.ParkAndRideScheme.SchemeGridReference,
													 ((privateViaLocation !=null) ? privateViaLocation.GridReference : null)
												 };
                if (osGridArray.Length == 0)
                    return;
                //create the map envelope using the osgrids that are provided. 
                createMapEnvelope(osGridArray);
            }
            else
            {
                //Return journey: create the symbol for the origin of the park and ride scheme. 
                map.AddSymbolPoint(
                    originLocation.ParkAndRideScheme.SchemeGridReference.Easting,
                    originLocation.ParkAndRideScheme.SchemeGridReference.Northing,
                    "CIRCLE",
                    originLocation.ParkAndRideScheme.Location.ToString()
                    );

                OSGridReference[] osGridArray = {destinationLocation.GridReference,
													 originLocation.CarPark.GridReference,
													 originLocation.ParkAndRideScheme.SchemeGridReference,
													 ((privateViaLocation !=null) ? privateViaLocation.GridReference : null)
												 };
                if (osGridArray.Length == 0)
                    return;
                //create the map envelope using the osgrids that are provided. 
                createMapEnvelope(osGridArray);
            }
        }

        /// <summary>
        /// Method uses and array of osgrid reference to determine the max and min eastings and northings
        /// it then passes this information to the zoom envelopme method to create a map that includes all
        /// the listed points. 
        /// </summary>
        /// <param name="osgr">Array of osgr's used to determine the outer boundaries of the journey map</param>
        private void createMapEnvelope(OSGridReference[] osgr)
        {

            double minEasting = double.MaxValue;
            double maxEasting = double.MinValue;
            double minNorthing = double.MaxValue;
            double maxNorthing = double.MinValue;

            for (int i = 0; i < osgr.Length; i++)
            {
                if (osgr[i] != null)
                {
                    //compare Easting to current min and max
                    if (osgr[i].Easting != -1)
                    {
                        minEasting = Math.Min(osgr[i].Easting, minEasting);
                        maxEasting = Math.Max(osgr[i].Easting, maxEasting);
                    }
                    //compare Northing to current min and max
                    if (osgr[i].Northing != -1)
                    {
                        minNorthing = Math.Min(osgr[i].Northing, minNorthing);
                        maxNorthing = Math.Max(osgr[i].Northing, maxNorthing);
                    }
                }
            }

            double eastingPadding = Math.Max((maxEasting - minEasting) / 20, 300 - (maxEasting - minEasting) / 2);
            minEasting = minEasting - eastingPadding;
            maxEasting = maxEasting + eastingPadding;

            double northingPadding = Math.Max((maxNorthing - minNorthing) / 20, 300 - (maxNorthing - minNorthing) / 2);
            minNorthing = minNorthing - northingPadding;
            maxNorthing = maxNorthing + northingPadding;

            try
            {
               map.ZoomToEnvelope(minEasting, minNorthing, maxEasting, maxNorthing);
            }
            catch (PropertiesNotSetException pnse)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + pnse.Message);

                Logger.Write(operationalEvent);
            }
            catch (MapNotStartedException mnse)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + mnse.Message);

                Logger.Write(operationalEvent);
            }
            catch (ScaleZeroOrNegativeException szone)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + szone.Message);

                Logger.Write(operationalEvent);
            }
            catch (EnvelopeZeroOrNegativeException ezone)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + ezone.Message);

                Logger.Write(operationalEvent);
            }
            catch (ScaleOutOfRangeException soore)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + soore.Message);

                Logger.Write(operationalEvent);
            }
            catch (RouteInvalidException rie)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + rie.Message);

                Logger.Write(operationalEvent);
            }
            catch (NoPreviousExtentException npee)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + npee.Message);

                Logger.Write(operationalEvent);
            }
            catch (MapExceptionGeneral meg)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + meg.Message);

                Logger.Write(operationalEvent);
            }

        }



        #endregion
    }
}
