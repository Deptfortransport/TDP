// *********************************************** 
// NAME                 : DateTimeDropDownControl.ascx.cs 
// AUTHOR               : James Haydock & Halim Ahad
// DATE CREATED         : 25/09/2003 
// DESCRIPTION          : Control allowing users to 
//                        select Date & Time using DropDown Boxes
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/DateTimeDropDownControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:19:52   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:04   mturner
//Initial revision.
//
//   Rev 1.7   Mar 10 2004 16:22:56   AWindley
//DEL5.2 Additional label text for Screen Reader
//
//   Rev 1.6   Mar 09 2004 12:44:16   AWindley
//DEL5.2 Added label text for Screen Reader
//
//   Rev 1.5   Jan 05 2004 15:47:56   asinclair
//Updated for IR 566, 5a.
//
//   Rev 1.4   Dec 22 2003 15:30:10   asinclair
//added leading zero to hours drop down
//
//   Rev 1.3   Oct 16 2003 15:19:50   ALole
//Removed TrafficMap.aspx specific code (Now in new Control, TrafficDateTimeDropDownControl)
//
//   Rev 1.2   Oct 15 2003 11:17:38   ALole
//Changed internal DateTimes to TDDateTimes
//
//   Rev 1.1   Oct 14 2003 18:06:12   ALole
//Added Calendar and Ok buttons.
//Changed Text for labels.
//Added method to make the buttons visible/invisble.
//
//   Rev 1.0   Sep 30 2003 10:40:28   hahad
//Initial Revision
namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Web.Support;
	using TransportDirect.Common;

	/// <summary>
	///		Summary description for DateTimeDropDownControl.
	/// </summary>
	public partial  class DateTimeDropDownControl : TDUserControl
	{

		private int _monthsDefault = -6;
		private TDDateTime setDateTime = new TDDateTime( new DateTime( 0 ) );
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			ListItem li;
			string liText;
			string liValue;
			TDDateTime dateTime;

			//Add values to Day drop down
			li = new ListItem("-", "-");
			DropDownListDay.Items.Add(li);

			for (int i = 1; i < 32; i++)
			{
				liValue = i.ToString("D2");
				li = new ListItem(liValue, liValue);
				DropDownListDay.Items.Add(li);
			}

			//Add values to Month Year drop down
			li = new ListItem("-", "-");
			DropDownListMonthYear.Items.Add(li);

			for ( int i = 0; i > _monthsDefault; i-- )
			{
				dateTime = TDDateTime.Now.AddMonths( i );
				liText = dateTime.ToString( "MMM yyyy" );
				liValue = dateTime.ToString( "yyyy-M" );
				li = new ListItem( liText, liValue );
				DropDownListMonthYear.Items.Add( li );
			}

			//need to added code to allow for positive values

			//Add values to Hours drop down
			li = new ListItem("-", "-");
			DropDownListHours.Items.Add(li);

			for (int i = 0; i < 24; i++)
			{
				
				liValue = i.ToString("D2");
				li = new ListItem(liValue, liValue);
			
				DropDownListHours.Items.Add(li);
			}

			//Add values to Minutes drop down
			li = new ListItem("-", "-");
			DropDownListMinutes.Items.Add(li);

			for (int i = 0; i < 60; i+=15)
			{
				
				liValue = i.ToString("D2");
				li = new ListItem(liValue, liValue);
			
				DropDownListMinutes.Items.Add(li);
			}

			if ( setDateTime != new TDDateTime( new DateTime( 0 ) ) )
				//Set the datetime to the set value
				SetDateTimeValue( setDateTime );
		}

		public int Months
		{
			get
			{
				return _monthsDefault;
			}
			set
			{
				_monthsDefault = value;
			}
		}

		private bool IsLeapYear(int year)
		{
			return TDDateTime.IsLeapYear(year);
		}

		public bool IsValidDateTime
		{
			get
			{
				if (
					DropDownListDay.SelectedItem.Value == "-"
					|| DropDownListMonthYear.SelectedItem.Value == "-"
					|| DropDownListHours.SelectedItem.Value == "-"
					|| DropDownListMinutes.SelectedItem.Value == "-"
					)
					return false;

				string[] monthYear = DropDownListMonthYear.SelectedItem.Value.Split('-');
				int year = Convert.ToInt32(monthYear[0]);
				int month = Convert.ToInt32(monthYear[1]);
				int day = Convert.ToInt32(DropDownListDay.SelectedItem.Value);
				//two lines added to check
				int hour = Convert.ToInt32(DropDownListHours.SelectedItem.Value);
				int minute = Convert.ToInt32(DropDownListMinutes.SelectedItem.Value);

				if (month == 2)
					if (IsLeapYear(year))
						return day <= 29;
					else
						return day <= 28;
				else if (month == 9 || month == 4 || month == 6 || month == 11)
					return day <= 30;
				else
					return true;
			}
		}

		public TDDateTime DateTimeValue
		{
			get
			{
				if (IsValidDateTime)
				{
					string[] monthYear = DropDownListMonthYear.SelectedItem.Value.Split('-');
					int year = Convert.ToInt32(monthYear[0]);
					int month = Convert.ToInt32(monthYear[1]);
					int day = Convert.ToInt32(DropDownListDay.SelectedItem.Value);
					int hour = Convert.ToInt32(DropDownListHours.SelectedItem.Value);
					int minute = Convert.ToInt32(DropDownListMinutes.SelectedItem.Value);
					return new TDDateTime(year, month, day, hour, minute, 00);
									
				}
				else
				{
					return new TDDateTime(new DateTime(0));
				}
			}
			set
			{
				setDateTime = value;

				if (IsPostBack)
					SetDateTimeValue(setDateTime);
			}
		}

		private void SetDateTimeValue(TDDateTime dateTimeValue)
		{
			DropDownListDay.SelectedIndex = DropDownListDay.Items.IndexOf(DropDownListDay.Items.FindByValue(dateTimeValue.Day.ToString()));
			//            DropDownListDay.Items.FindByValue(dateTimeValue.Day.ToString()).Selected = true;
			DropDownListMonthYear.SelectedIndex = DropDownListMonthYear.Items.IndexOf(DropDownListMonthYear.Items.FindByValue(dateTimeValue.ToString("yyyy-M")));
			DropDownListHours.SelectedIndex = DropDownListHours.Items.IndexOf(DropDownListHours.Items.FindByValue(dateTimeValue.Hour.ToString()));
			int minute = dateTimeValue.Minute;
			minute = Convert.ToInt32((double)minute / 15) * 15;
			if (minute == 60)
				minute = 0;
			DropDownListMinutes.SelectedIndex = DropDownListMinutes.Items.IndexOf(DropDownListMinutes.Items.FindByValue(minute.ToString()));
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

		public ImageButton ControlCalendar
		{
			get
			{
				return commandCalendar;
			}
		}

		public ImageButton ControlOK
		{
			get
			{
				return commandOk;
			}
		}

		public void ButtonsVisible( bool visibleState )
		{
			ControlCalendar.Visible = visibleState;
			ControlOK.Visible = visibleState;
		}
	}
}
