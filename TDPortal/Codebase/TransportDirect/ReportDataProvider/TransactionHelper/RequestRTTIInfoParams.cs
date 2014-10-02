// *********************************************** 
// NAME                 : RequestRTTIInfoParams.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 06/04/2005 
// DESCRIPTION  		: Implementation of the RequestRTTIInfoParams class. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionHelper/RequestRTTIInfoParams.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:39:44   mturner
//Initial revision.
//
//   Rev 1.0   Apr 09 2005 15:08:18   schand
//Initial revision.


using System;

namespace TransportDirect.ReportDataProvider.TransactionHelper
{
	/// <summary>
	/// Summary description for RTTIRequestInfoParams.
	/// </summary>
	public struct RequestRTTIInfoParams
	{
		public string origin;
		public string destination;
		public string serviceNumber;
	    public bool showDepartures; 
		public bool showCallingPoints;
	}
}
