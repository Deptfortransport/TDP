// ***********************************************
// NAME                 : TriStateDateControl.ascx.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 09/07/2004 
// DESCRIPTION          : Web control container class   
//                        for FindDate controls
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TriStateDateControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Oct 28 2010 13:38:06   rbroddle
//Removed explicit wire up to Page_PreRender and Page_Init as AutoEventWireUp=true for this control so they were firing more than once
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.2   Mar 31 2008 13:23:30   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:30   mturner
//Initial revision.
//
//   Rev 1.25   Aug 06 2007 16:07:02   pscott
//IR 4473 - fix of return date defaults when incomplete return date entered
//
//   Rev 1.24   May 01 2007 14:02:44   jfrank
// Fix to make No Return and Open Return appear on the ambiguity page
//Resolution for 4397: "No Return" and "Open Return" options do not appear on the journey ambiguity screen
//
//   Rev 1.23   Apr 07 2006 10:43:02   kjosling
//A Fix to Fix my last Fix
//
//   Rev 1.22   Apr 05 2006 12:23:36   kjosling
//Fix. 
//Resolution for 3780: DN062 Amend Tool: 'Any time' option appears twice on Confirm Details page
//
//   Rev 1.21   Apr 04 2006 17:50:50   kjosling
//Fix. 
//Resolution for 3771: DN062 Amend Tool: 'No return' option appears twice on input pages
//
//   Rev 1.20   Mar 24 2006 17:17:32   kjosling
//Manually merged stream 0023 into trunk
//
//   Rev 1.19   Mar 22 2006 17:29:58   halkatib
//Changes due to Merge of stream3152 Landing Page phase 3
//
//   Rev 1.18   Feb 23 2006 16:14:22   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.17   Nov 21 2005 17:31:46   NMoorhouse
//Ensure that "Anytime" can be generically be displayed on Input page.
//Resolution for 3055: UEE: Select Amend on Find a Flight results does not retain "Anytime" for Return journey
//
//   Rev 1.16   Nov 09 2005 14:03:34   NMoorhouse
//TD93 - UEE Input Pages - Update Visit Planner
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.15   Nov 03 2005 17:08:04   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.14.1.0   Oct 27 2005 13:59:50   NMoorhouse
//TD93 - UEE Input Pages, Date Control element CUT
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.14   Apr 07 2005 18:34:56   tmollart
//Added code to add "Open Return" to return date drop down when a cost based page has been requested e.g. FindFareInput
//
//   Rev 1.13   Mar 04 2005 11:31:08   tmollart
//Modified get statement of flexibility property so that correct value is returned dependant on mode. 
//Added two propertys time/flexibility controls visible. These set the vixibility of all child controls. Makes it easy to set overall visibility. 
//Updated overloaded populate method that has parameter for flexibility.  This calls an existing populate method and then sets the flexibility dependant on the controls display mode.
//
//   Rev 1.12   Feb 11 2005 09:58:32   tmollart
//Added property for flexibility.
//
//   Rev 1.11   Oct 01 2004 11:03:48   COwczarek
//Make behaviour consistent for all Find A input pages when in ambiguity mode.
//Resolution for 1562: Find A input page in ambiguity mode always shows travel details
//
//   Rev 1.10   Sep 08 2004 15:00:28   COwczarek
//Set the ControlDateType property of the nested DateSelectControl to be either "Leave on" or "Return on".
//Resolution for 1336: Date controls for in Find A Flight inconsistent with other Find A pages
//
//   Rev 1.9   Sep 06 2004 11:48:10   jbroome
//IR 1474 - Added Any Time values to ambiguity drop downs if required.
//
//   Rev 1.8   Sep 02 2004 17:02:36   passuied
//Replaced hard coded value of "Any" by the unique key stored in FindInputAdapter
//Resolution for 1465: FindA "Amend journey" does not work with "Any time"
//
//   Rev 1.7   Aug 24 2004 11:31:18   passuied
//Added security not to populate extra items in date control ("-", "NoReturn") twice.
//
//Resolution for 1426: Find a car hour and Minutes have duplicate "-"
//
//   Rev 1.6   Jul 29 2004 17:07:30   COwczarek
//Add IsOpenReturnSelected and IsNoReturnSelected properties
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.5   Jul 28 2004 16:11:04   passuied
//Changes to make the FindA date controls work
//
//   Rev 1.4   Jul 27 2004 17:26:44   COwczarek
//Added Populate method
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.3   Jul 22 2004 11:56:18   esevern
//added properties to access date controls
//
//   Rev 1.2   Jul 13 2004 15:28:08   esevern
//added default case on page load
//
//   Rev 1.1   Jul 13 2004 10:58:20   esevern
//Interim check-in for addition to pages
//
//   Rev 1.0   Jul 09 2004 11:49:58   esevern
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using TransportDirect.Common.ResourceManager;
	using System.Globalization;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Common;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.SessionManager;

	/// <summary>
	///	This control provides access to input, modification and 
	///	display of journey dates and times
	/// </summary>
	public partial  class TriStateDateControl : TDUserControl
	{
		#region Declarations	
		private bool allowAnyTime;
		private bool allowOpenReturn;
		private bool outward = false;
		private bool timeControlsVisible = true;
		private GenericDisplayMode controlDisplayMode;
		private bool flexibilityControlsVisible = false;
		private bool arriveByOption = true;

		//Nested controls.
		protected DateDisplayControl displayControl;
		protected AmbiguousDateSelectControl ambiguousSelectControl; 
		#endregion

		#region Private Event Handlers
		/// <summary>
		/// Checks current display mode and sets control visibility accordingly
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
			switch (controlDisplayMode) 
			{
				case GenericDisplayMode.Normal:
					DisplayNormal();
					break;
				case GenericDisplayMode.Ambiguity:
					DisplayAmbiguity();
					break;
				case GenericDisplayMode.ReadOnly:
					DisplayReadOnly();
					break;
			}
		}

		#endregion

		#region Set control visibility

		/// <summary>
		/// Displays input control, hides ambiguity and read only controls.
		/// Calls populate method of DateSelectControl.
		/// </summary>
		private void DisplayNormal() 
		{
			displayControl.Visible = false;
			ambiguousSelectControl.Visible = true; 
			ambiguousSelectControl.ErrorMode = false;
		}

		/// <summary>
		/// Displays ambiguity control, hides input and read only controls
		/// </summary>
		private void DisplayAmbiguity() 
		{
			displayControl.Visible = false;
			ambiguousSelectControl.Visible = true;
			ambiguousSelectControl.ErrorMode = true;
		}

		/// <summary>
		/// Displays read only control, hides ambiguity and input controls. Calls
		/// populate method of DateDisplayControl.
		/// </summary>
		private void DisplayReadOnly() 
		{
			displayControl.Visible = true;
			ambiguousSelectControl.Visible = false; 
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

		#region public properties
		
		/// <summary>
		/// Read/write property to set or obtain the controls
		/// display mode (Normal, ReadOnly, Ambiguous)
		/// </summary>
		public GenericDisplayMode DisplayMode 
		{
			get 
			{
				return controlDisplayMode;
			}
			set 
			{
				controlDisplayMode = value;
			}
		}

		/// <summary>
		/// Read/write property that is set to true to enable "anytime" option
		/// </summary>
		public bool AllowAnyTime 
		{
			get 
			{
				return allowAnyTime;
			}
			set 
			{
				allowAnyTime = value;
			}
		}

		/// <summary>
		/// Read/write property that is set to true to enable "OpenReturn" option
		/// </summary>
		public bool AllowOpenReturn 
		{
			get 
			{
				return allowOpenReturn;
			}
			set 
			{
				allowOpenReturn = value;
			}
		}

		public bool IsOutward
		{
			get
			{
				return outward;
			}
			set
			{
				outward = value;
			}
		}

		/// <summary>
		/// Read property that is true if user has selected "anytime" option. 
		/// Will return false if anytime not selected or mode is read only.
		/// </summary>
		public bool IsAnyTimeSelected
		{
			get 
			{
				switch (controlDisplayMode) 
				{
					case GenericDisplayMode.Normal:
						return ambiguousSelectControl.Hour.Equals(Adapters.FindInputAdapter.AnyTimeValue);
					case GenericDisplayMode.Ambiguity:
						return ambiguousSelectControl.Hour.Equals(Adapters.FindInputAdapter.AnyTimeValue);
					default:
						return false;
				}
			}
		}

		/// <summary>
		/// Read property that is true if user has selected "no return".
		/// Will return false if "no return" not selected, mode is not normal input 
		/// or this control is not for return date/time.
		/// </summary>
		public bool IsNoReturnSelected
		{
			get 
			{
				if (controlDisplayMode == GenericDisplayMode.Normal && !outward) 
				{
					return ambiguousSelectControl.MonthYear.Equals("NoReturn");
				} 
				else 
				{
					return false;
				}
			}
		}

		/// <summary>
		/// Read property that is true if user has selected "open return".
		/// Will return false if "open return" not selected, mode is not normal input 
		/// or this control is not for return date/time.
		/// </summary>
		public bool IsOpenReturnSelected
		{
			get 
			{
				if (controlDisplayMode == GenericDisplayMode.Normal && !outward) 
				{
					return ambiguousSelectControl.MonthYear.Equals("OpenReturn");
				} 
				else 
				{
					return false;
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
				switch (controlDisplayMode) 
				{
					case GenericDisplayMode.Normal:
						return ambiguousSelectControl.Day;
					case GenericDisplayMode.Ambiguity:
						return ambiguousSelectControl.Day;
					default:
						return string.Empty;
				}
			}
		}

		/// <summary>
		/// Gets/Sets the Month and Year
		/// </summary>
		public string MonthYear
		{
			get
			{
				switch (controlDisplayMode) 
				{
					case GenericDisplayMode.Normal:
						return ambiguousSelectControl.MonthYear;
					case GenericDisplayMode.Ambiguity:
						return ambiguousSelectControl.MonthYear;
					default:
						return string.Empty;
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
				switch (controlDisplayMode) 
				{
					case GenericDisplayMode.Normal:
						return ambiguousSelectControl.Hour;
					case GenericDisplayMode.Ambiguity:
						return ambiguousSelectControl.Hour;
					default:
						return string.Empty;
				}
			}
		}

		/// <summary>
		///  Gets/Sets the Minute
		/// </summary>
		public string Minute
		{
			get
			{
				switch (controlDisplayMode) 
				{
					case GenericDisplayMode.Normal:
						return ambiguousSelectControl.Minute;
					case GenericDisplayMode.Ambiguity:
						return ambiguousSelectControl.Minute;
					default:
						return string.Empty;
				}
			}
		}

		/// <summary>
		/// Gets the flexibility value from the display/select control depending on mode.
		/// Sets the flexibility value on both the display and select controls.
		/// </summary>
		public int Flexibility
		{
			get 
			{
				switch (controlDisplayMode)
				{
					case GenericDisplayMode.Normal :
						return ambiguousSelectControl.Flexibility;
					case GenericDisplayMode.Ambiguity :
						return ambiguousSelectControl.Flexibility;
					default:
						return ambiguousSelectControl.Flexibility;
				}
			}
			set
			{
				ambiguousSelectControl.Flexibility = value;
				displayControl.Flexibility = value;
				ambiguousSelectControl.Flexibility = value;
			}
		}

		/// <summary>
		/// Gets/Sets if ArriveBefore /Leave after
		/// </summary>
		public bool ArriveBefore
		{
			get
			{
				switch (controlDisplayMode) 
				{
					case GenericDisplayMode.Normal:
						return ambiguousSelectControl.ArriveBefore;
					default:
						return false;
				}
			}
		}

		/// <summary>
		/// Read only property returning the date display control
		/// </summary>
		public DateDisplayControl DateDisplayControl
		{
			get 
			{
				return displayControl;
			}
		}
		
		/// <summary>
		/// Read only property returning the ambiguous date select control
		/// </summary>
		public AmbiguousDateSelectControl AmbiguousDateControl
		{
			get 
			{
				return ambiguousSelectControl; 
			}
		}

		/// <summary>
		/// Read/Write. Controls the visibility of the time controls on nested controls.
		/// Set to False so no time controls are shown. Set to true to show time controls.
		/// Sets: Select, Display and Ambiguity controls.
		/// </summary>
		public bool TimeControlsVisible
		{
			get {return timeControlsVisible;}
			set
			{
				timeControlsVisible = value;
				ambiguousSelectControl.TimeControlsVisible = value;
				displayControl.TimeControlsVisible = value;
				ambiguousSelectControl.TimeControlsVisible = value;
			}
		}

		/// <summary>
		/// Read/Write. Controls the visibility of the flexibility controls on nested controls.
		/// Set to False so no flex. controls are shown or true to show the controls.
		/// Sets: Select, Display and Ambiguity controls.
		/// </summary>
		public bool FlexibilityControlsVisible
		{
			get {return flexibilityControlsVisible;}
			set
			{
				flexibilityControlsVisible = value;
				ambiguousSelectControl.FlexibilityControlsVisible = value;
				displayControl.FlexibilityControlsVisible = value;
				ambiguousSelectControl.FlexibilityControlsVisible = value;
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
				ambiguousSelectControl.ArriveByOption = value;
			}
		}
		#endregion

		#region public methods

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
            if (!IsPostBack)
            {
                ambiguousSelectControl.Populate();

                if (allowAnyTime)
                {
                    ListItem item = new ListItem(GetResource("FindFlightInput.DataSelectControl.AnyTime"), Adapters.FindInputAdapter.AnyTimeValue);
                    ambiguousSelectControl.ControlHours.Items.Add(item);

                    if (outward)
                    {
                        item = new ListItem("-", string.Empty);
                        ambiguousSelectControl.ControlMinutes.Items.Add(item);
                    }
                }

                // security : don't populate it twice
                if (!outward && ambiguousSelectControl.ControlDays.Items[ambiguousSelectControl.ControlDays.Items.Count - 1].Value != string.Empty)
                {
                    ListItem item = new ListItem("-", string.Empty);
                    ambiguousSelectControl.ControlDays.Items.Add(item);

                    item = new ListItem("-", string.Empty);
                    ambiguousSelectControl.ControlHours.Items.Add(item);

                    item = new ListItem("-", string.Empty);
                    ambiguousSelectControl.ControlMinutes.Items.Add(item);

                    item = new ListItem(GetResource("FindFlightInput.DataSelectControl.NoReturn"), "NoReturn");
                    ambiguousSelectControl.ControlMonths.Items.Add(item);
                }
            }

			if(!outward)
			{
				ListItem item = new ListItem(GetResource("FindFlightInput.DataSelectControl.NoReturn"), "NoReturn");
				if(!ambiguousSelectControl.ControlMonths.Items.Contains(item))
				{
					ambiguousSelectControl.ControlMonths.Items.Add(item);
				}
				if (allowOpenReturn)
				{
					item = new ListItem(GetResource("OpenReturn"), "OpenReturn");
					if(!ambiguousSelectControl.ControlMonths.Items.Contains(item))
					{
						ambiguousSelectControl.ControlMonths.Items.Add(item);
					}
				}
			}

			switch (controlDisplayMode) 
			{
				case GenericDisplayMode.Normal:
					if (hour.Equals(Adapters.FindInputAdapter.AnyTimeValue))
					{
						ambiguousSelectControl.Populate(day,monthYear,arriveBefore);
					} 
					else 
					{
						ambiguousSelectControl.Hour = hour;
						ambiguousSelectControl.Minute = minute;
						ambiguousSelectControl.Day = day;
						ambiguousSelectControl.MonthYear = monthYear;
						ambiguousSelectControl.ArriveBefore = arriveBefore;
					}
					break;
				case GenericDisplayMode.Ambiguity:
					if (hour.Equals(Adapters.FindInputAdapter.AnyTimeValue) )
					{
						ambiguousSelectControl.Populate(day,monthYear,arriveBefore);
					} 
					else if (ambiguousSelectControl.ControlMonths.Items.Count >= 0)
					{
						ambiguousSelectControl.Populate(day,monthYear,hour,minute,arriveBefore);

						ListItem item = new ListItem(GetResource("FindFlightInput.DataSelectControl.NoReturn"), "NoReturn");
						if(!ambiguousSelectControl.ControlMonths.Items.Contains(item))
						{
							ambiguousSelectControl.ControlMonths.Items.Add(item);
						}
						if (allowOpenReturn)
						{
							item = new ListItem(GetResource("OpenReturn"), "OpenReturn");
							if(!ambiguousSelectControl.ControlMonths.Items.Contains(item))
							{
								ambiguousSelectControl.ControlMonths.Items.Add(item);
							}
						}

					}

					break;
				case GenericDisplayMode.ReadOnly:
					if (monthYear.Equals("NoReturn") || monthYear.Equals("OpenReturn")) 
					{
						displayControl.PopulateNoReturn();
					} 
					else 
					{
						if (hour.Equals(Adapters.FindInputAdapter.AnyTimeValue) || (hour.Length == 0))
							displayControl.Populate( TDDateTime.Parse( day + " " + monthYear , CultureInfo.CurrentCulture ), arriveBefore, true);
						else
							displayControl.Populate( TDDateTime.Parse( day + " " + monthYear + " " + hour +":" + minute , CultureInfo.CurrentCulture ), arriveBefore, false);
					}
					break;
				default:
					break;
			} 
		}

		/// <summary>
		/// Initialises the underlying date controls with the supplied parameters.
		/// Overloaded method containing paremter for flexibility control as well.
		/// </summary>
		/// <param name="outward">True if the control is for outward date/time</param>
		/// <param name="hour">initial hour value in format "hh"</param>
		/// <param name="minute">initial minute value in format "mm"</param>
		/// <param name="day">initial day of month value in format "dd"</param>
		/// <param name="monthYear">initial month/year value in format "mm/yyyy"</param>
		/// <param name="arriveBefore">True if arrive by time, false if leave after time</param>
		/// <param name="flexibility">Sets the flexibility drop down</param>
		public void Populate(string hour, string minute, string day, string monthYear, bool arriveBefore, int flexibility)
		{
			//Call main populate method and then set the flexibility afterwards.
			Populate(hour, minute, day, monthYear, arriveBefore);

			//Following pattern in main populate method, set the flexibility
			//dependent on the controlDisplayMode. Due to nature of existing code 
			//decided not to use a populate method and just to set the property directly.
			switch (controlDisplayMode)
			{
					//If in Normal e.g. date select mode set the flexibility.
				case GenericDisplayMode.Normal:
					ambiguousSelectControl.Flexibility = flexibility;
					break;
					//If in Ambiguity mode, call the populate flexibility 
				case GenericDisplayMode.Ambiguity:
					ambiguousSelectControl.Flexibility = flexibility;
					break;
				case GenericDisplayMode.ReadOnly:
					displayControl.Flexibility = flexibility;
					break;
			}
		}

		#endregion
	}
}
