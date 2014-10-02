namespace TransportDirect.UserPortal.Web.Controls
{
	using System;	
	using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Text.RegularExpressions;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.Web.Support;
	using TransportDirect.Common;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.Common.PropertyService.Properties;
	using TransportDirect.Common.Logging;
	using Logger = System.Diagnostics.Trace;
	using System.Collections;
	using System.Globalization;
	using TransportDirect.UserPortal.SessionManager;

	/// <summary>
	///		Date select control for the ambiguity page
	/// </summary>
	public partial  class iFrameAmbiguousDateSelectControl : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.TableRow flexibilityTableRow;


		private DataServices populator;
		protected TDResourceManager resourceManager = Global.tdResourceManager;

		//Property values, preset to true for time controls and false for
		//the flexibility controls.
		private bool timeControlsVisible = true;
		private bool flexibilityControlsVisible = false;
		private int flexibilityValue;
		private bool arriveByOption = true;
		private bool errorMode;
		private string monthListResourceId = "DateSelectControl.listMonths";
		private bool shortLayoutMode;

		protected iFrameAmbiguousDateSelectControl()
		{
			populator = (DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];	
		}


		/// <summary>
		/// Page Load method - sets image URL and Text.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{	
			commandCalendar.ImageUrl = resourceManager.GetString("DateSelectControl.CalendarURL", TDCultureInfo.CurrentUICulture);
			commandCalendar.AlternateText = resourceManager.GetString("DateSelectControl.commandCalendar.AlternateText", TDCultureInfo.CurrentUICulture);
			labelSRDate.Text = resourceManager.GetString("outwardSelect.labelSRDate", TDCultureInfo.CurrentUICulture);	
			labelSRMonthYear.Text = resourceManager.GetString("outwardSelect.labelSRMonthYear", TDCultureInfo.CurrentUICulture);
			labelSRHours.Text = resourceManager.GetString("outwardSelect.labelSRHours", TDCultureInfo.CurrentUICulture);
			labelSRMinutes.Text = resourceManager.GetString("outwardSelect.labelSRMinutes", TDCultureInfo.CurrentUICulture);
			//flexibilitySRLabel.Text = GetResource("");	
		}

		/// <summary>
		/// Changes display of control if in Error Mode
		/// </summary>
		protected override void OnPreRender(EventArgs e)
		{
			if (errorMode == true)
			{
				cellDate.Attributes.Add("class", "alerterror");
				//Display relevant Ambiguous Labels and disable relevant Normal DropDowns
				labelLeaveArrive.Visible = timeControlsVisible;
				listLeaveArrive.Visible = false;
				flexibilityLabel.Visible = flexibilityControlsVisible;
				flexibilitySRLabel.Visible = false;
				flexibilityDropDown.Visible = false;
			}
			else
			{
				cellDate.Attributes.Remove("class");
				//Display relevant Normal DropDowns and disable relevant Ambiguous Labels
				if (shortLayoutMode)
				{
					labelLeaveArrive.Visible = false;
					listLeaveArrive.Visible = false;
				}
				else
				{
					if(arriveByOption)
					{
						labelLeaveArrive.Visible = false;
						listLeaveArrive.Visible = timeControlsVisible;
					}
					else
					{
						labelLeaveArrive.Visible = timeControlsVisible;
						listLeaveArrive.Visible = false;
					}
				}
				
				flexibilityLabel.Visible = false;
				flexibilitySRLabel.Visible = flexibilityControlsVisible;
				flexibilityDropDown.Visible = flexibilityControlsVisible;
			}
			// Bind control for simple data binding calls
			this.DataBind();

			// Determine if JavaScript is supported
//			bool javaScriptSupported = bool.Parse((string) Session[((TDPage)Page).Javascript_Support]);
//			string javaScriptDom = (string) Session[((TDPage)Page).Javascript_Dom];
//			bool javaScriptSupported = false;
//			string javaScriptDom = "JavaScriptDom";
//
//			ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
//
//			if (javaScriptSupported)
//			{
//				listMonths.EnableClientScript = true; 
//				listHours.EnableClientScript = true; 
//				listDays.EnableClientScript = true;
//				listMonths.Action = string.Format("return MonthSelectionChanged('{0}')", this.ClientID);
//				listHours.Action = string.Format("return HoursSelectionChanged('{0}')", this.ClientID);
//				listDays.Action = string.Format("return DaySelectionChanged('{0}')", this.ClientID);
//				Page.RegisterStartupScript(listMonths.ScriptName, scriptRepository.GetScript(listMonths.ScriptName, javaScriptDom));
//				Page.RegisterStartupScript(listHours.ScriptName, scriptRepository.GetScript(listHours.ScriptName, javaScriptDom));
//				Page.RegisterStartupScript(listDays.ScriptName, scriptRepository.GetScript(listDays.ScriptName, javaScriptDom));
//			}
//			else
//			{
//				listMonths.EnableClientScript = false; 
//				listHours.EnableClientScript = false;
//				listDays.EnableClientScript = false;
//			}

		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected string GetCssStyle()
		{
			if (errorMode)
				return "txtsevenwhite";
			else
				return "txtseven";
		}

		private ListItem GetAnyTimeItem()
		{
			return listHours.Items.FindByValue("Any");
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

		#region Public Methods

		/// <summary>
		/// Populate the control
		/// </summary>
		/// <param name="day">day</param>
		/// <param name="monthYear">month and year</param>
		/// <param name="hour">hour</param>
		/// <param name="minute">minute</param>
		/// <param name="arriveBefore">arrive by(true) / leave after(false)</param>
		public void Populate(string day, string monthYear, string hour, string minute, bool arriveBefore)
		{
			// Populate the controls
			// listMonths
			string[]months = Global.tdResourceManager.GetString(MonthListResourceId, TDCultureInfo.CurrentUICulture).Split(',');
			listMonths.Items.Clear();
			int currentMonth = DateTime.Now.Month-1; //conversion to have months from 0-11
			int numberOfMonths ;
			try
			{
				numberOfMonths = Convert.ToInt32(Properties.Current["controls.numberofmonths"], CultureInfo.CurrentUICulture.NumberFormat);
			}
			catch (Exception)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Infrastructure,
					TDTraceLevel.Error,
					"Unset/Bad format 'controls.numberofmonths' property");

				Logger.Write(oe);

				throw new TDException("Unset/Bad format 'controls.numberofmonths' properties", true, TDExceptionIdentifier.BTCInvalidNumberOfMonths);

			}
			string monthYearSeparator = Properties.Current["journeyplanner.monthyearseparator"];
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
				string monthToAddDigit = string.Format(TDCultureInfo.CurrentUICulture,
					"{0:D2}",(monthToAdd%12)+1); // convert month to be in 1-12 range
				ListItem liMonth = new ListItem(strMonth +" " +strYear, 
					monthToAddDigit +monthYearSeparator +strYear);

				listMonths.Items.Insert(0,liMonth);
			}

			// if month selected but other items not, select outward(day, hour, min)
			TDJourneyParameters journeyParameters = TDSessionManager.Current.JourneyParameters;
			try
			{
				listDays.SelectedIndex = Convert.ToInt32(day,CultureInfo.CurrentCulture.NumberFormat)-1; 
			}
			catch
			{
				listDays.SelectedIndex = Convert.ToInt32(journeyParameters.OutwardDayOfMonth,CultureInfo.CurrentCulture.NumberFormat)-1; 
			}
			populator.Select(listMonths, monthYear);

			try
			{
				listHours.SelectedIndex = Convert.ToInt32(hour,CultureInfo.CurrentCulture.NumberFormat);				
			}
			catch
			{
				// Add this code just in case the outward value, is not valid either.
				// default is select 1st item in list
				try
				{
					listHours.SelectedIndex = Convert.ToInt32(journeyParameters.OutwardHour,CultureInfo.CurrentCulture.NumberFormat);				
				}
				catch
				{
					// If outward not filled yet, then Initialise it to its default value, as it would be done in
					// JourneyParameters.Initialise() method
					TDSessionManager.Current.JourneyParameters.InitialiseDefaultOutwardTime();
					listHours.SelectedIndex = Convert.ToInt32(journeyParameters.OutwardHour,CultureInfo.CurrentCulture.NumberFormat);				
					
				}
			}
			try
			{
				listMinutes.SelectedIndex = Convert.ToInt32(minute,CultureInfo.CurrentCulture.NumberFormat)/5; // increments of 5				
			}
			catch
			{
				// Add this code just in case the outward value, is not valid either.
				// default is select 1st item in list
				try
				{
					listMinutes.SelectedIndex = (int)(Convert.ToInt32(journeyParameters.OutwardMinute,CultureInfo.CurrentCulture.NumberFormat)/5); // increments of 5				
				}
				catch
				{
					// If outward not filled yet, then Initialise it to its default value, as it would be done in
					// JourneyParameters.Initialise() method
					TDSessionManager.Current.JourneyParameters.InitialiseDefaultOutwardTime();
					listMinutes.SelectedIndex = (int)(Convert.ToInt32(journeyParameters.OutwardMinute,CultureInfo.CurrentCulture.NumberFormat)/5); // increments of 5				
					
					
				}
			}
			labelLeaveArrive.Text = populator.GetText(DataServiceType.LeaveArriveDrop, arriveBefore.ToString().ToLower(TDCultureInfo.CurrentUICulture));
			populator.LoadListControl(DataServiceType.LeaveArriveDrop, listLeaveArrive);
		}

		/// <summary>
		/// Populates the control
		/// </summary>
		public void Populate()
		{
			// listMonths
			string[]months = Global.tdResourceManager.GetString(MonthListResourceId, TDCultureInfo.CurrentUICulture).Split(',');
			listMonths.Items.Clear();
			int currentMonth = DateTime.Now.Month-1; //conversion to have months from 0-11
			int numberOfMonths ;
			try
			{
				numberOfMonths = Convert.ToInt32(Properties.Current["controls.numberofmonths"], CultureInfo.CurrentUICulture.NumberFormat);
			}
			catch (Exception)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Infrastructure,
					TDTraceLevel.Error,
					"Unset/Bad format 'controls.numberofmonths' property");

				Logger.Write(oe);

				throw new TDException("Unset/Bad format 'controls.numberofmonths' properties", true, TDExceptionIdentifier.BTCInvalidNumberOfMonths);

			}
			string monthYearSeparator = Properties.Current["journeyplanner.monthyearseparator"];
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
				string monthToAddDigit = string.Format(TDCultureInfo.CurrentUICulture,
					"{0:D2}",(monthToAdd%12)+1); // convert month to be in 1-12 range
				ListItem liMonth = new ListItem(strMonth +" " +strYear, 
					monthToAddDigit +monthYearSeparator +strYear);

				listMonths.Items.Insert(0,liMonth);

			}
			populator.LoadListControl(DataServiceType.LeaveArriveDrop, listLeaveArrive);

			// Flexibility drop down
			populator.LoadListControl(DataServiceType.DateFlexibilityDrop, flexibilityDropDown);

			labelLeaveArrive.Text = populator.GetText(DataServiceType.LeaveArriveDrop, false.ToString().ToLower(TDCultureInfo.CurrentUICulture));

		}



		/// <summary>
		/// Populate the control with "Any" specified as the time
		/// </summary>
		/// <param name="day">day</param>
		/// <param name="monthYear">month and year</param>
		/// <param name="arriveBefore">arrive by(true) / leave after(false)</param>
		public void Populate(string day, string monthYear, bool arriveBefore)
		{
			// Populate the controls
			// listMonths
			string[]months = Global.tdResourceManager.GetString(MonthListResourceId, TDCultureInfo.CurrentUICulture).Split(',');
			listMonths.Items.Clear();
			int currentMonth = DateTime.Now.Month-1; //conversion to have months from 0-11
			int numberOfMonths ;
			try
			{
				numberOfMonths = Convert.ToInt32(Properties.Current["controls.numberofmonths"], CultureInfo.CurrentUICulture.NumberFormat);
			}
			catch (Exception)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Infrastructure,
					TDTraceLevel.Error,
					"Unset/Bad format 'controls.numberofmonths' property");

				Logger.Write(oe);

				throw new TDException("Unset/Bad format 'controls.numberofmonths' properties", true, TDExceptionIdentifier.BTCInvalidNumberOfMonths);

			}
			string monthYearSeparator = Properties.Current["journeyplanner.monthyearseparator"];
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
				string monthToAddDigit = string.Format(TDCultureInfo.CurrentUICulture,
					"{0:D2}",(monthToAdd%12)+1); // convert month to be in 1-12 range
				ListItem liMonth = new ListItem(strMonth +" " +strYear, 
					monthToAddDigit +monthYearSeparator +strYear);

				listMonths.Items.Insert(0,liMonth);
			}
			//populate leave arrive by
			populator.LoadListControl(DataServiceType.LeaveArriveDrop, listLeaveArrive);

			// if month selected but other items not, select outward(day, hour, min)
			TDJourneyParameters journeyParameters = TDSessionManager.Current.JourneyParameters;
			try
			{
				listDays.SelectedIndex = Convert.ToInt32(day,CultureInfo.CurrentCulture.NumberFormat)-1; 
			}
			catch
			{
				listDays.SelectedIndex = Convert.ToInt32(journeyParameters.OutwardDayOfMonth,CultureInfo.CurrentCulture.NumberFormat)-1; 

			}
			populator.Select(listMonths, monthYear);

			listHours.SelectedIndex = listHours.Items.IndexOf(GetAnyTimeItem());
			listMinutes.SelectedIndex = listMinutes.Items.IndexOf(listMinutes.Items.FindByText("-"));

			labelLeaveArrive.Text = populator.GetText(DataServiceType.LeaveArriveDrop, arriveBefore.ToString().ToLower(TDCultureInfo.CurrentUICulture));
		
		}
		#endregion

		#region Properties Get/Set selected values in controls
		/// <summary>
		/// Gets/Sets the current TDDateTime for the control
		/// </summary>
		public TDDateTime Current
		{
			get
			{
				int day, month, year;//, hour, minute;
				try
				{
					day = Convert.ToInt16(listDays.SelectedItem.Text, 
						TDCultureInfo.CurrentCulture.NumberFormat);
					month = Convert.ToInt16(listMonths.SelectedItem.Value.Substring(0,2),
						TDCultureInfo.CurrentCulture.NumberFormat);
					year = Convert.ToInt16 (listMonths.SelectedItem.Value.Substring(3,4),
						TDCultureInfo.CurrentCulture.NumberFormat);

					
				}
				catch
				{
					return null;
				}
				
				try
				{
					return new TDDateTime(year, month, day, 0, 0,0);
					
				}
				catch (ArgumentOutOfRangeException)
				{
					return null;
				}

			}

		
			set
			{
				TDDateTime dt = value;

				listDays.SelectedIndex = dt.Day -1;
				listMonths.SelectedIndex = (dt.Year - TDDateTime.Now.Year)*12 + dt.Month - TDDateTime.Now.Month; //index = selected month - current Month (as the 1st month in the list is the current month) --> relative index!

				
				
			}
				
		}

		/// <summary>
		/// Gets/Sets Day
		/// </summary>
		public string Day
		{
			get
			{
				return listDays.SelectedItem.Value;
			}
			set
			{

				int dayToSelect;
				try
				{
					dayToSelect = Convert.ToInt32(value, TDCultureInfo.InvariantCulture.NumberFormat);

				}
			
				catch (FormatException)
				{
					// if exception means, value is No Return/Open return --> select last index ("-")				
					listDays.SelectedIndex = listDays.Items.Count-1;
					return;
				}

				listDays.SelectedIndex = dayToSelect-1; // day is from 1 to last day of month
				// index is from 0 to last day of month -1


			}
		}

		/// <summary>
		/// Indicates if the control is a return/Outward. If return, the month list will have a bigger number of items
		/// </summary>
		private bool IsReturnControl
		{
			get
			{
				int numberOfMonths;
				try
				{
					numberOfMonths= Convert.ToInt32(Properties.Current["numberofmonths"],CultureInfo.CurrentCulture.NumberFormat);
				}
				catch
				{
					OperationalEvent oe = new OperationalEvent(
						TDEventCategory.Infrastructure,
						TDTraceLevel.Error,
						"Unset/Bad format 'controls.numberofmonths' property");

					Logger.Write(oe);

					throw new TDException("Unset/Bad format 'controls.numberofmonths' properties", true, TDExceptionIdentifier.BTCInvalidNumberOfMonths);

				}

				if (listMonths.Items.Count > numberOfMonths)
					return true;
				else
					return false;
			}
		}

		/// <summary>
		/// Gets/Sets the Month and Year
		/// </summary>
		public string MonthYear
		{
			get
			{
				
				ListItem itemSelected = listMonths.SelectedItem;
				// if Return case (count = numberofmonths +2), check that No Return or Open return not selected
				if (IsReturnControl)
				{
					ArrayList data = populator.GetList(DataServiceType.ReturnMonthYearDrop);

					foreach (DSDropItem item in data)
					{
						if (itemSelected.Value == item.ResourceID)
							return item.ItemValue;
					}
				
				}
				
				// Generic case. Returns the MonthYear string
				return itemSelected.Value;
				

			}

			set
			{
				// We expect MonthYear in format "MM<monthYearSeparator>YYYY" OR be OPenReturn/NoReturn. Check that it is the actual format
				string monthYearSeparator = Properties.Current["journeyplanner.monthyearseparator"];
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
				// Check the format is respected
				Regex formatValidator = new Regex("[0-9]{2}" +monthYearSeparator +"[0-9]{4}");
				


				string valueToSelect = value;
				// If Return control test if OpenReturn/No return is selected
				if (IsReturnControl)
				{
					string resourceId = populator.GetResourceId(DataServiceType.ReturnMonthYearDrop, value);
					if (resourceId.Length != 0)
						valueToSelect = resourceId;
					else
					{
						// if have a return date check correct format
						if (!formatValidator.IsMatch(valueToSelect))
						{
							OperationalEvent oe = new OperationalEvent(
								TDEventCategory.Infrastructure,
								TDTraceLevel.Error,
								"Bad format for MonthYear value(correct is 'MM/YYYY')");

							Logger.Write(oe);

							throw new TDException("Bad format for MonthYear value (correct is 'MM/YYYY')",
								true,
								TDExceptionIdentifier.BTCBadMonthYearFormat);

						}
						

					}
				}
				else
				{
					// if have a return date check correct format
					if (!formatValidator.IsMatch(valueToSelect))
					{
						OperationalEvent oe = new OperationalEvent(
							TDEventCategory.Infrastructure,
							TDTraceLevel.Error,
							"Bad format for MonthYear value(correct is 'MM/YYYY')");

						Logger.Write(oe);

						throw new TDException("Bad format for MonthYear value (correct is 'MM/YYYY')",
							true,
							TDExceptionIdentifier.BTCBadMonthYearFormat);

					}
				}
				
				// reset selection
				listMonths.SelectedIndex = -1;
				for (int i=0; i<listMonths.Items.Count; i++)
				{
					ListItem item = listMonths.Items[i];
					if (valueToSelect == item.Value)
					{
						listMonths.SelectedIndex = i;
						break;
					}
				}

				// security. if didn't work, select first item by default
				if (listMonths.SelectedIndex == -1)
					listMonths.SelectedIndex = 0;

			}
		}

		/// <summary>
		/// Gets/Sets if ArriveBefore /Leave after
		/// </summary>
		public bool ArriveBefore
		{
			get
			{
				bool arriveBefore = Convert.ToBoolean(populator.GetValue
					(DataServiceType.LeaveArriveDrop, listLeaveArrive.SelectedItem.Value)
					);

				
				return arriveBefore;
			}

			set
			{
				string resourceId = populator.GetResourceId(DataServiceType.LeaveArriveDrop, value.ToString(TDCultureInfo.CurrentCulture).ToLower(TDCultureInfo.CurrentCulture));

				for (int i=0; i< listLeaveArrive.Items.Count; i++)
				{
					ListItem item = listLeaveArrive.Items[i];
					if (item.Value == resourceId)
					{
						listLeaveArrive.SelectedIndex = i;
						break;
					}
						
				}


			}	
		}

		/// <summary>
		/// Gets/Sets Hour
		/// </summary>
		public string Hour
		{
			get
			{
				return listHours.SelectedItem.Value;
			}

			set
			{
				// if not a number then it's should be the last index selected (no selection "-")
				int hour;
				try
				{
					hour = Convert.ToInt32(value, CultureInfo.CurrentCulture.NumberFormat);
				}
				catch
				{
					listHours.SelectedIndex = listHours.Items.Count-1;
					return;
				}

				listHours.SelectedIndex = hour;

			}
		}

		/// <summary>
		/// Gets/Sets Minute
		/// </summary>
		public string Minute
		{
			get
			{
				return listMinutes.SelectedItem.Value;
			}
			set
			{
			
				// if not a number then it's should be the last index selected (no selection "-")
				int minute;
				try
				{
					minute = Convert.ToInt32(value, CultureInfo.CurrentCulture.NumberFormat);
				}
				catch
				{
					listMinutes.SelectedIndex = listMinutes.Items.Count-1;
					return;
				}

				listMinutes.SelectedIndex = minute/5; // div/5 because increments of 5
			}
		}

		/// <summary>
		/// Gets the Day DropDownList
		/// </summary>
		public DropDownList ControlDays
		{
			get
			{
				return listDays;
			}
		}

		/// <summary>
		/// Gets the Month dropdownlist
		/// </summary>
		public DropDownList ControlMonths
		{
			get
			{
				return listMonths;
			}
		}

		/// <summary>
		/// Gets the Arrive by/Leave After radiobuttonlist
		/// </summary>
		public DropDownList ControlTimeType
		{
			get
			{
				return listLeaveArrive;
			}
		}

		/// <summary>
		/// gets the Hour dropdownlist
		/// </summary>
		public DropDownList ControlHours
		{
			get
			{	
				return listHours;
			}
		}
		/// <summary>
		/// Gets the Minute dropdownlist
		/// </summary>
		public DropDownList ControlMinutes
		{
			get
			{
				return listMinutes;
			}
		}

		#endregion
		
		/// <summary>
		/// Get Property. Returns the calendar button
		/// </summary>
		public ImageButton ButtonCalendar
		{
			get
			{
				return commandCalendar;
			}
		}

		/// <summary>
		/// Gets the flexibility screen reader label.
		/// </summary>
		public Label FlexibilityScreenReaderLabel
		{
			get
			{
				return flexibilitySRLabel;
			}
		}

		/// <summary>
		/// Read/Write. Sets/gets the visibility of the time controls.
		/// </summary>
		public bool TimeControlsVisible
		{
			get {return timeControlsVisible;}
			set 
			{
				//Note: Set for each control as placing in a panel caused
				//the time controls to appear in a new line in the table.
				timeControlsVisible = value;
				labelLeaveArrive.Visible = value;
				listLeaveArrive.Visible = value;
				labelSRHours.Visible = value;
				listHours.Visible = value;
				labelSRMinutes.Visible = value;
				listMinutes.Visible = value;
			}
		}

		/// <summary>
		/// Read/Write. Gets/Sets the visibility of the flexiibility label.
		/// </summary>
		public bool FlexibilityControlsVisible
		{
			get {return flexibilityControlsVisible;}
			set
			{
				flexibilityControlsVisible = value;
				flexibilityLabel.Visible = value;
				flexibilitySRLabel.Visible = value;
				flexibilityDropDown.Visible = value;
			}
		}

		/// <summary>
		/// Read/Write: Sets whether Arriving By Option is valid on child ambiguousSelectControl
		/// </summary>
		public bool ArriveByOption
		{
			get {return arriveByOption;}
			set
			{
				arriveByOption = value;
			}
		}

		/// <summary>
		/// Read/Write. Gets/Sets the flexibility label. Note this sets and returns
		/// the lookup value from dataservices and not the actual text.
		/// </summary>
		public int Flexibility
		{
			get 
			{
				populator = (DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
				return Convert.ToInt32(populator.GetValue(DataServiceType.DateFlexibilityDrop, flexibilityDropDown.SelectedItem.Value));
			}
			set 
			{
				populator = (DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
				populator.Select(flexibilityDropDown, populator.GetResourceId(DataServiceType.DateFlexibilityDrop, value.ToString()));
				flexibilityLabel.Text = "(" + populator.GetText(DataServiceType.DateFlexibilityDrop, value.ToString()) + ")";
			}

		}

		/// <summary>
		/// Read/Write. Gets/sets the labelSRDate label
		/// </summary>
		public Label DateLabel
		{
			get
			{
				return labelSRDate;
			}
			set
			{
				labelSRDate = value;
			}
		}

		/// <summary>
		/// Read/Write. Whether the control should be shown in error mode.
		/// </summary>
		public bool ErrorMode
		{
			get
			{
				return errorMode;
			}
			set
			{
				errorMode = value;
			}
		}

		/// <summary>
		/// Read/Write. Property to represent the month string in langstrings.
		/// Defaults to long months name but can be set to short values.
		/// </summary>
		public string MonthListResourceId
		{
			get
			{
				return monthListResourceId;
			}
			set
			{
				monthListResourceId = value;
			}
		}

		/// <summary>
		/// Read/Write. Property to hide labels/calendar image for short layout mode.
		/// Sets the visibility of appropriate labels.
		/// </summary>
		public bool ShortLayoutMode
		{
			get
			{
				return shortLayoutMode;
			}
			set
			{
				shortLayoutMode = value;
				labelLeaveArrive.Visible = !value;
				commandCalendar.Visible = !value;
				flexibilityDropDown.Visible = !value;
				listLeaveArrive.Visible = !value;
			}
		}
		/// <summary>
		/// Returns to CSS style to be used by the table element on this control
		/// </summary>
		/// <returns>CSS style to use</returns>
		protected string TableCSS()
		{
			return "ambiguousdateselectcontrolshort";
		}

		protected override void LoadViewState(object savedState) 
		{
			if (savedState != null)
			{
				// Load State from the array of objects that was saved at ;
				// SavedViewState.
				object[] myState = (object[])savedState;
				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				if (myState[1] != null)
					flexibilityValue = (int)myState[1];
				else
					flexibilityValue = 1;
			}
		}

		protected override object SaveViewState()
		{  
			// Save State as a cumulative array of objects.
			object[] allStates = new object[2];
			allStates[0] = base.SaveViewState();
			allStates[1] = flexibilityValue;
			return allStates;
		}

	}
}
