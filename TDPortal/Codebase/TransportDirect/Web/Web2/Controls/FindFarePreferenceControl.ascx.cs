// *********************************************** 
// NAME                 : FindFarePreferenceControl.ascx.cs
// AUTHOR               : Tim Mollart
// DATE CREATED         : 07/01/2005
// DESCRIPTION			: Preferences for Find a Fare
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindFarePreferenceControl.ascx.cs-arc  $
//
//   Rev 1.3   May 01 2008 11:42:04   mmodi
//Fixed bug with rail card not being remembered on find cheap rail fare screen.
//Resolution for 4918: Discount rail card setting not maintained between pages.
//
//   Rev 1.2   Mar 31 2008 13:20:34   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:14:10   mturner
//Initial revision.
//
//   Rev 1.14   Nov 14 2006 10:01:04   rbroddle
//Merge for stream4220
//
//   Rev 1.13.1.1   Nov 12 2006 15:05:34   dsawe
//added code for travelDetailsControl to be invisible
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.13.1.0   Nov 07 2006 11:36:54   tmollart
//Added property to surpress the appearance of the coach discount option.
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.13   Feb 23 2006 16:11:04   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.12   Dec 09 2005 13:19:54   rhopkins
//Corrected text case of class names, so that they are interpretted correctly by all browsers.
//Resolution for 2988: UEE Post Build Issue: Large text on Find Fare Advance Option
//
//   Rev 1.11   Nov 03 2005 16:11:16   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.10.1.1   Oct 12 2005 12:50:20   mtillett
//Updates to advanced options control to remove help and move hide button to single place in FindPageOptionsControl
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.10.1.0   Oct 07 2005 15:45:30   mtillett
//Remove help control from the advanced options controls
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.10   Apr 18 2005 11:47:44   tmollart
//Added code so that when in ambiguity (read only) mode the save travel details check box is not visible.
//Resolution for 2159: Find a fare save preferences check-box
//
//   Rev 1.9   Apr 15 2005 11:45:56   tmollart
//Modified how visibilty of preferences is controlled.
//Resolution for 2090: PT: Layout issues on Find Fare Input
//
//   Rev 1.8   Mar 01 2005 18:23:30   tmollart
//Fixed bug so panel is displayed correctly in read only mode.
//
//   Rev 1.7   Feb 25 2005 10:38:36   tmollart
//Added Screen Reader labels.
//Modified lang string references for consistency.
//Modified to use local resource manager for populaton of label controls.
//Changed adult child drop radio list to use new relevant data type.
//
//   Rev 1.6   Feb 15 2005 16:17:24   tmollart
//Changed the adult/child fares selection drop down to radio buttons.
//
//   Rev 1.5   Feb 11 2005 10:02:16   tmollart
//Added event to fire when Adult/Child drop changes. Also updated adult/child drop so that it works correctly.
//
//   Rev 1.4   Jan 28 2005 18:45:04   ralavi
//Updated for car costing
//
//   Rev 1.3   Jan 12 2005 14:57:22   tmollart
//Del 7 - Work in progress.
//
//   Rev 1.2   Jan 11 2005 13:33:56   tmollart
//Work in progress.
//
//   Rev 1.1   Jan 07 2005 10:29:06   tmollart
//Work in progress.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;	
	using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.Web.Support;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.DataServices;

	/// <summary>
	///	FindFarePreferenceControl. Contains controls for selecting journey
	///	parameters for use with Find A Fare functionality. Enables used to select
	///	discount cards and adult/child fares. Contains an ambiguity mode that
	///	makes the controls read only. In this scenario a label is shown instead of the
	///	selected drop down value.
	/// </summary>
	public partial  class FindFarePreferenceControl : TDUserControl
	{

		


		protected FindPreferencesOptionsControl preferencesOptionsControl;
		protected TransportDirect.UserPortal.Web.Controls.FindPageOptionsControl pageOptionsControl;
		protected TransportDirect.UserPortal.Web.Controls.TravelDetailsControl travelDetailsControl;

		#region Variables/Constants
		private bool ambiguityMode;
		private string adultString = "AdultFares";
		private string childString = "ChildFares";
		private bool overrideCoachVisibility;
		private bool travelDetailsVisible = true; //added new property for turning off or on traveldetailscontrol --dsawe

		//Constants for resouce management keys.
		private const string FIND_A_FARE_RM = "FindAFare";
		private const string FareDetailsLabelKey = "FindFare.PreferencesControl.FareDetailsLabel";
		private const string DiscountCardsLabelKey = "FindFare.PreferencesControl.DiscountCardsLabel";
		private const string RailCardLabelKey = "FindFare.PreferencesControl.RailCardLabel";
		private const string CoachCardLabelKey = "FindFare.PreferencesControl.CoachCardLabel";
		private const string AdultChildLabelKey = "FindFare.PreferencesControl.AdultChildLabel";
		private const string ShowLabelKey = "FindFare.PreferencesControl.ShowLabel";
		private const string FaresLabelKey = "FindFare.PreferencesControl.FaresLabel";

		//Help Control
		private const string HelpTextKey = "FindFare.PreferencesControl.HelpText";
		private const string HelpIconAltTextKey = "FindFare.PreferencesControl.HelpIcon.AltText";

		//Screen reader
		private const string RailCardSRKey = "FindFare.PreferenceControl.RailCardSRLabel";
		private const string CoachCardSRKey = "FindFare.PreferenceControl.CoachCardSRLabel";
		private const string AdultChildSRKey = "FindFare.PreferenceControl.AdultChildSRLabel";

		#endregion

		#region Public Events

		public event EventHandler DiscountRailCardChanged;
		public event EventHandler DiscountCoachCardChanged;
		public event EventHandler AdultChildChanged;
		//public event EventHandler PreferencesVisibleChanged;
        private static readonly object PreferencesVisibleChangedEventKey = new object();

        /// <summary>
        /// Occurs when the user clicks the show preferences or hide preferences buttons
        /// </summary>
        public event EventHandler PreferencesVisibleChanged
        {
            add { this.Events.AddHandler(PreferencesVisibleChangedEventKey, value); }
            remove { this.Events.RemoveHandler(PreferencesVisibleChangedEventKey, value); }
        }

		#endregion

		#region Constructor
		public FindFarePreferenceControl()
		{
			LocalResourceManager = TDResourceManager.FIND_A_FARE_RM;
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

		}
		#endregion

		#region Page Load Init & PreRender
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //Make a back up of control values, since they will be lost during 
            //entry population:
            int coachCardDropListSelectedIndex = coachCardDropList.SelectedIndex;
            int railCardDropListSelectedIndex = railCardDropList.SelectedIndex;
            int adultChildRadioListSelectedIndex = adultChildRadioList.SelectedIndex;

            //Initialise the drop down controls if the page is not a post back.
            initialiseStaticControls();

            //Restore combobox values:
            coachCardDropList.SelectedIndex = coachCardDropListSelectedIndex;
            railCardDropList.SelectedIndex = railCardDropListSelectedIndex;
            adultChildRadioList.SelectedIndex = adultChildRadioListSelectedIndex;

			//General labels etc.
			fareDetailsLabel.Text = GetResource(FareDetailsLabelKey);
			discountCardsLabel.Text = GetResource(DiscountCardsLabelKey);
			railCardLabel.Text = GetResource(RailCardLabelKey);
			coachCardLabel.Text = GetResource(CoachCardLabelKey);
			adultChildLabel.Text = GetResource(AdultChildLabelKey);
			showLabel.Text = GetResource(ShowLabelKey);
			pageOptionsControl.AllowShowAdvancedOptions = true;

			//Screen reader controls.
			railCardSRLabel.Text = GetResource(RailCardSRKey);
			coachCardSRLabel.Text = GetResource(CoachCardSRKey);
			adultChildSRLabel.Text = GetResource(AdultChildSRKey);
		}

		/// <summary>
		/// Initialised custom event handlers.
		/// </summary>
		protected void Page_Init(object sender, System.EventArgs e)
		{
			this.pageOptionsControl.ShowAdvancedOptions += new System.EventHandler(this.ShowPreferences_Clicked);
			//this.pageOptionsControl.HideAdvancedOptions += new System.EventHandler(this.HidePreferences_Clicked);

			//Control user options changes.
			this.railCardDropList.SelectedIndexChanged += new System.EventHandler(this.railCardDropList_SelectedIndexChanged);
			this.coachCardDropList.SelectedIndexChanged += new System.EventHandler(this.coachCardDropList_SelectedIndexChanged);
			this.adultChildRadioList.SelectedIndexChanged += new System.EventHandler(this.adultChildRadioList_SelectedIndexChanged);
		}

		protected void Page_PreRender(object sender, System.EventArgs e)
		{

			bool showPreferences;

			//Initialise hte dynamic controls on the page.
			initialiseDynamicControls();

			//Control visibility of the control.
			if (ambiguityMode)
			{
				//Show preferences if the preferences panel is visible.
				showPreferences = preferencesPanel.Visible;
			}
			else
			{
				showPreferences = preferencesPanel.Visible;
			}

			//Back button will be available if in ambiguity mode;
			pageOptionsControl.AllowBack = ambiguityMode;
			if(!ambiguityMode && travelDetailsVisible)
			{
				travelDetailsControl.Visible = travelDetailsVisible;
			}
			
			pageOptionsControl.AllowShowAdvancedOptions = !AmbiguityMode && !PreferencesVisible;
			
			if (showPreferences)
			{
				pageOptionsControl.AllowHideAdvancedOptions = !AmbiguityMode;	
			} 

			// Coach visibility overrride
			if ( overrideCoachVisibility )
			{
				coachCardLabel.Visible = false;
				coachCardDropList.Visible = false;
			}
		}

		#endregion

		#region Public Properties

		// Property for setting travelDetailsControl visible 
		public bool TravelDetailsVisible
		{
			set 
			{
				travelDetailsVisible = value;
			}
		}

		/// <summary>
		/// Read/Write. Sets and gets the visibility of the panel containing
		/// the preferences control.
		/// </summary>
		public bool PreferencesVisible
		{
			get {return preferencesPanel.Visible;}
			set
			{
                if (preferencesPanel.Visible != value)
                {
                    preferencesPanel.Visible = value;
                    RaiseEvent(PreferencesVisibleChangedEventKey);
                }
			}
		}

		/// <summary>
		/// Read/Write. Sets and gets visiblity of the coach options.
		/// </summary>
		public bool OverrideCoachOptionsToInvisible
		{
			get { return overrideCoachVisibility; }
			set { overrideCoachVisibility = value; }
		}


		/// <summary>
		/// Read/Write. Sets and gets the value of the ambiguity mode.
		/// When set to TRUE values of the control displayed as read only labels. When set to
		/// FALSE values available for selection in drop down lists.
		/// </summary>
		public bool AmbiguityMode
		{
			get {return ambiguityMode;}
			set {ambiguityMode = value;}
		}

		/// <summary>
		/// Read/Write. Sets and gets the value of the discount
		/// rail card drop down list. Note uses the internal value and not textual value
		/// of the control.
		/// </summary>
		public string DiscountRailCard
		{
			get 
			{
				IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
				return ds.GetValue(DataServiceType.DiscountRailCardDrop, railCardDropList.SelectedItem.Value);
			}
			set
			{
				IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
				ds.Select(railCardDropList, ds.GetResourceId(DataServiceType.DiscountRailCardDrop, value));
			}
		}

		/// <summary>
		/// Read/Write. Sets and gets the value of the discount
		/// coach card drop down list. Note uses internal value and not textual value of
		/// the control.
		/// </summary>
		public string DiscountCoachCard
		{
			get 
			{
				IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
				return ds.GetValue(DataServiceType.DiscountCoachCardDrop, coachCardDropList.SelectedItem.Value);
			}
			set
			{
				IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
				ds.Select(coachCardDropList, ds.GetResourceId(DataServiceType.DiscountCoachCardDrop, value));
			}
		}
		/// <summary>
		/// Read/Write. Returns TRUE if adult is selected or FALSE if
		/// child is selected.
		/// </summary>
		public bool AdultChild
		{
			get
			{
				//Return true if the selected items value is the same as adult string.
				return (adultChildRadioList.SelectedItem.Value == adultString);
			}
			set
			{
				//If value is true select item identitfied by item adult
				//otherwise use item identified by child.
				if (value == true)
				{
					IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
					ds.Select(adultChildRadioList, adultString);
				}
				else
				{
					IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
					ds.Select(adultChildRadioList, childString);
				}
			}
		}

		/// <summary>
		/// Read only. Returns the FindPreferences options control contained
		/// within the FindFarePreferenceControl.
		/// NOTE: The visibility of the this control is handled internally and
		/// there is no need to handle the event. The preferencesVisible_Changed
		/// will be raised in the event of a change in visibility.
		/// </summary>
		public FindPreferencesOptionsControl PreferencesOptionsControl
		{
			get {return preferencesOptionsControl;}
		}

		/// <summary>
		/// Read only. Returns the contained travelDetailsControl.
		/// </summary>
		public TravelDetailsControl PreferencesTravelDetailControl
		{
			get {return travelDetailsControl;}
		}

		/// <summary>
		/// Read only. Returns the contained pageOptionsControl.
		/// </summary>
		public FindPageOptionsControl PreferencesPageOptionsControl
		{
			get {return pageOptionsControl;}
		}

		#endregion Public Properties

		#region Private Methods

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
		/// Initialises that static controls on the page. This includes populating
		/// dropdowns.
		/// </summary>
		private void initialiseStaticControls()
		{
			IDataServices populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			
			populator.LoadListControl(DataServiceType.DiscountCoachCardDrop, coachCardDropList);
			populator.LoadListControl(DataServiceType.DiscountRailCardDrop, railCardDropList);
			populator.LoadListControl(DataServiceType.AdultChildRadioList, adultChildRadioList);
		}

		/// <summary>
		/// Initalise the dynamic controls on the page such as text labels and visibility
		/// of controls dependant on ambiguity mode.
		/// </summary>
		private void initialiseDynamicControls()
		{
			//Preset areas to visible.
			RailCardRow.Visible = true;
			CoachCardRow.Visible = true;
			adultChildPanel.Visible = true;

			//Visible when in ambiguity mode as user cannot
			//change the selection.
			selectedRailCardLabel.Visible = ambiguityMode;
			selectedCoachCardLabel.Visible = ambiguityMode;
			selectedAdultChildLabel.Visible = ambiguityMode;

			//Screen reader labels not to be visibile in ambiguity
			//as there is no user selection available.
			railCardSRLabel.Visible = !ambiguityMode;
			coachCardSRLabel.Visible = !ambiguityMode;
			adultChildSRLabel.Visible = !ambiguityMode;

			//Drop list should only be visible when not in ambiguity
			//mode to allow user selection.
			railCardDropList.Visible = !ambiguityMode;
			coachCardDropList.Visible = !ambiguityMode;
			adultChildRadioList.Visible = !ambiguityMode;

			//Set discount card and adult child *ambiguity mode* labels to
			//be the same as the values of the drop down boxes.
			selectedRailCardLabel.Text = railCardDropList.SelectedItem.Text;
			selectedCoachCardLabel.Text = coachCardDropList.SelectedItem.Text;

			//Set visibility of rail card controls. This only needs to be done
			//if we are in ambiguity mode
			if (ambiguityMode)
			{
				RailCardRow.Visible = (railCardDropList.SelectedIndex != 0);
				CoachCardRow.Visible = (coachCardDropList.SelectedIndex != 0);
				DiscountCardsRow.Visible = (RailCardRow.Visible || CoachCardRow.Visible);
				adultChildPanel.Visible = (adultChildRadioList.SelectedIndex != 0);			 

				selectedAdultChildLabel.Text = adultChildRadioList.SelectedItem.Text;

				//Set cssClass of controls dependant on ambiguity mode.
				railCardLabel.CssClass = "txtsevenb";
				coachCardLabel.CssClass = railCardLabel.CssClass;
				showLabel.CssClass = railCardLabel.CssClass;

				//Set overall visibility
				preferencesPanel.Visible = (railCardDropList.SelectedIndex != 0) || (coachCardDropList.SelectedIndex != 0) || (adultChildRadioList.SelectedIndex != 0);
			}
			else
			{
				//Set cssClass of controls dependant on ambiguity mode.
				railCardLabel.CssClass = "txtseven";
				coachCardLabel.CssClass = railCardLabel.CssClass;
				showLabel.CssClass = railCardLabel.CssClass;
			}
		}

		#endregion Private Methods

		#region Event Handlers

		private void railCardDropList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (DiscountRailCardChanged != null)
				DiscountRailCardChanged(this, e);
		}

		private void coachCardDropList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (DiscountCoachCardChanged != null)
				DiscountCoachCardChanged(this,e);
		}

		private void adultChildRadioList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (AdultChildChanged != null)
				AdultChildChanged(this,e);
		}

        //private void OnPreferencesVisibleChanged(System.EventArgs e)
        //{
        //    if (PreferencesVisibleChanged != null)
        //        PreferencesVisibleChanged(this, System.EventArgs.Empty);
        //}

		private void HidePreferences_Clicked(object sender, System.EventArgs e)
		{
			PreferencesVisible = false;
			//OnPreferencesVisibleChanged(System.EventArgs.Empty);
		}

		private void ShowPreferences_Clicked(object sender, System.EventArgs e)
		{
			PreferencesVisible = true;
			//OnPreferencesVisibleChanged(System.EventArgs.Empty);
		}

		#endregion
	}
}