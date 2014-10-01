// *********************************************** 
// NAME             : StopInformation.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2014
// DESCRIPTION  	: StopInformation page for details about a stop/locaiton, e.g. Station board
//                  : THIS IS CURRENTLY FOR PROTOTYPING SO FUNCTIONALITY SHOULD BE DISABLED
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Web.UI;
using TDP.Common;
using TDP.Common.EventLogging;
using TDP.Common.Extenders;
using TDP.Common.LocationService;
using TDP.Common.PropertyManager;
using TDP.Common.ServiceDiscovery;
using TDP.Common.Web;
using TDP.UserPortal.DepartureBoardService.DepartureBoardServiceTypes;
using TDP.UserPortal.SessionManager;
using TDP.UserPortal.TDPMobile.Controls;
using DBS = TDP.UserPortal.DepartureBoardService;
using Logger = System.Diagnostics.Trace;

namespace TDP.UserPortal.TDPMobile
{
    /// <summary>
    /// Stop Information page
    /// </summary>
    public partial class StopInformation : TDPPageMobile
    {
        #region Private variables

        private bool stopInformationEnabled = false;

        private StopInformationHelper stopInformationHelper = null; 

        private TDPStopLocation location = null;
        private StopInformationMode mode = StopInformationMode.StationBoardDeparture;
        private DBSResult stationBoardResult = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public StopInformation()
            : base(Global.TDPResourceManager)
        {
            pageId = PageId.MobileStopInformation;
        }

        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_Init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            stationBoardControl.ShowDirectionHandler += new OnShowStationBoard(ShowStationBoardEvent);
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            stopInformationHelper = new StopInformationHelper();

            // Check if stop info functionality is enabled
            stopInformationEnabled = stopInformationHelper.StopInformationAvailable();
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupControls();

            SetupResources();

            SetupControlVisibility();

            // Use the browser back in the header
            ((TDPMobile)Master).DisplayBrowserBack = true;
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Shows the selected station board
        /// </summary>
        protected void ShowStationBoardEvent(object sender, StationBoardEventArgs e)
        {
            if (e != null)
            {
                // Persist selected station board for the direction to session
                stopInformationHelper.UpdateStopInformationMode(
                    e.ShowDepartures ? StopInformationMode.StationBoardDeparture : StopInformationMode.StationBoardArrival);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads resource strings for labels/controls
        /// </summary>
        private void SetupResources()
        {
            TDPPageMobile page = (TDPPageMobile)Page;

            if (stopInformationEnabled)
            {
                if (location == null)
                {
                    locationTitleDiv.Visible = false;
                    ((TDPMobile)this.Master).DisplayMessage(new TDPMessage("StopInformation.Location.Invalid", TDPMessageType.Error));
                }
                else
                {
                    locationTitle.Text = string.Format("{0}", location.DisplayName);

                    if (stationBoardResult == null || stationBoardResult.StopEvents == null || stationBoardResult.StopEvents.Length == 0)
                    {
                        List<string> args = new List<string>();
                        args.Add(location.DisplayName);

                        ((TDPMobile)this.Master).DisplayMessage(new TDPMessage(
                            string.Empty,
                            "StopInformation.StationBoard.Unavailable", string.Empty, string.Empty,
                            args, 0, 0,
                            TDPMessageType.Error));
                    }
                }
            }
        }

        /// <summary>
        /// Sets up the controls on the page
        /// </summary>
        private void SetupControls()
        {
            if (stopInformationEnabled)
            {
                // Identify location to display stop information for
                location = (Page.IsPostBack) ? 
                    stopInformationHelper.GetStopInformationLocation(false) : 
                    stopInformationHelper.GetStopInformationLocation(true);

                if (location != null)
                {
                    // Determine what information is being displayed
                    mode = (Page.IsPostBack) ? 
                        stopInformationHelper.GetStopInformationMode(false) : 
                        stopInformationHelper.GetStopInformationMode(true); 
                    
                    switch (mode)
                    {
                        case StopInformationMode.StationBoardDeparture:
                            stationBoardResult = GetStationBoard(location, true);
                            stationBoardControl.Initialise(location, true, stationBoardResult);
                            break;

                        case StopInformationMode.StationBoardArrival:
                            stationBoardResult = GetStationBoard(location, false);
                            stationBoardControl.Initialise(location, false, stationBoardResult);
                            break;
                    }
                }
                // Else Location not identified, message will be displayed
            }
        }

        /// <summary>
        /// Sets up the control visibilities
        /// </summary>
        private void SetupControlVisibility()
        {
            if (stopInformationEnabled)
            {
                // Hide the station board control if no station board
                if (stationBoardResult == null || stationBoardResult.StopEvents == null || stationBoardResult.StopEvents.Length == 0)
                    stationBoardControl.Visible = false;
            }
            else
            {
                stopInfoContainer.Visible = false;
            }
        }

        /// <summary>
        /// Retrieves the a service detail result for the service id
        /// </summary>
        /// <param name="serviceId"></param>
        private DBSResult GetStationBoard(TDPStopLocation location, bool showDepartures)
        {
            if (location != null)
            {
                // Get latest station board for stop location
                DBS.DepartureBoardService departureBoardService = TDPServiceDiscovery.Current.Get<DBS.DepartureBoardService>(ServiceDiscoveryKey.DepartureBoardService);

                // Parameters for call
                string requestId = TDPSessionManager.Current.Session.SessionID;
                int numberOfRows = Properties.Current["StopInformation.StationBoard.NumberOfRows"].Parse(10);
                int durationMins = Properties.Current["StopInformation.StationBoard.Duration.Mins"].Parse(60);

                DBSResult stationBoardResult = departureBoardService.GetStationBoard(
                    requestId, location, null, showDepartures, DateTime.Now, numberOfRows, durationMins);

                #region Log message

                if (TDPTraceSwitch.TraceVerbose)
                {
                    // Log any messages
                    if (stationBoardResult != null && stationBoardResult.Messages != null && stationBoardResult.Messages.Length > 0)
                    {
                        foreach (DBSMessage message in stationBoardResult.Messages)
                        {
                            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose,
                                string.Format("Station board result for {0}, Message: Code[{1}] Description[{2}]",
                                location.ID, message.Code, message.Description)));
                        }
                    }
                }

                #endregion

                return stationBoardResult;
            }

            return null;
        }

        #endregion
    }
}