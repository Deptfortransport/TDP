// *********************************************** 
// NAME             : TravelNewsDetail.aspx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 11 May 2012
// DESCRIPTION  	: TravelNewsDetails page to display details for a specific travel or underground news item
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TDP.Common;
using TDP.Common.DataServices;
using TDP.Common.Extenders;
using TDP.Common.LocationService;
using TDP.Common.PropertyManager;
using TDP.Common.ServiceDiscovery;
using TDP.Common.Web;
using TDP.Reporting.Events;
using TDP.UserPortal.SessionManager;
using TDP.UserPortal.TravelNews.SessionData;
using Logger = System.Diagnostics.Trace;
using TDP.UserPortal.TravelNews;
using TDP.UserPortal.TravelNews.TravelNewsData;
using TDP.UserPortal.UndergroundNews;

namespace TDP.UserPortal.TDPMobile
{
    /// <summary>
    /// TravelNewsDetail
    /// </summary>
    public partial class TravelNewsDetail : TDPPageMobile
    {
        #region Variables

        // Read from properties to control overall showing of news controls
        private bool showTravelNews = true;
        private bool showUndergroundStatus = true;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public TravelNewsDetail()
            : base(Global.TDPResourceManager)
        {
            pageId = PageId.MobileTravelNewsDetail;
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
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitialiseControls();

            SetupControls();
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupResources();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialises travel news controls
        /// </summary>
        private void InitialiseControls()
        {
            showTravelNews = Properties.Current["TravelNews.Enabled.Switch"].Parse(true);
            showUndergroundStatus = Properties.Current["UndergroundNews.Enabled.Switch"].Parse(true);

            if (!showTravelNews && !showUndergroundStatus)
            {
                DisplayMessage(new TDPMessage("TravelNews.lblUnavailable.Text", TDPMessageType.Error));
            }
        }

        /// <summary>
        /// Loads resource strings for labels/controls
        /// </summary>
        private void SetupResources()
        {

        }

        /// <summary>
        /// Setup the controls on the page
        /// </summary>
        private void SetupControls()
        {
            // Hide both by default
            travelNewsDetailControl.Visible = false;
            undergroundStatusDetailControl.Visible = false;

            // Get news id to display (this will either be a travel news id or an underground status id

            TravelNewsHelper tnHelper = new TravelNewsHelper();

            string newsId = tnHelper.GetTravelNewsId(true);

            if (!string.IsNullOrEmpty(newsId))
            {
                // Get travel news items
                ITravelNewsHandler travelNewsHandler = TDPServiceDiscovery.Current.Get<ITravelNewsHandler>(ServiceDiscoveryKey.TravelNews);

                TravelNewsItem tni = travelNewsHandler.GetDetailsByUid(newsId);

                if (tni != null)
                {
                    // Found a travel news item, display this control
                    travelNewsDetailControl.Initialise(tni);

                    travelNewsDetailControl.Visible = true && showTravelNews;
                }
                else
                {
                    // Check the underground status for the news item
                    IUndergroundNewsHandler undergroundNewsHandler = TDPServiceDiscovery.Current.Get<IUndergroundNewsHandler>(ServiceDiscoveryKey.UndergroundNews);

                    List<UndergroundStatusItem> items = undergroundNewsHandler.GetUndergroundStatusItems();
                    
                    if (items != null && items.Count > 0)
                    {
                        // Find the underground status item
                        string lineName = newsId.Replace("_", " ").ToLower();
                        
                        UndergroundStatusItem usi = items.Find(delegate(UndergroundStatusItem i) { return i.LineName.ToLower() == lineName; });

                        undergroundStatusDetailControl.Initialise(usi);

                        undergroundStatusDetailControl.Visible = true && showUndergroundStatus;
                    }
                }
            }
        }
        
        /// <summary>
        /// Displays a message
        /// </summary>
        private void DisplayMessage(TDPMessage tdpMessage)
        {
            ((TDPMobile)this.Master).DisplayMessage(tdpMessage);
        }

        #endregion
    }
}