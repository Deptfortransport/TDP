using System;
using TransportDirect.UserPortal.DepartureBoardService;   
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using TransportDirect.UserPortal.DepartureBoardService.RTTIManager ;
 
namespace TransportDirect.UserPortal.DepartureBoardService.RTTIManager
{
	/// <summary>
	/// Summary description for IRTTILookupHandler.
	/// </summary>
	public interface IRTTILookupHandler
	{
		
		
		bool DataLookUpAvailable {get;}
		string GetOperatorName(string operatorCode);
		string GetReasonDescription(string reasonCode);


	}
}
