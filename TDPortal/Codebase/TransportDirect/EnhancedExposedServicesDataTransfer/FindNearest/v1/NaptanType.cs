// *********************************************** 
// NAME                 : NaptanType.cs
// AUTHOR               : Marcus Tillett
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: NaptanType Data Transfer enumeration.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/FindNearest/v1/NaptanType.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:26   mturner
//Initial revision.
//
//   Rev 1.2   Feb 01 2006 18:50:22   schand
//Code review changes
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 04 2006 12:08:40   mtillett
//Create stubs for data transfer class
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.0   Jan 04 2006 12:07:34   mtillett
//Initial revision.

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.FindNearest.V1
{
	public enum NaptanType
	{
		Airport,
		Station,
		BusStop
	}
}
