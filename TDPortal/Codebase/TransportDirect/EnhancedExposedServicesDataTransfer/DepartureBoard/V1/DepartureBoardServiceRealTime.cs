// *********************************************** 
// NAME                 : DepartureBoardServiceRealTime.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/12/2005 
// DESCRIPTION  		: Class for Departure Board Service Realtime information
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/DepartureBoard/V1/DepartureBoardServiceRealTime.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:22   mturner
//Initial revision.
//
//   Rev 1.0   Jan 27 2006 16:30:48   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 20 2006 16:24:36   schand
//Added more comments and Serialization attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Dec 14 2005 15:38:36   schand
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.DepartureBoard.V1
{
	/// <summary>
	/// Class for Departure Board Service Realtime information
	/// </summary>
	[System.Serializable]
	public class DepartureBoardServiceRealTime
	{
		#region  Private Members   		
		private DateTime dtDepartTime;
		private DepartureBoardServiceRealTimeType rttDepartTimeType;
		private DateTime dtArriveTime;
		private DepartureBoardServiceRealTimeType rttArriveTimeType;
		#endregion


		#region Constructor
		
		/// <summary>
		/// Default constructor to initialise varaible to their default state
		/// </summary>
		public DepartureBoardServiceRealTime()
		{
			dtDepartTime = DateTime.MinValue;
			dtArriveTime = DateTime.MinValue;
			rttDepartTimeType = DepartureBoardServiceRealTimeType.Estimated;
			rttArriveTimeType = DepartureBoardServiceRealTimeType.Estimated;
		}
		#endregion

		#region  Public Properties

		/// <summary>
		/// Read-Write property for Departure Real Time 
		/// </summary>
		public DateTime DepartTime
		{
			get{ return dtDepartTime;}
			set{ dtDepartTime = value;}
		}

		/// <summary>
		/// Read-Write property for Departure Board Service Departure Real Time Type (Actual or Estimated)
		/// </summary>
		public DepartureBoardServiceRealTimeType DepartTimeType
		{
			get{ return rttDepartTimeType; }
			set{ rttDepartTimeType = value;}
		}

		/// <summary>
		/// Read-Write property for Arrival Real Time 
		/// </summary>
		public DateTime ArriveTime
		{
			get{ return dtArriveTime;}
			set{ dtArriveTime = value;}
		}

		/// <summary>
		/// Read-Write property for Departure Board Service Arrival Real Time Type (Actual or Estimated)
		/// </summary>
		public DepartureBoardServiceRealTimeType ArriveTimeType
		{
			get{ return rttArriveTimeType;}
			set{ rttArriveTimeType = value;}
		}
		#endregion
		
	}
}
