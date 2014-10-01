// *********************************************** 
// NAME			: ITDGradientProfileRequest.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/07/2008
// DESCRIPTION	: Definition of the ITDGradientProfileRequest Interface
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/ITDGradientProfileRequest.cs-arc  $
//
//   Rev 1.0   Jul 18 2008 13:42:06   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    public interface ITDGradientProfileRequest
    {   
        // Reference/Sequence number used for the request ID sent to Gradient Profiler
        int ReferenceNumber { get; set;}
        int SequenceNumber { get; set;}

        // Settings
        int Resolution { get; set; }
        char EastingNorthingSeperator { get; set; }
        char PointSeperator { get; set; }

        // TDPolyline arrays
        Dictionary<int, TDPolyline[]> TDPolylines { get; set; }
    }
}
