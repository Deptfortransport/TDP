// *********************************************** 
// NAME                 : StopInformation.aspx.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 10/09/2009 
// DESCRIPTION  		: Stop information page CCN 526
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/StopInformation.aspx.cs-arc  $
//
//   Rev 1.25   Mar 22 2013 10:49:24   DLane
//CCN0648c - Super Batch enhancements
//Resolution for 5907: CCN0648c - "Super Batch" enhancements
//
//   Rev 1.24   Jul 20 2010 15:05:50   MTurner
//Updated to correct fault with resolving 9000 codes
//Resolution for 5549: Two issues with stop names on station information page
//
//   Rev 1.23   Jun 08 2010 15:22:18   MTurner
//Changed case of string to be removed from some stop names.
//Resolution for 5549: Two issues with stop names on station information page
//
//   Rev 1.22   Apr 16 2010 16:24:50   MTurner
//Made searches on stop name identifiers case insensitive as there appears to be consistency across localities.
//Resolution for 5489: Bus stop names on Stop Information page not adequate
//
//   Rev 1.21   Apr 15 2010 10:33:52   mturner
//Further changes to calculation of bus stop names.
//Resolution for 5489: Bus stop names on Stop Information page not adequate
//
//   Rev 1.20   Mar 31 2010 15:34:28   mturner
//Updated the way stop names are calculated.
//Resolution for 5489: Bus stop names on Stop Information page not adequate
//
//   Rev 1.19   Mar 29 2010 15:38:00   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.18   Mar 26 2010 12:00:22   RHopkins
//Reduce number of calls made to CJP when showing Departure Boards and Stop Events
//Resolution for 5450: Stop Information pages make excessive calls to CJP
//
//   Rev 1.17   Dec 08 2009 11:28:56   mmodi
//Updated to prevent original Map location being lost when coming from FindMapResult page. Map Expand button flag is used to control overwriting the map location
//
//   Rev 1.16   Dec 04 2009 14:59:32   apatel
//Departure board changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.15   Nov 19 2009 09:47:04   pghumra
//Removed stop name fix as it was subsequently decided that it will not be included in the release of patch for 10.8.2.3
//Resolution for 5338: Stop Information Code Fixes
//
//   Rev 1.14   Nov 18 2009 16:00:30   pghumra
//Code fixes for TDP release patch 10.8.2.3 - Stop Information page
//Resolution for 5338: Stop Information Code Fixes
//
//   Rev 1.13   Oct 30 2009 11:50:50   apatel
//Stop information changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.12   Oct 28 2009 16:23:18   apatel
//put a space in the stop code text
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.11   Oct 27 2009 09:53:24   apatel
//Stop Information Departure board control changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.10   Oct 23 2009 12:15:34   apatel
//corrected method name for Initialise method
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.9   Oct 23 2009 09:05:06   apatel
//Stop info departure board control changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.8   Oct 15 2009 14:49:10   apatel
//Stop Information Departure Board Service code changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.7   Oct 12 2009 09:13:20   apatel
//WAI compliance changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.6   Oct 06 2009 14:41:42   apatel
//Stop Information code changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.5   Sep 30 2009 09:09:10   apatel
//Stop Information code changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.4   Sep 24 2009 10:18:24   apatel
//update for Map expand button
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.3   Sep 15 2009 15:47:52   apatel
//ShowHideControls logic moved to Page_Load event as user control's events doesn't fire due to dynamic loading of controls
//
//   Rev 1.2   Sep 15 2009 11:40:04   apatel
//ShowHide controls logic moved to the prerender method
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.1   Sep 14 2009 15:19:22   apatel
//Stop Information Pages - CCN 526
//Resolution for 5315: CCN526 Stop Information Page Landing

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.AdditionalDataModule;
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.CommonWeb.Helpers;
using System.Data;
using System.Collections;
using System.Text;

namespace TransportDirect.UserPortal.Web.JourneyPlanning
{
    /// <summary>
    /// Stop information page replaces old location information page
    /// </summary>
    public partial class StopInformation : TDPage
    {
        #region Private Fields
        private TDStopType stopType;
        private InputPageState inputPageState;
        private bool showDeparture = true;

        private string smsCode = string.Empty;
        private string crsCode = string.Empty;
        private string stopName = string.Empty;
        private string stopNaptan = string.Empty;

        Dictionary<string, string> stopsIdentifiers = new Dictionary<string, string>();

        //controls
        private StopInformationPlanJourneyControl planAJourneyControl;
        private StopInformationFacilityControl facilityControl;
        private StopInformationMapControl mapControl;
        private StopInformationDepartureBoardControl departureControl;
        private StopInformationTaxiControl taxiControl;
        private StopInformationRealTimeControl realTimeControl;
        private StopInformationOperatorControl operatorControl;
        private StopInformationLocalityControl localityControl;

        /// <summary>
        /// Helper class for Landing Page functionality.
        /// </summary>
        private LandingPageHelper landingPageHelper = new LandingPageHelper();

        #endregion

        #region Constructor
        public StopInformation()
        {
            pageId = TransportDirect.Common.PageId.StopInformation;
        }
        #endregion

       

        #region Page Events
        /// <summary>
        /// On init event of the page
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            LoadControls();
            ExtraEventWireUp();
            
            base.OnInit(e);

        }

        /// <summary>
        /// Page load event of the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.PageTitle = GetResource("StopInformation.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

            TransportDirect.UserPortal.SuggestionLinkService.Context context;

            if (TDSessionManager.Current.FindAMode == FindAMode.Station)
                context = TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuFindAPlace;
            else
                context = TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney;

            expandableMenuControl.AddContext(context);
            
            //added for white labelling Related link part of side menu
            //relatedLinksControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextLocationInformation);


            inputPageState = TDSessionManager.Current.InputPageState;

            showDeparture = inputPageState.DepartureBoardShowDeparture;
            
            SetupControlResorces();

            ResolveStopCode();

            if (!labelErrorMessage.Visible)
            {
                ShowHideStopControls();
            }

            departureControl.ArrivalDepartureToggleButton.Click += new EventHandler(ArrivalDepartureToggleButton_Click);
           
            if (TDSessionManager.Current.Session[SessionKey.LandingPageCheck])
            {
                buttonBack.Visible = false;
            }

            if (!Page.IsPostBack)
            {
                // If it's not a postback then this must be a fresh hit on this page,
                // so reset the flag for whether we have looked for bot types of results
                inputPageState.StopInformationDepartArriveChecked = false;
            }
        }

            
        /// <summary>
        /// Page pre render event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// Page Unload event handler
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_Unload(object sender, System.EventArgs e)
        {
            //reset landing page session parameters
            if (TDSessionManager.Current != null)
            {
                if (TDSessionManager.Current.Session[SessionKey.LandingPageCheck])
                {
                    landingPageHelper.ResetLandingPageSessionParameters();
                }
            }

        }
        #endregion

        #region Control Events
        /// <summary>
        /// BackButton click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButtonClick(object sender, EventArgs e)
        {
            // Reset the Map Expand button flag - calling page will set again if needed
            inputPageState.ShowStopInformationMapControlExpandButton = true;

            // Only need to induce a flag ensuring that this is on the way back from the location page.
            TDSessionManager.Current.SetOneUseKey(SessionKey.IndirectLocationPostBack, string.Empty);
            TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.StopInformationBack;
        }

        /// <summary>
        /// ArrivalDepartureToggle button click event. This is the Stop Infomation departure control's service button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ArrivalDepartureToggleButton_Click(object sender, EventArgs e)
        {
            
            this.showDeparture = !this.showDeparture;

            inputPageState.DepartureBoardShowDeparture = this.showDeparture;
            TDCodeType codetype = TDCodeType.CRS;
            if (stopType == TDStopType.Rail || stopType == TDStopType.Bus)
            {
                int range = int.Parse(Properties.Current["StopInformation.ServiceControl.Train.MaxServices"]);

                string code = crsCode;

                if (stopType == TDStopType.Bus)
                {
                    range = int.Parse(Properties.Current["StopInformation.ServiceControl.Bus.MaxServices"]);
                    code = smsCode;
                    codetype = TDCodeType.SMS;
                }
                if (string.IsNullOrEmpty(code))
                {
                    code = stopNaptan;
                    codetype = TDCodeType.NAPTAN;
                }

                departureControl.Initialise(stopType, code, codetype, stopName, showDeparture, range, TDDateTime.Now);

                departureControl.Refresh();

            }

            
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Extra event subscription
        /// </summary>
        private void ExtraEventWireUp()
        {
            //removed for white labelling
            //Ensure eventhandlers are wired up to the HyperlinkPostBackControls on the page
            buttonBack.Click += new EventHandler(this.BackButtonClick);
 
        }

        /// <summary>
        /// Loads user controls to the page
        /// </summary>
        private void LoadControls()
        {
            planAJourneyControl = LoadControl("../Controls/StopInformationPlanJourneyControl.ascx") as StopInformationPlanJourneyControl;
            facilityControl = LoadControl("../Controls/StopInformationFacilityControl.ascx") as StopInformationFacilityControl;

            mapControl = LoadControl("../Controls/StopInformationMapControl.ascx") as StopInformationMapControl;

            departureControl = LoadControl("../Controls/StopInformationDepartureBoardControl.ascx") as StopInformationDepartureBoardControl;

            taxiControl = LoadControl("../Controls/StopInformationTaxiControl.ascx") as StopInformationTaxiControl;

            realTimeControl = LoadControl("../Controls/StopInformationRealTimeControl.ascx") as StopInformationRealTimeControl;

            operatorControl = LoadControl("../Controls/StopInformationOperatorControl.ascx") as StopInformationOperatorControl;

            localityControl = LoadControl("../Controls/StopInformationLocalityControl.ascx") as StopInformationLocalityControl;

        }

        /// <summary>
        /// Sets up control resources
        /// </summary>
        private void SetupControlResorces()
        {
            buttonBack.Text = GetResource("LocationInformation.buttonBack.Text");

            // Setup gif resource for images (1 invisible image for all skip links)
            string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");

            summaryPlannersSkipLink.Src = skipLinkImageUrl;
            summaryPlannersSkipLink.Alt = GetResource("StopInformation.SkipLink.AlternateText");
        }

        /// <summary>
        /// Resolves stop code and initializes controls
        /// </summary>
        private void ResolveStopCode()
        {
            string naptan = string.Empty;
            string iataCode = string.Empty;
            

            TDCodeDetail[] codeDetails;
            codeDetails = FindCodeDetails(inputPageState.StopCode);

           
            //determing the type of code as crs, iata, sms with the napta of the stop
            if (codeDetails != null)
            {
                
                foreach (TDCodeDetail codeDetail in codeDetails)
                {
                    if (codeDetail.CodeType == inputPageState.StopCodeType)
                    {
                        naptan = codeDetail.NaptanId;
                        stopName = codeDetail.Description;

                        if (codeDetail.CodeType == TDCodeType.CRS)
                        {
                            crsCode = codeDetail.Code;
                        }
                        else if (codeDetail.CodeType == TDCodeType.IATA)
                        {
                            iataCode = codeDetail.Code;
                        }
                        else if (codeDetail.CodeType == TDCodeType.SMS)
                        {
                            smsCode = codeDetail.Code;
                        }
                    }
                }

            }

            if (!string.IsNullOrEmpty(naptan))
            {
                //determines the stop type
                stopType = GetStopType(naptan);
                this.stopNaptan = naptan;

                // uses Additional data to get stop name and crsCode and iata code if not already defined.

                IAdditionalData addData = (IAdditionalData)TDServiceDiscovery.Current[ServiceDiscoveryKey.AdditionalData];

                if (stopType == TDStopType.Air && naptan.Length == Airport.NaptanPrefix.Length + 3)
                {
                    // We need to provide a Naptan with a terminal number on the end. As all airports currently
                    // have a terminal 1 use that.
                    stopName = GetStopName(string.Format("{0}1", naptan));
                }
                else
                {
                    stopName = GetStopName(naptan);
                }

                labelStationName.Text = stopName;
                
                if (inputPageState.StopCodeType != TDCodeType.CRS && string.IsNullOrEmpty(crsCode))
                {
                    crsCode = addData.LookupCrsForNaptan(naptan);
                }

                if (inputPageState.StopCodeType != TDCodeType.IATA && string.IsNullOrEmpty(iataCode) && stopType == TDStopType.Air)
                {
                    iataCode = naptan.Substring(Airport.NaptanPrefix.Length, 3);
                }

                if (inputPageState.StopCodeType != TDCodeType.SMS && string.IsNullOrEmpty(smsCode) && (stopType == TDStopType.Bus || stopType == TDStopType.Coach))
                {
                    smsCode = GetSMSCodeForNaptan(naptan);
                }

                string stationCodeString = GetResource("StopInformation.labelStationCode.Text");

                string mobileServiceString = GetResource("StopInformation.labelStationCode.MobileService.Text");

                if (!string.IsNullOrEmpty(crsCode) && stopType == TDStopType.Rail)
                {
                    labelStationCode.Text = string.Format(stationCodeString, stopName, crsCode) + " " + mobileServiceString;
                }
                else if (!string.IsNullOrEmpty(iataCode) && stopType == TDStopType.Air)
                {
                    labelStationCode.Text = string.Format(stationCodeString, stopName, iataCode);
                }
                else if (!string.IsNullOrEmpty(smsCode) && (stopType == TDStopType.Bus || stopType == TDStopType.Coach))
                {
                    labelStationCode.Text = string.Format(stationCodeString, stopName, smsCode) + " " + mobileServiceString;
                }
                else
                {
                    labelStationCode.Visible = false;
                }

                InitialiseStopControls(stopType, naptan, crsCode, iataCode, smsCode, stopName);
            }
            else
            {
                labelErrorMessage.Text = GetResource("StopInformation.labelErroMessage.unknownStopCode.Text");
                labelErrorMessage.Visible = true;
            }

            

        }

        /// <summary>
        /// gets the detail of the code from code gazetter
        /// </summary>
        /// <param name="code">code to find a details for</param>
        /// <returns></returns>
        private TDCodeDetail[] FindCodeDetails(string code)
        {
            TDCodeDetail[] codedetail = null;
            try
            {
                // get the Code Gazetter reference
                ITDCodeGazetteer cg = (ITDCodeGazetteer)TDServiceDiscovery.Current[ServiceDiscoveryKey.CodeGazetteer];
                
                codedetail = cg.FindCode(code);
                try
                {
                    if (codedetail.Length == 0)
                    {
                        // No data has been found so try to find data from the NaPTAN cache
                        NaptanCacheEntry nce = NaptanLookup.Get(code, "");
                        if (nce.Found == true)
                        {
                            codedetail = new TDCodeDetail[1];
                            codedetail[0] = new TDCodeDetail();
                            codedetail[0].Code = nce.Naptan;
                            codedetail[0].CodeType = TDCodeType.NAPTAN;
                            codedetail[0].Description = nce.Description;
                            codedetail[0].Easting = nce.OSGR.Easting;
                            codedetail[0].Locality = nce.Locality;
                            codedetail[0].NaptanId = nce.Naptan;
                            codedetail[0].Northing = nce.OSGR.Northing;
                            switch (nce.Naptan.Substring(0, 4))
                            {
                                case ("9000"):
                                    {
                                        codedetail[0].ModeType = TDModeType.Coach;
                                        break;
                                    }
                                case ("9100"):
                                    {
                                        codedetail[0].ModeType = TDModeType.Rail;
                                        break;
                                    }
                                case ("9200"):
                                    {
                                        codedetail[0].ModeType = TDModeType.Air;
                                        break;
                                    }
                                case ("9300"):
                                    {
                                        codedetail[0].ModeType = TDModeType.Ferry;
                                        break;
                                    }
                                case ("9400"):
                                    {
                                        codedetail[0].ModeType = TDModeType.Metro;
                                        break;
                                    }
                                default:
                                    {
                                        codedetail[0].ModeType = TDModeType.Bus;
                                        break;
                                    }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    // Log the exception
                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.Business, TDTraceLevel.Error, "Stop Information Exception." + exception.Message);

                    Logger.Write(operationalEvent);
                }
               
               
               
            }
           
            catch (Exception exception)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.Business, TDTraceLevel.Error, "Stop Information Exception." + exception.Message);

                Logger.Write(operationalEvent);
            }

            return codedetail;
        }

       
        /// <summary>
        /// Initializes stop user controls using one or more of the parameter passed to the method
        /// </summary>
        /// <param name="stopType">Type of stop - TDStopType</param>
        /// <param name="naptan">Naptan of the stop</param>
        /// <param name="crsCode">CrsCode of the stop</param>
        /// <param name="iataCode">Iata code of the stop</param>
        /// <param name="smsCode">Sms code of the stop</param>
        /// <param name="stopName">Stop name</param>
        private void InitialiseStopControls(TDStopType stopType, string naptan, string crsCode, string iataCode, string smsCode, string stopName)
        {
            if (bool.Parse(Properties.Current["StopInformation.ShowJourney"]))
            {
                planAJourneyControl.initialize(true, stopType, naptan, stopName);
            }

            if (bool.Parse(Properties.Current["StopInformation.ShowFacilities"]))
            {
                facilityControl.Initialise(stopType, naptan, crsCode);
            }

            if (bool.Parse(Properties.Current["StopInformation.ShowMap"]))
            {
                NaptanCacheEntry x = NaptanLookup.Get(naptan, "Naptan");
                MapSearch(x);

                if (inputPageState != null)
                {
                    mapControl.Initialise(stopType, x.OSGR, stopName, inputPageState.ShowStopInformationMapControlExpandButton);
                }
                else
                {
                    mapControl.Initialise(stopType, x.OSGR, stopName, true);
                }
            }


            if ((stopType == TDStopType.Bus && bool.Parse(Properties.Current["StopInformation.ShowServices.Bus"]))
                        || (stopType == TDStopType.Rail && bool.Parse(Properties.Current["StopInformation.ShowServices.Rail"])))
            {
                //initialize departure board control only if stop type is bus or rail
                if (stopType == TDStopType.Rail || stopType == TDStopType.Bus)
                {
                    int range = int.Parse(Properties.Current["StopInformation.ServiceControl.Train.MaxServices"]);

                    string code = crsCode;
                    TDCodeType codeType = TDCodeType.CRS;
                    
                    if (stopType == TDStopType.Bus)
                    {
                        range = int.Parse(Properties.Current["StopInformation.ServiceControl.Bus.MaxServices"]);
                        code = smsCode;
                        codeType = TDCodeType.SMS;
                    }
                    if (string.IsNullOrEmpty(code))
                    {
                        code = naptan;
                        codeType = TDCodeType.NAPTAN;
                    }


                    departureControl.Initialise(stopType, code, codeType, stopName, showDeparture, range, TDDateTime.Now);
                    
                }
            }

            if (bool.Parse(Properties.Current["StopInformation.ShowTaxi"]))
            {
                taxiControl.Initialise(naptan);
            }

            if (bool.Parse(Properties.Current["StopInformation.ShowOperator"]))
            {
                operatorControl.Initialise(stopType, naptan, iataCode);
            }

            if(bool.Parse(Properties.Current["StopInformation.ShowLocality"]))
            {
            localityControl.initialise(naptan);
            }

            if (bool.Parse(Properties.Current["StopInformation.ShowRealTimeLinks"]))
            {
                realTimeControl.Initialise(stopType, naptan, crsCode);
            }
           
        }

        /// <summary>
        /// Sets up parameter for journey location map when map control's expand button clicked
        /// </summary>
        /// <param name="x">Naptan Cache Entry object</param>
        public virtual void MapSearch(NaptanCacheEntry x)
        {
            // Only do this if the flag to show Expand button is set.
            // This is to prevent the existing Map location from being lost if we come from the Map page
            // using the Show stop information link. 
            if (inputPageState.ShowStopInformationMapControlExpandButton)
            {

                inputPageState.MapLocationSearch.ClearAll();
                inputPageState.MapLocation.Status = TDLocationStatus.Valid;
                inputPageState.MapLocationSearch.SearchType = SearchType.AllStationStops;
                inputPageState.MapMode = CurrentMapMode.FromFindAInput;

                LocationSearch thisSearch = inputPageState.MapLocationSearch;

                TDLocation location = new TDLocation();

                location.NaPTANs = new TDNaptan[] { new TDNaptan(x.Naptan, x.OSGR, x.Description) };

                location.GridReference = x.OSGR;

                location.Locality = x.Locality;

                location.Description = x.Description;

                TDLocation thisLocation = location;

                LocationSearchHelper.SetupLocationParameters(
                    x.Description,
                    SearchType.AllStationStops,
                    false,
                    0,
                    0,
                    0,
                    ref thisSearch,
                    ref thisLocation,
                    false,
                    false,
                    StationType.Undetermined
                    );

                inputPageState.MapLocationSearch = thisSearch;
                inputPageState.MapLocationSearch.LocationFixed = true;
                inputPageState.MapLocation = location;
                inputPageState.MapLocation.Status = TDLocationStatus.Valid;
            }
        }

        /// <summary>
        /// Returns SMS code for naptan for bus stations
        /// </summary>
        /// <param name="naptan">naptan</param>
        /// <returns>SMS code as string</returns>
        private string GetSMSCodeForNaptan(string naptan)
        {
            string smsCode = string.Empty;

            try
            {
                IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
                QuerySchema gisResult = gisQuery.FindStopsInfoForStops(new string[] { naptan });


                for (int i = 0; i < gisResult.Stops.Rows.Count; i++)
                {
                    QuerySchema.StopsRow row = (QuerySchema.StopsRow)gisResult.Stops.Rows[i];

                    if (!string.IsNullOrEmpty(row.smsnumber))
                    {
                        smsCode = row.smsnumber;
                    }
                }

            }
            catch (Exception exception)
            {
                
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.Business, TDTraceLevel.Error, "Stop Information Exception." + exception.Message);

                Logger.Write(operationalEvent);

            }

            return smsCode;


        }


        /// <summary>
        /// Gets the stop type using naptan of the stop
        /// </summary>
        /// <param name="naptan">Naptan of the stop</param>
        /// <returns></returns>
        private TDStopType GetStopType(string naptan)
        {
            string stopType = string.Empty;
            TDStopType tdStopType = TDStopType.Unknown;

            labelLocationInformationTitle.Text = GetResource("StopInformation.labelLocationInformationTitle");

            try
            {
                IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
                QuerySchema gisResult = gisQuery.FindStopsInfoForStops(new string[] { naptan });

                
                for (int i = 0; i < gisResult.Stops.Rows.Count; i++)
                {
                    QuerySchema.StopsRow row = (QuerySchema.StopsRow)gisResult.Stops.Rows[i];

                    stopType = row.stoptype;
                }

                #region Determining stop type
                switch (stopType)
                {
                    case "AIR":	// Air
                    case "GAT":
                        tdStopType = TDStopType.Air;
                        labelLocationInformationTitle.Text = GetResource("StopInformation.labelLocationInformationTitle.air");
                        break;
                    case "BCE":	// coach
                    case "BST":
                        tdStopType = TDStopType.Coach;
                        labelLocationInformationTitle.Text = GetResource("StopInformation.labelLocationInformationTitle.coach");
                        break;
                    case "BCQ":
                    case "BCT":	// Bus
                    case "BCS":
                        tdStopType = TDStopType.Bus;
                        break;
                    case "RLY":	// Rail
                    case "RPL":
                    case "RSE":
                        tdStopType = TDStopType.Rail;
                        labelLocationInformationTitle.Text = GetResource("StopInformation.labelLocationInformationTitle.rail");
                        break;
                    case "MET":	// Light/rail
                    case "PLT":
                    case "TMU":
                        tdStopType = TDStopType.LightRail;
                        break;
                    case "TXR":	// Taxi
                    case "STR":
                        tdStopType = TDStopType.Taxi;
                        break;
                    case "FER":	// Ferry
                    case "FTD":
                        tdStopType = TDStopType.Ferry;
                        labelLocationInformationTitle.Text = GetResource("StopInformation.labelLocationInformationTitle.ferry");
                        break;
                    default:
                        tdStopType = TDStopType.Unknown;
                        break;
                }
                #endregion

                
            }
            catch (Exception exception)
            {
                tdStopType = TDStopType.Unknown;
               
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.Business, TDTraceLevel.Error, "Stop Information Exception." + exception.Message);

                Logger.Write(operationalEvent);

            }

            if (tdStopType == TDStopType.Unknown)
            {
                tdStopType = FindStopTypeFromNaptanPrefix(naptan);
            }

            return tdStopType;
        }

        /// <summary>
        /// Finds the stop type using prefix of the naptan
        /// </summary>
        /// <param name="naptan">Naptan for the stop</param>
        /// <returns></returns>
        private TDStopType FindStopTypeFromNaptanPrefix(string naptan)
        {
            TDStopType stopType = TDStopType.Unknown;

            if (naptan.StartsWith(Properties.Current["FindA.NaptanPrefix.Airport"]))
            {
                stopType = TDStopType.Air;
            }
            else if (naptan.StartsWith(Properties.Current["FindA.NaptanPrefix.Rail"]))
            {
                stopType = TDStopType.Rail;
            }
            else if (naptan.StartsWith(Properties.Current["FindA.NaptanPrefix.Coach"]))
            {
                stopType = TDStopType.Coach;
            }

            return stopType;
        }

        /// <summary>
        /// Add stop controls to relevant container if they are appearing on the screen
        /// </summary>
        private void ShowHideStopControls()
        {
            List<Control> controlToDisplay = new List<Control>();

            if (PlanAJourneyControlVisible())
            {
                controlToDisplay.Add(planAJourneyControl);
            }

            if (RealTimeControlVisible())
            {
                controlToDisplay.Add(realTimeControl);
            }

            if (TaxiControlVisible())
            {
                controlToDisplay.Add(taxiControl);
            }

            if (FacilityControlVisible())
            {
                controlToDisplay.Add(facilityControl);
            }

            if (OperatorControlVisible())
            {
                controlToDisplay.Add(operatorControl);
            }

            if (LocalityControlVisible())
            {
                controlToDisplay.Add(localityControl);
            }

            if (MapControlVisible())
            {
                controlToDisplay.Add(mapControl);
            }

            if (DepartureControlVisible())
            {
                if (controlToDisplay.Count == 0)
                {
                    controlToDisplay.Add(departureControl);
                }
                else
                {
                    controlToDisplay.Insert(1, departureControl);
                }
            }

            int container = 0;
            foreach (Control ctrl in controlToDisplay)
            {
                if (container == 0)
                {
                    StopInformationControlContainer1.Controls.Add(ctrl);
                    container++;
                }
                else
                {
                    StopInformationControlContainer2.Controls.Add(ctrl);
                    container--;
                }
            }
        }

        /// <summary>
        /// Decides wether Departure Control should be displayed or not on stop infromation page
        /// </summary>
        /// <returns>true if Daparture Control can be displayed</returns>
        private bool DepartureControlVisible()
        {
            bool visible = true;

            if (stopType != TDStopType.Bus && stopType != TDStopType.Rail)
            {
                visible = false;
            }
            else
            {
                visible = inputPageState.ShowStopInformationDepartureBoardControl;
            }

            if (visible)
            {
                 visible = (stopType == TDStopType.Bus && bool.Parse(Properties.Current["StopInformation.ShowServices.Bus"]))
                        || (stopType == TDStopType.Rail && bool.Parse(Properties.Current["StopInformation.ShowServices.Rail"]));
 
            }

            return visible; 
        }

        /// <summary>
        /// Decides wether Map Control should be displayed or not on stop infromation page
        /// </summary>
        /// <returns>true if Map Control can be displayed on stop infromation page</returns>
        private bool MapControlVisible()
        {
            bool visible = true;

            visible = inputPageState.ShowStopInformationMapControl;
            

            if (visible)
            {
                visible = bool.Parse(Properties.Current["StopInformation.ShowMap"]);
            }

            return visible;
        }

        /// <summary>
        /// Decides wether Locality Control should be displayed or not on stop infromation page
        /// </summary>
        /// <returns>true if Locality Control can be displayed on stop infromation page</returns>
        private bool LocalityControlVisible()
        {
            bool visible = true;

            if (localityControl.IsEmpty || stopType == TDStopType.Coach)
            {
                visible = false;
            }
            else
            {
                visible = inputPageState.ShowStopInformationLocalityControl;
            }

            if (visible)
            {
                visible = bool.Parse(Properties.Current["StopInformation.ShowLocality"]);
            }

            return visible;

        }

        /// <summary>
        /// Decides wether Operator Control should be displayed or not on stop infromation page
        /// </summary>
        /// <returns>true if Operator Control can be displayed on stop infromation page</returns>
        private bool OperatorControlVisible()
        {
            bool visible = true;

            if (operatorControl.IsEmpty || stopType != TDStopType.Air)
            {
                visible = false;
            }
            else
            {
                visible = inputPageState.ShowStopInformationOperatorControl;
            }

            if (visible)
            {
                visible = bool.Parse(Properties.Current["StopInformation.ShowOperator"]);
            }

            return visible;

        }

        /// <summary>
        /// Decides wether Facility Control should be displayed or not on stop infromation page
        /// </summary>
        /// <returns>true if Facility Control can be displayed on stop infromation page</returns>
        private bool FacilityControlVisible()
        {
            bool visible = true;

            if (facilityControl.IsEmpty)
            {
                visible = false;
            }
                // stop types Coach and Light rail added as per CCN to improve accessibility information
            else if (stopType != TDStopType.Rail && stopType != TDStopType.Air
                        && stopType != TDStopType.Ferry && stopType != TDStopType.Coach
                        && stopType != TDStopType.LightRail)
            {
                visible = false;
            }
            else
            {
                visible = inputPageState.ShowStopInformationFacilityControl;
            }

            if (visible)
            {
                visible = bool.Parse(Properties.Current["StopInformation.ShowFacilities"]);
            }

            return visible;

        }

        /// <summary>
        /// Decides wether Taxi Control should be displayed or not on stop infromation page
        /// </summary>
        /// <returns>true if Taxi Control can be displayed on stop infromation page</returns>
        private bool TaxiControlVisible()
        {
            bool visible = true;

            if (taxiControl.IsEmpty)
            {
                visible = false;
            }
            else
            {
                visible = inputPageState.ShowStopInformationTaxiControl;
            }

            if (visible)
            {
                visible = bool.Parse(Properties.Current["StopInformation.ShowTaxi"]);
            }

            return visible;

        }

        /// <summary>
        /// Decides wether Real Time Control should be displayed or not on stop infromation page
        /// </summary>
        /// <returns>true if Real Time Control can be displayed on stop infromation page</returns>
        private bool RealTimeControlVisible()
        {
            bool visible = true;

            if (realTimeControl.IsEmpty || stopType != TDStopType.Air || DepartureControlVisible())
            {
                visible = false;
            }
            else
            {
                visible = inputPageState.ShowStopInformationRealTimeControl;
            }

            if (visible)
            {
                visible = bool.Parse(Properties.Current["StopInformation.ShowRealTimeLinks"]);
            }

            return visible;

        }

        /// <summary>
        /// Decides wether Plan A Journey Control should be displayed or not on stop infromation page
        /// </summary>
        /// <returns>true if Plan A Journey Control can be displayed on stop infromation page</returns>
        private bool PlanAJourneyControlVisible()
        {
            bool visible = true;

            visible = inputPageState.ShowStopInformationPlanJourneyControl;
            
            if (visible)
            {
                visible = bool.Parse(Properties.Current["StopInformation.ShowJourney"]);
            }

            return visible;

        }

        private string GetStopName(string stopNaptan)
        {
            try
            {
                string stopName = string.Empty;

                if (stopNaptan.StartsWith("9000") || stopNaptan.StartsWith("9100") || stopNaptan.StartsWith("9200") || stopNaptan.StartsWith("9300") || stopNaptan.StartsWith("9400"))
                {
                    IAdditionalData addData = (IAdditionalData)TDServiceDiscovery.Current[ServiceDiscoveryKey.AdditionalData];
                    stopName = addData.LookupStationNameForNaptan(stopNaptan);
                    if (string.IsNullOrEmpty(stopName))
                    {
                        // lookup in gaz
                        NaptanCacheEntry nce = NaptanLookup.Get(stopNaptan, "");
                        if (nce.Found)
                        {
                            stopName = nce.Description;
                        }
                    }
                    
                }
                else
                {
                    // This is a bus stop and bespoke name rules apply so call the bus stop formatter
                    BusStopNameFormatter nameFormatter = new BusStopNameFormatter(stopNaptan);
                    stopName = nameFormatter.Format();
                }
                return stopName;
            }
            catch (Exception e)
            {
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.Business, TDTraceLevel.Error, "Stop Information Exception." + e.Message);

                Logger.Write(operationalEvent);

                return string.Empty;
            }
        }
        #endregion
    }
}
