// *********************************************** 
// NAME             : HeightPoint.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Structure that represents land height at perticular point 
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/GradientProfile/V1/HeightPoint.cs-arc  $
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
    /// Structure that represents land height at perticular point 
    /// </summary>
    [Serializable]
    public struct HeightPoint
    {
        #region Public Fields
        public int ID;
        public int Height;
        #endregion

        #region Constructors
        
        /// <summary>
        /// Height point struct constructor.
        /// Ensure ID used is in ascending numerical order when there is an array of Height points
        /// </summary>
        /// <param name="id"></param>
        /// <param name="height"></param>
        public HeightPoint(int id, int height)
        {
            this.ID = id;
            this.Height = height;
        }
        #endregion
    }
}
