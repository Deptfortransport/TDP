// *********************************************** 
// NAME			: RoadJourneyDetailMapInfo.cs
// AUTHOR		: Andrew Sinclair
// DATE CREATED	: 17/02/2005 
// DESCRIPTION	: Container class for toids returned for car legs
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/RoadJourneyDetailMapInfo.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:58   mturner
//Initial revision.
//
//   Rev 1.0   Mar 01 2005 15:37:48   asinclair
//Initial revision.
using System;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for RoadJourneyDetailMapInfo.
	/// </summary>
	[Serializable()]
	public class RoadJourneyDetailMapInfo
	{
		private string FirstToid;
		private string LastToid;
		//private string toid;

		public RoadJourneyDetailMapInfo(string firsttoid, string lasttoid)
		{
			FirstToid = firsttoid;
			LastToid = lasttoid;
		}

		public string firstToid
		{
			get { return FirstToid; }
		}

		public string lastToid
		{
			get { return LastToid; }
		}

//		public string[] Toid
//		{
//			get { return toid[]; }
//			set { toid[] = value; }
//		}
	}
}
