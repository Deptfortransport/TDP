// *********************************************** 
// NAME                 : TrafficDateTimeDropDownControl.ascx.cs 
// AUTHOR               : Andy Lole
// DATE CREATED         : 16/10/2003 
// DESCRIPTION          : Control allowing users to 
//                        select Date & Time using DropDown Boxes
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TrafficDateTimeDropDownControl.ascx.cs-arc  $
//
//   Rev 1.5   Oct 12 2011 12:02:22   mmodi
//Updated logic for Traffic Maps page to set and persist the selected date and times
//Resolution for 5753: Traffic Levels map does not use selected date
//
//   Rev 1.4   Aug 16 2011 15:49:06   MTurner
//Updated to clear any existing values from the date time dropdowns before they are populated.
//Resolution for 5725: Traffic Map Date Drop Down repeats multiple times
//
//   Rev 1.3   Jul 28 2011 16:19:28   DLane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.2   Mar 31 2008 13:23:20   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:18   mturner
//Initial revision.
//
//   Rev 1.19   Feb 23 2006 19:17:12   build
//Automatically merged from branch for stream3129
//
//   Rev 1.18.1.0   Jan 10 2006 15:27:44   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.18   Nov 03 2005 17:04:36   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.17.1.1   Nov 02 2005 16:56:44   RGriffith
//Calendar Alternate Text Added
//
//   Rev 1.17.1.0   Oct 24 2005 18:52:04   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.17   Sep 29 2005 12:50:08   build
//Automatically merged from branch for stream2673
//
//   Rev 1.16.1.1   Sep 14 2005 13:22:22   rgreenwood
//DN079 ES015 Code review
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.16.1.0   Sep 07 2005 13:55:36   rgreenwood
//DN079 ES015 24 Hour Help Button removal
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.17.1.0   Sep 07 2005 13:05:06   rgreenwood
//DN079 UEE ES015 24 Hour Help Button removal
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.17   Sep 07 2005 13:03:52   rgreenwood
//DN079 UEE ES015 24 Hour Help Button removal
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.16   Jul 27 2004 00:05:10   CHosegood
//Now sets time to 0:00 when system time is greater than 23:53.
//Resolution for 1227: Traffic Map not the same time as drop down DEL 6
//
//   Rev 1.15   Jul 19 2004 09:45:58   asinclair
//Fix for IR977
//
//   Rev 1.14   Jun 18 2004 12:59:58   asinclair
//Fix for IR 1034
//
//   Rev 1.13   Apr 20 2004 14:56:16   COwczarek
//Fixes to ensure drop down lists are refreshed correctly. Removal
//of "-" entries from drop down lists.
//Resolution for 789: Using calender to change date on traffic maps does not work
//
//   Rev 1.12   Apr 19 2004 17:43:16   COwczarek
//Correctly set the selected index of the month drop down list
//Resolution for 789: Using calender to change date on traffic maps does not work
//
//   Rev 1.11   Apr 15 2004 17:07:36   ESevern
//added leading zero to day in SetDateTimeValue - see IR776
//Resolution for 776: Traffic Maps - Date drop downs contain incorrectly formatted values
//
//   Rev 1.10   Apr 15 2004 09:18:22   AWindley
//DEL5.2 QA: Resolution for 776: Date drop downs contain incorrectly formatted values
//
//   Rev 1.9   Mar 31 2004 18:09:16   CHosegood
// Del 5.2 map QA changes.
//Resolution for 674: DEL 5.2 Map QA changes
//
//   Rev 1.8   Mar 15 2004 16:49:22   pnorell
//Updated for help
//
//   Rev 1.7   Mar 12 2004 10:32:24   CHosegood
//Get label text from resource manager
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.6   Mar 11 2004 17:09:18   asinclair
//Added ToolTip for 24 clock button
//
//   Rev 1.5   Mar 11 2004 11:18:34   AWindley
//DEL5.2 Added label text for Screen Reader
//
//   Rev 1.3   Dec 11 2003 13:37:30   asinclair
//Changed code to insert a 0 infront of hours on time dro down to fix IR 512
//
//   Rev 1.2   Nov 21 2003 10:02:32   alole
//Updated the return type for GetDateTime value method.
//
//   Rev 1.1   Nov 18 2003 16:14:42   alole
//Updated to include alt text
//
//   Rev 1.0   Oct 21 2003 09:13:50   ALole
//Initial Revision

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Globalization;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Common;
	using TransportDirect.Common.Logging;
	using TransportDirect.Common.PropertyService.Properties;
	using TransportDirect.Web.Support;
	using Logger = System.Diagnostics.Trace;

	/// <summary>
	/// Summary description for TrafficDateTimeDropDownControl.
	/// </summary>
	public partial  class TrafficDateTimeDropDownControl : TDUserControl
	{

		public event EventHandler DateSelectionEvent;

        #region Instance members

		private int monthsTraffic = 6;
		private TDDateTime setDateTime;

		public static readonly TDDateTime NullDate = new DateTime(0);

        #endregion

        #region Public properties
		/// <summary>
		/// 
		/// </summary>
		public ImageButton ControlCalendar
		{
			get
			{
				return commandCalendar;
			}
		}

		/// <summary>
		/// Get the ShowOnMap image button
		/// </summary>
		public TDButton ControlShowOnMap
		{
			get
			{
				return commandShowOnMap;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int MonthsTraffic
		{
			get
			{
				return monthsTraffic;
			}
			set
			{
				monthsTraffic = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool IsValidDateTime
		{
			get
			{
				int month = Convert.ToInt32(DropDownListMonthYear.SelectedItem.Value.Substring(0,2),
					TDCultureInfo.CurrentCulture.NumberFormat);
				int year = Convert.ToInt32 (DropDownListMonthYear.SelectedItem.Value.Substring(3,4),
					TDCultureInfo.CurrentCulture.NumberFormat);
				int day = Convert.ToInt32(DropDownListDay.SelectedItem.Value);

				if (month == 2)
					if (IsLeapYear(year))
						return ( day <= 29 );
					else
						return ( day <= 28 );
				else if (month == 9 || month == 4 || month == 6 || month == 11)
					return ( day <= 30 );
				else
					return true;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public TDDateTime DateTimeValue
		{
			get
			{
				if (IsValidDateTime)
				{
					int month = Convert.ToInt32(DropDownListMonthYear.SelectedItem.Value.Substring(0,2),
						TDCultureInfo.CurrentCulture.NumberFormat);

					int year = Convert.ToInt32 (DropDownListMonthYear.SelectedItem.Value.Substring(3,4),
						TDCultureInfo.CurrentCulture.NumberFormat);

					int day = Convert.ToInt32(DropDownListDay.SelectedItem.Value);
					int hour = Convert.ToInt32(DropDownListHours.SelectedItem.Value);
					int minute = Convert.ToInt32(DropDownListMinutes.SelectedItem.Value);
					return new TDDateTime(year, month, day, hour, minute, 0);
				}
				else
				{
					return NullDate;
				}
			}
			set
			{
				setDateTime = value;
			}
		}

        #endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="Year"></param>
		/// <returns></returns>
		private bool IsLeapYear(int Year)
		{
			double dblYear = (double)Year;

			if ((double)(Year / 4) == dblYear / 4)
				if ((double)(Year / 100) == dblYear / 100)
					if ((double)(Year / 400) == dblYear / 400)
						return true;
					else
						return false;
				else
					return true;
			else
				return false;
		}


		/// <summary>
		/// Sets the selected values in the day/month/hour/min drop down lists to show the
		/// date and time set with the DateTimeValue property
		/// </summary>
		public void Populate()
		{
			if ((setDateTime != null) && (DropDownListDay.Items.Count > 0))
			{
                int minute = setDateTime.Minute;
                int day = setDateTime.Day;
                int hour = setDateTime.Hour;

                //minute = (( int ) Math.Ceiling( (double)setDateTime.Minute / 15 ))*15;
				minute = Convert.ToInt32((double)minute / 15) * 15;

                //If we've gone to the next hour (i.e. it's 10:57) make sure that the
                //day, month and year are modified as well, if needed.
                if (minute == 60) 
                {
                    setDateTime = setDateTime.AddMinutes( 60 - setDateTime.Minute );

                    minute = setDateTime.Minute;
                    hour = setDateTime.Hour;
                    day = setDateTime.Day;
                }

                //Set the selected day in the drop down
                if(day < 10)
                    DropDownListDay.SelectedIndex = DropDownListDay.Items.IndexOf(DropDownListDay.Items.FindByValue("0" + day ));
                else
                    DropDownListDay.SelectedIndex = DropDownListDay.Items.IndexOf(DropDownListDay.Items.FindByValue(setDateTime.Day.ToString()));

                //Set the selected month & year in the drop down
                DropDownListMonthYear.SelectedIndex = DropDownListMonthYear.Items.IndexOf(DropDownListMonthYear.Items.FindByValue(setDateTime.ToString("MM/yyyy")));

                //Set the selected minute in the drop down
				DropDownListMinutes.SelectedIndex = DropDownListMinutes.Items.IndexOf(DropDownListMinutes.Items.FindByValue( minute.ToString() ) );

                //Set the selected hour in the drop down
                if (hour < 10)
                    DropDownListHours.SelectedIndex = DropDownListHours.Items.IndexOf(DropDownListHours.Items.FindByValue("0"+ hour.ToString() ) );
                else
                    DropDownListHours.SelectedIndex = DropDownListHours.Items.IndexOf(DropDownListHours.Items.FindByValue( hour.ToString() ) );
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="visibleState"></param>
		public void ButtonsVisible( bool visibleState )
		{
			commandCalendar.Visible = visibleState;
			//commandShowOnMap.Visible = visibleState;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			ButtonsVisible( true );

			commandShowOnMap.Text = GetResource("TrafficDateTimeDropDownControl.commandShowOnMap.Text");

			this.commandCalendar.ImageUrl = Global.tdResourceManager.GetString(
				"TrafficDateTimeDropDownControl.commandCalendar.ImageUrl", TDCultureInfo.CurrentUICulture);
			this.commandCalendar.AlternateText = Global.tdResourceManager.GetString(
				"TrafficDateTimeDropDownControl.commandCalendar.AlternateText", TDCultureInfo.CurrentUICulture);

			this.labelDate.Text = Global.tdResourceManager.GetString(
				"TrafficDateTimeDropDownControl.labelDate.Text", TDCultureInfo.CurrentUICulture);

			this.labelTime.Text = Global.tdResourceManager.GetString(
				"TrafficDateTimeDropDownControl.labelTime.Text", TDCultureInfo.CurrentUICulture);

			this.label24HourClock.Text = Global.tdResourceManager.GetString(
				"TrafficDateTimeDropDownControl.label24HourClock.Text", TDCultureInfo.CurrentUICulture);

            labelSRDate.Text = GetResource("outwardSelect.labelSRDate");
            labelSRMonthYear.Text = GetResource("outwardSelect.labelSRMonthYear");
            labelSRHoursPre.Text = GetResource("outwardSelect.labelSRHours");
            labelSRMinutesPre.Text = GetResource("outwardSelect.labelSRMinutes");

            if (!Page.IsPostBack)
            {
                // Populate dropdowns on first postback only
                populateDays();
                populateMonths();
                populateHours();
                populateMinutes();

                // Ensure the initial set date is selected
                Populate();
            }
		}

		/// <summary>
		/// Display the specified date in the dropdowns
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
            ShowOnMapRow.Visible = ControlShowOnMap.Visible;
		}

		#region Web Form Designer generated code]
		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
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
			this.commandShowOnMap.Click += new EventHandler(this.commandShowOnMap_Click);

		}
		#endregion

        #region Populate methods
		/// <summary>
		/// Populates the month/year dropdown
		/// </summary>
		private void populateMonths()
		{
            // Clear out any existing entries. Fix for USD UK:11224312
            DropDownListMonthYear.Items.Clear();
            
			// Populate the controls
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

				DropDownListMonthYear.Items.Insert( 0, liMonth );
			}
		}


		/// <summary>
		/// Populates the day dropdown
		/// </summary>
		private void populateDays() 
		{
			ListItem li;
			string liValue;

            // Clear out any existing entries. Fix for USD UK:11224312
            DropDownListDay.Items.Clear();

			//Add values to Day drop down
			for (int i = 1; i < 32; i++)
			{
				if ( i < 10 )
				{
					liValue = "0" + i.ToString();
					li = new ListItem(liValue, liValue);
					
				}
				else
				{
					liValue = i.ToString();
					li = new ListItem(liValue, liValue);
				}
				DropDownListDay.Items.Add(li);
			}
		}


		/// <summary>
		/// Populates the Hours dropdown
		/// </summary>
		private void populateHours() 
		{
			ListItem li;
			string liValue;

            // Clear out any existing entries. Fix for USD UK:11224312
            DropDownListHours.Items.Clear();

			//Add values to Hours drop down
			for (int i = 0; i < 24; i++)
			{
				if ( i < 10 )
				{
					liValue = "0" + i.ToString();
					li = new ListItem(liValue, liValue);
				}
				else
				{
					liValue = i.ToString();
					li = new ListItem(liValue, liValue);
				}
				DropDownListHours.Items.Add(li);
			}
		}


		/// <summary>
		/// Populates the Minutes dropdown
		/// </summary>
		private void populateMinutes() 
		{
			ListItem li;
			string liValue;

            // Clear out any existing entries. Fix for USD UK:11224312
            DropDownListMinutes.Items.Clear();

			//Add values to Minutes drop down
			for (int i = 0; i < 60; i+=15)
			{
				if ( i == 0 )
				{
					liValue = "0" + i.ToString();
					li = new ListItem(liValue, "0");
				}
				else
				{
					liValue = i.ToString();
					li = new ListItem(liValue, liValue);
				}
				DropDownListMinutes.Items.Add(li);
			}
		}

        #endregion

		private void commandShowOnMap_Click(object sender, EventArgs e)
		{
			DateSelectionEvent( this, EventArgs.Empty );
		}
	}
}
