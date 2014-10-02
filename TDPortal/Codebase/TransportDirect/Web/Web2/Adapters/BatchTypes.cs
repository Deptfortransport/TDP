// *********************************************** 
// NAME             : BatchTypes.cs      
// AUTHOR           : David Lane
// DATE CREATED     : 23 Feb 2012
// DESCRIPTION  	: Enums defining batch statuses and batch user statuses
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/BatchTypes.cs-arc  $
//
//   Rev 1.0   Mar 05 2012 10:30:14   dlane
//Initial revision.
//Resolution for 5787: Batch Journey Planner
//
// 


using System;
using System.Collections.Generic;
using System.Web;

namespace TransportDirect.UserPortal.Web.Adapters
{
    /// <summary>
    /// Enum to define batch statuses
    /// </summary>
    public enum BatchStatus
    {
        Queued = 1,
        InProgress = 2,
        Complete = 3,
        Errored = 4
    }

    /// <summary>
    /// Enum to define batch detail statuses
    /// </summary>
    public enum BatchDetailStatus
    {
        Submitted = 1,
        Complete = 2,
        Errored = 3,
        ValidationError = 4
    }

    /// <summary>
    /// Enum to define batch user statuses
    /// </summary>
    public enum BatchUserStatus
    {
        Pending = 1,
        Active = 2,
        Suspended = 3
    }
}
