// *********************************************** 
// NAME			: TDGradientProfileRequest.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/07/2008
// DESCRIPTION	: Implementation of the ITDGradientProfileRequest
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/TDGradientProfileRequest.cs-arc  $
//
//   Rev 1.1   Feb 12 2009 12:43:54   mturner
//Fixed session management bug
//Resolution for 5245: Cycle Planner - After Amend Map page shows wrong journey
//
//   Rev 1.0   Jul 18 2008 13:43:38   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    [Serializable()]
    public class TDGradientProfileRequest : ITDGradientProfileRequest, ITDSessionAware, ICloneable
    {
        #region Private members

        // Reference number
        private int referenceNumber = 0;
        private int sequenceNumber = 0;

        // Settings
        private int resolution = 10;
        private char eastingNorthingSeperator = ',';
        private char pointSeperator = ' ';

        // Polylines to get the GradientProfile for
        private Dictionary<int, TDPolyline[]> tdPolylines;

        private bool isDirty = true;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TDGradientProfileRequest()
        {
        }

        #endregion

        #region ITDGradientProfileRequest Members

        /// <summary>
        /// Read/write. The ReferenceNumber used to track this request.
        /// This will be set by the GradientProfilerManager and therefore should NOT be set manually.
        /// </summary>
        public int ReferenceNumber
        {
            get { return referenceNumber; }
            set 
            {
                IsDirty = true;
                referenceNumber = value;
            }
        }

        /// <summary>
        /// Read/write. The SequenceNumber which is incremented each time this Request is sent to the Gradient Profiler
        /// This will be set by the GradientProfilerManager and therefore should NOT be set manually.
        /// </summary>
        public int SequenceNumber
        {
            get { return sequenceNumber; }
            set 
            {
                IsDirty = true;
                sequenceNumber = value; 
            }
        }

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
                IsDirty = true;
                resolution = value;
            }
        }

        /// <summary>
        /// Read/write. The character used to sepearte the easting and northing of each coordinate
        /// </summary>
        public char EastingNorthingSeperator
        {
            get { return eastingNorthingSeperator; }
            set 
            {
                IsDirty = true;
                eastingNorthingSeperator = value;
            }
        }

        /// <summary>
        /// Read/write. The character used to seperate the points within the polyline string
        /// </summary>
        public char PointSeperator
        {
            get { return pointSeperator; }
            set 
            {
                IsDirty = true;
                pointSeperator = value; 
            }
        }

        /// <summary>
        /// Read/write. The groups of polylines to request the Gradient profile for
        /// </summary>
        public Dictionary<int, TDPolyline[]> TDPolylines
        {
            get { return tdPolylines; }
            set 
            {
                IsDirty = true;
                tdPolylines = value; 
            }
        }

        #endregion

        #region ITDSessionAware Members

        /// <summary>
        /// Read/write property indicating whether or not the object has changed since
        /// it was last saved. 
        /// </summary>
        public bool IsDirty
        {
            get { return isDirty; }
            set { isDirty = value; }
        }

        #endregion

        #region ICloneable Members

        /// <summary>
        /// Performs a memberwise clone of this object. 
        /// </summary>
        /// <returns>A copy of this object</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

    }
}
