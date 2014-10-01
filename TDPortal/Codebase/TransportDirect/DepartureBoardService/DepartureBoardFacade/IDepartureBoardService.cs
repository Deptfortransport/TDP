// *********************************************** 
// NAME                 : IDepartureBoardService.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 30/12/2004
// DESCRIPTION  : Interface for DepartureBoardService
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardFacade/IDepartureBoardService.cs-arc  $
//
//   Rev 1.1   Feb 17 2010 16:42:22   RHopkins
//Pass OperatorCode in StopEvent request
//Resolution for 5403: Pass OperatorCode in StopEvents request
//
//   Rev 1.0   Nov 08 2007 12:21:28   mturner
//Initial revision.
//
//   Rev 1.1   Jun 27 2005 18:24:10   passuied
//added method in interface and implementation to get the Time request types to display in mobile UI. Retrieved from the DataServices
//Resolution for 2561: Del 8 Stream: Create WAP Prototype pages
//
//   Rev 1.0   Feb 28 2005 16:21:38   passuied
//Initial revision.
//
//   Rev 1.3   Jan 17 2005 14:48:02   passuied
//changes in the interface
//
//   Rev 1.2   Jan 14 2005 10:19:54   passuied
//Changes in interface
//
//   Rev 1.1   Jan 05 2005 10:01:06   passuied
//changes to the interface...
//
//   Rev 1.0   Dec 30 2004 14:24:28   passuied
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade
{
	/// <summary>
	/// Interface for DepartureBoardService
	/// </summary>
	public interface IDepartureBoardService
	{
		/// <summary>
		/// Trip level request method for DepartureBoard information
		/// </summary>
		/// <param name="token">Authentications token</param>
		/// <param name="originLocation">naptanId of Origin</param>
		/// <param name="destinationLocation">naptanId of Destination</param>
		/// <param name="type">type of DepartureBoard requested</param>
		/// <param name="serviceNumber"> requested service number (optional)</param>
		/// <param name="time"> start time</param>
		/// <param name="rangeType">type of the range for the request (sequence, interval)</param>
		/// <param name="range"> max number of returned results</param>
		/// <param name="showDepartures"> show departure times / show arrival times</param>
		/// <param name="showCallingStops">show calling stops if available</param>
		/// <returns></returns>
		DBSResult GetDepartureBoardTrip(
			string token,
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
		/// <param name="token">Authentication token</param>
		/// <param name="stopNaptan">NaptanId of the stop</param>
		/// <param name="type">type of departure board requested</param>
		/// <param name="serviceNumber"> service number requested (optional)</param>
		/// <param name="showDepartures"> show departure times / arrival times</param>
		/// <param name="showCallingStops"> show calling stops if available</param>
		/// <returns></returns>
		DBSResult GetDepartureBoardStop(
			string token,
			DBSLocation stopLocation,
            string operatorCode,
            string serviceNumber,
			bool showDepartures,
			bool showCallingStops);

		/// <summary>
		/// Use the dataService to return the Time Request Types to display in the UI
		/// </summary>
		/// <returns>an array of TimeRequestType objects sorted in the order to be displayed. The firsts one being the default one.</returns>
		TimeRequestType[] GetTimeRequestTypesToDisplay();

	}
}
