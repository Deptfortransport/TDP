// *********************************************** 
// NAME                 : DateDisplayControl
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 22/09/2003 
// DESCRIPTION  : Control displaying a valid date
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/DateDisplayControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:19:50   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:58   mturner
//Initial revision.
//
//   Rev 1.16   Feb 23 2006 19:16:28   build
//Automatically merged from branch for stream3129
//
//   Rev 1.15.1.0   Jan 10 2006 15:23:58   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.15   Nov 01 2005 15:11:36   build
//Automatically merged from branch for stream2638
//
//   Rev 1.14.1.0   Sep 29 2005 14:07:14   tolomolaiye
//Updated to use DisplayFormatAdapter
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.15   Sep 27 2005 14:27:34   tolomolaiye
//Use DisplayFormatAdapter to format date
//
//   Rev 1.14   Mar 04 2005 11:22:18   tmollart
//Renamed methods that set time/flexibility controls visible for consistency.
//
//
//   Rev 1.13   Jan 19 2005 17:06:36   tmollart
//Added properties to control visibility of time element and flexibility,
//
//   Rev 1.12   Jan 19 2005 15:16:02   tmollart
//Updated control to contain a label for the date flexibility with property to set it that looks up required value from Data Services.
//
//   Rev 1.11   Nov 17 2004 17:03:36   jgeorge
//Change use of CurrentCulture to CurrentUICulture when retrieving resource text for "Any time".
//
//   Rev 1.10   Oct 01 2004 11:03:44   COwczarek
//Make behaviour consistent for all Find A input pages when in ambiguity mode.
//Resolution for 1562: Find A input page in ambiguity mode always shows travel details
//
//   Rev 1.9   Aug 23 2004 17:27:10   jmorrissey
//Fix for IR1301
//
//   Rev 1.8   Jul 27 2004 17:04:10   COwczarek
//Add PopulateNoReturn method
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.7   Jun 09 2004 16:33:00   jgeorge
//Added "Any time" functionality for Find a Flight
//
//   Rev 1.6   Nov 18 2003 11:48:56   passuied
//missing comments
//
//   Rev 1.5   Nov 18 2003 10:44:04   passuied
//changes to hopefully pass code review
//
//   Rev 1.4   Oct 28 2003 10:38:58   passuied
//changes after fxcop
//
//   Rev 1.3   Oct 20 2003 10:52:58   passuied
//Changes after fxcop
//
//   Rev 1.2   Oct 15 2003 15:33:32   passuied
//corrected langstrings pbs on input side
//
//   Rev 1.1   Sep 23 2003 02:05:22   passuied
//Latest working version
//
//   Rev 1.0   Sep 22 2003 19:33:46   passuied
//Initial Revision


namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Common;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.Common.ServiceDiscovery;
	using System.Globalization;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.Web.Adapters;

	/// <summary>
	///		Summary description for DateDisplayControl.
	/// </summary>
	public partial  class DateDisplayControl : TDUserControl
	{
		private IDataServices populator;
        private bool isNoReturn;
		private int flexibilityValue;
		private bool timeVisible = true;

		/// <summary>
		/// Constructor
		/// </summary>
		protected DateDisplayControl()
		{
			populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// Populates the control
		/// </summary>
		/// <param name="dt">DateTime to use</param>
		/// <param name="arriveBefore">arrive by(true) / leave after(false)</param>
		public void Populate(TDDateTime dt, bool arriveBefore)
		{
			Populate(dt, arriveBefore, false);
		}

		/// <summary>
		/// Populates the control, allowing the "Any time" facility used by Find a Flight
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="arriveBefore"></param>
		/// <param name="anyTime"></param>
		public void Populate(TDDateTime dt, bool arriveBefore, bool anyTime)
		{
			string[] months = Global.tdResourceManager.GetString("DateSelectControl.listMonths", TDCultureInfo.CurrentUICulture).Split(',');

			textDate.Text = DisplayFormatAdapter.StandardDateFormat(dt);

			//If timeVisible is set to false there is no need to construct the time element of the label.
			if (timeVisible)
			{
				//IR1301 - if anytime, still need to include the Leaving at or Arriving by text as well
				if (anyTime)
					textDate.Text += String.Format(" {0}: {1}", populator.GetText(DataServiceType.LeaveArriveDrop, arriveBefore.ToString().ToLower(TDCultureInfo.CurrentCulture)), Global.tdResourceManager.GetString("FindFlightInput.DataSelectControl.AnyTime", TDCultureInfo.CurrentUICulture));
				else
					textDate.Text += String.Format(" {0}: {1}", populator.GetText(DataServiceType.LeaveArriveDrop, arriveBefore.ToString().ToLower(TDCultureInfo.CurrentCulture)), DisplayFormatAdapter.StandardTimeFormat(dt));
				
				isNoReturn = false;
			}
		}

        /// <summary>
        /// Populates the control to display "No return"
        /// </summary>
        public void PopulateNoReturn()
        {
			textDate.Text = String.Empty;
            textDate.Text = Global.tdResourceManager.GetString(
                "panelNoReturn.labelNoReturn", TDCultureInfo.CurrentUICulture);
            isNoReturn = true;
        }

        /// <summary>
        /// Read only property that returns true if this control is displaying "No return" or false if displaying
        /// a date/time.
        /// </summary>
        public bool IsNoReturn 
        {
            get {
                return isNoReturn;
            }
        }

		/// <summary>
		/// Read/Write. Determines if the time element of the display is shown.
		/// This is evaluated by the Populate method which will construct the label
		/// contents string with or without the time element.
		/// </summary>
		public bool TimeControlsVisible
		{
			set
			{timeVisible = value;}
			get{return timeVisible;}
		}

		/// <summary>
		/// Read/Write. Sets/Gets the visiblity of the flexibility label contained
		/// on the control.
		/// </summary>
		public bool FlexibilityControlsVisible
		{
			set{flexibilityLabel.Visible = value;}
			get{return flexibilityLabel.Visible;}
		}

		/// <summary>
		/// Read/Write. Sets the flexibility label display of the control.
		/// NOTE: Use the flexibility value and not the actual required flexibility text.
		/// Data services will be used to look up the required text.
		/// </summary>
		public int Flexibility
		{
			set
			{
				flexibilityValue = value;
				//Look up flexibility text using data services and populate the label.
				flexibilityLabel.Text = "(" + populator.GetText(DataServiceType.DateFlexibilityDrop, value.ToString()) + ")";
			}
			get {return flexibilityValue;}
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
	}
}
