// *********************************************** 
// NAME                 : DBSTimeRequest.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 13/01/2005
// DESCRIPTION  : Class holding time information for the service
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardFacade/DBSTimeRequest.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:26   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:21:38   passuied
//Initial revision.
//
//   Rev 1.0   Jan 14 2005 10:17:06   passuied
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade
{
	/// <summary>
	/// Class holding time information for the service
	/// </summary>
	public class DBSTimeRequest
	{
		TimeRequestType trType;
		int nHour;
		int nMinute;

		public DBSTimeRequest()
		{
			trType = TimeRequestType.Now;
			nHour = 0;
			nMinute = 0;
		}

		public TimeRequestType Type
		{
			get{ return trType;}
			set{ trType = value;}
		}

		public int Hour
		{
			get{ return nHour;}
			set{ nHour = value;}
		}

		public int Minute
		{
			get{ return nMinute;}
			set{ nMinute = value;}
		}


	}
}
