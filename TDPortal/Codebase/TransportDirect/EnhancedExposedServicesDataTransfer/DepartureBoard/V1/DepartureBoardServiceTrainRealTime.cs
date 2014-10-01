// *********************************************** 
// NAME                 : DepartureBoardServiceTrainRealTime.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/12/2005 
// DESCRIPTION  		: Departure Board Service Train RealTime information
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/DepartureBoard/V1/DepartureBoardServiceTrainRealTime.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:24   mturner
//Initial revision.
//
//   Rev 1.0   Jan 27 2006 16:30:52   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 20 2006 16:24:40   schand
//Added more comments and Serialization attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Dec 14 2005 15:38:40   schand
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.DepartureBoard.V1
{
	/// <summary>
	/// Class for Train RealTime information
	/// </summary>
	[System.Serializable]
	public class DepartureBoardServiceTrainRealTime :  DepartureBoardServiceRealTime
	{
		#region  Private Members
		
		private bool isDelayed;
		private bool isUncertain;		
		#endregion


		#region Constructor
		

		/// <summary>
		/// Default constructor to initialise varaible to their default state
		/// </summary>
		public DepartureBoardServiceTrainRealTime() : base ()
		{
			isDelayed = false;
			isUncertain = false;			
		}

		#endregion        

		#region  Public Properties

		/// <summary>
		/// Read-Write property to indicate whether train is delayed
		/// </summary>
		public bool Delayed
		{
			get{ return isDelayed;}
			set{ isDelayed = value;}
		}

		/// <summary>
		/// Read-Write property to indicate whether train status is uncertain.
		/// </summary>
		public bool Uncertain
		{
			get{ return isUncertain;}
			set{ isUncertain = value;}
		} 
		#endregion        
	}
}
