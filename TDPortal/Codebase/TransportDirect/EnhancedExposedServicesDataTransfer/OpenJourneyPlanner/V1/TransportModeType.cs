// *********************************************** 
// NAME                 : TransportModeType.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: Enumerator that represents a type of transport mode
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/TransportModeType.cs-arc  $
//
//   Rev 1.2   Jan 23 2013 11:52:32   mmodi
//Updatede to support Telecabine
//Resolution for 5884: CCN:677 - Telecabine modetype
//
//   Rev 1.1   Feb 18 2010 14:15:54   mturner
//Removed car and cycle from list of valid modes
//Resolution for 5404: EES - Open journey planner throws exception
//
//   Rev 1.0   Nov 08 2007 12:22:48   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:21:58   COwczarek
//Initial revision.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1 
{

    /// <summary>
    /// Enumerator that represents a type of transport mode
    /// </summary>
    [Serializable]
    public enum TransportModeType
    {
        Air,
        Bus,
        Coach,
        Drt,
        Ferry,
        Metro,
        Rail,
        RailReplacementBus,
        Taxi,
        Telecabine,
        Tram,
        Underground,
        Walk,
        CheckIn,
        CheckOut,
        Transfer
    }
 
}