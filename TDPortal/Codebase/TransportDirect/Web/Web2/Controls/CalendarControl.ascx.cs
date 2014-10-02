// *********************************************** 
// NAME                 :  CalendarControl.ascx
// AUTHOR               : Andy Dow
// DATE CREATED         : 29/07/2003 
// DESCRIPTION  : Control to implement Transport Direct Calendar control 
//                that does not rely on any client side code.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/CalendarControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:19:28   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:26   mturner
//Initial revision.
//
//   Rev 1.29   Feb 23 2006 19:16:22   build
//Automatically merged from branch for stream3129
//
//   Rev 1.28.1.0   Jan 10 2006 15:23:40   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.28   Sep 08 2004 15:19:46   jgeorge
//Changed call to Page.DataBind() to call to this.DataBind() to stop adverse affects on client pages.
//Resolution for 1502: Find a flight selecting a date from the calendar resets to/from locations
//
//   Rev 1.27   Jul 29 2004 11:15:28   passuied
//Added a static method in TDPage to close all calendar and help windows. Replaced local use.
//
//   Rev 1.26   Jul 28 2004 17:27:44   passuied
//Implementation of a DateSelected event in calendar to indicate to subscribers a date has been selected in calendar.
//
//   Rev 1.25   Jul 13 2004 09:42:06   jbroome
//IR 1184 - WAI testing. Associate table data with column headings.
//
//   Rev 1.25   Jul 02 2004 16:06:36   jbroome
//IR1071 - W.A.I. Data table HTML formatting for screen reader accessibility
//
//   Rev 1.24   Mar 11 2004 11:53:24   asinclair
//Added ToolTip text for Close icon Del 5.2
//
//   Rev 1.23   Jan 13 2004 11:46:04   COwczarek
//Disable legend buttons - they should not be clickable
//Resolution for 591: Calendar Bank Holidays Incorrect
//
//   Rev 1.22   Oct 15 2003 11:50:52   passuied
//removed the transitionevent, as it is useless and causes pbs when calendar is used in different pages than input.
//
//   Rev 1.21   Oct 01 2003 12:03:40   abork
//Bug Fixed
//
//   Rev 1.20   Oct 01 2003 10:59:58   abork
//Updated Bank Holiday Display
//
//   Rev 1.19   Sep 30 2003 15:22:56   PNorell
//Added support for ensuring only one "window" open on a web page at the same time.
//Fixed numerous click bug in the Help control.
//Fixed numerous language issues with the help control.
//Updated the journey planner input pages to contain the updated code for ensuring one window.
//Updated the wait page and took out the debug logging.
//
//   Rev 1.18   Sep 26 2003 08:48:56   PNorell
//Fixed calendar - It now uses the data services and the "greying out" and disabling works as it is supposed to.
//
//   Rev 1.17   Sep 25 2003 18:06:24   PNorell
//Ensured everything is linked up together.
//Fixed various small bugs.
//
//   Rev 1.16   Sep 19 2003 21:20:50   passuied
//working version up to date
//
//   Rev 1.15   Sep 19 2003 12:26:20   jcotton
//Implement Bank Holidays into Calendar control
//
//   Rev 1.14   Sep 11 2003 10:40:30   adow
//Code review fixes
//
//   Rev 1.13   Sep 04 2003 10:21:26   adow
//Fixed naming conventions
//
//   Rev 1.12   Sep 02 2003 13:28:06   adow
//chaning the IsDateSelected propery
//
//   Rev 1.11   Sep 02 2003 11:47:10   adow
//bug fixes and new disable button functionality

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Collections;
	using System.Data;
	using System.Data.SqlClient;
	using System.Drawing;
	using System.Diagnostics;
	using Logger = System.Diagnostics.Trace;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Common;
	using System.Globalization;
	using TransportDirect.Common.DatabaseInfrastructure;
	using TransportDirect.Common.Logging;
	using TransportDirect.Common.PropertyService.Properties;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.Web.Support;

	/// <summary>
	///		Implements the Transport Direct Calendar User Control
	/// </summary>
	public partial  class CalendarControl : TDUserControl, ISingleWindow
	{
		
		#region UI Control declaration
		
		#endregion

		protected TDDateTime sDateTime;
		protected TDDateTime td;
		//local variable for IsDateSelected property
		protected bool isDS;
		

		/// <summary>
		/// Used by CalendarControl for database connectivity and interaction.
		/// Opened on each call to the database and immeadiately closed on retrun.
		/// </summary>
		private SqlHelper CalendarControlSql = new SqlHelper();

		/// <summary>
		/// Event indicating a date has been selected in the calendar.
		/// </summary>
		public event EventHandler DateSelected;

		/// <summary>
		///		Stores if a date has been selected from the day buttons. Is read/write.
		/// </summary>
		public bool IsDateSelected
		{
			get 
			{
				return isDS;
			}

			set
			{
				isDS = value;
			}
		}
		
		/// <summary>
		///		Stores use selected date. Is read/write.
		/// </summary>
		public TDDateTime TravelDate
		{
			get 
			{
				if (isDS == false)
				{
					return null;
				}
				else 
				{
					return td;
				}
			}

			set
			{
				td = value;
			}
		
		}

		

		protected void Page_Load(object sender, System.EventArgs e)
		{
			#region Event handler setup - lengthy
			//Set up calendar day event handlers
			this.fwdButton.Click += new System.EventHandler(this.FwdButtonClick);
			this.backButton.Click += new System.EventHandler(this.BackButtonClick);
			this.day1.Click += new System.EventHandler(this.DayClick);
			this.day2.Click += new System.EventHandler(this.DayClick);
			this.day3.Click += new System.EventHandler(this.DayClick);
			this.day4.Click += new System.EventHandler(this.DayClick);
			this.day5.Click += new System.EventHandler(this.DayClick);
			this.day6.Click += new System.EventHandler(this.DayClick);
			this.day7.Click += new System.EventHandler(this.DayClick);
			this.day8.Click += new System.EventHandler(this.DayClick);
			this.day9.Click += new System.EventHandler(this.DayClick);
			this.day10.Click += new System.EventHandler(this.DayClick);
			this.day11.Click += new System.EventHandler(this.DayClick);
			this.day12.Click += new System.EventHandler(this.DayClick);
			this.day13.Click += new System.EventHandler(this.DayClick);
			this.day14.Click += new System.EventHandler(this.DayClick);
			this.day15.Click += new System.EventHandler(this.DayClick);
			this.day16.Click += new System.EventHandler(this.DayClick);
			this.day17.Click += new System.EventHandler(this.DayClick);
			this.day18.Click += new System.EventHandler(this.DayClick);
			this.day19.Click += new System.EventHandler(this.DayClick);
			this.day20.Click += new System.EventHandler(this.DayClick);
			this.day21.Click += new System.EventHandler(this.DayClick);
			this.day22.Click += new System.EventHandler(this.DayClick);
			this.day23.Click += new System.EventHandler(this.DayClick);
			this.day24.Click += new System.EventHandler(this.DayClick);
			this.day25.Click += new System.EventHandler(this.DayClick);
			this.day26.Click += new System.EventHandler(this.DayClick);
			this.day27.Click += new System.EventHandler(this.DayClick);
			this.day28.Click += new System.EventHandler(this.DayClick);
			this.day29.Click += new System.EventHandler(this.DayClick);
			this.day30.Click += new System.EventHandler(this.DayClick);
			this.day31.Click += new System.EventHandler(this.DayClick);
			this.day32.Click += new System.EventHandler(this.DayClick);
			this.day33.Click += new System.EventHandler(this.DayClick);
			this.day34.Click += new System.EventHandler(this.DayClick);
			this.day35.Click += new System.EventHandler(this.DayClick);
			this.day36.Click += new System.EventHandler(this.DayClick);
			this.day37.Click += new System.EventHandler(this.DayClick);
 
			this.cancel.Click += new System.EventHandler(this.CancelClick);

			#endregion
			
			#region Create days of week from resource manager strings
			this.calendarMonday.Text =  Global.tdResourceManager.GetString("CalendarMonday");
			this.calendarTuesday.Text = Global.tdResourceManager.GetString("CalendarTuesday");
			this.calendarWednesday.Text = Global.tdResourceManager.GetString("CalendarWednesday");
			this.calendarThursday.Text = Global.tdResourceManager.GetString("CalendarThursday");
			this.calendarFriday.Text = Global.tdResourceManager.GetString("CalendarFriday");
			this.calendarSaturday.Text =  Global.tdResourceManager.GetString("CalendarSaturday");
			this.calendarSunday.Text = Global.tdResourceManager.GetString("CalendarSunday");

			this.lblBankHoliday0.Text = Global.tdResourceManager.GetString("Calendar.BankHolidays.0");
			this.lblBankHoliday1.Text = Global.tdResourceManager.GetString("Calendar.BankHolidays.1");
			this.lblBankHoliday2.Text = Global.tdResourceManager.GetString("Calendar.BankHolidays.2");
			this.lblBankHoliday3.Text = Global.tdResourceManager.GetString("Calendar.BankHolidays.3");

			this.cancel.ToolTip = Global.tdResourceManager.GetString("CalendarControl.cancel.ToolTip", TDCultureInfo.CurrentUICulture);
		
			this.cancelText.Text = Global.tdResourceManager.GetString("CalendarControl.cancel.ToolTip", TDCultureInfo.CurrentUICulture);
			this.fwdText.Text = Global.tdResourceManager.GetString("CalendarControl.NextMonth", TDCultureInfo.CurrentUICulture);
			this.backText.Text = Global.tdResourceManager.GetString("CalendarControl.PreviousMonth", TDCultureInfo.CurrentUICulture);

			#endregion

			//set default action for buttons
			backButton.Enabled = true;
			fwdButton.Enabled = true;
			
			//prevent legend buttons from being clicked
			btnBankHoliday1.Enabled = false;
            btnBankHoliday2.Enabled = false;
            btnBankHoliday3.Enabled = false;
			
		}

		/// <summary>
		/// Databinds page and calls base OnPreRender
		/// </summary>
		protected override void OnPreRender(EventArgs e)
		{
			this.DataBind();
			base.OnPreRender(e);
		}

		/// <summary>
		///		Creates the caledar control from a TDDateTime
		/// </summary>
		/// <param name=”selectedDateTime”> The Date selected by the user </param>
		/// <param name=”title”> the title the control should render depending on 
		/// which button from the DateSelectControl opened it</param>
		public void SetCalendar(TDDateTime selectedDateTime, string title)
		{
			//set the calendar title
			calendarTitle.Text = title;

			
			//Check if fwd, back or day buttons need to be disabled
			if (selectedDateTime.Month == TDDateTime.Now.Month)
			{
				backButton.Enabled = false;
			}
			
			string numMonth = Properties.Current[ "controls.numberofmonths" ];
			int months = System.Convert.ToInt32(numMonth);
			int adjustedSelectedMonth = ((selectedDateTime.Year - TDDateTime.Now.Year) * 12) + selectedDateTime.Month;
			
			if (adjustedSelectedMonth - TDDateTime.Now.Month >= (months - 1))
			{
				fwdButton.Enabled = false;
			}

		
			//Set up resource Manager to get back month text
			int selectedMonth = selectedDateTime.Month;
			string monthName = "Month" + selectedMonth.ToString();
			string selectedMonthText = Global.tdResourceManager.GetString(monthName);
			

			//set the labels text
			monthLabel.Text = selectedMonthText;
			yearLabel.Text = selectedDateTime.Year.ToString();

			

			//get the number of days in the month and month start day
			int monthDays = TDDateTime.DaysInMonth(selectedDateTime.Year, selectedMonth);
			
			DateTimeFormatInfo dtFormatInfo =  new DateTimeFormatInfo();
			dtFormatInfo.FirstDayOfWeek = DayOfWeek.Monday;

			//Find the day of the week the month starts on
			DayOfWeek first;
			TDDateTime aDT = new TDDateTime(selectedDateTime.Year, selectedDateTime.Month, 1);
			first = aDT.DayOfWeek;
			
			//Look up the cell location
			int d = GetCellIndex(first);
		
			
			//Delete the dayButton controls before the start day. 
			//We know that only the first week can be effected so 
			//we can just loop through this to optimise the process
			
			
			for (int i = 0; i < d; i++)
			{
				calendarTable.Rows[3].Cells[i].Controls.Clear();	
			}

			//Delete the dayButton controls after the last day of the month.
			
			// We add the number of days in the month to the index day of the first cell.
			// The minus 1 are to adjust to 0 indexing. This gives us the last cell ID for the 
			//days in a given month
			int lastCell = (monthDays + (d) - 1) ;
			int endDays = 0;
			

			//We know that if there are more than 34 cells used it will go into the 7th row
			if (lastCell <= 34)
			{
				calendarTable.Rows[8].Controls.Clear();	
				
				//For the 6th row 27 is the cell number of the end of the previous row. 
				//We subtract this from the last cell to get the number of cells to be 
				//deleted from the last row.
				endDays = lastCell - 27;

				for (int j = endDays; j < 7; j++)
				{
					calendarTable.Rows[7].Cells[j].Controls.Clear();
				}
			
			}

			else
			{
				//For the 7th row 34 is the cell number of the end of the previous row. 
				//We subtract this from the last cell to get the number of cells to be 
				//deleted from the last row
				endDays = lastCell - 34;
				for (int j = endDays; j < 2; j++)
				{
					calendarTable.Rows[8].Cells[j].Controls.Clear();						
				}
			}
				

			//Set the text of the buttons to the correct date and format them. 

			// Hide all bank holiday legends
			trBankHoliday0.Visible = false;
			trBankHoliday1.Visible = false;
			trBankHoliday2.Visible = false;
			trBankHoliday3.Visible = false;

			TDDateTime buttonDate = aDT;
			TimeSpan oneDay = new TimeSpan( 1, 0, 0, 0 );
			
			int calDate = 1;
			
			//r represents rows
			for(int r = 3; r < 9; r++)
			{
				//c represents cells
				for (int c = 0; c < 7; c++)
				{
					// the below formular works out the corrrect date and hence control name 
					//dependant on the row and column.
					string controlName = "day" + System.Convert.ToString(c + (((r-3)*7)+1));
					
					//Make sure this isnt a cell or a row that has had its control removed.

					if (calendarTable.Rows[r].HasControls() && calendarTable.Rows[r].Cells[c].HasControls())
					{
						Button b = (Button) calendarTable.Rows[r].Cells[c].Controls[0].FindControl(controlName);
						if( b == null )
						{
							// This row lack button -> skip to next one
							continue;
						}
						int bankHolidayCountry = 0;

						// Data services
						DataServices ds = (DataServices) TDServiceDiscovery.Current[ ServiceDiscoveryKey.DataServices ];
						bool england = ds.IsHoliday( DataServiceType.BankHolidays, buttonDate, DataServiceCountries.EnglandWales );
						bool scottland = ds.IsHoliday( DataServiceType.BankHolidays, buttonDate, DataServiceCountries.Scotland );
						if( england && scottland )
						{ 
							bankHolidayCountry = 1;
						}
						else if( scottland )
						{
							bankHolidayCountry = 2;
						}
						else if( england )
						{
							bankHolidayCountry = 3;
						}

						string prefix = string.Empty;
						bool enabled = (calDate >= TDDateTime.Now.Day) || (selectedDateTime.Month != TDDateTime.Now.Month);
						if( !enabled )
						{
							prefix = "Grey";
						}
						if( selectedDateTime.Month == buttonDate.Month && calDate.ToString() == selectedDateTime.Day.ToString())
						{
							prefix = "Selected";
						}

						switch( bankHolidayCountry )
						{
							case 1:
								b.CssClass = prefix+"CalButton1";
								trBankHoliday1.Visible = true;
								trBankHoliday0.Visible = true;
								break;
							case 2:
								b.CssClass = prefix+"CalButton2";
								trBankHoliday2.Visible = true;
								trBankHoliday0.Visible = true;
								break;
							case 3:
								b.CssClass = prefix+"CalButton3";
								trBankHoliday3.Visible = true;
								trBankHoliday0.Visible = true;
								break;
							default:
								b.CssClass = prefix+"CalButton";
								break;
						}

						b.Text = calDate.ToString();
						b.Enabled = enabled;

						buttonDate = buttonDate.Add( oneDay );
						calDate++;
					}
	
				}	
	
			}


			//Set the hidden date label to the current selected DateTime for retrieval on postback

			date.Text = selectedDateTime.ToString();

		}

		/// <summary>
		///		Lookup for the cell index of the start date of a month
		/// </summary>
		/// <param name=”day”> The start day of a given month </param>
		private int GetCellIndex(DayOfWeek day)
		{
			
			switch (day)
			{
				case DayOfWeek.Monday:
				
					return 0;
					

				case DayOfWeek.Tuesday:

					return 1;

				case DayOfWeek.Wednesday:

					return 2;

				case DayOfWeek.Thursday:
					
					return 3;

				case DayOfWeek.Friday:

					return 4;


				case DayOfWeek.Saturday:

					return 5;


				case DayOfWeek.Sunday:

					return 6;


				default:

					//error has occured.
					return -1;
					
					

			}

		
		
		}


		/// <summary>
		///		Event to handle the fwd button click
		/// </summary>
		/// <param name=”sender”> the control sender</param>
		/// <param name=”e”> the event arguments </param>
		private void FwdButtonClick(object sender, System.EventArgs e)
		{		
			//Get the current date and move forward a month
			sDateTime = (TDDateTime) System.Convert.ToDateTime(date.Text); 
			TDDateTime fwdDate = sDateTime.AddMonths(1);
			// keep traveldate to the old date as we do not want to update the page on a clickon the fwd or back buttons
			TravelDate = sDateTime;	
			SetCalendar(fwdDate, calendarTitle.Text);
		}

		/// <summary>
		///		Event to handle the back button click
		/// </summary>
		/// <param name=”sender”> the control sender</param>
		/// <param name=”e”> the event arguments </param>
		private void BackButtonClick(object sender, System.EventArgs e)
		{
			
			//Get the current date and move back a month
			sDateTime = (TDDateTime) System.Convert.ToDateTime(date.Text);
			TDDateTime backDate = sDateTime.AddMonths(-1);
			// keep traveldate to the old date as we do not want to update the page on a clickon the fwd or back buttons
			TravelDate = sDateTime;
			SetCalendar(backDate, calendarTitle.Text);
			
		
		
		}
		/// <summary>
		///		Event to handle the day button
		/// </summary>
		/// <param name=”sender”> the control sender</param>
		/// <param name=”e”> the event arguments </param>
		protected void DayClick(object sender, System.EventArgs e)
		{
			//Get the button that was clicked
			System.Web.UI.WebControls.Button buttonClicked = (System.Web.UI.WebControls.Button) sender;
			int travelDay;
			travelDay = 0;
			travelDay = System.Convert.ToInt32(buttonClicked.Text);
			//update the sDateTime variable
			sDateTime = (TDDateTime) System.Convert.ToDateTime(date.Text);
			sDateTime = new TDDateTime(sDateTime.Year, sDateTime.Month, travelDay);
			//Set our TravelDate proerty to the new selected date
			TravelDate = sDateTime;
			SetCalendar(sDateTime, calendarTitle.Text);
			Close();
			//Set the IsDateSelected property
			IsDateSelected = true;


			// Raise event to indicate a date has been selected
			if (DateSelected != null)
				DateSelected(sender, new EventArgs());


			
		}

		protected void CancelClick(object sender, System.EventArgs e)
		{
			Close();
		}

		#region ISingleWindow interface code
		public void Close()
		{
			this.Visible = false;
		}

		public void Open()
		{
			// Ensure everything else is closed before opening
			TDPage.CloseAllSingleWindows(this.Page);
			this.Visible = true;
		}

		public bool IsOpen
		{
			get
			{
				return this.Visible;
			}
		}

		/// <summary>
		/// Returns the full day of week text from lagstrings according to culture.
		/// </summary>
		/// <param name="index"></param>
		public string getDayofWeek(int index)
		{
			switch (index)
			{
				case 1:
					return Global.tdResourceManager.GetString("CalendarMondayFull");
				case 2:
					return Global.tdResourceManager.GetString("CalendarTuesdayFull");
				case 3:
					return Global.tdResourceManager.GetString("CalendarWednesdayFull");
				case 4:
					return Global.tdResourceManager.GetString("CalendarThursdayFull");
				case 5:
					return Global.tdResourceManager.GetString("CalendarFridayFull");
				case 6:
					return Global.tdResourceManager.GetString("CalendarSaturdayFull");
				case 7:
					return Global.tdResourceManager.GetString("CalendarSundayFull");
				default:
					return string.Empty;
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
            this.day1.Click += new EventHandler(this.DayClick);
            this.day2.Click += new EventHandler(this.DayClick);
            this.day3.Click += new EventHandler(this.DayClick);
            this.day4.Click += new EventHandler(this.DayClick);
            this.day5.Click += new EventHandler(this.DayClick);
            this.day6.Click += new EventHandler(this.DayClick);
            this.day7.Click += new EventHandler(this.DayClick);
            this.day8.Click += new EventHandler(this.DayClick);
            this.day9.Click += new EventHandler(this.DayClick);
            this.day10.Click += new EventHandler(this.DayClick);
            this.day11.Click += new EventHandler(this.DayClick);
            this.day12.Click += new EventHandler(this.DayClick);
            this.day13.Click += new EventHandler(this.DayClick);
            this.day14.Click += new EventHandler(this.DayClick);
            this.day15.Click += new EventHandler(this.DayClick);
            this.day16.Click += new EventHandler(this.DayClick);
            this.day17.Click += new EventHandler(this.DayClick);
            this.day18.Click += new EventHandler(this.DayClick);
            this.day19.Click += new EventHandler(this.DayClick);
            this.day20.Click += new EventHandler(this.DayClick);
            this.day21.Click += new EventHandler(this.DayClick);
            this.day22.Click += new EventHandler(this.DayClick);
            this.day23.Click += new EventHandler(this.DayClick);
            this.day24.Click += new EventHandler(this.DayClick);
            this.day25.Click += new EventHandler(this.DayClick);
            this.day26.Click += new EventHandler(this.DayClick);
            this.day27.Click += new EventHandler(this.DayClick);
            this.day28.Click += new EventHandler(this.DayClick);
            this.day29.Click += new EventHandler(this.DayClick);
            this.day30.Click += new EventHandler(this.DayClick);
            this.day31.Click += new EventHandler(this.DayClick);
            this.day32.Click += new EventHandler(this.DayClick);
            this.day33.Click += new EventHandler(this.DayClick);
            this.day34.Click += new EventHandler(this.DayClick);
            this.day35.Click += new EventHandler(this.DayClick);
            this.day36.Click += new EventHandler(this.DayClick);
            this.day37.Click += new EventHandler(this.DayClick);
		}

		#endregion
	}
}
