// *********************************************** 
// NAME                 : DepartureBoardServiceInformation.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/12/2005 
// DESCRIPTION  		: Class for Departure Board Service Information
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/DepartureBoard/V1/DepartureBoardServiceInformation.cs-arc  $ 
//
//   Rev 1.1   Jun 15 2010 12:48:46   apatel
//Updated to add new  "Cancelled" attribute to the DBSEvent object
//Resolution for 5554: Departure Board service detail page cancelled train issue
//
//   Rev 1.0   Nov 08 2007 12:22:20   mturner
//Initial revision.
//
//   Rev 1.0   Jan 27 2006 16:30:46   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 20 2006 16:24:18   schand
//Added more comments and Serialization attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Dec 14 2005 15:38:32   schand
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.DepartureBoard.V1
{
	/// <summary>
	/// Departure Board Service Information
	/// </summary>
	[System.Serializable]
	public class DepartureBoardServiceInformation
	{
		#region Private Members
		private DepartureBoardServiceActivityType atActivityType;
		private DateTime dtDepartTime;
		private DateTime dtArriveTime;
		private DepartureBoardServiceRealTime rtRealTime;
		private DepartureBoardServiceStop stStop;
        private bool cancelled = false;
		#endregion

		#region Constructor
		/// <summary>
		/// Default constructor to initialise varaible to their default state
		/// </summary>
		public DepartureBoardServiceInformation()
		{
			atActivityType = DepartureBoardServiceActivityType.Depart;
			dtDepartTime = DateTime.MinValue;
			dtArriveTime = DateTime.MinValue;
			rtRealTime = null;
			stStop = null;
            cancelled = false;
		}
		#endregion


		#region Public properties

		/// <summary>
		/// Read-Write property for Departure Board Service ActivityType 
		/// </summary>
		public DepartureBoardServiceActivityType ActivityType
		{
			get{ return atActivityType;}
			set{ atActivityType = value;}
		}

		/// <summary>
		/// Read-Write property for Departure Time 
		/// </summary>
		public DateTime DepartTime
		{
			get{ return dtDepartTime;}
			set{ dtDepartTime = value;}
		}

		/// <summary>
		/// Read-Write property for Arrival Time 
		/// </summary>
		public DateTime ArriveTime
		{
			get{ return dtArriveTime;}
			set{ dtArriveTime = value;}
		}

		/// <summary>
		/// Read-Write property for Real Time information like 
		/// actual/estimated arrival time 
		/// </summary>
		public DepartureBoardServiceRealTime RealTime
		{
			get{ return rtRealTime;}
			set{ rtRealTime = value;}
		}

		/// <summary>
		/// Read-Write property for Departure Board Stop 
		/// which includes the information about stop ex. NaptanId
		/// </summary>
		public DepartureBoardServiceStop Stop
		{
			get{ return stStop;}
			set{ stStop = value;}
		}

        /// <summary>
        /// Read-Write property determining if the service is cancelled 
        /// for stop represented by this DBS event
        /// </summary>
        public bool Cancelled
        {
            get { return cancelled; }
            set { cancelled = value; }
        }
		#endregion


	}
}
