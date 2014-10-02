// *********************************************** 
// NAME                 : D2DPreferencesOptionsControl.ascx.cs
// AUTHOR               : Jonathan George
// DATE CREATED         : 12/07/2004
// DESCRIPTION  : Control to display preferences header bar
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/D2DPreferencesOptionsControl.ascx.cs-arc  $ 
//
//   Rev 1.0   Jan 10 2013 16:34:00   dlane
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.2   Mar 31 2008 13:20:48   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Mar 28 2008 16:40 dgath
//  Modified Page_Load to set commandSubmit to be not visible on FindABus page, as this 
//  page already had Next buttons on both the main options and Advanced / Public Transport 
//  Journey Details options
//
//  Rev DevFactory Feb 09 2008 07:38 apatel
//  Modified page_Load method for not showing the submit button for VisitPlannerInput page.
//
// RevDevfactory Feb 04 2008 15:28:03   aahmed
//  removed the following PageId == PageId.JourneyPlannerInput 
// PageId == PageId.JourneyPlannerAmbiguity
//
//RevDevfactory Feb 01 2008 14:18:43   sjohal
// removed the following PageId == PageId.ParkAndRideInput
//
//   Rev 1.0   Nov 08 2007 13:14:26   mturner
//Initial revision.
//
//   Rev 1.30   Jun 07 2007 15:23:22   mmodi
//Updated command submit flag to be true
//Resolution for 4409: Minor Portal Changes
//
//   Rev 1.29   Jun 05 2007 17:26:42   nrankin
//IR4409
//Resolution for 4409: Minor Portal Changes
//
//   Rev 1.28   Jun 05 2007 15:10:42   mmodi
//Added Submit button and event handler for it code
//Resolution for 4409: Minor Portal Changes
//
//   Rev 1.27   Apr 24 2006 17:46:12   halkatib
//Resolution for IR3951 Added logical checks for the ParkAndRideInput page where there are checks for the FindACarPage since we are mimicing this behaviour
//
//   Rev 1.26   Apr 05 2006 15:42:52   build
//Automatically merged from branch for stream0030
//
//   Rev 1.25.1.0   Mar 24 2006 12:28:12   esevern
//added check for FindBusInput page when setting visibility loginSaveOption control.
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.25   Feb 23 2006 19:16:42   build
//Automatically merged from branch for stream3129
//
//   Rev 1.24.1.0   Jan 10 2006 15:24:52   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.24   Nov 18 2005 10:07:48   asinclair
//Fixed Merge Errors
//
//   Rev 1.23   Nov 17 2005 15:35:20   pcross
//Merge fix
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.22   Nov 17 2005 11:16:26   pcross
//Manual merge of stream2880
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.21   Nov 15 2005 09:45:08   asinclair
//IR Fix
//Resolution for 3000: Visit Planner: Login/register text appears when Advanced options switched on and off
//
//   Rev 1.20.1.0   Nov 10 2005 11:57:48   RGriffith
//Removed Help TDButton
//
//   Rev 1.20   Nov 08 2005 19:42:22   AViitanen
//HTML button change
//
//   Rev 1.19   Nov 08 2005 19:23:04   AViitanen
//Removing Hide details button
//
//   Rev 1.18   Nov 07 2005 18:02:14   ralonso
//Fixed preferencesHelpControl.AlternateText problem
//
//   Rev 1.17   May 19 2005 12:21:50   ralavi
//Modifications as the result of FXCop run.
//
//   Rev 1.16   Apr 18 2005 11:51:30   tmollart
//Modified so that save check box or message not visible when Page is FindFareInput. This is because the FindFarePreferenceControl has its own save check box.
//Resolution for 2159: Find a fare save preferences check-box
//
//   Rev 1.15   Apr 13 2005 12:10:12   Ralavi
//Adding Help alt text
//
//   Rev 1.14   Mar 23 2005 10:04:40   rscott
//Updated properties
//
//   Rev 1.13   Mar 14 2005 15:36:46   RAlavi
//Modifying preferences labels for car costing
//
//   Rev 1.12   Mar 08 2005 09:35:50   PNorell
//new functionality for saving preferences.
//Resolution for 1958: DEV CODE Review: Preferences (Car and Public)
//
//   Rev 1.11   Feb 23 2005 16:33:18   RAlavi
//Changed for car costing
//
//   Rev 1.10   Feb 22 2005 10:09:48   tmollart
//Disabled view state and modified pre_render code so that control text is reloaded regardless of post back status.
//
//   Rev 1.9   Feb 22 2005 10:03:30   RAlavi
//Checked in after modifications relating to car costing
//
//   Rev 1.8   Feb 14 2005 14:20:22   tmollart
//Added property to set visibility of help control.
//
//   Rev 1.7   Feb 14 2005 13:52:58   RAlavi
//changed properties to false for help label
//
//   Rev 1.6   Jan 28 2005 18:45:32   ralavi
//Updated for car costing
//
//   Rev 1.5   Jul 20 2004 15:24:58   jgeorge
//Removed properties from ViewState
//
//   Rev 1.4   Jul 20 2004 14:59:24   COwczarek
//Use CurrentUICutlure rather than CurrentCulture when accessing langstrings
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.3   Jul 19 2004 11:47:46   jgeorge
//Bug fix
//
//   Rev 1.2   Jul 14 2004 12:47:52   jgeorge
//Updated after testing
//
//   Rev 1.1   Jul 13 2004 10:59:42   jgeorge
//Interim check-in
//
//   Rev 1.0   Jul 13 2004 10:53:04   jgeorge
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

	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Web.Support;
	using TransportDirect.Common;

	/// <summary>
	///	Control to display preferences header bar.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial  class D2DPreferencesOptionsControl : TDUserControl
	{
		#region Controls
				
		// Keys used to obtain strings from the resource file
		private const string HidePreferencesImageUrlKey = "PreferencesOptionsControl.HidePreferences.Url.{0}";
		private const string HidePreferencesImageAltTextKey = "PreferencesOptionsControl.HidePreferences.Alt.{0}";
		private const string PreferencesHeaderLabelTextKey = "PreferencesOptionsControl.PreferencesHeaderLabel.{0}";
		private const string PreferencesCarHeaderLabelTextKey = "PreferencesOptionsControl.PreferencesCarHeaderLabel.{0}";
		private const string PreferencesHelpKey = "preferencesHelpLabel";
		private const string SubmitImageTextKey = "FindPageOptionsControl.Submit.Text";
		
		protected TravelDetailsControl loginSaveOption;


		#endregion

		#region Public events

		// The following three lines declare objects that can be used as
		// keys in the EventHandlers table for the control.
		private static readonly object HidePreferencesEventKey = new object();

		/// <summary>
		/// Occurs when the hide preferences button is clicked
		/// </summary>
		public event EventHandler HidePreferences
		{
			add { this.Events.AddHandler(HidePreferencesEventKey, value); }
			remove { this.Events.RemoveHandler(HidePreferencesEventKey, value); }
		}


		// For when the Submit button is clicked
		private static readonly object SubmitEventKey = new object();

		/// <summary>
		/// Occurs when the Submit button is clicked
		/// </summary>
		public event EventHandler Submit
		{
			add { this.Events.AddHandler(SubmitEventKey, value); }
			remove { this.Events.RemoveHandler(SubmitEventKey, value); }
		}

		#endregion

		#region Constants/variables


		#endregion

		#region Page lifecycle event handlers

		/// <summary>
		/// Handler for the Load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
			// Load the appropriate image/test based on the parent page
			TDResourceManager rm = Global.tdResourceManager;
			
			// Hide the login save when in journey planner input and find a car pages
            if (!IsPostBack)
            {

                if (PageId == PageId.JourneyPlannerInput || PageId == PageId.FindCarInput
                    || PageId == PageId.JourneyPlannerAmbiguity
                    || PageId == PageId.FindFareInput
                    || PageId == PageId.FindBusInput
                    || PageId == PageId.VisitPlannerInput
                    || PageId == PageId.ParkAndRideInput)
                {
                    loginSaveOption.Visible = false;
                }
                else
                {
                    loginSaveOption.Visible = true;
                }
            }
		}

		/// <summary>
		/// Handler for the PreRender event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			// Set state of the preferences control
			if(TDSessionManager.Current.Authenticated)
				loginSaveOption.LoggedInDisplay();
			else
				loginSaveOption.LoggedOutDisplay();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Controls visibility of the "save preferences" facility
		/// </summary>
		public bool AllowSavePreferences
		{
			get { return loginSaveOption.Visible; }
			set { loginSaveOption.Visible = value; }
		}

		/// <summary>
		/// Returns true if the user is logged in and has elected to save their travel details
		/// Read only.
		/// </summary>
		public bool SavePreferences
		{
			get { return loginSaveOption.SaveDetails; }
		}

		#endregion

		#region Control event handler

		/// <summary>
		/// Handles the click event of the command buttons and raises the appropriate event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnCommandButtonClick(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			EventHandler theDelegate = null;

			if (theDelegate != null)
				theDelegate(this, EventArgs.Empty);
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
