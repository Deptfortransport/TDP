// *********************************************** 
// NAME                 : AmendSaveSendAmendControl.ascx.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 20/08/2003 
// DESCRIPTION			: Amend pane for the
// AmendSaveSend control.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AmendSaveSendAmendControl.ascx.cs-arc  $
//
//   Rev 1.8   Sep 19 2011 10:50:46   mmodi
//Clear Avoid toids from journey parameters as they should not influence journey when amending using this control
//Resolution for 5737: CCN 0548 - Real Time Information in Car issues
//
//   Rev 1.7   Mar 09 2010 11:59:38   MTurner
//Dropped 'anytime' from the list of hour values used by cycle planning.
//Resolution for 5443: AmendSaveSendControl for Cycle contains 'anytime'
//
//   Rev 1.6   Feb 05 2010 15:55:20   mturner
//Added code to clear out journey results regardless when a new journey is planned.
//Resolution for 5385: Amend Save send control does not clear journeys from session
//
//   Rev 1.5   Feb 13 2009 14:25:28   apatel
//Javascript functionality added to day, month and hour return date time select dropdowns to work same as day, month and hour dropdowns on input pages.
//Resolution for 5249: Make the Amend return datetime control same as Input pages
//
//   Rev 1.4   Oct 13 2008 16:41:34   build
//Automatically merged from branch for stream5014
//
//   Rev 1.3.1.1   Sep 15 2008 10:49:50   mmodi
//Updated for xhtml compliance
//
//   Rev 1.3.1.0   Jun 20 2008 14:39:42   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   May 07 2008 10:15:42   mmodi
//Updated to return user to Details screen
//Resolution for 4951: Amend journey toolbar returns user to Summary screen
//
//   Rev 1.2   Mar 31 2008 13:19:12   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:04   mturner
//Initial revision.
//
//   Rev DevFactory Jan 20 2008 19:00:00 dgath
//CCN0382b City to City enhancements: On OK button click, check FindAMode, if in Trunk mode return user to the  Journey Overview page
//
//   Rev 1.50   May 16 2006 16:30:48   CRees
//Hid return date control for pre-Del 8.1 park and ride issues.
//
//   Rev 1.49   Apr 28 2006 16:57:40   rwilby
//Code fixes for IR4021, IR4022 and IR4023. See comments in code (pre-fixed with IR number) for more details.
//Resolution for 4021: DN062 Amend Tool: Date/time error converting single journey to open return
//
//   Rev 1.48   Apr 26 2006 11:43:46   esevern
//Added check for journey parameters being TDJourneyParametersMulti before doing check for 'IsOpenReturn' since this control is also used on FindAFlight (which uses TDJourneyParametersFlight)
//Resolution for 3897: DN062 Amend Tool: 'No return' shown on Amend tool when open return fares displayed in Find A Fare
//
//   Rev 1.47   Apr 24 2006 17:24:24   esevern
//added check for 'open return' selected for journey request - if so have this as the selected option in the drop down and don't add 'no return' to list
//Resolution for 3897: DN062 Amend Tool: 'No return' shown on Amend tool when open return fares displayed in Find A Fare
//
//   Rev 1.46   Apr 05 2006 15:23:50   esevern
//Manual merge of stream0030
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.45   Apr 05 2006 11:44:46   kjosling
//Fix.
//Resolution for 3781: DN062 Amend Tool: Default '-' option appears twice on results page
//
//   Rev 1.44   Mar 24 2006 17:28:32   kjosling
//Merged stream 0023
//
//   Rev 1.43.1.2   Mar 20 2006 16:07:10   kjosling
//Updated in response to code review comments
//
//   Rev 1.43.1.1   Mar 14 2006 11:56:32   kjosling
//Changed resource dependencies
//
//   Rev 1.43.1.0   Mar 13 2006 10:14:24   kjosling
//Added support for single to return journeys
//Resolution for 23: DEL 8.1 Workstream - Journey Results - Phase 1
//
//   Rev 1.43   Feb 23 2006 16:10:32   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.42   Feb 10 2006 15:04:42   build
//Automatically merged from branch for stream3180
//
//   Rev 1.41.1.1   Jan 09 2006 16:13:46   RGriffith
//Changes made in light of code review comments
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.41.1.0   Dec 22 2005 14:27:46   tmollart
//Removed method to call to now redundant SaveCurrentFindaMode.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.41   Nov 09 2005 16:58:46   jgeorge
//Manual merge for stream2818 (Search by Price)
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.40   Nov 08 2005 18:04:30   AViitanen
//HTML button changes
//
//   Rev 1.39   Nov 03 2005 17:02:22   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.38.2.0   Oct 20 2005 11:23:14   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.38   Mar 01 2005 16:25:46   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.37   Feb 11 2005 16:33:18   RScott
//Vantive 3586879 fix - added TDSessionManager.Current.SaveCurrentFindAMode();
//
//   Rev 1.36   Sep 29 2004 15:44:08   esevern
//amended TDCultureInfo to CurrentUICulture when setting entries for leaving and return hours (to include 'Anytime')
//
//   Rev 1.35   Sep 21 2004 16:33:30   jgeorge
//Amended to always populate dropdowns when they are empty.
//Resolution for 1613: Amend date/time fields not populated after journey extensions removed
//
//   Rev 1.34   Aug 31 2004 10:34:04   passuied
//Set Ambiguity Mode when clicking Amend DateTime Ok button + Moved Date Ambiguity Setting before DateControl population
//
//   Rev 1.33   Aug 18 2004 16:13:48   passuied
//Fixed error when any time selected. Compare .Value to "Any". Not the .Text.
//Fixed Redirection.
//Resolution for 1371: Find A Train. Can't select AnyTime in Results page - Amend Date
//
//   Rev 1.32   Aug 18 2004 14:19:22   passuied
//Use of non duplicated code to Get transitionevent from FindAMode mode
//
//   Rev 1.31   Aug 18 2004 10:16:52   passuied
//removed AnyTime option from control for car.
//
//   Rev 1.30   Jul 22 2004 11:13:30   jgeorge
//Updates for Find a (Del 6.1)
//
//   Rev 1.29   Jun 17 2004 11:10:16   jgeorge
//Changed to allow "Any time" for flights
//
//   Rev 1.28   Jun 08 2004 12:53:06   JHaydock
//Update to AmendSaveSendControl for Find A Flight plus formatting changes for Welsh display
//
//   Rev 1.27   May 26 2004 09:16:52   jgeorge
//Update for TDJourneyParameters changes
//
//   Rev 1.26   Mar 11 2004 12:55:30   AWindley
//DEL5.2 Added label text for Screen Reader
//
//   Rev 1.25   Feb 19 2004 17:34:58   asinclair
//Changed ref for ok button on Change Dates screen Del 5.2
//
//   Rev 1.24   Dec 30 2003 10:18:00   passuied
//formatted dropdownlist so they're populated with 2 digits numbers
//Resolution for 570: Single digit dates in ranges should be zero filled to be consistent with JP input page.
//
//   Rev 1.23   Nov 26 2003 11:37:24   passuied
//retrieved channel language to pass to ValidateAndRun
//Resolution for 397: Wrong language passed to JW
//
//   Rev 1.22   Nov 24 2003 13:43:18   kcheung
//Population of 6 months ahead just like the input page.
//
//   Rev 1.21   Nov 24 2003 11:14:56   passuied
//changed control population to use a common separator retrieved from propertyservice
//
//   Rev 1.20   Nov 18 2003 14:56:06   CHosegood
//Removed width from OK button
//
//   Rev 1.19   Nov 04 2003 16:12:34   kcheung
//Fixed bug where exception is thrown if "open return" is selected.
//
//   Rev 1.18   Oct 29 2003 12:30:08   kcheung
//Correct minutes so that steps are in 5 mins.  Arrive Before/Leave After bug was previously fixed.
//
//   Rev 1.17   Oct 28 2003 12:26:14   kcheung
//Fixed for QA
//
//   Rev 1.16   Oct 22 2003 13:35:30   kcheung
//Fixed bug where drop down list for Arrive Before/Depart After defaults to Arrive Before because it uses the value to compare instead of text.
//
//   Rev 1.15   Oct 22 2003 12:20:12   RPhilpott
//Improve CJP error handling
//
//   Rev 1.14   Oct 20 2003 18:28:32   kcheung
//Fixed bug where constraint drop down is not correctly initialised to the journey request constraint. Also added viewstate code to keep the current page id.
//
//   Rev 1.13   Oct 17 2003 16:39:44   kcheung
//Fixed for FXCOP
//
//   Rev 1.12   Oct 17 2003 16:20:30   kcheung
//Fixed after FXCop comments
//
//   Rev 1.11   Oct 14 2003 14:05:04   kcheung
//Fixed help labels
//
//   Rev 1.10   Oct 10 2003 13:19:08   kcheung
//Updated Alternate text
//
//   Rev 1.9   Sep 25 2003 17:39:46   kcheung
//Added help text
//
//   Rev 1.8   Sep 25 2003 16:39:06   kcheung
//Integrated stylesheet stuff
//
//   Rev 1.7   Sep 21 2003 14:49:40   kcheung
//Removed all date validation stuff because this is all handled by JourneyPlannerRunner
//
//   Rev 1.6   Sep 08 2003 16:52:52   kcheung
//Updated error messages
//
//   Rev 1.5   Sep 08 2003 15:07:40   kcheung
//Updated 
//
//   Rev 1.4   Aug 20 2003 17:14:10   kcheung
//Added header

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;	
	using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Common;
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.Common.PropertyService.Properties;
	using TransportDirect.Common.Logging;
	using Logger= System.Diagnostics.Trace;
	using TransportDirect.UserPortal.Web.Adapters;
    using TransportDirect.UserPortal.CyclePlannerControl;

	/// <summary>
	///	Amend pane for the AmendSaveSend control.
	/// </summary>
	public partial  class AmendSaveSendAmendControl : TDUserControl
	{



		private TDDateTime initialLeavingDateTime;
		private TDDateTime initialReturningDateTime;


		private int[] years = {2003, 2004, 2005, 2006, 2007, 2008,
								  2009, 2010, 2011, 2012, 2013};

		private string monthYearSeparator = string.Empty;

		private PageId callingPageId = PageId.Empty;

		/// <summary>
		/// Initialises this control.
		/// </summary>
		/// <param name="callingPageId"></param>
		public void Initialise(PageId callingPageId)
		{
			this.callingPageId = callingPageId;
		}
		/// <summary>
		/// Hides the return date selection
		/// </summary>
		public void HideReturnDate()
		{
			//added CR 16/5/06 to allow Park and Ride to hide return options.
			labelReturning.Visible = false;
			dropDownListReturningDate.Visible = false;
			dropDownListLeavingTimeConstraint.Visible = false;
			dropDownListReturningMonthYear.Visible = false;
			dropDownListReturningTimeConstraint.Visible = false;
			dropDownListReturningHour.Visible = false;
			dropDownListReturningMinute.Visible =false;
			labelSRReturningDate.Visible = false;
			labelSRReturningMonthYear.Visible = false;
			labelSRReturningConstraint.Visible = false;
			labelSRReturningHoursPre.Visible = false;
			labelSRReturningHoursPost.Visible = false;
			labelSRReturningMinutesPre.Visible = false;
			labelSRReturningMinutesPost.Visible = false;
		}


		#region ViewState Code
		/// <summary>
		/// Loads the ViewState
		/// </summary>
		protected override void LoadViewState(object savedState) 
		{
			if (savedState != null)
			{
				// Load State from the array of objects that was saved at SavedViewState.
				object[] myState = (object[])savedState;

				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				if (myState[1] != null)
					callingPageId = (PageId)myState[1]; 


			}
		}
	
		/// <summary>
		/// Overrides the base SaveViewState to customise viestate behaviour.
		/// </summary>
		/// <returns>The ViewState object to be saved.</returns>
		protected override object SaveViewState()
		{ 
			// Save State as a cumulative array of objects.
			object baseState = base.SaveViewState();
		
			object[] allStates = new object[2];
			allStates[0] = baseState;
			allStates[1] = callingPageId;

			return allStates;
		}
		#endregion

		#region Page Load method

		/// <summary>
		/// Page Load method - initialises all drop down lists.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Do nothing if Original Journey Request is null.
            ITDJourneyRequest originalJourneyRequest = TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest;
            ITDCyclePlannerRequest originalCycleRequest = TDSessionManager.Current.CycleRequest;

            bool isCycleMode = (TDSessionManager.Current.FindAMode == FindAMode.Cycle);

            // Cycle mode and no request
            if (isCycleMode && originalCycleRequest == null)
                return;
            // All other modes and no request
            else if (!isCycleMode && originalJourneyRequest == null)
                return;


			// Initialise the OK button text
			buttonOK.Text = GetResource("AmendSaveSendAmendControl.buttonOK.Text");

			// Get the leaving date time from TDSessionManager
			initialLeavingDateTime = (isCycleMode ? originalCycleRequest.OutwardDateTime[0] : originalJourneyRequest.OutwardDateTime[0]);

			// Check to see if a return journey was requested
            bool isReturnRequired = (isCycleMode ? originalCycleRequest.IsReturnRequired : originalJourneyRequest.IsReturnRequired);
			if(isReturnRequired)
				initialReturningDateTime = (isCycleMode ? originalCycleRequest.ReturnDateTime[0] : originalJourneyRequest.ReturnDateTime[0]);

            int indexdropDownListLeavingDate = dropDownListLeavingDate.SelectedIndex;
            int indexdropDownListLeavingHour = dropDownListLeavingHour.SelectedIndex;
            int indexdropDownListLeavingMinute = dropDownListLeavingMinute.SelectedIndex;
            int indexdropDownListLeavingMonthYear = dropDownListLeavingMonthYear.SelectedIndex;
            int indexdropDownListLeavingTimeConstraint = dropDownListLeavingTimeConstraint.SelectedIndex;
            int indexdropDownListReturningDate = dropDownListReturningDate.SelectedIndex;
            int indexdropDownListReturningHour = dropDownListReturningHour.SelectedIndex;
            int indexdropDownListReturningMinute = dropDownListReturningMinute.SelectedIndex;
            int indexdropDownListReturningMonthYear = dropDownListReturningMonthYear.SelectedIndex;
            int indexdropDownListReturningTimeConstraint = dropDownListReturningTimeConstraint.SelectedIndex;

            // clearing dropdown lists to prevent duplicates on postback
            dropDownListLeavingDate.Items.Clear();
            dropDownListLeavingHour.Items.Clear();
            dropDownListLeavingMinute.Items.Clear();
            dropDownListLeavingMonthYear.Items.Clear();
            dropDownListLeavingTimeConstraint.Items.Clear();
            dropDownListReturningDate.Items.Clear();
            dropDownListReturningHour.Items.Clear();
            dropDownListReturningMinute.Items.Clear();
            dropDownListReturningMonthYear.Items.Clear();
            dropDownListReturningTimeConstraint.Items.Clear();

			// If any of the dropdowns are empty, we need to reload their data.
            //if(dropDownListLeavingDate.Items.Count == 0)
            //{
				labelLeaving.Text = Global.tdResourceManager.GetString
					("AmendSaveSendAmend.labelLeaving", TDCultureInfo.CurrentUICulture);

				labelReturning.Text = Global.tdResourceManager.GetString
					("AmendSaveSendAmend.labelReturning", TDCultureInfo.CurrentUICulture);

				#region Populate the drop down lists with data

				// populate date drop downs
				for(int date=1; date<32; date++)
				{
					dropDownListLeavingDate.Items.Add(date.ToString("D2",TDCultureInfo.CurrentCulture.NumberFormat));
					dropDownListReturningDate.Items.Add(date.ToString("D2",TDCultureInfo.CurrentCulture.NumberFormat));
				}

				// Populate the "arrive before/depart after" drop down list from DataServices
				DataServices populator = (DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

				// Populate the list control using Data Services.
				populator.LoadListControl(DataServiceType.LeaveArriveDrop, dropDownListLeavingTimeConstraint);
				populator.LoadListControl(DataServiceType.LeaveArriveDrop, dropDownListReturningTimeConstraint);

				// Get the MonthYear common separator 
				
				monthYearSeparator = Properties.Current["journeyplanner.monthyearseparator"];
				if (monthYearSeparator == null || monthYearSeparator.Length == 0)
				{
					OperationalEvent oe = new OperationalEvent(
						TDEventCategory.Infrastructure,
						TDTraceLevel.Error,
						"Missing Property : 'journeyplanner.monthyearseparator'");

					Logger.Write(oe);

					throw new TDException("Missing Property : 'journeyplanner.monthyearseparator'",
						true,
						TDExceptionIdentifier.PSMissingProperty);

				}

				// month-year drop down list population
				Populate(dropDownListLeavingMonthYear);
				Populate(dropDownListReturningMonthYear);

				// hour drop down lists.
				for(int hours=0; hours<24; hours++)
				{
					string toAdd = String.Empty;

					

					toAdd = hours.ToString("D2",TDCultureInfo.CurrentCulture.NumberFormat);

					// add to the drop down lists
					dropDownListLeavingHour.Items.Add(toAdd);
					dropDownListReturningHour.Items.Add(toAdd);
				}

				// minutes drop down lists
				for(int minutes=0; minutes<60; minutes+=5)
				{
					string toAdd = String.Empty;

					

					toAdd = minutes.ToString("D2",TDCultureInfo.CurrentCulture.NumberFormat);

					dropDownListLeavingMinute.Items.Add(toAdd);
					dropDownListReturningMinute.Items.Add(toAdd);
				}
				
				// If original journey params are for flight, train, coach, trunk, add "any time" option
                if (TDSessionManager.Current.IsFindAMode && TDSessionManager.Current.FindAMode != FindAMode.Car && TDSessionManager.Current.FindAMode != FindAMode.Cycle)
				{
					dropDownListLeavingHour.Items.Add(new ListItem(Global.tdResourceManager.GetString("FindFlightInput.DataSelectControl.AnyTime", TDCultureInfo.CurrentUICulture), "Any"));
					dropDownListReturningHour.Items.Add(new ListItem(Global.tdResourceManager.GetString("FindFlightInput.DataSelectControl.AnyTime", TDCultureInfo.CurrentUICulture), "Any"));
					dropDownListLeavingMinute.Items.Add(new ListItem("-", String.Empty));
					dropDownListReturningMinute.Items.Add(new ListItem("-", String.Empty));
				}	

				// set selected index of the drop downs to the initial values
				int initialLeavingDate = initialLeavingDateTime.Day;
				int initialLeavingMonth = initialLeavingDateTime.Month;
				int initialLeavingYear = initialLeavingDateTime.Year;

				string monthToAddDigitOutward =
					string.Format(TDCultureInfo.CurrentUICulture,
					"{0:D2}", initialLeavingMonth); 

				// calculate the items to select in the drop downs
				dropDownListLeavingDate.SelectedIndex = (initialLeavingDate-1);
					
				string valueToFind = monthToAddDigitOutward + monthYearSeparator + initialLeavingYear;
				int count = 0;
				bool found = false;
				while(count < dropDownListLeavingMonthYear.Items.Count && !found)
				{
					if(dropDownListLeavingMonthYear.Items[count].Value == valueToFind)
					{
						found = true;
					}
					else
					{
						count ++;
					}
				}
				if(found)
				{
					dropDownListLeavingMonthYear.SelectedIndex = count;
				}
				
				// Set the hour and minutes
                bool isOutwardAnyTime = (isCycleMode ? originalCycleRequest.OutwardAnyTime : TDSessionManager.Current.JourneyRequest.OutwardAnyTime);
				if (isOutwardAnyTime)
				{
					dropDownListLeavingHour.SelectedIndex = dropDownListLeavingHour.Items.Count - 1;
					dropDownListLeavingMinute.SelectedIndex = dropDownListLeavingMinute.Items.Count - 1;
				}
				else
				{
					dropDownListLeavingHour.SelectedIndex = initialLeavingDateTime.Hour;
					dropDownListLeavingMinute.SelectedIndex = initialLeavingDateTime.Minute / 5;
				}

				// Set the initial selection of the "Arrive Before" drop for outward journey.
				bool outwardArriveBefore = (isCycleMode ? originalCycleRequest.OutwardArriveBefore :
					TDSessionManager.Current.JourneyViewState.JourneyLeavingTimeSearchType);

				dropDownListLeavingTimeConstraint.SelectedIndex = outwardArriveBefore ? 1 : 0;

				// Set the return controls only if a (non open-return) return request was made.

                if (isReturnRequired &&
					( TDSessionManager.Current.JourneyParameters is TDJourneyParametersMulti ? !((TDJourneyParametersMulti)TDSessionManager.Current.JourneyParameters).IsOpenReturn : true ) )
				{
					int initialReturningDate = initialReturningDateTime.Day;
					int initialReturningMonth = initialReturningDateTime.Month;
					int initialReturningYear = initialReturningDateTime.Year;

					string monthToAddDigitReturn =
						string.Format(TDCultureInfo.CurrentUICulture,
						"{0:D2}", initialReturningMonth); 

					dropDownListReturningDate.SelectedIndex = (initialReturningDate-1);
				
					valueToFind = monthToAddDigitReturn + monthYearSeparator + initialReturningYear;
					count = 0;
					found = false;
					while(count < dropDownListReturningMonthYear.Items.Count && !found)
					{
						if(dropDownListReturningMonthYear.Items[count].Value == valueToFind)
						{
							found = true;
						}
						else
						{
							count++;
						}
					}
					if(found)
					{
						dropDownListReturningMonthYear.SelectedIndex = count;
					}

					// Set the hour and minutes
                    bool isReturnAnyTime = (isCycleMode ? originalCycleRequest.ReturnAnyTime : TDSessionManager.Current.JourneyRequest.ReturnAnyTime);
					if (isReturnAnyTime)
					{
						dropDownListReturningHour.SelectedIndex = dropDownListReturningHour.Items.Count - 1;
						dropDownListReturningMinute.SelectedIndex = dropDownListReturningMinute.Items.Count - 1;
					}
					else
					{
						dropDownListReturningHour.SelectedIndex = initialReturningDateTime.Hour;
						dropDownListReturningMinute.SelectedIndex = initialReturningDateTime.Minute / 5;
					}
			
					// Set the initial selection of the "Arrive Before" drop for return journey.
					bool returnArriveBefore = (isCycleMode ? originalCycleRequest.ReturnArriveBefore :
						TDSessionManager.Current.JourneyViewState.JourneyReturningTimeSearchType);

					dropDownListReturningTimeConstraint.SelectedIndex = returnArriveBefore ? 1 : 0;
				}
				else
				{
					ListItem item = new ListItem("-",string.Empty);
					dropDownListReturningDate.Items.Insert(0, item);
					
					dropDownListReturningHour.Items.Add(item);			
					dropDownListReturningHour.SelectedIndex = dropDownListReturningHour.Items.Count - 1;
					if(dropDownListReturningMinute.Items.Contains(item))
					{	
						dropDownListReturningMinute.SelectedIndex = dropDownListReturningMinute.Items.Count - 1;
					}
					else
					{
						dropDownListReturningMinute.Items.Insert(0, item);
					}


					//IR4022: Only add the 'Open return' option for door to door and find a fare functionality 
					if (TDSessionManager.Current.FindAMode == FindAMode.None ||
						TDSessionManager.Current.FindAMode == FindAMode.Fare)
					{
						//Add 'open return' option
						item = new ListItem(GetResource("DataServices.ReturnMonthYearDrop.OpenReturn"), "OpenReturn");
						dropDownListReturningMonthYear.Items.Add(item);
					}

					// if its an open return, set this as the default selected value and don't
					// add the 'no return' option (check first its TDJourneyParametersMulti,
					// as it could be TDJourneyParametersFlight)
					if(TDSessionManager.Current.JourneyParameters is TDJourneyParametersMulti)
					{
						if( ((TDJourneyParametersMulti)TDSessionManager.Current.JourneyParameters).IsOpenReturn )
						{
							dropDownListReturningMonthYear.SelectedIndex = dropDownListReturningMonthYear.Items.Count - 1;
						}
						else 
						{
							// add 'no return' option
							item = new ListItem(GetResource("DataServices.ReturnMonthYearDrop.NoReturn"), "NoReturn");
							dropDownListReturningMonthYear.Items.Add(item);
							dropDownListReturningMonthYear.SelectedIndex = dropDownListReturningMonthYear.Items.Count - 1;
						}
					}
					else
					{
						//IR4023: Add "No return" option without the IsOpenReturn check for
						//JourneyParameters types other than TDJourneyParametersMulti.
						item = new ListItem(GetResource("DataServices.ReturnMonthYearDrop.NoReturn"), "NoReturn");
						dropDownListReturningMonthYear.Items.Add(item);
						dropDownListReturningMonthYear.SelectedIndex = dropDownListReturningMonthYear.Items.Count - 1;
					}
				}


				#endregion

                if (Page.IsPostBack)
                {
                    if(!(indexdropDownListLeavingDate < 0))
                    dropDownListLeavingDate.SelectedIndex = indexdropDownListLeavingDate;

                    if(!(indexdropDownListLeavingHour < 0))
                    dropDownListLeavingHour.SelectedIndex = indexdropDownListLeavingHour;

                    if(!(indexdropDownListLeavingMinute < 0))
                    dropDownListLeavingMinute.SelectedIndex = indexdropDownListLeavingMinute;
                    
                    if(!(indexdropDownListLeavingMonthYear < 0))
                    dropDownListLeavingMonthYear.SelectedIndex = indexdropDownListLeavingMonthYear;
                    
                    if(!(indexdropDownListLeavingTimeConstraint < 0))
                    dropDownListLeavingTimeConstraint.SelectedIndex = indexdropDownListLeavingTimeConstraint;
                    
                    if(!(indexdropDownListReturningDate < 0))
                    dropDownListReturningDate.SelectedIndex = indexdropDownListReturningDate;
                    
                    if(!(indexdropDownListReturningHour < 0))
                    dropDownListReturningHour.SelectedIndex = indexdropDownListReturningHour;
                    
                    if(!(indexdropDownListReturningMinute < 0))
                    dropDownListReturningMinute.SelectedIndex = indexdropDownListReturningMinute;
                    
                    if(!(indexdropDownListReturningMonthYear < 0))
                    dropDownListReturningMonthYear.SelectedIndex = indexdropDownListReturningMonthYear;
                    
                    if(!(indexdropDownListReturningTimeConstraint < 0))
                    dropDownListReturningTimeConstraint.SelectedIndex = indexdropDownListReturningTimeConstraint;
                }

            //}
		}

        /// <summary>
        /// Pre Render method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            // Determine if JavaScript is supported
            bool javaScriptSupported = bool.Parse((string)Session[((TDPage)Page).Javascript_Support]);
            string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];
            ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

            if (javaScriptSupported)
            {
                dropDownListReturningDate.Attributes["onchange"] = string.Format("return DaySelectionChanged('{0}')", this.ClientID);
                dropDownListReturningMonthYear.Attributes["onchange"] = string.Format("return MonthSelectionChanged('{0}')", this.ClientID);
                dropDownListReturningHour.Attributes["onchange"] = string.Format("return HoursSelectionChanged('{0}')", this.ClientID);
                Page.ClientScript.RegisterStartupScript(typeof(AmendSaveSendAmendControl), "AmendDateSelectControl", scriptRepository.GetScript("AmendDateSelectControl", javaScriptDom));

            }

            base.OnPreRender(e);
        }

		#endregion

		#region Code to retrive months and years

		/// <summary>
		/// Populates the give month year drop downs
		/// </summary>
		private void Populate(DropDownList list)
		{
			// Get the name for all the months from Properties Service.
			string[] months = Global.tdResourceManager.GetString("DateSelectControl.listMonths", TDCultureInfo.CurrentUICulture).Split(',');
				
			int currentMonth = DateTime.Now.Month-1; //conversion to have months from 0-11
				
			int numberOfMonths ;
			try
			{
				numberOfMonths = Convert.ToInt32(Properties.Current["controls.numberofmonths"], TDCultureInfo.CurrentUICulture.NumberFormat);
			}
				// Catch exception because if it fails for any reason, then
				// it must be handled.
			catch (Exception)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Infrastructure,
					TDTraceLevel.Error,
					"Unset/Bad format 'controls.numberofmonths' property");

				Logger.Write(oe);

				throw new TDException("Unset/Bad format 'controls.numberofmonths' properties", true, TDExceptionIdentifier.BTCInvalidNumberOfMonths);
			}
			for (int i=numberOfMonths-1; i>=0; i--)
			{
				int monthToAdd = (currentMonth+i);
				string strYear;
				if (monthToAdd%12 >= currentMonth ) // if the month is between currentMonth and december --> current year
					strYear = DateTime.Now.Year.ToString(TDCultureInfo.CurrentUICulture.DateTimeFormat);
				else // if the month is between january and currentMonth-1 --> next year
				{
					int nextYear = (DateTime.Now.Year +1);
					strYear = nextYear.ToString(TDCultureInfo.CurrentUICulture.DateTimeFormat);
				}

				string strMonth;
				try
				{
					strMonth = months[(monthToAdd)%12];
				}
				catch (ArgumentOutOfRangeException ex)
				{
					throw new TDException(ex.Message, false, TDExceptionIdentifier.BTCInvalidNumberOfMonths);
				}

				// convert month to be in 1-12 range
				string monthToAddDigit =
					string.Format(TDCultureInfo.CurrentUICulture,
					"{0:D2}",(monthToAdd%12)+1); 
				
				// Create the list item
				ListItem listItemMonthYear = new ListItem(
					strMonth + " " + strYear, 
					monthToAddDigit + monthYearSeparator + strYear);

				// Add the list list item to the list.
				list.Items.Insert(0, listItemMonthYear);
			}
		}

		#endregion
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			

			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);

		}
		#endregion
		
		#region Event Handling Code for OK button

		/// <summary>
		/// Event handler to handle the Amend Submit button click.
		/// </summary>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			// Update Journey Parameters with the new date.
			TDJourneyParameters journeyParameters = TDSessionManager.Current.JourneyParameters;
			FindAMode findMode;
			if (TDSessionManager.Current.FindPageState == null)
				findMode = FindAMode.None;
			else
				findMode = TDSessionManager.Current.FindPageState.Mode;

			journeyParameters.OutwardArriveBefore = dropDownListLeavingTimeConstraint.SelectedIndex == 1;
			journeyParameters.OutwardDayOfMonth = dropDownListLeavingDate.SelectedItem.Text.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
			journeyParameters.OutwardMonthYear = dropDownListLeavingMonthYear.SelectedItem.Value.ToString(TDCultureInfo.CurrentCulture);

			if (findMode != FindAMode.None)
			{
				// Cater for possible "Any time" option
				if (dropDownListLeavingHour.SelectedItem.Value == "Any")
					journeyParameters.OutwardAnyTime = true;
				else
				{
					journeyParameters.OutwardAnyTime = false;
					journeyParameters.OutwardHour = dropDownListLeavingHour.SelectedItem.Text.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
					if (dropDownListLeavingMinute.SelectedItem.Text.Length == 0)
						journeyParameters.OutwardMinute = ((int)0).ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
					else
						journeyParameters.OutwardMinute = dropDownListLeavingMinute.SelectedItem.Text.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
				}
			}	
			else
			{
				journeyParameters.OutwardHour = dropDownListLeavingHour.SelectedItem.Text.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
				journeyParameters.OutwardMinute = dropDownListLeavingMinute.SelectedItem.Text.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
			}

			// Check to see if a return journey was requested before attempting to
			// update Journey Parameters for the return journey.
			//if(originalJourneyRequest.IsReturnRequired)

			//IR4021: Added logic to set the journeyParameters.ReturnMonthYear property to OpenReturn if OpenReturn is selected.
			if(string.Equals(dropDownListReturningMonthYear.SelectedItem.Text, GetResource("DataServices.ReturnMonthYearDrop.OpenReturn")))
			{
				journeyParameters.ReturnMonthYear = ReturnType.OpenReturn.ToString();
			}
			else if(!string.Equals(dropDownListReturningMonthYear.SelectedItem.Text, GetResource("DataServices.ReturnMonthYearDrop.NoReturn")))
			{
				journeyParameters.ReturnArriveBefore = dropDownListReturningTimeConstraint.SelectedIndex == 1;
				journeyParameters.ReturnDayOfMonth = dropDownListReturningDate.SelectedItem.Text.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
				journeyParameters.ReturnMonthYear = dropDownListReturningMonthYear.SelectedItem.Value.ToString(TDCultureInfo.CurrentCulture);

				if (findMode != FindAMode.None)
				{
					// Cater for possible "Any time" option
					if (dropDownListReturningHour.SelectedItem.Value == "Any")
						journeyParameters.ReturnAnyTime = true;
					else
					{
						journeyParameters.ReturnAnyTime = false;
						journeyParameters.ReturnHour = dropDownListReturningHour.SelectedItem.Text.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
						if (dropDownListReturningMinute.SelectedItem.Text.Length == 0)
							journeyParameters.ReturnMinute = ((int)0).ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
						else
							journeyParameters.ReturnMinute = dropDownListReturningMinute.SelectedItem.Text.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
					}
				}
				else
				{
					journeyParameters.ReturnHour = dropDownListReturningHour.SelectedItem.Text.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
					journeyParameters.ReturnMinute = dropDownListReturningMinute.SelectedItem.Text.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
				}
			
			}

            if (journeyParameters is TDJourneyParametersMulti)
            {
                TDJourneyParametersMulti journeyParametersMulti = journeyParameters as TDJourneyParametersMulti;

                // Clear any Avoid toids, as they should not influence a journey being amended using this control
                journeyParametersMulti.ResetToidListToAvoid(true);
                journeyParametersMulti.ResetToidListToAvoid(false);
            }

			// Journey Parameters has been updated. Call JourneyPlannerRunner
			// ValidateAndRun method.

			// Journey Plan Runner
			JourneyPlanRunner.IJourneyPlanRunner runner;
            if (findMode == FindAMode.Flight)
                runner = new JourneyPlanRunner.FlightJourneyPlanRunner(Global.tdResourceManager);
            else if (findMode == FindAMode.Cycle)
                runner = new JourneyPlanRunner.CycleJourneyPlanRunner(Global.tdResourceManager);
            else
                runner = new JourneyPlanRunner.JourneyPlanRunner(Global.tdResourceManager);

			AsyncCallState acs = new JourneyPlanState();
			// Determine refresh interval and resource string for the wait page
			acs.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.AmendDateAndTime"]);
			acs.WaitPageMessageResourceFile = "langStrings";
			acs.WaitPageMessageResourceId = "WaitPageMessage.AmendDateAndTime";

            // In Trunk mode return user to the  Journey Overview page
            if (TDSessionManager.Current.FindAMode == FindAMode.Trunk)
            {
                acs.AmbiguityPage = PageId.JourneyOverview;
                acs.DestinationPage = PageId.JourneyOverview;
                acs.ErrorPage = PageId.JourneyOverview;
            }
            else if (findMode == FindAMode.Cycle)
            {
                acs.AmbiguityPage = PageId.CycleJourneyDetails;
                acs.DestinationPage = PageId.CycleJourneyDetails;
                acs.ErrorPage = PageId.CycleJourneyDetails;
            }
            else
            {
                acs.AmbiguityPage = PageId.JourneyDetails;
                acs.DestinationPage = PageId.JourneyDetails;
                acs.ErrorPage = PageId.JourneyDetails;
            }

			TDSessionManager.Current.AsyncCallState = acs;

            // Invalidate any existing journeys in the session.
            // If we do not do this any failed journey requests result in a page being displayed to the user
            // that has a mixture of the new request and the old results.
            if (findMode == FindAMode.Cycle)
            {
                if (TDSessionManager.Current.CycleResult != null)
                {
                    TDSessionManager.Current.CycleResult.IsValid = false;
                }
            }
            else
            {
                if (TDSessionManager.Current.JourneyResult != null)
                {
                    TDSessionManager.Current.JourneyResult.IsValid = false;
                }
            }

			if (runner.ValidateAndRun(TDSessionManager.Current, TDSessionManager.Current.JourneyParameters, TDPage.GetChannelLanguage(TDPage.SessionChannelName)))
			{
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.WaitingRefresh;
			}
			else
			{
				TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(callingPageId);
				// Redirect user to input page.
				if (TDSessionManager.Current.FindAMode == FindAMode.None)
					TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputErrors;
				else
				{
					TDSessionManager.Current.FindPageState.AmbiguityMode = true;
					TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = FindInputAdapter.GetTransitionEventFromMode(TDSessionManager.Current.FindAMode);
				}
			}
			
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(TransitionEvent.FindAInputRedirectToResults);
		}

		#endregion
	}
}
