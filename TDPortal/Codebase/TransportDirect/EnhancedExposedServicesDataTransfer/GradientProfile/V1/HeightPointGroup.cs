// *********************************************** 
// NAME             : HeightPointGroup.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: These represent the land height at points separated 
//                  : by the specified resolution within the group (i.e. section of the route).  
//                  : It is possible that if the group represents a particularly short section of 
//                  : the route (i.e. shorter than the resolution) then it may not contain any height points
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/GradientProfile/V1/HeightPointGroup.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:43:06   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.GradientProfile.V1
{
    /// <summary>
    /// These represent the land height at points separated by the specified resolution 
    /// within the group (i.e. section of the route). 
    /// It is possible that if the group represents a particularly short section of the 
    /// route (i.e. shorter than the resolution) then it may not contain any height points
    /// </summary>
    [Serializable]
    public class HeightPointGroup
    {
        #region Private Fields
        private int id;
        private HeightPoint[] heightPoints;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public HeightPointGroup()
        { }
        #endregion

        #region Public Properties
        /// <summary>
        /// Read/write.
        /// Specify the unique identifier for the group within the height groups array
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Read/write.
        /// These represent the land height at points separated by the specified resolution 
        /// within the group (i.e. section of the route)
        /// </summary>
        public HeightPoint[] HeightPoints
        {
            get { return heightPoints; }
            set { heightPoints = value; }
        }
        #endregion
    }
}
