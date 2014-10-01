// *********************************************** 
// NAME             : JourneyRequestPopulator.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 30 Mar 2011
// DESCRIPTION  	: JourneyRequestPopulator base class for hierarchy of classes
//                responsible for populating requests of various types for the CJP or CTP.	
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.PropertyManager;
using TDP.Common.LocationService;

namespace TDP.UserPortal.JourneyControl
{
    /// <summary>
    /// JourneyRequestPopulator base class
    /// </summary>
    public abstract class JourneyRequestPopulator
    {
        #region Private members

        protected ITDPJourneyRequest tdpJourneyRequest;
        protected IPropertyProvider properties;
        protected LocationService locationService;

        #endregion
        
        #region Public methods

        /// <summary>
        /// Creates the CJPRequest objects needed to call the CJP for the current 
        /// ITDPJourneyRequest, and returns them encapsulated in an array of CJPCall objects.
        /// </summary>
        public abstract CJPCall[] PopulateRequestsCJP(int referenceNumber,
                                                   int seqNo,
                                                   string sessionId,
                                                   bool referenceTransaction,
                                                   int userType,
                                                   string language);

        /// <summary>
        /// Creates the CyclePlannerRequest objects needed to call the Cycle planner service for the current 
        /// ITDPJourneyRequest, and returns them encapsulated in an array of CyclePlannerCall objects.
        /// </summary>
        public abstract CyclePlannerCall[] PopulateRequestsCTP(int referenceNumber,
                                                   int seqNo,
                                                   string sessionId,
                                                   bool referenceTransaction,
                                                   int userType,
                                                   string language);

        #endregion

        #region Public properties

        /// <summary>
        /// Read/write-only - the ITDPJourneyRequest supllied by the caller
        /// </summary>
        protected ITDPJourneyRequest TDPJourneyRequest
        {
            get { return tdpJourneyRequest; }
            set { tdpJourneyRequest = value; }
        }

        #endregion
        
        #region Protected methods

        /// <summary>
        /// Formats a sequence number for use as a request-id
        /// </summary>
        /// <param name="seqNo"></param>
        /// <returns>Formatted string</returns>
        protected string FormatSeqNo(int seqNo)
        {
            return seqNo.ToString("-0000");
        }

        #endregion
    }
}
