// *********************************************** 
// NAME			: FindJourneyInputAdapter.cs
// AUTHOR		: Richard Hopkins
// DATE CREATED	: 10/01/05
// DESCRIPTION	: Responsible for common functionality
// required by Find A Journey input pages.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/FindJourneyInputAdapter.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 12:59:02   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:20   mturner
//Initial revision.
//
//   Rev 1.6   Apr 21 2006 15:08:04   jbroome
//Ensure locations are reset in InitJourneyParameters()
//Resolution for 3920: DN077 Landing Page: Clear page does not reset planner input page
//Resolution for 3960: DN058 Park and Ride Phase 2 - 'Clear' on Find a car not resetting Park and Ride scheme correctly
//
//   Rev 1.5   Mar 23 2006 17:58:36   build
//Automatically merged from branch for stream0025
//
//   Rev 1.4.1.2   Mar 14 2006 10:31:38   halkatib
//Changes made for park and ride phase 2
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.4.1.1   Mar 10 2006 16:46:56   halkatib
//changes for park and ride phase 2
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.4.1.0   Mar 07 2006 11:12:28   halkatib
//Changes made by ParkandRide phase 2
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.4   Feb 23 2006 17:00:18   RWilby
//Merged stream3129
//
//   Rev 1.3   Nov 22 2005 10:49:26   NMoorhouse
//Fix problem with Arriving by field not being persisted if date is modified in Ambiguous mode
//Resolution for 3069: DN77 - Ambiguity pages not holding values
//
//   Rev 1.2   Mar 17 2005 14:07:20   rhopkins
//Move journeyParams from FindInputAdapter
//Resolution for 1927: DEV Code Review: FAF date selection
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.1   Feb 24 2005 11:37:14   PNorell
//Updated for favourite details.
//
//   Rev 1.0   Jan 25 2005 12:14:30   rhopkins
//Initial revision.
//

using System;
using System.Web.UI.WebControls;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;


namespace TransportDirect.UserPortal.Web.Adapters
{

    /// <summary>
    /// Responsible for common functionality required by Find A Journey input pages.
    /// </summary>
    public abstract class FindJourneyInputAdapter : FindInputAdapter
    {
		/// <summary>
		/// Journey parameters for Find A time-based input pages
		/// </summary>
		protected TDJourneyParametersMulti journeyParams;

		/// <summary>
		/// Map page state
		/// </summary>
		protected InputPageState inputPageState;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="journeyParams"></param>
        /// <param name="pageState"></param>


		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="pageState">Page state for Find A Journey input pages</param>
		/// <param name="sessionManager">Current SessionManager containing relevant session state</param>
		/// <param name="inputPageState">Variables indicating state of page, return stack for navigation etc.</param>
		/// <param name="journeyParams">Journey parameters for Find A journey planning</param>
		public FindJourneyInputAdapter(FindPageState pageState, ITDSessionManager sessionManager, InputPageState inputPageState, TDJourneyParametersMulti journeyParams)
			: base(pageState, sessionManager)
		{
            this.journeyParams = journeyParams;
			this.inputPageState = inputPageState;
		}

        #region Protected Methods

		/// <summary>
		/// Template method that returns a station type to be used to filter 
		/// major stations gazetteer searches -- will return StationType.Undetermined  
		/// by default, must be overriden by subclasses to return mode-specific values.
		/// </summary>
		protected virtual StationType GetStationTypeFilter()
		{
			return StationType.Undetermined;
		}


        #endregion Protected Methods

        #region Public Methods

		/// <summary>
		/// Initialises journey parameters with default values for Find A journey planning.
		/// </summary>
		public override void InitJourneyParameters() 
		{
			journeyParams.Initialise();
			journeyParams.DestinationType = new 
			TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);
			journeyParams.OriginType = new 
			TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);

			journeyParams.Destination = new LocationSearch();
			journeyParams.Origin = new LocationSearch();

			// Also Initialise the ValidationError object
			if (tdSessionManager.ValidationError != null)
				tdSessionManager.ValidationError.Initialise();
		}

		/// <summary>
        /// Initialises the locationsControl with journey parameters
        /// </summary>
        /// <param name="locationsControl">The control to initialise</param>
        public override void InitLocationsControl(FindToFromLocationsControl locationsControl)
        {
            locationsControl.OriginLocation = journeyParams.OriginLocation;
            locationsControl.OriginSearch = journeyParams.Origin;
            locationsControl.DestinationLocation = journeyParams.DestinationLocation;
            locationsControl.DestinationSearch = journeyParams.Destination;
            locationsControl.FromLocationControl.LocationControlType = journeyParams.OriginType;
            locationsControl.ToLocationControl.LocationControlType = journeyParams.DestinationType;
        }

		/// <summary>
		/// Initialises the FindLeaveReturnDatesControl with journey parameters.
		/// </summary>
		/// <param name="locationsControl">The control to initialise</param>
		public override void InitDateControl(FindLeaveReturnDatesControl dateControl)
		{
			dateControl.LeaveDateControl.AmbiguityMode = pageState.AmbiguityMode;
			dateControl.ReturnDateControl.AmbiguityMode = pageState.AmbiguityMode;
			dateControl.LeaveDateControl.DateErrors = tdSessionManager.ValidationError;
			dateControl.ReturnDateControl.DateErrors = tdSessionManager.ValidationError;

			dateControl.LeaveDateControl.Populate(
				journeyParams.OutwardHour,
				journeyParams.OutwardMinute,
				journeyParams.OutwardDayOfMonth,
				journeyParams.OutwardMonthYear,
				journeyParams.OutwardArriveBefore);

			dateControl.ReturnDateControl.Populate(
				journeyParams.ReturnHour,
				journeyParams.ReturnMinute,
				journeyParams.ReturnDayOfMonth,
				journeyParams.ReturnMonthYear,
				journeyParams.ReturnArriveBefore);
		}

		/// <summary>
		/// Initialises the dateControl with journey parameters
		/// </summary>
		/// <param name="dateControl">The control to initialise</param>
		public override void UpdateDateControl(FindLeaveReturnDatesControl dateControl)
		{
			dateControl.LeaveDateControl.Populate(
				journeyParams.OutwardHour,
				journeyParams.OutwardMinute,
				journeyParams.OutwardDayOfMonth,
				journeyParams.OutwardMonthYear,
				journeyParams.OutwardArriveBefore);

			dateControl.ReturnDateControl.Populate(
				journeyParams.ReturnHour,
				journeyParams.ReturnMinute,
				journeyParams.ReturnDayOfMonth,
				journeyParams.ReturnMonthYear,
				journeyParams.ReturnArriveBefore);
		}

		/// <summary>
		/// Updates the journey parameters with the values of dateControl
		/// </summary>
		/// <param name="dateControl">The control supplying the values</param>
		public override void UpdateJourneyDates(FindLeaveReturnDatesControl dateControl)
		{
			dateControl.LeaveDateControl.AmbiguityMode = pageState.AmbiguityMode;
			dateControl.ReturnDateControl.AmbiguityMode = pageState.AmbiguityMode;
			dateControl.LeaveDateControl.DateErrors = tdSessionManager.ValidationError;
			dateControl.ReturnDateControl.DateErrors = tdSessionManager.ValidationError;
            
			if (dateControl.LeaveDateControl.DateControl.DisplayMode != GenericDisplayMode.ReadOnly)
			{
				journeyParams.OutwardDayOfMonth = dateControl.LeaveDateControl.DateControl.Day;
				journeyParams.OutwardMonthYear = dateControl.LeaveDateControl.DateControl.MonthYear;
				journeyParams.OutwardMinute = dateControl.LeaveDateControl.DateControl.Minute;
				journeyParams.OutwardHour = dateControl.LeaveDateControl.DateControl.Hour;
				journeyParams.OutwardAnyTime = dateControl.LeaveDateControl.DateControl.IsAnyTimeSelected; 
				if (dateControl.LeaveDateControl.DateControl.DisplayMode != GenericDisplayMode.Ambiguity)
				{
					journeyParams.OutwardArriveBefore = dateControl.LeaveDateControl.DateControl.ArriveBefore;
				}
			}

			if (dateControl.ReturnDateControl.DateControl.DisplayMode != GenericDisplayMode.ReadOnly) 
			{
				journeyParams.ReturnDayOfMonth = dateControl.ReturnDateControl.DateControl.Day;
				journeyParams.ReturnMonthYear = dateControl.ReturnDateControl.DateControl.MonthYear;
				journeyParams.ReturnMinute = dateControl.ReturnDateControl.DateControl.Minute;
				journeyParams.ReturnHour = dateControl.ReturnDateControl.DateControl.Hour;
				journeyParams.ReturnAnyTime = dateControl.ReturnDateControl.DateControl.IsAnyTimeSelected;
				journeyParams.IsReturnRequired = !dateControl.ReturnDateControl.DateControl.IsNoReturnSelected;
				if (dateControl.ReturnDateControl.DateControl.DisplayMode != GenericDisplayMode.Ambiguity) 
				{
					journeyParams.ReturnArriveBefore = dateControl.ReturnDateControl.DateControl.ArriveBefore;
				}
			}
		}

		/// <summary>
        /// Returns true if the journey parameters for from, to and public via location searches are 
        /// currently set to their highest level of hierarchy.
        /// </summary>
        /// <returns>True if all at highest level, false otherwise</returns>
        public virtual bool IsAtHighestLevel() 
        {
            return !(
                (journeyParams.OriginLocation.Status == TDLocationStatus.Ambiguous && 
                journeyParams.Origin.CurrentLevel > 0) ||
                (journeyParams.DestinationLocation.Status == TDLocationStatus.Ambiguous && 
                journeyParams.Destination.CurrentLevel > 0) ||
                (journeyParams.PublicViaLocation.Status == TDLocationStatus.Ambiguous && 
                journeyParams.PublicVia.CurrentLevel > 0) ||
				(journeyParams.PrivateViaLocation.Status == TDLocationStatus.Ambiguous && 
				journeyParams.PrivateVia.CurrentLevel > 0));
        }

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


		public virtual void MapSearch(string inputText, SearchType searchType, bool fuzzy)
		{
			inputPageState.MapLocationSearch.ClearAll();
			inputPageState.MapLocation.Status = TDLocationStatus.Unspecified;
			inputPageState.MapLocationSearch.SearchType = searchType;

			LocationSearch thisSearch = inputPageState.MapLocationSearch;
			TDLocation thisLocation = inputPageState.MapLocation;

			LocationSearchHelper.SetupLocationParameters(
				inputText,
				searchType,
				fuzzy,
				0,
				journeyParams.MaxWalkingTime,
				journeyParams.WalkingSpeed,
				ref thisSearch,
				ref thisLocation,
				true,
				true,
				GetStationTypeFilter()
				);
            
			inputPageState.MapLocationSearch = thisSearch;
			inputPageState.MapLocation = thisLocation;
		}

        /// <summary>
        /// Returns true if supplied ValidationError object contains errors indicating
        /// ambiguity for any location (to, from, public via or private via).
        /// </summary>
        /// <param name="errors">errors to search</param>
        /// <returns>True if errors indicating ambiguity for any location, false otherwise</returns>
        public virtual bool AreLocationsAmbiguous(ValidationError errors)
        {
            return
                errors.Contains(ValidationErrorID.AmbiguousOriginLocation) ||
                errors.Contains(ValidationErrorID.AmbiguousDestinationLocation) ||
                errors.Contains(ValidationErrorID.AmbiguousPublicViaLocation) ||
                errors.Contains(ValidationErrorID.AmbiguousPrivateViaLocation);
        }

        /// <summary>
        /// Returns true if supplied ValidationError object contains errors indicating
        /// erroneous results for any location (to, from, public via or private via).
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
                errors.Contains(ValidationErrorID.PrivateViaLocationInvalid) ||
                errors.Contains(ValidationErrorID.PrivateViaLocationInvalidAndOtherErrors) ||
                errors.Contains(ValidationErrorID.PublicViaLocationInvalid) ||
                errors.Contains(ValidationErrorID.PublicViaLocationInvalidAndOtherErrors);
        }

		/// <summary>
		/// Returns true if supplied ValidationError object contains errors indicating
		/// erroneous results for any location (to, from, public via or private via).
		/// </summary>
		/// <param name="errors">errors to search</param>
		/// <returns>True if errors indicate erroneous results for any location, false otherwise</returns>
		public virtual bool AreOriginAndViaLocationsOverlapping(ValidationError errors)
		{
            return (errors.Contains(ValidationErrorID.OriginAndViaOverlap));
					
		}

		/// <summary>
		/// Returns true if supplied ValidationError object contains errors indicating
		/// erroneous results for any location (to, from, public via or private via).
		/// </summary>
		/// <param name="errors">errors to search</param>
		/// <returns>True if errors indicate erroneous results for any location, false otherwise</returns>
		public virtual bool AreDestinationAndViaLocationsOverlapping(ValidationError errors)
		{							
			return (errors.Contains(ValidationErrorID.DestinationAndViaOverlap));							
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

                // Display "select from list..." if origin or destination locations are
                // ambiguous
                if(AreLocationsAmbiguous(errors)) 
                {
                    labelErrorMessages.Text = Global.tdResourceManager.GetString(
                        "ValidateAndRun.SelectFromList", TDCultureInfo.CurrentUICulture) + " ";
                }

                // Display "Select/type in a new location" if origin or destination locations
                // have not been specified or cannot be resolved
                if(AreLocationsUnspecified(errors))
                {
                    labelErrorMessages.Text += Global.tdResourceManager.GetString(
                        LocationsUnspecifiedKey, TDCultureInfo.CurrentUICulture) + " ";
                }

				// IR1527 - Display "The origin and destination locations you have chosen overlap..." 
				//if origin and destination locations overlap i.e. share the same naptans 				
				if(AreOriginAndDestinationLocationsOverlapping(errors)) 
				{
					//get resource id of the error from the errors hashtable					
					errors.MsgResourceID = errors.MessageIDs[ValidationErrorID.OriginAndDestinationOverlap].ToString();

					labelErrorMessages.Text = Global.tdResourceManager.GetString(
						errors.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ";
					showNextText = false;
				}
				
				// IR1527 - Display "The origin and via locations you have chosen overlap..." 
				//if origin and via locations overlap i.e. share the same naptans 				
				if(AreOriginAndViaLocationsOverlapping(errors)) 
				{
					//set resource id of the error from the errors hashtable					
					errors.MsgResourceID = errors.MessageIDs[ValidationErrorID.OriginAndViaOverlap].ToString();

					labelErrorMessages.Text = Global.tdResourceManager.GetString(
						errors.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ";
					showNextText = false;
				}
				
				// IR1527 - Display "The destination and via locations you have chosen overlap..." 
				//if destination and via locations overlap i.e. share the same naptans 				
				if(AreDestinationAndViaLocationsOverlapping(errors))  
				{
					//set resource id of the error from the errors hashtable					
					errors.MsgResourceID = errors.MessageIDs[ValidationErrorID.DestinationAndViaOverlap].ToString();

					labelErrorMessages.Text = Global.tdResourceManager.GetString(
						errors.MsgResourceID, TDCultureInfo.CurrentUICulture) + " ";
					showNextText = false;
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

        #endregion Public Methods
    }
}
