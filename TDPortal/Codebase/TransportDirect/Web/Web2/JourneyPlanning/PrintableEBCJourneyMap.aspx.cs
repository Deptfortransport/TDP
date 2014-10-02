// *********************************************** 
// NAME                 : PrintableEBCJourneyMap.aspx.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 15/10/2009 
// DESCRIPTION  		: Printable EBC journey map page
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableEBCJourneyMap.aspx.cs-arc  $
//
//   Rev 1.1   Oct 22 2009 10:51:20   apatel
//Added header info
//Resolution for 5323: CCN539 Environmental Benefit Calculator

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;

namespace TransportDirect.UserPortal.Web.JourneyPlanning
{
    public partial class PrintableEBCJourneyMap : TDPrintablePage, INewWindowPage
    {
        #region Private Fields
        // Session variables
        private ITDSessionManager sessionManager;

        // State of results
        /// <summary>
        ///  True if there is an outward trip for the current selection
        /// </summary>
        private bool outwardExists = false;
        #endregion

        #region Constructor
        /// <summary>
		/// Constructor - sets the Page Id.
		/// </summary>
        public PrintableEBCJourneyMap()
		{
			pageId = PageId.PrintableEBCJourneyMap;
        }
        #endregion

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the session values needed by this page
            sessionManager = TDSessionManager.Current;

            InitialiseControls();

            LoadResources();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Establish whether we have any results
        /// </summary>
        private void DetermineStateOfResults()
        {
            //check for road journey result
            ITDJourneyResult result = sessionManager.JourneyResult;
            if (result != null)
            {
                outwardExists = ((result.OutwardRoadJourneyCount) > 0) && result.IsValid;
            }
        }

        /// <summary>
        /// Loads text and images on the page
        /// </summary>
        private void LoadResources()
        {
            labelPrinterFriendly.Text = Global.tdResourceManager.GetString(
                "EBCPlanner.StaticPrinterFriendly.labelPrinterFriendly", TDCultureInfo.CurrentUICulture);

            labelInstructions.Visible = false;

        }

        /// <summary>
        /// Initialises controls on page with page and journey details
        /// </summary>
        private void InitialiseControls()
        {
            DetermineStateOfResults();

            journeysSearchedForControl.UseRouteFoundForHeading = true;

            string UrlQueryString = string.Empty;
            
            //The Query params is set using javascript on the non-printable page
            UrlQueryString = Request.Params["units"];
            if (UrlQueryString == "kms")
            {
                mapOutward.CarJourneyDetails.RoadUnits = RoadUnitsEnum.Kms;
                carSummaryControl.RoadUnits = RoadUnitsEnum.Kms;
                ebcCalculationDetailsTableControl.RoadUnits = RoadUnitsEnum.Kms;
            }
            else
            {
                mapOutward.CarJourneyDetails.RoadUnits = RoadUnitsEnum.Miles;
                carSummaryControl.RoadUnits = RoadUnitsEnum.Miles;
                ebcCalculationDetailsTableControl.RoadUnits = RoadUnitsEnum.Miles;
            }

            TDItineraryManager itineraryManager = TDItineraryManager.Current;

            
            TDJourneyViewState viewState = itineraryManager.JourneyViewState;
            ITDJourneyResult result = sessionManager.JourneyResult;

            if (result != null)
            {
                labelReference.Text = result.JourneyReferenceNumber.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
            }

            // Map outward is visible only if outward results exist.
            if (outwardExists)
            {
                panelMapOutward.Visible = true;

                carSummaryControl.Initialise(result.OutwardRoadJourney(), outwardExists);

                FindEBCPageState findEBCPageState = (FindEBCPageState)sessionManager.FindPageState;
                ebcCalculationDetailsTableControl.Initialise( findEBCPageState.EnvironmentalBenefits);

                ebcCalculationDetailsTableControl.Printable = true;

                
                mapOutward.Populate(true, false, TDSessionManager.Current.IsFindAMode);

                labelReferenceTitle.Visible = (result != null);
                labelReference.Visible = (result != null);

                labelDateTime.Text = TDDateTime.Now.ToString("G");

                labelUsernameTitle.Visible = sessionManager.Authenticated;
                labelUsername.Visible = sessionManager.Authenticated;
                if (sessionManager.Authenticated)
                {
                    labelUsername.Text = sessionManager.CurrentUser.Username;
                }
            }
        }

        #endregion
    }
}
