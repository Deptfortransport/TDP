// *********************************************** 
// NAME                 : JourneyEmissionsCarInputControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 13/11/2006 
// DESCRIPTION			: Control displaying the input details for a Car used to calculate Emissions
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyEmissionsCarInputControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:21:24   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:26   mturner
//Initial revision.
//
//   Rev 1.11   Apr 03 2007 17:49:18   mmodi
//Updated to ensure correct Fuel radio button is selected when loaded
//Resolution for 4374: CO2: Mpg value is not used when viewing PT Emissions from Car Emissions
//
//   Rev 1.10   Jan 04 2007 14:11:20   mmodi
//Corrected input boxes background colour issue
//Resolution for 4330: CO2: Amend car details panel does not convert CO2 entered
//
//   Rev 1.9   Dec 14 2006 11:10:48   mmodi
//Corrected Radio button select issue, and removed dedundant code
//Resolution for 4316: CO2: Radio button select issue on Journey Emissions page
//
//   Rev 1.8   Dec 06 2006 11:18:34   mmodi
//Set visibility of the Title/Car Images panel, attempting to reduce blue space
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.7   Dec 06 2006 10:57:22   dsawe
//added code for validation radio button list & autopostback
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.6   Dec 05 2006 16:18:36   mmodi
//Reset all input values to default when in Output mode
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.5   Dec 05 2006 15:58:42   dsawe
//changed displayFuelConsumptionErrorLabel.Text to show min & max values
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.4   Dec 04 2006 14:34:56   mmodi
//Set fuel drop down to default to Diesel on the Journey emissions Output state page
//Resolution for 4240: CO2 Emissions
//Resolution for 4283: CO2: Drop downs should default to Small Diesel on the Output page
//
//   Rev 1.3   Nov 26 2006 15:43:10   mmodi
//Added Printable property, improved code
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.2   Nov 25 2006 14:07:30   mmodi
//Updated error keys and initial input values
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.1   Nov 24 2006 11:16:54   mmodi
//Code changes and updates as part of workstream
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.0   Nov 19 2006 10:37:04   mmodi
//Initial revision.
//Resolution for 4240: CO2 Emissions
//

using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///		Summary description for JourneyEmissionsCarInputControl.
	/// </summary>
	public partial class JourneyEmissionsCarInputControl : TDUserControl
	{
		#region Controls


		protected System.Web.UI.WebControls.Label displayCarDetailsLabel;
		protected System.Web.UI.WebControls.Label displayFuelConsumptionLabel;
		protected System.Web.UI.WebControls.Label displayFuelCostLabel;

		//images

		#endregion

		private bool indexChanged = false;

		#region Constants
		// Keys used to obtain strings from the resource file
		private const string IHaveaKey = "FindCarPreferencesControl.IHaveA";
		private const string SizedKey = "FindCarPreferencesControl.Sized";
		private const string CarKey = "FindCarPreferencesControl.Car";
		private const string FormatCarTypeSentenceKey = "FindCarPreferencesControl.FormatCarTypeSentence";
		private const string FuelConsumptionKey = "JourneyEmissionsCarInputControl.FuelConsumption";
		private const string AverageForCarKey = "FindCarPreferencesControl.AverageForMyCar";
		private const string SpecificConsumptionKey = "FindCarPreferencesControl.SpecificConsumption";
		private const string FuelCostKey = "JourneyEmissionsCarInputControl.FuelCost";
		private const string DisplayFuelCostKey = "FindCarPreferencesControl.DisplayFuelCost";
		private const string SpecificFuelCostKey = "FindCarPreferencesControl.SpecificFuelCost";
		private const string PencePerLitreKey = "FindCarPreferencesControl.PencePerLitre";
		private const string FuelConsumptionMPGErrorKey = "FindCarPreferencesControl.FuelConsumptionMPGErrorKey";
		private const string FuelConsumptionLKMErrorKey = "FindCarPreferencesControl.FuelConsumptionLKMErrorKey";
		private const string FuelConsumptionCO2ErrorKey = "JourneyEmissionsCarInputControl.FuelConsumptionCO2ErrorKey";
		private const string FuelCostErrorKey = "FindCarPreferencesControl.FuelCostErrorKey";
		private const string PETROL_PER_LITRE = "JourneyEmissionsCarInputControl.PetrolPerLitre";
		private const string DIESEL_PER_LITRE = "FindCarPreferencesControl.DieselPerLitre";

		private const string TxtSeven = "txtseven";
		private const string TxtSevenB = "txtsevenb";
		private const string TxtSevenR = "txtsevenr";
        		
		#endregion

		#region Private variables

		// Page level variables
		private IDataServices populator;

		private bool printable = false;

		private bool validFuelConsumption = true;
		private bool validFuelCost = true;

		private bool preferencesChanged;

		private bool carMoreDetailsVisible;
		private bool carImagesVisible;

		private static readonly object PreferencesVisibleChangedEventKey = new object();
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
				
		#endregion

		#region Page_Init, Page_Load, Page_Prerender

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
		} 

		/// <summary>
		/// Populates drop down lists and sets label text
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            int indexListCarSize = listCarSize.SelectedIndex;
            int indexListFuelType = listFuelType.SelectedIndex;
            int indexListFuelConsumptionUnit = listFuelConsumptionUnit.SelectedIndex;
            int indexFuelConsumptionSelectRadio = fuelConsumptionSelectRadio.SelectedIndex;
            int indexFuelCostSelectRadio = fuelCostSelectRadio.SelectedIndex;

            // Load strings from the languages file
            carInputDetailsSubHeading.Text = GetResource("JourneyEmissionsCarInputControl.carInputDetailsSubHeading.Text");
            sizedLabel.Text = GetResource(SizedKey);
            carLabel.Text = GetResource(CarKey);
            myFuelConsumptionIsLabel.Text = GetResource(FuelConsumptionKey);
            myFuelCostIsLabel.Text = GetResource(FuelCostKey);
            pencePerLitreLabel.Text = GetResource(PencePerLitreKey);
            displayFuelConsumptionErrorLabel.Text = GetResource(FuelConsumptionMPGErrorKey);
            displayFuelCostErrorLabel.Text = GetResource(FuelCostErrorKey);
               
                          
				// Load dropdown info
				populator.LoadListControl(DataServiceType.ListCarSizeDrop, listCarSize);
				populator.LoadListControl(DataServiceType.ListFuelTypeDrop, listFuelType);
				populator.LoadListControl(DataServiceType.FuelConsumptionUnitDrop, listFuelConsumptionUnit);
				populator.LoadListControl(DataServiceType.FuelConsumptionCO2SelectRadio, fuelConsumptionSelectRadio);
				populator.LoadListControl(DataServiceType.FuelCostSelectRadio, fuelCostSelectRadio);

            
                if (!(indexListCarSize < 0))
                    listCarSize.SelectedIndex = indexListCarSize;

                if (!(indexListFuelType < 0))
                listFuelType.SelectedIndex = indexListFuelType;

                if (!(indexListFuelConsumptionUnit < 0))
                listFuelConsumptionUnit.SelectedIndex = indexListFuelConsumptionUnit;
                
                if (!(indexFuelConsumptionSelectRadio < 0))
                fuelConsumptionSelectRadio.SelectedIndex = indexFuelConsumptionSelectRadio;

                if (!(indexFuelCostSelectRadio <0))
                fuelCostSelectRadio.SelectedIndex = indexFuelCostSelectRadio;
            
                if ((!IsPostBack))
                {
               
                  // add the current petrol and diesel cost to 'current average cost: '
                    ListItem averageFuelCost = fuelCostSelectRadio.SelectedItem;
                    CarCostCalculator costCalculator = (CarCostCalculator)TDServiceDiscovery.Current[ServiceDiscoveryKey.CarCostCalculator];
                    string petrolCost = ((Convert.ToDecimal(costCalculator.GetFuelCost("petrol"))) / 10).ToString();
                    string dieselCost = ((Convert.ToDecimal(costCalculator.GetFuelCost("diesel"))) / 10).ToString();
                    averageFuelCost.Text = averageFuelCost.Text + " " +
                        petrolCost + " " + GetResource(PETROL_PER_LITRE) + " " +
                        dieselCost + " " + GetResource(DIESEL_PER_LITRE) + " ";


                    PopulateCarPreferences();
                }

			textFuelConsumption.CssClass ="";
			textCO2Consumption.CssClass = "";
			textFuelCost.CssClass ="";
		}

		/// <summary>
		/// Sets visibility of controls according to the property values
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			UpdateChangesControl();
			PopulateImages();
			
			// Set heading labels text and visibility
			if ((TDSessionManager.Current.JourneyEmissionsPageState.JourneyEmissionsState == JourneyEmissionsState.Input) 
				|| (TDSessionManager.Current.JourneyEmissionsPageState.JourneyEmissionsState == JourneyEmissionsState.InputDetails))
			{
				carInputDetailsTitle.Text = GetResource("JourneyEmissionsControl.Title.Text");
				iHaveaLabel.Text = GetResource(IHaveaKey);
			}
			else
			{
				carInputDetailsTitle.Text = GetResource("JourneyEmissionsCarInputControl.carInputDetailsTitle.Compare.Text");
				iHaveaLabel.Text = GetResource("JourneyEmissionsCarInputControl.CompareMyCar.Text");

				panelFirstRow.Visible = false;

				panelCarImages.Visible = false;
				carInputDetailsSubHeading.Visible = false;
			}


			// Set the Car size drop down to Small, and Fuel type to Diesel selected when we're in the Output state only
			// Also set all radio button options to default, removing any input values previously entered
			if ((TDSessionManager.Current.JourneyEmissionsPageState.JourneyEmissionsState == JourneyEmissionsState.Output) 
				|| (TDSessionManager.Current.JourneyEmissionsPageState.JourneyEmissionsState == JourneyEmissionsState.OutputDetails))
			{
				if (indexChanged)
				{
					EnableTextBoxes();				
				}
				else
				{
					// If valid, then the user has entered Output state for the first time, 
					// therefore default the controls.
					// If it is not valid, then the user was already on the Output state but
					// has submitted an invalid value. This condition therefore prevents the input controls 
					// being reset to their default state, preserving any invalid values and the radio button selected
					if (TDSessionManager.Current.JourneyEmissionsPageState.FuelConsumptionValid)
					{
						listCarSize.SelectedIndex = 0;
						listFuelType.SelectedIndex = 1;
						fuelConsumptionSelectRadio.SelectedIndex = 0;

						// Clear values in all input text boxes
						textFuelConsumption.Text = string.Empty;
						textCO2Consumption.Text = string.Empty;

						listFuelConsumptionUnit.SelectedIndex = 0;
						listFuelConsumptionUnit.Enabled = false;

						textFuelConsumption.Enabled = false;
						textCO2Consumption.Enabled = false;

						this.textFuelConsumption.BackColor = Color.LightGray;
						this.textCO2Consumption.BackColor = Color.LightGray;
					}

					if (TDSessionManager.Current.JourneyEmissionsPageState.FuelCostValid)
					{
						fuelCostSelectRadio.SelectedIndex = 0;

						// Clear values in all input text boxes
						textFuelCost.Text = string.Empty;				
						textFuelCost.Enabled = false;
						textFuelCost.BackColor = Color.LightGray;
					}
				}
			}		
			else
			{
				EnableTextBoxes();
			}
		}


		#endregion

		#region Private methods

		private void EnableTextBoxes()
		{

			switch (fuelConsumptionSelectRadio.SelectedIndex)
			{
				case 0:
				{
					
					this.textFuelConsumption.Enabled = false;
					this.textCO2Consumption.Enabled = false;
					this.listFuelConsumptionUnit.Enabled = false;

					this.textFuelConsumption.BackColor = Color.LightGray;
					this.textCO2Consumption.BackColor = Color.LightGray;

					this.textFuelConsumption.CssClass = "";
					this.textCO2Consumption.CssClass ="";
				}
					break;

				case 1:
				{
					this.textFuelConsumption.Enabled = true;
					this.listFuelConsumptionUnit.Enabled = true;
					this.textCO2Consumption.Enabled = false;

					this.textCO2Consumption.BackColor = Color.LightGray;
					this.textFuelConsumption.BackColor = Color.White;

					this.textCO2Consumption.CssClass ="";
				}
					break;

				case 2:
				{
					this.textCO2Consumption.Enabled = true;
					this.textFuelConsumption.Enabled = false;
					this.listFuelConsumptionUnit.Enabled = false;

					this.textCO2Consumption.BackColor = Color.White;
					this.textFuelConsumption.BackColor = Color.LightGray;

					this.textFuelConsumption.CssClass = "";
				}
					break;
			}

			switch (fuelCostSelectRadio.SelectedIndex)
			{
				case 0:
				{
					textFuelCost.Enabled = false;

					textFuelCost.BackColor = Color.LightGray;
					this.textFuelCost.CssClass = "";
				}
					break;

				case 1:
				{
					textFuelCost.Enabled = true;
					textFuelCost.BackColor = Color.White;
				}
					break;
			}

			indexChanged = false;
		}


		/// <summary>
		/// Populates car preferences stored within the JourneyEmissionsPageState
		/// </summary>
		private void PopulateCarPreferences()
		{
			// If advanced options orginally entered then we want to display then first time JourneyEmissions page is loaded
			JourneyEmissionsPageState emissionsPageState = TDSessionManager.Current.JourneyEmissionsPageState;

			this.CarSize = emissionsPageState.CarSize;
			this.FuelType = emissionsPageState.CarFuelType;

			// False indicates user has specified a fuel consumption
			if (!emissionsPageState.FuelConsumptionOption)
			{
				carMoreDetailsVisible = true;

				this.FuelConsumptionOption = false;
				this.FuelConsumptionUnit = emissionsPageState.FuelConsumptionUnit;
				if (emissionsPageState.FuelConsumptionUnit == 3)
				{
					textCO2Consumption.Text = emissionsPageState.FuelConsumptionEntered;
					textFuelConsumption.Enabled = false;
					listFuelConsumptionUnit.Enabled = false;
					textCO2Consumption.Enabled = true;
					// ensure correct radio button is selected
					fuelConsumptionSelectRadio.SelectedIndex = 2;
				}
				else
				{
					textFuelConsumption.Text = emissionsPageState.FuelConsumptionEntered;
					textFuelConsumption.Enabled = true;
					listFuelConsumptionUnit.Enabled = true;
					textCO2Consumption.Enabled = false;
				}				
			}
			else
			{
				textFuelConsumption.Enabled = false;
				listFuelConsumptionUnit.Enabled = false;
				textCO2Consumption.Enabled = false;
			}

			// False indicates user has specified a fuel cost
			if (!emissionsPageState.FuelCostOption)
			{
				carMoreDetailsVisible = true;

				textFuelCost.Text = emissionsPageState.FuelCostEntered;
				this.FuelCostOption = false;
		
				textFuelCost.Enabled = true;
			}
			else
			{
				textFuelCost.Enabled = false;
			}
		}

		/// <summary>
		/// Sets the state of the changes controls.  NB: the display label text should be 
		/// set from the container page (requires journey parameter values entered by user)
		/// </summary>
		private void UpdateChangesControl()
		{
			// Note: This is a simplified setting of values, compared to the same method in FindCarPreferencesControl
			// because all the text and entry fields are shown within the JourneyEmissions page.
			// We only need to control the visibility of the validation error messages.

			panelCarDetails.Visible = true;
			panelCarDetailsMore.Visible = carMoreDetailsVisible;

			// Set Car size values
			iHaveaLabel.Visible = true;
			listCarSize.Visible = true;
			sizedLabel.Visible = true;
			listFuelType.Visible = true;
			carLabel.Visible = true;

			// Set Fuel Consumption Option Mode
			myFuelConsumptionIsLabel.Visible = carMoreDetailsVisible;
			fuelConsumptionSelectRadio.Visible = carMoreDetailsVisible;
			textFuelConsumption.Visible = carMoreDetailsVisible;
			textCO2Consumption.Visible = carMoreDetailsVisible;
			listFuelConsumptionUnit.Visible = carMoreDetailsVisible;
			displayFuelConsumptionErrorLabel.Visible = false;

			// Set Fuel Cost Option Mode
			myFuelCostIsLabel.Visible = carMoreDetailsVisible;
			fuelCostSelectRadio.Visible = carMoreDetailsVisible;
			textFuelCost.Visible = carMoreDetailsVisible;
			pencePerLitreLabel.Visible = carMoreDetailsVisible;
			displayFuelCostErrorLabel.Visible = false;

			// Set the Validation errors if there are any
			if ((!FuelConsumptionValid) || 
				(!TDSessionManager.Current.JourneyEmissionsPageState.FuelConsumptionValid))
			{	
				// Determine if fuel or Co2 consumption is being used
				if (fuelConsumptionSelectRadio.SelectedIndex == 2)
				{
					textCO2Consumption.CssClass = "alertboxerror";
					displayFuelConsumptionErrorLabel.Text = string.Format(
						GetResource(FuelConsumptionCO2ErrorKey), Properties.Current[ "CarCosting.MaxCO2PerKm" ]);
				}
				else
				{
					textFuelConsumption.CssClass ="alertboxerror";
					// Get the correct error message dependent on fuel units selected
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
				displayFuelConsumptionErrorLabel.Visible = true;
			}

			if ((!FuelCostValid) ||
				(!TDSessionManager.Current.JourneyEmissionsPageState.FuelCostValid))
			{	
				textFuelCost.CssClass ="alertboxerror";				
				displayFuelCostErrorLabel.Visible = true;
				displayFuelCostErrorLabel.Text = string.Format(
					GetResource(FuelCostErrorKey), Properties.Current[ "CarCosting.MaxFuelCost" ]);
			}
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
		/// Populates the images and alt text
		/// </summary>
		private void PopulateImages()
		{
			// Assign images to image controls
			imageSmallCar.ImageUrl = GetResource("JourneyEmissionsCarInputControl.imageSmallCar.ImageUrl");
			imageMediumCar.ImageUrl = GetResource("JourneyEmissionsCarInputControl.imageMediumCar.ImageUrl");
			imageLargeCar.ImageUrl = GetResource("JourneyEmissionsCarInputControl.imageLargeCar.ImageUrl");

			// Assign alternate text
			imageSmallCar.AlternateText = GetResource("JourneyEmissionsCarInputControl.imageSmallCar.AlternateText");
			imageMediumCar.AlternateText = GetResource("JourneyEmissionsCarInputControl.imageMediumCar.AlternateText");
			imageLargeCar.AlternateText = GetResource("JourneyEmissionsCarInputControl.imageLargeCar.AlternateText");

			if (carImagesVisible) 
			{
				panelCarImages.Visible = true;
			}
			else
			{
				panelCarImages.Visible = false;
			}
		}

		#endregion

		#region Private Event handlers


		/// <summary>
		/// handles the selectedindexchanged event of fuelCostSelectRadio
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void fuelCostSelectRadio_SelectedIndexChanged(object sender, EventArgs e)
		{
			indexChanged = true;

			// reset the Valid flag to ensure red border is not reapplied to the input boxes when
			// moving between the options
			TDSessionManager.Current.JourneyEmissionsPageState.FuelCostValid = true;
		}

		/// <summary>
		/// handles the selectedindexchanged event of fuelConsumptionSelectRadio
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void fuelConsumptionSelectRadio_SelectedIndexChanged(object sender, EventArgs e)
		{
			indexChanged = true;

			// reset the Valid flag to ensure red border is not reapplied to the input boxes when
			// moving between the options
			TDSessionManager.Current.JourneyEmissionsPageState.FuelConsumptionValid = true;
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

		/// <summary>
		/// Gets/Sets input fuel consumption text
		/// </summary>
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
				// 3 indicates CO2 Emissions value was entered
				if (FuelConsumptionUnit == 3)
					return textCO2Consumption.Text;
				else
					return textFuelConsumption.Text;
			}
		}

		public string FuelCostText
		{
			get
			{
				return textFuelCost.Text;
			}
		}

		/// <summary>
		/// Gets/Sets input fuel cost text
		/// </summary>
		public string InputCostText
		{
			get
			{
				return HttpUtility.HtmlDecode( FuelCostValue );
			}
			set
			{
				FuelCostValue = HttpUtility.HtmlEncode(value);
			}
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
					textCO2Consumption.Text = string.Empty;
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

				// Indicates user has selected the CO2 fuel consumption option, so return the appropriate text box value
				if (this.fuelConsumptionSelectRadio.SelectedIndex == 2)
				{
					return textCO2Consumption.Text;
				}

				return textFuelConsumption.Text;
			}

			set
			{
				textFuelConsumption.Text = FuelConsumptionOption ? string.Empty : value;
			}
		}

		/// <summary>
		/// Gets/Sets FuelConsumption unit change in dropdown list
		/// </summary>
		public int FuelConsumptionUnit
		{
			get
			{
				if (this.fuelConsumptionSelectRadio.SelectedIndex == 2)
					return 3;
				else
				{
					string itemValue = populator.GetValue(DataServiceType.FuelConsumptionUnitDrop, listFuelConsumptionUnit.SelectedItem.Value);
					return Convert.ToInt32(itemValue);
				}
			}
			set
			{
				string fuelUseUnitChangeId = populator.GetResourceId(DataServiceType.FuelConsumptionUnitDrop, value.ToString());
			
				populator.Select(listFuelConsumptionUnit, fuelUseUnitChangeId);
			}
		}

		/// <summary>
		/// Read only property returning Fuel Consumption display label
		/// used when in ambiguity mode to display the fuel consumption
		/// details
		/// </summary>
		public Label FuelConsumptionLabel 
		{ 
			get 
			{
				return displayFuelConsumptionLabel;
			}
		}

		/// <summary>
		/// Read only property returning the Fuel Cost display label
		/// used when in ambiguity mode to display the fuel cost details
		/// </summary>
		public Label FuelCostLabel 
		{ 
			get 
			{
				return displayFuelCostLabel;
			}
		}

		/// <summary>
		/// Returns true if the preferences changed
		/// </summary>

		public bool PreferencesChanged
		{
			get 
			{
				return preferencesChanged;
			}

			set
			{
				preferencesChanged=value;
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
		/// Returns true if the fuel cost entered is valid
		/// </summary>
		public bool FuelCostValid
		{
			get
			{
				return validFuelCost;
			}
			set 
			{
				validFuelCost = value;
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

		/// <summary>
		/// Returns true if the chosen fuelCostOption is the radio button default value, false otherwise
		/// </summary>
		/// <returns>true if the chosen fuelCostOption is the radio button default value, false otherwise</returns>
		public bool FuelCostOptionIsDefault() 
		{
			return this.FuelCostOption;
		}


		/// <summary>
		/// Gets/sets the visibility flag for the More details panel of the car park details
		/// </summary>
		public bool CarMoreDetailsVisible
		{
			get { return carMoreDetailsVisible; }
			set { carMoreDetailsVisible = value; }
		}

		/// <summary>
		/// Gets/sets the visibility flag for the More details panel of the car park details
		/// </summary>
		public bool CarImagesVisible
		{
			get { return carImagesVisible; }
			set { carImagesVisible = value; }
		}

		/// <summary>
		/// Get/Set property returning the Gramms of CO2 per Km.
		/// This relies on the fact that the correct Fuel consumption Option (radio button) has
		/// been selected before this is asked, otherwise it will return the Fuel consumption value
		/// </summary>
		public string FuelConsumptionCO2Value
		{
			get
			{	
				CarCostCalculator costCalculator = (CarCostCalculator)TDServiceDiscovery.Current[ ServiceDiscoveryKey.CarCostCalculator ];
				
				if( this.FuelConsumptionOption )
				{
					
					return string.Format(costCalculator.GetFuelConsumption(CarSize, FuelType).ToString());
				}


				return textCO2Consumption.Text;
			}

			set
			{
		
			{
				textCO2Consumption.Text = FuelConsumptionOption ? string.Empty : value;
			}
			}
		}
        	
		/// <summary>
		/// Gets and sets if the control is Printable or not
		/// </summary>
		public bool Printable
		{
			get { return printable; }
			set { printable = value; }
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
            listCarSize.SelectedIndexChanged    += new EventHandler(this.OnCarSizeOptionChanged);
            listFuelType.SelectedIndexChanged   += new EventHandler(this.OnFuelTypeOptionChanged);
            textFuelConsumption.TextChanged     += new EventHandler(this.OnFuelConsumptionTextChanged);
            textFuelCost.TextChanged            += new EventHandler(this.OnFuelCostTextChanged);

            fuelConsumptionSelectRadio.SelectedIndexChanged 
                += new EventHandler(this.fuelConsumptionSelectRadio_SelectedIndexChanged);
            listFuelConsumptionUnit.SelectedIndexChanged
                += new EventHandler(this.OnFuelConsumptionUnitChanged);
            fuelCostSelectRadio.SelectedIndexChanged
                += new EventHandler(this.fuelCostSelectRadio_SelectedIndexChanged);
        }
		#endregion
	}
}
