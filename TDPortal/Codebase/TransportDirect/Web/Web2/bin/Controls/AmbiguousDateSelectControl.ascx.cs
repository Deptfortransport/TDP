// *********************************************** 
// NAME                 : AmbiguousDateSelectControl.ascx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 22/09/2003 
// DESCRIPTION  : Date select control for the ambiguity page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AmbiguousDateSelectControl.ascx.cs-arc  $ 
//
//   Rev 1.5   Sep 16 2011 09:44:22   DLane
//WAI additional changes
//Resolution for 5738: WAI additional changes
//
//   Rev 1.4   Mar 30 2010 16:01:18   mmodi
//Change calendar to be an ImageButton to correct accesability reader issue
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.3   Oct 08 2008 10:22:16   mturner
//Updated for XHTML compliance
//
//   Rev 1.2.1.0   Sep 15 2008 10:48:50   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:19:02   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:52   mturner
//Initial revision.
//
//   Rev 1.40   Mar 24 2006 12:29:20   mtillett
//Change date control to use CSS style instead of cellspacing. The 3px padding from the right of the table is removed for use on the home page.
//Resolution for 3608: Homepage Phase 2:  Mini planner date/time control wraps onto second line
//Resolution for 3617: Homepage phase 2: alignment of Find A Place & Plan A Journey buttons
//Resolution for 3618: Homepage phase 2: unusual display of homepage zones on Mac/Safari
//
//   Rev 1.39   Feb 23 2006 16:10:26   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.38   Dec 16 2005 14:12:28   mtillett
//Remove hrs and min labels
//Resolution for 3082: UEE: Netscape - Quick Planner - "Leave on" wraps over two lines
//
//   Rev 1.37   Nov 17 2005 16:28:22   pcross
//Merge fix
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.36   Nov 17 2005 13:00:16   pcross
//Manual merge of stream2880
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.35   Nov 09 2005 14:03:30   NMoorhouse
//TD93 - UEE Input Pages - Update Visit Planner
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.34.1.4   Nov 14 2005 10:15:06   AViitanen
//Updated for screenreader.
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.36   Nov 11 2005 17:05:10   AViitanen
//Updated so date text is visible for screenreader.
//
//   Rev 1.34.1.2   Nov 09 2005 11:08:06   RGriffith
//Fixes to MonthListResourceId to make it independent from ShortLayoutMode
//
//   Rev 1.34.1.1   Nov 08 2005 14:38:40   pcross
//Fix to allow ShortLayoutMode property not to be intialised.
//
//   Rev 1.34.1.0   Nov 07 2005 15:31:08   RGriffith
//Changes to control to allow for ShortLayoutMode
//
//   Rev 1.34   Nov 04 2005 10:57:14   NMoorhouse
//Manual merge of stream2816
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.33   Nov 01 2005 15:11:40   build
//Automatically merged from branch for stream2638
//
//   Rev 1.32.1.1.1.3   Nov 02 2005 16:52:26   RGriffith
//Calendar Alternate Text added
//
//   Rev 1.32.1.1.1.2   Oct 28 2005 14:11:12   NMoorhouse
//Set screen reader labels
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.32.1.1.1.1   Oct 27 2005 14:01:46   NMoorhouse
//TD93 - UEE Input Pages, Date Control element CUT
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.32.1.1.1.0   Oct 25 2005 11:18:24   NMoorhouse
//TD93 - Branched for Input Pages
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.32.1.2   Oct 25 2005 11:29:22   jbroome
//Fixed bugs in control for Visit Planner
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.32.1.1   Oct 17 2005 10:03:52   asinclair
//Added Override of OnPreRender()
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.32.1.0   Oct 07 2005 15:32:08   asinclair
//Added ErrorMode bool and Populate() method for use by VP
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.32   Aug 18 2005 11:30:16   jgeorge
//Automatically merged from branch for stream2558
//
//   Rev 1.31.1.0   Jul 01 2005 09:49:40   jmorrissey
//Added DateLabel property
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.31   Mar 04 2005 11:20:58   tmollart
//Removed populate method that sets flexibility. Control now uses a property to set flexibility.
//Renamed property that sets flexibility controls visible for consistency.
//Added methods to retain internal flexibility value on the view state. Required as controls viewstate only retains label text and not the data services look up value.
//
//   Rev 1.30   Jan 20 2005 15:43:50   tmollart
//Updated as follows:
//New populate method that allows flexibility to be set.
//Properties for controlling visibility of flexibility and time controls.
//New property for setting and returning current flexibility value.
//
//   Rev 1.29   Oct 28 2004 11:28:26   esevern
//IR1692: amended TDCultureInfo to use CurrentUICulture - correction for welsh text not being displayed when 'anytime' selected.
//
//   Rev 1.28   Sep 06 2004 11:46:44   jbroome
//IR 1474 - Exposed ControlHours and ControlMinutes drop down lists so that the Any Time list items can be added if necesary.
//
//   Rev 1.27   Jul 28 2004 16:11:08   passuied
//Changes to make the FindA date controls work
//
//   Rev 1.26   Jul 28 2004 11:59:38   passuied
//In populate Method, clear the month list before repopulating it.
//
//   Rev 1.25   Jul 27 2004 17:00:00   COwczarek
//Remove redundant AllowAnyTime method
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.24   Jun 09 2004 16:31:30   jgeorge
//Modified "Any time" functionality
//
//   Rev 1.22   Apr 06 2004 16:41:32   COwczarek
//Modify Populate method to repopulate control contents on post
//back. Current design relies on control retaining date values in
//viewstate. This will not be the case if this control is made
//invisible then visible again which happens in some circumstances.
//Resolution for 695: DEL 5.2 QA Changes - Ambiguity page
//
//   Rev 1.21   Mar 10 2004 14:55:56   AWindley
//DEL5.2 Added label text for Screen Reader
//
//   Rev 1.20   Mar 10 2004 13:22:16   esevern
//DEL5.2 time labels now white text
//
//   Rev 1.19   Nov 24 2003 14:02:54   passuied
//fixed bug IR313
//Resolution for 313: Date error on Amend journey function
//
//   Rev 1.18   Nov 18 2003 11:49:04   passuied
//missing comments
//
//   Rev 1.17   Nov 18 2003 10:43:58   passuied
//changes to hopefully pass code review
//
//   Rev 1.16   Nov 05 2003 13:23:40   passuied
//fixed various problems with font size
//
//   Rev 1.15   Oct 28 2003 10:38:56   passuied
//changes after fxcop
//
//   Rev 1.14   Oct 20 2003 10:52:56   passuied
//Changes after fxcop
//
//   Rev 1.13   Oct 07 2003 17:14:10   passuied
//updated so when return is selected without day hour or minute the outward one are selected by default rather than current 
//
//   Rev 1.12   Oct 03 2003 13:34:16   PNorell
//Updated the new exception identifier.
//
//   Rev 1.11   Oct 02 2003 17:49:02   COwczarek
//id parameter passed in TDException constructor set to -1 to enable compilation after introduction of new TDException constructor which takes an enum type for id. This is a temporary fix - the constructor taking an
//id of type long will be removed.
//
//   Rev 1.10   Sep 26 2003 10:47:42   passuied
//latest working version
//
//   Rev 1.9   Sep 25 2003 18:36:28   passuied
//Added missing headers


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
	public partial  class AmbiguousDateSelectControl : TDUserControl
	{
		protected System.Web.UI.WebControls.TableRow flexibilityTableRow;


		private DataServices populator;

		//Property values, preset to true for time controls and false for
		//the flexibility controls.
		private bool timeControlsVisible = true;
		private bool flexibilityControlsVisible = false;
		private int flexibilityValue;
		private bool arriveByOption = true;
		private bool errorMode;
		private string monthListResourceId = "DateSelectControl.listMonths";
		private bool shortLayoutMode;

		protected AmbiguousDateSelectControl()
		{
			populator = (DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];	
		}


		/// <summary>
		/// Page Load method - sets image URL and Text.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{	
			commandCalendarImage.ImageUrl = GetResource("DateSelectControl.CalendarURL");
            commandCalendarImage.AlternateText = GetResource("DateSelectControl.commandCalendar.AlternateText");
            commandCalendarImage.ToolTip = GetResource("DateSelectControl.commandCalendar.AlternateText");
                        
			labelSRDate.Text = GetResource("outwardSelect.labelSRDate");	
			labelSRMonthYear.Text = GetResource("outwardSelect.labelSRMonthYear");
			labelSRHours.Text = GetResource("outwardSelect.labelSRHours");
			labelSRMinutes.Text = GetResource("outwardSelect.labelSRMinutes");
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
                        labelLeaveArrive.Visible = timeControlsVisible;
						listLeaveArrive.Visible = timeControlsVisible;
					}
					else
					{
						labelLeaveArrive.Visible = timeControlsVisible;
                        listLeaveArrive.Visible = timeControlsVisible;
					}
				}
				
				flexibilityLabel.Visible = false;
				flexibilitySRLabel.Visible = flexibilityControlsVisible;
				flexibilityDropDown.Visible = flexibilityControlsVisible;
			}
			// Bind control for simple data binding calls
			this.DataBind();

			// Determine if JavaScript is supported
			bool javaScriptSupported = bool.Parse((string) Session[((TDPage)Page).Javascript_Support]);
			string javaScriptDom = (string) Session[((TDPage)Page).Javascript_Dom];
			ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

			if (javaScriptSupported)
			{
				listMonths.EnableClientScript = true; 
				listHours.EnableClientScript = true; 
				listDays.EnableClientScript = true;
				listMonths.Action = string.Format("return MonthSelectionChanged('{0}')", this.ClientID);
				listHours.Action = string.Format("return HoursSelectionChanged('{0}')", this.ClientID);
				listDays.Action = string.Format("return DaySelectionChanged('{0}')", this.ClientID);
				Page.ClientScript.RegisterStartupScript(typeof(AmbiguousDateSelectControl),listMonths.ScriptName, scriptRepository.GetScript(listMonths.ScriptName, javaScriptDom));
				Page.ClientScript.RegisterStartupScript(typeof(AmbiguousDateSelectControl),listHours.ScriptName, scriptRepository.GetScript(listHours.ScriptName, javaScriptDom));
				Page.ClientScript.RegisterStartupScript(typeof(AmbiguousDateSelectControl),listDays.ScriptName, scriptRepository.GetScript(listDays.ScriptName, javaScriptDom));
			}
			else
			{
				listMonths.EnableClientScript = false; 
				listHours.EnableClientScript = false;
				listDays.EnableClientScript = false;
			}

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
            labelLeaveArrive.Text = populator.GetText(DataServiceType.LeaveArriveDrop, arriveBefore.ToString().ToLower(TDCultureInfo.CurrentUICulture));
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

                if (dt != null)
                {
                    listDays.SelectedIndex = dt.Day - 1;
                    listMonths.SelectedIndex = (dt.Year - TDDateTime.Now.Year) * 12 + dt.Month - TDDateTime.Now.Month; //index = selected month - current Month (as the 1st month in the list is the current month) --> relative index!
                }
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
				return commandCalendarImage;
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
				commandCalendarImage.Visible = !value;
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
			switch(this.PageId)
			{
				case PageId.Home:
					return "ambiguousdateselectcontrolshort";
				default:
					return "ambiguousdateselectcontrolnormal";
			}
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
