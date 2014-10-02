using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using TransportDirect.UserPortal.Web.Templates;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common;

using Logger = System.Diagnostics.Trace;
using TDPublicJourney = TransportDirect.UserPortal.JourneyControl.PublicJourney;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using System.Globalization;
using System.Xml;
using TransportDirect.UserPortal.PricingRetail.RetailXmlHandoff;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.Resource;

namespace TransportDirect.UserPortal.Web.JourneyPlanning
{
    public partial class TicketRetailersHandOffFinal : TDPage
    {
        #region Controls and private members

        private string strMode = string.Empty;
        private TransportDirect.UserPortal.PricingRetail.Domain.Discounts objDiscounts;

        /// <summary>
        /// The XML to be passed to the retailer
        /// </summary>
        private string handOffXml = string.Empty;

        /// <summary>
        /// The retailer that was selected from the ticket retailers page
        /// </summary>
        private Retailer selectedRetailer;


        /// <summary>
        /// The retail unit that was selected from the ticket retailers page
        /// </summary>
        private RetailUnit selectedRetailUnit;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor. Sets PageId and LocalResourceManager
        /// </summary>
        public TicketRetailersHandOffFinal()
            : base()
        {
            pageId = PageId.TicketRetailersHandOffFinal;

            LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The XML to be passed to the retailer suitable for embedding in an HTML document
        /// (i.e. encoded as HTML text)
        /// </summary>
        public string EncodedRetailerXml
        {
            get { return HttpUtility.HtmlEncode(handOffXml); }
        }

        /// <summary>
        /// The handoff URL of the retailer
        /// </summary>
        public string RetailerUrl
        {
            get { return selectedRetailer.HandoffUrl; }
        }

        #endregion Properties

        #region page events
        protected void Page_Load(object sender, EventArgs e)
        {
            getRetailerDetails();
            generateXml();
            logEvent();
            RemotePost myremotepost = new RemotePost();
            myremotepost.Url = RetailerUrl;
            myremotepost.JSDisabledText = GetResource("TicketRetailersHandOffFinal.JavascriptDisabledText");
            myremotepost.Add("RetailXML", EncodedRetailerXml);
            myremotepost.Post();

            Response.End();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Generates the retailer XML document based on session data. This document is
        /// available through the EncodedRetailerXml property.
        /// </summary>
        private void generateXml()
        {
            // Get the pricing retail state from Session Manager
            PricingRetailOptionsState pricingRetailState = TDItineraryManager.Current.PricingRetailOptions;


            #region Get the specific legs of the journey to hand off.

            PublicJourneyDetail[] outwardLegs;
            PublicJourneyDetail[] inwardLegs = null;

            if (selectedRetailUnit.Tickets.Length > 1)
            {
                TDPublicJourney outwardJourney = pricingRetailState.JourneyItinerary.OutwardJourney as TDPublicJourney;
                TDPublicJourney inwardJourney = pricingRetailState.JourneyItinerary.ReturnJourney as TDPublicJourney;

                // Hand off all the legs
                outwardLegs = outwardJourney.Details;
                if (inwardJourney != null)
                    inwardLegs = inwardJourney.Details;
            }
            else
            {
                // Hand off the legs from the specific ticket
                outwardLegs = selectedRetailUnit.Tickets[0].OutboundLegs;
                inwardLegs = selectedRetailUnit.Tickets[0].InboundLegs; // this may be null
            }

            #endregion

            #region Get user options

            ITDJourneyRequest journeyRequest = TDItineraryManager.Current.JourneyRequest;
            int interchangeSpeed = journeyRequest.InterchangeSpeed;
            bool noChanges = journeyRequest.PublicAlgorithm == PublicAlgorithmType.NoChanges;
            TDLocation publicVia = null;
            if ((journeyRequest.PublicViaLocations != null) && (journeyRequest.PublicViaLocations.Length > 0))
                publicVia = journeyRequest.PublicViaLocations[0];
            ItineraryType itineraryType;

            if (TicketRetailersHelper.IsCostBasedSearch)
                itineraryType = pricingRetailState.OverrideItineraryType;
            else
                itineraryType = pricingRetailState.JourneyItinerary.Type;

            #endregion

            #region Other parameters


            #endregion

            objDiscounts = new TransportDirect.UserPortal.PricingRetail.Domain.Discounts();

            //If there is a rail and coach discount card declared then work out which mode the retail handoff is for
            //and only add the appropriate discount card to the XML.
            if (pricingRetailState.Discounts.RailDiscount != null && pricingRetailState.Discounts.CoachDiscount != null)
            {

                strMode = outwardLegs[0].Mode.ToString();

                if (outwardLegs.Length > 1)
                {
                    for (int i = 1; i < outwardLegs.Length; i++)
                    {
                        if (outwardLegs[i].Mode.ToString() != strMode)
                        {
                            strMode = "both";
                        }
                    }
                }

                if (inwardLegs.Length > 0 && strMode != "both")
                {
                    for (int i = 0; i < inwardLegs.Length; i++)
                    {
                        if (inwardLegs[i].Mode.ToString() != strMode)
                        {
                            strMode = "both";
                        }
                    }
                }

                if (strMode == "Coach")
                {
                    objDiscounts.CoachDiscount = pricingRetailState.Discounts.CoachDiscount;
                }
                else if (strMode == "Rail")
                {
                    objDiscounts.RailDiscount = pricingRetailState.Discounts.RailDiscount;
                }
                else
                {
                    objDiscounts = pricingRetailState.Discounts;
                }
            }
            else
            {
                objDiscounts = pricingRetailState.Discounts;
            }

            // Generate the hand-off XML
            XmlDocument handoffXmlDoc = (XmlDocument)RetailXmlDocument.GenerateXml(
                    outwardLegs,
                    inwardLegs,
                    itineraryType,
                    pricingRetailState.OverrideItineraryType,
                    pricingRetailState.LastRetailerSelectionIsForReturn,
                    interchangeSpeed,
                    noChanges,
                    objDiscounts,
                    publicVia,
                    pricingRetailState.AdultPassengers,
                    pricingRetailState.ChildPassengers);
            handOffXml = handoffXmlDoc.OuterXml.ToString(CultureInfo.InvariantCulture);

        }

        /// <summary>
        /// Gets details of retailer selected on ticket retailers page
        /// </summary>
        private void getRetailerDetails()
        {
            PricingRetailOptionsState options = TDItineraryManager.Current.PricingRetailOptions;

            selectedRetailer = options.LastRetailerSelection;

            bool isForReturn = options.LastRetailerSelectionIsForReturn;

            if (isForReturn)
                selectedRetailUnit = options.SelectedReturnRetailUnit;
            else
                selectedRetailUnit = options.SelectedOutwardRetailUnit;

        }

        /// <summary>
        /// Logs the event that the user is performing handoff to retailer site
        /// </summary>
        private void logEvent()
        {

            RetailerHandoffEvent handoffEvent = new RetailerHandoffEvent(
                selectedRetailer.Id,
                TDSessionManager.Current.Session.SessionID,
                TDSessionManager.Current.Authenticated);
            Logger.Write(handoffEvent);

        }
        #endregion
    }
}
