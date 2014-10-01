// *********************************************** 
// NAME             : VenueMapControl.cs      
// AUTHOR           : Mark Danforth
// DATE CREATED     : 15 Apr 2012 i think
// DESCRIPTION  	: VenueMapControl
// ************************************************
// 
                
using System;
using System.Linq;
using System.Web.UI.WebControls;
using TDP.Common.LocationService;
using TDP.Common.ResourceManager;
using TDP.Common.Web;

namespace TDP.UserPortal.TDPMobile.Controls
{
    /// <summary>
    /// VenueMapControl
    /// </summary>
    public partial class VenueMapControl : System.Web.UI.UserControl
    {
        /// <summary>
        /// returns the venue map image control
        /// </summary>
        public Image VenueMapImage
        {
            get
            {
                return venueMap;
            }
        }

        /// <summary>
        /// returns the venue map pdf control
        /// </summary>
        public HyperLink VenuePdfLink
        {
            get
            {
                return venueMapPdf;
            }
        }

        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// page pre render event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            // hide the pdf link if it's unavailable
            if (venueMapPdf.NavigateUrl == null)
            {
                venueMapPdf.Visible = false;
            }

            SetupResources();
        }

        /// <summary>
        /// Gets the appropriate resources for the given destination
        /// </summary>
        /// <param name="venue"></param>
        /// <param name="journeyDateTime"></param>
        public void SetVenue(TDPVenueLocation venue, DateTime journeyDateTime)
        {
            TDPPageMobile currentPage = (TDPPageMobile)Page;

            // Get map image url from resource manager, for the journey date
            venueMap.ImageUrl = currentPage.GetResource(TDPResourceManager.GROUP_JOURNEYOUTPUT, TDPResourceManager.COLLECTION_JOURNEY, string.Format("Venue.VenueMapImage.{0}.Url.{1}",
                venue.Naptan.FirstOrDefault(),
                journeyDateTime.ToString("yyyyMMdd")));

            // Otherwise get default map image url from resource manager
            if (string.IsNullOrEmpty(venueMap.ImageUrl))
            {
                venueMap.ImageUrl = currentPage.GetResource(TDPResourceManager.GROUP_JOURNEYOUTPUT, TDPResourceManager.COLLECTION_JOURNEY, string.Format("Venue.VenueMapImage.{0}.Url",
                venue.Naptan.FirstOrDefault()));
            }

            // add correct path to the url
            venueMap.ImageUrl = currentPage.ImagePath + venueMap.ImageUrl;

            // set up the text resources
            venueMap.AlternateText = currentPage.GetResource(TDPResourceManager.GROUP_JOURNEYOUTPUT, TDPResourceManager.COLLECTION_JOURNEY, "JourneyOutput.Text.VenueMapLink");
            venueMap.ToolTip = string.Format(currentPage.GetResource(TDPResourceManager.GROUP_JOURNEYOUTPUT, TDPResourceManager.COLLECTION_JOURNEY, "JourneyOutput.Text.VenueMapLinkToolTip"), venue.DisplayName);

            // set up the pdf link and it's text resources
            venueMapPdf.NavigateUrl = venue.VenueMapUrl;
            venueMapPdf.Text = currentPage.GetResource(TDPResourceManager.GROUP_JOURNEYOUTPUT, TDPResourceManager.COLLECTION_JOURNEY, "JourneyOutput.Text.VenueMapLink") + " (pdf)";
            venueMapPdf.ToolTip = string.Format(currentPage.GetResource(TDPResourceManager.GROUP_JOURNEYOUTPUT, TDPResourceManager.COLLECTION_JOURNEY, "JourneyOutput.Text.VenueMapLinkToolTip"), venue.DisplayName);

            this.Visible = true;
        }

        /// <summary>
        /// Sets up resource string from content database
        /// </summary>
        private void SetupResources()
        {
            TDPPageMobile page = (TDPPageMobile)Page;

            venueMapPageHeading.InnerText = page.GetResourceMobile("JourneyDetail.VenueMapPage.Heading.Text");
            venueMapLbl.Text = page.GetResourceMobile("JourneyDetail.VenueMapPage.InfoLabel.Text");
            closevenuemap.Text = page.GetResourceMobile("JourneyInput.Back.MobileDetail.Text");
            closevenuemap.ToolTip = page.GetResourceMobile("JourneyInput.Back.MobileDetail.ToolTip");
        }
    }
}