#region History
// *********************************************** 
// NAME                 : FindCarPreferencesControl.ascx.cs 
// AUTHOR               : Esther Severn 
// DATE CREATED         : 22/07/2004 
// DESCRIPTION          : Displays /allows input of 
//                        car specific preferences   
//                        for 'Find A ...'. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindCarPreferencesControl.ascx.cs-arc  $ 
//
//   Rev 1.5   Aug 28 2012 10:21:02   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.4   Apr 08 2008 13:50:42   apatel
//set pageoptionscontrol visibility 
//
//   Rev 1.3   Apr 07 2008 12:58:58   mmodi
//Corrected retaining advanced options selected items when control reloads
//Resolution for 4838: Del 10 - Car options not displayed correctly in Car Journey Details
//
//   Rev 1.2   Mar 31 2008 13:20:28   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Jan 30 2008 08:26:00 dsawe
//  Changes made for moving pageOptionsControls inside boxes. removed PageOptionsControls from this user control and 
//  moved it to findcarjourneyoptionscontrol.
//
//   Rev 1.0   Nov 08 2007 13:14:02   mturner
//Initial revision.
//
//   Rev 1.55   Jun 07 2007 15:22:38   mmodi
//Removed flag setting for Preferences Submit button
//Resolution for 4409: Minor Portal Changes
//
//   Rev 1.54   Jun 05 2007 17:27:30   nrankin
//IR4409
//Resolution for 4409: Minor Portal Changes
//
//   Rev 1.53   Jun 05 2007 15:12:30   mmodi
//Forced Submit button on FindPreferencesControl to be displayed always when used on this control
//Resolution for 4409: Minor Portal Changes
//
//   Rev 1.52   Dec 07 2006 14:37:32   build
//Automatically merged from branch for stream4240
//
//   Rev 1.51.1.1   Dec 05 2006 16:17:04   dsawe
//changed displayFuelConsumptionErrorLabel.Text to show max & min values
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.51.1.0   Nov 25 2006 10:23:10   mmodi
//Updated error messages
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.51   Apr 24 2006 15:34:56   halkatib
//Resolution for IR3958. Added logical check for the PageID of Parkandrideinput in the updatechangescontrol method
//
//   Rev 1.50   Apr 19 2006 11:43:22   asinclair
//Added code to support the correct functioning of this control when used on Extend Input page
//Resolution for 3744: DN068 Extend: Problems with 'via' fields on Extend input page
//
//   Rev 1.49   Mar 31 2006 12:47:00   asinclair
//Removed a check for showing Advanced preferences if not in ambiguity mode as no longer needed
//Resolution for 3691: DN068 Extend: Remove option to save user preferences on Extend Input
//
//   Rev 1.48   Feb 23 2006 16:10:58   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.47   Feb 17 2006 14:40:22   aviitanen
//Merge from Del8.0 to 8.1
//
//   Rev 1.46   Nov 17 2005 15:38:50   ralonso
//Hide button fix in Find a car
//Resolution for 3034: UEE - Find a Car - 'Hide' button fault
//
//   Rev 1.45   Nov 08 2005 19:33:00   AViitanen
//HTML button changes
//
//   Rev 1.43   Nov 07 2005 15:27:42   rgreenwood
//Manual merge for stream2816
//
//   Rev 1.42   Jun 30 2005 15:48:40   pcross
//Handling sentence with format "I have a large diesel engined car".
//Note that this is not handled properly as far as placement of list boxes on data entry - only on showing the sentence as text in a label.
//Resolution for 2367: DEL 7 Welsh text missing
//
//   Rev 1.41   May 19 2005 12:17:08   ralavi
//Modifications as the result of FXCop.
//
//   Rev 1.40   May 04 2005 15:59:32   Ralavi
//Making sure car size and fuel type are displayed in lower case.
//
//   Rev 1.39   Apr 28 2005 17:56:52   rgeraghty
//Added code to hide the new panelDivHider depending on whether both the panelCarDetails and panelTypeOfJourney are hidden
//Resolution for 2222: Door To Door Location Ambiguity Page Blue Box Display Formatting
//
//   Rev 1.38   Apr 26 2005 09:14:38   Ralavi
//No change.
//
//   Rev 1.37   Apr 20 2005 13:50:14   Ralavi
//Removed "Specifically" text for the fuel cost
//
//   Rev 1.36   Apr 19 2005 16:52:26   esevern
//corrected diesel and petrol costs to display pence to one decimal place
//
//   Rev 1.35   Apr 19 2005 16:02:54   Ralavi
//Added fix for fuel consumption and fuel costs problems and Welsh for drop downs
//
//   Rev 1.34   Apr 13 2005 15:29:38   Ralavi
//changed it back to version 1.32
//
//   Rev 1.32   Apr 13 2005 12:14:48   Ralavi
//Validating fuel consumption and fuel cost and modifying page format
//
//   Rev 1.31   Apr 06 2005 15:39:34   esevern
//sets additional text on fuelcostselectradio default entry (now includes current petrol and diesel prices)
//
//   Rev 1.30   Apr 06 2005 11:30:26   Ralavi
//Changes to ensure fuel consumption unit is cleared when clear button is selected. 
//
//   Rev 1.29   Apr 05 2005 10:08:22   Ralavi
//To ensure Fuel consumption is converted correctly and modifications to format
//
//   Rev 1.28   Apr 01 2005 13:30:16   Ralavi
//Changes to correctly convert fuel consumption
//
//   Rev 1.27   Mar 23 2005 11:53:08   RAlavi
//Making sure car details are displayed in both find a car and door to door
//
//   Rev 1.26   Mar 23 2005 11:17:40   RAlavi
//Changes to allow car size and fuel type to be displayed for door to door.
//
//   Rev 1.25   Mar 22 2005 18:59:58   esevern
//only hide car details display labels if the current page is journeyplanner input
//
//   Rev 1.24   Mar 18 2005 17:25:18   RAlavi
//Car costing fuel consumption and fuel cost values changes
//
//   Rev 1.23   Mar 18 2005 10:12:02   RAlavi
//added code for passing average fuel consumption and fuel cost values to CJP.
//
//   Rev 1.22   Mar 16 2005 11:26:24   RAlavi
//FuelConsumptionTextChanged and FuelCostTextChanged added
//
//   Rev 1.21   Mar 15 2005 18:12:50   esevern
//car size and fuel type now obtained via populator.getValue, not resourceID
//
//   Rev 1.20   Mar 14 2005 14:21:08   RAlavi
//Adding code to display correct labels for public transport and car details in door to door
//
//   Rev 1.19   Mar 08 2005 09:35:12   PNorell
//new functionality for saving preferences.
//Resolution for 1958: DEV CODE Review: Preferences (Car and Public)
//
//   Rev 1.18   Mar 07 2005 11:36:48   RAlavi
//Modifications related to car costing
//
//   Rev 1.17   Mar 04 2005 16:27:18   RAlavi
//Modified doNotUseMotorways
//
//   Rev 1.16   Mar 02 2005 15:24:40   RAlavi
//Changes for ambiguity
//
//   Rev 1.15   Feb 23 2005 16:32:08   RAlavi
//Changed for car costing
//
//   Rev 1.14   Feb 21 2005 11:39:30   esevern
//uncommented drop down list code
//
//   Rev 1.13   Feb 18 2005 17:11:10   esevern
//Car costing - interim working copy checked in for code integration
//
//   Rev 1.12   Jan 28 2005 18:43:28   ralavi
//Updated for car costing
//
//   Rev 1.11   Nov 18 2004 09:59:56   SWillcock
//Updated logic to display preferences panel
//Resolution for 1562: Find A input page in ambiguity mode always shows travel details
//
//   Rev 1.10   Oct 28 2004 10:46:56   esevern
//corrected setting of TDCultureInfo to use CurrentUICulture
//
//   Rev 1.9   Oct 01 2004 11:03:46   COwczarek
//Make behaviour consistent for all Find A input pages when in ambiguity mode.
//Resolution for 1562: Find A input page in ambiguity mode always shows travel details
//
//   Rev 1.8   Sep 17 2004 12:06:38   rgeraghty
//Show help button for Confirming avoid road
//
//   Rev 1.7   Aug 26 2004 16:46:34   COwczarek
//Changes to display journey preferences consistently in read only mode across all Find A pages
//Resolution for 1421: Find a ambiguity pages (QA)
//
//   Rev 1.6   Aug 26 2004 09:32:06   passuied
//Removal of extra label
//Resolution for 1442: Find A car : Car Travel details label discrepancies
//
//   Rev 1.5   Aug 19 2004 10:38:00   passuied
//Fixed wrong use of DataServiceType enum in UpdateControls.
//Resolution for 1344: Cannot load preferences on Find A car page
//
//   Rev 1.4   Aug 16 2004 10:50:08   esevern
//added help for journey type and avoid road
//
//   Rev 1.3   Aug 11 2004 14:02:38   esevern
//added help label and alt text
//
//   Rev 1.2   Aug 02 2004 15:37:26   passuied
//working verson of FindCarInput page + changes to some adapters, controls...
//
//   Rev 1.1   Jul 27 2004 10:55:32   esevern
//added controls to allow display/input of car preferences for journey
//
//   Rev 1.0   Jul 22 2004 15:12:34   esevern
//Initial revision.
#endregion 

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
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.Common.PropertyService.Properties;
	using TransportDirect.JourneyPlanning.CJPInterface;
	using TransportDirect.Common;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.JourneyControl;



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
	public partial  class FindCarPreferencesControl : TDUserControl
	{
		
		#region controls
        // moved PageOptionsControl from this user control to findcarjourneyoptionscontrol
		//protected FindPageOptionsControl pageOptionsControl;
		protected FindPreferencesOptionsControl preferencesOptionsControl;
		protected FindCarJourneyOptionsControl journeyOptionsControl;
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


        #region page init, load, render

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
            //White Labeling - modified to handle FindCarJourneyOptionsControl's PageOptionsControl events.
			journeyOptionsControl.PageOptionsControl.ShowAdvancedOptions += new EventHandler(this.OnShowCarDetails);
            journeyOptionsControl.PageOptionsControl.HideAdvancedOptions += new EventHandler(this.OnHideCarDetails);
			
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

            #region Load lists
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
            #endregion

            UpdateControls();

            #region Load resources
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
            CarCostCalculator costCalculator = (CarCostCalculator)TDServiceDiscovery.Current[ ServiceDiscoveryKey.CarCostCalculator ];
			string petrolCost = ((Convert.ToDecimal(costCalculator.GetFuelCost("petrol")))/10).ToString();
			string dieselCost = ((Convert.ToDecimal(costCalculator.GetFuelCost("diesel")))/10).ToString(); 
            averageFuelCost.Text = averageFuelCost.Text + " " + 
                petrolCost + " " + GetResource(PETROL_PER_LITRE) + " " +
                dieselCost + " " + GetResource(DIESEL_PER_LITRE) + " ";

			textFuelConsumption.CssClass ="";
			textFuelCost.CssClass ="";
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

            journeyOptionsControl.PageOptionsControl.AllowBack = false;
            journeyOptionsControl.PageOptionsControl.AllowShowAdvancedOptions = !AmbiguityMode && !PreferencesVisible;
            journeyOptionsControl.PageOptionsControl.AllowHideAdvancedOptions = !AmbiguityMode && PreferencesVisible;

            if (!journeyOptionsControl.PageOptionsControl.AllowShowAdvancedOptions)
			{
				preferencesOptionsControl.LabelPreferencesHeader.Text= resourceManager.GetString(String.Format(PreferencesCarHeaderLabelTextKey, this.PageId.ToString()), TDCultureInfo.CurrentUICulture);
			}
		}
		#endregion

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


		
		#endregion



		#region public properties

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
		/// </summary>
		public FindPageOptionsControl PageOptionsControl
		{
            get { return journeyOptionsControl.PageOptionsControl; }
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
		/// Allows access to the FindCarJourneyOptionsControl contained within this control.
		/// </summary>
		public FindCarJourneyOptionsControl JourneyOptionsControl
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
		/// <summary>
		/// Gets/sets if motorways should not be used
		/// </summary>
		public bool DoNotUseMotorways
		{
			get
			{
				return this.doNotUseMotorwaysCheckBox.Checked;
			}

			set
			{
				doNotUseMotorwaysCheckBox.Checked = value;
			}
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
		/// Read only property returning the journey type display label
		/// used when in ambiguity mode to display the selected journey
		/// type
		/// </summary>
		public Label JourneyTypeDisplayLabel 
		{ 
			get 
			{
				return displayTypeListLabel;
			}
		}

		/// <summary>
		/// Read only property returning the journey type display label
		/// used when in ambiguity mode to display the selected journey
		/// type
		/// </summary>
		public Label SpeedDisplayLabel 
		{ 
			get 
			{
				return displaySpeedListLabel;
			}
		}

		/// <summary>
		/// Read only property returning the car details display label
		/// used when in ambiguity mode to display the car details
		/// </summary>
		public Label CarDetailsLabel 
		{ 
			get 
			{
				return displayCarDetailsLabel;
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
		/// Controls the visibility of the "save preferences" facility
		/// </summary>
		public bool AllowSavePreferences
		{
			get { return loginSaveOption.Visible; }
			set { loginSaveOption.Visible = value; }
		}


		/// <summary>
		/// Read only property which exposes the control's DivHider panel 
		/// The DivHider panel contains the TypeOfJourney and CareDetails panels
		/// </summary>
		public Panel PanelDivHider
		{
			get
			{
				return panelDivHider;
			}
		}

		/// <summary>
		/// Read only property which exposes the control's TypeOfJourney panel 
		/// </summary>
		public Panel PanelTypeOfJourney
		{
			get
			{
				return panelTypeOfJourney;
			}
		}

		/// <summary>
		/// Read only property which exposes the control's car details panel
		/// </summary>
		public Panel PanelCarDetails
		{
			get
			{
				return panelCarDetails;
			}

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

		#endregion

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

	
		
	
	}
}
