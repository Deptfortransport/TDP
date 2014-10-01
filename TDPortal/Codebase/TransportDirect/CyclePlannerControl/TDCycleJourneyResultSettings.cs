// *********************************************** 
// NAME             : TDCycleJourneyResultSettings.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Cycle journey result settings used to customises cycle journey result
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/TDCycleJourneyResultSettings.cs-arc  $
//
//   Rev 1.1   Oct 05 2010 13:52:12   apatel
//Updated to add comments and header
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    /// <summary>
    /// TDCycleJourneyResultSettings class
    /// </summary>
    [Serializable]
    public class TDCycleJourneyResultSettings
    {
        #region Private Fields
        private bool includeGeometry;
        private char pointSeparator;
        private char eastingNorthingSeparator;
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
        /// The ASCII character code to separate OSGR coordinates included in the geometry
        /// </summary>
        public char PointSeparator
        {
            get { return pointSeparator; }
            set { pointSeparator = value; }
        }

        /// <summary>
        /// The ASCII character code to separate the easting and northing for an OSGR
        /// </summary>
        public char EastingNorthingSeparator
        {
            get { return eastingNorthingSeparator; }
            set { eastingNorthingSeparator = value; }
        }
        #endregion
    }
}
