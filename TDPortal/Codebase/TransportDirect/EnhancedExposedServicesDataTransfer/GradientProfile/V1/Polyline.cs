// *********************************************** 
// NAME             : Polyline.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Class contains a list of OSGR co-ordinates which represents a geometry of a path/route
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/GradientProfile/V1/Polyline.cs-arc  $
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
    /// Class contains a list of OSGR co-ordinates which represents a geometry of a path/route
    /// </summary>
    [Serializable]
    public class Polyline
    {
        #region Private members
        // identifier for the polyline object
        private int id = 0;

        // The list of co-ordinates in OSGR format
        private string polylineGridReferences = null;

        // Flag used by the GradientProfiler. 
        // if True, gradient profiler uses the land height at start and end of polyline to interpolate
        // the height along it
        // if False, gradient profiler will look up the land height values in the DTM data.
        // Default is false
        private bool interpolateGradient = false;

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Polyline()
        {
        }

        /// <summary>
        /// Constructor accepting an id and grid references
        /// </summary>
        /// <param name="id">id (within an array of polylines for a group, the identifiers must be in 
        /// ascending numerical order)</param>
        /// <param name="osGridReference">list of OSGR coordinates which represent the polyline</param>
        /// <param name="interpolateGradient">
        /// if True, gradient profiler uses the land height at start and end of polyline to interpolate
        /// the height along it
        /// if False, gradient profiler will look up the land height values in the DTM data.
        /// Default is false</param>
        public Polyline(int id, string polylineGridReferences, bool interpolateGradient)
        {
            this.id = id;
            this.polylineGridReferences = polylineGridReferences;
            this.interpolateGradient = interpolateGradient;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/write. The ID of the polyline. Must be in ascending numerical order if 
        /// there is an array of Polyline objects for a group
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Read/write. The array of OSGR coordinates representing the polyline as a string
        /// </summary>
        public string PolylineGridReferences
        {
            get { return polylineGridReferences; }
            set { polylineGridReferences = value; }
        }

        // Read/write.
        // if True, gradient profiler uses the land height at start and end of polyline to interpolate
        // the height along it
        // if False, gradient profiler will look up the land height values in the DTM data.
        // Default is false
        public bool InterpolateGradient
        {
            get { return interpolateGradient; }
            set { interpolateGradient = value; }
        }

        #endregion
    }
}
