// *********************************************** 
// NAME                 : IRDHandler.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 31/12/2004
// DESCRIPTION  : Interface for RTTI data handler
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/RTTIManager/IRDHandler.cs-arc  $ 
//
//   Rev 1.1   Feb 17 2010 16:42:24   RHopkins
//Pass OperatorCode in StopEvent request
//Resolution for 5403: Pass OperatorCode in StopEvents request
//
//   Rev 1.0   Nov 08 2007 12:21:36   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:23:04   passuied
//Initial revision.
//
//   Rev 1.6   Feb 11 2005 11:35:54   schand
//IRDHandler's public interface has been changed.
//i.e. returning DBSResult object instead of boolean.
//UpdateRequested counter is demoted to private 
//
//   Rev 1.5   Jan 18 2005 19:57:46   schand
//Interface has been changed. Added addition param: duration


using System;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade ;   

namespace TransportDirect.UserPortal.DepartureBoardService.RTTIManager

{
	/// <summary>
	/// Summary description for IRDHandler.
	/// </summary>
	public interface IRDHandler
	{	
		/// <summary>
		/// Station level request method for DepartureBoard information
		/// </summary>
		/// <param name="stopLocation">DBSlocation type which contains a CRS and collection of Naptan Id for station</param>
		/// <param name="serviceNumber"> RID for train. This is an optional parameter. If value is provided for this param then get the infor for train</param>
		/// <param name="showDepartures"> boolean flag to indicate whether to show departure or arrival. Bydefault is false i.e. show arrival</param>		
		/// <param name="showCallingStops"> boolean flag to indicate whether to show calling points or not</param>				
		/// <returns>DBSResult which consists of Data or Messages </returns>
		DepartureBoardFacade.DBSResult GetDepartureBoardStop(
			DBSLocation stopLocation, string operatorCode, string serviceNumber, bool showDepartures, 
			bool showCallingStops);		
		

		/// <summary>
		/// Station level request method for DepartureBoard information
		/// </summary>
		/// <param name="originLocation">DBSlocation type which contains a CRS and collection of Naptan Id for station</param>
		/// <param name="destinationLocation">DBSlocation type which contains a CRS and collection of Naptan Id for station</param>
		/// <param name="serviceNumber"> RID for train. This is an optional parameter. If value is provided for this param then get the infor for train</param>
		/// <param name="requestedTime"> DBSTimeRequest contains Hour, Minute & TimeRequestType. </param>
		/// <param name="rangeType"> Sequence or Interval </param>
		/// <param name="range"> number  of response requested or response for an interval </param>
		/// <param name="showDepartures"> boolean flag to indicate whether to show departure or arrival. Bydefault is false i.e. show arrival</param>				
		/// <param name="showCallingStops"> boolean flag to indicate whether to show calling points or not</param>				
		/// <returns>DBSResult which consists of Data or Messages </returns>
		DepartureBoardFacade.DBSResult GetDepartureBoardTrip(
			DBSLocation originLocation, DBSLocation destinationLocation,
            string operatorCode, string serviceNumber, DepartureBoardFacade.DBSTimeRequest requestedTime, DBSRangeType rangeType, int range, 
			bool showDepartures, bool showCallingStops);

	}
}
