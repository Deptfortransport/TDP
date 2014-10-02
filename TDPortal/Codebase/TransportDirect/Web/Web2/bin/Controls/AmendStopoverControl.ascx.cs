// *********************************************** 
// NAME                 : AmendStopoverControl.ascx.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 05/05/2004
// DESCRIPTION          : Custom control for amendments 
//						  to stopover time
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AmendStopoverControl.ascx.cs-arc  $ 
//
//   Rev 1.4   Jan 08 2009 14:57:12   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Apr 08 2008 14:35:08   apatel
//stop over dropdown populate method updated to make dropdown retain selectedIndex values on postback
//Resolution for 4848: Stop over time on ExtendJourneyInput resets to 00 every time
//
//   Rev 1.2   Mar 31 2008 13:19:22   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:18   mturner
//Initial revision.
//
//   Rev 1.26   Apr 28 2006 12:23:46   asinclair
//Added check for ExtendInput page
//Resolution for 3962: DN068: Extend: Ambiguiy / Error messages on Extend Input page
//
//   Rev 1.25   Apr 05 2006 15:42:52   build
//Automatically merged from branch for stream0030
//
//   Rev 1.24.1.0   Mar 29 2006 11:15:28   RGriffith
//Wait Page Ehancement changes
//Resolution for 33: DEL 8.1 Workstream: Wait Pages
//
//   Rev 1.24   Mar 13 2006 16:38:14   asinclair
//Merge for stream3353 Extend, Adjust and Replan
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.23   Feb 23 2006 19:16:22   build
//Automatically merged from branch for stream3129
//
//   Rev 1.22.1.0   Jan 10 2006 15:23:32   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.22   Nov 09 2005 16:58:46   jgeorge
//Manual merge for stream2818 (Search by Price)
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.21   Nov 07 2005 12:03:52   ralonso
//Problem fixed with ExtraWiring subroutine
//
//   Rev 1.20   Nov 04 2005 14:26:50   ECHAN
//Merge of stream2816 for DEL8
//
//   Rev 1.19   Nov 01 2005 15:12:12   build
//Automatically merged from branch for stream2638
//
//   Rev 1.18.1.1   Oct 13 2005 10:29:22   jbroome
//Updated to be used on Visit Planner results
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.18.1.0   Oct 12 2005 16:33:40   jbroome
//Checked in to force branch
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.18   Sep 30 2004 13:13:06   jbroome
//Extend Journey additional changes (DD02101d)
//
//   Rev 1.17   Sep 13 2004 16:15:42   RHopkins
//IR1484 Add new attributes to the JourneyRequest for ReturnOriginLocation and ReturnDestinationLocation to allow Extensions to be made to/from Return locations that differ from the corresponding Outward location.
//
//   Rev 1.16   Aug 23 2004 15:52:18   RHopkins
//Changes to the label text.
//
//   Rev 1.15   Aug 03 2004 11:54:08   COwczarek
//Use new IsFindAMode property
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.14   Jul 28 2004 10:03:42   jbroome
//IR 1247 - Correct dynamic formatting of control according to parent page.
//
//   Rev 1.13   Jul 22 2004 11:13:34   jgeorge
//Updates for Find a (Del 6.1)
//
//   Rev 1.12   Jul 16 2004 14:03:18   rgreenwood
//IR1108 Fix to SetControlVisibility method
//
//   Rev 1.11   Jul 14 2004 15:11:38   RHopkins
//IR1124 Removed duplicate population of the Return Hours dropdown.
//
//   Rev 1.10   Jul 12 2004 15:12:52   jbroome
//Actioned Extend Journey code review comments.
//
//   Rev 1.9   Jun 28 2004 20:48:58   JHaydock
//JourneyPlannerInput clear page and back buttons for extend journey
//
//   Rev 1.8   Jun 24 2004 14:08:16   esevern
//don't bother creating control if current page is find a 
//
//   Rev 1.7   Jun 23 2004 16:41:04   COwczarek
//Modifications to allow erroneous stop over times to be
//highlighted and modified by user.
//Resolution for 1044: Add date validation to extend journey functionality
//
//   Rev 1.6   Jun 09 2004 16:16:58   esevern
//fix for ambiguity page back button error (incorrect date format).  
//
//   Rev 1.5   Jun 09 2004 11:15:20   esevern
//correction to display - on ambiguity page, only displays if extend is in progress
//
//   Rev 1.4   Jun 08 2004 17:01:28   esevern
//added display of control in read only state (for jp ambiguity page)
//
//   Rev 1.3   Jun 04 2004 13:40:04   RHopkins
//Corrected time calculation;  Allowed values to be defaulted from TDJourneyParameters;  Made time calculation method public so it can be called by parent pages
//
//   Rev 1.2   May 25 2004 15:44:18   ESevern
//added search button and event handling
//
//   Rev 1.1   May 12 2004 12:04:38   ESevern
//added dropdowns for day/hour/mins
//
//   Rev 1.0   May 05 2004 15:46:46   ESevern
//Initial Revision

namespace TransportDirect.UserPortal.Web.Controls 
{
	using System;

	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.Common;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Web.Support;
	using TransportDirect.Common.PropertyService.Properties;
	using TransportDirect.Common.ResourceManager;

	/// <summary>
	///	Allows the user to amend the stopover time (days, hours, mins) for 
	///	their selected extension.
	/// </summary>
	public partial  class AmendStopoverControl : TDUserControl
	{

		// Screenreader labels

		// Display attributes
		private bool returnRequired = false;
        private bool outwardReadOnly = false;
        private bool returnReadOnly = false;
        protected bool displayOutwardAsError = false;
        protected bool displayReturnAsError = false;

		// constants used in drop list population
		private const int HOURS = 24;
		private const int DAYS = 7;
		private const int MINUTES = 60;
		private PageId callingPageId = PageId.Empty;
		private const int MONTH = 10;

        /// <summary>
        /// Initialises this control.
        /// </summary>
        /// <param name="callingPageId">Page id of containing page</param>
        /// <param name="readOnly">True if control should be displayed readonly</param>
        /// <param name="displayOutwardAsError">True if outward stop over time is erroneous</param>
        /// <param name="displayReturnAsError">True if return stop over time is erroneous</param>
        public void Initialise(PageId callingPageId, bool readOnly, bool displayOutwardAsError, bool displayReturnAsError)
        {
            this.callingPageId = callingPageId;
            this.displayOutwardAsError = displayOutwardAsError;
            this.displayReturnAsError = displayReturnAsError;

            outwardReadOnly = readOnly;
            returnReadOnly = readOnly;            

            // If we are displaying erroneous times, override readonly state 
            // based on whether outward or return times are invalid
            if (displayReturnAsError == true && displayOutwardAsError == true) 
            {
                // If both outward and return times are shown as errors, allow both to be editable
                outwardReadOnly = false;
                returnReadOnly = false;            
            } 
            else 
            {
                // otherwise only allow erroneuous time to be editable and other to be readonly
                if (displayOutwardAsError) 
                {
                    returnReadOnly = true;
                }
                if (displayReturnAsError) 
                {
                    outwardReadOnly = true;
                }
            }
        }

        /// <summary>
        /// Initialises this control. By default the outward and return stop over times
        /// are editable.
        /// </summary>
        /// <param name="callingPageId">Page id of containing page</param>
        public void Initialise(PageId callingPageId)
        {
            this.Initialise(callingPageId,false,false,false);
        }

		/// <summary>
		/// Resets the drop downs to the journey parameter's values
		/// </summary>
		public void Reset()
		{
			SetOutwardDropDown();

			if (returnRequired)
				SetReturnDropDown();
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// if this is find a ... page or a Visit Planner page, don't do anything.
			if (( TDSessionManager.Current.IsFindAMode) || (callingPageId == PageId.VisitPlannerResults))
				return;

			returnRequired = TDItineraryManager.Current.ReturnExtendPermitted;

			// set alt text, image url and labels

			labelTitleOutward.Text = GetResource("theAmendStopoverControl.labelTitle.StopoverTime");

			labelOutward.Text = GetResource("AmendStopoverControl.labelOutward");
			labelOutwardDays.Text = GetResource("AmendStopoverControl.labelOutwardDays");
			labelOutwardHours.Text = GetResource("AmendStopoverControl.labelOutwardHours");
			labelOutwardMinutes.Text = GetResource("AmendStopoverControl.labelOutwardMinutes");
			labelSROutwardDays.Text = GetResource("panelStopoverOutward.labelSRdropStopoverOutwardDays");
			labelSROutwardHours.Text = GetResource("panelStopoverOutward.labelSRdropStopoverOutwardHours");
			labelSROutwardMinutes.Text = GetResource("panelStopoverOutward.labelSRdropStopoverOutwardMinutes");

			// check for extension to start or from end ... 
			if(TDItineraryManager.Current.ExtendEndOfItinerary)  
			{
				// update stopover time label text with the start point of the extension
				labelTitleOutward.Text += TDSessionManager.Current.JourneyParameters.OriginLocation.Description;
			}
			else 
			{
				// update stopover time label text with the end point of the extension
				labelTitleOutward.Text += TDSessionManager.Current.JourneyParameters.DestinationLocation.Description;
			}
	
			if (returnRequired)
			{
				labelReturn.Text = GetResource("AmendStopoverControl.labelReturn");
				labelReturnDays.Text = GetResource("AmendStopoverControl.labelReturnDays");
				labelReturnHours.Text = GetResource("AmendStopoverControl.labelReturnHours");
				labelReturnMinutes.Text = GetResource("AmendStopoverControl.labelReturnMinutes");
				labelSRReturnDays.Text = GetResource("panelStopoverOutward.labelSRdropStopoverReturnDays");
				labelSRReturnHours.Text = GetResource("panelStopoverOutward.labelSRdropStopoverReturnHours");
				labelSRReturnMinutes.Text = GetResource("panelStopoverOutward.labelSRdropStopoverReturnMinutes");
					
				if(TDItineraryManager.Current.ExtendEndOfItinerary)  
				{
					// Check if return location is different
					if (TDSessionManager.Current.JourneyParameters.ReturnDestinationLocation != null)
					{
						labelTitleReturn.Text = GetResource("theAmendStopoverControl.labelTitle.StopoverTime");
						labelTitleReturn.Text += TDSessionManager.Current.JourneyParameters.ReturnDestinationLocation.Description;
					}
				}
				else 
				{
					// Check if return location is different
					if (TDSessionManager.Current.JourneyParameters.ReturnOriginLocation != null)
					{
						labelTitleReturn.Text = GetResource("theAmendStopoverControl.labelTitle.StopoverTime");
						labelTitleReturn.Text += TDSessionManager.Current.JourneyParameters.ReturnOriginLocation.Description;
					}
				}

				PopulateReturnDropDown();
			}				
                
			if(((TDPage)Page).PageId == PageId.ExtendJourneyInput)
			{
				buttonSearch.Visible = false;
				labelTitleOutward.Visible = true;

			}
            
			else if (((TDPage)Page).PageId != PageId.JourneyPlannerInput &&
				((TDPage)Page).PageId != PageId.JourneyPlannerAmbiguity)
			{
				buttonSearch.Text = GetResource("AmendStopoverControl.buttonSearch.Text");                    buttonSearch.Visible = true;
				labelTitleOutward.Visible = false;
				labelTitleReturn.Visible = false;
			}  
            else 
            {
                buttonSearch.Visible = false;
            }

			PopulateOutwardDropDown();

            Refresh();

		}

        /// <summary>
        /// Refresh the state of controls
        /// </summary>
        private void Refresh() 
        {
            if (this.PageId == PageId.JourneyPlannerAmbiguity || this.PageId == PageId.ExtendJourneyInput)
            {
                if (displayOutwardAsError) 
                {
                    tableOutwardStopover.Attributes.Add ("class", "alerterror");
                } 
                else 
                {
                    tableOutwardStopover.Attributes.Remove ("class");
                }

                if (displayReturnAsError) 
                {
                    tableReturnStopover.Attributes.Add ("class", "alerterror");
                } 
                else 
                {
                    tableReturnStopover.Attributes.Remove ("class");
                }

            }

			// Set the appearance style of the control using cellpadding / cellspacing
			// If on the JourneySummary Page, then control is more tightly spaced.
			if (((TDPage)Page).PageId == PageId.JourneySummary)
			{
				tableAmendStopoverControl.Attributes.Add ("cellspacing", "0");
				tableAmendStopoverControl.Attributes.Remove ("cellpadding");
				tableOutwardStopover.Attributes.Add ("cellspacing", "0");
				tableOutwardStopover.Attributes.Add ("cellpadding", "0");
				tableReturnStopover.Attributes.Add ("cellspacing", "0");
				tableReturnStopover.Attributes.Add ("cellpadding", "0");
			}
			else
			{
				tableAmendStopoverControl.Attributes.Add ("cellspacing", "0");
				tableAmendStopoverControl.Attributes.Add ("cellpadding", "0");
				tableOutwardStopover.Attributes.Add ("cellspacing", "5");
				tableOutwardStopover.Attributes.Remove ("cellpadding");
				tableReturnStopover.Attributes.Add ("cellspacing", "5");
				tableReturnStopover.Attributes.Remove ("cellpadding");
			}

            SetControlVisibility();
        }

        /// <summary>
        /// Prerender event handler
        /// </summary>
        /// <param name="e">event arguments</param>
        protected override void OnPreRender(EventArgs e)
        {
			// Only bother doing this if the control is visible
			if (this.Visible)
				Refresh();
        }

		/// <summary>
		/// Populates the day, hour and minute drop downs for outward and return.  The 
		/// return drop downs should only be visible if return was part of original request.
		/// </summary>
		private void PopulateOutwardDropDown() 
		{
            // storing dropdown's selected indexes
            int dayindex = dropListOutwardDays.SelectedIndex;
            int hourindex = dropListOutwardHours.SelectedIndex;
            int minuteindex = dropListOutwardMinutes.SelectedIndex;

			// set up the day, hour and minute dropdowns
			string toAdd = string.Empty;
			for(int dayCount = 0; dayCount<DAYS; dayCount++)
			{
				toAdd = dayCount.ToString("D2",TDCultureInfo.CurrentCulture.NumberFormat);
				dropListOutwardDays.Items.Add(toAdd);
			}	

			for(int hourCount = 0; hourCount<HOURS; hourCount++)
			{
				toAdd = hourCount.ToString("D2",TDCultureInfo.CurrentCulture.NumberFormat);
				dropListOutwardHours.Items.Add(toAdd);
			}

			for(int minuteCount = 0; minuteCount<MINUTES; minuteCount+=5)
			{
				toAdd = minuteCount.ToString("D2",TDCultureInfo.CurrentCulture.NumberFormat);
				dropListOutwardMinutes.Items.Add(toAdd);
			}

            

			// Set default display
			SetOutwardDropDown();

            // setting selected index for dropdowns
            dropListOutwardDays.SelectedIndex = dayindex;
            dropListOutwardHours.SelectedIndex = hourindex;
            dropListOutwardMinutes.SelectedIndex = minuteindex;
		}

		/// <summary>
		/// Sets the outward drop down to show the values as in journey parameters
		/// </summary>
		private void SetOutwardDropDown()
		{
			TDJourneyParametersMulti journeyParameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
			dropListOutwardDays.SelectedIndex = journeyParameters.OutwardStopoverDays;
			dropListOutwardHours.SelectedIndex = journeyParameters.OutwardStopoverHours;
			dropListOutwardMinutes.SelectedIndex = journeyParameters.OutwardStopoverMinutes / 5;
		}

		/// <summary>
		/// Populates the day, hour and minute drop downs for return.  This control  
		/// should only be visible if return was part of original request.
		/// </summary>
		private void PopulateReturnDropDown() 
		{
            // storing dropdown's selected indexes
            int dayindex = dropListReturnDays.SelectedIndex;
            int hourindex = dropListReturnHours.SelectedIndex;
            int minuteindex = dropListReturnMinutes.SelectedIndex;

			// set up the day, hour and minute dropdowns
			string toAdd = string.Empty;
			for(int dayCount = 0; dayCount<DAYS; dayCount++)
			{
				toAdd = dayCount.ToString("D2",TDCultureInfo.CurrentCulture.NumberFormat);
				dropListReturnDays.Items.Add(toAdd);
			}	

			for(int hourCount = 0; hourCount<HOURS; hourCount++)
			{
				toAdd = hourCount.ToString("D2",TDCultureInfo.CurrentCulture.NumberFormat);
				dropListReturnHours.Items.Add(toAdd);
			}

			for(int minuteCount = 0; minuteCount<MINUTES; minuteCount+=5)
			{
				toAdd = minuteCount.ToString("D2",TDCultureInfo.CurrentCulture.NumberFormat);
				dropListReturnMinutes.Items.Add(toAdd);
			}

            

			// Set default display
			SetReturnDropDown();

            // setting selected index for dropdowns
            dropListReturnDays.SelectedIndex = dayindex;
            dropListReturnHours.SelectedIndex = hourindex;
            dropListReturnMinutes.SelectedIndex = minuteindex;
		}

		/// <summary>
		/// Sets the return drop down to show the values as in journey parameters
		/// </summary>
		private void SetReturnDropDown()
		{
			TDJourneyParametersMulti journeyParameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
			dropListReturnDays.SelectedIndex = journeyParameters.ReturnStopoverDays;
			dropListReturnHours.SelectedIndex = journeyParameters.ReturnStopoverHours;
			dropListReturnMinutes.SelectedIndex = journeyParameters.ReturnStopoverMinutes / 5;
		}


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

		}
		#endregion

		/// <summary>
		/// Updates journeyParameters object with stopover dates and times selected in drop downs.
		/// </summary>
		public void UpdateRequestedTimes()
		{
			// check for end or start extension
			TDJourneyParametersMulti journeyParameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			bool extendEnd = itineraryManager.ExtendEndOfItinerary;
			bool returnRequired = itineraryManager.ReturnExtendPermitted;
			TDDateTime outwardStopover = null;
			TDDateTime returnStopover = null;
			int outDays = 0;
			int outHours = 0;
			int outMins = 0;
			int retDays = 0;
			int retHours = 0;
			int retMins = 0;
			string monthYearSeparator = Properties.Current["journeyplanner.monthyearseparator"];

			outDays = Convert.ToInt32(dropListOutwardDays.SelectedItem.Value.ToString(
				TDCultureInfo.CurrentUICulture.NumberFormat));

			outHours = Convert.ToInt32(dropListOutwardHours.SelectedItem.Value.ToString(
				TDCultureInfo.CurrentUICulture.NumberFormat));

			outMins = Convert.ToInt32(dropListOutwardMinutes.SelectedItem.Value.ToString(
				TDCultureInfo.CurrentUICulture.NumberFormat));

			System.TimeSpan oStopover = new TimeSpan(outDays, outHours, outMins, 0);
			
			if(extendEnd)
			{
				// extend from end. Add the selected stopover time to the intinerary manager's 
				// stored outward arrival time to get new departure time (ie, 'leave after')
				outwardStopover = itineraryManager.OutwardArriveDateTime().Add(oStopover); 
			}
			else 
			{
				// extend to start. Subtract the selected stopover time from the itinerary manager's 
				// stored outward departure time to get new arrival time (ie. 'arrive before')
				outwardStopover = itineraryManager.OutwardDepartDateTime().Subtract(oStopover);
			}

			journeyParameters.OutwardStopoverDays = outDays;
			journeyParameters.OutwardStopoverHours = outHours;
			journeyParameters.OutwardStopoverMinutes = outMins;

			journeyParameters.OutwardHour = outwardStopover.Hour.ToString();
			journeyParameters.OutwardMinute = outwardStopover.Minute.ToString();
			journeyParameters.OutwardDayOfMonth = outwardStopover.Day.ToString();
 
			string tempOut = string.Empty;
			if(outwardStopover.Month < MONTH) 
			{
				tempOut = "0"+ outwardStopover.Month.ToString();
			}
			else 
			{
				tempOut = outwardStopover.Month.ToString();
			}
			journeyParameters.OutwardMonthYear = tempOut  
				+ monthYearSeparator + outwardStopover.Year.ToString();

			// Check to see if a return journey was requested
			if(returnRequired)
			{
				
				retDays = Convert.ToInt32(dropListReturnDays.SelectedItem.Value.ToString(
					TDCultureInfo.CurrentUICulture.NumberFormat));

				retHours = Convert.ToInt32(dropListReturnHours.SelectedItem.Value.ToString(
					TDCultureInfo.CurrentUICulture.NumberFormat));

				retMins = Convert.ToInt32(dropListReturnMinutes.SelectedItem.Value.ToString(
					TDCultureInfo.CurrentUICulture.NumberFormat));

				System.TimeSpan rStopover = new TimeSpan(retDays, retHours, retMins, 0);			
	
				if(extendEnd)
				{
					returnStopover = itineraryManager.ReturnDepartDateTime().Subtract(rStopover);
					journeyParameters.ReturnArriveBefore = true;
				}
				else 
				{
					returnStopover = itineraryManager.ReturnArriveDateTime().Add(rStopover);
					journeyParameters.ReturnArriveBefore = false;
				}

                journeyParameters.ReturnStopoverDays = retDays;
                journeyParameters.ReturnStopoverHours = retHours;
                journeyParameters.ReturnStopoverMinutes = retMins;

                journeyParameters.ReturnHour = returnStopover.Hour.ToString();
				journeyParameters.ReturnMinute = returnStopover.Minute.ToString();
				journeyParameters.ReturnDayOfMonth = returnStopover.Day.ToString(); 
				
				string tempRet = string.Empty;
				if(returnStopover.Month < MONTH) 
				{
					tempRet = "0"+ returnStopover.Month.ToString();
				}
				else 
				{
					tempRet = returnStopover.Month.ToString();
				}
				journeyParameters.ReturnMonthYear = tempRet
					+ monthYearSeparator + returnStopover.Year.ToString();
			}
		}

		/// <summary>
		/// Performs journey search using new departure/arrival date/time calculated using
		/// user supplied stopover values
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">ImageClickEventArgs</param>
		private void buttonSearch_Click(object sender, EventArgs e)
		{
			UpdateRequestedTimes();

			ITDSessionManager sessionManager = TDSessionManager.Current;

			// Journey Parameters have been updated. Call JourneyPlannerRunner ValidateAndRun method.
			JourneyPlanRunner.JourneyPlanRunner runner = new JourneyPlanRunner.JourneyPlanRunner(Global.tdResourceManager);

			if (runner.ValidateAndRun(
                sessionManager, 
                sessionManager.JourneyParameters, 
                TDPage.GetChannelLanguage(TDPage.SessionChannelName),ValidateDates()))
			{			
				AsyncCallState acs = new JourneyPlanState();

				// Determine refresh interval and resource string for the wait page
				acs.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.AmendStopOverTime"]);
				acs.WaitPageMessageResourceFile = "langStrings";
				acs.WaitPageMessageResourceId = "WaitPageMessage.AmendStopOverTime";

                acs.AmbiguityPage = PageId.JourneyDetails;
                acs.DestinationPage = PageId.JourneyDetails;
                acs.ErrorPage = PageId.JourneyDetails;
				sessionManager.AsyncCallState = acs;
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.WaitingRefresh;
			}
			else
			{
				sessionManager.InputPageState.JourneyInputReturnStack.Push(callingPageId);
				sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputErrors;
			}

		}

		/// <summary>
		/// Set the visibility of controls based on read only and error values passed by
		/// initialise method and whether or not a return journey is available.
		/// </summary>
		private void SetControlVisibility() 
		{

			TDJourneyParametersMulti journeyParameters = 
				TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;

			string days = string.Empty;
			string hours = string.Empty;
			string mins = string.Empty;
			string space = " ";
		
			labelReturn.Visible = returnRequired;

            labelOutwardReadonly.Visible = false;
            labelOutwardDays.Visible = labelOutwardDays.Visible = labelOutwardHours.Visible = labelOutwardMinutes.Visible = false;
            dropListOutwardDays.Visible = dropListOutwardHours.Visible = dropListOutwardMinutes.Visible = false;

            if (outwardReadOnly) 
            {
                days = journeyParameters.OutwardStopoverDays + space + labelOutwardDays.Text;
                hours = journeyParameters.OutwardStopoverHours + space + labelOutwardHours.Text;
                mins = journeyParameters.OutwardStopoverMinutes + space + labelOutwardMinutes.Text;
                labelOutwardReadonly.Text = days + space + hours + space + mins;
                labelOutwardReadonly.Visible = true;
            } 
            else 
            {
                labelOutwardDays.Visible = labelOutwardDays.Visible = labelOutwardHours.Visible = labelOutwardMinutes.Visible = true;
                dropListOutwardDays.Visible = dropListOutwardHours.Visible = dropListOutwardMinutes.Visible = true;
            }

            labelReturnDays.Visible = labelReturnDays.Visible = labelReturnHours.Visible = labelReturnMinutes.Visible = false;
            dropListReturnDays.Visible = dropListReturnHours.Visible = dropListReturnMinutes.Visible = false;
            labelReturnReadonly.Visible = false;

			if(returnRequired) 
			{
				returnRow.Visible = true;
				returnTitleRow.Visible = true;
				if (returnReadOnly) 
				{
					days = journeyParameters.ReturnStopoverDays + space + labelReturnDays.Text;
					hours = journeyParameters.ReturnStopoverHours + space + labelReturnHours.Text;
					mins = journeyParameters.ReturnStopoverMinutes + space + labelReturnMinutes.Text;
					labelReturnReadonly.Text = days + space + hours + space + mins;
					labelReturnReadonly.Visible = true;
				} 
				else 
				{
					dropListReturnDays.Visible = dropListReturnHours.Visible = dropListReturnMinutes.Visible = true;
					labelReturnDays.Visible = labelReturnDays.Visible = labelReturnHours.Visible = labelReturnMinutes.Visible = true;
				}
			}
			else
			{
				returnRow.Visible = false;
				returnTitleRow.Visible = false;
			}

		}

        /// <summary>
        /// Returns true if date validation should be performed on journey parameters.
        /// It will be true if an extend is not in progress or if an extend is in progress
        /// and the stop over times have been modified.
        /// </summary>
        /// <returns>True if date validation required, false otherwise</returns>
        public static bool ValidateDates()
        {
            TDJourneyParametersMulti journeyParameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
            if (journeyParameters != null) 
            {
                return !TDItineraryManager.Current.ExtendInProgress || (
                    TDItineraryManager.Current.ExtendInProgress && 
                    (journeyParameters.OutwardStopOverSet || journeyParameters.ReturnStopOverSet));
            } 
            else 
            {
                return false;
            }
        }


	}
}
