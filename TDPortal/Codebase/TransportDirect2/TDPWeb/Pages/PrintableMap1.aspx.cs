// *********************************************** 
// NAME             : PrintableMap1.aspx.cs      
// AUTHOR           : David Lane
// DATE CREATED     : 29 May 2012
// DESCRIPTION  	: Printable map page for Bing maps
// ************************************************
// 

using TDP.Common;
using TDP.Common.LocationService;
using TDP.Common.ResourceManager;
using TDP.Common.Web;
using TDP.UserPortal.JourneyControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TDP.UserPortal.TDPWeb.Pages
{
    /// <summary>
    /// Printable page for Bing maps
    /// </summary>
    public partial class PrintableMap1 : TDPPage
    {
        // Resource strings
        private string TXT_JourneyDirectionOutward = string.Empty;
        private string TXT_JourneyDirectionReturn = string.Empty;
        private string TXT_JourneyHeaderOutward = string.Empty;
        private string TXT_JourneyHeaderReturn = string.Empty;
        private string TXT_JourneyArriveBy = string.Empty;
        private string TXT_JourneyLeavingAt = string.Empty;
        private string TXT_Transport = string.Empty;
        private string TXT_Changes = string.Empty;
        private string TXT_Leave = string.Empty;
        private string TXT_Arrive = string.Empty;
        private string TXT_JourneyTime = string.Empty;
        private string TXT_Select = string.Empty;
        private static string RG = TDPResourceManager.GROUP_JOURNEYOUTPUT;
        private static string RC = TDPResourceManager.COLLECTION_JOURNEY;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public PrintableMap1()
            : base(Global.TDPResourceManager)
        {
            pageId = PageId.PrintableMapBing;
        }

        #endregion
    
        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SetupPage();
            AddJavascript("Map1.js");
            AddJavascript("Common.js");
            AddStyleSheet("SJPPrint.css");
        }

        /// <summary>
        /// Setup page content
        /// </summary>
        private void SetupPage()
        {
            // Don't display the sidebars
            ((TDPWeb)this.Master).DisplaySideBarLeft = false;
            ((TDPWeb)this.Master).DisplaySideBarRight = false;

            TDPPage page = (TDPPage)Page;

            // Check we have journey request hash and journey id
            if (page.QueryStringContains(QueryStringKey.JourneyRequestHash))
            {
                // Are we displaying outward or return?
                if (page.QueryStringContains(QueryStringKey.JourneyIdOutward))
                {
                    // Add the route information to the hidden field
                    SetupRoute(true);
                }
                else if (page.QueryStringContains(QueryStringKey.JourneyIdReturn))
                {
                    // Add the route information to the hidden field
                    SetupRoute(false);
                }
                else
                {
                    DisplayErrorAndHideMap();
                }
            }
            else
            {
                DisplayErrorAndHideMap();
            }
        }

        /// <summary>
        /// Setup the route details
        /// </summary>
        /// <param name="outward"></param>
        private void SetupRoute(bool outward)
        {
            JourneyResultHelper resultHelper = new JourneyResultHelper();
            JourneyHelper journeyHelper = new JourneyHelper();

            // Journey request/result
            ITDPJourneyRequest journeyRequest = resultHelper.JourneyRequest;
            ITDPJourneyResult journeyResult = resultHelper.CheckJourneyResultAvailability();

            // Journey to be shown
            Journey journey = null;

            TDPPage page = (TDPPage)Page;
            TXT_JourneyDirectionOutward = page.GetResource(RG, RC, "JourneyOutput.JourneyDirection.Outward.Text");
            TXT_JourneyDirectionReturn = page.GetResource(RG, RC, "JourneyOutput.JourneyDirection.Return.Text");
            TXT_JourneyHeaderOutward = page.GetResource(RG, RC, "JourneyOutput.JourneyHeader.Outward.Text");
            TXT_JourneyHeaderReturn = page.GetResource(RG, RC, "JourneyOutput.JourneyHeader.Return.Text");
            TXT_JourneyArriveBy = page.GetResource(RG, RC, "JourneyOutput.JourneyHeader.ArriveBy.Text");
            TXT_JourneyLeavingAt = page.GetResource(RG, RC, "JourneyOutput.JourneyHeader.LeavingAt.Text");
            TXT_Transport = page.GetResource(RG, RC, "JourneyOutput.HeaderRow.Transport.Text");
            TXT_Changes = page.GetResource(RG, RC, "JourneyOutput.HeaderRow.Changes.Text");
            TXT_Leave = page.GetResource(RG, RC, "JourneyOutput.HeaderRow.Leave.Text");
            TXT_Arrive = page.GetResource(RG, RC, "JourneyOutput.HeaderRow.Arrive.Text");
            TXT_JourneyTime = page.GetResource(RG, RC, "JourneyOutput.HeaderRow.JourneyTime.Text");
            TXT_Select = page.GetResource(RG, RC, "JourneyOutput.HeaderRow.Select.Text");

            if (journeyResult != null)
            {
                if (outward)
                {
                    if (journeyResult.OutwardJourneys.Count == 1)
                    {
                        journey = journeyResult.OutwardJourneys[0];
                    }
                }
                else
                {
                    if (journeyResult.ReturnJourneys.Count == 1)
                    {
                        journey = journeyResult.ReturnJourneys[0];
                    }
                }
            }

            if (journey == null)
            {
                DisplayErrorAndHideMap();
            }
            else
            {
                // Build and set journey coordinates
                MapHelper mapHelper = new MapHelper(Global.TDPResourceManager);

                // Get map points and add to the hidden journey map points field
                journeyPoints.Value = mapHelper.GetJourneyMapPoints(journey, ((TDPPage)Page).ImagePath, false);

                // Initialise the cycle leg details
                JourneyLeg theCycleLeg = null;
                foreach (JourneyLeg leg in journey.JourneyLegs)
                {
                    if (leg.Mode == TDPModeType.Cycle)
                    {
                        theCycleLeg = leg;
                        cycleLeg.Initialise(journeyRequest, leg);
                        break;
                    }
                }

                string origin = outward ? journeyRequest.Origin.DisplayName : journeyRequest.ReturnOrigin.DisplayName;
                string destination = outward ? journeyRequest.Destination.DisplayName : journeyRequest.ReturnDestination.DisplayName;
                DateTime journeyTime = outward ? journeyRequest.OutwardDateTime : journeyRequest.ReturnDateTime;
                string resource = outward ? TXT_JourneyHeaderOutward : TXT_JourneyHeaderReturn;
                string resourceDateTime = outward ? TXT_JourneyArriveBy : TXT_JourneyLeavingAt;

                journeyHeader.Text = string.Format(
                        resource,
                        origin, destination);
                journeyDateTime.Text = string.Format(resourceDateTime, journeyTime.ToString("dd/MM/yyyy HH:mm"));
                journeyDirection.Text = outward ? TXT_JourneyDirectionOutward : TXT_JourneyDirectionReturn;

                startLocation.Text = origin;
                endLocation.Text = destination;

                if ((outward && (journeyRequest.Origin is TDPVenueLocation)) ||
                    (!outward && (journeyRequest.ReturnOrigin is TDPVenueLocation)))
                {
                    startLocation.Text = startLocation.Text + " - " + theCycleLeg.LegStart.Location.DisplayName;
                }
                else
                {
                    endLocation.Text = endLocation.Text + " - " + theCycleLeg.LegEnd.Location.DisplayName;
                }
            }
        }

        /// <summary>
        /// Displays a message
        /// </summary>
        private void DisplayMessage(TDPMessage tdpMessage)
        {
            ((TDPWeb)this.Master).DisplayMessage(tdpMessage);
        }

        /// <summary>
        /// Bomb out
        /// </summary>
        private void DisplayErrorAndHideMap()
        {
            // Display error
            DisplayMessage(new TDPMessage("Map.Error.NoJourney", TDPMessageType.Error));

            // Hide the map area (this map opens in a separate window so no back button required)
            cycleJourney.Visible = false;
        }
    }
}