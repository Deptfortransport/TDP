// *********************************************** 
// NAME                 : PtWalkingSpeedOptionsControl.ascx
// AUTHOR               : Reza Bamshad
// DATE CREATED         : 11/01/2005
// DESCRIPTION  : Public Transport journey details allowing user to allocate 
//walking speed and duration. This is used in both find a and door to door.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/PtWalkingSpeedOptionsControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Apr 08 2008 13:50:44   apatel
//set pageoptionscontrol visibility 
//
//   Rev 1.2   Mar 31 2008 13:22:34   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Feb 09 2008 06:33:00 apatel
//  White Labeling - modified to add PageOptionsControl.
//
//   Rev 1.0   Nov 08 2007 13:17:10   mturner
//Initial revision.
//
//   Rev 1.9   Apr 05 2006 15:42:52   build
//Automatically merged from branch for stream0030
//
//   Rev 1.8.1.0   Mar 28 2006 18:26:50   esevern
//Corrected setting of displayWalkingSpeed and displayWalkingTime labels for FindBusInput
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.8   Feb 23 2006 19:17:02   build
//Automatically merged from branch for stream3129
//
//   Rev 1.7.1.1   Jan 30 2006 14:41:20   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.7.1.0   Jan 10 2006 15:26:54   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.7   May 19 2005 13:48:28   ralavi
//Modifications after running FXCop and ensuring that car details label is visible in ambiguity page if  walking speed or duration is changed.
//
//   Rev 1.6   Apr 26 2005 10:32:46   Ralavi
//Fixed Welsh for drop downs.
//
//   Rev 1.5   Apr 06 2005 11:34:40   Ralavi
//Ensuring extra "mins" is not displayed under the walking heading
//
//   Rev 1.4   Mar 24 2005 14:58:42   RAlavi
//Changes to ensure via maps work correctly in door to door
//
//   Rev 1.3   Mar 18 2005 10:37:36   RAlavi
//Ensuring that public transport values are passed on to journey parameters
//
//   Rev 1.2   Mar 08 2005 09:35:20   PNorell
//new functionality for saving preferences.
//Resolution for 1958: DEV CODE Review: Preferences (Car and Public)
//
//   Rev 1.1   Feb 23 2005 16:34:48   RAlavi
//Changed for car costing
//
//   Rev 1.0   Feb 01 2005 10:13:58   rgreenwood
//Initial revision.
//
namespace TransportDirect.UserPortal.Web.Controls
{
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


	/// <summary>
	///		Summary description for PtWalkingSpeedOptionsControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class PtWalkingSpeedOptionsControl : TDUserControl
	{
		#region Controls

		

		#endregion

		#region Constants/variables

		// Keys used to obtain strings from the resource file
		private const string WalkingHeaderKey = "PtWalkingSpeedOptionsControl.WalkingHeader";
		private const string WalkingSpeedTitleKey = "PtWalkingSpeedOptionsControl.WalkingSpeedTitle";
		private const string WalkingDurationTitleKey = "PtWalkingSpeedOptionsControl.WalkingDurationTitle";
		private const string WalkingTimeMinutesKey = "PtWalkingSpeedOptionsControl.WalkingTimeMinutes";
		private const string DisplayWalkingSpeedKey = "ptWalkingSpeedOptionsControl.DisplayWalkingSpeed";
		private const string DisplayWalkingTimeKey = "ptWalkingSpeedOptionsControl.DisplayWalkingTime";
		private const string DisplayWalkingTimeMinutesKey = "ptWalkingSpeedOptionsControl.DisplayWalkingTimeMinutes";

        //private field for holding value for PageOptionsControl visiblility.
        private bool pageOptionsVisible = true;



		// Page level variables
		private IDataServices ds;

		private GenericDisplayMode walkingSpeedDisplayMode = GenericDisplayMode.Normal;
		private GenericDisplayMode walkingDurationDisplayMode = GenericDisplayMode.Normal;

		private bool preferencesChanged;

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
		/// Readonly property ensuring that if WalkingSpeed or duration are changed, preferencesChanged
		/// is set to true. 
		/// </summary>

		public bool PreferencesChanged
		{
			get 
			{
				if ((!WalkingSpeedIsDefault()) || (!WalkingDurationIsDefault()))
				{
					preferencesChanged = true;
				}
				else
				{
					preferencesChanged = false;
				}
				return preferencesChanged; 
			}
		}

        /// <summary>
        /// Read Only property allows access to PageOptionsControl contained within this control.
        /// </summary>
        public FindPageOptionsControl PageOptionsControl
        {
            get
            { 
                return pageOptionsControl;
            }
            
        }

        /// <summary>
        /// White labeling - Allows to set wether PageOptionsControl should be visible or not.
        /// </summary>
        public bool IsPageOptionsVisible
        {

            get { return pageOptionsVisible; }
            set { pageOptionsVisible = value; }
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
			UpdateControls();

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
		}

		/// <summary>
		/// Sets visibility of controls according to the property values
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			if( !IsPostBack )
			{
				UpdateControls();
			}
			UpdateWalkingControl();

            //Setting PageOptionsControl's visibility
            pageOptionsControl.Visible = IsPageOptionsVisible;		
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
		/// Updates the state of nested controls with this object's property values
		/// </summary>
		private void UpdateControls() 
		{
						
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
