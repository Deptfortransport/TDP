// *********************************************** 
// NAME                 : DBSStop.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 30/12/2004
// DESCRIPTION  : Class for Stop Information
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardFacade/DBSStop.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:26   mturner
//Initial revision.
//
//   Rev 1.1   Apr 01 2005 16:08:42   schand
//Added ShortCode property. Fix for 5.7, 5.8
//
//   Rev 1.0   Feb 28 2005 16:21:38   passuied
//Initial revision.
//
//   Rev 1.0   Dec 30 2004 14:24:10   passuied
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade
{
	/// <summary>
	/// Class for Stop Information
	/// </summary>
    [Serializable]
	public class DBSStop
	{
		private string sNaptanId;
		private string sName;
		private string sShortCode;

		public DBSStop()
		{
			sNaptanId = string.Empty;
			sName = string.Empty;
			sShortCode = string.Empty; 
		}

		public string NaptanId
		{
			get{ return sNaptanId;}
			set{ sNaptanId = value;}
		}

		public string Name
		{
			get{ return sName;}
			set{ sName = value;}
		}

		public string ShortCode
		{
			get{ return sShortCode;}
			set{ sShortCode = value;}
		}
	}
}
