// *************************************************************************** 
// NAME                 : D2DPTPreferencesControl.ascx
// AUTHOR               : David Lane
// DATE CREATED         : 09/01/2013 
// DESCRIPTION			: This control is a container for FindPreferencesOptionsControl,PtJourneyChangesOptionsControl
// PtWalkingSpeedOptionsControl and LocationControlVia instances on the door to door page.
// *************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/D2DPTPreferencesControl.ascx.cs-arc  $
//
//   Rev 1.3   Jan 30 2013 13:47:46   mmodi
//Fixed showing advanced options on ambiguity page
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.1   Jan 17 2013 09:46:00   mmodi
//Updates to D2D advanced options for better js and non-js behaviour
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Jan 10 2013 16:34:04   dlane
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//

namespace TransportDirect.UserPortal.Web.Controls
{
    using System;
    using TransportDirect.Common;
    using TransportDirect.Common.ServiceDiscovery;
    using TransportDirect.UserPortal.DataServices;
    using TransportDirect.Web.Support;
    using TransportDirect.UserPortal.LocationService;

    /// <summary>
	///		Summary description for D2DPTPreferencesControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial  class D2DPTPreferencesControl : TDUserControl
	{
		#region Controls

		protected D2DPtJourneyChangesOptionsControl journeyChangesOptionsControl;
		protected D2DPtWalkingSpeedOptionsControl walkingSpeedOptionsControl;
		
		#endregion

		#region Constants/variables
        
		// Page level variables
		private IDataServices populator;
		
		private GenericDisplayMode changesDisplayMode = GenericDisplayMode.Normal;
		private GenericDisplayMode changesSpeedDisplayMode = GenericDisplayMode.Normal;
		
		private bool newLocationClicked;

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
            bool showPreferences;

            if (AmbiguityMode)
            {
                showPreferences = (LocationControlVia.Visible
                                   || ptPreferencesPanel.Visible);
            }
            else
            {
                showPreferences = PreferencesVisible;
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
		/// Allows access to the PtJourneyChangesOptionsControl contained within this control.
		/// </summary>
		public D2DPtJourneyChangesOptionsControl JourneyChangesOptionsControl
		{
			get { return journeyChangesOptionsControl; }
		}

		/// <summary>
		/// Allows access to the PtWalkingSpeedOptionsControl contained within this control.
		/// </summary>
		public D2DPtWalkingSpeedOptionsControl WalkingSpeedOptionsControl
		{
			get { return walkingSpeedOptionsControl; }
		}

        /// <summary>
        /// Allows access to the LocationControlVia (auto suggest version) contained within this control.
        /// </summary>
        public D2DLocationControlVia LocationControlVia
        {
            get { return locationControlVia; }
        }

        /// <summary>
        /// Read/Write. Show the standard via location control (default)
        /// or the "auto-suggest" via location control
        /// </summary>
        public bool ShowLocationControlAutoSuggest
        {
            get { return showLocationControlAutoSuggest; }
            set { showLocationControlAutoSuggest = value;

                locationControlVia.Visible = showLocationControlAutoSuggest;
            }
        }
        		
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

        /// <summary>
        /// Read only. Returns true if non-default options have been selected
        /// </summary>
        public bool IsOptionSelected
        {
            get
            {
                return journeyChangesOptionsControl.IsOptionSelected
                       || walkingSpeedOptionsControl.IsOptionSelected
                       || (locationControlVia.Location != null && 
                           (locationControlVia.Location.Status == TDLocationStatus.Valid ||
                           locationControlVia.Location.Status == TDLocationStatus.Ambiguous));
            }
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
            
            // Show the appropriate via location control (&& itself in case it's visibility has been set above
            locationControlVia.Visible = locationControlVia.Visible && showLocationControlAutoSuggest;
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