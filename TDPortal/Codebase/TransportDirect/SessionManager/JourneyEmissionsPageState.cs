// *********************************************** 
// NAME                 : JourneyEmissionsPageState.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/11/2006 
// DESCRIPTION			: Session state for the Journey Emissions page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/JourneyEmissionsPageState.cs-arc  $ 
//
//   Rev 1.4   Feb 22 2010 08:19:32   rbroddle
//Added isInternationalJourney flag
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.3   Feb 16 2010 10:56:58   RBroddle
//Added isInternationalJourney variable
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Mar 10 2008 15:27:08   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:48:32   mturner
//Initial revision.
//
//   Rev 1.10   Aug 31 2007 16:20:00   build
//Automatically merged from branch for stream4474
//
//   Rev 1.9.1.0   Aug 30 2007 17:48:40   asinclair
//Added JourneyEmissionsVisualMode
//Resolution for 4474: DEL 9.7 Stream : Public Transport CO2
//
//   Rev 1.9   Jun 01 2007 11:07:58   mmodi
//Reinstated initialise car size values in the Initialise method
//Resolution for 4437: CO2: View emissions for a replan Car journey causes error
//
//   Rev 1.8   Apr 12 2007 11:12:46   mmodi
//Added properrty to retain value entered by user to display
//Resolution for 4383: CO2: Rounding of Distance on CO2 emissions compare panel
//
//   Rev 1.7   Mar 21 2007 14:18:00   asinclair
//Removed the code setting yourcar values to use the dropdown values
//
//   Rev 1.6   Mar 06 2007 12:28:22   Build
//Automatically merged from branch for stream4350
//
//   Rev 1.5.1.1   Mar 05 2007 17:24:56   mmodi
//Added poperty to hold journey passengers
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.5.1.0   Feb 20 2007 17:16:30   mmodi
//Added properties used for PT emissions
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.5   Jan 03 2007 14:27:16   mmodi
//Added Your Car public properties
//Resolution for 4308: CO2: Find detailed journey costs should replan journey
//
//   Rev 1.4   Dec 15 2006 11:04:22   mmodi
//Updated code to use decimal values for Scale values
//Resolution for 4321: CO2: Use accurate Scales when calculating angle
//
//   Rev 1.3   Nov 26 2006 15:53:56   mmodi
//Set default flags for some variables
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.2   Nov 24 2006 12:14:02   mmodi
//Updates for Journey emissions controls
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.1   Nov 19 2006 10:42:10   mmodi
//Added properties
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.0   Nov 16 2006 17:26:16   mmodi
//Initial revision.
//Resolution for 4240: CO2 Emissions
//

using System;
using System.Data;
using System.Globalization;

using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;

namespace TransportDirect.UserPortal.SessionManager
{
	public enum JourneyEmissionsState
	{
		Input,
		InputDetails,
		Output,
		OutputDetails,
		Compare,
		CompareDetails
	}

	public enum JourneyEmissionsCompareState
	{
		InputDefault,
		InputCompare,
		JourneyDefault,
		JourneyCompare
	}

	public enum JourneyEmissionsCompareMode
	{
		DistanceDefault,
		JourneyDefault,
		JourneyCompare
	}

	public enum JourneyEmissionsVisualMode
	{
		Diagram,
		Table
	}

	/// <summary>
	/// Session state for Journey Emissions
	/// </summary>
	[CLSCompliant(false)]
	[Serializable()]
	public class JourneyEmissionsPageState
	{
		#region Private variables

		private JourneyEmissionsState journeyEmissionsState = JourneyEmissionsState.Input;

		private string fuelCostValue;
		private string emissionsValue;

		private string fuelCostCompareValue;
		private string emissionsCompareValue;

		private decimal fuelCostScaleMax;
		private decimal fuelCostScaleMin;
		private decimal emissionsScaleMax;
		private decimal emissionsScaleMin;

		// Car costing changes
		private bool fuelCostOption = true;
		private string fuelCostEntered;
		private bool fuelConsumptionOption = true;
		private string fuelConsumptionEntered;
		private int fuelConsumptionUnit;
		private string carSize;
		private string carFuelType;
		private int fuelUseUnitChange;
		private bool fuelConsumptionValid = true;
		private bool fuelCostValid = true;

		// "YourCar..." refers to the first set of parameters specified by user on the JourneyEmissions page
		private string yourCarSize;
		private string yourCarFuelType;
		private bool yourCarFuelCostOption = true;
		private string yourCarFuelCostEntered;
		private bool yourCarFuelConsumptionOption = true;
		private int yourCarFuelConsumptionUnit;
		private string yourCarFuelConsumptionEntered;


		// Values used for Public Transport emissions
		private JourneyEmissionsCompareState journeyEmissionsCompareState = JourneyEmissionsCompareState.InputDefault;
		private JourneyEmissionsCompareMode journeyEmissionsCompareMode = JourneyEmissionsCompareMode.DistanceDefault;
		
		private JourneyEmissionsVisualMode journeyEmissionsVisualMode;


		private string journeyDistance;
		private string journeyDistanceToDisplay;
		private bool journeyDistanceValid = true;
		private int journeyDistanceUnit;

		private RoadUnitsEnum units;
		private int carPassenger;
		private int largeCarPassenger;
		private int journeyPassenger;


        private bool landingModeAll;
        private bool landingModeSmallCar;
        private bool landingModeLargeCar;
        private bool landingModeTrain;
        private bool landingModeCoach;
        private bool landingModePlane;

        private bool isLandingPageActive;

        private bool isInternationalJourney;

		#endregion

		#region Constructor
		/// <summary>
		/// Default constructor
		/// </summary>
		public JourneyEmissionsPageState()
		{

		}

		#endregion

		#region Public properties - Journey Emissions

		/// <summary>
		/// Read/Write. State of the JourneyEmissions page of type JourneyEmissionsState
		/// </summary>
		public JourneyEmissionsState JourneyEmissionsState
		{
			get { return journeyEmissionsState; }
			set { journeyEmissionsState = value; }
		}

		/// <summary>
		/// Read/Write. Fuel cost value for JourneyEmissions page,
		/// used to hold calculated Fuel Costs. 
		/// This is NOT the fuel cost entered by user
		/// </summary>
		public string FuelCostValue
		{
			get { return fuelCostValue; }
			set { fuelCostValue = value; }
		}

		/// <summary>
		/// Read/Write. Emissions value for JourneyEmissions page, 
		/// used to hold calculated CO2 Emissions. 
		/// This is NOT the CO2 emissions entered by user
		/// </summary>
		public string EmissionsValue
		{
			get { return emissionsValue; }
			set { emissionsValue = value; }
		}

		/// <summary>
		/// Read/Write. Fuel cost Compare value for JourneyEmissions page,, 
		/// used to hold calculated Fuel Cost Compare value. 
		/// This is NOT the fuel cost entered by user
		/// </summary>
		public string FuelCostCompareValue
		{
			get { return fuelCostCompareValue; }
			set { fuelCostCompareValue = value; }
		}

		/// <summary>
		/// Read/Write. Emission Compare value for JourneyEmissions page,
		/// used to hold calculated Emission Compare value. 
		/// This is NOT the CO2 emissions entered by user
		/// </summary>
		public string EmissionsCompareValue
		{
			get { return emissionsCompareValue; }
			set { emissionsCompareValue = value; }
		}

		/// <summary>
		/// Read/Write. Fuel cost scale Max value, used to set speedo dial max scale
		/// </summary>
		public decimal FuelCostScaleMax
		{
			get { return fuelCostScaleMax; }
			set { fuelCostScaleMax = value; }
		}

		/// <summary>
		/// Read/Write. Fuel cost scale Min value, used to set speedo dial min scale
		/// </summary>
		public decimal FuelCostScaleMin
		{
			get { return fuelCostScaleMin; }
			set { fuelCostScaleMin = value; }
		}

		/// <summary>
		/// Read/Write. Emissions scale Max value, used to set speedo dial max scale
		/// </summary>
		public decimal EmissionsScaleMax
		{
			get { return emissionsScaleMax; }
			set { emissionsScaleMax = value; }
		}

		/// <summary>
		/// Read/Write. Emissions scale Min value, used to set speedo dial min scale
		/// </summary>
		public decimal EmissionsScaleMin
		{
			get { return emissionsScaleMin; }
			set { emissionsScaleMin = value; }
		}

		/// <summary>
		/// Read/Write. The status of the fuel cost option. Has the user accepted
		/// the default fuel price or entered their own. False indicates the users
		/// own value has been entered.
		/// </summary>
		public bool FuelCostOption
		{
			get { return fuelCostOption; }
			set { fuelCostOption = value; }
		}

		/// <summary>
		/// Read/Write. Entered by the user and indicates the cost of the fuel.
		/// </summary>
		public string FuelCostEntered
		{
			get { return fuelCostEntered; }
			set { fuelCostEntered = value; }
		}
		
		/// <summary>
		/// Read/Write. The status of the fuel consumption check box. Has the user
		/// accepted the default fuel consumption (dependant on car/fuel type selected)
		/// or entered their own value for fuel consumption. False indicates user
		/// has specified a value.
		/// </summary>
		public bool FuelConsumptionOption
		{
			get { return fuelConsumptionOption; }
			set { fuelConsumptionOption = value; }
		}

		/// <summary>
		/// Read/Write. The entered fuel consumption value. Unit of measurement
		/// depends on unit of measurement selected by user. Default unit is MPG.
		/// </summary>
		public string FuelConsumptionEntered
		{
			get { return fuelConsumptionEntered; }
			set { fuelConsumptionEntered = value; }
		}

		/// <summary>
		/// Read/Write. Is the fuel consumption entered by the user valid.
		/// False indicates that an invalid entry e.g. "abc" has been entered .
		/// </summary>
		public bool FuelConsumptionValid
		{
			get { return fuelConsumptionValid; }
			set { fuelConsumptionValid = value; }
		}

		/// <summary>
		/// Read/Write. Is the fuel cost entered by the user valid. False 
		/// indicates an invalid cost such as "abc" has been entered.
		/// </summary>
		public bool FuelCostValid
		{
			get { return fuelCostValid; }
			set { fuelCostValid = value; }
		}

		/// <summary>
		/// Read/Write. The selected fuel consumption unit. Numerical respresentation
		/// of either MPG or Ltr/KM etc. Specified in database.
		/// </summary>
		public int FuelConsumptionUnit
		{
			get { return fuelConsumptionUnit; }
			set { fuelConsumptionUnit = value; }
		}

		/// <summary>
		/// Read/Write. Selected car size e.g. "small", "medium" or "large".
		/// </summary>
		public string CarSize
		{
			get { return carSize; }
			set { carSize = value; }
		}

		/// <summary>
		/// Read/Write. Selected fuel type e.g. "petrol" or "diesel".
		/// </summary>
		public string CarFuelType
		{
			get { return carFuelType; }
			set { carFuelType = value; }
		}

		/// <summary>
		/// Read/Write. Fuel Use Unit Change - possibly not used (TCM).
		/// </summary>
		public int FuelUseUnitChange
		{
			get { return fuelUseUnitChange; }
			set { fuelUseUnitChange = value; }
		}

		# region Your Car public properties

		/// <summary>
		/// Read/Write. The status of the Your Car fuel cost option. Has the user accepted
		/// the default fuel price or entered their own. False indicates the users
		/// own value has been entered.
		/// </summary>
		public bool YourCarFuelCostOption
		{
			get { return yourCarFuelCostOption; }
			set { yourCarFuelCostOption = value; }
		}

		/// <summary>
		/// Read/Write. Your Car fuel cost entered by the user, indicates the cost of the fuel.
		/// </summary>
		public string YourCarFuelCostEntered
		{
			get { return yourCarFuelCostEntered; }
			set { yourCarFuelCostEntered = value; }
		}
		
		/// <summary>
		/// Read/Write. The status of the Your Car fuel consumption check box. Has the user
		/// accepted the default fuel consumption (dependant on car/fuel type selected)
		/// or entered their own value for fuel consumption. False indicates user
		/// has specified a value.
		/// </summary>
		public bool YourCarFuelConsumptionOption
		{
			get { return yourCarFuelConsumptionOption; }
			set { yourCarFuelConsumptionOption = value; }
		}

		/// <summary>
		/// Read/Write. The entered Your Car fuel consumption value. Unit of measurement
		/// depends on unit of measurement selected by user. Default unit is MPG.
		/// </summary>
		public string YourCarFuelConsumptionEntered
		{
			get { return yourCarFuelConsumptionEntered; }
			set { yourCarFuelConsumptionEntered = value; }
		}

		/// <summary>
		/// Read/Write. The selected Your Car fuel consumption unit. Numerical respresentation
		/// of either MPG or Ltr/KM etc. Specified in database.
		/// </summary>
		public int YourCarFuelConsumptionUnit
		{
			get { return yourCarFuelConsumptionUnit; }
			set { yourCarFuelConsumptionUnit = value; }
		}

		/// <summary>
		/// Read/Write. Selected Your Car car size e.g. "small", "medium" or "large".
		/// </summary>
		public string YourCarSize
		{
			get { return yourCarSize; }
			set { yourCarSize = value; }
		}

		/// <summary>
		/// Read/Write. Selected Your Car fuel type e.g. "petrol" or "diesel".
		/// </summary>
		public string YourCarFuelType
		{
			get { return yourCarFuelType; }
			set { yourCarFuelType = value; }
		}

		#endregion

        /// <summary>
        /// Read/Write. landing mode value for JourneyEmissionsCompare page,
        /// Thess are the transport modes passed via landing page
        /// </summary>
        public bool LandingModeAll
        {
            get { return landingModeAll; }
            set { landingModeAll = value; }
        }
        public bool LandingModeSmallCar
        {
            get { return landingModeSmallCar; }
            set { landingModeSmallCar = value; }
        }
        public bool LandingModeLargeCar
        {
            get { return landingModeLargeCar; }
            set { landingModeLargeCar = value; }
        }
        public bool LandingModeTrain
        {
            get { return landingModeTrain; }
            set { landingModeTrain = value; }
        }
        public bool LandingModeCoach
        {
            get { return landingModeCoach; }
            set { landingModeCoach = value; }
        }
        public bool LandingModePlane
        {
            get { return landingModePlane; }
            set { landingModePlane = value; }
        }

        /// <summary>
        /// Read/Write. landing active for JourneyEmissionsCompare page,
        /// This is transport mode passed via landing page
        /// </summary>
        public bool IsLandingPageActive
        {
            get { return isLandingPageActive; }
            set { isLandingPageActive = value; }
        }


		#endregion

		#region Public properties - Journey Emissions Compare

		/// <summary>
		/// Read/Write. State of the JourneyEmissionsCompare page of type JourneyEmissionsCompareState
		/// </summary>
		public JourneyEmissionsCompareState JourneyEmissionsCompareState
		{
			get { return journeyEmissionsCompareState; }
			set { journeyEmissionsCompareState = value; }
		}

		public JourneyEmissionsVisualMode JourneyEmissionsVisualMode
		{
			get{ return journeyEmissionsVisualMode; }
			set { journeyEmissionsVisualMode = value; }
		}

		/// <summary>
		/// Read/Write. Journey distance value for JourneyEmissionsCompare page,
		/// used to calculate CO2 emissions for a distance.
		/// This can be the distance entered by a user, or the calculated distance of a planned journey
		/// </summary>
		public string JourneyDistance
		{
			get { return journeyDistance; }
			set { journeyDistance = value; }
		}

		/// <summary>
		/// Read/Write. Journey distance to display for JourneyEmissionsCompare page
		/// This should be the distance entered by the user
		/// </summary>
		public string JourneyDistanceToDisplay
		{
			get { return journeyDistanceToDisplay; }
			set { journeyDistanceToDisplay = value; }
		}

		/// <summary>
		/// Read/Write. The JourneyDistance unit, km or mile, as an int
		/// </summary>
		public int JourneyDistanceUnit
		{
			get { return journeyDistanceUnit; }
			set { journeyDistanceUnit = value; }
		}

		/// <summary>
		/// Read/Write. Used to hold the value of if the JourneyDistance entered is valid
		/// </summary>
		public bool JourneyDistanceValid
		{
			get { return journeyDistanceValid; }
			set { journeyDistanceValid = value; }
		}

        /// <summary>
        /// Read/Write. Used to hold the value of if the Journey being compared is international
        /// </summary>
        public bool IsInternationalJourney
        {
            get { return isInternationalJourney; }
            set { isInternationalJourney = value; }
        }

		/// <summary>
		/// Read/Write. Road units selected on the JourneyEmissionsCompareControl
		/// </summary>
		public  RoadUnitsEnum Units 
		{
			get { return units; }
			set	{ units = value; }
		}

		/// <summary>
		/// Read/Write. Used to hold the number of Car Passengers selected
		/// </summary>
		public int CarPassenger 
		{
			get { return carPassenger; }
			set	{ carPassenger = value; }
		}

		
		/// <summary>
		/// Read/Write. Used to hold the number of Car Passengers selected
		/// </summary>
		public int LargeCarPassenger 
		{
			get { return largeCarPassenger; }
			set	{ largeCarPassenger = value; }
		}

		/// <summary>
		/// Read/Write. Used to hold the number of Journey Passengers selected
		/// </summary>
		public  int JourneyPassenger 
		{
			get { return journeyPassenger; }
			set	{ journeyPassenger = value; }
		}

		/// <summary>
		/// Read/Write. State of the JourneyEmissionsCompareControl of type JourneyEmissionsCompareMode
		/// </summary>
		public JourneyEmissionsCompareMode JourneyEmissionsCompareMode
		{
			get { return journeyEmissionsCompareMode; }
			set { journeyEmissionsCompareMode = value; }
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Initialises the Journey emissions page state, setting all values to their defaults;
		/// </summary>
		public void Initialise()
		{

			journeyEmissionsState = JourneyEmissionsState.Input;

			fuelCostValue = string.Empty;
			emissionsValue = string.Empty;

			fuelCostCompareValue = null;
			emissionsCompareValue = null;

			fuelCostScaleMax = 0;
			fuelCostScaleMin = 0;
			emissionsScaleMax = 0;
			emissionsScaleMin = 0;

			// journey emissions compare values
			journeyEmissionsCompareState = JourneyEmissionsCompareState.InputDefault;
			journeyDistanceValid = true;
			journeyDistanceUnit = 1;
			journeyDistance = null;
			journeyDistanceToDisplay = String.Empty;
			carPassenger = 0;
			largeCarPassenger = 0;
			journeyPassenger = 0;

			InitialiseGeneric();

            isInternationalJourney = false;
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Initialisation common to all initialisation methods
		/// </summary>
		private void InitialiseGeneric()
		{
			// Default is from the data services
			DataServices.IDataServices populator = (DataServices.IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
					
			carSize = populator.GetDefaultListControlValue(DataServiceType.ListCarSizeDrop);
			carFuelType = populator.GetDefaultListControlValue(DataServiceType.ListFuelTypeDrop);

			// Fuel consumption option and cost option are always set to true, because default setting is always used primarily
			fuelConsumptionOption = true; 
			fuelCostOption = true;
			fuelConsumptionValid = true;
			fuelCostValid = true;
			fuelUseUnitChange = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.UnitsDrop), CultureInfo.CurrentCulture);

			yourCarSize = populator.GetDefaultListControlValue(DataServiceType.ListCarSizeDrop);
			yourCarFuelType = populator.GetDefaultListControlValue(DataServiceType.ListFuelTypeDrop);
			yourCarFuelCostOption = true;
			yourCarFuelConsumptionOption = true;

            landingModeAll = true;
            landingModeSmallCar = false;
            landingModeLargeCar = false;
            landingModeTrain = false;
            landingModeCoach = false;
            landingModePlane = false;
            isLandingPageActive = false;

		}

		#endregion
	}
}
