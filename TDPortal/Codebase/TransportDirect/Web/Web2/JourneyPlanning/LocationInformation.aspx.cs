// ********************************************************************* 
// NAME                 : LocationInformation.cs 
// AUTHOR               : Andrew Toner/Richard Philpott
// DATE CREATED         : 05/11/2003
// DESCRIPTION			: Station Information Page
// ********************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/LocationInformation.aspx.cs-arc  $ 
//
//   Rev 1.12   Dec 15 2008 11:36:38   apatel
//XHTML Compliance changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.11   Dec 15 2008 09:57:36   apatel
//XHTML Compliance changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.10   Oct 17 2008 11:55:28   build
//Automatically merged from branch for stream0093
//
//   Rev 1.9.1.0   Aug 05 2008 11:39:28   apatel
//added text "(opens new window)" to arrivalboard and departureboard text
//Resolution for 5096: ArrivalBoard and DepartureBoard labels missing "Opens new window" text
//
//   Rev 1.9   Jul 24 2008 13:45:50   apatel
//External links added text "(opens new window)"
//Resolution for 5087: Accessibility - external links text changes
//
//   Rev 1.8   Jul 24 2008 10:44:32   apatel
//Removed External Links tooltip and added (opens new window) text to the links
//Resolution for 5087: Accessibility - external links text changes
//
//   Rev 1.7   Jul 17 2008 13:05:08   apatel
//Updated to show links based on naptan or admin-district area but not base on both.
//Resolution for 5071: Zonal accessibilty links do not display in the correct section of the screen
//
//   Rev 1.6   Jul 08 2008 09:25:50   apatel
//Accessibility link CCN 458 updates
//Resolution for 5033: CCN0458 - Accessability Updates improve linking
//
//   Rev 1.5   Jun 27 2008 09:41:20   apatel
//CCN - 458 Accessibility Updates - Improved linking
//Resolution for 5033: CCN0458 - Accessability Updates improve linking
//
//   Rev 1.4   May 29 2008 15:41:18   mturner
//Fix for IR5012 - Changed to show Journey Planning left hand links when you are in the process of planning a journey.
//Resolution for 5012: Incorrect left hand links on JourneyEmissionsCompare and LocationInformation Pages
//
//   Rev 1.3   Apr 03 2008 15:29:56   apatel
//set labelarrivalnavigation text
//
//   Rev 1.2   Mar 31 2008 13:25:02   mturner
//Drop3 from Dev Factory
//
// Rev DevFactory Feb 07 2008 10:03:55 aahmed
// removed existing side menu as the new standard side menu should now hold all this information
// only commented out code and not removed as may help when adding side menu contents
//
//   Rev 1.2   Nov 29 2007 15:01:58   mturner
//Removed compiler warnings caused by the merge of Del 9.8 into the .Net2 code base
//
//   Rev 1.2   Nov 29 2007 13:07:54   mturner
//Declared as partial class to make Del 9.8 cde .Net2 compliant
//
//   Rev 1.1   Nov 29 2007 11:35:48   mturner
//Updated for Del 9.8
//
//   Rev 1.36   Oct 25 2007 16:02:12   mmodi
//Changes to display Air departure boards
//Resolution for 4518: Del 9.8 - Air Departure Boards
//
//   Rev 1.35   Sep 20 2007 15:28:54   rbroddle
//Corrected fault with non - naptan locations  when attempting naptan cache lookup.
//Resolution for 4504: Location information page error on non - naptan locations
//
//   Rev 1.34   Sep 13 2007 18:00:18   rbroddle
//Added NaPTAN Cache Lookup
//Resolution for 4468: Coach Stop Taxi Enhancements
//
//   Rev 1.33   Sep 13 2007 16:55:04   rbroddle
//Amended for Coach Stop Taxi Enhancements CCN393
//Resolution for 4468: Coach Stop Taxi Enhancements
//
//   Rev 1.32   Jun 14 2007 13:58:10   jfrank
//Fix for USD UK:1054298 - Local Fare Information on the Station Information Page
//Resolution for 4451: Local Fare Information on the Station Information Page
//
//   Rev 1.31   Apr 16 2007 13:15:32   dsawe
//local zonal services bug fixes
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.30   Apr 02 2007 16:28:54   dsawe
//added for local zonal
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.29   Mar 21 2007 16:14:10   dsawe
//added for displaying operator & taxi controls at same time
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.28   Mar 19 2007 15:56:32   dsawe
//updated for local zonal services
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.27   Mar 16 2007 10:00:50   build
//Automatically merged from branch for stream4362
//
//   Rev 1.26.1.0   Mar 15 2007 18:27:32   dsawe
//added methods for local zonal services
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.26   Feb 23 2006 17:59:36   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.25   Feb 17 2006 11:57:22   halkatib
//Added fixes for IR3573
//
//   Rev 1.24   Feb 16 2006 16:46:22   kjosling
//Fixed merge. Removed references to old header controls
//
//   Rev 1.23   Feb 16 2006 15:37:58   halkatib
//Merged stream 0002 into the trunk.
//
//   Rev 1.22   Feb 10 2006 15:09:16   build
//Automatically merged from branch for stream3180
//
//   Rev 1.21.1.0   Dec 06 2005 14:51:28   AViitanen
//Changed to use HeaderControl and HeadElementControl as part of Homepage phase2. 
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.21   Nov 11 2005 10:41:52   rgreenwood
//IR2981 Removed top back button and changed button naming convention
//Resolution for 2981: UEE: Door to Door - station information
//
//   Rev 1.20   Nov 03 2005 16:55:30   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.19.1.0   Oct 12 2005 11:35:32   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.20   Oct 11 2005 14:55:00   RGriffith
//Replacing the image button with HTML button
//
//   Rev 1.19   Sep 27 2005 17:54:54   kjosling
//Merged stream 2625 with trunk. 
//Resolution for 2625: DEL 8 Stream: TrainTaxi Amendments
//
//   Rev 1.18.1.3   Sep 23 2005 12:32:18   MTillett
//Update code so that the Taxi information is no dependent on the CRS object (i.e. so that it is displayed for both train and air where data is available).
//Resolution for 2625: DEL 8 Stream: TrainTaxi Amendments
//
//   Rev 1.18.1.2   Sep 21 2005 19:10:50   rgeraghty
//Bug Fix for Accessibility Icon and text for other modes like Airport.  
//Resolution for 2625: DEL 8 Stream: TrainTaxi Amendments
//
//   Rev 1.18.1.1   Sep 01 2005 11:42:24   kjosling
//Updated following code review
//
//   Rev 1.18.1.0   Aug 15 2005 09:55:22   kjosling
//Added a TaxiInformationControl user control to the page, which replaces the TrainTaxiInfo object formally used. Resolution for 2625: DEL 8 Stream: TrainTaxi Amendments
//Resolution for 2625: DEL 8 Stream: TrainTaxi Amendments
//
//   Rev 1.18   Sep 20 2004 14:02:38   passuied
//changed condition to show tabs by looking at isFindAMode instead of TabSection.
//Also changed name of HeaderControl1 to HeaderControlJourneyPlanner1 instead since the name matters for the resource population
//Resolution for 1604: Selecting the information button in door to door changes header to quick planner
//
//   Rev 1.17   Aug 24 2004 14:35:36   COwczarek
//Add Find A header. Set visibility of headers conditionally.
//Resolution for 1320: Find a train station information page header
//
//   Rev 1.16   Jul 15 2004 13:41:24   jbroome
//IR1217. Page header only displays Station Information if location is a station.
//
//   Rev 1.15   Mar 10 2004 15:53:38   PNorell
//Updated for IR498 to be included in 5.2
//
//   Rev 1.14   Mar 08 2004 17:13:46   COwczarek
//Remove handling of redundant "header click" event. Now performed in header controls.
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.13   Nov 27 2003 09:25:18   passuied
//Cleared JourneyInputReturnStack when click on header (break of screenflow)
//
//
//   Rev 1.12   Nov 17 2003 16:41:58   passuied
//tidied up after Html validation
//
//   Rev 1.11   Nov 17 2003 16:20:14   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.10   Nov 07 2003 15:16:24   passuied
//Additions of taxi Information Display
//
//   Rev 1.9   Nov 06 2003 18:39:12   passuied
//created structure for LocationInformation page
//
//   Rev 1.8   Nov 05 2003 19:17:34   RPhilpott
//Use AdditionalData wrapper 

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.UserSupport;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.AdditionalDataModule;
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.LocationInformationService;
using TransportDirect.Presentation.InteractiveMapping;
using Logger = System.Diagnostics.Trace;


namespace TransportDirect.UserPortal.Web.Templates
{
    /// <summary>
    /// Summary description for LocationInformation.
    /// </summary>
    /// 
    public partial class LocationInformation : TDPage
    {
        #region Controls
        protected System.Web.UI.WebControls.HyperLink ArrivalsBoardHyperlink;
        //protected System.Web.UI.WebControls.Panel panelGoTo;
        //protected System.Web.UI.WebControls.Label labelGoTo;
        protected System.Web.UI.WebControls.Literal literalPageTitle;
        #endregion

        #region Web user controls

        //protected TransportDirect.UserPortal.Web.Controls.HyperlinkPostbackControl taxiHyperlinkPostbackControl;
        //protected TransportDirect.UserPortal.Web.Controls.HyperlinkPostbackControl faresHyperlinkPostbackControl;
        protected ZonalServiceLinksControl ZonalServiceLinksControl1;
       
        //protected TransportDirect.UserPortal.Web.Controls.HyperlinkPostbackControl airoperatorsHyperlinkPostbackControl;
        protected TransportDirect.UserPortal.Web.Controls.ZonalAirportOperatorControl ZonalAirportOperatorControl1;

        protected HeaderControl headerControl;
        protected TaxiInformationControl TaxiInformationControl1;

        #endregion

        public LocationInformation()
        {
            pageId = PageId.LocationInformation;
        }

        /// <summary>
        /// The page title property
        /// </summary>
        new public string PageTitle
        {
            get
            {
                return Global.tdResourceManager.GetString(
                    "JourneyPlanner.DefaultPageTitle",
                    TDCultureInfo.CurrentUICulture);
            }
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            // Only need to induce a flag ensuring that this is on the way back from the location page.
            TDSessionManager.Current.SetOneUseKey(SessionKey.IndirectLocationPostBack, string.Empty);
            TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.LocationInformationBack;
            summaryPlannersSkipLink.Alt = GetResource("LocationInformation.SkipLink_Fares.AlternateText");
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            ExtraEventWireUp();
            InitializeComponent();
            base.OnInit(e);

        }

        /// <summary>
        /// Extra event subscription
        /// </summary>
        private void ExtraEventWireUp()
        {
            //removed for white labelling
            //Ensure eventhandlers are wired up to the HyperlinkPostBackControls on the page
            buttonBack.Click += new EventHandler(this.BackButtonClick);
            //taxiHyperlinkPostbackControl.link_Clicked += new EventHandler(taxiHyperlinkPostbackControl_link_Clicked);
            //faresHyperlinkPostbackControl.link_Clicked += new EventHandler(faresHyperlinkPostbackControl_link_Clicked);
            //airoperatorsHyperlinkPostbackControl.link_Clicked += new EventHandler(airoperatorsHyperlinkPostbackControl_link_Clicked);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        #region Event Handlers

        /// <summary>
        /// Runs when the air operators Hyperlink is clicked
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        //private void airoperatorsHyperlinkPostbackControl_link_Clicked(object sender, EventArgs e)
        //{
        //    TaxiInformationControl1.Visible = true;
        //    ZonalServiceLinksControl1.Visible = false;
        //    ZonalAirportOperatorControl1.Visible = true;
        //    panelOperatorDetails.Visible = true;
        //    labelSummaryTitle.Text = GetResource("LocationInformation.labelSummaryTitle_Taxis.Text");
        //    summaryPlannersSkipLink.Alt = GetResource("LocationInformation.SkipLink_Operators.AlternateText");
        //}

        /// <summary>
        /// Runs when the Taxi information hyperlink is clicked
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        //private void taxiHyperlinkPostbackControl_link_Clicked(object sender, EventArgs e)
        //{
        //    TaxiInformationControl1.Visible = true;
        //    ZonalServiceLinksControl1.Visible = false;
        //    if(!ZonalAirportOperatorControl1.IsEmpty)
        //    {
        //        panelOperatorDetails.Visible = true;
        //        ZonalAirportOperatorControl1.Visible = true;
        //    }
        //    else
        //    {
        //        panelOperatorDetails.Visible = false;
        //        ZonalAirportOperatorControl1.Visible = false;
        //    }

        //    labelSummaryTitle.Text = GetResource("LocationInformation.labelSummaryTitle_Taxis.Text");
        //    summaryPlannersSkipLink.Alt = GetResource("LocationInformation.SkipLink_Taxi.AlternateText");
        //}

        /// <summary>
        /// Runs when the Fares information hyperlink is clicked
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        //private void faresHyperlinkPostbackControl_link_Clicked(object sender, EventArgs e)
        //{
        //    MakeFaresInfoVisible();
        //    ZonalAirportOperatorControl1.Visible = false;
        //    panelOperatorDetails.Visible = false;
        //}

        /// <summary>
        /// Runs when the page loads
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void Page_Load(object sender, System.EventArgs e)
        {
            InputPageState inputPageState = TDSessionManager.Current.InputPageState;
            IAdditionalData addData = (IAdditionalData)TDServiceDiscovery.Current[ServiceDiscoveryKey.AdditionalData];
            string crs = addData.LookupCrsForNaptan(inputPageState.AdditionalDataLocation);

            // Set the location/station name label
            if ((addData.LookupStationNameForNaptan(inputPageState.AdditionalDataLocation) != null) && (addData.LookupStationNameForNaptan(inputPageState.AdditionalDataLocation).Length != 0))
            {
                labelStationName.Text = addData.LookupStationNameForNaptan(inputPageState.AdditionalDataLocation);
            }

            buttonBack.Text = GetResource("LocationInformation.buttonBack.Text");

            TransportDirect.UserPortal.SuggestionLinkService.Context context;

            if (TDSessionManager.Current.FindAMode == FindAMode.Station)
                context = TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuFindAPlace;
            else
                context = TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney;

            expandableMenuControl.AddContext(context);
            //expandableMenuControl.AddContext(SuggestionLinkService.Context.HomePageMenuFindAPlace);

            //added for white labelling Related link part of side menu
            relatedLinksControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextLocationInformation);

            //removed for white labelling
            //taxiHyperlinkPostbackControl.Text = GetResource("LocationInformation.taxiHyperlinkPostbackControl.Text");
            //faresHyperlinkPostbackControl.Text = GetResource("LocationInformation.faresHyperlinkPostbackControl.Text");
            //airoperatorsHyperlinkPostbackControl.Text = GetResource("LocationInformation.labelSummaryTitle_Operators.Text");
            labelSummaryTitle.Text = GetResource("LocationInformation.labelSummaryTitle_Taxis.Text");
            localInformation.Text = GetResource("LocationInformation.localInformation.Text");
            //stationFacilities.Text = GetResource("LocationInformation.stationFacilities.Text");
            realTimeInfo.Text = GetResource("LocationInformation.realTimeInfo.Text");

            string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
            summaryPlannersSkipLink.Src = skipLinkImageUrl;
            summaryPlannersSkipLink.Alt = GetResource("LocationInformation.SkipLink_Taxi.AlternateText");

            labelOperator.Text = GetResource("LocationInformation.labelSummaryTitle_Operators.Text");

            #region Set up Go To Section

            labelDepartureBoardNavigation.Text = string.Format("{0} {1}", GetResource("DepartureBoardHyperLink.labelDepartureBoardNavigation"), GetResource("ExternalLinks.OpensNewWindowText")); 

            labelArrivalsBoardNavigation.Text = string.Format("{0} {1}", GetResource("ArrivalsBoardHyperlink.labelArrivalsBoardNavigation"), GetResource("ExternalLinks.OpensNewWindowText")); 

            StationAccessibilityLink.Text = string.Format("{0} {1}", GetResource("JourneyDetails.accessibilityLink.Text"), GetResource("ExternalLinks.OpensNewWindowText"));
            //removed for white labellinh
            //labelGoTo.Text = GetResource("LocationInformation.labelGoTo.Text");

            // Initially disable links in Go To section
            //panelGoTo.Visible = false;
            DepartureBoardHyperLink.Visible = false;
            ArrivalsBoardHyperlink.Visible = false;
            FurtherDetailsHyperLink.Visible = false;
            labelStationInformationTitle.Visible = false;
            labelLocationInformationTitle.Visible = true;
            realTime.Visible = false;
            stationInfo.Visible = false;
            StationAccessibilityLink.Visible = false;



            operatorInfo.Visible = false;
            //labelAirportInformationTitle.Visible = false;

            //remioved for white labelling
            #region Set up Go To url's for train station
            if (crs.Length != 0)
            {

                try
                {
                    DepartureBoardHyperLink.NavigateUrl = string.Format(Properties.Current["locationinformation.departureboardurl"], crs);
                    DepartureBoardHyperLink.Target = "_blank";
                    DepartureBoardHyperLink.Visible = true;
                    DepartBoardDiv.Visible = true;
                    realTime.Visible = true;
                    

                }
                catch
                {
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "missing property in PropertyService : locationinformation.departureboardurl");
                    Logger.Write(oe);
                    throw new TDException("missing property in PropertyService : locationinformation.departureboardurl", true, TDExceptionIdentifier.PSMissingProperty);
                }

                try
                {
                    FurtherDetailsHyperLink.NavigateUrl = string.Format(Properties.Current["locationinformation.furtherdetailsurl"], crs);
                    FurtherDetailsHyperLink.Target = "_blank";
                    FurtherDetailsHyperLink.Visible = true;
                    FurtherDetailsDiv.Visible = true;
                    
                    stationInfo.Visible = true;
                    stationFacilities.Text = GetResource("LocationInformation.stationFacilities.Text");
                    SetFurtherDetailsLinkText(inputPageState.AdditionalDataLocation);
                    StationAccessibilityLink.NavigateUrl = string.Format(Properties.Current["locationinformation.accessibilityurl"], crs);
                    StationAccessibilityLink.Target = "_blank";
                    StationAccessibilityLink.Visible = true;
                    StationAccessDiv.Visible = true;
                    
                }
                catch
                {
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "missing property in PropertyService : locationinformation.furtherdetailsurl");
                    Logger.Write(oe);
                    throw new TDException("missing property in PropertyService : locationinformation.furtherdetailsurl", true, TDExceptionIdentifier.PSMissingProperty);
                }

                labelStationInformationTitle.Visible = true;
                labelLocationInformationTitle.Visible = false;
                //labelAirportInformationTitle.Visible = false;
            }
            #endregion

            #region Set up Go To url's for airport
            // Is it an Airport location
            if (inputPageState.AdditionalDataLocation != "")
            {
                if (inputPageState.AdditionalDataLocation.Substring(0, Airport.NaptanPrefix.Length) == Airport.NaptanPrefix)
                {
                    // Obtain Go To urls for airport
                    LocationInformationCatalogue refData = (LocationInformationCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationInformation];
                    LocationInformationService.LocationInformation locInfo = refData.GetLocationInformation(inputPageState.AdditionalDataLocation);

                    if (locInfo != null)
                    {
                        // Set up the hyperlinks
                        if (locInfo.DepartureLink != null)
                        {
                            DepartureBoardHyperLink.Target = "_blank";
                            DepartureBoardHyperLink.Visible = true;
                            DepartBoardDiv.Visible = true;
                            realTime.Visible = true;
                            DepartureBoardHyperLink.NavigateUrl = locInfo.DepartureLink.Url;
                            
                        }

                        if (locInfo.ArrivalLink != null)
                        {
                            ArrivalsBoardHyperlink.Target = "_blank";
                            ArrivalsBoardHyperlink.Visible = true;
                            ArrivalBoardDiv.Visible = true;
                            realTime.Visible = true;
                            ArrivalsBoardHyperlink.NavigateUrl = locInfo.ArrivalLink.Url;
                            
                        }

                        if (locInfo.InformationLink != null)
                        {
                            FurtherDetailsHyperLink.Target = "_blank";
                            FurtherDetailsHyperLink.Visible = true;
                            FurtherDetailsDiv.Visible = true;
                            stationInfo.Visible = true;
                            stationFacilities.Text = GetResource("LocationInformation.airportFacilities.Text");
                            FurtherDetailsHyperLink.NavigateUrl = locInfo.InformationLink.Url;
                            SetFurtherDetailsLinkText(inputPageState.AdditionalDataLocation);
                            
                        }

                        if (locInfo.AccessibilityLink != null)
                        {
                            stationInfo.Visible = true;
                            stationFacilities.Text = GetResource("LocationInformation.airportFacilities.Text");
                            StationAccessibilityLink.Target = "_blank";
                            StationAccessibilityLink.Visible = true;
                            StationAccessDiv.Visible = true;
                            StationAccessibilityLink.NavigateUrl = locInfo.AccessibilityLink.Url;
                           
                        }
                    }
                }
            }

            #endregion

            //// Show Go To panel if links available
            //if ((DepartureBoardHyperLink.NavigateUrl != string.Empty) ||
            //    (ArrivalsBoardHyperlink.NavigateUrl != string.Empty) ||
            //    (FurtherDetailsHyperLink.NavigateUrl != string.Empty))
            //{
            //    panelGoTo.Visible = true;
            //}

            #endregion

            try
            {
                if (inputPageState.AdditionalDataLocation != "")
                {
                    if (inputPageState.AdditionalDataLocation.Substring(0, Airport.NaptanPrefix.Length) == Airport.NaptanPrefix)
                    {
                        labelStationInformationTitle.Visible = false;
                        //labelAirportInformationTitle.Visible = true;
                        labelLocationInformationTitle.Visible = false;

                        ZonalAirportOperatorControl1.AirportNaptan = inputPageState.AdditionalDataLocation.Substring(0, Airport.NaptanPrefix.Length + 3);
                    }
                }

                StopTaxiInformation selectedStop = new StopTaxiInformation(inputPageState.AdditionalDataLocation, true);
                //If we obtained a stop name from Atos DB on the StopTaxiInformation we need to populate
                //labelStationName.Text as this may be blank from the Atkins lookup previously done
                if (labelStationName.Text == "" && selectedStop.InformationAvailable)
                {
                    //Do a Naptan Lookup as the TrainTaxi names can differ
                    NaptanCacheEntry x = NaptanLookup.Get(selectedStop.StopNaptan, "Naptan");
                    labelStationName.Text = x.Description;
                }
                ZonalServiceLinksControl1.Naptan = inputPageState.AdditionalDataLocation;
                ZonalAccessibilityLinksControl1.AccessibilityByNaptan = false;
                ZonalAccessibilityLinksControl1.Naptan = inputPageState.AdditionalDataLocation;

                if (!selectedStop.InformationAvailable && ZonalServiceLinksControl1.IsEmpty && ZonalAccessibilityLinksControl1.IsEmpty && ZonalAirportOperatorControl1.IsEmpty)
                {
                    //panelTaxiDetails.Visible = false;
                    labelErrorMessage.Visible = true;
                    taxi.Visible = false;
                    TaxiInformationControl1.Visible = false;
                    localInfo.Visible = false;
                    stationInfo.Visible = false;
                    operatorInfo.Visible = false;
                    localInfo.Visible = false;
                    realTime.Visible = false;
                    //faresHyperlinkPostbackControl.Visible = false;
                    //airoperatorsHyperlinkPostbackControl.Visible = false;
                }
                else if (!selectedStop.InformationAvailable && ZonalAirportOperatorControl1.IsEmpty && ZonalAccessibilityLinksControl1.IsEmpty)
                {
                    MakeFaresInfoVisible();
                    taxi.Visible = false;
                    TaxiInformationControl1.Visible = false;
                    operatorInfo.Visible = false;
                    localInfo.Visible = false;
                    //taxiHyperlinkPostbackControl.Visible = false;
                    //airoperatorsHyperlinkPostbackControl.Visible = false;
                }
                else if (ZonalServiceLinksControl1.IsEmpty || ZonalAirportOperatorControl1.IsEmpty || ZonalAccessibilityLinksControl1.IsEmpty)
                {
                    TaxiInformationControl1.Data = selectedStop;

                    if (ZonalServiceLinksControl1.IsEmpty && ZonalAccessibilityLinksControl1.IsEmpty)
                    {
                        ZonalServiceLinksControl1.Visible = false;
                        ZonalAccessibilityLinksControl1.Visible = false;
                        localInfo.Visible = false;

                        //faresHyperlinkPostbackControl.Visible = false;
                    }

                    if (ZonalAccessibilityLinksControl1.IsEmpty)
                    {
                        ZonalAccessibilityLinksControl1.Visible = false;

                    }

                    if (ZonalServiceLinksControl1.IsEmpty)
                    {
                        ZonalServiceLinksControl1.Visible = false;

                    }

                    //if (ZonalAccessibilityLinksControl1.IsEmpty && !ZonalServiceLinksControl1.IsEmpty)
                    //{
                    //    ZonalAccessibilityLinksControl1.Visible = false;
                    //    ZonalServiceLinksControl1.Visible = true;
                    //    localInfo.Visible = true;
                    //}

                    //if (!ZonalAccessibilityLinksControl1.IsEmpty && ZonalServiceLinksControl1.IsEmpty)
                    //{
                    //    ZonalAccessibilityLinksControl1.Visible = true;
                    //    ZonalServiceLinksControl1.Visible = false;
                    //    localInfo.Visible = true;
                    //}

                    if (ZonalAirportOperatorControl1.IsEmpty)
                    {
                        ZonalAirportOperatorControl1.Visible = false;
                        operatorInfo.Visible = false;
                        //airoperatorsHyperlinkPostbackControl.Visible = false;
                    }
                    else
                    {
                        ZonalAirportOperatorControl1.Visible = true;
                        operatorInfo.Visible = true;
                        panelOperatorDetails.Visible = true;


                    }

                }
                else
                {
                    TaxiInformationControl1.Data = selectedStop;
                    panelOperatorDetails.Visible = true;
                    ZonalAirportOperatorControl1.Visible = true;
                    operatorInfo.Visible = true;

                }

                StationZonalAccessibilityLinks.AccessibilityByNaptan = true;
                StationZonalAccessibilityLinks.Naptan = inputPageState.AdditionalDataLocation;


                if (StationZonalAccessibilityLinks.IsEmpty)
                {
                    StationZonalAccessibilityLinks.Visible = false;
                    StationAccessibilityLink.Visible = true;
                }
                else
                {
                    StationZonalAccessibilityLinks.Visible = true;
                    StationAccessibilityLink.Visible = false;
                }

            }
            catch
            {
                TaxiInformationControl1.Visible = false;
                taxi.Visible = false;
                localInfo.Visible = false;
                labelErrorMessage.Visible = true;
                OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "missing property in PropertyService : locationinformation.furtherdetailsurl");
                Logger.Write(oe);
            }
        }

        private void MakeFaresInfoVisible()
        {
            TaxiInformationControl1.Visible = false;
            ZonalServiceLinksControl1.Visible = true;
            ZonalAccessibilityLinksControl1.Visible = true;
            localInfo.Visible = true;
            labelSummaryTitle.Text = GetResource("LocationInformation.labelSummaryTitle_Fares.Text");
            summaryPlannersSkipLink.Alt = GetResource("LocationInformation.SkipLink_Fares.AlternateText");
        }

        #endregion

        #region Private methods
        /// <summary>
        /// Sets the Further details link text based on the stop type of the Naptan
        /// </summary>
        /// <param name="naptan"></param>
        private void SetFurtherDetailsLinkText(string naptan)
        {
            try
            {
                IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
                QuerySchema gisResult = gisQuery.FindStopsInfoForStops(new string[] { naptan });

                string stopType = string.Empty;
                string linkText = string.Empty;
                string linkResource = GetResource("FurtherDetailsHyperLink.labelFurtherDetailsNavigation.StopTypeText");

                for (int i = 0; i < gisResult.Stops.Rows.Count; i++)
                {
                    QuerySchema.StopsRow row = (QuerySchema.StopsRow)gisResult.Stops.Rows[i];

                    stopType = row.stoptype;
                }

                #region Stop type text
                switch (stopType)
                {
                    case "AIR":	// Air
                    case "GAT":
                        linkText = string.Format(linkResource, GetResource("LocationInformation.FurtherDetailsLink.Airport.Text"));
                        break;
                    case "BCE":	// Bus/coach
                    case "BST":
                    case "BCQ":
                    case "BCS":
                    case "RLY":	// Rail
                    case "RPL":
                    case "RSE":
                        linkText = string.Format(linkResource, GetResource("LocationInformation.FurtherDetailsLink.Station.Text"));
                        break;
                    case "BCT":	// Bus/coach
                    case "MET":	// Light/rail
                    case "PLT":
                    case "TMU":
                        linkText = string.Format(linkResource, GetResource("LocationInformation.FurtherDetailsLink.Stop.Text"));
                        break;
                    case "TXR":	// Taxi
                    case "STR":
                        linkText = string.Format(linkResource, GetResource("LocationInformation.FurtherDetailsLink.Rank.Text"));
                        break;
                    case "FER":	// Ferry
                    case "FTD":
                        linkText = string.Format(linkResource, GetResource("LocationInformation.FurtherDetailsLink.Terminal.Text"));
                        break;
                    default:
                        linkText = GetResource("FurtherDetailsHyperLink.labelFurtherDetailsNavigation");
                        break;
                }
                #endregion

                labelFurtherDetailsNavigation.Text = string.Format("{0} {1}", linkText, GetResource("ExternalLinks.OpensNewWindowText"));
            }
            catch
            {
                labelFurtherDetailsNavigation.Text = string.Format("{0} {1}", GetResource("FurtherDetailsHyperLink.labelFurtherDetailsNavigation"), GetResource("ExternalLinks.OpensNewWindowText"));
            }
        }
        #endregion
    }
}