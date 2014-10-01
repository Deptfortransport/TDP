// *********************************************** 
// NAME             : Settings.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: This type represents the settings for OSGRs in the 
//                  : polylines in gradient profile request 
//                  : and resolution which influences the result
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/GradientProfile/V1/Settings.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:43:10   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.GradientProfile.V1
{
    /// <summary>
    /// This type represents the settings for OSGRs in the polylines in gradient profile request 
    /// and resolution which influences the result
    /// </summary>
    [Serializable]
    public class Settings
    {
        #region Private Fields
        private int resolution = 10;
        private int eastingNorthingSeperator = 44;
        private int pointSeperator = 32;
        #endregion

        #region Public Properties
        /// <summary>
        /// Read/write. The size (metres) of the intervals between the points for which the Gradient profiler
        /// gets the land height.
        /// Value must be above 0. A value less than 10 metres has no benefit as source data is in 10 metre grids
        /// </summary>
        public int Resolution
        {
            get { return resolution; }
            set
            {
                resolution = value;
            }
        }

        /// <summary>
        /// Read/write. The character used to sepearte the easting and northing of each coordinate
        /// </summary>
        public int EastingNorthingSeperator
        {
            get { return eastingNorthingSeperator; }
            set
            {
                eastingNorthingSeperator = value;
            }
        }

        /// <summary>
        /// Read/write. The character used to seperate the points within the polyline string
        /// </summary>
        public int PointSeperator
        {
            get { return pointSeperator; }
            set
            {
                pointSeperator = value;
            }
        }
        #endregion
    }
}
