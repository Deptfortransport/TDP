// *********************************************** 
// NAME                 : CarCostHelper.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Helper class to calculate the costs for a Car journey
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/CarCostHelper.cs-arc  $
//
//   Rev 1.0   Aug 04 2009 14:41:10   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;

using cjpInterface = TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1
{
    /// <summary>
    /// Helper class to calculate the costs for the outward and return Car journeys
    /// </summary>
    public class CarCostHelper
    {
        #region Private members

        private static CarCostCalculator costCalculator = (CarCostCalculator)TDServiceDiscovery.Current[ServiceDiscoveryKey.CarCostCalculator];

        private ITDJourneyRequest tdJourneyRequest = null;
        private ITDJourneyResult tdJourneyResult = null;
        private TDResourceManager tdResourceManager = null;

        private bool outward = true;

        private bool congestionChargeAdded = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CarCostHelper()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public CarCostHelper(bool outward, ITDJourneyRequest tdJourneyRequest, ITDJourneyResult tdJourneyResult, TDResourceManager tdResourceManager)
        {
            this.outward = outward;
            this.tdJourneyRequest = tdJourneyRequest;
            this.tdJourneyResult = tdJourneyResult;
            this.tdResourceManager = tdResourceManager;
            
            this.congestionChargeAdded = false;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Calculates the fuel cost for the road journey. 
        /// Uses the Outward property to determine which Road journey to use.
        /// </summary>
        public decimal CalculateFuelCost()
        {
            decimal fuelCost = 0;

            RoadJourney roadJourney = GetRoadJourney(outward);

            if (roadJourney != null)
            {
                decimal pence = Convert.ToDecimal(roadJourney.TotalFuelCost);

                // Cost is returned in 10,000
                // Therefore divide by 10000 to return it to pence
                pence = pence / 10000;

                // value is returned in pence, so convert to pounds, and round to nearest whole number
                fuelCost = Decimal.Round((pence / 100), 0);

                //Minimum fuel cost is 1 - then round it to nearest decimal point.
                if (fuelCost < 1)
                    fuelCost = 1;

            }

            return fuelCost;
        }

        /// <summary>
        /// Calculates the running cost for the road journey.
        /// Uses the ITDJourneyRequest and Outward properties to determine which Road journey to use
        /// </summary>
        /// <returns></returns>
        public decimal CalculateRunningCost()
        {
            decimal runningCost = 0;

            if (tdJourneyRequest != null)
            {
                RoadJourney roadJourney = GetRoadJourney(outward);

                if (roadJourney != null)
                {
                    string carFuelType = tdJourneyRequest.FuelType;
                    string carSize = tdJourneyRequest.CarSize;

                    // Total distance is in metres, so convert to kilometres for carcostcalculator
                    double distance = Convert.ToDouble(roadJourney.TotalDistance / 1000);

                    // returned value from cost calculator is in tenths of pence, so convert to pounds
                    decimal cost = Convert.ToDecimal(costCalculator.CalcRunningCost(carSize, carFuelType, distance));

                    // round it to nearest whole number
                    runningCost = Decimal.Round((cost / 1000), 0);

                    // minimum running cost is 1
                    if (runningCost < 1)
                        runningCost = 1;
                }
            }

            return runningCost;
        }

        /// <summary>
        /// Calculates the total of the Other costs for the road journey.
        /// Uses the Outward property to determine which Road journey to use.
        /// The CongestionChargeAdded property is used to ensure the congestion charge is not added to the
        /// return journey is already added to the outward. By default this property is false
        /// </summary>
        public decimal CalculateTotalOtherCosts()
        {
            decimal totalTollsCost = 0;

            RoadJourney roadJourney = GetRoadJourney(outward);
            RoadJourney returnRoadJourney = (outward) ? GetRoadJourney(!outward) : null;

            if (roadJourney != null)
            {
                // Loop through the array of RoadJourneyDetails and get any associated RoadJourneyChargeItems
                for (int journeyDetailIndex = 0; journeyDetailIndex < roadJourney.Details.Length; journeyDetailIndex++)
                {
                    //If there is a charge item then we need to decide if this should be added
                    //to the tolls total or not.
                    if (roadJourney.Details[journeyDetailIndex].ChargeItem != null)
                    {
                        //If it is the outward journey, congestion entry, and there is a charge,
                        //therefore for the return journey do not want to display a Congestion Entry Charge

                        // This doesn't handle the case where traveling between two different congestion zones.
                        if (returnRoadJourney != null)
                        {
                            TDDateTime outwardTime = roadJourney.JourneyLegs[0].LegStart.DepartureDateTime;
                            TDDateTime returnTime = returnRoadJourney.JourneyLegs[0].LegStart.DepartureDateTime;

                            if (outward
                                && (outwardTime.GetDifferenceDates(returnTime) == 0)
                                && (roadJourney.Details[journeyDetailIndex].TollCost > 0)
                                && ((roadJourney.Details[journeyDetailIndex].ChargeItem.SectionType == cjpInterface.StopoverSectionType.CongestionZoneEntry) ||
                                    (roadJourney.Details[journeyDetailIndex].ChargeItem.SectionType == cjpInterface.StopoverSectionType.CongestionZoneEnd) ||
                                    (roadJourney.Details[journeyDetailIndex].ChargeItem.SectionType == cjpInterface.StopoverSectionType.CongestionZoneExit)))
                            {
                                congestionChargeAdded = true;
                            }
                        }

                        // Test for toll being congestion related - either add the congestion toll or don't dependent on
                        // if it has already been added.
                        // This doesn't handle the case where traveling between two different congestion zones.
                        if ((!outward && congestionChargeAdded)
                            && ((roadJourney.Details[journeyDetailIndex].ChargeItem.SectionType == cjpInterface.StopoverSectionType.CongestionZoneEntry) ||
                                (roadJourney.Details[journeyDetailIndex].ChargeItem.SectionType == cjpInterface.StopoverSectionType.CongestionZoneEnd) ||
                                (roadJourney.Details[journeyDetailIndex].ChargeItem.SectionType == cjpInterface.StopoverSectionType.CongestionZoneExit)))
                        {
                            // Then there must be an outward journey with a congestion charge, so we add 0 for this stage
                            totalTollsCost = Decimal.Add(totalTollsCost, 0);
                        }
                        else
                        {
                            if (roadJourney.Details[journeyDetailIndex].ChargeItem.Charge < 0)
                            {
                                // Don't add if charge is undefined
                            }
                            else
                            {
                                //Add the cost of this charge item to the tolls total.
                                totalTollsCost = Decimal.Add(totalTollsCost, (decimal)roadJourney.Details[journeyDetailIndex].ChargeItem.Charge / 100);
                            }
                        }
                    }
                }   // End loop

                //Check the last leg of the destination for an associated car park. If there is 
                //a match, add the cost of the car park to the total journey costs.
                TDLocation location = roadJourney.DestinationLocation;
                if (location.ParkAndRideScheme != null)
                {
                    // Below amended CR 16/05/06 to use destination -1 toid to get car park costs.
                    CarParkInfo info = location.CarPark = location.ParkAndRideScheme.MatchCarPark(roadJourney.Details[roadJourney.Details.Length - 1].Toid);
                    if (info != null)
                    {
                        if (info.MinimumCost > 0)
                        {
                            decimal chargeItem = (decimal)info.MinimumCost / 100;
                            totalTollsCost = Decimal.Add(totalTollsCost, chargeItem);
                        }
                    }
                }

                //Check if its a Car park (not a Park and Ride Scheme car park) and add the cost.
                //Only add if its the Destination of the Outward segment
                if (outward)
                {
                    if (location.CarParking != null)
                    {
                        if (location.CarParking.MinimumCost > 0)
                        {
                            decimal chargeItem = (decimal)location.CarParking.MinimumCost / 100;
                            totalTollsCost = Decimal.Add(totalTollsCost, chargeItem);
                        }
                    }
                }

            }

            return totalTollsCost;
        }


        /// <summary>
        /// Method which loops through the road journey's charge items and creates CarCost dto objects
        /// Uses the Outward property to determine which Road journey to use.
        /// </summary>
        public CarCost[] CreateOtherCostItems()
        {
            RoadJourney roadJourney = GetRoadJourney(outward);
            
            ArrayList carCostItems = new ArrayList();

            if (roadJourney != null)
            {
                CarCost carCost = null;

                // Used to track first occurance of adding a Congestion charge
                bool congestionAdded = false;

                // Loop through the array of RoadJourneyDetails and get any associated RoadJourneyChargeItems
                for (int journeyDetailIndex = 0; journeyDetailIndex < roadJourney.Details.Length; journeyDetailIndex++)
                {
                    carCost = null;

                    if (roadJourney.Details[journeyDetailIndex].ChargeItem != null)
                    {
                        RoadJourneyChargeItem chargeItem = roadJourney.Details[journeyDetailIndex].ChargeItem;

                        // For CongestionZones, we want to add the first occurance of a Congestion charge, 
                        // and then only subsequent Congestion charges > £0

                        // Check for CongestionZone type and first congestionChargeAdded
                        if (
                            ((chargeItem.SectionType == cjpInterface.StopoverSectionType.CongestionZoneEntry) ||
                            (chargeItem.SectionType == cjpInterface.StopoverSectionType.CongestionZoneEnd) ||
                            (chargeItem.SectionType == cjpInterface.StopoverSectionType.CongestionZoneExit)) &&
                            !congestionAdded
                            )
                        {
                            carCost = FormatChargeData(roadJourney, journeyDetailIndex);

                            congestionAdded = true;
                        }
                        // Check for CongestionZone type and only add if its greater than £0
                        else if (
                            ((chargeItem.SectionType == cjpInterface.StopoverSectionType.CongestionZoneEntry) ||
                            (chargeItem.SectionType == cjpInterface.StopoverSectionType.CongestionZoneEnd) ||
                            (chargeItem.SectionType == cjpInterface.StopoverSectionType.CongestionZoneExit)) &&
                            congestionAdded
                            )
                        {
                            if (chargeItem.Charge > 0)
                                carCost = FormatChargeData(roadJourney, journeyDetailIndex);
                        }
                        // For all other types of charges, just add the detail
                        else
                        {
                            carCost = FormatChargeData(roadJourney, journeyDetailIndex);
                        }
                    }

                    if (carCost != null)
                        carCostItems.Add(carCost);
                }

                // Check if location contains a park and ride scheme
                if (roadJourney.DestinationLocation.ParkAndRideScheme != null)
                {
                    carCost = FormatParkAndRide(roadJourney.DestinationLocation, roadJourney.Details[roadJourney.Details.Length - 1].Toid);

                    if (carCost != null)
                        carCostItems.Add(carCost);
                }
            }

            return (CarCost[])carCostItems.ToArray(typeof(CarCost));
        }


        #endregion

        #region Properties

        /// <summary>
        /// Read/write property. Flag which sets if costs are calculated for the outward or return journey
        /// </summary>
        public bool Outward
        {
            get { return outward; }
            set { outward = value; }
        }

        /// <summary>
        /// Read/write property to flag if congestion charge has been added.
        /// This is initially set to false and becomes true once the charge has been added 
        /// after calling calculate costs
        /// </summary>
        public bool CongestionChargeAdded
        {
            get { return congestionChargeAdded; }
            set { congestionChargeAdded = true; }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Returns the RoadJourney or null
        /// </summary>
        private RoadJourney GetRoadJourney(bool outward)
        {
            if (tdJourneyResult != null)
            {
                // Get the journey
                return (outward) ? tdJourneyResult.OutwardRoadJourney() : tdJourneyResult.ReturnRoadJourney();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns a CarCost item that has the cost, description, and hyperlink 
        /// for the road journey charge item
        /// </summary>
        private CarCost FormatChargeData(RoadJourney roadJourney, int journeyDetailIndex)
        {
            CarCost carCost = null;

            RoadJourneyChargeItem chargeItem = roadJourney.Details[journeyDetailIndex].ChargeItem;

            if (chargeItem.Charge < 0)
            {
                // If the charge is < 0, then pass that value through
                carCost = CarJourneyPlannerAssembler.CreateCarCostDT(CarCostType.OtherCost, chargeItem.Charge,
                    chargeItem.SectionType.ToString(), chargeItem.CompanyName, chargeItem.Url);
            }
            else
            {
                // convert the charge item (int) to pounds and pence
                double cost = Convert.ToDouble(chargeItem.Charge) / 100;

                //if we are on the return Journey and CongestionCostAdded is true, then Charge is zero
                if ((!outward && congestionChargeAdded) && roadJourney.Details[journeyDetailIndex].CongestionEntry)
                {
                    cost = 0;
                }

                carCost = CarJourneyPlannerAssembler.CreateCarCostDT(CarCostType.OtherCost, cost,
                    chargeItem.SectionType.ToString(), chargeItem.CompanyName, chargeItem.Url);
            }

            return carCost;
        }

        /// <summary>
        /// Obtains the ParkAndRideInfo that corresponds to the required scheme
        /// </summary>
        /// <param name="parkInfo">The ParkAndRideInfo object</param>
        /// <param name="toids">An array of toids</param>
        /// <returns>A string array</returns>
        private CarCost FormatParkAndRide(TDLocation location, string[] toids)
        {
            CarCost carCost = null;

            //update the car park using the park and ride scheme every time
            location.CarPark = location.ParkAndRideScheme.MatchCarPark(toids);

            if (location.CarPark != null)
            {
                CarParkInfo carPark = location.CarPark;

                // Set Car park name
                string carParkDisplayName = carPark.CarParkName +
                    tdResourceManager.GetString("ParkAndRide.CarkPark.Suffix", TDCultureInfo.CurrentUICulture);

                string url = string.Empty;

                // Check for url
                if (!string.IsNullOrEmpty(carPark.UrlLink))
                {
                    url = carPark.UrlLink;
                }
                else if (!string.IsNullOrEmpty(location.ParkAndRideScheme.UrlLink))
                {
                    url = location.ParkAndRideScheme.UrlLink;
                }

                // Set the cost
                decimal cost = carPark.MinimumCost;

                if (cost > 0)
                {
                    // convert the charge item (int) to pounds and pence
                    cost = (decimal)carPark.MinimumCost / 100;
                }

                // Set the description using car park comments
                string description = string.Empty;

                if (!string.IsNullOrEmpty(carPark.Comments.Trim()))
                    description = tdResourceManager.GetString("ParkAndRide.Comments.Prefix", TDCultureInfo.CurrentUICulture) + carPark.Comments.Trim();

                carCost = CarJourneyPlannerAssembler.CreateCarCostDT(CarCostType.OtherCost, Convert.ToDouble(cost), description, carParkDisplayName, url);
            }

            return carCost;
        }

        #endregion
    }
}
