// *********************************************** 
// NAME                 : TrainStopEvent.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 30/12/2004
// DESCRIPTION  : Root Class for Train Stop/Event-related information
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardFacade/TrainStopEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:28   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:21:38   passuied
//Initial revision.
//
//   Rev 1.2   Jan 14 2005 21:25:52   schand
//Moved LateReason from TrainRealTime to TrainStopEvent 
//
//   Rev 1.1   Jan 10 2005 10:38:42   passuied
//added missing properties
//
//   Rev 1.0   Dec 30 2004 14:24:38   passuied
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade
{
	/// <summary>
	/// Root Class for Train Stop/Event-related information
	/// </summary>
    [Serializable]
	public class TrainStopEvent : DBSStopEvent
	{

		private bool isCircularRoute;
		private string sFalseDestination;
		private string sVia;
		private bool isCancelled;
		private string sCancellationReason;
		private string sLateReason;

		public TrainStopEvent() : base()
		{
			isCircularRoute = false;
			sFalseDestination = string.Empty;
			sVia = string.Empty;
			isCancelled = false;
			sCancellationReason = string.Empty;
			sLateReason = string.Empty;
		}

		public bool CircularRoute
		{
			get{ return isCircularRoute;}
			set{ isCircularRoute = value;}
		}

		public string FalseDestination
		{
			get{ return sFalseDestination;}
			set{ sFalseDestination = value;}
		}

		public string Via
		{
			get{ return sVia;}
			set{ sVia = value;}
		}

		public bool Cancelled
		{
			get{ return isCancelled;}
			set{ isCancelled = value;}
		}

		public string CancellationReason
		{
			get{ return sCancellationReason;}
			set{ sCancellationReason = value;}
		}

		public string LateReason
		{
			get{ return sLateReason;}
			set{ sLateReason = value;}
		}
	}
}
