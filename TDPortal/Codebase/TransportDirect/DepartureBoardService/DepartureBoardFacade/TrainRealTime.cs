// *********************************************** 
// NAME                 : TrainRealTime.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 30/12/2004
// DESCRIPTION  : Class for Train RealTime information
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardFacade/TrainRealTime.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:28   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:21:38   passuied
//Initial revision.
//
//   Rev 1.2   Jan 14 2005 21:25:56   schand
//Moved LateReason from TrainRealTime to TrainStopEvent 
//
//   Rev 1.1   Jan 10 2005 10:38:32   passuied
//added missing properties to Train specific classes
//
//   Rev 1.0   Dec 30 2004 14:24:34   passuied
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade
{
	/// <summary>
	/// Class for Train RealTime information
	/// </summary>
    [Serializable]
	public class TrainRealTime : DBSRealTime
	{
		private bool isDelayed;
		private bool isUncertain;		

		public TrainRealTime() : base ()
		{
			isDelayed = false;
			isUncertain = false;			
		}

		public bool Delayed
		{
			get{ return isDelayed;}
			set{ isDelayed = value;}
		}

		public bool Uncertain
		{
			get{ return isUncertain;}
			set{ isUncertain = value;}
		}

		


	}
}
