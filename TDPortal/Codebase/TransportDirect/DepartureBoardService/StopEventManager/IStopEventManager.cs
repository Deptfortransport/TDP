// *********************************************** 
// NAME                 : IStopEventManager.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 05/01/2005
// DESCRIPTION  : Interface for the StopEvent Manager.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/StopEventManager/IStopEventManager.cs-arc  $
//
//   Rev 1.1   Feb 17 2010 16:42:24   RHopkins
//Pass OperatorCode in StopEvent request
//Resolution for 5403: Pass OperatorCode in StopEvents request
//
//   Rev 1.0   Nov 08 2007 12:21:44   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:23:58   passuied
//Initial revision.
//
//   Rev 1.1   Jan 14 2005 10:21:32   passuied
//changes in interface
//
//   Rev 1.0   Jan 05 2005 16:52:10   passuied
//Initial revision.

using System;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;


namespace TransportDirect.UserPortal.DepartureBoardService.StopEventManager
{
	/// <summary>
	/// Interface for the StopEvent Manager.
	/// </summary>
	public interface IStopEventManager
	{
		/// <summary>
		/// Trip level request method for DepartureBoard information
		/// </summary>
		/// <param name="originLocation">Location Information for origin</param>
		/// <param name="destinationLocation">Location Informaiton for destination</param>
		/// <param name="type">type of DepartureBoard requested</param>
		/// <param name="serviceNumber"> requested service number (optional)</param>
		/// <param name="time"> start time</param>
		/// <param name="rangeType">type of the range for the request (sequence, interval)</param>
		/// <param name="range"> max number of returned results</param>
		/// <param name="showDepartures"> show departure times / show arrival times</param>
		/// <param name="showCallingStops">show calling stops if available</param>
		/// <returns></returns>
		DBSResult GetDepartureBoardTrip(
			DBSLocation originLocation,
			DBSLocation destinationLocation,
            string operatorCode,
            string serviceNumber,
			DBSTimeRequest time,
			DBSRangeType rangeType,
			int range,
			bool showDepartures,
			bool showCallingStops);
		
		
		/// <summary>
		/// Stop level request method for DepartureBoard Information
		/// </summary>
		/// <param name="stopLocation">Location information for the stop</param>
		/// <param name="type">type of departure board requested</param>
		/// <param name="serviceNumber"> service number requested (optional)</param>
		/// <param name="showDepartures"> show departure times / arrival times</param>
		/// <param name="showCallingStops"> show calling stops if available</param>
		/// <returns></returns>
		DBSResult GetDepartureBoardStop(
			DBSLocation stopLocation,
            string operatorCode,
            string serviceNumber,
			bool showDepartures,
			bool showCallingStops);

	}
}
