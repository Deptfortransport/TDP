#region History
// *********************************************** 
// NAME                 : D2DCarPreferencesControl.ascx.cs 
// AUTHOR               : David Lane 
// DATE CREATED         : 09/01/2013 
// DESCRIPTION          : Displays /allows input of 
//                        car specific preferences   
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/D2DCarPreferencesControl.ascx.cs-arc  $ 
//
//   Rev 1.4   Jan 30 2013 13:47:24   mmodi
//Fixed showing advanced options on ambiguity page
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.1   Jan 17 2013 09:45:46   mmodi
//Updates to D2D advanced options for better js and non-js behaviour
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Jan 10 2013 16:33:50   dlane
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
#endregion 

using System;
using TransportDirect.Common.ResourceManager;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Web.Support;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///	Preferences for find a car journeys.
	///	Notes:
	///	<list type="bullet">
	///	<item><description>When adding this control to a page, you should 
	///	ensure that there are help labels on this control for that page.
	///	</description></item>
	///	</list>
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial  class D2DCarPreferencesControl : TDUserControl
    {
        #region Private members

        #region controls

        protected D2DPreferencesOptionsControl preferencesOptionsControl;
		protected D2DCarJourneyOptionsControl journeyOptionsControl;
		protected TravelDetailsControl loginSaveOption;
	
		protected System.Web.UI.WebControls.Label sizeLabel;
        
		#endregion
	
		#region Constants
		// Keys used to obtain strings from the resource file
        private const string JourneyTypeKey = "FindCarPreferencesControl.JourneyTypeHeader";
        private const string FindKey = "FindCarPreferencesControl.CarFind";
        private const string JourneysKey = "FindCarPreferencesControl.Journeys";
        private const string CarSpeedKey = "FindCarPreferencesControl.CarSpeed";
        private const string MotorwayKey = "FindCarPreferencesControl.DoNotUseMotorways";
        private const string CarDetailsKey = "FindCarPreferencesControl.CarDetailsHeader";
        private const string IHaveaKey = "FindCarPreferencesControl.IHaveA";
        private const string SizedKey = "FindCarPreferencesControl.Sized";
        private const string CarKey = "FindCarPreferencesControl.Car";
        private const string FormatCarTypeSentenceKey = "FindCarPreferencesControl.FormatCarTypeSentence";
        private const string FuelConsumptionKey = "FindCarPreferencesControl.FuelConsumption";
        private const string AverageForCarKey = "FindCarPreferencesControl.AverageForMyCar";
        private const string SpecificConsumptionKey = "FindCarPreferencesControl.SpecificConsumption";
        private const string FuelCostKey = "FindCarPreferencesControl.FuelCost";
        private const string DisplayFuelCostKey = "FindCarPreferencesControl.DisplayFuelCost";
        private const string SpecificFuelCostKey = "FindCarPreferencesControl.SpecificFuelCost";
        private const string PencePerLitreKey = "FindCarPreferencesControl.PencePerLitre";
        private const string FuelConsumptionErrorKey = "FindCarPreferencesControl.FuelConsumptionErrorKey";
        private const string FuelConsumptionMPGErrorKey = "FindCarPreferencesControl.FuelConsumptionMPGErrorKey";
        private const string FuelConsumptionLKMErrorKey = "FindCarPreferencesControl.FuelConsumptionLKMErrorKey";
        private const string FuelCostErrorKey = "FindCarPreferencesControl.FuelCostErrorKey";
        private const string PETROL_PER_LITRE = "FindCarPreferencesControl.PetrolPerLitre";
        private const string DIESEL_PER_LITRE = "FindCarPreferencesControl.DieselPerLitre";

		private const string PreferencesHeaderLabelTextKey = "FindPreferencesOptionsControl.PreferencesHeaderLabel.{0}";
		private const string PreferencesCarHeaderLabelTextKey = "FindPreferencesOptionsControl.PreferencesCarHeaderLabel.{0}";

		private const string TxtSeven = "txtseven";
		private const string TxtSevenB = "txtsevenb";
		private const string TxtSevenR = "txtsevenr";
		
		#endregion

        #region Private variables
        // Page level variables
		private IDataServices populator;

		private GenericDisplayMode journeyTypeDisplayMode = GenericDisplayMode.Normal;
		private GenericDisplayMode speedDisplayMode = GenericDisplayMode.Normal;
		private GenericDisplayMode carSizeDisplayMode = GenericDisplayMode.Normal;
		private GenericDisplayMode fuelTypeDisplayMode = GenericDisplayMode.Normal;
		private GenericDisplayMode fuelUseUnitDisplayMode = GenericDisplayMode.Normal;
		private GenericDisplayMode fuelConsumptionOptionMode = GenericDisplayMode.Normal;
		private GenericDisplayMode fuelCostOptionMode = GenericDisplayMode.Normal;
		private bool newLocationClicked;
		private bool validFuelConsumption;
		private bool validFuelCost;
      
		private bool preferencesChanged;
	

		private static readonly object PreferencesVisibleChangedEventKey = new object();
		private static readonly object SpeedChangedEventKey = new object();
		private static readonly object JourneyTypeChangedEventKey = new object();
		private static readonly object DoNotUseMotorwaysChangedKey = new object();
		private static readonly object CarSizeChangedEventKey = new object();
		private static readonly object FuelTypeChangedEventKey = new object();
		private static readonly object AverageFuelUseChangedKey = new object();
		private static readonly object SpecificFuelUseChangedKey = new object();
		private static readonly object SpecificFuelUseUnitChangedKey = new object();
		private static readonly object AverageFuelCostChangedEventKey = new object();
		private static readonly object SpecificFuelCostChangedEventKey = new object();	
		private static readonly object FuelConsumptionOptionChangedEventKey = new object();
		private static readonly object FuelConsumptionTextChangedEventKey = new object();
		private static readonly object FuelCostTextChangedEventKey = new object();
		private static readonly object FuelCostOptionChangedEventKey = new object();
		protected System.Web.UI.WebControls.CheckBox doNotUseMotorwaysCheckox;

        // Variables to retain the selected index of lists
        private int listCarSpeedIndex = -1;
        private int listCarJourneyTypeIndex = -1;
        private int listCarSizeIndex = -1;
        private int listFuelTypeIndex = -1;
        private int listFuelConsumptionUnitIndex = -1;
        private int fuelConsumptionSelectRadioIndex = -1;
        private int fuelCostSelectRadioIndex = -1;

        #endregion

        #endregion

        #region Page_Init, Page_Load, Page_PreRender

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
			journeyOptionsControl.TristateLocationControl.NewLocation += new EventHandler(OnNewLocation);
			journeyOptionsControl.LocationControl.NewLocationClick += new EventHandler(OnNewLocation);
		} 

		/// <summary>
		/// Populates drop down lists and sets label text
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// only called from journeyPlannerInput (if on ambiguity page, we may need to show these labels!)
			if(this.PageId == PageId.JourneyPlannerInput)
			{
				ShowDisplayLabels(false);
            }
            
            LoadLists();

            UpdateControls();

            LoadResources();
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
		
			// Set state of the preferences control
			if(TDSessionManager.Current.Authenticated) 
			{
				loginSaveOption.LoggedInDisplay();
			}
			else
			{
				loginSaveOption.LoggedOutDisplay();
			}

			bool showPreferences;

			if (AmbiguityMode) 
			{
				// Fix for IR 1562 - added  || panelAvoidSelect.Visible to this line
				showPreferences = (journeyOptionsControl.TristateLocationControl.Visible 
                                   || journeyOptionsControl.LocationControl.Visible
                                   || panelTypeOfJourney.Visible 
                                   || panelCarDetails.Visible 
                                   || journeyOptionsControl.Visible);
			} 
			else 
			{
				showPreferences = PreferencesVisible;
			}

			preferencesPanel.Visible = showPreferences;

			//IR2222 Hide the div to prevent empty blue box being displayed when the cardetails and type of journey details are not displayed
			if (preferencesPanel.Visible)
			{
				if (!panelCarDetails.Visible && !panelTypeOfJourney.Visible)
				{
					panelDivHider.Visible=false;
				}
				else
					panelDivHider.Visible=true;
			}
		}
		#endregion

        #region Events

        #region public event handling

        /// <summary>
		/// Occurs when the selection is changed in the "Changes" dropdown
		/// </summary>
		public event EventHandler SpeedChanged   
		{
			add { this.Events.AddHandler(SpeedChangedEventKey, value); }
			remove { this.Events.RemoveHandler(SpeedChangedEventKey, value); }
		}

		/// <summary>
		/// Occurs when the selection is changed in the "Changes Speed" dropdown
		/// </summary>
		public event EventHandler JourneyTypeChanged
		{
			add { this.Events.AddHandler(JourneyTypeChangedEventKey, value); }
			remove { this.Events.RemoveHandler(JourneyTypeChangedEventKey, value); }
		}

		/// <summary>
		/// Occurs when the user clicks the show preferences or hide preferences buttons
		/// </summary>
		public event EventHandler PreferencesVisibleChanged
		{
			add { this.Events.AddHandler(PreferencesVisibleChangedEventKey, value); }
			remove { this.Events.RemoveHandler(PreferencesVisibleChangedEventKey, value); }
		}

		/// <summary>
		/// Occurs when the user ticks the do not use motorways check box
		/// </summary>
		public event EventHandler DoNotUseMotorwayChanged
		{
			add { this.Events.AddHandler(DoNotUseMotorwaysChangedKey, value); }
			remove { this.Events.RemoveHandler(DoNotUseMotorwaysChangedKey, value); }
		}

		/// <summary>
		/// Occurs when the selection is changed in the "CarSize" dropdown
		/// </summary>

		public event EventHandler CarSizeChanged
		{
			add { this.Events.AddHandler(CarSizeChangedEventKey, value); }
			remove { this.Events.RemoveHandler(CarSizeChangedEventKey, value); }
		}

		/// <summary>
		/// Occurs when the selection is changed in the "FuelType" dropdown
		/// </summary>

		public event EventHandler FuelTypeChanged
		{
			add { this.Events.AddHandler(FuelTypeChangedEventKey, value); }
			remove { this.Events.RemoveHandler(FuelTypeChangedEventKey, value); }
		}

		/// <summary>
		/// Occurs when the user selects "consumption option" radio button
		/// </summary>
		
		public event EventHandler ConsumptionOptionChanged
		{
			add { this.Events.AddHandler(FuelConsumptionOptionChangedEventKey, value); }
			remove { this.Events.RemoveHandler(FuelConsumptionOptionChangedEventKey, value); }
		}

		/// <summary>
		/// Occurs when the user selects "fuel cost option" radio button
		/// </summary>
		
		public event EventHandler FuelCostOptionChanged
		{
			add { this.Events.AddHandler(FuelCostOptionChangedEventKey, value); }
			remove { this.Events.RemoveHandler(FuelCostOptionChangedEventKey, value); }
		}
			

		public event EventHandler FuelConsumptionTextChanged
		{
			add { this.Events.AddHandler(FuelConsumptionTextChangedEventKey, value); }
			remove { this.Events.RemoveHandler(FuelConsumptionTextChangedEventKey, value); }
		}
			
		public event EventHandler FuelCostTextChanged
		{
			add { this.Events.AddHandler(FuelCostTextChangedEventKey, value); }
			remove { this.Events.RemoveHandler(FuelCostTextChangedEventKey, value); }
		}
		
		/// <summary>
		/// Occurs when the selection is changed in the "specific Fuel use triStateLocationControl" dropdown.
		/// </summary>
		
		public event EventHandler SpecificFuelUseUnitChanged
		{
			add { this.Events.AddHandler(SpecificFuelUseUnitChangedKey, value); }
			remove { this.Events.RemoveHandler(SpecificFuelUseUnitChangedKey, value); }
		}


		
		#endregion

		#region private event handling
		
		/// <summary>
		/// Handles the Changed event of the changes dropdown and raises
		/// the event on to the client
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnJourneyTypeOptionChanged(object sender, EventArgs e)
		{
			RaiseEvent(JourneyTypeChangedEventKey);
		}

		/// <summary>
		/// Handles the Changed event of the change speed dropdown and
		/// raises the event on to the client
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnSpeedOptionChanged(object sender, EventArgs e)
		{
            
            RaiseEvent(SpeedChangedEventKey);
		}

		/// <summary>
		/// Handles the Changed event of the car size dropdown and raises
		/// the event on to the client
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnCarSizeOptionChanged(object sender, EventArgs e)
		{
			RaiseEvent(CarSizeChangedEventKey);
		}

		/// <summary>
		/// Handles the Changed event of the Fuel Type dropdown and raises
		/// the event on to the client
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnFuelTypeOptionChanged(object sender, EventArgs e)
		{
			RaiseEvent(FuelTypeChangedEventKey);
		}

		/// <summary>
		/// Handles the Changed event of the Fuel consumption radio button and raises
		/// the event on to the client
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnFuelConsumptionOptionChanged(object sender, EventArgs e)
		{
			RaiseEvent(FuelConsumptionOptionChangedEventKey);
		}

        /// <summary>
        /// Handles the Changed event of the Fuel consumption text and raises
        /// the event on to the client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

		protected void OnFuelConsumptionTextChanged(object sender, EventArgs e)
		{
			RaiseEvent(FuelConsumptionTextChangedEventKey);
		}

        /// <summary>
        /// Handles the Changed event of the Fuel cost text and raises
        /// the event on to the client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		protected void OnFuelCostTextChanged(object sender, EventArgs e)
		{
			RaiseEvent(FuelCostTextChangedEventKey);
		}


		/// <summary>
		/// Handles the Changed event of the Fuel Consumption unit dropdown and raises
		/// the event on to the client
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnFuelConsumptionUnitChanged(object sender, EventArgs e)
		{
			RaiseEvent(SpecificFuelUseUnitChangedKey);
		}
		
		/// <summary>
		/// Handles the Changed event of the Fuel cost radio button and raises
		/// the event on to the client
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnFuelCostOptionChanged(object sender, EventArgs e)
		{
			RaiseEvent(FuelCostOptionChangedEventKey);
		}

        /// <summary>
        /// Handles the Changed event of doNotUseMotorways tick box and raises
        /// the event on to the client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		protected void OnDoNotUseMotorwaysChanged(object sender, System.EventArgs e)
		{
			RaiseEvent(DoNotUseMotorwaysChangedKey);
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
		/// Handles the ShowPreferences event of the FindPageOptionsControl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnShowCarDetails(object sender, EventArgs e)
		{
			PreferencesVisible = true;
		}

		
		/// <summary>
		/// Handles the ShowPreferences event of the FindPageOptionsControl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnHideCarDetails(object sender, EventArgs e)
		{
			PreferencesVisible = false;
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

        #endregion

        #region public properties

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
		public D2DPreferencesOptionsControl PreferencesOptionsControl
		{
			get { return preferencesOptionsControl; }
		}

		/// <summary>
		/// Allows access to the FindCarJourneyOptionsControl contained within this control.
		/// </summary>
		public D2DCarJourneyOptionsControl JourneyOptionsControl
		{
			get { return journeyOptionsControl; }
		}

		/// <summary>
		/// Controls whether or not the preferences panel is visible
		/// </summary>
		public bool PreferencesVisible
		{
			get { return preferencesPanel.Visible; }
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
		/// Returns true if the control is being displayed in ambiguity mode. This is determined
		/// from the values of <code>journeyTypeDisplayMode</code> and <code>speedDisplayMode</code>.
		/// </summary>
		public bool AmbiguityMode
		{
			get { return ((TypeJourneyDisplayMode != GenericDisplayMode.Normal) || (SpeedChangeDisplayMode != GenericDisplayMode.Normal)
						|| (CarSizeDisplayMode != GenericDisplayMode.Normal) || (FuelTypeDisplayMode != GenericDisplayMode.Normal)
						|| (FuelUseUnitDisplayMode != GenericDisplayMode.Normal)); }

        }

        #region Display modes

        /// <summary>
		/// Sets the mode for the Changes dropdown.
		/// Note that in this context, GenericDisplayMode.Ambiguity is treated
		/// the same way as GenericDisplayMode.Readonly.
		/// </summary>
		public GenericDisplayMode TypeJourneyDisplayMode
		{
			get { return journeyTypeDisplayMode; }
			set { journeyTypeDisplayMode = value; }
		}

		/// <summary>
		/// Sets the mode for the Changes Speed dropdown.
		/// Note that in this context, GenericDisplayMode.Ambiguity is treated
		/// the same way as GenericDisplayMode.Readonly.
		/// </summary>
		public GenericDisplayMode SpeedChangeDisplayMode
		{
			get { return speedDisplayMode; }
			set { speedDisplayMode = value; }
		}

		/// <summary>
		/// Sets the mode for the car size dropdown.
		/// Note that in this context, GenericDisplayMode.Ambiguity is treated
		/// the same way as GenericDisplayMode.Readonly.
		/// </summary>
		public GenericDisplayMode CarSizeDisplayMode
		{
			get { return carSizeDisplayMode; }
			set { carSizeDisplayMode = value; }
		}

		/// <summary>
		/// Sets the mode for the FuelType dropdown.
		/// Note that in this context, GenericDisplayMode.Ambiguity is treated
		/// the same way as GenericDisplayMode.Readonly.
		/// </summary>
		public GenericDisplayMode FuelTypeDisplayMode
		{
			get { return fuelTypeDisplayMode; }
			set { fuelTypeDisplayMode = value; }
		}

		/// <summary>
		/// Sets the mode for the FuelConsumption unit dropdown.
		/// Note that in this context, GenericDisplayMode.Ambiguity is treated
		/// the same way as GenericDisplayMode.Readonly.
		/// </summary>
		public GenericDisplayMode FuelUseUnitDisplayMode
		{
			get { return fuelUseUnitDisplayMode; }
			set { fuelUseUnitDisplayMode = value; }
		}

		/// <summary>
		/// Sets the mode for the FuelConsumption option radio button.
		/// Note that in this context, GenericDisplayMode.Ambiguity is treated
		/// the same way as GenericDisplayMode.Readonly.
		/// </summary>
		public GenericDisplayMode FuelConsumptionOptionMode
		{
			get { return fuelConsumptionOptionMode; }
			set { fuelConsumptionOptionMode = value; }
		}

		/// <summary>
		/// Sets the mode for the FuelCost option radio button.
		/// Note that in this context, GenericDisplayMode.Ambiguity is treated
		/// the same way as GenericDisplayMode.Readonly.
		/// </summary>
		public GenericDisplayMode FuelCostOptionMode
		{
			get { return fuelCostOptionMode; }
			set { fuelCostOptionMode = value; }
        }

        #endregion

        /// <summary>
		/// Gets/sets if motorways should not be used
		/// </summary>
		public bool DoNotUseMotorways
		{
			get { return this.doNotUseMotorwaysCheckBox.Checked; }
			set { doNotUseMotorwaysCheckBox.Checked = value; }
		}

		/// <summary>
		/// Gets/Sets journey type in dropdown list
		/// </summary>
		public PrivateAlgorithmType FindJourneyType
		{
			get
			{
				string itemValue = populator.GetValue(DataServiceType.DrivingFindDrop, listCarJourneyType.SelectedItem.Value);
				return (PrivateAlgorithmType)Enum.Parse(typeof(PrivateAlgorithmType),itemValue);
			}
			set
			{
				string typeId = populator.GetResourceId(DataServiceType.DrivingFindDrop,
					Enum.GetName(typeof(PrivateAlgorithmType), value ));
				populator.Select(listCarJourneyType, typeId); 
			}
		}

		/// <summary>
		/// Gets/Sets driving speed in dropdown list
		/// </summary>
		public int DrivingSpeed
		{
			get
			{
				string itemValue = populator.GetValue(DataServiceType.DrivingMaxSpeedDrop,listCarSpeed.SelectedItem.Value);
				return Convert.ToInt32(itemValue);
			}
			set
			{
				string speedId = populator.GetResourceId(DataServiceType.DrivingMaxSpeedDrop, value.ToString() );                
				populator.Select(listCarSpeed, speedId);
			}
		}

		/// <summary>
		/// Gets/Sets car size in dropdown list
		/// </summary>
		public string CarSize
		{
			get
			{
				return populator.GetValue(DataServiceType.ListCarSizeDrop, listCarSize.SelectedItem.Value);
			}
			set
			{
				
                string carSizeId = populator.GetResourceId(DataServiceType.ListCarSizeDrop, value);
				populator.Select(listCarSize, carSizeId);
			}
		}

		/// <summary>
		/// Gets/Sets Fuel type in dropdown list
		/// </summary>
		public string FuelType
		{
			get
			{
				return populator.GetValue(DataServiceType.ListFuelTypeDrop, listFuelType.SelectedItem.Value);
			}
			set
			{
				string fuelTypeId = populator.GetResourceId(DataServiceType.ListFuelTypeDrop, value);
				populator.Select(listFuelType, fuelTypeId);
			}
		}

		/// <summary>
		/// Gets/Sets input fuel consumption text
		/// </summary>
		public string InputConsumptionText
		{
			get { return HttpUtility.HtmlDecode( FuelConsumptionValue ); }
			set { FuelConsumptionValue = HttpUtility.HtmlEncode(value); }
		}

        /// <summary>
        /// Gets fuel consumption text
        /// </summary>
        public string FuelConsumptionText
        {
            get { return textFuelConsumption.Text; }
        }

        /// <summary>
        /// Gets fuel cost text
        /// </summary>
        public string FuelCostText
        {
            get { return textFuelCost.Text; }
        }

		/// <summary>
		/// Gets/Sets input fuel cost text
		/// </summary>
		public string InputCostText
		{
            get { return HttpUtility.HtmlDecode(FuelCostValue); }
            set { FuelCostValue = HttpUtility.HtmlEncode(value); }
		}

		/// <summary>
		/// Get/set property returning if default fuel-cost should be used or not.
		/// This returns true if default fuel-cost is used, false means user is expected to input
		/// a value.
		/// </summary>
		public bool FuelCostOption
		{
			get
			{
				return fuelCostSelectRadio.SelectedIndex == 0;
			}
			set
			{
				fuelCostSelectRadio.SelectedIndex = value ? 0 : 1;
				if( value )
				{
					textFuelCost.Text = string.Empty;
				}
			}
		}

		/// <summary>
		/// Set/get property returning the fuel cost.
		/// This relies on fuel-type and that the correct value
		/// for FuelCostOption has been set prior.
		/// </summary>
		public string FuelCostValue
		{
			get
			{
				CarCostCalculator costCalculator = (CarCostCalculator)TDServiceDiscovery.Current[ ServiceDiscoveryKey.CarCostCalculator ];

				if( FuelCostOption )
				{
					
					return string.Format(costCalculator.GetFuelCost(FuelType).ToString());
					
				}
				return textFuelCost.Text;
			}
			set
			{
				textFuelCost.Text = FuelCostOption  ? string.Empty : value;
			}
		}

		/// <summary>
		/// Set/Get property if the default fuel consumption value is to be used or not.
		/// true means default fuel consumption value should be used.
		/// </summary>
		public bool FuelConsumptionOption
		{
			get
			{
				return this.fuelConsumptionSelectRadio.SelectedIndex == 0;
			}
			set
			{
				this.fuelConsumptionSelectRadio.SelectedIndex = value ? 0 : 1;
				if( value )
				{
					textFuelConsumption.Text = string.Empty;
				}
			}
		}

		/// <summary>
		/// Get/Set property returning the fuel consumption (miles/gallon) control
		/// This will return the default value if the user has ticked the correct
		/// box. This relies on the fact that correct car-size and fuel type has
		/// been set before this is asked for the value.
		/// </summary>
		public string FuelConsumptionValue
		{
			get
			{	
				CarCostCalculator costCalculator = (CarCostCalculator)TDServiceDiscovery.Current[ ServiceDiscoveryKey.CarCostCalculator ];
				
				if( this.FuelConsumptionOption )
				{
					
					return string.Format(costCalculator.GetFuelConsumption(CarSize, FuelType).ToString());
				}

                return textFuelConsumption.Text;
			}
			set
			{
				{
					textFuelConsumption.Text = FuelConsumptionOption ? string.Empty : value;
				}
			}
		}
		
		/// <summary>
		/// Gets/Sets FuelConsumption unit change in dropdown list
		/// </summary>
		public int FuelConsumptionUnit
		{
			get
			{
				string itemValue = populator.GetValue(DataServiceType.FuelConsumptionUnitDrop, listFuelConsumptionUnit.SelectedItem.Value);
				return Convert.ToInt32(itemValue);
			}
			set
			{
				string fuelUseUnitChangeId = populator.GetResourceId(DataServiceType.FuelConsumptionUnitDrop, value.ToString());
			
				populator.Select(listFuelConsumptionUnit, fuelUseUnitChangeId);
			}
        }
        #region Labels and Panels

        /// <summary>
		/// Read only property returning the journey type display label
		/// used when in ambiguity mode to display the selected journey
		/// type
		/// </summary>
		public Label JourneyTypeDisplayLabel 
		{
            get { return displayTypeListLabel; }
		}

		/// <summary>
		/// Read only property returning the journey type display label
		/// used when in ambiguity mode to display the selected journey
		/// type
		/// </summary>
		public Label SpeedDisplayLabel 
		{
            get { return displaySpeedListLabel; }
		}

		/// <summary>
		/// Read only property returning the car details display label
		/// used when in ambiguity mode to display the car details
		/// </summary>
		public Label CarDetailsLabel 
		{
            get { return displayCarDetailsLabel; }
		}

		/// <summary>
		/// Read only property returning Fuel Consumption display label
		/// used when in ambiguity mode to display the fuel consumption
		/// details
		/// </summary>
		public Label FuelConsumptionLabel 
		{
            get { return displayFuelConsumptionLabel; }
		}

		/// <summary>
		/// Read only property returning the Fuel Cost display label
		/// used when in ambiguity mode to display the fuel cost details
		/// </summary>
		public Label FuelCostLabel 
		{
            get { return displayFuelCostLabel; }
		}

		/// <summary>
		/// Read only property which exposes the control's DivHider panel 
		/// The DivHider panel contains the TypeOfJourney and CareDetails panels
		/// </summary>
		public Panel PanelDivHider
		{
            get { return panelDivHider; }
		}

		/// <summary>
		/// Read only property which exposes the control's TypeOfJourney panel 
		/// </summary>
		public Panel PanelTypeOfJourney
		{
            get { return panelTypeOfJourney; }
		}

		/// <summary>
		/// Read only property which exposes the control's car details panel
		/// </summary>
		public Panel PanelCarDetails
		{
            get { return panelCarDetails; }
        }

        #endregion

        /// <summary>
        /// Controls the visibility of the "save preferences" facility
        /// </summary>
        public bool AllowSavePreferences
        {
            get { return loginSaveOption.Visible; }
            set { loginSaveOption.Visible = value; }
        }
		
		/// <summary>
		/// Returns true if the user is logged in and has elected to save their
		/// travel details
		/// Read only.
		/// </summary>
		public bool SavePreferences
		{
			get { return loginSaveOption.SaveDetails; }
		}

        /// <summary>
        /// Returns true if the preferences changed
        /// </summary>

        public bool PreferencesChanged
        {
            get { return preferencesChanged; }

            set
            {
                preferencesChanged = value;
            }
        }

        /// <summary>
        /// Returns true if the fuel consumption entered is valid
        /// </summary>
        public bool FuelConsumptionValid
        {
            get { return validFuelConsumption; }
            set { validFuelConsumption = value; }
        }
           
        /// <summary>
        /// Returns true if the fuel cost entered is valid
        /// </summary>
        public bool FuelCostValid
        {
            get { return validFuelCost; }
            set { validFuelCost = value; }
        }

        /// <summary>
        /// Read only. Returns true if non-default options have been selected
        /// </summary>
        public bool IsOptionSelected
        {
            get
            {
                return !DrivingSpeedIsDefault()
                    || !DrivingTypeIsDefault()
                    || !CarSizeIsDefault()
                    || !FuelTypeIsDefault()
                    || !FuelUseUnitIsDefault()
                    || !FuelConsumptionOptionIsDefault()
                    || !FuelCostOptionIsDefault()
                    || DoNotUseMotorways
                    || (journeyOptionsControl.LocationControl.Location != null && 
                        (journeyOptionsControl.LocationControl.Location.Status == TDLocationStatus.Valid ||
                         journeyOptionsControl.LocationControl.Location.Status == TDLocationStatus.Ambiguous));
            }
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

        public void ShowDisplayLabels(bool visibility)
        {
            // car details display labels
            this.displayFuelCostLabel.Visible = visibility;
            this.displayFuelConsumptionLabel.Visible = visibility;
            this.displayCarDetailsLabel.Visible = visibility;

            //type of journey display labels
            this.displaySpeedListLabel.Visible = visibility;
            this.displayTypeListLabel.Visible = visibility;
            this.displayDoNotUseMotorwaysLabel.Visible = visibility;
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
            listCarJourneyType.SelectedIndexChanged += new EventHandler(this.OnJourneyTypeOptionChanged);
            listCarSpeed.SelectedIndexChanged += new EventHandler(this.OnSpeedOptionChanged);
            doNotUseMotorwaysCheckBox.CheckedChanged += new EventHandler(this.OnDoNotUseMotorwaysChanged);
            listCarSize.SelectedIndexChanged += new EventHandler(this.OnCarSizeOptionChanged);
            listFuelType.SelectedIndexChanged += new EventHandler(this.OnFuelTypeOptionChanged);
            fuelConsumptionSelectRadio.SelectedIndexChanged += new EventHandler(this.OnFuelConsumptionOptionChanged);
            listFuelConsumptionUnit.SelectedIndexChanged += new EventHandler(this.OnFuelConsumptionUnitChanged);
            fuelCostSelectRadio.SelectedIndexChanged += new EventHandler(this.OnFuelCostOptionChanged);
            textFuelConsumption.TextChanged += new EventHandler(this.OnFuelConsumptionTextChanged);
            textFuelCost.TextChanged += new EventHandler(this.OnFuelCostTextChanged);
		}

		
		#endregion

        #region Private methods

        /// <summary>
        /// Loads the list controls
        /// </summary>
        private void LoadLists()
        {
            if (Page.IsPostBack)
            {
                GetListSelectedItems();
            }

            // Load dropdown info
            populator.LoadListControl(DataServiceType.DrivingMaxSpeedDrop, listCarSpeed);
            populator.LoadListControl(DataServiceType.DrivingFindDrop, listCarJourneyType);
            populator.LoadListControl(DataServiceType.ListCarSizeDrop, listCarSize);
            populator.LoadListControl(DataServiceType.ListFuelTypeDrop, listFuelType);
            populator.LoadListControl(DataServiceType.FuelConsumptionUnitDrop, listFuelConsumptionUnit);
            populator.LoadListControl(DataServiceType.FuelConsumptionSelectRadio, fuelConsumptionSelectRadio);
            populator.LoadListControl(DataServiceType.FuelCostSelectRadio, fuelCostSelectRadio);

            if (Page.IsPostBack)
            {
                SetListSelectedItems();
            }
        }

        /// <summary>
        /// Loads resource strings and text
        /// </summary>
        private void LoadResources()
        {
            // Load strings from the languages file
            journeyTypeLabel.Text = resourceManager.GetString(JourneyTypeKey, TDCultureInfo.CurrentUICulture);
            findLabel.Text = resourceManager.GetString(FindKey, TDCultureInfo.CurrentUICulture) + " ";
            journeysLabel.Text = resourceManager.GetString(JourneysKey, TDCultureInfo.CurrentUICulture);

            carSpeedLabel.Text = resourceManager.GetString(CarSpeedKey, TDCultureInfo.CurrentUICulture) + " ";
            doNotUseMotorwaysCheckBox.Text = resourceManager.GetString(MotorwayKey, TDCultureInfo.CurrentUICulture);
            displayDoNotUseMotorwaysLabel.Text = GetResource(MotorwayKey);
            labelCarDetails.Text = GetResource(CarDetailsKey);
            iHaveaLabel.Text = GetResource(IHaveaKey);
            sizedLabel.Text = GetResource(SizedKey);
            carLabel.Text = GetResource(CarKey);
            myFuelConsumptionIsLabel.Text = GetResource(FuelConsumptionKey);
            myFuelCostIsLabel.Text = GetResource(FuelCostKey);
            pencePerLitreLabel.Text = GetResource(PencePerLitreKey);
            displayFuelConsumptionErrorLabel.Text = GetResource(FuelConsumptionErrorKey);
            displayFuelCostErrorLabel.Text = GetResource(FuelCostErrorKey);

            // add the current petrol and diesel cost to 'current average cost: '
            ListItem averageFuelCost = fuelCostSelectRadio.SelectedItem;
            CarCostCalculator costCalculator = (CarCostCalculator)TDServiceDiscovery.Current[ServiceDiscoveryKey.CarCostCalculator];
            string petrolCost = ((Convert.ToDecimal(costCalculator.GetFuelCost("petrol"))) / 10).ToString();
            string dieselCost = ((Convert.ToDecimal(costCalculator.GetFuelCost("diesel"))) / 10).ToString();
            averageFuelCost.Text = averageFuelCost.Text + " " +
                petrolCost + " " + GetResource(PETROL_PER_LITRE) + " " +
                dieselCost + " " + GetResource(DIESEL_PER_LITRE) + " ";

            textFuelConsumption.CssClass = "";
            textFuelCost.CssClass = "";

            labelJsQuestion.Text = GetResource("CarPreferencesControl.Question");
            labelOptionsSelected.Text = GetResource("CarPreferencesControl.OptionsSelected");

            btnShow.Text = GetResource("AdvancedOptions.Show.Text");
        }

        #region update controls

        /// <summary>
		/// Updates the state of nested controls with this object's property values
		/// </summary>
		private void UpdateControls() 
		{

			// Don't show the via location control or FindCarJourneyOptionsControl if in ambiguous mode and 
			// no roads to avoid or no location has been entered
			// However if in ambiguous mode and user clicks new location, this location will be empty and
			// we do need to make the control visible so use newLocationClicked to determine this case.
			// Don't do this if the control is called from door to door ambiguity page because this is handled
			// in the ambiguity page.
			if (PageId != PageId.JourneyPlannerAmbiguity)
			{
                if (journeyOptionsControl.TristateLocationControl.Visible)
                {
                    if (AmbiguityMode
                        && journeyOptionsControl.TheSearch.InputText.Length == 0
                        && !newLocationClicked)
                        journeyOptionsControl.TristateLocationControl.Visible = false;
                    else
                        journeyOptionsControl.TristateLocationControl.Visible = true;
                }

                if (journeyOptionsControl.LocationControl.Visible)
                {
                    if (AmbiguityMode
                        && journeyOptionsControl.LocationControl.Search.InputText.Length == 0
                        && !newLocationClicked)
                        journeyOptionsControl.LocationControl.Visible = false;
                    else
                        journeyOptionsControl.LocationControl.Visible = true;
                }
			}
		}

		/// <summary>
		/// Returns true if the choosen driving speed is the drop down default value, false otherwise
		/// </summary>
		/// <returns>true if the choosen driving speed is the drop down default value, false otherwise</returns>
		private bool DrivingSpeedIsDefault() 
		{
			string defaultItemValue = populator.GetDefaultListControlValue(DataServiceType.DrivingMaxSpeedDrop);
			return this.DrivingSpeed == Convert.ToInt32(defaultItemValue);
		}

		/// <summary>
		/// Returns true if the choosen driving type is the drop down default value, false otherwise
		/// </summary>
		/// <returns>true if the choosen driving type is the drop down default value, false otherwise</returns>
		private bool DrivingTypeIsDefault() 
		{
			string defaultItemValue = populator.GetDefaultListControlValue(DataServiceType.DrivingFindDrop);
			return this.FindJourneyType == (PrivateAlgorithmType)Enum.Parse(typeof(PrivateAlgorithmType),defaultItemValue);            
		}

		/// <summary>
		/// Returns true if the chosen car size is the drop down default value, false otherwise
		/// </summary>
		/// <returns>true if the chosen car size is the drop down default value, false otherwise</returns>
		private bool CarSizeIsDefault() 
		{
			string defaultItemValue = populator.GetDefaultListControlValue(DataServiceType.ListCarSizeDrop);
			return this.CarSize == defaultItemValue;            
		}

		/// <summary>
		/// Returns true if the chosen fuel type is the drop down default value, false otherwise
		/// </summary>
		/// <returns>true if the chosen fuel type is the drop down default value, false otherwise</returns>
		private bool FuelTypeIsDefault() 
		{
			string defaultItemValue = populator.GetDefaultListControlValue(DataServiceType.ListFuelTypeDrop);
			return this.FuelType == defaultItemValue; 
		}

		/// <summary>
		/// Returns true if the chosen fuel consumption unit is the drop down default value, false otherwise
		/// </summary>
		/// <returns>true if the chosen fuel consumption unit is the drop down default value, false otherwise</returns>
		public bool FuelUseUnitIsDefault() 
		{
			string defaultItemValue = populator.GetDefaultListControlValue(DataServiceType.FuelConsumptionUnitDrop);
			// fuelUseUnitChange 
			return this.FuelConsumptionUnit  == Convert.ToInt32(defaultItemValue);            
		}

		/// <summary>
		/// Returns true if the chosen fuelConsumptionOption is the radio button default value, false otherwise
		/// </summary>
		/// <returns>true if the chosen fuelConsumptionOption is the radio button default value, false otherwise</returns>
		public bool FuelConsumptionOptionIsDefault() 
		{
			return this.FuelConsumptionOption;            
		}

		/// <summary>
		/// Returns true if the chosen fuelCostOption is the radio button default value, false otherwise
		/// </summary>
		/// <returns>true if the chosen fuelCostOption is the radio button default value, false otherwise</returns>
		public bool FuelCostOptionIsDefault() 
		{
			return this.FuelCostOption;
		}


		/// <summary>
		/// Sets the state of the changes controls.  NB: the display label text should be 
		/// set from the container page (requires journey parameter values entered by user)
		/// </summary>
		private void UpdateChangesControl()
		{

			bool showSpeedChange = !DrivingSpeedIsDefault();
			bool showTypeChange = !DrivingTypeIsDefault();
			bool showCarSizeChange = !CarSizeIsDefault();
			bool showFuelTypeChange = !FuelTypeIsDefault();
			bool showFuelUnitChange = !FuelUseUnitIsDefault();
			bool showFuelConsumptionOptionChange = !FuelConsumptionOptionIsDefault();
			bool showFuelCostOptionChange = !FuelCostOptionIsDefault();


			// since the journey type, speed, car size, fuel type and fuel consumption unit can't be ambiguous, 
			//they will either be input or display only
			switch (journeyTypeDisplayMode)
			{
				case GenericDisplayMode.ReadOnly:
				case GenericDisplayMode.Ambiguity:

					listCarJourneyType.Visible = false; 
					findLabel.Visible = false;
					displayTypeListLabel.Visible = showTypeChange;
					if (showTypeChange) 
					{
						displayTypeListLabel.Text = GetResource(FindKey) + ": " + listCarJourneyType.SelectedItem.Text + " " +
							GetResource(JourneysKey);
					}
					journeysLabel.Visible = false;
					panelTypeOfJourney.Visible = showSpeedChange || showTypeChange || DoNotUseMotorways;
					break;
				case GenericDisplayMode.Normal:
				default:
					listCarJourneyType.Visible = true;
					findLabel.Visible = true;
					displayTypeListLabel.Visible = false;
					journeysLabel.Visible = true;
					panelTypeOfJourney.Visible = true;
					break;
			}

			switch (speedDisplayMode)
			{
				case GenericDisplayMode.ReadOnly:
				case GenericDisplayMode.Ambiguity:
					listCarSpeed.Visible = false;
					carSpeedLabel.Visible = false;
					displaySpeedListLabel.Visible = showSpeedChange;
					if (showSpeedChange) 
					{
						displaySpeedListLabel.Text = GetResource(CarSpeedKey) + ": " + listCarSpeed.SelectedItem.Text;
					}
					panelTypeOfJourney.Visible = showSpeedChange || showTypeChange || DoNotUseMotorways;
					break;
				case GenericDisplayMode.Normal:
				default:
					listCarSpeed.Visible = true; 
					carSpeedLabel.Visible = true;
					displaySpeedListLabel.Visible = false;
					panelTypeOfJourney.Visible = true;
					break;
			}

			switch (carSizeDisplayMode)
			{
				case GenericDisplayMode.ReadOnly:
				case GenericDisplayMode.Ambiguity:

					listCarSize.Visible = false;
					iHaveaLabel.Visible = false;

					if (this.PageId == PageId.FindCarInput  || this.PageId == PageId.ExtendJourneyInput || this.PageId == PageId.ParkAndRideInput)
					{
						displayCarDetailsLabel.Visible = showCarSizeChange || showFuelTypeChange;
						if (showCarSizeChange || showFuelTypeChange) 
						{
							// This sentence to be shown on a label is a concatenation of 5 parts to get something like:
							// "I have a medium sized diesel engined car".
							// The Welsh version of the sentence has a different order of parts so we have a resource key for
							// the sentence that receives the parts and orders them accordingly.
							
							// Set up the array of sentence parts to be ordered.
							string[] CarTypeSentenceParts = new string[4];
							CarTypeSentenceParts[0] = listCarSize.SelectedItem.Text.ToLower();
							CarTypeSentenceParts[1] = GetResource(SizedKey);
							CarTypeSentenceParts[2] = listFuelType.SelectedItem.Text.ToLower();
							CarTypeSentenceParts[3] = GetResource(CarKey);
							
							displayCarDetailsLabel.Text = GetResource(IHaveaKey) + " " + String.Format(GetResource(FormatCarTypeSentenceKey), CarTypeSentenceParts);

							panelCarDetails.Visible = true;
						}
						panelCarDetails.Visible = showCarSizeChange || showFuelTypeChange || showFuelUnitChange || showFuelConsumptionOptionChange || showFuelCostOptionChange;
					}
					sizedLabel.Visible = false;
					listFuelType.Visible = false;
					carLabel.Visible = false;
					break;
				case GenericDisplayMode.Normal:
				default:
					listCarSize.Visible = true;
					iHaveaLabel.Visible = true;
					displayCarDetailsLabel.Visible = false;
					sizedLabel.Visible = true;
					listFuelType.Visible = true;
					displayFuelConsumptionLabel.Visible = false;
					carLabel.Visible = true;
					panelCarDetails.Visible = true;
					break;
			}

			switch (fuelTypeDisplayMode)
			{
				case GenericDisplayMode.ReadOnly:
				case GenericDisplayMode.Ambiguity:

					listFuelType.Visible = false;

					if (this.PageId == PageId.FindCarInput || this.PageId == PageId.ExtendJourneyInput || this.PageId == PageId.ParkAndRideInput)
					{
						displayCarDetailsLabel.Visible = showFuelTypeChange || showCarSizeChange;
						if (showFuelTypeChange || showCarSizeChange) 
						{
							// This sentence to be shown on a label is a concatenation of 5 parts to get something like:
							// "I have a medium sized diesel engined car".
							// The Welsh version of the sentence has a different order of parts so we have a resource key for
							// the sentence that receives the parts and orders them accordingly.
							
							// Set up the array of sentence parts to be ordered.
							string[] CarTypeSentenceParts = new string[4];
							CarTypeSentenceParts[0] = listCarSize.SelectedItem.Text.ToLower();
							CarTypeSentenceParts[1] = GetResource(SizedKey);
							CarTypeSentenceParts[2] = listFuelType.SelectedItem.Text.ToLower();
							CarTypeSentenceParts[3] = GetResource(CarKey);

							displayCarDetailsLabel.Text = GetResource(IHaveaKey) + " " + String.Format(GetResource(FormatCarTypeSentenceKey), CarTypeSentenceParts);

							panelCarDetails.Visible = true;
						}
						panelCarDetails.Visible = showCarSizeChange || showFuelTypeChange || showFuelUnitChange || showFuelConsumptionOptionChange || showFuelCostOptionChange;		
					}
					listCarSize.Visible = false;
					iHaveaLabel.Visible = false;
					sizedLabel.Visible = false;
					carLabel.Visible = false;
					break;
				case GenericDisplayMode.Normal:
				default:
					listFuelType.Visible = true;
					displayFuelConsumptionLabel.Visible = false;
					carLabel.Visible = true;
					listCarSize.Visible = true;
					iHaveaLabel.Visible = true;
					displayCarDetailsLabel.Visible = false;
					sizedLabel.Visible = true;
					panelCarDetails.Visible = true;
					break;
			}

			switch (fuelConsumptionOptionMode)
			{
				case GenericDisplayMode.ReadOnly:
				case GenericDisplayMode.Ambiguity:

					fuelConsumptionSelectRadio.Visible = false;
					myFuelConsumptionIsLabel.Visible = false;
					textFuelConsumption.Visible = false;
					listFuelConsumptionUnit.Visible = false;
					displayFuelConsumptionErrorLabel.Visible = false;

					if (this.PageId == PageId.FindCarInput || this.PageId == PageId.ExtendJourneyInput || this.PageId == PageId.ParkAndRideInput)
					{
						
						if (showFuelConsumptionOptionChange && InputConsumptionText.Length!=0)
						{
							displayFuelConsumptionLabel.Visible = true;
						
							displayFuelConsumptionLabel.Text = GetResource(FuelConsumptionKey) + " " +
								textFuelConsumption.Text + " " + listFuelConsumptionUnit.SelectedItem.Text; 
						}

											
						panelCarDetails.Visible = ((showCarSizeChange || showFuelTypeChange) || (showFuelCostOptionChange && InputCostText.Length != 0) || (showFuelConsumptionOptionChange && InputConsumptionText.Length != 0));
					}
					else
					{
						displayFuelConsumptionLabel.Visible = showFuelConsumptionOptionChange;
						
					}

					if (!FuelConsumptionValid)
					{	
						textFuelConsumption.CssClass ="alertboxerror";
						fuelConsumptionSelectRadio.Visible = true;
						myFuelConsumptionIsLabel.Visible = true;
						textFuelConsumption.Visible = true;
						listFuelConsumptionUnit.Visible = true;
						panelCarDetails.Visible = true;
						displayFuelConsumptionLabel.Visible = false;
						displayFuelConsumptionErrorLabel.Visible = true;
						if (listFuelConsumptionUnit.SelectedIndex == 0)
						{
							displayFuelConsumptionErrorLabel.Text = string.Format(
								GetResource(FuelConsumptionMPGErrorKey), Properties.Current[ "CarCosting.MinFuelConsumption" ], Properties.Current["CarCosting.MaxConsumptionMilesPerGallon"]);
						}
						else
						{
							displayFuelConsumptionErrorLabel.Text = string.Format(
								GetResource(FuelConsumptionLKMErrorKey), Properties.Current[ "CarCosting.MaxConsumptionLitresPer100Km" ]);
						}
					}
					
					break;
				case GenericDisplayMode.Normal:
				default:
					fuelConsumptionSelectRadio.Visible = true;
					myFuelConsumptionIsLabel.Visible = true;
					textFuelConsumption.Visible = true;
					listFuelConsumptionUnit.Visible = true;
					panelCarDetails.Visible = true;
					displayFuelConsumptionErrorLabel.Visible = false;
					break;
			}

			switch (fuelCostOptionMode)
			{
				case GenericDisplayMode.ReadOnly:
				case GenericDisplayMode.Ambiguity:

					fuelCostSelectRadio.Visible = false;
					myFuelCostIsLabel.Visible = false;
					textFuelCost.Visible = false;
					pencePerLitreLabel.Visible = false;
					displayFuelCostErrorLabel.Visible = false;
					

					if (this.PageId == PageId.FindCarInput || this.PageId == PageId.ExtendJourneyInput || this.PageId == PageId.ParkAndRideInput)
					{
						
						if (showFuelCostOptionChange && InputCostText.Length != 0)
						{
							displayFuelCostLabel.Visible = true;
							displayFuelCostLabel.Text = GetResource(FuelCostKey) + " " + 
								textFuelCost.Text + " " + GetResource(PencePerLitreKey); 
						
	
						}
						
						panelCarDetails.Visible = ((showCarSizeChange || showFuelTypeChange) || (showFuelConsumptionOptionChange && InputConsumptionText.Length != 0) || (showFuelCostOptionChange && InputCostText.Length != 0));
					}
					else
					{
						displayFuelCostLabel.Visible = showFuelCostOptionChange;

					}
					if (!FuelCostValid)
					{	
						textFuelCost.CssClass ="alertboxerror";
						fuelCostSelectRadio.Visible = true;
                        pencePerLitreLabel.Visible = true;
						myFuelCostIsLabel.Visible = true;
						textFuelCost.Visible = true;
						panelCarDetails.Visible = true;
						displayFuelCostLabel.Visible = false;
						displayFuelCostErrorLabel.Visible = true;
						displayFuelCostErrorLabel.Text = string.Format(
							GetResource(FuelCostErrorKey), Properties.Current[ "CarCosting.MaxFuelCost" ]);
					}
					else
					{
						if (!FuelConsumptionValid)
						{
							panelCarDetails.Visible = true;
						}
					}
					break;
				case GenericDisplayMode.Normal:
				default:
					fuelCostSelectRadio.Visible = true;
					myFuelCostIsLabel.Visible = true;
					textFuelCost.Visible = true;
					pencePerLitreLabel.Visible = true;
					panelCarDetails.Visible = true;
					displayFuelCostLabel.Visible = false;
					displayFuelCostErrorLabel.Visible = false;
					break;
			}

		
			// DoNotUseMotorways visibility
			if (AmbiguityMode && DoNotUseMotorways)
			{
				displayDoNotUseMotorwaysLabel.Visible = true;
				doNotUseMotorwaysCheckBox.Visible = false;
			}
			else
			{
				displayDoNotUseMotorwaysLabel.Visible = false;
				doNotUseMotorwaysCheckBox.Visible = !AmbiguityMode;
			}

			if ((panelTypeOfJourney.Visible) 
                || (panelCarDetails.Visible) 
                || (DoNotUseMotorways)
                || (journeyOptionsControl.PreferencesChanged))
			{
				preferencesOptionsControl.Visible = true;
			}
			else
			{
				preferencesOptionsControl.Visible = false;
			}
									
		}

        /// <summary>
        /// Method to get the selected item in all lists. 
        /// Used to ensure the value is not lost when a list is populated
        /// </summary>
        private void GetListSelectedItems()
        {
            listCarSpeedIndex = listCarSpeed.SelectedIndex;
            listCarJourneyTypeIndex = listCarJourneyType.SelectedIndex;
            listCarSizeIndex = listCarSize.SelectedIndex;
            listFuelTypeIndex = listFuelType.SelectedIndex;
            listFuelConsumptionUnitIndex = listFuelConsumptionUnit.SelectedIndex;
            fuelConsumptionSelectRadioIndex = fuelConsumptionSelectRadio.SelectedIndex;
            fuelCostSelectRadioIndex = fuelCostSelectRadio.SelectedIndex;
        }

        /// <summary>
        /// Method to set the selected item in all lists.
        /// Used to ensure the value is not list when a list is populated
        /// </summary>
        private void SetListSelectedItems()
        {
            listCarSpeed.SelectedIndex = listCarSpeedIndex;
            listCarJourneyType.SelectedIndex = listCarJourneyTypeIndex;
            listCarSize.SelectedIndex = listCarSizeIndex;
            listFuelType.SelectedIndex = listFuelTypeIndex;
            listFuelConsumptionUnit.SelectedIndex = listFuelConsumptionUnitIndex;
            fuelConsumptionSelectRadio.SelectedIndex = fuelConsumptionSelectRadioIndex;
            fuelCostSelectRadio.SelectedIndex = fuelCostSelectRadioIndex;
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

        #endregion
    }
}
