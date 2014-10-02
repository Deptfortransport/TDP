// ****************************************************************************************** 
// NAME                 : SimpleDateControl.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 28/07/2005
// ****************************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/SimpleDateControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Jul 28 2011 16:19:26   DLane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.2   Mar 31 2008 13:23:06   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:58   mturner
//Initial revision.
//
//   Rev 1.5   Feb 23 2006 19:16:52   build
//Automatically merged from branch for stream3129
//
//   Rev 1.4.1.0   Jan 10 2006 15:27:32   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   Nov 08 2005 18:01:38   ralonso
//commandCalendar imagebutton removed
//
//   Rev 1.3   Sep 26 2005 14:26:36   CRees
//Removed fixed width for date drop down, to accommodate longer month names
//
//   Rev 1.2   Sep 02 2005 17:09:14   RWilby
//Fix for IR2687 to allow Current property to be set before Page_Load has occurred
//Resolution for 2687: DN018 - When map region is selected the date is re-set (IE 5.5 only?)
//
//   Rev 1.1   Aug 03 2005 17:38:12   jgeorge
//FxCop recommended changes
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.0   Jul 28 2005 12:02:20   jgeorge
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Globalization;

	using TransportDirect.Common;
	using TransportDirect.UserPortal.ScriptRepository;
	using TransportDirect.Common.PropertyService.Properties;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.Web.Support;
	using TransportDirect.Common.Logging;
	using Logger = System.Diagnostics.Trace;

	/// <summary>
	///	Provides a simple date selection/display control with optional calendar button.
	///	Has layout advantages over the tristatedate control, and is more straightforward
	///	to use since it doesn't have a different subcontrol to initialise for each display
	///	mode.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class SimpleDateControl : TDPrintableUserControl, ILanguageHandlerIndependent
	{

		private GenericDisplayMode displayMode;
		private string monthYearSeparator;

		/// <summary>
		/// Event raised when the user clicks the calendar button. The host page is responsible
		/// for displaying a calendary control.
		/// </summary>
		public event EventHandler CalendarClicked;

		/// <summary>
		/// Event raises when the user changes the selected date.
		/// </summary>
		public event EventHandler DateChanged;

		#region Page event handlers

		/// <summary>
		/// Handler for the load event. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
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
			Populate();

            labelSRDate.Text = GetResource("outwardSelect.labelSRDate");
            labelSRMonthYear.Text = GetResource("outwardSelect.labelSRMonthYear");
        }

		/// <summary>
		/// Handler for the prerender event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			if (displayMode == GenericDisplayMode.ReadOnly || PrinterFriendly)
			{
				labelRODay.Visible = true;
				labelRODay.Text = listDays.SelectedItem.Text;

				labelSRMonthYear.Visible = true;
				labelSRMonthYear.Text = listMonths.SelectedItem.Text;

				listDays.Visible = false;
				listMonths.Visible = false;
				labelSRDate.Visible = false;
				labelSRMonthYear.Visible = false;
			}
			else
			{
				labelRODay.Visible = false;
				labelSRMonthYear.Visible = false;
				listDays.Visible = true;
				listMonths.Visible = true;
				labelSRDate.Visible = true;
				labelSRMonthYear.Visible = true;

				if (displayMode == GenericDisplayMode.Normal)
				{
					datePanel.CssClass = string.Empty;
				}
				else
				{
					datePanel.CssClass = "alerterror";
				}
			}
		}

		#endregion

		#region Control event handlers

		/// <summary>
		/// Handler for the SelectedIndexChanged event of both dropdowns
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void lists_SelectedIndexChanged(object sender, EventArgs e)
		{
			OnDateChanged();
		}


		#endregion

		#region Properties

		
		/// <summary>
		/// The text box which will appear to the left of the date dropdowns.
		/// Exposed to allow setting of text and control of visibility, etc.
		/// </summary>
		public Label HeadingLabel
		{
			get { return labelDate; }
		}

		/// <summary>
		/// Read/write property for the display mode of the control
		/// </summary>
		public GenericDisplayMode DisplayMode
		{
			get { return displayMode; }
			set { displayMode = value; }
		}

		/// <summary>
		/// Returns the current date, or null if it's invalid
		/// </summary>
		public TDDateTime Current
		{
			get 
			{
				string selected = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[] { listDays.SelectedValue, listMonths.SelectedValue });
				DateTime current;
				try
				{
					current = DateTime.ParseExact( selected, "dd MM" + monthYearSeparator + "yyyy", CultureInfo.InvariantCulture);
					return new TDDateTime(current);
				}
				catch (FormatException)
				{
					return null;
				}
			}
			set
			{
				//Fix for IR2687 to allow property to be set before Page_Load has occurred
				monthYearSeparator = Properties.Current["journeyplanner.monthyearseparator"];
				Populate();

				listDays.SelectedIndex = value.Day - 1;
				listMonths.SelectedValue = value.ToString("MM" + monthYearSeparator + "yyyy");
			}
		}

		#endregion

		#region Private/protected methods

		/// <summary>
		/// Raises the DateChanged event
		/// </summary>
		protected void OnDateChanged()
		{
			EventHandler e = DateChanged;
			if ( e != null )
				e(this, EventArgs.Empty);
		}

		/// <summary>
		/// Raises the calendarclicked event
		/// </summary>
		protected void OnCalendarClicked()
		{
			EventHandler e = CalendarClicked;
			if ( e != null )
				e(this, EventArgs.Empty);
		}

		/// <summary>
		/// Loads the list of months into the dropdown.
		/// </summary>
		private void Populate()
		{
            int indexlistMonths = listMonths.SelectedIndex;
			string[] months = Global.tdResourceManager.GetString("DateSelectControl.listMonths", TDCultureInfo.CurrentUICulture).Split(',');
			int currentMonth = DateTime.Now.Month-1; //conversion to have months from 0-11
			int numberOfMonths ;
			listMonths.Items.Clear();
			try
			{
				numberOfMonths = Convert.ToInt32(Properties.Current["controls.numberofmonths"], CultureInfo.CurrentUICulture.NumberFormat);
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
			
			string thisYear = DateTime.Now.Year.ToString(TDCultureInfo.CurrentUICulture.DateTimeFormat);
			int nextYearInt = DateTime.Now.Year + 1;
			string nextYear = nextYearInt.ToString(TDCultureInfo.CurrentUICulture.DateTimeFormat);

			for ( int i = 0 ; i < numberOfMonths ; i++ )
			{
				int monthToAdd = (currentMonth+i);
				string strYear;
				
				if (monthToAdd % 12 >= currentMonth ) // if the month is between currentMonth and december --> current year
					strYear = thisYear;
				else // if the month is between january and currentMonth-1 --> next year
					strYear = nextYear;

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
				ListItem liMonth = new ListItem(strMonth + " " + strYear, monthToAddDigit + monthYearSeparator + strYear);

				listMonths.Items.Add(liMonth);
			}
            listMonths.SelectedIndex = indexlistMonths;
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
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            listDays.SelectedIndexChanged += new EventHandler(this.lists_SelectedIndexChanged);
            listMonths.SelectedIndexChanged += new EventHandler(this.lists_SelectedIndexChanged);
		}
		#endregion
	}
}
