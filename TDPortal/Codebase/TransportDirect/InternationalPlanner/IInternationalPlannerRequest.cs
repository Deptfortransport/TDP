// *********************************************** 
// NAME			: IInternationalPlannerRequest.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 26/01/2010
// DESCRIPTION	: Interface Class which defines the public properties and methods which make up an 
//                International planner request
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlanner/IInternationalPlannerRequest.cs-arc  $
//
//   Rev 1.1   Feb 04 2010 10:25:56   mmodi
//Updates as part of development
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Jan 29 2010 12:04:28   mmodi
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.InternationalPlanner
{
    /// <summary>
    /// Interface Class which defines the public properties and methods which make up an 
    /// International planner request
    /// </summary>
    public interface IInternationalPlannerRequest
    {
        string RequestID { get; set; }
        string SessionID { get; set; }
        int UserType { get; set; }
        
        DateTime OutwardDateTime { get; set; }
        InternationalModeType[] ModeType { get; set; }
        string OriginName { get; set; }
        string OriginCityID { get; set; }
        string[] OriginNaptans { get; set; }
        string DestinationName { get; set; }
        string DestinationCityID { get; set; }
        string[] DestinationNaptans { get; set; }
    }
}
