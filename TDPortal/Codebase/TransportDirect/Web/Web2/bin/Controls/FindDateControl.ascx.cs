// ***********************************************
// NAME                 : FindDateControl.ascx.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 09/07/2004 
// DESCRIPTION          : Web control allowing user   
//                        to view or specify date
//						  and time of travel. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindDateControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Oct 27 2010 11:12:40   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.2   Mar 31 2008 13:20:32   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:14:06   mturner
//Initial revision.
//
//   Rev 1.29   Feb 23 2006 19:16:34   build
//Automatically merged from branch for stream3129
//
//   Rev 1.28.1.0   Jan 10 2006 15:24:34   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.28   Nov 09 2005 14:03:32   NMoorhouse
//TD93 - UEE Input Pages - Update Visit Planner
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.27   Nov 03 2005 17:02:28   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.26.1.1   Oct 27 2005 14:01:40   NMoorhouse
//TD93 - UEE Input Pages, Date Control element CUT
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.26.1.0   Oct 07 2005 13:56:00   mtillett
//Update layout for the UEE changes
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.26   Sep 29 2005 12:47:00   build
//Automatically merged from branch for stream2673
//
//   Rev 1.25.1.3   Sep 14 2005 13:15:10   rgreenwood
//DN079 ES015 Code Review
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.25   Mar 04 2005 11:26:30   tmollart
//Added propertys to control visibility of flexibility and time controls. This is so these can be set from a high level within the parent control.
//Added #region statements to code.
//
//   Rev 1.24   Feb 25 2005 10:33:42   tmollart
//Updated SetHelpLabel method so that help labels are correctly set for FindFare.
//
//   Rev 1.23   Feb 10 2005 16:52:44   tmollart
//Added facility to hide the 24 hour clock help button. 
//Added new overloaded initialisation method.
//
//   Rev 1.22   Oct 01 2004 11:03:46   COwczarek
//Make behaviour consistent for all Find A input pages when in ambiguity mode.
//Resolution for 1562: Find A input page in ambiguity mode always shows travel details
//
//   Rev 1.21   Sep 23 2004 10:19:08   RGeraghty
//Updated SetHelpLabel to link to correct Find A Car help
//
//   Rev 1.20   Sep 08 2004 16:31:10   passuied
//added extra check when both outwar and Return are invalid or in the past
//Resolution for 1544: Find a Train - Error messages incorrect for date/time
//
//   Rev 1.19   Sep 08 2004 14:58:24   COwczarek
//Set date type label to be the same as used on the journey
//planner ambiguity page when date/time ambiguous. To
//improve formatting, there are now two 24 hour clock help
//buttons in diffrent positions - one visible when date ambiguous
//and one when in input mode. Also, help text is set correctly
//depending on which mode date control is in.
//Resolution for 1336: Date controls for in Find A Flight inconsistent with other Find A pages
//
//   Rev 1.18   Aug 26 2004 14:16:00   esevern
//amended hiding of dateHelpCustomControl to be visible only on  CarInput ambiguity page
//
//   Rev 1.17   Aug 23 2004 17:27:10   jmorrissey
//Fix for IR1301
//
//   Rev 1.15   Aug 16 2004 10:45:04   esevern
//amendments to setting of help text
//
//   Rev 1.14   Aug 05 2004 15:11:16   esevern
//moved SetDisplayMode to Pre_Render - when called in Page_Init, was checking for IsOutward before the property had been set in FindLeaveReturnDatesControl Page_Load, so both controls were 'Return' when first displayed.
//
//   Rev 1.13   Aug 05 2004 14:34:38   esevern
//put helpboxfinda style back in for datehelplabel - help should be horizontal
//
//   Rev 1.12   Aug 03 2004 10:07:24   passuied
//check in for little Esther...
//
//   Rev 1.11   Jul 29 2004 11:40:06   COwczarek
//Make clock help button invisible when in read only mode
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.10   Jul 28 2004 17:27:42   passuied
//Implementation of a DateSelected event in calendar to indicate to subscribers a date has been selected in calendar.
//
//   Rev 1.9   Jul 28 2004 17:10:18   passuied
//added code to use calendar control. Created event triggered in case date supplied by calendar has changed.
//
//   Rev 1.8   Jul 28 2004 16:11:00   passuied
//Changes to make the FindA date controls work
//
//   Rev 1.7   Jul 28 2004 14:25:40   esevern
//amended helpbox label style to display horizontally on page
//
//   Rev 1.6   Jul 27 2004 17:20:22   COwczarek
//Removed placeholders to position tri state date control
//dynamically. Instead use different labels and change their
//visibility dynamically to achieve same effect. 
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.5   Jul 22 2004 11:53:40   esevern
//only set text on first page load
//
//   Rev 1.4   Jul 22 2004 11:30:10   esevern
//changes in line with amended Help control (Text and MoreUrl properties)
//
//   Rev 1.3   Jul 16 2004 14:28:00   esevern
//removed all the extra help labels that were added last time, and we now don't need.
//
//   Rev 1.2   Jul 15 2004 11:48:56   esevern
//added help labels 
//
//   Rev 1.1   Jul 13 2004 10:58:16   esevern
//Interim check-in for addition to pages
//
//   Rev 1.0   Jul 09 2004 11:49:56   esevern
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.Common;

	/// <summary>
	///	The behaviour of this control should be identical on all pages
	///	(FindTrainInput, FindCoachInput, FindCarInput, FindTrunkInput)
	///	Allows user to specify, view or correct the date and time
	///	of travel
	/// </summary>
	public partial  class FindDateControl : TDUserControl
	{
		#region Declarations

		protected TransportDirect.UserPortal.SessionManager.ValidationError errors;
		protected TransportDirect.UserPortal.Web.Controls.TriStateDateControl triDateControl;
		
		private bool ambiguousMode;
		private bool timeControlsVisible = true;
		private bool flexibilityControlsVisible = false;
		private bool arriveByOption = true;
		
		public event EventHandler DateChanged;
		
		#endregion

		#region Private Event Handlers
		/// <summary>
		/// Sets up controls inc help labels
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

        /// <summary>
        /// Event handler for page pre-render event
        /// </summary>
        /// <param name="sender">event originator</param>
        /// <param name="e">event arguments</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
			SetDisplayMode();            
			UpdateErrorMessage();
        }

		#endregion

		#region Private Methods
		/// <summary>
		/// Checks ambiguity mode and for existing date errors, setting
		/// TriStateDateControl display mode and visibility of labels accordingly
		/// </summary>
		private void SetDisplayMode() 
		{
            string labelText;

            if(IsOutward) 
            {
                labelText = Global.tdResourceManager.GetString(
                    "JourneyPlannerAmbiguity.labelOutwardTitle", TDCultureInfo.CurrentUICulture);
            }
            else 
            {
                labelText = Global.tdResourceManager.GetString(
                    "JourneyPlannerAmbiguity.labelReturnTitle", TDCultureInfo.CurrentUICulture);
            }
			directionEntryLabel.Text = labelText;
            
            if(AmbiguityMode) 
			{
				if (IsOutward) 
                {
                    if(OutwardDateErrorsExist()) 
                    {
                        triDateControl.DisplayMode = GenericDisplayMode.Ambiguity;

                    }
                    else 
                    {
                        triDateControl.DisplayMode = GenericDisplayMode.ReadOnly;
                    }
                } 
                else 
                {
                    if(ReturnDateErrorsExist()) 
                    {
                        triDateControl.DisplayMode = GenericDisplayMode.Ambiguity;
                        
                    }
                    else 
                    {
                        triDateControl.DisplayMode = GenericDisplayMode.ReadOnly;
                        this.Visible = !triDateControl.DateDisplayControl.IsNoReturn;
                    }
                    
                }              
            }
			else 
			{
				triDateControl.DisplayMode = GenericDisplayMode.Normal;
            }


		}

		/// <summary>
		/// Uses FindDateValidation helper class to check for specific date 
		/// errors and sets appropriate error message text.
		/// </summary>
		private void UpdateErrorMessage()
		{

            errorMessageLabel.Text = string.Empty;
			
            if (errors == null || ambiguousMode == false) 
            {
                return;
            }

			// Checks for both dates being incorrect are not made, as we're displaying seperate control 
			// for outward and return.	

			if (!TDItineraryManager.Current.ExtendInProgress) 
			{

				if(IsOutward) 
				{
					if(InvalidOutward || InvalidDates) 
					{
						errorMessageLabel.Text += Global.tdResourceManager.GetString(
							"ValidateAndRun.DateNotValid", TDCultureInfo.CurrentUICulture) + " ";
					}
					if(OutwardDateInPast || AreDatesPast)
					{
						errorMessageLabel.Text += Global.tdResourceManager.GetString(
							"ValidateAndRun.DateTimeIsInThePast", TDCultureInfo.CurrentUICulture) + " ";
					}

					if (OutwardExtensionFromEndInPast) 
					{
						errorMessageLabel.Text += Global.tdResourceManager.GetString(
							"ValidateAndRun.ExtendFromEndOutwardInPast", TDCultureInfo.CurrentUICulture);
					}
					else if (OutwardExtensionToStartInPast) 
					{
						errorMessageLabel.Text += Global.tdResourceManager.GetString(
							"ValidateAndRun.ExtendToStartOutwardInPast", TDCultureInfo.CurrentUICulture);
            
					}
				}
				else 
				{
					if (InvalidReturn || InvalidDates) 
					{
						errorMessageLabel.Text += Global.tdResourceManager.GetString(
							"ValidateAndRun.DateNotValid", TDCultureInfo.CurrentUICulture) + " ";
					}
					// Display message if return date is earlier than outward date
					if(ReturnBeforeOutward) 
					{
						errorMessageLabel.Text += Global.tdResourceManager.GetString(
							"ValidateAndRun.OutwardDateIsAfterReturnDate",
							TDCultureInfo.CurrentUICulture) + " ";
					}
					if(ReturnDateInPast || AreDatesPast)
					{
						errorMessageLabel.Text += Global.tdResourceManager.GetString(
							"ValidateAndRun.DateTimeIsInThePast", TDCultureInfo.CurrentUICulture) + " ";
					}

					// Display error message if return time valid but either return day or 
					// month not specified
					if(ReturnDateMissing) 
					{
						errorMessageLabel.Text += Global.tdResourceManager.GetString(
							"ValidateAndRun.ReturnDateMissing", TDCultureInfo.CurrentUICulture) + " ";
					}

					// Display error message if return day specified but not return month 
					if(ReturnMonthMissing)
					{
						errorMessageLabel.Text += Global.tdResourceManager.GetString(
							"ValidateAndRun.ReturnMonthMissing", TDCultureInfo.CurrentUICulture) + " ";
					}

					// Display error message if return date specified but not return hour 
					// (mins defaulted to 00)
					if(ReturnTimeMissing)
					{
						errorMessageLabel.Text += Global.tdResourceManager.GetString(
							"ValidateAndRun.ReturnTimeMissing", TDCultureInfo.CurrentUICulture) + " ";
					}

					if (ReturnExtensionFromEndInPast) 
					{
						errorMessageLabel.Text = Global.tdResourceManager.GetString(
							"ValidateAndRun.ExtendFromEndReturnInPast", TDCultureInfo.CurrentUICulture);
					}
					else if (ReturnExtensionToStartInPast ) 
					{
						errorMessageLabel.Text += Global.tdResourceManager.GetString(
							"ValidateAndRun.ExtendToStartReturnInPast", TDCultureInfo.CurrentUICulture);
            
					}
				}
			} 
			else 
			{
				errorMessageLabel.Text = string.Empty;

				if(IsOutward) 
				{
					// outward extension time is past
					if (OutwardExtensionToStartInPast)  
					{
						errorMessageLabel.Text += Global.tdResourceManager.GetString(
							"ValidateAndRun.StopOverTimeIntoPast", TDCultureInfo.CurrentUICulture) + " ";
					}
				}
				else 
				{
					// return extension time is past
					if (ReturnExtensionToStartInPast) 
					{
						errorMessageLabel.Text += Global.tdResourceManager.GetString(
							"ValidateAndRun.StopOverTimeIntoPast", TDCultureInfo.CurrentUICulture) + " ";
					}
				}

				if (ExtensionReturnOverlap) 
				{
					errorMessageLabel.Text += Global.tdResourceManager.GetString(
						"ValidateAndRun.ExtensionReturnOverlap",
						TDCultureInfo.CurrentUICulture);
				}

			}
			
		}
		#endregion

		#region public properties

		/// <summary>
		/// Read/write property defining whether this control
		/// is being used for entry of leave and return date/
		/// times.  Returns true if outward only
		/// </summary>
		public bool IsOutward 
		{
			get 
			{
				return triDateControl.IsOutward;
			}
			set 
			{
				triDateControl.IsOutward = value;
			}
		}

		/// <summary>
		/// Read/write property defining whether this control
		/// is being used for entry of leave and return date/
		/// times.  Returns true if displaying ambiguities.
		/// </summary>
		public bool AmbiguityMode 
		{
			get 
			{
				return ambiguousMode;
				//return TDSessionManager.Current.FindPageState.AmbiguityMode;
			}
			set 
			{
                ambiguousMode = value;
                SetDisplayMode();
			}
		}

		/// <summary>
		/// Read/write property. Sets/returns ValidationError, containing
		/// all error ids (including date related items) set during validate 
		/// and run. Must be set before FindDateControl used.
		/// </summary>
		public ValidationError DateErrors 
		{
			get 
			{
				return errors;
				//return TDSessionManager.Current.ValidationError;
			}
			set 
			{
				errors = value;
                SetDisplayMode();
            }
		}

		/// <summary>
		/// Read only property, returning TriStateDateControl
		/// </summary>
		public TriStateDateControl DateControl 
		{
			get 
			{
				return triDateControl;
			}
		}

		/// <summary>
		///Read/Write. Sets whether time controls are visible on child triDateControl
		/// </summary>
		public bool TimeControlsVisible
		{
			get {return timeControlsVisible;}
			set 
			{
				timeControlsVisible = value;
				triDateControl.TimeControlsVisible = value;
			}
		}

		/// <summary>
		/// Read/Write: Sets whether flexibiliyt controls are visible on child triDateControl
		/// </summary>
		public bool FlexibilityControlsVisible
		{
			get {return flexibilityControlsVisible;}
			set
			{
				flexibilityControlsVisible = value;
				triDateControl.FlexibilityControlsVisible = value;
			}
		}		
	
		/// <summary>
		/// Read/Write: Sets whether Arriving By Option is valid on child triDateControl
		/// </summary>
		public bool ArriveByOption
		{
			get {return arriveByOption;}
			set
			{
				arriveByOption = value;
				triDateControl.ArriveByOption = value;
			}
		}
		#endregion

		#region validation error id checks

		/// <summary>
		/// Returns true if ValidationError contains an outward date related
		/// error id, false otherwise 
		/// </summary>
		/// <returns>bool</returns>
		private bool OutwardDateErrorsExist() 
		{
			if(DateErrors != null) 
			{			
				return 
					DateErrors.Contains( ValidationErrorID.OutwardDateTimeInvalid ) ||
					DateErrors.Contains( ValidationErrorID.OutwardDateTimeNotLaterThanNow ) ||
					DateErrors.Contains( ValidationErrorID.OutwardAndReturnDateTimeInvalid ) ||
					DateErrors.Contains( ValidationErrorID.ReturnDateTimeNotLaterThanOutwardDateTime ) ||
					DateErrors.Contains( ValidationErrorID.OutwardAndReturnDateTimeNotLaterThanNow );
			}
			else 
			{
				return false;
			}
		
		}

        /// <summary>
        /// Returns true if ValidationError contains a return date related
        /// error id, false otherwise 
        /// </summary>
        /// <returns>bool</returns>
        private bool ReturnDateErrorsExist() 
        {
            if(DateErrors != null) 
            {
                return 
                    DateErrors.Contains( ValidationErrorID.ReturnDateTimeInvalid ) ||
                    DateErrors.Contains( ValidationErrorID.ReturnDateTimeNotLaterThanNow ) ||
                    DateErrors.Contains( ValidationErrorID.OutwardAndReturnDateTimeInvalid ) ||
                    DateErrors.Contains( ValidationErrorID.ReturnDateTimeNotLaterThanOutwardDateTime ) ||
                    DateErrors.Contains( ValidationErrorID.OutwardAndReturnDateTimeNotLaterThanNow ) ||
                    DateErrors.Contains( ValidationErrorID.ReturnMonthMissing ) ||
                    DateErrors.Contains( ValidationErrorID.ReturnDateMissing ) ||
                    DateErrors.Contains( ValidationErrorID.ReturnTimeMissing );
            }
            else 
            {
                return false;
            }
		
        }

        /// <summary>
        /// Returns true if ValidationError contains a date related
        /// error id, false otherwise 
        /// </summary>
        /// <returns>bool</returns>
        private bool DateErrorsExist() 
        {
            return OutwardDateErrorsExist() || ReturnDateErrorsExist();
        }


		/// <summary>
		///  Read only property returning true if outward datetime is
		///  invalid, false otherwise
		/// </summary>
		public bool InvalidOutward
		{
			get 
			{ 
				return errors.Contains(ValidationErrorID.OutwardDateTimeInvalid);
			}
		}
        

		/// <summary>
		///  Read only property returning true if return datetime is
		///  invalid, false otherwise
		/// </summary>
		public bool InvalidReturn
		{
			get 
			{ 
				return errors.Contains(ValidationErrorID.ReturnDateTimeInvalid); 
			}
			
		}

		/// <summary>
		///  Read only property returning true if outward and return 
		///  datetimes are invalid, false otherwise
		/// </summary>
		public  bool InvalidDates
		{
			get 
			{ 
				return errors.Contains(ValidationErrorID.OutwardAndReturnDateTimeInvalid); 
			}
		}

		/// <summary>
		/// Read only property, returning true if the outward
		/// date is in the past
		/// </summary>
		public bool OutwardDateInPast 
		{
			get 
			{
				return errors.Contains(ValidationErrorID.OutwardDateTimeNotLaterThanNow);
			}
		}

		/// <summary>
		/// Read only property, returning true if the return
		/// date is in the past
		/// </summary>
		public bool ReturnDateInPast		
		{
			get 
			{
				return errors.Contains(ValidationErrorID.ReturnDateTimeNotLaterThanNow);
			}
		}

		/// <summary>
		/// Read only property, returning true if the outward
		/// and return date is in the past
		/// </summary>
		public bool AreDatesPast
		{
			get 
			{
				return errors.Contains(ValidationErrorID.OutwardAndReturnDateTimeNotLaterThanNow);
			}
		}

		/// <summary>
		/// Read only property, returning true if the return date selected 
		/// is after the outward date selected
		/// </summary>
		public bool ReturnBeforeOutward 
		{
			get 
			{
				return errors.Contains(ValidationErrorID.ReturnDateTimeNotLaterThanOutwardDateTime);
			}
		}

		/// <summary>
		/// Read only property, returning true if the return
		/// month is missing
		/// </summary>
		public bool ReturnMonthMissing 
		{
			get 
			{
				return errors.Contains(ValidationErrorID.ReturnMonthMissing);
			}
		}

		/// <summary>
		/// Read only property, returning true if the return
		/// time is missing
		/// </summary>
		public bool ReturnTimeMissing 
		{
			get 
			{
				return errors.Contains(ValidationErrorID.ReturnTimeMissing);
			}
		}

		/// <summary>
		/// Read only property, returning true if the return
		/// date is missing
		/// </summary>
		public bool ReturnDateMissing 
		{
			get 
			{
				return errors.Contains(ValidationErrorID.ReturnDateMissing);
			}
		}

		/// <summary>
		/// Read only property, returning true if the extend to  
		/// start outward arrive/leave by time is in the past
		/// </summary>
		public bool OutwardExtensionToStartInPast
		{   
			get
			{
				return errors.Contains(ValidationErrorID.ExtendToStartOutwardInPast);
			}
		}

		/// <summary>
		/// Read only property, returning true if the extend from 
		/// end outward arrive/leave by time is in the past
		/// </summary>
		public bool OutwardExtensionFromEndInPast 
		{
			get 
			{
				return errors.Contains(ValidationErrorID.ExtendFromEndOutwardInPast);
			}
		}

		/// <summary>
		/// Read only property, returning true if the extend to  
		/// start return arrive/leave by time is in the past
		/// </summary>
		public bool ReturnExtensionToStartInPast
		{   
			get
			{
				return errors.Contains(ValidationErrorID.ExtendToStartReturnInPast);
			}
		}

		/// <summary>
		/// Read only property, returning true if the extend from  
		/// end return arrive/leave by time is in the past
		/// </summary>
		public bool ReturnExtensionFromEndInPast 
		{
			get 
			{
				return errors.Contains(ValidationErrorID.ExtendFromEndReturnInPast);
			}
		}

		/// <summary>
		/// Read only property, returning true if an extension  
		/// outward or return arrive/leave by time is in the past
		/// </summary>
		public bool ExtendIntoPast
		{   
			get
			{
				return 
					errors.Contains( ValidationErrorID.ExtendFromEndOutwardInPast ) || 
					errors.Contains( ValidationErrorID.ExtendFromEndReturnInPast ) || 
					errors.Contains( ValidationErrorID.ExtendToStartOutwardInPast ) || 
					errors.Contains( ValidationErrorID.ExtendToStartReturnInPast );
			}
		}


		/// <summary>
		/// Read only property, returning true if the outward and return date is 
		/// invalid or the return time is not later than outward date time
		/// </summary>
		public bool ExtensionReturnOverlap
		{   
			get
			{
				return (errors.Contains(ValidationErrorID.ReturnDateTimeNotLaterThanOutwardDateTime) || 
					errors.Contains(ValidationErrorID.OutwardAndReturnDateTimeInvalid));
			}
		}

		#endregion
        
		#region Public Methods
		/// <summary>
        /// Initialises the underlying date controls with the supplied parameters
        /// </summary>
        /// <param name="outward">True if the control is for outward date/time</param>
        /// <param name="hour">initial hour value in format "hh"</param>
        /// <param name="minute">initial minute value in format "mm"</param>
        /// <param name="day">initial day of month value in format "dd"</param>
        /// <param name="monthYear">initial month/year value in format "mm/yyyy"</param>
        /// <param name="arriveBefore">True if arrive by time, false if leave after time</param>
        public void Populate( string hour, string minute, string day, string monthYear, bool arriveBefore) 
        {
			triDateControl.Populate(hour,minute,day,monthYear,arriveBefore);
        }

		/// <summary>
		/// Initialises the underlying date controls with the supplied parameters.
		/// Overloaded method that also sets the flexibility drop down as well.
		/// </summary>
		/// <param name="outward">True if the control is for outward date/time</param>
		/// <param name="hour">initial hour value in format "hh"</param>
		/// <param name="minute">initial minute value in format "mm"</param>
		/// <param name="day">initial day of month value in format "dd"</param>
		/// <param name="monthYear">initial month/year value in format "mm/yyyy"</param>
		/// <param name="arriveBefore">True if arrive by time, false if leave after time</param>
		/// <param name="flexibility">Date flexibility</param>
		public void Populate(string hour, string minute, string day, string monthYear, bool arriveBefore, int flexibility)
		{
			//Call overloaded method on the triDateControl that sets the flexibility as well.
			triDateControl.Populate(hour,minute,day,monthYear,arriveBefore,flexibility);
		}

		/// <summary>
		/// Initialises the underlying date controls with the supplies parameters.
		/// Automatically inserts values for hour (00), minute (00) and arriveBefore (true)
		/// </summary>
		/// <param name="day">Initial day of month value in format "dd"</param>
		/// <param name="monthYear">Initial month/year value in format "mm/yyyy"</param>
		/// <param name="flexibility">Date flexibility</param>
		public void Populate(string day, string monthYear, int flexibility)
		{
			triDateControl.Populate("00", "00", day, monthYear, true, flexibility);
		}

		public void RaiseDateChangedEvent()
		{
			// if changed, then raise event
			if (DateChanged != null)
				DateChanged(this, new EventArgs());
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

        }
		#endregion

	}
}
