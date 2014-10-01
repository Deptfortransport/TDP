// *********************************************** 
// NAME             : DetailsLegControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Feb 2012
// DESCRIPTION  	: Displays a high level view of the leg mode (e.g. walk, ferry, etc)
// ************************************************
// 

using System;
using System.Collections.Generic;
using TDP.Common;
using TDP.Common.Extenders;
using TDP.Common.LocationService;
using TDP.Common.PropertyManager;
using TDP.Common.ResourceManager;
using TDP.Common.Web;
using TDP.UserPortal.JourneyControl;
using TDP.UserPortal.Retail;
using LS = TDP.Common.LocationService;

namespace TDP.UserPortal.TDPMobile.Controls
{
    /// <summary>
    /// Displays a high level view of the leg mode (e.g. walk, ferry, etc)
    /// </summary>
    public partial class DetailsLegControl : System.Web.UI.UserControl
    {
        #region Private Fields

        /// <summary>
        /// Leg control adapter to load controls and labels common to both Web and Mobile
        /// </summary>
        protected LegControlAdapter LCA = null;

        #region Resources

        // Resource manager
        private static string RG = TDPResourceManager.GROUP_JOURNEYOUTPUT;
        private static string RC = TDPResourceManager.COLLECTION_JOURNEY;
        private TDPResourceManager RM = Global.TDPResourceManager;

        private string URL_OpenInNewWindowImage = string.Empty;
        private string URL_OpenInNewWindowImage_Blue = string.Empty;
        private string ALTTXT_OpenInNewWindow = string.Empty;
        private string TOOLTIP_OpenInNewWindow = string.Empty;

        private string TXT_BookTicketPhone = string.Empty;
        private string TXT_BookTicketPhoneCoachAndRail = string.Empty;
        private string TXT_TravelcardLink = string.Empty;
        private string TXT_DirectionsMapLink = string.Empty;
        private string TXT_DirectionsMapLinkToolTip = string.Empty;
        private string TXT_DirectionsMapLink_Walk = string.Empty;
        private string TXT_DirectionsMapLinkToolTip_Walk = string.Empty;

        private string URL_TravelcardLink = string.Empty;

        #endregion
        
        private ITDPJourneyRequest journeyRequest = null;

        private JourneyLeg previousJourneyLeg;
        private JourneyLeg currentJourneyLeg;
        private JourneyLeg nextJourneyLeg;

        // Retailers which apply to the current leg
        private List<Retailer> retailers = null;
        private bool isCoachAndRailRetailer = false; // Used for showing the "Book combined" text
        private bool isTravelcardRetailer = false; // Used to show leg "covered by Games Travelcard" text

        // Used in updating image dimensions
        private bool vehicleFeaturesAvailable = false;
        private bool accessibleFeaturesLocationAvailable = false;
        private bool accessibleFeaturesLocationEndAvailable = false;
        private bool accessibleFeaturesAvailable = false;
        private bool bookTicketAvailable = false;
        private bool bookTicketTravelcardAvailable = false;
        private bool gpxLinkAvailable = false;
        private bool locationLinkAvailable = false;
        private bool endLocationLinkAvailable = false;

        private bool intermediateStopsAvailable = false;
                
        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitialiseText();

            InitialiseImageResource();

            SetupLeg();
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Set visibilities
            legLocation.Visible = !locationLinkAvailable;
            legLocationLink.Visible = locationLinkAvailable;

            endLocation.Visible = !endLocationLinkAvailable;
            endLocationLink.Visible = endLocationLinkAvailable;

            legModeImgDiv.Visible = legMode.Visible;

            vehicleFeaturesDiv.Visible = (vehicleFeaturesAvailable || accessibleFeaturesAvailable);

            travelNotesLinkDiv.Visible = ((LCA.DisplayNotes != null) && (LCA.DisplayNotes.Count > 0));

            bookTicketDiv.Visible = bookTicketTravelcardAvailable;

            callingPointsRow.Visible = intermediateStopsAvailable;

            locationMapLinkDiv.Visible = (!string.IsNullOrEmpty(locationMapLink.NavigateUrl));
            endLocationMapLinkDiv.Visible = (!string.IsNullOrEmpty(endLocationMapLink.NavigateUrl)); 
            directionsMapLinkDiv.Visible = (!string.IsNullOrEmpty(directionsMapLink.Text));

            pnlMapLocationRow.Visible = (!string.IsNullOrEmpty(locationMapLink.NavigateUrl));
            pnlMapEndLocationRow.Visible = (!string.IsNullOrEmpty(endLocationMapLink.NavigateUrl));
        }

        #endregion
        
        #region Public Methods

        /// <summary>
        /// Initialise
        /// </summary>
        /// <param name="journeyLegDetail"></param>
        public void Initialise(ITDPJourneyRequest journeyRequest, JourneyLeg previousJourneyLeg, JourneyLeg currentJourneyLeg, JourneyLeg nextJourneyLeg,
            List<Retailer> retailers, bool isCoachAndRailRetailer, bool isTravelcardRetailer,
            bool showAccessibleFeatures, bool showAccessibleInfo,
            bool isAccessibleFriendly)
        {
            this.journeyRequest = journeyRequest;
            this.previousJourneyLeg = previousJourneyLeg;
            this.currentJourneyLeg = currentJourneyLeg;
            this.nextJourneyLeg = nextJourneyLeg;

            // Values specific to this control
            this.retailers = retailers;
            this.isCoachAndRailRetailer = isCoachAndRailRetailer;
            this.isTravelcardRetailer = isTravelcardRetailer;

            bool accessibleJourney = false;
            if ((journeyRequest.AccessiblePreferences != null) && (journeyRequest.AccessiblePreferences.RequireSpecialAssistance ||
                                                                    journeyRequest.AccessiblePreferences.RequireStepFreeAccess))
            {
                accessibleJourney = true;
            }

            TDPPageMobile page = (TDPPageMobile)Page;

            LCA = new LegControlAdapter(journeyRequest,
                previousJourneyLeg, currentJourneyLeg, nextJourneyLeg,
                showAccessibleFeatures, showAccessibleInfo,
                false, isAccessibleFriendly, false, true,
                Global.TDPResourceManager, page.ImagePath, accessibleJourney);
        }

        /// <summary>
        /// Refresh method to re-populate the controls, e.g. if initialise called after Page_Load
        /// </summary>
        public void Refresh()
        {
            SetupLeg();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets up all the leg details
        /// </summary>
        private void SetupLeg()
        {
            TDPPageMobile page = (TDPPageMobile)Page;

            if (currentJourneyLeg != null)
            {
                // Setup this control using the LegControlAdapter
                LCA.SetupLegInstruction(legLocation, legInstruction, legInstructionTime, legModeDetail, legArrive, legArriveTime, legDepart, legDepartTime);
                LCA.SetupLegMode(legMode, legModeImgDiv, LCA.CurrentMode);
                LCA.SetupCheckConstraint(legMode, legModeDetail, legInstruction);
                LCA.SetupLegAccessibleInterchange(legMode, legInstruction);
                LCA.SetupLegTelecabine(legMode);
                LCA.SetupDisplayNotes(divNotes, travelNotesLink, null, travelNotesLinkSymbol);
                LCA.SetupLegLineImages(legNodeImage, legLineImage1, legLineImage2, legLocation, null, false);
                LCA.SetupVenueMapLink(locationMapLink, endLocationMapLink);
                LCA.SetupInterchangeLeg(pnlInterchangeRow, interchangeNodeImage, interchangeLocation,
                    interchangeArrive, interchangeArriveTime);
                LCA.SetupEndLocationLeg(pnlEndLocationRow, endLocation, endArrive, endArriveTime,
                    null, endNodeImage, ResolveClientUrl(URL_OpenInNewWindowImage_Blue));

                // Setup elements directly in this control
                SetupLegVehicleFeatures();
                SetupLegAccessibleFeatures();
                SetupBookTravelLink();
                SetupDirectionsMapLink();
                SetupLegCallingPoints();
                SetupNodeStyle();
                SetupLegLocationLink();

                // Adjust image dimensions where required
                LCA.UpdateImageDimensions(legNodeImage, legLineImage1, legLineImage2, legInstruction, 
                    bookTicketAvailable, bookTicketTravelcardAvailable,
                    vehicleFeaturesAvailable, accessibleFeaturesAvailable, gpxLinkAvailable, false);
            }

        }

        /// <summary>
        /// Method which initialises the text string used by this control
        /// </summary>
        private void InitialiseText()
        {
            TDPPageMobile page = (TDPPageMobile)Page;

            Language language = CurrentLanguage.Value;

            TXT_BookTicketPhone = RM.GetString(language, RG, RC, "JourneyOutput.Text.BookTicketPhone");
            TXT_BookTicketPhoneCoachAndRail = RM.GetString(language, RG, RC, "JourneyOutput.Text.BookTicketPhone.CoachAndRail");
            TXT_TravelcardLink = RM.GetString(language, RG, RC, "JourneyOutput.Text.TravelcardLink");
            TXT_DirectionsMapLink = RM.GetString(language, RG, RC, "JourneyOutput.Text.DirectionsMapLink.Cycle");
            TXT_DirectionsMapLinkToolTip = RM.GetString(language, RG, RC, "JourneyOutput.Text.DirectionsMapLinkToolTip.Cycle");
            TXT_DirectionsMapLink_Walk = RM.GetString(language, RG, RC, "JourneyOutput.Text.DirectionsMapLink.Walk");
            TXT_DirectionsMapLinkToolTip_Walk = RM.GetString(language, RG, RC, "JourneyOutput.Text.DirectionsMapLinkToolTip.Walk");

            URL_TravelcardLink = RM.GetString(language, RG, RC, "JourneyOutput.Url.TravelcardLink.Mobile");
        }

        /// <summary>
        /// Method which initialises the resource strings used by the image controls
        /// </summary>
        private void InitialiseImageResource()
        {
            TDPPageMobile page = (TDPPageMobile)Page;

            Language language = CurrentLanguage.Value;
                        
            URL_OpenInNewWindowImage = page.ImagePath + Global.TDPResourceManager.GetString(language, "OpenInNewWindow.URL");
            URL_OpenInNewWindowImage_Blue = page.ImagePath + Global.TDPResourceManager.GetString(language, "OpenInNewWindow.Blue.URL");
            ALTTXT_OpenInNewWindow = Global.TDPResourceManager.GetString(language, "OpenInNewWindow.AlternateText");
            TOOLTIP_OpenInNewWindow = Global.TDPResourceManager.GetString(language, "OpenInNewWindow.Text");
        }

        /// <summary>
        /// Sets up the vehicle features for the journey leg, only if it contains a PublicJourneyDetail
        /// </summary>
        private void SetupLegVehicleFeatures()
        {
            // Vehicle Features
            PublicJourneyDetail pjd = LCA.GetPublicJourneyDetail();

            if (pjd != null)
            {
                legVehicleFeatures.Initialise(pjd.VehicleFeatures);

                vehicleFeaturesAvailable = (pjd.VehicleFeatures.Count > 0);
            }

            // Display if features found
            legVehicleFeatures.Visible = vehicleFeaturesAvailable;
        }

        /// <summary>
        /// Sets up the accessible features for the journey leg, only if it contains a PublicJourneyDetail
        /// </summary>
        private void SetupLegAccessibleFeatures()
        {
            // Accessible Features
            if (LCA.ShowAccessibleFeatures)
            {
                PublicJourneyDetail pjd = LCA.GetPublicJourneyDetail();

                if (pjd != null)
                {
                    List<string> suppressForNaPTANsPrefix = new List<string>();

                    string suppressForNaPTANPrefix = Properties.Current["JourneyDetails.Accessibility.Icons.SuppressForNaPTANs"];

                    if (!string.IsNullOrEmpty(suppressForNaPTANPrefix))
                    {
                        suppressForNaPTANsPrefix.AddRange(suppressForNaPTANPrefix.ToUpper().Split(','));
                    }

                    #region Board location

                    if (LCA.ShowAccessibleForNaptan(LCA.GetJourneyLegNaPTAN(true), suppressForNaPTANsPrefix))
                    {
                        // Accessibility features for location (board)
                        legLocationAccessibleFeatures.Initialise(pjd.BoardAccessibility);
                        accessibleFeaturesLocationAvailable = (pjd.BoardAccessibility.Count > 0);
                    }

                    #endregion

                    #region Leg/Service

                    // Accessibility features for leg/service
                    List<TDPAccessibilityType> accessibilityFeatures = new List<TDPAccessibilityType>();


                    if (pjd is PublicJourneyInterchangeDetail)
                    {
                        // Interchange detail can contain additional accessibility details
                        PublicJourneyInterchangeDetail pjid = (PublicJourneyInterchangeDetail)pjd;

                        accessibilityFeatures.AddRange(pjid.InterchangeLegAccessibility);
                    }

                    accessibilityFeatures.AddRange(pjd.ServiceAccessibility);

                    // Filter out any accessible icons that shouldn't be shown
                    accessibilityFeatures = LCA.FilterAccessibilityTypes(accessibilityFeatures);

                    legAccessibleFeatures.Initialise(accessibilityFeatures);
                    accessibleFeaturesAvailable = (accessibilityFeatures.Count > 0);

                    #endregion

                    #region Alight location

                    if (LCA.LastLeg)
                    {
                        if (LCA.ShowAccessibleForNaptan(LCA.GetJourneyLegNaPTAN(false), suppressForNaPTANsPrefix))
                        {
                            // Accessibility features for location (alight)
                            endLocationAccessibleFeatures.Initialise(pjd.AlightAccessibility);
                            accessibleFeaturesLocationEndAvailable = (pjd.AlightAccessibility.Count > 0);
                        }
                    }

                    #endregion

                    // Accessible instructions
                    accessibleInstruction.Initialise(pjd, journeyRequest.AccessiblePreferences.RequireSpecialAssistance, journeyRequest.AccessiblePreferences.RequireStepFreeAccess, currentJourneyLeg.StartTime);
                }
            }

            // Display if features found
            legLocationAccessibleFeatures.Visible = accessibleFeaturesLocationAvailable;
            legAccessibleFeatures.Visible = accessibleFeaturesAvailable;
            endLocationAccessibleFeatures.Visible = accessibleFeaturesLocationEndAvailable;
        }

        /// <summary>
        /// Sets up travel retailer information
        /// </summary>
        private void SetupBookTravelLink()
        {
            string handoffUrl = string.Empty;

            RetailerHelper retailerHelper = new RetailerHelper();

            bookTicketPhoneText.Visible = false;
            travelcardLink.Visible = false;
            
            if (isTravelcardRetailer)
            {
                travelcardLink.NavigateUrl = URL_TravelcardLink;
                travelcardLink.Visible = true;

                travelcardLinkText.Text = TXT_TravelcardLink;
                travelcardLinkText.ToolTip = travelcardLinkText.Text;

                // Set flag for adjusting line image
                bookTicketTravelcardAvailable = true;
            }
            else if (retailers.Count > 0)
            {
                // If travelcard is available and retailer available 
                // and is for Rail or Coach, then don't display the booking link
                if (!(isTravelcardRetailer &&
                    (currentJourneyLeg.Mode == TDPModeType.Rail || currentJourneyLeg.Mode == TDPModeType.Coach)))
                {
                    // Get retailer phone number(s)
                    string phoneNumber = string.Empty;

                    // Phone number link to allow devices native phone dialler to be shown (if it supports)
                    string phoneNumberLink = "<a href=\"tel:{0}\">{1}</a>";

                    foreach (Retailer retailer in retailers)
                    {
                        if (!string.IsNullOrEmpty(phoneNumber))
                        {
                            phoneNumber += "; ";
                        }

                        phoneNumber += string.Format(phoneNumberLink, retailer.PhoneNumber, retailer.PhoneNumberDisplay);
                    }

                    if (!string.IsNullOrEmpty(phoneNumber))
                    {
                        if (isCoachAndRailRetailer)
                        {
                            bookTicketPhoneText.Text = string.Format(TXT_BookTicketPhoneCoachAndRail, phoneNumber);
                        }
                        else
                        {
                            bookTicketPhoneText.Text = string.Format(TXT_BookTicketPhone, currentJourneyLeg.Mode, phoneNumber);
                        }

                        bookTicketPhoneText.Visible = true;

                        // Set flag for adjusting line image
                        bookTicketTravelcardAvailable = true;
                    }
                }
            }
        }

        /// <summary>
        /// Sets up the link for going to the maps page to show the journey directions on a map
        /// </summary>
        private void SetupDirectionsMapLink()
        {
            // Only show the map button for cycle journeys
            if (LCA.CurrentMode == TDPModeType.Cycle)
            {
                if (Properties.Current["Map.Journey.Cycle.Enabled.Switch"].Parse(true))
                {
                    directionsMapLink.Text = TXT_DirectionsMapLink;
                    directionsMapLink.ToolTip = TXT_DirectionsMapLinkToolTip;
                }
            }
            // Show the map link for walk leg 
            else if (LCA.CurrentMode == TDPModeType.Walk)
            {
                #region Set up map button for walk leg

                if (Properties.Current["Map.Journey.Walk.Enabled.Switch"].Parse(true))
                {
                    // Display only for first or last walk leg, but NOT for 
                    // - a walk only journey
                    // - leg starts at a naptan (first leg)
                    // - leg ends at a naptan (last leg)
                    if (((journeyRequest.Origin is TDPVenueLocation) && LCA.LastLeg && !(LCA.FirstLeg && LCA.LastLeg))
                        || ((journeyRequest.Destination is TDPVenueLocation) && LCA.FirstLeg && !(LCA.FirstLeg && LCA.LastLeg)))
                    {
                        if (((journeyRequest.Destination is TDPVenueLocation) && currentJourneyLeg.LegStart.Location.Naptan.Count == 0)
                            || ((journeyRequest.Origin is TDPVenueLocation) && currentJourneyLeg.LegEnd.Location.Naptan.Count == 0))
                        {
                            directionsMapLink.Text = TXT_DirectionsMapLink_Walk;
                            directionsMapLink.ToolTip = TXT_DirectionsMapLinkToolTip_Walk;
                        }
                    }
                }

                #endregion
            }
        }

        /// <summary>
        /// Sets up the leg intermediate calling points
        /// </summary>
        private void SetupLegCallingPoints()
        {
            PublicJourneyDetail pjd = LCA.GetPublicJourneyDetail();

            if (pjd != null && pjd.IntermediatesLeg.Count > 0)
            {
                legCallingPoints.Initialise(pjd.GetFilteredIntermediatesLeg());

                intermediateStopsAvailable = legCallingPoints.HasCallingPoints;
            }
        }

        /// <summary>
        /// Sets up the styling specific for the Leg location node column
        /// </summary>
        private void SetupNodeStyle()
        {
            if (LCA.FirstLeg)
            {
                columnNode.Attributes["class"] = string.Format("{0} columnNodeStart", columnNode.Attributes["class"]);
            }
            else
            {
                columnNode.Attributes["class"] = string.Format("{0} columnNodeMiddle", columnNode.Attributes["class"]);
            }
            // Last leg end location node is shown on the end location row so doesnt need dynamically added style
        }

        /// <summary>
        /// Sets up the leg location hyperlink if stop information is available for it
        /// </summary>
        private void SetupLegLocationLink()
        {
            StopInformationHelper stopInformationHelper = new StopInformationHelper();
            
            if (stopInformationHelper.StopInformationAvailable())
            {
                // Stop info is currently only for rail stops
                string railPrefix = Properties.Current[LS.Keys.NaptanPrefix_Rail];

                TDPPageMobile page = (TDPPageMobile)Page;
                string stopInfoURL = null;

                string naptan = LCA.GetJourneyLegNaPTAN(true);

                if (!string.IsNullOrEmpty(naptan) && naptan.StartsWith(railPrefix))
                {
                    stopInfoURL = stopInformationHelper.BuildStopInformationUrl(page, currentJourneyLeg.LegStart.Location, StopInformationMode.StationBoardDeparture);

                    if (!string.IsNullOrEmpty(stopInfoURL))
                    {
                        // Replace the legLocation control with the legLocationLinnk
                        legLocationLink.Text = legLocation.Text;
                        legLocationLink.ToolTip = legLocation.Text;
                        legLocationLink.NavigateUrl = stopInfoURL;

                        locationLinkAvailable = true;
                    }
                }

                if (LCA.LastLeg)
                {
                    naptan = LCA.GetJourneyLegNaPTAN(false);

                    if (!string.IsNullOrEmpty(naptan) && naptan.StartsWith(railPrefix))
                    {
                        stopInfoURL = stopInformationHelper.BuildStopInformationUrl(page, currentJourneyLeg.LegEnd.Location, StopInformationMode.StationBoardDeparture);

                        if (!string.IsNullOrEmpty(stopInfoURL))
                        {
                            // Replace the endLocation control with the endLocationLinnk
                            endLocationLink.Text = endLocation.Text;
                            endLocationLink.ToolTip = endLocation.Text;
                            endLocationLink.NavigateUrl = stopInfoURL;

                            endLocationLinkAvailable = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Directions map link click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void directionsMapLink_Click(object sender, EventArgs e)
        {
            TDPPageMobile page = (TDPPageMobile)Page;
            
            page.SetPageTransfer(PageId.MobileMap);
            page.AddQueryStringForPage(PageId.MobileMapJourney);

            if (LCA.CurrentMode == TDPModeType.Walk)
            {
                page.AddQueryString(QueryStringKey.ShowWalk, "1");
            }
        }

        #endregion
    }
}
