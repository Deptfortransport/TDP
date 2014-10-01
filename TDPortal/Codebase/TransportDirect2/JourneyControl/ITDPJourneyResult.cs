﻿// *********************************************** 
// NAME             : ITDPJourneyResult.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Feb 2011
// DESCRIPTION  	: Interface for TDPJourneyResult
// ************************************************
// 
                
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.LocationService;
using TDP.Common;

namespace TDP.UserPortal.JourneyControl
{
    /// <summary>
    /// Interface for TDPJourneyResult
    /// </summary>
    public interface ITDPJourneyResult
    {
        #region Public properties

        /// <summary>
        /// TDPJourneyRequest Hash value this journey belongs to
        /// </summary>
        string JourneyRequestHash { get; set; }

        /// <summary>
        /// JourneyReferenceNumber to uniquely identify this TDPJourneyResult
        /// </summary>
        int JourneyReferenceNumber { get; set; }

        /// <summary>
        /// Messages returned by the journey planner
        /// </summary>
        List<TDPMessage> Messages { get; set; }
        
        /// <summary>
        /// Origin Location for the journey result
        /// </summary>
        TDPLocation Origin { get; set; }

        /// <summary>
        /// Destination Location for the journey result
        /// </summary>
        TDPLocation Destination { get; set; }

        /// <summary>
        /// Return Origin Location for the journey request (if different from the Outward Destination)
        /// </summary>
        TDPLocation ReturnOrigin { get; set; }

        /// <summary>
        /// Return Destination Location for the journey request (if different from the Outward Origin)
        /// </summary>
        TDPLocation ReturnDestination { get; set; }

        /// <summary>
        /// Outward DateTime
        /// </summary>
        DateTime OutwardDateTime { get; set; }

        /// <summary>
        /// Return DateTime
        /// </summary>
        DateTime ReturnDateTime { get; set; }

        /// <summary>
        /// Outward arrive before time flag
        /// </summary>
        bool OutwardArriveBefore { get; set; }

        /// <summary>
        /// Return arrive before time flag
        /// </summary>
        bool ReturnArriveBefore { get; set; }

        /// <summary>
        /// Outward journeys
        /// </summary>
        List<Journey> OutwardJourneys { get; set; }
        
        /// <summary>
        /// Return journeys
        /// </summary>
        List<Journey> ReturnJourneys { get; set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns the journey for the id requested. Null if not found.
        /// </summary>
        /// <param name="journeyId">Journey Id</param>
        /// <returns></returns>
        Journey GetJourney(int journeyId);

        /// <summary>
        /// Sorts the journeys containined in this result 
        /// </summary>
        void SortJourneys();

        #endregion
    }
}
