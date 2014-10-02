// *********************************************** 
// NAME                 :  DateSelectControl.ascx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 29/07/2003 
// DESCRIPTION  : Control allowing date and time input. used for Data capture
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/DateSelectControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Jan 26 2009 12:55:32   apatel
//No change.
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:19:50   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:02   mturner
//Initial revision.
//
//   Rev 1.48   Mar 24 2006 17:26:54   kjosling
//Merged stream 0023
//
//   Rev 1.47.1.1   Mar 14 2006 12:02:42   kjosling
//Check to stop control repopulating on postback
//
//   Rev 1.47.1.0   Mar 13 2006 10:20:02   kjosling
//Changed conditions which determine whether the control is for a return journey
//Resolution for 23: DEL 8.1 Workstream - Journey Results - Phase 1
//
//   Rev 1.47   Feb 23 2006 19:16:28   build
//Automatically merged from branch for stream3129
//
//   Rev 1.46.1.0   Jan 10 2006 15:23:58   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.46   Nov 03 2005 16:18:46   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.45.1.0   Nov 02 2005 16:54:42   RGriffith
//Calendar Alternate Text Added
//
//   Rev 1.45   Sep 29 2005 12:46:04   build
//Automatically merged from branch for stream2673
//
//   Rev 1.44.1.0   Sep 07 2005 13:45:38   rgreenwood
//DN079 UEE ES015 24 Hour Help Button removal
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.44   Apr 15 2005 15:12:54   tmollart
//Flexibility drop down visibility now controlled by table row.
//Resolution for 2090: PT: Layout issues on Find Fare Input
//
//   Rev 1.43   Mar 30 2005 16:19:22   rhopkins
//Populate method should always execute its code, regardless of whether this is a postback.  It is the parent page/control that should determine whether to invoke the method.
//Resolution for 1927: DEV Code Review: FAF date selection
//
//   Rev 1.42   Mar 04 2005 11:24:36   tmollart
//Removed condition around population of flexibility so that the control is always populated
//
//   Rev 1.41   Feb 25 2005 10:26:20   tmollart
//Added flexibility screen reader label and a property to access it.
//
//   Rev 1.40   Feb 10 2005 16:49:48   tmollart
//Modified flexibility  property.
//
//   Rev 1.39   Jan 19 2005 15:14:52   tmollart
//Modified flexibility property - now an int.
//
//   Rev 1.38   Jan 18 2005 17:47:38   tmollart
//Del 7 - Added panel and property to control visibility of time selection controls. Added drop down for date flexibility and property to control its visibility.
//
//   Rev 1.37   Oct 08 2004 13:59:50   jbroome
//IR 1595 - added JavaScript functionality to control
//
//   Rev 1.36   Sep 08 2004 14:50:54   COwczarek
//Remove dateLabel and timeLable and reintroduce labelDateType
//Resolution for 1336: Date controls for in Find A Flight inconsistent with other Find A pages
//
//   Rev 1.35   Sep 02 2004 17:03:08   passuied
//Added Selection of AnyTime item in Hour list if Hour is set the AnyTime value ("Any")
//Resolution for 1465: FindA "Amend journey" does not work with "Any time"
//
//   Rev 1.34   Jul 21 2004 17:45:14   asinclair
//Fix for IR 1179
//
//   Rev 1.33   Jul 21 2004 17:40:26   esevern
//removed redundant code in page load
//
//   Rev 1.32   Jul 21 2004 11:41:36   esevern
//removed commented out code
//
//   Rev 1.31   Jul 15 2004 12:10:46   esevern
//removed redundant ControlDateType.  Added new labels and properties for date and time
//
//   Rev 1.30   Mar 11 2004 12:55:34   AWindley
//DEL5.2 Added label text for Screen Reader
//
//   Rev 1.29   Feb 20 2004 11:30:38   esevern
//DEL5.2 changes relating to journey planner input page format
//
//   Rev 1.28   Jan 14 2004 16:33:46   asinclair
//Made a change to alignment of Leave on and Return on labels so they line up with From and To labels in the control above on inout page.
//
//   Rev 1.27   Jan 09 2004 10:57:24   passuied
//added security for MonthYear selection
//
//   Rev 1.26   Dec 29 2003 13:34:02   passuied
//fixed page_load wiring in page deleted from InitializeComponent
//
//   Rev 1.25   Dec 18 2003 12:24:46   JHaydock
//Formatting changes for DEL 5.1
//
//   Rev 1.24   Nov 24 2003 14:02:40   passuied
//fixed bug313
//Resolution for 313: Date error on Amend journey function
//
//   Rev 1.23   Nov 22 2003 09:48:26   passuied
//fixed problem created by Visual Studio
//
//   Rev 1.22   Nov 21 2003 11:40:50   passuied
//style changes
//
//   Rev 1.21   Nov 18 2003 11:48:58   passuied
//missing comments
//
//   Rev 1.20   Nov 18 2003 10:44:06   passuied
//changes to hopefully pass code review
//
//   Rev 1.19   Nov 05 2003 13:23:46   passuied
//fixed various problems with font size
//
//   Rev 1.18   Oct 28 2003 10:39:00   passuied
//changes after fxcop
//
//   Rev 1.17   Oct 20 2003 10:53:00   passuied
//Changes after fxcop
//
//   Rev 1.16   Oct 03 2003 13:34:14   PNorell
//Updated the new exception identifier.
//
//   Rev 1.15   Oct 02 2003 17:49:30   COwczarek
//id parameter passed in TDException constructor set to -1 to enable compilation after introduction of new TDException constructor which takes an enum type for id. This is a temporary fix - the constructor taking an
//id of type long will be removed.
//
//   Rev 1.14   Sep 25 2003 14:00:10   passuied
//last working version (LocationSearch fixed)
//
//   Rev 1.13   Sep 24 2003 16:15:56   PNorell
//Updated for html integration.
//
//   Rev 1.12   Sep 23 2003 19:47:18   PNorell
//Updated for html layout.
//
//   Rev 1.11   Sep 20 2003 14:41:20   passuied
//updated
//
//   Rev 1.10   Sep 19 2003 21:20:48   passuied
//working version up to date
//
//   Rev 1.9   Sep 01 2003 11:59:38   passuied
//corrected update of month  index
//
//   Rev 1.8   Aug 28 2003 14:11:26   adow
//Calendar property added.
//
//   Rev 1.7   Aug 28 2003 09:33:16   passuied
//latest update
//
//   Rev 1.6   Aug 26 2003 10:02:48   passuied
//added ServiceDiscovery Init
//
//   Rev 1.5   Aug 20 2003 17:11:26   passuied
//partial updates after wireframe changes
//
//   Rev 1.4   Aug 15 2003 14:38:12   passuied
//latest version
//
//   Rev 1.3   Aug 08 2003 11:32:42   passuied
//update after wireframe updated
//
//   Rev 1.2   Jul 31 2003 17:37:24   passuied
//building of the capture webpage and update of the used controls
//
//   Rev 1.1   Jul 29 2003 16:00:22   passuied
//update and addition of user control


namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Text.RegularExpressions;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Globalization;
	using System.Collections;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.Web.Support;
	using TransportDirect.Common;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.Common.PropertyService.Properties;
	using TransportDirect.Common.Logging;
	using Logger = System.Diagnostics.Trace;

	/// <summary>
	///		Summary description for DateSelectControl.
	/// </summary>
	public partial  class DateSelectControl : TDUserControl
	{
		protected TransportDirect.UserPortal.Web.Controls.HelpLabelControl transportTypesHelpLabel;
		protected System.Web.UI.WebControls.Label Label1;

		private DataServices populator;
		private bool isForReturnJourney = false;

		/// <summary>
		/// Constructor
		/// </summary>
		protected DateSelectControl()
		{
			populator = (DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
		}
		
		/// <summary>
		/// Sets control text
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			commandCalendar.ImageUrl = Global.tdResourceManager.GetString(
				"DateSelectControl.CalendarURL", TDCultureInfo.CurrentUICulture);
			commandCalendar.AlternateText = Global.tdResourceManager.GetString(
				"DateSelectControl.commandCalendar.AlternateText", TDCultureInfo.CurrentUICulture);
			labelHrs.Text = Global.tdResourceManager.GetString(
				"DateSelectControl.labelHrs", TDCultureInfo.CurrentUICulture);	
			labelMinutes.Text = Global.tdResourceManager.GetString(
				"DateSelectControl.labelMinutes", TDCultureInfo.CurrentUICulture);

		}

		/// <summary>
		/// Pre Render method
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			// Determine if JavaScript is supported
			bool javaScriptSupported = bool.Parse((string) Session[((TDPage)Page).Javascript_Support]);
			string javaScriptDom = (string) Session[((TDPage)Page).Javascript_Dom];
			ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

			if (javaScriptSupported)
			{
				listMonths.EnableClientScript = true; 
				listHours.EnableClientScript = true; 
				listMonths.Action = string.Format("return MonthSelectionChanged('{0}')", this.ClientID);
				listHours.Action = string.Format("return HoursSelectionChanged('{0}')", this.ClientID);
				Page.ClientScript.RegisterStartupScript(typeof(DateSelectControl), listMonths.ScriptName, scriptRepository.GetScript(listMonths.ScriptName, javaScriptDom));
                Page.ClientScript.RegisterStartupScript(typeof(DateSelectControl), listHours.ScriptName, scriptRepository.GetScript(listHours.ScriptName, javaScriptDom));
			}
			else
			{
				listMonths.EnableClientScript = false; 
				listHours.EnableClientScript = false; 
			}
			base.OnPreRender(e);
		}

		/// <summary>
		/// Populates the control
		/// </summary>
		public void Populate()
		{
			if(this.listMonths.Items.Count > 0) return;
			// listMonths
			string[]months = Global.tdResourceManager.GetString("DateSelectControl.listMonths", TDCultureInfo.CurrentUICulture).Split(',');
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
	
			// radioTimeType
			populator.LoadListControl(DataServiceType.LeaveArriveDrop, radioTimeType);	

			// Flexibility drop down
			populator.LoadListControl(DataServiceType.DateFlexibilityDrop, flexibilityDropDown);
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

		#region Read-only properties to access controls
		/// <summary>
		/// read-only property returning the label that specifies whether the control is for outward
		/// or return journeys
		/// </summary>
		public Label ControlDateType
		{
			get
			{
				return labelDateType;
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
		public RadioButtonList ControlTimeType
		{
			get
			{
				return radioTimeType;
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

		/// <summary>
		/// gets the calendar button 
		/// </summary>
		public ImageButton ControlCalendar
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

		#endregion

		#region Properties Get/Set selected values in controls

		/// <summary>
		/// Gets/Sets the current TDDateTime from the controls
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
					return new TDDateTime(year, month, day,0,0,0);
					
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
		/// Gets/Sets if ArriveBefore /Leave after
		/// </summary>
		public bool ArriveBefore
		{
			get
			{
				bool arriveBefore = Convert.ToBoolean(populator.GetValue
					(DataServiceType.LeaveArriveDrop, radioTimeType.SelectedItem.Value)
					);

				
				return arriveBefore;
			}

			set
			{


				string resourceId = populator.GetResourceId(DataServiceType.LeaveArriveDrop, value.ToString(TDCultureInfo.CurrentCulture).ToLower(TDCultureInfo.CurrentCulture));

				for (int i=0; i< radioTimeType.Items.Count; i++)
				{
					ListItem item = radioTimeType.Items[i];
					if (item.Value == resourceId)
					{
						radioTimeType.SelectedIndex = i;
						break;
					}
						
				}


			}	
		}


		/// <summary>
		/// Gets/Sets the Day
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
		public bool IsReturnControl
		{
			get{ return isForReturnJourney;	}
			set{ isForReturnJourney = value; }
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
		/// 
		/// </summary>
		private void SelectAnyTime()
		{
			// backward loop because, Any is more likely to be
			// positionned at the end
			for (int i=listHours.Items.Count-1; i>=0; i--)
			{
				if (listHours.Items[i].Value == Adapters.FindInputAdapter.AnyTimeValue)
				{
					listHours.SelectedIndex = i;
					break;
				}
			
			}
		}
		/// <summary>
		/// Gets/Sets the Hour
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
					
					// code handling case where we need to select AnyTime. This happens when we click AmendJourney
					// and AnyTime was selected from the Output.
					if (value.Equals(Adapters.FindInputAdapter.AnyTimeValue))
					{
						// Select AnyTime and exit property
						SelectAnyTime();
						return;
					}

					hour = Convert.ToInt32(value, CultureInfo.CurrentCulture.NumberFormat);
				}
				catch
				{
					listHours.SelectedIndex = listHours.Items.Count-1;
					return;
				}

				// executed if hour is empty or set. Skipped if value is "AnyTime"
				listHours.SelectedIndex = hour;

			}
		}

		/// <summary>
		///  Gets/Sets the Minute
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
		/// Read/Write. Sets the flexibility drop down visibility.
		/// </summary>
		public bool FlexibilityControlsVisible
		{
			get {return flexibilityTableRow.Visible;}
			set {flexibilityTableRow.Visible = value;}
		}

		/// <summary>
		/// Read/Write. Sets the visibility of the time type radio, list/label for hours
		/// and list/label for minutes. These are all contained in a panel control.
		/// </summary>
		public bool TimeControlsVisible
		{
			get {return timeControlsPanel.Visible;}
			set {timeControlsPanel.Visible = value;}
		}

		/// <summary>
		/// Read/Write. Gets/sets the flexibility drop down control's value.
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
			}
		}

		#endregion


	}
}
