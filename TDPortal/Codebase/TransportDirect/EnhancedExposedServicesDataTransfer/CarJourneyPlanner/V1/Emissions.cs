// *********************************************** 
// NAME                 : Emissions.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Class to hold the Emissions for a journey
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/Emissions.cs-arc  $
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
    /// Class to hold the Emissions for a journey
    /// </summary>
    [System.Serializable]
    public class Emissions
    {
        private VehicleType vehicleType;
        private double emissionValue;

        /// <summary>
        /// Constructor
        /// </summary>
        public Emissions()
        {
        }

        /// <summary>
        /// The type of vehicle the emissions value is for
        /// </summary>
        public VehicleType VehicleType
        {
            get { return vehicleType; }
            set { vehicleType = value; }
        }

        /// <summary>
        /// The CO2 emissions value in kg
        /// </summary>
        public double CO2Emissions
        {
            get { return emissionValue; }
            set { emissionValue = value; }
        }
    }
}
