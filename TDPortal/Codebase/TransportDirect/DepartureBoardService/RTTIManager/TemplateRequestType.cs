// *********************************************** 
// NAME                 : TemplateRequestType.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/01/2005 
// DESCRIPTION  		: This enum contains list of RTTI request type
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/RTTIManager/TemplateRequestType.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:42   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:23:08   passuied
//Initial revision.
//
//   Rev 1.0   Jan 21 2005 14:21:40   schand
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.RTTIManager
{
	/// <summary>
	/// Summary description for TemplateRequestType.
	/// </summary>	
	public enum TemplateRequestType
	{
		StationRequestByTiploc,
		TripRequestByTiploc,
		StationRequestByCRS,
		TripRequestByCRS,
		TrainRequestByRID,
		ErrorResponse,
	}
	
}
