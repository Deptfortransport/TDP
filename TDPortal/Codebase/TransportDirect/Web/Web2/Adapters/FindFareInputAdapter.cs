// *********************************************** 
// NAME			: FindFareInputAdapter.cs
// AUTHOR		: Richard Hopkins
// DATE CREATED	: 10/01/05
// DESCRIPTION	: Responsible for common functionality
// required by Find A Fare input pages.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/FindFareInputAdapter.cs-arc  $
//
//   Rev 1.4   Dec 05 2012 13:55:42   mmodi
//Supress unnecessary warnings during compile
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.3   Aug 01 2011 14:52:44   apatel
//Code update to stop FindTrainCostInput page and FindCarParkInput page from breaking in specific navigation flow
//Resolution for 5717: Find Cheaper Rail Fare page breaks in certain navigation flow
//
//   Rev 1.2   Mar 31 2008 12:59:00   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:18   mturner
//Initial revision.
//
//   Rev 1.24   Nov 14 2006 09:53:38   rbroddle
//Merge for stream4220
//
//   Rev 1.23.1.0   Nov 07 2006 11:28:06   tmollart
//Added/Updated methods for Rail Search By Price.
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.23   Apr 05 2006 15:42:50   build
//Automatically merged from branch for stream0030
//
//   Rev 1.22.1.0   Mar 29 2006 11:15:30   RGriffith
//Wait Page Ehancement changes
//Resolution for 33: DEL 8.1 Workstream: Wait Pages
//
//   Rev 1.22   Feb 23 2006 16:58:32   RWilby
//Merged stream3129
//
//   Rev 1.21   Jan 16 2006 15:58:40   RPhilpott
//Code review changes.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.20   Nov 09 2005 12:31:56   build
//Automatically merged from branch for stream2818
//
//   Rev 1.19.1.0   Oct 14 2005 15:28:12   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.20   Oct 14 2005 15:24:48   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.19   Sep 29 2005 12:38:26   build
//Automatically merged from branch for stream2673
//
//   Rev 1.18.1.1   Sep 29 2005 11:20:22   build
//Automatically merged from branch for stream2673
//
//   Rev 1.18.1.0   Sep 14 2005 12:31:48   rgreenwood
//DN079 ES015 24 Hr Help Code Review: removed redundant code
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.18   May 10 2005 17:41:52   rhopkins
//FxCop corrections
//
//   Rev 1.17   Apr 25 2005 11:49:42   COwczarek
//Fix InvokeValidateAndRunServices to pass both inward and outward tickets to ValidateAndRunServices
//Resolution for 2191: PT - Correct dates not being used to obtain services for selected ticket
//
//   Rev 1.16   Apr 21 2005 13:23:50   rhopkins
//Changed DisplayMessages() so that it now always uses langStrings.  Also overloaded method with version that does not hide the instruction panel, so that error messages can be displayed as continuable warnings.
//Resolution for 2287: FindAFare date selection is not displaying errors correctly
//
//   Rev 1.15   Apr 04 2005 15:33:48   tmollart
//Modified InitPreferencesControl method so that the value of SearchParams.showChild is swapped so that the Adult/Child radio buttons are populated correctly.
//
//   Rev 1.14   Mar 31 2005 14:36:32   tmollart
//Changed InitJourneyParameters method so that air is not added to the travel mode params.
//
//   Rev 1.13   Mar 29 2005 11:56:04   tmollart
//Added initialisation for parameters in CostSearchParams.
//
//   Rev 1.12   Mar 22 2005 11:57:02   tmollart
//Added AmbiguitySearch method.
//
//   Rev 1.11   Mar 18 2005 15:17:24   COwczarek
//Fix to InvokeValidateServices method. Add DisplayMessages method.
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.10   Mar 16 2005 14:18:06   tmollart
//Modified invoke validation and run fares method. Refreshes internal pageState object from session manager and returns searchWaitStateData object direct from session manager. Added additional comments.
//
//   Rev 1.9   Mar 14 2005 16:59:58   COwczarek
//Implement methods to invoke cost search runner services
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.8   Mar 11 2005 13:41:08   tmollart
//Added code to initialise page state discount cards.
//Added code to clear deffered session storage.
//
//   Rev 1.7   Mar 10 2005 16:49:44   COwczarek
//Changed/added methods that invoke cost search runner
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.6   Mar 09 2005 15:48:52   rscott
//DEL 7 - Added method GetCostBasedSearchWaitControlData public method.
//
//   Rev 1.5   Mar 04 2005 11:03:18   tmollart
//Final work in progress revision.
//
//   Rev 1.4   Mar 01 2005 18:27:56   tmollart
//Work in progress.
//
//   Rev 1.3   Feb 24 2005 12:02:48   tmollart
//Work in progress.
//
//   Rev 1.2   Feb 10 2005 17:28:18   tmollart
//Work in progress.
//
//   Rev 1.1   Jan 31 2005 17:00:04   tmollart
//Work in progress for Del 7 Find A Fare.
//
//   Rev 1.0   Jan 25 2005 12:15:18   rhopkins
//Initial revision.
//

using System;
using System.Collections;
using System.Globalization;
using TransportDirect.Common;
using System.Web.UI.WebControls;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.CostSearchRunner;
using TransportDirect.UserPortal.CostSearch;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.ResourceManager;
using System.Text;

namespace TransportDirect.UserPortal.Web.Adapters
{

    /// <summary>
    /// Responsible for common functionality required by Find A Fare input pages.
    /// </summary>
    public class FindFareInputAdapter : FindInputAdapter
    {
		/// <summary>
		/// Journey parameters for Find A Fare planning
		/// </summary>
		private CostSearchParams searchParams;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="pageState">Page state for Find A Fare input pages</param>
		/// <param name="sessionManager">Current SessionManager</param>
		public FindFareInputAdapter(FindCostBasedPageState pageState, ITDSessionManager sessionManager)
			: base(pageState, sessionManager)
		{
            if (tdSessionManager.JourneyParameters is CostSearchParams)
            {
                searchParams = (CostSearchParams)tdSessionManager.JourneyParameters;
            }
            else
            {
                searchParams = new CostSearchParams();
                searchParams.Initialise();
            }
		}

        private const string constNoFareAvailable = "FindFareTicketSelection.messages.Text";

		#region General Public Methods

        /// <summary>
		/// Retrieves a JourneyPlanControlData object for FindFare plans
		/// Obsolete - Use InitialiseAsyncCallStateForFaresSearch() or InitialiseAsyncCallStateForServicesSearch()
		/// </summary>
		/// <returns></returns>
#pragma warning disable 809
        [Obsolete("Use InitialiseAsyncCallStateForFaresSearch() or InitialiseAsyncCallStateForServicesSearch()", false)]
        public override void InitialiseAsyncCallState()
		{
			throw new NotImplementedException("Use InitialiseAsyncCallStateForFaresSearch() or InitialiseAsyncCallStateForServicesSearch()");
        }
#pragma warning restore 809

        /// <summary>
		/// 
		/// </summary>
		/// <param name="refreshInterval"></param>
		/// <param name="waitPageResourceFileName"></param>
		/// <param name="waitPageResourceFileId"></param>
		public void InitialiseAsyncCallStateForRailSearchByPrice(int refreshInterval, string waitPageResourceFileName, string waitPageResourceFileId)
		{
			AsyncCallState acs = new CostBasedFaresSearchState();

			// Determine refresh interval and resource string for the wait page from parameters passed in
			acs.WaitPageRefreshInterval = refreshInterval;
			acs.WaitPageMessageResourceFile = waitPageResourceFileName;
			acs.WaitPageMessageResourceId = waitPageResourceFileId;

			acs.AmbiguityPage = PageId.FindTrainCostInput;
			acs.DestinationPage = PageId.FindFareDateSelection;	// <<<<<
			acs.ErrorPage = PageId.FindFareDateSelection;		// <<<<<  SPEAK TO MARK !!
			acs.Status = AsyncCallStatus.None;					// <<<<<
			tdSessionManager.AsyncCallState = acs;
		}



		/// <summary>
		/// Initialises the AsyncCallState property of TDSessionManager for fares search
		/// </summary>
		/// <param name="refreshInterval">Defines the length of time to wait for each refresh</param>
		/// <param name="waitPageResourceFileName">Defines the resource file name containing the wait page message</param>
		/// <param name="waitPageResourceFileId">Defines the resource identifier containing the wait page message</param>
		/// <returns></returns>
		public void InitialiseAsyncCallStateForFaresSearch(int refreshInterval, string waitPageResourceFileName, string waitPageResourceFileId)
		{
			AsyncCallState acs = new CostBasedFaresSearchState();

			// Determine refresh interval and resource string for the wait page from parameters passed in
			acs.WaitPageRefreshInterval = refreshInterval;
			acs.WaitPageMessageResourceFile = waitPageResourceFileName;
			acs.WaitPageMessageResourceId = waitPageResourceFileId;

			acs.AmbiguityPage = PageId.FindFareInput;
			acs.DestinationPage = PageId.FindFareDateSelection;
			acs.ErrorPage = PageId.FindFareDateSelection;
			acs.Status = AsyncCallStatus.None;
			tdSessionManager.AsyncCallState = acs;
		}

		/// <summary>
		/// Initialises the AsyncCallState property of TDSessionManager for services search
		/// </summary>
		public void InitialiseAsyncCallStateForServicesSearch(PageId ticketSelectionPage)
		{
			AsyncCallState acs = new CostBasedServicesSearchState();
			
			// Determine refresh interval and resource string for the wait page
			acs.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.FindFare.TicketSelection"]);
			acs.WaitPageMessageResourceFile = "langStrings";
			acs.WaitPageMessageResourceId = "WaitPageMessage.FindFare.TicketSelection";

			acs.AmbiguityPage = ticketSelectionPage;
            acs.DestinationPage = PageId.JourneyDetails;
			acs.ErrorPage = ticketSelectionPage;
			acs.Status = AsyncCallStatus.None;
			tdSessionManager.AsyncCallState = acs;
		}

		/// <summary>
		/// Initialises journey parameters with default values for Find A Fare planning.
		/// </summary>
		public override void InitJourneyParameters() 
		{
			IDataServices populator;
			populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

			//Call method on searchParams class to initialise own and inherited parameters.
			searchParams.Initialise();

			//Initialise travel modes. NOTE: Air NOT added as it is not part of DEL 7 at this time.
			ArrayList modeList = new ArrayList(3);
			modeList.Add(TicketTravelMode.Rail);
			modeList.Add(TicketTravelMode.Coach);
			//modeList.Add(TicketTravelMode.Air);
			searchParams.TravelModesParams = (TicketTravelMode[])modeList.ToArray(typeof(TicketTravelMode));

			//Initialise date flexibility
			searchParams.OutwardFlexibilityDays	= Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.DateFlexibilityDrop), CultureInfo.InvariantCulture);
			searchParams.InwardFlexibilityDays = searchParams.OutwardFlexibilityDays;

			//Initialise preferences options
			searchParams.RailDiscountedCard = populator.GetDefaultListControlValue(DataServiceType.DiscountRailCardDrop);
			searchParams.CoachDiscountedCard = populator.GetDefaultListControlValue(DataServiceType.DiscountCoachCardDrop);

			// Also Initialise the ValidationError object
			if (tdSessionManager.ValidationError != null)
				tdSessionManager.ValidationError.Initialise();
		}

		/// <summary>
        /// Initialises the locationsControl with values from the search parameters
        /// </summary>
        /// <param name="locationsControl">The control to initialise</param>
        public override void InitLocationsControl(FindToFromLocationsControl locationsControl)
        {
            //Initialise the start and end locations.
			locationsControl.OriginLocation = searchParams.OriginLocation;
            locationsControl.DestinationLocation = searchParams.DestinationLocation;

			//Initialise the start/end location control types.
			locationsControl.FromLocationControl.LocationControlType = searchParams.OriginType;
			locationsControl.ToLocationControl.LocationControlType = searchParams.DestinationType;

			//Initialise the start/search objects.
			locationsControl.OriginSearch = searchParams.Origin;
			locationsControl.DestinationSearch = searchParams.Destination;
        }

		/// <summary>
		/// Saves the values from the location controls into the search params.
		/// </summary>
		/// <param name="locationsControl"></param>
		public void SaveJourneyLocations(FindToFromLocationsControl locationsControl)
		{
			searchParams.OriginLocation = locationsControl.OriginLocation;
			searchParams.DestinationLocation = locationsControl.DestinationLocation;

			searchParams.OriginType = locationsControl.FromLocationControl.LocationControlType;
			searchParams.DestinationType = locationsControl.ToLocationControl.LocationControlType;

			searchParams.Origin = locationsControl.OriginSearch;
			searchParams.Destination = locationsControl.DestinationSearch;
		}

		/// <summary>
		/// Intialised the preferences control with values from the search parameters.
		/// </summary>
		/// <param name="preferencesControl"></param>
		public void InitPreferencesControl(FindFarePreferenceControl preferencesControl)
		{
			preferencesControl.DiscountRailCard = searchParams.RailDiscountedCard;
			preferencesControl.DiscountCoachCard = searchParams.CoachDiscountedCard;
			preferencesControl.AdultChild = !((FindCostBasedPageState)pageState).ShowChild;
			preferencesControl.PreferencesVisible = pageState.TravelDetailsVisible;
		 }

		/// <summary>
		/// Initialises the ModeSelectControl from the search params.
		/// </summary>
		/// <param name="modeSelectControl"></param>
		public void InitTravelModes(ModeSelectControl modeSelectControl)
		{
			//Set modes back to default.
			modeSelectControl.RailSelected = false;
			modeSelectControl.CoachSelected = false;
			modeSelectControl.AirSelected = false;

			//Check boxes not declared as a list control so set each one manually.
			if (searchParams.TravelModesParams != null)
			{
				for(int i=0; i<searchParams.TravelModesParams.Length; i++)
				{
					switch (searchParams.TravelModesParams[i])
					{
						case TicketTravelMode.Rail:
							modeSelectControl.RailSelected = true;
							break;
						case TicketTravelMode.Coach:
							modeSelectControl.CoachSelected = true;
							break;
						case TicketTravelMode.Air:
							modeSelectControl.AirSelected = true;
							break;
					}
				}
			}
		}

		#endregion

		#region Public Date Control Methods

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public virtual bool IsAtHighestLevel() 
		{
			return !( (searchParams.OriginLocation.Status == TDLocationStatus.Ambiguous && searchParams.Origin.CurrentLevel > 0) ||
					  (searchParams.DestinationLocation.Status == TDLocationStatus.Ambiguous && searchParams.Destination.CurrentLevel > 0) );
		}



		/// <summary>
		/// Initialises the FindLeaveReturnDatesControl with cost based search parameters.
		/// </summary>
		/// <param name="dateControl">The control to initialise</param>
		public override void InitDateControl(FindLeaveReturnDatesControl dateControl)
		{
			UpdateDateControl(dateControl);
		}

		/// <summary>
		/// Updates the dateControl with vales from the searchParams (Cost Based) object
		/// </summary>
		/// <param name="dateControl">The control to initialise</param>
		public override void UpdateDateControl(FindLeaveReturnDatesControl dateControl)
		{
			//Before populating the date control it is important to set the ambiguity
			//mode as this controls which nested date control (e.g. select/read only/
			//ambiguity) is populated.
			dateControl.LeaveDateControl.AmbiguityMode = pageState.AmbiguityMode;
			dateControl.ReturnDateControl.AmbiguityMode = pageState.AmbiguityMode;
			dateControl.LeaveDateControl.DateErrors = tdSessionManager.ValidationError;
			dateControl.ReturnDateControl.DateErrors = tdSessionManager.ValidationError;

			dateControl.LeaveDateControl.Populate(
				searchParams.OutwardDayOfMonth,
				searchParams.OutwardMonthYear,
				searchParams.OutwardFlexibilityDays
				);

			dateControl.ReturnDateControl.Populate(
				searchParams.ReturnDayOfMonth,
				searchParams.ReturnMonthYear,
				searchParams.InwardFlexibilityDays
				);
		}

		/// <summary>
		/// Saves the current date control values into the searchParams (Cost Based) object.
		/// </summary>
		/// <param name="dateControl">The control supplying the values</param>
		public override void UpdateJourneyDates(FindLeaveReturnDatesControl dateControl)
		{
			//This is needed here as the date controls rely on the ambiguty mode
			//being set to extract the correct day/month etc value. E.g. if we are 
			//currently in ambiguity mode it is required to extract the values from 
			//the ambiguity select control into the search params.

			dateControl.LeaveDateControl.AmbiguityMode = pageState.AmbiguityMode;
			dateControl.ReturnDateControl.AmbiguityMode = pageState.AmbiguityMode;
			dateControl.LeaveDateControl.DateErrors = tdSessionManager.ValidationError;
			dateControl.ReturnDateControl.DateErrors = tdSessionManager.ValidationError;
            
			if (dateControl.LeaveDateControl.DateControl.DisplayMode != GenericDisplayMode.ReadOnly)
			{
				searchParams.OutwardDayOfMonth = dateControl.LeaveDateControl.DateControl.Day;
				searchParams.OutwardMonthYear = dateControl.LeaveDateControl.DateControl.MonthYear;
				searchParams.OutwardFlexibilityDays = dateControl.LeaveDateControl.DateControl.Flexibility;
			}

			if (dateControl.ReturnDateControl.DateControl.DisplayMode != GenericDisplayMode.ReadOnly) 
			{
				searchParams.ReturnDayOfMonth = dateControl.ReturnDateControl.DateControl.Day;
				searchParams.ReturnMonthYear = dateControl.ReturnDateControl.DateControl.MonthYear;
				searchParams.InwardFlexibilityDays = dateControl.ReturnDateControl.DateControl.Flexibility;
				searchParams.IsReturnRequired = !dateControl.ReturnDateControl.DateControl.IsNoReturnSelected;
			}
		}

		#endregion

		/// <summary>
		/// Creates new location and search objects for "to" and "from" locations and 
		/// assigns those to the journey parameters and supplied locationsControl. Then initiates a 
		/// new search on those objects using the original user's input values. 
		/// </summary>
		/// <param name="locationsControl"></param>
		public virtual void AmbiguitySearch(FindToFromLocationsControl locationsControl)
		{
			if (locationsControl.OriginLocation.Status == TDLocationStatus.Unspecified)
			{
				locationsControl.FromLocationControl.Search();
			} 

			if (locationsControl.DestinationLocation.Status == TDLocationStatus.Unspecified)
			{
				locationsControl.ToLocationControl.Search();
			} 
		}

		/// <summary>
        /// Returns true if supplied ValidationError object contains errors indicating
        /// erroneous results for any location (to, from).
        /// </summary>
        /// <param name="errors">errors to search</param>
        /// <returns>True if errors indicate erroneous results for any location, false otherwise</returns>
        public virtual bool AreLocationsUnspecified(ValidationError errors)
        {
            return
                errors.Contains(ValidationErrorID.OriginLocationInvalid) ||
                errors.Contains(ValidationErrorID.OriginLocationInvalidAndOtherErrors) ||
                errors.Contains(ValidationErrorID.DestinationLocationInvalid) ||
                errors.Contains(ValidationErrorID.DestinationLocationInvalidAndOtherErrors) ||
				errors.Contains(ValidationErrorID.NoValidRoutes);
        }

        /// <summary>
        /// Updates supplied label with message to correct errors. Message depends on errors found in supplied
        /// errors object. The visibility of panelErrorMessage is set to true if the label is set with a message
        /// or false if not.
        /// </summary>
        /// <param name="panelErrorMessage">Panel to set visibility of</param>
        /// <param name="labelErrorMessages">Label to update with message</param>
        /// <param name="errors">errors to search</param>
        public override void UpdateErrorMessages(Panel panelErrorMessage, Label labelErrorMessages, ValidationError errors)
        {
			//bool which decides whether to show the "Then click Next" label. It is needed in all cases
			//except when there are overlapping errors
			bool showNextText = true;

            if(errors != null && errors.MessageIDs.Count > 0)
            {
                labelErrorMessages.Text = string.Empty;

                // Display "Select a new location" if origin or destination locations
                // have not been specified or cannot be resolved
                if(AreLocationsUnspecified(errors))
                {
                    labelErrorMessages.Text += Global.tdResourceManager.GetString(
                        LocationsUnspecifiedKey, TDCultureInfo.CurrentUICulture) + " ";
                }

				// Display "The origin and destination locations you have chosen overlap..." 
				// if origin and destination locations overlap i.e. share the same naptans 				
				if(AreOriginAndDestinationLocationsOverlapping(errors)) 
				{
					//get resource id of the error from the errors hashtable
					errors.MsgResourceID = errors.MessageIDs[ValidationErrorID.OriginAndDestinationOverlap].ToString();

					labelErrorMessages.Text = Global.tdResourceManager.GetString(
						errors.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ";
				}

                // Display "correct the sections marked in red" for those errors that 
                // are signified with red bordered fields (dates)
                if(FindDateValidation.OutwardDateErrors || FindDateValidation.ReturnDateErrors) 
                {
                    labelErrorMessages.Text += Global.tdResourceManager.GetString(
                        "ValidateAndRun.Correct", TDCultureInfo.CurrentUICulture) + " ";
                }

                // Add "then click next" but only if there was error text displayed
                if ((labelErrorMessages.Text.Length != 0) && (showNextText))
                {
                    labelErrorMessages.Text += Global.tdResourceManager.GetString(
                        "ValidateAndRun.ClickNext",
                        TDCultureInfo.CurrentUICulture);
                }

            } 
            else 
            {
                labelErrorMessages.Text = string.Empty;
            }

            panelErrorMessage.Visible = labelErrorMessages.Text.Length != 0;
        }

		/// <summary>
		/// Invokes the cost search runner to obtain fares data. 
		/// Returned CostBasedSearchWaitStateData object can be interegated to get the search status.
		/// </summary>
		/// <returns>CostBasedSearchStatus object. Can be interegated for status of search</returns>
		public AsyncCallStatus InvokeValidateAndRunFares()
		{
            ICostSearchRunner searchRunner = (ICostSearchRunner)TDServiceDiscovery.Current[ServiceDiscoveryKey.CostSearchRunner];
			searchRunner.ValidateAndRunFares(searchParams);

			//Clear the deferred data.
			tdSessionManager.ClearDeferredData();
			
			//Refresh the pageState object directly from the sessionManager and return
			// the searchaWaitStateData object directly from the session manager.
			pageState = (FindCostBasedPageState)tdSessionManager.FindPageState;
			return tdSessionManager.AsyncCallState.Status;
		}

        /// <summary>
        /// Invokes the cost search runner to obtain service data for fares. 
        /// Returned CostBasedSearchWaitStateData object can be interegated to get the search status.
        /// </summary>
        /// <returns>CostBasedSearchStatus object. Can be interegated for status of search</returns>
        public AsyncCallStatus InvokeValidateAndRunServices()
        {
            FindCostBasedPageState costBasedPageState = (FindCostBasedPageState)pageState;

            TravelDate travelDate = costBasedPageState.SelectedTravelDate.TravelDate;

            ICostSearchRunner searchRunner = (ICostSearchRunner)TDServiceDiscovery.Current[ServiceDiscoveryKey.CostSearchRunner];

            if (travelDate.TicketType != TicketType.Singles) 
            {
                CostSearchTicket outwardTicket = costBasedPageState.SelectedSingleOrReturnTicket.CostSearchTickets[0];
                searchRunner.ValidateAndRunServices(outwardTicket);
            } 
            else 
            {
                CostSearchTicket outwardTicket = costBasedPageState.SelectedOutwardTicket.CostSearchTickets[0];
                CostSearchTicket inwardTicket = costBasedPageState.SelectedInwardTicket.CostSearchTickets[0];
                searchRunner.ValidateAndRunServices(outwardTicket,inwardTicket);
            }

            // Clear the deferred data.
            tdSessionManager.ClearDeferredData();
			
            // Refresh the pageState object directly from the sessionManager and return
            // the searchaWaitStateData object directly from the session manager.
            pageState = (FindCostBasedPageState)tdSessionManager.FindPageState;

            return tdSessionManager.AsyncCallState.Status;

        }

        /// <summary>
		/// Sets up the supplied date control. Hides clock help, sets time controls
		/// to invisible, shows flexibility controls and populates the control. Affects
		/// both Leave and Return date controls.
		/// </summary>
		/// <param name="dateControl">Supplied date control to be set up</param>
		public void SetupDateControl(FindLeaveReturnDatesControl dateControl)
		{
			//Make time controls invisible and flexibility controls visible.
			dateControl.LeaveDateControl.FlexibilityControlsVisible = true;
			dateControl.LeaveDateControl.TimeControlsVisible = false;

			dateControl.ReturnDateControl.FlexibilityControlsVisible = true;
			dateControl.ReturnDateControl.TimeControlsVisible = false;
		}

        /// <summary>
        /// Tests the cost search results object in page state for any error messages
        /// returned by the cost search runner component. If present, hides the 
        /// instructionPanel, makes the messagePanel visible and displays the error messages
        /// in messageslabel.
		/// Messages are retrieved from langStrings, rather than LocalResourceManager
		/// </summary>
        /// <param name="instructionPanel">Panel containing instructional text</param>
        /// <param name="messagePanel">Panel containing message text</param>
        /// <param name="messageslabel">Label displaying message text</param>
		public void DisplayMessages(Panel instructionPanel, Panel messagePanel, Label messagesLabel)
        {
            CostSearchError[] errors = ((FindCostBasedPageState)pageState).SearchResult.GetErrors();

			if (errors.Length != 0)
			{
				instructionPanel.Visible = false;
				DisplayMessageText(messagePanel, messagesLabel, errors);
			}
		}

		/// <summary>
		/// Tests the cost search results object in page state for any error messages
		/// returned by the cost search runner component. If present makes the
		/// messagePanel visible and displays the error messages in messageslabel.
		/// This does not hide any other Information panel that may be present.
		/// Messages are retrieved from langStrings, rather than LocalResourceManager
		/// </summary>
		/// <param name="messagePanel">Panel containing message text</param>
		/// <param name="messageslabel">Label displaying message text</param>
		public void DisplayMessages(Panel messagePanel, Label messagesLabel)
		{
			CostSearchError[] errors = ((FindCostBasedPageState)pageState).SearchResult.GetErrors();

			if (errors.Length != 0)
			{
				DisplayMessageText(messagePanel, messagesLabel, errors);
			}
		}

		/// <summary>
		/// Display any error messages returned by the cost search runner component.
		/// Local implementation of the processing that is common to the public DisplayMessages() methods
		/// </summary>
		/// <param name="messagePanel">Panel containing message text</param>
		/// <param name="messageslabel">Label displaying message text</param>
		/// <param name="errors">Array of errors that has been generated by the query</param>
		private void DisplayMessageText(Panel messagePanel, Label messagesLabel, CostSearchError[] errors)
		{
			string messageText;
			bool multipleErrors = (errors.Length > 1);

			messagePanel.Visible = true;
			messagesLabel.Text = string.Empty;

			foreach(CostSearchError error in errors)
			{
                if (string.Equals(error.ResourceID, constNoFareAvailable))
                {  
                    // if its the fare not available message, then need to insert the selected fare
                    messageText = String.Format(Global.tdResourceManager.GetString(error.ResourceID, TDCultureInfo.CurrentUICulture), FormattedSelectedFare());
                }
                else
                {
				    messageText = Global.tdResourceManager.GetString(error.ResourceID, TDCultureInfo.CurrentUICulture);
                }

				if (messageText != null)
				{
					messagesLabel.Text += messageText;

					if (multipleErrors) 
					{
						messagesLabel.Text += "<br>";
					}
				}
			}
		}

        /// <summary>
        /// Returns the selected ticket(s) as a string, e.g. "£10.00 Standard Advance"
        /// </summary>
        /// <returns></returns>
        private string FormattedSelectedFare()
        {
            FindCostBasedPageState costBasedPageState = (FindCostBasedPageState)pageState;

            TravelDate travelDate = costBasedPageState.SelectedTravelDate.TravelDate;

            StringBuilder formattedFare = new StringBuilder();

            bool discountedFare = (costBasedPageState.SearchRequest.RailDiscountedCard != string.Empty);

            if (travelDate.TicketType != TicketType.Singles)
            {
                DisplayableCostSearchTicket outwardDisplayableTicket = costBasedPageState.SelectedSingleOrReturnTicket;

                string ticketName = FormatTicketNames(outwardDisplayableTicket.CostSearchTickets);
                string ticketCost = FormatFare(outwardDisplayableTicket, discountedFare);

                formattedFare.Append(ticketCost);
                formattedFare.Append(" ");
                formattedFare.Append(ticketName);
            }
            else
            {
                DisplayableCostSearchTicket outwardDisplayableTicket = costBasedPageState.SelectedOutwardTicket;
                DisplayableCostSearchTicket inwardDisplayableTicket = costBasedPageState.SelectedInwardTicket;

                string outwardTicketName = FormatTicketNames(outwardDisplayableTicket.CostSearchTickets);
                string returnTicketName = FormatTicketNames(inwardDisplayableTicket.CostSearchTickets);

                string outwardTicketCost = FormatFare(outwardDisplayableTicket, discountedFare);
                string returnTicketCost = FormatFare(inwardDisplayableTicket, discountedFare);

                formattedFare.Append(outwardTicketCost);
                formattedFare.Append(" ");
                formattedFare.Append(outwardTicketName);
                formattedFare.Append(", ");
                formattedFare.Append(returnTicketCost);
                formattedFare.Append(" ");
                formattedFare.Append(returnTicketName);
            }

            return formattedFare.ToString();
        }

        /// <summary>
        /// Build up the names of all the required tickets (per direction of travel) into a single string
        /// </summary>
        /// <param name="tickets">CostSearchTicket array of the tickets required for this direction of travel</param>
        /// <returns>String of formatted ticket names</returns>
        private string FormatTicketNames(CostSearchTicket[] tickets)
        {
            StringBuilder ticketLabel = new StringBuilder();
            for (int i = 0; i < tickets.Length; i++)
            {
                ticketLabel.Append(tickets[i].Code);

                if (i != (tickets.Length - 1))
                    ticketLabel.Append(", ");
            }

            return ticketLabel.ToString();
        }

        /// <summary>
        /// Format the appropriate fare depending upon whether discount fare is required
        /// </summary>
        /// <param name="ticket">DisplayableCostSearchTicket that contains fare data</param>
        /// <param name="discount">Is discount fare required?</param>
        /// <returns>String containing formatted fare</returns>
        private string FormatFare(DisplayableCostSearchTicket ticket, bool discount)
        {
            if (discount && (ticket.DiscountedFare != String.Empty))
            {
                return "&pound;" + ticket.DiscountedFare;
            }
            else
            {
                return "&pound;" + ticket.Fare;
            }
        }
	}
}
