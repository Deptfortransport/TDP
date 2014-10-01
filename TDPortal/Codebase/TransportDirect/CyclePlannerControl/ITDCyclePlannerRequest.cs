// *********************************************** 
// NAME			: ITDCyclePlannerRequest.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/06/2008
// DESCRIPTION	: Definition of the ITDCycleRequest Interface
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/ITDCyclePlannerRequest.cs-arc  $
//
//   Rev 1.4   Sep 29 2010 11:26:16   apatel
//EES WebServices for Cycle Code changes
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.3   Sep 02 2008 10:37:02   mmodi
//Updated user preference object to enable serialization
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Aug 22 2008 10:10:02   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Aug 04 2008 10:19:46   mmodi
//Updates to work with actual web service
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 15:42:02   mmodi
//Initial revision.
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    public interface ITDCyclePlannerRequest
    {
        bool IsReturnRequired { get; set; }
        bool OutwardArriveBefore { get; set; }
        bool ReturnArriveBefore { get; set; }
        bool OutwardAnyTime { get; set; }
        bool ReturnAnyTime { get; set; }

        TDDateTime[] OutwardDateTime { get; set; }
        TDDateTime[] ReturnDateTime { get; set; }

        TDLocation OriginLocation { get; set; }
        TDLocation DestinationLocation { get; set; }
        
        TDLocation[] CycleViaLocations { get; set;}

        string CycleJourneyType { get; set;}
        string PenaltyFunction { get; set; }
        TDCycleUserPreference[] UserPreferences { get; set; }
        TDCycleJourneyResultSettings ResultSettings { get; set; }

        //ICloneable Interface Members
        object Clone();

    }
}
