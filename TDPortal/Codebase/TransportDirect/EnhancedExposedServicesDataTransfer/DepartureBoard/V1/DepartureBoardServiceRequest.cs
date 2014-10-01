// *********************************************** 
// NAME                 : DepartureBoardServiceRequest.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/12/2005 
// DESCRIPTION  		: Departure Board Service Request Class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/DepartureBoard/V1/DepartureBoardServiceRequest.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:22   mturner
//Initial revision.
//
//   Rev 1.1   Jan 30 2006 14:46:52   schand
//Initialising origin and destination to null explicitly
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Jan 27 2006 16:30:50   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.2   Jan 20 2006 16:24:38   schand
//Added more comments and Serialization attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Dec 22 2005 11:32:00   asinclair
//Check in of Sanjeev's Work In Progress code
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.0   Dec 14 2005 15:38:36   schand
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements

using System;
namespace TransportDirect.EnhancedExposedServices.DataTransfer.DepartureBoard.V1
{
	/// <summary>
	/// Departure Board Service Request Class
	/// </summary>
	[System.Serializable]
	public class DepartureBoardServiceRequest
	{
		#region  Private Members 
		private DepartureBoardServiceLocation originLocation;
		private DepartureBoardServiceLocation destinationLocation;
		private DepartureBoardServiceTimeRequest journeyTime;    
		private DepartureBoardServiceRangeType rangeType;
		private int range;                               
		private bool showDepartures;                     
		private string serviceNumber;                   
		private bool showCallingStops;       
		#endregion        
        
		#region Constructor
		
		/// <summary>
		/// Default constructor to initialise varaible to their default state
		/// </summary>
		public DepartureBoardServiceRequest()
		{   			  
			serviceNumber = string.Empty; 
			rangeType = DepartureBoardServiceRangeType.Sequence;
			originLocation = null;
			destinationLocation = null;
		}
		#endregion

		#region  Public Properties
         		
		/// <summary>
		/// Read-Write property for Origin Location (From)
		/// </summary>
		public DepartureBoardServiceLocation OriginLocation
		{
			get{return originLocation;}
			set{originLocation = value;}
		}
		
		/// <summary>
		/// Read-Write property for Destination Location (To)
		/// </summary>
		public DepartureBoardServiceLocation DestinationLocation
		{
			get{return destinationLocation;}
			set {destinationLocation=value;}
		}

		
		/// <summary>
		/// Read-Write property for Departure Board Service TimeRequest 
		/// </summary>
		public DepartureBoardServiceTimeRequest JourneyTimeInformation
		{
			get{return journeyTime;}
			set{journeyTime=value;}
		}

		/// <summary>
		/// Read-Write property for Departure Board Service Range Type 
		/// </summary>
		public DepartureBoardServiceRangeType RangeType
		{
			get{return rangeType;}
			set{rangeType=value;}
		}

		
		/// <summary>
		/// Read-Write property for Departure Board Service Range 
		/// </summary>
		public int Range
		{
			get{return range;}
			set {range=value;}
		}
		
		/// <summary>
		/// Read-Write property for Departure Board Service deparure or arrival
		/// </summary>
		public bool ShowDepartures
		{
			get{return showDepartures;}
			set {showDepartures =value;}
		}

		
		/// <summary>
		/// Read-Write property for Departure Board Service Number
		/// </summary>
		public string ServiceNumber
		{
			get{return serviceNumber;}
			set {serviceNumber =value;}
		}

		
		/// <summary>
		/// Read-Write property to show Departure Board Service calling points
		/// </summary>
		public bool ShowCallingStops
		{
			get{return showCallingStops;}
			set {showCallingStops=value;}
		}
		#endregion


	}

} 
