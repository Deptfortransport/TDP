// *********************************************** 
// NAME                 : StopInformationDepartureBoardControl.ascx.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 10/09/2009 
// DESCRIPTION  		: Stop information departure board control displays departure board info
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/StopInformationDepartureBoardControl.ascx.cs-arc  $
//
//   Rev 1.15   Jun 13 2012 15:31:12   PScott
//Add NRE logo and hyperlink at bottom of stop info page for rail stations
//Resolution for 5816: Add NRE link to footer of stop information Next Departures frame
//
//   Rev 1.14   Oct 06 2010 13:22:12   apatel
//Initialize method updated to reset inputpagestate's variables which tracks the departure board result for arrival and departure.
//Resolution for 5616: Stop Information departure board issues spotted by DFT in Del 10.13
//
//   Rev 1.13   Mar 26 2010 12:00:20   RHopkins
//Reduce number of calls made to CJP when showing Departure Boards and Stop Events
//Resolution for 5450: Stop Information pages make excessive calls to CJP
//
//   Rev 1.12   Feb 18 2010 12:37:58   RHopkins
//Pass OperatorCode in StopEvent request
//Resolution for 5403: Pass OperatorCode in StopEvents request
//
//   Rev 1.11   Dec 04 2009 14:59:32   apatel
//Departure board changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.10   Dec 02 2009 16:27:24   mmodi
//Check for null stopevents before working with them
//
//   Rev 1.9   Nov 22 2009 15:57:56   pghumra
//Stop Information departure board changes
//Resolution for 5338: Stop Information Code Fixes
//
//   Rev 1.8   Nov 18 2009 16:00:28   pghumra
//Code fixes for TDP release patch 10.8.2.3 - Stop Information page
//Resolution for 5338: Stop Information Code Fixes
//
//   Rev 1.7   Oct 29 2009 11:05:08   apatel
//Stop Information changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.6   Oct 27 2009 09:53:22   apatel
//Stop Information Departure board control changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.5   Oct 23 2009 09:05:02   apatel
//Stop info departure board control changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.4   Oct 15 2009 14:49:00   apatel
//Stop Information Departure Board Service code changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.3   Oct 06 2009 14:41:38   apatel
//Stop Information code changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.2   Sep 14 2009 15:31:24   apatel
//corrected initialise method for arrival dbs result
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.1   Sep 14 2009 15:15:34   apatel
//updated header logging
//Resolution for 5315: CCN526 Stop Information Page Landing

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using System.Text;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Stop information departure board control
    /// </summary>
    public partial class StopInformationDepartureBoardControl : TDUserControl
    {
        #region Private Fields
        private InputPageState inputPageState;
        private bool isEmpty = true;
        private DBSResult dbsResult = null;
        private bool showDepartures = true;
        private TDStopType stopType = TDStopType.Unknown;
        private TDCodeType codeType = TDCodeType.SMS;
        private string code = string.Empty;
        private int maxNumOfServices;
        private TDDateTime serviceTime = TDDateTime.Now;

        #endregion

        #region Public Properties
        /// <summary>
        /// Read only property specifing if the control got no data to show
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return isEmpty;
            }
        }

        /// <summary>
        /// Read only property for service button
        /// </summary>
        public TDButton ArrivalDepartureToggleButton
        {
            get
            {
                return ServiceButton;
            }
        }
        #endregion

        #region Page Events
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MobileServiceLink.Click += new EventHandler(MobileServiceLink_Click);

        }


        #endregion

        /// <summary>
        /// Page load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            inputPageState = TDSessionManager.Current.InputPageState;

            ServiceButton.Text = GetResource("StopInformationDepartureBoardControl.serviceButton.Arrivals.Text");
            NRELogo.ImageUrl = GetResource("StopInformationDepartureBoardControl.imageNRELogo");
            NRELink.Text = GetResource("StopInformationDepartureBoardControl.labelNRELink");

            if (stopType == TDStopType.Rail)
            {
                NRELogo.Visible = true;
                NRELink.Visible = true;
            }
            else
            {
                NRELogo.Visible = false;
                NRELink.Visible = false;
            }
            MobileServiceLink.Text = GetResource("StopInformationDepartureBoardControl.mobileServiceLink.Text");


            MobileServiceLink.Visible = bool.Parse(Properties.Current["StopInformation.DepartureBoardControl.ShowMobileLink"]);

            ServiceButton.Visible = bool.Parse(Properties.Current["StopInformation.DepartureBoardControl.ShowServiceButton"]);

            string serviceTimeStr = GetResource("StopInformationDepartureBoardControl.labelDepartureBoardTime.Text");

            if (!string.IsNullOrEmpty(serviceTimeStr))
            {
                labelDepartureBoardTime.Text = string.Format(serviceTimeStr, serviceTime.ToString("ddd dd MMM yy"));
            }


            this.MobileServiceLink.Click += new EventHandler(MobileServiceLink_Click);

            SetupMobileServiceUrl();
        }


        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadDepartureServiceResults();
            }

            if (isEmpty)
            {
                this.Visible = false;
            }
        }
        #endregion

        #region Control Events
        /// <summary>
        /// Mobile Service Link click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MobileServiceLink_Click(object sender, EventArgs e)
        {

            TDSessionManager.Current.IsStopInformationMode = true;

            // Only need to induce a flag ensuring that this is on the way back from the location page.
            TDSessionManager.Current.SetOneUseKey(SessionKey.IndirectLocationPostBack, string.Empty);
            TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.TDOnTheMove;


        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Initialises the control
        /// </summary>
        /// <param name="stopType"></param>
        /// <param name="code"></param>
        /// <param name="codeType"></param>
        /// <param name="stopName"></param>
        /// <param name="showDepartures"></param>
        /// <param name="maxNumOfServices"></param>
        /// <param name="tdDateTime"></param>
        public void Initialise(TDStopType stopType, string code, TDCodeType codeType, string stopName,
            bool showDepartures, int maxNumOfServices, TDDateTime tdDateTime)
        {

            this.showDepartures = showDepartures;

            this.stopType = stopType;

            this.codeType = codeType;

            labelDepartureBoardSubTitle.Text = stopName;

            this.code = code;

            this.maxNumOfServices = maxNumOfServices;

            this.serviceTime = tdDateTime;

            // Reset the input page state variables
            inputPageState = TDSessionManager.Current.InputPageState;

            inputPageState.StopInformationArriveExists = false;

            inputPageState.StopInformationDepartExists = false;

            inputPageState.StopInformationDepartArriveChecked = false;



        }

        /// <summary>
        /// Refresh the control
        /// </summary>
        public void Refresh()
        {
            SetupMobileServiceUrl();

            LoadDepartureServiceResults();
        }




        #endregion

        #region Private Methods
        /// <summary>
        /// Does a Departure board request and returns the result coming from the Departure board service
        /// </summary>
        /// <param name="stopType"></param>
        /// <param name="code"></param>
        /// <param name="codeType"></param>
        /// <param name="maxNumOfServices"></param>
        /// <param name="tdDateTime"></param>
        /// <param name="showDepartures"></param>
        /// <returns>Departure board service result</returns>
        private DBSResult GetDBSResult(TDStopType stopType, string code, TDCodeType codeType, int maxNumOfServices, TDDateTime tdDateTime, bool showDepartures)
        {
            DBSResult dbsResult = null;
            try
            {

                DepartureBoardHelper dbHelper = new DepartureBoardHelper();

                IDepartureBoardService dbs = (IDepartureBoardService)TDServiceDiscovery.Current[ServiceDiscoveryKey.DepartureBoardService];

                TDCodeType ct = TDCodeType.SMS;
                if ((codeType == TDCodeType.SMS) && (stopType == TDStopType.Bus))
                {
                    ct = TDCodeType.SMS;
                }
                else if (codeType == TDCodeType.NAPTAN)
                {
                    ct = TDCodeType.NAPTAN;
                }
                else
                {
                    ct = TDCodeType.CRS;
                }
                //Build DBSRequest object
                DBSRequest dbsRequest = dbHelper.CreateDepartureBoardServiceRequest(TDSessionManager.Current.Session.SessionID, code, ct,
                    maxNumOfServices, tdDateTime, showDepartures);


                dbsResult = new DBSResult();

                //Request to a Departure board service
                dbsResult = dbs.GetDepartureBoardTrip(dbsRequest.TransactionId, dbsRequest.OriginLocation, dbsRequest.DestinationLocation,
                    dbsRequest.OperatorCode, dbsRequest.ServiceNumber, dbsRequest.JourneyTimeInformation, dbsRequest.RangeType, dbsRequest.Range,
                    dbsRequest.ShowDepartures, dbsRequest.ShowCallingStops);

            }
            catch (TDException tdException)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.Business, TDTraceLevel.Error, "Departure Board Service Exception." + tdException.Message);

                Logger.Write(operationalEvent);

            }
            catch (Exception exception)
            {

                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.Business, TDTraceLevel.Error, "Departure Board Service Exception." + exception.Message);

                Logger.Write(operationalEvent);
            }

            return dbsResult;
        }

        /// <summary>
        /// Logs departure board service result messages
        /// </summary>
        /// <param name="dbsResult">Departure board service result</param>
        private void LogDeparturBoardServiceMessages(DBSResult dbsResult)
        {
            if (dbsResult.Messages.Length > 0)
            {
                StringBuilder messageBuilder = new StringBuilder();

                foreach (DBSMessage message in dbsResult.Messages)
                {
                    if (!string.IsNullOrEmpty(message.Description))
                    {
                        if (string.IsNullOrEmpty(messageBuilder.ToString()))
                        {
                            messageBuilder.Append(",");
                        }
                        messageBuilder.Append(string.Format("{0}:{1}", message.Code, message.Description));
                    }
                }
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.Business, TDTraceLevel.Error, "Departure Board Service Messages:" + messageBuilder.ToString());

                Logger.Write(operationalEvent);
            }
        }

        /// <summary>
        /// Setup urls for TDOnMove page
        /// </summary>
        private void SetupMobileServiceUrl()
        {
            inputPageState.StopServiceDetailsStopCode = code;

            inputPageState.StopServiceDetailsStopType = stopType;

            inputPageState.StopServiceDetailsCodeType = codeType;

            string tdOnMoveUrl = string.Empty;

            if (showDepartures)
            {
                //url for departure
                tdOnMoveUrl = string.Format(Properties.Current["TDOnTheMove.TDMobileUI.URL.Departure"], code, stopType.ToString().ToLower(),
                    serviceTime.GetDateTime() == DateTime.MinValue ? "now" : serviceTime.ToString("HHmm"));
            }
            else
            {
                //url for arrival
                tdOnMoveUrl = string.Format(Properties.Current["TDOnTheMove.TDMobileUI.URL.Arrival"], code, stopType.ToString().ToLower(),
                   serviceTime.GetDateTime() == DateTime.MinValue ? "now" : serviceTime.ToString("HHmm"));
            }

            inputPageState.StopInfromationTDOnMoveUrl = tdOnMoveUrl;
        }

        /// <summary>
        /// Loads Departure service results
        /// </summary>
        private void LoadDepartureServiceResults()
        {
            if (showDepartures || !inputPageState.StopInformationDepartArriveChecked)
            {
                // result for departure
                DBSResult departureResult = GetDBSResult(stopType, code, codeType, maxNumOfServices, serviceTime, true);

                if (departureResult != null)
                {
                    if ((departureResult.StopEvents != null) && (departureResult.StopEvents.Length > 0))
                    {
                        inputPageState.StopInformationDepartExists = true;
                        dbsResult = departureResult;
                    }
                    else
                    {
                        LogDeparturBoardServiceMessages(departureResult);
                    }
                }
            }

            if (showDepartures && !inputPageState.StopInformationDepartExists)
            {
                showDepartures = false;
            }

            if (!showDepartures || !inputPageState.StopInformationDepartArriveChecked)
            {
                //result for arrival
                DBSResult arrivalResult = GetDBSResult(stopType, code, codeType, maxNumOfServices, serviceTime, false);

                if (arrivalResult != null)
                {
                    if ((arrivalResult.StopEvents != null) && (arrivalResult.StopEvents.Length > 0))
                    {
                        inputPageState.StopInformationArriveExists = true;
                        if (!showDepartures)
                        {
                            dbsResult = arrivalResult;
                        }
                    }
                }
                else
                {
                    LogDeparturBoardServiceMessages(arrivalResult);
                }
            }

            if (!inputPageState.StopInformationDepartExists && !inputPageState.StopInformationArriveExists)
            {
                isEmpty = true;

            }
            else
            {
                isEmpty = false;

                #region Set labels

                if (!showDepartures)
                {
                    ServiceButton.Text = GetResource("StopInformationDepartureBoardControl.serviceButton.Departures.Text");
                    labelDepartureBoardTitle.Text = GetResource("StopInformationDepartureBoardControl.labelDepartureBoardTitle.Arrivals.Text");

                    imageDepartureBoard.ImageUrl = GetResource("StopInformationDepartureBoardControl.imageArrivalBoardUrl");
                    imageDepartureBoard.AlternateText = GetResource("StopInformationDepartureBoardControl.imageArrivalBoardUrl.AlternateText");

                }
                else
                {
                    ServiceButton.Text = GetResource("StopInformationDepartureBoardControl.serviceButton.Arrivals.Text");
                    labelDepartureBoardTitle.Text = GetResource("StopInformationDepartureBoardControl.labelDepartureBoardTitle.Departures.Text");

                    imageDepartureBoard.ImageUrl = GetResource("StopInformationDepartureBoardControl.imageDepartureBoardUrl");
                    imageDepartureBoard.AlternateText = GetResource("StopInformationDepartureBoardControl.imageDepartureBoardUrl.AlternateText");
                }

                #endregion

                //if only departure result or arrival result found hide servicebutton
                ServiceButton.Visible = (inputPageState.StopInformationDepartExists && inputPageState.StopInformationArriveExists);

                // initialise departure board result control
                DepartureBoardResult.Initialise(dbsResult, stopType, showDepartures);
            }
            
            // We have looked for both Depart and Arrive
            inputPageState.StopInformationDepartArriveChecked = true;
        }

        #endregion


    }
}