// *********************************************** 
// NAME                 : DBSRealTime.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 30/12/2004
// DESCRIPTION  : Class for Realtime information
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardFacade/DBSRealTime.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:24   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:21:36   passuied
//Initial revision.
//
//   Rev 1.0   Dec 30 2004 14:23:48   passuied
//Initial revision.

using System;
using System.Xml.Serialization;

namespace TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade
{
	/// <summary>
	/// Class for Realtime information
	/// </summary>
    [XmlInclude(typeof(TrainRealTime))] // Define derived types to allow serialisation
    [Serializable]
	public class DBSRealTime
	{
		private DateTime dtDepartTime;
		private DBSRealTimeType rttDepartTimeType;
		private DateTime dtArriveTime;
		private DBSRealTimeType rttArriveTimeType;

		/// <summary>
		/// Default constructor
		/// </summary>
		public DBSRealTime()
		{
			dtDepartTime = DateTime.MinValue;
			dtArriveTime = DateTime.MinValue;
			rttDepartTimeType = DBSRealTimeType.Estimated;
			rttArriveTimeType = DBSRealTimeType.Estimated;
		}

		public DateTime DepartTime
		{
			get{ return dtDepartTime;}
			set{ dtDepartTime = value;}
		}

		public DBSRealTimeType DepartTimeType
		{
			get{ return rttDepartTimeType; }
			set{ rttDepartTimeType = value;}
		}

		public DateTime ArriveTime
		{
			get{ return dtArriveTime;}
			set{ dtArriveTime = value;}
		}

		public DBSRealTimeType ArriveTimeType
		{
			get{ return rttArriveTimeType;}
			set{ rttArriveTimeType = value;}
		}
		
	}
}
