// *********************************************** 
// NAME			: ITDGradientProfileResult.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/07/2008
// DESCRIPTION	: Definition of the ITDGradientProfileResult Interface
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/ITDGradientProfileResult.cs-arc  $
//
//   Rev 1.0   Jul 18 2008 13:42:08   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    public interface ITDGradientProfileResult
    {
        // Messages returned by the gradient profiler (reused CyclePlannerMessage to avoid creating new message type)
        CyclePlannerMessage[] Messages { get; }

        // Gradient Profile height points
        Dictionary<int, TDHeightPoint[]> TDHeightPoints { get; set; }

        // Value to describe the interval distance between the height points
        int Resolution { get; }

        // Indicates if the Result is valid
        bool IsValid { get; }

        /// <summary>
        /// Clears the current list of messages.
        /// </summary>
        void ClearMessages();

        /// <summary>
        /// Adds a new message to the current message array.
        /// </summary>
        void AddMessageToArray(string description, string resourceId, int majorCode, int minorCode);

        /// <summary>
        /// Adds a new message to the current message array.
        /// </summary>
        void AddMessageToArray(string description, string resourceId, int majorCode, int minorCode, ErrorsType type);
    }

    /// <summary>
    /// Public struct to define a height point value
    /// </summary>
    [Serializable()]
    public struct TDHeightPoint
    {
        public int ID;
        public int Height;
 
        /// <summary>
        /// Height point struct constructor.
        /// Ensure ID used is in ascending numerical order when there is an array of Height points
        /// </summary>
        /// <param name="id"></param>
        /// <param name="height"></param>
        public TDHeightPoint(int id, int height)
        {
            this.ID = id;
            this.Height = height;
        }
    }
}
