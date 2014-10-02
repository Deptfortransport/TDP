// *********************************************** 
// NAME                 : AmendCarDetailsControl.ascx
// AUTHOR               : Darshan Sawe
// DATE CREATED         : 22/11/2006 
// DESCRIPTION			: Control displaying the input details for a Car in the amend tabs at the bottom of page
// ************************************************ 

using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Common.PropertyService.Properties;

using MeasureConvert = System.Convert;

namespace TransportDirect.UserPortal.Web.Controls
{

	/// <summary>
	///		Summary description for AmendCarDetailsControl.
	/// </summary>
	public partial class AmendCarDetailsControl : TDUserControl
	{
		
		#region Constants
		
		private const string IHaveaKey = "FindCarPreferencesControl.IHaveA";
		private const string SizedKey = "FindCarPreferencesControl.Sized";
		private const string CarKey = "FindCarPreferencesControl.Car";
		private const string FuelConsumptionKey = "AmendCarDetails.labelMyFuelConsumption";
		private const string FuelConsumptionErrorKey = "FindCarPreferencesControl.FuelConsumptionErrorKey";
		private const string FuelConsumptionMPGErrorKey = "FindCarPreferencesControl.FuelConsumptionMPGErrorKey";
		private const string FuelConsumptionLKMErrorKey = "FindCarPreferencesControl.FuelConsumptionLKMErrorKey";
		private const string OkButtonKey = "AmendSaveSendCostSearchDate.buttonOK.Text";

		#endregion

		#region Private variables
		
		// Page level variables
		private JourneyEmissionsPageState pageState;
		
		private TDJourneyParametersMulti journeyParameters;

		private IDataServices populator;
		private bool validFuelConsumption = true;

		private static readonly object CarSizeChangedEventKey = new object();
		private static readonly object FuelTypeChangedEventKey = new object();
		private static readonly object FuelConsumptionOptionChangedEventKey = new object();
		private static readonly object SpecificFuelUseUnitChangedKey = new object();
		private static readonly object FuelConsumptionTextChangedEventKey = new object();


		#endregion
		
		/// <summary>
		/// Constructor
		/// </summary>
		public AmendCarDetailsControl()
		{

		}

		#region Page_Init, Page_Load Page_PreRender

		protected void Page_PreRender(object sender, EventArgs e)
		{
			if (FuelConsumptionValid)
			{
				PopulateCarPreferences();
			}
		}

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

			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			this.listCarSize.SelectedIndexChanged += new System.EventHandler(this.OnCarSizeOptionChanged);
			this.listFuelType.SelectedIndexChanged += new System.EventHandler(this.OnFuelTypeOptionChanged);
			this.fuelConsumptionSelectRadio.SelectedIndexChanged += new System.EventHandler(this.OnFuelConsumptionOptionChanged);
			this.textFuelConsumption.TextChanged += new System.EventHandler(this.OnFuelConsumptionTextChanged);
			this.listFuelConsumptionUnit.SelectedIndexChanged += new System.EventHandler(this.OnFuelConsumptionUnitChanged);

		} 

		/// <summary>
		/// Populates drop down lists and sets label text
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			SetUpControls();
			
			//Moved to PreRender
			//PopulateCarPreferences();

			// Set the Validation errors if there are any
			if (!FuelConsumptionValid)
			{	
				DisplayErrorMessage();
			}
			
		}

		#endregion

		#region Private Methods
		/// <summary>
		/// Updates the state of nested controls with this object's property values
		/// </summary>
		private void PopulateCarPreferences()
		{
			// We want to display the preferences the user has selected on this page
			pageState = TDSessionManager.Current.JourneyEmissionsPageState;

			if(pageState.CarSize != null)
			{
				this.CarSize = pageState.CarSize;
			}
			if(pageState.CarFuelType !=null)
			{
				this.FuelType = pageState.CarFuelType;
			}
			if(!pageState.FuelConsumptionOption)
			{
				this.FuelConsumptionOption = false;
				if (pageState.FuelConsumptionUnit == 3)
				{
					this.FuelConsumptionUnit = 1;
					this.textFuelConsumption.Text = ConvertCO2toMPG(pageState.FuelConsumptionEntered, pageState.CarFuelType);
				}
				else
				{
					this.FuelConsumptionUnit = pageState.FuelConsumptionUnit;
					this.textFuelConsumption.Text = pageState.FuelConsumptionEntered;
				}
			}
		}

		private void SetUpControls()
		{
				pageState = TDSessionManager.Current.JourneyEmissionsPageState;

				iHaveaLabel.Text = GetResource(IHaveaKey);
				sizedLabel.Text = GetResource(SizedKey);
				carLabel.Text = GetResource(CarKey);
				buttonOK.Text = GetResource(OkButtonKey);
				myFuelConsumptionIsLabel.Text = GetResource(FuelConsumptionKey);
				displayFuelConsumptionErrorLabel.Text = GetResource(FuelConsumptionErrorKey);
//>>>v1.2 changes (preserve index selections)
                int indexListCarSize                    = listCarSize.SelectedIndex;
                int indexListListFuelType               = listFuelType.SelectedIndex;
                int indexListFuelConsumptionUnit        = listFuelConsumptionUnit.SelectedIndex;
                int indexListFuelConsumptionSelectRadio = fuelConsumptionSelectRadio.SelectedIndex;

				populator.LoadListControl(DataServiceType.ListCarSizeDrop, listCarSize);
				populator.LoadListControl(DataServiceType.ListFuelTypeDrop, listFuelType);
				populator.LoadListControl(DataServiceType.FuelConsumptionUnitDrop, listFuelConsumptionUnit);
				populator.LoadListControl(DataServiceType.FuelConsumptionSelectRadio, fuelConsumptionSelectRadio);

                listCarSize.SelectedIndex                = indexListCarSize;
                listFuelType.SelectedIndex               =indexListListFuelType;
                listFuelConsumptionUnit.SelectedIndex    = indexListFuelConsumptionUnit;
                fuelConsumptionSelectRadio.SelectedIndex =indexListFuelConsumptionSelectRadio;

//<<<  v1.2 changes





				iHaveaLabel.Visible = true;
				listCarSize.Visible = true;
				sizedLabel.Visible = true;
				listFuelType.Visible = true;
				carLabel.Visible = true;

				// Set Fuel Consumption Option Mode
				myFuelConsumptionIsLabel.Visible = true;
				fuelConsumptionSelectRadio.Visible = true;
				textFuelConsumption.Visible = true;
				listFuelConsumptionUnit.Visible = true;
				displayFuelConsumptionErrorLabel.Visible = false;
		}

		/// <summary>
		/// Translates from CO2 g per km to MPG
		/// </summary>
		/// <param name="CO2Value">CO2 value as string</param>
		/// <returns>CO2 value converted to MPG</returns>
		private string ConvertCO2toMPG(string CO2Value, string fuelType )
		{
			CarCostCalculator calculator = new CarCostCalculator();

			// fuel factor is stored as 234 (for petrol), but we need it as 2.34 kg/l
			double fuelFactor = calculator.GetFuelFactor(fuelType);
			fuelFactor = fuelFactor / 100;			

			// Convert grams entered to Kg
			double fuelConsumption = MeasureConvert.ToDouble(CO2Value) / 1000; 
					
			// Convert Kg/L to Km/L by dividing by Kg/Km
			fuelConsumption = fuelFactor / fuelConsumption;

			// Convert Km/L to MPG 
			fuelConsumption = fuelConsumption * 2.8248105347;

			// Round so that if user returns to Journey emissions page we dont show a value with 10 dp 
			// in the MPG input text box
			decimal roundedFuelConsuption = decimal.Round(Convert.ToDecimal(fuelConsumption),2);

			return roundedFuelConsuption.ToString();
		}

		/// <summary>
		/// Validates entered fuel consumption 
		/// </summary>
		/// <returns>True if entered data is valid</returns>
		private bool ValidateFuelConsumption()
		{
			CarCostingFuelValidationHelper FuelValidation = new CarCostingFuelValidationHelper();

			if (FuelConsumptionOption)
				this.FuelConsumptionValid = true;
			else
				this.FuelConsumptionValid = 
					FuelValidation.ValidateFuelConsumption(FuelConsumptionText, FuelConsumptionUnit);

			return this.FuelConsumptionValid;
		}

		/// <summary>
		/// Displays the error message if invalid fuel consumption entered
		/// </summary>
		private void DisplayErrorMessage()
		{
			textFuelConsumption.CssClass ="alertboxerror";				
			displayFuelConsumptionErrorLabel.Visible = true;
			if (listFuelConsumptionUnit.SelectedIndex == 0)
				displayFuelConsumptionErrorLabel.Text = string.Format(
					GetResource(FuelConsumptionMPGErrorKey), Properties.Current[ "CarCosting.MinFuelConsumption" ], Properties.Current["CarCosting.MaxConsumptionMilesPerGallon"]);
			else
				displayFuelConsumptionErrorLabel.Text = string.Format(
					GetResource(FuelConsumptionLKMErrorKey), Properties.Current[ "CarCosting.MaxConsumptionLitresPer100Km" ]);

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

		#endregion


		#region Private Event handlers

		/// <summary>
		/// Event handler to handle the Ok button click.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (ValidateFuelConsumption())
			{
				journeyParameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
				journeyParameters.CarSize = this.CarSize;
				journeyParameters.CarFuelType = this.FuelType;
				journeyParameters.FuelConsumptionOption = this.FuelConsumptionOption;
				journeyParameters.FuelConsumptionEntered = this.FuelConsumptionValue;
				journeyParameters.FuelConsumptionUnit = this.FuelConsumptionUnit;

				TDSessionManager.Current.JourneyParameters = journeyParameters as TDJourneyParameters;

				// Journey Plan Runner
				JourneyPlanRunner.IJourneyPlanRunner runner;
				runner = new JourneyPlanRunner.JourneyPlanRunner(Global.tdResourceManager);

				AsyncCallState acs = new JourneyPlanState();
				// Determine refresh interval and resource string for the wait page
				acs.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.AmendDateAndTime"]);
				acs.WaitPageMessageResourceFile = "langStrings";
				acs.WaitPageMessageResourceId = "WaitPageMessage.AmendDateAndTime";

                acs.AmbiguityPage = PageId.JourneyDetails;
                acs.DestinationPage = PageId.JourneyDetails;
                acs.ErrorPage = PageId.JourneyDetails;
				TDSessionManager.Current.AsyncCallState = acs;

				if (runner.ValidateAndRun(TDSessionManager.Current, TDSessionManager.Current.JourneyParameters, TDPage.GetChannelLanguage(TDPage.SessionChannelName)))
				{
					TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.WaitingRefresh;
				}
				else
				{
					// TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(callingPageId);
					// Redirect user to input page.
					if (TDSessionManager.Current.FindAMode == FindAMode.None)
						TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputErrors;
					else
					{
						TDSessionManager.Current.JourneyResult.IsValid = false;
						TDSessionManager.Current.FindPageState.AmbiguityMode = true;
						TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = FindInputAdapter.GetTransitionEventFromMode(TDSessionManager.Current.FindAMode);
					}
				}
				TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
				TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(TransitionEvent.FindAInputRedirectToResults);
			}
			else
			{
				DisplayErrorMessage();
				// Stay on this page
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyEmissions;
			}

		}
		/// <summary>
		/// Handles the Changed event of the car size dropdown and raises
		/// the event on to the client
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnCarSizeOptionChanged(object sender, EventArgs e)
		{
			RaiseEvent(CarSizeChangedEventKey);
		}

		/// <summary>
		/// Handles the Changed event of the Fuel Type dropdown and raises
		/// the event on to the client
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnFuelTypeOptionChanged(object sender, EventArgs e)
		{
			RaiseEvent(FuelTypeChangedEventKey);
		}

		/// <summary>
		/// Handles the Changed event of the Fuel consumption radio button and raises
		/// the event on to the client
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnFuelConsumptionOptionChanged(object sender, EventArgs e)
		{
			RaiseEvent(FuelConsumptionOptionChangedEventKey);
		}

		/// <summary>
		/// Handles the Changed event of the Fuel consumption text and raises
		/// the event on to the client
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnFuelConsumptionTextChanged(object sender, EventArgs e)
		{
			RaiseEvent(FuelConsumptionTextChangedEventKey);
		}

		/// <summary>
		/// Handles the Changed event of the Fuel Consumption unit dropdown and raises
		/// the event on to the client
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnFuelConsumptionUnitChanged(object sender, EventArgs e)
		{
			RaiseEvent(SpecificFuelUseUnitChangedKey);

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

		#endregion


		#region Public properties

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

		public string InputConsumptionText
		{
			get
			{
				return HttpUtility.HtmlDecode( FuelConsumptionValue );
			}
			set
			{
				FuelConsumptionValue = HttpUtility.HtmlEncode(value);
			}
		}

		public string FuelConsumptionText
		{
			get
			{
				return textFuelConsumption.Text;
			}
		}

		/// <summary>
		/// Set/Get property if the default fuel consumption value is to be used or not.
		/// True indicates user has specified a value
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

		/// <summary>
		/// Returns true if the fuel consumption entered is valid
		/// </summary>
		public bool FuelConsumptionValid
		{
			get
			{
				return validFuelConsumption;
			}
			set
			{
				validFuelConsumption=value;
			}
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
	}
}
