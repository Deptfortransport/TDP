// *********************************************** 
// NAME                 : PtJourneyChangesOptionsControl.ascx.cs 
// AUTHOR               : Reza Bamshad-Alavi
// DATE CREATED         : 24/01/2003
// DESCRIPTION          : Public Transport Preferences Control (?)
//
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/PtJourneyChangesOptionsControl.ascx.cs-arc  $
//
//   Rev 1.3   Apr 08 2008 13:50:44   apatel
//set pageoptionscontrol visibility 
//
//   Rev 1.2   Mar 31 2008 13:22:32   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:06   mturner
//Initial revision.
//
//   Rev 1.12   Apr 05 2006 15:17:50   mdambrine
//Manual merge from stream0030
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.11   Mar 31 2006 12:48:58   asinclair
//Removed a check to allow save preferences to be displayed if not in ambiguity mode
//Resolution for 3691: DN068 Extend: Remove option to save user preferences on Extend Input
//
//   Rev 1.10   Feb 23 2006 19:17:02   build
//Automatically merged from branch for stream3129
//
//   Rev 1.9.1.0   Jan 10 2006 15:26:50   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.9   Nov 15 2005 09:47:00   asinclair
//Removed unnecessary code
//Resolution for 3000: Visit Planner: Login/register text appears when Advanced options switched on and off
//
//   Rev 1.8   Nov 10 2005 17:49:30   asinclair
//Bug fix for VP
//
//   Rev 1.7   May 19 2005 12:25:36   ralavi
//Modifications as the result of FXCop modifications and ensuring that title "Public Transport Journey Details" is displayed in ambiguity page when changing values in Public Transport section.
//
//   Rev 1.6   Apr 26 2005 10:33:22   Ralavi
//Fixed Welsh for dropdowns.
//
//   Rev 1.5   Apr 06 2005 12:13:30   Ralavi
//Removing references to redundant labelChangesNote
//
//   Rev 1.4   Apr 04 2005 17:55:16   Ralavi
//Moved loading dropdown list from Page_load to after initilaiseComponent method
//
//   Rev 1.3   Mar 24 2005 14:52:10   RAlavi
//Changes to ensure via location for public transport in door to door is not visible in ambiguity page  when not changed.
//
//   Rev 1.2   Mar 08 2005 09:35:36   PNorell
//new functionality for saving preferences.
//Resolution for 1958: DEV CODE Review: Preferences (Car and Public)
//
//   Rev 1.1   Feb 23 2005 16:34:18   RAlavi
//Changed for car costing
//
//   Rev 1.0   Feb 01 2005 10:13:58   rgreenwood
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

	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.JourneyPlanning.CJPInterface;
	using TransportDirect.UserPortal.SessionManager;

	/// <summary>
	///		Summary description for PtJourneyChangesOptionsControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial  class PtJourneyChangesOptionsControl : TDUserControl
	{
		#region Controls

		protected TravelDetailsControl loginSaveOption;

		#endregion

		#region Constants/variables

		private const string ChangesHeaderKey = "PtJourneyChangesOptionsControl.ChangesHeader";
		private const string ChangesTitleKey = "PtJourneyChangesOptionsControl.ChangesTitle";
		private const string ChangeShowNoteKey = "PtJourneyChangesOptionsControl.ChangesShowNote";
		private const string ChangeSpeedTitleKey = "PtJourneyChangesOptionsControl.ChangesSpeedTitle";
		private const string ChangeSpeedNoteKey = "PtJourneyChangesOptionsControl.ChangesSpeedNote";

		private IDataServices ds;

		private GenericDisplayMode changesDisplayMode = GenericDisplayMode.Normal;
		private GenericDisplayMode changesSpeedDisplayMode = GenericDisplayMode.Normal;

		public bool changedPreferences;
		#endregion

		#region Public events

		// The following lines declare objects that can be used as 
		// keys in the EventHandlers table for the control.
		private static readonly object ChangesOptionChangedEventKey = new object();
		private static readonly object ChangeSpeedOptionChangedEventKey = new object();
		

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

		

		#endregion

		#region Properties



		/// <summary>
		/// Returns true if the control is being displayed in ambiguity mode. This is determined
		/// from the values of <code>ChangesDisplayMode</code> and <code>ChangesSpeedDisplayMode</code>.
		/// </summary>
		public bool AmbiguityMode
		{
			get { return ((ChangesDisplayMode != GenericDisplayMode.Normal) || (ChangesSpeedDisplayMode != GenericDisplayMode.Normal)); }
		}

		/// <summary>
		/// Sets the mode for the changes dropdown.
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
				string itemValue = ds.GetValue(DataServiceType.ChangesFindDrop, listChangesShow.SelectedItem.Value);
				return (PublicAlgorithmType)Enum.Parse(typeof(PublicAlgorithmType),itemValue);
			}
			set
			{
				string changesId = ds.GetResourceId(DataServiceType.ChangesFindDrop,Enum.GetName(typeof(PublicAlgorithmType), value));
				ds.Select(listChangesShow, changesId);
			}
		}

		/// <summary>
		/// The option selected for changes speed
		/// </summary>
		public int ChangesSpeed
		{
			get
			{
				string itemValue = ds.GetValue(DataServiceType.ChangesSpeedDrop,listChangesSpeed.SelectedItem.Value);
				return Convert.ToInt32(itemValue);
			}
			set
			{
				string speedId = ds.GetResourceId(DataServiceType.ChangesSpeedDrop,value.ToString());
				ds.Select(listChangesSpeed, speedId);
			}
		}

		/// <summary>
		/// Controls the visibility of the "save preferences" facility
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

		public bool PreferencesChanged
		{
			get 
			{
				if ((!ChangesIsDefault()) || (!ChangesSpeedIsDefault()))
				{
					changedPreferences = true;
				}
				else
				{
					changedPreferences = false;
				}
				return changedPreferences; 
			}
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
			//Assign values to page level variables
			ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];			
			
		}

		/// <summary>
		/// Handler for the Load event. Sets up the page.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			UpdateControls();

			// Load strings from the language file
			labelChanges.Text = GetResource(ChangesHeaderKey);
			//labelChangesNote.Text = GetResource(String.Format(ChangesNoteKey, this.PageId.ToString()));
			labelChangesShowTitle.Text = GetResource(ChangesTitleKey) + " ";
			labelChangesShowNote.Text = GetResource(ChangeShowNoteKey);
			labelChangesSpeedTitle.Text = GetResource(ChangeSpeedTitleKey)+ " ";
			labelChangesSpeedNote.Text = GetResource(ChangeSpeedNoteKey);

            //Load the dropdowns
            // setting the selected index of the dropdowns
            int changesShowIndex = listChangesShow.SelectedIndex;
            int changesSpeedIndex = listChangesSpeed.SelectedIndex;
            ds.LoadListControl(DataServiceType.ChangesFindDrop, listChangesShow);
            ds.LoadListControl(DataServiceType.ChangesSpeedDrop, listChangesSpeed);
            listChangesShow.SelectedIndex = changesShowIndex;
            listChangesSpeed.SelectedIndex = changesSpeedIndex;
		}


		/// <summary>
		/// Sets visibility of controls according to the property values
		/// </summary>
		/// <paramref name=" name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			if( !IsPostBack )
			{
				UpdateControls();
			}
			UpdateChangesControl();

			// Set state of the preferences control
			if (TDSessionManager.Current.Authenticated)
				loginSaveOption.LoggedInDisplay();
			else
				loginSaveOption.LoggedOutDisplay();



			
			if (AmbiguityMode)
			{
				this.AllowSavePreferences = false;
			
			}

		}
		#endregion

		#region Control event handlers

		/// <summary>
		/// Handles the changed event of the changes dropdown and raises
		/// the event on  to the client
		/// </summary>
		/// <param name=" name="sender"></param>
		/// <param name="e"></param>
		protected void OnChangesOptionChanged(object sender, EventArgs e)
		{
			RaiseEvent(ChangesOptionChangedEventKey);
		}

		/// <summary>
		/// Handles the Changed event of the change speed dropdown and raises the event on to the client
		/// </summary>
		/// <param name=" name="sender"></param>
		/// <param name="e"></param>
		protected void OnChangeSpeedOptionChanged(object sender, EventArgs e)
		{
			RaiseEvent(ChangeSpeedOptionChangedEventKey);
		}
		#endregion

			
		#region Helper methods

		/// <summary>
		/// Retrieves the delegate attached to an event handler from the Events 
		/// list and calls it.
		/// </summary>
		/// <param name="e"></param>
		private void RaiseEvent(object key)
		{
			EventHandler theDelegate = Events[key] as EventHandler;
			if (theDelegate != null)
				theDelegate(this, EventArgs.Empty);
		}

		/// <summary>
		/// Returns true if the chosen number of changes is the drop down default value, false otherwise
		/// </summary>
		/// <returns>true if the chosen number of changes is the drop down default value, false otherwise</returns>
		private bool ChangesIsDefault()
		{
			string defaultItemValue = ds.GetDefaultListControlValue(DataServiceType.ChangesFindDrop);
			PublicAlgorithmType defaultChangesValue = (PublicAlgorithmType)Enum.Parse(typeof(PublicAlgorithmType),defaultItemValue);
			return defaultChangesValue == Changes;
		}

		/// <summary>
		/// Returns true if the chosen change speed is the drop down default value, false otherwise
		/// </summary>
		///  <returns>true if the chosen change speed is the drop down default value, false otherwise</returns>
		private bool ChangesSpeedIsDefault()
		{
			string defaultItemValue = ds.GetDefaultListControlValue(DataServiceType.ChangesSpeedDrop);
			int defaultChangesSpeed = Convert.ToInt32(defaultItemValue);
			return defaultChangesSpeed == ChangesSpeed;
		}

		/// <summary>
		/// Sets the state of the changes controls
		/// </summary>
		private void UpdateChangesControl()
		{
			// Firstly, if we are in ambiguity mode then hide all of the notes
			//labelChangesNote.Visible = !AmbiguityMode;
			labelChangesShowNote.Visible = !AmbiguityMode;
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
							resourceManager.GetString(ChangesTitleKey, TDCultureInfo.CurrentUICulture) + ": " + listChangesShow.SelectedItem.Text;
					}
					labelChangesShowTitle.Visible = false;
					panelChanges.Visible = showChanges || showChangesSpeed;// || savePreferences;
					break;
				case GenericDisplayMode.Normal:
				default:
					listChangesShow.Visible = true;
					listChangesShowFixed.Visible = false;
					labelChangesShowTitle.Visible = true;
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
							resourceManager.GetString(ChangeSpeedTitleKey, TDCultureInfo.CurrentCulture) + ": " + listChangesSpeed.SelectedItem.Text;
					}
					labelChangesSpeedTitle.Visible = false;
					panelChanges.Visible = showChanges || showChangesSpeed;// || savePreferences;
					break;
				case GenericDisplayMode.Normal:
				default:
					listChangesSpeed.Visible = true;
					listChangesSpeedFixed.Visible = false;
					labelChangesSpeedTitle.Visible = true;
					panelChanges.Visible = true;
					break;
			}

			if (panelChanges.Visible) 	
			{
				changedPreferences = true;
			}
			else
			{
				changedPreferences = false;
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
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            listChangesShow.SelectedIndexChanged  += new EventHandler(this.OnChangesOptionChanged);
            listChangesSpeed.SelectedIndexChanged += new EventHandler(this.OnChangeSpeedOptionChanged);
		}
		#endregion
	}
}
