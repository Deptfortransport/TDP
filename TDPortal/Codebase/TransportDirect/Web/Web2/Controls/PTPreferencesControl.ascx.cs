// *************************************************************************** 
// NAME                 : PtPreferencesControl.ascx
// AUTHOR               : Reza bamshad
// DATE CREATED         : 11/01/2005 
// DESCRIPTION			: This control is a container for FindPreferencesOptionsControl,PtJourneyChangesOptionsControl
// PtWalkingSpeedOptionsControl and FindViaLocationControl instances on 'Find a' and door to door pages.
// *************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/PTPreferencesControl.ascx.cs-arc  $
//
//   Rev 1.4   Mar 08 2013 11:18:54   mmodi
//Updates to hide the via location control
//Resolution for 5895: Error displayed when clicked on Advanced options button
//
//   Rev 1.3   Aug 28 2012 10:21:24   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.2   Mar 31 2008 13:22:32   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:08   mturner
//Initial revision.
//
//   Rev 1.10   Apr 05 2006 14:12:38   asinclair
//Fixed bug on VisitPlanner
//Resolution for 3777: DEL 8.1: Error on Visit Planner page when selecting 'Advanced options'
//
//   Rev 1.9   Apr 01 2006 17:21:08   asinclair
//Enabled the UpdateControls() method as it was needed for an IR fix
//Resolution for 3744: DN068 Extend: Problems with 'via' fields on Extend input page
//
//   Rev 1.8   Feb 23 2006 19:17:02   build
//Automatically merged from branch for stream3129
//
//   Rev 1.7.1.1   Jan 30 2006 14:41:18   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.7.1.0   Jan 10 2006 15:26:54   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.7   Nov 15 2005 09:55:58   asinclair
//IR fix for VP
//Resolution for 3000: Visit Planner: Login/register text appears when Advanced options switched on and off
//
//   Rev 1.6   Nov 08 2005 19:25:54   AViitanen
//Removing Hide details button
//
//   Rev 1.5   May 19 2005 12:29:34   ralavi
//Changes as the result of FXCop run and adding a new property newLocationClick so that when the new location for via location is clicked, via location remains visible.
//
//   Rev 1.4   May 06 2005 17:11:42   Ralavi
//Removed code that is not used.
//
//   Rev 1.3   Mar 24 2005 14:56:36   RAlavi
//Changes to ensure via maps function correctly
//
//   Rev 1.2   Mar 08 2005 09:35:28   PNorell
//new functionality for saving preferences.
//Resolution for 1958: DEV CODE Review: Preferences (Car and Public)
//
//   Rev 1.1   Feb 23 2005 16:34:32   RAlavi
//Changed for car costing
//
//   Rev 1.0   Jan 28 2005 18:56:06   ralavi
//Initial revision.
//
namespace TransportDirect.UserPortal.Web.Controls
{
    using System;
    using TransportDirect.Common;
    using TransportDirect.Common.ServiceDiscovery;
    using TransportDirect.UserPortal.DataServices;
    using TransportDirect.Web.Support;

    /// <summary>
	///		Summary description for PtPreferencesControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial  class PtPreferencesControl : TDUserControl
	{
		#region Controls

		protected FindPreferencesOptionsControl preferencesOptionsControl;
		protected PtJourneyChangesOptionsControl journeyChangesOptionsControl;
		protected PtWalkingSpeedOptionsControl walkingSpeedOptionsControl;
		protected FindViaLocationControl viaLocationControl;
		
		#endregion

		#region Constants/variables
        
		// Page level variables
		private IDataServices populator;
		
		private GenericDisplayMode changesDisplayMode = GenericDisplayMode.Normal;
		private GenericDisplayMode changesSpeedDisplayMode = GenericDisplayMode.Normal;
		
		private bool newLocationClicked;

        private bool showViaLocationControl = true; // Default to show
        private bool showLocationControlAutoSuggest = false;

		#endregion

		#region Page event handlers

		/// <summary>
		/// Handler for the Init event. Sets up global variables and additional event handlers.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Init(object sender, System.EventArgs e)
		{
			// Assign values to page level variables
			
			populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			resourceManager = Global.tdResourceManager;
			preferencesOptionsControl.HidePreferences += new EventHandler(this.OnHidePreferences);
			
            viaLocationControl.NewLocation += new EventHandler(OnNewLocation);
            locationControlVia.NewLocation += new EventHandler(OnNewLocation);
		}

        /// <summary>
        /// Sets visibility of controls according to the property values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            UpdateControls();
            UpdateChangesControl();
            bool showPreferences;

            if (AmbiguityMode)
            {
                showPreferences = (ViaLocationControl.Visible 
                                   || LocationControlVia.Visible
                                   || ptPreferencesPanel.Visible);
            }
            else
            {
                showPreferences = PreferencesVisible;
            }

            if (PageId == PageId.VisitPlannerInput)
            {
                preferencesOptionsControl.Visible = false;
            }
            else
            {
                ptPreferencesPanel.Visible = showPreferences;
            }
        }

        #endregion

		#region Event handlers

		// The following lines declare objects that can be used as 
		//keys in the EventHandlers table for the control.
		private static readonly object PreferencesVisibleChangedEventKey = new object();

		/// <summary>
		/// Occurs when the user clicks the show preferences or hide preferences buttons
		/// </summary>
		public event EventHandler PreferencesVisibleChanged
		{
			add { this.Events.AddHandler(PreferencesVisibleChangedEventKey, value); }
			remove { this.Events.RemoveHandler(PreferencesVisibleChangedEventKey, value); }
		}

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
		/// Handles the HidePreferences event of the FindPreferencesOptionsControl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnHidePreferences(object sender, EventArgs e)
		{
			PreferencesVisible = false;
		}

		/// <summary>
		/// Handles the event indicating that the new location button has been clicked
		/// on the nested via location control
		/// </summary>
		/// <param name="sender">event originator</param>
		/// <param name="e">event arguments</param>
		private void OnNewLocation(object sender, EventArgs e)
		{
			newLocationClicked = true;
		}
	
		#endregion

		#region Public properties

        /// <summary>
		/// Allows access to the FindPreferencesOptionsControl contained within
		/// this control. This is provided so that event handlers can be attached to
		/// the events that it raises.
		/// The following should be considered when using this property:
		/// <list type="bullet">
		///     <item><description>DO NOT handle the HidePreferences event in order to set the PreferencesVisible property of this control. This will be done internally, and the PreferencesVisibilityChanged event will be raised to indicate that this has happened.</description></item>
		///     <item><description>Take care when setting the AllowSavePreferences property. This is normally controlled by the AmbiguityMode property of this control. Changing it could cause the control to appear in an inconsistant state.</description></item>
		///     <item><description>Take care when setting the AllowHidePreferences property. This is normally controlled by the AmbiguityMode property of this control. Changing it could cause the control to appear in an inconsistant state.</description></item>
		/// </list>
		/// Read only.
		/// </summary>
		public FindPreferencesOptionsControl PreferencesOptionsControl
		{
			get { return preferencesOptionsControl; }
		}

		/// <summary>
		/// Allows access to the PtJourneyChangesOptionsControl contained within this control.
		/// </summary>
		public PtJourneyChangesOptionsControl JourneyChangesOptionsControl
		{
			get { return journeyChangesOptionsControl; }
		}

		/// <summary>
		/// Allows access to the PtWalkingSpeedOptionsControl contained within this control.
		/// </summary>
		public PtWalkingSpeedOptionsControl WalkingSpeedOptionsControl
		{
			get { return walkingSpeedOptionsControl; }
        }

        #region Via location

        /// <summary>
		/// Allows access to the FindViaLocationControl contained within this control.
		/// </summary>
		public FindViaLocationControl ViaLocationControl
		{
			get { return viaLocationControl; }
		}

        /// <summary>
        /// Allows access to the LocationControlVia (auto suggest version) contained within this control.
        /// </summary>
        public LocationControlVia LocationControlVia
        {
            get { return locationControlVia; }
        }

        /// <summary>
        /// Read/Write. Sets the visibility of the via location control
        /// </summary>
        public bool ShowViaLocationControl
        {
            get { return showViaLocationControl; }
            set { showViaLocationControl = value; }
        }

        /// <summary>
        /// Read/Write. Show the standard via location control (default)
        /// or the "auto-suggest" via location control
        /// </summary>
        public bool ShowLocationControlAutoSuggest
        {
            get { return showLocationControlAutoSuggest; }
            set { showLocationControlAutoSuggest = value;

                viaLocationControl.Visible = !showLocationControlAutoSuggest;
                locationControlVia.Visible = showLocationControlAutoSuggest;
            }
        }

        #endregion

        /// <summary>
		/// Allows access to the FindPageOptionsControl contained within this control.
		/// </summary>
		/*public FindPageOptionsControl PageOptionsControl
		{
			get { return pageOptionsControl; }
		}*/

		/// <summary>
		/// Controls whether or not the preferences panel is visible
		/// </summary>
		public bool PreferencesVisible
		{
			get { return ptPreferencesPanel.Visible; }
			set 
			{
				if (ptPreferencesPanel.Visible != value)
				{
					ptPreferencesPanel.Visible = value; 
					RaiseEvent(PreferencesVisibleChangedEventKey);
				}
			}
		}

		/// <summary>
		/// Returns true if the control is being displayed in ambiguity mode. This is determined
		/// from the values of <code>ChangesDisplayMode</code> and <code>ChangesSpeedDisplayMode</code>.
		/// </summary>
		public bool AmbiguityMode
		{
			get { return ((journeyChangesOptionsControl.ChangesDisplayMode != GenericDisplayMode.Normal) || (journeyChangesOptionsControl.ChangesSpeedDisplayMode != GenericDisplayMode.Normal)); }
		}

		/// <summary>
		/// Sets the mode for the Changes dropdown.
		/// Note that in this context, GenericDisplayMode.Ambiguity is treated
		/// the same way as GenericDisplayMode.Readonly.
		/// </summary>
		public GenericDisplayMode ChangesDisplayMode
		{
			get { return changesDisplayMode; }
			set { changesDisplayMode = value; }
		}

		/// <summary>
		/// Sets the mode for the Changes Speed dropdown.
		/// Note that in this context, GenericDisplayMode.Ambiguity is treated
		/// the same way as GenericDisplayMode.Readonly.
		/// </summary>
		public GenericDisplayMode ChangesSpeedDisplayMode
		{
			get { return changesSpeedDisplayMode; }
			set { changesSpeedDisplayMode = value; }
		}


		/// <summary>
		/// Returns true if the user is logged in and has elected to save their
		/// travel details
		/// Read only.
		/// </summary>
		public bool SavePreferences
		{
			get { return journeyChangesOptionsControl.SavePreferences; }
		}

		public bool NewLocationClicked
		{
			get { return newLocationClicked; }
		}

		#endregion

		#region Private methods

        /// <summary>
        /// Updates the state of nested controls with this object's property values
        /// </summary>
        private void UpdateControls()
        {
            // Don't show the via location control if in ambiguous mode and no location has been entered
            // However if in ambiguous mode and user clicks new location, this location will be empty and
            // we do need to make the control visible so use newLocationClicked to determine this case
            if (showLocationControlAutoSuggest)
            {
                if (AmbiguityMode
                    && locationControlVia.Search.InputText.Length == 0
                    && !newLocationClicked)
                    locationControlVia.Visible = false;
                else
                    locationControlVia.Visible = true;
            }
            else
            {
                if (AmbiguityMode 
                    && viaLocationControl.TheSearch.InputText.Length == 0 
                    && !newLocationClicked)
                    viaLocationControl.Visible = false;
                else
                    viaLocationControl.Visible = true;
            }

            // Show the appropriate via location control (&& itself in case it's visibility has been set above
            viaLocationControl.Visible = viaLocationControl.Visible && !showLocationControlAutoSuggest;
            locationControlVia.Visible = locationControlVia.Visible && showLocationControlAutoSuggest;

            // And set the override visibility if needed)
            if (!showViaLocationControl)
            {
                viaLocationControl.Visible = false;
                locationControlVia.Visible = false;
            }
        }

		/// <summary>
		/// Sets the state of the changes controls
		/// </summary>
		private void UpdateChangesControl()
		{
			if ((journeyChangesOptionsControl.PreferencesChanged) 
                || (walkingSpeedOptionsControl.PreferencesChanged) 
			    || (ViaLocationControl.Visible)
                || (LocationControlVia.Visible))
			{
				preferencesOptionsControl.Visible = true;
			}
			else
			{
				preferencesOptionsControl.Visible = false;
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

		}
		#endregion
	}
}