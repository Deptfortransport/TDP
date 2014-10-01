// *********************************************** 
// NAME                 : DBSEvent.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 30/12/2004
// DESCRIPTION  : Class for Event information
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardFacade/DBSEvent.cs-arc  $
//
//   Rev 1.1   Jun 15 2010 12:48:44   apatel
//Updated to add new  "Cancelled" attribute to the DBSEvent object
//Resolution for 5554: Departure Board service detail page cancelled train issue
//
//   Rev 1.0   Nov 08 2007 12:21:22   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:21:36   passuied
//Initial revision.
//
//   Rev 1.1   Jan 06 2005 10:49:38   passuied
//fixed DBSEvent and DBSStopEvent design... Service is component of DBSStopEvent!!!	
//
//   Rev 1.0   Dec 30 2004 14:23:36   passuied
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade
{
	/// <summary>
	/// Class for Event information
	/// </summary>
    [Serializable]
	public class DBSEvent
	{
		private DBSActivityType atActivityType;
		private DateTime dtDepartTime;
		private DateTime dtArriveTime;
		private DBSRealTime rtRealTime;
		private DBSStop stStop;
        private bool cancelled = false;

		public DBSEvent()
		{
			atActivityType = DBSActivityType.Depart;
			dtDepartTime = DateTime.MinValue;
			dtArriveTime = DateTime.MinValue;
			rtRealTime = null;
			stStop = null;
		}

		public DBSActivityType ActivityType
		{
			get{ return atActivityType;}
			set{ atActivityType = value;}
		}

		public DateTime DepartTime
		{
			get{ return dtDepartTime;}
			set{ dtDepartTime = value;}
		}

		public DateTime ArriveTime
		{
			get{ return dtArriveTime;}
			set{ dtArriveTime = value;}
		}

		public DBSRealTime RealTime
		{
			get{ return rtRealTime;}
			set{ rtRealTime = value;}
		}

		public DBSStop Stop
		{
			get{ return stStop;}
			set{ stStop = value;}
		}

        public bool Cancelled
        {
            get { return cancelled; }
            set { cancelled = value; }
        }

	}
}
