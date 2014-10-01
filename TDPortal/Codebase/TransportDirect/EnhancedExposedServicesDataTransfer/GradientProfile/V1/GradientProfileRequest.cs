// *********************************************** 
// NAME             : GradientProfileRequest.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Gradient profile request represents the array of coordinates 
//                  : for which height points needed to create gradient profile
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/GradientProfile/V1/GradientProfileRequest.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:43:04   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.GradientProfile.V1
{
    /// <summary>
    /// Gradient profile request represents the array of coordinates 
    /// for which height points needed to create gradient profile
    /// </summary>
    [Serializable]
    public class GradientProfileRequest
    {
        #region Private Fields
        
        // Settings
        private Settings settings;

        private PolylineGroup[] polylineGroups;

        #endregion

        #region Constructor
        public GradientProfileRequest() { }
        #endregion

        #region Public Properties
        /// <summary>
        /// Read/write. Specifies settings which influences request and response
        /// </summary>
        public Settings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        /// <summary>
        /// Read/write. The groups of polylines to request the Gradient profile for
        /// </summary>
        public PolylineGroup[] PolylineGroups
        {
            get { return polylineGroups; }
            set
            {
                polylineGroups = value;
            }
        }
        #endregion
    }
}
