// *********************************************** 
// NAME                 : RealTimeRoadEventType.cs 
// AUTHOR               : Amit Patel
// DATE CREATED         : 31/08/2011
// DESCRIPTION          : Real time road event type enum
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/RealTimeRoadEventType.cs-arc  $
//
//   Rev 1.0   Sep 02 2011 10:34:50   apatel
//Initial revision.
//Resolution for 5731: CCN 0548 - Real Time Information in Car

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
    /// <summary>
    /// Real time road event type enum
    /// </summary>
    public enum RealTimeRoadEventType
    {
        RealTimeRoadJourneyTravelNewsMatching,
        RealTimeRoadJOurneyReplanAvoidingTravelNews
    }
}
