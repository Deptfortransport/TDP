// ***********************************************
// NAME                 : FindLeaveReturnDatesControl.ascx.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 09/07/2004 
// DESCRIPTION          : Web control container for   
//                        the FindDateControl
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindLeaveReturnDatesControl.ascx.cs-arc  $ 
//
//   Rev 1.6   Dec 06 2010 12:54:58   apatel
//Code updated to implement show all show 10 feature for journey results and to remove anytime option from the input page.
//Resolution for 5651: CCN 593 - Show 10 results or show all
//
//   Rev 1.5   Mar 30 2010 16:00:20   mmodi
//Attached to event for the calendar ImageButton
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.4   Feb 11 2010 08:53:22   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.3   Oct 08 2008 10:29:44   mturner
//Updates for XHTML compliance
//
//   Rev 1.2.1.0   Sep 15 2008 10:57:24   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:20:42   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:14:18   mturner
//Initial revision.
//
//   Rev 1.16   May 01 2007 13:55:02   jfrank
//Fix so that No Return and Open Return options display on the ambguity pages
//Resolution for 4397: "No Return" and "Open Return" options do not appear on the journey ambiguity screen
//
//   Rev 1.15   Mar 10 2006 12:31:14   pscott
//SCr3510 add ClaendarClosed method
//
//   Rev 1.14   Feb 23 2006 19:16:40   build
//Automatically merged from branch for stream3129
//
//   Rev 1.13.1.0   Jan 10 2006 15:24:46   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.13   Nov 09 2005 14:03:32   NMoorhouse
//TD93 - UEE Input Pages - Update Visit Planner
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.12   Nov 03 2005 17:01:28   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.11.1.0   Oct 27 2005 13:59:48   NMoorhouse
//TD93 - UEE Input Pages, Date Control element CUT
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.11   Sep 29 2005 12:47:28   build
//Automatically merged from branch for stream2673
//
//   Rev 1.10.1.1   Sep 14 2005 13:17:38   rgreenwood
//DN079 ES015 Code Review
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.10.1.0   Sep 07 2005 13:47:12   rgreenwood
//DN079 ES015 24 Hour Help Button removal
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.10   Mar 04 2005 11:27:56   tmollart
//#region statements added to code. Had intended other changes but these were not made. Checked in anyway with region statements as the only change.
//
//   Rev 1.9   Aug 24 2004 11:32:04   passuied
//put allowAnytime initialisation if OnInit instead of PageLoad
//Resolution for 1426: Find a car hour and Minutes have duplicate "-"
//
//   Rev 1.8   Aug 17 2004 14:33:32   passuied
//Removed anytime from date controls
//Resolution for 1349: Find a car "Any time" should not be an option
//
//   Rev 1.6   Jul 28 2004 16:11:04   passuied
//Changes to make the FindA date controls work
//
//   Rev 1.5   Jul 27 2004 17:22:54   COwczarek
//Remove panel and label used to display "No return".
//Responsibility is now in TriStateDateControl.
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.4   Jul 22 2004 11:31:54   esevern
//set isOutward property of outward control
//
//   Rev 1.3   Jul 15 2004 11:42:04   esevern
//Added panel to be displayed with 'no return' message if no return journey selected
//
//   Rev 1.2   Jul 13 2004 15:31:02   esevern
//added comments
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
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.Web.Support;
	using TransportDirect.Common;
	using TransportDirect.UserPortal.SessionManager;

	/// <summary>
	///	This control contains no functionality, acting solely as a container
	///	for the outward and return FindDateControl. 
	/// </summary>
	public partial  class FindLeaveReturnDatesControl : TDUserControl
	{
		protected TransportDirect.UserPortal.Web.Controls.FindDateControl theLeaveDateControl;
        protected TransportDirect.UserPortal.Web.Controls.FindDateControl theReturnDateControl;
		protected CalendarControl calendar;

        protected bool showPlanningTip = true;

		protected void Page_Load(object sender, System.EventArgs e)
		{

            labelPlanningTip.Text = GetResource("DateControl.labelPlanningTip");
			
		}

        /// <summary>
        /// Page pre render event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            labelPlanningTip.Visible = showPlanningTip;
        }

		#region Public properties
		
		/// <summary>
		/// Read only property returning the leaving FindDateControl
		/// </summary>
		public FindDateControl LeaveDateControl 
		{
			get 
			{
				return theLeaveDateControl;
			}
		}

		/// <summary>
		/// Read only property returning the return FindDateControl
		/// </summary>
		public FindDateControl ReturnDateControl 
		{
			get 
			{
				return theReturnDateControl;
			}
		}

        /// <summary>
        /// Write Only property determining if the Planning tip should display or not
        /// </summary>
        public bool ShowPlanningTip
        {
            set
            {
                showPlanningTip = value;
            }
        }
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//Set Open Return
			if (this.PageId == PageId.FindFareInput || 
				this.PageId == PageId.JourneyPlannerInput ||
				this.PageId == PageId.JourneyPlannerAmbiguity ||
				(this.PageId == PageId.FindTrunkInput && TDSessionManager.Current.FindAMode == FindAMode.TrunkCostBased))
			{
				theReturnDateControl.DateControl.AllowOpenReturn = true;
			}

			theLeaveDateControl.IsOutward = true;
			theReturnDateControl.IsOutward = false;

			theLeaveDateControl.DateControl.AmbiguousDateControl.ButtonCalendar.Click += new System.Web.UI.ImageClickEventHandler(OnOutwardCalendarAmbiguousClick);
			theReturnDateControl.DateControl.AmbiguousDateControl.ButtonCalendar.Click += new System.Web.UI.ImageClickEventHandler(OnReturnCalendarAmbiguousClick);
			calendar.DateSelected += new EventHandler(OnCalendarDateSelected);
			
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		override protected void OnPreRender(EventArgs e)
		{
			//Hide Return Date Control
			if (this.PageId == PageId.VisitPlannerInput)
			{
				theReturnDateControl.Visible = false;
			}

			base.OnPreRender(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}


		#endregion

		#region Event Handlers
		private const string RES_CALENDARTITLE_OUT = "OutboundCalendarTitle";
		private const string RES_CALENDARTITLE_RET = "ReturnCalendarTitle";

		/// <summary>
		/// Handler for the outward calendar button click event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnOutwardCalendarAmbiguousClick(object sender, EventArgs e)
		{

			string resTitle = RES_CALENDARTITLE_OUT;
			

			if (theLeaveDateControl.DateControl.AmbiguousDateControl.Current != null)
				calendar.SetCalendar(theLeaveDateControl.DateControl.AmbiguousDateControl.Current, GetResource(resTitle));
			else
				calendar.SetCalendar(TDDateTime.Now, GetResource(resTitle));

			if (TDSessionManager.Current.FindPageState != null)
			{
				TDSessionManager.Current.FindPageState.CalendarIsForOutward = true;
			}
			else
			{
				TDSessionManager.Current.InputPageState.CalendarIsForOutward = true;
			}
			
			calendar.Open();
		}

		/// <summary>
		/// Handler for the return calendar button click event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnReturnCalendarAmbiguousClick(object sender, EventArgs e)
		{

			string resTitle = RES_CALENDARTITLE_RET;
			

			if (theReturnDateControl.DateControl.AmbiguousDateControl.Current != null)
				calendar.SetCalendar(theReturnDateControl.DateControl.AmbiguousDateControl.Current, GetResource(resTitle));
			else
				calendar.SetCalendar(TDDateTime.Now, GetResource(resTitle));

			if (TDSessionManager.Current.FindPageState != null)
			{
				TDSessionManager.Current.FindPageState.CalendarIsForOutward = false;
			}
			else
			{
				TDSessionManager.Current.InputPageState.CalendarIsForOutward = false;
			}
			
			
			calendar.Open();
		}

		private void OnCalendarDateSelected(object sender, EventArgs e)
		{
			TransferCalendarSelection();
		}
		#endregion



		/// <summary>
		/// Method to close down calendar.
		/// </summary>

		public void CalendarClose()
		{
			if( calendar.Visible== true)
			{
				calendar.Visible = false;
				calendar.Close();
			}


		}



		/// <summary>
		/// Method which transfer the calendar selection to the date controls
		/// then if it has changed, raise an event to advise the page to update the session.
		/// </summary>
		private void TransferCalendarSelection()
		{
			// We always update both normal and ambiguous controls, to keep it consistent
			if (calendar.TravelDate != null)
			{
				bool changed = false;

				// date to transfer to outward control
				if (TDSessionManager.Current.FindPageState != null)
				{
					if (TDSessionManager.Current.FindPageState.CalendarIsForOutward)
					{
						if (calendar.TravelDate != theLeaveDateControl.DateControl.AmbiguousDateControl.Current)
						{
							theLeaveDateControl.DateControl.AmbiguousDateControl.Current = calendar.TravelDate;	
							changed = true;
						}

						if (changed)
						{
							theLeaveDateControl.RaiseDateChangedEvent();
						}
					}
					else
						// date to transfer to return control
					{

						if (calendar.TravelDate != theReturnDateControl.DateControl.AmbiguousDateControl.Current)
						{
							theReturnDateControl.DateControl.AmbiguousDateControl.Current = calendar.TravelDate;	
							changed = true;
						}

						if (changed)
						{
							theReturnDateControl.RaiseDateChangedEvent();
						
						}
					}
				}
				else
				{
					if (TDSessionManager.Current.InputPageState.CalendarIsForOutward)
					{
						if (calendar.TravelDate != theLeaveDateControl.DateControl.AmbiguousDateControl.Current)
						{
							theLeaveDateControl.DateControl.AmbiguousDateControl.Current = calendar.TravelDate;	
							changed = true;
						}

						if (changed)
						{
							theLeaveDateControl.RaiseDateChangedEvent();
						}
					}
					else
						// date to transfer to return control
					{

						if (calendar.TravelDate != theReturnDateControl.DateControl.AmbiguousDateControl.Current)
						{
							theReturnDateControl.DateControl.AmbiguousDateControl.Current = calendar.TravelDate;	
							changed = true;
						}

						if (changed)
						{
							theReturnDateControl.RaiseDateChangedEvent();
						
						}
					}
				}
			}
		}
	}
}
