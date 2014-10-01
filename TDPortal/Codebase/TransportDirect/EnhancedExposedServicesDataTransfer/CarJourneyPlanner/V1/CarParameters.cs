// *********************************************** 
// NAME                 : CarParameters.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Class that represents journey planning parameters for a car journey
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/CarParameters.cs-arc  $
//
//   Rev 1.1   Mar 14 2011 15:11:48   RPhilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.0   Aug 04 2009 14:41:14   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1
{
    /// <summary>
    /// Class that represents journey planning parameters for a car journey
    /// </summary>
    [System.Serializable]
    public class CarParameters
    {
        private CarAlgorithmType algorithm;
        private CarSizeType carSizeType;
        private FuelType fuelType;
        private int maxSpeed;
        private int fuelConsumption;
        private FuelConsumptionUnit fuelConsumptionUnit;
        private double fuelCost;
        private bool banMotorway;
        private bool avoidToll;
        private bool avoidFerries;
        private bool avoidMotorway;
        private bool banLimitedAccess;
        private string[] avoidRoads;
        private string[] useRoads;

        // Properties to allow user to optionally include above properties in the request
        private bool algorithmSpecified;
        private bool carSizeTypeSpecified;
        private bool fuelTypeSpecified;
        private bool maxSpeedSpecified;
        private bool fuelConsumptionSpecified;
        private bool fuelConsumptionUnitSpecified;
        private bool fuelCostSpecified;
        private bool banMotorwaySpecified;
        private bool avoidTollSpecified;
        private bool avoidFerriesSpecified;
        private bool banLimitedAccessSpecified;    
        private bool avoidMotorwaySpecified;
        private bool avoidRoadsSpecified;
        private bool useRoadsSpecified;

        /// <summary>
        /// Constructor
        /// </summary>
        public CarParameters()
        {
        }

        #region Properties

        /// <summary>
        /// The Car journey planning algorithm to use: Fastest, Shortest, MostEconomical, Cheapest
        /// </summary>
        public CarAlgorithmType Algorithm
        {
            get { return algorithm; }
            set { algorithm = value; }
        }

        /// <summary>
        /// The Car size type is used in determining the fuel consumption value: Small, Medium, Large
        /// </summary>
        public CarSizeType CarSizeType
        {
            get { return carSizeType; }
            set { carSizeType = value; }
        }

        /// <summary>
        /// The Fuel type is used in determining the fuel consumption value: Petrol, Diesel
        /// </summary>
        public FuelType FuelType
        {
            get { return fuelType; }
            set { fuelType = value; }
        }

        /// <summary>
        /// The maximum speed for the car the journey planner should use, in miles per hour
        /// </summary>
        public int MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }

        /// <summary>
        /// The fuel consumption value of the car the journey planner should use. 
        /// This value is automatically calculated using the CarType and FuelType values. 
        /// If this is populated, then this overrides the calculated value, 
        /// e.g 50 Miles per gallon, or 50 Litres per 100 km
        /// </summary>
        public int FuelConsumption
        {
            get { return fuelConsumption; }
            set { fuelConsumption = value; }
        }

        /// <summary>
        /// The fuel consumption unit if a FuelConsumptionValue is specified: MilesPerGallon, LitresPer100Km
        /// </summary>
        public FuelConsumptionUnit FuelConsumptionUnit
        {
            get { return fuelConsumptionUnit; }
            set { fuelConsumptionUnit = value; }
        }

        /// <summary>
        /// The fuel cost value which is used in providing an overall cost to the journey. 
        /// This value is automatically set using the FuelType.
        /// If this value is populated, the FuelType value is ignored, e.g. 102.8 pence per litre
        /// </summary>
        public double FuelCost
        {
            get { return fuelCost; }
            set { fuelCost = value; }
        }

        /// <summary>
        /// Flag indicating if the car journey planner must not use motorways
        /// </summary>
        public bool BanMotorway
        {
            get { return banMotorway; }
            set { banMotorway = value; }
        }

        /// <summary>
        /// Flag indicating if the car journey planner should attempt to avoid toll roads
        /// </summary>
        public bool AvoidToll
        {
            get { return avoidToll; }
            set { avoidToll = value; }
        }

        /// <summary>
        /// Flag indicating if the car journey planner should attempt to avoid unknown restricted access
        /// </summary>
        public bool BanLimitedAccess
        {
            get { return banLimitedAccess; }
            set { banLimitedAccess = value; }
        }
        
        /// <summary>
        /// Flag indicating if the car journey planner should attempt to avoid ferries
        /// </summary>
        public bool AvoidFerries
        {
            get { return avoidFerries; }
            set { avoidFerries = value; }
        }

        /// <summary>
        /// Flag indicating if the car journey planner should attempt to avoid motorways
        /// </summary>
        public bool AvoidMotorway
        {
            get { return avoidMotorway; }
            set { avoidMotorway = value; }
        }

        /// <summary>
        /// A list of roads the car journey planner should avoid
        /// </summary>
        public string[] AvoidRoads
        {
            get { return avoidRoads; }
            set { avoidRoads = value; }
        }

        /// <summary>
        /// A list of roads the car journey planner should use
        /// </summary>
        public string[] UseRoads
        {
            get { return useRoads; }
            set { useRoads = value; }
        }

        #endregion

        #region Properties Internal (optional)
        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AlgorithmSpecified
        {
            get { return algorithmSpecified; }
            set { algorithmSpecified = value; }
        }

        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CarSizeTypeSpecified
        {
            get { return carSizeTypeSpecified; }
            set { carSizeTypeSpecified = value; }
        }

        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FuelTypeSpecified
        {
            get { return fuelTypeSpecified; }
            set { fuelTypeSpecified = value; }
        }

        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MaxSpeedSpecified
        {
            get { return maxSpeedSpecified; }
            set { maxSpeedSpecified = value; }
        }

        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FuelConsumptionSpecified
        {
            get { return fuelConsumptionSpecified; }
            set { fuelConsumptionSpecified = value; }
        }

        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FuelConsumptionUnitSpecified
        {
            get { return fuelConsumptionUnitSpecified; }
            set { fuelConsumptionUnitSpecified = value; }
        }

        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FuelCostSpecified
        {
            get { return fuelCostSpecified; }
            set { fuelCostSpecified = value; }
        }

        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BanMotorwaySpecified
        {
            get { return banMotorwaySpecified; }
            set { banMotorwaySpecified = value; }
        }

        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AvoidTollSpecified
        {
            get { return avoidTollSpecified; }
            set { avoidTollSpecified = value; }
        }
        
        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BanLimitedAccessSpecified
        {
            get { return banLimitedAccessSpecified; }
            set { banLimitedAccessSpecified = value; }
        }

        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AvoidFerriesSpecified
        {
            get { return avoidFerriesSpecified; }
            set { avoidFerriesSpecified = value; }
        }

        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AvoidMotorwaySpecified
        {
            get { return avoidMotorwaySpecified; }
            set { avoidMotorwaySpecified = value; }
        }

        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AvoidRoadsSpecified
        {
            get { return avoidRoadsSpecified; }
            set { avoidRoadsSpecified = value; }
        }

        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool UseRoadsSpecified
        {
            get { return useRoadsSpecified; }
            set { useRoadsSpecified = value; }
        }

        #endregion
    }
}
