using System;
using System.Collections.Generic;
using System.Web;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.Web.Controls;

namespace TransportDirect.UserPortal.Web.Adapters
{
    public class FindInternationalInputAdapter: FindJourneyInputAdapter
    {
        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="journeyParams">Journey parameters for Find A International journey planning</param>
        /// <param name="pageState">Page state for Find A International input pages</param>
        /// <param name="inputPageState">Variables indicating state of page, return stack for navigation etc.</param>
        public FindInternationalInputAdapter(TDJourneyParametersMulti journeyParams, 
            FindInternationalPageState pageState, InputPageState inputPageState) :
            base(pageState, TDSessionManager.Current, inputPageState, journeyParams)
        {
        }

        #endregion
        
        #region Public Methods
        /// <summary>
        /// Initialises FindToFromLocationsControl used on page
        /// </summary>
        public void InitialiseControls(FindToFromLocationsControl locationsControl)
        {
            InitLocationsControl(locationsControl);

            // Use the scriptable dropdown control for the From and To 
            locationsControl.SetScriptableDropdownList(null, null);

            // The location search object used to resolve location should call 
            // DisableGisQuery to prevent a nearest Naptan and TOIUD lookup.
            locationsControl.OriginSearch.DisableGisQuery();
            locationsControl.DestinationSearch.DisableGisQuery();


        }

        /// <summary>
        /// Retrieves a AsyncCallState object for International plans
        /// </summary>
        /// <returns></returns>
        public override void InitialiseAsyncCallState()
        {
            AsyncCallState acs = new JourneyPlanState();

            // Determine refresh interval and resource string for the wait page
            acs.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.FindInternational"]);
            acs.WaitPageMessageResourceFile = "langStrings";
            acs.WaitPageMessageResourceId = "WaitPageMessage.FindInternational";

            acs.AmbiguityPage = PageId.FindInternationalInput;
            acs.Status = AsyncCallStatus.None;

            // Depending on Trunk mode, send to JourneyOverview or JourneySummary
            if (tdSessionManager.FindAMode == FindAMode.International)
            {
                acs.DestinationPage = PageId.JourneyOverview;
                acs.ErrorPage = PageId.JourneyOverview;
            }
            else
            {
                acs.DestinationPage = PageId.JourneyDetails;
                acs.ErrorPage = PageId.JourneyDetails;
            }

            tdSessionManager.AsyncCallState = acs;
        }

        /// <summary>
		/// Initialises journey parameters with default values for Find A journey planning.
		/// </summary>
        public override void InitJourneyParameters()
        {
            base.InitJourneyParameters();
            TransportDirect.JourneyPlanning.CJPInterface.ModeType[] publicModes = new TransportDirect.JourneyPlanning.CJPInterface.ModeType[3];
            publicModes[0] = TransportDirect.JourneyPlanning.CJPInterface.ModeType.Air;
            publicModes[1] = TransportDirect.JourneyPlanning.CJPInterface.ModeType.Rail;
            publicModes[2] = TransportDirect.JourneyPlanning.CJPInterface.ModeType.Coach;
            journeyParams.PublicModes = publicModes;
        }
        #endregion
    }
}
