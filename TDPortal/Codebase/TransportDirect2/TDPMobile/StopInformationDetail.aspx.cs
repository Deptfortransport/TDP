// *********************************************** 
// NAME             : StopInformationDetail.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2014
// DESCRIPTION  	: StopInformationDetail page for detailed information, e.g. a train service
//                  : THIS IS CURRENTLY FOR PROTOTYPING SO FUNCTIONALITY SHOULD BE DISABLED
// ************************************************
// 

using System;
using System.Web.UI;
using TDP.Common;
using TDP.Common.Extenders;
using TDP.Common.PropertyManager;
using TDP.Common.ServiceDiscovery;
using TDP.Common.Web;
using TDP.UserPortal.DepartureBoardService.DepartureBoardServiceTypes;
using TDP.UserPortal.SessionManager;
using DBS = TDP.UserPortal.DepartureBoardService;

namespace TDP.UserPortal.TDPMobile
{
    /// <summary>
    /// StopInformationDetail page for detailed information, e.g. a train service
    /// </summary>
    public partial class StopInformationDetail : TDPPageMobile
    {
        #region Private variables

        private StopInformationHelper stopInformationHelper = null; 

        private bool stopInformationEnabled = false;
        private string serviceId = null;
        private DBSResult serviceResult = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public StopInformationDetail()
            : base(Global.TDPResourceManager)
        {
            pageId = PageId.MobileStopInformationDetail;
        }

        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            stopInformationHelper = new StopInformationHelper();

            // Check if stop info functionality is enabled
            stopInformationEnabled = Properties.Current["StopInformation.Enabled.Switch"].Parse(false);

            AddJavascript("Information.js");
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
                if (string.IsNullOrEmpty(serviceId))
                {
                    // No service id provided
                    ((TDPMobile)this.Master).DisplayMessage(new TDPMessage("StopInformation.ServiceId.Invalid", TDPMessageType.Error));
                }
                else if (serviceResult == null || serviceResult.StopEvents == null || serviceResult.StopEvents.Length == 0)
                {
                    // No service result available for this service id
                    ((TDPMobile)this.Master).DisplayMessage(new TDPMessage("StopInformation.ServiceDetail.Unavailable", TDPMessageType.Error));
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
                // Currently only displaying for service details (train service calling points and times)
                serviceId = (Page.IsPostBack) ?
                    stopInformationHelper.GetStopInformationServiceId(false) :
                    stopInformationHelper.GetStopInformationServiceId(true);

                if (!string.IsNullOrEmpty(serviceId))
                {
                    // Retrieve the service detail result
                    serviceResult = GetServiceDetails(serviceId);

                    serviceDetailsControl.Initialise(serviceId, serviceResult);
                }
                // Service id not specified, message will be displayed
            }
        }

        /// <summary>
        /// Sets up the control visibilities
        /// </summary>
        private void SetupControlVisibility()
        {
            if (stopInformationEnabled)
            {
                // Hide the service details control if no service result
                if (serviceResult == null || serviceResult.StopEvents == null || serviceResult.StopEvents.Length == 0)
                    serviceDetailsControl.Visible = false;
            }
            else
            {
                stopInfoDetailContainer.Visible = false;
            }
        }

        /// <summary>
        /// Retrieves the a service detail result for the service id
        /// </summary>
        /// <param name="serviceId"></param>
        private DBSResult GetServiceDetails(string serviceId)
        {
            if (!string.IsNullOrEmpty(serviceId))
            {
                // Get latest service details for the service id
                DBS.DepartureBoardService departureBoardService = TDPServiceDiscovery.Current.Get<DBS.DepartureBoardService>(ServiceDiscoveryKey.DepartureBoardService);

                // Parameters for call
                string requestId = TDPSessionManager.Current.Session.SessionID;

                DBSResult serviceResult = departureBoardService.GetServiceDetails(
                    requestId, DateTime.Now, serviceId);

                return serviceResult;
            }

            return null;
        }

        #endregion
    }
}