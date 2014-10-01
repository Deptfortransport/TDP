// *********************************************** 
// NAME             : Direction.aspx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 22 Feb 2012
// DESCRIPTION  	: Journey Direction page (showing detailed cycle/car directions)
// ************************************************
// 

using System;
using System.Web.UI;
using TDP.Common;
using TDP.Common.Extenders;
using TDP.Common.PropertyManager;
using TDP.Common.Web;
using TDP.UserPortal.JourneyControl;

namespace TDP.UserPortal.TDPMobile
{
    /// <summary>
    /// Journey Direction page
    /// </summary>
    public partial class Direction : TDPPageMobile
    {
        #region Variables

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public Direction()
            : base(Global.TDPResourceManager)
        {
            pageId = PageId.MobileDirection;
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
            if (!Page.IsPostBack)
            {
                SetupControls();
            }
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupResources();

            SetupMapLink();
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
        }

        /// <summary>
        /// Sets up the controls on the page
        /// </summary>
        private void SetupControls()
        {
            JourneyResultHelper resultHelper = new JourneyResultHelper();
            JourneyHelper journeyHelper = new JourneyHelper();

            // Journey request/result
            ITDPJourneyRequest journeyRequest = resultHelper.JourneyRequest;
            ITDPJourneyResult journeyResult = resultHelper.CheckJourneyResultAvailability();

            // Journey to be shown
            string journeyRequestHash = string.Empty;
            Journey journeyOutward = null;
            Journey journeyReturn = null;

            // Should only find an outward journey
            journeyHelper.GetJourneys(out journeyRequestHash, out journeyOutward, out journeyReturn);

            // If arrived on page without user selecting a journey (e.g. Cycle may not show Summary page)
            if ((journeyResult != null) && (journeyResult.OutwardJourneys.Count == 1))
            {
                journeyOutward = journeyResult.OutwardJourneys[0];
            }

            if (journeyOutward != null)
            {
                // Display the journey leg detail directions
                cycleLeg.Initialise(journeyRequest, GetCycleJourneyLeg(journeyOutward));

                legCycle.Visible = true;
            }
        }

        /// <summary>
        /// Method returns the JourneyLeg in the Journey containg the cycle transport mode
        /// </summary>
        /// <param name="journey"></param>
        /// <returns></returns>
        private JourneyLeg GetCycleJourneyLeg(Journey journey)
        {
            foreach (JourneyLeg journeyLeg in journey.JourneyLegs)
            {
                if (journeyLeg.Mode == TDPModeType.Cycle)
                {
                    return journeyLeg;
                }
            }

            return null;
        }

        /// <summary>
        /// Sets up the display of the map link, click logic is in master page
        /// </summary>
        private void SetupMapLink()
        {
            if (Properties.Current["Map.Journey.Cycle.Enabled.Switch"].Parse(true))
            {
                ((TDPMobile)Master).DisplayNext = true;
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