// *********************************************** 
// NAME             : PolylineGroup.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Class to hold a group of polylines specifying a section/complete route geometry
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/GradientProfile/V1/PolylineGroup.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:43:08   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.GradientProfile.V1
{
    /// <summary>
    /// Class to hold a group of polylines specifying a section/complete route geometry
    /// </summary>
    [Serializable]
    public class PolylineGroup
    {
        #region Private Fields
        private int id;
        private Polyline[] polylines;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PolylineGroup()
        { }
        #endregion

        #region Public Properties
        /// <summary>
        /// Read/write.
        /// Specifying identifier for the polyline group
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Read/write. Array of polylines specifying a section/complete geometry locations of route
        /// </summary>
        public Polyline[] Polylines
        {
            get { return polylines; }
            set { polylines = value; }
        }
        #endregion
    }
}
