// *********************************************** 
// NAME                 : D2DPtWalkingSpeedOptionsControl.ascx
// AUTHOR               : David Lane
// DATE CREATED         : 09/01/2013
// DESCRIPTION  : Public Transport journey details allowing user to allocate 
//walking speed and duration. This is used in door to door.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/D2DPtWalkingSpeedOptionsControl.ascx.cs-arc  $ 
//
//   Rev 1.4   Jan 30 2013 13:47:48   mmodi
//Fixed showing advanced options on ambiguity page
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.1   Jan 17 2013 09:46:04   mmodi
//Updates to D2D advanced options for better js and non-js behaviour
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Jan 10 2013 16:34:06   DLane
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//

using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;

using TransportDirect.Web.Support;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///	Summary description for D2DPtWalkingSpeedOptionsControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class D2DPtWalkingSpeedOptionsControl : TDUserControl
	{
		#region Constants/variables

		// Keys used to obtain strings from the resource file
		private const string WalkingHeaderKey = "PtWalkingSpeedOptionsControl.WalkingHeader";
		private const string WalkingSpeedTitleKey = "PtWalkingSpeedOptionsControl.WalkingSpeedTitle";
		private const string WalkingDurationTitleKey = "PtWalkingSpeedOptionsControl.WalkingDurationTitle";
		private const string WalkingTimeMinutesKey = "PtWalkingSpeedOptionsControl.WalkingTimeMinutes";
		private const string DisplayWalkingSpeedKey = "PtWalkingSpeedOptionsControl.DisplayWalkingSpeed";
		private const string DisplayWalkingTimeKey = "PtWalkingSpeedOptionsControl.DisplayWalkingTime";
		private const string DisplayWalkingTimeMinutesKey = "PtWalkingSpeedOptionsControl.DisplayWalkingTimeMinutes";
                
		// Page level variables
		private IDataServices ds;

		private GenericDisplayMode walkingSpeedDisplayMode = GenericDisplayMode.Normal;
		private GenericDisplayMode walkingDurationDisplayMode = GenericDisplayMode.Normal;

		#endregion

		#region Public events

		// The following lines declare objects that can be used as
		// keys in the EventHandlers table for the control.
		private static readonly object WalkingSpeedOptionChangedEventKey = new object();
		private static readonly object WalkingDurationOptionChangedEventKey = new object();
		private static readonly object PreferencesVisibleWalkingEventKey = new object();

		/// <summary>
		/// Occurs when the selection is changed in the "Speed" dropdown
		/// </summary>
		public event EventHandler WalkingSpeedOptionChanged
		{
			add { this.Events.AddHandler(WalkingSpeedOptionChangedEventKey, value); }
			remove { this.Events.RemoveHandler(WalkingSpeedOptionChangedEventKey, value); }
		}

		/// <summary>
		/// Occurs when the selection is changed in the "Duration" dropdown
		/// </summary>
		public event EventHandler WalkingDurationOptionChanged
		{
			add { this.Events.AddHandler(WalkingDurationOptionChangedEventKey, value); }
			remove { this.Events.RemoveHandler(WalkingDurationOptionChangedEventKey, value); }
		}

		/// <summary>
		/// Occurs when the user clicks the show preferences or hide preferences buttons
		/// </summary>
		public event EventHandler PreferencesVisibleChanged
		{

			add { this.Events.AddHandler(PreferencesVisibleWalkingEventKey, value); }
			remove { this.Events.RemoveHandler(PreferencesVisibleWalkingEventKey, value); }
		
		}

		#endregion

		#region Properties

		
		/// <summary>
		/// Returns true if the control is being displayed in ambiguity mode. This is determined
		/// from the values of <code>SpeedDisplayMode</code> and <code>DurationDisplayMode</code>.
		/// </summary>
		public bool AmbiguityMode
		{
			get { return ((WalkingSpeedDisplayMode != GenericDisplayMode.Normal) || (WalkingDurationDisplayMode != GenericDisplayMode.Normal)); }
		}

		/// <summary>
		/// Sets the mode for the Walking Speed dropdown.
		/// Note that in this context, GenericDisplayMode.Ambiguity is treated
		/// the same way as GenericDisplayMode.Readonly.
		/// </summary>
		public GenericDisplayMode WalkingSpeedDisplayMode
		{
			get { return walkingSpeedDisplayMode; }
			set { walkingSpeedDisplayMode = value; }
		}

		/// <summary>
		/// Sets the mode for the Walking Duration dropdown.
		/// Note that in this context, GenericDisplayMode.Ambiguity is treated
		/// the same way as GenericDisplayMode.Readonly.
		/// </summary>
		public GenericDisplayMode WalkingDurationDisplayMode
		{
			get { return walkingDurationDisplayMode; }
			set { walkingDurationDisplayMode = value; }
		}
	
		/// <summary>
		/// The option selected for walking speed
		/// </summary>
		public int WalkingSpeed
		{
			get 
			{
				string itemValue = ds.GetValue(DataServiceType.WalkingSpeedDrop, listWalkingSpeed.SelectedItem.Value);
				return Convert.ToInt32(itemValue);
			}
			set
			{
				string speedId = ds.GetResourceId(DataServiceType.WalkingSpeedDrop,value.ToString());
				ds.Select(listWalkingSpeed, speedId); 
			}
		}

		/// <summary>
		/// The option selected for walking duration
		/// </summary>
		public int WalkingDuration
		{
			get 
			{ 
				string itemValue = ds.GetValue(DataServiceType.WalkingMaxTimeDrop,listWalkingTime.SelectedItem.Value);
				return Convert.ToInt32(itemValue);
			}
			set
			{
				string durationId = ds.GetResourceId(DataServiceType.WalkingMaxTimeDrop,value.ToString());                
				ds.Select(listWalkingTime, durationId);
			}
		}
                
        /// <summary>
        /// Read only. Returns true if non-default options have been selected
        /// </summary>
        public bool IsOptionSelected
        {
            get { return !WalkingSpeedIsDefault() || !WalkingDurationIsDefault(); }
        }
		
		#endregion

		#region Page event handlers

		/// <summary>
		/// Handler for the Init event. Sets up global variables and additional
		/// event handlers.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Init(object sender, System.EventArgs e)
		{
			// Assign values to page level variables
			ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			resourceManager = Global.tdResourceManager;	
		}

		/// <summary>
		/// Handler for the Load event. Sets up the page.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Load strings from the languages file
			labelWalking.Text = resourceManager.GetString(WalkingHeaderKey, TDCultureInfo.CurrentUICulture);
			labelWalkingSpeedTitle.Text = resourceManager.GetString(WalkingSpeedTitleKey, TDCultureInfo.CurrentUICulture) + " ";
			labelWalkingTimeTitle.Text = resourceManager.GetString(WalkingDurationTitleKey, TDCultureInfo.CurrentUICulture) + " ";
			labelWalkingTimeMinutes.Text = " " + resourceManager.GetString(WalkingTimeMinutesKey, TDCultureInfo.CurrentUICulture);
			
            // Load the dropdowns
            // setting the selected index of the dropdowns
            int walkingSpeedIndex = listWalkingSpeed.SelectedIndex;
            int walkingTimeIndex = listWalkingTime.SelectedIndex;
            ds.LoadListControl(DataServiceType.WalkingSpeedDrop, listWalkingSpeed);
            ds.LoadListControl(DataServiceType.WalkingMaxTimeDrop, listWalkingTime);
            listWalkingSpeed.SelectedIndex = walkingSpeedIndex;
            listWalkingTime.SelectedIndex = walkingTimeIndex;

            labelJsQuestion.Text = GetResource("WalkingSpeedControl.Question");
            labelOptionsSelected.Text = GetResource("WalkingSpeedControl.OptionsSelected");

            btnShow.Text = GetResource("AdvancedOptions.Show.Text");
        }

		/// <summary>
		/// Sets visibility of controls according to the property values
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			UpdateWalkingControl();
		}

		#endregion

		#region Control event handlers

		/// <summary>
		/// Handles the Changed event of the walking dropdown and raises
		/// the event on to the client
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnWalkingOptionChanged(object sender, EventArgs e)
		{
			RaiseEvent(WalkingSpeedOptionChangedEventKey);
		}

		/// <summary>
		/// Handles the Changed event of the walking duration dropdown and
		/// raises the event on to the client
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnWalkingDurationOptionChanged(object sender, EventArgs e)
		{
			RaiseEvent(WalkingDurationOptionChangedEventKey);
		}

        /// <summary>
        /// Show button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShow_Click(object sender, EventArgs e)
        {
            UpdateOptionsVisibility(true);

            btnShow.Visible = false;
        }
		
		#endregion

        #region Public methods

        /// <summary>
        /// Resets the control and its state
        /// </summary>
        public void Reset()
        {
            UpdateOptionsVisibility(false);

            btnShow.Visible = true;
        }

        #endregion

		#region Helper methods

		/// <summary>
		/// Retrieves the delegate attached to an event handler from the Events
		/// list and calls it.
		/// </summary>
		/// <param name="key"></param>
		private void RaiseEvent(object key)
		{
			EventHandler theDelegate = Events[key] as EventHandler;
			if (theDelegate != null)
				theDelegate(this, EventArgs.Empty);
		}

		/// <summary>
		/// Returns true if the choosen walking speed is the drop down default value, false otherwise
		/// </summary>
		/// <returns>true if the choosen walking speed is the drop down default value, false otherwise</returns>
		private bool WalkingSpeedIsDefault() 
		{
			string defaultItemValue = ds.GetDefaultListControlValue(DataServiceType.WalkingSpeedDrop);
			int defaultWalkingSpeed = Convert.ToInt32(defaultItemValue);
			return defaultWalkingSpeed == WalkingSpeed;
		}

		/// <summary>
		/// Returns true if the choosen walking duration is the drop down default value, false otherwise
		/// </summary>
		/// <returns>true if the choosen walking duration is the drop down default value, false otherwise</returns>
		private bool WalkingDurationIsDefault()
		{
			string defaultItemValue = ds.GetDefaultListControlValue(DataServiceType.WalkingMaxTimeDrop);
			int defaultWalkingDuration = Convert.ToInt32(defaultItemValue);
			return defaultWalkingDuration == WalkingDuration;
		}

		/// <summary>
		/// Sets the state of the walking controls
		/// </summary>
		private void UpdateWalkingControl()
		{
			bool showWalkingSpeed = !WalkingSpeedIsDefault();
			bool showWalkingDuration = !WalkingDurationIsDefault();

			switch (WalkingSpeedDisplayMode)
			{
				case GenericDisplayMode.ReadOnly:
				case GenericDisplayMode.Ambiguity:
					listWalkingSpeed.Visible = false;
					labelSRWalkingSpeed.Visible = showWalkingSpeed;
					displayWalkingSpeed.Visible = showWalkingSpeed;
					
					if (showWalkingSpeed) 
					{
						displayWalkingSpeed.Text = 
							resourceManager.GetString(DisplayWalkingSpeedKey, TDCultureInfo.CurrentCulture) + ": " + listWalkingSpeed.SelectedItem.Text;
					}

					labelWalkingSpeedTitle.Visible = false;
					panelChanges.Visible = showWalkingSpeed || showWalkingDuration;
					break;				
				case GenericDisplayMode.Normal:		
				default:
					listWalkingSpeed.Visible = true;
					labelSRWalkingSpeed.Visible = false;
					labelWalkingSpeedTitle.Visible = true;
					displayWalkingSpeed.Visible = false;
					panelChanges.Visible = true;
					break;
			}

			switch (WalkingDurationDisplayMode)
			{
				case GenericDisplayMode.ReadOnly:
				case GenericDisplayMode.Ambiguity:
					listWalkingTime.Visible = false;
					labelSRWalkingTime.Visible = showWalkingDuration;
					displayWalkingTime.Visible = showWalkingDuration;

					if (showWalkingDuration) 
					{
						displayWalkingTime.Text =
							resourceManager.GetString(DisplayWalkingTimeKey, TDCultureInfo.CurrentCulture) + ": " + listWalkingTime.SelectedItem.Text + " "
							+ resourceManager.GetString(DisplayWalkingTimeMinutesKey, TDCultureInfo.CurrentCulture);
					}

					labelWalkingTimeTitle.Visible = false;
					labelWalkingTimeMinutes.Visible = false;
					panelChanges.Visible = showWalkingSpeed || showWalkingDuration;
					break;
				case GenericDisplayMode.Normal:
				default:
					listWalkingTime.Visible = true;
					labelSRWalkingTime.Visible = false;
					labelWalkingTimeTitle.Visible = true;
					displayWalkingTime.Visible = false;
					panelChanges.Visible = true;
					break;
			}
		}

        /// <summary>
        /// Updates the display class of the options content
        /// </summary>
        private void UpdateOptionsVisibility(bool showExpanded)
        {
            if (showExpanded)
            {
                if (!optionContentRow.Attributes["class"].Contains("show"))
                    optionContentRow.Attributes["class"] = string.Format("{0} show",
                        optionContentRow.Attributes["class"].Replace("hide", string.Empty));
            }
            else
            {
                if (!optionContentRow.Attributes["class"].Contains("hide"))
                    optionContentRow.Attributes["class"] = string.Format("{0} hide",
                        optionContentRow.Attributes["class"].Replace("show", string.Empty));
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
            listWalkingTime.SelectedIndexChanged += new EventHandler(this.OnWalkingDurationOptionChanged);
            listWalkingSpeed.SelectedIndexChanged += new EventHandler(this.OnWalkingOptionChanged);
        }
		#endregion
	}
}
