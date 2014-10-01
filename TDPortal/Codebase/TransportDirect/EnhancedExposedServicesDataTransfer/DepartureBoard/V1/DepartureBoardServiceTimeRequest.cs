// *********************************************** 
// NAME                 : DepartureBoardServiceTimeRequest.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/12/2005 
// DESCRIPTION  		: Departure Board Service Time Request Class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/DepartureBoard/V1/DepartureBoardServiceTimeRequest.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:24   mturner
//Initial revision.
//
//   Rev 1.0   Jan 27 2006 16:30:52   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 20 2006 16:24:38   schand
//Added more comments and Serialization attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Dec 14 2005 15:38:38   schand
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements


using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.DepartureBoard.V1
{
	/// <summary>
	/// Departure Board Service Time Request Class.
	/// </summary>
	[System.Serializable]
	public class DepartureBoardServiceTimeRequest
	{
		#region  Private Members
		
		DepartureBoardServiceTimeRequestType trType;
		int nHour;
		int nMinute;
		#endregion


		#region Constructor

		/// <summary>
		/// Default constructor to initialise varaible to their default state
		/// </summary>
		public DepartureBoardServiceTimeRequest()
		{
			trType = DepartureBoardServiceTimeRequestType.Now;
			nHour = 0;
			nMinute = 0;
		}
		#endregion


		#region  Private properties

		/// <summary>
		/// Read-Write property for Departure Board Service Time Request Type
		/// </summary>
		public DepartureBoardServiceTimeRequestType Type
		{
			get{ return trType;}
			set{ trType = value;}
		}

		/// <summary>
		/// Read-Write property for Departure Board Service Arrival/Departure Hour
		/// </summary>
		public int Hour
		{
			get{ return nHour;}
			set{ nHour = value;}
		}

		/// <summary>
		/// Read-Write property for Departure Board Service Arrival/Departure Minute
		/// </summary>
		public int Minute
		{
			get{ return nMinute;}
			set{ nMinute = value;}
		}
		#endregion
            
	}
}
