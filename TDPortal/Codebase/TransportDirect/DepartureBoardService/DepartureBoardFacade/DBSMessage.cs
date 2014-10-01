// *********************************************** 
// NAME                 : DBSMessage.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 30/12/2004
// DESCRIPTION  : Class for Departure Board Message information
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardFacade/DBSMessage.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:24   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:21:36   passuied
//Initial revision.
//
//   Rev 1.0   Dec 30 2004 14:23:42   passuied
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade
{
	/// <summary>
	/// Class for Departure Board Message information
	/// </summary>
    [Serializable]
	public class DBSMessage
	{
		private int nCode;
		private string sDescription;

		public DBSMessage()
		{ 
			nCode = -1;
			sDescription = string.Empty;
		}

		public int Code
		{
			get{ return nCode;}
			set{ nCode = value;}
		}

		public string Description
		{
			get{ return sDescription;}
			set{ sDescription = value;}
		}
	}
}
