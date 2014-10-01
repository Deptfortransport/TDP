// *********************************************** 
// NAME             : GradientProfileResult.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Class to hold the result of the Gradient Profile planning returned from GPES
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/GradientProfile/V1/GradientProfileResult.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:43:06   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;


namespace TransportDirect.EnhancedExposedServices.DataTransfer.GradientProfile.V1
{
    /// <summary>
    /// Class to hold the result of the Gradient Profile planning 
    /// returned from GPES (Gradient Profile Exposed Service)
    /// </summary>
    [Serializable]
    public class GradientProfileResult
    {
        #region Private Fields
        private CompletionStatus completionStatus;
        private Message[] errorMessages;
        private Message[] userWarnings;
        private HeightPointGroup[] heightGroups;
        private int resolution;
        #endregion

        #region Constructor
        #endregion

        #region Public Properties
        /// <summary>
        /// The overall completion status of the Gradient Profile.
        /// This will have a status of Success if the gradient profile returned for the request.
        /// A gradient profile request which failed will contain error messages specifying failure
        /// </summary>
        public CompletionStatus CompletionStatus
        {
            get { return completionStatus; }
            set { completionStatus = value; }
        }

        /// <summary>
        /// Contains any errors that occured when processing gradient profile request
        /// </summary>
        public Message[] ErrorMessages
        {
            get { return errorMessages; }
            set { errorMessages = value; }
        }

        /// <summary>
        /// Contains any warning that the end user of the results should be aware of 
        /// This text is language sensitive. This array may be populated only if journey 
        /// planning was successful
        /// </summary>
        public Message[] UserWarnings
        {
            get { return userWarnings; }
            set { userWarnings = value; }
        }

        /// <summary>
        /// Read. The size (metres) of the intervals between the points for which the Gradient profiler
        /// used to get the land height.
        /// </summary>
        public int Resolution
        {
            get { return resolution; }
            set { resolution = value; }
        }

        /// <summary>
        /// Gradient profile result will contain an array of height groups.  
        /// The height groups included in the gradient profile result will correspond to the polylines groups specified in the request.  
        /// </summary>
        public HeightPointGroup[] HeightGroups {
            get { return heightGroups; }
            set { heightGroups = value; } 
        }
        #endregion
    }
}
