// *********************************************** 
// NAME             : Detail.aspx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Feb 2012
// DESCRIPTION  	: Journey Detail page
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TDP.Common;
using TDP.Common.Extenders;
using TDP.Common.Web;
using TDP.UserPortal.JourneyControl;
using TDP.UserPortal.TDPMobile.Controls;
using TDP.Common.PropertyManager;

namespace TDP.UserPortal.TDPMobile
{
    /// <summary>
    /// Journey Detail page
    /// </summary>
    public partial class Detail : TDPPageMobile
    {
        #region Variables

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public Detail()
            : base(Global.TDPResourceManager)
        {
            pageId = PageId.MobileDetail;
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
            journeyPageControl.ShowJourneyHandler += new OnShowJourney(ShowJourneyEvent);
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SetupControls();

            AddJavascript("Detail.js");
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
        
        #region Event handlers
        
        #region Show journey events

        /// <summary>
        /// Shows the selected journey
        /// </summary>
        protected void ShowJourneyEvent(object sender, JourneyEventArgs e)
        {
            if (e != null)
            {
                // Get the selected journey, and refresh the controls
                JourneyResultHelper resultHelper = new JourneyResultHelper();
                JourneyHelper journeyHelper = new JourneyHelper();

                // Persist selected journey to session (for any browser navigation)
                journeyHelper.SetJourneySelected(true, e.JourneyId);
                
                ITDPJourneyRequest journeyRequest = resultHelper.JourneyRequest;
                ITDPJourneyResult journeyResult = resultHelper.CheckJourneyResultAvailability();
                Journey journey = journeyResult.GetJourney(e.JourneyId);
                bool accessibleFriendly = (CurrentStyle.AccessibleStyleValue != AccessibleStyle.Normal);

                if (journey != null)
                {
                    // Display the journey pageing control
                    journeyPageControl.Initialise(journeyRequest, journeyResult, journey);
                    journeyPageControl.Refresh();

                    // Display the journey leg details
                    legsDetails.Initialise(journeyRequest, journey.JourneyLegs,
                        journey.AccessibleJourney, accessibleFriendly);
                    legsDetails.Refresh();

                    // Display the journey duration control foot
                    journeyHeadingControlFooter.Initialise(journeyRequest, journey, false, true);
                }
            }
        }

        #endregion

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

            // Determine if the details control should render in accessible mode
            bool accessibleFriendly = (CurrentStyle.AccessibleStyleValue != AccessibleStyle.Normal);

            if (journeyRequest != null)
            {
                // Display the journey heading control
                journeyHeadingControl.Initialise(journeyRequest, null, true, false);
            }

            if (journeyOutward != null)
            {
                // Display the journey pageing control
                journeyPageControl.Initialise(journeyRequest, journeyResult, journeyOutward);

                // Display the journey leg details
                legsDetails.Initialise(journeyRequest, journeyOutward.JourneyLegs, 
                    journeyOutward.AccessibleJourney, accessibleFriendly);

                // Display the journey duration control foot
                journeyHeadingControlFooter.Initialise(journeyRequest, journeyOutward, false, true);

                if (journeyOutward.GetUsedModes().Contains(TDPModeType.Cycle))
                {
                    ((TDPMobile)Master).DisplayNext = true;
                }
                else
                {
                    // Travel News functionality available
                    if (Properties.Current["TravelNews.Enabled.Switch"].Parse(true))
                    {
                        ((TDPMobile)Master).DisplayNext = true;
                        ((TDPMobile)Master).ButtonNextPage = PageId.MobileTravelNews;   
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