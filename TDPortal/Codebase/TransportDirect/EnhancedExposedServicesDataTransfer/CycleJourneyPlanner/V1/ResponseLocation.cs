// *********************************************** 
// NAME             : ResponseLocation.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Class to hold details of the location for a cycle JourneyResult
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/ResponseLocation.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:39:56   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// Class to hold details of the location for a cycle JourneyResult
    /// </summary>
    [Serializable]
    public class ResponseLocation
    {
        #region Private Fields
        private string description;
        private LocationType type;
        private OSGridReference gridReference;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ResponseLocation() { }
        #endregion

        #region Public Properties
        /// <summary>
        /// Specifies the display name of the location
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Specifies the format of the location
        /// </summary>
        public LocationType Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// Grid reference of location
        /// </summary>
        public OSGridReference GridReference
        {
            get { return gridReference; }
            set { gridReference = value; }
        }
        #endregion
    }
}
