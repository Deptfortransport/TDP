// *********************************************** 
// NAME			: RequestJourneySleepParams.cs
// AUTHOR		: Patrick Assuied
// DATE CREATED	: 10/06/04 
// DESCRIPTION	: Provides conversion methods to
// convert Transaction Injector parameters between
// string format and binary format. This is necessary
// in order to ensure parameters are passed to 
// TD Transaction Web Service correctly.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionHelper/RequestJourneySleepParams.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:42   mturner
//Initial revision.
//
//   Rev 1.0   Jun 10 2004 17:08:16   passuied
//Initial Revision
using System;

namespace TransportDirect.ReportDataProvider.TransactionHelper
{
	/// <summary>
	/// Summary description for RequestJourneySleepParams.
	/// </summary>
	public struct RequestJourneySleepParams
	{
		public int sleep;
	}
}
