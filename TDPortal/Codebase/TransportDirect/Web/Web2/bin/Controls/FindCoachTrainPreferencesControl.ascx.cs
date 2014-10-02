// *********************************************** 
// NAME                 : FindCoachTrainPreferencesControl.ascx.cs
// AUTHOR               : Jonathan George
// DATE CREATED         : 12/07/2004
// DESCRIPTION  : Preferences for coach and train
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindCoachTrainPreferencesControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Apr 29 2008 16:19:44   mmodi
//Corrected dropdown list selection and via location being shown in ambiguity mode
//Resolution for 4903: Find a train - Advanced options don't work
//
//   Rev 1.2   Mar 31 2008 13:20:30   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Jan 30 2008 08:26:00 apatel
//  Changes made for moving pageOptionsControls inside boxes. removed PageOptionsControls from this user control and 
//  moved it to FindViaLocationsControl.
//
//   Rev 1.0   Nov 08 2007 13:14:04   mturner
//Initial revision.
//
//   Rev 1.17   Feb 23 2006 19:16:34   build
//Automatically merged from branch for stream3129
//
//   Rev 1.16.1.1   Jan 30 2006 14:41:06   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.16.1.0   Jan 10 2006 15:24:32   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.16   Nov 03 2005 17:04:00   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.15.1.1   Oct 12 2005 12:50:20   mtillett
//Updates to advanced options control to remove help and move hide button to single place in FindPageOptionsControl
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.15.1.0   Oct 07 2005 15:45:30   mtillett
//Remove help control from the advanced options controls
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.15   Mar 02 2005 15:25:14   RAlavi
//Changes for car costing
//
//   Rev 1.14   Jan 28 2005 19:34:44   ralavi
//Updated version for car costing
//
//   Rev 1.13   Oct 01 2004 11:03:46   COwczarek
//Make behaviour consistent for all Find A input pages when in ambiguity mode.
//Resolution for 1562: Find A input page in ambiguity mode always shows travel details
//
//   Rev 1.12   Sep 16 2004 09:26:20   rgeraghty
//Resolution for IR1531 - Updated Page_Load event to ensure correct resource language is used
//
//   Rev 1.11   Sep 01 2004 10:31:18   jmorrissey
//Added alt text. Also made helpIconChanges not visible when this control is Read Only. 
//
//   Rev 1.10   Aug 26 2004 16:46:34   COwczarek
//Changes to display journey preferences consistently in read only mode across all Find A pages
//Resolution for 1421: Find a ambiguity pages (QA)
//
//   Rev 1.9   Aug 25 2004 14:24:44   esevern
//preferences help labe more url now set on page load - fix for IR1299
//
//   Rev 1.8   Jul 23 2004 09:10:16   COwczarek
//Detect new location click on via location control so that it is made visible correctly in ambiguity mode
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.7   Jul 21 2004 15:15:36   jgeorge
//Hide via location when in ambiguity mode an no location specified
//
//   Rev 1.6   Jul 20 2004 17:18:50   jgeorge
//Removed property from ViewState
//
//   Rev 1.5   Jul 20 2004 15:24:50   jgeorge
//Removed properties from ViewState
//
//   Rev 1.4   Jul 20 2004 13:34:44   COwczarek
//Changes and ChangesSpeed Properties no longer update state of nested controls directly
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.3   Jul 19 2004 11:47:16   jgeorge
//Added via control
//
//   Rev 1.2   Jul 14 2004 12:47:46   jgeorge
//Updated after testing
//
//   Rev 1.1   Jul 13 2004 10:59:44   jgeorge
//Interim check-in
//
//   Rev 1.0   Jul 13 2004 10:53:02   jgeorge
//Initial revision.

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
	///	Preferences for coach and train.
	///	Notes:
	///	<list type="bullet">
	///		<item><description>When adding this control to a page, you should ensure that there are help labels on this control for that page.</description></item>
	///	</list>
	/// </summary>
	public partial  class FindCoachTrainPreferencesControl : TDUserControl
	{
		#region Controls


		// removed PageOptionsControl from this user control
		protected FindPreferencesOptionsControl preferencesOptionsControl;
		protected FindViaLocationControl viaLocationControl;
	

		#endregion

		#region Constants/variables

		// Keys used to obtain strings from the resource file
		private const string ChangesHeaderKey = "FindCoachTrainPreferencesControl.ChangesHeader";
		private const string ChangesTitleKey = "FindCoachTrainPreferencesControl.ChangesTitle";
		private const string ChangesNoteKey = "FindCoachTrainPreferencesControl.ChangesNote.{0}";
		private const string ChangeSpeedTitleKey = "FindCoachTrainPreferencesControl.ChangeSpeedTitle";
		private const string ChangeSpeedNoteKey = "FindCoachTrainPreferencesControl.ChangeSpeedNote";

		private const string ChangesHelpKey = "FindCoachTrainPreferencesControl.ChangesHelp.{0}";
		private const string ChangesHelpMoreKey = "FindCoachTrainPreferencesControl.ChangesHelp.{0}.moreUrl";
		private const string ChangesHelpAltTextKey = "FindCoachTrainPreferencesControl.ChangesHelpAltText.{0}";

		// Key used for help label assignment
		private const string ChangesHelpLabelBase = "ChangesHelp{0}";

		// Page level variables
		private TDResourceManager rm = null;
		private IDataServices ds = null;

		private GenericDisplayMode changesDisplayMode = GenericDisplayMode.Normal;
		private GenericDisplayMode changesSpeedDisplayMode = GenericDisplayMode.Normal;

        private int changesSpeed;
        private PublicAlgorithmType changes;
        private bool newLocationClicked = false;

		#endregion

		#region Public events

		// The following lines declare objects that can be used as
		// keys in the EventHandlers table for the control.
		private static readonly object ChangesOptionChangedEventKey = new object();
		private static readonly object ChangeSpeedOptionChangedEventKey = new object();
		private static readonly object PreferencesVisibleChangedEventKey = new object();

		/// <summary>
		/// Occurs when the selection is changed in the "Changes" dropdown
		/// </summary>
		public event EventHandler ChangesOptionChanged
		{
			add { this.Events.AddHandler(ChangesOptionChangedEventKey, value); }
			remove { this.Events.RemoveHandler(ChangesOptionChangedEventKey, value); }
		}

		/// <summary>
		/// Occurs when the selection is changed in the "Changes Speed" dropdown
		/// </summary>
		public event EventHandler ChangeSpeedOptionChanged
		{
			add { this.Events.AddHandler(ChangeSpeedOptionChangedEventKey, value); }
			remove { this.Events.RemoveHandler(ChangeSpeedOptionChangedEventKey, value); }
		}

		/// <summary>
		/// Occurs when the user clicks the show preferences or hide preferences buttons
		/// </summary>
		public event EventHandler PreferencesVisibleChanged
		{
			add { this.Events.AddHandler(PreferencesVisibleChangedEventKey, value); }
			remove { this.Events.RemoveHandler(PreferencesVisibleChangedEventKey, value); }
		}

		#endregion

		#region Properties

		/// <summary>
		/// Controls whether or not the preferences panel is visible
		/// </summary>
		public bool PreferencesVisible
		{
			get { return panelPreferences.Visible; }
			set 
			{
				if (panelPreferences.Visible != value)
				{
					panelPreferences.Visible = value; 
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
			get { return ((ChangesDisplayMode != GenericDisplayMode.Normal) || (ChangesSpeedDisplayMode != GenericDisplayMode.Normal)); }
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
		/// The option selected for changes
		/// </summary>
		public PublicAlgorithmType Changes
		{
            get 
            {
                return changes;
            }
			set
            {
                changes = value;
            }
		}

		/// <summary>
		/// The option selected for changes speed
		/// </summary>
		public int ChangesSpeed
		{
			get 
            { 
                return changesSpeed;
            }
			set
            {
                changesSpeed = value;
            }
		}

		/// <summary>
		/// Allows access to the FindPageOptionsControl contained within
		/// this control. This is provided so that event handlers can be
		/// attached to the events that it raises.
		/// The following should be considered when using this property:
		/// <list type="bullet">
		///     <item><description>DO NOT handle the ShowPreferences event in order to set the PreferencesVisible property of this control. This will be done internally, and the PreferencesVisibilityChanged event will be raised to indicate that this has happened.</description></item>
		///     <item><description>Take care when setting the AllowShowPreferences property. The visibility of this button is normally dependent on whether or not the preferences are visible, as well as the AmbiguityMode property of this control. Changing it could cause the control to appear in an inconsistant state.</description></item>
		///     <item><description>Take care when setting the AllowBack property. This is normally controlled by the AmbiguityMode property of this control. Changing it could cause the control to appear in an inconsistant state.</description></item>
		/// </list>
		/// Read only.
        /// White Labeling - Modified property to get FindViaLocationControl's PageOptionsControl user control as it moved to that page.
		/// </summary>
		public FindPageOptionsControl PageOptionsControl
		{
            get { return viaLocationControl.PageOptionsControl; }
		}

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
		/// Allows access to the FindViaLocationControl contained within this control.
		/// </summary>
		public FindViaLocationControl ViaLocationControl
		{
			get { return viaLocationControl; }
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
			rm = Global.tdResourceManager;

            //White Labeling - modified to handle FindViaLocationControl's PageOptionsControl events.
			viaLocationControl.PageOptionsControl.ShowAdvancedOptions += new EventHandler(this.OnShowPreferences);
            viaLocationControl.PageOptionsControl.HideAdvancedOptions += new EventHandler(this.OnHidePreferences);
            viaLocationControl.NewLocation += new EventHandler(OnNewLocation);
		}

		/// <summary>
		/// Handler for the Load event. Sets up the page.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
        {
            #region Load dropdowns
            // Load the dropdowns, retaining the seleced item through postbacks
            int listChangesShowSelectedIndex = listChangesShow.SelectedIndex;
            int listChangesSpeedSelectedIndex = listChangesSpeed.SelectedIndex;
			
            ds.LoadListControl(DataServiceType.ChangesFindDrop, listChangesShow);
			ds.LoadListControl(DataServiceType.ChangesSpeedDrop, listChangesSpeed);

            listChangesShow.SelectedIndex = listChangesShowSelectedIndex;
            listChangesSpeed.SelectedIndex = listChangesSpeedSelectedIndex;
            #endregion

            UpdateControls();

            #region Load text
            // Load strings from the languages file
			labelChanges.Text = rm.GetString(ChangesHeaderKey, TDCultureInfo.CurrentUICulture);
			labelChangesNote.Text = rm.GetString(String.Format(ChangesNoteKey, this.PageId.ToString()), TDCultureInfo.CurrentUICulture);
			labelChangesShowTitle.Text = rm.GetString(ChangesTitleKey, TDCultureInfo.CurrentUICulture) + " ";
			labelChangesSpeedTitle.Text = rm.GetString(ChangeSpeedTitleKey, TDCultureInfo.CurrentUICulture) + " ";
			labelChangesSpeedNote.Text = rm.GetString(ChangeSpeedNoteKey, TDCultureInfo.CurrentUICulture);
            #endregion
        }

		/// <summary>
		/// Sets visibility of controls according to the property values
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{

            UpdateControls();
            UpdatedDropdownControls();
            UpdateChangesControl();			

            bool showPreferences;

			if (AmbiguityMode) 
            {
                showPreferences = (ViaLocationControl.Visible || panelChanges.Visible);
            } 
            else 
            {
                showPreferences = PreferencesVisible;
            }

			panelPreferences.Visible = showPreferences;

            //White Labeling - modified to set FindViaLocationControl's properties.

            viaLocationControl.PageOptionsControl.AllowBack = AmbiguityMode;
            viaLocationControl.PageOptionsControl.AllowShowAdvancedOptions = !AmbiguityMode && !PreferencesVisible;

            if (showPreferences)
            {
                viaLocationControl.PageOptionsControl.AllowHideAdvancedOptions = !AmbiguityMode;
                preferencesOptionsControl.AllowSavePreferences = !AmbiguityMode;
			
            } 
			
		}

		#endregion

		#region Control event handlers

		/// <summary>
		/// Handles the Changed event of the changes dropdown and raises
		/// the event on to the client
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnChangesOptionChanged(object sender, EventArgs e)
		{
            string itemValue = ds.GetValue(DataServiceType.ChangesFindDrop, listChangesShow.SelectedItem.Value);
            changes = (PublicAlgorithmType)Enum.Parse(typeof(PublicAlgorithmType),itemValue);
            RaiseEvent(ChangesOptionChangedEventKey);
		}

		/// <summary>
		/// Handles the Changed event of the change speed dropdown and
		/// raises the event on to the client
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnChangeSpeedOptionChanged(object sender, EventArgs e)
		{
            string itemValue = ds.GetValue(DataServiceType.ChangesSpeedDrop,listChangesSpeed.SelectedItem.Value);
            changesSpeed = Convert.ToInt32(itemValue);
            RaiseEvent(ChangeSpeedOptionChangedEventKey);
		}

		/// <summary>
		/// Handles the ShowPreferences event of the FindPageOptionsControl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnShowPreferences(object sender, EventArgs e)
		{
			PreferencesVisible = true;
           
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
        /// Returns true if the choosen number of changes is the drop down default value, false otherwise
        /// </summary>
        /// <returns>true if the choosen number of changes is the drop down default value, false otherwise</returns>
        private bool ChangesIsDefault() 
        {
            string defaultItemValue = ds.GetDefaultListControlValue(DataServiceType.ChangesFindDrop);
            PublicAlgorithmType defaultChangesValue = (PublicAlgorithmType)Enum.Parse(typeof(PublicAlgorithmType),defaultItemValue);
            return defaultChangesValue == changes;
        }

        /// <summary>
        /// Returns true if the choosen change speed is the drop down default value, false otherwise
        /// </summary>
        /// <returns>true if the choosen change speed is the drop down default value, false otherwise</returns>
        private bool ChangesSpeedIsDefault()
        {
            string defaultItemValue = ds.GetDefaultListControlValue(DataServiceType.ChangesSpeedDrop);
            int defaultChangesSpeed = Convert.ToInt32(defaultItemValue);
            return defaultChangesSpeed == changesSpeed;
        }

		/// <summary>
		/// Sets the state of the changes controls
		/// </summary>
		private void UpdateChangesControl()
		{
			// Firstly, if we are in ambiguity mode then hide all of the notes
			labelChangesNote.Visible = !AmbiguityMode;
			labelChangesSpeedNote.Visible = !AmbiguityMode;
            bool showChanges = !ChangesIsDefault();
            bool showChangesSpeed = !ChangesSpeedIsDefault();
		
			switch (ChangesDisplayMode)
			{
                case GenericDisplayMode.ReadOnly:
                case GenericDisplayMode.Ambiguity:
					listChangesShow.Visible = false;
					listChangesShowFixed.Visible = showChanges;
                    if (showChanges) 
                    {
                        listChangesShowFixed.Text = 
                            rm.GetString(ChangesTitleKey, TDCultureInfo.CurrentCulture) + ": " + listChangesShow.SelectedItem.Text;
                    }
					labelChangesShowTitle.Visible = false;
                    panelChanges.Visible = showChanges || showChangesSpeed;
					break;				
                case GenericDisplayMode.Normal:		
				default:
					listChangesShow.Visible = true;
					listChangesShowFixed.Visible = false;
                    labelChangesShowTitle.Visible = true;
                    panelChanges.Visible = true;
                    break;
			}

			switch (ChangesSpeedDisplayMode)
			{
				case GenericDisplayMode.ReadOnly:
				case GenericDisplayMode.Ambiguity:
					listChangesSpeed.Visible = false;
					listChangesSpeedFixed.Visible = showChangesSpeed;
                    if (showChangesSpeed) 
                    {
                        listChangesSpeedFixed.Text =
                            rm.GetString(ChangeSpeedTitleKey, TDCultureInfo.CurrentCulture) + ": " + listChangesSpeed.SelectedItem.Text;
                    }
                    labelChangesSpeedTitle.Visible = false;
                    panelChanges.Visible = showChanges || showChangesSpeed;
                    break;
				case GenericDisplayMode.Normal:
				default:
					listChangesSpeed.Visible = true;
					listChangesSpeedFixed.Visible = false;
                    labelChangesSpeedTitle.Visible = true;
                    panelChanges.Visible = true;
                    break;
			}
		}

        /// <summary>
        /// Updates the state of nested controls with this object's property values
        /// </summary>
        private void UpdateControls() 
        {
            // Don't show the via location control if in ambiguous mode and no location has been entered
            // However if in ambiguous mode and user clicks new location, this location will be empty and
            // we do need to make the control visible so use newLocationClicked to determine this case
            if (AmbiguityMode && viaLocationControl.TheSearch.InputText.Length == 0 && !newLocationClicked)
                viaLocationControl.Visible = false;
            else
            {
                viaLocationControl.Visible = true;

                try // Place in try for sanity. Shouldn't get error when casting location object
                {
                    // if the viaLocation is visible, and there is no search text entered, ensure it is in its default state
                    if (viaLocationControl.TristateLocationControl.Status == TransportDirect.UserPortal.LocationService.TDLocationStatus.Unspecified)
                    {
                        LocationSelectControl2 locationControl = (LocationSelectControl2)viaLocationControl.TristateLocationControl.LocationControl;

                        if ((viaLocationControl.TheSearch.InputText.Length == 0) &&
                            (string.IsNullOrEmpty(locationControl.InputText)))
                        {
                            viaLocationControl.SetLocationUnspecified();
                        }
                    }
                }
                catch
                {
                    // No need to do anthing, if we can't get the correct location object then there must be a location, 
                    // so keep it!
                }
            }
        }

        /// <summary>
        /// Updates the state of the dropdown controls with this object's property values
        /// </summary>
        private void UpdatedDropdownControls()
        {
            string speedId = ds.GetResourceId(DataServiceType.ChangesSpeedDrop,changesSpeed.ToString());                
            ds.Select(listChangesSpeed, speedId);

            string changesId = ds.GetResourceId(DataServiceType.ChangesFindDrop,Enum.GetName(typeof(PublicAlgorithmType), changes));
            ds.Select(listChangesShow, changesId); 
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
            this.listChangesShow.SelectedIndexChanged += new EventHandler(this.OnChangesOptionChanged);
            this.listChangesSpeed.SelectedIndexChanged += new EventHandler(this.OnChangeSpeedOptionChanged);
		}

		#endregion
	}
}
