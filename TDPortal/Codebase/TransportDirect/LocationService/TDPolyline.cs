// *********************************************** 
// NAME                 : TDPolyline.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 10/07/2008
// DESCRIPTION          : Class to hold an array of OSGridReferences[] (the geometry) 
//                      : which represent a polyline
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/TDPolyline.cs-arc  $
//
//   Rev 1.0   Jul 18 2008 13:45:32   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.LocationService
{
    [Serializable()]
    public class TDPolyline
    {
        #region Private members
        // identifier for the polyline object
        private int id = 0;

        // OSGR coordinates for this polyline
        private OSGridReference[] osGridReference = null;

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
        public TDPolyline()
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
        public TDPolyline(int id, OSGridReference[] osGridReference, bool interpolateGradient)
        {
            this.id = id;
            this.osGridReference = osGridReference;
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
        /// Read/write. The array of OSGR coordinates representing the polyline
        /// </summary>
        public OSGridReference[] Geometry
        {
            get { return osGridReference; }
            set { osGridReference = value; }
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

        #region Public methods

        /// <summary>
        /// Returns the OSGR coordinates as a string, representing the polyline of this object
        /// </summary>
        /// <returns></returns>
        public string GetPolyline()
        {
            if ((osGridReference != null) && (osGridReference.Length > 0))
            {
                StringBuilder polyline = new StringBuilder();

                foreach (OSGridReference osgr in osGridReference)
                {
                    polyline.Append(osgr.Easting.ToString());
                    polyline.Append(",");
                    polyline.Append(osgr.Northing.ToString());
                    polyline.Append(" ");
                }

                return polyline.ToString().TrimEnd(new char[]{' '});
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion
    }
}
