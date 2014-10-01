// *********************************************** 
// NAME                 : DepartureBoardServiceStop.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/12/2005 
// DESCRIPTION  		: Departure Board Service Stop Information Class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/DepartureBoard/V1/DepartureBoardServiceStopInformation.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:24   mturner
//Initial revision.
//
//   Rev 1.0   Jan 27 2006 16:30:52   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.2   Jan 20 2006 16:24:38   schand
//Added more comments and Serialization attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 12 2006 20:00:30   schand
//Added some properties for train specific details
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.0   Dec 14 2005 15:38:38   schand
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements

using System;
using System.Collections;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.DepartureBoard.V1
{
    	
	/// <summary>
	///  Departure Board Service Stop Information Class.
	/// </summary>
	[System.Serializable]	
	public class DepartureBoardServiceStopInformation
	{
		#region  Private Members
		private DepartureBoardServiceCallingStopStatus cssCallingStopStatus;  		
		private DepartureBoardServiceInformation dbeDeparture;
		private DepartureBoardServiceInformation[] dbePreviousIntermediates;
		private DepartureBoardServiceInformation dbeStop;
		private DepartureBoardServiceInformation[] dbeOnwardIntermediates;
		private DepartureBoardServiceInformation dbeArrival;
		private DepartureBoardServiceItinerary svService;
		private DepartureBoardServiceType mode;
		// Including TrainStop Information here 
		private bool isCircularRoute;
		private string sFalseDestination;
		private string sVia;
		private bool isCancelled;
		private string sCancellationReason;
		private string sLateReason;
		private bool hasTrainDetails;
		#endregion


		#region Constructor
		
		/// <summary>
		/// Default constructor to initialise varaible to their default state
		/// </summary>
		public DepartureBoardServiceStopInformation()
		{
			cssCallingStopStatus = DepartureBoardServiceCallingStopStatus.Unknown;
			dbeDeparture = null;
			dbePreviousIntermediates = new DepartureBoardServiceInformation[0];
			dbeStop = null;
			dbeOnwardIntermediates = new DepartureBoardServiceInformation[0];
			dbeArrival = null;

			isCircularRoute = false;
			sFalseDestination = string.Empty;
			sVia = string.Empty;
			isCancelled = false;
			sCancellationReason = string.Empty;
			sLateReason = string.Empty;
			hasTrainDetails = false;
		}
		#endregion
		

		#region  Public Properties

		/// <summary>
		/// Read-Write property for Departure Board Service Calling Stop Status
		/// </summary>
		public DepartureBoardServiceCallingStopStatus CallingStopStatus
		{
			get{ return cssCallingStopStatus;}
			set{ cssCallingStopStatus = value;}
		}
		
		/// <summary>
		/// Read-Write property for Departure Board Service Departure   Stop
		/// </summary>
		public DepartureBoardServiceInformation Departure
		{
			get{ return dbeDeparture;}
			set{ dbeDeparture = value;}
		}

		/// <summary>
		/// Read-Write property for Departure Board Service Previous Stops
		/// </summary>
		public DepartureBoardServiceInformation[] PreviousIntermediates
		{
			get{ return dbePreviousIntermediates;}
			set{ dbePreviousIntermediates = value;}
		}

		
		/// <summary>
		/// Read-Write property for Departure Board Service Stop Information 
		/// </summary>
		public DepartureBoardServiceInformation Stop
		{
			get{ return dbeStop;}
			set{ dbeStop = value;}
		}

		/// <summary>
		/// Read-Write property for Departure Board Service Onward/Later Stops
		/// </summary>
		public DepartureBoardServiceInformation[] OnwardIntermediates
		{
			get{ return dbeOnwardIntermediates;}
			set{ dbeOnwardIntermediates = value;}
		}

		/// <summary>
		/// Read-Write property for Departure Board Service Arrival Stop
		/// </summary>
		public DepartureBoardServiceInformation Arrival
		{
			get{ return dbeArrival;}
			set{ dbeArrival = value;}
		}

		/// <summary>
		/// Read-Write property for Departure Board Service Itinerary
		/// </summary>
		public DepartureBoardServiceItinerary Service
		{
			get{ return svService;}
			set{ svService = value;}
		}

		/// <summary>
		/// Read-Write property for Departure Board Service Mode
		/// </summary>
		public DepartureBoardServiceType Mode
		{
			get{return mode;}
			set{mode = value;}
		}

		/// <summary>
		/// Read-Write property to indicate whether train has a circular route
		/// </summary>
		public bool CircularRoute
		{
			get{ return isCircularRoute;}
			set{ isCircularRoute = value;}
		}

		/// <summary>
		/// Read-Write property to indicate whether train has false destination
		/// </summary>
		public string FalseDestination
		{
			get{ return sFalseDestination;}
			set{ sFalseDestination = value;}
		}

		/// <summary>
		/// Read-Write property to indicate whether train train is going any via location
		/// </summary>
		public string Via
		{
			get{ return sVia;}
			set{ sVia = value;}
		}

		/// <summary>
		/// Read-Write property to indicate whether train is has been cancel
		/// </summary>
		public bool Cancelled
		{
			get{ return isCancelled;}
			set{ isCancelled = value;}
		}

		/// <summary>
		/// Read-Write property to indicate  cancellation reason
		/// </summary>
		public string CancellationReason
		{
			get{ return sCancellationReason;}
			set{ sCancellationReason = value;}
		}

		/// <summary>
		/// Read-Write property to indicate reason for late running.
		/// </summary>
		public string LateReason
		{
			get{ return sLateReason;}
			set{ sLateReason = value;}
		}

		/// <summary>
		/// Read-Write property to indicate whether it has details train information.
		/// </summary>
		public bool HasTrainDetails
		{
			get
			{return hasTrainDetails;}
			set{hasTrainDetails = value;}
		}
		#endregion

	}
		
}
