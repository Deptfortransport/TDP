// ****************************************************************
// NAME         : IJourneyPlanRunnerCaller.cs
// AUTHOR       : Andrew Sinclair
// DATE CREATED : 2005-06-06
// DESCRIPTION  : An interface to allow the JourneyPlanRunnerCaller 
// to be stubbed out for unit testing purposes.
// ****************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/IJourneyPlanRunnerCaller.cs-arc  $  
//
//   Rev 1.2   Sep 01 2011 10:43:32   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.1   Dec 21 2010 14:05:06   apatel
//Code updated to request services for the day of travel starting from 01:00 on the current day to 01:00 the following day for Find a train, Find a flight and City to City requests
//Resolution for 5651: CCN 593 - Show 10 results or show all
//
//   Rev 1.0   Nov 08 2007 12:24:42   mturner
//Initial revision.
//
//   Rev 1.2   Jun 30 2005 16:37:26   asinclair
//Checked in after FXCop Changes
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.1   Jun 21 2005 14:57:00   asinclair
//Removed IsTrunkRequest - no longer needed
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.0   Jun 15 2005 14:21:04   asinclair
//Initial revision.

using System;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;


namespace TransportDirect.UserPortal.JourneyPlanRunner
{
	/// <summary>
	/// Summary description for IJourneyPlanRunnerCaller.
	/// </summary>
	public interface IJourneyPlanRunnerCaller
	{

		///<summary>
		///Invoke the CJP Manager for a new journey request
		///</summary>
		void InvokeCJPManager(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition, string lang,
			Guid cjpRequestId, TDJourneyParameters tdJourneyParameters, ITDJourneyParameterConverter converter,
			TDDateTime outwardDateTime, TDDateTime returnDateTime, bool isExtension, FindAPlannerMode findAMode);


		///<summary>
		///Invoke the CJP Manager for amendments to a journey
		///</summary>
		void InvokeCJPManager(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition, Guid cjpRequestId, int referenceNumber,
			int lastSequenceNumber, ITDJourneyRequest tdJourneyRequest, bool modifyOriginalResult);

	}
}
