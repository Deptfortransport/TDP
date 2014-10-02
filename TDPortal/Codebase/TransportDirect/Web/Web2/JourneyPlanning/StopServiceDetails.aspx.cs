// *********************************************** 
// NAME                 : StopServiceDetails.aspx.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 10/09/2009 
// DESCRIPTION  		: Stop information service detail page similar to service detail page
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/StopServiceDetails.aspx.cs-arc  $ 
//
//   Rev 1.12   Mar 21 2013 15:45:30   rbroddle
//Amended to translate operators for MDV region prefixes
//Resolution for 5906: MDV Issue - Stop Events Not Being Displayed in MDV Regions Except London
//
//   Rev 1.11   Apr 27 2010 15:55:56   rhopkins
//Corrected date conversion error
//Resolution for 5450: Stop Information pages make excessive calls to CJP
//
//   Rev 1.10   Mar 26 2010 12:00:24   RHopkins
//Reduce number of calls made to CJP when showing Departure Boards and Stop Events
//Resolution for 5450: Stop Information pages make excessive calls to CJP
//
//   Rev 1.9   Feb 18 2010 12:37:58   RHopkins
//Pass OperatorCode in StopEvent request
//Resolution for 5403: Pass OperatorCode in StopEvents request
//
//   Rev 1.8   Dec 04 2009 14:59:34   apatel
//Departure board changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.7   Nov 22 2009 15:57:54   pghumra
//Stop Information departure board changes
//Resolution for 5338: Stop Information Code Fixes
//
//   Rev 1.6   Oct 23 2009 13:28:46   apatel
//Bus Stop service changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.5   Oct 23 2009 13:08:40   apatel
//Added check to see if stop events are null
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.4   Oct 23 2009 09:05:06   apatel
//Stop info departure board control changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.3   Oct 15 2009 14:49:12   apatel
//Stop Information Departure Board Service code changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.2   Oct 06 2009 14:41:46   apatel
//Stop Information code changes
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

using TransportDirect.Common;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.JourneyControl;
using Logger = System.Diagnostics.Trace;
using System.Globalization;

namespace TransportDirect.UserPortal.Web.JourneyPlanning
{
    /// <summary>
    /// Stop Service detail page
    /// </summary>
    public partial class StopServiceDetails : TDPage
    {
        #region Constructor
        /// <summary>
        /// Constructor - sets the page id
        /// </summary>
        public StopServiceDetails()
            : base()
        {
            pageId = PageId.StopServiceDetails;
        }
        #endregion

        #region Page Events

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
            buttonBack.Click += new EventHandler(this.buttonBackClick);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ID = "StopServiceDetails";

        }
        #endregion

        /// <summary>
        /// Obtains the PublicJourneyDetail for the current leg of the journey 
        /// and uses it to initialise the constituent controls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.PageTitle = GetResource("ServiceDetails.PageTitle");

            labelServiceDetailsTitle.Text = PageTitle;

            buttonBack.Text = GetResource("JourneyPlannerLocationMap.buttonBack.Text");


            DBSResult dbsResult = null;

            if (!IsPostBack)
            {
                dbsResult = GetDBSStopEventForService();
            }

            if (dbsResult != null)
            {

                if (dbsResult.StopEvents != null)
                {
                    if (dbsResult.StopEvents.Length > 0)
                    {
                        InitialiseControls(dbsResult.StopEvents[0]);
                    }
                }

                if (dbsResult.Messages.Length > 0)
                {
                    string errormessages = string.Empty;

                    foreach (DBSMessage message in dbsResult.Messages)
                    {
                        errormessages += message.Description;
                    }

                    // Log the messages
                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.Business, TDTraceLevel.Info, "Departure Board Service Messages:" + errormessages);

                    Logger.Write(operationalEvent);

                    labelErrorMessages.Text = GetResource("StopServiceDetails.NoStopEvents.Text");
                    panelErrorMessage.Visible = true;
                    StopServiceDetailDiv.Visible = false;

                }

            }

            //Added for white labelling:
            ConfigureLeftMenu("FindCoachInput.clientLink.BookmarkTitle", "FindCoachInput.clientLink.LinkText", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextServiceDetails);
            expandableMenuControl.AddExpandedCategory("Related links");

        }

        #endregion

        #region Control Events
        /// <summary>
        /// Back button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBackClick(object sender, EventArgs e)
        {
            TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.StopInformation;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Gets the Departure board result for the service
        /// </summary>
        /// <returns>DBSResult object</returns>
        private DBSResult GetDBSStopEventForService()
        {
            try
            {
                DepartureBoardHelper dbHelper = new DepartureBoardHelper();

                IDepartureBoardService dbs = (IDepartureBoardService)TDServiceDiscovery.Current[ServiceDiscoveryKey.DepartureBoardService];

                IOperatorCatalogue operatorCatalogue = OperatorCatalogue.Current;

                InputPageState inputPageState = TDSessionManager.Current.InputPageState;

                TDStopType stopType = inputPageState.StopServiceDetailsStopType;

                int range = int.Parse(Properties.Current["StopInformation.ServiceControl.Train.MaxServices"]);

                if (stopType == TDStopType.Bus)
                {
                    range = int.Parse(Properties.Current["StopInformation.ServiceControl.Bus.MaxServices"]);

                }

                string code = inputPageState.StopServiceDetailsStopCode;

                string encodedServiceTime = (string)Request.QueryString["servicetime"];
                TDDateTime serviceTime = TDDateTime.Parse(HttpUtility.UrlDecode(encodedServiceTime), CultureInfo.CurrentCulture);

                TDCodeType codeType = inputPageState.StopServiceDetailsCodeType;

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

                DBSRequest dbsRequest = dbHelper.CreateDepartureBoardServiceRequest(TDSessionManager.Current.Session.SessionID, code, ct,
                    range, serviceTime, true);

                dbsRequest.ShowCallingStops = true;
                dbsRequest.OperatorCode = operatorCatalogue.TranslateOperator((string)Request.QueryString["operatorcode"]);
                dbsRequest.ServiceNumber = (string)Request.QueryString["servicenumber"];


                DBSResult dbsResult = new DBSResult();
                dbsResult = dbs.GetDepartureBoardTrip(dbsRequest.TransactionId, dbsRequest.OriginLocation, dbsRequest.DestinationLocation,
                    dbsRequest.OperatorCode, dbsRequest.ServiceNumber, dbsRequest.JourneyTimeInformation, dbsRequest.RangeType, dbsRequest.Range,
                    dbsRequest.ShowDepartures, dbsRequest.ShowCallingStops);

                return dbsResult;
            }
            catch (Exception exception)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.Business, TDTraceLevel.Error, "Departure Board Service Exception." + exception.Message);

                Logger.Write(operationalEvent);
            }

            return null;


        }

        /// <summary>
        /// initialises the page controls for the service
        /// </summary>
        /// <param name="stopEvent">Departure board service stop event</param>
        private void InitialiseControls(DBSStopEvent stopEvent)
        {
            try
            {
                string headerFormatString = GetResource("ServiceDetails.serviceHeaderLabel.Text");

                if (!string.IsNullOrEmpty(headerFormatString))
                {
                    serviceHeaderLabel.Text = string.Format(headerFormatString, stopEvent.Stop.Stop.Name);
                }

                StopCallingPointsFormatter callingPoints = new StopCallingPointsFormatter(stopEvent);

                callingPointsControlBefore.Mode = CallingPointControlType.Before;
                callingPointsControlBefore.CallingPoints = callingPoints.GetCallingPointLines(CallingPointControlType.Before);

                callingPointsControlLeg.Mode = CallingPointControlType.Leg;
                callingPointsControlLeg.CallingPoints = callingPoints.GetCallingPointLines(CallingPointControlType.Leg);

                callingPointsControlAfter.Mode = CallingPointControlType.After;
                callingPointsControlAfter.CallingPoints = callingPoints.GetCallingPointLines(CallingPointControlType.After);
            }
            catch (Exception exception)
            {
                // Log the exception
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.Business, TDTraceLevel.Error, "Stop Service Detail Exception." + exception.Message);

                Logger.Write(operationalEvent);
            }
        }



        #endregion




    }
}
