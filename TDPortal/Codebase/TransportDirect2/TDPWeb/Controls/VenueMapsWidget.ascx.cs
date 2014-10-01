// *********************************************** 
// NAME             : VenueMapsWidget.ascx.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 07 May 2011
// DESCRIPTION  	: Venue maps widget to be shown in the right hand section
// ************************************************


using System;
using TDP.Common.LocationService;
using TDP.Common.Web;
using TDP.UserPortal.JourneyControl;

namespace TDP.UserPortal.TDPWeb.Controls
{
    /// <summary>
    /// Venue maps widget to be shown in the right hand section
    /// </summary>
    public partial class VenueMapsWidget : System.Web.UI.UserControl
    {
        #region Private Fields
        #endregion

        #region Public Properties
        
        #endregion

        #region Page_Init, Page_Load, Page_PreRender
        /// <summary>
        /// Page PreRender event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupVenueMapsLinks();
        }

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        /// <summary>
        /// Set up venue related link in the context of the mode of journey choosed
        /// i.e. if park and ride journey is being plan show maps of park and ride locations
        /// </summary>
        private void SetupVenueMapsLinks()
        {
            TDPPage page = (TDPPage)Page;

            SessionHelper sessionHelper = new SessionHelper();
            ITDPJourneyRequest journeyRequest = sessionHelper.GetTDPJourneyRequest();

            // Get venue location, checking the Destination first
            TDPVenueLocation venue = null;

            if (journeyRequest.Destination is TDPVenueLocation)
            {
                venue = (TDPVenueLocation)journeyRequest.Destination;
            }
            else if (journeyRequest.Origin is TDPVenueLocation)
            {
                venue = (TDPVenueLocation)journeyRequest.Origin;
            }

            if (venue != null && !string.IsNullOrEmpty(venue.VenueMapUrl))
            {
                venueImageLink.NavigateUrl = pdfLink.NavigateUrl = widgetHeadingLink.NavigateUrl = venue.VenueMapUrl;

                string url = page.GetResource(string.Format("VenueMapsWidget.VenueMapImage.{0}.Url", venue.ID));
                if (string.IsNullOrEmpty(url))
                {
                    url = page.GetResource("VenueMapsWidget.VenueMapImage.Url");
                }

                venueImage.ImageUrl = page.ImagePath + url;
                venueImage.AlternateText = venueImage.ToolTip = string.Format(page.GetResource("VenueMapsWidget.WidgetHeading.Text"), venue.DisplayName);
                
                widgetHeadingLink.Text = widgetHeadingLink.ToolTip = venueImage.ToolTip;

                pdfDownloadButton.Text = page.GetResource("VenueMapsWidget.PDFLinkText.Text");
                pdfDownloadButton.ToolTip = page.GetResource("VenueMapsWidget.PDFLinkText.AlternateText");
            }
            else
            {
                this.Visible = false;
            }

        }
        #endregion
    }
}