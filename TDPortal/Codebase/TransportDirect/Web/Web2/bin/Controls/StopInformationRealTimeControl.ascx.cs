// *********************************************** 
// NAME                 : StopInformationRealTimeControl.ascx.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 10/09/2009 
// DESCRIPTION  		: Stop information real time links control
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/StopInformationRealTimeControl.ascx.cs-arc  $
//
//   Rev 1.1   Sep 14 2009 15:15:50   apatel
//updated header logging
//Resolution for 5315: CCN526 Stop Information Page Landing

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;
using TransportDirect.Common;
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.UserPortal.LocationInformationService;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Stop information real time links control
    /// </summary>
    public partial class StopInformationRealTimeControl : TDUserControl
    {
        #region Private Fields
        private bool isEmpty = true;
        private TDStopType stopType;
        private string naptan = string.Empty;
        private string crsCode = string.Empty;
        #endregion

        #region Public Properties
        /// <summary>
        /// Read only property specifying if the control got any data to display
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return isEmpty;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Initialises control
        /// </summary>
        /// <param name="stopType"></param>
        /// <param name="naptan"></param>
        /// <param name="crsCode"></param>
        public void Initialise(TDStopType stopType, string naptan, string crsCode)
        {
            this.stopType = stopType;
            this.naptan = naptan;
            this.crsCode = crsCode;

            SetupControls();
            
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Sets up page controls
        /// </summary>
        private void SetupControls()
        {
            realTimeInfo.Text = GetResource("LocationInformation.realTimeInfo.Text");

            labelDepartureBoardNavigation.Text = string.Format("{0} {1}", GetResource("DepartureBoardHyperLink.labelDepartureBoardNavigation"), GetResource("ExternalLinks.OpensNewWindowImage"));

            labelArrivalsBoardNavigation.Text = string.Format("{0} {1}", GetResource("ArrivalsBoardHyperlink.labelArrivalsBoardNavigation"), GetResource("ExternalLinks.OpensNewWindowImage"));


            // check if the stop type is rail than show links for rail
            if (stopType == TDStopType.Rail && crsCode.Length != 0)
            {

                try
                {
                    DepartureBoardHyperLink.NavigateUrl = string.Format(Properties.Current["locationinformation.departureboardurl"], crsCode);
                    DepartureBoardHyperLink.Target = "_blank";
                    DepartureBoardHyperLink.Visible = true;


                }
                catch
                {
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "missing property in PropertyService : locationinformation.departureboardurl");
                    Logger.Write(oe);
                    throw new TDException("missing property in PropertyService : locationinformation.departureboardurl", true, TDExceptionIdentifier.PSMissingProperty);
                }
            }

            if (!string.IsNullOrEmpty(naptan))
            {
                if (naptan.Substring(0, Airport.NaptanPrefix.Length) == Airport.NaptanPrefix)
                {
                    // Obtain Go To urls for airport
                    LocationInformationCatalogue refData = (LocationInformationCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationInformation];
                    LocationInformationService.LocationInformation locInfo = refData.GetLocationInformation(naptan);

                    if (locInfo != null)
                    {
                        // Set up the hyperlinks
                        if (locInfo.DepartureLink != null)
                        {
                            DepartureBoardHyperLink.Target = "_blank";
                            DepartureBoardHyperLink.Visible = true;
                            DepartureBoardHyperLink.NavigateUrl = locInfo.DepartureLink.Url;

                        }

                        if (locInfo.ArrivalLink != null)
                        {
                            ArrivalsBoardHyperlink.Target = "_blank";
                            ArrivalsBoardHyperlink.Visible = true;
                            ArrivalsBoardHyperlink.NavigateUrl = locInfo.ArrivalLink.Url;

                        }


                    }
                }
            }

            if (!string.IsNullOrEmpty(DepartureBoardHyperLink.NavigateUrl)
                || !string.IsNullOrEmpty(ArrivalsBoardHyperlink.NavigateUrl))
            {
                isEmpty = false;
            }
        }
        #endregion
    }
}