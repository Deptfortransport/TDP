// *********************************************** 
// NAME                 : StopInformationFacilityControl.ascx.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 10/09/2009 
// DESCRIPTION  		: Stop information facility control
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/StopInformationFacilityControl.ascx.cs-arc  $
//
//   Rev 1.3   Oct 30 2009 09:36:22   apatel
//Added fix for airport naptan for zonal accessibility link
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.2   Oct 29 2009 11:05:14   apatel
//Stop Information changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.1   Sep 14 2009 15:15:38   apatel
//updated header logging
//Resolution for 5315: CCN526 Stop Information Page Landing

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.LocationInformationService;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.UserPortal.AdditionalDataModule;
using TransportDirect.UserPortal.ZonalServices;
using TransportDirect.UserPortal.ExternalLinkService;
using TransportDirect.UserPortal.AirDataProvider;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Stop information facility links control
    /// </summary>
    public partial class StopInformationFacilityControl : TDUserControl
    {
        #region Private Fields
        private bool isEmpty = true;
        private TDStopType stopType;
        private string naptan = string.Empty;
        private string crsCode = string.Empty;
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
        #endregion

        #region Public Methods
        /// <summary>
        /// Initialises the control
        /// </summary>
        /// <param name="stopType"></param>
        /// <param name="naptan"></param>
        /// <param name="crsCode"></param>
        public void Initialise(TDStopType stopType, string naptan, string crsCode)
        {
            this.stopType = stopType;
            this.naptan = naptan;
            this.crsCode = crsCode;

            SetControlTitle(stopType);

            IAdditionalData addData = (IAdditionalData)TDServiceDiscovery.Current[ServiceDiscoveryKey.AdditionalData];

            string stopName = addData.LookupStationNameForNaptan(naptan);

            labelStationFacilitiesText.Text = string.Format(GetResource("StopInformation.labelStationFacilitiesText.Text"), stopName);

            StationZonalAccessibilityLinks.AccessibilityByNaptan = true;
            StationZonalAccessibilityLinks.Naptan = naptan;

            if (StationZonalAccessibilityLinks.IsEmpty)
            {
                if (stopType == TDStopType.Air)
                {
                    string naptanWithoutTerminalNo = naptan.Substring(0, Airport.NaptanPrefix.Length + 3);

                    StationZonalAccessibilityLinks.AccessibilityByNaptan = true;
                    StationZonalAccessibilityLinks.Naptan = naptanWithoutTerminalNo;
                }
            }
            

            SetFurtherDetailsLinkText(naptan);

            StationAccessibilityLink.Text = string.Format("{0} {1}", GetResource("JourneyDetails.accessibilityLink.Text"), GetResource("ExternalLinks.OpensNewWindowImage"));

            if (!string.IsNullOrEmpty(crsCode))
            {
                StationAccessibilityLink.NavigateUrl = string.Format(Properties.Current["locationinformation.accessibilityurl"], crsCode);
                StationAccessibilityLink.Target = "_blank";

                FurtherDetailsHyperLink.NavigateUrl = string.Format(Properties.Current["locationinformation.furtherdetailsurl"], crsCode);
                FurtherDetailsHyperLink.Target = "_blank";
                FurtherDetailsHyperLink.Visible = true;


            }
            else
            {
                LocationInformationCatalogue refData = (LocationInformationCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationInformation];
                LocationInformationService.LocationInformation locInfo = refData.GetLocationInformation(naptan);

                if (locInfo != null)
                {
                    if (locInfo.AccessibilityLink != null)
                    {
                        StationAccessibilityLink.NavigateUrl = locInfo.AccessibilityLink.Url;
                        StationAccessibilityLink.Target = "_blank";
                    }

                    if (locInfo.InformationLink != null)
                    {
                        FurtherDetailsHyperLink.Target = "_blank";
                        FurtherDetailsHyperLink.Visible = true;
                        FurtherDetailsHyperLink.NavigateUrl = locInfo.InformationLink.Url;

                    }
                }

            }


            if (StationZonalAccessibilityLinks.IsEmpty)
            {
                StationZonalAccessibilityLinks.Visible = false;
                StationAccessibilityLink.Visible = !string.IsNullOrEmpty(StationAccessibilityLink.NavigateUrl);
               
            }
            else
            {
                StationZonalAccessibilityLinks.Visible = true;
                StationAccessibilityLink.Visible = false;
            }


            if (!string.IsNullOrEmpty(StationAccessibilityLink.NavigateUrl)
                || !string.IsNullOrEmpty(FurtherDetailsHyperLink.NavigateUrl)
                || !StationZonalAccessibilityLinks.IsEmpty)
            {
                isEmpty = false;
            }
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

                labelFurtherDetailsNavigation.Text = string.Format("{0} {1}", linkText, GetResource("ExternalLinks.OpensNewWindowImage"));
            }
            catch
            {
                labelFurtherDetailsNavigation.Text = string.Format("{0} {1}", GetResource("FurtherDetailsHyperLink.labelFurtherDetailsNavigation"), GetResource("ExternalLinks.OpensNewWindowImage"));
            }
        }

        /// <summary>
        /// Sets the Facility control's title
        /// </summary>
        /// <param name="stopType">Type of stop for which facility information displayed i.e. air, rail, etc.</param>
        private void SetControlTitle(TDStopType stopType)
        {
            string stopText = GetResource("LocationInformation.facilities.Text");

            if (stopType == TDStopType.Air)
            {
                stopText = GetResource("LocationInformation.airportFacilities.Text");
            }
            else if (stopType == TDStopType.Rail)
            {
                stopText = GetResource("LocationInformation.stationFacilities.Text");
            }
            else if (stopType == TDStopType.Ferry)
            {
                stopText = GetResource("LocationInformation.ferryFacilities.Text");
            }

            stationFacilities.Text = stopText;

        }
        #endregion
    }
}