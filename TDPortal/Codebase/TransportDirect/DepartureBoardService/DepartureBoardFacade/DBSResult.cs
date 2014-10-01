// *********************************************** 
// NAME                 : DBSResult.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 30/12/2004
// DESCRIPTION  : Root Class for DepartureBoard result information
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardFacade/DBSResult.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:26   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:21:36   passuied
//Initial revision.
//
//   Rev 1.1   Jan 10 2005 15:25:34   passuied
//changes in initialisation
//
//   Rev 1.0   Dec 30 2004 14:24:00   passuied
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade
{
	/// <summary>
	/// Root Class for DepartureBoard result information
	/// </summary>
    [Serializable]
	public class DBSResult
	{
		private DBSStopEvent[] seStopEvents;
		private DBSMessage[] dbmMessages;

		public DBSResult()
		{
			seStopEvents =  new DBSStopEvent[0];
			dbmMessages = new DBSMessage[0];
		}

		public DBSStopEvent[] StopEvents
		{
			get{ return seStopEvents;}
			set{ seStopEvents = value;}
		}

		public DBSMessage[] Messages
		{
			get{ return dbmMessages;}
			set{ dbmMessages = value;}
		}
	}
}
