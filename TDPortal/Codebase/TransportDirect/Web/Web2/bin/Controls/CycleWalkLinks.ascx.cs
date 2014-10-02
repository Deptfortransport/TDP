using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.Web.Adapters;
using Logger = System.Diagnostics.Trace;
using TransportDirect.CommonWeb.Helpers;

namespace TransportDirect.UserPortal.Web.Controls
{

    public partial class CycleWalkLinks : System.Web.UI.UserControl
    {
        private string walkerImage = string.Empty;
        private string cyclerImage = string.Empty;
        private string walkitImage = string.Empty;
        private string cyclePlanImage = string.Empty;
        private string walkText = string.Empty;
        private string cycleText = string.Empty;
        private string walkCycleText = string.Empty;
        private string walkTextSingleMile = string.Empty;
        private string cycleTextSingleMile = string.Empty;
        private string walkCycleTextSingleMile = string.Empty;
        private string walkitLinkAltText = string.Empty;
        private string walkitLinkTooltip = string.Empty;
        private string cycleLinkAltText = string.Empty;
        private string cycleLinkTooltip = string.Empty;
        private string walkitLinkUrl = string.Empty;
        private string popupMessage = string.Empty;
        private string popupCancel = string.Empty;
        private string popupNext = string.Empty;
        private bool outward = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            SetupResources();
            SetControlVisibility();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            planCycle.Click += new ImageClickEventHandler(PlanCycle_Click);
        }

        public bool Outward
        {
            get { return outward; }
            set { outward = value; }
        }

        private void SetupResources()
        {
            TDPage page = (TDPage)Page;
            walkerImage = page.GetResource("CycleWalkLinks.WalkerImage");
            cyclerImage = page.GetResource("CycleWalkLinks.CyclerImage");
            walkitImage = page.GetResource("CycleWalkLinks.WalkitImage");
            cyclePlanImage = page.GetResource("CycleWalkLinks.CyclePlanImage");
            walkText = page.GetResource("CycleWalkLinks.WalkText");
            cycleText = page.GetResource("CycleWalkLinks.CycleText");
            walkCycleText = page.GetResource("CycleWalkLinks.WalkCycleText");
            walkitLinkAltText = page.GetResource("CycleWalkLinks.WalkitAltText");
            walkitLinkTooltip = page.GetResource("CycleWalkLinks.WalkitTooltip");
            cycleLinkAltText = page.GetResource("CycleWalkLinks.CycleAltText");
            cycleLinkTooltip = page.GetResource("CycleWalkLinks.CycleTooltip");
            walkTextSingleMile = page.GetResource("CycleWalkLinks.WalkTextSingleMile");
            cycleTextSingleMile = page.GetResource("CycleWalkLinks.CycleTextSingleMile");
            walkCycleTextSingleMile = page.GetResource("CycleWalkLinks.WalkCycleTextSingleMile");
            popupMessage = page.GetResource("CycleWalkLinks.ModalPopupWarning");
            popupCancel = page.GetResource("ModalPopupMessage.CancelButton.Text");
            popupNext = page.GetResource("ModalPopupMessage.OkButton.Text");
        }

        private void SetControlVisibility()
        {
            bool walkAlternative = false;
            bool cycleAlternative = false;
            decimal distance;
            int metresInAMile = 1609;

            // Only carry on if there are journey results,
            // and not an accessible journey request
            ITDSessionManager tdSessionManager = TDSessionManager.Current;
            if ( tdSessionManager.JourneyResult != null 
                &&  ((outward && (tdSessionManager.JourneyResult.OutwardPublicJourneyCount > 0 || tdSessionManager.JourneyResult.OutwardRoadJourneyCount > 0))
                     || (!outward && (tdSessionManager.JourneyResult.ReturnPublicJourneyCount > 0 || tdSessionManager.JourneyResult.ReturnRoadJourneyCount > 0)))
                && !IsAccessibleRequest(tdSessionManager)
                )
            {
                // Check we're door to door otherwise don't display
                if (tdSessionManager.FindAMode == FindAMode.None)
                {
                    walkAlternative = IsWalkAvailable(tdSessionManager);
                    cycleAlternative = IsCycleAvailable(tdSessionManager);
                }

                if (walkAlternative && cycleAlternative)
                {
                    distance = GetDistance(tdSessionManager);
                    string displayDistance = Math.Ceiling(distance / metresInAMile).ToString();

                    // Both available so show combined message and walk image
                    if (displayDistance == "1")
                    {
                        cycleWalkMessage.InnerText = string.Format(walkCycleTextSingleMile, displayDistance);
                    }
                    else
                    {
                        cycleWalkMessage.InnerText = string.Format(walkCycleText, displayDistance);
                    }
                    cycleWalkImage.ImageUrl = walkerImage;

                    // Show links
                    linkWalkit.Visible = true;
                    linkWalkit.ImageUrl = walkitImage;
                    linkWalkit.ToolTip = walkitLinkTooltip;
                    linkWalkit.Text = walkitLinkAltText;
                    linkWalkit.NavigateUrl = walkitLinkUrl;

                    planCycle.Visible = true;
                    planCycle.ImageUrl = cyclePlanImage;
                    planCycle.AlternateText = cycleLinkAltText;
                    planCycle.ToolTip = cycleLinkTooltip;

                    cyclewalkspacer.Visible = true;
                }
                else if (walkAlternative)
                {
                    distance = GetDistance(tdSessionManager);
                    string displayDistance = Math.Ceiling(distance / metresInAMile).ToString();

                    // Walk message only
                    if (displayDistance == "1")
                    {
                        cycleWalkMessage.InnerText = string.Format(walkTextSingleMile, displayDistance);
                    }
                    else
                    {
                        cycleWalkMessage.InnerText = string.Format(walkText, displayDistance);
                    }
                    cycleWalkImage.ImageUrl = walkerImage;

                    // Show links
                    linkWalkit.Visible = true;
                    linkWalkit.ImageUrl = walkitImage;
                    linkWalkit.ToolTip = walkitLinkTooltip;
                    linkWalkit.Text = walkitLinkAltText;
                    linkWalkit.NavigateUrl = walkitLinkUrl;
                }
                else if (cycleAlternative)
                {
                    distance = GetDistance(tdSessionManager);
                    string displayDistance = Math.Ceiling(distance / metresInAMile).ToString();

                    // Cycle message only
                    if (displayDistance == "1")
                    {
                        cycleWalkMessage.InnerText = string.Format(cycleTextSingleMile, displayDistance);
                    }
                    else
                    {
                        cycleWalkMessage.InnerText = string.Format(cycleText, displayDistance);
                    }
                    cycleWalkImage.ImageUrl = cyclerImage;

                    // Show links
                    planCycle.Visible = true;
                    planCycle.ImageUrl = cyclePlanImage;
                    planCycle.AlternateText = cycleLinkAltText;
                    planCycle.ToolTip = cycleLinkTooltip;
                }
                else
                {
                    // Hide this control
                    cyclewalklinksdiv.Visible = false;
                }

                if (cycleAlternative)
                {
                    modalPopup.TargetControl = planCycle;
                    modalPopup.OkButtonText = popupNext;
                    modalPopup.CancelButtonText = popupCancel;
                    modalPopup.PopupMessage = popupMessage;
                    modalPopup.PrinterFriendly = false;
                    modalPopup.Visible = true;
                }
            }
            else
            {
                // Hide this control
                cyclewalklinksdiv.Visible = false;
            }
        }

        private void PlanCycle_Click(object sender, EventArgs e)
        {
            // log click
            PageEntryEvent logPage = new PageEntryEvent(PageId.CycleLinkClicked, Session.SessionID, TDSessionManager.Current.Authenticated);
            Logger.Write(logPage);
            Server.Transfer(GenerateCycleURL(), false);
        }

        /// <summary>
        /// Generate a landing page url for the cycle journey
        /// </summary>
        private string GenerateCycleURL()
        {
            ITDSessionManager tdSessionManager = TDSessionManager.Current;

            // Construct the URL
            LandingPageHelper lpHelper = new LandingPageHelper();
            //StringBuilder targetUrl = new StringBuilder(lpHelper.GetLandingPageUrl(PageId.JPLandingPage));
            IPageController controller = (IPageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
            PageTransferDetails jpLandingPageTransferDetails = controller.GetPageTransferDetails(PageId.JPLandingPage);
            StringBuilder targetUrl = new StringBuilder("~/");
            targetUrl.Append(jpLandingPageTransferDetails.PageUrl);
            targetUrl.Append("?");

            // Now build up the parameters for the remainder of the URL
            string locationType = string.Empty;
            string locationText = string.Empty;
            string locationData = string.Empty;

            // Outward journey
            // Add origin data to the url
            lpHelper.GetLocationData(tdSessionManager.JourneyRequest.OriginLocation, ref locationType, ref locationData, ref locationText);

            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginType, locationType);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginText, locationText);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOriginData, locationData);

            // date, time, outwardarrivedepart
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOutwardDate, tdSessionManager.JourneyRequest.OutwardDateTime[0].ToString("ddMMyyyy"));
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOutwardTime, tdSessionManager.JourneyRequest.OutwardDateTime[0].ToString("HHmm"));
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterOutwardDepartArrive, tdSessionManager.JourneyRequest.OutwardArriveBefore ? LandingPageHelperConstants.ValueDateTypeArriveBy : LandingPageHelperConstants.ValueDateTypeDepartAt);

            // Destination location
            lpHelper.GetLocationData(tdSessionManager.JourneyRequest.DestinationLocation, ref locationType, ref locationData, ref locationText);

            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationType, locationType);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationText, locationText);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterDestinationData, locationData);

            // Return journey (if applicable)
            if (tdSessionManager.JourneyRequest.IsReturnRequired == true)
            {
                AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterReturnRequired, "true");

                // date, time, returnarrivedepart
                AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterReturnDate, tdSessionManager.JourneyRequest.ReturnDateTime[0].ToString("ddMMyyyy"));
                AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterReturnTime, tdSessionManager.JourneyRequest.ReturnDateTime[0].ToString("HHmm"));
                AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterReturnDepartArrive, tdSessionManager.JourneyRequest.ReturnArriveBefore ? LandingPageHelperConstants.ValueDateTypeArriveBy : LandingPageHelperConstants.ValueDateTypeDepartAt);
            }

            // Miscellaneous other parameters
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterMode, LandingPageHelperConstants.ValueModeCycle);
            AddParameterToUrl(targetUrl, LandingPageHelperConstants.ParameterAutoPlan, LandingPageHelperConstants.ValueAutoPlanOn, true);

            return targetUrl.ToString();
        }

        /// <summary>
        /// Adds a querystring parameter to the URL contained in the string builder.
        /// </summary>
        /// <param name="url">StringBuilder containing the Url. This should currently end in either ? or &</param>
        /// <param name="parameter">The name of the parameter to add.</param>
        /// <param name="value">The value of the parameter to add.</param>
        private void AddParameterToUrl(StringBuilder url, string parameter, string value)
        {
            //Assume that this isn't the final parameter
            AddParameterToUrl(url, parameter, value, false);
        }

        /// <summary>
        /// Adds a querystring parameter to the URL contained in the string builder.
        /// </summary>
        /// <param name="url">StringBuilder containing the Url. This should currently end in either ? or &</param>
        /// <param name="parameter">The name of the parameter to add.</param>
        /// <param name="value">The value of the parameter to add.</param>
        /// <param name="isFinalParameter">Denotes whether is final parameter - if so no trailing amprasand is required</param>
        private void AddParameterToUrl(StringBuilder url, string parameter, string value, bool isFinalParameter)
        {
            url.Append(parameter);
            url.Append("=");
            string s = HttpUtility.UrlEncode(value);
            url.Append(s);

            if (!isFinalParameter)
            {
                url.Append("&");
            }
        }

        private decimal GetDistance(ITDSessionManager tdSessionManager)
        {
            long east = tdSessionManager.JourneyRequest.OriginLocation.GridReference.Easting - tdSessionManager.JourneyRequest.DestinationLocation.GridReference.Easting;
            long north = tdSessionManager.JourneyRequest.OriginLocation.GridReference.Northing - tdSessionManager.JourneyRequest.DestinationLocation.GridReference.Northing;
            decimal distance = decimal.Parse(Math.Sqrt((east * east) + (north * north)).ToString());
            return distance;
        }

        private bool IsWalkAvailable(ITDSessionManager tdSessionManager)
        {
            // Check for origin / destination being an exchange group - assume if there are multiple 
            // Naptans then it's a group
            if (tdSessionManager.JourneyRequest.OriginLocation.NaPTANs.Length > 1)
            {
                return false;
            }
            else if (tdSessionManager.JourneyRequest.DestinationLocation.NaPTANs.Length > 1)
            {
                return false;
            }

            // Check the result is not a single walk leg
            if (outward)
            {
                if (tdSessionManager.JourneyResult.OutwardPublicJourneyCount > 0)
                {
                    foreach (TransportDirect.UserPortal.JourneyControl.PublicJourney journey in tdSessionManager.JourneyResult.OutwardPublicJourneys)
                    {
                        if (journey.JourneyLegs.Length == 1 && journey.JourneyLegs[0].Mode == ModeType.Walk)
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (tdSessionManager.JourneyResult.ReturnPublicJourneyCount > 0)
                {
                    foreach (TransportDirect.UserPortal.JourneyControl.PublicJourney journey in tdSessionManager.JourneyResult.ReturnPublicJourneys)
                    {
                        if (journey.JourneyLegs.Length == 1 && journey.JourneyLegs[0].Mode == ModeType.Walk)
                        {
                            return false;
                        }
                    }
                }
            }

            // Check distance is within limit
            IPropertyProvider properties = Properties.Current;
            int maxDistance = int.Parse(properties["CycleWalkLinks.MaxWalkDistance"]);
            decimal distance = GetDistance(tdSessionManager);

            if (distance > maxDistance)
            {
                return false;
            }

            // Check origin & destination are within the same walkit data area
            string theUrl = string.Empty;
            if (outward)
            {
                WalkitURLHandoffHelper helper = new WalkitURLHandoffHelper(tdSessionManager.JourneyRequest.OriginLocation, tdSessionManager.JourneyRequest.DestinationLocation);
                theUrl = helper.GetWalkitHandoffURL();
            }
            else
            {
                WalkitURLHandoffHelper helper = new WalkitURLHandoffHelper(tdSessionManager.JourneyRequest.DestinationLocation, tdSessionManager.JourneyRequest.OriginLocation);
                theUrl = helper.GetWalkitHandoffURL();
            }

            if (theUrl == string.Empty)
            {
                return false;
            }

            // Success
            walkitLinkUrl = theUrl;
            return true;
        }

        private bool IsCycleAvailable(ITDSessionManager tdSessionManager)
        {
            // Check if this has been done already
            if (!tdSessionManager.JourneyResult.CycleAlternativeCheckDone)
            {
                // Check for origin / destination being an exchange group - assume if there are multiple 
                // Naptans then it's a group
                if (tdSessionManager.JourneyRequest.OriginLocation.NaPTANs.Length > 1)
                {
                    tdSessionManager.JourneyResult.CycleAlternativeCheckDone = true;
                    tdSessionManager.JourneyResult.CycleAlternativeAvailable = false;
                    return false;
                }
                else if (tdSessionManager.JourneyRequest.DestinationLocation.NaPTANs.Length > 1)
                {
                    tdSessionManager.JourneyResult.CycleAlternativeCheckDone = true;
                    tdSessionManager.JourneyResult.CycleAlternativeAvailable = false;
                    return false;
                }

                // Check distance is within limit
                IPropertyProvider properties = Properties.Current;
                int maxDistance = int.Parse(properties["CycleWalkLinks.MaxCycleDistance"]);
                decimal distance = GetDistance(tdSessionManager);

                if (distance > maxDistance)
                {
                    tdSessionManager.JourneyResult.CycleAlternativeCheckDone = true;
                    tdSessionManager.JourneyResult.CycleAlternativeAvailable = false;
                    return false;
                }

                // Check origin & destination are within the same cycle data area
                #region Valiate Points in Cycle Data areas
                bool validatePoints = bool.Parse(Properties.Current["CyclePlanner.Planner.PointValidation.Switch"]);
                bool planInSameCycleArea = bool.Parse(Properties.Current["CyclePlanner.Planner.PointValidation.PlanSameAreaOnly"]);

                if (validatePoints)
                {
                    // Get all the points
                    ArrayList pointsArray = new ArrayList();
                    pointsArray.Add(tdSessionManager.JourneyRequest.OriginLocation.OSGRAsPoint());
                    pointsArray.Add(tdSessionManager.JourneyRequest.DestinationLocation.OSGRAsPoint());

                    Point[] points = (Point[])pointsArray.ToArray(typeof(Point));

                    // First check: make sure the Points are in valid cycle data areas
                    IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

                    if (planInSameCycleArea)
                    {
                        if (!gisQuery.IsPointsInCycleDataArea(points, true))
                        {
                            tdSessionManager.JourneyResult.CycleAlternativeCheckDone = true;
                            tdSessionManager.JourneyResult.CycleAlternativeAvailable = false;
                            return false;
                        }
                    }
                    else
                    {
                        if (!gisQuery.IsPointsInCycleDataArea(points, false))
                        {
                            tdSessionManager.JourneyResult.CycleAlternativeCheckDone = true;
                            tdSessionManager.JourneyResult.CycleAlternativeAvailable = false;
                            return false;
                        }
                    }
                }
                #endregion
            }
            else
            {
                return tdSessionManager.JourneyResult.CycleAlternativeAvailable;
            }

            tdSessionManager.JourneyResult.CycleAlternativeCheckDone = true;
            tdSessionManager.JourneyResult.CycleAlternativeAvailable = true;
            return true;
        }

        /// <summary>
        /// Returns true if the TDJourneyRequest in session is for an accessible journey request
        /// </summary>
        /// <param name="tdSessionManager"></param>
        /// <returns></returns>
        private bool IsAccessibleRequest(ITDSessionManager tdSessionManager)
        {
            return (tdSessionManager.JourneyRequest != null
                && tdSessionManager.JourneyRequest.AccessiblePreferences != null
                && tdSessionManager.JourneyRequest.AccessiblePreferences.Accessible);
        }
    }
}