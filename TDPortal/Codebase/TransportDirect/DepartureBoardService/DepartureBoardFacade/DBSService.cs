// *********************************************** 
// NAME                 : DBSService.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 30/12/2004
// DESCRIPTION  : Class for Service information
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardFacade/DBSService.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:26   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:21:36   passuied
//Initial revision.
//
//   Rev 1.0   Dec 30 2004 14:24:04   passuied
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade
{
	/// <summary>
	/// Class for Service information
	/// </summary>
    [Serializable]
	public class DBSService
	{
		private string sOperatorCode;
		private string sOperatorName;
		private string sServiceNumber;

		public DBSService()
		{
			sOperatorCode = string.Empty;
			sOperatorName = string.Empty;
			sServiceNumber = string.Empty;
		}

		public string OperatorCode
		{
			get{ return sOperatorCode;}
			set{ sOperatorCode = value;}
		}

		public string OperatorName
		{
			get{ return sOperatorName;}
			set{ sOperatorName = value;}
		}

		public string ServiceNumber
		{
			get{ return sServiceNumber;}
			set{ sServiceNumber = value;}
		}
	}
}
