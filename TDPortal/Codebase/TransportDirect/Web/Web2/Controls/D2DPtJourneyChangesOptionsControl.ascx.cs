// *********************************************** 
// NAME                 : D2DPtJourneyChangesOptionsControl.ascx.cs 
// AUTHOR               : David Lane
// DATE CREATED         : 09/01/2013
// DESCRIPTION          : Public Transport Preferences Control 
//
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/D2DPtJourneyChangesOptionsControl.ascx.cs-arc  $
//
//   Rev 1.4   Jan 30 2013 13:47:42   mmodi
//Fixed showing advanced options on ambiguity page
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.1   Jan 17 2013 09:45:58   mmodi
//Updates to D2D advanced options for better js and non-js behaviour
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Jan 10 2013 16:34:02   DLane
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//

using System;
using TransportDirect.Common.ResourceManager;
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

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///	Summary description for D2DPtJourneyChangesOptionsControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial  class D2DPtJourneyChangesOptionsControl : TDUserControl
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

        /// <summary>
        /// Read only. Returns true if non-default options have been selected
        /// </summary>
        public bool IsOptionSelected
        {
            get { return !ChangesIsDefault() || !ChangesSpeedIsDefault(); }
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

            labelJsQuestion.Text = GetResource("ChangesControl.Question");
            labelOptionsSelected.Text = GetResource("ChangesControl.OptionsSelected");

            btnShow.Text = GetResource("AdvancedOptions.Show.Text");
        }
        
		/// <summary>
		/// Sets visibility of controls according to the property values
		/// </summary>
		/// <paramref name=" name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
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
