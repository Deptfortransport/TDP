// *********************************************** 
// NAME             : ResultSettings.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Class to hold the settings to be used for the Cycle journey result
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/ResultSettings.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:39:58   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// Class to hold the settings to be used for the Cycle journey result
    /// </summary>
    [Serializable]
    public class ResultSettings
    {
        #region Static Fields
        public static string DEFAULT_LANGUAGE = "en-GB";
        #endregion

        #region Private Fields
        private bool includeGeometry = true;
        private int pointSeparator = 32;
        private int eastingNorthingSeparator = 44;
        private DistanceUnit distanceUnit = DistanceUnit.Miles;
        private string language = DEFAULT_LANGUAGE;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public ResultSettings()
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Specifies if the OSGR coordinates for each journey direction be included in the result
        /// </summary>
        public bool IncludeGeometry
        {
            get { return includeGeometry; }
            set { includeGeometry = value; }
        }

        /// <summary>
        /// The ASCII character code integer to separate OSGR coordinates included in the geometry
        /// </summary>
        public int PointSeparator
        {
            get { return pointSeparator; }
            set { pointSeparator = value; }
        }

        /// <summary>
        /// The ASCII character code integer to separate the easting and northing for an OSGR
        /// coordinate included in the geometry, e.g. 44 represents ","
        /// </summary>
        public int EastingNorthingSeparator
        {
            get { return eastingNorthingSeparator; }
            set { eastingNorthingSeparator = value; }
        }

        /// <summary>
        /// The distance units the journey should be in e.g. Miles or Kms
        /// </summary>
        public DistanceUnit DistanceUnit
        {
            get { return distanceUnit; }
            set { distanceUnit = value; }
        }

        /// <summary>
        /// The language the result cycle journey instructions should be in.
        /// </summary>
        public string Language
        {
            get { return language; }
            set { language = value; }
        }
        #endregion
    }
}
